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
	public class ToolInfServer : DrawTools.ToolRectangle
	{
        public ToolInfServer(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            InitPropriete("dc541485-b4f9-4f6b-b2b6-26369cacd772");
		}

        
        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            /*Point pt= new Point(e.X, e.Y);
            DrawInfServer dm;

            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                if (drawArea.GraphicsList.Count == 0)
                {
                    LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);
                    dm = (DrawInfServer)Owner.GraphicsList[0];
                    dm.rectangle.X = e.X; dm.rectangle.Y = e.Y;
                    dm.Normalize();
                }
                else
                {
                    for (int i = 0; i < drawArea.GraphicsList.Count; i++)
                    {
                        if (CreateObjetFromMouse(drawArea, i, (string)Owner.Owner.tvObjet.SelectedNode.Name, pt)) break;
                    }
                }
            }*/
            
        }

        
        /// <summary>
        /// Left mouse is released.
        /// New object is created and resized.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            DrawInfServer dm = (DrawInfServer)drawArea.GraphicsList[0];
                        
            
        }

        

        public override string GetSimpleFrom(string sTable)
        {
            return "FROM " + sTable + ", Location, DiskClass, BackupClass, ExploitClass, TechnoRef";
            //return base.GetSimpleFrom("ServerPhy");
        }

        
        public override string GetSimpleWhere(string sTable, string GuidObjet)
        {
            return "WHERE Guid" + sTable + "='" + GuidObjet + "' AND " + sTable + ".GuidLocation=Location.GuidLocation" + " AND " + sTable + ".GuidDiskClass=DiskClass.GuidDiskClass" + " AND " + sTable + ".GuidBackupClass=BackupClass.GuidBackupClass" + " AND " + sTable + ".GuidExploitClass=ExploitClass.GuidExploitClass" + " AND " + sTable + ".GuidTechnoRef=TechnoRef.GuidTechnoRef";
            //return base.GetSimpleWhere("ServerPhy", GuidObjet);
        }

        public override string GetTypeSimpleTable()
        {
            return "ServerPhy";
        }

        /*public override string GetTypeSimpleGTable()
        {
            return "GServer";
        }*/

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

        public override string GetFrom(string sType, string sGType)
        {
            return "From DansVue, " + sType + ", " + sGType + ", Location, DiskClass, BackupClass, ExploitClass, TechnoRef, ServerLink, Server";
            //return base.GetFrom(sType);
        }

        public override string GetWhere(string sType, string sGType, string GuidGVue, string sGuidVueSrvPhy)
        {
            string sTypeInf = "Server";
            string w1 = "WHERE DansVue.GuidGVue ='" + GuidGVue + "' and GuidObjet=Guid" + sGType + " and " + sGType + ".Guid" + sTypeInf + "=" + sTypeInf + ".Guid" + sTypeInf + " AND " + sType + ".GuidLocation=Location.GuidLocation" + " AND " + sType + ".GuidDiskClass=DiskClass.GuidDiskClass" + " AND " + sType + ".GuidBackupClass=BackupClass.GuidBackupClass" + " AND " + sType + ".GuidExploitClass=ExploitClass.GuidExploitClass" + " AND " + sType + ".GuidTechnoRef=TechnoRef.GuidTechnoRef ";
            string w2 = " and Server.GuidServer=ServerLink.GuidServer and ServerPhy.GuidServerPhy=ServerLink.GuidServerPhy and ServerLink.GuidVue='" + sGuidVueSrvPhy + "'";
            //string w2 = " and Server.GuidServer=ServerLink.GuidServer and ServerPhy.GuidServerPhy=ServerLink.GuidServerPhy and ServerPhy.GuidServerPhy In (Select ServerPhy.GuidServerPhy From ServerPhy, DansTypeVue, Vue Where ServerPhy.GuidServerPhy=DansTypeVue.GuidObjet and DansTypeVue.GuidTypeVue=Vue.GuidTypeVue and GuidVue='" + sGuidVueSrvPhy + "')";
            //string w2 = " and Server.GuidServer=ServerLink.GuidServer and ServerPhy.GuidServerPhy=ServerLink.GuidServerPhy and ServerPhy.GuidServerPhy In (Select DISTINCT ServerPhy.GuidServerPhy From DansVue, ServerPhy, GServerPhy Where GuidObjet=GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and GuidVue='" + sGuidVueSrvPhy + "')";
            return w1 + w2;
            //return base.GetWhere(sType);
        }

        public override string GetsType(bool Reel)
        {
            if (Reel) return base.GetsType(Reel);
            return "ServerPhy";
        }

        
        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawInfServer dis;
            bool selected=false;

            dis = new DrawInfServer(Owner.Owner, LstValue, LstValueG);
            
            if (dis.rectangle.X == 0) selected = true;
            AddNewObject(Owner.Owner.drawArea, dis, selected);

            if (dis.rectangle.X == 0)
            {
                selected = true;
                ArrayList lstInfNCard = Owner.Owner.oCnxBase.CreatInfNCard(dis);
                for (int i = 0; i < lstInfNCard.Count; i++)
                {
                    Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.InfNCard].LoadSimpleObject((string)lstInfNCard[i]);
                    int j = Owner.GraphicsList.FindObjet(0, (string)lstInfNCard[i]);
                    DrawInfNCard dinc = (DrawInfNCard)Owner.GraphicsList[j];
                    dinc.GuidkeyObjet = Guid.NewGuid(); //meme que infserver
                    dis.AttachLink(dinc, DrawObject.TypeAttach.Child);
                    dinc.AttachLink(dis, DrawObject.TypeAttach.Parent);
                }
            }            
        }


	}
}
