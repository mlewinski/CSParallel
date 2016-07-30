using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BackgroundWorkerReader_DoWork(object sender, DoWorkEventArgs e)
        {
            const int dataCountToRead = 100;
            const int msDelayTime = 50;
            Random r = new Random();
            for (int i = 0; i < dataCountToRead; i++)
            {
                Thread.Sleep(msDelayTime);
                if (BackgroundWorkerReader.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    BackgroundWorkerReader.ReportProgress(100*i/dataCountToRead,r.Next(255));
                }
            }
        }

        private void BackgroundWorkerReader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                lbData.Items.Add(e.UserState);
                lbData.SelectedIndex = lbData.Items.Count - 1;
            }
            Progress.Value = e.ProgressPercentage;
        }

        private void BackgroundWorkerReader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ConfigureButtonState(false);
        }

        private void ConfigureButtonState(bool threadActive)
        {
            btnStart.Enabled = !threadActive;
            btnStop.Enabled = threadActive;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            BackgroundWorkerReader.RunWorkerAsync();
            ConfigureButtonState(true);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if(BackgroundWorkerReader.IsBusy) BackgroundWorkerReader.CancelAsync();
            ConfigureButtonState(false);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            lbData.Items.Clear();
        }
    }
}
