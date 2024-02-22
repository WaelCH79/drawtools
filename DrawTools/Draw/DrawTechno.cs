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
	public class DrawTechno : DrawTools.DrawRectangle
	{
		public DrawTechno()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawTechno(Form1 of)
        {
            F = of;
            OkMove = false;
            Align = false;
            Rectangle = new Rectangle(1, 1, 1, 1);
            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = new ArrayList();

            int iIndexSeparator = F.tvObjet.SelectedNode.Text.LastIndexOf('-');
            Texte = F.tvObjet.SelectedNode.Text.Substring(0, iIndexSeparator).Trim();
            
            GuidkeyObjet = Guid.NewGuid();
            Guidkey = Guid.NewGuid();

            InitProp();
            SetValueFromName("NomTechnoRef", Texte);
            SetValueFromName("IndexImgOS", (object)F.tvObjet.SelectedNode.Text.Substring(iIndexSeparator + 1).Trim());
            SetValueFromName("GuidTechnoRef", (string)F.tvObjet.SelectedNode.Name);
            Initialize();
        }

        public DrawTechno(Form1 of, Dictionary<string, object> dic)
        {
            F = of;
            object o = null;
            OkMove = false;
            Align = false;

            LstParent = new ArrayList();
            LstChild = null;
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

        public DrawTechno(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = false;
            Align = false;
            InitRectangle(lstValG);
            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = lstVal;
            Guidkey = Guid.NewGuid();

            o = GetValueFromLib("Guid");
            if(o!=null)
                GuidkeyObjet = new Guid((string) o);
            o = GetValueFromName("GuidTechnoRef");
            if (o != null)
            {
                TreeNode[] ArrayTreeNode = F.tvObjet.Nodes.Find((string)o, true);
                if (ArrayTreeNode.Length == 1)
                {
                    int iIndexSeparator = ArrayTreeNode[0].Text.LastIndexOf('-');
                    Texte = ArrayTreeNode[0].Text.Substring(0, iIndexSeparator).Trim();
                    SetValueFromName("NomTechnoRef", Texte);
                    SetValueFromName("IndexImgOS", (object)ArrayTreeNode[0].Text.Substring(iIndexSeparator+1).Trim());
                }
            }
            Initialize();
        }

        public override bool ParentPointInObject(Point point)
        {
            return false;
        }

        public override bool AttachPointInObject(Point point)
        {
            return false;
        }

        public void Aligne(int x, int y, int width, int height)
        {
            Rectangle = new Rectangle(x, y, width, height);
        }

        public override DrawArea.DrawToolType GetToolTypeForObjExp()
        {
            return DrawArea.DrawToolType.Techno;
        }

        public override Guid GetGuidForObjExp()
        {
            object o = GetValueFromName("GuidTechnoRef");
            if (o != null) return new Guid((string)o);
            return GuidkeyObjet;
        }


        public override void Draw(Graphics g)
        {
            if (!F.bPtt)
            {
                ToolTechno to = (ToolTechno)F.drawArea.tools[(int)DrawArea.DrawToolType.Techno];
                Pen pen = new Pen(to.LineCouleur, to.LineWidth);
                Rectangle r;

                r = DrawRectangle.GetNormalizedRectangle(Rectangle);

                if (r.Width > 20 && r.Height > 10)
                {
                    AffRec(g, r, to, 0);
                    DrawGrpTxt(g, 1, 1, r.Left + Axe, r.Top + r.Height / 2 - 3 * HeightFont[0] / 4, 0, to.Pen1Couleur, to.BkGrCouleur);
                }
                else g.DrawRectangle(pen, r);

                pen.Dispose();
            }
        }

        public override void VisioDraw(ArrayList lstGuid, ArrayList lstShape, MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
            if (lstGuid.IndexOf(GuidkeyObjet.ToString()) == -1)
            {
                ToolTechno to = (ToolTechno)F.drawArea.tools[(int)DrawArea.DrawToolType.Techno];

                if (LstParent != null)
                    for (int ip = 0; ip < LstParent.Count; ip++)
                        if (lstGuid.IndexOf(((DrawObject)LstParent[ip]).GuidkeyObjet.ToString()) == -1)
                            ((DrawObject)LstParent[ip]).VisioDraw(lstGuid, lstShape, page, yPage, qxPage, qyPage);

                //Ombre
                int epOmbre = 2;
                MOI.Visio.Shape shapeOm = page.DrawRectangle((Rectangle.Left + epOmbre) * qxPage, yPage - (Rectangle.Top + epOmbre) * qyPage, (Rectangle.Right + epOmbre) * qxPage, yPage - (Rectangle.Bottom + epOmbre) * qyPage);
                shapeOm.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillForegnd).FormulaU = "RGB(" + Color.Black.R.ToString() + "," + Color.Black.G.ToString() + "," + Color.Black.B.ToString() + ")";
                //shapeOm.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineRounding).FormulaU = "3 mm";

                //Dessiner l'objet
                MOI.Visio.Shape shape = page.DrawRectangle(Rectangle.Left * qxPage, yPage - Rectangle.Top * qyPage, Rectangle.Right * qxPage, yPage - Rectangle.Bottom * qyPage);

                /*MOI.Visio.Selection s = page.CreateSelection(Microsoft.Office.Interop.Visio.VisSelectionTypes.visSelTypeSingle, Microsoft.Office.Interop.Visio.VisSelectMode.visSelModeOnlySuper, shapeOm);
                s.Select(shapeOm, 0);
                s.Select(shape, 0);
                s.ConvertToGroup();*/

                //Inserer le texte + Couleur + taille
                shape.Text = Texte;
                shape.Characters.set_CharProps((short)MOI.Visio.VisCellIndices.visCharacterColor, (short)MOI.Visio.VisDefaultColors.visBlack);
                shape.Characters.set_CharProps((short)MOI.Visio.VisCellIndices.visCharacterSize, 7);
                //Couleur trait
                //shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + Couleur.R.ToString() + "," + Couleur.G.ToString() + "," + Couleur.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLinePattern).ResultIU = 0;

                //Couleur Fond
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillBkgnd).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillForegnd).FormulaU = "RGB(" + Color.White.R.ToString() + "," + Color.White.G.ToString() + "," + Color.White.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillPattern).ResultIU = 32;

                lstShape.Add(shape);
                lstGuid.Add(GuidkeyObjet.ToString());
            }
        }


        public override void dataGrid_CellClick(DataGridView odgv, DataGridViewCellEventArgs e)
        {
            int n;

            n = GetIndexFromName("NomTechnoRef");
            if (n > -1 && e.RowIndex == n) // Link Applicatif
            {
                string GuidTechnoRef = (string)GetValueFromName("GuidTechnoRef");
                FormChangeProp fcp = new FormChangeProp(F, odgv);

                fcp.AddlSourceFromDB("Select GuidTechnoRef, NomTechnoRef From TechnoRef Where GuidProduit in (Select GuidProduit from TechnoRef Where GuidTechnoRef='" + GuidTechnoRef + "')", "Create");
                fcp.ShowDialog(F);
                if (fcp.Valider)
                {
                    string[] aValue = F.oCnxBase.CmdText.Split('(', ')');
                    F.drawArea.GraphicsList.MajValueObjects((string) LstValue[0], aValue[0].Trim(), aValue[1].Trim());
                }
            }
        }

        public override void AttachLink(DrawObject o, TypeAttach Attach)
        {
            string oParent = "GuidTechnoHost";

            switch (Attach)
            {
                case TypeAttach.Parent:
                    SetValueFromName(oParent, (string) o.LstValue[0]);
                    break;
            }
            base.AttachLink(o, Attach);
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

            return XmlCreatObject(xmlDB); // Table Objet
        }

        public override int GetGrpAff()
        {
            return 1;
        }

        public override string GetKeyComment()
        {
            string sGuid = "0000-00000-00000-0000-000";
            if (F.oCnxBase.CBRecherche("Select GuidProduit From TechnoRef Where GuidTechnoRef='" + GetValueFromName("GuidTechnoRef") + "'"))
                sGuid = F.oCnxBase.Reader.GetString(0);
            F.oCnxBase.CBReaderClose();
            return sGuid;
            //return base.GetKeyComment();
        }

        public override void CWInsertChild(ControlDoc cw, string sBook)
        {
            object o = GetValueFromName("GuidTechnoRef");
            if (o != null)
            {
                DateTime dt = DateTime.Now;
                if (F.oCnxBase.CBRecherche("Select GuidTechnoRef, NomTechnoRef, Version, ValIndicator, " + (int)Form1.ImgList.fail + ", Norme From TechnoRef, IndicatorLink, Indicator Where GuidTechnoRef=GuidObjet and IndicatorLink.GuidIndicator=Indicator.GuidIndicator and NomIndicator='1-Fin Support' and GuidTechnoRef='" + (string)o + "'"))
                {
                    F.oCnxBase.Reader.Read();
                    cw.InsertRowFromReaderId(sBook, F.oCnxBase.Reader, "TabTechnoRef");
                }
                F.oCnxBase.CBReaderClose();
            }
        }

        /*
        public override void CWInsert(ControlDoc cw, char cTypeVue)
        {
            if (cTypeVue == '2' || cTypeVue == 'I')
            {
                string sType = GetType().Name.Substring("Draw".Length);
                string sGuidP = typeof(DrawServer).ToString().Substring(14, 3) + cTypeVue + ((DrawObject)((DrawObject)LstParent[0]).LstParent[0]).GuidkeyObjet.ToString().Replace("-", "");
                string sGuid = cTypeVue + GuidkeyObjet.ToString().Replace("-", "");
                //sType ne doit pas depasse 4 caracteres
                string sBook = sType.Substring(0, 3) + sGuid;
                if (cw.Exist("n" + sGuid) > -1)
                {
                    //cw.InsertTexBookmark("n" + sType, true, Texte + "\n", "Titre 3");
                }
                else if (cw.Exist(sGuidP) > -1)
                {
                    object o = GetValueFromName("GuidTechnoRef");
                    if (o != null)
                    {
                        DateTime dt = DateTime.Now;
                        if (F.oCnxBase.CBRecherche("Select GuidTechnoRef, NomTechnoRef, Version, ValIndicator, " + (int)Form1.ImgList.fail + ", Norme From TechnoRef, IndicatorLink, Indicator Where GuidTechnoRef=GuidObjet and IndicatorLink.GuidIndicator=Indicator.GuidIndicator and NomIndicator='1-Fin Support' and GuidTechnoRef='" + (string)o + "'"))
                        {
                            F.oCnxBase.Reader.Read();
                            cw.InsertRowFromReaderId(sGuidP, F.oCnxBase.Reader, "TabTechnoRef");
                        }
                        F.oCnxBase.CBReaderClose();
                    }

                    //cw.CreatBookmark("n" + sGuid, sType.Substring(0, 3) + sGuid, 2);
                }
            }
        }
        */
	}
}
