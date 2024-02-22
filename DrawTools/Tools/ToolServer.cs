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
	public class ToolServer : DrawTools.ToolRectangle
	{
		public ToolServer(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            InitPropriete("d4446b03-48b4-4be5-8b50-b213dcae0590");
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                if (Owner.Owner.tvObjet.SelectedNode.Parent.Name == "FonctionServer")
                {

                    //LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);
                    DrawServer ds = new DrawServer(drawArea.Owner, e.X, e.Y, 1, 1, (string)Owner.Owner.tvObjet.SelectedNode.Name, Owner.Owner.tvObjet.SelectedNode.Text, drawArea.GraphicsList.Count);
                    //int i = Owner.GraphicsList.FindObjet(0, (string)Owner.Owner.tvObjet.SelectedNode.Name);
                    //DrawServer ds = (DrawServer)Owner.GraphicsList[i];

                    DrawServerType dst = new DrawServerType(Owner.Owner, Owner.Owner.tvObjet.SelectedNode);

                    ds.AttachLink(dst, DrawObject.TypeAttach.Child);
                    dst.AttachLink(ds, DrawObject.TypeAttach.Parent);
                    AddNewObject(Owner.Owner.drawArea, dst, false);
                    AddNewObject(Owner.Owner.drawArea, ds, true);

                    ds.rectangle.X = e.X; ds.rectangle.Y = e.Y;
                    ds.Normalize();
                }
                else
                {
                    bool bObjetTraite = false;
                    for (int i = 0; i < drawArea.GraphicsList.Count; i++)
                    {
                        if (drawArea.GraphicsList[i].GetType() == typeof(DrawApplication) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                        {
                            DrawApplication da = (DrawApplication)drawArea.GraphicsList[i];
                            if (da.GetPlateformPattern && da.NbrServerType() == 0)
                            {
                                bObjetTraite = true;
                                da.LoadServerType((string)Owner.Owner.tvObjet.SelectedNode.Name);
                                drawArea.GraphicsList.UnselectAll();
                                da.Selected = true;
                                if (drawArea.GraphicsList.MoveSelectionToBack()) { drawArea.MajObjets(); }
                                da.AligneObjet();
                            }
                            break;
                        }
                        if (drawArea.GraphicsList[i].GetType() == typeof(DrawServer) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                        {
                            bObjetTraite = true;
                            DrawServer ds = (DrawServer)drawArea.GraphicsList[i];
                            //ds.LoadServerType_Techno((string)Owner.Owner.tvObjet.SelectedNode.Name);
                            ds.LoadServerType((string)Owner.Owner.tvObjet.SelectedNode.Name);
                            drawArea.GraphicsList.UnselectAll();
                            ds.Selected = true;
                            if (drawArea.GraphicsList.MoveSelectionToBack()) { drawArea.MajObjets(); }
                            ds.AligneObjet();
                            break;
                        }
                        if (drawArea.GraphicsList[i].GetType() == typeof(DrawTechUser) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                        {
                            bObjetTraite = true;
                            DrawTechUser dtu = (DrawTechUser)drawArea.GraphicsList[i];
                            dtu.LoadUserType_Techno((string)Owner.Owner.tvObjet.SelectedNode.Name);
                            drawArea.GraphicsList.UnselectAll();
                            dtu.Selected = true;
                            if (drawArea.GraphicsList.MoveSelectionToBack()) { drawArea.MajObjets(); }
                            dtu.AligneObjet();
                            break;
                        }
                        if (drawArea.GraphicsList[i].GetType() == typeof(DrawGensas) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                        {
                            bObjetTraite = true;
                            DrawGensas dgs = (DrawGensas)drawArea.GraphicsList[i];
                            dgs.LoadServerType((string)Owner.Owner.tvObjet.SelectedNode.Name);
                            drawArea.GraphicsList.UnselectAll();
                            dgs.Selected = true;
                            if (drawArea.GraphicsList.MoveSelectionToBack()) { drawArea.MajObjets(); }
                            dgs.AligneObjet();
                            break;
                        }
                    }
                    if (!bObjetTraite)
                    {
                        DrawServer ds = new DrawServer(drawArea.Owner, e.X, e.Y, 1, 1, (string)Owner.Owner.tvObjet.SelectedNode.Parent.Name, Owner.Owner.tvObjet.SelectedNode.Parent.Text, drawArea.GraphicsList.Count);
                        //ds.LoadServerType_Techno((string)Owner.Owner.tvObjet.SelectedNode.Name);
                        ds.LoadServerType((string)Owner.Owner.tvObjet.SelectedNode.Name);
                        AddNewObject(Owner.Owner.drawArea, ds, true);
                        ds.rectangle.X = e.X; ds.rectangle.Y = e.Y;
                        ds.Normalize();
                    }
                }
            }
            //else AddNewObject(drawArea, new DrawServer(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count), true);            
        }
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            Owner.Owner.drawArea.GraphicsList.MoveSelectionToBack();
            base.OnMouseUp(drawArea, e);
        }

        public override string GetFrom(string sType, string sGType)
        {
            return "From DansVue, " + sType + " Left Join LayerLink On Guid" + sType + "=GuidObj and layerlink.GuidAppVersion='" + Owner.Owner.GetGuidAppVersion()  + "', " + sGType + ", Fonction";
            //return "From DansVue, " + sType + ", " + sGType + ", ServerTypeLink, ServerType, Fonction";
            //return base.GetFrom(sType);
        }

        public override string GetWhere(string sType, string sGType, string GuidGVue, string sGuidVueSrvPhy)
        {
            return "WHERE GuidGVue ='" + GuidGVue + "' and GuidObjet=Guid" + sGType + " and " + sGType + ".Guid" + sType + "=" + sType + ".Guid" + sType + " AND " + sType + ".GuidMainFonction=Fonction.GuidFonction" + Owner.Owner.wkApp.GetWhereLayer();
            //return "WHERE GuidVue ='" + GuidVue + "' and GuidObjet=Guid" + sGType + " and " + sGType + ".Guid" + sType + "=" + sType + ".Guid" + sType + " AND " + sType + ".Guid" + sType + "=ServerTypeLink.Guid" + sType + " AND ServerTypeLink.GuidServerType=ServerType.GuidServerType AND ServerType.GuidFonction=Fonction.GuidFonction AND Fonction.GuidFonction NOT LIKE 'b66cbab1-aa49-4bf5-9eb5-71c4242030d0'";
            //return base.GetWhere(sType);
        }

        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawServer ds;

            ds = new DrawServer(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = ds;
            else AddNewObject(Owner.Owner.drawArea, ds, false);
            return ds;
        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawServer ds;
            bool selected = false;

            ds = new DrawServer( Owner.Owner, LstValue, LstValueG);
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

	}
}
