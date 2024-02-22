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
	
    public class DrawServerType: DrawTools.DrawRectangle
	{
		public DrawServerType()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawServerType(Form1 of, Dictionary<string, object> dic)
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

        public DrawServerType(Form1 of, TreeNode tnFonction)
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

            string[] aValue = F.tvObjet.SelectedNode.Text.Split('-');
            Texte = aValue[0].Trim();
            
            GuidkeyObjet = Guid.NewGuid();
            Guidkey = Guid.NewGuid();

            InitProp();
            if (tnFonction != null)
            {
                SetValueFromName("GuidFonction", tnFonction.Name);
                SetValueFromName("NomFonction", tnFonction.Text + "       (" + tnFonction.Name + ")");
            }
            Initialize();
        }

        public DrawServerType(Form1 of, ArrayList lstVal, ArrayList lstValG)
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
            return DrawArea.DrawToolType.ServerType;
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
            ToolServerType to = (ToolServerType)F.drawArea.tools[(int)DrawArea.DrawToolType.ServerType];

            Pen pen = new Pen(to.LineCouleur, to.LineWidth);
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            
            if (r.Width > 20 && r.Height > 10) {
                AffRec(g, r, to, 0);
                if (F.bPtt) DrawGrpTxt(g, 1, 0, r.Left + Axe, r.Top + r.Height / 2 - 3 * HeightFont[0] / 4, 0, to.Pen1Couleur, to.BkGrCouleur);
            } else g.DrawRectangle(pen, r);

            pen.Dispose();
        }

        public override void VisioDraw(ArrayList lstGuid, ArrayList lstShape, MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
            ToolServerType to = (ToolServerType)F.drawArea.tools[(int)DrawArea.DrawToolType.ServerType];

            if (lstGuid.IndexOf(GuidkeyObjet.ToString()) == -1)
            {
                if (LstParent != null)
                    for (int ip = 0; ip < LstParent.Count; ip++)
                        if (lstGuid.IndexOf(((DrawObject)LstParent[ip]).GuidkeyObjet.ToString()) == -1)
                            ((DrawObject)LstParent[ip]).VisioDraw(lstGuid, lstShape, page, yPage, qxPage, qyPage);

                //Dessiner l'objet
                MOI.Visio.Shape shape = page.DrawRectangle(Rectangle.Left * qxPage, yPage - Rectangle.Top * qyPage, Rectangle.Right * qxPage, yPage - Rectangle.Bottom * qyPage);

                //Couleur trait
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                //shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLinePattern).ResultIU = 0;

                //Couleur Fond
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillPattern).ResultIU = 0;

                lstShape.Add(shape);
                lstGuid.Add(GuidkeyObjet.ToString());
            }
        }


        public override void AttachLink(DrawObject o, TypeAttach Attach)
        {
            string oParent = "GuidServer";

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
            string s = GetTypeSimpleTable();

            if (LstParent != null)
            {
                for (int i = 0; i < LstParent.Count; i++) ((DrawObject)LstParent[i]).savetoXml(xmlDB, GObj);
            }

            XmlElement elo = XmlCreatObject(xmlDB); // Table Objet
            if (elo != null)
            {
                //ServerTypeLink
                for (int i = 0; i < LstParent.Count; i++)
                {
                    XmlElement el, elAtts;
                    DrawObject o = (DrawObject)LstParent[i];
                    string sType = o.GetType().Name.Substring("Draw".Length);
                    switch(sType)
                    {
                        case "TechUser":
                            sType = "AppUser";
                            el = xmlDB.XmlCreatEl(xmlDB.XmlGetFirstElFromParent(elo, "After"), sType + "TypeLink", "Guid" + sType + ",GuidServerType");
                            elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");
                            xmlDB.XmlSetAttFromEl(elAtts, "Guid" + sType, "s", o.GuidkeyObjet.ToString());
                            xmlDB.XmlSetAttFromEl(elAtts, "GuidServerType", "s", (string)LstValue[0]);
                            break;
                        case "Server":
                            el = xmlDB.XmlCreatEl(xmlDB.XmlGetFirstElFromParent(elo, "After"), sType + "TypeLink", "Guid" + sType + ",GuidServerType");
                            elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");
                            xmlDB.XmlSetAttFromEl(elAtts, "Guid" + sType, "s", o.GuidkeyObjet.ToString());
                            xmlDB.XmlSetAttFromEl(elAtts, "GuidServerType", "s", (string)LstValue[0]);
                            break;
                        case "Gensas":
                            el = xmlDB.XmlCreatEl(xmlDB.XmlGetFirstElFromParent(elo, "After"), "svcserverTypeLink", "Guid" + sType + ",GuidServerType");
                            elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");
                            xmlDB.XmlSetAttFromEl(elAtts, "Guid" + sType, "s", o.GuidkeyObjet.ToString());
                            xmlDB.XmlSetAttFromEl(elAtts, "GuidServerType", "s", (string)LstValue[0]);
                            break;
                    }
                    
                    /*
                    el = xmlDB.XmlCreatEl(xmlDB.XmlGetFirstElFromParent(elo, "After"), sType + "TypeLink", "Guid" + sType + ",GuidServerType");
                    elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");
                    xmlDB.XmlSetAttFromEl(elAtts, "Guid" + sType, "s", o.GuidkeyObjet.ToString());
                    xmlDB.XmlSetAttFromEl(elAtts, "GuidServerType", "s", (string)LstValue[0]);
                    */
                }
                return elo;
            }
            return null;
        }
	}
}
