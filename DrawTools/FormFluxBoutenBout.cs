using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Collections;

namespace DrawTools
{
    public partial class FormFluxBoutenBout : Form
    {
        private Form1 parent;
        private XmlExcel xmlFluxParent;
        private XmlExcel xmlFluxChild;
        private TreeNode tnCurrent;
        private string sFlux;

        public new Form1 Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        public FormFluxBoutenBout(Form1 p, string sF, XmlExcel xmlFlxP, XmlExcel xmlFlxC)
        {
            Parent = p;
            sFlux = sF;
            InitializeComponent();
            InitEvent();
            xmlFluxParent = xmlFlxP;
            xmlFluxChild = xmlFlxC;
            InitFluxChild();
            InitFluxParentWithChild();
            tnCurrent = null;
        }
        public void InitEvent()
        {
            /*treeView1.ItemDrag += new ItemDragEventHandler(treeView1_ItemDrag);
            26.treeView1.DragEnter += new DragEventHandler(treeView1_DragEnter);
            27.treeView1.DragOver += new DragEventHandler(treeView1_DragOver);
            28.treeView1.DragDrop += new DragEventHandler(treeView1_DragDrop);*/

            tvFluxTech.ItemDrag += TvFluxTech_ItemDrag;
            tvFluxTech.DragEnter += TvFluxTech_DragEnter;
            tvFluxTech.DragOver += TvFluxTech_DragOver;
            tvFluxTech.DragDrop += TvFluxTech_DragDrop;
            tvFluxTech.DragLeave += TvFluxTech_DragLeave;

            tvFluxApp.ItemDrag += TvFluxApp_ItemDrag;
            tvFluxApp.DragEnter += TvFluxApp_DragEnter;
            tvFluxApp.DragOver += TvFluxApp_DragOver;
            tvFluxApp.DragDrop += TvFluxApp_DragDrop;
            tvFluxApp.DragLeave += TvFluxApp_DragLeave;

            bOK.Click += BOK_Click;
            bEnd.Click += BEnd_Click;

        }

        private void BEnd_Click(object sender, EventArgs e)
        {
            Close();
            //throw new NotImplementedException();
        }

        private void BOK_Click(object sender, EventArgs e)
        {
            if (sFlux == "App")
            {
                string sWhere = "", sInsertAtt = "", sInsertValue = "";
                if (parent.oCnxBase.isDataTableContainColumn("guidvue", "techlinkapp")) //  ExistSchema("techlinkapp", "guidvue"))
                {
                    sWhere = " and GuidVue='" + parent.GetGuidVue() + "'";
                    sInsertAtt = ", guidvue";
                    sInsertValue = ",'" + parent.GetGuidVue() + "'";
                }


                for (int i = 0; i < tvFluxApp.Nodes.Count; i++)
                {
                    TreeNode tnApp = tvFluxApp.Nodes[i];

                    //parent.oCnxBase.CBWrite("delete from techlinkapp where GuidLink='" + tnApp.Name + "'" + sWhere);
                    parent.oCnxBase.CBWrite("delete from techlinkapp where GuidLink='" + tnApp.Name + "'");
                    for (int j = 0; j < tnApp.Nodes.Count; j++)
                    {
                        TreeNode tnTech = tnApp.Nodes[j];
                        parent.oCnxBase.CBWrite("insert into techlinkapp (guidtechlink, guidlink" + sInsertAtt + ", num) values ('" + tnTech.Name + "','" + tnApp.Name + "'" + sInsertValue + ", " + j + ")");
                    }
                }
            }
            else
            {
                for (int i = 0; i < tvFluxApp.Nodes.Count; i++)
                {
                    TreeNode tnApp = tvFluxApp.Nodes[i];

                    //parent.oCnxBase.CBWrite("delete from techlinkapp where GuidLink='" + tnApp.Name + "'" + sWhere);
                    parent.oCnxBase.CBWrite("delete from applinkfonc where GuidFoncLink='" + tnApp.Name + "'");
                    for (int j = 0; j < tnApp.Nodes.Count; j++)
                    {
                        TreeNode tnTech = tnApp.Nodes[j];
                        parent.oCnxBase.CBWrite("insert into applinkfonc (guidapplink, guidfonclink, guidvue, num) values('" + tnTech.Name + "', '" + tnApp.Name + "', '" + parent.GetGuidVue() + "', " + j + ")");
                    }
                }
            }
            Close();
            //throw new NotImplementedException();

        }

        private void TvFluxApp_DragLeave(object sender, EventArgs e)
        {
            //MessageBox.Show("TvFluxApp draglease");
            if (tnCurrent != null) // tvFluxApp
            {
                tnCurrent.Parent.Nodes.Remove(tnCurrent);
                tnCurrent = null;
            }
            //throw new NotImplementedException();
        }

        private void TvFluxApp_DragOver(object sender, DragEventArgs e)
        {
            //MessageBox.Show("TvFluxApp dragover");
            // déclancher lorsque la sourie passe sur l'element apres un dragenter même si c'est un autre controle qui a initié le dodragdrop
            //throw new NotImplementedException();
        }

        private void TvFluxApp_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
            //MessageBox.Show("TvFluxApp dragenter");
            // déclancher lorsque la sourie passe sur l'element apres un dodrapdrop même si c'est un autre controle qui a initié le dodragdrop
            //throw new NotImplementedException();
        }

        private void TvFluxApp_ItemDrag(object sender, ItemDragEventArgs e)
        {
            tnCurrent = (TreeNode)e.Item;
            if (tnCurrent.Parent != null) DoDragDrop(e.Item, DragDropEffects.Copy);
            else tnCurrent = null;
            //MessageBox.Show("TvFluxApp itemdrag");
            // debut déclancher lorsque on glisse l'élément
            //throw new NotImplementedException();
        }

        private void TvFluxTech_DragLeave(object sender, EventArgs e)
        {
            //MessageBox.Show("TvFluxTech draglease");
            // déclancher lorsque la sourie sort du controle qui à positionner le dodragdrop
            //throw new NotImplementedException();
        }

        private void TvFluxTech_DragDrop(object sender, DragEventArgs e)
        {
            MessageBox.Show("TvFluxTech dragdrop");
            //throw new NotImplementedException();
        }

        private void TvFluxTech_DragOver(object sender, DragEventArgs e)
        {
            //MessageBox.Show("TvFluxTech dragover");
            // déclancher lorsque la sourie passse sur l'element apres un DragEnter
            //throw new NotImplementedException();
        }

        private void TvFluxTech_DragEnter(object sender, DragEventArgs e)
        {
            //MessageBox.Show("TvFluxTech dragenter");
            // déclancher lorsque la sourie passse sur l'element apres un dodrapdrop

            //throw new NotImplementedException();
        }

        private void TvFluxApp_DragDrop(object sender, DragEventArgs e)
        {
            tnCurrent = null;
            //MessageBox.Show("TvFluxApp dragdrop");
            Point pt = new Point();
            pt.X = e.X; pt.Y = e.Y;
            pt  = tvFluxApp.PointToClient(pt);
            TreeNode dropNode = this.tvFluxApp.GetNodeAt(pt);
            TreeNode dragNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            if (dropNode != null)
            {
                if (dropNode.Nodes.Find(dragNode.Name, true).Count<TreeNode>() == 0)
                {
                    if (dropNode.Parent == null)
                    {
                        //MessageBox.Show(dropNode.Text + " (" + dropNode.Name + ")  -> " + dragNode.Text + " (" + dragNode.Name + ")");
                        dropNode.Nodes.Add((TreeNode)dragNode.Clone());
                        //TreeNode DragNode = this.NodeToBeDeleted;
                        //DropNode.Parent.Nodes.Remove(this.NodeToBeDeleted);
                        //DropNode.Parent.Nodes.Insert(DropNode.Index + 1, DragNode);
                    }
                } else
                {
                    if(dragNode.Parent != null && dragNode.Parent == dropNode) // tvFluxApp
                    {
                        TreeNode tnTemp = (TreeNode)dragNode.Clone();
                        
                        dropNode.Nodes.Remove(dragNode);
                        dropNode.Nodes.Add(tnTemp);
                    }
                }
            }
            //throw new NotImplementedException();
        }

        private void TvFluxTech_ItemDrag(object sender, ItemDragEventArgs e)
        {
            tnCurrent = null;
            //MessageBox.Show("TvFluxTech itemdrag");
            // debut déclancher lorsque on glisse l'élément
            //NodeToBeDeleted = (TreeNode)e.Item;
            DoDragDrop(e.Item, DragDropEffects.Copy);
            //throw new NotImplementedException();
        }

        public void InitFluxChild()
        {
            IEnumerator ienum = xmlFluxChild.root.GetEnumerator();
            XmlNode Node;
            string sName = "FluxTech";
            if (sFlux != "App") sName = "FluxApp";

            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element && (Node.ParentNode != xmlFluxChild.root || Node.Name == sName))
                {
                    tvFluxTech.Nodes.Add(((XmlElement)Node).GetAttribute("Guid"), ((XmlElement)Node).GetAttribute("Id") + " " + ((XmlElement)Node).GetAttribute("Nom"));
                }
            }
        }
        public void InitFluxParentWithChild()
        {
            IEnumerator ienum = xmlFluxParent.root.GetEnumerator();
            XmlNode Node;
            string sNameP = "FluxApp", sNameC= "FluxTech";
            if (sFlux != "App") { sNameP = "FluxFonc"; sNameC = "FluxApp"; }

            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element && (Node.ParentNode != xmlFluxParent.root || Node.Name == sNameP))
                {
                    TreeNode tn = tvFluxApp.Nodes.Add(((XmlElement)Node).GetAttribute("Guid"), ((XmlElement)Node).GetAttribute("Id") + " " + ((XmlElement)Node).GetAttribute("Nom"));

                    ArrayList lstFlux = xmlFluxParent.XmlGetLstElFromName((XmlElement)Node, sNameC);

                    for (int i = 0; i < lstFlux.Count; i++)
                    {
                        XmlNode nodeChild = (XmlNode)lstFlux[i];
                        tn.Nodes.Add(((XmlElement)nodeChild).GetAttribute("Guid"), ((XmlElement)nodeChild).GetAttribute("Id") + " " + ((XmlElement)nodeChild).GetAttribute("Nom"));
                    }
                }
            }
        }
    }
}
