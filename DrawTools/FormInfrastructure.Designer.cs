namespace DrawTools
{
    partial class FormInfrastructure
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgApplication = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgComposant = new System.Windows.Forms.DataGridView();
            this.GuidComposant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Composant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Criticite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatutComp = new System.Windows.Forms.DataGridViewImageColumn();
            this.Norme = new System.Windows.Forms.DataGridViewImageColumn();
            this.bReportAll = new System.Windows.Forms.Button();
            this.bReport = new System.Windows.Forms.Button();
            this.bImport = new System.Windows.Forms.Button();
            this.bExportAll = new System.Windows.Forms.Button();
            this.bExport = new System.Windows.Forms.Button();
            this.GuidApplication = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Application = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GuidAppVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AppVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GuidVue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Statut = new System.Windows.Forms.DataGridViewImageColumn();
            this.NomTypeVue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgApplication)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgComposant)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgApplication);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(707, 181);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Application";
            // 
            // dgApplication
            // 
            this.dgApplication.AllowUserToAddRows = false;
            this.dgApplication.AllowUserToDeleteRows = false;
            this.dgApplication.AllowUserToResizeColumns = false;
            this.dgApplication.AllowUserToResizeRows = false;
            this.dgApplication.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgApplication.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GuidApplication,
            this.Application,
            this.GuidAppVersion,
            this.AppVersion,
            this.GuidVue,
            this.Vue,
            this.Column1,
            this.Statut,
            this.NomTypeVue});
            this.dgApplication.Location = new System.Drawing.Point(6, 19);
            this.dgApplication.MultiSelect = false;
            this.dgApplication.Name = "dgApplication";
            this.dgApplication.ReadOnly = true;
            this.dgApplication.RowHeadersVisible = false;
            this.dgApplication.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgApplication.Size = new System.Drawing.Size(690, 151);
            this.dgApplication.TabIndex = 0;
            this.dgApplication.SelectionChanged += new System.EventHandler(this.dgApplication_SelectionChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgComposant);
            this.groupBox2.Location = new System.Drawing.Point(12, 199);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(609, 300);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Composant";
            // 
            // dgComposant
            // 
            this.dgComposant.AllowUserToAddRows = false;
            this.dgComposant.AllowUserToDeleteRows = false;
            this.dgComposant.AllowUserToResizeColumns = false;
            this.dgComposant.AllowUserToResizeRows = false;
            this.dgComposant.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgComposant.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GuidComposant,
            this.Composant,
            this.Version,
            this.Date,
            this.Criticite,
            this.StatutComp,
            this.Norme});
            this.dgComposant.Location = new System.Drawing.Point(6, 19);
            this.dgComposant.Name = "dgComposant";
            this.dgComposant.RowHeadersVisible = false;
            this.dgComposant.RowTemplate.Height = 21;
            this.dgComposant.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgComposant.Size = new System.Drawing.Size(597, 275);
            this.dgComposant.TabIndex = 1;
            // 
            // GuidComposant
            // 
            this.GuidComposant.HeaderText = "GuidComposant";
            this.GuidComposant.Name = "GuidComposant";
            this.GuidComposant.Visible = false;
            // 
            // Composant
            // 
            this.Composant.HeaderText = "Composant";
            this.Composant.Name = "Composant";
            this.Composant.Width = 200;
            // 
            // Version
            // 
            this.Version.HeaderText = "Version";
            this.Version.Name = "Version";
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            // 
            // Criticite
            // 
            this.Criticite.HeaderText = "Criticite";
            this.Criticite.Name = "Criticite";
            this.Criticite.Width = 50;
            // 
            // StatutComp
            // 
            this.StatutComp.HeaderText = "Statut";
            this.StatutComp.Name = "StatutComp";
            this.StatutComp.Width = 50;
            // 
            // Norme
            // 
            this.Norme.HeaderText = "N&S";
            this.Norme.Name = "Norme";
            this.Norme.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Norme.Width = 50;
            // 
            // bReportAll
            // 
            this.bReportAll.Location = new System.Drawing.Point(12, 505);
            this.bReportAll.Name = "bReportAll";
            this.bReportAll.Size = new System.Drawing.Size(79, 22);
            this.bReportAll.TabIndex = 5;
            this.bReportAll.Text = "Total Report ";
            this.bReportAll.UseVisualStyleBackColor = true;
            this.bReportAll.Click += new System.EventHandler(this.bReportAll_Click);
            // 
            // bReport
            // 
            this.bReport.Location = new System.Drawing.Point(97, 505);
            this.bReport.Name = "bReport";
            this.bReport.Size = new System.Drawing.Size(79, 22);
            this.bReport.TabIndex = 6;
            this.bReport.Text = "Report ";
            this.bReport.UseVisualStyleBackColor = true;
            this.bReport.Click += new System.EventHandler(this.bReport_Click);
            // 
            // bImport
            // 
            this.bImport.Location = new System.Drawing.Point(182, 505);
            this.bImport.Name = "bImport";
            this.bImport.Size = new System.Drawing.Size(79, 22);
            this.bImport.TabIndex = 8;
            this.bImport.Text = "Import";
            this.bImport.UseVisualStyleBackColor = true;
            this.bImport.Click += new System.EventHandler(this.bImport_Click);
            // 
            // bExportAll
            // 
            this.bExportAll.Location = new System.Drawing.Point(267, 505);
            this.bExportAll.Name = "bExportAll";
            this.bExportAll.Size = new System.Drawing.Size(79, 22);
            this.bExportAll.TabIndex = 9;
            this.bExportAll.Text = "Total Export";
            this.bExportAll.UseVisualStyleBackColor = true;
            this.bExportAll.Click += new System.EventHandler(this.bExportAll_Click);
            // 
            // bExport
            // 
            this.bExport.Location = new System.Drawing.Point(352, 505);
            this.bExport.Name = "bExport";
            this.bExport.Size = new System.Drawing.Size(79, 22);
            this.bExport.TabIndex = 10;
            this.bExport.Text = "Export";
            this.bExport.UseVisualStyleBackColor = true;
            this.bExport.Click += new System.EventHandler(this.bExport_Click);
            // 
            // GuidApplication
            // 
            this.GuidApplication.HeaderText = "GuidApplication";
            this.GuidApplication.Name = "GuidApplication";
            this.GuidApplication.ReadOnly = true;
            this.GuidApplication.Visible = false;
            // 
            // Application
            // 
            this.Application.HeaderText = "Application";
            this.Application.Name = "Application";
            this.Application.ReadOnly = true;
            this.Application.Width = 180;
            // 
            // GuidAppVersion
            // 
            this.GuidAppVersion.HeaderText = "GuidAppVersion";
            this.GuidAppVersion.Name = "GuidAppVersion";
            this.GuidAppVersion.ReadOnly = true;
            this.GuidAppVersion.Visible = false;
            // 
            // AppVersion
            // 
            this.AppVersion.HeaderText = "Version";
            this.AppVersion.Name = "AppVersion";
            this.AppVersion.ReadOnly = true;
            // 
            // GuidVue
            // 
            this.GuidVue.HeaderText = "GuidVue";
            this.GuidVue.Name = "GuidVue";
            this.GuidVue.ReadOnly = true;
            this.GuidVue.Visible = false;
            // 
            // Vue
            // 
            this.Vue.HeaderText = "Vue";
            this.Vue.Name = "Vue";
            this.Vue.ReadOnly = true;
            this.Vue.Width = 150;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Date";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 180;
            // 
            // Statut
            // 
            this.Statut.HeaderText = "Status";
            this.Statut.Name = "Statut";
            this.Statut.ReadOnly = true;
            this.Statut.Width = 50;
            // 
            // NomTypeVue
            // 
            this.NomTypeVue.HeaderText = "NomTypeVue";
            this.NomTypeVue.Name = "NomTypeVue";
            this.NomTypeVue.ReadOnly = true;
            this.NomTypeVue.Visible = false;
            // 
            // FormInfrastructure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 545);
            this.Controls.Add(this.bExport);
            this.Controls.Add(this.bExportAll);
            this.Controls.Add(this.bImport);
            this.Controls.Add(this.bReport);
            this.Controls.Add(this.bReportAll);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormInfrastructure";
            this.Text = "FormInfrastructure";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgApplication)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgComposant)).EndInit();
            this.ResumeLayout(false);

        }

        
        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgComposant;
        private System.Windows.Forms.DataGridView dgApplication;
        private System.Windows.Forms.DataGridViewTextBoxColumn GuidComposant;
        private System.Windows.Forms.DataGridViewTextBoxColumn Composant;
        private System.Windows.Forms.DataGridViewTextBoxColumn Version;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Criticite;
        private System.Windows.Forms.DataGridViewImageColumn StatutComp;
        private System.Windows.Forms.DataGridViewImageColumn Norme;
        private System.Windows.Forms.Button bReportAll;
        private System.Windows.Forms.Button bReport;
        private System.Windows.Forms.Button bImport;
        private System.Windows.Forms.Button bExportAll;
        private System.Windows.Forms.Button bExport;
        private System.Windows.Forms.DataGridViewTextBoxColumn GuidApplication;
        private System.Windows.Forms.DataGridViewTextBoxColumn Application;
        private System.Windows.Forms.DataGridViewTextBoxColumn GuidAppVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn AppVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn GuidVue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewImageColumn Statut;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomTypeVue;
    }
}