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
	public class DrawGenks : DrawTools.DrawRectangle
	{
		public DrawGenks()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawGenks(Form1 of, Dictionary<string, object> dic)
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

        public DrawGenks(Form1 of, int x, int y, int width, int height, int count)
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
            Texte = "Genks" + count;
            Guidkey = Guid.NewGuid();

            InitProp();
            SetValueFromName("TypeIt", 1);
            Initialize();
        }

        public DrawGenks(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = true;
            Align = true;
            InitRectangle(lstValG);
            CorrectionRatio();
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

        public DrawGenks(Form1 of, ArrayList lstVal)
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
        }

        public DrawGenks(Form1 of)
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
            SetValueFromName("NomGenks", Texte);
            SetValueFromName("GuidGenks", (string)F.tvObjet.SelectedNode.Name);
            Initialize();
        }

        public override bool AttachPointInObject(Point point)
        {
            return false;
        }

        public override void dataGrid_CellClick(DataGridView odgv, DataGridViewCellEventArgs e)
        {
            int n;

            n = GetIndexFromName("GuidLabel");
            if (n > -1 && e.RowIndex == n) // Link Label
            {
                FormLabel fl = new FormLabel(F, odgv);
                fl.AddtvLabelClassFromDB();
                fl.AddlDestinationFromProp();
                //fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fl.ShowDialog(F);
            }
        }

        public override void Draw(Graphics g)
        {
            ToolGenks ti = (ToolGenks)F.drawArea.tools[(int)DrawArea.DrawToolType.Genks];
            TemplateDt oTemplate = (TemplateDt)ti.oLayers[0].lstTemplate[ti.GetTemplate((string)GetValueFromName("GuidLayer"))];

            Pen pen = new Pen(ti.LineCouleur, ti.LineWidth);
            Pen pen1 = new Pen(ti.LineCouleur, ti.Line1Width);
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
        }
        

        public override void savetoDB()
        {
            if (!savetoDBFait())
            {
                base.savetoDB();
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
            if (cTypeVue == '2')
            {
                string sType = GetType().Name.Substring("Draw".Length);
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
                else if (cw.Exist(sType) > -1)
                {
                    //sType ne doit pas depasse 4 caracteres
                    cw.InsertTextFromId(sType, false, "\n", null);
                    cw.CreatIdFromIdP(sBook, sType);
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
                
                InsertChild(cw, cTypeVue, sBook, typeof(DrawGensvc).ToString().Substring(17, 3) + sGuid, "Services", typeof(DrawGensvc).ToString());
                InsertChild(cw, cTypeVue, sBook, typeof(DrawGening).ToString().Substring(17, 3) + sGuid, "Ingres", typeof(DrawGening).ToString());
                InsertChild(cw, cTypeVue, sBook, typeof(DrawGenpod).ToString().Substring(17, 3) + sGuid, "Pods", typeof(DrawGenpod).ToString());
            }
        }
	}
}
