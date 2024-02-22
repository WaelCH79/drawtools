using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolInterface : DrawTools.ToolRectangle
	{
        public static string[] ssCat = { "Composant", "Module en Entree", "Module en Sortie" }; 
        public ToolInterface(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");

            //initialisation des proprietes
            oLayers = new LayerList[1];
            oLayers[0] = new LayerList("8da0d0c7-dadc-4d5e-a905-5fbfe2ef495c");
            oLayers[0].AddTemplate(Owner.Owner, "", Owner.Owner.sGuidTemplate);
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawInterface di;

            di = new DrawInterface(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);
            AddNewObject(drawArea, di, true);
        }

        public override string GetFrom(string sType, string sGType)
        {
            return "From DansVue, " + sType + " Left Join LayerLink On Guid" + sType + "=GuidObj and layerlink.GuidAppVersion='" + Owner.Owner.GetGuidAppVersion()  + "' Left Join ProduitApp On " + sType  + ".GuidProduitApp=ProduitApp.GuidProduitApp, " + sGType;
        }

        public override string GetWhere(string sType, string sGType, string GuidGVue, string sGuidVueSrvPhy)
        {
            return "WHERE GuidGVue ='" + GuidGVue + "' and GuidObjet=Guid" + sGType + " and " + sGType + ".Guid" + sType + "=" + sType + ".Guid" + sType;
        }

        public override string GetSimpleFrom(string sTable)
        {
            return "FROM " + sTable + ", ProduitApp";
        }

        public override string GetSimpleWhere(string sTable, string GuidObjet)
        {
            return "WHERE Guid" + sTable + "='" + GuidObjet + "' AND " + sTable + ".GuidProduitApp=ProduitApp.GuidProduitApp";
        }

        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawInterface di;

            di = new DrawInterface(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = di;
            else {
                AddNewObject(Owner.Owner.drawArea, di, false);
                //Owner.Owner.drawArea.GraphicsList.Add(dc);
                CreatObjetLink(di, "GuidMainComposant", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            }
            return di;
        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawInterface di;

            di = new DrawInterface(Owner.Owner, LstValue, LstValueG);
            AddNewObject(Owner.Owner.drawArea, di, false);

            CreatObjetLink(di, "GuidMainComposant", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            
            //base.CreatObjetFromBD(From1 f, LstValue, LstValueG);
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
                case 0: //Composant
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT MainComposant.GuidMainComposant, NomMainComposant FROM Interface, MainComposant WHERE Interface.GuidMainComposant=MainComposant.GuidMainComposant AND GuidInterface='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.MainComposant);
                    break;
                case 1: //Module en Entree
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT AppUser.GuidAppUser, NomAppUser FROM AppUser, Link WHERE GuidComposantL1In=GuidAppUser AND GuidComposantL1Out='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.AppUser);
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Application.GuidApplication, NomApplication FROM Application, Link WHERE GuidComposantL1In=GuidApplication AND GuidComposantL1Out='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Application);
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Composant.GuidComposant, NomComposant FROM Composant, Link WHERE GuidComposantL1In=GuidComposant AND GuidComposantL1Out='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Composant);
                    break;
                case 2: //Module en sortie
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT d.GuidComposant, NomComposant FROM Interface s, Composant d, Link WHERE GuidComposantL1Out=s.GuidInterface AND GuidComposantL1In=d.GuidComposant AND s.GuidInterface='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Composant);
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT d.GuidFile, NomFile FROM Interface s, File d, Link WHERE GuidComposantL1Out=s.GuidInterface AND GuidComposantL1In=d.GuidFile AND s.GuidInterface='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.File);
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT d.GuidBase, NomBase FROM Interface s, Base d, Link WHERE GuidComposantL1Out=s.GuidInterface AND GuidComposantL1In=d.GuidBase AND s.GuidInterface='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Base);
                    break;
            }

            //base.ExpandObj(eo);
        }



	}
}
