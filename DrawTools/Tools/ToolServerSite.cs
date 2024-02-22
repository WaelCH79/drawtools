using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
    public class ToolServerSite : DrawTools.ToolRectangle
	{
        public ToolServerSite(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            InitPropriete("c94a8fc9-a698-41d0-93fa-afdf391d3a52");
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

        public bool CreateObjetFromMouse(DrawArea drawArea, int i, string sGuid, Point e)
        {
            DrawServerSite dss;
            bool Create = false;

            if (drawArea.GraphicsList[i].GetType() == typeof(DrawLocation) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
            {
                DrawLocation ds = (DrawLocation)drawArea.GraphicsList[i];
                LoadSimpleObject(sGuid);

                int j = Owner.GraphicsList.FindObjet(0, sGuid);
                dss = (DrawServerSite)Owner.GraphicsList[j];
                dss.rectangle.X = e.X; dss.rectangle.Y = e.Y;

                ds.AttachLink(dss, DrawObject.TypeAttach.Child);
                dss.AttachLink(ds, DrawObject.TypeAttach.Parent);
                dss.Normalize();
                Create = true;
            }
            return Create;
        }

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);

            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                int nbr = drawArea.GraphicsList.Count;
                for (int i = 0; i < nbr; i++)
                {
                    if (CreateObjetFromMouse(drawArea, i, (string)Owner.Owner.tvObjet.SelectedNode.Name, pt)) break;
                }
                if (nbr == drawArea.GraphicsList.Count)
                {
                    drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
                    drawArea.Capture = false;
                    drawArea.Refresh();
                    drawArea.Owner.SetStateOfControls();
                }
            }
            
            //drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            //drawArea.Capture = false;
            //drawArea.Refresh();
            //drawArea.Owner.SetStateOfControls();
        } 
       
        public void CreateSrv(DrawArea drawArea, string Srv, Point pt, string OParent)
        {
            CreateObjetFromMouse(drawArea, drawArea.GraphicsList.FindObjet(0, OParent), Srv, pt);
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

        
        public override string GetsType(bool Reel)
        {
            if (Reel) return base.GetsType(Reel);
            return "ServerPhy";
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawServerSite dss;
            bool selected = false;

            dss = new DrawServerSite(Owner.Owner, LstValue, LstValueG);
            if (dss.rectangle.X == 0) selected = true;
            AddNewObject(Owner.Owner.drawArea, dss, selected);

            CreatObjetLink(dss, "GuidLocation", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
        }

	}
}
