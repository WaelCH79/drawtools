using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data.Odbc;
using MOI = Microsoft.Office.Interop;
 
using System.Xml;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	
    public class DrawManagedsvc : DrawTools.DrawRectangle
	{
		public DrawManagedsvc()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawManagedsvc(Form1 of, Dictionary<string, object> dic)
        {
            F = of;
            object o = null;
            OkMove = false;
            Align = false;

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

        public DrawManagedsvc(Form1 of, int count)
        {
            F = of;
            OkMove = false;
            Align = false;
            Rectangle = new Rectangle(1, 1, 1, 1);
            LstParent = new ArrayList();
            LstChild = new ArrayList();
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = new ArrayList();
            GuidkeyObjet = Guid.NewGuid();
            Texte = "ManagedService" + count;

            Guidkey = Guid.NewGuid();
            InitProp();
            Initialize();
        }

        public DrawManagedsvc(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = false;
            Align = false;
            InitRectangle(lstValG);
            LstParent = new ArrayList();
            LstChild = new ArrayList();
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = lstVal;
            Guidkey = Guid.NewGuid();

            o = GetValueFromLib("Guid");
            if(o!=null) GuidkeyObjet = new Guid((string) o);
            o = GetValueFromLib("Nom");
            if (o != null)
                Texte = (string)o;

            Initialize();
        }

        public override bool AttachPointInObject(Point point)
        {
            return false;
        }

        public override DrawArea.DrawToolType GetToolTypeForObjExp()
        {
            return DrawArea.DrawToolType.Managedsvc;
        }

        public override Guid GetGuidForObjExp()
        {
            object o = GetValueFromLib("Guid");
            if (o != null) return new Guid((string)o);
            return GuidkeyObjet;
        }

        public void Aligne(int x, int y, int width, int height)
        {
            Rectangle = new Rectangle(x, y, width, height);
            AligneObjet();
        }

        private int NbrTechno()
        {
            int CountObj = 0;

            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType() == typeof(DrawTechno)) CountObj++;
            return CountObj;
        }

        
        public void AligneObjet()
        {
            int CountTechno = NbrTechno();
            int WidthObjet = Rectangle.Width,  HeightObjet = 15;

            if (CountTechno==0) rectangle.Height = HeightObjet;
                else rectangle.Height = CountTechno * (Axe + HeightTechno) + Axe;
            for (int i = LstChild.Count - 1; i >= 0; i--)
            {
                if (LstChild[i].GetType() == typeof(DrawTechno))
                {
                    ((DrawTechno)LstChild[i]).Aligne(Rectangle.X + 3 * Axe, Rectangle.Y + Axe + (CountTechno-1) * (HeightTechno + Axe), WidthObjet - 6 * Axe, HeightObjet);
                    CountTechno--;
                }
            }
        }

        public override void Draw(Graphics g)
        {
            ToolManagedsvc tms = (ToolManagedsvc)F.drawArea.tools[(int)DrawArea.DrawToolType.Managedsvc];
            TemplateDt oTemplate = (TemplateDt)tms.oLayers[0].lstTemplate[tms.GetTemplate((string)GetValueFromName("GuidLayer"))];

            Pen pen = new Pen(oTemplate.Couleur, oTemplate.LineWidth);
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10) {
                AffRec(g, r, oTemplate, 0);
                if (F.bPtt) DrawGrpTxt(g, 1, 0, r.Left + Axe, r.Top + r.Height / 2 - 3 * HeightFont[0] / 4, 0, oTemplate.Pen1Couleur, oTemplate.BkGrCouleur);
            } else g.DrawRectangle(pen, r);

            pen.Dispose();
        }

        public override void VisioDraw(ArrayList lstGuid, ArrayList lstShape, MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
            ToolManagedsvc tms = (ToolManagedsvc)F.drawArea.tools[(int)DrawArea.DrawToolType.Managedsvc];

            if (lstGuid.IndexOf(GuidkeyObjet.ToString()) == -1)
            {
                if (LstParent != null)
                    for (int ip = 0; ip < LstParent.Count; ip++)
                        if (lstGuid.IndexOf(((DrawObject)LstParent[ip]).GuidkeyObjet.ToString()) == -1)
                            ((DrawObject)LstParent[ip]).VisioDraw(lstGuid, lstShape, page, yPage, qxPage, qyPage);

                //Dessiner l'objet
                MOI.Visio.Shape shape = page.DrawRectangle(Rectangle.Left * qxPage, yPage - Rectangle.Top * qyPage, Rectangle.Right * qxPage, yPage - Rectangle.Bottom * qyPage);

                //Couleur trait
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + tms.Couleur.R.ToString() + "," + tms.Couleur.G.ToString() + "," + tms.Couleur.B.ToString() + ")";
                //shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLinePattern).ResultIU = 0;

                //Couleur Fond
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillPattern).ResultIU = 0;

                lstShape.Add(shape);
                lstGuid.Add(GuidkeyObjet.ToString());
            }
        }


        public override void AttachLink(DrawObject o, TypeAttach Attach)
        {
            string oParent = "GuidGensas";

            switch (Attach)
            {
                case TypeAttach.Parent:
                    SetValueFromName(oParent, o.GuidkeyObjet.ToString());
                    break;
            }
            base.AttachLink(o, Attach);
        }

        public override void CWInsertProp(ControlDoc cw, string sBook, string sPolicy)
        {
            for (int i = 0; i < LstChild.Count; i++)
            {
                if (LstChild[i].GetType() == typeof(DrawTechno))
                {
                    DrawTechno dt = (DrawTechno)LstChild[i];
                    dt.CWInsertProp(cw, sBook, sPolicy);
                }
            }
        }

        public override void savetoDB()
        {
            if (!savetoDBFait())
            {
                savetoDBOK();
            }
        }


        public override XmlElement savetoXml(XmlDB xmlDB, bool GObj)
        {
            //string s = GetTypeSimpleTable();

            if (LstParent != null)
            {
                for (int i = 0; i < LstParent.Count; i++) ((DrawObject)LstParent[i]).savetoXml(xmlDB, GObj);
            }

            XmlElement elo = XmlCreatObject(xmlDB); // Table Objet
            if (elo != null)
            {
                //ManagedsvcLink
                for (int i = 0; i < LstParent.Count; i++)
                {
                    DrawObject o = (DrawObject)LstParent[i];
                    string sType = o.GetType().Name.Substring("Draw".Length);
                    string sTypeObjLink = GetType().Name.Substring("Draw".Length);

                    XmlElement el = xmlDB.XmlCreatEl(xmlDB.XmlGetFirstElFromParent(elo, "After"), sTypeObjLink + "Link", "Guid" + sType + ",Guid" + sTypeObjLink);
                    XmlElement elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");
                    xmlDB.XmlSetAttFromEl(elAtts, "Guid" + sType, "s", o.GuidkeyObjet.ToString());
                    xmlDB.XmlSetAttFromEl(elAtts, "Guid" + sTypeObjLink, "s", (string)LstValue[0]);
                }
                return elo;
            }
            return null;
        }
	}
}
