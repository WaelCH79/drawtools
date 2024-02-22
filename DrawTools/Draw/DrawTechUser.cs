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
	public class DrawTechUser : DrawTools.DrawRectangle
	{
        public DrawTechUser()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawTechUser(Form1 of, int x, int y, int width, int height, int count)
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
            Texte = "User" + count;
            Guidkey = Guid.NewGuid();

            InitProp();
            Initialize();
        }

        public DrawTechUser(Form1 of, int x, int y, int width, int height, string GuidFonction, string NomFonction, int count)
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
            Texte = "User" + count;
            Guidkey = Guid.NewGuid();

            InitProp();

            Initialize();
        }

        public DrawTechUser(Form1 of, ArrayList lstVal, ArrayList lstValG)
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
            Guidkey = Guid.NewGuid();

            o = GetValueFromLib("Guid");
            if(o!=null)
                GuidkeyObjet = new Guid((string) o);
            o = GetValueFromLib("Nom");
            if (o != null)
                Texte = (string)o;

            Initialize();
        }

        public override void Draw(Graphics g)
        {
            ToolTechUser to = (ToolTechUser)F.drawArea.tools[(int)DrawArea.DrawToolType.TechUser];
            TemplateDt oTemplate = (TemplateDt)to.oLayers[0].lstTemplate[to.GetTemplate((string)GetValueFromName("GuidLayer"))];

            Pen pen = new Pen(oTemplate.LineCouleur, oTemplate.LineWidth);
            Pen pen1 = new Pen(oTemplate.LineCouleur, oTemplate.Line1Width);
            Rectangle r;
            
            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10) {
                AffRec(g, r, oTemplate, 0);
                g.DrawLine(pen1, r.X, r.Y + HeightFont[0] + 2 * Axe, r.X + r.Width, r.Y + HeightFont[0] + 2 * Axe);

                DrawGrpTxt(g, 1, 0, r.Left + Axe, r.Top, 0, oTemplate.Pen1Couleur, oTemplate.BkGrCouleur);
                
            } else g.DrawRectangle(pen, r);

            pen.Dispose();
        }

        public override void VisioDraw(ArrayList lstGuid, ArrayList lstShape, MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
            ToolTechUser to = (ToolTechUser)F.drawArea.tools[(int)DrawArea.DrawToolType.TechUser];

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

        public override string GetTypeSimpleTable()
        {
            return  "AppUser" ;
        }

        public override string GetsType(bool Reel)
        {
            if (Reel) return base.GetsType(Reel);
            return  "AppUser" ;
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

        private int NbrMCompServ()
        {
            int CountObj = 0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawMCompServ)) CountObj++;
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

        public void AligneObjet()
        {
            int HeightMainComposant = HeightMComposant();
            int HeightServerT = HeightServerType(), HeightServerT1 = HeightServerT;
            int WidthObjet = Rectangle.Width;

            for (int i = LstChild.Count - 1; i >= 0; i--)
            {
                if (LstChild[i].GetType() == typeof(DrawServerType))
                {
                    ((DrawServerType)LstChild[i]).Aligne(Rectangle.X + Axe, Rectangle.Y + HeightServer + HeightServerT - ((DrawServerType)LstChild[i]).Rectangle.Height, WidthObjet - 2 * Axe, ((DrawServerType)LstChild[i]).Rectangle.Height);
                    HeightServerT -= ((DrawServerType)LstChild[i]).Rectangle.Height + Axe;
                    ((DrawServerType)LstChild[i]).AligneObjet();
                }
                else if (LstChild[i].GetType() == typeof(DrawMainComposant))
                {
                    ((DrawMainComposant)LstChild[i]).Aligne(Rectangle.X + Axe, Rectangle.Y + HeightServer + HeightServerT1 + HeightMainComposant - ((DrawMainComposant)LstChild[i]).Rectangle.Height, WidthObjet - 2 * Axe, ((DrawMainComposant)LstChild[i]).Rectangle.Height);
                    HeightMainComposant -= ((DrawMainComposant)LstChild[i]).Rectangle.Height + Axe;
                    ((DrawMainComposant)LstChild[i]).AligneObjet();
                }
            }
        }

        public void LoadMainComposant_UserMComp(string sGuid)
        {
            F.drawArea.tools[(int)DrawArea.DrawToolType.MainComposant].LoadSimpleObject(sGuid);
            int j = F.drawArea.GraphicsList.FindObjet(0, sGuid);

            DrawMainComposant dmc = (DrawMainComposant)F.drawArea.GraphicsList[j];

            dmc.GuidkeyObjet = Guid.NewGuid(); //Afin de différencier les mêmes MainComposant dans une Vue
            AttachLink(dmc, DrawObject.TypeAttach.Child);
            dmc.AttachLink(this, DrawObject.TypeAttach.Parent);

        }

        public void LoadUserType_Techno(string sGuid)
        {
            F.drawArea.tools[(int)DrawArea.DrawToolType.ServerType].LoadSimpleObject(sGuid);
            int j = F.drawArea.GraphicsList.FindObjet(0, sGuid);

            DrawServerType dst = (DrawServerType)F.drawArea.GraphicsList[j];

            dst.GuidkeyObjet = Guid.NewGuid();
            AttachLink(dst, DrawObject.TypeAttach.Child);
            dst.AttachLink(this, DrawObject.TypeAttach.Parent);
        }

        public override void savetoDB()
        {
            if (!savetoDBFait())
            {
                base.savetoDB();
                //ServerTypeServer
                F.oCnxBase.DeleteUserTypeLink(this);
                //MainComposant
                F.oCnxBase.DeleteUserMComp(this);

                if (LstChild != null)
                {
                    for (int i = 0; i < LstChild.Count; i++)
                    {
                        if (LstChild[i].GetType() == typeof(DrawServerType))
                        {
                            DrawServerType dst = (DrawServerType)LstChild[i];
                            // Recherche dans la table Objet 'Rectangle'
                            if (!F.oCnxBase.ExistGuid(0, dst)) F.oCnxBase.CreatObject(dst); // Table Objet
                            else F.oCnxBase.UpdateObject(dst); // Update de la Table Objet
                            F.oCnxBase.CreatUserTypeLink(this, dst);
                            for (int j = 0; j < dst.LstChild.Count; j++)
                            {
                                if (dst.LstChild[j].GetType() == typeof(DrawTechno))
                                {
                                    DrawTechno dt = (DrawTechno)dst.LstChild[j];
                                    if (!F.oCnxBase.ExistGuid(0, dt)) F.oCnxBase.CreatObject(dt); // Table Objet
                                    else F.oCnxBase.UpdateObject(dt); // Update de la Table Objet
                                }
                            }
                        }

                        if (LstChild[i].GetType() == typeof(DrawMainComposant))
                        {
                            DrawMainComposant dmc = (DrawMainComposant)LstChild[i];
                            // Recherche dans la table Objet 'Rectangle'
                            if (!F.oCnxBase.ExistGuid(0, dmc)) F.oCnxBase.CreatObject(dmc); // Table Objet
                            else F.oCnxBase.UpdateObject(dmc); // Update de la Table Objet
                            F.oCnxBase.CreatUserMComp(this, dmc);

                            /*
                            for (int j = 0; j < dmc.LstChild.Count; j++)
                            {
                                if (dmc.LstChild[j].GetType() == typeof(DrawMCompServ))
                                {
                                    DrawMCompServ dmcs = (DrawMCompServ)dmc.LstChild[j];
                                    if (!F.oCnxBase.ExistGuid(0, dmcs)) F.oCnxBase.CreatObject(dmcs); // Table Objet
                                    else F.oCnxBase.UpdateObject(dmcs); // Update de la Table Objet
                                }
                            }
                             */
                        }
                    }
                }
                savetoDBOK();
            }
        }

	}
}
