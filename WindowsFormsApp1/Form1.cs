using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var progress = new Progress<Info>((info) =>
            {
                label1.Text = info.s1;
                label2.Text = info.s2;
            }
            );

            
            Info result = await Task.Factory.StartNew<Info>(
                                                     () => Worker.SomeLongOperation(progress),
                                                     TaskCreationOptions.LongRunning);

            this.label1.Text = result.s1;
            this.label2.Text = result.s2;
            
        }
    }

    class Info
    {
        public string s1 { get; set; }
        public string s2 { get; set; }
    }

    class Worker
    {
        public static Info SomeLongOperation(IProgress<Info> progress)
        {
            // Perform a long running work...
            for (var i = 0; i < 10; i++)
            {
                Task.Delay(500).Wait();
                progress.Report(new Info() { s1 = $"{i}", s2 = $"{i+1}"});
            }
            return new Info() {s1="0",s2="1" };
        }
    }

}
