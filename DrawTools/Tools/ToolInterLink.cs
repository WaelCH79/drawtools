using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;


namespace DrawTools
{
	/// <summary>
	/// Polygon tool
	/// </summary>
	public class ToolInterLink : DrawTools.ToolObject
	{
		public ToolInterLink(DrawArea da)
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
            AddNewObject(drawArea, new DrawInterLink(drawArea.Owner, point.X, point.Y, point.X + 1, point.Y + 1, drawArea.GraphicsList.Count), true);
            if (oAttach != null)
            {
                DrawInterLink dl = (DrawInterLink) drawArea.GraphicsList[0];
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
            DrawInterLink dil = (DrawInterLink)drawArea.GraphicsList[0];
            Point p0 = (Point)dil.pointArray[0];
            Point p1 = (Point)dil.pointArray[1];
            if (p0.X == p1.X && p0.Y == p1.Y)
            {
                drawArea.GraphicsList.Remove(0);
            }
            else
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
            }
            //dil.InitDatagrid(Owner.Owner.dataGrid);
            drawArea.GraphicsList.UnselectAll();
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }

        public override void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
        {
            DrawInterLink o;
            drawArea.Cursor = Cursor;

            if (e.Button == MouseButtons.Left)
            {
                Point point = new Point(e.X, e.Y);
                o = (DrawInterLink)drawArea.GraphicsList.GetSelectedObject(0);
                o.MoveHandleTo(point, (o.pointArray.Count - 1) * 4 + 1);
                drawArea.Refresh();
            }
        }

        public override string GetFrom(string sType, string sGType)
        {
            return "From DansVue, " + sType + " Left Join LayerLink On Guid" + sType + "=GuidObj and layerlink.GuidAppVersion='" + Owner.Owner.GetGuidAppVersion()  + "', " + sGType + ", GPoint, GroupService";
            //return base.GetFrom(sType);
        }

        public override string GetWhere(string sType, string sGType, string GuidGVue, string sGuidVueSrvPhy)
        {
            return "WHERE GuidGVue ='" + GuidGVue + "' and GuidObjet=Guid" + sGType + " and " + sGType + ".Guid" + sType + "=" + sType + ".Guid" + sType + " AND " + sType + ".GuidGroupService=GroupService.GuidGroupService AND " + sGType + ".Guid" + sGType + "=GPoint.GuidGObjet ORDER BY Id DESC, Guid" + sGType + ", I";
            //return base.GetWhere(sType);
        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawInterLink dl;
            int n;

            n = Owner.GraphicsList.FindObjet(0, (string)LstValue[0]);
            if (n > -1)
            {
                dl = (DrawInterLink)Owner.GraphicsList[n];
                dl.AddPoint(LstValueG);
            }
            else
            {
                dl = new DrawInterLink(Owner.Owner, LstValue, LstValueG);
                Owner.GraphicsList.Add(dl);
                              
                CreatObjetLink(dl, "GuidServerSiteIn", DrawObject.TypeAttach.Entree, DrawObject.TypeAttach.Sortie);
                CreatObjetLink(dl, "GuidServerSiteOut", DrawObject.TypeAttach.Sortie, DrawObject.TypeAttach.Entree);
            }
        }
    }
}
