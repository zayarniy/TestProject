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

namespace TestProject
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public static Progress<string> progress;

        FilesSeek FilesSeek = new FilesSeek();

        int timeSpent;

        public Form1()
        {
            InitializeComponent();
            tbFindingText.DataBindings.Add("Text", Properties.Settings.Default, "Text");
            tbFindingText.DataBindings[0].ControlUpdateMode = ControlUpdateMode.OnPropertyChanged;
            tbFolder.DataBindings.Add("Text", Properties.Settings.Default, "Folder");
            tbFolder.DataBindings[0].ControlUpdateMode = ControlUpdateMode.OnPropertyChanged;
            tbTemplate.DataBindings.Add("Text", Properties.Settings.Default, "Template");
            tbTemplate.DataBindings[0].ControlUpdateMode = ControlUpdateMode.OnPropertyChanged;

            timer.Interval = 1000;
            timer.Tick += Timer_Tick;

            progress = new Progress<string>(
                (info) =>
                {
                    switch (FilesSeek.Status)
                    {
                        case SeekStatus.Done:
                            tsslStatus.Text = "Search done";
                            tsslCurrentFile.Text = "";
                            btnStartSeach.Enabled = true;
                            //tsProgressBar.Value = 0;
                            btnStop.Enabled = false;
                            btnPause.Enabled = false;
                            timer.Enabled = false;
                            tbTemplate.Enabled = true;
                            tbFindingText.Enabled = true;
                            tbFolder.Enabled = true;
                            timeSpent = 0;
                            //tvFindedFiles.Refresh();
                            return;
                        case SeekStatus.Pause:
                            tsslStatus.Text = "Search paused";
                            timer.Enabled = false;
                            return;
                        case SeekStatus.Progress:
                            tsslCurrentFile.Text = FilesSeek.CurrentFile;
                            //tsProgressBar.Value = FilesSeek.CurrentIndex;
                            tsslFileCounter.Text = FilesSeek.CurrentIndex.ToString();
                            tsslFindedFiles.Text = FilesSeek.CountFindedFiles.ToString();
                            if (info != null)
                                lbFindedFiles.Items.Add(info);
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
                //FilesSeek.CurrentFileIndex = 0;
                tsslCurrentFile.Text = "";
                tsslStatus.Text = "";
                tsslTimeSpan.Text = "0";
                Properties.Settings.Default.Save();
                tvFindedFiles.Nodes.Clear();
                lbFindedFiles.Items.Clear();
                FilesSeek.SetFileSystemTreeView(tvFindedFiles, tbFolder.Text);
            }
        }

        async void Start()
        {
            //Status - before Start
            switch (FilesSeek.Status)
            {
                case SeekStatus.Pause:
                    {
                        tsslStatus.Text = "Search in progress";
                        FilesSeek.Status = SeekStatus.Progress;
                        timer.Enabled = true;
                        var state = Task.Run(()=>FilesSeek.Progress(progress));
                        var res = await state;
                    }
                    break;
                case SeekStatus.Stopped:
                case SeekStatus.Done:
                    {
                        //Buttons
                        btnStartSeach.Enabled = false;
                        btnStop.Enabled = true;
                        btnPause.Enabled = true;
                        timer.Enabled = true;
                        timeSpent = 0;
                        tvFindedFiles.Nodes.Clear();
                        lbFiles.Items.Clear();
                        tbFindingText.Enabled = false;
                        tbTemplate.Enabled = false;
                        tbFolder.Enabled = false;
                        lbFindedFiles.Items.Clear();

                        //FilesSeek.SetFileSystemTreeView(tvFindedFiles, tbFolder.Text);

                        FilesSeek.Start(tbFolder.Text, tbFindingText.Text, tbTemplate.Text);

                        tsProgressBar.Minimum = 0;
                        tsProgressBar.Value = 0;
                        tsProgressBar.Maximum = FilesSeek.FilesAll;
                        tsslAllFiles.Text = FilesSeek.FilesAll.ToString();
                        tsslFindedFiles.Text = "0";
                        tsslStatus.Text = "Search in progress";
                        FilesSeek.Status = SeekStatus.Progress;

                        var state = Task.Run<string>(() => FilesSeek.Progress(progress));
                        var res = await state;
                        break;
                    }
                case SeekStatus.Progress:
                    timer.Enabled = false;
                    
                    break;
            }

        }

        private void btnStartSeach_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(tbFolder.Text))
            {
                Start();                                
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tsslTimeSpan.Text = (++timeSpent).ToString();
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
            tbFindingText.Enabled = true;
            tbFolder.Enabled = true;
            tbTemplate.Enabled = true;
            timeSpent = 0;

        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            FilesSeek.Status = SeekStatus.Pause;
            btnStartSeach.Enabled = true;
            timer.Enabled = false;

        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            TestProject.Properties.Settings.Default.Save();
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
            //FilesSeek.SetFileSystemTreeView(tvFindedFiles, tbFolder.Text);

        }

        private void tvFindedFiles_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tsslFolder.Text = e.Node.FullPath;
        }

        private void tvFindedFiles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            tsslFolder.Text = e.Node.FullPath;
            lbFiles.Items.Clear();
            if (FilesSeek.Tree.ContainsKey(tsslFolder.Text))
                lbFiles.Items.AddRange(FilesSeek.Tree[tsslFolder.Text].ToArray());
        }

        private void tsmiReadListOfFolder_Click(object sender, EventArgs e)
        {
            FilesSeek.SetFileSystemTreeView(tvFindedFiles, tbFolder.Text);
        }
    }
}
