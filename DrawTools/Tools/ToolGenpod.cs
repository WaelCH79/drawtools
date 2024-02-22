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
	public class ToolGenpod : DrawTools.ToolRectangle
	{
		public ToolGenpod(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            oLayers = new LayerList[1];
            oLayers[0] = new LayerList("854589a2-db35-44ce-a594-a8db8c3b0024");
            oLayers[0].AddTemplate(Owner.Owner, "", Owner.Owner.sGuidTemplate);
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawGenpod dgp;

            dgp = new DrawGenpod(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);
            AddNewObject(drawArea, dgp, true);
        }

        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawGenpod dgp;

            dgp = new DrawGenpod(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dgp;
            else {
                AddNewObject(Owner.Owner.drawArea, dgp, false);
                //Owner.Owner.drawArea.GraphicsList.Add(dc);
                CreatObjetLink(dgp, "Guidks", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            }
            return dgp;
        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawGenpod dgp;
            
            dgp = new DrawGenpod(Owner.Owner, LstValue, LstValueG);
            AddNewObject(Owner.Owner.drawArea, dgp, false);

            CreatObjetLink(dgp, "GuidGenks", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
        }


	}
}
