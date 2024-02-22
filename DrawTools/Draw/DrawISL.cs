using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawISL : DrawTools.DrawRectangle
	{
        public int NbrInt
        {
            get
            {
                return (int)this.GetValueFromName("NbrInt"); ;
            }
            set
            {
                this.InitProp("NbrInt", (object)value, true);
            }
        }

        public DrawISL()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawISL(Form1 of, int x, int y, int width, int height, int count)
        {
            F = of;
            OkMove = true;
            Align = true;
            Rectangle = new Rectangle(x, y, width, height);
            LstParent = null; 
            LstChild = new ArrayList();
            LstLinkIn = new ArrayList();
            LstLinkOut = new ArrayList();
            LstValue = new ArrayList();
            GuidkeyObjet = Guid.NewGuid();
            Texte = "ISL" + count;
            Guidkey = Guid.NewGuid();
            
            InitProp();

            InitProp("NbrInt", (object)4, true);
            Initialize();
        }

        public DrawISL(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = true;
            Align = true;
            InitRectangle(lstValG);
            LstParent = null;
            LstChild = new ArrayList();
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
                
        public void DeleteServerLink(string field)
        {
            ArrayList aServerLink = new ArrayList();

            int idx = GetIndexFromName("Guid" + field);
            if (idx > -1)
            {
                string sQuery = "SELECT " + field + ".Guid" + field + " FROM " + field + ", " + field + "Link WHERE " + field + ".Guid" + field + "=" + field + "Link.Guid" + field + " and GuidServerPhy ='" + GuidkeyObjet + "'";
                if (F.oCnxBase.CBRecherche(sQuery))
                {
                    while (F.oCnxBase.Reader.Read()) aServerLink.Add(F.oCnxBase.Reader.GetString(0));
                    F.oCnxBase.CBReaderClose();
                }
                else F.oCnxBase.CBReaderClose();
            }
            for (int i = 0; i < aServerLink.Count; i++)
                F.oCnxBase.CBWrite("DELETE FROM " + field + "Link WHERE Guid" + field + "='" + (string) aServerLink[i] + "' AND GuidServerPhy ='" + GuidkeyObjet + "'");
        }

        public void GetServerLinks(string field)
        {
            int idx = GetIndexFromName("Guid" + field);
            if (idx > -1)
            {
                string fieldInf = "";
                string sQuery = "SELECT " + field + ".Guid" + field + ", Nom" + field + " FROM " + field + ", " + field + "Link WHERE " + field + ".Guid" + field +"=" + field + "Link.Guid" + field + " and GuidServerPhy ='" + GuidkeyObjet + "'";
                if (F.oCnxBase.CBRecherche(sQuery))
                {
                    while (F.oCnxBase.Reader.Read())
                    {
                        fieldInf += ";" + F.oCnxBase.Reader.GetString(1) + "   (" + F.oCnxBase.Reader.GetString(0) + ")";
                    }
                    F.oCnxBase.CBReaderClose();
                    LstValue[idx] = fieldInf.Substring(1);
                }
                else F.oCnxBase.CBReaderClose();
            }
        }

        /*
        public int EpaisseurCard
        {
            get { return HeightCard;}
        }
        */
        public override void Draw(Graphics g)
        {
            ToolISL to = (ToolISL)F.drawArea.tools[(int)DrawArea.DrawToolType.ISL];

            Pen pen = new Pen(to.Couleur, to.LineWidth);

            Rectangle r;
            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            AffRec(g, r, to);

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


        /// <summary>
        /// Save Object to the Data Base
        /// </summary>
        public override void savetoDB()
        {
            if (!savetoDBFait())
            {
                base.savetoDB();

                savetoDBOK();
            }
        }

        public void SetServerLinks(string field)
        {

            object o = GetValueFromName(field);
            if (o != null)
            {
                string Link = (string)o;
                if (Link != "")
                {
                    string[] aLink = Link.Split(new Char[] { '(', ')' });
                    for (int i = 1; i < aLink.Length; i += 2)
                    {
                        if (!F.oCnxBase.ExistISLLink(field, aLink[i].Trim(), GuidkeyObjet.ToString()))
                            F.oCnxBase.CBWrite("INSERT INTO " + field.Substring("Guid".Length) + "Link (" + field + ", GuidServerPhy) VALUES ('" + aLink[i].Trim() + "','" + GuidkeyObjet + "')");
                    }
                }
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
            if (NbrInt > CountNCard)
            {
                int Nbr = NbrInt - CountNCard;
                for (int i = 0; i < Nbr; i++)
                {
                    DrawSanCard dsc = new DrawSanCard(F, new Point(rectangle.X, rectangle.Y), this);
                    F.drawArea.tools[(int)DrawArea.DrawToolType.ISL].AddNewObjectFromDraw(F.drawArea, dsc, false);
                    AttachLink(dsc, DrawObject.TypeAttach.Child);
                    dsc.AttachLink(this, DrawObject.TypeAttach.Parent);
                }
                CountNCard = NbrNCard();
            }
            if(CountNCard!=0)
            {
                int div, rangx, rangy, WidthObjet, HeightObjet = HeightCard - Axe;
                int nbrLigne = Math.DivRem(rectangle.Height, HeightCard, out rangx);

                if (CountNCard <= nbrLigne) div = 1;
                else if (nbrLigne == 0) div = CountNCard;
                else
                {
                    div = Math.DivRem(CountNCard, nbrLigne, out rangx);
                    if(rangx!=0) div++;
                }
                WidthObjet = (Rectangle.Width-Axe) / div;

                for (int i = LstChild.Count - 1; i >= 0; i--)
                {

                    rangy = Math.DivRem(i, div, out rangx);
                    if (LstChild[i].GetType() == typeof(DrawSanCard))
                    {
                        ((DrawSanCard)LstChild[i]).Aligne(Rectangle.Left + WidthObjet * rangx + Axe, Rectangle.Top + Axe + rangy * HeightCard, WidthObjet - Axe, HeightObjet);
                    }
                }
            }
        }       

	}
}
