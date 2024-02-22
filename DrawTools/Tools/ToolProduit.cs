using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
    /// 
	public class ToolProduit : DrawTools.ToolRectangle
	{
        public ToolProduit(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
		}

        public bool CreateObjetFromMouse(DrawArea drawArea, int i, string sGuid, Point e)
        {
            DrawProduit dp;
            bool Create = false;

            LoadSimpleObject(sGuid);
            dp = (DrawProduit)Owner.GraphicsList[0];
            dp.rectangle.X = e.X; dp.rectangle.Y = e.Y;
            dp.Normalize();
            Create = true;

            return Create;
        }


        public override void LoadObject(char TypeData, string sGuidgvue, string sData)
        {
            string Select, From, Where;
            string sType = GetTypeSimpleTable();
            string sGType = GetTypeSimpleGTable();
            //string sGType = GetTypeSimpleTable();

            CnxBase ocnx = Owner.Owner.oCnxBase;
            ArrayList aGuidCadreREf = new ArrayList();

            Select = "SELECT CadreRef.GuidCadreRef ";
            From = GetFrom(sType, sGType);
            Where = GetWhere(sType, sGType, sGuidgvue, sData);
            if (ocnx.CBRecherche(Select + " " + From + " " + Where))
            {
                while (ocnx.Reader.Read()) aGuidCadreREf.Add(ocnx.Reader.GetString(0));
                ocnx.CBReaderClose();
            }
            else ocnx.CBReaderClose();

            Select = GetSelect(sType, sGType);
            for (int i = 0; i < aGuidCadreREf.Count; i++)
            {
                ocnx.CBRecherche(Select + " " + From + " " + Where + " AND CadreRef.GuidCadreRef='" + aGuidCadreREf[i] + "'");
                CreatObjetsFromBD(true, ConfDataBase.FieldOption.Select);
                ocnx.CBReaderClose();

                DrawProduit dp; // = (DrawCadreRefN)drawArea.GraphicsList[0];
                int n = Owner.Owner.drawArea.GraphicsList.FindObjet(0, (string)aGuidCadreREf[i]);
                dp = (DrawProduit)Owner.GraphicsList[n];
                dp.AligneObjet();
            }            
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawProduit dp;
            bool selected = false;

            dp = new DrawProduit(Owner.Owner, LstValue, LstValueG);
            if (dp.rectangle.X == 0) selected = true;
            AddNewObject(Owner.Owner.drawArea, dp, selected);

            //Owner.Owner.drawArea.GraphicsList.MoveLastToBack();


            //base.CreatObjetFromBD(From1 f, LstValue, LstValueG);
        }
	}
}
