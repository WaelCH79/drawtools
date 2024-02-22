namespace DrawTools
{
    partial class FormLabelEnv
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
            this.bAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbLabel = new System.Windows.Forms.TextBox();
            this.cbEnv = new System.Windows.Forms.ComboBox();
            this.bAnnuler = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bAdd
            // 
            this.bAdd.Enabled = false;
            this.bAdd.Location = new System.Drawing.Point(15, 74);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(75, 23);
            this.bAdd.TabIndex = 0;
            this.bAdd.Text = "Add";
            this.bAdd.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Prefix";
            // 
            // tbLabel
            // 
            this.tbLabel.Enabled = false;
            this.tbLabel.Location = new System.Drawing.Point(15, 29);
            this.tbLabel.Name = "tbLabel";
            this.tbLabel.Size = new System.Drawing.Size(132, 22);
            this.tbLabel.TabIndex = 2;
            // 
            // cbEnv
            // 
            this.cbEnv.FormattingEnabled = true;
            this.cbEnv.Items.AddRange(new object[] {
            "PROD",
            "PPROD",
            "QUAL",
            "INT",
            "DEV",
            "TEST",
            "ENV-NA"});
            this.cbEnv.Location = new System.Drawing.Point(174, 29);
            this.cbEnv.Name = "cbEnv";
            this.cbEnv.Size = new System.Drawing.Size(278, 24);
            this.cbEnv.TabIndex = 3;
            // 
            // bAnnuler
            // 
            this.bAnnuler.Location = new System.Drawing.Point(129, 74);
            this.bAnnuler.Name = "bAnnuler";
            this.bAnnuler.Size = new System.Drawing.Size(75, 23);
            this.bAnnuler.TabIndex = 4;
            this.bAnnuler.Text = "Annuler";
            this.bAnnuler.UseVisualStyleBackColor = true;
            // 
            // FormLabelEnv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 133);
            this.Controls.Add(this.bAnnuler);
            this.Controls.Add(this.cbEnv);
            this.Controls.Add(this.tbLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bAdd);
            this.Name = "FormLabelEnv";
            this.Text = "FormLabelEnv";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbLabel;
        private System.Windows.Forms.ComboBox cbEnv;
        private System.Windows.Forms.Button bAnnuler;
    }
}