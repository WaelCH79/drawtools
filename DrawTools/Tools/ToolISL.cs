using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolISL : DrawTools.ToolRectangle
	{
        public ToolISL(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            InitPropriete("8cb88143-9b10-4733-ba98-5b818c2a4d13");
		}


        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawISL di;
            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);

                int i = Owner.GraphicsList.FindObjet(0, (string)Owner.Owner.tvObjet.SelectedNode.Name);
                di = (DrawISL)Owner.GraphicsList[i];
                di.rectangle.X = e.X; di.rectangle.Y = e.Y;

                di.Normalize();
            }
            else
            {
                di = new DrawISL(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);
                for (int i = 0; i < 4; i++) 
                {
                    DrawSanCard dsc = new DrawSanCard(drawArea.Owner, new Point(e.X,e.Y), di);
                    AddNewObject(drawArea, dsc, false);
                    di.AttachLink(dsc, DrawObject.TypeAttach.Child);
                    dsc.AttachLink(di, DrawObject.TypeAttach.Parent);
                    dsc.InitProp("GuidHote", (object)((DrawISL)dsc.LstParent[0]).GuidkeyObjet.ToString(), true);
                }
                AddNewObject(drawArea, di, true);
            }
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawISL di;
            bool selected = false;

            di = new DrawISL(Owner.Owner, LstValue, LstValueG);
            if (di.rectangle.X == 0) selected = true;
            AddNewObject(Owner.Owner.drawArea, di, selected);
        }
	}
}
