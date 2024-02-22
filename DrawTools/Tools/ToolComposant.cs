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
	public class ToolComposant : DrawTools.ToolRectangle
	{
        public ToolComposant(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");

            //initialisation des proprietes
            oLayers = new LayerList[1];
            oLayers[0] = new LayerList("a5e94d6a-6bb7-4a84-aaa5-33fba950c4d1");
            oLayers[0].AddTemplate(Owner.Owner, "", Owner.Owner.sGuidTemplate);
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawComposant dc;

            dc = new DrawComposant(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);
            AddNewObject(drawArea, dc, true);
        }

        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawComposant dc;

            dc = new DrawComposant(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dc;
            else {
                AddNewObject(Owner.Owner.drawArea, dc, false);
                //Owner.Owner.drawArea.GraphicsList.Add(dc);
                CreatObjetLink(dc, "GuidMainComposant", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            }
            return dc;
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawComposant dc;
            
            dc = new DrawComposant(Owner.Owner, LstValue, LstValueG);
            AddNewObject(Owner.Owner.drawArea, dc, false);

            CreatObjetLink(dc, "GuidMainComposant", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            
            //base.CreatObjetFromBD(From1 f, LstValue, LstValueG);
        }


	}
}
