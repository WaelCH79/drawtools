using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolCnx : DrawTools.ToolRectangle
	{
        public ToolCnx(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            InitPropriete("17514de1-1919-4b7a-b99f-426acd916b78");
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawCnx dc;

            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);

                dc = (DrawCnx)Owner.GraphicsList[0];
                dc.rectangle.X = e.X; dc.rectangle.Y = e.Y;
                dc.Normalize();
            }
            else
            {
                dc = new DrawCnx(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);
                AddNewObject(drawArea, dc, true);
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
            DrawCnx dc = (DrawCnx)drawArea.GraphicsList[0];
            dc.Normalize();
            for (int i = 1; i < drawArea.GraphicsList.Count; i++)
            {
                if (drawArea.GraphicsList[i].ParentPointInObject(new Point(dc.Rectangle.Left, dc.Rectangle.Top)) &
                    drawArea.GraphicsList[i].ParentPointInObject(new Point(dc.Rectangle.Right, dc.Rectangle.Bottom)))
                {
                    //point = drawArea.GraphicsList[i].GetPointObject(point);
                    drawArea.GraphicsList[i].AttachLink(drawArea.GraphicsList[0], DrawObject.TypeAttach.Child);
                    drawArea.GraphicsList[0].AttachLink(drawArea.GraphicsList[i], DrawObject.TypeAttach.Parent);
                    break;
                }
            }

            dc.InitDatagrid(Owner.Owner.dataGrid);
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawCnx dc;
            bool selected = false;

            dc = new DrawCnx(Owner.Owner, LstValue, LstValueG);
            if (dc.rectangle.X == 0) selected = true;
            AddNewObject(Owner.Owner.drawArea, dc, selected);
        }
	}
}
