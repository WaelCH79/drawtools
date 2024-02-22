namespace DrawTools
{
    partial class FormDeployComposant
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
            this.tvPackage = new System.Windows.Forms.TreeView();
            this.dgvPackage = new System.Windows.Forms.DataGridView();
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.lLinkApp = new System.Windows.Forms.ListBox();
            this.bAdd = new System.Windows.Forms.Button();
            this.bSup = new System.Windows.Forms.Button();
            this.lLinkAppAdd = new System.Windows.Forms.ListBox();
            this.ExpObjPropriete = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valeur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpObjPls = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ExpObjNomVisible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPackage)).BeginInit();
            this.SuspendLayout();
            // 
            // tvPackage
            // 
            this.tvPackage.Location = new System.Drawing.Point(12, 156);
            this.tvPackage.Name = "tvPackage";
            this.tvPackage.Size = new System.Drawing.Size(253, 271);
            this.tvPackage.TabIndex = 0;
            // 
            // dgvPackage
            // 
            this.dgvPackage.AllowUserToAddRows = false;
            this.dgvPackage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPackage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ExpObjPropriete,
            this.Valeur,
            this.ExpObjPls,
            this.ExpObjNomVisible});
            this.dgvPackage.Location = new System.Drawing.Point(271, 156);
            this.dgvPackage.Name = "dgvPackage";
            this.dgvPackage.RowHeadersVisible = false;
            this.dgvPackage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvPackage.Size = new System.Drawing.Size(345, 242);
            this.dgvPackage.TabIndex = 1;
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(323, 404);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 2;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(500, 404);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 3;
            this.bCancel.Text = "Annuler";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // lLinkApp
            // 
            this.lLinkApp.FormattingEnabled = true;
            this.lLinkApp.Location = new System.Drawing.Point(12, 12);
            this.lLinkApp.Name = "lLinkApp";
            this.lLinkApp.Size = new System.Drawing.Size(253, 134);
            this.lLinkApp.TabIndex = 4;
            // 
            // bAdd
            // 
            this.bAdd.Location = new System.Drawing.Point(271, 39);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(75, 23);
            this.bAdd.TabIndex = 5;
            this.bAdd.Text = ">>";
            this.bAdd.UseVisualStyleBackColor = true;
            // 
            // bSup
            // 
            this.bSup.Location = new System.Drawing.Point(271, 68);
            this.bSup.Name = "bSup";
            this.bSup.Size = new System.Drawing.Size(75, 23);
            this.bSup.TabIndex = 6;
            this.bSup.Text = "<<";
            this.bSup.UseVisualStyleBackColor = true;
            // 
            // lLinkAppAdd
            // 
            this.lLinkAppAdd.FormattingEnabled = true;
            this.lLinkAppAdd.Location = new System.Drawing.Point(352, 12);
            this.lLinkAppAdd.Name = "lLinkAppAdd";
            this.lLinkAppAdd.Size = new System.Drawing.Size(264, 134);
            this.lLinkAppAdd.TabIndex = 7;
            // 
            // ExpObjPropriete
            // 
            this.ExpObjPropriete.HeaderText = "Propriete";
            this.ExpObjPropriete.Name = "ExpObjPropriete";
            this.ExpObjPropriete.Width = 140;
            // 
            // Valeur
            // 
            this.Valeur.HeaderText = "ExpObjValeur";
            this.Valeur.Name = "Valeur";
            this.Valeur.Width = 160;
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
            // FormDeployComposant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 438);
            this.Controls.Add(this.lLinkAppAdd);
            this.Controls.Add(this.bSup);
            this.Controls.Add(this.bAdd);
            this.Controls.Add(this.lLinkApp);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.dgvPackage);
            this.Controls.Add(this.tvPackage);
            this.Name = "FormDeployComposant";
            this.Text = "DeployComposant";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPackage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvPackage;
        private System.Windows.Forms.DataGridView dgvPackage;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.ListBox lLinkApp;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.Button bSup;
        private System.Windows.Forms.ListBox lLinkAppAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpObjPropriete;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valeur;
        private System.Windows.Forms.DataGridViewButtonColumn ExpObjPls;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpObjNomVisible;
    }
}