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
        //System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        Progress<Info> progress;
        FilesSeek FilesSeek = new FilesSeek();
        string[] files=null;

        public Form1()
        {
            InitializeComponent();
            progress = new Progress<Info>(
                (info) =>
                {
                    if (FilesSeek.Status==SeekStatus.Done)
                    {
                        tsslStatus.Text = "Search is done";
                        btnStartSeach.Enabled = true;
                        //tsProgressBar.Value = 0;
                        btnStop.Enabled = false;
                        btnPause.Enabled = false;
                        return;
                    }
                    if (FilesSeek.Status==SeekStatus.Pause)
                    {
                        tsslStatus.Text = "Search is pause";
                        return;
                    }
                    tsslTimeSpan.Text = $"Spent:{(DateTime.Now - (DateTime)tsslTimeSpan.Tag).TotalSeconds:F2}";
                    tsProgressBar.Value = info.i;
                    tsslCurrentFile.Text = FilesSeek.CurrentFile;
                    tsslFileCounter.Text = info.i.ToString();
                    if (info.isIt)
                    {
                        tvFindedFiles.Nodes.Add(files[info.i]);
                    }

                }
                );
            tbFindingText.DataBindings.Add("Text", Properties.Settings.Default, "Text");
            tbFolder.DataBindings.Add("Text", Properties.Settings.Default, "Folder");
            tbTemplate.DataBindings.Add("Text", Properties.Settings.Default, "Template");

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
                FilesSeek.Current = 0;
                tsslCurrentFile.Text = "";
                tsslStatus.Text = "";
                tsslTimeSpan.Text = "";
                Properties.Settings.Default.Save();

            }
        }

        string[] LoadList(string Folder, string Pattern,bool AllDirectories)
        {
            string[] files;
            try
            {
                files = Directory.GetFiles(Folder, Pattern, AllDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            return files;
        }

        async void Start()
        {
            switch (FilesSeek.Status)
            {
                case SeekStatus.Pause:
                    {
                        FilesSeek.Status = SeekStatus.Progress;                        
                        var state = await Task.Factory.StartNew<Info>(() => FilesSeek.Start(progress), TaskCreationOptions.LongRunning);
                    }
                    break;
                case SeekStatus.Done:
                    {
                        files = LoadList(tbFolder.Text, tbTemplate.Text, cbAllDirectories.Checked);
                        if (files == null) return;
                        FilesSeek.Current = 0;
                        tvFindedFiles.Nodes.Clear();
                        FilesSeek.Status = SeekStatus.Progress;
                        btnStartSeach.Enabled = false;
                        btnStop.Enabled = true;
                        btnPause.Enabled = true;
                        tsProgressBar.Minimum = 0;
                        tsProgressBar.Value = FilesSeek.Current;
                        tsProgressBar.Maximum = files.Length - 1;
                        FilesSeek.files = files;
                        FilesSeek.textToFind = tbFindingText.Text;
                        tsslStatus.Text = "Search is progress";
                        tsslTimeSpan.Tag = (DateTime)DateTime.Now;
                        var state = await Task.Factory.StartNew<Info>(() => FilesSeek.Start(progress), TaskCreationOptions.LongRunning);
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
    }
}
