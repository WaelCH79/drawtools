using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawBaie : DrawTools.DrawRectangle
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

		public DrawBaie()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawBaie(Form1 of, int x, int y, int width, int height, int count)
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
            Texte = "Baie" + count;

            InitProp();
            NbrU = 42;
            Initialize();
        }

        public DrawBaie(Form1 of, ArrayList lstVal, ArrayList lstValG)
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
            ToolBaie to = (ToolBaie)F.drawArea.tools[(int)DrawArea.DrawToolType.Baie];
            Pen pen = new Pen(to.LineCouleur, to.LineWidth);
            Rectangle r;
            
            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10) {
                AffRec(g, r, to);
                DrawGrpTxt(g, 2, 0, r.Left + Axe, r.Top, 0, to.Pen1Couleur, to.BkGrCouleur);
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
                    cw.InsertTextFromId(sBook, true, Texte + "\n", "Titre 6");
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
