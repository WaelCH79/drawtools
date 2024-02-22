using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Xml;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawIndicator : DrawTools.DrawRectangle
	{
        static private Color Couleur = Color.WhiteSmoke;
        static private Color LineCouleur = Color.Black;
        static private int LineWidth = 1;

		public DrawIndicator()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawIndicator(Form1 of, int x, int y, int width, int height, int count)
        {
            F = of;
            OkMove = true;
            Align = true;
            Rectangle = new Rectangle(x, y, width, height);
            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = new ArrayList();
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
            Texte = "I" + count;

            InitProp();
            Initialize();
        }

        public DrawIndicator(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }

        /*
        public DrawIndicator(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = true;
            InitRectangle(lstValG);
            LstParent = new ArrayList();
            LstChild = null;
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
        }*/

        /*public int NbrNCard()
        {
            int CountObj = 0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawNCard)) CountObj++;

            return CountObj;
        }*/

        public void AligneObjet()
        {
            /*string sTypeVue = (string)F.cbTypeVue.SelectedItem;

            int CountNCard = NbrNCard();
            if (CountNCard != 0)
            {
                int WidthObjet = (Rectangle.Width - Axe) / CountNCard, HeightObjet = HeightCard - 2 * Axe;

                for (int i = LstChild.Count - 1; i >= 0; i--)
                {
                    if (LstChild[i].GetType() == typeof(DrawNCard))
                    {
                        ((DrawNCard)LstChild[i]).Aligne(Rectangle.Left + WidthObjet * (CountNCard - 1) + Axe, Rectangle.Top + Axe + HeightFont[0]*2, WidthObjet - Axe, HeightObjet);
                        CountNCard--;
                    }
                }
            }*/
        }
             

        public override bool AttachPointInObject(Point point)
        {
            return false;
        }

        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(LineCouleur, LineWidth);
            Rectangle r;
            
            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            AffRec(g, r, LineCouleur, LineWidth, Couleur, 5, false, false, true);

            ArrayList aArray = (ArrayList)GetValueFromName("Indicator");
            switch (aArray.Count)
            {
                case 1:
                    Image Img = Image.FromFile(F.sPathRoot + @"\bouton\" + F.sImgList[Convert.ToInt16(aArray[0])]);
                    g.DrawImage(Img, new Rectangle(r.Left + 1, r.Top + 1, HeightIndicator - 2, HeightIndicator - 2));
                    break;
                case 3:
                    double Total=0;
                    for (int n = 0; n < aArray.Count; n++) Total += (double)aArray[n];
                    if (Total != 0)
                    {
                        for (int n = 0; n < aArray.Count; n++)
                        {
                            //if ((double)aArray[n] != 0) g.DrawLine(pen, r.Left + 2, r.Top + 4 + n * (r.Height - 8) / (aArray.Count-1), (float)(r.Left + 2 + (double)aArray[n] / Total * (r.Width - 4)), r.Top + 4 + n * (r.Height - 8) / (aArray.Count-1));
                            if ((double)aArray[n] != 0) g.FillRectangle(new System.Drawing.SolidBrush(Color.BlueViolet), r.Left + 2, (r.Top + 4 + n * (r.Height - 8) / (aArray.Count - 1))-2, (float)((double)aArray[n] / Total * (r.Width - 4)), 4);
                        }
                        //if ((double)aArray[1] != 0) g.DrawLine(pen, r.Left + 1, r.Top + 10, (float)(r.Left + (double)aArray[1] / Total * (r.Width - 2)), r.Top + 10);
                        //if ((double)aArray[2] != 0) g.DrawLine(pen, r.Left + 1, r.Top + 15, (float)(r.Left + (double)aArray[2] / Total * (r.Width - 2)), r.Top + 15);
                    }
                    break;
            }
            
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

        public string FindServerFromTv()
        {
            string ServerInTV = "";
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

        public override void savetoDB()
        {
            if (!savetoDBFait())
            {
                savetoDBOK();
            }
        }

        public override XmlElement savetoXml(XmlDB xmlDB, bool GObj)
        {
            //return base.savetoXml(elVue);
            return null;
        }
    }
}
