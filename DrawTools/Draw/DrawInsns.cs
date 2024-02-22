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
	public class DrawInsns : DrawTools.DrawRectangle
	{
		public DrawInsns()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawInsns(Form1 of, Dictionary<string, object> dic)
        {
            F = of;
            object o = null;
            OkMove = true;
            Align = true;

            LstParent = new ArrayList();
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

        public DrawInsns(Form1 of, int count)
        {
            F = of;
            OkMove = true;
            Align = true;
            Rectangle = new Rectangle(1, 1, 1, 1);
            LstParent = new ArrayList();
            LstChild = new ArrayList();
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = new ArrayList();
            GuidkeyObjet = Guid.NewGuid();
            Texte = "Insns" + count;
            Guidkey = Guid.NewGuid();

            InitProp();
            SetValueFromName("TypeIt", 1);
            SetValueFromName("GuidVue", of.GuidVue.ToString());
            Initialize();
        }

        
        public DrawInsns(Form1 of, ArrayList lstVal, ArrayList lstValG)
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

        public DrawInsns(Form1 of)
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
            SetValueFromName("NomInsnd", Texte);
            SetValueFromName("GuidInsnd", (string)F.tvObjet.SelectedNode.Name);
            SetValueFromName("GuidVue", of.GuidVue.ToString());
            Initialize();
        }

        public override bool AttachPointInObject(Point point)
        {
            return false;
        }

        public override void AttachLink(DrawObject o, TypeAttach Attach)
        {
            string oParent = "GuidInsks";

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
            ToolInsns tins = (ToolInsns)F.drawArea.tools[(int)DrawArea.DrawToolType.Insns];
            TemplateDt oTemplate = (TemplateDt)tins.oLayers[0].lstTemplate[tins.GetTemplate((string)GetValueFromName("GuidLayer"))];

            Pen pen = new Pen(tins.LineCouleur, tins.LineWidth);
            Pen pen1 = new Pen(tins.LineCouleur, tins.Line1Width);
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

        private int NbrPod()
        {
            int Nbr = 0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawInspod))
                    Nbr++;
            return Nbr;
        }

        public void AligneObjet()
        {
            int nbrPod = NbrPod(), Widthpod = 0;
            if(nbrPod > 0) Widthpod = (Rectangle.Width - Axe - WidthMaxIcon - (nbrPod -1) * (WidthShiftIcon + Axe)) / nbrPod;
            
            for (int i = LstChild.Count - 1; i >= 0; i--)
            {
                if (LstChild[i].GetType() == typeof(DrawInspod))
                {
                    ((DrawInspod)LstChild[i]).Aligne(Rectangle.X +  WidthMaxIcon + i * (Widthpod + Axe + WidthShiftIcon), Rectangle.Y + HeightMaxIcon, Widthpod, Rectangle.Height - HeightMaxIcon - Axe);
                }
            }
        }

        public void Aligne(int x, int y, int width, int height)
        {
            Rectangle = new Rectangle(x, y, width, height);
            AligneObjet();
        }
        /*
        public override void dataGrid_CellClick(DataGridView odgv, DataGridViewCellEventArgs e)
        {
            int n;

            n = GetIndexFromName("NomFonction");
            if (n > -1 && e.RowIndex == n) // Service/protole
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromTv("FonctionServer");
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }
        }
        */

        public override void savetoDB()
        {
            if (!savetoDBFait())
            {
                savetoDBOK();
            }
        }

        /*
        public override string CWInsertChild(ControlDoc cw, char cTypeVue)
        {
            string sType = GetType().Name.Substring("Draw".Length);
            string sShortType = sType.Substring(3, sType.Length - 3);
            string sGuidP = sShortType + cTypeVue + ((DrawObject)LstParent[0]).GuidkeyObjet.ToString().Replace("-", "");
            string sGuid = cTypeVue + GuidkeyObjet.ToString().Replace("-", "");

            string sBook = sType.Substring(0, 3) + sGuid;

            if (cw.Exist("n" + sGuid) > -1)
            {
                cw.InsertTextFromId(sGuid, true, Texte, "Titre 4");
                cw.InsertTabFromId("n" + sBook, true, this, null, false, null);
            }
            else if (cw.Exist(sGuidP) > -1)
            {
                cw.InsertTextFromId(sGuidP, false, "\n", null);
                cw.CreatIdFromIdP(sBook, sGuidP);
                cw.InsertTextFromId(sBook, true, Texte + "\n", "Titre 4");
                cw.CreatIdFromIdP(sGuid, sBook);
                CWInsertProp(cw, sBook, "P");

                cw.InsertTextFromId(sBook, false, "Specifications\n", "Titre 6");
                cw.InsertTextFromId(sBook, false, "\n", null);
                cw.CreatIdFromIdP("n" + sGuid, sBook);
                cw.InsertTextFromId("n" + sGuid, false, "\n", null);
                cw.InsertTabFromId("n" + sGuid, false, this, null, false, null);
            }
            return sBook;

        }
        */

    }
}
