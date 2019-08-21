using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject
{

    public enum SeekStatus
    {
        Progress, Pause, Done
    }


    static public class FilesSeek
    {
        static public string CurrentFile { get; private set; }
        static public string CurrentFolder { get; private set; }
        static public SeekStatus Status { get; set; } = SeekStatus.Done;
        static public int CountFindedFiles { get; private set; }
        static public int CurrentIndex { get; private set; } = 0;


        static object locker =new object();

        static List<string> Files { get; set; } = new List<string>();

        static List<string> Folders { get; set; } = new List<string>();

        static Regex Regex { get; set; }

        static int CurrentFileIndex  = 0;
        static int CurrentFolderIndex  = 0;

        static string pattern;
        static string StartPath;


        static public void Prepair(string startPath, string TextToFind, string Pattern)
        {
            switch (Status)
            {
                case SeekStatus.Done:
                    StartPath = startPath;
                    Regex = new Regex(TextToFind);
                    CurrentIndex = 0;
                    CurrentFileIndex = 0;
                    CurrentFolderIndex = 0;
                    CountFindedFiles = 0;
                    pattern = Pattern;
                    Folders.Clear();
                    Folders.Add(startPath);
                    Folders.AddRange(Directory.GetDirectories(StartPath));
                    
                    break;
                case SeekStatus.Progress:
                case SeekStatus.Pause:
                    break;
            }
        }

        static async Task<bool> IsIt(object filename)
        {
            string file;
            using (StreamReader sr = new StreamReader((string)filename))
            {
                file = await sr.ReadToEndAsync(); //File.ReadAllText((string)filename);
            }
            return Regex.IsMatch(file);
            
        }


        static public string Progress(IProgress<string> progress)
        {            

            //Start or continue search
            for (; CurrentFolderIndex < Folders.Count; CurrentFolderIndex++)
            {
                try
                {
                    Files = Directory.GetFiles(Folders[CurrentFolderIndex], pattern).ToList();
                }
                catch(Exception exc)
                {
                    File.AppendAllText("log.txt", $"{DateTime.Now.ToLocalTime().ToString()}:{exc.Message}\r\n");
                    Console.WriteLine($"{DateTime.Now.ToLocalTime().ToString()}:{exc.Message}\r\n");
                    continue;
                }
                for (; CurrentFileIndex < Files.Count; CurrentFileIndex++)
                {
                    CurrentFile = Files[CurrentFileIndex];
                    progress.Report(null);
                    bool isIt = false;
                    switch (Status)
                    {
                        case SeekStatus.Progress:
                            {
                                try
                                {

                                    isIt = Regex.IsMatch(File.ReadAllText(CurrentFile));
                                    var res=Task<Task<bool>>.Factory.StartNew(IsIt, CurrentFile);
                                    res.Wait();
                                    //res.Dispose();
                                    CurrentIndex++;
                                }
                                catch (Exception exc)
                                {
                                    lock(locker)
                                     {
                                        File.AppendAllText("log.txt", $"{DateTime.Now.ToLocalTime().ToString()}:{CurrentFile}:{exc.Message}\r\n");
                                    }
                                }
                                if (isIt)
                                {
                                    string path = Path.GetDirectoryName(CurrentFile);
                                    CountFindedFiles++;
                                    progress.Report(CurrentFile);
                                }
                                else progress.Report(null);
                                break;
                            }
                        case SeekStatus.Pause:
                        case SeekStatus.Done:
                            {
                                progress.Report("Done");
                                return "Done";
                            }
                    }

                }
                CurrentFileIndex = 0;
            }
            Status = SeekStatus.Done;
            progress.Report(null);
            return null;
        }
        
    }
}

    
    

