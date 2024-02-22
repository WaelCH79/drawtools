using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawCadreRefEnd : DrawTools.DrawRectangle
	{
        static private Color Couleur = Color.Tan;
        static private Color LineCouleur = Color.Black;
        static private int LineWidth = 1;

		public DrawCadreRefEnd()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawCadreRefEnd(Form1 of, int x, int y, int width, int height, int count)
        {
            F = of;
            OkMove = true;
            Align = true;
            Rectangle = new Rectangle(x, y, width, height);
            LstParent = null;
            LstChild = new ArrayList();
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = new ArrayList();
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
            Texte = "CadreRef" + count;

            InitProp();
            Initialize();
        }

        public DrawCadreRefEnd(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = true;
            Align = true;
            InitRectangle(lstValG);
            LstParent = null;
            LstChild = new ArrayList(); ;
            LstLinkIn = null;
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

        public override string GetTypeSimpleTable()
        {
            return "CadreRef";
        }

        public int NbrProduit()
        {
            int CountObj = 0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawProduit)) CountObj++;

            return CountObj;
        }

        public void AligneObjet()
        {
            string sTypeVue = F.tbTypeVue.Text; // (string)F.cbTypeVue.SelectedItem;

            int CountC = NbrProduit();
            if (CountC != 0)
            {
                int i= CountC-1; 

                for (int j = LstChild.Count - 1; j >= 0; j--)
                {
                    if (LstChild[j].GetType() == typeof(DrawProduit))
                    {
                        DrawRectangle o = (DrawRectangle)LstChild[j];

                        o.rectangle.Width = Rectangle.Width - 6 * Axe;
                        o.rectangle.Height = HeightCadreRef;
                        o.rectangle.X = Rectangle.X + 3 * Axe;
                        o.rectangle.Y = Rectangle.Y + HeightCadreRef + i * (o.Rectangle.Height + Axe);
                        i--;
                    }
                }
            }
        }
             

        public override bool AttachPointInObject(Point point)
        {
            return false;
        }

        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(LineCouleur, LineWidth);
            Rectangle r;
                       
            r = DrawRectangle.GetNormalizedRectangle(Rectangle);
            //r.Height = 2 * HeightCadreRef + HeightIndicator + 3 * Axe;

            if (r.Width > 20 && r.Height > 10) {
                AffRec(g, r, LineCouleur, LineWidth, Couleur, 5, true, true, false);
                DrawGrpTxt(g, 2, 0, r.Left + Axe, r.Top, 0, Color.Black, Color.Transparent);
            } else g.DrawRectangle(pen, r);

            pen.Dispose();
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


        public ArrayList FindProduit()
        {
            ArrayList aProduit = new ArrayList();
            DateTime dt = DateTime.Now;
            TimeSpan ts = new TimeSpan(180, 0, 0, 0);
            ArrayList TComposant = new ArrayList();
            ArrayList WComposant = new ArrayList();


            // prise en compte des technos présentent dans les schémas
            bool ComposantSchema = false, ComposantInstalle = false;

            if (F.oCnxBase.CBRecherche("SELECT Parameter From OptionsDraw WHERE NumOption=1")) // uniquement les Composants utilises dans les schemas
                if (F.oCnxBase.Reader.GetString(0) == "Oui") ComposantSchema = true;
            F.oCnxBase.CBReaderClose();

            // prise en compte des technos présentent dans les schémas dont au moins un environnement est installé
            if (F.oCnxBase.CBRecherche("SELECT Parameter From OptionsDraw WHERE NumOption=3")) // uniquement les composants installes en production
                if (F.oCnxBase.Reader.GetString(0) == "Oui") { ComposantSchema = false; ComposantInstalle = true; }
            F.oCnxBase.CBReaderClose();

            if (ComposantSchema)
            {
                TComposant.Add(", Techno"); WComposant.Add("AND TechnoRef.GuidTechnoRef=Techno.GuidTechnoRef");
                TComposant.Add(", ServerPhy"); WComposant.Add("AND TechnoRef.GuidTechnoRef=ServerPhy.GuidTechnoRef");
            }
            else if (ComposantInstalle)
            {
                TComposant.Add(", Techno, ServerType, ServerTypeLink, Server, GServer, DansVue, Vue, Application"); WComposant.Add("AND TechnoRef.GuidTechnoRef=Techno.GuidTechnoRef AND Techno.GuidTechnoHost=ServerType.GuidServerType AND ServerType.GuidServerType=ServerTypeLink.GuidServerType AND ServerTypeLink.GuidServer=Server.GuidServer AND Server.GuidServer=GServer.GuidServer AND GServer.GuidGServer=DansVue.GuidObjet AND DansVue.GuidGVue=Vue.GuidGVue AND Vue.GuidApplication=Application.GuidApplication AND Installee=1");
                TComposant.Add(", ServerPhy"); WComposant.Add("AND TechnoRef.GuidTechnoRef=ServerPhy.GuidTechnoRef");
            }
            else { TComposant.Add(""); WComposant.Add(""); }
                                    
            for (int k = 0; k < TComposant.Count; k++)
            {
                if (F.oCnxBase.CBRecherche("Select Produit.GuidProduit, MIN(ValIndicator), MAX(Norme), SUM(VNmoins1), SUM(VN), SUM(VNplus1) FROM Produit, TechnoRef, IndicatorLink, Indicator" + TComposant[k] + " WHERE TechnoRef.GuidProduit=Produit.GuidProduit and TechnoRef.GuidTechnoRef=IndicatorLink.GuidObjet and IndicatorLink.GuidIndicator=Indicator.GuidIndicator and NomIndicator='1-Fin Support' " + WComposant[k] + " AND Produit.GuidCadreRef='" + GuidkeyObjet.ToString() + "' GROUP BY Produit.GuidProduit"))
                {
                    while (F.oCnxBase.Reader.Read())
                    {
                        CadreRefN1 cr = new CadreRefN1(F.oCnxBase.Reader.GetString(0));
                        ArrayList aDate = new ArrayList();
                        ArrayList aNorme = new ArrayList();
                        ArrayList aUsed = new ArrayList();

                        DateTime dtc = DateTime.FromOADate(F.oCnxBase.Reader.GetDouble(1));
                        if (DateTime.Compare(dtc, dt) < 0) aDate.Add((double)Form1.ImgList.fail);
                        else if (DateTime.Compare(dtc - ts, dt) < 0) aDate.Add((double)Form1.ImgList.alert);
                        else aDate.Add((double)Form1.ImgList.pass);

                        aNorme.Add((double)Form1.ImgList.Nettbd + (double)F.oCnxBase.Reader.GetInt16(2));

                        if (F.oCnxBase.Reader.IsDBNull(3)) aUsed.Add(0.0); else aUsed.Add(F.oCnxBase.Reader.GetDouble(3));
                        if (F.oCnxBase.Reader.IsDBNull(4)) aUsed.Add(0.0); else aUsed.Add(F.oCnxBase.Reader.GetDouble(4));
                        if (F.oCnxBase.Reader.IsDBNull(5)) aUsed.Add(0.0); else aUsed.Add(F.oCnxBase.Reader.GetDouble(5));

                        cr.aIndicator.Add(aDate);
                        cr.aIndicator.Add(aNorme);
                        cr.aIndicator.Add(aUsed);
                        aProduit.Add(cr);
                    }
                }
                F.oCnxBase.CBReaderClose();
            }
            return aProduit;
        }

        public override void dataGrid_CellClick(DataGridView odgv, DataGridViewCellEventArgs e)
        {
            int n;

            n = GetIndexFromName("NomVue");
            if (n > -1 && e.RowIndex == n) 
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromDB("Select NomVue From Vue Where GuidTypeVue='5f6bd456-9f64-48e8-bb64-22f414465928'", "Value");
                //fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }
        }
    }
}
