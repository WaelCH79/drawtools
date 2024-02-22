namespace DrawTools
{
    partial class FormRules
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbDomaine = new System.Windows.Forms.ComboBox();
            this.dgRules = new System.Windows.Forms.DataGridView();
            this.GuidRule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomRule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bDelRule = new System.Windows.Forms.Button();
            this.bAddRule = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgPremisse = new System.Windows.Forms.DataGridView();
            this.GuidPremisse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Premisse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Objet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Eval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operateur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valeur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Connecteur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bDelPremisse = new System.Windows.Forms.Button();
            this.bAddPremisse = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.GuidConclusion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Conclusion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bDelConclusion = new System.Windows.Forms.Button();
            this.bAddConclusion = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRules)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPremisse)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cbDomaine);
            this.groupBox2.Controls.Add(this.dgRules);
            this.groupBox2.Controls.Add(this.bDelRule);
            this.groupBox2.Controls.Add(this.bAddRule);
            this.groupBox2.Location = new System.Drawing.Point(12, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(268, 387);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rule";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Domaine";
            // 
            // cbDomaine
            // 
            this.cbDomaine.FormattingEnabled = true;
            this.cbDomaine.Items.AddRange(new object[] {
            "Base de données"});
            this.cbDomaine.Location = new System.Drawing.Point(86, 19);
            this.cbDomaine.Name = "cbDomaine";
            this.cbDomaine.Size = new System.Drawing.Size(174, 21);
            this.cbDomaine.TabIndex = 4;
            // 
            // dgRules
            // 
            this.dgRules.AllowUserToAddRows = false;
            this.dgRules.AllowUserToDeleteRows = false;
            this.dgRules.AllowUserToResizeColumns = false;
            this.dgRules.AllowUserToResizeRows = false;
            this.dgRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgRules.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GuidRule,
            this.NomRule});
            this.dgRules.Location = new System.Drawing.Point(31, 44);
            this.dgRules.MultiSelect = false;
            this.dgRules.Name = "dgRules";
            this.dgRules.ReadOnly = true;
            this.dgRules.RowHeadersVisible = false;
            this.dgRules.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgRules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgRules.Size = new System.Drawing.Size(229, 337);
            this.dgRules.TabIndex = 3;
            // 
            // GuidRule
            // 
            this.GuidRule.HeaderText = "Guid";
            this.GuidRule.Name = "GuidRule";
            this.GuidRule.ReadOnly = true;
            this.GuidRule.Width = 50;
            // 
            // NomRule
            // 
            this.NomRule.HeaderText = "Regle";
            this.NomRule.Name = "NomRule";
            this.NomRule.ReadOnly = true;
            // 
            // bDelRule
            // 
            this.bDelRule.Location = new System.Drawing.Point(6, 44);
            this.bDelRule.Name = "bDelRule";
            this.bDelRule.Size = new System.Drawing.Size(22, 23);
            this.bDelRule.TabIndex = 2;
            this.bDelRule.Text = "-";
            this.bDelRule.UseVisualStyleBackColor = true;
            this.bDelRule.Click += new System.EventHandler(this.bDelRule_Click);
            // 
            // bAddRule
            // 
            this.bAddRule.Location = new System.Drawing.Point(6, 19);
            this.bAddRule.Name = "bAddRule";
            this.bAddRule.Size = new System.Drawing.Size(22, 23);
            this.bAddRule.TabIndex = 1;
            this.bAddRule.Text = "+";
            this.bAddRule.UseVisualStyleBackColor = true;
            this.bAddRule.Click += new System.EventHandler(this.bAddRule_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgPremisse);
            this.groupBox1.Controls.Add(this.bDelPremisse);
            this.groupBox1.Controls.Add(this.bAddPremisse);
            this.groupBox1.Location = new System.Drawing.Point(286, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(677, 190);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Premisse";
            // 
            // dgPremisse
            // 
            this.dgPremisse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPremisse.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GuidPremisse,
            this.Premisse,
            this.Objet,
            this.Eval,
            this.Operateur,
            this.Valeur,
            this.Connecteur});
            this.dgPremisse.Location = new System.Drawing.Point(34, 20);
            this.dgPremisse.Name = "dgPremisse";
            this.dgPremisse.Size = new System.Drawing.Size(637, 163);
            this.dgPremisse.TabIndex = 6;
            // 
            // GuidPremisse
            // 
            this.GuidPremisse.HeaderText = "Guid";
            this.GuidPremisse.Name = "GuidPremisse";
            // 
            // Premisse
            // 
            this.Premisse.HeaderText = "Premisse";
            this.Premisse.Name = "Premisse";
            // 
            // Objet
            // 
            this.Objet.HeaderText = "Objet";
            this.Objet.Name = "Objet";
            this.Objet.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Eval
            // 
            this.Eval.HeaderText = "Eval";
            this.Eval.Name = "Eval";
            this.Eval.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Eval.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Operateur
            // 
            this.Operateur.HeaderText = "Op";
            this.Operateur.Name = "Operateur";
            this.Operateur.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Operateur.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Operateur.Width = 40;
            // 
            // Valeur
            // 
            this.Valeur.HeaderText = "Valeur";
            this.Valeur.Name = "Valeur";
            // 
            // Connecteur
            // 
            this.Connecteur.HeaderText = "Cnct";
            this.Connecteur.Name = "Connecteur";
            this.Connecteur.Width = 40;
            // 
            // bDelPremisse
            // 
            this.bDelPremisse.Location = new System.Drawing.Point(6, 45);
            this.bDelPremisse.Name = "bDelPremisse";
            this.bDelPremisse.Size = new System.Drawing.Size(22, 23);
            this.bDelPremisse.TabIndex = 4;
            this.bDelPremisse.Text = "-";
            this.bDelPremisse.UseVisualStyleBackColor = true;
            this.bDelPremisse.Click += new System.EventHandler(this.bDelPremisse_Click);
            // 
            // bAddPremisse
            // 
            this.bAddPremisse.Location = new System.Drawing.Point(6, 20);
            this.bAddPremisse.Name = "bAddPremisse";
            this.bAddPremisse.Size = new System.Drawing.Size(22, 23);
            this.bAddPremisse.TabIndex = 3;
            this.bAddPremisse.Text = "+";
            this.bAddPremisse.UseVisualStyleBackColor = true;
            this.bAddPremisse.Click += new System.EventHandler(this.bAddPremisse_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView3);
            this.groupBox3.Controls.Add(this.bDelConclusion);
            this.groupBox3.Controls.Add(this.bAddConclusion);
            this.groupBox3.Location = new System.Drawing.Point(286, 207);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(677, 190);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Conclusion";
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GuidConclusion,
            this.Conclusion});
            this.dataGridView3.Location = new System.Drawing.Point(34, 21);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(637, 163);
            this.dataGridView3.TabIndex = 7;
            // 
            // GuidConclusion
            // 
            this.GuidConclusion.HeaderText = "Guid";
            this.GuidConclusion.Name = "GuidConclusion";
            // 
            // Conclusion
            // 
            this.Conclusion.HeaderText = "Conclusion";
            this.Conclusion.Name = "Conclusion";
            // 
            // bDelConclusion
            // 
            this.bDelConclusion.Location = new System.Drawing.Point(6, 45);
            this.bDelConclusion.Name = "bDelConclusion";
            this.bDelConclusion.Size = new System.Drawing.Size(22, 23);
            this.bDelConclusion.TabIndex = 6;
            this.bDelConclusion.Text = "-";
            this.bDelConclusion.UseVisualStyleBackColor = true;
            // 
            // bAddConclusion
            // 
            this.bAddConclusion.Location = new System.Drawing.Point(6, 20);
            this.bAddConclusion.Name = "bAddConclusion";
            this.bAddConclusion.Size = new System.Drawing.Size(22, 23);
            this.bAddConclusion.TabIndex = 5;
            this.bAddConclusion.Text = "+";
            this.bAddConclusion.UseVisualStyleBackColor = true;
            // 
            // FormRules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 405);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "FormRules";
            this.Text = "FormRules";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRules)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPremisse)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button bDelRule;
        private System.Windows.Forms.Button bAddRule;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbDomaine;
        private System.Windows.Forms.DataGridView dgRules;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bDelPremisse;
        private System.Windows.Forms.Button bAddPremisse;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button bDelConclusion;
        private System.Windows.Forms.Button bAddConclusion;
        private System.Windows.Forms.DataGridViewTextBoxColumn GuidRule;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomRule;
        private System.Windows.Forms.DataGridView dgPremisse;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridViewTextBoxColumn GuidConclusion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Conclusion;
        private System.Windows.Forms.DataGridViewTextBoxColumn GuidPremisse;
        private System.Windows.Forms.DataGridViewTextBoxColumn Premisse;
        private System.Windows.Forms.DataGridViewTextBoxColumn Objet;
        private System.Windows.Forms.DataGridViewTextBoxColumn Eval;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operateur;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valeur;
        private System.Windows.Forms.DataGridViewTextBoxColumn Connecteur;
    }
}