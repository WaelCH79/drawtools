using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data.Odbc;
using MOI = Microsoft.Office.Interop;
 
using System.Xml;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawNCard : DrawTools.DrawRectangle
	{
        private int handleVLan;
        
        public int Hauteur
        {
            get
            {
                return (int) this.GetValueFromName("Hauteur");;
            }
            set
            {
                this.InitProp("Hauteur", (object) value, true);
            }
        }

		public DrawNCard()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawNCard(Form1 of, Point pt, DrawServerPhy ds)
        {
            F = of;
            OkMove = false;
            Align = false;
            Rectangle = new Rectangle(1, 1, 1, 1);
            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = new ArrayList();
            LstValue = new ArrayList();
            LstLinkOut = null;

            Texte = "e" + ((int)(ds.NbrNCard('N')+1));
            GuidkeyObjet = Guid.NewGuid();
            Guidkey = Guid.NewGuid();
            InitProp();
            Hauteur = setHauteur(pt, ds);
            Initialize();
        }

        public DrawNCard(Form1 of, Point pt, DrawCluster ds)
        {
            F = of;
            OkMove = false;
            Align = false;
            Rectangle = new Rectangle(1, 1, 1, 1);
            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = new ArrayList();
            LstValue = new ArrayList();
            LstLinkOut = null;

            Texte = "e" + ((int)(ds.NbrNCard()+1));
            GuidkeyObjet = Guid.NewGuid();
            Guidkey = Guid.NewGuid();
            InitProp();
            Hauteur = setHauteur(pt, ds);
            Initialize();
        }

        public DrawNCard(Form1 of, DrawInsnd dind)
        {
            F = of;
            OkMove = false;
            Align = false;
            Rectangle = new Rectangle(1, 1, 1, 1);
            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = new ArrayList();
            LstValue = new ArrayList();
            LstLinkOut = null;

            Texte = "e" + ((int)(dind.NbrNCard() + 1));
            GuidkeyObjet = Guid.NewGuid();
            Guidkey = Guid.NewGuid();
            InitProp();
            Hauteur = 1;
            Initialize();
        }

        public DrawNCard(Form1 of, DrawInssvc disvc)
        {
            F = of;
            OkMove = false;
            Align = false;
            Rectangle = new Rectangle(1, 1, 1, 1);
            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = new ArrayList();
            LstValue = new ArrayList();
            LstLinkOut = null;

            Texte = "e" + ((int)(disvc.NbrNCard() + 1));
            GuidkeyObjet = Guid.NewGuid();
            Guidkey = Guid.NewGuid();
            InitProp();
            Hauteur = 0;
            Initialize();
        }

        public DrawNCard(Form1 of, DrawInsing diing)
        {
            F = of;
            OkMove = false;
            Align = false;
            Rectangle = new Rectangle(1, 1, 1, 1);
            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = new ArrayList();
            LstValue = new ArrayList();
            LstLinkOut = null;

            Texte = "e" + ((int)(diing.NbrNCard() + 1));
            GuidkeyObjet = Guid.NewGuid();
            Guidkey = Guid.NewGuid();
            InitProp();
            Hauteur = 0;
            Initialize();
        }

        public DrawNCard(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = false;
            Align = false;
            InitRectangle(lstValG);
            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = new ArrayList();
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

        public override void DelLinkInProperty()
        {
            SetValueFromName("GuidVlan", "");
        }

        public override bool ParentPointInObject(Point point)
        {
            return false;
        }

        public int setHauteur(Point pt, DrawRectangle ds)
        {
            if (pt.Y - ds.Rectangle.Top <= ds.Rectangle.Bottom - pt.Y)
            {
                return 0;
            }
            else
            {
                //return ds.Rectangle.Bottom-ds.EpaisseurCard;
                return 1;
            }
        }

        public override DrawArea.DrawToolType GetToolTypeForObjExp()
        {
            return DrawArea.DrawToolType.NCard;
        }

        public void Aligne(int x, int y, int width, int height)
        {
            DrawRectangle ds = (DrawRectangle)LstParent[0];
            int newy = y + Hauteur * (ds.Rectangle.Height - ds.EpaisseurCard);

            if (width != Rectangle.Width)
            {
                for (int i = 0; i < LstLinkIn.Count; i++)
                {
                    if (LstLinkIn[i].GetType() == typeof(DrawVLan))
                    {
                        DrawVLan dv = (DrawVLan)LstLinkIn[i];
                        int n = dv.LstLinkOut.IndexOf(this);
                        if(n>-1 && n < dv.pointArray.Count)
                            dv.AjustePoint(n, ((float)width) / ((float)rectangle.Width), rectangle.X, x, Hauteur == 0 ? y : y + HeightCard);
                    }
                    //if (LstLinkIn[i].GetType() == typeof(DrawVLan) && handleVLan >= 0) ((DrawVLan)LstLinkIn[i]).AjustePoint(HandleVLan, ((float)width) / ((float)rectangle.Width), rectangle.X, x, y);
                }
            }
            
            Rectangle = new Rectangle(x, y, width, height);
            //F.drawArea.Refresh();
        }


        public override void dataGrid_CellClick(DataGridView odgv, DataGridViewCellEventArgs e)
        {

            int n;

            n = GetIndexFromName("Alias");
            if (n > -1 && e.RowIndex == n)
            {
                MessageBox.Show("Cette fonction n'est plus disponible. Veuillez utiliser la boite de dialogue des flux");
                /*FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.EnableProp();
                fcp.lSourceDisable();
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);*/
            }

            n = GetIndexFromName("FluxIn");
            if (n > -1 && e.RowIndex == n)
            {
                MessageBox.Show("Cette fonction n'est plus disponible. Veuillez utiliser la boite de dialogue des flux");
                /*FormChangeProp fcp = new FormChangeProp(F, odgv);
                if (fcp.AddcbAlias())
                {
                    fcp.AddlSourceFromTv("Link");
                    fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                    fcp.ShowDialog(F);
                }
                else MessageBox.Show("L'interface sélectionnée ne possède pas l'alias");*/
            }

            n = GetIndexFromName("FluxOut");
            if (n > -1 && e.RowIndex == n)
            {
                MessageBox.Show("Cette fonction n'est plus disponible. Veuillez utiliser la boite de dialogue des flux");
                /*FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromTv("Link");
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);*/
            }

        }

        public override void Draw(Graphics g)
        {
            ToolNCard to = (ToolNCard)F.drawArea.tools[(int)DrawArea.DrawToolType.NCard];

            Pen pen = new Pen(to.LineCouleur, to.LineWidth);
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);
            if (LstParent.Count != 0)
            {
                DrawRectangle dsp = (DrawRectangle)LstParent[0];
                if (dsp.ModeGraphic == modeGraphic.detail) ModeGraphic = modeGraphic.detail;
                else if (dsp.ModeGraphic == modeGraphic.resume) ModeGraphic = modeGraphic.resume;
                else if (dsp.ModeGraphic == modeGraphic.nom) ModeGraphic = modeGraphic.vide;
                else if (dsp.ModeGraphic == modeGraphic.icon) ModeGraphic = modeGraphic.vide;
                else if (dsp.ModeGraphic == modeGraphic.forme) ModeGraphic = modeGraphic.vide;
                else if (dsp.ModeGraphic == modeGraphic.vide) ModeGraphic = modeGraphic.vide;

                switch (ModeGraphic)
                {
                    case modeGraphic.detail:
                        if (!F.bPtt)
                        {

                            AffRec(g, r, to);

                            if (dsp.GetType() == typeof(DrawServerPhy))
                            {
                                DrawGrpTxt(g, 1, 0, r.Left + Axe, r.Top + r.Height / 2 - 3 * HeightFont[0] / 4, 0, to.Pen1Couleur, to.BkGrCouleur);


                                string sType = (string)dsp.GetValueFromName("Type");
                                DrawObject oParent = (DrawObject)LstParent[0];
                                int idx = oParent.GetIndexFromListe(oParent.LstChild, GuidkeyObjet);

                                //if (sType != "") DrawGrpTxt(g, 2, 0, dsp.rectangle.Left + Axe, (Convert.ToInt32(Texte.Substring(Texte.Length - 1)) - 1) * (HeightFont[1] + Axe) + dsp.rectangle.Top + HeightCard + 2 * Axe + HeightServer, 1, to.Pen1Couleur, to.BkGrCouleur);
                                //else DrawGrpTxt(g, 2, 0, dsp.rectangle.Left + Axe + imgIconWidth, (Convert.ToInt32(Texte.Substring(Texte.Length - 1)) - 1) * (HeightFont[1] + Axe) + dsp.rectangle.Top + HeightCard + 2 * Axe + HeightServer, 1, to.Pen1Couleur, to.BkGrCouleur);
                                if (idx > -1)
                                {
                                    if (sType != "" && sType[0] != 32) DrawGrpTxt(g, 2, 0, dsp.rectangle.Left + Axe, idx * (HeightFont[1] + Axe) + dsp.rectangle.Top + HeightCard + 2 * Axe + HeightServer, 1, to.Pen1Couleur, to.BkGrCouleur);
                                    else DrawGrpTxt(g, 2, 0, dsp.rectangle.Left + Axe + imgIconWidth, idx * (HeightFont[1] + Axe) + dsp.rectangle.Top + HeightCard + 2 * Axe + HeightServer, 1, to.Pen1Couleur, to.BkGrCouleur);
                                }                                
                            }
                            else
                            {
                                DrawGrpTxt(g, 1, 0, r.Left + Axe, r.Top + r.Height / 2 - 3 * HeightFont[0] / 4, 0, to.Pen1Couleur, to.BkGrCouleur);
                            }
                        }
                        break;
                    case modeGraphic.resume:
                    case modeGraphic.nom:
                        if (!F.bPtt)
                        {
                            AffRec(g, r, to);
                            if (dsp.GetType() == typeof(DrawCluster))
                                DrawGrpTxt(g, 1, 0, r.Left + Axe, r.Top + r.Height / 2 - 3 * HeightFont[0] / 4, 0, to.Pen1Couleur, to.BkGrCouleur);
                            else
                                DrawGrpTxt(g, 1, 0, r.Left + Axe, r.Top + r.Height / 2 - 3 * HeightFont[0] / 4, 0, to.Pen1Couleur, to.BkGrCouleur);
                        }
                        break;
                    case modeGraphic.icon:
                        break;
                    case modeGraphic.forme:
                        break;
                    case modeGraphic.vide:
                        break;
                }
                
                pen.Dispose();
            }
        }

        public override void VisioDraw(ArrayList lstGuid, ArrayList lstShape, MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
            ToolNCard to = (ToolNCard)F.drawArea.tools[(int)DrawArea.DrawToolType.NCard];

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

                /*MOI.Visio.Selection s = page.CreateSelection(Microsoft.Office.Interop.Visio.VisSelectionTypes.visSelTypeSingle, Microsoft.Office.Interop.Visio.VisSelectMode.visSelModeOnlySuper, shapeOm);
                s.Select(shapeOm, 0);
                s.Select(shape, 0);
                s.ConvertToGroup();*/

                //Inserer le texte + Couleur + taille
                DrawRectangle dsp = (DrawRectangle)LstParent[0];
                if (dsp.GetType() == typeof(DrawCluster)) DrawGrpTxt(shape, 2, 0, 0, 0, 1, Color.Black, Color.Transparent);
                
                else
                {
                    DrawGrpTxt(shape, 1, 0, 0, 0, 0, Color.Black, Color.Transparent);

                    string sType = (string)dsp.GetValueFromName("Type");
                    if (sType != "")
                    {
                        int iTop = dsp.Rectangle.Top + 2 * HeightCard + 2 * Axe + (Convert.ToInt32(Texte.Substring(Texte.Length - 1)) - 1) * (HeightFont[1] + Axe);
                        MOI.Visio.Shape shTxt2 = page.DrawRectangle(dsp.Rectangle.Left * qxPage, yPage - iTop * qyPage, (dsp.Rectangle.Left + dsp.Rectangle.Width / 2) * qxPage, yPage - (iTop + HeightFont[1] + Axe) * qyPage);
                        DrawGrpTxt(shTxt2, 2, 0, 0, 0, 1, Color.Black, Color.Transparent);
                        shTxt2.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLinePattern).ResultIU = 0;
                        shTxt2.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillPattern).ResultIU = 0;
                    }
                    else
                    {
                        int iTop = dsp.Rectangle.Top + 2 * HeightCard + 2 * Axe + (Convert.ToInt32(Texte.Substring(Texte.Length - 1)) - 1) * (HeightFont[1] + Axe);
                        MOI.Visio.Shape shTxt2 = page.DrawRectangle((dsp.Rectangle.Left + Axe + imgIconWidth) * qxPage, yPage - iTop * qyPage, dsp.Rectangle.Right * qxPage, yPage - (iTop + HeightFont[1] + Axe) * qyPage);
                        DrawGrpTxt(shTxt2, 2, 0, 0, 0, 1, Color.Black, Color.Transparent);
                        shTxt2.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLinePattern).ResultIU = 0;
                        shTxt2.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillPattern).ResultIU = 0;
                    }
                }
                
                //Couleur trait
                //shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + Couleur.R.ToString() + "," + Couleur.G.ToString() + "," + Couleur.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLinePattern).ResultIU = 0;

                //Couleur Fond
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillBkgnd).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillForegnd).FormulaU = "RGB(" + Color.White.R.ToString() + "," + Color.White.G.ToString() + "," + Color.White.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillPattern).ResultIU = 32;

                lstShape.Add(shape);
                lstGuid.Add(GuidkeyObjet.ToString());
            }
        }

        public void saveLinkAlias()
        {
            //F.oCnxBase.MajNCardAlias(this);
            //F.oCnxBase.CreatNCardLinkIn(this);
            //F.oCnxBase.DelNCardAlias(this);
            //F.oCnxBase.CreatNCardLink(this, "Out");
        }

        /// <summary>
        /// Save Object to the Data Base
        /// </summary>
        public override void savetoDB()
        {
            if (!savetoDBFait())
            {
                base.savetoDB();
                saveLinkAlias();
                savetoDBOK();
            }
        }

        public override XmlElement savetoXml(XmlDB xmlDB, bool GObj)
        {
            System.Xml.XmlElement elo = base.savetoXml(xmlDB, GObj);
            if (elo != null)
            {
                //Alias
                if (F.oCnxBase.CBRecherche("Select GuidAlias, NomAlias FROM Alias WHERE GuidNCard='" + GuidkeyObjet.ToString() + "'"))
                {
                    while (F.oCnxBase.Reader.Read())
                    {
                        XmlElement el = xmlDB.XmlCreatEl(xmlDB.XmlGetFirstElFromParent(elo, "After"), "Alias", "GuidAlias");
                        XmlElement elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");
                        xmlDB.XmlSetAttFromEl(elAtts, "GuidAlias", "s", F.oCnxBase.Reader.GetString(0));
                        xmlDB.XmlSetAttFromEl(elAtts, "NomAlias", "s", F.oCnxBase.Reader.GetString(1));
                        xmlDB.XmlSetAttFromEl(elAtts, "GuidNCard", "s", GuidkeyObjet.ToString());
                    }
                }
                F.oCnxBase.CBReaderClose();

                //NCardLinkIn
                if (F.oCnxBase.CBRecherche("Select GuidTechLink, GuidAlias FROM NCardLinkIn WHERE GuidNCard='" + GuidkeyObjet.ToString() + "' AND GuidTechLink IN (SELECT TechLink.GuidTechLink FROM Vue, DansVue, GTechLink, TechLink WHERE Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGTechLink AND GTechLink.GuidTechLink=TechLink.GuidTechLink AND GuidAppVersion='" + F.GetGuidAppVersion().ToString() + "')"))
                {
                    while (F.oCnxBase.Reader.Read())
                    {
                        XmlElement el = xmlDB.XmlCreatEl(xmlDB.XmlGetFirstElFromParent(elo, "After"), "NCardLinkIn", "GuidNCard,GuidTechLink");
                        XmlElement elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");
                        xmlDB.XmlSetAttFromEl(elAtts, "GuidNCard", "s", GuidkeyObjet.ToString());
                        xmlDB.XmlSetAttFromEl(elAtts, "GuidTechLink", "s", F.oCnxBase.Reader.GetString(0));
                        if (!F.oCnxBase.Reader.IsDBNull(1)) xmlDB.XmlSetAttFromEl(elAtts, "GuidAlias", "s", F.oCnxBase.Reader.GetString(1));
                    }
                }
                F.oCnxBase.CBReaderClose();

                //NCardLinkOut
                if (F.oCnxBase.CBRecherche("Select GuidTechLink FROM NCardLinkOut WHERE GuidNCard='" + GuidkeyObjet.ToString() + "' AND GuidTechLink IN (SELECT TechLink.GuidTechLink FROM Vue, DansVue, GTechLink, TechLink WHERE Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGTechLink AND GTechLink.GuidTechLink=TechLink.GuidTechLink AND GuidAppVersion='" + F.GetGuidAppVersion().ToString() + "')"))
                {
                    while (F.oCnxBase.Reader.Read())
                    {
                        XmlElement el = xmlDB.XmlCreatEl(xmlDB.XmlGetFirstElFromParent(elo, "After"), "NCardLinkOut", "GuidNCard,GuidTechLink");
                        XmlElement elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");
                        xmlDB.XmlSetAttFromEl(elAtts, "GuidNCard", "s", GuidkeyObjet.ToString());
                        xmlDB.XmlSetAttFromEl(elAtts, "GuidTechLink", "s", F.oCnxBase.Reader.GetString(0));
                    }
                }
                F.oCnxBase.CBReaderClose();
                
                return elo;
            }
            return null;
        }
	}
}
