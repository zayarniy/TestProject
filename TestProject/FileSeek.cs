using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TestProject
{
     public struct Info
    {
        public int i { get; set; }
        public bool? isIt { get; set; }    
 
        public Info(int i,bool? isIt)
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
        //public Dictionary<int, string[]> Files { get; set; }
        public TreeNode CurrentTreeNode { get; private set; }
        public string[] Folders { get; set; }
        public string CurrentFile { get; private set; }
        public string CurrentPath { get; private set; }

        public string textToFind { get; set; }

        public int i=0;

        public int CurrentFileIndex { get; set; } = 0;
        public int CurrentFolderIndex { get; set; } = 0;

//        public void LoadList(string Folder, string Pattern, bool AllDirectories)
//        {
//         //   Dictionary<int, string[]> files = new Dictionary<int, string[]>();
//            try
//            {
//                Folders = Directory.GetDirectories(Folder, "*.", SearchOption.AllDirectories);
//                try
//                {
//                    for(int i=0;i<Folders.Length;CurrentFolderIndex++)
//                        Files.Add(i, Directory.GetFiles(Folder, Pattern, AllDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly));
//                }
//                catch (Exception ex)
//                {
//                    File.AppendAllText("log.txt", ex.Message);
//                }
//            }
//            catch (Exception ex)
//            {
//                File.AppendAllText("log.txt", ex.Message);
//                //return null;
//            }
////            return files;
//        }


        public Info Start(IProgress<Info> progress, TreeNode treeNode)
        {
            try {
                Regex regex = new Regex(textToFind);//Optimize!
                bool isIt = false;
                CurrentTreeNode = treeNode;
                CurrentPath = treeNode.Text;
                string[] files = (string[])treeNode.Tag;
                for (; CurrentFileIndex < files.Length; CurrentFileIndex++)
                {
                    CurrentFile = files[CurrentFileIndex];
                    switch (Status)
                    {
                        case SeekStatus.Progress:
                            {
                                progress.Report(new Info(i, null));
                                isIt = regex.IsMatch(File.ReadAllText(CurrentFile));
                                progress.Report(new Info(i, isIt));
                                break;
                            }
                        case SeekStatus.Pause:
                            {
                                Console.WriteLine("Pause");
                                progress.Report(new Info(i, null));
                                return new Info();
                            }
                        case SeekStatus.Done:
                            {
                                progress.Report(new Info(i, isIt));
                                return new Info();
                            }
                    }
                }
            }
            catch (Exception exc)
            {
                File.AppendAllText("log.txt", $"{DateTime.Now.ToLocalTime().ToString()}:{CurrentFile}:{exc.Message}\r\n");
            }                
            foreach (TreeNode tn in treeNode.Nodes)
                Start(progress, tn);
            Status = SeekStatus.Done;
            progress.Report(new Info(-1, null));
            return new Info();
        }

        //public Info Start(IProgress<Info> progress)
        //{

        //    Regex regex = new Regex(textToFind);
        //    bool isIt = false;
        //    for (;CurrentFolderIndex<Folders.Length;CurrentFolderIndex++)
        //    {
        //        CurrentPath = Folders[i];
        //        for (; CurrentFileIndex < Files[i].Length; CurrentFileIndex++)
        //        {
        //            CurrentFile = Files[CurrentFolderIndex][CurrentFileIndex];
        //            try
        //            {
        //                switch (Status)
        //                {
        //                    case SeekStatus.Progress:
        //                        {
        //                            progress.Report(new Info(i, null));
        //                            isIt = regex.IsMatch(File.ReadAllText(CurrentFile));
        //                            progress.Report(new Info(i, isIt));
        //                            break;
        //                        }

        //                    case SeekStatus.Pause:
        //                        {
        //                            Console.WriteLine("Pause");
        //                            progress.Report(new Info(i, null));
        //                             return new Info();
        //                        }
        //                    case SeekStatus.Done:
        //                        {
        //                            progress.Report(new Info(i, isIt));
        //                            return new Info();
        //                        }
        //                }
        //            }
        //            catch (Exception exc)
        //            {
        //                File.AppendAllText("log.txt", $"{DateTime.Now.ToLocalTime().ToString()}:{CurrentFile}:{exc.Message}\r\n");
        //            }
        //        }
        //    }
        //    Status = SeekStatus.Done;
        //    progress.Report(new Info(-1, null));
        //    return new Info();
        //}
    }
}