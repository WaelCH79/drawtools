using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data.Odbc;
using MOI = Microsoft.Office.Interop;
 

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawPtCnx : DrawTools.DrawRectangle
	{
        private int handleSanSwitch;
        public int HandleSanSwitch
        {
            get
            {
                return handleSanSwitch;
            }
            set
            {
                handleSanSwitch = value;
            }
        }

		public DrawPtCnx()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawPtCnx(Form1 of, Point pt, DrawLocation dl)
        {
            F = of;
            OkMove = false;
            Align = false;
            Rectangle = new Rectangle(1, 1, 1, 1);
            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = new ArrayList();
            LstValue = new ArrayList();
            LstLinkOut = null;

            Texte = "c" + dl.NbrNPtCnx();
            GuidkeyObjet = Guid.NewGuid();
            Guidkey = Guid.NewGuid();
            InitProp();
            Initialize();
        }

        public DrawPtCnx(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = false;
            Align = false;
            InitRectangle(lstValG);
            LstParent = new ArrayList();
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

        public void Aligne(int x, int y, int width, int height)
        {
            DrawRectangle dr;

            dr = (DrawRectangle)LstParent[0];
            Rectangle = new Rectangle(x, y + Axe, width, height);
        }

        public override void Draw(Graphics g)
        {
            ToolPtCnx to = (ToolPtCnx)F.drawArea.tools[(int)DrawArea.DrawToolType.PtCnx];
            Pen pen = new Pen(to.LineCouleur, to.LineWidth);
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10)
            {
                AffRec(g, r, to); //LineCouleur, LineWidth, Couleur, 2, true, false, false);
                DrawRectangle dr = (DrawRectangle)LstParent[0];
                DrawGrpTxt(g, 3, 0, r.Left + Axe, r.Top + Axe, 0, to.Pen1Couleur, to.BkGrCouleur);
               
            } else g.DrawRectangle(pen, r);

            pen.Dispose();
        }
        public override void VisioDraw(ArrayList lstGuid, ArrayList lstShape, MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
            ToolPtCnx to = (ToolPtCnx)F.drawArea.tools[(int)DrawArea.DrawToolType.PtCnx];

            if (lstGuid.IndexOf(GuidkeyObjet.ToString()) == -1)
            {
                if (LstParent != null)
                    for (int ip = 0; ip < LstParent.Count; ip++)
                        if (lstGuid.IndexOf(((DrawObject)LstParent[ip]).GuidkeyObjet.ToString()) == -1)
                            ((DrawObject)LstParent[ip]).VisioDraw(lstGuid, lstShape, page, yPage, qxPage, qyPage);

                //Ombre

                //Dessiner l'objet
                MOI.Visio.Shape shape = page.DrawRectangle(Rectangle.Left * qxPage, yPage - Rectangle.Top * qyPage, Rectangle.Right * qxPage, yPage - Rectangle.Bottom * qyPage);

                //Inserer le texte + Couleur + taille
                DrawGrpTxt(shape, 3, 0, 0, 0, 0, Color.Black, Color.Transparent);

                //Couleur trait
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLinePattern).ResultIU = 0;
                
                //Couleur Fond
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillBkgnd).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillForegnd).FormulaU = "RGB(" + Color.White.R.ToString() + "," + Color.White.G.ToString() + "," + Color.White.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillPattern).ResultIU = 28;

                lstShape.Add(shape);
                lstGuid.Add(GuidkeyObjet.ToString());
            }
        }
	}
}
