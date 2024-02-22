using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Xml;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
    
    public class ToolManagedsvc : DrawTools.ToolRectangle
	{
        public static string[] ssCat = { "Application", "Composant", "Module en Entree", "Module en Sortie" };
        public ToolManagedsvc(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            oLayers = new LayerList[1];
            oLayers[0] = new LayerList("73dc7f6f-7627-4db6-8f33-c2995907289b");
            oLayers[0].AddTemplate(Owner.Owner, "", Owner.Owner.sGuidTemplate);
		}
                       
        public override void LoadObjectXml(XmlNode Node)
        {
            Table t, tg;
            ArrayList LstValue;
            ArrayList aGNode = new ArrayList();
            ConfDataBase ConfDB = Owner.Owner.oCnxBase.ConfDB;
            string sTablen = GetTypeSimpleTable();
            string sTablem = GetTypeSimpleGTable();
            int n = ConfDB.FindTable(sTablen);
            int m = ConfDB.FindTable(sTablem);
            
            if (n > -1 && m > -1)
            {
                t = (Table)ConfDB.LstTable[n];
                tg = (Table)ConfDB.LstTable[m];
                LstValue = t.InitValueFieldFromXmlNode(Node);

                /* issue  de l'objet servertype, a reecrire lorsqu'il sera necessaire de le faire       
                for (int i = 0; i < aGNode.Count; i++)
                {
                    LstValueG = tg.InitValueFieldFromXmlNode((XmlNode)aGNode[i]);
                    int idx = t.FindField("GuidServer");
                    if (bServer) { if (idx > -1) LstValue[idx] = ((XmlElement)aGNode[i]).GetAttribute("sGuidServer"); }
                    else { if (idx > -1) LstValue[idx] = ((XmlElement)aGNode[i]).GetAttribute("sGuidAppUser"); }
                    CreatObjetFromXml(LstValue, LstValueG);
                }
                */
            }
        }

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            
        }
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
        }

        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawManagedsvc dms;

            dms = new DrawManagedsvc(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dms;
            else {
                AddNewObject(Owner.Owner.drawArea, dms, false);
                //Owner.Owner.drawArea.GraphicsList.Add(dc);
                CreatObjetLink(dms, "GuidSGensas", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);

            }
            return dms;
        }

        public override void CreatObjetFromXml(ArrayList LstValue, ArrayList LstValueG)
        {
            DrawManagedsvc dms;
            bool selected = false;
            int n;

            dms = new DrawManagedsvc(Owner.Owner, LstValue, LstValueG);
            AddNewObject(Owner.Owner.drawArea, dms, selected);
            n = CreatObjetLink(dms, "GuidGensas", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            if (n > -1)
            {
                DrawObject dob = (DrawObject)Owner.GraphicsList[n];
                if (dob.GetType().Name == "DrawGensas") ((DrawGensas)dob).AligneObjet();
            }
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawManagedsvc dms;
            bool selected = false;
            

            dms = new DrawManagedsvc(Owner.Owner, LstValue, LstValueG);
            //CreatObjetLink(dms, "GuidGensas", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            AddNewObject(Owner.Owner.drawArea, dms, selected);
            if (dms.rectangle.X == 0)
            {
                selected = true;
                ArrayList lstTechno = Owner.Owner.oCnxBase.CreatTechnoServer(dms);
                for (int i = 0; i < lstTechno.Count; i++)
                {
                    Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.Techno].LoadSimpleObject((string)lstTechno[i]);
                    int j = Owner.GraphicsList.FindObjet(0, (string)lstTechno[i]);
                    DrawTechno dt = (DrawTechno)Owner.GraphicsList[j];
                    dt.GuidkeyObjet = Guid.NewGuid(); //dt.SetValueFromName("GuidTechno", (object)dt.GuidkeyObjet.ToString());
                    dms.AttachLink(dt, DrawObject.TypeAttach.Child);
                    dt.AttachLink(dms, DrawObject.TypeAttach.Parent);
                }
            }
            //AddNewObject(Owner.Owner.drawArea, dst, selected);

        }

        /*
        public override void ExpandObj(ArrayList lstObj, ExpObj eo)
        {
            
            CnxBase ocnx = Owner.Owner.oCnxBase;

            switch (eo.iCat)
            {
                case -1:
                    ExpandObjRoot(0, ssCat.Length - 1, lstObj, eo, ssCat);
                    break;
                case 0: //Application
                    ocnx.CBAddNodeObjExp(lstObj, "SELECT DISTINCT Application.GuidApplication, NomApplication FROM Application, AppVersion, Vue, DansVue, GServer, Server, ServerTypeLink WHERE Application.GuidApplication=AppVersion.GuidApplication And AppVersion.GuidAppVersion=Vue.GuidAppVersion AND Vue.GuidGVue=DansVue.GuidGVue AND DansVue.GuidObjet=GServer.GuidGServer AND GServer.GuidServer=Server.GuidServer AND Server.GuidServer=ServerTypeLink.GuidServer AND ServerTypeLink.GuidServerType='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Application);
                    break;
                case 1: // Composant
                    ocnx.CBAddNodeObjExp(lstObj, "SELECT DISTINCT MainComposant.GuidMainComposant, NomMainComposant FROM Module, MainComposant WHERE MainComposant.GuidMainComposant=Module.GuidMainComposant AND Module.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.MainComposant);
                    break;
                case 2: // Module en Entree
                    ocnx.CBAddNodeObjExp(lstObj, "SELECT DISTINCT AppUser.GuidAppUser, NomAppUser FROM Module, Link, User WHERE Module.GuidModule=Link.GuidModuleOut AND Link.GuidModuleIn=AppUser.GuidAppUser AND Module.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.AppUser);
                    ocnx.CBAddNodeObjExp(lstObj, "SELECT DISTINCT Module.GuidModule, Module.NomModule FROM Module m, Link, Module WHERE m.GuidModule=Link.GuidModuleOut AND Link.GuidModuleIn=Module.GuidModule AND m.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Module);
                    break;
                case 3: // Module en Sortie
                    ocnx.CBAddNodeObjExp(lstObj, "SELECT DISTINCT Module.GuidModule, Module.NomModule FROM Module m, Link, Module WHERE m.GuidModule=Link.GuidModuleIn AND Link.GuidModuleOut=Module.GuidModule AND m.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Module);
                    break;
            }
        }
        */
	}
}
