using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data.Odbc;
using MOI = Microsoft.Office.Interop;
 
using System.Xml;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawCnx : DrawTools.DrawRectangle
	{
        public ArrayList pointArray;         // list of points > 9
        private ArrayList attachhandle;
        
		public DrawCnx()
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

        public DrawCnx(Form1 of, int x, int y, int width, int height,int count)
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
            Texte = "Cnx" + count;
            Guidkey = Guid.NewGuid();

            InitProp();
            Initialize();
        }

        public DrawCnx(Form1 of, ArrayList lstVal, ArrayList lstValG)
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
            ToolCnx to = (ToolCnx)F.drawArea.tools[(int)DrawArea.DrawToolType.Cnx];

            Pen pen = new Pen(to.LineCouleur, to.LineWidth);
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10) {
                g.DrawEllipse(pen, r);

                Point ptCenter = new Point((r.Left + r.Right) / 2, (r.Top + 2* r.Height / 3));
                for (int i = 0; i < pointArray.Count; i++)
                {
                    Point pt = (Point)pointArray[i];
                    g.DrawLine(pen, ptCenter.X, ptCenter.Y, pt.X, ptCenter.Y);
                    g.DrawLine(pen, pt.X, ptCenter.Y, pt.X, pt.Y);
                }
                DrawGrpTxt(g, 2, 0, r.Left + 10 * Axe, r.Top + 5, 0, to.Pen1Couleur, to.BkGrCouleur);
                
            } else g.DrawRectangle(pen, r);

            pen.Dispose();
        }

        public override void VisioDraw(ArrayList lstGuid, ArrayList lstShape, MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
            ToolCnx to = (ToolCnx)F.drawArea.tools[(int)DrawArea.DrawToolType.Cnx];

            if (lstGuid.IndexOf(GuidkeyObjet.ToString()) == -1)
            {
                //////////////////PAS le meme que les autres/////////////////////////////////////////////////
                if (LstLinkOut !=null)
                    for (int ip = 0; ip < LstLinkOut.Count; ip++)
                        if (lstGuid.IndexOf(((DrawObject)LstLinkOut[ip]).GuidkeyObjet.ToString()) == -1)
                            ((DrawObject)LstLinkOut[ip]).VisioDraw(lstGuid, lstShape, page, yPage, qxPage, qyPage);
                ////////////////////////////////////////////////////////////////////////////////////////////

                //Dessiner l'objet
                MOI.Visio.Shape shape = page.DrawOval(Rectangle.Left * qxPage, yPage - Rectangle.Top * qyPage, Rectangle.Right * qxPage, yPage - Rectangle.Bottom * qyPage);

                //Inserer le texte + Couleur + taille
                int iWidth = DrawGrpTxt(null, 2, 0, 0, 0, 0, Color.Black, Color.Transparent);
                DrawGrpTxt(shape, 2, 0, LibWidth / 2 + (Rectangle.Width - iWidth) / 2 * qxPage, 0, 0, Color.Black, Color.Transparent);

                //Couleur trait
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                //Couleur Fond
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillBkgnd).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillForegnd).FormulaU = "RGB(" + Color.White.R.ToString() + "," + Color.White.G.ToString() + "," + Color.White.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillPattern).ResultIU = 28;


                //Link avec les ptCncx 
                MOI.Visio.Master masterLink = page.Application.Documents[2].Masters.get_ItemU("Dynamic Connector");
                MOI.Visio.Cell XCell, YCell, Attach;
                ArrayList lstP = pointArray;
                ArrayList lstd = new ArrayList();
                /*
                System.Collections.Generic.List<double> doubles = new System.Collections.Generic.List<double>();
                for (int j = 0; j < lstP.Count; j++)
                {
                    doubles.Add(((double)((Point)lstP[j]).X * qxPage));
                    doubles.Add(((double)yPage - ((Point)lstP[j]).Y * qyPage));
                }
                Array xyarray = doubles.ToArray();
                */
                //MOI.Visio.Shape shapeLien = page.Drop(masterLink, 0, 0);

                //Cretation Point de connection  sur l'objet Cnx
                short cncRowShape = -1;
                cncRowShape = shape.AddRow((short)MOI.Visio.VisSectionIndices.visSectionConnectionPts, (short)MOI.Visio.VisRowIndices.visRowLast, (short)MOI.Visio.VisRowTags.visTagDefault);
                XCell = shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionConnectionPts, cncRowShape, (short)MOI.Visio.VisCellIndices.visCnnctX);
                XCell.ResultIU = Rectangle.Width / 2 * qxPage;
                YCell = shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionConnectionPts, cncRowShape, (short)MOI.Visio.VisCellIndices.visCnnctY);
                YCell.ResultIU = Rectangle.Height / 2 * qyPage;

                for (int idxcnx = 0; idxcnx < LstLinkOut.Count; idxcnx++)
                {
                    MOI.Visio.Shape shapeLien = page.Drop(masterLink, 0, 0);

                    //Cretation Point de connection  sur l'objet ptCnx
                    string SearchGuid = ((DrawObject)LstLinkOut[idxcnx]).GuidkeyObjet.ToString();
                    int idx = F.drawArea.GraphicsList.FindObjet(0, SearchGuid);
                    if (idx > -1)
                    {
                        DrawRectangle oRec = (DrawRectangle)F.drawArea.GraphicsList[idx];
                        MOI.Visio.Shape shapeRec = (MOI.Visio.Shape)lstShape[lstGuid.IndexOf(SearchGuid)];

                        short connectorRow = -1;
                        connectorRow = shapeRec.AddRow((short)MOI.Visio.VisSectionIndices.visSectionConnectionPts, (short)MOI.Visio.VisRowIndices.visRowLast, (short)MOI.Visio.VisRowTags.visTagDefault);
                        XCell = shapeRec.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionConnectionPts, connectorRow, (short)MOI.Visio.VisCellIndices.visCnnctX);
                        XCell.ResultIU = (((Point)lstP[idxcnx]).X - oRec.Rectangle.Left) * qxPage;
                        YCell = shapeRec.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionConnectionPts, connectorRow, (short)MOI.Visio.VisCellIndices.visCnnctY);
                        YCell.ResultIU = (oRec.Rectangle.Bottom - ((Point)lstP[idxcnx]).Y) * qyPage;

                        //Attach Link PtCnx
                        Attach = shapeLien.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXForm1D, (short)MOI.Visio.VisCellIndices.vis1DBeginX);
                        Attach.GlueTo(shapeRec.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionConnectionPts, connectorRow, (short)MOI.Visio.VisCellIndices.visCnnctX));

                        //Attach Link Cnx
                        Attach = shapeLien.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXForm1D, (short)MOI.Visio.VisCellIndices.vis1DEndX);
                        Attach.GlueTo(shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionConnectionPts, cncRowShape, (short)MOI.Visio.VisCellIndices.visCnnctX));

                    }
                }
                /*
                //Cretation Point de connection Out
                SearchGuid = ((DrawObject)o.LstLinkOut[0]).GuidkeyObjet.ToString();
                idx = drawArea.GraphicsList.FindObjet(0, SearchGuid);
                if (idx > -1)
                {
                    DrawRectangle oRec = (DrawRectangle)drawArea.GraphicsList[idx];
                    MOI.Visio.Shape shapeRec = (MOI.Visio.Shape)lstShape[lstGuid.IndexOf(SearchGuid)];

                    short connectorRow = -1;
                    connectorRow = shapeRec.AddRow((short)MOI.Visio.VisSectionIndices.visSectionConnectionPts, (short)MOI.Visio.VisRowIndices.visRowLast, (short)MOI.Visio.VisRowTags.visTagDefault);
                    MOI.Visio.Cell XCell = shapeRec.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionConnectionPts, connectorRow, (short)MOI.Visio.VisCellIndices.visCnnctX);
                    XCell.ResultIU = (((Point)lstP[lstP.Count - 1]).X - oRec.Rectangle.Left) * qxPage;
                    MOI.Visio.Cell YCell = shapeRec.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionConnectionPts, connectorRow, (short)MOI.Visio.VisCellIndices.visCnnctY);
                    YCell.ResultIU = (oRec.Rectangle.Bottom - ((Point)lstP[lstP.Count - 1]).Y) * qyPage;

                    //Attach Link shapeLien
                    //MOI.Visio.Cell Attach = shapeL.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXForm1D, (short)MOI.Visio.VisCellIndices.vis1DEndX);
                    MOI.Visio.Cell Attach = shapeLien.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXForm1D, (short)MOI.Visio.VisCellIndices.vis1DEndX);
                    Attach.GlueTo(shapeRec.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionConnectionPts, connectorRow, (short)MOI.Visio.VisCellIndices.visCnnctX));

                }
                */











                lstShape.Add(shape);
                lstGuid.Add(GuidkeyObjet.ToString());
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
                    if (o.GetType() == typeof(DrawPtCnx)) // || o.GetType() == typeof(DrawRouter))
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
                F.oCnxBase.CreatGCnxPoint(this);
                savetoDBOK();
            }
        }

        public void XmlCreatGCnxPoint(XmlDB xmlDB, XmlElement elParent)
        {

            for (int i = 0; i < pointArray.Count; i++)
            {
                XmlElement el = xmlDB.XmlCreatEl(elParent, "GPoint", "GuidGObjet,I");
                XmlElement elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");
                xmlDB.XmlSetAttFromEl(elAtts, "GuidGObjet", "s", Guidkey.ToString());
                xmlDB.XmlSetAttFromEl(elAtts, "I", "i", i.ToString());
                xmlDB.XmlSetAttFromEl(elAtts, "X", "i", ((Point)pointArray[i]).X.ToString());
                xmlDB.XmlSetAttFromEl(elAtts, "Y", "i", ((Point)pointArray[i]).Y.ToString());
            }
        }

        public override XmlElement savetoXml(XmlDB xmlDB, bool GObj)
        {
            System.Xml.XmlElement el = base.savetoXml(xmlDB, GObj);
            if (el != null)
            {
                XmlCreatGCnxPoint(xmlDB, xmlDB.XmlGetFirstElFromParent(el, "After"));
                return el;
            }
            return null;
        }

        public override void CWInsert(ControlDoc cw, char cTypeVue)
        {
            if (cTypeVue == '6') // 6-Site
            {
                string sType = GetType().Name.Substring("Draw".Length);
                string sGuid = GuidkeyObjet.ToString().Replace("-", "");
                //sType ne doit pas depasse 4 caracteres
                string sBook = sType.Substring(0, 3) + sGuid;
                if (cw.Exist("n" + sBook) > -1)
                {
                    cw.InsertTextFromId("n" + sBook, true, Texte, "Titre 4");
                    cw.InsertTabFromId(sBook, true, this, null, false, null);
                }
                else if (cw.Exist(sType) > -1)
                {
                    cw.InsertTextFromId(sType, false, "\n", null);
                    cw.CreatIdFromIdP(sType.Substring(0, 3) + sGuid, sType);
                    cw.InsertTextFromId(sBook, true, Texte + "\n", "Titre 4");
                    cw.CreatIdFromIdP("n" + sBook, sType.Substring(0, 3) + sGuid);
                    cw.InsertTextFromId(sBook, false, "\n", null);
                    cw.InsertTabFromId(sBook, false, this, null, false, null);

                    //cw.CreatBookmark("n" + sGuid, sType.Substring(0, 3) + sGuid, 2);
                }
            }
        }

	}
}
