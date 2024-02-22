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
	public class ToolGening : DrawTools.ToolRectangle
	{
		public ToolGening(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            oLayers = new LayerList[1];
            oLayers[0] = new LayerList("e474e352-6710-460e-a38d-0dbc7ba3b6e4");
            oLayers[0].AddTemplate(Owner.Owner, "", Owner.Owner.sGuidTemplate);
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawGening dgi;

            dgi = new DrawGening(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);
            AddNewObject(drawArea, dgi, true);
        }

        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawGening dgi;

            dgi = new DrawGening(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dgi;
            else {
                AddNewObject(Owner.Owner.drawArea, dgi, false);
                //Owner.Owner.drawArea.GraphicsList.Add(dc);
                CreatObjetLink(dgi, "Guidks", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            }
            return dgi;
        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawGening dgi;
            
            dgi = new DrawGening(Owner.Owner, LstValue, LstValueG);
            AddNewObject(Owner.Owner.drawArea, dgi, false);

            CreatObjetLink(dgi, "GuidGenks", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
        }


	}
}
