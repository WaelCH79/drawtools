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
	public class ToolFile : DrawTools.ToolRectangle
	{
		public ToolFile(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");

            //initialisation des proprietes
            oLayers = new LayerList[1];
            oLayers[0] = new LayerList("56edcbb5-8916-40e4-baea-68c02f6a8f99");
            oLayers[0].AddTemplate(Owner.Owner, "", Owner.Owner.sGuidTemplate);
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawFile df;

            df = new DrawFile(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);
            AddNewObject(drawArea, df, true);
        }

        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawFile df;

            df = new DrawFile(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = df;
            else {
                AddNewObject(Owner.Owner.drawArea, df, false);
                //Owner.Owner.drawArea.GraphicsList.Add(dc);
                CreatObjetLink(df, "GuidMainComposant", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            }
            return df;
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawFile df;

            df = new DrawFile(Owner.Owner, LstValue, LstValueG);
            AddNewObject(Owner.Owner.drawArea, df, false);

            CreatObjetLink(df, "GuidMainComposant", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
        }
	}
}
