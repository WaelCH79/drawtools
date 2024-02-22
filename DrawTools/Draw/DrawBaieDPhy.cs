using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawBaieDPhy : DrawTools.DrawRectangle
	{
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

		public DrawBaieDPhy()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawBaieDPhy(Form1 of, ArrayList lstVal, ArrayList lstValG)
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

        public override bool ParentPointInObject(Point point)
        {
            return false;
        }

        public override string GetTypeSimpleTable()
        {
            //return base.GetTypeSimpleTable();
            return "Baie";
        }

        public override void Draw(Graphics g)
        {
            ToolBaieDPhy to = (ToolBaieDPhy)F.drawArea.tools[(int)DrawArea.DrawToolType.BaieDPhy];

            Pen pen = new Pen(to.LineCouleur, to.LineWidth);
            Rectangle r;

            rectangle.Height = NbrU * HeightU;
            rectangle.Width = WidthBaie;
            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10) {
                AffRec(g, r, to);
                DrawGrpTxt(g, 2, 0, r.Left + Axe, r.Top - 2 * HeightFont[1], 0, to.Pen1Couleur, to.BkGrCouleur);
            } else g.DrawRectangle(pen, r);

            pen.Dispose();
        }

        /// <summary>
        /// Vérifie si l'objet à déplacer peut l'être
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

        public override string GetsType(bool Reel)
        {
            if (Reel) return base.GetsType(Reel);
            return "Baie";
        }
    }
}
