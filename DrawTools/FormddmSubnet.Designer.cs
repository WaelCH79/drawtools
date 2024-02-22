namespace DrawTools
{
    partial class FormddmSubnet
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
            this.button1 = new System.Windows.Forms.Button();
            this.dgSubnet = new System.Windows.Forms.DataGridView();
            this.tbUpdateDisco = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbGuidddmSubnet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbNomddmSubnet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbGuidVlan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbNomVlan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSubnet)).BeginInit();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgSubnet);
            this.splitContainer1.Size = new System.Drawing.Size(811, 519);
            this.splitContainer1.SplitterDistance = 32;
            this.splitContainer1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // dgSubnet
            // 
            this.dgSubnet.AllowUserToAddRows = false;
            this.dgSubnet.AllowUserToDeleteRows = false;
            this.dgSubnet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgSubnet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSubnet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tbUpdateDisco,
            this.tbGuidddmSubnet,
            this.tbNomddmSubnet,
            this.tbGuidVlan,
            this.tbNomVlan});
            this.dgSubnet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSubnet.Location = new System.Drawing.Point(0, 0);
            this.dgSubnet.Name = "dgSubnet";
            this.dgSubnet.ReadOnly = true;
            this.dgSubnet.RowTemplate.Height = 24;
            this.dgSubnet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSubnet.Size = new System.Drawing.Size(811, 483);
            this.dgSubnet.TabIndex = 0;
            // 
            // tbUpdateDisco
            // 
            this.tbUpdateDisco.HeaderText = "UpdateDisco";
            this.tbUpdateDisco.Name = "tbUpdateDisco";
            this.tbUpdateDisco.ReadOnly = true;
            this.tbUpdateDisco.Width = 118;
            // 
            // tbGuidddmSubnet
            // 
            this.tbGuidddmSubnet.HeaderText = "GuidddmSubnet";
            this.tbGuidddmSubnet.Name = "tbGuidddmSubnet";
            this.tbGuidddmSubnet.ReadOnly = true;
            this.tbGuidddmSubnet.Width = 139;
            // 
            // tbNomddmSubnet
            // 
            this.tbNomddmSubnet.HeaderText = "NomddmSubnet";
            this.tbNomddmSubnet.Name = "tbNomddmSubnet";
            this.tbNomddmSubnet.ReadOnly = true;
            this.tbNomddmSubnet.Width = 138;
            // 
            // tbGuidVlan
            // 
            this.tbGuidVlan.HeaderText = "GuidVlan";
            this.tbGuidVlan.Name = "tbGuidVlan";
            this.tbGuidVlan.ReadOnly = true;
            this.tbGuidVlan.Width = 95;
            // 
            // tbNomVlan
            // 
            this.tbNomVlan.HeaderText = "NomVlan";
            this.tbNomVlan.Name = "tbNomVlan";
            this.tbNomVlan.ReadOnly = true;
            this.tbNomVlan.Width = 94;
            // 
            // FormddmSubnet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 519);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormddmSubnet";
            this.Text = "FormddmServeurs";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgSubnet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgSubnet;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbUpdateDisco;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbGuidddmSubnet;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbNomddmSubnet;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbGuidVlan;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbNomVlan;
    }
}