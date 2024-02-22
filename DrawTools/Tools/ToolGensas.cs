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
	public class ToolGensas : DrawTools.ToolRectangle
	{
		public ToolGensas(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            oLayers = new LayerList[1];
            oLayers[0] = new LayerList("04b03335-09a2-4f5b-b223-74b1cca31cc8");
            oLayers[0].AddTemplate(Owner.Owner, "", Owner.Owner.sGuidTemplate);
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawGensas dgs;
            DrawManagedsvc dms;

            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                dgs = new DrawGensas(drawArea.Owner, e.X, e.Y, 1, 1, (string)Owner.Owner.tvObjet.SelectedNode.Name, Owner.Owner.tvObjet.SelectedNode.Text, drawArea.GraphicsList.Count);
                if (Owner.Owner.tvObjet.SelectedNode.Name == "6ManagedService")
                {
                    dms = new DrawManagedsvc(Owner.Owner, drawArea.GraphicsList.Count);

                    dgs.AttachLink(dms, DrawObject.TypeAttach.Child);
                    dms.AttachLink(dgs, DrawObject.TypeAttach.Parent);
                    AddNewObject(Owner.Owner.drawArea, dms, false);
                }
                else
                {
                    dgs.LoadManagedsvc_Techno((string)Owner.Owner.tvObjet.SelectedNode.Name);
                }
                AddNewObject(Owner.Owner.drawArea, dgs, true);
                dgs.rectangle.X = e.X; dgs.rectangle.Y = e.Y;
                dgs.Normalize();
            }
            
        }

        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            Owner.Owner.drawArea.GraphicsList.MoveSelectionToBack();
            base.OnMouseUp(drawArea, e);
        }

       
        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawGensas dgs;

            dgs = new DrawGensas(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dgs;
            else AddNewObject(Owner.Owner.drawArea, dgs, false);
            return dgs;
        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawGensas ds;
            bool selected = false;

            ds = new DrawGensas( Owner.Owner, LstValue, LstValueG);
            if (ds.rectangle.X == 0)
            {
                selected = true;

                //Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.ServerType].LoadSimpleObject((string)lstNCard[i]);
                //int j = Owner.GraphicsList.FindObjet(0, (string)lstNCard[i]);

                
                //ds.AligneObjet();
                //dst.Normalize();
                //AddNewObject(Owner.Owner.drawArea, dst, true);
            } else AddNewObject(Owner.Owner.drawArea, ds, selected);
            //base.CreatObjetFromBD(From1 f, LstValue, LstValueG);
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
