using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
    /// 
	public class ToolCadreRefEnd : DrawTools.ToolRectangle
	{
        public ToolCadreRefEnd(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
		}

        /*
        public bool CreateObjetFromMouse(DrawArea drawArea, int i, string sGuid, Point e)
        {
            DrawCadreRefEnd dcre;
            bool Create = false;

            if (drawArea.GraphicsList[i].GetType() == typeof(DrawCadreRefN1) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
            {
                DrawCadreRefN1 dcrn1 = (DrawCadreRefN1)drawArea.GraphicsList[i];
                LoadSimpleObject(sGuid);
                dcrn = (DrawCadreRefN)Owner.GraphicsList[0];

                dcrn1.AttachLink(dcrn, DrawObject.TypeAttach.Child);
                dcrn.AttachLink(dcrn1, DrawObject.TypeAttach.Parent);
                dcrn.rectangle.X = e.X; dcrn.rectangle.Y = e.Y;
                dcrn.Normalize();
                Create = true;
            }
            else
            {
                LoadSimpleObject(sGuid);
                dcrn = (DrawCadreRefN)Owner.GraphicsList[0];
                dcrn.rectangle.X = e.X; dcrn.rectangle.Y = e.Y;
                dcrn.Normalize();
                Create = true;
            }
            return Create;
        }*/

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            DrawCadreRefEnd dcre;

            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);
                int n = Owner.Owner.drawArea.GraphicsList.FindObjet(0, (string)Owner.Owner.tvObjet.SelectedNode.Name);
                while (((DrawObject)Owner.GraphicsList[n]).GetType() == typeof(DrawCadreRefN1))
                    n = Owner.Owner.drawArea.GraphicsList.FindObjet(n + 1, (string)Owner.Owner.tvObjet.SelectedNode.Name);
                dcre = (DrawCadreRefEnd)Owner.GraphicsList[n];
                dcre.rectangle.X = e.X; dcre.rectangle.Y = e.Y;
                dcre.Normalize();
            }
        }
        
        public override void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
        {
            drawArea.Cursor = Cursor;
            DrawCadreRefEnd dcre;

            if (e.Button == MouseButtons.Left)
            {
                int n = Owner.Owner.drawArea.GraphicsList.FindObjet(0, (string)Owner.Owner.tvObjet.SelectedNode.Name);
                while (((DrawObject)Owner.GraphicsList[n]).GetType() == typeof(DrawCadreRefN1))
                    n = Owner.Owner.drawArea.GraphicsList.FindObjet(n + 1, (string)Owner.Owner.tvObjet.SelectedNode.Name);
                dcre = (DrawCadreRefEnd)Owner.GraphicsList[n];
                
                Point point = new Point(e.X, e.Y);
                dcre.MoveHandleTo(point, 5);
                drawArea.Refresh();
            }
        }

        public override string GetTypeSimpleTable()
        {
            return "CadreRef";
        }

        public void CreateProduit(DrawCadreRefEnd dcre)
        {
            ArrayList aProduit = dcre.FindProduit();
                        
            for (int i = 0; i < aProduit.Count; i++)
            {
                CadreRefN1 cr = (CadreRefN1)aProduit[i];
                Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.Produit].LoadSimpleObject(cr.GuidCadreRefN1);
                int j = Owner.GraphicsList.FindObjet(0, cr.GuidCadreRefN1);

                DrawProduit dp = (DrawProduit)Owner.GraphicsList[j];
                dcre.AttachLink(dp, DrawObject.TypeAttach.Child);
                dp.AttachLink(dcre, DrawObject.TypeAttach.Parent);
                dp.CreateIndicator(cr);
            }
        }

        /// <summary>
        /// Left mouse is released.
        /// New object is created and resized.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            DrawCadreRefEnd dcre; // = (DrawCadreRefN)drawArea.GraphicsList[0];

            int n = Owner.Owner.drawArea.GraphicsList.FindObjet(0, (string)Owner.Owner.tvObjet.SelectedNode.Name);
            while (((DrawObject)Owner.GraphicsList[n]).GetType() == typeof(DrawCadreRefN1))
                n = Owner.Owner.drawArea.GraphicsList.FindObjet(n + 1, (string)Owner.Owner.tvObjet.SelectedNode.Name);
            dcre = (DrawCadreRefEnd)Owner.GraphicsList[n];
            dcre.Normalize();
            //CreateCadreRefN1_Indicator(dcrn);
                       
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }

        public override string GetWhere(string sType, string sGType, string GuidGVue, string sGuidVueSrvPhy)
        {
            return "WHERE GuidGVue ='" + GuidGVue + "' and GuidObjet=Guid" + sGType + " and " + sGType + ".Guid" + sType + "=" + sType + ".Guid" + sType;
        }

        public override void LoadObject(char TypeData, string sGuidgvue, string sData)
        {
            string Select, From, Where;
            string sType = GetTypeSimpleTable();
            string sGType = GetTypeSimpleGTable();
            //string sGType = GetTypeSimpleTable();

            CnxBase ocnx = Owner.Owner.oCnxBase;
            ArrayList aGuidCadreREf = new ArrayList();

            Select = "SELECT CadreRef.GuidCadreRef ";
            From = GetFrom(sType, sGType);
            Where = GetWhere(sType, sGType, sGuidgvue, sData);
            if (ocnx.CBRecherche(Select + " " + From + " " + Where))
            {
                while (ocnx.Reader.Read()) aGuidCadreREf.Add(ocnx.Reader.GetString(0));
                ocnx.CBReaderClose();
            }
            else ocnx.CBReaderClose();

            Select = GetSelect(sType, sGType);
            for (int i = 0; i < aGuidCadreREf.Count; i++)
            {
                ocnx.CBRecherche(Select + " " + From + " " + Where + " AND CadreRef.GuidCadreRef='" + aGuidCadreREf[i] + "'");
                CreatObjetsFromBD(true, ConfDataBase.FieldOption.Select);
                ocnx.CBReaderClose();

                DrawCadreRefEnd dcre; // = (DrawCadreRefN)drawArea.GraphicsList[0];
                int n = Owner.Owner.drawArea.GraphicsList.FindObjet(0, (string)aGuidCadreREf[i]);
                if ((Owner.GraphicsList[n]).GetType() != typeof(DrawCadreRefEnd)) n = Owner.Owner.drawArea.GraphicsList.FindObjet(++n, (string)aGuidCadreREf[i]);
                dcre = (DrawCadreRefEnd)Owner.GraphicsList[n];
                dcre.AligneObjet();
            }            
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawCadreRefEnd dcre;
            bool selected = false;

            dcre = new DrawCadreRefEnd(Owner.Owner, LstValue, LstValueG);
            if (dcre.rectangle.X == 0) selected = true;
            CreateProduit(dcre);
            AddNewObject(Owner.Owner.drawArea, dcre, selected);
            Owner.Owner.drawArea.GraphicsList.MoveLastToBack();


            //base.CreatObjetFromBD(From1 f, LstValue, LstValueG);
        }
	}
}
