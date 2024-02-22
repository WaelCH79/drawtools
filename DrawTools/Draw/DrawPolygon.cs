using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Xml;

namespace DrawTools
{
	/// <summary>
	/// Polygon graphic object
	/// </summary>
	public class DrawPolygon : DrawTools.DrawLine
	{
        public ArrayList pointArray;         // list of points
        private Cursor handleCursor;

        private const string entryLength = "Length";
        private const string entryPoint = "Point";


		public DrawPolygon()
		{
            pointArray = new ArrayList();

            LoadCursor();
            Initialize();
		}

        public DrawPolygon(int x1, int y1, int x2, int y2)
        {
            pointArray = new ArrayList();
            pointArray.Add(new Point(x1, y1));
            pointArray.Add(new Point(x2, y2));
            LstParent = null;
            LstChild = null;
            LstLinkIn = null;
            LstLinkOut = null;

            LoadCursor();
            Initialize();
        }

        public override void Draw(Graphics g)
        {
            int x1 = 0, y1 = 0;     // previous point
            int x2, y2;             // current point

            g.SmoothingMode = SmoothingMode.AntiAlias;

            Pen pen = new Pen(Color, PenWidth);

            IEnumerator enumerator = pointArray.GetEnumerator();

            if ( enumerator.MoveNext() )
            {
                x1 = ((Point)enumerator.Current).X;
                y1 = ((Point)enumerator.Current).Y;
            }

            while ( enumerator.MoveNext() )
            {
                x2 = ((Point)enumerator.Current).X;
                y2 = ((Point)enumerator.Current).Y;

                g.DrawLine(pen, x1, y1, x2, y2);

                x1 = x2;
                y1 = y2;
            }

            pen.Dispose();
        }

        public void AddPoint(Point point)
        {
            pointArray.Add(point);
        }

        public override int HandleCount
        {
            get
            {
                return pointArray.Count;
            }
        }

        /// <summary>
        /// Get handle point by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public override Point GetHandle(int handleNumber)
        {
            if ( handleNumber < 1 )
                handleNumber = 1;

            if ( handleNumber > pointArray.Count )
                handleNumber = pointArray.Count;

            return ((Point)pointArray[handleNumber - 1]);
        }

        public override Cursor GetHandleCursor(int handleNumber)
        {
            return handleCursor;
        }

        public override void MoveHandleTo(Point point, int handleNumber)
        {
            if ( handleNumber < 1 )
                handleNumber = 1;

            if ( handleNumber > pointArray.Count)
                handleNumber = pointArray.Count;

            pointArray[handleNumber-1] = point;

            Invalidate();
        }

        public override void Move(int deltaX, int deltaY)
        {
            int n = pointArray.Count;
            Point point;

            for ( int i = 0; i < n; i++ )
            {
                point = new Point( ((Point)pointArray[i]).X + deltaX, ((Point)pointArray[i]).Y + deltaY);

                pointArray[i] = point;
            }

            Invalidate();
        }

        public override void SaveToStream(System.Runtime.Serialization.SerializationInfo info, int orderNumber)
        {
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryLength, orderNumber),
                pointArray.Count);

            int i = 0;
            foreach ( Point p in pointArray )
            {
                info.AddValue(
                    String.Format(CultureInfo.InvariantCulture,
                    "{0}{1}-{2}",
                    entryPoint, orderNumber, i++),
                    p);

            }

            base.SaveToStream (info, orderNumber);  // ??
        }

        public override void LoadFromStream(System.Runtime.Serialization.SerializationInfo info, int orderNumber)
        {
            Point point;
            int n = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryLength, orderNumber));

            for ( int i = 0; i < n; i++ )
            {
                point = (Point)info.GetValue(
                    String.Format(CultureInfo.InvariantCulture,
                    "{0}{1}-{2}",
                    entryPoint, orderNumber, i),
                    typeof(Point));

                pointArray.Add(point);
            }

            base.LoadFromStream (info, orderNumber);
        }


        /// <summary>
        /// Create graphic object used for hit test
        /// </summary>
        protected override void CreateObjects()
        {
            if ( AreaPath != null )
                return;

            // Create closed path which contains all polygon vertexes
            AreaPath = new GraphicsPath();

            int x1 = 0, y1 = 0;     // previous point
            int x2, y2;             // current point

            IEnumerator enumerator = pointArray.GetEnumerator();

            if ( enumerator.MoveNext() )
            {
                x1 = ((Point)enumerator.Current).X;
                y1 = ((Point)enumerator.Current).Y;
            }

            while ( enumerator.MoveNext() )
            {
                x2 = ((Point)enumerator.Current).X;
                y2 = ((Point)enumerator.Current).Y;

                AreaPath.AddLine(x1, y1, x2, y2);

                x1 = x2;
                y1 = y2;
            }

            AreaPath.CloseFigure();

            // Create region from the path
            AreaRegion = new Region(AreaPath);
        }

        private void LoadCursor()
        {
            handleCursor = new Cursor(GetType(), "PolyHandle.cur");
        }

        public void InitPoint(DrawApplication daIn, DrawApplication daOut)
        {
            int difL = daOut.Rectangle.Left - daIn.Rectangle.Left, difR = daOut.Rectangle.Right - daIn.Rectangle.Right;
            int difT = daOut.Rectangle.Top - daIn.Rectangle.Top, difB = daOut.Rectangle.Bottom - daIn.Rectangle.Bottom;

            int xa1 = 0, ya1 = 0, xa2 = 0, ya2 = 0, deltaXa = 0;
            int xb1 = 0, yb1 = 0, xb2 = 0, yb2 = 0, deltaXb = 0;

            if (difR > 0 && difB < 0) //depart horizontalR, fin verticalT
            {
                xa1 = daIn.Rectangle.Right; ya1 = Math.Max(daIn.Rectangle.Top + daIn.Rectangle.Height / 2, daIn.Rectangle.Bottom + difB / 2);
                xa2 = Math.Max(daOut.Rectangle.Left + daOut.Rectangle.Width / 2, daIn.Rectangle.Left + difR / 2); ya2 = daOut.Rectangle.Bottom;
                deltaXa = Math.Abs(difR);
            }
            else if (difL < 0 && difB < 0) //depart horizontalL, fin verticalT
            {
                xa1 = daIn.Rectangle.Left; ya1 = Math.Max(daIn.Rectangle.Top + daIn.Rectangle.Height / 2, daIn.Rectangle.Bottom + difB / 2);
                xa2 = Math.Min(daOut.Rectangle.Left + daOut.Rectangle.Width / 2, daIn.Rectangle.Left + difL / 2); ya2 = daOut.Rectangle.Bottom;
                deltaXa = Math.Abs(difL);
            }
            else if (difL < 0 && difT > 0) //depart horizontalL, fin verticalB
            {
                xa1 = daIn.Rectangle.Left; ya1 = Math.Min(daIn.Rectangle.Top + daIn.Rectangle.Height / 2, daIn.Rectangle.Top + difT / 2);
                xa2 = Math.Min(daOut.Rectangle.Left + daOut.Rectangle.Width / 2, daIn.Rectangle.Left + difL / 2); ya2 = daOut.Rectangle.Top;
                deltaXa = Math.Abs(difL);
            }
            else if (difR > 0 && difT > 0) //depart horizontalR, fin verticalB
            {
                xa1 = daIn.Rectangle.Right; ya1 = Math.Min(daIn.Rectangle.Top + daIn.Rectangle.Height / 2, daIn.Rectangle.Top + difT / 2);
                xa2 = Math.Max(daOut.Rectangle.Left + daOut.Rectangle.Width / 2, daIn.Rectangle.Right + difR / 2); ya2 = daOut.Rectangle.Top;
                deltaXa = Math.Abs(difR);
            }


            if (difL > 0 && difT < 0) //depart verticalT, fin horizontalR
            {
                xb1 = Math.Min(daIn.Rectangle.Left + daIn.Rectangle.Width / 2, daIn.Rectangle.Left + difL / 2); yb1 = daIn.Rectangle.Top;
                xb2 = daOut.Rectangle.Left; yb2 = Math.Min(daOut.Rectangle.Top + daIn.Rectangle.Height / 2, daIn.Rectangle.Top + difT / 2);
                deltaXb = Math.Abs(difL);
            }
            else if (difR < 0 && difT < 0) //depart verticalT, fin horizontalL
            {
                xb1 = Math.Max(daIn.Rectangle.Left + daIn.Rectangle.Width / 2, daIn.Rectangle.Right + difR / 2); yb1 = daIn.Rectangle.Top;
                xb2 = daOut.Rectangle.Right; yb2 = Math.Min(daOut.Rectangle.Top + daIn.Rectangle.Height / 2, daIn.Rectangle.Top + difT / 2);
                deltaXb = Math.Abs(difR);
            }
            else if (difR < 0 && difB > 0) //depart verticalB, fin horizontalL
            {
                xb1 = Math.Max(daIn.Rectangle.Left + daIn.Rectangle.Width / 2, daIn.Rectangle.Right + difR / 2); yb1 = daIn.Rectangle.Bottom;
                xb2 = daOut.Rectangle.Right; yb2 = Math.Max(daOut.Rectangle.Top + daIn.Rectangle.Height / 2, daIn.Rectangle.Bottom + difB / 2);
                deltaXb = Math.Abs(difR);
            }
            else if (difL > 0 && difB > 0) //depart verticalB, fin horizontalR
            {
                xb1 = Math.Min(daIn.Rectangle.Left + daIn.Rectangle.Width / 2, daIn.Rectangle.Left + difL / 2); yb1 = daIn.Rectangle.Bottom;
                xb2 = daOut.Rectangle.Left; yb2 = Math.Max(daOut.Rectangle.Top + daIn.Rectangle.Height / 2, daIn.Rectangle.Bottom + difB / 2);
                deltaXb = Math.Abs(difL);
            }

            //if (deltaXa != 0 && deltaXb != 0)
            //    if (deltaXb > deltaXa) deltaXa=0; else deltaXb=0;
            if (deltaXb != 0)
            {
                pointArray.Add(new Point(xb1, yb1));
                pointArray.Add(new Point(xb1, yb2));
                pointArray.Add(new Point(xb2, yb2));
            }
            else if (deltaXa != 0)
            {
                pointArray.Add(new Point(xa1, ya1));
                pointArray.Add(new Point(xa2, ya1));
                pointArray.Add(new Point(xa2, ya2));
            }
            else if (difT == 0 && difB == 0)
            {
                if (daOut.Rectangle.Left > daIn.Rectangle.Right)
                {
                    pointArray.Add(new Point(daIn.Rectangle.Right, daIn.Rectangle.Top + Axe));
                    pointArray.Add(new Point((daIn.Rectangle.Right + daOut.Rectangle.Left) / 2, daIn.Rectangle.Top + Axe));
                    pointArray.Add(new Point((daIn.Rectangle.Right + daOut.Rectangle.Left) / 2, daOut.Rectangle.Bottom - Axe));
                    pointArray.Add(new Point(daOut.Rectangle.Left, daOut.Rectangle.Bottom - Axe));
                }
                else
                {
                    pointArray.Add(new Point(daIn.Rectangle.Left, daIn.Rectangle.Top + Axe));
                    pointArray.Add(new Point((daIn.Rectangle.Left + daOut.Rectangle.Right) / 2, daIn.Rectangle.Top + Axe));
                    pointArray.Add(new Point((daIn.Rectangle.Left + daOut.Rectangle.Right) / 2, daOut.Rectangle.Bottom - Axe));
                    pointArray.Add(new Point(daOut.Rectangle.Right, daOut.Rectangle.Bottom - Axe));
                }
            }
            else if (difL == 0 && difR == 0)
            {
                if (daOut.Rectangle.Bottom > daIn.Rectangle.Top)
                {
                    pointArray.Add(new Point(daIn.Rectangle.Left + Axe, daIn.Rectangle.Top));
                    pointArray.Add(new Point(daIn.Rectangle.Left + Axe, (daIn.Rectangle.Top + daOut.Rectangle.Bottom) / 2));
                    pointArray.Add(new Point(daOut.Rectangle.Right - Axe, (daIn.Rectangle.Top + daOut.Rectangle.Bottom) / 2));
                    pointArray.Add(new Point(daOut.Rectangle.Right - Axe, daOut.Rectangle.Bottom));
                }
                else
                {
                    pointArray.Add(new Point(daIn.Rectangle.Left + Axe, daIn.Rectangle.Bottom));
                    pointArray.Add(new Point(daIn.Rectangle.Left + Axe, (daIn.Rectangle.Bottom + daOut.Rectangle.Top) / 2));
                    pointArray.Add(new Point(daOut.Rectangle.Right - Axe, (daIn.Rectangle.Bottom + daOut.Rectangle.Top) / 2));
                    pointArray.Add(new Point(daOut.Rectangle.Right - Axe, daOut.Rectangle.Top));
                }
            }
            else // link impossible --> deplacer un objet
            {

            }
        }

        public override XmlElement XmlCreatGObject(XmlDB xmlDB, XmlElement elParent)
        {
            string sType = GetTypeSimpleTable();

            XmlElement el = xmlDB.XmlCreatEl(elParent, "G" + sType, "GuidG" + sType, Guidkey.ToString());
            XmlElement elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");
            xmlDB.XmlSetAttFromEl(elAtts, "GuidG" + sType, "s", Guidkey.ToString());
            xmlDB.XmlSetAttFromEl(elAtts, "Guid" + sType, "s", GuidkeyObjet.ToString());
            xmlDB.XmlSetAttFromEl(elAtts, "Pos", "i", ((int)GetValueFromName("Pos")).ToString());

            XmlCreatGPoint(xmlDB, xmlDB.XmlGetFirstElFromParent(el, "After"));

            return el;
        }

        public void XmlCreatGPoint(XmlDB xmlDB, XmlElement elParent)
        {

            for (int i = 0; i < pointArray.Count; i++)
            {
                XmlElement el = xmlDB.XmlCreatEl(elParent, "GPoint", "GuidGObjet,I", GuidkeyObjet.ToString());
                XmlElement elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");
                xmlDB.XmlSetAttFromEl(elAtts, "GuidGObjet", "s", Guidkey.ToString());
                xmlDB.XmlSetAttFromEl(elAtts, "I", "i", i.ToString());
                xmlDB.XmlSetAttFromEl(elAtts, "X", "i", ((Point)pointArray[i]).X.ToString());
                xmlDB.XmlSetAttFromEl(elAtts, "Y", "i", ((Point)pointArray[i]).Y.ToString());
            }
        }

        public override XmlElement savetoXml(XmlDB xmlDB, bool GObj)
        {
            string s = GetTypeSimpleTable();

            if (LstLinkIn.Count > 0) ((DrawObject)LstLinkIn[0]).savetoXml(xmlDB, GObj);
            if (LstLinkOut.Count > 0) ((DrawObject)LstLinkOut[0]).savetoXml(xmlDB, GObj);

            XmlElement elo = XmlCreatObject(xmlDB); // Table Objet
            if (elo != null)
            {
                XmlCreatDansTypeVueEl(xmlDB, xmlDB.XmlGetFirstElFromParent(elo, "After"), s);
                if (GObj) XmlInsertGObject(xmlDB, xmlDB.XmlGetFirstElFromParent(elo, "After"), s);
                return elo;
            }
            return null;
        }
    }
}
