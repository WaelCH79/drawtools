using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using MOI = Microsoft.Office.Interop;
 
using System.Xml;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawRoadmap : DrawTools.DrawRectangle
	{
        static private Color Couleur = Color.Tan;
        static private int LineWidth = 1;
        char cTechFonc;

		public DrawRoadmap()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawRoadmap(Form1 of, string sGuidProduit, char switchTechFonc)
        {
            F = of;
            OkMove = true;
            Align = true;
            Rectangle = new Rectangle(5, 5, 425, 85);
            LstParent = null; 
            LstChild = new ArrayList();
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = new ArrayList();
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
            Texte = "11";
            cTechFonc = switchTechFonc;

            InitProp();
            Initialize();
            /*
            if (F.oCnxBase.CBRecherche("SELECT GuidTechnoRef, NomTechnoRef, ValIndicator FROM TechnoRef, IndicatorLink WHERE GuidProduit='" + sGuidProduit + "' AND GuidTechnoRef=GuidObjet AND GuidIndicator='b00b12bd-a447-47e6-92f6-e3b76ad22830' ORDER BY ValIndicator"))
            {

                while (F.oCnxBase.Reader.Read()) LstChild.Add(F.oCnxBase.Reader.GetString(0) + ';' + F.oCnxBase.Reader.GetString(1) + ';' + F.oCnxBase.Reader.GetDouble(2).ToString());
                F.oCnxBase.CBReaderClose();

                for (int i = 0; i < LstChild.Count; i++)
                {
                    string[] aTechno = ((string)LstChild[i]).Split(';');
                    if (F.oCnxBase.CBRecherche("SELECT ValIndicator FROM IndicatorLink WHERE GuidObjet='" + aTechno[0] + "' AND GuidIndicator='f59e72d5-987e-4b84-bee0-61d36830a03c'"))
                        LstChild[i] = (string)LstChild[i] + ";" + F.oCnxBase.Reader.GetDouble(0).ToString();
                    else LstChild[i] = (string)LstChild[i] + ";0";
                    F.oCnxBase.CBReaderClose();
                }
            }
            F.oCnxBase.CBReaderClose();
            rectangle.Height = (HeightFont[0] + 2 * Axe) * (LstChild.Count + 2);
            rectangle.Width = iLongLib + 4 * iLongAn;
            */

            int nbrTechno = 0;
            if (F.oCnxBase.CBRecherche("SELECT GuidTechnoRef, NomTechnoRef, UpComingStart, UpComingEnd, ReferenceStart, ReferenceEnd, ConfinedStart, ConfinedEnd, DecommStart, DecommEnd, ValIndicator FROM TechnoRef, IndicatorLink WHERE GuidProduit='" + sGuidProduit + "' AND GuidTechnoRef=GuidObjet AND GuidIndicator='b00b12bd-a447-47e6-92f6-e3b76ad22830' ORDER BY ValIndicator"))
            {
                XmlDocument xmlReader = F.oCnxBase.CreatXmlDocFromDB();
                XmlElement root = xmlReader.DocumentElement;
                nbrTechno = root.ChildNodes.Count;
                LstChild.Add(xmlReader);
            }
            F.oCnxBase.CBReaderClose();
            rectangle.Height = (HeightFont[0] + 2 * Axe) * (nbrTechno + 2);
            rectangle.Width = iLongLib + 4 * iLongAn;
        }

        

        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(Couleur, LineWidth);
            Pen pen1 = new Pen(Couleur, LineWidth+1);
            Rectangle r;

            
            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            //AffRec(g, r, Couleur, 0, true, false, false, 1);
            g.DrawLine(pen1, r.Left, r.Top + HeightFont[0] + Axe, r.Right, r.Top + HeightFont[0] + Axe);
            g.DrawLine(pen, r.Left + iLongLib, r.Top, r.Left + iLongLib, r.Bottom - (HeightFont[0] + 2 * Axe));
            int y = DateTime.Now.Year;
            double dan = (new DateTime(y, 1, 1)).ToOADate(), dmax = (new DateTime(y + 4, 1, 1)).ToOADate();
            for (int i = 0; i < 4; i++)
            {
                DrawGrpTxt(g, 1, (y + i).ToString(), r.Left + iLongLib + Axe + iLongAn * i, r.Top-Axe, Color.Black);
                g.DrawLine(pen, r.Left + iLongLib + iLongAn * (i + 1), r.Top, r.Left + iLongLib + iLongAn * (i + 1), r.Bottom - (HeightFont[0] + 2 * Axe));
            }

            XmlDocument xmlDoc = (XmlDocument)LstChild[0];
            IEnumerator ienum = xmlDoc.DocumentElement.GetEnumerator();
            XmlNode Node;
            int idx = -1;

            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    idx++;
                    XmlElement el = (XmlElement)Node;
                    string TechonName = (string)F.oCnxBase.GetValueFromNameInXmlReader(el, "NomTechnoRef");
                    double dUpComingStart = ((DateTime)F.oCnxBase.GetValueFromNameInXmlReader(el, "UpComingStart")).ToOADate();
                    double dUpComingEnd = ((DateTime)F.oCnxBase.GetValueFromNameInXmlReader(el, "UpComingEnd")).ToOADate();
                    double dReferenceStart = ((DateTime)F.oCnxBase.GetValueFromNameInXmlReader(el, "ReferenceStart")).ToOADate();
                    double dReferenceEnd = ((DateTime)F.oCnxBase.GetValueFromNameInXmlReader(el, "ReferenceEnd")).ToOADate();
                    double dConfinedStart = ((DateTime)F.oCnxBase.GetValueFromNameInXmlReader(el, "ConfinedStart")).ToOADate();
                    double dConfinedEnd = ((DateTime)F.oCnxBase.GetValueFromNameInXmlReader(el, "ConfinedEnd")).ToOADate();
                    double dDecommStart = ((DateTime)F.oCnxBase.GetValueFromNameInXmlReader(el, "DecommStart")).ToOADate();
                    double dDecommEnd = ((DateTime)F.oCnxBase.GetValueFromNameInXmlReader(el, "DecommEnd")).ToOADate();
                    double dSupport = (double)F.oCnxBase.GetValueFromNameInXmlReader(el, "ValIndicator");
                    double dDeb = 0, dFin = 0;
                    
                    // Libelle Techno
                    DrawGrpTxt(g, 1, TechonName, r.Left + 10, r.Top + (HeightFont[0] + 2 * Axe) * (idx + 1), Color.Black);

                    // Roadmap
                    dDeb = dUpComingStart - dan; if (dDeb < 0) dDeb = 0;
                    dFin = dUpComingEnd - dan; if (dFin > dmax - dan) dFin = dmax - dan;
                    AffRec(g, new Rectangle(r.Left + iLongLib + (int)dDeb * iLongAn / 365, r.Top + (HeightFont[0] + 2 * Axe) * (idx + 1), (int)(dFin-dDeb) * iLongAn / 365, HeightFont[0]), Color.White, LineWidth, Color.Gray, 1, false, false, false);

                    dDeb = dReferenceStart - dan; if (dDeb < 0) dDeb = 0;
                    dFin = dReferenceEnd - dan; if (dFin > dmax - dan) dFin = dmax - dan;
                    AffRec(g, new Rectangle(r.Left + iLongLib + (int)dDeb * iLongAn / 365, r.Top + (HeightFont[0] + 2 * Axe) * (idx + 1), (int)(dFin - dDeb) * iLongAn / 365, HeightFont[0]), Color.White, LineWidth, Color.Green, 1, false, false, false);

                    dDeb = dConfinedStart - dan; if (dDeb < 0) dDeb = 0;
                    dFin = dConfinedEnd - dan; if (dFin > dmax - dan) dFin = dmax - dan;
                    AffRec(g, new Rectangle(r.Left + iLongLib + (int)dDeb * iLongAn / 365, r.Top + (HeightFont[0] + 2 * Axe) * (idx + 1), (int)(dFin - dDeb) * iLongAn / 365, HeightFont[0]), Color.White, LineWidth, Color.Orange, 1, false, false, false);

                    dDeb = dDecommStart - dan; if (dDeb < 0) dDeb = 0;
                    dFin = dDecommEnd - dan; if (dFin > dmax - dan) dFin = dmax - dan;
                    AffRec(g, new Rectangle(r.Left + iLongLib + (int)dDeb * iLongAn / 365, r.Top + (HeightFont[0] + 2 * Axe) * (idx + 1), (int)(dFin - dDeb) * iLongAn / 365, HeightFont[0]), Color.White, LineWidth, Color.Red, 1, false, false, false);

                    //Icon Fin de support
                    if (dSupport > dan && dSupport < dmax)
                    {
                        dDeb = dSupport - dan;
                        g.DrawImage((Bitmap)Image.FromFile(F.sPathRoot + @"\bouton\bmp\requiredBang.gif", true), new Point(r.Left + iLongLib + (int)dDeb * iLongAn / 365, r.Top + (HeightFont[0] + 2 * Axe) * (idx + 1)));

                    }
                    switch (cTechFonc)
                    {
                        case 'S':
                            break;
                        case 'H':
                            break;
                    }
                }
            }

            //Legend
            AffRec(g, new Rectangle(r.Left + 10, r.Bottom - HeightFont[0], 20, HeightFont[0]), Color.White, LineWidth, Color.Gray, 1, false, false, false);
            DrawGrpTxt(g, 2, "Upcoming", r.Left + 32, r.Bottom - HeightFont[0], Color.Black);
            AffRec(g, new Rectangle(r.Left + 90, r.Bottom - HeightFont[0], 20, HeightFont[0]), Color.White, LineWidth, Color.Green, 1, false, false, false);
            DrawGrpTxt(g, 2, "Reference", r.Left + 112, r.Bottom - HeightFont[0], Color.Black);
            AffRec(g, new Rectangle(r.Left + 170, r.Bottom - HeightFont[0], 20, HeightFont[0]), Color.White, LineWidth, Color.Orange, 1, false, false, false);
            DrawGrpTxt(g, 2, "Confined", r.Left + 192, r.Bottom - HeightFont[0], Color.Black);
            AffRec(g, new Rectangle(r.Left + 245, r.Bottom - HeightFont[0], 20, HeightFont[0]), Color.White, LineWidth, Color.Red, 1, false, false, false);
            DrawGrpTxt(g, 2, "Decommissioned", r.Left + 267, r.Bottom - HeightFont[0], Color.Black);

            g.DrawImage((Bitmap)Image.FromFile(F.sPathRoot + @"\bouton\bmp\requiredBang.gif", true), new Point(r.Left + 350, r.Bottom - HeightFont[0]));
            DrawGrpTxt(g, 2, "End Of Support", r.Left + 367, r.Bottom - HeightFont[0], Color.Black);


            /*
            for (int i = 0; i < LstChild.Count; i++)
            {
                string[] aTechno = ((string)LstChild[i]).Split(';');
                //if (aTechno[1] == "RH Linux 5.5 64bits") aTechno[0] = aTechno[0];
                double timespan = 400, dproduit = Convert.ToDouble(aTechno[2]), dfinref = Convert.ToDouble(aTechno[3]), dwarning = dfinref > 0 && dfinref < dproduit - timespan ? dfinref : dproduit - timespan;
                double dDeltaDeb=0, dDeltaFin=0, dCompDate = dan;
                dDeltaDeb = dproduit - dan;

                Bitmap image=null;
                switch (cTechFonc)
                {
                    case 'S':
                        image = (Bitmap)Image.FromFile(F.sPathRoot + @"\bouton\bmp\requiredBang.gif", true);
                        if (dDeltaDeb >= 0)
                        {
                            //debut barre
                            if (i == 0) dDeltaDeb = 0;
                            else
                            {
                                string[] aTechnoPre = ((string)LstChild[i - 1]).Split(';');
                                double dproduitPre = Convert.ToDouble(aTechnoPre[2]), dfinrefPre = Convert.ToDouble(aTechnoPre[3]), dwarningPre = dfinrefPre > 0 && dfinrefPre < dproduitPre - timespan ? dfinrefPre : dproduitPre - timespan;
                                dDeltaDeb = Math.Max(0, dwarningPre - dan);
                                if (dDeltaDeb > 0) dCompDate = dwarningPre;
                            }
                            // barre upcoming
                            if (dDeltaDeb != 0)
                            {
                                double dInComing = dDeltaDeb - timespan / 2 > 0 ? dDeltaDeb - timespan / 2 : 0;
                                AffRec(g, new Rectangle(r.Left + iLongLib + (int)dInComing * iLongAn / 365, r.Top + (HeightFont[0] + 2 * Axe) * (i + 1), (int)dDeltaDeb * iLongAn / 365, HeightFont[0]), Color.White, LineWidth, Color.Gray, 1, false, false, false);
                            }

                            //barre reference
                            if (dwarning - dmax > 0) dwarning = dmax;
                            dDeltaFin = dwarning - dCompDate;
                            if (dDeltaFin > 0)
                            {
                                AffRec(g, new Rectangle(r.Left + iLongLib + (int)dDeltaDeb * iLongAn / 365, r.Top + (HeightFont[0] + 2 * Axe) * (i + 1), (int)dDeltaFin * iLongAn / 365, HeightFont[0]), Color.White, LineWidth, Color.Green, 1, false, false, false);
                                dDeltaDeb += dDeltaFin;
                                dCompDate = dwarning;
                            }
                            if (dwarning != dmax)
                            {
                                //barre warning
                                if (dproduit - dmax > 0) dproduit = dmax;
                                dDeltaFin = dproduit - dCompDate;
                                if (dDeltaFin > 0) AffRec(g, new Rectangle(r.Left + iLongLib + (int)dDeltaDeb * iLongAn / 365, r.Top + (HeightFont[0] + 2 * Axe) * (i + 1), (int)dDeltaFin * iLongAn / 365, HeightFont[0]), Color.White, LineWidth, Color.Red, 1, false, false, false);

                                if (dproduit < dmax)
                                {
                                    // Icone Fin Support
                                    g.DrawImage(image, new Point(r.Left + iLongLib + (int)(dDeltaDeb + dDeltaFin) * iLongAn / 365, r.Top + (HeightFont[0] + 2 * Axe) * (i + 1)));
                                }
                            }
                            //Legend
                            AffRec(g, new Rectangle(r.Left + 10, r.Bottom - HeightFont[0], 20, HeightFont[0]), Color.White, LineWidth, Color.Gray, 1, false, false, false);
                            DrawGrpTxt(g, 2, "Upcoming", r.Left + 32, r.Bottom - HeightFont[0], Color.Black);
                            AffRec(g, new Rectangle(r.Left + 90, r.Bottom - HeightFont[0], 20, HeightFont[0]), Color.White, LineWidth, Color.Green, 1, false, false, false);
                            DrawGrpTxt(g, 2, "Reference", r.Left + 112, r.Bottom - HeightFont[0], Color.Black);
                            AffRec(g, new Rectangle(r.Left + 170, r.Bottom - HeightFont[0], 20, HeightFont[0]), Color.White, LineWidth, Color.Red, 1, false, false, false);
                            DrawGrpTxt(g, 2, "Confined", r.Left + 192, r.Bottom - HeightFont[0], Color.Black);

                            g.DrawImage(image, new Point(r.Left + 250, r.Bottom - HeightFont[0]));
                            DrawGrpTxt(g, 2, "End Of Support", r.Left + 260, r.Bottom - HeightFont[0], Color.Black);
                        }

                        break;
                    case 'H':
                        image = (Bitmap)Image.FromFile(F.sPathRoot + @"\bouton\bmp\bullet.gif", true);
                        if (dDeltaDeb >= 0)
                        {
                            //debut barre
                            dDeltaDeb = 0;
                            Color c = Color.Red;
                                //barre warning
                            if (i == LstChild.Count - 1) c = Color.Green;
                            if (dproduit - dmax > 0) dproduit = dmax;
                            dDeltaFin = dproduit - dCompDate;
                            if (dDeltaFin > 0) 
                                //AffRec(g, new Rectangle(r.Left + iLongLib, r.Top + (HeightFont[0] + 2 * Axe) * (i + 1), iLongAn, HeightFont[0]), Color.White, LineWidth, c, 1, false, false, false);
AffRec(g, new Rectangle(r.Left + iLongLib, r.Top + (HeightFont[0] + 2 * Axe) * (i + 1), (int)(dDeltaDeb + dDeltaFin) * iLongAn / 365, HeightFont[0]), Color.White, LineWidth, c, 1, false, false, false);
                            if (dproduit < dmax)
                            {
                                // Icone Fin Support
                                g.DrawImage(image, new Point(r.Left + iLongLib + (int)(dDeltaDeb + dDeltaFin) * iLongAn / 365, r.Top + (HeightFont[0] + 2 * Axe) * (i + 1)));
                            }

                            //Legend
                            AffRec(g, new Rectangle(r.Left + 10, r.Bottom - HeightFont[0], 20, HeightFont[0]), Color.White, LineWidth, Color.Gray, 1, false, false, false);
                            DrawGrpTxt(g, 2, "Upcoming", r.Left + 32, r.Bottom - HeightFont[0], Color.Black);
                            AffRec(g, new Rectangle(r.Left + 90, r.Bottom - HeightFont[0], 20, HeightFont[0]), Color.White, LineWidth, Color.Green, 1, false, false, false);
                            DrawGrpTxt(g, 2, "Reference", r.Left + 112, r.Bottom - HeightFont[0], Color.Black);
                            AffRec(g, new Rectangle(r.Left + 170, r.Bottom - HeightFont[0], 20, HeightFont[0]), Color.White, LineWidth, Color.Red, 1, false, false, false);
                            DrawGrpTxt(g, 2, "Confined", r.Left + 192, r.Bottom - HeightFont[0], Color.Black);

                            g.DrawImage(image, new Point(r.Left + 250, r.Bottom - HeightFont[0]));
                            DrawGrpTxt(g, 2, "End Of maintenance", r.Left + 260, r.Bottom - HeightFont[0], Color.Black);
                        }
                        break;

                    case 'A':
                        image = (Bitmap)Image.FromFile(F.sPathRoot + @"\bouton\bmp\requiredBang.gif", true);
                        break;
                }
                DrawGrpTxt(g, 1, aTechno[1], r.Left + 10, r.Top + (HeightFont[0] + 2 * Axe) * (i + 1), Color.Black);
            }*/
            
            pen.Dispose();
        }
    }
}
