namespace DrawTools
{
    partial class FormLabel
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
            this.bSup = new System.Windows.Forms.Button();
            this.bAdd = new System.Windows.Forms.Button();
            this.tvLabelClass = new System.Windows.Forms.TreeView();
            this.bAddLink = new System.Windows.Forms.Button();
            this.bSupLink = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvLabel = new System.Windows.Forms.DataGridView();
            this.bAnnuler = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.GuidLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabel)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bSup);
            this.groupBox1.Controls.Add(this.bAdd);
            this.groupBox1.Controls.Add(this.tvLabelClass);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(445, 426);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Liste des Labels / Valeurs";
            // 
            // bSup
            // 
            this.bSup.Location = new System.Drawing.Point(87, 21);
            this.bSup.Name = "bSup";
            this.bSup.Size = new System.Drawing.Size(75, 23);
            this.bSup.TabIndex = 2;
            this.bSup.Text = "-";
            this.bSup.UseVisualStyleBackColor = true;
            // 
            // bAdd
            // 
            this.bAdd.Location = new System.Drawing.Point(6, 21);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(75, 23);
            this.bAdd.TabIndex = 1;
            this.bAdd.Text = "+";
            this.bAdd.UseVisualStyleBackColor = true;
            // 
            // tvLabelClass
            // 
            this.tvLabelClass.Location = new System.Drawing.Point(6, 50);
            this.tvLabelClass.Name = "tvLabelClass";
            this.tvLabelClass.Size = new System.Drawing.Size(433, 370);
            this.tvLabelClass.TabIndex = 0;
            // 
            // bAddLink
            // 
            this.bAddLink.Enabled = false;
            this.bAddLink.Location = new System.Drawing.Point(463, 148);
            this.bAddLink.Name = "bAddLink";
            this.bAddLink.Size = new System.Drawing.Size(35, 23);
            this.bAddLink.TabIndex = 3;
            this.bAddLink.Text = ">";
            this.bAddLink.UseVisualStyleBackColor = true;
            // 
            // bSupLink
            // 
            this.bSupLink.Enabled = false;
            this.bSupLink.Location = new System.Drawing.Point(463, 177);
            this.bSupLink.Name = "bSupLink";
            this.bSupLink.Size = new System.Drawing.Size(35, 23);
            this.bSupLink.TabIndex = 4;
            this.bSupLink.Text = "<";
            this.bSupLink.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvLabel);
            this.groupBox2.Controls.Add(this.bAnnuler);
            this.groupBox2.Controls.Add(this.bSave);
            this.groupBox2.Location = new System.Drawing.Point(507, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(281, 426);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Valeurs associées";
            // 
            // dgvLabel
            // 
            this.dgvLabel.AllowUserToAddRows = false;
            this.dgvLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLabel.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvLabel.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvLabel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvLabel.ColumnHeadersVisible = false;
            this.dgvLabel.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GuidLabel,
            this.NomLabel});
            this.dgvLabel.EnableHeadersVisualStyles = false;
            this.dgvLabel.Location = new System.Drawing.Point(6, 50);
            this.dgvLabel.MultiSelect = false;
            this.dgvLabel.Name = "dgvLabel";
            this.dgvLabel.ReadOnly = true;
            this.dgvLabel.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvLabel.RowHeadersVisible = false;
            this.dgvLabel.RowTemplate.Height = 24;
            this.dgvLabel.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvLabel.Size = new System.Drawing.Size(269, 370);
            this.dgvLabel.TabIndex = 6;
            // 
            // bAnnuler
            // 
            this.bAnnuler.Location = new System.Drawing.Point(87, 19);
            this.bAnnuler.Name = "bAnnuler";
            this.bAnnuler.Size = new System.Drawing.Size(75, 23);
            this.bAnnuler.TabIndex = 4;
            this.bAnnuler.Text = "Annuler";
            this.bAnnuler.UseVisualStyleBackColor = true;
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(6, 19);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(75, 23);
            this.bSave.TabIndex = 3;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = true;
            // 
            // GuidLabel
            // 
            this.GuidLabel.HeaderText = "GuidLabel";
            this.GuidLabel.Name = "GuidLabel";
            this.GuidLabel.ReadOnly = true;
            this.GuidLabel.Visible = false;
            // 
            // NomLabel
            // 
            this.NomLabel.HeaderText = "NomLabel";
            this.NomLabel.Name = "NomLabel";
            this.NomLabel.ReadOnly = true;
            this.NomLabel.Width = 220;
            // 
            // FormLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.bSupLink);
            this.Controls.Add(this.bAddLink);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormLabel";
            this.Text = "FormTag";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bSup;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.TreeView tvLabelClass;
        private System.Windows.Forms.Button bAddLink;
        private System.Windows.Forms.Button bSupLink;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button bAnnuler;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.DataGridView dgvLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn GuidLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomLabel;
    }
}