using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolLun : DrawTools.ToolRectangle
	{
		public ToolLun(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            InitPropriete("69243233-a3f6-4a77-9614-f40ebb1877fd");
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawLun dl;

            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);
                dl = (DrawLun)Owner.GraphicsList[0];
                dl.rectangle.X = e.X; dl.rectangle.Y = e.Y;
                dl.Normalize();
            }
            else
            {
                dl = new DrawLun(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);
                AddNewObject(drawArea, dl, true);
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
            DrawLun dl = (DrawLun)drawArea.GraphicsList[0];
            dl.Normalize();
            for (int i = 1; i < drawArea.GraphicsList.Count; i++)
            {
                if (drawArea.GraphicsList[i].ParentPointInObject(new Point(dl.Rectangle.Left, dl.Rectangle.Top)) &
                    drawArea.GraphicsList[i].ParentPointInObject(new Point(dl.Rectangle.Right, dl.Rectangle.Bottom)))
                {
                    //point = drawArea.GraphicsList[i].GetPointObject(point);
                    drawArea.GraphicsList[i].AttachLink(drawArea.GraphicsList[0], DrawObject.TypeAttach.Child);
                    drawArea.GraphicsList[0].AttachLink(drawArea.GraphicsList[i], DrawObject.TypeAttach.Parent);
                    break;
                }
            }
            dl.InitDatagrid(Owner.Owner.dataGrid);
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawLun dl;

            dl = new DrawLun(Owner.Owner, LstValue, LstValueG);
            AddNewObject(Owner.Owner.drawArea, dl, false);

            CreatObjetLink(dl, "GuidBaie", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            /*n = CreatObjetLink(dl, "GuidZone", DrawObject.TypeAttach.Entree, DrawObject.TypeAttach.Sortie);
            if (n > -1)
            {
                dz = (DrawZone)Owner.GraphicsList[n];
                n = dz.GetAttchHandle(dl.Rectangle);
                dz.AttachHandle.Add(n + 10); // +10, car il y a 9 points prient par le rectangle du VLAn
            }*/
        }


	}
}
