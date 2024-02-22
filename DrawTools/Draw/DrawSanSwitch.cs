using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data.Odbc;
using System.Xml;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawSanSwitch : DrawTools.DrawRectangle
	{
        public ArrayList pointArray;         // list of points > 9
        private ArrayList attachhandle;

		public DrawSanSwitch()
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

        public DrawSanSwitch(Form1 of, int x, int y, int width, int height,int count)
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
            Texte = "Switch" + count;
            Guidkey = Guid.NewGuid();

            InitProp();
            Initialize();
        }

        public DrawSanSwitch(Form1 of, ArrayList lstVal, ArrayList lstValG)
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

        public override bool AttachPointInObject(Point point)
        {
            return false;
        }

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
                return ((int)AttachHandle[i]);
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

        public override void Draw(Graphics g)
        {
            ToolSanSwitch to = (ToolSanSwitch)F.drawArea.tools[(int)DrawArea.DrawToolType.SanSwitch];
            Pen pen = new Pen(to.LineCouleur, to.LineWidth);
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10) {
                AffRec(g, r, to);

                Point ptCenter = new Point((r.Left + r.Right) / 2, (r.Top + 2* r.Height / 3));
                for (int i = 0; i < pointArray.Count; i++)
                {
                    Point pt = (Point)pointArray[i];
                    g.DrawLine(pen, ptCenter.X, ptCenter.Y, pt.X, ptCenter.Y);
                    g.DrawLine(pen, pt.X, ptCenter.Y, pt.X, pt.Y);
                }
                DrawGrpTxt(g, 2, 0, r.Left + 10 * Axe, r.Top, 0, to.Pen1Couleur, to.BkGrCouleur);
                
            } else g.DrawRectangle(pen, r);

            pen.Dispose();
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
                if (handleNumber > 9 + pointArray.Count)
                    handleNumber = pointArray.Count;

                for (int i = 0; i < F.drawArea.GraphicsList.Count; i++)
                {
                    DrawObject o = F.drawArea.GraphicsList[i];
                    if (o.GetType() == typeof(DrawSanCard)) // || o.GetType() == typeof(DrawRouter))
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

        public override void savetoDB()
        {
            if (!savetoDBFait())
            {
                base.savetoDB();
                F.oCnxBase.CreatGSanSwitchPoint(this);

                savetoDBOK();
            }
        }

        public override XmlElement savetoXml(XmlDB xmlDB, bool GObj)
        {
            XmlElement elo = base.savetoXml(xmlDB, GObj);

            if (elo != null)
            {
                for (int i = 0; i < pointArray.Count; i++)
                {
                    XmlElement el = xmlDB.XmlCreatEl(xmlDB.XmlGetFirstElFromParent(elo, "After"), "GPoint", "GuidGObjet,I");
                    XmlElement elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");
                    xmlDB.XmlSetAttFromEl(elAtts, "GuidGObjet", "s", Guidkey.ToString());
                    xmlDB.XmlSetAttFromEl(elAtts, "I", "i", i.ToString());
                    xmlDB.XmlSetAttFromEl(elAtts, "X", "i", ((int)((Point)pointArray[i]).X).ToString());
                    xmlDB.XmlSetAttFromEl(elAtts, "Y", "i", ((int)((Point)pointArray[i]).Y).ToString());
                }
                return elo;
            }
            return null;
        }

        public override void CWInsert(ControlDoc cw, char cTypeVue)
        {
            if (cTypeVue == 'A' || cTypeVue == '9')
            {
                object o = null;
                string sType = "San";

                for (int i=0; i < LstLinkOut.Count; i++)
                {
                    DrawObject oscH = (DrawObject)LstLinkOut[i];
                    o = oscH.GetValueFromName("GuidSanCardA");
                    if (o != null)
                    {
                        string Link = (string)o;
                        if (Link != "")
                        {
                            string[] aLink = Link.Split(new Char[] { '(', ')' });
                            for (int j = 1; j < aLink.Length; j += 2)
                            {
                                int n = F.drawArea.GraphicsList.FindObjet(0, aLink[j].Trim());
                                if (n > -1)
                                {
                                    DrawObject oSrv = (DrawObject) oscH.LstParent[0];
                                    DrawObject oscB = (DrawObject)F.drawArea.GraphicsList[n];
                                    DrawObject oBaie = (DrawObject)oscB.LstParent[0];
                                    
                                    //t.LstField.Add(new Field("Switch", "Switch", 's', 0, 80, FieldOption.Base));
                                    //t.LstField.Add(new Field("Zone", "Zone", 's', 0, 101, FieldOption.Base));
                                    //t.LstField.Add(new Field("AliasH", "Alias Hote", 's', 0, 80, FieldOption.Base));
                                    //t.LstField.Add(new Field("WWNH", "WWN Hote", 's', 0, 102, FieldOption.Base));
                                    //t.LstField.Add(new Field("AliasB", "Alias Baie", 's', 0, 80, FieldOption.Base));
                                    //t.LstField.Add(new Field("WWNB", "WWN Baie", 's', 0, 102, FieldOption.Base));
                                    
                                    
                                    DrawTab oTab = new DrawTab(F, "TabSan");
                                    oTab.LstValue.Add(Texte);
                                    oTab.LstValue.Add("Z_" + oSrv.Texte + "_" + oscH.Texte + "_" + oBaie.Texte + "_" + oscB.Texte);
                                    oTab.LstValue.Add("A_" + oSrv.Texte + "_" + oscH.Texte);
                                    oTab.LstValue.Add(oscH.GetValueFromName("WWN")); //o = oscH.GetValueFromName("WWN"); if (o == null) oTab.LstValue.Add(""); else oTab.LstValue.Add(o);
                                    oTab.LstValue.Add("A_" + oBaie.Texte + "_" + oscB.Texte);
                                    oTab.LstValue.Add(oscB.GetValueFromName("WWN")); //o = oscB.GetValueFromName("WWN"); if (o == null) oTab.LstValue.Add(""); else oTab.LstValue.Add(o);
                                    if (cw.Exist(sType + cTypeVue) > -1)
                                        cw.InsertRowFromId(sType + cTypeVue, oTab);
                                }
                            }
                        }
                    }
                }
            }
        }        
	}
}
