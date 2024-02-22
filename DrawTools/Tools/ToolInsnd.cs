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
	public class ToolInsnd : DrawTools.ToolRectangle
	{
		public ToolInsnd(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            oLayers = new LayerList[1];
            oLayers[0] = new LayerList("dd5a0a25-095d-4fb2-8987-3f60006f7bf8");
            oLayers[0].AddTemplate(Owner.Owner, "", Owner.Owner.sGuidTemplate);
        }

               
        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawInsnd dind = new DrawInsnd(Owner.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);
            AddNewObject(drawArea, dind, true);
        }

        
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            base.OnMouseUp(drawArea, e);

            DrawInsnd dind = (DrawInsnd)drawArea.GraphicsList[0];
            dind.Normalize();
            DrawNCard dnc = new DrawNCard(Owner.Owner, dind);

            dind.AttachLink(dnc, DrawObject.TypeAttach.Child);
            dnc.AttachLink(dind, DrawObject.TypeAttach.Parent);
            dnc.InitProp("GuidHote", (object)((DrawInsnd)dnc.LstParent[0]).GuidkeyObjet.ToString(), true);
            AddNewObject(Owner.Owner.drawArea, dnc, false);

            dind.AligneObjet();
            dind.InitDatagrid(Owner.Owner.dataGrid);

            //Owner.Owner.drawArea.GraphicsList.MoveSelectionToBack();
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();

        }

        /*
        public bool CreateObjetFromMouse(DrawArea drawArea, int i, string sGuid, Point e)
        {
            bool Create = false;

            Owner.Owner.oCureo = new ExpObj(new Guid(sGuid), "", DrawArea.DrawToolType.Genks);
            drawArea.tools[(int)Owner.Owner.oCureo.ObjTool].LoadSimpleObjectSansGraph(Owner.Owner.oCureo.GuidObj.ToString());

            if (Owner.Owner.oCureo.oDraw != null)
            {
                AddNewObject(drawArea, new DrawInsks(drawArea.Owner, e.X, e.Y, 1, 1, (DrawGenks)Owner.Owner.oCureo.oDraw, drawArea.GraphicsList.Count), true);
                Owner.Owner.oCureo = null;
                Create = true;
            }
            
            return Create;
        }
        */

        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawInsnd dind;

            dind = new DrawInsnd(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dind;
            else AddNewObject(Owner.Owner.drawArea, dind, false);
            return dind;
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawInsnd dind;
            bool selected = false;

            dind = new DrawInsnd( Owner.Owner, LstValue, LstValueG);
            CreatObjetLink(dind, "GuidInsks", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            AddNewObject(Owner.Owner.drawArea, dind, selected);
            if (dind.rectangle.X == 0)
            {
                selected = true;
                int j;
                ArrayList lstNCard = Owner.Owner.oCnxBase.CreatNcardHote(dind);

                for (int i = 0; i < lstNCard.Count; i++)
                {
                    Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.NCard].LoadSimpleObject((string)lstNCard[i]);
                    j = Owner.GraphicsList.FindObjet(0, (string)lstNCard[i]);
                    DrawNCard dc = (DrawNCard)Owner.GraphicsList[j];
                    dind.AttachLink(dc, DrawObject.TypeAttach.Child);
                    dc.AttachLink(dind, DrawObject.TypeAttach.Parent);
                }
                dind.AligneObjet();
                //ToolInspod tipod = (ToolInspod)Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.Inspod];
                //tipod.CreatObjetFromns(Owner.Owner.drawArea, dins, (int)DrawArea.DrawToolType.Insks, (string)dins.GetValueFromName("GuidInsks"));
            }
            
        }

        /*
        public void CreatObjetFromks(DrawArea drawArea, DrawInsks dik)
        {
            DrawInsnd dind = new DrawInsnd(Owner.Owner, drawArea.GraphicsList.Count);

            //CreatObjetLink(dins, "GuidInsks", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            dik.AttachLink(dind, DrawObject.TypeAttach.Child);
            dind.AttachLink(dik, DrawObject.TypeAttach.Parent);
            AddNewObject(Owner.Owner.drawArea, dind, false);

        }
        */

    }
}
