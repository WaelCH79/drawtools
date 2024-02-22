using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using MOI = Microsoft.Office.Interop;
 
using System.Xml;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawServerPhy : DrawTools.DrawRectangle
	{
        public int thickColor
        {
            get
            {
                return (int) this.GetValueFromName("thickColor");
            }
            set
            {
                this.InitProp("thickColor", (object) value, true);
            }
        }

        public int forme
        {
            get
            {
                return (int)this.GetValueFromName("Forme"); ;
            }
            set
            {
                this.InitProp("Forme", (object)value, true);
            }
        }

        public int CPUCoreA
        {
            get
            {
                return (int)this.GetValueFromName("CPUCoreA"); ;
            }
            set
            {
                this.InitProp("CPUCoreA", (object)value, true);
            }
        }

        public int RAMA
        {
            get
            {
                return (int)this.GetValueFromName("RAMA"); ;
            }
            set
            {
                this.InitProp("RAMA", (object)value, true);
            }
        }


        public DrawServerPhy()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawServerPhy(Form1 of, int x, int y, int width, int height, int count)
        {           
            F = of;
            OkMove = true;
            Align = true;
            ModeGraphic = modeGraphic.detail;
            Rectangle = new Rectangle(x, y, width, height);
            LstParent = new ArrayList(); 
            LstChild = new ArrayList();
            LstLinkIn = new ArrayList();
            LstLinkOut = new ArrayList();
            LstValue = new ArrayList();
            GuidkeyObjet = Guid.NewGuid();
            Texte = "Server" + count;
            Guidkey = Guid.NewGuid();
            InitProp();

            //Location par défaut
            string sguid = "97f0837b-2ee9-4e10-ac47-0dfc1e829dfc";
            string sVal = "N/A" + "   (" + sguid + ")";
            SetValueFromName("NomLocation", (object)sVal);
            SetValueFromName("GuidLocation", (object)sguid);
            SetValueFromName("NomDiskClass", (object)sVal);
            SetValueFromName("GuidDiskClass", (object)sguid);
            SetValueFromName("NomBackupClass", (object)sVal);
            SetValueFromName("GuidBackupClass", (object)sguid);
            SetValueFromName("NomExploitClass", (object)sVal);
            SetValueFromName("GuidExploitClass", (object)sguid);
            SetValueFromName("NomTechnoRef", (object)sVal);
            SetValueFromName("GuidTechnoRef", (object)sguid);


            Initialize();
        }

        public DrawServerPhy(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = true;
            Align = true;
            ModeGraphic = modeGraphic.detail;
            InitRectangle(lstValG);
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

            o = lstValG[GetIndexFromGName("Forme")];
            if (o != null) 
                SetValueFromName("Forme", o);

            o = lstValG[GetIndexFromGName("thickColor")];
            if (o != null)
                SetValueFromName("thickColor", o);

            o = lstValG[GetIndexFromGName("CPUCoreA")];
            if (o != null)
                //SetValueFromName("CPUCoreA", o);
                SetValueFromName("CPUCoreA", 1);

            o = lstValG[GetIndexFromGName("RAMA")];
            if (o != null)
                //SetValueFromName("RAMA", o);
                SetValueFromName("RAMA", 1);
            o = GetValueFromName("GuidLocation");
            if (o != null)
            {
                //object o1 = GetValueFromName("NomLocation");
                //string s = (string)o1 + "   (" + (string)o + ")";
                //SetValueFromName("NomLocation", (object)s);
            }
            o = GetValueFromName("GuidDiskClass");
            if (o != null)
            {
                //object o1 = GetValueFromName("NomDiskClass");
                //string s = (string)o1 + "   (" + (string)o + ")";
                //SetValueFromName("NomDiskClass", (object)s);
            }
            o = GetValueFromName("GuidBackupClass");
            if (o != null)
            {
                //object o1 = GetValueFromName("NomBackupClass");
                //string s = (string)o1 + "   (" + (string)o + ")";
                //SetValueFromName("NomBackupClass", (object)s);
            }
            o = GetValueFromName("GuidExploitClass");
            if (o != null)
            {
                //object o1 = GetValueFromName("NomExploitClass");
                //string s = (string)o1 + "   (" + (string)o + ")";
                //SetValueFromName("NomExploitClass", (object)s);
            }

            Initialize();
        }

        public void DeleteServerLink(string field)
        {
            ArrayList aServerLink = new ArrayList();

            int idx = GetIndexFromName("Guid" + field);
            if (idx > -1)
            {
                string sQuery = "SELECT " + field + ".Guid" + field + " FROM " + field + ", " + field + "Link WHERE " + field + ".Guid" + field + "=" + field + "Link.Guid" + field + " and GuidServerPhy ='" + GuidkeyObjet + "' and GuidVue ='" + F.GuidVue + "'";
                if (F.oCnxBase.CBRecherche(sQuery))
                {
                    while (F.oCnxBase.Reader.Read()) aServerLink.Add(F.oCnxBase.Reader.GetString(0));
                    F.oCnxBase.CBReaderClose();
                }
                else F.oCnxBase.CBReaderClose();
            }
            for (int i = 0; i < aServerLink.Count; i++)
                    F.oCnxBase.CBWrite("DELETE FROM " + field + "Link WHERE Guid" + field + "='" + (string)aServerLink[i] + "' AND GuidServerPhy ='" + GuidkeyObjet + "' AND GuidVue='" + F.GuidVue + "'");
        }

        public void GetServerLinks(string field)
        {
            int idx = GetIndexFromName("Guid" + field);
            int idx1 = GetIndexFromName("Image");
            if (idx > -1 && idx1 > -1)
            {
                string fieldInf = "";
                //string sQuery = "SELECT " + field + ".Guid" + field + ", Nom" + field + " FROM " + field + ", " + field + "Link WHERE " + field + ".Guid" + field +"=" + field + "Link.Guid" + field + " and GuidServerPhy ='" + GuidkeyObjet + "'";
                //string sQuery = "SELECT " + field + ".Guid" + field + ", Nom" + field + " FROM " + field + ", " + field + "Link WHERE " + field + ".Guid" + field + "=" + field + "Link.Guid" + field + " and GuidServerPhy ='" + GuidkeyObjet + "'";
                string sQuery = "SELECT " + field + ".Guid" + field + ", Nom" + field + ", Image FROM " + field + ", " + field + "Link WHERE GuidServerPhy ='" + GuidkeyObjet + "' and GuidVue='" + F.GuidVue + "' and " + field + ".Guid" + field + "=" + field + "Link.Guid" + field;
                if (F.oCnxBase.CBRecherche(sQuery))
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

        public override DrawArea.DrawToolType GetToolTypeForObjExp()
        {
            return DrawArea.DrawToolType.ServerPhy;
        }

        public void GetServerLinksApp(string field)
        {
            int idx = GetIndexFromName("Guid" + field);
            int idx1 = GetIndexFromName("Image");
            int idx2 = GetIndexFromName("Nom" + field);
            if (idx > -1 && idx1 > -1)
            {
                string fieldInf = "";
                string sQuery = "SELECT DISTINCT " + field + ".Guid" + field + ", NomFonction, Image, NomServer FROM " + field + ", " + field + "Link, Fonction WHERE " + field + ".Guid" + field +"=" + field + "Link.Guid" + field + " and " + field + ".GuidMainFonction=Fonction.GuidFonction and GuidServerPhy ='" + GuidkeyObjet + "' and GuidVue='" + F.GuidVue + "'";
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
                        if (!F.oCnxBase.Reader.IsDBNull(3)) LstValue[idx2] = F.oCnxBase.Reader.GetString(3);
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

        public void GetServerLinksApp()
        {
            int idx = GetIndexFromName("GuidApplication");
            int idx1 = GetIndexFromName("Image");
            if (idx > -1 && idx1 > -1)
            {
                string fieldInf = "";
                string sQuery = "SELECT Application.GuidApplication, NomApplication, Image, Trigramme FROM Application, ApplicationLink WHERE GuidServerPhy ='" + GuidkeyObjet + "' and GuidVue='" + F.GuidVue + "' and Application.GuidApplication=ApplicationLink.GuidApplication";
                if (F.oCnxBase.CBRecherche(sQuery))
                {
                    while (F.oCnxBase.Reader.Read())
                    {
                        if (F.oCnxBase.Reader.IsDBNull(3) || F.oCnxBase.Reader.GetString(3)=="") fieldInf += ";" + F.oCnxBase.Reader.GetString(1) + "   (" + F.oCnxBase.Reader.GetString(0) + ")";
                        else fieldInf += ";[" + F.oCnxBase.Reader.GetString(3) + "]   (" + F.oCnxBase.Reader.GetString(0) + ")";
                        if (!F.oCnxBase.Reader.IsDBNull(2)) LstValue[idx1] = F.oCnxBase.Reader.GetString(2);
                    }
                    F.oCnxBase.CBReaderClose();
                    LstValue[idx] = fieldInf.Substring(1);
                }
                else F.oCnxBase.CBReaderClose();
            }
        }

        public override void AttachLink(DrawObject o, TypeAttach Attach)
        {
            string oParent = "GuidCluster";

            switch (Attach)
            {
                case TypeAttach.Parent:
                    SetValueFromName(oParent, o.GuidkeyObjet.ToString());
                    break;
            }
            base.AttachLink(o, Attach);
        }

                
        public override void Draw(Graphics g)
        {
            string sTypeVue = F.tbTypeVue.Text; // (string)F.cbTypeVue.SelectedItem;
            int EmplacementYCard = HeightCard + 2 * Axe;

            ToolServerPhy to = (ToolServerPhy)F.drawArea.tools[(int)DrawArea.DrawToolType.ServerPhy];
            Pen pen = new Pen(to.LineCouleur, Math.Max(thickColor, to.LineWidth));
            Pen pen1 = new Pen(to.LineCouleur, to.Line1Width);
            SolidBrush brsh = new SolidBrush(to.Couleur);
            int yImg = 0, hImg = 0, wImg = 0, idxImg = 0, w = 0, wApp = 0;
            double dw;
            string ImgObj;
            Bitmap image2;

            Rectangle r;
            r = DrawRectangle.GetNormalizedRectangle(Rectangle);
            if (forme != 0 && forme !=4) ModeGraphic = modeGraphic.icon;
            else if (r.Width <= 20 || r.Height <= 10) ModeGraphic = modeGraphic.forme;
            else if (r.Width <= 60 || r.Height <= 70)
            {
                if (r.Height <= 30) ModeGraphic = modeGraphic.nom; else ModeGraphic = modeGraphic.resume;
            }
            else ModeGraphic = modeGraphic.detail;

            switch(ModeGraphic)
            {
                case modeGraphic.detail:
                    if (F.bPtt)
                    {
                        wImg = imgLogoWidth;
                        hImg = imgLogoHeight;
                        yImg = r.Bottom - hImg - HeightCard + 2 * Axe;
                        
                        AffRec(g, r, to.LineCouleur, to.LineWidth, LightColor(to.Couleur), 3, true, true, true);
                        ImgObj = (string)GetValueFromName("Image");
                        if (ImgObj == "") ImgObj = "defaut";
                        image2 = (Bitmap)Image.FromFile(F.sPathRoot + @"\bouton\" + ImgObj + ".png", true);
                        if (imgHeight * image2.Width / image2.Height <= imgWidth)
                            g.DrawImage(image2, new Rectangle(r.Left + Axe, r.Top + HeightCard - 2 * Axe, imgHeight * image2.Width / image2.Height, imgHeight));
                        else
                            g.DrawImage(image2, new Rectangle(r.Left + Axe, r.Top + HeightCard - 2 * Axe, imgWidth, imgWidth * image2.Height / image2.Width));

                        DrawGrpTxt(g, 4, 0, r.Left + Axe + imgWidth, r.Top + Axe + (imgHeight - HeightFont[3]) / 2, 0, Color.Black, Color.Transparent);

                        //Logo Type Server

                        string ImgSrvType = (string)GetValueFromName("Type"); //M/P/V/H/E
                        if (ImgSrvType != "")
                        {
                            Bitmap ImgSrv = (Bitmap)Image.FromFile(F.sPathRoot + @"\bouton\" + ImgSrvType + ".png", true);

                            if (hImg * ImgSrv.Width / ImgSrv.Height <= wImg)
                                g.DrawImage(ImgSrv, new Rectangle(r.Left + Axe + idxImg * wImg, yImg, hImg * ImgSrv.Width / ImgSrv.Height, hImg));
                            else
                                g.DrawImage(ImgSrv, new Rectangle(r.Left + Axe + idxImg * wImg, yImg, wImg, wImg * ImgSrv.Height / ImgSrv.Width));
                            idxImg++;
                        }

                        //Logo System
                        Bitmap ImgSystem = (Bitmap)Image.FromFile(F.sImgList[(int)Form1.ImgList.OS + Convert.ToInt32(GetValueFromName("IndexImgOS"))]);

                        if (hImg * ImgSystem.Width / ImgSystem.Height <= wImg)
                            g.DrawImage(ImgSystem, new Rectangle(r.Left + Axe + idxImg * wImg + Axe, yImg, hImg * ImgSystem.Width / ImgSystem.Height, hImg));
                        else
                            g.DrawImage(ImgSystem, new Rectangle(r.Left + Axe + idxImg * wImg + Axe, yImg, wImg, wImg * ImgSystem.Height / ImgSystem.Width));
                        idxImg++;

                    } else
                    {
                        wImg = imgLogoWidth - 5;
                        hImg = imgLogoHeight - 5;
                        yImg = r.Bottom - HeightCard - 2 * Axe - hImg;

                        string sType = (string)GetValueFromName("Type");  //M/P/V/H/E

                        //AffRec(g, r, LineCouleur, Couleur, 5, true, true, false, thickColor);
                        if (Convert.ToInt32(GetValueFromName("IndexImgOS")) != 0) wApp = 6;
                        w = Math.Max(Math.Max(thickColor, to.LineWidth), wApp);
                        dw = w;
                        AffRec(g, r, to, w);

                        g.DrawLine(pen1, r.Left, r.Top + HeightCard + 2 * Axe, r.Right, r.Top + HeightCard + 2 * Axe);
                        g.DrawLine(pen1, r.Left, r.Bottom - HeightCard - 2 * Axe, r.Right, r.Bottom - HeightCard - 2 * Axe);
                        g.FillRectangle(brsh, r.Left + (float)Math.Round(dw / 2, MidpointRounding.AwayFromZero), r.Top + HeightCard + 2 * Axe, r.Width - w, HeightServer);

                        DrawGrpTxt(g, 1, 0, r.Left + Axe, r.Top + HeightCard + 2 * Axe, 0, Color.White, Color.Transparent);
                        DrawGrpTxt(g, 2, 0, r.Left + Axe, r.Top + HeightCard + 2 * Axe + HeightNomServer, 0, Color.White, Color.Transparent);

                        switch (sTypeVue[0])
                        {
                            case '3': // 3-Production
                            case '5':
                            case '4':
                            case 'F':
                            case 'U':

                                if (sType != "" && sType[0] != 32)
                                {
                                    g.DrawLine(pen1, r.Left + r.Width * 1 / 2, r.Top + HeightCard + 2 * Axe + HeightServer, r.Left + r.Width * 1 / 2, r.Bottom - HeightCard);

                                    //DrawGrpTxt(g, 2, 0, r.Left + r.Width * 1 / 2 + 5, r.Top + HeightCard + HeightServer, 1, Color.Black);
                                    if (forme != 4)
                                        DrawGrpTxt(g, 3, 0, r.Left + r.Width * 1 / 2 + 5, r.Top + HeightCard + 2 * Axe + HeightServer, 1, Color.Black, Color.Transparent);
                                }
                                break;
                            case 'A':
                                break;
                        }

                        //Logo Role Server
                        ImgObj = (string)GetValueFromName("Image");
                        if (ImgObj == "") ImgObj = "defaut";
                        if (!System.IO.File.Exists(F.sPathRoot + @"\bouton\" + ImgObj + ".png")) ImgObj = "defaut";
                        image2 = (Bitmap)Image.FromFile(F.sPathRoot + @"\bouton\" + ImgObj + ".png", true);
                        if (hImg * image2.Width / image2.Height <= wImg)
                            g.DrawImage(image2, new Rectangle(r.Left + Axe + idxImg * wImg, yImg, hImg * image2.Width / image2.Height, hImg));
                        else
                            g.DrawImage(image2, new Rectangle(r.Left + Axe + idxImg * wImg, yImg, wImg, wImg * image2.Height / image2.Width));
                        idxImg++;

                        //Logo Type Server

                        string ImgSrvType = (string)GetValueFromName("Type"); //M/P/V/H/E
                        if (sType != "" && sType[0] != 32)
                        {
                            if (System.IO.File.Exists(F.sPathRoot + @"\bouton\" + ImgSrvType + ".png"))
                            {
                                Bitmap ImgSrv = (Bitmap)Image.FromFile(F.sPathRoot + @"\bouton\" + ImgSrvType + ".png", true);

                                if (hImg * ImgSrv.Width / ImgSrv.Height <= wImg)
                                    g.DrawImage(ImgSrv, new Rectangle(r.Left + Axe + idxImg * wImg, yImg, hImg * ImgSrv.Width / ImgSrv.Height, hImg));
                                else
                                    g.DrawImage(ImgSrv, new Rectangle(r.Left + Axe + idxImg * wImg, yImg, wImg, wImg * ImgSrv.Height / ImgSrv.Width));
                                idxImg++;
                            }
                        }


                        //Logo System
                        Bitmap ImgSystem = (Bitmap)Image.FromFile(F.sPathRoot + "\\bouton\\" + F.sImgList[(int)Form1.ImgList.OS + Convert.ToInt32(GetValueFromName("IndexImgOS"))]);

                        if (hImg * ImgSystem.Width / ImgSystem.Height <= wImg)
                            g.DrawImage(ImgSystem, new Rectangle(r.Left + Axe + idxImg * wImg + Axe, yImg, hImg * ImgSystem.Width / ImgSystem.Height, hImg));
                        else
                            g.DrawImage(ImgSystem, new Rectangle(r.Left + Axe + idxImg * wImg + Axe, yImg, wImg, wImg * ImgSystem.Height / ImgSystem.Width));
                        idxImg++;
                    }
                    break;
                case modeGraphic.resume:
                    if (Convert.ToInt32(GetValueFromName("IndexImgOS")) != 0) wApp = 6;
                    w = Math.Max(Math.Max(thickColor, to.LineWidth), wApp);
                    dw = w;
                    AffRec(g, r, to, w);
                    DrawGrpTxt(g, 1, 0, r.Left + Axe, r.Top + 2 * Axe + HeightCard, 0, Color.White, Color.Transparent);
                    DrawGrpTxt(g, 2, 0, r.Left + Axe, r.Top + 2 * Axe + HeightCard + HeightNomServer, 0, Color.White, Color.Transparent);
                    break;
                case modeGraphic.nom:
                    if (Convert.ToInt32(GetValueFromName("IndexImgOS")) != 0) wApp = 6;
                    w = Math.Max(Math.Max(thickColor, to.LineWidth), wApp);
                    dw = w;
                    AffRec(g, r, to, w);
                    DrawGrpTxt(g, 1, 0, r.Left + Axe, r.Top + Axe, 0, Color.White, Color.Transparent);
                    break;
                case modeGraphic.icon:
                    //wImg = r.Width;
                    //hImg = r.Height;
                    //Logo Role Server
                    ImgObj = (string)GetValueFromName("Image");
                    if (ImgObj == "") ImgObj = "defaut";
                    image2 = (Bitmap)Image.FromFile(F.sPathRoot + @"\bouton\" + ImgObj + ".png", true);

                    if (r.Height * image2.Width / image2.Height <= r.Width) rectangle.Width = r.Height * image2.Width / image2.Height;
                    else rectangle.Height = r.Width * image2.Height / image2.Width;

                    g.DrawImage(image2, new Rectangle(r.Left, r.Top, rectangle.Width, rectangle.Height));
                    int iWidth = DrawGrpTxt(null, 1, 0, 0, 0, 0, Color.Black, Color.Transparent);

                    DrawGrpTxt(g, 1, 0, r.Left + r.Width / 2 - iWidth / 2, r.Bottom, 0, Color.Black, Color.Transparent);
                    break;
                case modeGraphic.forme:
                    g.DrawRectangle(pen, r);
                    break;
                case modeGraphic.vide:
                    break;
            }

            AligneObjet();
            /*for (int i = 0; i < LstChild.Count; i++)
            {
                ((DrawObject)LstChild[i]).Draw(g);

            }*/


            pen.Dispose();
        }

        public override void VisioDraw(ArrayList lstGuid, ArrayList lstShape, MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
            ToolServerPhy to = (ToolServerPhy)F.drawArea.tools[(int)DrawArea.DrawToolType.ServerPhy];

            if (lstGuid.IndexOf(GuidkeyObjet.ToString()) == -1)
            {
                if (LstParent != null)
                    for (int ip = 0; ip < LstParent.Count; ip++)
                        if (lstGuid.IndexOf(((DrawObject)LstParent[ip]).GuidkeyObjet.ToString()) == -1)
                            ((DrawObject)LstParent[ip]).VisioDraw(lstGuid, lstShape, page, yPage, qxPage, qyPage);


                double yImg = 0, hImg = 0, wImg = 0, idxImg = 0;
                double ImgW = 0, ImgH = 0;
                wImg = (imgLogoWidth - 5) * qxPage;
                hImg = (imgLogoHeight - 5) * qyPage;
                yImg = yPage - (Rectangle.Bottom - HeightCard - 2 * Axe) * qyPage + hImg / 2;

                //Dessiner l'objet
                MOI.Visio.Shape shMain = page.DrawRectangle(Rectangle.Left * qxPage, yPage - Rectangle.Top * qyPage, Rectangle.Right * qxPage, yPage - Rectangle.Bottom * qyPage);
                MOI.Visio.Shape shBand = page.DrawRectangle(Rectangle.Left * qxPage, yPage - (Rectangle.Top + HeightCard + 2 * Axe) * qyPage, Rectangle.Right * qxPage, yPage - (Rectangle.Top + 2 * HeightCard) * qyPage);
                MOI.Visio.Shape shLineH = page.DrawRectangle(Rectangle.Left * qxPage, yPage - (Rectangle.Bottom - HeightCard + 2 * Axe) * qyPage, Rectangle.Right * qxPage, yPage - (Rectangle.Bottom - HeightCard) * qyPage);               

                //Inserer le texte + Couleur + taille
                DrawGrpTxt(shBand, 1, 0, 0, 0, 0, Color.White, Color.Transparent);
                string ImgSrvType = (string)GetValueFromName("Type"); //M/P/V/H/E
                if (ImgSrvType != "")
                {
                    MOI.Visio.Shape shLineV = page.DrawRectangle((Rectangle.Left + Rectangle.Width / 2) * qxPage, yPage - (Rectangle.Top + HeightCard) * qyPage, (Rectangle.Left + Rectangle.Width / 2) * qxPage, yPage - (Rectangle.Bottom - HeightCard) * qyPage);
                    shLineV.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                    MOI.Visio.Shape shTxt3 = page.DrawRectangle((Rectangle.Left + Rectangle.Width / 2) * qxPage, yPage - (Rectangle.Top + 2 * HeightCard) * qyPage, Rectangle.Right * qxPage, yPage - (Rectangle.Bottom - HeightCard) * qyPage);
                    DrawGrpTxt(shTxt3, 3, 0, 0, 0, 1, Color.Black, Color.Transparent);
                    shTxt3.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLinePattern).ResultIU = 0;
                    shTxt3.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillPattern).ResultIU = 0;
                }


                //r.Left + r.Width * 1 / 2 + 5, r.Top + HeightCard + HeightServer, 1, Color.Black);
                

                //shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionCharacter, (short)MOI.Visio.VisRowIndices.visRowCharacter, (short)MOI.Visio.VisCellIndices.vis1DBeginY).ResultIU = yPage - Rectangle.Top * qyPage;
                //Couleur trait
                shMain.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                shBand.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                shLineH.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";

                //Couleur Fond
                shMain.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillBkgnd).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                shMain.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillForegnd).FormulaU = "RGB(" + Color.White.R.ToString() + "," + Color.White.G.ToString() + "," + Color.White.B.ToString() + ")";
                shMain.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillPattern).ResultIU = 28;
                shBand.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillForegnd).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                //Arrondi
                shMain.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineRounding).FormulaU = "3 mm";

                //Logo Role Server
                string ImgObj = (string)GetValueFromName("Image");
                if (ImgObj == "") ImgObj = "defaut";
                MOI.Visio.Shape ImgRole = page.Import(F.sPathRoot + @"\bouton\" + ImgObj + ".png");
                ImgW = ImgRole.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormWidth).ResultIU;
                ImgH = ImgRole.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormHeight).ResultIU;

                if (hImg * ImgW / ImgH <= wImg)
                {
                    ImgRole.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormOut, (short)MOI.Visio.VisCellIndices.visXFormPinX).ResultIU = (Rectangle.Left + Axe) * qxPage + wImg / 2 + idxImg * wImg;
                    ImgRole.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormOut, (short)MOI.Visio.VisCellIndices.visXFormPinY).ResultIU = yImg;
                    ImgRole.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormWidth).ResultIU = hImg * ImgW / ImgH;
                    ImgRole.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormHeight).ResultIU = hImg;
                }
                else
                {
                    ImgRole.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormOut, (short)MOI.Visio.VisCellIndices.visXFormPinX).ResultIU = (Rectangle.Left + Axe) * qxPage + wImg / 2 + idxImg * wImg;
                    ImgRole.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormOut, (short)MOI.Visio.VisCellIndices.visXFormPinY).ResultIU = yImg;
                    ImgRole.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormWidth).ResultIU = wImg;
                    ImgRole.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormHeight).ResultIU = wImg * ImgH / ImgW;

                }
                idxImg++;

                //Logo Type Server
                if (ImgSrvType != "")
                {
                    MOI.Visio.Shape ImgType = page.Import(F.sPathRoot + @"\bouton\" + ImgSrvType + ".png");
                    ImgW = ImgType.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormWidth).ResultIU;
                    ImgH = ImgType.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormHeight).ResultIU;

                    if (hImg * ImgW / ImgH <= wImg)
                    {
                        ImgType.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormOut, (short)MOI.Visio.VisCellIndices.visXFormPinX).ResultIU = (Rectangle.Left + Axe) * qxPage + wImg / 2 + idxImg * wImg;
                        ImgType.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormOut, (short)MOI.Visio.VisCellIndices.visXFormPinY).ResultIU = yImg;
                        ImgType.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormWidth).ResultIU = hImg * ImgW / ImgH;
                        ImgType.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormHeight).ResultIU = hImg;
                    }
                    else
                    {
                        ImgType.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormOut, (short)MOI.Visio.VisCellIndices.visXFormPinX).ResultIU = (Rectangle.Left + Axe) * qxPage + wImg / 2 + idxImg * wImg;
                        ImgType.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormOut, (short)MOI.Visio.VisCellIndices.visXFormPinY).ResultIU = yImg;
                        ImgType.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormWidth).ResultIU = wImg;
                        ImgType.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormHeight).ResultIU = wImg * ImgH / ImgW;

                    }
                    idxImg++;
                }

                //Logo System
                MOI.Visio.Shape ImgSys = page.Import(F.sPathRoot + @"\bouton\" + F.sImgList[(int)Form1.ImgList.OS + Convert.ToInt32(GetValueFromName("IndexImgOS"))]);
                ImgW = ImgSys.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormWidth).ResultIU;
                ImgH = ImgSys.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormHeight).ResultIU;

                if (hImg * ImgW / ImgH <= wImg)
                {
                    ImgSys.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormOut, (short)MOI.Visio.VisCellIndices.visXFormPinX).ResultIU = (Rectangle.Left + Axe) * qxPage + wImg / 2 + idxImg * wImg;
                    ImgSys.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormOut, (short)MOI.Visio.VisCellIndices.visXFormPinY).ResultIU = yImg;
                    ImgSys.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormWidth).ResultIU = hImg * ImgW / ImgH;
                    ImgSys.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormHeight).ResultIU = hImg;
                }
                else
                {
                    ImgSys.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormOut, (short)MOI.Visio.VisCellIndices.visXFormPinX).ResultIU = (Rectangle.Left + Axe) * qxPage + wImg / 2 + idxImg * wImg;
                    ImgSys.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormOut, (short)MOI.Visio.VisCellIndices.visXFormPinY).ResultIU = yImg;
                    ImgSys.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormWidth).ResultIU = wImg;
                    ImgSys.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormHeight).ResultIU = wImg * ImgH / ImgW;

                }
                idxImg++;




                lstShape.Add(shMain);
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
        /// Save Object to the Data Base
        /// </summary>
        public override void savetoDB()
        {
            if (!savetoDBFait())
            {
                // Creation Enreg ServerLink Server <-> ServerPhy

                //DeleteServerLink("User");
                //DeleteServerLink("Application");
                //DeleteServerLink("Server");

                base.savetoDB();
                SetServerLinks("GuidAppUser");
                SetServerLinks("GuidApplication");
                SetServerLinks("GuidServer");
                //Label
                SetLabelLinks();

                savetoDBOK();
            }
        }

        public void SetServerLinks(string field)
        {
            List<string[]> lstLink = new List<string[]>();
            object o = GetValueFromName(field);

            if (F.oCnxBase.CBRecherche("Select GuidServerPhy, GuidVue, " + field + " From " + field.Substring("Guid".Length) + "Link  Where GuidServerPhy='" + GuidkeyObjet + "' and GuidVue='" + F.GuidVue + "'")) {
                while(F.oCnxBase.Reader.Read())
                {
                    string [] sTabCur = new string[4];
                    sTabCur[0] = F.oCnxBase.Reader.GetString(0);
                    sTabCur[1] = F.oCnxBase.Reader.GetString(1);
                    sTabCur[2] = F.oCnxBase.Reader.GetString(2);
                    sTabCur[3] = " ";
                    lstLink.Add(sTabCur);
                }
            }
            F.oCnxBase.CBReaderClose();

            if (o != null)
            {
                string Link = (string)o;
                if (Link != "")
                {
                    string[] aLink = Link.Split(new Char[] { '(', ')' });
                    for (int i = 1; i < aLink.Length; i += 2)
                    {
                        string[] sTabCur;
                        sTabCur = lstLink.Find(elFind => elFind[2] == aLink[i].Trim());
                        if (sTabCur != null) sTabCur[3] = "x";
                        if (!F.oCnxBase.ExistServerLink(field, GuidkeyObjet.ToString(), F.GuidVue.ToString(), aLink[i].Trim()))
                            F.oCnxBase.CBWrite("INSERT INTO " + field.Substring("Guid".Length) + "Link (GuidServerPhy, GuidVue, " + field + ") VALUES ('" + GuidkeyObjet + "','" + F.GuidVue + "','" + aLink[i].Trim() + "')");
                    }
                    for(int i=0; i<lstLink.Count; i++)
                    {
                        if(lstLink[i][3] !="x")
                            F.oCnxBase.CBWrite("DELETE FROM " + field.Substring("Guid".Length) + "Link WHERE GuidServerPhy='" + lstLink[i][0] + "' AND GuidVue ='" + lstLink[i][1] + "' AND " + field + "='" + lstLink[i][2] + "'");
                    }
                }
            }
        }

        public override XmlElement savetoXml(XmlDB xmlDB, bool GObj)
        {
            XmlElement elo = base.savetoXml(xmlDB, GObj);
            string[] aObjet = {  "AppUser" , "Application", "Server" };

            if (elo != null)
            {
                //aObjetLink
                for (int i = 0; i < aObjet.Length; i++)
                {
                    List<string> lstGuidObjLink = new List<string>();
                    Guid guid = F.GuidVue;

                    if (F.oCnxBase.CBRecherche("Select Guid" + aObjet[i] + " FROM " + aObjet[i] + "Link WHERE GuidServerPhy='" + GuidkeyObjet.ToString() + "' AND GuidVue='" + guid.ToString() + "'"))
                        while (F.oCnxBase.Reader.Read()) lstGuidObjLink.Add(F.oCnxBase.Reader.GetString(0));
                    F.oCnxBase.CBReaderClose();

                    for(int j=0; j < lstGuidObjLink.Count; j++)
                    { 
                        XmlElement el = xmlDB.XmlCreatEl(xmlDB.XmlGetFirstElFromParent(elo, "After"), aObjet[i] + "Link", "GuidServerPhy,GuidVue,Guid" + aObjet[i]);
                        XmlElement elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");
                        xmlDB.XmlSetAttFromEl(elAtts, "GuidServerPhy", "s", GuidkeyObjet.ToString());
                        xmlDB.XmlSetAttFromEl(elAtts, "GuidVue", "s", guid.ToString());
                        xmlDB.XmlSetAttFromEl(elAtts, "Guid" + aObjet[i], "s", lstGuidObjLink[j]);
                        if (aObjet[i] == "Server")
                        {
                            Table t;
                            int n = F.oCnxBase.ConfDB.FindTable("Package");
                            if (n > -1)
                            {
                                List<ExpObj> lstPackage = new List<ExpObj>();
                                t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                                if (F.oCnxBase.CBRecherche("Select " + t.GetKey(false) + " FROM MCompApp, MCompServ, Package Where MCompApp.GuidServer = '" + lstGuidObjLink[j] + "' and Package.GuidVue = '" + guid.ToString() + "' and MCompApp.GuidMainComposant = MCompServ.GuidMainComposant and MCompserv.GuidMCompServ = Package.GuidMCompServ"))
                                {
                                    
                                    while (F.oCnxBase.Reader.Read())
                                    {
                                        List<string> lstKey = new List<string>();
                                        lstKey.Add(F.oCnxBase.Reader.GetString(0));
                                        lstKey.Add(F.oCnxBase.Reader.GetString(1));
                                        lstKey.Add(F.oCnxBase.Reader.GetString(2));
                                        ExpObj eo = new ExpObj(lstKey, "Package", DrawArea.DrawToolType.Package);
                                        lstPackage.Add(eo);
                                    }
                                }
                                F.oCnxBase.CBReaderClose();
                                XmlElement elSaveCursor = xmlDB.GetCursor();
                                xmlDB.CursorClose();
                                if (xmlDB.SetCursor(xmlDB.XmlGetFirstElFromParent(el, "After")))
                                {
                                    for (int k = 0; k < lstPackage.Count; k++)
                                    {
                                        ExpObj eo = lstPackage[k];
                                        eo.setConfTable(F.drawArea);
                                        F.oCureo = eo;

                                        F.drawArea.tools[(int)eo.ObjTool].LoadSimpleObjectSansGraph(eo.lstKeyObj, eo.confTable);

                                        if (eo.oDraw != null)
                                        {
                                            XmlElement elodp = eo.oDraw.XmlCreatObject(xmlDB);
                                        }
                                        F.oCureo = null;
                                    }
                                }
                                xmlDB.CursorClose();
                                xmlDB.SetCursor(elSaveCursor);

                                /*
                                if (F.oCnxBase.CBRecherche("Select " + t.GetSelectField(ConfDataBase.FieldOption.Select) + " FROM MCompApp, MCompServ, Package Where MCompApp.GuidServer = '" + lstGuidObjLink[j] + "' and Package.GuidVue = '" + guid.ToString() + "' and MCompApp.GuidMainComposant = MCompServ.GuidMainComposant and MCompserv.GuidMCompServ = Package.GuidMCompServ"))
                                {
                                    while (F.oCnxBase.Reader.Read())
                                    {
                                        el = xmlDB.XmlCreatEl(xmlDB.XmlGetFirstElFromParent(elo, "After"), "Package", t.GetKey());
                                        elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");
                                        System.Data.Odbc.OdbcDataReader r = F.oCnxBase.Reader;

                                        for (int k = 0; k < r.FieldCount; k++)
                                        {
                                            if (!F.oCnxBase.Reader.IsDBNull(k))
                                            {
                                                switch (r.GetFieldType(k).Name)
                                                {
                                                    case "String": xmlDB.XmlSetAttFromEl(elAtts, r.GetName(k), "s", r.GetString(k));
                                                        break;
                                                    case "Int32": xmlDB.XmlSetAttFromEl(elAtts, r.GetName(k), "i", r.GetInt32(k).ToString());
                                                        break;
                                                    case "Double": xmlDB.XmlSetAttFromEl(elAtts, r.GetName(k), "d", r.GetDouble(k).ToString());
                                                        break;
                                                    case "DateTime": xmlDB.XmlSetAttFromEl(elAtts, r.GetName(k), "t", r.GetDate(k).ToShortDateString());
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                }
                                F.oCnxBase.CBReaderClose();
                                */
                            }
                        }
                    }
                }
                return elo;
            }
            return null;
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

        public int NbrNCard(char c)
        {
            int CountObj = 0;

            switch (c)
            {
                case 'N':
                case '3':// 3-Production
                case '5':
                case '4':
                case 'F':
                case 'U':
                    for (int i = 0; i < LstChild.Count; i++)
                        if (LstChild[i].GetType() == typeof(DrawNCard)) CountObj++;
                    break;
                case 'S':
                case 'A':
                case '9':
                    for (int i = 0; i < LstChild.Count; i++)
                        if (LstChild[i].GetType() == typeof(DrawSanCard)) CountObj++;
                    break;
            }

            return CountObj;
        }

        public void LoadNCard(string sGuid)
        {
            int j = F.drawArea.GraphicsList.FindObjet(0, sGuid);
            if (j < 0)
            {
                F.drawArea.tools[(int)DrawArea.DrawToolType.NCard].LoadSimpleObject(sGuid);
                j = F.drawArea.GraphicsList.FindObjet(0, sGuid);

                if (j > -1)
                {
                    DrawNCard dnc = (DrawNCard)F.drawArea.GraphicsList[j];

                    AttachLink(dnc, DrawObject.TypeAttach.Child);
                    dnc.AttachLink(this, DrawObject.TypeAttach.Parent);
                }
            }
        }

        public void Aligne(int x, int y, int width, int height)
        {
            Rectangle = new Rectangle(x, y, width, height);
            AligneObjet();
        }
        public override int GetTopYNCard()
        {
            return YMin() + Axe;
        }

        public override int GetBottomYNCard()
        {
            return YMax() - Axe;
        }

        public void AligneObjet()
        {
            string sTypeVue = F.tbTypeVue.Text; // (string)F.cbTypeVue.SelectedItem;

            int CountNCard = NbrNCard(sTypeVue[0]);
            if(CountNCard!=0)
            {
                int WidthObjet = (Rectangle.Width - 2 * radius - Axe) / CountNCard, HeightObjet = HeightCard - 2 * Axe;

                switch (sTypeVue[0])
                {
                    case '3': // 3-Production
                    case '5':
                    case '4':
                    case 'F':
                    case 'U':
                        for (int i = LstChild.Count - 1; i >= 0; i--)
                        {
                            if (LstChild[i].GetType() == typeof(DrawNCard))
                            {
                                int y = YMin() + Axe;
                                int Hauteur = ((DrawNCard)LstChild[i]).Hauteur;
                                if(Hauteur > 0) y = YMax() - Axe - EpaisseurCard;
                                if (WidthObjet <= Axe*3)
                                    ((DrawNCard)LstChild[i]).Aligne(Rectangle.Left, Rectangle.Top, 2, 2);
                                ((DrawNCard)LstChild[i]).Aligne(Rectangle.Left + WidthObjet * (CountNCard - 1) + radius + Axe, y, WidthObjet - Axe, EpaisseurCard);
                                CountNCard--;
                            }
                        }
                        break;
                    case 'A':
                    case '9':
                        for (int i = LstChild.Count - 1; i >= 0; i--)
                        {
                            if (LstChild[i].GetType() == typeof(DrawSanCard))
                            {
                                ((DrawSanCard)LstChild[i]).Aligne(Rectangle.Left + WidthObjet * (CountNCard - 1) + radius + Axe, Rectangle.Top + Axe, WidthObjet - Axe, HeightObjet);
                                CountNCard--;
                            }
                        }
                        break;
                }
            }
        }

        public override void dataGrid_CellClick(DataGridView odgv, DataGridViewCellEventArgs e)
        {
            //if (odgv.CurrentCell.RowIndex == 2) // Ligne Link Applicatif
            int n;
            
            n = GetIndexFromName("NomLocation");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromTv("4Localisation");
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }

            n = GetIndexFromName("GuidAppUser");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                if (!F.bDevelop[0] && F.sGuidVueInf != null) F.ObjetLiesDevelop();
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
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }

            n = GetIndexFromName("GuidServer");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormDeployComposant fdc = new FormDeployComposant(F, odgv);
                fdc.AddlSourceFromDB("Select Server.GuidServer, NomServer From Server, Vue, DansVue, GServer Where Vue.GuidVue='" + F.sGuidVueInf + "'AND Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGServer AND GServer.GuidServer=Server.GuidServer");
                fdc.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fdc.InitPackage(GuidkeyObjet, Texte);
                fdc.init();
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
            n = GetIndexFromName("NomDiskClass");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromDB("Select GuidDiskClass, NomDiskClass From DiskClass", "Value");
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }
            n = GetIndexFromName("NomBackupClass");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromDB("Select GuidBackupClass, NomBackupClass From BackupClass", "Value");
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }
            n = GetIndexFromName("NomExploitClass");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromDB("Select GuidExploitClass, NomExploitClass From ExploitClass", "Value");
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }
            n = GetIndexFromName("NomTechnoRef");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromTv("Patrimoine");
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }
            n = GetIndexFromName("Indicator");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormIndicator fi = new FormIndicator(F, GuidkeyObjet.ToString());
                fi.ShowDialog(F);
            }
        }

        /*
        public override void ExternProperty(string Name, Microsoft.Office.Interop.Word.Table tb, int iRow)
        {
            if (Name == "IPAddress")
            {
                int n = F.oCnxBase.ConfDB.FindTable("NCard");
                if (n > -1)
                {
                    Table t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                    object numCard = LstChild.Count+1;
                    object PropCard = t.GetNbrTabField();
                    object stBold = "dtBold";

                    if ((int)numCard != 0)
                    {
                        tb.Rows.Add(ref missing);
                        tb.Cell(iRow, 1).Range.Text = "IP Address";
                        tb.Cell(iRow, 2).Split(ref numCard, ref PropCard);

                        int j = 0;
                        for (int k = 0; k < t.LstField.Count; k++)
                        {
                            if ((((Field)t.LstField[k]).fieldOption & ConfDataBase.FieldOption.TabNonVisible) == 0)
                            {
                                tb.Cell(iRow, 2 + j).Range.Text = ((Field)t.LstField[k]).Libelle;
                                tb.Cell(iRow, 2 + j).Range.set_Style(ref stBold);
                                j++;
                            }
                        }
                        
                        for (int i = 1; i < (int)numCard; i++)
                        {
                            DrawNCard oNCard = (DrawNCard)LstChild[i-1];
                            j = 0;
                            for (int k = 0; k < t.LstField.Count; k++)
                            {
                                if ((((Field)t.LstField[k]).fieldOption & ConfDataBase.FieldOption.TabNonVisible) == 0)
                                {*/
                                    /*switch (((Field)t.LstField[k]).Type)
                                    {
                                        case 's':
                                            tb.Cell(iRow + i, 2 + j).Range.Text = (string)oNCard.LstValue[k];
                                            break;
                                        case 'i':
                                            tb.Cell(iRow + i, 2 + j).Range.Text = (int)oNCard.LstValue[k];
                                            break;
                                        case 'd':
                                            if (Init) LstValue[i] = (double)o;
                                            else LstValue[i] = (double)LstValue[i] + (double)o;
                                            break;
                                    }*/
                                    /*tb.Cell(iRow + i , 2 + j).Range.Text = oNCard.LstValue[k].ToString();
                                    j++;
                                }
                            }

                        }
                    }
                }
            }
        }
        */

        public override void CWInsert(ControlDoc cw, char cTypeVue)
        {
            if (cTypeVue == '3' || cTypeVue == '5' || cTypeVue == '4' || cTypeVue == 'U')
            {

                object o = null;
                string sType = GetType().Name.Substring("Draw".Length);
                string sPackage = "p";
                Guid guid = F.GuidVue;

                sType = "AppExt";
                o = GetValueFromName("GuidAppUser"); if (o != null && (string)o!="") sType =  "AppUser" ;
                o = GetValueFromName("GuidApplication"); if (o != null && (string)o != "") sType = "AppExt";
                o = GetValueFromName("GuidServer"); if (o != null && (string)o != "") sType = "Server";

                string sGuidVue = guid.ToString().Replace("-", "");
                string sServerBook = sType + sGuidVue;
                string sGuidKey = GuidkeyObjet.ToString().Replace("-", "");
                string sObj = sGuidVue.Substring(0, 16) + sGuidKey.Substring(0, 16);
                string sBook = sType.Substring(0, 3) + sObj;
                string sGuid = "n" + cTypeVue + sBook;


                if (cw.Exist(sGuid) > -1)
                {
                    cw.InsertTextFromId(sGuid, true, Texte, "Titre 4");
                    cw.InsertTabFromId("n" + sBook, true, this, null, false, null);
                }
                else if (cw.Exist(sServerBook) > -1)
                {
                    
                    //sType ne doit pas depasse 4 caracteres
                    cw.InsertTextFromId(sServerBook, false, "\n", null);
                    cw.CreatIdFromIdP(sBook, sServerBook);
                    cw.InsertTextFromId(sBook, true, Texte + "\n", "Titre 4");
                    cw.CreatIdFromIdP(sGuid, sBook);

                    CWInsertProp(cw, sBook, "P");

                    cw.InsertTextFromId(sBook, false, "Specifications\n", "Titre 6");
                    cw.InsertTextFromId(sBook, false, "\n", null);
                    cw.CreatIdFromIdP("n" + sBook, sBook);
                    cw.InsertTextFromId("n" + sBook, false, "\n", null);
                    cw.InsertTabFromId("n" + sBook, false, this, null, false, null);
                    if(sType == "Server")
                    {
                        string[] aServer = ((string)o).Split(new Char[] { '(', ')' });
                        for (int i = 1; i < aServer.Length; i += 2)
                        {
                            F.oCnxBase.ConfDB.AddTabPackage(); //Chargement Table specifique
                            int n = F.oCnxBase.ConfDB.FindTable("TabPackage");
                            if (n > -1)
                            {
                                Table t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                                
                                if (F.oCnxBase.CBRecherche("Select MainComposantRef.GuidMainComposantRef," + t.GetSelectField(ConfDataBase.FieldOption.Select) + " From Param, Package, MCompServ, MainComposant, MainComposantRef, ProduitApp Where Param.GuidParam = Package.GuidParam and GuidVue = '" + F.GuidVue + "' and Package.GuidMCompServ = MCompServ.GuidMCompServ and MCompServ.GuidMainComposant = MainComposant.GuidMainComposant and MCompServ.GuidMainComposantRef = MainComposantRef.GuidMainComposantRef and MainComposantRef.GuidProduitApp = ProduitApp.GuidProduitApp and MainComposant.GuidMainComposant in (Select GuidMainComposant From MCompApp Where GuidServer = '" + aServer[i] + "' ) Order By MainComposantRef.GuidMainComposantRef"))
                                {
                                    string sGuidComposantRef = "";
                                    string sTabBookmark = "";

                                    while (F.oCnxBase.Reader.Read())
                                    {
                                        if(sGuidComposantRef != F.oCnxBase.Reader.GetString(0))
                                        {
                                            sGuidComposantRef = F.oCnxBase.Reader.GetString(0);
                                            sTabBookmark = sPackage + sGuidVue.Substring(0, 16) + sGuidComposantRef.Replace("-","").Substring(0,16);
                                            cw.InsertTextFromId(sBook, false, "Package: " + F.oCnxBase.Reader.GetString(2) + "\n", "Titre 6");
                                            cw.InsertTextFromId(sBook, false, "\n", null);
                                            cw.CreatIdFromIdP(sTabBookmark, sBook);
                                            cw.InsertTextFromId(sTabBookmark, true, "\n", null);
                                            cw.InsertHeadTabFromId(sTabBookmark, false, t, null);
                                        }

                                        ArrayList LstValue = new ArrayList();
                                        LstValue = t.InitValueFieldFromBD(F.oCnxBase.Reader, ConfDataBase.FieldOption.Select);
                                        DrawTabPackage oTabPackage = new DrawTabPackage(F, LstValue);
                                        string sValue = (string) oTabPackage.GetValueFromName("Value");
                                        char[] separator = new char[2];
                                        separator[0] = '['; separator[1] = ']';
                                        string[] astr = sValue.Split(separator);
                                        for(int j=1; j <astr.Length; j+=2) astr[j] = (string)this.GetValueFromName(astr[j]);
                                        sValue = "";
                                        for (int j = 0; j < astr.Length; j++) sValue += astr[j];
                                        oTabPackage.SetValueFromName("Value", sValue);
                                        cw.InsertRowFromId(sTabBookmark, oTabPackage);
                                    }
                                    F.oCnxBase.CBReaderClose();
                                }
                                else F.oCnxBase.CBReaderClose();
                            }
                        }
                        
                    }

                }
            }
        }

	}
}
