using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TestProject
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public static Progress<string> progress;

        //FilesSeek FilesSeek = new FilesSeek();

        TimeSpan timeSpent;

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
                            btnStartSearch.Enabled = true;
                            btnStop.Enabled = false;
                            btnPause.Enabled = false;
                            timer.Enabled = false;
                            tbTemplate.Enabled = true;
                            tbFindingText.Enabled = true;
                            tbFolder.Enabled = true;
                            timeSpent = TimeSpan.Zero;
                            return;
                        case SeekStatus.Pause:
                            timer.Enabled = false;
                            tsslStatus.Text = "Search paused";
                            return;
                        case SeekStatus.Progress:
                            tsslCurrentFile.Text = FilesSeek.CurrentFile;
                            tsslFileCounter.Text = $"Index:{FilesSeek.CurrentIndex}";
                            tsslFindedFiles.Text = $"Finded:{FilesSeek.CountFindedFiles}";
                            if (info != null)
                            {
                                //lbFindedFiles.Items.Add(info);
                                int getSeparator = info.IndexOf('\\');
                                string tempfilename = info.Substring(getSeparator + 1, info.Length-getSeparator-1);
                                AddFilename(tempfilename, tvFindedFiles);
                                tvFindedFiles.ExpandAll();
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
                //FilesSeek.CurrentFileIndex = 0;
                tsslCurrentFile.Text = "";
                tsslStatus.Text = "";
                tsslTimeSpan.Text = $"Time spent:0";
                Properties.Settings.Default.Save();                
                //lbFindedFiles.Items.Clear();
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
                        btnPause.Enabled = true;
                        btnStartSearch.Enabled = false;
                        FilesSeek.Status = SeekStatus.Progress;
                        timer.Enabled = true;
                        var state = Task.Run(()=>FilesSeek.Progress(progress));
                        var res = await state;
                    }
                    break;
                case SeekStatus.Done:
                    {
                        //Buttons
                        btnStartSearch.Enabled = false;
                        btnStop.Enabled = true;
                        btnPause.Enabled = true;
                        timer.Enabled = true;
                        timeSpent = TimeSpan.Zero;
                        tbFindingText.Enabled = false;
                        tbTemplate.Enabled = false;
                        tbFolder.Enabled = false;
                        btnStartSearch.Enabled = false;
                        TreeViewInit(Path.GetPathRoot(tbFolder.Text), tvFindedFiles);
                        FilesSeek.Prepair(tbFolder.Text, tbFindingText.Text, tbTemplate.Text);
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
            else
                MessageBox.Show("Folder doesn't exists", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeSpent=timeSpent.Add(new TimeSpan(0, 0, 1));   
            tsslTimeSpan.Text = $"Time spent:{(timeSpent)}";
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
            timer.Enabled = false;
            tsslStatus.Text = "Search is stopped";
            FilesSeek.Status = SeekStatus.Done;
            tbFindingText.Enabled = true;
            tbFolder.Enabled = true;
            tbTemplate.Enabled = true;
            btnPause.Enabled = false;
            btnStartSearch.Enabled = true;
            btnStop.Enabled = false;
            
            //timeSpent = 0;

        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            FilesSeek.Status = SeekStatus.Pause;
            tsslStatus.Text = "Search paused";
            btnStartSearch.Enabled = true;
            btnPause.Enabled = false;
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

        private void tbFolder_DoubleClick(object sender, EventArgs e)
        {
            tsmiSetFolder.PerformClick();
        }

        void TreeViewInit(string rootDir, TreeView tree)
        {            
            tree.Nodes.Clear();
            TreeNode root = new TreeNode(rootDir);
            tree.Nodes.Add(root);
        }

        void AddFilename(string filename, TreeView tree)
        {
            TreeNode root = tree.Nodes[0];
            TreeNode node;

            {
                node = root;
                foreach (string pathBits in filename.Split('\\'))
                {
                    node = AddNode(node, pathBits);
                }
            }
        }


        TreeNode AddNode(TreeNode node, string key)
        {
            if (node.Nodes.ContainsKey(key))
            {
                return node.Nodes[key];
            }
            else
            {
                return node.Nodes.Add(key, key);
            }
        }

        static public void AddToTree(TreeNode tree, string filename)
        {
            //TreeNodeCollection nodes = tree.Nodes;
            TreeNode currentNode = tree;
            int separate = filename.IndexOf('\\') - 1;
            string part = filename.Substring(0, separate);
            foreach (TreeNode node in currentNode.Nodes)
            {
                if (node.Text == part) AddToTree(node.NextNode, filename.Substring(separate + 1, filename.Length));
            }
            currentNode.Text = part;
        }

    }

}
