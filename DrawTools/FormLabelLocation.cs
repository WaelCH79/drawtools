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
    public partial class FormLabelLocation : Form
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
        public FormLabelLocation(FormLabel p)
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
            cbLocation.SelectedIndexChanged += CbLocation_SelectedIndexChanged;
            cbCloudHosting.SelectedIndexChanged += CbCloudHosting_SelectedIndexChanged;
            cbVendor.SelectedIndexChanged += CbVendor_SelectedIndexChanged;
            cbInfra.SelectedIndexChanged += CbInfra_SelectedIndexChanged;
        }

        private void checkCompletude()
        {
            if (tbPrefix.Text.Length != 0 && cbLocation.SelectedItem != null && cbCloudHosting.SelectedItem != null && cbVendor.SelectedItem != null && cbInfra.SelectedItem != null)
                bAdd.Enabled = true;
            else bAdd.Enabled = false;
        }

        private void CbInfra_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkCompletude();
        }

        private void CbVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkCompletude();
        }

        private void CbCloudHosting_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkCompletude();
        }

        private void CbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkCompletude();
        }

        private void BAdd_Click(object sender, EventArgs e)
        {
            Parent.sLabel = tbPrefix.Text + "_" + cbLocation.SelectedItem + "_" + cbCloudHosting.SelectedItem + "_" + cbVendor.SelectedItem + "_" + cbInfra.SelectedItem;
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
