using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data.Odbc;


namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolMachineCTI : DrawTools.ToolRectangle
	{
        public ToolMachineCTI(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            InitPropriete("d5d02838-e840-4e51-b1af-9db50ad582e6");
		}


        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);

            drawArea.AddObjet = false;
            for (int i = 0; i < drawArea.GraphicsList.Count; i++)
            {
                if (CreateObjetFromMouse(drawArea, i, (string)Owner.Owner.tvObjet.SelectedNode.Name, pt)) break;
            }
        }


        public bool CreateObjetFromMouse(DrawArea drawArea, int i, string sGuid, Point e)
        {
            DrawMachineCTI dm;
            bool Create = false;

            
            if (drawArea.GraphicsList[i].GetType() == typeof(DrawBaiePhy) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
            {

                DrawBaiePhy dc = (DrawBaiePhy)drawArea.GraphicsList[i];
                LoadSimpleObject(sGuid);
                dm = (DrawMachineCTI)Owner.GraphicsList[0];
                dm.rectangle.X = dc.rectangle.X;
                dm.rectangle.Y = dc.rectangle.Y + dc.rectangle.Height;

                dc.AttachLink(dm, DrawObject.TypeAttach.Child);
                dm.AttachLink(dc, DrawObject.TypeAttach.Parent);
                dm.Normalize();
                Create = true;
            }

            if (drawArea.GraphicsList[i].GetType() == typeof(DrawDrawer) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
            {
                DrawDrawer dd = (DrawDrawer)drawArea.GraphicsList[i];
                LoadSimpleObject(sGuid);
                dm = (DrawMachineCTI)Owner.GraphicsList[0];
                dm.rectangle.X = dd.rectangle.X;
                dm.rectangle.Y = dd.rectangle.Y + dd.rectangle.Height;

                dd.AttachLink(dm, DrawObject.TypeAttach.Child);
                dm.AttachLink(dd, DrawObject.TypeAttach.Parent);
                dm.Normalize();
                Create = true;
            } 
            return Create;
        }

        public void CreateSrv(DrawArea drawArea, string Srv, Point pt, string OParent)
        {
            CreateObjetFromMouse(drawArea, drawArea.GraphicsList.FindObjet(0, OParent), Srv, pt);
        }

        public override string GetFrom(string sType, string sGType)
        {
            return "From DansVue, " + sType + ", " + sGType + ", Location, DiskClass, BackupClass, ExploitClass, TechnoRef";
        }

        public override string GetWhere(string sType, string sGType, string GuidGVue, string sGuidVueSrvPhy)
        {
            return "WHERE GuidGVue ='" + GuidGVue + "' and GuidObjet=Guid" + sGType + " and " + sGType + ".Guid" + sType + "=" + sType + ".Guid" + sType + " AND " + sType + ".GuidLocation=Location.GuidLocation" + " AND " + sType + ".GuidDiskClass=DiskClass.GuidDiskClass" + " AND " + sType + ".GuidBackupClass=BackupClass.GuidBackupClass" + " AND " + sType + ".GuidExploitClass=ExploitClass.GuidExploitClass" + " AND " + sType + ".GuidTechnoRef=TechnoRef.GuidTechnoRef";
        }


        public override string GetSimpleFrom(string sTable)
        {
            return "FROM " + sTable + ", Location, DiskClass, BackupClass, ExploitClass, TechnoRef";
        }

        public override string GetSimpleWhere(string sTable, string GuidObjet)
        {
            return "WHERE Guid" + sTable + "='" + GuidObjet + "' AND " + sTable + ".GuidLocation=Location.GuidLocation" + " AND " + sTable + ".GuidDiskClass=DiskClass.GuidDiskClass" + " AND " + sTable + ".GuidBackupClass=BackupClass.GuidBackupClass" + " AND " + sTable + ".GuidExploitClass=ExploitClass.GuidExploitClass" + " AND " + sTable + ".GuidTechnoRef=TechnoRef.GuidTechnoRef";
        }




        public override string GetTypeSimpleTable()
        {
            return "ServerPhy";
        }

        public override void LoadSimpleObject(string GuidObjet)
        {
            string Select, From, Where;
            string sType = GetTypeSimpleTable();
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
        

        public override string GetSelect(string sTable, string sGTable)
        {
            Table t, tg;
            ConfDataBase ConfDB = Owner.Owner.oCnxBase.ConfDB;
            int n = ConfDB.FindTable(sTable);
            int m = ConfDB.FindTable(sGTable);
            if (n > -1 && m > 1)
            {
                t = (Table)ConfDB.LstTable[n];
                tg = (Table)ConfDB.LstTable[m];
                return "SELECT " + t.GetSelectField(ConfDataBase.FieldOption.InterneBD) + ", " + tg.GetSelectField(ConfDataBase.FieldOption.Base);
            }

            return null;
        }

        public override string GetsType(bool Reel)
        {
            if (Reel) return base.GetsType(Reel);
            return "ServerPhy";
        }

        
        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawMachineCTI dm;
            bool selected=false;

            dm = new DrawMachineCTI(Owner.Owner, LstValue, LstValueG);
            CreatObjetLink(dm, "GuidBaiePhy", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            
            if (dm.rectangle.X == 0) selected = true;
            AddNewObject(Owner.Owner.drawArea, dm, selected);
                       
            //TreeNode[] ArrayTreeNode = Owner.Owner.tvObjet.Nodes.Find((string) LstValue[0], true);
            //if (ArrayTreeNode.Length == 1) ArrayTreeNode[0].Remove();
            
        }
	}
}
