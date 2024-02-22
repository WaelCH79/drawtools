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
	public class ToolGenks : DrawTools.ToolRectangle
	{
		public ToolGenks(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            oLayers = new LayerList[1];
            oLayers[0] = new LayerList("355266ed-dcd4-425b-a4fa-4daf9d42de8f");
            oLayers[0].AddTemplate(Owner.Owner, "", Owner.Owner.sGuidTemplate);

        }

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            if (drawArea.AddObjet) drawArea.AddObjet = false;
            else AddNewObject(drawArea, new DrawGenks(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count), true);            
        }
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            base.OnMouseUp(drawArea, e);
            Owner.Owner.drawArea.GraphicsList.MoveSelectionToBack();
            
        }

        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawGenks di;

            di = new DrawGenks(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = di;
            else AddNewObject(Owner.Owner.drawArea, di, false);
            return di;
        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawGenks di;
            bool selected = false;

            di = new DrawGenks( Owner.Owner, LstValue, LstValueG);
            if (di.rectangle.X == 0)
            {
                selected = true;
            } else AddNewObject(Owner.Owner.drawArea, di, selected);
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {

            DrawGenks dgk;

            dgk = new DrawGenks(Owner.Owner, LstValue);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dgk;
            else Owner.Owner.drawArea.GraphicsList.Add(dgk);
        }

    }
}
