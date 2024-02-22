using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Data.Odbc;
using System.Xml;


namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawVLan : DrawTools.DrawRectangle
	{
        public ArrayList pointArray;         // list of points > 9
        private ArrayList attachhandle;

        public Color colorVLan
        {
            get
            {
                Color c;
                int n = GetIndexFromName("Couleur");
                if (n > -1) c = Color.FromName((string)LstValue[n]); else c = Color.Black;

                return (Color)c;
            }
            set
            {
                this.InitProp("Couleur", (object)value, true);
            }
        }

		public DrawVLan()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public ArrayList AttachHandle
        {
            get
            {
                return attachhandle;
            }
            set
            {
                attachhandle = value;
            }
        }

        public int GetAttchHandle(Rectangle r)
        {
            r.X -= 1; r.Y -= 1; r.Width += 2; r.Height += 2;
            for (int i = 0; i < pointArray.Count; i++)
            {
                if(r.Contains((Point)pointArray[i])) return i;
            }
            return -1;
        }

        public DrawVLan(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }

        public DrawVLan(Form1 of, int x, int y, int width, int height,int count)
        {
            F = of;
            OkMove = true;
            Align = true;
            Rectangle = new Rectangle(x, y, width, height);
            pointArray = new ArrayList();
            LstParent = null;
            LstChild = null;
            LstLinkIn = null;
            LstLinkOut = new ArrayList();
            LstValue = new ArrayList();
            AttachHandle = new ArrayList();
            GuidkeyObjet = Guid.NewGuid();
            Texte = "VLan" + count;
            Guidkey = Guid.NewGuid();

            InitProp();
            string sguid = "97f0837b-2ee9-4e10-ac47-0dfc1e829dfc";
            string sVal = "N/A" + "   (" + sguid + ")";
            SetValueFromName("NomVlanClass", (object)sVal);
            SetValueFromName("GuidVlanClass", (object)sguid);
            Initialize();
        }

        public DrawVLan(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = true;
            Align = true;
            InitRectangle(lstValG);
            pointArray = new ArrayList();
            LstParent = null;
            LstChild = null;
            LstLinkIn = null;
            LstLinkOut = new ArrayList();
            LstValue = lstVal;
            AttachHandle = new ArrayList();
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

        /*public override bool AttachPointInObject(Point point)
        {
            return false;
        }*/

        public override int HandleEvent(Point point, int Handle)
        {
            if (Handle == 9)
            {
                pointArray.Add(point);
                Handle += pointArray.Count;
            }
            return Handle;
        }
                
        /// <summary>
        /// Vérifie si l'objet à déplacer n'est attaché avec d'autres objets
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override int MovePossible(DrawObject o)
        {
            int i = LstLinkOut.IndexOf(o); 
            if (i > -1)
            {
                return i + 10;
                //return ((int)AttachHandle[i]);
            }
            return -1;
        }
                
        /// <summary>
        /// Move un point de l'object
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public override Point MoveHandle(int handleNumber, int deltaX, int deltaY)
        {
            if (handleNumber > 9)
            {
                return new Point(((Point)pointArray[handleNumber - 10]).X + deltaX, ((Point)pointArray[handleNumber - 10]).Y + deltaY);
            }
            return new Point(0, 0);
        }

        public int NbrNCard()
        {
            int CountObj = 0;

            for (int i = 0; i < LstLinkOut.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawNCard)) CountObj++;
            return CountObj;
        }

        public override DrawArea.DrawToolType GetToolTypeForObjExp()
        {
            return DrawArea.DrawToolType.VLan;
        }

        public override void dataGrid_CellClick(DataGridView odgv, DataGridViewCellEventArgs e)
        {
            //if (odgv.CurrentCell.RowIndex == 2) // Ligne Link Applicatif
            int n;

            n = GetIndexFromName("Couleur");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceColor();
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }

            n = GetIndexFromName("NomVlanClass");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromDB("Select GuidVlanClass, NomVlanClass From VlanClass", "Value");
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }

        }

        public override void Draw(Graphics g)
        {

            if (F != null)
            {
                ToolVLan to = (ToolVLan)F.drawArea.tools[(int)DrawArea.DrawToolType.VLan];


                Pen pen = new Pen(colorVLan, to.LineWidth);
                Rectangle r;

                r = DrawRectangle.GetNormalizedRectangle(Rectangle);


                if (r.Width > 20 && r.Height > 10)
                {

                    Point ptCenter = new Point((r.Left + r.Right) / 2, (r.Top + 2 * r.Height / 3));
                    for (int i = 0; i < pointArray.Count; i++)
                    {
                        Point pt = (Point)pointArray[i];
                        g.DrawLine(pen, ptCenter.X, ptCenter.Y, pt.X, ptCenter.Y);
                        g.DrawLine(pen, pt.X, ptCenter.Y, pt.X, pt.Y);

                    }

                    AffRec(g, r, to, colorVLan, colorVLan);
                    AffRec(g, new Rectangle(r.X, r.Y, 2 * radius, r.Height), to, colorVLan, colorVLan);

                    if (F.bPtt)
                        DrawGrpTxt(g, 4, 0, r.Left + 10 * Axe, r.Top, 0, to.Pen1Couleur, to.BkGrCouleur);
                    else
                    {
                        /*GraphicsPath gp = new GraphicsPath();
                        gp.AddLine(r.X + r.Width / 24, r.Y, r.X + r.Width * 23 / 24, r.Y);
                        gp.AddArc(r.X + r.Width * 11 / 12, r.Y, r.Width / 12, r.Height, 270, 180);
                        gp.AddLine(r.X + r.Width / 24, r.Y + r.Height, r.X + r.Width * 23 / 24, r.Y + r.Height);
                        gp.AddArc(r.X, r.Top, r.Width / 12, r.Height, 90, -180);
                        AffMasque(g, r, colorVLan, 2, true, gp);

                        gp = new GraphicsPath();
                        gp.AddEllipse(r.X, r.Y, r.Width / 12, r.Height);
                        AffMasque(g, r, colorVLan, 1, true, gp);*/

                        DrawGrpTxt(g, 2, 0, r.Left + 10 * Axe, r.Top, 0, to.Pen1Couleur, to.BkGrCouleur);
                    }
                }
                else g.DrawRectangle(pen, r);

                pen.Dispose();
            }
        }

        /// <summary>
        /// Get number of handles
        /// </summary>
        public override int HandleCount
        {
            get
            {
                return 9 + pointArray.Count;
            }
        }

        /// <summary>
        /// Move object
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public override void Move(int deltaX, int deltaY)
        {
            rectangle.X += deltaX;
            rectangle.Y += deltaY;
        }

        /// <summary>
        /// Get handle point by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public override Point GetHandle(int handleNumber)
        {
            int x, y, xCenter, yCenter;

            if (handleNumber < 10)
            {
                xCenter = Rectangle.X + Rectangle.Width / 2;
                yCenter = Rectangle.Y + Rectangle.Height / 2;
                x = Rectangle.X;
                y = Rectangle.Y;

                switch (handleNumber)
                {
                    case 1:
                        x = Rectangle.X;
                        y = Rectangle.Y;
                        break;
                    case 2:
                        x = xCenter;
                        y = Rectangle.Y;
                        break;
                    case 3:
                        x = Rectangle.Right;
                        y = Rectangle.Y;
                        break;
                    case 4:
                        x = Rectangle.Right;
                        y = yCenter;
                        break;
                    case 5:
                        x = Rectangle.Right;
                        y = Rectangle.Bottom;
                        break;
                    case 6:
                        x = xCenter;
                        y = Rectangle.Bottom;
                        break;
                    case 7:
                        x = Rectangle.X;
                        y = Rectangle.Bottom;
                        break;
                    case 8:
                        x = Rectangle.X;
                        y = yCenter;
                        break;
                    case 9:
                        x = xCenter;
                        y = yCenter;
                        break;
                }

                return new Point(x, y);
            }

            if (handleNumber > 9 + pointArray.Count)
                handleNumber = pointArray.Count;

            return ((Point)pointArray[handleNumber - 10]);

        }

        public void AjustePoint(int handlepoint, float ratio, int oldx, int newx, int y)
        {
            Point pt = (Point)pointArray[handlepoint];
            pt.X = newx + (int)(pt.X * ratio) - (int)(oldx * ratio);
            pt.Y = y;
            pointArray[handlepoint] = pt;
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

            if (handleNumber < 10)
            {
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
            }
            else
            {
                //if (handleNumber > 9 + pointArray.Count) handleNumber = pointArray.Count;

                for (int i = 0; i < F.drawArea.GraphicsList.Count; i++)
                {
                    DrawObject o = F.drawArea.GraphicsList[i];
                    if (o.GetType() == typeof(DrawNCard) || o.GetType() == typeof(DrawRouter))
                    {
                        if (F.drawArea.GraphicsList[i].AttachPointInObject(point))
                        {
                            point = F.drawArea.GraphicsList[i].GetPointObject(point);
                            break;
                        }
                    }
                }
                pointArray[handleNumber - 10] = point;
            }

            SetRectangle(left, top, right - left, bottom - top);
        }       

        public override void RemoveGSpecifique(string obj)
        {
            F.oCnxBase.CBWrite("DELETE FROM GPoint WHERE GuidGObjet = '" + obj + "'");
        }

        public override void savetoDB()
        {
            if (!savetoDBFait())
            {
                base.savetoDB();
                F.oCnxBase.CreatGVLanPoint(this);

                savetoDBOK();
            }
        }

        public override void CWInsert(ControlDoc cw, char cTypeVue)
        {
            if (cTypeVue == '3' || cTypeVue == '4' || cTypeVue == '5')
            {
                string sGuidVue = F.GuidVue.ToString().Replace("-", "");
                string sType = GetType().Name.Substring("Draw".Length);
                string sVLanBook = sType + sGuidVue;
                string sGuidKey = GuidkeyObjet.ToString().Replace("-", "");
                string sObj = sGuidVue.Substring(0, 16) + sGuidKey.Substring(0, 16);
                string sBook = sType.Substring(0, 3) + sObj;
                string sGuid = "n" + cTypeVue + sBook;

                if (cw.Exist(sGuid) > -1)
                {
                    cw.InsertTextFromId(sGuid, true, Texte, "Titre 4");
                }
                else if (cw.Exist(sVLanBook) > -1)
                {
                    //sType ne doit pas depasse 4 caracteres
                    cw.InsertTextFromId(sVLanBook, false, "\n", null);
                    cw.CreatIdFromIdP(sBook, sVLanBook);
                    cw.InsertTextFromId(sBook, true, Texte + "\n", "Titre 4");
                    cw.CreatIdFromIdP(sGuid, sBook);

                    CWInsertProp(cw, sBook, "P");
                }
            } 
       }

        public override XmlElement XmlCreatGObject(XmlDB xmlDB, XmlElement elParent)
        {
            XmlElement elG = base.XmlCreatGObject(xmlDB, elParent);
            if (elG != null)
            {
                for (int i = 0; i < pointArray.Count; i++)
                {
                    XmlElement el = xmlDB.XmlCreatEl(xmlDB.XmlGetFirstElFromParent(elG, "After"), "GPoint", "GuidGObjet,I");
                    XmlElement elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");
                    xmlDB.XmlSetAttFromEl(elAtts, "GuidGObjet", "s", Guidkey.ToString());
                    xmlDB.XmlSetAttFromEl(elAtts, "I", "i", i.ToString());
                    xmlDB.XmlSetAttFromEl(elAtts, "X", "i", ((int)((Point)pointArray[i]).X).ToString());
                    xmlDB.XmlSetAttFromEl(elAtts, "Y", "i", ((int)((Point)pointArray[i]).Y).ToString());
                }
            }

            return elG;
        }
        
	}
}
