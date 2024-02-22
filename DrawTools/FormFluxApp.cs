using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections;

namespace DrawTools
{
    public partial class FormFluxApp : Form
    {
        private Form1 parent;
        private bool bdgFluxInit; //, bdgServerOrigineInit;
        private XmlExcel xmlFlux; //, xmlDelLinkOut, xmlDelLinkIn;
        private bool bActiveAddAlias, bActiveDelAlias;
        private string sGuidNewAlias;

        public new Form1 Parent
        {
            get { return parent; }
            set { parent = value;}
        }

        //public FormFluxApp(Form1 p, XmlExcel xmlFlx, XmlExcel xmlDelLOut, XmlExcel xmlDelLIn)
        public FormFluxApp(Form1 p, XmlExcel xmlFlx)
        {
            Parent = p;
            bdgFluxInit = false;
            //bdgServerOrigineInit = false;
            InitializeComponent();
            InitEvent();
            xmlFlux = xmlFlx;
            //xmlDelLinkOut = xmlDelLOut;
            //xmlDelLinkIn = xmlDelLIn;
            InitWithXmlDataFromElFlux(xmlFlux.root, dgFlux);
            bdgFluxInit = true;
            bActiveAddAlias = false;
            bActiveDelAlias = false;
        }

        public void InitEvent()
        {
            dgFlux.CellContentClick += dgFlux_CellContentClick;
            dgFlux.CellValueChanged += dgFlux_CellValueChanged;
            dgFlux.SelectionChanged += dgFlux_SelectionChanged;

            dgServerOrigine.CellContentClick += dgServerOrigine_CellContentClick;
            dgServerOrigine.CellValueChanged += dgServerOrigine_CellValueChanged;
            dgServerOrigine.SelectionChanged += dgServerOrigine_SelectionChanged;
            

            dgServerCible.CellContentClick += dgServerCible_CellContentClick;
            dgServerCible.CellValueChanged += dgServerCible_CellValueChanged;
            dgServerCible.SelectionChanged += dgServerCible_SelectionChanged;

            dgIPOrigine.CellContentClick += dgIPOrigine_CellContentClick;
            dgIPOrigine.CellValueChanged += dgIPOrigine_CellValueChanged;
            dgLabelOrigine.CellContentClick += DgLabelOrigine_CellContentClick;
            dgLabelOrigine.CellValueChanged += DgLabelOrigine_CellValueChanged;

            dgIPCible.CellContentClick += dgIPCible_CellContentClick;
            dgIPCible.CellValueChanged += dgIPCible_CellValueChanged;
            dgIPCible.SelectionChanged += dgIPCible_SelectionChanged;
            dgLabelCible.CellContentClick += DgLabelCible_CellContentClick;
            dgLabelCible.CellValueChanged += DgLabelCible_CellValueChanged;

            dgAliasCible.CellContentClick += dgAliasCible_CellContentClick;
            dgAliasCible.CellValueChanged += dgAliasCible_CellValueChanged;
            bAddAlias.Click += new System.EventHandler(bAddAlias_Click);
            bDelAlias.Click += new System.EventHandler(bDelAlias_Click);
            dgAliasCible.SelectionChanged += dgAliasCible_SelectionChanged;
            dgAliasCible.Validating += dgAliasCible_Validating;

        }

        private void DgLabelCible_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (bdgFluxInit)
            {
                if (dgFlux.SelectedRows.Count != 0 && dgServerCible.SelectedRows.Count != 0 && dgLabelCible.SelectedRows.Count != 0)
                {
                    XmlElement root = xmlFlux.root;
                    XmlElement elFlux = parent.XmlFindElFromAtt(root, "Guid", (string)dgFlux.SelectedRows[0].Cells["GuidFlux"].Value);
                    if (elFlux != null)
                    {
                        XmlElement elRootServer = parent.XmlFindFirstElFromName(elFlux, "Cible");
                        if (elRootServer != null)
                        {
                            XmlElement elLabelValue = parent.XmlFindElFromAtt(elRootServer, "Guid", (string)dgLabelCible.SelectedRows[0].Cells[1].Value);

                            if (elLabelValue != null)
                            {

                                //if ((bool)dgLabelCible.SelectedRows[0].Cells["SelectedLabelCible"].Value)
                                if ((bool)dgLabelCible.SelectedRows[0].Cells[0].Value)
                                {
                                    //if (((XmlElement)elLabelValue.ParentNode).GetAttribute("Selected") != null) ((XmlElement)elLabelValue.ParentNode).SetAttribute("Selected", "Yes");
                                    //if (elLabelValue.GetAttribute("Selected") != null) elLabelValue.SetAttribute("Selected", "Yes");
                                    ((XmlElement)elLabelValue.ParentNode).SetAttribute("Selected", "Yes");
                                    elLabelValue.SetAttribute("Selected", "Yes");
                                }
                                else
                                {
                                    ((XmlElement)elLabelValue.ParentNode).SetAttribute("Selected", "No");
                                    elLabelValue.SetAttribute("Selected", "No");
                                }
                            }

                        }
                    }
                }
            }
        }

        private void DgLabelCible_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgLabelCible.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void DgLabelOrigine_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (bdgFluxInit)
            {
                if (dgFlux.SelectedRows.Count != 0 && dgServerOrigine.SelectedRows.Count != 0 && dgServerOrigine.SelectedRows.Count != 0)
                {
                    XmlElement root = xmlFlux.root;
                    XmlElement elFlux = parent.XmlFindElFromAtt(root, "Guid", (string)dgFlux.SelectedRows[0].Cells["GuidFlux"].Value);
                    if (elFlux != null)
                    {
                        XmlElement elRootServer = parent.XmlFindFirstElFromName(elFlux, "Origine");
                        if (elRootServer != null)
                        {
                            //XmlElement elLabelValue = parent.XmlFindElFromAtt(elLabelClass, "Guid", (string)dgLabelOrigine.SelectedRows[0].Cells["GuidLabelOrigine"].Value);
                            XmlElement elLabelValue = parent.XmlFindElFromAtt(elRootServer, "Guid", (string)dgLabelOrigine.SelectedRows[0].Cells[1].Value);

                            if (elLabelValue != null)
                            {
                                if ((bool)dgLabelOrigine.SelectedRows[0].Cells[0].Value)
                                {
                                    ((XmlElement)elLabelValue.ParentNode).SetAttribute("Selected", "Yes");
                                    elLabelValue.SetAttribute("Selected", "Yes");
                                }
                                else
                                {
                                    ((XmlElement)elLabelValue.ParentNode).SetAttribute("Selected", "No");
                                    elLabelValue.SetAttribute("Selected", "No");
                                }
                            }
                        }
                    }
                }
            }
        }

        private void DgLabelOrigine_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgLabelOrigine.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        

        void dgAliasCible_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            dgAliasCible.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        void dgAliasCible_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (bdgFluxInit)
            {
                if (dgFlux.SelectedRows.Count != 0 && dgServerCible.SelectedRows.Count != 0 && dgIPCible.SelectedRows.Count != 0 && dgAliasCible.SelectedRows.Count != 0)
                {
                    XmlElement root = xmlFlux.root;
                    XmlElement elFlux = parent.XmlFindElFromAtt(root, "Guid", (string)dgFlux.SelectedRows[0].Cells["GuidFlux"].Value);
                    if (elFlux != null)
                    {
                        XmlElement elRootServer = parent.XmlFindFirstElFromName(elFlux, "Cible");
                        if (elRootServer != null)
                        {
                            XmlElement elServer = parent.XmlFindElFromAtt(elRootServer, "Guid", (string)dgServerCible.SelectedRows[0].Cells["GuidServerCible"].Value);
                            if (elServer != null)
                            {
                                XmlElement elIP = parent.XmlFindElFromAtt(elServer, "Guid", (string)dgIPCible.SelectedRows[0].Cells["GuidIPCible"].Value);
                                if (elIP != null)
                                {
                                    XmlElement elAlias = parent.XmlFindElFromAtt(elIP, "Guid", (string)dgAliasCible.SelectedRows[0].Cells["GuidAliasCible"].Value);
                                    if (elAlias != null)
                                    {
                                        if ((bool)dgAliasCible.SelectedRows[0].Cells["SelectedAliasCible"].Value)
                                        { if (elAlias.GetAttribute("Selected") != null) elAlias.SetAttribute("Selected", "Yes"); }
                                        else
                                        { if (elAlias.GetAttribute("Selected") != null) elAlias.SetAttribute("Selected", "No"); }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void dgIPCible_Changed()
        {
            dgAliasCible.Rows.Clear();
            if (dgFlux.SelectedRows.Count != 0 && dgServerCible.SelectedRows.Count != 0 && dgIPCible.SelectedRows.Count != 0)
            {
                XmlElement root = xmlFlux.root;
                XmlElement elFlux = parent.XmlFindElFromAtt(root, "Guid", (string)dgFlux.SelectedRows[0].Cells["GuidFlux"].Value);
                if (elFlux != null)
                {
                    XmlElement elRootServer = parent.XmlFindFirstElFromName(elFlux, "Cible");
                    if (elRootServer != null)
                    {
                        XmlElement elServer = parent.XmlFindElFromAtt(elRootServer, "Guid", (string)dgServerCible.SelectedRows[0].Cells["GuidServerCible"].Value);
                        if (elServer != null)
                        {
                            XmlElement elIP = parent.XmlFindElFromAtt(elServer, "Guid", (string)dgIPCible.SelectedRows[0].Cells["GuidIPCible"].Value);
                            if (elIP != null) InitWithXmlDataFromElAlias(elIP, dgAliasCible);
                        }
                            
                    }
                }
            }
        }

        void dgIPCible_SelectionChanged(object sender, EventArgs e)
        {
            dgIPCible_Changed();
            //throw new NotImplementedException();
        }
        

        void dgIPCible_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            dgIPCible.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        void dgIPCible_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (bdgFluxInit)
            {
                if (dgFlux.SelectedRows.Count != 0 && dgServerCible.SelectedRows.Count != 0 && dgIPCible.SelectedRows.Count != 0)
                {
                    XmlElement root = xmlFlux.root;
                    XmlElement elFlux = parent.XmlFindElFromAtt(root, "Guid", (string)dgFlux.SelectedRows[0].Cells["GuidFlux"].Value);
                    if (elFlux != null)
                    {
                        XmlElement elRootServer = parent.XmlFindFirstElFromName(elFlux, "Cible");
                        if (elRootServer != null)
                        {
                            XmlElement elServer = parent.XmlFindElFromAtt(elRootServer, "Guid", (string)dgServerCible.SelectedRows[0].Cells["GuidServerCible"].Value);
                            if (elServer != null)
                            {
                                XmlElement elIP = parent.XmlFindElFromAtt(elServer, "Guid", (string)dgIPCible.SelectedRows[0].Cells["GuidIPCible"].Value);
                                if (elIP != null)
                                {
                                    if ((bool)dgIPCible.SelectedRows[0].Cells["SelectedIPCible"].Value)
                                    { 
                                        if (elIP.GetAttribute("Selected") != null) elIP.SetAttribute("Selected", "Yes");
                                        parent.XmlAllSetAttributValueFromEl(elIP, "Selected", "Yes");
                                    }
                                    else
                                    { 
                                        if (elIP.GetAttribute("Selected") != null) elIP.SetAttribute("Selected", "No");
                                        parent.XmlAllSetAttributValueFromEl(elIP, "Selected", "No");
                                    }
                                }
                            }
                        }
                    }
                }
                dgIPCible_Changed();
            }
        }

        void dgIPOrigine_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            dgIPOrigine.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        void dgIPOrigine_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (bdgFluxInit)
            {
                if (dgFlux.SelectedRows.Count != 0 && dgServerOrigine.SelectedRows.Count != 0 && dgIPOrigine.SelectedRows.Count != 0)
                {
                    XmlElement root = xmlFlux.root;
                    XmlElement elFlux = parent.XmlFindElFromAtt(root, "Guid", (string)dgFlux.SelectedRows[0].Cells["GuidFlux"].Value);
                    if (elFlux != null)
                    {
                        XmlElement elRootServer = parent.XmlFindFirstElFromName(elFlux, "Origine");
                        if (elRootServer != null)
                        {
                            XmlElement elServer = parent.XmlFindElFromAtt(elRootServer, "Guid", (string)dgServerOrigine.SelectedRows[0].Cells["GuidServerOrigine"].Value);
                            if (elServer != null)
                            {
                                XmlElement elIP = parent.XmlFindElFromAtt(elRootServer, "Guid", (string)dgIPOrigine.SelectedRows[0].Cells["GuidIPOrigine"].Value);
                                if (elIP != null)
                                {
                                    if ((bool)dgIPOrigine.SelectedRows[0].Cells["SelectedIPOrigine"].Value)
                                    { if (elIP.GetAttribute("Selected") != null) elIP.SetAttribute("Selected", "Yes"); }
                                    else
                                    { if (elIP.GetAttribute("Selected") != null) elIP.SetAttribute("Selected", "No"); }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void dgServerCible_Changed()
        {
            dgIPCible.Rows.Clear();
            dgLabelCible.Rows.Clear();
            if (dgFlux.SelectedRows.Count != 0 && dgServerCible.SelectedRows.Count != 0)
            {
                XmlElement root = xmlFlux.root;
                XmlElement elFlux = parent.XmlFindElFromAtt(root, "Guid", (string)dgFlux.SelectedRows[0].Cells["GuidFlux"].Value);
                if (elFlux != null)
                {
                    XmlElement elRootServer = parent.XmlFindFirstElFromName(elFlux, "Cible");
                    if (elRootServer != null)
                    {
                        XmlElement elServer = parent.XmlFindElFromAtt(elRootServer, "Guid", (string)dgServerCible.SelectedRows[0].Cells["GuidServerCible"].Value);
                        if (elRootServer != null) InitWithXmlDataFromElServer(elServer, dgIPCible, dgLabelCible);
                    }
                }
            }
            dgIPCible.Sort(dgIPCible.Columns[2], ListSortDirection.Ascending);
            dgLabelCible.Sort(dgLabelCible.Columns[2], ListSortDirection.Ascending);
        }

        void dgServerCible_SelectionChanged(object sender, EventArgs e)
        {
            dgServerCible_Changed();
            //throw new NotImplementedException();
        }


        void dgServerCible_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            dgServerCible.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        void dgServerCible_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (bdgFluxInit)
            {
                if (dgFlux.SelectedRows.Count != 0 && dgServerCible.SelectedRows.Count != 0)
                {
                    XmlElement root = xmlFlux.root;
                    XmlElement elFlux = parent.XmlFindElFromAtt(root, "Guid", (string)dgFlux.SelectedRows[0].Cells["GuidFlux"].Value);
                    if (elFlux != null)
                    {
                        XmlElement elRootServer = parent.XmlFindFirstElFromName(elFlux, "Cible");
                        if (elRootServer != null)
                        {
                            XmlElement elServer = parent.XmlFindElFromAtt(elRootServer, "Guid", (string)dgServerCible.SelectedRows[0].Cells["GuidServerCible"].Value);
                            if (elServer != null)
                            {
                                if ((bool)dgServerCible.SelectedRows[0].Cells["SelectedServerCible"].Value)
                                {
                                    if (elServer.GetAttribute("Selected") != null) elServer.SetAttribute("Selected", "Yes");
                                    parent.XmlAllSetAttributValueFromEl(elServer, "Selected", "Yes");
                                }
                                else
                                {
                                    if (elServer.GetAttribute("Selected") != null) elServer.SetAttribute("Selected", "No");
                                    parent.XmlAllSetAttributValueFromEl(elServer, "Selected", "No");
                                }
                            }
                        }
                    }
                }
                dgServerCible_Changed();
            }
        }

        public void dgServerOrigine_Changed()
        {
            dgIPOrigine.Rows.Clear();
            dgLabelOrigine.Rows.Clear();
            if (dgFlux.SelectedRows.Count != 0 && dgServerOrigine.SelectedRows.Count != 0)
            {
                XmlElement root = xmlFlux.root;
                XmlElement elFlux = parent.XmlFindElFromAtt(root, "Guid", (string)dgFlux.SelectedRows[0].Cells["GuidFlux"].Value);
                if (elFlux != null)
                {
                    XmlElement elRootServer = parent.XmlFindFirstElFromName(elFlux, "Origine");
                    if (elRootServer != null)
                    {
                        XmlElement elServer = parent.XmlFindElFromAtt(elRootServer, "Guid", (string)dgServerOrigine.SelectedRows[0].Cells["GuidServerOrigine"].Value);
                        if (elRootServer != null) InitWithXmlDataFromElServer(elServer, dgIPOrigine, dgLabelOrigine);
                    }
                }
            }
            dgIPOrigine.Sort(dgIPOrigine.Columns[2], ListSortDirection.Ascending);
            dgLabelOrigine.Sort(dgLabelOrigine.Columns[2], ListSortDirection.Ascending);
        }

        void dgServerOrigine_SelectionChanged(object sender, EventArgs e)
        {
            dgServerOrigine_Changed();
            
            //throw new NotImplementedException();
        }

        void dgServerOrigine_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            dgServerOrigine.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        void dgServerOrigine_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (bdgFluxInit)
            {
                if (dgFlux.SelectedRows.Count != 0 && dgServerOrigine.SelectedRows.Count != 0)
                {
                    XmlElement root = xmlFlux.root;
                    XmlElement elFlux = parent.XmlFindElFromAtt(root, "Guid", (string)dgFlux.SelectedRows[0].Cells["GuidFlux"].Value);
                    if (elFlux != null)
                    {
                        XmlElement elRootServer = parent.XmlFindFirstElFromName(elFlux, "Origine");
                        if (elRootServer != null)
                        {
                            XmlElement elServer = parent.XmlFindElFromAtt(elRootServer, "Guid", (string)dgServerOrigine.SelectedRows[0].Cells["GuidServerOrigine"].Value);
                            if (elServer != null)
                            {
                                if ((bool)dgServerOrigine.SelectedRows[0].Cells["SelectedServerOrigine"].Value)
                                {
                                    if (elServer.GetAttribute("Selected") != null) elServer.SetAttribute("Selected", "Yes");
                                    parent.XmlAllSetAttributValueFromEl(elServer, "Selected", "Yes", "LabelClass"); 
                                }
                                else
                                {
                                    if (elServer.GetAttribute("Selected") != null) elServer.SetAttribute("Selected", "No");
                                    parent.XmlAllSetAttributValueFromEl(elServer, "Selected", "No", "LabelClass");
                                }
                            }
                        }
                    }
                }
                dgServerOrigine_Changed();
            }
        }

        void dgFlux_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            dgFlux.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        void dgFlux_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (bdgFluxInit)
            {
                if (dgFlux.SelectedRows[0].Cells["SelectedFlux"].Value != null && (bool)dgFlux.SelectedRows[0].Cells["SelectedFlux"].Value)
                {
                    XmlElement root = xmlFlux.root;
                    XmlElement elFlux = parent.XmlFindElFromAtt(root, "Guid", (string)dgFlux.SelectedRows[0].Cells["GuidFlux"].Value);
                    if (elFlux != null)
                    {
                        if (elFlux.GetAttribute("Selected") != null) elFlux.SetAttribute("Selected", "Yes");
                        parent.XmlAllSetAttributValueFromEl(elFlux, "Selected", "Yes");
                    }
                }
                else
                {
                    XmlElement root = xmlFlux.root;
                    XmlElement elFlux = parent.XmlFindElFromAtt(root, "Guid", (string)dgFlux.SelectedRows[0].Cells["GuidFlux"].Value);
                    if (elFlux != null)
                    {
                        if (elFlux.GetAttribute("Selected") != null) elFlux.SetAttribute("Selected", "No");
                        parent.XmlAllSetAttributValueFromEl(elFlux, "Selected", "No");
                    }
                }
                dgFlux_Changed();
            }
        }

        public void dgFlux_Changed()
        {
            dgServerOrigine.Rows.Clear();
            dgServerCible.Rows.Clear();
            //dgAliasCible.Rows.Clear();
            InitServer((string)dgFlux.SelectedRows[0].Cells["GuidFlux"].Value, dgServerOrigine, "Origine");
            InitServer((string)dgFlux.SelectedRows[0].Cells["GuidFlux"].Value, dgServerCible, "Cible");
        }

        void dgFlux_SelectionChanged(object sender, System.EventArgs e)
        {
            dgFlux_Changed();
        }

        public void InitServer(string sGuidFlux, DataGridView dg, string sTypeExtremite)
        {
            XmlElement root = xmlFlux.root;
            XmlElement elFind = parent.XmlFindElFromAtt(root, "Guid", sGuidFlux);
            IEnumerator ienum = root.GetEnumerator();

            if (elFind != null)
            {
                XmlElement elServer = parent.XmlFindFirstElFromName(elFind, sTypeExtremite);
                if (elServer != null) InitWithXmlDataFromEl(elServer, dg);
            }
        }

        public void InitWithXmlDataFromElLabelClass(XmlElement el, DataGridView dg)
        {
            IEnumerator ienum = el.GetEnumerator();
            XmlNode Node;

            dg.Rows.Clear();

            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element && Node.Name == "LabelClass" && (Node.ParentNode != xmlFlux.root || Node.Name == "Flux"))
                {
                    bool bSelected = true;
                    if (((XmlElement)Node).GetAttribute("Selected") == "No") bSelected = false;
                    XmlElement elValue = parent.XmlFindFirstElFromName((XmlElement)Node, "LabelValue");
                    if (elValue != null)
                    {
                        bSelected = true;
                        if (((XmlElement)elValue).GetAttribute("Selected") == "No") bSelected = false;
                        object[] row = { bSelected, ((XmlElement)elValue).GetAttribute("Guid"), ((XmlElement)elValue).GetAttribute("Nom") };
                        dg.Rows.Add(row);
                    }
                }
            }

        }

        public void InitWithXmlDataFromElServer(XmlElement el, DataGridView dgIP, DataGridView dgLabel)
        {
            IEnumerator ienum = el.GetEnumerator();
            XmlNode Node;

            dgIP.Rows.Clear();
            dgLabel.Rows.Clear();

            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element && Node.Name =="NCard" && (Node.ParentNode != xmlFlux.root || Node.Name == "Flux"))
                {
                    bool bSelected = true;
                    if (((XmlElement)Node).GetAttribute("Selected") == "No") bSelected = false;
                    object[] row = { bSelected, ((XmlElement)Node).GetAttribute("Guid"), ((XmlElement)Node).GetAttribute("Nom") + " [" + ((XmlElement)Node).GetAttribute("IP") + "]" };
                    dgIP.Rows.Add(row);
                }
                if (Node.NodeType == XmlNodeType.Element && Node.Name == "LabelClass" && (Node.ParentNode != xmlFlux.root || Node.Name == "Flux"))
                {
                    bool bSelected = true;
                    XmlElement elLabelValue = parent.XmlFindFirstElFromName((XmlElement)Node, "LabelValue");

                    if (elLabelValue != null)
                    {
                        if (elLabelValue.GetAttribute("Selected") == "No") bSelected = false;
                        object[] row = { bSelected, elLabelValue.GetAttribute("Guid"), elLabelValue.GetAttribute("Nom")  };
                        dgLabel.Rows.Add(row);
                    }
                }
            }
        }

        public void InitWithXmlDataFromElFlux(XmlElement el, DataGridView dg)
        {
            IEnumerator ienum = el.GetEnumerator();
            XmlNode Node;
            
            dg.Rows.Clear();

            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element && (Node.ParentNode != xmlFlux.root || Node.Name == "Flux"))
                {
                    bool bSelected = true;
                    if (((XmlElement)Node).GetAttribute("Selected") == "No") bSelected = false;
                    object[] row = { bSelected, ((XmlElement)Node).GetAttribute("Guid"), ((XmlElement)Node).GetAttribute("Id") + " " + ((XmlElement)Node).GetAttribute("Nom") }; 
                    dg.Rows.Add(row);
                }
            }
            
        }

        public void InitWithXmlDataFromElAlias(XmlElement el, DataGridView dg)
        {
            IEnumerator ienum = el.GetEnumerator();
            XmlNode Node;

            dg.Rows.Clear();

            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element && Node.Name == "Alias")
                {
                    bool bSelected = true;
                    if (((XmlElement)Node).GetAttribute("Selected") == "No") bSelected = false;
                    object[] row = { bSelected, ((XmlElement)Node).GetAttribute("Guid"), ((XmlElement)Node).GetAttribute("Nom") };
                    dg.Rows.Add(row);
                }
            }
        }

        public void InitWithXmlDataFromEl(XmlElement el, DataGridView dg)
        {
            IEnumerator ienum = el.GetEnumerator();
            XmlNode Node;
            bool bSelected;

            dg.Rows.Clear();

            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element && Node.ParentNode != xmlFlux.root)
                {
                    bSelected = true;
                    if (((XmlElement)Node).GetAttribute("Selected") == "No") bSelected = false;
                    object[] row = { bSelected, ((XmlElement)Node).GetAttribute("Guid"), ((XmlElement)Node).GetAttribute("Nom") };
                    dg.Rows.Add(row);
                    
                    
                    /*
                    IEnumerator ienumLabel = Node.GetEnumerator();
                    XmlNode NodeLabel;
                    while (ienumLabel.MoveNext())
                    {
                        NodeLabel = (XmlNode)ienumLabel.Current;
                        if (NodeLabel.NodeType == XmlNodeType.Element && NodeLabel.Name == "LabelClass")
                        {
                            bSelected = true;
                            if (((XmlElement)NodeLabel).GetAttribute("Selected") == "No") bSelected = false;
                            object[] rowLabel = { bSelected, ((XmlElement)NodeLabel).GetAttribute("Guid"), ((XmlElement)NodeLabel).GetAttribute("Nom") };
                            dgLabelClass.Rows.Add(rowLabel);
                        }
                    }
                    */
                }
            }

        }

        private bool GetXmlNCardLinkAlias(string sGuidFlux, XmlElement elFluxRead, XmlDocument xmlDoc)
        {
            bool bretour = false;
            string sGuidNCard = elFluxRead.GetAttribute("Guid");

            if (elFluxRead != null)
            {
                IEnumerator ienum = elFluxRead.GetEnumerator();
                XmlNode Node;

                while (ienum.MoveNext())
                {
                    Node = (XmlNode)ienum.Current;
                    if (Node.NodeType == XmlNodeType.Element)
                    {
                        XmlElement el = (XmlElement)Node;
                        if (el.Name == "Alias" && el.GetAttribute("Selected") == "Yes")
                        {
                                XmlElement root = xmlDoc.DocumentElement;
                                XmlElement elLink = xmlDoc.CreateElement("NCardLinkIn");
                                if (Parent.oCnxBase.bAttribut)
                                {
                                    elLink.SetAttribute("SearchKey", "GuidNCard,GuidAlias,GuidTechLink");
                                    XmlElement elAtts = xmlDoc.CreateElement("Attributs");
                                    parent.XmlSetAttFromEl(xmlDoc, elAtts, "GuidNCard", "s", sGuidNCard);
                                    parent.XmlSetAttFromEl(xmlDoc, elAtts, "GuidAlias", "s", el.GetAttribute("Guid"));
                                    parent.XmlSetAttFromEl(xmlDoc, elAtts, "GuidTechLink", "s", sGuidFlux);
                                    elLink.AppendChild(elAtts);
                                }
                                else
                                {
                                    elLink.SetAttribute("GuidNCard", sGuidNCard);
                                    elLink.SetAttribute("GuidAlias", el.GetAttribute("Guid"));
                                    elLink.SetAttribute("GuidTechLink", sGuidFlux);
                                }
                                root.AppendChild(elLink);
                                bretour = true;
                        }
                    }
                }
            }
            return bretour;
        }

        private bool GetXmlLink(string sGuidFlux, XmlElement elFluxRead, XmlDocument xmlDoc)
        {
            bool bretour = false;
            string sSens = "Out";

            if (elFluxRead.ParentNode.Name == "Cible") sSens = "In";
            if (elFluxRead != null)
            {
                IEnumerator ienum = elFluxRead.GetEnumerator();
                XmlNode Node;

                while (ienum.MoveNext())
                {
                    Node = (XmlNode)ienum.Current;
                    if (Node.NodeType == XmlNodeType.Element)
                    {
                        XmlElement el = (XmlElement)Node;
                        if (el.Name == "NCard" && el.GetAttribute("Selected") == "Yes")
                        {
                            if (sSens == "In") bretour |= GetXmlNCardLinkAlias(sGuidFlux, el, xmlDoc);
                            else
                            {
                                XmlElement root = xmlDoc.DocumentElement;
                                XmlElement elLink = xmlDoc.CreateElement("NCardLink" + sSens);
                                if (Parent.oCnxBase.bAttribut)
                                {
                                    elLink.SetAttribute("SearchKey", "GuidNCard,GuidTechLink");
                                    XmlElement elAtts = xmlDoc.CreateElement("Attributs");
                                    parent.XmlSetAttFromEl(xmlDoc, elAtts, "GuidNCard", "s", el.GetAttribute("Guid"));
                                    parent.XmlSetAttFromEl(xmlDoc, elAtts, "GuidTechLink", "s", sGuidFlux);
                                    elLink.AppendChild(elAtts);
                                }
                                else
                                {
                                    elLink.SetAttribute("GuidNCard", el.GetAttribute("Guid"));
                                    elLink.SetAttribute("GuidTechLink", sGuidFlux);
                                }
                                root.AppendChild(elLink);
                                bretour = true;
                            }
                        }
                        if(el.Name == "LabelClass" && el.GetAttribute("Selected") == "Yes")
                        {
                            XmlElement elLabelValue= (XmlElement) el.FirstChild;
                            if(elLabelValue != null && elLabelValue.Name == "LabelValue" && elLabelValue.GetAttribute("Selected") == "Yes")
                            {
                                XmlElement root = xmlDoc.DocumentElement;
                                XmlElement elLink = xmlDoc.CreateElement("LabelLink" + sSens);
                                if (Parent.oCnxBase.bAttribut)
                                {
                                    elLink.SetAttribute("SearchKey", "GuidVue,GuidTechLink,GuidLabel");
                                    XmlElement elAtts = xmlDoc.CreateElement("Attributs");
                                    parent.XmlSetAttFromEl(xmlDoc, elAtts, "GuidVue", "s", Parent.GuidVue.ToString());
                                    parent.XmlSetAttFromEl(xmlDoc, elAtts, "GuidTechLink", "s", sGuidFlux);
                                    parent.XmlSetAttFromEl(xmlDoc, elAtts, "GuidLabel", "s", elLabelValue.GetAttribute("Guid"));
                                    elLink.AppendChild(elAtts);
                                }
                                else
                                {
                                    elLink.SetAttribute("GuidVue", Parent.GuidVue.ToString());
                                    elLink.SetAttribute("GuidAlias", sGuidFlux);
                                    elLink.SetAttribute("GuidTechLink", elLabelValue.GetAttribute("Guid"));
                                }
                                root.AppendChild(elLink);
                                bretour = true;
                            }
                            
                        }
                    }
                }
            }
            return bretour;
        }

        private bool GetXmlNCardLinkCard(string sGuidFlux, XmlElement elFluxRead, XmlDocument xmlDoc)
        {
            bool bretour = false;
            string sSens = "Out";

            if (elFluxRead.ParentNode.Name == "Cible") sSens = "In";
            if (elFluxRead != null)
            {
                IEnumerator ienum = elFluxRead.GetEnumerator();
                XmlNode Node;

                while (ienum.MoveNext())
                {
                    Node = (XmlNode)ienum.Current;
                    if (Node.NodeType == XmlNodeType.Element)
                    {
                        XmlElement root = xmlDoc.DocumentElement;
                        XmlElement el = (XmlElement)Node;
                        if (el.Name == "NCard" && el.GetAttribute("Selected") == "Yes")
                        {
                            if (sSens == "In") bretour |= GetXmlNCardLinkAlias(sGuidFlux, el, xmlDoc);
                            else
                            {
                                XmlElement elLink = xmlDoc.CreateElement("NCardLink" + sSens);
                                if (Parent.oCnxBase.bAttribut)
                                {
                                    elLink.SetAttribute("SearchKey", "GuidNCard,GuidTechLink");
                                    XmlElement elAtts = xmlDoc.CreateElement("Attributs");
                                    parent.XmlSetAttFromEl(xmlDoc, elAtts, "GuidNCard", "s", el.GetAttribute("Guid"));
                                    parent.XmlSetAttFromEl(xmlDoc, elAtts, "GuidTechLink", "s", sGuidFlux);
                                    elLink.AppendChild(elAtts);
                                }
                                else
                                {
                                    elLink.SetAttribute("GuidNCard", el.GetAttribute("Guid"));
                                    elLink.SetAttribute("GuidTechLink", sGuidFlux);
                                }
                                root.AppendChild(elLink);
                                bretour = true;
                            }
                        }
                        if(el.Name == "LabelClass" && el.GetAttribute("Selected") == "Yes")
                        {
                            XmlElement elLabelValue = parent.XmlFindFirstElFromName(el, "LabelValue");
                            if (elLabelValue != null && elLabelValue.GetAttribute("Selected") == "Yes")
                            {
                                XmlElement elLink = xmlDoc.CreateElement("LabelLink" + sSens);
                                if (Parent.oCnxBase.bAttribut)
                                {
                                    elLink.SetAttribute("SearchKey", "GuidVue,GuidTechLink,GuidLabel");
                                    XmlElement elAtts = xmlDoc.CreateElement("Attributs");
                                    parent.XmlSetAttFromEl(xmlDoc, elAtts, "GuidVue", "s", Parent.GuidVue.ToString());
                                    parent.XmlSetAttFromEl(xmlDoc, elAtts, "GuidTechLink", "s", sGuidFlux);
                                    parent.XmlSetAttFromEl(xmlDoc, elAtts, "GuidLabel", "s", elLabelValue.GetAttribute("Guid"));
                                    elLink.AppendChild(elAtts);
                                }
                                else
                                {
                                    elLink.SetAttribute("GuidVue", Parent.GuidVue.ToString());
                                    elLink.SetAttribute("GuidAlias", sGuidFlux);
                                    elLink.SetAttribute("GuidTechLink", elLabelValue.GetAttribute("Guid"));
                                }
                                root.AppendChild(elLink);
                                bretour = true;

                            }
                        }
                    }
                }
            }
            return bretour;
        }

        /*
        private bool GetXmlLabelLink(string sGuidFlux, XmlElement elFluxRead, XmlDocument xmlDoc)
        {
            bool bretour = false;
            string sSens = "Out";

            if (elFluxRead.ParentNode.Name == "Cible") sSens = "In";
            if (elFluxRead != null)
            {
                IEnumerator ienumLabel = elFluxRead.GetEnumerator();
                XmlNode NodeLabel;

                while (ienumLabel.MoveNext())
                {
                    NodeLabel = (XmlNode)ienumLabel.Current;
                    if (NodeLabel.NodeType == XmlNodeType.Element)
                    {
                        XmlElement elLabel = (XmlElement)NodeLabel;
                        if (elLabel.Name == "LabelValue" && elLabel.GetAttribute("Selected") == "Yes")
                        {
                            XmlElement root = xmlDoc.DocumentElement;
                            XmlElement elLink = xmlDoc.CreateElement("LabelLink" + sSens);
                            if (Parent.oCnxBase.bAttribut)
                            {
                                elLink.SetAttribute("SearchKey", "GuidVue,GuidTechLink,GuidLabel");
                                XmlElement elAtts = xmlDoc.CreateElement("Attributs");
                                parent.XmlSetAttFromEl(xmlDoc, elAtts, "GuidVue", "s", Parent.GuidVue.ToString());
                                parent.XmlSetAttFromEl(xmlDoc, elAtts, "GuidTechLink", "s", sGuidFlux);
                                parent.XmlSetAttFromEl(xmlDoc, elAtts, "GuidLabel", "s", elLabel.GetAttribute("Guid"));
                                elLink.AppendChild(elAtts);
                            }
                            else
                            {
                                elLink.SetAttribute("GuidVue", Parent.GuidVue.ToString());
                                elLink.SetAttribute("GuidAlias", sGuidFlux);
                                elLink.SetAttribute("GuidTechLink", elLabel.GetAttribute("Guid"));
                            }
                            root.AppendChild(elLink);
                            bretour = true;
                        }
                    }
                }
            }
            return bretour;
        }
        */

        private bool GetXmlNCardLinkServeur(string sGuidFlux, XmlElement elFluxRead, XmlDocument xmlDoc)
        {
            bool bretour = false;
            if (elFluxRead != null)
            {
                IEnumerator ienum = elFluxRead.GetEnumerator();
                XmlNode Node;

                while (ienum.MoveNext())
                {
                    Node = (XmlNode)ienum.Current;
                    if (Node.NodeType == XmlNodeType.Element)
                    {
                        XmlElement el = (XmlElement)Node;
                        if (el.GetAttribute("Selected") == "Yes") bretour |= GetXmlNCardLinkCard(sGuidFlux, el, xmlDoc);
                    }
                }
            }
            return bretour;
        }

        public void MajDBAlias(XmlElement elParent, XmlElement el1, XmlElement el2)
        {
            // elParent -> NomServeur
            // el1-> Ncard

            // suppression des alias de la Ncard si demande
            ArrayList lstAlias = new ArrayList();
            if(Parent.oCnxBase.CBRecherche("Select GuidAlias From Alias Where GuidNCard='" + el1.GetAttribute("Guid") + "'"))
            {
                while (Parent.oCnxBase.Reader.Read())
                {
                    //if (Parent.XmlFindElFromAttValue(el1, "Guid", Parent.oCnxBase.Reader.GetString(0)) == null) lstAlias.Add(Parent.oCnxBase.Reader.GetString(0));
                    if (Parent.XmlFindElFromAtt(el1, "Guid", Parent.oCnxBase.Reader.GetString(0)) == null) lstAlias.Add(Parent.oCnxBase.Reader.GetString(0));
                }
            }
            Parent.oCnxBase.CBReaderClose();
            for (int i = 0; i < lstAlias.Count; i++)
            {
                if (Parent.oCnxBase.CBRecherche("Select NomApplication, NomTechLink From NCardLinkIn, TechLink, GTechLink, DansVue, Vue, AppVersion, Application Where NCardLinkIn.GuidTechLink=TechLink.GuidTechLink and TechLink.GuidTechLink=GTechLink.GuidTechLink and GTechLink.GuidGTechLink=DansVue.GuidObjet and DansVue.GuidGVue=Vue.GuidGVue and Vue.GuidAppVersion=AppVersion.GuidAppVersion and AppVersion.GuidApplication=Application.GuidApplication and GuidAlias='" + lstAlias[i] + "'"))
                {
                    string msg = "L'alias est utilisé par les flux des applications suivants:\n";
                    while (Parent.oCnxBase.Reader.Read()) msg += "   -" + Parent.oCnxBase.Reader.GetString(1) + " (" + Parent.oCnxBase.Reader.GetString(0) + ")\n";
                    Parent.oCnxBase.CBReaderClose();
                    msg += "Voulez-vous continuer?";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    result = MessageBox.Show(msg, "suppression", buttons);
                    if (result == System.Windows.Forms.DialogResult.Yes) Parent.oCnxBase.CBWrite("Delete from Alias Where GuidAlias='" + lstAlias[i] + "'");
                }
                else
                {
                    Parent.oCnxBase.CBReaderClose();
                    Parent.oCnxBase.CBWrite("Delete from Alias Where GuidAlias='" + lstAlias[i] + "'");
                }
            }

            // Création des alias à partir du Xml
            XmlElement root = xmlFlux.root;
            XmlElement elNCard = xmlFlux.docXml.CreateElement("NCard");
            IEnumerator ienum = el1.GetEnumerator();
            XmlNode Node;

            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element && Node.Name == "Alias")
                {
                    XmlElement el = (XmlElement)Node;
                    XmlElement elAlias = xmlFlux.docXml.CreateElement("Alias");
                    if (Parent.oCnxBase.bAttribut)
                    {
                        elAlias.SetAttribute("SearchKey", "GuidAlias");
                        XmlElement elAtts = xmlFlux.docXml.CreateElement("Attributs");
                        parent.XmlSetAttFromEl(xmlFlux.docXml, elAtts, "GuidAlias", "s", el.GetAttribute("Guid"));
                        parent.XmlSetAttFromEl(xmlFlux.docXml, elAtts, "NomAlias", "s", el.GetAttribute("Nom"));
                        parent.XmlSetAttFromEl(xmlFlux.docXml, elAtts, "GuidNCard", "s", el1.GetAttribute("Guid"));
                        elAlias.AppendChild(elAtts);
                    }
                    else
                    {
                        elAlias.SetAttribute("SearchKey", "sGuidAlias");
                        elAlias.SetAttribute("sGuidAlias", el.GetAttribute("Guid"));
                        elAlias.SetAttribute("sNomAlias", el.GetAttribute("Nom"));
                        elAlias.SetAttribute("sGuidNCard", el1.GetAttribute("Guid"));
                    }
                    elNCard.AppendChild(elAlias);
                 }
            }
            Parent.XmlinsertBaseFromXml(elNCard);
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            // Supprimer de la base ts les flux rattaches aux NCard de la vue (Source & Cible)
            //parent.XmldeleteBaseFromXml(xmlDelLinkOut.root);
            //parent.XmldeleteBaseFromXml(xmlDelLinkIn.root);

            parent.XmldeleteBaseFromXml(Parent.XmlFindFirstElFromName(xmlFlux.root, "DelLinkOut"));
            parent.XmldeleteBaseFromXml(Parent.XmlFindFirstElFromName(xmlFlux.root, "DelLinkIn"));

            xmlFlux.docXml.Save("\\dat\\ListTest.xml");

            XmlElement root = xmlFlux.root;
            IEnumerator ienum = root.GetEnumerator();
            XmlNode Node;

            // Supprimer & créer les alias
            parent.XmlExecActionElFromName(root, "NCard", null, new Form1.XMLEXECACTIONELFROMNAME(MajDBAlias));


            //Etat de xml à sauvegarder
            xmlFlux.docXml.Save("\\dat\\FluxTest.xml");

            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    XmlElement elFlux = (XmlElement) Node;
                    if (elFlux.Name == "Flux" && elFlux.GetAttribute("Selected")=="Yes")
                    {
                        XmlDocument xmlNcardLink = new XmlDocument();
                        xmlNcardLink.LoadXml("<Flux></Flux>");
                        
                        if (GetXmlNCardLinkServeur(elFlux.GetAttribute("Guid"), parent.XmlFindFirstElFromName(elFlux, "Origine"), xmlNcardLink) && GetXmlNCardLinkServeur(elFlux.GetAttribute("Guid"), parent.XmlFindFirstElFromName(elFlux, "Cible"), xmlNcardLink))
                            parent.XmlinsertBaseFromXml(xmlNcardLink.DocumentElement);
                    }
                }
            }
            Close();
            //Parent.ClearVue(false);
            Parent.drawArea.GraphicsList.Clear();
            parent.LoadVue();
        }

        private void bAnnuler_Click(object sender, EventArgs e)
        {
            Close();
            // retablir les links supprimer avant la forme
            //parent.oCnxBase.RestoreLink();
        }

        public void AddAlias(XmlElement elParent, XmlElement el1, XmlElement el2)
        {
            XmlElement el = Parent.XmlCopyEl(xmlFlux.docXml, el2);
            el1.AppendChild(el);
        }

        void dgAliasCible_Validating(object sender, EventArgs e)
        {
            this.NomAliasCible.ReadOnly = true;
            if (bdgFluxInit & bActiveAddAlias & !bActiveDelAlias)
            {
                XmlElement root = xmlFlux.root;
                XmlElement el = xmlFlux.docXml.CreateElement("Alias");
                for (int i = 0; i < dgAliasCible.Rows.Count; i++)
                {
                    if (sGuidNewAlias == (string)dgAliasCible.Rows[i].Cells["GuidAliasCible"].Value)
                    {
                        el.SetAttribute("Guid", (string)dgAliasCible.Rows[i].Cells["GuidAliasCible"].Value);
                        el.SetAttribute("Nom", (string)dgAliasCible.Rows[i].Cells["NomAliasCible"].Value);
                        el.SetAttribute("Selected", "Yes");

                        parent.XmlExecActionElFromAtt(root, "Guid", (string)dgIPCible.SelectedRows[0].Cells["GuidIpCible"].Value, el, new Form1.XMLEXECACTIONELFROMATT(AddAlias));
                    }
                }
                bActiveAddAlias = false;
                sGuidNewAlias = null;
            }
        }

        void dgAliasCible_SelectionChanged(object sender, EventArgs e)
        {
            this.NomAliasCible.ReadOnly = true;
        }

        private void bAddAlias_Click(object sender, EventArgs e)
        {
            bActiveAddAlias = true;
            sGuidNewAlias = Guid.NewGuid().ToString();
            if (bdgFluxInit)
            {
                object[] row = { true, sGuidNewAlias, "NewAlias" };
                dgAliasCible.Rows.Add(row);
                dgAliasCible.Rows[dgAliasCible.Rows.Count - 1].Cells["NomAliasCible"].Selected = true;
                this.NomAliasCible.ReadOnly = false;
                dgAliasCible.Focus();

            }
        }

        public void DeleteAlias(XmlElement elParent, XmlElement el1, XmlElement el2)
        {
            elParent.RemoveChild(el1);
        }

        private void bDelAlias_Click(object sender, EventArgs e)
        {
            bActiveDelAlias = true;
            if (dgAliasCible.SelectedRows.Count != 0)
            {
                if (bdgFluxInit)
                {
                    XmlElement root = xmlFlux.root;
                    XmlElement el = xmlFlux.docXml.CreateElement("Alias");
                    el.SetAttribute("Guid", (string)dgAliasCible.SelectedRows[0].Cells["GuidAliasCible"].Value);
                    el.SetAttribute("Nom", (string)dgAliasCible.SelectedRows[0].Cells["NomAliasCible"].Value);
                    el.SetAttribute("Selected", "No");

                    parent.XmlExecActionElFromAtt(root, "Guid", el.GetAttribute("Guid"), el, new Form1.XMLEXECACTIONELFROMATT(DeleteAlias));

                    dgAliasCible.Rows.Remove(dgAliasCible.SelectedRows[0]);

                }
            }
            bActiveDelAlias = false;
        }
    }
}
