using System;
using System.Windows.Forms;
using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolTechUser : DrawTools.ToolRectangle
	{
        public ToolTechUser(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");

            //initialisation des proprietes
            oLayers = new LayerList[1];
            oLayers[0] = new LayerList("9450d0fc-e53d-44d3-9542-744641af908e");
            oLayers[0].AddTemplate(Owner.Owner, "", Owner.Owner.sGuidTemplate);
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawTechUser dtu;

            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;

                LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);


                int i = Owner.GraphicsList.FindObjet(0, (string)Owner.Owner.tvObjet.SelectedNode.Name);
                dtu = (DrawTechUser)Owner.GraphicsList[i];
                dtu.rectangle.X = e.X; dtu.rectangle.Y = e.Y;
                dtu.Normalize();
                //AddNewObject(drawArea, new DrawTechUser(drawArea.Owner, e.X, e.Y, 1, 1, (string)Owner.Owner.tvObjet.SelectedNode.Name, Owner.Owner.tvObjet.SelectedNode.Text, drawArea.GraphicsList.Count), true);
            }
            else AddNewObject(drawArea, new DrawTechUser(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count), true);            
        }

        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            base.OnMouseUp(drawArea, e);
            Owner.Owner.drawArea.GraphicsList.MoveSelectionToBack();

        }

        public override string GetTypeSimpleTable()
        {
            return  "AppUser" ;
        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawTechUser dtu;

            dtu = new DrawTechUser(Owner.Owner, LstValue, LstValueG);
            if (dtu.rectangle.X == 0 && dtu.rectangle.Y == 0)
            {
                DrawServerType dst = new DrawServerType(Owner.Owner, (TreeNode)null);

                dtu.AttachLink(dst, DrawObject.TypeAttach.Child);
                dst.AttachLink(dtu, DrawObject.TypeAttach.Parent);
                AddNewObject(Owner.Owner.drawArea, dst, false);
            }
            AddNewObject(Owner.Owner.drawArea, dtu, true);
            //base.CreatObjetFromBD(From1 f, LstValue, LstValueG);
        }

	}
}
