using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
    public class ToolPtCnx : DrawTools.ToolRectangle
	{
        public ToolPtCnx(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            InitPropriete("0a51891b-628e-4d26-9782-93dab509d05c");
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            for (int i = 0; i < drawArea.GraphicsList.Count; i++)
            {
                if (drawArea.GraphicsList[i].GetType() == typeof(DrawLocation))
                {
                    if (drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                    {
                        DrawLocation dl = (DrawLocation)drawArea.GraphicsList[i];
                        DrawPtCnx dpc = new DrawPtCnx(drawArea.Owner, new Point(e.X, e.Y), dl);
                        AddNewObject(drawArea, dpc, true);
                        dl.AttachLink(dpc, DrawObject.TypeAttach.Child);
                        dpc.AttachLink(dl, DrawObject.TypeAttach.Parent);
                        dpc.InitProp("GuidLocation", (object)((DrawLocation)dpc.LstParent[0]).GuidkeyObjet.ToString(), true);
                        dl.setSens(new Point(e.X, e.Y));
                        dl.AligneObjet();
                        dpc.Normalize();
                        dpc.InitDatagrid(Owner.Owner.dataGrid);
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

        public override string GetWhere(string sType, string sGType, string GuidGVue, string sGuidVueSrvPhy)
        {
            return "WHERE GuidGVue ='" + GuidGVue + "' and GuidObjet=Guid" + sGType + " and " + sGType + ".Guid" + sType + "=" + sType + ".Guid" + sType + " ORDER BY " + sType + ".Nom" + sType;
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawPtCnx dpc; //dsc
            DrawCnx dc; // dss
            int n;
            bool selected = false;

            dpc = new DrawPtCnx(Owner.Owner, LstValue, LstValueG);
            if (dpc.rectangle.X == 0) selected = true;

            n = CreatObjetLink(dpc, "GuidLocation", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            if (n > -1)
            {
                n = CreatObjetLink(dpc, "GuidCnx", DrawObject.TypeAttach.Entree, DrawObject.TypeAttach.Sortie);
                if (n > -1)
                {
                    dc = (DrawCnx)Owner.GraphicsList[n];
                    n = dc.GetAttchHandle(dpc.Rectangle);
                    dc.AttachHandle.Add(n + 10); // +10, car il y a 9 points prient par le rectangle du VLAn
                }
            }
            AddNewObject(Owner.Owner.drawArea, dpc, selected);
        }
	}
}
