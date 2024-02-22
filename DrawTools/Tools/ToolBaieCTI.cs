using System;
using System.Windows.Forms;
using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
    public class ToolBaieCTI : DrawTools.ToolRectangle
	{
        public ToolBaieCTI(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            InitPropriete("af71182d-20c5-4e3e-ba76-a4d56cbb8662");
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawBaieCTI db;

            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);
                db = (DrawBaieCTI)Owner.GraphicsList[0];
                db.rectangle.X = e.X; db.rectangle.Y = e.Y;
                db.Normalize();
            }
            else
            {
                db = new DrawBaieCTI(drawArea.Owner, e.X, e.Y, 110, 294, drawArea.GraphicsList.Count);
                AddNewObject(drawArea, db, true);
            }

            /*drawArea.GraphicsList[0].Normalize();
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;

            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();*/
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
            DrawBaieCTI db;
            bool selected = false;

            db = new DrawBaieCTI(Owner.Owner, LstValue, LstValueG);
            if (db.rectangle.X == 0) selected = true;
            AddNewObject(Owner.Owner.drawArea, db, selected);
            //base.CreatObjetFromBD(From1 f, LstValue, LstValueG);
        }

	}

    
}
