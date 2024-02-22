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
	public class ToolGensvc : DrawTools.ToolRectangle
	{
		public ToolGensvc(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            oLayers = new LayerList[1];
            oLayers[0] = new LayerList("a88bfefc-94d1-450c-ae0b-4513d2d1ab23");
            oLayers[0].AddTemplate(Owner.Owner, "", Owner.Owner.sGuidTemplate);
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawGensvc dgs;

            dgs = new DrawGensvc(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);
            AddNewObject(drawArea, dgs, true);
        }

        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawGensvc dgs;

            dgs = new DrawGensvc(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dgs;
            else {
                AddNewObject(Owner.Owner.drawArea, dgs, false);
                //Owner.Owner.drawArea.GraphicsList.Add(dc);
                CreatObjetLink(dgs, "Guidks", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            }
            return dgs;
        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawGensvc dgs;
            
            dgs = new DrawGensvc(Owner.Owner, LstValue, LstValueG);
            AddNewObject(Owner.Owner.drawArea, dgs, false);

            CreatObjetLink(dgs, "GuidGenks", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
        }


	}
}
