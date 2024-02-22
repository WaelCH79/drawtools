using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolContainer : DrawTools.ToolRectangle
	{
		public ToolContainer(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            oLayers = new LayerList[1];
            oLayers[0] = new LayerList("1fcbeaba-e4ff-4488-a0a8-daff58a2a8dd");
            oLayers[0].AddTemplate(Owner.Owner, "", Owner.Owner.sGuidTemplate);
		}

        /*
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            DrawRectangle dr = (DrawRectangle)drawArea.GraphicsList[0];
            if (dr.LstParent != null)
            {
                dr.Normalize();
                for (int i = 1; i < drawArea.GraphicsList.Count; i++)
                {
                    if (drawArea.GraphicsList[i].GetType() == typeof(DrawGenpod))
                    {

                        if (drawArea.GraphicsList[i].ParentPointInObject(new Point(dr.Rectangle.Left, dr.Rectangle.Top)) &
                        drawArea.GraphicsList[i].ParentPointInObject(new Point(dr.Rectangle.Right, dr.Rectangle.Bottom)))
                        {
                            //point = drawArea.GraphicsList[i].GetPointObject(point);
                            drawArea.GraphicsList[i].AttachLink(drawArea.GraphicsList[0], DrawObject.TypeAttach.Child);
                            drawArea.GraphicsList[0].AttachLink(drawArea.GraphicsList[i], DrawObject.TypeAttach.Parent);
                            break;
                        }
                    }
                }
            }

            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }
        */
        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                DrawContainer dc;

                if (Owner.Owner.tvObjet.SelectedNode.Parent == null)
                {
                    dc = new DrawContainer(drawArea.Owner, e.X, e.Y, drawArea.GraphicsList.Count);
                } else
                {
                    LoadSimpleObject(Owner.Owner.tvObjet.SelectedNode.Name);
                    int j = Owner.Owner.drawArea.GraphicsList.FindObjet(0, Owner.Owner.tvObjet.SelectedNode.Name);
                    dc = (DrawContainer)Owner.Owner.drawArea.GraphicsList[j];
                    dc.Rectangle = new Rectangle(e.X, e.Y, 1, 1);
                    
                }
                if (dc.LstParent != null)
                {
                    dc.Normalize();
                    dc.GuidkeyObjet = Guid.NewGuid(); //Afin de différencier les mêmes ServerType dans une Vue
                    for (int i = 0; i < drawArea.GraphicsList.Count; i++)
                    {
                        if (drawArea.GraphicsList[i].GetType() == typeof(DrawGenpod))
                        {

                            if (drawArea.GraphicsList[i].ParentPointInObject(new Point(dc.Rectangle.Left, dc.Rectangle.Top)) &
                            drawArea.GraphicsList[i].ParentPointInObject(new Point(dc.Rectangle.Right, dc.Rectangle.Bottom)))
                            {
                                DrawGenpod dgp = (DrawGenpod)drawArea.GraphicsList[i];
                                AddNewObject(drawArea, dc, true);
                                dgp.AttachLink(dc, DrawObject.TypeAttach.Child);
                                dc.AttachLink(dgp, DrawObject.TypeAttach.Parent);
                                dgp.AligneObjet();
                                break;
                            }
                        }
                    }
                }

                
                drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
                drawArea.Capture = false;
                drawArea.Refresh();
                drawArea.Owner.SetStateOfControls();

            }
        }
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
        }


        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawContainer dc;

            dc = new DrawContainer(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dc;
            else {
                AddNewObject(Owner.Owner.drawArea, dc, false);
                //Owner.Owner.drawArea.GraphicsList.Add(dc);
                CreatObjetLink(dc, "Guidks", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            }
            return dc;
        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawContainer dc;
            bool selected = false;

            dc = new DrawContainer(Owner.Owner, LstValue, LstValueG);

            CreatObjetLink(dc, "GuidGenpod", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            AddNewObject(Owner.Owner.drawArea, dc, selected);
            if (dc.rectangle.X == 0)
            {
                selected = true;
                ArrayList lstTechno = Owner.Owner.oCnxBase.CreatTechnoServer(dc);
                for (int i = 0; i < lstTechno.Count; i++)
                {
                    Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.Techno].LoadSimpleObject((string)lstTechno[i]);
                    
                    int j = Owner.GraphicsList.FindObjet(0, (string)lstTechno[i]);
                    if (j != -1)
                    {

                        DrawTechno dt = (DrawTechno)Owner.GraphicsList[j];
                        dt.GuidkeyObjet = Guid.NewGuid(); //dt.SetValueFromName("GuidTechno", (object)dt.GuidkeyObjet.ToString());
                        dc.AttachLink(dt, DrawObject.TypeAttach.Child);
                        dt.AttachLink(dc, DrawObject.TypeAttach.Parent);
                    }
                    else MessageBox.Show("Attention, la technologie ne peut pas etre chargée. Vérifier qu'elle est bien créée avec la date de fin de support./nLe guid est: " + (string)lstTechno[i]);
                }
            }


        }


	}
}
