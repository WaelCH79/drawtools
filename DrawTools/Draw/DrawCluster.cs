using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using MOI = Microsoft.Office.Interop;
 

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawCluster : DrawTools.DrawRectangle
	{
		public DrawCluster()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawCluster(Form1 of, int x, int y, int width, int height, int count)
        {
            F = of;
            OkMove = true;
            Align = true;
            Rectangle = new Rectangle(x, y, width, height);
            ModeGraphic = modeGraphic.detail;
            LstParent = null; 
            LstChild = new ArrayList();
            LstLinkIn = null;
            LstLinkOut = new ArrayList();
            LstValue = new ArrayList();
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
            Texte = "Cluster" + count;


            InitProp();
            Initialize();
        }

        public DrawCluster(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = true;
            Align = true;
            ModeGraphic = modeGraphic.detail;
            InitRectangle(lstValG);
            LstParent = null;
            LstChild = new ArrayList();
            LstLinkIn = null;
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

        public int NbrNCard()
        {
            int CountObj = 0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawNCard)) CountObj++;

            return CountObj;
        }

        public int NbrServerPhy()
        {
            int CountObj = 0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawServerPhy)) CountObj++;

            return CountObj;
        }

        public void LoadServerPhy(string sGuid)
        {
            int j = F.drawArea.GraphicsList.FindObjet(0, sGuid);
            if (j < 0)
            {
                F.drawArea.tools[(int)DrawArea.DrawToolType.ServerPhy].LoadSimpleObject(sGuid);
                j = F.drawArea.GraphicsList.FindObjet(0, sGuid);

                if (j > -1)
                {
                    DrawServerPhy dsp = (DrawServerPhy)F.drawArea.GraphicsList[j];

                    AttachLink(dsp, DrawObject.TypeAttach.Child);
                    dsp.AttachLink(this, DrawObject.TypeAttach.Parent);
                }
            }
        }

        public override int GetTopYNCard()
        {
            return YMin() + HeightFont[0] * 2 + Axe;
        }

        public override int GetBottomYNCard()
        {
            return YMax()- Axe;
        }

        public void AligneObjet()
        {
            int WidthServerMin = 110;
            
            string sTypeVue = F.tbTypeVue.Text; // (string)F.cbTypeVue.SelectedItem;
            int iEspace = 3 * Axe;
            int nbrServerparLigne=0, nbrLigne=0;


            int CountNCard = NbrNCard(), CountServerPhy = NbrServerPhy();
            int WidthNCard = 0, HeightNCard = 0, WidthServerPhy = 0, HeightServerPhy = 0;
            int TopOfServerPhy = HeightCard + 2 * Axe + HeightFont[0] * 2;
            if (CountNCard != 0) { WidthNCard = (Rectangle.Width - Axe) / CountNCard; HeightNCard = HeightCard - 2 * Axe; }
            if (CountServerPhy != 0) {
                nbrServerparLigne = (Rectangle.Width - iEspace) / WidthServerMin;
                if (nbrServerparLigne == 0) nbrServerparLigne++;
                nbrLigne = CountServerPhy / nbrServerparLigne;
                if (CountServerPhy % nbrServerparLigne != 0) nbrLigne++;
                if (nbrLigne > 1) WidthServerPhy = WidthServerMin; else WidthServerPhy = (Rectangle.Width - iEspace) / CountServerPhy;
                HeightServerPhy = (Rectangle.Height - iEspace - TopOfServerPhy) / nbrLigne;
                if (HeightServerPhy < iEspace) HeightServerPhy = iEspace;
            }

            int xServer = 0, yServer = 0, xNcard = 0;
            for (int i = 0; i < LstChild.Count; i++)
            {
                if (LstChild[i].GetType() == typeof(DrawNCard))
                {
                    ((DrawNCard)LstChild[i]).Aligne(Rectangle.Left + WidthNCard * xNcard + Axe, GetTopYNCard(), WidthNCard - Axe, HeightCard);
                    xNcard++;
                }
                else if (LstChild[i].GetType() == typeof(DrawServerPhy))
                {
                    if (HeightServerPhy<= iEspace || WidthServerPhy<=iEspace)
                        ((DrawServerPhy)LstChild[i]).Aligne(Rectangle.Left, Rectangle.Top, 2, 2);
                    else 
                        ((DrawServerPhy)LstChild[i]).Aligne(Rectangle.Left + WidthServerPhy * xServer + iEspace, Rectangle.Top + TopOfServerPhy + HeightServerPhy * yServer + iEspace, WidthServerPhy - iEspace, HeightServerPhy - iEspace);
                    if(xServer >= (nbrServerparLigne -1)) { yServer++; xServer = 0; } else xServer++;
                }
            }
            
        }

        public override bool AttachPointInObject(Point point)
        {
            return false;
        }

        public override void Draw(Graphics g)
        {
            ToolCluster to = (ToolCluster)F.drawArea.tools[(int)DrawArea.DrawToolType.Cluster];
            
            Pen pen = new Pen(to.LineCouleur, to.LineWidth);
            Rectangle r;
            
            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10) {
                AffRec(g, r, to);
                DrawGrpTxt(g, 2, 0, r.Left + Axe, r.Top, 0, to.Pen1Couleur, to.BkGrCouleur);
            } 

            pen.Dispose();
        }

        public void AddNcard()
        {
            ArrayList lstNCard = F.oCnxBase.CreatNcardCluster(this);

            for (int i = 0; i < lstNCard.Count; i++)
            {
                F.drawArea.tools[(int)DrawArea.DrawToolType.NCard].LoadSimpleObject((string)lstNCard[i]);
                int j = F.drawArea.GraphicsList.FindObjet(0, (string)lstNCard[i]);
                DrawNCard dnc = (DrawNCard)F.drawArea.GraphicsList[j];
                AttachLink(dnc, DrawObject.TypeAttach.Child);
                dnc.AttachLink(this, DrawObject.TypeAttach.Parent);
            }
        }

        public override void dataGrid_CellClick(DataGridView odgv, DataGridViewCellEventArgs e)
        {
            int n;

            n = GetIndexFromName("NCard");
            if (n > -1 && e.RowIndex == n)
                AddNcard();

            n = GetIndexFromName("GuidServerPhy");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromDB("Select Distinct GuidServerPhy, NomServerPhy From ServerPhy Order by NomServerPhy", "Objet");
                fcp.AddlDestinationFromLstChild(this,"DrawServerPhy");
                fcp.ShowDialog(F);
            }

            n = GetIndexFromName("GuidAppUser");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromTv("00PatternUser");
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }

            n = GetIndexFromName("GuidApplication");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromTv("01PatternApplication");
                fcp.AddlDestinationFromChild(this, "ServerPhy");
                fcp.ShowDialog(F);
            }

            n = GetIndexFromName("GuidServer");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormDeployComposant fdc = new FormDeployComposant(F, odgv);
                fdc.AddlSourceFromDB("Select Server.GuidServer, NomServer From Server, Vue, DansVue, GServer Where Vue.GuidVue='" + F.sGuidVueInf + "'AND Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGServer AND GServer.GuidServer=Server.GuidServer");
                //fdc.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fdc.AddlDestinationFromChild(this, "ServerPhy");
                fdc.InitPackage(GuidkeyObjet, Texte);
                fdc.init();
            }
        }

        public override void VisioDraw(ArrayList lstGuid, ArrayList lstShape, MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
            ToolCluster to = (ToolCluster)F.drawArea.tools[(int)DrawArea.DrawToolType.Cluster];

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
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillPattern).ResultIU = 37;
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

        public string FindServerFromTv()
        {
            string ServerInTV = "";
            if (F.oCnxBase.CBRecherche("Select GuidServerPhy, Type From ServerPhy Where GuidHost='" + GuidkeyObjet + "'"))
            {
                while (F.oCnxBase.Reader.Read())
                {
                    TreeNode[] ArrayTreeNode = F.tvObjet.Nodes.Find(F.oCnxBase.Reader.GetString(0), true);
                    if (ArrayTreeNode.Length == 1)
                        ServerInTV += ";" + ArrayTreeNode[0].Text + "   -" + F.oCnxBase.Reader.GetString(1) + " (" + ArrayTreeNode[0].Name + ")";
                }
            }
            F.oCnxBase.CBReaderClose();
            if (ServerInTV != "") return ServerInTV.Substring(1);
            return null;
        }
        
        public override void CWInsert(ControlDoc cw, char cTypeVue)
        {
            if (cTypeVue == '3' || cTypeVue == '5' || cTypeVue == '4' || cTypeVue == '8' || cTypeVue == '7')
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
                    if (cTypeVue == '8' || cTypeVue == '7')
                        cw.InsertTextFromId(sBook, true, Texte + "\n", "Titre 5");
                    else
                        cw.InsertTextFromId(sBook, true, Texte + "\n", "Titre 4");
                    cw.InsertTabFromId("n" + sBook, true, this, null, false, null);
                }
                else if (cw.Exist(sClusterBook) > -1)
                {
                    //sType ne doit pas depasse 4 caracteres

                    cw.InsertTextFromId(sClusterBook, false, "\n", null);
                    cw.CreatIdFromIdP(sBook, sClusterBook);
                    if (cTypeVue == '8' || cTypeVue == '7') cw.InsertTextFromId(sBook, true, Texte + "\n", "Titre 5");
                    else cw.InsertTextFromId(sBook, true, Texte + "\n", "Titre 4");
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
    }
}
