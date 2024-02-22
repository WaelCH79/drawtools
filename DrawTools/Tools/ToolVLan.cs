using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolVLan : DrawTools.ToolRectangle
	{
        public static string[] ssCat = { "Applications", "Serveurs" };
        public ToolVLan(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            InitPropriete("ceb5a71e-e9ae-4c4f-be02-fab629b03dc4");
		}


        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawVLan dv;

            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);

                dv = (DrawVLan)Owner.GraphicsList[0];
                dv.rectangle.X = e.X; dv.rectangle.Y = e.Y;
                dv.Normalize();
            }
            else
            {
                dv = new DrawVLan(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);
                AddNewObject(drawArea, dv, true);
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
            //Point point = new Point(e.X, e.Y);
            DrawVLan dv = (DrawVLan)drawArea.GraphicsList[0];
            dv.Normalize();
            for (int i = 1; i < drawArea.GraphicsList.Count; i++)
            {
                if (drawArea.GraphicsList[i].ParentPointInObject(new Point(dv.Rectangle.Left, dv.Rectangle.Top)) &
                    drawArea.GraphicsList[i].ParentPointInObject(new Point(dv.Rectangle.Right, dv.Rectangle.Bottom)))
                {
                    //point = drawArea.GraphicsList[i].GetPointObject(point);
                    drawArea.GraphicsList[i].AttachLink(drawArea.GraphicsList[0], DrawObject.TypeAttach.Child);
                    drawArea.GraphicsList[0].AttachLink(drawArea.GraphicsList[i], DrawObject.TypeAttach.Parent);
                    break;
                }
            }
            dv.InitDatagrid(Owner.Owner.dataGrid);
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }

        public override string GetFrom(string sType, string sGType)
        {
            return "From DansVue, " + sType + " Left Join LayerLink On Guid" + sType + "=GuidObj and layerlink.GuidAppVersion='" + Owner.Owner.GetGuidAppVersion()  + "', " + sGType + ", VlanClass";
        }

        public override string GetWhere(string sType, string sGType, string GuidGVue, string sGuidVueSrvPhy)
        {
            return "WHERE GuidGVue ='" + GuidGVue + "' and GuidObjet=Guid" + sGType + " and " + sGType + ".Guid" + sType + "=" + sType + ".Guid" + sType + " AND " + sType + ".GuidVlanClass=VlanClass.GuidVlanClass" + Owner.Owner.wkApp.GetWhereLayer();
        }

        public override string GetSimpleSelect(Table t)
        {
            return t.GetSelectField(ConfDataBase.FieldOption.InterneBD);
        }

        public override string GetSimpleFrom(string sTable)
        {
            return "FROM " + sTable + ", VlanClass";
        }

        public override string GetSimpleWhere(string sTable, string GuidObjet)
        {
            return "WHERE Guid" + sTable + "='" + GuidObjet + "' AND " + sTable + ".GuidVlanClass=VlanClass.GuidVlanClass";
        }

        public virtual void LoadSimpleObject(string GuidObjet)
        {
            string Select, From, Where;
            string sType = GetTypeSimpleTable();
            //string sGType = GetTypeSimpleGTable();
            CnxBase ocnx = Owner.Owner.oCnxBase;


            Table t;
            int n = ocnx.ConfDB.FindTable(sType);
            if (n > -1)
            {
                t = (Table)ocnx.ConfDB.LstTable[n];
                Select = "SELECT " + t.GetSelectField(ConfDataBase.FieldOption.InterneBD);
                From = GetSimpleFrom(sType);
                Where = GetSimpleWhere(sType, GuidObjet);
                if (ocnx.CBRecherche(Select + " " + From + " " + Where))
                {
                    if (ocnx.Reader.Read())
                    {
                        CreatObjetsFromBD(true, ConfDataBase.FieldOption.Select);
                    }
                    else ocnx.CBReaderClose();
                }
                else ocnx.CBReaderClose();
            }
        }
        public override void LoadObjectSansGraph( string Where="")
        {

            string Select, From;
            string sType = GetTypeSimpleTable();
            CnxBase ocnx = Owner.Owner.oCnxBase;
            Table t;

            int n = ocnx.ConfDB.FindTable(sType);
            if (n > -1)
            {
                t = (Table)ocnx.ConfDB.LstTable[n];
                Select = "Select " + t.GetSelectFieldFromOption(ConfDataBase.FieldOption.InterneBD);
                From = "From Vlan";
                if (ocnx.CBRecherche(Select + " " + From))
                {
                    while (ocnx.Reader.Read())
                    {
                        CreatObjetsFromBDSansGraph(false, t);
                    }
                    ocnx.CBReaderClose();
                }
                else ocnx.CBReaderClose();
            }
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawVLan dv;

            dv = new DrawVLan(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(dv);
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawVLan dv;
            bool selected = false;

            dv = new DrawVLan(Owner.Owner, LstValue, LstValueG);
            if (dv.rectangle.X == 0) selected = true;
            AddNewObject(Owner.Owner.drawArea, dv, selected);
        }

        public override void ExpandObj(FormExplorObj feo, ExpObj eo)
        {
            //MessageBox.Show(eo.tn.Parent.Text);
            CnxBase ocnx = Owner.Owner.oCnxBase;

            switch (eo.iCat)
            {
                case -1:
                    ExpandObjRoot(0, ssCat.Length - 1, feo, eo, ssCat);
                    break;
                case 0: //Applications
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Application.GuidApplication, NomApplication FROM Application, AppVersion, Vue, DansVue, GVLan, VLan WHERE Application.GuidApplication = AppVersion.GuidApplication AND AppVersion.GuidAppVersion = Vue.GuidAppVersion AND Vue.GuidGVue = DansVue.GuidGVue AND DansVue.GuidObjet = GVLan.GuidGVLan AND GVLan.GuidVLan = VLan.GuidVLan AND VLan.GuidVLan='" + eo.GuidObj.ToString() + "' Order By NomApplication", eo.tn, DrawArea.DrawToolType.Application);
                    break;
                case 1: // ServerPhy
                    //ocnx.CBAddNodeObjExp(lstObj, "SELECT DISTINCT Fonction.GuidFonction, NomFonction FROM Fonction, Server, ServerLink WHERE Fonction.GuidFonction=Server.GuidMainFonction AND Server.GuidServer=ServerLink.GuidServer AND GuidServerPhy='" + eo.GuidObj.ToString() + "' Order By NomFonction", eo.tn, DrawArea.DrawToolType.Fonction);
                    break;
            }
        }
    }
}
