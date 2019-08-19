using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using Helper;

namespace TestProject
{
    public partial class Form1 : Form
    {
        //System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        Progress<Info> progress;
        FilesSeek FilesSeek = new FilesSeek();
        Dictionary<string, string[]> files =null;

        public Form1()
        {
            InitializeComponent();
            tbFindingText.DataBindings.Add("Text", Properties.Settings.Default, "Text");
            tbFolder.DataBindings.Add("Text", Properties.Settings.Default, "Folder");
            tbTemplate.DataBindings.Add("Text", Properties.Settings.Default, "Template");
            Helper.Help.SetFileSystemTreeView(tvFindedFiles,"C:\\System");
            progress = new Progress<Info>(
                (info) =>
                {
                    switch (FilesSeek.Status)
                    {
                        case SeekStatus.Done:
                            tsslStatus.Text = "Search is done";
                            btnStartSeach.Enabled = true;
                            //tsProgressBar.Value = 0;
                            btnStop.Enabled = false;
                            btnPause.Enabled = false;
                            return;
                        case SeekStatus.Pause:
                            tsslStatus.Text = "Search pause";
                            return;
                        case SeekStatus.Progress:
                            tsslTimeSpan.Text = $"Spent:{(DateTime.Now - (DateTime)tsslTimeSpan.Tag).TotalSeconds:F0}";
                            if (info.isIt == null)
                            {
                                tsslCurrentFile.Text = FilesSeek.CurrentFile;
                                tsProgressBar.Value = info.i;
                                tsslFileCounter.Text = info.i.ToString();

                            }
                            else
                            if ((bool)info.isIt)
                            {
                                tvFindedFiles.Nodes.Add(FilesSeek.CurrentFile);
                            }
                            break;
                    }
                }
                );

        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmiSetFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog()==DialogResult.OK)
            {
                tbFolder.Text = fbd.SelectedPath;
                FilesSeek.CurrentFileIndex = 0;
                tsslCurrentFile.Text = "";
                tsslStatus.Text = "";
                tsslTimeSpan.Text = "";
                Properties.Settings.Default.Save();
                tvFindedFiles.Nodes.Clear();
                Helper.Help.SetFileSystemTreeView(tvFindedFiles, tbFolder.Text);
            }
        }

        async void Start()
        {
            switch (FilesSeek.Status)
            {
                case SeekStatus.Pause:
                    {
                        FilesSeek.Status = SeekStatus.Progress;

                        //Task<Info> state =Task.Factory.StartNew<Info>(() => FilesSeek.Start(progress), TaskCreationOptions.LongRunning);
                        var state = Task.Run<Info>(()=>FilesSeek.Start(progress,tvFindedFiles.Nodes[0]));
                        var res = await state;
                    }
                    break;
                case SeekStatus.Done:
                    {
                        //FilesSeek.LoadList(tbFolder.Text, tbTemplate.Text, cbAllDirectories.Checked);
                        Helper.Help.SetFileSystemTreeView(tvFindedFiles, tbFolder.Text);
                        //if (files == null) return;
                        FilesSeek.CurrentFileIndex = 0;
                        tvFindedFiles.Nodes.Clear();
                        FilesSeek.Status = SeekStatus.Progress;
                        btnStartSeach.Enabled = false;
                        btnStop.Enabled = true;
                        btnPause.Enabled = true;
                        tsProgressBar.Minimum = 0;
                        tsProgressBar.Value = FilesSeek.CurrentFileIndex;
                        tsProgressBar.Maximum =  files[FilesSeek.CurrentPath].Length - 1;
                        //FilesSeek.filses = files;
                        FilesSeek.textToFind = tbFindingText.Text;
                        tsslStatus.Text = "Search is progress";
                        tsslTimeSpan.Tag = (DateTime)DateTime.Now;
                        var state = await Task.Factory.StartNew<Info>(() => FilesSeek.Start(progress,tvFindedFiles.Nodes[FilesSeek.CurrentPath]), TaskCreationOptions.LongRunning);
                        break;
                    }
                case SeekStatus.Progress:
                    break;
            }

        }

        private void btnStartSeach_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(tbFolder.Text))
            {
                tvFindedFiles.Nodes.Clear();
                Start();                                
            }
        }

        private void tsmiLogShow_Click(object sender, EventArgs e)
        {
            if (File.Exists("log.txt")) System.Diagnostics.Process.Start("log.txt");
            else MessageBox.Show("Log doesn't exists");
        }

        private void tsmiClearLogFile_Click(object sender, EventArgs e)
        {
            if (File.Exists("log.txt")) File.Delete("log.txt");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            tsslStatus.Text = "Search is stopped";
            FilesSeek.Status = SeekStatus.Done;
            

        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            FilesSeek.Status = SeekStatus.Pause;
            //timer.Stop();
            btnStartSeach.Enabled = true;

        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            //string result = FilesSeek.Save("param.xml");
            //if (result != String.Empty) MessageBox.Show(result);
            TestProject.Properties.Settings.Default.Save();
        }

        private void tsmiLoad_Click(object sender, EventArgs e)
        {
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            TestProject.Properties.Settings.Default.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TestProject.Properties.Settings.Default.Reload();
            tbFindingText.Text = Properties.Settings.Default.Text;
            tbFolder.Text = Properties.Settings.Default.Folder;
            tbTemplate.Text = Properties.Settings.Default.Template;
        }

        private void tvFindedFiles_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tsslFolder.Text = e.Node.FullPath;
        }

        private void tvFindedFiles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            tsslFolder.Text = e.Node.FullPath;
        }
    }
}
