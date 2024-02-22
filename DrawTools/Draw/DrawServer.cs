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
	public class DrawServer : DrawTools.DrawRectangle
	{
		public DrawServer()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawServer(Form1 of, Dictionary<string, object> dic)
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

            Initialize();
        }

        public DrawServer(Form1 of, int x, int y, int width, int height, int count)
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
            Texte = "Server" + count;
            Guidkey = Guid.NewGuid();

            InitProp();
            Initialize();
        }

        public DrawServer(Form1 of, int x, int y, int width, int height, string GuidFonction, string NomFonction, int count)
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
            Texte = "Server" + count;
            Guidkey = Guid.NewGuid();

            InitProp();

            string sVal = NomFonction + "   (" + GuidFonction + ")";
            SetValueFromName("NomFonction", (object)sVal);
            SetValueFromName("GuidMainFonction", (object)GuidFonction);

            Initialize();
        }

        public DrawServer(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = true;
            Align = true;
            InitRectangle(lstValG);
            CorrectionRatio();
            Temporaire = F.bTemporaire;
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
            o = GetValueFromName("GuidMainFonction");
            if (o != null)
            {
                //object o1 = GetValueFromName("NomFonction");
                //string s = (string)o1 + "   (" + (string)o + ")";
                //SetValueFromName("NomFonction", (object)s);
            }

            Initialize();
        }

        public override void Draw(Graphics g)
        {
            ToolServer to = (ToolServer)F.drawArea.tools[(int)DrawArea.DrawToolType.Server];
            Pen pen = new Pen(to.LineCouleur, to.LineWidth);
            Pen pen1 = new Pen(to.LineCouleur, to.Line1Width);
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10)
            {
                AffRec(g, r, to, 0);
                if (F.bPtt)
                {
                    DrawGrpTxt(g, 4, 0, r.Left + Axe, r.Top, 0, to.Pen1Couleur, to.BkGrCouleur);
                }
                else
                {
                    DrawGrpTxt(g, 1, 0, r.Left + Axe, r.Top, 0, to.Pen1Couleur, to.BkGrCouleur);
                    string sTypeVue = F.tbTypeVue.Text; // (string)F.cbTypeVue.SelectedItem;
                    if (sTypeVue[0] != 'D')
                    {
                        g.DrawLine(pen1, r.X, r.Y + HeightFont[0] + 2 * Axe, r.X + r.Width, r.Y + HeightFont[0] + 2 * Axe);
                        int iNormeRef = 0;
                        DateTime dDateRef = DateTime.MaxValue;
                        for (int i = 0; i < LstChild.Count; i++)
                        {
                            if (LstChild[i].GetType() == typeof(DrawServerType))
                            {
                                DrawServerType dst = (DrawServerType)LstChild[i];
                                for (int j = 0; j < dst.LstChild.Count; j++)
                                {
                                    if (dst.LstChild[j].GetType() == typeof(DrawTechno))
                                    {
                                        DrawTechno dt = (DrawTechno)dst.LstChild[j];
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
            ToolServer to = (ToolServer)F.drawArea.tools[(int)DrawArea.DrawToolType.Server];

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
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                //Couleur Fond
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillBkgnd).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
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
            AligneObjet();
        }

        public override void CreatNewGuid()
        {
            Guid newGuid = Guid.NewGuid();
            GuidkeyObjet = newGuid;
            SetValueFromName("GuidServer", newGuid.ToString());
            Temporaire = false;
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

        private int HeightInfServer()
        {
            int CountObj = 0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawInfServer))
                {
                    ((DrawInfServer)LstChild[i]).AligneObjet();
                    CountObj += Axe + ((DrawInfServer)LstChild[i]).Rectangle.Height;
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

        private int NbrServerType()
        {
            int CountObj = 0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawServerType)) CountObj++;
            return CountObj;
        }

        public override void dataGrid_CellClick(DataGridView odgv, DataGridViewCellEventArgs e)
        {
            int n;

            n = GetIndexFromName("NomFonction");
            if (n > -1 && e.RowIndex == n) // Service/protole
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromTv("FonctionServer");
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
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
        }

        public void AligneObjet()
        {
            int HeightMainComposant = HeightMComposant(), WidthObjet = Rectangle.Width; 
            int HeightServerT = HeightServerType() + HeightInfServer() + imgSmallIconHeight, HeightServerT1 = HeightServerT;


            for (int i = LstChild.Count-1; i >= 0; i--)
            {
                if (LstChild[i].GetType() == typeof(DrawServerType))
                {
                    ((DrawServerType)LstChild[i]).Aligne(Rectangle.X + Axe, Rectangle.Y + HeightServer + HeightServerT - ((DrawServerType)LstChild[i]).Rectangle.Height, WidthObjet - 2 * Axe, ((DrawServerType)LstChild[i]).Rectangle.Height);
                    HeightServerT -= ((DrawServerType)LstChild[i]).Rectangle.Height + Axe;
                    ((DrawServerType)LstChild[i]).AligneObjet();
                }
                else if (LstChild[i].GetType() == typeof(DrawInfServer))
                {
                    ((DrawInfServer)LstChild[i]).Aligne(Rectangle.X + Axe, Rectangle.Y + HeightServer + HeightServerT - ((DrawInfServer)LstChild[i]).Rectangle.Height, WidthObjet - 2 * Axe, ((DrawInfServer)LstChild[i]).Rectangle.Height);
                    HeightServerT -= ((DrawInfServer)LstChild[i]).Rectangle.Height + 3 * Axe;
                    ((DrawInfServer)LstChild[i]).AligneObjet();
                }
                else if (LstChild[i].GetType() == typeof(DrawMainComposant))
                {
                    ((DrawMainComposant)LstChild[i]).Aligne(Rectangle.X + Axe, Rectangle.Y + HeightServer + HeightServerT1 + HeightMainComposant - ((DrawMainComposant)LstChild[i]).Rectangle.Height, WidthObjet - 2 * Axe, ((DrawMainComposant)LstChild[i]).Rectangle.Height);
                    HeightMainComposant -= ((DrawMainComposant)LstChild[i]).Rectangle.Height + Axe;
                    ((DrawMainComposant)LstChild[i]).AligneObjet();
                }
            }
        }

        public void LoadServerPhy_NCard(string sGuid)
        {
            F.drawArea.tools[(int)DrawArea.DrawToolType.InfServer].LoadSimpleObject(sGuid);
            int j = F.drawArea.GraphicsList.FindObjet(0, sGuid);

            DrawInfServer dis = (DrawInfServer)F.drawArea.GraphicsList[j];

            dis.GuidkeyObjet = Guid.NewGuid(); //Afin de différencier les mêmes ServerPhy dans une Vue
            AttachLink(dis, DrawObject.TypeAttach.Child);
            dis.AttachLink(this, DrawObject.TypeAttach.Parent);
        }

        /*
        public void LoadServerType_Techno(string sGuid)
        {
            F.drawArea.tools[(int)DrawArea.DrawToolType.ServerType].LoadSimpleObject(sGuid);
            int j = F.drawArea.GraphicsList.FindObjet(0, sGuid);

            DrawServerType dst = (DrawServerType)F.drawArea.GraphicsList[j];

            dst.GuidkeyObjet = Guid.NewGuid(); //Afin de différencier les mêmes ServerType dans une Vue
            AttachLink(dst, DrawObject.TypeAttach.Child);
            dst.AttachLink(this, DrawObject.TypeAttach.Parent);
        }
        */

        public override void savetoDB()
        {
            if (!savetoDBFait())
            {
                base.savetoDB();
                //ServerTypeServer
                //F.oCnxBase.DeleteServerTypeLink(this);

                List<string[]> lstTechno = F.oCnxBase.GetListTechno("ServerType", GetType().Name.Substring("Draw".Length), this.GuidkeyObjet.ToString());
                List<string> lstServerTypeLink = F.oCnxBase.GetListObjByObjRef("ServerType", GetType().Name.Substring("Draw".Length), this.GuidkeyObjet.ToString());

                //MainComposant
                //F.oCnxBase.DeleteMCompApp(this);

                List<string[]> lstMCompServ = F.oCnxBase.GetListMCompInfra(GetType().Name.Substring("Draw".Length), this.GuidkeyObjet.ToString());
                List<string> lstMCompApp = F.oCnxBase.GetListMCompObjLink(GetType().Name.Substring("Draw".Length), this.GuidkeyObjet.ToString());

                if (LstChild != null)
                {
                    for (int i = 0; i < LstChild.Count; i++)
                    {
                        if (LstChild[i].GetType() == typeof(DrawServerType))
                        {
                            DrawServerType dst = (DrawServerType)LstChild[i];
                            if (!F.oCnxBase.ExistGuid(0, dst)) F.oCnxBase.CreatObject(dst); // Table Objet
                            else F.oCnxBase.UpdateObject(dst); // Update de la Table Objet

                            // Recherche dans la liste existante
                            string sSearchServerTypeLink = lstServerTypeLink.Find(el => el == (string)dst.GetValueFromName("GuidServerType"));
                            if (sSearchServerTypeLink == null) F.oCnxBase.CreatObjLink("ServerType", (string)dst.LstValue[0], GetType().Name.Substring("Draw".Length), GuidkeyObjet.ToString());
                            else lstServerTypeLink.Remove(sSearchServerTypeLink);


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
                            if (sSearchMCompApp == null) F.oCnxBase.CreatMCompApp(this, dmc);
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
                for (int i = 0; i < lstServerTypeLink.Count; i++)
                {
                    string[] sSearchTechno = null;
                    do
                    {
                        sSearchTechno = lstTechno.Find(el => el[1] == lstServerTypeLink[i]);
                        if (sSearchTechno != null) lstTechno.Remove(sSearchTechno);
                    } while (sSearchTechno != null);

                    F.oCnxBase.CBWrite("DELETE FROM ServerTypeLink WHERE GuidServerType='" + lstServerTypeLink[i] + "' and GuidServer='" + this.GuidkeyObjet + "'");
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

                    F.oCnxBase.CBWrite("DELETE FROM MCompApp WHERE GuidMainComposant='" + lstMCompApp[i] + "' and GuidServer='" + this.GuidkeyObjet + "'");
                }

                for (int i = 0; i < lstMCompServ.Count; i++)
                    F.oCnxBase.CBWrite("Delete From MCompServ Where GuidMCompServ='" + lstMCompServ[i][0] + "'");

                //Label
                SetLabelLinks();

                savetoDBOK();
            }
        }


        public override void CWInsert(ControlDoc cw, char cTypeVue)
        {
            string sBootRoot="";
            if (cTypeVue == '2') sBootRoot = GetType().Name.Substring("Draw".Length);
            else if (cTypeVue == 'I')
            {
                Guid guid = F.GuidVue;
                sBootRoot = "Ser" + guid.ToString().Replace("-", "");
            }
            if (cTypeVue == '2' || cTypeVue=='I')
            {
                string sType = GetType().Name.Substring("Draw".Length);
                string sGuid = cTypeVue + GuidkeyObjet.ToString().Replace("-", "");
                string sVueBookmark = "Diag" + sGuid;
                string sBook = sType.Substring(0, 3) + sGuid;
                string sDiagram = F.SaveDiagramFromPath(sVueBookmark, cw.getImagePath(), GuidkeyObjet.ToString());
                ArrayList aValue = new ArrayList();
                aValue = GetValueEtCache((string)GetValueFromName("NomFonction"));

                if (cw.Exist("n" + sGuid) > -1)
                {
                    cw.InsertTextFromId("n" + sGuid, true, (string)aValue[0], "Titre 3");
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
                        if (o.GetType().ToString() == typeof(DrawTechno).ToString()) o.CWInsert(cw, cTypeVue);
                    }
                }
                else if (cw.Exist(sBootRoot) > -1)
                {
                    //sType ne doit pas depasse 4 caracteres
                    cw.InsertTextFromId(sBootRoot, false, "\n", null);
                    cw.CreatIdFromIdP(sBook, sBootRoot);
                    cw.InsertTextFromId(sBook, true, aValue[0] + "\n", "Titre 3");
                    cw.CreatIdFromIdP("n" + sGuid, sBook);
                    CWInsertProp(cw, sBook, "P");

                    cw.InsertTextFromId(sBook, false, "Diagram\n", "Titre 6");
                    cw.InsertTextFromId(sBook, false, "\n", null);
                    cw.CreatIdFromIdP(sVueBookmark, sBook);
                    cw.InsertTextFromId(sVueBookmark, true, "\n", null);
                    cw.InsertImgFromId(sVueBookmark, false, sDiagram, null);
                    cw.InsertTextFromId(sVueBookmark, false, "\n", null);

                    cw.InsertTextFromId(sBook, false, "Operating System\n", "Titre 6");
                    for (int i = 0; i < LstChild.Count; i++)
                    {
                        if (LstChild[i].GetType() == typeof(DrawServerType))
                        {
                            DrawServerType dst = (DrawServerType)LstChild[i];
                            if ((string)dst.GetValueFromName("GuidFonction") == "3c088931-c0ab-48f1-8d9c-b436b4e1d716") //socle
                                dst.CWInsertProp(cw, sBook, "L");
                        }
                    }

                    cw.InsertTextFromId(sBook, false, "Technologies\n", "Titre 6");
                    for (int i = 0; i < LstChild.Count; i++)
                    {
                        if (LstChild[i].GetType() == typeof(DrawServerType))
                        {
                            DrawServerType dst = (DrawServerType)LstChild[i];
                            if ((string)dst.GetValueFromName("GuidFonction") != "3c088931-c0ab-48f1-8d9c-b436b4e1d716") //socle
                                dst.CWInsertProp(cw, sBook, "L");
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
                    int n =  F.oCnxBase.ConfDB.FindTable("TabTechnoRef");
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
                        if (o.GetType().ToString() == typeof(DrawServerType).ToString())
                        {
                            for (int j = 0; j < o.LstChild.Count; j++)
                            {
                                DrawObject o1 = (DrawObject)o.LstChild[j];
                                if (o1.GetType().ToString() == typeof(DrawTechno).ToString())
                                    o1.CWInsertChild(cw, "n" + sBook);
                                    //o1.CWInsert(cw, cTypeVue);
                            }                            
                        }
                    }
                    //InsertChild(cw, cTypeVue, sBook, typeof(DrawTechno).ToString().Substring(14, 3) + sGuid, "Techno", typeof(DrawTechno).ToString());
                    //cw.CreatBookmark("n" + sGuid, sType.Substring(0, 3) + sGuid, 2);
                }
            }
        }
	}
}
