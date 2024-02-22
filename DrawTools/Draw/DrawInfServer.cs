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
	public class DrawInfServer : DrawTools.DrawRectangle
	{
        static private Color Couleur = Color.YellowGreen;
        static private Color LineCouleur = Color.Black;

		public DrawInfServer()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        
        public DrawInfServer(Form1 of, ArrayList lstVal, ArrayList lstValG)
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

        public override void InitRectangle(ArrayList lstValG, bool bValG=true)
        {
            string sType = GetTypeSimpleGTable();

            Table t;
            int n = F.oCnxBase.ConfDB.FindTable(sType);
            if (n > -1)
            {
                t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                int x = t.FindField(t.LstField, "X");
                int y = t.FindField(t.LstField, "Y");
                int w = t.FindField(t.LstField, "Width");
                int h = t.FindField(t.LstField, "Height");
                /*if (x > -1 && y > -1 && w > -1 && h > -1)
                {
                    int x1 = F.drawArea.GraphicsList.GetNewX("InfServer", (int)lstValG[x], (int)lstValG[y]);
                    if (x1 != (int)lstValG[x])
                    {
                        Rectangle = new Rectangle(x1, (int)lstValG[y], WidthServerSite, 4 * HeightCard);
                    }
                    else Rectangle = new Rectangle((int)lstValG[x], (int)lstValG[y], (int)lstValG[w], (int)lstValG[h]);
                }
                else*/ Rectangle = new Rectangle(0, 0, 0, 0);
            }
        }

        public override bool AttachPointInObject(Point point)
        {
            return false;
        }

        public void Aligne(int x, int y, int width, int height)
        {
            Rectangle = new Rectangle(x, y, width, height);
            AligneObjet();
        }

        public void AligneObjet()
        {
            int CountInfNCard = NbrInfNCard();
            int WidthObjet = Rectangle.Width, HeightObjet = 15;

            if (CountInfNCard == 0) rectangle.Height = HeightObjet;
            else rectangle.Height = CountInfNCard * (Axe + HeightTechno) + Axe;
            for (int i = LstChild.Count - 1; i >= 0; i--)
            {
                if (LstChild[i].GetType() == typeof(DrawInfNCard))
                {
                    ((DrawInfNCard)LstChild[i]).Aligne(Rectangle.X + 3 * Axe, Rectangle.Y + 3 * Axe + (CountInfNCard - 1) * (HeightInfNCard + Axe), WidthObjet - 6 * Axe, HeightInfNCard);
                    CountInfNCard--;
                }
            }
        }

        public override void Draw(Graphics g)
        {

            ToolInfServer to = (ToolInfServer)F.drawArea.tools[(int)DrawArea.DrawToolType.InfServer];

            Pen pen = new Pen(to.LineCouleur, to.LineWidth);
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);


            if (r.Width > 20 && r.Height > 10)
            {
                AffRec(g, r, to, 0);
                //DrawGrpTxt(g, 1, 2, r.Left + Axe, r.Top + r.Height / 2 - 3 * HeightFont[0] / 4, 0, to.Pen1Couleur);
                DrawGrpTxt(g, 1, 2, r.Left + Axe, r.Top - HeightFont[0] / 2, 0, to.Pen1Couleur, to.BkGrCouleur);
            }
            else g.DrawRectangle(pen, r);

            pen.Dispose();
        }

        

        public int NbrInfNCard()
        {
            int CountObj = 0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawInfNCard)) CountObj++;
            return CountObj;
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

        public override string GetTypeSimpleGTable()
        {
            return "GServer";
        }
	}
}
