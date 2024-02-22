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
	public class ToolMachine : DrawTools.ToolRectangle
	{
		public ToolMachine(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            InitPropriete("002669bf-0300-48e1-ba2f-3ba175284313");
		}

        public bool CreateObjetFromMouse(DrawArea drawArea, int i, string sGuid, Point e)
        {
            DrawMachine dm;
            bool Create = false;

            if (drawArea.GraphicsList[i].GetType() == typeof(DrawCluster) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                {
                    DrawCluster dc = (DrawCluster)drawArea.GraphicsList[i];
                    LoadSimpleObject(sGuid);
                    dm = (DrawMachine)Owner.GraphicsList[0];
                    dc.AttachLink(dm, DrawObject.TypeAttach.Child);
                    dm.AttachLink(dc, DrawObject.TypeAttach.Parent);
                    dm.rectangle.X = e.X; dm.rectangle.Y = e.Y;
                    dm.Normalize();
                    Create = true;
                }
            else if (drawArea.GraphicsList[i].GetType() == typeof(DrawMachine) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                {
                    DrawMachine dmp = (DrawMachine)drawArea.GraphicsList[i];
                    LoadSimpleObject(sGuid);
                    dm = (DrawMachine)Owner.GraphicsList[0];

                    dmp.AttachLink(dm, DrawObject.TypeAttach.Child);
                    dm.AttachLink(dmp, DrawObject.TypeAttach.Parent);
                    dm.rectangle.X = e.X; dm.rectangle.Y = e.Y;
                    dm.Normalize();
                    Create = true;
                }
                else
                {
                    LoadSimpleObject(sGuid);
                    dm = (DrawMachine)Owner.GraphicsList[0];
                    dm.rectangle.X = e.X; dm.rectangle.Y = e.Y;
                    dm.Normalize();
                    Create = true;
                }
            return Create;
        }

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point pt= new Point(e.X, e.Y);
            DrawMachine dm;

            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                if (drawArea.GraphicsList.Count == 0)
                {
                    LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);
                    dm = (DrawMachine)Owner.GraphicsList[0];
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
            }
            else
            {
                dm = new DrawMachine(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);

                AddNewObject(drawArea, dm, true);

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
            DrawMachine dm = (DrawMachine)drawArea.GraphicsList[0];
                        
            dm.Normalize();
            for (int i = 1; i < drawArea.GraphicsList.Count; i++)
            {
                if (drawArea.GraphicsList[i].ParentPointInObject(new Point(dm.Rectangle.Left, dm.Rectangle.Top)) &
                    drawArea.GraphicsList[i].ParentPointInObject(new Point(dm.Rectangle.Right, dm.Rectangle.Bottom)))
                {
                    drawArea.GraphicsList[i].AttachLink(drawArea.GraphicsList[0], DrawObject.TypeAttach.Child);
                    drawArea.GraphicsList[0].AttachLink(drawArea.GraphicsList[i], DrawObject.TypeAttach.Parent);

                    break;
                }
            }

            ifCreat(drawArea, dm);
            dm.InitDatagrid(Owner.Owner.dataGrid);
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }

        public void ifCreat(DrawArea  drawArea, DrawMachine dm)
        {
            string Srv;

            Srv = dm.FindServerFromTv();
            if (Srv != null)
            {
                Owner.Owner.oCnxBase.CmdText = "";
                FormChangeProp fcp = new FormChangeProp(Owner.Owner, null);
                fcp.AddlSourceFromString(Srv);
                fcp.ShowDialog(Owner.Owner);
                if (fcp.Valider)
                {
                    string[] aValue;

                    aValue = Owner.Owner.oCnxBase.CmdText.Split('(', ')');
                    dm.NbrCreatChild = (aValue.Length - 1) / 2;
                    for (int i = 1; i < aValue.Length; i += 2)
                    {
                        if (aValue[i - 1].IndexOf("-P") != -1)
                        {
                            int x = 0, y = 0;
                            if (dm.rectangle.Height > dm.rectangle.Width)
                            {
                                x = dm.Rectangle.X + Marge;
                                y = dm.Rectangle.Y + Marge + (i - 1) / 2 * (dm.Rectangle.Height - 2 * Marge) / dm.NbrCreatChild;
                            }
                            else
                            {
                                x = dm.Rectangle.X + Marge + (i - 1) / 2 * (dm.Rectangle.Width - 2 * Marge) / dm.NbrCreatChild;
                                y = dm.Rectangle.Y + Marge;
                            }
                            CreateSrv(drawArea, aValue[i], new Point(x, y), dm.GuidkeyObjet.ToString());                            
                        }
                        else if (aValue[i - 1].IndexOf("-V") != -1)
                        {
                            ToolVirtuel tv = (ToolVirtuel)drawArea.tools[(int)DrawArea.DrawToolType.Virtuel];
                            tv.CreateSrv(drawArea, aValue[i], new Point(dm.Rectangle.X, dm.Rectangle.Y), dm.GuidkeyObjet.ToString());
                        }
                    }
                }
            }
        }

        public void CreateSrv(DrawArea drawArea, string Srv, Point pt, string OParent)
        {
            CreateObjetFromMouse(drawArea, drawArea.GraphicsList.FindObjet(0, OParent), Srv, pt);
            int n = drawArea.GraphicsList.FindObjet(0, Srv);
            if (n > -1)
            {
                DrawMachine dm = (DrawMachine)drawArea.GraphicsList[n];
                ifCreat(drawArea, dm);
            }
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
            DrawMachine dm;
            bool selected=false;
            int n;

            dm = new DrawMachine(Owner.Owner, LstValue, LstValueG);
            n = CreatObjetLink(dm, "GuidHost", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            if (n > -1 )
            {
                DrawObject dob = (DrawObject) Owner.GraphicsList[n];
                string sType = dob.GetType().Name.Substring("Tool".Length);
                if (sType == "Machine")
                {
                    DrawMachine dmp = (DrawMachine)dob;
                    if (dmp.NbrCreatChild > 0)
                    {
                        if (dmp.rectangle.Height > dmp.rectangle.Width)
                        {
                            dm.rectangle.Width = dmp.rectangle.Width - 2 * Marge;
                            dm.rectangle.Height = (dmp.rectangle.Height - 2 * Marge) / dmp.NbrCreatChild - 10;
                        }
                        else
                        {
                            dm.rectangle.Width = (dmp.rectangle.Width - 2 * Marge) / dmp.NbrCreatChild - 10;
                            dm.rectangle.Height = dmp.rectangle.Height - 2 * Marge;
                        }
                    }
                }
                else if (sType == "Cluster")
                {
                    DrawCluster dc = (DrawCluster)dob;
                    if (dc.NbrCreatChild > 0)
                    {
                        if (dc.rectangle.Height > dc.rectangle.Width)
                        {
                            dm.rectangle.Width = dc.rectangle.Width - 2 * Marge;
                            dm.rectangle.Height = (dc.rectangle.Height - 2 * Marge) / dc.NbrCreatChild - 10;

                        }
                        else
                        {
                            dm.rectangle.Width = (dc.rectangle.Width - 2 * Marge) / dc.NbrCreatChild - 10;
                            dm.rectangle.Height = dc.rectangle.Height - 2 * Marge;
                        }
                    }
                }
            }

            if (dm.rectangle.X == 0) selected = true;
            AddNewObject(Owner.Owner.drawArea, dm, selected);
                       
            //TreeNode[] ArrayTreeNode = Owner.Owner.tvObjet.Nodes.Find((string) LstValue[0], true);
            //if (ArrayTreeNode.Length == 1) ArrayTreeNode[0].Remove();
            
        }


	}
}
