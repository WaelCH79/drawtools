using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Data.Odbc;
using MOI = Microsoft.Office.Interop;
 

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawAxes : DrawTools.DrawRectangle
	{
        static private Color Couleur= Color.Black;
        static private int LineWidth = 1;

		public DrawAxes()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawAxes(Form1 of, int x, int y, int width, int height,int count)
        {
            string sGuidKeyObjet;

            F = of;
            OkMove = true;
            Align = false;
            Rectangle = new Rectangle(x, y, width, height);
            LstParent = null;
            LstChild = new ArrayList();
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = new ArrayList();
            if (F.drawArea.AddObjet)
            {
                F.drawArea.AddObjet = false;
                
                Texte = F.tvObjet.SelectedNode.Text;
                sGuidKeyObjet = F.oCnxBase.FindGuidFromNom("Module", Texte);
                if (sGuidKeyObjet != null)
                {
                    GuidkeyObjet = new Guid(sGuidKeyObjet);
                }
                else
                {
                    GuidkeyObjet = Guid.NewGuid();
                    Texte = "Axes" + count;
                }
            }
            else
            {
                GuidkeyObjet = Guid.NewGuid();
                Texte = "Axes" + count;
            }
            Guidkey = Guid.NewGuid();
            InitProp();
            Initialize();
        }

        public DrawAxes(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = true;
            Align = false;

            InitRectangle(lstValG);
            LstParent = null;
            LstChild = new ArrayList();
            LstLinkIn = null;
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
            Pen pen = new Pen(Couleur, LineWidth);
            Pen pen1 = new Pen(Color.Gray, LineWidth);
            Rectangle r;
            int longfleche = 10;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);
            
            if (r.Width > 20 && r.Height > 10) {
                int xMoy = r.Left + r.Width / 2, yMoy = r.Top + r.Height / 2;

                AffRec(g, new Rectangle(r.Left, r.Top, r.Width / 2, r.Height / 2), Color.White, LineWidth, Color.LightBlue, 5, false, false, false);
                AffRec(g, new Rectangle(xMoy, r.Top, r.Width / 2, r.Height / 2), Color.White, LineWidth, Color.LightGreen, 5, false, false, false);
                AffRec(g, new Rectangle(xMoy, yMoy, r.Width / 2, r.Height / 2), Color.White, LineWidth, Color.LightSalmon, 5, false, false, false);
                AffRec(g, new Rectangle(r.Left, yMoy, r.Width / 2, r.Height / 2), Color.White, LineWidth, Color.Yellow, 5, false, false, false);

                g.DrawLine(pen, r.Left, r.Bottom, r.Right - longfleche, r.Bottom);
                g.DrawLine(pen, r.Right, r.Bottom, r.Right - longfleche, r.Bottom - longfleche / 2);
                g.DrawLine(pen, r.Right, r.Bottom, r.Right - longfleche, r.Bottom + longfleche / 2);
                g.DrawLine(pen, r.Right - longfleche, r.Bottom + longfleche / 2, r.Right - longfleche, r.Bottom - longfleche / 2);

                g.DrawLine(pen, r.Left, r.Bottom, r.Left, r.Top + longfleche);
                g.DrawLine(pen, r.Left, r.Top, r.Left - longfleche / 2, r.Top + longfleche);
                g.DrawLine(pen, r.Left, r.Top, r.Left + longfleche / 2, r.Top + longfleche);
                g.DrawLine(pen, r.Left - longfleche / 2, r.Top + longfleche, r.Left + longfleche / 2, r.Top + longfleche);

                g.DrawLine(pen1, r.Left, yMoy, r.Right, yMoy);
                g.DrawLine(pen1, xMoy, r.Top, xMoy, r.Bottom);

                g.DrawString((string)GetValueFromName("XAxe"), new Font("Arial", HeightFont[2], FontStyle.Bold), new SolidBrush(Color.Black), r.Right - 20, r.Bottom + 8);
                StringFormat format = new StringFormat(StringFormatFlags.DirectionVertical);
                g.DrawString((string)GetValueFromName("YAxe"), new Font("Arial", HeightFont[2], FontStyle.Bold), new SolidBrush(Color.Black), r.Left - 14, r.Top - 15, format);


            } else g.DrawRectangle(pen, r);

            pen.Dispose();
        }

        public override void VisioDraw(ArrayList lstGuid, ArrayList lstShape, MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
            if (lstGuid.IndexOf(GuidkeyObjet.ToString()) == -1)
            {
                if (LstParent != null)
                    for (int ip = 0; ip < LstParent.Count; ip++)
                        if (lstGuid.IndexOf(((DrawObject)LstParent[ip]).GuidkeyObjet.ToString()) == -1)
                            ((DrawObject)LstParent[ip]).VisioDraw(lstGuid, lstShape, page, yPage, qxPage, qyPage);

                //Ombre
                int epOmbre = 2;
                MOI.Visio.Shape shapeOm = page.DrawRectangle((Rectangle.Left + epOmbre) * qxPage, yPage - (Rectangle.Top + epOmbre) * qyPage, (Rectangle.Right + epOmbre) * qxPage, yPage - (Rectangle.Bottom + epOmbre) * qyPage);
                shapeOm.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillForegnd).FormulaU = "RGB(" + Color.Black.R.ToString() + "," + Color.Black.G.ToString() + "," + Color.Black.B.ToString() + ")";
                shapeOm.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineRounding).FormulaU = "3 mm";

                //Dessiner l'objet
                MOI.Visio.Shape shape = page.DrawRectangle(Rectangle.Left * qxPage, yPage - Rectangle.Top * qyPage, Rectangle.Right * qxPage, yPage - Rectangle.Bottom * qyPage);

                //Inserer le texte + Couleur + taille
                DrawGrpTxt(shape, 1, 0, 0, 0, 0, Color.Black, Color.Transparent);

                //Couleur trait
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + Couleur.R.ToString() + "," + Couleur.G.ToString() + "," + Couleur.B.ToString() + ")";
                //Couleur Fond
                //shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillForegnd).FormulaU = "RGB(" + Color.Yellow.R.ToString() + "," + Color.Yellow.G.ToString() + "," + Color.Yellow.B.ToString() + ")";
                //Arrondi
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineRounding).FormulaU = "3 mm";

                lstShape.Add(shape);
                lstGuid.Add(GuidkeyObjet.ToString());
            }
        }

        public override void dataGrid_CellClick(DataGridView odgv, DataGridViewCellEventArgs e)
        {
            //if (odgv.CurrentCell.RowIndex == 2) // Ligne Link Applicatif
            int n;

            n = GetIndexFromName("PWord");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormPropWord fp = new FormPropWord(F, this);
                fp.ShowDialog(F);
            }
        }

        
        public override void CWInsert(ControlDoc cw, char cTypeVue)
        {
            if (cTypeVue == '0')
            {
                string sType = GetType().Name.Substring("Draw".Length);
                string sGuid = cTypeVue + GuidkeyObjet.ToString().Replace("-", "");
                if (cw.Exist("n" + sGuid) > -1)
                {
                    cw.InsertTextFromId("n" + sGuid, true, Texte, "Titre 3");
                }
                else if (cw.Exist(sType) > -1)
                {
                    //sType ne doit pas depasse 4 caracteres
                    cw.InsertTextFromId(sType, false, "\n", null);
                    cw.CreatIdFromIdP(sType.Substring(0, 3) + sGuid, sType);
                    cw.InsertTextFromId(sType.Substring(0, 3) + sGuid, true, Texte + "\n", "Titre 3");
                    cw.CreatIdFromIdP("n" + sGuid, sType.Substring(0, 3) + sGuid);
                    cw.InsertTextFromId(sType.Substring(0, 3) + sGuid, false, "Definition\n", "Titre 6");
                    //cw.CreatBookmark("n" + sGuid, sType.Substring(0, 3) + sGuid, 2);
                }
            }
        }
    }
}
