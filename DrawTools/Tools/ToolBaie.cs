using System;
using System.Windows.Forms;
using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolBaie : DrawTools.ToolRectangle
	{
        public ToolBaie(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            InitPropriete("5d616de3-5009-4708-ab0a-49b79a9c9fa7");
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawBaie db;

            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);
                db = (DrawBaie)Owner.GraphicsList[0];
                db.rectangle.X = e.X; db.rectangle.Y = e.Y;
                db.Normalize();
            }
            else
            {
                db = new DrawBaie(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);
                AddNewObject(drawArea, db, true);
            }
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawBaie db;

            db = new DrawBaie(Owner.Owner, LstValue, LstValueG);
            AddNewObject(Owner.Owner.drawArea, db, false);
            //base.CreatObjetFromBD(From1 f, LstValue, LstValueG);
        }

	}

    
}
