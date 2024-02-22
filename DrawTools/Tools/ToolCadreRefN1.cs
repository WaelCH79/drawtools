using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolCadreRefN1 : DrawTools.ToolRectangle
	{
        public ToolCadreRefN1(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
		}

        public bool CreateObjetFromMouse(DrawArea drawArea, int i, string sGuid, Point e)
        {
            DrawCadreRefN1 dcrn1;
            bool Create = false;

            LoadSimpleObject(sGuid);
            dcrn1 = (DrawCadreRefN1)Owner.GraphicsList[0];
            dcrn1.rectangle.X = e.X; dcrn1.rectangle.Y = e.Y;
            dcrn1.Normalize();
            Create = true;

            return Create;
        }
        /*
        public override void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
        {
            drawArea.Cursor = Cursor;
            DrawCadreRefN1 dcrn;

            if (e.Button == MouseButtons.Left)
            {
                int n = Owner.Owner.drawArea.GraphicsList.FindObjet(0, (string)Owner.Owner.tvObjet.SelectedNode.Name);
                dcrn = (DrawCadreRefN1)Owner.GraphicsList[n];
                
                Point point = new Point(e.X, e.Y);
                dcrn.MoveHandleTo(point, 5);
                drawArea.Refresh();
            }
        }*/
        
        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            DrawCadreRefN1 dcrn1;

            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                if (drawArea.GraphicsList.Count == 0)
                {
                    LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);
                    dcrn1 = (DrawCadreRefN1)Owner.GraphicsList[0];
                    dcrn1.rectangle.X = e.X; dcrn1.rectangle.Y = e.Y;
                    dcrn1.Normalize();
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
                dcrn1 = new DrawCadreRefN1(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);

                AddNewObject(drawArea, dcrn1, true);

            }
        }

        public override string GetTypeSimpleTable()
        {
            return "CadreRef";
        }

        /// <summary>
        /// Left mouse is released.
        /// New object is created and resized.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            DrawCadreRefN1 dcrn1 = (DrawCadreRefN1)drawArea.GraphicsList[0];

            dcrn1.Normalize();
            
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawCadreRefN1 dcrn1;
            bool selected = false;

            dcrn1 = new DrawCadreRefN1(Owner.Owner, LstValue, LstValueG);

            AddNewObject(Owner.Owner.drawArea, dcrn1, selected);
        }

	}

    
}
