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
	public class DrawComposant : DrawTools.DrawRectangle
	{
        
        public string TypeC
        {
            get
            {
                return (string)this.GetValueFromName("Mode");
            }
            set
            {
                this.InitProp("Mode", (object)value, true);
            }
        }

		public DrawComposant()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawComposant(Form1 of, Dictionary<string, object> dic)
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


        public DrawComposant(Form1 of, int x, int y, int width, int height,int count)
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
            Texte = "Composant" + count;
            Guidkey = Guid.NewGuid();

            InitProp();
            TypeC = "S";
            Initialize();
        }

        public override bool ParentPointInObject(Point point)
        {
            return false;
        }

        public DrawComposant(Form1 of, ArrayList lstVal, ArrayList lstValG)
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

        public override void Draw(Graphics g)
        {
            ToolComposant to = (ToolComposant)F.drawArea.tools[(int)DrawArea.DrawToolType.Composant];
            TemplateDt oTemplate = (TemplateDt)to.oLayers[0].lstTemplate[to.GetTemplate((string)GetValueFromName("GuidLayer"))];

            Pen pen = new Pen(oTemplate.LineCouleur, oTemplate.LineWidth);
            Pen pen1 = new Pen(oTemplate.LineCouleur, oTemplate.Line1Width);
            int iHeightFont;
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);
            
            if (r.Width > 20 && r.Height > 10) {
                AffRec(g, r, oTemplate, 0);
                AffIcon(g, r, oTemplate);
                /*
                //g.DrawRectangle(pen, r);
                if (Rectangle.Height > 0 && Rectangle.Width > 0 && TypeC[0] == 'A')
                {
                    int h1= Rectangle.Height;
                    if (h1 > 40) h1 = 40;
                    g.DrawArc(pen, Rectangle.Left - h1 / 5, Rectangle.Top - h1 / 5, h1 / 2, h1 / 2, 0, 270);
                    int x1 = Rectangle.Left + h1 / 20 + 3;
                    int y1 = Rectangle.Top - h1 / 5;
                    g.DrawLine(pen1, x1, y1, x1 - 4, y1 - 2);
                    g.DrawLine(pen1, x1 - 4, y1 - 2, x1 - 4, y1 + 3);
                    g.DrawLine(pen1, x1 - 4, y1 + 3, x1, y1);

                }*/
                if (r.Height > 26) iHeightFont = 16; else iHeightFont = r.Height - 10;
                
                DrawGrpTxt(g, 1, 0, r.Left + Axe, r.Top + r.Height / 2 - 3 * HeightFont[0] / 4, 0, Color.Black, Color.Transparent);
            } else g.DrawRectangle(pen, r);

            pen.Dispose();
        }

        public override void dataGrid_CellClick(DataGridView odgv, DataGridViewCellEventArgs e)
        {
            int n;

            n = GetIndexFromName("PWord");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormPropWord fp = new FormPropWord(F, this);
                fp.ShowDialog(F);
            }
        }

        public override void VisioDraw(ArrayList lstGuid, ArrayList lstShape, MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
            ToolComposant to = (ToolComposant)F.drawArea.tools[(int)DrawArea.DrawToolType.Composant];
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

                //Inserer le texte + Couleur + taille
                DrawGrpTxt(shape, 1, 0, 0, 0, 0, Color.Black, Color.Transparent);

                //Couleur trait
                //shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + Couleur.R.ToString() + "," + Couleur.G.ToString() + "," + Couleur.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLinePattern).ResultIU = 0;
                //Couleur Fond
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillBkgnd).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillForegnd).FormulaU = "RGB(" + Color.White.R.ToString() + "," + Color.White.G.ToString() + "," + Color.White.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillPattern).ResultIU = 25;

                lstShape.Add(shape);
                lstGuid.Add(GuidkeyObjet.ToString());
            }
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


        public override void CWInsert(ControlDoc cw, char cTypeVue)
        {
            if (cTypeVue == '1')
            {
                string sType = GetType().Name.Substring("Draw".Length);
                string sGuidP = typeof(DrawComposant).ToString().Substring(14, 3) + cTypeVue + ((DrawObject)LstParent[0]).GuidkeyObjet.ToString().Replace("-", "");
                string sGuid = cTypeVue + GuidkeyObjet.ToString().Replace("-", "");
                //sType ne doit pas depasse 4 caracteres
                string sBook = sType.Substring(0, 3) + sGuid;
                string sTabBookmark = "Tab" + sGuid;
                if (cw.Exist("n" + sGuid) > -1)
                {
                    cw.InsertTextFromId("n" + sGuid, true, Texte, "Titre 5");
                    cw.InsertTextFromId(sTabBookmark, true, "\n", null);
                    cw.InsertTabFromId(sTabBookmark, true, this, null, false, null);
                }
                else if (cw.Exist(sGuidP) > -1)
                {
                    cw.InsertTextFromId(sGuidP, false, "\n", null);
                    cw.CreatIdFromIdP(sBook, sGuidP);
                    cw.InsertTextFromId(sBook, true, Texte + "\n", "Titre 5");
                    cw.CreatIdFromIdP("n" + sGuid, sBook);

                    CWInsertProp(cw, sBook, "P");

                    cw.InsertTextFromId(sBook, false, "Properties\n", "Titre 6");
                    cw.InsertTextFromId(sBook, false, "\n", null);
                    cw.CreatIdFromIdP(sTabBookmark, sBook);
                    cw.InsertTextFromId(sTabBookmark, true, "\n", null);
                    cw.InsertTabFromId(sTabBookmark, false, this, null, false, null);
                }
            }
        }

	}
}
