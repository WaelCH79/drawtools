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
	public class DrawGenpod : DrawTools.DrawRectangle
	{
		public DrawGenpod()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawGenpod(Form1 of, Dictionary<string, object> dic)
        {
            F = of;
            object o = null;
            OkMove = true;
            Align = true;

            LstParent = new ArrayList();
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


        public DrawGenpod(Form1 of, int x, int y, int width, int height,int count)
        {
            F = of;
            OkMove = true;
            Align = true;
            Rectangle = new Rectangle(x, y, width, height);
            LstParent = new ArrayList();
            LstChild = new ArrayList();
            LstLinkIn = new ArrayList();
            LstLinkOut = new ArrayList();
            LstValue = new ArrayList();
            GuidkeyObjet = Guid.NewGuid();
            Texte = "Genpod" + count;
            Guidkey = Guid.NewGuid();

            InitProp();
            SetValueFromName("TypeIt", 1);
            Initialize();
        }

        public DrawGenpod(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = true;
            Align = true;
            InitRectangle(lstValG);
            CorrectionRatio();
            LstParent = new ArrayList();
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

            Initialize();
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

        private int HeightContainer()
        {
            int CountObj = 0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawContainer))
                {
                    ((DrawContainer)LstChild[i]).AligneObjet();
                    CountObj += Axe + ((DrawContainer)LstChild[i]).Rectangle.Height;
                }
            return CountObj;
        }

        public void AligneObjet()
        {
            
            int HeightMainComposant = HeightMComposant(), WidthObjet = Rectangle.Width;
            int HeightContainerT = HeightContainer() + imgSmallIconHeight, HeightContainerT1 = HeightContainerT;


            for (int i = LstChild.Count - 1; i >= 0; i--)
            {
                if (LstChild[i].GetType() == typeof(DrawContainer))
                {
                    ((DrawContainer)LstChild[i]).Aligne(Rectangle.X + Axe, Rectangle.Y + HeightServer + HeightContainerT - ((DrawContainer)LstChild[i]).Rectangle.Height, WidthObjet - 2 * Axe, ((DrawContainer)LstChild[i]).Rectangle.Height);
                    HeightContainerT -= ((DrawContainer)LstChild[i]).Rectangle.Height + Axe;
                    ((DrawContainer)LstChild[i]).AligneObjet();
                }
                else if (LstChild[i].GetType() == typeof(DrawMainComposant))
                {
                    ((DrawMainComposant)LstChild[i]).Aligne(Rectangle.X + Axe, Rectangle.Y + HeightServer + HeightContainerT1 + HeightMainComposant - ((DrawMainComposant)LstChild[i]).Rectangle.Height, WidthObjet - 2 * Axe, ((DrawMainComposant)LstChild[i]).Rectangle.Height);
                    HeightMainComposant -= ((DrawMainComposant)LstChild[i]).Rectangle.Height + Axe;
                    ((DrawMainComposant)LstChild[i]).AligneObjet();
                }
            }
        }

        public override void dataGrid_CellClick(DataGridView odgv, DataGridViewCellEventArgs e)
        {
            //if (odgv.CurrentCell.RowIndex == 2) // Ligne Link Applicatif
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
            ToolGenpod tgp = (ToolGenpod)F.drawArea.tools[(int)DrawArea.DrawToolType.Genpod];
            TemplateDt oTemplate = (TemplateDt)tgp.oLayers[0].lstTemplate[tgp.GetTemplate((string)GetValueFromName("GuidLayer"))];

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

        public override bActions MajDelActions(bActions stActions)
        {
            if (!stActions.delBase)
            {
                stActions.bConfirmation = false;
                stActions.delBase = true;
                stActions.delObjGraphique = true;
                stActions.delObjVue = true;
            }
            return stActions;
        }

        public override void VisioDraw(ArrayList lstGuid, ArrayList lstShape, MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
            ToolGenpod to = (ToolGenpod)F.drawArea.tools[(int)DrawArea.DrawToolType.Genpod];

            if (lstGuid.IndexOf(GuidkeyObjet.ToString()) == -1)
            {
                if (LstParent != null)
                    for (int ip = 0; ip < LstParent.Count; ip++)
                        if (lstGuid.IndexOf(((DrawObject)LstParent[ip]).GuidkeyObjet.ToString()) == -1)
                            ((DrawObject)LstParent[ip]).VisioDraw(lstGuid, lstShape, page, yPage, qxPage, qyPage);


                //Dessiner l'objet
                //MOI.Visio.Shape shape = page.InsertFromFile(@"C:\Dat\bouton\file3.gif", (short)MOI.Visio.VisInsertObjArgs.visInsertLink);
                MOI.Visio.Shape shape = page.Import(F.sPathRoot + @"\bouton\db.png");
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
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                //Couleur Fond
                //shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillForegnd).FormulaU = "RGB(" + Color.Yellow.R.ToString() + "," + Color.Yellow.G.ToString() + "," + Color.Yellow.B.ToString() + ")";
                //Arrondi
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineRounding).FormulaU = "3 mm";

                lstShape.Add(shape);
                lstGuid.Add(GuidkeyObjet.ToString());
            }
        }
        public override void MoveHandleTo(Point point, int handleNumber)
        {
            base.MoveHandleTo(point, handleNumber);
            AligneObjet();
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

        public void LoadContainer_Techno(string sGuid)
        {
            F.drawArea.tools[(int)DrawArea.DrawToolType.Container].LoadSimpleObject(sGuid);
            int j = F.drawArea.GraphicsList.FindObjet(0, sGuid);

            DrawContainer dc = (DrawContainer)F.drawArea.GraphicsList[j];

            dc.GuidkeyObjet = Guid.NewGuid(); //Afin de différencier les mêmes ServerType dans une Vue
            AttachLink(dc, DrawObject.TypeAttach.Child);
            dc.AttachLink(this, DrawObject.TypeAttach.Parent);
        }

        


        public override void savetoDB()
        {

            if (!savetoDBFait())
            {
                base.savetoDB();

                //Container
                List<string[]> lstTechno = F.oCnxBase.GetListTechno("Container", GetType().Name.Substring("Draw".Length), this.GuidkeyObjet.ToString());
                List<string> lstContainerLink = F.oCnxBase.GetListObjByObjRef("Container", GetType().Name.Substring("Draw".Length), this.GuidkeyObjet.ToString());

                //MainComposant
                List<string[]> lstMCompServ = F.oCnxBase.GetListMCompInfra(GetType().Name.Substring("Draw".Length), this.GuidkeyObjet.ToString(), "MComppod");
                List<string> lstMCompApp = F.oCnxBase.GetListMCompObjLink(GetType().Name.Substring("Draw".Length), this.GuidkeyObjet.ToString(), "MComppod");

                //Label
                SetLabelLinks();


                if (LstChild != null)
                {
                    for (int i = 0; i < LstChild.Count; i++)
                    {
                        if (LstChild[i].GetType() == typeof(DrawContainer))
                        {
                            DrawContainer dc = (DrawContainer)LstChild[i];
                            if (!F.oCnxBase.ExistGuid(0, dc)) F.oCnxBase.CreatObject(dc); // Table Objet
                            else F.oCnxBase.UpdateObject(dc); // Update de la Table Objet

                            // Recherche dans la liste existante
                            string sSearchContainerLink = lstContainerLink.Find(el => el == (string)dc.GetValueFromName("GuidContainer"));
                            if (sSearchContainerLink == null) F.oCnxBase.CreatObjLink("Container", (string)dc.LstValue[0], GetType().Name.Substring("Draw".Length), GuidkeyObjet.ToString());
                            else lstContainerLink.Remove(sSearchContainerLink);


                            for (int j = 0; j < dc.LstChild.Count; j++)
                            {
                                if (dc.LstChild[j].GetType() == typeof(DrawTechno))
                                {
                                    DrawTechno dt = (DrawTechno)dc.LstChild[j];
                                    //Recheche dans la liste existante
                                    string[] sSearchTechno = lstTechno.Find(el => el[0] == (string)dt.GetValueFromName("GuidTechno") && el[1] == (string)dc.GetValueFromName("GuidContainer"));
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
                            string sSearchMCompApp = lstMCompApp.Find(el => el == (string) dmc.GetValueFromName("GuidMainComposant"));
                            if (sSearchMCompApp == null) F.oCnxBase.CreatMCompPod(this, dmc);
                            else lstMCompApp.Remove(sSearchMCompApp);

                            for (int j = 0; j < dmc.LstChild.Count; j++)
                            {
                                if (dmc.LstChild[j].GetType() == typeof(DrawMCompServ))
                                {
                                    DrawMCompServ dmcs = (DrawMCompServ)dmc.LstChild[j];
                                    //Recheche dans la liste existante
                                    string [] sSearchMCompServ = lstMCompServ.Find(el => el[0] == (string) dmcs.GetValueFromName("GuidMCompServ") && el[1] == (string)dmc.GetValueFromName("GuidMainComposant"));
                                    if (sSearchMCompServ == null && !F.oCnxBase.ExistGuid(0, dmcs)) F.oCnxBase.CreatObject(dmcs); // Table Objet
                                    else lstMCompServ.Remove(sSearchMCompServ);
                                }
                            }
                        }
                    }
                }

                //Suppression des Objets existants
                for (int i = 0; i < lstContainerLink.Count; i++)
                {
                    string[] sSearchTechno = null;
                    do
                    {
                        sSearchTechno = lstTechno.Find(el => el[1] == lstContainerLink[i]);
                        if (sSearchTechno != null) lstTechno.Remove(sSearchTechno);
                    } while (sSearchTechno != null);

                    F.oCnxBase.CBWrite("DELETE FROM ContainerLink WHERE GuidContainer='" + lstContainerLink[i] + "' and GuidGenpod='" + this.GuidkeyObjet + "'");
                }

                for (int i = 0; i < lstTechno.Count; i++)
                    F.oCnxBase.CBWrite("Delete From Techno Where GuidTechno='" + lstTechno[i][0] + "'");

                /*
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
                    */
                savetoDBOK();
            }
        }

        public override string CWInsertChild(ControlDoc cw, char cTypeVue)
        {
            string sBook = base.CWInsertChild(cw, cTypeVue);
            string sBookContainer = "c" + sBook;

            if (cw.Exist(sBook) > -1)
            {
                //cw.InsertTextFromId(sBook, false, "\n", null);
                cw.InsertTextFromId(sBook, false, "Containers\n", "Titre 6");
                cw.InsertTextFromId(sBook, false, "\n", null);

                cw.CreatIdFromIdP(sBookContainer, sBook);
                cw.InsertTextFromId(sBook, false, "Technologies\n", "Titre 6");
                for (int i = 0; i < LstChild.Count; i++)
                {
                    if (LstChild[i].GetType() == typeof(DrawContainer))
                    {
                        DrawContainer dc = (DrawContainer)LstChild[i];
                        cw.InsertTextFromId("c" + sBook, false, dc.GetValueFromName("NomContainer") + "\n", "Titre 7");
                        cw.InsertTabFromId("c" + sBook, false, dc, null, false, null);
                        //cw.InsertTextFromId("c" + sBook, false, "\n", null);
                        dc.CWInsertProp(cw, sBook, "L");
                    }
                }

                
                cw.InsertTextFromId(sBook, false, "\n", null);
                cw.InsertTextFromId(sBook, false, "Componant Hosted\n", "Titre 6");
                cw.InsertTextFromId(sBook, false, "Le pod héberge les composants suivants:\n", null);
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
                    if (o.GetType().ToString() == typeof(DrawContainer).ToString())
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
            return sBook;

        }
    }
}
