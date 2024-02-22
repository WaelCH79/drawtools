using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawTools
{
    public partial class FormLabelEnv : Form
    {
        private FormLabel parent;
        private string sPrefixlabel = "E_";
        private bool bOk;
        private string sLabel;

        public new FormLabel Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public Boolean Valider
        {
            get { return bOk; }
        }

        public string sRetourLabel
        {
            get { return sLabel; }
        }
        public FormLabelEnv(FormLabel p)
        {
            Parent = p;
            bOk = false;

            InitializeComponent();
            tbLabel.Text = sPrefixlabel;
            InitEvent();
        }

        public void InitEvent()
        {
            bAnnuler.Click += BAnnuler_Click;
            bAdd.Click += BOk_Click;
            cbEnv.SelectedIndexChanged += CbEnv_SelectedIndexChanged;
            
        }

        private void BOk_Click(object sender, EventArgs e)
        {
            sLabel = tbLabel.Text;
            bOk = true;
            Close();
            //throw new NotImplementedException();
        }

        private void CbEnv_SelectedIndexChanged(object sender, EventArgs e)
        {
            bAdd.Enabled = true;
            tbLabel.Text = sPrefixlabel + cbEnv.SelectedItem;
            //throw new NotImplementedException();
        }

        private void BAnnuler_Click(object sender, EventArgs e)
        {
            sLabel = "";
            bOk = false;
            Close();
            //throw new NotImplementedException();
        }
    }
}
