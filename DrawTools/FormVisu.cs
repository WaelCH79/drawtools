using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace DrawTools
{
    public partial class FormVisu : Form
    {
        private Form1 parent;

        public new Form1 Parent
        {
            get { return parent; }
            set { parent = value;}
        }

        public FormVisu(Form1 p)
        {
            Parent = p;
            InitializeComponent();
        }

        private void AddNodeApplication(XmlElement elApp)
        {
            XmlAttributeCollection lstAtt = elApp.Attributes;

            string sApp = elApp.GetAttribute("sNomApplication");
            string sGuidApp = elApp.GetAttribute("sGuidApplication");
            if (sApp != "" && sGuidApp != "")
            {
                TreeNode tn = tvVues.Nodes.Add(sGuidApp, sApp);
                IEnumerator ienum = elApp.GetEnumerator();
                XmlNode Node;
                while (ienum.MoveNext())
                {
                    Node = (XmlNode)ienum.Current;
                    switch (Node.NodeType)
                    {
                        case XmlNodeType.Element:
                            string sVue = ((XmlElement)Node).GetAttribute("sNomVue");
                            string sGuidVue = ((XmlElement)Node).GetAttribute("sGuidVue");
                            if (sVue != "" && sGuidVue !="") tn.Nodes.Add(sGuidVue, sVue);
                            break;
                    }
                }


            }

        }

        private void AddNodes(XmlElement elParent)
        {
            IEnumerator ienum = elParent.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                switch (Node.NodeType)
                {
                    case XmlNodeType.Element:
                        AddNodeApplication((XmlElement)Node);
                        break;
                }
            }
        }

        private void bLoadXml_Click(object sender, EventArgs e)
        {
            tbXml.Text="";
            tvVues.Nodes.Clear();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = Parent.sPathRoot + "\\";
            openFileDialog1.Filter = "xml files (*.xml)|*.xml";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Parent.docXml = new XmlDocument();
                Parent.docXml.Load(openFileDialog1.FileName);
                XmlElement root = Parent.docXml.DocumentElement;
                if (root.Name == "Applications")
                {
                    tbXml.Text = openFileDialog1.FileName;
                    AddNodes(root);
                    //ImportXml(root);
                }
            }
        }

        private void bLoadVue_Click(object sender, EventArgs e)
        {
            XmlElement root = Parent.docXml.DocumentElement;
            IEnumerator ienum = root.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    if (((XmlElement)Node).GetAttribute("sGuidApplication") == tvVues.SelectedNode.Parent.Name)
                    {
                        ienum = Node.GetEnumerator();
                        while (ienum.MoveNext())
                        {
                            Node = (XmlNode)ienum.Current;
                            if (Node.NodeType == XmlNodeType.Element)
                            {
                                if (((XmlElement)Node).GetAttribute("sGuidVue") == tvVues.SelectedNode.Name)
                                {
                                    LoadVue(Node);
                                    //MessageBox.Show("chargement de la vue " + tvVues.SelectedNode.Text + " de l application " + tvVues.SelectedNode.Parent.Text);
                                    break;
                                }
                            }
                        }
                        break;
                    }
                    
                }
            }
        }

        private void LoadVue(XmlNode nodeVue)
        {

            string sGuidEnvironnement = ((XmlElement)nodeVue).GetAttribute("sGuidEnvironnement");
            parent.GuidVue = new Guid(((XmlElement)nodeVue).GetAttribute("sGuidVue"));
            //parent.GuidApplication = new Guid(((XmlElement)nodeVue).GetAttribute("sGuidApplication"));
            parent.GuidTypeVue = new Guid(((XmlElement)nodeVue).GetAttribute("sGuidTypeVue"));
            if (parent.oCnxBase.CBRecherche("Select NomTypeVue From TypeVue Where GuidTypeVue='" + parent.GuidTypeVue + "'"))
            {
                string dd = parent.oCnxBase.Reader.GetString(0);
                parent.oCnxBase.CBReaderClose();
                //parent.cbTypeVue.SelectedItem = dd;
                parent.tbTypeVue.Text = dd;
                
                
                IEnumerator ienum = nodeVue.GetEnumerator();
                ArrayList lstValue = new ArrayList();
                ArrayList lstValueG = new ArrayList();
                XmlNode Node;
                while (ienum.MoveNext())
                {
                    Node = (XmlNode)ienum.Current;
                    if (Node.NodeType == XmlNodeType.Element)
                    {
                        //switch (((string)parent.cbTypeVue.SelectedItem)[0])
                        switch(parent.tbTypeVue.Text[0])
                        {
                            case '0':
                                if (Node.Name == "AppUser") parent.drawArea.tools[(int)DrawArea.DrawToolType.AppUser].LoadObjectXml(Node);
                                if (Node.Name == "Module") parent.drawArea.tools[(int)DrawArea.DrawToolType.Module].LoadObjectXml(Node);
                                if (Node.Name == "Link") parent.drawArea.tools[(int)DrawArea.DrawToolType.Link].LoadObjectXml(Node);
                                break;
                            case '1':
                                if (Node.Name == "Application") parent.drawArea.tools[(int)DrawArea.DrawToolType.Application].LoadObjectXml(Node);
                                if (Node.Name == "MainComposant") parent.drawArea.tools[(int)DrawArea.DrawToolType.MainComposant].LoadObjectXml(Node);
                                if (Node.Name == "AppUser") parent.drawArea.tools[(int)DrawArea.DrawToolType.AppUser].LoadObjectXml(Node);
                                if (Node.Name == "CompFonc") parent.drawArea.tools[(int)DrawArea.DrawToolType.CompFonc].LoadObjectXml(Node);
                                if (Node.Name == "Composant") parent.drawArea.tools[(int)DrawArea.DrawToolType.Composant].LoadObjectXml(Node);
                                if (Node.Name == "Interface") parent.drawArea.tools[(int)DrawArea.DrawToolType.Interface].LoadObjectXml(Node);
                                if (Node.Name == "Base") parent.drawArea.tools[(int)DrawArea.DrawToolType.Base].LoadObjectXml(Node);
                                if (Node.Name == "File") parent.drawArea.tools[(int)DrawArea.DrawToolType.File].LoadObjectXml(Node);
                                if (Node.Name == "Link") parent.drawArea.tools[(int)DrawArea.DrawToolType.LinkA].LoadObjectXml(Node);
                                break;
                            case '2':
                                if (Node.Name == "AppUser") parent.drawArea.tools[(int)DrawArea.DrawToolType.TechUser].LoadObjectXml(Node);
                                //oCnxBase.LoadUserType_Techno();
                                if (Node.Name == "Application") parent.drawArea.tools[(int)DrawArea.DrawToolType.Application].LoadObjectXml(Node);
                                if (Node.Name == "Server") parent.drawArea.tools[(int)DrawArea.DrawToolType.Server].LoadObjectXml(Node);
                                if (Node.Name == "MainComposant") parent.drawArea.tools[(int)DrawArea.DrawToolType.MainComposant].LoadObjectXml(Node);
                                if (Node.Name == "ServerType") parent.drawArea.tools[(int)DrawArea.DrawToolType.ServerType].LoadObjectXml(Node);
                                if (Node.Name == "Techno") parent.drawArea.tools[(int)DrawArea.DrawToolType.Techno].LoadObjectXml(Node);

                                //parent.oCnxBase.LoadSousObjets();
                                if (Node.Name == "TechLink") parent.drawArea.tools[(int)DrawArea.DrawToolType.TechLink].LoadObjectXml(Node);
                                //oCnxBase.LoadTechLinkApp();
                                break;
                            case '3': // 3-Production
                            case '5': // 5-Pre-Production
                            case '4': // 4-Hors Production
                            case 'F': // F-Service Infra
                                if (Node.Name == "Cluser") parent.drawArea.tools[(int)DrawArea.DrawToolType.Cluster].LoadObjectXml(Node);
                                if (Node.Name == "ServerPhy") parent.drawArea.tools[(int)DrawArea.DrawToolType.ServerPhy].LoadObjectXml(Node);
                                //GetServerLinks
                                if (Node.Name == "VLan") parent.drawArea.tools[(int)DrawArea.DrawToolType.VLan].LoadObjectXml(Node);
                                //LoadVLanPoint();
                                if (Node.Name == "Router") parent.drawArea.tools[(int)DrawArea.DrawToolType.Router].LoadObjectXml(Node);
                                //LoadRouterLink();
                                if (Node.Name == "NCard") parent.drawArea.tools[(int)DrawArea.DrawToolType.NCard].LoadObjectXml(Node);
                                //LoadAlias();
                                //LoadNCardLinkIn();
                                //LoadNCardLink("Out");
                                break;
                            case '6': // 6-Sites
                                if (Node.Name == "Cnx") parent.drawArea.tools[(int)DrawArea.DrawToolType.Cnx].LoadObjectXml(Node);
                                //LoadCnxPoint();
                                if (Node.Name == "Location") parent.drawArea.tools[(int)DrawArea.DrawToolType.Location].LoadObjectXml(Node);
                                if (Node.Name == "ServerSite") parent.drawArea.tools[(int)DrawArea.DrawToolType.ServerSite].LoadObjectXml(Node);
                                //GetServerLinks();
                                if (Node.Name == "InterLink") parent.drawArea.tools[(int)DrawArea.DrawToolType.InterLink].LoadObjectXml(Node);
                                //GetInterLinks();
                                if (Node.Name == "PtCnx") parent.drawArea.tools[(int)DrawArea.DrawToolType.PtCnx].LoadObjectXml(Node);
                                break;
                        }
                    }
                }
            }
            parent.oCnxBase.CBReaderClose();
            parent.drawArea.Capture = true;
            parent.drawArea.GraphicsList.UnselectAll();
            parent.drawArea.MajObjets();
            Close();
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tvVues_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent != null) bLoadVue.Enabled = true; else bLoadVue.Enabled = false;
        }
    }
}
