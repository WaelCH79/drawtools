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
	public class DrawRouter : DrawTools.DrawRectangle
	{
        private int handleVLan;

        public int HandleVLan
        {
            get
            {
                return handleVLan;
            }
            set
            {
                handleVLan = value;
            }
        }

		public DrawRouter()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawRouter(Form1 of, int x, int y, int width, int height,int count)
        {
            F = of;
            OkMove = true;
            Align = true;
            Rectangle = new Rectangle(x, y, width, height);
            LstParent = null;
            LstChild = null;
            LstLinkIn = new ArrayList();
            LstLinkOut = null;
            LstValue = new ArrayList();
            GuidkeyObjet = Guid.NewGuid();
            Texte = "Router" + count;
            Guidkey = Guid.NewGuid();
            InitProp();
            Initialize();
        }

        public DrawRouter(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = true;
            Align = true;
            InitRectangle(lstValG);
            LstParent = null;
            LstChild = null;
            LstLinkIn = new ArrayList();
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
        }

        public override bool ParentPointInObject(Point point)
        {
            return false;
        }

        public override void Draw(Graphics g)
        {
            ToolRouter to = (ToolRouter)F.drawArea.tools[(int)DrawArea.DrawToolType.Router];

            Pen pen = new Pen(to.Couleur, to.LineWidth);
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10) {
                
                //Bitmap image1 = (Bitmap)Image.FromFile(F.sPathRoot + @"\bouton\firewall1.png", true);
                //g.DrawImage(image1, r);
                //g.DrawRectangle(pen, r);
                /*g.DrawRectangle(pen, r.Right-r.Width/6,r.Top+r.Height/3,r.Width/6,r.Height*2/3);
                g.DrawLine(pen, r.Right - r.Width / 6, r.Top + r.Height / 3, r.Left, r.Top);
                g.DrawLine(pen, r.Right, r.Top + r.Height / 3, r.Left+r.Width/6, r.Top);
                g.DrawLine(pen, r.Left, r.Top, r.Left+r.Width/6, r.Top);
                g.DrawLine(pen, r.Right - r.Width / 6, r.Bottom, r.Left, r.Bottom-r.Height/3);
                g.DrawLine(pen, r.Left, r.Bottom - r.Height / 3, r.Left, r.Top);*/
                AffIcon(g, r, to);
                DrawGrpTxt(g, 1, 0, r.Left + Axe, r.Top + r.Height / 2 - 3 * HeightFont[0] / 4, 0, to.Pen1Couleur, to.BkGrCouleur);
            } else g.DrawRectangle(pen, r);

            pen.Dispose();
        }

        public override void VisioDraw(ArrayList lstGuid, ArrayList lstShape, MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
            ToolRouter to = (ToolRouter)F.drawArea.tools[(int)DrawArea.DrawToolType.Router];

            if (lstGuid.IndexOf(GuidkeyObjet.ToString()) == -1)
            {
                if (LstParent != null)
                    for (int ip = 0; ip < LstParent.Count; ip++)
                        if (lstGuid.IndexOf(((DrawObject)LstParent[ip]).GuidkeyObjet.ToString()) == -1)
                            ((DrawObject)LstParent[ip]).VisioDraw(lstGuid, lstShape, page, yPage, qxPage, qyPage);

                //Dessiner l'objet
                MOI.Visio.Shape shape = page.Import(F.sPathRoot + @"\bouton\Firewall1.png");
                //taille image
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormOut, (short)MOI.Visio.VisCellIndices.visXFormPinX).ResultIU = (Rectangle.Left + Rectangle.Width / 2) * qxPage;
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormOut, (short)MOI.Visio.VisCellIndices.visXFormPinY).ResultIU = yPage - (Rectangle.Top + HeightFont[0] + (Rectangle.Height - HeightFont[0]) / 2) * qyPage;
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormWidth).ResultIU = Rectangle.Width * qxPage;
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormHeight).ResultIU = (Rectangle.Height - HeightFont[0]) * qyPage;

                //Texte
                DrawGrpTxt(shape, 1, 0, 0, (Rectangle.Height / 2 + Axe) * qyPage, 0, Color.White, Color.Transparent);

                //Couleur trait
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                //Couleur Fond
                //shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillForegnd).FormulaU = "RGB(" + Color.Yellow.R.ToString() + "," + Color.Yellow.G.ToString() + "," + Color.Yellow.B.ToString() + ")";
                //Arrondi
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineRounding).FormulaU = "3 mm";

                lstShape.Add(shape);
                lstGuid.Add(GuidkeyObjet.ToString());
            }
        }

        /// <summary>
        /// Save Object to the Data Base
        /// </summary>
        public override void savetoDB()
        {
            if (!savetoDBFait())
            {
                base.savetoDB();
                F.oCnxBase.CreatRouterLink(this);

                savetoDBOK();
            }
        }

        public override XmlElement savetoXml(XmlDB xmlDB, bool GObj)
        {
            XmlElement elo = base.savetoXml(xmlDB, GObj);
            if (elo != null)
            {
                //RouterLink
                for (int i = 0; i < LstLinkIn.Count; i++)
                {
                    XmlElement el = xmlDB.XmlCreatEl(xmlDB.XmlGetFirstElFromParent(elo, "After"), "RouterLink", "GuidRouter,GuidVlan");
                    XmlElement elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");
                    xmlDB.XmlSetAttFromEl(elAtts, "GuidRouter", "s", GuidkeyObjet.ToString());
                    xmlDB.XmlSetAttFromEl(elAtts, "GuidVlan", "s", ((DrawObject)LstLinkIn[i]).GuidkeyObjet.ToString());
                }
                return elo;
            }
            return null;
        }
    }
}
