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
    public class DrawApplication : DrawTools.DrawRectangle
    {
        bool bPlateformPattern;

        public bool GetPlateformPattern
        {
            get {
                return bPlateformPattern;
            }

        }
        
        public DrawApplication()
        {
            SetRectangle(0, 0, 1, 1);
            Initialize();
        }

		public DrawApplication(Form1 of, int x, int y, int width, int height,int count)
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
            GuidkeyObjet = Guid.NewGuid();
            Texte = "Application" + count;
            
            Guidkey = Guid.NewGuid();
            InitProp();

            string sguid = "764079ad-621b-4c57-92be-9d1530fb20cb";
            string sVal = "default" + "   (" + sguid + ")";
            bPlateformPattern = SetAppType();
            SetValueFromName("GuidArborescence", (object)sguid);
            //SetValueFromName("NomArborescence", (object)sVal);
            Initialize();
        }

        public DrawApplication(Form1 of, Dictionary<string, object> dic)
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
            bPlateformPattern = SetAppType();

            Initialize();
        }

        public DrawApplication(Form1 of, ArrayList lstVal, ArrayList lstValG)
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
            Guidkey = Guid.NewGuid();

            o = GetValueFromLib("Guid");
            if(o!=null)
                GuidkeyObjet = new Guid((string) o);
            o = GetValueFromLib("Nom");
            if (o != null)
                Texte = (string)o;
            o = GetValueFromName("GuidArborescence");
            if (o != null)
            {
                //object o1 = GetValueFromName("NomArborescence");
                //string s = (string)o1 + "   (" + (string)o + ")";
                //SetValueFromName("NomArborescence", (object)s);
            }
            bPlateformPattern = SetAppType();

            Initialize();
        }

        public DrawApplication(Form1 of, ArrayList lstVal)
        {
            F = of;
            object o = null;

            LstParent = null;
            LstChild = new ArrayList();
            LstLinkIn = new ArrayList();
            LstLinkOut = new ArrayList();
            LstValue = lstVal;

            o = GetValueFromLib("Guid");
            if (o != null)
                GuidkeyObjet = new Guid((string)o);
            o = GetValueFromLib("Nom");
            bPlateformPattern = SetAppType();
        }

        public bool SetAppType()
        {
            string sGuidType = (string)GetValueFromName("GuidApplicationType");
            if (sGuidType == "76664cf9-21bb-4138-83c1-81f4be6fb4fd") return true;
                return false;
        }

        public int NbrServerType()
        {
            int CountObj = 0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawServerType)) CountObj++;
            return CountObj;
        }
        private int HeightServerType()
        {
            int CountObj = 0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawServerType))
                {
                    ((DrawServerType)LstChild[i]).AligneObjet();
                    CountObj += Axe + ((DrawServerType)LstChild[i]).Rectangle.Height;
                }
            return CountObj;
        }

        public void AligneObjet()
        {
            int WidthObjet = Rectangle.Width;
            int HeightServerT = HeightServerType() + imgSmallIconHeight, HeightServerT1 = HeightServerT;


            for (int i = LstChild.Count - 1; i >= 0; i--)
            {
                if (LstChild[i].GetType() == typeof(DrawServerType))
                {
                    ((DrawServerType)LstChild[i]).Aligne(Rectangle.X + image0Width + Axe, Rectangle.Y + HeightServerT - ((DrawServerType)LstChild[i]).Rectangle.Height, WidthObjet - image0Width - 2 * Axe, ((DrawServerType)LstChild[i]).Rectangle.Height);
                    HeightServerT -= ((DrawServerType)LstChild[i]).Rectangle.Height + Axe;
                    ((DrawServerType)LstChild[i]).AligneObjet();
                }
                /*
                else if (LstChild[i].GetType() == typeof(DrawMainComposant))
                {
                    ((DrawMainComposant)LstChild[i]).Aligne(Rectangle.X + Axe, Rectangle.Y + HeightServer + HeightServerT1 + HeightMainComposant - ((DrawMainComposant)LstChild[i]).Rectangle.Height, WidthObjet - 2 * Axe, ((DrawMainComposant)LstChild[i]).Rectangle.Height);
                    HeightMainComposant -= ((DrawMainComposant)LstChild[i]).Rectangle.Height + Axe;
                    ((DrawMainComposant)LstChild[i]).AligneObjet();
                }
                */
            }
        }

        public override void Draw(Graphics g)
        {
            ToolApplication to = (ToolApplication)F.drawArea.tools[(int)DrawArea.DrawToolType.Application];
            TemplateDt oTemplate;
            Pen pen = new Pen(Color.Black, 1), pen1;
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10) {

                string sGuidType = (string)GetValueFromName("GuidApplicationType");
                if (!bPlateformPattern)
                {
                    oTemplate = (TemplateDt)to.oLayers[0].lstTemplate[to.GetTemplate((string)GetValueFromName("GuidLayer"))];

                    pen = new Pen(oTemplate.LineCouleur, oTemplate.LineWidth);
                    pen1 = new Pen(oTemplate.LineCouleur, oTemplate.Line1Width);

                    //AffRec(g, r, LineCouleur, Couleur, Fill, Contour, Arrondi, Ombre, Thickness);
                    AffRec(g, r, oTemplate, 0);
                    g.DrawLine(pen1, r.Left + WidthApplication, r.Top, r.Left + WidthApplication, r.Bottom);
                    g.DrawLine(pen1, r.Left + r.Width - WidthApplication, r.Top, r.Left + r.Width - WidthApplication, r.Bottom);
                    AffIcon(g, r, oTemplate);

                    Color c = oTemplate.Pen1Couleur;
                    if ((string)GetValueFromName("GuidAppVersion") == "") c = oTemplate.Pen2Couleur;
                    DrawGrpTxt(g, 1, 0, r.Left + WidthApplication + Axe, r.Top + r.Height / 2 - 4 * HeightFont[0] / 4, 0, c, oTemplate.BkGrCouleur);
                    DrawGrpTxt(g, 3, 0, r.Left + WidthApplication + Axe, r.Top + r.Height / 2 + 2 * HeightFont[0] / 4, 0, c, oTemplate.BkGrCouleur);
                }
                else
                {
                    oTemplate = (TemplateDt)to.oLayers[1].lstTemplate[to.GetTemplate((string)GetValueFromName("GuidLayer"), 1)];

                    pen = new Pen(oTemplate.LineCouleur, oTemplate.LineWidth);
                    pen1 = new Pen(oTemplate.LineCouleur, oTemplate.Line1Width);

                    //int iWidth = DrawGrpTxt(null, 1, 0, 0, 0, 0, oTemplate.Pen1Couleur, oTemplate.BkGrCouleur);
                    AffRec(g, r, oTemplate, 0);
                    g.DrawLine(pen1, r.Left + WidthApplication, r.Top, r.Left + WidthApplication, r.Bottom);
                    g.DrawLine(pen1, r.Left + r.Width - WidthApplication, r.Top, r.Left + r.Width - WidthApplication, r.Bottom);
                    //g.DrawLine(pen, r.Left + image0Width, r.Top + HeightFont[0], r.Right, r.Top + HeightFont[0]);
                    //g.DrawLine(pen, r.Right, r.Top + HeightFont[0], r.Right, r.Bottom - HeightFont[0]);
                    //g.DrawLine(pen, r.Left + image0Width, r.Bottom - HeightFont[0], r.Right, r.Bottom - HeightFont[0]);
                    AffIcon(g, new Rectangle(r.Left, r.Top, r.Width, r.Height - HeightFont[0]), oTemplate);

                    DrawGrpTxt(g, 1, 0, r.Left, r.Bottom - HeightFont[0] - Axe, 0, oTemplate.Pen1Couleur, oTemplate.BkGrCouleur);
                    AligneObjet();
                }
            } else g.DrawRectangle(pen, r);

            pen.Dispose();
        }

        public override DrawArea.DrawToolType GetToolTypeForObjExp()
        {
            return DrawArea.DrawToolType.Application;
        }

        //public void CreatAppLink(string sGuidLink, string ComposantIn, string ComposantOut)
        public void CreatAppLink(ArrayList LstValueLink)
        {
            DrawApplication daIn = null, daOut = null;
            int Appi = 0, Appj = 0, iSens = -1; // 0: Appi --> App, 1: App --> Appj, 2: Appi--> Appj 
            bool bFind = false;
            int n=0;
            string sGuidLink = "", ComposantIn = "", ComposantOut = "";
            string sNameLink = "GuidLink", sNameIn = "GuidComposantL1In", sNameOut = "GuidComposantL1Out", sPreTable="";
            string sTypeVue = F.tbTypeVue.Text; // (string)F.cbTypeVue.SelectedItem;

            if (sTypeVue[0] == 'W') {sNameLink = "GuidTechLink"; sNameIn = "GuidServerIn"; sNameOut = "GuidServerOut"; sPreTable="Tech";}
            n = F.oCnxBase.ConfDB.FindField(sPreTable + "Link", sNameLink); if (n > -1) sGuidLink = (string)LstValueLink[n];
            n = F.oCnxBase.ConfDB.FindField(sPreTable + "Link", sNameIn); if (n > -1) ComposantIn = (string)LstValueLink[n];
            n = F.oCnxBase.ConfDB.FindField(sPreTable + "Link", sNameOut); if (n > -1) ComposantOut = (string)LstValueLink[n];

            Appi = F.drawArea.GraphicsList.FindObjet(0, ComposantIn);
            if (Appi != -1)
            {
                daIn = (DrawApplication)F.drawArea.GraphicsList[Appi];
                iSens = 0;
            }
            Appj = F.drawArea.GraphicsList.FindObjet(0, ComposantOut);
            if (Appj != -1)
            {
                daOut = (DrawApplication)F.drawArea.GraphicsList[Appj];
                if (iSens != 0)
                {
                    daIn = this;
                    ComposantIn = daIn.GuidkeyObjet.ToString();
                }
            }
            else if (iSens == 0)
            {
                daOut = this;
                ComposantOut = daOut.GuidkeyObjet.ToString();
            }

            if (daIn !=null && daOut != null)
            {
                for (int i = 0; i < F.drawArea.GraphicsList.Count; i++)
                {
                    DrawObject o = (DrawObject)F.drawArea.GraphicsList[i];
                    if (o.GetType() == typeof(DrawLink) || o.GetType() == typeof(DrawTechLink))
                    {
                        if ((string)(o.GetValueFromName("GuidAppIn")) == ComposantIn && ((string)o.GetValueFromName("GuidAppOut")) == ComposantOut)
                        {
                            bFind = true;
                            break;
                        }
                        if(sGuidLink==o.GuidkeyObjet.ToString())
                        //if (oal.LstLinkIn.IndexOf(daIn) != -1 && oal.LstLinkOut.IndexOf(daOut) != -1)
                        {
                            bFind = true;
                            break;
                        }
                    }
                }
                if (!bFind)
                {
                    //F.drawArea.tools[(int)DrawArea.DrawToolType.AppLink].AddNewObjectFromDraw(F.drawArea, new DrawAppLink(F.drawArea.Owner, sGuidLink, daIn, daOut, F.drawArea.GraphicsList.Count), true);
                    if (sTypeVue[0] == 'W') 
                        F.drawArea.tools[(int)DrawArea.DrawToolType.TechLink].AddNewObjectFromDraw(F.drawArea, new DrawTechLink(F.drawArea.Owner, LstValueLink, daIn, daOut, F.drawArea.GraphicsList.Count), true);
                    else
                        F.drawArea.tools[(int)DrawArea.DrawToolType.LinkA].AddNewObjectFromDraw(F.drawArea, new DrawLink(F.drawArea.Owner, LstValueLink, daIn, daOut, F.drawArea.GraphicsList.Count), true);
                    F.drawArea.GraphicsList.MoveLastToBack();
                }
            }
        }

        public override void dataGrid_CellClick(DataGridView odgv, DataGridViewCellEventArgs e)
        {
            //if (odgv.CurrentCell.RowIndex == 2) // Ligne Link Applicatif
            int n;

            n = GetIndexFromName("NomArborescence");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromDB("Select GuidArborescence, NomArborescence From Arborescence", "Value");
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }

            n = GetIndexFromName("NomCadreRefFonc");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.ModeTv();
                fcp.InitTv('F');
                fcp.ShowDialog(F);
            }

            n = GetIndexFromName("GuidLabel");
            if (n > -1 && e.RowIndex == n) // Link Label
            {
                FormLabel fl = new FormLabel(F, odgv);
                fl.AddtvLabelClassFromDB();
                fl.AddlDestinationFromProp();
                //fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fl.ShowDialog(F);
            }

            n = GetIndexFromName("Indicator");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormIndicator fi = new FormIndicator(F, GuidkeyObjet.ToString());
                fi.ShowDialog(F);
            }
            
            n = GetIndexFromName("PWord");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormPropWord fp = new FormPropWord(F, this);
                fp.ShowDialog(F);
            }
            
        }

        public override void VisioDraw(ArrayList lstGuid, ArrayList lstShape, MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
            ToolApplication to = (ToolApplication)F.drawArea.tools[(int)DrawArea.DrawToolType.Application];
            if (lstGuid.IndexOf(GuidkeyObjet.ToString()) == -1)
            {
                if (LstParent != null)
                    for (int ip = 0; ip < LstParent.Count; ip++)
                        if (lstGuid.IndexOf(((DrawObject)LstParent[ip]).GuidkeyObjet.ToString()) == -1)
                            ((DrawObject)LstParent[ip]).VisioDraw(lstGuid, lstShape, page, yPage, qxPage, qyPage);

                //Dessiner l'objet
                MOI.Visio.Shape shape = page.DrawRectangle(Rectangle.Left * qxPage, yPage - Rectangle.Top * qyPage, Rectangle.Right * qxPage, yPage - Rectangle.Bottom * qyPage);

                //Inserer le texte + Couleur + taille
                DrawGrpTxt(shape, 1, 0, 0, 0, 0, Color.Black, Color.Transparent);

                //Couleur trait
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                //Couleur Fond
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillBkgnd).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillForegnd).FormulaU = "RGB(" + Color.White.R.ToString() + "," + Color.White.G.ToString() + "," + Color.White.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillPattern).ResultIU = 28;

                lstShape.Add(shape);
                lstGuid.Add(GuidkeyObjet.ToString());
            }
        }

        public override void savetoDB()
        {

            if (!savetoDBFait())
            {
                base.savetoDB();
                //Label
                SetLabelLinks();
                if (GetPlateformPattern & NbrServerType() == 1)
                {
                    DrawObject oServerType = (DrawObject)LstChild[0];
                    if (F.oCnxBase.CBRecherche("SELECT GuidApplication from PlateformPatternLink where GuidApplication ='" + (string)GetValueFromName("GuidApplication") + "' and GuidVue ='" + F.GuidVue.ToString() + "'"))
                    { // update
                        F.oCnxBase.CBReaderClose();
                        F.oCnxBase.CBWrite(" update PlateformPatternLink set GuidServerType='" + (string)((DrawObject)LstChild[0]).GetValueFromName("GuidServerType") + "' where GuidApplication = '" + (string)GetValueFromName("GuidApplication") + "' and GuidVue = '" + F.GuidVue.ToString() + "'");

                    }
                    else
                    {
                        // create
                        F.oCnxBase.CBReaderClose();
                        F.oCnxBase.CBWrite("insert into PlateformPatternLink (GuidApplication, GuidVue, GuidServerType) VALUES ('" + (string)GetValueFromName("GuidApplication") + "','" + F.GuidVue.ToString() + "','" + (string)((DrawObject)LstChild[0]).GetValueFromName("GuidServerType") + "')");
                    }
                } 
                savetoDBOK();
            }
        }

        public override void CWInsert(ControlDoc cw, char cTypeVue)
        {
            if (cTypeVue == '1')
            {
                string sType = GetType().Name.Substring("Draw".Length);
                string sGuid = cTypeVue + GuidkeyObjet.ToString().Replace("-", "");
                string sBook = sType.Substring(0, 3) + sGuid;
                string sTabBookmark = "Tab" + sGuid;
                if (cw.Exist("n" + sGuid) > -1 )
                {
                    cw.InsertTextFromId("n" + sGuid, true, Texte, "Titre 3");
                }
                else if (cw.Exist(sType) > -1 )
                {
                    //sType ne doit pas depasse 4 caracteres
                    cw.InsertTextFromId(sType, false, "\n", null);
                    cw.CreatIdFromIdP(sBook, sType);
                    cw.InsertTextFromId(sBook, true, Texte + " ["  + GetValueFromName("Trigramme") + "]\n", "Titre 3");
                    cw.CreatIdFromIdP("n" + sGuid, sBook);

                    CWInsertProp(cw, sBook, "P");

                    cw.InsertTextFromId(sBook, false, "Properties\n", "Titre 6");
                    cw.InsertTextFromId(sBook, false, "\n", null);
                    cw.CreatIdFromIdP(sTabBookmark, sBook);
                    cw.InsertTextFromId(sTabBookmark, true, "\n", null);
                    cw.InsertTabFromId(sTabBookmark, false, this, null, false, null);

                    cw.InsertTextFromId(sBook, false, "Flow In\n", "Titre 6");
                    for (int i = 0; i < LstLinkIn.Count; i++) if (LstLinkIn[i].GetType() == typeof(DrawLink)) ((DrawLink)LstLinkIn[i]).CWInsertProp(cw, sBook, "L");

                    cw.InsertTextFromId(sBook, false, "\n", null);
                    cw.InsertTextFromId(sBook, false, "Flow Out\n", "Titre 6");
                    for (int i = 0; i < LstLinkOut.Count; i++) if (LstLinkOut[i].GetType() == typeof(DrawLink)) ((DrawLink)LstLinkOut[i]).CWInsertProp(cw, sBook, "L");

                    //cw.CreatBookmark("n" + sGuid, sType.Substring(0, 3) + sGuid, 2);
                }
            }
        }
    }
}
