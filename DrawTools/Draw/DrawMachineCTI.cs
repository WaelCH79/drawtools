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
	public class DrawMachineCTI : DrawTools.DrawRectangle
	{
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

        public int Start
        {
            get
            {
                return (int)this.GetValueFromName("Start");
            }
            set
            {
                this.InitProp("Start", (object)value, true);
            }
        }

		public DrawMachineCTI()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        
        public DrawMachineCTI(Form1 of, ArrayList lstVal, ArrayList lstValG)
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
            LstLinkOut = new ArrayList();
            LstValue = lstVal;
            Guidkey = Guid.NewGuid();

            o = GetValueFromLib("Guid");
            if(o!=null)
                GuidkeyObjet = new Guid((string) o);
            o = GetValueFromLib("Nom");
            if (o != null)
                Texte = (string)o;
            o = GetValueFromName("GuidLocation");
            if (o != null)
            {
                object o1 = GetValueFromName("NomLocation");
                string s = (string)o1 + "   (" + (string)o + ")";
                SetValueFromName("NomLocation", (object)s);
            }
            if (Start == 0 && NbrU == 0) NbrU = 4;

            Initialize();
        }

        public override bool AttachPointInObject(Point point)
        {
            return false;
        }

        public override string GetTypeSimpleTable()
        {
            //return base.GetTypeSimpleTable();
            return "ServerPhy";
        }
        public override void Draw(Graphics g)
        {
            ToolMachineCTI to = (ToolMachineCTI)F.drawArea.tools[(int)DrawArea.DrawToolType.MachineCTI];

            Pen pen = new Pen(to.LineCouleur, to.LineWidth);
            Rectangle r;

            rectangle.Height = NbrU * HeightU - 1;
            rectangle.Width = WidthBaie - 2;
            if (LstParent != null && LstParent.Count == 1)
            {
                DrawRectangle dr = (DrawRectangle)LstParent[0];
                if (dr.GetType() == typeof(DrawDrawer))
                {
                    DrawDrawer dd = (DrawDrawer)dr;
                    int nbrLames = (int)dd.GetValueFromName("NbrE");
                    if (nbrLames != 0)
                    {
                        int rangx;
                        int rangy = Math.DivRem(Start, nbrLames / 2, out rangx);
                        rectangle.X = dd.rectangle.X + 1 + rangx * (WidthBaie - 4) / nbrLames * 2;
                        rectangle.Y = dd.rectangle.Y + 1 + rangy * ((int)dd.GetValueFromName("NbrU") / 2 * HeightU-1);
                        rectangle.Width = (WidthBaie - 4)/nbrLames*2-1;
                        rectangle.Height = (int)dd.GetValueFromName("NbrU") / 2 * HeightU - 2;
                        DrawGrpTxt(g, 2, 1, dd.Rectangle.Right + 11 * AXE, dd.TextyTop + AXE + (Start + 1) * (HeightFont[2] + Axe), 0, to.Pen1Couleur, to.BkGrCouleur);
                    }
                }
                else
                {
                    rectangle.X = dr.rectangle.X + 1;
                    rectangle.Y = dr.rectangle.Y + dr.rectangle.Height - Start * HeightU;
                }
            }

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10) {
                AffRec(g, r, to);
                //DrawGrpTxt(g, 1, 0, r.Left + Axe, r.Top + Axe, 0, false);
            }
            else AffRec(g, r, to);

            pen.Dispose();
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

        private int NbrVirtuel()
        {
            int CountObj = 0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawVirtuel)) CountObj++;
            return CountObj;
        }

        public void AligneObjet()
        {
            int CountVirtuel = NbrVirtuel(), DebutMComp = CountVirtuel + 1;
            //int WidthObjet = Rectangle.Width / 2, HeightObjet = 15;
            int WidthObjet = Rectangle.Width, HeightObjet = 15;

            for (int i = LstChild.Count - 1; i >= 0; i--)
            {
                if (LstChild[i].GetType() == typeof(DrawVirtuel))
                {
                    ((DrawVirtuel)LstChild[i]).Aligne(Rectangle.X + Axe, Rectangle.Y + 2 * Axe + CountVirtuel * (HeightObjet + Axe), WidthObjet - 2 * Axe, HeightObjet);
                    CountVirtuel--;
                }
            }
        }

        public override void AttachLink(DrawObject o, TypeAttach Attach)
        {
            //string oParent = "GuidBaiePhy";
            string oParent = "Guid" + o.GetsType(true);
            if (LstParent != null && LstParent.Count == 0)
            {

                switch (Attach)
                {
                    case TypeAttach.Parent:
                        SetValueFromName(oParent, o.GuidkeyObjet.ToString());
                        break;
                }
                base.AttachLink(o, Attach);
            }
        }

        public override string GetsType(bool Reel)
        {
            if(Reel) return base.GetsType(Reel);
            return "ServerPhy";
        }

        public string FindServerFromTv()
        {
            string ServerInTV="";
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
	}
}
