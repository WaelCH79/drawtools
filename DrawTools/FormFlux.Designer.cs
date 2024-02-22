namespace DrawTools
{
    partial class FormFlux
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgFlux = new System.Windows.Forms.DataGridView();
            this.cbApp = new System.Windows.Forms.ComboBox();
            this.cbTypeVue = new System.Windows.Forms.ComboBox();
            this.cbServerSrc = new System.Windows.Forms.ComboBox();
            this.bGo = new System.Windows.Forms.Button();
            this.bAdd = new System.Windows.Forms.Button();
            this.bInit = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.bExport = new System.Windows.Forms.Button();
            this.bFiltre = new System.Windows.Forms.Button();
            this.tbApp = new System.Windows.Forms.ComboBox();
            this.tbTypeVue = new System.Windows.Forms.ComboBox();
            this.tbServerSrc = new System.Windows.Forms.TextBox();
            this.Application = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeServer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServeurSrc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FonctionSrc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IPSrc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServeurCbl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FonctionCbl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IPCbl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbService = new System.Windows.Forms.ComboBox();
            this.cbService = new System.Windows.Forms.ComboBox();
            this.cbFonctionSrc = new System.Windows.Forms.ComboBox();
            this.tbFonctionSrc = new System.Windows.Forms.ComboBox();
            this.tbIPSrc = new System.Windows.Forms.TextBox();
            this.cbIPSrc = new System.Windows.Forms.ComboBox();
            this.tbIPcbl = new System.Windows.Forms.TextBox();
            this.cbIPcbl = new System.Windows.Forms.ComboBox();
            this.tbFonctionCbl = new System.Windows.Forms.ComboBox();
            this.cbFonctionCbl = new System.Windows.Forms.ComboBox();
            this.tbServeurCbl = new System.Windows.Forms.TextBox();
            this.cbServeurCbl = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgFlux)).BeginInit();
            this.SuspendLayout();
            // 
            // dgFlux
            // 
            this.dgFlux.AllowUserToAddRows = false;
            this.dgFlux.AllowUserToDeleteRows = false;
            this.dgFlux.AllowUserToOrderColumns = true;
            this.dgFlux.AllowUserToResizeColumns = false;
            this.dgFlux.AllowUserToResizeRows = false;
            this.dgFlux.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dgFlux.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgFlux.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Application,
            this.Vue,
            this.TypeServer,
            this.ServeurSrc,
            this.FonctionSrc,
            this.IPSrc,
            this.ServeurCbl,
            this.FonctionCbl,
            this.IPCbl});
            this.dgFlux.Location = new System.Drawing.Point(12, 87);
            this.dgFlux.Name = "dgFlux";
            this.dgFlux.RowHeadersVisible = false;
            this.dgFlux.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgFlux.Size = new System.Drawing.Size(944, 381);
            this.dgFlux.TabIndex = 0;
            // 
            // cbApp
            // 
            this.cbApp.FormattingEnabled = true;
            this.cbApp.Location = new System.Drawing.Point(12, 66);
            this.cbApp.MaximumSize = new System.Drawing.Size(120, 0);
            this.cbApp.Name = "cbApp";
            this.cbApp.Size = new System.Drawing.Size(99, 21);
            this.cbApp.TabIndex = 1;
            this.cbApp.SelectedIndexChanged += new System.EventHandler(this.cb_SelectedIndexChanged);
            // 
            // cbTypeVue
            // 
            this.cbTypeVue.FormattingEnabled = true;
            this.cbTypeVue.Location = new System.Drawing.Point(112, 66);
            this.cbTypeVue.Name = "cbTypeVue";
            this.cbTypeVue.Size = new System.Drawing.Size(80, 21);
            this.cbTypeVue.TabIndex = 3;
            this.cbTypeVue.SelectedIndexChanged += new System.EventHandler(this.cb_SelectedIndexChanged);
            // 
            // cbServerSrc
            // 
            this.cbServerSrc.FormattingEnabled = true;
            this.cbServerSrc.Location = new System.Drawing.Point(275, 66);
            this.cbServerSrc.Name = "cbServerSrc";
            this.cbServerSrc.Size = new System.Drawing.Size(108, 21);
            this.cbServerSrc.TabIndex = 5;
            this.cbServerSrc.SelectedIndexChanged += new System.EventHandler(this.cb_SelectedIndexChanged);
            // 
            // bGo
            // 
            this.bGo.Location = new System.Drawing.Point(223, 8);
            this.bGo.Name = "bGo";
            this.bGo.Size = new System.Drawing.Size(69, 25);
            this.bGo.TabIndex = 8;
            this.bGo.Text = "Executer";
            this.bGo.UseVisualStyleBackColor = true;
            this.bGo.Click += new System.EventHandler(this.bGo_Click);
            // 
            // bAdd
            // 
            this.bAdd.Location = new System.Drawing.Point(153, 8);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(69, 25);
            this.bAdd.TabIndex = 12;
            this.bAdd.Text = "Add";
            this.bAdd.UseVisualStyleBackColor = true;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // bInit
            // 
            this.bInit.Location = new System.Drawing.Point(82, 8);
            this.bInit.Name = "bInit";
            this.bInit.Size = new System.Drawing.Size(69, 25);
            this.bInit.TabIndex = 13;
            this.bInit.Text = "Init";
            this.bInit.UseVisualStyleBackColor = true;
            this.bInit.Click += new System.EventHandler(this.bInit_Click);
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(292, 8);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(69, 25);
            this.bSave.TabIndex = 14;
            this.bSave.Text = "Enregistrer";
            this.bSave.UseVisualStyleBackColor = true;
            // 
            // bExport
            // 
            this.bExport.Location = new System.Drawing.Point(361, 8);
            this.bExport.Name = "bExport";
            this.bExport.Size = new System.Drawing.Size(69, 25);
            this.bExport.TabIndex = 15;
            this.bExport.Text = "Exporter";
            this.bExport.UseVisualStyleBackColor = true;
            // 
            // bFiltre
            // 
            this.bFiltre.Location = new System.Drawing.Point(12, 8);
            this.bFiltre.Name = "bFiltre";
            this.bFiltre.Size = new System.Drawing.Size(69, 25);
            this.bFiltre.TabIndex = 17;
            this.bFiltre.Text = "Init Filtre";
            this.bFiltre.UseVisualStyleBackColor = true;
            this.bFiltre.Click += new System.EventHandler(this.bFiltre_Click);
            // 
            // tbApp
            // 
            this.tbApp.FormattingEnabled = true;
            this.tbApp.Items.AddRange(new object[] {
            "none"});
            this.tbApp.Location = new System.Drawing.Point(12, 45);
            this.tbApp.MaximumSize = new System.Drawing.Size(120, 0);
            this.tbApp.Name = "tbApp";
            this.tbApp.Size = new System.Drawing.Size(99, 21);
            this.tbApp.TabIndex = 18;
            // 
            // tbTypeVue
            // 
            this.tbTypeVue.FormattingEnabled = true;
            this.tbTypeVue.Location = new System.Drawing.Point(112, 45);
            this.tbTypeVue.Name = "tbTypeVue";
            this.tbTypeVue.Size = new System.Drawing.Size(80, 21);
            this.tbTypeVue.TabIndex = 19;
            // 
            // tbServerSrc
            // 
            this.tbServerSrc.Location = new System.Drawing.Point(275, 45);
            this.tbServerSrc.Name = "tbServerSrc";
            this.tbServerSrc.Size = new System.Drawing.Size(108, 20);
            this.tbServerSrc.TabIndex = 20;
            // 
            // Application
            // 
            this.Application.HeaderText = "Application";
            this.Application.Name = "Application";
            this.Application.ReadOnly = true;
            // 
            // Vue
            // 
            this.Vue.HeaderText = "Vue";
            this.Vue.Name = "Vue";
            this.Vue.ReadOnly = true;
            this.Vue.Width = 80;
            // 
            // TypeServer
            // 
            this.TypeServer.HeaderText = "Service";
            this.TypeServer.Name = "TypeServer";
            this.TypeServer.ReadOnly = true;
            this.TypeServer.Width = 80;
            // 
            // ServeurSrc
            // 
            this.ServeurSrc.HeaderText = "Serveur Src";
            this.ServeurSrc.Name = "ServeurSrc";
            this.ServeurSrc.ReadOnly = true;
            this.ServeurSrc.Width = 110;
            // 
            // FonctionSrc
            // 
            this.FonctionSrc.HeaderText = "Fonction Src";
            this.FonctionSrc.Name = "FonctionSrc";
            this.FonctionSrc.ReadOnly = true;
            this.FonctionSrc.Width = 110;
            // 
            // IPSrc
            // 
            this.IPSrc.HeaderText = "IP Src";
            this.IPSrc.Name = "IPSrc";
            this.IPSrc.ReadOnly = true;
            // 
            // ServeurCbl
            // 
            this.ServeurCbl.HeaderText = "Serveur Cbl";
            this.ServeurCbl.Name = "ServeurCbl";
            this.ServeurCbl.ReadOnly = true;
            this.ServeurCbl.Width = 110;
            // 
            // FonctionCbl
            // 
            this.FonctionCbl.HeaderText = "Fonction Cbl";
            this.FonctionCbl.Name = "FonctionCbl";
            this.FonctionCbl.ReadOnly = true;
            this.FonctionCbl.Width = 110;
            // 
            // IPCbl
            // 
            this.IPCbl.HeaderText = "IP Cbl";
            this.IPCbl.Name = "IPCbl";
            this.IPCbl.ReadOnly = true;
            // 
            // tbService
            // 
            this.tbService.FormattingEnabled = true;
            this.tbService.Location = new System.Drawing.Point(194, 45);
            this.tbService.Name = "tbService";
            this.tbService.Size = new System.Drawing.Size(80, 21);
            this.tbService.TabIndex = 22;
            // 
            // cbService
            // 
            this.cbService.FormattingEnabled = true;
            this.cbService.Location = new System.Drawing.Point(194, 66);
            this.cbService.Name = "cbService";
            this.cbService.Size = new System.Drawing.Size(80, 21);
            this.cbService.TabIndex = 21;
            // 
            // cbFonctionSrc
            // 
            this.cbFonctionSrc.FormattingEnabled = true;
            this.cbFonctionSrc.Location = new System.Drawing.Point(384, 66);
            this.cbFonctionSrc.Name = "cbFonctionSrc";
            this.cbFonctionSrc.Size = new System.Drawing.Size(108, 21);
            this.cbFonctionSrc.TabIndex = 23;
            // 
            // tbFonctionSrc
            // 
            this.tbFonctionSrc.FormattingEnabled = true;
            this.tbFonctionSrc.Location = new System.Drawing.Point(384, 45);
            this.tbFonctionSrc.Name = "tbFonctionSrc";
            this.tbFonctionSrc.Size = new System.Drawing.Size(108, 21);
            this.tbFonctionSrc.TabIndex = 24;
            // 
            // tbIPSrc
            // 
            this.tbIPSrc.Location = new System.Drawing.Point(493, 45);
            this.tbIPSrc.Name = "tbIPSrc";
            this.tbIPSrc.Size = new System.Drawing.Size(100, 20);
            this.tbIPSrc.TabIndex = 26;
            // 
            // cbIPSrc
            // 
            this.cbIPSrc.FormattingEnabled = true;
            this.cbIPSrc.Location = new System.Drawing.Point(493, 66);
            this.cbIPSrc.Name = "cbIPSrc";
            this.cbIPSrc.Size = new System.Drawing.Size(100, 21);
            this.cbIPSrc.TabIndex = 25;
            // 
            // tbIPcbl
            // 
            this.tbIPcbl.Location = new System.Drawing.Point(813, 45);
            this.tbIPcbl.Name = "tbIPcbl";
            this.tbIPcbl.Size = new System.Drawing.Size(100, 20);
            this.tbIPcbl.TabIndex = 32;
            // 
            // cbIPcbl
            // 
            this.cbIPcbl.FormattingEnabled = true;
            this.cbIPcbl.Location = new System.Drawing.Point(813, 66);
            this.cbIPcbl.Name = "cbIPcbl";
            this.cbIPcbl.Size = new System.Drawing.Size(100, 21);
            this.cbIPcbl.TabIndex = 31;
            // 
            // tbFonctionCbl
            // 
            this.tbFonctionCbl.FormattingEnabled = true;
            this.tbFonctionCbl.Location = new System.Drawing.Point(704, 45);
            this.tbFonctionCbl.Name = "tbFonctionCbl";
            this.tbFonctionCbl.Size = new System.Drawing.Size(108, 21);
            this.tbFonctionCbl.TabIndex = 30;
            // 
            // cbFonctionCbl
            // 
            this.cbFonctionCbl.FormattingEnabled = true;
            this.cbFonctionCbl.Location = new System.Drawing.Point(704, 66);
            this.cbFonctionCbl.Name = "cbFonctionCbl";
            this.cbFonctionCbl.Size = new System.Drawing.Size(108, 21);
            this.cbFonctionCbl.TabIndex = 29;
            // 
            // tbServeurCbl
            // 
            this.tbServeurCbl.Location = new System.Drawing.Point(595, 45);
            this.tbServeurCbl.Name = "tbServeurCbl";
            this.tbServeurCbl.Size = new System.Drawing.Size(108, 20);
            this.tbServeurCbl.TabIndex = 28;
            // 
            // cbServeurCbl
            // 
            this.cbServeurCbl.FormattingEnabled = true;
            this.cbServeurCbl.Location = new System.Drawing.Point(595, 66);
            this.cbServeurCbl.Name = "cbServeurCbl";
            this.cbServeurCbl.Size = new System.Drawing.Size(108, 21);
            this.cbServeurCbl.TabIndex = 27;
            // 
            // FormProvisionServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 480);
            this.Controls.Add(this.tbIPcbl);
            this.Controls.Add(this.cbIPcbl);
            this.Controls.Add(this.tbFonctionCbl);
            this.Controls.Add(this.cbFonctionCbl);
            this.Controls.Add(this.tbServeurCbl);
            this.Controls.Add(this.cbServeurCbl);
            this.Controls.Add(this.tbIPSrc);
            this.Controls.Add(this.cbIPSrc);
            this.Controls.Add(this.tbFonctionSrc);
            this.Controls.Add(this.cbFonctionSrc);
            this.Controls.Add(this.tbService);
            this.Controls.Add(this.cbService);
            this.Controls.Add(this.tbServerSrc);
            this.Controls.Add(this.tbTypeVue);
            this.Controls.Add(this.tbApp);
            this.Controls.Add(this.bFiltre);
            this.Controls.Add(this.bExport);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.bInit);
            this.Controls.Add(this.bAdd);
            this.Controls.Add(this.bGo);
            this.Controls.Add(this.cbServerSrc);
            this.Controls.Add(this.cbTypeVue);
            this.Controls.Add(this.cbApp);
            this.Controls.Add(this.dgFlux);
            this.Name = "FormProvisionServer";
            this.Text = "FormProvisionServer";
            ((System.ComponentModel.ISupportInitialize)(this.dgFlux)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.DataGridView dgFlux;
        private System.Windows.Forms.ComboBox cbApp;
        private System.Windows.Forms.ComboBox cbTypeVue;
        private System.Windows.Forms.ComboBox cbServerSrc;
        private System.Windows.Forms.Button bGo;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.Button bInit;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Button bExport;
        private System.Windows.Forms.Button bFiltre;
        private System.Windows.Forms.ComboBox tbApp;
        private System.Windows.Forms.ComboBox tbTypeVue;
        private System.Windows.Forms.TextBox tbServerSrc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Application;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vue;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeServer;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServeurSrc;
        private System.Windows.Forms.DataGridViewTextBoxColumn FonctionSrc;
        private System.Windows.Forms.DataGridViewTextBoxColumn IPSrc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServeurCbl;
        private System.Windows.Forms.DataGridViewTextBoxColumn FonctionCbl;
        private System.Windows.Forms.DataGridViewTextBoxColumn IPCbl;
        private System.Windows.Forms.ComboBox tbService;
        private System.Windows.Forms.ComboBox cbService;
        private System.Windows.Forms.ComboBox cbFonctionSrc;
        private System.Windows.Forms.ComboBox tbFonctionSrc;
        private System.Windows.Forms.TextBox tbIPSrc;
        private System.Windows.Forms.ComboBox cbIPSrc;
        private System.Windows.Forms.TextBox tbIPcbl;
        private System.Windows.Forms.ComboBox cbIPcbl;
        private System.Windows.Forms.ComboBox tbFonctionCbl;
        private System.Windows.Forms.ComboBox cbFonctionCbl;
        private System.Windows.Forms.TextBox tbServeurCbl;
        private System.Windows.Forms.ComboBox cbServeurCbl;
    }
}