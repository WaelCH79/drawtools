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
	/// Polygon tool
	/// </summary>
	public class ToolTechLink : DrawTools.ToolObject
	{
		public ToolTechLink(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "Link.cur");
            InitPropriete("025a3538-f699-41c2-885e-37a67ea0d8ea");
        }

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            // Create new polygon, add it to the list
            // and keep reference to it

            Point point = new Point(e.X, e.Y);
            DrawObject oAttach=null;
            
            for (int i = 0; i < drawArea.GraphicsList.Count; i++)
            {
                if (drawArea.GraphicsList[i].AttachPointInObject(point))
                {
                    point = drawArea.GraphicsList[i].GetPointObject(point);
                    oAttach = drawArea.GraphicsList[i];
                    break;
                }
            }
            AddNewObject(drawArea, new DrawTechLink(drawArea.Owner, point.X, point.Y, point.X + 1, point.Y + 1, drawArea.GraphicsList.Count), true);
            if (oAttach != null)
            {
                DrawTechLink dl = (DrawTechLink) drawArea.GraphicsList[0];
                oAttach.AttachLink(drawArea.GraphicsList[0], DrawObject.TypeAttach.Sortie);
                dl.AttachLink(oAttach, DrawObject.TypeAttach .Entree);
            }
        }

        /// <summary>
        /// Left mouse is released.
        /// New object is created and resized.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {

            
            DrawTechLink dtl = (DrawTechLink) drawArea.GraphicsList[0];
            Point p0 = (Point)dtl.pointArray[0];
            Point p1 = (Point)dtl.pointArray[1];
            if (p0.X == p1.X && p0.Y == p1.Y)
            {
                drawArea.GraphicsList.Remove(0);
            }
            else
            {
                Point point = new Point(e.X, e.Y);
                for (int i = 1; i < drawArea.GraphicsList.Count; i++)
                {
                    if (drawArea.GraphicsList[i].AttachPointInObject(point))
                    {
                        point = drawArea.GraphicsList[i].GetPointObject(point);
                        drawArea.GraphicsList[i].AttachLink(drawArea.GraphicsList[0], DrawObject.TypeAttach.Entree);
                        drawArea.GraphicsList[0].AttachLink(drawArea.GraphicsList[i], DrawObject.TypeAttach.Sortie);

                        drawArea.GraphicsList[0].CompleteLink(DrawObject.TypeAttach.Entree);
                        drawArea.GraphicsList[0].CompleteLink(DrawObject.TypeAttach.Sortie);
                        break;
                    }
                }
                drawArea.GraphicsList[0].Normalize();
            }
            //dtl.InitDatagrid(Owner.Owner.dataGrid);
            drawArea.GraphicsList.UnselectAll();
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }

        public override void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
        {
            DrawTechLink o;
            drawArea.Cursor = Cursor;

            if (e.Button == MouseButtons.Left)
            {
                Point point = new Point(e.X, e.Y);
                o = (DrawTechLink)drawArea.GraphicsList.GetSelectedObject(0);
                o.MoveHandleTo(point, (o.pointArray.Count - 1) * 4 + 1);
                drawArea.Refresh();
            }
        }

        public override string GetFrom(string sType, string sGType)
        {
            return "From DansVue, " + sType + " Left Join LayerLink On " + sType + ".Guid" + sType + "=GuidObj" + " and layerlink.GuidAppVersion='" + Owner.Owner.GetGuidAppVersion() + "', " + sGType + ", GPoint, GroupService";
            //return base.GetFrom(sType);
        }

        public override string GetWhere(string sType, string sGType, string GuidGVue, string sGuidVueSrvPhy)
        {
            return "WHERE GuidGVue ='" + GuidGVue + "' and GuidObjet=Guid" + sGType + " and " + sGType + ".Guid" + sType + "=" + sType + ".Guid" + sType + " AND " + sType + ".GuidGroupService=GroupService.GuidGroupService AND " + sGType + ".Guid" + sGType + "=GPoint.GuidGObjet" + Owner.Owner.wkApp.GetWhereLayer() + " ORDER BY Id DESC, Guid" + sGType + ", I";
            //return base.GetWhere(sType);
        }

        public override void LoadObjectXml(XmlNode Node)
        {
            Table t, tg;
            ArrayList LstValue;
            ArrayList LstValueG = new ArrayList(), LstValuePtG, LstValueFusion;
            ArrayList aGNode = new ArrayList();
            ConfDataBase ConfDB = Owner.Owner.oCnxBase.ConfDB;
            string sTablen = GetTypeSimpleTable();
            string sTablem = GetTypeSimpleGTable();
            int n = ConfDB.FindTable(sTablen);
            int m = ConfDB.FindTable(sTablem);
            aGNode = Owner.Owner.GetNode(Node, sTablem);

            if (n > -1 && m > -1)
            {
                t = (Table)ConfDB.LstTable[n];
                tg = (Table)ConfDB.LstTable[m];
                LstValue = t.InitValueFieldFromXmlNode(Node);


                if (aGNode.Count > 0)
                {
                    LstValueG = tg.InitValueFieldFromXmlNode((XmlNode)aGNode[0]);
                    aGNode = Owner.Owner.GetNode((XmlNode)aGNode[0], "GPoint");
                }

                for (int i = 0; i < aGNode.Count; i++)
                {
                    LstValuePtG = tg.InitValueFieldFromXmlNode((XmlNode)aGNode[i]);
                    LstValueFusion = tg.Merge(LstValueG, LstValuePtG);
                    CreatObjetFromXml(LstValue, LstValueFusion);
                }
            }
        }

        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawTechLink dt;

            dt = new DrawTechLink(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dt;
            else attachEtCreatLink(dt, false);
            Newtonsoft.Json.Linq.JArray aPoints = (Newtonsoft.Json.Linq.JArray)dic["points"];
            for (int j = 0; j < aPoints.Count; j++)
            {
                Newtonsoft.Json.Linq.JObject jo = (Newtonsoft.Json.Linq.JObject)aPoints[j];
                Dictionary<string, object> values = Newtonsoft.Json.Linq.JObject.FromObject(jo).ToObject<Dictionary<string, object>>();
                dt.pointArray.Add(new Point(Convert.ToInt32(jo.GetValue("X").ToString()), Convert.ToInt32(jo.GetValue("Y").ToString())));
            }
            return dt;
        }

        public void attachEtCreatLink(DrawTechLink dl, bool selected)
        {
            string sTypeVue = Owner.Owner.tbTypeVue.Text; // (string)Owner.Owner.cbTypeVue.SelectedItem;
            string sIn = "", sOut = "";

            switch (sTypeVue[0])
            {
                case '2': // 2-Infrastructure
                    sIn = "GuidServerIn"; sOut = "GuidServerOut";
                    break;
                case 'W': // W-Si Inf*
                    sIn = "GuidAppIn"; sOut = "GuidAppOut";
                    break;
            }

            int i = CreatObjetLink(dl, sIn, DrawObject.TypeAttach.Entree, DrawObject.TypeAttach.Sortie);
            int j = CreatObjetLink(dl, sOut, DrawObject.TypeAttach.Sortie, DrawObject.TypeAttach.Entree);
            if (i > -1 && j > -1)
            {
                AddNewObject(Owner.Owner.drawArea, dl, selected);
                TreeNode[] ArrayTreeNode = Owner.Owner.tvObjet.Nodes.Find(dl.GuidkeyObjet.ToString(), true);
                if (ArrayTreeNode.Length == 1) ArrayTreeNode[0].Remove();
            }
            else {
                RemoveLink(dl, sIn, DrawObject.TypeAttach.Entree, DrawObject.TypeAttach.Sortie);
                RemoveLink(dl, sOut, DrawObject.TypeAttach.Sortie, DrawObject.TypeAttach.Entree);
            }

        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawTechLink dl;
            int n;
            bool selected = false;

            n = Owner.GraphicsList.FindObjet(0, (string)LstValue[0]);
            if (n > -1)
            {
                dl = (DrawTechLink)Owner.GraphicsList[n];
                dl.AddPoint(LstValueG);
            }
            else
            {
                string sTypeVue = Owner.Owner.tbTypeVue.Text; // (string)Owner.Owner.cbTypeVue.SelectedItem;

                dl = new DrawTechLink(Owner.Owner, LstValue, LstValueG);
                attachEtCreatLink(dl, selected);

            }
        }
    }
}
