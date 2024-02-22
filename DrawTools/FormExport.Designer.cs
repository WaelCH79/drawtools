namespace DrawTools
{
    partial class FormExport
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
            this.cbGuidApplication = new System.Windows.Forms.ComboBox();
            this.cbApplication = new System.Windows.Forms.ComboBox();
            this.bAnnuler = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            this.cbVersion = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbVersion);
            this.groupBox1.Controls.Add(this.cbGuidApplication);
            this.groupBox1.Controls.Add(this.cbApplication);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 81);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Application";
            // 
            // cbGuidApplication
            // 
            this.cbGuidApplication.FormattingEnabled = true;
            this.cbGuidApplication.Location = new System.Drawing.Point(95, 10);
            this.cbGuidApplication.Name = "cbGuidApplication";
            this.cbGuidApplication.Size = new System.Drawing.Size(142, 21);
            this.cbGuidApplication.TabIndex = 1;
            this.cbGuidApplication.Visible = false;
            // 
            // cbApplication
            // 
            this.cbApplication.FormattingEnabled = true;
            this.cbApplication.Location = new System.Drawing.Point(6, 19);
            this.cbApplication.Name = "cbApplication";
            this.cbApplication.Size = new System.Drawing.Size(256, 21);
            this.cbApplication.TabIndex = 0;
            this.cbApplication.SelectedIndexChanged += new System.EventHandler(this.cbApplication_SelectedIndexChanged);
            // 
            // bAnnuler
            // 
            this.bAnnuler.Location = new System.Drawing.Point(12, 98);
            this.bAnnuler.Name = "bAnnuler";
            this.bAnnuler.Size = new System.Drawing.Size(63, 23);
            this.bAnnuler.TabIndex = 1;
            this.bAnnuler.Text = "Annuler";
            this.bAnnuler.UseVisualStyleBackColor = true;
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(81, 98);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(63, 23);
            this.bOK.TabIndex = 2;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);

            // 
            // cbVersion
            // 
            this.cbVersion.FormattingEnabled = true;
            this.cbVersion.Location = new System.Drawing.Point(6, 46);
            this.cbVersion.Name = "cbVersion";
            this.cbVersion.Size = new System.Drawing.Size(256, 21);
            this.cbVersion.TabIndex = 2;
            // 
            // FormExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 131);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.bAnnuler);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormExport";
            this.Text = "FormExport";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbApplication;
        private System.Windows.Forms.Button bAnnuler;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.ComboBox cbGuidApplication;
        private System.Windows.Forms.ComboBox cbVersion;
    }
}