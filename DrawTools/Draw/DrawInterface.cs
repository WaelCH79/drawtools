using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using MOI = Microsoft.Office.Interop;
 
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawInterface : DrawTools.DrawRectangle
	{
        //Liste des interfaces deployées
        //
        //SELECT Distinct
        //    NomApplication, Lettre, NomVue, NomInterface, NomProduitApp, NomMainComposant, NomMainComposantRef
        //    user 
        //From Interface, ProduitApp, MainComposant, MainComposantRef, MCompServ, Package, Vue, Application, Environnement
        //WHERE
        //    Interface.GuidMainComposant = MainComposant.GuidMainComposant and
        //    Interface.GuidProduitApp = ProduitApp.GuidProduitApp and
        //    ProduitApp.GuidProduitApp = MainComposantRef.GuidProduitApp and
        //    MainComposantRef.GuidMainComposantRef = MCompServ.GuidMainComposantRef and
        //    MainComposant.GuidMainComposant = MCompServ.GuidMainComposant and
        //    MCompServ.GuidMCompServ = Package.GuidMCompServ and
        //    Package.GuidVue = Vue.GuidVue and
        //    Vue.GuidApplication = Application.GuidApplication and
        //    Vue.GuidEnvironnement = Environnement.GuidEnvironnement

        public DrawInterface()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawInterface(Form1 of, int x, int y, int width, int height,int count)
        {
            F = of;
            OkMove = true;
            Align = true;
            Rectangle = new Rectangle(x, y, width, height);
            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = new ArrayList();
            LstLinkOut = new ArrayList();
            LstValue = new ArrayList();
            GuidkeyObjet = Guid.NewGuid();
            Texte = "Interface" + count;
            Guidkey = Guid.NewGuid();

            InitProp();
            Initialize();
        }

        public DrawInterface(Form1 of, Dictionary<string, object> dic)
        {
            F = of;
            object o = null;
            OkMove = true;
            Align = true;

            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = new ArrayList();
            LstLinkOut = new ArrayList();
            LstValue = new ArrayList();
            Guidkey = Guid.NewGuid();
            dicObj = dic;
            InitProp();
            InitValueFromDic(dic);
            InitRectangle(LstValue, false);

            o = GetValueFromLib("Guid");
            if (o != null)
                GuidkeyObjet = new Guid((string)o);
            o = GetValueFromLib("Nom");
            if (o != null)
                Texte = (string)o;

            Initialize();
        }

        public DrawInterface(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = true;
            Align = true;
            InitRectangle(lstValG);
            LstParent = new ArrayList();
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

            Initialize();
        }

        public override bool ParentPointInObject(Point point)
        {
            return false;
        }

        public override void Draw(Graphics g)
        {
            ToolInterface to = (ToolInterface)F.drawArea.tools[(int)DrawArea.DrawToolType.Interface];
            TemplateDt oTemplate = (TemplateDt)to.oLayers[0].lstTemplate[to.GetTemplate((string)GetValueFromName("GuidLayer"))];

            Pen pen = new Pen(oTemplate.LineCouleur, oTemplate.LineWidth);
            Pen pen1 = new Pen(oTemplate.LineCouleur, oTemplate.Line1Width);
            int iHeightFont;
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10) {
                if (r.Height > 26) iHeightFont = 16; else iHeightFont = r.Height - 10;
                Rectangle rIcon = AffIcon(g, new Rectangle(r.Left, r.Top, 0, r.Height), oTemplate);
                if(rIcon.Left != 0)
                    DrawGrpTxt(g, 1, 0, rIcon.Right + Axe, r.Top + r.Height / 2 - 3 * HeightFont[0] / 4, 0, oTemplate.Pen1Couleur, oTemplate.BkGrCouleur);
                else
                {
                    AffRec(g, r, oTemplate, 0);

                    g.DrawLine(pen1, r.Left, r.Top + HeightInterface, r.Left + r.Width, r.Top + HeightInterface);
                    g.DrawLine(pen1, r.Left, r.Top + r.Height - HeightInterface, r.Left + r.Width, r.Top + r.Height - HeightInterface);

                    DrawGrpTxt(g, 1, 0, r.Left + Axe, r.Top + r.Height / 2 - 3 * HeightFont[0] / 4, 0, oTemplate.Pen1Couleur, oTemplate.BkGrCouleur);
                }
                            
                //DrawGrpTxt(g, 1, 0, rIcon.Right + Axe, r.Top + r.Height / 2 - 3 * HeightFont[0] / 4, 0, oTemplate.Pen1Couleur, oTemplate.BkGrCouleur);
            } else g.DrawRectangle(pen, r);

            pen.Dispose();
        }

        public override void AttachLink(DrawObject o, TypeAttach Attach)
        {
            string oParent = "GuidMainComposant";

            switch (Attach)
            {
                case TypeAttach.Parent:
                    SetValueFromName(oParent, o.GuidkeyObjet.ToString());
                    break;
            }
            base.AttachLink(o, Attach);
        }

        public override void dataGrid_CellClick(DataGridView odgv, DataGridViewCellEventArgs e)
        {
            //if (odgv.CurrentCell.RowIndex == 2) // Ligne Link Applicatif
            int n;

            n = GetIndexFromName("NomProduitApp");
            if (n > -1 && e.RowIndex == n) 
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromDB("Select GuidProduitApp, NomProduitApp From ProduitApp Order By NomProduitApp", "Value");
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }
        }

        public override void VisioDraw(ArrayList lstGuid, ArrayList lstShape, MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
            ToolInterface to = (ToolInterface)F.drawArea.tools[(int)DrawArea.DrawToolType.Interface];

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
                MOI.Visio.Shape LnH1 = page.DrawLine(Rectangle.Left * qxPage, yPage - (Rectangle.Top + HeightInterface) * qyPage, Rectangle.Right * qxPage, yPage - (Rectangle.Top + HeightInterface) * qyPage);
                MOI.Visio.Shape LnH2 = page.DrawLine(Rectangle.Left * qxPage, yPage - (Rectangle.Bottom - HeightInterface) * qyPage, Rectangle.Right * qxPage, yPage - (Rectangle.Bottom - HeightInterface) * qyPage);

                /*MOI.Visio.Selection s = page.CreateSelection(Microsoft.Office.Interop.Visio.VisSelectionTypes.visSelTypeSingle, Microsoft.Office.Interop.Visio.VisSelectMode.visSelModeOnlySuper, shapeOm);
                s.Select(shapeOm, 0);
                s.Select(shape, 0);
                s.ConvertToGroup();*/

                //Inserer le texte + Couleur + taille
                DrawGrpTxt(shape, 1, 0, 0, 0, 0, Color.Black, Color.Transparent);
                
                //Couleur trait
                //shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + Couleur.R.ToString() + "," + Couleur.G.ToString() + "," + Couleur.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLinePattern).ResultIU = 0;
                LnH1.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                LnH2.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                //Couleur Fond
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillBkgnd).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillForegnd).FormulaU = "RGB(" + Color.White.R.ToString() + "," + Color.White.G.ToString() + "," + Color.White.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillPattern).ResultIU = 32;

                lstShape.Add(shape);
                lstGuid.Add(GuidkeyObjet.ToString());
            }
        }

        

        public override DrawArea.DrawToolType GetToolTypeForObjExp()
        {
            return DrawArea.DrawToolType.Interface;
        }
	}
}
