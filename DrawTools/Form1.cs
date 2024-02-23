#define WRITE //Form1 & ToolPointer
#define _APIREADY
#define CLUSTERREADY
#define _OIDC
#define _HABILITATION

using IdentityModel.OidcClient;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Diagnostics;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using MOI = Microsoft.Office.Interop;

//using System.Net; 
//using System.Text;



/// DrawTools sample application.
/// Written by Alex Farber
/// alexf2062@yahoo.com
/// 
/// Drawing of graphics shapes (line, rectangle,
/// ellipse, polygon) on the window client area
/// using mouse.
/// Program works like MFC sample DRAWCLI
/// and uses some design decisions from this sample.
/// 
/// Dependencies: DocToolkit Library.



namespace DrawTools
{
    /// <summary>
    /// Main application form
    /// </summary>
    /// 


    public class Form1 : System.Windows.Forms.Form
    {
        //static string _authority = "http://127.0.0.1:8080/auth/realms/Test";
        //static string _api = "http://127.0.0.1:8080/auth/realms/Test/api";$
        //static OidcClient _oidcClient;
        //static HttpClient _apiClient = new HttpClient { BaseAddress = new Uri(_api) };

        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuFileNew;
        private System.Windows.Forms.MenuItem menuFileOpen;
        private System.Windows.Forms.MenuItem menuFileSave;
        private System.Windows.Forms.MenuItem menuFileSaveAs;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuFileRecentFiles;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem menuFileExit;
        private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.MenuItem menuHelpAbout;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.ToolBarButton tbNew;
        private System.Windows.Forms.ToolBarButton tbOpen;
        private System.Windows.Forms.ToolBarButton tbSave;
        private System.Windows.Forms.ToolBarButton tbAbout;
        public DrawTools.DrawArea drawArea;
        private System.Windows.Forms.ToolBarButton tbPointer;
        private System.Windows.Forms.ToolBarButton tbRectangle;
        private System.Windows.Forms.ToolBarButton tbEllipse;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuDrawPointer;
        private System.Windows.Forms.MenuItem menuDrawRectangle;
        private System.Windows.Forms.MenuItem menuDrawEllipse;
        private System.Windows.Forms.ToolBarButton tbLine;
        private System.Windows.Forms.MenuItem menuDrawLine;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuEditSelectAll;
        private System.Windows.Forms.MenuItem menuEditUnselectAll;
        private System.Windows.Forms.MenuItem menuEditDelete;
        private System.Windows.Forms.MenuItem menuEditDeleteAll;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuEditMoveToFront;
        private System.Windows.Forms.MenuItem menuEditMoveToBack;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuEditProperties;
        private System.Windows.Forms.ToolBarButton tbPolygon;
        private System.Windows.Forms.MenuItem menuDrawPolygon;
        private System.ComponentModel.IContainer components;
        private ToolBarButton tbLink;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private ToolBarButton tbServer;
        private SplitContainer splitContainer3;
        public DataGridView dataGrid;
        public ComboBox cbVue;
        private Label Vue;
        private Label Type;
        public string SelectedBase;
        private ToolBarButton tbseparator1;
        private ToolBarButton tbComposant;
        private ToolBarButton tbTechno;
        private ToolBarButton tbcard;
        private ToolBarButton tbVlan;
        private ToolBarButton tbLinkA;
        private ToolBarButton tbLinkI;
        private ToolBarButton tbServeurE;
        private Button bSave;
        public ComboBox cbApplication;
        private Label NomApplication;
        public Button bAdd;
        public TreeView tvObjet;
        private ToolBarButton tbBase;
        private ToolBarButton tbFile;
        private ToolBarButton tbMainComposant;
        private ToolBarButton tbCompFonc;
        private ToolBarButton tbServMComp;
        private ToolBarButton tbRouter;
        private DataGridViewTextBoxColumn Propriete;
        private DataGridViewTextBoxColumn Valeur;
        private DataGridViewButtonColumn Pls;
        private DataGridViewTextBoxColumn NonVisible;
        private ToolBarButton tbUser;
        private ToolBarButton tbApplication;
        private ToolBarButton tbInterface;
        public TextBox tbGuid;
        private ToolBarButton tbEACB;
        private ToolBarButton tbCluster;
        private ToolBarButton tbMachine;
        private ToolBarButton tbBaie1;
        private ToolBarButton tbLun;
        private ToolBarButton tbZone;
        private ToolBarButton tbVirtuel;
        private ToolBarButton tbBaiePhy;
        private ToolBarButton tbseparator8;
        private ToolBarButton tbMachineCTI;
        private ToolBarButton tbBaieCTI;
        private ToolBarButton tbDrawer;
        private ToolBarButton tbSanCard;
        private ToolBarButton tbSanSwitch;
        private ToolBarButton tbISL;
        private ToolBarButton tbBaieDPhy;
        private MenuItem menuItem7;

        private System.Windows.Forms.ToolBarButton tbModule;
        private MenuItem menuItem9;
        private ToolBarButton tbStatut;
        private MenuItem menuItem11;
        private MenuItem menuItem12;
        private MenuItem menuItem13;
        private MenuItem menuItem14;
        public ComboBox cbGuidVue;
        private MenuItem menuItem15;
        private MenuItem menuItem16;
        private ToolBarButton tbSite;
        private ToolBarButton tbServerSite;
        private ToolBarButton tbCnx;
        private MenuItem menuItem17;
        private MenuItem menuItem18;
        private ToolBarButton tbPtCnx;
        private MenuItem menuItem19;
        private ToolBarButton tbTechUser;
        private ToolBarButton tbCadreRefN;
        private ToolBarButton tbCadreRefN1;
        private Label label1;
        private ToolBarButton tbIndicator;
        private MenuItem menuItem20;
        private MenuItem menuItem21;
        private MenuItem menuItem22;
        private MenuItem menuItem23;
        private MenuItem menuItem24;
        private MenuItem menuItem25;
        private ToolBarButton tbInterLink;
        private MenuItem menuItem26;
        private ToolBarButton tbPatrimoine;
        private ToolBarButton tbVisio;
        private ToolBarButton tbSi;
        private MenuItem menuItem27;
        private ToolBarButton tbAxes;
        private ToolBarButton tbReport;
        private MenuItem menuItem28;
        private MenuItem menuItem29;
        private MenuItem menuItem30;
        private MenuItem menuItem32;
        private MenuItem menuItem33;
        private Label label2;
        private MenuItem menuItem34;
        private MenuItem menuItem35;
        private MenuItem menuItem31;
        private MenuItem menuItem36;
        private MenuItem menuItem37;
        private MenuItem menuItem38;
        private MenuItem menuItem39;
        private MenuItem menuItem40;
        private MenuItem menuItem41;
        private MenuItem menuItem42;
        private MenuItem menuItem43;
        private MenuItem menuItem44;
        private MenuItem menuItem45;
        private MenuItem menuItem46;
        private MenuItem menuItem47;
        private MenuItem menuItem48;
        public ComboBox cbGuidApplication;
        private ToolBarButton tbExportXLSReport;
        private Button bOpApp;
        private Button bOpVue;
        public ComboBox cbOpApp;
        public ComboBox cbOpVue;
        private TextBox tbEnv;
        private TextBox tbVueInf;
        public TextBox tbTypeVue;
        private MenuItem menuItem49;
        private MenuItem menuItem51;
        private MenuItem menuItem52;
        public Button bLayer;


        /* public enum NodesInfra
         {
             User = 0,
             Application = 1,
             Composant = 2,
             Link = 3,
             Technologie = 4,
             Service = 5,
         }*/

        public enum rbTypeRecherche
        {
            Vide = 0x00,
            Application = 0x01,
            Server = 0x02,
            Techno = 0x04,
        }

        public enum ImgList
        {
            pass,
            alert,
            fail,
            Nettbd,  //3 (0) - Non définit
            NetSstG, //4 (1) - Standard Group
            NetSst,  //5 (2) - Standard LS
            NetSstV, //6 (3) - Standard LS - Conflit Version
            NetSstP, //7 (4) - Standard LS - Conflit Product
            NetSnst, //8 (5) - Non Standard      
            OS,      // 9
        }


        public string[] sImgList = {
            "bmp\\pass.png",        //0
            "bmp\\alert.png",       //1
            "bmp\\fail.png",        //2
            "bmp\\vide.png",        //3
            "bmp\\nm.gif",          //4
            "bmp\\st.gif",          //5
            "bmp\\em.gif",          //6
            "bmp\\nr.gif",          //7
            "bmp\\nr.gif",        //8
            "bmp\\vide.png",        //9
            "bmp\\linux.png",
            "bmp\\aix6.png",
            "bmp\\win.png",
            "bmp\\aix5.png",
            "bmp\\as.png",
            "bmp\\appliance.png",
        };
        private MenuItem menuItem50;
        public string[] sStatutTechno =
        {
            "Not_Defined",
            "Group_IT_Standard",
            "Complementary_Choice",
            "Conflict_Version",
            "Conflict_Product",
            "Decommissioning"
        };

        private MenuItem menuItem53;
        private MenuItem menuItem54;
        private MenuItem menuItem55;
        private MenuItem menuItem56;
        private MenuItem menuItem57;
        private Label Ver;
        public ComboBox cbVersion;
        public Button bDescriptionApp;
        private MenuItem menuItem58;
        private MenuItem menuItem59;
        private ToolBarButton tbQueue;
        private ToolBarButton tbNatRule;
        private MenuItem menuItem60;
        private ToolBarButton tbFluxApp;
        private ToolBarButton tbFluxBoutEnBout;
        private ToolBarButton tbFlux;
        private ToolBarButton tbFluxBoutEnBoutFonc;
        private ToolBarButton tbGenks;
        private ToolBarButton tbGenpod;
        private ToolBarButton tbGening;
        private ToolBarButton tbGensvc;
        private ToolBarButton tbInsks;
        private ToolBarButton tbContainer;
        private ToolBarButton tbGensas;
        private ToolBarButton tbManagedsvc;
        private ToolBarButton tbPattern;
        private ToolBarButton tbPatternIns;
        private TextBox tbFind;
        private Label label3;
        private ToolBarButton tbInsnd;
        public Button bDescriptionVue;
        public ArrayList lstSaveObj = new ArrayList();
        public bool bCreatsousObj;
        private MenuItem menuItem61;
        private MenuItem menuItem62;
        private ToolBarButton tbInssas;
        private MenuItem menuItem63;
        private MenuItem menuItem64;
        private MenuItem menuItem65;
        private MenuItem menuItem66;
        private MenuItem menuItem67;
        public Color[] aColor =
        {
            Color.Blue,
            Color.BlueViolet,
            Color.Brown,
            Color.BurlyWood,
            Color.CadetBlue,
            Color.Chocolate,
            Color.Coral,
            Color.CornflowerBlue,
            Color.Crimson,
            Color.Cyan,
            Color.DarkBlue,
            Color.DarkCyan,
            Color.DarkGoldenrod,
            Color.DarkGray,
            Color.DarkGreen,
            Color.DarkKhaki,
            Color.DarkMagenta,
            Color.DarkOliveGreen,
            Color.DarkOrange,
            Color.DarkOrchid,
            Color.DarkRed,
            Color.DarkSalmon,
            Color.DarkSeaGreen,
            Color.DarkSlateBlue,
            Color.DarkSlateGray,
            Color.DarkTurquoise,
            Color.DarkViolet,
            Color.DeepPink,
            Color.DeepSkyBlue
        };

        public void initlstTabProduit(List<string> lstLibTab)
        {
            //GuidProduit, NomProduit Supplier Scope, SupportTeam, Entity, SubEntity, MandatedEntityForSupport, 
            //Root, Family, Subfamily, Topic, TechnologyArea, UseCase, Comments, ShowOrHide, Catalogue, UserID, UpdateDate

            lstLibTab.Add("GuidProduit");                    //Produit
            lstLibTab.Add("NomProduit");                      //Produit
            lstLibTab.Add("Supplier");                        //Produit
            lstLibTab.Add("Scope");                           //Produit
            lstLibTab.Add("SupportTeam");
            lstLibTab.Add("Entity");
            lstLibTab.Add("SubEntity");
            lstLibTab.Add("MandatedEntityForSupport");
            lstLibTab.Add("Root");
            lstLibTab.Add("Family");                          //CadreRef
            lstLibTab.Add("SubFamily");                       //CadreRef
            lstLibTab.Add("Topic");                           //CadreRef --> Recup Guid pour Produit
            lstLibTab.Add("TechnologyArea");                  //TechnoArea --> Recup Guid pour Produit
            lstLibTab.Add("UseCase");                         //Produit
            lstLibTab.Add("Comments");
            lstLibTab.Add("ShowOrHide");                      //Produit (Catalogue)
            lstLibTab.Add("UserID");
            lstLibTab.Add("UpdateDate");
        }

        public void initlstTabTechno(List<string> lstLibTab)
        {
            //GuidTechnoRef, TechnologyName, TechnologyVersion, TechnologyType, ObsoScore, GuidProduit, DerogationEndDate, GroupITStandardInCompetition, 
            //RoadmapUpcomingStartDate, RoadmapUpcomingEndDate, RoadmapReferenceStartDate, RoadmapReferenceEndDate, RoadmapConfinedStartDate,
            //RoadmapConfinedEndDate, RoadmapDecommissionedStartDate, RoadmapDecommissionedEndDate, RoadmapSupplierEndOfSupportDate, UserID, UpdateDate

            lstLibTab.Add("GuidTechnoRef");                   //TechnoRef
            lstLibTab.Add("TechnologyName");                  //TechnoRef
            lstLibTab.Add("TechnologyVersion");               //TechnoRef
            lstLibTab.Add("TechnologyType");                  // Norme
            lstLibTab.Add("ObsoScore");
            lstLibTab.Add("GuidProduit");
            lstLibTab.Add("DerogationEndDate");
            lstLibTab.Add("GroupITStandardInCompetition");
            lstLibTab.Add("RoadmapUpcomingStartDate");        //TechnoRef
            lstLibTab.Add("RoadmapUpcomingEndDate");          //TechnoRef
            lstLibTab.Add("RoadmapReferenceStartDate");       //TechnoRef
            lstLibTab.Add("RoadmapReferenceEndDate");         //TechnoRef
            lstLibTab.Add("RoadmapConfinedStartDate");        //TechnoRef
            lstLibTab.Add("RoadmapConfinedEndDate");          //TechnoRef
            lstLibTab.Add("RoadmapDecommissionedStartDate");  //TechnoRef
            lstLibTab.Add("RoadmapDecommissionedEndDate");    //TechnoRef
            lstLibTab.Add("RoadmapSupplierEndOfSupportDate"); //TechnoRef
            lstLibTab.Add("UserID");
            lstLibTab.Add("UpdateDate");
        }

        #region Constructor, Dispose

        public Form1()
        {
            // Initialise Lst Nom User
            initKnownDTUserLst();

            //Initialise la connexion à la base
            oCnxBase = new CnxBase(this);
            if (SelectedBase != null)
            {

                // Initialiseles habilitations
                Compte.InitHabilitations(oCnxBase);
                Compte.InitCompteRights();
                //
                // Required for Windows Form Designer support
                //
                InitializeComponent();
                InitEvent();
                Compte.SetRight(this);
            }
        }



        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuFileNew = new System.Windows.Forms.MenuItem();
            this.menuFileOpen = new System.Windows.Forms.MenuItem();
            this.menuFileSaveAs = new System.Windows.Forms.MenuItem();
            this.menuFileSave = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuFileRecentFiles = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuFileExit = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuEditSelectAll = new System.Windows.Forms.MenuItem();
            this.menuEditUnselectAll = new System.Windows.Forms.MenuItem();
            this.menuEditDelete = new System.Windows.Forms.MenuItem();
            this.menuEditDeleteAll = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuEditMoveToFront = new System.Windows.Forms.MenuItem();
            this.menuEditMoveToBack = new System.Windows.Forms.MenuItem();
            this.menuItem33 = new System.Windows.Forms.MenuItem();
            this.menuItem41 = new System.Windows.Forms.MenuItem();
            this.menuItem42 = new System.Windows.Forms.MenuItem();
            this.menuItem43 = new System.Windows.Forms.MenuItem();
            this.menuItem44 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuEditProperties = new System.Windows.Forms.MenuItem();
            this.menuItem20 = new System.Windows.Forms.MenuItem();
            this.menuItem23 = new System.Windows.Forms.MenuItem();
            this.menuItem27 = new System.Windows.Forms.MenuItem();
            this.menuItem48 = new System.Windows.Forms.MenuItem();
            this.menuItem52 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuDrawPointer = new System.Windows.Forms.MenuItem();
            this.menuDrawRectangle = new System.Windows.Forms.MenuItem();
            this.menuDrawEllipse = new System.Windows.Forms.MenuItem();
            this.menuDrawLine = new System.Windows.Forms.MenuItem();
            this.menuDrawPolygon = new System.Windows.Forms.MenuItem();
            this.menuItem31 = new System.Windows.Forms.MenuItem();
            this.menuItem51 = new System.Windows.Forms.MenuItem();
            this.menuItem36 = new System.Windows.Forms.MenuItem();
            this.menuItem37 = new System.Windows.Forms.MenuItem();
            this.menuItem38 = new System.Windows.Forms.MenuItem();
            this.menuItem39 = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.menuItem21 = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.menuItem22 = new System.Windows.Forms.MenuItem();
            this.menuItem49 = new System.Windows.Forms.MenuItem();
            this.menuItem50 = new System.Windows.Forms.MenuItem();
            this.menuItem60 = new System.Windows.Forms.MenuItem();
            this.menuItem24 = new System.Windows.Forms.MenuItem();
            this.menuItem62 = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.menuItem25 = new System.Windows.Forms.MenuItem();
            this.menuItem59 = new System.Windows.Forms.MenuItem();
            this.menuItem67 = new System.Windows.Forms.MenuItem();
            this.menuItem26 = new System.Windows.Forms.MenuItem();
            this.menuItem34 = new System.Windows.Forms.MenuItem();
            this.menuItem28 = new System.Windows.Forms.MenuItem();
            this.menuItem29 = new System.Windows.Forms.MenuItem();
            this.menuItem30 = new System.Windows.Forms.MenuItem();
            this.menuItem58 = new System.Windows.Forms.MenuItem();
            this.menuItem32 = new System.Windows.Forms.MenuItem();
            this.menuItem45 = new System.Windows.Forms.MenuItem();
            this.menuItem46 = new System.Windows.Forms.MenuItem();
            this.menuItem47 = new System.Windows.Forms.MenuItem();
            this.menuItem56 = new System.Windows.Forms.MenuItem();
            this.menuItem57 = new System.Windows.Forms.MenuItem();
            this.menuItem35 = new System.Windows.Forms.MenuItem();
            this.menuItem55 = new System.Windows.Forms.MenuItem();
            this.menuItem61 = new System.Windows.Forms.MenuItem();
            this.menuItem40 = new System.Windows.Forms.MenuItem();
            this.menuItem53 = new System.Windows.Forms.MenuItem();
            this.menuItem54 = new System.Windows.Forms.MenuItem();
            this.menuItem63 = new System.Windows.Forms.MenuItem();
            this.menuItem64 = new System.Windows.Forms.MenuItem();
            this.menuItem65 = new System.Windows.Forms.MenuItem();
            this.menuItem66 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuHelpAbout = new System.Windows.Forms.MenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.tbNew = new System.Windows.Forms.ToolBarButton();
            this.tbOpen = new System.Windows.Forms.ToolBarButton();
            this.tbSave = new System.Windows.Forms.ToolBarButton();
            this.tbseparator1 = new System.Windows.Forms.ToolBarButton();
            this.tbPointer = new System.Windows.Forms.ToolBarButton();
            this.tbRectangle = new System.Windows.Forms.ToolBarButton();
            this.tbEllipse = new System.Windows.Forms.ToolBarButton();
            this.tbLine = new System.Windows.Forms.ToolBarButton();
            this.tbPolygon = new System.Windows.Forms.ToolBarButton();
            this.tbModule = new System.Windows.Forms.ToolBarButton();
            this.tbLink = new System.Windows.Forms.ToolBarButton();
            this.tbUser = new System.Windows.Forms.ToolBarButton();
            this.tbApplication = new System.Windows.Forms.ToolBarButton();
            this.tbMainComposant = new System.Windows.Forms.ToolBarButton();
            this.tbInterface = new System.Windows.Forms.ToolBarButton();
            this.tbComposant = new System.Windows.Forms.ToolBarButton();
            this.tbBase = new System.Windows.Forms.ToolBarButton();
            this.tbQueue = new System.Windows.Forms.ToolBarButton();
            this.tbFile = new System.Windows.Forms.ToolBarButton();
            this.tbLinkA = new System.Windows.Forms.ToolBarButton();
            this.tbFluxBoutEnBoutFonc = new System.Windows.Forms.ToolBarButton();
            this.tbServer = new System.Windows.Forms.ToolBarButton();
            this.tbGenks = new System.Windows.Forms.ToolBarButton();
            this.tbGenpod = new System.Windows.Forms.ToolBarButton();
            this.tbGensas = new System.Windows.Forms.ToolBarButton();
            this.tbManagedsvc = new System.Windows.Forms.ToolBarButton();
            this.tbContainer = new System.Windows.Forms.ToolBarButton();
            this.tbGening = new System.Windows.Forms.ToolBarButton();
            this.tbGensvc = new System.Windows.Forms.ToolBarButton();
            this.tbPattern = new System.Windows.Forms.ToolBarButton();
            this.tbPatternIns = new System.Windows.Forms.ToolBarButton();
            this.tbInsks = new System.Windows.Forms.ToolBarButton();
            this.tbTechno = new System.Windows.Forms.ToolBarButton();
            this.tbLinkI = new System.Windows.Forms.ToolBarButton();
            this.tbFluxBoutEnBout = new System.Windows.Forms.ToolBarButton();
            this.tbEACB = new System.Windows.Forms.ToolBarButton();
            this.tbStatut = new System.Windows.Forms.ToolBarButton();
            this.tbSite = new System.Windows.Forms.ToolBarButton();
            this.tbPtCnx = new System.Windows.Forms.ToolBarButton();
            this.tbCnx = new System.Windows.Forms.ToolBarButton();
            this.tbServeurE = new System.Windows.Forms.ToolBarButton();
            this.tbInsnd = new System.Windows.Forms.ToolBarButton();
            this.tbInssas = new System.Windows.Forms.ToolBarButton();
            this.tbcard = new System.Windows.Forms.ToolBarButton();
            this.tbNatRule = new System.Windows.Forms.ToolBarButton();
            this.tbVlan = new System.Windows.Forms.ToolBarButton();
            this.tbRouter = new System.Windows.Forms.ToolBarButton();
            this.tbFluxApp = new System.Windows.Forms.ToolBarButton();
            this.tbFlux = new System.Windows.Forms.ToolBarButton();
            this.tbCompFonc = new System.Windows.Forms.ToolBarButton();
            this.tbServMComp = new System.Windows.Forms.ToolBarButton();
            this.tbCluster = new System.Windows.Forms.ToolBarButton();
            this.tbMachine = new System.Windows.Forms.ToolBarButton();
            this.tbVirtuel = new System.Windows.Forms.ToolBarButton();
            this.tbBaie1 = new System.Windows.Forms.ToolBarButton();
            this.tbLun = new System.Windows.Forms.ToolBarButton();
            this.tbZone = new System.Windows.Forms.ToolBarButton();
            this.tbSanCard = new System.Windows.Forms.ToolBarButton();
            this.tbSanSwitch = new System.Windows.Forms.ToolBarButton();
            this.tbBaieCTI = new System.Windows.Forms.ToolBarButton();
            this.tbISL = new System.Windows.Forms.ToolBarButton();
            this.tbBaiePhy = new System.Windows.Forms.ToolBarButton();
            this.tbBaieDPhy = new System.Windows.Forms.ToolBarButton();
            this.tbMachineCTI = new System.Windows.Forms.ToolBarButton();
            this.tbInterLink = new System.Windows.Forms.ToolBarButton();
            this.tbDrawer = new System.Windows.Forms.ToolBarButton();
            this.tbseparator8 = new System.Windows.Forms.ToolBarButton();
            this.tbAbout = new System.Windows.Forms.ToolBarButton();
            this.tbServerSite = new System.Windows.Forms.ToolBarButton();
            this.tbTechUser = new System.Windows.Forms.ToolBarButton();
            this.tbCadreRefN = new System.Windows.Forms.ToolBarButton();
            this.tbCadreRefN1 = new System.Windows.Forms.ToolBarButton();
            this.tbIndicator = new System.Windows.Forms.ToolBarButton();
            this.tbPatrimoine = new System.Windows.Forms.ToolBarButton();
            this.tbVisio = new System.Windows.Forms.ToolBarButton();
            this.tbSi = new System.Windows.Forms.ToolBarButton();
            this.tbAxes = new System.Windows.Forms.ToolBarButton();
            this.tbReport = new System.Windows.Forms.ToolBarButton();
            this.tbExportXLSReport = new System.Windows.Forms.ToolBarButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.bDescriptionVue = new System.Windows.Forms.Button();
            this.tbFind = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTypeVue = new System.Windows.Forms.TextBox();
            this.bDescriptionApp = new System.Windows.Forms.Button();
            this.Type = new System.Windows.Forms.Label();
            this.Ver = new System.Windows.Forms.Label();
            this.cbVersion = new System.Windows.Forms.ComboBox();
            this.bLayer = new System.Windows.Forms.Button();
            this.tbVueInf = new System.Windows.Forms.TextBox();
            this.tbEnv = new System.Windows.Forms.TextBox();
            this.bOpVue = new System.Windows.Forms.Button();
            this.cbOpVue = new System.Windows.Forms.ComboBox();
            this.bOpApp = new System.Windows.Forms.Button();
            this.cbOpApp = new System.Windows.Forms.ComboBox();
            this.tbGuid = new System.Windows.Forms.TextBox();
            this.cbGuidApplication = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbGuidVue = new System.Windows.Forms.ComboBox();
            this.tvObjet = new System.Windows.Forms.TreeView();
            this.bAdd = new System.Windows.Forms.Button();
            this.NomApplication = new System.Windows.Forms.Label();
            this.cbApplication = new System.Windows.Forms.ComboBox();
            this.bSave = new System.Windows.Forms.Button();
            this.Vue = new System.Windows.Forms.Label();
            this.cbVue = new System.Windows.Forms.ComboBox();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.Propriete = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valeur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pls = new System.Windows.Forms.DataGridViewButtonColumn();
            this.NonVisible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drawArea = new DrawTools.DrawArea();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem3,
            this.menuItem2,
            this.menuItem31,
            this.menuItem15,
            this.menuItem10});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuFileNew,
            this.menuFileOpen,
            this.menuFileSaveAs,
            this.menuFileSave,
            this.menuItem7,
            this.menuItem6,
            this.menuFileRecentFiles,
            this.menuItem8,
            this.menuFileExit});
            this.menuItem1.Text = "File";
            // 
            // menuFileNew
            // 
            this.menuFileNew.Index = 0;
            this.menuFileNew.Text = "";
            // 
            // menuFileOpen
            // 
            this.menuFileOpen.Index = 1;
            this.menuFileOpen.Text = "";
            // 
            // menuFileSaveAs
            // 
            this.menuFileSaveAs.Index = 2;
            this.menuFileSaveAs.Text = "";
            // 
            // menuFileSave
            // 
            this.menuFileSave.Index = 3;
            this.menuFileSave.Text = "Save as Image";
            this.menuFileSave.Click += new System.EventHandler(this.menuFileSave_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 4;
            this.menuItem7.Text = "Options";
            this.menuItem7.Click += new System.EventHandler(this.menuOptions_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 5;
            this.menuItem6.Text = "-";
            // 
            // menuFileRecentFiles
            // 
            this.menuFileRecentFiles.Index = 6;
            this.menuFileRecentFiles.Text = "Recent Files";
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 7;
            this.menuItem8.Text = "-";
            // 
            // menuFileExit
            // 
            this.menuFileExit.Index = 8;
            this.menuFileExit.Text = "Exit";
            this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuEditSelectAll,
            this.menuEditUnselectAll,
            this.menuEditDelete,
            this.menuEditDeleteAll,
            this.menuItem4,
            this.menuEditMoveToFront,
            this.menuEditMoveToBack,
            this.menuItem33,
            this.menuItem41,
            this.menuItem42,
            this.menuItem43,
            this.menuItem44,
            this.menuItem5,
            this.menuEditProperties,
            this.menuItem20,
            this.menuItem23,
            this.menuItem27,
            this.menuItem48,
            this.menuItem52});
            this.menuItem3.Text = "Edit";
            // 
            // menuEditSelectAll
            // 
            this.menuEditSelectAll.Index = 0;
            this.menuEditSelectAll.Text = "Select All";
            this.menuEditSelectAll.Click += new System.EventHandler(this.menuEditSelectAll_Click);
            // 
            // menuEditUnselectAll
            // 
            this.menuEditUnselectAll.Index = 1;
            this.menuEditUnselectAll.Text = "Unselect All";
            this.menuEditUnselectAll.Click += new System.EventHandler(this.menuEditUnselectAll_Click);
            // 
            // menuEditDelete
            // 
            this.menuEditDelete.Index = 2;
            this.menuEditDelete.Text = "Delete";
            this.menuEditDelete.Click += new System.EventHandler(this.menuEditDelete_Click);
            // 
            // menuEditDeleteAll
            // 
            this.menuEditDeleteAll.Index = 3;
            this.menuEditDeleteAll.Text = "DeleteAll";
            this.menuEditDeleteAll.Click += new System.EventHandler(this.menuEditDeleteAll_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 4;
            this.menuItem4.Text = "-";
            // 
            // menuEditMoveToFront
            // 
            this.menuEditMoveToFront.Index = 5;
            this.menuEditMoveToFront.Text = "Move to Front";
            this.menuEditMoveToFront.Click += new System.EventHandler(this.menuEditMoveToFront_Click);
            // 
            // menuEditMoveToBack
            // 
            this.menuEditMoveToBack.Index = 6;
            this.menuEditMoveToBack.Text = "Move to Back";
            this.menuEditMoveToBack.Click += new System.EventHandler(this.menuEditMoveToBack_Click);
            // 
            // menuItem33
            // 
            this.menuItem33.Index = 7;
            this.menuItem33.Text = "Copy Dim";
            this.menuItem33.Click += new System.EventHandler(this.menuEditCopy_Click);
            // 
            // menuItem41
            // 
            this.menuItem41.Index = 8;
            this.menuItem41.Text = "Top Align";
            this.menuItem41.Click += new System.EventHandler(this.TopAlign_Click);
            // 
            // menuItem42
            // 
            this.menuItem42.Index = 9;
            this.menuItem42.Text = "Bottom Align";
            this.menuItem42.Click += new System.EventHandler(this.BottomAlign_Click);
            // 
            // menuItem43
            // 
            this.menuItem43.Index = 10;
            this.menuItem43.Text = "Left Align";
            this.menuItem43.Click += new System.EventHandler(this.LeftAlign_Click);
            // 
            // menuItem44
            // 
            this.menuItem44.Index = 11;
            this.menuItem44.Text = "Right Align";
            this.menuItem44.Click += new System.EventHandler(this.RightAlign_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 12;
            this.menuItem5.Text = "-";
            // 
            // menuEditProperties
            // 
            this.menuEditProperties.Index = 13;
            this.menuEditProperties.Text = "Properties";
            this.menuEditProperties.Click += new System.EventHandler(this.menuEditProperties_Click);
            // 
            // menuItem20
            // 
            this.menuItem20.Index = 14;
            this.menuItem20.Text = "Link Diagram";
            this.menuItem20.Click += new System.EventHandler(this.menuEditLinkDiagrem_Click);
            // 
            // menuItem23
            // 
            this.menuItem23.Index = 15;
            this.menuItem23.Text = "Object Explorer";
            this.menuItem23.Click += new System.EventHandler(this.menuEditObjectExplorer_Click);
            // 
            // menuItem27
            // 
            this.menuItem27.Index = 16;
            this.menuItem27.Text = "Link View";
            this.menuItem27.Click += new System.EventHandler(this.menuEditLinkView_Click);
            // 
            // menuItem48
            // 
            this.menuItem48.Index = 17;
            this.menuItem48.Text = "rules";
            this.menuItem48.Click += new System.EventHandler(this.menuEditRules_Click);
            // 
            // menuItem52
            // 
            this.menuItem52.Index = 18;
            this.menuItem52.Text = "Asign Layer";
            this.menuItem52.Click += new System.EventHandler(this.menuSelectLayer_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 2;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuDrawPointer,
            this.menuDrawRectangle,
            this.menuDrawEllipse,
            this.menuDrawLine,
            this.menuDrawPolygon});
            this.menuItem2.Text = "Draw";
            this.menuItem2.Visible = false;
            // 
            // menuDrawPointer
            // 
            this.menuDrawPointer.Index = 0;
            this.menuDrawPointer.Text = "Pointer";
            this.menuDrawPointer.Click += new System.EventHandler(this.menuDrawPointer_Click);
            // 
            // menuDrawRectangle
            // 
            this.menuDrawRectangle.Index = 1;
            this.menuDrawRectangle.Text = "Rectangle";
            this.menuDrawRectangle.Click += new System.EventHandler(this.menuDrawRectangle_Click);
            // 
            // menuDrawEllipse
            // 
            this.menuDrawEllipse.Index = 2;
            this.menuDrawEllipse.Text = "Ellipse";
            this.menuDrawEllipse.Click += new System.EventHandler(this.menuDrawEllipse_Click);
            // 
            // menuDrawLine
            // 
            this.menuDrawLine.Index = 3;
            this.menuDrawLine.Text = "Line";
            this.menuDrawLine.Click += new System.EventHandler(this.menuDrawLine_Click);
            // 
            // menuDrawPolygon
            // 
            this.menuDrawPolygon.Index = 4;
            this.menuDrawPolygon.Text = "Pencil";
            this.menuDrawPolygon.Click += new System.EventHandler(this.menuDrawPolygon_Click);
            // 
            // menuItem31
            // 
            this.menuItem31.Index = 3;
            this.menuItem31.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem51,
            this.menuItem36});
            this.menuItem31.Text = "Create";
            // 
            // menuItem51
            // 
            this.menuItem51.Index = 0;
            this.menuItem51.Text = "Layer Manager";
            this.menuItem51.Click += new System.EventHandler(this.menuCreateLayer_Click);
            // 
            // menuItem36
            // 
            this.menuItem36.Index = 1;
            this.menuItem36.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem37,
            this.menuItem38,
            this.menuItem39});
            this.menuItem36.Text = "IP Service";
            // 
            // menuItem37
            // 
            this.menuItem37.Index = 0;
            this.menuItem37.Text = "Services";
            this.menuItem37.Click += new System.EventHandler(this.GroupService_Click);
            // 
            // menuItem38
            // 
            this.menuItem38.Index = 1;
            this.menuItem38.Text = "Ports";
            this.menuItem38.Click += new System.EventHandler(this.Service_Click);
            // 
            // menuItem39
            // 
            this.menuItem39.Index = 2;
            this.menuItem39.Text = "Link Services-Ports";
            this.menuItem39.Click += new System.EventHandler(this.ServiceLink_Click);
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 4;
            this.menuItem15.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem11,
            this.menuItem17,
            this.menuItem24,
            this.menuItem34,
            this.menuItem40,
            this.menuItem53,
            this.menuItem63});
            this.menuItem15.Text = "Analyze";
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 0;
            this.menuItem11.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem12,
            this.menuItem13,
            this.menuItem14,
            this.menuItem21});
            this.menuItem11.Text = "Cadre de référence";
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 0;
            this.menuItem12.Text = "Ref Fonctionnel";
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 1;
            this.menuItem13.Text = "Ref Applicatif";
            this.menuItem13.Click += new System.EventHandler(this.menuRefApp_Click);
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 2;
            this.menuItem14.Text = "Ref Technique Soft";
            this.menuItem14.Click += new System.EventHandler(this.menuRefTechnique_Click);
            // 
            // menuItem21
            // 
            this.menuItem21.Index = 3;
            this.menuItem21.Text = "Ref Technique Hard";
            this.menuItem21.Click += new System.EventHandler(this.menuItem21_Click);
            // 
            // menuItem17
            // 
            this.menuItem17.Index = 1;
            this.menuItem17.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem18,
            this.menuItem9,
            this.menuItem19,
            this.menuItem22,
            this.menuItem49,
            this.menuItem50,
            this.menuItem60});
            this.menuItem17.Text = "Application";
            // 
            // menuItem18
            // 
            this.menuItem18.Index = 0;
            this.menuItem18.Text = "Provisioning Serveurs";
            this.menuItem18.Click += new System.EventHandler(this.menuProvisionServer_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 1;
            this.menuItem9.Text = "Etat des Technologies";
            this.menuItem9.Click += new System.EventHandler(this.menuStatutApp_Click);
            // 
            // menuItem19
            // 
            this.menuItem19.Index = 2;
            this.menuItem19.Text = "TAD Updating";
            this.menuItem19.Click += new System.EventHandler(this.menuTadUpdating_Click);
            // 
            // menuItem22
            // 
            this.menuItem22.Index = 3;
            this.menuItem22.Text = "Object Explorer";
            this.menuItem22.Click += new System.EventHandler(this.menuObjectExplorer_Click);
            // 
            // menuItem49
            // 
            this.menuItem49.Index = 4;
            this.menuItem49.Text = "List Applications Export";
            this.menuItem49.Click += new System.EventHandler(this.menuApplicationsExport_Click);
            // 
            // menuItem50
            // 
            this.menuItem50.Index = 5;
            this.menuItem50.Text = "Obsolescence Map";
            this.menuItem50.Click += new System.EventHandler(this.menuObsolescenceMap_Click);
            // 
            // menuItem60
            // 
            this.menuItem60.Index = 6;
            this.menuItem60.Text = "Info Cloud";
            this.menuItem60.Click += new System.EventHandler(this.ApiDiscovery_Click);
            // 
            // menuItem24
            // 
            this.menuItem24.Index = 2;
            this.menuItem24.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem62,
            this.menuItem16,
            this.menuItem25,
            this.menuItem59,
            this.menuItem67,
            this.menuItem26});
            this.menuItem24.Text = "Flows";
            // 
            // menuItem62
            // 
            this.menuItem62.Index = 0;
            this.menuItem62.Text = "IP/Vlan";
            this.menuItem62.Click += new System.EventHandler(this.menuItem62_Click);
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 1;
            this.menuItem16.Text = "Flow requests";
            this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
            // 
            // menuItem25
            // 
            this.menuItem25.Index = 2;
            this.menuItem25.Text = "Flow Rules";
            this.menuItem25.Click += new System.EventHandler(this.menuItem25_Click);
            // 
            // menuItem59
            // 
            this.menuItem59.Index = 3;
            this.menuItem59.Text = "Mise à jour Lien App";
            this.menuItem59.Click += new System.EventHandler(this.menuMAJLienApp_Click);
            // 
            // menuItem67
            // 
            this.menuItem67.Index = 4;
            this.menuItem67.Text = "Mise à jour Lien Tech";
            this.menuItem67.Click += new System.EventHandler(this.menuMAJLienTech_Click);
            // 
            // menuItem26
            // 
            this.menuItem26.Index = 5;
            this.menuItem26.Text = "";
            // 
            // menuItem34
            // 
            this.menuItem34.Index = 3;
            this.menuItem34.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem28,
            this.menuItem32,
            this.menuItem45,
            this.menuItem56,
            this.menuItem61});
            this.menuItem34.Text = "Import/Export";
            // 
            // menuItem28
            // 
            this.menuItem28.Index = 0;
            this.menuItem28.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem29,
            this.menuItem30,
            this.menuItem58});
            this.menuItem28.Text = "View";
            // 
            // menuItem29
            // 
            this.menuItem29.Index = 0;
            this.menuItem29.Text = "Export View";
            this.menuItem29.Click += new System.EventHandler(this.menuExport_Click);
            // 
            // menuItem30
            // 
            this.menuItem30.Index = 1;
            this.menuItem30.Text = "Import View";
            this.menuItem30.Click += new System.EventHandler(this.menuImport_Click);
            // 
            // menuItem58
            // 
            this.menuItem58.Index = 2;
            this.menuItem58.Text = "Copy Field";
            this.menuItem58.Click += new System.EventHandler(this.menuCopyField_Click);
            // 
            // menuItem32
            // 
            this.menuItem32.Index = 1;
            this.menuItem32.Text = "Export IP/Nom/App";
            this.menuItem32.Click += new System.EventHandler(this.menuExtractIP_Click);
            // 
            // menuItem45
            // 
            this.menuItem45.Index = 2;
            this.menuItem45.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem46,
            this.menuItem47});
            this.menuItem45.Text = "Server/IP";
            // 
            // menuItem46
            // 
            this.menuItem46.Index = 0;
            this.menuItem46.Text = "Export Server/IP";
            this.menuItem46.Click += new System.EventHandler(this.menuExportServer_Click);
            // 
            // menuItem47
            // 
            this.menuItem47.Index = 1;
            this.menuItem47.Text = "Import Server/IP";
            // 
            // menuItem56
            // 
            this.menuItem56.Index = 3;
            this.menuItem56.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem57,
            this.menuItem35,
            this.menuItem55});
            this.menuItem56.Text = "Global Export";
            // 
            // menuItem57
            // 
            this.menuItem57.Index = 0;
            this.menuItem57.Text = "Etat DB";
            this.menuItem57.Click += new System.EventHandler(this.menuEtatDB_Click);
            // 
            // menuItem35
            // 
            this.menuItem35.Index = 1;
            this.menuItem35.Text = "Export Ref Data";
            this.menuItem35.Click += new System.EventHandler(this.menuExportRefData_Click);
            // 
            // menuItem55
            // 
            this.menuItem55.Index = 2;
            this.menuItem55.Text = "Export DB";
            this.menuItem55.Click += new System.EventHandler(this.menuExportDB_Click);
            // 
            // menuItem61
            // 
            this.menuItem61.Index = 4;
            this.menuItem61.Text = "ImportObj";
            this.menuItem61.Click += new System.EventHandler(this.menuImportObj_Click);
            // 
            // menuItem40
            // 
            this.menuItem40.Index = 4;
            this.menuItem40.Text = "Visualisation xml";
            this.menuItem40.Click += new System.EventHandler(this.menuItem40_Click);
            // 
            // menuItem53
            // 
            this.menuItem53.Index = 5;
            this.menuItem53.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem54});
            this.menuItem53.Text = "Traitements";
            // 
            // menuItem54
            // 
            this.menuItem54.Index = 0;
            this.menuItem54.Text = "Mise à jour Libellés";
            this.menuItem54.Click += new System.EventHandler(this.menuMiseAJourLibelles_Click);
            // 
            // menuItem63
            // 
            this.menuItem63.Index = 6;
            this.menuItem63.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem64,
            this.menuItem65,
            this.menuItem66});
            this.menuItem63.Text = "Discovery";
            // 
            // menuItem64
            // 
            this.menuItem64.Index = 0;
            this.menuItem64.Text = "Reseaux";
            this.menuItem64.Click += new System.EventHandler(this.DiscoveryTechnologies_Click);
            // 
            // menuItem65
            // 
            this.menuItem65.Index = 1;
            this.menuItem65.Text = "Serveurs";
            this.menuItem65.Click += new System.EventHandler(this.DiscoveryServeurs_Click);
            // 
            // menuItem66
            // 
            this.menuItem66.Index = 2;
            this.menuItem66.Text = "Technologies";
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 5;
            this.menuItem10.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuHelpAbout});
            this.menuItem10.Text = "Help";
            // 
            // menuHelpAbout
            // 
            this.menuHelpAbout.Index = 0;
            this.menuHelpAbout.Text = "About";
            this.menuHelpAbout.Click += new System.EventHandler(this.menuHelpAbout_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Silver;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.imageList1.Images.SetKeyName(7, "");
            this.imageList1.Images.SetKeyName(8, "");
            this.imageList1.Images.SetKeyName(9, "link.bmp");
            this.imageList1.Images.SetKeyName(10, "Separator.bmp");
            this.imageList1.Images.SetKeyName(11, "Server.bmp");
            this.imageList1.Images.SetKeyName(12, "Module.bmp");
            this.imageList1.Images.SetKeyName(13, "Vlan.bmp");
            this.imageList1.Images.SetKeyName(14, "Base.bmp");
            this.imageList1.Images.SetKeyName(15, "Card.bmp");
            this.imageList1.Images.SetKeyName(16, "Fichier.bmp");
            this.imageList1.Images.SetKeyName(17, "Techno.bmp");
            this.imageList1.Images.SetKeyName(18, "Composant.bmp");
            this.imageList1.Images.SetKeyName(19, "Router.bmp");
            this.imageList1.Images.SetKeyName(20, "user.bmp");
            this.imageList1.Images.SetKeyName(21, "Application.bmp");
            this.imageList1.Images.SetKeyName(22, "Interface.bmp");
            this.imageList1.Images.SetKeyName(23, "EACB.bmp");
            this.imageList1.Images.SetKeyName(24, "Zone.bmp");
            this.imageList1.Images.SetKeyName(25, "Baie.bmp");
            this.imageList1.Images.SetKeyName(26, "Cluster.bmp");
            this.imageList1.Images.SetKeyName(27, "Lun.bmp");
            this.imageList1.Images.SetKeyName(28, "Machine.bmp");
            this.imageList1.Images.SetKeyName(29, "Drawer.bmp");
            this.imageList1.Images.SetKeyName(30, "pass.bmp");
            this.imageList1.Images.SetKeyName(31, "ptCnx.bmp");
            this.imageList1.Images.SetKeyName(32, "flux.bmp");
            this.imageList1.Images.SetKeyName(33, "Queue.bmp");
            this.imageList1.Images.SetKeyName(34, "visio.png");
            this.imageList1.Images.SetKeyName(35, "infKUBE.png");
            this.imageList1.Images.SetKeyName(36, "infPOD.png");
            this.imageList1.Images.SetKeyName(37, "infING.png");
            this.imageList1.Images.SetKeyName(38, "infSVC.png");
            this.imageList1.Images.SetKeyName(39, "save.png");
            // 
            // toolBar1
            // 
            this.toolBar1.AutoSize = false;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbNew,
            this.tbOpen,
            this.tbSave,
            this.tbseparator1,
            this.tbPointer,
            this.tbRectangle,
            this.tbEllipse,
            this.tbLine,
            this.tbPolygon,
            this.tbModule,
            this.tbLink,
            this.tbUser,
            this.tbApplication,
            this.tbMainComposant,
            this.tbInterface,
            this.tbComposant,
            this.tbBase,
            this.tbQueue,
            this.tbFile,
            this.tbLinkA,
            this.tbFluxBoutEnBoutFonc,
            this.tbServer,
            this.tbGenks,
            this.tbGenpod,
            this.tbGensas,
            this.tbManagedsvc,
            this.tbContainer,
            this.tbGening,
            this.tbGensvc,
            this.tbPattern,
            this.tbPatternIns,
            this.tbInsks,
            this.tbTechno,
            this.tbLinkI,
            this.tbFluxBoutEnBout,
            this.tbEACB,
            this.tbStatut,
            this.tbSite,
            this.tbPtCnx,
            this.tbCnx,
            this.tbServeurE,
            this.tbInsnd,
            this.tbInssas,
            this.tbcard,
            this.tbNatRule,
            this.tbVlan,
            this.tbRouter,
            this.tbFluxApp,
            this.tbFlux,
            this.tbCompFonc,
            this.tbServMComp,
            this.tbCluster,
            this.tbMachine,
            this.tbVirtuel,
            this.tbBaie1,
            this.tbLun,
            this.tbZone,
            this.tbSanCard,
            this.tbSanSwitch,
            this.tbBaieCTI,
            this.tbISL,
            this.tbBaiePhy,
            this.tbBaieDPhy,
            this.tbMachineCTI,
            this.tbInterLink,
            this.tbDrawer,
            this.tbseparator8,
            this.tbAbout,
            this.tbServerSite,
            this.tbTechUser,
            this.tbCadreRefN,
            this.tbCadreRefN1,
            this.tbIndicator,
            this.tbPatrimoine,
            this.tbVisio,
            this.tbSi,
            this.tbAxes,
            this.tbReport,
            this.tbExportXLSReport});
            this.toolBar1.ButtonSize = new System.Drawing.Size(20, 20);
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(918, 44);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // tbNew
            // 
            this.tbNew.Name = "tbNew";
            this.tbNew.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbNew.ToolTipText = "New";
            this.tbNew.Visible = false;
            // 
            // tbOpen
            // 
            this.tbOpen.ImageIndex = 1;
            this.tbOpen.Name = "tbOpen";
            this.tbOpen.ToolTipText = "Open";
            this.tbOpen.Visible = false;
            // 
            // tbSave
            // 
            this.tbSave.ImageIndex = 2;
            this.tbSave.Name = "tbSave";
            this.tbSave.ToolTipText = "Save";
            this.tbSave.Visible = false;
            // 
            // tbseparator1
            // 
            this.tbseparator1.Enabled = false;
            this.tbseparator1.ImageIndex = 10;
            this.tbseparator1.Name = "tbseparator1";
            this.tbseparator1.Visible = false;
            // 
            // tbPointer
            // 
            this.tbPointer.ImageIndex = 4;
            this.tbPointer.Name = "tbPointer";
            this.tbPointer.Pushed = true;
            this.tbPointer.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbPointer.ToolTipText = "Pointer";
            // 
            // tbRectangle
            // 
            this.tbRectangle.ImageIndex = 5;
            this.tbRectangle.Name = "tbRectangle";
            this.tbRectangle.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbRectangle.ToolTipText = "Recatngle";
            this.tbRectangle.Visible = false;
            // 
            // tbEllipse
            // 
            this.tbEllipse.ImageIndex = 6;
            this.tbEllipse.Name = "tbEllipse";
            this.tbEllipse.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbEllipse.ToolTipText = "Ellipse";
            this.tbEllipse.Visible = false;
            // 
            // tbLine
            // 
            this.tbLine.ImageIndex = 7;
            this.tbLine.Name = "tbLine";
            this.tbLine.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbLine.ToolTipText = "Line";
            this.tbLine.Visible = false;
            // 
            // tbPolygon
            // 
            this.tbPolygon.ImageIndex = 8;
            this.tbPolygon.Name = "tbPolygon";
            this.tbPolygon.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbPolygon.ToolTipText = "Pencil";
            this.tbPolygon.Visible = false;
            // 
            // tbModule
            // 
            this.tbModule.ImageIndex = 12;
            this.tbModule.Name = "tbModule";
            this.tbModule.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbModule.Tag = "bedef478-547e-47de-819b-4377a94e78c5";
            this.tbModule.ToolTipText = "Fonction";
            this.tbModule.Visible = false;
            // 
            // tbLink
            // 
            this.tbLink.ImageIndex = 9;
            this.tbLink.Name = "tbLink";
            this.tbLink.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbLink.Tag = "37a0ca71-25a8-46e8-a605-aa14387c5b7c";
            this.tbLink.ToolTipText = "Lien";
            this.tbLink.Visible = false;
            // 
            // tbUser
            // 
            this.tbUser.ImageIndex = 20;
            this.tbUser.Name = "tbUser";
            this.tbUser.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbUser.Tag = "46883717-5f55-4789-a833-dc10f59385b8";
            this.tbUser.ToolTipText = "Utilisateur";
            this.tbUser.Visible = false;
            // 
            // tbApplication
            // 
            this.tbApplication.ImageIndex = 21;
            this.tbApplication.Name = "tbApplication";
            this.tbApplication.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbApplication.ToolTipText = "Application Externe";
            this.tbApplication.Visible = false;
            // 
            // tbMainComposant
            // 
            this.tbMainComposant.ImageIndex = 5;
            this.tbMainComposant.Name = "tbMainComposant";
            this.tbMainComposant.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbMainComposant.ToolTipText = "Container";
            this.tbMainComposant.Visible = false;
            // 
            // tbInterface
            // 
            this.tbInterface.ImageIndex = 22;
            this.tbInterface.Name = "tbInterface";
            this.tbInterface.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbInterface.ToolTipText = "Interface";
            this.tbInterface.Visible = false;
            // 
            // tbComposant
            // 
            this.tbComposant.ImageIndex = 18;
            this.tbComposant.Name = "tbComposant";
            this.tbComposant.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbComposant.ToolTipText = "Composant Applicatif";
            this.tbComposant.Visible = false;
            // 
            // tbBase
            // 
            this.tbBase.ImageIndex = 14;
            this.tbBase.Name = "tbBase";
            this.tbBase.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbBase.ToolTipText = "Base de données";
            this.tbBase.Visible = false;
            // 
            // tbQueue
            // 
            this.tbQueue.ImageIndex = 33;
            this.tbQueue.Name = "tbQueue";
            this.tbQueue.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbQueue.ToolTipText = "File";
            this.tbQueue.Visible = false;
            // 
            // tbFile
            // 
            this.tbFile.ImageIndex = 16;
            this.tbFile.Name = "tbFile";
            this.tbFile.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbFile.ToolTipText = "Fichier";
            this.tbFile.Visible = false;
            // 
            // tbLinkA
            // 
            this.tbLinkA.ImageIndex = 9;
            this.tbLinkA.Name = "tbLinkA";
            this.tbLinkA.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbLinkA.ToolTipText = "Lien Applicatif";
            this.tbLinkA.Visible = false;
            // 
            // tbFluxBoutEnBoutFonc
            // 
            this.tbFluxBoutEnBoutFonc.ImageIndex = 32;
            this.tbFluxBoutEnBoutFonc.Name = "tbFluxBoutEnBoutFonc";
            this.tbFluxBoutEnBoutFonc.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbFluxBoutEnBoutFonc.Visible = false;
            // 
            // tbServer
            // 
            this.tbServer.ImageIndex = 11;
            this.tbServer.Name = "tbServer";
            this.tbServer.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbServer.ToolTipText = "Serveur d\'Infrastructure";
            this.tbServer.Visible = false;
            // 
            // tbGenks
            // 
            this.tbGenks.ImageIndex = 35;
            this.tbGenks.Name = "tbGenks";
            this.tbGenks.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbGenks.ToolTipText = "Kube";
            this.tbGenks.Visible = false;
            // 
            // tbGenpod
            // 
            this.tbGenpod.ImageIndex = 36;
            this.tbGenpod.Name = "tbGenpod";
            this.tbGenpod.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbGenpod.ToolTipText = "pod";
            this.tbGenpod.Visible = false;
            // 
            // tbGensas
            // 
            this.tbGensas.ImageIndex = 29;
            this.tbGensas.Name = "tbGensas";
            this.tbGensas.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbGensas.ToolTipText = "Services managés";
            this.tbGensas.Visible = false;
            // 
            // tbManagedsvc
            // 
            this.tbManagedsvc.Name = "tbManagedsvc";
            this.tbManagedsvc.Visible = false;
            // 
            // tbContainer
            // 
            this.tbContainer.ImageIndex = 16;
            this.tbContainer.Name = "tbContainer";
            this.tbContainer.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbContainer.ToolTipText = "Container";
            this.tbContainer.Visible = false;
            // 
            // tbGening
            // 
            this.tbGening.ImageIndex = 37;
            this.tbGening.Name = "tbGening";
            this.tbGening.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbGening.ToolTipText = "Ing";
            this.tbGening.Visible = false;
            // 
            // tbGensvc
            // 
            this.tbGensvc.ImageIndex = 38;
            this.tbGensvc.Name = "tbGensvc";
            this.tbGensvc.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbGensvc.ToolTipText = "service";
            this.tbGensvc.Visible = false;
            // 
            // tbPattern
            // 
            this.tbPattern.ImageIndex = 24;
            this.tbPattern.Name = "tbPattern";
            // 
            // tbPatternIns
            // 
            this.tbPatternIns.Enabled = false;
            this.tbPatternIns.Name = "tbPatternIns";
            // 
            // tbInsks
            // 
            this.tbInsks.ImageIndex = 30;
            this.tbInsks.Name = "tbInsks";
            this.tbInsks.Visible = false;
            // 
            // tbTechno
            // 
            this.tbTechno.ImageIndex = 17;
            this.tbTechno.Name = "tbTechno";
            this.tbTechno.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbTechno.ToolTipText = "Technologie d\'infrastructure";
            this.tbTechno.Visible = false;
            // 
            // tbLinkI
            // 
            this.tbLinkI.ImageIndex = 9;
            this.tbLinkI.Name = "tbLinkI";
            this.tbLinkI.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbLinkI.ToolTipText = "Lien Infrastructure";
            this.tbLinkI.Visible = false;
            // 
            // tbFluxBoutEnBout
            // 
            this.tbFluxBoutEnBout.ImageIndex = 32;
            this.tbFluxBoutEnBout.Name = "tbFluxBoutEnBout";
            this.tbFluxBoutEnBout.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbFluxBoutEnBout.ToolTipText = "Aide flux bout en bout";
            this.tbFluxBoutEnBout.Visible = false;
            // 
            // tbEACB
            // 
            this.tbEACB.ImageIndex = 23;
            this.tbEACB.Name = "tbEACB";
            this.tbEACB.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbEACB.ToolTipText = "Creation EACB";
            this.tbEACB.Visible = false;
            // 
            // tbStatut
            // 
            this.tbStatut.ImageIndex = 30;
            this.tbStatut.Name = "tbStatut";
            this.tbStatut.ToolTipText = "Statut";
            this.tbStatut.Visible = false;
            // 
            // tbSite
            // 
            this.tbSite.ImageIndex = 26;
            this.tbSite.Name = "tbSite";
            this.tbSite.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbSite.ToolTipText = "Site";
            this.tbSite.Visible = false;
            // 
            // tbPtCnx
            // 
            this.tbPtCnx.ImageIndex = 31;
            this.tbPtCnx.Name = "tbPtCnx";
            this.tbPtCnx.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbPtCnx.ToolTipText = "Point de connexion";
            this.tbPtCnx.Visible = false;
            // 
            // tbCnx
            // 
            this.tbCnx.ImageIndex = 24;
            this.tbCnx.Name = "tbCnx";
            this.tbCnx.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbCnx.ToolTipText = "Connexion";
            this.tbCnx.Visible = false;
            // 
            // tbServeurE
            // 
            this.tbServeurE.ImageIndex = 11;
            this.tbServeurE.Name = "tbServeurE";
            this.tbServeurE.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbServeurE.ToolTipText = "Serveur d\'un environnement";
            this.tbServeurE.Visible = false;
            // 
            // tbInsnd
            // 
            this.tbInsnd.ImageIndex = 36;
            this.tbInsnd.Name = "tbInsnd";
            this.tbInsnd.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbInsnd.ToolTipText = "Cluster Node d\'un Kube";
            this.tbInsnd.Visible = false;
            // 
            // tbInssas
            // 
            this.tbInssas.ImageIndex = 29;
            this.tbInssas.Name = "tbInssas";
            this.tbInssas.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbInssas.ToolTipText = "Instance Service Manage";
            this.tbInssas.Visible = false;
            // 
            // tbcard
            // 
            this.tbcard.ImageIndex = 15;
            this.tbcard.Name = "tbcard";
            this.tbcard.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbcard.ToolTipText = "Network Card";
            this.tbcard.Visible = false;
            // 
            // tbNatRule
            // 
            this.tbNatRule.ImageIndex = 34;
            this.tbNatRule.Name = "tbNatRule";
            this.tbNatRule.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbNatRule.Visible = false;
            // 
            // tbVlan
            // 
            this.tbVlan.ImageIndex = 13;
            this.tbVlan.Name = "tbVlan";
            this.tbVlan.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbVlan.ToolTipText = "Vlan";
            this.tbVlan.Visible = false;
            // 
            // tbRouter
            // 
            this.tbRouter.ImageIndex = 19;
            this.tbRouter.Name = "tbRouter";
            this.tbRouter.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbRouter.ToolTipText = "Router / FW";
            this.tbRouter.Visible = false;
            // 
            // tbFluxApp
            // 
            this.tbFluxApp.ImageIndex = 23;
            this.tbFluxApp.Name = "tbFluxApp";
            this.tbFluxApp.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbFluxApp.ToolTipText = "Aide flux App / flux Tech";
            this.tbFluxApp.Visible = false;
            // 
            // tbFlux
            // 
            this.tbFlux.ImageIndex = 23;
            this.tbFlux.Name = "tbFlux";
            this.tbFlux.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbFlux.ToolTipText = "Aide flux d\'infrastructure";
            this.tbFlux.Visible = false;
            // 
            // tbCompFonc
            // 
            this.tbCompFonc.Name = "tbCompFonc";
            this.tbCompFonc.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbCompFonc.ToolTipText = "Fonction";
            this.tbCompFonc.Visible = false;
            // 
            // tbServMComp
            // 
            this.tbServMComp.Name = "tbServMComp";
            this.tbServMComp.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbServMComp.Visible = false;
            // 
            // tbCluster
            // 
            this.tbCluster.ImageIndex = 26;
            this.tbCluster.Name = "tbCluster";
            this.tbCluster.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbCluster.ToolTipText = "Cluster";
            this.tbCluster.Visible = false;
            // 
            // tbMachine
            // 
            this.tbMachine.ImageIndex = 28;
            this.tbMachine.Name = "tbMachine";
            this.tbMachine.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbMachine.ToolTipText = "Serveur Physique";
            this.tbMachine.Visible = false;
            // 
            // tbVirtuel
            // 
            this.tbVirtuel.Name = "tbVirtuel";
            this.tbVirtuel.ToolTipText = "Serveur Virtuel";
            this.tbVirtuel.Visible = false;
            // 
            // tbBaie1
            // 
            this.tbBaie1.ImageIndex = 25;
            this.tbBaie1.Name = "tbBaie1";
            this.tbBaie1.ToolTipText = "Baie";
            this.tbBaie1.Visible = false;
            // 
            // tbLun
            // 
            this.tbLun.ImageIndex = 27;
            this.tbLun.Name = "tbLun";
            this.tbLun.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbLun.ToolTipText = "Lun";
            this.tbLun.Visible = false;
            // 
            // tbZone
            // 
            this.tbZone.ImageIndex = 24;
            this.tbZone.Name = "tbZone";
            this.tbZone.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbZone.ToolTipText = "Zone";
            this.tbZone.Visible = false;
            // 
            // tbSanCard
            // 
            this.tbSanCard.ImageIndex = 15;
            this.tbSanCard.Name = "tbSanCard";
            this.tbSanCard.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbSanCard.ToolTipText = "San Card";
            this.tbSanCard.Visible = false;
            // 
            // tbSanSwitch
            // 
            this.tbSanSwitch.ImageIndex = 13;
            this.tbSanSwitch.Name = "tbSanSwitch";
            this.tbSanSwitch.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbSanSwitch.ToolTipText = "San switch";
            this.tbSanSwitch.Visible = false;
            // 
            // tbBaieCTI
            // 
            this.tbBaieCTI.Name = "tbBaieCTI";
            this.tbBaieCTI.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbBaieCTI.ToolTipText = "Baie";
            this.tbBaieCTI.Visible = false;
            // 
            // tbISL
            // 
            this.tbISL.ImageIndex = 9;
            this.tbISL.Name = "tbISL";
            this.tbISL.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbISL.ToolTipText = "ISL";
            this.tbISL.Visible = false;
            // 
            // tbBaiePhy
            // 
            this.tbBaiePhy.ImageIndex = 25;
            this.tbBaiePhy.Name = "tbBaiePhy";
            this.tbBaiePhy.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbBaiePhy.ToolTipText = "Baie Physique";
            this.tbBaiePhy.Visible = false;
            // 
            // tbBaieDPhy
            // 
            this.tbBaieDPhy.Name = "tbBaieDPhy";
            this.tbBaieDPhy.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbBaieDPhy.ToolTipText = "Baie Physique";
            this.tbBaieDPhy.Visible = false;
            // 
            // tbMachineCTI
            // 
            this.tbMachineCTI.Name = "tbMachineCTI";
            this.tbMachineCTI.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbMachineCTI.ToolTipText = "Serveur Physique";
            this.tbMachineCTI.Visible = false;
            // 
            // tbInterLink
            // 
            this.tbInterLink.ImageIndex = 9;
            this.tbInterLink.Name = "tbInterLink";
            this.tbInterLink.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbInterLink.ToolTipText = "Lien Inter-Site";
            this.tbInterLink.Visible = false;
            // 
            // tbDrawer
            // 
            this.tbDrawer.ImageIndex = 29;
            this.tbDrawer.Name = "tbDrawer";
            this.tbDrawer.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbDrawer.ToolTipText = "Drawer";
            this.tbDrawer.Visible = false;
            // 
            // tbseparator8
            // 
            this.tbseparator8.Enabled = false;
            this.tbseparator8.ImageIndex = 10;
            this.tbseparator8.Name = "tbseparator8";
            // 
            // tbAbout
            // 
            this.tbAbout.ImageIndex = 3;
            this.tbAbout.Name = "tbAbout";
            this.tbAbout.ToolTipText = "About ...";
            // 
            // tbServerSite
            // 
            this.tbServerSite.Name = "tbServerSite";
            this.tbServerSite.ToolTipText = "Serveur";
            this.tbServerSite.Visible = false;
            // 
            // tbTechUser
            // 
            this.tbTechUser.Name = "tbTechUser";
            this.tbTechUser.ToolTipText = "Utilisateur";
            this.tbTechUser.Visible = false;
            // 
            // tbCadreRefN
            // 
            this.tbCadreRefN.Name = "tbCadreRefN";
            this.tbCadreRefN.ToolTipText = "Cadre de Reference";
            this.tbCadreRefN.Visible = false;
            // 
            // tbCadreRefN1
            // 
            this.tbCadreRefN1.Name = "tbCadreRefN1";
            this.tbCadreRefN1.ToolTipText = "Cadre de Reference";
            this.tbCadreRefN1.Visible = false;
            // 
            // tbIndicator
            // 
            this.tbIndicator.Name = "tbIndicator";
            this.tbIndicator.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbIndicator.ToolTipText = "Indicateur";
            this.tbIndicator.Visible = false;
            // 
            // tbPatrimoine
            // 
            this.tbPatrimoine.ImageIndex = 23;
            this.tbPatrimoine.Name = "tbPatrimoine";
            this.tbPatrimoine.ToolTipText = "Rapport patrimoine";
            this.tbPatrimoine.Visible = false;           
            // 
            // tbSi
            // 
            this.tbSi.ImageIndex = 23;
            this.tbSi.Name = "tbSi";
            this.tbSi.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbSi.ToolTipText = "Generate SI";
            this.tbSi.Visible = false;
            // 
            // tbAxes
            // 
            this.tbAxes.ImageIndex = 4;
            this.tbAxes.Name = "tbAxes";
            this.tbAxes.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbAxes.ToolTipText = "Axe";
            this.tbAxes.Visible = false;
            // 
            // tbReport
            // 
            this.tbReport.ImageIndex = 23;
            this.tbReport.Name = "tbReport";
            this.tbReport.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbReport.ToolTipText = "Report";
            this.tbReport.Visible = false;
            // 
            // tbExportXLSReport
            // 
            this.tbExportXLSReport.ImageIndex = 39;
            this.tbExportXLSReport.Name = "tbExportXLSReport";
            this.tbExportXLSReport.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbExportXLSReport.ToolTipText = "Export csv";
            this.tbExportXLSReport.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(918, 374);
            this.splitContainer1.SplitterDistance = 35;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.AutoScroll = true;
            this.splitContainer2.Panel2.Controls.Add(this.drawArea);
            this.splitContainer2.Size = new System.Drawing.Size(918, 335);
            this.splitContainer2.SplitterDistance = 312;
            this.splitContainer2.TabIndex = 2;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.bDescriptionVue);
            this.splitContainer3.Panel1.Controls.Add(this.tbFind);
            this.splitContainer3.Panel1.Controls.Add(this.label3);
            this.splitContainer3.Panel1.Controls.Add(this.tbTypeVue);
            this.splitContainer3.Panel1.Controls.Add(this.bDescriptionApp);
            this.splitContainer3.Panel1.Controls.Add(this.Type);
            this.splitContainer3.Panel1.Controls.Add(this.Ver);
            this.splitContainer3.Panel1.Controls.Add(this.cbVersion);
            this.splitContainer3.Panel1.Controls.Add(this.bLayer);
            this.splitContainer3.Panel1.Controls.Add(this.tbVueInf);
            this.splitContainer3.Panel1.Controls.Add(this.tbEnv);
            this.splitContainer3.Panel1.Controls.Add(this.bOpVue);
            this.splitContainer3.Panel1.Controls.Add(this.cbOpVue);
            this.splitContainer3.Panel1.Controls.Add(this.bOpApp);
            this.splitContainer3.Panel1.Controls.Add(this.cbOpApp);
            this.splitContainer3.Panel1.Controls.Add(this.tbGuid);
            this.splitContainer3.Panel1.Controls.Add(this.cbGuidApplication);
            this.splitContainer3.Panel1.Controls.Add(this.label2);
            this.splitContainer3.Panel1.Controls.Add(this.label1);
            this.splitContainer3.Panel1.Controls.Add(this.cbGuidVue);
            this.splitContainer3.Panel1.Controls.Add(this.tvObjet);
            this.splitContainer3.Panel1.Controls.Add(this.bAdd);
            this.splitContainer3.Panel1.Controls.Add(this.NomApplication);
            this.splitContainer3.Panel1.Controls.Add(this.cbApplication);
            this.splitContainer3.Panel1.Controls.Add(this.bSave);
            this.splitContainer3.Panel1.Controls.Add(this.Vue);
            this.splitContainer3.Panel1.Controls.Add(this.cbVue);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.dataGrid);
            this.splitContainer3.Size = new System.Drawing.Size(308, 331);
            this.splitContainer3.SplitterDistance = 298;
            this.splitContainer3.TabIndex = 0;
            // 
            // bDescriptionVue
            // 
            this.bDescriptionVue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bDescriptionVue.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bDescriptionVue.Location = new System.Drawing.Point(262, 124);
            this.bDescriptionVue.Margin = new System.Windows.Forms.Padding(0);
            this.bDescriptionVue.Name = "bDescriptionVue";
            this.bDescriptionVue.Size = new System.Drawing.Size(39, 32);
            this.bDescriptionVue.TabIndex = 35;
            this.bDescriptionVue.Text = "...";
            this.bDescriptionVue.UseVisualStyleBackColor = true;
            this.bDescriptionVue.Click += new System.EventHandler(this.bDescriptionVue_Click);
            // 
            // tbFind
            // 
            this.tbFind.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFind.Location = new System.Drawing.Point(49, 204);
            this.tbFind.Name = "tbFind";
            this.tbFind.Size = new System.Drawing.Size(255, 26);
            this.tbFind.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 20);
            this.label3.TabIndex = 34;
            this.label3.Text = "Find";
            // 
            // tbTypeVue
            // 
            this.tbTypeVue.Location = new System.Drawing.Point(49, 165);
            this.tbTypeVue.Name = "tbTypeVue";
            this.tbTypeVue.ReadOnly = true;
            this.tbTypeVue.Size = new System.Drawing.Size(166, 26);
            this.tbTypeVue.TabIndex = 29;
            // 
            // bDescriptionApp
            // 
            this.bDescriptionApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bDescriptionApp.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bDescriptionApp.Location = new System.Drawing.Point(262, 46);
            this.bDescriptionApp.Margin = new System.Windows.Forms.Padding(0);
            this.bDescriptionApp.Name = "bDescriptionApp";
            this.bDescriptionApp.Size = new System.Drawing.Size(39, 31);
            this.bDescriptionApp.TabIndex = 33;
            this.bDescriptionApp.Text = "...";
            this.bDescriptionApp.UseVisualStyleBackColor = true;
            this.bDescriptionApp.Click += new System.EventHandler(this.bDescriptionApp_Click);
            // 
            // Type
            // 
            this.Type.AutoSize = true;
            this.Type.Location = new System.Drawing.Point(1, 170);
            this.Type.Name = "Type";
            this.Type.Size = new System.Drawing.Size(43, 20);
            this.Type.TabIndex = 4;
            this.Type.Text = "Type";
            // 
            // Ver
            // 
            this.Ver.AutoSize = true;
            this.Ver.Location = new System.Drawing.Point(1, 91);
            this.Ver.Name = "Ver";
            this.Ver.Size = new System.Drawing.Size(34, 20);
            this.Ver.TabIndex = 32;
            this.Ver.Text = "Ver";
            // 
            // cbVersion
            // 
            this.cbVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVersion.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbVersion.FormattingEnabled = true;
            this.cbVersion.Location = new System.Drawing.Point(49, 86);
            this.cbVersion.Name = "cbVersion";
            this.cbVersion.Size = new System.Drawing.Size(136, 28);
            this.cbVersion.TabIndex = 31;
            this.cbVersion.SelectedIndexChanged += new System.EventHandler(this.cbVersion_SelectedIndexChanged);
            // 
            // bLayer
            // 
            this.bLayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bLayer.Location = new System.Drawing.Point(185, 85);
            this.bLayer.Margin = new System.Windows.Forms.Padding(0);
            this.bLayer.Name = "bLayer";
            this.bLayer.Size = new System.Drawing.Size(32, 32);
            this.bLayer.TabIndex = 30;
            this.bLayer.Text = "+";
            this.bLayer.UseVisualStyleBackColor = true;
            this.bLayer.Click += new System.EventHandler(this.bLayer_Click);
            // 
            // tbVueInf
            // 
            this.tbVueInf.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbVueInf.Location = new System.Drawing.Point(277, 85);
            this.tbVueInf.Name = "tbVueInf";
            this.tbVueInf.ReadOnly = true;
            this.tbVueInf.Size = new System.Drawing.Size(24, 26);
            this.tbVueInf.TabIndex = 28;
            // 
            // tbEnv
            // 
            this.tbEnv.Location = new System.Drawing.Point(275, 165);
            this.tbEnv.Name = "tbEnv";
            this.tbEnv.ReadOnly = true;
            this.tbEnv.Size = new System.Drawing.Size(166, 26);
            this.tbEnv.TabIndex = 27;
            // 
            // bOpVue
            // 
            this.bOpVue.Enabled = false;
            this.bOpVue.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bOpVue.Location = new System.Drawing.Point(129, 6);
            this.bOpVue.Margin = new System.Windows.Forms.Padding(0);
            this.bOpVue.Name = "bOpVue";
            this.bOpVue.Size = new System.Drawing.Size(88, 32);
            this.bOpVue.TabIndex = 23;
            this.bOpVue.Tag = "5fa9fa6e-2caa-42dc-975f-12f0aca7075d";
            this.bOpVue.Text = "Creat Vue";
            this.bOpVue.UseVisualStyleBackColor = true;
            this.bOpVue.Click += new System.EventHandler(this.bOpVue_Click);
            // 
            // cbOpVue
            // 
            this.cbOpVue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOpVue.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbOpVue.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOpVue.FormattingEnabled = true;
            this.cbOpVue.Items.AddRange(new object[] {
            "Creat Vue",
            "Updt Vue",
            "Copy Vue",
            "Del Vue"});
            this.cbOpVue.Location = new System.Drawing.Point(129, 8);
            this.cbOpVue.Margin = new System.Windows.Forms.Padding(0);
            this.cbOpVue.Name = "cbOpVue";
            this.cbOpVue.Size = new System.Drawing.Size(111, 24);
            this.cbOpVue.TabIndex = 26;
            this.cbOpVue.Tag = "6db2c391-c05e-4cde-aec3-d7ed7a773de7";
            this.cbOpVue.SelectedIndexChanged += new System.EventHandler(this.cbOpVue_SelectedIndexChanged);
            // 
            // bOpApp
            // 
            this.bOpApp.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bOpApp.Location = new System.Drawing.Point(1, 8);
            this.bOpApp.Margin = new System.Windows.Forms.Padding(0);
            this.bOpApp.Name = "bOpApp";
            this.bOpApp.Size = new System.Drawing.Size(88, 31);
            this.bOpApp.TabIndex = 24;
            this.bOpApp.Tag = "05915b4d-6eaf-4ed1-8c07-c31a4527a2b2";
            this.bOpApp.Text = "Creat App";
            this.bOpApp.UseVisualStyleBackColor = true;
            this.bOpApp.Click += new System.EventHandler(this.bOpApp_Click);
            // 
            // cbOpApp
            // 
            this.cbOpApp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOpApp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbOpApp.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOpApp.FormattingEnabled = true;
            this.cbOpApp.Items.AddRange(new object[] {
            "Creat App",
            "Updt App",
            "Copy App",
            "Copy Ver",
            "Move Ver"});
            this.cbOpApp.Location = new System.Drawing.Point(3, 9);
            this.cbOpApp.Margin = new System.Windows.Forms.Padding(0);
            this.cbOpApp.Name = "cbOpApp";
            this.cbOpApp.Size = new System.Drawing.Size(110, 24);
            this.cbOpApp.TabIndex = 25;
            this.cbOpApp.Tag = "5416a957-e669-4403-be06-6f7a71f616ce";
            this.cbOpApp.SelectedIndexChanged += new System.EventHandler(this.cbOpApp_SelectedIndexChanged);
            // 
            // tbGuid
            // 
            this.tbGuid.Location = new System.Drawing.Point(89, 571);
            this.tbGuid.Name = "tbGuid";
            this.tbGuid.ReadOnly = true;
            this.tbGuid.Size = new System.Drawing.Size(306, 26);
            this.tbGuid.TabIndex = 7;
            this.tbGuid.Text = "Guid";
            this.tbGuid.Visible = false;
            // 
            // cbGuidApplication
            // 
            this.cbGuidApplication.FormattingEnabled = true;
            this.cbGuidApplication.Location = new System.Drawing.Point(160, 35);
            this.cbGuidApplication.Name = "cbGuidApplication";
            this.cbGuidApplication.Size = new System.Drawing.Size(105, 28);
            this.cbGuidApplication.TabIndex = 22;
            this.cbGuidApplication.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(232, 171);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 20);
            this.label2.TabIndex = 19;
            this.label2.Text = "Env";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(223, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 20);
            this.label1.TabIndex = 18;
            this.label1.Text = "Vue-1";
            // 
            // cbGuidVue
            // 
            this.cbGuidVue.FormattingEnabled = true;
            this.cbGuidVue.Location = new System.Drawing.Point(161, 141);
            this.cbGuidVue.Name = "cbGuidVue";
            this.cbGuidVue.Size = new System.Drawing.Size(106, 28);
            this.cbGuidVue.TabIndex = 17;
            this.cbGuidVue.Visible = false;
            // 
            // tvObjet
            // 
            this.tvObjet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvObjet.Location = new System.Drawing.Point(9, 241);
            this.tvObjet.Name = "tvObjet";
            this.tvObjet.Size = new System.Drawing.Size(296, 55);
            this.tvObjet.TabIndex = 14;
            this.tvObjet.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvObjet_AfterSelect);
            // 
            // bAdd
            // 
            this.bAdd.Enabled = false;
            this.bAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bAdd.Location = new System.Drawing.Point(355, 6);
            this.bAdd.Margin = new System.Windows.Forms.Padding(0);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(88, 32);
            this.bAdd.TabIndex = 13;
            this.bAdd.Text = "Add Obj";
            this.bAdd.UseVisualStyleBackColor = true;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // NomApplication
            // 
            this.NomApplication.AutoSize = true;
            this.NomApplication.Location = new System.Drawing.Point(3, 51);
            this.NomApplication.Name = "NomApplication";
            this.NomApplication.Size = new System.Drawing.Size(44, 20);
            this.NomApplication.TabIndex = 12;
            this.NomApplication.Text = "Appli";
            this.NomApplication.Click += new System.EventHandler(this.NomApplication_Click);
            // 
            // cbApplication
            // 
            this.cbApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbApplication.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbApplication.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbApplication.FormattingEnabled = true;
            this.cbApplication.Location = new System.Drawing.Point(49, 47);
            this.cbApplication.Name = "cbApplication";
            this.cbApplication.Size = new System.Drawing.Size(209, 28);
            this.cbApplication.TabIndex = 0;
            this.cbApplication.SelectedIndexChanged += new System.EventHandler(this.cbApplication_SelectedIndexChanged);
            this.cbApplication.Validated += new System.EventHandler(this.cbApplication_Validated);
            // 
            // bSave
            // 
            this.bSave.Enabled = false;
            this.bSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bSave.Location = new System.Drawing.Point(253, 6);
            this.bSave.Margin = new System.Windows.Forms.Padding(0);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(88, 32);
            this.bSave.TabIndex = 10;
            this.bSave.Tag = "78fc4a02-cfc6-44ae-a59a-4a75952b2604";
            this.bSave.Text = "Save Vue";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // Vue
            // 
            this.Vue.AutoSize = true;
            this.Vue.Location = new System.Drawing.Point(5, 130);
            this.Vue.Name = "Vue";
            this.Vue.Size = new System.Drawing.Size(38, 20);
            this.Vue.TabIndex = 3;
            this.Vue.Text = "Vue";
            // 
            // cbVue
            // 
            this.cbVue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbVue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVue.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbVue.FormattingEnabled = true;
            this.cbVue.Location = new System.Drawing.Point(49, 125);
            this.cbVue.Name = "cbVue";
            this.cbVue.Size = new System.Drawing.Size(209, 28);
            this.cbVue.TabIndex = 1;
            this.cbVue.SelectedIndexChanged += new System.EventHandler(this.cbVue_SelectedIndexChanged);
            this.cbVue.Validated += new System.EventHandler(this.cbVue_Validated);
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Propriete,
            this.Valeur,
            this.Pls,
            this.NonVisible});
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid.Location = new System.Drawing.Point(0, 0);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.RowHeadersWidth = 62;
            this.dataGrid.Size = new System.Drawing.Size(308, 29);
            this.dataGrid.TabIndex = 0;
            this.dataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellClick);
            this.dataGrid.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellValidated);
            // 
            // Propriete
            // 
            this.Propriete.HeaderText = "Propriete";
            this.Propriete.MinimumWidth = 8;
            this.Propriete.Name = "Propriete";
            this.Propriete.ReadOnly = true;
            this.Propriete.Width = 150;
            // 
            // Valeur
            // 
            this.Valeur.HeaderText = "Valeur";
            this.Valeur.MinimumWidth = 8;
            this.Valeur.Name = "Valeur";
            this.Valeur.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Valeur.Width = 150;
            // 
            // Pls
            // 
            this.Pls.HeaderText = "";
            this.Pls.MinimumWidth = 8;
            this.Pls.Name = "Pls";
            this.Pls.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Pls.Width = 15;
            // 
            // NonVisible
            // 
            this.NonVisible.HeaderText = "NonVisible";
            this.NonVisible.MinimumWidth = 8;
            this.NonVisible.Name = "NonVisible";
            this.NonVisible.ReadOnly = true;
            this.NonVisible.Visible = false;
            this.NonVisible.Width = 150;
            // 
            // drawArea
            // 
            this.drawArea.ActiveTool = DrawTools.DrawArea.DrawToolType.Pointer;
            this.drawArea.AddObjet = false;
            this.drawArea.AutoScroll = true;
            this.drawArea.AutoSize = true;
            this.drawArea.DrawNetRectangle = false;
            this.drawArea.GraphicsList = null;
            this.drawArea.Location = new System.Drawing.Point(19, 1);
            this.drawArea.Name = "drawArea";
            this.drawArea.NetRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.drawArea.OSelected = null;
            this.drawArea.Owner = null;
            this.drawArea.Size = new System.Drawing.Size(6410, 5803);
            this.drawArea.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(918, 374);
            this.Controls.Add(this.toolBar1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "DrawTools";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        private void InitEvent()
        {
            /*
            this.menuFileSave.Click += new System.EventHandler(this.menuFileSave_Click);
            this.menuItem7.Click += new System.EventHandler(this.menuOptions_Click);
            this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
            this.menuEditSelectAll.Click += new System.EventHandler(this.menuEditSelectAll_Click);
            this.menuEditUnselectAll.Click += new System.EventHandler(this.menuEditUnselectAll_Click);
            this.menuEditDelete.Click += new System.EventHandler(this.menuEditDelete_Click);
            this.menuEditDeleteAll.Click += new System.EventHandler(this.menuEditDeleteAll_Click);
            this.menuEditMoveToFront.Click += new System.EventHandler(this.menuEditMoveToFront_Click);
            this.menuEditMoveToBack.Click += new System.EventHandler(this.menuEditMoveToBack_Click);
            this.menuItem33.Click += new System.EventHandler(this.menuEditCopy_Click);
            this.menuItem41.Click += new System.EventHandler(this.TopAlign_Click);
            this.menuItem42.Click += new System.EventHandler(this.BottomAlign_Click);
            this.menuItem43.Click += new System.EventHandler(this.LeftAlign_Click);
            this.menuItem44.Click += new System.EventHandler(this.RightAlign_Click);
            this.menuEditProperties.Click += new System.EventHandler(this.menuEditProperties_Click);
            this.menuItem20.Click += new System.EventHandler(this.menuEditLinkDiagrem_Click);
            this.menuItem23.Click += new System.EventHandler(this.menuEditObjectExplorer_Click);
            this.menuItem27.Click += new System.EventHandler(this.menuEditLinkView_Click);
            this.menuItem48.Click += new System.EventHandler(this.menuEditRules_Click);
            this.menuItem52.Click += new System.EventHandler(this.menuSelectLayer_Click);
            this.menuDrawPointer.Click += new System.EventHandler(this.menuDrawPointer_Click);
            this.menuDrawRectangle.Click += new System.EventHandler(this.menuDrawRectangle_Click);
            this.menuDrawEllipse.Click += new System.EventHandler(this.menuDrawEllipse_Click);
            this.menuDrawLine.Click += new System.EventHandler(this.menuDrawLine_Click);
            this.menuItem51.Click += new System.EventHandler(this.menuCreateLayer_Click);
            this.menuItem37.Click += new System.EventHandler(this.GroupService_Click);
            this.menuItem38.Click += new System.EventHandler(this.Service_Click);
            this.menuItem39.Click += new System.EventHandler(this.ServiceLink_Click);
            this.menuItem13.Click += new System.EventHandler(this.menuRefApp_Click);
            this.menuItem14.Click += new System.EventHandler(this.menuRefTechnique_Click);
            this.menuItem21.Click += new System.EventHandler(this.menuItem21_Click);
            this.menuItem18.Click += new System.EventHandler(this.menuProvisionServer_Click);
            this.menuItem19.Click += new System.EventHandler(this.menuTadUpdating_Click);
            this.menuItem49.Click += new System.EventHandler(this.menuApplicationsExport_Click);
            this.menuItem50.Click += new System.EventHandler(this.menuObsolescenceMap_Click);
            this.menuItem59.Click += new System.EventHandler(this.menuMAJLienApp_Click);
            this.menuItem60.Click += new System.EventHandler(this.tokenCloud_Click);
            this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
            this.menuItem25.Click += new System.EventHandler(this.menuItem25_Click);
            this.menuItem29.Click += new System.EventHandler(this.menuExport_Click);
            this.menuItem30.Click += new System.EventHandler(this.menuImport_Click);
            this.menuItem58.Click += new System.EventHandler(this.menuCopyField_Click);
            this.menuItem32.Click += new System.EventHandler(this.menuExtractIP_Click);
            this.menuItem46.Click += new System.EventHandler(this.menuExportServer_Click);
            this.menuItem57.Click += new System.EventHandler(this.menuEtatDB_Click);
            this.menuItem35.Click += new System.EventHandler(this.menuExportRefData_Click);
            this.menuItem55.Click += new System.EventHandler(this.menuExportDB_Click);
            this.menuItem40.Click += new System.EventHandler(this.menuItem40_Click);
            this.menuItem54.Click += new System.EventHandler(this.menuMiseAJourLibelles_Click);
            this.menuHelpAbout.Click += new System.EventHandler(this.menuHelpAbout_Click);
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            this.tvObjet.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvObjet_AfterSelect);
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            this.NomApplication.Click += new System.EventHandler(this.NomApplication_Click);
            this.cbApplication.SelectedIndexChanged += new System.EventHandler(this.cbApplication_SelectedIndexChanged);
            this.cbApplication.Validated += new System.EventHandler(this.cbApplication_Validated);
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            this.cbVue.SelectedIndexChanged += new System.EventHandler(this.cbVue_SelectedIndexChanged);
            this.dataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellClick);
            this.dataGrid.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellValidated);


            */
            tbFind.TextChanged += TbFind_TextChanged;
        }

        private TreeNode findNodes(string t, TreeNodeCollection tnc)
        {
            for (int i = 0; i < tnc.Count; i++)
            {
                TreeNode tn = tnc[i];
                if (tn.Text.Contains(t))
                    return tn;
            }
            for (int i = 0; i < tnc.Count; i++)
            {
                TreeNode tn = findNodes(t, tnc[i].Nodes);
                if (tn != null) return tn;
            }

            return null;
        }

        private void TbFind_TextChanged(object sender, EventArgs e)
        {
            if (tvObjet.GetNodeCount(true) != 0)
            {
                if (tbFind.Text.Length >= 3)
                {
                    TreeNodeCollection tnc;
                    if (tvObjet.SelectedNode == null) tnc = tvObjet.Nodes;
                    else if (tvObjet.SelectedNode.Nodes.Count > 0) tnc = tvObjet.SelectedNode.Nodes;
                    else tnc = tvObjet.SelectedNode.Parent.Nodes;
                    //else if (tvObjet.SelectedNode.Parent == null) tnc = tvObjet.Nodes;
                    //else tnc = tvObjet.SelectedNode.Parent.Nodes.;
                    TreeNode tn = findNodes(tbFind.Text, tnc);
                    if (tn != null)
                    {
                        tvObjet.SelectedNode = tn;
                        //tvObjet.Focus();
                    }
                }
            }
            //throw new NotImplementedException();
        }

        private void MenuItem54_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MAJLienTechObj(string sSens, string obj, string gobj="")
        {
            // mise à jour objet 
            List<string[]> lstMAJLink = new List<string[]>();
            if (gobj == "") gobj = obj;

            if (oCnxBase.CBRecherche("Select Distinct Guidtechlink, " + obj + ".Guid" + obj + ", Nom" + obj + ", Application.GuidApplication, NomApplication From techLink, " + obj + ", G" + gobj + ", Dansvue, Vue, Appversion, Application Where GuidServer" + sSens + "=" + obj + ".Guid" + obj + " And " + obj + ".Guid" + obj + "=G" + gobj + ".Guid" + obj + " And GuidG" + gobj + "=GuidObjet And DansVue.GuidGVue=Vue.GuidGVue And Vue.GuidAppVersion=Appversion.GuidAppVersion And Appversion.GuidApplication=Application.GuidApplication"))
            {
                while (oCnxBase.Reader.Read())
                {
                    string[] aEnreg = new string[6];
                    aEnreg[0] = oCnxBase.Reader.GetString(0);   // GuidtechLink
                    aEnreg[1] = oCnxBase.Reader.GetString(1);   // Guidobj
                    aEnreg[2] = oCnxBase.Reader.GetString(2);   // Nomobj
                    aEnreg[3] = oCnxBase.Reader.GetString(3);   // GuidApplication
                    aEnreg[4] = oCnxBase.Reader.GetString(4);   // NomApplication
                    lstMAJLink.Add(aEnreg);
                }
            }
            oCnxBase.Reader.Close();
            for (int i = 0; i < lstMAJLink.Count; i++)
                oCnxBase.CBWrite("Update TechLink Set NomServer" + sSens + " = '" + lstMAJLink[i][2] + "', TypeServer" + sSens + " = '" + obj + "', GuidServerL2" + sSens + " = '" + lstMAJLink[i][1] + "', NomServerL2" + sSens + " = '" + lstMAJLink[i][2] + "', TypeServerL2" + sSens + " = '" + obj + "', GuidApp" + sSens + " = '" + lstMAJLink[i][3] + "', NomApp" + sSens + " = '" + lstMAJLink[i][4] + "' Where GuidTechLink = '" + lstMAJLink[i][0] + "'");
        }

        private void menuMAJLienTech_Click(object sender, EventArgs e)
        {
            
            List<string[]> lstMAJLink = new List<string[]>();
            // *** mise à jour des sources ***

            
            // mise à jour objet application In
            if (oCnxBase.CBRecherche("Select Guidtechlink, GuidApplication, NomApplication From techlink, application where GuidServerIn = GuidApplication"))
            {
                while (oCnxBase.Reader.Read())
                {
                    string[] aEnreg = new string[3];
                    aEnreg[0] = oCnxBase.Reader.GetString(0);   // GuidTechLink
                    aEnreg[1] = oCnxBase.Reader.GetString(1);   // GuidApplication
                    aEnreg[2] = oCnxBase.Reader.GetString(2);   // NomApplication
                    lstMAJLink.Add(aEnreg);
                }
            }
            oCnxBase.Reader.Close();

            
            for (int i = 0; i < lstMAJLink.Count; i++)
                oCnxBase.CBWrite("Update TechLink Set NomServerIn = '" + lstMAJLink[i][2]  + "', TypeServerIn = 'Application', GuidServerL2In = '" + lstMAJLink[i][1] + "', NomServerL2In = '" + lstMAJLink[i][2] + "', TypeServerL2In = 'Application', GuidAppIn = '" + lstMAJLink[i][1] + "',  NomAppIn = '" + lstMAJLink[i][2] + "' Where GuidTechLink = '" + lstMAJLink[i][0] + "'");
            
            MAJLienTechObj("In", "AppUser", "TechUser"); // mise à jour objet appuser
            MAJLienTechObj("In", "Server"); // mise à jour objet server
            
            MAJLienTechObj("In", "Gening"); // mise à jour objet ingres
            MAJLienTechObj("In", "Gensvc"); // mise à jour objet services
            MAJLienTechObj("In", "Genpod"); // mise à jour objet pod
            MAJLienTechObj("In", "Gensas"); // mise à jour objet saas


            // mise à jour objet application out
            if (oCnxBase.CBRecherche("Select Guidtechlink, GuidApplication, NomApplication From techlink, application where GuidServerOut = GuidApplication"))
            {
                while (oCnxBase.Reader.Read())
                {
                    string[] aEnreg = new string[3];
                    aEnreg[0] = oCnxBase.Reader.GetString(0);   // GuidTechLink
                    aEnreg[1] = oCnxBase.Reader.GetString(1);   // GuidApplication
                    aEnreg[2] = oCnxBase.Reader.GetString(2);   // NomApplication
                    lstMAJLink.Add(aEnreg);
                }
            }
            oCnxBase.Reader.Close();


            for (int i = 0; i < lstMAJLink.Count; i++)
                oCnxBase.CBWrite("Update TechLink Set NomServerOut = '" + lstMAJLink[i][2] + "', TypeServerOut = 'Application', GuidServerL2Out = '" + lstMAJLink[i][1] + "', NomServerL2Out = '" + lstMAJLink[i][2] + "', TypeServerL2Out = 'Application', GuidAppOut = '" + lstMAJLink[i][1] + "',  NomAppOut = '" + lstMAJLink[i][2] + "' Where GuidTechLink = '" + lstMAJLink[i][0] + "'");
            
            MAJLienTechObj("Out", "AppUser", "TechUser"); // mise à jour objet appuser
            MAJLienTechObj("Out", "Server"); // mise à jour objet server

            MAJLienTechObj("Out", "Gening"); // mise à jour objet ingres
            MAJLienTechObj("Out", "Gensvc"); // mise à jour objet services
            MAJLienTechObj("Out", "Genpod"); // mise à jour objet pod
            MAJLienTechObj("Out", "Gensas"); // mise à jour objet saas


        }

        private void menuMAJLienApp_Click(object sender, EventArgs e)
        {
            /*
            List<string[]> lstMAJLink = new List<string[]>();
            // *** mise à jour des sources ***
            
            // mise à jour objet application
            if (oCnxBase.CBRecherche("Select Guidlink, GuidApplication, NomApplication From link, application where GuidComposantL1In = GuidApplication"))
            {
                while (oCnxBase.Reader.Read())
                {
                    string[] aEnreg = new string[3];
                    aEnreg[0] = oCnxBase.Reader.GetString(0);   // GuidLink
                    aEnreg[1] = oCnxBase.Reader.GetString(1);   // GuidApplication
                    aEnreg[2] = oCnxBase.Reader.GetString(2);   // NomApplication
                    lstMAJLink.Add(aEnreg);
                }
            }
            oCnxBase.Reader.Close();
            for (int i = 0; i < lstMAJLink.Count; i++)
                oCnxBase.CBWrite("Update Link Set NomComposantL1In = '" + lstMAJLink[i][2]  + "', TypeComposantL1In = 'Application', GuidComposantL2In = '" + lstMAJLink[i][1] + "', NomComposantL2In = '" + lstMAJLink[i][2] + "', TypeComposantL2In = 'Application', GuidAppIn = '" + lstMAJLink[i][1] + "', NomAppIn = '" + lstMAJLink[i][2] + "' Where GuidLink = '" + lstMAJLink[i][0] + "'");
            
            // mise à jour objet user
            lstMAJLink.Clear();
            if (oCnxBase.CBRecherche("Select Distinct Guidlink, User.GuidAppUser, NomAppUser, Application.GuidApplication, NomApplication From Link, User, GUser, DansVue, Vue, AppVersion, Application Where GuidComposantL1In=User.GuidAppUser And User.GuidAppUser=GUser.GuidAppUser And GuidGAppUser=GuidObjet And DansVue.GuidGVue=Vue.GuidGVue And Vue.GuidAppVersion=AppVersion.GuidAppVersion And AppVersion.GuidApplication=Application.GuidApplication"))
            {
                while (oCnxBase.Reader.Read())
                {
                    string[] aEnreg = new string[5];
                    aEnreg[0] = oCnxBase.Reader.GetString(0);   // GuidLink
                    aEnreg[1] = oCnxBase.Reader.GetString(1);   // GuidAppUser
                    aEnreg[2] = oCnxBase.Reader.GetString(2);   // NomAppUser
                    aEnreg[3] = oCnxBase.Reader.GetString(3);   // GuidApplication
                    aEnreg[4] = oCnxBase.Reader.GetString(4);   // NomdApplication
                    lstMAJLink.Add(aEnreg);
                }
            }
            oCnxBase.Reader.Close();
            for (int i = 0; i < lstMAJLink.Count; i++)
                oCnxBase.CBWrite("Update Link Set NomComposantL1In = '" + lstMAJLink[i][2] + "', TypeComposantL1In = 'User', GuidComposantL2In = '" + lstMAJLink[i][1] + "', NomComposantL2In = '" + lstMAJLink[i][2] + "', TypeComposantL2In = 'User', GuidAppIn = '" + lstMAJLink[i][3] + "', NomAppIn = '" + lstMAJLink[i][4] + "' Where GuidLink = '" + lstMAJLink[i][0] + "'");
            
            
            // mise à jour objet composant
            lstMAJLink.Clear();
            if (oCnxBase.CBRecherche("Select Distinct Guidlink, NomComposant, MainComposant.GuidMainComposant, NomMainComposant, Application.GuidApplication, NomApplication From Link, Composant, MainComposant, GComposant, Dansvue, Vue, Appversion, Application Where GuidComposantL1In=Composant.GuidComposant And Composant.GuidMainComposant=MainComposant.GuidMainComposant And Composant.GuidComposant=GComposant.GuidComposant And GuidGComposant=GuidObjet And DansVue.GuidGVue=Vue.GuidGVue And Vue.GuidAppVersion=Appversion.GuidAppVersion And Appversion.GuidApplication=Application.GuidApplication"))
            {
                while (oCnxBase.Reader.Read())
                {
                    string[] aEnreg = new string[6];
                    aEnreg[0] = oCnxBase.Reader.GetString(0);   // GuidLink
                    aEnreg[1] = oCnxBase.Reader.GetString(1);   // NomComposant
                    aEnreg[2] = oCnxBase.Reader.GetString(2);   // GuidMainComposant
                    aEnreg[3] = oCnxBase.Reader.GetString(3);   // NomMainComponsant
                    aEnreg[4] = oCnxBase.Reader.GetString(4);   // GuidApplication
                    aEnreg[5] = oCnxBase.Reader.GetString(5);   // NomApplication
                    lstMAJLink.Add(aEnreg);
                }
            }
            oCnxBase.Reader.Close();
            for (int i = 0; i < lstMAJLink.Count; i++)
                oCnxBase.CBWrite("Update Link Set NomComposantL1In = '" + lstMAJLink[i][1] + "', TypeComposantL1In = 'Composant', GuidComposantL2In = '" + lstMAJLink[i][2] + "', NomComposantL2In = '" + lstMAJLink[i][3] + "', TypeComposantL2In = 'MainComposant', GuidAppIn = '" + lstMAJLink[i][4] + "', NomAppIn = '" + lstMAJLink[i][5] + "' Where GuidLink = '" + lstMAJLink[i][0] + "'");
            
            // mise à jour objet file
            lstMAJLink.Clear();
            if (oCnxBase.CBRecherche("Select Distinct Guidlink, NomFile, MainComposant.GuidMainComposant, NomMainComposant, Application.GuidApplication, NomApplication From Link, file, MainComposant, GFile, Dansvue, Vue, Appversion, Application Where GuidComposantL1In=File.GuidFile And File.GuidMainComposant=MainComposant.GuidMainComposant And File.GuidFile=GFile.GuidFile And GuidGFile=GuidObjet And DansVue.GuidGVue=Vue.GuidGVue And Vue.GuidAppVersion=Appversion.GuidAppVersion And Appversion.GuidApplication=Application.GuidApplication"))
            {
                while (oCnxBase.Reader.Read())
                {
                    string[] aEnreg = new string[6];
                    aEnreg[0] = oCnxBase.Reader.GetString(0);   // GuidLink
                    aEnreg[1] = oCnxBase.Reader.GetString(1);   // NomFile
                    aEnreg[2] = oCnxBase.Reader.GetString(2);   // GuidMainComposant
                    aEnreg[3] = oCnxBase.Reader.GetString(3);   // NomMainComponsant
                    aEnreg[4] = oCnxBase.Reader.GetString(4);   // GuidApplication
                    aEnreg[5] = oCnxBase.Reader.GetString(5);   // NomApplication
                    lstMAJLink.Add(aEnreg);
                }
            }
            oCnxBase.Reader.Close();
            for (int i = 0; i < lstMAJLink.Count; i++)
                oCnxBase.CBWrite("Update Link Set NomComposantL1In = '" + lstMAJLink[i][1] + "', TypeComposantL1In = 'File', GuidComposantL2In = '" + lstMAJLink[i][2] + "', NomComposantL2In = '" + lstMAJLink[i][3] + "', TypeComposantL2In = 'MainComposant', GuidAppIn = '" + lstMAJLink[i][4] + "', NomAppIn = '" + lstMAJLink[i][5] + "' Where GuidLink = '" + lstMAJLink[i][0] + "'");
            
            
            // mise à jour objet base
            lstMAJLink.Clear();
            if (oCnxBase.CBRecherche("Select Distinct Guidlink, NomBase, MainComposant.GuidMainComposant, NomMainComposant, Application.GuidApplication, NomApplication From Link, Base, MainComposant, GBase, Dansvue, Vue, Appversion, Application Where GuidComposantL1In=Base.GuidBase And Base.GuidMainComposant=MainComposant.GuidMainComposant And Base.GuidBase=GBase.GuidBase And GuidGBase=GuidObjet And DansVue.GuidGVue=Vue.GuidGVue And Vue.GuidAppVersion=Appversion.GuidAppVersion And Appversion.GuidApplication=Application.GuidApplication"))
            {
                while (oCnxBase.Reader.Read())
                {
                    string[] aEnreg = new string[6];
                    aEnreg[0] = oCnxBase.Reader.GetString(0);   // GuidLink
                    aEnreg[1] = oCnxBase.Reader.GetString(1);   // NomBase
                    aEnreg[2] = oCnxBase.Reader.GetString(2);   // GuidMainComposant
                    aEnreg[3] = oCnxBase.Reader.GetString(3);   // NomMainComponsant
                    aEnreg[4] = oCnxBase.Reader.GetString(4);   // GuidApplication
                    aEnreg[5] = oCnxBase.Reader.GetString(5);   // NomApplication
                    lstMAJLink.Add(aEnreg);
                }
            }
            oCnxBase.Reader.Close();
            for (int i = 0; i < lstMAJLink.Count; i++)
                oCnxBase.CBWrite("Update Link Set NomComposantL1In = '" + lstMAJLink[i][1] + "', TypeComposantL1In = 'Base', GuidComposantL2In = '" + lstMAJLink[i][2] + "', NomComposantL2In = '" + lstMAJLink[i][3] + "', TypeComposantL2In = 'MainComposant', GuidAppIn = '" + lstMAJLink[i][4] + "', NomAppIn = '" + lstMAJLink[i][5] + "' Where GuidLink = '" + lstMAJLink[i][0] + "'");

            
            // mise à jour objet interface
            lstMAJLink.Clear();
            if (oCnxBase.CBRecherche("Select Distinct Guidlink, NomInterface, MainComposant.GuidMainComposant, NomMainComposant, Application.GuidApplication, NomApplication From Link, Interface, MainComposant, GInterface, Dansvue, Vue, Appversion, Application Where GuidComposantL1In=Interface.GuidInterface And Interface.GuidMainComposant=MainComposant.GuidMainComposant And Interface.GuidInterface=GInterface.GuidInterface And GuidGInterface=GuidObjet And DansVue.GuidGVue=Vue.GuidGVue And Vue.GuidAppVersion=Appversion.GuidAppVersion And Appversion.GuidApplication=Application.GuidApplication"))
            {
                while (oCnxBase.Reader.Read())
                {
                    string[] aEnreg = new string[6];
                    aEnreg[0] = oCnxBase.Reader.GetString(0);   // GuidLink
                    aEnreg[1] = oCnxBase.Reader.GetString(1);   // NomInterface
                    aEnreg[2] = oCnxBase.Reader.GetString(2);   // GuidMainComposant
                    aEnreg[3] = oCnxBase.Reader.GetString(3);   // NomMainComponsant
                    aEnreg[4] = oCnxBase.Reader.GetString(4);   // GuidApplication
                    aEnreg[5] = oCnxBase.Reader.GetString(5);   // NomApplication
                    lstMAJLink.Add(aEnreg);
                }
            }
            oCnxBase.Reader.Close();
            for (int i = 0; i < lstMAJLink.Count; i++)
                oCnxBase.CBWrite("Update Link Set NomComposantL1In = '" + lstMAJLink[i][1] + "', TypeComposantL1In = 'Interface', GuidComposantL2In = '" + lstMAJLink[i][2] + "', NomComposantL2In = '" + lstMAJLink[i][3] + "', TypeComposantL2In = 'MainComposant', GuidAppIn = '" + lstMAJLink[i][4] + "', NomAppIn = '" + lstMAJLink[i][5] + "' Where GuidLink = '" + lstMAJLink[i][0] + "'");

            
            // *** mise à jour des target ***

            // mise à jour objet application
            lstMAJLink.Clear();
            if (oCnxBase.CBRecherche("Select Guidlink, GuidApplication, NomApplication From link, application where GuidComposantL1Out = GuidApplication"))
            {
                while (oCnxBase.Reader.Read())
                {
                    string[] aEnreg = new string[3];
                    aEnreg[0] = oCnxBase.Reader.GetString(0);   // GuidLink
                    aEnreg[1] = oCnxBase.Reader.GetString(1);   // GuidApplication
                    aEnreg[2] = oCnxBase.Reader.GetString(2);   // NomApplication
                    lstMAJLink.Add(aEnreg);
                }
            }
            oCnxBase.Reader.Close();
            for (int i = 0; i < lstMAJLink.Count; i++)
                oCnxBase.CBWrite("Update Link Set NomComposantL1Out = '" + lstMAJLink[i][2] + "', TypeComposantL1Out = 'Application', GuidComposantL2Out = '" + lstMAJLink[i][1] + "', NomComposantL2Out = '" + lstMAJLink[i][2] + "', TypeComposantL2Out = 'Application', GuidAppOut = '" + lstMAJLink[i][1] + "', NomAppOut = '" + lstMAJLink[i][2] + "' Where GuidLink = '" + lstMAJLink[i][0] + "'");

            
            // mise à jour objet user
            lstMAJLink.Clear();
            if (oCnxBase.CBRecherche("Select Distinct Guidlink, User.GuidAppUser, NomAppUser, Application.GuidApplication, NomApplication From Link, User, GUser, DansVue, Vue, AppVersion, Application Where GuidComposantL1Out=User.GuidAppUser And User.GuidAppUser=GUser.GuidAppUser And GuidGAppUser=GuidObjet And DansVue.GuidGVue=Vue.GuidGVue And Vue.GuidAppVersion=AppVersion.GuidAppVersion And AppVersion.GuidApplication=Application.GuidApplication"))
            {
                while (oCnxBase.Reader.Read())
                {
                    string[] aEnreg = new string[5];
                    aEnreg[0] = oCnxBase.Reader.GetString(0);   // GuidLink
                    aEnreg[1] = oCnxBase.Reader.GetString(1);   // GuidAppUser
                    aEnreg[2] = oCnxBase.Reader.GetString(2);   // NomAppUser
                    aEnreg[3] = oCnxBase.Reader.GetString(3);   // GuidApplication
                    aEnreg[4] = oCnxBase.Reader.GetString(4);   // NomdApplication
                    lstMAJLink.Add(aEnreg);
                }
            }
            oCnxBase.Reader.Close();
            for (int i = 0; i < lstMAJLink.Count; i++)
                oCnxBase.CBWrite("Update Link Set NomComposantL1Out = '" + lstMAJLink[i][2] + "', TypeComposantL1Out = 'User', GuidComposantL2Out = '" + lstMAJLink[i][1] + "', NomComposantL2Out = '" + lstMAJLink[i][2] + "', TypeComposantL2Out = 'User', GuidAppOut = '" + lstMAJLink[i][3] + "', NomAppOut = '" + lstMAJLink[i][4] + "' Where GuidLink = '" + lstMAJLink[i][0] + "'");

            
            // mise à jour objet composant
            lstMAJLink.Clear();
            if (oCnxBase.CBRecherche("Select Distinct Guidlink, NomComposant, MainComposant.GuidMainComposant, NomMainComposant, Application.GuidApplication, NomApplication From Link, Composant, MainComposant, GComposant, Dansvue, Vue, Appversion, Application Where GuidComposantL1Out=Composant.GuidComposant And Composant.GuidMainComposant=MainComposant.GuidMainComposant And Composant.GuidComposant=GComposant.GuidComposant And GuidGComposant=GuidObjet And DansVue.GuidGVue=Vue.GuidGVue And Vue.GuidAppVersion=Appversion.GuidAppVersion And Appversion.GuidApplication=Application.GuidApplication"))
            {
                while (oCnxBase.Reader.Read())
                {
                    string[] aEnreg = new string[6];
                    aEnreg[0] = oCnxBase.Reader.GetString(0);   // GuidLink
                    aEnreg[1] = oCnxBase.Reader.GetString(1);   // NomComposant
                    aEnreg[2] = oCnxBase.Reader.GetString(2);   // GuidMainComposant
                    aEnreg[3] = oCnxBase.Reader.GetString(3);   // NomMainComponsant
                    aEnreg[4] = oCnxBase.Reader.GetString(4);   // GuidApplication
                    aEnreg[5] = oCnxBase.Reader.GetString(5);   // NomApplication
                    lstMAJLink.Add(aEnreg);
                }
            }
            oCnxBase.Reader.Close();
            for (int i = 0; i < lstMAJLink.Count; i++)
                oCnxBase.CBWrite("Update Link Set NomComposantL1Out = '" + lstMAJLink[i][1] + "', TypeComposantL1Out = 'Composant', GuidComposantL2Out = '" + lstMAJLink[i][2] + "', NomComposantL2Out = '" + lstMAJLink[i][3] + "', TypeComposantL2Out = 'MainComposant', GuidAppOut = '" + lstMAJLink[i][4] + "', NomAppOut = '" + lstMAJLink[i][5] + "' Where GuidLink = '" + lstMAJLink[i][0] + "'");

            // mise à jour objet file
            lstMAJLink.Clear();
            if (oCnxBase.CBRecherche("Select Distinct Guidlink, NomFile, MainComposant.GuidMainComposant, NomMainComposant, Application.GuidApplication, NomApplication From Link, file, MainComposant, GFile, Dansvue, Vue, Appversion, Application Where GuidComposantL1Out=File.GuidFile And File.GuidMainComposant=MainComposant.GuidMainComposant And File.GuidFile=GFile.GuidFile And GuidGFile=GuidObjet And DansVue.GuidGVue=Vue.GuidGVue And Vue.GuidAppVersion=Appversion.GuidAppVersion And Appversion.GuidApplication=Application.GuidApplication"))
            {
                while (oCnxBase.Reader.Read())
                {
                    string[] aEnreg = new string[6];
                    aEnreg[0] = oCnxBase.Reader.GetString(0);   // GuidLink
                    aEnreg[1] = oCnxBase.Reader.GetString(1);   // NomFile
                    aEnreg[2] = oCnxBase.Reader.GetString(2);   // GuidMainComposant
                    aEnreg[3] = oCnxBase.Reader.GetString(3);   // NomComponsant
                    aEnreg[4] = oCnxBase.Reader.GetString(4);   // GuidApplication
                    aEnreg[5] = oCnxBase.Reader.GetString(5);   // NomApplication
                    lstMAJLink.Add(aEnreg);
                }
            }
            oCnxBase.Reader.Close();
            for (int i = 0; i < lstMAJLink.Count; i++)
                oCnxBase.CBWrite("Update Link Set NomComposantL1Out = '" + lstMAJLink[i][1] + "', TypeComposantL1Out = 'File', GuidComposantL2Out = '" + lstMAJLink[i][2] + "', NomComposantL2Out = '" + lstMAJLink[i][3] + "', TypeComposantL2Out = 'MainComposant', GuidAppOut = '" + lstMAJLink[i][4] + "', NomAppOut = '" + lstMAJLink[i][5] + "' Where GuidLink = '" + lstMAJLink[i][0] + "'");


            // mise à jour objet base
            lstMAJLink.Clear();
            if (oCnxBase.CBRecherche("Select Distinct Guidlink, NomBase, MainComposant.GuidMainComposant, NomMainComposant, Application.GuidApplication, NomApplication From Link, Base, MainComposant, GBase, Dansvue, Vue, Appversion, Application Where GuidComposantL1Out=Base.GuidBase And Base.GuidMainComposant=MainComposant.GuidMainComposant And Base.GuidBase=GBase.GuidBase And GuidGBase=GuidObjet And DansVue.GuidGVue=Vue.GuidGVue And Vue.GuidAppVersion=Appversion.GuidAppVersion And Appversion.GuidApplication=Application.GuidApplication"))
            {
                while (oCnxBase.Reader.Read())
                {
                    string[] aEnreg = new string[6];
                    aEnreg[0] = oCnxBase.Reader.GetString(0);   // GuidLink
                    aEnreg[1] = oCnxBase.Reader.GetString(1);   // NomBase
                    aEnreg[2] = oCnxBase.Reader.GetString(2);   // GuidMainComposant
                    aEnreg[3] = oCnxBase.Reader.GetString(3);   // NomMainComponsant
                    aEnreg[4] = oCnxBase.Reader.GetString(4);   // GuidApplication
                    aEnreg[5] = oCnxBase.Reader.GetString(5);   // NomApplication
                    lstMAJLink.Add(aEnreg);
                }
            }
            oCnxBase.Reader.Close();
            for (int i = 0; i < lstMAJLink.Count; i++)
                oCnxBase.CBWrite("Update Link Set NomComposantL1Out = '" + lstMAJLink[i][1] + "', TypeComposantL1Out = 'Base', GuidComposantL2Out = '" + lstMAJLink[i][2] + "', NomComposantL2Out = '" + lstMAJLink[i][3] + "', TypeComposantL2Out = 'MainComposant', GuidAppOut = '" + lstMAJLink[i][4] + "', NomAppOut = '" + lstMAJLink[i][5] + "' Where GuidLink = '" + lstMAJLink[i][0] + "'");


            // mise à jour objet interface
            lstMAJLink.Clear();
            if (oCnxBase.CBRecherche("Select Distinct Guidlink, NomInterface, MainComposant.GuidMainComposant, NomMainComposant, Application.GuidApplication, NomApplication From Link, Interface, MainComposant, GInterface, Dansvue, Vue, Appversion, Application Where GuidComposantL1Out=Interface.GuidInterface And Interface.GuidMainComposant=MainComposant.GuidMainComposant And Interface.GuidInterface=GInterface.GuidInterface And GuidGInterface=GuidObjet And DansVue.GuidGVue=Vue.GuidGVue And Vue.GuidAppVersion=Appversion.GuidAppVersion And Appversion.GuidApplication=Application.GuidApplication"))
            {
                while (oCnxBase.Reader.Read())
                {
                    string[] aEnreg = new string[6];
                    aEnreg[0] = oCnxBase.Reader.GetString(0);   // GuidLink
                    aEnreg[1] = oCnxBase.Reader.GetString(1);   // NomInterface
                    aEnreg[2] = oCnxBase.Reader.GetString(2);   // GuidMainComposant
                    aEnreg[3] = oCnxBase.Reader.GetString(3);   // NomMainComponsant
                    aEnreg[4] = oCnxBase.Reader.GetString(4);   // GuidApplication
                    aEnreg[5] = oCnxBase.Reader.GetString(5);   // NomApplication
                    lstMAJLink.Add(aEnreg);
                }
            }
            oCnxBase.Reader.Close();
            for (int i = 0; i < lstMAJLink.Count; i++)
                oCnxBase.CBWrite("Update Link Set NomComposantL1Out = '" + lstMAJLink[i][1] + "', TypeComposantL1Out = 'Interface', GuidComposantL2Out = '" + lstMAJLink[i][2] + "', NomComposantL2Out = '" + lstMAJLink[i][3] + "', TypeComposantL2Out = 'MainComposant', GuidAppOut = '" + lstMAJLink[i][4] + "', NomAppOut = '" + lstMAJLink[i][5] + "' Where GuidLink = '" + lstMAJLink[i][0] + "'");

            */
        }

        void cbApplication_Validating(object sender, CancelEventArgs e)
        {
            //ClearApp();
            //throw new NotImplementedException();
        }

        void cbApplication_Validated(object sender, EventArgs e)
        {

            /*
            ArrayList lstTemplate;
            lstTemplate = new ArrayList();
            if (cbApplication.Text != "")
            {
                cbApplication.SelectedIndex = cbApplication.FindString(cbApplication.Text);
                if (cbApplication.SelectedIndex == -1) cbApplication.SelectedIndex = 0;
                cbApplication.SelectedItem = cbApplication.Items[cbApplication.SelectedIndex];
                cbApplication.Text = cbApplication.SelectedItem.ToString();
                bOpVue.Enabled = true;

                //Load les Templates des Layers liés à l'application
                //oCnxBase.CBRecherche("Select Guid
            }
            else
            {
                cbApplication.SelectedIndex = -1;
                cbApplication.SelectedItem = null;
                cbApplication.Text = "";
                initVarApp();
            }
            */
        }


        void cbVue_Validating(object sender, CancelEventArgs e)
        {

            //ClearVue(false);
            //throw new NotImplementedException();
        }

        void cbVue_Validated(object sender, EventArgs e)
        {
            ClearVue(false);
            if (cbVue.Text != "")
            {
                cbVue.SelectedIndex = cbVue.FindString(cbVue.Text);
                if (cbVue.SelectedIndex == -1) cbVue.SelectedIndex = 0;
                cbVue.SelectedItem = cbVue.Items[cbVue.SelectedIndex];
                cbVue.Text = cbVue.SelectedItem.ToString();
                if (!wkApp.ChgLayers) setCtrlEnabled(bSave, true); else setCtrlEnabled(bSave, false);


                GuidVue = new Guid((string)(this.cbGuidVue.Items[this.cbVue.SelectedIndex]));
#if APIREADY
                clVue clV = lstApps.applications[cbApplication.SelectedIndex].appVersions[cbVersion.SelectedIndex].vues[cbVue.SelectedIndex];
                if(clV.guidGVue!=null)
                {
                    tbTypeVue.Text = clV.nomTypeVue;
                    sTypeVue = clV.nomTypeVue;
                    GuidGVue = new Guid(clV.guidGVue);
                    sNomEnvironnement = clV.nomEnvironnement;
                    sGuidVueInf = clV.guidVueInf;
                    tbVueInf.Text = clV.nomVueInf;
                    LoadVue();
                }

#else
                if (!oCnxBase.CBRecherche("SELECT NomTypeVue, GuidGVue FROM TypeVue, Vue WHERE GuidVue ='" + GuidVue + "' and TypeVue.GuidTypeVue=Vue.GuidTypeVue"))
                {
                    oCnxBase.CBReaderClose();
                }
                else
                {
                    tbTypeVue.Text = oCnxBase.Reader.GetString(0);
                    GuidGVue = new Guid(oCnxBase.Reader.GetString(1));
                    sTypeVue = oCnxBase.Reader.GetString(0);
                    oCnxBase.CBReaderClose();
                    sNomEnvironnement = null;
                    sGuidVueInf = null;
                    if (oCnxBase.CBRecherche("SELECT NomEnvironnement, v1.GuidVueInf, v2.NomVue FROM Vue v2, Vue v1 Left  Join  Environnement On v1.GuidEnvironnement=Environnement.GuidEnvironnement WHERE v1.GuidVue ='" + GuidVue + "' and v2.GuidVue=v1.GuidVueInf"))
                    //if (oCnxBase.CBRecherche("SELECT NomEnvironnement, GuidVueInf FROM Vue LEFT JOIN  Environnement ON Vue.GuidEnvironnement=Environnement.GuidEnvironnement WHERE GuidVue ='" + GuidVue + "'"))
                    {
                        if (!oCnxBase.Reader.IsDBNull(0)) sNomEnvironnement = oCnxBase.Reader.GetString(0); else sNomEnvironnement = null;
                        if (!oCnxBase.Reader.IsDBNull(1)) sGuidVueInf = oCnxBase.Reader.GetString(1); else sGuidVueInf = null;
                        if (!oCnxBase.Reader.IsDBNull(2)) tbVueInf.Text = oCnxBase.Reader.GetString(2); else tbVueInf.Text = null;
                    }

                    oCnxBase.CBReaderClose();
                    LoadVue();
                }
#endif
            }
            else ClearVue(true);

        }

        private void menuEditCopy_Click(object sender, EventArgs e)
        {
            if (drawArea.OSelected != null && drawArea.OSelected.Align)
            {
                DrawRectangle dr = (DrawRectangle)drawArea.OSelected;

                for (int n = 0; n < drawArea.GraphicsList.Count; n++)
                {
                    if (drawArea.GraphicsList[n].Selected && drawArea.GraphicsList[n].Align)
                    {
                        DrawRectangle o = (DrawRectangle)drawArea.GraphicsList[n];
                        o.rectangle.Width = dr.Rectangle.Width;
                        o.rectangle.Height = dr.Rectangle.Height;
                    }
                }
                drawArea.Refresh();
            }
            //drawArea.ActiveTool = DrawArea.DrawToolType.Copy;
        }

        void TopAlign_Click(object sender, EventArgs e)
        {
            if (drawArea.OSelected != null && drawArea.OSelected.Align)
            {
                DrawRectangle dr = (DrawRectangle)drawArea.OSelected;

                for (int n = 0; n < drawArea.GraphicsList.Count; n++)
                {
                    if (drawArea.GraphicsList[n].Selected && drawArea.GraphicsList[n].Align)
                    {
                        DrawRectangle o = (DrawRectangle)drawArea.GraphicsList[n];
                        o.rectangle.Y = dr.Rectangle.Y;
                    }
                }
                drawArea.Refresh();
            }
            //throw new NotImplementedException();
        }

        void BottomAlign_Click(object sender, EventArgs e)
        {
            if (drawArea.OSelected != null && drawArea.OSelected.Align)
            {
                DrawRectangle dr = (DrawRectangle)drawArea.OSelected;

                for (int n = 0; n < drawArea.GraphicsList.Count; n++)
                {
                    if (drawArea.GraphicsList[n].Selected && drawArea.GraphicsList[n].Align)
                    {
                        DrawRectangle o = (DrawRectangle)drawArea.GraphicsList[n];
                        o.rectangle.Y = dr.Rectangle.Bottom - o.Rectangle.Bottom + o.Rectangle.Y;
                    }
                }
                drawArea.Refresh();
            }
            //throw new NotImplementedException();
        }

        void LeftAlign_Click(object sender, EventArgs e)
        {
            if (drawArea.OSelected != null && drawArea.OSelected.Align)
            {
                DrawRectangle dr = (DrawRectangle)drawArea.OSelected;

                for (int n = 0; n < drawArea.GraphicsList.Count; n++)
                {
                    if (drawArea.GraphicsList[n].Selected && drawArea.GraphicsList[n].Align)
                    {
                        DrawRectangle o = (DrawRectangle)drawArea.GraphicsList[n];
                        o.rectangle.X = dr.Rectangle.X;
                    }
                }
                drawArea.Refresh();
            }
            //throw new NotImplementedException();
        }

        void RightAlign_Click(object sender, EventArgs e)
        {
            if (drawArea.OSelected != null && drawArea.OSelected.Align)
            {
                DrawRectangle dr = (DrawRectangle)drawArea.OSelected;

                for (int n = 0; n < drawArea.GraphicsList.Count; n++)
                {
                    if (drawArea.GraphicsList[n].Selected && drawArea.GraphicsList[n].Align)
                    {
                        DrawRectangle o = (DrawRectangle)drawArea.GraphicsList[n];
                        o.rectangle.X = dr.Rectangle.Right - o.Rectangle.Right + o.Rectangle.X;
                    }
                }
                drawArea.Refresh();
            }
            //throw new NotImplementedException();
        }

        void menuItem62_Click(object sender, EventArgs e)
        {
            CommandIPVlan();
            //throw new NotImplementedException();
        }


        void menuItem16_Click(object sender, EventArgs e)
        {
            CommandFlux();
            //throw new NotImplementedException();
        }

        void menuItem25_Click(object sender, EventArgs e)
        {
            CommandRules();
            //throw new NotImplementedException();
        }


        void DiscoveryReseaux_Click(object sender, EventArgs e)
        {
            FormddmSubnet fs = new FormddmSubnet(this);
            fs.ShowDialog(this);
        }

        void DiscoveryServeurs_Click(object sender, EventArgs e)
        {
            // requete : SELECT ddmhost.guidddmhost, nomddmhost, codepays, syncdate FROM ddmhost, ddmsubnet left join vlan on ddmsubnet.GuidVLan = vlan.GuidVlan where ddmhost.GuidddmSubnet = ddmsubnet.GuidddmSubnet and guidserverphy is null order by codepays

        }

        void DiscoveryTechnologies_Click(object sender, EventArgs e)
        {

        }



        void ApiDiscovery_Click(object sender, EventArgs e)
        {
            // Disco subnet
            //CommandApiDiscorery("Subnet", "", "", true);

            // Disco Host via Subnet
            //GetHostsApiDiscovery();
            
            // Map avec ServerPhy
            GetSyncHost();

            // Get Soft by Server
            //GetSoftsApiDiscovery();
         
            /*
            string[] lines = File.ReadAllLines(@"c:\dat\tmp\ServeursinconnusDT.txt");

            foreach (string hostname in lines)
            {
                CommandApiDiscorery("SoftwareInstance", "ddmNomHost", hostname, false);
            }
            */
        }

        void GetSyncHost()
        {
            TimeSpan tSpan = new TimeSpan(10, 0, 0, 0); // 10 jours
            DateTime dDate = DateTime.Now - tSpan;
            List<string[]> lstServers = new List<string[]>();
            

            if (oCnxBase.CBRecherche("SELECT guidddmhost, NomddmHost FROM ddmhost where Syncdate is null or SyncDate < '" + dDate.ToString("yyyy-MM-dd") + "' limit 50"))
            {
                while (oCnxBase.Reader.Read())
                {
                    
                    string[] aServer = oCnxBase.Reader.GetString(1).Split('.');
                    string[] EnregServer = new string[2];

                    EnregServer[0] = oCnxBase.Reader.GetString(0);
                    EnregServer[1] = aServer[0];
                    lstServers.Add(EnregServer);
                }

            }
            oCnxBase.Reader.Close();
            for (int i = 0; i < lstServers.Count; i++)
            {
                if (oCnxBase.CBRecherche("SELECT GuidServerPhy FROM Serverphy where NomServerPhy = '" + lstServers[i][1] + "'"))
                {
                    string sGuid = oCnxBase.Reader.GetString(0);
                    oCnxBase.Reader.Close();
                    oCnxBase.CBWrite("update ddmhost set GuidServerPhy = '" + sGuid + "', SyncDate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' where Guidddmhost='" + lstServers[i][0] + "'");
                }
                else
                {
                    oCnxBase.Reader.Close();
                    oCnxBase.CBWrite("update ddmhost set GuidServerPhy = null, SyncDate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' where Guidddmhost='" + lstServers[i][0] + "'");
                }
            }

        }

        void GetHostsApiDiscovery()
        {
            ArrayList lstSubnets = new ArrayList();
            TimeSpan tSpan = new TimeSpan(20, 0, 0, 0); // 20 jours
            DateTime dDate = DateTime.Now - tSpan;
            
            if (oCnxBase.CBRecherche("SELECT GuidddmSubnet FROM ddmsubnet where UpdateDate is null or UpdateDate < '" + dDate.ToString("yyyy-MM-dd") + "' limit 20"))
            {
                while (oCnxBase.Reader.Read()) lstSubnets.Add(oCnxBase.Reader.GetString(0));
                
            }
            oCnxBase.Reader.Close();
            for (int i = 0; i < lstSubnets.Count; i++) GetHostApiDiscorery((string)lstSubnets[i], false);
            
        }

        void GetSoftsApiDiscovery()
        {
            ArrayList lstHosts = new ArrayList();
            TimeSpan tSpan = new TimeSpan(10, 0, 0, 0); // 10 jours
            DateTime dDate = DateTime.Now - tSpan;

            //GetHostApiDiscorery("4efd55abcad1d695319c13d76ddc2277", false);


            if (oCnxBase.CBRecherche("SELECT GuidddmHost FROM ddmhost where UpdateDate is null or UpdateDate < '" + dDate.ToString("yyyy-MM-dd") + "' limit 50"))
            {
                while (oCnxBase.Reader.Read()) lstHosts.Add(oCnxBase.Reader.GetString(0));

            }
            oCnxBase.Reader.Close();
            for (int i = 0; i < lstHosts.Count; i++) GetSoftApiDiscorery((string)lstHosts[i], false);

        }


        public STDiscoRequest stDiscoRequest;
        public void GetHostApiDiscorery(string sGuidddmSubnet, bool bMapping)
        {
            CommandApiDiscorery("Host", "GuidddmSubnet", sGuidddmSubnet, bMapping);
            
            // mise à jour update sGuidddmSubnet
            oCnxBase.CBWrite("update ddmSubnet set updatedate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' where GuidddmSubnet = '" + sGuidddmSubnet + "'");
        }

        public void GetSoftApiDiscorery(string sGuidddmHost, bool bMapping)
        {
            CommandApiDiscorery("SoftwareInstance", "ddmGuidHost", sGuidddmHost, bMapping);
            oCnxBase.CBWrite("update ddmHost set updatedate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' where GuidddmHost = '" + sGuidddmHost + "'");
        }

        public void CommandApiDiscorery(string sKind, string sSearchAtt, string sSearchValue, bool bMapping)
        {
            //HtmlUtility.HtmlEncode(string s)  

            stDiscoRequest.bMapping = bMapping;
            stDiscoRequest.sSearchValue = sSearchValue;

            if (stDiscoRequest.lstSTDiscovery == null)
            {
                stDiscoRequest.lstSTDiscovery = new List<STDiscovery>();
                STDiscovery stDiscovery;


                //def vlan
                stDiscovery.sTable = "Vlan";
                stDiscovery.sKindDisco = "Subnet";
                stDiscovery.iMappingKey = 1;
                stDiscovery.sFilterKey = "";
                stDiscovery.sRequete = "search Subnet show hash(key) as 'GuidddmSubnet',ip_address_range as 'NomddmSubnet'";
                stDiscovery.ExecRespAPIDiscovery = webClient_GetGenericApiDiscovery;
                stDiscoRequest.lstSTDiscovery.Add(stDiscovery);

                //def Server by vlan
                stDiscovery.sTable = "ServerPhy";
                stDiscovery.sKindDisco = "Host";
                stDiscovery.iMappingKey = 1;
                stDiscovery.sFilterKey = "GuidddmSubnet";
                stDiscovery.sRequete = "search Host show hash(key) as 'GuidddmHost', hostname as 'NomddmHost', model," +
                    "os, os_version as 'osversion', os_class as 'osclass', time(creationTime(#)) as 'createtime', last_update as 'lastupdate'," +
                    "hash(#DeviceWithAddress:DeviceAddress:IPv4Address:IPAddress.#DeviceOnSubnet:DeviceSubnet:Subnet:Subnet.key) as 'GuidddmSubnet' " +
                    "processwith where hash(#DeviceWithAddress:DeviceAddress:IPv4Address:IPAddress.#DeviceOnSubnet:DeviceSubnet:Subnet:Subnet.key) = 'stDiscoRequest.sSearchValue'";
                stDiscovery.ExecRespAPIDiscovery = webClient_GetGenericApiDiscovery;
                stDiscoRequest.lstSTDiscovery.Add(stDiscovery);

                //def Software by Server
                stDiscovery.sTable = "TechnoRef";
                stDiscovery.sKindDisco = "SoftwareInstance";
                stDiscovery.iMappingKey = 1;
                stDiscovery.sFilterKey = "ddmGuidHost";
                stDiscovery.sRequete = "search SoftwareInstance show hash(key) as 'GuidddmSoftwareInstance', type as 'NomddmProduit', product_version as 'Version'," +
                    "hash(#RunningSoftware:HostedSoftware:Host:Host.key) as 'GuidddmHost'," +
                    "#ElementWithDetail:SupportDetail:SoftwareDetail:SupportDetail.end_support_date as 'EOS'," +
                    "#ElementWithDetail:SupportDetail:SoftwareDetail:SupportDetail.end_ext_support_date as 'EOES' " +
                    "processwith where hash(#RunningSoftware:HostedSoftware:Host:Host.key) = 'stDiscoRequest.sSearchValue'";
                stDiscovery.ExecRespAPIDiscovery = webClient_GetTechnoApiDiscovery;
                stDiscoRequest.lstSTDiscovery.Add(stDiscovery);

                //def Software by ServerName
                stDiscovery.sTable = "TechnoRef";
                stDiscovery.sKindDisco = "SoftwareInstance";
                stDiscovery.iMappingKey = 1;
                stDiscovery.sFilterKey = "ddmNomHost";
                stDiscovery.sRequete = "search SoftwareInstance show hash(key) as 'GuidddmSoftwareInstance', type as 'NomProduit', product_version as 'Version'," +
                    "hash(#RunningSoftware:HostedSoftware:Host:Host.key) as 'GuidHost', #RunningSoftware:HostedSoftware:Host:Host.hostname as 'NomHost'," +
                    "#ElementWithDetail:SupportDetail:SoftwareDetail:SupportDetail.end_support_date as 'EOS'," +
                    "#ElementWithDetail:SupportDetail:SoftwareDetail:SupportDetail.end_ext_support_date as 'EOES' " +
                    "processwith where #RunningSoftware:HostedSoftware:Host:Host.hostname = 'stDiscoRequest.sSearchValue'";
                stDiscovery.ExecRespAPIDiscovery = webClient_GetApiDiscovery;
                stDiscoRequest.lstSTDiscovery.Add(stDiscovery);
            }

            int iDisco = stDiscoRequest.lstSTDiscovery.FindIndex(el => el.sKindDisco == sKind && el.sFilterKey == sSearchAtt);
            if (iDisco != -1)
            {
                stDiscoRequest.iDiscoRequete = iDisco;
                STDiscovery stDisco = stDiscoRequest.lstSTDiscovery[iDisco];
            
                using (var webClient = new System.Net.WebClient())
                {
                    webClient.Encoding = System.Text.UTF8Encoding.UTF8;

                    System.Net.NetworkCredential netCred = new System.Net.NetworkCredential();
                    netCred.UserName = "140488";
                    netCred.Password = "Leone!57";
                    webClient.Credentials = netCred;
                    System.Net.WebProxy wp = new System.Net.WebProxy("10.160.249.135:8080", true);
                    wp.Credentials = netCred;
                    webClient.Proxy = wp;

                    webClient.Headers.Add("Accept", "application/json");
                    webClient.Headers.Add("Authorization", "Bearer <NDpJVEE6OjpCT2x5cGluaDJFRTZOZHMwVzhFMzJKNWVOT3NCTW94bWRYRUJFd2NIS0hyQXhvOWNVTlBQaWc6MC03ZWY1ZjMwOWY0Y2JjZmU0NjFkMTBmZGFjYjE0YzRiMDg5NzIwNmY1NGY5MDA2ZTBlNGFhNDQ1YzlkYzY0MDk0>");
                    webClient.Headers.Add("KindSearch", stDisco.sKindDisco);
                    webClient.Headers.Add("SearchValue", sSearchValue);
                    webClient.Headers.Add("Requete", stDisco.sRequete);

                    var DiscoQuery = new clDiscoQuery { query = stDisco.sRequete.Replace("stDiscoRequest.sSearchValue", sSearchValue)};

                    webClient.UploadStringCompleted += stDisco.ExecRespAPIDiscovery;
                    Uri urlToRequest = new Uri(@"https://ddm.leasingsolutions.rb.echonet/api/v1.3/data/search?limit=0");
                    webClient.UploadStringAsync(urlToRequest, "POST", JsonConvert.SerializeObject(DiscoQuery));

                }
            }
            //throw new NotImplementedException();

        }

        public void webClient_GetTechnoApiDiscovery(object sender, System.Net.UploadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }
            if (e.Result != null && e.Result.Length > 0)
            {
                System.Net.WebClient webClient = (System.Net.WebClient)sender;
                STDiscovery stDisco = stDiscoRequest.lstSTDiscovery[stDiscoRequest.iDiscoRequete];
                string data = e.Result;
                dynamic jresults = JsonConvert.DeserializeObject(data);
                
                //GuidddmSoftwareInstance, NomddmProduit, Version, GuidddmHost, EOS, EOES

                string sCommand = "insert into ddm" + stDisco.sKindDisco;
                string sGuidTechno, sGuidProduit;
                if (jresults[0].count > 0)
                {
                    foreach (dynamic el in jresults[0].results)
                    {
                        if (oCnxBase.CBRecherche("select GuidddmProduit from ddmProduit where NomddmProduit = '" + el[1].Value + "'"))
                        { // guid existant
                            sGuidProduit = oCnxBase.Reader.GetString(0);
                            oCnxBase.Reader.Close();

                        }
                        else
                        {
                            oCnxBase.Reader.Close();
                            sGuidProduit = Guid.NewGuid().ToString();
                            oCnxBase.CBWrite("insert into ddmProduit (GuidddmProduit, NomddmProduit) Values ('" + sGuidProduit + "','" + el[1].Value + "')");
                        }

                        if (oCnxBase.CBRecherche("select GuidddmTechnoRef from ddmTechnoRef where NomddmTechnoRef = '" + el[1].Value + " " + el[2].Value + "'"))
                        { // guid existant
                            sGuidTechno = oCnxBase.Reader.GetString(0);
                            oCnxBase.Reader.Close();
                        }
                        else
                        {
                            oCnxBase.Reader.Close();
                            sGuidTechno = Guid.NewGuid().ToString();
                            oCnxBase.CBWrite("insert into ddmTechnoRef (GuidddmTechnoRef, NomddmTechnoRef, EOS, EOES, GuidddmProduit) Values ('" + sGuidTechno + "','" + el[1].Value + " " + el[2].Value + "','" + el[4].Value + "','" + el[5].Value + "','" + sGuidProduit + "')");
                        }

                        if (!oCnxBase.CBRecherche("select GuidddmSoftwareInstance from ddmSoftwareInstance where GuidddmSoftwareInstance = '" + el[0].Value + "'"))
                        { // guid non existant
                            oCnxBase.Reader.Close();
                            oCnxBase.CBWrite("insert into ddmSoftwareInstance (GuidddmSoftwareInstance, GuidddmTechnoRef, GuidddmHost) Values ('" + el[0].Value + "','" + sGuidTechno + "','" + el[3].Value + "')");
                        }
                        oCnxBase.Reader.Close();
                    }
                }
                else
                {
                    oCnxBase.CBWrite("insert into ddmErreur (GuidddmErreur, NomProcess, Value, Comment) Values ('" + Guid.NewGuid() + "', 'Import Techno Serveur Inconnu','" + webClient.Headers.Get("SearchValue") + "','pas de techno')");
                }

            }
        }

        public void webClient_GetGenericApiDiscovery(object sender, System.Net.UploadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }
            if (e.Result != null && e.Result.Length > 0)
            {
                System.Net.WebClient webClient = (System.Net.WebClient) sender;
                //string data = System.Text.Encoding.UTF8.GetString(e.Result);
                string data = e.Result;
                dynamic jsonResult = JsonConvert.DeserializeObject(data);

                STDiscovery stDisco = stDiscoRequest.lstSTDiscovery[stDiscoRequest.iDiscoRequete];

                string sCommandIns = "insert into ddm" + stDisco.sKindDisco;
                string sCommandUpd = "update ddm" + stDisco.sKindDisco + " set ";

                if (jsonResult[0].count > 0)
                {
                    foreach (dynamic el in jsonResult[0].results)
                    {
                        int idx = 0;
                        string sKey = "", sKeyValue = "";
                        string sAttributs = "", sValue = "";
                        string sSetUpd = "";


                        int iForeignAttr = stDisco.iMappingKey;
                        foreach (dynamic val in el)
                        {
                            if(idx==0)
                            {
                                sKey = (jsonResult[0].headings)[idx];
                                sKeyValue = val.Value;
                                sAttributs += "," + (jsonResult[0].headings)[idx++];
                                sValue += ",'" + val.Value + "'";
                            } else
                            {
                                sSetUpd += "," + (jsonResult[0].headings)[idx] + "= '" + val.Value + "'";
                                sAttributs += "," + (jsonResult[0].headings)[idx++];
                                sValue += ",'" + val.Value + "'";
                            }
                        }
                        if (stDiscoRequest.bMapping)
                        {
                            if (oCnxBase.CBRecherche("select GuidVLan from VLan where NomReseau='" + el[iForeignAttr].Value + "'"))
                            {
                                sAttributs += "," + "GuidVLan";
                                sValue += ",'" + oCnxBase.Reader.GetString(0) + "'";
                                sSetUpd += ", GuidVLan ='" + oCnxBase.Reader.GetString(0) + "'";
                            }
                            oCnxBase.CBReaderClose();
                        }
                        sAttributs += "," + "updatedisco";
                        sValue += ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                        sSetUpd += ", updatedisco ='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";

                        if (oCnxBase.CBRecherche("select " + sKey + " from ddm" + stDisco.sKindDisco + " where " + sKey + " = '" + sKeyValue + "'"))                        {
                            //update
                            oCnxBase.CBReaderClose();
                            oCnxBase.CBWrite(sCommandUpd + sSetUpd.Substring(1) + " where " + sKey + " = '" + sKeyValue + "'");
                        }
                        else
                        {
                            //create
                            oCnxBase.CBReaderClose();
                            oCnxBase.CBWrite(sCommandIns + " (" + sAttributs.Substring(1) + ") Values (" + sValue.Substring(1) + ")");

                        }
                    }
                }
                else
                {
                    oCnxBase.CBWrite("insert into ddmErreur (GuidddmErreur, NomProcess, Value, Comment) Values ('" + Guid.NewGuid() + "', '" + webClient.Headers.Get("KindSearch") + "','" + webClient.Headers.Get("SearchValue") + "','" + webClient.Headers.Get("Requete").Replace("'", "''") + "')");
                }

            }
        }

        public void webClient_GetApiDiscovery(object sender, System.Net.UploadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }
            if (e.Result != null && e.Result.Length > 0)
            {
                System.Net.WebClient webClient = (System.Net.WebClient)sender;
                //string data = System.Text.Encoding.UTF8.GetString(e.Result);
                string data = e.Result;
                dynamic jresults = JsonConvert.DeserializeObject(data);
                result_ApiDiscovery(sender, jresults);

            }
        }

        public void result_ApiDiscovery(object sender, dynamic jsonResult)
        {
            // jresults[0].kind, jresults[0].headings, jresults[0].results
            
            STDiscovery stDisco = stDiscoRequest.lstSTDiscovery[stDiscoRequest.iDiscoRequete];
            System.Net.WebClient webClient = (System.Net.WebClient)sender;

            string sCommand = "insert into ddm" + stDisco.sKindDisco;

            if (jsonResult[0].count > 0)
            {
                foreach (dynamic el in jsonResult[0].results)
                {
                    int idx = 0;
                    string sAttributs = "";
                    string sValue = "";
                    int iForeignAttr = stDisco.iMappingKey;
                    foreach (dynamic val in el)
                    {
                        sAttributs += "," + (jsonResult[0].headings)[idx++];
                        sValue += ",'" + val.Value + "'";
                    }
                    if (stDiscoRequest.bMapping)
                    {
                        if (oCnxBase.CBRecherche("select GuidVLan from VLan where NomReseau='" + el[iForeignAttr].Value + "'"))
                        {
                            sAttributs += "," + "GuidVLan";
                            sValue += ",'" + oCnxBase.Reader.GetString(0) + "'";
                        }
                        oCnxBase.CBReaderClose();
                    }
                    /*
                    if (oCnxBase.isDataTableContainColumn("updatedate", "ddm" + stDisco.sKindDisco))
                    {
                        sAttributs += "," + "updatedate";
                        sValue += ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                    }
                    */
                    oCnxBase.CBWrite(sCommand + " (" + sAttributs.Substring(1) + ") Values (" + sValue.Substring(1) + ")");
                }
            }
            else
            {
                oCnxBase.CBWrite("insert into ddmErreur (GuidddmErreur, NomProcess, Value, Comment) Values ('" + Guid.NewGuid() + "', '" + webClient.Headers.Get("KindSearch") + "','" + webClient.Headers.Get("SearchValue") + "','" + webClient.Headers.Get("Requete").Replace("'","''") + "')" );
            }
        }

        void tokenCloud_Click(object sender, EventArgs e)
        {
            using (var webClient = new System.Net.WebClient())
            {
                webClient.Encoding = System.Text.UTF8Encoding.UTF8;

                System.Net.NetworkCredential netCred = new System.Net.NetworkCredential();
                netCred.UserName = "140488";
                netCred.Password = "Leone!40";
                webClient.Credentials = netCred;
                System.Net.WebProxy wp = new System.Net.WebProxy("10.160.249.135:8080", true);
                wp.Credentials = netCred;
                //wp.Address = new Uri(@"http://ieconfig.sig.echonet/proxy.pac");
                webClient.Proxy = wp;

                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                webClient.Headers.Add("Accept", "application/json");

                webClient.QueryString.Add("grant_type", "urn:ibm:params:oauth:grant-type:apikey");
                webClient.QueryString.Add("apikey", "XlKCPnuV_JU39KCnfMUbBM3PocVu3AbL4ARuDuv01YVE");

                webClient.UploadValuesCompleted += webClient_GetTokenCloud;
                Uri urlToRequest = new Uri(@"https://iam.cloud.ibm.com/identity/token");
                webClient.UploadValuesAsync(urlToRequest, "POST", webClient.QueryString);
            }
            //throw new NotImplementedException();
        }

        void result_CloudToken(dynamic json)
        {
            string token = json.access_token;
            using (var webClient = new System.Net.WebClient())
            {
                webClient.Encoding = System.Text.UTF8Encoding.UTF8;

                System.Net.NetworkCredential netCred = new System.Net.NetworkCredential();
                netCred.UserName = "140488";
                netCred.Password = "Leone!40";
                webClient.Credentials = netCred;
                System.Net.WebProxy wp = new System.Net.WebProxy("10.160.249.135:8080", true);
                wp.Credentials = netCred;
                webClient.Proxy = wp;

                webClient.Headers.Add("Accept", "application/json");
                webClient.Headers.Add("Authorization", token);

                webClient.DownloadDataCompleted += webClient_GetClusters;
                Uri urlToRequest = new Uri(@"https://containers.cloud.ibm.com/global/v1/clusters");
                webClient.DownloadDataAsync(urlToRequest);
            }
        }
        void ServiceLink_Click(object sender, EventArgs e)
        {
            CommandServiceLink();
            //throw new NotImplementedException();
        }

        void Service_Click(object sender, EventArgs e)
        {
            CommandService();
            //throw new NotImplementedException();
        }

        void GroupService_Click(object sender, EventArgs e)
        {
            CommandGroupService();
            //throw new NotImplementedException();
        }

        void menuOptions_Click(object sender, EventArgs e)
        {
            CommandOptions();
            //throw new NotImplementedException();
        }


        void menuTadUpdating_Click(object sender, EventArgs e)
        {
            CommandTadUpgating();
        }

        void menuApplicationsExport_Click(object sender, EventArgs e)
        {
            CommandApplicationsExport();
        }

        void menuObsolescenceMap_Click(object sender, EventArgs e)
        {
            CommandObsolescenceMap();

            /*
            using (StreamWriter sfile = File.CreateText(@"c:\dat\temp\ServerPhy.csv"))
            {
                sfile.WriteLine("GuidServerPhy;NomServerPhy");
                string line;

                string[] aLib = { "NomServerPhy", "Description", "GuidLocation" };

                System.IO.StreamReader file = new System.IO.StreamReader(@"c:\dat\temp\ServerFR.csv");
                while ((line = file.ReadLine()) != null)
                {
                    string[] aEnreg = line.Split(';');
                    if (oCnxBase.CBRecherche("SELECT  GuidServerPhy, NomServerPhy FROM ServerPhy WHERE NomServerPhy='" + aEnreg[0] + "'"))
                    {
                        sfile.WriteLine(oCnxBase.Reader.GetString(0) + ";" + oCnxBase.Reader.GetString(1));
                    }
                    else
                    {
                        oCnxBase.CBReaderClose();
                        if (aEnreg[2].Length == 0)
                        {
                            oCnxBase.CBWrite("Insert Into ServerPhy (GuidServerPhy, NomServerPhy, Description) Values ('" + Guid.NewGuid() + "','" + aEnreg[0] + "','" + aEnreg[1] + "')");
                        }
                        else
                        {
                            oCnxBase.CBWrite("Insert Into ServerPhy (GuidServerPhy, NomServerPhy, Description, GuidLocation) Values ('" + Guid.NewGuid() + "','" + aEnreg[0] + "','" + aEnreg[1] + "','" + aEnreg[2] + "')");
                        }
                    }

                    
                    //if (oCnxBase.CBRecherche("SELECT  GuidTechnoRef FROM TechnoRef WHERE GuidTechnoRef='" + aEnreg[0] + "'"))
                    //{
                    //    oCnxBase.CBReaderClose();
                    //    for (int i = 1; i < 9; i++)
                    //    {
                    //        if (aEnreg[i] != "NULL")
                    //        {
                    //            DateTime dt = Convert.ToDateTime(aEnreg[i]); //DateTime.FromOADate(Convert.ToDouble(aEnreg[i]));
                    //            oCnxBase.CBWrite("UPDATE TechnoRef SET " + aLib[i] + "='" + dt.ToString("yyyy-MM-dd") + "' WHERE GuidTechnoRef = '" + aEnreg[0] + "'");
                    //        }
                    //    }
                    //}
                    oCnxBase.CBReaderClose();

                }

                file.Close();
            }
            */


            /*
            FileStream fstr = File.OpenRead("c:\\dat\\appli.csv");
            string line;

            string[] aLib = {"","UpComingStart","UpComingEnd","ReferenceStart","ReferenceEnd","ConfinedStart","ConfinedEnd","DecommStart","DecommEnd" };

            System.IO.StreamReader file = new System.IO.StreamReader(@"c:\dat\appli.csv");
            while ((line = file.ReadLine()) != null)
            {
                string[] aEnreg = line.Split(';');
                if (oCnxBase.CBRecherche("SELECT  GuidTechnoRef FROM TechnoRef WHERE GuidTechnoRef='" + aEnreg[0] + "'"))
                {
                    oCnxBase.CBReaderClose();
                    // UpComingStart	UpComingEnd	ReferenceStart	ReferenceEnd	ConfinedStart	ConfinedEnd	DecommStart	DecommEnd
                    for (int i = 1; i < 9; i++)
                    {
                        if (aEnreg[i] != "NULL")
                        {
                            DateTime dt = Convert.ToDateTime(aEnreg[i]); //DateTime.FromOADate(Convert.ToDouble(aEnreg[i]));
                            
                            oCnxBase.CBWrite("UPDATE TechnoRef SET " + aLib[i] + "='" + dt.ToString("yyyy-MM-dd") + "' WHERE GuidTechnoRef = '" + aEnreg[0] + "'");
                            //MessageBox.Show(aLib[i] + " :" + aEnreg[i]);
                        }
                    }
                }
                oCnxBase.CBReaderClose();
            }

            file.Close();
            */
        }

        void menuMiseAJourLibelles_Click(object sender, EventArgs e)
        {
            CommandMiseAJourLibelles();
        }

        void menuObjectExplorer_Click(object sender, EventArgs e)
        {
            CommandObjectExplorer();
            //throw new NotImplementedException();
        }

        void menuProvisionServer_Click(object sender, EventArgs e)
        {
            CommandProvisionServer();
            //throw new NotImplementedException();
        }
        void menuExportServer_Click(object sender, EventArgs e)
        {
            CommandProvisionServer();
            //throw new NotImplementedException();
        }

        void menuExport_Click(object sender, EventArgs e)
        {
            CommandExport();
            //throw new NotImplementedException();
        }

        void menuEtatDB_Click(object sender, EventArgs e)
        {
            CommandEtatDB();
            //throw new NotImplementedException();
        }

        void menuExportRefData_Click(object sender, EventArgs e)
        {
            CommandExportRefData();
            //throw new NotImplementedException();
        }
        void menuExportDB_Click(object sender, EventArgs e)
        {
            CommandExportDB();
            //throw new NotImplementedException();
        }

        void menuImportObj_Click(object sender, EventArgs e)
        {
            CommandImportObj();
            //throw new NotImplementedException();
        }

        void menuCopyField_Click(object sender, EventArgs e)
        {
            CommandCopyField();
            //throw new NotImplementedException();
        }

        void menuImport_Click(object sender, EventArgs e)
        {
            CommandImport();
            //throw new NotImplementedException();
        }

        void menuStatutApp_Click(object sender, EventArgs e)
        {
            CommandStatut();
            //throw new NotImplementedException();
        }

        void menuRefApp_Click(object sender, EventArgs e)
        {
            FormProduct fp = new FormProduct(this, 'A');
            fp.ShowDialog(this);
        }

        void menuRefTechnique_Click(object sender, EventArgs e)
        {
            FormProduct fp = new FormProduct(this, 'S');
            fp.ShowDialog(this);
        }

        private void menuItem21_Click(object sender, EventArgs e)
        {
            FormProduct fp = new FormProduct(this, 'H');
            fp.ShowDialog(this);
        }

        void menuExtractIP_Click(object sender, EventArgs e)
        {
            using (StreamWriter sfile = File.CreateText(sPathRoot + @"\ip.csv"))
            {
                if (oCnxBase.CBRecherche("SELECT DISTINCT IPAddr, NomServerPhy, NomApplication FROM NCard, ServerPhy, GServerPhy, DansVue, Vue, Application WHERE Vue.GuidApplication=Application.GuidApplication AND DansVue.GuidGVue=Vue.GuidGVue AND GServerPhy.GuidGServerPhy=DansVue.GuidObjet AND ServerPhy.GuidServerPhy=GServerPhy.GuidServerPhy AND GuidHote=ServerPhy.GuidServerPhy ORDER BY IPAddr"))
                {
                    sfile.WriteLine("adresse IP;Nom Serveur;Nom Application");
                    while (oCnxBase.Reader.Read())
                    {
                        if (!oCnxBase.Reader.IsDBNull(0))
                        {
                            sfile.WriteLine(oCnxBase.Reader.GetString(0) + ";" + oCnxBase.Reader.GetString(1) + ";" + oCnxBase.Reader.GetString(2));
                        }
                    }
                }
                oCnxBase.CBReaderClose();
            }
            //throw new NotImplementedException();
        }

        string GetItemRoot(TreeNode n)
        {
            if (n.Parent != null) return GetItemRoot(n.Parent);
            return n.Name;
        }

        public void ObjetLiesDevelop()
        {
            bDevelop[0] = true;
            tvObjet.Nodes[0].Nodes[3].Nodes.Add("030Kubernetes", "Kubernetes Infra");
            tvObjet.Nodes[0].Nodes[3].Nodes.Add("031ServicesCloud", "Services Cloud Infra");

            oCnxBase.CBAddNode("SELECT Distinct AppUser.GuidAppUser, NomAppUser FROM AppUser, Vue, DansVue, GTechUser WHERE Vue.GuidVue='" + sGuidVueInf + "' AND Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGTechUser And GTechUser.GuidAppUser=AppUser.GuidAppUser Order By NomAppUser", tvObjet.Nodes[0].Nodes[0].Nodes);
            oCnxBase.CBAddNode("SELECT Distinct Application.GuidApplication, NomApplication FROM Application, Vue, DansVue, GApplication WHERE Vue.GuidVue='" + sGuidVueInf + "' AND Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGApplication And GApplication.GuidApplication=Application.GuidApplication  Order By NomApplication", tvObjet.Nodes[0].Nodes[1].Nodes);
            oCnxBase.CBAddNode("SELECT Distinct Server.GuidServer, NomFonction FROM Server, Fonction, Vue, DansVue, GServer WHERE Vue.GuidVue='" + sGuidVueInf + "' AND Vue.GuidGVue=DansVue.GuidGVue AND Fonction.GuidFonction=Server.GuidMainFonction AND GuidObjet=GuidGServer And GServer.GuidServer=Server.GuidServer  Order By NomFonction", tvObjet.Nodes[0].Nodes[2].Nodes);
            oCnxBase.CBAddNode("SELECT Distinct Genks.GuidGenks, NomGenks FROM Genks, Vue, DansVue, GGenks WHERE Vue.GuidVue='" + sGuidVueInf + "' AND Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGGenks And GGenks.GuidGenks=Genks.GuidGenks  Order By NomGenks", tvObjet.Nodes[0].Nodes[3].Nodes[0].Nodes);
            oCnxBase.CBAddNode("SELECT Distinct Gensas.GuidGensas, NomGensas FROM Gensas, Vue, DansVue, GGensas WHERE Vue.GuidVue='" + sGuidVueInf + "' AND Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGGensas And GGensas.GuidGensas=Gensas.GuidGensas  Order By NomGensas", tvObjet.Nodes[0].Nodes[3].Nodes[1].Nodes);
            oCnxBase.CBAddNode("SELECT Distinct TechLink.GuidTechLink, NomTechLink FROM TechLink, Vue, DansVue, GTechLink WHERE Vue.GuidVue='" + sGuidVueInf + "' AND Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGTechLink And GTechLink.GuidTechLink=TechLink.GuidTechLink  Order By NomTechLink", tvObjet.Nodes[0].Nodes[4].Nodes);
        }

        void tvObjet_AfterSelect(object sender, TreeViewEventArgs e)
        {
#if WRITE
            if (tbTypeVue.Text != null && tbTypeVue.Text != "")
            //if (cbTypeVue.SelectedItem != null)
            {
                string sTypeVue = tbTypeVue.Text; // (string)cbTypeVue.SelectedItem;
                
                setCtrlEnabled(bAdd, false);


                switch (sTypeVue[0])
                {
                    case '0': //1-Fonctionnelle
                        if (e.Node.Parent != null)
                        {
                            switch (e.Node.Parent.Text[0])
                            {
                                case 'U': //User
                                    setCtrlEnabled(bAdd, true);
                                    break;
                                case 'A': //Application
                                    setCtrlEnabled(bAdd, true);
                                    break;
                            }
                        }

                        break;
                    case '1': // 1-Applicative
                        if (e.Node.Parent != null)
                        {
                            switch (e.Node.Parent.Text[0])
                            {
                                case 'U': //User
                                    setCtrlEnabled(bAdd, true);
                                    break;
                                case 'A': //Application
                                    setCtrlEnabled(bAdd, true);
                                    break;
                                case 'M': //Module
                                    setCtrlEnabled(bAdd, true);
                                    break;
                                case 'L': //Link
                                    setCtrlEnabled(bAdd, true);
                                    break;
                            }
                        }

                        break;
                    case '2': // 2-Infrastructure
                        if (e.Node.Parent != null)
                        {
                            if (e.Node.ForeColor != Color.Gray)
                            {
                                TreeNode tnRef = e.Node;
                                while (tnRef.Parent != null) tnRef = tnRef.Parent;
                                switch (tnRef.Name[0])
                                {
                                    case 'U': //User
                                    case 'F': //Fonction
                                    case '6': // 6ManagedService
                                    case '7': // 7Container
                                    case 'A': //Application
                                    case 'L': //LSApplication
                                    case 'C': //Composant
                                    case 'P': //Packages
                                    case 'T': //Technologie
                                    case 'b': //Pattern
                                        setCtrlEnabled(bAdd, true);
                                        break;
                                }
                                if (e.Node.Parent.Parent != null && e.Node.Parent.Parent.Name[0] == 'F') setCtrlEnabled(bAdd, true);
                            }
                        }
                        else
                        {
                            switch (e.Node.Name[0])
                            {
                                case '6': // 6ManagedService
                                case '7': // 7Container
                                    setCtrlEnabled(bAdd, true);
                                    break;
                            }
                        }
                        break;
                    case '6': // 6-Sites
                        if (e.Node.Parent != null)
                        {
                            switch (e.Node.Parent.Text[0])
                            {
                                case 'L': //Location
                                case 'U': //User
                                case 'A': //Application
                                case 'P': //Prod Server
                                case 'H': //Hors Prod Server
                                case 'Q': //Qualif Server
                                    setCtrlEnabled(bAdd, true);
                                    break;
                            }
                        }
                        break;
                    case '3': // 3-Production
                    case '5': // 5-Pre-Production
                    case '4': // 4-Hors Production
                    case 'F': // F-Service Infra

                        switch (GetItemRoot(e.Node)[0])
                        {
                            case '0': // objets infra
                                if (e.Node.Parent != null)
                                {
                                    if (e.Node.Parent.Parent != null)
                                    {
                                        switch (e.Node.Parent.Name[1])
                                        {
                                            case '3':
                                                if (e.Node.Parent.Parent.Parent != null) setCtrlEnabled(bAdd, true);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        if (!bDevelop[0] && sGuidVueInf != null) ObjetLiesDevelop();
                                    }
                                }
                                break;
                            case '1': // Objets App Externe à ajouter
                                if (e.Node.Parent != null)
                                {
                                    if (e.Node.Parent.Parent != null) setCtrlEnabled(bAdd, true);
                                    else
                                    {
                                        if (!bDevelop[1] && sGuidVueInf != null)
                                        {
                                            bDevelop[1] = true;
                                            oCnxBase.CBAddNode("SELECT DisTinct Cluster.GuidCluster, concat(NomCluster, '   [', NomApplication, ']') NomClusterLong From Cluster, ServerPhy, ApplicationLink, Application, GApplication, DansVue, Vue vInfra, Vue vDeployApp, Vue vDeploy Where Cluster.GuidCluster = ServerPhy.GuidCluster and ServerPhy.GuidServerPhy = ApplicationLink.GuidServerPhy and ApplicationLink.GuidApplication = Application.GuidApplication and Application.GuidApplication = GApplication.GuidApplication and GApplication.GuidGApplication = DansVue.GuidObjet and DansVue.GuidGVue = vINfra.GuidGVue and vInfra.GuidVue = vDeployApp.GuidVueinf and ApplicationLink.GuidVue = vDeploy.GuidVue and vDeploy.GuidEnvironnement = vDeployApp.GuidEnvironnement and vDeployApp.GuidVue = '" + GuidVue + "' ORDER BY NomClusterLong, Cluster.GuidCluster", tvObjet.Nodes[1].Nodes[0].Nodes);
                                            oCnxBase.CBAddNode("SELECT DisTinct ServerPhy.GuidServerphy, concat(NomServerPhy, '   [', NomApplication, ']') NomServerPhyLong From ServerPhy, ApplicationLink, Application, GApplication, DansVue, Vue vInfra, Vue vDeployApp, Vue vDeploy Where ServerPhy.GuidServerPhy = ApplicationLink.GuidServerPhy and ApplicationLink.GuidApplication = Application.GuidApplication and Application.GuidApplication = GApplication.GuidApplication and GApplication.GuidGApplication = DansVue.GuidObjet and DansVue.GuidGVue = vINfra.GuidGVue and vInfra.GuidVue = vDeployApp.GuidVueinf and ApplicationLink.GuidVue = vDeploy.GuidVue and vDeploy.GuidEnvironnement = vDeployApp.GuidEnvironnement and vDeployApp.GuidVue = '" + GuidVue + "' and GuidCluster is null ORDER BY NomServerPhyLong, ServerPhy.GuidServerPhy", tvObjet.Nodes[1].Nodes[1].Nodes);
                                        }
                                    }
                                }
                                break;
                            case '2': // Objets instancés
                                if (e.Node.Parent != null)
                                {
                                    if (e.Node.Parent.Parent != null)
                                    {
                                        switch (e.Node.Parent.Name[1])
                                        {
                                            case '4':
                                                if (e.Node.Parent.Parent.Parent != null) setCtrlEnabled(bAdd, true);
                                                break;
                                            default:  // 0: user, 1:  
                                                setCtrlEnabled(bAdd, true);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        if (!bDevelop[2] && sGuidVueInf != null)
                                        {
                                            bDevelop[2] = true;
                                            tvObjet.Nodes[2].Nodes[4].Nodes.Add("240InstanceServices", "Services Managés");
                                            tvObjet.Nodes[2].Nodes[4].Nodes.Add("241InstanceNS", "Kube - Name Space");
                                            tvObjet.Nodes[2].Nodes[4].Nodes.Add("242InstanceIngres", "Kube - Ingres");
                                            tvObjet.Nodes[2].Nodes[4].Nodes.Add("243InstanceSrvs", "Kube - Services");
                                            tvObjet.Nodes[2].Nodes[4].Nodes.Add("244InstancePods", "Kube - Pods");
                                            

                                            oCnxBase.CBAddNode("SELECT DISTINCT GuidCluster, NomCluster FROM Cluster, DansTypeVue, TypeVue WHERE NomTypeVue ='" + sTypeVue + "' AND DansTypeVue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidObjet=GuidCluster And (GuidClusterType != '74cc8591-f07f-4ae1-9287-050459112392' OR GuidClusterType is null) ORDER BY NomCluster", tvObjet.Nodes[2].Nodes[0].Nodes);
                                            oCnxBase.CBAddNode("SELECT DISTINCT GuidServerPhy, NomServerPhy FROM ServerPhy, DansTypeVue, TypeVue WHERE NomTypeVue ='" + sTypeVue + "' AND DansTypeVue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidObjet=GuidServerPhy ORDER BY NomServerPhy", tvObjet.Nodes[2].Nodes[1].Nodes);
                                            oCnxBase.CBAddNode("SELECT DISTINCT GuidVlan, NomVlan FROM Vlan, DansTypeVue, TypeVue WHERE NomTypeVue ='" + sTypeVue + "' AND DansTypeVue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidObjet=GuidVlan ORDER BY NomVlan", tvObjet.Nodes[2].Nodes[2].Nodes);
                                            oCnxBase.CBAddNode("SELECT DISTINCT GuidRouter, NomRouter FROM Router, DansTypeVue, TypeVue WHERE NomTypeVue ='" + sTypeVue + "' AND DansTypeVue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidObjet=GuidRouter ORDER BY NomRouter", tvObjet.Nodes[2].Nodes[3].Nodes);
                                            oCnxBase.CBAddNode("SELECT Distinct Inssas.GuidInssas, NomInssas FROM Inssas Order By NomInssas", tvObjet.Nodes[2].Nodes[4].Nodes[0].Nodes);
                                            oCnxBase.CBAddNode("SELECT Distinct Insns.GuidInsns, NomInsns FROM Insns, Insks, GInsks, Dansvue Where Dansvue.GuidObjet = GInsks.GuidGInsks and GInsks.GuidInsks = Insks.GuidInsks and Insks.GuidInsks = Insns.GuidInsks  Order By NomInsns", tvObjet.Nodes[2].Nodes[4].Nodes[1].Nodes);
                                            oCnxBase.CBAddNode("SELECT Distinct Insing.GuidInsing, NomInsing FROM Insing Order By NomInsing", tvObjet.Nodes[2].Nodes[4].Nodes[2].Nodes);
                                            oCnxBase.CBAddNode("SELECT Distinct Inssvc.GuidInssvc, NomInssvc FROM Inssvc Order By NomInssvc", tvObjet.Nodes[2].Nodes[4].Nodes[3].Nodes);
                                            oCnxBase.CBAddNode("SELECT Distinct Inspod.GuidInspod, NomInspod FROM Inspod Order By NomInspod", tvObjet.Nodes[2].Nodes[4].Nodes[4].Nodes);
                                            
                                            oCnxBase.CBAddNode("SELECT DISTINCT GuidCluster, NomCluster FROM Cluster WHERE GuidClusterType ='74cc8591-f07f-4ae1-9287-050459112392' ORDER BY NomCluster", tvObjet.Nodes[2].Nodes[5].Nodes);
                                        }
                                    }
                                }
                                break;
                            case '3': // objets serveur des différents sites
                                if (e.Node.Parent != null)
                                {
                                    switch (e.Node.Parent.Name[1])
                                    {
                                        case '0':
                                            setCtrlEnabled(bAdd, true);
                                            break;
                                    }
                                }
                                else
                                {
                                    if (!bDevelop[3] && sGuidVueInf != null)
                                    {
                                        bDevelop[3] = true;
                                        if (oCnxBase.CBRecherche("SELECT GuidLocation, NomLocation FROM Location ORDER BY NomLocation"))
                                        {
                                            ArrayList GuidL = new ArrayList(), NomL = new ArrayList();
                                            while (oCnxBase.Reader.Read()) { GuidL.Add(oCnxBase.Reader.GetString(0)); NomL.Add(oCnxBase.Reader.GetString(1)); }
                                            oCnxBase.CBReaderClose();
                                            for (int nbrL = 0; nbrL < GuidL.Count; nbrL++)
                                            {
                                                tvObjet.Nodes[3].Nodes.Add("30Server", "Servers " + (string)NomL[nbrL]);
                                                oCnxBase.CBAddNode("SELECT DISTINCT GuidServerPhy, NomServerPhy FROM ServerPhy WHERE GuidLocation='" + (string)GuidL[nbrL] + "' ORDER BY NomServerPhy", tvObjet.Nodes[3].Nodes[tvObjet.Nodes[3].Nodes.Count - 1].Nodes);
                                            }
                                        }
                                    }
                                    oCnxBase.CBReaderClose();
                                }
                                break;

                        }
                            break;
                    case '8': // 8-ZoningProd
                    case '7': // 7-ZoningHorsProd
                        if (e.Node.Parent != null)
                        {
                            switch (e.Node.Parent.Name[0])
                            {
                                case 'C': //ClusterVip
                                case 'M': //Serveurs Physique, Serveurs Hotes
                                case 'P': //Partition
                                case 'V': //Virtuel
                                case 'H': // Serveurs Hotes
                                case 'G': //Groupe
                                case 'B': //Baie
                                case 'L': //Lun
                                case 'Z': //Zone
                                    setCtrlEnabled(bAdd, true);
                                    break;
                            }
                        }
                        break;
                    case 'A': //A-SanProd
                    case '9': //9-SanHorsProd
                        if (e.Node.Parent != null)
                        {
                            switch (e.Node.Parent.Name[0])
                            {
                                case 'V': // VIO
                                case 'P': // Partitions
                                case 'M': // Machine
                                case 'B': //Baie de disques
                                    setCtrlEnabled(bAdd, true);
                                    break;
                            }
                        }
                        break;
                    case 'C': // C-CTIProd
                    case 'B': // B-CTIHorsProd
                        if (e.Node.Parent != null)
                        {
                            switch (e.Node.Parent.Name[0])
                            {
                                case 'M': //Serveurs Physiques
                                case 'H': //Serveurs Hotes
                                case 'D': //Baie de disques
                                case 'B': //Baie Physique
                                    setCtrlEnabled(bAdd, true);
                                    break;
                            }
                        }
                        break;
                    case 'Y': // Y-Cadre Ref
                        if (e.Node.Parent != null)
                        {
                            setCtrlEnabled(bAdd, true);
                        }
                        break;
                }

            }
            //throw new NotImplementedException();
#endif
        }

        public void CommandEACB()
        {
            AECBForm faecb = new AECBForm(this);
            faecb.init();
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Owner.SetStateOfControls();
        }

        public void CommandReport()
        {
            AECBForm faecb = new AECBForm(this);
            faecb.init();
        }

        public void RapPatrimoine(ControlWord cw, string sBookPat, int Titre)
        {
            ArrayList lstCadreRefGuid = new ArrayList();
            ArrayList lstCadreRefName = new ArrayList();
            ArrayList lstCadreRefVue = new ArrayList();
            ArrayList lstCadreRefNGuid = new ArrayList();
            ArrayList lstCadreRefNName = new ArrayList();
            ArrayList lstCadreRefNVue = new ArrayList();
            bool bCadreRefEnd = false;
            string sBook = "";

            if (cw.Exist(sBookPat) > -1)
            {
                DrawObject o = null;
                for (int j = 0; j < drawArea.GraphicsList.Count; j++)
                {
                    o = drawArea.GraphicsList[j];
                    if (o.GetType() == typeof(DrawCadreRefN) && o.LstChild.Count != 0)
                    {
                        int idx = 0;
                        bool find = false;
                        idx = drawArea.GraphicsList.FindObjetFromValue(idx, 0, o.GuidkeyObjet.ToString());
                        while (idx != -1)
                        {
                            DrawObject ofind = drawArea.GraphicsList[idx];
                            if (ofind.GetType() == typeof(DrawCadreRefN1)) { find = true; break; }
                            else idx = drawArea.GraphicsList.FindObjetFromValue(++idx, 0, o.GuidkeyObjet.ToString());
                        }
                        if (!find)
                        {
                            lstCadreRefGuid.Add(o.GuidkeyObjet.ToString());
                            lstCadreRefName.Add(o.Texte);
                            lstCadreRefVue.Add((string)o.GetValueFromName("NomVue"));
                        }
                        else
                        {
                            lstCadreRefNGuid.Add(o.GuidkeyObjet.ToString());
                            lstCadreRefNName.Add(o.Texte);
                            lstCadreRefNVue.Add((string)o.GetValueFromName("NomVue"));
                        }
                        //cw.InsertTexBookmark(sBookPat, false, o.Texte + "\n", "Titre 1");
                    } else if (o.GetType() == typeof(DrawCadreRefEnd)) bCadreRefEnd = true;
                }
                if (lstCadreRefName.Count == 1)
                {
                    sBook = "Pat" + lstCadreRefGuid[0].ToString().Replace("-", "");
                    cw.InsertTextFromId(sBookPat, false, (string)lstCadreRefName[0] + "\n", "Titre " + Titre);
                    cw.InsertTextFromId(sBookPat, false, "\n", null);
                    cw.CreatIdFromIdP(sBook, sBookPat);
                    //string sDiagram = SaveDiagram((string)cbVue.SelectedItem, GuidApplication.ToString(), "");
                    string sDiagram = SaveDiagram(lstCadreRefVue[0].ToString(), wkApp, "");
                    cw.InsertTextFromId(sBook, false, "\n", null);
                    cw.InsertImgFromId(sBook, false, sDiagram, null);
                    cw.InsertTextFromId(sBook, false, "\n", null);

                    if (!bCadreRefEnd)
                    {
                        for (int iCadre = 0; iCadre < lstCadreRefNVue.Count; iCadre++)
                        {
                            if (oCnxBase.CBRecherche("SELECT GuidVue, GuidGVue, NomTypeVue FROM Vue, TypeVue WHERE Vue.GuidTypeVue = TypeVue.GuidTypeVue and NomVue='" + lstCadreRefNVue[iCadre].ToString() + "'"))
                            {
                                oCnxBase.Reader.Read();
                                GuidVue = new Guid(oCnxBase.Reader.GetString(0));
                                GuidGVue = new Guid(oCnxBase.Reader.GetString(1));
                                sTypeVue = oCnxBase.Reader.GetString(2);
                                oCnxBase.CBReaderClose();
                                drawArea.GraphicsList.Clear();

                                //GuidApplication;
                                //cbTypeVue.SelectedItem;
                                LoadVue();
                                //cbVue.SelectedItem = (string)lstCadreRefNVue[iCadre];
                                RapPatrimoine(cw, sBook, Titre + 1);
                            }
                            else oCnxBase.CBReaderClose();
                        }
                    }
                }
                else
                {
                    sBook = "Pat" + "Global";
                    cw.InsertTextFromId(sBookPat, false, "Global Patrimoine" + "\n", "Titre " + Titre);
                    cw.InsertTextFromId(sBookPat, false, "\n", null);
                    cw.CreatIdFromIdP(sBook, sBookPat);
                    string sDiagram = SaveDiagram((string)cbVue.SelectedItem, wkApp, "");
                    cw.InsertTextFromId(sBook, false, "\n", null);
                    cw.InsertImgFromId(sBook, false, sDiagram, null);
                    cw.InsertTextFromId(sBook, false, "\n", null);
                    for (int iCadre = 0; iCadre < lstCadreRefVue.Count; iCadre++)
                    {
                        if (oCnxBase.CBRecherche("SELECT GuidVue, GuidGVue FROM Vue, TypeVue WHERE Vue.GuidTypeVue = TypeVue.GuidTypeVue and NomVue='" + lstCadreRefVue[iCadre].ToString() + "'"))
                        {
                            oCnxBase.Reader.Read();
                            GuidVue = new Guid(oCnxBase.Reader.GetString(0));
                            GuidGVue = new Guid(oCnxBase.Reader.GetString(1));
                            sTypeVue = oCnxBase.Reader.GetString(2);
                            oCnxBase.CBReaderClose();
                            drawArea.GraphicsList.Clear();

                            //GuidApplication;
                            //cbTypeVue.SelectedItem;
                            LoadVue();
                            //cbVue.SelectedItem = (string)lstCadreRefVue[iCadre];
                            RapPatrimoine(cw, sBook, Titre + 1);
                        }
                        else oCnxBase.CBReaderClose();
                    }
                    //cw.InsertTexBookmark(sBookPat, false, o.Texte + "\n", "Titre 1");
                }
            }
        }

        public int SearchFromArray(ArrayList lst, int idxSearchField, string sSearch)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                string[] row = (string[])lst[i];
                if (row[idxSearchField] == sSearch) return i;
            }
            return -1;
        }

        private AppList CommandSi1passe(ArrayList lstApplication, string sType, string sNomTypeVue)
        {
            string sTypeVue = tbTypeVue.Text;
            ArrayList LstVue = new ArrayList();
            AppList lstAppObj = new AppList();
            string Select = "", sCmd = "";

            for (int i = 0; i < lstApplication.Count; i++)
            {
                sCmd = "SELECT GuidVue, GuidGVue FROM Application, Vue, TypeVue WHERE Application.GuidAppVersion=Vue.GuidAppVersion AND Vue.GuidTypeVue=TypeVue.GuidTypeVue AND NomTypeVue='" + sNomTypeVue + "' AND Application.GuidApplication='" + ((string[])lstApplication[i])[0] + "'";
                if (oCnxBase.CBRecherche(sCmd))
                {
                    while (oCnxBase.Reader.Read())
                    {
                        string[] row = new string[5];
                        row[0] = ((string[])lstApplication[i])[0];
                        row[1] = ((string[])lstApplication[i])[1];
                        row[2] = ((string[])lstApplication[i])[2];
                        row[3] = oCnxBase.Reader.GetString(0);
                        row[4] = oCnxBase.Reader.GetString(1);
                        LstVue.Add(row);
                    }
                }
                oCnxBase.CBReaderClose();
            }
            for (int i = 0; i < LstVue.Count; i++)
            {
                string[] row = (string[])LstVue[i];
                string sLibIn = "GuidServerIn", sLibOut = "GuidServerOut";
                App a;
                int idx = lstAppObj.SearchApp(row[0]);
                if (idx < 0) { a = new App(row[0], row[1], Convert.ToInt32(row[2].Substring(2))); lstAppObj.Add(a); } else a = lstAppObj.GetAppObj(idx);
                GuidVue = new Guid(row[3]);
                GuidGVue = new Guid(row[4]);
                if (sTypeVue[0] == 'W')
                {
                    Select = drawArea.tools[(int)DrawArea.DrawToolType.TechLink].GetSelect(sType, "GVide");
                    sCmd = Select + " FROM DansVue, TechLink, GTechLink, GroupService WHERE GuidObjet=GuidGTechLink AND GTechLink.GuidTechLink=TechLink.GuidTechLink AND TechLink.GuidGroupService=GroupService.GuidGroupService AND GuidGVue='" + GuidGVue + "' AND NOT EXISTS (SELECT GuidAppUser FROM AppUser WHERE GuidAppUser=GuidServerIn)";
                }
                else
                {
                    Select = drawArea.tools[(int)DrawArea.DrawToolType.LinkA].GetSelect(sType, "GVide");
                    sCmd = Select + " FROM DansVue, Link, GLink WHERE GuidObjet=GuidGLink AND GLink.GuidLink=Link.GuidLink AND DansVue.GuidGVue='" + GuidGVue + "' AND NOT EXISTS (SELECT GuidAppUser FROM AppUser WHERE GuidAppUser=GuidComposantL1In)";
                    sLibIn = "GuidComposantL1In"; sLibOut = "GuidComposantL1Out";
                }

                if (oCnxBase.CBRecherche(sCmd))
                {

                    while (oCnxBase.Reader.Read())
                    {
                        ArrayList LstValue;
                        int n = oCnxBase.ConfDB.FindTable(sType);
                        if (n > -1)
                        {
                            Table t = (Table)oCnxBase.ConfDB.LstTable[n];
                            LstValue = t.InitValueFieldFromBD(oCnxBase.Reader, ConfDataBase.FieldOption.Select);
                            string sGuidIn = (string)LstValue[t.FindField(t.LstField, sLibIn)];
                            string sGuidOut = (string)LstValue[t.FindField(t.LstField, sLibOut)];
                            int iAppInValide = SearchFromArray(lstApplication, 0, sGuidIn);
                            int iAppOutValide = SearchFromArray(lstApplication, 0, sGuidOut);
                            if (iAppInValide < 0 || iAppOutValide < 0)
                            {
                                if (iAppInValide > -1)
                                {
                                    App aIn;
                                    int idxIn = lstAppObj.SearchApp(sGuidIn);
                                    if (idxIn < 0)
                                    {
                                        string sNomApp = "", sCriticiteMetier = "CL3";
                                        idxIn = SearchFromArray(lstApplication, 0, sGuidIn);
                                        if (idxIn > -1)
                                        {
                                            sNomApp = ((string[])lstApplication[idxIn])[1];
                                            sCriticiteMetier = ((string[])lstApplication[idxIn])[2];
                                        }
                                        aIn = new App(sGuidIn, sNomApp, Convert.ToInt32(sCriticiteMetier.Substring(2)));
                                        lstAppObj.Add(aIn);
                                        a.lstAppBefore.Add(aIn);
                                    }
                                    else {
                                        aIn = lstAppObj.GetAppObj(idxIn);
                                        if (a.lstAppBefore.IndexOf(aIn) == -1) a.lstAppBefore.Add(aIn);
                                    }
                                }
                                if (iAppOutValide > -1)
                                {
                                    App aOut;
                                    int idxOut = lstAppObj.SearchApp(sGuidOut);
                                    if (idxOut < 0)
                                    {
                                        string sNomApp = "", sCriticiteMetier = "0";
                                        idxOut = SearchFromArray(lstApplication, 0, sGuidOut);
                                        if (idxOut > -1)
                                        {
                                            sNomApp = ((string[])lstApplication[idxOut])[1];
                                            sCriticiteMetier = ((string[])lstApplication[idxOut])[2];
                                        }
                                        aOut = new App(sGuidOut, sNomApp, Convert.ToInt32(sCriticiteMetier.Substring(2)));
                                        lstAppObj.Add(aOut);
                                        a.lstAppAfter.Add(aOut);
                                    }
                                    else {
                                        aOut = lstAppObj.GetAppObj(idxOut);
                                        if (a.lstAppAfter.IndexOf(aOut) == -1) a.lstAppAfter.Add(aOut);
                                    }
                                }
                            }
                        }
                    }
                }
                oCnxBase.CBReaderClose();
            }
            lstAppObj.Propagation();
            // voir integration dans la f() CalcIndicateurInstance()
            // mise à jour indicateur Criticite & ImpactBusiness
            //suppression des deux indicateurs
            oCnxBase.CBWrite("Delete from IndicatorLink Where GuidIndicator='7bd86a23-ad30-4843-8e28-1220f5ef7224' or GuidIndicator='a821f90d-53ce-4e4e-9303-84b1d4730c8a'");
            //création des deux indicateurs

            for (int i = 0; i < lstAppObj.Count(); i++)
            {
                App a = lstAppObj.GetAppObj(i);

                oCnxBase.CBWrite("Insert Into IndicatorLink (GuidObjet, GuidIndicator, ValIndicator) Value('" + a.GuidApplication + "','7bd86a23-ad30-4843-8e28-1220f5ef7224'," + a.AjusterCriticiteMetier() + ")");
                oCnxBase.CBWrite("Insert Into IndicatorLink (GuidObjet, GuidIndicator, ValIndicator) Value('" + a.GuidApplication + "','a821f90d-53ce-4e4e-9303-84b1d4730c8a'," + a.Impact + ")");
            }

            return lstAppObj;
        }

        private void AfficheApp(AppList lstAppObj)
        {
            StdFile sf = new StdFile(this, @"C:\DAT\neo4j.txt");
            StdFile sfcsv = new StdFile(this, @"C:\DAT\appSI.csv");
            sf.SWwrite("create");
            sfcsv.SWwrite("GuidApplication;NomApplication;CriticiteI;Creticite;Impact");
            int nbr = 0;
            for (int i = 0; i < lstAppObj.Count(); i++)
            {
                App a = lstAppObj.GetAppObj(i);
                sfcsv.SWwrite(a.GuidApplication + ";" + a.NomApplication + ";" + a.CriticiteInitiale.ToString() + ";" + a.CriticiteMetier.ToString() + ";" + a.Impact.ToString());
                int ja = drawArea.GraphicsList.FindObjet(0, a.GuidApplication);
                DrawApplication da = null;
                if (ja == -1)
                {
                    drawArea.tools[(int)DrawArea.DrawToolType.Application].LoadSimpleObject(a.GuidApplication);
                    da = (DrawApplication)drawArea.GraphicsList[drawArea.GraphicsList.FindObjet(0, a.GuidApplication)];
                    da.rectangle.Y = nbr++ * (da.HEIGHTAPPLICATION + 3 * da.AXE);
                    da.rectangle.X = drawArea.GraphicsList.GetXMax(da.rectangle.Y) + 3 * da.AXE;
                    da.rectangle.Width = da.DrawGrpTxt(null, 1, 0, 0, 0, 0, Color.Black, Color.Transparent) + 4 * (da.WIDTHAPPLICATION + da.AXE);
                    da.rectangle.Height = da.HEIGHTAPPLICATION;
                    sf.SWwrite("(a" + a.GuidApplication.Replace("-", "") + ":Application{name:'" + da.GetValueFromName("NomApplication") + "'}),");
                }
                else da = (DrawApplication)drawArea.GraphicsList[ja];

                for (int ibefore = 0; ibefore < a.lstAppBefore.Count; ibefore++) {
                    App b = (App)a.lstAppBefore[ibefore];
                    int jb = drawArea.GraphicsList.FindObjet(0, b.GuidApplication);
                    DrawApplication db = null;
                    if (jb == -1)
                    {
                        drawArea.tools[(int)DrawArea.DrawToolType.Application].LoadSimpleObject(b.GuidApplication);
                        db = (DrawApplication)drawArea.GraphicsList[drawArea.GraphicsList.FindObjet(0, b.GuidApplication)];
                        db.rectangle.Y = nbr++ * (db.HEIGHTAPPLICATION + 3 * db.AXE);
                        db.rectangle.X = da.XMax() + drawArea.GraphicsList.GetXMax(db.rectangle.Y) + 3 * db.AXE;
                        //int Width = db.DrawGrpTxt(null, 1, 0, 0, 0, 0, Color.Black);
                        db.rectangle.Width = db.DrawGrpTxt(null, 1, 0, 0, 0, 0, Color.Black, Color.Transparent) + 4 * (db.WIDTHAPPLICATION + db.AXE);
                        db.rectangle.Height = db.HEIGHTAPPLICATION;
                        sf.SWwrite("(a" + b.GuidApplication.Replace("-", "") + ":Application{name:'" + db.GetValueFromName("NomApplication") + "'}),");
                    }
                    else db = (DrawApplication)drawArea.GraphicsList[jb];
                    //check Link
                    if (!CheckExistLink(db, da))
                    {
                        //Creation du link
                        drawArea.tools[(int)DrawArea.DrawToolType.LinkA].AddNewObjectFromDraw(drawArea, new DrawLink(drawArea.Owner, db, da, drawArea.GraphicsList.Count), true);
                        drawArea.GraphicsList.MoveLastToBack();
                        sf.SWwrite("(a" + b.GuidApplication.Replace("-", "") + ")-[:talk_to]->(a" + a.GuidApplication.Replace("-", "") + "),");
                    }
                }
                for (int iafter = 0; iafter < a.lstAppAfter.Count; iafter++)
                {
                    App b = (App)a.lstAppAfter[iafter];
                    int jb = drawArea.GraphicsList.FindObjet(0, b.GuidApplication);
                    DrawApplication db = null;
                    if (jb == -1)
                    {
                        drawArea.tools[(int)DrawArea.DrawToolType.Application].LoadSimpleObject(b.GuidApplication);
                        db = (DrawApplication)drawArea.GraphicsList[drawArea.GraphicsList.FindObjet(0, b.GuidApplication)];
                        db.rectangle.Y = nbr++ * (db.HEIGHTAPPLICATION + 3 * db.AXE);
                        db.rectangle.X = da.XMax() + drawArea.GraphicsList.GetXMax(db.rectangle.Y) + 3 * db.AXE;
                        //int Width = db.DrawGrpTxt(null, 1, 0, 0, 0, 0, Color.Black);
                        db.rectangle.Width = db.DrawGrpTxt(null, 1, 0, 0, 0, 0, Color.Black, Color.Transparent) + 4 * (db.WIDTHAPPLICATION + db.AXE);
                        db.rectangle.Height = db.HEIGHTAPPLICATION;
                        sf.SWwrite("(a" + b.GuidApplication.Replace("-", "") + ":Application{name:'" + db.GetValueFromName("NomApplication") + "'}),");
                    }
                    else db = (DrawApplication)drawArea.GraphicsList[jb];
                    //check Link
                    if (!CheckExistLink(da, db))
                    {
                        //Creation du link
                        drawArea.tools[(int)DrawArea.DrawToolType.LinkA].AddNewObjectFromDraw(drawArea, new DrawLink(drawArea.Owner, da, db, drawArea.GraphicsList.Count), true);
                        drawArea.GraphicsList.MoveLastToBack();
                        sf.SWwrite("(a" + a.GuidApplication.Replace("-", "") + ")-[:talk_to]->(a" + b.GuidApplication.Replace("-", "") + "),");
                    }

                }

                /*
                GuidVue = new Guid(row[2]);
                //if(olstApp.ExistApp(row[0])>-1) drawArea.tools[(int)DrawArea.DrawToolType.Application].LoadObject('A', row[0]);
                if (sTypeVue[0] == 'W')
                {
                    Select = drawArea.tools[(int)DrawArea.DrawToolType.TechLink].GetSelect(sType, "GVide");
                    sCmd = Select + " FROM DansVue, TechLink, GTechLink, GroupService WHERE GuidObjet=GuidGTechLink AND GTechLink.GuidTechLink=TechLink.GuidTechLink AND TechLink.GuidGroupService=GroupService.GuidGroupService AND GuidVue='" + row[2] + "' AND NOT EXISTS (SELECT GuidAppUser FROM USER WHERE GuidAppUser=GuidServerIn)";
                }
                else
                {
                    Select = drawArea.tools[(int)DrawArea.DrawToolType.LinkA].GetSelect(sType, "GVide");
                    sCmd = Select + " FROM DansVue, Link, GLink WHERE GuidObjet=GuidGLink AND GLink.GuidLink=Link.GuidLink AND GuidVue='" + row[2] + "' AND NOT EXISTS (SELECT GuidAppUser FROM USER WHERE GuidAppUser=GuidComposantL1In)";
                }

                if (oCnxBase.CBRecherche(sCmd))
                {
                    while (oCnxBase.Reader.Read())
                    {
                        ArrayList LstValue;
                        int n = oCnxBase.ConfDB.FindTable(sType);
                        if (n > -1)
                        {
                            Table t = (Table)oCnxBase.ConfDB.LstTable[n];
                            LstValue = t.InitValueFieldFromBD(oCnxBase.Reader, ConfDataBase.FieldOption.Select);
                            da.CreatAppLink(LstValue);
                        }

                    }
                }
                oCnxBase.CBReaderClose();

            */
            }
            sf.SWclose();
            sfcsv.SWclose();
        }

        private bool CheckExistLink(DrawApplication daIn, DrawApplication daOut)
        {
            for (int i = 0; i < drawArea.GraphicsList.Count; i++)
            {
                DrawObject o = drawArea.GraphicsList[i];
                if (o.GetType() == typeof(DrawLink))
                {
                    if ((string)o.GetValueFromName("GuidAppIn") == daIn.GuidkeyObjet.ToString() && (string)o.GetValueFromName("GuidAppOut") == daOut.GuidkeyObjet.ToString())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /*private void CommandSi1passe(ListeApp olstApp, string sType, string sNomTypeVue)
        {
            string sTypeVue = tbTypeVue.Text;
            ArrayList LstEnreg = new ArrayList();
            string Select = "", sCmd = "";

            for (int i = 0; i < olstApp.lstApp.Count; i++)
            {
                sCmd = "SELECT Application.GuidApplication, NomApplication, GuidVue FROM Application, Vue, TypeVue WHERE Application.GuidApplication=Vue.GuidApplication AND Vue.GuidTypeVue=TypeVue.GuidTypeVue AND NomTypeVue='" + sNomTypeVue + "' AND Application.GuidApplication='" + olstApp.lstApp[i] + "' And Application.Installee=1";
                if (oCnxBase.CBRecherche(sCmd))
                {
                    while (oCnxBase.Reader.Read())
                    {
                        string[] row = new string[3];
                        row[0] = oCnxBase.Reader.GetString(0);
                        row[1] = oCnxBase.Reader.GetString(1);
                        row[2] = oCnxBase.Reader.GetString(2);
                        LstEnreg.Add(row);
                    }
                }
                oCnxBase.CBReaderClose();
                
                // ce code rajoute les links des applications directement connectées 
                sCmd = "SELECT app.GuidApplication, app.NomApplication, Vue.GuidVue FROM Application app, Vue, TypeVue, DansVue, GApplication, Application apps WHERE app.GuidApplication=Vue.GuidApplication AND Vue.GuidTypeVue=TypeVue.GuidTypeVue AND Vue.GuidVue=DansVue.GuidVue AND DansVue.GuidObjet=GApplication.GuidGApplication AND GApplication.GuidApplication=apps.GuidApplication AND NomTypeVue='" + sNomTypeVue + "' AND apps.GuidApplication='" + olstApp.lstApp[i] + "' And App.Installee=1";
                if (oCnxBase.CBRecherche(sCmd))
                {
                    while (oCnxBase.Reader.Read())
                    {
                        string[] row = new string[3];
                        row[0] = oCnxBase.Reader.GetString(0);
                        row[1] = oCnxBase.Reader.GetString(1);
                        row[2] = oCnxBase.Reader.GetString(2);
                        LstEnreg.Add(row);
                    }
                }
                oCnxBase.CBReaderClose();
            }

            int nbr = 0;
            for (int i = 0; i < LstEnreg.Count; i++)
            {
                string[] row = (string[])LstEnreg[i];
                int j = drawArea.GraphicsList.FindObjet(0, row[0]);
                DrawApplication da = null;
                if (j == -1)
                {
                    drawArea.tools[(int)DrawArea.DrawToolType.Application].LoadSimpleObject(row[0]);
                    da = (DrawApplication)drawArea.GraphicsList[drawArea.GraphicsList.FindObjet(0, row[0])];
                    da.rectangle.Y = nbr++ * (da.HEIGHTAPPLICATION + 3 * da.AXE);
                    da.rectangle.X = drawArea.GraphicsList.GetXMax(da.rectangle.Y) + 3 * da.AXE;
                    //int Width = da.DrawGrpTxt(null, 1, 0, 0, 0, 0, Color.Black);
                    da.rectangle.Width = da.DrawGrpTxt(null, 1, 0, 0, 0, 0, Color.Black, Color.Transparent) + 4 * (da.WIDTHAPPLICATION + da.AXE);
                    da.rectangle.Height = da.HEIGHTAPPLICATION;
                }
                else da = (DrawApplication)drawArea.GraphicsList[j];

                GuidVue = new Guid(row[2]);
                //if(olstApp.ExistApp(row[0])>-1) drawArea.tools[(int)DrawArea.DrawToolType.Application].LoadObject('A', row[0]);
                if (sTypeVue[0] == 'W')
                {
                    Select = drawArea.tools[(int)DrawArea.DrawToolType.TechLink].GetSelect(sType, "GVide");
                    sCmd = Select + " FROM DansVue, TechLink, GTechLink, GroupService WHERE GuidObjet=GuidGTechLink AND GTechLink.GuidTechLink=TechLink.GuidTechLink AND TechLink.GuidGroupService=GroupService.GuidGroupService AND GuidVue='" + row[2] + "' AND NOT EXISTS (SELECT GuidAppUser FROM USER WHERE GuidAppUser=GuidServerIn)";
                }
                else
                {
                    Select = drawArea.tools[(int)DrawArea.DrawToolType.LinkA].GetSelect(sType, "GVide");
                    sCmd = Select + " FROM DansVue, Link, GLink WHERE GuidObjet=GuidGLink AND GLink.GuidLink=Link.GuidLink AND GuidVue='" + row[2] + "' AND NOT EXISTS (SELECT GuidAppUser FROM USER WHERE GuidAppUser=GuidComposantL1In)";
                }

                if (oCnxBase.CBRecherche(sCmd))
                {
                    while (oCnxBase.Reader.Read())
                    {
                        ArrayList LstValue;
                        int n = oCnxBase.ConfDB.FindTable(sType);
                        if (n > -1)
                        {
                            Table t = (Table)oCnxBase.ConfDB.LstTable[n];
                            LstValue = t.InitValueFieldFromBD(oCnxBase.Reader, ConfDataBase.FieldOption.Select);
                            da.CreatAppLink(LstValue);
                        }

                    }
                }
                oCnxBase.CBReaderClose();
            }
        }*/

        public void CommandSi()
        {
            ArrayList lstApp = new ArrayList();

            if (oCnxBase.CBRecherche("SELECT GuidApplication, NomApplication, Description FROM Application left join ApplicationClass on Application.GuidApplicationClass=ApplicationClass.GuidApplicationClass"))
            {
                while (oCnxBase.Reader.Read())
                {
                    //lstApp.Add(oCnxBase.Reader.GetString(0));
                    string[] row = new string[3];
                    row[0] = oCnxBase.Reader.GetString(0);
                    row[1] = oCnxBase.Reader.GetString(1);
                    if (oCnxBase.Reader.IsDBNull(2)) row[2] = "CL3"; else row[2] = oCnxBase.Reader.GetString(2);
                    lstApp.Add(row);
                }
            }
            oCnxBase.CBReaderClose();

            string sTypeVue = tbTypeVue.Text;
            string sType = "Link", sNomTypeVue = "1-Applicative";

            if (sTypeVue[0] == 'W')
            {
                sType = "TechLink"; sNomTypeVue = "2-Infrastructure";
            }
            AfficheApp(CommandSi1passe(lstApp, sType, sNomTypeVue));
            //if (i == 8) i = LstEnreg.Count;
            //"SELECT Application.GuidApplication, Application.NomApplication, Application.Statut, Application.Type, Application.Image, Application.GuidCadreRef, Application.GuidCode, NomArborescence, GApplication.X, GApplication.Y, GApplication.Width, GApplication.Height From DansVue, Application, GApplication, Arborescence WHERE GuidVue ='2949eb62-400c-4e4a-95de-e60dc0a06dda' and GuidObjet=GuidGApplication and GApplication.GuidApplication=Application.GuidApplication AND Application.GuidCode=Arborescence.GuidCode"
            drawArea.Refresh();
        }

        /*public void CommandSi()
        {
            ListeApp olstApp = new ListeApp();
            FormSI fSi = new FormSI(this, olstApp);
            fSi.ShowDialog(this);

            string sTypeVue = tbTypeVue.Text;
            string sType = "Link", sNomTypeVue = "1-Applicative";

            if (sTypeVue[0] == 'W')
            {
                sType = "TechLink"; sNomTypeVue = "2-Infrastructure";
            }
            CommandSi1passe(olstApp, sType, sNomTypeVue);
            //if (i == 8) i = LstEnreg.Count;
            //"SELECT Application.GuidApplication, Application.NomApplication, Application.Statut, Application.Type, Application.Image, Application.GuidCadreRef, Application.GuidCode, NomArborescence, GApplication.X, GApplication.Y, GApplication.Width, GApplication.Height From DansVue, Application, GApplication, Arborescence WHERE GuidVue ='2949eb62-400c-4e4a-95de-e60dc0a06dda' and GuidObjet=GuidGApplication and GApplication.GuidApplication=Application.GuidApplication AND Application.GuidCode=Arborescence.GuidCode"
            drawArea.Refresh();
        }*/

        public XmlExcel xmlCreatFluxFonc(string sGuidVueFonc, string sGuidVueApp)
        {
            //  <FluxFoncs>
            //     <FluxFonc Id Guid Nom NomSource NomCible>
            //        <FluxApps>
            //           <FluxApp Id Guid Nom>

            XmlExcel xmlFlux = new XmlExcel(this, "FluxFoncs");
            if (sGuidVueFonc == null || sGuidVueApp == null || sGuidVueFonc == "" || sGuidVueApp == "") return xmlFlux;

            if (oCnxBase.CBRecherche("Select ff.id, ff.GuidLink, ff.NomLink, ff.GuidModuleIn, ff.GuidModuleOut, fa.Id, fa.GuidLink, fa.NomLink from Link ff left join AppLinkFonc on ff.GuidLink=AppLinkFonc.GuidFoncLink and AppLinkFonc.GuidVue='" + sGuidVueApp + "' left join Link fa on AppLinkFonc.GuidAppLink=fa.GuidLink, GLink, DansVue, Vue Where ff.GuidLink=GLink.GuidLink and GLink.GuidGLink=DansVue.GuidObjet and DansVue.GuidGVue=Vue.GuidGVue and Vue.GuidVue='" + sGuidVueFonc + "' order by ff.Id, ff.NomLink, num"))
            {
                string sGuidLinkFonc = null;
                XmlElement elFluxFonc = null, elFluxApps = null;
                while (oCnxBase.Reader.Read())
                {
                    if (oCnxBase.Reader.GetString(1) != sGuidLinkFonc)
                    {
                        sGuidLinkFonc = oCnxBase.Reader.GetString(1);
                        elFluxFonc = xmlFlux.XmlCreatEl(xmlFlux.root, "FluxFonc");
                        elFluxApps = xmlFlux.XmlCreatEl(elFluxFonc, "FluxApps");
                        if (oCnxBase.Reader.IsDBNull(0)) elFluxFonc.SetAttribute("Id", ""); else elFluxFonc.SetAttribute("Id", oCnxBase.Reader.GetString(0));
                        elFluxFonc.SetAttribute("Guid", oCnxBase.Reader.GetString(1)); elFluxFonc.SetAttribute("Nom", oCnxBase.Reader.GetString(2));
                        elFluxFonc.SetAttribute("NomSource", oCnxBase.Reader.GetString(3)); elFluxFonc.SetAttribute("NomCible", oCnxBase.Reader.GetString(4));
                    }
                    if (!oCnxBase.Reader.IsDBNull(6))
                    {
                        XmlElement elFluxApp = xmlFlux.XmlCreatEl(elFluxApps, "FluxApp");
                        if (oCnxBase.Reader.IsDBNull(5)) elFluxApp.SetAttribute("Id", ""); else elFluxApp.SetAttribute("Id", oCnxBase.Reader.GetString(5));
                        elFluxApp.SetAttribute("Guid", oCnxBase.Reader.GetString(6)); elFluxApp.SetAttribute("Nom", oCnxBase.Reader.GetString(7));
                    }
                }
            }
            oCnxBase.CBReaderClose();
            return xmlFlux;
        }

        public XmlExcel xmlCreatFluxApp(string sGuidVueApp, string sGuidVueInf)
        {
            XmlExcel xmlFlux = new XmlExcel(this, "FluxApps");

            //  <FluxApps>
            //     <FluxApp Id Guid Nom NomSource NomCible>
            //        <FluxTechs>
            //           <FluxTech Id Guid Nom NomSouce NomCible>
            //              <MainComposantSources>
            //                 <MainComposant GuidMain>
            //                    <Packages>
            //                       <Package GuidPackage>
            //                          <Environnements>
            //                             <Environnement NomEnv>
            //                                <Serveurs>
            //                                <Parametres>
            //              <MainComposantCibles>
            //           <FluxTech/>
            //        <FluxTechs/>


            if (sGuidVueApp == "") return xmlFlux;
            string sql;
            // (0)link.id, (1)link.GuidLink, (2)NomLink, (3)GuidComposantL1In,  (4)NomComposantL1In, (5)GuidComposantL1Out, (6)NomComposantL1Out
            // (7)techlinka.Id, (8)techlinka.GuidTechLink, (9)techlinka.NomTechLink, (10)NomSource, (11)NomCible
            // (12)GuidMainSource, (13)GuidMainCible
            // (14)GuidPkgSource, (15)GuidPkgCible
            sql = " select link.id, link.GuidLink, NomLink, GuidComposantL1In, NomComposantL1In, GuidComposantL1Out, NomComposantL1Out, techlinka.Id, techlinka.GuidTechLink, techlinka.NomTechLink, NomSource, NomCible, GuidMainSource, GuidMainCible, GuidPkgSource, GuidPkgCible ";
            sql += "from glink, dansvue, vue , link left join";
            sql += "     ( select techlink.GuidTechLink, Id, num, NomTechLink, GuidLink, NomSource, GuidMainSource, GuidPkgSource, NomCible, GuidMainCible, GuidPkgCible";
            sql += "       from gtechlink, dansvue, vue, techlink left join techlinkapp on techlink.GuidTechLink = techlinkapp.GuidTechLink,";
            sql += "            ( select GuidApplication GuidSource, NomApplication NomSource, null GuidMainSource, null GuidPkgSource from application";
            sql += "              union select server.GuidServer GuidSource, NomServer NomSource, mcompapp.GuidMainComposant GuidMainSource, mcompserv.GuidMCompServ GuidPkgSource";
            sql += "                    from Server left join mcompapp on server.GuidServer = mcompapp.GuidServer left join mcompserv on mcompapp.GuidMainComposant = mcompserv.GuidMainComposant";
            sql += "              union select GuidAppUser GuidSource, NomAppUser NomSource, null GuidMainSource, null GuidPkgSource from appuser";
            sql += "              order by GuidSource) source,";
            sql += "            ( select GuidApplication GuidCible, NomApplication NomCible, null GuidMainCible, null GuidPkgCible from application";
            sql += "              union select server.GuidServer GuidCible, NomServer NomCible, mcompapp.GuidMainComposant GuidMainCible, mcompserv.GuidMCompServ GuidPkgCible";
            sql += "                    from Server left join mcompapp on server.GuidServer = mcompapp.GuidServer left join mcompserv on mcompapp.GuidMainComposant = mcompserv.GuidMainComposant";
            sql += "              union select GuidAppUser GuidCible, NomAppUser NomCible, null GuidMainCible, null GuidPkgCible from appuser";
            sql += "              order by GuidCible) cible";
            sql += "       where techlink.GuidTechLink = gtechlink.GuidTechLink and gtechlink.GuidGTechLink = dansvue.GuidObjet and";
            sql += "             dansvue.GuidGVue = vue.GuidGVue and vue.GuidVue = '" + sGuidVueInf + "' and";
            sql += "             techlink.GuidServerIn = source.GuidSource and techlink.GuidServerOut = cible.GuidCible";
            sql += "     ) techlinka on link.GuidLink = techlinka.GuidLink ";
            sql += "where link.GuidLink = glink.GuidLink and glink.GuidGLink = dansvue.GuidObjet and dansvue.GuidGVue = vue.GuidGVue and";
            sql += "      GuidVue = '" + sGuidVueApp + "' order by link.Id, num";

            if (oCnxBase.CBRecherche(sql))
            {
                while (oCnxBase.Reader.Read())
                {
                    XmlElement elFluxApp, elFluxTechs, elFluxTech, elMainComposantSources, elMainComposantCibles, elMainComposantSource, elMainComposantCible;
                    XmlElement elPackageSources, elPackageCibles, elPackageSource, elPackageCible;

                    elFluxApp = xmlFlux.XmlFindElFromAtt(xmlFlux.root, "Guid", oCnxBase.Reader.GetString(1), 1);
                    if (elFluxApp == null)
                    {
                        elFluxApp = xmlFlux.XmlCreatEl(xmlFlux.root, "FluxApp");
                        if (oCnxBase.Reader.IsDBNull(0)) elFluxApp.SetAttribute("Id", ""); else elFluxApp.SetAttribute("Id", oCnxBase.Reader.GetString(0));
                        elFluxApp.SetAttribute("Guid", oCnxBase.Reader.GetString(1)); elFluxApp.SetAttribute("Nom", oCnxBase.Reader.GetString(2));
                        elFluxTechs = xmlFlux.XmlCreatEl(elFluxApp, "FluxTechs");
                        try
                        {
                            elFluxApp.SetAttribute("GuidSource", oCnxBase.Reader.GetString(3)); elFluxApp.SetAttribute("NomSource", oCnxBase.Reader.GetString(4));
                            elFluxApp.SetAttribute("GuidCible", oCnxBase.Reader.GetString(5)); elFluxApp.SetAttribute("NomCible", oCnxBase.Reader.GetString(6));
                            
                        }
                        catch (InvalidCastException ex)
                        {
                            MessageBox.Show("Erreur - les propriétés du lien applicatif : " + oCnxBase.Reader.GetString(1) + " - " + oCnxBase.Reader.GetString(2) + " ne sont pas correctement remplies");
                            HandleRegistryException(ex);
                        }
                    }
                    else elFluxTechs = xmlFlux.XmlGetFirstElFromName(elFluxApp, "FluxTechs");

                    if (!oCnxBase.Reader.IsDBNull(8)) {
                        elFluxTech = xmlFlux.XmlFindElFromAtt(elFluxTechs, "Guid", oCnxBase.Reader.GetString(8), 1);
                        if (elFluxTech == null)
                        {
                            elFluxTech = xmlFlux.XmlCreatEl(elFluxTechs, "FluxTech");
                            if (oCnxBase.Reader.IsDBNull(7)) elFluxTech.SetAttribute("Id", ""); else elFluxTech.SetAttribute("Id", oCnxBase.Reader.GetString(7));
                            elFluxTech.SetAttribute("Guid", oCnxBase.Reader.GetString(8));
                            elFluxTech.SetAttribute("Nom", oCnxBase.Reader.GetString(9));
                            elFluxTech.SetAttribute("NomSource", oCnxBase.Reader.GetString(10));
                            elFluxTech.SetAttribute("NomCible", oCnxBase.Reader.GetString(11));
                            elMainComposantSources = xmlFlux.XmlCreatEl(elFluxTech, "MainComposantSources");
                            elMainComposantCibles = xmlFlux.XmlCreatEl(elFluxTech, "MainComposantCibles");
                        }
                        else
                        {
                            elMainComposantSources = xmlFlux.XmlGetFirstElFromName(elFluxTech, "MainComposantSources");
                            elMainComposantCibles = xmlFlux.XmlGetFirstElFromName(elFluxTech, "MainComposantCibles");
                        }

                        if (!oCnxBase.Reader.IsDBNull(12))
                        {
                            elMainComposantSource = xmlFlux.XmlFindElFromAtt(elMainComposantSources, "Guid", oCnxBase.Reader.GetString(12), 1);
                            if (elMainComposantSource == null)
                            {
                                elMainComposantSource = xmlFlux.XmlCreatEl(elMainComposantSources, "MainComposantSource");
                                elMainComposantSource.SetAttribute("Guid", oCnxBase.Reader.GetString(12));
                                elPackageSources = xmlFlux.XmlCreatEl(elMainComposantSource, "PackageSources");
                            }
                            else elPackageSources = xmlFlux.XmlGetFirstElFromName(elMainComposantSource, "PackageSources");
                            if (!oCnxBase.Reader.IsDBNull(14))
                            {
                                elPackageSource = xmlFlux.XmlFindElFromAtt(elPackageSources, "Guid", oCnxBase.Reader.GetString(14), 1);
                                if (elPackageSource == null)
                                {
                                    elPackageSource = xmlFlux.XmlCreatEl(elPackageSources, "PackageSource");
                                    elPackageSource.SetAttribute("Guid", oCnxBase.Reader.GetString(14));
                                }
                            }
                        }
                        if (!oCnxBase.Reader.IsDBNull(13))
                        {
                            elMainComposantCible = xmlFlux.XmlFindElFromAtt(elMainComposantCibles, "Guid", oCnxBase.Reader.GetString(13), 1);
                            if (elMainComposantCible == null)
                            {
                                elMainComposantCible = xmlFlux.XmlCreatEl(elMainComposantCibles, "MainComposantCible");
                                elMainComposantCible.SetAttribute("Guid", oCnxBase.Reader.GetString(13));
                                elPackageCibles = xmlFlux.XmlCreatEl(elMainComposantCible, "PackageCibles");
                            }
                            else elPackageCibles = xmlFlux.XmlGetFirstElFromName(elMainComposantCible, "PackageCibles");
                            if (!oCnxBase.Reader.IsDBNull(15))
                            {
                                elPackageCible = xmlFlux.XmlFindElFromAtt(elPackageCibles, "Guid", oCnxBase.Reader.GetString(15), 1);
                                if (elPackageCible == null)
                                {
                                    elPackageCible = xmlFlux.XmlCreatEl(elPackageCibles, "PackageCible");
                                    elPackageCible.SetAttribute("Guid", oCnxBase.Reader.GetString(15));
                                }
                            }
                        }
                    }
                }
            }
            oCnxBase.CBReaderClose();

            return xmlFlux;
        }

        public XmlExcel xmlCreatFluxApp(string sGuidVueApp)
        {
            XmlExcel xmlFlux = new XmlExcel(this, "FluxApps");
            //        <FluxApps>
            //           <FluxApp Id Guid Nom>
            //           <FluxApp/>
            //        <FluxApps/>

            if (oCnxBase.CBRecherche("select id, Link.GuidLink, NomLink from Link, GLink, DansVue, Vue Where Link.GuidLink=GLink.GuidLink and GLink.GuidGLink=DansVue.GuidObjet and DansVue.GuidGVue=Vue.GuidGVue and Vue.GuidVue='" + sGuidVueApp + "' order by id, NomLink"))
            {
                while (oCnxBase.Reader.Read())
                {
                    XmlElement elFluxApp = xmlFlux.XmlCreatEl(xmlFlux.root, "FluxApp");
                    if (oCnxBase.Reader.IsDBNull(0)) elFluxApp.SetAttribute("Id", ""); else elFluxApp.SetAttribute("Id", oCnxBase.Reader.GetString(0));
                    elFluxApp.SetAttribute("Guid", oCnxBase.Reader.GetString(1)); elFluxApp.SetAttribute("Nom", oCnxBase.Reader.GetString(2));
                }
            }
            oCnxBase.CBReaderClose();

            return xmlFlux;
        }

        public XmlExcel xmlCreatFluxInfra(string sGuidVueInfra)
        {
            XmlExcel xmlFlux = new XmlExcel(this, "FluxTechs");
            //        <FluxTechs>
            //           <FluxTech Id Guid Nom NomSouce NomCible>
            //           <FluxTech/>
            //        <FluxTechs/>


            if (sGuidVueInfra == "") return xmlFlux;
            string sql;
            // (0)link.id, (1)link.GuidLink, (2)NomLink, (3)GuidComposantL1In,  (4)NomComposantL1In, (5)GuidComposantL1Out, (6)NomComposantL1Out
            // (7)techlinka.Id, (8)techlinka.GuidTechLink, (9)techlinka.NomTechLink, (10)NomSource, (11)NomCible
            // (12)GuidMainSource, (13)GuidMainCible
            // (14)GuidPkgSource, (15)GuidPkgCible
            sql = " select techlink.id, techlink.Guidtechlink, Nomtechlink ";
            sql += "from gtechlink, dansvue, vue , techlink ";
            sql += "where techlink.Guidtechlink = gtechlink.Guidtechlink and gtechlink.GuidGtechlink = dansvue.guidobjet and dansvue.guidgvue = vue.guidgvue and";
            sql += "      GuidVue = '" + sGuidVueInfra + "' order by techlink.Id, NomTechLink";

            if (oCnxBase.CBRecherche(sql))
            {
                while (oCnxBase.Reader.Read())
                {
                    XmlElement elFluxTech;

                    elFluxTech = xmlFlux.XmlCreatEl(xmlFlux.root, "FluxTech");
                    if (oCnxBase.Reader.IsDBNull(0)) elFluxTech.SetAttribute("Id", ""); else elFluxTech.SetAttribute("Id", oCnxBase.Reader.GetString(0));
                    elFluxTech.SetAttribute("Guid", oCnxBase.Reader.GetString(1)); elFluxTech.SetAttribute("Nom", oCnxBase.Reader.GetString(2));
                }
            }
            oCnxBase.CBReaderClose();

            return xmlFlux;
        }

        public XmlExcel XmlCreatFlux(string sGuidVueInf, string sGuidVueDeploy)
        {
            XmlExcel xmlFlux = new XmlExcel(this, "Flux");

            // <Flux Guid Nom>
            //  <Origine Guid Type> 
            //      <User/Application/Server/Ing/Svc/Pod Guid Selected>
            //          <NCard Guid Selected>
            //              <VLan Guid >
            //                  <VlanClass Guid >
            //              <Alias Selected>
            //          <LabelClass Guid>
            //              <LabelValue Guid>
            //    <Cible Guid Type>
            //       <User/Application/Server/Ing/Svc Selected>
            //          <NCard Guid Selected>
            //              <VLan Guid>
            //                  <VlanClass Guid>
            //             <Alias Selected>
            //          <LabelClass Guid>
            //              <LabelValue Guid>
            //    <GroupService Guid Nom>
            //      <Service Guid Nom>
            // <Cluster>
            //    <Server>
            // <DelLinkOut>
            // <DelLinkIn>

            if (sGuidVueDeploy != "" && sGuidVueInf != "")
            {
                XmlElement elFluxApps = xmlFlux.XmlCreatEl(xmlFlux.root, "FluxApps");
                //if (oCnxBase.CBRecherche("Select distinct Techlink.GuidTechLink, NomTechLink, GuidServerIn, Sin.NomServer, GuidServerOut, SOut.NomServer From DansVue, GTechLink, TechLink, Server Sin, Server Sout Where DansVue.GuidVue='" + sGuidVueInf + "' and DansVue.GuidObjet=GTechLink.GuidGTechLink and GTechlink.GuidTechLink=TechLink.GuidTechLink and GuidServerIn=Sin.GuidServer and GuidServerOut=Sout.GuidServer Order by TechLink.Id, TechLink.GuidTechLink"))

                if (oCnxBase.CBRecherche("Select distinct Techlink.GuidTechLink, TechLink.Id, NomTechLink, GuidServerIn, GuidServerOut, TechLink.GuidGroupService, NomGroupService From Vue, DansVue, GTechLink, TechLink, GroupService Where Vue.GuidVue='" + sGuidVueInf + "' and Vue.GuidGVue=DansVue.GuidGVue and DansVue.GuidObjet=GTechLink.GuidGTechLink and GTechlink.GuidTechLink=TechLink.GuidTechLink and TechLink.GuidGroupService=GroupService.GuidGroupService Order by TechLink.Id, Techlink.GuidTechLink"))
                {
                    while (oCnxBase.Reader.Read())
                    {
                        XmlElement elFlux = xmlFlux.XmlCreatEl(xmlFlux.root, "Flux");
                        elFlux.SetAttribute("Guid", oCnxBase.Reader.GetString(0));
                        if (oCnxBase.Reader.IsDBNull(1)) elFlux.SetAttribute("Id", ""); else elFlux.SetAttribute("Id", oCnxBase.Reader.GetString(1));
                        elFlux.SetAttribute("Nom", oCnxBase.Reader.GetString(2));
                        elFlux.SetAttribute("Selected", "No");

                        XmlElement elO = xmlFlux.XmlCreatEl(elFlux, "Origine");
                        elO.SetAttribute("Guid", oCnxBase.Reader.GetString(3));

                        XmlElement elC = xmlFlux.XmlCreatEl(elFlux, "Cible");
                        elC.SetAttribute("Guid", oCnxBase.Reader.GetString(4));

                        XmlElement elGS = xmlFlux.XmlCreatEl(elFlux, "GroupService");
                        elGS.SetAttribute("Guid", oCnxBase.Reader.GetString(5));
                        elGS.SetAttribute("Nom", oCnxBase.Reader.GetString(6));
                        XmlElement elS = xmlFlux.XmlCreatEl(elGS, "Services");
                    }
                }
                oCnxBase.CBReaderClose();
                xmlFlux.XmlCreatEl(xmlFlux.root, "Clusters");
                xmlFlux.XmlCreatEl(xmlFlux.root, "DelLinkOut");
                xmlFlux.XmlCreatEl(xmlFlux.root, "DelLinkIn");
                
                CompleteService(xmlFlux, sGuidVueDeploy);
                CompleteXmlFlux(xmlFlux, "Origine", "AppUser", sGuidVueDeploy);
                CompleteXmlFlux(xmlFlux, "Origine", "Application", sGuidVueDeploy);
                CompleteXmlFluxAppCloud(xmlFlux, "Origine", sGuidVueDeploy);
                CompleteXmlFlux(xmlFlux, "Origine", "Server", sGuidVueDeploy);

                CompleteXmlFluxServiceCloud(xmlFlux, "Origine", "svc", sGuidVueDeploy);
                CompleteXmlFluxServiceCloud(xmlFlux, "Origine", "ing", sGuidVueDeploy);

                CompleteXmlFluxLabelCloud(xmlFlux, "Origine", "Server", sGuidVueDeploy);
                CompleteXmlFluxLabelCloud(xmlFlux, "Origine", "pod", sGuidVueDeploy);
                CompleteXmlFluxLabelCloud(xmlFlux, "Origine", "svc", sGuidVueDeploy);
                CompleteXmlFluxLabelCloud(xmlFlux, "Origine", "ing", sGuidVueDeploy);
                CompleteXmlFluxLabelCloud(xmlFlux, "Origine", "sas", sGuidVueDeploy);

                CompleteXmlFlux(xmlFlux, "Cible", "AppUser", sGuidVueDeploy);
                CompleteXmlFlux(xmlFlux, "Cible", "Application", sGuidVueDeploy);
                CompleteXmlFluxAppCloud(xmlFlux, "Cible", sGuidVueDeploy);
                CompleteXmlFlux(xmlFlux, "Cible", "Server", sGuidVueDeploy);

                CompleteXmlFluxServiceCloud(xmlFlux, "Cible", "svc", sGuidVueDeploy);
                CompleteXmlFluxServiceCloud(xmlFlux, "Cible", "ing", sGuidVueDeploy);

                CompleteXmlFluxLabelCloud(xmlFlux, "Cible", "Server", sGuidVueDeploy);
                CompleteXmlFluxLabelCloud(xmlFlux, "Cible", "pod", sGuidVueDeploy);
                CompleteXmlFluxLabelCloud(xmlFlux, "Cible", "svc", sGuidVueDeploy);
                CompleteXmlFluxLabelCloud(xmlFlux, "Cible", "ing", sGuidVueDeploy);
                CompleteXmlFluxLabelCloud(xmlFlux, "Cible", "sas", sGuidVueDeploy);

                //CompleteXmlFluxServiceCloud(xmlFlux, "Cible", sGuidVueDeploy);

                ActiveXmlFluxFromDB(xmlFlux, "Out", sGuidVueInf, sGuidVueDeploy);
                ActiveXmlFluxFromDB(xmlFlux, "In", sGuidVueInf, sGuidVueDeploy);

                xmlFlux.docXml.Save("\\dat\\FluxTest.xml");
            }
            oCnxBase.CBReaderClose();
            return xmlFlux;
        }

        public void CommandAideFluxBoutenBout(string sFlux)
        {
            XmlExcel xmlFlux = null, xmlFluxChild = null;
            if (sFlux == "App")
            {
                xmlFlux = xmlCreatFluxApp(sGuidVueInf, GuidVue.ToString());
                xmlFluxChild = xmlCreatFluxInfra(GuidVue.ToString());
            } else
            {
                xmlFlux = xmlCreatFluxFonc(sGuidVueInf, GuidVue.ToString());
                xmlFluxChild = xmlCreatFluxApp(GuidVue.ToString());
            }

            FormFluxBoutenBout ff = new FormFluxBoutenBout(this, sFlux, xmlFlux, xmlFluxChild);
            ff.ShowDialog(this);
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Owner.SetStateOfControls();
        }

        public void CommandAideFlux()
        {
            XmlExcel xmlFlux = XmlCreatFlux(sGuidVueInf, GuidVue.ToString());
            //xmlFlux.docXml.Save("\\dat\\flux.xml");

            //XmlExcel xmlDelLinkOut = new XmlExcel(this, "DelLink");
            //XmlExcel xmlDelLinkIn = new XmlExcel(this, "DelLink");

            //ActiveXmlFluxFromDB(xmlFlux, "Out", xmlDelLinkOut);
            //ActiveXmlFluxFromDB(xmlFlux, "In", xmlDelLinkIn);

            //FormFluxApp ff = new FormFluxApp(this, xmlFlux, xmlDelLinkOut, xmlDelLinkIn);

            FormFluxApp ff = new FormFluxApp(this, xmlFlux);
            ff.ShowDialog(this);
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Owner.SetStateOfControls();

            //docXml.Save("\\dat\\ListTest.xml");
        }

        /*
         public void CommandAideFlux()
        {
            docXml = new XmlDocument();
            docXml.LoadXml("<Flux></Flux>");
            
            XmlElement root = docXml.DocumentElement;

            if (oCnxBase.CBRecherche("Select distinct Techlink.GuidTechLink, NomTechLink, GuidServerIn, GuidServerOut From DansVue, GTechLink, TechLink Where DansVue.GuidVue='" + sGuidVueInf + "' and DansVue.GuidObjet=GTechLink.GuidGTechLink and GTechlink.GuidTechLink=TechLink.GuidTechLink Order by TechLink.Id, TechLink.GuidTechLink"))
            {
                while (oCnxBase.Reader.Read())
                {
                    XmlElement elFlux = docXml.CreateElement("Flux");
                    elFlux.SetAttribute("Guid", oCnxBase.Reader.GetString(0));
                    elFlux.SetAttribute("Nom", oCnxBase.Reader.GetString(1));
                    elFlux.SetAttribute("Selected", "No");
                    root.AppendChild(elFlux);

                    XmlElement elO = docXml.CreateElement("Origine");
                    elO.SetAttribute("Guid", oCnxBase.Reader.GetString(2));
                    elFlux.AppendChild(elO);

                    XmlElement elC = docXml.CreateElement("Cible");
                    elC.SetAttribute("Guid", oCnxBase.Reader.GetString(3));
                    elFlux.AppendChild(elC);
                }
            }
            oCnxBase.CBReaderClose();

            CompleteXmlFlux(root, "Origine", "User");
            CompleteXmlFlux(root, "Origine", "Application");
            CompleteXmlFlux(root, "Origine", "Server");
            CompleteXmlFlux(root, "Cible", "User");
            CompleteXmlFlux(root, "Cible", "Application");
            CompleteXmlFlux(root, "Cible", "Server");

            XmlDocument xmlDelLinkOut = new XmlDocument();
            ActiveXmlFluxFromDB(root, "Out", xmlDelLinkOut);

            XmlDocument xmlDelLinkIn = new XmlDocument();
            ActiveXmlFluxFromDB(root, "In", xmlDelLinkIn);

            FormFluxApp ff = new FormFluxApp(this, xmlDelLinkOut, xmlDelLinkIn);
            ff.ShowDialog(this);
            
            //docXml.Save("\\dat\\ListTest.xml");
        }
        */

        /*public void ActiveXmlFluxFromDB(XmlExcel xmlFlux, string sSens, XmlExcel xmlDelLink)
        {
            string sSensPlus = ""; string sPartenaire="Origine";
            if (sSens == "In") {sSensPlus = ", GuidAlias"; sPartenaire = "Cible";}

            oCnxBase.CBRecherche("Select GuidTechLink, NCardLink" + sSens + ".GuidNCard" + sSensPlus + " From NCardLink" + sSens + ", GNCard, DansVue Where DansVue.GuidVue='" + GuidVue + "' and DansVue.GuidObjet=GNCard.GuidGNCard and GNCard.GuidNCard=NCardLink" + sSens + ".GuidNCard and GuidTechlink in (Select distinct Techlink.GuidTechLink From DansVue, GTechLink, TechLink Where DansVue.GuidVue='" + sGuidVueInf + "' and DansVue.GuidObjet=GTechLink.GuidGTechLink and GTechlink.GuidTechLink=TechLink.GuidTechLink)");
            while (oCnxBase.Reader.Read())
            {
                XmlElement elFlux = xmlFlux.XmlGetFirstElFromParent(xmlFlux.root, "Flux", "Guid", oCnxBase.Reader.GetString(0));
                if (elFlux != null)
                {
                    XmlElement elServer = xmlFlux.XmlGetFirstElFromName(elFlux, sPartenaire);
                    if (elServer != null)
                    {
                        XmlElement elCard = xmlFlux.XmlGetFirstElFromParent(elServer, "NCard", "Guid", oCnxBase.Reader.GetString(1));
                        if (elCard != null)
                        {
                            if (sSens == "In")
                            {
                                XmlElement elAlias = xmlFlux.XmlGetFirstElFromParent(elCard, "Alias", "Guid", oCnxBase.Reader.GetString(2));
                                if (elAlias != null)
                                {
                                    xmlFlux.XmlAllSetParentAttributValueFromEl(elAlias, "Selected", "Yes");
                                    XmlElement elDelLink = xmlDelLink.XmlCreatEl(xmlDelLink.root, "NCardLinkIn"); 
                                    elDelLink.SetAttribute("GuidNCard", oCnxBase.Reader.GetString(1));
                                    elDelLink.SetAttribute("GuidAlias", oCnxBase.Reader.GetString(2));
                                    elDelLink.SetAttribute("GuidTechLink", oCnxBase.Reader.GetString(0));
                                }
                            }
                            else
                            {
                                xmlFlux.XmlAllSetParentAttributValueFromEl(elCard, "Selected", "Yes");
                                XmlElement elDelLink = xmlDelLink.XmlCreatEl(xmlDelLink.root, "NCardLinkOut"); 
                                elDelLink.SetAttribute("GuidNCard", oCnxBase.Reader.GetString(1));
                                elDelLink.SetAttribute("GuidTechLink", oCnxBase.Reader.GetString(0));
                            }
                        }
                    }
                }
            }
            oCnxBase.CBReaderClose();
            //XmldeleteBaseFromXml(xmlDelLink.DocumentElement);
        }*/

        public void ActiveXmlFluxFromDB(XmlExcel xmlFlux, string sSens, string sGuidVueInf, string sGuidVueDeploy)
        {
            string sSensPlus = ""; string sPartenaire = "Origine";
            XmlElement elDel = xmlFlux.XmlGetFirstElFromName(xmlFlux.root, "DelLink" + sSens);
            if (sSens == "In") { sSensPlus = ", GuidAlias"; sPartenaire = "Cible"; }

            oCnxBase.CBRecherche("Select GuidTechLink, NCardLink" + sSens + ".GuidNCard" + sSensPlus + " From NCardLink" + sSens + ", GNCard, DansVue, Vue Where Vue.GuidVue='" + sGuidVueDeploy + "'and Vue.GuidGVue=DansVue.GuidGVue and DansVue.GuidObjet=GNCard.GuidGNCard and GNCard.GuidNCard=NCardLink" + sSens + ".GuidNCard and GuidTechlink in (Select distinct Techlink.GuidTechLink From Vue, DansVue, GTechLink, TechLink Where Vue.GuidVue='" + sGuidVueInf + "' and Vue.GuidGVue=DansVue.GuidGVue and DansVue.GuidObjet=GTechLink.GuidGTechLink and GTechlink.GuidTechLink=TechLink.GuidTechLink)");
            while (oCnxBase.Reader.Read())
            {
                XmlElement elFlux = xmlFlux.XmlGetFirstElFromParent(xmlFlux.root, "Flux", "Guid", oCnxBase.Reader.GetString(0));
                if (elFlux != null)
                {
                    XmlElement elServer = xmlFlux.XmlGetFirstElFromName(elFlux, sPartenaire);
                    if (elServer != null)
                    {
                        XmlElement elCard = xmlFlux.XmlGetFirstElFromParent(elServer, "NCard", "Guid", oCnxBase.Reader.GetString(1));
                        if (elCard != null)
                        {
                            if (sSens == "In")
                            {
                                XmlElement elAlias = xmlFlux.XmlGetFirstElFromParent(elCard, "Alias", "Guid", oCnxBase.Reader.GetString(2));
                                if (elAlias != null)
                                {
                                    xmlFlux.XmlAllSetParentAttributValueFromEl(elAlias, "Selected", "Yes");
                                    XmlElement elDelLink = xmlFlux.XmlCreatEl(elDel, "NCardLinkIn");
                                    elDelLink.SetAttribute("GuidNCard", oCnxBase.Reader.GetString(1));
                                    elDelLink.SetAttribute("GuidAlias", oCnxBase.Reader.GetString(2));
                                    elDelLink.SetAttribute("GuidTechLink", oCnxBase.Reader.GetString(0));
                                }
                            }
                            else
                            {
                                xmlFlux.XmlAllSetParentAttributValueFromEl(elCard, "Selected", "Yes");
                                XmlElement elDelLink = xmlFlux.XmlCreatEl(elDel, "NCardLinkOut");
                                elDelLink.SetAttribute("GuidNCard", oCnxBase.Reader.GetString(1));
                                elDelLink.SetAttribute("GuidTechLink", oCnxBase.Reader.GetString(0));
                            }
                        }
                    }
                }
            }
            oCnxBase.CBReaderClose();
            oCnxBase.CBRecherche("Select GuidVue, GuidTechLink, Label.GuidLabelClass, Label.GuidLabel From LabelLink" + sSens + ", Label Where GuidVue = '" + sGuidVueDeploy + "' and LabelLink" + sSens + ".GuidLabel = Label.GuidLabel");
            while (oCnxBase.Reader.Read())
            {
                XmlElement elFlux = xmlFlux.XmlGetFirstElFromParent(xmlFlux.root, "Flux", "Guid", oCnxBase.Reader.GetString(1));
                if (elFlux != null)
                {
                    if (elFlux.GetAttribute("Id") == "04a")
                        elFlux = elFlux;
                    XmlElement elServer = xmlFlux.XmlGetFirstElFromName(elFlux, sPartenaire); // Element Origine ou Cible
                    
                    if (elServer != null)
                    {







                        XmlElement elLabelClass = xmlFlux.XmlGetFirstElFromParent(elServer, "LabelClass", "Guid", oCnxBase.Reader.GetString(2));
                        if (elLabelClass != null)
                        {
                            XmlElement elLabelValue = xmlFlux.XmlGetFirstElFromParent(elLabelClass, "LabelValue", "Guid", oCnxBase.Reader.GetString(3));
                            if (elLabelValue != null)
                            {
                                xmlFlux.XmlAllSetParentAttributValueFromEl(elLabelValue, "Selected", "Yes");

                                XmlElement elDelLink = xmlFlux.XmlCreatEl(elDel, "LabelLink" + sSens);
                                elDelLink.SetAttribute("GuidVue", oCnxBase.Reader.GetString(0));
                                elDelLink.SetAttribute("GuidTechLink", oCnxBase.Reader.GetString(1));
                                elDelLink.SetAttribute("GuidLabel", oCnxBase.Reader.GetString(3));
                            }

                        }
                    }
                }
            }
            oCnxBase.CBReaderClose();

            //XmldeleteBaseFromXml(xmlDelLink.DocumentElement);
        }

        public void ActiveXmlFluxInterSiteFromDB(XmlExcel xmlFlux, string sSens, string sGuidVueDeploy)
        {
            string sPartenaire = "Origine";
            if (sSens == "Out") sPartenaire = "Cible";

            oCnxBase.CBRecherche("Select Distinct InterLink.GuidInterLink, NCard.GuidNCard From Vue, DansVue, InterLink, GInterLink, ServerPhy, NCard, NCardInterLink" + sSens + " Where Vue.GuidVue = '" + sGuidVueDeploy + "' and Vue.GuidGvue = DansVue.GuidGVue and GuidObjet = GuidGInterLink and GInterLink.GuidInterLink = InterLink.GuidInterLink and GuidServerSite" + sSens + " = ServerPhy.GuidServerPhy and ServerPhy.GuidServerPhy = NCard.GuidHote and NCard.GuidNCard = NCardInterLink" + sSens + ".GuidNCard");
            
            while (oCnxBase.Reader.Read())
            {

                XmlElement elFlux = xmlFlux.XmlGetFirstElFromParent(xmlFlux.root, "Flux", "Guid", oCnxBase.Reader.GetString(0));
                if (elFlux != null)
                {
                    XmlElement elServer = xmlFlux.XmlGetFirstElFromName(elFlux, sPartenaire);
                    if (elServer != null)
                    {
                        XmlElement elCard = xmlFlux.XmlGetFirstElFromParent(elServer, "NCard", "Guid", oCnxBase.Reader.GetString(1));
                        if (elCard != null)
                        {

                            xmlFlux.XmlAllSetParentAttributValueFromEl(elCard, "Selected", "Yes");
                            //XmlElement elDelLink = xmlFlux.XmlCreatEl(elDel, "NCardLinkOut");
                            //elDelLink.SetAttribute("GuidNCard", oCnxBase.Reader.GetString(1));
                            //elDelLink.SetAttribute("GuidTechLink", oCnxBase.Reader.GetString(0));

                        }
                    }
                }
            }
            oCnxBase.CBReaderClose();
            //XmldeleteBaseFromXml(xmlDelLink.DocumentElement);
        }
        public void CompleteXmlFluxFromResultSqlCluster(XmlExcel xmlFlux, string sTypeExtremiteFlux, string sGuidCluster)
        {

            while (oCnxBase.Reader.Read())
            {
                IEnumerator ienum = xmlFlux.root.GetEnumerator();
                XmlNode Node;

                while (ienum.MoveNext())
                {
                    Node = (XmlNode)ienum.Current;
                    if (Node.NodeType == XmlNodeType.Element && Node.Name == "Flux")
                    {
                        XmlElement el = xmlFlux.XmlGetFirstElFromParent((XmlElement)Node, sTypeExtremiteFlux);
                        XmlElement elObj = xmlFlux.XmlFindElFromAtt(el, "Guid", sGuidCluster, 0);
                        if (elObj != null)
                        {


                            XmlElement elCluster = xmlFlux.XmlCreatEl(elObj, "Server");
                            elCluster.SetAttribute("Guid", oCnxBase.Reader.GetString(0));
                            elCluster.SetAttribute("Nom", oCnxBase.Reader.GetString(1));
                        }
                    }
                }
            }
        }

        public ArrayList CompleteXmlFluxFromResultSql(XmlExcel xmlFlux, string sTypeExtremiteFlux, string sTypeObjet, string sTypHote = "Server")
        {
            IEnumerator ienum = xmlFlux.root.GetEnumerator();
            XmlNode Node;
            ArrayList lstGuidServerPhy = new ArrayList();

            bool eor = true;
            while (ienum.MoveNext() && eor)
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    XmlElement elOrigine = xmlFlux.XmlGetFirstElFromParent((XmlElement)Node, sTypeExtremiteFlux);
                    while (eor && ((XmlElement)Node).GetAttribute("Guid") == oCnxBase.Reader.GetString(1) && elOrigine != null && elOrigine.GetAttribute("Guid") == oCnxBase.Reader.GetString(2))
                    {
                        if (!oCnxBase.Reader.IsDBNull(3)) elOrigine.SetAttribute("NomObjInfra", oCnxBase.Reader.GetString(3));
                        XmlElement elTypeOrigine = xmlFlux.XmlGetFirstElFromParent(elOrigine, sTypeObjet, "Guid", oCnxBase.Reader.GetString(4));
                        if (elTypeOrigine == null)
                        {
                            elTypeOrigine = xmlFlux.XmlCreatEl(elOrigine, sTypeObjet);
                            elTypeOrigine.SetAttribute("Guid", oCnxBase.Reader.GetString(4));
                            if (lstGuidServerPhy.IndexOf(oCnxBase.Reader.GetString(4)) == -1) lstGuidServerPhy.Add(oCnxBase.Reader.GetString(4));
                            elTypeOrigine.SetAttribute("Nom", oCnxBase.Reader.GetString(5));
                            elTypeOrigine.SetAttribute("Location", oCnxBase.Reader.GetString(6));
                            elTypeOrigine.SetAttribute("Selected", "No");
                            if (sTypHote == "Cluster") elTypeOrigine.SetAttribute("Cluster", "Yes");
                        }


                        XmlElement elCard = xmlFlux.XmlGetFirstElFromParent(elTypeOrigine, "NCard", "Guid", oCnxBase.Reader.GetString(7));
                        if (elCard == null) // && !oCnxBase.Reader.IsDBNull(5))
                        {
                            elCard = xmlFlux.XmlCreatEl(elTypeOrigine, "NCard");
                            elCard.SetAttribute("Guid", oCnxBase.Reader.GetString(7));
                            elCard.SetAttribute("Nom", oCnxBase.Reader.GetString(11));
                            if (oCnxBase.Reader.IsDBNull(8)) elCard.SetAttribute("IP", ""); else elCard.SetAttribute("IP", oCnxBase.Reader.GetString(8));
                            if (oCnxBase.Reader.IsDBNull(12)) elCard.SetAttribute("Nat", ""); else elCard.SetAttribute("Nat", oCnxBase.Reader.GetString(12));
                            elCard.SetAttribute("Selected", "No");
                        }
                        if (elCard != null && !oCnxBase.Reader.IsDBNull(8) && !oCnxBase.Reader.IsDBNull(10))
                        {
                            XmlElement elAlias = xmlFlux.XmlGetFirstElFromParent(elCard, "Alias", "Guid", oCnxBase.Reader.GetString(9));
                            if (elAlias == null)
                            {
                                elAlias = xmlFlux.XmlCreatEl(elCard, "Alias");
                                elAlias.SetAttribute("Guid", oCnxBase.Reader.GetString(9));
                                elAlias.SetAttribute("Nom", oCnxBase.Reader.GetString(10));
                                elAlias.SetAttribute("Selected", "No");
                            }
                        }
                        if (elCard != null && !oCnxBase.Reader.IsDBNull(13) && !oCnxBase.Reader.IsDBNull(14))
                        {
                            XmlElement elVlan = xmlFlux.XmlGetFirstElFromParent(elCard, "Vlan", "Guid", oCnxBase.Reader.GetString(13));
                            if (elVlan == null)
                            {
                                elVlan = xmlFlux.XmlCreatEl(elCard, "Vlan");
                                elVlan.SetAttribute("Guid", oCnxBase.Reader.GetString(13));
                                XmlElement elVlanClass = xmlFlux.XmlCreatEl(elVlan, "VlanClass");
                                elVlanClass.SetAttribute("Guid", oCnxBase.Reader.GetString(14));
                            }
                        }
                        eor = oCnxBase.Reader.Read();
                    }

                    //oCnxBase.Reader.Read()

                }
                //if (Node.Name == SearchName) aNode.Add(Node);
            }
            return lstGuidServerPhy;
        }

        public ArrayList CompleteXmlFluxFromResultSqlLabel(XmlExcel xmlFlux, string sTypeExtremiteFlux, string sTypeObjet)
        {
            //(0)TechLink.Id, (1)TechLink.GuidTechLink, (2)GuidServerIn/Out, (3)NomInfra(Gen.../Server,App,...), (4)GuidInsPod/GuidServerPhy, (5)NomInsPod, ' ', (6)GuidLabelClass, (7)NomLabelClass, (8)GuidLabel, (9)NomLabel
            IEnumerator ienum = xmlFlux.root.GetEnumerator();
            XmlNode Node;
            ArrayList lstGuidServerPhy = new ArrayList();

            bool eor = true;
            while (ienum.MoveNext() && eor)
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    XmlElement elOrigine = xmlFlux.XmlGetFirstElFromParent((XmlElement)Node, sTypeExtremiteFlux);
                    while (eor && ((XmlElement)Node).GetAttribute("Guid") == oCnxBase.Reader.GetString(1) && elOrigine != null && elOrigine.GetAttribute("Guid") == oCnxBase.Reader.GetString(2))
                    {
                        if (!oCnxBase.Reader.IsDBNull(3)) elOrigine.SetAttribute("NomObjInfra", oCnxBase.Reader.GetString(3));
                        XmlElement elTypeOrigine = xmlFlux.XmlGetFirstElFromParent(elOrigine, sTypeObjet, "Guid", oCnxBase.Reader.GetString(4));
                        if (elTypeOrigine == null)
                        {
                            elTypeOrigine = xmlFlux.XmlCreatEl(elOrigine, sTypeObjet);
                            elTypeOrigine.SetAttribute("Guid", oCnxBase.Reader.GetString(4));
                            if (lstGuidServerPhy.IndexOf(oCnxBase.Reader.GetString(4)) == -1) lstGuidServerPhy.Add(oCnxBase.Reader.GetString(4));
                            elTypeOrigine.SetAttribute("Nom", oCnxBase.Reader.GetString(5));
                            elTypeOrigine.SetAttribute("Location", oCnxBase.Reader.GetString(6));
                            elTypeOrigine.SetAttribute("Selected", "No");
                        }
                        XmlElement elLabelClass = xmlFlux.XmlGetFirstElFromParent(elTypeOrigine, "LabelClass", "Guid", oCnxBase.Reader.GetString(7));
                        if (elLabelClass == null)
                        {
                            elLabelClass = xmlFlux.XmlCreatEl(elTypeOrigine, "LabelClass");
                            elLabelClass.SetAttribute("Guid", oCnxBase.Reader.GetString(7));
                            elLabelClass.SetAttribute("Nom", oCnxBase.Reader.GetString(8));
                            elLabelClass.SetAttribute("Selected", "No");
                        }
                        XmlElement elLabelValue = xmlFlux.XmlGetFirstElFromParent(elLabelClass, "LabelValue", "Guid", oCnxBase.Reader.GetString(9));
                        if (elLabelValue == null ) elLabelValue = xmlFlux.XmlCreatEl(elLabelClass, "LabelValue");
                        elLabelValue.SetAttribute("Guid", oCnxBase.Reader.GetString(9));
                        elLabelValue.SetAttribute("Nom", oCnxBase.Reader.GetString(10));
                        elLabelValue.SetAttribute("Selected", "No");
                        eor = oCnxBase.Reader.Read();
                    }

                    //oCnxBase.Reader.Read()

                }
                //if (Node.Name == SearchName) aNode.Add(Node);
            }
            return lstGuidServerPhy;
        }


        public void CompleteService(XmlExcel xmlFlux, string sGuidVueDeploy)
        {
            if (oCnxBase.CBRecherche("select TechLink.GuidTechLink, Service.GuidService, NomService, Protocole, Ports from Vue vDeploy, Vue vInfra, DansVue, GTechLink, TechLink, ServiceLink, Service where vdeploy.GuidVueInf=vInfra.GuidVue and vInfra.GuidGVue=DansVue.GuidGVue and DansVue.GuidObjet=GTechLink.GuidGTechLink and GTechLink.GuidTechLink=TechLink.GuidTechLink and TechLink.GuidGroupService=ServiceLink.GuidGroupService and ServiceLink.GuidEnvironnement=vdeploy.GuidEnvironnement and ServiceLink.GuidService=Service.GuidService and vdeploy.GuidVue='" + sGuidVueDeploy + "' order by TechLink.GuidTechLink"))
            {
                string sTechLinkLast = null;
                XmlElement elTechLink = null, elServices = null;
                while (oCnxBase.Reader.Read())
                {
                    if (oCnxBase.Reader.GetString(0) != sTechLinkLast) {
                        sTechLinkLast = oCnxBase.Reader.GetString(0);
                        elTechLink = xmlFlux.XmlFindElFromAtt(xmlFlux.root, "Guid", sTechLinkLast);
                        if (elTechLink != null) // possible si plusieurs vue infra (voir l'application infrastructure)
                            elServices = xmlFlux.XmlGetFirstElFromName(elTechLink, "Services");
                        else sTechLinkLast = null;
                    }
                    if (sTechLinkLast != null)
                    {
                        XmlElement elS = xmlFlux.XmlCreatEl(elServices, "Service");
                        elS.SetAttribute("Guid", oCnxBase.Reader.GetString(1));
                        elS.SetAttribute("Nom", oCnxBase.Reader.GetString(2));
                        elS.SetAttribute("Protocol", oCnxBase.Reader.GetString(3));
                        elS.SetAttribute("Ports", oCnxBase.Reader.GetString(4));
                    }

                }
            }
            oCnxBase.CBReaderClose();
        }

        public void CompleteXmlFlux(XmlExcel xmlFlux, string sTypeExtremiteFlux, string sTypeObjet, string sGuidDeploy)
        {
            // SELECT distinct techlink.GuidTechLink, techlink.NomTechLink, techlink.GuidServerIn, NomServerPhy, IPAddr, NomAlias  FROM Vue, dansvue, gtechlink, techlink, serverlink, serverphy, NCard, Alias where Vue.GuidVue='77aa97ac-40a8-4b5e-a50a-f6382389374c' and Vue.GuidVueInf=DansVue.GuidVue and Vue.GuidVue=ServerLink.GuidVue and dansvue.guidobjet=gtechlink.GuidGTechLink and gtechlink.guidtechlink=techlink.GuidTechLink and techlink.GuidServerIn=ServerLink.GuidServer and ServerLink.GuidServerPhy=ServerPhy.GuidServerPhy and ServerPhy.GuidServerPhy=NCard.GuidHote and NCard.GuidNCard=Alias.GuidNCard order by GuidTechLink;
            string sGuidExtremite = "GuidServerIn";
            string sNCard = "and NCard.GuidNCard in (Select NCard.GuidNCard FROM Vue, Dansvue, GNCard, NCard Where Vue.GuidVue='" + sGuidDeploy + "' and Vue.GuidGVue=DansVue.GuidGVue and DansVue.GuidObjet=GNCard.GuidGNCard and GNCard.GuidNCard=NCard.GuidNCard)";

            if (sTypeExtremiteFlux == "Cible")
            {
                sGuidExtremite = "GuidServerOut";
            }
            string sNomObjInfra = "Nom" + sTypeObjet;
            if (sNomObjInfra == "NomApplication") sNomObjInfra = "Trigramme";
            if (oCnxBase.CBRecherche("SELECT distinct TechLink.Id, TechLink.GuidTechLink, " + sGuidExtremite + ", " + sNomObjInfra + ", ServerPhy.GuidServerPhy, NomServerPhy, NomLocation, NCard.GuidNCard, IPAddr, Alias.GuidAlias, NomAlias, NomNCard, IPNat, VLan.GuidVLan, VlanClass.GuidVlanClass FROM Vue vDeploy, Vue vInfra, Dansvue, GTechLink, TechLink, " + sTypeObjet + "Link, " + sTypeObjet + ", serverphy, Location, NCard  left join Alias on NCard.GuidNCard=Alias.GuidNCard left join VLan on NCard.GuidVLan=VLan.GuidVLan left join VlanClass on VLan.GuidVlanClass=VlanClass.GuidVlanClass WHERE vDeploy.GuidVue='" + sGuidDeploy + "' and vDeploy.GuidVueInf=vInfra.GuidVue and vInfra.GuidGVue=DansVue.GuidGVue and vDeploy.GuidVue=" + sTypeObjet + "Link.GuidVue and " + sGuidExtremite + "=" + sTypeObjet + ".Guid" + sTypeObjet + " and DansVue.GuidObjet=GTechLink.GuidGTechLink and GTechLink.GuidTechLink=TechLink.GuidTechLink and TechLink." + sGuidExtremite + "=" + sTypeObjet + "Link.Guid" + sTypeObjet + " and " + sTypeObjet + "Link.GuidServerPhy=ServerPhy.GuidServerPhy and ServerPhy.GuidLocation=Location.GuidLocation and ServerPhy.GuidServerPhy=NCard.GuidHote order by TechLink.Id, TechLink.GuidTechLink"))
                //if (oCnxBase.CBRecherche("SELECT distinct TechLink.GuidTechLink, " + sGuidExtremite + ", ServerPhy.GuidServerPhy, NomServerPhy, NCard.GuidNCard, IPAddr, Alias.GuidAlias, NomAlias FROM Vue, Dansvue, GTechLink, TechLink, " + sTypeObjet + "Link, " + sTypeObjet + ", serverphy, NCard  left join Alias on NCard.GuidNCard=Alias.GuidNCard WHERE Vue.GuidVue='" + GuidVue + "' and Vue.GuidVueInf=DansVue.GuidVue and Vue.GuidVue=" + sTypeObjet + "Link.GuidVue and " + sGuidExtremite + "=" + sTypeObjet + ".Guid" + sTypeObjet + " and DansVue.GuidObjet=GTechLink.GuidGTechLink and GTechLink.GuidTechLink=TechLink.GuidTechLink and TechLink." + sGuidExtremite + "=" + sTypeObjet + "Link.Guid" + sTypeObjet + " and " + sTypeObjet + "Link.GuidServerPhy=ServerPhy.GuidServerPhy and ServerPhy.GuidServerPhy=NCard.GuidHote order by TechLink.Id, TechLink.GuidTechLink"))
                CompleteXmlFluxFromResultSql(xmlFlux, sTypeExtremiteFlux, sTypeObjet);
            oCnxBase.CBReaderClose();

            ArrayList lstGuidCluster = new ArrayList();
            if (oCnxBase.CBRecherche("SELECT distinct TechLink.Id, TechLink.GuidTechLink, " + sGuidExtremite + ",NULL, Cluster.GuidCluster, NomCluster, ' ', NCard.GuidNCard, IPAddr, Alias.GuidAlias, NomAlias, NomNCard, IPNat, VLan.GuidVLan, VlanClass.GuidVlanClass FROM Vue vDeploy, Vue vInfra, Dansvue, GTechLink, TechLink, " + sTypeObjet + "Link, serverphy, cluster, NCard  left join Alias on NCard.GuidNCard=Alias.GuidNCard left join VLan on NCard.GuidVLan=VLan.GuidVLan left join VlanClass on VLan.GuidVlanClass=VlanClass.GuidVlanClass WHERE VDeploy.GuidVue='" + sGuidDeploy + "'and vDeploy.GuidVueInf=vInfra.GuidVue and vInfra.GuidGVue=DansVue.GuidGVue and vDeploy.GuidVue=" + sTypeObjet + "Link.GuidVue and DansVue.GuidObjet=GTechLink.GuidGTechLink and GTechLink.GuidTechLink=TechLink.GuidTechLink and TechLink." + sGuidExtremite + "=" + sTypeObjet + "Link.Guid" + sTypeObjet + " and " + sTypeObjet + "Link.GuidServerPhy=ServerPhy.GuidServerPhy and ServerPhy.GuidCluster=Cluster.GuidCluster and Cluster.GuidCluster=NCard.GuidHote " + sNCard + " order by TechLink.Id, TechLink.GuidTechLink"))
                lstGuidCluster = CompleteXmlFluxFromResultSql(xmlFlux, sTypeExtremiteFlux, sTypeObjet, "Cluster");
            oCnxBase.CBReaderClose();
            XmlElement elClusters = xmlFlux.XmlGetFirstElFromName(xmlFlux.root, "Clusters");
            for (int i = 0; i < lstGuidCluster.Count; i++)
            {
                XmlElement elCluster = xmlFlux.XmlFindElFromAtt(elClusters, "Guid", (string)lstGuidCluster[i]);
                if (elCluster == null)
                {
                    //Create le cluster et ajout des serveurs sur cluster
                    elCluster = xmlFlux.XmlCreatEl(elClusters, "Cluster");
                    elCluster.SetAttribute("Guid", (string)lstGuidCluster[i]);
                    if (oCnxBase.CBRecherche("Select GuidServerPhy, NomServerPhy From ServerPhy Where GuidCluster='" + lstGuidCluster[i] + "'"))
                    {
                        while (oCnxBase.Reader.Read())
                        {
                            XmlElement elSrv = xmlFlux.XmlCreatEl(elCluster, "Server");
                            elSrv.SetAttribute("Guid", oCnxBase.Reader.GetString(0));
                            elSrv.SetAttribute("Nom", oCnxBase.Reader.GetString(1));
                        }

                    }
                    oCnxBase.CBReaderClose();
                }
            }

            /*for(int i=0; i< lstGuidCluster.Count; i++)
            {
                if (oCnxBase.CBRecherche("Select distinct GuidServerPhy, NomServerPhy From Cluster, ServerPhy Where ServerPhy.GuidCluster=Cluster.GuidCluster and Cluster.GuidCluster='" + lstGuidCluster[i] + "'"))
                    CompleteXmlFluxFromResultSqlCluster(xmlFlux, sTypeExtremiteFlux, (string)lstGuidCluster[i]);
                oCnxBase.CBReaderClose();
            }
            oCnxBase.CBReaderClose();*/
        }

        
        public void CompleteXmlFluxServiceCloud(XmlExcel xmlFlux, string sTypeExtremiteFlux, string sTypeObjet, string sGuidDeploy)
        {
            
            // SELECT distinct techlink.GuidTechLink, techlink.NomTechLink, techlink.GuidServerIn, NomServerPhy, IPAddr, NomAlias  FROM Vue, dansvue, gtechlink, techlink, serverlink, serverphy, NCard, Alias where Vue.GuidVue='77aa97ac-40a8-4b5e-a50a-f6382389374c' and Vue.GuidVueInf=DansVue.GuidVue and Vue.GuidVue=ServerLink.GuidVue and dansvue.guidobjet=gtechlink.GuidGTechLink and gtechlink.guidtechlink=techlink.GuidTechLink and techlink.GuidServerIn=ServerLink.GuidServer and ServerLink.GuidServerPhy=ServerPhy.GuidServerPhy and ServerPhy.GuidServerPhy=NCard.GuidHote and NCard.GuidNCard=Alias.GuidNCard order by GuidTechLink;
            string sGuidExtremite = "GuidServerIn";

            if (sTypeExtremiteFlux == "Cible")
            {
                sGuidExtremite = "GuidServerOut";
            }
            if (oCnxBase.CBRecherche("SELECT distinct TechLink.Id, TechLink.GuidTechLink, " + sGuidExtremite + ", NomGen" + sTypeObjet + ", Ins" + sTypeObjet  + ".GuidIns" + sTypeObjet + ", Ins" + sTypeObjet + ".NomIns" + sTypeObjet + ", ' ', NCard.GuidNCard, IPAddr, Alias.GuidAlias, NomAlias, NomNCard, IPNat, VLan.GuidVLan, VlanClass.GuidVlanClass " +
                "FROM Vue vDeploy, Vue vInfra, Dansvue vTechlink, GTechLink, TechLink, Gen" + sTypeObjet + ", Ins" + sTypeObjet + ", Inspod, Insns, GInsks, Dansvue vIns, NCard  left join Alias on NCard.GuidNCard = Alias.GuidNCard left join VLan on NCard.GuidVLan=VLan.GuidVLan left join VlanClass on VLan.GuidVlanClass=VlanClass.GuidVlanClass " +
                " WHERE vDeploy.GuidVue = '" + sGuidDeploy + "' and vDeploy.GuidVueInf = vInfra.GuidVue and vInfra.GuidGVue = vTechlink.GuidGVue and " +
                       "vTechlink.GuidObjet = GTechLink.GuidGTechLink and GTechLink.GuidTechLink = TechLink.GuidTechLink and " + sGuidExtremite + " = Gen" + sTypeObjet + ".GuidGen" + sTypeObjet + " and " +
                       "Gen" + sTypeObjet + ".GuidGen" + sTypeObjet + " = Ins" + sTypeObjet + ".GuidGen" + sTypeObjet + " and Ins" + sTypeObjet + ".GuidInspod = Inspod.GuidInspod and Inspod.GuidInsns = Insns.GuidInsns and Insns.GuidInsks = GInsks.GuidInsks and " +
                       "GInsks.GuidGInsks = vIns.GuidObjet and vIns.GuidGVue = vDeploy.GuidGVue and Ins" + sTypeObjet + ".GuidIns" + sTypeObjet + " = NCard.GuidHote order by TechLink.Id, TechLink.GuidTechLink"))
                CompleteXmlFluxFromResultSql(xmlFlux, sTypeExtremiteFlux, sTypeObjet);
            oCnxBase.CBReaderClose();

            if (oCnxBase.CBRecherche("SELECT distinct TechLink.Id, TechLink.GuidTechLink, " + sGuidExtremite + ", NomGen" + sTypeObjet + ", Insnd.GuidInsnd, Insnd.NomInsnd, ' ', NCard.GuidNCard, IPAddr, Alias.GuidAlias, NomAlias, NomNCard, IPNat, VLan.GuidVLan, VlanClass.GuidVlanClass " +
                "FROM Vue vDeploy, Vue vInfra, Dansvue vTechlink, GTechLink, TechLink, Gen" + sTypeObjet + ", Ins" + sTypeObjet + ", Inspod, Insns, GInsks, Dansvue vIns, Insnd, NCard  left join Alias on NCard.GuidNCard = Alias.GuidNCard left join VLan on NCard.GuidVLan=VLan.GuidVLan left join VlanClass on VLan.GuidVlanClass=VlanClass.GuidVlanClass " +
                " WHERE vDeploy.GuidVue = '" + sGuidDeploy + "' and vDeploy.GuidVueInf = vInfra.GuidVue and vInfra.GuidGVue = vTechlink.GuidGVue and " +
                       "vTechlink.GuidObjet = GTechLink.GuidGTechLink and GTechLink.GuidTechLink = TechLink.GuidTechLink and " + sGuidExtremite + " = Gen" + sTypeObjet + ".GuidGen" + sTypeObjet + " and " +
                       "Gen" + sTypeObjet + ".GuidGen" + sTypeObjet + " = Ins" + sTypeObjet + ".GuidGen" + sTypeObjet + " and Ins" + sTypeObjet + ".GuidInspod = Inspod.GuidInspod and Inspod.GuidInsns = Insns.GuidInsns and Insns.GuidInsks = GInsks.GuidInsks and " +
                       "GInsks.GuidGInsks = vIns.GuidObjet and vIns.GuidGVue = vDeploy.GuidGVue and GInsks.GuidInsks = insnd.GuidInsks and Insnd.GuidInsnd = NCard.GuidHote  order by TechLink.Id, TechLink.GuidTechLink"))
                CompleteXmlFluxFromResultSql(xmlFlux, sTypeExtremiteFlux, sTypeObjet);
            oCnxBase.CBReaderClose();
            /*
            ArrayList lstGuidCluster = new ArrayList();
            if (oCnxBase.CBRecherche("SELECT distinct TechLink.Id, TechLink.GuidTechLink, " + sGuidExtremite + ",NULL, Cluster.GuidCluster, NomCluster, NCard.GuidNCard, IPAddr, Alias.GuidAlias, NomAlias, NomNCard, IPNat FROM Vue vDeploy, Vue vInfra, Dansvue, GTechLink, TechLink, " + sTypeObjet + "Link, serverphy, cluster, NCard  left join Alias on NCard.GuidNCard=Alias.GuidNCard WHERE VDeploy.GuidVue='" + sGuidDeploy + "'and vDeploy.GuidVueInf=vInfra.GuidVue and vInfra.GuidGVue=DansVue.GuidGVue and vDeploy.GuidVue=" + sTypeObjet + "Link.GuidVue and DansVue.GuidObjet=GTechLink.GuidGTechLink and GTechLink.GuidTechLink=TechLink.GuidTechLink and TechLink." + sGuidExtremite + "=" + sTypeObjet + "Link.Guid" + sTypeObjet + " and " + sTypeObjet + "Link.GuidServerPhy=ServerPhy.GuidServerPhy and ServerPhy.GuidCluster=Cluster.GuidCluster and Cluster.GuidCluster=NCard.GuidHote " + sNCard + " order by TechLink.Id, TechLink.GuidTechLink"))
                lstGuidCluster = CompleteXmlFluxFromResultSql(xmlFlux, sTypeExtremiteFlux, sTypeObjet, "Cluster");
            oCnxBase.CBReaderClose();
            XmlElement elClusters = xmlFlux.XmlGetFirstElFromName(xmlFlux.root, "Clusters");
            for (int i = 0; i < lstGuidCluster.Count; i++)
            {
                XmlElement elCluster = xmlFlux.XmlFindElFromAtt(elClusters, "Guid", (string)lstGuidCluster[i]);
                if (elCluster == null)
                {
                    //Create le cluster et ajout des serveurs sur cluster
                    elCluster = xmlFlux.XmlCreatEl(elClusters, "Cluster");
                    elCluster.SetAttribute("Guid", (string)lstGuidCluster[i]);
                    if (oCnxBase.CBRecherche("Select GuidServerPhy, NomServerPhy From ServerPhy Where GuidCluster='" + lstGuidCluster[i] + "'"))
                    {
                        while (oCnxBase.Reader.Read())
                        {
                            XmlElement elSrv = xmlFlux.XmlCreatEl(elCluster, "Server");
                            elSrv.SetAttribute("Guid", oCnxBase.Reader.GetString(0));
                            elSrv.SetAttribute("Nom", oCnxBase.Reader.GetString(1));
                        }

                    }
                    oCnxBase.CBReaderClose();
                }
            }
            */

            /*for(int i=0; i< lstGuidCluster.Count; i++)
            {
                if (oCnxBase.CBRecherche("Select distinct GuidServerPhy, NomServerPhy From Cluster, ServerPhy Where ServerPhy.GuidCluster=Cluster.GuidCluster and Cluster.GuidCluster='" + lstGuidCluster[i] + "'"))
                    CompleteXmlFluxFromResultSqlCluster(xmlFlux, sTypeExtremiteFlux, (string)lstGuidCluster[i]);
                oCnxBase.CBReaderClose();
            }
            oCnxBase.CBReaderClose();*/
        }

        public void CompleteXmlFluxLabelCloud(XmlExcel xmlFlux, string sTypeExtremiteFlux, string sTypeObjet, string sGuidDeploy)
        {
            // SELECT distinct techlink.GuidTechLink, techlink.NomTechLink, techlink.GuidServerIn, NomServerPhy, IPAddr, NomAlias  FROM Vue, dansvue, gtechlink, techlink, serverlink, serverphy, NCard, Alias where Vue.GuidVue='77aa97ac-40a8-4b5e-a50a-f6382389374c' and Vue.GuidVueInf=DansVue.GuidVue and Vue.GuidVue=ServerLink.GuidVue and dansvue.guidobjet=gtechlink.GuidGTechLink and gtechlink.guidtechlink=techlink.GuidTechLink and techlink.GuidServerIn=ServerLink.GuidServer and ServerLink.GuidServerPhy=ServerPhy.GuidServerPhy and ServerPhy.GuidServerPhy=NCard.GuidHote and NCard.GuidNCard=Alias.GuidNCard order by GuidTechLink;
            string sGuidExtremite = "GuidServerIn";

            string sqlComposantLabel = "", sqlFlux = "", sqlServerPhyLabel = "";

            if (sTypeExtremiteFlux == "Cible")
            {
                sGuidExtremite = "GuidServerOut";
            }

            

            if (sTypeObjet == "Server" || sTypeObjet == "Application" || sTypeObjet== "AppUser")
            {
                sqlServerPhyLabel =
                    // Objets ServerPhy
                    "select distinct LabelLink.GuidObjet GuidObjet, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from LabelLink, Label, LabelClass " +
                    " where LabelLink.GuidLabel = Label.GuidLabel and Label.GuidLabelClass = LabelClass.GuidLabelClass " +
                    // Objet Vue
                    "union select distinct GServerPhy.GuidServerPhy GuidObjet, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from GServerPhy, DansVue, Vue, LabelLink, Label, LabelClass " +
                    " where GuidVue = '" + sGuidDeploy + "' and GServerPhy.GuidGServerPhy = DansVue.GuidObjet and DansVue.GuidGVue = Vue.GuidGVue and GuidVue = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel and Label.GuidLabelClass = LabelClass.GuidLabelClass " +
                    // Objets Server (vue inf)
                    "union select distinct ServerLink.GuidServerPhy GuidObjet, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from ServerLink, LabelLink, Label, LabelClass " +
                    " where GuidVue = '" + sGuidDeploy + "' and GuidServer = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel and Label.GuidLabelClass = LabelClass.GuidLabelClass " +
                    // Objets Application (vue inf)
                    "union select distinct ApplicationLink.GuidServerPhy GuidObjet, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from ApplicationLink, LabelLink, Label, LabelClass " +
                    " where GuidVue = '" + sGuidDeploy + "' and GuidApplication = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel and Label.GuidLabelClass = LabelClass.GuidLabelClass " +
                    // Objets AppUser (vue inf)
                    "union select distinct AppUserLink.GuidServerPhy GuidObjet, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from AppUserLink, LabelLink, Label, LabelClass " +
                    " where GuidVue = '" + sGuidDeploy + "' and GuidAppUser = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel and Label.GuidLabelClass = LabelClass.GuidLabelClass " +
                    //Objet Application meme
                    "union select distinct ServerLink.GuidServerPhy GuidObjet, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from Vue, AppVersion, ServerLink, LabelLink, Label, LabelClass " +
                    " where Vue.GuidVue = '" + sGuidDeploy + "' and Vue.GuidVue = ServerLink.GuidVue and Vue.GuidAppVersion = AppVersion.GuidAppVersion and GuidApplication = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel and Label.GuidLabelClass = LabelClass.GuidLabelClass";

                sqlFlux =
                    "select distinct TechLink.Id, TechLink.GuidTechLink, " + sGuidExtremite + ", " + sTypeObjet + ".Nom" + sTypeObjet + ", ServerPhy.GuidServerPhy, NomServerPhy, NomLocation, vLabel.GuidLabelClass, vLabel.NomLabelClass, vLabel.GuidLabel, vLabel.NomLabel " +
                    "from Vue vDeploy, Vue vInfra, Dansvue, GTechLink, TechLink, " + sTypeObjet + "Link, " + sTypeObjet + ", serverphy, Location,  (" + sqlServerPhyLabel + ") vLabel " +
                    "where vDeploy.GuidVue = '" + sGuidDeploy + "' and vDeploy.GuidVueInf = vInfra.GuidVue and vInfra.GuidGVue = DansVue.GuidGVue and vDeploy.GuidVue = " + sTypeObjet + "Link.GuidVue and " + sGuidExtremite + " = " + sTypeObjet + ".Guid" + sTypeObjet + " and " +
                        "DansVue.GuidObjet = GTechLink.GuidGTechLink and GTechLink.GuidTechLink = TechLink.GuidTechLink and TechLink." + sGuidExtremite + " = " + sTypeObjet + "Link.Guid" + sTypeObjet + " and " + sTypeObjet + "Link.GuidServerPhy = ServerPhy.GuidServerPhy and ServerPhy.GuidLocation = Location.GuidLocation and ServerPhy.GuidServerPhy = vLabel.GuidObjet " +
                    "order by TechLink.Id, TechLink.GuidTechLink";


            } else if (sTypeObjet == "pod")
            {
                sqlComposantLabel =
                    // Objets Ins<obj> // svc, pod, ing
                    "select distinct Ins" + sTypeObjet + ".GuidIns" + sTypeObjet + "  GuidObjLabel, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from Ins" + sTypeObjet + ", LabelLink, Label, LabelClass" +
                    " where Ins" + sTypeObjet + ".GuidIns" + sTypeObjet + " = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel and Label.GuidLabelClass = LabelClass.GuidLabelClass " +
                    // Objet Gen<obj> // svc, pod, ing
                    "union select distinct Ins" + sTypeObjet + ".GuidIns" + sTypeObjet + "  GuidObjLabel, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from Ins" + sTypeObjet + ", LabelLink, Label, LabelClass" +
                    " where Ins" + sTypeObjet + ".GuidGen" + sTypeObjet + " = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel and Label.GuidLabelClass = LabelClass.GuidLabelClass " +
                    // Objet Insns
                    "union select distinct Ins" + sTypeObjet + ".GuidIns" + sTypeObjet + "  GuidObjLabel, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from Ins" + sTypeObjet + ", LabelLink, Label, LabelClass" +
                    " where Ins" + sTypeObjet + ".GuidInsns = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel  and Label.GuidLabelClass = LabelClass.GuidLabelClass " +
                    // Objet Insks
                    "union select distinct Ins" + sTypeObjet + ".GuidIns" + sTypeObjet + "  GuidObjLabel, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from Ins" + sTypeObjet + ", Insns, LabelLink, Label, LabelClass" +
                    " where Ins" + sTypeObjet + ".GuidInsns = Insns.GuidInsns and Insns.GuidInsks = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel  and Label.GuidLabelClass = LabelClass.GuidLabelClass " +
                    // Objet Genks
                    "union select distinct Ins" + sTypeObjet + ".GuidIns" + sTypeObjet + "  GuidObjLabel, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from Ins" + sTypeObjet + ", Insns, Insks, LabelLink, Label, LabelClass" +
                    " where Ins" + sTypeObjet + ".GuidInsns = Insns.GuidInsns and Insns.GuidInsks = Insks.GuidInsks and Insks.GuidGenks = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel and Label.GuidLabelClass = LabelClass.GuidLabelClass " +
                    // Objet Vue
                    "union select distinct Ins" + sTypeObjet + ".GuidIns" + sTypeObjet + "  GuidObjLabel, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from Ins" + sTypeObjet + ", Insns, GInsks, DansVue, Vue, LabelLink, Label, LabelClass" +
                    " where Vue.GuidVue = '" + sGuidDeploy + "' and Ins" + sTypeObjet + ".GuidInsns = Insns.GuidInsns and Insns.GuidInsks = GInsks.GuidInsks and GInsks.GuidGInsks = DansVue.GuidObjet and DansVue.GuidGVue = Vue.GuidGVue and Vue.GuidVue = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel  and Label.GuidLabelClass = LabelClass.GuidLabelClass " +
                    // Objet Application
                    "union select distinct Ins" + sTypeObjet + ".GuidIns" + sTypeObjet + "  GuidObjLabel, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from Ins" + sTypeObjet + ", Insns, GInsks, DansVue, Vue, AppVersion, LabelLink, Label, LabelClass" +
                    " where Vue.GuidVue = '" + sGuidDeploy + "' and Ins" + sTypeObjet + ".GuidInsns = Insns.GuidInsns and Insns.GuidInsks = GInsks.GuidInsks and GInsks.GuidGInsks = DansVue.GuidObjet and DansVue.GuidGVue = Vue.GuidGVue and Vue.GuidAppVersion = AppVersion.GuidAppVersion and GuidApplication = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel  and Label.GuidLabelClass = LabelClass.GuidLabelClass ";

                sqlFlux =
                    "select distinct TechLink.Id, TechLink.GuidTechLink, " + sGuidExtremite + ", NomGen" + sTypeObjet + ", Ins" + sTypeObjet + ".GuidIns" + sTypeObjet + ", NomIns" + sTypeObjet + ", ' ', vLabel.GuidLabelClass, vLabel.NomLabelClass, vLabel.GuidLabel, vLabel.NomLabel " +
                    "from Vue vDeploy, Vue vInfra, Dansvue vTechlink, GTechLink, TechLink, Gen" + sTypeObjet + ", Ins" + sTypeObjet + ", Insns, GInsks, Dansvue vIns, ( " + sqlComposantLabel + " ) vLabel " +
                    "where vDeploy.GuidVue = '" + sGuidDeploy + "' and vDeploy.GuidVueInf = vInfra.GuidVue and vInfra.GuidGVue = vTechlink.GuidGVue and vTechlink.GuidObjet = GTechLink.GuidGTechLink and GTechLink.GuidTechLink = TechLink.GuidTechLink and " +
                        sGuidExtremite + " = Gen" + sTypeObjet + ".GuidGen" + sTypeObjet + " and Gen" + sTypeObjet + ".GuidGen" + sTypeObjet + " = Ins" + sTypeObjet + ".GuidGen" + sTypeObjet + " and Ins" + sTypeObjet + ".GuidInsns = Insns.GuidInsns and Insns.GuidInsks = GInsks.GuidInsks and GInsks.GuidGInsks = vIns.GuidObjet and vIns.GuidGVue = vDeploy.GuidGVue and Ins" + sTypeObjet + ".GuidIns" + sTypeObjet + " = vLabel.GuidObjLabel " +
                    "order by TechLink.Id, TechLink.GuidTechLink";
            }
            else if (sTypeObjet == "svc" || sTypeObjet == "ing")
            {
                sqlComposantLabel =
                    // Objets Ins<obj> // svc, ing
                    "select distinct Ins" + sTypeObjet + ".GuidIns" + sTypeObjet + "  GuidObjLabel, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from Ins" + sTypeObjet + ", LabelLink, Label, LabelClass" +
                    " where Ins" + sTypeObjet + ".GuidIns" + sTypeObjet + " = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel and Label.GuidLabelClass = LabelClass.GuidLabelClass " +
                    // Objet Gen<obj> // svc, ing
                    "union select distinct Ins" + sTypeObjet + ".GuidIns" + sTypeObjet + "  GuidObjLabel, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from Ins" + sTypeObjet + ", LabelLink, Label, LabelClass" +
                    " where Ins" + sTypeObjet + ".GuidGen" + sTypeObjet + " = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel and Label.GuidLabelClass = LabelClass.GuidLabelClass " +
                    // Objet Insns
                    "union select distinct Ins" + sTypeObjet + ".GuidIns" + sTypeObjet + "  GuidObjLabel, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from Ins" + sTypeObjet + ", Inspod, LabelLink, Label, LabelClass" +
                    " where Ins" + sTypeObjet + ".GuidInspod = Inspod.GuidInspod and Inspod.GuidInsns = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel  and Label.GuidLabelClass = LabelClass.GuidLabelClass " +
                    // Objet Insks
                    "union select distinct Ins" + sTypeObjet + ".GuidIns" + sTypeObjet + "  GuidObjLabel, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from Ins" + sTypeObjet + ", Inspod, Insns, LabelLink, Label, LabelClass" +
                    " where Ins" + sTypeObjet + ".GuidInspod = Inspod.GuidInspod and Inspod.GuidInsns = Insns.GuidInsns and Insns.GuidInsks = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel  and Label.GuidLabelClass = LabelClass.GuidLabelClass " +
                    // Objet Genks
                    "union select distinct Ins" + sTypeObjet + ".GuidIns" + sTypeObjet + "  GuidObjLabel, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from Ins" + sTypeObjet + ", Inspod, Insns, Insks, LabelLink, Label, LabelClass" +
                    " where Ins" + sTypeObjet + ".GuidInspod = Inspod.GuidInspod and Inspod.GuidInsns = Insns.GuidInsns and Insns.GuidInsks = Insks.GuidInsks and Insks.GuidGenks = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel and Label.GuidLabelClass = LabelClass.GuidLabelClass " +
                    // Objet Vue
                    "union select distinct Ins" + sTypeObjet + ".GuidIns" + sTypeObjet + "  GuidObjLabel, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from Ins" + sTypeObjet + ", Inspod, Insns, GInsks, DansVue, Vue, LabelLink, Label, LabelClass" +
                    " where Vue.GuidVue = '" + sGuidDeploy + "' and Ins" + sTypeObjet + ".GuidInspod = Inspod.GuidInspod and Inspod.GuidInsns = Insns.GuidInsns and Insns.GuidInsks = GInsks.GuidInsks and GInsks.GuidGInsks = DansVue.GuidObjet and DansVue.GuidGVue = Vue.GuidGVue and Vue.GuidVue = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel  and Label.GuidLabelClass = LabelClass.GuidLabelClass " +
                    // Objet Application
                    "union select distinct Ins" + sTypeObjet + ".GuidIns" + sTypeObjet + "  GuidObjLabel, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from Ins" + sTypeObjet + ", Inspod, Insns, GInsks, DansVue, Vue, AppVersion, LabelLink, Label, LabelClass" +
                    " where Vue.GuidVue = '" + sGuidDeploy + "' and Ins" + sTypeObjet + ".GuidInspod = Inspod.GuidInspod and Inspod.GuidInsns = Insns.GuidInsns and Insns.GuidInsks = GInsks.GuidInsks and GInsks.GuidGInsks = DansVue.GuidObjet and DansVue.GuidGVue = Vue.GuidGVue and Vue.GuidAppVersion = AppVersion.GuidAppVersion and GuidApplication = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel  and Label.GuidLabelClass = LabelClass.GuidLabelClass ";

                sqlFlux =
                    "select distinct TechLink.Id, TechLink.GuidTechLink, " + sGuidExtremite + ", NomGen" + sTypeObjet + ", Ins" + sTypeObjet + ".GuidIns" + sTypeObjet + ", NomIns" + sTypeObjet + ", ' ', vLabel.GuidLabelClass, vLabel.NomLabelClass, vLabel.GuidLabel, vLabel.NomLabel " +
                    "from Vue vDeploy, Vue vInfra, Dansvue vTechlink, GTechLink, TechLink, Gen" + sTypeObjet + ", Ins" + sTypeObjet + ", Inspod, Insns, GInsks, Dansvue vIns, ( " + sqlComposantLabel + " ) vLabel " +
                    "where vDeploy.GuidVue = '" + sGuidDeploy + "' and vDeploy.GuidVueInf = vInfra.GuidVue and vInfra.GuidGVue = vTechlink.GuidGVue and vTechlink.GuidObjet = GTechLink.GuidGTechLink and GTechLink.GuidTechLink = TechLink.GuidTechLink and " +
                        sGuidExtremite + " = Gen" + sTypeObjet + ".GuidGen" + sTypeObjet + " and Gen" + sTypeObjet + ".GuidGen" + sTypeObjet + " = Ins" + sTypeObjet + ".GuidGen" + sTypeObjet + " and Ins" + sTypeObjet + ".GuidInspod = Inspod.GuidInspod and Inspod.GuidInsns = Insns.GuidInsns and Insns.GuidInsks = GInsks.GuidInsks and GInsks.GuidGInsks = vIns.GuidObjet and vIns.GuidGVue = vDeploy.GuidGVue and Ins" + sTypeObjet + ".GuidIns" + sTypeObjet + " = vLabel.GuidObjLabel " +
                    "order by TechLink.Id, TechLink.GuidTechLink";
            } else if (sTypeObjet == "sas")
            {
                sqlComposantLabel =
                    // Objets Inssas
                    "select distinct Inssas.GuidInssas GuidObjLabel, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from Inssas, LabelLink, Label, LabelClass" +
                    " where Inssas.GuidInssas = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel and Label.GuidLabelClass = LabelClass.GuidLabelClass " +
                    // Objets Gensas
                    "union select distinct Inssas.GuidInssas GuidObjLabel, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from Inssas, LabelLink, Label, LabelClass" +
                    " where Inssas.GuidGensas = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel and Label.GuidLabelClass = LabelClass.GuidLabelClass " +
                    // Objet Vue
                    "union select distinct GInssas.GuidInssas GuidObjLabel, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from GInssas, DansVue, Vue, LabelLink, Label, LabelClass " +
                    " where Vue.GuidVue = '" + sGuidDeploy + "' and GInssas.GuidGInssas = DansVue.GuidObjet and DansVue.GuidGVue = Vue.GuidGVue and GuidVue = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel and Label.GuidLabelClass = LabelClass.GuidLabelClass " +
                    // Objet Application
                    "union select distinct GInssas.GuidInssas GuidObjLabel, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from GInssas, DansVue, Vue, AppVersion, LabelLink, Label, LabelClass " +
                    " where Vue.GuidVue = '" + sGuidDeploy + "' and GInssas.GuidGInssas = DansVue.GuidObjet and DansVue.GuidGVue = Vue.GuidGVue and Vue.GuidAppVersion = AppVersion.GuidAppVersion and GuidApplication = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel and Label.GuidLabelClass = LabelClass.GuidLabelClass ";
                    
                sqlFlux =
                    "select distinct TechLink.Id, TechLink.GuidTechLink, " + sGuidExtremite + ", NomGensas, Inssas.GuidInssas, NomInssas, ' ', vLabel.GuidLabelClass, vLabel.NomLabelClass, vLabel.GuidLabel, vLabel.NomLabel " +
                    "from Vue vDeploy, Vue vInfra, Dansvue vTechlink, GTechLink, TechLink, Gensas, Inssas, GInssas, Dansvue vIns, (" + sqlComposantLabel + ") vLabel " +
                    "where vDeploy.GuidVue = '" + sGuidDeploy + "' and vDeploy.GuidVueInf = vInfra.GuidVue and vInfra.GuidGVue = vTechlink.GuidGVue and vTechlink.GuidObjet = GTechLink.GuidGTechLink and " +
                    " GTechLink.GuidTechLink = TechLink.GuidTechLink and " + sGuidExtremite + " = Gensas.GuidGensas and Gensas.GuidGensas = Inssas.GuidGensas and Inssas.GuidInssas = GInssas.GuidInssas and " +
                    " GInssas.GuidGInssas = vIns.GuidObjet and vIns.GuidGVue = vDeploy.GuidGVue and Inssas.GuidInssas = vLabel.GuidObjLabel " +
                    "order by TechLink.Id, TechLink.GuidTechLink";

            }

            if (oCnxBase.CBRecherche(sqlFlux))
                CompleteXmlFluxFromResultSqlLabel(xmlFlux, sTypeExtremiteFlux, sTypeObjet);
            oCnxBase.CBReaderClose();
            
        }

        public void CompleteXmlFluxAppCloud(XmlExcel xmlFlux, string sTypeExtremiteFlux, string sGuidDeploy)
        {
            // SELECT distinct techlink.GuidTechLink, techlink.NomTechLink, techlink.GuidServerIn, NomServerPhy, IPAddr, NomAlias  FROM Vue, dansvue, gtechlink, techlink, serverlink, serverphy, NCard, Alias where Vue.GuidVue='77aa97ac-40a8-4b5e-a50a-f6382389374c' and Vue.GuidVueInf=DansVue.GuidVue and Vue.GuidVue=ServerLink.GuidVue and dansvue.guidobjet=gtechlink.GuidGTechLink and gtechlink.guidtechlink=techlink.GuidTechLink and techlink.GuidServerIn=ServerLink.GuidServer and ServerLink.GuidServerPhy=ServerPhy.GuidServerPhy and ServerPhy.GuidServerPhy=NCard.GuidHote and NCard.GuidNCard=Alias.GuidNCard order by GuidTechLink;
            string sGuidExtremite = "GuidServerIn";

            string sqlFlux = "", sqlServerPhyLabel = "";

            if (sTypeExtremiteFlux == "Cible")
            {
                sGuidExtremite = "GuidServerOut";
            }


            sqlServerPhyLabel = //getsqlServerPhyLabel();
                // Objet Application
                "select distinct Inssas.GuidInssas GuidObjLabel, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass " +
                "from Gensas, Inssas, GInssas, DansVue, Vue, AppVersion, LabelLink, Label, LabelClass " +
                " where Vue.GuidVue = '" + sGuidDeploy + "' and Gensas.GuidGensas = Inssas.GuidGensas and Inssas.guidinssas = ginssas.guidinssas and GInssas.GuidGInssas = DansVue.GuidObjet and DansVue.GuidGVue = Vue.GuidGVue and gensas.GuidAppVersion = AppVersion.GuidAppVersion and GuidApplication = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel and Label.GuidLabelClass = LabelClass.GuidLabelClass " +

                // Objet Vue
                "union select distinct GInssas.GuidInssas GuidObjLabel, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from GInssas, DansVue, Vue, LabelLink, Label, LabelClass " +
                " where Vue.GuidVue = '" + sGuidDeploy + "' and GInssas.GuidGInssas = DansVue.GuidObjet and DansVue.GuidGVue = Vue.GuidGVue and GuidVue = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel and Label.GuidLabelClass = LabelClass.GuidLabelClass " +

                // Objet Infra
                "union select distinct Inssas.GuidInssas GuidObjLabel, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from Inssas, GInssas, Dansvue, Vue, LabelLink, Label, LabelClass" +
                " where Vue.GuidVue = '" + sGuidDeploy + "' and inssas.GuidInssas = ginssas.GuidInssas and ginssas.GuidgInssas = dansvue.guidobjet and dansvue.guidgvue = vue.guidgvue and Inssas.GuidGensas = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel and Label.GuidLabelClass = LabelClass.GuidLabelClass " +

                //Objet Deploy
                "union select distinct Inssas.GuidInssas GuidObjLabel, Label.GuidLabel, NomLabel, LabelClass.GuidLabelClass, NomLabelClass from Inssas, GInssas, Dansvue, Vue, LabelLink, Label, LabelClass" +
                " where Vue.GuidVue = '" + sGuidDeploy + "' and inssas.GuidInssas = ginssas.GuidInssas and ginssas.GuidgInssas = dansvue.guidobjet and dansvue.guidgvue = vue.guidgvue and Inssas.GuidInssas = LabelLink.GuidObjet and LabelLink.GuidLabel = Label.GuidLabel and Label.GuidLabelClass = LabelClass.GuidLabelClass ";
                
                
            sqlFlux =
                "select distinct TechLink.Id, TechLink.GuidTechLink, " + sGuidExtremite + ", Nomapplication, inssas.guidInssas, NomInssas, ' ', vLabel.GuidLabelClass, vLabel.NomLabelClass, vLabel.GuidLabel, vLabel.NomLabel " +
                "from Vue vDeploy, Vue vInfra, Dansvue vTechlink, GTechLink, TechLink, Appversion, Application, inssas, gensas, ( " + sqlServerPhyLabel + " ) vLabel " +
                "where vDeploy.GuidVue = '" + sGuidDeploy + "' and vDeploy.GuidVueInf = vInfra.GuidVue and vInfra.GuidGVue = vTechlink.GuidGVue and vTechlink.GuidObjet = GTechLink.GuidGTechLink and GTechLink.GuidTechLink = TechLink.GuidTechLink and " +
                  sGuidExtremite + " = application.guidapplication and appversion.guidappversion = gensas.guidappversion and gensas.guidgensas = inssas.guidgensas and application.guidapplication = appversion.GuidApplication and inssas.guidInssas = vLabel.GuidObjLabel " +
                "order by TechLink.Id, TechLink.GuidTechLink";

            if (oCnxBase.CBRecherche(sqlFlux))
                CompleteXmlFluxFromResultSqlLabel(xmlFlux, sTypeExtremiteFlux, "Application");
            oCnxBase.CBReaderClose();

        }

        public void CommandVisio()
        {
            //double xPage = 8.66, yPage = 13.69; // origine
            //double qxPage = xPage / 1000, qyPage = yPage / 1414; // origine
            //double xPage = 10.3, yPage = 11.67; // A4 Portrait
            double xPage = 10.3, yPage = 8.25; // A4 Paysage
            double qxPage = xPage / 1010, qyPage = yPage / 770;

            MOI.Visio.Application appvis = new MOI.Visio.Application();
            MOI.Visio.Document docvis = appvis.Documents.Add("");
            MOI.Visio.Page page = appvis.ActivePage;
            MOI.Visio.Document docLiens = appvis.Documents.Add(sPathRoot + @"\Liens.vss");
            MOI.Visio.Master masterLink = docLiens.Masters.get_ItemU("Dynamic Connector");
            MOI.Visio.Master masterEth = docLiens.Masters.get_ItemU("Ethernet");
            ArrayList lstGuid = new ArrayList();
            ArrayList lstShape = new ArrayList();

            for (int i = 0; i < drawArea.GraphicsList.Count; i++)
            {
                DrawObject o = (DrawObject)drawArea.GraphicsList[i];
                if (o.GetType() != typeof(DrawLink) || o.GetType() != typeof(DrawTechLink) || o.GetType() != typeof(DrawVLan))
                    o.VisioDraw(lstGuid, lstShape, page, yPage, qxPage, qyPage);
            }

            for (int i = 0; i < drawArea.GraphicsList.Count; i++)
            {
                DrawObject o = (DrawObject)drawArea.GraphicsList[i];

                if (o.GetType() == typeof(DrawLink) || o.GetType() == typeof(DrawTechLink) || o.GetType() == typeof(DrawInterLink))
                {
                    DrawPolygon om = (DrawPolygon)o;
                    ArrayList lstP = om.pointArray;
                    ArrayList lstd = new ArrayList();
                    List<double> doubles = new List<double>();
                    for (int j = 0; j < lstP.Count; j++)
                    {
                        doubles.Add(((double)((Point)lstP[j]).X * qxPage));
                        doubles.Add(((double)yPage - ((Point)lstP[j]).Y * qyPage));
                    }
                    Array xyarray = doubles.ToArray();
                    //MOI.Visio.Shape shapeL = page.DrawPolyline(ref xyarray, 8);

                    MOI.Visio.Shape shapeLien = page.Drop(masterLink, 0, 0);


                    //MOI.Visio.Shape shapeC = page.
                    //shapeL.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineEndArrow).ResultIU = 8;
                    shapeLien.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineEndArrow).ResultIU = 8;

                    //Cretation Point de connection In
                    string SearchGuid = ((DrawObject)o.LstLinkIn[0]).GuidkeyObjet.ToString();
                    int idx = drawArea.GraphicsList.FindObjet(0, SearchGuid);
                    if (idx > -1)
                    {
                        DrawRectangle oRec = (DrawRectangle)drawArea.GraphicsList[idx];
                        MOI.Visio.Shape shapeRec = (MOI.Visio.Shape)lstShape[lstGuid.IndexOf(SearchGuid)];

                        short connectorRow = -1;
                        connectorRow = shapeRec.AddRow((short)MOI.Visio.VisSectionIndices.visSectionConnectionPts, (short)MOI.Visio.VisRowIndices.visRowLast, (short)MOI.Visio.VisRowTags.visTagDefault);
                        MOI.Visio.Cell XCell = shapeRec.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionConnectionPts, connectorRow, (short)MOI.Visio.VisCellIndices.visCnnctX);
                        XCell.ResultIU = (((Point)lstP[0]).X - oRec.Rectangle.Left) * qxPage;
                        MOI.Visio.Cell YCell = shapeRec.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionConnectionPts, connectorRow, (short)MOI.Visio.VisCellIndices.visCnnctY);
                        YCell.ResultIU = (oRec.Rectangle.Bottom - ((Point)lstP[0]).Y) * qyPage;

                        //Attach Link
                        //MOI.Visio.Cell Attach = shapeL.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXForm1D, (short)MOI.Visio.VisCellIndices.vis1DBeginX);
                        MOI.Visio.Cell Attach = shapeLien.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXForm1D, (short)MOI.Visio.VisCellIndices.vis1DBeginX);
                        Attach.GlueTo(shapeRec.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionConnectionPts, connectorRow, (short)MOI.Visio.VisCellIndices.visCnnctX));

                    }

                    //Cretation Point de connection Out
                    SearchGuid = ((DrawObject)o.LstLinkOut[0]).GuidkeyObjet.ToString();
                    idx = drawArea.GraphicsList.FindObjet(0, SearchGuid);
                    if (idx > -1)
                    {
                        DrawRectangle oRec = (DrawRectangle)drawArea.GraphicsList[idx];
                        MOI.Visio.Shape shapeRec = (MOI.Visio.Shape)lstShape[lstGuid.IndexOf(SearchGuid)];

                        short connectorRow = -1;
                        connectorRow = shapeRec.AddRow((short)MOI.Visio.VisSectionIndices.visSectionConnectionPts, (short)MOI.Visio.VisRowIndices.visRowLast, (short)MOI.Visio.VisRowTags.visTagDefault);
                        MOI.Visio.Cell XCell = shapeRec.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionConnectionPts, connectorRow, (short)MOI.Visio.VisCellIndices.visCnnctX);
                        XCell.ResultIU = (((Point)lstP[lstP.Count - 1]).X - oRec.Rectangle.Left) * qxPage;
                        MOI.Visio.Cell YCell = shapeRec.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionConnectionPts, connectorRow, (short)MOI.Visio.VisCellIndices.visCnnctY);
                        YCell.ResultIU = (oRec.Rectangle.Bottom - ((Point)lstP[lstP.Count - 1]).Y) * qyPage;

                        //Attach Link shapeLien
                        //MOI.Visio.Cell Attach = shapeL.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXForm1D, (short)MOI.Visio.VisCellIndices.vis1DEndX);
                        MOI.Visio.Cell Attach = shapeLien.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXForm1D, (short)MOI.Visio.VisCellIndices.vis1DEndX);
                        Attach.GlueTo(shapeRec.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionConnectionPts, connectorRow, (short)MOI.Visio.VisCellIndices.visCnnctX));

                    }
                }
                else if (o.GetType() == typeof(DrawVLan))
                {
                    DrawVLan dvlan = (DrawVLan)o;

                    Color Couleur = Color.Black;
                    int n = dvlan.GetIndexFromName("Couleur");
                    if (n > -1) Couleur = Color.FromName((string)dvlan.LstValue[n]);

                    int iCnx = 0, CnxMax = 5;
                    MOI.Visio.Shape shapeEth = page.Drop(masterEth, (dvlan.Rectangle.Left + dvlan.Rectangle.Width / 2) * qxPage, yPage - (dvlan.Rectangle.Top + dvlan.Rectangle.Height / 2) * qyPage);
                    //Taille
                    shapeEth.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormWidth).ResultIU = dvlan.Rectangle.Width * qxPage;
                    shapeEth.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormHeight).ResultIU = dvlan.Rectangle.Height * qyPage;
                    //Trait
                    shapeEth.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + Couleur.R.ToString() + "," + Couleur.G.ToString() + "," + Couleur.B.ToString() + ")";
                    //fond
                    shapeEth.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillBkgnd).FormulaU = "RGB(" + Couleur.R.ToString() + "," + Couleur.G.ToString() + "," + Couleur.B.ToString() + ")";
                    shapeEth.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillForegnd).FormulaU = "RGB(" + Color.White.R.ToString() + "," + Color.White.G.ToString() + "," + Color.White.B.ToString() + ")";
                    shapeEth.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillPattern).ResultIU = 28;
                    //texte
                    shapeEth.Text = "";

                    for (int iEth = 0; iEth < o.LstLinkOut.Count; iEth++)
                    {
                        string SearchGuid = ((DrawObject)o.LstLinkOut[iEth]).GuidkeyObjet.ToString();
                        int idx = drawArea.GraphicsList.FindObjet(0, SearchGuid);
                        if (idx > -1)
                        {
                            DrawRectangle oRec = (DrawRectangle)drawArea.GraphicsList[idx];
                            MOI.Visio.Shape shapeRec = (MOI.Visio.Shape)lstShape[lstGuid.IndexOf(SearchGuid)];

                            short connectorRow = -1;
                            connectorRow = shapeRec.AddRow((short)MOI.Visio.VisSectionIndices.visSectionConnectionPts, (short)MOI.Visio.VisRowIndices.visRowLast, (short)MOI.Visio.VisRowTags.visTagDefault);
                            int Ptidx = dvlan.GetAttchHandle(oRec.Rectangle);
                            if (Ptidx > -1 && dvlan.pointArray.Count > Ptidx)
                            {
                                Point pt = (Point)dvlan.pointArray[Ptidx];
                                MOI.Visio.Cell XCell = shapeRec.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionConnectionPts, connectorRow, (short)MOI.Visio.VisCellIndices.visCnnctX);
                                XCell.ResultIU = (pt.X - oRec.Rectangle.Left) * qxPage;
                                MOI.Visio.Cell YCell = shapeRec.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionConnectionPts, connectorRow, (short)MOI.Visio.VisCellIndices.visCnnctY);
                                YCell.ResultIU = (oRec.Rectangle.Bottom - pt.Y) * qyPage;

                                /*
                                MOI.Visio.Section Section =  shapeEth.get_Section((short)MOI.Visio.VisSectionIndices.visSectionControls);
                                int s = Section.Count;
                                short CtrlRow = shapeEth.AddRow((short)MOI.Visio.VisSectionIndices.visSectionControls, (short)MOI.Visio.VisRowIndices.visRowLast + 1, (short)MOI.Visio.VisRowTags.visTagDefault);
                                double aa = shapeRec.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionControls, (short)MOI.Visio.VisRowIndices.visRowLast-1, 0).ResultIU;
                                aa = shapeRec.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionControls, 0, 1).ResultIU;
                                aa = shapeRec.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionControls, 0, 2).ResultIU;
                                aa = shapeRec.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionControls, 0, 3).ResultIU;
                                aa = shapeRec.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionControls, 0, 4).ResultIU;
                                aa = shapeRec.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionControls, 0, 5).ResultIU;
                                aa = shapeRec.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionControls, 0, 6).ResultIU;


                                s = Section.Count;
                                */
                                //shapeEth.
                                if (iCnx >= CnxMax)
                                {
                                    shapeEth = page.Drop(masterEth, (dvlan.Rectangle.Left + dvlan.Rectangle.Width / 2) * qxPage, yPage - (dvlan.Rectangle.Top + dvlan.Rectangle.Height / 2) * qyPage);
                                    //Taille
                                    shapeEth.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormWidth).ResultIU = dvlan.Rectangle.Width * qxPage;
                                    shapeEth.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXFormIn, (short)MOI.Visio.VisCellIndices.visXFormHeight).ResultIU = dvlan.Rectangle.Height * qyPage;
                                    //Trait
                                    shapeEth.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowLine, (short)MOI.Visio.VisCellIndices.visLineColor).FormulaU = "RGB(" + Couleur.R.ToString() + "," + Couleur.G.ToString() + "," + Couleur.B.ToString() + ")";
                                    //fond
                                    shapeEth.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillBkgnd).FormulaU = "RGB(" + Couleur.R.ToString() + "," + Couleur.G.ToString() + "," + Couleur.B.ToString() + ")";
                                    shapeEth.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillForegnd).FormulaU = "RGB(" + Color.White.R.ToString() + "," + Color.White.G.ToString() + "," + Color.White.B.ToString() + ")";
                                    shapeEth.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowFill, (short)MOI.Visio.VisCellIndices.visFillPattern).ResultIU = 28;
                                    //texte
                                    shapeEth.Text = "";
                                    iCnx = 0;
                                }
                                MOI.Visio.Cell Attach = shapeEth.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionControls, (short)(MOI.Visio.VisRowIndices.visRowFirst + iCnx++), (short)MOI.Visio.VisCellIndices.vis1DBeginX);
                                //MOI.Visio.Cell Attach = shapeEth.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowXForm1D, (short)MOI.Visio.VisCellIndices.vis1DEndX);
                                Attach.GlueTo(shapeRec.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionConnectionPts, connectorRow, (short)MOI.Visio.VisCellIndices.visCnnctX));
                            }
                        }
                    }
                    dvlan.DrawGrpTxt(shapeEth, 2, 0, 0, 0, 0, Color.Black, Color.Transparent);

                    for (int idx = iCnx; idx < CnxMax; idx++)
                        shapeEth.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionControls, (short)(MOI.Visio.VisRowIndices.visRowFirst + idx), (short)MOI.Visio.VisCellIndices.vis1DBeginY).ResultIU = dvlan.Rectangle.Height / 2 * qxPage;
                }
            }
        }

        public void CommandPatrimoine()
        {
            ControlWord cw = null;
            cw = new ControlWord(this, sPathRoot + @"\Patrimoine_modele-V3R1-Eng.dot", true);
            if (cw != null)
            {
                string sBookPat = "Patrimoine";
                RapPatrimoine(cw, sBookPat, 1);

            }
        }

        void bAdd_Click(object sender, EventArgs e)
        {
            drawArea.AddObjet = true;
            setCtrlEnabled(bAdd, false);

            switch (tbTypeVue.Text[0])
            //switch (((string)cbTypeVue.SelectedItem)[0])
            {
                case '0': //0-Fonctionnelle
                    if (tvObjet.SelectedNode.Parent != null)
                    {
                        switch (tvObjet.SelectedNode.Parent.Text[0])
                        {
                            case 'U': //User
                                SwitchCommand(tbUser);
                                break;
                            case 'A': //Application
                                SwitchCommand(tbApplication);
                                break;
                        }
                    }

                    break;
                case '1': // 1-Applicative
                    if (tvObjet.SelectedNode.Parent != null)
                    {
                        switch (tvObjet.SelectedNode.Parent.Text[0])
                        {
                            case 'U': //User
                                SwitchCommand(tbUser);
                                break;
                            case 'A': //Application
#if APIREADY
                                //MessageBox.Show(tvObjet.SelectedNode.Name + " ; " + tvObjet.SelectedNode.Text);
                                using (var webClient = new System.Net.WebClient())
                                {
                                    webClient.Headers.Add("Content-Type", "application/json");
                                    webClient.Encoding = System.Text.UTF8Encoding.UTF8;
                                    webClient.DownloadDataCompleted += webClient_GetApplication;
                                    Uri urlToRequest = new Uri(@"http://localhost:3001/application/" + tvObjet.SelectedNode.Name + @"/proterties");
                                    webClient.DownloadDataAsync(urlToRequest);
                                }
#else
                                SwitchCommand(tbApplication);
#endif
                                break;
                            case 'M': //Module
                                SwitchCommand(tbCompFonc);
                                break;
                            case 'L': //Link
                                SwitchCommand(tbLinkA);
                                break;

                        }
                    }
                    break;

                case '2': // 2-Infrastructure
                    TreeNode tnRef = tvObjet.SelectedNode;
                    while (tnRef.Parent != null) tnRef = tnRef.Parent;
                    switch (tnRef.Name[0])
                    {
                        case 'U': //User
                            SwitchCommand(tbTechUser);
                            break;
                        case 'A': //Application
                            SwitchCommand(tbApplication);
                            break;
                        case 'L': //LSApplication
                            SwitchCommand(tbApplication);
                            break;
                        case 'F': //Fonction
                            SwitchCommand(tbServer);
                            break;
                        case '6': //6Managed Service
                            SwitchCommand(tbGensas);
                            break;
                        case '7': //7Container
                            SwitchCommand(tbContainer);
                            break;
                        case 'C': //Composant
                            SwitchCommand(tbMainComposant);
                            break;
                        case 'P': //Packages Applicatifs
                            SwitchCommand(tbServMComp);
                            break;
                        case 'T': //Technologie
                            SwitchCommand(tbTechno);
                            break;
                        case 'b': //Pattern
                            SwitchCommand(tbPatternIns);
                            break;
                    }
                    if (tnRef.Text[0] == 'F') SwitchCommand(tbServer);
                    break;
                case '6': // 6-Sites
                    if (tvObjet.SelectedNode.Parent != null)
                    {
                        switch (tvObjet.SelectedNode.Parent.Text[0])
                        {
                            case 'L': //Location
                                SwitchCommand(tbSite);
                                break;
                            case 'U': //User
                                SwitchCommand(tbUser);
                                break;
                            case 'A': //Application
                                SwitchCommand(tbApplication);
                                break;
                            case 'P': //Prod Server
                                SwitchCommand(tbServerSite);
                                break;
                            case 'H': //Hors Prod Server
                                SwitchCommand(tbServerSite);
                                break;
                            case 'Q': //Qualif Server
                                SwitchCommand(tbServerSite);
                                break;
                        }
                    }
                    break;

                case '3': // 3-Production
                case '5': // 5-Pre-Production
                case '4': // 4-Hors Production
                case 'F': // F-Service Infra

                    if (tvObjet.SelectedNode.Parent != null)
                    {

                        switch (tvObjet.SelectedNode.Parent.Name[0])
                        {
                            case '0': // objets infra
                                if (tvObjet.SelectedNode.Parent.Parent != null)
                                {
                                    switch (tvObjet.SelectedNode.Parent.Name[1])
                                    {
                                        case '3':
                                            if (tvObjet.SelectedNode.Parent.Parent.Parent != null)
                                            {
                                                switch (tvObjet.SelectedNode.Parent.Name[2])
                                                {
                                                    case '0':
                                                        SwitchCommand(tbGenks);
                                                        break;
                                                    case '1':
                                                        SwitchCommand(tbGensas);
                                                        break;
                                                }
                                            }
                                            break;
                                    }
                                }
                                break;
                            case '1': // Objets App Externe à ajouter
                                switch (tvObjet.SelectedNode.Parent.Name[1])
                                {
                                    case '0': SwitchCommand(tbCluster);
                                        break;
                                    case '1':
                                        SwitchCommand(tbServeurE);
                                        break;

                                }
                                break;
                            case '2': // Objets instancés
                                switch (tvObjet.SelectedNode.Parent.Name[1])
                                {
                                    case '0':
                                        SwitchCommand(tbCluster);
                                        break;
                                    case '1':
                                        SwitchCommand(tbServeurE);
                                        break;
                                    case '2':
                                        SwitchCommand(tbVlan);
                                        break;
                                    case '3':
                                        SwitchCommand(tbRouter);
                                        break;
                                    case '4':
                                        if (tvObjet.SelectedNode.Parent.Parent.Parent != null)
                                        {
                                            switch (tvObjet.SelectedNode.Parent.Name[2])
                                            {
                                                case '0': // InsService managé
                                                    SwitchCommand(tbInssas);
                                                    
                                                    break;
                                                case '1': // Name Space selectionné mais ks ajouté
                                                    SwitchCommand(tbInsks);
                                                    break;
                                                case '2': // Ins ingres
                                                    break;
                                                case '3': // Ins Services
                                                    break;
                                                case '4': // Ins pod
                                                    SwitchCommand(tbInssas);
                                                    break;
                                            }
                                        }
                                        break;
                                    case '5': // Cluster infra
                                        SwitchCommand(tbCluster);
                                        break;
                                }
                                break;
                            case '3': // Objets Serveur / Location
                                switch (tvObjet.SelectedNode.Parent.Name[1])
                                {
                                    case '0':
                                        SwitchCommand(tbServeurE);
                                        break;
                                }
                                break;
                        }
                    }
                    break;
                case '8': // 8-ZoningProd
                case '7': // 7-ZoningHorsProd
                    if (tvObjet.SelectedNode.Parent != null)
                    {
                        switch (tvObjet.SelectedNode.Parent.Name[0])
                        {
                            case 'C': //ClusterVip
                                SwitchCommand(tbCluster);
                                break;
                            case 'M': //Serveurs Physiques, Serveurs Hotes
                            case 'P': //Partition
                            case 'H': //Serveurs Hotes
                                SwitchCommand(tbMachine);
                                break;
                            case 'V': //Virtuel
                                SwitchCommand(tbVirtuel);
                                break;
                            case 'G': //Serveurs Groupe
                                SwitchCommand(tbCluster);
                                break;
                            case 'B': //Baie de disques
                                SwitchCommand(tbBaie1);
                                break;
                            case 'L': //Lun
                                SwitchCommand(tbLun);
                                break;
                            case 'Z': //Zone
                                SwitchCommand(tbZone);
                                break;
                        }
                    }
                    break;
                case 'A': // A-SanProd
                case '9': // 9-SanHorsProd
                    if (tvObjet.SelectedNode.Parent != null)
                    {
                        switch (tvObjet.SelectedNode.Parent.Name[0])
                        {
                            case 'V': //I/O Serveurs
                            case 'P': //Partitions
                            case 'M': // Serveurs Physiques
                                SwitchCommand(tbServeurE);
                                break;
                            case 'B': //Baie de disques
                                SwitchCommand(tbBaieCTI);
                                break;
                        }
                    }
                    break;

                case 'C': // C-CTIProd
                case 'B': // B-CTIHorsProd
                    if (tvObjet.SelectedNode.Parent != null)
                    {
                        switch (tvObjet.SelectedNode.Parent.Name[0])
                        {
                            case 'M': //Serveurs Physiques
                            case 'H': //Serveurs Hotes
                                SwitchCommand(tbMachineCTI);
                                break;
                            case 'D': //Baie de disques
                                SwitchCommand(tbBaieDPhy);
                                break;
                            case 'B': //Baie de disques
                                SwitchCommand(tbBaiePhy);
                                break;
                        }
                    }
                    break;
                case 'Y': // Y-Cadre Ref
                    if (tvObjet.SelectedNode.Parent != null)
                    {
                        SwitchCommand(tbCadreRefN);
                    }
                    break;

            }

            //throw new NotImplementedException();
        }

        void ActiveObjetsVueFonctionnelle(bool Value)
        {
#if WRITE
            tbModule.Visible = Value;
            tbLink.Visible = Value;
            tbUser.Visible = Value;
            tbApplication.Visible = Value;
            tbVisio.Visible = Value;
#endif
        }

        void ActiveObjetsVueApplicative(bool Value)
        {
#if WRITE
            tbApplication.Visible = Value;
            tbBase.Visible = Value;
            tbQueue.Visible = Value;
            tbFile.Visible = Value;
            tbComposant.Visible = Value;
            tbLinkA.Visible = Value;
            tbMainComposant.Visible = Value;
            tbCompFonc.Visible = Value;
            tbInterface.Visible = Value;
            tbFluxBoutEnBoutFonc.Visible = Value;
            //if (!tbVisio.Visible) tbVisio.Visible = Value;
#endif
        }

        void ActiveObjetsVueInfrastructure(bool Value)
        {
#if WRITE
            //tbTechno.Visible= Value;
            tbGenks.Visible = Value;
            tbGenpod.Visible = Value;
            tbGening.Visible = Value;
            tbGensvc.Visible = Value;
            tbPattern.Visible = Value;
            tbLinkI.Visible = Value;
            //tbServer.Visible = Value;
            tbServMComp.Visible = Value;
            tbFluxBoutEnBout.Visible = Value;
            tbEACB.Visible = Value;
            tbStatut.Visible = Value;
            //if (!tbVisio.Visible) tbVisio.Visible = Value;
#endif
        }

        void ActiveObjetsVuePhysique(bool Value)
        {
#if WRITE
            tbVlan.Visible = Value;
            tbCluster.Visible = Value;
            tbServeurE.Visible = Value;
            tbInsnd.Visible = Value;
            tbcard.Visible = Value;
            tbNatRule.Visible = Value;
            tbRouter.Visible = Value;
            tbFlux.Visible = Value;
            //if (!tbVisio.Visible) tbVisio.Visible = Value;
#endif
        }

        void ActiveObjetsVueSites(bool Value)
        {
#if WRITE
            tbSite.Visible = Value;
            tbPtCnx.Visible = Value;
            tbCnx.Visible = Value;
            tbInterLink.Visible = Value;
            //if (!tbVisio.Visible) tbVisio.Visible = Value;
#endif
        }

        void ActiveObjetsVueSan(bool Value)
        {
#if WRITE
            if (!tbCluster.Visible) tbCluster.Visible = Value;
            tbMachine.Visible = Value;
            tbBaie1.Visible = Value;
            tbLun.Visible = Value;
            tbZone.Visible = Value;
#endif
        }

        void ActiveObjetsVueSanSwitch(bool Value)
        {
#if WRITE
            tbSanCard.Visible = Value;
            tbSanSwitch.Visible = Value;
            tbISL.Visible = Value;
#endif
        }

        void ActiveObjetsVueCTI(bool Value)
        {
#if WRITE
            tbBaiePhy.Visible = Value;
            tbDrawer.Visible = Value;
#endif
        }

        void ActiveObjetsVueCadreRef(bool Value)
        {
#if WRITE
            //tbBaiePhy.Visible = Value;
            tbPatrimoine.Visible = Value;
#endif
        }

        void ActiveObjetsVueSta(bool Value)
        {
#if WRITE
            if (!tbModule.Visible) tbModule.Visible = Value;
            if (!tbUser.Visible) tbUser.Visible = Value;
            //if (!tbLink.Visible) tbLink.Visible = Value;


            if (!tbApplication.Visible) tbApplication.Visible = Value;
            if (!tbMainComposant.Visible) tbMainComposant.Visible = Value;
            if (!tbBase.Visible) tbBase.Visible = Value;
            if (!tbQueue.Visible) tbQueue.Visible = Value;
            if (!tbFile.Visible) tbFile.Visible = Value;
            if (!tbComposant.Visible) tbComposant.Visible = Value;
            if (!tbInterface.Visible) tbInterface.Visible = Value;
            if (!tbLinkA.Visible) tbLinkA.Visible = Value;
            //if (!tbLinkI.Visible) tbLinkI.Visible = Value;

            //if (!tbVlan.Visible) tbVlan.Visible = Value;
            //if (!tbCluster.Visible) tbCluster.Visible = Value;
            //if (!tbServeurE.Visible) tbServeurE.Visible = Value;
            //if (!tbcard.Visible) tbcard.Visible = Value;
            //if (!tbRouter.Visible) tbRouter.Visible = Value;

#endif
        }

        void ActiveObjetsVueSIApp(bool Value)
        {
#if WRITE
            //if (!tbSi.Visible) 
            tbSi.Visible = Value;
            //if (!tbVisio.Visible) tbVisio.Visible = Value;
#endif
        }

        void ActiveObjetsVueAxes(bool Value)
        {
#if WRITE
            //if (!tbSi.Visible) 
            tbAxes.Visible = Value;
            tbReport.Visible = Value;
            tbExportXLSReport.Visible = Value;
#endif
        }

        void ActiveObjetsVueSIInf(bool Value)
        {
#if WRITE
            if (!tbSi.Visible) tbSi.Visible = Value;
            //if (!tbVisio.Visible) tbVisio.Visible = Value;
#endif
        }

        private void InitPackagesApp(TreeNodeCollection tn, string guidParent)
        {
            List<String[]> lstPackage = new List<string[]>();
            //ArrayList guidCadreRef = new ArrayList();
            //ArrayList NomCadreRef = new ArrayList();
            string sSelect = "";

            sSelect = "Select GuidMainComposantRef, NomMainComposantRef FROM MainComposantRef, ProduitApp WHERE MainComposantRef.GuidProduitApp=ProduitApp.GuidProduitApp and GuidCadreRefApp='" + guidParent + "'";
            if (oCnxBase.CBRecherche(sSelect))
            {
                while (oCnxBase.Reader.Read())
                    tn.Add(oCnxBase.Reader.GetString(0), oCnxBase.Reader.GetString(1));
            }
            oCnxBase.CBReaderClose();

            sSelect = "Select GuidCadreRefApp, NomCadreRefApp FROM CadreRefApp WHERE GuidParentApp='" + guidParent + "'";

            if (oCnxBase.CBRecherche(sSelect))
            {
                while (oCnxBase.Reader.Read())
                {
                    string[] aPackage = new string[2];
                    aPackage[0] = oCnxBase.Reader.GetString(0);
                    aPackage[1] = oCnxBase.Reader.GetString(1);
                    lstPackage.Add(aPackage);
                }
            }
            oCnxBase.CBReaderClose();
            for (int i = 0; i < lstPackage.Count; i++)
            {
                tn.Add(lstPackage[i][0], lstPackage[i][1]);
                int n = tn.Count;
                //Font fontpro = tn. new Font("arial", 8);
                //tn[n - 1].NodeFont = new Font(fontpro, FontStyle.Bold);
                tn[n - 1].ForeColor = Color.Gray;
                InitPackagesApp(tn[n - 1].Nodes, lstPackage[i][0]);
            }
        }

        public void ChangeTreeViewObjet(XmlDocument xmlDoc)
        {
            XmlElement root = xmlDoc.DocumentElement;

            string sTypeVue = XmlGetAttValueAFromAttValueB(root, "Value", "Nom", "NomTypeVue");
            for (int i = 0; i < 10; i++) bDevelop[i] = false;

            //drawArea.GraphicsList.Clear();
            if (sTypeVue != null && sTypeVue != "")
            //if (cbTypeVue.SelectedItem != null)
            {
                XmlElement elTreeView = xmlDoc.CreateElement("treeview");
                root.AppendChild(elTreeView);

                string sGuidAppVersion = GetGuidAppVersion();
                if (sNomEnvironnement != null && sNomEnvironnement != "") tbEnv.Text = sNomEnvironnement;
                //if (sGuidVueInf != null && sGuidVueInf != "") tbVueInf.Text = sGuidVueInf;


                tvObjet.Nodes.Clear();
                switch (sTypeVue[0])
                {
                    case '0': //0-Fonctionnelle
                        ActiveObjetsVueApplicative(false);
                        ActiveObjetsVueInfrastructure(false);
                        ActiveObjetsVueSites(false);
                        ActiveObjetsVuePhysique(false);
                        ActiveObjetsVueSan(false);
                        ActiveObjetsVueCTI(false);
                        ActiveObjetsVueSanSwitch(false);
                        ActiveObjetsVueCadreRef(false);
                        ActiveObjetsVueSIApp(false);
                        ActiveObjetsVueSIInf(false);
                        ActiveObjetsVueAxes(false);
                        ActiveObjetsVueFonctionnelle(true);

                        tvObjet.Nodes.Add("AppUser", "User");
                        tvObjet.Nodes.Add("Application", "Application");

                        oCnxBase.CBAddNode("SELECT GuidAppUser, NomAppUser FROM AppUser ORDER BY NomAppUser", tvObjet.Nodes[0].Nodes);
                        oCnxBase.CBAddNode("SELECT GuidApplication, NomApplication FROM Application ORDER BY NomApplication", tvObjet.Nodes[1].Nodes);
                        

                        break;
                    case '1': // 1-Applicative
                              /*
                              XmlCreatElKeyNode(xmlDoc, elTreeView, "User", "User");
                              XmlCreatElKeyNode(xmlDoc, elTreeView, "Application", "Application");
                              XmlCreatElKeyNode(xmlDoc, elTreeView, "Module", "Module");
                              XmlCreatElKeyNode(xmlDoc, elTreeView, "Link", "Link");

                              oCnxBase.CBAddNode("SELECT GuidApplication, NomApplication FROM Application ORDER BY NomApplication", xmlDoc, XmlGetFirstElFromParent(elTreeView, "Application"));

                              if (sGuidVueInf != null)
                              {
                                  oCnxBase.CBAddNode("SELECT User.GuidAppUser, NomAppUser FROM User, DansVue, GUser WHERE GuidVue='" + sGuidVueInf + "' AND GuidObjet=GuidGAppUser And GUser.GuidAppUser=User.GuidAppUser Order By NomAppUser", xmlDoc, XmlGetFirstElFromParent(elTreeView, "User"));
                                  oCnxBase.CBAddNode("SELECT Module.GuidModule, NomModule FROM Module, DansVue, GModule WHERE GuidVue='" + sGuidVueInf + "' AND GuidObjet=GuidGModule And GModule.GuidModule=Module.GuidModule Order By NomModule", xmlDoc, XmlGetFirstElFromParent(elTreeView, "Module"));
                                  oCnxBase.CBAddNode("SELECT Link.GuidLink, NomLink FROM Link, DansVue, GLink WHERE GuidVue='" + sGuidVueInf + "' AND GuidObjet=GuidGLink And GLink.GuidLink=Link.GuidLink  Order By NomLink", xmlDoc, XmlGetFirstElFromParent(elTreeView, "Link"));
                              }
                              */
#if APIREADY
                        using (var webClient = new System.Net.WebClient())
                        {
                            webClient.Headers.Add("Content-Type", "application/json");
                            webClient.Encoding = System.Text.UTF8Encoding.UTF8;
                            webClient.DownloadDataCompleted += webClient_TVAddNodeApps;
                            Uri urlToRequest = new Uri(@"http://localhost:3001/apps/list");
                            webClient.DownloadDataAsync(urlToRequest);
                        }
                        using (var webClient = new System.Net.WebClient())
                        {
                            webClient.Headers.Add("Content-Type", "application/json");
                            webClient.Encoding = System.Text.UTF8Encoding.UTF8;
                            webClient.DownloadDataCompleted += webClient_TVAddNodeUsers;
                            Uri urlToRequest = new Uri(@"http://localhost:3001/vue/" + sGuidVueInf + @"/users/list");
                            webClient.DownloadDataAsync(urlToRequest);
                        }
                        using (var webClient = new System.Net.WebClient())
                        {
                            webClient.Headers.Add("Content-Type", "application/json");
                            webClient.Encoding = System.Text.UTF8Encoding.UTF8;
                            webClient.DownloadDataCompleted += webClient_TVAddNodeModules;
                            Uri urlToRequest = new Uri(@"http://localhost:3001/vue/" + sGuidVueInf + @"/modules/list");
                            webClient.DownloadDataAsync(urlToRequest);
                        }
                        using (var webClient = new System.Net.WebClient())
                        {
                            webClient.Headers.Add("Content-Type", "application/json");
                            webClient.Encoding = System.Text.UTF8Encoding.UTF8;
                            webClient.DownloadDataCompleted += webClient_TVAddNodeLinks;
                            Uri urlToRequest = new Uri(@"http://localhost:3001/vue/" + sGuidVueInf + @"/links/list");
                            webClient.DownloadDataAsync(urlToRequest);
                        }
#else
                        tvObjet.Nodes.Add("AppUser", "User");
                        tvObjet.Nodes.Add("Application", "Application");
                        tvObjet.Nodes.Add("Module", "Module");
                        tvObjet.Nodes.Add("Link", "Link");
                        tvObjet.Nodes.Add("Patterns", "Patterns");

                        oCnxBase.CBAddNode("SELECT GuidApplication, NomApplication FROM Application ORDER BY NomApplication", tvObjet.Nodes[1].Nodes);

                        if (sGuidVueInf != null)
                        {
                            oCnxBase.CBAddNode("SELECT AppUser.GuidAppUser, NomAppUser FROM AppUser, Vue, DansVue, GAppUser WHERE Vue.GuidVue='" + sGuidVueInf + "' AND Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGAppUser And GAppUser.GuidAppUser=AppUser.GuidAppUser Order By NomAppUser", tvObjet.Nodes[0].Nodes);
                            oCnxBase.CBAddNode("SELECT Module.GuidModule, NomModule FROM Module, Vue, DansVue, GModule WHERE Vue.GuidVue='" + sGuidVueInf + "' AND Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGModule And GModule.GuidModule=Module.GuidModule Order By NomModule", tvObjet.Nodes[2].Nodes);
                            oCnxBase.CBAddNode("SELECT Link.GuidLink, NomLink FROM Link, Vue, DansVue, GLink WHERE Vue.GuidVue='" + sGuidVueInf + "' AND Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGLink And GLink.GuidLink=Link.GuidLink  Order By NomLink", tvObjet.Nodes[3].Nodes);
                        }

#endif

                        ActiveObjetsVueFonctionnelle(false);
                        ActiveObjetsVueApplicative(true);
                        ActiveObjetsVueInfrastructure(false);
                        ActiveObjetsVueSites(false);
                        ActiveObjetsVuePhysique(false);
                        ActiveObjetsVueSan(false);
                        ActiveObjetsVueCTI(false);
                        ActiveObjetsVueSanSwitch(false);
                        ActiveObjetsVueCadreRef(false);
                        ActiveObjetsVueSIApp(false);
                        ActiveObjetsVueSIInf(false);
                        ActiveObjetsVueAxes(false);

                        break;
                    case '2': // 2-Infrastructure
                              /*
                              XmlCreatElKeyNode(xmlDoc, elTreeView, "User", "User");
                              XmlCreatElKeyNode(xmlDoc, elTreeView, "Application", "Application");
                              XmlCreatElKeyNode(xmlDoc, elTreeView, "LSApplication", "LS Application");
                              XmlCreatElKeyNode(xmlDoc, elTreeView, "Composant", "Composant");
                              XmlCreatElKeyNode(xmlDoc, elTreeView, "Link", "Link");
                              XmlCreatElKeyNode(xmlDoc, elTreeView, "FonctionServer", "FonctionServer");
                              XmlCreatElKeyNode(xmlDoc, elTreeView, "Package", "Packages Applicatifs");
                              XmlCreatElKeyNode(xmlDoc, elTreeView, "Technologie", "Technologie");
                              XmlCreatElKeyNode(xmlDoc, elTreeView, "Service", "Service");

                              oCnxBase.CBAddNode("SELECT GuidApplication, NomApplication FROM Application ORDER BY NomApplication", xmlDoc, XmlGetFirstElFromParent(elTreeView, "LSApplication"));
                              if (sGuidVueInf != null)
                              {
                                  oCnxBase.CBAddNode("SELECT User.GuidAppUser, NomAppUser FROM User, DansVue, GUser WHERE GuidVue='" + sGuidVueInf + "' AND GuidObjet=GuidGAppUser And GUser.GuidAppUser=User.GuidAppUser Order By NomAppUser", xmlDoc, XmlGetFirstElFromParent(elTreeView, "User"));
                                  oCnxBase.CBAddNode("SELECT Application.GuidApplication, NomApplication FROM Application, DansVue, GApplication WHERE GuidVue='" + sGuidVueInf + "' AND GuidObjet=GuidGApplication And GApplication.GuidApplication=Application.GuidApplication  Order By NomApplication", xmlDoc, XmlGetFirstElFromParent(elTreeView, "Application"));
                                  oCnxBase.CBAddNode("SELECT MainComposant.GuidMainComposant, NomMainComposant FROM MainComposant, DansVue, GMainComposant WHERE GuidVue='" + sGuidVueInf + "' AND GuidObjet=GuidGMaincomposant And GMainComposant.GuidMainComposant=MainComposant.GuidMainComposant  Order By NomMainComposant", xmlDoc, XmlGetFirstElFromParent(elTreeView, "Composant"));
                                  oCnxBase.CBAddNode("SELECT Link.GuidLink, NomLink FROM Link, DansVue, GLink WHERE GuidVue='" + sGuidVueInf + "' AND GuidObjet=GuidGLink And GLink.GuidLink=Link.GuidLink AND TypeLink='E'  Order By NomLink", xmlDoc, XmlGetFirstElFromParent(elTreeView, "Link"));
                              }
                              else oCnxBase.CBReaderClose();

                              XmlElement el = XmlGetFirstElFromParent(elTreeView, "FonctionServer");
                              oCnxBase.CBAddNode("SELECT GuidFonction, NomFonction FROM Fonction ORDER BY NomFonction", xmlDoc, el);

                              IEnumerator ienum = el.GetEnumerator();
                              XmlNode Node;
                              while (ienum.MoveNext())
                              {
                                  Node = (XmlNode)ienum.Current;
                                  if (Node.NodeType == XmlNodeType.Element)
                                  {
                                      if (Node.Name != "Attributs") {
                                          oCnxBase.CBAddNode("SELECT DISTINCT GuidServerType, NomServerType FROM ServerType Where GuidFonction='" + XmlGetAttValueAFromAttValueB((XmlElement)Node, "Value", "Nom", "NomTypeVue") + "' ORDER BY NomServerType", xmlDoc, (XmlElement)Node);
                                      }
                                  }
                              }

                              oCnxBase.CBAddNode("SELECT DISTINCT GuidMainComposantRef, NomMainComposantRef FROM MainComposantRef, ProduitApp, CadreRefApp WHERE MainComposantRef.GuidProduitApp=ProduitApp.GuidProduitApp AND ProduitApp.GuidCadreRefApp=CadreRefApp.GuidCadreRefApp ORDER BY NomMainComposantRef ", xmlDoc, XmlGetFirstElFromParent(elTreeView, "Package"));
                              oCnxBase.CBAddNode("SELECT DISTINCT GuidTechnoRef, NomTechnoRef, IndexImgOS FROM TechnoRef, Produit, CadreRef WHERE TechnoRef.GuidProduit=Produit.GuidProduit AND Produit.GuidCadreRef=CadreRef.GuidCadreRef AND(TypeCadreRef IS NULL OR TypeCadreRef='S') ORDER BY NomTechnoRef ", xmlDoc, XmlGetFirstElFromParent(elTreeView, "Technologie"));
                              oCnxBase.CBAddNode("SELECT GuidGroupService, NomGroupService FROM GroupService ORDER BY NomGroupService", xmlDoc, XmlGetFirstElFromParent(elTreeView, "Service"));

                              */

#if APIREADY
                        using (var webClient = new System.Net.WebClient())
                        {
                            webClient.Headers.Add("Content-Type", "application/json");
                            webClient.Encoding = System.Text.UTF8Encoding.UTF8;
                            webClient.DownloadDataCompleted += webClient_TVAddNodeLSApps;
                            Uri urlToRequest = new Uri(@"http://localhost:3001/apps/list");
                            webClient.DownloadDataAsync(urlToRequest);
                        }
                        
                        using (var webClient = new System.Net.WebClient())
                        {
                            webClient.Headers.Add("Content-Type", "application/json");
                            webClient.Encoding = System.Text.UTF8Encoding.UTF8;
                            webClient.DownloadDataCompleted += webClient_TVAddNodeUsers;
                            Uri urlToRequest = new Uri(@"http://localhost:3001/vue/" + sGuidVueInf + @"/users/list");
                            webClient.DownloadDataAsync(urlToRequest);
                        }
                        
                        using (var webClient = new System.Net.WebClient())
                        {
                            webClient.Headers.Add("Content-Type", "application/json");
                            webClient.Encoding = System.Text.UTF8Encoding.UTF8;
                            webClient.DownloadDataCompleted += webClient_TVAddNodeApps;
                            Uri urlToRequest = new Uri(@"http://localhost:3001/vue/" + sGuidVueInf + @"/applications/list");
                            webClient.DownloadDataAsync(urlToRequest);
                        }

                        using (var webClient = new System.Net.WebClient())
                        {
                            webClient.Headers.Add("Content-Type", "application/json");
                            webClient.Encoding = System.Text.UTF8Encoding.UTF8;
                            webClient.DownloadDataCompleted += webClient_TVAddNodeMainComposants;
                            Uri urlToRequest = new Uri(@"http://localhost:3001/vue/" + sGuidVueInf + @"/maincomposants/list");
                            webClient.DownloadDataAsync(urlToRequest);
                        }
                        
                        using (var webClient = new System.Net.WebClient())
                        {
                            webClient.Headers.Add("Content-Type", "application/json");
                            webClient.Encoding = System.Text.UTF8Encoding.UTF8;
                            webClient.DownloadDataCompleted += webClient_TVAddNodeLinks;
                            Uri urlToRequest = new Uri(@"http://localhost:3001/vue/" + sGuidVueInf + @"/links/list");
                            webClient.DownloadDataAsync(urlToRequest);
                        }
                        
#else

                        tvObjet.Nodes.Add("User", "User");
                        tvObjet.Nodes.Add("Application", "Application");
                        tvObjet.Nodes.Add("LSApplication", "LS Application");
                        tvObjet.Nodes.Add("Composant", "Composant");
                        tvObjet.Nodes.Add("Link", "Link");
                        tvObjet.Nodes.Add("FonctionServer", "FonctionServer");
                        tvObjet.Nodes.Add("6ManagedService", "Services Cloud");
                        tvObjet.Nodes.Add("7Container", "Container");
                        //tvObjet.Nodes.Add("Composant", "Composant");
                        tvObjet.Nodes.Add("Package", "Packages Applicatifs");
                        tvObjet.Nodes.Add("Technologie", "Technologies");
                        tvObjet.Nodes.Add("Service", "Service");
                        tvObjet.Nodes.Add("bPattern", "Pattern");

                        
                        oCnxBase.CBAddNode("SELECT GuidApplication, NomApplication FROM Application ORDER BY NomApplication", tvObjet.Nodes[2].Nodes);
                        if (sGuidVueInf != null)
                        {
                            oCnxBase.CBAddNode("SELECT AppUser.GuidAppUser, NomAppUser FROM AppUser, Vue, DansVue, GAppUser WHERE Vue.GuidVue='" + sGuidVueInf + "' AND Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGAppUser And GAppUser.GuidAppUser=AppUser.GuidAppUser Order By NomAppUser", tvObjet.Nodes[0].Nodes);
                            oCnxBase.CBAddNode("SELECT Application.GuidApplication, NomApplication FROM Application, Vue, DansVue, GApplication WHERE Vue.GuidVue='" + sGuidVueInf + "' AND Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGApplication And GApplication.GuidApplication=Application.GuidApplication  Order By NomApplication", tvObjet.Nodes[1].Nodes);
                            oCnxBase.CBAddNode("SELECT MainComposant.GuidMainComposant, NomMainComposant FROM MainComposant, Vue, DansVue, GMainComposant WHERE Vue.GuidVue='" + sGuidVueInf + "' AND Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGMaincomposant And GMainComposant.GuidMainComposant=MainComposant.GuidMainComposant  Order By NomMainComposant", tvObjet.Nodes[3].Nodes);
                            oCnxBase.CBAddNode("SELECT Link.GuidLink, NomLink FROM Link, Vue, DansVue, GLink WHERE Vue.GuidVue='" + sGuidVueInf + "' AND Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGLink And GLink.GuidLink=Link.GuidLink AND TypeLink='E'  Order By NomLink", tvObjet.Nodes[4].Nodes);
                        }
                        else oCnxBase.CBReaderClose();


                        oCnxBase.CBAddNode("SELECT GuidFonction, NomFonction FROM Fonction ORDER BY NomFonction", tvObjet.Nodes[5].Nodes);

                        for (int i = 0; i < tvObjet.Nodes[5].Nodes.Count; i++)
                        {
                            oCnxBase.CBAddNode("SELECT DISTINCT GuidServerType, NomServerType FROM ServerType Where GuidFonction='" + tvObjet.Nodes[5].Nodes[i].Name + "' ORDER BY NomServerType", tvObjet.Nodes[5].Nodes[i].Nodes);
                        }

                        oCnxBase.CBAddNode("SELECT GuidManagedsvc, NomManagedsvc FROM Managedsvc ORDER BY NomManagedsvc", tvObjet.Nodes[6].Nodes);
                        oCnxBase.CBAddNode("SELECT GuidContainer, NomContainer FROM Container ORDER BY NomContainer", tvObjet.Nodes[7].Nodes);


                        if (oCnxBase.CBRecherche("Select GuidCadreRefApp FROM CadreRefApp WHERE GuidParentApp='Root'"))
                        {
                            string sGuidRoot = oCnxBase.Reader.GetString(0);
                            oCnxBase.CBReaderClose();
                            InitPackagesApp(tvObjet.Nodes[8].Nodes, sGuidRoot);
                        }
                        oCnxBase.CBReaderClose();

                        //oCnxBase.CBAddNode("SELECT DISTINCT GuidMainComposantRef, NomMainComposantRef FROM MainComposantRef, ProduitApp, CadreRefApp WHERE MainComposantRef.GuidProduitApp=ProduitApp.GuidProduitApp AND ProduitApp.GuidCadreRefApp=CadreRefApp.GuidCadreRefApp ORDER BY NomMainComposantRef ", tvObjet.Nodes[6].Nodes);

                        oCnxBase.CBAddNode("SELECT DISTINCT GuidTechnoRef, NomTechnoRef, IndexImgOS FROM TechnoRef, Produit, CadreRef WHERE TechnoRef.GuidProduit=Produit.GuidProduit AND Produit.GuidCadreRef=CadreRef.GuidCadreRef AND(TypeCadreRef IS NULL OR TypeCadreRef='S') ORDER BY NomTechnoRef ", tvObjet.Nodes[9].Nodes);
                        oCnxBase.CBAddNode("SELECT GuidGroupService, NomGroupService FROM GroupService ORDER BY NomGroupService", tvObjet.Nodes[10].Nodes);
                        oCnxBase.CBAddNode("SELECT GuidPattern, NomPattern FROM Pattern, Vue, TypeVue WHERE Pattern.GuidVue=Vue.GuidVue and Vue.GuidTypeVue=TypeVue.GuidTypeVue and Vue.GuidTypeVue='d5b533a9-06ac-4f8c-a5ab-e345b0212542' order by NomPattern", tvObjet.Nodes[11].Nodes);
                        
#endif

                        ActiveObjetsVueFonctionnelle(false);
                        ActiveObjetsVueApplicative(false);
                        ActiveObjetsVueInfrastructure(true);
                        ActiveObjetsVueSites(false);
                        ActiveObjetsVuePhysique(false);
                        ActiveObjetsVueSan(false);
                        ActiveObjetsVueSanSwitch(false);
                        ActiveObjetsVueCTI(false);
                        ActiveObjetsVueCadreRef(false);
                        ActiveObjetsVueSIApp(false);
                        ActiveObjetsVueSIInf(false);
                        ActiveObjetsVueAxes(false);

                        break;
                    case '6': // 6-Sites
                        tvObjet.Nodes.Add("0-Location", "Location");
                        tvObjet.Nodes.Add("Server Prod", "Prod-Server");
                        tvObjet.Nodes.Add("Server HorsProd", "HorsProd-Server");
                        tvObjet.Nodes.Add("Server PreProd", "Qualif-Server");
                        tvObjet.Nodes.Add("Service", "Service");

                        oCnxBase.CBAddNode("SELECT GuidLocation, NomLocation FROM Location Order By NomLocation", tvObjet.Nodes[0].Nodes);

                        //Production
                        if (oCnxBase.CBRecherche("SELECT GuidVue FROM TypeVue, Vue WHERE " + "NomTypeVue ='3-Production' AND Vue.GuidAppVersion='" + GetGuidAppVersion() + "' AND TypeVue.GuidTypeVue=Vue.GuidTypeVue"))
                        {
                            ArrayList lstVue = new ArrayList();
                            while (oCnxBase.Reader.Read()) lstVue.Add(oCnxBase.Reader.GetString(0));
                            oCnxBase.CBReaderClose();
                            for (int i = 0; i < lstVue.Count; i++)
                                oCnxBase.CBAddNode("SELECT ServerPhy.GuidServerPhy, NomServerPhy FROM ServerPhy, Vue, DansVue, GServerPhy WHERE Vue.GuidVue='" + lstVue[i] + "' and Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGServerPhy And GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy ORDER BY NomServerPhy", tvObjet.Nodes[1].Nodes);
                            oCnxBase.CBReaderClose();
                        }
                        else oCnxBase.CBReaderClose();

                        //Hors-Production
                        if (oCnxBase.CBRecherche("SELECT GuidVue FROM TypeVue, Vue WHERE " + "NomTypeVue ='4-Hors Production' AND Vue.GuidAppVersion='" + GetGuidAppVersion() + "' AND TypeVue.GuidTypeVue=Vue.GuidTypeVue"))
                        {
                            ArrayList lstVue = new ArrayList();
                            while (oCnxBase.Reader.Read()) lstVue.Add(oCnxBase.Reader.GetString(0));
                            oCnxBase.CBReaderClose();
                            for (int i = 0; i < lstVue.Count; i++)
                                oCnxBase.CBAddNode("SELECT ServerPhy.GuidServerPhy, NomServerPhy FROM ServerPhy, Vue, DansVue, GServerPhy WHERE Vue.GuidVue='" + lstVue[i] + "' and Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGServerPhy And GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy ORDER BY NomServerPhy", tvObjet.Nodes[2].Nodes);
                        }
                        else oCnxBase.CBReaderClose();

                        //PRe-Production
                        if (oCnxBase.CBRecherche("SELECT GuidVue FROM TypeVue, Vue WHERE " + "NomTypeVue ='5-Pre-Production' AND Vue.GuidAppVersion='" + GetGuidAppVersion() + "' AND TypeVue.GuidTypeVue=Vue.GuidTypeVue"))
                        {
                            ArrayList lstVue = new ArrayList();
                            while (oCnxBase.Reader.Read()) lstVue.Add(oCnxBase.Reader.GetString(0));
                            oCnxBase.CBReaderClose();
                            for (int i = 0; i < lstVue.Count; i++)
                                oCnxBase.CBAddNode("SELECT ServerPhy.GuidServerPhy, NomServerPhy FROM ServerPhy, Vue, DansVue, GServerPhy WHERE Vue.GuidVue='" + lstVue[i] + "' and Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGServerPhy And GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy ORDER BY NomServerPhy", tvObjet.Nodes[3].Nodes);
                        }
                        else oCnxBase.CBReaderClose();
                        oCnxBase.CBAddNode("SELECT GuidGroupService, NomGroupService FROM GroupService ORDER BY NomGroupService", tvObjet.Nodes[4].Nodes);

                        ActiveObjetsVueFonctionnelle(false);
                        ActiveObjetsVueApplicative(false);
                        ActiveObjetsVueInfrastructure(false);
                        ActiveObjetsVueSites(true);
                        ActiveObjetsVuePhysique(false);
                        ActiveObjetsVueSan(false);
                        ActiveObjetsVueSanSwitch(false);
                        ActiveObjetsVueCTI(false);
                        ActiveObjetsVueCadreRef(false);
                        ActiveObjetsVueSIApp(false);
                        ActiveObjetsVueSIInf(false);
                        ActiveObjetsVueAxes(false);
                        break;
                    case 'F': // F-Service Infra
                        tvObjet.Nodes.Add("TemplateUser", "User");
                        tvObjet.Nodes.Add("TemplateApplication", "Application");
                        tvObjet.Nodes.Add("TemplateServer", "Template Server");
                        tvObjet.Nodes.Add("Link", "Link");
                        tvObjet.Nodes.Add("CLuster", "Cluster");
                        tvObjet.Nodes.Add("Server", "Server");
                        tvObjet.Nodes.Add("VLAN", "VLAN");
                        tvObjet.Nodes.Add("Router", "Router");
                        tvObjet.Nodes.Add("8-Localisation", "Localisation");
                        tvObjet.Nodes.Add("Patrimoine", "Patrimoire Technique");

                        if (sGuidVueInf != null)
                        {
                            oCnxBase.CBAddNode("SELECT AppUser.GuidAppUser, NomAppUser FROM AppUser, Vue, DansVue, GTechUser WHERE Vue.GuidVue='" + sGuidVueInf + "' and Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGTechUser And GTechUser.GuidAppUser=AppUser.GuidAppUser Order By NomAppUser", tvObjet.Nodes[0].Nodes);
                            oCnxBase.CBAddNode("SELECT Application.GuidApplication, NomApplication FROM Application, Vue, DansVue, GApplication WHERE GuidVue='" + sGuidVueInf + "' and Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGApplication And GApplication.GuidApplication=Application.GuidApplication  Order By NomApplication", tvObjet.Nodes[1].Nodes);
                            oCnxBase.CBAddNode("SELECT Server.GuidServer, NomFonction FROM Server, Fonction, Vue, DansVue, GServer WHERE GuidVue='" + sGuidVueInf + "' and Vue.GuidGVue=DansVue.GuidGVue AND Fonction.GuidFonction=Server.GuidMainFonction AND GuidObjet=GuidGServer And GServer.GuidServer=Server.GuidServer  Order By NomFonction", tvObjet.Nodes[2].Nodes);
                            oCnxBase.CBAddNode("SELECT TechLink.GuidTechLink, NomTechLink FROM TechLink, Vue, DansVue, GTechLink WHERE GuidVue='" + sGuidVueInf + "' and Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGTechLink And GTechLink.GuidTechLink=TechLink.GuidTechLink  Order By NomTechLink", tvObjet.Nodes[3].Nodes);
                        }
                        oCnxBase.CBAddNode("SELECT DISTINCT GuidCluster, NomCluster FROM Cluster, DansTypeVue, TypeVue WHERE GroupVue='d' AND DansTypeVue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidObjet=GuidCluster ORDER BY NomCluster", tvObjet.Nodes[4].Nodes);
                        oCnxBase.CBAddNode("SELECT DISTINCT GuidServerPhy, NomServerPhy FROM ServerPhy, DansTypeVue, TypeVue WHERE GroupVue='d' AND DansTypeVue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidObjet=GuidServerPhy ORDER BY NomServerPhy", tvObjet.Nodes[5].Nodes);
                        oCnxBase.CBAddNode("SELECT DISTINCT GuidVlan, NomVlan FROM Vlan, DansTypeVue, TypeVue WHERE GroupVue='d' AND DansTypeVue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidObjet=GuidVlan ORDER BY NomVlan", tvObjet.Nodes[6].Nodes);
                        oCnxBase.CBAddNode("SELECT DISTINCT GuidRouter, NomRouter FROM Router, DansTypeVue, TypeVue WHERE GroupVue='d' AND DansTypeVue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidObjet=GuidRouter ORDER BY NomRouter", tvObjet.Nodes[7].Nodes);
                        oCnxBase.CBAddNode("SELECT DISTINCT GuidLocation, NomLocation FROM Location", tvObjet.Nodes[8].Nodes);
                        oCnxBase.CBAddNode("SELECT DISTINCT GuidTechnoRef, NomTechnoRef, IndexImgOS FROM TechnoRef, Produit, CadreRef WHERE TechnoRef.GuidProduit=Produit.GuidProduit AND Produit.GuidCadreRef=CadreRef.GuidCadreRef AND(TypeCadreRef IS NULL OR TypeCadreRef='H') ORDER BY NomTechnoRef ", tvObjet.Nodes[9].Nodes);

                        if (oCnxBase.CBRecherche("SELECT GuidLocation, NomLocation FROM Location ORDER BY NomLocation"))
                        {
                            ArrayList GuidL = new ArrayList(), NomL = new ArrayList();
                            while (oCnxBase.Reader.Read()) { GuidL.Add(oCnxBase.Reader.GetString(0)); NomL.Add(oCnxBase.Reader.GetString(1)); }
                            oCnxBase.CBReaderClose();
                            for (int nbrL = 0; nbrL < GuidL.Count; nbrL++)
                            {
                                tvObjet.Nodes.Add("Server", "Servers " + (string)NomL[nbrL]);
                                oCnxBase.CBAddNode("SELECT DISTINCT GuidServerPhy, NomServerPhy FROM ServerPhy WHERE GuidLocation='" + (string)GuidL[nbrL] + "' ORDER BY NomServerPhy", tvObjet.Nodes[tvObjet.Nodes.Count - 1].Nodes);
                            }
                        }
                        oCnxBase.CBReaderClose();

                        ActiveObjetsVueFonctionnelle(false);
                        ActiveObjetsVueApplicative(false);
                        ActiveObjetsVueInfrastructure(false);
                        ActiveObjetsVueSites(false);
                        ActiveObjetsVuePhysique(false);
                        ActiveObjetsVueSan(false);
                        ActiveObjetsVueSanSwitch(false);
                        ActiveObjetsVueCTI(false);
                        ActiveObjetsVueCadreRef(false);
                        ActiveObjetsVueSIApp(false);
                        ActiveObjetsVueSIInf(false);
                        ActiveObjetsVueAxes(false);
                        break;

                    case '3': // 3-Production
                    case '5': // 5-Pre-Production
                    case '4': // 4-Hors Production
                        tvObjet.Nodes.Add("0ObjetInfra", "Objets Liés vue Infrastructure");
                        tvObjet.Nodes[0].Nodes.Add("00PatternUser", "User Infra");
                        tvObjet.Nodes[0].Nodes.Add("01PatternApplication", "Application infra");
                        tvObjet.Nodes[0].Nodes.Add("02PatternServer", "Server Infra");
                        tvObjet.Nodes[0].Nodes.Add("03PatternServiceCloud", "Services Cloud Infra");
                        tvObjet.Nodes[0].Nodes.Add("04Link", "Link Infra");
                        tvObjet.Nodes.Add("1ObjetsAppInfra", "Objets Applications externes vue infrastructure");
                        tvObjet.Nodes[1].Nodes.Add("10ClusterApp", "Cluster App");
                        tvObjet.Nodes[1].Nodes.Add("11ServerApp", "Server App");
                        tvObjet.Nodes.Add("2ObjetsInstanciés", "Objets Instanciés");
                        tvObjet.Nodes[2].Nodes.Add("20CLuster", "Cluster");
                        tvObjet.Nodes[2].Nodes.Add("21Server", "Server");
                        tvObjet.Nodes[2].Nodes.Add("22VLAN", "VLAN");
                        tvObjet.Nodes[2].Nodes.Add("23Router", "Router");
                        tvObjet.Nodes[2].Nodes.Add("24InstanceCloud", "Services Cloud");
                        tvObjet.Nodes[2].Nodes.Add("25Cluster Infrastructure", "Cluster Infrastructure");
                        tvObjet.Nodes.Add("3Serveurs", "Serveurs par Site");
                        tvObjet.Nodes.Add("4Localisation", "Localisation"); //tvObjet.Nodes[tvObjet.Nodes.Count-1].

                        //tvObjet.Nodes.Add("Patrimoine", "Patrimoire Technique");
                        /*
                        if (sGuidVueInf != null)
                        {
                            oCnxBase.CBAddNode("SELECT Distinct AppUser.GuidAppUser, NomAppUser FROM AppUser, Vue, DansVue, GTechUser WHERE Vue.GuidVue='" + sGuidVueInf + "' AND Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGTechUser And GTechUser.GuidAppUser=AppUser.GuidAppUser Order By NomAppUser", tvObjet.Nodes[0].Nodes[0].Nodes);
                            oCnxBase.CBAddNode("SELECT Distinct Application.GuidApplication, NomApplication FROM Application, Vue, DansVue, GApplication WHERE Vue.GuidVue='" + sGuidVueInf + "' AND Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGApplication And GApplication.GuidApplication=Application.GuidApplication  Order By NomApplication", tvObjet.Nodes[0].Nodes[1].Nodes);
                            oCnxBase.CBAddNode("SELECT Distinct Server.GuidServer, NomFonction FROM Server, Fonction, Vue, DansVue, GServer WHERE Vue.GuidVue='" + sGuidVueInf + "' AND Vue.GuidGVue=DansVue.GuidGVue AND Fonction.GuidFonction=Server.GuidMainFonction AND GuidObjet=GuidGServer And GServer.GuidServer=Server.GuidServer  Order By NomFonction", tvObjet.Nodes[0].Nodes[2].Nodes);
                            oCnxBase.CBAddNode("SELECT Distinct Genks.GuidGenks, NomGenks FROM Genks, Vue, DansVue, GGenks WHERE Vue.GuidVue='" + sGuidVueInf + "' AND Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGGenks And GGenks.GuidGenks=Genks.GuidGenks  Order By NomGenks", tvObjet.Nodes[0].Nodes[3].Nodes[0].Nodes);
                            oCnxBase.CBAddNode("SELECT Distinct Gensas.GuidGensas, NomGensas FROM Gensas, Vue, DansVue, GGensas WHERE Vue.GuidVue='" + sGuidVueInf + "' AND Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGGensas And GGensas.GuidGensas=Gensas.GuidGensas  Order By NomGensas", tvObjet.Nodes[0].Nodes[3].Nodes[1].Nodes);
                            oCnxBase.CBAddNode("SELECT Distinct TechLink.GuidTechLink, NomTechLink FROM TechLink, Vue, DansVue, GTechLink WHERE Vue.GuidVue='" + sGuidVueInf + "' AND Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGTechLink And GTechLink.GuidTechLink=TechLink.GuidTechLink  Order By NomTechLink", tvObjet.Nodes[0].Nodes[4].Nodes);
                        }

                        oCnxBase.CBAddNode("SELECT DisTinct Cluster.GuidCluster, concat(NomCluster, '   [', NomApplication, ']') NomClusterLong From Cluster, ServerPhy, ApplicationLink, Application, GApplication, DansVue, Vue vInfra, Vue vDeployApp, Vue vDeploy Where Cluster.GuidCluster = ServerPhy.GuidCluster and ServerPhy.GuidServerPhy = ApplicationLink.GuidServerPhy and ApplicationLink.GuidApplication = Application.GuidApplication and Application.GuidApplication = GApplication.GuidApplication and GApplication.GuidGApplication = DansVue.GuidObjet and DansVue.GuidGVue = vINfra.GuidGVue and vInfra.GuidVue = vDeployApp.GuidVueinf and ApplicationLink.GuidVue = vDeploy.GuidVue and vDeploy.GuidEnvironnement = vDeployApp.GuidEnvironnement and vDeployApp.GuidVue = '" + GuidVue + "' ORDER BY NomClusterLong, Cluster.GuidCluster", tvObjet.Nodes[1].Nodes[0].Nodes);
                        oCnxBase.CBAddNode("SELECT DisTinct ServerPhy.GuidServerphy, concat(NomServerPhy, '   [', NomApplication, ']') NomServerPhyLong From ServerPhy, ApplicationLink, Application, GApplication, DansVue, Vue vInfra, Vue vDeployApp, Vue vDeploy Where ServerPhy.GuidServerPhy = ApplicationLink.GuidServerPhy and ApplicationLink.GuidApplication = Application.GuidApplication and Application.GuidApplication = GApplication.GuidApplication and GApplication.GuidGApplication = DansVue.GuidObjet and DansVue.GuidGVue = vINfra.GuidGVue and vInfra.GuidVue = vDeployApp.GuidVueinf and ApplicationLink.GuidVue = vDeploy.GuidVue and vDeploy.GuidEnvironnement = vDeployApp.GuidEnvironnement and vDeployApp.GuidVue = '" + GuidVue + "' and GuidCluster is null ORDER BY NomServerPhyLong, ServerPhy.GuidServerPhy", tvObjet.Nodes[1].Nodes[1].Nodes);

                        
                        oCnxBase.CBAddNode("SELECT DISTINCT GuidCluster, NomCluster FROM Cluster, DansTypeVue, TypeVue WHERE NomTypeVue ='" + sTypeVue + "' AND DansTypeVue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidObjet=GuidCluster And (GuidClusterType != '74cc8591-f07f-4ae1-9287-050459112392' OR GuidClusterType is null) ORDER BY NomCluster", tvObjet.Nodes[2].Nodes[0].Nodes);
                        oCnxBase.CBAddNode("SELECT DISTINCT GuidServerPhy, NomServerPhy FROM ServerPhy, DansTypeVue, TypeVue WHERE NomTypeVue ='" + sTypeVue + "' AND DansTypeVue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidObjet=GuidServerPhy ORDER BY NomServerPhy", tvObjet.Nodes[2].Nodes[1].Nodes);
                        oCnxBase.CBAddNode("SELECT DISTINCT GuidVlan, NomVlan FROM Vlan, DansTypeVue, TypeVue WHERE NomTypeVue ='" + sTypeVue + "' AND DansTypeVue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidObjet=GuidVlan ORDER BY NomVlan", tvObjet.Nodes[2].Nodes[2].Nodes);
                        oCnxBase.CBAddNode("SELECT DISTINCT GuidRouter, NomRouter FROM Router, DansTypeVue, TypeVue WHERE NomTypeVue ='" + sTypeVue + "' AND DansTypeVue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidObjet=GuidRouter ORDER BY NomRouter", tvObjet.Nodes[2].Nodes[3].Nodes);
                        oCnxBase.CBAddNode("SELECT Distinct Insns.GuidInsns, NomInsns FROM Insns, Insks, GInsks, Dansvue Where Dansvue.GuidObjet = GInsks.GuidGInsks and GInsks.GuidInsks = Insks.GuidInsks and Insks.GuidInsks = Insns.GuidInsks  Order By NomInsns", tvObjet.Nodes[2].Nodes[4].Nodes[0].Nodes);
                        oCnxBase.CBAddNode("SELECT Distinct Insing.GuidInsing, NomInsing FROM Insing, Inspod, Insns Order By NomInsing", tvObjet.Nodes[2].Nodes[4].Nodes[1].Nodes);
                        oCnxBase.CBAddNode("SELECT Distinct Inssvc.GuidInssvc, NomInssvc FROM Inssvc Order By NomInssvc", tvObjet.Nodes[2].Nodes[4].Nodes[2].Nodes);
                        oCnxBase.CBAddNode("SELECT Distinct Inspod.GuidInspod, NomInspod FROM Inspod Order By NomInspod", tvObjet.Nodes[2].Nodes[4].Nodes[2].Nodes);
                        oCnxBase.CBAddNode("SELECT Distinct Inssas.GuidInssas, NomInssas FROM Inssas Order By NomInssas", tvObjet.Nodes[2].Nodes[4].Nodes[4].Nodes);
                        oCnxBase.CBAddNode("SELECT DISTINCT GuidCluster, NomCluster FROM Cluster WHERE GuidClusterType ='74cc8591-f07f-4ae1-9287-050459112392' ORDER BY NomCluster", tvObjet.Nodes[2].Nodes[5].Nodes);

                        if (oCnxBase.CBRecherche("SELECT GuidLocation, NomLocation FROM Location ORDER BY NomLocation"))
                        {
                            ArrayList GuidL = new ArrayList(), NomL = new ArrayList();
                            while (oCnxBase.Reader.Read()) { GuidL.Add(oCnxBase.Reader.GetString(0)); NomL.Add(oCnxBase.Reader.GetString(1)); }
                            oCnxBase.CBReaderClose();
                            for (int nbrL = 0; nbrL < GuidL.Count; nbrL++)
                            {
                                tvObjet.Nodes[3].Nodes.Add("30Server", "Servers " + (string)NomL[nbrL]);
                                oCnxBase.CBAddNode("SELECT DISTINCT GuidServerPhy, NomServerPhy FROM ServerPhy WHERE GuidLocation='" + (string)GuidL[nbrL] + "' ORDER BY NomServerPhy", tvObjet.Nodes[3].Nodes[tvObjet.Nodes[3].Nodes.Count - 1].Nodes);
                            }
                        }
                        oCnxBase.CBReaderClose();
                         
                         */

                        oCnxBase.CBAddNode("SELECT DISTINCT GuidLocation, NomLocation FROM Location Order By NomLocation", tvObjet.Nodes[4].Nodes);
                        //oCnxBase.CBAddNode("SELECT DISTINCT GuidTechnoRef, NomTechnoRef, IndexImgOS FROM TechnoRef, Produit, CadreRef WHERE TechnoRef.GuidProduit=Produit.GuidProduit AND Produit.GuidCadreRef=CadreRef.GuidCadreRef AND(TypeCadreRef IS NULL OR TypeCadreRef='H') ORDER BY NomTechnoRef ", tvObjet.Nodes[11].Nodes);
                        
                        

                        ActiveObjetsVueFonctionnelle(false);
                        ActiveObjetsVueApplicative(false);
                        ActiveObjetsVueInfrastructure(false);
                        ActiveObjetsVueSites(false);
                        ActiveObjetsVuePhysique(true);
                        ActiveObjetsVueSan(false);
                        ActiveObjetsVueSanSwitch(false);
                        ActiveObjetsVueCTI(false);
                        ActiveObjetsVueCadreRef(false);
                        ActiveObjetsVueSIApp(false);
                        ActiveObjetsVueSIInf(false);
                        ActiveObjetsVueAxes(false);
                        break;
                    case 'D': // D-Inf Server
                        if (sGuidVueInf != null)
                        {
                            setCtrlEnabled(bSave, false);
                            GuidVue = new Guid(sGuidVueInf);
                            if (oCnxBase.CBRecherche("Select GuidGVue From Vue Where GuidVue='" + GuidVue + "'"))
                            {
                                GuidGVue = new Guid(oCnxBase.Reader.GetString(0));
                                oCnxBase.CBReaderClose();

                                FormChangeProp fcp = new FormChangeProp(this, null);

                                //fcp.AddlSourceFromDB("SELECT GuidVue, NomVue FROM Vue, TypeVue Where GuidApplication='" + GuidApplication + "' AND Vue.GuidTypeVue=TypeVue.GuidTypeVue AND( NomTypeVue='3-Production' OR NomTypeVue='5-Pre-Production' OR NomTypeVue='4-Hors Production' OR NomTypeVue='F-Service Infra')", "Create");
                                fcp.AddlSourceFromDB("SELECT GuidVue, NomVue FROM Vue, TypeVue Where GuidAppVersion='" + GetGuidAppVersion() + "' AND Vue.GuidTypeVue=TypeVue.GuidTypeVue AND GroupVue='d'", "Create");
                                fcp.ShowDialog(this);
                                if (fcp.Valider)
                                {
                                    string[] aValue = oCnxBase.CmdText.Split('(', ')');
                                    if (aValue.Length > 1) MakeVueInf(aValue[1]);
                                }
                            }
                        }

                        break;
                    case 'E': // E-Inf Server 3D
                        if (sGuidVueInf != null)
                        {
                            GuidVue = new Guid(sGuidVueInf);
                            FormChangeProp fcp = new FormChangeProp(this, null);

                            //fcp.AddlSourceFromDB("SELECT GuidVue, NomVue FROM Vue, TypeVue Where GuidApplication='" + GuidApplication + "' AND Vue.GuidTypeVue=TypeVue.GuidTypeVue AND (NomTypeVue='4-Production' OR NomTypeVue='5-Pre-Production' OR NomTypeVue='4-Hors Production')", "Create");
                            fcp.AddlSourceFromDB("SELECT GuidVue, NomVue FROM Vue, TypeVue Where GuidAppVersion='" + GetGuidAppVersion() + "' AND Vue.GuidTypeVue=TypeVue.GuidTypeVue AND GroupVue='d'", "Create");
                            fcp.ShowDialog(this);
                            if (fcp.Valider)
                            {
                                string[] aValue = oCnxBase.CmdText.Split('(', ')');
                                if (aValue.Length > 1) MakeVueInf3D(aValue[1]);
                            }
                        }

                        break;
                    case 'G': // G-Sequence Flux Fonctionnels
                        if (sGuidVueInf != null)
                        {
                            GuidVue = new Guid(sGuidVueInf);
                            FormChangeProp fcp = new FormChangeProp(this, null);

                            //fcp.AddlSourceFromDB("SELECT GuidVue, NomVue FROM Vue, TypeVue Where GuidApplication='" + GuidApplication + "' AND Vue.GuidTypeVue=TypeVue.GuidTypeVue AND (NomTypeVue='4-Production' OR NomTypeVue='5-Pre-Production' OR NomTypeVue='4-Hors Production')", "Create");
                            fcp.AddlSourceFromDB("SELECT GuidVue, NomVue FROM Vue, TypeVue Where GuidAppVersion='" + GetGuidAppVersion() + "' AND Vue.GuidTypeVue=TypeVue.GuidTypeVue AND GroupVue='d'", "Create");
                            fcp.ShowDialog(this);
                            if (fcp.Valider)
                            {
                                string[] aValue = oCnxBase.CmdText.Split('(', ')');
                                if (aValue.Length > 1) MakeSequenceFluxFonc(aValue[1], sGuidVueInf, null);
                            }
                        }
                        break;
                    case '7': // 7-ZoningHorsProd
                    case '8': // 8-ZoningProd
                        //if (sTypeVue[0] == '8') sTypeVueLevelInf = "NomTypeVue ='" + "3-Production" + "'";
                        //else sTypeVueLevelInf = "(NomTypeVue ='" + "4-Hors Production" + "' OR NomTypeVue ='" + "5-Pre-Production" + "')";
                        tvObjet.Nodes.Add("Hote", "Serveurs Hotes");
                        tvObjet.Nodes.Add("Groupe", "Serveurs Groupe");
                        tvObjet.Nodes.Add("Cluster", "ClusterVip");
                        tvObjet.Nodes.Add("Machine", "Serveurs Physiques");
                        tvObjet.Nodes.Add("Partition", "Partition");
                        tvObjet.Nodes.Add("Virtuel", "Virtuel");
                        tvObjet.Nodes.Add("Baie", "Baie de Disques");
                        tvObjet.Nodes.Add("Lun", "Luns de la Baie");
                        tvObjet.Nodes.Add("Zone", "Zones liées au Lun");


                        oCnxBase.CBAddNode("SELECT GuidServerPhy, NomServerPhy FROM ServerPhy, DansTypeVue, TypeVue WHERE NomTypeVue ='" + sTypeVue + "' AND DansTypeVue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidObjet=GuidServerPhy AND (Type='H' OR Type='E') ORDER BY NomServerPhy", tvObjet.Nodes[0].Nodes);
                        oCnxBase.CBAddNode("SELECT GuidCluster, NomCluster FROM Cluster, DansTypeVue, TypeVue WHERE NomTypeVue ='" + sTypeVue + "' AND DansTypeVue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidObjet=GuidCluster ORDER BY NomCluster", tvObjet.Nodes[1].Nodes);

                        //if (cbVueInf.SelectedItem != null && ((string)cbVueInf.SelectedItem).Length != 0) resultFind = oCnxBase.CBRecherche("SELECT GuidVue FROM Vue WHERE NomVue ='" + (string)cbVueInf.SelectedItem + "'");
                        //else resultFind = oCnxBase.CBRecherche("SELECT GuidVue FROM Application, TypeVue, Vue WHERE " + sTypeVueLevelInf + " AND NomApplication='" + sApplication + "' AND Application.GuidApplication=Vue.GuidApplication AND TypeVue.GuidTypeVue=Vue.GuidTypeVue");
                        //resultFind = oCnxBase.CBRecherche("SELECT GuidVue FROM Application, TypeVue, Vue WHERE " + sTypeVueLevelInf + " AND NomApplication='" + sApplication + "' AND Application.GuidApplication=Vue.GuidApplication AND TypeVue.GuidTypeVue=Vue.GuidTypeVue");
                        if (sGuidVueInf != null)
                        {
                            oCnxBase.CBAddNode("SELECT Cluster.GuidCluster, NomCluster FROM Cluster, Vue, DansVue, GCluster WHERE GuidVue='" + sGuidVueInf + "' and Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGCluster And GCluster.GuidCluster=Cluster.GuidCluster  Order By NomCluster", tvObjet.Nodes[2].Nodes);
                            oCnxBase.CBAddNode("SELECT ServerPhy.GuidServerPhy, NomServerPhy FROM ServerPhy, Vue, DansVue, GServerPhy WHERE GuidVue='" + sGuidVueInf + "' and Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGServerPhy And GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy AND TYPE='M' ORDER BY NomServerPhy", tvObjet.Nodes[3].Nodes);
                            oCnxBase.CBAddNode("SELECT ServerPhy.GuidServerPhy, NomServerPhy FROM ServerPhy, Vue, DansVue, GServerPhy WHERE GuidVue='" + sGuidVueInf + "' and Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGServerPhy And GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy AND TYPE='P' ORDER BY NomServerPhy", tvObjet.Nodes[4].Nodes);
                            oCnxBase.CBAddNode("SELECT ServerPhy.GuidServerPhy, NomServerPhy FROM ServerPhy, Vue, DansVue, GServerPhy WHERE GuidVue='" + sGuidVueInf + "' and Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGServerPhy And GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy AND TYPE='V' ORDER BY NomServerPhy", tvObjet.Nodes[5].Nodes);
                        }
                        oCnxBase.CBAddNode("SELECT GuidBaie, NomBaie FROM Baie, DansTypeVue, TypeVue WHERE NomTypeVue ='" + sTypeVue + "' AND DansTypeVue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidObjet=GuidBaie ORDER BY NomBaie", tvObjet.Nodes[6].Nodes);

                        ActiveObjetsVueFonctionnelle(false);
                        ActiveObjetsVueApplicative(false);
                        ActiveObjetsVueInfrastructure(false);
                        ActiveObjetsVueSites(false);
                        ActiveObjetsVuePhysique(false);
                        ActiveObjetsVueSan(true);
                        ActiveObjetsVueCTI(false);
                        ActiveObjetsVueCadreRef(false);
                        ActiveObjetsVueSIApp(false);
                        ActiveObjetsVueSIInf(false);
                        ActiveObjetsVueAxes(false);
                        break;
                    case '9': // 9-SanHorsProd
                    case 'A': // A-SanProd
                        //if (sTypeVue[0] == 'A') sTypeVueLevelInf = "8-ZoningProd";
                        //else sTypeVueLevelInf = "7-ZoningHorsProd";
                        tvObjet.Nodes.Add("VIO", "I/O Serveurs");
                        tvObjet.Nodes.Add("Partition", "Partitions");
                        tvObjet.Nodes.Add("Machine", "Serveurs Physiques");
                        tvObjet.Nodes.Add("Baie", "Baie de Disques");

                        //if (cbVueInf.SelectedItem != null && ((string)cbVueInf.SelectedItem).Length != 0) resultFind = oCnxBase.CBRecherche("SELECT GuidVue FROM Vue WHERE NomVue ='" + (string)cbVueInf.SelectedItem + "'");
                        //else resultFind = oCnxBase.CBRecherche("SELECT GuidVue FROM Application, TypeVue, Vue WHERE NomTypeVue ='" + sTypeVueLevelInf + "' AND NomApplication='" + sApplication + "' AND Application.GuidApplication=Vue.GuidApplication AND TypeVue.GuidTypeVue=Vue.GuidTypeVue");
                        if (sGuidVueInf != null)
                        {
                            oCnxBase.CBAddNode("SELECT ServerPhy.GuidServerPhy, NomServerPhy FROM ServerPhy, Vue, DansVue, GMachine WHERE Vue.GuidVue='" + sGuidVueInf + "' and Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGMachine And GMachine.GuidServerPhy=ServerPhy.GuidServerPhy AND TYPE='E' ORDER BY NomServerPhy", tvObjet.Nodes[0].Nodes);
                            oCnxBase.CBAddNode("SELECT ServerPhy.GuidServerPhy, NomServerPhy FROM ServerPhy, Vue, DansVue, GMachine WHERE Vue.GuidVue='" + sGuidVueInf + "' and Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGMachine And GMachine.GuidServerPhy=ServerPhy.GuidServerPhy AND TYPE='P' ORDER BY NomServerPhy", tvObjet.Nodes[1].Nodes);
                            oCnxBase.CBAddNode("SELECT ServerPhy.GuidServerPhy, NomServerPhy FROM ServerPhy, Vue, DansVue, GMachine WHERE Vue.GuidVue='" + sGuidVueInf + "' and Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGMachine And GMachine.GuidServerPhy=ServerPhy.GuidServerPhy AND TYPE='M' ORDER BY NomServerPhy", tvObjet.Nodes[2].Nodes);
                            oCnxBase.CBAddNode("SELECT Baie.GuidBaie, NomBaie FROM Baie, Vue, DansVue, GBaie WHERE Vue.GuidVue='" + sGuidVueInf + "' and Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGBaie And GBaie.GuidBaie=Baie.GuidBaie ORDER BY NomBaie", tvObjet.Nodes[3].Nodes);
                        }

                        ActiveObjetsVueFonctionnelle(false);
                        ActiveObjetsVueApplicative(false);
                        ActiveObjetsVueInfrastructure(false);
                        ActiveObjetsVueSites(false);
                        ActiveObjetsVuePhysique(false);
                        ActiveObjetsVueSan(false);
                        ActiveObjetsVueSanSwitch(true);
                        ActiveObjetsVueCTI(false);
                        ActiveObjetsVueCadreRef(false);
                        ActiveObjetsVueSIApp(false);
                        ActiveObjetsVueSIInf(false);
                        ActiveObjetsVueAxes(false);
                        break;
                    case 'C': // C-CTIProd
                    case 'B': // B-CTIHorsProd
                        //if (sTypeVue[0] == 'C') sTypeVueLevelInf = "8-ZoningProd";
                        //else sTypeVueLevelInf = "7-ZoningHorsProd";
                        tvObjet.Nodes.Add("Hote", "Serveurs Hotes");
                        tvObjet.Nodes.Add("Machine", "Serveurs Physiques");
                        tvObjet.Nodes.Add("DBaie", "Baie de Disques");
                        tvObjet.Nodes.Add("BaiePhy", "Baie");

                        //if (cbVueInf.SelectedItem != null && ((string)cbVueInf.SelectedItem).Length != 0) resultFind = oCnxBase.CBRecherche("SELECT GuidVue FROM Vue WHERE NomVue ='" + (string)cbVueInf.SelectedItem + "'");
                        //else resultFind = oCnxBase.CBRecherche("SELECT GuidVue FROM Application, TypeVue, Vue WHERE NomTypeVue ='" + sTypeVueLevelInf + "' AND NomApplication='" + sApplication + "' AND Application.GuidApplication=Vue.GuidApplication AND TypeVue.GuidTypeVue=Vue.GuidTypeVue");
                        if (sGuidVueInf != null)
                        {
                            oCnxBase.CBAddNode("SELECT ServerPhy.GuidServerPhy, NomServerPhy FROM ServerPhy, Vue, DansVue, GMachine WHERE Vue.GuidVue='" + sGuidVueInf + "' and Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGMachine And GMachine.GuidServerPhy=ServerPhy.GuidServerPhy AND (TYPE='H'OR Type='E') ORDER BY NomServerPhy", tvObjet.Nodes[0].Nodes);
                            oCnxBase.CBAddNode("SELECT ServerPhy.GuidServerPhy, NomServerPhy FROM ServerPhy, Vue, DansVue, GMachine WHERE Vue.GuidVue='" + sGuidVueInf + "' and Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGMachine And GMachine.GuidServerPhy=ServerPhy.GuidServerPhy AND TYPE='M' ORDER BY NomServerPhy", tvObjet.Nodes[1].Nodes);
                            oCnxBase.CBAddNode("SELECT Baie.GuidBaie, NomBaie FROM Baie, Vue, DansVue, GBaie WHERE Vue.GuidVue='" + sGuidVueInf + "' and Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGBaie And GBaie.GuidBaie=Baie.GuidBaie ORDER BY NomBaie", tvObjet.Nodes[2].Nodes);
                        }
                        oCnxBase.CBAddNode("SELECT GuidBaiePhy, NomBaiePhy FROM BaiePhy, DansTypeVue, TypeVue WHERE NomTypeVue ='" + sTypeVue + "' AND DansTypeVue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidObjet=GuidBaiePhy ORDER BY NomBaiePhy", tvObjet.Nodes[3].Nodes);

                        ActiveObjetsVueFonctionnelle(false);
                        ActiveObjetsVueApplicative(false);
                        ActiveObjetsVueInfrastructure(false);
                        ActiveObjetsVueSites(false);
                        ActiveObjetsVuePhysique(false);
                        ActiveObjetsVueSan(false);
                        ActiveObjetsVueSanSwitch(false);
                        ActiveObjetsVueCTI(true);
                        ActiveObjetsVueCadreRef(false);
                        ActiveObjetsVueSIApp(false);
                        ActiveObjetsVueSIInf(false);
                        ActiveObjetsVueAxes(false);
                        break;
                    case 'U': // Vue Libre
                        ActiveObjetsVueFonctionnelle(false);
                        ActiveObjetsVueApplicative(false);
                        ActiveObjetsVueInfrastructure(false);
                        ActiveObjetsVueSites(false);
                        ActiveObjetsVuePhysique(false);
                        ActiveObjetsVueSan(false);
                        ActiveObjetsVueSanSwitch(false);
                        ActiveObjetsVueCTI(false);
                        ActiveObjetsVueCadreRef(false);
                        ActiveObjetsVueSta(true);
                        ActiveObjetsVueSIApp(false);
                        ActiveObjetsVueSIInf(false);
                        ActiveObjetsVueAxes(false);

                        tvObjet.Nodes.Add("AppUser", "User");
                        tvObjet.Nodes.Add("Application", "Application");
                        tvObjet.Nodes.Add("Module", "Module");
                        tvObjet.Nodes.Add("CLuster", "Cluster");
                        tvObjet.Nodes.Add("VLAN", "VLAN");
                        tvObjet.Nodes.Add("Router", "Router");
                        tvObjet.Nodes.Add("Server Deploiement", "ServerPhy");

                        tvObjet.Nodes.Add("Link", "Link");
                        tvObjet.Nodes.Add("TechLink", "TechLink");

                        oCnxBase.CBAddNode("SELECT DISTINCT GuidAppUser, NomAppUser FROM AppUser, DansTypeVue, TypeVue WHERE NomTypeVue ='" + sTypeVue + "' AND DansTypeVue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidObjet=GuidAppUser ORDER BY NomAppUser", tvObjet.Nodes[0].Nodes);
                        oCnxBase.CBAddNode("SELECT DISTINCT GuidApplication, NomApplication FROM Application, DansTypeVue, TypeVue WHERE NomTypeVue ='" + sTypeVue + "' AND DansTypeVue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidObjet=GuidApplication ORDER BY NomApplication", tvObjet.Nodes[1].Nodes);
                        oCnxBase.CBAddNode("SELECT DISTINCT GuidModule, NomModule FROM Module, DansTypeVue, TypeVue WHERE NomTypeVue ='" + sTypeVue + "' AND DansTypeVue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidObjet=GuidModule ORDER BY NomModule", tvObjet.Nodes[2].Nodes);
                        oCnxBase.CBAddNode("SELECT DISTINCT GuidCluster, NomCluster FROM Cluster, DansTypeVue, TypeVue WHERE NomTypeVue ='" + sTypeVue + "' AND DansTypeVue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidObjet=GuidCluster ORDER BY NomCluster", tvObjet.Nodes[3].Nodes);
                        oCnxBase.CBAddNode("SELECT DISTINCT GuidVlan, NomVlan FROM Vlan, DansTypeVue, TypeVue WHERE NomTypeVue ='" + sTypeVue + "' AND DansTypeVue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidObjet=GuidVlan ORDER BY NomVlan", tvObjet.Nodes[4].Nodes);
                        oCnxBase.CBAddNode("SELECT DISTINCT GuidRouter, NomRouter FROM Router, DansTypeVue, TypeVue WHERE NomTypeVue ='" + sTypeVue + "' AND DansTypeVue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidObjet=GuidRouter ORDER BY NomRouter", tvObjet.Nodes[5].Nodes);
                        oCnxBase.CBAddNode("SELECT DISTINCT GuidServerPhy, NomServerPhy FROM ServerPhy, DansTypeVue, TypeVue WHERE NomTypeVue ='" + sTypeVue + "' AND DansTypeVue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidObjet=GuidServerPhy ORDER BY NomServerPhy", tvObjet.Nodes[6].Nodes);



                        break;
                    case 'V': // V-SI App
                        ActiveObjetsVueFonctionnelle(false);
                        ActiveObjetsVueApplicative(false);
                        ActiveObjetsVueInfrastructure(false);
                        ActiveObjetsVueSites(false);
                        ActiveObjetsVuePhysique(false);
                        ActiveObjetsVueSan(false);
                        ActiveObjetsVueSanSwitch(false);
                        ActiveObjetsVueCTI(false);
                        ActiveObjetsVueCadreRef(false);
                        ActiveObjetsVueSIApp(true);
                        ActiveObjetsVueSIInf(false);
                        ActiveObjetsVueAxes(false);
                        break;

                    case 'W': // W-SI Inf
                        ActiveObjetsVueFonctionnelle(false);
                        ActiveObjetsVueApplicative(false);
                        ActiveObjetsVueInfrastructure(false);
                        ActiveObjetsVueSites(false);
                        ActiveObjetsVuePhysique(false);
                        ActiveObjetsVueSan(false);
                        ActiveObjetsVueSanSwitch(false);
                        ActiveObjetsVueCTI(false);
                        ActiveObjetsVueCadreRef(false);
                        ActiveObjetsVueSIApp(false);
                        ActiveObjetsVueSIInf(true);
                        ActiveObjetsVueAxes(false);
                        break;
                    case 'Y': // Y-Cadre Ref
                        tvObjet.Nodes.Add("CadreRef", "Cadre de Reference");
                        oCnxBase.InitCadreRef('T', tvObjet.Nodes[0].Nodes, "Root");
                        ActiveObjetsVueFonctionnelle(false);
                        ActiveObjetsVueApplicative(false);
                        ActiveObjetsVueInfrastructure(false);
                        ActiveObjetsVueSites(false);
                        ActiveObjetsVuePhysique(false);
                        ActiveObjetsVueSan(false);
                        ActiveObjetsVueSanSwitch(false);
                        ActiveObjetsVueCTI(false);
                        ActiveObjetsVueCadreRef(true);
                        ActiveObjetsVueSIApp(false);
                        ActiveObjetsVueSIInf(false);
                        ActiveObjetsVueAxes(false);
                        break;
                    case 'Z': // Z-Report
                        ActiveObjetsVueFonctionnelle(false);
                        ActiveObjetsVueApplicative(false);
                        ActiveObjetsVueInfrastructure(false);
                        ActiveObjetsVueSites(false);
                        ActiveObjetsVuePhysique(false);
                        ActiveObjetsVueSan(false);
                        ActiveObjetsVueSanSwitch(false);
                        ActiveObjetsVueCTI(false);
                        ActiveObjetsVueCadreRef(false);
                        ActiveObjetsVueSIApp(false);
                        ActiveObjetsVueSIInf(false);
                        ActiveObjetsVueAxes(true);
                        break;
                }
            }
        }

        void cbOpApp_SelectedIndexChanged(object sender, EventArgs e)
        {
            bOpApp.Text = (string)cbOpApp.SelectedItem;
            if (bOpApp.Text == "Creat App") setCtrlEnabled(bOpApp, true);
            else
            {
                if (GetGuidApplication() != null) setCtrlEnabled(bOpApp, true); else setCtrlEnabled(bOpApp, false);
            }
        }

        void cbOpVue_SelectedIndexChanged(object sender, EventArgs e)
        {
            bOpVue.Text = (string)cbOpVue.SelectedItem;

            if (bOpVue.Text == "Creat Vue") {
                if (GetGuidApplication() != null) setCtrlEnabled(bOpVue, true); else setCtrlEnabled(bOpVue, false);
            }
            else
            {
                if (GetGuidVue() != null) setCtrlEnabled(bOpVue, true); else setCtrlEnabled(bOpVue, false);
            }
        }
        /*
        void cbTypeVue_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbVue.Text = "";
            ChangeTreeViewObjet();
            drawArea.Capture = true;
            drawArea.GraphicsList.UnselectAll();
            drawArea.Refresh();
            drawArea.SetDirty();
            //throw new NotImplementedException();
        }*/

        void cbVue_SelectedIndexChanged(object sender, EventArgs e)
        {
            drawArea.Focus();

            //sGuidVueInf = null;
            //sNomEnvironnement = null;
            //sGuidTemplate = null;

            //tbVueInf.Text = "";
            //tbTypeVue.Text = "";
        }

        /*public void SetGuidApplication(Guid GuidApp)
        {
            string sLayersTemp = " (null)";
            bOktoSave = true;

            GuidApplication = GuidApp;
            if (oCnxBase.CBRecherche("SELECT GuidLayer, NomLayer FROM Layer Where GuidApplication='" + GetGuidApplication() + "'"))
            {
                while (oCnxBase.Reader.Read())
                    sLayersTemp += ";" + oCnxBase.Reader.GetString(1) + "     (" + oCnxBase.Reader.GetString(0) + ")";
            }
            oCnxBase.CBReaderClose();
            sLayers = sLayersTemp.Substring(1);
        }*/

        public string GetGuidAppVersion()
        {
            if (wkApp != null) return wkApp.GuidAppVersion.ToString();
            return "";
        }

        public string GetGuidApplication()
        {
            if (wkApp != null) return wkApp.Guid.ToString();
            return "";
            //if (GuidApplication != new Guid("00000000-0000-0000-0000-000000000000")) return GuidApplication.ToString();
            //if (cbApplication.SelectedIndex != -1) return (string)cbGuidApplication.Items[cbApplication.SelectedIndex];
            //return null;
            //GuidApplication
            //bug sur nouvelle Vue
        }

        public string GetTrigramme(string sGuidApp)
        {
            string sGuidApplication = null;
            string sTrigramme = null;
            if (sGuidApp == null) sGuidApplication = GetGuidApplication(); else sGuidApplication = sGuidApp;
            if (sGuidApplication != null)
            {
                if (oCnxBase.CBRecherche("SELECT Trigramme FROM Application WHERE GuidApplication='" + sGuidApplication + "'"))
                {
                    if (!oCnxBase.Reader.IsDBNull(0)) sTrigramme = oCnxBase.Reader.GetString(0);
                }
                oCnxBase.CBReaderClose();
            }
            return sTrigramme;
        }

        public string GetGuidVue()
        {
            if (cbVue.SelectedIndex != -1) return (string)cbGuidVue.Items[cbVue.SelectedIndex];
            else return null;
            //GuidApplication
            //bug sur nouvelle Vue
        }

        public void initVarApp()
        {
            drawArea.GraphicsList.Clear();
            //cbEnv.Text = "";
            tbEnv.Text = "";
            tbTypeVue.Text = "";
            //cbTypeVue.Text = "";
            cbVue.Text = "";
            cbApplication.Text = "";
            tbVueInf.Text = "";
            //cbVueInf.Text = "";
            cbVersion.Text = "";
            wkApp = null;
            //GuidApplication = new Guid("00000000-0000-0000-0000-000000000000");
            ClearApp();
            drawArea.Refresh();
            cbVersion.Items.Clear();
            //cbGuidVueInf.Items.Clear();
            cbGuidVue.Items.Clear();
            //cbVueInf.Items.Clear();
            cbVue.Items.Clear();
            //cbEnv.Items.Clear();
            tvObjet.Nodes.Clear();
            cbGuidVue.SelectedItem = null;
            cbVue.SelectedItem = null;
            //cbGuidVueInf.SelectedItem = null;
            //cbVueInf.SelectedItem = null;
            //cbEnv.SelectedItem = null;
            //cbTypeVue.SelectedItem = null;
            setCtrlEnabled(bSave, false);
            setCtrlEnabled(bOpVue, false);
        }

        public void initKnownDTUserLst()
        {
            lstKnownDTUser.Add(new string[2] { "635402", "Nicolas LE" });
            lstKnownDTUser.Add(new string[2] { "f47915", "Rihem MRISSA" });
            lstKnownDTUser.Add(new string[2] { "e82474", "Christophe PAN" });
            lstKnownDTUser.Add(new string[2] { "e16907", "Jerome PENE SEKU" });
            lstKnownDTUser.Add(new string[2] { "204480", "Frantz SOTIER" });
            lstKnownDTUser.Add(new string[2] { "140488", "Gilles ALDEGUER" });
        }

        void cbVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearVue(true);
            cbGuidVue.Text = ""; cbGuidVue.Items.Clear();
            cbVue.Text = ""; cbVue.Items.Clear();


            if (cbVersion.SelectedIndex != -1)
            {
                if (oCnxBase.CBRecherche("SELECT Parameter From OptionsDraw WHERE NumOption=2")) // forme powerpoint
                {
                    oCnxBase.Reader.Read();
                    if (oCnxBase.Reader.GetString(0) == "Oui") bPtt = true;
                    else bPtt = false;
                }
                else bPtt = false;
                oCnxBase.CBReaderClose();

                List<string[]> lstGuidVue = lstAppVersion.FindAll(el => el[1] == (string)cbVersion.Items[cbVersion.SelectedIndex]);
                wkApp = new WorkApplication(this, (string)cbGuidApplication.Items[cbApplication.SelectedIndex], (string)cbApplication.SelectedItem, lstGuidVue[0][0], lstGuidVue[0][1]);
                for (int i = 0; i < lstGuidVue.Count; i++)
                {
                    if (lstGuidVue[i][2] != "" && cbGuidVue.FindString(lstGuidVue[i][2]) == -1)
                    {
                        cbGuidVue.Items.Add(lstGuidVue[i][2]);
                        cbVue.Items.Add(lstGuidVue[i][3]);
                    }
                }
            }
            else
            {
                setCtrlEnabled(bOpVue, false);
            }
        }

        void cbApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearVue(true);
            cbGuidVue.Text = ""; cbGuidVue.Items.Clear();
            cbVue.Text = ""; cbVue.Items.Clear();
            cbVersion.Text = ""; cbVersion.Items.Clear();
            lstAppVersion.Clear();

            if (cbApplication.SelectedIndex != -1)
            {

#if APIREADY
                clApplication clApp = lstApps.applications[cbApplication.SelectedIndex];
                for (int i = 0; i < clApp.appVersions.Count; i++)
                {
                    if(clApp.appVersions[i].vues.Count == 0)
                    {
                        string[] aEnreg = new string[4];

                        aEnreg[0] = clApp.appVersions[i].guidAppVersion;
                        aEnreg[1] = clApp.appVersions[i].version;
                        aEnreg[2] = "";
                        aEnreg[3] = "";
                        lstAppVersion.Add(aEnreg);
                    }
                    for (int j = 0; j < clApp.appVersions[i].vues.Count; j++) {
                        string[] aEnreg = new string[4];

                        aEnreg[0] = clApp.appVersions[i].guidAppVersion;
                        aEnreg[1] = clApp.appVersions[i].version;
                        aEnreg[2] = clApp.appVersions[i].vues[j].guidVue;
                        aEnreg[3] = clApp.appVersions[i].vues[j].nomVue;
                        lstAppVersion.Add(aEnreg);
                    }
                }
#else

                if (oCnxBase.CBRecherche("SELECT AppVersion.GuidAppVersion, Version, GuidVue, NomVue FROM AppVersion Left Join Vue On AppVersion.GuidAppVersion=Vue.GuidAppVersion Where GuidApplication = '" + (string)cbGuidApplication.Items[cbApplication.SelectedIndex] + "' ORDER BY Version, NomVue"))
                {
                    while (oCnxBase.Reader.Read())
                    {
                        string[] aEnreg = new string[4];
                        aEnreg[0] = oCnxBase.Reader.GetString(0);
                        aEnreg[1] = oCnxBase.Reader.GetString(1);
                        aEnreg[2] = oCnxBase.Reader.IsDBNull(2) ? "" : oCnxBase.Reader.GetString(2);
                        aEnreg[3] = oCnxBase.Reader.IsDBNull(3) ? "" : oCnxBase.Reader.GetString(3);
                        lstAppVersion.Add(aEnreg);
                    }
                }
                oCnxBase.CBReaderClose();
#endif
                for (int i = 0; i < lstAppVersion.Count; i++)
                {
                    if (cbVersion.FindString(lstAppVersion[i][1]) == -1)
                        cbVersion.Items.Add(lstAppVersion[i][1]);
                }
            }

        }


        void tbNom_Validated(object sender, EventArgs e)
        {
            TextBox tb;

            tb = (TextBox)sender;
            if (drawArea.OSelected != null) drawArea.OSelected.ChangeName((string)tb.Text);

            //throw new NotImplementedException();
        }




        #endregion


        #region Main

        Thread newThread = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
#if OIDC
            Compte.Login().GetAwaiter().GetResult();
#else

            string[] propCompte = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);

            Compte.lstRoles.Add("78201911-53be-4074-9d7a-6ff2cbf809aa"); // Roles lié au profil Architecte [Admin]
            Compte.lstRoles.Add("b3d2b96b-36ad-4114-8ff7-29109d8c0144"); // Roles lié au profil Architecte [Fonctionnel]
            Compte.id = propCompte[1];

#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.DoEvents();

            // Check command line
            if (args.Length > 1)
            {
                MessageBox.Show("Incorrect number of arguments. Usage: DrawTools.exe [file]", "DrawTools");
            }


            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            
            // Load main form, taking command line into account
            Form1 form = new Form1();
            if (form.SelectedBase != null)
            {
                if (args.Length == 1)
                    form.ArgumentFile = args[0];

                form.cbApplication.Focus();
                Application.Run(form);
            }
        }

        // The thread we start up to demonstrate non-UI exception handling.
        void newThread_Execute()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        // Handle the UI exceptions by showing a dialog box, and asking the user whether
        // or not they wish to abort execution.
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs t)
        {
            DialogResult result = DialogResult.Cancel;
            try
            {
                result = ShowThreadExceptionDialog("Windows Forms Error", t.Exception);
            }
            catch
            {
                try
                {
                    MessageBox.Show("Fatal Windows Forms Error",
                        "Fatal Windows Forms Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }

            // Exits the program when the user clicks Abort.
            if (result == DialogResult.Abort)
                Application.Exit();
        }

        // Handle the UI exceptions by showing a dialog box, and asking the user whether
        // or not they wish to abort execution.
        // NOTE: This exception cannot be kept from terminating the application - it can only
        // log the event, and inform the user about it.
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                string errorMsg = "An application error occurred. Please contact the adminstrator " +
                    "with the following information:\n\n";

                // Since we can't prevent the app from terminating, log this to the event log.
                if (!EventLog.SourceExists("ThreadException"))
                {
                    EventLog.CreateEventSource("ThreadException", "Application");
                }

                // Create an EventLog instance and assign its source.
                EventLog myLog = new EventLog();
                myLog.Source = "ThreadException";
                myLog.WriteEntry(errorMsg + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace);
            }
            catch (Exception exc)
            {
                try
                {
                    MessageBox.Show("Fatal Non-UI Error",
                        "Fatal Non-UI Error. Could not write the error to the event log. Reason: "
                        + exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }
        }

        // Creates the error message and displays it.
        private static DialogResult ShowThreadExceptionDialog(string title, Exception e)
        {
            string errorMsg = "An application error occurred. Please contact the adminstrator " +
                "with the following information:\n\n";
            errorMsg = errorMsg + e.Message + "\n\nStack Trace:\n" + e.StackTrace;
            return MessageBox.Show(errorMsg, title, MessageBoxButtons.AbortRetryIgnore,
                MessageBoxIcon.Stop);
        }

        #endregion

        #region Members

        private string argumentFile = "";   // file name from command line

        const string registryPath = "Software\\Gilles\\DrawTools";
        public double xRatio;
        public double yRatio;
        public Point pTranslate;
        public bool bTemporaire;
        public CnxBase oCnxBase;
        public string sPathRoot = null;
        public string sGuidTemplate = null;
        public List<string[]> lstAppVersion = new List<string[]>();
        public List<string[]> lstKnownDTUser = new List<string[]>();
        public List<DrawObject> lstObject = new List<DrawObject>();
        //public string sLayers = null;
        //public string sTabFile = null;
        public WorkApplication wkApp;
        //public Guid GuidApplication;
        public Guid GuidTypeVue;
        public Guid GuidVue;
        public Guid GuidGVue;
        public string sTypeVue;
        public string sGuidVueInf;
        public string sNomEnvironnement;
        public bool bPtt = false;
        public XmlDocument docXml;
        public ExpObj oCureo = null;
        public bool bActiveToolTip = false;
        public Color[] colors = new Color[10];
        public LstApplications lstApps;
        public LstApplications lstVues;
        public ApplicationResp AppData;
        public bool[] bDevelop = new bool[10];

        #endregion

        #region Properties

        /// <summary>
        /// File name from the command line
        /// </summary>
        public string ArgumentFile
        {
            get
            {
                return argumentFile;
            }
            set
            {
                argumentFile = value;
            }
        }

        public void setCtrlEnabled(Control ctrl, bool value)
        {
            if (value)
            {
                // Check des droits  du compte connecté
                ctrl.Enabled = Compte.GetRighttoObj(ctrl.Tag);
            }
            else ctrl.Enabled = value;
        }


        #endregion

        #region Event Handlers

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, System.EventArgs e)
        {


            LoadSettingsFromRegistry();

            // Initialize les coleurs
            colors[0] = Color.DarkCyan;
            colors[1] = Color.DarkBlue;
            colors[2] = Color.DarkGreen;
            colors[3] = Color.DarkMagenta;
            colors[4] = Color.DarkOrange;
            colors[5] = Color.DarkRed;
            colors[6] = Color.DarkViolet;
            colors[7] = Color.YellowGreen;
            colors[8] = Color.DarkSalmon;
            colors[9] = Color.Black;

            if (SelectedBase != null)
            {
                Text = "DrawTools - " + SelectedBase;
                // check si le compte exist, dans le cas contraire creat compte
                if (Compte.guid == null)
                {
                    if (oCnxBase.CBRecherche("SELECT guidcompte From compte Where nomcompte='" + Compte.id + "'"))
                    {
                        oCnxBase.Reader.Read();
                        Compte.guid = oCnxBase.Reader.GetString(0);
                        oCnxBase.CBReaderClose();
                    }
                    else
                    {
                        oCnxBase.CBReaderClose();
                        Compte.guid = Guid.NewGuid().ToString();
                        oCnxBase.CBWrite("insert into compte (guidcompte, nomcompte) values('" + Compte.guid + "','" + Compte.id + "')");
                    }
                }
                //Initialise le template graphique
                // Origine : d163540a-c7c3-405a-b700-979a4ba33b70
                // Blue : c49b44f1-3caf-47c8-9c5f-55d603130274
                if (oCnxBase.CBRecherche("SELECT NumOption, Parameter From OptionsDraw Where NumOption=4")) //Template Graphique
                {
                    oCnxBase.Reader.Read();
                    sGuidTemplate = oCnxBase.Reader.GetString(1);
                }
                else sGuidTemplate = "";
                oCnxBase.CBReaderClose();
                // Initialize draw area
                ResizeDrawArea();
                drawArea.Initialize(this);



                //Initialize le FileSystem App
                if (oCnxBase.CBRecherche("SELECT NumOption, Parameter From OptionsDraw Where NumOption=0")) //RootPath
                {
                    oCnxBase.Reader.Read();
                    sPathRoot = oCnxBase.Reader.GetString(1);
                }
                else sPathRoot = @"C:\";
                oCnxBase.CBReaderClose();

                //Initialize la cbTypeVue
                //oCnxBase.CBAddComboBox("SELECT NomTypeVue FROM TypeVue ORDER BY NomTypeVue", this.cbTypeVue);
                ActiveObjetsVueFonctionnelle(false);
                ActiveObjetsVueApplicative(false);
                ActiveObjetsVueInfrastructure(false);
                ActiveObjetsVueSites(false);
                ActiveObjetsVuePhysique(false);
                ActiveObjetsVueSan(false);
                ActiveObjetsVueSanSwitch(false);
                ActiveObjetsVueCTI(false);
                ActiveObjetsVueCadreRef(false);
                ActiveObjetsVueSIApp(false);
                ActiveObjetsVueSIInf(false);
                ActiveObjetsVueAxes(false);

                //Initialize cbApplication
                InitCbApplication();
            }
            else this.Close();
        }

        /*
        public void InitCbVue()
        {
            ClearVue(true);
            cbGuidVue.Items.Clear();
            cbVue.Items.Clear();
            //this.cbGuidVueInf.Items.Clear();
            //this.cbVueInf.Items.Clear();
            //this.cbEnv.Items.Clear();
            tvObjet.Nodes.Clear();
            oCnxBase.CBAddComboBox("SELECT GuidVue, NomVue FROM Vue Where GuidApplication='" + GetGuidApplication() + "' ORDER BY NomVue", this.cbGuidVue, this.cbVue);
        }
        */

        public void InitCbApplication()
        {
            ClearApp();
            ClearVue(true);
            //cbApplication.Items.Clear();
            string sTri = "NomApplication";
            if (NomApplication.Text != "Appli") sTri = "Trigramme, NomApplication";

#if APIREADY
            using (var webClient = new System.Net.WebClient())
            {
                //webClient.Headers.Add("Content-Type", "application/json; charset=utf-8");
                webClient.Encoding = System.Text.UTF8Encoding.UTF8;
                webClient.DownloadDataCompleted += webClient_GetApp;
                //Uri urlToRequest = new Uri(@"http://localhost:3001/appsvues");
                Uri urlToRequest = new Uri(@"http://localhost:8080/AppsVues");
                webClient.DownloadDataAsync(urlToRequest);
            }

            //System.Net.WebClient client = new System.Net.WebClient();
            //client.Headers.Add("content-type", "application/json");//set your header here, you can add multiple headers
            //string s = System.Text.Encoding.ASCII.GetString(client.UploadData("http://localhost:1111/Service.svc/SignIn", "POST", System.Text.Encoding.Default.GetBytes("{\"EmailId\": \"admin@admin.com\",\"Password\": \"pass#123\"}")));

#else
            oCnxBase.CBAddComboBox("SELECT GuidApplication, NomApplication, Trigramme FROM Application ORDER BY " + sTri, this.cbGuidApplication, this.cbApplication);
#endif
            initVarApp();
            //oCnxBase.CBAddComboBox("SELECT NomApplication FROM Application ORDER BY NomApplication", this.cbApplication);
            //oCnxBase.CBAddComboBox("SELECT NomApplication, Trigramme FROM Application ORDER BY NomApplication", this.cbApplication);
        }



        /// <summary>
        /// Idle processing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_Idle(object sender, EventArgs e)
        {
            SetStateOfControls();
        }

        /// <summary>
        /// Exit menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// About menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuHelpAbout_Click(object sender, System.EventArgs e)
        {
            /*
            using System.DirectoryServices.AccountManagement;

            string fullName = null;
            using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
            {
                using (UserPrincipal user = UserPrincipal.FindByIdentity(context, "hajani"))
                {
                    if (user != null)
                    {
                        fullName = user.DisplayName;
                        lbl_Login.Text = fullName;
                    }
                }
            }
            */

            MessageBox.Show(System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            CommandAbout();
        }


        /// <summary>
        /// Resize draw area when form is resized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, System.EventArgs e)
        {
            this.cbApplication.Focus();
            if (this.WindowState != FormWindowState.Minimized)
            {
                ResizeDrawArea();
            }
        }

        /// <summary>
        /// Toolbar buttons handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            SwitchCommand(e.Button);
        }

        private void SwitchCommand(ToolBarButton tb)
        {
            if (tb == tbPointer) CommandPointer();
            else if (tb == tbRectangle) CommandRectangle();
            else if (tb == tbEllipse) CommandEllipse();
            else if (tb == tbLine) CommandLine();
            else if (tb == tbPolygon) CommandPolygon();
            else if (tb == tbAbout) CommandAbout();
            else if (tb == tbSave) CommandSave();
            else if (tb == tbModule) CommandModule();
            else if (tb == tbLink) CommandLink();
            else if (tb == tbServer) CommandServer();
            else if (tb == tbBase) CommandBase();
            else if (tb == tbQueue) CommandQueue();
            else if (tb == tbFile) CommandFile();
            else if (tb == tbLinkA) CommandLinkA();
            else if (tb == tbGenks) CommandGenks();
            else if (tb == tbGensas) CommandGensas();
            else if (tb == tbManagedsvc) CommandManagedsvc();
            else if (tb == tbPattern) CommandPattern("Creat");
            else if (tb == tbInsks) CommandInsks();
            else if (tb == tbInssas) CommandInssas();
            else if (tb == tbGenpod) CommandGenpod();
            else if (tb == tbContainer) CommandContainer();
            else if (tb == tbGening) CommandGening();
            else if (tb == tbGensvc) CommandGensvc();
            else if (tb == tbPatternIns) CommandPattern("Ins");
            else if (tb == tbComposant) CommandComposant();
            else if (tb == tbMainComposant) CommandMainComposant();
            else if (tb == tbCompFonc) CommandCompFonc();
            else if (tb == tbServMComp) CommandServMComp();
            else if (tb == tbTechno) CommandTechno();
            else if (tb == tbLinkI) CommandLinkI();
            else if (tb == tbServeurE) CommandServerPhy();
            else if (tb == tbInsnd) CommandInsnd();
            else if (tb == tbcard) CommandNCard();
            else if (tb == tbVlan) CommandVLan();
            else if (tb == tbRouter) CommandRouter();
            else if (tb == tbFlux) CommandAideFlux();
            else if (tb == tbFluxBoutEnBout) CommandAideFluxBoutenBout("App");
            else if (tb == tbFluxBoutEnBoutFonc) CommandAideFluxBoutenBout("Fonc");
            else if (tb == tbUser) CommandUser();
            else if (tb == tbApplication) CommandApplication();
            else if (tb == tbInterface) CommandInterface();
            else if (tb == tbEACB) CommandEACB();
            else if (tb == tbStatut) CommandStatutApp();
            else if (tb == tbCluster) CommandCluster();
            else if (tb == tbMachine) CommandMachine();
            else if (tb == tbVirtuel) CommandVirtuel();
            else if (tb == tbBaie1) CommandBaie();
            else if (tb == tbLun) CommandLun();
            else if (tb == tbZone) CommandZone();
            else if (tb == tbBaiePhy) CommandBaiePhy();
            else if (tb == tbMachineCTI) CommandMachineCTI();
            else if (tb == tbBaieCTI) CommandBaieCTI();
            else if (tb == tbDrawer) CommandDrawer();
            else if (tb == tbSanCard) CommandSanCard();
            else if (tb == tbSanSwitch) CommandSanSwitch();
            else if (tb == tbISL) CommandISL();
            else if (tb == tbBaieDPhy) CommandBaieDPhy();
            else if (tb == tbSite) CommandLocation();
            else if (tb == tbServerSite) CommandServerSite();
            else if (tb == tbCnx) CommandCnx();
            else if (tb == tbPtCnx) CommandPtCnx();
            else if (tb == tbTechUser) CommandTechUser();
            else if (tb == tbCadreRefN) CommandCadreRefN();
            else if (tb == tbCadreRefN1) CommandCadreRefN1();
            else if (tb == tbIndicator) CommandIndicator();
            else if (tb == tbInterLink) CommandInterLink();
            else if (tb == tbPatrimoine) CommandPatrimoine();
            else if (tb == tbVisio) CommandVisio();
            else if (tb == tbSi) CommandSi();
            else if (tb == tbAxes) CommandAxes();
            else if (tb == tbExportXLSReport) CommandExportXLSReport();
            else if (tb == tbEACB) CommandReport();
            SetStateOfControls();
        }

        #region File Menu


        /// <summary>
        /// File - Save menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileSave_Click(object sender, System.EventArgs e)
        {
            CommandSave();
        }


        #endregion

        #region Draw menu

        /// <summary>
        /// Pointer Tool menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuDrawPointer_Click(object sender, System.EventArgs e)
        {
            CommandPointer();
        }

        /// <summary>
        /// Rectangle Tool menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuDrawRectangle_Click(object sender, System.EventArgs e)
        {
            CommandRectangle();
        }

        /// <summary>
        /// Ellipse Tool menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuDrawEllipse_Click(object sender, System.EventArgs e)
        {
            CommandEllipse();
        }

        /// <summary>
        /// Line Tool menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuDrawLine_Click(object sender, System.EventArgs e)
        {
            CommandLine();
        }

        /// <summary>
        /// Polygon Tool menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuDrawPolygon_Click(object sender, System.EventArgs e)
        {
            CommandPolygon();
        }

        #endregion

        #region Edit Menu

        private void menuEditSelectAll_Click(object sender, System.EventArgs e)
        {
            drawArea.GraphicsList.SelectAll();
            drawArea.Refresh();
        }

        private void menuEditUnselectAll_Click(object sender, System.EventArgs e)
        {
            drawArea.GraphicsList.UnselectAll();
            drawArea.Refresh();
        }

        private void menuEditDelete_Click(object sender, System.EventArgs e)
        {
            FormDeleteObject fd = new FormDeleteObject(this);
            fd.ShowDialog(this);
        }

        private void ClearApp()
        {
            ClearVue(true);

            cbApplication.Text = ""; cbApplication.SelectedIndex = -1;
            wkApp = null;
            //GuidApplication = new Guid("00000000-0000-0000-0000-000000000000");
        }

        public void ClearVue(bool bAll)
        {
            drawArea.GraphicsList.Clear();
            drawArea.MajObjets();
            setCtrlEnabled(bSave, false);
            setCtrlEnabled(bOpVue, false);
            tbVueInf.Text = "";
            //cbVueInf.Text = ""; cbVueInf.SelectedIndex = -1;cbVueInf.Items.Clear();
            //cbGuidVueInf.Text = "";
            if (bAll) { cbVue.Items.Clear(); cbVue.Text = ""; cbVue.SelectedIndex = -1; cbGuidVue.Text = ""; cbGuidVue.SelectedIndex = -1; cbGuidVue.Items.Clear(); }
            tbEnv.Text = "";
            //cbEnv.Text = ""; cbEnv.SelectedIndex = -1;
            tbTypeVue.Text = "";
            //cbTypeVue.Text = ""; cbTypeVue.SelectedIndex = -1;
            GuidVue = new Guid("00000000-0000-0000-0000-000000000000");
            GuidGVue = new Guid("00000000-0000-0000-0000-000000000000");
            GuidTypeVue = new Guid("00000000-0000-0000-0000-000000000000");
            sGuidVueInf = null;
            sNomEnvironnement = null;
            sGuidTemplate = null;
        }

        private void menuEditDeleteAll_Click(object sender, System.EventArgs e)
        {
            ClearVue(false);
            cbVue.Text = ""; cbVue.SelectedIndex = -1;
        }

        private void menuEditLinkView_Click(object sender, System.EventArgs e)
        {
            GraphicsList graphlst = drawArea.GraphicsList;
            int n = graphlst.Count;

            for (int i = n - 1; i >= 0; i--)
            {
                DrawObject o = (DrawObject)graphlst[i];

                if (o.Selected)
                {
                    o.Link();
                    /*
                    string fullpath = GetFullPath(o.GuidkeyObjet.ToString());
                    if (fullpath != null)
                    {
                        if (oCnxBase.CBRecherche("SELECT NomVue FROM Vue WHERE GuidTypeVue='d5b533a9-06ac-4f8c-a5ab-e345b0212542' AND GuidApplication='" + o.GuidkeyObjet + "'"))
                        {
                            string sNomVue = oCnxBase.Reader.GetString(0);
                            oCnxBase.CBReaderClose();
                            if (File.Exists(fullpath + "\\" + sNomVue + ".jpg"))
                            {
                                Process ExecVue = new Process();

                                ExecVue.StartInfo.FileName = fullpath + "\\" + sNomVue + ".jpg";
                                ExecVue.Start();                                    
                            }
                        }
                        oCnxBase.CBReaderClose();
                    }
                    */

                    break;
                }
            }
        }

        private void menuEditRules_Click(object sender, System.EventArgs e)
        {
            GraphicsList graphlst = drawArea.GraphicsList;
            int n = graphlst.Count;

            for (int i = n - 1; i >= 0; i--)
            {
                DrawObject o = (DrawObject)graphlst[i];

                if (o.Selected)
                {
                    FormRules fr = new FormRules(this);
                    fr.init();
                }
            }
        }

        private void menuCreateLayer_Click(object sender, System.EventArgs e)
        {
            FormLayer fl = new FormLayer(this);
            fl.init();
        }

        private void menuSelectLayer_Click(object sender, System.EventArgs e)
        {
            FormChangeProp fcp = new FormChangeProp(this, null);

            if (GetGuidApplication() != null && drawArea.GraphicsList.SelectionCount == 1)
            {
                DrawObject o = drawArea.GraphicsList.GetSelectedObject(0);
                fcp.AddlSourceFromDB("SELECT GuidLayer, NomLayer FROM Layer Where GuidAppVersion='" + GetGuidAppVersion() + "'", "Create");
                fcp.AddlDestinationFromDB("SELECT Layer.GuidLayer, NomLayer FROM Layer, LayerLink Where Layer.GuidLayer=LayerLink.GuidLayer and GuidObj='" + o.GuidkeyObjet + "' and Layer.GuidAppVersion='" + GetGuidAppVersion() + "'");
                fcp.ShowDialog(this);
                if (fcp.Valider)
                {
                    string[] aValue = oCnxBase.CmdText.Split('(', ')');
                    //oCnxBase.CBWrite("Delete From LayerLink Where GuidObj='" + o.GuidkeyObjet + "' and GuidLayer in (Select GuidLayer From Layer Where GuidApplication='" + GetGuidApplication() + "')");
                    oCnxBase.CBWrite("Delete From LayerLink Where GuidObj='" + o.GuidkeyObjet + "' and GuidAppVersoin='" + GetGuidAppVersion() + "'");
                    for (int i = 1; i < aValue.Length; i += 2)
                    {
                        oCnxBase.CBWrite("Insert Into LayerLink (GuidObj, GuidLayer, GuidAppVersion) Value('" + o.GuidkeyObjet + "','" + aValue[i] + "','" + GetGuidAppVersion() + "')");
                    }

                }
            }
        }

        private void menuDeleteLayer_Click(object sender, System.EventArgs e)
        {
        }

        private void menuEditObjectExplorer_Click(object sender, System.EventArgs e)
        {
            GraphicsList graphlst = drawArea.GraphicsList;
            int n = graphlst.Count;

            for (int i = n - 1; i >= 0; i--)
            {
                DrawObject o = (DrawObject)graphlst[i];

                if (o.Selected)
                {
                    ExpObj eo = new ExpObj(o.GetGuidForObjExp(), o.Texte, o.GetToolTypeForObjExp());
                    FormExplorObj feo = new FormExplorObj(this);
                    feo.init(eo);
                    oCureo = null;
                    break;
                }
            }
        }

        private void menuEditLinkDiagrem_Click(object sender, System.EventArgs e)
        {
            /*
            GraphicsList graphlst = drawArea.GraphicsList;
            int n = graphlst.Count;

            for (int i = n - 1; i >= 0; i--)
            {
                DrawObject o = (DrawObject)graphlst[i];

                if (o.Selected)
                {
                    string sVue = (string) o.GetValueFromName("NomVue");
                    if (sVue!="" && drawArea.GraphicsList.Clear())
                    {
                        if (oCnxBase.CBRecherche("SELECT GuidVue, NomVue, NomTypeVue, Application.GuidApplication, Vue.GuidTypeVue FROM Application, Vue, typeVue WHERE Vue.GuidApplication=Application.GuidApplication AND Vue.GuidTypeVue=TypeVue.GuidTypeVue AND NomVue ='" + sVue + "'"))
                        {
                            oCnxBase.Reader.Read();
                            GuidVue = new Guid(oCnxBase.Reader.GetString(0));
                            //GuidApplication = new Guid(oCnxBase.Reader.GetString(3));
                            //wkApp = new WorkApplication(this, oCnxBase.Reader.GetString(3), null);
                            tbTypeVue.Text = oCnxBase.Reader.GetString(2);
                            //cbTypeVue.SelectedItem = oCnxBase.Reader.GetString(2);
                            cbGuidVue.Text = oCnxBase.Reader.GetString(0);
                            oCnxBase.CBReaderClose();
                            cbVue.Text = sVue; ;
                            bSave.Enabled = true;
                            //LoadVue(); // LoadVue est déclenché par l'evenement changeVue
                            drawArea.MajObjets();
                        }
                        else oCnxBase.CBReaderClose();
                    }
                    break;
                }
            }
            */
        }

        private void menuEditMoveToFront_Click(object sender, System.EventArgs e)
        {
            if (drawArea.GraphicsList.MoveSelectionToFront())
                drawArea.MajObjets();
        }

        private void menuEditMoveToBack_Click(object sender, System.EventArgs e)
        {
            if (drawArea.GraphicsList.MoveSelectionToBack())
                drawArea.MajObjets();
        }

        private void menuEditProperties_Click(object sender, System.EventArgs e)
        {

            GraphicsList graphlst = drawArea.GraphicsList;
            int n = graphlst.Count;

            for (int i = n - 1; i >= 0; i--)
            {
                DrawObject o = (DrawObject)graphlst[i];

                if (o.Selected)
                {
                    FormPropWord fp = new FormPropWord(this, o);
                    fp.ShowDialog(this);

                    break;
                }
            }

            /*
            if ( drawArea.GraphicsList.ShowPropertiesDialog(this) )
            {
                drawArea.SetDirty();
                drawArea.Refresh();
            }*/

        }

        #endregion

        #endregion

        #region DocManager Event Handlers

        /// <summary>
        /// DocManager reports hat it executed New command.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void docManager_ClearEvent(object sender, EventArgs e)
        {
            if (drawArea.GraphicsList != null)
            {
                drawArea.GraphicsList.Clear();
                drawArea.Refresh();
            }
        }

        /// <summary>
        /// DocManager reports that document was changed (loaded from file)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void docManager_DocChangedEvent(object sender, EventArgs e)
        {
            drawArea.Refresh();
        }

        #endregion



        #region Other Functions

        /// <summary>
        /// Set Pointer draw tool
        /// </summary>
        private void CommandPointer()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
        }

        /// <summary>
        /// Set Rectangle draw tool
        /// </summary>
        private void CommandRectangle()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Rectangle;
        }

        /// <summary>
        /// Set Ellipse draw tool
        /// </summary>
        private void CommandEllipse()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Ellipse;
        }

        /// <summary>
        /// Set Line draw tool
        /// </summary>
        private void CommandLine()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Line;
        }

        /// <summary>
        /// Set Polygon draw tool
        /// </summary>
        private void CommandPolygon()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Polygon;
        }

        private void CommandCluster()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Cluster;
        }

        private void CommandSanCard()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.SanCard;
        }

        private void CommandSanSwitch()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.SanSwitch;
        }

        private void CommandISL()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.ISL;
        }

        private void CommandBaieDPhy()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.BaieDPhy;
        }

        private void CommandMachine()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Machine;
        }

        private void CommandIndicator()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Indicator;
        }

        /*private void CommandMCompApp()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.MCompApp;
        }*/

        private void CommandInterLink()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.InterLink;
        }

        private void CommandVirtuel()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Virtuel;
        }

        private void CommandBaie()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Baie;
        }

        private void CommandBaiePhy()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.BaiePhy;
        }

        private void CommandBaieCTI()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.BaieCTI;
        }

        private void CommandDrawer()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Drawer;
        }

        private void CommandMachineCTI()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.MachineCTI;
        }

        private void CommandLun()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Lun;
        }

        private void CommandServerSite()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.ServerSite;
        }

        private void CommandLocation()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Location;
        }

        private void CommandCnx()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Cnx;
        }

        private void CommandPtCnx()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.PtCnx;
        }

        private void CommandTechUser()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.TechUser;
        }


        private void CommandZone()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Zone;
        }

        private void CommandCadreRefN()
        {
            if (tvObjet.SelectedNode.Nodes.Count != 0) drawArea.ActiveTool = DrawArea.DrawToolType.CadreRefN;
            else drawArea.ActiveTool = DrawArea.DrawToolType.CadreRefEnd;
        }

        private void CommandCadreRefN1()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.CadreRefN1;
        }

        /// <summary>
        /// Show About dialog
        /// </summary>
        private void CommandAbout()
        {
            FormAbout frm = new FormAbout();
            frm.ShowDialog(this);
        }


        /// <summary>
        /// Set Rectangle draw tool
        /// </summary>
        private void CommandModule()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Module;
        }

        private void CommandAxes()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Axes;
        }

        private void CommandExportXLSReport()
        {
            docXml = new XmlDocument();
            docXml.LoadXml("<ListPtNiveau></ListPtNiveau>");
            XmlElement root = docXml.DocumentElement;
            List<String[]> lstApplication = new List<string[]>();

            lstApplication = oCnxBase.GetLstAppWithSoftsarePackage();

            for (int i = 0; i < drawArea.GraphicsList.Count; i++)
            {
                DrawObject o = (DrawObject)drawArea.GraphicsList[i];
                if (o.GetType() == typeof(DrawPtNiveau))
                {
                    DrawPtNiveau optn = (DrawPtNiveau)o;
                    XmlElement elrow = docXml.CreateElement($"row");

                    XmlElement el;
                    el = docXml.CreateElement("GuidPtNiveau"); el.InnerText = (string)optn.GuidObj; elrow.AppendChild(el);
                    el = docXml.CreateElement("NomPtNiveau"); el.InnerText = (string)optn.GetValueFromName("NomPtNiveau"); elrow.AppendChild(el);
                    el = docXml.CreateElement("NivAbs"); el.InnerText = ((double)optn.GetValueFromName("NivAbs")).ToString(); elrow.AppendChild(el);
                    el = docXml.CreateElement("NivOrd"); el.InnerText = ((double)optn.GetValueFromName("NivOrd")).ToString(); elrow.AppendChild(el);
                    if (optn.TypeR == rbTypeRecherche.Application)
                    {
                        if (oCnxBase.CBRecherche("select trigramme from application where guidapplication='" + (string)optn.GuidObj + "'"))
                        { el = docXml.CreateElement("Trigramme"); el.InnerText = oCnxBase.Reader.IsDBNull(0) ? "" : oCnxBase.Reader.GetString(0); elrow.AppendChild(el); }
                        else
                        { el = docXml.CreateElement("Trigramme"); el.InnerText = ""; elrow.AppendChild(el); }
                        oCnxBase.CBReaderClose();
                    }
                    else if(optn.TypeR == rbTypeRecherche.Techno)
                    {
                        if (oCnxBase.CBRecherche("select Norme, valindicator from technoref, indicatorlink where technoref.guidtechnoref = indicatorlink.guidobjet and indicatorlink.guidindicator = 'b00b12bd-a447-47e6-92f6-e3b76ad22830' and technoref.guidtechnoref = '" + (string)optn.GuidObj + "'"))
                        {
                            el = docXml.CreateElement("Norme"); el.InnerText = oCnxBase.Reader.IsDBNull(0) ? "" : el.InnerText = sStatutTechno[oCnxBase.Reader.GetInt32(0)]; elrow.AppendChild(el);
                            el = docXml.CreateElement("DateFinSupport"); el.InnerText = oCnxBase.Reader.IsDBNull(1) ? "" : el.InnerText = DateTime.FromOADate(oCnxBase.Reader.GetDouble(1)).ToShortDateString(); elrow.AppendChild(el);
                        }
                        else
                        {
                            el = docXml.CreateElement("Norme"); el.InnerText = ""; elrow.AppendChild(el);
                            el = docXml.CreateElement("DateFinSupport"); el.InnerText = ""; elrow.AppendChild(el);
                        }
                        oCnxBase.CBReaderClose();

                    }

                    el = docXml.CreateElement("SoftPackage");
                    string[] elApp = lstApplication.Find(ela => ela[0] == optn.GuidObj);
                    if (elApp != null) { el.InnerText = elApp[1]; elrow.AppendChild(el); }
                    root.AppendChild(elrow);
                }
            }
            WorkApplication wkTemp = new WorkApplication(this, "3337bb33-7447-4101-ad3a-a3d2f1002f3f", "STA", "3337bb33-7447-4101-ad3a-a3d2f1002f3f");
            docXml.Save(GetFullPath(wkTemp) + "\\ListPtNiveau.xml");
            docXml.RemoveAll();
        }

        private void CommandCompFonc()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.CompFonc;
        }

        private void CommandServMComp()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.ServMComp;
        }

        private void CommandTechno()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Techno;
        }

        private void CommandComposant()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Composant;
        }

        private void CommandMainComposant()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.MainComposant;
        }

        /// <summary>
        /// Set Rectangle draw tool
        /// </summary>
        private void CommandLink()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Link;
        }

        private void CommandLinkA()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.LinkA;
        }

        private void CommandLinkI()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.TechLink;
        }

        private void CommandServerPhy()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.ServerPhy;
        }

        private void CommandInsnd()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Insnd;
        }

        private void CommandNCard()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.NCard;
        }


        private void CommandVLan()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.VLan;
        }

        private void CommandRouter()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Router;
        }

        private void CommandInterface()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Interface;
        }

        /// <summary>
        /// Set Rectangle draw tool
        /// </summary>
        public void CommandServer()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Server;
        }


        /// <summary>
        /// Set Rectangle draw tool
        /// </summary>
        private void CommandBase()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Base;
        }

        /// <summary>
        /// Set Rectangle draw tool
        /// </summary>
        private void CommandFile()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.File;
        }

        private void CommandQueue()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Queue;
        }

        private void CommandGenks()
        {
            switch (sTypeVue[0]) {
                case '2': //2-Infrastrucutre
                    drawArea.ActiveTool = DrawArea.DrawToolType.Genks;
                    break;
                case '3':
                case '4':
                case '5':
                    drawArea.ActiveTool = DrawArea.DrawToolType.Insks;
                    break;
            }
        }

        private void CommandGensas()
        {
            switch (sTypeVue[0])
            {
                case '2': //2-Infrastrucutre
                    drawArea.ActiveTool = DrawArea.DrawToolType.Gensas;
                    break;
                case '3':
                case '4':
                case '5':
                    drawArea.ActiveTool = DrawArea.DrawToolType.Inssas;
                    break;
            }
        }


        private void CommandManagedsvc()
        {
            switch (sTypeVue[0])
            {
                case '2': //2-Infrastrucutre
                    drawArea.ActiveTool = DrawArea.DrawToolType.Managedsvc;
                    break;
            }
        }

        private void CommandInssas()
        {
            switch (sTypeVue[0])
            {
                case '3':
                case '4':
                case '5':
                    drawArea.ActiveTool = DrawArea.DrawToolType.Inssas;
                    break;
            }
        }
        private void CommandInsks()
        {
            switch (sTypeVue[0])
            {
                case '3':
                case '4':
                case '5':
                    drawArea.ActiveTool = DrawArea.DrawToolType.Insks;
                    break;
            }
        }

        private void CommandGenpod()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Genpod;
        }

        private void CommandPattern(string sFonction)
        {
            if (sFonction == "Creat")
            {
                FormPattern fp = new FormPattern(this);
                fp.ShowDialog(this);
                drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
                drawArea.Owner.SetStateOfControls();
            }
            else if (sFonction == "Ins")
            {
                drawArea.ActiveTool = DrawArea.DrawToolType.Cadre;
            }
        }

        private void CommandContainer()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Container;
        }

        private void CommandGening()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Gening;
        }

        private void CommandGensvc()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Gensvc;
        }

        private void CommandUser()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.AppUser;
        }

        private void CommandApplication()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Application;
        }


        /// <summary>
        /// Set draw area to all form client space except toolbar
        /// </summary>
        private void ResizeDrawArea()
        {
            Rectangle rect = this.ClientRectangle;

            //drawArea.Left = rect.Left;
            //drawArea.Top = rect.Top + toolBar1.Height;
            //drawArea.Width = rect.Width;
            //drawArea.Height = rect.Height - toolBar1.Height;
        }

        /// <summary>
        /// Load application settings from the Registry
        /// </summary>
        void LoadSettingsFromRegistry()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey(registryPath);

                DrawObject.LastUsedColor = Color.FromArgb((int)key.GetValue(
                    "Color",
                    Color.Black.ToArgb()));

                DrawObject.LastUsedPenWidth = (int)key.GetValue(
                    "Width",
                    1);
            }
            catch (ArgumentNullException ex)
            {
                HandleRegistryException(ex);
            }
            catch (SecurityException ex)
            {
                HandleRegistryException(ex);
            }
            catch (ArgumentException ex)
            {
                HandleRegistryException(ex);
            }
            catch (ObjectDisposedException ex)
            {
                HandleRegistryException(ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                HandleRegistryException(ex);
            }
        }

        /// <summary>
        /// Save application settings to the Registry
        /// </summary>
        void SaveSettingsToRegistry()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey(registryPath);

                key.SetValue("Color", DrawObject.LastUsedColor.ToArgb());
                key.SetValue("Width", DrawObject.LastUsedPenWidth);
            }
            catch (SecurityException ex)
            {
                HandleRegistryException(ex);
            }
            catch (ArgumentException ex)
            {
                HandleRegistryException(ex);
            }
            catch (ObjectDisposedException ex)
            {
                HandleRegistryException(ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                HandleRegistryException(ex);
            }
        }

        private void HandleRegistryException(Exception ex)
        {
            Trace.WriteLine("Registry operation failed: " + ex.Message);
        }

        /// <summary>
        /// Set state of controls.
        /// Function is called at idle time.
        /// </summary>
        public void SetStateOfControls()
        {
            // Select active tool
            tbPointer.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Pointer);
            tbRectangle.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Rectangle);
            tbEllipse.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Ellipse);
            tbLine.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Line);
            tbPolygon.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Polygon);
            tbModule.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Module);
            tbLink.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Link);
            tbServer.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Server);
            tbBase.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Base);
            tbQueue.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Queue);
            tbFile.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.File);
            tbLinkA.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.LinkA);
            tbComposant.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Composant);
            tbMainComposant.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.MainComposant);
            //tbCompFonc.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.CompFonc);
            //tbServer.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.ServMComp);
            tbTechno.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Techno);
            tbLinkI.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.TechLink);
            tbServeurE.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.ServerPhy);
            tbcard.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.NCard);
            tbVlan.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.VLan);
            tbRouter.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Router);
            tbUser.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.AppUser);
            tbApplication.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Application);
            tbInterface.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Interface);
            tbCluster.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Cluster);
            tbMachine.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Machine);
            tbBaie1.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Baie);
            tbLun.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Lun);
            tbZone.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Zone);
            tbBaiePhy.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.BaiePhy);
            tbDrawer.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Drawer);
            tbSanCard.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.SanCard);
            tbSanSwitch.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.SanSwitch);
            tbISL.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.ISL);
            tbBaieDPhy.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.BaieDPhy);
            tbSite.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Location);
            tbCnx.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Cnx);
            tbPtCnx.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.PtCnx);
            tbCadreRefN.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.CadreRefN);
            tbCadreRefN1.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.CadreRefN1);
            tbIndicator.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Indicator);
            tbInterLink.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.InterLink);
            tbAxes.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Axes);
            tbGening.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Gening);
            tbGenks.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Genks);
            tbGenpod.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Genpod);
            tbGensas.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Gensas);
            tbGensvc.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Gensvc);
            tbInsks.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Insks);

            tbFluxBoutEnBout.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.FluxBoutEnBout);
            tbFluxBoutEnBoutFonc.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.FluxBoutEnBoutFonc);
            tbFlux.Pushed = (drawArea.ActiveTool == DrawArea.DrawToolType.Flux);


            menuDrawPointer.Checked = (drawArea.ActiveTool == DrawArea.DrawToolType.Pointer);
            menuDrawRectangle.Checked = (drawArea.ActiveTool == DrawArea.DrawToolType.Rectangle);
            menuDrawEllipse.Checked = (drawArea.ActiveTool == DrawArea.DrawToolType.Ellipse);
            menuDrawLine.Checked = (drawArea.ActiveTool == DrawArea.DrawToolType.Line);
            menuDrawPolygon.Checked = (drawArea.ActiveTool == DrawArea.DrawToolType.Polygon);


            bool objects = (drawArea.GraphicsList.Count > 0);
            bool selectedObjects = (drawArea.GraphicsList.SelectionCount > 0);
            bool selectedCadreRef = (drawArea.GraphicsList.SelectionCadreRefCount > 0);

            // File operations
            menuFileSave.Enabled = objects;
            menuFileSaveAs.Enabled = objects;
            tbSave.Enabled = objects;

            // Edit operations
            menuEditDelete.Enabled = selectedObjects;
            menuEditDeleteAll.Enabled = objects;
            menuEditSelectAll.Enabled = objects;
            menuEditUnselectAll.Enabled = objects;
            menuEditMoveToFront.Enabled = selectedObjects;
            menuEditMoveToBack.Enabled = selectedObjects;
            menuEditProperties.Enabled = selectedObjects;
            menuItem23.Enabled = selectedObjects;
            menuItem27.Enabled = selectedObjects;
            menuItem20.Enabled = selectedCadreRef;
        }



        public string GetPath(string Guid)
        {
            string fullpath = "";
            if (oCnxBase.CBRecherche("Select NomArborescence, GuidParent From Arborescence Where GuidArborescence='" + Guid + "'"))
            {
                string NomArborescence = oCnxBase.Reader.GetString(0);
                if (oCnxBase.Reader.IsDBNull(1))
                {
                    fullpath = "\\" + NomArborescence;
                    oCnxBase.CBReaderClose();
                }
                else
                {
                    string Parent = oCnxBase.Reader.GetString(1);
                    oCnxBase.CBReaderClose();
                    fullpath = GetPath(Parent) + "\\" + NomArborescence;
                }
                //else fullpath = path + GetPath(Parent) + NomArborescence + "\\test.jpg";
            }
            else oCnxBase.CBReaderClose();
            return fullpath;
        }

        public string GetFullPath(WorkApplication wkApp)
        {
            string fullpath;

            if (oCnxBase.CBRecherche("Select NomArborescence, GuidParent, NomApplication, Trigramme, Version From Arborescence, Application, AppVersion Where Application.GuidApplication='" + wkApp.Guid + "' AND Application.GuidArborescence=Arborescence.GuidArborescence And Application.GuidApplication=AppVersion.GuidApplication And AppVersion.GuidAppVersion='" + wkApp.GuidAppVersion + "'"))
            {
                string NomArborescence = oCnxBase.Reader.GetString(0);
                if (oCnxBase.Reader.IsDBNull(1))
                {
                    if (oCnxBase.Reader.IsDBNull(3) || oCnxBase.Reader.GetString(3) == "") fullpath = sPathRoot + "\\" + NomArborescence + "\\" + oCnxBase.Reader.GetString(2) + "\\" + oCnxBase.Reader.GetString(4);
                    else fullpath = sPathRoot + "\\" + NomArborescence + "\\" + oCnxBase.Reader.GetString(3) + "\\" + oCnxBase.Reader.GetString(4);
                    oCnxBase.CBReaderClose();
                }
                else
                {
                    string Parent = oCnxBase.Reader.GetString(1);
                    string sVersion = oCnxBase.Reader.GetString(4);
                    string sApp = oCnxBase.Reader.GetString(2);
                    oCnxBase.CBReaderClose();
                    fullpath = sPathRoot + GetPath(Parent) + "\\" + NomArborescence + "\\" + sApp + "\\" + sVersion;
                }

                if (!Directory.Exists(fullpath))
                {
                    Directory.CreateDirectory(fullpath);
                }
                return fullpath;
            }
            else oCnxBase.CBReaderClose();
            return null;
        }


        public string SaveDiagram(string sVue, WorkApplication wkApp, string sGuidObj)
        {
            string fullpath = GetFullPath(wkApp);

            if (fullpath != null) return (SaveDiagramFromPath(sVue, fullpath, sGuidObj));
            return null;
        }

        public string SaveDiagramFromPath(string sVue, string fullpath, string sGuidObj)
        {

            int nbrO = drawArea.GraphicsList.Count, xmax = 0, ymax = 0, xmin = 0, ymin = 0;
            int iSupObj = 30;
            DrawObject o = null;
            //0629ddb4-9dab-43c6-b913-2a3f46e4f08c

            int n = drawArea.GraphicsList.FindObjet(0, sGuidObj);
            if (n > -1)
            {
                int xdelta = iSupObj, ydelta = iSupObj;

                o = (DrawObject)drawArea.GraphicsList[n];
                xmin = o.XMin(); xmax = o.XMax() - xmin;
                ymin = o.YMin(); ymax = o.YMax() - ymin;
                if (xmin > iSupObj) xmin -= iSupObj; else { xdelta = xmin; xmin = 0; }
                if (ymin > iSupObj) ymin -= iSupObj; else { ydelta = ymin; ymin = 0; }
                xmax += xdelta + iSupObj;
                ymax += ydelta + iSupObj;
            }
            else
            {
                for (int i = 0; i < nbrO; i++)
                {
                    o = drawArea.GraphicsList[i];
                    int x = o.XMax(), y = o.YMax();
                    if (x > xmax) xmax = x;
                    if (y > ymax) ymax = y;
                }
            }

            xmax += 10; ymax += 10;
            Bitmap bmp = new Bitmap(xmax, ymax);

            Graphics g = Graphics.FromImage(bmp);

            g.SetClip(new Rectangle(0, 0, xmax, ymax));
            g.TranslateTransform(-1 * xmin, -1 * ymin);

            drawArea.GraphicsList.Draw(g);

            DateTime dt = DateTime.Now;
            if (!Directory.Exists(fullpath)) Directory.CreateDirectory(fullpath);
            bmp.Save(fullpath + "\\" + sVue + "-" + dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString() + "-" + dt.GetHashCode().ToString() + ".png");
            return fullpath + "\\" + sVue + "-" + dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString() + "-" + dt.GetHashCode().ToString() + ".png";
        }

        /// <summary>
        /// Save file
        /// </summary>
        private void CommandSave()
        {
            string sPath = SaveDiagram((string)cbVue.SelectedItem, wkApp, "");
            FormMsgAndLinkOk f = new FormMsgAndLinkOk(this, "Le diagramm a été enregistré sous le nom:", sPath);
            f.init();
            //docManager.SaveDocument(DocManager.SaveType.Save);
        }

        private void CommandOptions()
        {
            FormPropriete fp = new FormPropriete(this);
            fp.CompleteField();
            fp.ShowDialog(this);
        }

        private void CommandObjectExplorer()
        {
            FormExplorObj feo = new FormExplorObj(this);
            feo.init(null);
            oCureo = null;
        }

        private void CommandApplicationsExport()
        {
            oCnxBase.Genere_ListApp();

        }

        private void CommandMiseAJourLibelles() {
            if (oCnxBase.CBRecherche("SELECT GuidObjet, TypeObjet FROM DansTypeVue"))
            {
                XmlExcel xmlExcel = new XmlExcel(this, "ListObjet");

                while (oCnxBase.Reader.Read())
                {
                    xmlExcel.CreatXmlFromReader();
                }
                oCnxBase.CBReaderClose();

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
                                IEnumerator ienumField = Node.GetEnumerator();
                                XmlNode NodeField;
                                string[] row = { null, null };
                                int i = 0;
                                while (ienumField.MoveNext())
                                {
                                    NodeField = (XmlNode)ienumField.Current;
                                    switch (NodeField.NodeType)
                                    {
                                        case XmlNodeType.Element:
                                            row[i] = NodeField.InnerText;
                                            i++;
                                            break;
                                    }
                                }
                                string s = "SELECT Nom" + row[1] + " FROM " + row[1] + " WHERE Guid" + row[1] + "= '" + row[0] + "'";
                                if (oCnxBase.CBRecherche("SELECT Nom" + row[1] + " FROM " + row[1] + " WHERE Guid" + row[1] + " = '" + row[0] + "'"))
                                {
                                    string sNom = oCnxBase.Reader.GetString(0);
                                    oCnxBase.CBReaderClose();
                                    oCnxBase.CBWrite("UPDATE DansTypeVue SET NomObjet='" + sNom + "' WHERE GuidObjet = '" + row[0] + "'");

                                }
                                else {
                                    oCnxBase.CBReaderClose();
                                    oCnxBase.CBWrite("UPDATE DansTypeVue SET NomObjet='Non Reference' WHERE GuidObjet = '" + row[0] + "'");
                                }
                            }
                            break;
                    }

                }
            }
            oCnxBase.CBReaderClose();

        }

        private void CommandObsolescenceMap()
        {
            ArrayList lstCriteres = new ArrayList();
            ArrayList lstEffectif = new ArrayList();

            lstCriteres.Add(new Critere(this, "7bd86a23-ad30-4843-8e28-1220f5ef7224", "7-Criticité"));
            lstCriteres.Add(new Critere(this, "590e5709-506e-4062-8281-69e7d765a3da", "4-Complexité"));
            lstCriteres.Add(new Critere(this, "b00b12bd-a447-47e6-92f6-e3b76ad22830", "1-Fin Support"));
            lstCriteres.Add(new Critere(this, "5f051eef-3016-4c68-a4f5-926e0ab6eb68", "9-Obsolescence"));
            lstCriteres.Add(new Critere(this, "1e5421a8-457f-429a-99bd-d76e3cfa055a", "B-Obsolescence+1"));
            lstCriteres.Add(new Critere(this, "82ddd17e-baff-47f3-95fd-89b7ca3588f1", "C-Obsolescence+2"));
            lstCriteres.Add(new Critere(this, "13b8ede4-156d-49de-84e6-9d2e6b8ce3e0", "D-Obsolescence+3"));
            lstCriteres.Add(new Critere(this, "e9b6a159-4b26-476c-99de-aa1878dc79ee", "E-Obsolescence+4"));

            //if (oCnxBase.CBRecherche("SELECT GuidApplication, NomApplication FROM Application, IndicatorLink b WHERE b.GuidObjet=GuidApplication AND b.GuidIndicator='90aaebca-b358-45b0-9c84-81cd6c66bfdc' ORDER BY b.ValIndicator DESC"))
            if (oCnxBase.CBRecherche("SELECT GuidApplication, NomApplication FROM Application ORDER BY NomApplication"))
            {
                while (oCnxBase.Reader.Read())
                {
                    Effectif oEffApp = new Effectif(this, oCnxBase.Reader.GetString(0), oCnxBase.Reader.GetString(1), lstCriteres, 0);
                    lstEffectif.Add(oEffApp);
                }
            }
            oCnxBase.CBReaderClose();
            report(lstEffectif, lstCriteres, Form1.rbTypeRecherche.Application);

            string[] aRowHead = { "Application", "Criticite", "Complexite", "support", "n+0", "n+1", "n+2", "n+3", "n+4" };
            //string[] aRowHead = { "Application", "support", "n+0", "n+1", "n+2", "n+3", "n+4" };
            HtmlFile htmlFile = new HtmlFile(this);
            XmlElement el = htmlFile.XmlGetFirstElFromName(htmlFile.root, "body");
            XmlElement elDiv = htmlFile.XmlCreatDivEl(el, "ListeApplication");
            XmlElement elTab = htmlFile.XmlCreatTableEl(elDiv, "TableApp", aRowHead);
            XmlElement elTB = htmlFile.XmlGetFirstElFromName(elTab, "tbody");
            for (int i = 0; i < lstEffectif.Count; i++)
            {
                Effectif e = (Effectif)lstEffectif[i];
                htmlFile.XmlCreatRowEl(elTB, e);
            }
            htmlFile.XmlSave("c:\\temp\\test.html");
        }
        private void CommandTadUpgating()
        {
            FormTadUpdating ftu = new FormTadUpdating(this);
            ftu.init();
        }

        private void CommandProvisionServer()
        {
            ClearApp();
            FormAppServer fps = new FormAppServer(this);
            fps.ShowDialog(this);
        }

        private void CommandIPVlan()
        {
            FormFlux fflux = new FormFlux(this);
            fflux.ShowDialog(this);
        }

        private void CommandFlux()
        {
            FormFlux fflux = new FormFlux(this);
            fflux.ShowDialog(this);
        }

        private void CommandRules()
        {
            FormFlowRule ffr = new FormFlowRule(this);
            ffr.ShowDialog(this);
        }

        private void CommandServiceLink()
        {
            FormServiceLink fs = new FormServiceLink(this);
            fs.ShowDialog(this);
        }

        private void CommandGroupService()
        {
            FormGroupService fs = new FormGroupService(this);
            fs.init();
            //fs.ShowDialog(this);
        }

        private void CommandService()
        {
            Form fs = new FormService(this);
            fs.ShowDialog(this);
        }

        private void CommandCopyField()
        {
            Form_ModifField ff = new Form_ModifField(this);
            ff.ShowDialog(this);
        }


        private void CommandImport()
        {
            FormInfrastructure fp = new FormInfrastructure(this);
            fp.ShowDialog(this);
        }

        private void CommandExport()
        {
            FormExport fe = new FormExport(this);
            fe.ShowDialog(this);
        }

        private void CommandVisu()
        {
            FormVisu fv = new FormVisu(this);
            fv.ShowDialog(this);
        }

        private void CommandImportObj()
        {
            string[] lines = File.ReadAllLines(@"c:\dat\tmp\impobj.txt");

            oCnxBase.CBWrite("delete from ImportObj");
            foreach (string line in lines)
            {
                oCnxBase.CBWrite("Insert Into ImportObj  (GuidImportObj) Value ('" + line + "')");
            }
        }

        private void CommandExportDB()
        {
            drawArea.GraphicsList.Clear();
            XmlDB xmlDB = new XmlDB(this, "Applications");
            int iCourant = 0, iFichier = 0, iMax = 10;
            ArrayList lstApp = new ArrayList();

            if (oCnxBase.CBRecherche("SELECT GuidApplication FROM Application"))
            {
                while (oCnxBase.Reader.Read())
                {
                    lstApp.Add(oCnxBase.Reader.GetString(0));
                }
            }
            oCnxBase.CBReaderClose();

            FormProgress fpg = new FormProgress(this, false);
            fpg.Show(this);
            fpg.initbar(lstApp.Count);


            for (int i = 0; i < lstApp.Count; i++, iCourant++)
            {
                fpg.stepbar((string)lstApp[i], 0);
                XmlCreatXmldb(xmlDB, (string)lstApp[i]);
                if (iCourant > iMax)
                {
                    xmlDB.docXml.Save(sPathRoot + "\\Apps" + iFichier + "-" + iCourant + ".xml");
                    xmlDB.docXml.RemoveAll();
                    iFichier++; iCourant = 0;
                    xmlDB = new XmlDB(this, "Applications");
                }
            }
            xmlDB.docXml.Save(sPathRoot + "\\Apps" + iFichier + "-" + iCourant + ".xml");
            xmlDB.docXml.RemoveAll();
            fpg.Close();
        }

        private void CommandEtatDB()
        {
            using (StreamWriter sfile = File.CreateText(sPathRoot + @"\EtatDB.csv"))
            {
                ArrayList lstTables = new ArrayList();
                if (oCnxBase.CBRecherche("Show tables from cmdb"))
                {
                    while (oCnxBase.Reader.Read())
                    {
                        lstTables.Add(oCnxBase.Reader.GetString(0));
                    }
                }
                oCnxBase.CBReaderClose();
                sfile.WriteLine("tablel;nbr_enreg");
                for (int i = 0; i < lstTables.Count; i++)
                {
                    if (oCnxBase.CBRecherche("SELECT count(*) FROM " + lstTables[i]))
                        sfile.WriteLine(lstTables[i] + ";" + oCnxBase.Reader.GetString(0));
                    oCnxBase.CBReaderClose();
                }
            }
        }

        private void CommandExportRefData()
        {

            using (StreamWriter sfile = File.CreateText(sPathRoot + @"\RefData.sql"))
            {
                //ApplicationClass
                drawArea.tools[(int)DrawArea.DrawToolType.ApplicationClass].LoadObjectSansGraph();

                //ApplicationType
                drawArea.tools[(int)DrawArea.DrawToolType.ApplicationType].LoadObjectSansGraph();

                //Arborescence
                //oCnxBase.ConfDB.AddArborescence();
                drawArea.tools[(int)DrawArea.DrawToolType.Arborescence].LoadObjectSansGraph();

                //BackupClass
                oCnxBase.ConfDB.AddBackupClass();
                drawArea.tools[(int)DrawArea.DrawToolType.BackupClass].LoadObjectSansGraph();

                //CadreRef
                drawArea.tools[(int)DrawArea.DrawToolType.CadreRef].LoadObjectSansGraph();

                //CadreRefApp
                oCnxBase.ConfDB.AddCadreRefApp();
                drawArea.tools[(int)DrawArea.DrawToolType.CadreRefApp].LoadObjectSansGraph();

                //CadreRefFonc
                drawArea.tools[(int)DrawArea.DrawToolType.CadreRefFonc].LoadObjectSansGraph();

                //DiskClass
                //oCnxBase.ConfDB.AddDiskClass();
                drawArea.tools[(int)DrawArea.DrawToolType.DiskClass].LoadObjectSansGraph();

                //TypeVue
                //oCnxBase.ConfDB.AddTypeVue();
                drawArea.tools[(int)DrawArea.DrawToolType.TypeVue].LoadObjectSansGraph();

                // Environnement
                //oCnxBase.ConfDB.AddEnvironnement();
                drawArea.tools[(int)DrawArea.DrawToolType.Environnement].LoadObjectSansGraph();

                //ExploitClass
                oCnxBase.ConfDB.AddExploitClass();
                drawArea.tools[(int)DrawArea.DrawToolType.ExploitClass].LoadObjectSansGraph();

                //Fonction
                //oCnxBase.ConfDB.AddFonction();
                drawArea.tools[(int)DrawArea.DrawToolType.Fonction].LoadObjectSansGraph();

                //FonctionService
                drawArea.tools[(int)DrawArea.DrawToolType.FonctionService].LoadObjectSansGraph();

                //GroupService
                drawArea.tools[(int)DrawArea.DrawToolType.GroupService].LoadObjectSansGraph();

                //Indicator
                //oCnxBase.ConfDB.AddIndicator();
                drawArea.tools[(int)DrawArea.DrawToolType.Indicator].LoadObjectSansGraph();

                //Location
                drawArea.tools[(int)DrawArea.DrawToolType.Location].LoadObjectSansGraph();

                //OptionDraw
                oCnxBase.ConfDB.AddOptionsDraw();
                drawArea.tools[(int)DrawArea.DrawToolType.OptionsDraw].LoadObjectSansGraph();

                //ProduitApp
                //drawArea.tools[(int)DrawArea.DrawToolType.ProduitApp].LoadObjectSansGraph();

                //Service
                drawArea.tools[(int)DrawArea.DrawToolType.Service].LoadObjectSansGraph();

                //ServiceLink
                //oCnxBase.ConfDB.AddServiceLink();
                drawArea.tools[(int)DrawArea.DrawToolType.ServiceLink].LoadObjectSansGraph();

                //Template
                oCnxBase.ConfDB.AddTemplate();
                drawArea.tools[(int)DrawArea.DrawToolType.Template].LoadObjectSansGraph();

                //StaticTable
                oCnxBase.ConfDB.AddStaticTable();
                drawArea.tools[(int)DrawArea.DrawToolType.StaticTable].LoadObjectSansGraph();

                //Statut
                drawArea.tools[(int)DrawArea.DrawToolType.Statut].LoadObjectSansGraph();

                //TechnoArea
                oCnxBase.ConfDB.AddTechnoArea();
                drawArea.tools[(int)DrawArea.DrawToolType.TechnoArea].LoadObjectSansGraph();


                //VlanClass
                oCnxBase.ConfDB.AddVlanClass();
                drawArea.tools[(int)DrawArea.DrawToolType.VlanClass].LoadObjectSansGraph();

                //VLan
                drawArea.tools[(int)DrawArea.DrawToolType.VLan].LoadObjectSansGraph();

                sfile.WriteLine("use cmdb");
                for (int i = drawArea.GraphicsList.Count - 1; i >= 0; i--)
                {
                    DrawObject o = (DrawObject)drawArea.GraphicsList[i];
                    sfile.WriteLine(oCnxBase.CreatObjectString(o) + ";");
                }
                sfile.WriteLine("Insert Into Application  (GuidApplication, NomApplication, GuidArborescence) Value ('00613319-a3c1-420e-b12b-ebcc3e7b9b9e', 'Temp', '764079ad-621b-4c57-92be-9d1530fb20cb');");
            }
        }

        private void CommandStatut()
        {
            FormInfrastructure fp = new FormInfrastructure(this);
            fp.ShowDialog(this);
        }

        private void CommandStatutApp()
        {
            FormInfrastructure fp = new FormInfrastructure(this, wkApp);
            fp.ShowDialog(this);
        }

        #endregion


        void dataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DrawObject o = drawArea.GraphicsList.GetSelectedObject(0);

            //if (odgv.CurrentCell.ColumnIndex==2) { // bouton Pls
            if (e.ColumnIndex == 2) //Bouton Pls
            {
                o.dataGrid_CellClick((DataGridView)sender, e);

                //if (odgv.CurrentCell.RowIndex == 2) // Ligne Link Applicatif
                //if (e.RowIndex == 2) // Ligne Link Applicatif
                //{
                //    FormChangeProp fcp = new FormChangeProp(this);
                //    fcp.ShowDialog(this);
                //}               
            }

            //throw new NotImplementedException();
        }


        private void dataGrid_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView odgv;

            odgv = (DataGridView)sender;

            if (odgv.CurrentCell.RowIndex == 1 && odgv.CurrentCell.ColumnIndex == 1)
            {
                if (drawArea.OSelected != null) drawArea.OSelected.ChangeName((string)odgv.CurrentCell.Value);
            }
            //throw new NotImplementedException();
        }

        

        private void bSave_Click(object sender, EventArgs e)
        {
            string sGuidOldGVue, sGuidEnvironnement = null;
            if (!wkApp.ChgLayers)
            {
                /*
                if (drawArea.GraphicsList.SelectionCount == 1)
                {
                    DrawObject o = drawArea.GraphicsList.GetSelectedObject(0);
                    o.SaveProp(dataGrid);
                    drawArea.GraphicsList.Owner.ClearPropObjet();
                }
                */
                lstSaveObj.Clear();
                drawArea.GraphicsList.UnselectAll();
                if (oCnxBase.CBRecherche("Select GuidEnvironnement From Environnement Where NomEnvironnement='" + tbEnv.Text + "'"))
                    sGuidEnvironnement = oCnxBase.Reader.GetString(0);
                oCnxBase.CBReaderClose();

                if (this.cbVue.Text.Length != 0 && this.cbApplication.Text.Length != 0 && tbTypeVue.Text != null && tbTypeVue.Text != "") // cbTypeVue.SelectedItem!=null)
                {

                    //Recherche Guid de la Table TypeVue (GuidTypeVue)
                    GuidTypeVue = new Guid(oCnxBase.FindGuidFromNom("TypeVue", tbTypeVue.Text)); // (string)cbTypeVue.SelectedItem));


                    //Création de la vue (GuidGVue) avec mémorisation de l'ancienne Vue (sGuidOldGVue)
                    sGuidOldGVue = GuidGVue.ToString();
                    GuidGVue = Guid.NewGuid();
                    //oCnxBase.CBWrite("INSERT INTO Vue (GuidVue, NomVue, GuidGVue, GuidTypeVue, GuidAppVersion) VALUES ('" + GuidGVue + "','tmp_" + cbVue.Text + "','" + GuidGVue + "','" + GuidTypeVue + "','" + wkApp.GuidAppVersion + "')");

                    int nbrO = drawArea.GraphicsList.Count;
                    DrawObject o = null;

                    // suppression des liens objets avec la vue
                    oCnxBase.DeleteObjetsLink(GuidVue);
                    //DeleteServerLink("User");
                    //oCnxBase.DeleteNCardLink(sGuidOldVue, "In", sGuidVueInf);
                    //oCnxBase.DeleteNCardLink(sGuidOldVue, "Out", sGuidVueInf);
                    for (int i = 0; i < nbrO; i++)
                    {
                        o = drawArea.GraphicsList[i];
                        //string ss = o.GetType().Name.Substring("Draw".Length);
                        //if (ss == "ServerPhy") { int d = 1; }
                        o.savetoDB();
                    }
                    if (sGuidOldGVue.Length != 0)
                    {
                        // Suppression des references de l'ancienne Vue
                        oCnxBase.DeleteVue(sGuidOldGVue);
                        oCnxBase.CBWrite("DELETE FROM DansVue Where GuidGVue = '" + sGuidOldGVue + "'");
                    }

                    //Mise à jour de la nouvelle référence
                    //string sGuidGVueTemp = Guid.NewGuid().ToString();
                    oCnxBase.CBWrite("UPDATE Vue SET GuidGVue='" + GuidGVue + "' WHERE GuidVue = '" + GuidVue + "'");
                    //oCnxBase.CBWrite("UPDATE DansVue SET GuidGVue='" + sGuidGVueTemp + "' WHERE GuidGVue = '" + GuidGVue + "'");                        
                    setCtrlEnabled(bSave, false);
                    drawArea.GraphicsList.Clear();
                    drawArea.Refresh();
                    cbGuidVue.SelectedItem = null;
                    cbVue.SelectedItem = null;
                    //string sGuidApplication = GetGuidApplication();
                    //InitCbApplication();
                    //oCnxBase.CBAddComboBox("SELECT NomVue FROM Vue Where GuidApplication='" + GuidApplication + "' ORDER BY NomVue", this.cbVue1);
                    oCnxBase.CBAddComboBox("SELECT GuidVue, NomVue FROM Vue Where GuidAppVersion='" + wkApp.GuidAppVersion + "' ORDER BY NomVue", this.cbGuidVue, this.cbVue);


                    /*drawArea.GraphicsList.Clear();
                    ChangeTreeViewObjet();
                    LoadVue(); */

                }
                else
                {
                    MessageBox.Show("Le nom de l'application ou/et le nom de la Vue n'ont pas été renseignés");
                }
            } else MessageBox.Show("Sauvegarde impossible lorsque la configuration des layers n'est pas par défaut");
        }

        public void propLoadDefault()
        {
            xRatio = 1; yRatio = 1;
            pTranslate.X = 0; pTranslate.Y = 0;
            bTemporaire = false;
        }

        public void LoadVue()
        {

            propLoadDefault();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<vue></vue>");
            XmlElement root = xmlDoc.DocumentElement;

            XmlElement elAtts = xmlDoc.CreateElement("Attributs");
            XmlSetAttFromEl(xmlDoc, elAtts, "GuidVue", "s", GuidVue.ToString());
            XmlSetAttFromEl(xmlDoc, elAtts, "GuidGVue", "s", GuidGVue.ToString());
            XmlSetAttFromEl(xmlDoc, elAtts, "NomTypeVue", "s", sTypeVue);
            root.AppendChild(elAtts);

            drawArea.Switch(false);
            //ClearVue();
            
            ChangeTreeViewObjet(xmlDoc);

            //Chargement des templates de chaque Layer liés à l'application
            ArrayList lstGuidLayer = new ArrayList();
            if (oCnxBase.CBRecherche("SELECT GuidLayer, GuidTemplate From Layer WHERE GuidAppVersion='" + GetGuidAppVersion() + "'"))
            {
                while (oCnxBase.Reader.Read()) lstGuidLayer.Add(oCnxBase.Reader.GetString(0) + ";" + oCnxBase.Reader.GetString(1));
            }
            oCnxBase.CBReaderClose();
            tbTypeVue.Text = sTypeVue;

            LoadVue(xmlDoc, lstGuidLayer);
            drawArea.Switch(true);
            drawArea.MajObjets();
        }

        public void LoadVue(string sguidvue, string sguidgvue, Point pTrans, double xratio, double yratio) // XRec/XVue
        {
            xRatio = xratio; yRatio = yratio;
            pTranslate= pTrans;
            bTemporaire = true;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<vue></vue>");
            XmlElement root = xmlDoc.DocumentElement;

            XmlElement elAtts = xmlDoc.CreateElement("Attributs");
            XmlSetAttFromEl(xmlDoc, elAtts, "GuidVue", "s", sguidvue);
            XmlSetAttFromEl(xmlDoc, elAtts, "GuidGVue", "s", sguidgvue);
            XmlSetAttFromEl(xmlDoc, elAtts, "NomTypeVue", "s", sTypeVue);
            root.AppendChild(elAtts);

            ArrayList lstGuidLayer = new ArrayList();

            LoadVue(xmlDoc, lstGuidLayer);
            drawArea.GraphicsList.PutObjetDefinif();
            drawArea.MajObjets();
            propLoadDefault();
        }

        public void SaveTest(String sGuidVue, LstApplications lstVues)
        {

            
            using (var webClient = new System.Net.WebClient())
            {
                //webClient.Headers.Add("Content-Type", "application/json; charset=utf-8");
                webClient.Headers.Add("Content-Type", "application/json");
                webClient.Encoding = System.Text.UTF8Encoding.UTF8;
                String listvue = JsonConvert.SerializeObject(AppData);
                byte[] data = System.Text.Encoding.UTF8.GetBytes(listvue); 
                webClient.UploadDataCompleted += webClient_PutVue;
                Uri urlToRequest = new Uri(@"http://localhost:8080/Vues/" + sGuidVue + "/Update");
                webClient.UploadDataAsync(urlToRequest, "put", data);
            }
        }


        public void LoadVue(XmlDocument xmlDoc, ArrayList lstGuidLayer)
        {
            XmlElement root = xmlDoc.DocumentElement;

            string sGuidVue = XmlGetAttValueAFromAttValueB(root, "Value","Nom", "GuidVue");
            string sGuidGVue = XmlGetAttValueAFromAttValueB(root, "Value", "Nom", "GuidGVue");
            string sTypeVue = XmlGetAttValueAFromAttValueB(root, "Value", "Nom", "NomTypeVue");

            switch (sTypeVue[0])
            {
                case '0': //0-Fonctionnelle

#if APIREADY
                    using (var webClient = new System.Net.WebClient())
                    {
                        //webClient.Headers.Add("Content-Type", "application/json; charset=utf-8");
                        webClient.Headers.Add("Content-Type", "application/json");
                        webClient.Encoding = System.Text.UTF8Encoding.UTF8;
                        webClient.DownloadDataCompleted += webClient_GetVue;
                        Uri urlToRequest = new Uri(@"http://localhost:8080/Vues/" + sGuidVue + "/Diagramm");
                        webClient.DownloadDataAsync(urlToRequest);
                    }
#else

                    //Vérification de l'existance de Modules dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.Module].LoadTemplate(lstGuidLayer);
                    drawArea.tools[(int)DrawArea.DrawToolType.Module].LoadObject(' ', sGuidGVue, null);
                    
                    //Vérification de l'existance de Users dans la Vue
                    drawArea.tools[(int)DrawArea.DrawToolType.AppUser].LoadTemplate(lstGuidLayer);
                    drawArea.tools[(int)DrawArea.DrawToolType.AppUser].LoadObject(' ', sGuidGVue, null);

                    //Vérification de l'existance d'Applications Externes dans la Vue
                    drawArea.tools[(int)DrawArea.DrawToolType.Application].LoadTemplate(lstGuidLayer);
                    drawArea.tools[(int)DrawArea.DrawToolType.Application].LoadObject(' ', sGuidGVue, null);

                    //Vérification de l'existance de Link dans la Vue
                    drawArea.tools[(int)DrawArea.DrawToolType.Link].LoadObject(' ', sGuidGVue, null);

#endif
                    break;
                case '1': // 1-Applicative
#if APIREADY
                    using (var webClient = new System.Net.WebClient())
                    {
                        //webClient.Headers.Add("Content-Type", "application/json; charset=utf-8");
                        webClient.Headers.Add("Content-Type", "application/json");
                        webClient.Encoding = System.Text.UTF8Encoding.UTF8;
                        webClient.DownloadDataCompleted += webClient_GetVue;
                        Uri urlToRequest = new Uri(@"http://localhost:8080/Vues/" + sGuidVue + "/Diagramm");
                        webClient.DownloadDataAsync(urlToRequest);
                    }

#else
                    //Vérification de l'existance de Application dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.Application].LoadTemplate(lstGuidLayer);
                    drawArea.tools[(int)DrawArea.DrawToolType.Application].LoadObject(' ', sGuidGVue, null);

                    //Vérification de l'existance de MainComposant dans la vue
                    //oCnxBase.LoadMainComposant();
                    drawArea.tools[(int)DrawArea.DrawToolType.MainComposant].LoadTemplate(lstGuidLayer);
                    drawArea.tools[(int)DrawArea.DrawToolType.MainComposant].LoadObject(' ', sGuidGVue, null);
                    //oCnxBase.LoadObject(drawArea.tools[(int)DrawArea.DrawToolType.MainComposant]);

                    //Vérification de l'existance de User dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.AppUser].LoadTemplate(lstGuidLayer);
                    drawArea.tools[(int)DrawArea.DrawToolType.AppUser].LoadObject(' ', sGuidGVue, null);

                    //Vérification de l'existance de Fonction (CompFonc) dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.CompFonc].LoadTemplate(lstGuidLayer);
                    drawArea.tools[(int)DrawArea.DrawToolType.CompFonc].LoadObject(' ', sGuidGVue, null);

                    //Vérification de l'existance de Composant dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.Composant].LoadTemplate(lstGuidLayer);
                    drawArea.tools[(int)DrawArea.DrawToolType.Composant].LoadObject(' ', sGuidGVue, null);

                    //Vérification de l'existance de Interface dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.Interface].LoadTemplate(lstGuidLayer);
                    drawArea.tools[(int)DrawArea.DrawToolType.Interface].LoadObject(' ', sGuidGVue, null);

                    //Vérification de l'existance de Base dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.Base].LoadTemplate(lstGuidLayer);
                    drawArea.tools[(int)DrawArea.DrawToolType.Base].LoadObject(' ', sGuidGVue, null);

                    //Vérification de l'existance de Base dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.File].LoadTemplate(lstGuidLayer);
                    drawArea.tools[(int)DrawArea.DrawToolType.File].LoadObject(' ', sGuidGVue, null);

                    //Vérification de l'existance de Base dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.Queue].LoadTemplate(lstGuidLayer);
                    drawArea.tools[(int)DrawArea.DrawToolType.Queue].LoadObject(' ', sGuidGVue, null);

                    //Vérification de l'existance de Link dans la Vue
                    drawArea.tools[(int)DrawArea.DrawToolType.LinkA].LoadObject(' ', sGuidGVue, null);
#endif

                    break;
                case '2': // 2-Infrastructure
#if APIREADY
                    using (var webClient = new System.Net.WebClient())
                    {
                        //webClient.Headers.Add("Content-Type", "application/json; charset=utf-8");
                        webClient.Headers.Add("Content-Type", "application/json");
                        webClient.Encoding = System.Text.UTF8Encoding.UTF8;
                        webClient.DownloadDataCompleted += webClient_GetVue;
                        Uri urlToRequest = new Uri(@"http://localhost:3001/vues/" + sGuidVue + "/Diagramm");
                        webClient.DownloadDataAsync(urlToRequest);
                    }
#else

                    //Vérification de l'existance de User dans la vue
                    //drawArea.tools[(int)DrawArea.DrawToolType.User].LoadObject(null);
                    //drawArea.tools[(int)DrawArea.DrawToolType.TechUser].LoadTemplate(lstGuidLayer);
                    drawArea.tools[(int)DrawArea.DrawToolType.TechUser].LoadObject(' ', sGuidGVue, null);
                    oCnxBase.LoadSousObjets((int)DrawArea.DrawToolType.TechUser);

                    //Vérification de l'existance de Application dans la vue
                    //drawArea.tools[(int)DrawArea.DrawToolType.Application].LoadTemplate(lstGuidLayer);
                    drawArea.tools[(int)DrawArea.DrawToolType.Application].LoadObject(' ', sGuidGVue, null);

                    //Vérification de l'existance de Server dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.Server].LoadObject(' ', sGuidGVue, null);
                    oCnxBase.LoadSousObjets((int)DrawArea.DrawToolType.Server);

                    //Vérification de l'existance de Composant dans un server (MainComposant) dans la vue
                    //drawArea.tools[(int)DrawArea.DrawToolType.MCompApp].LoadObject(' ', null);

                    //Vérification de l'existance de Package applicatif dans un server dans la vue
                    //drawArea.tools[(int)DrawArea.DrawToolType.ServMComp].LoadObject(' ', null);

                    //Vérification de l'existance de ServerType dans un server dans la vue
                    //drawArea.tools[(int)DrawArea.DrawToolType.ServerType].LoadObject(null);

                    //Vérification de l'existance de Techno dans un serverType (Techno) dans la vue
                    //drawArea.tools[(int)DrawArea.DrawToolType.Techno].LoadObject(null);

                    //Vérification de l'existance de Genks dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.Genks].LoadObject(' ', sGuidGVue, null);

                    //Vérification de l'existance de Genks dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.Genpod].LoadObject(' ', sGuidGVue, null);
                    oCnxBase.LoadSousObjets((int)DrawArea.DrawToolType.Genpod);

                    //Vérification de l'existance de Genks dans la vue
                    //drawArea.tools[(int)DrawArea.DrawToolType.Container].LoadObject(' ', null);

                    //Vérification de l'existance de Genks dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.Gensvc].LoadObject(' ', sGuidGVue, null);

                    //Vérification de l'existance de Genks dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.Gening].LoadObject(' ', sGuidGVue, null);

                    //Vérification de l'existance de Genks dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.Gensas].LoadObject(' ', sGuidGVue, null);
                    oCnxBase.LoadSousObjets((int)DrawArea.DrawToolType.Gensas);

                    //Charger les ServerTypes gérés par les PlateformPatterns
                    GetServerTypeFormPlateformPattern();

                    //Charger les labels liés aux objects
                    GetLabelLinks();

                    //Vérification de l'existance de Link dans la Vue
                    drawArea.tools[(int)DrawArea.DrawToolType.TechLink].LoadObject(' ', sGuidGVue, null);
                    oCnxBase.LoadTechLinkApp();

                    oCnxBase.LoadExtention();

#endif
                    break;
                case '6': // 6-Sites
                    drawArea.tools[(int)DrawArea.DrawToolType.Cnx].LoadObject(' ', sGuidGVue, null);
                    oCnxBase.LoadCnxPoint();

                    //Vérification de l'existance de Location dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.Location].LoadObject(' ', sGuidGVue, null);

                    //Vérification de l'existance de SiteSite (ServerPhy) dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.ServerSite].LoadObject(' ', sGuidGVue, null);
                    GetServerLinks();

                    //Vérification de l'existance de SiteSite (ServerPhy) dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.InterLink].LoadObject(' ', sGuidGVue, null);
                    GetInterLinks();

                    //Vérification de l'existance de PointDeConnexion (ServerPhy) dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.PtCnx].LoadObject(' ', sGuidGVue, null);
                    //oCnxBase.LoadCnxPoint();

                    break;
                case '3': // 3-Production
                case '5': // 5-Pre-Production
                case '4': // 4-Hors Production
                case 'F': // F-Service Infra
                    drawArea.tools[(int)DrawArea.DrawToolType.Cluster].LoadObject(' ', sGuidGVue, null);
#if CLUSTERREADY
                    oCnxBase.LoadSousObjets((int)DrawArea.DrawToolType.Cluster);

                    drawArea.tools[(int)DrawArea.DrawToolType.ServerPhy].LoadObject(' ', sGuidGVue, null);
                    GetServerLinks();
                    oCnxBase.LoadSousObjets((int)DrawArea.DrawToolType.ServerPhy);

                    drawArea.tools[(int)DrawArea.DrawToolType.Insks].LoadObject(' ', sGuidGVue, null);
                    oCnxBase.LoadSousObjets((int)DrawArea.DrawToolType.Insks);

                    drawArea.tools[(int)DrawArea.DrawToolType.Inssas].LoadObject(' ', sGuidGVue, null);

                    drawArea.tools[(int)DrawArea.DrawToolType.VLan].LoadObject(' ', sGuidGVue, null);
                    oCnxBase.LoadVLanPoint();

                    drawArea.tools[(int)DrawArea.DrawToolType.NCard].LoadObject(' ', sGuidGVue, null);

                    oCnxBase.LinkNCardWithVLan();

                    drawArea.tools[(int)DrawArea.DrawToolType.Router].LoadObject(' ', sGuidGVue, null);
                    oCnxBase.LoadRouterLink();

                    //Charger les labels liés aux objects
                    GetLabelLinks();

                    oCnxBase.LoadAlias();
                    oCnxBase.LoadNCardLinkIn();
                    oCnxBase.LoadNCardLink("Out");

                    oCnxBase.LoadExtention();
#else
                    drawArea.tools[(int)DrawArea.DrawToolType.ServerPhy].LoadObject(' ', null);
                    GetServerLinks();

                    drawArea.tools[(int)DrawArea.DrawToolType.VLan].LoadObject(' ', null);
                    oCnxBase.LoadVLanPoint();

                    drawArea.tools[(int)DrawArea.DrawToolType.Router].LoadObject(' ', null);
                    oCnxBase.LoadRouterLink();

                    drawArea.tools[(int)DrawArea.DrawToolType.NCard].LoadObject(' ', null);
                    oCnxBase.LoadAlias();
                    oCnxBase.LoadNCardLinkIn();
                    oCnxBase.LoadNCardLink("Out");
                    
#endif
                    break;
                case '8': // 8-ZoningProd
                case '7': // 7-ZoningHorsProd
                    drawArea.tools[(int)DrawArea.DrawToolType.Cluster].LoadObject(' ', sGuidGVue, null);

                    drawArea.tools[(int)DrawArea.DrawToolType.Baie].LoadObject(' ', sGuidGVue, null);

                    drawArea.tools[(int)DrawArea.DrawToolType.Machine].LoadObject(' ', sGuidGVue, null);

                    drawArea.tools[(int)DrawArea.DrawToolType.Virtuel].LoadObject(' ', sGuidGVue, null);

                    drawArea.tools[(int)DrawArea.DrawToolType.Lun].LoadObject(' ', sGuidGVue, null);

                    drawArea.tools[(int)DrawArea.DrawToolType.Zone].LoadObject(' ', sGuidGVue, null);
                    //oCnxBase.LoadZonePoint();

                    break;
                case 'A': // A-SanProd
                case '9': // 9-SanHorsProd
                    drawArea.tools[(int)DrawArea.DrawToolType.ServerPhy].LoadObject(' ', sGuidGVue, null);

                    drawArea.tools[(int)DrawArea.DrawToolType.BaieCTI].LoadObject(' ', sGuidGVue, null);

                    drawArea.tools[(int)DrawArea.DrawToolType.ISL].LoadObject(' ', sGuidGVue, null);

                    drawArea.tools[(int)DrawArea.DrawToolType.SanSwitch].LoadObject(' ', sGuidGVue, null);
                    oCnxBase.LoadSanSwitchPoint();

                    drawArea.tools[(int)DrawArea.DrawToolType.SanCard].LoadObject(' ', sGuidGVue, null);
                    GetSanCardLinks();

                    break;
                case 'C': // C-CTIProd
                case 'B': // B-CTIHorsProd
                    drawArea.tools[(int)DrawArea.DrawToolType.BaieDPhy].LoadObject(' ', sGuidGVue, null);

                    drawArea.tools[(int)DrawArea.DrawToolType.BaiePhy].LoadObject(' ', sGuidGVue, null);

                    drawArea.tools[(int)DrawArea.DrawToolType.Drawer].LoadObject(' ', sGuidGVue, null);

                    drawArea.tools[(int)DrawArea.DrawToolType.MachineCTI].LoadObject(' ', sGuidGVue, null);

                    break;
                case 'D': // D-InfProd
                    break;
                case 'U':
                    drawArea.tools[(int)DrawArea.DrawToolType.Module].LoadObject(' ', sGuidGVue, null);
                    drawArea.tools[(int)DrawArea.DrawToolType.AppUser].LoadObject(' ', sGuidGVue, null);
                    drawArea.tools[(int)DrawArea.DrawToolType.Application].LoadObject(' ', sGuidGVue, null);
                    //drawArea.tools[(int)DrawArea.DrawToolType.Link].LoadObject(' ', null);


                    //Vérification de l'existance de MainComposant dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.MainComposant].LoadObject(' ', sGuidGVue, null);
                    drawArea.tools[(int)DrawArea.DrawToolType.Composant].LoadObject(' ', sGuidGVue, null);
                    drawArea.tools[(int)DrawArea.DrawToolType.Interface].LoadObject(' ', sGuidGVue, null);
                    drawArea.tools[(int)DrawArea.DrawToolType.Base].LoadObject(' ', sGuidGVue, null);
                    drawArea.tools[(int)DrawArea.DrawToolType.File].LoadObject(' ', sGuidGVue, null);
                    drawArea.tools[(int)DrawArea.DrawToolType.LinkA].LoadObject(' ', sGuidGVue, null);

                    //drawArea.tools[(int)DrawArea.DrawToolType.User].LoadObject(null);
                    drawArea.tools[(int)DrawArea.DrawToolType.TechUser].LoadObject(' ', sGuidGVue, null);
                    oCnxBase.LoadSousObjets((int)DrawArea.DrawToolType.TechUser);
                    drawArea.tools[(int)DrawArea.DrawToolType.Server].LoadObject(' ', sGuidGVue, null);
                    oCnxBase.LoadSousObjets((int)DrawArea.DrawToolType.Server);
                    drawArea.tools[(int)DrawArea.DrawToolType.TechLink].LoadObject(' ', sGuidGVue, null);
                    oCnxBase.LoadTechLinkApp();

                    //Vérification de l'existance de Location dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.Cnx].LoadObject(' ', sGuidGVue, null);
                    oCnxBase.LoadCnxPoint();
                    drawArea.tools[(int)DrawArea.DrawToolType.Location].LoadObject(' ', sGuidGVue, null);
                    drawArea.tools[(int)DrawArea.DrawToolType.ServerSite].LoadObject(' ', sGuidGVue, null);
                    GetServerLinks();
                    drawArea.tools[(int)DrawArea.DrawToolType.InterLink].LoadObject(' ', sGuidGVue, null);
                    GetInterLinks();
                    drawArea.tools[(int)DrawArea.DrawToolType.PtCnx].LoadObject(' ', sGuidGVue, null);

                    //Vérification de l'existance de PointDeConnexion (ServerPhy) dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.Cluster].LoadObject(' ', sGuidGVue, null);
                    drawArea.tools[(int)DrawArea.DrawToolType.ServerPhy].LoadObject(' ', sGuidGVue, null);
                    GetServerLinks();
                    drawArea.tools[(int)DrawArea.DrawToolType.VLan].LoadObject(' ', sGuidGVue, null);
                    oCnxBase.LoadVLanPoint();
                    drawArea.tools[(int)DrawArea.DrawToolType.Router].LoadObject(' ', sGuidGVue, null);
                    oCnxBase.LoadRouterLink();
                    drawArea.tools[(int)DrawArea.DrawToolType.NCard].LoadObject(' ', sGuidGVue, null);
                    oCnxBase.LoadNCardLink("In");
                    oCnxBase.LoadNCardLink("Out");

                    //Vérification de l'existance de PointDeConnexion (ServerPhy) dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.Baie].LoadObject(' ', sGuidGVue, null);
                    drawArea.tools[(int)DrawArea.DrawToolType.Machine].LoadObject(' ', sGuidGVue, null);
                    drawArea.tools[(int)DrawArea.DrawToolType.Virtuel].LoadObject(' ', sGuidGVue, null);
                    drawArea.tools[(int)DrawArea.DrawToolType.Lun].LoadObject(' ', sGuidGVue, null);
                    drawArea.tools[(int)DrawArea.DrawToolType.Zone].LoadObject(' ', sGuidGVue, null);

                    //Vérification de l'existance de PointDeConnexion (ServerPhy) dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.BaieCTI].LoadObject(' ', sGuidGVue, null);
                    drawArea.tools[(int)DrawArea.DrawToolType.ISL].LoadObject(' ', sGuidGVue, null);
                    drawArea.tools[(int)DrawArea.DrawToolType.SanSwitch].LoadObject(' ', sGuidGVue, null);
                    oCnxBase.LoadSanSwitchPoint();
                    drawArea.tools[(int)DrawArea.DrawToolType.SanCard].LoadObject(' ', sGuidGVue, null);
                    GetSanCardLinks();

                    //Vérification de l'existance de PointDeConnexion (ServerPhy) dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.BaieDPhy].LoadObject(' ', sGuidGVue, null);
                    drawArea.tools[(int)DrawArea.DrawToolType.BaiePhy].LoadObject(' ', sGuidGVue, null);
                    drawArea.tools[(int)DrawArea.DrawToolType.Drawer].LoadObject(' ', sGuidGVue, null);
                    drawArea.tools[(int)DrawArea.DrawToolType.MachineCTI].LoadObject(' ', sGuidGVue, null);

                    break;
                case 'V': // V-Si App
                    //Vérification de l'existance de Application dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.Application].LoadObject(' ', sGuidGVue, null);

                    //Vérification de l'existance de Link dans la Vue
                    drawArea.tools[(int)DrawArea.DrawToolType.LinkA].LoadObject(' ', sGuidGVue, null);
                    break;
                case 'W': // W-Si Inf
                    //Vérification de l'existance de Application dans la vue
                    drawArea.tools[(int)DrawArea.DrawToolType.Application].LoadObject(' ', sGuidGVue, null);

                    //Vérification de l'existance de Link dans la Vue
                    drawArea.tools[(int)DrawArea.DrawToolType.TechLink].LoadObject(' ', sGuidGVue, null);
                    break;
                case 'Y': // Y-Cadre Ref
                    drawArea.tools[(int)DrawArea.DrawToolType.CadreRefN].LoadObject(' ', sGuidGVue, null);

                    drawArea.tools[(int)DrawArea.DrawToolType.CadreRefEnd].LoadObject(' ', sGuidGVue, null);
                    break;
            }
            //Recfresh drawArea
            drawArea.Capture = true;
            drawArea.GraphicsList.UnselectAll();
        }

        public void CreatGridObjet()
        {
            DrawGrid dg = new DrawGrid(drawArea.Owner);
            drawArea.GraphicsList.Add(dg);
        }

        public void Xml3dCreatServer(XmlFile xmlFlux, XmlFile xmlF, XmlElement elXmlSource, string NomServer, string NomServerLinked, string sIdFlux, string sNomFlux)
        {
            XmlElement elClusters = xmlFlux.XmlGetFirstElFromName(xmlFlux.docXml.DocumentElement, "Clusters");
            XmlElement elServer = xmlF.XmlCreatEl(xmlF.root, "Server");
            elServer.SetAttribute("Nom", NomServer);

            IEnumerator ienum = elXmlSource.GetEnumerator();
            XmlNode Node;

            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    if (((XmlElement)Node).GetAttribute("Selected") == "Yes")
                    {
                        if (((XmlElement)Node).GetAttribute("Cluster") == "Yes")
                        {
                            XmlElement elCluster = xmlFlux.XmlFindElFromAtt(elClusters, "Guid", ((XmlElement)Node).GetAttribute("Guid"));

                            IEnumerator ienumSrv = elCluster.GetEnumerator();
                            XmlNode NodeSrv;
                            while (ienumSrv.MoveNext())
                            {
                                NodeSrv = (XmlNode)ienumSrv.Current;
                                if (NodeSrv.NodeType == XmlNodeType.Element)
                                {
                                    XmlElement elCur = (XmlElement)NodeSrv;
                                    if (elCur.Name == "Server")
                                    {
                                        XmlElement elSrvPhy = xmlF.XmlCreatEl(elServer, "ServerPhy");
                                        elSrvPhy.SetAttribute("Guid", ((XmlElement)NodeSrv).GetAttribute("Guid"));
                                        elSrvPhy.SetAttribute("Nom", ((XmlElement)NodeSrv).GetAttribute("Nom"));
                                    }
                                }
                            }
                        }
                        else {
                            XmlElement elSrvPhy = xmlF.XmlCreatEl(elServer, "ServerPhy");
                            elSrvPhy.SetAttribute("Guid", ((XmlElement)Node).GetAttribute("Guid"));
                            elSrvPhy.SetAttribute("Nom", ((XmlElement)Node).GetAttribute("Nom"));
                        }
                    }
                }
            }
            XmlElement elLink = xmlF.XmlCreatEl(elServer, "Link");
            elLink.SetAttribute("Nom", NomServerLinked);
            elLink.SetAttribute("IdFlux", sIdFlux);
            elLink.SetAttribute("NomFlux", sNomFlux);
        }
        public XmlExcel Xml3dCreatLink(XmlExcel xmlFlux)
        {
            XmlExcel xmlLink = new XmlExcel(this, "Links");
            
            IEnumerator ienum = xmlFlux.root.GetEnumerator();
            XmlNode Node;

            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element && Node.Name == "Flux")
                {
                    
                    if (((XmlElement)Node).GetAttribute("Selected") == "Yes")
                    {
                        string sIdFlux = ((XmlElement)Node).GetAttribute("Id");
                        string sNomFlux = ((XmlElement)Node).GetAttribute("Nom");

                        XmlElement elOrigine = xmlFlux.XmlGetFirstElFromName((XmlElement)Node, "Origine");
                        XmlElement elCible = xmlFlux.XmlGetFirstElFromName((XmlElement)Node, "Cible");
                        if (elOrigine != null && elCible != null)
                        {
                            string sFirstNomServerOrigine = xmlFlux.XmlGetAttValueAFromAttValueB(elOrigine, "Nom", "Selected", "Yes", 0);
                            string sFirstNomServerCible = xmlFlux.XmlGetAttValueAFromAttValueB(elCible, "Nom", "Selected", "Yes", 0);

                            if (sFirstNomServerOrigine != "" && sFirstNomServerCible != "")
                            {
                                XmlElement elServer = xmlLink.XmlFindElFromAtt(xmlLink.root, "Nom", elOrigine.GetAttribute("NomObjInfra") + "_" + sFirstNomServerOrigine, 0);
                                if (elServer == null)
                                    Xml3dCreatServer(xmlFlux, xmlLink, elOrigine, elOrigine.GetAttribute("NomObjInfra") + "_" + sFirstNomServerOrigine, elCible.GetAttribute("NomObjInfra") + "_" + sFirstNomServerCible, sIdFlux, sNomFlux);
                                else
                                {
                                    XmlElement elLink = xmlLink.XmlFindElFromAtt(elServer, "Nom", elCible.GetAttribute("NomObjInfra") + "_" + sFirstNomServerCible);
                                    if (elLink == null)
                                    {
                                        elLink = xmlLink.XmlCreatEl(elServer, "Link");
                                        elLink.SetAttribute("Nom", elCible.GetAttribute("NomObjInfra") + "_" + sFirstNomServerCible);
                                        elLink.SetAttribute("IdFlux", sIdFlux);
                                        elLink.SetAttribute("NomFlux", sNomFlux);
                                    }

                                }
                                elServer = xmlLink.XmlFindElFromAtt(xmlLink.root, "Nom", elCible.GetAttribute("NomObjInfra") + "_" + sFirstNomServerCible, 0);
                                if (elServer == null)
                                    Xml3dCreatServer(xmlFlux, xmlLink, elCible, elCible.GetAttribute("NomObjInfra") + "_" + sFirstNomServerCible, elOrigine.GetAttribute("NomObjInfra") + "_" + sFirstNomServerOrigine, sIdFlux, sNomFlux);
                                else
                                {
                                    XmlElement elLink = xmlLink.XmlFindElFromAtt(elServer, "Nom", elOrigine.GetAttribute("NomObjInfra") + "_" + sFirstNomServerOrigine);
                                    if (elLink == null)
                                    {
                                        elLink = xmlLink.XmlCreatEl(elServer, "Link");
                                        elLink.SetAttribute("Nom", elOrigine.GetAttribute("NomObjInfra") + "_" + sFirstNomServerOrigine);
                                        elLink.SetAttribute("IdFlux", sIdFlux);
                                        elLink.SetAttribute("NomFlux", sNomFlux);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            xmlLink.docXml.Save("\\dat\\FluxTest1.xml");
            //Supprimer les links des Clusters
            ienum = xmlLink.root.GetEnumerator();
            
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element && Node.Name == "Server" && ((XmlElement)Node).GetAttribute("Cluster") == "Yes")
                {
                    XmlElement elCluster = (XmlElement)Node;
                    string sNom = elCluster.GetAttribute("Nom");
                    string sNomApp = sNom.Substring(0, sNom.LastIndexOf('_'));
                    ArrayList lstServerPhy = Xml3dGetLstServerPhyFromAtt(elCluster, "Nom");
                    XmlElement el = null;
                    for (int i = 0; i < lstServerPhy.Count; i++)
                    {
                        el = xmlLink.XmlFindElFromContaintAtt(xmlLink.root, "Nom", "_" + lstServerPhy[i], 0);
                        if (el != null)
                        {
                            IEnumerator ienumLink = elCluster.GetEnumerator();

                            ArrayList lstElLink = xmlLink.XmlGetLstElFromName(elCluster, "Link");
                            for (int j = 0; j < lstElLink.Count; j++)
                            {
                                XmlElement elLink;
                                elLink = (XmlElement) lstElLink[j];
                                {
                                    if (xmlLink.XmlFindElFromAtt(el, "Nom", elLink.GetAttribute("Nom")) == null)
                                    {
                                        //transfert de l'élément
                                        el.AppendChild(elLink);
                                        // Maj à jour du partenaire
                                        XmlElement elPart = xmlLink.XmlFindElFromAtt(xmlLink.root, "Nom", elLink.GetAttribute("Nom"), 0);
                                        XmlElement elPLink = xmlLink.XmlFindElFromAtt(elPart, "Nom", elCluster.GetAttribute("Nom"), 0);
                                        //elPLink.SetAttribute("Nom", sNomApp + "_" + lstServerPhy[i]);
                                        elPLink.SetAttribute("Nom", el.GetAttribute("Nom"));
                                    } else
                                    {
                                        elCluster.RemoveChild(elLink);
                                        // Maj à jour du partenaire
                                        XmlElement elPart = xmlLink.XmlFindElFromAtt(xmlLink.root, "Nom", elLink.GetAttribute("Nom"), 0);
                                        XmlElement elPLink = xmlLink.XmlFindElFromAtt(elPart, "Nom", elCluster.GetAttribute("Nom"), 0);
                                        elPart.RemoveChild(elPLink);
                                    }
                                }
                            }

                        }
                        break;
                    }
                }
            }
            xmlLink.docXml.Save("\\dat\\FluxTest2.xml");

            return xmlLink;
        }

        public ArrayList Xml3dGetLstServerPhyFromAtt(XmlElement el, string sAtt)
        {
            ArrayList lstServerPhyGuids = new ArrayList();

            IEnumerator ienum = el.GetEnumerator();
            XmlNode Node;

            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element && Node.Name == "ServerPhy")
                {
                    lstServerPhyGuids.Add(((XmlElement)Node).GetAttribute(sAtt));
                }
            }

            return lstServerPhyGuids;
        }

        public int MakeSequenceFluxOneFonc(XmlElement elFluxFonc, XmlExcel xmlFluxFoncApp, XmlExcel xmlFluxAppTech, XmlExcel xmlFluxTech, int yRelatif)
        {
            List<xmlFlux> lstxmlFlux = new List<xmlFlux>();
            List<String[]> lstServer = new List<string[]>();
            ArrayList lstFluxTech = null;
            int yHeight = 0;

            xmlFlux oFlux = new xmlFlux
            {
                elFlux = elFluxFonc,
                lstElChild = new List<xmlFlux>()
            };
            lstxmlFlux.Add(oFlux);

            ArrayList lstFluxApp = xmlFluxFoncApp.XmlGetLstElFromName(lstxmlFlux[0].elFlux, "FluxApp");
            for (int j = 0; j < lstFluxApp.Count; j++)
            {
                XmlElement nodeFluxApp = (XmlElement)lstFluxApp[j];
                oFlux = new xmlFlux
                {
                    elFlux = xmlFluxAppTech.XmlFindElFromAtt(xmlFluxAppTech.root, "Guid", nodeFluxApp.GetAttribute("Guid")),
                    lstElChild = new List<xmlFlux>()
                };
                lstxmlFlux[0].lstElChild.Add(oFlux);

                lstFluxTech = xmlFluxAppTech.XmlGetLstElFromName(lstxmlFlux[0].lstElChild[j].elFlux, "FluxTech");
                for (int k = 0; k < lstFluxTech.Count; k++)
                {
                    XmlElement nodeFluxTech = (XmlElement)lstFluxTech[k];
                    oFlux = new xmlFlux
                    {
                        elFlux = xmlFluxTech.XmlFindElFromAtt(xmlFluxTech.root, "Guid", nodeFluxTech.GetAttribute("Guid")),
                        lstElChild = new List<xmlFlux>()
                    };

                    if (oFlux.elFlux.GetAttribute("Selected") == "Yes")
                    {
                        XmlElement nodeOrigine = xmlFluxTech.XmlGetFirstElFromName(oFlux.elFlux, "Origine");
                        XmlElement nodeCible = xmlFluxTech.XmlGetFirstElFromName(oFlux.elFlux, "Cible");
                        if (nodeOrigine.GetAttribute("Selected") == "Yes" && nodeCible.GetAttribute("Selected") == "Yes")
                        {
                            string[] elS = lstServer.Find(el => el[0] == nodeOrigine.GetAttribute("Guid"));
                            if (elS == null)
                            {
                                string[] aServer = new string[2];
                                aServer[0] = nodeOrigine.GetAttribute("Guid");
                                aServer[1] = nodeOrigine.GetAttribute("NomObjInfra");
                                lstServer.Add(aServer);
                            }
                            elS = lstServer.Find(el => el[0] == nodeCible.GetAttribute("Guid"));
                            if (elS == null)
                            {
                                string[] aServer = new string[2];
                                aServer[0] = nodeCible.GetAttribute("Guid");
                                aServer[1] = nodeCible.GetAttribute("NomObjInfra");
                                lstServer.Add(aServer);
                            }

                            lstxmlFlux[0].lstElChild[j].lstElChild.Add(oFlux);
                            yHeight += DrawObject.HeightFlux;
                        }
                    }
                }
            }
            if (yHeight > 0)
            {
                DrawSeqFluxFonc dsff = new DrawSeqFluxFonc(drawArea.Owner, lstxmlFlux, yRelatif, xmlFluxTech, lstServer);
                drawArea.GraphicsList.Add(dsff);
            }
            return yHeight;
        }
        

        public ArrayList MakeSequenceFluxFonc(string sGuidVueDeploy, string sGuidVueInf, ControlDoc cw)
        {
            ArrayList lstImg = new ArrayList();
            XmlExcel xmlFluxFoncApp = null, xmlFluxAppTech = null, xmlFluxTech = null;
            string sGuidVueFonc = null, sGuidVueApp = null;
            ArrayList lstFluxFonc = null, lstFluxTech = null;

            if (oCnxBase.CBRecherche("select vapp.GuidVueInf, vapp.GuidVue from Vue vapp, Vue vinf where vinf.GuidVueInf=vapp.GuidVue and vinf.GuidVue='" + sGuidVueInf + "'"))
            {
                if(!oCnxBase.Reader.IsDBNull(0)) sGuidVueFonc = oCnxBase.Reader.GetString(0);
                if (!oCnxBase.Reader.IsDBNull(1)) sGuidVueApp = oCnxBase.Reader.GetString(1);
            }
            oCnxBase.CBReaderClose();

            if (sGuidVueFonc != null)
            {
                xmlFluxFoncApp = xmlCreatFluxFonc(sGuidVueFonc, sGuidVueApp);
                lstFluxFonc = xmlFluxFoncApp.XmlGetLstElFromName(xmlFluxFoncApp.root, "FluxFonc");
                if(lstFluxFonc.Count == 0)
                {
                    if(cw == null) MessageBox.Show("Aucun flux fonctionnel est rattaché aux flux applicatifs");
                    return lstImg;
                } 
                xmlFluxAppTech = xmlCreatFluxApp(sGuidVueApp, sGuidVueInf);
                lstFluxTech = xmlFluxAppTech.XmlGetLstElFromName(xmlFluxAppTech.root, "FluxTech");
                if (lstFluxTech.Count == 0)
                {
                    if (cw == null) MessageBox.Show("Aucun flux Applicatif est rattaché aux flux techniquess");
                    return lstImg;
                }
                xmlFluxTech = XmlCreatFlux(sGuidVueInf, sGuidVueDeploy);
            }
            else return lstImg;
            //xmlFluxFoncApp.docXml.Save("c:\\dat\\tmp\\xmlfoncapp.xml");
            //xmlFluxAppTech.docXml.Save("c:\\dat\\tmp\\xmlapptech.xml");
            //xmlFluxTech.docXml.Save("c:\\dat\\tmp\\xmltech.xml");

            int yRelatif = DrawObject.Axe + DrawObject.HeightFluxServer;
            for (int i = 0; i < lstFluxFonc.Count; i++)
            {
                if (cw == null)
                {
                    yRelatif += MakeSequenceFluxOneFonc((XmlElement)lstFluxFonc[i], xmlFluxFoncApp, xmlFluxAppTech, xmlFluxTech, yRelatif) + DrawObject.Axe + DrawObject.HeightFluxServer;
                }
                else
                {
                    drawArea.GraphicsList.Clear();
                    yRelatif = DrawObject.Axe + DrawObject.HeightFluxServer;
                    drawArea.Switch(false);
                    yRelatif = MakeSequenceFluxOneFonc((XmlElement)lstFluxFonc[i], xmlFluxFoncApp, xmlFluxAppTech, xmlFluxTech, yRelatif) + DrawObject.Axe + DrawObject.HeightFluxServer;
                    drawArea.Switch(true);
                    drawArea.MajObjets();
                    string sDiagram = SaveDiagramFromPath("Seq" + i + sGuidVueDeploy, cw.getImagePath(), "");
                    if(yRelatif != DrawObject.Axe + DrawObject.HeightFluxServer)
                        lstImg.Add(sDiagram);
                }
            }
            return lstImg;
        }

        public void MakeVueInf3D(string sGuidVueSrvPhy)
        {
            XmlExcel xmlFlux = XmlCreatFlux(sGuidVueInf, sGuidVueSrvPhy);
            ToolServer3D t3d = (ToolServer3D)drawArea.tools[(int)DrawArea.DrawToolType.Server3D];
            t3d.xmlLink = Xml3dCreatLink(xmlFlux);

            CreatGridObjet();

            //drawArea.tools[(int)DrawArea.DrawToolType.Server3D].LoadObject(' ', GuidGVue.ToString(), sGuidVueSrvPhy);
            drawArea.tools[(int)DrawArea.DrawToolType.Server3D].LoadObject(' ', sGuidVueSrvPhy, sGuidVueSrvPhy);
            DrawGrid dg = (DrawGrid)drawArea.GraphicsList[drawArea.GraphicsList.Count - 1];
            dg.CreatVlan3D();

            
            IEnumerator ienum = t3d.xmlLink.root.GetEnumerator();
            XmlNode Node1;
            int iCont = 9;

            while (ienum.MoveNext() && iCont > 0)
            {
                Node1 = (XmlNode)ienum.Current;
                if (Node1.NodeType == XmlNodeType.Element && Node1.Name == "Server")
                {
                    ArrayList lstServerPhy1 = Xml3dGetLstServerPhyFromAtt((XmlElement)Node1, "Guid");
                    if (lstServerPhy1.Count > 0)
                    {
                        int idxObj1 = drawArea.GraphicsList.FindObjet(0, (string)lstServerPhy1[0]);
                        int iNbrLink1 = t3d.xmlLink.XmlGetNbrElFromName((XmlElement)Node1, "Link", 0);
                        int idxLink1 = t3d.xmlLink.XmlGetNbrElFromNameAndAtt((XmlElement)Node1, "Link", "Done", "1", 0);

                        IEnumerator ienuml = ((XmlElement)Node1).GetEnumerator();
                        XmlNode NodeLink1;

                        while (ienuml.MoveNext() && iCont > 0)
                        {
                            NodeLink1 = (XmlNode)ienuml.Current;
                            if (NodeLink1.NodeType == XmlNodeType.Element && NodeLink1.Name == "Link")
                            {
                                if (((XmlElement)NodeLink1).GetAttribute("Done") != "1")
                                {
                                    XmlElement Node2 = t3d.xmlLink.XmlFindElFromAtt(t3d.xmlLink.root, "Nom", ((XmlElement)NodeLink1).GetAttribute("Nom"), 0);
                                    ArrayList lstServerPhy2 = Xml3dGetLstServerPhyFromAtt(Node2, "Guid");
                                    int idxObj2 = drawArea.GraphicsList.FindObjet(0, (string)lstServerPhy2[0]);
                                    int iNbrLink2 = t3d.xmlLink.XmlGetNbrElFromName(Node2, "Link", 0);
                                    int idxLink2 = t3d.xmlLink.XmlGetNbrElFromNameAndAtt(Node2, "Link", "Done", "1", 0);
                                    XmlElement NodeLink2 = t3d.xmlLink.XmlFindElFromAtt(Node2, "Nom", ((XmlElement)Node1).GetAttribute("Nom"), 0);

                                    if (idxObj1 > -1 && idxObj2 > -1)
                                    {
                                        bool bCreat = true;
                                        for (int i = 0; i < lstServerPhy1.Count; i++)
                                            if (lstServerPhy2.Contains(lstServerPhy1[i])) bCreat = false;
                                        if (bCreat)
                                        {
                                            DrawLink3D dl = new DrawLink3D(this, ((XmlElement)NodeLink1).GetAttribute("IdFlux"), ((XmlElement)NodeLink1).GetAttribute("NomFlux"), lstServerPhy1, idxLink1, iNbrLink1, lstServerPhy2, idxLink2, iNbrLink2);
                                            drawArea.GraphicsList.Add(dl);
                                            drawArea.GraphicsList.MoveLastToBack();
                                        }
                                        ((XmlElement)NodeLink1).SetAttribute("Done", "1");
                                        ((XmlElement)NodeLink2).SetAttribute("Done", "1");
                                    }
                                }
                                //}
                                //iCont--;
                            }
                        }

                        // idxObj=-1 si c'est une VIP (lb). Ce test doit être enleve, et les vips doivent être traitees
                        // est-ce encore d'actualite?
                        /*
                        if (idxObj1 > -1)
                        {
                            //DrawServer3D dServerPhy = (DrawServer3D)drawArea.GraphicsList[idxObj];
                            //DrawLink3D dl = new DrawLink3D(this, "Link", dServerPhy.rectangle.X, dServerPhy.rectangle.Y, 20, 20);
                            //drawArea.GraphicsList.Add(dl);
                        }
                        */
                    }
                }
            }
            


            //Vérification de l'existance de User dans la vue
            //drawArea.tools[(int)DrawArea.DrawToolType.TechUser].LoadObject(' ', null);

            //Vérification de l'existance de Application dans la vue
            //drawArea.tools[(int)DrawArea.DrawToolType.Application].LoadObject(' ', null);
        }

        public void MakeVueInf(string sGuiVueSrvPhy)
        {
            
            //Vérification de l'existance de User dans la vue
            drawArea.tools[(int)DrawArea.DrawToolType.TechUser].LoadObject(' ', GuidGVue.ToString(), null);

            //Vérification de l'existance de Application dans la vue
            drawArea.tools[(int)DrawArea.DrawToolType.Application].LoadObject(' ', GuidGVue.ToString(), null);

            drawArea.tools[(int)DrawArea.DrawToolType.Server].LoadObject(' ', GuidGVue.ToString(), null);
            oCnxBase.LoadSousObjetsInf(sGuiVueSrvPhy, (int) DrawArea.DrawToolType.Server);

            drawArea.tools[(int)DrawArea.DrawToolType.Genks].LoadObject(' ', GuidGVue.ToString(), null);
            oCnxBase.LoadSousObjetsInf(sGuiVueSrvPhy, (int)DrawArea.DrawToolType.Genks);

            drawArea.tools[(int)DrawArea.DrawToolType.Genpod].LoadObject(' ', GuidGVue.ToString(), null);
            oCnxBase.LoadSousObjetsInf(sGuiVueSrvPhy, (int)DrawArea.DrawToolType.Genpod);

            drawArea.tools[(int)DrawArea.DrawToolType.Gensas].LoadObject(' ', GuidGVue.ToString(), null);
            oCnxBase.LoadSousObjetsInf(sGuiVueSrvPhy, (int)DrawArea.DrawToolType.Gensas);

            
            drawArea.tools[(int)DrawArea.DrawToolType.InfLink].LoadObject(' ', GuidGVue.ToString(), null);
            
            oCnxBase.LoadService_AliasInf(sGuiVueSrvPhy);
            
        }

        public void GetInterLinks()
        {
            int n = drawArea.GraphicsList.Count;

            for (int i = 0; i < n; i++)
            {
                if (drawArea.GraphicsList[i].GetType() == typeof(DrawInterLink))
                {
                    DrawInterLink dil = (DrawInterLink)drawArea.GraphicsList[i];
                    dil.GetInterLinks();
                }
                
            }
        }

        public void GetServerLinks()
        {
            int n = drawArea.GraphicsList.Count;

            for (int i = 0; i < n; i++)
            {
                if (drawArea.GraphicsList[i].GetType() == typeof(DrawServerPhy))
                {
                    DrawServerPhy dsp = (DrawServerPhy) drawArea.GraphicsList[i];
                    dsp.GetServerLinks( "AppUser" );
                    //dsp.GetServerLinks("Application");
                    dsp.GetServerLinksApp();
                    dsp.GetServerLinksApp("Server");
                }
                else if (drawArea.GraphicsList[i].GetType() == typeof(DrawServerSite))
                {
                    DrawServerSite dsp = (DrawServerSite)drawArea.GraphicsList[i];
                    dsp.GetServerLinksU( "AppUser" );
                    //dsp.GetServerLinks("Application");
                    dsp.GetServerLinksApp();
                    dsp.GetServerLinksApp("Server");
                }
            }
        }

        public void GetServerTypeFormPlateformPattern()
        {
            List<string[]> lstServerType = new List<string[]>();

            if (oCnxBase.CBRecherche("select GuidApplication, GuidServerType from PlateformPatternLink where GuidVue='" + GuidVue.ToString() + "'"))
            {
                while (oCnxBase.Reader.Read())
                {
                    string[] aEng;
                    aEng = new string[2];
                    aEng[0] = oCnxBase.Reader.GetString(0); aEng[1] = oCnxBase.Reader.GetString(1);
                    lstServerType.Add(aEng);
                }
            }
            oCnxBase.CBReaderClose();
            foreach (var el in lstServerType)
            {
                int i = drawArea.GraphicsList.FindObjet(0, el[0]);
                if (i > -1)
                {
                    DrawApplication da = (DrawApplication)drawArea.GraphicsList[i];
                    da.LoadServerType(el[1]);
                    da.AligneObjet();
                }
            }
        }

        public void GetLabelLinks()
        {
            int n = drawArea.GraphicsList.Count;

            for (int i = 0; i < n; i++)
            {
                if (drawArea.GraphicsList[i].GetType() == typeof(DrawGenpod))
                {
                    DrawGenpod dgp = (DrawGenpod)drawArea.GraphicsList[i];
                    dgp.GetLabelLinks();
                }
                else if (drawArea.GraphicsList[i].GetType() == typeof(DrawGening))
                {
                    DrawGening dgi = (DrawGening)drawArea.GraphicsList[i];
                    dgi.GetLabelLinks();
                }
                else if (drawArea.GraphicsList[i].GetType() == typeof(DrawGensas))
                {
                    DrawGensas dgs = (DrawGensas)drawArea.GraphicsList[i];
                    dgs.GetLabelLinks();
                }
                else if (drawArea.GraphicsList[i].GetType() == typeof(DrawGensvc))
                {
                    DrawGensvc dgs = (DrawGensvc)drawArea.GraphicsList[i];
                    dgs.GetLabelLinks();
                }
                else if (drawArea.GraphicsList[i].GetType() == typeof(DrawInspod))
                {
                    DrawInspod dip = (DrawInspod)drawArea.GraphicsList[i];
                    dip.GetLabelLinks();
                }
                else if (drawArea.GraphicsList[i].GetType() == typeof(DrawInsing))
                {
                    DrawInsing dii = (DrawInsing)drawArea.GraphicsList[i];
                    dii.GetLabelLinks();
                }
                else if (drawArea.GraphicsList[i].GetType() == typeof(DrawInssas))
                {
                    DrawInssas dis = (DrawInssas)drawArea.GraphicsList[i];
                    dis.GetLabelLinks();
                }
                else if (drawArea.GraphicsList[i].GetType() == typeof(DrawInssvc))
                {
                    DrawInssvc dis = (DrawInssvc)drawArea.GraphicsList[i];
                    dis.GetLabelLinks();
                }
                else if (drawArea.GraphicsList[i].GetType() == typeof(DrawVue))
                { 
                    DrawVue dv = (DrawVue)drawArea.GraphicsList[i];
                    dv.GetLabelLinks();
                }
                else if (drawArea.GraphicsList[i].GetType() == typeof(DrawApplication))
                {
                    DrawApplication da = (DrawApplication)drawArea.GraphicsList[i];
                    da.GetLabelLinks();
                }
                else if (drawArea.GraphicsList[i].GetType() == typeof(DrawServer))
                {
                    DrawServer ds = (DrawServer)drawArea.GraphicsList[i];
                    ds.GetLabelLinks();
                }
                else if (drawArea.GraphicsList[i].GetType() == typeof(DrawServerPhy))
                {
                    DrawServerPhy dsp = (DrawServerPhy)drawArea.GraphicsList[i];
                    dsp.GetLabelLinks();
                }
            }
        }

        public void GetSanCardLinks()
        {
            int n = drawArea.GraphicsList.Count;

            for (int i = 0; i < n; i++)
            {
                if (drawArea.GraphicsList[i].GetType() == typeof(DrawSanCard))
                {
                    DrawSanCard dsc = (DrawSanCard)drawArea.GraphicsList[i];
                    dsc.GetSanCardLinks("SanCardA");
                }
            }
        }

        public void ClearPropObjet()
        {
            tbGuid.Text = "";
            
            // Initialize the properties
            for(int i=dataGrid.Rows.Count-1;i>=0; i--) {
                dataGrid.Rows.RemoveAt(i);
            }
        }

        public DrawObject RechercheObjet(string sGuidKey)
        {
            DrawObject o;

            for (int i = 0; i < drawArea.GraphicsList.Count; i++)
            {
                o = drawArea.GraphicsList[i];
                if(o.GuidkeyObjet == new Guid(sGuidKey)) return o;
            }
            return null;
        }

        

        private void menuItem40_Click(object sender, EventArgs e)
        {
            CommandVisu();
        }

        //*****************************************************************************************
        //*****************************************************************************************
        //*****************************************************************************************
        //
        //         Methodes utilisees par les autres forms
        //
        //*****************************************************************************************
        //*****************************************************************************************
        //*****************************************************************************************


        // Il y a d'autres fonction Xml à regrouper dans un objet ex: GetElFromInnerXml ("DELETE FROM DansVue Where GuidObjet = '" + obj + "'");




        public int XmlAnalyseFluxNode(string sSeparator, XmlExcel xmlFlux, XmlElement Node, DrawTab oTab)
        {
            // sFormat ex: "," ou "<br>", ou ....
            IEnumerator ienum;
            XmlNode nOrigine, nCible;
            string OrigineLocation = "", OrigineNom = "", OrigineNCard = "", OrigineIP = "", OrigineIPNat = "", OrigineVlan = "", OrigineVlanClass = "";
            string CibleLocation = "", CibleNom = "", CibleNCard  = "", CibleIP = "", CibleIPNat = "", CibleVlan = "", CibleVlanClass = "";
            string ServiceNom = "", ServiceProtocol = "", ServicePort = "";
            XmlElement elOrigine, elCible, elGroupService;
            string sautLigne = sSeparator;
            int preFix = sautLigne.Length;
            int nbrLigne = 0;
            
            bool bNext = false; 

            if (Node.GetAttribute("Selected") != "Yes") return nbrLigne;
            oTab.LstValue.Add(Node.GetAttribute("Id"));
            oTab.LstValue.Add(Node.GetAttribute("Nom"));

            elOrigine = xmlFlux.XmlGetFirstElFromName(Node, "Origine");
            if (elOrigine.GetAttribute("Selected") != "Yes") return nbrLigne;
            ienum = elOrigine.GetEnumerator();
            while (ienum.MoveNext())
            {
                nOrigine = (XmlNode)ienum.Current;
                if (nOrigine.NodeType == XmlNodeType.Element && ((XmlElement)nOrigine).GetAttribute("Selected") == "Yes")
                {
                    XmlElement elCur = (XmlElement)nOrigine;

                    ArrayList lstNCard = xmlFlux.XmlGetLstElFromName(elCur, "NCard");
                    ArrayList lstLabelClass = xmlFlux.XmlGetLstElFromName(elCur, "LabelClass");
                    for (int i = 0; i < lstNCard.Count; i++)
                    {
                        XmlElement elNCard = (XmlElement)lstNCard[i];
                        if (elNCard.GetAttribute("Selected") == "Yes")
                        {
                            bNext = true;
                            xmlFlux.XmlGetAttValueFromElName(elNCard, "Guild", "Vlan");
                            OrigineLocation += sautLigne + elCur.GetAttribute("Location");
                            OrigineNom += sautLigne + elCur.GetAttribute("Nom");
                            OrigineNCard += sautLigne + elNCard.GetAttribute("Guid");
                            OrigineIP += sautLigne + elNCard.GetAttribute("IP");
                            OrigineIPNat += sautLigne + elNCard.GetAttribute("Nat");
                            OrigineVlan += sautLigne + xmlFlux.XmlGetAttValueFromElName(elNCard, "Guid", "Vlan");
                            OrigineVlanClass += sautLigne + xmlFlux.XmlGetAttValueFromElName(elNCard, "Guid", "VlanClass");
                        }
                    }
                    for (int i = 0; i < lstLabelClass.Count; i++)
                    {
                        XmlElement elLabelClass = (XmlElement)lstLabelClass[i];
                        if (elLabelClass.GetAttribute("Selected") == "Yes")
                        {
                            ArrayList lstLabelValue = xmlFlux.XmlGetLstElFromName(elLabelClass, "LabelValue");
                            for (int j = 0; j < lstLabelValue.Count; j++)
                            {
                                XmlElement elLabelValue = (XmlElement)lstLabelValue[j];
                                if (elLabelValue.GetAttribute("Selected") == "Yes")
                                {
                                    bNext = true;
                                    OrigineLocation += sautLigne + elCur.GetAttribute("Location");
                                    OrigineNom += sautLigne + elCur.GetAttribute("Nom");
                                    OrigineNCard += sautLigne + elLabelValue.GetAttribute("Guid"); //elNCard.GetAttribute("Guid");
                                                                                                   //OrigineIP += sautLigne + elLabelClass.GetAttribute("Nom") + "=" + elLabelValue.GetAttribute("Nom");
                                    OrigineIP += sautLigne + elLabelValue.GetAttribute("Nom");
                                    OrigineIPNat += sautLigne + ""; // elNCard.GetAttribute("Nat");
                                    OrigineVlan += sautLigne + ""; // xmlFlux.XmlGetAttValueFromElName(elNCard, "Guid", "Vlan");
                                    OrigineVlanClass += sautLigne + ""; // xmlFlux.XmlGetAttValueFromElName(elNCard, "Guid", "VlanClass");
                                }
                            }
                        }
                    }

                }
            }
            
            if (!bNext) return nbrLigne;
            oTab.LstValue.Add(OrigineLocation.Substring(preFix));
            oTab.LstValue.Add(OrigineNom.Substring(preFix));
            oTab.LstValue.Add(OrigineNCard.Substring(preFix));
            oTab.LstValue.Add(OrigineIP.Substring(preFix));
            oTab.LstValue.Add(OrigineIPNat.Substring(preFix));
            oTab.LstValue.Add(OrigineVlan.Substring(preFix));
            oTab.LstValue.Add(OrigineVlanClass.Substring(preFix));

            bNext = false;
            elCible = xmlFlux.XmlGetFirstElFromName(Node, "Cible");
            if (elCible.GetAttribute("Selected") != "Yes") return nbrLigne;
            ienum = elCible.GetEnumerator();
            while (ienum.MoveNext())
            {
                nCible = (XmlNode)ienum.Current;
                if (nCible.NodeType == XmlNodeType.Element && ((XmlElement)nCible).GetAttribute("Selected") == "Yes")
                {
                    XmlElement elCur = (XmlElement)nCible;

                    ArrayList lstNCard = xmlFlux.XmlGetLstElFromName(elCur, "NCard");
                    ArrayList lstLabelClass = xmlFlux.XmlGetLstElFromName(elCur, "LabelClass");
                    for (int i = 0; i < lstNCard.Count; i++)
                    {
                        XmlElement elNCard = (XmlElement)lstNCard[i];
                        if (elNCard.GetAttribute("Selected") == "Yes")
                        {
                            bNext = true;
                            CibleLocation += sautLigne + elCur.GetAttribute("Location");
                            CibleNom += sautLigne + elCur.GetAttribute("Nom");
                            CibleNCard += sautLigne + elNCard.GetAttribute("Guid");
                            CibleIP += sautLigne + elNCard.GetAttribute("IP");
                            CibleIPNat += sautLigne + elNCard.GetAttribute("Nat");
                            CibleVlan += sautLigne + xmlFlux.XmlGetAttValueFromElName(elNCard, "Guid", "Vlan");
                            CibleVlanClass += sautLigne + xmlFlux.XmlGetAttValueFromElName(elNCard, "Guid", "VlanClass");
                        }
                    }
                    for (int i = 0; i < lstLabelClass.Count; i++)
                    {
                        XmlElement elLabelClass = (XmlElement)lstLabelClass[i];
                        if (elLabelClass.GetAttribute("Selected") == "Yes")
                        {
                            ArrayList lstLabelValue = xmlFlux.XmlGetLstElFromName(elLabelClass, "LabelValue");
                            for (int j = 0; j < lstLabelValue.Count; j++)
                            {
                                XmlElement elLabelValue = (XmlElement)lstLabelValue[j];
                                if (elLabelValue.GetAttribute("Selected") == "Yes")
                                {
                                    bNext = true;
                                    CibleLocation += sautLigne + elCur.GetAttribute("Location");
                                    CibleNom += sautLigne + elCur.GetAttribute("Nom");
                                    CibleNCard += sautLigne + elLabelValue.GetAttribute("Guid");  //elNCard.GetAttribute("Guid");
                                                                                                  //CibleIP += sautLigne + elLabelClass.GetAttribute("Nom") + "=" + elLabelValue.GetAttribute("Nom");
                                    CibleIP += sautLigne + elLabelValue.GetAttribute("Nom");
                                    CibleIPNat += sautLigne + ""; // elNCard.GetAttribute("Nat");
                                    CibleVlan += sautLigne + ""; // xmlFlux.XmlGetAttValueFromElName(elNCard, "Guid", "Vlan");
                                    CibleVlanClass += sautLigne + ""; // xmlFlux.XmlGetAttValueFromElName(elNCard, "Guid", "VlanClass");
                                }
                            }
                        }
                    }

                }
            }

            
            if (!bNext) return nbrLigne;
            oTab.LstValue.Add(CibleLocation.Substring(preFix));
            oTab.LstValue.Add(CibleNom.Substring(preFix));
            oTab.LstValue.Add(CibleNCard.Substring(preFix));
            oTab.LstValue.Add(CibleIP.Substring(preFix));
            oTab.LstValue.Add(CibleIPNat.Substring(preFix));
            oTab.LstValue.Add(CibleVlan.Substring(preFix));
            oTab.LstValue.Add(CibleVlanClass.Substring(preFix));

            elGroupService = xmlFlux.XmlGetFirstElFromName(Node, "GroupService");
            oTab.LstValue.Add(elGroupService.GetAttribute("Guid"));
            ArrayList lstService = xmlFlux.XmlGetLstElFromName(elGroupService, "Service");
            nbrLigne = lstService.Count;
            for (int i = 0; i < nbrLigne; i++)
            {
                XmlElement elService = (XmlElement)lstService[i];
                ServiceNom += sautLigne + elService.GetAttribute("Nom");
                ServiceProtocol += sautLigne + elService.GetAttribute("Protocol");
                ServicePort += sautLigne + elService.GetAttribute("Ports");
            }
            if (ServiceNom.Length != 0)
            {
                oTab.LstValue.Add(ServiceNom.Substring(preFix));
                oTab.LstValue.Add(ServiceProtocol.Substring(preFix));
                oTab.LstValue.Add(ServicePort.Substring(preFix));
            } else
            {
                nbrLigne = 1;
                oTab.LstValue.Add("Not Defined");
                oTab.LstValue.Add("Not Defined");
                oTab.LstValue.Add("Not Defined");
            }
            return nbrLigne;
        }



        public XmlElement XmlCopyEl(XmlDocument xmlDoc, XmlElement elOri)
        {
            XmlElement el = xmlDoc.CreateElement(elOri.Name);
            for (int i = 0; i < elOri.Attributes.Count; i++)
                el.SetAttribute(elOri.Attributes[i].Name, elOri.Attributes[i].Value);
            return el;
        }
        
        
        public void XmldeleteBaseFromXml(XmlElement elCur)
        {
            IEnumerator ienum = elCur.GetEnumerator();
            XmlNode Node;

            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    XmlElement el = (XmlElement)Node;
                    XmlAttributeCollection lstAtt = el.Attributes;
                    string sSql = "delete from " + el.Name + " where ";
                    for (int i = 0; i < lstAtt.Count; i++) sSql += lstAtt[i].Name + "='" + lstAtt[i].Value + "' and ";
                    oCnxBase.CBWrite(sSql.Substring(0,sSql.Length-5));
                }
            }
        }

        
        
        public void XmlinsertBaseFromXml(XmlElement elCur)
        {
            IEnumerator ienum = elCur.GetEnumerator();
            XmlNode Node;

            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    int iAction = 0;
                    XmlElement el = (XmlElement)Node;
                    XmlAttributeCollection lstAtt = el.Attributes;
                    string sSql1 = "Insert into " + el.Name + " (", sSql2 = "Values (";
                    string[] Keys = null;

                    if (lstAtt[0].Name == "SearchKey")
                    {
                        iAction = 2;
                        Keys = lstAtt[0].Value.Split(',');
                        if (oCnxBase.CBRecherche("SELECT " + oCnxBase.GetSelectSearchKey(Keys) + " FROM " + el.Name + " WHERE " + oCnxBase.XmlGetWhereSearchKey(el, Keys))) iAction++;
                        oCnxBase.CBReaderClose();
                    }

                    switch (iAction)
                    {
                        case 0: // creation sans confDataBase
                            for (int i = 0; i < lstAtt.Count; i++)
                            {
                                sSql1 += lstAtt[i].Name + ",";
                                sSql2 += "'" + lstAtt[i].Value + "',";
                            }
                            sSql1 = sSql1.Substring(0, sSql1.Length - 1) + ") ";
                            sSql2 = sSql2.Substring(0, sSql2.Length - 1) + ") ";
                            oCnxBase.CBWrite(sSql1 + " " + sSql2);
                            break;
                        case 1: //update sans confDataBase (att SearchKey existant & present en base)
                            break;
                        case 2: //creation conforme avec oCnxBase (att SearchKey et atts with caractère type au début)
                            oCnxBase.XmlCreateFromXml(el);
                            break;
                        case 3: //update conforme avec oCnxBase (att SearchKey et atts with caractère type au début)
                            oCnxBase.XmlUpdateFromXml(el, Keys);
                            break;
                            
                    }
                }
            }
        }

        public void XmlAllSetParentAttributValueFromEl(XmlElement el, string sAtt, string sValue)
        {
            if(el.GetAttribute(sAtt, sValue) !=null) el.SetAttribute(sAtt, sValue);
            if (el.ParentNode != null && el.ParentNode.NodeType == XmlNodeType.Element) XmlAllSetParentAttributValueFromEl((XmlElement)el.ParentNode, sAtt, sValue);
        }
        
        public void XmlAllSetAttributValueFromEl(XmlElement el, string sAtt, string sValue, string sFiltre = null)
        {
            IEnumerator ienum = el.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    XmlElement elCur = (XmlElement)Node;
                    if (sFiltre == null || sFiltre.Contains(elCur.Name))
                    {
                        if (elCur.GetAttribute(sAtt) != null) elCur.SetAttribute(sAtt, sValue);
                        XmlAllSetAttributValueFromEl(elCur, sAtt, sValue);
                    }
                }
            }
        }

        public XmlElement XmlFindFirstElFromName(XmlElement parent, string sName)
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
                    elCur = XmlFindFirstElFromName((XmlElement)Node, sName);
                    if (elCur != null) return elCur;
                }
            }
            return null;
        }

        public string XmlGetTextEl(XmlElement el)
        {

            IEnumerator ienum = el.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Text)
                {
                    XmlText elT = (XmlText)Node;
                    return elT.Value;
                }
            }
            return null;
        }

        public int xmlExistElFromLst(XmlElement elFind, ArrayList lstEl, string sComparaison)
        {
            string sValelFind = XmlGetAttValueAFromAttValueB(elFind, "Value", "Nom", sComparaison);
            if (sValelFind != "")
            {
                for (int i = 0; i < lstEl.Count; i++)
                {
                    XmlElement el = (XmlElement)lstEl[i];
                    if (sValelFind == XmlGetAttValueAFromAttValueB(el, "Value", "Nom", sComparaison)) return i;
                }
                return -1; // El non trouvé
            }
            return -2; // Att de comparaison son trouvé
        }

        public ArrayList XmlGetLstElFromName(XmlElement parent, string sName, int Profondeur)
        {
            ArrayList lstEl = new ArrayList();
            if (--Profondeur >= 0)
            {
                IEnumerator ienum = parent.GetEnumerator();
                XmlNode Node;
                while (ienum.MoveNext())
                {
                    Node = (XmlNode)ienum.Current;
                    if (Node.NodeType == XmlNodeType.Element)
                    {
                        XmlElement elCur = (XmlElement)Node;
                        if (elCur.Name == sName && xmlExistElFromLst(elCur, lstEl, "Guid" + sName) <= -1) lstEl.Add(elCur);
                        ArrayList lstElfils = XmlGetLstElFromName((XmlElement)Node, sName, Profondeur);
                        for (int i = 0; i < lstElfils.Count; i++)
                        {
                            if (xmlExistElFromLst(elCur, lstEl, "Guid" + sName) <= -1) lstEl.Add(lstElfils[i]);
                        }
                    }
                }
            }
            return lstEl;
        }

        public XmlElement XmlFindFirstElFromName(XmlElement parent, string sName, int Profondeur)
        {
            if (--Profondeur >= 0)
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
                        elCur = XmlFindFirstElFromName((XmlElement)Node, sName, Profondeur);
                        if (elCur != null) return elCur;
                    }
                }
            }
            return null;
        }

        public void XmlCreatXmldb(XmlDB xmlDB, string sGuidApplication, string sGuidAppVersion="")
        {
            if (sGuidAppVersion != "")
            {
                drawArea.GraphicsList.Clear();
                oCureo = new ExpObj(new Guid(sGuidApplication), "", DrawArea.DrawToolType.Application);
                drawArea.tools[(int)oCureo.ObjTool].LoadSimpleObjectSansGraph(oCureo);

                if (oCureo.oDraw != null)
                {
                    XmlElement el = null;
                    DrawObject o = oCureo.oDraw;
                    if (xmlDB.SetCursor("root"))
                    {
                        //el = o.XmlCreatObject(xmlDB);
                        el = o.xmlCreatObjetFromCursor(xmlDB);
                        xmlDB.CursorClose();

                        oCureo = new ExpObj(new Guid(sGuidAppVersion), "", DrawArea.DrawToolType.AppVersion);
                        drawArea.tools[(int)oCureo.ObjTool].LoadSimpleObjectSansGraph(oCureo);

                        if (oCureo.oDraw != null)
                        {
                            if (xmlDB.SetCursor(xmlDB.XmlGetFirstElFromParent(el, "After")))
                            {
                                o = oCureo.oDraw;
                                el = o.xmlCreatObjetFromCursor(xmlDB);
                                xmlDB.CursorClose();

                                List<string[]> lstVue = new List<string[]>();
                                ArrayList lstLayer = null;

                                if (oCnxBase.CBRecherche("SELECT GuidVue, NomVue, GuidGVue, GuidEnvironnement, GuidVueInf, TypeVue.GuidTypeVue, NomTypeVue FROM Vue, TypeVue WHERE Vue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidAppVersion='" + sGuidAppVersion + "' ORDER BY NomTypeVue"))
                                    while (oCnxBase.Reader.Read())
                                    {
                                        string sGuidTypeVue = oCnxBase.Reader.GetString(5);
                                        if (sGuidTypeVue != "c4e818ea-5e91-4ede-8038-9e19efb9d3bd" && sGuidTypeVue != "1532eac8-4ae4-4c09-9d2c-7532f3b303db") {
                                            //vue info server ou info server 3d
                                            string sEnv = "", sGuidVueInf = "";
                                            if (!oCnxBase.Reader.IsDBNull(3)) sEnv = oCnxBase.Reader.GetString(3);
                                            if (!oCnxBase.Reader.IsDBNull(4)) sGuidVueInf = oCnxBase.Reader.GetString(4);

                                            string[] aEnreg = new string[7];
                                            aEnreg[0] = oCnxBase.Reader.GetString(0);   // GuidVue
                                            aEnreg[1] = oCnxBase.Reader.GetString(1);   // NomVue
                                            aEnreg[2] = oCnxBase.Reader.GetString(2);   // GuidGVue
                                            aEnreg[3] = sEnv;                           // GuidEnvironnement
                                            aEnreg[4] = sGuidVueInf;                    // GuidVueInf
                                            aEnreg[5] = oCnxBase.Reader.GetString(5);   // GuidTypeVue
                                            aEnreg[6] = oCnxBase.Reader.GetString(6);   // NomTypeVue

                                            lstVue.Add(aEnreg);
                                            //lstVue.Add(oCnxBase.Reader.GetString(0) + "," + oCnxBase.Reader.GetString(1) + "," + sEnv + "," + sGuidVueInf + "," + oCnxBase.Reader.GetString(4) + "," + oCnxBase.Reader.GetString(5));
                                        }
                                    }
                                oCnxBase.CBReaderClose();

                                if (oCnxBase.CBRecherche("SELECT GuidLayer, NomLayer FROM Layer WHERE GuidAppVersion='" + sGuidAppVersion + "'"))
                                {
                                    lstLayer = new ArrayList();
                                    while (oCnxBase.Reader.Read()) lstLayer.Add(oCnxBase.Reader.GetString(0) + "," + oCnxBase.Reader.GetString(1));
                                }
                                oCnxBase.CBReaderClose();

                                xmlDB.CursorClose();
                                oCnxBase.CreaXmlApplication(xmlDB, el, sGuidApplication, sGuidAppVersion, lstVue, lstLayer);

                                //xmlDB.docXml.Save(Parent.GetFullPath((string)cbGuidApplication.Items[cbApplication.SelectedIndex]) + "\\" + (string)cbApplication.Items[cbApplication.SelectedIndex] + "Serveur.xml");
                            }
                            xmlDB.CursorClose();
                        }
                    }
                    xmlDB.CursorClose();
                }
            }

        }

        public void XmlCreatXmlVue(XmlDB xmlDB, WorkApplication wkApp, string sGuidVue)
        {
            if (wkApp.GuidAppVersion != null)
            {
                oCureo = new ExpObj(wkApp.Guid, "", DrawArea.DrawToolType.Application);
                drawArea.tools[(int)oCureo.ObjTool].LoadSimpleObjectSansGraph(oCureo);

                if (oCureo.oDraw != null)
                {
                    XmlElement el = null;
                    DrawObject o = oCureo.oDraw;
                    if (xmlDB.SetCursor("root"))
                    {
                        //el = o.XmlCreatObject(xmlDB);
                        el = o.xmlCreatObjetFromCursor(xmlDB);
                        xmlDB.CursorClose();

                        oCureo = new ExpObj(wkApp.GuidAppVersion, "", DrawArea.DrawToolType.AppVersion);
                        drawArea.tools[(int)oCureo.ObjTool].LoadSimpleObjectSansGraph(oCureo);

                        if (oCureo.oDraw != null)
                        {
                            if (xmlDB.SetCursor(xmlDB.XmlGetFirstElFromParent(el, "After")))
                            {
                                o = oCureo.oDraw;
                                el = o.xmlCreatObjetFromCursor(xmlDB);
                                xmlDB.CursorClose();

                                List<string[]> lstVue = new List<string[]>();
                                ArrayList lstLayer = null;

                                if (oCnxBase.CBRecherche("SELECT GuidVue, NomVue, GuidGVue, GuidEnvironnement, GuidVueInf, TypeVue.GuidTypeVue, NomTypeVue FROM Vue, TypeVue WHERE Vue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidAppVersion='" + wkApp.GuidAppVersion + "' AND Vue.GuidVue='" + sGuidVue + "' ORDER BY NomTypeVue"))
                                    while (oCnxBase.Reader.Read())
                                    {
                                        string sEnv = "", sGuidVueInf = "";
                                        if (!oCnxBase.Reader.IsDBNull(3)) sEnv = oCnxBase.Reader.GetString(3);
                                        if (!oCnxBase.Reader.IsDBNull(4)) sGuidVueInf = oCnxBase.Reader.GetString(4);

                                        string[] aEnreg = new string[7];
                                        aEnreg[0] = oCnxBase.Reader.GetString(0);   // GuidVue
                                        aEnreg[1] = oCnxBase.Reader.GetString(1);   // NomVue
                                        aEnreg[2] = oCnxBase.Reader.GetString(2);   // GuidGVue
                                        aEnreg[3] = sEnv;                           // GuidEnvironnement
                                        aEnreg[4] = sGuidVueInf;                    // GuidVueInf
                                        aEnreg[5] = oCnxBase.Reader.GetString(5);   // GuidTypeVue
                                        aEnreg[6] = oCnxBase.Reader.GetString(6);   // NomTypeVue

                                        lstVue.Add(aEnreg);
                                        //lstVue.Add(oCnxBase.Reader.GetString(0) + "," + oCnxBase.Reader.GetString(1) + "," + sEnv + "," + sGuidVueInf + "," + oCnxBase.Reader.GetString(4) + "," + oCnxBase.Reader.GetString(5));
                                    }
                                oCnxBase.CBReaderClose();

                                if (oCnxBase.CBRecherche("SELECT GuidLayer, NomLayer FROM Layer WHERE GuidAppVersion='" + wkApp.GuidAppVersion + "'"))
                                {
                                    lstLayer = new ArrayList();
                                    while (oCnxBase.Reader.Read()) lstLayer.Add(oCnxBase.Reader.GetString(0) + "," + oCnxBase.Reader.GetString(1));
                                }
                                oCnxBase.CBReaderClose();

                                xmlDB.CursorClose();
                                oCnxBase.CreaXmlApplication(xmlDB, el, wkApp.Guid.ToString() , wkApp.GuidAppVersion.ToString(), lstVue, lstLayer);

                                xmlDB.docXml.Save(GetFullPath(wkApp) + "\\Vue.xml");
                            }
                            xmlDB.CursorClose();
                        }
                    }
                    xmlDB.CursorClose();
                }
            }

        }

        /*public XmlDB XmlCreatXmldb(string sGuidApplication)
        {
            drawArea.GraphicsList.Clear();
            oCureo = new ExpObj(new Guid(sGuidApplication), "", DrawArea.DrawToolType.Application);
            drawArea.tools[(int)oCureo.ObjTool].LoadSimpleObjectSansGraph(oCureo.GuidObj.ToString());

            if (oCureo.oDraw != null)
            {
                XmlDB xmlDB = new XmlDB(this, "Applications");
                XmlElement el = null;

                drawArea.tools[(int)DrawArea.DrawToolType.Application].LoadSimpleObject(sGuidApplication);
                int i = drawArea.GraphicsList.FindObjet(0, sGuidApplication);
                DrawObject o = oCureo.oDraw;
                //DrawApplication da = (DrawApplication)drawArea.GraphicsList[i];
                if (xmlDB.SetCursor("root"))
                {
                    el = o.XmlCreatObject(xmlDB);
                    xmlDB.CursorClose();
                    ArrayList lstVue = new ArrayList();
                    ArrayList lstLayer = null;

                    if (oCnxBase.CBRecherche("SELECT GuidVue, NomVue, GuidEnvironnement, GuidVueInf, TypeVue.GuidTypeVue, NomTypeVue FROM Vue, TypeVue WHERE Vue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidApplication='" + sGuidApplication + "' ORDER BY NomTypeVue"))
                        while (oCnxBase.Reader.Read())
                        {
                            string sEnv = "", sGuidVueInf = "";
                            if (!oCnxBase.Reader.IsDBNull(2)) sEnv = oCnxBase.Reader.GetString(2);
                            if (!oCnxBase.Reader.IsDBNull(3)) sGuidVueInf = oCnxBase.Reader.GetString(3);
                            lstVue.Add(oCnxBase.Reader.GetString(0) + "," + oCnxBase.Reader.GetString(1) + "," + sEnv + "," + sGuidVueInf + "," + oCnxBase.Reader.GetString(4) + "," + oCnxBase.Reader.GetString(5));
                        }
                    oCnxBase.CBReaderClose();

                    if (oCnxBase.CBRecherche("SELECT GuidLayer, NomLayer FROM Layer WHERE GuidApplication='" + sGuidApplication + "'"))
                    {
                        lstLayer = new ArrayList();
                        while (oCnxBase.Reader.Read()) lstLayer.Add(oCnxBase.Reader.GetString(0) + "," + oCnxBase.Reader.GetString(1));
                    }
                    oCnxBase.CBReaderClose();

                    xmlDB.CursorClose();
                    oCnxBase.CreaXmlApplication(xmlDB, sGuidApplication, lstVue, lstLayer);

                    return xmlDB;
                    //xmlDB.docXml.Save(Parent.GetFullPath((string)cbGuidApplication.Items[cbApplication.SelectedIndex]) + "\\" + (string)cbApplication.Items[cbApplication.SelectedIndex] + "Serveur.xml");
                }
                xmlDB.CursorClose();
            }

            return null;
        }*/

        public delegate void XMLEXECACTIONELFROMATT(XmlElement parent, XmlElement el1, XmlElement el2);
        public void XmlExecActionElFromAtt(XmlElement parent, string sAtt, string sValue, XmlElement el, XMLEXECACTIONELFROMATT fonc )
        {
            IEnumerator ienum = parent.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    XmlElement elCur = (XmlElement)Node;
                    XmlExecActionElFromAtt((XmlElement)Node, sAtt, sValue, el, fonc);
                    if (elCur.GetAttribute(sAtt) == sValue) 
                        fonc(parent, elCur, el);
                    //if (elCur.GetAttribute(sAtt) == el.GetAttribute(sAtt)) fonc(parent, elCur);
                        //MessageBox.Show(elCur.ToString());
                }
            }
        }

        

        public delegate void XMLEXECACTIONELFROMNAME(XmlElement parent, XmlElement el1, XmlElement el2);
        public void XmlExecActionElFromName(XmlElement parent, string sValue, XmlElement el, XMLEXECACTIONELFROMNAME fonc)
        {
            IEnumerator ienum = parent.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    XmlElement elCur = (XmlElement)Node;
                    XmlExecActionElFromName((XmlElement)Node, sValue, el, fonc);
                    if (elCur.Name == sValue)
                        fonc(parent, elCur, el);
                }
            }
        }

        public void XmlCreatElKeyNode(XmlDocument xmlDoc, XmlElement elParent, string sKey, string sNom)
        {
            XmlElement el = XmlCreatEl(xmlDoc, elParent, sKey);
            XmlElement elAtts = XmlGetFirstElFromParent(el, "Attributs");
            XmlSetAttFromEl(xmlDoc, elAtts, "KeyNode", "s", sKey);
            XmlSetAttFromEl(xmlDoc, elAtts, "NomNode", "s", sNom);

        }

        public XmlElement XmlCreatEl(XmlDocument xmlDoc, XmlElement elParent, string sNom)
        {
            XmlElement el = xmlDoc.CreateElement(sNom);
            XmlElement elAtts = xmlDoc.CreateElement("Attributs");
            el.AppendChild(elAtts); elParent.AppendChild(el);
            return el;
        }

        public string XmlGetAttValueAFromAttValueB(XmlElement parent, string sAttA, string sAttB, string sValue)
        {
            XmlElement el = XmlFindElFromAtt(parent, sAttB, sValue);
            if (el != null)
            {
                return el.GetAttribute(sAttA);
            }
            return "";
        }

        public XmlElement XmlFindElFromAtt(XmlElement parent, string sAtt, string sValue)
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
                    elCur = XmlFindElFromAtt((XmlElement)Node, sAtt, sValue);
                    if (elCur != null) return elCur;
                }
            }
            return null;
        }

        /*public XmlElement XmlFindElFromAttValue(XmlElement elCur, string sAtt, string sValue)
        {
            IEnumerator ienum = elCur.GetEnumerator();
            XmlNode Node;

            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    XmlElement el = (XmlElement)Node;
                    XmlAttributeCollection lstAtt = el.Attributes;
                    if (lstAtt[sAtt] != null && lstAtt[sAtt].Value == sValue) return el;
                }
            }
            return null;
        }*/
        
        public XmlElement XmlGetFirstElFromParent(XmlElement Parent, string sLibNode)
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

        public string XmlGetProppertyValueFirstElFromParent(XmlElement Parent, string sLibNode, string sPropertyNode)
        {
            IEnumerator ienum = Parent.GetEnumerator();
            XmlNode Node;

            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    if (Node.Name == sLibNode)
                        return ((XmlElement)Node).GetAttribute(sPropertyNode);
                }
            }

            return "";
        }

        public void XmlSetAttFromEl(XmlDocument xmlDoc, XmlElement Parent , string sAtt, string sType, string sValue)
        {
            XmlElement el = xmlDoc.CreateElement("Attribut");
            el.SetAttribute("Nom", sAtt);
            el.SetAttribute("Type", sType);
            el.SetAttribute("Value", sValue);
            Parent.AppendChild(el);
        }

        public ArrayList GetNode(XmlNode NodeRoot, string SearchName)
        {
            IEnumerator ienum = NodeRoot.GetEnumerator();
            XmlNode Node;
            ArrayList aNode = new ArrayList();
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element)
                    if (Node.Name == SearchName) aNode.Add(Node);
            }

            return aNode;
        }

        public void InitCadreRef1(TreeNodeCollection tn, string guidParent)
        {
            ArrayList guidCadreRef = new ArrayList();
            ArrayList NomCadreRef = new ArrayList();
            string sSelect = "";

            //sSelect = "Select GuidCadreRef, NomCadreRef FROM CadreRef WHERE GuidParent='" + guidParent + "' AND (TypeCadreRef='" + switchTechFonc + "' OR TypeCadreRef IS NULL)";
            sSelect = "Select GuidCadreRefFonc, NomCadreRefFonc FROM CadreRefFonc WHERE GuidParentFonc='" + guidParent + "'";

            if (oCnxBase.CBRecherche(sSelect))
            {
                while (oCnxBase.Reader.Read())
                {
                    guidCadreRef.Add((object)oCnxBase.Reader.GetString(0));
                    NomCadreRef.Add((object)oCnxBase.Reader.GetString(1));
                }
            }
            oCnxBase.CBReaderClose();
            for (int i = 0; i < guidCadreRef.Count; i++)
            {
                tn.Add((string)guidCadreRef[i], (string)NomCadreRef[i]);
                sSelect = "Select GuidApplication, NomApplication FROM Application WHERE GuidCadreRef='" + (string)guidCadreRef[i] + "'";
                oCnxBase.CBRecherche(sSelect);
                while (oCnxBase.Reader.Read())
                {
                    tn[tn.Count - 1].Nodes.Add(oCnxBase.Reader.GetString(0) + "," + (int)DrawArea.DrawToolType.Application, oCnxBase.Reader.GetString(1));
                    Font fontpro = new Font("arial", 8);
                    tn[tn.Count - 1].Nodes[tn[tn.Count - 1].Nodes.Count - 1].NodeFont = new Font(fontpro, FontStyle.Bold);
                    tn[tn.Count - 1].Nodes[tn[tn.Count - 1].Nodes.Count - 1].ForeColor = Color.Blue;
                }
                oCnxBase.CBReaderClose();
                InitCadreRef1(tn[tn.Count - 1].Nodes, (string)guidCadreRef[i]);
            }
        }

        private void NomApplication_Click(object sender, EventArgs e)
        {
            if (NomApplication.Text == "Appli") {
                NomApplication.Text = "Trig";
                InitCbApplication();
            } else {
                NomApplication.Text = "Appli";
                InitCbApplication();
            }
        }

        private void bOpVue_Click(object sender, EventArgs e)
        {
            if (bOpVue.Text == "Creat Vue")
            {
                FormPropVue fpv = new FormPropVue(this, null, wkApp, cbOpVue.SelectedIndex);
                fpv.ShowDialog(this);
            }
            else if (bOpVue.Text == "Del Vue")
            {
                string msg = "Voulez-vous supprimer la vue : " + cbVue.Items[cbVue.SelectedIndex].ToString() + " (" + cbGuidVue.Items[cbVue.SelectedIndex].ToString() + ")\n";
                msg += "Voulez-vous continuer?";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                result = MessageBox.Show(msg, "suppression", buttons);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    oCnxBase.CBWrite("DELETE FROM Vue Where GuidVue = '" + cbGuidVue.Items[cbVue.SelectedIndex].ToString() + "'");
                    InitCbApplication();
                }
            }
            else
            {
                FormPropVue fpv = new FormPropVue(this, cbGuidVue.Items[cbVue.SelectedIndex].ToString(), wkApp, cbOpVue.SelectedIndex);
                fpv.ShowDialog(this);             
            }
        }

        private void bOpApp_Click(object sender, EventArgs e)
        {
            if (bOpApp.Text == "Creat App")
            {
                FormPropApp fpa = new FormPropApp(this, null);
                fpa.ShowDialog(this);
            }
            else
            {
                if (cbApplication.SelectedIndex != -1)
                {
                    FormPropApp fpa = new FormPropApp(this, (string) cbGuidApplication.Items[cbApplication.SelectedIndex], cbOpApp.SelectedIndex);
                    fpa.ShowDialog(this);
                }
            } 
        }

        private void bLayer_Click(object sender, EventArgs e)
        {
            if(wkApp!=null) wkApp.SetLayers();
        }

        private ArrayList GetLstEffectifServerByCadre(string sGuidNode, ArrayList lstCriteres)
        {
            ArrayList LstEffectif = new ArrayList();
            if (oCnxBase.CBRecherche("Select Distinct ServerPhy.GuidServerPhy, NomServerPhy From Vue, DansVue, ServerPhy, GServerPhy, Application Where DansVue.GuidGVue=Vue.GuidGVue and GuidObjet=GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and  Vue.GuidAppVersion=Application.GuidAppVersion and Application.GuidCadreRef = '" + sGuidNode + "'"))
            {
                while (oCnxBase.Reader.Read())
                {
                    LstEffectif.Add(new Effectif(this, oCnxBase.Reader.GetString(0), oCnxBase.Reader.GetString(1), lstCriteres, 0));
                }
            }
            oCnxBase.CBReaderClose();

            return LstEffectif;
        }
        private ArrayList GetLstEffectifAppByCadre(string sGuidNode, ArrayList lstCriteres)
        {
            ArrayList LstEffectif = new ArrayList();

            if (oCnxBase.CBRecherche("Select GuidApplication, NomApplication FROM Application WHERE GuidCadreRef = '" + sGuidNode + "'"))
            {
                while (oCnxBase.Reader.Read())
                {
                    LstEffectif.Add(new Effectif(this, oCnxBase.Reader.GetString(0), oCnxBase.Reader.GetString(1), lstCriteres, 0));
                }
            }
            oCnxBase.CBReaderClose();

            return LstEffectif;
        }

        private ArrayList GetLstEffectifTechnoByProduct(string sGuidNode, ArrayList lstCriteres)
        {
            //double dDay = DateTime.Now.ToOADate();
            ArrayList LstEffectif = new ArrayList();

            if (oCnxBase.CBRecherche("Select GuidTechnoRef, NomTechnoRef FROM Produit, TechnoRef WHERE TechnoRef.GuidProduit = Produit.GuidProduit and GuidCadreRef='" + sGuidNode + "'"))
            //if (oCnxBase.CBRecherche("Select GuidTechnoRef, NomTechnoRef FROM Produit, TechnoRef, IndicatorLink WHERE TechnoRef.GuidProduit = Produit.GuidProduit and TechnoRef.GuidTechnoRef = IndicatorLink.GuidObjet and IndicatorLink.GuidIndicator='b00b12bd-a447-47e6-92f6-e3b76ad22830' and ValIndicator <= '" + dDay + "' and GuidCadreRef='" + sGuidNode + "'"))
            {
                while (oCnxBase.Reader.Read())
                {
                    LstEffectif.Add(new Effectif(this, oCnxBase.Reader.GetString(0), oCnxBase.Reader.GetString(1), lstCriteres, 0));
                }
            }
            oCnxBase.CBReaderClose();

            return LstEffectif;
        }

        private ArrayList GetLstEffectif(TreeNode tNode, Form1.rbTypeRecherche rbTypeRech, ArrayList lstCriteres)
        {
            ArrayList LstEffectif = new ArrayList();

            for (int i = 0; i < tNode.Nodes.Count; i++)
            {
                ArrayList LstEffectifChild = new ArrayList();
                LstEffectifChild = GetLstEffectif(tNode.Nodes[i], rbTypeRech, lstCriteres);
                for (int j = 0; j < LstEffectifChild.Count; j ++) LstEffectif.Add(LstEffectifChild[j]); 
            }
            if (tNode.Nodes.Count == 0) LstEffectif.Add(new Effectif(this, tNode.Name, tNode.Text, lstCriteres, 0));
            return LstEffectif;
        }

        private ArrayList GetLstEffectif(string sGuidNode, Form1.rbTypeRecherche rbTypeRech, ArrayList lstCriteres)
        {
            ArrayList LstEffectif = new ArrayList();
            ArrayList LstCadreRef = new ArrayList();
            string sSelect = null;

            {
                ArrayList lstEffectifTemp = null;
                if ((rbTypeRech & Form1.rbTypeRecherche.Application) != 0)
                {
                    sSelect = "Select GuidCadreRefFonc FROM CadreRefFonc WHERE GuidParentFonc='" + sGuidNode + "'";
                    lstEffectifTemp = GetLstEffectifAppByCadre(sGuidNode, lstCriteres);
                }
                /* else if ((rbTypeRech & Form1.rbTypeRecherche.Server) != 0)
                {
                    sSelect = "Select GuidCadreRefFonc FROM CadreRefFonc WHERE GuidParentFonc='" + sGuidNode + "'";
                    lstEffectifTemp = GetLstEffectifServerByCadre(sGuidNode, lstCriteres);
                }*/
                else if ((rbTypeRech & Form1.rbTypeRecherche.Techno) != 0)
                {
                    sSelect = "Select GuidCadreRef FROM CadreRef WHERE GuidParent='" + sGuidNode + "'"; //software et hard  AND (TypeCadreRef='" + switchTechFonc + "' OR TypeCadreRef IS NULL)";
                                                                                                        //"Select GuidCadreRefApp, NomCadreRefApp FROM CadreRefApp WHERE GuidParentApp='" + guidParent + "'";
                                                                                                        //"Select GuidCadreRefFonc, NomCadreRefFonc FROM CadreRefFonc WHERE GuidParentFonc='" + guidParent + "'";
                    lstEffectifTemp = GetLstEffectifTechnoByProduct(sGuidNode, lstCriteres);
                }
                for (int j = 0; j < lstEffectifTemp.Count; j++) LstEffectif.Add(lstEffectifTemp[j]);
            }

            if (oCnxBase.CBRecherche(sSelect))
            {
                while (oCnxBase.Reader.Read())
                    LstCadreRef.Add((object)oCnxBase.Reader.GetString(0));
            }
            oCnxBase.CBReaderClose();
            for (int i = 0; i < LstCadreRef.Count; i++)
            {
                ArrayList lstEffectifTemp = GetLstEffectif((string)LstCadreRef[i], rbTypeRech, lstCriteres);
                for (int j = 0; j < lstEffectifTemp.Count; j++) LstEffectif.Add(lstEffectifTemp[j]);
            }

            return LstEffectif;
        }

        public void CreatLstNiveau(FormProgress fp, Tool[] aTool, ArrayList lstEffectifs, ArrayList lstCriteres, bool bOption)
        {
            oCnxBase.SWwriteLog(2, "Les calculs des " + lstEffectifs.Count + " Effectifs", true);


            fp.initbar(lstEffectifs.Count);
            for (int i = 0; i < lstEffectifs.Count; i++)
            {
                Effectif oEffectif = (Effectif)lstEffectifs[i];
                oCnxBase.SWwriteLog(4, "calcul de l'éffectif  " + oEffectif.NomEffectif, true);
                fp.stepbar(oEffectif.NomEffectif, 0);
                for (int k = 0; k < aTool.Length; k++)
                {
                    if(aTool[k] != null) ((Tool)aTool[k]).EvalCriteres(oEffectif, bOption);
                }
                for (int j = 0; j < lstCriteres.Count; j++)
                {
                    Critere oCritere = (Critere)lstCriteres[j];
                    Niveau oNiv = (Niveau)oEffectif.lstNivEffectif[j];
                    if (oNiv != null)
                    {
                        if (i == 0)
                        {
                            oCritere.dMax = oNiv.Val;
                            oCritere.dMin = oNiv.Val;
                        }
                        else
                        {
                            if (oCritere.dMax < oNiv.Val) oCritere.dMax = oNiv.Val;
                            if (oCritere.dMin > oNiv.Val) oCritere.dMin = oNiv.Val;
                        }
                    }
                }

                /*
                                if ((rn & RetourNiveau.Absisse) != 0 && (rn & RetourNiveau.Ordonnee) != 0)
                                {
                                    DrawPtNiveau dpn;
                                    dpn = (DrawPtNiveau)LstPtNiveau[i];

                                    Owner.Owner.oCnxBase.SWwriteLog(6, "le point Niveau : " + ((DrawPtNiveau)LstPtNiveau[i]).Texte + " possede une abscisse : " + dpn.NivAbs[0].Val + " et une ordonnée : " + dpn.NivOrd[0].Val, true);

                                    if (dpn.NivAbs[1] != null)
                                    {
                                        dpn.SetValueFromName("NivAbs", dpn.NivAbs[0].CalculWithRef(dpn.NivAbs[1].Val));
                                        dpn.SetValueFromName("IconStatusX", dpn.NivAbs[0].IconStatus(dpn.NivAbs[1].Val));
                                    }
                                    else dpn.SetValueFromName("NivAbs", dpn.NivAbs[0].Val);
                                    if (dpn.NivOrd[1] != null)
                                    {
                                        dpn.SetValueFromName("NivOrd", dpn.NivOrd[0].CalculWithRef(dpn.NivOrd[1].Val));
                                        dpn.SetValueFromName("IconStatusY", dpn.NivOrd[0].IconStatus(dpn.NivOrd[1].Val));
                                    }
                                    else dpn.SetValueFromName("NivOrd", dpn.NivOrd[0].Val);
                                    lstPtNiveauOK.Add(LstPtNiveau[i]);
                                }*/
            }
            oCnxBase.SWwriteLog(2, "", true);
        }

        public void CalcAgregaEffectif(ArrayList lstEffectif, ArrayList lstCriteres)
        {
            for (int i = 0; i < lstEffectif.Count; i++)
            {
                Effectif oEff = (Effectif)lstEffectif[i];
                oEff.Val = 0;
                for (int j = 0; j < lstCriteres.Count; j++)
                {
                    Critere oCri = (Critere)lstCriteres[j];
                    if(oCri.Calc)
                    {
                        Niveau oNiv = (Niveau)oEff.lstNivEffectif[j];
                        double dRef = 0;
                        if (oNiv.GetAlertMin() == "ValMin") dRef = oCri.dMin; else dRef = oCri.dMax;
                        oEff.Val += Math.Pow((dRef - oNiv.Val), 2);
                    }
                }
            }
            IComparer Comp = new SortEffectif();
            lstEffectif.Sort(Comp);
        }

        public ArrayList GetApplications(ArrayList lstGuids, ArrayList lstCriteres)
        {
            ArrayList lstEffectifsApp = new ArrayList();
            for (int i = 0; i < lstGuids.Count; i++)
            {
                if (oCnxBase.CBRecherche("SELECT DISTINCT Application.GuidApplication, NomApplication FROM Application, Vue, DansVue, GServer, Server, ServerTypeLink, ServerType, Techno WHERE Application.GuidAppVersion=Vue.GuidAppVersion AND Vue.GuidGVue=DansVue.GuidGVue AND DansVue.GuidObjet=GServer.GuidGServer AND GServer.GuidServer=Server.GuidServer AND Server.GuidServer=ServerTypeLink.GuidServer AND ServerTypeLink.GuidServerType=ServerType.GuidServerType AND ServerType.GuidServerType=Techno.GuidTechnoHost AND Techno.GuidTechnoRef='" + (string) lstGuids[i] + "'"))
                {
                    while (oCnxBase.Reader.Read())
                    {
                        Effectif oEffApp = new Effectif(this, oCnxBase.Reader.GetString(0), oCnxBase.Reader.GetString(1), lstCriteres, 0);
                        if (oEffApp.FindEffectifFromLST(lstEffectifsApp) == -1) lstEffectifsApp.Add(oEffApp);
                    }
                }
                oCnxBase.CBReaderClose();
            } 
            return lstEffectifsApp;
        }

        public void report(ArrayList lstEffectif, ArrayList lstCriteres, Form1.rbTypeRecherche rbTypeRech)
        {
            FormProgress fp = new FormProgress(this, false);
            fp.Show(this);
            oCnxBase.SWopen(@"C:\_logfiles\test.txt");
            oCnxBase.SWwriteLog(0, "debut calcul Niveau", true);
            for (int i = 0; i < lstCriteres.Count; i++)
            {
                Critere oCritere = (Critere)lstCriteres[i];
                oCnxBase.SWwriteLog(0, "Critere " + i + " : " + oCritere.NomCritere, false);
            }
            oCnxBase.SWwriteLog(0, "", true);

            if (lstEffectif != null && lstEffectif.Count >= 1)
            {

                if ((rbTypeRech & Form1.rbTypeRecherche.Application) != 0)
                {
                    if (lstEffectif.Count == 0)
                    {
                        //CreatLstNiveauApp(drawArea.tools, lstPtNiveauOK);
                    }
                    else
                    {
                        oCnxBase.SWwriteLog(0, "Initialisation des deleges sur chacun des objets", true);
                        for (int i = 0; i < drawArea.tools.Length; i++)
                        {
                            if(drawArea.tools[i] != null) ((Tool)drawArea.tools[i]).EvalCriteres = new Tool.EVALCRITERES(((Tool)drawArea.tools[i]).CreatNiveauForApp);
                        }
                        oCnxBase.SWwriteLog(0, "", true);
                        CreatLstNiveau(fp, drawArea.tools, lstEffectif, lstCriteres, false);
                    }
                }
                else if ((rbTypeRech & Form1.rbTypeRecherche.Server) != 0)
                {
                    oCnxBase.SWwriteLog(0, "Initialisation des deleges sur chacun des objets", true);
                    for (int i = 0; i < drawArea.tools.Length; i++)
                    {
                        if (drawArea.tools[i] != null) ((Tool)drawArea.tools[i]).EvalCriteres = new Tool.EVALCRITERES(((Tool)drawArea.tools[i]).CreatNiveauForServer);
                    }
                    oCnxBase.SWwriteLog(0, "", true);
                    CreatLstNiveau(fp, drawArea.tools, lstEffectif, lstCriteres, false);
                }
                else if ((rbTypeRech & Form1.rbTypeRecherche.Techno) != 0)
                {
                    oCnxBase.SWwriteLog(0, "Initialisation des deleges sur chacun des objets", true);
                    for (int i = 0; i < drawArea.tools.Length; i++)
                    {
                        if(drawArea.tools[i] != null)
                            ((Tool)drawArea.tools[i]).EvalCriteres = new Tool.EVALCRITERES(((Tool)drawArea.tools[i]).CreatNiveauForTechno);
                    }
                    oCnxBase.SWwriteLog(0, "", true);
                    CreatLstNiveau(fp, drawArea.tools, lstEffectif, lstCriteres, false);
                }
            }

            //check des indicateurs
            if (lstEffectif != null && lstEffectif.Count >= 1)
            {
                for (int i = 0; i < lstEffectif.Count; i++)
                {
                    Effectif e = (Effectif)lstEffectif[i];
                    for (int j = 0; j < e.lstNivEffectif.Count; j++)
                    {
                        Niveau n = (Niveau)e.lstNivEffectif[j];
                        if (n != null)
                        {
                            if (!n.CheckValidite())
                            {
                                lstEffectif.RemoveAt(i);
                                i -= 1;
                                j = e.lstNivEffectif.Count;
                            }
                        }
                    }
                }
            }

            oCnxBase.SWclose();
            fp.Close();

        }
        
        public ArrayList report(TreeNode tNode, string sGuidNode, ArrayList lstCriteres, Form1.rbTypeRecherche rbTypeRech)
        {
            ArrayList lstEffectif = null;

            if ((rbTypeRech & Form1.rbTypeRecherche.Server) != 0)
            {
                lstEffectif = GetLstEffectif(tNode, rbTypeRech, lstCriteres);

            }
            else lstEffectif = GetLstEffectif(sGuidNode, rbTypeRech, lstCriteres);



            report(lstEffectif, lstCriteres, rbTypeRech);

            return lstEffectif;
        }

        private void bDescriptionApp_Click(object sender, EventArgs e)
        {
            if (cbApplication.SelectedIndex != -1 && oCureo == null)
            {
                ExpObj eo = new ExpObj(new Guid((string)cbGuidApplication.Items[cbApplication.SelectedIndex]), cbApplication.SelectedItem.ToString(), DrawArea.DrawToolType.Application);
                FormExplorObj feo = new FormExplorObj(this);
                feo.init(eo);
                oCureo = null;
            }
        }

        private void bDescriptionVue_Click(object sender, EventArgs e)
        {
            if (cbVue.SelectedIndex != -1 && oCureo == null)
            {
                ExpObj eo = new ExpObj(new Guid((string)cbGuidVue.Items[cbVue.SelectedIndex]), cbVue.SelectedItem.ToString(), DrawArea.DrawToolType.Vue);
                FormExplorObj feo = new FormExplorObj(this);
                feo.init(eo);
                oCureo = null;
            }
        }


        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
        //                                         - Fonctions Transverses-
        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
        public bool isNum(char c)
        {
            try {
                int i = Convert.ToInt32(new string(c, 1));
                return true;
            }
            catch 
            {
                return false;
            }
        }
        

        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
        //                                         - API CallBack-
        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------

        public void TVAddNodeFromJson(string sTVName, byte[] d)
        {
            tvObjet.Nodes.Add(sTVName, sTVName);
            TreeNodeCollection tn = tvObjet.Nodes[tvObjet.Nodes.Count - 1].Nodes;

            string data = System.Text.Encoding.UTF8.GetString(d);

            LstNodes lstNodes = JsonConvert.DeserializeObject<LstNodes>(data);

            for (int i = 0; i < lstNodes.nodes.Count; i++)
            {
                string[] aValue = new string[2];
                int j = 0;
                foreach (KeyValuePair<string, object> o in lstNodes.nodes[i].free)
                    aValue[j++] = (string)o.Value;
                tn.Add(aValue[0], aValue[1]);
            }
        }

        public void TVAddLinkFromJson(string sTVName, byte[] d)
        {
            tvObjet.Nodes.Add(sTVName, sTVName);
            TreeNodeCollection tn = tvObjet.Nodes[tvObjet.Nodes.Count - 1].Nodes;

            string data = System.Text.Encoding.UTF8.GetString(d);

            LstLinks lstLinks = JsonConvert.DeserializeObject<LstLinks>(data);

            for (int i = 0; i < lstLinks.links.Count; i++)
            {
                string[] aValue = new string[2];
                int j = 0;
                foreach (KeyValuePair<string, object> o in lstLinks.links[i].free)
                    aValue[j++] = (string)o.Value;
                tn.Add(aValue[0], aValue[1]);
            }
        }

        public void webClient_GetApp(object sender, System.Net.DownloadDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }

            if (e.Result != null && e.Result.Length > 0)
            {
                //string data = System.Text.Encoding.Default.GetString(e.Result);
                string data = System.Text.Encoding.UTF8.GetString(e.Result);
                // do things with data here
                ApplicationResp resp = JsonConvert.DeserializeObject<ApplicationResp>(data);
                lstApps = resp.content;
                for (int i = 0; i < lstApps.applications.Count; i++)
                {
                    cbGuidApplication.Items.Add(lstApps.applications[i].guidApplication);
                    cbApplication.Items.Add(lstApps.applications[i].nomApplication);
                }
            }
            else
            {
                MessageBox.Show("No data was downloaded.");
            }
        }

        public void webClient_PutVue(object sender, System.Net.UploadDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }

            if (e.Result != null && e.Result.Length > 0)
            {
                string data = System.Text.Encoding.UTF8.GetString(e.Result);
                // do things with data here
                AppData = JsonConvert.DeserializeObject<ApplicationResp>(data);

                lstVues = AppData.content;

                if (lstVues.applications.Count > 0)
                {
                    if (lstVues.applications[0].appVersions.Count > 0)
                    {
                        if (lstVues.applications[0].appVersions[0].vues.Count > 0)
                        {
                            clVue clVue = lstVues.applications[0].appVersions[0].vues[0];
                            for (int i = 0; i < clVue.nodes.Count; i++)
                            {
                                clNode node = clVue.nodes[i];
                                //Dictionary<string, object> free = node.free;
                                switch (node.typeNode)
                                {
                                    case "module":
                                        drawArea.tools[(int)DrawArea.DrawToolType.Module].CreatObjetFromJson(node.free);
                                        break;
                                    case "appuser":
                                        drawArea.tools[(int)DrawArea.DrawToolType.AppUser].CreatObjetFromJson(node.free);
                                        break;
                                    case "application":
                                        drawArea.tools[(int)DrawArea.DrawToolType.Application].CreatObjetFromJson(node.free);
                                        break;
                                    case "maincomposant":
                                        drawArea.tools[(int)DrawArea.DrawToolType.MainComposant].CreatObjetFromJson(node.free);
                                        for (int j = 0; j < node.nodes.Count; j++)
                                        {
                                            clNode childNode = node.nodes[j];
                                            switch (childNode.typeNode)
                                            {
                                                case "compfonc":
                                                    drawArea.tools[(int)DrawArea.DrawToolType.CompFonc].CreatObjetFromJson(childNode.free);
                                                    break;
                                                case "composant":
                                                    drawArea.tools[(int)DrawArea.DrawToolType.Composant].CreatObjetFromJson(childNode.free);
                                                    break;
                                                case "base":
                                                    drawArea.tools[(int)DrawArea.DrawToolType.Base].CreatObjetFromJson(childNode.free);
                                                    break;
                                                case "interface":
                                                    drawArea.tools[(int)DrawArea.DrawToolType.Interface].CreatObjetFromJson(childNode.free);
                                                    break;
                                                case "file":
                                                    drawArea.tools[(int)DrawArea.DrawToolType.File].CreatObjetFromJson(childNode.free);
                                                    break;
                                            }
                                        }
                                        break;
                                    case "server":
                                        DrawServer ds = (DrawServer)drawArea.tools[(int)DrawArea.DrawToolType.Server].CreatObjetFromJson(node.free);
                                        for (int j = 0; j < node.nodes.Count; j++)
                                        {
                                            clNode childNode = node.nodes[j];
                                            switch (childNode.typeNode)
                                            {
                                                case "servertype":
                                                    drawArea.tools[(int)DrawArea.DrawToolType.ServerType].CreatObjetFromJson(childNode.free);
                                                    for (int k = 0; k < childNode.nodes.Count; k++)
                                                    {
                                                        clNode sschildNode = childNode.nodes[k];
                                                        switch (sschildNode.typeNode)
                                                        {
                                                            case "techno":
                                                                drawArea.tools[(int)DrawArea.DrawToolType.Techno].CreatObjetFromJson(sschildNode.free);
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case "maincomposant":
                                                    drawArea.tools[(int)DrawArea.DrawToolType.MainComposant].CreatObjetFromJson(childNode.free);
                                                    for (int k = 0; k < childNode.nodes.Count; k++)
                                                    {
                                                        clNode sschildNode = childNode.nodes[k];
                                                        switch (sschildNode.typeNode)
                                                        {
                                                            case "servmcomp":
                                                                drawArea.tools[(int)DrawArea.DrawToolType.ServMComp].CreatObjetFromJson(sschildNode.free);
                                                                break;
                                                        }
                                                    }
                                                    break;
                                            }
                                        }
                                        ds.AligneObjet();

                                        break;
                                }
                            }
                            for (int i = 0; i < clVue.links.Count; i++)
                            {
                                clLink link = clVue.links[i];

                                switch (link.typeLink)
                                {
                                    case "link":
                                        drawArea.tools[(int)DrawArea.DrawToolType.Link].CreatObjetFromJson(link.free);
                                        break;
                                    case "techlink":
                                        drawArea.tools[(int)DrawArea.DrawToolType.TechLink].CreatObjetFromJson(link.free);
                                        break;
                                }
                            }
                        }
                    }
                }
                drawArea.Refresh();
            }
            else
            {
                MessageBox.Show("No data was downloaded.");
            }
        }

        public void webClient_GetVue(object sender, System.Net.DownloadDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }

            if (e.Result != null && e.Result.Length > 0)
            {
                //string data = System.Text.Encoding.Default.GetString(e.Result);
                string data = System.Text.Encoding.UTF8.GetString(e.Result);
                // do things with data here
                AppData = JsonConvert.DeserializeObject<ApplicationResp>(data);

                lstVues = AppData.content;

                SaveTest("2cd09ef2-285e-4122-bb67-91aad6535cc0", lstVues);
                if (lstVues.applications.Count > 0)
                {
                    if(lstVues.applications[0].appVersions.Count > 0)
                    {
                        if(lstVues.applications[0].appVersions[0].vues.Count > 0)
                        {
                            clVue clVue = lstVues.applications[0].appVersions[0].vues[0];
                            for (int i = 0; i < clVue.nodes.Count; i++)
                            {
                                clNode node = clVue.nodes[i];
                                //Dictionary<string, object> free = node.free;
                                switch (node.typeNode)
                                {
                                    case "module":
                                        drawArea.tools[(int)DrawArea.DrawToolType.Module].CreatObjetFromJson(node.free);
                                        break;
                                    case  "appuser" :
                                        drawArea.tools[(int)DrawArea.DrawToolType.AppUser].CreatObjetFromJson(node.free);
                                        break;
                                    case "application":
                                        drawArea.tools[(int)DrawArea.DrawToolType.Application].CreatObjetFromJson(node.free);
                                        break;
                                    case "maincomposant":
                                        drawArea.tools[(int)DrawArea.DrawToolType.MainComposant].CreatObjetFromJson(node.free);
                                        for(int j=0; j< node.nodes.Count; j++)
                                        {
                                            clNode childNode = node.nodes[j];
                                            switch (childNode.typeNode)
                                            {
                                                case "compfonc":
                                                    drawArea.tools[(int)DrawArea.DrawToolType.CompFonc].CreatObjetFromJson(childNode.free);
                                                    break;
                                                case "composant":
                                                    drawArea.tools[(int)DrawArea.DrawToolType.Composant].CreatObjetFromJson(childNode.free);
                                                    break;
                                                case "base":
                                                    drawArea.tools[(int)DrawArea.DrawToolType.Base].CreatObjetFromJson(childNode.free);
                                                    break;
                                                case "interface":
                                                    drawArea.tools[(int)DrawArea.DrawToolType.Interface].CreatObjetFromJson(childNode.free);
                                                    break;
                                                case "file":
                                                    drawArea.tools[(int)DrawArea.DrawToolType.File].CreatObjetFromJson(childNode.free);
                                                    break;
                                            }
                                        }
                                        break;
                                    case "server":
                                        DrawServer ds = (DrawServer) drawArea.tools[(int)DrawArea.DrawToolType.Server].CreatObjetFromJson(node.free);
                                        for (int j = 0; j < node.nodes.Count; j++)
                                        {
                                            clNode childNode = node.nodes[j];
                                            switch (childNode.typeNode)
                                            {
                                                case "servertype":
                                                    drawArea.tools[(int)DrawArea.DrawToolType.ServerType].CreatObjetFromJson(childNode.free);
                                                    for (int k = 0; k < childNode.nodes.Count; k++)
                                                    {
                                                        clNode sschildNode = childNode.nodes[k];
                                                        switch (sschildNode.typeNode)
                                                        {
                                                            case "techno":
                                                                drawArea.tools[(int)DrawArea.DrawToolType.Techno].CreatObjetFromJson(sschildNode.free);
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case "maincomposant":
                                                    drawArea.tools[(int)DrawArea.DrawToolType.MainComposant].CreatObjetFromJson(childNode.free);
                                                    for (int k = 0; k < childNode.nodes.Count; k++)
                                                    {
                                                        clNode sschildNode = childNode.nodes[k];
                                                        switch (sschildNode.typeNode)
                                                        {
                                                            case "servmcomp":
                                                                drawArea.tools[(int)DrawArea.DrawToolType.ServMComp].CreatObjetFromJson(sschildNode.free);
                                                                break;
                                                        }
                                                    }
                                                    break;
                                            }
                                        }
                                        ds.AligneObjet();
                                        
                                        break;
                                }
                            }
                            for (int i = 0; i < clVue.links.Count; i++)
                            {
                                clLink link = clVue.links[i];

                                switch (link.typeLink)
                                {
                                    case "link":
                                        drawArea.tools[(int)DrawArea.DrawToolType.Link].CreatObjetFromJson(link.free);
                                        break;
                                    case "techlink":
                                        drawArea.tools[(int)DrawArea.DrawToolType.TechLink].CreatObjetFromJson(link.free);
                                        break;
                                }
                            }
                        }
                    }
                }
                drawArea.Refresh();
            }
            else
            {
                MessageBox.Show("No data was downloaded.");
            }
        }

        public void webClient_TVAddNodeLSApps(object sender, System.Net.DownloadDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }

            if (e.Result != null && e.Result.Length > 0) TVAddNodeFromJson("LSApplication", e.Result);
        }

        public void webClient_TVAddNodeApps(object sender, System.Net.DownloadDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }

            if (e.Result != null && e.Result.Length > 0) TVAddNodeFromJson("Application", e.Result);
        }

        public void webClient_TVAddNodeMainComposants(object sender, System.Net.DownloadDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }

            if (e.Result != null && e.Result.Length > 0) TVAddNodeFromJson("MainComposant", e.Result);
        }

        public void webClient_TVAddNodeUsers(object sender, System.Net.DownloadDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }

            if (e.Result != null && e.Result.Length > 0) TVAddNodeFromJson( "AppUser" , e.Result);
         }

        public void webClient_TVAddNodeModules(object sender, System.Net.DownloadDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }

            if (e.Result != null && e.Result.Length > 0) TVAddNodeFromJson("Module", e.Result);
        }

        public void webClient_TVAddNodeLinks(object sender, System.Net.DownloadDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }

            if (e.Result != null && e.Result.Length > 0) TVAddLinkFromJson("Link", e.Result);
        }

        public void webClient_GetTokenCloud(object sender, System.Net.UploadValuesCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }
            if (e.Result != null && e.Result.Length > 0)
            {
                string data = System.Text.Encoding.UTF8.GetString(e.Result);
                dynamic json = JsonConvert.DeserializeObject(data);
                result_CloudToken(json);
                
            }
        }

        private void webClient_GetClusters(object sender, System.Net.DownloadDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }

            if (e.Result != null && e.Result.Length > 0)
            {
                string data = System.Text.Encoding.UTF8.GetString(e.Result);
            }
        }

        public void webClient_GetApplication(object sender, System.Net.DownloadDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }

            if (e.Result != null && e.Result.Length > 0) TVAddNodeFromJson("Application", e.Result);
        }


        // -------------------------------------------- FIN -------------------------------------
    }

    public class cObj
    {
        public string Guid { get; set; }
        public string Name { get; set; }
        public cObj This { get; set; }

        public cObj(string sGuid, string sNom)
        {
            Name = sNom;
            Guid = sGuid;
            This = this;
        }
    }

    public class Compte
    {
        static string auth = "http://127.0.0.1:8080/auth/realms/Test";
        static string api = "http://127.0.0.1:8080/auth/realms/Test/api";
        static OidcClient oidcClient;
        static HttpClient apiClient = new HttpClient { BaseAddress = new Uri(api) };

        static string guidCompte, idCompte, emailCompte, nomCompte, prenomCompte;
        static string identityToken, accessToken, refreshToken;
        public static List<string> lstRoles = new List<string>();
        public static List<string> lstCompteRigths = new List<string>();
        static List<string[]> lstHabilitations = new List<string[]>();

        public static string guid
        {
            get { return guidCompte; }
            set { guidCompte = value; }
        }

        public static string id
        {
            get { return idCompte; }
            set { idCompte = value;}
        }

        public static void InitCompteRights()
        {
            //string exist = lstRoles.Find(el => el == (string)o);
            foreach (var elr in lstRoles) {
                List<string[]> lst = lstHabilitations.FindAll(el => el[0] == elr);
                foreach(var elh in lst) lstCompteRigths.Add(elh[1]);
            }
        }
        public static void InitHabilitations(CnxBase cnx)
        {

#if HABILITATION
            string[] aHabitidation = new string[2];
            aHabitidation[0] = "78201911-53be-4074-9d7a-6ff2cbf809aa";
            aHabitidation[1] = "05915b4d-6eaf-4ed1-8c07-c31a4527a2b2";
            lstHabilitations.Add(aHabitidation);
            aHabitidation[1] = "5416a957-e669-4403-be06-6f7a71f616ce";
            lstHabilitations.Add(aHabitidation);
            aHabitidation[1] = "5fa9fa6e-2caa-42dc-975f-12f0aca7075d";
            lstHabilitations.Add(aHabitidation);
            aHabitidation[1] = "6db2c391-c05e-4cde-aec3-d7ed7a773de7";
            lstHabilitations.Add(aHabitidation);
            aHabitidation[0] = "b3d2b96b-36ad-4114-8ff7-29109d8c0144";
            aHabitidation[1] = "37a0ca71-25a8-46e8-a605-aa14387c5b7c";
            lstHabilitations.Add(aHabitidation);
            aHabitidation[1] = "46883717-5f55-4789-a833-dc10f59385b8";
            lstHabilitations.Add(aHabitidation);
            aHabitidation[1] = "78fc4a02-cfc6-44ae-a59a-4a75952b2604";
            lstHabilitations.Add(aHabitidation);
            aHabitidation[1] = "bedef478-547e-47de-819b-4377a94e78c5";
            lstHabilitations.Add(aHabitidation);
#else            
            
            cnx.CBRecherche("Select GuidRole, GuidHabilitation From Habilitation Order By GuidRole");
            while (cnx.Reader.Read())
            {
                string[] aHabitidation = new string[2];
                aHabitidation[0] = cnx.Reader.GetString(0);
                aHabitidation[1] = cnx.Reader.GetString(1);

                lstHabilitations.Add(aHabitidation);
            }
            cnx.CBReaderClose();
#endif       
        }
        public static void loginOidc()
        {
            Login().GetAwaiter().GetResult();
        }

        public static async Task Login()
        {

            var browser = new SystemBrowser();
            string redirectUri = string.Format($"http://127.0.0.1:9998");

            var options = new OidcClientOptions
            {
                Authority = auth,
                ClientId = "FatClientDrawTools",
                RedirectUri = redirectUri,
                //Scope = "openid roles profile api offline_access",
                Scope = "openid roles",
                ClientSecret = "0b91c43f-c94b-4b4e-9c2d-2a29a453b83b",
                FilterClaims = false,
                Policy = new Policy { RequireAccessTokenHash = false },
                ResponseMode = OidcClientOptions.AuthorizeResponseMode.Redirect,
                Flow = OidcClientOptions.AuthenticationFlow.AuthorizationCode,
                Browser = browser,
                RefreshTokenInnerHttpHandler = new HttpClientHandler()
            };
            var serilog = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .WriteTo.File("c:\\dat\\temp\\log.txt") // .LiterateConsole(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message}{NewLine}{Exception}{NewLine}")
                .CreateLogger();

            options.LoggerFactory.AddSerilog(serilog);

            oidcClient = new OidcClient(options);
            var result = await oidcClient.LoginAsync(new LoginRequest());

            apiClient = new HttpClient(result.RefreshTokenHandler) { BaseAddress = new Uri(api) };

            InitResult(result);
            //await NextSteps(result);
        }

        static void InitResult(LoginResult result)
        {
            if (result.IsError)
            {
                MessageBox.Show("Error: " + result.Error);
                return;
            }

            foreach (var claim in result.User.Claims)
            {
                switch(claim.Type)
                {
                    case "sub":
                        guidCompte = claim.Value;
                        break;
                    case "preferred_username":
                        idCompte = claim.Value;
                        break;
                    case "given_name":
                        prenomCompte = claim.Value;
                        break;
                    case "family_name":
                        nomCompte = claim.Value;
                        break;
                    case "email":
                        emailCompte = claim.Value;
                        break;
                }
            }
            identityToken = result.IdentityToken;
            accessToken = result.AccessToken;
            refreshToken = result?.RefreshToken ?? "none";

            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken tokenS = (JwtSecurityToken)handler.ReadToken(result.AccessToken);
            dynamic json = JsonConvert.DeserializeObject(tokenS.Payload.SerializeToJson());

            JArray ja = (JArray)json.realm_access.roles; // list des profils 
            
            // mettre à jour le compte dans la table

            // créer la liste des roles  en fonction de la liste dess profils transmise via le tableau JArray et la table profil

            //lstRoles.Add("6eaf-4ed1-8c07-c31a4527a2b2");
            //foreach (var el in ja) lstRoles.Add(el.ToString());
            
        }

        public static async void SetRight(Form f)
        {
            await SetRighttoForm(f);
        }

        public static bool GetRighttoObj(object o)
        {
            if (o != null)
            {
                //MessageBox.Show((string)o);
                string exist = lstCompteRigths.Find(el=>el == (string) o);
                if (exist != null) return true;
            }
            else return true;
            return false;
        }

        static async Task SetRighttoForm(Form f)
        {
            foreach (Control ctrl in f.Controls)
            {
                if (!GetRighttoObj(ctrl.Tag))
                    ctrl.Enabled = false;
                else ctrl.Enabled = true;
                if (ctrl.GetType() == typeof(ToolBar))
                {
                    foreach (ToolBarButton bt in ((ToolBar)ctrl).Buttons)
                        if (!GetRighttoObj(bt.Tag)) bt.Enabled = false;
                }
                await SetRighttoControl(ctrl);
            }
            if (f.Menu != null)
            {
                foreach (MenuItem mnu in f.Menu.MenuItems)
                {
                    if (!GetRighttoObj(mnu.Tag)) mnu.Enabled = false;
                    await SetRighttoMnu(mnu);
                }
            }
        }

        static async Task SetRighttoMnu(MenuItem m)
        {
            foreach (MenuItem mnu in m.MenuItems)
            {
                if (!GetRighttoObj(mnu.Tag)) mnu.Enabled = false;
                await SetRighttoMnu(mnu);
            }
        }

        static async Task SetRighttoControl(Control c)
        {
            foreach (Control ctrl in c.Controls)
            {
                if (!GetRighttoObj(ctrl.Tag)) ctrl.Enabled = false;
                await SetRighttoControl(ctrl);
            }
        }

    }

    public class CnxBase
    {
        private OdbcConnection connection;
        private OdbcCommand command;
        private int currentReader;
        private OdbcDataReader[] reader = new OdbcDataReader[3];
        private Form1 parent;
        public ConfDataBase ConfDB;
        public System.IO.StreamWriter sw = null;
        public bool bAttribut;
        
        //private OdbcDataReader reader2;

        public CnxBase(Form1 F)
        {
            bAttribut = true;

            //#if WRITE

            //F.SelectedBase = null;
            //Form fs = new FormCnxBase(F);
            //fs.ShowDialog(F);
            F.SelectedBase = "cmdbRead";
            if (F.SelectedBase != null)
            {
                connection = new OdbcConnection("DSN=" + F.SelectedBase);
                command = new OdbcCommand();
                currentReader = 0;
                command.Connection = connection;
                command.Connection.Open();
                parent = F;
                ConfDB = new ConfDataBase();
            }
        }

        public OdbcConnection Connection
        {
            get
            {
                return connection;
            }
        }

        public string DBLog(string guidObj, string nomObj, string guidAction, string desc)
        {
            string guid = Guid.NewGuid().ToString();
            
            CBWrite("insert into log (guidlog, date, guidobjet, nomobjet, guidaction, guidcompte, nomcompte, description) values ('" + guid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "','" + guidObj + "','" + nomObj + "','" + guidAction + "','" + Compte.guid + "','" + Compte.id + "','" + desc + "')");

            return (guid);
        }

        public void SWopen(string s)
        {
            if (sw != null) MessageBox.Show("Attention, le streamwriter n'est pas null");
            sw = System.IO.File.CreateText(s);
        }

        public void SWwriteLog(int indent, string s, bool l)
        {
            string sIndent = "";

            for (int i=0; i < indent; i++) sIndent += " ";
            sw.WriteLine(DateTime.Now.ToLongTimeString() + sIndent + s);
            if (l) sw.WriteLine(sIndent + "------------------------------");
        }

        public void SWclose()
        {
            sw.Close();
            sw = null;
        }


        public string CmdText
        {
            get
            {
                return command.CommandText;
            }
            set
            {
                command.CommandText = value;
            }
        }
        public OdbcCommand Cmd
        {
            get
            {
                return command;
            }
        }

        public OdbcDataReader Reader
        {
            get
            {
                return reader[currentReader];
            }
            set
            {
                reader[currentReader] = value;
            }
        }

        public List<string> getListTechLinkFromLink(string guid, string guidvue)
        {
            List<string> lstTechLink = new List<string>();
            string sql = "select distinct tlinkrest.guidtechlink from techlinkapp tlinkrest where ";
            sql += "tlinkrest.guidvue = '" + guidvue + "' and tlinkrest.guidlink in (select guidlink from techlinkapp ";
            sql += "where guidtechlink = '" + guid + "' and guidvue = '" + guidvue + "')";
            //sql += "tlinkrest.guidvue = '" + guidvue + "' and tlinkrest.guidlink in (select guidlink from techlink tlinkselect, techlinkapp ";
            //sql += "where tlinkselect.guidtechlink = techlinkapp.guidtechlink and tlinkselect.guidtechlink = '" + guid + "' and ";
            //sql += "techlinkapp.guidvue = '" + guidvue + "')";
            if (CBRecherche(sql))
                while (Reader.Read()) lstTechLink.Add(Reader.GetString(0));
            CBReaderClose();
            return lstTechLink;
        }

        public List<string[]> InitCadreRef()
        {
            List<string[]> lstCadreRef = new List<string[]>();
            if (CBRecherche("select guidcadreref, nomcadreref, guidparent from cadreref"))
            {
                while (Reader.Read())
                {
                    string[] el = new string[3];
                    el[0] = Reader.GetString(0); el[1] = Reader.GetString(1);
                    el[2] = Reader.GetString(2);
                    lstCadreRef.Add(el);
                }
            }
            CBReaderClose();
            return lstCadreRef;
        }

        public ArrayList getContextTechno(string guidCadreRef, List<string[]> lstcadreref)
        {
            ArrayList lstContext1 = new ArrayList(), lstContext = new ArrayList();
            string elCur = guidCadreRef;

            do {
                string[] elFind = lstcadreref.Find(el => el[0] == elCur);
                lstContext1.Add(elFind[1]);
                elCur = elFind[2];
            } while (elCur != "Root");
            for (int i = lstContext1.Count - 1; i >= 0; i--) lstContext.Add(lstContext1[i]);
            
            return lstContext;
        }

        

        public List<String[]> GetLstAppWithSoftsarePackage()
        {
            List<String[]> lstApplication = new List<string[]>();

            if (CBRecherche("Select GuidApplication, SoftwarePackage From Application"))
            {
                while (Reader.Read())
                {
                    string[] aApp = new string[2];
                    aApp[0] = Reader.GetString(0);
                    aApp[1] = Reader.GetString(1);
                    lstApplication.Add(aApp);
                }
            }
            CBReaderClose();

            return lstApplication;
        }

        public int fillTechnoXls(string requete, MOI.Excel._Worksheet oSheet, List<string[]> lstcadreref, int row, int col)
        {
            //(0)GuidTechnoRef, (1)TechnologyName, (2)TechnologyVersion, (3)TechnologyType, (4)ObsoScore, (5)GuidProduit, (6)DerogationEndDate, (7)GroupITStandardInCompetition, 
            //(8)RoadmapUpcomingStartDate, (9)RoadmapUpcomingEndDate, (10)RoadmapReferenceStartDate, (11)RoadmapReferenceEndDate, (12)RoadmapConfinedStartDate,
            //(13)RoadmapConfinedEndDate, (14)RoadmapDecommissionedStartDate, (15)RoadmapDecommissionedEndDate, (16)RoadmapSupplierEndOfSupportDate, (17)UserID, (18)UpdateDate

            ArrayList lstcol = new ArrayList();
            if (CBRecherche(requete))
            {
                while (Reader.Read())
                {
                    int iStattutTechno = 0;
                    if (!Reader.IsDBNull(3)) iStattutTechno = Reader.GetInt32(3);

                    //iStattutTechno: (0)Not_Defined, (1)Group_IT_Standard, (2)Complementary_Choice, (3)Conflict_Version, (4)Conflict_Product, (5)Decommissioning
                    if (iStattutTechno < 6)
                    {
                        //Guid Techno
                        oSheet.Cells[++row, 1] = Reader.GetString(0);

                        //TechnologyName, TechnologyVersion, TechnologyType
                        if (!Reader.IsDBNull(1)) oSheet.Cells[row, 2] = Reader.GetString(1);
                        if (!Reader.IsDBNull(2)) oSheet.Cells[row, 3] = Reader.GetString(2);
                        oSheet.Cells[row, 4] = parent.sStatutTechno[iStattutTechno];

                        //Obso

                        //guidproduit
                        if (!Reader.IsDBNull(5)) oSheet.Cells[row, 6] = Reader.GetString(5);

                        //Dates
                        if (!Reader.IsDBNull(8)) oSheet.Cells[row, 9] = Reader.GetDate(8);
                        if (!Reader.IsDBNull(9)) oSheet.Cells[row, 10] = Reader.GetDate(9);
                        if (!Reader.IsDBNull(10)) oSheet.Cells[row, 11] = Reader.GetDate(10);
                        if (!Reader.IsDBNull(11)) oSheet.Cells[row, 12] = Reader.GetDate(11);
                        if (!Reader.IsDBNull(12)) oSheet.Cells[row, 13] = Reader.GetDate(12);
                        if (!Reader.IsDBNull(13)) oSheet.Cells[row, 14] = Reader.GetDate(13);
                        if (!Reader.IsDBNull(14)) oSheet.Cells[row, 15] = Reader.GetDate(14);
                        if (!Reader.IsDBNull(15)) oSheet.Cells[row, 16] = Reader.GetDate(15);

                        if (!Reader.IsDBNull(16)) oSheet.Cells[row, 17] = DateTime.FromOADate(Reader.GetDouble(16)).ToShortDateString();

                        // UserID , Date
                        oSheet.Cells[row, 18] = "Gilles Aldeguer";
                        oSheet.Cells[row, 19] = DateTime.Now.ToShortDateString();

                    }
                }
            }
            CBReaderClose();
            return row;
        }

        public void InitCadreRef(char TechFonc, TreeNodeCollection tn, string guidParent)
        {
            ArrayList guidCadreRef = new ArrayList();
            ArrayList NomCadreRef = new ArrayList();
            string sSelect = "";

            switch (TechFonc)
            {
                case 'T':
                    sSelect = "Select GuidCadreRef, NomCadreRef FROM CadreRef WHERE GuidParent='" + guidParent + "' ORDER BY NomCadreRef";
                    break;
                case 'F':
                    sSelect = "Select GuidCadreRefFonc, NomCadreRefFonc FROM CadreRefFonc WHERE GuidParentFonc='" + guidParent + "' ORDER BY NomCadreRefFonc";
                    break;
            }

            if (CBRecherche(sSelect))
            {
                while (Reader.Read())
                {
                    guidCadreRef.Add((object)Reader.GetString(0));
                    NomCadreRef.Add((object)Reader.GetString(1));
                }
            }
            CBReaderClose();
            for (int i = 0; i < guidCadreRef.Count; i++)
            {
                tn.Add((string)guidCadreRef[i], (string)NomCadreRef[i]);
                InitCadreRef(TechFonc, tn[tn.Count - 1].Nodes, (string)guidCadreRef[i]);
            }
        }

        public void CBAddComboBox(string strSelect, List<string[]> lstApp, ComboBox cb, int index)
        {
            SelectReader();
            lstApp.Clear();
            cb.Items.Clear();
            try
            {
                CmdText = strSelect;
                Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    string[] aEnreg = new string[Reader.FieldCount];
                    for (int i = 0; i < Reader.FieldCount; i++)
                    {
                        switch (Reader.GetFieldType(i).Name)
                        {
                            case "String":
                                if(Reader.IsDBNull(i)) aEnreg[i] = ""; else aEnreg[i] = Reader.GetString(i);
                                break;
                            case "Int32":
                                if (Reader.IsDBNull(i)) aEnreg[i] = "0"; else aEnreg[i] = Reader.GetInt32(i).ToString();
                                break;
                            case "Double":
                                if (Reader.IsDBNull(i)) aEnreg[i] = "0"; else aEnreg[i] = Reader.GetDouble(i).ToString();
                                break;
                            case "DateTime":
                                if (Reader.IsDBNull(i)) aEnreg[i] = ""; else aEnreg[i] = Reader.GetDate(i).ToShortDateString();
                                break;
                        }
                    }
                    lstApp.Add(aEnreg);
                    cb.Items.Add(aEnreg[index]);
                }
                CBReaderClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CBAddComboBox(string strSelect, ComboBox cb)
        {
            SelectReader();
            cb.Items.Clear();
            try
            {
                CmdText = strSelect;
                Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    if (Reader.FieldCount > 1 && !Reader.IsDBNull(1) && Reader.GetString(1)!="") cb.Items.Add(Reader.GetString(0) + "[" + Reader.GetString(1) + "]");
                    else cb.Items.Add((string)Reader[0]);
                }
                CBReaderClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
                 
        public void CBAddComboBox(string strSelect, ComboBox cb1, ComboBox cb2)
        {
            SelectReader();
            cb1.Items.Clear(); cb2.Items.Clear();
            try
            {
                CmdText = strSelect;
                Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    cb1.Items.Add((string)Reader[0]);
                    if (Reader.FieldCount > 2 && !Reader.IsDBNull(2) && Reader.GetString(2) != "") cb2.Items.Add(Reader.GetString(1) + " [" + Reader.GetString(2) + "]");
                    else cb2.Items.Add((string)Reader[1]);
                }
                CBReaderClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CBAddComboBox(string strSelect, ComboBox cb1, ComboBox cb2, ComboBox cb3)
        {
            SelectReader();
            cb1.Items.Clear(); cb2.Items.Clear(); cb3.Items.Clear();
            try
            {
                CmdText = strSelect;
                Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    cb1.Items.Add((string)Reader[0]);
                    cb2.Items.Add((string)Reader[1]);
                    cb3.Items.Add((string)Reader[2]);
                }
                CBReaderClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*
        public void CBAddComboBox(string strSelect, DataGridViewComboBoxColumn cb1, DataGridViewComboBoxColumn cb2)
        {
            SelectReader();
            cb1.Items.Clear(); cb2.Items.Clear();
            try
            {
                CmdText = strSelect;
                Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    cb1.Items.Add((string)Reader[0]);
                    cb2.Items.Add((string)Reader[1]);
                }
                CBReaderClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/

        public void CBAddArrayListProp(string sGuidStaticTable, ArrayList lstProp, ArrayList lstVal)
        {
            SelectReader();
            try
            {
                CmdText = "SELECT Propriete, Val FROM StaticTable WHERE GuidStaticProfil='" + parent.sGuidTemplate + "' AND GuidStaticTable='" + sGuidStaticTable + "'";
                Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    lstProp.Add((string)Reader[0]);
                    lstVal.Add((string)Reader[1]);
                }
                CBReaderClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CBAddArrayListProp(string sGuidTemplate, string sGuidStaticTable, ArrayList lstProp, ArrayList lstVal)
        {
            SelectReader();
            try
            {
                CmdText = "SELECT Propriete, Val FROM StaticTable WHERE GuidStaticProfil='" + sGuidTemplate + "' AND GuidStaticTable='" + sGuidStaticTable + "'";
                Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    lstProp.Add((string)Reader[0]);
                    lstVal.Add((string)Reader[1]);
                }
                CBReaderClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CBAddArrayListcObj(string strSelect, ArrayList lst1)
        {
            SelectReader();
            try
            {
                CmdText = strSelect;
                Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    lst1.Add(new cObj((string)Reader[0], (string)Reader[1]));
                }
                CBReaderClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string GetValueInStringNomGuid(string NomGuid, int idx)
        {
            string[] aNomCle = NomGuid.Split(new Char[] { '(', ')' });
            if (aNomCle.Length == 3 && idx < 2)
            {
                return aNomCle[idx].Trim();
            }
            return "";
        }

        public void CBAddListBoxItem(string Nom, string Guid, ListBox lb)
        {
            lb.Items.Add(Nom + "     (" + Guid + ")");
        }


        public bool CBAddListBox(string strSelect, ListBox lb)
        {
            bool bRetour = false;
            SelectReader();
            lb.Items.Clear();
            try
            {
                CmdText = strSelect;
                Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    bRetour = true;
                    if (Reader.FieldCount == 1) lb.Items.Add(Reader.GetString(0));
                    else CBAddListBoxItem(Reader.GetString(1), Reader.GetString(0), lb);
  
                }
                CBReaderClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return bRetour;
        }

        public void CBAddNodeWithTv(string strSelect, TreeNodeCollection tn)
        {
            if (CBRecherche(strSelect))
            {
                while (Reader.Read())
                {
                    TreeNode[] ArrayTreeNode = parent.tvObjet.Nodes.Find(Reader.GetString(0), true);
                    if (ArrayTreeNode.Length == 1)
                        tn.Add((string)Reader[0], (string)Reader[1]);
                } 
            }
            CBReaderClose();
        }

        public void CBAddNodeWithKeyObjet(string strSelect, TreeNodeCollection tn)
        {
            if (CBRecherche(strSelect))
            {
                while (Reader.Read())
                {
                    int n = parent.drawArea.GraphicsList.FindObjet(0, Reader.GetString(0));
                    if (n > -1) tn.Add((string)Reader[1], (string)Reader[2]);
                }
            }
            CBReaderClose();
        }

        public void CBAddNode (string strSelect, XmlDocument xmlDoc, XmlElement el)
        {
            SelectReader();
            try
            {
                CmdText = strSelect;
                Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    if (Reader.FieldCount == 3)
                    {
                        parent.XmlCreatElKeyNode(xmlDoc, el, Reader.GetString(0), Reader.GetString(1));
                        XmlElement elAtts = parent.XmlGetFirstElFromParent(el, "Attributs");
                        parent.XmlSetAttFromEl(xmlDoc, elAtts, "NumInfo", "d", Reader.GetInt32(2).ToString());
                    }
                    else parent.XmlCreatElKeyNode(xmlDoc, el, Reader.GetString(0), Reader.GetString(1));
                }
                CBReaderClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        public void CBAddNode(string strSelect, TreeNodeCollection tn)
        {
            SelectReader();
            try
            {
                CmdText = strSelect;
                Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    if (Reader.FieldCount == 3) tn.Add((string)Reader[0], (string)Reader[1] + "                     -" + Reader.GetInt32(2).ToString());
                    else tn.Add(Reader.GetString(0), Reader.GetString(1));
                }
                CBReaderClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CBAddNodeObjExp(FormExplorObj feo, string strSelect, TreeNode tn, DrawArea.DrawToolType dt)
        {
            SelectReader();
            try
            {
                CmdText = strSelect;
                Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    
                    if (feo.FindObj(Reader.GetString(0), tn.Name) == null)
                    {
                        Guid g = Guid.NewGuid();
                        TreeNode t = tn.Nodes.Add(g.ToString(), Reader.GetString(1));
                        int n = tn.Nodes.Count;
                        Font fontpro = new Font("arial", 8);
                        tn.Nodes[n - 1].NodeFont = new Font(fontpro, FontStyle.Bold);
                        tn.Nodes[n - 1].ForeColor = Color.Blue;
                        //(tn.Nodes[n-1].NodeFont, tn.Nodes[n-1].NodeFont.Style | FontStyle.Bold);
                        ExpObj eo = new ExpObj(g, new Guid(Reader.GetString(0)), dt, -1, t);
                        feo.lstObj.Add((object)eo);

                    }
                    else
                        tn = tn;
                }
                CBReaderClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void SelectReader()
        {
            if (Reader != null)
            {
                if (!Reader.IsClosed && currentReader<3)
                {
                    currentReader++;
                }
            }
        }
    
        public bool CBRecherche(string strSelect)
        {
            SelectReader();
            try
            {
                CmdText = strSelect;
                if (sw != null)
                    SWwriteLog(0, CmdText, false);
                Reader = Cmd.ExecuteReader();
                return Reader.HasRows;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false; 
        }

        public void CBReaderClose()
        {
            Reader.Close();
            CmdText = "";
            if (currentReader > 0) currentReader--;
        }

        public void CBWrite(string strSelect)

        {
#if WRITE
            try
            {
                CmdText = strSelect.Replace("\\","\\\\");
                //Cmd.Parameters.AddWithValue(

                Cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message);
            }
#endif
        }

        public void CBWriteWithObj(string strSelect, object o)
        {
#if WRITE
            try
            {
                CmdText = CmdText = strSelect.Replace("\\", "\\\\");
                //Cmd.Parameters.Add(Param, OdbcType.Binary);
                //Cmd.Parameters[Param].Value = o;
                //Cmd.Parameters.AddWithValue("buffer", o);

                //Cmd.Parameters.Add("param1", OdbcType.Binary).Value = o;
                if(o!=null) Cmd.Parameters.AddWithValue("param1", o);
                //Cmd.ExecuteNonQuery();
                
                Cmd.ExecuteScalar();
                Cmd.Parameters.Clear();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
#endif
        }

        public void DeleteVue(string sGuidGVue)
        {
            if (sGuidGVue != null)
            {
                // Suppression des objets GModule de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Module], sGuidGVue);

                // Suppression des objets GModule de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.AppUser], sGuidGVue);

                // Suppression des objets GModule de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Application], sGuidGVue);

                // Suppression des objets GMainComposant de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.MainComposant], sGuidGVue);

                // Suppression des objets GCompFonc de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.CompFonc], sGuidGVue);

                // Suppression des objets GComposant de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Composant], sGuidGVue);

                // Suppression des objets GInterface de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Interface], sGuidGVue);

                // Suppression des objets GFile de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.File], sGuidGVue);

                // Suppression des objets GBase de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Base], sGuidGVue);

                // Suppression des objets GTechUser de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.TechUser], sGuidGVue);

                // Suppression des objets GServer de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Server], sGuidGVue);

                // Suppression des objets GServMComp de l'ancienne Vue
                //DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.ServMComp], sGuidGVue);

                // Suppression des objets GLink de l'ancienne Vue
                DeleteGObjetEtPoint(parent.drawArea.tools[(int)DrawArea.DrawToolType.Link], sGuidGVue);

                // Suppression des objets GTechLink de l'ancienne Vue
                DeleteGObjetEtPoint(parent.drawArea.tools[(int)DrawArea.DrawToolType.TechLink], sGuidGVue);

                // Suppression des objets GServerPhy de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.ServerPhy], sGuidGVue);

                // Suppression des objets GVLan de l'ancienne Vue
                DeleteGObjetEtPoint(parent.drawArea.tools[(int)DrawArea.DrawToolType.VLan], sGuidGVue);

                // Suppression des objets GRouter de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Router], sGuidGVue);

                // Suppression des objets GNCard de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.NCard], sGuidGVue);

                // Suppression des objets GCluster de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Cluster], sGuidGVue);

                // Suppression des objets GBaie de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Baie], sGuidGVue);

                // Suppression des objets GMachine de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Machine], sGuidGVue);

                // Suppression des objets GVirtuel de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Virtuel], sGuidGVue);

                // Suppression des objets GLun de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Lun], sGuidGVue);

                // Suppression des objets GLun de l'ancienne Vue
                DeleteGObjetEtPoint(parent.drawArea.tools[(int)DrawArea.DrawToolType.Zone], sGuidGVue);

                // Suppression des objets GLun de l'ancienne Vue
                DeleteGObjetEtPoint(parent.drawArea.tools[(int)DrawArea.DrawToolType.ServerSite], sGuidGVue);

                // Suppression des objets GLun de l'ancienne Vue
                DeleteGObjetEtPoint(parent.drawArea.tools[(int)DrawArea.DrawToolType.Location], sGuidGVue);

                // Suppression des objets GLun de l'ancienne Vue
                DeleteGObjetEtPoint(parent.drawArea.tools[(int)DrawArea.DrawToolType.PtCnx], sGuidGVue);

                // Suppression des objets GLun de l'ancienne Vue
                DeleteGObjetEtPoint(parent.drawArea.tools[(int)DrawArea.DrawToolType.InterLink], sGuidGVue);

                // Suppression des objets GLun de l'ancienne Vue
                DeleteGObjetEtPoint(parent.drawArea.tools[(int)DrawArea.DrawToolType.Cnx], sGuidGVue);

                // Suppression des objets GBaieCTI de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.BaieCTI], sGuidGVue);

                // Suppression des objets GSanWitch de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.ISL], sGuidGVue);

                // Suppression des objets GSanWitch de l'ancienne Vue
                DeleteGObjetEtPoint(parent.drawArea.tools[(int)DrawArea.DrawToolType.SanSwitch], sGuidGVue);

                // Suppression des objets GSanCard de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.SanCard], sGuidGVue);

                // Suppression des objets GBaieDPhy de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.BaieDPhy], sGuidGVue);

                // Suppression des objets GDrawer de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Drawer], sGuidGVue);

                // Suppression des objets GBaiePhy de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.BaiePhy], sGuidGVue);

                // Suppression des objets GMachine de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.MachineCTI], sGuidGVue);

                // Suppression des objets GCadreRef de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.CadreRefN], sGuidGVue);

                // Suppression des objets GCadreRef de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.CadreRefEnd], sGuidGVue);

                //CBWrite("DELETE FROM Vue WHERE GuidVue='" + sGuidVue + "'");
            }
        }


        public void UpdateObject(DrawObject o)
        {
            //string sType = o.GetsType(false);
            string sType = o.GetTypeSimpleTable();
            string Update = "UPDATE " + sType;
            string Set = "SET ";
            string Where = "WHERE ";

            Table t;
            int n = ConfDB.FindTable(sType);
            if (n > -1)
            {
                t = (Table)ConfDB.LstTable[n];
                bool InsFieldW = false, InsFieldR = false;

                for (int i = 0; i < t.LstField.Count; i++)
                {
                    if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.InterneBD) != 0)
                    {

                        if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.Key) != 0)
                        {
                            switch (((Field)t.LstField[i]).Type)
                            {
                                case 's':
                                    if ((string)o.LstValue[i] != "" && ((string)o.LstValue[i])[0] != 32)
                                    {
                                        if (InsFieldW) Set += " and ";
                                        Where += ((Field)t.LstField[i]).Name + "='" + o.LstValue[i] + "'";
                                        InsFieldW = true;
                                    }
                                    break;
                                case 'p': //picture
                                case 'q': //picture
                                case 'i':
                                    if ((int)o.LstValue[i] != 0)
                                    {
                                        if (InsFieldW) Set += " and ";
                                        Where += ((Field)t.LstField[i]).Name + "=" + o.LstValue[i];
                                        InsFieldW = true;
                                    }
                                    break;
                                case 'd':
                                    if ((double)o.LstValue[i] != 0)
                                    {
                                        if (InsFieldR) Set += ", ";
                                        Set += ((Field)t.LstField[i]).Name + "=" + o.LstValue[i];
                                        InsFieldR = true;
                                    }
                                    break;
                                case 't':
                                    //if (o.LstValue[i] != null && (string)o.LstValue[i] != "" && (DateTime)o.LstValue[i] != DateTime.MinValue)
                                    if (o.LstValue[i] != null && (string)o.LstValue[i] != "")
                                    {
                                        DateTime dt = (DateTime)o.LstValue[i];
                                        if (InsFieldR) Set += ", ";
                                        Set += ((Field)t.LstField[i]).Name + "='" + dt.ToString("yyyy-MM-dd") + "'"; //dt.ToShortDateString() + "'";
                                        InsFieldR = true;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            switch (((Field)t.LstField[i]).Type)
                            {
                                case 's':
                                    if (InsFieldR) Set += " , ";
                                    InsFieldR = true;
                                    if ((string)o.LstValue[i] != "")
                                        Set += ((Field)t.LstField[i]).Name + "='" + o.LstValue[i].ToString().Replace("'", "''") + "'";
                                    else
                                        Set += ((Field)t.LstField[i]).Name + "= null";
                                    break;
                                case 'p': //picture
                                case 'q': //picture
                                case 'i':
                                    if ((int)o.LstValue[i] != 0)
                                    {
                                        if (InsFieldR) Set += " , ";
                                        Set += ((Field)t.LstField[i]).Name + "=" + o.LstValue[i];
                                        InsFieldR = true;
                                    }
                                    break;
                                case 'd':
                                    if ((double)o.LstValue[i] != 0)
                                    {
                                        if (InsFieldR) Set += " , ";
                                        Set += ((Field)t.LstField[i]).Name + "=" + o.LstValue[i].ToString().Replace(',', '.');
                                        InsFieldR = true;
                                    }
                                    break;
                                case 't':
                                    if (o.LstValue[i] != null && (DateTime)o.LstValue[i] != DateTime.MinValue)
                                    {
                                        DateTime dt = (DateTime)o.LstValue[i];
                                        if (InsFieldR) Set += ", ";
                                        Set += ((Field)t.LstField[i]).Name + "='" + dt.ToString("yyyy-MM-dd") + "'"; //dt.ToShortDateString() + "'";
                                        InsFieldR = true;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            //DataTable datatable1 = connection.GetSchema("Tables", new string[] { null, sType, null });
            //Fdatatable1 = connection.GetSchema(OdbcMetaDataCollectionNames.Views);
            //datatable1 = connection.GetSchema(OdbcMetaDataCollectionNames.Indexes);
            //datatable1 = connection.GetSchema(OdbcMetaDataCollectionNames.Procedures);
            //datatable1 = connection.GetSchema(OdbcMetaDataCollectionNames.Columns);
            //DataTable datatable1 = connection.GetSchema(OdbcMetaDataCollectionNames.Columns, new string[] { sType });
            //DataTable datatable = connection.GetSchema("columns", new[] { null, null, sType, "updatedate" });
            
            // cette fonction UpdateObject existe aussi avec l'objet enreg
            if (isDataTableContainColumn("updatedate", sType))
                Set += ", updatedate='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
            CBWrite(Update + " " + Set + " " + Where);
        }

        public bool isDataTableContainColumn(string sColumnName, string sTableName)
        {
            CBRecherche("select * from " + sTableName + " where 1=0");

            //DataTable table = connection.GetSchema("Tables", new string[] { null, null, sTableName });
            //DataColumnCollection columns = table.Columns;

            DataTable table = Reader.GetSchemaTable();
            Reader.Close();
            
            //For each field in the table...
            foreach (DataRow Field in table.Rows)
            {
                if (Field[0].ToString() == sColumnName) return true;
                
            }
            //if (columns.Contains(sColumnName)) return true;
            
            return false;
        }

        public bool isDataTableExist(string sTableName)
        {
            bool bExist = false;
            CmdText = "select count(*) from " + sTableName;
            try
            {
                Reader = Cmd.ExecuteReader();

                bExist = true;

            } catch
            {
                bExist = false;
            }
            CBReaderClose();
            return bExist;
        }

        public object GetValueFromNameInXmlReader(XmlElement el, string ValueName)
        {
            XmlElement elFind = parent.XmlFindElFromAtt(el, "Name", ValueName);
            if (elFind != null)
            {
                string sType = elFind.GetAttribute("Type");
                if (sType == "String")
                {
                    if (elFind.GetAttribute("Value") == "null") return null;
                    return elFind.GetAttribute("Value");
                }
                else if (sType == "Double")
                {
                    if (elFind.GetAttribute("Value") == "null") return 0;
                    return Convert.ToDouble(elFind.GetAttribute("Value"));
                }
                else if (sType == "DateTime")
                {
                    if (elFind.GetAttribute("Value") == "null") return DateTime.MinValue;
                    return Convert.ToDateTime(elFind.GetAttribute("Value"));
                }
            }
            //if (elCur.GetAttribute(sAtt) != null) elCur.SetAttribute(sAtt, sValue);
            //XmlAttributeCollection lstAtt = el.Attributes;

            return null;
        }

        public XmlDocument CreatXmlDocFromXmlFiles(XmlDocument xmlData, XmlDocument xmlTemplate)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<Export></Export>");

            CreatXmlDocFromXmlFiles(xmlDoc, xmlData, xmlTemplate);

            return xmlDoc;
        }
            
        public void CreatXmlDocFromXmlFiles(XmlDocument xmlDoc, XmlDocument xmlData, XmlDocument xmlTemplate)
        {   
            XmlElement root = xmlDoc.DocumentElement;
            XmlElement rootData = xmlData.DocumentElement;
            XmlElement rootTemplate = xmlTemplate.DocumentElement;

            IEnumerator ienum = rootData.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    XmlElement elRow = xmlDoc.CreateElement("row");
                    XmlElement elRowData = (XmlElement)Node;

                    IEnumerator ienumTemplate = rootTemplate.GetEnumerator();
                    XmlNode NodeTemplate;
                    while (ienumTemplate.MoveNext())
                    {
                        NodeTemplate = (XmlNode)ienumTemplate.Current;
                        if (NodeTemplate.NodeType == XmlNodeType.Element)
                        {
                            XmlElement elTemplate = (XmlElement)NodeTemplate;
                            string sElName = elTemplate.GetAttribute("Name");
                            if (sElName != null)
                            {
                                XmlElement el = xmlDoc.CreateElement(sElName);
                                string[] aTemplate = elTemplate.GetAttribute("Template").Split('+');
                                string sTemplate = "";
                                for (int i = 0; i < aTemplate.Length; i++)
                                {
                                    if (aTemplate[i][0] == '@')
                                    {
                                        XmlElement elField = parent.XmlFindElFromAtt(elRowData, "Name", aTemplate[i].Substring(1));
                                        if (elField != null) sTemplate += elField.GetAttribute("Value");
                                    }
                                    else
                                    {
                                        sTemplate += aTemplate[i];
                                    }
                                }
                                el.InnerText = sTemplate;
                                elRow.AppendChild(el);
                            }
                        }
                    }
                    root.AppendChild(elRow);
                }
            }
        }

        public XmlDocument CreatXmlDocFromDB()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<Reader></Reader>");
            XmlElement root = xmlDoc.DocumentElement;
            

            while (Reader.Read())
            {
                XmlElement xmlRow = xmlDoc.CreateElement("row");
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    XmlElement xmlField = xmlDoc.CreateElement("Field");
                    xmlField.SetAttribute("Name", Reader.GetName(i));
                    xmlField.SetAttribute("Type", Reader.GetFieldType(i).ToString().Substring(7)); //System.
                    if (Reader.IsDBNull(i)) xmlField.SetAttribute("Value", "null");
                    else
                    {
                        if (Reader.GetFieldType(i) == typeof(string)) xmlField.SetAttribute("Value", Reader.GetString(i));
                        else if (Reader.GetFieldType(i) == typeof(Double)) xmlField.SetAttribute("Value", Reader.GetDouble(i).ToString());
                        else if (Reader.GetFieldType(i) == typeof(DateTime)) xmlField.SetAttribute("Value", Reader.GetDateTime(i).ToShortDateString());
                    }
                    xmlRow.AppendChild(xmlField);
                }
                root.AppendChild(xmlRow);
            }
            CBReaderClose();

            return xmlDoc;
        }
        
        public void CreatDansTypeVue(Guid GuidObjet, string TypeObjet)
        {
            CBWrite("INSERT INTO DansTypeVue (GuidTypeVue, GuidObjet, TypeObjet) VALUES ('" +  parent.GuidTypeVue + "','" + GuidObjet + "','" + TypeObjet + "')");
        }

        public void CreatServiceLink(string sGroupService, string sEnvironnement, ListBox lb)
        {
            for (int i = 0; i < lb.Items.Count; i++)
            {
                CBWrite("Insert Into ServiceLink (GuidGroupService, GuidEnvironnement, GuidService) Values ('" + sGroupService + "','" + sEnvironnement + "','" + GetValueInStringNomGuid((string)lb.Items[i],1) + "')");
            }
        }

        public void CreatTechLink(DrawTechLink dl)
        {
            
            CBWrite("INSERT INTO TechLink (GuidTechLink, NomTechLink, GuidServerIn, GuidServerOut) VALUES ('" + dl.GuidkeyObjet + "','" + dl.Texte + "','" + ((DrawObject)dl.LstLinkIn[0]).GuidkeyObjet + "','" + ((DrawObject)dl.LstLinkOut[0]).GuidkeyObjet + "')");
            string sLinkApp = dl.sValue[2];
            
            string[] aLinkApp = sLinkApp.Split(new Char[] { '(', ')' });
            for (int i = 1; i < aLinkApp.Length; i += 2)
            {
                CBWrite("UPDATE Link SET GuidTechLink='" + dl.GuidkeyObjet + "' WHERE GuidLink = '" + aLinkApp[i] + "'");
            }
        }

        /*
        public void CreatMCompServ(DrawServMComp ds)
        {
            for (int i = 0; i<ds.LstParent.Count; i++)
            {
                if (!CBRecherche("Select GuidMainComposant From MCompServ WHERE GuidMainComposant='" + ds.GuidkeyObjet + "' AND GuidServer='" + ((DrawObject)ds.LstParent[i]).GuidkeyObjet + "'"))
                {
                    CBReaderClose();
                    CBWrite("INSERT INTO MCompServ (GuidMainComposant, GuidServer) VALUES ('" + ds.GuidkeyObjet + "','" + ((DrawObject)ds.LstParent[i]).GuidkeyObjet + "')");
                }
                else CBReaderClose();
            }
        }*/

        public void CreatTechLinkApp(DrawTechLink dt)
        {
            object o;

            o = dt.GetValueFromName("LinksApplicatifs");
            if (o != null && (string)o != "")
            {
                string[] aLinkApp = ((string)o).Split(new Char[] { '(', ')' });
                for (int i = 1; i < aLinkApp.Length; i += 2)
                {
                    if (!CBRecherche("Select GuidTechLink From TechLinkApp" + " WHERE GuidTechLink='" + dt.GuidkeyObjet + "' AND GuidLink='" + aLinkApp[i].Trim() + "'"))
                    {
                        CBReaderClose();
                        CBWrite("INSERT INTO TechLinkApp (GuidTechLink, GuidLink) VALUES ('" + dt.GuidkeyObjet + "','" + aLinkApp[i].Trim() + "')");
                    }
                    else CBReaderClose();
                }
            }
        }

        public void DeleteServerTypeLink(DrawServer ds)
        {
            CBWrite("Delete From Techno Where GuidTechnoHost in (Select GuidServerType From ServerTypeLink Where GuidServer='" + ds.GuidkeyObjet + "')");
            CBWrite("Delete From ServerTypeLink WHERE GuidServer='" + ds.GuidkeyObjet + "'");
        }
        
        public List<string[]> GetListTechno(string sType, string sTypeRef, string Guid, string prefix="")
        {
            List<string[]> lstTechno = new List<string[]>();
            //if (CBRecherche("Select GuidTechno, GuidServerType From Techno Where GuidServerType in (Select GuidServerType From ServerTypeLink Where GuidServer='" + ds.GuidkeyObjet + "')"))
            if (CBRecherche("Select GuidTechno, GuidTechnoHost From Techno Where GuidTechnoHost in (Select Guid" + sType + " From " + prefix + sType + "Link Where Guid" + sTypeRef + "='" + Guid + "')"))
            {

                while (Reader.Read())
                {
                    string[] aEnreg = new string[2];
                    aEnreg[0] = Reader.GetString(0);
                    aEnreg[1] = Reader.GetString(1);
                    lstTechno.Add(aEnreg);
                }
                CBReaderClose();
            }
            else CBReaderClose();
            return lstTechno;
        }

        
        public List<string[]>  GetListMCompInfra(string sTypeRef, string Guid, string sTableLink= "MCompApp")
        {
            List<string[]> lstMCompServ = new List<string[]>();
            if (CBRecherche("Select GuidMCompServ, GuidMainComposant From MCompServ Where GuidMainComposant in (Select GuidMainComposant From " + sTableLink + "  Where Guid" + sTypeRef + "='" + Guid + "')"))
            {
                
                while (Reader.Read())
                {
                    string[] aEnreg = new string[2];
                    aEnreg[0] = Reader.GetString(0);
                    aEnreg[1] = Reader.GetString(1);
                    lstMCompServ.Add(aEnreg);
                }
                CBReaderClose();
            }
            else CBReaderClose();
            return lstMCompServ;
        }

        public List<string> GetListObjByObjRef(string sType, string sTypeRef, string Guid, string prefix="")
        {
            List<string> lstObj = new List<string>();
            if (CBRecherche("Select Guid" + sType + " FROM " + prefix + sType + "Link WHERE Guid" + sTypeRef + "='" + Guid + "'"))
            {
                while (Reader.Read()) lstObj.Add(Reader.GetString(0));
                CBReaderClose();
            }
            else CBReaderClose();
            return lstObj;
        }

        public List<string> GetListObjByObjRefFromSas(string sType, string sTypeRef, string Guid)
        {
            List<string> lstObj = new List<string>();
            if (CBRecherche("Select GuidServerType FROM SasServerTypeLink" + sType + "Link WHERE Guid" + sTypeRef + "='" + Guid + "'"))
            {
                while (Reader.Read()) lstObj.Add(Reader.GetString(0));
                CBReaderClose();
            }
            else CBReaderClose();
            return lstObj;
        }

        public List<string> GetListMCompObjLink(string sTypeRef, string Guid, string sTableLink= "MCompApp")
        {
            List<string> lstMCompApp = new List<string>();
            if (CBRecherche("Select GuidMainComposant FROM " + sTableLink + " WHERE Guid" + sTypeRef + "='" + Guid + "'"))
            {
                while (Reader.Read()) lstMCompApp.Add(Reader.GetString(0));
                CBReaderClose();
            }
            else CBReaderClose();
            return lstMCompApp;
        }
        public void DeleteMCompApp(DrawServer ds)
        {
            CBWrite("Delete From MCompServ Where GuidMainComposant in (Select GuidMainComposant From MCompApp Where GuidServer='" + ds.GuidkeyObjet + "')");
            CBWrite("DELETE FROM MCompApp WHERE GuidServer='" + ds.GuidkeyObjet + "'");
        }

        public void DeleteUserMComp(DrawTechUser dtu)
        {
            //CBWrite("Delete From MCompServ Where GuidMainComposant in (Select GuidMainComposant From MCompApp Where GuidServer='" + ds.GuidkeyObjet + "')");
            CBWrite("DELETE FROM AppUserMComp WHERE GuidAppUser='" + dtu.GuidkeyObjet + "'");
        }

        public void DeleteUserTypeLink(DrawTechUser dtu)
        {
            CBWrite("DELETE FROM AppUserTypeLink WHERE GuidAppUser='" + dtu.GuidkeyObjet + "'");
        }

        public void CreatUserTypeLink(DrawTechUser dtu, DrawServerType dst)
        {
            CBWrite("INSERT INTO AppUserTypeLink (GuidAppUser, GuidServerType) VALUES ('" + dtu.GuidkeyObjet + "','" + (string)dst.LstValue[0] + "')");
        }

        public void CreatMCompApp(DrawServer ds, DrawMainComposant dmc)
        {
            CBWrite("INSERT INTO MCompApp (GuidServer, GuidMainComposant) VALUES ('" + ds.GuidkeyObjet + "','" + (string) dmc.LstValue[0] + "')");
        }

        public void CreatMCompSas(DrawGensas dgs, DrawMainComposant dmc)
        {
            CBWrite("INSERT INTO MCompSas (GuidGenSas, GuidMainComposant) VALUES ('" + dgs.GuidkeyObjet + "','" + (string)dmc.LstValue[0] + "')");
        }

        public void CreatMCompPod(DrawGenpod dgp, DrawMainComposant dmc)
        {
            CBWrite("INSERT INTO MCompPod (GuidGenPod, GuidMainComposant) VALUES ('" + dgp.GuidkeyObjet + "','" + (string)dmc.LstValue[0] + "')");
        }

        public void CreatUserMComp(DrawTechUser dtu, DrawMainComposant dmc)
        {
            CBWrite("INSERT INTO AppUserMComp (GuidAppUser, GuidMainComposant) VALUES ('" + dtu.GuidkeyObjet + "','" + (string)dmc.LstValue[0] + "')");
        }

        public void CreatServerTypeLink(DrawServer ds, DrawServerType dst)
        {
            CBWrite("INSERT INTO ServerTypeLink (GuidServer, GuidServerType) VALUES ('" + ds.GuidkeyObjet + "','" + (string)dst.LstValue[0] + "')");
        }

        public void CreatObjLink(string sTypeLink, string GuidTypeLink, string sTypeRef, string GuidTypeRef, string prefix = "")
        {
            CBWrite("INSERT INTO " + prefix + sTypeLink + "Link (Guid" + sTypeRef + ", Guid" + sTypeLink + ") VALUES ('" + GuidTypeRef + "','" + GuidTypeLink + "')");
        }


        public void DeleteServiceLink(string sGuidGroupService)
        {
            CBWrite("Delete From ServiceLink Where GuidGroupService='" + sGuidGroupService + "'");
        }

        public void DeleteObjetsLink(Guid GuidVue)
        {
            CBWrite("Delete From ApplicationLink Where GuidVue='" + GuidVue.ToString() + "'");
            CBWrite("Delete From AppUserLink Where GuidVue='" + GuidVue.ToString() + "'");
            ArrayList aLstServerPhy = new ArrayList();
            if (CBRecherche("Select GuidServerPhy From ServerLink Where GuidVue='" + GuidVue.ToString() + "'"))
            {
                // suppression uniquement de serverphy non présent dans la vue. Le reste est géré avec les packages associés
                while (Reader.Read()) aLstServerPhy.Add(Reader.GetString(0));
            }
            CBReaderClose();
            for(int i=0; i<aLstServerPhy.Count;i++)
            {
                if(parent.drawArea.GraphicsList.FindObjet(0, (string) aLstServerPhy[i]) < 0)
                {
                    CBWrite("Delete From ServerLink Where GuidVue='" + GuidVue.ToString() + "' and GuidServerPhy='" + aLstServerPhy[i] + "'");
                }
            }
            //CBWrite("Delete From ServerLink Where GuidVue='" + GuidVue.ToString() + "'"); // avant il faut gérer les pacakges dans la vue est direct en base
        }

        public void DeleteNCardLink(string GVue, string Sens, string sGuidVueInf)
        {
            ArrayList aEnreg = new ArrayList();
            //string sQuery = "SELECT NCardLink" + Sens + ".GuidNCard, NCardLink" + Sens + ".GuidTechLink, NomTechLink FROM DansVue, GNCard, NCardLink" + Sens + ", TechLink WHERE GuidVue ='" + GVue + "' AND GuidObjet=GuidGNCard AND GNCard.GuidNCard=NCardLink" + Sens + ".GuidNCard AND NCardLink" + Sens + ".GuidTechLink=TechLink.GuidTechLink ORDER BY NCardLink" + Sens + ".GuidNCard";
            //if (Sens == "Out")
            //{
                string sQuery = "SELECT NCardLink" + Sens + ".GuidNCard, NCardLink" + Sens + ".GuidTechLink, NomTechLink FROM Vue, DansVue, GNCard, NCardLink" + Sens + ", TechLink WHERE Vue.GuidVue ='" + GVue + "' and Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGNCard AND GNCard.GuidNCard=NCardLink" + Sens + ".GuidNCard AND NCardLink" + Sens + ".GuidTechLink=TechLink.GuidTechLink ";
                sQuery += "AND TechLink.GuidTechLink IN (Select GuidTechLink FROM Vue, DansVue, GTechLink Where Vue.GuidVue='" + sGuidVueInf + "' and Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGTechLink) ORDER BY NCardLink" + Sens + ".GuidNCard";
            //}
            
            if (CBRecherche(sQuery))
            {               
                while (Reader.Read())
                {
                    aEnreg.Add(Reader.GetString(0) + ";" + Reader.GetString(1));
                }
                CBReaderClose();
            }
            else CBReaderClose();
            for (int i = 0; i < aEnreg.Count; i++)
            {
                string[] Enreg = ((string)aEnreg[i]).Split(';');
                CBWrite("DELETE FROM NCardLink" + Sens + "  WHERE GuidNCard='" + Enreg[0] + "' AND GuidTechLink='" + Enreg[1] + "'");
            }
        }

        public void DelNCardAlias(DrawNCard dnc)
        {
        }

        public void MajNCardAlias(DrawNCard dnc)
        {
            object o;
            o = dnc.GetValueFromName("Alias");
            if (o != null && (string)o != "")
            {
                string[] aAlias = ((string)o).Split(new Char[] { '(', ')', ';' });
                for (int i = 1; i < aAlias.Length; i += 3)
                {
                    if (!CBRecherche("Select GuidAlias From Alias WHERE GuidAlias='" + aAlias[i].Trim() + "'"))
                    {
                        CBReaderClose();
                        CBWrite("Insert Into Alias (GuidAlias, NomAlias, GuidNCard) Values('" + aAlias[i].Trim() + "','" + aAlias[i - 1].Trim() + "','" + dnc.GuidkeyObjet + "')");
                    }
                    else
                    {
                        CBReaderClose();
                        CBWrite("Update Alias Set NomAlias='" + aAlias[i - 1].Trim() + "' Where GuidAlias='" + aAlias[i].Trim() + "'");
                    }
                }
            }
        }
        
        public void CreatNCardLink(DrawNCard dnc, string Sens)
        {
            object o;

            o = dnc.GetValueFromName("Flux" + Sens);
            if (o != null && (string)o != "")
            {
                string[] aLinkApp = ((string)o).Split(new Char[] { '(', ')' });
                for (int i = 1; i < aLinkApp.Length; i += 2)
                    CBWrite("INSERT INTO NCardLink" + Sens + " (GuidNCard, GuidTechLink) VALUES ('" + dnc.GuidkeyObjet + "','" + aLinkApp[i].Trim() + "')");
            }
        }

        public void CreatNCardLinkIn(DrawNCard dnc)
        {
            object o;

            o = dnc.GetValueFromName("FluxIn");
            if (o != null && (string)o != "")
            {
                string[] aLinkApp = ((string)o).Split(new Char[] { '(', ':', ')' });
                for (int i = 1; i < aLinkApp.Length; i += 3)
                    CBWrite("INSERT INTO NCardLinkIn (GuidNCard, GuidTechLink, GuidAlias) VALUES ('" + dnc.GuidkeyObjet + "','" + aLinkApp[i].Trim() + "','" + aLinkApp[i + 1].Trim() + "')");

            }
        }
            
        public void CreatRouterLink(DrawRouter dr)
        {
            for (int i = 0; i < dr.LstLinkIn.Count; i++)
            {
                if (!CBRecherche("Select GuidRouter FROM RouterLink WHERE GuidRouter='" + dr.GuidkeyObjet + "' AND GuidVLan='" + ((DrawVLan)dr.LstLinkIn[i]).GuidkeyObjet + "'"))
                {
                    CBReaderClose();
                    CBWrite("INSERT INTO RouterLink (GuidRouter, GuidVLan) VALUES ('" + dr.GuidkeyObjet + "','" + ((DrawVLan)dr.LstLinkIn[i]).GuidkeyObjet + "')");
                }
                else CBReaderClose();
            }
        }

        public void LoadExtention()
        {
            Table t = null; int n; string sType;

            for (int i = 0; i < parent.drawArea.GraphicsList.Count; i++)
            {
                DrawObject dobj = (DrawObject)parent.drawArea.GraphicsList[i];

                string sSelect = dobj.GetSelectExtention();
                string sGuid;

                if (sSelect != null) {

                    sType = dobj.GetTypeSimpleTable();
                    sGuid = (string )dobj.GetValueFromName("GuidManagedsvc");

                    //Chargement des méta données de l'objet
                    n = ConfDB.FindTable(sType);
                    if (n > -1) t = (Table)ConfDB.LstTable[n];

                    if (t != null && sGuid != null)
                    {
                        STExtention stEx = t.lstExtention.Find(el => el.sGuidExtention == sGuid);
                        if (stEx.sGuidExtention == null)
                        {
                            if (CBRecherche(sSelect + " and Type='" + sType + "' order by pos"))
                            {
                                stEx.sGuidExtention = sGuid;
                                stEx.lstFieldExtention = new ArrayList();

                                while (Reader.Read())
                                {
                                    stEx.lstFieldExtention.Add(new Field(Reader.GetString(1), Reader.GetString(2), 's', Reader.GetInt32(3), 0, ConfDataBase.FieldOption.Base));
                                }
                                t.lstExtention.Add(stEx);
                            }
                            CBReaderClose();
                        }
                        if (stEx.sGuidExtention != null)
                        {
                            dobj.LstValueExtention = t.InitValueExtention(sGuid);

                            CBRecherche("Select GuidParam, Value From ExtentionParamValue Where GuidObject = '" + dobj.GuidkeyObjet + "'");
                            while (Reader.Read())
                                dobj.SetValueFromName(Reader.GetString(0), Reader.GetString(1), true);
                            CBReaderClose();
                        }
                    }
                    
                }
            }
        }
        

        public void LoadSousObjets(int iTypeObjet)
        {
            ArrayList LstObjet = new ArrayList();
            switch(iTypeObjet)
            {
                case (int)DrawArea.DrawToolType.Server:
                    for (int i = 0; i < parent.drawArea.GraphicsList.Count; i++)
                    {
                        if (parent.drawArea.GraphicsList[i].GetType() == typeof(DrawServer)) LstObjet.Add(parent.drawArea.GraphicsList[i]);
                    }
                    LoadServerType_Techno(LstObjet);
                    LoadMainComposant_ServMComp(LstObjet);
                    break;
                case (int)DrawArea.DrawToolType.Gensas:
                    for (int i = 0; i < parent.drawArea.GraphicsList.Count; i++)
                    {
                        if (parent.drawArea.GraphicsList[i].GetType() == typeof(DrawGensas)) LstObjet.Add(parent.drawArea.GraphicsList[i]);
                    }
                    LoadManagedsvc_Techno(LstObjet);
                    LoadSvcServerType_Techno(LstObjet);
                    LoadMainComposant_ServMSas(LstObjet);
                    break;
                case (int)DrawArea.DrawToolType.Genpod:
                    for (int i = 0; i < parent.drawArea.GraphicsList.Count; i++)
                    {
                        if (parent.drawArea.GraphicsList[i].GetType() == typeof(DrawGenpod)) LstObjet.Add(parent.drawArea.GraphicsList[i]);
                    }
                    LoadContainer_Techno(LstObjet);
                    LoadMainComposant_ServMpod(LstObjet);
                    break;
                case (int)DrawArea.DrawToolType.TechUser:
                    for (int i = 0; i < parent.drawArea.GraphicsList.Count; i++)
                    {
                        if (parent.drawArea.GraphicsList[i].GetType() == typeof(DrawTechUser)) LstObjet.Add(parent.drawArea.GraphicsList[i]);
                    }
                    LoadUserType_Techno(LstObjet);
                    LoadMainComposant_UserMComp(LstObjet);
                    break;
                case (int)DrawArea.DrawToolType.Cluster:
                    for (int i = 0; i < parent.drawArea.GraphicsList.Count; i++)
                    {
                        if (parent.drawArea.GraphicsList[i].GetType() == typeof(DrawCluster)) LstObjet.Add(parent.drawArea.GraphicsList[i]);
                    }
                    LoadServerPhy_Cluster(LstObjet);
                    break;
                case (int)DrawArea.DrawToolType.ServerPhy:
                    for (int i = 0; i < parent.drawArea.GraphicsList.Count; i++)
                    {
                        if (parent.drawArea.GraphicsList[i].GetType() == typeof(DrawServerPhy)) LstObjet.Add(parent.drawArea.GraphicsList[i]);
                    }
                    LoadNcard(LstObjet);
                    break;
                case (int)DrawArea.DrawToolType.Insks:
                    for (int i = 0; i < parent.drawArea.GraphicsList.Count; i++)
                    {
                        if (parent.drawArea.GraphicsList[i].GetType() == typeof(DrawInsks))
                            LstObjet.Add(parent.drawArea.GraphicsList[i]);
                    }
                    LoadNameSpace(LstObjet);
                    LoadNode(LstObjet);
                    break;
            }
        }

        public void LoadService_AliasInf(string sGuidVueSrvPhy)
        {
            ArrayList LstInfLink = new ArrayList();
            for (int i = 0; i < parent.drawArea.GraphicsList.Count; i++)
            {
                if (parent.drawArea.GraphicsList[i].GetType() == typeof(DrawInfLink)) LstInfLink.Add(parent.drawArea.GraphicsList[i]);
            }

            for (int i = 0; i < LstInfLink.Count; i++)
            {
                DrawInfLink dl = (DrawInfLink)LstInfLink[i];
                if (CBRecherche("SELECT NomAlias FROM NCardLinkIn, Alias, NCard, GNCard, Vue, DansVue WHERE GuidTechLink='" + dl.GuidkeyObjet + "' AND NCardLinkIn.GuidAlias=Alias.GuidAlias AND NCardLinkIn.GuidNCard=Alias.GuidNCard AND NCardLinkIn.GuidNCard=NCard.GuidNCard AND NCard.GuidNCard=GNCard.GuidNCard AND GNCard.GuidGNCard=DansVue.GuidObjet AND DansVue.GuidGVue=Vue.GuidGVue AND Vue.GuidVue='" + sGuidVueSrvPhy + "'"))
                {
                    string sAlias = "";
                    while (Reader.Read()) sAlias += "," + Reader.GetString(0);
                    dl.SetValueFromName("Alias", sAlias.Substring(1));
                }
                CBReaderClose();
                ArrayList lstVal = new ArrayList();
                lstVal = dl.GetValueEtCache((string)dl.GetValueFromName("NomGroupService"));
                if (lstVal.Count > 1 && lstVal[1] != null)
                {
                    if (CBRecherche("SELECT Ports FROM ServiceLink, Service, Environnement, Vue WHERE GuidGroupService='" + lstVal[1] + "' AND GuidVue='" + sGuidVueSrvPhy + "' AND ServiceLink.GuidService=Service.GuidService AND ServiceLink.GuidEnvironnement=Environnement.GuidEnvironnement AND Environnement.GuidEnvironnement=Vue.GuidEnvironnement"))
                    {
                        string sPorts = "";
                        while (Reader.Read()) sPorts += ";" + Reader.GetString(0);
                        dl.SetValueFromName("NomService", sPorts.Substring(1));
                    }
                    CBReaderClose();
                }
            }

        }

        public void LoadSousObjetsInf(string sGuidVueSrvPhy, int iTypeObjet)
        {

            ArrayList LstObjet = new ArrayList();
            switch (iTypeObjet)
            {
                case (int)DrawArea.DrawToolType.Server:
                    for (int i = 0; i < parent.drawArea.GraphicsList.Count; i++)
                    {
                        if (parent.drawArea.GraphicsList[i].GetType() == typeof(DrawServer)) LstObjet.Add(parent.drawArea.GraphicsList[i]);
                    }

                    LoadServerPhy_NCard(LstObjet, sGuidVueSrvPhy);
                    LoadMainComposant_ServMComp(LstObjet);

                    break;
                case (int)DrawArea.DrawToolType.Gensas:
                    for (int i = 0; i < parent.drawArea.GraphicsList.Count; i++)
                    {
                        if (parent.drawArea.GraphicsList[i].GetType() == typeof(DrawGensas)) LstObjet.Add(parent.drawArea.GraphicsList[i]);
                    }

                    LoadObjet_Label(LstObjet, sGuidVueSrvPhy);
                    //LoadMainComposant_ServMComp(LstObjet);

                    break;
            }
            
        }

        public void LoadMainComposant_UserMComp(ArrayList LstUser)
        {
            for (int i = 0; i < LstUser.Count; i++)
            {
                DrawTechUser dtu = (DrawTechUser)LstUser[i];
                if (dtu.GetNbrChild(typeof(DrawMainComposant)) == 0)
                {
                    ArrayList LstMainComposant = new ArrayList();
                    if (CBRecherche("SELECT GuidMainComposant FROM AppUserMComp WHERE GuidAppUser='" + dtu.GuidkeyObjet + "'"))
                        while (Reader.Read()) LstMainComposant.Add(Reader.GetString(0));
                    CBReaderClose();
                    for (int j = 0; j < LstMainComposant.Count; j++) dtu.LoadMainComposant_UserMComp((string)LstMainComposant[j]);
                    dtu.AligneObjet();
                }
            }
        }

        public void LoadMainComposant_ServMSas(ArrayList LstGensas)
        {
            for (int i = 0; i < LstGensas.Count; i++)
            {
                DrawGensas dgs = (DrawGensas)LstGensas[i];
                if (dgs.GetNbrChild(typeof(DrawMainComposant)) == 0)
                {
                    ArrayList LstMainComposant = new ArrayList();
                    if (CBRecherche("SELECT GuidMainComposant FROM MCompSas WHERE GuidGensas='" + dgs.GuidkeyObjet + "'"))
                        while (Reader.Read()) LstMainComposant.Add(Reader.GetString(0));
                    CBReaderClose();
                    for (int j = 0; j < LstMainComposant.Count; j++) dgs.LoadMainComposant_Serv((string)LstMainComposant[j]);
                    dgs.AligneObjet();
                }
            }
        }

        public void LoadMainComposant_ServMpod(ArrayList LstGenpod)
        {
            for (int i = 0; i < LstGenpod.Count; i++)
            {
                DrawGenpod dgp = (DrawGenpod)LstGenpod[i];
                if (dgp.GetNbrChild(typeof(DrawMainComposant)) == 0)
                {
                    ArrayList LstMainComposant = new ArrayList();
                    if (CBRecherche("SELECT GuidMainComposant FROM MComppod WHERE GuidGenpod='" + dgp.GuidkeyObjet + "'"))
                        while (Reader.Read()) LstMainComposant.Add(Reader.GetString(0));
                    CBReaderClose();
                    for (int j = 0; j < LstMainComposant.Count; j++) dgp.LoadMainComposant_Serv((string)LstMainComposant[j]);
                    dgp.AligneObjet();
                }
            }
        }

        public void LoadMainComposant_ServMComp(ArrayList LstServer)
        {
            for (int i = 0; i < LstServer.Count; i++)
            {
                DrawServer ds = (DrawServer)LstServer[i];
                if (ds.GetNbrChild(typeof(DrawMainComposant)) == 0)
                {
                    ArrayList LstMainComposant = new ArrayList();
                    if (CBRecherche("SELECT GuidMainComposant FROM MCompApp WHERE GuidServer='" + ds.GuidkeyObjet + "'"))
                        while (Reader.Read()) LstMainComposant.Add(Reader.GetString(0));
                    CBReaderClose();
                    for (int j = 0; j < LstMainComposant.Count; j++) ds.LoadMainComposant_Serv((string)LstMainComposant[j]);
                    ds.AligneObjet();
                }
            }
        }

        public void LoadUserType_Techno(ArrayList LstUser)
        {
            for (int i = 0; i < LstUser.Count; i++)
            {
                DrawTechUser dtu = (DrawTechUser)LstUser[i];
                if (dtu.GetNbrChild(typeof(DrawServerType)) == 0)
                {
                    ArrayList LstUserType = new ArrayList();
                    if (CBRecherche("SELECT GuidServerType FROM AppUserTypeLink WHERE GuidAppUser='" + dtu.GuidkeyObjet + "'"))
                        while (Reader.Read()) LstUserType.Add(Reader.GetString(0));
                    CBReaderClose();
                    for (int j = 0; j < LstUserType.Count; j++) dtu.LoadUserType_Techno((string)LstUserType[j]);
                    dtu.AligneObjet();
                }
            }
        }

        public void LoadNcard(ArrayList LstServerPhy)
        {
            for (int i = 0; i < LstServerPhy.Count; i++)
            {
                DrawServerPhy ds = (DrawServerPhy)LstServerPhy[i];
                if (ds.GetNbrChild(typeof(DrawNCard)) == 0)
                {
                    ArrayList LstNCard = new ArrayList();
                    if (CBRecherche("SELECT GuidNCard FROM NCard WHERE GuidHote='" + ds.GuidkeyObjet + "' Order by NomNCard"))
                        while (Reader.Read()) LstNCard.Add(Reader.GetString(0));
                    CBReaderClose();
                    for (int j = 0; j < LstNCard.Count; j++)
                        ds.LoadNCard((string)LstNCard[j]);
                    //ds.AligneObjet();
                }
            }
        }

        public void LoadNameSpace(ArrayList LstInsks)
        {
            for (int i = 0; i < LstInsks.Count; i++)
            {
                DrawInsks diks = (DrawInsks)LstInsks[i];

                ArrayList Lstns = new ArrayList();
                // avoir la liste instancier sur le ks par rapport à la vue
                if (CBRecherche("Select GuidInsns From Insns Where GuidVue = '" + parent.GuidVue + "' and GuidInsks = '" + diks.GetValueFromName("GuidInsks") + "'"))
                {
                    while (Reader.Read()) Lstns.Add(Reader.GetString(0));
                    CBReaderClose();
                    for (int j = 0; j < Lstns.Count; j++) diks.Loadns((string)Lstns[j]);
                }
                else
                {
                    CBReaderClose();
                    // Il n'existe pas d'instance namespace sur la vue
                    ToolInsns tins = (ToolInsns)parent.drawArea.tools[(int)DrawArea.DrawToolType.Insns];
                    tins.CreatObjetFromks(parent.drawArea, diks);


                }
                diks.AligneObjet();
            }
        }

        public void LoadNode(ArrayList LstInsks)
        {
            for (int i = 0; i < LstInsks.Count; i++)
            {
                // pour le node
                // avoir la liste instancier sur l'objet et non la vue
                DrawInsks diks = (DrawInsks)LstInsks[i];
                ArrayList Lstnd = new ArrayList();
                // avoir la liste des noeuds instanciés sur le ks 
                if (CBRecherche("Select GuidInsnd From Insnd Where GuidInsks = '" + diks.GetValueFromName("GuidInsks") + "'"))
                {
                    while (Reader.Read()) Lstnd.Add(Reader.GetString(0));
                    CBReaderClose();
                    for (int j = 0; j < Lstnd.Count; j++) diks.Loadnd((string)Lstnd[j]);
                }
                else
                {
                    CBReaderClose();
                    /*
                    // Il n'existe pas d'instance node sur le ks
                    ToolInsnd tind = (ToolInsnd)parent.drawArea.tools[(int)DrawArea.DrawToolType.Insnd];
                    tind.CreatObjetFromks(parent.drawArea, diks);
                    */
                }
                
                diks.AligneObjet();
            }
        }

        public void LoadServerPhy_Cluster(ArrayList LstCluster)
        {
            for (int i = 0; i < LstCluster.Count; i++)
            {
                DrawCluster dc = (DrawCluster)LstCluster[i];
                if (dc.GetNbrChild(typeof(DrawServerPhy)) == 0)
                {
                    ArrayList LstServerPhy = new ArrayList();
                    if (CBRecherche("SELECT GuidServerPhy FROM ServerPhy WHERE GuidCluster='" + dc.GuidkeyObjet + "' Order By NomServerPhy"))
                        while (Reader.Read()) LstServerPhy.Add(Reader.GetString(0));
                    CBReaderClose();
                    for (int j = 0; j < LstServerPhy.Count; j++)
                        dc.LoadServerPhy((string)LstServerPhy[j]);
                    dc.AligneObjet();
                }
            }
        }

        public void LoadServerType_Techno(ArrayList LstServer)
        {
            for (int i = 0; i < LstServer.Count; i++)
            {
                DrawServer ds = (DrawServer) LstServer[i];

                if (ds.GetNbrChild(typeof(DrawServerType)) == 0)
                {
                    ArrayList LstServerType = new ArrayList();
                    if (CBRecherche("SELECT GuidServerType FROM ServerTypeLink WHERE GuidServer='" + ds.GuidkeyObjet + "'"))
                        while (Reader.Read()) LstServerType.Add(Reader.GetString(0));
                    CBReaderClose();
                    //for (int j = 0; j < LstServerType.Count; j++) ds.LoadServerType_Techno((string)LstServerType[j]);
                    for (int j = 0; j < LstServerType.Count; j++) ds.LoadServerType((string)LstServerType[j]);
                    ds.AligneObjet();
                }
            }
        }

        public void LoadSvcServerType_Techno(ArrayList LstGensas)
        {
            for (int i = 0; i < LstGensas.Count; i++)
            {
                DrawGensas dgs = (DrawGensas)LstGensas[i];

                if (dgs.GetNbrChild(typeof(DrawServerType)) == 0)
                {
                    ArrayList LstServerType = new ArrayList();
                    if (CBRecherche("SELECT GuidServerType FROM SvcServerTypeLink WHERE GuidGensas='" + dgs.GuidkeyObjet + "'"))
                        while (Reader.Read()) LstServerType.Add(Reader.GetString(0));
                    CBReaderClose();
                    //for (int j = 0; j < LstServerType.Count; j++) ds.LoadServerType_Techno((string)LstServerType[j]);
                    for (int j = 0; j < LstServerType.Count; j++) dgs.LoadServerType((string)LstServerType[j]);
                    dgs.AligneObjet();
                }
            }
        }

        public void LoadContainer_Techno(ArrayList LstGenpod)
        {
            for (int i = 0; i < LstGenpod.Count; i++)
            {
                DrawGenpod dgp = (DrawGenpod)LstGenpod[i];
                if (dgp.GetNbrChild(typeof(DrawContainer)) == 0)
                {
                    ArrayList LstContainer = new ArrayList();
                    if (CBRecherche("SELECT GuidContainer FROM ContainerLink WHERE GuidGenpod='" + dgp.GuidkeyObjet + "'"))
                        while (Reader.Read()) LstContainer.Add(Reader.GetString(0));
                    CBReaderClose();
                    for (int j = 0; j < LstContainer.Count; j++) dgp.LoadContainer_Techno((string)LstContainer[j]);
                    dgp.AligneObjet();
                }
            }
        }

        public void LoadManagedsvc_Techno(ArrayList LstGensas)
        {
            for (int i = 0; i < LstGensas.Count; i++)
            {
                DrawGensas dgs = (DrawGensas)LstGensas[i];
                if (dgs.GetNbrChild(typeof(DrawManagedsvc)) == 0)
                {
                    ArrayList LstManagedsvc = new ArrayList();
                    if (CBRecherche("SELECT GuidManagedsvc FROM ManagedsvcLink WHERE GuidGensas='" + dgs.GuidkeyObjet + "'"))
                        while (Reader.Read()) LstManagedsvc.Add(Reader.GetString(0));
                    CBReaderClose();
                    for (int j = 0; j < LstManagedsvc.Count; j++) dgs.LoadManagedsvc_Techno((string)LstManagedsvc[j]);
                    dgs.AligneObjet();
                }
            }
        }

        public void LoadObjet_Label(ArrayList LstGenObjet, string sGuidVueSrvPhy)
        {
            for (int i = 0; i < LstGenObjet.Count; i++)
            {
                DrawGensas dobj = (DrawGensas)LstGenObjet[i];
                ArrayList LstInsobj = new ArrayList();
                
                
                if (CBRecherche("select distinct Inssas.GuidInssas, NomInssas from Inssas, GInssas, DansVue, Vue, Gensas where Inssas.GuidInssas = GInssas.GuidInssas and GInssas.GuidGInssas = DansVue.GuidObjet and DansVue.GuidGVue = Vue.GuidGVue and Vue.GuidVue = '" + sGuidVueSrvPhy + "' and Inssas. GuidGensas = '" + dobj.GuidkeyObjet + "'"))
                    while (Reader.Read()) LstInsobj.Add(Reader.GetString(0));
                CBReaderClose();
                for (int j = 0; j < LstInsobj.Count; j++) dobj.LoadInsobj_Label((string)LstInsobj[j]);
                dobj.AligneObjet();
            }
        }

        public void LoadServerPhy_NCard(ArrayList LstServer, string sGuidVueSrvPhy)
        {
            for (int i = 0; i < LstServer.Count; i++)
            {
                DrawServer ds = (DrawServer)LstServer[i];
                ArrayList LstServerPhy = new ArrayList();
                if (CBRecherche("SELECT GuidServerPhy FROM ServerLink WHERE GuidServer='" + ds.GuidkeyObjet + "' AND GuidVue='" + sGuidVueSrvPhy + "'"))
                    while (Reader.Read()) LstServerPhy.Add(Reader.GetString(0));
                CBReaderClose();
                for (int j = 0; j < LstServerPhy.Count; j++) ds.LoadServerPhy_NCard((string)LstServerPhy[j]);
                ds.AligneObjet();
            }
        }

        /*public void RestoreLink()
        {
            for (int i = 0; i < parent.drawArea.GraphicsList.Count; i++)
            {
                if (parent.drawArea.GraphicsList[i].GetType() == typeof(DrawNCard))
                {
                    DrawNCard dnc = (DrawNCard)parent.drawArea.GraphicsList[i];
                    dnc.saveLinkAlias();
                }
            }
        }*/

        public void LoadTechLinkApp()
        {
            string sQuery = "SELECT TechLinkApp.GuidTechLink, TechLinkApp.GuidLink, NomLink FROM DansVue, GTechLink, TechLinkApp, Link WHERE GuidGVue ='" + parent.GuidGVue + "' AND GuidObjet=GuidGTechLink AND GTechLink.GuidTechLink=TechLinkApp.GuidTechLink AND TechLinkApp.GuidLink=Link.GuidLink ORDER BY TechLinkApp.GuidTechLink";
            
            if (CBRecherche(sQuery))
            {
                DrawTechLink dl = null;
                string Link = "";
                int n;

                while (Reader.Read())
                {
                    if (dl != null && dl.GuidkeyObjet.ToString() == Reader.GetString(0))
                        Link += ";" + Reader.GetString(2) + "  (" + Reader.GetString(1) + ")";
                    else
                    {
                        if (dl != null) dl.InitProp("LinksApplicatifs", (object)Link, true);

                        n = parent.drawArea.GraphicsList.FindObjet(0, Reader.GetString(0));
                        if (n > -1)
                        {
                            dl = (DrawTechLink)parent.drawArea.GraphicsList[n];
                            Link = Reader.GetString(2) + "  (" + Reader.GetString(1) + ")";
                        }
                    }
                }
                if (dl != null) dl.InitProp("LinksApplicatifs", (object)Link, true);
                CBReaderClose();
            }
            else CBReaderClose();
        }

        public void LoadAlias()
        {
            string sQuery = "SELECT Distinct NCard.GuidNCard, GuidAlias, NomAlias FROM DansVue, GNCard, NCard, Alias WHERE GuidGVue ='" + parent.GuidGVue + "' AND GuidObjet=GuidGNCard AND GNCard.GuidNCard=NCard.GuidNCard AND NCard.GuidNCard=Alias.GuidNCard ORDER BY NCard.GuidNCard";
            if (CBRecherche(sQuery))
            {
                DrawNCard dnc = null;
                string Alias = "";
                int n;

                while (Reader.Read())
                {
                    if (dnc != null && dnc.GuidkeyObjet.ToString() == Reader.GetString(0))
                        Alias += ";" + Reader.GetString(2) + "  (" + Reader.GetString(1) + ")";
                    else
                    {
                        if (dnc != null) dnc.InitProp("Alias", (object)Alias, true);

                        n = parent.drawArea.GraphicsList.FindObjet(0, Reader.GetString(0));
                        if (n > -1)
                        {
                            dnc = (DrawNCard)parent.drawArea.GraphicsList[n];
                            Alias = Reader.GetString(2) + "  (" + Reader.GetString(1) + ")";
                        }
                    }
                }
                if (dnc != null) dnc.InitProp("Alias", (object)Alias, true);
                CBReaderClose();
            }
            else CBReaderClose();
        }

        public void LoadNCardLinkIn()
        {
            string sQuery = "SELECT Distinct NCardLinkIn.GuidNCard, NCardLinkIn.GuidTechLink, NomTechLink, GuidAlias FROM DansVue, GNCard, NCardLinkIn, TechLink WHERE GuidGVue ='" + parent.GuidGVue + "' AND GuidObjet=GuidGNCard AND GNCard.GuidNCard=NCardLinkIn.GuidNCard AND NCardLinkIn.GuidTechLink=TechLink.GuidTechLink ";
            sQuery += "AND TechLink.GuidTechLink IN (Select GuidTechLink FROM Vue, DansVue, GTechLink Where Vue.GuidVue='" + parent.sGuidVueInf + "' AND Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGTechLink)  ORDER BY NCardLinkIn.GuidNCard";
            if (CBRecherche(sQuery))
            {
                DrawNCard dnc = null;
                string Link = "";
                int n;

                while (Reader.Read())
                {
                    if (dnc != null && dnc.GuidkeyObjet.ToString() == Reader.GetString(0))
                        if(Reader.IsDBNull(3)) Link += ";" + Reader.GetString(2) + "  (" + Reader.GetString(1) + ")";
                        else Link += ";" + Reader.GetString(2) + "  (" + Reader.GetString(1) + ":" + Reader.GetString(3) + ")";
                    else
                    {
                        if (dnc != null) dnc.InitProp("FluxIn", (object)Link, true);

                        n = parent.drawArea.GraphicsList.FindObjet(0, Reader.GetString(0));
                        if (n > -1)
                        {
                            dnc = (DrawNCard)parent.drawArea.GraphicsList[n];
                            if (Reader.IsDBNull(3)) Link = Reader.GetString(2) + "  (" + Reader.GetString(1) + ")";
                            else Link = Reader.GetString(2) + "  (" + Reader.GetString(1) + ":" + Reader.GetString(3) + ")";
                        }
                    }
                }
                if (dnc != null) dnc.InitProp("FluxIn", (object)Link, true);
                CBReaderClose();
            }
            else CBReaderClose();
        }


        public void LoadNCardLink(string Sens)
        {
            string sQuery = "SELECT Distinct NCardLink" + Sens + ".GuidNCard, NCardLink" + Sens + ".GuidTechLink, NomTechLink FROM DansVue, GNCard, NCardLink" + Sens + ", TechLink WHERE GuidGVue ='" + parent.GuidGVue + "' AND GuidObjet=GuidGNCard AND GNCard.GuidNCard=NCardLink" + Sens + ".GuidNCard AND NCardLink" + Sens + ".GuidTechLink=TechLink.GuidTechLink ";
            sQuery += "AND TechLink.GuidTechLink IN (Select GuidTechLink FROM Vue, DansVue, GTechLink Where Vue.GuidVue='" + parent.sGuidVueInf + "' AND Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGTechLink) ORDER BY NCardLink" + Sens + ".GuidNCard";
            
            //string sQuery = "SELECT NomTechLink FROM DansVue, GTechLink, TechLink WHERE GuidVue ='" + parent.GuidVue + "' AND GuidObjet=GuidGTechLink AND GTechLink.GuidTechLink=TechLink.GuidTechLink";
            //string sQuery = "SELECT NCardLink" + Sens + ".GuidNCard, NCardLink" + Sens + ".GuidTechLink, NomTechLink FROM DansVue, GTechLink, TechLink, NCardLink" + Sens + " WHERE GuidVue ='" + parent.GuidVue + "' AND GuidObjet=GuidGTechLink AND GTechLink.GuidTechLink=TechLink.GuidTechLink  AND NCardLink" + Sens + ".GuidTechLink=GTechLink.GuidTechLink ORDER BY NCardLink" + Sens + ".GuidNCard";

            if (CBRecherche(sQuery))
            {
                DrawNCard dnc = null;
                string Link = new string(' ', 0);
                int n;

                while (Reader.Read())
                {
                    if (dnc != null && dnc.GuidkeyObjet.ToString() == Reader.GetString(0))
                        Link += ";" + Reader.GetString(2) + "  (" + Reader.GetString(1) + ")";
                    else
                    {
                        if (dnc != null) dnc.InitProp("Flux" + Sens, (object) Link, true);

                        n = parent.drawArea.GraphicsList.FindObjet(0, Reader.GetString(0));
                        if (n > -1)
                        {
                            dnc = (DrawNCard)parent.drawArea.GraphicsList[n];
                            Link = new string(' ',0);
                            Link = Reader.GetString(2) + "  (" + Reader.GetString(1) + ")";
                        }
                    }
                }
                if (dnc != null) dnc.InitProp("Flux" + Sens, (object)Link, true);
                CBReaderClose();
            }
            else CBReaderClose();
        }

        public int LinkPointVlanWithCard(DrawVLan dvl, DrawObject oLink, int iTopY, int iBottomY)
        {
            int Hauteur = 0;
            ArrayList lstIndexPointVlanX = new ArrayList();
            
            for (int j = 0; j < dvl.pointArray.Count; j++)
            {
                int x = ((Point)dvl.pointArray[j]).X;
                if (x >= oLink.XMin() && x <= oLink.XMax()) lstIndexPointVlanX.Add(j);
            }

            //int iTopY = oParent.GetTopYNCard(), iBottomY = oParent.GetBottomYNCard();
            int index = -1, iCard = iTopY, deltaMax = 10, delta = deltaMax;
            for (int j = 0; j < lstIndexPointVlanX.Count; j++)
            {
                delta = Math.Abs((((Point)dvl.pointArray[(int)lstIndexPointVlanX[j]]).Y - iTopY));
                if (delta < deltaMax)
                {
                    if (index == -1) index = (int)lstIndexPointVlanX[j];

                    if (Math.Abs((((Point)dvl.pointArray[index]).Y - iCard)) > delta)
                    {
                        index = (int)lstIndexPointVlanX[j]; iCard = iTopY;
                    }
                }
                delta = Math.Abs((((Point)dvl.pointArray[(int)lstIndexPointVlanX[j]]).Y - iBottomY));
                if (delta < deltaMax)
                {
                    if (index == -1) index = (int)lstIndexPointVlanX[j];

                    if (Math.Abs((((Point)dvl.pointArray[index]).Y - iCard)) > delta)
                    {
                        index = (int)lstIndexPointVlanX[j]; iCard = iBottomY;
                    }
                }
            }
            if (index != -1 && dvl.pointArray.Count > dvl.LstLinkOut.Count)
            {
                int indexFromPointArray = dvl.LstLinkOut.Count;
                oLink.AttachLink(dvl, DrawObject.TypeAttach.Entree);
                dvl.AttachLink(oLink, DrawObject.TypeAttach.Sortie);

                Point pt = (Point)dvl.pointArray[index];


                if (iCard == iTopY)
                {
                    pt.Y = iTopY;
                }
                else
                {
                    Hauteur = 1;
                    pt.Y = iBottomY;
                }
                if (index == indexFromPointArray) dvl.pointArray[index] = pt;
                else
                {
                    Point ptTemp = (Point)dvl.pointArray[indexFromPointArray];
                    dvl.pointArray[indexFromPointArray] = pt;
                    dvl.pointArray[index] = ptTemp;
                }
            }
            return Hauteur;
        }

        public void LoadRouterLink()
        {
            
            string sQuery = "SELECT RouterLink.GuidRouter, RouterLink.GuidVLan FROM DansVue, GRouter, RouterLink WHERE GuidGVue ='" + parent.GuidGVue + "' AND GuidObjet=GuidGRouter AND GRouter.GuidRouter=RouterLink.GuidRouter ORDER BY RouterLink.GuidRouter";

            if (CBRecherche(sQuery))
            {
                DrawRouter dr = null;
                int n;

                while (Reader.Read())
                {
                    if (dr != null && dr.GuidkeyObjet.ToString() == Reader.GetString(0))
                    {
                        n = parent.drawArea.GraphicsList.FindObjet(0, Reader.GetString(1));
                        if (n > -1)
                        {
                            DrawVLan dv = (DrawVLan)parent.drawArea.GraphicsList[n];
                            LinkPointVlanWithCard(dv, dr, dr.YMin(), dr.YMax());
                        }
                    }
                    else
                    {
                        n = parent.drawArea.GraphicsList.FindObjet(0, Reader.GetString(0));
                        if (n > -1)
                        {
                            dr = (DrawRouter)parent.drawArea.GraphicsList[n];

                            n = parent.drawArea.GraphicsList.FindObjet(0, Reader.GetString(1));
                            if (n > -1)
                            {
                                DrawVLan dv = (DrawVLan)parent.drawArea.GraphicsList[n];
                                LinkPointVlanWithCard(dv, dr, dr.YMin(), dr.YMax());
                            }
                        }
                    }
                }
                CBReaderClose();
            }
            else CBReaderClose();
        }

        public string CreatObjectString(DrawObject o)
        {
            string sType = o.GetTypeSimpleTable();
            string RequeteField = "INSERT INTO " + o.GetTable(sType) + " (";
            string RequeteValue = "VALUES (";

            Table t;
            int n = ConfDB.FindTable(sType);
            if (n > -1)
            {
                t = (Table)ConfDB.LstTable[n];
                bool InsField = false;
                for (int i = 0; i < t.LstField.Count; i++)
                {
                    if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.InterneBD) != 0)
                    {
                        switch (((Field)t.LstField[i]).Type)
                        {
                            case 's':
                                if ((string)o.LstValue[i] != "")
                                {
                                    if (InsField) { RequeteField += ", "; RequeteValue += ", "; }
                                    RequeteField += ((Field)t.LstField[i]).Name;
                                    RequeteValue += "'" + o.LstValue[i] + "'";
                                    InsField = true;
                                }
                                break;
                            case 'p': //picture
                            case 'q': //picture
                            case 'i':
                                //if ((int)o.LstValue[i] != 0)
                                //{
                                    if (InsField) { RequeteField += ", "; RequeteValue += ", "; }
                                    RequeteField += ((Field)t.LstField[i]).Name;
                                    RequeteValue += o.LstValue[i];
                                    InsField = true;
                                //}
                                break;
                            case 'd':
                                if ((double)o.LstValue[i] != 0)
                                {
                                    if (InsField) { RequeteField += ", "; RequeteValue += ", "; }
                                    RequeteField += ((Field)t.LstField[i]).Name;
                                    RequeteValue += o.LstValue[i];
                                    InsField = true;
                                }
                                break;
                            case 't':
                                //if (o.LstValue[i] != null && o.LstValue[i].ToString() != "" && (DateTime)o.LstValue[i] != DateTime.MinValue)
                                if (o.LstValue[i] != null && o.LstValue[i].ToString() != "")
                                {
                                    DateTime dt = (DateTime)o.LstValue[i];
                                    if (InsField) { RequeteField += ", "; RequeteValue += ", "; }
                                    RequeteField += ((Field)t.LstField[i]).Name;
                                    RequeteValue += "'" + dt.ToString("yyyy-MM-dd") + "'";
                                    InsField = true;
                                }
                                break;
                        }
                    }
                }
                RequeteField += ") ";
                RequeteValue += ")";
            }

            return(RequeteField + RequeteValue);
        }

        public void CreatObject(DrawObject o)
        {
            CBWrite(CreatObjectString(o));
        }

        public XmlElement GetElFromInnerXml(XmlElement parent, string sInnerXmlSearch)
        {
            IEnumerator ienum = parent.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Text)
                    if (Node.InnerText == sInnerXmlSearch) return parent;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    XmlElement elss = GetElFromInnerXml((XmlElement) Node, sInnerXmlSearch);
                    if (elss != null) return elss;
                }
            }
            return null;
        }


        public void CreaXmlApplication(XmlDB xmlDB, XmlElement elApp, string sGuidApplication, string sGuidAppVersion, List<string []> lstVue, ArrayList lstLayer = null)
        {
            //XmlElement elApp = xmlDB.XmlGetElFromInnerText(xmlDB.root, sGuidApp);
            XmlElement elAppAfter = xmlDB.XmlGetFirstElFromParent(elApp, "After");
            for (int i = 0; i < lstVue.Count; i++)
            {
                string[] aEnregVue = lstVue[i]; //GuidVue, NomVue, GuidGVue, GuidEnvironnement, GuidVueInf, GuidTypeVue, NomTypeVue
                if (xmlDB.SetCursor(elAppAfter))
                {
                    string sGuidEnvironnement = aEnregVue[3];
                    if (aEnregVue[4] != "") parent.sGuidVueInf = (new Guid(aEnregVue[4])).ToString(); else parent.sGuidVueInf = null;

                    //parent.GuidApplication = new Guid(sGuidApp);
                    parent.wkApp = new WorkApplication(parent, sGuidApplication, null, sGuidAppVersion);

                    parent.ClearVue(true);

                    parent.GuidVue = new Guid(aEnregVue[0]);
                    parent.cbGuidVue.Items.Add(parent.GuidVue); parent.cbGuidVue.SelectedIndex = 0; // GuidVue
                    parent.cbVue.Items.Add(aEnregVue[1]); parent.cbVue.SelectedIndex = 0; //NomVue
                    parent.GuidGVue = new Guid(aEnregVue[2]); //GuidGVue
                    parent.tbTypeVue.Text = aEnregVue[6]; // NomTypeVue
                    parent.sTypeVue = aEnregVue[6]; // NomTypeVue
                    parent.GuidTypeVue = new Guid(aEnregVue[5]); // GuidTypeVue

                    parent.oCureo = new ExpObj(parent.GuidVue, (string)parent.cbVue.SelectedItem, DrawArea.DrawToolType.Vue);
                    parent.drawArea.tools[(int)parent.oCureo.ObjTool].LoadSimpleObjectSansGraph(parent.oCureo);
                    if (parent.oCureo.oDraw != null)
                    {
                        //Création de la Vue
                        DrawVue dv = (DrawVue)parent.oCureo.oDraw;
                        XmlElement elVue = dv.savetoXml(xmlDB, true);
                        parent.oCureo = null;
                        //parent.drawArea.GraphicsList[j];

                        //XmlElement elVue = xmlDB.XmlCreatEl(xmlDB.GetCursor(), "Vue", "NomVue,GuidTypeVue,GuidApplication", fields[0]);
                        //XmlElement elAtts = xmlDB.XmlGetFirstElFromParent(elVue, "Attributs");
                        //xmlDB.XmlSetAttFromEl(elAtts, "GuidVue", "s", fields[0]);
                        //xmlDB.XmlSetAttFromEl(elAtts, "NomVue", "s", fields[1]);
                        //if (sGuidEnvironnement != "") xmlDB.XmlSetAttFromEl(elAtts, "GuidEnvironnement", "s", sGuidEnvironnement);
                        //if (parent.sGuidVueInf != null) xmlDB.XmlSetAttFromEl(elAtts, "GuidVueInf", "s", parent.sGuidVueInf);
                        //xmlDB.XmlSetAttFromEl(elAtts, "GuidTypeVue", "s", fields[4]);
                        //xmlDB.XmlSetAttFromEl(elAtts, "GuidApplication", "s", sGuidApp);

                        //xmlDB.XmlCreatExternRef(xmlDB.XmlGetFirstElFromParent(elVue, "Before"), ConfDB.FindTable("Environnement"), sGuidEnvironnement);
                        //xmlDB.XmlCreatComment(elVue, fields[0]);

                        xmlDB.CursorClose();

                        parent.LoadVue();
                        int nbrObj = parent.drawArea.GraphicsList.Count;
                        DrawObject obj = null;
                        for (int j = 0; j < nbrObj; j++)
                        {
                            obj = parent.drawArea.GraphicsList[j];
                            xmlDB.CursorClose();
                            if (xmlDB.SetCursor(xmlDB.XmlGetFirstElFromParent(elVue, "After"))) obj.savetoXml(xmlDB, true);
                            xmlDB.CursorClose();
                        }
                    }
                    parent.drawArea.GraphicsList.Clear();
                }
            }
            // Création des layers à la suite des vue
            if (lstLayer != null)
            {
                ConfDB.AddLayer();
                ConfDB.AddLayerLink();

                for (int i = 0; i < lstLayer.Count; i++)
                {
                    //XmlElement elApp = xmlDB.XmlGetElFromInnerText(xmlDB.root, sGuidApp);
                    if (xmlDB.SetCursor(elAppAfter))
                    {
                        
                        string[] fields = ((string)lstLayer[i]).Split(','); //GuidLayer, NomLayer
                        parent.oCureo = new ExpObj(new Guid(fields[0]), fields[1], DrawArea.DrawToolType.Layer);
                        parent.drawArea.tools[(int)parent.oCureo.ObjTool].LoadSimpleObjectSansGraph(parent.oCureo);

                            DrawLayer dl = (DrawLayer)parent.oCureo.oDraw;
                            XmlElement elLayer = dl.savetoXml(xmlDB, true);
                            parent.oCureo = null;

                            xmlDB.CursorClose();
                            parent.drawArea.GraphicsList.Clear();

                        if (xmlDB.SetCursor(xmlDB.XmlGetFirstElFromParent(elLayer, "After")))
                        {

                            parent.drawArea.tools[(int)DrawArea.DrawToolType.LayerLink].LoadObjectSansGraph("Where GuidLayer = '" + fields[0] + "'");
                            for (int j = 0; j < parent.drawArea.GraphicsList.Count; j++)
                            {
                                DrawLayerLink dll = (DrawLayerLink)parent.drawArea.GraphicsList[j];
                                dll.savetoXml(xmlDB, true);
                            }

                            parent.drawArea.GraphicsList.Clear();
                            xmlDB.CursorClose();
                        }
                    }
                }
            }
        }

        public void CreatDansVue(Guid GuidObjet, string TypeObjet)
        {
            CBWrite("INSERT INTO DansVue (GuidGVue, GuidObjet, TypeObjet) VALUES ('" + parent.GuidGVue + "','" + GuidObjet + "','" + TypeObjet + "')");
        }

        public void CreatGLink(DrawPolygon dl)
        {
            string sType = dl.GetTypeSimpleTable();

            CBWrite("INSERT INTO G" + sType + " (GuidG" + sType + ", Guid" + sType + ", Pos) VALUES ('" + dl.Guidkey + "','" + dl.GuidkeyObjet + "'," + (int)dl.GetValueFromName("Pos") + ")");

            //Table GPoint
            for (int i = 0; i < dl.pointArray.Count; i++)
            {
                CBWrite("INSERT INTO GPoint (GuidGObjet, I, X, Y) VALUES ('" + dl.Guidkey + "','" + i + "','" + ((Point)dl.pointArray[i]).X + "','" + ((Point)dl.pointArray[i]).Y + "')");
            }
        }

        /*
        public void CreatGLink(DrawLink dl)
        {
            CBWrite("INSERT INTO GLink (GuidGLink, GuidLink, Pos) VALUES ('" + dl.Guidkey + "','" + dl.GuidkeyObjet + "',"  + (int) dl.GetValueFromName("Pos") + ")");
                    
            //Table GPoint
            for (int i = 0; i < dl.pointArray.Count; i++)
            {
                CBWrite("INSERT INTO GPoint (GuidGObjet, I, X, Y) VALUES ('" + dl.Guidkey + "','" + i + "','" + ((Point)dl.pointArray[i]).X + "','" + ((Point)dl.pointArray[i]).Y + "')");
            }
        }

        public void CreatGTechLink(DrawTechLink dl)
        {
            CBWrite("INSERT INTO GTechLink (GuidGTechLink, GuidTechLink, Pos) VALUES ('" + dl.Guidkey + "','" + dl.GuidkeyObjet + "'," + (int)dl.GetValueFromName("Pos") + ")");
            
            //Table GPoint
            for (int i = 0; i < dl.pointArray.Count; i++)
            {
                CBWrite("INSERT INTO GPoint (GuidGObjet, I, X, Y) VALUES ('" + dl.Guidkey + "','" + i + "','" + ((Point)dl.pointArray[i]).X + "','" + ((Point)dl.pointArray[i]).Y + "')");
            }
        }

        public void CreatGInterLink(DrawInterLink dl)
        {
            CBWrite("INSERT INTO GInterLink (GuidGInterLink, GuidInterLink, Pos) VALUES ('" + dl.Guidkey + "','" + dl.GuidkeyObjet + "'," + (int)dl.GetValueFromName("Pos") + ")");

            //Table GPoint
            for (int i = 0; i < dl.pointArray.Count; i++)
            {
                CBWrite("INSERT INTO GPoint (GuidGObjet, I, X, Y) VALUES ('" + dl.Guidkey + "','" + i + "','" + ((Point)dl.pointArray[i]).X + "','" + ((Point)dl.pointArray[i]).Y + "')");
            }
        }
        */
        public void CreatGZone(DrawZone dz)
        {
            CBWrite("INSERT INTO GZone (GuidGZone, GuidZone, Pos) VALUES ('" + dz.Guidkey + "','" + dz.GuidkeyObjet + "'," + (int)dz.GetValueFromName("Pos") + ")");

            //Table GPoint
            for (int i = 0; i < dz.pointArray.Count; i++)
            {
                CBWrite("INSERT INTO GPoint (GuidGObjet, I, X, Y) VALUES ('" + dz.Guidkey + "','" + i + "','" + ((Point)dz.pointArray[i]).X + "','" + ((Point)dz.pointArray[i]).Y + "')");
            }
        }

        public void CreatGObjectRect(DrawRectangle o)
        {
            string sType = o.GetTypeSimpleTable();
            //string sTypeF = o.GetsType(false);
            string sTypeG = o.GetTypeSimpleGTable();
            if (sTypeG == "GServerPhy")
            {
                //CBWrite("INSERT INTO " + sTypeG + " (Guid" + sTypeG + ", Guid" + sType + ", X, Y, Width, Height, Forme, thickColor, CPUCoreA, RAMA) VALUES ('" + o.Guidkey + "','" + o.GuidkeyObjet + "','" + o.Rectangle.X + "','" + o.Rectangle.Y + "','" + o.Rectangle.Width + "','" + o.Rectangle.Height + "','" + (int)o.GetValueFromName("Forme") + "','" + (int)o.GetValueFromName("thickColor") + "','" + (double)o.GetValueFromName("CPUCoreA") + "','" + (double)o.GetValueFromName("RAMA") + "')");
                CBWrite("INSERT INTO " + sTypeG + " (Guid" + sTypeG + ", Guid" + sType + ", X, Y, Width, Height, Forme, thickColor) VALUES ('" + o.Guidkey + "','" + o.GuidkeyObjet + "','" + o.Rectangle.X + "','" + o.Rectangle.Y + "','" + o.Rectangle.Width + "','" + o.Rectangle.Height + "','" + (int)o.GetValueFromName("Forme") + "','" + (int)o.GetValueFromName("thickColor") + "')");
            }
            else CBWrite("INSERT INTO " + sTypeG + " (Guid" + sTypeG + ", Guid" + sType + ", X, Y, Width, Height) VALUES ('" + o.Guidkey + "','" + o.GuidkeyObjet + "','" + o.Rectangle.X + "','" + o.Rectangle.Y + "','" + o.Rectangle.Width + "','" + o.Rectangle.Height + "')");
        }

        
        /*
        public void CreatGServMComp(DrawServMComp o)
        {
            string sTypeT = o.GetsType(true);
            string sTypeF = o.GetsType(false);
            CBWrite("INSERT INTO G" + sTypeT + " (GuidG" + sTypeT + ", Guid" + sTypeF + ", GuidServeur, X, Y, Width, Height) VALUES ('" + o.Guidkey + "','" + o.GuidkeyObjet + "','" + ((DrawServer) o.LstParent[0]).GuidkeyObjet + "','" + o.Rectangle.X + "','" + o.Rectangle.Y + "','" + o.Rectangle.Width + "','" + o.Rectangle.Height + "')");
        }
        */

        public void CreatGVLanPoint(DrawVLan dv)
        {
            //Table GPoint
            for (int i = 0; i < dv.pointArray.Count; i++)
            {
                if (!CBRecherche("Select GuidGObjet FROM GPoint WHERE GuidGObjet='" + dv.Guidkey + "' AND I=" + i))
                {
                    CBReaderClose();
                    CBWrite("INSERT INTO GPoint (GuidGObjet, I, X, Y) VALUES ('" + dv.Guidkey + "','" + i + "','" + ((Point)dv.pointArray[i]).X + "','" + ((Point)dv.pointArray[i]).Y + "')");
                }
                else CBReaderClose();
            }
        }

        public void CreatGSanSwitchPoint(DrawSanSwitch dss)
        {
            //Table GPoint
            for (int i = 0; i < dss.pointArray.Count; i++)
            {
                if (!CBRecherche("Select GuidGObjet FROM GPoint WHERE GuidGObjet='" + dss.Guidkey + "' AND I=" + i))
                {
                    CBReaderClose();
                    CBWrite("INSERT INTO GPoint (GuidGObjet, I, X, Y) VALUES ('" + dss.Guidkey + "','" + i + "','" + ((Point)dss.pointArray[i]).X + "','" + ((Point)dss.pointArray[i]).Y + "')");
                }
                else CBReaderClose();
            }
        }

        public void CreatGCnxPoint(DrawCnx dc)
        {
            //Table GPoint
            for (int i = 0; i < dc.pointArray.Count; i++)
            {
                if (!CBRecherche("Select GuidGObjet FROM GPoint WHERE GuidGObjet='" + dc.Guidkey + "' AND I=" + i))
                {
                    CBReaderClose();
                    CBWrite("INSERT INTO GPoint (GuidGObjet, I, X, Y) VALUES ('" + dc.Guidkey + "','" + i + "','" + ((Point)dc.pointArray[i]).X + "','" + ((Point)dc.pointArray[i]).Y + "')");
                }
                else CBReaderClose();
            }
        }

        public string GetSelectSearchKey(string[] Keys)
        {
            string sSelect = "";
            int index = 1;
            // si suppression battribut -> supprimer le substrings(index)
            if (bAttribut) index = 0;
            for (int i = 0; i < Keys.Length; i++)
            {
                sSelect += "," + Keys[i].Substring(index);
            }
            
            return sSelect.Substring(1);
        }

        public string XmlGetWhereSearchKey(XmlElement el, string[] Keys)
        {
            string sSelect = "";
            for (int i = 0; i < Keys.Length; i++)
            {
                if (bAttribut)
                {
                    XmlElement elAtts = parent.XmlFindFirstElFromName(el, "Attributs", 1);
                    if (elAtts != null)
                    {
                        switch (parent.XmlGetAttValueAFromAttValueB(elAtts, "Type", "Nom", Keys[i])[0])
                        {
                            case 's':
                                sSelect += " AND " + Keys[i] + "='" + parent.XmlGetAttValueAFromAttValueB(elAtts, "Value", "Nom", Keys[i]) + "'";
                                break;
                            case 'i':
                                sSelect += " AND " + Keys[i] + "=" + parent.XmlGetAttValueAFromAttValueB(elAtts, "Value", "Nom", Keys[i]);
                                break;
                            case 'd':
                                sSelect += " AND " + Keys[i] + "=" + parent.XmlGetAttValueAFromAttValueB(elAtts, "Value", "Nom", Keys[i]);
                                break;
                        }
                    }
                }
                else
                {
                    switch (Keys[i][0])
                    {
                        case 's':
                            sSelect += " AND " + Keys[i].Substring(1) + "='" + el.GetAttribute(Keys[i]) + "'";
                            break;
                        case 'i':
                            sSelect += " AND " + Keys[i].Substring(1) + "=" + el.GetAttribute(Keys[i]);
                            break;
                        case 'd':
                            sSelect += " AND " + Keys[i].Substring(1) + "=" + el.GetAttribute(Keys[i]);
                            break;
                    }
                }
            }
            return sSelect.Substring(5);
        }

        public void XmlUpdateFromXml(XmlElement el, string[] Keys)
        {
            string cmdUpdate = "UPDATE " + el.Name + " SET ";
            string setval = "";
            byte[] rawData = null;

            if (bAttribut)
            {
                XmlElement elAtts = parent.XmlFindFirstElFromName(el, "Attributs", 1);
                if (elAtts != null)
                {
                    IEnumerator ienum = elAtts.GetEnumerator();
                    XmlNode Node;

                    while (ienum.MoveNext())
                    {
                        Node = (XmlNode)ienum.Current;
                        if (Node.NodeType == XmlNodeType.Element)
                        {
                            XmlElement elAtt = (XmlElement)Node;
                            if (elAtt.Name == "Attribut")
                            {
                                switch (elAtt.GetAttribute("Type")[0])
                                {
                                    case 's':
                                        if (GetSelectSearchKey(Keys).IndexOf(elAtt.GetAttribute("Nom")) < 0) setval += ", " + elAtt.GetAttribute("Nom") + "='" + elAtt.GetAttribute("Value") + "'";
                                        break;
                                    case 'i':
                                        if (GetSelectSearchKey(Keys).IndexOf(elAtt.GetAttribute("Nom")) < 0) setval += ", " + elAtt.GetAttribute("Nom") + "=" + elAtt.GetAttribute("Value");
                                        break;
                                    case 'd':
                                        if (GetSelectSearchKey(Keys).IndexOf(elAtt.GetAttribute("Nom")) < 0) setval += ", " + elAtt.GetAttribute("Nom") + "=" + elAtt.GetAttribute("Value");
                                        break;
                                    case 'b':
                                        if (GetSelectSearchKey(Keys).IndexOf(elAtt.GetAttribute("Nom")) < 0 && rawData == null)
                                        {
                                            setval += ", " + elAtt.GetAttribute("Nom") + "= ?";
                                            rawData = StringToByteArray(elAtt.GetAttribute("Value"));
                                        }
                                        break;
                                }

                            }
                        }
                    }
                }
            }
            else
            {
                XmlAttributeCollection lstAtt = el.Attributes;
                XmlAttribute att = lstAtt[0];

                for (int i = 1; i < lstAtt.Count; i++)
                {
                    switch (lstAtt[i].Name[0])
                    {
                        case 's':
                            if (att.Value.IndexOf(lstAtt[i].Name) < 0) setval += ", " + lstAtt[i].Name.Substring(1) + "='" + lstAtt[i].Value + "'";
                            break;
                        case 'i':
                            if (att.Name.IndexOf(lstAtt[i].Name) < 0) setval += ", " + lstAtt[i].Name.Substring(1) + "=" + lstAtt[i].Value;
                            break;
                        case 'd':
                            if (att.Name.IndexOf(lstAtt[i].Name) < 0) setval += ", " + lstAtt[i].Name.Substring(1) + "=" + lstAtt[i].Value;
                            break;
                        case 'b':
                            if (att.Name.IndexOf(lstAtt[i].Name) < 0 && rawData == null)
                            {
                                setval += ", " + lstAtt[i].Name.Substring(1) + "= ?";
                                rawData = StringToByteArray(lstAtt[i].Value);
                            }
                            break;
                    }
                }
            }
            //if (el.Name == "NCardLinkIn") MessageBox.Show(cmdUpdate + " " + setval.Substring(2) + " WHERE " + GetWhereSearchKey(el, Keys));
            if (el.Name != "NCardLinkIn")
            {
                // si table = NCardLinkIn il y aura obligatoirement une duplique entry : l'enregistrement est une clé à trois valeur donc non modifiable
                if (setval != "") CBWriteWithObj(cmdUpdate + " " + setval.Substring(2) + " WHERE " + XmlGetWhereSearchKey(el, Keys), rawData);
            }
        }

        public void XmlCreateFromXml(XmlElement el)
        {
            //if (el.Name == "GPoint")  {}
            string cmdInsert = "INSERT INTO " + el.Name + " (", cmdInsert2 = ") VALUES (";
            string setname = "", setval = "";
            byte[] rawData = null;

            if (bAttribut)
            {
                 XmlElement elAtts = parent.XmlFindFirstElFromName(el, "Attributs", 1);
                 if (elAtts != null)
                 {
                     IEnumerator ienum = elAtts.GetEnumerator();
                     XmlNode Node;
                     
                     while (ienum.MoveNext())
                     {
                         Node = (XmlNode)ienum.Current;
                         if (Node.NodeType == XmlNodeType.Element)
                         {
                             XmlElement elAtt = (XmlElement)Node;
                             if (elAtt.Name == "Attribut")
                             {
                                 switch (elAtt.GetAttribute("Type")[0])
                                 {
                                     case 's':
                                         setname += ", " + elAtt.GetAttribute("Nom");
                                         setval += ", '" + elAtt.GetAttribute("Value") + "'";
                                         break;
                                     case 'i':
                                         setname += ", " + elAtt.GetAttribute("Nom"); setval += ", " + elAtt.GetAttribute("Value");
                                         break;
                                     case 'd':
                                         setname += ", " + elAtt.GetAttribute("Nom"); setval += ", " + elAtt.GetAttribute("Value");
                                         break;
                                     case 'b':
                                         if (rawData == null)
                                         {
                                             setname += ", " + elAtt.GetAttribute("Nom");
                                             setval += ", ?";
                                             rawData = StringToByteArray(elAtt.GetAttribute("Value"));
                                         }
                                         break;
                                 }
                             }
                         }
                     }
                 }
            }
            else
            {
                XmlAttributeCollection lstAtt = el.Attributes;
                for (int i = 1; i < lstAtt.Count; i++)
                {
                    switch (lstAtt[i].Name[0])
                    {
                        case 's':
                            setname += ", " + lstAtt[i].Name.Substring(1); setval += ", '" + lstAtt[i].Value + "'";
                            break;
                        case 'i':
                            setname += ", " + lstAtt[i].Name.Substring(1); setval += ", " + lstAtt[i].Value;
                            break;
                        case 'd':
                            setname += ", " + lstAtt[i].Name.Substring(1); setval += ", " + lstAtt[i].Value;
                            break;
                        case 'b':
                            if (rawData == null)
                            {
                                setname += ", " + lstAtt[i].Name.Substring(1);
                                setval += ", ?";
                                rawData = StringToByteArray(lstAtt[i].Value);
                            }
                            break;
                    }
                }
            }
            //if (el.Name == "NCardLinkIn") MessageBox.Show(cmdInsert + " " + setname.Substring(2) + cmdInsert2 + setval.Substring(2) + ")");
            CBWriteWithObj(cmdInsert  + " " + setname.Substring(2) + cmdInsert2 + setval.Substring(2) + ")", rawData);
        }

        public byte[] StringToByteArray(String hData)
        {
            int iCars = hData.Length;
            byte[] bytes = new byte[iCars / 2];
            for (int i = 0; i < iCars; i += 2)
                bytes[i / 2] = Convert.ToByte(hData.Substring(i, 2), 16);
            return bytes;
        }
        
        private void CalcProvision(string sGuidVue, XmlExcel xmlExcel, bool full)
        {
            int n = -1;
            string sType = "", sqlString1 = "", sqlString2 = "", sqlString3 = "";
            if (full) { ConfDB.AddTabServerList(); sType = "ServerList"; }
            else { ConfDB.AddTabServerTechList(); sType = "ServerTechList"; }

            n = ConfDB.FindTable(sType);
            if (n > -1)
            {
                Table t = (Table)ConfDB.LstTable[n];
                string field = t.GetSelectFieldFromOption(ConfDataBase.FieldOption.NomCourt);

                if (full)
                {
                    //sqlString1 = "Select Distinct " + t.GetSelectFieldFromOption(ConfDataBase.FieldOption.NomCourt) + " From Application, AppVersion, Vue, Environnement, DansVue, GServerPhy, ServerPhy  Left Join LayerLink On ServerPhy.GuidServerPhy=GuidObj and layerlink.GuidAppVersion='" + parent.GetGuidAppVersion() + "', ServerLink, Server, NCard, VLan Where Vue.GuidVue='" + sGuidVue + "' and Application.GuidApplication=AppVersion.GuidApplication and Vue.GuidAppVersion=AppVersion.GuidAppVersion and Vue.GuidEnvironnement=Environnement.GuidEnvironnement and Vue.GuidGVue=DansVue.GuidGVue and GuidObjet=GServerPhy.GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and ServerPhy.GuidServerPhy=ServerLink.GuidServerPhy and ServerLink.GuidServer=Server.GuidServer and ServerPhy.GuidServerPhy=GuidHote and NCard.GuidVLan=VLan.GuidVLan" + parent.wkApp.GetWhereLayer();
                    sqlString1 = "Select Distinct " + t.GetSelectFieldFromOption(ConfDataBase.FieldOption.NomCourt) + " From Application, AppVersion, Vue, Environnement, DansVue, GServerPhy, ServerPhy  Left Join LayerLink On ServerPhy.GuidServerPhy=GuidObj and layerlink.GuidAppVersion='" + parent.GetGuidAppVersion() + "', ServerLink, Server, NCard Left Join VLan On NCard.GuidVLan=VLan.GuidVLan Where Vue.GuidVue='" + sGuidVue + "' and Application.GuidApplication=AppVersion.GuidApplication and Vue.GuidAppVersion=AppVersion.GuidAppVersion and Vue.GuidEnvironnement=Environnement.GuidEnvironnement and Vue.GuidGVue=DansVue.GuidGVue and GuidObjet=GServerPhy.GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and ServerPhy.GuidServerPhy=ServerLink.GuidServerPhy and ServerLink.GuidServer=Server.GuidServer and ServerPhy.GuidServerPhy=GuidHote" + parent.wkApp.GetWhereLayer();
                }
                else
                {
                    sqlString1 = "Select Distinct " + t.GetSelectFieldFromOption(ConfDataBase.FieldOption.NomCourt) + " From Application, AppVersion, Vue, Environnement, DansVue, GServerPhy, ServerPhy  Left Join LayerLink On ServerPhy.GuidServerPhy=GuidObj and layerlink.GuidAppVersion='" + parent.GetGuidAppVersion() + "', ServerLink, Server, NCard, VLan, ServerTypeLink, ServerType, Techno, TechnoRef Where Vue.GuidVue='" + sGuidVue + "' and Application.GuidApplication=AppVersion.GuidApplication and Vue.GuidAppVersion=AppVersion.GuidAppVersion and Vue.GuidEnvironnement=Environnement.GuidEnvironnement and Vue.GuidGVue=DansVue.GuidGVue and GuidObjet=GServerPhy.GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and ServerPhy.GuidServerPhy=ServerLink.GuidServerPhy and ServerLink.GuidServer=Server.GuidServer and ServerPhy.GuidServerPhy=GuidHote and NCard.GuidVLan=VLan.GuidVLan" + parent.wkApp.GetWhereLayer() + " and Server.GuidServer=ServerTypeLink.GuidServer and ServerTypeLink.GuidServerType=ServerType.GuidServerType and ServerType.GuidServerType=Techno.GuidTechnoHost and Techno.GuidTechnoRef=TechnoRef.GuidTechnoRef ";
                }

                sqlString2 = " and Server.GuidServer IN (SELECT Server.GuidServer FROM Vue vinf, Vue v, DansVue, GServer, Server WHERE vinf.GuidVue='" + sGuidVue + "' and vinf.GuidVueInf=v.GuidVue and v.GuidGVue=DansVue.GuidGVue AND DansVue.GuidObjet=GServer.GuidGServer AND GServer.GuidServer=Server.GuidServer)";
                sqlString3 = " order by NomServer, NomServerPhy, NomVLan";

                if (CBRecherche(sqlString1 + sqlString2 + sqlString3))
                {
                    while (Reader.Read())
                        xmlExcel.CreatXmlFromReader(sType, ConfDataBase.FieldOption.NomCourt);
                }
                CBReaderClose();
            }
        }

        public XmlElement xmlGetIndicatorObsolescence()
        {
            parent.docXml = new XmlDocument();
            parent.docXml.LoadXml("<Indicator></Indicator>");
            XmlElement root = parent.docXml.DocumentElement;

            //if (CBRecherche("Select ind1.GuidObjet, ind1.ValIndicator, ind2.ValIndicator From indicatorlink ind1, indicatorlink ind2  Where ind1.GuidObjet=ind2.GuidObjet and ind1.GuidIndicator='" + "3b854340-f70e-474b-8d4a-7eaa4263b9ec" + "' and ind2.GuidIndicator='" + "b00b12bd-a447-47e6-92f6-e3b76ad22830" + "'"))
            //if (CBRecherche("Select GuidTechnoRef, ConfinedStart, ConfinedEnd, ReferenceStart, DecommEnd From TechnoRef"))
            if (CBRecherche("Select GuidTechnoRef, ConfinedStart, ConfinedEnd, ReferenceStart, valindicator From TechnoRef, indicatorlink where technoref.GuidTechnoRef = indicatorlink.GuidObjet and indicatorlink.GuidIndicator = 'b00b12bd-a447-47e6-92f6-e3b76ad22830'"))
            {
                while (Reader.Read())
                {
                    XmlElement elrow = parent.docXml.CreateElement("row");
                    
                    if (!Reader.IsDBNull(0))
                    {
                        elrow.SetAttribute("GuidObjet", Reader.GetString(0));
                        if (Reader.IsDBNull(3) || Reader.IsDBNull(4))
                        {
                            if (Reader.IsDBNull(1)) elrow.SetAttribute("PeriodeStart", "0"); else elrow.SetAttribute("PeriodeStart", Reader.GetDateTime(1).ToOADate().ToString());
                            if (Reader.IsDBNull(2)) elrow.SetAttribute("PeriodeEnd", "0"); else elrow.SetAttribute("PeriodeEnd", Reader.GetDateTime(2).ToOADate().ToString());
                        }
                        else
                        {
                            elrow.SetAttribute("PeriodeStart", Reader.GetDateTime(3).ToOADate().ToString());
                            elrow.SetAttribute("PeriodeEnd", Reader.GetDouble(4).ToString());
                            //elrow.SetAttribute("PeriodeEnd", Reader.GetDateTime(4).ToOADate().ToString());
                        }

                        /*el = parent.docXml.CreateElement("GuidObjet");
                        el.InnerText = Reader.GetString(0); elrow.AppendChild(el);
                        el = parent.docXml.CreateElement("FinCommercial");
                        if (Reader.IsDBNull(1)) el.InnerText = "0"; else el.InnerText = Reader.GetDouble(1).ToString(); elrow.AppendChild(el);
                        el = parent.docXml.CreateElement("FinSupport");
                        if (Reader.IsDBNull(2)) el.InnerText = "0"; else el.InnerText = Reader.GetDouble(2).ToString(); elrow.AppendChild(el);*/
                        
                        root.AppendChild(elrow);
                    }
                }
            }
            CBReaderClose();
            parent.docXml.Save("c:\\dat\\temptechno.xml");
            return root;
        }

        public void Genere_ListApp()
        {
            //parent.docXml = new XmlDocument();
            //parent.docXml.LoadXml("<ListApplications></ListApplications>");
            //XmlElement root = parent.docXml.DocumentElement;

            // Export Serveur
            /*
            ArrayList aListVue = new ArrayList();
            if (CBRecherche("SELECT Distinct GuidVue FROM Vue, TypeVue WHERE Vue.GuidTypeVue='2a4c3691-e714-4d05-9400-8fbbb06f2d62' OR Vue.GuidTypeVue='ef667e58-a617-49fd-91a8-2beeda856475'  OR Vue.GuidTypeVue='7afca945-9d41-48fb-b634-5b6ffda90d4e'"))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml("<Export></Export>");
                XmlDocument xmlDoc2 = new XmlDocument();
                xmlDoc2.LoadXml("<row><Field Name=\"CI_NameServer\" Template=\"@NomServerPhy\"></Field><Field Name=\"CI_NameApplication\" Template=\"@Trigramme+-+@NomApplication+ - +@NomEnvironnement\"></Field></row>");
                while (Reader.Read())
                {
                    aListVue.Add(Reader.GetString(0));
                }
                CBReaderClose();

                for (int i = 0; i < aListVue.Count; i++)
                {
                    //NomVLan, IPAddr, Type, CPUCore, RAM
                    string sqlString1 = "Select Distinct  NomApplication, Trigramme, NomEnvironnement, NomServerPhy From Application, Vue, Environnement, DansVue, GServerPhy, ServerPhy, ServerLink, Server, NCard, VLan Where Vue.GuidVue='" + aListVue[i] + "' and Vue.GuidApplication=Application.GuidApplication and Vue.GuidEnvironnement=Environnement.GuidEnvironnement and Vue.GuidVue=DansVue.GuidVue and GuidObjet=GServerPhy.GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and ServerPhy.GuidServerPhy=ServerLink.GuidServerPhy and ServerLink.GuidServer=Server.GuidServer and ServerPhy.GuidServerPhy=GuidHote and NCard.GuidVLan=VLan.GuidVLan and ServerPhy.Type <> '' and (Application.GuidStatut is null or Application.GuidStatut<>'adc55f3f-abcc-41c9-a8b2-76b6083e8064') and Trigramme <> ''";
                    string sqlString2 = " and Server.GuidServer IN (SELECT Server.GuidServer FROM Vue vinf, Vue v, DansVue, GServer, Server WHERE vinf.GuidVue='" + aListVue[i] + "' and vinf.GuidVueInf=v.GuidVue and v.GuidVue=DansVue.GuidVue AND DansVue.GuidObjet=GServer.GuidGServer AND GServer.GuidServer=Server.GuidServer)";
                    
                    if (CBRecherche(sqlString1 + sqlString2))// + sqlString3))
                    {
                            XmlDocument xmlDoc1 = CreatXmlDocFromDB();
                            CreatXmlDocFromXmlFiles(xmlDoc, xmlDoc1, xmlDoc2);
                    }
                    CBReaderClose();

                }
                xmlDoc.Save(parent.sPathRoot + "\\ListServeur.xml");
            }
            else CBReaderClose();
            */


            // Export Application

            if (CBRecherche("Select Distinct * From Vue, Environnement, AppVersion Left Join Statut On Application.GuidStatut=Statut.GuidStatut, Application Left Join ApplicationType On Application.GuidApplicationType=ApplicationType.GuidApplicationType Left Join ApplicationClass On Application.GuidApplicationClass=ApplicationClass.GuidApplicationClass Where Application.GuidApplication=AppVersion.GuidApplication And AppVersion.GuidAppVersion=Vue.GuidAppVersion And Vue.GuidEnvironnement=Environnement.GuidEnvironnement and Trigramme <> '' and (AppVersion.GuidStatut is null or AppVersion.GuidStatut<>'adc55f3f-abcc-41c9-a8b2-76b6083e8064')"))
            //if (CBRecherche("Select Distinct * From Application, Vue, Environnement Where Application.GuidApplication=Vue.GuidApplication And Vue.GuidEnvironnement=Environnement.GuidEnvironnement"))
            {
                XmlDocument xmlDoc1 = CreatXmlDocFromDB();

                XmlDocument xmlDoc2 = new XmlDocument();
                xmlDoc2.LoadXml("<row><Field Name=\"CI_ID\" Template=\"@Trigramme+-+@NomApplication+ - +@NomEnvironnement\"></Field><Field Name=\"CI_Name\" Template=\"@Trigramme+-+@NomApplication+ - +@NomEnvironnement\"></Field><Field Name=\"CI_Description\" Template=\"@Trigramme+-+@NomApplication+ - +@NomEnvironnement\"></Field><Field Name=\"Tier1\" Template=\"Application\"></Field><Field Name=\"Tier2\" Template=\"@NomApplicationType\"></Field><Field Name=\"Tier3\" Template=\"@NomApplication+_(+@Trigramme+)\"></Field><Field Name=\"Product_Name\" Template=\"@Trigramme+-+@NomApplication+-+@NomApplicationClass\"></Field><Field Name=\"Environnement\" Template=\"@NomEnvironnement\"></Field><Field Name=\"Status\" Template=\"@NomStatut\"></Field></row>");
                XmlDocument xmlDoc = CreatXmlDocFromXmlFiles(xmlDoc1, xmlDoc2);
                xmlDoc.Save(parent.sPathRoot + "\\ListApplications.xml");
                //root.AppendChild(CreatXmlFromDB("Application", ConfDataBase.FieldOption.InterneBD));
            }
            CBReaderClose();
        }

        public void Genere_ListeServer2(XmlExcel xmlExcel, bool full)
        {
            ArrayList aListVue = new ArrayList();
           
            string requete = "SELECT Distinct GuidVue FROM Vue, TypeVue WHERE GuidAppVersion='" + parent.GetGuidAppVersion() + "' AND (Vue.GuidTypeVue='2a4c3691-e714-4d05-9400-8fbbb06f2d62' OR Vue.GuidTypeVue='ef667e58-a617-49fd-91a8-2beeda856475'  OR Vue.GuidTypeVue='7afca945-9d41-48fb-b634-5b6ffda90d4e')";


            if (CBRecherche(requete))
            {
                while (Reader.Read())
                {
                    aListVue.Add(Reader.GetString(0));
                }
                CBReaderClose();
                for (int i = 0; i < aListVue.Count; i++)
                {
                    //CalcProvision((string)aListVue[i], root);
                    CalcProvision((string)aListVue[i], xmlExcel, full);
                }
            }
            else CBReaderClose();
        }

        public XmlExcel Genere_ListeServer()
        {
            XmlExcel xmlExcel = new XmlExcel(parent, "ListServer");
            List<string[]> lstAppVersion = new List<string[]>();

            if (CBRecherche("SELECT Application.GuidApplication, NomApplication, AppVersion.GuidAppVersion FROM Application, AppVersion Where Application.GuidApplication = AppVersion.GuidApplication ORDER BY NomApplication"))            {
                while (Reader.Read())
                {
                    string[] aEnreg = new string[3];
                    aEnreg[0] = Reader.GetString(0);    // GuidApplication
                    aEnreg[1] = Reader.GetString(1);    // NomApplication
                    aEnreg[2] = Reader.GetString(2);    // GuidAppVersion
                    lstAppVersion.Add(aEnreg);
                }
            }
            CBReaderClose();
            
            for(int i=0; i<lstAppVersion.Count;i++)
            {
                parent.wkApp = new WorkApplication(parent,  lstAppVersion[i][0], lstAppVersion[i][1], lstAppVersion[i][2]);
                Genere_ListeServer2(xmlExcel, false);
            }
            parent.wkApp = null;
            
            return xmlExcel;
        }

        public XmlExcel Genere_ListeServer(bool full)
        {
            ArrayList aListVue = new ArrayList();
            XmlExcel xmlExcel = new XmlExcel(parent, "ListServer");
            Genere_ListeServer2(xmlExcel, full);
            
            return xmlExcel;
        }

        public void CreatEnregFromXml(XmlElement el)
        {
            XmlElement elParent = (XmlElement)el.ParentNode;

            XmlAttributeCollection lstAtt = elParent.Attributes;
            if(el.Name=="Attributs" && lstAtt.Count!=0 && lstAtt[0].Name== "SearchKey")
            //if (el.Name == "ProduitApp") att = att;
            {
                string[] Keys = lstAtt[0].Value.Split(',');
                if (CBRecherche("SELECT " + GetSelectSearchKey(Keys) + " FROM " + elParent.Name + " WHERE " + XmlGetWhereSearchKey(elParent, Keys)))
                {
                    CBReaderClose();
                    if (el.Name == "Vue")
                    {
                        //deleteVue + Objetsassocies
                        //DeleteNCardLink(el.GetAttribute("sGuidVue"), "In");
                        //DeleteNCardLink(el.GetAttribute("sGuidVue"), "Out");
                        //DeleteVue(el.GetAttribute("sGuidVue"));
                        DeleteVue(parent.XmlGetTextEl(elParent));
                        XmlCreateFromXml(elParent); // Create
                    }
                    else
                        XmlUpdateFromXml(elParent, Keys); //Update

                }
                else
                {
                    CBReaderClose();
                    XmlCreateFromXml(elParent); //Create
                }
            }
        }       

        public ArrayList CreatNcardHote(DrawRectangle ds)
        {
            ArrayList lstGuidNCard = new ArrayList();

            if (CBRecherche("SELECT GuidNCard FROM NCard WHERE GuidHote='" + ds.GuidkeyObjet + "' ORDER BY NomNCard"))
            {
                while (Reader.Read())
                {
                    lstGuidNCard.Add((object) Reader.GetString(0));                    
                }
                CBReaderClose();
            }
            CBReaderClose();
                        
            return lstGuidNCard;
        }

        public ArrayList CreatNameSpace(DrawRectangle dr)
        {
            ArrayList lstGuidNameSpace = new ArrayList();

            FormChangeProp fcp = new FormChangeProp(parent, null);
            fcp.AddlSourceFromDB("SELECT GuidInsns, NomInsns FROM Insns WHERE GuidInsks='" + dr.GuidkeyObjet + "' ORDER BY NomInsns", "Create");
            fcp.ShowDialog(parent);

            if (fcp.Valider)
            {
                string[] aValue = CmdText.Split('(', ')');
                for (int i = 1; i < aValue.Length; i += 2)
                    lstGuidNameSpace.Add(aValue[i].Trim());
            }
            return lstGuidNameSpace;
        }

        public ArrayList CreatNcardCluster(DrawRectangle ds)
        {
            ArrayList lstGuidNCard = new ArrayList();

            FormChangeProp fcp = new FormChangeProp(parent, null);
            fcp.AddlSourceFromDB("SELECT GuidNCard, NomNCard FROM NCard WHERE GuidHote='" + ds.GuidkeyObjet + "' ORDER BY NomNCard", "Create");
            fcp.ShowDialog(parent);

            if (fcp.Valider)
            {
                string[] aValue = CmdText.Split('(', ')');
                for (int i = 1; i < aValue.Length; i += 2)
                    lstGuidNCard.Add(aValue[i].Trim());
            }
            return lstGuidNCard;
        }

        public ArrayList CreatTechnoServer(DrawRectangle dst)
        {
            ArrayList lstGuidTechno = new ArrayList();

            if (CBRecherche("SELECT GuidTechno FROM Techno WHERE GuidTechnoHost='" + dst.GuidkeyObjet + "'"))
            {
                while (Reader.Read())
                {
                    lstGuidTechno.Add((object)Reader.GetString(0));
                }
                CBReaderClose();
            }
            CBReaderClose();
            return lstGuidTechno;
        }

        public ArrayList CreatInfNCard(DrawRectangle dst)
        {
            ArrayList lstInfNCard = new ArrayList();

            if (CBRecherche("SELECT GuidNCard FROM NCard WHERE GuidHote='" + dst.GuidkeyObjet + "'"))
            {
                while (Reader.Read())
                {
                    lstInfNCard.Add((object)Reader.GetString(0));
                }
                CBReaderClose();
            }
            CBReaderClose();
            return lstInfNCard;
        }

        public ArrayList CreatInfLabel(DrawRectangle dst)
        {
            ArrayList lstInfNCard = new ArrayList();

            if (CBRecherche("SELECT GuidNCard FROM NCard WHERE GuidHote='" + dst.GuidkeyObjet + "'"))
            {
                while (Reader.Read())
                {
                    lstInfNCard.Add((object)Reader.GetString(0));
                }
                CBReaderClose();
            }
            CBReaderClose();
            return lstInfNCard;
        }
        public ArrayList CreatMCompServ(DrawRectangle dmc)
        {
            ArrayList lstGuidMCompServ = new ArrayList();

            if (CBRecherche("SELECT GuidMCompServ FROM MCompServ WHERE GuidMainComposant='" + dmc.GuidkeyObjet + "'"))
            {
                while (Reader.Read())
                {
                    lstGuidMCompServ.Add((object)Reader.GetString(0));
                }
                CBReaderClose();
            }
            CBReaderClose();
            return lstGuidMCompServ;
        }

        public ArrayList CreatServerCluster(DrawRectangle ds)
        {
            ArrayList lstGuidServerPhy = new ArrayList();

            if (CBRecherche("SELECT GuidServerPhy FROM ServerPhy WHERE GuidCluster='" + ds.GuidkeyObjet + "' ORDER BY NomServerPhy"))
            {
                while (Reader.Read())
                {
                    lstGuidServerPhy.Add((object)Reader.GetString(0));
                }
                CBReaderClose();
            }
            CBReaderClose();
            return lstGuidServerPhy;
        }

        public List<String[]> GetlstGening(string sGuidGenpod)
        {
            List<String[]> lstGening = new List<String[]>();

            if (CBRecherche("Select svclink.GuidServerIn guiding, NomGening nom From Techlink podlink, Gensvc, Techlink svclink, Gening WHERE svclink.GuidServerIn = Gening.GuidGening And podlink.GuidServerIn = svclink.GuidServerOut And podlink.GuidServerIn = Gensvc.GuidGensvc And podlink.GuidServerOut = '" + sGuidGenpod + "'"))
            {
                while (Reader.Read())
                {
                    String[] aEnreg = new String[2];
                    aEnreg[0] = Reader.GetString(0); aEnreg[1] = Reader.GetString(1);
                    lstGening.Add(aEnreg);
                }
                CBReaderClose();
            }
            CBReaderClose();
            return lstGening;
        }
        

        public ArrayList GetlstNCardFromsvc(string sGuidInssvc)
        {
            ArrayList lstNCard = new ArrayList();

            if (CBRecherche("SELECT GuidNCard FROM NCard WHERE GuidHote='" + sGuidInssvc + "'"))
            {
                while (Reader.Read())
                {
                    lstNCard.Add((object)Reader.GetString(0));
                }
                CBReaderClose();
            }
            CBReaderClose();
            return lstNCard;
        }
        public List<String[]> GetlstGensvc(string sGuidGenpod)
        {
            List<String[]> lstGensvc = new List<String[]>();
            string sqlRequest = "SELECT GuidServerIn guid, NomGensvc nom FROM TechLink, Gensvc WHERE GuidServerIn = GuidGensvc and GuidServerOut = '" + sGuidGenpod + "'";

            sqlRequest += " Union Select linkref.GuidServerOut guid, NomGensvc nom FROM Techlink linkref, Gensvc WHERE GuidServerOut = GuidGensvc and linkref.GuidServerOut not in (";
            sqlRequest += "    Select linknext.GuidServerIn from TechLink linknext, Genpod where linknext.GuidServerOut = Genpod.GuidGenpod) and ";
            sqlRequest += "         linkref.GuidServerIn = '" + sGuidGenpod + "'";

            if (CBRecherche(sqlRequest))
            {
                while (Reader.Read())
                {
                    string[] aEnreg = new string[2];
                    aEnreg[0] = Reader.GetString(0); aEnreg[1] = Reader.GetString(1);
                    lstGensvc.Add(aEnreg);
                }
                CBReaderClose();
            }
            CBReaderClose();
            return lstGensvc;
        }

        public List<String[]> GetlstGenpod(int iTypeObjet, string sGuid)
        {
            List<String[]> lstGenpod = new List<string[]>();

            string request = "";

            switch (iTypeObjet) {
                case (int)DrawArea.DrawToolType.Genks:
                    request = "SELECT GuidGenpod, NomGenpod FROM Genpod WHERE GuidGenks='" + sGuid + "' ORDER BY NomGenpod";   
                    break;
                case (int)DrawArea.DrawToolType.Insks:
                    request = "SELECT GuidGenpod, NomGenpod FROM Genpod, Genks, Insks WHERE Genpod.GuidGenks = Genks.GuidGenks and Genks.GuidGenks = Insks.GuidGenks and GuidInsks='" + sGuid + "' ORDER BY NomGenpod";
                    break;
            }
            if (CBRecherche(request))
            {
                while (Reader.Read())
                {
                    string[] aEnreg = new string[2];
                    aEnreg[0] = Reader.GetString(0); aEnreg[1] = Reader.GetString(1);
                    lstGenpod.Add(aEnreg);
                }
            }
            CBReaderClose();
            return lstGenpod;
        }

        public List<String[]> GetlstInssvc(string sGuid)
        {
            List<String[]> lstInssvc = new List<string[]>();

            if (CBRecherche("SELECT GuidInssvc, GuidGensvc FROM Inssvc WHERE GuidInspod='" + sGuid + "' ORDER BY NomInssvc"))
            {
                while (Reader.Read())
                {
                    string[] aEnreg = new string[2];
                    aEnreg[0] = Reader.GetString(0); aEnreg[1] = Reader.GetString(1);
                    lstInssvc.Add(aEnreg);
                }
            }
            CBReaderClose();
            return lstInssvc;
        }

        public List<String[]> GetlstInsing(string sGuid)
        {
            List<String[]> lstInsing = new List<string[]>();

            if (CBRecherche("SELECT GuidInsing, GuidGening FROM Insing WHERE GuidInspod='" + sGuid + "' ORDER BY NomInsing"))
            {
                while (Reader.Read())
                {
                    string[] aEnreg = new string[2];
                    aEnreg[0] = Reader.GetString(0); aEnreg[1] = Reader.GetString(1);
                    lstInsing.Add(aEnreg);
                }
            }
            CBReaderClose();
            return lstInsing;
        }
        public List<String[]> GetlstInspod(string sGuid)
        {
            List<String[]> lstInspod = new List<string[]>();

            if (CBRecherche("SELECT GuidInspod, GuidGenpod FROM Inspod WHERE GuidInsns='" + sGuid + "' ORDER BY NomInspod"))
            {
                while (Reader.Read())
                {
                    string[] aEnreg = new string[2];
                    aEnreg[0] = Reader.GetString(0); aEnreg[1] = Reader.GetString(1);
                    lstInspod.Add(aEnreg);
                }
            }
            CBReaderClose();
            return lstInspod;
        }

        public ArrayList CreatServerLocation(DrawLocation dl)
        {
            ArrayList lstGuidServerPhy = new ArrayList();

            if (CBRecherche("SELECT GuidServerPhy FROM ServerPhy WHERE GuidLocation='" + dl.GuidkeyObjet + "' ORDER BY NomServerPhy"))
            {
                
                while (Reader.Read())
                {
                    TreeNode[] ArrayTreeNode = parent.tvObjet.Nodes.Find(Reader.GetString(0), true);
                    if (ArrayTreeNode.Length > 0)
                        lstGuidServerPhy.Add((object)Reader.GetString(0));
                }
                CBReaderClose();
            }
            CBReaderClose();
            return lstGuidServerPhy;
        }
                
        public bool ExistGuid(DrawObject dob)
        {
            //string sType = dob.GetsType(false);
            string sType = dob.GetTypeSimpleTable();
                //dob.GetType().Name.Substring("Draw".Length);
            if (CBRecherche("SELECT Guid" + sType + " FROM " + sType + " WHERE Guid" + sType + " ='" + dob.GuidkeyObjet + "'"))
            {
                CBReaderClose();
                return true;
            }
            CBReaderClose();
            return false;
        }

        public bool ExistGuid(int idx, DrawObject dob)
        {
            //string sType = dob.GetsType(false);
            string sType = dob.GetTypeSimpleTable();
            //dob.GetType().Name.Substring("Draw".Length);
            if (CBRecherche("SELECT Guid" + sType + " FROM " + sType + " WHERE Guid" + sType + " ='" + (string) dob.LstValue[idx] + "'"))
            {
                CBReaderClose();
                return true;
            }
            CBReaderClose();
            return false;
        }

        public bool ExistGuid(Enreg o)
        {
            string sType = o.sTable;
            
            if (CBRecherche("SELECT Guid" + sType + " FROM " + sType + " WHERE Guid" + sType + " ='" + (string)o.LstValue[0] + "'"))
            {
                CBReaderClose();
                return true;
            }
            CBReaderClose();
            return false;
        }

        public void CreatObject(Enreg o)
        {
            string sType = o.sTable;
            string RequeteField = "INSERT INTO " + sType + " (";
            string RequeteValue = "VALUES (";

            Table t;
            int n = ConfDB.FindTable(sType);
            if (n > -1)
            {
                t = (Table)ConfDB.LstTable[n];
                bool InsField = false;
                for (int i = 0; i < t.LstField.Count; i++)
                {
                    if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.InterneBD) != 0)
                    {
                        switch (((Field)t.LstField[i]).Type)
                        {
                            case 's':
                                if ((string)o.LstValue[i] != "")
                                {
                                    if (InsField) { RequeteField += ", "; RequeteValue += ", "; }
                                    RequeteField += ((Field)t.LstField[i]).Name;
                                    RequeteValue += "'" + o.LstValue[i] + "'";
                                    InsField = true;
                                }
                                break;
                            case 'p': //picture
                            case 'q': //picture
                            case 'i':
                                if ((int)o.LstValue[i] != 0)
                                {
                                    if (InsField) { RequeteField += ", "; RequeteValue += ", "; }
                                    RequeteField += ((Field)t.LstField[i]).Name;
                                    RequeteValue += o.LstValue[i];
                                    InsField = true;
                                }
                                break;
                            case 'd':
                                if ((double)o.LstValue[i] != 0)
                                {
                                    if (InsField) { RequeteField += ", "; RequeteValue += ", "; }
                                    RequeteField += ((Field)t.LstField[i]).Name;
                                    RequeteValue += o.LstValue[i];
                                    InsField = true;
                                }
                                break;
                            case 't':
                                //if (o.LstValue[i] != null && o.LstValue[i].ToString() != "" && (DateTime)o.LstValue[i] != DateTime.MinValue)
                                if (o.LstValue[i] != null && o.LstValue[i].ToString() != "")
                                {
                                    DateTime dt = (DateTime)o.LstValue[i];
                                    if (InsField) { RequeteField += ", "; RequeteValue += ", "; }
                                    RequeteField += ((Field)t.LstField[i]).Name;
                                    RequeteValue += "'" + dt.ToString("yyyy-MM-dd") + "'";
                                    InsField = true;
                                }
                                break;

                        }
                    }
                }
                RequeteField += ") ";
                RequeteValue += ")";
            }

            CBWrite(RequeteField + RequeteValue);
        }

        public void UpdateObject(Enreg o)
        {
            string sType = o.sTable;
            string Update = "UPDATE " + sType;
            string Set = "SET ";
            string Where = "WHERE ";

            Table t;
            int n = ConfDB.FindTable(sType);
            if (n > -1)
            {
                t = (Table)ConfDB.LstTable[n];
                bool InsFieldW = false, InsFieldR = false;

                for (int i = 0; i < t.LstField.Count; i++)
                {
                    if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.InterneBD) != 0)
                    {

                        if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.Key) != 0)
                        {
                            switch (((Field)t.LstField[i]).Type)
                            {
                                case 's':
                                    if ((string)o.LstValue[i] != "")
                                    {
                                        if (InsFieldW) Set += " and ";
                                        Where += ((Field)t.LstField[i]).Name + "='" + o.LstValue[i] + "'";
                                        InsFieldW = true;
                                    }
                                    break;
                                case 'p': //picture
                                case 'q': //picture
                                case 'i':
                                    if ((int)o.LstValue[i] != 0)
                                    {
                                        if (InsFieldW) Set += " and ";
                                        Where += ((Field)t.LstField[i]).Name + "=" + o.LstValue[i];
                                        InsFieldW = true;
                                    }
                                    break;
                                case 'd':
                                    if ((double)o.LstValue[i] != 0)
                                    {
                                        if (InsFieldR) Set += ", ";
                                        Set += ((Field)t.LstField[i]).Name + "=" + o.LstValue[i];
                                        InsFieldR = true;
                                    }
                                    break;
                                case 't':
                                    //if (o.LstValue[i] != null && (string)o.LstValue[i] != "" && (DateTime)o.LstValue[i] != DateTime.MinValue)
                                    if (o.LstValue[i] != null && (string)o.LstValue[i] != "")
                                    {
                                        DateTime dt = (DateTime)o.LstValue[i];
                                        if (InsFieldR) Set += ", ";
                                        Set += ((Field)t.LstField[i]).Name + "='" + dt.ToString("yyyy-MM-dd") + "'"; //dt.ToShortDateString() + "'";
                                        InsFieldR = true;
                                    }
                                    break;

                            }
                        }
                        else
                        {
                            switch (((Field)t.LstField[i]).Type)
                            {
                                case 's':
                                    if ((string)o.LstValue[i] != "")
                                    {
                                        if (InsFieldR) Set += ", ";
                                        Set += ((Field)t.LstField[i]).Name + "='" + o.LstValue[i] + "'";
                                        InsFieldR = true;
                                    }
                                    break;
                                case 'p': //picture
                                case 'q': //picture
                                case 'i':
                                    if ((int)o.LstValue[i] != null)
                                    {
                                        if (InsFieldR) Set += ", ";
                                        Set += ((Field)t.LstField[i]).Name + "=" + o.LstValue[i];
                                        InsFieldR = true;
                                    }
                                    break;
                                case 'd':
                                    if ((double)o.LstValue[i] != null)
                                    {
                                        if (InsFieldR) Set += ", ";
                                        Set += ((Field)t.LstField[i]).Name + "=" + o.LstValue[i];
                                        InsFieldR = true;
                                    }
                                    break;
                                case 't':
                                    if (o.LstValue[i] != null && (DateTime)o.LstValue[i] != DateTime.MinValue)
                                    //if (o.LstValue[i] != null && (string)o.LstValue[i] != "" && (DateTime)o.LstValue[i] != DateTime.MinValue)
                                        {
                                        DateTime dt = (DateTime)o.LstValue[i];
                                        if (InsFieldR) Set += ", ";
                                        Set += ((Field)t.LstField[i]).Name + "='" + dt.ToString("yyyy-MM-dd") + "'"; //dt.ToShortDateString() + "'";
                                        //Set += ((Field)t.LstField[i]).Name + "='" + dt.ToShortDateString() + "'";
                                        InsFieldR = true;
                                    }
                                    break;

                            }
                        }
                    }
                }
            }
            // cette fonction UpdateObject existe aussi avec l'objet drawobject
            DataTable datatable = connection.GetSchema("columns", new[] { null, null, sType, "updatedate" });
            if (datatable.Rows.Count == 1)
                Set += ", updatedate='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
            CBWrite(Update + " " + Set + " " + Where);
        }

        public bool ExistGuid(DrawObject dob, string sType)
        {
            if (CBRecherche("SELECT Guid" + sType + " FROM " + sType + " WHERE Guid" + sType + " ='" + dob.GuidkeyObjet + "'"))
            {
                CBReaderClose();
                return true;
            }
            CBReaderClose();
            return false;
        }

        public bool ExistLabelLink(string GuidLabel, string GuidObjet)
        {
            if (CBRecherche("Select GuidLabel From LabelLink Where GuidLabel='" + GuidLabel + "' and GuidObjet='" + GuidObjet + "'"))
            {
                CBReaderClose();
                return true;
            }
            CBReaderClose();
            return false;
        }

        public bool ExistExtentionValue(string GuidObj, string GuidParam)
        {
            if (CBRecherche("SELECT GuidParam FROM ExtentionParamValue WHERE GuidObject = '" + GuidObj + "' and GuidParam = '" + GuidParam + "'"))
            {
                CBReaderClose();
                return true;
            }
            CBReaderClose();

            return false;
        }

        public bool ExistServerLink(string field, string GuidServerPhy, string GuidVue ,string Guidfield)
        {
            if (CBRecherche("SELECT " + field + " FROM " + field.Substring("Guid".Length) + "Link WHERE GuidServerPhy='" + GuidServerPhy + "' and GuidVue='" + GuidVue + "' and " + field + "='" + Guidfield + "'"))
            {
                CBReaderClose();
                return true;
            }
            CBReaderClose();
            return false;
        }

        public bool ExistSanCardLink(string field, string GuidSanCardSrc, string GuidVue, string GuidSanCardCbl)
        {
            if (CBRecherche("SELECT GuidSanCardSrc FROM " + field.Substring("Guid".Length) + "Link WHERE GuidSanCardSrc='" + GuidSanCardSrc + "' and GuidVue='" + GuidVue + "' and GuidSanCardCbl='" + GuidSanCardCbl + "'"))
            {
                CBReaderClose();
                return true;
            }
            CBReaderClose();
            return false;
        }

        public bool ExistISLLink(string field, string Guidfield, string GuidServerPhy)
        {
            if (CBRecherche("SELECT " + field + " FROM " + field.Substring("Guid".Length) + "Link WHERE " + field + "='" + Guidfield + "' and GuidServerPhy='" + GuidServerPhy + "'"))
            {
                CBReaderClose();
                return true;
            }
            CBReaderClose();
            return false;
        }


        public bool ExistGuidG(DrawObject dob)
        {
            string sType = dob.GetTypeSimpleGTable();
            if (CBRecherche("SELECT Guid" + sType + " FROM " + sType + " WHERE Guid" + sType + " ='" + dob.Guidkey + "'"))
            {
                CBReaderClose();
                return true;
            }
            CBReaderClose();
            return false;
        }

        public bool ExistGuidG(DrawObject dob, string sType)
        {
            if (CBRecherche("SELECT GuidG" + sType + " FROM G" + sType + " WHERE GuidG" + sType + " ='" + dob.Guidkey + "'"))
            {
                CBReaderClose();
                return true;
            }
            CBReaderClose();
            return false;
        }

        public string FindGuidFromNom(string sTable, string sNom)
        {
            string sGuid = null;
            if (CBRecherche("SELECT Guid" + sTable + " FROM " + sTable + " where Nom" + sTable + " ='" + sNom + "'"))
            {
                sGuid = Reader.GetString(0);
                CBReaderClose();
            }
            else CBReaderClose();
            return sGuid;
        }
        /*
        public void CreatAppli()
        {
            //if (!CBRecherche("SELECT GuidApplication FROM Application where NomApplication ='" + parent.cbApplication.Text + "'"))
            if (!CBRecherche("SELECT GuidApplication FROM Application where GuidApplication ='" + parent.GetGuidApplication() + "'"))
            {
                CBReaderClose();
                parent.GuidApplication = Guid.NewGuid();
                CBWrite("INSERT INTO Application (GuidApplication, NomApplication, GuidCode) VALUES ('" + parent.GuidApplication + "','" + parent.cbApplication.Text + "','764079ad-621b-4c57-92be-9d1530fb20cb')");
            }
            else
            {
                parent.GuidApplication = new Guid(Reader.GetString(0));
                CBReaderClose();
            }
        }*/

        public void LoadLink()
        {
            string sTypeVue = parent.tbTypeVue.Text; // (string)parent.cbTypeVue.SelectedItem;
            string sQuery="";
            sQuery = "SELECT GuidGTechLink, TechLink.GuidTechLink, NomTechLink, GuidServerIn, GuidServerOut, X, Y FROM DansVue, GTechLink, TechLink, GPoint WHERE GuidGVue ='" + parent.GuidGVue + "' AND DansVue.GuidObjet=GTechLink.GuidGTechLink AND GTechLink.GuidTechLink=TechLink.GuidTechLink AND GTechLink.GuidGTechLink=GPoint.GuidGObjet ORDER BY GuidGTechLink, I";
            
            if (CBRecherche(sQuery))
            {
                string sGuidGLink = "";
                string sGuidLink = "";
                string sNomLink = "";
                string sGuidModuleIn = "";
                string sGuidModuleOut = "";
                DrawTechLink dl;
                ArrayList pointArray = new ArrayList();
                
                while (Reader.Read())
                {
                    if (Reader.GetString(0) == sGuidGLink)
                    {
                        pointArray.Add(new Point(Reader.GetInt16(5), Reader.GetInt16(6)));
                    }
                    else
                    {
                        if (pointArray.Count != 0)
                        {
                            DrawObject obj;

                            // add GLink Objet
                            dl = new DrawTechLink(parent, sGuidLink, sNomLink, pointArray);

                            obj = parent.RechercheObjet(sGuidModuleIn);
                            dl.LstLinkIn.Add(obj); obj.LstLinkOut.Add(dl);
                            obj = parent.RechercheObjet(sGuidModuleOut);
                            dl.LstLinkOut.Add(obj); obj.LstLinkIn.Add(dl);
                            parent.drawArea.GraphicsList.Add(dl);

                            
                            TreeNode[] ArrayTreeNode = parent.tvObjet.Nodes.Find(sGuidLink,true);
                            if (ArrayTreeNode.Length == 1) ArrayTreeNode[0].Remove();
                        }
                        pointArray.Clear();
                        sGuidGLink = Reader.GetString(0);
                        sGuidLink = Reader.GetString(1);
                        sNomLink = Reader.GetString(2);
                        sGuidModuleIn = Reader.GetString(3);
                        sGuidModuleOut = Reader.GetString(4);
                        pointArray.Add(new Point(Reader.GetInt16(5), Reader.GetInt16(6)));
                    }
                }
                if (pointArray.Count != 0)
                {
                    // add GLink Objet
                    DrawObject obj;

                    dl = new DrawTechLink(parent, sGuidLink, sNomLink, pointArray);

                    obj = parent.RechercheObjet(sGuidModuleIn);
                    dl.LstLinkIn.Add(obj); obj.LstLinkOut.Add(dl);
                    obj = parent.RechercheObjet(sGuidModuleOut);
                    dl.LstLinkOut.Add(obj); obj.LstLinkIn.Add(dl);
                    parent.drawArea.GraphicsList.Add(dl);
                    

                    TreeNode[] ArrayTreeNode = parent.tvObjet.Nodes.Find(sGuidLink, true);
                    if (ArrayTreeNode.Length == 1) ArrayTreeNode[0].Remove();
                }
                CBReaderClose();
            }
            else CBReaderClose();
        }


        public void LinkNCardWithVLan()
        {
            for(int i=0; i < parent.drawArea.GraphicsList.Count; i++)
            {
                DrawObject o = (DrawObject)parent.drawArea.GraphicsList[i];
                if (o.GetType() == typeof(DrawNCard))
                {

                    DrawNCard dnc = (DrawNCard)o;
                    DrawObject oParent = (DrawObject)dnc.LstParent[0];
                    int j = parent.drawArea.GraphicsList.FindObjet(0, (string)dnc.GetValueFromName("GuidVlan"));
                    if (j != -1)
                    {
                        DrawVLan dvl = (DrawVLan)parent.drawArea.GraphicsList[j];

                        dnc.Hauteur = LinkPointVlanWithCard(dvl, dnc, oParent.GetTopYNCard(), oParent.GetBottomYNCard());
                        /*
                        ArrayList lstIndexPointVlanX = new ArrayList();                       

                        for (j = 0; j < dvl.pointArray.Count; j++)
                        {
                            int x = ((Point)dvl.pointArray[j]).X;
                            if (x >= dnc.XMin() && x <= dnc.XMax()) lstIndexPointVlanX.Add(j);
                        }

                        int iTopY = oParent.GetTopYNCard(), iBottomY = oParent.GetBottomYNCard();
                        //int iMiddleTopY = dnc.YMin(), iMiddleBottomY = dnc.YMax();
                        int index = -1, iCard = iTopY, deltaMax = 10, delta = deltaMax;
                        for(j=0; j<lstIndexPointVlanX.Count; j++)
                        {
                            

                            delta = Math.Abs((((Point)dvl.pointArray[(int)lstIndexPointVlanX[j]]).Y - iTopY));
                            if (delta < deltaMax)
                            {
                                if (index == -1) index = (int)lstIndexPointVlanX[j];

                                if (Math.Abs((((Point)dvl.pointArray[index]).Y - iCard)) > delta)
                                {
                                    index = (int)lstIndexPointVlanX[j]; iCard = iTopY;
                                }
                            }
                            delta = Math.Abs((((Point)dvl.pointArray[(int)lstIndexPointVlanX[j]]).Y - iBottomY));
                            if (delta < deltaMax)
                            {
                                if (index == -1) index = (int)lstIndexPointVlanX[j];

                                if (Math.Abs((((Point)dvl.pointArray[index]).Y - iCard)) > delta)
                                {
                                    index = (int)lstIndexPointVlanX[j]; iCard = iBottomY;
                                }
                            }
                        }
                        if (index != -1)
                        {
                            int indexFromPointArray = dvl.LstLinkOut.Count;
                            dnc.AttachLink(dvl, DrawObject.TypeAttach.Entree);
                            dvl.AttachLink(dnc, DrawObject.TypeAttach.Sortie);

                            Point pt = (Point)dvl.pointArray[index];
                            

                            if (iCard == iTopY)
                            {
                                dnc.Hauteur = 0;
                                pt.Y = oParent.GetTopYNCard();
                            }
                            else
                            {
                                dnc.Hauteur = 1;
                                pt.Y = oParent.GetBottomYNCard();
                            }
                            //dnc.HandleVLan = index;
                            if(index == indexFromPointArray) dvl.pointArray[index] = pt;
                            else
                            {
                                Point ptTemp = (Point) dvl.pointArray[indexFromPointArray];
                                dvl.pointArray[indexFromPointArray] = pt;
                                dvl.pointArray[index] = ptTemp;
                            }
                            //dvl.pointArray.RemoveAt(index);
                            //dvl.pointArray.Insert(index,pt);
                            //dnc.HandleVLan = index;
                            //dvl.AttachHandle.Add(dnc.HandleVLan + 10); // + 9 car il y a 9 points prient par le rectangle du VLAn
                        }*/
                    }
                }
            }
        }

        public void LoadVLanPoint()
        {
            string sQuery = "SELECT GuidVLan, GPoint.X, GPoint.Y FROM DansVue, GVLan, GPoint WHERE GuidGVue ='" + parent.GuidGVue + "' AND GuidObjet=GuidGVLan AND GuidGVLan=GuidGObjet ORDER BY GuidGVLan, I";
            
            if (CBRecherche(sQuery))
            {
                DrawVLan dv=null;
                ArrayList pointArray = new ArrayList();
                //string sGuidGVLan = "";

                while (Reader.Read())
                {
                    if (dv != null &&  dv.GuidkeyObjet.ToString() == Reader.GetString(0))
                    {
                        pointArray.Add(new Point(Reader.GetInt16(1), Reader.GetInt16(2)));
                    }
                    else
                    {
                        if (pointArray.Count != 0)
                        {
                            dv.pointArray = pointArray;
                        }
                        pointArray = new ArrayList();
                        dv = null;
                        int i = parent.drawArea.GraphicsList.FindObjet(0, Reader.GetString(0));
                        if (i != -1)
                        {
                            
                            dv = (DrawVLan)parent.drawArea.GraphicsList[i];
                            pointArray.Add(new Point(Reader.GetInt16(1), Reader.GetInt16(2)));
                        }
                    }
                }
                if (pointArray.Count != 0) dv.pointArray = pointArray;
                CBReaderClose();
            }
            else CBReaderClose();
        }

        public void LoadSanSwitchPoint()
        {
            string sQuery = "SELECT GuidSanSwitch, GPoint.X, GPoint.Y FROM DansVue, GSanSwitch, GPoint WHERE GuidGVue ='" + parent.GuidGVue + "' AND GuidObjet=GuidGSanSwitch AND GuidGSanSwitch=GuidGObjet ORDER BY GuidGSanSwitch, I";

            if (CBRecherche(sQuery))
            {
                DrawSanSwitch dss = null;
                ArrayList pointArray = new ArrayList();
                //string sGuidGVLan = "";

                while (Reader.Read())
                {
                    if (dss != null && dss.GuidkeyObjet.ToString() == Reader.GetString(0))
                    {
                        pointArray.Add(new Point(Reader.GetInt16(1), Reader.GetInt16(2)));
                    }
                    else
                    {
                        if (pointArray.Count != 0)
                        {
                            dss.pointArray = pointArray;
                        }
                        pointArray = new ArrayList();
                        dss = null;
                        int i = parent.drawArea.GraphicsList.FindObjet(0, Reader.GetString(0));
                        if (i != -1)
                        {
                            dss = (DrawSanSwitch)parent.drawArea.GraphicsList[i];
                            pointArray.Add(new Point(Reader.GetInt16(1), Reader.GetInt16(2)));
                        }
                    }
                }
                if (pointArray.Count != 0) dss.pointArray = pointArray;
                CBReaderClose();
            }
            else CBReaderClose();
        }

        public void LoadCnxPoint()
        {
            string sQuery = "SELECT GuidCnx, GPoint.X, GPoint.Y FROM DansVue, GCnx, GPoint WHERE GuidGVue ='" + parent.GuidGVue + "' AND GuidObjet=GuidGCnx AND GuidGCnx=GuidGObjet ORDER BY GuidGCnx, I";

            if (CBRecherche(sQuery))
            {
                DrawCnx dc = null;
                ArrayList pointArray = new ArrayList();
                //string sGuidGVLan = "";

                while (Reader.Read())
                {
                    if (dc != null && dc.GuidkeyObjet.ToString() == Reader.GetString(0))
                    {
                        pointArray.Add(new Point(Reader.GetInt16(1), Reader.GetInt16(2)));
                    }
                    else
                    {
                        if (pointArray.Count != 0)
                        {
                            dc.pointArray = pointArray;
                        }
                        pointArray = new ArrayList();
                        dc = null;
                        int i = parent.drawArea.GraphicsList.FindObjet(0, Reader.GetString(0));
                        if (i != -1)
                        {
                            dc = (DrawCnx)parent.drawArea.GraphicsList[i];
                            pointArray.Add(new Point(Reader.GetInt16(1), Reader.GetInt16(2)));
                        }
                    }
                }
                if (pointArray.Count != 0) dc.pointArray = pointArray;
                CBReaderClose();
            }
            else CBReaderClose();
        }

        public void LoadZonePoint()
        {
            string sQuery = "SELECT GuidZone, GPoint.X, GPoint.Y FROM DansVue, GZone, GPoint WHERE GuidGVue ='" + parent.GuidGVue + "' AND GuidObjet=GuidGZone AND GuidGZone=GuidGObjet ORDER BY GuidZone, I";

            if (CBRecherche(sQuery))
            {
                DrawZone dz = null;
                ArrayList pointArray = new ArrayList();
                //string sGuidGVLan = "";

                while (Reader.Read())
                {
                    if (dz != null && dz.GuidkeyObjet.ToString() == Reader.GetString(0))
                    {
                        pointArray.Add(new Point(Reader.GetInt16(1), Reader.GetInt16(2)));
                    }
                    else
                    {
                        if (pointArray.Count != 0)
                        {
                            dz.pointArray = pointArray;
                        }
                        pointArray = new ArrayList();
                        dz = null;
                        int i = parent.drawArea.GraphicsList.FindObjet(0, Reader.GetString(0));
                        if (i != -1)
                        {
                            dz = (DrawZone)parent.drawArea.GraphicsList[i];
                            pointArray.Add(new Point(Reader.GetInt16(1), Reader.GetInt16(2)));
                        }
                    }
                }
                if (pointArray.Count != 0) dz.pointArray = pointArray;
                CBReaderClose();
            }
            else CBReaderClose();
        }


        public void DeleteGObject(Tool tool, string sGuidGVue)
        {
            string Select, From, Where;
            string sType = tool.GetType().Name.Substring("Tool".Length);

            ArrayList LstObjet = new ArrayList();

            Select = "SELECT GuidG" + sType;
            From = tool.GetFromG(sType);
            Where = tool.GetWhereG(sType, sGuidGVue);

            if (CBRecherche(Select + " " + From + " " + Where))
            {
                while (Reader.Read()) LstObjet.Add(Reader.GetString(0));
                CBReaderClose();
                for (int i = 0; i < LstObjet.Count; i++)
                    CBWrite("DELETE FROM G" + sType + "  WHERE GuidG" + sType + "='" + (string)LstObjet[i] + "'");
            }
            else CBReaderClose();
        }

        public void DeleteGPoint (string GuidObjet)
        {
            if (CBRecherche("SELECT GuidGObjet From GPoint Where GuidGObjet='" + GuidObjet + "'"))
            {
                CBReaderClose();
                CBWrite("DELETE FROM GPoint WHERE GuidGObjet='" + GuidObjet + "'");
            }
            else CBReaderClose();
        }

        public void DeleteGObjetEtPoint(Tool tool, string sGuidGVue)
        {
            string Select, From, Where;
            string sType = tool.GetType().Name.Substring("Tool".Length);

            ArrayList LstObjet = new ArrayList();

            Select = "SELECT GuidG" + sType;
            From = tool.GetFromG(sType);
            Where = tool.GetWhereG(sType, sGuidGVue);

            if (CBRecherche(Select + " " + From + " " + Where))
            {
                while (Reader.Read()) LstObjet.Add(Reader.GetString(0));
                CBReaderClose();
                for (int i = 0; i < LstObjet.Count; i++)
                {
                    DeleteGPoint((string)LstObjet[i]);
                    CBWrite("DELETE FROM G" + sType + "  WHERE GuidG" + sType + "='" + (string)LstObjet[i] + "'");
                }
            }
            else CBReaderClose();
        }                    
    }

    

    public class WorkApplication
    {
        private Guid guidApplication;
        private string sApplication;
        private Guid guidVer;
        private string sLayers;
        private Form1 F;
        private string sTadFile;
        private Boolean bChgLayers;
        private string sVersion;

        public Guid Guid
        {
            get { return guidApplication; }
        }

        public Guid GuidAppVersion
        {
            get { return guidVer; }
        }

        public string Version
        {
            get { return sVersion; }
        }

        public string Layers
        {
            get { return sLayers; }
        }

        public string Application
        {
            get { return sApplication; }
        }

        public string TadFile
        {
            get { return sTadFile; }
        }

        public Boolean ChgLayers
        {
            get { return bChgLayers; }
        }

        public WorkApplication(Form1 f, string sGuid, string sName, string sGuidVer, string sV = "")
        {
            F = f;
            guidApplication = new Guid(sGuid);
            guidVer = new Guid(sGuidVer);
            sApplication = sName;
            sVersion = sV;
            F.setCtrlEnabled(F.cbOpApp, true);
            F.setCtrlEnabled(F.cbOpVue, true);
            sLayers = InitLayers();
            if (sApplication != null) sTadFile = F.GetFullPath(this) + @"\DAT\DAT_" + Application + ".docx";
            else sTadFile = null;
            bChgLayers = false;
        }

        public string InitLayers()
        {
            string sLayersTemp = " (null)";

            if (F.oCnxBase.CBRecherche("SELECT GuidLayer, NomLayer FROM Layer Where GuidAppVersion ='" + guidApplication + "'"))
            {
                while (F.oCnxBase.Reader.Read())
                    sLayersTemp += ";" + F.oCnxBase.Reader.GetString(1) + "     (" + F.oCnxBase.Reader.GetString(0) + ")";
            }
            F.oCnxBase.CBReaderClose();
            return sLayersTemp.Substring(1);
        }

        public void SetLayers()
        {
            FormChangeProp fcp = new FormChangeProp(F, null);

            if (GuidAppVersion != null && sLayers != "")
            {
                DrawObject o = F.drawArea.GraphicsList.GetSelectedObject(0);

                fcp.AddlSourceFromDB("SELECT GuidLayer, NomLayer FROM Layer Where GuidAppVersion='" + GuidAppVersion + "'", "Create");
                fcp.AffCheckBoxDefaultLayer();
                fcp.AddlDestinationFromValue(sLayers);
                fcp.ShowDialog(F);
                if (fcp.Valider)
                {
                    sLayers = F.oCnxBase.CmdText;
                    if (!bChgLayers)
                    {
                        MessageBox.Show("Le changement de la configuration des couches désactive la sauvegarde");
                        bChgLayers = true;
                    }
                }
            }
        }

        public string GetWhereLayer()
        {
            string sWhereLayer = " and ( ";
            int deb = 1;
            bool bDebOr = false;

            if (sLayers != "")
            {
                string[] aValue = sLayers.Split('(', ')');
                if (aValue.Length > 1 && aValue[1] == "null")
                {
                    sWhereLayer = " and (GuidLayer is null ";
                    deb = 3;
                    bDebOr = true;
                }
                for (int i = deb; i < aValue.Length; i += 2)
                {
                    sWhereLayer += (bDebOr ? "or " : " ") + "GuidLayer='" + aValue[i] + "' ";
                    bDebOr = true;
                }
            }
            sWhereLayer += ")";
            return sWhereLayer;
        }
    }
}
