namespace DrawTools
{
    partial class AECBForm
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
            this.dgEACB = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Flux = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IPSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LieuxSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomCible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IPCible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LieuxCible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Service = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Protocole = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ports = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bGo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbApplication = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbVue = new System.Windows.Forms.ComboBox();
            this.bQuit = new System.Windows.Forms.Button();
            this.cbGuidVue = new System.Windows.Forms.ComboBox();
            this.bExport = new System.Windows.Forms.Button();
            this.tbAppVersion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dgEACB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgEACB
            // 
            this.dgEACB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEACB.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Flux,
            this.NomSource,
            this.IPSource,
            this.LieuxSource,
            this.NomCible,
            this.IPCible,
            this.LieuxCible,
            this.Service,
            this.Protocole,
            this.Ports});
            this.dgEACB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgEACB.Location = new System.Drawing.Point(0, 0);
            this.dgEACB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgEACB.Name = "dgEACB";
            this.dgEACB.ReadOnly = true;
            this.dgEACB.Size = new System.Drawing.Size(1383, 470);
            this.dgEACB.TabIndex = 0;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Width = 30;
            // 
            // Flux
            // 
            this.Flux.HeaderText = "Flux";
            this.Flux.Name = "Flux";
            this.Flux.ReadOnly = true;
            this.Flux.Width = 140;
            // 
            // NomSource
            // 
            this.NomSource.HeaderText = "Nom Source";
            this.NomSource.Name = "NomSource";
            this.NomSource.ReadOnly = true;
            this.NomSource.Width = 110;
            // 
            // IPSource
            // 
            this.IPSource.HeaderText = "IP Source";
            this.IPSource.Name = "IPSource";
            this.IPSource.ReadOnly = true;
            // 
            // LieuxSource
            // 
            this.LieuxSource.HeaderText = "Localisation Source";
            this.LieuxSource.Name = "LieuxSource";
            this.LieuxSource.ReadOnly = true;
            this.LieuxSource.Width = 80;
            // 
            // NomCible
            // 
            this.NomCible.HeaderText = "Nom Cible";
            this.NomCible.Name = "NomCible";
            this.NomCible.ReadOnly = true;
            this.NomCible.Width = 110;
            // 
            // IPCible
            // 
            this.IPCible.HeaderText = "IP Cible";
            this.IPCible.Name = "IPCible";
            this.IPCible.ReadOnly = true;
            // 
            // LieuxCible
            // 
            this.LieuxCible.HeaderText = "Localisation Cible";
            this.LieuxCible.Name = "LieuxCible";
            this.LieuxCible.ReadOnly = true;
            this.LieuxCible.Width = 80;
            // 
            // Service
            // 
            this.Service.HeaderText = "Service";
            this.Service.Name = "Service";
            this.Service.ReadOnly = true;
            this.Service.Width = 80;
            // 
            // Protocole
            // 
            this.Protocole.HeaderText = "Protocole";
            this.Protocole.Name = "Protocole";
            this.Protocole.ReadOnly = true;
            this.Protocole.Width = 55;
            // 
            // Ports
            // 
            this.Ports.HeaderText = "Ports";
            this.Ports.Name = "Ports";
            this.Ports.ReadOnly = true;
            this.Ports.Width = 80;
            // 
            // bGo
            // 
            this.bGo.Location = new System.Drawing.Point(885, 17);
            this.bGo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bGo.Name = "bGo";
            this.bGo.Size = new System.Drawing.Size(100, 28);
            this.bGo.TabIndex = 1;
            this.bGo.Text = "Executer";
            this.bGo.UseVisualStyleBackColor = true;
            this.bGo.Click += new System.EventHandler(this.bGo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Application";
            // 
            // tbApplication
            // 
            this.tbApplication.Location = new System.Drawing.Point(105, 18);
            this.tbApplication.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbApplication.Name = "tbApplication";
            this.tbApplication.ReadOnly = true;
            this.tbApplication.Size = new System.Drawing.Size(189, 22);
            this.tbApplication.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(610, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Vue";
            // 
            // cbVue
            // 
            this.cbVue.FormattingEnabled = true;
            this.cbVue.Location = new System.Drawing.Point(649, 17);
            this.cbVue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbVue.Name = "cbVue";
            this.cbVue.Size = new System.Drawing.Size(217, 24);
            this.cbVue.TabIndex = 5;
            // 
            // bQuit
            // 
            this.bQuit.Location = new System.Drawing.Point(1101, 17);
            this.bQuit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bQuit.Name = "bQuit";
            this.bQuit.Size = new System.Drawing.Size(100, 28);
            this.bQuit.TabIndex = 6;
            this.bQuit.Text = "Quitter";
            this.bQuit.UseVisualStyleBackColor = true;
            this.bQuit.Click += new System.EventHandler(this.bQuit_Click);
            // 
            // cbGuidVue
            // 
            this.cbGuidVue.FormattingEnabled = true;
            this.cbGuidVue.Location = new System.Drawing.Point(680, 2);
            this.cbGuidVue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbGuidVue.Name = "cbGuidVue";
            this.cbGuidVue.Size = new System.Drawing.Size(57, 24);
            this.cbGuidVue.TabIndex = 7;
            this.cbGuidVue.Visible = false;
            // 
            // bExport
            // 
            this.bExport.Location = new System.Drawing.Point(993, 17);
            this.bExport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bExport.Name = "bExport";
            this.bExport.Size = new System.Drawing.Size(100, 28);
            this.bExport.TabIndex = 8;
            this.bExport.Text = "Exporter";
            this.bExport.UseVisualStyleBackColor = true;
            this.bExport.Click += new System.EventHandler(this.bExport_Click);
            // 
            // tbAppVersion
            // 
            this.tbAppVersion.Location = new System.Drawing.Point(400, 19);
            this.tbAppVersion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbAppVersion.Name = "tbAppVersion";
            this.tbAppVersion.ReadOnly = true;
            this.tbAppVersion.Size = new System.Drawing.Size(189, 22);
            this.tbAppVersion.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(341, 24);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Version";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.bGo);
            this.splitContainer1.Panel1.Controls.Add(this.tbAppVersion);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.tbApplication);
            this.splitContainer1.Panel1.Controls.Add(this.bExport);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.cbGuidVue);
            this.splitContainer1.Panel1.Controls.Add(this.cbVue);
            this.splitContainer1.Panel1.Controls.Add(this.bQuit);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgEACB);
            this.splitContainer1.Size = new System.Drawing.Size(1383, 527);
            this.splitContainer1.SplitterDistance = 53;
            this.splitContainer1.TabIndex = 1;
            // 
            // AECBForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1383, 527);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AECBForm";
            this.Text = "AECBForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgEACB)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bGo;
        public System.Windows.Forms.DataGridView dgEACB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbApplication;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbVue;
        private System.Windows.Forms.Button bQuit;
        private System.Windows.Forms.ComboBox cbGuidVue;
        private System.Windows.Forms.Button bExport;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Flux;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn IPSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn LieuxSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomCible;
        private System.Windows.Forms.DataGridViewTextBoxColumn IPCible;
        private System.Windows.Forms.DataGridViewTextBoxColumn LieuxCible;
        private System.Windows.Forms.DataGridViewTextBoxColumn Service;
        private System.Windows.Forms.DataGridViewTextBoxColumn Protocole;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ports;
        private System.Windows.Forms.TextBox tbAppVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}