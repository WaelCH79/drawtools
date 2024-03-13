using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DrawTools
{
    public partial class FormService : Form
    {
        private Form1 parent;


        public new Form1 Parent
        {
            get { return parent; }
            set { parent = value;}
        }

        public FormService(Form1 p)
        {
            Parent = p;
            InitializeComponent();
            Parent.oCnxBase.CBAddListBox("Select GuidService, NomService From Service Order by NomService", lbService);
        }

        void tbServiceFiltre_TextChanged(object sender, System.EventArgs e)
        {
            lbService.Items.Clear();
            if (tbServiceFiltre.Text != "")
                Parent.oCnxBase.CBAddListBox("Select GuidService, NomService From Service Where NomService Like '%" + tbServiceFiltre.Text + "%' Order by NomService", lbService);
            else
                Parent.oCnxBase.CBAddListBox("Select GuidService, NomService From Service Order by NomService", lbService);
        }

        void lbService_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (Parent.oCnxBase.CBRecherche("Select NomService, InfoSup, Protocole, Ports, Description From Service Where GuidService='" + Parent.oCnxBase.GetValueInStringNomGuid((string)lbService.SelectedItem, 1) + "'"))
            {
                tbNomService.Text = Parent.oCnxBase.Reader.GetString(0);
                if (!Parent.oCnxBase.Reader.IsDBNull(1)) tbInfoSup.Text = Parent.oCnxBase.Reader.GetString(1); else tbInfoSup.Text = "";
                if (!Parent.oCnxBase.Reader.IsDBNull(2)) tbProtocole.Text = Parent.oCnxBase.Reader.GetString(2); else tbProtocole.Text = "";
                if (!Parent.oCnxBase.Reader.IsDBNull(3)) tbPorts.Text = Parent.oCnxBase.Reader.GetString(3); else tbPorts.Text = "";
                if (!Parent.oCnxBase.Reader.IsDBNull(4)) tbDescription.Text = Parent.oCnxBase.Reader.GetString(4); else tbDescription.Text = ""; 
            }
            Parent.oCnxBase.CBReaderClose();
        }

        private void bCreate_Click(object sender, EventArgs e)
        {
            if (tbNomService.Text != "" && tbProtocole.Text!="" && tbPorts.Text!="")
            {
                tbServiceFiltre.Text = "xxxxxx";
                string sField = "(GuidService, NomService, Protocole, Ports";
                string sValues = "Values ('" + Guid.NewGuid().ToString() + "','" + tbNomService.Text + "','" + tbProtocole.Text + "','" + tbPorts.Text +"'";
                if (tbInfoSup.Text != "")
                {
                    sField += ", InfoSup";
                    sValues += ",'" + tbInfoSup.Text + "'";
                }
                if (tbDescription.Text != "")
                {
                    sField += ", Description";
                    sValues += ",'" + tbDescription.Text + "'";
                }
                sField += ")"; sValues += ")";
                Parent.oCnxBase.CBWrite("Insert Into Service " + sField + " " + sValues);
                tbServiceFiltre.Text = tbNomService.Text[0] + "";
                tbNomService.Text = ""; tbProtocole.Text = ""; tbPorts.Text = ""; tbInfoSup.Text = ""; tbDescription.Text = "";
            }
        }

        private void bModify_Click(object sender, EventArgs e)
        {
            if (lbService.SelectedItem != null && tbNomService.Text != "" && tbProtocole.Text != "" && tbPorts.Text != "")
            {

                string sServiceSave = (string)lbService.SelectedItem;
                tbServiceFiltre.Text = "xxxxxx";
                string sField = "Set NomService='" + tbNomService.Text + "', Protocole='" + tbProtocole.Text + "', Ports='" + tbPorts.Text + "'";
                if (tbInfoSup.Text != "") sField += ", InfoSup='" + tbInfoSup.Text + "'";
                if (tbDescription.Text != "") sField += ", Description='" + tbDescription.Text + "'";
                Parent.oCnxBase.CBWrite("Update Service " + sField + " Where GuidService='" + Parent.oCnxBase.GetValueInStringNomGuid(sServiceSave, 1) + "'");
                tbServiceFiltre.Text = Parent.oCnxBase.GetValueInStringNomGuid(sServiceSave, 0)[0] + "";
                tbNomService.Text = ""; tbProtocole.Text = ""; tbPorts.Text = ""; tbInfoSup.Text = ""; tbDescription.Text = "";
            }
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            if (lbService.SelectedItem != null)
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show("Etes-vous sur de supprimer cet objet.\nToutes les références seront supprimées.", "suppression", buttons);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    string sServiceSave = (string)lbService.SelectedItem;
                    tbServiceFiltre.Text = "xxxxxx";
                    Parent.oCnxBase.CBWrite("Delete From Service Where GuidService='" + Parent.oCnxBase.GetValueInStringNomGuid(sServiceSave, 1) + "'");
                    tbServiceFiltre.Text = Parent.oCnxBase.GetValueInStringNomGuid(sServiceSave, 0)[0] + "";
                    tbNomService.Text = ""; tbProtocole.Text = ""; tbPorts.Text = ""; tbInfoSup.Text = ""; tbDescription.Text = "";
                }
            }
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
