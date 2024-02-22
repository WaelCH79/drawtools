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
    public class ToolMCompServ : DrawTools.ToolRectangle
	{
        public ToolMCompServ(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            InitPropriete("59be2b47-4e8b-450a-9d38-90d23318c899");
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawMCompServ dmcs = null;
            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                for (int i = 0; i < drawArea.GraphicsList.Count; i++)
                {
                    if (drawArea.GraphicsList[i].GetType() == typeof(DrawMainComposant) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                    {
                        DrawMainComposant dmcInit = (DrawMainComposant)drawArea.GraphicsList[i];
                        string sGuid = (string)dmcInit.GetValueFromName("GuidMainComposant");
                        int j = 0;
                        j = Owner.GraphicsList.FindObjetFromValue(j, 0, sGuid);
                        while (j != -1)
                        {
                            DrawMainComposant dmc = (DrawMainComposant)drawArea.GraphicsList[j];
                            dmcs = new DrawMCompServ(drawArea.Owner);
                            dmcs.GuidkeyObjet = Guid.NewGuid();
                            dmc.AttachLink(dmcs, DrawObject.TypeAttach.Child);
                            dmcs.AttachLink(dmc, DrawObject.TypeAttach.Parent);
                            dmc.AligneObjet();
                            dmcs.Normalize();
                            AddNewObject(Owner.Owner.drawArea, dmcs, true);
                            j += 2;
                            j = Owner.GraphicsList.FindObjetFromValue(j, 0, sGuid);
                        }
                        break;
                    }
                }
            }
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }
       
        /// <summary>
        /// Left mouse is released.
        /// New object is created and resized.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            //drawArea.Owner.SetStateOfControls();
        }

        public override string GetSimpleFrom(string sTable)
        {
            return "FROM " + sTable + ",MainComposantRef";
        }


        public override string GetSimpleWhere(string sTable, string GuidObjet)
        {
            return "WHERE " + sTable + ".GuidMainComposantRef=MainComposantRef.GuidMainComposantRef and Guid" + sTable + "='" + GuidObjet + "'";
        }

        public override string GetSimpleSelect(Table t)
        {
            return "GuidMCompServ, MainComposantRef.NomMainComposantRef, MCompServ.GuidMainComposantRef, MCompserv.GuidMainComposant";
        }

        public override string GetWhere(string sType, string sGType, string GuidGVue, string sGuidVueSrvPhy)
        {
            return "WHERE GuidGVue ='" + GuidGVue + "' and GuidObjet=Guid" + sGType + " and " + sGType + ".Guid" + sType + "=" + sType + ".Guid" + sType;
        }

        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawMCompServ dc;

            dc = new DrawMCompServ(Owner.Owner, dic);
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
            DrawMCompServ dmcs;
            bool selected = false;

            dmcs = new DrawMCompServ(Owner.Owner, LstValue, LstValueG);
            if (dmcs.rectangle.X == 0) selected = true;
            AddNewObject(Owner.Owner.drawArea, dmcs, selected);

            CreatObjetLink(dmcs, "GuidMainComposant", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
        }

	}
}
