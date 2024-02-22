namespace DrawTools
{
    partial class FormVarenv
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvVarenv = new System.Windows.Forms.DataGridView();
            this.bOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPod = new System.Windows.Forms.TextBox();
            this.bCancel = new System.Windows.Forms.Button();
            this.tbGuidPod = new System.Windows.Forms.TextBox();
            this.tbGuidContainer = new System.Windows.Forms.TextBox();
            this.Guid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GuidGenpod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GuidContainer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valeur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVarenv)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvVarenv);
            this.panel1.Location = new System.Drawing.Point(15, 75);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(748, 223);
            this.panel1.TabIndex = 0;
            // 
            // dgvVarenv
            // 
            this.dgvVarenv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvVarenv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Guid,
            this.GuidGenpod,
            this.GuidContainer,
            this.Nom,
            this.Valeur});
            this.dgvVarenv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVarenv.Location = new System.Drawing.Point(0, 0);
            this.dgvVarenv.Name = "dgvVarenv";
            this.dgvVarenv.RowHeadersWidth = 24;
            this.dgvVarenv.RowTemplate.Height = 24;
            this.dgvVarenv.Size = new System.Drawing.Size(748, 223);
            this.dgvVarenv.TabIndex = 0;
            // 
            // bOk
            // 
            this.bOk.Location = new System.Drawing.Point(52, 304);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(75, 23);
            this.bOk.TabIndex = 1;
            this.bOk.Text = "Ok";
            this.bOk.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nom Pod";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(-187, -147);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(123, 47);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(280, 22);
            this.textBox2.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nom Container";
            // 
            // tbPod
            // 
            this.tbPod.Location = new System.Drawing.Point(123, 20);
            this.tbPod.Name = "tbPod";
            this.tbPod.ReadOnly = true;
            this.tbPod.Size = new System.Drawing.Size(280, 22);
            this.tbPod.TabIndex = 6;
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(146, 304);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 7;
            this.bCancel.Text = "Annuler";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // tbGuidPod
            // 
            this.tbGuidPod.Location = new System.Drawing.Point(409, 20);
            this.tbGuidPod.Name = "tbGuidPod";
            this.tbGuidPod.ReadOnly = true;
            this.tbGuidPod.Size = new System.Drawing.Size(280, 22);
            this.tbGuidPod.TabIndex = 8;
            // 
            // tbGuidContainer
            // 
            this.tbGuidContainer.Location = new System.Drawing.Point(409, 47);
            this.tbGuidContainer.Name = "tbGuidContainer";
            this.tbGuidContainer.ReadOnly = true;
            this.tbGuidContainer.Size = new System.Drawing.Size(280, 22);
            this.tbGuidContainer.TabIndex = 9;
            // 
            // Guid
            // 
            this.Guid.HeaderText = "GuidVarenv";
            this.Guid.Name = "Guid";
            this.Guid.ToolTipText = "GuidVarenv";
            this.Guid.Visible = false;
            // 
            // GuidGenpod
            // 
            this.GuidGenpod.HeaderText = "GuidGenpod";
            this.GuidGenpod.Name = "GuidGenpod";
            this.GuidGenpod.ToolTipText = "GuidGenpod";
            this.GuidGenpod.Visible = false;
            // 
            // GuidContainer
            // 
            this.GuidContainer.HeaderText = "GuidContainer";
            this.GuidContainer.Name = "GuidContainer";
            this.GuidContainer.ToolTipText = "GuidContainer";
            this.GuidContainer.Visible = false;
            // 
            // Nom
            // 
            this.Nom.HeaderText = "Nom";
            this.Nom.Name = "Nom";
            this.Nom.ToolTipText = "NomVarenv";
            this.Nom.Width = 180;
            // 
            // Valeur
            // 
            this.Valeur.HeaderText = "Valeur";
            this.Valeur.Name = "Valeur";
            this.Valeur.ToolTipText = "ValeurVarenv";
            this.Valeur.Width = 380;
            // 
            // FormVarenv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 339);
            this.Controls.Add(this.tbGuidContainer);
            this.Controls.Add(this.tbGuidPod);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.tbPod);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.panel1);
            this.Name = "FormVarenv";
            this.Text = "FormVarenv";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVarenv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvVarenv;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPod;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.TextBox tbGuidPod;
        private System.Windows.Forms.TextBox tbGuidContainer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Guid;
        private System.Windows.Forms.DataGridViewTextBoxColumn GuidGenpod;
        private System.Windows.Forms.DataGridViewTextBoxColumn GuidContainer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nom;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valeur;
    }
}