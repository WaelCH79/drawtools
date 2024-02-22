using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
    public class ToolInfNCard : DrawTools.ToolRectangle
	{
        public ToolInfNCard(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            InitPropriete("7284655e-3e35-40b1-88f5-98d2557f3c74");
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
        }
        
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            //drawArea.Owner.SetStateOfControls();
        }

        public override string GetTypeSimpleTable()
        {
            return "NCard";
        }

        public override string GetTypeSimpleGTable()
        {
            return "GNCard";
        }

        public override string GetWhere(string sType, string sGType, string GuidGVue, string sGuidVueSrvPhy)
        {
            return "WHERE GuidGVue ='" + GuidGVue + "' and GuidObjet=Guid" + sGType + " and " + sGType + ".Guid" + sType + "=" + sType + ".Guid" + sType + " ORDER BY " + sType + ".Nom" + sType;
        }

        public override void LoadObject(char TypeData, string sGuidgvue, string sData)
        {
            string Select, From, Where;
            string sType = GetTypeSimpleTable();
            string sGType = GetTypeSimpleGTable();
            //string sGType = GetTypeSimpleTable();

            CnxBase ocnx = Owner.Owner.oCnxBase;

            Select = GetSelect(sType, sGType);
            From = GetFrom(sType, sGType);
            Where = GetWhere(sType, sGType, sGuidgvue, null);
            if (ocnx.CBRecherche(Select + " " + From + " " + Where))
            {
                while (ocnx.Reader.Read())
                {
                    CreatObjetsFromBD(false, ConfDataBase.FieldOption.Select);
                }
                ocnx.CBReaderClose();
            }
            else ocnx.CBReaderClose();
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawInfNCard dnc;
            int n;
            bool selected = false;

            dnc = new DrawInfNCard(Owner.Owner, LstValue, LstValueG);
            if (dnc.rectangle.X == 0) selected = true;

            n = CreatObjetLink(dnc, "GuidHote", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            if (n > -1)
            {
                DrawInfServer ds = (DrawInfServer)dnc.LstParent[0]; 
                dnc.rectangle.X = ds.Rectangle.X + dnc.AXE;
                dnc.rectangle.Y = ds.Rectangle.Y + ds.NbrInfNCard() * (dnc.EpaisseurCard+ dnc.AXE);
                dnc.rectangle.Width = ds.Rectangle.Width - 2 * dnc.AXE;
                dnc.rectangle.Height = dnc.EpaisseurCard;
            }
            AddNewObject(Owner.Owner.drawArea, dnc, selected);
        }
	}
}
