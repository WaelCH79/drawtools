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
	public class ToolInspod : DrawTools.ToolRectangle
	{
		public ToolInspod(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            oLayers = new LayerList[1];
            oLayers[0] = new LayerList("4b4267e9-6ccb-4547-9494-07adccda698a");
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
        */

        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            DrawInspod dipod = (DrawInspod)drawArea.GraphicsList[0];

            dipod.Normalize();

            Owner.Owner.drawArea.GraphicsList.MoveSelectionToBack();
            
        }

        /*
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

        public void CreatObjetFromns(DrawArea drawArea, DrawInsns dins, int iTypeObjet, string sGuid)
        {
            //ArrayList lstGenpod = Owner.Owner.oCnxBase.GetlstGenpod(iTypeObjet, sGuid);
            List<String[]> lstGenpod = Owner.Owner.oCnxBase.GetlstGenpod(iTypeObjet, sGuid);
            List<String[]> lstInspod = Owner.Owner.oCnxBase.GetlstInspod((string)dins.GetValueFromName("GuidInsns"));
            for (int iPod = 0; iPod < lstInspod.Count; iPod++)
            {
                int n = lstGenpod.FindIndex(el => el[0] == lstInspod[iPod][1]);
                if (n < 0) Owner.Owner.oCnxBase.CBWrite("Delete From Inspod WHERE GuidInspod='" + lstInspod[iPod][0] + "'");
                else
                {
                    lstGenpod.RemoveAt(n);
                    Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.Inspod].LoadSimpleObject(lstInspod[iPod][0]);
                }
            }

            for (int iPod = 0; iPod < lstGenpod.Count; iPod++)
            {
                DrawInspod dipod = new DrawInspod(Owner.Owner, lstGenpod[iPod], drawArea.GraphicsList.Count);

                dins.AttachLink(dipod, DrawObject.TypeAttach.Child);
                dipod.AttachLink(dins, DrawObject.TypeAttach.Parent);
                AddNewObject(Owner.Owner.drawArea, dipod, false);

                ToolInssvc tisvc = (ToolInssvc)Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.Inssvc];
                tisvc.CreatObjetFrompod(dipod);

                ToolInsing tiing = (ToolInsing)Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.Insing];
                tiing.CreatObjetFrompod(dipod);
            }
        }
        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawInspod dipod;

            dipod = new DrawInspod(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dipod;
            else AddNewObject(Owner.Owner.drawArea, dipod, false);
            return dipod;
        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawInspod dipod;
            bool selected = false;

            dipod = new DrawInspod( Owner.Owner, LstValue, LstValueG);
            CreatObjetLink(dipod, "GuidInsns", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            AddNewObject(Owner.Owner.drawArea, dipod, selected);
            if (dipod.rectangle.X == 0)
            {
                selected = true;

                ToolInssvc tisvc = (ToolInssvc)Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.Inssvc];
                tisvc.CreatObjetFrompod(dipod);

                ToolInsing tiing = (ToolInsing)Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.Insing];
                tiing.CreatObjetFrompod(dipod);

            }
        }
        

	}
}
