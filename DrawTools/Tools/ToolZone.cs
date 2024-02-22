using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;


namespace DrawTools
{
	/// <summary>
	/// Polygon tool
	/// </summary>
	public class ToolZone : DrawTools.ToolObject
	{
		public ToolZone(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "Link.cur");
        }

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            // Create new polygon, add it to the list
            // and keep reference to it

            Point point = new Point(e.X, e.Y);
            DrawObject oAttach=null;
            DrawZone dz;

            
            for (int i = 0; i < drawArea.GraphicsList.Count; i++)
            {
                if (drawArea.GraphicsList[i].AttachPointInObject(point))
                {
                    point = drawArea.GraphicsList[i].GetPointObject(point);
                    oAttach = drawArea.GraphicsList[i];
                    break;
                }
            }
            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);

                dz = (DrawZone)Owner.GraphicsList[0];
                dz.Add3Points(point);
                //dl..rectangle.X = e.X; du.rectangle.Y = e.Y;
                dz.Normalize();
            } else AddNewObject(drawArea, new DrawZone(drawArea.Owner, point.X, point.Y, point.X, point.Y, drawArea.GraphicsList.Count), true);
            if (oAttach != null)
            {
                dz = (DrawZone)drawArea.GraphicsList[0];
                oAttach.AttachLink(drawArea.GraphicsList[0], DrawObject.TypeAttach.Sortie);
                dz.AttachLink(oAttach, DrawObject.TypeAttach .Entree);
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
            drawArea.GraphicsList[0].InitDatagrid(Owner.Owner.dataGrid);
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }

        public override void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
        {
            drawArea.Cursor = Cursor;

            if (e.Button == MouseButtons.Left)
            {
                Point point = new Point(e.X, e.Y);
                for (int i = 0; i < drawArea.GraphicsList.Count; i++)
                {
                    if (drawArea.GraphicsList[i].AttachPointInObject(point))
                    {
                        point = drawArea.GraphicsList[i].GetPointObject(point);
                        //drawArea.GraphicsList[i].AttachLink(drawArea.GraphicsList[0]);
                        //drawArea.GraphicsList[0].MoveHandleTo(point, 1);
                        break;
                    }
                }
                drawArea.GraphicsList.GetSelectedObject(0).MoveHandleTo(point, 5);
                drawArea.Refresh();
            }
        }

        public override string GetFrom(string sType, string sGType)
        {
            return "From DansVue, " + sType + ", " + sGType + ", GPoint";
            //return base.GetFrom(sType);
        }

        public override string GetWhere(string sType, string sGType, string GuidGVue, string sGuidVueSrvPhy)
        {
            return "WHERE GuidGVue ='" + GuidGVue + "' and GuidObjet=Guid" + sGType + " and " + sGType + ".Guid" + sType + "=" + sType + ".Guid" + sType + " AND " + sGType + ".Guid" + sGType + "=GPoint.GuidGObjet ORDER BY Guid" + sGType + ", I";
            //return base.GetWhere(sType);
        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawZone dz;
            int n;
            bool selected = false;

            n = Owner.GraphicsList.FindObjet(0, (string)LstValue[0]);
            if (n > -1)
            {
                dz = (DrawZone)Owner.GraphicsList[n];
                dz.AddPoint(LstValueG);
            }
            else
            {
                dz = new DrawZone(Owner.Owner, LstValue, LstValueG);
                if (dz.pointArray.Count == 0) { dz.Add3Points(new Point(0, 0)); selected = true; }
                AddNewObject(Owner.Owner.drawArea, dz, selected);

                CreatObjetLink(dz, "GuidServerPhy", DrawObject.TypeAttach.Entree, DrawObject.TypeAttach.Sortie);
                CreatObjetLink(dz, "GuidLun", DrawObject.TypeAttach.Sortie, DrawObject.TypeAttach.Entree);
            }
        }
    }
}
