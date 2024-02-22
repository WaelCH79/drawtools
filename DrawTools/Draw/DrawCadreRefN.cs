using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawCadreRefN : DrawTools.DrawRectangle
	{
        static private int LineWidth = 1;

        public DrawCadreRefN()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawCadreRefN(Form1 of, int x, int y, int width, int height, int count)
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
            SetValueFromName("Couleur", "Tan");

            Initialize();
        }

        public DrawCadreRefN(Form1 of, ArrayList lstVal, ArrayList lstValG)
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
            //SetValueFromName("Couleur", "Tan");

            Initialize();
        }

        public override string GetTypeSimpleTable()
        {
            return "CadreRef";
        }

        public int NbrCadreRefN1()
        {
            int CountObj = 0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawCadreRefN1)) CountObj++;

            return CountObj;
        }

        public int NbrIndicator()
        {
            int CountObj = 0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawIndicator)) CountObj++;

            return CountObj;
        }

        public void AligneObjet()
        {
            string sTypeVue = F.tbTypeVue.Text; // (string)F.cbTypeVue.SelectedItem;

            int CountC = NbrCadreRefN1();
            if (CountC != 0)
            {
                int i= CountC-1; 

                for (int j = LstChild.Count - 1; j >= 0; j--)
                {
                    if (LstChild[j].GetType() == typeof(DrawCadreRefN1))
                    {
                        DrawRectangle o = (DrawRectangle)LstChild[j];

                        o.rectangle.Width = (Rectangle.Width - (CountC + 1) * Axe) / CountC;
                        o.rectangle.X = Rectangle.X + Axe + i * (o.Rectangle.Width + Axe);
                        o.rectangle.Y = Rectangle.Y + HeightCadreRef;
                        o.rectangle.Height = HeightCadreRef;
                        i--;
                    }
                }
            }

            CountC = NbrIndicator();
            if (CountC != 0)
            {
                int i = CountC - 1;

                for (int j = LstChild.Count - 1; j >= 0; j--)
                {
                    if (LstChild[j].GetType() == typeof(DrawIndicator))
                    {
                        DrawRectangle o = (DrawRectangle)LstChild[j];

                        o.rectangle.Width = HeightIndicator;
                        o.rectangle.X = Rectangle.X + Axe + i * (o.Rectangle.Width + Axe);
                        o.rectangle.Y = Rectangle.Y + 2 * (HeightCadreRef + Axe);
                        o.rectangle.Height = HeightIndicator;
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
            Color Couleur = Color.FromName((string)GetValueFromName("Couleur"));
            Pen pen = new Pen(Couleur, LineWidth);
            Rectangle r;
                       
            r = DrawRectangle.GetNormalizedRectangle(Rectangle);
            //r.Height = 2 * HeightCadreRef + HeightIndicator + 3 * Axe;

            if (r.Width > 20 && r.Height > 10) {
                AffRec(g, r, Couleur, LineWidth, Couleur, 5, true, true, false);
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


        public ArrayList FindCadreRefN1FromTv()
        {
            ArrayList CadreRefN1InTV = new ArrayList();
            TreeNode[] ArrayTreeNode = F.tvObjet.Nodes.Find(GuidkeyObjet.ToString(), true);
            ArrayList TComposant = new ArrayList();
            ArrayList WComposant = new ArrayList();

            // prise en compte des technos présentent dans les schémas
            bool ComposantSchema = false, ComposantInstalle = false;

            if (F.oCnxBase.CBRecherche("SELECT Parameter From OptionsDraw WHERE NumOption=1")) // uniquement les composants utilises dans les schemas
                if (F.oCnxBase.Reader.GetString(0) == "Oui") ComposantSchema = true;
            F.oCnxBase.CBReaderClose();

            // prise en compte des technos présentent dans les schémas dont au moins un environnement est installé
            if (F.oCnxBase.CBRecherche("SELECT Parameter From OptionsDraw WHERE NumOption=3")) // uniquement les componsants installes en production
                if (F.oCnxBase.Reader.GetString(0) == "Oui") { ComposantSchema = false; ComposantInstalle = true;}
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

          /*  if (F.oCnxBase.CBRecherche("SELECT Parameter From 
           WHERE NumOption=1"))
            {
                F.oCnxBase.Reader.Read();
                if (F.oCnxBase.Reader.GetString(0) == "Oui")
                {
                    TComposant.Add(", Techno"); WComposant.Add("AND TechnoRef.GuidTechnoRef=Techno.GuidTechnoRef");
                    TComposant.Add(", ServerPhy"); WComposant.Add("AND TechnoRef.GuidTechnoRef=ServerPhy.GuidTechnoRef");
                }
                else { TComposant.Add(""); WComposant.Add(""); }
            }
            else { TComposant.Add(""); WComposant.Add(""); }
            F.oCnxBase.CBReaderClose();
            */

            if (ArrayTreeNode.Length == 1)
            {
                DateTime dt = DateTime.Now;
                TimeSpan ts = new TimeSpan(180, 0, 0, 0);
                for (int i = 0; i < ArrayTreeNode[0].Nodes.Count; i++)
                {
                    bool CheckExistCadreRef = false;
                    ArrayList aDate = new ArrayList();
                    ArrayList aNorme = new ArrayList();
                    ArrayList aUsed = new ArrayList();

                    TreeNode tni = ArrayTreeNode[0].Nodes[i];
                    if (tni.Nodes.Count == 0)
                    {
                        for (int k = 0; k < TComposant.Count; k++)
                        {
                            if (F.oCnxBase.CBRecherche("Select MIN(ValIndicator), MAX(Norme), SUM(VNmoins1), SUM(VN), SUM(VNplus1) FROM Produit, TechnoRef, IndicatorLink, Indicator" + TComposant[k] + " WHERE TechnoRef.GuidProduit=Produit.GuidProduit and TechnoRef.GuidTechnoRef=IndicatorLink.GuidObjet and IndicatorLink.GuidIndicator=Indicator.GuidIndicator and NomIndicator='1-Fin Support' " + WComposant[k] + " AND Produit.GuidCadreRef='" + tni.Name + "' GROUP BY Produit.GuidCadreRef"))
                            {
                                CheckExistCadreRef = true;

                                DateTime dtc = DateTime.FromOADate(F.oCnxBase.Reader.GetDouble(0));
                                if (DateTime.Compare(dtc, dt) < 0) aDate.Add((double)Form1.ImgList.fail);
                                else if (DateTime.Compare(dtc - ts, dt) < 0) aDate.Add((double)Form1.ImgList.alert);
                                else aDate.Add((double)Form1.ImgList.pass);
                                
                                aNorme.Add((double)F.oCnxBase.Reader.GetInt16(1));
                                if (F.oCnxBase.Reader.IsDBNull(2)) aUsed.Add(0.0); else aUsed.Add((double)F.oCnxBase.Reader.GetDouble(2));
                                if (F.oCnxBase.Reader.IsDBNull(3)) aUsed.Add(0.0); else aUsed.Add((double)F.oCnxBase.Reader.GetDouble(3));
                                if (F.oCnxBase.Reader.IsDBNull(4)) aUsed.Add(0.0); else aUsed.Add((double)F.oCnxBase.Reader.GetDouble(4));
                            }
                            F.oCnxBase.CBReaderClose();
                        }
                        if (CheckExistCadreRef)
                        {
                            CadreRefN1 cr = new CadreRefN1(tni.Name);
                            
//indicator
                            cr.aIndicator.Add(aDate);
                            cr.aIndicator.Add(aNorme);
                            cr.aIndicator.Add(aUsed);
                            CadreRefN1InTV.Add(cr);
                        }
                    }
                    else
                    {
                        aDate.Add((double)Form1.ImgList.pass);
                        aNorme.Add(0.0);
                        aUsed.Add(0.0); //nbr used N-1
                        aUsed.Add(0.0); //nbr used N
                        aUsed.Add(0.0); //nbr used N+1
                        for (int j = 0; j < tni.Nodes.Count; j++)
                        {
                            ArrayList aDateTemp = new ArrayList();
                            ArrayList aNormeTemp = new ArrayList();
                            ArrayList aUsedTemp = new ArrayList();
                            
                            for (int k = 0; k < TComposant.Count; k++)
                            {
                                if (F.oCnxBase.CBRecherche("Select MIN(ValIndicator), MAX(Norme), SUM(VNmoins1), SUM(VN), SUM(VNplus1) FROM Produit, TechnoRef, IndicatorLink, Indicator" + TComposant[k] + " WHERE TechnoRef.GuidProduit=Produit.GuidProduit and TechnoRef.GuidTechnoRef=IndicatorLink.GuidObjet and IndicatorLink.GuidIndicator=Indicator.GuidIndicator and NomIndicator='1-Fin Support' " + WComposant[k] + " AND Produit.GuidCadreRef='" + tni.Nodes[j].Name + "' GROUP BY Produit.GuidCadreRef"))
                                {
                                    CheckExistCadreRef = true;

                                    DateTime dtc = DateTime.FromOADate(F.oCnxBase.Reader.GetDouble(0));
                                    if (DateTime.Compare(dtc, dt) < 0) aDateTemp.Add((double)Form1.ImgList.fail);
                                    else if (DateTime.Compare(dtc - ts, dt) < 0) aDateTemp.Add((double)Form1.ImgList.alert);
                                    else aDateTemp.Add((double)Form1.ImgList.pass);

                                    aNormeTemp.Add((double)F.oCnxBase.Reader.GetInt16(1));

                                    if (F.oCnxBase.Reader.IsDBNull(2)) aUsedTemp.Add(0.0); else aUsedTemp.Add((double)F.oCnxBase.Reader.GetDouble(2));
                                    if (F.oCnxBase.Reader.IsDBNull(3)) aUsedTemp.Add(0.0); else aUsedTemp.Add((double)F.oCnxBase.Reader.GetDouble(3));
                                    if (F.oCnxBase.Reader.IsDBNull(4)) aUsedTemp.Add(0.0); else aUsedTemp.Add((double)F.oCnxBase.Reader.GetDouble(4));

                                    if ((double)aDate[0] < (double)aDateTemp[0]) aDate[0] = aDateTemp[0];
                                    if ((double)aNorme[0] < (double)aNormeTemp[0]) aNorme[0] = aNormeTemp[0];
                                    for (int n = 0; n < aUsed.Count; n++) aUsed[n] = (double)aUsed[n] + (double)aUsedTemp[n];
                                }
                                F.oCnxBase.CBReaderClose();
                            }
                            
                        }
                        if (CheckExistCadreRef)
                        {
                            CadreRefN1 cr = new CadreRefN1(tni.Name);
//indicator

                            cr.aIndicator.Add(aDate);
                            cr.aIndicator.Add(aNorme);
                            cr.aIndicator.Add(aUsed);
                            CadreRefN1InTV.Add(cr);
                        }
                    }
                }
            }
            return CadreRefN1InTV;  
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

            n = GetIndexFromName("Couleur");
            if (n > -1 && e.RowIndex == n)
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceColor();
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }
        }
    }
}
