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
	public class DrawMCompApp: DrawTools.DrawRectangle
	{
        static private Color Couleur = Color.Olive;
        static private int LineWidth = 1;

		public DrawMCompApp()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawMCompApp(Form1 of)
        {
            F = of;
            OkMove = false;
            Rectangle = new Rectangle(1, 1, 1, 1);
            LstParent = new ArrayList();
            LstChild = new ArrayList();
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = new ArrayList();

            string[] aValue = F.tvObjet.SelectedNode.Text.Split('-');
            Texte = aValue[0].Trim() + "   (" + F.tvObjet.SelectedNode.Name + ")";
            
            GuidkeyObjet = Guid.NewGuid();
            Guidkey = Guid.NewGuid();

            InitProp();
            SetValueFromName("NomMainComposant", Texte);
            SetValueFromName("NomComposant", Texte);
            SetValueFromName("GuidMainComposant", (string)F.tvObjet.SelectedNode.Name);
            Initialize();
        }

        public DrawMCompApp(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = false;

            InitRectangle(lstValG);
            LstParent = new ArrayList();
            LstChild = new ArrayList();
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = lstVal;
            Guidkey = Guid.NewGuid();

            o = GetValueFromLib("Guid");
            if(o!=null)
                GuidkeyObjet = new Guid((string) o);
            o = GetValueFromName("NomMainComposant");
            if (o != null)
            {
//                TreeNode[] ArrayTreeNode = F.tvObjet.Nodes.Find((string)o, true);
//                if (ArrayTreeNode.Length == 1)
//                {
//                    string[] aValue = ArrayTreeNode[0].Text.Split('-');
                    Texte = (string)o;
                    SetValueFromName("NomComposant", Texte);
//                }
            }

            Initialize();
        }

        public override bool AttachPointInObject(Point point)
        {
            return false;
        }

        public void Aligne(int x, int y, int width, int height)
        {
            Rectangle = new Rectangle(x, y, width, height);
        }

        private int NbrMCompServ()
        {
            int CountObj = 0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawMCompServ)) CountObj++;
            return CountObj;
        }

        public void AligneObjet()
        {
            int CountMComp = NbrMCompServ();
            int WidthObjet = Rectangle.Width,  HeightObjet = 15;

            rectangle.Height = (CountMComp + 1) * (Axe + HeightMCompServ) + Axe;
            for (int i = LstChild.Count - 1; i >= 0; i--)
            {
                if (LstChild[i].GetType() == typeof(DrawMCompServ))
                {
                    ((DrawMCompServ)LstChild[i]).Aligne(Rectangle.X + 6 * Axe, Rectangle.Y + Axe + CountMComp * (HeightMCompServ + Axe), WidthObjet - 7 * Axe, HeightObjet);
                    CountMComp--;
                }
            }
        }

        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(Couleur, LineWidth);
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10) {

                AffRec(g, r, Couleur, 5, false, false, true, 1);

                //g.DrawRectangle(pen, r);

                if (F.bPtt) DrawGrpTxt(g, 2, -1, r.Left + Axe, r.Top + Axe, 0, Color.Black);
                else DrawGrpTxt(g, 2, 1, r.Left + Axe, r.Top + Axe, 0, Color.Black);
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
                //shapeOm.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineRounding).FormulaU = "3 mm";

                //Dessiner l'objet
                MOI.Visio.Shape shape = page.DrawRectangle(Rectangle.Left * qxPage, yPage - Rectangle.Top * qyPage, Rectangle.Right * qxPage, yPage - Rectangle.Bottom * qyPage);

                /*MOI.Visio.Selection s = page.CreateSelection(Microsoft.Office.Interop.Visio.VisSelectionTypes.visSelTypeSingle, Microsoft.Office.Interop.Visio.VisSelectMode.visSelModeOnlySuper, shapeOm);
                s.Select(shapeOm, 0);
                s.Select(shape, 0);
                s.ConvertToGroup();*/

                //Inserer le texte + Couleur + taille
                ArrayList aL = new ArrayList();
                aL = GetValueEtCache(LstValue[GetIndexFromName("NomComposant")].ToString());
                shape.Text = (string)aL[0];
                shape.Characters.set_CharProps((short)MOI.Visio.VisCellIndices.visCharacterColor, (short)MOI.Visio.VisDefaultColors.visBlack);
                shape.Characters.set_CharProps((short)MOI.Visio.VisCellIndices.visCharacterSize, 8);
                //Couleur trait
                //shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + Couleur.R.ToString() + "," + Couleur.G.ToString() + "," + Couleur.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLinePattern).ResultIU = 0;

                //Couleur Fond
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillBkgnd).FormulaU = "RGB(" + Couleur.R.ToString() + "," + Couleur.G.ToString() + "," + Couleur.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillForegnd).FormulaU = "RGB(" + Color.White.R.ToString() + "," + Color.White.G.ToString() + "," + Color.White.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillPattern).ResultIU = 32;

                lstShape.Add(shape);
                lstGuid.Add(GuidkeyObjet.ToString());
            }
        }

        public override void AttachLink(DrawObject o, TypeAttach Attach)
        {
            string oParent = "GuidServer";

            switch (Attach)
            {
                case TypeAttach.Parent:
                    SetValueFromName(oParent, o.GuidkeyObjet.ToString());
                    break;
            }
            base.AttachLink(o, Attach);
        }
	}
}
