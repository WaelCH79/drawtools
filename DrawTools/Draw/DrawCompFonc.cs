using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data.Odbc;
using MOI = Microsoft.Office.Interop;
 
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawCompFonc : DrawTools.DrawRectangle
	{
        public DrawCompFonc()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawCompFonc(Form1 of, Dictionary<string, object> dic)
        {
            F = of;
            object o = null;
            OkMove = true;
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


        public DrawCompFonc(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            OkMove = false;
            Align = false;
            object o= null; 

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
            o = GetValueFromLib("Nom");
            if (o != null)
                Texte = (string)o;

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

        public override void initLinkParent()
        {
            SetValueFromName("GuidMainComposant", (object)"");
        }

        public override void initBaseLinkParent()
        {
            F.oCnxBase.CBWrite("UPDATE Module SET GuidMainComposant = null WHERE GuidModule='" + GuidkeyObjet + "'");
        }

        public void Aligne(int x, int y, int width, int height)
        {
            Rectangle = new Rectangle(x, y, width, height);
        }

        public override void Draw(Graphics g)
        {
            ToolCompFonc to = (ToolCompFonc)F.drawArea.tools[(int)DrawArea.DrawToolType.CompFonc];
            TemplateDt oTemplate = (TemplateDt)to.oLayers[0].lstTemplate[to.GetTemplate((string)GetValueFromName("GuidLayer"))];

            Pen pen = new Pen(oTemplate.LineCouleur, oTemplate.LineWidth);
            int iHeightFont;
            Rectangle r;

            r = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (r.Width > 20 && r.Height > 10) {
                //g.DrawRectangle(pen, r);
                //g.DrawRectangle(pen, r);
                AffRec(g, r, oTemplate, 0);
                iHeightFont = r.Height - 5;

                DrawGrpTxt(g, 1, 1, r.Left + Axe, r.Top + r.Height / 2 - 3 * HeightFont[0] / 4, 0, oTemplate.Pen1Couleur, oTemplate.BkGrCouleur);
            } else g.DrawRectangle(pen, r);

            pen.Dispose();
        }

        public override void VisioDraw(ArrayList lstGuid, ArrayList lstShape, MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
            ToolCompFonc to = (ToolCompFonc)F.drawArea.tools[(int)DrawArea.DrawToolType.CompFonc];

            if (lstGuid.IndexOf(GuidkeyObjet.ToString()) == -1)
            {
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

                //Inserer le texte + Couleur + taille
                shape.Text = Texte;
                shape.Characters.set_CharProps((short)MOI.Visio.VisCellIndices.visCharacterColor, (short)MOI.Visio.VisDefaultColors.visBlack);
                shape.Characters.set_CharProps((short)MOI.Visio.VisCellIndices.visCharacterSize, 8);
                //Couleur trait + epaisseur
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLinePattern).ResultIU = 0;
                //Couleur Fond
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillBkgnd).FormulaU = "RGB(" + to.Couleur.R.ToString() + "," + to.Couleur.G.ToString() + "," + to.Couleur.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillForegnd).FormulaU = "RGB(" + Color.White.R.ToString() + "," + Color.White.G.ToString() + "," + Color.White.B.ToString() + ")";
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillPattern).ResultIU = 28;

                lstShape.Add(shape);
                lstGuid.Add(GuidkeyObjet.ToString());
            }
        }

        public override string GetTypeSimpleTable()
        {
            //return base.GetTypeSimpleTable();
            return "Module";
        }

        /// <summary>
        /// Save Object to the Data Base
        /// </summary>
        public override void savetoDB()
        {
            if (!savetoDBFait())
            {
                if (LstParent.Count > 0) ((DrawObject)LstParent[0]).savetoDB();

                //Update Table Module
                F.oCnxBase.UpdateObject(this);

                if (!F.oCnxBase.ExistGuidG(this))
                {
                    F.oCnxBase.CreatGObjectRect(this); //Table GCompFonc
                    F.oCnxBase.CreatDansVue(Guidkey, "G" + this.GetType().Name.Substring("Draw".Length));  // Table DansVue
                }
                savetoDBOK();
            }
        }

        public override void AttachLink(DrawObject o, TypeAttach Attach)
        {
            string oParent = "GuidMainComposant";

            switch (Attach)
            {
                case TypeAttach.Parent:
                    SetValueFromName(oParent, o.GuidkeyObjet.ToString());
                    break;
            }
            base.AttachLink(o, Attach);
        }

        public override string GetsType(bool Reel)
        {
            if(Reel) return base.GetsType(Reel);
            return "Module";
        }
	}
}
