using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DrawTools
{
    
    public partial class FormProgress : Form
    {
        BackgroundWorker oWorker;
        bool bWorker;
        Form parent;

        public BackgroundWorker oWork { get { return oWorker; } }

        public FormProgress(Form fp, bool bworker)
        {
            InitializeComponent();
            parent = fp;
            bWorker = bworker;
            if (bWorker)
            {
                oWorker = new BackgroundWorker();
                oWorker.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
                oWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);
                oWorker.WorkerReportsProgress = true;
                oWorker.WorkerSupportsCancellation = true;
            }
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbTraitement.Value = e.ProgressPercentage;
            lText.Text = (string)e.UserState;
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            /*
            //If it was cancelled midway
            if (e.Cancelled)
            {
                lText.Text = "Traitement annulé.";
            }
            else if (e.Error != null)
            {
                lText.Text = "Il y a eu des erreurs durant le traitement.";
            }
            else
            {
                lText.Text = "Traitement terminé...";
            }*/
            Close();
        }

        public void init()
        {
            if (bWorker) oWorker.RunWorkerAsync();
        }

        public void initbar(int iMax)
        {
            pbTraitement.Minimum = 0;
            pbTraitement.Maximum = iMax-1;
        }

        public void stepbar(string sText, int iwait)
        {
            lText.Text = sText;
            pbTraitement.Increment(1);
            Thread.Sleep(iwait);
            Refresh();
        }

    }

    
}
