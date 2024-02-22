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
	public class DrawAppUser : DrawTools.DrawRectangle
	{
                
		public DrawAppUser()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawAppUser(Form1 of, int x, int y, int width, int height,int count)
        {
            F = of;
            OkMove = true;
            Align = true;
            Rectangle = new Rectangle(x, y, width, height);
            LstParent = null;
            LstChild = null;
            LstLinkIn = new ArrayList();
            LstLinkOut = new ArrayList();
            LstValue = new ArrayList();
            GuidkeyObjet = Guid.NewGuid();
            Texte = "User" + count;
            
            Guidkey = Guid.NewGuid();
            InitProp();
            SetValueFromName("Image", "PosteUser");

            Initialize();
        }

        public DrawAppUser(Form1 of, Dictionary<string, object> dic)
        {
            F = of;
            object o = null;
            OkMove = true;
            Align = true;

            LstParent = null;
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

        public DrawAppUser(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = true;
            Align = true;
            InitRectangle(lstValG);
            LstParent = null;
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
            ToolAppUser to = (ToolAppUser)F.drawArea.tools[(int)DrawArea.DrawToolType.AppUser];
            TemplateDt oTemplate = (TemplateDt)to.oLayers[0].lstTemplate[to.GetTemplate((string)GetValueFromName("GuidLayer"))];

            Pen pen = new Pen(oTemplate.Couleur, oTemplate.LineWidth);
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10) {
                int iWidth = DrawGrpTxt(null, 1, 0, 0, 0, 0, Color.Black, Color.Transparent);

                AffIcon(g, r, oTemplate);
                DrawGrpTxt(g, 1, 0, r.Left + r.Width / 2 - iWidth / 2, r.Top - AXE, 0, oTemplate.Pen1Couleur, oTemplate.BkGrCouleur);
            } else g.DrawRectangle(pen, r);

            pen.Dispose();
        }

        public override DrawArea.DrawToolType GetToolTypeForObjExp()
        {
            return DrawArea.DrawToolType.AppUser;
        }

        public override void VisioDraw(ArrayList lstGuid, ArrayList lstShape, MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
            ToolAppUser to = (ToolAppUser)F.drawArea.tools[(int)DrawArea.DrawToolType.AppUser];

            if (lstGuid.IndexOf(GuidkeyObjet.ToString()) == -1)
            {
                if (LstParent != null)
                    for (int ip = 0; ip < LstParent.Count; ip++)
                        if (lstGuid.IndexOf(((DrawObject)LstParent[ip]).GuidkeyObjet.ToString()) == -1)
                            ((DrawObject)LstParent[ip]).VisioDraw(lstGuid, lstShape, page, yPage, qxPage, qyPage);

                //Dessiner l'objet
                MOI.Visio.Shape shape = page.Import(F.sPathRoot + @"\bouton\boy1.png");
                //taille image
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormOut, (short)MOI.Visio.VisCellIndices.visXFormPinX).ResultIU = (Rectangle.Left + Rectangle.Width / 2) * qxPage;
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormOut, (short)MOI.Visio.VisCellIndices.visXFormPinY).ResultIU = yPage - (Rectangle.Top + HeightFont[0] + (Rectangle.Height - HeightFont[0]) / 2) * qyPage;
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormWidth).ResultIU = Rectangle.Width * qxPage;
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormHeight).ResultIU = (Rectangle.Height - HeightFont[0]) * qyPage;

                //Inserer le texte + Couleur + taille + largeur + Y
                int iWidth = DrawGrpTxt(null, 1, 0, 0, 0, 0, Color.Black, Color.Transparent);

                //DrawGrpTxt(shape, 1, 0, (Rectangle.Width / 2 + iWidth/2) * qxPage, (Rectangle.Height + HeightFont[0]) * qyPage, 0, Color.Black);
                DrawGrpTxt(shape, 1, 0, LibWidth / 2 + (Rectangle.Width - iWidth) / 2 * qxPage, (Rectangle.Height + HeightFont[0]) * qyPage, 0, Color.Black, Color.Transparent);

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
                    CWInsertProp(cw, sType.Substring(0, 3) + sGuid, "P");
                }
            }
        }
    }
}
