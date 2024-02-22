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
	public class ToolInssvc : DrawTools.ToolRectangle
	{
		public ToolInssvc(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            oLayers = new LayerList[1];
            oLayers[0] = new LayerList("56716f7f-4feb-4bd3-981b-671972111261");
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
            DrawInssvc disvc;

            disvc = new DrawInssvc(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = disvc;
            else AddNewObject(Owner.Owner.drawArea, disvc, false);
            return disvc;
        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawInssvc disvc;
            bool selected = false;

            disvc = new DrawInssvc( Owner.Owner, LstValue, LstValueG);
            CreatObjetLink(disvc, "GuidInspod", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            AddNewObject(Owner.Owner.drawArea, disvc, selected);
            if (disvc.rectangle.X == 0)
            {
                selected = true;
                ToolNCard tnc = (ToolNCard)Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.NCard];
                tnc.CreatObjetFromParent(disvc);
            }
        }

        public void CreatObjetFrompod(DrawInspod dipod)
        {
            List<String[]> lstGensvc = Owner.Owner.oCnxBase.GetlstGensvc((string)dipod.GetValueFromName("GuidGenpod"));
            List<String[]> lstInssvc = Owner.Owner.oCnxBase.GetlstInssvc((string)dipod.GetValueFromName("GuidInspod"));
            for(int iSvc = 0; iSvc < lstInssvc.Count; iSvc++)
            {
                int n = lstGensvc.FindIndex(el => el[0] == lstInssvc[iSvc][1]);
                if (n < 0) Owner.Owner.oCnxBase.CBWrite("Delete From Inssvc WHERE GuidInssvc='" + lstInssvc[iSvc][0] + "'");
                else
                {
                    lstGensvc.RemoveAt(n);
                    Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.Inssvc].LoadSimpleObject(lstInssvc[iSvc][0]);
                }
            }
            for (int iSvc = 0; iSvc < lstGensvc.Count; iSvc++)
            {
                DrawInssvc disvc = new DrawInssvc(Owner.Owner, lstGensvc[iSvc], Owner.Owner.drawArea.GraphicsList.Count);

                dipod.AttachLink(disvc, DrawObject.TypeAttach.Child);
                disvc.AttachLink(dipod, DrawObject.TypeAttach.Parent);
                AddNewObject(Owner.Owner.drawArea, disvc, false);

                DrawNCard dnc = new DrawNCard(Owner.Owner, disvc);

                disvc.AttachLink(dnc, DrawObject.TypeAttach.Child);
                dnc.AttachLink(disvc, DrawObject.TypeAttach.Parent);
                dnc.InitProp("GuidHote", (object)((DrawInssvc)dnc.LstParent[0]).GuidkeyObjet.ToString(), true);
                AddNewObject(Owner.Owner.drawArea, dnc, false);
            }
        }


    }
}
