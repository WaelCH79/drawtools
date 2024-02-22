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
	public class DrawMainComposant : DrawTools.DrawRectangle
	{

		public DrawMainComposant()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawMainComposant(Form1 of, Dictionary<string, object> dic)
        {
            F = of;
            object o = null;
            OkMove = true;
            Align = true;

            LstParent = null;
            LstChild = new ArrayList();
            LstLinkIn = null;
            LstLinkOut = null;
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


        public DrawMainComposant(Form1 of, int x, int y, int width, int height, int count)
        {
            F = of;
            OkMove = true;
            Align = true;
            Rectangle = new Rectangle(x, y, width, height);
            LstParent = null; 
            LstChild = new ArrayList();
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = new ArrayList();
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
            Texte = "Composant" + count;

            InitProp();
            Initialize();
        }

        public DrawMainComposant(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = true;
            Align = true;
            InitRectangle(lstValG);
            LstParent = new ArrayList(); // vue inf
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

        public DrawMainComposant(Form1 of)
        {
            F = of;
            OkMove = false;
            Align = false;
            Rectangle = new Rectangle(1, 1, 1, 1);
            LstParent = new ArrayList(); // Vue Inf
            LstChild = new ArrayList();
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = new ArrayList();

            Texte = F.tvObjet.SelectedNode.Text.Trim();
            
            GuidkeyObjet = Guid.NewGuid();
            Guidkey = Guid.NewGuid();

            InitProp();
            SetValueFromName("NomMainComposant", Texte);
            SetValueFromName("GuidMainComposant", (string)F.tvObjet.SelectedNode.Name);
            Initialize();
        }

        private int NbrMCompServ()
        {
            int CountObj = 0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawMCompServ)) CountObj++;
            return CountObj;
        }

        public void Aligne(int x, int y, int width, int height)
        {
            Rectangle = new Rectangle(x, y, width, height);
        }

        public void AligneObjet()
        {
            int CountMComp = NbrMCompServ();
            int WidthObjet = Rectangle.Width, HeightObjet = 15;

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

        public override bool AttachPointInObject(Point point)
        {
            return false;
        }

        public override void dataGrid_CellClick(DataGridView odgv, DataGridViewCellEventArgs e)
        {
            int n;

            n = GetIndexFromName("GuidCadreRefFonc");
            if (n > -1 && e.RowIndex == n)
            {
                FormProduct fp = new FormProduct(F, 'F');
                if((string)F.dataGrid.Rows[e.RowIndex].Cells[1].Value=="") fp.InitMainComposantToTextBox(this, true);
                else fp.InitMainComposantToTextBox(this, false);
                fp.ShowDialog(F);
            }

            n = GetIndexFromName("PWord");
            if (n > -1 && e.RowIndex == n)
            {
                FormPropWord fp = new FormPropWord(F, this);
                fp.ShowDialog(F);
            }
        }

        public override void Draw(Graphics g)
        {
            ToolMainComposant to = (ToolMainComposant)F.drawArea.tools[(int)DrawArea.DrawToolType.MainComposant];
            TemplateDt oTemplate = (TemplateDt)to.oLayers[0].lstTemplate[to.GetTemplate((string)GetValueFromName("GuidLayer"))];

            Pen pen = new Pen(oTemplate.LineCouleur, oTemplate.LineWidth);
            Rectangle r;
            
            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10) {

                AffRec(g, r, oTemplate, 0);
                DrawGrpTxt(g, 2, 0, r.Left + Axe, r.Top, 0, oTemplate.Pen1Couleur, oTemplate.BkGrCouleur);
            } else g.DrawRectangle(pen, r);

            pen.Dispose();
        }

        public override void VisioDraw(ArrayList lstGuid, ArrayList lstShape, MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
            ToolMainComposant to = (ToolMainComposant)F.drawArea.tools[(int)DrawArea.DrawToolType.MainComposant];

            if (lstGuid.IndexOf(GuidkeyObjet.ToString()) == -1)
            {
                if (LstParent != null)
                    for (int ip = 0; ip < LstParent.Count; ip++)
                        if (lstGuid.IndexOf(((DrawObject)LstParent[ip]).GuidkeyObjet.ToString()) == -1)
                            ((DrawObject)LstParent[ip]).VisioDraw(lstGuid, lstShape, page, yPage, qxPage, qyPage);

                //Dessiner l'objet
                MOI.Visio.Shape shape = page.DrawRectangle(Rectangle.Left * qxPage, yPage - Rectangle.Top * qyPage, Rectangle.Right * qxPage, yPage - Rectangle.Bottom * qyPage);

                //Inserer le texte
                DrawGrpTxt(shape, 2, 0, 0, (Rectangle.Height - AXE - HeightFont[1]) * qyPage, 0, Color.Black, Color.Transparent);
                //shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowText, (short)MOI.Visio.VisCellIndices.visTxtBlkVerticalAlign).ResultIU = 0; // Texte en Haut
                //shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowText, (short)MOI.Visio.VisCellIndices.visTxtBlkDirection).ResultIU = 1; // Texte Vertical
                //shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionParagraph, (short)MOI.Visio.VisRowIndices.visRowParagraph, (short)MOI.Visio.VisCellIndices.visHorzAlign).ResultIU = 0; // Gauche

                //Couleur trait
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                //Couleur Fond
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillBkgnd).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillForegnd).FormulaU = "RGB(" + Color.White.R.ToString() + "," + Color.White.G.ToString() + "," + Color.White.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillPattern).ResultIU = 25;
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
            int left = Rectangle.Left;
            int top = Rectangle.Top;
            int right = Rectangle.Right;
            int bottom = Rectangle.Bottom;

            switch (handleNumber)
            {
                case 1:
                    left = point.X;
                    top = point.Y;
                    break;
                case 2:
                    top = point.Y;
                    break;
                case 3:
                    right = point.X;
                    top = point.Y;
                    break;
                case 4:
                    right = point.X;
                    break;
                case 5:
                    right = point.X;
                    bottom = point.Y;
                    break;
                case 6:
                    bottom = point.Y;
                    break;
                case 7:
                    left = point.X;
                    bottom = point.Y;
                    break;
                case 8:
                    left = point.X;
                    break;
            }

            SetRectangle(left, top, right - left, bottom - top);
            AligneFonction();
        }

        private int NbrCompFonc()
        {
            int CountFonc=0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawCompFonc)) CountFonc++;
            return CountFonc;
        }

        public void AligneFonction()
        {
            int CountFonc= NbrCompFonc();
            int WidthCompFonc = 0, YCompFonc = Rectangle.Y;

            if (CountFonc != 0)
            {
                WidthCompFonc = Rectangle.Width / CountFonc;
                if(WidthCompFonc-Axe*2<0) WidthCompFonc = Axe*2;
                for (int i = LstChild.Count - 1; i >= 0; i--)
                {
                    if (LstChild[i].GetType() == typeof(DrawCompFonc))
                    {
                        CountFonc--;
                        ((DrawCompFonc)LstChild[i]).Aligne(Rectangle.X + CountFonc * WidthCompFonc + Axe, YCompFonc + HeightFont[1] + 3 * Axe, WidthCompFonc - 2* Axe, HeightCompFonc);
                    }
                }
                 
            }
        }

        private void InsertChild(ControlDoc cw, char cTypeVue, string sBook, string sBookChild, string sChild, string sType)
        {
            if (cw.Exist(sBook) > -1)
            {
                for (int i = 0; i < LstChild.Count; i++)
                {
                    DrawObject o = (DrawObject)LstChild[i];
                    if (o.GetType().ToString() == sType)
                    {
                        if (cw.Exist(sBookChild) == -1)
                        {
                            cw.InsertTextFromId(sBook, false, sChild + "\n", "Titre 4");
                            cw.InsertTextFromId(sBook, false, "\n", null);
                            cw.CreatIdFromIdP(sBookChild, sBook);
                        }
                        o.CWInsertChild(cw, cTypeVue);
                    }
                }
            }
        }

        public override void CWInsert(ControlDoc cw, char cTypeVue)
        {
            if (cTypeVue == '1')
            {
                string sType = GetType().Name.Substring("Draw".Length);
                string sGuid = cTypeVue + GuidkeyObjet.ToString().Replace("-", "");
                string sBook = sType.Substring(0, 3) + sGuid;
                string sVueBookmark = "Diag" + sGuid;
                string sDiagram = F.SaveDiagramFromPath(sVueBookmark, cw.getImagePath(), GuidkeyObjet.ToString());

                if (cw.Exist("n" + sGuid) > -1)
                {
                    cw.InsertTextFromId("n" + sGuid, true, Texte, "Titre 3");

                    cw.InsertTextFromId(sVueBookmark, true, "\n", null);
                    cw.InsertImgFromId(sVueBookmark, false, sDiagram, null);
                    cw.InsertTextFromId(sVueBookmark, false, "\n", null);
                }
                else if (cw.Exist(sType) > -1)
                {
                    //sType ne doit pas depasse 4 caracteres
                    cw.InsertTextFromId(sType, false, "\n", null);
                    cw.CreatIdFromIdP(sBook, sType);
                    cw.InsertTextFromId(sBook, true, Texte + "\n", "Titre 3");
                    cw.CreatIdFromIdP("n" + sGuid, sBook);
                    CWInsertProp(cw, sBook, "P");
                    cw.InsertTextFromId(sBook, false, "Diagram\n", "Titre 6");
                    cw.InsertTextFromId(sBook, false, "\n", null);
                    cw.CreatIdFromIdP(sVueBookmark, sBook);
                    cw.InsertTextFromId(sVueBookmark, true, "\n", null);
                    cw.InsertImgFromId(sVueBookmark, false, sDiagram, null);
                    cw.InsertTextFromId(sVueBookmark, false, "\n", null);

                }
                InsertChild(cw, cTypeVue, sBook, typeof(DrawInterface).ToString().Substring(14, 3) + sGuid, "Interface", typeof(DrawInterface).ToString());
                InsertChild(cw, cTypeVue, sBook, typeof(DrawComposant).ToString().Substring(14, 3) + sGuid, "Componant", typeof(DrawComposant).ToString());
                InsertChild(cw, cTypeVue, sBook, typeof(DrawBase).ToString().Substring(14, 3) + sGuid, "Base", typeof(DrawBase).ToString());
                InsertChild(cw, cTypeVue, sBook, typeof(DrawFile).ToString().Substring(14, 3) + sGuid, "File", typeof(DrawFile).ToString());
                InsertChild(cw, cTypeVue, sBook, typeof(DrawQueue).ToString().Substring(14, 3) + sGuid, "Queue", typeof(DrawQueue).ToString());
            }
        }

        public override DrawArea.DrawToolType GetToolTypeForObjExp()
        {
            return DrawArea.DrawToolType.MainComposant;
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

        public override void savetoDB()
        {
            if (!savetoDBFait())
            {
                string sTypeVue = F.tbTypeVue.Text; // (string)F.cbTypeVue.SelectedItem;
                switch (sTypeVue[0])
                {
                    case '1': // 1-Applicative
                    case 'U': // U-STA
                            base.savetoDB();
                        break;
                }
                savetoDBOK();
            }
        }

        public override string GetKeyComment()
        {
            string sTypeVue = F.tbTypeVue.Text; // (string)F.cbTypeVue.SelectedItem;
            switch (sTypeVue[0])
            {
                case '1': // 1-Applicative
                case 'U': // U-STA
                    return GuidkeyObjet.ToString();
            }
            return GetValueFromName("GuidMainComposant").ToString();
        }

        public override XmlElement savetoXml(XmlDB xmlDB, bool GObj)
        {

            string sTypeVue = F.tbTypeVue.Text; // (string)F.cbTypeVue.SelectedItem;
            switch (sTypeVue[0])
            {
                case '1': // 1-Applicative
                    return base.savetoXml(xmlDB, GObj);
                    break;
                case '2': // 2-Infrastructure

                    string s = GetTypeSimpleTable();

                    if (LstParent != null)
                    {
                        for (int i = 0; i < LstParent.Count; i++) ((DrawObject)LstParent[i]).savetoXml(xmlDB, GObj);
                    }

                    XmlElement elo = XmlCreatObject(xmlDB); // Table Objet
                    if (elo != null)
                    {
                        //MCompApp
                        for (int i = 0; i < LstParent.Count; i++)
                        {
                            System.Xml.XmlElement el;
                            DrawObject o = (DrawObject)LstParent[i];
                            if (o.GetType() == typeof(DrawServer))
                            {
                                el = xmlDB.XmlCreatEl(xmlDB.XmlGetFirstElFromParent(elo, "After"), "MCompApp", "GuidMainComposant,GuidServer");
                                XmlElement elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");
                                xmlDB.XmlSetAttFromEl(elAtts, "GuidMainComposant", "s", (string)LstValue[0]);
                                xmlDB.XmlSetAttFromEl(elAtts, "GuidServer", "s", o.GuidkeyObjet.ToString());
                            }
                            else if (o.GetType() == typeof(DrawTechUser))
                            {
                                el = xmlDB.XmlCreatEl(xmlDB.XmlGetFirstElFromParent(elo, "After"), "AppUserMComp", "GuidMainComposant,GuidAppUser");
                                XmlElement elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");
                                xmlDB.XmlSetAttFromEl(elAtts, "GuidMainComposant", "s", (string)LstValue[0]);
                                xmlDB.XmlSetAttFromEl(elAtts, "GuidAppUser", "s", o.GuidkeyObjet.ToString());               
                            }
                        }
                        return elo;
                    }
                    break;
            }
            return null;
        }
    }
}
