using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawBaiePhy : DrawTools.DrawRectangle
	{
        static private Color Couleur = Color.YellowGreen;
        static private Color LineCouleur = Color.Black;
        static private int LineWidth = 1;

        public int NbrU
        {
            get
            {
                return (int)this.GetValueFromName("NbrU"); ;
            }
            set
            {
                this.InitProp("NbrU", (object)value, true);
            }
        }

		public DrawBaiePhy()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawBaiePhy(Form1 of, int x, int y, int width, int count)
        {
            F = of;
            OkMove = true;
            Align = true;
            LstParent = null; 
            LstChild = new ArrayList();
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = new ArrayList();
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
            Texte = "BaiePhy" + count;
            InitProp();
            NbrU = 42;
            Rectangle = new Rectangle(x, y, width, NbrU * HeightU);
            Initialize();
        }

        public DrawBaiePhy(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = true;
            Align = true;
            InitRectangle(lstValG);
            LstParent = null;
            LstChild = new ArrayList();
            LstLinkIn = null;
            LstLinkOut = null;
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

        public string FindElementFromTv()
        {
            string Element = "";
            if (F.oCnxBase.CBRecherche("Select GuidServerPhy, Type From ServerPhy Where GuidBaiePhy='" + GuidkeyObjet + "'"))
            {
                while (F.oCnxBase.Reader.Read())
                {
                    TreeNode[] ArrayTreeNode = F.tvObjet.Nodes.Find(F.oCnxBase.Reader.GetString(0), true);
                    if (ArrayTreeNode.Length == 1)
                        Element += ";" + ArrayTreeNode[0].Text + "   -" + F.oCnxBase.Reader.GetString(1) + " (" + ArrayTreeNode[0].Name + ")";
                }
            }
            F.oCnxBase.CBReaderClose();
            if (F.oCnxBase.CBRecherche("Select GuidDrawer, NomDrawer From Drawer Where GuidBaiePhy='" + GuidkeyObjet + "'"))
            {
                while (F.oCnxBase.Reader.Read())
                    Element += ";" + F.oCnxBase.Reader.GetString(1) + "   -" + "D" + " (" + F.oCnxBase.Reader.GetString(0) + ")";
            }
            F.oCnxBase.CBReaderClose();

            if (Element != "") return Element.Substring(1);
            return null;
        }

        public void GetObjSup(int j)
        {
            DrawRectangle dri, drRef;
            int k=-1;
            drRef = (DrawRectangle)LstChild[j];
            for (int i = j; i < LstChild.Count; i++)
            {
                dri = (DrawRectangle)LstChild[i];
                if (dri.Rectangle.Top < drRef.Rectangle.Top)
                {
                    k = i;
                    drRef = (DrawRectangle)LstChild[i];
                }
            }
            if (k != -1)
            {
                DrawObject oTemp = (DrawObject)LstChild[j];
                LstChild[j] = LstChild[k];
                LstChild[k] = oTemp;
            }
        }

        public void TreeChild()
        {
            for (int i = 0; i < LstChild.Count; i++) GetObjSup(i);
        }

        public int GetTopText(DrawRectangle o)
        {
            TreeChild();
            int yTop = 0;
            for (int i = 0; i < LstChild.Count; i++)
            {
                DrawRectangle dr = (DrawRectangle) LstChild[i];
                if (o == dr) return yTop;
                yTop = dr.GetNewyTop(yTop);
            }
            return 0;
        }

        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(LineCouleur, LineWidth);
            Rectangle r;

            rectangle.Height = NbrU * HeightU;
            rectangle.Width = WidthBaie;
            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10) {
                AffRec(g, r, LineCouleur, LineWidth, Couleur, 7, true, false, false);
                DrawGrpTxt(g, 2, 0, r.Left + Axe, r.Top - 2 * HeightFont[1], 0, Color.Black, Color.Transparent);
            } else g.DrawRectangle(pen, r);

            pen.Dispose();
        }

        /// <summary>
        /// V�rifie si l'objet � d�placer peut l'�tre
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override int MovePossible(DrawObject o)
        {
            return 0;
        }

        public override void MajTreeView(string Name)
        {
            TreeNode[] ArrayTreeNode = F.tvObjet.Nodes.Find(Name, true);
            if (ArrayTreeNode.Length == 1)
            {
                F.oCnxBase.CBAddNode("SELECT GuidLun, NomLun FROM Lun WHERE GuidBaie ='" + GuidkeyObjet.ToString() + "'", ArrayTreeNode[0].Nodes);
            }
        }

        /// <summary>
        /// Move handle to new point (resizing)
        /// </summary>
        /// <param name="point"></param>
        /// <param name="handleNumber"></param>
        public override void MoveHandleTo(Point point, int handleNumber)
        {
            int left = Rectangle.Left;
            int top = Rectangle.Top;
            int right = Rectangle.Right;
            int bottom = Rectangle.Bottom;

            switch (handleNumber)
            {
                case 1:
                    left = point.X;
                    top = point.Y;
                    break;
                case 2:
                    top = point.Y;
                    break;
                case 3:
                    right = point.X;
                    top = point.Y;
                    break;
                case 4:
                    right = point.X;
                    break;
                case 5:
                    right = point.X;
                    bottom = point.Y;
                    break;
                case 6:
                    bottom = point.Y;
                    break;
                case 7:
                    left = point.X;
                    bottom = point.Y;
                    break;
                case 8:
                    left = point.X;
                    break;
            }

            SetRectangle(left, top, right - left, bottom - top);
        }
    }
}
