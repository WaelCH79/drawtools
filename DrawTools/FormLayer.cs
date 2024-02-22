using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DrawTools
{
    public partial class FormLayer : Form
    {
        private Form1 parent;

        public new Form1 Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public FormLayer(Form1 p)
        {
            Parent = p;
            InitializeComponent();
        }

        public void init()
        {
            Parent.oCnxBase.CBAddComboBox("SELECT GuidApplication, NomApplication FROM Application ORDER BY NomApplication", this.cbGuidApplication, this.cbApplication);
            Parent.oCnxBase.CBAddComboBox("SELECT GuidTemplate, NomTemplate FROM Template ORDER BY NomTemplate", this.cbGuidTemplate, this.cbTemplate);
            this.cbApplication.SelectedIndexChanged += cbApplication_SelectedIndexChanged;
            this.bClose.Click += bClose_Click;
            this.bCreate.Click += bCreate_Click;
            this.bModify.Click += bModify_Click;
            this.bDelete.Click += bDelete_Click;
            this.tbLayerFiltre.TextChanged += tbLayerFiltre_TextChanged;
            this.lbLayer.SelectedIndexChanged += lbLayer_SelectedIndexChanged;
            ShowDialog(Parent);
        }

        void bDelete_Click(object sender, EventArgs e)
        {
            if (lbLayer.SelectedItem != null && tbNomLayer.Text != "")
            {

                string sLayerSave = (string)lbLayer.SelectedItem;
                tbLayerFiltre.Text = "xxxxxx";
                string sField = "Set NomLayer='" + tbNomLayer.Text + "'";
                Parent.oCnxBase.CBWrite("Delete from Layer Where GuidLayer='" + Parent.oCnxBase.GetValueInStringNomGuid(sLayerSave, 1) + "'");
                tbLayerFiltre.Text = "";
                tbNomLayer.Text = "";
            }
            //throw new NotImplementedException();
            //throw new NotImplementedException();
        }

        void bModify_Click(object sender, EventArgs e)
        {
            if (lbLayer.SelectedItem != null && tbNomLayer.Text != "" && cbTemplate.SelectedIndex !=-1)
            {

                string sLayerSave = (string)lbLayer.SelectedItem;
                tbLayerFiltre.Text = "xxxxxx";
                string sField = "Set NomLayer='" + tbNomLayer.Text + "', GuidTemplate='" + (string)cbGuidTemplate.Items[cbTemplate.SelectedIndex] + "'";
                Parent.oCnxBase.CBWrite("Update Layer " + sField + " Where GuidLayer='" + Parent.oCnxBase.GetValueInStringNomGuid(sLayerSave, 1) + "'");
                tbLayerFiltre.Text = Parent.oCnxBase.GetValueInStringNomGuid(sLayerSave, 0)[0] + "";
                tbNomLayer.Text = "";
            }
            //throw new NotImplementedException();
        }

        void lbLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Parent.oCnxBase.CBRecherche("Select NomLayer, GuidTemplate From Layer Where GuidLayer='" + Parent.oCnxBase.GetValueInStringNomGuid((string)lbLayer.SelectedItem, 1) + "'"))
            {
                tbNomLayer.Text = Parent.oCnxBase.Reader.GetString(0);
                cbTemplate.SelectedIndex = cbGuidTemplate.FindString(Parent.oCnxBase.Reader.GetString(1));
            }
            Parent.oCnxBase.CBReaderClose();
            //throw new NotImplementedException();
        }

        void tbLayerFiltre_TextChanged(object sender, EventArgs e)
        {
            lbLayer.Items.Clear();
            if (tbLayerFiltre.Text != "")
                Parent.oCnxBase.CBAddListBox("Select GuidLayer, NomLayer From Layer Where NomLayer Like '" + tbLayerFiltre.Text + "%' and GuidApplication='" + (string)cbGuidApplication.Items[cbApplication.SelectedIndex] + "' Order by NomLayer", lbLayer);
            else
                Parent.oCnxBase.CBAddListBox("Select GuidLayer, NomLayer From Layer Where GuidApplication='" + (string)cbGuidApplication.Items[cbApplication.SelectedIndex] + "' Order by NomLayer", lbLayer);
            //throw new NotImplementedException();
        }

        void bCreate_Click(object sender, EventArgs e)
        {
            if (tbNomLayer.Text != "" && cbApplication.SelectedIndex != -1 && cbTemplate.SelectedIndex != -1)
            {
                string sField = "(GuidLayer, NomLayer, GuidApplication, GuidTemplate)";
                string sValues = "Values ('" + Guid.NewGuid().ToString() + "','" + tbNomLayer.Text + "','" + (string)cbGuidApplication.Items[cbApplication.SelectedIndex] + "','" + (string)cbGuidTemplate.Items[cbTemplate.SelectedIndex] + "')";
                
                Parent.oCnxBase.CBWrite("Insert Into Layer " + sField + " " + sValues);
                tbLayerFiltre.Text = tbNomLayer.Text[0] + "";
                tbNomLayer.Text = "";
            }
            //throw new NotImplementedException();
        }

        void bClose_Click(object sender, EventArgs e)
        {
            Close();
            //throw new NotImplementedException();
        }

        void cbApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbLayer.Items.Clear();
            Parent.oCnxBase.CBAddListBox("Select GuidLayer, NomLayer From Layer Where GuidApplication='" + (string)cbGuidApplication.Items[cbApplication.SelectedIndex] + "' Order by NomLayer", lbLayer);
            //throw new NotImplementedException();
        }
    }
}
