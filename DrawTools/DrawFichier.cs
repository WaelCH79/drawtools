using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data.Odbc;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawModule : DrawTools.DrawRectangle
	{

        private static string[] sProprietes = { "Version", "Editeur", "Applications", "CadreRef", "Composants" }; 

		public DrawModule()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawModule(Form1 of, int x, int y, int width, int height,int count)
        {
            string sGuidKeyObjet;

            F = of;
            Rectangle = new Rectangle(x, y, width, height);
            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = new ArrayList();
            LstLinkOut = new ArrayList();
            if (F.drawArea.AddObjet)
            {
                F.drawArea.AddObjet = false;
                
                Texte = F.tvObjet.SelectedNode.Text;
                sGuidKeyObjet = F.oCnxBase.GetGuidModule(Texte);
                if (sGuidKeyObjet.Length != 0)
                {
                    GuidkeyObjet = new Guid(sGuidKeyObjet);
                }
                else
                {
                    GuidkeyObjet = Guid.NewGuid();
                    Texte = "Module" + count;
                }
            }
            else
            {
                GuidkeyObjet = Guid.NewGuid();
                Texte = "Module" + count;
            }
            Guidkey = Guid.NewGuid();
            Initialize();
        }

        public DrawModule(Form1 of ,string sGuidKeyObjet, string sTexte, int x, int y, int width, int height)
        {
            F = of;
            Rectangle = new Rectangle(x, y, width, height);
            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = new ArrayList();
            LstLinkOut = new ArrayList();
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = new Guid(sGuidKeyObjet);
            Texte = sTexte;
            Initialize();
        }

        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(Color, PenWidth);
            int iHeightFont;
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10) {
                //g.DrawRectangle(pen, r);
                g.DrawRectangle(pen, r);
                if (r.Height > 26) iHeightFont = 16; else iHeightFont = r.Height - 10;

                g.DrawString(Texte, new Font("Times New Roman", iHeightFont), Brushes.Black, r.X+10, r.Y+ r.Height/2-3*iHeightFont/4);
            //    g.DrawRectangle(pen, r.X+10, r.Y, r.Width-20, 20);
            //    g.DrawRectangle(pen, r.X, r.Y + 10, r.Width, r.Height - 10);
            } else g.DrawRectangle(pen, r);

            pen.Dispose();
        }

        /// <summary>
        /// Save Object to the Data Base
        /// </summary>
        public override void savetoDB()
        {
            // Recherche dans la table Module
            if (!F.oCnxBase.ExistGuidModule(GuidkeyObjet))
            {
                // Creation des liens avec le nouveau Module
                F.oCnxBase.CreatModule(GuidkeyObjet, Texte); // Table Module
                F.oCnxBase.CreatDansTypeVue(GuidkeyObjet, "Module"); // Table DansTypeVue
            }
            else
                F.oCnxBase.UpdateModule(GuidkeyObjet, Texte); // Update de la Table Module

            //Recherche GModule
            if (!F.oCnxBase.ExistGuidGModule(Guidkey))
            {
                // Creation des liens avec le nouveau GModule 
                F.oCnxBase.CreatGModule(Guidkey, GuidkeyObjet, Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height); //Table GModule
                F.oCnxBase.CreatDansVue(Guidkey, "GModule");  // Table DansVue
            }
        }


        public override void InitDatagrid(Form1 f)
        {
            IEnumerator ienum = sProprietes.GetEnumerator();
                        
            f.tbGuid.Text = Guidkey.ToString();
            f.tbNom.Text = Texte;

            ienum.Reset();
            while (ienum.MoveNext())
            {
                string[] row = { (string)ienum.Current, ""};
                f.dataGrid.Rows.Add(row);
            }

        }

	}
}
