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
	public class ToolInsns : DrawTools.ToolRectangle
	{
		public ToolInsns(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            oLayers = new LayerList[1];
            oLayers[0] = new LayerList("ee5ac924-1826-4b4f-8b3d-55e07290e9e4");
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
            DrawInsns dins = (DrawInsns)drawArea.GraphicsList[0];

            dins.Normalize();

            Owner.Owner.drawArea.GraphicsList.MoveSelectionToBack();
            
        }

        /*
        public bool CreateObjetFromMouse(DrawArea drawArea, int i, string sGuid, Point e)
        {
            bool Create = false;

            Owner.Owner.oCureo = new ExpObj(new Guid(sGuid), "", DrawArea.DrawToolType.Insns);
            drawArea.tools[(int)Owner.Owner.oCureo.ObjTool].LoadSimpleObjectSansGraph(Owner.Owner.oCureo.GuidObj.ToString());

            if (Owner.Owner.oCureo.oDraw != null)
            {
                AddNewObject(drawArea, new DrawInsns(drawArea.Owner, e.X, e.Y, 1, 1, (DrawInsns)Owner.Owner.oCureo.oDraw, drawArea.GraphicsList.Count), true);
                Owner.Owner.oCureo = null;
                Create = true;
            }
            
            return Create;
        }
        */

        public void CreatObjetFromks(DrawArea drawArea, DrawInsks dik)
        {
            DrawInsns dins = new DrawInsns(Owner.Owner, drawArea.GraphicsList.Count);

            //CreatObjetLink(dins, "GuidInsks", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            dik.AttachLink(dins, DrawObject.TypeAttach.Child);
            dins.AttachLink(dik, DrawObject.TypeAttach.Parent);
            AddNewObject(Owner.Owner.drawArea, dins, false);

            //ArrayList lstGenpod = Owner.Owner.oCnxBase.GetlstGenpod((int)DrawArea.DrawToolType.Genks,(string)dik.GetValueFromName("GuidGenks"));
            ToolInspod tipod = (ToolInspod)Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.Inspod];
            tipod.CreatObjetFromns(drawArea, dins, (int)DrawArea.DrawToolType.Genks, (string)dik.GetValueFromName("GuidGenks"));
        }

        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawInsns dins;

            dins = new DrawInsns(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dins;
            else AddNewObject(Owner.Owner.drawArea, dins, false);
            return dins;
        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawInsns dins;
            bool selected = false;

            dins = new DrawInsns( Owner.Owner, LstValue, LstValueG);
            CreatObjetLink(dins, "GuidInsks", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            AddNewObject(Owner.Owner.drawArea, dins, selected);
            if (dins.rectangle.X == 0)
            {
                selected = true;
                //ArrayList lstGenpod = Owner.Owner.oCnxBase.GetlstGenpod((int)DrawArea.DrawToolType.Insks,(string)dins.GetValueFromName("GuidInsks"));

                ToolInspod tipod = (ToolInspod)Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.Inspod];
                tipod.CreatObjetFromns(Owner.Owner.drawArea, dins, (int)DrawArea.DrawToolType.Insks, (string)dins.GetValueFromName("GuidInsks"));
                /*
                ArrayList lstTechno = Owner.Owner.oCnxBase.CreatTechnoServer(dst);
                for (int i = 0; i < lstTechno.Count; i++)
                {
                    Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.Techno].LoadSimpleObject((string)lstTechno[i]);
                    int j = Owner.GraphicsList.FindObjet(0, (string)lstTechno[i]);
                    DrawTechno dt = (DrawTechno)Owner.GraphicsList[j];
                    dt.GuidkeyObjet = Guid.NewGuid(); //dt.SetValueFromName("GuidTechno", (object)dt.GuidkeyObjet.ToString());
                    dst.AttachLink(dt, DrawObject.TypeAttach.Child);
                    dt.AttachLink(dst, DrawObject.TypeAttach.Parent);
                }
                */
            }
        }
    }
}
