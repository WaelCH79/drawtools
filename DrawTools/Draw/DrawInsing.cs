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
	public class DrawInsing : DrawTools.DrawRectangle
	{
		public DrawInsing()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawInsing(Form1 of, Dictionary<string, object> dic)
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

        public DrawInsing(Form1 of, String[] aGening, int count)
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
            Texte = aGening[1] + "_I";
            Guidkey = Guid.NewGuid();

            InitProp();
            SetValueFromName("TypeIt", 1);
            SetValueFromName("GuidGening", aGening[0]);
            Initialize();
        }

        
        public DrawInsing(Form1 of, ArrayList lstVal, ArrayList lstValG)
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

        public DrawInsing(Form1 of)
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
            SetValueFromName("NomInsing", Texte);
            SetValueFromName("GuidInsing", (string)F.tvObjet.SelectedNode.Name);
            Initialize();
        }

        public override bool AttachPointInObject(Point point)
        {
            return false;
        }

        public override void AttachLink(DrawObject o, TypeAttach Attach)
        {
            string oParent = "GuidInspod";

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
            ToolInsing tiing = (ToolInsing)F.drawArea.tools[(int)DrawArea.DrawToolType.Insing];
            TemplateDt oTemplate = (TemplateDt)tiing.oLayers[0].lstTemplate[tiing.GetTemplate((string)GetValueFromName("GuidLayer"))];

            Pen pen = new Pen(tiing.LineCouleur, tiing.LineWidth);
            Pen pen1 = new Pen(tiing.LineCouleur, tiing.Line1Width);
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10)
            {
                AffRec(g, r, oTemplate, 0);

                DrawGrpTxt(g, 1, 0, r.Left + HeightMaxIcon, r.Top + HeightCard + Axe, 0, oTemplate.Pen1Couleur, oTemplate.BkGrCouleur);
                AffIcon(g, r, oTemplate);


            }
            else g.DrawRectangle(pen, r);

            pen.Dispose();
        }

        public override void dataGrid_CellClick(DataGridView odgv, DataGridViewCellEventArgs e)
        {
            int n;

            n = GetIndexFromName("GuidApplication");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromTv("01PatternApplication");
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }

            n = GetIndexFromName("GuidLabel");
            if (n > -1 && e.RowIndex == n) // Link Label
            {
                FormLabel fl = new FormLabel(F, odgv);
                fl.AddtvLabelClassFromDB();
                fl.AddlDestinationFromProp();

                fl.ShowDialog(F);
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
            if (nbrNCard > 0) WidthNCard = (Rectangle.Width - WidthMaxIcon + WidthShiftIcon - radius - Axe) / nbrNCard;

            for (int i = LstChild.Count - 1; i >= 0; i--)
            {
                if (LstChild[i].GetType() == typeof(DrawNCard))
                {
                    int y = YMin() + Axe;
                    int Hauteur = ((DrawNCard)LstChild[i]).Hauteur;
                    if (Hauteur > 0) y = YMax() - Axe - EpaisseurCard;
                    ((DrawNCard)LstChild[i]).Aligne(Rectangle.Left + WidthNCard * (nbrNCard - 1) + WidthMaxIcon - WidthShiftIcon + Axe, y, WidthNCard - Axe, EpaisseurCard);
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

                //SetServerLinks("GuidAppUser");
            }
        }

        public void SetServerLinks(string field)
        {
            List<string[]> lstLink = new List<string[]>();
            object o = GetValueFromName(field);
            string sObj = GetType().Name.Substring("Draw".Length);
            string sObjLink = field.Substring("Guid".Length);

            if (F.oCnxBase.CBRecherche("Select Guid" + sObj + ", GuidVue, " + field + " From " + sObjLink + "Link  Where GuidInsing='" + GuidkeyObjet + "' and GuidVue='" + F.GuidVue + "'"))
            {
                while (F.oCnxBase.Reader.Read())
                {
                    string[] sTabCur = new string[4];
                    sTabCur[0] = F.oCnxBase.Reader.GetString(0);
                    sTabCur[1] = F.oCnxBase.Reader.GetString(1);
                    sTabCur[2] = F.oCnxBase.Reader.GetString(2);
                    sTabCur[3] = " ";
                    lstLink.Add(sTabCur);
                }
            }
            F.oCnxBase.CBReaderClose();

            if (o != null)
            {
                string Link = (string)o;
                if (Link != "")
                {
                    string[] aLink = Link.Split(new Char[] { '(', ')' });
                    for (int i = 1; i < aLink.Length; i += 2)
                    {
                        string[] sTabCur;
                        sTabCur = lstLink.Find(elFind => elFind[2] == aLink[i].Trim());
                        if (sTabCur != null) sTabCur[3] = "x";
                        if (!F.oCnxBase.ExistServerLink(field, GuidkeyObjet.ToString(), F.GuidVue.ToString(), aLink[i].Trim()))
                            F.oCnxBase.CBWrite("INSERT INTO " + sObj + "Link (GuidServerPhy, GuidVue, " + field + ") VALUES ('" + GuidkeyObjet + "','" + F.GuidVue + "','" + aLink[i].Trim() + "')");
                    }
                    for (int i = 0; i < lstLink.Count; i++)
                    {
                        if (lstLink[i][3] != "x")
                            F.oCnxBase.CBWrite("DELETE FROM " + field.Substring("Guid".Length) + "Link WHERE GuidServerPhy='" + lstLink[i][0] + "' AND GuidVue ='" + lstLink[i][1] + "' AND " + field + "='" + lstLink[i][2] + "'");
                    }
                }
            }
        }


        public override void CWInsert(ControlDoc cw, char cTypeVue)
        {
            
        }
	}
}
