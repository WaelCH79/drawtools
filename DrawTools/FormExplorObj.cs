using System;
using System.Runtime.Serialization;

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DrawTools
{
        public partial class FormExplorObj : Form
    {
        private Form1 parent;
        public ArrayList lstObj;

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

        public FormExplorObj(Form1 p)
        {
            Parent = p;
            lstObj = new ArrayList();
            InitializeComponent();
            InitEvent();
        }

        public void InitEvent()
        {
            tvObj.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvObj_AfterSelect);
            tvObj.HandleDestroyed += TvObj_HandleDestroyed;
            bExport.Click += BExport_Click;
        }

        private void BExport_Click(object sender, EventArgs e)
        {
            if (Parent.oCureo != null && Parent.oCureo.bExpand && Parent.oCureo.iCat != -1)
            {
                using (StreamWriter sfile = File.CreateText(@"c:\dat\tmp\exportExObj.csv"))
                {
                    sfile.WriteLine("GuidObjet;NomObj");
                    for (int i = 0; i < Parent.oCureo.tn.Nodes.Count; i++)
                    {
                        TreeNode tno = Parent.oCureo.tn.Nodes[i];
                        sfile.WriteLine(tno.Name + ";" + tno.Text);
                    }
                }
            
            }
            //throw new NotImplementedException();
        }

        private void TvObj_HandleDestroyed(object sender, EventArgs e)
        {
            if (Parent.oCureo != null)
            {
                if (Parent.oCureo.oDraw != null)
                {
                    DrawObject o = Parent.oCureo.oDraw;
                    o.SaveProp(dgvExpObj);
                    o.saveObjecttoDB();
                }
            }
        }

        public void init(ExpObj o)
        {
            if (o == null)
            {
                tvObj.Nodes.Add("App", "Application");
                Parent.oCnxBase.CBAddNodeObjExp(this, "SELECT GuidApplication, NomApplication FROM Application ORDER BY NomApplication", tvObj.Nodes[0], DrawArea.DrawToolType.Application);
            }
            else
            {
                //ExpObj o = new ExpObj("objetkeyguid","Texte", DrawArea.DrawToolType.Application);
                o.GuidKey = Guid.NewGuid();
                o.tn = tvObj.Nodes.Add(o.GuidKey.ToString(), o.sName);

                lstObj.Add((object)o);

            }
            ShowDialog(Parent);
        }

        public ExpObj Find(string s)
        {
            for (int i = 0; i < lstObj.Count; i++)
            {
                ExpObj eo = (ExpObj) lstObj[i];
                if (s == eo.GuidKey.ToString()) return eo;
            }
            return null;
        }

        public ExpObj FindObj(string s, string sGuidParent)
        {
            for (int i = 0; i < lstObj.Count; i++)
            {
                ExpObj eo = (ExpObj)lstObj[i];
                if (eo.tn.Parent != null && eo.tn.Parent.Name == sGuidParent && s == eo.GuidObj.ToString()) return eo;
            }
            return null;
        }

        void dataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DrawObject o =  Parent.oCureo.oDraw;

            if (e.ColumnIndex == 2) //Bouton Pls
            {
                o.dataGrid_CellClick((DataGridView)sender, e);        
            }

            //throw new NotImplementedException();
        }

        void tvObj_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                TreeViewHitTestInfo hitinfo = tvObj.HitTest(e.X, e.Y);
                ExpObj eo = Find(hitinfo.Node.Name);
                if (eo != null && eo.iCat==-1 && Parent.drawArea.tools[(int)eo.ObjTool].mnuObj != null)
                {
                    Parent.drawArea.tools[(int)eo.ObjTool].guidCurContext = eo.GuidObj.ToString();
                    Parent.drawArea.tools[(int)eo.ObjTool].eCurTool = eo.ObjTool;
                    Parent.oCureo = eo;
                    Parent.drawArea.tools[(int)eo.ObjTool].mnuObj.Show(this, new Point(e.X, e.Y+25));
                }

            }
            // throw new System.NotImplementedException();
        }

               

        void tvObj_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {

            ExpObj eo = Find(tvObj.SelectedNode.Name);
            
            if (eo != null)
            {
                if (Parent.oCureo != null)
                {
                    if (Parent.oCureo.oDraw != null)
                    {
                        DrawObject o = Parent.oCureo.oDraw;
                        o.SaveProp(dgvExpObj);
                        o.saveObjecttoDB();
                    }
                }

                Parent.oCureo = eo;
                //Parent.drawArea.tools[(int)eo.ObjTool].foexplor=this;
                if (eo.oDraw == null) Parent.drawArea.tools[(int)eo.ObjTool].LoadSimpleObjectSansGraph(eo);
                if (eo.oDraw != null) eo.oDraw.InitDatagrid(dgvExpObj);
                //Parent.drawArea.tools[(int)eo.ObjTool].foexplor=null;
                if (!eo.bExpand)
                {
                    eo.bExpand = true;
                    if (checkbAllVersions.Checked) eo.bChkAllVersion = true;
                    Parent.drawArea.tools[(int)eo.ObjTool].ExpandObj(this, eo);
                }

            }
            //throw new System.NotImplementedException();
        }
    }

    public class ExpObj
    {
        public Guid GuidKey;
        public Guid GuidObj;
        public List<string> lstKeyObj;
        public DrawArea.DrawToolType ObjTool;
        public bool bExpand;
        public bool bChkAllVersion;
        public TreeNode tn;
        public int iCat;
        public string sName;
        public DrawObject oDraw;
        public List<ExpObj> lstKExpObj;
        public Table confTable;

        public ExpObj(Guid go, string Name, DrawArea.DrawToolType t)
        {
            GuidObj = go;
            ObjTool = t;
            bExpand = false;
            bChkAllVersion = false;
            iCat = -1;
            sName = Name;
            oDraw = null;
            lstKExpObj = null;
            confTable = null;
        }
        public ExpObj(List<string> lstKey, string Name, DrawArea.DrawToolType t)
        {
            lstKeyObj = lstKey;
            ObjTool = t;
            bExpand = false;
            bChkAllVersion = false;
            iCat = -1;
            sName = Name;
            oDraw = null;
            lstKExpObj = null;
            confTable = null;
        }

        public ExpObj(Guid gk, Guid go, DrawArea.DrawToolType t, int i, TreeNode n)
        {
            GuidKey = gk;
            GuidObj = go;
            ObjTool = t;
            bExpand = false;
            bChkAllVersion = false;
            tn = n;
            iCat = i;
            oDraw = null;
            lstKExpObj = null;
            confTable = null;
        }

        public void setConfTable(DrawArea oDrawArea)
        {
            confTable = oDrawArea.tools[(int)ObjTool].GetTable(oDrawArea.tools[(int)ObjTool].GetTypeSimpleTable(), lstKeyObj[0]);
        }

    }
}
