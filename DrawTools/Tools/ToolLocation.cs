using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolLocation : DrawTools.ToolRectangle
	{
		public ToolLocation(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            InitPropriete("f4647c7c-e30c-4a5e-927a-75f8cdd18f50");
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            DrawLocation dl;

            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);
                dl = (DrawLocation)Owner.GraphicsList[0];
                dl.rectangle.X = e.X; dl.rectangle.Y = e.Y;
                dl.Normalize();
            }
            else
            {
                AddNewObject(drawArea, new DrawLocation(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count), true);
            }
        }

        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            DrawLocation dl = (DrawLocation)drawArea.GraphicsList[0];

            dl.Normalize();

            ArrayList lstServerPhy = Owner.Owner.oCnxBase.CreatServerLocation(dl);

            for (int i = 0; i < lstServerPhy.Count; i++)
            {
                Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.ServerSite].LoadSimpleObject((string)lstServerPhy[i]);
                int j = Owner.GraphicsList.FindObjet(0, (string)lstServerPhy[i]);
                DrawServerSite dss = (DrawServerSite)Owner.GraphicsList[j];
                dl.AttachLink(dss, DrawObject.TypeAttach.Child);
                dss.AttachLink(dl, DrawObject.TypeAttach.Parent);

                dss.InitRectangle(i, lstServerPhy.Count, dl.rectangle.Left, dl.rectangle.Top);
            }
            dl.InitDatagrid(Owner.Owner.dataGrid);
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawLocation dl;

            dl = new DrawLocation(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(dl);
        }
       
        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawLocation dl;
            bool selected = false;

            dl = new DrawLocation(Owner.Owner, LstValue, LstValueG);
            if (dl.rectangle.X == 0)
            {
                /*selected = true;
                ArrayList lstCnx = Owner.Owner.oCnxBase.CreatCnxLocation(dl);

                for (int i = 0; i < lstCnx.Count; i++)
                {
                    int j = Owner.GraphicsList.FindObjet((string)lstCnx[i]);
                    if (j > -1)
                    {
                        DrawCnx dc = (DrawCnx)Owner.GraphicsList[j];
                        dl.AttachLink(dc, DrawObject.TypeAttach.Entree);
                        dc.AttachLink(dl, DrawObject.TypeAttach.Sortie);
                    }
                }*/
            }
            AddNewObject(Owner.Owner.drawArea, dl, selected);
            //base.CreatObjetFromBD(From1 f, LstValue, LstValueG);
        }

	}

    
}
