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
	public class DrawInsnd : DrawTools.DrawRectangle
	{
		public DrawInsnd()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawInsnd(Form1 of, Dictionary<string, object> dic)
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

        public DrawInsnd(Form1 of, int x, int y, int width, int height, int count)
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
            GuidkeyObjet = Guid.NewGuid();
            Texte = "Insnd" + count;
            Guidkey = Guid.NewGuid();

            InitProp();
            SetValueFromName("TypeIt", 1);
            Initialize();
        }

        
        public DrawInsnd(Form1 of, ArrayList lstVal, ArrayList lstValG)
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

        public DrawInsnd(Form1 of)
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
            ToolInsnd tind = (ToolInsnd)F.drawArea.tools[(int)DrawArea.DrawToolType.Insnd];
            TemplateDt oTemplate = (TemplateDt)tind.oLayers[0].lstTemplate[tind.GetTemplate((string)GetValueFromName("GuidLayer"))];

            Pen pen = new Pen(tind.LineCouleur, tind.LineWidth);
            Pen pen1 = new Pen(tind.LineCouleur, tind.Line1Width);
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

        public int NbrNCard()
        {
            int Nbr = 0;

            for (int i = 0; i < LstChild.Count; i++)

                if (LstChild[i].GetType() == typeof(DrawNCard))
                    Nbr++;

            return Nbr;
        }

        public void AligneObjet()
        {
            int nbrNCard = NbrNCard(), WidthNCard = 0;
            if (nbrNCard > 0) WidthNCard = (Rectangle.Width - 2 * radius - Axe) / nbrNCard;

            for (int i = LstChild.Count - 1; i >= 0; i--)
            {
                if (LstChild[i].GetType() == typeof(DrawNCard))
                {
                    int y = YMax() - Axe - EpaisseurCard;
                    ((DrawNCard)LstChild[i]).Aligne(Rectangle.Left + WidthNCard * (nbrNCard - 1) + radius + Axe, y, WidthNCard - Axe, EpaisseurCard);
                    nbrNCard--;
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
    }
}
