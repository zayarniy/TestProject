using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace TestProject
{
     public struct Info
    {
        public int i { get; set; }
        public bool isIt { get; set; }                
        public Info(int i,bool isIt)
        {
            this.i = i;
            this.isIt = isIt;            
        }

    }

    public enum SeekStatus
    {
        Progress, Pause, Done
    }

    public class FilesSeek
    {

        public SeekStatus Status { get; set; } = SeekStatus.Done;

        public string[] files { get; set; }
        public string CurrentFile { get; private set; }

        public string textToFind { get; set; }

        //public bool Pause { get; set; } = false;
        public int i=0;

        public int Current { get; set; }
        //public bool Done { get; set; } = false;

        public FilesSeek()//For Serialization
        {

        }

         public Info Start(IProgress<Info> progress)
        {
            Regex regex = new Regex(textToFind);
            bool isIt = false;
            for (i = Current; i < files.Length; i++)
            {
                try
                {
                    switch (Status)
                    {
                        case SeekStatus.Progress:
                            {
                                CurrentFile = files[i];
                                progress.Report(new Info(i, isIt));
                                isIt = regex.IsMatch(File.ReadAllText(files[i]));
                                break;
                            }

                        case SeekStatus.Pause:
                            {
                                Current = i;
                                Console.WriteLine("Pause");
                                progress.Report(new Info(i, isIt));
                                return new Info();
                            }
                        case SeekStatus.Done:
                            {
                                progress.Report(new Info(i, isIt));
                                return new Info();
                            }


                    }
                }
                catch (Exception exc)
                {
                    File.AppendAllText("log.txt", $"{DateTime.Now.ToLocalTime().ToString()}:{files[i]}:{exc.Message}\r\n");
                }

            }
            
            progress.Report(new Info(files.Length-1, false));
            Status = SeekStatus.Done;
            return new Info();

        }


    }
}
