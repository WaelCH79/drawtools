namespace DrawTools
{
    partial class FormCnxBase
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
            this.lbBase = new System.Windows.Forms.ListBox();
            this.bSelect = new System.Windows.Forms.Button();
            this.bQuitter = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbBase);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 189);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bases disponibles";
            // 
            // lbBase
            // 
            this.lbBase.FormattingEnabled = true;
            this.lbBase.Location = new System.Drawing.Point(6, 19);
            this.lbBase.Name = "lbBase";
            this.lbBase.Size = new System.Drawing.Size(248, 160);
            this.lbBase.TabIndex = 1;
            // 
            // bSelect
            // 
            this.bSelect.Location = new System.Drawing.Point(12, 207);
            this.bSelect.Name = "bSelect";
            this.bSelect.Size = new System.Drawing.Size(75, 23);
            this.bSelect.TabIndex = 1;
            this.bSelect.Text = "Sélectionner";
            this.bSelect.UseVisualStyleBackColor = true;
            this.bSelect.Click += new System.EventHandler(this.bSelect_Click);
            // 
            // bQuitter
            // 
            this.bQuitter.Location = new System.Drawing.Point(93, 207);
            this.bQuitter.Name = "bQuitter";
            this.bQuitter.Size = new System.Drawing.Size(75, 23);
            this.bQuitter.TabIndex = 2;
            this.bQuitter.Text = "Quitter";
            this.bQuitter.UseVisualStyleBackColor = true;
            this.bQuitter.Click += new System.EventHandler(this.bQuitter_Click);
            // 
            // FormCnxBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 243);
            this.Controls.Add(this.bQuitter);
            this.Controls.Add(this.bSelect);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormCnxBase";
            this.Text = "FormCnxBase";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lbBase;
        private System.Windows.Forms.Button bSelect;
        private System.Windows.Forms.Button bQuitter;
    }
}