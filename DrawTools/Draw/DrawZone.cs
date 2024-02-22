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
	public class DrawZone : DrawTools.DrawPolygon
	{
        static private Color Couleur = Color.Black;
        static private int LineWidth = 2;
        
        //public ArrayList pointArray;         // list of points
        private Cursor handleCursor;

        private const string entryLength = "Length";
        private const string entryPoint = "Point";

        //private string[] sProprietes;

        public DrawZone()
		{
            pointArray = new ArrayList();

            LoadCursor();
            Initialize();
		}

        public DrawZone(Form1 of, int x1, int y1, int x2, int y2, int count)
        {
            F = of;
            OkMove = true;
            Align = true;
            string sTypeVue = F.tbTypeVue.Text; // (string)F.cbTypeVue.SelectedItem;

            pointArray = new ArrayList();
            pointArray.Add(new Point(x1, y1));
            pointArray.Add(new Point(x2, y1));
            pointArray.Add(new Point(x2, y2));
            LstParent = null;
            LstChild = null;
            LstLinkIn = new ArrayList();
            LstLinkOut = new ArrayList();
            LstValue = new ArrayList();
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
            Texte = "Zone" + count;
           
            LoadCursor();
            InitProp();
            Initialize();
        }

        public DrawZone(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = true;
            Align = true;
            pointArray = new ArrayList();
            AddPoint(lstValG);
            LstParent = null;
            LstChild = null;
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
            o = lstValG[GetIndexFromGName("Pos")];
            if (o != null)
                SetValueFromName("Pos", o);              

            Initialize();
        }

        
        public void AddPoint(ArrayList lstValG)
        {
            string sType = "G" + this.GetType().Name.Substring("Draw".Length);

            Table t;
            int n = F.oCnxBase.ConfDB.FindTable(sType);
            if (n > -1)
            {
                t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                int x = t.FindField(t.LstField, "X");
                int y = t.FindField(t.LstField, "Y");

                if (x > -1 && y > -1 && ((int)lstValG[x]) != 0) pointArray.Add(new Point((int)lstValG[x], (int)lstValG[y]));
            }
        }

        /// <summary>
        /// Save Object to the Data Base
        /// </summary>
        public override void savetoDB()
        {

            if (!savetoDBFait())
            {
                string sTypeVue = F.tbTypeVue.Text; // (string)F.cbTypeVue.SelectedItem;

                if (LstLinkIn.Count > 0) ((DrawObject)LstLinkIn[0]).savetoDB();
                if (LstLinkOut.Count > 0) ((DrawObject)LstLinkOut[0]).savetoDB();

                // Recherche dans la table Link
                if (!F.oCnxBase.ExistGuid(this, GetType().Name.Substring("Draw".Length)))
                {
                    // Creation des liens avec le nouveau Link
                    F.oCnxBase.CreatObject(this); // Table Link
                    F.oCnxBase.CreatDansTypeVue(GuidkeyObjet, GetType().Name.Substring("Draw".Length)); // Table DansTypeVue
                }
                else
                    F.oCnxBase.UpdateObject(this); // Update de la Table Link

                //F.oCnxBase.CreatTechLinkApp(this);
                if (!F.oCnxBase.ExistGuidG(this))
                {
                    F.oCnxBase.CreatGZone(this); // Table GZone + GPoint
                    F.oCnxBase.CreatDansVue(Guidkey, "G" + GetType().Name.Substring("Draw".Length));  // Table DansVue
                }
                savetoDBOK();
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
                /*
                if (GObj)
                {
                    //GObjet
                    F.oCnxBase.CreatGLinkXml(elo, this);
                    XmlCreatDansVueEl(elo, s);
                }
                */
            }
            return null;
        }

        public override void Draw(Graphics g)
        {
            int x1 = 0, y1 = 0;     // previous point
            int x2, y2;             // current point
            string sType = this.GetType().Name.Substring("Draw".Length);
            int iPos = 0;
            int DeltaXC = 0, DeltaX = 0, iSegment = 0;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            
            Pen pen = new Pen(Couleur, LineWidth);

            IEnumerator enumerator = pointArray.GetEnumerator();
            
            int n = F.oCnxBase.ConfDB.FindTable(sType);
            if (n > -1)
            {
                Table t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                int m = t.FindField(t.LstField, "Pos");
                if (m > -1) iPos = Convert.ToInt32((int)LstValue[m]);
            }
            n = 0;
            if (enumerator.MoveNext()) x1 = ((Point)enumerator.Current).X;
            while (enumerator.MoveNext())
            {
                x2 = ((Point)enumerator.Current).X;
                DeltaXC = Math.Abs(x2 - x1);
                if (DeltaX < DeltaXC) { DeltaX = DeltaXC; iSegment = n; }
                n++; x1 = x2;
            }

            enumerator.Reset();
            n = 0;
            if (enumerator.MoveNext())
            {
                x1 = ((Point)enumerator.Current).X;
                y1 = ((Point)enumerator.Current).Y;
            }

            while (enumerator.MoveNext())
            {
                x2 = ((Point)enumerator.Current).X;
                y2 = ((Point)enumerator.Current).Y;

                g.DrawLine(pen, x1, y1, x2, y2);
                if (y1 == y2 && n == iSegment)
                {
                    switch (iPos)
                    {
                        case 0:
                            break;
                        case 1:
                            DrawGrpTxt(g, 2, 0, x1 + 4 * Axe, y1 - 2 * Axe - HeightFont[1], 0, Color.Black, Color.Transparent);
                            break;
                        case 2:
                            DrawGrpTxt(g, 2, 0, x2 + 4 * Axe, y1 - 2 * Axe - HeightFont[1], 0, Color.Black, Color.Transparent);
                            break;
                    }
                }
                n++; x1 = x2; y1 = y2;
            }

            double deltax = (((Point)pointArray[pointArray.Count - 1]).X - ((Point)pointArray[pointArray.Count - 2]).X);
            int signex = 1; if (deltax < 0) signex = -1;
            double deltay = (((Point)pointArray[pointArray.Count - 1]).Y - ((Point)pointArray[pointArray.Count - 2]).Y);
            int signey = 1; if (deltay < 0) signey = -1;
            double angle = 1.58; if (deltax != 0) angle = Math.Atan(Math.Abs(deltay) / Math.Abs(deltax));
            double varangle = 0.4;
            int longfleche = 10;
            int xa = (int)(((Point)pointArray[pointArray.Count - 1]).X - Math.Cos(angle - varangle) * longfleche * signex);
            int xb = (int)(((Point)pointArray[pointArray.Count - 1]).X - Math.Cos(angle + varangle) * longfleche * signex);
            int ya = (int)(((Point)pointArray[pointArray.Count - 1]).Y - Math.Sin(angle - varangle) * longfleche * signey);
            int yb = (int)(((Point)pointArray[pointArray.Count - 1]).Y - Math.Sin(angle + varangle) * longfleche * signey);

            g.DrawLine(pen, xa, ya, ((Point)pointArray[pointArray.Count - 1]).X, ((Point)pointArray[pointArray.Count - 1]).Y);
            g.DrawLine(pen, xb, yb, ((Point)pointArray[pointArray.Count - 1]).X, ((Point)pointArray[pointArray.Count - 1]).Y);
            g.DrawLine(pen, xa, ya, xb, yb);

            pen.Dispose();
        }

        public override void AttachLink(DrawObject o, TypeAttach Attach)
        {
            string oIn = "GuidServerPhy", oOut = "GuidLun";
            
            switch (Attach)
            {
                case TypeAttach.Entree:
                    SetValueFromName(oIn, o.GuidkeyObjet.ToString());
                    break;
                case TypeAttach.Sortie:
                    SetValueFromName(oOut, o.GuidkeyObjet.ToString());
                    break;
            }
            base.AttachLink(o, Attach);
        }

        public new void AddPoint(Point point)
        {
            pointArray.Add(point);
        }

        public override int HandleCount
        {
            get
            {
                //return pointArray.Count;
                return (pointArray.Count - 1) * 4 + 1;
            }
        }

        public Point EndHandle(Point point)
        {
            bool AttachObjectOut = false;
            Point pointTemp;

            for (int i = 0; i < F.drawArea.GraphicsList.Count; i++)
            {
                if (F.drawArea.GraphicsList[i].AttachPointInObject(point))
                {
                    AttachObjectOut = true;
                    DrawRectangle dr = (DrawRectangle)F.drawArea.GraphicsList[i];
                    point = dr.GetPointObject(point);
                    if (dr.LePointEstSitue(point) > 2) //Left ou Right
                    {
                        if (LstLinkIn.Count != 0 && ((DrawRectangle)LstLinkIn[0]).LePointEstSitue(((Point)pointArray[0])) < 3)
                        {
                            if (LstLinkOut.Count == 0 || !F.drawArea.GraphicsList[i].AttachPointInObject((Point)pointArray[pointArray.Count - 1]) || dr.LePointEstSitue((Point)pointArray[pointArray.Count - 1]) < 3)
                            {
                                pointTemp = new Point(((Point)pointArray[0]).X, ((Point)pointArray[0]).Y);
                                pointArray.Clear();
                                pointArray.Add(new Point(pointTemp.X, pointTemp.Y));
                                pointArray.Add(new Point(pointTemp.X, point.Y));
                                pointArray.Add(new Point(point.X, point.Y));
                            }
                        }
                        else if (LstLinkIn.Count != 0 && ((DrawRectangle)LstLinkIn[0]) != dr && ((DrawRectangle)LstLinkIn[0]).LePointEstSitue(((Point)pointArray[0])) > 2)
                        {
                            if (LstLinkOut.Count == 0 || !F.drawArea.GraphicsList[i].AttachPointInObject((Point)pointArray[pointArray.Count - 1]) || dr.LePointEstSitue((Point)pointArray[pointArray.Count - 1]) < 3)
                            {
                                pointTemp = new Point(((Point)pointArray[0]).X, ((Point)pointArray[0]).Y);
                                pointArray.Clear();
                                if (pointTemp.Y == point.Y)
                                {
                                    pointArray.Add(new Point(pointTemp.X, pointTemp.Y));
                                    pointArray.Add(new Point(point.X, point.Y));
                                }
                                else
                                {
                                    pointArray.Add(new Point(pointTemp.X, pointTemp.Y));
                                    pointArray.Add(new Point((pointTemp.X + point.X) / 2, pointTemp.Y));
                                    pointArray.Add(new Point((pointTemp.X + point.X) / 2, point.Y));
                                    pointArray.Add(new Point(point.X, point.Y));
                                }
                            }
                        }
                    }
                    else if (dr.LePointEstSitue(point) < 3) //Top ou Bottom
                    {
                        if (LstLinkIn.Count != 0 && ((DrawRectangle)LstLinkIn[0]).LePointEstSitue(((Point)pointArray[0])) > 2)
                        {
                            if (LstLinkOut.Count == 0 || !F.drawArea.GraphicsList[i].AttachPointInObject((Point)pointArray[pointArray.Count - 1]) || dr.LePointEstSitue((Point)pointArray[pointArray.Count - 1]) > 2)
                            {
                                pointTemp = new Point(((Point)pointArray[0]).X, ((Point)pointArray[0]).Y);
                                pointArray.Clear();
                                pointArray.Add(new Point(pointTemp.X, pointTemp.Y));
                                pointArray.Add(new Point(point.X, pointTemp.Y));
                                pointArray.Add(new Point(point.X, point.Y));
                            }
                        }
                        else if (LstLinkIn.Count != 0 && ((DrawRectangle)LstLinkIn[0]) != dr && ((DrawRectangle)LstLinkIn[0]).LePointEstSitue(((Point)pointArray[0])) < 3)
                        {
                            if (LstLinkOut.Count == 0 || !F.drawArea.GraphicsList[i].AttachPointInObject((Point)pointArray[pointArray.Count - 1]) || dr.LePointEstSitue((Point)pointArray[pointArray.Count - 1]) > 2)
                            {
                                pointTemp = new Point(((Point)pointArray[0]).X, ((Point)pointArray[0]).Y);
                                pointArray.Clear();
                                if (pointTemp.X == point.X)
                                {
                                    pointArray.Add(new Point(pointTemp.X, pointTemp.Y));
                                    pointArray.Add(new Point(point.X, point.Y));
                                }
                                else
                                {
                                    pointArray.Add(new Point(pointTemp.X, pointTemp.Y));
                                    pointArray.Add(new Point(pointTemp.X, (pointTemp.Y + point.Y) / 2));
                                    pointArray.Add(new Point(point.X, (pointTemp.Y + point.Y) / 2));
                                    pointArray.Add(new Point(point.X, point.Y));
                                }
                            }
                        }
                        else AttachObjectOut = false;
                    }
                    break;
                }
            }
            if (!AttachObjectOut && pointArray.Count != 2)
            {
                pointTemp = new Point(((Point)pointArray[0]).X, ((Point)pointArray[0]).Y);
                pointArray.Clear();
                pointArray.Add(new Point(pointTemp.X, pointTemp.Y));
                if (LstLinkIn.Count != 0 && ((DrawRectangle)LstLinkIn[0]).LePointEstSitue(((Point)pointArray[0])) < 3)
                    pointArray.Add(new Point(pointTemp.X, point.Y));
                else if (LstLinkIn.Count != 0 && ((DrawRectangle)LstLinkIn[0]).LePointEstSitue(((Point)pointArray[0])) > 2)
                    pointArray.Add(new Point(point.X, pointTemp.Y));
                else pointArray.Add(new Point(point.X, point.Y));
            }

            ToolPointer tp = (ToolPointer)F.drawArea.tools[(int)DrawArea.DrawToolType.Pointer];
            tp.RESIZEOBJECTHANDLE = HandleEvent(point, (pointArray.Count - 1) * 4 + 1);

            return point;
        }

        /// <summary>
        /// Get handle point by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public override Point GetHandle(int handleNumber)
        {
            int x = 0, y = 0;
            int handle;
            int idxpoint = Math.DivRem(handleNumber - 1, 4, out handle);
            if (idxpoint == pointArray.Count - 1)
            {
                x = ((Point)pointArray[idxpoint]).X;
                y = ((Point)pointArray[idxpoint]).Y;
            }
            else
            {
                x = ((Point)pointArray[idxpoint]).X + (((Point)pointArray[idxpoint + 1]).X - ((Point)pointArray[idxpoint]).X) * handle / 4;
                y = ((Point)pointArray[idxpoint]).Y + (((Point)pointArray[idxpoint + 1]).Y - ((Point)pointArray[idxpoint]).Y) * handle / 4;
            }
            return new Point(x, y);
        }

        public override Cursor GetHandleCursor(int handleNumber)
        {
            return handleCursor;
        }

        public override void MoveHandleTo(Point point, int handleNumber)
        {
            Point pointTemp;
            int handle;
            int idxpoint = Math.DivRem(handleNumber - 1, 4, out handle);
            switch (handle)
            {
                case 0:
                    if (idxpoint == 0)
                    {
                        if (pointArray.Count != 2)
                        {
                            if (((Point)pointArray[0]).X == ((Point)pointArray[1]).X)
                            {
                                pointTemp = new Point(point.X, ((Point)pointArray[1]).Y);
                                pointArray[1] = pointTemp;
                            }
                            else
                            {
                                pointTemp = new Point(((Point)pointArray[1]).X, point.Y);
                                pointArray[1] = pointTemp;
                            }
                            pointArray[0] = point;
                        }
                        else if (LstLinkOut.Count != 0)
                        {
                            DrawRectangle dr = (DrawRectangle)LstLinkOut[0];
                            if (dr.LePointEstSitue((Point)pointArray[1]) > 2)
                            {
                                pointTemp = new Point(point.X, ((Point)pointArray[0]).Y);
                                pointArray[0] = pointTemp;
                            }
                            else
                            {
                                pointTemp = new Point(((Point)pointArray[0]).X, point.Y);
                                pointArray[0] = pointTemp;
                            }
                        }
                        else pointArray[0] = point;

                    }
                    else if (idxpoint == pointArray.Count - 1)
                    {
                        if (Selected && F.drawArea.GraphicsList.SelectionCount == 1) point = EndHandle(point);
                        if (pointArray.Count != 2)
                        {
                            if (((Point)pointArray[pointArray.Count - 1]).X == ((Point)pointArray[pointArray.Count - 2]).X)
                            {
                                pointTemp = new Point(point.X, ((Point)pointArray[pointArray.Count - 2]).Y);
                                pointArray[pointArray.Count - 2] = pointTemp;
                            }
                            else
                            {
                                pointTemp = new Point(((Point)pointArray[pointArray.Count - 2]).X, point.Y);
                                pointArray[pointArray.Count - 2] = pointTemp;
                            }
                            pointArray[pointArray.Count - 1] = point;
                        }
                        else if (LstLinkIn.Count != 0)
                        {
                            DrawRectangle dr = (DrawRectangle)LstLinkIn[0];
                            if (dr.LePointEstSitue((Point)pointArray[0]) > 2)
                            {
                                pointTemp = new Point(point.X, ((Point)pointArray[1]).Y);
                                pointArray[1] = pointTemp;
                            }
                            else
                            {
                                pointTemp = new Point(((Point)pointArray[1]).X, point.Y);
                                pointArray[1] = pointTemp;
                            }

                        }
                        else pointArray[pointArray.Count - 1] = point;
                    }
                    break;
                case 1:
                    if (Math.Abs(point.X - ((Point)pointArray[idxpoint + 1]).X) > 5 && Math.Abs(point.Y - ((Point)pointArray[idxpoint + 1]).Y) > 5)
                    {
                        if (((Point)pointArray[idxpoint]).X == ((Point)pointArray[idxpoint + 1]).X)
                        {
                            pointArray.Insert(idxpoint + 1, new Point(((Point)pointArray[idxpoint + 1]).X, point.Y));
                            pointArray.Insert(idxpoint + 1, new Point(((Point)pointArray[idxpoint + 1]).X, point.Y));
                        }
                        else
                        {
                            pointArray.Insert(idxpoint + 1, new Point(point.X, ((Point)pointArray[idxpoint + 1]).Y));
                            pointArray.Insert(idxpoint + 1, new Point(point.X, ((Point)pointArray[idxpoint + 1]).Y));
                        }
                    }

                    break;
                case 2:
                    if (pointArray.Count != 2 || LstLinkIn.Count != 0)
                    {
                        if (((Point)pointArray[idxpoint]).X == ((Point)pointArray[idxpoint + 1]).X)
                        {
                            pointTemp = new Point(point.X, ((Point)pointArray[idxpoint]).Y);
                            pointArray[idxpoint] = pointTemp;
                            pointTemp = new Point(point.X, ((Point)pointArray[idxpoint + 1]).Y);
                            pointArray[idxpoint + 1] = pointTemp;
                        }
                        else
                        {
                            pointTemp = new Point(((Point)pointArray[idxpoint]).X, point.Y);
                            pointArray[idxpoint] = pointTemp;
                            pointTemp = new Point(((Point)pointArray[idxpoint + 1]).X, point.Y);
                            pointArray[idxpoint + 1] = pointTemp;
                        }
                    }
                    else
                    {
                        double deltaX = point.X - (double)(((Point)pointArray[1]).X + ((Point)pointArray[0]).X) / 2;
                        double deltaY = point.Y - (double)(((Point)pointArray[1]).Y + ((Point)pointArray[0]).Y) / 2;
                        pointTemp = new Point(((Point)pointArray[0]).X + (int)deltaX, ((Point)pointArray[0]).Y + (int)deltaY);
                        pointArray[0] = pointTemp;
                        pointTemp = new Point(((Point)pointArray[1]).X + (int)deltaX, ((Point)pointArray[1]).Y + (int)deltaY);
                        pointArray[1] = pointTemp;

                    }
                    break;
                case 3:
                    break;
            }

            Invalidate();
        }

        /// <summary>
        /// Move un point de l'object
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public override Point MoveHandle(int handleNumber,int deltaX, int deltaY)
        {
            if (handleNumber == 1)
            {
                return new Point(((Point)pointArray[0]).X + deltaX, ((Point)pointArray[0]).Y + deltaY);
            }
            else if (handleNumber == (pointArray.Count - 1) * 4 + 1)
            {
                return new Point(((Point)pointArray[(handleNumber - 1) / 4]).X + deltaX, ((Point)pointArray[(handleNumber - 1) / 4]).Y + deltaY);
            }
            return new Point(0, 0);  
        }

        public void Add3Points(Point ptDepart)
        {
            pointArray.Clear();
            pointArray.Add(new Point(ptDepart.X, ptDepart.Y));
            pointArray.Add(new Point(ptDepart.X + 1, ptDepart.Y));
            pointArray.Add(new Point(ptDepart.X + 1, ptDepart.Y + 1));

        }

        public override void Move(int deltaX, int deltaY)
        {
            int n = pointArray.Count;
            Point point;
            
            if(LstLinkIn.Count==0 && LstLinkOut.Count==0)
            {
                for ( int i = 0; i < n; i++ )
                {
                    point = new Point( ((Point)pointArray[i]).X + deltaX, ((Point)pointArray[i]).Y + deltaY);

                    pointArray[i] = point;
                }
            }else if(LstLinkIn.Count>0 && LstLinkOut.Count>0)
            {
                for ( int i = 0; i < n; i++ )
                {
                    point = new Point( ((Point)pointArray[i]).X + deltaX, ((Point)pointArray[i]).Y + deltaY);

                    pointArray[i] = point;
                }

                DrawObject o;
                for (int j = 0; j < LstLinkIn.Count; j++)
                {
                    o = (DrawObject)LstLinkIn[j];
                    if (!o.Selected) o.Move(deltaX, deltaY);
                }
                for (int j = 0; j < LstLinkOut.Count; j++)
                {
                    o = (DrawObject)LstLinkOut[j];
                    if (!o.Selected) o.Move(deltaX, deltaY);
                }
            } else if(LstLinkIn.Count>0) MoveHandleTo(MoveHandle(5, deltaX, deltaY), 5);
                else MoveHandleTo(MoveHandle(1, deltaX, deltaY), 1);
            
            Invalidate();
        }
        
        
        /// <summary>
        /// Vérifie si l'objet à déplacer n'est attaché avec d'autres objets
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override int MovePossible(DrawObject o)
        {
            if (this.LstLinkOut.Count > 0)
            {
                if (this.LstLinkOut.IndexOf(o) != -1) return (pointArray.Count - 1) * 4 + 1;
            }
            if (this.LstLinkIn.Count > 0)
            {
                if (this.LstLinkIn.IndexOf(o) != -1) return 1;
            }

            return -1;
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
            AreaPen = new Pen(Color.Black, 7);

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

            AreaPath.Widen(AreaPen);

            // Create region from the path
            AreaRegion = new Region(AreaPath);
        }

        private void LoadCursor()
        {
            handleCursor = new Cursor(GetType(), "PolyHandle.cur");
        }

        public override void CWInsert(ControlDoc cw, char cTypeVue)
        {

            if (cTypeVue == '8' || cTypeVue == '7')
            {
                string sType = GetType().Name.Substring("Draw".Length);

                if (cw.Exist(sType + cTypeVue) > -1)
                    cw.InsertRowFromId(sType + cTypeVue, this);

            }
        }
	}
}
