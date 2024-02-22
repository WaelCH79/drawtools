using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawProduit : DrawTools.DrawRectangle
	{
        static private Color Couleur = Color.Snow;
        static private int LineWidth = 1;

		public DrawProduit()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawProduit(Form1 of)
        {
            F = of;
            SetRectangle(0, 0, 1, 1);
            InitProp();
            Initialize();
        }

        public DrawProduit(Form1 of, int x, int y, int width, int height, int count)
        {
            F = of;
            OkMove = true;
            Align = true;
            Rectangle = new Rectangle(x, y, width, height);
            LstParent = new ArrayList();
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

        public DrawProduit(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = true;
            Align = true;
            InitRectangle(lstValG);
            LstParent = new ArrayList();
            LstChild = new ArrayList();
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

        public int NbrIndicator()
        {
            int CountObj = 0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawIndicator)) CountObj++;

            return CountObj;
        }

        public void AligneObjet()
        {
            int CountC;

            CountC = NbrIndicator();
            if (CountC != 0)
            {
                //int i = CountC - 1;
                int i = 0;

                //for (int j = LstChild.Count - 1; j >= 0; j--)
                for (int j=0; j<LstChild.Count; j++)
                {
                    if (LstChild[j].GetType() == typeof(DrawIndicator))
                    {
                        DrawRectangle o = (DrawRectangle)LstChild[j];

                        o.rectangle.Width = HeightIndicator;
                        o.rectangle.X = Rectangle.Right - (CountC-i) * (o.Rectangle.Width + Axe);
                        o.rectangle.Y = Rectangle.Y;
                        o.rectangle.Height = HeightIndicator;
                        i++;
                        //i--;
                    }
                }
            }
        }
             

        public override bool AttachPointInObject(Point point)
        {
            return false;
        }

        public void CreateIndicator(CadreRefN1 cr)
        {
            for (int i = 0; i < cr.aIndicator.Count; i++)
            {
                F.drawArea.tools[(int)DrawArea.DrawToolType.Indicator].CreatObjetFromCadreRef((ArrayList)cr.aIndicator[i]);

                DrawIndicator di = (DrawIndicator)F.drawArea.GraphicsList[0];
                AttachLink(di, DrawObject.TypeAttach.Child);
                di.AttachLink(this, DrawObject.TypeAttach.Parent);
            }
        }

        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(Couleur, LineWidth);
            Rectangle r;
            //DrawRectangle dp;
                       
            r = DrawRectangle.GetNormalizedRectangle(Rectangle);
            if (LstParent.Count != 0)
            {
                //dp = (DrawRectangle)LstParent[0];
                g.DrawLine(pen, r.Left + Axe, r.Top, r.Right, r.Top);
            }
            //r.Height = 2 * HeightCadreRef + HeightIndicator + 3 * Axe;
            //g.DrawRectangle(pen, r);
            DrawGrpTxt(g, 3, 0, r.Left + Axe, r.Top, 0, Color.Black, Color.Transparent);
            pen.Dispose();
            AligneObjet();
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

/*
        public ArrayList FindCadreRefN1FromTv()
        {
            ArrayList CadreRefN1InTV = new ArrayList();
            TreeNode[] ArrayTreeNode = F.tvObjet.Nodes.Find(GuidkeyObjet.ToString(), true);
            
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
                        if (F.oCnxBase.CBRecherche("Select MIN(DateFinMain), MAX(Norme), SUM(VNmoins1), SUM(VN), SUM(VNplus1) FROM Produit, TechnoRef, Techno WHERE TechnoRef.GuidTechnoRef=Techno.GuidTechnoRef AND TechnoRef.GuidProduit=Produit.GuidProduit AND GuidCadreRef='" + tni.Name + "' GROUP BY GuidCadreRef"))
                        {
                            CheckExistCadreRef = true;

                            if (DateTime.Compare(F.oCnxBase.Reader.GetDate(0), dt) < 0) aDate.Add((double)Form1.ImgList.fail);
                            else if (DateTime.Compare(F.oCnxBase.Reader.GetDate(0) - ts, dt) < 0) aDate.Add((double)Form1.ImgList.alert);
                            else aDate.Add((double)Form1.ImgList.pass);

                            aNorme.Add((double)F.oCnxBase.Reader.GetInt16(1));

                            if (F.oCnxBase.Reader.IsDBNull(2)) aUsed.Add(0.0); else aUsed.Add(F.oCnxBase.Reader.GetDouble(2));
                            if (F.oCnxBase.Reader.IsDBNull(3)) aUsed.Add(0.0); else aUsed.Add(F.oCnxBase.Reader.GetDouble(3));
                            if (F.oCnxBase.Reader.IsDBNull(4)) aUsed.Add(0.0); else aUsed.Add(F.oCnxBase.Reader.GetDouble(4));

                        }
                        F.oCnxBase.CBReaderClose();
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

                            if (F.oCnxBase.CBRecherche("Select MIN(DateFinMain), MAX(Norme), SUM(VNmoins1), SUM(VN), SUM(VNplus1) FROM Produit, TechnoRef, Techno WHERE TechnoRef.GuidTechnoRef=Techno.GuidTechnoRef AND TechnoRef.GuidProduit=Produit.GuidProduit AND GuidCadreRef='" + tni.Nodes[j].Name + "' GROUP BY GuidCadreRef"))
                            {
                                CheckExistCadreRef = true;

                                if (DateTime.Compare(F.oCnxBase.Reader.GetDate(0), dt) < 0) aDateTemp.Add((double)Form1.ImgList.fail);
                                else if (DateTime.Compare(F.oCnxBase.Reader.GetDate(0) - ts, dt) < 0) aDateTemp.Add((double)Form1.ImgList.alert);
                                else aDateTemp.Add((double)Form1.ImgList.pass);

                                aNormeTemp.Add((double)F.oCnxBase.Reader.GetInt16(1));

                                if (F.oCnxBase.Reader.IsDBNull(2)) aUsedTemp.Add(0.0); else aUsedTemp.Add(F.oCnxBase.Reader.GetDouble(2));
                                if (F.oCnxBase.Reader.IsDBNull(3)) aUsedTemp.Add(0.0); else aUsedTemp.Add(F.oCnxBase.Reader.GetDouble(3));
                                if (F.oCnxBase.Reader.IsDBNull(4)) aUsedTemp.Add(0.0); else aUsedTemp.Add(F.oCnxBase.Reader.GetDouble(4));

                                if ((double)aDate[0] < (double)aDateTemp[0]) aDate[0] = aDateTemp[0];
                                if ((double)aNorme[0] < (double)aNormeTemp[0]) aNorme[0] = aNormeTemp[0];
                                for (int n = 0; n < aUsed.Count; n++) aUsed[n] = (double)aUsed[n] + (double)aUsedTemp[n];
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
                }
            }
            return CadreRefN1InTV;  
        }
 */

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


        public override void savetoDB()
        {
            if (!savetoDBFait())
            {
                base.savetoDB();

                savetoDBOK();
            }
        }
        

        /*
        public override System.Xml.XmlElement savetoXml(System.Xml.XmlElement elVue, bool GObj)
        {
            return base.savetoXml(elVue, false);
            //Creat Ttes les TechnoRef liées au Produit
            //F.oCnxBase.CBRecherche("SELECT GuidTechnoRef FROM TechnoRef WHERE GuidProduit='" + GuidkeyObjet.ToString() + "'");
        }
        */
    }
}
