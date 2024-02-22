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
	public class ToolBase : DrawTools.ToolRectangle
	{
		public ToolBase(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            oLayers = new LayerList[1];
            oLayers[0] = new LayerList("bbdebd07-7235-4838-bcc8-e384dc49fbb2");
            oLayers[0].AddTemplate(Owner.Owner, "", Owner.Owner.sGuidTemplate);
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawBase db;

            db = new DrawBase(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);
            AddNewObject(drawArea, db, true);
        }

        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawBase db;

            db = new DrawBase(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = db;
            else {
                AddNewObject(Owner.Owner.drawArea, db, false);
                //Owner.Owner.drawArea.GraphicsList.Add(dc);
                CreatObjetLink(db, "GuidMainComposant", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            }
            return db;
        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawBase db;
            
            db = new DrawBase(Owner.Owner, LstValue, LstValueG);
            AddNewObject(Owner.Owner.drawArea, db, false);

            CreatObjetLink(db, "GuidMainComposant", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
        }


	}
}
