using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
    public class ToolMCompApp : DrawTools.ToolRectangle
	{
        public ToolMCompApp(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawMCompApp dmca;
            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                for (int i = 0; i < drawArea.GraphicsList.Count; i++)
                {
                    if (drawArea.GraphicsList[i].GetType() == typeof(DrawServer) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                    {
                        DrawServer ds = (DrawServer)drawArea.GraphicsList[i];

                        dmca = new DrawMCompApp(drawArea.Owner);

                        ds.AttachLink(dmca, DrawObject.TypeAttach.Child);
                        dmca.AttachLink(ds, DrawObject.TypeAttach.Parent);
                        ds.AligneObjet();
                        dmca.Normalize();
                        AddNewObject(Owner.Owner.drawArea, dmca, true);

                        /*
                        LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);
                        dmca = (DrawMCompApp)Owner.GraphicsList[0];
                                                                    
                        ds.AttachLink(dmca, DrawObject.TypeAttach.Child);
                        dmca.AttachLink(ds, DrawObject.TypeAttach.Parent);
                        ds.AligneObjet();
                        dmca.Normalize();*/
                                                
                        break;
                    }
                    if (drawArea.GraphicsList[i].GetType() == typeof(DrawTechUser) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                    {
                        DrawTechUser dtu = (DrawTechUser)drawArea.GraphicsList[i];
                        dmca = new DrawMCompApp(drawArea.Owner);

                        dtu.AttachLink(dmca, DrawObject.TypeAttach.Child);
                        dmca.AttachLink(dtu, DrawObject.TypeAttach.Parent);
                        dtu.AligneObjet();
                        dmca.Normalize();
                        AddNewObject(Owner.Owner.drawArea, dmca, true);

                        break;
                    }
                }
            }
            
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }
                       
        /// <summary>
        /// Left mouse is released.
        /// New object is created and resized.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            //drawArea.Owner.SetStateOfControls();
        }

        public override string GetFrom(string sType, string sGType)
        {
            return "From DansVue, " + sType + ", " + sGType + ", MainComposant";
        }

        public override string GetWhere(string sType, string sGType, Guid GuidVue, string sGuidVueSrvPhy)
        {
            return "WHERE GuidVue ='" + GuidVue + "' and GuidObjet=Guid" + sGType + " and " + sGType + ".Guid" + sType + "=" + sType + ".Guid" + sType + " AND " + sType + ".GuidMainComposant=MainComposant.GuidMainComposant";
        }


        public override string GetSimpleFrom(string sTable)
        {
            return "FROM " + sTable + ", MainComposant";
        }

        public override string GetSimpleWhere(string sTable, string GuidObjet)
        {
            return "WHERE Guid" + sTable + "='" + GuidObjet + "' AND " + sTable + ".GuidMainComposant=MainComposant.GuidMainComposant";
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG)
        {
            DrawMCompApp dmca;
            bool selected = false;

            dmca = new DrawMCompApp(Owner.Owner, LstValue, LstValueG);
            if (dmca.rectangle.X == 0) selected = true;
            AddNewObject(Owner.Owner.drawArea, dmca, selected);

            CreatObjetLink(dmca, "GuidServer", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
        }
	}
}
