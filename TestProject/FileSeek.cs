using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace TestProject
{

    public enum SeekStatus
    {
        Progress, Pause, Done, Stopped
    }

    public class FilesSeek
    {


        public Dictionary<string, List<string>> Tree { get; private set; } = new Dictionary<string, List<string>>();

        public List<string> Files { get; private set; }

        public List<string> Folders { get; private set; }

        public int CurrentIndex { get; private set; } = 0;
        Regex Regex { get; set; }

        public string CurrentFile { get; private set; }
        public string CurrentFolder { get; private set; }

        public int CountFindedFiles { get; private set; }

        public string StartPath { get; private set; }
        public int CurrentFileIndex { get; set; } = 0;
        public int CurrentFolderIndex { get; set; } = 0;

        public SeekStatus Status { get; set; } = SeekStatus.Done;

        public int FilesAll { get => Files.Count; }

        string pattern;
        static public string Message { get; private set; } = "";

        public FilesSeek()
        {
            Files = new List<string>();
            Tree = new Dictionary<string, List<string>>();
        }


        void GetFiles(string path, string pattern, List<string> files)
        {
            files.AddRange(Directory.GetFiles(path, pattern));
            string[] dirs = Directory.GetDirectories(path);
            foreach (string dir in dirs)
            {
                try
                {
                    if (Directory.GetDirectories(dir).Length > 0) GetFiles(dir, pattern, files);
                }
                catch (Exception exc)
                {
                    File.AppendAllText("log.txt", $"{DateTime.Now.ToLocalTime().ToString()}:{exc.Message}\r\n");
                }

            }

        }

        List<string> EnumerateFiles(string path, string pattern)
        {
            List<string> list = new List<string>();
            try
            {
                var filesFind = from file in Directory.EnumerateFiles(path,
                            pattern, SearchOption.AllDirectories)
                                select file;

                foreach (var file in filesFind)
                    list.Add(file);
            }
            catch (UnauthorizedAccessException UAEx)
            {
                Console.WriteLine(UAEx.Message);
            }
            catch (PathTooLongException PathEx)
            {
                Console.WriteLine(PathEx.Message);
            }
            catch (AccessViolationException AVEx)
            {
                Console.WriteLine(AVEx.Message);
            }
            catch (IOException IOEx)
            {
                Console.WriteLine(IOEx.Message);
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }
        
            return list;
        }


        public void Start(string startPath, string TextToFind, string Pattern)
        {
            //FoldersToList();
            switch (Status)
            {
                case SeekStatus.Done:
                case SeekStatus.Stopped:
                    this.StartPath = startPath;
                    Regex = new System.Text.RegularExpressions.Regex(TextToFind);
                    CurrentIndex = 0;
                    CurrentFileIndex = 0;
                    CurrentFolderIndex = 0;
                    CountFindedFiles = 0;
                    this.pattern = Pattern;
                    Folders = Directory.GetDirectories(StartPath).ToList();
                    break;
                case SeekStatus.Progress:
                case SeekStatus.Pause:
                    break;
            }
        }

        public string Progress(IProgress<string> progress)
        {            

            //Start or continue search
            for (; CurrentFolderIndex < Folders.Count; CurrentFolderIndex++)
            {
                try
                {
                    Files = Directory.GetFiles(Folders[CurrentFolderIndex], pattern, SearchOption.AllDirectories).ToList();
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
                    CurrentIndex++;
                    progress.Report(null);
                    bool isIt = false;
                    switch (Status)
                    {
                        case SeekStatus.Progress:
                            {
                                try
                                {

                                    isIt = Regex.IsMatch(File.ReadAllText(CurrentFile));
                                }
                                catch (Exception exc)
                                {
                                    File.AppendAllText("log.txt", $"{DateTime.Now.ToLocalTime().ToString()}:{CurrentFile}:{exc.Message}\r\n");
                                }
                                if (isIt)
                                {
                                    string path = Path.GetDirectoryName(CurrentFile);
                                    if (!Tree.ContainsKey(path)) Tree[path] = new List<string>();
                                    Tree[path].Add(Path.GetFileName(Files[CurrentFileIndex]));
                                    CountFindedFiles++;
                                    progress.Report(CurrentFile);
                                }
                                else progress.Report(null);
                                break;
                            }
                        case SeekStatus.Stopped:
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



        public void SetFileSystemTreeView(TreeView tree, string startFolder)
        {
            Files.Clear();
            try
            {
                //string sf = startFolder.Substring(0, startFolder.LastIndexOf('\\'));
                //startFolder = (startFolder.Last<char>()=='\\')?startFolder.TrimEnd('\\'):startFolder;
                TreeNode node = new TreeNode((startFolder.Last<char>() == '\\') ? startFolder.TrimEnd('\\') : startFolder); //sf.Last<char>()==':'?"":sf);
                tree.Nodes.Add(node);
                foreach(string path in Directory.GetDirectories(startFolder))
                    GetFolderList(node, new DirectoryInfo(path));
            }
            catch (Exception exc)
            {
                File.AppendAllText("log.txt", $"{DateTime.Now.ToLocalTime().ToString()}:{exc.Message}\r\n");
                Console.WriteLine($"{DateTime.Now.ToLocalTime().ToString()}:{exc.Message}\r\n");
            }
        }

         void  GetFolderList(TreeNode ParentNode, DirectoryInfo dirInfo)
        {
            TreeNode childNode = new TreeNode(dirInfo.Name);
            ParentNode.Nodes.Add(childNode);
            try
            {
                foreach (var dir in dirInfo.GetDirectories())
                   GetFolderList(childNode, dir);
            }
            catch (Exception exc)
            {
                Console.WriteLine($"{DateTime.Now.ToLocalTime().ToString()}:{exc.Message}\r\n");
            }            
        }
        
    }
}

    
    

