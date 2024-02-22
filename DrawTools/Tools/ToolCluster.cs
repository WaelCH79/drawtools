using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolCluster : DrawTools.ToolRectangle
	{
        public ToolCluster(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            InitPropriete("a8f4f169-ab49-4488-9d36-a573a707efb2");
		}

        public bool CreateObjetFromMouse(DrawArea drawArea, string sGuid, Point e)
        {
            DrawCluster dc;
            bool Create = false;
            /*
            if (drawArea.GraphicsList[i].GetType() == typeof(DrawMachine) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
            {
                DrawMachine dmp = (DrawMachine)drawArea.GraphicsList[i];
                LoadSimpleObject(sGuid);
                dc = (DrawCluster)Owner.GraphicsList[0];

                dmp.AttachLink(dc, DrawObject.TypeAttach.Child);
                dc.AttachLink(dmp, DrawObject.TypeAttach.Parent);
                dc.rectangle.X = e.X; dc.rectangle.Y = e.Y;
                dc.Normalize();
                Create = true;
            }
            else
            */
            {
                LoadSimpleObject(sGuid);
                int n = Owner.GraphicsList.FindObjet(0, sGuid);
                if (n > -1)
                {
                    dc = (DrawCluster)Owner.GraphicsList[n];

                    Owner.GraphicsList.UnselectAll();
                    Owner.GraphicsList[n].Selected = true;
                    drawArea.OSelected = dc;

                    dc.rectangle.X = e.X; dc.rectangle.Y = e.Y;
                    dc.Normalize();
                    Create = true;
                }
            }
            return Create;
        }
        
        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            DrawCluster dc;

            if (drawArea.AddObjet)
            {
                if (drawArea.AddObjet)
                {
                    CreateObjetFromMouse(drawArea, (string)Owner.Owner.tvObjet.SelectedNode.Name, pt);
                    
                    drawArea.AddObjet = false;

                }
                /*
                drawArea.AddObjet = false;
                if (drawArea.GraphicsList.Count == 0)
                {
                    LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);
                    dc = (DrawCluster)Owner.GraphicsList[0];
                    dc.rectangle.X = e.X; dc.rectangle.Y = e.Y;
                    dc.Normalize();
                }
                else
                {
                    for (int i = 0; i < drawArea.GraphicsList.Count; i++)
                    {
                        if (CreateObjetFromMouse(drawArea, i, (string)Owner.Owner.tvObjet.SelectedNode.Name, pt)) break;
                    }
                }
                */
            }
            else
            {
                dc = new DrawCluster(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);

                AddNewObject(drawArea, dc, true);

            }
        }

        /// <summary>
        /// Left mouse is released.
        /// New object is created and resized.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            DrawCluster dc = null;
            string Srv = null;

            string sTypeVue = Owner.Owner.tbTypeVue.Text; // (string)Owner.Owner.cbTypeVue.SelectedItem;
            switch (sTypeVue[0])
            {
                
                case '3': // 3-Production
                case '5':
                case '4':
                case 'F':
                    
                    //dc.AddNcard();
                    /*
                    ArrayList lstServerPhy = Owner.Owner.oCnxBase.CreatServerCluster(dc);

                    for (int i = 0; i < lstServerPhy.Count; i++)
                    {
                        Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.ServerPhy].LoadSimpleObject((string)lstServerPhy[i]);
                        int j = Owner.GraphicsList.FindObjet(0, (string)lstServerPhy[i]);
                        DrawServerPhy ds = (DrawServerPhy)Owner.GraphicsList[j];
                        dc.AttachLink(ds, DrawObject.TypeAttach.Child);
                        ds.AttachLink(dc, DrawObject.TypeAttach.Parent);

                        ds.rectangle.Width = (dc.Rectangle.Width - (lstServerPhy.Count + 1) * Marge) / lstServerPhy.Count;
                        ds.rectangle.X = dc.Rectangle.X + Marge + i * (ds.Rectangle.Width + Marge);
                        ds.rectangle.Y = dc.Rectangle.Y + 3*Marge;
                        ds.rectangle.Height = dc.Rectangle.Height - 2 * Marge;
                    }
                */
                    break;
                    
                case '8':
                case '7':
                    dc = (DrawCluster)drawArea.GraphicsList[0];

                    dc.Normalize();


                    Srv = dc.FindServerFromTv();
                    if (Srv != null)
                    {
                        Owner.Owner.oCnxBase.CmdText = "";
                        FormChangeProp fcp = new FormChangeProp(Owner.Owner, null);
                        fcp.AddlSourceFromString(Srv);
                        fcp.ShowDialog(Owner.Owner);
                        if (fcp.Valider)
                        {
                            string[] aValue;
                            
                            aValue = Owner.Owner.oCnxBase.CmdText.Split('(', ')');
                            dc.NbrCreatChild = (aValue.Length - 1) / 2;
                            ToolMachine tm = (ToolMachine)drawArea.tools[(int)DrawArea.DrawToolType.Machine];
                            for (int i = 1; i < aValue.Length; i += 2)
                            {
                                int x = 0, y = 0;
                                if (dc.rectangle.Height > dc.rectangle.Width)
                                {
                                    x = dc.Rectangle.X + Marge;
                                    y = dc.Rectangle.Y + Marge + (i - 1) / 2 * (dc.Rectangle.Height - 2 * Marge) / dc.NbrCreatChild;
                                }
                                else
                                {
                                    x = dc.Rectangle.X + Marge + (i - 1) / 2 * (dc.Rectangle.Width - 2 * Marge) / dc.NbrCreatChild;
                                    y = dc.Rectangle.Y + Marge;
                                }
                                tm.CreateSrv(drawArea, aValue[i], new Point(x, y), dc.GuidkeyObjet.ToString());
                            }
                        }
                    }
                    else
                    {
                        ArrayList lstMachine = Owner.Owner.oCnxBase.CreatServerCluster(dc);

                        for (int i = 0; i < lstMachine.Count; i++)
                        {
                            Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.Machine].LoadSimpleObject((string)lstMachine[i]);
                            int j = Owner.GraphicsList.FindObjet(0, (string)lstMachine[i]);
                            DrawMachine dm = (DrawMachine)Owner.GraphicsList[j];
                            dc.AttachLink(dm, DrawObject.TypeAttach.Child);
                            dm.AttachLink(dc, DrawObject.TypeAttach.Parent);

                            dm.rectangle.Width = (dc.Rectangle.Width - (lstMachine.Count + 1) * Marge) / lstMachine.Count;
                            dm.rectangle.X = dc.Rectangle.X + Marge + i * (dm.Rectangle.Width + Marge);
                            dm.rectangle.Y = dc.Rectangle.Y + Marge;
                            dm.rectangle.Height = dc.Rectangle.Height - 2 * Marge;
                        }

                    }

                    break;
            }
            //dc.InitDatagrid(Owner.Owner.dataGrid);
            Owner.Owner.drawArea.GraphicsList.UnselectAll();
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawCluster dc;
            bool selected = false;

            dc = new DrawCluster(Owner.Owner, LstValue, LstValueG);
            if (dc.rectangle.X == 0)
            {
                selected = true;

                //dc.AddNcard();

                ArrayList lstServerPhy = Owner.Owner.oCnxBase.CreatServerCluster(dc);

                for (int i = 0; i < lstServerPhy.Count; i++)
                {
                    Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.ServerPhy].LoadSimpleObject((string)lstServerPhy[i]);
                    int j = Owner.GraphicsList.FindObjet(0, (string)lstServerPhy[i]);
                    DrawServerPhy ds = (DrawServerPhy)Owner.GraphicsList[j];
                    dc.AttachLink(ds, DrawObject.TypeAttach.Child);
                    ds.AttachLink(dc, DrawObject.TypeAttach.Parent);

                    ds.rectangle.Width = (dc.Rectangle.Width - (lstServerPhy.Count + 1) * Marge) / lstServerPhy.Count;
                    ds.rectangle.X = dc.Rectangle.X + Marge + i * (ds.Rectangle.Width + Marge);
                    ds.rectangle.Y = dc.Rectangle.Y + 3 * Marge;
                    ds.rectangle.Height = dc.Rectangle.Height - 2 * Marge;
                }
            }

                AddNewObject(Owner.Owner.drawArea, dc, selected);
            //base.CreatObjetFromBD(From1 f, LstValue, LstValueG);
        }

	}

    
}
