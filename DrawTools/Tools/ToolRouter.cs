using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolRouter : DrawTools.ToolRectangle
	{
        public ToolRouter(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            InitPropriete("017f4f0a-1f0a-4cb2-9453-6f38fcbe403a");
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawRouter dr;
            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);

                dr = (DrawRouter)Owner.GraphicsList[0];
                dr.rectangle.X = e.X; dr.rectangle.Y = e.Y;
                dr.Normalize();
            }
            else
            {
                dr = new DrawRouter(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);
                AddNewObject(drawArea, dr, true);
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
            DrawRouter dm = (DrawRouter) drawArea.GraphicsList[0];
            dm.Normalize();
            for (int i = 1; i < drawArea.GraphicsList.Count; i++)
            {
                if (drawArea.GraphicsList[i].ParentPointInObject(new Point(dm.Rectangle.Left, dm.Rectangle.Top)) &
                    drawArea.GraphicsList[i].ParentPointInObject(new Point(dm.Rectangle.Right, dm.Rectangle.Bottom)))
                {
                    //point = drawArea.GraphicsList[i].GetPointObject(point);
                    drawArea.GraphicsList[i].AttachLink(drawArea.GraphicsList[0], DrawObject.TypeAttach.Child);
                    drawArea.GraphicsList[0].AttachLink(drawArea.GraphicsList[i], DrawObject.TypeAttach.Parent);
                    break;
                }
            }
            dm.InitDatagrid(Owner.Owner.dataGrid);
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawRouter dr;
            bool selected = false;

            dr = new DrawRouter(Owner.Owner, LstValue, LstValueG);
            if (dr.rectangle.X == 0) selected = true;
            AddNewObject(Owner.Owner.drawArea, dr, selected);
        }
	}
}
