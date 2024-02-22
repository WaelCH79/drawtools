using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data.Odbc;
using MOI = Microsoft.Office.Interop;
using Newtonsoft.Json;
using System.Collections.Generic;
 

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawGensvc : DrawTools.DrawRectangle
	{
		public DrawGensvc()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawGensvc(Form1 of, Dictionary<string, object> dic)
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


        public DrawGensvc(Form1 of, int x, int y, int width, int height,int count)
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
            Texte = "Gensvc" + count;
            Guidkey = Guid.NewGuid();

            InitProp();
            SetValueFromName("TypeIt", 1);
            Initialize();
        }

        public DrawGensvc(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = true;
            Align = true;
            InitRectangle(lstValG);
            CorrectionRatio();
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

        public override void dataGrid_CellClick(DataGridView odgv, DataGridViewCellEventArgs e)
        {
            int n;

            n = GetIndexFromName("GuidLabel");
            if (n > -1 && e.RowIndex == n) // Link Label
            {
                FormLabel fl = new FormLabel(F, odgv);
                fl.AddtvLabelClassFromDB();
                fl.AddlDestinationFromProp();
                //fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fl.ShowDialog(F);
            }
        }

        public override void Draw(Graphics g)
        {
            ToolGensvc tgs = (ToolGensvc)F.drawArea.tools[(int)DrawArea.DrawToolType.Gensvc];
            TemplateDt oTemplate = (TemplateDt)tgs.oLayers[0].lstTemplate[tgs.GetTemplate((string)GetValueFromName("GuidLayer"))];

            Pen pen = new Pen(oTemplate.Couleur, oTemplate.LineWidth);
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10) {
                AffRec(g, r, oTemplate, 0);

                DrawGrpTxt(g, 1, 0, r.Left + HeightMaxIcon, r.Top + Axe, 0, oTemplate.Pen1Couleur, oTemplate.BkGrCouleur);
                AffIcon(g, r, oTemplate);

            } else g.DrawRectangle(pen, r);

            pen.Dispose();
        }

        public override void savetoDB()
        {

            if (!savetoDBFait())
            {
                base.savetoDB();

                //Label
                SetLabelLinks();
                savetoDBOK();
            }
        }

        public override void VisioDraw(ArrayList lstGuid, ArrayList lstShape, MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
            ToolGensvc tgs = (ToolGensvc)F.drawArea.tools[(int)DrawArea.DrawToolType.Gensvc];

            if (lstGuid.IndexOf(GuidkeyObjet.ToString()) == -1)
            {
                if (LstParent != null)
                    for (int ip = 0; ip < LstParent.Count; ip++)
                        if (lstGuid.IndexOf(((DrawObject)LstParent[ip]).GuidkeyObjet.ToString()) == -1)
                            ((DrawObject)LstParent[ip]).VisioDraw(lstGuid, lstShape, page, yPage, qxPage, qyPage);


                //Dessiner l'objet
                //MOI.Visio.Shape shape = page.InsertFromFile(@"C:\Dat\bouton\file3.gif", (short)MOI.Visio.VisInsertObjArgs.visInsertLink);
                MOI.Visio.Shape shape = page.Import(F.sPathRoot + @"\bouton\svc.png");
                //taille image
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormOut, (short)MOI.Visio.VisCellIndices.visXFormPinX).ResultIU = (Rectangle.Left + Rectangle.Width / 2) * qxPage;
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormOut, (short)MOI.Visio.VisCellIndices.visXFormPinY).ResultIU = yPage - (Rectangle.Top + Rectangle.Height / 2) * qyPage;
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormWidth).ResultIU = Rectangle.Width * qxPage;
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormHeight).ResultIU = (Rectangle.Height - HeightFont[0]) * qyPage;

                //Inserer le texte + Couleur + taille + largeur + Y
                //DrawGrpTxt(shape, 1, 0, 3, 2 * AXE * qyPage, 0, Color.Black);
                int iWidth = DrawGrpTxt(null, 1, 0, 0, 0, 0, Color.Black, Color.Transparent);
                DrawGrpTxt(shape, 1, 0, LibWidth / 2 + (Rectangle.Width - iWidth) / 2 * qxPage, 0, 0, Color.Black, Color.Transparent);

                
                //Couleur trait
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + tgs.Couleur.R.ToString() + "," + tgs.Couleur.G.ToString() + "," + tgs.Couleur.B.ToString() + ")";
                //Couleur Fond
                //shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillForegnd).FormulaU = "RGB(" + Color.Yellow.R.ToString() + "," + Color.Yellow.G.ToString() + "," + Color.Yellow.B.ToString() + ")";
                //Arrondi
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineRounding).FormulaU = "3 mm";

                lstShape.Add(shape);
                lstGuid.Add(GuidkeyObjet.ToString());
            }
        }

        public override void AttachLink(DrawObject o, TypeAttach Attach)
        {
            string oParent = "GuidGenks";

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
