using System;
using System.Windows.Forms;

namespace DrawTools
{
	/// <summary>
	/// Ellipse tool
	/// </summary>
	public class ToolEllipse : DrawTools.ToolRectangle
	{
		public ToolEllipse(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "Ellipse.cur");
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            AddNewObject(drawArea, new DrawEllipse(e.X, e.Y, 1, 1),true);
        }

	}
}
