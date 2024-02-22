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
	public class ToolQueue : DrawTools.ToolRectangle
	{
		public ToolQueue(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");

            //initialisation des proprietes
            oLayers = new LayerList[1];
            oLayers[0] = new LayerList("abfa809b-e73f-4b2f-866c-432942a1019f");
            oLayers[0].AddTemplate(Owner.Owner, "", Owner.Owner.sGuidTemplate);
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawQueue dq;

            dq = new DrawQueue(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);
            AddNewObject(drawArea, dq, true);
        }

        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawQueue dq;

            dq = new DrawQueue(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dq;
            else {
                AddNewObject(Owner.Owner.drawArea, dq, false);
                //Owner.Owner.drawArea.GraphicsList.Add(dc);
                CreatObjetLink(dq, "GuidMainComposant", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            }
            return dq;
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawQueue dq;

            dq = new DrawQueue(Owner.Owner, LstValue, LstValueG);
            AddNewObject(Owner.Owner.drawArea, dq, false);

            CreatObjetLink(dq, "GuidMainComposant", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
        }
	}
}
