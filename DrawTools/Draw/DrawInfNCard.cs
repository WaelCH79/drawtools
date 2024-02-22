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
	public class DrawInfNCard : DrawTools.DrawRectangle
	{
        static private Color Couleur = Color.PaleGoldenrod;
        static private Color LineCouleur = Color.Black;

        private int handleVLan;
        public int HandleVLan
        {
            get
            {
                return handleVLan;
            }
            set
            {
                handleVLan = value;
            }
        }

        public int Hauteur
        {
            get
            {
                return (int) this.GetValueFromName("Hauteur");;
            }
            set
            {
                this.InitProp("Hauteur", (object) value, true);
            }
        }

		public DrawInfNCard()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}
        /*
        public DrawInfNCard(Form1 of, Point pt, DrawServerPhy ds)
        {
            F = of;
            OkMove = false;
            Rectangle = new Rectangle(1, 1, 1, 1);
            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = new ArrayList();
            LstValue = new ArrayList();
            LstLinkOut = null;

            Texte = "e" + ds.NbrNCard('N');
            GuidkeyObjet = Guid.NewGuid();
            Guidkey = Guid.NewGuid();
            InitProp();
            Hauteur = setHauteur(pt, ds);
            Initialize();
        }

        public DrawInfNCard(Form1 of, Point pt, DrawCluster ds)
        {
            F = of;
            OkMove = false;
            Rectangle = new Rectangle(1, 1, 1, 1);
            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = new ArrayList();
            LstValue = new ArrayList();
            LstLinkOut = null;

            Texte = "e" + ds.NbrNCard();
            GuidkeyObjet = Guid.NewGuid();
            Guidkey = Guid.NewGuid();
            InitProp();
            Hauteur = setHauteur(pt, ds);
            Initialize();
        }
        */

        public DrawInfNCard(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = false;
            Align = false;
            InitRectangle(lstValG);
            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = new ArrayList();
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

        public override string GetTypeSimpleTable()
        {
            return "NCard";
        }

        public override bool ParentPointInObject(Point point)
        {
            return false;
        }

        public int setHauteur(Point pt, DrawRectangle ds)
        {
            if (pt.Y - ds.Rectangle.Top < ds.Rectangle.Bottom - pt.Y)
            {
                return 0;
            }
            else
            {
                //return ds.Rectangle.Bottom-ds.EpaisseurCard;
                return 1;
            }
        }

        public void Aligne(int x, int y, int width, int height)
        {
            DrawRectangle ds;

            ds = (DrawRectangle) LstParent[0];
            Rectangle = new Rectangle(x, y + Hauteur * (ds.Rectangle.Height - ds.EpaisseurCard), width, height);
        }


        public override void Draw(Graphics g)
        {

            ToolInfNCard to = (ToolInfNCard)F.drawArea.tools[(int)DrawArea.DrawToolType.InfNCard];
            Pen pen = new Pen(to.LineCouleur, to.LineWidth);
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height >= 10)
            {
                AffRec(g, r, to, 0);

                DrawGrpTxt(g, 2, 1, r.Left + Axe, r.Top + r.Height / 2 - 3 * HeightFont[0] / 4, 0, to.Pen1Couleur, to.BkGrCouleur);
            }
            else g.DrawRectangle(pen, r);

            pen.Dispose();

        }

	}
}
