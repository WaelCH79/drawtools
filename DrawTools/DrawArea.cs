using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;

namespace DrawTools
{
	/// <summary>
	/// Working area.
	/// Handles mouse input and draws graphics objects.
	/// </summary>
	public class DrawArea : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        #region Constructor, Dispose

		public DrawArea()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

        #endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.SuspendLayout();
            // 
            // DrawArea
            // 
            this.AutoScroll = true;
            this.AutoSize = true;
            this.Name = "DrawArea";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawArea_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawArea_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawArea_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawArea_MouseUp);
            this.ResumeLayout(false);

        }
		#endregion

        #region Enumerations

        public enum DrawToolType
        {
            Pointer,
            Rectangle,
            Ellipse,
            Line,
            Polygon,
            Cadre,
            Module,
            Link,
            Server,
            Server3D,
            Base,
            File,
            Queue,
            Genks,
            Genpod,
            Container,
            Gening,
            Gensvc,
            Gensas,
            Managedsvc,
            Insks,
            Insns,
            Inspod,
            Inssvc,
            Insing,
            Inssas,
            LinkA,
            Composant,
            MainComposant,
            CompFonc,
            ServMComp,
            Techno,
            TechLink,
            ServerPhy,
            Insnd,
            NCard,
            VLan,
            Router,
            AppUser,
            Application,
            Interface,
            Cluster,
            Machine,
            Virtuel,
            Baie,
            Lun,
            Zone,
            BaiePhy,
            MachineCTI,
            BaieCTI,
            Drawer,
            SanCard,
            SanSwitch,
            ISL,
            BaieDPhy,
            Location,
            ServerSite,
            Cnx,
            PtCnx,
            InfServer,
            InfInssas,
            InfNCard,
            TechUser,
            CadreRefN,
            CadreRefN1,
            Indicator,
            CadreRefEnd,
            Produit,
            //MCompApp,
            TechnoRef,
            Fonction,
            FonctionService,
            Statut,
            Template,
            TechnoArea,
            ApplicationClass,
            ApplicationType,
            InterLink,
            ServerType,
            Vue,
            Axes,
            PtNiveau,
            InfLink,
            Arborescence,
            DiskClass,
            BackupClass,
            ExploitClass,
            CadreRef,
            CadreRefApp,
            CadreRefFonc,
            //IndicatorInfo,
            GroupService,
            OptionsDraw,
            ProduitApp,
            Service,
            StaticTable,
            TypeVue,
            Package,
            PackageDynamic,
            Environnement,
            ServiceLink,
            VlanClass,
            Layer,
            LayerLink,
            AppVersion,
            FluxBoutEnBout,
            FluxBoutEnBoutFonc,
            Flux,
            //Copy,
            NumberOfDrawTools,
        };

        #endregion

        #region Members

        private GraphicsList graphicsList;    // list of draw objects
                          // (instances of DrawObject-derived classes)

        private DrawToolType activeTool;      // active drawing tool
        private bool addobjet = false;
        public Tool[] tools;                 // array of tools

        // group selection rectangle
        private Rectangle netRectangle;
        private bool drawNetRectangle = false;

        // Information about owner form
        private Form1 owner;
        private DrawObject oSelected;

        #endregion

        #region Properties

        /// <summary>
        /// Reference to the owner form
        /// </summary>
        public Form1 Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
            }
        }

        public bool AddObjet
        {
            get
            {
                return addobjet;
            }
            set
            {
                addobjet = value;
            }
        }

        public DrawObject OSelected
        {
            get
            {
                return oSelected;
            }
            set
            {
                oSelected = value;
            }
        }

        
        /// <summary>
        /// Group selection rectangle. Used for drawing.
        /// </summary>
        public Rectangle NetRectangle
        {
            get
            {
                return netRectangle;
            }
            set
            {
                netRectangle = value;
            }
        }

        /// <summary>
        /// Flas is set to true if group selection rectangle should be drawn.
        /// </summary>
        public bool DrawNetRectangle
        {
            get
            {
                return drawNetRectangle;
            }
            set
            {
                drawNetRectangle = value;
            }
        }

        /// <summary>
        /// Active drawing tool.
        /// </summary>
        public DrawToolType ActiveTool
        {
            get
            {
                return activeTool;
            }
            set
            {
                activeTool = value;
            }
        }

        /// <summary>
        /// List of graphics objects.
        /// </summary>
        public GraphicsList GraphicsList
        {
            get
            {
                return graphicsList;
            }
            set
            {
                graphicsList = value;
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Draw graphic objects and 
        /// group selection rectangle (optionally)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawArea_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            SolidBrush brush = new SolidBrush(Color.FromArgb(255, 255, 255));
            e.Graphics.FillRectangle(brush, 
                this.ClientRectangle);

            if ( graphicsList != null)
                //if (graphicsList != null)
            {
                graphicsList.Draw(e.Graphics);
            }

            DrawNetSelection(e.Graphics);

            brush.Dispose();
        }

        /// <summary>
        /// Mouse down.
        /// Left button down event is passed to active tool.
        /// Right button down event is handled in this class.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawArea_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            tools[(int)DrawToolType.Pointer].InitToolTip();
            if (e.Button == MouseButtons.Left)
                tools[(int)activeTool].OnMouseDown(this, e);
            else if (e.Button == MouseButtons.Right)
                OnContextMenu(e);
        }


        /// <summary>
        /// Mouse move.
        /// Moving without button pressed or with left button pressed
        /// is passed to active tool.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawArea_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if ( e.Button == MouseButtons.Left  ||  e.Button == MouseButtons.None )
                tools[(int)activeTool].OnMouseMove(this, e);
            else
                this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Mouse up event.
        /// Left button up event is passed to active tool.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawArea_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if ( e.Button == MouseButtons.Left )
                tools[(int)activeTool].OnMouseUp(this, e);
        }

        #endregion

        #region Other Functions

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="docManager"></param>
        public void Initialize(Form1 owner)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | 
                ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            // Keep reference to owner form
            this.Owner = owner;

            // set default tool
            activeTool = DrawToolType.Pointer;

            // create list of graphic objects
            graphicsList = new GraphicsList(Owner);

            // create array of drawing tools
            tools = new Tool[(int)DrawToolType.NumberOfDrawTools];
            tools[(int)DrawToolType.Pointer] = new ToolPointer(this);
            tools[(int)DrawToolType.Rectangle] = new ToolRectangle(this);
            tools[(int)DrawToolType.Ellipse] = new ToolEllipse(this);
            tools[(int)DrawToolType.Line] = new ToolLine(this);
            tools[(int)DrawToolType.Polygon] = new ToolPolygon(this);
            tools[(int)DrawToolType.Cadre] = new ToolCadre(this);
            tools[(int)DrawToolType.Module] = new ToolModule(this);
            tools[(int)DrawToolType.Link] = new ToolLink(this);
            tools[(int)DrawToolType.Server] = new ToolServer(this);
            tools[(int)DrawToolType.Genks] = new ToolGenks(this);
            tools[(int)DrawToolType.Genpod] = new ToolGenpod(this);
            tools[(int)DrawToolType.Container] = new ToolContainer(this);
            tools[(int)DrawToolType.Gening] = new ToolGening(this);
            tools[(int)DrawToolType.Gensvc] = new ToolGensvc(this);
            tools[(int)DrawToolType.Gensas] = new ToolGensas(this);
            tools[(int)DrawToolType.Managedsvc] = new ToolManagedsvc(this);
            tools[(int)DrawToolType.Insks] = new ToolInsks(this);
            tools[(int)DrawToolType.Insnd] = new ToolInsnd(this);
            tools[(int)DrawToolType.Insns] = new ToolInsns(this);
            tools[(int)DrawToolType.Inspod] = new ToolInspod(this);
            tools[(int)DrawToolType.Inssvc] = new ToolInssvc(this);
            tools[(int)DrawToolType.Insing] = new ToolInsing(this);
            tools[(int)DrawToolType.Inssas] = new ToolInssas(this);
            tools[(int)DrawToolType.Server3D] = new ToolServer3D(this);
            tools[(int)DrawToolType.Base] = new ToolBase(this);
            tools[(int)DrawToolType.File] = new ToolFile(this);
            tools[(int)DrawToolType.Queue] = new ToolQueue(this);
            tools[(int)DrawToolType.LinkA] = new ToolLink(this);
            tools[(int)DrawToolType.Composant] = new ToolComposant(this);
            tools[(int)DrawToolType.MainComposant] = new ToolMainComposant(this);
            tools[(int)DrawToolType.CompFonc] = new ToolCompFonc(this);
            tools[(int)DrawToolType.ServMComp] = new ToolMCompServ(this);
            tools[(int)DrawToolType.Techno] = new ToolTechno(this);
            tools[(int)DrawToolType.TechLink] = new ToolTechLink(this);
            tools[(int)DrawToolType.ServerPhy] = new ToolServerPhy(this);
            tools[(int)DrawToolType.NCard] = new ToolNCard(this);
            tools[(int)DrawToolType.VLan] = new ToolVLan(this);
            tools[(int)DrawToolType.Router] = new ToolRouter(this);
            tools[(int)DrawToolType.AppUser] = new ToolAppUser(this);
            tools[(int)DrawToolType.Application] = new ToolApplication(this, "59be2b47-4e8b-450a-9d38-90d23318c899");
            tools[(int)DrawToolType.Interface] = new ToolInterface(this);
            tools[(int)DrawToolType.Cluster] = new ToolCluster(this);
            tools[(int)DrawToolType.Machine] = new ToolMachine(this);
            tools[(int)DrawToolType.Virtuel] = new ToolVirtuel(this);
            tools[(int)DrawToolType.Baie] = new ToolBaie(this);
            tools[(int)DrawToolType.Lun] = new ToolLun(this);
            tools[(int)DrawToolType.Zone] = new ToolZone(this);
            tools[(int)DrawToolType.BaiePhy] = new ToolBaiePhy(this);
            tools[(int)DrawToolType.MachineCTI] = new ToolMachineCTI(this);
            tools[(int)DrawToolType.BaieCTI] = new ToolBaieCTI(this);
            tools[(int)DrawToolType.Drawer] = new ToolDrawer(this);
            tools[(int)DrawToolType.SanCard] = new ToolSanCard(this);
            tools[(int)DrawToolType.SanSwitch] = new ToolSanSwitch(this);
            tools[(int)DrawToolType.ISL] = new ToolISL(this);
            tools[(int)DrawToolType.BaieDPhy] = new ToolBaieDPhy(this);
            tools[(int)DrawToolType.Location] = new ToolLocation(this);
            tools[(int)DrawToolType.ServerSite] = new ToolServerSite(this);
            tools[(int)DrawToolType.Cnx] = new ToolCnx(this);
            tools[(int)DrawToolType.PtCnx] = new ToolPtCnx(this);
            tools[(int)DrawToolType.InfServer] = new ToolInfServer(this);
            tools[(int)DrawToolType.InfInssas] = new ToolInfInssas(this);
            tools[(int)DrawToolType.InfNCard] = new ToolInfNCard(this);
            tools[(int)DrawToolType.TechUser] = new ToolTechUser(this);
            tools[(int)DrawToolType.CadreRefN] = new ToolCadreRefN(this);
            tools[(int)DrawToolType.CadreRefN1] = new ToolCadreRefN1(this);
            tools[(int)DrawToolType.Indicator] = new ToolIndicator(this);
            tools[(int)DrawToolType.CadreRefEnd] = new ToolCadreRefEnd(this);
            tools[(int)DrawToolType.Produit] = new ToolProduit(this);
            tools[(int)DrawToolType.TechnoRef] = new ToolTechnoRef(this);
            tools[(int)DrawToolType.Fonction] = new ToolFonction(this);
            tools[(int)DrawToolType.FonctionService] = new ToolFonctionService(this);
            tools[(int)DrawToolType.Statut] = new ToolStatut(this);
            tools[(int)DrawToolType.Template] = new ToolTemplate(this);
            tools[(int)DrawToolType.Layer] = new ToolLayer(this);
            tools[(int)DrawToolType.LayerLink] = new ToolLayerLink(this);
            tools[(int)DrawToolType.TechnoArea] = new ToolTechnoArea(this);
            tools[(int)DrawToolType.InterLink] = new ToolInterLink(this);
            tools[(int)DrawToolType.ServerType] = new ToolServerType(this);
            tools[(int)DrawToolType.Vue] = new ToolVue(this);
            tools[(int)DrawToolType.Axes] = new ToolAxes(this);
            tools[(int)DrawToolType.PtNiveau] = new ToolPtNiveau(this);
            tools[(int)DrawToolType.InfLink] = new ToolInfLink(this);
            tools[(int)DrawToolType.Arborescence] = new ToolArborescence(this);
            tools[(int)DrawToolType.DiskClass] = new ToolDiskClass(this);
            tools[(int)DrawToolType.BackupClass] = new ToolBackupClass(this);
            tools[(int)DrawToolType.ExploitClass] = new ToolExploitClass(this);
            tools[(int)DrawToolType.CadreRef] = new ToolCadreRef(this);
            tools[(int)DrawToolType.CadreRefApp] = new ToolCadreRefApp(this);
            tools[(int)DrawToolType.CadreRefFonc] = new ToolCadreRefFonc(this);
            //tools[(int)DrawToolType.IndicatorInfo] = new ToolIndicatorInfo(this);
            tools[(int)DrawToolType.GroupService] = new ToolGroupService(this);
            tools[(int)DrawToolType.OptionsDraw] = new ToolOptionsDraw(this);
            tools[(int)DrawToolType.ProduitApp] = new ToolProduitApp(this);
            tools[(int)DrawToolType.Service] = new ToolService(this);
            tools[(int)DrawToolType.StaticTable] = new ToolStaticTable(this);
            tools[(int)DrawToolType.TypeVue] = new ToolTypeVue(this);
            tools[(int)DrawToolType.Package] = new ToolPackage(this);
            tools[(int)DrawToolType.PackageDynamic] = new ToolPackageDynamic(this);
            tools[(int)DrawToolType.Environnement] = new ToolEnvironnement(this);
            tools[(int)DrawToolType.ServiceLink] = new ToolServiceLink(this);
            tools[(int)DrawToolType.VlanClass] = new ToolVlanClass(this);
            tools[(int)DrawToolType.ApplicationClass] = new ToolApplicationClass(this);
            tools[(int)DrawToolType.ApplicationType] = new ToolApplicationType(this);
            tools[(int)DrawToolType.AppVersion] = new ToolAppVersion(this);

        }

        public void Switch(bool bSwitch)
        {
            GraphicsList.Switch(bSwitch);
        }

        public int getiTools(string sObjetSelected)
        {
            for (int i = 0; i < tools.Length; i++)
            {
                if(tools[i].GetType().Name.Substring("Tool".Length) == sObjetSelected) return i;
            }
            return -1;
        }

        public void MajObjets()
        {
            Owner.Text = "DrawTools - " + Owner.SelectedBase;
            Refresh();
        }

        
        /// <summary>
        ///  Draw group selection rectangle
        /// </summary>
        /// <param name="g"></param>
        public void DrawNetSelection(Graphics g)
        {
            if ( ! DrawNetRectangle )
                return;

            ControlPaint.DrawFocusRectangle(g, NetRectangle, Color.Black, Color.Transparent);
            /*if (bsave)
            {
                bsave = false;
                Bitmap bmp = new Bitmap(Width, Height, g);
                bmp.Save("c:\\test.bmp");

            }*/
        }

                /// <summary>
        /// Right-click handler
        /// </summary>
        /// <param name="e"></param>
        private void OnContextMenu(MouseEventArgs e)
        {
            // Change current selection if necessary

            Point point = new Point(e.X, e.Y);

            int n = GraphicsList.Count;
            DrawObject o = null;

            for ( int i = 0; i < n; i++ )
            {
                if ( GraphicsList[i].HitTest(point) == 0 )
                {
                    o = GraphicsList[i];
                    break;
                }
            }

            if ( o != null )
            {
                if ( ! o.Selected )
                    GraphicsList.UnselectAll();

                // Select clicked object
                o.Selected = true;

            }
            else
            {
                GraphicsList.UnselectAll();
            }

            Refresh();

            // Show context menu.
            // Make ugly trick which saves a lot of code.
            // Get menu items from Edit menu in main form and
            // make context menu from them.
            // These menu items are handled in the parent form without
            // any additional efforts.

            MainMenu mainMenu = Owner.Menu;    // Main menu
            MenuItem editItem = mainMenu.MenuItems[1];            // Edit submenu

            // Make array of items for ContextMenu constructor
            // taking them from the Edit submenu
            MenuItem[] items = new MenuItem[editItem.MenuItems.Count];

            for ( int i = 0; i < editItem.MenuItems.Count; i++ )
            {
                items[i] = editItem.MenuItems[i];
            }

            Owner.SetStateOfControls();  // enable/disable menu items

            // Create and show context menu
            ContextMenu menu = new ContextMenu(items);
            menu.Show(this, point);

            // Restore items in the Edit menu (without this line Edit menu
            // is empty after forst right-click)
            editItem.MergeMenu(menu);
        }



        #endregion
	}
}
