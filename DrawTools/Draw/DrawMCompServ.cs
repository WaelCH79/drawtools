using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data.Odbc;
using System.Xml;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawMCompServ : DrawTools.DrawRectangle
	{
        static private Color Couleur = Color.DarkGreen;
        static private Color LineCouleur = Color.Gray;

		public DrawMCompServ()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawMCompServ(Form1 of)
        {
            F = of;
            OkMove = false;
            Align = true;
            Rectangle = new Rectangle(1, 1, 1, 1);
            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = new ArrayList();

            Texte = F.tvObjet.SelectedNode.Text;
            GuidkeyObjet = Guid.NewGuid();
            Guidkey = Guid.NewGuid();

            InitProp();
            SetValueFromName("NomMainComposantRef", Texte);
            SetValueFromName("GuidMainComposantRef", (string)F.tvObjet.SelectedNode.Name);
            Initialize();
        }

        public DrawMCompServ(Form1 of, Dictionary<string, object> dic)
        {
            F = of;
            object o = null;
            OkMove = false;
            Align = false;

            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = new ArrayList();
            Guidkey = Guid.NewGuid();
            dicObj = dic;
            InitProp();
            InitValueFromDic(dic);
            InitRectangle(LstValue, false);

            o = GetValueFromLib("Guid");
            if (o != null)
                GuidkeyObjet = new Guid((string)o);
            o = GetValueFromLib("Nom");
            if (o != null)
                Texte = (string)o;

            Initialize();
        }

        public DrawMCompServ(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = false;
            Align = true;
            InitRectangle(lstValG);
            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = lstVal;
            Guidkey = Guid.NewGuid();

            o = GetValueFromLib("Guid");
            if (o != null)
                GuidkeyObjet = new Guid((string)o);
            o = GetValueFromName("GuidMainComposantRef");
            if (o != null)
            {
                TreeNode[] ArrayTreeNode = F.tvObjet.Nodes.Find((string)o, true);
                if (ArrayTreeNode.Length == 1)
                {
                    Texte = ArrayTreeNode[0].Text;
                    SetValueFromName("NomMainComposantRef", Texte);
                }
            }
            Initialize();
        }

        public override bool ParentPointInObject(Point point)
        {
            return false;
        }

        public override bool AttachPointInObject(Point point)
        {
            return false;
        }

        public void Aligne(int x, int y, int width, int height)
        {
            Rectangle = new Rectangle(x, y, width, height);
        }

        public override void Draw(Graphics g)
        {
            ToolMCompServ to = (ToolMCompServ)F.drawArea.tools[(int)DrawArea.DrawToolType.ServMComp];
            Pen pen = new Pen(to.LineCouleur, to.LineWidth);
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10) {
                AffRec(g, r, to); //LineCouleur, LineWidth, Couleur, 5, false, false, true);

                DrawGrpTxt(g, 1, 1, r.Left + Axe, r.Top + r.Height / 2 - 3 * HeightFont[0] / 4, 0, to.Pen1Couleur, to.BkGrCouleur);
            } else g.DrawRectangle(pen, r);

            pen.Dispose();
        }

        public override void dataGrid_CellClick(DataGridView odgv, DataGridViewCellEventArgs e)
        {
            int n;

            n = GetIndexFromName("NomMainComposantRef");
            if (n > -1 && e.RowIndex == n) // Link Applicatif
            {
                string GuidMainComposantRef = (string)GetValueFromName("GuidMainComposantRef");
                FormChangeProp fcp = new FormChangeProp(F, null);

                fcp.AddlSourceFromDB("Select GuidMainComposantRef, NomMainComposantRef From MainComposantRef Where GuidProduitApp in (Select GuidProduitApp from MainComposantRef Where GuidMainComposantRef='" + GuidMainComposantRef + "')", "Create");
                fcp.ShowDialog(F);
                if (fcp.Valider)
                {
                    string[] aValue = F.oCnxBase.CmdText.Split('(', ')');
                    SetValueFromNameTodgv(F.dataGrid, "NomMainComposantRef", (object) aValue[0].Trim());
                    SetValueFromNameTodgv(F.dataGrid, "GuidMainComposantRef", (object)aValue[1].Trim());
                }
            }
        }

        public override void AttachLink(DrawObject o, TypeAttach Attach)
        {
            string oParent = "GuidMainComposant";

            switch (Attach)
            {
                case TypeAttach.Parent:
                    SetValueFromName(oParent, (string)o.LstValue[0]);
                    break;
            }
            base.AttachLink(o, Attach);
        }

        public override void savetoDB()
        {
            if (!savetoDBFait())
            {
                savetoDBOK();
            }
        }

        public override XmlElement savetoXml(XmlDB xmlDB, bool GObj)
        {
            string s = GetTypeSimpleTable();

            if (LstParent != null)
            {
                for (int i = 0; i < LstParent.Count; i++) ((DrawObject)LstParent[i]).savetoXml(xmlDB, GObj);
            }

            return XmlCreatObject(xmlDB); // Table Objet
        }
	}
}
