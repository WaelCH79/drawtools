using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DrawTools
{
    public partial class FormDeployComposant : Form
    {
        private Form1 parent;
        private DataGridView odgv;
        private List<ExpObj> lstPackage;
        private DrawObject o;

        public new Form1 Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public FormDeployComposant(Form1 p, DataGridView dgv)
        {
            Parent = p;
            odgv = dgv;
            Parent.oCnxBase.CmdText = "";
            lstPackage = new List<ExpObj>();
            InitializeComponent();
        }

        public void init()
        {
            this.lLinkApp.DoubleClick += lLinkApp_DoubleClick;
            this.lLinkAppAdd.DoubleClick += lLinkAppAdd_DoubleClick;
            this.bOK.Click += bOK_Click;
            this.bCancel.Click += bCancel_Click;
            this.bAdd.Click += bAdd_Click;
            this.bSup.Click += bSup_Click;
            this.tvPackage.AfterSelect += tvPackage_AfterSelect;


            ShowDialog(Parent);
        }

        private void objExpSave()
        {
            if (Parent.oCureo != null)
            {
                Parent.oCureo.oDraw.SaveProp(dgvPackage, Parent.oCureo.confTable);
                //Parent.oCureo.oDraw = null;
                Parent.oCureo = null;
            }
            for (int i = 0; i < lstPackage.Count; i++)
            {
                ExpObj eo = lstPackage[i];
                if (eo.oDraw != null)
                {
                    DrawObject obj = eo.oDraw;
                    //obj.SaveProp(dgvPackage, eo.confTable);
                    obj.saveObjecttoDB(eo.confTable);
                }
            }
        }

        private void tvPackage_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Guid g = new Guid(tvPackage.SelectedNode.Name);
            //ExpObj eo = lstPackage.Find(el => el.GuidObj == g);

            ExpObj eo = lstPackage.Find(el => el.lstKeyObj[0] == tvPackage.SelectedNode.Name);

            if (eo != null)
            {
                if (Parent.oCureo != null) Parent.oCureo.oDraw.SaveProp(dgvPackage, eo.confTable);

                Parent.oCureo = eo;
                if (eo.oDraw != null) eo.oDraw.InitDatagrid(dgvPackage, eo.confTable);
            }
        }

        public void InitPackage(Guid guid, string sNom)
        {
            TreeNode tnServer = tvPackage.Nodes.Add(guid.ToString(), sNom);
            AddPackage(tnServer);
        }

        public void AddPackage(TreeNode tnServer)
        {
            CnxBase cnx = Parent.oCnxBase;
            /*
            string sSelect = "select Server.NomServer, MainComposant.GuidMainComposant, MainComposant.NomMainComposant, MCompServ.GuidMCompServ, MainComposantRef.NomMainComposantRef ";
            string sFrom = "from Server, MCompApp, MainComposant, MainComposantRef, mcompserv ";
            string sWhere = "where Server.GuidServer = MCompApp.GuidServer AND MCompApp.GuidMainComposant = MainComposant.GuidMainComposant AND MainComposant.GuidMainComposant = MCompServ.GuidMainComposant AND MCompServ.GuidMainComposantRef = MainComposantRef.GuidMainComposantRef AND ";
            */


            string sSelect = "select Server.NomServer, MainComposant.GuidMainComposant, MainComposant.NomMainComposant, MCompServ.GuidMCompServ, MainComposantRef.NomMainComposantRef ";
            string sFrom = "from Server ";
            sFrom += "Left Join MCompApp On Server.GuidServer = MCompApp.GuidServer ";
            sFrom += "Left Join MainComposant On MCompApp.GuidMainComposant = MainComposant.GuidMainComposant ";
            sFrom += "Left Join Mcompserv On MainComposant.GuidMainComposant = MCompServ.GuidMainComposant ";
            sFrom += "Left Join MainComposantRef On MCompServ.GuidMainComposantRef = MainComposantRef.GuidMainComposantRef ";

            for (int i = 0; i < lLinkAppAdd.Items.Count; i++)
            {
                string[] aValue = ((string)lLinkAppAdd.Items[i]).Split('(', ')');
                TreeNode tn = null;
                TreeNode [] atn = null;
                if (cnx.CBRecherche(sSelect + sFrom + "Where Server.GuidServer = '" + aValue[1] + "'"))
                {
                    while (cnx.Reader.Read())
                    {
                        // Prenier Niveau
                        tn = tvPackage.Nodes[0];
                        atn = tn.Nodes.Find(aValue[1], true);
                        if (atn.Length == 0) tn = tn.Nodes.Add(aValue[1], cnx.Reader.GetString(0));
                        else tn = atn[0];
                        // Second Niveau
                        if (!cnx.Reader.IsDBNull(1))
                        {
                            atn = tn.Nodes.Find(cnx.Reader.GetString(1), true);
                            if (atn.Length == 0) tn = tn.Nodes.Add(cnx.Reader.GetString(1), cnx.Reader.GetString(2));
                            else tn = atn[0];
                            // Troisième Niveau
                            if (!cnx.Reader.IsDBNull(3))
                            {
                                atn = tn.Nodes.Find(cnx.Reader.GetString(3), true);
                                if (atn.Length == 0)
                                {
                                    tn = tn.Nodes.Add(cnx.Reader.GetString(3), cnx.Reader.GetString(4));
                                    List<string> lstKey = new List<string>();
                                    lstKey.Add(cnx.Reader.GetString(3));
                                    lstKey.Add(Parent.GetGuidVue());
                                    ExpObj eo = new ExpObj(lstKey, cnx.Reader.GetString(4), DrawArea.DrawToolType.PackageDynamic);
                                    //ExpObj eo = new ExpObj(new Guid(cnx.Reader.GetString(3)), cnx.Reader.GetString(4), DrawArea.DrawToolType.Package);
                                    lstPackage.Add(eo);
                                }
                                else tn = atn[0];
                            }
                        }

                    }
                }
                cnx.CBReaderClose();
                for (int j=0; j < lstPackage.Count; j++)
                {
                    ExpObj eo = lstPackage[j];
                    eo.setConfTable(Parent.drawArea);

                    Parent.oCureo = eo;

                    Parent.drawArea.tools[(int)eo.ObjTool].LoadSimpleObjectSansGraph(eo.lstKeyObj, eo.confTable);

                    if (eo.oDraw == null)
                    {
                        eo.oDraw = new DrawPackageDynamic(Parent, eo.lstKeyObj, eo.confTable);

                    }
                    Parent.oCureo = null;
                }
            }
        }

        public void AddlDestinationFromChild(DrawObject oParent, string sType)
        {
            o = oParent;
            int n = o.GetIndexFirtObj(sType);
            if (n > -1)
            {
                string NomPropriete = (string)odgv.CurrentRow.Cells[0].Value;
                DrawObject oChild = (DrawObject)o.LstChild[n];

                //string value = (string)odgv.CurrentRow.Cells[1].Value;
                //string valueNonVisible = (string)odgv.CurrentRow.Cells[3].Value;
                Table t;

                n = Parent.oCnxBase.ConfDB.FindTable(sType);
                if (n > -1)
                {
                    t = (Table)Parent.oCnxBase.ConfDB.LstTable[n];
                    string sValuePropriteObjet = (string)oChild.GetValueFromLib(NomPropriete);

                    if (sValuePropriteObjet != "")
                    {
                        string[] aValue = sValuePropriteObjet.Split(';');
                        for (int i = 0; i < aValue.Length; i++) lLinkAppAdd.Items.Add(aValue[i]);
                    }
                }
            }
        }

        public void AddlDestinationFromProp(string sType)
        {
            string value = (string)odgv.CurrentRow.Cells[1].Value;
            string valueNonVisible = (string)odgv.CurrentRow.Cells[3].Value;
            Table t;

            int n = Parent.oCnxBase.ConfDB.FindTable(sType);

            if (n > -1)
            {
                t = (Table)Parent.oCnxBase.ConfDB.LstTable[n];
                if (value != "")
                {
                    string[] aValue = value.Split(';');
                    if ((((Field)t.LstField[odgv.CurrentCell.RowIndex]).fieldOption & ConfDataBase.FieldOption.CacheVal) != 0)
                    {
                        string[] aValueNonVisible = valueNonVisible.Split(';');
                        for (int i = 0; i < aValue.Length; i++) lLinkAppAdd.Items.Add(aValue[i] + "  (" + aValueNonVisible[i] + ")");
                    }
                    else for (int i = 0; i < aValue.Length; i++) lLinkAppAdd.Items.Add(aValue[i]);
                }
            }
        }

        public void AddlSourceFromDB(string strSelect)
        {
            Parent.oCnxBase.CBAddListBox(strSelect, lLinkApp);
        }

        private void SupProp()
        {

            if (lLinkAppAdd.SelectedItem != null)
            {
                string[] aValue = ((string)(lLinkAppAdd.SelectedItem)).Split('(', ')');
                TreeNode[] atn = tvPackage.Nodes.Find(aValue[1], true);
                for (int i = 0; i < atn.Length; i++) atn[i].Remove();

                lLinkApp.Items.Add(lLinkAppAdd.SelectedItem);
                lLinkAppAdd.Items.Remove(lLinkAppAdd.SelectedItem);
            }
        }

        private void AddProp()
        {
            if (lLinkApp.SelectedItem != null)
            {
                lLinkAppAdd.Items.Add(lLinkApp.SelectedItem);
                lLinkApp.Items.Remove(lLinkApp.SelectedItem);
                AddPackage(tvPackage.Nodes[0]);
            }
        }

        private void bSup_Click(object sender, EventArgs e)
        {
            SupProp();
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            AddProp();
            //throw new NotImplementedException();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Parent.oCnxBase.CmdText = "";
            if (Parent.oCureo != null)
            {
                Parent.oCureo.oDraw = null;
                Parent.oCureo = null;
            }
            Close();
        }

        private void ValueOKClickToChild()
        {
            string Cell1 = "";
            odgv.CurrentRow.Cells[1].Value = "";
            odgv.CurrentRow.Cells[3].Value = "";

            if (lLinkAppAdd.Items.Count != 0 && o != null)
            {
                for (int i = 0; i < lLinkAppAdd.Items.Count; i++)
                    Cell1 += ";" + ((string)lLinkAppAdd.Items[i]);

                for (int i = 0; i < o.LstChild.Count; i++)
                {
                    string NomPropriete = (string)odgv.CurrentRow.Cells[0].Value;
                    DrawObject oChild = (DrawObject)o.LstChild[i];

                    if (oChild.GetType() == typeof(DrawServerPhy))
                    {
                        Table t;
                        int n = Parent.oCnxBase.ConfDB.FindTable("ServerPhy");

                        if (n > -1)
                        {
                            t = (Table)Parent.oCnxBase.ConfDB.LstTable[n];
                            oChild.SetValueFromLib(NomPropriete, Cell1.Substring(1));

                        }
                    }
                }
            }
        }

        private void ValueOKClick()
        {
            int i;
            string[] aValue;
            string Cell1 = "", Cell3 = "";
            odgv.CurrentRow.Cells[1].Value = "";
            odgv.CurrentRow.Cells[3].Value = "";

            if (lLinkAppAdd.Items.Count != 0)
            {
                if (((string)lLinkAppAdd.Items[0]).IndexOf('(') > -1)
                {
                    for (i = 0; i < lLinkAppAdd.Items.Count; i++)
                    {
                        aValue = ((string)lLinkAppAdd.Items[i]).Split('(', ')');
                        Cell1 += ";" + aValue[0].Trim();
                        Cell3 += ";" + aValue[1];
                    }
                    odgv.CurrentRow.Cells[3].Value = Cell3.Substring(1); ;
                }
                else
                {
                    for (i = 0; i < lLinkAppAdd.Items.Count; i++)
                        Cell1 += ";" + ((string)lLinkAppAdd.Items[i]);

                }
                odgv.CurrentRow.Cells[1].Value = Cell1.Substring(1);
            }
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            objExpSave();
            if(o != null) ValueOKClickToChild();
            else ValueOKClick();
            if (Parent.oCureo != null)
            {
                Parent.oCureo.oDraw = null;
                Parent.oCureo = null;
            }
            Close();
        }

        
        private void lLinkAppAdd_DoubleClick(object sender, EventArgs e)
        {
            SupProp();
            //throw new NotImplementedException();
        }

        private void lLinkApp_DoubleClick(object sender, EventArgs e)
        {
            AddProp();
            //throw new NotImplementedException();
        }
    }
}
