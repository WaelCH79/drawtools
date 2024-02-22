namespace DrawTools
{
    partial class FormFlux1
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
            this.bClose = new System.Windows.Forms.Button();
            this.bExec = new System.Windows.Forms.Button();
            this.lbService = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgFlux = new System.Windows.Forms.DataGridView();
            this.Application = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CibleNom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CibleIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFlux)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bClose);
            this.groupBox1.Controls.Add(this.bExec);
            this.groupBox1.Controls.Add(this.lbService);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(134, 369);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Param";
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(6, 341);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(75, 23);
            this.bClose.TabIndex = 4;
            this.bClose.Text = "Fermer";
            this.bClose.UseVisualStyleBackColor = true;
            // 
            // bExec
            // 
            this.bExec.Location = new System.Drawing.Point(6, 312);
            this.bExec.Name = "bExec";
            this.bExec.Size = new System.Drawing.Size(75, 23);
            this.bExec.TabIndex = 5;
            this.bExec.Text = "Executer";
            this.bExec.UseVisualStyleBackColor = true;
            // 
            // lbService
            // 
            this.lbService.FormattingEnabled = true;
            this.lbService.Location = new System.Drawing.Point(6, 19);
            this.lbService.Name = "lbService";
            this.lbService.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbService.Size = new System.Drawing.Size(120, 290);
            this.lbService.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgFlux);
            this.groupBox2.Location = new System.Drawing.Point(152, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(649, 369);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Param";
            // 
            // dgFlux
            // 
            this.dgFlux.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgFlux.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Application,
            this.SourceName,
            this.SourceIP,
            this.CibleNom,
            this.CibleIP});
            this.dgFlux.Location = new System.Drawing.Point(6, 19);
            this.dgFlux.Name = "dgFlux";
            this.dgFlux.Size = new System.Drawing.Size(637, 344);
            this.dgFlux.TabIndex = 0;
            // 
            // Application
            // 
            this.Application.HeaderText = "Application";
            this.Application.Name = "Application";
            // 
            // SourceName
            // 
            this.SourceName.HeaderText = "SourceNom";
            this.SourceName.Name = "SourceName";
            // 
            // SourceIP
            // 
            this.SourceIP.HeaderText = "SourceIP";
            this.SourceIP.Name = "SourceIP";
            // 
            // CibleNom
            // 
            this.CibleNom.HeaderText = "CibleNom";
            this.CibleNom.Name = "CibleNom";
            // 
            // CibleIP
            // 
            this.CibleIP.HeaderText = "CibleIP";
            this.CibleIP.Name = "CibleIP";
            // 
            // FormFlux
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 393);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormFlux";
            this.Text = "FormFlux";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgFlux)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.Button bExec;
        private System.Windows.Forms.ListBox lbService;
        private System.Windows.Forms.DataGridView dgFlux;
        private System.Windows.Forms.DataGridViewTextBoxColumn Application;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn CibleNom;
        private System.Windows.Forms.DataGridViewTextBoxColumn CibleIP;
    }
}