using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using MOI = Microsoft.Office.Interop;
 
using System.Data.Odbc;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
    public class DrawSeqFluxFonc : DrawTools.DrawObject
    {
        public List<xmlFlux> lstxmlFlux;
        public List<String[]> lstServer;
        public int yRelatif, xRelatif;
        public XmlExcel xmlFluxTech;
        private int xMax;
        private int yMax;

        public DrawSeqFluxFonc()
        {
            Initialize();
        }

        

        public DrawSeqFluxFonc(Form1 of, List<xmlFlux> lstFlux, int y, XmlExcel xml, List<String[]> lstS)
        {
            F = of;
            lstxmlFlux = lstFlux;
            lstServer = lstS;
            xmlFluxTech = xml;
            yRelatif = y;
            xRelatif = AXE;
            xMax = 0; yMax = 0;
            GuidkeyObjet = Guid.NewGuid();

            Initialize();

        }

        public override int YMax()
        {
            return yMax;
        }

        public override int XMax()
        {
            return xMax;
        }

        public string getStringNom(XmlExcel xmlFluxTech, XmlElement node, bool bWithNom)
        {
            string espace = " - ";
            string s = "";
            

            ArrayList lstUser = xmlFluxTech.XmlGetLstElFromName(node, "User");
            ArrayList lstApplication = xmlFluxTech.XmlGetLstElFromName(node, "Application");
            ArrayList lstServer = xmlFluxTech.XmlGetLstElFromName(node, "Server");

            for(int i = 0; i < lstUser.Count; i++)
            {
                if(((XmlElement)lstUser[i]).GetAttribute("Selected") == "Yes")
                {
                    ArrayList lstNCard = xmlFluxTech.XmlGetLstElFromName(((XmlElement)lstUser[i]), "NCard");
                    for (int j = 0; j < lstNCard.Count; j++)
                    {
                        if (((XmlElement)lstNCard[j]).GetAttribute("Selected") == "Yes")
                        {
                            s += espace + ((XmlElement)lstNCard[j]).GetAttribute("Nom");
                            if (bWithNom)
                            {
                                string sA = "";
                                ArrayList lstAlias = xmlFluxTech.XmlGetLstElFromName(((XmlElement)lstNCard[j]), "Alias");
                                for (int k = 0; k < lstAlias.Count; k++)
                                {
                                    if (((XmlElement)lstAlias[k]).GetAttribute("Selected") == "Yes")
                                    {
                                        sA += espace + ((XmlElement)lstAlias[k]).GetAttribute("Nom");
                                    }
                                }
                                if (sA != "") s += " (" + sA.Substring(espace.Length) + ")";
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < lstApplication.Count; i++)
            {
                if (((XmlElement)lstApplication[i]).GetAttribute("Selected") == "Yes")
                {
                    ArrayList lstNCard = xmlFluxTech.XmlGetLstElFromName(((XmlElement)lstApplication[i]), "NCard");
                    for (int j = 0; j < lstNCard.Count; j++)
                    {
                        if (((XmlElement)lstNCard[j]).GetAttribute("Selected") == "Yes")
                        {
                            s += espace + ((XmlElement)lstNCard[j]).GetAttribute("Nom");
                            if (bWithNom)
                            {
                                string sA = "";
                                ArrayList lstAlias = xmlFluxTech.XmlGetLstElFromName(((XmlElement)lstNCard[j]), "Alias");
                                for (int k = 0; k < lstAlias.Count; k++)
                                {
                                    if (((XmlElement)lstAlias[k]).GetAttribute("Selected") == "Yes")
                                    {
                                        sA += espace + ((XmlElement)lstAlias[k]).GetAttribute("Nom");
                                    }
                                }
                                if (sA != "") s += " (" + sA.Substring(espace.Length) + ")";
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < lstServer.Count; i++)
            {
                if (((XmlElement)lstServer[i]).GetAttribute("Selected") == "Yes")
                {
                    ArrayList lstNCard = xmlFluxTech.XmlGetLstElFromName(((XmlElement)lstServer[i]), "NCard");
                    for (int j = 0; j < lstNCard.Count; j++)
                    {
                        if (((XmlElement)lstNCard[j]).GetAttribute("Selected") == "Yes")
                        {
                            s += espace + ((XmlElement)lstNCard[j]).GetAttribute("Nom");
                            if (bWithNom)
                            {
                                string sA = "";
                                ArrayList lstAlias = xmlFluxTech.XmlGetLstElFromName(((XmlElement)lstNCard[j]), "Alias");
                                for (int k = 0; k < lstAlias.Count; k++)
                                {
                                    if (((XmlElement)lstAlias[k]).GetAttribute("Selected") == "Yes")
                                    {
                                        sA += espace + ((XmlElement)lstAlias[k]).GetAttribute("Nom");
                                    }
                                }
                                if (sA != "") s += " (" + sA.Substring(espace.Length) + ")";
                            }
                        }
                    }
                }
            }

            if (s != "") return s.Substring(espace.Length);
            return s;
        }



        public override void Draw(Graphics g)
        {
            if (lstServer.Count > 1)
            {
                //HeightFlux = 30;
                //WidthFlux = 100;
                
                Pen pen = new Pen(Color, 1);
                Pen penGridServer = new Pen(Color.LightGray, 1);
                Pen penFlux = new Pen(Color.DarkBlue, 2);

                int y = yRelatif;
                int idxFont = 2;
                int ifont = HeightFont[idxFont];
                Font nameFont = new Font("Arial", ifont);

                // initialisation distance internserver
                int xdebServer = xRelatif + 2 * WidthFlux + WidthFluxId + WidthFluxService + 10;
                int widthInterServer = (WidthFluxMaxDefault - xdebServer) / (lstServer.Count - 1);
                if (widthInterServer < WidthFluxMinIntervaleServer) widthInterServer = WidthFluxMinIntervaleServer;

                // trace des lignes
                g.DrawLine(pen, xRelatif, yRelatif, xRelatif + 2 * WidthFlux + WidthFluxId + WidthFluxService, yRelatif);
                g.DrawLine(penGridServer, xdebServer, yRelatif, xdebServer + widthInterServer * (lstServer.Count - 1), yRelatif);

                for (int i = 0; i < lstxmlFlux.Count; i++)
                {
                    int ydebFonc = y;
                    for (int j = 0; j < lstxmlFlux[i].lstElChild.Count; j++)
                    {
                        int ydebApp = y;
                        for (int k = 0; k < lstxmlFlux[i].lstElChild[j].lstElChild.Count; k++)
                        {
                            XmlElement ftech = lstxmlFlux[i].lstElChild[j].lstElChild[k].elFlux;

                            //g.DrawString(ftech.GetAttribute("Id"), nameFont, new SolidBrush(Color.Black), xRelatif + 2 * WidthFlux + AXE, y + (HeightFlux - ifont) / 2);
                            //g.DrawString(((XmlElement)ftech.LastChild).GetAttribute("Nom"), nameFont, new SolidBrush(Color.Black), xRelatif + 2 * WidthFlux + WidthFluxId + AXE, y + (HeightFlux - ifont) / 2);
                            DrawGrpTxt(g, idxFont, ftech.GetAttribute("Id"), xRelatif + 2 * WidthFlux + AXE, y + (HeightFlux - ifont) / 2, Color.Black);
                            DrawGrpTxt(g, idxFont, ((XmlElement)ftech.LastChild).GetAttribute("Nom"), xRelatif + 2 * WidthFlux + WidthFluxId + AXE, y + (HeightFlux - ifont) / 2, Color.Black);

                            XmlElement nodeOrigine = xmlFluxTech.XmlGetFirstElFromName(ftech, "Origine");
                            int xOrigine = xdebServer + widthInterServer * lstServer.FindIndex(el => el[0] == nodeOrigine.GetAttribute("Guid"));
                            string sOrigne = getStringNom(xmlFluxTech, nodeOrigine, false);
                            SizeF sSizeOrigine = new SizeF();
                            sSizeOrigine = g.MeasureString(sOrigne, nameFont);
                            XmlElement nodeCible = xmlFluxTech.XmlGetFirstElFromName(ftech, "Cible");
                            int xCible = xdebServer + widthInterServer * lstServer.FindIndex(el => el[0] == nodeCible.GetAttribute("Guid"));
                            string sCible = getStringNom(xmlFluxTech, nodeCible, true);
                            SizeF sSizeCible = new SizeF();
                            sSizeCible = g.MeasureString(sCible, nameFont);
                            if (xOrigine < xCible)
                            {
                                g.DrawLine(penFlux, xOrigine + AXE, y + HeightFlux / 2, xCible - 3 * AXE, y + HeightFlux / 2);
                                g.DrawLine(penFlux, xCible - 3 * AXE, y + HeightFlux / 2 - AXE, xCible - 3 * AXE, y + HeightFlux / 2 + AXE);
                                g.DrawLine(penFlux, xCible - 3 * AXE, y + HeightFlux / 2 - AXE, xCible - AXE, y + HeightFlux / 2);
                                g.DrawLine(penFlux, xCible - 3 * AXE, y + HeightFlux / 2 + AXE, xCible - AXE, y + HeightFlux / 2);

                                //g.DrawString(sOrigne, nameFont, new SolidBrush(Color.Black), xOrigine + AXE, y + HeightFlux / 2 - sSizeOrigine.Height - AXE);
                                //g.DrawString(sCible, nameFont, new SolidBrush(Color.Black), xCible - AXE - sSizeCible.Width, y + HeightFlux / 2 + AXE);
                                DrawGrpTxt(g, idxFont, sOrigne, xOrigine + AXE, y + HeightFlux / 2 - sSizeOrigine.Height - AXE, Color.Black);
                                DrawGrpTxt(g, idxFont, sCible, xCible - AXE - sSizeCible.Width, y + HeightFlux / 2 + AXE, Color.Black);
                            } else
                            {
                                g.DrawLine(penFlux, xOrigine - AXE, y + HeightFlux / 2, xCible + 3 * AXE, y + HeightFlux / 2);
                                g.DrawLine(penFlux, xCible + 3 * AXE, y + HeightFlux / 2 - AXE, xCible + 3 * AXE, y + HeightFlux / 2 + AXE);
                                g.DrawLine(penFlux, xCible + 3 * AXE, y + HeightFlux / 2 - AXE, xCible + AXE, y + HeightFlux / 2);
                                g.DrawLine(penFlux, xCible + 3 * AXE, y + HeightFlux / 2 + AXE, xCible + AXE, y + HeightFlux / 2);

                                //g.DrawString(sOrigne, nameFont, new SolidBrush(Color.Black), xOrigine - AXE - sSizeOrigine.Width, y + HeightFlux / 2 - sSizeOrigine.Height - AXE);
                                //g.DrawString(sCible, nameFont, new SolidBrush(Color.Black), xCible + AXE, y + HeightFlux / 2 + AXE);
                                DrawGrpTxt(g, idxFont, sOrigne, xOrigine - AXE - sSizeOrigine.Width, y + HeightFlux / 2 - sSizeOrigine.Height - AXE, Color.Black);
                                DrawGrpTxt(g, idxFont, sCible, xCible + AXE, y + HeightFlux / 2 + AXE, Color.Black);
                            }

                            y += HeightFlux;
                            g.DrawLine(pen, xRelatif + 2 * WidthFlux, y, xRelatif + 2 * WidthFlux + WidthFluxId + WidthFluxService, y);
                            g.DrawLine(penGridServer, xdebServer, y, xdebServer + widthInterServer * (lstServer.Count - 1), y);

                        }
                        if (lstxmlFlux[i].lstElChild[j].lstElChild.Count > 0) {
                            //g.DrawString(lstxmlFlux[i].lstElChild[j].elFlux.GetAttribute("Id") + " - " + lstxmlFlux[i].lstElChild[j].elFlux.GetAttribute("Nom"), nameFont, new SolidBrush(Color.Black), xRelatif + WidthFlux + AXE, ydebApp + (y - ydebApp - ifont) / 2);
                            DrawGrpTxt(g, idxFont, lstxmlFlux[i].lstElChild[j].elFlux.GetAttribute("Id") + " - " + lstxmlFlux[i].lstElChild[j].elFlux.GetAttribute("Nom"), xRelatif + WidthFlux + AXE, ydebApp + (y - ydebApp - ifont) / 2, Color.Black);
                            g.DrawLine(pen, xRelatif + WidthFlux, y, xRelatif + 2 * WidthFlux + WidthFluxId + WidthFluxService, y);
                        }
                    }

                    //g.DrawString(lstxmlFlux[i].elFlux.GetAttribute("Id") + " - " + lstxmlFlux[i].elFlux.GetAttribute("Nom"), nameFont, new SolidBrush(Color.Black), xRelatif + AXE, ydebFonc + (y - ydebFonc - ifont) / 2);
                    DrawGrpTxt(g, idxFont, lstxmlFlux[i].elFlux.GetAttribute("Id") + " - " + lstxmlFlux[i].elFlux.GetAttribute("Nom"), xRelatif + AXE, ydebFonc + (y - ydebFonc - ifont) / 2, Color.Black);
                    g.DrawLine(pen, xRelatif, y, xRelatif + 2 * WidthFlux + WidthFluxId + WidthFluxService, y);

                }

                // Trace des colonnes
                g.DrawLine(pen, xRelatif, yRelatif, xRelatif, y);
                g.DrawLine(pen, xRelatif + WidthFlux, yRelatif, xRelatif + WidthFlux, y);
                g.DrawLine(pen, xRelatif + 2 * WidthFlux, yRelatif, xRelatif + 2 * WidthFlux, y);
                g.DrawLine(pen, xRelatif + 2 * WidthFlux + WidthFluxId, yRelatif, xRelatif + 2 * WidthFlux + WidthFluxId, y);
                g.DrawLine(pen, xRelatif + 2 * WidthFlux + WidthFluxId + WidthFluxService, yRelatif, xRelatif + 2 * WidthFlux + WidthFluxId + WidthFluxService, y);



                // Trace des colonnes Serveur
                for(int i=0; i < lstServer.Count; i++)
                {
                    SizeF stringSize = new SizeF();
                    string s = lstServer[i][1];
                    stringSize = g.MeasureString(s, nameFont);
                    //g.DrawString(lstServer[i][1], nameFont, new SolidBrush(Color.Black), xdebServer + widthInterServer * i - stringSize.Width / 2, yRelatif - stringSize.Height - AXE);
                    DrawGrpTxt(g, idxFont, lstServer[i][1], xdebServer + widthInterServer * i - stringSize.Width / 2, yRelatif - stringSize.Height - AXE, Color.Black);
                    g.DrawLine(penGridServer, xdebServer + widthInterServer * i, yRelatif, xdebServer + widthInterServer * i, y);
                }

                // xMax & yMax
                xMax = xdebServer + widthInterServer * (lstServer.Count - 1);
                yMax = y;

            }
            /*
            for (int i = 0; i < lstLigne.Count; i++)
            {
                ArrayList lstPt = (ArrayList)lstLigne[i];
                g.DrawLine(pen, ((Point) lstPt[0]).X, ((Point)lstPt[0]).Y, ((Point)lstPt[lstPt.Count-1]).X, ((Point)lstPt[lstPt.Count - 1]).Y);
            }
            */
        }

    }

}
