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
	public class ToolModule : DrawTools.ToolRectangle
	{
        public static string[] ssCat = { "Application", "Composant", "Module en Entree", "Module en Sortie" }; 
		public ToolModule(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");

            //initialisation des proprietes
            oLayers = new LayerList[1];
            oLayers[0] = new LayerList("e3b11b1b-74e1-476d-a0b2-caede83933f4");
            oLayers[0].AddTemplate(Owner.Owner, "", Owner.Owner.sGuidTemplate);
            
            initContextMenu();
            mnuObj = new ContextMenu(new MenuItem[] { mnuProp, mnuDel });
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawModule dm;

            dm = new DrawModule(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);
            AddNewObject(drawArea, dm, true);
        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawModule dm;

            dm = new DrawModule(Owner.Owner, LstValue, LstValueG);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dm;
            else AddNewObject(Owner.Owner.drawArea, dm, false);

            //base.CreatObjetFromBD(From1 f, LstValue, LstValueG);

        }

        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawModule dm;

            dm = new DrawModule(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dm;
            else Owner.Owner.drawArea.GraphicsList.Add(dm);
            return dm;
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawModule dm;

            dm = new DrawModule(Owner.Owner, LstValue);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dm;
            else Owner.Owner.drawArea.GraphicsList.Add(dm);
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
                case 0: //Application
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Application.GuidApplication, NomApplication FROM Application, AppVersion, Vue, DansVue, GModule, Module WHERE Application.GuidApplication=AppVersion.GuidApplication And AppVersion.GuidAppVersion=Vue.GuidAppVersion AND Vue.GuidGVue=DansVue.GuidGVue AND DansVue.GuidObjet=GModule.GuidGModule AND GModule.GuidModule=Module.GuidModule AND Module.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Application);
                    break;
                case 1: // Composant
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT MainComposant.GuidMainComposant, NomMainComposant FROM Module, MainComposant WHERE MainComposant.GuidMainComposant=Module.GuidMainComposant AND Module.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.MainComposant);
                    break;
                case 2: // Module en Entree
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT AppUser.GuidAppUser, NomAppUser FROM Module, Link, User WHERE Module.GuidModule=Link.GuidModuleOut AND Link.GuidModuleIn=AppUser.GuidAppUser AND Module.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.AppUser);
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Module.GuidModule, Module.NomModule FROM Module m, Link, Module WHERE m.GuidModule=Link.GuidModuleOut AND Link.GuidModuleIn=Module.GuidModule AND m.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Module);
                    break;
                case 3: // Module en Sortie
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Module.GuidModule, Module.NomModule FROM Module m, Link, Module WHERE m.GuidModule=Link.GuidModuleIn AND Link.GuidModuleOut=Module.GuidModule AND m.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Module);
                    break;
            }

            //base.ExpandObj(eo);
        }
	}
}
