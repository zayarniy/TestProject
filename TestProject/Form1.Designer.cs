namespace TestProject
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSetFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLogShow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClearLogFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslTimeSpan = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tsslFileCounter = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslCurrentFile = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvFindedFiles = new System.Windows.Forms.TreeView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTemplate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFindingText = new System.Windows.Forms.TextBox();
            this.btnStartSeach = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.cbAllDirectories = new System.Windows.Forms.CheckBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tsslFolder = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1534, 42);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSetFolder,
            this.tsmiSave,
            this.tsmiLogShow,
            this.tsmiClearLogFile,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(64, 36);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // tsmiSetFolder
            // 
            this.tsmiSetFolder.Name = "tsmiSetFolder";
            this.tsmiSetFolder.Size = new System.Drawing.Size(210, 36);
            this.tsmiSetFolder.Text = "Set folder";
            this.tsmiSetFolder.Click += new System.EventHandler(this.tsmiSetFolder_Click);
            // 
            // tsmiSave
            // 
            this.tsmiSave.Name = "tsmiSave";
            this.tsmiSave.Size = new System.Drawing.Size(210, 36);
            this.tsmiSave.Text = "Save";
            this.tsmiSave.Click += new System.EventHandler(this.tsmiSave_Click);
            // 
            // tsmiLogShow
            // 
            this.tsmiLogShow.Name = "tsmiLogShow";
            this.tsmiLogShow.Size = new System.Drawing.Size(210, 36);
            this.tsmiLogShow.Text = "Show log";
            this.tsmiLogShow.Click += new System.EventHandler(this.tsmiLogShow_Click);
            // 
            // tsmiClearLogFile
            // 
            this.tsmiClearLogFile.Name = "tsmiClearLogFile";
            this.tsmiClearLogFile.Size = new System.Drawing.Size(210, 36);
            this.tsmiClearLogFile.Text = "Clear log";
            this.tsmiClearLogFile.Click += new System.EventHandler(this.tsmiClearLogFile_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(207, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(210, 36);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslTimeSpan,
            this.tsslStatus,
            this.tsProgressBar,
            this.tsslFileCounter,
            this.tsslCurrentFile,
            this.tsslFolder});
            this.statusStrip1.Location = new System.Drawing.Point(0, 912);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1534, 31);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslTimeSpan
            // 
            this.tsslTimeSpan.Name = "tsslTimeSpan";
            this.tsslTimeSpan.Size = new System.Drawing.Size(0, 26);
            // 
            // tsslStatus
            // 
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(0, 26);
            // 
            // tsProgressBar
            // 
            this.tsProgressBar.Name = "tsProgressBar";
            this.tsProgressBar.Size = new System.Drawing.Size(300, 25);
            // 
            // tsslFileCounter
            // 
            this.tsslFileCounter.Name = "tsslFileCounter";
            this.tsslFileCounter.Size = new System.Drawing.Size(0, 26);
            // 
            // tsslCurrentFile
            // 
            this.tsslCurrentFile.Name = "tsslCurrentFile";
            this.tsslCurrentFile.Size = new System.Drawing.Size(0, 26);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 42);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvFindedFiles);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(1534, 870);
            this.splitContainer1.SplitterDistance = 928;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 3;
            // 
            // tvFindedFiles
            // 
            this.tvFindedFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvFindedFiles.Location = new System.Drawing.Point(0, 0);
            this.tvFindedFiles.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tvFindedFiles.Name = "tvFindedFiles";
            this.tvFindedFiles.Size = new System.Drawing.Size(928, 870);
            this.tvFindedFiles.TabIndex = 0;
            this.tvFindedFiles.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFindedFiles_AfterSelect);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.tbFolder);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.tbTemplate);
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.tbFindingText);
            this.flowLayoutPanel1.Controls.Add(this.btnStartSeach);
            this.flowLayoutPanel1.Controls.Add(this.btnPause);
            this.flowLayoutPanel1.Controls.Add(this.btnStop);
            this.flowLayoutPanel1.Controls.Add(this.cbAllDirectories);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(600, 870);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Starpath";
            // 
            // tbFolder
            // 
            this.flowLayoutPanel1.SetFlowBreak(this.tbFolder, true);
            this.tbFolder.Location = new System.Drawing.Point(4, 34);
            this.tbFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbFolder.Name = "tbFolder";
            this.tbFolder.ReadOnly = true;
            this.tbFolder.Size = new System.Drawing.Size(592, 35);
            this.tbFolder.TabIndex = 1;
            this.tbFolder.Text = "D:\\Temp\\";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 74);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 29);
            this.label2.TabIndex = 2;
            this.label2.Text = "File template";
            // 
            // tbTemplate
            // 
            this.flowLayoutPanel1.SetFlowBreak(this.tbTemplate, true);
            this.tbTemplate.Location = new System.Drawing.Point(4, 108);
            this.tbTemplate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbTemplate.Name = "tbTemplate";
            this.tbTemplate.Size = new System.Drawing.Size(592, 35);
            this.tbTemplate.TabIndex = 3;
            this.tbTemplate.Text = "*.*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 148);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 29);
            this.label3.TabIndex = 4;
            this.label3.Text = "Text to find";
            // 
            // tbFindingText
            // 
            this.tbFindingText.Location = new System.Drawing.Point(4, 182);
            this.tbFindingText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbFindingText.Multiline = true;
            this.tbFindingText.Name = "tbFindingText";
            this.tbFindingText.Size = new System.Drawing.Size(572, 175);
            this.tbFindingText.TabIndex = 5;
            this.tbFindingText.Text = "0";
            // 
            // btnStartSeach
            // 
            this.btnStartSeach.Location = new System.Drawing.Point(4, 367);
            this.btnStartSeach.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStartSeach.Name = "btnStartSeach";
            this.btnStartSeach.Size = new System.Drawing.Size(112, 35);
            this.btnStartSeach.TabIndex = 6;
            this.btnStartSeach.Text = "Start";
            this.btnStartSeach.UseVisualStyleBackColor = true;
            this.btnStartSeach.Click += new System.EventHandler(this.btnStartSeach_Click);
            // 
            // btnPause
            // 
            this.btnPause.Enabled = false;
            this.btnPause.Location = new System.Drawing.Point(124, 367);
            this.btnPause.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(112, 35);
            this.btnPause.TabIndex = 9;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(244, 367);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(112, 35);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // cbAllDirectories
            // 
            this.cbAllDirectories.AutoSize = true;
            this.cbAllDirectories.CheckAlign = System.Drawing.ContentAlignment.BottomRight;
            this.cbAllDirectories.Checked = true;
            this.cbAllDirectories.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAllDirectories.Location = new System.Drawing.Point(364, 367);
            this.cbAllDirectories.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbAllDirectories.Name = "cbAllDirectories";
            this.cbAllDirectories.Size = new System.Drawing.Size(189, 33);
            this.cbAllDirectories.TabIndex = 8;
            this.cbAllDirectories.Text = "All Directories";
            this.cbAllDirectories.UseVisualStyleBackColor = true;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // tsslFolder
            // 
            this.tsslFolder.Name = "tsslFolder";
            this.tsslFolder.Size = new System.Drawing.Size(88, 26);
            this.tsslFolder.Text = "tsslFolder";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1534, 943);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "File selector";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiSave;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiSetFolder;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvFindedFiles;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbTemplate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFindingText;
        private System.Windows.Forms.ToolStripStatusLabel tsslFileCounter;
        private System.Windows.Forms.ToolStripStatusLabel tsslTimeSpan;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.Windows.Forms.ToolStripStatusLabel tsslCurrentFile;
        private System.Windows.Forms.Button btnStartSeach;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ToolStripMenuItem tsmiLogShow;
        private System.Windows.Forms.CheckBox cbAllDirectories;
        private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
        private System.Windows.Forms.ToolStripMenuItem tsmiClearLogFile;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.ToolStripStatusLabel tsslFolder;
    }
}

