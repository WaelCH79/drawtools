using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace DrawTools
{

    public partial class FormChangeProp : Form
    {
        private Form1 parent;
        private DataGridView odgv;
        private string typeform;
        private Boolean bOk;
        private DrawObject o;

        public new Form1 Parent
        {
            get { return parent; }
            set {parent = value; }
        }

        public Boolean Valider
        {
            get { return bOk; }
        }

        public String TypeForm
        {
            get
            {
                return typeform;
            }
            set
            {
                typeform = value;
            }
        }

        public FormChangeProp(Form1 p, DataGridView dgv)
        {

            Parent = p;
            odgv = dgv;
            bOk = false;
            InitializeComponent();
            InitEvent();
            Parent.oCnxBase.CmdText = "";
            //AddlLinkApp();
        }

        public void InitEvent() {
            this.lLinkApp.DoubleClick+=lLinkApp_DoubleClick;
            this.lLinkAppAdd.DoubleClick+=lLinkAppAdd_DoubleClick;
        }

        public void ModeTv()
        {
            TypeForm = "TreeView";
            this.cbAlias.Visible = false;
            this.cbGuidAlias.Visible = false;
            this.lLinkApp.Visible = false;
            this.lLinkAppAdd.Visible = false;
            this.bAdd.Visible = false;
            this.bSup.Visible = false;
            this.tbProp.Visible = false;
            this.bAddProp.Visible = false;
            this.bDelProp.Visible = false;
            this.tvCadreRef.Visible = true;
        }

        public void EnableProp()
        {
            this.tbProp.Enabled = true;
            this.bAddProp.Enabled = true;
            this.bDelProp.Enabled = true;
        }

        public void InitTv(char switchTechFonc)
        {
            InitCadreRef(tvCadreRef.Nodes, "Root", switchTechFonc);

        }

        private void InitCadreRef(TreeNodeCollection tn, string guidParent, char switchTechFonc)
        {
            ArrayList guidCadreRef = new ArrayList();
            ArrayList NomCadreRef = new ArrayList();
            string sSelect = "";

            switch (switchTechFonc)
            {
                case 'F':
                    sSelect = "Select GuidCadreRefFonc, NomCadreRefFonc FROM CadreRefFonc WHERE GuidParentFonc='" + guidParent + "'";
                    break;
                case 'S':
                case 'H':
                    sSelect = "Select GuidCadreRef, NomCadreRef FROM CadreRef WHERE GuidParent='" + guidParent + "' AND (TypeCadreRef='" + switchTechFonc + "' OR TypeCadreRef IS NULL)";
                    break;
                case 'A':
                    sSelect = "Select GuidCadreRefApp, NomCadreRefApp FROM CadreRefApp WHERE GuidParentApp='" + guidParent + "'";
                    break;
            }

            if (Parent.oCnxBase.CBRecherche(sSelect))
            {
                while (Parent.oCnxBase.Reader.Read())
                {
                    guidCadreRef.Add((object)Parent.oCnxBase.Reader.GetString(0));
                    NomCadreRef.Add((object)Parent.oCnxBase.Reader.GetString(1));
                }
            }
            Parent.oCnxBase.CBReaderClose();
            for (int i = 0; i < guidCadreRef.Count; i++)
            {
                tn.Add((string)guidCadreRef[i], (string)NomCadreRef[i]);
                InitCadreRef(tn[tn.Count - 1].Nodes, (string)guidCadreRef[i], switchTechFonc);
            }
        }

        public void AddlSourceColor()
        {
            TypeForm = "Value";
            lLinkApp.Items.Add("AliceBlue");
            lLinkApp.Items.Add("Aqua");
            lLinkApp.Items.Add("Azure");
            lLinkApp.Items.Add("Beige");
            lLinkApp.Items.Add("Black");
            lLinkApp.Items.Add("Blue");
            lLinkApp.Items.Add("Brown");
            lLinkApp.Items.Add("Coral");
            lLinkApp.Items.Add("Cyan");
            lLinkApp.Items.Add("DarkBlue");
            lLinkApp.Items.Add("DarkCyan");
            lLinkApp.Items.Add("DarkGray");
            lLinkApp.Items.Add("DarkGreen");
            lLinkApp.Items.Add("DarkOrange");
            lLinkApp.Items.Add("DarkRed");
            lLinkApp.Items.Add("Gold");
            lLinkApp.Items.Add("Gray");
            lLinkApp.Items.Add("Green");
            lLinkApp.Items.Add("LightBlue");
            lLinkApp.Items.Add("LightCyan");
            lLinkApp.Items.Add("LightGray");
            lLinkApp.Items.Add("LightGreen");
            lLinkApp.Items.Add("Magenta");
            lLinkApp.Items.Add("Olive");
            lLinkApp.Items.Add("Orange");
            lLinkApp.Items.Add("Purple");
            lLinkApp.Items.Add("Red");
            lLinkApp.Items.Add("Turquoise");
            lLinkApp.Items.Add("Violet");
            lLinkApp.Items.Add("Yellow");
        }

        public void lSourceDisable()
        {
            typeform = "Value";

            this.tbProp.Enabled = true;
            this.bAddProp.Enabled = true;
            this.bDelProp.Enabled = true;

            this.lLinkApp.Enabled = false;
            this.bAdd.Enabled = false;
            this.bSup.Enabled = false;
        }

        public bool AddcbAlias()
        {
            this.cbAlias.Visible = true;
            int idx;

            for (idx = 0; idx < odgv.Rows.Count; idx++) if ((string) (odgv.Rows[idx].Cells[0].Value) == "Alias") break;

            string[] aValue = ((string)odgv.Rows[idx].Cells[1].Value).Split(';');
            string[] avalueNonVisible = ((string)odgv.Rows[idx].Cells[3].Value).Split(';');
            if (aValue[0]!="")
            {
                for (int i = 0; i < aValue.Length; i++)
                {
                    cbAlias.Items.Add(aValue[i]);
                    cbGuidAlias.Items.Add(avalueNonVisible[i]);
                }
                cbAlias.SelectedIndex = 0;
                return true;
            }
            return false;
        }

        public void AddlSourceFromString(string sValue)
        {
            TypeForm = "Create";
            if (sValue != "")
            {
                string[] aValue = sValue.Split(';');
                for (int i = 0; i < aValue.Length; i++) lLinkApp.Items.Add(aValue[i]);
            }
        }


        public void AddlSourceFromTv(string FindString)
        {
            TypeForm = "Value";
            TreeNode[] ArrayTreeNode = Parent.tvObjet.Nodes.Find(FindString, true);
            if(ArrayTreeNode.Length==1)
            {
                for (int i = 0; i < ArrayTreeNode[0].Nodes.Count; i++)
                {
                    lLinkApp.Items.Add(ArrayTreeNode[0].Nodes[i].Text + "  (" + ArrayTreeNode[0].Nodes[i].Name + ")");
                }
            }
            
        }

        public void AddlChildSourceFromTv(string FindString, string sTypeChild)
        {
            TypeForm = "Value";
            TreeNode[] ArrayTreeNode = Parent.tvObjet.Nodes.Find(FindString, true);
            DrawObject o, oChild;
            if (ArrayTreeNode.Length == 1)
            {
                for (int i = 0; i < ArrayTreeNode[0].Nodes.Count; i++)
                {
                    string GuidObjet = ArrayTreeNode[0].Nodes[i].Name;
                    string NameObjet = ArrayTreeNode[0].Nodes[i].Text;

                    int j = Parent.drawArea.GraphicsList.FindObjet(0, GuidObjet);
                    if (j != -1)
                    {
                        o = (DrawObject)Parent.drawArea.GraphicsList[j];

                        for (j = 0; j < o.LstChild.Count; j++)
                        {
                            oChild = (DrawObject)o.LstChild[j];
                            if (oChild.GetType().Name.Substring("Draw".Length) == sTypeChild)
                                lLinkApp.Items.Add(ArrayTreeNode[0].Nodes[i].Text + "_" + oChild.Texte + "  (" + oChild.GuidkeyObjet + ")");
                        }

                    }
                    //lLinkApp.Items.Add(ArrayTreeNode[0].Nodes[i].Text + "  (" + ArrayTreeNode[0].Nodes[i].Name + ")");
                }
            }

        }


        
        public bool AddlSourceFromDB(string strSelect, string sTypeForm)
        {
            TypeForm = sTypeForm;
            return Parent.oCnxBase.CBAddListBox(strSelect, lLinkApp);
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            AddProp();
        }

        private void AddProp()
        {
            if (lLinkApp.SelectedItem != null)
            {
                if (cbAlias.Visible != true) lLinkAppAdd.Items.Add(lLinkApp.SelectedItem);
                else
                {
                    string s = (string)lLinkApp.SelectedItem;
                    lLinkAppAdd.Items.Add(s.Replace(")", ":" + cbGuidAlias.Items[cbAlias.SelectedIndex] + ")"));
                }
                lLinkApp.Items.Remove(lLinkApp.SelectedItem);
            }
        }

        private void bSup_Click(object sender, EventArgs e)
        {
            SupProp();
        }

        void lLinkAppAdd_DoubleClick(object sender, System.EventArgs e)
        {
            SupProp();
            //throw new System.NotImplementedException();
        }

        void lLinkApp_DoubleClick(object sender, System.EventArgs e)
        {
            AddProp();
            //throw new System.NotImplementedException();
        }

        private void SupProp()
        {
            if (lLinkAppAdd.SelectedItem != null)
            {
                lLinkApp.Items.Add(lLinkAppAdd.SelectedItem);
                lLinkAppAdd.Items.Remove(lLinkAppAdd.SelectedItem);
            }
        }

        public void AffCheckBoxDefaultLayer()
        {
            this.ckbDefaultLayer.Visible = true;
        }

        public void AddlDestinationFromValue(string sValue)
        {
            lLinkAppAdd.Items.Clear();
            if (sValue != "")
            {
                string[] aValue = sValue.Split(';');
                for (int i = 0; i < aValue.Length; i++) {
                    if (aValue[i].IndexOf("null") != -1) ckbDefaultLayer.Checked = true;
                    else lLinkAppAdd.Items.Add(aValue[i]);
                }
            }
        }

        public int nbrItemListAdd()
        {
            return lLinkAppAdd.Items.Count;
        }

        public void AddlDestinationFromDB(string strSelect)
        {
            Parent.oCnxBase.CBAddListBox(strSelect, lLinkAppAdd);
        }

        

        public void AddlDestinationFromProp(string sType)
        {
            string value = (string) odgv.CurrentRow.Cells[1].Value;
            string valueNonVisible = (string)odgv.CurrentRow.Cells[3].Value;
            Table t;
            
            int n =  Parent.oCnxBase.ConfDB.FindTable(sType);


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

        public void AddlDestinationFromLstChild(DrawObject obj, string sType)
        {
            o = obj;
            for (int i = 0; i < o.LstChild.Count; i++)
                if (o.LstChild[i].GetType().Name == sType)
                {
                    DrawObject oi = (DrawObject)o.LstChild[i];
                    lLinkAppAdd.Items.Add(oi.Texte + "  (" + oi.GuidkeyObjet + ")");
                }
        }

        public void AddlDestinationFromChild(DrawObject oParent, string sType)
        {
            if(TypeForm == "Value") TypeForm = "Walue";
            o = oParent;
            int n = o.GetIndexFirtObj(sType);
            if (n > -1)
            {
                string NomPropriete = (string)odgv.CurrentRow.Cells[0].Value;
                DrawObject oChild = (DrawObject) o.LstChild[n];

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

        private void CreateOKClick()
        {
            string Value="";
            if (ckbDefaultLayer.Visible && ckbDefaultLayer.Checked) Value += "; (null)";

            if (lLinkAppAdd.Items.Count != 0)
            {
                for (int i = 0; i < lLinkAppAdd.Items.Count; i++)
                {
                    Value += ";" + (string)lLinkAppAdd.Items[i];
                }
            }
            if (Value.Length == 0) Parent.oCnxBase.CmdText = "";
            else Parent.oCnxBase.CmdText = Value.Substring(1);
        }

        private void TreeViewOKClick()
        {
            if (tvCadreRef.SelectedNode != null && tvCadreRef.SelectedNode.Nodes.Count == 0)
            {
                odgv.CurrentRow.Cells[3].Value = tvCadreRef.SelectedNode.Name;
                odgv.CurrentRow.Cells[1].Value = tvCadreRef.SelectedNode.Text;
            }
        }

        private void ObjectOkClick()
        {
            int i;
            string[] aValue;
            string guidObj = null;
            
            if (lLinkAppAdd.Items.Count != 0)
            {
                if (((string)lLinkAppAdd.Items[0]).IndexOf('(') > -1)
                {
                    for (i = 0; i < lLinkAppAdd.Items.Count; i++)
                    {
                        aValue = ((string)lLinkAppAdd.Items[i]).Split('(', ')');
                        guidObj = aValue[1];
                        if (Parent.drawArea.GraphicsList.FindObjet(0, guidObj) < 0)
                        { // ajoute l'objet sur l'objet parent
                            Parent.drawArea.tools[(int)DrawArea.DrawToolType.ServerPhy].LoadSimpleObject(guidObj);
                            int j = Parent.drawArea.GraphicsList.FindObjet(0, guidObj);
                            if (j > -1)
                            {
                                DrawServerPhy ds = (DrawServerPhy)Parent.drawArea.GraphicsList[j];
                                o.AttachLink(ds, DrawObject.TypeAttach.Child);
                                ds.AttachLink(o, DrawObject.TypeAttach.Parent);
                                ((DrawCluster)o).AligneObjet();
                            }
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
                        //Table t;
                        int n = Parent.oCnxBase.ConfDB.FindTable("ServerPhy");

                        if (n > -1)
                        {
                            //t = (Table)Parent.oCnxBase.ConfDB.LstTable[n];
                            oChild.SetValueFromLib(NomPropriete, Cell1.Substring(1));

                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < o.LstChild.Count; i++)
                {
                    string NomPropriete = (string)odgv.CurrentRow.Cells[0].Value;
                    DrawObject oChild = (DrawObject)o.LstChild[i];

                    if (oChild.GetType() == typeof(DrawServerPhy))
                    {
                        int n = Parent.oCnxBase.ConfDB.FindTable("ServerPhy");
                        if (n > -1) oChild.SetValueFromLib(NomPropriete, "");
                    }
                }
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            bOk = true;
            switch (TypeForm[0])
            {
                case 'V': //Value
                    ValueOKClick();
                    break;
                case 'W': //Value avec maj sur les enfants
                    ValueOKClickToChild();
                    break;
                case 'C': //Create
                    CreateOKClick();
                    break;
                case 'T':
                    TreeViewOKClick();
                    break;
                case 'O':
                    ObjectOkClick();
                    break;
            }
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Parent.oCnxBase.CmdText = "";
            Close();
        }

        private void bAddProp_Click(object sender, EventArgs e)
        {
            if (tbProp.Text != "")
            {
                lLinkAppAdd.Items.Add(tbProp.Text + " (" + Guid.NewGuid().ToString() + ")");
                tbProp.Text = "";
            }

        }

        private void bDelProp_Click(object sender, EventArgs e)
        {
            if (lLinkAppAdd.SelectedItem != null)
            {
                lLinkAppAdd.Items.Remove(lLinkAppAdd.SelectedItem);
                MessageBox.Show("Attention, cette action supprimera lensemble des objets liés de cette vue, mais aussi des autres vues");
            }
        }

        private void lLinkAppAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bFind = false;
            if (cbAlias.Visible == true)
            {
                string Value = (string)lLinkAppAdd.SelectedItem;
                if(Value!=null && Value.IndexOf(':')>-1)
                {
                    for (int i = 0; i < cbGuidAlias.Items.Count; i++)
                    {
                        if (Value.IndexOf((string)cbGuidAlias.Items[i]) > -1)
                        {
                            cbAlias.SelectedIndex = i;
                            bFind = true;
                        }
                    }
                    if (!bFind) cbAlias.SelectedItem = null;
                }
            }
        }
    }
}
