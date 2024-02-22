using System;
using System.Windows.Forms;
using System.Drawing;


namespace DrawTools
{
	/// <summary>
	/// Rectangle tool
	/// </summary>
	public class ToolRectangle : DrawTools.ToolObject
	{
        public static int Marge = 25;

        public ToolRectangle()
        {
        }

		public ToolRectangle(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "Rectangle.cur");
		}
        
        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            AddNewObject(drawArea, new DrawRectangle(e.X, e.Y, 1, 1), true);
        }

        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {

            //Point point = new Point(e.X, e.Y);
            string sGuid= null;
            DrawRectangle dr = null;
            if (drawArea.OSelected != null)
            {
                sGuid = drawArea.OSelected.GuidkeyObjet.ToString();
                dr = (DrawRectangle)drawArea.GraphicsList[drawArea.GraphicsList.FindObjet(0, sGuid)];
            }
            else dr = (DrawRectangle)drawArea.OSelected;
            if (dr.LstParent != null)
            {
                dr.Normalize();
                for (int i = 0; i < drawArea.GraphicsList.Count; i++)
                {
                    if (dr != drawArea.GraphicsList[i])
                    {
                        //if (drawArea.GraphicsList[i].GetType() != typeof(DrawTechLink) && drawArea.GraphicsList[i].GetType() != typeof(DrawLink))

                        if (drawArea.GraphicsList[i].ParentPointInObject(new Point(dr.Rectangle.Left, dr.Rectangle.Top)) &
                            drawArea.GraphicsList[i].ParentPointInObject(new Point(dr.Rectangle.Right, dr.Rectangle.Bottom)))
                        {
                            //point = drawArea.GraphicsList[i].GetPointObject(point);
                            drawArea.GraphicsList[i].AttachLink(dr, DrawObject.TypeAttach.Child);
                            dr.AttachLink(drawArea.GraphicsList[i], DrawObject.TypeAttach.Parent);
                            break;
                        }
                    }
                }
            }
            //dr.InitDatagrid(Owner.Owner.dataGrid);
            drawArea.GraphicsList.UnselectAll();
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }

        public override void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
        {
            drawArea.Cursor = Cursor;

            if ( e.Button == MouseButtons.Left )
            {
                Point point = new Point(e.X, e.Y);
                //drawArea.GraphicsList[0].MoveHandleTo(point, 5);
                drawArea.GraphicsList.GetSelectedObject(0).MoveHandleTo(point, 5);
                drawArea.Refresh();
            }
        }

        public override string GetFrom(string sType, string sGType)
        {
            /*int n = Owner.Owner.oCnxBase.ConfDB.FindFieldFromLib(sType, "HyperLien");
            if (n > -1) 
                return "From DansVue, " + sType + ", " + sGType + ", Comment"; 
            else */
            return "From DansVue, " + sType + " Left Join LayerLink On " + sType + ".Guid" + sType + "=GuidObj" + " and layerlink.GuidAppVersion='" + Owner.Owner.GetGuidAppVersion()  + "', " + sGType; 
            //return base.GetFrom(sType);
        }

        public override string GetWhere(string sType, string sGType, string GuidGVue, string sGuidVueSrvPhy)
        {
            /*int n = Owner.Owner.oCnxBase.ConfDB.FindFieldFromLib(sType, "HyperLien");
            if (n > -1)
                return "WHERE GuidVue ='" + GuidVue + "' and GuidObjet=Guid" + sGType + " and " + sGType + ".Guid" + sType + "=" + sType + ".Guid" + sType + " AND " + sType + ".GuidComment=Comment.GuidComment";
            else*/
            return "WHERE GuidGVue ='" + GuidGVue + "' and GuidObjet=Guid" + sGType + " and " + sGType + ".Guid" + sType + "=" + sType + ".Guid" + sType + Owner.Owner.wkApp.GetWhereLayer() + " ORDER BY Nom" + sType + " DESC";
            //return base.GetWhere(sType);
        }
	}
}
