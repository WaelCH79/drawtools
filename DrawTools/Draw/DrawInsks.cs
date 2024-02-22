using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using MOI = Microsoft.Office.Interop;
 
using Newtonsoft.Json;
using System.Xml;
using System.Collections.Generic;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawInsks : DrawTools.DrawRectangle
	{
		public DrawInsks()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawInsks(Form1 of, Dictionary<string, object> dic)
        {
            F = of;
            object o = null;
            OkMove = true;
            Align = true;

            LstParent = null;
            LstChild = new ArrayList();
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = new ArrayList();
            Guidkey = Guid.NewGuid();
            dicObj = dic;
            InitProp();
            InitValueFromDic(dic);
            InitRectangle(LstValue, false);

            o = GetValueFromLib("Guid");
            if (o != null)
                GuidkeyObjet = new Guid((string)o);
            o = GetValueFromLib("Nom");
            if (o != null)
                Texte = (string)o;

            Initialize();
        }

        public DrawInsks(Form1 of, int x, int y, int width, int height, int count)
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
            GuidkeyObjet = Guid.NewGuid();
            Texte = "Insks" + count;
            Guidkey = Guid.NewGuid();

            InitProp();
            SetValueFromName("TypeIt", 1);
            Initialize();
        }

        public DrawInsks(Form1 of, int x, int y, int width, int height, DrawGenks dgk,  int count)
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
            GuidkeyObjet = Guid.NewGuid();
            Guidkey = Guid.NewGuid();

            InitProp();

            SetValueFromName("NomInsks", (string) dgk.GetValueFromName("NomGenks") + "_I");
            SetValueFromName("TypeIt", dgk.GetValueFromName("TypeIt"));
            SetValueFromName("Guidgenks", dgk.GetValueFromName("Guidgenks"));
            Texte = (string)GetValueFromName("NomInsks");
            Initialize();
        }

        public DrawInsks(Form1 of, int x, int y, int width, int height, ArrayList lstVal, int count)
        {
            F = of;
            object o = null;

            OkMove = true;
            Align = true;
            Rectangle = new Rectangle(x, y, width, height);
            LstParent = null;
            LstChild = new ArrayList();
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = lstVal;

            o = GetValueFromLib("Guid");
            if (o != null)
                GuidkeyObjet = new Guid((string)o);
            o = GetValueFromLib("Nom");
            Guidkey = Guid.NewGuid();
            Initialize();
        }

        public DrawInsks(Form1 of, ArrayList lstVal)
        {
            F = of;
            object o = null;

            LstParent = null;
            LstChild = new ArrayList();
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = lstVal;

            o = GetValueFromLib("Guid");
            if (o != null)
                GuidkeyObjet = new Guid((string)o);
            o = GetValueFromLib("Nom");
            Guidkey = Guid.NewGuid();
        }

        public DrawInsks(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = true;
            Align = true;
            InitRectangle(lstValG);
            LstParent = null;
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

        public DrawInsks(Form1 of)
        {
            F = of;
            OkMove = false;
            Align = false;
            Rectangle = new Rectangle(1, 1, 1, 1);
            LstParent = new ArrayList(); // Vue Inf
            LstChild = new ArrayList();
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = new ArrayList();

            string[] aValue = F.tvObjet.SelectedNode.Text.Split('-');
            Texte = aValue[0].Trim();

            GuidkeyObjet = Guid.NewGuid();
            Guidkey = Guid.NewGuid();

            InitProp();
            SetValueFromName("NomInsks", Texte);
            SetValueFromName("GuidInsks", (string)F.tvObjet.SelectedNode.Name);
            Initialize();
        }

        public override bool AttachPointInObject(Point point)
        {
            return false;
        }

        public override void Draw(Graphics g)
        {
            ToolInsks tik = (ToolInsks)F.drawArea.tools[(int)DrawArea.DrawToolType.Insks];
            TemplateDt oTemplate = (TemplateDt)tik.oLayers[0].lstTemplate[tik.GetTemplate((string)GetValueFromName("GuidLayer"))];

            Pen pen = new Pen(tik.LineCouleur, tik.LineWidth);
            Pen pen1 = new Pen(tik.LineCouleur, tik.Line1Width);
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10)
            {
                AffRec(g, r, oTemplate, 0);

                DrawGrpTxt(g, 1, 0, r.Left + HeightMaxIcon, r.Top + Axe, 0, oTemplate.Pen1Couleur, oTemplate.BkGrCouleur);
                AffIcon(g, r, oTemplate);


            }
            else g.DrawRectangle(pen, r);
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

        public void Loadns(string sGuid)
        {
            int j = F.drawArea.GraphicsList.FindObjet(0, sGuid);
            if (j < 0)
            {
                F.drawArea.tools[(int)DrawArea.DrawToolType.Insns].LoadSimpleObject(sGuid);
            }
        }

        public void Loadnd(string sGuid)
        {
            int j = F.drawArea.GraphicsList.FindObjet(0, sGuid);
            if (j < 0)
            {
                F.drawArea.tools[(int)DrawArea.DrawToolType.Insnd].LoadSimpleObject(sGuid);
            }
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

        public int NbrNode()
        {
            int CountObj = 0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawInsnd)) CountObj++;

            return CountObj;
        }

        public void AligneObjet()
        {
            //HeightNode, WidthNode, HeightMinNameSpace, WidthMinNameSpace : définis dans drawobjects
            int xNode = 0;
            int CountNode = NbrNode(), WidthNode = 0;
            if (CountNode > 0) WidthNode = (Rectangle.Width - Axe - WidthMaxIcon - (CountNode - 1) * (WidthShiftIcon + Axe)) / CountNode;

            for (int i = LstChild.Count - 1; i >= 0; i--)
            {
                if (LstChild[i].GetType() == typeof(DrawInsnd))
                {
                    ((DrawInsnd)LstChild[i]).Aligne(Rectangle.Left + WidthMaxIcon + (WidthNode + Axe + WidthShiftIcon) * xNode, Rectangle.Bottom - HeightNode - Axe, WidthNode, HeightNode);
                    xNode++;

                }
                
                else if (LstChild[i].GetType() == typeof(DrawInsns))
                {
                    int w, h;
                    if (Rectangle.Width > WidthMaxIcon + WidthMinNameSpace + Axe) w = Rectangle.Width - WidthMaxIcon - Axe; else w = WidthMinNameSpace;
                    //if (Rectangle.Width > WidthMaxIcon + WidthNode + WidthShiftIcon + WidthMinNameSpace + 2 * Axe) w = Rectangle.Width - WidthMaxIcon - WidthNode - WidthShiftIcon - 2 * Axe; else w = WidthMinNameSpace;
                    if (Rectangle.Height > HeightMaxIcon + HeightMinNameSpace + HeightShiftIcon + 2 * Axe + HeightNode) h = Rectangle.Height - HeightMaxIcon - 2 * Axe - HeightShiftIcon - HeightNode; else h = HeightMinNameSpace;
                    ((DrawInsns)LstChild[i]).Aligne(Rectangle.X + WidthMaxIcon, Rectangle.Y + HeightMaxIcon, w, h);
                    //((DrawInsns)LstChild[i]).AligneObjet();
                }
                
            }
        }

        public void AddNameSpace()
        {
            ArrayList lstNameSpace = F.oCnxBase.CreatNameSpace(this);

            for (int i = 0; i < lstNameSpace.Count; i++) this.Loadns((string)lstNameSpace[i]);
            this.AligneObjet();
        }



        public override void dataGrid_CellClick(DataGridView odgv, DataGridViewCellEventArgs e)
        {
            int n;

            n = GetIndexFromName("NameSpace");
            if (n > -1 && e.RowIndex == n)
                AddNameSpace();
            /*
            n = GetIndexFromName("NomFonction");
            if (n > -1 && e.RowIndex == n) // Service/protole
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromTv("FonctionServer");
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }
            */
        }

        
        public override void savetoDB()
        {
            if (!savetoDBFait())
            {
                base.savetoDB();

                if (LstChild != null)
                {
                    for (int i = 0; i < LstChild.Count; i++)
                    {
                        if (LstChild[i].GetType() == typeof(DrawInsns))
                        {
                            DrawInsns dins = (DrawInsns)LstChild[i];
                            if (!F.oCnxBase.ExistGuid(0, dins)) F.oCnxBase.CreatObject(dins); // Table Objet
                            else F.oCnxBase.UpdateObject(dins); // Update de la Table Objet
                            for (int nsChild = 0; nsChild < dins.LstChild.Count; nsChild++)
                            {
                                if (dins.LstChild[nsChild].GetType() == typeof(DrawInspod))
                                {
                                    DrawInspod dipod = (DrawInspod)dins.LstChild[nsChild];
                                    if (!F.oCnxBase.ExistGuid(0, dipod)) F.oCnxBase.CreatObject(dipod); // Table Objet
                                    else F.oCnxBase.UpdateObject(dipod); // Update de la Table Objet
                                    dipod.SetLabelLinks(); //Label
                                    for (int podChild = 0; podChild < dipod.LstChild.Count; podChild++)
                                    {
                                        if (dipod.LstChild[podChild].GetType() == typeof(DrawInssvc))
                                        {
                                            DrawInssvc disvc = (DrawInssvc)dipod.LstChild[podChild];
                                            if (!F.oCnxBase.ExistGuid(0, disvc)) F.oCnxBase.CreatObject(disvc); // Table Objet
                                            else F.oCnxBase.UpdateObject(disvc); // Update de la Table Objet
                                            disvc.SetLabelLinks(); // Label
                                            for (int svcChild = 0; svcChild < disvc.LstChild.Count; svcChild++)
                                            {
                                                if (disvc.LstChild[svcChild].GetType() == typeof(DrawNCard))
                                                {
                                                    DrawNCard dnc = (DrawNCard)disvc.LstChild[svcChild];
                                                    if (!F.oCnxBase.ExistGuid(0, dnc)) F.oCnxBase.CreatObject(dnc); // Table Objet
                                                    else F.oCnxBase.UpdateObject(dnc); // Update de la Table Objet
                                                }
                                            }
                                        }
                                        if (dipod.LstChild[podChild].GetType() == typeof(DrawInsing))
                                        {
                                            DrawInsing diing = (DrawInsing)dipod.LstChild[podChild];
                                            if (!F.oCnxBase.ExistGuid(0, diing)) F.oCnxBase.CreatObject(diing); // Table Objet
                                            else F.oCnxBase.UpdateObject(diing); // Update de la Table Objet
                                            diing.SetLabelLinks(); // Label
                                            for (int ingChild = 0; ingChild < diing.LstChild.Count; ingChild++)
                                            {
                                                if (diing.LstChild[ingChild].GetType() == typeof(DrawNCard))
                                                {
                                                    DrawNCard dnc = (DrawNCard)diing.LstChild[ingChild];
                                                    if (!F.oCnxBase.ExistGuid(0, dnc)) F.oCnxBase.CreatObject(dnc); // Table Objet
                                                    else F.oCnxBase.UpdateObject(dnc); // Update de la Table Objet
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (LstChild[i].GetType() == typeof(DrawInsnd))
                        {
                            DrawInsnd dind = (DrawInsnd)LstChild[i];
                            if (!F.oCnxBase.ExistGuid(0, dind)) F.oCnxBase.CreatObject(dind); // Table Objet
                            else F.oCnxBase.UpdateObject(dind); // Update de la Table Objet
                            for (int ndChild = 0; ndChild < dind.LstChild.Count; ndChild++)
                            {
                                if (dind.LstChild[ndChild].GetType() == typeof(DrawNCard))
                                {
                                    DrawNCard dnc = (DrawNCard)dind.LstChild[ndChild];
                                    if (!F.oCnxBase.ExistGuid(0, dnc)) F.oCnxBase.CreatObject(dnc); // Table Objet
                                    else F.oCnxBase.UpdateObject(dnc); // Update de la Table Objet
                                }

                            }
                        }

                    }
                }
                savetoDBOK();
            }
        }

        private void InsertChild(ControlDoc cw, char cTypeVue, string sBook, string sBookChild, string sChild, string sType)
        {
            if (cw.Exist(sBook) > -1)
            {
                for (int i = 0; i < LstChild.Count; i++)
                {
                    DrawObject o = (DrawObject)LstChild[i];
                    if (o.GetType().ToString() == sType)
                    {
                        if (cw.Exist(sBookChild) == -1)
                        {
                            cw.InsertTextFromId(sBook, false, sChild + "\n", "Titre 4");
                            cw.InsertTextFromId(sBook, false, "\n", null);
                            cw.CreatIdFromIdP(sBookChild, sBook);
                        }
                        o.CWInsertChild(cw, cTypeVue);
                    }
                }
            }
        }


        public override void CWInsert(ControlDoc cw, char cTypeVue)
        {

            Guid guid = F.GuidVue;
            string sGuidVue = guid.ToString().Replace("-", "");

            string sType = GetType().Name.Substring("Draw".Length);
            string sksBook = sType + sGuidVue;
            string sGuid = cTypeVue + GuidkeyObjet.ToString().Replace("-", "");
            string sBook = sType.Substring(0, 3) + sGuid;
            string sVueBookmark = "Diag" + sGuid;
            string sDiagram = F.SaveDiagramFromPath(sVueBookmark, cw.getImagePath(), GuidkeyObjet.ToString());

            if (cw.Exist("n" + sGuid) > -1)
            {
                cw.InsertTextFromId("n" + sGuid, true, Texte, "Titre 3");

                cw.InsertTextFromId(sVueBookmark, true, "\n", null);
                cw.InsertImgFromId(sVueBookmark, false, sDiagram, null);
                cw.InsertTextFromId(sVueBookmark, false, "\n", null);
            }
            else if (cw.Exist(sksBook) > -1)
            {
                //sType ne doit pas depasse 4 caracteres
                cw.InsertTextFromId(sksBook, false, "\n", null);
                cw.CreatIdFromIdP(sBook, sksBook);
                cw.InsertTextFromId(sBook, true, Texte + "\n", "Titre 3");
                cw.CreatIdFromIdP("n" + sGuid, sBook);
                CWInsertProp(cw, sBook, "P");
                cw.InsertTextFromId(sBook, false, "Diagram\n", "Titre 6");
                cw.InsertTextFromId(sBook, false, "\n", null);
                cw.CreatIdFromIdP(sVueBookmark, sBook);
                cw.InsertTextFromId(sVueBookmark, true, "\n", null);
                cw.InsertImgFromId(sVueBookmark, false, sDiagram, null);
                cw.InsertTextFromId(sVueBookmark, false, "\n", null);

            }
            
            InsertChild(cw, cTypeVue, sBook, typeof(DrawInsns).ToString().Substring(17, 2) + sGuid, "Name space", typeof(DrawInsns).ToString());
            InsertChild(cw, cTypeVue, sBook, typeof(DrawInsnd).ToString().Substring(17, 2) + sGuid, "Nodes", typeof(DrawInsnd).ToString());
            
        }
	}
}
