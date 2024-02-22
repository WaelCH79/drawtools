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
    public class ToolTechno : DrawTools.ToolRectangle
	{
        public static string[] ssCat = { "Application", "Composant", "Module en Entree", "Module en Sortie" };
        public ToolTechno(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            InitPropriete("9fcc491b-558f-44d2-a846-91d96b3c6f46");
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawTechno dt;
            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                for (int i = 0; i < drawArea.GraphicsList.Count; i++)
                {
                    if (drawArea.GraphicsList[i].GetType() == typeof(DrawServerType) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                    {
                        DrawServerType dstInit = (DrawServerType)drawArea.GraphicsList[i];
                        string sGuid = (string)dstInit.GetValueFromName("GuidServerType");
                        int j = 0;
                        j = Owner.GraphicsList.FindObjetFromValue(j, 0, sGuid);
                        while (j != -1)
                        {
                            DrawServerType dst = (DrawServerType)drawArea.GraphicsList[j];
                            dt = new DrawTechno(drawArea.Owner);
                            dt.GuidkeyObjet = Guid.NewGuid();
                            dst.AttachLink(dt, DrawObject.TypeAttach.Child);
                            dt.AttachLink(dst, DrawObject.TypeAttach.Parent);
                            dst.AligneObjet();
                            dt.Normalize();
                            AddNewObject(Owner.Owner.drawArea, dt, true);
                            j += 2;
                            j = Owner.GraphicsList.FindObjetFromValue(j, 0, sGuid);
                        }
                        break;
                    }
                    else if (drawArea.GraphicsList[i].GetType() == typeof(DrawManagedsvc) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                    {
                        DrawManagedsvc dmsInit = (DrawManagedsvc)drawArea.GraphicsList[i];
                        string sGuid = (string)dmsInit.GetValueFromName("GuidManagedsvc");
                        int j = 0;
                        j = Owner.GraphicsList.FindObjetFromValue(j, 0, sGuid);
                        while (j != -1)
                        {
                            DrawManagedsvc dms = (DrawManagedsvc)drawArea.GraphicsList[j];
                            dt = new DrawTechno(drawArea.Owner);
                            dt.GuidkeyObjet = Guid.NewGuid();
                            dms.AttachLink(dt, DrawObject.TypeAttach.Child);
                            dt.AttachLink(dms, DrawObject.TypeAttach.Parent);
                            dms.AligneObjet();
                            dt.Normalize();
                            AddNewObject(Owner.Owner.drawArea, dt, true);
                            j += 2;
                            j = Owner.GraphicsList.FindObjetFromValue(j, 0, sGuid);
                        }
                        break;
                    }
                    else if (drawArea.GraphicsList[i].GetType() == typeof(DrawContainer) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                    {
                        DrawContainer dcInit = (DrawContainer)drawArea.GraphicsList[i];
                        string sGuid = (string)dcInit.GetValueFromName("GuidContainer");
                        int j = 0;
                        j = Owner.GraphicsList.FindObjetFromValue(j, 0, sGuid);
                        while (j != -1)
                        {
                            DrawContainer dc = (DrawContainer)drawArea.GraphicsList[j];
                            dt = new DrawTechno(drawArea.Owner);
                            dt.GuidkeyObjet = Guid.NewGuid();
                            dc.AttachLink(dt, DrawObject.TypeAttach.Child);
                            dt.AttachLink(dc, DrawObject.TypeAttach.Parent);
                            dc.AligneObjet();
                            dt.Normalize();
                            AddNewObject(Owner.Owner.drawArea, dt, true);
                            j += 2;
                            j = Owner.GraphicsList.FindObjetFromValue(j, 0, sGuid);
                        }
                        break;
                    }
                    else if (drawArea.GraphicsList[i].GetType() == typeof(DrawTechUser) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                    {
                        DrawTechUser dtu = (DrawTechUser)drawArea.GraphicsList[i];
                        dt = new DrawTechno(drawArea.Owner);

                        dtu.AttachLink(dt, DrawObject.TypeAttach.Child);
                        dt.AttachLink(dtu, DrawObject.TypeAttach.Parent);
                        dtu.AligneObjet();
                        dt.Normalize();
                        AddNewObject(Owner.Owner.drawArea, dt, true);

                        break;
                    }
                }
            }
            
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
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

        //--------------------------------- Application ------------------------------------------
        //public ArrayList DefauftNivForAppQuery(string GuidObj) { return LstQuery("SELECT DISTINCT TechnoRef.GuidTechnoRef, TechnoRef.NomTechnoRef, ServerPhy.GuidServerPhy FROM Vue, DansVue, ServerPhy, GServerPhy, ServerLink, Server, ServerTypeLink, ServerType, Techno, TechnoRef WHERE Vue.GuidVue=DansVue.GuidVue and GuidObjet=GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and ServerPhy.GuidServerPhy=ServerLink.GuidServerPhy and ServerLink.GuidServer=Server.GuidServer and Server.GuidServer=ServerTypeLink.GuidServer and ServerTypeLink.GuidServerType=ServerType.GuidServerType and ServerType.GuidServerType=Techno.GuidServerType and Techno.GuidTechnoRef=TechnoRef.GuidTechnoRef and GuidApplication='" + GuidObj + "'"); }
        //"SELECT DISTINCT TechnoRef.GuidTechnoRef, NomTechnoRef From Vue, DansVue, GServer, Server, ServerTypeLink, ServerType, Techno, TechnoRef WHERE	Vue.GuidVue=DansVue.GuidVue and DansVue.GuidObjet=GuidGServer and GServer.GuidServer=Server.GuidServer and Server.GuidServer = ServerTYpeLink.GuidServer and ServerTypeLink.GuidServerType = ServerType.GuidServerType and ServerType.GuidServerType=Techno.GuidServerType and TechnoRef.GuidTechnoRef=Techno.GuidTechnoRef and GuidApplication='5cdd06d9-4a1a-4295-a633-9ecee20d8a21'"
        public ArrayList DefauftNivForAppQuery(string GuidObj, bool bOption)
        {
            if (bOption)
                // en tenant compte des application externe
                return LstQuery("SELECT DISTINCT TechnoRef.GuidTechnoRef, TechnoRef.NomTechnoRef FROM Application, Vue, DansVue, ServerPhy, GServerPhy, ServerLink, Server, ServerTypeLink, ServerType, Techno, TechnoRef WHERE Vue.GuidGVue=DansVue.GuidGVue and GuidObjet=GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and ServerPhy.GuidServerPhy=ServerLink.GuidServerPhy and ServerLink.GuidServer=Server.GuidServer and Server.GuidServer=ServerTypeLink.GuidServer and ServerTypeLink.GuidServerType=ServerType.GuidServerType and ServerType.GuidServerType=Techno.GuidTechnoHost and Techno.GuidTechnoRef=TechnoRef.GuidTechnoRef and Vue.GuidAppVersion=Application.GuidAppVersion and GuidApplication='" + GuidObj + "'");
            else
                // en tenant compte uniquemement des techno de l'application
                return LstQuery("SELECT DISTINCT TechnoRef.GuidTechnoRef, NomTechnoRef From Application, Vue, DansVue, GServer, Server, ServerTypeLink, ServerType, Techno, TechnoRef WHERE Vue.GuidGVue=DansVue.GuidGVue and DansVue.GuidObjet=GuidGServer and GServer.GuidServer=Server.GuidServer and Server.GuidServer = ServerTYpeLink.GuidServer and ServerTypeLink.GuidServerType = ServerType.GuidServerType and ServerType.GuidServerType=Techno.GuidTechnoHost and TechnoRef.GuidTechnoRef=Techno.GuidTechnoRef and Vue.GuidAppVersion=Application.GuidAppVersion and GuidApplication='" + GuidObj + "'");
        }
        public override ArrayList CoutForAppQuery(string GuidObj, bool bOption) { return LstQueryCout("SELECT DISTINCT TechnoRef.GuidTechnoRef, TechnoRef.NomTechnoRef, CPUCore, RAM, CPUCoreA, RAMA FROM Application, Vue, DansVue, ServerPhy, GServerPhy, ServerLink, Server, ServerTypeLink, ServerType, Techno, TechnoRef WHERE Vue.GuidGVue=DansVue.GuidGVue and GuidObjet=GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and ServerPhy.GuidServerPhy=ServerLink.GuidServerPhy and ServerLink.GuidServer=Server.GuidServer and Server.GuidServer=ServerTypeLink.GuidServer and ServerTypeLink.GuidServerType=ServerType.GuidServerType and ServerType.GuidServerType=Techno.GuidTechnoHost and Techno.GuidTechnoRef=TechnoRef.GuidTechnoRef and Vue.GuidAppVersion=Application.GuidAppVersion and GuidApplication='" + GuidObj + "'"); }
        public override ArrayList FinCommercialForAppQuery(string GuidObj, bool bOption) { return DefauftNivForAppQuery(GuidObj, bOption); }
        public override ArrayList SupportForAppQuery(string GuidObj, bool bOption) { return DefauftNivForAppQuery(GuidObj, bOption); }
        public override ArrayList ComplexiteForAppQuery(string GuidObj, bool bOption) { return DefauftNivForAppQuery(GuidObj, bOption); }
        public override ArrayList ExpertiseForAppQuery(string GuidObj, bool bOption) { return DefauftNivForAppQuery(GuidObj, bOption); }
        public override ArrayList ObsolescenceForAppQuery(string GuidObj, bool bOption) { return DefauftNivForAppQuery(GuidObj, bOption); }
        public override ArrayList BusinessForAppQuery(string GuidObj, bool bOption) { return DefauftNivForAppQuery(GuidObj, bOption); }
        public override ArrayList InstanceForAppQuery(string GuidObj, bool bOption) { return DefauftNivForAppQuery(GuidObj, bOption); }
        public override ArrayList CriticiteForAppQuery(string GuidObj, bool bOption) { return DefauftNivForAppQuery(GuidObj, bOption); }
        public override ArrayList SecuriteForAppQuery(string GuidObj, bool bOption) { return DefauftNivForAppQuery(GuidObj, bOption); }
        public override ArrayList ImpactForAppQuery(string GuidObj, bool bOption) { return DefauftNivForAppQuery(GuidObj, bOption); }
        public override ArrayList ImpactBusinessForAppQuery(string GuidObj, bool bOption) { return DefauftNivForAppQuery(GuidObj, bOption); }

        //---------------------------- Server ---------------------------------
        public ArrayList DefauftNivForServerQuery(string GuidObj, bool bOption) { return LstQuery("SELECT DISTINCT TechnoRef.GuidTechnoRef, TechnoRef.NomTechnoRef FROM ServerLink, Server, ServerTypeLink, ServerType, Techno, TechnoRef WHERE ServerLink.GuidServer=Server.GuidServer and Server.GuidServer=ServerTypeLink.GuidServer and ServerTypeLink.GuidServerType=ServerType.GuidServerType and ServerType.GuidServerType=Techno.GuidTechnoHost and Techno.GuidTechnoRef=TechnoRef.GuidTechnoRef and GuidServerPhy='" + GuidObj + "'"); }
        public override ArrayList CoutForServerQuery(string GuidObj, bool bOption) { return DefauftNivForServerQuery(GuidObj, bOption); }
        public override ArrayList FinCommercialForServerQuery(string GuidObj, bool bOption) { return DefauftNivForServerQuery(GuidObj, bOption); }
        public override ArrayList SupportForServerQuery(string GuidObj, bool bOption) { return DefauftNivForServerQuery(GuidObj, bOption); }
        public override ArrayList ComplexiteForServerQuery(string GuidObj, bool bOption) { return DefauftNivForServerQuery(GuidObj, bOption); }
        public override ArrayList ExpertiseForServerQuery(string GuidObj, bool bOption) { return DefauftNivForServerQuery(GuidObj, bOption); }
        public override ArrayList ObsolescenceForServerQuery(string GuidObj, bool bOption) { return DefauftNivForServerQuery(GuidObj, bOption); }
        public override ArrayList BusinessForServerQuery(string GuidObj, bool bOption) { return DefauftNivForServerQuery(GuidObj, bOption); }
        public override ArrayList InstanceForServerQuery(string GuidObj, bool bOption) { return DefauftNivForServerQuery(GuidObj, bOption); }
        public override ArrayList CriticiteForServerQuery(string GuidObj, bool bOption) { return DefauftNivForServerQuery(GuidObj, bOption); }
        public override ArrayList SecuriteForServerQuery(string GuidObj, bool bOption) { return DefauftNivForServerQuery(GuidObj, bOption); }
        public override ArrayList ImpactForServerQuery(string GuidObj, bool bOption) { return DefauftNivForServerQuery(GuidObj, bOption); }
        public override ArrayList ImpactBusinessForServerQuery(string GuidObj, bool bOption) { return DefauftNivForServerQuery(GuidObj, bOption); }

        public override bool CalcIndicator(ObjectAndNiveau objniv, Niveau Niv)
        {
            ArrayList LstNiveau = new ArrayList();

            Owner.Owner.oCnxBase.SWwriteLog(12, "Creation de la liste des objets " + GetType().Name.Substring("Tool".Length) + " et de son Niveau par rapport à l'objet " + objniv.sGuid, true);
            Owner.Owner.oCnxBase.CBRecherche("SELECT DISTINCT TechnoRef.NomTechnoRef, IndicatorLink.ValIndicator FROM TechnoRef, IndicatorLink WHERE TechnoRef.GuidTechnoRef=IndicatorLink.GuidObjet and TechnoRef.GuidTechnoRef='" + objniv.sGuid + "' and GuidIndicator='" + Niv.GuidNiveau + "'");
            while (Owner.Owner.oCnxBase.Reader.Read())
            {
                Owner.Owner.oCnxBase.SWwriteLog(14, "Ajout dans la liste l'objet : " + Owner.Owner.oCnxBase.Reader.GetString(0) + "     (" + objniv.sGuid + ")", false);
                LstNiveau.Add(new ObjectAndNiveau(objniv.sGuid, Owner.Owner.oCnxBase.Reader.GetDouble(1), objniv.oParam));
            }
            Owner.Owner.oCnxBase.CBReaderClose();
            Owner.Owner.oCnxBase.SWwriteLog(12, "", true);
            if (LstNiveau.Count == 0) return false; else Niv.Calcul(LstNiveau);
            return true;
        }
        
        
        public override string GetSimpleFrom(string sTable)
        {
            return "FROM " + sTable + ",TechnoRef, IndicatorLink, Indicator";
        }

        
        public override string GetSimpleWhere(string sTable, string GuidObjet)
        {
            return "WHERE " + sTable + ".GuidTechnoRef=TechnoRef.GuidTechnoRef and TechnoRef.GuidTechnoRef=GuidObjet and Indicatorlink.GuidIndicator=Indicator.GuidIndicator and NomIndicator='1-Fin Support' and Guid" + sTable + "='" + GuidObjet + "'";
        }

        public override string GetSimpleSelect(Table t)
        {
            return "GuidTechno, TechnoRef.NomTechnoRef, TechnoRef.Version, ValIndicator, TechnoRef.Norme, TechnoRef.IndexImgOs, Techno.GuidTechnoRef, Techno.GuidTechnoHost";
        }

        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawTechno dt;

            dt = new DrawTechno(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dt;
            else {
                AddNewObject(Owner.Owner.drawArea, dt, false);
                //Owner.Owner.drawArea.GraphicsList.Add(dc);
                CreatObjetLink(dt, "GuidServerType", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            }
            return dt;
        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawTechno dt;
            bool selected = false;

            dt = new DrawTechno(Owner.Owner, LstValue, LstValueG);
            if (dt.rectangle.X == 0) selected = true;
            AddNewObject(Owner.Owner.drawArea, dt, selected);

            int n= CreatObjetLink(dt, "GuidServerType", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
            if (n > -1)
            {
                DrawServerType dst = (DrawServerType)Owner.GraphicsList[n];
                object o = dst.GetValueFromName("GuidServer");
                if (o != null && (string)o != "")
                {
                    n = Owner.GraphicsList.FindObjet(0, (string)o);
                    if (n > -1)
                    {
                        DrawServer ds = (DrawServer)Owner.GraphicsList[n];
                        ds.AligneObjet();
                    }
                }
            }
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
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Application.GuidApplication, NomApplication FROM Application, AppVersion, Vue, DansVue, GServer, Server, ServerTypeLink, ServerType, Techno WHERE Application.GuidApplication=AppVersion.GuidApplication And AppVersion.GuidAppVersion=Vue.GuidAppVersion AND Vue.GuidGVue=DansVue.GuidGVue AND DansVue.GuidObjet=GServer.GuidGServer AND GServer.GuidServer=Server.GuidServer AND Server.GuidServer=ServerTypeLink.GuidServer AND ServerTypeLink.GuidServerType=ServerType.GuidServerType AND ServerType.GuidServerType=Techno.GuidTechnoHost AND Techno.GuidTechnoRef='" + eo.GuidObj.ToString() + "'" +
                        " union " +
                        "SELECT DISTINCT Application.GuidApplication, NomApplication FROM Application, AppVersion, Vue, DansVue, ggenpod, genpod, containerlink, container, Techno WHERE Application.GuidApplication = AppVersion.GuidApplication And AppVersion.GuidAppVersion = Vue.GuidAppVersion AND Vue.GuidGVue = DansVue.GuidGVue AND DansVue.GuidObjet = Ggenpod.GuidGgenpod AND Ggenpod.Guidgenpod = genpod.Guidgenpod AND genpod.Guidgenpod = containerLink.Guidgenpod AND containerlink.GuidContainer = container.Guidcontainer AND container.Guidcontainer = Techno.GuidTechnoHost AND Techno.GuidTechnoRef = '" + eo.GuidObj.ToString() + "'" +
                        " union " +
                        "SELECT DISTINCT Application.GuidApplication, NomApplication FROM Application, AppVersion, Vue, DansVue, ggensas, gensas, svcservertypelink, ServerType, Techno WHERE Application.GuidApplication = AppVersion.GuidApplication And AppVersion.GuidAppVersion = Vue.GuidAppVersion AND Vue.GuidGVue = DansVue.GuidGVue AND DansVue.GuidObjet = Ggensas.GuidGgensas AND Ggensas.Guidgensas = gensas.Guidgensas AND gensas.GuidAppVersion = appversion.GuidAppVersion AND gensas.Guidgensas = svcservertypelink.Guidgensas AND svcservertypelink.GuidServerType = ServerType.GuidServerType AND ServerType.GuidServerType = Techno.GuidTechnoHost AND Techno.GuidTechnoRef = '" + eo.GuidObj.ToString() + "'" +
                        " union " +
                        "SELECT distinct Application.GuidApplication, NomApplication FROM Application, AppVersion, Vue, DansVue, ggensas, gensas, managedsvclink, managedsvc, techno WHERE Application.GuidApplication = AppVersion.GuidApplication And AppVersion.GuidAppVersion = Vue.GuidAppVersion AND Vue.GuidGVue = DansVue.GuidGVue AND DansVue.GuidObjet = Ggensas.GuidGgensas AND   Ggensas.Guidgensas = gensas.Guidgensas AND gensas.GuidAppVersion = appversion.GuidAppVersion AND gensas.Guidgensas = managedsvclink.GuidGensas AND managedsvclink.GuidManagedsvc = managedsvc.GuidManagedsvc AND managedsvc.GuidManagedsvc = techno.GuidTechnoHost AND techno.GuidTechnoRef ='"  + eo.GuidObj.ToString() + "'"
                        , eo.tn, DrawArea.DrawToolType.Application);
                    break;
                case 1: // Composant
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT MainComposant.GuidMainComposant, NomMainComposant FROM Module, MainComposant WHERE MainComposant.GuidMainComposant=Module.GuidMainComposant AND Module.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.MainComposant);
                    break;
                case 2: // Module en Entree
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Module.GuidModule, Module.NomModule FROM Module m, Link, Module WHERE m.GuidModule=Link.GuidModuleOut AND Link.GuidModuleIn=Module.GuidModule AND m.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Module);
                    break;
                case 3: // Module en Sortie
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Module.GuidModule, Module.NomModule FROM Module m, Link, Module WHERE m.GuidModule=Link.GuidModuleIn AND Link.GuidModuleOut=Module.GuidModule AND m.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Module);
                    break;
            }
        }
	}
}
