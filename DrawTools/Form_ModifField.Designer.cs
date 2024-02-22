namespace DrawTools
{
    partial class Form_ModifField
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
            this.bExport = new System.Windows.Forms.Button();
            this.tbTable = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Guid = new System.Windows.Forms.Label();
            this.tbGuid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbField = new System.Windows.Forms.TextBox();
            this.bImport = new System.Windows.Forms.Button();
            this.bAnnuler = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bExport
            // 
            this.bExport.Location = new System.Drawing.Point(16, 96);
            this.bExport.Name = "bExport";
            this.bExport.Size = new System.Drawing.Size(75, 23);
            this.bExport.TabIndex = 0;
            this.bExport.Text = "Export";
            this.bExport.UseVisualStyleBackColor = true;
            // 
            // tbTable
            // 
            this.tbTable.Location = new System.Drawing.Point(115, 10);
            this.tbTable.Name = "tbTable";
            this.tbTable.Size = new System.Drawing.Size(138, 20);
            this.tbTable.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Table";
            // 
            // Guid
            // 
            this.Guid.AutoSize = true;
            this.Guid.Location = new System.Drawing.Point(13, 39);
            this.Guid.Name = "Guid";
            this.Guid.Size = new System.Drawing.Size(29, 13);
            this.Guid.TabIndex = 4;
            this.Guid.Text = "Guid";
            // 
            // tbGuid
            // 
            this.tbGuid.Location = new System.Drawing.Point(115, 36);
            this.tbGuid.Name = "tbGuid";
            this.tbGuid.Size = new System.Drawing.Size(138, 20);
            this.tbGuid.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Champ à modifier";
            // 
            // tbField
            // 
            this.tbField.Location = new System.Drawing.Point(115, 62);
            this.tbField.Name = "tbField";
            this.tbField.Size = new System.Drawing.Size(138, 20);
            this.tbField.TabIndex = 5;
            // 
            // bImport
            // 
            this.bImport.Location = new System.Drawing.Point(97, 96);
            this.bImport.Name = "bImport";
            this.bImport.Size = new System.Drawing.Size(75, 23);
            this.bImport.TabIndex = 7;
            this.bImport.Text = "Import";
            this.bImport.UseVisualStyleBackColor = true;
            // 
            // bAnnuler
            // 
            this.bAnnuler.Location = new System.Drawing.Point(178, 96);
            this.bAnnuler.Name = "bAnnuler";
            this.bAnnuler.Size = new System.Drawing.Size(75, 23);
            this.bAnnuler.TabIndex = 8;
            this.bAnnuler.Text = "Annuler";
            this.bAnnuler.UseVisualStyleBackColor = true;
            // 
            // Form_ModifField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 132);
            this.Controls.Add(this.bAnnuler);
            this.Controls.Add(this.bImport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbField);
            this.Controls.Add(this.Guid);
            this.Controls.Add(this.tbGuid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbTable);
            this.Controls.Add(this.bExport);
            this.Name = "Form_ModifField";
            this.Text = "Form_ModifField";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bExport;
        private System.Windows.Forms.TextBox tbTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Guid;
        private System.Windows.Forms.TextBox tbGuid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbField;
        private System.Windows.Forms.Button bImport;
        private System.Windows.Forms.Button bAnnuler;
    }
}