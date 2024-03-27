using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Windows.Forms;
using System.Xml;


namespace DrawTools
{

    public struct STExtention
    {
        public string sGuidExtention;
        public ArrayList lstFieldExtention;
    }

    public struct STDiscovery
    {
        public string sKindDisco;
        public string sTable;
        public string sRequete;
        public int iMappingKey;
        public string sFilterKey;
        public System.Net.UploadStringCompletedEventHandler ExecRespAPIDiscovery;
    }

    public struct STDiscoRequest
    {
        public string sKind;
        public string sSearchAtt;
        public string sSearchValue;
        public bool bMapping;
        public int iDiscoRequete;
        public List<STDiscovery> lstSTDiscovery; 
    }

    /// <summary>
    /// Ellipse tool
    /// </summary>
    public class ConfDataBase
    {
        public static string LibGuid = "Guid";
        public static string LibNom = "Nom";
        public static string LibGuidObjet = "GuidObjet";


        public enum FieldOption
        {
            Base = 0x00,
            ReadOnly = 0x1,
            Concat = 0x2,
            InterneBD = 0x4,
            Select = 0x8,
            NomCourt = 0x10,
            Key = 0x20,
            CacheVal = 0x40,
            NonVisible = 0x80,
            LinkTv = 0x100,
            ForeignKey = 0x200,
            ObjKey = 0x400,
            TabNonVisible = 0x800,
            ExternProperty = 0x1000,
            ExternKeyTable = 0x2000,
            Mandatory = 0x4000,
            Calcule = 0x8000,
            NomRef = 0x10000,
            SearchKey = 0x20000,
            Param = 0x40000,
            ForceSearchKey = 0x80000,
        }
        public ArrayList LstTable;


        public ConfDataBase()
        {
            LstTable = new ArrayList();

            AddGVide();

            //AddVue();
            
            AddProduit();// Avant TechnoRef
            AddG("Produit");

            AddEcosystem(); // Avant Vue 
            AddEnvironnement(); // Avant AddServerPhy & ServiceLink
            AddService(); // ServiceLink

            AddServiceLink(); // Avant GroupService

            AddProduitApp(); //Avant MainComposantRef
            AddTechnoRef(); //Avant Techno
            AddMainComposantRef(); // Avant MCompServ
            AddFonctionService(); // Avant GroupService
            AddGroupService(); // Avant TechLink
            

            AddApplicationClass(); // Avant Application
            AddApplicationType(); // Avant Application
            AddStatut(); // Avant Application
            AddCadreRefFonc(); // Avant Application
            AddArborescence(); // Avant Application
            AddAppVersion(); // Avant Application

            AddApplication(); //Avant TechLink
            AddG("Application");
            
            AddLink();
            AddGLink();

            AddTechLink();
            AddGTechLink();

            AddInterLink();
            AddGInterLink();

            AddBase();
            AddG("Base");

            AddLun();
            AddG("Lun");

            AddComposant();
            AddG("Composant");

            AddInterface();
            AddG("Interface");

            AddFile();
            AddG("File");

            AddQueue();
            AddG("Queue");

            AddLocation(); // Avant AddServerPhy
            AddG("Location");

            
            AddTypeVue();          // Avant Vue

            AddDiskClass(); // Avant AddServerPhy

            AddServerPhy();
            AddGServerPhy();

            AddFonction(); // Avant Add Server

            AddServer(); // Avant AddMainComposant
            AddG("Server");

            AddServer3D();

            AddGenks();
            AddG("Genks");

            AddGensas();
            AddG("Gensas");

            AddManagedsvc();
            AddG("Managedsvc");

            AddInsks();
            AddG("Insks");

            AddInsnd();
            AddG("Insnd");

            AddInsns();
            AddG("Insns");

            AddInspod();
            AddG("Inspod");

            AddInssvc();
            AddG("Inssvc");

            AddInsing();
            AddG("Insing");

            AddInssas();
            AddG("Inssas");

            AddGenpod();
            AddG("Genpod");

            AddContainer();
            AddG("Container");

            AddVarenv();

            AddGening();
            AddG("Gening");

            AddGensvc();
            AddG("Gensvc");

            AddVlan3D();

            AddMainComposant(); // Avant AddModule
            AddGMainComposant();

            AddModule();
            AddG("Module");

            AddAxes();
            AddG("Axes");

            AddPtNiveau();
            AddG("PtNiveau");

            AddServerType();
            AddG("ServerType");

            AddInfServer();
            AddG("InfServer");

            AddInfInssas();
            AddG("InfInssas");

            AddTechno();
            AddG("Techno");

            AddMCompServ();
            AddG("MCompServ");

            AddParamLink();
            AddParam();
            AddPackage();

            AddCompFonc();
            AddGCompFonc();

            AddVLan();
            AddG("VLan");

            AddSanSwitch();
            AddG("SanSwitch");

            AddCnx(); // Avant PtCnx
            AddG("Cnx");

            AddPtCnx();
            AddG("PtCnx");

            AddZone();
            AddGZone();

            AddRouter();
            AddG("Router");

            AddISL();
            AddG("ISL");

            AddNCard();
            AddG("NCard");

            AddSanCard();
            AddG("SanCard");

            AddAppUser();
            AddG("AppUser");
            AddG("TechUser");

            AddCluster();
            AddG("Cluster");

            AddNatRule();
            AddG("NatRule");

            AddMachine();
            AddGMachine();

            AddVirtuel();
            AddGVirtuel();

            AddServerSite();
            AddGServerSite();

            AddBaie();
            AddG("Baie");

            AddBaiePhy();
            AddG("BaiePhy");

            AddMachineCTI();
            AddGMachineCTI();

            AddBaieCTI();
            AddGBaieCTI();

            AddBaieDPhy();
            AddGBaieDPhy();

            AddDrawer();
            AddG("Drawer");
            
            AddCadreRef();
            AddGCadreRefN();
            AddG("CadreRefN1");
            AddG("CadreRefEnd");

            AddIndicator();
            AddG("Indicator");

            AddVue();

        }

        public int FindTable(string name)
        {
            Table t;

            for (int i = 0; i < LstTable.Count; i++)
            {
                t= (Table) LstTable[i];
                if (t.Name == name) 
                    return i;
            }
            return -1;
        }

        public int FindFieldFromLib(string Tname, string Flib)
        {
            Table t;

            int n = FindTable(Tname);
            if (n > -1)
            {
                t = (Table)LstTable[n];
                return t.FindFieldFromLib(Flib);
            }
            return -1;
        }

        public int FindField(string Tname, string Fname)
        {
            Table t;

            int n = FindTable(Tname);
            if (n > -1)
            {
                t = (Table)LstTable[n];
                return t.FindField(t.LstField, Fname);
            }
            return -1;
        }

        public int FindField(Table t, string Tname, string Fname)
        {
            return t.FindField(t.LstField, Fname);
        }


        public string FindLib(string Tname, string Fname)
        {
            Table t;

            int n = FindTable(Tname);
            if (n > -1)
            {
                t = (Table)LstTable[n];
                return t.FindLib(Fname);
            }
            return null;
        }


        public ArrayList FindNames(string Tname, string Fname)
        {
            Table t;
            ArrayList aFindNames = new ArrayList();


            int n = FindTable(Tname);
            if (n > -1)
            {
                t = (Table)LstTable[n];
                return t.FindNames(Fname);
            }
            return aFindNames;
        }

        public void AddTable(Table t)
        {
            LstTable.Add(t);
        }

        public void AddMachine()
        {
            Table t = new Table("Machine");

            FieldServerPhy(t);
                        
            AddTable(t);
        }

        public void AddInfServer()
        {
            Table t = new Table("InfServer");

            FieldServerPhy(t);

            AddTable(t);
        }

        public void AddInfInssas()
        {
            Table t = new Table("InfInssas");

            AddFieldInssas(t);

            AddTable(t);
        }


        public void AddMachineCTI()
        {
            Table t = new Table("MachineCTI");

            FieldServerPhy(t);
            int i = t.FindField(t.LstField, "NomServerPhy");
            if (i > -1)
            {
                Field f = (Field)t.LstField[i];
                f.GrpAffiche = 2;
                f.fieldOption = FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt;
            }

            AddTable(t);
        }

        public void AddVirtuel()
        {
            Table t = new Table("Virtuel");

            FieldServerPhy(t);

            AddTable(t);
        }

        public void AddServerSite()
        {
            int i;
            Table t = new Table("ServerSite");

            FieldServerPhy(t);

            //FieldOption.InterneBD | FieldOption.Select | FieldOption.Concat | FieldOption.NomCourt | FieldOption.TabNonVisible));

            /*i = t.FindField("NomServerPhy");
            if (i > -1)
            {
                Field f = (Field)t.LstField[i];
                f.fieldOption -= FieldOption.Concat;
            }*/
            i = t.FindField(t.LstField, "GuidAppUser");
            if (i > -1)
            {
                Field f = (Field)t.LstField[i];
                f.GrpAffiche = 2;
                f.fieldOption -= FieldOption.Concat;
            }
            i = t.FindField(t.LstField, "GuidApplication");
            if (i > -1)
            {
                Field f = (Field)t.LstField[i];
                f.GrpAffiche = 2;
                f.fieldOption -= FieldOption.Concat;
            }
            i = t.FindField(t.LstField, "GuidServer");
            if (i > -1)
            {
                Field f = (Field)t.LstField[i];
                f.GrpAffiche = 2;
            }


            AddTable(t);
        }

        /*public void AddVue() // Vue
        {
            Table t = new Table("Vue");
            t.LstField.Add(new Field("GuidVue", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select));
            t.LstField.Add(new Field("NomVue", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.ReadOnly));
            t.LstField.Add(new Field("GuidTypeVue", "GuidTypeVue", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.ReadOnly));
            t.LstField.Add(new Field("GuidApplication", "GuidApplication", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.ReadOnly));
            AddTable(t);
        }*/

        public void AddGServerPhy()
        {
            Table t = new Table("GServerPhy");

            t.LstField.Add(new Field("GuidGServerPhy", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Key));
            t.LstField.Add(new Field("GuidServerPhy", LibGuidObjet, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.ObjKey));
            FieldRectangle(t);
            t.LstField.Add(new Field("Forme", "Forme", 'i', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
            t.LstField.Add(new Field("thickColor", "Epaisseur Couleur", 'i', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
            t.LstField.Add(new Field("CPUCoreA", "CPU/Core App", 'd', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
            t.LstField.Add(new Field("RAMA", "RAM App", 'd', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));

            AddTable(t);
        }

        public void AddServerPhy()
        {
            Table t = new Table("ServerPhy");

            FieldServerPhy(t);

            AddTable(t);
        }

        public void AddVlan3D()
        {
            Table t = new Table("Vlan3D");

            t.LstField.Add(new Field("GuidVlan3D", LibGuid, 's', 0, 0, FieldOption.ReadOnly));
            t.LstField.Add(new Field("NomVlan3D", LibNom, 's', 2, 0, FieldOption.ReadOnly));

            AddTable(t);
        }

        public void AddServer3D()
        {

            Table t = new Table("Server3D");

            FieldServerPhy1(t);
            FieldServerPhy11(t);
            FieldServerPhy2(t);
            FieldServerPhy3(t);
            //t.LstField.Add(new Field("Trigramme", "Trigramme", 's', 3, 0, FieldOption.Concat | FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select));
            t.LstField.Add(new Field("Trigramme", "Trigramme", 's', 2, 0, FieldOption.Concat | FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select));
            FieldServer1(t);
            FieldFonction1(t);
            FieldVLanClass1(t);


            t.LstField.Add(new Field("Code", "Code", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select));
            t.LstField.Add(new Field("FImage", "Image", 's', 0, 0, FieldOption.ReadOnly));
            t.LstField.Add(new Field("IdxLigne", "Index Ligne", 's', 0, 0, FieldOption.ReadOnly));
            int i;
            i = t.FindField(t.LstField, "Description");
            if (i > -1) ((Field)t.LstField[i]).GrpAffiche = 3;
            i = t.FindField(t.LstField, "CPUcore");
            if (i > -1) ((Field)t.LstField[i]).GrpAffiche = 0;
            i = t.FindField(t.LstField, "RAM");
            if (i > -1) ((Field)t.LstField[i]).GrpAffiche = 0;
            /*
            i = t.FindField("NomServer");
            if (i > -1)
            {
                ((Field)t.LstField[i]).GrpAffiche = 3;
                ((Field)t.LstField[i]).fieldOption -= FieldOption.Concat;
            }
            */    
           

            AddTable(t);

        }

        public void FieldServerPhy1(Table t)
        {
            t.LstField.Add(new Field("GuidServerPhy", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomServerPhy", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt | FieldOption.TabNonVisible)); // FieldOption.Concat | 
            //t.LstField.Add(new Field("GuidLayer", "Layer", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.ReadOnly | FieldOption.TabNonVisible));

        }

        public void FieldServerPhy2(Table t)
        {
            t.LstField.Add(new Field("Type", "Type (M/P/V/H/E)", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
        }

        public void FieldServerPhy3(Table t)
        {
            t.LstField.Add(new Field("Profil", "Profil", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
            t.LstField.Add(new Field("CPUcore", "rperf/core", 'd', 3, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt | FieldOption.Concat));
            t.LstField.Add(new Field("RAM", "ram", 'd', 3, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
        }

        public void FieldServerPhy11(Table t)
        {
            t.LstField.Add(new Field("Description", "Description", 's', 4, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
        }

        public void FieldServerPhy(Table t)
        {
            FieldServerPhy1(t);
            t.LstField.Add(new Field("NomServer", "Nom Server", 's', 2, 0, FieldOption.Concat | FieldOption.TabNonVisible)); 
            t.LstField.Add(new Field("Image", "Image", 's', 0, 0, FieldOption.ReadOnly));
            FieldServerPhy11(t);
            t.LstField.Add(new Field("GuidLocation", "GuidLocation", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.NonVisible | FieldOption.TabNonVisible | FieldOption.ExternKeyTable, FindTable("Location")));
            t.LstField.Add(new Field("NomLocation", "Localisation", 's', 0, 0, FieldOption.ReadOnly | FieldOption.Select | FieldOption.CacheVal | FieldOption.NomCourt, "GuidLocation"));
            FieldServerPhy2(t);
            t.LstField.Add(new Field("GuidAppUser", "User Infra", 's', 2, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.Concat | FieldOption.NomCourt));
            t.LstField.Add(new Field("GuidApplication", "Application Infra", 's', 2, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.Concat | FieldOption.NomCourt));
            t.LstField.Add(new Field("GuidServer", "Serveurs Infra", 's', 2, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.NomCourt));
            t.LstField.Add(new Field("NCard", "Network Interface", 's', 0, 0, FieldOption.ExternProperty | FieldOption.NonVisible));
            t.LstField.Add(new Field("GuidLabel", "Label", 's', 0, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.NomCourt | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("SanCard", "HBA Interface", 's', 0, 0, FieldOption.NonVisible));
            t.LstField.Add(new Field("Forme", "Forme", 'i', 0, 0, FieldOption.Base | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("thickColor", "Epaisseur Couleur", 'i', 0, 0, FieldOption.Base | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidTechnoRef", "GuidTechnoRef", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.NonVisible | FieldOption.TabNonVisible | FieldOption.ExternKeyTable, FindTable("TechnoRef")));
            t.LstField.Add(new Field("NomTechnoRef", "Cadre de Ref", 's', 0, 0, FieldOption.ReadOnly | FieldOption.Select | FieldOption.CacheVal | FieldOption.NomCourt, "GuidTechnoRef"));           
            t.LstField.Add(new Field("Start", "Emplacement de départ", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NbrU", "Nombre de U", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("IndexImgOS", "Index Img OS", 'd', 0, 0, FieldOption.ReadOnly | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("OSVersion", "Version OS", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
            FieldServerPhy3(t);
            t.LstField.Add(new Field("DiskData", "Disk Données", 'd', 3, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
            t.LstField.Add(new Field("GuidDiskClass", "GuidDiskClass", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.NonVisible | FieldOption.TabNonVisible | FieldOption.ExternKeyTable, FindTable("DiskClass")));
            t.LstField.Add(new Field("NomDiskClass", "disk class", 's', 3, 0, FieldOption.ReadOnly | FieldOption.Select | FieldOption.CacheVal | FieldOption.NomCourt, "GuidDiskClass"));
            t.LstField.Add(new Field("GuidBackupClass", "GuidBackupClass", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomBackupClass", "backup class", 's', 3, 0, FieldOption.ReadOnly | FieldOption.Select | FieldOption.CacheVal | FieldOption.NomCourt, "GuidBackupClass"));
            t.LstField.Add(new Field("GuidExploitClass", "GuidExploitClass", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomExploitClass", "exploit class", 's', 3, 0, FieldOption.ReadOnly | FieldOption.Select | FieldOption.CacheVal | FieldOption.NomCourt, "GuidExploitClass"));
            t.LstField.Add(new Field("GuidHost", "Hote", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.ReadOnly | FieldOption.NomCourt | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidCluster", "Cluster", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.ReadOnly | FieldOption.NomCourt | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidBaiePhy", "Baie Physique", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.ReadOnly | FieldOption.NomCourt | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Indicator", "Indicateur", 's', 0, 0, FieldOption.ReadOnly));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PropRole", "Role", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
        }

        public void AddVLan()
        {
            Table t = new Table("VLan");

            FieldVLan(t);
            
            AddTable(t);
        }

        public void FieldVLanClass1(Table t)
        {
            t.LstField.Add(new Field("GuidVlanClass", "GuidVlanClass", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key));
            t.LstField.Add(new Field("NomVlanClass", "NomVlanClass", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
        }

        public void FieldVLan(Table t)
        {
            t.LstField.Add(new Field("GuidVlan", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Key | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomVlan", LibNom, 's', 2, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Concat));
            t.LstField.Add(new Field("NumVlan", "Numéro Vlan", 's', 2, 0, FieldOption.Concat | FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("NomReseau", "Réseau", 's', 2, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Passerelle", "IP Passerelle", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("CodePays", "Code Pays", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Couleur", "Couleur (ARGB)", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.ReadOnly | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidVlanClass", "GuidVlanClass", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.NonVisible | FieldOption.TabNonVisible | FieldOption.ExternKeyTable, FindTable("VlanClass")));
            t.LstField.Add(new Field("NomVlanClass", "Vlan Class", 's', 4, 0, FieldOption.ReadOnly | FieldOption.Select | FieldOption.CacheVal | FieldOption.NomCourt, "GuidVlanClass"));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
        }

        public void AddEcosystem()
        {
            Table t = new Table("Ecosystem");

            t.LstField.Add(new Field("GuidEcosystem", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Key | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Mandatory | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomEcosystem", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory | FieldOption.NomRef));
            t.LstField.Add(new Field("IdEcosystem", "Id Ecosystem", 's', 1, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory));
            t.LstField.Add(new Field("CodeAp", "Code App", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory));
            
            AddTable(t);
        }


        public void AddNCard()
        {
            Table t = new Table("NCard");

            t.LstField.Add(new Field("GuidNCard", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Key | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomNCard", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidHote", "Guid Hote", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));

            t.LstField.Add(new Field("GuidVlan", "Guid Vlan", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.NonVisible | FieldOption.TabNonVisible | FieldOption.ExternKeyTable, FindTable("Vlan")));
            t.LstField.Add(new Field("NomVlan", "Nom VLan", 's', 0, 0, FieldOption.ReadOnly | FieldOption.Select | FieldOption.CacheVal | FieldOption.TabNonVisible | FieldOption.NomCourt, "GuidVlan"));
            //t.LstField.Add(new Field("GuidVlan", "Guid Vlan", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("VLan", "DMZ Vlan", 's', 0, 0, FieldOption.NonVisible | FieldOption.ExternProperty));

            t.LstField.Add(new Field("IPAddr", "IP", 's', 2, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("IPNat", "IP Nat", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Alias0", "Alias0", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Alias1", "Alias1", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Alias", "Alias", 's', 0, 0, FieldOption.ReadOnly | FieldOption.CacheVal));
            t.LstField.Add(new Field("Hauteur", "Position (0/1)", 'i', 0, 0, FieldOption.Base | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("FluxIn", "Flux Entrant", 's', 0, 0, FieldOption.ReadOnly | FieldOption.CacheVal));
            t.LstField.Add(new Field("FluxOut", "Flux Sortant", 's', 0, 0, FieldOption.ReadOnly | FieldOption.CacheVal));
            t.LstField.Add(new Field("TabDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            
            AddTable(t);
        }

        public void AddSanCard()
        {
            Table t = new Table("SanCard");
            
            t.LstField.Add(new Field("GuidSanCard", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Key | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Mandatory));
            t.LstField.Add(new Field("NomSanCard", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory));
            t.LstField.Add(new Field("GuidHote", "Guid Hote", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Mandatory));
            t.LstField.Add(new Field("GuidSanSwitch", "Guid Switch", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select));
            t.LstField.Add(new Field("WWN", "WWN", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Alias", "Alias", 's', 2, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Port", "Port San Switch", 's', 3, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidSanCardA", "SCard Switch", 's', 0, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.NomCourt));
            t.LstField.Add(new Field("GuidLun", "Linked Lun", 's', 0, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.NomCourt));
            t.LstField.Add(new Field("Hauteur", "Position (0/1)", 'i', 0, 0, FieldOption.Base));

            AddTable(t);
        }

        public void AddPtCnx()
        {
            Table t = new Table("PtCnx");

            t.LstField.Add(new Field("GuidPtCnx", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Key | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomPtCnx", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.ReadOnly));
            t.LstField.Add(new Field("Libelle", "Libelle", 's', 3, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Debit", "Debit", 's', 3, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidLocation", "Location", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey));
            t.LstField.Add(new Field("GuidCnx", "Connection", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey | FieldOption.ExternKeyTable, FindTable("Cnx")));            
            AddTable(t);
        }

        public void AddRouter()
        {
            Table t = new Table("Router");

            t.LstField.Add(new Field("GuidRouter", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Key | FieldOption.ReadOnly | FieldOption.Select));
            t.LstField.Add(new Field("NomRouter", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select));
            //t.LstField.Add(new Field("GuidVlan", "Guid Vlan", 's', 0, 0, FieldOption.ReadOnly));
            
            AddTable(t);
        }

        public void AddISL()
        {
            Table t = new Table("ISL");

            t.LstField.Add(new Field("GuidISL", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Key | FieldOption.ReadOnly | FieldOption.Select));
            t.LstField.Add(new Field("NomISL", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("NbrInt", "Nombre Interfaces", 'i', 0, 0, FieldOption.Base));

            AddTable(t);
        }


        public void AddSanSwitch()
        {
            Table t = new Table("SanSwitch");

            t.LstField.Add(new Field("GuidSanSwitch", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Key | FieldOption.ReadOnly | FieldOption.Select));
            t.LstField.Add(new Field("NomSanSwitch", LibNom, 's', 2, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Concat));
            t.LstField.Add(new Field("Modele", "Modèle", 's', 2, 0, FieldOption.InterneBD | FieldOption.Select));
            AddTable(t);
        }

        public void AddCnx()
        {
            Table t = new Table("Cnx");

            t.LstField.Add(new Field("GuidCnx", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Key | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible | FieldOption.Mandatory));
            t.LstField.Add(new Field("NomCnx", LibNom, 's', 2, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory));
            t.LstField.Add(new Field("PtCnx", "Access Point", 's', 0, 0, FieldOption.ExternProperty | FieldOption.NonVisible));
            AddTable(t);
        }


        public void AddZone()
        {
            Table t = new Table("Zone");

            t.LstField.Add(new Field("GuidZone", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Key | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomZone", LibNom, 's', 2, 200, FieldOption.InterneBD | FieldOption.Select | FieldOption.Concat));
            t.LstField.Add(new Field("Pos", "Position Nom (0,1,2)", 'i', 0, 0, FieldOption.Base | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NumZone", "Numéro Zone", 's', 2, 105, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidServerPhy", "Hote", 's', 0, 120, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey));
            t.LstField.Add(new Field("GuidLun", "Lun", 's', 0, 120, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey));
            t.LstField.Add(new Field("Couleur", "Couleur", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.ReadOnly | FieldOption.TabNonVisible));


            AddTable(t);
        }

        public void AddModule()
        {
            Table t = new Table("Module");

            FieldModule(t);

            t.LstField.Add(new Field("GuidGModule", "GuidGModule", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("X", "X", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Y", "Y", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Width", "Width", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Height", "Height", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void AddCompFonc()
        {
            Table t = new Table("CompFonc");

            FieldModule(t);
            int i = t.FindField(t.LstField, "NomModule");
            if (i > -1)
            {
                Field f = (Field) t.LstField[i];
            }

            t.LstField.Add(new Field("GuidGCompFonc", "GuidGCompFonc", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("X", "X", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Y", "Y", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Width", "Width", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Height", "Height", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void FieldModule(Table t)
        {
            //public Field(string name, string lib, char type, int grpaff, ConfDataBase.FieldOption fo)
            t.LstField.Add(new Field("GuidModule", LibGuid, 's', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select | FieldOption.Key));
            t.LstField.Add(new Field("NomModule", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
            t.LstField.Add(new Field("GuidLayer", "Layer", 's', 0, 0, FieldOption.Select | FieldOption.NomCourt | FieldOption.ReadOnly | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidMainComposant", "Guid MainComposant", 's', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt | FieldOption.ExternKeyTable, FindTable("MainComposant")));
            //t.LstField.Add(new Field("GuidMainComposant", "Guid MainComposant", 's', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
            //t.LstField.Add(new Field("LinkTables", "GModule;GCompFonc", 's', 0, 0, FieldOption.NonVisible));
            t.LstField.Add(new Field("PWord", "Texte", 's', 0, 0, FieldOption.ReadOnly |FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PropRole", "Role", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

        }

        public void AddAxes()
        {
            Table t = new Table("Axes");

            t.LstField.Add(new Field("GuidAxes", LibGuid, 's', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select | FieldOption.Key));
            t.LstField.Add(new Field("NomAxes", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
            t.LstField.Add(new Field("XAxe", "XAxe", 's', 3, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("YAxe", "YAxe", 's', 3, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("XBorneMin", "XBorneMin", 'd', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NonVisible));
            t.LstField.Add(new Field("XBorneMax", "XBorneMax", 'd', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NonVisible));
            t.LstField.Add(new Field("XBorneMoy", "XBorneMoy", 'd', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NonVisible));
            t.LstField.Add(new Field("YBorneMin", "YBorneMin", 'd', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NonVisible));
            t.LstField.Add(new Field("YBorneMax", "YBorneMax", 'd', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NonVisible));
            t.LstField.Add(new Field("YBorneMoy", "YBorneMoy", 'd', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NonVisible));
            
            AddTable(t);
        }

        public void AddPtNiveau()
        {
            Table t = new Table("PtNiveau");

            t.LstField.Add(new Field("GuidPtNiveau", LibGuid, 's', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select | FieldOption.Key));
            t.LstField.Add(new Field("NomPtNiveau", LibNom, 's', 3, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
            t.LstField.Add(new Field("GuidAxes", "Guid Axes", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NivAbs", "Niveau Abs", 'd', 0, 0, FieldOption.ReadOnly | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NivOrd", "Niveau Ord", 'd', 0, 0, FieldOption.ReadOnly | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("IconStatusX", "IconStatusX", 'd', 0, 0, FieldOption.ReadOnly | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("IconStatusY", "IconStatusY", 'd', 0, 0, FieldOption.ReadOnly | FieldOption.TabNonVisible));
            
            AddTable(t);
        }

        public void AddG(string sObjet)
        {
            Table t = new Table("G" + sObjet);

            t.LstField.Add(new Field("GuidG" + sObjet, LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Key));
            t.LstField.Add(new Field("Guid" + sObjet, LibGuidObjet, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.ObjKey));
            FieldRectangle(t);

            AddTable(t);
        }

        public void AddGCadreRefN()
        {
            Table t = new Table("GCadreRefN");

            t.LstField.Add(new Field("GuidGCadreRefN", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Key));
            t.LstField.Add(new Field("GuidCadreRef", LibGuidObjet, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.ObjKey));
            FieldRectangle(t);

            AddTable(t);
        }

        public void AddGBaieCTI()
        {
            Table t = new Table("GBaieCTI");

            t.LstField.Add(new Field("GuidGBaieCTI", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Key));
            t.LstField.Add(new Field("GuidBaie", LibGuidObjet, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.ObjKey));
            FieldRectangle(t);

            AddTable(t);
        }

        public void AddGBaieDPhy()
        {
            Table t = new Table("GBaieDPhy");

            t.LstField.Add(new Field("GuidGBaieDPhy", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Key));
            t.LstField.Add(new Field("GuidBaie", LibGuidObjet, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.ObjKey));
            FieldRectangle(t);

            AddTable(t);
        }

        public void AddGVide()
        {
            Table t = new Table("GVide");

            AddTable(t);
        }

        public void AddGLink()
        {
            Table t = new Table("GLink");

            t.LstField.Add(new Field("GuidGLink", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select| FieldOption.ReadOnly | FieldOption.Key));
            t.LstField.Add(new Field("GuidLink", LibGuidObjet, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.ObjKey));
            t.LstField.Add(new Field("I", "Num", 'i', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
            t.LstField.Add(new Field("X", "Left", 'i', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
            t.LstField.Add(new Field("Y", "Top", 'i', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
            t.LstField.Add(new Field("Pos", "Position", 'i', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));

            AddTable(t);
        }

        public void AddGTechLink()
        {
            Table t = new Table("GTechLink");

            t.LstField.Add(new Field("GuidGTechLink", LibGuid, 's', 0, 0, FieldOption.InterneBD |  FieldOption.Select |FieldOption.ReadOnly | FieldOption.Key));
            t.LstField.Add(new Field("GuidTechLink", LibGuidObjet, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.ObjKey));
            t.LstField.Add(new Field("I", "Num", 'i', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
            t.LstField.Add(new Field("X", "Left", 'i', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
            t.LstField.Add(new Field("Y", "Top", 'i', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
            t.LstField.Add(new Field("Pos", "Position", 'i', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));

            AddTable(t);
        }

        
        public void AddGInterLink()
        {
            Table t = new Table("GInterLink");

            t.LstField.Add(new Field("GuidGInterLink", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly |  FieldOption.Select | FieldOption.Key));
            t.LstField.Add(new Field("GuidInterLink", LibGuidObjet, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.ObjKey));
            t.LstField.Add(new Field("I", "Num", 'i', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
            t.LstField.Add(new Field("X", "Left", 'i', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
            t.LstField.Add(new Field("Y", "Top", 'i', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
            t.LstField.Add(new Field("Pos", "Position", 'i', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));

            AddTable(t);
        }

        public void AddGZone()
        {
            Table t = new Table("GZone");

            t.LstField.Add(new Field("GuidGZone", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Key));
            t.LstField.Add(new Field("GuidZone", LibGuidObjet, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.ObjKey));
            t.LstField.Add(new Field("X", "Left", 'i', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
            t.LstField.Add(new Field("Y", "Top", 'i', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
            t.LstField.Add(new Field("Pos", "Position", 'i', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));

            AddTable(t);
        }

        public void AddGCompFonc()
        {
            Table t = new Table("GCompFonc");

            t.LstField.Add(new Field("GuidGCompFonc", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Key));
            t.LstField.Add(new Field("GuidModule", LibGuidObjet, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.ObjKey));
            FieldRectangle(t);

            AddTable(t);
        }

        public void AddGMachine()
        {
            Table t = new Table("GMachine");

            t.LstField.Add(new Field("GuidGMachine", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Key));
            t.LstField.Add(new Field("GuidServerPhy", LibGuidObjet, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.ObjKey));
            FieldRectangle(t);

            AddTable(t);
        }

        public void AddGMachineCTI()
        {
            Table t = new Table("GMachineCTI");

            t.LstField.Add(new Field("GuidGMachineCTI", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Key));
            t.LstField.Add(new Field("GuidServerPhy", LibGuidObjet, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.ObjKey));
            FieldRectangle(t);

            AddTable(t);
        }

        public void AddGVirtuel()
        {
            Table t = new Table("GVirtuel");

            t.LstField.Add(new Field("GuidGVirtuel", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Key));
            t.LstField.Add(new Field("GuidServerPhy", LibGuidObjet, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.ObjKey));
            FieldRectangle(t);

            AddTable(t);
        }

        public void AddGServerSite()
        {
            Table t = new Table("GServerSite");

            t.LstField.Add(new Field("GuidGServerSite", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Key));
            t.LstField.Add(new Field("GuidServerPhy", LibGuidObjet, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.ObjKey));
            FieldRectangle(t);

            AddTable(t);
        }

        public void FieldRectangle(Table t)
        {
            t.LstField.Add(new Field("X", "Left", 'i', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Y", "Top", 'i', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Width", "Width", 'i', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Height", "Height", 'i', 0, 0, FieldOption.ReadOnly | FieldOption.InterneBD | FieldOption.Select));
        }

        public void AddLink()
        {
            Table t = new Table("Link");

            t.LstField.Add(new Field("GuidLink", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Key | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Id", "Id", 's', 2, 15, FieldOption.InterneBD | FieldOption.Select | FieldOption.Concat));
            t.LstField.Add(new Field("NomLink", LibNom, 's', 2, 100, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Pos", "Position Nom (0,1,2)", 'i', 0, 0, FieldOption.Base | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidModuleIn", "Module entrant", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible | FieldOption.ObjKey));
            t.LstField.Add(new Field("GuidModuleOut", "Module sortant", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible | FieldOption.ObjKey));
            t.LstField.Add(new Field("TypeLink", "Type", 's', 0, 25, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidComposantL1In", "Guid Source", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey | FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomComposantL1In", "Nom Source", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey));
            t.LstField.Add(new Field("TypeComposantL1In", "Type Source", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey | FieldOption.NonVisible));
            t.LstField.Add(new Field("GuidComposantL1Out", "Target", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey | FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomComposantL1Out", "Nom Target", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey));
            t.LstField.Add(new Field("TypeComposantL1Out", "Type Target", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey | FieldOption.NonVisible));
            t.LstField.Add(new Field("GuidComposantL2In", "MComposantSource", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey | FieldOption.TabNonVisible | FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomComposantL2In", "Nom MainC Source", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey | FieldOption.TabNonVisible | FieldOption.NonVisible));
            t.LstField.Add(new Field("TypeComposantL2In", "Nom MainC Source", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey | FieldOption.TabNonVisible | FieldOption.NonVisible));
            t.LstField.Add(new Field("GuidComposantL2Out", "MComposantTarget", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey | FieldOption.TabNonVisible | FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomComposantL2Out", "Nom MainC Target", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey | FieldOption.TabNonVisible | FieldOption.NonVisible));
            t.LstField.Add(new Field("TypeComposantL2Out", "Nom MainC Target", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey | FieldOption.TabNonVisible | FieldOption.NonVisible));
            t.LstField.Add(new Field("GuidAppIn", "Guid App Source", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey | FieldOption.TabNonVisible | FieldOption.NonVisible | FieldOption.NonVisible));
            t.LstField.Add(new Field("NomAppIn", "Nom App Source", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey | FieldOption.TabNonVisible | FieldOption.NonVisible));
            t.LstField.Add(new Field("GuidAppOut", "Guid App Target", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey | FieldOption.TabNonVisible | FieldOption.NonVisible | FieldOption.NonVisible));
            t.LstField.Add(new Field("NomAppOut", "Nom App Target", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey | FieldOption.TabNonVisible | FieldOption.NonVisible));
            t.LstField.Add(new Field("Arrow", "Arrow", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Support", "Support", 's', 0, 40, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Mechanism", "Mech.", 's', 0, 40, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Frequency", "Freq.", 's', 0, 40, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Volumetry", "Vol.", 'd', 0, 40, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Tracability", "(T)", 's', 0, 15, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Confidentiality", "(C)", 's', 0, 15, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Integrity", "(I)", 's', 0, 15, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("PropDescription", "Description Fonc", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("InLineDescription", "Description App", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("InLineCarac", "Caracteristiques", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            AddTable(t);
        }

        public void AddTechLink()
        {
            Table t = new Table("TechLink");

            t.LstField.Add(new Field("GuidTechLink", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Key | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Id", "Id", 's', 2, 15, FieldOption.InterneBD | FieldOption.Select | FieldOption.Concat));
            t.LstField.Add(new Field("NomTechLink", LibNom, 's', 0, 94, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Pos", "Position Nom (0,1,2)", 'i', 0, 0, FieldOption.Base | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidGroupService", "Service", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.NonVisible | FieldOption.TabNonVisible | FieldOption.ExternKeyTable, FindTable("GroupService")));
            t.LstField.Add(new Field("NomGroupService", "Protocol", 's', 2, 50, FieldOption.ReadOnly | FieldOption.Select | FieldOption.CacheVal | FieldOption.NomCourt, "GuidGroupService"));
            t.LstField.Add(new Field("Couleur", "Couleur", 's', 0, 0, FieldOption.Select | FieldOption.NomCourt | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("LinksApplicatifs", "Links Applicatifs", 's', 0, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidServerIn", "Guid Sender", 's', 0, 93, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey | FieldOption.NonVisible));
            t.LstField.Add(new Field("NomServerIn", "Nom Sender", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey));
            t.LstField.Add(new Field("TypeServerIn", "Type Sender", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey | FieldOption.NonVisible));
            //t.LstField.Add(new Field("NomServiceIn", "Sender", 's', 0, 50, FieldOption.ReadOnly));
            t.LstField.Add(new Field("GuidServerOut", "Recipient", 's', 0, 93, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey | FieldOption.NonVisible));
            t.LstField.Add(new Field("NomServerOut", "Nom Recipient", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey));
            t.LstField.Add(new Field("TypeServerOut", "Type Recipient", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey | FieldOption.NonVisible));
            //t.LstField.Add(new Field("NomServiceOut", "Provider", 's', 0, 50, FieldOption.ReadOnly));
            t.LstField.Add(new Field("GuidServerL2In", "Guid Sender", 's', 0, 93, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey | FieldOption.NonVisible));
            t.LstField.Add(new Field("NomServerL2In", "Nom Sender", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey));
            t.LstField.Add(new Field("TypeServerL2In", "Type Sender", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey | FieldOption.NonVisible));
            t.LstField.Add(new Field("GuidServerL2Out", "Guid Sender", 's', 0, 93, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey | FieldOption.NonVisible));
            t.LstField.Add(new Field("NomServerL2Out", "Nom Sender", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey));
            t.LstField.Add(new Field("TypeServerL2Out", "Type Sender", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey | FieldOption.NonVisible));
            t.LstField.Add(new Field("GuidAppIn", "AppSource", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey | FieldOption.TabNonVisible | FieldOption.Calcule)); // FieldOption.ExternKeyTable, FindTable("Application")));
            t.LstField.Add(new Field("GuidAppOut", "AppTarget", 's', 0, 100, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey | FieldOption.TabNonVisible | FieldOption.Calcule)); // FieldOption.ExternKeyTable, FindTable("Application")));
            t.LstField.Add(new Field("Alias", "Alias", 's', 3, 0, FieldOption.ReadOnly)); // FieldOption.NonVisible
            t.LstField.Add(new Field("NomService", "Service", 's', 3, 0, FieldOption.ReadOnly)); //FieldOption.NonVisible
            t.LstField.Add(new Field("Volume", "Volume", 'd', 0, 50, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Frecency", "Frequency", 's', 0, 50, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Whenever", "Whenever", 's', 0, 50, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Response", "Resp. Time", 's', 0, 50, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("InLineDescInLine", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("InLineCarac", "Caracteristiques", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            AddTable(t);
        }

        
        public void AddInterLink()
        {
            Table t = new Table("InterLink");

            t.LstField.Add(new Field("GuidInterLink", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Key | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Id", "Id", 's', 2, 15, FieldOption.InterneBD | FieldOption.Select | FieldOption.Concat));
            t.LstField.Add(new Field("NomInterLink", LibNom, 's', 0, 94, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Pos", "Position Nom (0,1,2)", 'i', 0, 0, FieldOption.Base | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidServerSiteIn", "Sender", 's', 0, 93, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey));
            t.LstField.Add(new Field("GuidNCardIn", "GuidNCardIn", 's', 0, 0, FieldOption.ReadOnly | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("EthIn", "EthIn eX", 's', 0, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.NomCourt, "GuidNCardIn"));
            t.LstField.Add(new Field("GuidServerSiteOut", "Recipient", 's', 0, 93, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey));
            t.LstField.Add(new Field("GuidNCardOut", "GuidNCardOut", 's', 0, 0, FieldOption.ReadOnly | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("EthOut", "EthOut eX", 's', 0, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.NomCourt, "GuidNCardOut"));
            t.LstField.Add(new Field("GuidGroupService", "Service", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomGroupService", "Protocol", 's', 2, 50, FieldOption.ReadOnly | FieldOption.Select | FieldOption.CacheVal | FieldOption.NomCourt, "GuidGroupService"));
            t.LstField.Add(new Field("Ports", "List Ports", 's', 3, 0, FieldOption.ReadOnly));
            t.LstField.Add(new Field("Volume", "Volume", 'd', 0, 50, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Frecency", "Frequency", 's', 0, 50, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Whenever", "Whenever", 's', 0, 50, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Response", "Resp. Time", 's', 0, 50, FieldOption.InterneBD | FieldOption.Select));
            AddTable(t);
        }

        public void AddGMainComposant()
        {
            Table t = new Table("GMainComposant");

            t.LstField.Add(new Field("GMainComposant", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Key));
            t.LstField.Add(new Field("MainComposant", LibGuidObjet, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.ObjKey));
            FieldRectangle(t);


            AddTable(t);
        }

        public void AddMainComposant()
        {
            Table t = new Table("MainComposant");

            FieldMainComposant(t);

            t.LstField.Add(new Field("GuidGMainComposant", "GuidGMainCommosant", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("X", "X", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Y", "Y", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Width", "Width", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Height", "Height", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void FieldMainComposant(Table t)
        {
            t.LstField.Add(new Field("GuidMainComposant", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Key | FieldOption.Mandatory));
            t.LstField.Add(new Field("NomMainComposant", LibNom, 's', 2, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt | FieldOption.Mandatory));
            t.LstField.Add(new Field("GuidLayer", "Layer", 's', 0, 0, FieldOption.Select | FieldOption.NomCourt | FieldOption.ReadOnly | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidServer", "GuidParent", 's', 0, 0, FieldOption.Base));
            t.LstField.Add(new Field("PWord", "Texte", 's', 0, 0, FieldOption.ReadOnly));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
        }

        public void AddComposant()
        {
            Table t = new Table("Composant");

            t.LstField.Add(new Field("GuidComposant", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomComposant", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidLayer", "Layer", 's', 0, 0, FieldOption.Select | FieldOption.NomCourt | FieldOption.ReadOnly | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Mode", "Mode (Sync/Async)", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Type", "Type", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("TypeIt", "Icon Sup", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("TypeIb", "Icon Inf", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Localization", "Localization", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Role", "Role", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("CompUser", "User", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Access", "Access", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("StartCmd", "Start Command", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("StopCmd", "Stop Command", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Parameter", "Parameters", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("BatchName", "Nom Job UC4", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Schedule", "Schedule", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select)); // planification, frequence, temps de traitement?
            t.LstField.Add(new Field("GuidMainComposant", "Guid MainComposant", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PWord", "Texte", 's', 0, 0, FieldOption.ReadOnly));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));


            t.LstField.Add(new Field("GuidGComposant", "GuidGComposant", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("X", "X", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Y", "Y", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Width", "Width", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Height", "Height", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void AddProduit()
        {
            Table t = new Table("Produit");

            t.ExternalKeyTableOption = FieldOption.InterneBD;
            t.LstField.Add(new Field("GuidProduit", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomProduit", LibNom, 's', 3, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidTechnoArea", "GuidTechnoArea", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.ForeignKey | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Family", "Family", 's', 0, 0, FieldOption.Base));
            t.LstField.Add(new Field("Domain", "Domain", 's', 0, 0, FieldOption.Base));
            t.LstField.Add(new Field("Subdomain", "Subdomain", 's', 0, 0, FieldOption.Base));
            t.LstField.Add(new Field("NormeG", "Group Status", 'p', 0, 0, FieldOption.Base));
            t.LstField.Add(new Field("Norme", "LS Status", 'p', 0, 0, FieldOption.Base));
            t.LstField.Add(new Field("Editeur", "Supplier", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Support", "Support", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("UseCase", "Use Cases and policy", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Roadmap", "Roadmap", 'o', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Catalogue", "Catalogue", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidCadreRef", "GuidCadreRef", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.ForeignKey | FieldOption.TabNonVisible));

            AddTable(t);
        }

        
        public void AddGroupService()
        {
            Table t = new Table("GroupService");

            //t.LstField.Add(new Field("GuidGroupService", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.Mandatory | FieldOption.ExternKeyTable, FindTable("ServiceLink")));
            t.LstField.Add(new Field("GuidGroupService", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.Mandatory | FieldOption.ExternKeyTable | FieldOption.SearchKey, FindTable("ServiceLink")));
            t.LstField.Add(new Field("NomGroupService", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory));
            t.LstField.Add(new Field("GuidFonctionService", "Guid Fonction", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible | FieldOption.Mandatory | FieldOption.ExternKeyTable, FindTable("FonctionService")));
            t.LstField.Add(new Field("Couleur", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));

            AddTable(t);
        }

        public void AddService()
        {
            Table t = new Table("Service");

            t.LstField.Add(new Field("GuidService", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.Mandatory));
            t.LstField.Add(new Field("NomService", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory));
            t.LstField.Add(new Field("InfoSup", "InfoSup", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Protocole", "Protocole", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory));
            t.LstField.Add(new Field("Ports", "Ports", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory));
            t.LstField.Add(new Field("Description", "Description", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            

            AddTable(t);
        }
                
        public void AddServerType()
        {
            Table t = new Table("ServerType");

            t.LstField.Add(new Field("GuidServerType", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key));
            t.LstField.Add(new Field("NomServerType", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidServer", "GuidParent", 's', 0, 0, FieldOption.ReadOnly));
            t.LstField.Add(new Field("GuidFonction", "GuidFonction", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.NonVisible | FieldOption.TabNonVisible | FieldOption.ExternKeyTable, FindTable("Fonction")));
            t.LstField.Add(new Field("NomFonction", "Fonction Server", 's', 3, 0, FieldOption.ReadOnly | FieldOption.Select | FieldOption.CacheVal | FieldOption.NomCourt, "GuidFonction"));


            AddTable(t);
        }

        public void AddProduitApp()
        {
            Table t = new Table("ProduitApp");

            t.ExternalKeyTableOption = FieldOption.InterneBD;
            t.LstField.Add(new Field("GuidProduitApp", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key));
            t.LstField.Add(new Field("NomProduitApp", LibNom, 's', 3, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Editeur", "Editeur", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidCadreRefApp", "GuidCadreRefApp", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.ForeignKey));

            AddTable(t);
        }

        public void AddDrawer()
        {
            Table t = new Table("Drawer");

            t.LstField.Add(new Field("GuidDrawer", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Key));
            t.LstField.Add(new Field("NomDrawer", LibNom, 's', 3, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidBaiePhy", "Baie", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select));
            t.LstField.Add(new Field("StartU", "U de départ", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("NbrU", "Nombre de U", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("NbrE", "Nombre d'Equipement", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select));

            AddTable(t);
        }

        public void AddCluster()
        {
            Table t = new Table("Cluster");

            t.LstField.Add(new Field("GuidCluster", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomCluster", LibNom, 's', 2, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("NCard", "Network Interface", 's', 0, 0, FieldOption.ExternProperty | FieldOption.ReadOnly));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidServerPhy", "Serveurs Membre", 's', 2, 0, FieldOption.Base));
            t.LstField.Add(new Field("GuidAppUser", "User Infra", 's', 2, 0, FieldOption.Base));
            t.LstField.Add(new Field("GuidApplication", "Application Infra", 's', 2, 0, FieldOption.Base));
            t.LstField.Add(new Field("GuidServer", "Serveurs Infra", 's', 2, 0, FieldOption.Base));
            //t.LstField.Add(new Field("NCard", "Network Interface", 's', 0, 0, FieldOption.ExternProperty | FieldOption.NonVisible));

            AddTable(t);
        }

        public void AddNatRule()
        {
            Table t = new Table("NatRule");

            t.LstField.Add(new Field("GuidNatRule", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomNatRule", LibNom, 's', 2, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("NCard", "Network Interface", 's', 0, 0, FieldOption.ExternProperty | FieldOption.ReadOnly));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void AddLocation()
        {
            Table t = new Table("Location");

            t.LstField.Add(new Field("GuidLocation", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible | FieldOption.Mandatory));
            t.LstField.Add(new Field("NomLocation", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory));
            t.LstField.Add(new Field("AccessPoint", "Access Point (C/L/E)", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("PtCnx", "Access Point", 's', 0, 0, FieldOption.ExternProperty | FieldOption.NonVisible));
            t.LstField.Add(new Field("Sens", "Sens (0/1)", 'i', 0, 0, FieldOption.Base | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void AddLayerLink()
        {
            Table t = new Table("LayerLink");

            t.LstField.Add(new Field("GuidObj", "GuidObj", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible | FieldOption.Mandatory));
            t.LstField.Add(new Field("GuidLayer", "GuidLayer", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.Mandatory));
            t.LstField.Add(new Field("GuidAppVersion", "GuidAppVersion", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible | FieldOption.Mandatory));

            AddTable(t);
        }

        public void AddLayer()
        {
            Table t = new Table("Layer");

            t.LstField.Add(new Field("GuidLayer", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible | FieldOption.Mandatory));
            t.LstField.Add(new Field("NomLayer", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory));
            t.LstField.Add(new Field("GuidAppVersion", "GuidAppVersion", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible | FieldOption.Mandatory));
            t.LstField.Add(new Field("GuidTemplate", "GuidTemplate", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible | FieldOption.Mandatory));

            AddTable(t);
        }

        public void AddBaiePhy() // Baie vue CTI
        {
            Table t = new Table("BaiePhy");
            t.LstField.Add(new Field("GuidBaiePhy", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Key));
            t.LstField.Add(new Field("NomBaiePhy", LibNom, 's', 2, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Concat));
            t.LstField.Add(new Field("NbrU", "Nombre de U", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Emplacement", "Emplacement", 's', 2, 0, FieldOption.InterneBD | FieldOption.Select));
            AddTable(t);
        }


        public void AddBaieCTI() //Baie Disque CTI
        {
            Table t = new Table("BaieCTI");
            FieldBaie(t);

            AddTable(t);
            int i = t.FindField(t.LstField, "NomBaie");
            if (i > -1)
            {
                Field f = (Field)t.LstField[i];
                f.GrpAffiche = 1;
                //f.fieldOption -= FieldOption.Concat;
            }
            i = t.FindField(t.LstField, "Emplacement");
            if (i > -1)
            {
                Field f = (Field)t.LstField[i];
                f.GrpAffiche = 1;
            }
        }

        public void AddBaieDPhy() //Baie Disque vue B-? & C-?
        {
            Table t = new Table("BaieDPhy");
            FieldBaie(t);

            AddTable(t);
        }

        public void AddBaie() // Baie Disque
        {
            Table t = new Table("Baie");
            FieldBaie(t);

            AddTable(t);
        }

        public void FieldBaie(Table t) // Baie Disque
        {
            t.LstField.Add(new Field("GuidBaie", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomBaie", LibNom, 's', 2, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt | FieldOption.Concat));
            t.LstField.Add(new Field("Capacite", "Capacité", 'd', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
            t.LstField.Add(new Field("NbrU", "Nbr de U", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
            t.LstField.Add(new Field("Emplacement", "Emplacement", 's', 2, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
            t.LstField.Add(new Field("Lun", "Dénombrement des Luns de la Baie", 's', 0, 0, FieldOption.NonVisible | FieldOption.LinkTv));
        }

        public void AddInterface()
        {
            Table t = new Table("Interface");

            t.LstField.Add(new Field("GuidInterface", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomInterface", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidLayer", "Layer", 's', 0, 0, FieldOption.Select | FieldOption.NomCourt | FieldOption.ReadOnly | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Localization", "Localization", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("TypeIb", "Icon Inf", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select));            
            t.LstField.Add(new Field("Role", "Role", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Access", "Access", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidProduitApp", "GuidProduitApp", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.NonVisible | FieldOption.TabNonVisible | FieldOption.ExternKeyTable, FindTable("ProduitApp")));
            t.LstField.Add(new Field("NomProduitApp", "Nom Produit", 's', 3, 0, FieldOption.ReadOnly | FieldOption.Select | FieldOption.CacheVal | FieldOption.NomCourt, "GuidProduitApp"));
            t.LstField.Add(new Field("GuidMainComposant", "Guid MainComposant", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));


            t.LstField.Add(new Field("GuidGInterface", "GuidGInterface", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("X", "X", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Y", "Y", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Width", "Width", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Height", "Height", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void AddBase()
        {
            Table t = new Table("Base");

            t.LstField.Add(new Field("GuidBase", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomBase", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Base", "Intance Name", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidLayer", "Layer", 's', 0, 0, FieldOption.Select | FieldOption.NomCourt | FieldOption.ReadOnly | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Charset", "Charset", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Role", "Role (DWH/TRA)", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select)); // DataWareHouse, Transactionnelle
            t.LstField.Add(new Field("SchemaBase", "Schema", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("LicOption", "Option Licence", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Volumetry", "Volumetry", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Mutualization", "Mutualization", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("TableSpaceName", "Tablespace Name", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("TableSpaceFile", "Tablespace File", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("TableSpaceSizeMax", "Tablespace SizeMax (Go)", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Parameter", "Parameters", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Save", "Save", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidMainComposant", "Guid MainComposant", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            t.LstField.Add(new Field("GuidGBase", "GuidGBase", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("X", "X", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Y", "Y", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Width", "Width", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Height", "Height", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void AddLun()
        {
            Table t = new Table("Lun");

            t.LstField.Add(new Field("GuidLun", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomLun", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidBaie", "Guid Baie", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.ObjKey));
            t.LstField.Add(new Field("Zone", "Dénombrement des Zones du Lun", 's', 0, 0, FieldOption.NonVisible | FieldOption.LinkTv));

            AddTable(t);
        }


        public void AddFile()
        {
            Table t = new Table("File");

            t.LstField.Add(new Field("GuidFile", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomFile", LibNom, 's', 2, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidLayer", "Layer", 's', 0, 0, FieldOption.Select | FieldOption.NomCourt | FieldOption.ReadOnly | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Type", "Type", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select)); //txt, rapport, DB, printing...
            t.LstField.Add(new Field("Path", "Path", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Size", "Size(Go)", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Nombre", "Nombre", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Role", "Role (conf,rpt,...)", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Access", "Access", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Archivage", "Archivage", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidMainComposant", "Guid MainComposant", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            t.LstField.Add(new Field("GuidFile", "GuidGFile", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("X", "X", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Y", "Y", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Width", "Width", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Height", "Height", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));


            AddTable(t);
        }

        public void AddQueue()
        {
            Table t = new Table("Queue");

            t.LstField.Add(new Field("GuidQueue", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomQueue", LibNom, 's', 2, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Type", "Type (Q/T)", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Utilisation", "Usage", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Size", "Size (Mo)", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Trace", "Trace", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Rejeux", "Rejeux", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidMainComposant", "Guid MainComposant", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            t.LstField.Add(new Field("GuidQueue", "GuidGQueue", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("X", "X", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Y", "Y", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Width", "Width", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Height", "Height", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void AddTechno()
        {
            Table t = new Table("Techno");

            t.LstField.Add(new Field("GuidTechno", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomTechnoRef", LibNom, 's', 1, 0, FieldOption.Select | FieldOption.ReadOnly));

            t.LstField.Add(new Field("Version", "Version", 's', 0, 0, FieldOption.Select | FieldOption.ReadOnly));
            t.LstField.Add(new Field("ValIndicator", "Date", 't', 0, 0, FieldOption.Select | FieldOption.ReadOnly)); // DateFinMain dans la table IndicatorLink avec NomIndicator='1-support'
            t.LstField.Add(new Field("Norme", "N&S", 'p', 0, 0, FieldOption.Select | FieldOption.ReadOnly)); // index Picture
            t.LstField.Add(new Field("IndexImgOs", "Index Img Os", 'i', 0, 0, FieldOption.Select | FieldOption.ReadOnly));

            t.LstField.Add(new Field("GuidTechnoRef", "Guid Techno Ref", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.NonVisible | FieldOption.TabNonVisible | FieldOption.ExternKeyTable, FindTable("TechnoRef")));
            t.LstField.Add(new Field("GuidTechnoHost", "GuidParent", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("InLineUsed", "Usage", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("InLineTree", "Choix", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("InLineDescription", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));


            AddTable(t);
        }

        public void AddMCompServ()
        {
            Table t = new Table("MCompServ");

            t.LstField.Add(new Field("GuidMCompServ", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Key));
            t.LstField.Add(new Field("NomMainComposantRef", LibNom, 's', 1, 0, FieldOption.Select | FieldOption.ReadOnly));
            t.LstField.Add(new Field("GuidMainComposantRef", "Guid MainComposant Ref", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.NonVisible | FieldOption.TabNonVisible | FieldOption.ExternKeyTable, FindTable("MainComposantRef")));
            t.LstField.Add(new Field("GuidMainComposant", "GuidParent", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select));

            AddTable(t);
        }

        public void AddPackage()
        {
            Table t = new Table("Package");

            //t.LstField.Add(new Field("GuidMCompServ", "GuidMCompServ", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.NonVisible | FieldOption.TabNonVisible| FieldOption.Select | FieldOption.Key | FieldOption.Mandatory | FieldOption.ExternKeyTable, FindTable("MCompServ")));
            //t.LstField.Add(new Field("GuidVue", "GuidVue", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.NonVisible | FieldOption.TabNonVisible | FieldOption.Select | FieldOption.Key | FieldOption.Mandatory | FieldOption.ExternKeyTable, FindTable("Vue")));
            t.LstField.Add(new Field("GuidMCompServ", "GuidMCompServ", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.NonVisible | FieldOption.TabNonVisible | FieldOption.Select | FieldOption.Key | FieldOption.Mandatory ));
            t.LstField.Add(new Field("GuidVue", "GuidVue", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.NonVisible | FieldOption.TabNonVisible | FieldOption.Select | FieldOption.Key | FieldOption.Mandatory));
            t.LstField.Add(new Field("GuidParam", "GuidParam", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.NonVisible | FieldOption.TabNonVisible | FieldOption.Select | FieldOption.Key | FieldOption.Mandatory | FieldOption.ExternKeyTable, FindTable("Param")));
            t.LstField.Add(new Field("Value", "Value", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Mandatory));

            AddTable(t);
        }

        public void AddParam()
        {
            Table t = new Table("Param");

            t.LstField.Add(new Field("GuidParam", "GuidParam", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.NonVisible | FieldOption.TabNonVisible | FieldOption.Select | FieldOption.Key | FieldOption.Mandatory | FieldOption.ExternKeyTable | FieldOption.SearchKey, FindTable("ParamLink")));
            t.LstField.Add(new Field("NomParam", "NomParam", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Mandatory));


            AddTable(t);
        }


        public void AddParamLink()
        {
            Table t = new Table("ParamLink");

            t.LstField.Add(new Field("GuidParam", "GuidParam", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.NonVisible | FieldOption.TabNonVisible | FieldOption.Select | FieldOption.Key | FieldOption.Mandatory));
            t.LstField.Add(new Field("GuidCadreRefApp", "GuidCadreRefApp", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.NonVisible | FieldOption.TabNonVisible | FieldOption.Select | FieldOption.Key | FieldOption.Mandatory | FieldOption.ExternKeyTable, FindTable("CadreRefApp")));


            AddTable(t);
        }

        public void AddMainComposantRef()
        {
            Table t = new Table("MainComposantRef");

            t.ExternalKeyTableOption = FieldOption.InterneBD;
            t.LstField.Add(new Field("GuidMainComposantRef", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key));
            t.LstField.Add(new Field("NomMainComposantRef", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.ObjKey));
            t.LstField.Add(new Field("Version", "Version", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            //t.LstField.Add(new Field("DateFinMain", "Date", 't', 0, 0, FieldOption.InterneBD | FieldOption.Select)); 
            t.LstField.Add(new Field("GuidProduitApp", "GuidProduitApp", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.ForeignKey | FieldOption.ReadOnly | FieldOption.ExternKeyTable, FindTable("ProduitApp")));

            AddTable(t);
        }

        public void AddTechnoRef()
        {
            Table t = new Table("TechnoRef");

            t.ExternalKeyTableOption = FieldOption.InterneBD;
            t.LstField.Add(new Field("GuidTechnoRef", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomTechnoRef", LibNom, 's', 0, 185, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Version", "Version", 's', 0, 100, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("NormeG", "N&S G", 'p', 0, 60, FieldOption.InterneBD | FieldOption.Select)); // index Picture
            t.LstField.Add(new Field("Norme", "N&S", 'p', 0, 60, FieldOption.InterneBD | FieldOption.Select)); // index Picture
            t.LstField.Add(new Field("IndexImgOs", "IndexImgOs", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidProduit", "GuidProduit", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.ForeignKey | FieldOption.TabNonVisible | FieldOption.ExternKeyTable, FindTable("Produit")));
            t.LstField.Add(new Field("Temp", "Temp", 's', 0, 0, FieldOption.TabNonVisible));
            //t.LstField.Add(new Field("Status", "Status", 'q', 0, 60, FieldOption.Select));
            t.LstField.Add(new Field("UpComingStart", "UStart", 't', 0, 60, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("UpComingEnd", "UEnd", 't', 0, 60, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("ReferenceStart", "RStart", 't', 0, 60, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("ReferenceEnd", "REnd", 't', 0, 60, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("ConfinedStart", "CStart", 't', 0, 60, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("ConfinedEnd", "CEnd", 't', 0, 60, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("DecommStart", "DStart", 't', 0, 60, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("DecommEnd", "DEnd", 't', 0, 60, FieldOption.InterneBD | FieldOption.Select));

            AddTable(t);
        }

        /*
        public void AddIndicator()
        {
            Table t = new Table("Indicator");

            t.LstField.Add(new Field("GuidIndicator", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomIndicator", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Indicator", "Indicator", 'a', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidParent", "GuidParent", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.ForeignKey | FieldOption.TabNonVisible));
            //t.LstField.Add(new Field("GuidIndicatorRef", "GuidIndicatorRef", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.ForeignKey | FieldOption.TabNonVisible));

            AddTable(t);
        }
        */

        
        public void AddIndicator()
        {
            Table t = new Table("Indicator");

            t.LstField.Add(new Field("GuidIndicator", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomIndicator", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Type", "Type", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidIndicatorRef", "GuidIndicatorRef", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.ForeignKey | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void AddGenks()
        {
            Table t = new Table("Genks");

            t.LstField.Add(new Field("GuidGenks", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomGenks", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("TypeIt", "Icon Sup", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidLabel", "Label", 's', 0, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.NomCourt | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void AddGensas()
        {
            Table t = new Table("Gensas");

            t.LstField.Add(new Field("GuidGensas", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomGensas", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("TypeIt", "Icon Sup", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidAppVersion", "Guid AppVersion", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidLabel", "Label", 's', 0, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.NomCourt | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidManagedsvc", "GuidManagedsvc", 's', 0, 0, FieldOption.ReadOnly | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomManagedsvc", "Service Managé", 's', 3, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.NomCourt, "GuidManagedsvc"));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void AddManagedsvc()
        {
            Table t = new Table("Managedsvc");

            t.LstField.Add(new Field("GuidManagedsvc", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomManagedsvc", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select));
            //t.LstField.Add(new Field("TypeIt", "Icon Sup", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidGensas", "Guid Gensas", 's', 0, 0, FieldOption.ReadOnly));
            AddTable(t);
        }

        public void AddInsks()
        {
            Table t = new Table("Insks");

            t.LstField.Add(new Field("GuidInsks", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomInsks", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("NameSpace", "Name Space", 's', 0, 0, FieldOption.ExternProperty | FieldOption.ReadOnly));
            t.LstField.Add(new Field("TypeIt", "Icon Sup", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidGenks", "Guid Genks", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidLabel", "Label", 's', 0, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.NomCourt | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void AddInsnd()
        {
            Table t = new Table("Insnd");

            t.LstField.Add(new Field("GuidInsnd", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomInsnd", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("TypeIt", "Icon Sup", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidInsks", "Guid Insks", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidLabel", "Label", 's', 0, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.NomCourt | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void AddInsns()
        {
            Table t = new Table("Insns");

            t.LstField.Add(new Field("GuidInsns", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomInsns", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("TypeIt", "Icon Sup", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidInsks", "Guid Insks", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidVue", "Guid Vue", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Inspod", "Pods", 's', 0, 0, FieldOption.ExternProperty | FieldOption.NonVisible));
            t.LstField.Add(new Field("GuidLabel", "Label", 's', 0, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.NomCourt | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void AddInspod()
        {
            Table t = new Table("Inspod");

            t.LstField.Add(new Field("GuidInspod", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomInspod", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("TypeIt", "Icon Sup", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidInsns", "Guid Insnc", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidGenpod", "Guid Gendpod", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Insing", "Ingres Services", 's', 0, 0, FieldOption.ExternProperty | FieldOption.NonVisible));
            t.LstField.Add(new Field("Inssvc", "Services", 's', 0, 0, FieldOption.ExternProperty | FieldOption.NonVisible));
            t.LstField.Add(new Field("GuidLabel", "Label", 's', 0, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.NomCourt | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void AddInssvc()
        {
            Table t = new Table("Inssvc");

            t.LstField.Add(new Field("GuidInssvc", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomInssvc", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("TypeIt", "Icon Sup", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidInspod", "Guid Inspod", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidGensvc", "Guid Gensvc", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NCard", "Network Interface", 's', 0, 0, FieldOption.ExternProperty | FieldOption.NonVisible));
            t.LstField.Add(new Field("GuidLabel", "Label", 's', 0, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.NomCourt | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void AddFieldInssas(Table t)
        {
            t.LstField.Add(new Field("GuidInssas", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomInssas", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("TypeIt", "Icon Sup", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidGensas", "Guid Gensas", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidLabel", "Label", 's', 0, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.NomCourt | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidManagedsvc", "GuidManagedsvc", 's', 0, 0, FieldOption.ReadOnly | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomManagedsvc", "Service Managé", 's', 3, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.NomCourt, "GuidManagedsvc"));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
        }

        public void AddInssas()
        {
            Table t = new Table("Inssas");

            AddFieldInssas(t);
            
            AddTable(t);
        }

        public void AddInsing()
        {
            Table t = new Table("Insing");

            t.LstField.Add(new Field("GuidInsing", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomInsing", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("TypeIt", "Icon Sup", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidInspod", "Guid Inspod", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidGening", "Guid Gensvc", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NCard", "Network Interface", 's', 0, 0, FieldOption.ExternProperty | FieldOption.NonVisible));
            t.LstField.Add(new Field("GuidApplication", "Application Infra", 's', 2, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.Concat | FieldOption.NomCourt));
            t.LstField.Add(new Field("GuidLabel", "Label", 's', 0, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.NomCourt | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void AddGenpod()
        {
            Table t = new Table("Genpod");

            t.LstField.Add(new Field("GuidGenpod", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomGenpod", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Replicas", "Replicas", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Componant", "Componant", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("TypeIt", "Icon Sup", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidGenks", "Guid Genks", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidLabel", "Label", 's', 0, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.NomCourt | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void AddContainer()
        {
            Table t = new Table("Container");

            t.LstField.Add(new Field("GuidContainer", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomContainer", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Image", "Image", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("LiveProbe", "Liveness Probe", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("ReadProbe", "Readiness Probe", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("varenv", "var env", 's', 0, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.NomCourt));
            //t.LstField.Add(new Field("TypeIt", "Icon Sup", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidGenpod", "Guid Genpod", 's', 0, 0, FieldOption.ReadOnly));

            AddTable(t);
        }

        public void AddVarenv()
        {
            Table t = new Table("Varenv");

            t.LstField.Add(new Field("GuidVarenv", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomVarenv", "NomVarenv", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("ValeurVarenv", "ValeurVarnev", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidGenpod", "GuidGenpod", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.ForeignKey | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidContainer", "GuidContainer", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.ForeignKey | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void AddGening()
        {
            Table t = new Table("Gening");

            t.LstField.Add(new Field("GuidGening", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomGening", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("TypeIt", "Icon Sup", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidGenks", "Guid Genks_", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidLabel", "Label", 's', 0, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.NomCourt | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void AddGensvc()
        {
            Table t = new Table("Gensvc");

            t.LstField.Add(new Field("GuidGensvc", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomGensvc", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("TypeIt", "Icon Sup", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidGenks", "Guid Genks_", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidLabel", "Label", 's', 0, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.NomCourt | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void AddTechnoArea()
        {
            Table t = new Table("TechnoArea");

            t.LstField.Add(new Field("GuidTechnoArea", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomTechnoArea", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));

            AddTable(t);
        }

        public void AddServer()
        {
            Table t = new Table("Server");

            FieldServer(t);

            t.LstField.Add(new Field("GuidGServer", "GuidGServer", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("X", "X", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Y", "Y", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Width", "Width", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Height", "Height", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void FieldServer1(Table t)
        {
            t.LstField.Add(new Field("GuidServer", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Key | FieldOption.Mandatory));
            t.LstField.Add(new Field("NomServer", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory | FieldOption.Concat));
            t.LstField.Add(new Field("GuidMainFonction", "GuidMainFonction", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.NonVisible | FieldOption.ExternKeyTable, FindTable("Fonction")));
            t.LstField.Add(new Field("GuidLabel", "Label", 's', 0, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.NomCourt | FieldOption.TabNonVisible));
        }

        public void FieldServer(Table t)
        {
            FieldServer1(t);
            t.LstField.Add(new Field("NomFonction", "Fonction Principale", 's', 1, 0, FieldOption.ReadOnly | FieldOption.Select | FieldOption.CacheVal | FieldOption.NomCourt, "GuidMainFonction"));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

        }

        public void FieldApplication1(Table t)
        {
            t.LstField.Add(new Field("NomApplication", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Trigramme", "Trigramme", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("CodeAp", "Code AP (AUID)", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
        }

        public void FieldApplication2(Table t)
        {
            t.LstField.Add(new Field("GuidCadreRef", "Cadre Référence", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.ReadOnly | FieldOption.NonVisible | FieldOption.TabNonVisible | FieldOption.ExternKeyTable, FindTable("CadreRefFonc")));
            t.LstField.Add(new Field("NomCadreRefFonc", "Cadre Ref", 's', 0, 0, FieldOption.ReadOnly | FieldOption.Select | FieldOption.CacheVal | FieldOption.TabNonVisible | FieldOption.NomCourt, "GuidCadreRef"));
        }

        public void AddApplication()
        {
            Table t = new Table("Application");

            t.ExternalKeyTableOption = FieldOption.InterneBD;
            t.LstField.Add(new Field("GuidApplication", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Key | FieldOption.NonVisible| FieldOption.TabNonVisible));
            FieldApplication1(t);

            //t.LstField.Add(new Field("GuidAppVersion", "Version", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select)); // | FieldOption.ExternKeyTable, FindTable("AppVersion")));
            t.LstField.Add(new Field("GuidAppVersion", "GuidAppVersion", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Version", "Version Ref", 's', 0, 0, FieldOption.ReadOnly | FieldOption.Select | FieldOption.CacheVal | FieldOption.NomCourt, "GuidAppVersion"));
            t.LstField.Add(new Field("GuidLabel", "Label", 's', 0, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.NomCourt | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidApplicationType", "Application Type", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.NonVisible | FieldOption.TabNonVisible | FieldOption.ExternKeyTable, FindTable("ApplicationType")));
            t.LstField.Add(new Field("ApplicationType", "Type", 's', 0, 0, FieldOption.NonVisible | FieldOption.ExternProperty));
            t.LstField.Add(new Field("GuidApplicationClass", "Application Class", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.NonVisible | FieldOption.TabNonVisible | FieldOption.ExternKeyTable, FindTable("ApplicationClass")));
            t.LstField.Add(new Field("ApplicationClass", "Classe", 's', 0, 0, FieldOption.NonVisible | FieldOption.ExternProperty));
            t.LstField.Add(new Field("TypeIb", "Icon Inf (1=tech/2=bus)", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Image", "Image", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Installee", "App Installee (0/1)", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            FieldApplication2(t);
            t.LstField.Add(new Field("GuidArborescence", "Arborescence", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.NonVisible | FieldOption.TabNonVisible | FieldOption.ExternKeyTable, FindTable("Arborescence")));
            t.LstField.Add(new Field("Indicator", "Indicateur", 's', 0, 0, FieldOption.ReadOnly | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PWord", "Texte", 's', 0, 0, FieldOption.ReadOnly | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            t.LstField.Add(new Field("GuidGApplication", "GuidGApplication", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("X", "X", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Y", "Y", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Width", "Width", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Height", "Height", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void FieldAppVersion1(Table t)
        {
            t.LstField.Add(new Field("Version", "Version", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
        }

        public void AddAppVersion()
        {
            Table t = new Table("AppVersion");

            t.ExternalKeyTableOption = FieldOption.InterneBD;
            t.LstField.Add(new Field("GuidAppVersion", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Key | FieldOption.NonVisible | FieldOption.TabNonVisible));
            FieldAppVersion1(t);
            t.LstField.Add(new Field("Statut", "Statut", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidApplication", "Guid Application", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidStatut", "Statut", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.NonVisible | FieldOption.ExternKeyTable, FindTable("Statut")));
            t.LstField.Add(new Field("GuidLayer", "Layer", 's', 0, 0, FieldOption.Select | FieldOption.NomCourt | FieldOption.ReadOnly | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void AddAppUser()
        {
            Table t = new Table("AppUser");

            t.LstField.Add(new Field("GuidAppUser", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.Key));
            t.LstField.Add(new Field("NomAppUser", LibNom, 's', 1, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidLayer", "Layer", 's', 0, 0, FieldOption.Select | FieldOption.NomCourt | FieldOption.ReadOnly | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("TypeIb", "Icon Inf", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Image", "Image", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Identification", "Identification", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Authentification", "Authentification", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Access", "Access", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));

            t.LstField.Add(new Field("GuidGAppUser", "GuidGAppUser", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("X", "X", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Y", "Y", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Width", "Width", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Height", "Height", 'i', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            
            t.LstField.Add(new Field("PropDefinition", "Definition", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PropScope", "Scope", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PropNumber", "Number", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));



            AddTable(t);
        }

        
        public void AddDiskClass()
        {
            Table t = new Table("DiskClass");
            t.LstField.Add(new Field("GuidDiskClass", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory | FieldOption.Key));
            t.LstField.Add(new Field("NomDiskClass", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory));

            AddTable(t);
        }

        public void AddEnvironnement()
        {
            Table t = new Table("Environnement");

            t.LstField.Add(new Field("GuidEnvironnement", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.Mandatory));
            t.LstField.Add(new Field("NomEnvironnement", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory));
            t.LstField.Add(new Field("Lettre", "Lettre", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidTypeVue", "GuidTypeVue", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));

            AddTable(t);
        }
        public void AddExploitClass()
        {
            Table t = new Table("ExploitClass");
            t.LstField.Add(new Field("GuidExploitClass", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.Mandatory));
            t.LstField.Add(new Field("NomExploitClass", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory));

            AddTable(t);
        }

        public void FieldFonction1(Table t)
        {
            t.LstField.Add(new Field("Image", "Image", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
        }

        public void AddFonction1()
        {
            Table t = new Table("Fonction");
            t.LstField.Add(new Field("GuidFonction", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory | FieldOption.Key));
            t.LstField.Add(new Field("NomFonction", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory));
            FieldFonction1(t);
            
            AddTable(t);
        }

        public void AddFonction()
        {
            Table t = new Table("Fonction");
            t.LstField.Add(new Field("GuidFonction", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory | FieldOption.Key));
            t.LstField.Add(new Field("NomFonction", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory));
            FieldFonction1(t);

            AddTable(t);
        }

        public void AddFonctionService()
        {
            Table t = new Table("FonctionService");

            t.LstField.Add(new Field("GuidFonctionService", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory | FieldOption.Key | FieldOption.ForceSearchKey));
            t.LstField.Add(new Field("NomFonctionService", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory | FieldOption.NomRef));

            AddTable(t);
        }

        public void FieldApplicationType1(Table t)
        {
            t.LstField.Add(new Field("NomApplicationType", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory | FieldOption.NomRef));
        }

        public void AddApplicationType()
        {
            Table t = new Table("ApplicationType");
            t.LstField.Add(new Field("GuidApplicationType", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory | FieldOption.Key));
            FieldApplicationType1(t);

            AddTable(t);
        }

        public void AddArborescence()
        {
            Table t = new Table("Arborescence");

            t.LstField.Add(new Field("GuidArborescence", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory | FieldOption.Key));
            t.LstField.Add(new Field("NomArborescence", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory | FieldOption.NomRef));
            t.LstField.Add(new Field("GuidParent", "GuidParent", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));

            AddTable(t);
        }

        public void AddBackupClass()
        {
            Table t = new Table("BackupClass");
            t.LstField.Add(new Field("GuidBackupClass", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.Mandatory));
            t.LstField.Add(new Field("NomBackupClass", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory));

            AddTable(t);
        }

        public void AddStatut()
        {
            Table t = new Table("Statut");
            t.LstField.Add(new Field("GuidStatut", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory | FieldOption.Key));
            t.LstField.Add(new Field("NomStatut", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory));

            AddTable(t);
        }

        public void AddTemplate()
        {
            Table t = new Table("Template");
            t.LstField.Add(new Field("GuidTemplate", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory | FieldOption.Key));
            t.LstField.Add(new Field("NomTemplate", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory));

            AddTable(t);
        }

        public void AddCadreRef()
        {
            Table t = new Table("CadreRef");
            AddTable(t);

            t.ExternalKeyTableOption = FieldOption.InterneBD;
            t.LstField.Add(new Field("GuidCadreRef", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomCadreRef", LibNom, 's', 2, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("TypeCadreRef", "TypeCadreRef", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidParent", "GuidParent", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.ForeignKey | FieldOption.TabNonVisible | FieldOption.ExternKeyTable, FindTable("CadreRef")));
            t.LstField.Add(new Field("NomVue", "Nom de la Vue", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Couleur", "Couleur (ARGB)", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.ReadOnly | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("Techno", "Techno", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Instance", "Instance", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("ConfinedStart", "ConfinedStart", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("InstanceConfinedStart", "InstanceConfinedStart", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select));

        }

        public void AddCadreRefApp()
        {
            Table t = new Table("CadreRefApp");
            t.LstField.Add(new Field("GuidCadreRefApp", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.Mandatory));
            t.LstField.Add(new Field("NomCadreRefApp", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory));
            t.LstField.Add(new Field("GuidParentApp", "GuidParentApp", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory));

            AddTable(t);
        }

        public void AddCadreRefFonc()
        {
            Table t = new Table("CadreRefFonc");
            t.LstField.Add(new Field("GuidCadreRefFonc", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory | FieldOption.Key));
            t.LstField.Add(new Field("NomCadreRefFonc", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory | FieldOption.NomRef));
            t.LstField.Add(new Field("GuidParentFonc", "GuidParentFonc", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory));

            AddTable(t);
        }

        public void FieldApplicationClass1(Table t)
        {
            t.LstField.Add(new Field("NomApplicationClass", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory | FieldOption.NomRef));
            t.LstField.Add(new Field("RTO", "RTO", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory));
            t.LstField.Add(new Field("RPO", "RPO", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory));
        }

        public void AddApplicationClass()
        {
            Table t = new Table("ApplicationClass");
            t.LstField.Add(new Field("GuidApplicationClass", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory | FieldOption.Key));
            FieldApplicationClass1(t);
            t.LstField.Add(new Field("Description", "Description", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));

            AddTable(t);
        }

        public void AddOptionsDraw()
        {
            Table t = new Table("OptionsDraw");

            t.LstField.Add(new Field("NumOption", "NumOption", 'i', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key));
            t.LstField.Add(new Field("Parameter", "Parameter", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));

            AddTable(t);
        }

        public void AddVue()
        {
            Table t = new Table("Vue");

            t.LstField.Add(new Field("GuidVue", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("NomVue", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            //t.LstField.Add(new Field("ID", "ID Vue", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            //t.LstField.Add(new Field("Type", "Type", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidGVue", "GuidG", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidEnvironnement", "Guid Env", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible | FieldOption.ExternKeyTable, FindTable("Environnement")));
            t.LstField.Add(new Field("GuidLabel", "Label", 's', 0, 0, FieldOption.ReadOnly | FieldOption.CacheVal | FieldOption.NomCourt | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidEcosystem", "Guid Ecosystem", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.ReadOnly | FieldOption.NonVisible | FieldOption.TabNonVisible | FieldOption.ExternKeyTable, FindTable("Ecosystem")));
            t.LstField.Add(new Field("NomEcosystem", "Ecosystem", 's', 0, 0, FieldOption.ReadOnly | FieldOption.Select | FieldOption.CacheVal | FieldOption.NomCourt, "GuidEcosystem"));
            t.LstField.Add(new Field("IdEcosystem", "IdEcosystem", 's', 0, 0, FieldOption.ReadOnly | FieldOption.Select | FieldOption.NomCourt));
            t.LstField.Add(new Field("GuidTypeVue", "Type Vue", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible | FieldOption.ExternKeyTable, FindTable("TypeVue")));
            t.LstField.Add(new Field("GuidAppVersion", "Guid App", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("GuidVueInf", "Guid Vue Inferieure", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PWord", "Texte", 's', 0, 0, FieldOption.ReadOnly));
            t.LstField.Add(new Field("PropDesc", "Description", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PropUser", "User", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PropModule", "Module", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PropContainer", "Container", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));
            t.LstField.Add(new Field("PropApplication", "Application", 's', 0, 0, FieldOption.NonVisible | FieldOption.TabNonVisible));

            AddTable(t);
        }

        public void AddTypeVue()
        {
            Table t = new Table("TypeVue");

            t.LstField.Add(new Field("GuidTypeVue", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.Mandatory));
            t.LstField.Add(new Field("NomTypeVue", LibNom, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Mandatory));
            t.LstField.Add(new Field("ID", "ID", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GroupVue", "GroupVue", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("GuidTypeVueInf", "GuidTypeVueInf", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));

            AddTable(t);
        }

        public void AddStaticTable()
        {
            Table t = new Table("StaticTable");

            t.LstField.Add(new Field("GuidStaticTable", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key));
            t.LstField.Add(new Field("GuidStaticProfil", "GuidStaticProfil", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key));
            t.LstField.Add(new Field("Propriete", "Propriete", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));
            t.LstField.Add(new Field("Val", "Val", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select));

            AddTable(t);
        }

        public void AddServiceLink()
        {
            Table t = new Table("ServiceLink");

            //t.LstField.Add(new Field("GuidApplicationClass", "Application Class", 's', 0, 0, FieldOption.InterneBD | FieldOption.ReadOnly | FieldOption.Select | FieldOption.NonVisible | FieldOption.ExternKeyTable, FindTable("ApplicationClass")));
            t.LstField.Add(new Field("GuidGroupService", "GuidGroupService", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.Mandatory));
            t.LstField.Add(new Field("GuidEnvironnement", "GuidEnvironnement", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.Mandatory | FieldOption.ExternKeyTable, FindTable("Environnement")));
            t.LstField.Add(new Field("GuidService", "GuidService", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.Mandatory | FieldOption.ExternKeyTable, FindTable("Service")));

            AddTable(t);
        }

        public void AddVlanClass()
        {
            Table t = new Table("VlanClass");

            FieldVLanClass1(t);
            
            AddTable(t);
        }

        public void AddTabServerList()
        {
            
            int n = FindTable("ServerList");
            if (n == -1)
            {
                Table t = new Table("ServerList");

                t.LstField.Add(new Field("NomApplication", "Application", 's', 0, 65, FieldOption.NomCourt));
                t.LstField.Add(new Field("Lettre", "Env", 's', 0, 20, FieldOption.NomCourt));
                t.LstField.Add(new Field("NomServer", "Fonction", 's', 0, 65, FieldOption.NomCourt));
                t.LstField.Add(new Field("NomServerPhy", "Serveur", 's', 0, 65, FieldOption.NomCourt));
                t.LstField.Add(new Field("Type", "Type", 's', 0, 25, FieldOption.NomCourt));
                t.LstField.Add(new Field("NomVLan", "DMZ", 's', 0, 150, FieldOption.NomCourt));
                t.LstField.Add(new Field("NumVlan", "DMZ", 's', 0, 65, FieldOption.NomCourt));
                t.LstField.Add(new Field("IPAddr", "IP", 's', 0, 65, FieldOption.NomCourt));
                t.LstField.Add(new Field("CPUCore", "cpu", 'd', 0, 20, FieldOption.NomCourt));
                t.LstField.Add(new Field("RAM", "ram", 'd', 0, 20, FieldOption.NomCourt));
                t.LstField.Add(new Field("DiskData", "disk", 'd', 0, 20, FieldOption.NomCourt));

                AddTable(t);
            }
        }

        public void AddTabServerTechList()
        {

            int n = FindTable("ServerTechList");
            if (n == -1)
            {
                Table t = new Table("ServerTechList");

                t.LstField.Add(new Field("NomApplication", "Application", 's', 0, 65, FieldOption.NomCourt));
                t.LstField.Add(new Field("Trigramme", "Trigramme", 's', 0, 20, FieldOption.NomCourt));
                t.LstField.Add(new Field("Lettre", "Env", 's', 0, 20, FieldOption.NomCourt));
                t.LstField.Add(new Field("NomServer", "Fonction", 's', 0, 65, FieldOption.NomCourt));
                t.LstField.Add(new Field("NomServerPhy", "Serveur", 's', 0, 65, FieldOption.NomCourt));
                t.LstField.Add(new Field("NomTechnoRef", "Techno", 's', 0, 65, FieldOption.NomCourt));

                AddTable(t);
            }
        }

        public void AddTabSan()
        {
            int n = FindTable("TabSan");
            if (n == -1)
            {
                Table t = new Table("TabSan");

                t.LstField.Add(new Field("Switch", "Switch", 's', 0, 70, FieldOption.Select));
                t.LstField.Add(new Field("Zone", "Zone", 's', 0, 111, FieldOption.Select));
                //t.LstField.Add(new Field("Hote", "Hote", 's', 0, 0, FieldOption.Base));
                //t.LstField.Add(new Field("SanCardH", "SanCard Hote", 's', 0, 0, FieldOption.Base));
                t.LstField.Add(new Field("AliasH", "Alias Hote", 's', 0, 70, FieldOption.Select));
                t.LstField.Add(new Field("WWNH", "WWN Hote", 's', 0, 112, FieldOption.Select));
                //t.LstField.Add(new Field("Baie", "Baie", 's', 0, 0, FieldOption.Base));
                //t.LstField.Add(new Field("SanCardB", "SanCard Baie", 's', 0, 0, FieldOption.Base));
                t.LstField.Add(new Field("AliasB", "Alias Baie", 's', 0, 70, FieldOption.Select));
                t.LstField.Add(new Field("WWNB", "WWN Baie", 's', 0, 112, FieldOption.Select));

                AddTable(t);
            }
        }

        public void AddTabVersion()
        {
            int n = FindTable("TabVersion");
            if (n == -1)
            {
                Table t = new Table("TabVersion");

                t.LstField.Add(new Field("Version", "Version", 's', 0, 100, FieldOption.Select));
                t.LstField.Add(new Field("Date", "Date", 's', 0, 100, FieldOption.Select));
                t.LstField.Add(new Field("Commentaire", "Commentaire", 's', 0, 600, FieldOption.Select));
                t.LstField.Add(new Field("Architecte", "Architecte", 's', 0, 150, FieldOption.Select));

                AddTable(t);
            }
        }

        public void AddTabEACB()
        {
            
            int n = FindTable("TabEACB");
            if (n == -1)
            {
                Table t = new Table("TabEACB");

                t.LstField.Add(new Field("Id", "Id", 's', 0, 10, FieldOption.NomCourt));
                t.LstField.Add(new Field("NomFlux", "Nom Flux", 's', 0, 65, FieldOption.NomCourt));
                t.LstField.Add(new Field("LocSrc", "Location Src", 's', 0, 100, FieldOption.NomCourt));
                t.LstField.Add(new Field("NomSrc", "Nom Src", 's', 0, 65, FieldOption.NomCourt));
                t.LstField.Add(new Field("GuidNCardSrc", "Guid NCard Src", 's', 0, 0, FieldOption.NomCourt | FieldOption.TabNonVisible));
                t.LstField.Add(new Field("IPSrc", "IP Src", 's', 0, 65, FieldOption.NomCourt));
                t.LstField.Add(new Field("IPNatSrc", "IP Nat Src", 's', 0, 65, FieldOption.NomCourt));
                t.LstField.Add(new Field("GuidVlanSrc", "Vlan Src", 's', 0, 0, FieldOption.NomCourt | FieldOption.TabNonVisible));
                t.LstField.Add(new Field("GuidVlanClassSrc", "VlanClass Src", 's', 0, 0, FieldOption.NomCourt | FieldOption.TabNonVisible));
                t.LstField.Add(new Field("LocCbl", "Location Cbl", 's', 0, 100, FieldOption.NomCourt));
                t.LstField.Add(new Field("NomCbl", "Nom Cbl", 's', 0, 65, FieldOption.NomCourt));
                t.LstField.Add(new Field("GuidNCardCbl", "Guid NCard Cbl", 's', 0, 0, FieldOption.NomCourt | FieldOption.TabNonVisible));
                t.LstField.Add(new Field("IPCbl", "IP Cbl", 's', 0, 65, FieldOption.NomCourt));
                t.LstField.Add(new Field("IPNatCbl", "IP Nat", 's', 0, 65, FieldOption.NomCourt));
                t.LstField.Add(new Field("GuidVlanCbl", "Vlan Cbl", 's', 0, 0, FieldOption.NomCourt | FieldOption.TabNonVisible));
                t.LstField.Add(new Field("GuidVlanClassCbl", "VlanClass Cbl", 's', 0, 0, FieldOption.NomCourt | FieldOption.TabNonVisible));
                t.LstField.Add(new Field("GuidGroupService", "GroupService", 's', 0, 0, FieldOption.NomCourt | FieldOption.TabNonVisible));
                t.LstField.Add(new Field("Service", "Service", 's', 0, 40, FieldOption.NomCourt));
                t.LstField.Add(new Field("Protocol", "Protocole", 's', 0, 40, FieldOption.NomCourt));
                t.LstField.Add(new Field("Ports", "Ports", 's', 0, 40, FieldOption.NomCourt));

                AddTable(t);
            }
        }

        public void AddTabAppforCAT()
        {
            int n = FindTable("TabAppforCAT");
            if (n == -1)
            {
                Table t = new Table("TabAppforCAT");

                FieldApplication1(t);
                FieldAppVersion1(t);
                FieldApplicationType1(t);
                FieldApplicationClass1(t);

                AddTable(t);
            }
        }

        public void AddTabApp()
        {
            int n = FindTable("TabApp");
            if (n == -1)
            {
                Table t = new Table("TabApp");

                t.LstField.Add(new Field("Nom", "Nom Application", 's', 0, 65, FieldOption.NomCourt));
                t.LstField.Add(new Field("Status", "Status", 'p', 0, 50, FieldOption.NomCourt));

                AddTable(t);
            }
        }

        public void AddTabPackage()
        {
            int n = FindTable("TabPackage");
            if (n == -1)
            {
                Table t = new Table("TabPackage");

                t.LstField.Add(new Field("NomMainComposant", "Composant principal", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
                t.LstField.Add(new Field("NomMainComposantRef", "Composant", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
                t.LstField.Add(new Field("NomProduitApp", "Produit", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
                t.LstField.Add(new Field("NomParam", "Param", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));
                t.LstField.Add(new Field("Value", "Value", 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.NomCourt));

                AddTable(t);
            }
        }

        public void AddTabTechnoRef()
        {
            int n = FindTable("TabTechnoRef");
            if (n == -1)
            {
                Table t = new Table("TabTechnoRef");

                t.LstField.Add(new Field("GuidTechnoRef", LibGuid, 's', 0, 0, FieldOption.InterneBD | FieldOption.Select | FieldOption.Key | FieldOption.TabNonVisible));
                t.LstField.Add(new Field("NomTechnoRef", LibNom, 's', 0, 185, FieldOption.InterneBD | FieldOption.Select));
                t.LstField.Add(new Field("Version", "Version", 's', 0, 100, FieldOption.InterneBD | FieldOption.Select));
                t.LstField.Add(new Field("ValIndicaor", "Date", 't', 0, 120, FieldOption.InterneBD | FieldOption.Select)); // DateFinMain dans la table IndicatorLink avec NomIndicator='1-support'
                t.LstField.Add(new Field("Status", "Status", 'q', 0, 60, FieldOption.Select));
                t.LstField.Add(new Field("Norme", "N&S", 'p', 0, 60, FieldOption.InterneBD | FieldOption.Select)); // index Picture
                AddTable(t);
            }
        }
    }

    public class xmlFlux
    {
        public XmlElement elFlux;
        public List<xmlFlux> lstElChild;
    }

    public class Table
    {
        public string Name;
        public string ReadExtention;
        public ArrayList LstField;
        public List<STExtention> lstExtention;
        public ConfDataBase.FieldOption ExternalKeyTableOption;

        public Table(string name)
        {
            Name = name;
            ExternalKeyTableOption= ConfDataBase.FieldOption.Mandatory;
            ReadExtention = "";
            LstField = new ArrayList();
            lstExtention = new List<STExtention>();
        }

        public void AddField(Field f)
        {
            LstField.Add(f);
        }

        public int FindField(ArrayList lstF, string name)
        {
            Field f;

            for (int i = 0; i < lstF.Count; i++)
            {
                f = (Field)lstF[i];
                if (f.Name.ToLower() == name.ToLower()) return i;
            }
            return -1;
        }

        public int FindField(string name, ConfDataBase.FieldOption fOption)
        {
            Field f;
            int j = -1;

            for (int i = 0; i < LstField.Count; i++)
            {
                f = (Field)LstField[i];
                if ((fOption & ((Field)LstField[i]).fieldOption) != 0)
                {
                    j++;
                    if (f.Name == name) return j;
                }
            }
            return -1;
        }

        public string FindLib(string name)
        {
            Field f;

            for (int i = 0; i < LstField.Count; i++)
            {
                f = (Field)LstField[i];
                if (f.Name == name) return f.Libelle;
            }
            return null;
        }

        public ArrayList FindNames(string name)
        {
            Field f;
            ArrayList aFindNames = new ArrayList();

            for (int i = 0; i < LstField.Count; i++)
            {
                f = (Field)LstField[i];
                if (f.Name.Contains(name)) aFindNames.Add(f.Name);
            }
            return aFindNames;
        }

        public int FindFieldFromLib(string lib)
        {
            Field f;

            for (int i = 0; i < LstField.Count; i++)
            {
                f = (Field)LstField[i];
                if (f.Libelle == lib) return i;
            }
            return -1;
        }

        public string GetLibFromName(string name)
        {
            Field f;
            for (int i = 0; i < LstField.Count; i++)
            {
                f = (Field)LstField[i];
                if (f.Name == name) return f.Libelle;
            }
            return "";
        }

        public int GetNbrSelectField()
        {
            int n = 0;

            for (int i = 0; i < LstField.Count; i++)
                if ((((Field)LstField[i]).fieldOption & ConfDataBase.FieldOption.Select) != 0) n++;

            return n;
        }

        public int GetNbrTabField()
        {
            int n = 0;

            for (int i = 0; i < LstField.Count; i++)
                if ((((Field)LstField[i]).fieldOption & ConfDataBase.FieldOption.TabNonVisible) == 0) n++;

            return n;
        }

        public string GetDefaultForeignKey()
        {
            string sDefaultForeignKey = "";
            switch(Name)
            {
                case "Produit":
                    sDefaultForeignKey = "GuidCadreRef";
                    break;
                default :
                    sDefaultForeignKey = GetSelectFieldFromOption(ConfDataBase.FieldOption.ForeignKey);
                    break;
            }
            return sDefaultForeignKey;
        }

        public string GetSelectField(ConfDataBase.FieldOption fOption)
        {
            Field f;
            string s = "";
            int n = GetNbrSelectField(), m = 0;
            ConfDataBase.FieldOption fOp;


            for (int i = 0; i < LstField.Count; i++)
            {
                f = (Field)LstField[i];
                fOp = fOption | ((Field)LstField[i]).fieldOption;
                if ((fOp & ConfDataBase.FieldOption.InterneBD) != 0)
                {
                    if ((fOp & ConfDataBase.FieldOption.Select) != 0)
                    //if ((((Field)LstField[i]).fieldOption & ConfDataBase.FieldOption.Select) != 0)
                    {
                        if (m != 0) s += ", ";
                        m++;
                        if ((fOp & ConfDataBase.FieldOption.NomCourt) != 0)
                            //if ((((Field)LstField[i]).fieldOption & ConfDataBase.FieldOption.NomCourt) != 0)
                            s += f.Name;
                        else s += Name + "." + f.Name;
                        
                    }
                }
                //if (m != n - 1) s += ", ";
                //m++;
            }
            return s;
        }

        public string GetSearchKey(Table tParent)
        {
            string s;
            s = GetSelectFieldFromOption(ConfDataBase.FieldOption.ForceSearchKey);
            if (s.Length > 0) return s;
            s = tParent.GetSelectFieldFromOption(ConfDataBase.FieldOption.SearchKey);
            if (s.Length > 0) return s;
            return "Guid" + Name;
        }

        public string GetSelectFieldFromOption(ConfDataBase.FieldOption fOption)
        {
            Field f;
            string s = "";

            for (int i = 0; i < LstField.Count; i++)
            {
                f = (Field)LstField[i];
                if ((fOption & ((Field)LstField[i]).fieldOption) != 0) s += ", " + f.Name;
            }
            if (s.Length > 0) return s.Substring(1);
            return "";
        }

        public string GetFields(ConfDataBase.FieldOption fOption)
        {
            Field f;
            string s = "";
                        
            for (int i = 0; i < LstField.Count; i++)
            {
                f = (Field)LstField[i];
                if ((((Field)LstField[i]).fieldOption & fOption) != 0)
                {
                    s = f.Name;
                    break;
                }
            }
            return s;
        }

        public string GetKey(bool bNomCourt = true)
        {
            string key = "";

            for (int i = 0; i < LstField.Count; i++)
            {
                if ((((Field)LstField[i]).fieldOption & ConfDataBase.FieldOption.Key) != 0)
                {
                    if(bNomCourt) key += "," + ((Field)LstField[i]).Name;
                    else key += "," + Name + "." + ((Field)LstField[i]).Name;
                }
            }
            if (key != "") return key.Substring(1);
            return null;
        }

        

        public ArrayList Merge(ArrayList LstA, ArrayList LstB)
        {
            Field f;
            ArrayList LstValue = new ArrayList();

            for (int i = 0; i < LstField.Count; i++)
            {
                f = (Field)LstField[i];
                switch (f.Type)
                {
                    case 's':
                    case 'o':
                        if ((((Field)LstField[i]).fieldOption & ConfDataBase.FieldOption.Select) != 0)
                        {
                            if((string)LstA[i]=="") LstValue.Add(LstB[i]); else LstValue.Add(LstA[i]);
                        }
                        else LstValue.Add("");
                        break;
                    case 'p': //picture & 'o' path de l'image
                    case 'q': //picture
                    case 'i':
                        if ((((Field)LstField[i]).fieldOption & ConfDataBase.FieldOption.Select) != 0)
                        {
                            if ((int)LstA[i] == 0) LstValue.Add(LstB[i]); else LstValue.Add(LstA[i]);
                        }
                        else LstValue.Add((int)0);
                        break;
                    case 'd':
                        if ((((Field)LstField[i]).fieldOption & ConfDataBase.FieldOption.Select) != 0)
                        {
                            if ((double)LstA[i] == 0) LstValue.Add(LstB[i]); else LstValue.Add(LstA[i]);
                        }
                        else LstValue.Add((double)0);
                        break;
                }
            }

            return LstValue;
        }

        public ArrayList InitValueFieldFromXmlNode(XmlNode Node)
        {
            Field f;
            ArrayList LstValue = new ArrayList();

            for (int i = 0; i < LstField.Count; i++)
            {
                f = (Field)LstField[i];
                switch (f.Type)
                {
                    case 's':
                    case 'o':
                        if ((((Field)LstField[i]).fieldOption & ConfDataBase.FieldOption.Select) != 0)
                        {
                            LstValue.Add(((XmlElement)Node).GetAttribute(f.Type + f.Name));

                        }
                        else LstValue.Add("");
                        break;
                    case 'p': //picture & 'o' path de l'image
                    case 'q': //picture
                    case 'i':
                        if ((((Field)LstField[i]).fieldOption & ConfDataBase.FieldOption.Select) != 0)
                        {
                            string field = ((XmlElement)Node).GetAttribute(f.Type + f.Name);
                            if (field != "") LstValue.Add(Convert.ToInt32(field)); else LstValue.Add((int)0);
                        }
                        else LstValue.Add((int)0);
                        break;
                    case 'd':
                        if ((((Field)LstField[i]).fieldOption & ConfDataBase.FieldOption.Select) != 0)
                        {
                            string field = ((XmlElement)Node).GetAttribute(f.Type + f.Name);
                            if (field != "") LstValue.Add(Convert.ToDouble(field)); else LstValue.Add((double)0);
                        }
                        else LstValue.Add((double)0);
                        break;
                    case 't':
                        if ((((Field)LstField[i]).fieldOption & ConfDataBase.FieldOption.Select) != 0)
                        {
                            string field = ((XmlElement)Node).GetAttribute(f.Type + f.Name);
                            if (field != "") LstValue.Add(DateTime.FromOADate(Convert.ToDouble(field))); else LstValue.Add((DateTime)DateTime.MinValue);
                        }
                        else LstValue.Add((DateTime)DateTime.MinValue);
                        break;
                }
            }

            return LstValue;
        }

        public void InitValue(ArrayList LstValue, Dictionary<string, object> dic)
        {
            foreach (KeyValuePair<string, object> d in dic)
            {
                int n = FindField(LstField, d.Key);
                if (n > -1)
                {
                    switch (((Field)LstField[n]).Type)
                    {
                        case 's':
                        case 'o':
                            if(d.Value != null) LstValue[n] = (string)d.Value;
                            break;
                        case 'p':
                        case 'q':
                        case 'i':
                            LstValue[n] = Convert.ToInt32((Int64)d.Value);
                            break;
                        case 'd':
                            LstValue[n] = Convert.ToDouble((Int64)d.Value);
                            break;
                        case 't':
                            LstValue[n] = DateTime.FromOADate(Convert.ToDouble((Int64)d.Value));
                            break;
                    }

                }
                    
            }
        }

        public ArrayList InitValue()
        {
            ArrayList LstValue = new ArrayList();
            for (int i = 0; i < LstField.Count; i++)
            {
                switch (((Field)LstField[i]).Type)
                {
                    case 's':
                    case 'o':
                        //if (i == x) LstValue.Add(GuidkeyObjet.ToString());
                        //else if (i == y) LstValue.Add(Texte);
                        LstValue.Add("");
                        break;
                    case 'p':
                    case 'q':
                    case 'i':
                        LstValue.Add((int)0);
                        break;
                    case 'd':
                        LstValue.Add((double)0);
                        break;
                    case 't':
                        LstValue.Add((DateTime)DateTime.MinValue);
                        break;
                    case 'a':
                        ArrayList aArray = new ArrayList(); aArray.Add(0);
                        LstValue.Add(aArray);
                        break;
                }
            }
            return LstValue;
        }

        public ArrayList InitValueExtention(string sGuid)
        {
            
            ArrayList LstValue = new ArrayList();
            
            STExtention stEx = lstExtention.Find(el => el.sGuidExtention == sGuid);
            if (stEx.lstFieldExtention != null)
            {
                for (int i = 0; i < stEx.lstFieldExtention.Count; i++)
                {
                    switch (((Field)stEx.lstFieldExtention[i]).Type)
                    {
                        case 's':
                            LstValue.Add("");
                            break;
                    }
                }
            }
            
            return LstValue;
        }

        public int getIField(string name)
        {
            for (int i = 0; i < LstField.Count; i++)
                if (((Field)LstField[i]).Name == name) return i;
            return -1;
        }

        public object initProp(object oLstValue, int i, object oAdd, bool Init)
        {
            object oOut=oLstValue;
            if (oAdd != null)
            {
                switch (((Field)LstField[i]).Type)
                {
                    case 's':
                        string sVal = (string)oLstValue;
                        if (Init || sVal == "") oOut = oAdd;
                        else oOut = sVal + " - " + (string)oAdd;
                        break;
                    case 'p':
                    case 'q':
                    case 'i':
                        int iVal=(int)oLstValue;
                        int iAdd;
                        if (oAdd.GetType() == typeof(string)) iAdd = Convert.ToInt32((string)oAdd);
                        else iAdd = (int)oAdd;
                        if (Init) oOut = iAdd;
                        else oOut = iVal + iAdd;
                        break;
                    case 'd':
                        double dVal = (double)oLstValue;
                        double dAdd;
                        if (oAdd.GetType() == typeof(string)) dAdd = Convert.ToDouble((string)oAdd);
                        else dAdd = (double)oAdd;
                        if (Init) oOut = dAdd;
                        else oOut = dVal + dAdd;
                        break;
                    case 't':
                        if ((string)oAdd!= "" ) oOut = DateTime.Parse((string)oAdd);
                        break;
                    case 'a':
                        oOut = oAdd;
                        break;
                }
            }
            return oOut;
        }

        public ArrayList CompleteValueFieldFromBD(OdbcDataReader r, ConfDataBase.FieldOption ConfField, ArrayList LstValue)
        {
            Field f;

            for (int i = 0; i < r.FieldCount; i++)
            {
                int iField = FindField(LstField, r.GetName(i));
                if (iField != -1)
                {
                    f = (Field)LstField[iField];
                    switch (f.Type)
                    {
                        case 's':
                        case 'o':
                            if (!r.IsDBNull(i))
                            {
                                if ((((Field)LstField[iField]).NameCpy != ""))
                                {
                                    int n = FindField(LstField, ((Field)LstField[iField]).NameCpy);
                                    if (n > -1)
                                    {
                                        LstValue[iField] = r.GetString(i) + "   (" + LstValue[n] + ")";
                                    }
                                }
                                else LstValue[iField] = r.GetString(i);
                            }
                            break;
                        case 'p': //picture & 'o' path de l'image
                        case 'q': //picture
                        case 'i':
                            if (!r.IsDBNull(i)) LstValue[iField] = r.GetInt32(i);
                            break;
                        case 'd':
                            if (!r.IsDBNull(i)) LstValue[iField] = (double)r.GetValue(i);
                            break;
                        case 't':

                            if (!r.IsDBNull(i))
                            {
                                if (r.GetFieldType(i) == typeof(double)) LstValue[iField] = DateTime.FromOADate(r.GetDouble(i));
                                else LstValue[iField] = ((DateTime)r.GetDate(i)).ToShortDateString();
                            }
                            break;
                    }
                }
                //else System.Windows.MessageBox.Show("Field inconnu dans ConfDataBase : " + r.GetName(i));
                //else  ("Field inconnu : " + r.GetName(i));
            }
            return LstValue;
        }

        public ArrayList InitValueFieldFromBD(OdbcDataReader r, ConfDataBase.FieldOption ConfField)
        {
            Field f;
            ArrayList LstValue = InitValue();
            
            for (int i = 0; i < r.FieldCount; i++)
            {
                int iField = FindField(LstField, r.GetName(i));
                if (iField != -1)
                {
                    f = (Field)LstField[iField];
                    switch (f.Type)
                    {
                        case 's':
                        case 'o':
                            if (!r.IsDBNull(i))
                            {
                                if ((((Field)LstField[iField]).NameCpy != ""))
                                {
                                    int n = FindField(LstField, ((Field)LstField[iField]).NameCpy);
                                    if (n > -1)
                                    {
                                        LstValue[iField] = r.GetString(i) + "   (" + LstValue[n] + ")";
                                    }
                                }
                                else LstValue[iField] = r.GetString(i);
                            }
                            break;
                        case 'p': //picture & 'o' path de l'image
                        case 'q': //picture
                        case 'i':
                            if (!r.IsDBNull(i)) LstValue[iField] = r.GetInt32(i);
                            break;
                        case 'd':
                            if (!r.IsDBNull(i)) LstValue[iField] = (double)r.GetValue(i);
                            break;
                        case 't':

                            if (!r.IsDBNull(i))
                            {
                                if (r.GetFieldType(i) == typeof(double)) LstValue[iField] = DateTime.FromOADate(r.GetDouble(i));
                                else LstValue[iField] = ((DateTime)r.GetDate(i)).ToShortDateString();
                            }
                            break;
                    }
                }
                //else System.Windows.MessageBox.Show("Field inconnu dans ConfDataBase : " + r.GetName(i));
                //else  ("Field inconnu : " + r.GetName(i));
            }
            return LstValue;
        }

        /*public ArrayList InitValueFieldFromBD(int Depart, OdbcDataReader r)
        {
            Field f;
            int m = Depart;
            ArrayList LstValue = new ArrayList();
            ConfDataBase.FieldOption fieldOp = ConfDataBase.FieldOption.InterneBD | ConfDataBase.FieldOption.Select;

            for (int i = 0; i < LstField.Count; i++)
            {
                f = (Field)LstField[i];


                switch (f.Type)
                {
                    case 's':
                    case 'o':
                        if (m >= r.FieldCount) { LstValue.Add(""); break; }
                        //if ((((Field)LstField[i]).fieldOption & ConfDataBase.FieldOption.Select) != 0)
                        if ((((Field)LstField[i]).fieldOption & fieldOp) != 0)
                        {
                            if (r.IsDBNull(m)) LstValue.Add("");
                            else
                            {
                                if ((((Field)LstField[i]).NameCpy != ""))
                                {
                                    int n = FindField(((Field)LstField[i]).NameCpy);
                                    if (n > -1)
                                    {
                                        LstValue.Add(r.GetString(m) + "   (" + LstValue[n] + ")");
                                    }
                                }
                                else LstValue.Add(r.GetString(m));
                            }
                            m++;
                        }
                        else LstValue.Add("");

                        break;
                    case 'p': //picture & 'o' path de l'image
                    case 'q': //picture
                    case 'i':
                        if (m >= r.FieldCount) { LstValue.Add((int)0); break; }
                        //if ((((Field)LstField[i]).fieldOption & ConfDataBase.FieldOption.Select) != 0)
                        if ((((Field)LstField[i]).fieldOption & fieldOp) != 0)
                        {
                            LstValue.Add(r.GetInt32(m));
                            m++;
                        }
                        else LstValue.Add((int)0);
                        break;
                    case 'd':
                        if (m >= r.FieldCount) { LstValue.Add((double)0); break; }
                        //if ((((Field)LstField[i]).fieldOption & ConfDataBase.FieldOption.Select) != 0)
                        if ((((Field)LstField[i]).fieldOption & fieldOp) != 0)
                        {
                            //string s = r.GetFieldType(m).Name;
                            LstValue.Add((double)r.GetValue(m));
                            m++;
                        }
                        else LstValue.Add((double)0);
                        break;
                    case 't':
                        if (m >= r.FieldCount) { LstValue.Add((DateTime)DateTime.MinValue); break; }
                        //if ((((Field)LstField[i]).fieldOption & ConfDataBase.FieldOption.Select) != 0)
                        if ((((Field)LstField[i]).fieldOption & fieldOp) != 0)
                        {
                            if (r.GetFieldType(m) == typeof(double)) LstValue.Add(DateTime.FromOADate(r.GetDouble(m)));
                            else LstValue.Add((DateTime)r.GetDate(m));
                            m++;
                        }
                        else LstValue.Add((DateTime)DateTime.MinValue);
                        break;
                }
            }
            
            return LstValue;
        }*/
    }
   
    public class Field
    {
        public string Name;
        public string Libelle;
        public char Type;
        public int GrpAffiche;
        public ConfDataBase.FieldOption fieldOption;
        public string NameCpy;
        public int Width;
        public int TableEx;


        /// <summary>
        /// Constructeur champs
        /// </summary>
        /// <param name="nom du champs"></param>
        /// <param name="libelle du champs"></param>
        /// <param name="type de la valeur: (s)tring, (i)nterger, (d)ouble"></param>
        /// <param name="ReadOnly sur la valeur du champ"></param>
        /// <param name="numéro du Groupe d'affichage (0):pas d'affichage"></param>
        /// <param name="Retour à la ligne ou '-' dans le group d'affichage"></param>
        /// <param name="Champ dans la base de données (true):oui"></param>
        /// <returns></returns>
        
        public Field(string name, string lib, char type, int grpaff, int width, ConfDataBase.FieldOption fo)
        {
            Name = name;
            Libelle = lib;
            Type = type;
            GrpAffiche = grpaff;
            fieldOption = fo;
            Width = width;
            TableEx = -1;
            NameCpy = "";
        }

        public Field(string name, string lib, char type, int grpaff, int width, ConfDataBase.FieldOption fo, string ncpy)
        {
            Name = name;
            Libelle = lib;
            Type = type;
            GrpAffiche = grpaff;
            fieldOption = fo;
            NameCpy = ncpy;
            Width = width;
            TableEx = -1;
        }

        public Field(string name, string lib, char type, int grpaff, int width, ConfDataBase.FieldOption fo, int tEx)
        {
            Name = name;
            Libelle = lib;
            Type = type;
            GrpAffiche = grpaff;
            fieldOption = fo;
            Width = width;
            TableEx = tEx;
            NameCpy = "";
            
        }
    }

    public abstract class TxtFile
    {
        public  StreamWriter sw;
        protected Form1 F;

        public virtual void SWwrite(string s)
        {
            sw.WriteLine(s);
        }

        public virtual void SWclose()
        {
            sw.Close();
        }
    }

    public class StdFile : TxtFile
    {
        public StdFile(Form1 f, string sPath)
        {
            sw = System.IO.File.CreateText(sPath);
            F = f;
        }
    }

    public class LogFile : TxtFile
    {
        public LogFile(Form1 f, string sPath)
        {
            sw = System.IO.File.CreateText(sPath);
            F = f;
        }

        public void SWwriteLog(int indent, string s, bool l=false)
        {
            string sIndent = " ";

            for (int i = 0; i < indent; i++) sIndent += " ";
            sw.WriteLine(DateTime.Now.ToLongTimeString() + sIndent + s);
            if (l) sw.WriteLine(sIndent + "------------------------------");
        }

        
    }

    public abstract class XmlFile
    {
        public XmlDocument docXml;
        public XmlElement root;
        public XmlElement Cursor;
        protected Form1 F;

        /// <summary>
        /// Constructeur champs
        /// </summary>
        /// <param name="nom du champs"></param>
        /// <param name="libelle du champs"></param>
        /// <param name="type de la valeur: (s)tring, (i)nterger, (d)ouble"></param>
        /// <param name="ReadOnly sur la valeur du champ"></param>
        /// <param name="numéro du Groupe d'affichage (0):pas d'affichage"></param>
        /// <param name="Retour à la ligne ou '-' dans le group d'affichage"></param>
        /// <param name="Champ dans la base de données (true):oui"></param>
        /// <returns></returns>


        public virtual XmlElement GetCursor()
        {
            return Cursor;
        }
        protected virtual bool CursorFree()
        {
            if (Cursor == null) return true;
            return false;
        }

        public virtual void CursorClose()
        {
            Cursor = null;
        }

        public virtual bool SetCursor(string sKey)
        {
            if (CursorFree())
            {
                if (sKey == "root")
                {
                    Cursor = docXml.DocumentElement;
                    return true;
                }
                else
                {
                    Cursor = XmlGetElFromInnerText(root, sKey);
                    if (Cursor != null) return true;
                }
            }
            return false;
        }

        public virtual bool SetCursor(XmlElement el)
        {
            if (CursorFree() && el != null)
            {
                Cursor = el;
                return true;
            }
            return false;
        }

        public virtual XmlElement XmlCreatSimpleEl(XmlElement elParent, string sNom)
        {
            XmlElement el = docXml.CreateElement(sNom);
            elParent.AppendChild(el);
            return el;
        }

        public virtual XmlElement XmlCreatEl(XmlElement elParent, string sNom)
        {
            XmlElement el = docXml.CreateElement(sNom);
            elParent.AppendChild(el);
            return el;
        }

        public virtual XmlElement XmlCreatEl(XmlElement elParent, string sNom, string sKey)
        {
            XmlElement el = XmlCreatEl(elParent, sNom);
            el.SetAttribute("SearchKey", sKey);
            return el;
        }

        public virtual void XmlAddStyle(XmlElement el, string sText)
        {
            el.SetAttribute("style", sText);
        }

        public virtual void XmlAddText(XmlElement el, string sText)
        {
            XmlNode node = docXml.CreateNode(XmlNodeType.Text, "", "");
            node.InnerText = sText;
            el.AppendChild(node);
        }

        public virtual XmlElement XmlCreatEl(XmlElement elParent, string sNom, string sKey, string Inner)
        {
            XmlElement el = XmlCreatEl(elParent, sNom, sKey);
            XmlAddText(el, Inner);
            return el;
        }
        //-<ApplicationClass SearchKey="GuidApplicationClass">

        public virtual void XmlSave(string path)
        {
            docXml.Save(path);
        }

        public virtual void XmlAllSetParentAttributValueFromEl(XmlElement el, string sAtt, string sValue)
        {
            if (el.GetAttribute(sAtt, sValue) != null) el.SetAttribute(sAtt, sValue);
            if (el.ParentNode != null && el.ParentNode.NodeType == XmlNodeType.Element) XmlAllSetParentAttributValueFromEl((XmlElement)el.ParentNode, sAtt, sValue);
        }

        public virtual string XmlGetName(XmlElement el)
        {
            return el.Name;
        }

        public virtual XmlElement XmlGetFirstElFromName(XmlElement parent, string sName)
        {
            IEnumerator ienum = parent.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    XmlElement elCur = (XmlElement)Node;
                    if (elCur.Name == sName) return elCur;
                    elCur = XmlGetFirstElFromName((XmlElement)Node, sName);
                    if (elCur != null) return elCur;
                }
            }
            return null;
        }

        public virtual void xmlSaveRowToDBTable(ArrayList lstRows, string table)
        {
            for(int i=0; i<lstRows.Count; i++)
            {
                XmlElement row = (XmlElement)lstRows[i];
                xmlSaveRowToDBTable(row, table);
            }
        }

        public virtual void xmlSaveRowToDBTable(XmlElement row, string table)
        {
            Table t;
            int n = F.oCnxBase.ConfDB.FindTable(table);

            if (n > -1)
            {

                t = (Table)F.oCnxBase.ConfDB.LstTable[n];

                IEnumerator ienum = row.GetEnumerator();
                ArrayList lstEl = new ArrayList();
                XmlNode Node;
                string sRequeteSql = "UPDATE " + table + " Set ";
                string sSet = "", sVirgule = " ' ";
                string sCondition = " WHERE ";
                //UPDATE Vue SET GuidGVue = '" + GuidGVue + "' WHERE GuidVue = '" + GuidVue + "'");
                while (ienum.MoveNext())
                {
                    Node = (XmlNode)ienum.Current;
                    if (Node.NodeType == XmlNodeType.Element) lstEl.Add(Node);
                }
                if (lstEl.Count > 1)
                {
                    XmlNode node = (XmlNode)lstEl[0];
                    sCondition += node.Name + "='" + node.InnerText + "'";
                    for (int i = 1; i < lstEl.Count; i++)
                    {
                        node = (XmlNode)lstEl[i];
                        n = t.FindField(t.LstField, node.Name);
                        if (n > -1)
                        {
                            switch (((Field)t.LstField[n]).Type)
                            {
                                case 's':
                                    if (node.InnerText != "")
                                        sSet += sVirgule + node.Name + "='" + node.InnerText + "'";
                                    else
                                        sSet += sVirgule + node.Name + "= null";
                                    break;
                                case 'p': //picture
                                case 'q': //picture
                                case 'i':
                                    sSet += sVirgule + node.Name + "=" + Convert.ToInt32(node.InnerText);
                                    break;
                                case 'd':
                                    sSet += sVirgule + node.Name + "=" + Convert.ToDouble(node.InnerText);
                                    break;
                            }
                        }
                    }
                    F.oCnxBase.CBWrite(sRequeteSql + sSet.Substring(sVirgule.Length) + sCondition);
                }
            }
        }

        public virtual ArrayList XmlGetLstElFromName(XmlElement parent, string sName)
        {
            IEnumerator ienum = parent.GetEnumerator();
            ArrayList lstEl = new ArrayList();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    XmlElement elCur = (XmlElement)Node;
                    if (elCur.Name == sName) lstEl.Add(elCur);
                    ArrayList lstSSEl;
                    lstSSEl = XmlGetLstElFromName((XmlElement)Node, sName);
                    for (int i = 0; i < lstSSEl.Count; i++) lstEl.Add(lstSSEl[i]);
                }
            }
            return lstEl;
        }

        public virtual XmlElement XmlGetFirstElFromParent(XmlElement Parent, string sLibNode)
        {
            if (Parent != null)
            {
                IEnumerator ienum = Parent.GetEnumerator();
                XmlNode Node;

                while (ienum.MoveNext())
                {
                    Node = (XmlNode)ienum.Current;
                    if (Node.NodeType == XmlNodeType.Element)
                    {
                        if (Node.Name == sLibNode)
                            return (XmlElement)Node;
                    }
                }
            }
            return null;
        }

        public XmlElement XmlGetFirstElFromParent(XmlElement Parent, string sLibNode, string sPropertyNode, string sValue)
        {
            IEnumerator ienum = Parent.GetEnumerator();
            XmlNode Node;

            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    if (Node.Name == sLibNode && ((XmlElement)Node).GetAttribute(sPropertyNode) == sValue)
                        return (XmlElement)Node;
                    XmlElement el = XmlGetFirstElFromParent((XmlElement)Node, sLibNode, sPropertyNode, sValue);
                    if (el != null) return el;
                }
            }

            return null;
        }

        public virtual void XmlSetAttFromEl(XmlElement Parent, string sAtt, string sType, string sValue)
        {
            XmlElement el = docXml.CreateElement("Attribut");
            el.SetAttribute("Nom", sAtt);
            el.SetAttribute("Type", sType);
            el.SetAttribute("Value", sValue);
            Parent.AppendChild(el);
        }

        public virtual int XmlGetNbrElFromName(XmlElement parent, string sName, int imax = 99)
        {
            int iNbr = 0;

            IEnumerator ienum = parent.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element && Node.Name == sName)
                {
                    iNbr++;
                    if (imax > 0) iNbr += XmlGetNbrElFromName((XmlElement)Node, sName, imax - 1);
                }
            }
            return iNbr;
        }

        public virtual int XmlGetNbrElFromNameAndAtt(XmlElement parent, string sName, string sAttName, string sValue, int imax = 99)
        {
            int iNbr = 0;

            IEnumerator ienum = parent.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element && Node.Name == sName && ((XmlElement)Node).GetAttribute(sAttName) == sValue)
                    iNbr++;
                if (imax > 0) iNbr += XmlGetNbrElFromNameAndAtt((XmlElement)Node, sName, sAttName, sValue, imax - 1);
            }
            return iNbr;
        }

        public virtual XmlNode GetElTextFromParent(XmlElement parent)
        {
            IEnumerator ienum = parent.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Text) return Node;
            }
            return null;
        }

        public virtual XmlElement XmlGetElFromInnerText(XmlElement parent, string sInnerXmlSearch)
        {
            IEnumerator ienum = parent.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                XmlNode elTxt = GetElTextFromParent(parent);
                if ( elTxt !=null && elTxt.InnerText == sInnerXmlSearch) return parent;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    XmlElement elss = XmlGetElFromInnerText((XmlElement)Node, sInnerXmlSearch);
                    if (elss != null) return elss;
                }
            }
            return null;
        }

        public virtual XmlElement XmlFindElFromAtt(XmlElement parent, string sAtt, string sValue, int imax = 99)
        {
            IEnumerator ienum = parent.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    XmlElement elCur = (XmlElement)Node;
                    if (elCur.GetAttribute(sAtt) == sValue) return elCur;

                    if (imax > 0)
                    {
                        elCur = XmlFindElFromAtt((XmlElement)Node, sAtt, sValue, imax - 1);
                        if (elCur != null) return elCur;
                    }
                }
            }
            return null;
        }

        public virtual XmlElement XmlFindElFromTwoAtt(XmlElement parent, string sAtt1, string sValue1, string sAtt2, string sValue2, int imax = 99)
        {
            IEnumerator ienum = parent.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    XmlElement elCur = (XmlElement)Node;
                    if (elCur.GetAttribute(sAtt1) == sValue1 && elCur.GetAttribute(sAtt2) == sValue2) return elCur;

                    if (imax > 0)
                    {
                        elCur = XmlFindElFromTwoAtt((XmlElement)Node, sAtt1, sValue1, sAtt2, sValue2, imax - 1);
                        if (elCur != null) return elCur;
                    }
                }
            }
            return null;
        }

        public virtual XmlElement XmlFindElFromContaintAtt(XmlElement parent, string sAtt, string sValue, int imax = 99)
        {
            IEnumerator ienum = parent.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    XmlElement elCur = (XmlElement)Node;
                    if (elCur.GetAttribute(sAtt).IndexOf(sValue) != -1) return elCur;

                    if (imax > 0)
                    {
                        elCur = XmlFindElFromContaintAtt((XmlElement)Node, sAtt, sValue, imax - 1);
                        if (elCur != null) return elCur;
                    }
                }
            }
            return null;
        }

        public virtual void XmlSetAllAttNewValue(XmlElement parent, string sAtt, string sOldValue, string sNewValue)
        {
            XmlElement el = XmlFindElFromTwoAtt(parent, "Nom", sAtt, "Value", sOldValue);
            IEnumerator ienum = parent.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    XmlElement elCur = (XmlElement)Node;
                    if (elCur.GetAttribute("Nom") == sAtt && elCur.GetAttribute("Value") == sOldValue)
                        elCur.SetAttribute("Value", sNewValue);
                    XmlSetAllAttNewValue((XmlElement)Node, sAtt, sOldValue, sNewValue);
                }
            }
        }

        public virtual void XmlSetTexteFromEl(XmlElement el, string sNewValue)
        {
            IEnumerator ienum = el.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                
                if (Node.NodeType == XmlNodeType.Text)
                {
                    XmlText elText = (XmlText)Node;
                    elText.Value = sNewValue;
                }
                
            }
        }

        public virtual bool XmlSetAttNewValue(XmlElement parent, string sAtt, string sOldValue, string sNewValue, int imax = 99)
        {
            XmlElement el = XmlFindElFromTwoAtt(parent, "Nom", sAtt, "Value", sOldValue);
            if (el != null) {
                el.SetAttribute("Value", sNewValue);
                return true;
            }
            return false;
        }

        public virtual string XmlGetAttValueFromElName(XmlElement parent, string sAtt, string sName)
        {
            XmlElement el = XmlGetFirstElFromName(parent, sName);
            if (el != null)
            {
                return el.GetAttribute(sAtt);
            }
            return "";
        }

        public virtual string XmlGetAttValueAFromAttValueB(XmlElement parent, string sAttA, string sAttB, string sValue, int imax = 99)
        {
            XmlElement el = XmlFindElFromAtt(parent, sAttB, sValue, imax);
            if (el != null)
            {
                return el.GetAttribute(sAttA);
            }
            return "";
        }



        public virtual void CreatXmlFromReader(string sType, ConfDataBase.FieldOption fOption)
        {
            XmlElement elrow = XmlCreatEl(root, "row");

            Field f;
            Table t;

            int n = F.oCnxBase.ConfDB.FindTable(sType);
            if (n > -1)
            {
                t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                int j = 0;
                for (int i = 0; i < t.LstField.Count; i++)
                {
                    f = (Field)t.LstField[i];
                    if ((fOption & f.fieldOption) != 0)
                    {

                        F.oCnxBase.Reader.GetName(j);
                        //n'ajoute pas le champs s'il est egal à null
                        //if (!F.oCnxBase.Reader.IsDBNull(j))
                        {
                            XmlElement el = XmlCreatSimpleEl(elrow, f.Name);

                            switch (f.Type)
                            {
                                case 's':
                                case 'o':
                                    if (F.oCnxBase.Reader.IsDBNull(j))  el.InnerText = ""; else
                                    el.InnerText = F.oCnxBase.Reader.GetString(j);
                                    break;
                                case 'i':
                                case 'p':
                                    if (F.oCnxBase.Reader.IsDBNull(j)) el.InnerText = ""; else
                                    el.InnerText = F.oCnxBase.Reader.GetInt32(j).ToString();
                                    break;
                                case 'd':
                                    if (F.oCnxBase.Reader.IsDBNull(j)) el.InnerText = ""; else 
                                    el.InnerText = F.oCnxBase.Reader.GetDouble(j).ToString();
                                    break;
                                case 't':
                                    if (F.oCnxBase.Reader.IsDBNull(j)) el.InnerText = ""; else 
                                    el.InnerText = F.oCnxBase.Reader.GetDate(j).ToShortDateString();
                                    break;
                            }
                        }
                        j++;
                    }
                }
                elrow.SetAttribute("NbrField", j.ToString());
            }
        }

        public virtual void CreatXmlFromReader()
        {
            XmlElement elrow = XmlCreatEl(root, "row");

            for(int i=0; i<F.oCnxBase.Reader.FieldCount; i++)
            {
                if(!F.oCnxBase.Reader.IsDBNull(i))
                {
                    XmlElement el = XmlCreatSimpleEl(elrow, F.oCnxBase.Reader.GetName(i));
                    switch(F.oCnxBase.Reader.GetFieldType(i).Name) {
                        case "String":
                            el.InnerText = F.oCnxBase.Reader.GetString(i);
                            break;
                        case "Int32":
                            el.InnerText = F.oCnxBase.Reader.GetInt32(i).ToString();
                            break;
                        case "Double":
                            el.InnerText = F.oCnxBase.Reader.GetDouble(i).ToString();
                            break;
                        case "DateTime":
                            el.InnerText = F.oCnxBase.Reader.GetDate(i).ToShortDateString();
                            break;
                    }
                }
            }
        }
    }

public class HtmlFile : DrawTools.XmlFile
    {

        public HtmlFile(Form1 f)
        {
            docXml = new XmlDocument();
            docXml.LoadXml("<html><head><meta http-equiv=\"Content - Language\" content=\"fr - fr\"/><meta charset = \"iso-8859-15\" /></head><body></body></html>");
            root = docXml.DocumentElement;
            F = f;
        }

        public override void XmlSetAttFromEl(XmlElement Parent, string sAtt, string sType, string sValue)
        {
            XmlElement el = docXml.CreateElement("Attribut");
            Parent.SetAttribute(sAtt, sValue);
        }
        public XmlElement XmlCreatRowEl(XmlElement elParent)
        {
            XmlElement elRow = XmlCreatEl(elParent, "tr");

            return elRow;
        }

        public XmlElement XmlCreatColHeadEl(XmlElement elParent, string sNom)
        {
            XmlElement elColHead = XmlCreatEl(elParent, "th");
            XmlAddText(elParent, sNom);

            return elColHead;
        }

        public XmlElement XmlCreatColEl(XmlElement elParent, Niveau n)
        {
            XmlElement elCol = XmlCreatEl(elParent, "td");
            XmlAddStyle(elCol, n.GetStyleHtml());
            XmlAddText(elCol, n.GetTexttHtml());

            return elCol;
        }

        public XmlElement XmlCreatColEl(XmlElement elParent, string sNom)
        {
            XmlElement elCol = XmlCreatEl(elParent, "td");
            XmlAddText(elCol, sNom);

            return elCol;
        }

        public XmlElement XmlCreatRowEl(XmlElement elParent, Effectif e)
        {
            XmlElement elRow = XmlCreatEl(elParent, "tr");
            XmlCreatColEl(elRow, e.NomEffectif);

            for (int i = 0; i < e.lstNivEffectif.Count; i++)
            {
                XmlCreatColEl(elRow, (Niveau)e.lstNivEffectif[i]);
            }

            return elRow;
        }

        public XmlElement XmlCreatRowEl(XmlElement elParent, OdbcDataReader r)
        {
            XmlElement elRow = XmlCreatEl(elParent, "tr");
            for (int i = 0; i < r.FieldCount; i++)
            {
                if (r.GetFieldType(i) == typeof(string)) XmlCreatColEl(elRow, r.GetString(i));
                else if (r.GetFieldType(i) == typeof(int)) XmlCreatColEl(elRow, r.GetInt32(i).ToString());
                else if (r.GetFieldType(i) == typeof(Double)) XmlCreatColEl(elRow, r.GetDouble(i).ToString());
                else if (r.GetFieldType(i) == typeof(DateTime)) XmlCreatColEl(elRow, r.GetDateTime(i).ToShortDateString());

            }

            return elRow;
        }

        public XmlElement XmlCreatTableEl(XmlElement elParent, string sId, string[] sEnTete)
        {
            XmlElement elTab = XmlCreatEl(elParent, "table");
            XmlSetAttFromEl(elTab, "id", "s", sId);
            XmlElement elss = XmlCreatEl(elTab, "thead");
            elss = XmlCreatEl(elTab, "tfoot");
            elss = XmlCreatEl(elTab, "tbody");
            XmlElement elRow = XmlCreatRowEl(elss);
            for(int i=0; i<sEnTete.Length; i++)
            {
                XmlCreatColHeadEl(elRow, sEnTete[i]);
            }

            return elTab;
        }

        public XmlElement XmlCreatDivEl(XmlElement elParent, string sId)
        {
            XmlElement elDiv = XmlCreatEl(elParent, "div");
            XmlSetAttFromEl(elDiv, "id", "s", sId);

            return elDiv;
        }
    }

    public class XmlExcel : DrawTools.XmlFile
    {
        public XmlExcel(Form1 f, string Entete, bool bFile = false)
        {
            docXml = new XmlDocument();
            if (bFile)
                docXml.Load(Entete);
            else
                docXml.LoadXml("<" + Entete + "></" + Entete + ">");
            root = docXml.DocumentElement;
            F = f;
        }
    }

    public class XmlDB : DrawTools.XmlFile
    {
        public XmlDB(Form1 f, string Entete)
        {
            docXml = new XmlDocument();
            docXml.LoadXml("<" + Entete + "></" + Entete + ">");
            root = docXml.DocumentElement;
            F = f;
        }

        public override XmlElement XmlCreatEl(XmlElement elParent, string sNom)
        {

            XmlElement el = docXml.CreateElement(sNom);
            XmlElement elBefore = docXml.CreateElement("Before"); el.AppendChild(elBefore);
            XmlElement elAtts = docXml.CreateElement("Attributs"); el.AppendChild(elAtts);
            XmlElement elAfter = docXml.CreateElement("After"); el.AppendChild(elAfter);
            elParent.AppendChild(el);
            return el;
        }

        public void XmlCreatIndicator(XmlElement el)
        {
            string sNomAttKey = el.GetAttribute("SearchKey");
            string sKey = XmlGetAttValueAFromAttValueB(el, "Value", "Nom", sNomAttKey);

            if (F.oCnxBase.CBRecherche("SELECT  GuidIndicator, ValIndicator FROM IndicatorLink WHERE GuidObjet='" + sKey + "'"))
            {
                XmlElement elParent = XmlGetFirstElFromParent(el, "After");
                while (F.oCnxBase.Reader.Read())
                {
                    XmlElement eli = XmlCreatEl(elParent, "IndicatorLink", "GuidObjet,GuidIndicator");
                    XmlElement elAtts = XmlGetFirstElFromParent(eli, "Attributs");
                    XmlSetAttFromEl(elAtts, "GuidObjet", "s", sKey);
                    XmlSetAttFromEl(elAtts, "GuidIndicator", "s", F.oCnxBase.Reader.GetString(0));
                    XmlSetAttFromEl(elAtts, "ValIndicator", "s", F.oCnxBase.Reader.GetDouble(1).ToString());
                }
            }
            F.oCnxBase.CBReaderClose();
        }

        public string GetAttr(object o, string sAttr)
        {
            XmlElement el = (XmlElement)o;
            return XmlGetAttValueAFromAttValueB(el, "Value", "Nom", sAttr);
        }

        public string GetObjetComment(string sGuidObj, string sNomPropVal, string sFormat)
        {
            ArrayList lstComment = GetObjets(sGuidObj, "Comment", 1);
            for(int i=0;i<lstComment.Count;i++)
            {
                if (SetCursor((XmlElement)lstComment[i]))
                {
                    if(XmlGetAttValueAFromAttValueB(GetCursor(), "Value", "Nom", "NomProp") == sNomPropVal)
                    {
                        string sData = XmlGetAttValueAFromAttValueB(GetCursor(), "Value", "Nom", "RichText");
                        if(sData!=null && sData.Length>0)
                        {
                            CursorClose();
                            byte[] rawData = F.oCnxBase.StringToByteArray(sData);
                            System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
                            rtBox.Rtf = System.Text.Encoding.UTF8.GetString(rawData);
                            return rtBox.Text;
                        }
                    }
                }
                CursorClose();
            }
            return "";
        }

        public ArrayList GetObjets(string sGuidParent, string NomChild, int iProfondeur)
        {
            ArrayList lstObjs = new ArrayList();
            if (SetCursor(sGuidParent))
            {
                XmlElement elAfter = F.XmlFindFirstElFromName(GetCursor(), "After", 1);
                if (elAfter != null)
                {
                    lstObjs = F.XmlGetLstElFromName(elAfter, NomChild, iProfondeur);
                }
            }
            CursorClose();

            return lstObjs;
        }

        public void XmlCreatComment(XmlElement el, string sGuidkeyObjet)
        {
            if (F.oCnxBase.CBRecherche("SELECT NomProp, Id, HyperLien,  Size, RichText, Policy FROM Comment Where GuidObject='" + sGuidkeyObjet + "'"))
            {
                XmlElement elParent = XmlGetFirstElFromParent(el, "After");
                while (F.oCnxBase.Reader.Read())
                {
                    XmlElement elc = XmlCreatEl(elParent, "Comment", "GuidObject,NomProp");
                    XmlElement elAtts = XmlGetFirstElFromParent(elc, "Attributs");

                    XmlSetAttFromEl(elAtts, "GuidObject", "s", sGuidkeyObjet);
                    XmlSetAttFromEl(elAtts, "NomProp", "s", F.oCnxBase.Reader.GetString(0));
                    XmlSetAttFromEl(elAtts, "Id", "s", F.oCnxBase.Reader.GetString(1));
                    if (!F.oCnxBase.Reader.IsDBNull(2)) XmlSetAttFromEl(elAtts, "HyperLien", "s", F.oCnxBase.Reader.GetString(2));
                    int nByte = F.oCnxBase.Reader.GetInt32(3);
                    XmlSetAttFromEl(elAtts, "Size", "i", nByte.ToString());
                    if (nByte > 0)
                    {
                        byte[] rawData = new byte[nByte];
                        F.oCnxBase.Reader.GetBytes(4, 0, rawData, 0, nByte);
                        string hData = BitConverter.ToString(rawData).Replace("-", string.Empty);
                        XmlSetAttFromEl(elAtts, "RichText", "b", hData);
                    }
                    XmlSetAttFromEl(elAtts, "Policy", "s", F.oCnxBase.Reader.GetString(5));
                }
            }
            F.oCnxBase.CBReaderClose();
        }

        private void ChangeObjet(XmlElement elAfterAppVersion, XmlElement elCur, string sObj, List<string[]> lstChangeObjet)
        {
            string sGuid = XmlGetAttValueAFromAttValueB(elCur, "Value", "Nom", "Guid" + sObj);
            if (sGuid != "")
            {
                // vérifier si l'objet n'a pas été déjà changé dans la liste

                if (lstChangeObjet.Find(elFind => elFind[1] == (string)sGuid) == null)
                {
                    string sNewGuid = Guid.NewGuid().ToString();
                    XmlSetAllAttNewValue(elAfterAppVersion, "Guid" + sObj, sGuid, sNewGuid);
                    XmlSetAllAttNewValue(elCur, "GuidObject", sGuid, sNewGuid);
                    XmlSetAllAttNewValue(elCur, "GuidObjet", sGuid, sNewGuid);

                    //Pour les modules
                    XmlSetAllAttNewValue(elAfterAppVersion, "GuidModuleIn", sGuid, sNewGuid);
                    XmlSetAllAttNewValue(elAfterAppVersion, "GuidModuleOut", sGuid, sNewGuid);

                    // Pour les composants, les interfaces, les bases, les files
                    XmlSetAllAttNewValue(elAfterAppVersion, "GuidComposantL1In", sGuid, sNewGuid);
                    XmlSetAllAttNewValue(elAfterAppVersion, "GuidComposantL1Out", sGuid, sNewGuid);

                    // Pour les servers
                    XmlSetAllAttNewValue(elAfterAppVersion, "GuidServerIn", sGuid, sNewGuid);
                    XmlSetAllAttNewValue(elAfterAppVersion, "GuidServerOut", sGuid, sNewGuid);


                    //ChangeGObjet(xmlDB, elCur, sObj);

                    // enregistrer l'objet changé
                    string[] aObj = new string[2];
                    aObj[0] = sObj;
                    aObj[1] = sNewGuid;
                    lstChangeObjet.Add(aObj);
                    sGuid = sNewGuid;
                }
            }
            //Change le Text de objet
            XmlSetTexteFromEl(elCur, sGuid);
        }

        public void ImportXml(XmlElement elParent)
        {
            //Att bug sur l'appel de la fonction deletencardlink : delete tout les enreg lies a la carte : il faut tenir compte des autres vue

            IEnumerator ienum = elParent.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                switch (Node.NodeType)
                {
                    case XmlNodeType.Element:
                        F.oCnxBase.CreatEnregFromXml((XmlElement)Node);
                        ImportXml((XmlElement)Node);
                        break;
                }
            }
        }

        public void XmlCopyVue(XmlElement elVue, XmlElement elGlobal, List<string[]> lstChangeObjet, bool bCpyOneVue = true)
        {

            string sGuidVue = XmlGetAttValueAFromAttValueB(elVue, "Value", "Nom", "GuidVue");
            string sGuidGVue = XmlGetAttValueAFromAttValueB(elVue, "Value", "Nom", "GuidGVue");
            //string sGuidTypeVue = xmlDB.XmlGetAttValueAFromAttValueB(elVue, "Value", "Nom", "GuidTypeVue");
            string sNewGuidVue = Guid.NewGuid().ToString();
            string sNewGuidGVue = Guid.NewGuid().ToString();
            // change le GuidVue
            XmlSetAllAttNewValue(elGlobal, "GuidVue", sGuidVue, sNewGuidVue);
            if(bCpyOneVue) XmlSetAllAttNewValue(elGlobal, "GuidVueInf", sGuidVue, sNewGuidVue);
            XmlSetAllAttNewValue(elVue, "GuidObject", sGuidVue, sNewGuidVue);
            // change le GuidGVue
            XmlSetAllAttNewValue(elVue, "GuidGVue", sGuidGVue, sNewGuidGVue);
            // change Texte
            XmlSetTexteFromEl(elVue, sNewGuidGVue);

            XmlElement elAfterVue = XmlGetFirstElFromParent(elVue, "After");
            IEnumerator ienum = elAfterVue.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    XmlElement elCur = (XmlElement)Node;

                    Table t;
                    
                    int n = F.oCnxBase.ConfDB.FindTable(elCur.Name);
                    if (n > -1)
                    {
                        t = (Table)F.oCnxBase.ConfDB.LstTable[n];

                        //string sKey = t.GetKey();
                        switch (t.Name)
                        {
                            case "Module":
                            case  "AppUser" :
                            case "Link":
                            case "MainComposant":
                            case "Composant":
                            case "Base":
                            case "Interface":
                            case "File":
                            case "Server":
                            case "TechLink":
                            case "InterLink":
                            case "MCompServ":
                                ChangeObjet(elGlobal, elCur, t.Name, lstChangeObjet);
                                break;
                        }
                    }
                }
            }
        }

        
        public void XmlCreatExternRef(XmlElement elParent, int TableParent, int TableEx, string sKey)
        {
            //Select les champs obligatoires pour creer la ref et les attributs obligatoires dans le fichier xml 
            string Select, From, Where;
            CnxBase cnx = F.oCnxBase;
            Table t;
            if (TableEx > -1 && sKey != "")
            {
                t = (Table)cnx.ConfDB.LstTable[TableEx];
                Select = "SELECT " + t.GetSelectFieldFromOption(t.ExternalKeyTableOption);
                From = "FROM " + t.Name;
                //Where = "WHERE Guid" + t.Name + "='" + sKey + "'";
                Where = "WHERE " + (TableParent == -1 ? "Guid" + t.Name : t.GetSearchKey((Table)cnx.ConfDB.LstTable[TableParent])) + "='" + sKey + "'";

                XmlExcel xmlExcel = new XmlExcel(F, t.Name);
                if (cnx.CBRecherche(Select + " " + From + " " + Where))
                {
                    while (cnx.Reader.Read())
                        xmlExcel.CreatXmlFromReader(t.Name, t.ExternalKeyTableOption);
                }
                cnx.CBReaderClose();

                XmlElement root = xmlExcel.docXml.DocumentElement;
                IEnumerator ienum = root.GetEnumerator();
                XmlNode Node;

                while (ienum.MoveNext())
                {
                    Node = (XmlNode)ienum.Current;
                    switch (Node.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (Node.Name == "row")
                            {
                                if (Node.Attributes[0].Name == "NbrField")
                                {
                                    IEnumerator ienumField = Node.GetEnumerator();
                                    XmlNode NodeField;
                                    XmlElement el = XmlCreatEl(elParent, t.Name, t.GetKey());
                                    XmlElement elAtts = XmlGetFirstElFromParent(el, "Attributs");
                                    ArrayList lstsKey = new ArrayList();
                                    ArrayList lstiTable = new ArrayList();

                                    while (ienumField.MoveNext())
                                    {
                                        NodeField = (XmlNode)ienumField.Current;
                                        switch (NodeField.NodeType)
                                        {
                                            case XmlNodeType.Element:
                                                

                                                int iField = t.FindField(t.LstField, NodeField.Name);
                                                if(iField > -1)
                                                {
                                                    if ((((Field)t.LstField[iField]).fieldOption & ConfDataBase.FieldOption.ExternKeyTable) != 0)
                                                    {
                                                        lstiTable.Add(((Field)t.LstField[iField]).TableEx);
                                                        lstsKey.Add(NodeField.InnerText);
                                                    }
                                                    XmlSetAttFromEl(elAtts, ((Field)t.LstField[iField]).Name, ((Field)t.LstField[iField]).Type.ToString(), NodeField.InnerText);
                                                } 
                                                break;
                                        }
                                    }
                                    for (int i = 0; i < lstsKey.Count; i++) XmlCreatExternRef(XmlGetFirstElFromParent(el, "Before"), TableEx, (int)lstiTable[i], (string)lstsKey[i]);
                                    XmlCreatIndicator(el);
                                    XmlCreatComment(el, sKey);
                                }
                            }
                            break;
                    }
                    //XmlElement el = XmlCreatEl(elParent, t.Name, ((Field)t.LstField[0]).Name);
                    //XmlElement elAtts = XmlGetFirstElFromParent(el, "Attributs");
                    //ArrayList lstsKey = new ArrayList();
                    //ArrayList lstiTable = new ArrayList();

                    /*for (int i = 0; i < t.LstField.Count; i++)
                    {
                        if ((((Field)t.LstField[i]).fieldOption & t.ExternalKeyTableOption) != 0)
                        {
                            switch (((Field)t.LstField[i]).Type)
                            {
                                case 's':
                                case 'o':
                                    if (!cnx.Reader.IsDBNull(iReader))
                                    {
                                        if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.ExternKeyTable) != 0)
                                        {
                                            lstiTable.Add(((Field)t.LstField[i]).TableEx);
                                            lstsKey.Add(F.oCnxBase.Reader.GetString(iReader));
                                        }
                                        XmlSetAttFromEl(elAtts, ((Field)t.LstField[i]).Name, "s", cnx.Reader.GetString(iReader));
                                    }
                                    break;
                                case 'i':
                                case 'p': // picture
                                    if (!cnx.Reader.IsDBNull(iReader))
                                        XmlSetAttFromEl(elAtts, ((Field)t.LstField[i]).Name, "i", cnx.Reader.GetInt32(iReader).ToString());
                                    break;
                                case 'd':
                                    if (!cnx.Reader.IsDBNull(iReader))
                                        XmlSetAttFromEl(elAtts, ((Field)t.LstField[i]).Name, "d", cnx.Reader.GetDouble(iReader).ToString());
                                    break;
                                case 't':
                                    if (!cnx.Reader.IsDBNull(iReader))
                                        XmlSetAttFromEl(elAtts, ((Field)t.LstField[i]).Name, "t", cnx.Reader.GetDate(iReader).ToShortDateString());
                                    break;

                            }
                            iReader++;
                        }
                    }*/

                    /*for (int i = 0; i < lstsKey.Count; i++) XmlCreatExternRef(XmlGetFirstElFromParent(el, "Before"), TableEx, (int)lstiTable[i], (string)lstsKey[i]);
                    XmlCreatIndicator(el);
                    XmlCreatComment(el, sKey);*/
                }
            }
        }
    }

}
