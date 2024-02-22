using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data.Odbc;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawLun : DrawTools.DrawRectangle
	{
        static private Color Couleur = Color.DarkSlateGray;

		public DrawLun()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawLun(Form1 of, int x, int y, int width, int height,int count)
        {
            F = of;
            OkMove = true;
            Align = true;
            Rectangle = new Rectangle(x, y, width, height);
            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = new ArrayList();
            LstLinkOut = new ArrayList();
            LstValue = new ArrayList();
            GuidkeyObjet = Guid.NewGuid();
            Texte = "Lun" + count;
            Guidkey = Guid.NewGuid();

            InitProp();
            Initialize();
        }

        public DrawLun(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = true;
            Align = true;
            InitRectangle(lstValG);
            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = new ArrayList();
            LstLinkOut = new ArrayList();
            LstValue = lstVal;
            Guidkey = Guid.NewGuid();

            o = GetValueFromLib("Guid");
            if(o!=null)
                GuidkeyObjet = new Guid((string) o);
            o = GetValueFromLib("Nom");
            if (o != null)
                Texte = (string)o;

            Initialize();
        }

        public override bool ParentPointInObject(Point point)
        {
            return false;
        }

        public override void Draw(Graphics g)
        {
            ToolLun to = (ToolLun)F.drawArea.tools[(int)DrawArea.DrawToolType.Lun];
            Pen pen = new Pen(to.Couleur, to.LineWidth);
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10) {
                AffIcon(g, r, to);

                DrawGrpTxt(g, 1, 0, r.Left + Axe, r.Bottom, 0, to.Pen1Couleur, to.BkGrCouleur);
            } else g.DrawRectangle(pen, r);

            pen.Dispose();
        }

        public override void MajTreeView(string Name)
        {
            TreeNode[] ArrayTreeNode = F.tvObjet.Nodes.Find(Name, true);
            if (ArrayTreeNode.Length == 1)
            {
                F.oCnxBase.CBAddNodeWithKeyObjet("SELECT GuidServerPhy, GuidZone, NomZone FROM Zone WHERE GuidLun ='" + GuidkeyObjet.ToString() + "'", ArrayTreeNode[0].Nodes);
            }
        }

        public override void AttachLink(DrawObject o, TypeAttach Attach)
        {
            string oParent = "GuidBaie";

            switch (Attach)
            {
                case TypeAttach.Parent:
                    SetValueFromName(oParent, o.GuidkeyObjet.ToString());
                    break;
            }
            base.AttachLink(o, Attach);
        }

        public override void CWInsert(ControlDoc cw, char cTypeVue)
        {
            if (cTypeVue == '8' || cTypeVue == '7')
            {
                string sType = GetType().Name.Substring("Draw".Length);
                string sGuid = cTypeVue + GuidkeyObjet.ToString().Replace("-", "");
                string sBook = sType.Substring(0, 3) + sGuid;

                if (cw.Exist("n" + sGuid) > -1)
                {
                    cw.InsertTextFromId("n" + sGuid, true, Texte, "Titre 5");
                    cw.InsertTabFromId("n" + sBook, true, this, null, false, null);
                }
                else if (cw.Exist(sType + cTypeVue) > -1)
                {
                    //sType ne doit pas depasse 4 caracteres
                    cw.InsertTextFromId(sType + cTypeVue, false, "\n", null);
                    cw.CreatIdFromIdP(sBook, sType + cTypeVue);
                    cw.InsertTextFromId(sBook, true, Texte + "\n", "Titre 5");
                    cw.CreatIdFromIdP("n" + sGuid, sBook);
                    cw.InsertTextFromId(sBook, false, "\n", null);
                    cw.CreatIdFromIdP("n" + sBook, sBook);
                    cw.InsertTextFromId("n" + sBook, false, "\n", null);
                    cw.InsertTabFromId("n" + sBook, false, this, null, false, null);
                }
            }
        }
	}
}
