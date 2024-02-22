using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using MOI = Microsoft.Office.Interop;
 
using Newtonsoft.Json;
using System.Xml;
using System.Collections.Generic;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawInssas : DrawTools.DrawRectangle
	{
		public DrawInssas()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawInssas(Form1 of, Dictionary<string, object> dic)
        {
            F = of;
            object o = null;
            OkMove = true;
            Align = true;

            LstParent = null;
            LstChild = new ArrayList();
            LstLinkIn = new ArrayList();
            LstLinkOut = new ArrayList();
            LstValue = new ArrayList();
            LstValueExtention = new ArrayList();
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

        public DrawInssas(Form1 of, int x, int y, int width, int height, int count)
        {
            F = of;
            OkMove = true;
            Align = true;
            Rectangle = new Rectangle(x, y, width, height);
            LstParent = null; 
            LstChild = new ArrayList();
            LstLinkIn = new ArrayList();
            LstLinkOut = new ArrayList();
            LstValue = new ArrayList();
            LstValueExtention = new ArrayList();
            GuidkeyObjet = Guid.NewGuid();
            Texte = "Inssas" + count;
            Guidkey = Guid.NewGuid();

            InitProp();
            SetValueFromName("TypeIt", 1);
            Initialize();
        }

        public DrawInssas(Form1 of, int x, int y, int width, int height, string GuidGensas, string NomGensas, int count)
        {
            F = of;
            OkMove = true;
            Align = true;
            Rectangle = new Rectangle(x, y, width, height);
            LstParent = null;
            LstChild = new ArrayList();
            LstLinkIn = new ArrayList();
            LstLinkOut = new ArrayList();
            LstValue = new ArrayList();
            LstValueExtention = new ArrayList();
            GuidkeyObjet = Guid.NewGuid();
            Guidkey = Guid.NewGuid();

            InitProp();

            SetValueFromName("TypeIt", 1);
            SetValueFromName("NomInssas", NomGensas + "_I");
            SetValueFromName("GuidGensas", GuidGensas);
            Texte = (string)GetValueFromName("NomInssas");
            Initialize();
        }

        public DrawInssas(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = true;
            Align = true;
            InitRectangle(lstValG);
            CorrectionRatio();
            LstParent = null;
            LstChild = new ArrayList();
            LstLinkIn = new ArrayList();
            LstLinkOut = new ArrayList();
            LstValue = lstVal;
            LstValueExtention = new ArrayList();
            Guidkey = Guid.NewGuid();

            o = GetValueFromLib("Guid");
            if(o!=null)
                GuidkeyObjet = new Guid((string) o);
            o = GetValueFromLib("Nom");
            if (o != null)
                Texte = (string)o;
            
            Initialize();
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
                
                fl.ShowDialog(F);
            }
        }

        public override void Draw(Graphics g)
        {
            ToolInssas tis = (ToolInssas)F.drawArea.tools[(int)DrawArea.DrawToolType.Inssas];
            TemplateDt oTemplate = (TemplateDt)tis.oLayers[0].lstTemplate[tis.GetTemplate((string)GetValueFromName("GuidLayer"))];

            Pen pen = new Pen(tis.LineCouleur, tis.LineWidth);
            Pen pen1 = new Pen(tis.LineCouleur, tis.Line1Width);
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10)
            {
                AffRec(g, r, oTemplate, 0);
                if (F.bPtt)
                {
                    DrawGrpTxt(g, 4, 0, r.Left + Axe, r.Top, 0, oTemplate.Pen1Couleur, oTemplate.BkGrCouleur);
                }
                else
                {
                    DrawGrpTxt(g, 1, 0, r.Left + HeightMaxIcon, r.Top + Axe, 0, oTemplate.Pen1Couleur, oTemplate.BkGrCouleur);
                    DrawGrpTxt(g, 3, 0, r.Left + Axe, r.Top + HeightMaxIcon, 1, oTemplate.Pen1Couleur, oTemplate.BkGrCouleur);
                    AffIcon(g, r, oTemplate);

                }

            }
            else g.DrawRectangle(pen, r);

            pen.Dispose();
        }


        public override void VisioDraw(ArrayList lstGuid, ArrayList lstShape, MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
            ToolInssas tgs = (ToolInssas)F.drawArea.tools[(int)DrawArea.DrawToolType.Inssas];

            if (lstGuid.IndexOf(GuidkeyObjet.ToString()) == -1)
            {
                if (LstParent != null)
                    for (int ip = 0; ip < LstParent.Count; ip++)
                        if (lstGuid.IndexOf(((DrawObject)LstParent[ip]).GuidkeyObjet.ToString()) == -1)
                            ((DrawObject)LstParent[ip]).VisioDraw(lstGuid, lstShape, page, yPage, qxPage, qyPage);

                //Dessiner l'objet
                MOI.Visio.Shape shape = page.DrawRectangle(Rectangle.Left * qxPage, yPage - Rectangle.Top * qyPage, Rectangle.Right * qxPage, yPage - Rectangle.Bottom * qyPage);

                //Inserer le texte + Couleur + taille
                shape.Text = Texte;
                shape.Characters.set_CharProps((short)MOI.Visio.VisCellIndices.visCharacterColor, (short)MOI.Visio.VisDefaultColors.visBlack);
                shape.Characters.set_CharProps((short)MOI.Visio.VisCellIndices.visCharacterSize, 8);
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowText, (short)MOI.Visio.VisCellIndices.visTxtBlkVerticalAlign).ResultIU = 0;
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowText, (short)MOI.Visio.VisCellIndices.visTxtBlkTopMargin).ResultIU = 0;
                //shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowText, (short)MOI.Visio.VisCellIndices.visTxtBlkDirection).ResultIU = 1;
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionParagraph, (short)MOI.Visio.VisRowIndices.visRowParagraph, (short)MOI.Visio.VisCellIndices.visHorzAlign).ResultIU = 0;

                //shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionCharacter, (short)MOI.Visio.VisRowIndices.visRowCharacter, (short)MOI.Visio.VisCellIndices.vis1DBeginY).ResultIU = yPage - Rectangle.Top * qyPage;
                //Couleur trait
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + tgs.Couleur.R.ToString() + "," + tgs.Couleur.G.ToString() + "," + tgs.Couleur.B.ToString() + ")";
                //Couleur Fond
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillBkgnd).FormulaU = "RGB(" + tgs.Couleur.R.ToString() + "," + tgs.Couleur.G.ToString() + "," + tgs.Couleur.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillForegnd).FormulaU = "RGB(" + Color.White.R.ToString() + "," + Color.White.G.ToString() + "," + Color.White.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillPattern).ResultIU = 33;
                //Arrondi
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineRounding).FormulaU = "3 mm";

                lstShape.Add(shape);
                lstGuid.Add(GuidkeyObjet.ToString());
            }
        }

        /// <summary>
        /// Vérifie si l'objet à déplacer peut l'être
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override int MovePossible(DrawObject o)
        {
            return 0;
        }

        /// <summary>
        /// Move handle to new point (resizing)
        /// </summary>
        /// <param name="point"></param>
        /// <param name="handleNumber"></param>
        public override void MoveHandleTo(Point point, int handleNumber)
        {
            base.MoveHandleTo(point, handleNumber);
        }

        public override void CWInsert(ControlDoc cw, char cTypeVue)
        {
            if (cTypeVue == '3' || cTypeVue == '5' || cTypeVue == '4')
            {
                string sType = GetType().Name.Substring("Draw".Length);
                Guid guid = F.GuidVue;
                string sGuidVue = guid.ToString().Replace("-", "");
                string sClusterBook = sType + sGuidVue;
                string sGuidKey = GuidkeyObjet.ToString().Replace("-", "");
                string sBook = sType.Substring(0, 3) + sGuidVue.Substring(0, 16) + sGuidKey.Substring(0, 16);
                string sGuid = "n" + cTypeVue + sBook;

                if (cw.Exist(sGuid) > -1)
                {
                    cw.InsertTextFromId(sBook, true, Texte + "\n", "Titre 4");
                    cw.InsertTabFromId("n" + sBook, true, this, null, false, null);
                }
                else if (cw.Exist(sClusterBook) > -1)
                {
                    //sType ne doit pas depasse 4 caracteres

                    cw.InsertTextFromId(sClusterBook, false, "\n", null);
                    cw.CreatIdFromIdP(sBook, sClusterBook);
                    cw.InsertTextFromId(sBook, true, Texte + "\n", "Titre 4");
                    cw.CreatIdFromIdP(sGuid, sBook);
                    CWInsertProp(cw, sBook, "P");
                    cw.InsertTextFromId(sBook, false, "Properties\n", "Titre 6");
                    cw.InsertTextFromId(sBook, false, "\n", null);
                    cw.CreatIdFromIdP("n" + sBook, sBook);
                    cw.InsertTextFromId("n" + sBook, false, "\n", null);
                    cw.InsertTabFromId("n" + sBook, false, this, null, false, null);
                }
            }

        }

        
        public override string GetSelectExtention()
        {
            return "select GuidExtention, Param.GuidParam, NomParam, AffNomProp from  ManagedsvcLink, Param, ExtentionParamLink Where ManagedsvcLink.GuidManagedsvc = ExtentionParamLink.GuidExtention and ExtentionParamLink.GuidParam = Param.GuidParam and ManagedsvcLink.GuidGensas = '" + GetValueFromName("GuidGensas") + "'";
        }

        public override void savetoDB()
        {
            if (!savetoDBFait())
            {
                base.savetoDB();

                //Extention
                SaveExtention();

                //Label
                SetLabelLinks();

                savetoDBOK();
            }
        }

	}
}
