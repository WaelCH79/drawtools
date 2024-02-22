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
	public class DrawDrawer : DrawTools.DrawRectangle
	{
        public int TextyTop;

        public int NbrE
        {
            get
            {
                return (int)this.GetValueFromName("NbrE");
            }
            set
            {
                this.InitProp("NbrE", (object)value, true);
            }
        }

        public int NbrU
        {
            get
            {
                return (int)this.GetValueFromName("NbrU");
            }
            set
            {
                this.InitProp("NbrU", (object)value, true);
            }
        }

        public int StartU
        {
            get
            {
                return (int)this.GetValueFromName("StartU");
            }
            set
            {
                this.InitProp("StartU", (object)value, true);
            }
        }


		public DrawDrawer()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawDrawer(Form1 of, int x, int y, int width, int height,int count)
        {
            F = of;
            OkMove = true;
            Align = true;
            NbrCreatChild = 0;
                        
            Rectangle = new Rectangle(x, y, width, height);
            LstParent = new ArrayList();
            LstChild = new ArrayList();
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = new ArrayList();
            GuidkeyObjet = Guid.NewGuid();
            Texte = "Drawer" + count;
            Guidkey = Guid.NewGuid();
            InitProp();
            NbrU = 4;
            NbrE = 16;
            Initialize();
        }

        public DrawDrawer(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            OkMove = true;
            Align = true;
            object o= null;
            NbrCreatChild = 0;

            InitRectangle(lstValG);
            LstParent = new ArrayList();
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

        public override int GetNewyTop(int yTop)
        {
            int yObj = 0;

            yObj = AXE + ((int)GetValueFromName("NbrE") + 1) * (HeightFont[2] + AXE);


            return yTop + yObj;
        }
        
        public override void Draw(Graphics g)
        {
            ToolDrawer to = (ToolDrawer)F.drawArea.tools[(int)DrawArea.DrawToolType.Drawer];
            Pen pen = new Pen(to.LineCouleur, to.LineWidth);
            Rectangle r;


            rectangle.Height = NbrU * HeightU-1;
            rectangle.Width = WidthBaie-2;
            if (LstParent != null && LstParent.Count==1)
            {
                DrawBaiePhy db = (DrawBaiePhy)LstParent[0];
                rectangle.X = db.rectangle.X + 1;
                rectangle.Y = db.rectangle.Y + db.rectangle.Height - StartU * HeightU;
            }
            
            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10) {
                AffRec(g, r, to); 

                //drawtxt + Lignes
                if (LstParent.Count != 0)
                {
                    DrawBaiePhy db = (DrawBaiePhy) LstParent[0];
                    int yTop = db.GetTopText(this);
                    TextyTop = yTop + db.Rectangle.Top;
                    g.DrawLine(new Pen(Color.Gray), r.Right, r.Top, r.Right + 10 * AXE, TextyTop + AXE);
                    g.DrawLine(new Pen(Color.Gray), r.Right + 10 * AXE, TextyTop + AXE, r.Right + 10 * AXE + 30, TextyTop + AXE);
                    DrawGrpTxt(g, 3, 0, r.Right + 10 * AXE, TextyTop + Axe, 0, to.Pen1Couleur, to.BkGrCouleur);
                    yTop = GetNewyTop(yTop);
                    g.DrawLine(new Pen(Color.Gray), r.Right, Rectangle.Bottom, Rectangle.Right + 10 * Axe, db.Rectangle.Top + yTop);
                    g.DrawLine(new Pen(Color.Gray), r.Right + 10 * Axe, db.Rectangle.Top + yTop, r.Right + 10 * Axe + 30, db.Rectangle.Top + yTop);
                    
                }
            }
            else AffRec(g, r, to);

            pen.Dispose();
        }

        public string FindServerFromTv()
        {
            string ServerInTV = "";
            if (F.oCnxBase.CBRecherche("Select GuidServerPhy, Type From ServerPhy Where GuidBaiePhy='" + GuidkeyObjet + "'"))
            {
                while (F.oCnxBase.Reader.Read())
                {
                    TreeNode[] ArrayTreeNode = F.tvObjet.Nodes.Find(F.oCnxBase.Reader.GetString(0), true);
                    if (ArrayTreeNode.Length == 1)
                        ServerInTV += ";" + ArrayTreeNode[0].Text + "   -" + F.oCnxBase.Reader.GetString(1) + " (" + ArrayTreeNode[0].Name + ")";
                }
            }
            F.oCnxBase.CBReaderClose();
            if (ServerInTV != "") return ServerInTV.Substring(1);
            return null;
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

        public override void AttachLink(DrawObject o, TypeAttach Attach)
        {
            string oParent = "GuidBaiePhy";

            switch (Attach)
            {
                case TypeAttach.Parent:
                    SetValueFromName(oParent, o.GuidkeyObjet.ToString());
                    break;
            }
            base.AttachLink(o, Attach);
        }
	}
}
