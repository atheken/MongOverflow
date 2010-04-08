using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DataLoader
{
    public partial class DataLoader : Form
    {
        public DataLoader()
        {
            InitializeComponent();
            this.loadThread.DoWork += new DoWorkEventHandler(loadThread_DoWork);
            this.loadThread.ProgressChanged += new ProgressChangedEventHandler(loadThread_ProgressChanged);
            this.loadThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(loadThread_RunWorkerCompleted);
        }

        void loadThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.btnLoadData.Enabled = true;
            this.btnSelectPath.Enabled = true;
        }

        

        

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            if (this.browseSourceDir.ShowDialog() == DialogResult.OK)
            {
                this.lblSourcePath.Text = this.browseSourceDir.SelectedPath;
                this.loadThread.RunWorkerAsync(this.lblSourcePath.Text);
            }
        }

        void loadThread_DoWork(object sender, DoWorkEventArgs e)
        {
            var path = sender as String;
            int i = 0;
            while(i < 100)
            {
                this.loadThread.ReportProgress(i);
                Thread.Sleep(250);
                i++;
            }

        }

        void loadThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar.Value = e.ProgressPercentage;
            this.progressBar.Update();
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            this.btnLoadData.Enabled = false;
            this.btnSelectPath.Enabled = false;
            this.loadThread.RunWorkerAsync();
        }
    }
}
