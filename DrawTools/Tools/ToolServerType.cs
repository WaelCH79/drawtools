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
    
    public class ToolServerType : DrawTools.ToolRectangle
	{
        public static string[] ssCat = { "Application", "Composant", "Module en Entree", "Module en Sortie" };
        public ToolServerType(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            InitPropriete("73dc7f6f-7627-4db6-8f33-c2995907289b");
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            /*DrawServerType dst;
            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                for (int i = 0; i < drawArea.GraphicsList.Count; i++)
                {
                    if (drawArea.GraphicsList[i].GetType() == typeof(DrawServer) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                    {
                        DrawServer ds = (DrawServer)drawArea.GraphicsList[i];

                        dst = new DrawServerType(drawArea.Owner);

                        ds.AttachLink(dst, DrawObject.TypeAttach.Child);
                        dst.AttachLink(ds, DrawObject.TypeAttach.Parent);
                        ds.AligneObjet();
                        dst.Normalize();
                        AddNewObject(Owner.Owner.drawArea, dst, true);
                                               
                        break;
                    }
                    if (drawArea.GraphicsList[i].GetType() == typeof(DrawTechUser) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                    {
                        DrawTechUser dtu = (DrawTechUser)drawArea.GraphicsList[i];
                        dst = new DrawServerType(drawArea.Owner);

                        dtu.AttachLink(dst, DrawObject.TypeAttach.Child);
                        dst.AttachLink(dtu, DrawObject.TypeAttach.Parent);
                        dtu.AligneObjet();
                        dst.Normalize();
                        AddNewObject(Owner.Owner.drawArea, dst, true);

                        break;
                    }
                }
            }
            
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();*/
        }
                       
        /// <summary>
        /// Left mouse is released.
        /// New object is created and resized.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            //drawArea.Owner.SetStateOfControls();
        }

        public override string GetSimpleSelect(Table t) { return t.GetSelectField(ConfDataBase.FieldOption.InterneBD) + ", Fonction.NomFonction"; }
        public override string GetSimpleFrom(string sTable) { return base.GetSimpleFrom(sTable) + " Left Join Fonction On ServerType.GuidFonction=Fonction.GuidFonction"; }
        
        public override string GetFrom(string sType, string sGType)
        {
            return "From DansVue, " + sType + " Left Join LayerLink On " + sType + ".Guid" + sType + "=GuidObj" + " and layerlink.GuidAppVersion='" + Owner.Owner.GetGuidAppVersion() + "' Left Join Fonction On ServerType.GuidFonction=Fonction.GuidFonction, " + sGType;
        }
        public override void LoadObjectXml(XmlNode Node)
        {
            Table t, tg;
            ArrayList LstValue;
            ArrayList LstValueG;
            bool bServer = true;
            ArrayList aGNode = new ArrayList();
            ConfDataBase ConfDB = Owner.Owner.oCnxBase.ConfDB;
            string sTablen = GetTypeSimpleTable();
            string sTablem = GetTypeSimpleGTable();
            int n = ConfDB.FindTable(sTablen);
            int m = ConfDB.FindTable(sTablem);
            aGNode = Owner.Owner.GetNode(Node, "ServerTypeLink");
            if (aGNode.Count == 0) { aGNode = Owner.Owner.GetNode(Node, "AppUserTypeLink"); bServer = false; }

            if (n > -1 && m > -1)
            {
                t = (Table)ConfDB.LstTable[n];
                tg = (Table)ConfDB.LstTable[m];
                LstValue = t.InitValueFieldFromXmlNode(Node);

                        
                for (int i = 0; i < aGNode.Count; i++)
                {
                    LstValueG = tg.InitValueFieldFromXmlNode((XmlNode)aGNode[i]);
                    int idx = t.FindField(t.LstField, "GuidServer");
                    if (bServer) { if (idx > -1) LstValue[idx] = ((XmlElement)aGNode[i]).GetAttribute("sGuidServer"); }
                    else { if (idx > -1) LstValue[idx] = ((XmlElement)aGNode[i]).GetAttribute("sGuidAppUser"); }
                    CreatObjetFromXml(LstValue, LstValueG);
                }
            }
        }

        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawServerType st;

            st = new DrawServerType(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = st;
            else {
                AddNewObject(Owner.Owner.drawArea, st, false);
                //Owner.Owner.drawArea.GraphicsList.Add(dc);
                CreatObjetLink(st, "GuidServer", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);

            }
            return st;
        }

        public override void CreatObjetFromXml(ArrayList LstValue, ArrayList LstValueG)
        {
            DrawServerType dst;
            bool selected = false;
            int n;

            dst = new DrawServerType(Owner.Owner, LstValue, LstValueG);
            AddNewObject(Owner.Owner.drawArea, dst, selected);
            n = CreatObjetLink(dst, "GuidServer", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            if (n > -1)
            {
                DrawObject dob = (DrawObject)Owner.GraphicsList[n];
                if (dob.GetType().Name == "DrawServer") ((DrawServer)dob).AligneObjet();
                else ((DrawTechUser)dob).AligneObjet();
            }
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawServerType dst;
            bool selected = false;
            

            dst = new DrawServerType(Owner.Owner, LstValue, LstValueG);
            CreatObjetLink(dst, "GuidServer", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            CreatObjetLink(dst, "GuidAppUser", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            AddNewObject(Owner.Owner.drawArea, dst, selected);
            if (dst.rectangle.X == 0)
            {
                selected = true;
                ArrayList lstTechno = Owner.Owner.oCnxBase.CreatTechnoServer(dst);
                for (int i = 0; i < lstTechno.Count; i++)
                {
                    Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.Techno].LoadSimpleObject((string)lstTechno[i]);
                    int j = Owner.GraphicsList.FindObjet(0, (string)lstTechno[i]);
                    DrawTechno dt = (DrawTechno)Owner.GraphicsList[j];
                    dt.GuidkeyObjet = Guid.NewGuid(); //dt.SetValueFromName("GuidTechno", (object)dt.GuidkeyObjet.ToString());
                    dst.AttachLink(dt, DrawObject.TypeAttach.Child);
                    dt.AttachLink(dst, DrawObject.TypeAttach.Parent);
                }
            }
            //AddNewObject(Owner.Owner.drawArea, dst, selected);

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
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Application.GuidApplication, NomApplication FROM Application, AppVersion, Vue, DansVue, GServer, Server, ServerTypeLink WHERE Application.GuidApplication=AppVersion.GuidApplication And AppVersion.GuidAppVersion=Vue.GuidAppVersion AND Vue.GuidGVue=DansVue.GuidGVue AND DansVue.GuidObjet=GServer.GuidGServer AND GServer.GuidServer=Server.GuidServer AND Server.GuidServer=ServerTypeLink.GuidServer AND ServerTypeLink.GuidServerType='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Application);
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
        }
	}
}
