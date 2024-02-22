namespace DrawTools
{
    partial class FormAppServer
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
            this.dgServer = new System.Windows.Forms.DataGridView();
            this.cbApp = new System.Windows.Forms.ComboBox();
            this.cbGuidApp = new System.Windows.Forms.ComboBox();
            this.bExport = new System.Windows.Forms.Button();
            this.bExportAll = new System.Windows.Forms.Button();
            this.cbVersion = new System.Windows.Forms.ComboBox();
            this.Application = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Env = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fonction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Serveur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dmz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vlan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IPAddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CoreA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MemoireA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Core = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgServer)).BeginInit();
            this.SuspendLayout();
            // 
            // dgServer
            // 
            this.dgServer.AllowUserToAddRows = false;
            this.dgServer.AllowUserToDeleteRows = false;
            this.dgServer.AllowUserToOrderColumns = true;
            this.dgServer.AllowUserToResizeColumns = false;
            this.dgServer.AllowUserToResizeRows = false;
            this.dgServer.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dgServer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgServer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Application,
            this.Env,
            this.Fonction,
            this.Serveur,
            this.Type,
            this.dmz,
            this.Vlan,
            this.IPAddr,
            this.CoreA,
            this.MemoireA,
            this.Core});
            this.dgServer.Location = new System.Drawing.Point(16, 44);
            this.dgServer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgServer.Name = "dgServer";
            this.dgServer.RowHeadersVisible = false;
            this.dgServer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgServer.Size = new System.Drawing.Size(1259, 532);
            this.dgServer.TabIndex = 0;
            // 
            // cbApp
            // 
            this.cbApp.FormattingEnabled = true;
            this.cbApp.Items.AddRange(new object[] {
            "none"});
            this.cbApp.Location = new System.Drawing.Point(16, 11);
            this.cbApp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbApp.MaximumSize = new System.Drawing.Size(399, 0);
            this.cbApp.Name = "cbApp";
            this.cbApp.Size = new System.Drawing.Size(399, 24);
            this.cbApp.TabIndex = 18;
            // 
            // cbGuidApp
            // 
            this.cbGuidApp.FormattingEnabled = true;
            this.cbGuidApp.Items.AddRange(new object[] {
            "none"});
            this.cbGuidApp.Location = new System.Drawing.Point(897, 11);
            this.cbGuidApp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbGuidApp.MaximumSize = new System.Drawing.Size(265, 0);
            this.cbGuidApp.Name = "cbGuidApp";
            this.cbGuidApp.Size = new System.Drawing.Size(83, 24);
            this.cbGuidApp.TabIndex = 19;
            this.cbGuidApp.Visible = false;
            // 
            // bExport
            // 
            this.bExport.Location = new System.Drawing.Point(608, 11);
            this.bExport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bExport.Name = "bExport";
            this.bExport.Size = new System.Drawing.Size(100, 28);
            this.bExport.TabIndex = 20;
            this.bExport.Text = "Export";
            this.bExport.UseVisualStyleBackColor = true;
            // 
            // bExportAll
            // 
            this.bExportAll.Location = new System.Drawing.Point(716, 11);
            this.bExportAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bExportAll.Name = "bExportAll";
            this.bExportAll.Size = new System.Drawing.Size(100, 28);
            this.bExportAll.TabIndex = 21;
            this.bExportAll.Text = "Export All";
            this.bExportAll.UseVisualStyleBackColor = true;
            // 
            // cbVersion
            // 
            this.cbVersion.FormattingEnabled = true;
            this.cbVersion.Items.AddRange(new object[] {
            "none"});
            this.cbVersion.Location = new System.Drawing.Point(420, 11);
            this.cbVersion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbVersion.MaximumSize = new System.Drawing.Size(399, 0);
            this.cbVersion.Name = "cbVersion";
            this.cbVersion.Size = new System.Drawing.Size(179, 24);
            this.cbVersion.TabIndex = 22;
            // 
            // Application
            // 
            this.Application.HeaderText = "Application";
            this.Application.Name = "Application";
            this.Application.ReadOnly = true;
            // 
            // Env
            // 
            this.Env.HeaderText = "Env";
            this.Env.Name = "Env";
            this.Env.ReadOnly = true;
            this.Env.Width = 30;
            // 
            // Fonction
            // 
            this.Fonction.HeaderText = "Fonction";
            this.Fonction.Name = "Fonction";
            // 
            // Serveur
            // 
            this.Serveur.HeaderText = "Serveur";
            this.Serveur.Name = "Serveur";
            this.Serveur.ReadOnly = true;
            this.Serveur.Width = 110;
            // 
            // Type
            // 
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Width = 40;
            // 
            // dmz
            // 
            this.dmz.HeaderText = "DMZ";
            this.dmz.Name = "dmz";
            this.dmz.ReadOnly = true;
            this.dmz.Width = 200;
            // 
            // Vlan
            // 
            this.Vlan.HeaderText = "Vlan";
            this.Vlan.Name = "Vlan";
            this.Vlan.Width = 50;
            // 
            // IPAddr
            // 
            this.IPAddr.HeaderText = "Adresse";
            this.IPAddr.Name = "IPAddr";
            this.IPAddr.ReadOnly = true;
            // 
            // CoreA
            // 
            this.CoreA.HeaderText = "Core";
            this.CoreA.Name = "CoreA";
            this.CoreA.ReadOnly = true;
            this.CoreA.Width = 40;
            // 
            // MemoireA
            // 
            this.MemoireA.HeaderText = "Ram";
            this.MemoireA.Name = "MemoireA";
            this.MemoireA.ReadOnly = true;
            this.MemoireA.Width = 40;
            // 
            // Core
            // 
            this.Core.HeaderText = "Disk";
            this.Core.Name = "Core";
            this.Core.ReadOnly = true;
            this.Core.Width = 40;
            // 
            // FormAppServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1291, 591);
            this.Controls.Add(this.cbVersion);
            this.Controls.Add(this.bExportAll);
            this.Controls.Add(this.bExport);
            this.Controls.Add(this.cbGuidApp);
            this.Controls.Add(this.cbApp);
            this.Controls.Add(this.dgServer);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormAppServer";
            this.Text = "FormProvisionServer";
            ((System.ComponentModel.ISupportInitialize)(this.dgServer)).EndInit();
            this.ResumeLayout(false);

        }

                
        #endregion

        private System.Windows.Forms.DataGridView dgServer;
        private System.Windows.Forms.ComboBox cbApp;
        private System.Windows.Forms.ComboBox cbGuidApp;
        private System.Windows.Forms.Button bExport;
        private System.Windows.Forms.Button bExportAll;
        private System.Windows.Forms.ComboBox cbVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Application;
        private System.Windows.Forms.DataGridViewTextBoxColumn Env;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fonction;
        private System.Windows.Forms.DataGridViewTextBoxColumn Serveur;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn dmz;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vlan;
        private System.Windows.Forms.DataGridViewTextBoxColumn IPAddr;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoreA;
        private System.Windows.Forms.DataGridViewTextBoxColumn MemoireA;
        private System.Windows.Forms.DataGridViewTextBoxColumn Core;
    }
}