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
    public class DrawInfLink : DrawTools.DrawPolygon
	{
        //public ArrayList pointArray;         // list of points : defined in Drawlin Class
        private Cursor handleCursor;

        private const string entryLength = "Length";
        private const string entryPoint = "Point";

        //private string[] sProprietes;

        public DrawInfLink()
		{
            pointArray = new ArrayList();

            LoadCursor();
            Initialize();
		}

        public DrawInfLink(Form1 of, int x1, int y1, int x2, int y2, int count)
        {
            F = of;
            OkMove = true;
            Align = false;
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
            Texte = "Link" + count;
           
            LoadCursor();
            InitProp();

            //service par défaut
            string sguid = "44609a5c-2caa-4c16-adb3-c8becfd2ca17";
            string sVal = "default" + "   (" + sguid + ")";
            SetValueFromName("NomGroupService", (object)sVal);
            SetValueFromName("GuidGroupService", (object)sguid);

            Initialize();
        }

        public DrawInfLink(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = true;
            Align = false;
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
            o = GetValueFromName("GuidGroupService");
            if (o != null)
            {
                //object o1 = GetValueFromName("NomService");
                //string s = (string)o1 + "   (" + (string)o + ")";
                //SetValueFromName("NomService", (object) s);
            }               

            Initialize();
        }

        public new void InitPoint(DrawApplication daIn, DrawApplication daOut)
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
            else // link impossible --> deplacer un objet
            {

            }
        }

        public DrawInfLink(Form1 of, ArrayList LstVal, DrawApplication daIn, DrawApplication daOut, int count)
        {
            F = of;
            OkMove = true;
            Align = false;
            pointArray = new ArrayList();
            InitPoint(daIn, daOut);
            LstParent = null;
            LstChild = null;
            LstLinkIn = new ArrayList();
            LstLinkOut = new ArrayList();
            LstValue = LstVal;
            Guidkey = Guid.NewGuid();
            Texte = "Link" + count;

            GuidkeyObjet = new Guid((string)GetValueFromLib("Guid"));

            LoadCursor();
            InitProp();
            Initialize();
            AttachLink(daIn, TypeAttach.Entree); daIn.AttachLink(this, TypeAttach.Sortie);
            AttachLink(daOut, TypeAttach.Sortie); daOut.AttachLink(this, TypeAttach.Entree);
        }

        public void AddPoint(ArrayList lstValG)
        {
            string sType = GetTypeSimpleGTable();

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

        public void Add3Points(Point ptDepart)
        {
            pointArray.Clear();
            pointArray.Add(new Point(ptDepart.X, ptDepart.Y));
            pointArray.Add(new Point(ptDepart.X + 1, ptDepart.Y));
            pointArray.Add(new Point(ptDepart.X + 1, ptDepart.Y + 1));

        }

        public DrawInfLink(Form1 of,string sGuidLink, string sTexte, ArrayList ptArray)
        {
            F = of;

            pointArray = new ArrayList();
            for (int i = 0; i < ptArray.Count; i++)
                pointArray.Add(new Point(((Point)ptArray[i]).X, ((Point)ptArray[i]).Y));
            LstParent = null;
            LstChild = null;
            LstLinkIn = new ArrayList();
            LstLinkOut = new ArrayList();
            LstValue = new ArrayList();
            Guidkey = Guid.NewGuid();

            GuidkeyObjet = new Guid(sGuidLink);
            Texte = sTexte;
            InitProp();
            Initialize();
        }

        public override void Draw(Graphics g)
        {
            int x1 = 0, y1 = 0;     // previous point
            int x2, y2;             // current point
            string sType = GetTypeSimpleTable();
            int iPos = 0;
            int DeltaXC = 0, DeltaX = 0, iSegment = 0;
            ToolInfLink to = (ToolInfLink)F.drawArea.tools[(int)DrawArea.DrawToolType.InfLink];
            Color Couleur;

            if (pointArray.Count >= 2)
            {
                int n = GetIndexFromName("Couleur");
                if (n > -1)
                {
                    if ((string)LstValue[n] == "") Couleur = Color.Black;
                    else Couleur = Color.FromName((string)LstValue[n]);
                }
                else Couleur = to.LineCouleur;

                g.SmoothingMode = SmoothingMode.AntiAlias;

                Pen pen = new Pen(Couleur, to.LineWidth);

                IEnumerator enumerator = pointArray.GetEnumerator();

                n = F.oCnxBase.ConfDB.FindTable(sType);
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
                                DrawGrpTxt(g, 3, 0, x1 + 2 * Axe, y1 - Axe - HeightFont[1], 0, to.Pen1Couleur, to.BkGrCouleur);
                                break;
                            case 2:
                                DrawGrpTxt(g, 3, 0, x2 + 2 * Axe, y1 - Axe - HeightFont[1], 0, to.Pen1Couleur, to.BkGrCouleur);
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
        }

        
        public override string GetTypeSimpleTable()
        {
            //return base.GetTypeSimpleTable();
            return "TechLink";
        }

        public override string GetTypeSimpleGTable()
        {
            //return base.GetTypeSimpleGTable();
            return "GTechLink";
        }

        public override int HandleCount
        {
            get
            {
                //return pointArray.Count;
                return (pointArray.Count - 1) * 4 + 1;
            }
        }

        public Point StartHandle(Point point)
        {
            bool AttachObjectIn = false;
            Point pointTemp;

            for (int i = 0; i < F.drawArea.GraphicsList.Count; i++)
            {
                if (F.drawArea.GraphicsList[i].AttachPointInObject(point))
                {
                    AttachObjectIn = true;
                    DrawRectangle dr = (DrawRectangle)F.drawArea.GraphicsList[i];
                    point = dr.GetPointObject(point);
                    if (dr.LePointEstSitue(point) > 2) //Left ou Right
                    {
                        if (LstLinkOut.Count != 0 && ((DrawRectangle)LstLinkOut[0]).LePointEstSitue(((Point)pointArray[pointArray.Count - 1])) < 3)
                        {
                            if (LstLinkIn.Count == 0 || !F.drawArea.GraphicsList[i].AttachPointInObject((Point)pointArray[0]) || dr.LePointEstSitue((Point)pointArray[0]) < 3)
                            {
                                pointTemp = new Point(((Point)pointArray[pointArray.Count - 1]).X, ((Point)pointArray[pointArray.Count - 1]).Y);
                                pointArray.Clear();
                                pointArray.Add(new Point(point.X, point.Y));
                                pointArray.Add(new Point(pointTemp.X, point.Y));
                                pointArray.Add(new Point(pointTemp.X, pointTemp.Y));


                            }
                        }
                        else if (LstLinkOut.Count != 0 && ((DrawRectangle)LstLinkOut[0]) != dr && ((DrawRectangle)LstLinkOut[0]).LePointEstSitue(((Point)pointArray[pointArray.Count - 1])) > 2)
                        {
                            if (LstLinkIn.Count == 0 || !F.drawArea.GraphicsList[i].AttachPointInObject((Point)pointArray[0]) || dr.LePointEstSitue((Point)pointArray[0]) < 3)
                            {
                                pointTemp = new Point(((Point)pointArray[pointArray.Count - 1]).X, ((Point)pointArray[pointArray.Count - 1]).Y);
                                pointArray.Clear();
                                if (pointTemp.Y == point.Y)
                                {
                                    pointArray.Add(new Point(point.X, point.Y));
                                    pointArray.Add(new Point(pointTemp.X, pointTemp.Y));
                                }
                                else
                                {
                                    pointArray.Add(new Point(point.X, point.Y));
                                    pointArray.Add(new Point((pointTemp.X + point.X) / 2, point.Y));
                                    pointArray.Add(new Point((pointTemp.X + point.X) / 2, pointTemp.Y));
                                    pointArray.Add(new Point(pointTemp.X, pointTemp.Y));
                                }
                            }
                        }
                    }
                    else if (dr.LePointEstSitue(point) < 3) //Top ou Bottom
                    {
                        if (LstLinkOut.Count != 0 && ((DrawRectangle)LstLinkOut[0]).LePointEstSitue(((Point)pointArray[pointArray.Count - 1])) > 2)
                        {
                            if (LstLinkIn.Count == 0 || !F.drawArea.GraphicsList[i].AttachPointInObject((Point)pointArray[0]) || dr.LePointEstSitue((Point)pointArray[0]) > 2)
                            {
                                pointTemp = new Point(((Point)pointArray[pointArray.Count - 1]).X, ((Point)pointArray[pointArray.Count - 1]).Y);
                                pointArray.Clear();
                                pointArray.Add(new Point(point.X, point.Y));
                                pointArray.Add(new Point(point.X, pointTemp.Y));
                                pointArray.Add(new Point(pointTemp.X, pointTemp.Y));
                            }
                        }
                        else if (LstLinkOut.Count != 0 && ((DrawRectangle)LstLinkOut[0]) != dr && ((DrawRectangle)LstLinkOut[0]).LePointEstSitue(((Point)pointArray[pointArray.Count - 1])) < 3)
                        {
                            if (LstLinkIn.Count == 0 || !F.drawArea.GraphicsList[i].AttachPointInObject((Point)pointArray[0]) || dr.LePointEstSitue((Point)pointArray[0]) > 2)
                            {
                                pointTemp = new Point(((Point)pointArray[pointArray.Count - 1]).X, ((Point)pointArray[pointArray.Count - 1]).Y);
                                pointArray.Clear();
                                if (pointTemp.X == point.X)
                                {
                                    pointArray.Add(new Point(point.X, point.Y));
                                    pointArray.Add(new Point(pointTemp.X, pointTemp.Y));
                                }
                                else
                                {
                                    pointArray.Add(new Point(point.X, point.Y));
                                    pointArray.Add(new Point(point.X, (pointTemp.Y + point.Y) / 2));
                                    pointArray.Add(new Point(pointTemp.X, (pointTemp.Y + point.Y) / 2));
                                    pointArray.Add(new Point(pointTemp.X, pointTemp.Y));
                                }
                            }
                        }
                        else AttachObjectIn = false;
                    }
                    break;
                }
            }
            if (!AttachObjectIn && pointArray.Count != 2)
            {
                pointTemp = new Point(((Point)pointArray[pointArray.Count - 1]).X, ((Point)pointArray[pointArray.Count - 1]).Y);
                pointArray.Clear();
                if (LstLinkOut.Count != 0 && ((DrawRectangle)LstLinkOut[0]).LePointEstSitue(((Point)pointTemp)) < 3)
                    pointArray.Add(new Point(pointTemp.X, point.Y));
                else if (LstLinkOut.Count != 0 && ((DrawRectangle)LstLinkOut[0]).LePointEstSitue(((Point)pointTemp)) > 2)
                    pointArray.Add(new Point(point.X, pointTemp.Y));
                else pointArray.Add(new Point(point.X, point.Y));
                pointArray.Add(new Point(pointTemp.X, pointTemp.Y));
            }

            ToolPointer tp = (ToolPointer)F.drawArea.tools[(int)DrawArea.DrawToolType.Pointer];
            tp.RESIZEOBJECTHANDLE = HandleEvent(point, 1);

            return point;
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
                        if (Selected && F.drawArea.GraphicsList.SelectionCount == 1) point = StartHandle(point);
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
        public override Point MoveHandle(int handleNumber, int deltaX, int deltaY)
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

        public override void Move(int deltaX, int deltaY)
        {
            int n = pointArray.Count;
            Point point;

            if (LstLinkIn.Count == 0 && LstLinkOut.Count == 0)
            {
                for (int i = 0; i < n; i++)
                {
                    point = new Point(((Point)pointArray[i]).X + deltaX, ((Point)pointArray[i]).Y + deltaY);

                    pointArray[i] = point;
                }
            }
            else if (LstLinkIn.Count > 0 && LstLinkOut.Count > 0)
            {
                for (int i = 0; i < n; i++)
                {
                    point = new Point(((Point)pointArray[i]).X + deltaX, ((Point)pointArray[i]).Y + deltaY);

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
            }
            else if (LstLinkIn.Count > 0) MoveHandleTo(MoveHandle(5, deltaX, deltaY), 5);
            else MoveHandleTo(MoveHandle(1, deltaX, deltaY), 1);

            Invalidate();
        }

        public override void dataGrid_CellClick(DataGridView odgv, DataGridViewCellEventArgs e)
        {
            int n;

            n = GetIndexFromName("LinksApplicatifs");
            if (n > -1 && e.RowIndex == n) // Link Applicatif
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromTv("Link");
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }
            n = GetIndexFromName("NomGroupService");
            if (n > -1 && e.RowIndex == n) // Service/protole
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromTv("Service");
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }
        }

        public override void initLinkIn()
        {
            SetValueFromName("GuidServerIn", (object)"");
        }

        public override void initLinkOut()
        {
            SetValueFromName("GuidServerOut", (object)"");
        }

        public override void initBaseLinkIn()
        {
            F.oCnxBase.CBWrite("UPDATE TechLink SET GuidServerIn = '' WHERE GuidTechLink='" + GuidkeyObjet + "'");
        }

        public override void initBaseLinkOut()
        {
            F.oCnxBase.CBWrite("UPDATE TechLink SET GuidServerOut = '' WHERE GuidTechLink='" + GuidkeyObjet + "'");
        }

        public override void RemoveGSpecifique(string obj)
        {
            F.oCnxBase.CBWrite("DELETE FROM GPoint WHERE GuidGObjet = '" + obj + "'");
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
            foreach (Point p in pointArray)
            {
                info.AddValue(
                    String.Format(CultureInfo.InvariantCulture,
                    "{0}{1}-{2}",
                    entryPoint, orderNumber, i++),
                    p);

            }

            base.SaveToStream(info, orderNumber);  // ??
        }

        public override void LoadFromStream(System.Runtime.Serialization.SerializationInfo info, int orderNumber)
        {
            Point point;
            int n = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryLength, orderNumber));

            for (int i = 0; i < n; i++)
            {
                point = (Point)info.GetValue(
                    String.Format(CultureInfo.InvariantCulture,
                    "{0}{1}-{2}",
                    entryPoint, orderNumber, i),
                    typeof(Point));

                pointArray.Add(point);
            }

            base.LoadFromStream(info, orderNumber);
        }

        /// <summary>
        /// Create graphic object used for hit test
        /// </summary>
        protected override void CreateObjects()
        {
            if (AreaPath != null)
                return;

            // Create closed path which contains all polygon vertexes
            AreaPath = new GraphicsPath();
            AreaPen = new Pen(Color.Black, 7);

            int x1 = 0, y1 = 0;     // previous point
            int x2, y2;             // current point

            IEnumerator enumerator = pointArray.GetEnumerator();

            if (enumerator.MoveNext())
            {
                x1 = ((Point)enumerator.Current).X;
                y1 = ((Point)enumerator.Current).Y;
            }

            while (enumerator.MoveNext())
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

        public override void AttachLink(DrawObject o, TypeAttach Attach)
        {
            string sTypeVue = F.tbTypeVue.Text; // (string)F.cbTypeVue.SelectedItem;

            string oIn = "GuidServerIn", oOut = "GuidServerOut";
            if (sTypeVue[0] == 'W') { oIn = "GuidAppIn"; oOut = "GuidAppOut"; }

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

        public override XmlElement savetoXml(XmlDB xmlDB, bool GObj)
        {
            XmlElement elo = base.savetoXml(xmlDB, GObj);
            if (elo != null)
            {
                //TechLinkApp
                if (F.oCnxBase.CBRecherche("Select GuidLink FROM TechLinkApp WHERE GuidTechLink='" + GuidkeyObjet.ToString() + "'"))
                {
                    while (F.oCnxBase.Reader.Read())
                    {
                        XmlElement el = xmlDB.XmlCreatEl(xmlDB.XmlGetFirstElFromParent(elo, "After"), "TechLinkApp", "GuidTechLink,GuidLink");
                        XmlElement elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");
                        xmlDB.XmlSetAttFromEl(elAtts, "GuidTechLink", "s", GuidkeyObjet.ToString());
                        xmlDB.XmlSetAttFromEl(elAtts, "GuidLink", "s", F.oCnxBase.Reader.GetString(0));
                    }
                }
                F.oCnxBase.CBReaderClose();
                return elo;
            }
            return null;
        }

        
        public override void CWInsert(ControlDoc cw, char cTypeVue)
        {

            string sBootRoot = "";
            if (cTypeVue == '2') sBootRoot = GetType().Name.Substring("Draw".Length) + cTypeVue;
            else if (cTypeVue == 'I')
            {
                Guid guid = F.GuidVue;
                sBootRoot = "Flo" + guid.ToString().Replace("-", "");
            }

            if (cTypeVue == '2' || cTypeVue == 'I')
            {
                if (cw.Exist(sBootRoot) > -1)
                    cw.InsertRowFromId(sBootRoot, this);
            }
        }
	}
}
