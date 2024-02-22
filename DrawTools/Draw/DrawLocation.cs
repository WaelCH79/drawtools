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
	public class DrawLocation : DrawTools.DrawRectangle
	{
        public int Sens
        {
            get
            {
                return (int)this.GetValueFromName("Sens");
            }
            set
            {
                this.InitProp("Sens", (object)value, true);
            }
        }

		public DrawLocation()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawLocation(Form1 of, ArrayList lstVal)
        {
            object o = null;
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();

            o = GetValueFromLib("Guid");
            if (o != null) GuidkeyObjet = new Guid((string)o);
        }

        public DrawLocation(Form1 of, int x, int y, int width, int height, int count)
        {
            F = of;
            OkMove = true;
            Align = true;
            Rectangle = new Rectangle(x, y, width, height);
            LstParent = null; 
            LstChild = new ArrayList();
            LstLinkIn = new ArrayList();
            LstLinkOut = null;
            LstValue = new ArrayList();
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
            Texte = "Site" + count;

            InitProp();
            Sens = 0;
            Initialize();
        }

        public DrawLocation(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = true;
            Align = true;
            InitRectangle(lstValG);
            LstParent = null;
            LstChild = new ArrayList();
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

        public void setSens(Point pt)
        {
            int CountNPtCnx = NbrNPtCnx();
            if (CountNPtCnx == 1)
            {
                for (int i = LstChild.Count - 1; i >= 0; i--)
                {
                    if (LstChild[i].GetType() == typeof(DrawPtCnx))
                    {
                        DrawPtCnx dpc = (DrawPtCnx)LstChild[i];
                        if (pt.X - Rectangle.Left < Rectangle.Right - pt.X) Sens = 0;
                        else Sens = 1;
                    }
                }
            }
        }

        public override bool AttachPointInObject(Point point)
        {
            return false;
        }

        public int NbrNPtCnx()
        {
            int CountObj = 0;


            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawPtCnx)) CountObj++;

            return CountObj;
        }


        public void AligneObjet()
        {
            int CountNPtCnx = NbrNPtCnx();
            if (CountNPtCnx != 0)
            {
                for (int i = LstChild.Count - 1; i >= 0; i--)
                {
                    if (LstChild[i].GetType() == typeof(DrawPtCnx))
                    {
                        ((DrawPtCnx)LstChild[i]).Aligne(Rectangle.Left + Sens * (Rectangle.Width - WidthPtCnx) + WidthPtCnx * (CountNPtCnx - 1) * (1 - 2 * Sens) + Axe * (1 - 2 * Sens), Rectangle.Bottom, WidthPtCnx - Axe, HeightPtCnx);
                        CountNPtCnx--;
                    }
                }

            }
        }


        public override void Draw(Graphics g)
        {
            if (F != null)
            {
                ToolLocation to = (ToolLocation)F.drawArea.tools[(int)DrawArea.DrawToolType.Location];

                Pen pen = new Pen(to.LineCouleur, to.LineWidth);
                Rectangle r;

                r = DrawRectangle.GetNormalizedRectangle(Rectangle);

                if (r.Width > 20 && r.Height > 10)
                {

                    int Esp = r.Width / 6;
                    AffRec(g, r, to); //LineCouleur, LineWidth, Couleur, 5, true, true, false);
                    AffRec(g, new Rectangle(r.Left + Esp, r.Top - HeightSite / 2, r.Width - 2 * Esp, HeightSite), to); //LineCouleur, LineWidth, Couleur, 2, true, true, false);

                    DrawGrpTxt(g, 1, 0, r.Left + Axe + Esp, r.Top - HeightSite / 2 + (HeightSite - HeightFont[1]) / 4, 0, Color.Black, Color.Transparent);
                }
                else g.DrawRectangle(pen, r);

                pen.Dispose();
            }
        }

        public override void VisioDraw(ArrayList lstGuid, ArrayList lstShape, MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
            ToolLocation to = (ToolLocation)F.drawArea.tools[(int)DrawArea.DrawToolType.Location];

            if (lstGuid.IndexOf(GuidkeyObjet.ToString()) == -1)
            {
                if (LstParent != null)
                    for (int ip = 0; ip < LstParent.Count; ip++)
                        if (lstGuid.IndexOf(((DrawObject)LstParent[ip]).GuidkeyObjet.ToString()) == -1)
                            ((DrawObject)LstParent[ip]).VisioDraw(lstGuid, lstShape, page, yPage, qxPage, qyPage);

                
                //Dessiner l'objet
                int Esp = Rectangle.Width / 6;
                MOI.Visio.Shape shape = page.DrawRectangle(Rectangle.Left * qxPage, yPage - Rectangle.Top * qyPage, Rectangle.Right * qxPage, yPage - Rectangle.Bottom * qyPage);
                MOI.Visio.Shape shTitle = page.DrawRectangle((Rectangle.Left + Esp) * qxPage, yPage - (Rectangle.Top - HeightSite / 2) * qyPage, (Rectangle.Right - Esp) * qxPage, yPage - (Rectangle.Top + HeightSite / 2) * qyPage);

                //Inserer le texte + Couleur + taille
//                DrawGrpTxt(g, 1, 0, r.Left + Axe + Esp, r.Top - HeightSite / 2 + (HeightSite - HeightFont[1]) / 4, 0, Color.Black);
                
                DrawGrpTxt(shTitle, 1, 0, 0, 0, 0, Color.Black, Color.Transparent);

                //Couleur trait
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                shTitle.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                //Couleur Fond
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillBkgnd).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillForegnd).FormulaU = "RGB(" + Color.White.R.ToString() + "," + Color.White.G.ToString() + "," + Color.White.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillPattern).ResultIU = 32;
                shTitle.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillBkgnd).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                shTitle.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillForegnd).FormulaU = "RGB(" + Color.White.R.ToString() + "," + Color.White.G.ToString() + "," + Color.White.B.ToString() + ")";
                shTitle.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillPattern).ResultIU = 28;
                //Arrondi
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineRounding).FormulaU = "3 mm";
                shTitle.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineRounding).FormulaU = "3 mm";

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

        public override void CWInsert(ControlDoc cw, char cTypeVue)
        {
            if (cTypeVue == '6') // 6-Site
            {
                string sType = GetType().Name.Substring("Draw".Length);
                string sTypeAP="E";
                string sGuid = GuidkeyObjet.ToString().Replace("-", "");
                object o = null;
                o = GetValueFromName("AccessPoint"); if (o != null && (string) o != "") sTypeAP = (string)o;
                //sType ne doit pas depasse 4 caracteres
                
                string sBook = sType.Substring(0, 3) + sGuid;
                if (cw.Exist("n" + sBook) > -1)
                {
                    cw.InsertTextFromId("n" + sBook, true, Texte, "Titre 5");
                    cw.InsertTabFromId(sBook, true, this, null, false, null);
                }
                else if (cw.Exist(sTypeAP + sType) > -1)
                {
                    cw.InsertTextFromId(sTypeAP + sType, false, "\n", null);
                    cw.CreatIdFromIdP(sBook, sTypeAP + sType);
                    cw.InsertTextFromId(sBook, true, Texte + "\n", "Titre 5");
                    cw.CreatIdFromIdP("n" + sBook, sBook);
                    cw.InsertTextFromId(sBook, false, "Definition\n", "Titre 6");
                    cw.InsertTextFromId(sBook, false, "\n", null);
                    cw.InsertTextFromId(sBook, false, "Properties\n", "Titre 6");
                    cw.InsertTextFromId(sBook, false, "\n", null);
                    cw.InsertTabFromId(sBook, false, this, null, false, null);

                    //cw.CreatBookmark("n" + sGuid, sType.Substring(0, 3) + sGuid, 2);
                }
            }
        }
    }
}
