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
    public partial class FormLabelRole : Form
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

        public FormLabelRole(FormLabel p)
        {
            Parent = p;
            bOk = false;

            InitializeComponent();
            InitEvent();
        }
        public void InitEvent()
        {
            bAnnuler.Click += BAnnuler_Click;
            bAdd.Click += BAdd_Click;
            cbExposition.SelectedIndexChanged += CbExposition_SelectedIndexChanged;
            cbApp.SelectedIndexChanged += CbApp_SelectedIndexChanged;
            cbOS.SelectedIndexChanged += CbOS_SelectedIndexChanged;
            cbSoftware.SelectedIndexChanged += CbSoftware_SelectedIndexChanged;

        }

        private void checkCompletude()
        {
            if (tbPrefix.Text.Length != 0 && cbExposition.SelectedItem != null && cbApp.SelectedItem != null && cbOS.SelectedItem != null && cbSoftware.SelectedItem != null)
                bAdd.Enabled = true;
            else bAdd.Enabled = false;
        }

        private void CbSoftware_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkCompletude();
        }

        private void CbOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkCompletude();
        }

        private void CbApp_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkCompletude();
        }

        private void CbExposition_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkCompletude();
        }

        private void BAdd_Click(object sender, EventArgs e)
        {
            Parent.sLabel = tbPrefix.Text + "_" + cbExposition.SelectedItem + "_" + cbApp.SelectedItem + "_" + cbOS.SelectedItem + "_" + cbSoftware.SelectedItem;
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
