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
    public partial class FormLabelApp : Form
    {
        private FormLabel parent;
        private bool bOk;

        public new FormLabel Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public Boolean Valider
        {
            get { return bOk; }
        }
        public FormLabelApp(FormLabel p)
        {
            Parent = p;
            bOk = false;

            InitializeComponent();
            InitData();
            InitEvent();
        }

        public void InitData()
        {
            Parent.Parent.oCnxBase.CBAddComboBox("SELECT GuidApplication, NomApplication FROM Application ORDER BY NomApplication", this.cbGuidApplication, this.cbApplication);
        }

        public void InitEvent()
        {
            bAnnuler.Click += BAnnuler_Click;
            bAdd.Click += BAdd_Click;
            cbApplication.SelectedIndexChanged += CbApplication_SelectedIndexChanged;
            tbIteration.TextChanged += TbIteration_TextChanged;
        }

        private void TbIteration_TextChanged(object sender, EventArgs e)
        {
            checkCompletude();
        }

        private void checkCompletude()
        {
            if (tbPrefix.Text.Length != 0 && tbCodeAP.Text.Length !=0 && tbTrigramme.Text.Length != 0 && tbIteration.Text.Length != 0)
                bAdd.Enabled = true;
            else bAdd.Enabled = false;
        }

        private void CbApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbApplication.SelectedItem != null)
            {
                if(Parent.Parent.oCnxBase.CBRecherche("SELECT CodeAp, Trigramme FROM Application WHERE GuidApplication='" + cbGuidApplication.Items[cbApplication.SelectedIndex] + "'"))
                {
                    tbCodeAP.Text = Parent.Parent.oCnxBase.Reader.IsDBNull(0) ? "" :  Parent.Parent.oCnxBase.Reader.GetString(0);
                    tbTrigramme.Text = Parent.Parent.oCnxBase.Reader.GetString(1);
                }
                Parent.Parent.oCnxBase.CBReaderClose();

            }
            checkCompletude();
        }

        private void BAdd_Click(object sender, EventArgs e)
        {
            Parent.sLabel = tbPrefix.Text + "_" + tbCodeAP.Text + "-ABPI-" + tbTrigramme.Text + "_" + tbIteration.Text;
            bOk = true;
            Close();
        }

        private void BAnnuler_Click(object sender, EventArgs e)
        {
            bOk = false;
            Close();
        }
    }
}
