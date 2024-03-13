using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DrawTools
{
    public partial class FormGroupService : Form
    {
        private Form1 parent;


        public new Form1 Parent
        {
            get { return parent; }
            set { parent = value;}
        }

        
        void tbGroupServiceFiltre_TextChanged(object sender, System.EventArgs e)
        {
            lbGroupService.Items.Clear();
            if (tbGroupServiceFiltre.Text != "")
                Parent.oCnxBase.CBAddListBox("Select GuidGroupService, NomGroupService From GroupService Where NomGroupService Like '%" + tbGroupServiceFiltre.Text + "%' Order by NomGroupService", lbGroupService);
            else
                Parent.oCnxBase.CBAddListBox("Select GuidGroupService, NomGroupService From GroupService Order by NomGroupService", lbGroupService);
        }

        void lbGroupService_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (Parent.oCnxBase.CBRecherche("Select NomGroupService, GuidFonctionService From GroupService Where GuidGroupService='" + Parent.oCnxBase.GetValueInStringNomGuid((string)lbGroupService.SelectedItem, 1) + "'"))
            {
                tbNomGroupService.Text = Parent.oCnxBase.Reader.GetString(0);
                if(!Parent.oCnxBase.Reader.IsDBNull(1))
                {
                    int idx = cbGuidFonctionService.FindString(Parent.oCnxBase.Reader.GetString(1));
                    cbNomFonctionService.SelectedIndex = idx;
                }
            }
            Parent.oCnxBase.CBReaderClose();
        }

        public FormGroupService(Form1 p)
        {
            Parent = p;
            InitializeComponent();
        }


        public void init()
        {
            Parent.oCnxBase.CBAddListBox("Select GuidGroupService, NomGroupService From GroupService Order by NomGroupService", lbGroupService);
            Parent.oCnxBase.CBAddComboBox("Select GuidFonctionService, NomFonctionService From FonctionService Order by NomFonctionService", cbGuidFonctionService, cbNomFonctionService);

            this.bCreate.Click += bCreate_Click;
            this.bModify.Click += bModify_Click;
            this.bDelete.Click += bDelete_Click;
            this.bClose.Click += bClose_Click;
            this.lbGroupService.SelectedIndexChanged += lbGroupService_SelectedIndexChanged;
            this.tbGroupServiceFiltre.TextChanged += tbGroupServiceFiltre_TextChanged;
            ShowDialog(Parent);
        }


        private void bCreate_Click(object sender, EventArgs e)
        {
            if (tbNomGroupService.Text != "") 
            {
                tbGroupServiceFiltre.Text = "xxxxxx";
                Parent.oCnxBase.CBWrite("Insert Into GroupService (GuidGroupService, NomGroupService) Values ('" + Guid.NewGuid().ToString() + "','" + tbNomGroupService.Text + "')");
                tbGroupServiceFiltre.Text = tbNomGroupService.Text[0] + "";
                tbNomGroupService.Text = "";
            }

        }

        private void bModify_Click(object sender, EventArgs e)
        {
            if (lbGroupService.SelectedItem != null && tbNomGroupService.Text != "")
            {

                string sGroupServiceSave = (string)lbGroupService.SelectedItem;
                tbGroupServiceFiltre.Text = "xxxxxx";
                if (cbNomFonctionService.SelectedIndex == -1)
                {
                    Parent.oCnxBase.CBWrite("Update GroupService Set NomGroupService = '" + tbNomGroupService.Text + "', GuidFonctionService = null Where GuidGroupService='" + Parent.oCnxBase.GetValueInStringNomGuid(sGroupServiceSave, 1) + "'");
                }
                else
                {
                    Parent.oCnxBase.CBWrite("Update GroupService Set NomGroupService = '" + tbNomGroupService.Text + "', GuidFonctionService = '" + cbGuidFonctionService.Items[cbNomFonctionService.SelectedIndex] + "' Where GuidGroupService='" + Parent.oCnxBase.GetValueInStringNomGuid(sGroupServiceSave, 1) + "'");
                }
                tbGroupServiceFiltre.Text = Parent.oCnxBase.GetValueInStringNomGuid(sGroupServiceSave, 0)[0] + "";
                tbNomGroupService.Text = "";

            }
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            if (lbGroupService.SelectedItem != null)
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show("Etes-vous sur de supprimer cet objet.\nToutes les références seront supprimées.", "suppression", buttons);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    string sGroupServiceSave = (string)lbGroupService.SelectedItem;
                    tbGroupServiceFiltre.Text = "xxxxxx";
                    Parent.oCnxBase.CBWrite("Delete From GroupService Where GuidGroupService='" + Parent.oCnxBase.GetValueInStringNomGuid(sGroupServiceSave, 1) + "'");
                    tbGroupServiceFiltre.Text = Parent.oCnxBase.GetValueInStringNomGuid(sGroupServiceSave, 0)[0] + "";
                    tbNomGroupService.Text = "";
                }
            }
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
