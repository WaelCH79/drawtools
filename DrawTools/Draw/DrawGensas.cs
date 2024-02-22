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
	public class DrawGensas : DrawTools.DrawRectangle
	{
		public DrawGensas()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawGensas(Form1 of, Dictionary<string, object> dic)
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

        public DrawGensas(Form1 of, int x, int y, int width, int height, int count)
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
            Texte = "Gensas" + count;
            Guidkey = Guid.NewGuid();

            InitProp();
            SetValueFromName("TypeIt", 1);
            Initialize();
        }

        public DrawGensas(Form1 of, int x, int y, int width, int height, string GuidManagedsvc, string NomManagedsvc, int count)
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
            Texte = "Gensas" + count;
            Guidkey = Guid.NewGuid();

            InitProp();

            SetValueFromName("TypeIt", 1);
            Initialize();
        }

        public DrawGensas(Form1 of, ArrayList lstVal, ArrayList lstValG)
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
                //fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fl.ShowDialog(F);
            }
        }

        public override void Draw(Graphics g)
        {
            ToolGensas tgs = (ToolGensas)F.drawArea.tools[(int)DrawArea.DrawToolType.Gensas];
            TemplateDt oTemplate = (TemplateDt)tgs.oLayers[0].lstTemplate[tgs.GetTemplate((string)GetValueFromName("GuidLayer"))];

            Pen pen = new Pen(tgs.LineCouleur, tgs.LineWidth);
            Pen pen1 = new Pen(tgs.LineCouleur, tgs.Line1Width);
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
                    AffIcon(g, r, oTemplate);
                    string sTypeVue = F.tbTypeVue.Text; // (string)F.cbTypeVue.SelectedItem;
                    if (sTypeVue[0] != 'D')
                    {
                        g.DrawLine(pen1, r.X, r.Y + HeightFont[0] + 2 * Axe, r.X + r.Width, r.Y + HeightFont[0] + 2 * Axe);
                        int iNormeRef = 0;
                        DateTime dDateRef = DateTime.MaxValue;
                        for (int i = 0; i < LstChild.Count; i++)
                        {
                            if (LstChild[i].GetType() == typeof(DrawManagedsvc))
                            {
                                DrawManagedsvc dms = (DrawManagedsvc)LstChild[i];
                                for (int j = 0; j < dms.LstChild.Count; j++)
                                {
                                    if (dms.LstChild[j].GetType() == typeof(DrawTechno))
                                    {
                                        DrawTechno dt = (DrawTechno)dms.LstChild[j];
                                        iNormeRef = Math.Max(iNormeRef, (int)dt.GetValueFromName("Norme"));
                                        if (DateTime.Compare((DateTime)dt.GetValueFromName("ValIndicator"), dDateRef) < 0) dDateRef = (DateTime)dt.GetValueFromName("ValIndicator");
                                    }
                                }
                            }
                        }
                        int hImg = imgSmallIconHeight, wImg = imgSmallIconWidth, idxImg = 0, yImg = r.Top + HeightServer;
                        Form1.ImgList fimglistEng;

                        //Support Status
                        DateTime dtnow = DateTime.Now;
                        TimeSpan ts = new TimeSpan(180, 0, 0, 0);
                        if (DateTime.Compare(dDateRef, dtnow) < 0) fimglistEng = Form1.ImgList.fail;
                        else if (DateTime.Compare(dDateRef - ts, dtnow) < 0) fimglistEng = Form1.ImgList.alert;
                        else fimglistEng = Form1.ImgList.pass;

                        Bitmap ImgSrv = (Bitmap)Image.FromFile(F.sPathRoot + "\\bouton\\" + F.sImgList[(int)fimglistEng], true);

                        if (hImg * ImgSrv.Width / ImgSrv.Height <= wImg)
                            g.DrawImage(ImgSrv, new Rectangle(r.Left + Axe + idxImg * wImg, yImg, hImg * ImgSrv.Width / ImgSrv.Height, hImg));
                        else
                            g.DrawImage(ImgSrv, new Rectangle(r.Left + Axe + idxImg * wImg, yImg, wImg, wImg * ImgSrv.Height / ImgSrv.Width));
                        idxImg++;

                        //Norme Status
                        ImgSrv = (Bitmap)Image.FromFile(F.sPathRoot + "\\bouton\\" + F.sImgList[iNormeRef + (int)Form1.ImgList.Nettbd], true);

                        if (hImg * ImgSrv.Width / ImgSrv.Height <= wImg)
                            g.DrawImage(ImgSrv, new Rectangle(r.Left + Axe + idxImg * wImg + Axe, yImg, hImg * ImgSrv.Width / ImgSrv.Height, hImg));
                        else
                            g.DrawImage(ImgSrv, new Rectangle(r.Left + Axe + idxImg * wImg + Axe, yImg, wImg, wImg * ImgSrv.Height / ImgSrv.Width));
                        idxImg++;
                    }
                }

            }
            else g.DrawRectangle(pen, r);

            pen.Dispose();
        }



        public override void VisioDraw(ArrayList lstGuid, ArrayList lstShape, MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
            ToolGensas tgs = (ToolGensas)F.drawArea.tools[(int)DrawArea.DrawToolType.Gensas];

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
            AligneObjet();
        }

        private int HeightManagedsvc()
        {
            int CountObj = 0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawManagedsvc))
                {
                    ((DrawManagedsvc)LstChild[i]).AligneObjet();
                    CountObj += Axe + ((DrawManagedsvc)LstChild[i]).Rectangle.Height;
                }
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

        private int HeightMComposant()
        {
            int CountObj = 0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawMainComposant))
                {
                    ((DrawMainComposant)LstChild[i]).AligneObjet();
                    CountObj += Axe + ((DrawMainComposant)LstChild[i]).Rectangle.Height;
                }
            return CountObj;
        }

        private int GetYServerType()
        {
            int YObj = 0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawServerType)) if(YObj<((DrawServerType)LstChild[i]).Rectangle.Bottom) YObj=((DrawServerType)LstChild[i]).Rectangle.Bottom;
            return YObj;
        }

        private int NbrManagedsvc()
        {
            int CountObj = 0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawServerType)) CountObj++;
            return CountObj;
        }

        
        public void AligneObjet()
        {
            int HeightMainComposant = HeightMComposant(), WidthObjet = Rectangle.Width; 
            int HeightManagedsvcT = HeightManagedsvc() + imgSmallIconHeight, HeightManagedsvcT1 = HeightManagedsvcT;
            int HeightServerTypeT = HeightServerType() + HeightManagedsvcT1, HeightServerTypeT1 = HeightServerTypeT;

            for (int i = LstChild.Count-1; i >= 0; i--)
            {
                if (LstChild[i].GetType() == typeof(DrawManagedsvc))
                {
                    ((DrawManagedsvc)LstChild[i]).Aligne(Rectangle.X + Axe, Rectangle.Y + HeightServer + HeightManagedsvcT - ((DrawManagedsvc)LstChild[i]).Rectangle.Height, WidthObjet - 2 * Axe, ((DrawManagedsvc)LstChild[i]).Rectangle.Height);
                    HeightManagedsvcT -= ((DrawManagedsvc)LstChild[i]).Rectangle.Height + Axe;
                    ((DrawManagedsvc)LstChild[i]).AligneObjet();
                }
                else if (LstChild[i].GetType() == typeof(DrawServerType))
                {
                    ((DrawServerType)LstChild[i]).Aligne(Rectangle.X + Axe, Rectangle.Y + HeightServer + HeightServerTypeT - ((DrawServerType)LstChild[i]).Rectangle.Height, WidthObjet - 2 * Axe, ((DrawServerType)LstChild[i]).Rectangle.Height);
                    HeightServerTypeT -= ((DrawServerType)LstChild[i]).Rectangle.Height + Axe;
                    ((DrawServerType)LstChild[i]).AligneObjet();
                }
                else if (LstChild[i].GetType() == typeof(DrawMainComposant))
                {
                    ((DrawMainComposant)LstChild[i]).Aligne(Rectangle.X + Axe, Rectangle.Y + HeightServer + HeightServerTypeT1 + HeightMainComposant - ((DrawMainComposant)LstChild[i]).Rectangle.Height, WidthObjet - 2 * Axe, ((DrawMainComposant)LstChild[i]).Rectangle.Height);
                    HeightMainComposant -= ((DrawMainComposant)LstChild[i]).Rectangle.Height + Axe;
                    ((DrawMainComposant)LstChild[i]).AligneObjet();
                }
            }
        }

        
        public void LoadManagedsvc_Techno(string sGuid)
        {
            F.drawArea.tools[(int)DrawArea.DrawToolType.Managedsvc].LoadSimpleObject(sGuid);
            int j = F.drawArea.GraphicsList.FindObjet(0, sGuid);

            DrawManagedsvc dms = (DrawManagedsvc)F.drawArea.GraphicsList[j];

            dms.GuidkeyObjet = Guid.NewGuid(); //Afin de différencier les mêmes ServerType dans une Vue
            AttachLink(dms, DrawObject.TypeAttach.Child);
            dms.AttachLink(this, DrawObject.TypeAttach.Parent);
            //SetValueFromName("GuidExtention", dms.GetValueFromName("GuidManagedsvc"));
        }

        public void LoadMainComposant_ServMComp(string sGuid)
        {
            F.drawArea.tools[(int)DrawArea.DrawToolType.MainComposant].LoadSimpleObject(sGuid);
            int j = F.drawArea.GraphicsList.FindObjet(0, sGuid);


            DrawMainComposant dmc = (DrawMainComposant)F.drawArea.GraphicsList[j];

            dmc.GuidkeyObjet = Guid.NewGuid(); //Afin de différencier les mêmes MainComposant dans une Vue
            AttachLink(dmc, DrawObject.TypeAttach.Child);
            dmc.AttachLink(this, DrawObject.TypeAttach.Parent);

        }


        public override void CWInsert(ControlDoc cw, char cTypeVue)
        {
            if (cTypeVue == '2')
            {
                string sType = GetType().Name.Substring("Draw".Length);
                string sGuid = cTypeVue + GuidkeyObjet.ToString().Replace("-", "");
                string sVueBookmark = "Diag" + sGuid;
                string sBook = sType.Substring(0, 3) + sGuid;
                string sDiagram = F.SaveDiagramFromPath(sVueBookmark, cw.getImagePath(), GuidkeyObjet.ToString());


                if (cw.Exist("n" + sGuid) > -1)
                {
                    cw.InsertTextFromId("n" + sGuid, true, Texte, "Titre 3");
                    cw.InsertTextFromId(sVueBookmark, true, "\n", null);
                    cw.InsertImgFromId(sVueBookmark, false, sDiagram, null);
                    cw.InsertTextFromId(sVueBookmark, false, "\n", null);
                    int n = F.oCnxBase.ConfDB.FindTable("TabTechnoRef");
                    if (n == -1)
                    {
                        F.oCnxBase.ConfDB.AddTabTechnoRef();//Chargement des Tables specifiques
                        n = F.oCnxBase.ConfDB.FindTable("TabTechnoRef");
                    }
                    Table t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                    cw.InsertHeadTabFromId("n" + sBook, true, t, null);
                    for (int i = 0; i < LstChild.Count; i++)
                    {
                        DrawObject o = (DrawObject)LstChild[i];
                        if (o.GetType().ToString() == typeof(DrawTechno).ToString()) o.CWInsertChild(cw, "n" + sBook);
                    }
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

                    cw.InsertTextFromId(sBook, false, "Technologies\n", "Titre 6");
                    for (int i = 0; i < LstChild.Count; i++)
                    {
                        if (LstChild[i].GetType() == typeof(DrawManagedsvc))
                        {
                            DrawManagedsvc dms = (DrawManagedsvc)LstChild[i];
                            dms.CWInsertProp(cw, sBook, "L");
                        }
                    }

                    cw.InsertTextFromId(sBook, false, "\n", null);
                    cw.InsertTextFromId(sBook, false, "Technical Flow In\n", "Titre 6");
                    for (int i = 0; i < LstLinkIn.Count; i++) if (LstLinkIn[i].GetType() == typeof(DrawTechLink)) ((DrawTechLink)LstLinkIn[i]).CWInsertProp(cw, sBook, "L");

                    cw.InsertTextFromId(sBook, false, "\n", null);
                    cw.InsertTextFromId(sBook, false, "Technical Flow Out\n", "Titre 6");
                    for (int i = 0; i < LstLinkOut.Count; i++) if (LstLinkOut[i].GetType() == typeof(DrawTechLink)) ((DrawTechLink)LstLinkOut[i]).CWInsertProp(cw, sBook, "L");

                    cw.InsertTextFromId(sBook, false, "\n", null);
                    cw.InsertTextFromId(sBook, false, "Componant Hosted\n", "Titre 6");
                    cw.InsertTextFromId(sBook, false, "Le serveur héberge les containers suivants:\n", null);
                    for (int i = 0; i < LstChild.Count; i++)
                    {
                        if (LstChild[i].GetType() == typeof(DrawMainComposant))
                        {
                            DrawMainComposant dst = (DrawMainComposant)LstChild[i];
                            cw.InsertTextFromId(sBook, false, dst.Texte + "\n", "Bullet");
                        }
                    }
                    cw.InsertTextFromId(sBook, false, "\n", null);
                    cw.InsertTextFromId(sBook, false, "Technologie Support\n", "Titre 6");
                    cw.InsertTextFromId(sBook, false, "\n", null);
                    cw.CreatIdFromIdP("n" + sBook, sBook);
                    cw.InsertTextFromId("n" + sBook, false, "\n", null);
                    int n = F.oCnxBase.ConfDB.FindTable("TabTechnoRef");
                    if (n == -1)
                    {
                        F.oCnxBase.ConfDB.AddTabTechnoRef();//Chargement des Tables specifiques
                        n = F.oCnxBase.ConfDB.FindTable("TabTechnoRef");
                    }
                    if (n > -1)
                    {
                        Table t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                        cw.InsertHeadTabFromId("n" + sBook, false, t, null);
                    }
                    for (int i = 0; i < LstChild.Count; i++)
                    {
                        DrawObject o = (DrawObject)LstChild[i];
                        if (o.GetType().ToString() == typeof(DrawManagedsvc).ToString())
                        {
                            for (int j = 0; j < o.LstChild.Count; j++)
                            {
                                DrawObject o1 = (DrawObject)o.LstChild[j];
                                if (o1.GetType().ToString() == typeof(DrawTechno).ToString())
                                    o1.CWInsertChild(cw, "n" + sBook);
                            }
                        }
                    }
                }
            }

        }

        public void LoadInsobj_Label(string sGuid)
        {
            F.drawArea.tools[(int)DrawArea.DrawToolType.InfInssas].LoadSimpleObject(sGuid);
            int j = F.drawArea.GraphicsList.FindObjet(0, sGuid);
            
            DrawInfInssas dis = (DrawInfInssas)F.drawArea.GraphicsList[j];

            dis.GuidkeyObjet = Guid.NewGuid(); //Afin de différencier les mêmes ServerPhy dans une Vue
            AttachLink(dis, DrawObject.TypeAttach.Child);
            dis.AttachLink(this, DrawObject.TypeAttach.Parent);
            
        }

        public override string GetSelectExtention()
        {
            return "select GuidExtention, Param.GuidParam, NomParam, AffNomProp from  ManagedsvcLink, Param, ExtentionParamLink Where ManagedsvcLink.GuidManagedsvc = ExtentionParamLink.GuidExtention and ExtentionParamLink.GuidParam = Param.GuidParam and ManagedsvcLink.GuidGensas = '" + GuidkeyObjet + "'";
        }

        public override void savetoDB()
        {

            if (!savetoDBFait())
            {
                base.savetoDB();

                //Managedsvc
                List<string[]> lstMngdTechno = F.oCnxBase.GetListTechno("Managedsvc", GetType().Name.Substring("Draw".Length), this.GuidkeyObjet.ToString());
                List<string> lstManagedsvcLink = F.oCnxBase.GetListObjByObjRef("Managedsvc", GetType().Name.Substring("Draw".Length), this.GuidkeyObjet.ToString());

                //ServerType
                List<string[]> lstTechno = F.oCnxBase.GetListTechno("ServerType", GetType().Name.Substring("Draw".Length), this.GuidkeyObjet.ToString(), "Svc");
                List<string> lstServerTypeLink = F.oCnxBase.GetListObjByObjRef("ServerType", GetType().Name.Substring("Draw".Length), this.GuidkeyObjet.ToString(), "Svc");

                //MainComposant
                List<string[]> lstMCompServ = F.oCnxBase.GetListMCompInfra(GetType().Name.Substring("Draw".Length), this.GuidkeyObjet.ToString(), "MCompSas");
                List<string> lstMCompApp = F.oCnxBase.GetListMCompObjLink(GetType().Name.Substring("Draw".Length), this.GuidkeyObjet.ToString(), "MCompSas");

                //Extention
                SaveExtention();

                if (LstChild != null)
                {
                    for (int i = 0; i < LstChild.Count; i++)
                    {
                        if (LstChild[i].GetType() == typeof(DrawManagedsvc))
                        {
                            DrawManagedsvc dms = (DrawManagedsvc)LstChild[i];
                            if (!F.oCnxBase.ExistGuid(0, dms)) F.oCnxBase.CreatObject(dms); // Table Objet
                            else F.oCnxBase.UpdateObject(dms); // Update de la Table Objet

                            // Recherche dans la liste existante
                            string sSearchManagedsvcLink = lstManagedsvcLink.Find(el => el == (string)dms.GetValueFromName("GuidManagedsvc"));
                            if (sSearchManagedsvcLink == null) F.oCnxBase.CreatObjLink("Managedsvc", (string)dms.LstValue[0], GetType().Name.Substring("Draw".Length), GuidkeyObjet.ToString());
                            else lstManagedsvcLink.Remove(sSearchManagedsvcLink);


                            for (int j = 0; j < dms.LstChild.Count; j++)
                            {
                                if (dms.LstChild[j].GetType() == typeof(DrawTechno))
                                {
                                    DrawTechno dt = (DrawTechno)dms.LstChild[j];
                                    //Recheche dans la liste existante
                                    string[] sSearchTechno = lstMngdTechno.Find(el => el[0] == (string)dt.GetValueFromName("GuidTechno") && el[1] == (string)dms.GetValueFromName("GuidManagedsvc"));
                                    if (sSearchTechno == null && !F.oCnxBase.ExistGuid(0, dt)) F.oCnxBase.CreatObject(dt);
                                    else lstMngdTechno.Remove(sSearchTechno);
                                }
                            }
                        }
                        if (LstChild[i].GetType() == typeof(DrawServerType))
                        {
                            DrawServerType dst = (DrawServerType)LstChild[i];
                            if (!F.oCnxBase.ExistGuid(0, dst)) F.oCnxBase.CreatObject(dst); // Table Objet
                            else F.oCnxBase.UpdateObject(dst); // Update de la Table Objet

                            // Recherche dans la liste existante
                            string sSearchSvcServerTypeLink = lstServerTypeLink.Find(el => el == (string)dst.GetValueFromName("GuidServerType"));
                            if (sSearchSvcServerTypeLink == null) F.oCnxBase.CreatObjLink("ServerType", (string)dst.LstValue[0], GetType().Name.Substring("Draw".Length), GuidkeyObjet.ToString(), "Svc");
                            else lstServerTypeLink.Remove(sSearchSvcServerTypeLink);


                            for (int j = 0; j < dst.LstChild.Count; j++)
                            {
                                if (dst.LstChild[j].GetType() == typeof(DrawTechno))
                                {
                                    DrawTechno dt = (DrawTechno)dst.LstChild[j];
                                    //Recheche dans la liste existante
                                    string[] sSearchTechno = lstTechno.Find(el => el[0] == (string)dt.GetValueFromName("GuidTechno") && el[1] == (string)dst.GetValueFromName("GuidServerType"));
                                    if (sSearchTechno == null && !F.oCnxBase.ExistGuid(0, dt)) F.oCnxBase.CreatObject(dt);
                                    else lstTechno.Remove(sSearchTechno);
                                }
                            }
                        }

                        if (LstChild[i].GetType() == typeof(DrawMainComposant))
                        {
                            DrawMainComposant dmc = (DrawMainComposant)LstChild[i];
                            F.oCnxBase.UpdateObject(dmc);

                            // Recherche dans la liste existante
                            string sSearchMCompApp = lstMCompApp.Find(el => el == (string)dmc.GetValueFromName("GuidMainComposant"));
                            if (sSearchMCompApp == null) F.oCnxBase.CreatMCompSas(this, dmc);
                            else lstMCompApp.Remove(sSearchMCompApp);

                            for (int j = 0; j < dmc.LstChild.Count; j++)
                            {
                                if (dmc.LstChild[j].GetType() == typeof(DrawMCompServ))
                                {
                                    DrawMCompServ dmcs = (DrawMCompServ)dmc.LstChild[j];
                                    //Recheche dans la liste existante
                                    string[] sSearchMCompServ = lstMCompServ.Find(el => el[0] == (string)dmcs.GetValueFromName("GuidMCompServ") && el[1] == (string)dmc.GetValueFromName("GuidMainComposant"));
                                    if (sSearchMCompServ == null && !F.oCnxBase.ExistGuid(0, dmcs)) F.oCnxBase.CreatObject(dmcs); // Table Objet
                                    else lstMCompServ.Remove(sSearchMCompServ);
                                }
                            }
                        }
                    }
                }

                //Suppression des Objets existants
                for (int i = 0; i < lstManagedsvcLink.Count; i++)
                {
                    string[] sSearchTechno = null;
                    do
                    {
                        sSearchTechno = lstTechno.Find(el => el[1] == lstManagedsvcLink[i]);
                        if (sSearchTechno != null) lstTechno.Remove(sSearchTechno);
                    } while (sSearchTechno != null);

                    F.oCnxBase.CBWrite("DELETE FROM ManagedsvcLink WHERE GuidManagedsvc='" + lstManagedsvcLink[i] + "' and GuidGendsas='" + this.GuidkeyObjet + "'");
                }

                for (int i = 0; i < lstTechno.Count; i++)
                    F.oCnxBase.CBWrite("Delete From Techno Where GuidTechno='" + lstTechno[i][0] + "'");

                
                for (int i = 0; i < lstMCompApp.Count; i++)
                {
                    string[] sSearchMCompServ = null;
                    do
                    {
                        sSearchMCompServ = lstMCompServ.Find(el => el[1] == lstMCompApp[i]);
                        if (sSearchMCompServ != null) lstMCompServ.Remove(sSearchMCompServ);
                    } while (sSearchMCompServ != null);

                    F.oCnxBase.CBWrite("DELETE FROM MCompSas WHERE GuidMainComposant='" + lstMCompApp[i] + "' and GuidGensas='" + this.GuidkeyObjet + "'");
                }

                /*
                for (int i = 0; i < lstMCompServ.Count; i++)
                    F.oCnxBase.CBWrite("Delete From MCompServ Where GuidMCompServ='" + lstMCompServ[i][0] + "'");
                */

                //Label
                SetLabelLinks();

                savetoDBOK();
            }
        }

	}
}
