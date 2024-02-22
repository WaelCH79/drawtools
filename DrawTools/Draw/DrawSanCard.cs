using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data.Odbc;
using System.Xml;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawSanCard : DrawTools.DrawRectangle
	{
        private int handleSanSwitch;
        public int HandleSanSwitch
        {
            get
            {
                return handleSanSwitch;
            }
            set
            {
                handleSanSwitch = value;
            }
        }

        public int Hauteur
        {
            get
            {
                return (int) this.GetValueFromName("Hauteur");
            }
            set
            {
                this.InitProp("Hauteur", (object) value, true);
            }
        }

		public DrawSanCard()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawSanCard(Form1 of, Point pt, DrawServerPhy ds)
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

            Texte = "s" + ds.NbrNCard('S');
            GuidkeyObjet = Guid.NewGuid();
            Guidkey = Guid.NewGuid();
            InitProp();
            Hauteur = setHauteur(pt, (DrawRectangle) ds);
            Initialize();
        }

        public DrawSanCard(Form1 of, Point pt, DrawBaieCTI db)
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

            Texte = "s" + db.NbrNCard();
            GuidkeyObjet = Guid.NewGuid();
            Guidkey = Guid.NewGuid();
            InitProp();
            Hauteur = setHauteur(pt, (DrawRectangle)db);
            Initialize();
        }

        public DrawSanCard(Form1 of, Point pt, DrawISL di)
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

            Texte = "s" + di.NbrNCard();
            GuidkeyObjet = Guid.NewGuid();
            Guidkey = Guid.NewGuid();
            InitProp();
            Hauteur = setHauteur(pt, (DrawRectangle) di);
            Initialize();
        }

        public DrawSanCard(Form1 of, ArrayList lstVal, ArrayList lstValG)
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

        public override bool ParentPointInObject(Point point)
        {
            return false;
        }

        public int setHauteur(Point pt, DrawRectangle dr)
        {
            if (pt.Y + EpaisseurCard/2 - dr.Rectangle.Top < dr.Rectangle.Bottom - pt.Y - EpaisseurCard/2)
            {
                return 0;
            }
            else
            {
                //return ds.Rectangle.Bottom-ds.EpaisseurCard;
                return 1;
            }
        }


        public void Aligne(int x, int y, int width, int height)
        {
            DrawRectangle dr;

            dr = (DrawRectangle)LstParent[0];
            Rectangle = new Rectangle(x, y + Hauteur * (dr.Rectangle.Height - dr.EpaisseurCard), width, height);
        }

        public override void Draw(Graphics g)
        {
            ToolSanCard to = (ToolSanCard)F.drawArea.tools[(int)DrawArea.DrawToolType.SanCard];

            Pen pen = new Pen(to.LineCouleur, to.LineWidth);
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10)
            {
                AffRec(g, r, to);
                //iHeightFont = r.Height - 5;
                //DrawServerPhy dsp = (DrawServerPhy)LstParent[0];
                DrawRectangle dr = (DrawRectangle)LstParent[0];
                DrawGrpTxt(g, 1, 0, r.Left + Axe, r.Top + r.Height / 2 - 3 * HeightFont[0] / 4, 0, to.Pen1Couleur, to.BkGrCouleur);

                DrawGrpTxt(g, 2, 0, dr.rectangle.Left + Axe, Convert.ToInt32(Texte.Substring(Texte.Length - 1)) * (HeightFont[1] + Axe) + dr.rectangle.Top + HeightCard + HeightServer, 2, to.Pen1Couleur, to.BkGrCouleur);
                
            } else g.DrawRectangle(pen, r);
            if (Hauteur == 0) DrawGrpTxt(g, 3, 0, r.Left + r.Width / 2, r.Top - 3 * HeightFont[2], 0, Color.Black, Color.Transparent);
            else DrawGrpTxt(g, 3, 0, r.Left + r.Width / 2, r.Bottom + 3 * HeightFont[2], 0, Color.Black, Color.Transparent);

            pen.Dispose();
        }

        public override void dataGrid_CellClick(DataGridView odgv, DataGridViewCellEventArgs e)
        {
            //if (odgv.CurrentCell.RowIndex == 2) // Ligne Link Applicatif
            int n;

            n = GetIndexFromName("GuidSanCardA");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                // Faire un message warning car les liens avec les differentes baies affiches
                // represententes l'ensemble des applications
                DrawObject o = (DrawObject)LstParent[0];
                if (o.GetType() == typeof(DrawServerPhy))
                {
                    FormChangeProp fcp = new FormChangeProp(F, odgv);
                    fcp.AddlSourceFromDB("SELECT DISTINCT GuidSanCard, Alias FROM Zone, Lun, Baie, SanCard WHERE Zone.GuidLun=Lun.GuidLun AND Lun.GuidBaie=Baie.GuidBaie AND Baie.GuidBaie=SanCard.GuidHote AND GuidServerPhy='" + o.GuidkeyObjet + "' AND GuidSanSwitch='" + (string)GetValueFromName("GuidSanSwitch") + "' ORDER BY Alias", "Value");
                    fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                    fcp.ShowDialog(F);
                }
            }
        }

        public void GetSanCardLinks(string field)
        {
            int idx = GetIndexFromName("Guid" + field);
            if (idx > -1)
            {
                string fieldInf = "";
                string sQuery = "SELECT GuidSanCard, NomSanCard, Alias FROM SanCard, " + field + "Link WHERE GuidSanCardSrc ='" + GuidkeyObjet + "' and GuidVue='" + F.GuidVue + "' and GuidSanCardCbl=GuidSanCard";
                if (F.oCnxBase.CBRecherche(sQuery))
                {
                    while (F.oCnxBase.Reader.Read())
                    {
                        string sAlias;
                        if (F.oCnxBase.Reader.IsDBNull(2)) sAlias = "notdefined"; else sAlias = F.oCnxBase.Reader.GetString(2);
                        fieldInf += ";" + sAlias + "_" + F.oCnxBase.Reader.GetString(1) + "   (" + F.oCnxBase.Reader.GetString(0) + ")";
                    }
                    F.oCnxBase.CBReaderClose();
                    LstValue[idx] = fieldInf.Substring(1);
                }
                else F.oCnxBase.CBReaderClose();
            }
        }

        public void SetSanCardLink(string field)
        {
            object o = GetValueFromName(field);
            if (o != null)
            {
                string Link = (string)o;
                if (Link != "")
                {
                    string[] aLink = Link.Split(new Char[] { '(', ')' });
                    //Supprimer tous les links
                    F.oCnxBase.CBWrite("DELETE FROM " + field.Substring("Guid".Length) + "Link  WHERE GuidSanCardSrc='" + GuidkeyObjet + "'");
                    for (int i = 1; i < aLink.Length; i += 2)
                    {
                        if (F.oCnxBase.CBRecherche("SELECT Alias FROM SanCard WHERE GuidSanCard='" + aLink[i].Trim() + "'"))
                        {
                            string AliasBaie;
                            if (F.oCnxBase.Reader.IsDBNull(0)) AliasBaie = ""; else AliasBaie = F.oCnxBase.Reader.GetString(0);

                            F.oCnxBase.CBReaderClose();
                            F.oCnxBase.CBWrite("INSERT INTO " + field.Substring("Guid".Length) + "Link (GuidSanCardSrc, GuidVue, GuidSanCardCbl, NomZoning) VALUES ('" + GuidkeyObjet + "','" + F.GuidVue + "','" + aLink[i].Trim() + "','Z_" + (string)GetValueFromName("Alias") + "_" + AliasBaie + "')");
                        }
                        F.oCnxBase.CBReaderClose();
                    }
                }
            }
        }

        public override void savetoDB()
        {
            // Creation Enreg ServerLink Server <-> ServerPhy
            if (!savetoDBFait())
            {
                base.savetoDB();
                SetSanCardLink("GuidSanCardA");

                savetoDBOK();
            }
        }

        public override XmlElement savetoXml(XmlDB xmlDB, bool GObj)
        {
            XmlElement elo = base.savetoXml(xmlDB, GObj);
            if (elo != null)
            {
                string field = "GuidSanCardA";
                object o = GetValueFromName(field);
                if (o != null)
                {
                    XmlElement el;
                    string Link = (string)o;
                    if (Link != "")
                    {
                        string[] aLink = Link.Split(new Char[] { '(', ')' });
                        for (int i = 1; i < aLink.Length; i += 2)
                        {
                            if (F.oCnxBase.CBRecherche("SELECT Alias FROM SanCard WHERE GuidSanCard='" + aLink[i].Trim() + "'"))
                            {
                                
                                if ( F.drawArea.GraphicsList.FindObjet(0, (aLink[i].Trim()))>=0)
                                {
                                    string AliasBaie;
                                    Guid guid = F.GuidVue;
                                    if (F.oCnxBase.Reader.IsDBNull(0)) AliasBaie = ""; else AliasBaie = F.oCnxBase.Reader.GetString(0);
                                    F.oCnxBase.CBReaderClose();

                                    if (xmlDB.XmlGetElFromInnerText(xmlDB.GetCursor(), (aLink[i].Trim())) == null)
                                        xmlDB.XmlCreatExternRef(xmlDB.XmlGetFirstElFromParent(elo, "After"), -1, F.oCnxBase.ConfDB.FindTable("SanCard"), (aLink[i].Trim()));

                                    el = xmlDB.XmlCreatEl(xmlDB.XmlGetFirstElFromParent(elo, "After"), field.Substring("Guid".Length) + "Link", "GuidSanCardSrc,GuidVue,GuidSanCardCbl");
                                    XmlElement elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");
                                    xmlDB.XmlSetAttFromEl(elAtts, "GuidSanCardSrc", "s", GuidkeyObjet.ToString());
                                    xmlDB.XmlSetAttFromEl(elAtts, "GuidVue", "s", guid.ToString());
                                    xmlDB.XmlSetAttFromEl(elAtts, "GuidSanCardCbl", "s", aLink[i].Trim());
                                    xmlDB.XmlSetAttFromEl(elAtts, "NomZoning", "s", "Z_" + (string)GetValueFromName("Alias") + "_" + AliasBaie);
                                }
                            }
                            F.oCnxBase.CBReaderClose();
                        }
                    }
                }

                return elo;
            }
            return null;
        }

        public override void CWInsert(ControlDoc cw, char cTypeVue)
        {

            string sBook = "SanA";
            if (F.tbTypeVue.Text[0] == '9') sBook = "San9";

            if (F.oCnxBase.CBRecherche("Select NomSanSwitch, NomZoning, M.Alias, M.WWN, B.Alias, B.WWN FROM SanSwitch, SanCard M, SanCard B, SanCardALink WHERE M.GuidSanSwitch=SanSwitch.GuidSanSwitch AND M.GuidSanCard=GuidSanCardSrc AND B.GuidSanCard=GuidSanCardCbl AND M.GuidSanCard='" + GuidkeyObjet + "'"))
            {
                F.oCnxBase.Reader.Read();
                cw.InsertRowFromReaderId(sBook, F.oCnxBase.Reader, "TabSan");
            }
            F.oCnxBase.CBReaderClose();
        }
    }
}
