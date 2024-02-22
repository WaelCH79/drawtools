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
	public class DrawMachine : DrawTools.DrawRectangle
	{
        
       	public DrawMachine()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawMachine(Form1 of, int x, int y, int width, int height,int count)
        {
            F = of;
            OkMove = true;
            Align = true;
            NbrCreatChild = 0;
                        
            Rectangle = new Rectangle(x, y, width, height);
            LstParent = new ArrayList();
            LstChild = new ArrayList();
            LstLinkIn = null;
            LstLinkOut = new ArrayList();
            LstValue = new ArrayList();
            GuidkeyObjet = Guid.NewGuid();
            Texte = "Machine" + count;
            Guidkey = Guid.NewGuid();
            InitProp();
            SetValueFromName("Type", (object)"H");
            Initialize();
        }

        public DrawMachine(Form1 of, ArrayList lstVal, ArrayList lstValG)
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

            Initialize();
        }

        //public override bool AttachPointInObject(Point point)
        //{
        //    return false;
        //}
        
        public override void Draw(Graphics g)
        {
            ToolMachine to = (ToolMachine)F.drawArea.tools[(int)DrawArea.DrawToolType.Machine];
            Pen pen = new Pen(to.LineCouleur, to.LineWidth);
            Pen pen1 = new Pen(to.LineCouleur, to.Line1Width);
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10) {
                AffRec(g, r, to);
                g.DrawLine(pen1, r.Left, r.Top + HeightServer, r.Right, r.Top + HeightServer);

                DrawGrpTxt(g, 1, 0, r.Left + Axe, r.Top + Axe, 0, to.Pen1Couleur, to.BkGrCouleur);
            } else g.DrawRectangle(pen, r);

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

        public override string GetTypeSimpleTable()
        {
            //return base.GetTypeSimpleTable();
            return "ServerPhy";
        }

        public override void AttachLink(DrawObject o, TypeAttach Attach)
        {
            string oParent = "GuidHost";

            switch (Attach)
            {
                case TypeAttach.Parent:
                    SetValueFromName(oParent, o.GuidkeyObjet.ToString());
                    break;
            }
            base.AttachLink(o, Attach);
        }

        public override string GetsType(bool Reel)
        {
            if(Reel) return base.GetsType(Reel);
            return "ServerPhy";
        }

        public string FindServerFromTv()
        {
            string ServerInTV="";
            if (F.oCnxBase.CBRecherche("Select GuidServerPhy, Type From ServerPhy Where GuidHost='" + GuidkeyObjet + "'"))
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
