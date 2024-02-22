namespace DrawTools
{
    partial class FormExplorObj
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvObj = new System.Windows.Forms.TreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgProp = new System.Windows.Forms.DataGridView();
            this.Property = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvExpObj = new System.Windows.Forms.DataGridView();
            this.ExpObjPropriete = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpObjValeur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpObjPls = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ExpObjNomVisible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkbAllVersions = new System.Windows.Forms.CheckBox();
            this.bExport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgProp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpObj)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(2, 45);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvObj);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1018, 613);
            this.splitContainer1.SplitterDistance = 394;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvObj
            // 
            this.tvObj.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvObj.Location = new System.Drawing.Point(0, 0);
            this.tvObj.Margin = new System.Windows.Forms.Padding(4);
            this.tvObj.Name = "tvObj";
            this.tvObj.Size = new System.Drawing.Size(390, 609);
            this.tvObj.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgProp);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgvExpObj);
            this.splitContainer2.Size = new System.Drawing.Size(615, 609);
            this.splitContainer2.SplitterDistance = 202;
            this.splitContainer2.TabIndex = 2;
            // 
            // dgProp
            // 
            this.dgProp.AllowUserToAddRows = false;
            this.dgProp.AllowUserToDeleteRows = false;
            this.dgProp.AllowUserToResizeColumns = false;
            this.dgProp.AllowUserToResizeRows = false;
            this.dgProp.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dgProp.ColumnHeadersHeight = 21;
            this.dgProp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgProp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Property,
            this.Value});
            this.dgProp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgProp.Location = new System.Drawing.Point(0, 0);
            this.dgProp.Margin = new System.Windows.Forms.Padding(4);
            this.dgProp.Name = "dgProp";
            this.dgProp.RowHeadersVisible = false;
            this.dgProp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgProp.Size = new System.Drawing.Size(615, 202);
            this.dgProp.TabIndex = 0;
            // 
            // Property
            // 
            this.Property.HeaderText = "Property";
            this.Property.Name = "Property";
            this.Property.Width = 140;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.Width = 140;
            // 
            // dgvExpObj
            // 
            this.dgvExpObj.AllowUserToAddRows = false;
            this.dgvExpObj.AllowUserToDeleteRows = false;
            this.dgvExpObj.AllowUserToResizeColumns = false;
            this.dgvExpObj.AllowUserToResizeRows = false;
            this.dgvExpObj.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dgvExpObj.ColumnHeadersHeight = 21;
            this.dgvExpObj.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvExpObj.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ExpObjPropriete,
            this.ExpObjValeur,
            this.ExpObjPls,
            this.ExpObjNomVisible});
            this.dgvExpObj.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExpObj.Location = new System.Drawing.Point(0, 0);
            this.dgvExpObj.Margin = new System.Windows.Forms.Padding(4);
            this.dgvExpObj.Name = "dgvExpObj";
            this.dgvExpObj.RowHeadersVisible = false;
            this.dgvExpObj.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvExpObj.Size = new System.Drawing.Size(615, 403);
            this.dgvExpObj.TabIndex = 1;
            this.dgvExpObj.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellClick);
            // 
            // ExpObjPropriete
            // 
            this.ExpObjPropriete.HeaderText = "Propritete";
            this.ExpObjPropriete.Name = "ExpObjPropriete";
            this.ExpObjPropriete.Width = 140;
            // 
            // ExpObjValeur
            // 
            this.ExpObjValeur.HeaderText = "Valeur";
            this.ExpObjValeur.Name = "ExpObjValeur";
            this.ExpObjValeur.Width = 140;
            // 
            // ExpObjPls
            // 
            this.ExpObjPls.HeaderText = "";
            this.ExpObjPls.Name = "ExpObjPls";
            this.ExpObjPls.Width = 15;
            // 
            // ExpObjNomVisible
            // 
            this.ExpObjNomVisible.HeaderText = "NomVisible";
            this.ExpObjNomVisible.Name = "ExpObjNomVisible";
            this.ExpObjNomVisible.Visible = false;
            // 
            // checkbAllVersions
            // 
            this.checkbAllVersions.AutoSize = true;
            this.checkbAllVersions.Location = new System.Drawing.Point(12, 12);
            this.checkbAllVersions.Name = "checkbAllVersions";
            this.checkbAllVersions.Size = new System.Drawing.Size(238, 21);
            this.checkbAllVersions.TabIndex = 1;
            this.checkbAllVersions.Text = "Recherche sur toute les versions";
            this.checkbAllVersions.UseVisualStyleBackColor = true;
            // 
            // bExport
            // 
            this.bExport.Location = new System.Drawing.Point(283, 10);
            this.bExport.Name = "bExport";
            this.bExport.Size = new System.Drawing.Size(75, 23);
            this.bExport.TabIndex = 2;
            this.bExport.Text = "Exporter";
            this.bExport.UseVisualStyleBackColor = true;
            // 
            // FormExplorObj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 661);
            this.Controls.Add(this.bExport);
            this.Controls.Add(this.checkbAllVersions);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormExplorObj";
            this.Text = "FormExplorObj";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgProp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpObj)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

                       
        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvObj;
        public System.Windows.Forms.DataGridView dgProp;
        private System.Windows.Forms.DataGridViewTextBoxColumn Property;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        public System.Windows.Forms.DataGridView dgvExpObj;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpObjPropriete;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpObjValeur;
        private System.Windows.Forms.DataGridViewButtonColumn ExpObjPls;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpObjNomVisible;
        private System.Windows.Forms.CheckBox checkbAllVersions;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button bExport;
    }
}