namespace DrawTools
{
    partial class FormLabelRole
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
            this.cbExposition = new System.Windows.Forms.ComboBox();
            this.bAdd = new System.Windows.Forms.Button();
            this.tbPrefix = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbApp = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbOS = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbSoftware = new System.Windows.Forms.ComboBox();
            this.bAnnuler = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbExposition
            // 
            this.cbExposition.FormattingEnabled = true;
            this.cbExposition.Items.AddRange(new object[] {
            "PREZI",
            "RESTRICTED",
            "INTRANET",
            "EXPO-NA"});
            this.cbExposition.Location = new System.Drawing.Point(247, 29);
            this.cbExposition.Name = "cbExposition";
            this.cbExposition.Size = new System.Drawing.Size(205, 24);
            this.cbExposition.TabIndex = 0;
            // 
            // bAdd
            // 
            this.bAdd.Enabled = false;
            this.bAdd.Location = new System.Drawing.Point(247, 182);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(75, 23);
            this.bAdd.TabIndex = 1;
            this.bAdd.Text = "Add";
            this.bAdd.UseVisualStyleBackColor = true;
            // 
            // tbPrefix
            // 
            this.tbPrefix.Location = new System.Drawing.Point(12, 29);
            this.tbPrefix.Name = "tbPrefix";
            this.tbPrefix.ReadOnly = true;
            this.tbPrefix.Size = new System.Drawing.Size(43, 22);
            this.tbPrefix.TabIndex = 2;
            this.tbPrefix.Text = "R";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Prefix";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Exposition";
            // 
            // cbApp
            // 
            this.cbApp.FormattingEnabled = true;
            this.cbApp.Items.AddRange(new object[] {
            "P",
            "A",
            "D",
            "PA",
            "AD",
            "PAD",
            "USER",
            "NO-TIER",
            "TIER-NA"});
            this.cbApp.Location = new System.Drawing.Point(12, 103);
            this.cbApp.Name = "cbApp";
            this.cbApp.Size = new System.Drawing.Size(205, 24);
            this.cbApp.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Applicatif Tiers";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(247, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Operating System";
            // 
            // cbOS
            // 
            this.cbOS.FormattingEnabled = true;
            this.cbOS.Items.AddRange(new object[] {
            "WIN",
            "RHEL",
            "AIX",
            "VIP",
            "NAS",
            "KUBE",
            "PAAS",
            "OTHER-OS"});
            this.cbOS.Location = new System.Drawing.Point(247, 103);
            this.cbOS.Name = "cbOS";
            this.cbOS.Size = new System.Drawing.Size(205, 24);
            this.cbOS.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Software";
            // 
            // cbSoftware
            // 
            this.cbSoftware.FormattingEnabled = true;
            this.cbSoftware.Items.AddRange(new object[] {
            "WEB",
            "WAS-NL",
            "SQL-SERVER",
            "ORACLE",
            "BIG-DATA",
            "COS",
            "K8S",
            "HOST-CONTAINER",
            "OTHER-SOFT",
            "SOFT-NA"});
            this.cbSoftware.Location = new System.Drawing.Point(12, 181);
            this.cbSoftware.Name = "cbSoftware";
            this.cbSoftware.Size = new System.Drawing.Size(205, 24);
            this.cbSoftware.TabIndex = 9;
            // 
            // bAnnuler
            // 
            this.bAnnuler.Location = new System.Drawing.Point(377, 182);
            this.bAnnuler.Name = "bAnnuler";
            this.bAnnuler.Size = new System.Drawing.Size(75, 23);
            this.bAnnuler.TabIndex = 11;
            this.bAnnuler.Text = "Annuler";
            this.bAnnuler.UseVisualStyleBackColor = true;
            // 
            // FormLabelRole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 221);
            this.Controls.Add(this.bAnnuler);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbSoftware);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbOS);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbApp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPrefix);
            this.Controls.Add(this.bAdd);
            this.Controls.Add(this.cbExposition);
            this.Name = "FormLabelRole";
            this.Text = "FormLabelRole";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbExposition;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.TextBox tbPrefix;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbApp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbOS;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbSoftware;
        private System.Windows.Forms.Button bAnnuler;
    }
}