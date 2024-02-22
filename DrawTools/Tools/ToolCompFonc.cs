using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data.Odbc;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolCompFonc : DrawTools.ToolRectangle
	{
		public ToolCompFonc(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");

            //initialisation des proprietes
            oLayers = new LayerList[1];
            oLayers[0] = new LayerList("1c385b68-bd4d-43d7-b74a-d7b3aa7c2dce");
            oLayers[0].AddTemplate(Owner.Owner, "", Owner.Owner.sGuidTemplate);
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawCompFonc dcf;
            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                for (int i = 0; i < drawArea.GraphicsList.Count; i++)
                {
                    if (drawArea.GraphicsList[i].GetType() == typeof(DrawMainComposant) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                    {
                        DrawMainComposant dc = (DrawMainComposant)drawArea.GraphicsList[i];
                        LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);
                        dcf = (DrawCompFonc)Owner.GraphicsList[0];

                        dc.AttachLink(dcf, DrawObject.TypeAttach.Child);
                        dcf.AttachLink(dc, DrawObject.TypeAttach.Parent);
                        dc.AligneFonction();
                        dcf.Normalize();
                        break;
                    }
                }
            }
            
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }
        
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {            
        }

        public override string GetTypeSimpleTable()
        {
            return "Module";
        }

        public override string GetSimpleFrom(string sTable)
        {
            return base.GetSimpleFrom("Module");
        }

        public override string GetSimpleWhere(string sTable, string GuidObjet)
        {
            return base.GetSimpleWhere("Module", GuidObjet);
        }

        /*public override string GetSelect(string sTable, string sGTable)
        {
            Table t, tg;
            ConfDataBase ConfDB = Owner.Owner.oCnxBase.ConfDB;
            int n = ConfDB.FindTable(sTable);
            int m = ConfDB.FindTable(sGTable);
            if (n > -1 && m > 1)
            {
                t = (Table)ConfDB.LstTable[n];
                tg = (Table)ConfDB.LstTable[m];
                return "SELECT " + t.GetSelectField(ConfDataBase.FieldOption.Base) + ", " + tg.GetSelectField(ConfDataBase.FieldOption.Base);
            }

            return null;
        }*/

        public override string GetFrom(string sType, string sGType)
        {
            return "From DansVue, " + "Module" + " Left Join LayerLink On Guid" + "Module" + "=GuidObj" + " and layerlink.GuidAppVersion='" + Owner.Owner.GetGuidAppVersion()  + "', " + sGType; 
            //return base.GetFrom(sType);
        }

        /*public override string GetWhere(string sType, string sGType, Guid GuidVue, string sGuidVueSrvPhy)
        {
            //return "WHERE GuidVue ='" + GuidVue + "' and GuidObjet=Guid" + sGType + " and " + sGType + ".Guid" + "Module" + "=" + "Module" + ".Guid" + "Module";
        return "WHERE GuidVue ='" + GuidVue + "' and GuidObjet=Guid" + sGType + " and " + sGType + ".Guid" + sType + "=" + sType + ".Guid" + sType;

            //return base.GetWhere(sType);
        }*/

        public override string GetsType(bool Reel)
        {
            if (Reel) return base.GetsType(Reel);
            return "Module";
        }

        /*
        public override void CreatObjetsFromBD(string sTable, bool CloseReader)
        {
            Table t, tg;
            ArrayList LstValue;
            ArrayList LstValueG;
            ConfDataBase ConfDB = Owner.Owner.oCnxBase.ConfDB;
            OdbcDataReader Reader = Owner.Owner.oCnxBase.Reader;

            int n = ConfDB.FindTable("Module");
            int m = ConfDB.FindTable("G" + sTable);
            if (n > -1 && m > 1)
            {
                t = (Table)ConfDB.LstTable[n];
                LstValue = t.InitValueFieldFromBD(0, Reader);

                tg = (Table)ConfDB.LstTable[m];
                LstValueG = tg.InitValueFieldFromBD(t.GetNbrSelectField(), Reader);
                if (CloseReader) Reader.Close();
                CreatObjetFromBD(LstValue, LstValueG);
            }
            if (CloseReader) Reader.Close();
        }
        */

        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawCompFonc dc;

            dc = new DrawCompFonc(Owner.Owner, dic);
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
            DrawCompFonc dcf;
            bool selected=false;
            
            dcf = new DrawCompFonc(Owner.Owner, LstValue, LstValueG);
            if (dcf.rectangle.X == 0) selected = true;
            AddNewObject(Owner.Owner.drawArea, dcf, selected);

            CreatObjetLink(dcf, "GuidMainComposant", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            
            TreeNode[] ArrayTreeNode = Owner.Owner.tvObjet.Nodes.Find((string) LstValue[0], true);
            if (ArrayTreeNode.Length == 1) ArrayTreeNode[0].Remove();
            
        }


	}
}
