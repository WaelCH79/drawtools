using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DrawTools
{
    public partial class FormFlux : Form
    {
        private Form1 parent;

        public new Form1 Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }

        public FormFlux(Form1 p)
        {
            Parent = p;
            InitializeComponent();

            Parent.oCnxBase.CBAddComboBox("SELECT NomApplication FROM Application ORDER BY NomApplication", this.tbApp);
            tbTypeVue.Items.Add("4-Hors Production");
            tbTypeVue.Items.Add("5-Pre-Production");
            tbTypeVue.Items.Add("3-Production");
            tbTypeVue.Items.Add("F-Service Infra");

        }

        
        public void CalcProvision(string GuidApp, string sVue)
        {
            string sWhereVue="AND (Vue.GuidTypeVue='2a4c3691-e714-4d05-9400-8fbbb06f2d62' OR Vue.GuidTypeVue='ef667e58-a617-49fd-91a8-2beeda856475'  OR Vue.GuidTypeVue='7afca945-9d41-48fb-b634-5b6ffda90d4e')";
            if (sVue.Length != 0) sWhereVue = sVue;
            string sqlString1 = "SELECT Distinct NomApplication, Statut, NomVue, NomServer, NomServerPhy, Type, CPUCoreA, RAMA, CPUcore, RAM FROM Application, Vue, TypeVue, DansVue, GServerPhy, ServerPhy, ServerLink, Server WHERE Application.GuidApplication=Vue.GuidApplication AND Vue.GuidGVue=DansVue.GuidGVue AND Vue.GuidTypeVue=TypeVue.GuidTypeVue AND DansVue.GuidObjet=GServerPhy.GuidGServerPhy AND GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy AND ServerPhy.GuidServerPhy=ServerLink.GuidServerPhy AND ServerLink.GuidServer=Server.GuidServer AND type='M' AND Application.GuidApplication='" + GuidApp + "' " + sWhereVue;
            string sqlString2 = " AND Server.GuidServer IN (SELECT Server.GuidServer FROM Application, Vue, DansVue, GServer, Server WHERE Application.GuidApplication=Vue.GuidApplication AND Vue.GuidGVue=DansVue.GuidGVue AND DansVue.GuidObjet=GServer.GuidGServer AND GServer.GuidServer=Server.GuidServer AND Application.GuidApplication='" + GuidApp + "' AND Vue.GuidTypeVue='d5b533a9-06ac-4f8c-a5ab-e345b0212542')";
            //if (Parent.oCnxBase.CBRecherche("SELECT NomApplication, Statut, NomVue, NomServer, NomServerPhy FROM Application, Vue, DansTypeVue, ServerPhy, ServerLink, Server WHERE Application.GuidApplication=Vue.GuidApplication AND Vue.GuidTypeVue=DansTypeVue.GuidTypeVue AND DansTypeVue.GuidObjet=ServerPhy.GuidServerPhy AND ServerPhy.GuidServerPhy=ServerLink.GuidServerPhy AND ServerLink.GuidServer=Server.GuidServer AND Statut>9 AND Statut<30 AND DansTypeVue.GuidTypeVue='2a4c3691-e714-4d05-9400-8fbbb06f2d62'"))
            if (Parent.oCnxBase.CBRecherche(sqlString1 + sqlString2))
            {
                while (Parent.oCnxBase.Reader.Read())
                {
                    string[] row = new string[10];
                    /*row[0] = Parent.oCnxBase.Reader.GetString(0);
                    row[1] = Parent.oCnxBase.Reader.GetInt32(1).ToString();
                    row[2] = Parent.oCnxBase.Reader.GetString(2);
                    row[3] = Parent.oCnxBase.Reader.GetString(3);
                    row[4] = Parent.oCnxBase.Reader.GetString(4);
                    row[5] = Parent.oCnxBase.Reader.GetString(5);
                    row[6] = Parent.oCnxBase.Reader.GetInt32(6).ToString();
                    row[7] = Parent.oCnxBase.Reader.GetInt32(7).ToString();
                    row[8] = Parent.oCnxBase.Reader.GetInt32(8).ToString();
                    row[9] = Parent.oCnxBase.Reader.GetInt32(9).ToString();

                    if(!cbApp.Items.Contains(row[0])) cbApp.Items.Add(row[0]);
                    if (!cbVersion.Items.Contains(row[1])) cbVersion.Items.Add(row[1]);
                    if (!cbTypeVue.Items.Contains(row[2])) cbTypeVue.Items.Add(row[2]);
                    if (!cbFonction.Items.Contains(row[3])) cbFonction.Items.Add(row[3]);
                    if (!cbServerSrc.Items.Contains(row[4])) cbServerSrc.Items.Add(row[4]);
                    if (!cbType.Items.Contains(row[5])) cbType.Items.Add(row[5]);

                    //if (!cbVersion.Items.Contains(row[1])) cbHote.Items.Add(row[0]);
                    dgFlux.Rows.Add(row);*/
                }
                Parent.oCnxBase.CBReaderClose();
            }
            else Parent.oCnxBase.CBReaderClose();
            //sqlString1 = "SELECT NomApplication, Statut, NomVue, NomServer, ServerPhy.NomServerPhy, ServerPhy.Type, CPUCoreA, RAMA, ServerPhy.CPUcore, ServerPhy.RAM, srv2.NomServerPhy, srv2.CPUcore, srv2.RAM FROM Application, Vue, DansVue, GServerPhy, ServerPhy, ServerLink, Server, ServerPhy srv2 WHERE Application.GuidApplication=Vue.GuidApplication AND Vue.GuidVue=DansVue.GuidVue AND DansVue.GuidObjet=GServerPhy.GuidGServerPhy AND GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy AND ServerPhy.GuidServerPhy=ServerLink.GuidServerPhy AND ServerPhy.GuidHost=srv2.GuidServerPhy AND ServerLink.GuidServer=Server.GuidServer AND ServerPhy.type<>'M' AND Application.GuidApplication='" + GuidApp + "' AND (Vue.GuidTypeVue='2a4c3691-e714-4d05-9400-8fbbb06f2d62' OR Vue.GuidTypeVue='ef667e58-a617-49fd-91a8-2beeda856475'  OR Vue.GuidTypeVue='7afca945-9d41-48fb-b634-5b6ffda90d4e')";
            sqlString1 = "SELECT Distinct NomApplication, Statut, NomVue, NomServer, ServerPhy.NomServerPhy, ServerPhy.Type, CPUCoreA, RAMA, ServerPhy.CPUcore, ServerPhy.RAM, srv2.NomServerPhy, srv2.CPUcore, srv2.RAM FROM Application, Vue, TypeVue, DansVue, GServerPhy, ServerPhy, ServerLink, Server, ServerPhy srv2 WHERE Application.GuidApplication=Vue.GuidApplication AND Vue.GuidGVue=DansVue.GuidGVue AND Vue.GuidTypeVue=TypeVue.GuidTypeVue AND DansVue.GuidObjet=GServerPhy.GuidGServerPhy AND GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy AND ServerPhy.GuidServerPhy=ServerLink.GuidServerPhy AND ServerPhy.GuidHost=srv2.GuidServerPhy AND ServerLink.GuidServer=Server.GuidServer AND ServerPhy.type<>'M' AND Application.GuidApplication='" + GuidApp + "' " + sWhereVue;
            sqlString2 = " AND Server.GuidServer IN (SELECT Server.GuidServer FROM Application, Vue, DansVue, GServer, Server WHERE Application.GuidApplication=Vue.GuidApplication AND Vue.GuidGVue=DansVue.GuidGVue AND DansVue.GuidObjet=GServer.GuidGServer AND GServer.GuidServer=Server.GuidServer AND Application.GuidApplication='" + GuidApp + "' AND Vue.GuidTypeVue='d5b533a9-06ac-4f8c-a5ab-e345b0212542')";
            //if (Parent.oCnxBase.CBRecherche("SELECT NomApplication, Statut, NomVue, NomServer, NomServerPhy FROM Application, Vue, DansTypeVue, ServerPhy, ServerLink, Server WHERE Application.GuidApplication=Vue.GuidApplication AND Vue.GuidTypeVue=DansTypeVue.GuidTypeVue AND DansTypeVue.GuidObjet=ServerPhy.GuidServerPhy AND ServerPhy.GuidServerPhy=ServerLink.GuidServerPhy AND ServerLink.GuidServer=Server.GuidServer AND Statut>9 AND Statut<30 AND DansTypeVue.GuidTypeVue='2a4c3691-e714-4d05-9400-8fbbb06f2d62'"))
            if (Parent.oCnxBase.CBRecherche(sqlString1 + sqlString2))
            {
                while (Parent.oCnxBase.Reader.Read())
                {
                    /*string[] row = new string[13];
                    row[0] = Parent.oCnxBase.Reader.GetString(0);
                    row[1] = Parent.oCnxBase.Reader.GetInt32(1).ToString();
                    row[2] = Parent.oCnxBase.Reader.GetString(2);
                    row[3] = Parent.oCnxBase.Reader.GetString(3);
                    row[4] = Parent.oCnxBase.Reader.GetString(4);
                    row[5] = Parent.oCnxBase.Reader.GetString(5);
                    row[6] = Parent.oCnxBase.Reader.GetInt32(6).ToString();
                    row[7] = Parent.oCnxBase.Reader.GetInt32(7).ToString();
                    row[8] = Parent.oCnxBase.Reader.GetInt32(8).ToString();
                    row[9] = Parent.oCnxBase.Reader.GetInt32(9).ToString();
                    row[10] = Parent.oCnxBase.Reader.GetString(10);
                    row[11] = Parent.oCnxBase.Reader.GetInt32(11).ToString();
                    row[12] = Parent.oCnxBase.Reader.GetInt32(12).ToString();

                    if (!cbApp.Items.Contains(row[0])) cbApp.Items.Add(row[0]);
                    if (!cbVersion.Items.Contains(row[1])) cbVersion.Items.Add(row[1]);
                    if (!cbTypeVue.Items.Contains(row[2])) cbTypeVue.Items.Add(row[2]);
                    if (!cbFonction.Items.Contains(row[3])) cbFonction.Items.Add(row[3]);
                    if (!cbServerSrc.Items.Contains(row[4])) cbServerSrc.Items.Add(row[4]);
                    if (!cbType.Items.Contains(row[5])) cbType.Items.Add(row[5]);

                    dgFlux.Rows.Add(row);*/
                }
                Parent.oCnxBase.CBReaderClose();
            }
            else Parent.oCnxBase.CBReaderClose();
        }

        private void bGo_Click(object sender, EventArgs e)
        {
            ArrayList aListApp = new ArrayList();
            string sWhere="", sVue="";

            dgFlux.Rows.Clear();

            if (cbApp.Items.Count != 0)
            {
                sWhere += " AND (";
                for (int i = 0; i < cbApp.Items.Count; i++)
                {
                    sWhere += "NomApplication='" + cbApp.Items[i] + "'";
                    if (i < cbApp.Items.Count - 1) sWhere += " OR ";
                }
                sWhere += ")";
            }

            if (cbTypeVue.Items.Count != 0)
            {
                sVue += " AND (";
                for (int i = 0; i < cbTypeVue.Items.Count; i++)
                {
                    sVue += "NomTypeVue='" + cbTypeVue.Items[i] + "'";
                    if (i < cbTypeVue.Items.Count - 1) sVue += " OR ";
                }
                sVue += ")";
            }

            cbApp.Items.Clear();
            cbTypeVue.Items.Clear();
           

            if (sWhere.Length == 0) sWhere = "AND Statut>9 AND Statut<30";

            if (Parent.oCnxBase.CBRecherche("SELECT Distinct Application.GuidApplication FROM Application, Vue, TypeVue WHERE Application.GuidApplication=Vue.GuidApplication And Vue.GuidTypeVue=TypeVue.GuidTypeVue " + sWhere))
            {
                while (Parent.oCnxBase.Reader.Read())
                {
                    aListApp.Add(Parent.oCnxBase.Reader.GetString(0));
                }
                Parent.oCnxBase.CBReaderClose();
                for (int i = 0; i < aListApp.Count; i++)
                {
                    CalcProvision((string)aListApp[i], sVue);
                }
            }
            else Parent.oCnxBase.CBReaderClose();
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            if (tbApp.SelectedItem != null)
            {
                cbApp.Items.Add(tbApp.SelectedItem.ToString());
                tbApp.SelectedItem = null;
            }
            else if (tbTypeVue.SelectedItem != null)
            {
                cbTypeVue.Items.Add(tbTypeVue.SelectedItem.ToString());
                tbTypeVue.SelectedItem = null;
            }
            /*else if (tbVersion.Text.Length != 0)
            {
                cbVersion.Items.Add(tbVersion.Text);
                tbVersion.Text = "";
            }
            else if (tbServerSrc.Text.Length != 0)
            {
                cbServerSrc.Items.Add(tbServerSrc.Text);
                tbServerSrc.Text = "";
            }
            else if (tbHote.Text.Length != 0)
            {
                cbHote.Items.Add(tbHote.Text);
                tbHote.Text = "";
            }*/
        }

        private void bInit_Click(object sender, EventArgs e)
        {
            cbApp.Items.Clear();
            cbTypeVue.Items.Clear();
            /*cbFonction.Items.Clear();
            cbHote.Items.Clear();
            cbServerSrc.Items.Clear();
            cbType.Items.Clear();
            cbVersion.Items.Clear();*/

            dgFlux.Rows.Clear();
        }

        void cb_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            filtredg();
            //throw new System.NotImplementedException();
        }

        void filtredg()
        {
            //cbApp.Items.Clear();
            //cbVersion.Items.Clear();
            //cbTypeVue.Items.Clear();
            //cbFonction.Items.Clear();
            //cbServer.Items.Clear();
            //cbType.Items.Clear();
            //cbHote.Items.Clear();
            
            for (int i = 0; i < dgFlux.Rows.Count; i++)
            {
                dgFlux.Rows[i].Visible = false;
                if (cbApp.SelectedItem == null || cbApp.SelectedItem.ToString() == dgFlux.Rows[i].Cells[0].Value.ToString())
                {
                    /*if (cbVersion.SelectedItem == null || cbVersion.SelectedItem.ToString() == dgFlux.Rows[i].Cells[1].Value.ToString())
                    {
                        if (cbTypeVue.SelectedItem == null || cbTypeVue.SelectedItem.ToString() == dgFlux.Rows[i].Cells[2].Value.ToString())
                        {
                            if (cbFonction.SelectedItem == null || cbFonction.SelectedItem.ToString() == dgFlux.Rows[i].Cells[3].Value.ToString())
                            {
                                if (cbServerSrc.SelectedItem == null || cbServerSrc.SelectedItem.ToString() == dgFlux.Rows[i].Cells[4].Value.ToString())
                                {
                                    if (cbType.SelectedItem == null || cbType.SelectedItem.ToString() == dgFlux.Rows[i].Cells[5].Value.ToString())
                                    {
                                        dgFlux.Rows[i].Visible = true;
                                    }
                                }
                            }
                        }
                    }*/
                }
            }
        }

        private void bFiltre_Click(object sender, EventArgs e)
        {
            cbApp.Text = null;
            cbTypeVue.Text = null;
            /*cbFonction.Text = null;
            cbHote.Text = null;
            cbServerSrc.Text = null;
            cbType.Text = null;
            cbVersion.Text = null;*/

            for (int i = 0; i < dgFlux.Rows.Count; i++)
                dgFlux.Rows[i].Visible = true;
        }
    }
}
