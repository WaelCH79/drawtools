using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
    public class ToolSanCard : DrawTools.ToolRectangle
	{
        public ToolSanCard(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            InitPropriete("4be334d1-8897-4eed-84d4-5f07b5d07a8f");
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            for (int i = 0; i < drawArea.GraphicsList.Count; i++)
            {
                if (drawArea.GraphicsList[i].GetType() == typeof(DrawServerPhy))
                {
                    if (drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                    {
                        DrawServerPhy ds = (DrawServerPhy)drawArea.GraphicsList[i];
                        DrawSanCard dc = new DrawSanCard(drawArea.Owner, new Point(e.X, e.Y), ds);
                        AddNewObject(drawArea, dc, true);
                        ds.AttachLink(dc, DrawObject.TypeAttach.Child);
                        dc.AttachLink(ds, DrawObject.TypeAttach.Parent);
                        dc.InitProp("GuidHote", (object)((DrawServerPhy)dc.LstParent[0]).GuidkeyObjet.ToString(), true);
                        ds.AligneObjet();
                        dc.Normalize();
                        break;
                    }
                }
                else if (drawArea.GraphicsList[i].GetType() == typeof(DrawBaieCTI))
                {
                    if (drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                    {
                        DrawBaieCTI db = (DrawBaieCTI)drawArea.GraphicsList[i];
                        DrawSanCard dc = new DrawSanCard(drawArea.Owner, new Point(e.X, e.Y), db);
                        AddNewObject(drawArea, dc, true);
                        db.AttachLink(dc, DrawObject.TypeAttach.Child);
                        dc.AttachLink(db, DrawObject.TypeAttach.Parent);
                        dc.InitProp("GuidHote", (object)((DrawBaieCTI)dc.LstParent[0]).GuidkeyObjet.ToString(), true);
                        db.AligneObjet();
                        dc.Normalize();
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
            //return base.GetWhere(sType, GuidVue);
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawSanCard dsc;
            DrawSanSwitch dss;
            int n;
            bool selected = false;

            dsc = new DrawSanCard(Owner.Owner, LstValue, LstValueG);
            if (dsc.rectangle.X == 0) selected = true;

            n = CreatObjetLink(dsc, "GuidHote", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            if (n > -1)
            {
                dsc.Hauteur = dsc.setHauteur(new Point(dsc.rectangle.X, dsc.rectangle.Y), (DrawRectangle)Owner.GraphicsList[n]);
                n = CreatObjetLink(dsc, "GuidSanSwitch", DrawObject.TypeAttach.Entree, DrawObject.TypeAttach.Sortie);
                if (n > -1)
                {
                    dss = (DrawSanSwitch)Owner.GraphicsList[n];
                    n = dss.GetAttchHandle(dsc.Rectangle);
                    dss.AttachHandle.Add(n + 10); // +10, car il y a 9 points prient par le rectangle du VLAn
                }
            }
            AddNewObject(Owner.Owner.drawArea, dsc, selected);
        }
	}
}
