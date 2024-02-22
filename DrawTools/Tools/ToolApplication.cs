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
	public class ToolApplication : DrawTools.ToolRectangle
	{
        public static string[] ssCat = {"Applications", "Vue","Fonctions", "Applications en Entree (Vue Inf)", "Applications en Sortie (Vue Inf)", "Composants"};
        //public static MainMenu mnuMainObj;
        public ToolApplication() { }



        public ToolApplication(DrawArea da, string sGuidObjProp)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");

            //initialisation des proprietes
            oLayers = new LayerList[2];
            oLayers[0] = new LayerList("59be2b47-4e8b-450a-9d38-90d23318c899");
            oLayers[1] = new LayerList("1738eea9-4c56-4cda-9a66-ef67dcc9fbf7");
            oLayers[0].AddTemplate(Owner.Owner, "", Owner.Owner.sGuidTemplate);
            oLayers[1].AddTemplate(Owner.Owner, "", Owner.Owner.sGuidTemplate);

            initContextMenu();
            mnuObj = new ContextMenu(new MenuItem[] { mnuProp, mnuDel });
            
        }

        

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawApplication da;

            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                int idx = Owner.Owner.drawArea.GraphicsList.FindObjet(0, (string)Owner.Owner.tvObjet.SelectedNode.Name);
                if (idx == -1)
                {
                    LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);

                    da = (DrawApplication)Owner.GraphicsList[Owner.Owner.drawArea.GraphicsList.FindObjet(0, (string)Owner.Owner.tvObjet.SelectedNode.Name)];
                    da.rectangle.X = e.X; da.rectangle.Y = e.Y;
                    da.Normalize();
                }
                else
                {
                    Owner.Owner.drawArea.GraphicsList.UnselectAll();
                    Owner.Owner.drawArea.GraphicsList[idx].Selected = true;
                }
            }
            else
            {
                da = new DrawApplication(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);
                AddNewObject(drawArea, da, true);
            }
        }

        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawApplication da;

            da = new DrawApplication(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = da;
            else Owner.Owner.drawArea.GraphicsList.Add(da);
            return da;
        }

        //---------------------------- Server ---------------------------------
        public ArrayList DefauftNivForServerQuery(string GuidObj, bool bOption) { return LstQuery("SELECT Application.GuidApplication, NomApplication FROM Application, Vue, DansVue, GServerPhy, ServerPhy WHERE Application.GuidAppVersion=Vue.GuidAppVersion AND Vue.GuidGVue=DansVue.GuidGVue AND DansVue.GuidObjet=GServerPhy.GuidGServerPhy AND GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy AND ServerPhy.GuidServerPhy='" + GuidObj + "'"); }
        public override ArrayList CoutForServerQuery(string GuidObj, bool bOption) { return DefauftNivForServerQuery(GuidObj, bOption); }
        public override ArrayList FinCommercialForServerQuery(string GuidObj, bool bOption) { return DefauftNivForServerQuery(GuidObj, bOption); }
        public override ArrayList SupportForServerQuery(string GuidObj, bool bOption) { return DefauftNivForServerQuery(GuidObj, bOption); }
        public override ArrayList ComplexiteForServerQuery(string GuidObj, bool bOption) { return DefauftNivForServerQuery(GuidObj, bOption); }
        public override ArrayList ObsolescenceForServerQuery(string GuidObj, bool bOption) { return DefauftNivForServerQuery(GuidObj, bOption); }
        public override ArrayList BusinessForServerQuery(string GuidObj, bool bOption) { return DefauftNivForServerQuery(GuidObj, bOption); }
        public override ArrayList InstanceForServerQuery(string GuidObj, bool bOption) { return DefauftNivForServerQuery(GuidObj, bOption); }
        public override ArrayList CriticiteForServerQuery(string GuidObj, bool bOption) { return DefauftNivForServerQuery(GuidObj, bOption); }
        public override ArrayList SecuriteForServerQuery(string GuidObj, bool bOption) { return DefauftNivForServerQuery(GuidObj, bOption); }
        public override ArrayList ImpactForServerQuery(string GuidObj, bool bOption) { return DefauftNivForServerQuery(GuidObj, bOption); }
        public override ArrayList ExpertiseForServerQuery(string GuidObj, bool bOption) { return DefauftNivForServerQuery(GuidObj, bOption); }
        public override ArrayList ImpactBusinessForServerQuery(string GuidObj, bool bOption) { return DefauftNivForServerQuery(GuidObj, bOption); }


        //---------------------------- Techno ---------------------------------
        public ArrayList DefauftNivForTechnoQuery(string GuidObj, bool bOption) { return LstQuery("SELECT Application.GuidApplication, Application.NomApplication FROM Application, Vue, DansVue, GServer, Server, ServerTypeLink, Techno WHERE Application.GuidAppVersion=Vue.GuidAppVersion AND Vue.GuidGVue=DansVue.GuidGVue AND DansVue.GuidObjet=GServer.GuidGServer AND GServer.GuidServer=Server.GuidServer AND Server.GuidServer=ServerTypeLink.GuidServer AND ServerTypeLink.GuidServerType=Techno.GuidTechnoHost AND Techno.GuidTechnoRef='" + GuidObj + "'"); }
        public override ArrayList CoutForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivForTechnoQuery(GuidObj, bOption); }
        public override ArrayList FinCommercialForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivForTechnoQuery(GuidObj, bOption); }
        public override ArrayList SupportForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivForTechnoQuery(GuidObj, bOption); }
        public override ArrayList ComplexiteForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivForTechnoQuery(GuidObj, bOption); }
        public override ArrayList ExpertiseForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivForTechnoQuery(GuidObj, bOption); }
        public override ArrayList ObsolescenceForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivForTechnoQuery(GuidObj, bOption); }
        public override ArrayList BusinessForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivForTechnoQuery(GuidObj, bOption); }
        public override ArrayList InstanceForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivForTechnoQuery(GuidObj, bOption); }
        public override ArrayList CriticiteForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivForTechnoQuery(GuidObj, bOption); }
        public override ArrayList SecuriteForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivForTechnoQuery(GuidObj, bOption); }
        public override ArrayList ImpactForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivForTechnoQuery(GuidObj, bOption); }
        public override ArrayList ImpactBusinessForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivForTechnoQuery(GuidObj, bOption); }



        public override bool CalcIndicator(ObjectAndNiveau objniv, Niveau Niv)
        {
            ArrayList LstNiveau = new ArrayList();

            Owner.Owner.oCnxBase.SWwriteLog(12, "Creation de la liste des objets " + GetType().Name.Substring("Tool".Length) + " et de son Niveau par rapport à l'objet " + objniv.sGuid, true);
            Owner.Owner.oCnxBase.CBRecherche("SELECT NomApplication, IndicatorLink.ValIndicator FROM Application, IndicatorLink WHERE Application.GuidApplication=IndicatorLink.GuidObjet and Application.GuidApplication='" + objniv.sGuid + "' and GuidIndicator='" + Niv.GuidNiveau + "'");
            while (Owner.Owner.oCnxBase.Reader.Read())
            {
                Owner.Owner.oCnxBase.SWwriteLog(14, "Ajout dans la liste l'objet : " + Owner.Owner.oCnxBase.Reader.GetString(0) + "     (" + objniv.sGuid + ")", false);
                LstNiveau.Add(new ObjectAndNiveau(objniv.sGuid, Owner.Owner.oCnxBase.Reader.GetDouble(1), objniv.oParam));
            }
            Owner.Owner.oCnxBase.CBReaderClose();
            if (LstNiveau.Count == 0) return false; else Niv.Calcul(LstNiveau);
            return true;
        }

        
        

        public override string GetSimpleSelect(Table t) { return t.GetSelectField(ConfDataBase.FieldOption.InterneBD) + ", Arborescence.NomArborescence"; }
        public override string GetSimpleFrom(string sTable) { return base.GetSimpleFrom(sTable) + " Left Join CadreRefFonc On Application.GuidCadreRef=CadreRefFonc.GuidCadreRefFonc Left Join AppVersion On Application.GuidAppVersion = AppVersion.GuidAppVersion, Arborescence";  }
        //public override string GetSimpleWhere(string sTable, string GuidObjet) { return base.GetSimpleWhere(sTable, GuidObjet) + "and application.GuidArborescence=arborescence.GuidArborescence" + " AND " + sTable + ".GuidAppVersion = AppVersion.GuidAppVersion"; }
        public override string GetSimpleWhere(string sTable, string GuidObjet) { return " Where Application.GuidApplication='" + GuidObjet +  "' and application.GuidArborescence=arborescence.GuidArborescence"; }

        public override string GetFrom(string sType, string sGType)
        {
            return "From DansVue, " + sType + " Left Join LayerLink On " + sType + ".Guid" + sType + "=GuidObj" + " and layerlink.GuidAppVersion='" + Owner.Owner.GetGuidAppVersion()  + "' Left Join CadreRefFonc On Application.GuidCadreRef=CadreRefFonc.GuidCadreRefFonc Left Join AppVersion On Application.GuidAppVersion = AppVersion.GuidAppVersion, " + sGType + ", Arborescence";
            //return base.GetFrom(sType);
        }

        public override string GetWhere(string sType, string sGType, string GuidGVue, string sGuidVueSrvPhy)
        {
            return "WHERE GuidGVue ='" + GuidGVue + "' and GuidObjet=Guid" + sGType + " and " + sGType + ".Guid" + sType + "=" + sType + ".Guid" + sType + " AND " + sType + ".GuidArborescence=Arborescence.GuidArborescence" + Owner.Owner.wkApp.GetWhereLayer();
            //return base.GetWhere(sType);
        }

        public override void LoadObject(char typeData, string sGuidgvue, string sData)
        {
            string Select, From, Where;
            string sType = GetTypeSimpleTable();
            string sGType = GetTypeSimpleGTable();
            //string sGType = GetTypeSimpleTable();

            CnxBase ocnx = Owner.Owner.oCnxBase;

            Select = GetSelect(sType, sGType);
            From = GetFrom(sType, sGType);
            Where = GetWhere(sType, sGType, sGuidgvue, sData);
            if (ocnx.CBRecherche(Select + " " + From + " " + Where))
            {
                int nbr = 0;
                DrawApplication da = null;
                //get sGuidApplication (sData)
                if (typeData == 'A') da = (DrawApplication)Owner.GraphicsList[Owner.GraphicsList.FindObjet(0, sData)];
                while (ocnx.Reader.Read())
                {
                    DrawApplication dan = null;
                    int i = Owner.GraphicsList.FindObjet(0, ocnx.Reader.GetString(0));
                    if (i == -1)
                    {
                        CreatObjetsFromBD(false, ConfDataBase.FieldOption.Select);
                        if (typeData == 'A')
                        {
                            dan = (DrawApplication)Owner.GraphicsList[Owner.GraphicsList.FindObjet(0, ocnx.Reader.GetString(0))];
                            dan.rectangle.X = 0; dan.rectangle.Width = 0; dan.rectangle.Height = 0;
                            dan.rectangle.Y = da.Rectangle.Y + ++nbr * (dan.HEIGHTAPPLICATION + 3 * dan.AXE);
                            dan.rectangle.X = Math.Max(da.Rectangle.X + da.Rectangle.Width / 2, Owner.GraphicsList.GetXMax(dan.rectangle.Y) + 3 * dan.AXE);
                            dan.rectangle.Height = dan.HEIGHTAPPLICATION;
                            dan.rectangle.Width = dan.DrawGrpTxt(null, 1, 0, 0, 0, 0, Color.Black, Color.Transparent) + 4 * (dan.WIDTHAPPLICATION + dan.AXE);
                        }
                    }
                }
                ocnx.CBReaderClose();
            }
            else ocnx.CBReaderClose();
        }

        
        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            /*if (foexplor != null)
            {
                InitDatagrid(foexplor.dgvExpObj, LstValue, GetTypeSimpleTable());
                InitDatagrid(LstValue);
            }
            else*/
            {
                DrawApplication da;
                bool selected = false;

                da = new DrawApplication(Owner.Owner, LstValue, LstValueG);
                if (da.rectangle.X == 0) selected = true;
                if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = da;
                else AddNewObject(Owner.Owner.drawArea, da, selected);
            }
            //base.CreatObjetFromBD(From1 f, LstValue, LstValueG);
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {

            DrawApplication da;

            da = new DrawApplication(Owner.Owner, LstValue);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = da;
            else Owner.Owner.drawArea.GraphicsList.Add(da);
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
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT App.GuidApplication, App.NomApplication FROM Application App, AppVersion, Vue, DansVue, GApplication, Application WHERE App.GuidApplication = AppVersion.GuidApplication AND AppVersion.GuidAppVersion = Vue.GuidAppVersion AND Vue.GuidGVue = DansVue.GuidGVue AND DansVue.GuidObjet = GApplication.GuidGApplication AND GApplication.GuidApplication = Application.GuidApplication AND Application.GuidApplication ='" + eo.GuidObj.ToString() + "' Order by App.NomApplication", eo.tn, DrawArea.DrawToolType.Application);
                    break;
                case 1: //Vue
                    ocnx.CBAddNodeObjExp(feo, "SELECT Vue.GuidVue, NomVue FROM Vue, Application WHERE Application.GuidAppVersion=Vue.GuidAppVersion AND GuidApplication='" + eo.GuidObj.ToString() + "' Order By NomVue", eo.tn, DrawArea.DrawToolType.Vue);
                    break;
                case 2: //Fonctions
                    ocnx.CBAddNodeObjExp(feo, "SELECT Module.GuidModule, NomModule FROM Vue, DansVue, GModule, Module, Application WHERE Vue.GuidGVue=DansVue.GuidGVue AND DansVue.GuidObjet=GModule.GuidGModule AND GModule.GuidModule=Module.GuidModule AND GuidTypeVue='f3fa584d-ed55-4e14-831e-0d0d3acded8e' AND Vue.GuidAppVersion=Application.GuidAppVersion AND GuidApplication='" + eo.GuidObj.ToString() + "' Order By NomModule", eo.tn, DrawArea.DrawToolType.Module);
                    break;
                case 3: //Applications en Entree (Vue Inf)
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT App.GuidApplication, App.NomApplication FROM TechLink, Application App WHERE TechLink.GuidAppIn=App.GuidApplication AND GuidAppOut='" + eo.GuidObj.ToString() + "' Order By App.NomApplication", eo.tn, DrawArea.DrawToolType.Application);
                    break;
                case 4: // Applications en Sortie (Vue Inf)
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT App.GuidApplication, App.NomApplication FROM TechLink, Application App WHERE TechLink.GuidAppOut=App.GuidApplication AND GuidAppIn='" + eo.GuidObj.ToString() + "' Order By App.NomApplication", eo.tn, DrawArea.DrawToolType.Application);
                    break;
                case 5: // Composants
                    ocnx.CBAddNodeObjExp(feo, "SELECT MainComposant.GuidMainComposant, NomMainComposant FROM Vue, DansVue, GMainComposant, MainComposant, Application WHERE Vue.GuidGVue=DansVue.GuidGVue AND DansVue.GuidObjet=GMainComposant.GuidGMainComposant AND GMainComposant.GuidMainComposant=MainComposant.GuidMainComposant AND GuidTypeVue='49c88d3d-f32f-44fe-ad6c-35977c5b812e' AND Application.GuidAppVersion=Vue.GuidAppVersion AND GuidApplication='" + eo.GuidObj.ToString() + "' Order By NomMainComposant", eo.tn, DrawArea.DrawToolType.MainComposant);
                    break;
            }
            
            //base.ExpandObj(eo);
        }
	}
}
