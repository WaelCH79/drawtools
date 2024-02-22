using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolSanSwitch : DrawTools.ToolRectangle
	{
        public ToolSanSwitch(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            InitPropriete("d2193a0c-f996-4056-b1d2-f4803ee359c4");
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawSanSwitch dss;

            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);

                dss = (DrawSanSwitch)Owner.GraphicsList[0];
                dss.rectangle.X = e.X; dss.rectangle.Y = e.Y;
                dss.Normalize();
            }
            else
            {
                dss = new DrawSanSwitch(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);
                AddNewObject(drawArea, dss, true);
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
            //Point point = new Point(e.X, e.Y);
            DrawSanSwitch dss = (DrawSanSwitch)drawArea.GraphicsList[0];
            dss.Normalize();
            for (int i = 1; i < drawArea.GraphicsList.Count; i++)
            {
                if (drawArea.GraphicsList[i].ParentPointInObject(new Point(dss.Rectangle.Left, dss.Rectangle.Top)) &
                    drawArea.GraphicsList[i].ParentPointInObject(new Point(dss.Rectangle.Right, dss.Rectangle.Bottom)))
                {
                    //point = drawArea.GraphicsList[i].GetPointObject(point);
                    drawArea.GraphicsList[i].AttachLink(drawArea.GraphicsList[0], DrawObject.TypeAttach.Child);
                    drawArea.GraphicsList[0].AttachLink(drawArea.GraphicsList[i], DrawObject.TypeAttach.Parent);
                    break;
                }
            }
            dss.InitDatagrid(Owner.Owner.dataGrid);
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawSanSwitch dss;
            bool selected = false;

            dss = new DrawSanSwitch(Owner.Owner, LstValue, LstValueG);
            if (dss.rectangle.X == 0) selected = true;
            AddNewObject(Owner.Owner.drawArea, dss, selected);
        }
	}
}
