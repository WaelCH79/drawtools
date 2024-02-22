namespace DrawTools
{
    partial class FormLabelApp
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
            this.label2 = new System.Windows.Forms.Label();
            this.bAdd = new System.Windows.Forms.Button();
            this.cbApplication = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbCodeAP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTrigramme = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbIteration = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbPrefix = new System.Windows.Forms.TextBox();
            this.cbGuidApplication = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // bAnnuler
            // 
            this.bAnnuler.Location = new System.Drawing.Point(193, 137);
            this.bAnnuler.Name = "bAnnuler";
            this.bAnnuler.Size = new System.Drawing.Size(75, 23);
            this.bAnnuler.TabIndex = 27;
            this.bAnnuler.Text = "Annuler";
            this.bAnnuler.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 17);
            this.label2.TabIndex = 26;
            this.label2.Text = "Applcation";
            // 
            // bAdd
            // 
            this.bAdd.Enabled = false;
            this.bAdd.Location = new System.Drawing.Point(63, 137);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(75, 23);
            this.bAdd.TabIndex = 25;
            this.bAdd.Text = "Add";
            this.bAdd.UseVisualStyleBackColor = true;
            // 
            // cbApplication
            // 
            this.cbApplication.FormattingEnabled = true;
            this.cbApplication.Items.AddRange(new object[] {
            "EMEA",
            "LOC-NA"});
            this.cbApplication.Location = new System.Drawing.Point(12, 32);
            this.cbApplication.Name = "cbApplication";
            this.cbApplication.Size = new System.Drawing.Size(353, 24);
            this.cbApplication.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 17);
            this.label1.TabIndex = 29;
            this.label1.Text = "Code AP";
            // 
            // tbCodeAP
            // 
            this.tbCodeAP.Location = new System.Drawing.Point(61, 100);
            this.tbCodeAP.Name = "tbCodeAP";
            this.tbCodeAP.ReadOnly = true;
            this.tbCodeAP.Size = new System.Drawing.Size(89, 22);
            this.tbCodeAP.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(156, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 17);
            this.label3.TabIndex = 31;
            this.label3.Text = "Trigramme";
            // 
            // tbTrigramme
            // 
            this.tbTrigramme.Location = new System.Drawing.Point(156, 100);
            this.tbTrigramme.Name = "tbTrigramme";
            this.tbTrigramme.ReadOnly = true;
            this.tbTrigramme.Size = new System.Drawing.Size(67, 22);
            this.tbTrigramme.TabIndex = 30;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(229, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 33;
            this.label4.Text = "Iteration";
            // 
            // tbIteration
            // 
            this.tbIteration.Location = new System.Drawing.Point(229, 100);
            this.tbIteration.Name = "tbIteration";
            this.tbIteration.Size = new System.Drawing.Size(136, 22);
            this.tbIteration.TabIndex = 32;
            this.tbIteration.Text = "0-DEFAULT";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 17);
            this.label5.TabIndex = 35;
            this.label5.Text = "Prefix";
            // 
            // tbPrefix
            // 
            this.tbPrefix.Location = new System.Drawing.Point(12, 100);
            this.tbPrefix.Name = "tbPrefix";
            this.tbPrefix.ReadOnly = true;
            this.tbPrefix.Size = new System.Drawing.Size(43, 22);
            this.tbPrefix.TabIndex = 34;
            this.tbPrefix.Text = "A";
            // 
            // cbGuidApplication
            // 
            this.cbGuidApplication.FormattingEnabled = true;
            this.cbGuidApplication.Items.AddRange(new object[] {
            "EMEA",
            "LOC-NA"});
            this.cbGuidApplication.Location = new System.Drawing.Point(111, 7);
            this.cbGuidApplication.Name = "cbGuidApplication";
            this.cbGuidApplication.Size = new System.Drawing.Size(85, 24);
            this.cbGuidApplication.TabIndex = 36;
            this.cbGuidApplication.Visible = false;
            // 
            // FormLabelApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 174);
            this.Controls.Add(this.cbGuidApplication);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbPrefix);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbIteration);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbTrigramme);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbCodeAP);
            this.Controls.Add(this.bAnnuler);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bAdd);
            this.Controls.Add(this.cbApplication);
            this.Name = "FormLabelApp";
            this.Text = "FormLabelApp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bAnnuler;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.ComboBox cbApplication;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbCodeAP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbTrigramme;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbIteration;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbPrefix;
        private System.Windows.Forms.ComboBox cbGuidApplication;
    }
}