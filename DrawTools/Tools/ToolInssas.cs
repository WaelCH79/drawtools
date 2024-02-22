using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Xml;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolInssas : DrawTools.ToolRectangle
	{
		public ToolInssas(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            oLayers = new LayerList[1];
            oLayers[0] = new LayerList("04b03335-09a2-4f5b-b223-74b1cca31cc8");
            oLayers[0].AddTemplate(Owner.Owner, "", Owner.Owner.sGuidTemplate);
		}

        public override string GetSimpleSelect(Table t)
        {
            return t.GetSelectField(ConfDataBase.FieldOption.InterneBD);
        }

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawInssas dis;

            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                int idx = Owner.Owner.drawArea.GraphicsList.FindObjet(0, (string)Owner.Owner.tvObjet.SelectedNode.Name);
                if (idx == -1)
                {
                    LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);
                    dis = (DrawInssas)Owner.GraphicsList[Owner.Owner.drawArea.GraphicsList.FindObjet(0, (string)Owner.Owner.tvObjet.SelectedNode.Name)];
                    if(dis == null)
                    {
                        // ajout d'un inssas via gensas
                        dis = new DrawInssas(drawArea.Owner, e.X, e.Y, 1, 1, (string)Owner.Owner.tvObjet.SelectedNode.Name, Owner.Owner.tvObjet.SelectedNode.Text, drawArea.GraphicsList.Count);
                        AddNewObject(Owner.Owner.drawArea, dis, true);
                    }

                    dis.rectangle.X = e.X; dis.rectangle.Y = e.Y;
                    dis.Normalize();
                }
                else
                {
                    Owner.Owner.drawArea.GraphicsList.UnselectAll();
                    Owner.Owner.drawArea.GraphicsList[idx].Selected = true;
                }
            }


            /*
                dis = new DrawInssas(drawArea.Owner, e.X, e.Y, 1, 1, (string)Owner.Owner.tvObjet.SelectedNode.Name, Owner.Owner.tvObjet.SelectedNode.Text, drawArea.GraphicsList.Count);
                
                //dis.SetValueFromName("GuidExtention", "04b03335-09a2-4f5b-b223-74b1cca31cc8");
                AddNewObject(Owner.Owner.drawArea, dis, true);
                dis.rectangle.X = e.X; dis.rectangle.Y = e.Y;
                dis.Normalize();
            */
            
        }

        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            base.OnMouseUp(drawArea, e);
            Owner.Owner.drawArea.GraphicsList.MoveSelectionToBack();
            
        }

       
        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawInssas dis;

            dis = new DrawInssas(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dis;
            else AddNewObject(Owner.Owner.drawArea, dis, false);
            return dis;
        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawInssas dis;
            bool selected = false;

            dis = new DrawInssas( Owner.Owner, LstValue, LstValueG);
            if (dis.rectangle.X == 0)selected = true;
            AddNewObject(Owner.Owner.drawArea, dis, selected);
        }

        public override string GetSelect(string sTable, string sGTable)
        {
            return base.GetSelect(sTable, sGTable) + ", Managedsvc.GuidManagedsvc, NomManagedsvc ";
        }

        public override string GetFrom(string sType, string sGType)
        {
            return "From DansVue, " + sType + " Left Join LayerLink On Guid" + sType + "=GuidObj and layerlink.GuidAppVersion='" + Owner.Owner.GetGuidAppVersion() + "', " + sGType + ", ManagedsvcLink, Managedsvc";
        }

        public override string GetWhere(string sType, string sGType, string GuidGVue, string sGuidVueSrvPhy)
        {
            return "WHERE GuidGVue ='" + GuidGVue + "' and GuidObjet=Guid" + sGType + " and " + sGType + ".Guid" + sType + "=" + sType + ".Guid" + sType + " AND " + sType + ".GuidGensas=ManagedsvcLink.GuidGensas" + " AND ManagedsvcLink.GuidManagedsvc=Managedsvc.GuidManagedsvc" + Owner.Owner.wkApp.GetWhereLayer();
        }


        public override string GetSimpleFrom(string sTable)
        {
            return "FROM " + sTable + ", ManagedsvcLink, Managedsvc";
        }

        public override string GetSimpleWhere(string sTable, string GuidObjet)
        {
            return "WHERE Guid" + sTable + "='" + GuidObjet + "' AND " + sTable + ".GuidGensas=ManagedsvcLink.GuidGensas" + " AND ManagedsvcLink.GuidManagedsvc=Managedsvc.GuidManagedsvc";
        }

    }
}
