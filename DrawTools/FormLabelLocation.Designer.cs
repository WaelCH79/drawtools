namespace DrawTools
{
    partial class FormLabelLocation
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
            this.bAnnuler = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbInfra = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbVendor = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbCloudHosting = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPrefix = new System.Windows.Forms.TextBox();
            this.bAdd = new System.Windows.Forms.Button();
            this.cbLocation = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // bAnnuler
            // 
            this.bAnnuler.Location = new System.Drawing.Point(377, 184);
            this.bAnnuler.Name = "bAnnuler";
            this.bAnnuler.Size = new System.Drawing.Size(75, 23);
            this.bAnnuler.TabIndex = 23;
            this.bAnnuler.Text = "Annuler";
            this.bAnnuler.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 17);
            this.label5.TabIndex = 22;
            this.label5.Text = "Infra";
            // 
            // cbInfra
            // 
            this.cbInfra.FormattingEnabled = true;
            this.cbInfra.Items.AddRange(new object[] {
            "CORE-BIZ",
            "DMZR-VPC-BIZ",
            "BPLS-BIZ",
            "BNL-BIZ",
            "FORTIS-BIZ",
            "INFRA-NA",
            "CYB-BIZ"});
            this.cbInfra.Location = new System.Drawing.Point(12, 183);
            this.cbInfra.Name = "cbInfra";
            this.cbInfra.Size = new System.Drawing.Size(205, 24);
            this.cbInfra.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(247, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 17);
            this.label4.TabIndex = 20;
            this.label4.Text = "Vendor";
            // 
            // cbVendor
            // 
            this.cbVendor.FormattingEnabled = true;
            this.cbVendor.Items.AddRange(new object[] {
            "BNPP",
            "IBM",
            "AWS",
            "AZURE",
            "GOOGLE",
            "OTHER-VENDOR",
            "VE?DOR-NA"});
            this.cbVendor.Location = new System.Drawing.Point(247, 105);
            this.cbVendor.Name = "cbVendor";
            this.cbVendor.Size = new System.Drawing.Size(205, 24);
            this.cbVendor.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 17);
            this.label3.TabIndex = 18;
            this.label3.Text = "Cloud Hosting";
            // 
            // cbCloudHosting
            // 
            this.cbCloudHosting.FormattingEnabled = true;
            this.cbCloudHosting.Items.AddRange(new object[] {
            "T1",
            "T2",
            "CLOUD-NA"});
            this.cbCloudHosting.Location = new System.Drawing.Point(12, 105);
            this.cbCloudHosting.Name = "cbCloudHosting";
            this.cbCloudHosting.Size = new System.Drawing.Size(205, 24);
            this.cbCloudHosting.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "Location";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Prefix";
            // 
            // tbPrefix
            // 
            this.tbPrefix.Location = new System.Drawing.Point(12, 31);
            this.tbPrefix.Name = "tbPrefix";
            this.tbPrefix.ReadOnly = true;
            this.tbPrefix.Size = new System.Drawing.Size(43, 22);
            this.tbPrefix.TabIndex = 14;
            this.tbPrefix.Text = "L";
            // 
            // bAdd
            // 
            this.bAdd.Enabled = false;
            this.bAdd.Location = new System.Drawing.Point(247, 184);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(75, 23);
            this.bAdd.TabIndex = 13;
            this.bAdd.Text = "Add";
            this.bAdd.UseVisualStyleBackColor = true;
            // 
            // cbLocation
            // 
            this.cbLocation.FormattingEnabled = true;
            this.cbLocation.Items.AddRange(new object[] {
            "EMEA",
            "LOC-NA"});
            this.cbLocation.Location = new System.Drawing.Point(247, 31);
            this.cbLocation.Name = "cbLocation";
            this.cbLocation.Size = new System.Drawing.Size(205, 24);
            this.cbLocation.TabIndex = 12;
            // 
            // FormLabelLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 224);
            this.Controls.Add(this.bAnnuler);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbInfra);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbVendor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbCloudHosting);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPrefix);
            this.Controls.Add(this.bAdd);
            this.Controls.Add(this.cbLocation);
            this.Name = "FormLabelLocation";
            this.Text = "FormLabelLocation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bAnnuler;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbInfra;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbVendor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbCloudHosting;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPrefix;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.ComboBox cbLocation;
    }
}