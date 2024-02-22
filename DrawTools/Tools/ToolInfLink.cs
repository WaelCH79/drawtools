using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;


namespace DrawTools
{
	/// <summary>
	/// Polygon tool
	/// </summary>
	public class ToolInfLink : DrawTools.ToolObject
	{
		public ToolInfLink(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "Link.cur");
            InitPropriete("025a3538-f699-41c2-885e-37a67ea0d8ea");
        }

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            // Create new polygon, add it to the list
            // and keep reference to it

            Point point = new Point(e.X, e.Y);
            DrawObject oAttach=null;
            
            for (int i = 0; i < drawArea.GraphicsList.Count; i++)
            {
                if (drawArea.GraphicsList[i].AttachPointInObject(point))
                {
                    point = drawArea.GraphicsList[i].GetPointObject(point);
                    oAttach = drawArea.GraphicsList[i];
                    break;
                }
            }
            AddNewObject(drawArea, new DrawInfLink(drawArea.Owner, point.X, point.Y, point.X + 1, point.Y + 1, drawArea.GraphicsList.Count), true);
            if (oAttach != null)
            {
                DrawInfLink dl = (DrawInfLink) drawArea.GraphicsList[0];
                oAttach.AttachLink(drawArea.GraphicsList[0], DrawObject.TypeAttach.Sortie);
                dl.AttachLink(oAttach, DrawObject.TypeAttach .Entree);
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

            Point point = new Point(e.X, e.Y);
            for (int i = 1; i < drawArea.GraphicsList.Count; i++)
            {
                if (drawArea.GraphicsList[i].AttachPointInObject(point))
                {
                    point = drawArea.GraphicsList[i].GetPointObject(point);
                    drawArea.GraphicsList[i].AttachLink(drawArea.GraphicsList[0], DrawObject.TypeAttach.Entree);
                    drawArea.GraphicsList[0].AttachLink(drawArea.GraphicsList[i], DrawObject.TypeAttach.Sortie);
                    break;
                }
            }
            drawArea.GraphicsList[0].Normalize();
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }

        public override void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
        {
            DrawInfLink o;
            drawArea.Cursor = Cursor;

            if (e.Button == MouseButtons.Left)
            {
                Point point = new Point(e.X, e.Y);
                o = (DrawInfLink)drawArea.GraphicsList.GetSelectedObject(0);
                o.MoveHandleTo(point, (o.pointArray.Count - 1) * 4 + 1);
                drawArea.Refresh();
            }
        }
        

        public override string GetsType(bool Reel)
        {
            if (Reel) return base.GetsType(Reel);
            return "TechLink";
        }

        public override string GetTypeSimpleTable()
        {
            return "TechLink";
        }

        public override string GetTypeSimpleGTable()
        {
            return "GTechLink";
        }

        public override string GetFrom(string sType, string sGType)
        {
            return "From DansVue, " + sType + " Left Join LayerLink On " + sType + ".Guid" + sType + "=GuidObj" + " and layerlink.GuidAppVersion='" + Owner.Owner.GetGuidAppVersion() + "', " + sGType + ", GPoint, GroupService";
            //return base.GetFrom(sType);
        }

        public override string GetWhere(string sType, string sGType, string GuidGVue, string sGuidVueSrvPhy)
        {
            return "WHERE GuidGVue ='" + GuidGVue + "' and GuidObjet=Guid" + sGType + " and " + sGType + ".Guid" + sType + "=" + sType + ".Guid" + sType + " AND " + sType + ".GuidGroupService=GroupService.GuidGroupService AND " + sGType + ".Guid" + sGType + "=GPoint.GuidGObjet" + Owner.Owner.wkApp.GetWhereLayer() + " ORDER BY Id DESC, Guid" + sGType + ", I";
            //return base.GetWhere(sType);
        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            bool selected = false;
            DrawInfLink dl;
            int n;

            n = Owner.GraphicsList.FindObjet(0, (string)LstValue[0]);
            if (n > -1)
            {
                dl = (DrawInfLink)Owner.GraphicsList[n];
                dl.AddPoint(LstValueG);
            }
            else
            {
                string sTypeVue = Owner.Owner.tbTypeVue.Text; // (string)Owner.Owner.cbTypeVue.SelectedItem;
                dl = new DrawInfLink(Owner.Owner, LstValue, LstValueG);
                string sIn = "GuidServerIn", sOut = "GuidServerOut";

                int i = CreatObjetLink(dl, sIn, DrawObject.TypeAttach.Entree, DrawObject.TypeAttach.Sortie);
                int j = CreatObjetLink(dl, sOut, DrawObject.TypeAttach.Sortie, DrawObject.TypeAttach.Entree);
                if (i > -1 && j > -1) AddNewObject(Owner.Owner.drawArea, dl, selected);
                else {
                    RemoveLink(dl, sIn, DrawObject.TypeAttach.Entree, DrawObject.TypeAttach.Sortie);
                    RemoveLink(dl, sOut, DrawObject.TypeAttach.Sortie, DrawObject.TypeAttach.Entree);
                }
            }
        }
    }
}
