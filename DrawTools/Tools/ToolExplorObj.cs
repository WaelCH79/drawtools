using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace DrawTools
{

    public class ToolVue : DrawTools.ToolObject
    {
        public static string[] ssCat = { "Fonction", "Utilisateur", "Application", "Composant", "Module", "Interface", "Base", "Fichier", "Utilisateur", "Serveur", "Composant App", "Composant Tech", "Site", "Cluster", "Server", "Vlan", "Router" };
        //                                 0         1              2              3            4         5            6       7          8              9          10               11                12      13         14        15      16        17        18      19        20        21      22
        public static int iDeltaVue;
        public ToolVue(DrawArea da)
        {
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");

            initContextMenu();
            mnuObj = new ContextMenu(new MenuItem[] { mnuProp, mnuDel });
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawVue dv;

            dv = new DrawVue(Owner.Owner, LstValue);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dv;
            else Owner.Owner.drawArea.GraphicsList.Add(dv);
        }
                
        public override void CreatObjetFromBDtoList(ArrayList LstValue)
        {
            DrawVue dv;

            dv = new DrawVue(Owner.Owner, LstValue);
            Owner.Owner.lstObject.Add(dv);
        }


        public override string GetSimpleSelect(Table t) { return t.GetSelectField(ConfDataBase.FieldOption.InterneBD); }
        public override string GetSimpleFrom(string sTable) { return base.GetSimpleFrom(sTable) + " Left Join Ecosystem On Vue.GuidEcosystem=Ecosystem.GuidEcosystem"; }
        
        public override void ExpandObj(FormExplorObj feo, ExpObj eo)
        {
            int depart = 0, fin = 0;
            //MessageBox.Show(eo.tn.Parent.Text);
            CnxBase ocnx = Owner.Owner.oCnxBase;

            switch (eo.iCat)
            {
                case -1:
                    if (ocnx.CBRecherche("SELECT NomTypeVue FROM TypeVue, Vue WHERE TypeVue.GuidTypeVue=Vue.GuidTypeVue AND GuidVue='" + eo.GuidObj.ToString() + "'"))
                    {
                        ocnx.Reader.Read();
                        string sTypeVue = ocnx.Reader.GetString(0);
                        ocnx.CBReaderClose();

                        switch (sTypeVue[0])
                        {
                            case '0': //0-Fonctionnelle
                                depart = 0; fin = 1;
                                break;
                            case '1': // 1-Applicative
                                depart = 2; fin = 7;
                                break;
                            case '2': // 2-Infrastructure
                                depart = 8; fin = 11;
                                break;
                            case '6': // 6-Sites
                                depart = 12; fin = 12;
                                break;
                            case '3': // 3-Production
                            case '5': // 5-Pre-Production
                            case '4': // 4-Hors Production
                            case 'F': // F-Service Infra
                                depart = 13; fin = 16;
                                break;
                            case '8': // 8-ZoningProd
                            case '7': // 7-ZoningHorsProd
                            case 'A': // A-SanProd
                            case '9': // 9-SanHorsProd
                            case 'C': // C-CTIProd
                            case 'B': // B-CTIHorsProd
                            case 'D': // D-InfProd
                            case 'Y': // Y-Cadre Ref
                            case 'Z': // Z-SI Global
                                break;
                        }

                    }
                    ocnx.CBReaderClose();
                    ExpandObjRoot(depart, fin, feo, eo, ssCat);
                    /*for (int i = depart; i <= fin; i++)
                    {
                        Guid g = Guid.NewGuid();
                        TreeNode t = eo.tn.Nodes.Add(g.ToString(), ssCat[i]);
                        ExpObj eobj = new ExpObj(g, eo.GuidObj, eo.ObjTool, i, t);
                        lstObj.Add((object)eobj);
                    }*/
                    break;
                case 0: //Fonction
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Module.GuidModule, NomModule FROM Vue, DansVue, GModule, Module WHERE DansVue.GuidObjet=GModule.GuidGModule AND GModule.GuidModule=Module.GuidModule AND Vue.GuidVue='" + eo.GuidObj.ToString() + "' and Vue.GuidGVue=DansVue.GuidGVue ", eo.tn, DrawArea.DrawToolType.Module);
                    break;
                case 1: // Utilisateur
                    MessageBox.Show("Utilisateur");
                    //ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT MainComposant.GuidMainComposant, NomMainComposant FROM Module, MainComposant WHERE MainComposant.GuidMainComposant=Module.GuidMainComposant AND Module.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.MainComposant);
                    break;
                case 2: // Application
                    MessageBox.Show("Application");
                    //ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT User.GuidAppUser, NomAppUser FROM Module, Link, User WHERE Module.GuidModule=Link.GuidModuleOut AND Link.GuidModuleIn=User.GuidAppUser AND Module.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.User);
                    break;
                case 3: // Composant
                    MessageBox.Show("Composant");
                    //ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Module.GuidModule, Module.NomModule FROM Module m, Link, Module WHERE m.GuidModule=Link.GuidModuleOut AND Link.GuidModuleIn=Module.GuidModule AND m.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Module);
                    break;
                case 4: // Module
                    MessageBox.Show("Module");
                    //"Serveur", "Composant App", "Composant Tech", "Site", "Cluster", "Server", "Vlan", "Router" };
                    //ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Module.GuidModule, Module.NomModule FROM Module m, Link, Module WHERE m.GuidModule=Link.GuidModuleIn AND Link.GuidModuleOut=Module.GuidModule AND m.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Module);
                    break;
                case 5: // Interface
                    MessageBox.Show("Interface");
                    //ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Module.GuidModule, Module.NomModule FROM Module m, Link, Module WHERE m.GuidModule=Link.GuidModuleOut AND Link.GuidModuleIn=Module.GuidModule AND m.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Module);
                    break;
                case 6: // Composant
                    MessageBox.Show("Base");
                    //ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Module.GuidModule, Module.NomModule FROM Module m, Link, Module WHERE m.GuidModule=Link.GuidModuleOut AND Link.GuidModuleIn=Module.GuidModule AND m.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Module);
                    break;
                case 7: // Composant
                    MessageBox.Show("Fichier");
                    //ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Module.GuidModule, Module.NomModule FROM Module m, Link, Module WHERE m.GuidModule=Link.GuidModuleOut AND Link.GuidModuleIn=Module.GuidModule AND m.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Module);
                    break;
                case 8: // Composant
                    MessageBox.Show("Utilisateur");
                    //ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Module.GuidModule, Module.NomModule FROM Module m, Link, Module WHERE m.GuidModule=Link.GuidModuleOut AND Link.GuidModuleIn=Module.GuidModule AND m.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Module);
                    break;
                case 9: // Serveur
                    MessageBox.Show("Serveur");
                   //ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT ServerPhy.GuidModule, Module.NomModule FROM Module m, Link, Module WHERE m.GuidModule=Link.GuidModuleOut AND Link.GuidModuleIn=Module.GuidModule AND m.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Module);
                    break;
                case 10: // Composant
                    MessageBox.Show("Composant App");
                    //ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Module.GuidModule, Module.NomModule FROM Module m, Link, Module WHERE m.GuidModule=Link.GuidModuleOut AND Link.GuidModuleIn=Module.GuidModule AND m.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Module);
                    break;
                case 11: // Composant
                    MessageBox.Show("Composant Tech");
                    //ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Module.GuidModule, Module.NomModule FROM Module m, Link, Module WHERE m.GuidModule=Link.GuidModuleOut AND Link.GuidModuleIn=Module.GuidModule AND m.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Module);
                    break;
                case 12: // Composant
                    MessageBox.Show("Site");
                    //ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Module.GuidModule, Module.NomModule FROM Module m, Link, Module WHERE m.GuidModule=Link.GuidModuleOut AND Link.GuidModuleIn=Module.GuidModule AND m.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Module);
                    break;
                case 13: // Composant
                    MessageBox.Show("Cluster");
                    //ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Module.GuidModule, Module.NomModule FROM Module m, Link, Module WHERE m.GuidModule=Link.GuidModuleOut AND Link.GuidModuleIn=Module.GuidModule AND m.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Module);
                    break;
                case 14: // ServerPhy
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT ServerPhy.GuidServerPhy, ServerPhy.NomServerPhy FROM ServerPhy s, Link, Module WHERE m.GuidModule=Link.GuidModuleOut AND Link.GuidModuleIn=Module.GuidModule AND m.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Module);
                    break;
                case 15: // Composant
                    MessageBox.Show("Vlan");
                    //ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Module.GuidModule, Module.NomModule FROM Module m, Link, Module WHERE m.GuidModule=Link.GuidModuleOut AND Link.GuidModuleIn=Module.GuidModule AND m.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Module);
                    break;
                case 16: // Composant
                    MessageBox.Show("Router");
                    //ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Module.GuidModule, Module.NomModule FROM Module m, Link, Module WHERE m.GuidModule=Link.GuidModuleOut AND Link.GuidModuleIn=Module.GuidModule AND m.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Module);
                    break;

            }

            //base.ExpandObj(eo);
        }
    }

   
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolTechnoRef : DrawTools.ToolRectangle
	{
        public static string[] ssCat = { "Serveurs"}; 
        public ToolTechnoRef(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
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
                case 0: //Serveurs
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT ServerPhy.GuidServerPhy, NomServerPhy FROM Techno, ServerType, ServerTypeLink, Server, ServerLink, ServerPhy WHERE Techno.GuidTechnoHost=ServerType.GuidServerType AND ServerType.GuidServerType=ServerTypeLink.GuidServerType AND ServerTypeLink.GuidServer=Server.GuidServer AND Server.GuidServer=ServerLink.GuidServer AND ServerLink.GuidServerPhy=ServerPhy.GuidServerPhy AND Techno.GuidTechnoRef='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.ServerPhy);
                    break;
                case 1: //
                    break;
                case 2: //
                    break;
                case 3: //
                    break;
                case 4: //
                    break;
                case 5: //
                    break;
                case 6: //
                    break;
                case 7: //
                    break;
            }
        }
	}

    public class ToolFonction : DrawTools.ToolRectangle
    {
        public static string[] ssCat = { "Serveurs" };
        public ToolFonction(DrawArea da)
        {
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawFonction df;

            df = new DrawFonction(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(df);
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
                case 0: //Serveurs
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT ServerPhy.GuidServerPhy, NomServerPhy FROM ServerPhy, ServerLink, Server WHERE ServerPhy.GuidServerPhy=ServerLink.GuidServerPhy AND ServerLink.GuidServer=Server.GuidServer AND Server.GuidMainFonction='" + eo.GuidObj.ToString() + "' ORDER BY NomServerPhy", eo.tn, DrawArea.DrawToolType.ServerPhy);
                    break;
                case 1: //
                    break;
                case 2: //
                    break;
                case 3: //
                    break;
                case 4: //
                    break;
                case 5: //
                    break;
                case 6: //
                    break;
                case 7: //
                    break;
            }
        }
    }
}
