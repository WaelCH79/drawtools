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
	public class DrawQueue : DrawTools.DrawRectangle
	{
        static private Color Couleur = Color.DarkSalmon;
        //static private int LineWidth = 2;

		public DrawQueue()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawQueue(Form1 of, int x, int y, int width, int height,int count)
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
            Texte = "Queue" + count;
            Guidkey = Guid.NewGuid();

            InitProp();
            Initialize();
        }

        public DrawQueue(Form1 of, Dictionary<string, object> dic)
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

        public DrawQueue(Form1 of, ArrayList lstVal, ArrayList lstValG)
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
            ToolQueue to = (ToolQueue)F.drawArea.tools[(int)DrawArea.DrawToolType.Queue];
            TemplateDt oTemplate = (TemplateDt)to.oLayers[0].lstTemplate[to.GetTemplate((string)GetValueFromName("GuidLayer"))];

            Pen pen = new Pen(oTemplate.Couleur, oTemplate.LineWidth);
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10) {
                int iWidth = DrawGrpTxt(null, 2, 0, 0, 0, 0, Color.Black, Color.Transparent);
                AffIcon(g, new Rectangle(r.Left, r.Top, r.Width, r.Height - HeightFont[0]), oTemplate);

                DrawGrpTxt(g, 2, 0, r.Left + r.Width / 2 - iWidth / 2, r.Bottom - HeightFont[0] - Axe, 0, oTemplate.Pen1Couleur, oTemplate.BkGrCouleur);
                
            } else g.DrawRectangle(pen, r);

            pen.Dispose();
        }

        public override void VisioDraw(ArrayList lstGuid, ArrayList lstShape, MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
            ToolFile to = (ToolFile)F.drawArea.tools[(int)DrawArea.DrawToolType.File];

            if (lstGuid.IndexOf(GuidkeyObjet.ToString()) == -1)
            {
                if (LstParent != null)
                    for (int ip = 0; ip < LstParent.Count; ip++)
                        if (lstGuid.IndexOf(((DrawObject)LstParent[ip]).GuidkeyObjet.ToString()) == -1)
                            ((DrawObject)LstParent[ip]).VisioDraw(lstGuid, lstShape, page, yPage, qxPage, qyPage);

                //Dessiner l'objet
                //MOI.Visio.Shape shape = page.InsertFromFile(@"C:\Dat\bouton\file3.gif", (short)MOI.Visio.VisInsertObjArgs.visInsertLink);
                MOI.Visio.Shape shape = page.Import(F.sPathRoot + @"\bouton\file3.png");
                //taille image
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormOut, (short)MOI.Visio.VisCellIndices.visXFormPinX).ResultIU = (Rectangle.Left + Rectangle.Width / 2) * qxPage;
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormOut, (short)MOI.Visio.VisCellIndices.visXFormPinY).ResultIU = yPage - (Rectangle.Top + Rectangle.Height / 2) * qyPage;
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormWidth).ResultIU = Rectangle.Width * qxPage;
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormHeight).ResultIU = (Rectangle.Height - HeightFont[0]) * qyPage;

                //Inserer le texte + Couleur + taille + largeur + Y
                int iWidth = DrawGrpTxt(null, 2, 0, 0, 0, 0, Color.Black, Color.Transparent);
                DrawGrpTxt(shape, 2, 0, LibWidth / 2 + (Rectangle.Width - iWidth) / 2 * qxPage, 0, 0, Color.Black, Color.Transparent);

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

        public override void initLinkParent()
        {
            SetValueFromName("GuidMainComposant", (object)"");
        }

        
        public override void initBaseLinkParent()
        {
            F.oCnxBase.CBWrite("UPDATE Queue SET GuidMainComposant = '' WHERE GuidMainComposant='" + GuidkeyObjet + "'");
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
                string sGuidP = typeof(DrawFile).ToString().Substring(14, 3) + cTypeVue + ((DrawObject)LstParent[0]).GuidkeyObjet.ToString().Replace("-", "");
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
