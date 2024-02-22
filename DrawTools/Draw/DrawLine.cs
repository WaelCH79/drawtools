using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
//using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Line graphic object
	/// </summary>
	public class DrawLine : DrawTools.DrawObject
	{
        private Point startPoint;
        private Point endPoint;
        //public ArrayList pointArray;         // list of points

        private const string entryStart = "Start";
        private const string entryEnd = "End";

        /// <summary>
        ///  Graphic objects for hit test
        /// </summary>
        private GraphicsPath areaPath = null;
        private Pen areaPen = null;
        private Region areaRegion = null;


		public DrawLine()
		{
            startPoint.X = 0;
            startPoint.Y = 0;
            endPoint.X = 1;
            endPoint.Y = 1;
            LstParent = null;
            LstChild = null;
            LstLinkIn = null;
            LstLinkOut = null;

            Initialize();
		}

        public DrawLine(int x1, int y1, int x2, int y2)
        {
            startPoint.X = x1;
            startPoint.Y = y1;
            endPoint.X = x2;
            endPoint.Y = y2;
            LstParent = null;
            LstChild = null;
            LstLinkIn = null;
            LstLinkOut = null;

            Guidkey = Guid.NewGuid();

            Initialize();
        }


        public override void Draw(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Pen pen = new Pen(Color, PenWidth);

            g.DrawLine(pen, startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);

            pen.Dispose();
        }

        public override int HandleCount
        {
            get
            {
                return 2;
            }
        }
        

        /// <summary>
        /// Get handle point by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public override Point GetHandle(int handleNumber)
        {
            if ( handleNumber == 1 )
                return startPoint;
            else
                return endPoint;
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

        public override bool PointInObject(Point point)
        {
            CreateObjects();

            return AreaRegion.IsVisible(point);
        }

        public override bool AttachPointInObject(Point point)
        {
            return false;
        }

        public override bool IntersectsWith(Rectangle rectangle)
        {
            CreateObjects();

            return AreaRegion.IsVisible(rectangle);
        }

        public override Cursor GetHandleCursor(int handleNumber)
        {
            switch ( handleNumber )
            {
                case 1:
                case 2:
                    return Cursors.SizeAll;
                default:
                    return Cursors.Default;
            }
        }

        public override void MoveHandleTo(Point point, int handleNumber)
        {
            if ( handleNumber == 1 )
                startPoint = point;
            else
                endPoint = point;

            Invalidate();
        }

        public override void Move(int deltaX, int deltaY)
        {
            startPoint.X += deltaX;
            startPoint.Y += deltaY;

            endPoint.X += deltaX;
            endPoint.Y += deltaY;

            Invalidate();
        }

        public virtual void AddNCard(ArrayList LstNCard, object oLink, DrawRectangle oRec, string NomNCard, string sLink)
        {
            string[] aLink = ((string)oLink).Split(new Char[] { '(', ')' });
            for (int i = 1; i < aLink.Length; i += 2)
            {
                if (F.oCnxBase.CBRecherche("SELECT DISTINCT NCard.GuidNCard From NCard, ServerPhy, " + sLink + "Link WHERE " + sLink + "Link.GuidServerPhy=ServerPhy.GuidServerPhy AND ServerPhy.GuidServerPhy=NCard.GuidHote AND ServerPhy.GuidLocation='" + (string)oRec.GetValueFromName("GuidLocation") + "' AND NomNCard='" + NomNCard + "' AND " + sLink + "Link.Guid" + sLink + "='" + aLink[i].Trim() + "'"))
                    while (F.oCnxBase.Reader.Read())
                        if(LstNCard.IndexOf(F.oCnxBase.Reader.GetString(0))==-1) LstNCard.Add(F.oCnxBase.Reader.GetString(0));
                F.oCnxBase.CBReaderClose();
            }
        }

        public virtual void CreatNCardLink(string Sens)
        {
            object oNCard;

            oNCard = GetValueFromName("Eth" + Sens);
            if (oNCard != null && (string)oNCard != "")
            {
                ArrayList LstNCard = new ArrayList();
                string[] aNCard = ((string)oNCard).Split(new Char[] { '(', ')' });
                string NomNCard = aNCard[0].Trim();
                DrawRectangle oRec = (DrawRectangle)F.drawArea.GraphicsList[F.drawArea.GraphicsList.FindObjet(0, (string)GetValueFromName("GuidServerSite" + Sens))];
                object oLink = null;
                oLink = oRec.GetValueFromName("GuidServer");
                if (oLink != null && (string)oLink != "") AddNCard(LstNCard, oLink, oRec, NomNCard, "Server");
                else
                {
                    oLink = oRec.GetValueFromName("GuidApplication");
                    if (oLink != null && (string)oLink != "") AddNCard(LstNCard, oLink, oRec, NomNCard, "Application");
                    else
                    {
                        oLink = oRec.GetValueFromName("GuidAppUser");
                        if (oLink != null && (string)oLink != "") AddNCard(LstNCard, oLink, oRec, NomNCard,  "AppUser" );
                    }
                }
                
                for (int i = 0; i < LstNCard.Count; i++)
                {
                    F.oCnxBase.CBWrite("INSERT INTO NCardInterLink" + Sens + " (GuidNCard, GuidInterLink) VALUES ('" + (string)LstNCard[i] + "','" + GuidkeyObjet + "')");
                }
            }
        }

        public override void SaveToStream(System.Runtime.Serialization.SerializationInfo info, int orderNumber)
        {
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryStart, orderNumber),
                startPoint);

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryEnd, orderNumber),
                endPoint);

            base.SaveToStream (info, orderNumber);
        }

        public override void LoadFromStream(SerializationInfo info, int orderNumber)
        {
            startPoint = (Point)info.GetValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryStart, orderNumber),
                typeof(Point));

            endPoint = (Point)info.GetValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryEnd, orderNumber),
                typeof(Point));

            base.LoadFromStream (info, orderNumber);
        }


        /// <summary>
        /// Invalidate object.
        /// When object is invalidated, path used for hit test
        /// is released and should be created again.
        /// </summary>
        protected void Invalidate()
        {
            if ( AreaPath != null )
            {
                AreaPath.Dispose();
                AreaPath = null;
            }

            if ( AreaPen != null )
            {
                AreaPen.Dispose();
                AreaPen = null;
            }

            if ( AreaRegion != null )
            {
                AreaRegion.Dispose();
                AreaRegion = null;
            }
        }

        /// <summary>
        /// Create graphic objects used from hit test.
        /// </summary>
        protected virtual void CreateObjects()
        {
            if ( AreaPath != null )
                return;

            // Create path which contains wide line
            // for easy mouse selection
            AreaPath = new GraphicsPath();
            AreaPen = new Pen(Color.Black, 7);
            AreaPath.AddLine(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
            AreaPath.Widen(AreaPen);

            // Create region from the path
            AreaRegion = new Region(AreaPath);
        }

        protected GraphicsPath AreaPath
        {
            get
            {
                return areaPath;
            }
            set
            {
                areaPath = value;
            }
        }

        protected Pen AreaPen
        {
            get
            {
                return areaPen;
            }
            set
            {
                areaPen = value;
            }
        }

        protected Region AreaRegion
        {
            get
            {
                return areaRegion;
            }
            set
            {
                areaRegion = value;
            }
        }
	}
}
