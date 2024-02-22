using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawBaieCTI : DrawTools.DrawRectangle
	{
		public DrawBaieCTI()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawBaieCTI(Form1 of, int x, int y, int width, int height, int count)
        {
            F = of;
            OkMove = true;
            Align = true;
            Rectangle = new Rectangle(x, y, width, height);
            LstParent = null; 
            LstChild = new ArrayList();
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = new ArrayList();
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
            Texte = "BaiePhy" + count;

            InitProp();
            Initialize();
        }

        public DrawBaieCTI(Form1 of, ArrayList lstVal, ArrayList lstValG)
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
                
        public override bool AttachPointInObject(Point point)
        {
            return false;
        }

        public override void Draw(Graphics g)
        {
            ToolBaieCTI to = (ToolBaieCTI)F.drawArea.tools[(int)DrawArea.DrawToolType.BaieCTI];
            Pen pen = new Pen(to.LineCouleur, to.LineWidth);
            Pen pen1 = new Pen(to.LineCouleur, to.Line1Width);
            Rectangle r;
            
            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10) {
                AffRec(g, r, to);
                g.DrawLine(pen1, r.Left, r.Top + HeightCard, r.Right, r.Top + HeightCard);
                g.DrawLine(pen1, r.Left, r.Bottom - HeightCard, r.Right, r.Bottom - HeightCard);
                g.DrawLine(pen1, r.Left, r.Top + HeightCard + HeightServer, r.Right, r.Top + HeightCard + HeightServer);

                DrawGrpTxt(g, 2, -1, r.Left + Axe, r.Top + HeightCard + Axe, 0, to.Pen1Couleur, to.BkGrCouleur);
                
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
            AligneObjet();
        }

        public override string GetsType(bool Reel)
        {
            if (Reel) return base.GetsType(Reel);
            return "Baie";
        }

        public override string GetTypeSimpleTable()
        {
            //return base.GetTypeSimpleTable();
            return "Baie";
        }

        public int NbrNCard()
        {
            int CountObj = 0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawSanCard)) CountObj++;
            return CountObj;
        }

        public void AligneObjet()
        {
            int CountNCard = NbrNCard();
            if (CountNCard != 0)
            {
                //int WidthObjet = (Rectangle.Width - Axe) / CountNCard, HeightObjet = HeightCard - 2 * Axe;
                int WidthObjet = (Rectangle.Width - 2 * radius - Axe) / CountNCard, HeightObjet = HeightCard - 2 * Axe;

                for (int i = LstChild.Count - 1; i >= 0; i--)
                {
                    if (LstChild[i].GetType() == typeof(DrawSanCard))
                    {
                        //((DrawSanCard)LstChild[i]).Aligne(Rectangle.Left + WidthObjet * (CountNCard - 1) + Axe, Rectangle.Top + Axe, WidthObjet - Axe, HeightObjet);
                        ((DrawSanCard)LstChild[i]).Aligne(Rectangle.Left + WidthObjet * (CountNCard - 1) + radius + Axe, Rectangle.Top + Axe, WidthObjet - Axe, HeightObjet);
                        CountNCard--;
                    }
                }
            }
        }


    }
}
