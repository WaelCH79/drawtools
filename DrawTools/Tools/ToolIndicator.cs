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
    public class ToolIndicator : DrawTools.ToolRectangle
	{
        public ToolIndicator(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
		}

        public bool CreateObjetFromMouse(DrawArea drawArea, int i, string sGuid, Point e)
        {
            DrawIndicator di;
            bool Create = false;

            LoadSimpleObject(sGuid);
            di = (DrawIndicator)Owner.GraphicsList[0];
            di.rectangle.X = e.X; di.rectangle.Y = e.Y;
            di.Normalize();
            Create = true;

            return Create;
        }
        /*
        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            DrawIndicator di;

            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                if (drawArea.GraphicsList.Count == 0)
                {
                    LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);
                    di = (DrawIndicator)Owner.GraphicsList[0];
                    di.rectangle.X = e.X; di.rectangle.Y = e.Y;
                    di.Normalize();
                }
                else
                {
                    for (int i = 0; i < drawArea.GraphicsList.Count; i++)
                    {
                        if (CreateObjetFromMouse(drawArea, i, (string)Owner.Owner.tvObjet.SelectedNode.Name, pt)) break;
                    }
                }
            }
            else
            {
                di = new DrawIndicator(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);

                AddNewObject(drawArea, di, false);

            }
        }*/

        /// <summary>
        /// Left mouse is released.
        /// New object is created and resized.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            DrawIndicator di = (DrawIndicator)drawArea.GraphicsList[0];

            di.Normalize();
                        
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }

        public override void CreatObjetFromCadreRef(ArrayList aIndicator)
        {
            DrawIndicator di;
            ArrayList aArray = new ArrayList();
            for (int i = 0; i < aIndicator.Count; i++) aArray.Add(aIndicator[i]);
            di = new DrawIndicator(Owner.Owner, 0, 0, 1, 1, Owner.Owner.drawArea.GraphicsList.Count);

            AddNewObject(Owner.Owner.drawArea, di, false);

            di.SetValueFromName("Indicator", aArray);
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawIndicator di;

            di = new DrawIndicator(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(di);
        }

        public override string GetSimpleFrom(string sTable)
        {
            return "FROM Indicator";
        }
    }

    
}
