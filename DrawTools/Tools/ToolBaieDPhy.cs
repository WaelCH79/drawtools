using System;
using System.Windows.Forms;
using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolBaieDPhy : DrawTools.ToolRectangle
	{
        public ToolBaieDPhy(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            InitPropriete("3e1dccdc-e172-491d-8423-d8c6a3b0d893");
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawBaieDPhy db;

            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);
                db = (DrawBaieDPhy)Owner.GraphicsList[0];
                db.rectangle.X = e.X; db.rectangle.Y = e.Y;
                db.Normalize();
                drawArea.GraphicsList[0].Normalize();
                drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;

                drawArea.Capture = false;
                drawArea.Refresh();
                drawArea.Owner.SetStateOfControls();
            }
        }

        public override string GetTypeSimpleTable()
        {
            return "Baie";
        }

        public override string GetSimpleFrom(string sTable)
        {
            return base.GetSimpleFrom("Baie");
        }

        public override string GetSimpleWhere(string sTable, string GuidObjet)
        {
            return base.GetSimpleWhere("Baie", GuidObjet);
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
                return "SELECT " + t.GetSelectField(ConfDataBase.FieldOption.Base) + ", " + tg.GetSelectField(ConfDataBase.FieldOption.Base);
            }

            return null;
        }

        public override string GetFrom(string sType, string sGType)
        {
            return "From DansVue, " + "Baie" + ", " + sGType;
            //return base.GetFrom(sType);
        }

        public override string GetWhere(string sType, string sGType, string GuidGVue, string sGuidVueSrvPhy)
        {
            return "WHERE GuidGVue ='" + GuidGVue + "' and GuidObjet=Guid" + sGType + " and " + sGType + ".Guid" + sType + "=" + sType + ".Guid" + sType;
            //return base.GetWhere(sType);
        }

        public override string GetsType(bool Reel)
        {
            if (Reel) return base.GetsType(Reel);
            return "Baie";
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawBaieDPhy db;

            db = new DrawBaieDPhy(Owner.Owner, LstValue, LstValueG);
            AddNewObject(Owner.Owner.drawArea, db, false);
            //base.CreatObjetFromBD(From1 f, LstValue, LstValueG);
        }

	}

    
}
