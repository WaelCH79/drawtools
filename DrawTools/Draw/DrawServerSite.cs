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
	public class DrawServerSite : DrawTools.DrawRectangle
	{
		public DrawServerSite()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawServerSite(Form1 of)
        {
            F = of;
            OkMove = true;
            Align = true;
            Rectangle = new Rectangle(1, 1, 1, 1);
            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = new ArrayList();
            LstLinkOut = new ArrayList();
            LstValue = new ArrayList();

            Texte = F.tvObjet.SelectedNode.Text;
            GuidkeyObjet = Guid.NewGuid();
            Guidkey = Guid.NewGuid();

            InitProp();
            Initialize();
        }

        public DrawServerSite(Form1 of, ArrayList lstVal, ArrayList lstValG)
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

            o = GetValueFromName("GuidLocation");
            if (o != null)
            {
                object o1 = GetValueFromName("NomLocation");
                string s = (string)o1 + "   (" + (string)o + ")";
                SetValueFromName("NomLocation", (object)s);
            }

            Initialize();
        }

        public void InitRectangle(int pos, int nbrSrv, int XLocation, int YLocation)
        {
            int nbr = nbrSrv / 4 + 1;
            int rangx;

            int rangy = Math.DivRem(pos, nbr, out rangx);

            rectangle.Width = WidthServerSite; rectangle.Height = HeightServerSite;

            rectangle.X = XLocation + Axe + rangx * (WidthServerSite + Axe) ;
            rectangle.Y = YLocation + HeightSite + rangy * (HeightServerSite + Axe);
        }

        public override bool ParentPointInObject(Point point)
        {
            return false;
        }

        public override string GetTypeSimpleTable()
        {
            //return base.GetTypeSimpleTable();
            return "ServerPhy";
        }

        public override void Draw(Graphics g)
        {
            ToolServerSite to = (ToolServerSite)F.drawArea.tools[(int)DrawArea.DrawToolType.ServerSite];

            Pen pen = new Pen(to.LineCouleur, to.LineWidth);
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (F.bPtt)
            {
                AffRec(g, r, to);

                string ImgObj = (string)GetValueFromName("Image");
                if (ImgObj == "") ImgObj = "defaut";
                Bitmap image2 = (Bitmap)Image.FromFile(F.sPathRoot + @"\bouton\" + ImgObj + ".png", true);
                if (imgIconHeight * image2.Width / image2.Height <= imgIconWidth)
                    g.DrawImage(image2, new Rectangle(r.Left + Axe, r.Top + r.Height/2 - imgIconHeight/2, imgIconHeight * image2.Width / image2.Height, imgIconHeight));
                else
                    g.DrawImage(image2, new Rectangle(r.Left + Axe, r.Top + r.Height / 2 - imgIconWidth * image2.Height / image2.Width/2, imgIconWidth, imgIconWidth * image2.Height / image2.Width));

                //DrawGrpTxt(g, 4, 0, r.Left + Axe, r.Top, 0, Color.Black);
                DrawGrpTxt(g, 4, 0, r.Left + imgIconWidth, r.Top + r.Height / 2 - 3 * HeightFont[3] / 4, 0, Color.Black, Color.Transparent);
            }
            else
            {
                if (r.Width > 20 && r.Height > 10)
                {
                    AffRec(g, r, to);

                    DrawGrpTxt(g, 1, 0, r.Left + Axe, r.Top, 0, to.Pen1Couleur, to.BkGrCouleur);
                    DrawGrpTxt(g, 2, 1, r.Left + Axe, r.Top + HeightFont[1] + 2 * Axe, 0, to.Pen1Couleur, to.BkGrCouleur);
                }
                else g.DrawRectangle(pen, r);
            }

            pen.Dispose();
        }

        public override XmlElement savetoXml(XmlDB xmlDB, bool GObj)
        {
            XmlElement elo = base.savetoXml(xmlDB, GObj);

            if (elo != null)
            {
                //Ncard si le serveur n'existe pas dans les vue de déploiement
                if (F.oCnxBase.CBRecherche("Select GuidNCard, NomNCard from NCard where GuidHote='" + GuidkeyObjet.ToString() + "'"))
                {
                    while (F.oCnxBase.Reader.Read())
                    {
                        XmlElement el = xmlDB.XmlCreatEl(xmlDB.XmlGetFirstElFromParent(elo, "After"), "NCard", "GuidNCard");
                        XmlElement elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");
                        xmlDB.XmlSetAttFromEl(elAtts, "GuidNCard", "s", F.oCnxBase.Reader.GetString(0));
                        xmlDB.XmlSetAttFromEl(elAtts, "NomNCard", "s", F.oCnxBase.Reader.GetString(1));
                        xmlDB.XmlSetAttFromEl(elAtts, "GuidHote", "s", GuidkeyObjet.ToString());       
                    }
                }
                F.oCnxBase.CBReaderClose();
                return elo;
            }
            return null;
        }


        public override void VisioDraw(ArrayList lstGuid, ArrayList lstShape, MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
            ToolServerSite to = (ToolServerSite)F.drawArea.tools[(int)DrawArea.DrawToolType.ServerSite];

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
                
                //Inserer le texte + Couleur + taille
                DrawGrpTxt(shape, 1, 0, 0, (Rectangle.Height - HeightFont[1]) * qyPage, 0, Color.Black, Color.Transparent);
                //Texte Server Type
                MOI.Visio.Shape shTxt3 = page.DrawRectangle(Rectangle.Left * qxPage, yPage - (Rectangle.Top + Axe + HeightFont[1]) * qyPage, Rectangle.Right * qxPage, yPage - (Rectangle.Top + Axe + 2 * (Axe +  HeightFont[0])) * qyPage);
                DrawGrpTxt(shTxt3, 2, 1, 0, 0, 0, Color.Black, Color.Transparent);
                shTxt3.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLinePattern).ResultIU = 0;
                shTxt3.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillPattern).ResultIU = 0;

                //Couleur trait
                //shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + Couleur.R.ToString() + "," + Couleur.G.ToString() + "," + Couleur.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLinePattern).ResultIU = 0;
                //Couleur Fond
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillBkgnd).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillForegnd).FormulaU = "RGB(" + Color.White.R.ToString() + "," + Color.White.G.ToString() + "," + Color.White.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillPattern).ResultIU = 31;

                lstShape.Add(shape);
                lstGuid.Add(GuidkeyObjet.ToString());
            }
        }

        public void GetServerLinksU(string field)
        {
            int idx = GetIndexFromName("Guid" + field);
            int idx1 = GetIndexFromName("Image");
            if (idx > -1 && idx1 > -1)
            {
                string fieldInf = "";
                //string sQuery = "SELECT " + field + ".Guid" + field + ", Nom" + field + " FROM " + field + ", " + field + "Link WHERE GuidServerPhy ='" + GuidkeyObjet + "' and GuidVue='" + F.cbGuidVue.Items[F.cbVue1.SelectedIndex] + "' and " + field + ".Guid" + field + "=" + field + "Link.Guid" + field;
                string sQuery = "SELECT DISTINCT " + field + ".Guid" + field + ", Nom" + field + ", Image FROM " + field + ", " + field + "Link WHERE GuidServerPhy ='" + GuidkeyObjet + "' and " + field + ".Guid" + field + "=" + field + "Link.Guid" + field;
                string sqlString2 = " AND " + field + ".Guid" + field + " IN (SELECT " + field + ".Guid" + field + " FROM Vue, DansVue, GTechUser, " + field + " WHERE Vue.GuidGVue=DansVue.GuidGVue AND DansVue.GuidObjet=GTechUser.GuidGTechUser AND GTechUser.Guid" + field + "=" + field + ".Guid" + field + " AND Vue.GuidAppVersion='" + F.GetGuidAppVersion() + "' AND Vue.GuidTypeVue='d5b533a9-06ac-4f8c-a5ab-e345b0212542')";
                if (F.oCnxBase.CBRecherche(sQuery + sqlString2))
                {
                    while (F.oCnxBase.Reader.Read())
                    {
                        fieldInf += ";" + F.oCnxBase.Reader.GetString(1) + "   (" + F.oCnxBase.Reader.GetString(0) + ")";
                        if (!F.oCnxBase.Reader.IsDBNull(2)) LstValue[idx1] = F.oCnxBase.Reader.GetString(2);
                    }
                    F.oCnxBase.CBReaderClose();
                    LstValue[idx] = fieldInf.Substring(1);
                }
                else F.oCnxBase.CBReaderClose();

            }
        }

        public void GetServerLinks(string field)
        {
            int idx = GetIndexFromName("Guid" + field);
            int idx1 = GetIndexFromName("Image");
            if (idx > -1 && idx1 > -1)
            {
                string fieldInf = "";
                //string sQuery = "SELECT " + field + ".Guid" + field + ", Nom" + field + " FROM " + field + ", " + field + "Link WHERE GuidServerPhy ='" + GuidkeyObjet + "' and GuidVue='" + F.cbGuidVue.Items[F.cbVue1.SelectedIndex] + "' and " + field + ".Guid" + field + "=" + field + "Link.Guid" + field;
                string sQuery = "SELECT DISTINCT " + field + ".Guid" + field + ", Nom" + field + ", Image FROM " + field + ", " + field + "Link WHERE GuidServerPhy ='" + GuidkeyObjet + "' and " + field + ".Guid" + field + "=" + field + "Link.Guid" + field;
                string sqlString2 = " AND " + field + ".Guid" + field + " IN (SELECT " + field + ".Guid" + field + " FROM Vue, DansVue, G" + field + ", " + field + " WHERE Vue.GuidGVue=DansVue.GuidGVue AND DansVue.GuidObjet=G" + field + ".GuidG" + field + " AND G" + field + ".Guid" + field + "=" + field + ".Guid" + field + " AND Vue.GuidAppVersion'" + F.GetGuidAppVersion() + "' AND Vue.GuidTypeVue='d5b533a9-06ac-4f8c-a5ab-e345b0212542')";
                if (F.oCnxBase.CBRecherche(sQuery + sqlString2))
                {
                    while (F.oCnxBase.Reader.Read())
                    {
                        fieldInf += ";" + F.oCnxBase.Reader.GetString(1) + "   (" + F.oCnxBase.Reader.GetString(0) + ")";
                        if (!F.oCnxBase.Reader.IsDBNull(2)) LstValue[idx1] = F.oCnxBase.Reader.GetString(2);
                    }
                    F.oCnxBase.CBReaderClose();
                    LstValue[idx] = fieldInf.Substring(1);
                }
                else F.oCnxBase.CBReaderClose();

            }
        }

        public void GetServerLinksApp()
        {
            int idx = GetIndexFromName("GuidApplication");
            int idx1 = GetIndexFromName("Image");
            if (idx > -1 && idx1 > -1)
            {
                string fieldInf = "";
                //string sQuery = "SELECT " + field + ".Guid" + field + ", Nom" + field + " FROM " + field + ", " + field + "Link WHERE GuidServerPhy ='" + GuidkeyObjet + "' and GuidVue='" + F.cbGuidVue.Items[F.cbVue1.SelectedIndex] + "' and " + field + ".Guid" + field + "=" + field + "Link.Guid" + field;
                string sQuery = "SELECT DISTINCT Application.GuidApplication, NomApplication, Image, Trigramme FROM Application, ApplicationLink WHERE GuidServerPhy ='" + GuidkeyObjet + "' and Application.GuidApplication=ApplicationLink.GuidApplication";
                string sqlString2 = " AND Application.GuidApplication IN (SELECT Application.GuidApplication FROM Vue, DansVue, GApplication, Application WHERE Vue.GuidGVue=DansVue.GuidGVue AND DansVue.GuidObjet=GApplication.GuidGApplication AND GApplication.GuidApplication=Application.GuidApplication AND Vue.GuidAppVersion='" + F.GetGuidAppVersion() + "' AND Vue.GuidTypeVue='d5b533a9-06ac-4f8c-a5ab-e345b0212542')";
                if (F.oCnxBase.CBRecherche(sQuery + sqlString2))
                {
                    while (F.oCnxBase.Reader.Read())
                    {
                        if (F.oCnxBase.Reader.IsDBNull(3)) fieldInf += ";" + F.oCnxBase.Reader.GetString(1) + "   (" + F.oCnxBase.Reader.GetString(0) + ")";
                        else fieldInf += ";[" + F.oCnxBase.Reader.GetString(3) + "]   (" + F.oCnxBase.Reader.GetString(0) + ")";
                        if (!F.oCnxBase.Reader.IsDBNull(2)) LstValue[idx1] = F.oCnxBase.Reader.GetString(2);
                    }
                    F.oCnxBase.CBReaderClose();
                    LstValue[idx] = fieldInf.Substring(1);
                }
                else F.oCnxBase.CBReaderClose();

            }
        }

        
        public void GetServerLinksApp(string field)
        {
            int idx = GetIndexFromName("Guid" + field);
            int idx1 = GetIndexFromName("Image");
            if (idx > -1 && idx1 > -1)
            {
                string fieldInf = "";
                string sQuery = "SELECT DISTINCT " + field + ".Guid" + field + ", NomFonction, Image FROM " + field + ", " + field + "Link, Fonction WHERE " + field + ".Guid" + field + "=" + field + "Link.Guid" + field + " and " + field + ".GuidMainFonction=Fonction.GuidFonction and GuidServerPhy ='" + GuidkeyObjet + "' ";
                string sqlString2 = " AND Server.GuidServer IN (SELECT Server.GuidServer FROM Vue, DansVue, GServer, Server WHERE Vue.GuidGVue=DansVue.GuidGVue AND DansVue.GuidObjet=GServer.GuidGServer AND GServer.GuidServer=Server.GuidServer AND Vue.GuidAppVersion='" + F.GetGuidAppVersion() + "' AND Vue.GuidTypeVue='d5b533a9-06ac-4f8c-a5ab-e345b0212542')";
                string sGuidServer = "";
                //string sQuery = "SELECT distinct " + field + ".Guid" + field + ", Nom" + field + ", IndexImgOS FROM " + field + ", " + field + "Link, Techno, TechnoRef  WHERE " + field + ".Guid" + field + "=" + field + "Link.Guid" + field + " and " + field + ".Guid" + field + "=Techno.Guid" + field + " and GuidServerPhy ='" + GuidkeyObjet + "' and Techno.GuidTechnoRef=TechnoRef.GuidTechnoRef";
                if (F.oCnxBase.CBRecherche(sQuery + sqlString2))
                {
                    while (F.oCnxBase.Reader.Read())
                    {
                        fieldInf += ";" + F.oCnxBase.Reader.GetString(1) + "   (" + F.oCnxBase.Reader.GetString(0) + ")";
                        sGuidServer = F.oCnxBase.Reader.GetString(0);
                        if (!F.oCnxBase.Reader.IsDBNull(2)) LstValue[idx1] = F.oCnxBase.Reader.GetString(2);
                    }
                    F.oCnxBase.CBReaderClose();
                    LstValue[idx] = fieldInf.Substring(1);
                    //sQuery = "SELECT IndexImgOS FROM Techno, TechnoRef  WHERE Techno.GuidTechnoRef=TechnoRef.GuidTechnoRef and GuidServer='" + sGuidServer + "' and IndexImgOS > 0";
                    sQuery = "SELECT IndexImgOS FROM ServerTypeLink, ServerType, Techno, TechnoRef  WHERE Techno.GuidTechnoRef=TechnoRef.GuidTechnoRef AND Techno.GuidTechnoHost=ServerType.GuidServerType AND ServerType.GuidServerType=ServerTypeLink.GuidServerType AND GuidServer='" + sGuidServer + "' and IndexImgOS > 0";
                    if (F.oCnxBase.CBRecherche(sQuery))
                    {
                        int r = F.oCnxBase.Reader.GetInt32(0);
                        SetValueFromName("IndexImgOS", (object)F.oCnxBase.Reader.GetInt32(0));
                    }
                    F.oCnxBase.CBReaderClose();
                }
                else F.oCnxBase.CBReaderClose();
            }
        }

        public override void AttachLink(DrawObject o, TypeAttach Attach)
        {
            string oParent = "GuidLocation";

            switch (Attach)
            {
                case TypeAttach.Parent:
                    SetValueFromName(oParent, o.GuidkeyObjet.ToString());
                    break;
            }
            base.AttachLink(o, Attach);
        }

        public override string GetsType(bool Reel)
        {
            if (Reel) return base.GetsType(Reel);
            return "ServerPhy";
        }
	}
}
