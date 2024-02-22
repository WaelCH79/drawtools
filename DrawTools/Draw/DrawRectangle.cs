using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;
using System.Collections;
using System.Runtime.Serialization;
using System.Xml;
using System.Collections.Generic;

namespace DrawTools
{
	/// <summary>
	/// Rectangle graphic object
	/// </summary>
	public class DrawRectangle : DrawTools.DrawObject
	{
        public Rectangle rectangle;
        public int NbrCreatChild;
        
        private const string entryRectangle = "Rect";


        public Rectangle Rectangle
        {
            get
            {
                return rectangle;
            }
            set
            {
                rectangle = value;
            }
        }
        
        
		public DrawRectangle()
		{
            SetRectangle(0, 0, 1,1);
            Initialize();
		}

        
        public DrawRectangle(int x, int y, int width, int height)
        {
            rectangle.X = x;
            rectangle.Y = y;
            rectangle.Width = width;
            rectangle.Height = height;
            
            LstParent = null;
            LstChild = null;
            LstLinkIn = null;
            LstLinkOut = null;

            Initialize();
        }

        public virtual void InitRectangle(ArrayList lstVal, bool bValG = true)
        {
            string sType = GetTypeSimpleGTable();

            if(!bValG) sType = GetTypeSimpleTable();

            Table t;
            int n = F.oCnxBase.ConfDB.FindTable(sType);
            if (n > -1)
            {
                t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                int x = t.FindField(t.LstField, "X");
                int y = t.FindField(t.LstField, "Y");
                int w = t.FindField(t.LstField, "Width");
                int h = t.FindField(t.LstField, "Height");
                if (x > -1 && y > -1 && w > -1 && h > -1)
                {
                    Rectangle = new Rectangle((int)lstVal[x], (int)lstVal[y], (int)lstVal[w], (int)lstVal[h]);
                }
                else Rectangle = new Rectangle(0, 0, 0, 0);
            }
        }

        public virtual int GetNewyTop(int yTop)
        {
            return 0;
        }


        /// <summary>
        /// Draw rectangle
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(Color, PenWidth);

            g.DrawRectangle(pen, DrawRectangle.GetNormalizedRectangle(Rectangle));

            pen.Dispose();
        }

        protected void SetRectangle(int x, int y, int width, int height)
        {
            rectangle.X = x;
            rectangle.Y = y;
            rectangle.Width = width;
            rectangle.Height = height;
        }

             


        /// <summary>
        /// Get number of handles
        /// </summary>
        public override int HandleCount
        {
            get
            {
                return 8;
            }
        }


        /// <summary>
        /// Get handle point by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public override Point GetHandle(int handleNumber)
        {
            int x, y, xCenter, yCenter;

            xCenter = rectangle.X + rectangle.Width/2;
            yCenter = rectangle.Y + rectangle.Height/2;
            x = rectangle.X;
            y = rectangle.Y;

            switch ( handleNumber )
            {
                case 1:
                    x = rectangle.X;
                    y = rectangle.Y;
                    break;
                case 2:
                    x = xCenter;
                    y = rectangle.Y;
                    break;
                case 3:
                    x = rectangle.Right;
                    y = rectangle.Y;
                    break;
                case 4:
                    x = rectangle.Right;
                    y = yCenter;
                    break;
                case 5:
                    x = rectangle.Right;
                    y = rectangle.Bottom;
                    break;
                case 6:
                    x = xCenter;
                    y = rectangle.Bottom;
                    break;
                case 7:
                    x = rectangle.X;
                    y = rectangle.Bottom;
                    break;
                case 8:
                    x = rectangle.X;
                    y = yCenter;
                    break;
                case 9:
                    x = xCenter;
                    y = yCenter;
                    break;
            }

            return new Point(x, y);

        }

        /// <summary>
        /// Hit test.
        /// Return value: -1 - no hit
        ///                0 - hit anywhere
        ///                > 1 - handle number
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override int HitTest(Point point)
        {
            if ( Selected )
            {
                for ( int i = 1; i <= HandleCount; i++ )
                {
                    if ( GetHandleRectangle(i).Contains(point) )
                        return i;
                }
            }

            if ( PointInObject(point) )
                return 0;

            return -1;
        }

        public void LoadMainComposant_Serv(string sGuid)
        {
            F.drawArea.tools[(int)DrawArea.DrawToolType.MainComposant].LoadSimpleObject(sGuid);
            int j = F.drawArea.GraphicsList.FindObjet(0, sGuid);


            DrawMainComposant dmc = (DrawMainComposant)F.drawArea.GraphicsList[j];

            dmc.GuidkeyObjet = Guid.NewGuid(); //Afin de différencier les mêmes MainComposant dans une Vue
            AttachLink(dmc, DrawObject.TypeAttach.Child);
            dmc.AttachLink(this, DrawObject.TypeAttach.Parent);

        }

        public void LoadServerType(string sGuid)
        {
            F.drawArea.tools[(int)DrawArea.DrawToolType.ServerType].LoadSimpleObject(sGuid);
            int j = F.drawArea.GraphicsList.FindObjet(0, sGuid);

            DrawServerType dst = (DrawServerType)F.drawArea.GraphicsList[j];

            dst.GuidkeyObjet = Guid.NewGuid(); //Afin de différencier les mêmes ServerType dans une Vue
            AttachLink(dst, DrawObject.TypeAttach.Child);
            dst.AttachLink(this, DrawObject.TypeAttach.Parent);
        }


        public override bool PointInObject(Point point)
        {
            if (rectangle.X == point.X && rectangle.Y == point.Y) return true;
            return rectangle.Contains(point);
        }

        public override bool ParentPointInObject(Point point)
        {
            if (rectangle.X == point.X && rectangle.Y == point.Y) return true;
            return rectangle.Contains(point);
        }

        public override bool AttachPointInObject(Point point)
        {
            if (rectangle.X == point.X && rectangle.Y == point.Y) return true;
            return rectangle.Contains(point);
        }
       
        /// <summary>
        /// Retourne le point le plus proche de l'objet
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override Point GetPointObject(Point point)
        {
            int ptx, pty;

            if (point.X - rectangle.Left >= rectangle.Right - point.X) ptx = rectangle.Right; else ptx = rectangle.Left;
            if (point.Y - rectangle.Top >= rectangle.Bottom - point.Y) pty = rectangle.Bottom; else pty = rectangle.Top;
            if (System.Math.Abs(ptx - point.X) >= System.Math.Abs(pty - point.Y)) point.Y = pty; else point.X = ptx;
                  
            return point;
        }

        /// <summary>
        /// Retourne ou se trouvre le point par rapport à l'objet
        /// </summary>
        /// <param name="point"></param>
        /// <returns>1:top, 2:bottom, 3:Left, 4:Right, 0:autre</returns>
        public override int LePointEstSitue(Point point)
        {
            int diff, diff1, ReturnValue=0;

            diff = Math.Abs(point.Y - rectangle.Top); ReturnValue = 1;
            diff1 = Math.Abs(point.Y - rectangle.Bottom);
            if (diff1 < diff) { diff = diff1; ReturnValue = 2; }
            diff1 = Math.Abs(point.X - rectangle.Left);
            if (diff1 < diff) { diff = diff1; ReturnValue = 3; }
            diff1 = Math.Abs(point.X - rectangle.Right);
            if (diff1 < diff) ReturnValue = 4;
            /*
            if (point.Y == rectangle.Top) return 1;
            if (point.Y == rectangle.Bottom) return 2;
            if (point.X == rectangle.Left) return 3;
            if (point.X == rectangle.Right) return 4;
            return 0;*/
            return ReturnValue;
        }

        /// <summary>
        /// Get cursor for the handle
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public override Cursor GetHandleCursor(int handleNumber)
        {
            switch ( handleNumber )
            {
                case 1:
                    return Cursors.SizeNWSE;
                case 2:
                    return Cursors.SizeNS;
                case 3:
                    return Cursors.SizeNESW;
                case 4:
                    return Cursors.SizeWE;
                case 5:
                    return Cursors.SizeNWSE;
                case 6:
                    return Cursors.SizeNS;
                case 7:
                    return Cursors.SizeNESW;
                case 8:
                    return Cursors.SizeWE;
                case 9:
                    return Cursors.SizeWE;
                default:
                    return Cursors.Default;
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

            switch ( handleNumber )
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

        

        public override bool IntersectsWith(Rectangle rectangle)
        {
            return Rectangle.IntersectsWith(rectangle);
        }

        /// <summary>
        /// Move object
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public override void Move(int deltaX, int deltaY)
        {
            if (!Selected || OkMove)
            {
                rectangle.X += deltaX;
                rectangle.Y += deltaY;
            }
            if (LstLinkIn != null)
            {
                DrawObject o;

                for (int i=0; i < LstLinkIn.Count; i++)
                {
                    o = (DrawObject) LstLinkIn[i];
                    if (!o.Selected)
                    {
                        int handle = o.MovePossible(this);
                        if (handle == -1)
                        {
                            o.Move(deltaX, deltaY);
                        }
                        else o.MoveHandleTo(o.MoveHandle(handle, deltaX, deltaY), handle);
                    }
                }
            }
            if (LstLinkOut != null)
            {
                DrawObject o;

                for (int i = 0; i < LstLinkOut.Count; i++)
                {
                    o = (DrawObject)LstLinkOut[i];
                    if (!o.Selected)
                    {
                        int handle = o.MovePossible(this);
                        if (handle == -1)
                        {
                            o.Move(deltaX, deltaY);
                        }
                        else o.MoveHandleTo(o.MoveHandle(handle, deltaX, deltaY), handle);
                    }
                }
            }
            if (LstChild != null)
            {
                DrawObject o;

                for (int i = 0; i < LstChild.Count; i++)
                {
                    o = (DrawObject)LstChild[i];
                    if (!o.Selected) o.Move(deltaX, deltaY);
                }
            }
        }

        public override void Dump()
        {
            base.Dump ();

            Trace.WriteLine("rectangle.X = " + rectangle.X.ToString(CultureInfo.InvariantCulture));
            Trace.WriteLine("rectangle.Y = " + rectangle.Y.ToString(CultureInfo.InvariantCulture));
            Trace.WriteLine("rectangle.Width = " + rectangle.Width.ToString(CultureInfo.InvariantCulture));
            Trace.WriteLine("rectangle.Height = " + rectangle.Height.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Normalize rectangle
        /// </summary>
        public override void Normalize()
        {
            rectangle = DrawRectangle.GetNormalizedRectangle(rectangle);
        }

        public override int XMin()
        {
            return Rectangle.Left;
            //return base.XMax();
        }

        public override int XMax()
        {
            return Rectangle.Right;
            //return base.XMax();
        }

        public override int YMin()
        {
            return Rectangle.Top;
            //return base.YMax();
        }

        public override int YMax()
        {
            return Rectangle.Bottom;
            //return base.YMax();
        }

        /// <summary>
        /// Save objevt to serialization stream
        /// </summary>
        /// <param name="info"></param>
        /// <param name="orderNumber"></param>
        public override void SaveToStream(System.Runtime.Serialization.SerializationInfo info, int orderNumber)
        {
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryRectangle, orderNumber),
                rectangle);

            base.SaveToStream (info, orderNumber);
        }

        /// <summary>
        /// LOad object from serialization stream
        /// </summary>
        /// <param name="info"></param>
        /// <param name="orderNumber"></param>
        public override void LoadFromStream(SerializationInfo info, int orderNumber)
        {
            rectangle = (Rectangle)info.GetValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryRectangle, orderNumber),
                typeof(Rectangle));

            base.LoadFromStream (info, orderNumber);
        }


        /*
        public override void saveObjecttoDB()
        {
            if (!F.oCnxBase.ExistGuid(this))
                F.oCnxBase.CreatObject(this); // Table Objet
            else
                F.oCnxBase.UpdateObject(this); // Update de la Table Objet
        }
        */

        public override void saveobjtoDB()
        {
            // Recherche dans la table Objet 'Rectangle'
            if (!F.oCnxBase.ExistGuid(this)) F.oCnxBase.CreatObject(this); // Table Objet
            else F.oCnxBase.UpdateObject(this); // Update de la Table Objet
        }

        public override void savetoDB()
        {
            if (!savetoDBFait())
            {
                if (LstValue[0].ToString() == "d0176102-c2ed-4447-b1a7-785d2e77fba2")
                    GuidkeyObjet = GuidkeyObjet;
                //string s = GetType().Name.Substring("Draw".Length);
                string s = GetTypeSimpleTable();
                if (s == "ServerPhy") s = "ServerSite";

                if (LstParent != null)
                {
                    for (int i = 0; i < LstParent.Count; i++) ((DrawObject)LstParent[i]).savetoDB();
                }
                if (LstLinkIn != null && (s == "NCard" || s == "Router" || s == "Location" || s == "SanCard"))
                {
                    for (int i = 0; i < LstLinkIn.Count; i++) ((DrawObject)LstLinkIn[i]).savetoDB();
                }
                // Recherche dans la table Objet 'Rectangle'
                if (!F.oCnxBase.ExistGuid(this))
                {

                    // Creation des liens avec le nouvel Objet
                    F.oCnxBase.CreatObject(this); // Table Objet
                    F.oCnxBase.CreatDansTypeVue(GuidkeyObjet, GetType().Name.Substring("Draw".Length)); // Table DansTypeVue
                }
                else
                    F.oCnxBase.UpdateObject(this); // Update de la Table Objet

                //Recherche G'Objet'
                if (!F.oCnxBase.ExistGuidG(this))
                {
                    // Creation des liens avec le nouveau G'Objet'
                    F.oCnxBase.CreatGObjectRect(this); //Table G'Objet'
                    F.oCnxBase.CreatDansVue(Guidkey, "G" + GetType().Name.Substring("Draw".Length));  // Table DansVue
                }

                savetoDBOK();
            }
        }

        

        public override XmlElement XmlCreatGObject(XmlDB xmlDB, XmlElement elParent)
        {
            string sType = GetTypeSimpleTable();
            string sTypeG = GetTypeSimpleGTable();

            XmlElement el = xmlDB.XmlCreatEl(elParent, sTypeG, "Guid" + sTypeG, Guidkey.ToString());
            XmlElement elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");

            xmlDB.XmlSetAttFromEl(elAtts, "Guid" + sTypeG, "s", Guidkey.ToString());
            xmlDB.XmlSetAttFromEl(elAtts, "Guid" + sType, "s", GuidkeyObjet.ToString());
            xmlDB.XmlSetAttFromEl(elAtts, "X", "i", Rectangle.X.ToString());
            xmlDB.XmlSetAttFromEl(elAtts, "Y", "i", Rectangle.Y.ToString());
            xmlDB.XmlSetAttFromEl(elAtts, "Width", "i", Rectangle.Width.ToString());
            xmlDB.XmlSetAttFromEl(elAtts, "Height", "i", Rectangle.Height.ToString());
            if (sTypeG == "GServerPhy")
            {
                xmlDB.XmlSetAttFromEl(elAtts, "Forme", "i", ((int)GetValueFromName("Forme")).ToString());
                xmlDB.XmlSetAttFromEl(elAtts, "thickColor", "i", ((int)GetValueFromName("thickColor")).ToString());
            }
            return el;
        }

        public override XmlElement savetoXml(XmlDB xmlDB, bool GObj)
        {
            string s = GetTypeSimpleTable();

            if (LstParent != null)
            {
                for (int i = 0; i < LstParent.Count; i++) ((DrawObject)LstParent[i]).savetoXml(xmlDB, GObj);
            }
            if (LstLinkIn != null && (s == "NCard" || s == "Router" || s == "Location" || s == "SanCard" ))
            {
                for (int i = 0; i < LstLinkIn.Count; i++) ((DrawObject)LstLinkIn[i]).savetoXml(xmlDB, GObj);
            }

            XmlElement elo = XmlCreatObject(xmlDB); // Table Objet
            if (elo != null) 
            {
                XmlCreatDansTypeVueEl(xmlDB, xmlDB.XmlGetFirstElFromParent(elo, "After"), s);
                if (GObj) XmlInsertGObject(xmlDB, xmlDB.XmlGetFirstElFromParent(elo, "After"), s);
                return elo;
            }
            return null;
        }

        public void CorrectionRatio()
        {
            rectangle.X = F.pTranslate.X + (int)((double)rectangle.X * F.xRatio);
            rectangle.Y = F.pTranslate.Y + (int)((double)rectangle.Y * F.yRatio);
            rectangle.Width = (int)((double)rectangle.Width * F.xRatio);
            rectangle.Height = (int)((double)rectangle.Height * F.yRatio);
        }


        #region Helper Functions

        public static Rectangle GetNormalizedRectangle(int x1, int y1, int x2, int y2)
        {
            if ( x2 < x1 )
            {
                int tmp = x2;
                x2 = x1;
                x1 = tmp;
            }

            if ( y2 < y1 )
            {
                int tmp = y2;
                y2 = y1;
                y1 = tmp;
            }

            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }

        public static Rectangle GetNormalizedRectangle(Point p1, Point p2)
        {
            return GetNormalizedRectangle(p1.X, p1.Y, p2.X, p2.Y);
        }

        public static Rectangle GetNormalizedRectangle(Rectangle r)
        {
            return GetNormalizedRectangle(r.X, r.Y, r.X + r.Width, r.Y + r.Height);
        }

        #endregion

    }
}
