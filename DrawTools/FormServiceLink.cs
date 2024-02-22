using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DrawTools
{
    public partial class FormServiceLink : Form
    {
        private Form1 parent;


        public new Form1 Parent
        {
            get { return parent; }
            set { parent = value;}
        }

        public FormServiceLink(Form1 p)
        {
            Parent = p;
            InitializeComponent();
            Parent.oCnxBase.CBAddListBox("Select GuidGroupService, NomGroupService From GroupService Order by NomGroupService", lbGroupService);
            Parent.oCnxBase.CBAddListBox("Select GuidService, NomService From Service Order by NomService", lbService);
            InitEvent();
        }

        public void InitEvent()
        {
            this.tbGroupServiceFiltre.TextChanged += tbGroupServiceFiltre_TextChanged;
            this.tbServiceFiltre.TextChanged += tbServiceFiltre_TextChanged;
            this.lbService.SelectedIndexChanged += lbService_SelectedIndexChanged;
            this.lbGroupService.SelectedIndexChanged += lbGroupService_SelectedIndexChanged;
            this.bAddDev.Click += bAddDev_Click;
            this.bAddInt.Click += bAddInt_Click;
            this.bAddPreProd.Click += bAddPreProd_Click;
            this.bAddProd.Click += bAddProd_Click;
            this.bAddPSI.Click += BAddPSI_Click;
            this.bAddRec.Click += bAddRec_Click;
            this.bSupDev.Click += bSupDev_Click;
            this.bSupInt.Click += bSupInt_Click;
            this.bSupPProd.Click += bSupPProd_Click;
            this.bSupProd.Click += bSupProd_Click;
            this.bSupPSI.Click += BSupPSI_Click;
            this.bSupRec.Click += bSupRec_Click;
            this.bApply.Click += bApply_Click;
            this.bClose.Click += bClose_Click;
        }

        private void tbServiceFiltre_TextChanged(object sender, EventArgs e)
        {
            lbService.Items.Clear();
            if (tbServiceFiltre.Text != "")
                Parent.oCnxBase.CBAddListBox("Select GuidService, NomService From Service Where NomService Like '" + tbServiceFiltre.Text + "%' Order by NomService", lbService);
            else
                Parent.oCnxBase.CBAddListBox("Select GuidService, NomService From Service Order by NomService", lbService);
        }

        private void tbGroupServiceFiltre_TextChanged(object sender, EventArgs e)
        {
            lbGroupService.Items.Clear();
            if (tbGroupServiceFiltre.Text != "")
                Parent.oCnxBase.CBAddListBox("Select GuidGroupService, NomGroupService From GroupService Where NomGroupService Like '" + tbGroupServiceFiltre.Text + "%' Order by NomGroupService", lbGroupService);
            else
                Parent.oCnxBase.CBAddListBox("Select GuidGroupService, NomGroupService From GroupService Order by NomGroupService", lbGroupService);
        }

        private void lbService_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Parent.oCnxBase.CBRecherche("Select NomService, Protocole, Ports From Service Where GuidService='" + Parent.oCnxBase.GetValueInStringNomGuid((string)lbService.SelectedItem, 1) + "'"))
            {
                tbNomService.Text = Parent.oCnxBase.Reader.GetString(0);
                tbProtocole.Text = Parent.oCnxBase.Reader.GetString(1);
                tbPorts.Text = Parent.oCnxBase.Reader.GetString(2);
            }
            Parent.oCnxBase.CBReaderClose();
        }

        private void lbGroupService_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbProd.Items.Clear();
            lbPSI.Items.Clear();
            lbPProd.Items.Clear();
            lbRec.Items.Clear();
            lbInt.Items.Clear();
            lbdev.Items.Clear();


            if (Parent.oCnxBase.CBRecherche("Select Service.GuidService, NomService, Lettre From ServiceLink, Service, Environnement Where Environnement.GuidEnvironnement=ServiceLink.GuidEnvironnement And Service.GuidService=ServiceLink.GuidService And GuidGroupService='" + Parent.oCnxBase.GetValueInStringNomGuid((string)lbGroupService.SelectedItem, 1) + "' Order by NomService"))
            {
                while (Parent.oCnxBase.Reader.Read())
                {
                    switch (Parent.oCnxBase.Reader.GetString(2)[0])
                    {
                        case 'r':
                            Parent.oCnxBase.CBAddListBoxItem(Parent.oCnxBase.Reader.GetString(1), Parent.oCnxBase.Reader.GetString(0), lbProd);
                            break;
                        case 'p':
                            Parent.oCnxBase.CBAddListBoxItem(Parent.oCnxBase.Reader.GetString(1), Parent.oCnxBase.Reader.GetString(0), lbPSI);
                            break;
                        case 'q':
                            Parent.oCnxBase.CBAddListBoxItem(Parent.oCnxBase.Reader.GetString(1), Parent.oCnxBase.Reader.GetString(0), lbPProd);
                            break;
                        case 'u':
                            Parent.oCnxBase.CBAddListBoxItem(Parent.oCnxBase.Reader.GetString(1), Parent.oCnxBase.Reader.GetString(0), lbRec);
                            break;
                        case 't':
                            Parent.oCnxBase.CBAddListBoxItem(Parent.oCnxBase.Reader.GetString(1), Parent.oCnxBase.Reader.GetString(0), lbInt);
                            break;
                        case 'd':
                            Parent.oCnxBase.CBAddListBoxItem(Parent.oCnxBase.Reader.GetString(1), Parent.oCnxBase.Reader.GetString(0), lbdev);
                            break;
                    }
                }
            }
            Parent.oCnxBase.CBReaderClose();
        }

        private void BAddPSI_Click(object sender, EventArgs e)
        {
            if (lbService.SelectedItem != null)
            {
                bApply.Enabled = true;
                lbPSI.Items.Add(lbService.SelectedItem);
            }
        }

        private void bAddProd_Click(object sender, EventArgs e)
        {
            if (lbService.SelectedItem != null)
            {
                bApply.Enabled = true;
                lbProd.Items.Add(lbService.SelectedItem);
            }

        }

        private void BSupPSI_Click(object sender, EventArgs e)
        {
            if (lbPSI.SelectedItem != null)
            {
                bApply.Enabled = true;
                lbPSI.Items.Remove(lbPSI.SelectedItem);
            }
        }

        private void bSupProd_Click(object sender, EventArgs e)
        {
            if (lbProd.SelectedItem != null)
            {
                bApply.Enabled = true;
                lbProd.Items.Remove(lbProd.SelectedItem);
            }
        }

        private void bAddPreProd_Click(object sender, EventArgs e)
        {
            if (lbService.SelectedItem != null)
            {
                bApply.Enabled = true;
                lbPProd.Items.Add(lbService.SelectedItem);
            }
        }

        private void bSupPProd_Click(object sender, EventArgs e)
        {
            if (lbPProd.SelectedItem != null)
            {
                bApply.Enabled = true;
                lbPProd.Items.Remove(lbPProd.SelectedItem);
            }
        }

        private void bAddRec_Click(object sender, EventArgs e)
        {
            if (lbService.SelectedItem != null)
            {
                bApply.Enabled = true;
                lbRec.Items.Add(lbService.SelectedItem);
            }
        }

        private void bSupRec_Click(object sender, EventArgs e)
        {
            if (lbRec.SelectedItem != null)
            {
                bApply.Enabled = true;
                lbRec.Items.Remove(lbRec.SelectedItem);
            }
        }

        private void bAddInt_Click(object sender, EventArgs e)
        {
            if (lbService.SelectedItem != null)
            {
                bApply.Enabled = true;
                lbInt.Items.Add(lbService.SelectedItem);
            }
        }

        private void bSupInt_Click(object sender, EventArgs e)
        {
            if (lbInt.SelectedItem != null)
            {
                bApply.Enabled = true;
                lbInt.Items.Remove(lbInt.SelectedItem);
            }
        }

        private void bAddDev_Click(object sender, EventArgs e)
        {
            if (lbService.SelectedItem != null)
            {
                bApply.Enabled = true;
                lbdev.Items.Add(lbService.SelectedItem);
            }
        }

        private void bSupDev_Click(object sender, EventArgs e)
        {
            if (lbdev.SelectedItem != null)
            {
                bApply.Enabled = true;
                lbdev.Items.Remove(lbdev.SelectedItem);
            }
        }

        private void bApply_Click(object sender, EventArgs e)
        {
            if(lbGroupService.SelectedItem != null) {
                Parent.oCnxBase.DeleteServiceLink(Parent.oCnxBase.GetValueInStringNomGuid((string) lbGroupService.SelectedItem, 1));
                Parent.oCnxBase.CreatServiceLink(Parent.oCnxBase.GetValueInStringNomGuid((string)lbGroupService.SelectedItem, 1), "96699e1c-d501-48c1-8585-3fce75a4c2e7", lbProd);
                Parent.oCnxBase.CreatServiceLink(Parent.oCnxBase.GetValueInStringNomGuid((string)lbGroupService.SelectedItem, 1), "9b101b08-daee-4a96-9463-81eede5c7d60", lbPSI);
                Parent.oCnxBase.CreatServiceLink(Parent.oCnxBase.GetValueInStringNomGuid((string)lbGroupService.SelectedItem, 1), "8ca15033-07a6-4ea7-8392-907fd3cb7e16", lbPProd);
                Parent.oCnxBase.CreatServiceLink(Parent.oCnxBase.GetValueInStringNomGuid((string)lbGroupService.SelectedItem, 1), "514c254c-68d3-4980-9ef6-9e811c5c770a", lbRec);
                Parent.oCnxBase.CreatServiceLink(Parent.oCnxBase.GetValueInStringNomGuid((string)lbGroupService.SelectedItem, 1), "57bdd6fc-0821-412c-a57d-cb8e7c405b61", lbInt);
                Parent.oCnxBase.CreatServiceLink(Parent.oCnxBase.GetValueInStringNomGuid((string)lbGroupService.SelectedItem, 1), "63044337-24f4-4562-8c2b-1a7d9374f1fd", lbdev);
                bApply.Enabled = false;
            }
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Close();

        }
    }
}
