using System;
using System.Windows.Forms;
using System.Drawing;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolModule : DrawTools.ToolRectangle
	{
		public ToolModule()
		{
            Cursor = new Cursor(GetType(), "module.cur");
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawModule dm;

            dm = new DrawModule(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);
            AddNewObject(drawArea, dm);
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
            DrawModule dm = (DrawModule) drawArea.GraphicsList[0];
            dm.Normalize();
            for (int i = 1; i < drawArea.GraphicsList.Count; i++)
            {
                if (drawArea.GraphicsList[i].PointInObject(new Point(dm.Rectangle.Left,dm.Rectangle.Top)) &
                    drawArea.GraphicsList[i].PointInObject(new Point(dm.Rectangle.Right, dm.Rectangle.Bottom)))
                {
                    //point = drawArea.GraphicsList[i].GetPointObject(point);
                    drawArea.GraphicsList[i].AttachLink(drawArea.GraphicsList[0], DrawObject.TypeAttach.Child);
                    drawArea.GraphicsList[0].AttachLink(drawArea.GraphicsList[i], DrawObject.TypeAttach.Parent);
                    break;
                }
            }

            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }


	}
}
