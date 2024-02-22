namespace DrawTools
{
    partial class FormIndicator
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
            this.bOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgIndicator = new System.Windows.Forms.DataGridView();
            this.GuidIndicator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomIndicator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lien = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Sup = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgIndicator)).BeginInit();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(12, 255);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(65, 21);
            this.bOK.TabIndex = 5;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgIndicator);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(608, 237);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Object";
            // 
            // dgIndicator
            // 
            this.dgIndicator.AllowUserToAddRows = false;
            this.dgIndicator.AllowUserToDeleteRows = false;
            this.dgIndicator.AllowUserToResizeColumns = false;
            this.dgIndicator.AllowUserToResizeRows = false;
            this.dgIndicator.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgIndicator.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GuidIndicator,
            this.NomIndicator,
            this.Type,
            this.Value,
            this.Lien,
            this.Sup});
            this.dgIndicator.Location = new System.Drawing.Point(6, 19);
            this.dgIndicator.MultiSelect = false;
            this.dgIndicator.Name = "dgIndicator";
            this.dgIndicator.RowHeadersVisible = false;
            this.dgIndicator.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgIndicator.Size = new System.Drawing.Size(596, 212);
            this.dgIndicator.TabIndex = 0;
            // 
            // GuidIndicator
            // 
            this.GuidIndicator.HeaderText = "GuiIndicator";
            this.GuidIndicator.Name = "GuidIndicator";
            this.GuidIndicator.ReadOnly = true;
            this.GuidIndicator.Visible = false;
            // 
            // NomIndicator
            // 
            this.NomIndicator.HeaderText = "Indicator";
            this.NomIndicator.Name = "NomIndicator";
            this.NomIndicator.ReadOnly = true;
            this.NomIndicator.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.NomIndicator.Width = 150;
            // 
            // Type
            // 
            this.Type.HeaderText = "type";
            this.Type.Name = "Type";
            this.Type.Visible = false;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Lien
            // 
            this.Lien.HeaderText = "Lien";
            this.Lien.Name = "Lien";
            this.Lien.ReadOnly = true;
            this.Lien.Width = 300;
            // 
            // Sup
            // 
            this.Sup.HeaderText = "x";
            this.Sup.Name = "Sup";
            this.Sup.ReadOnly = true;
            this.Sup.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Sup.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Sup.Width = 20;
            // 
            // FormIndicator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 289);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bOK);
            this.Name = "FormIndicator";
            this.Text = "FormIndicator";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgIndicator)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgIndicator;
        private System.Windows.Forms.DataGridViewTextBoxColumn GuidIndicator;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomIndicator;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewLinkColumn Lien;
        private System.Windows.Forms.DataGridViewButtonColumn Sup;
    }
}