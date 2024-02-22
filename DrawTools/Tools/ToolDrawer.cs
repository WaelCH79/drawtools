using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data.Odbc;


namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolDrawer : DrawTools.ToolRectangle
	{
        public ToolDrawer(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            InitPropriete("f542fa54-ca98-4c21-801f-400ef8059a85");
		}


        public bool CreateObjetFromMouse(DrawArea drawArea, int i, string sGuid, Point e)
        {
            DrawDrawer dd;
            DrawBaiePhy dc = (DrawBaiePhy)drawArea.GraphicsList[i];

            LoadSimpleObject(sGuid);
            dd = (DrawDrawer)Owner.GraphicsList[0];
            dd.rectangle.X = dc.rectangle.X;
            dd.rectangle.Y = dc.rectangle.Y + dc.rectangle.Height;
            dc.AttachLink(dd, DrawObject.TypeAttach.Child);
            dd.AttachLink(dc, DrawObject.TypeAttach.Parent);
            dd.Normalize();

            return true;
        }

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point pt= new Point(e.X, e.Y);
            DrawDrawer dd;

            for (int i = 0; i < drawArea.GraphicsList.Count; i++)
            {
                if (drawArea.GraphicsList[i].GetType() == typeof(DrawBaiePhy) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                {
                    if (drawArea.AddObjet)
                    {
                        CreateObjetFromMouse(drawArea, i, (string)Owner.Owner.tvObjet.SelectedNode.Name, pt);                                                                       
                        drawArea.AddObjet = false;
                    }
                    else
                    {
                        DrawBaiePhy dc = (DrawBaiePhy)drawArea.GraphicsList[i];

                        dd = new DrawDrawer(drawArea.Owner, dc.rectangle.X, dc.rectangle.Y + dc.rectangle.Height, 1, 1, drawArea.GraphicsList.Count);
                        AddNewObject(drawArea, dd, true);
                        dc.AttachLink(dd, DrawObject.TypeAttach.Child);
                        dd.AttachLink(dc, DrawObject.TypeAttach.Parent);
                        dd.Normalize();
                    }

                    break;
                }
            }
        }

        public void CreateSrv(DrawArea drawArea, string Srv, Point pt, string OParent)
        {
            CreateObjetFromMouse(drawArea, drawArea.GraphicsList.FindObjet(0, OParent), Srv, pt);
            int n = drawArea.GraphicsList.FindObjet(0, Srv);
            if (n > -1)
            {
                DrawDrawer dd = (DrawDrawer)drawArea.GraphicsList[n];
                ifCreat(drawArea, dd);
            }
        }

        public void ifCreat(DrawArea drawArea, DrawDrawer dd)
        {
            string Srv;

            Srv = dd.FindServerFromTv();
            if (Srv != null)
            {
                Owner.Owner.oCnxBase.CmdText = "";
                FormChangeProp fcp = new FormChangeProp(Owner.Owner, null);
                fcp.AddlSourceFromString(Srv);
                fcp.ShowDialog(Owner.Owner);
                if (fcp.Valider)
                {
                    string[] aValue;
                    ToolMachineCTI tm = (ToolMachineCTI)drawArea.tools[(int)DrawArea.DrawToolType.MachineCTI];

                    aValue = Owner.Owner.oCnxBase.CmdText.Split('(', ')');
                    dd.NbrCreatChild = (aValue.Length - 1) / 2;
                    for (int i = 1; i < aValue.Length; i += 2)
                    {
                        tm.CreateSrv(drawArea, aValue[i], new Point(dd.rectangle.X, dd.rectangle.Y), dd.GuidkeyObjet.ToString());
                    }
                }
            }
        }

        
        /// <summary>
        /// Left mouse is released.
        /// New object is created and resized.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        /// 
        /*
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            DrawDrawer dd = (DrawDrawer)drawArea.GraphicsList[0];
                        
            dd.Normalize();
            for (int i = 1; i < drawArea.GraphicsList.Count; i++)
            {
                if (drawArea.GraphicsList[i].PointInObject(new Point(dd.Rectangle.Left, dd.Rectangle.Top)) &
                    drawArea.GraphicsList[i].PointInObject(new Point(dd.Rectangle.Right, dd.Rectangle.Bottom)))
                {
                    drawArea.GraphicsList[i].AttachLink(drawArea.GraphicsList[0], DrawObject.TypeAttach.Child);
                    drawArea.GraphicsList[0].AttachLink(drawArea.GraphicsList[i], DrawObject.TypeAttach.Parent);

                    break;
                }
            }

            //ifCreat(drawArea, dm);
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }*/

        
        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawDrawer dd;
            bool selected=false;

            dd = new DrawDrawer(Owner.Owner, LstValue, LstValueG);
            CreatObjetLink(dd, "GuidBaiePhy", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            
            if (dd.rectangle.X == 0) selected = true;
            AddNewObject(Owner.Owner.drawArea, dd, selected);
                       
            //TreeNode[] ArrayTreeNode = Owner.Owner.tvObjet.Nodes.Find((string) LstValue[0], true);
            //if (ArrayTreeNode.Length == 1) ArrayTreeNode[0].Remove();
            
        }


	}
}
