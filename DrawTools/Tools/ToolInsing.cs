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
	public class ToolInsing : DrawTools.ToolRectangle
	{
		public ToolInsing(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            oLayers = new LayerList[1];
            oLayers[0] = new LayerList("3b7cc9bf-1eee-4056-8faf-20c995637769");
            oLayers[0].AddTemplate(Owner.Owner, "", Owner.Owner.sGuidTemplate);
        }
        
        /*
        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);

            if (drawArea.AddObjet)
            {
                CreateObjetFromMouse(drawArea, GetIndexObjetFromPoint(pt), (string)Owner.Owner.tvObjet.SelectedNode.Name, pt);

                drawArea.AddObjet = false;
                
            }
            else AddNewObject(drawArea, new DrawInsks(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count), true);            
        }
        

        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            DrawInspod dipod = (DrawInspod)drawArea.GraphicsList[0];

            dipod.Normalize();

            Owner.Owner.drawArea.GraphicsList.MoveSelectionToBack();
            
        }

        
        public bool CreateObjetFromMouse(DrawArea drawArea, int i, string sGuid, Point e)
        {
            bool Create = false;

            Owner.Owner.oCureo = new ExpObj(new Guid(sGuid), "", DrawArea.DrawToolType.Inspod);
            drawArea.tools[(int)Owner.Owner.oCureo.ObjTool].LoadSimpleObjectSansGraph(Owner.Owner.oCureo.GuidObj.ToString());

            if (Owner.Owner.oCureo.oDraw != null)
            {
                AddNewObject(drawArea, new DrawInsks(drawArea.Owner, e.X, e.Y, 1, 1, (DrawInspod)Owner.Owner.oCureo.oDraw, drawArea.GraphicsList.Count), true);
                Owner.Owner.oCureo = null;
                Create = true;
            }
            
            return Create;
        }
        */
        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawInsing diing;

            diing = new DrawInsing(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = diing;
            else AddNewObject(Owner.Owner.drawArea, diing, false);
            return diing;
        }

        public void CreatObjetFrompod(DrawInspod dipod)
        {
            List<String[]> lstGening = Owner.Owner.oCnxBase.GetlstGening((string)dipod.GetValueFromName("GuidGenpod"));
            List<String[]> lstInsing = Owner.Owner.oCnxBase.GetlstInsing((string)dipod.GetValueFromName("GuidInspod"));

            for (int iIng = 0; iIng < lstInsing.Count; iIng++)
            {
                int n = lstGening.FindIndex(el => el[0] == lstInsing[iIng][1]);
                if (n < 0) Owner.Owner.oCnxBase.CBWrite("Delete From Insing WHERE GuidInsing='" + lstInsing[iIng][0] + "'");
                else
                {
                    lstGening.RemoveAt(n);
                    Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.Insing].LoadSimpleObject(lstInsing[iIng][0]);
                }
            }

            for (int iIng = 0; iIng < lstGening.Count; iIng++)
            {
                DrawInsing diing = new DrawInsing(Owner.Owner, lstGening[iIng], Owner.Owner.drawArea.GraphicsList.Count);

                dipod.AttachLink(diing, DrawObject.TypeAttach.Child);
                diing.AttachLink(dipod, DrawObject.TypeAttach.Parent);
                AddNewObject(Owner.Owner.drawArea, diing, false);

                DrawNCard dnc = new DrawNCard(Owner.Owner, diing);

                diing.AttachLink(dnc, DrawObject.TypeAttach.Child);
                dnc.AttachLink(diing, DrawObject.TypeAttach.Parent);
                dnc.InitProp("GuidHote", (object)((DrawInsing)dnc.LstParent[0]).GuidkeyObjet.ToString(), true);
                AddNewObject(Owner.Owner.drawArea, dnc, false);

            }

        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawInsing diing;
            bool selected = false;

            diing = new DrawInsing( Owner.Owner, LstValue, LstValueG);
            CreatObjetLink(diing, "GuidInspod", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            AddNewObject(Owner.Owner.drawArea, diing, selected);
            if (diing.rectangle.X == 0)
            {
                selected = true;
                ToolNCard tnc = (ToolNCard)Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.NCard];
                tnc.CreatObjetFromParent(diing);
            } 
        }
        

	}
}
