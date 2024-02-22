namespace DrawTools
{
    partial class FormVisu
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
            this.tvVues = new System.Windows.Forms.TreeView();
            this.bLoadXml = new System.Windows.Forms.Button();
            this.tbXml = new System.Windows.Forms.TextBox();
            this.bLoadVue = new System.Windows.Forms.Button();
            this.bClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tvVues
            // 
            this.tvVues.Location = new System.Drawing.Point(12, 41);
            this.tvVues.Name = "tvVues";
            this.tvVues.Size = new System.Drawing.Size(228, 239);
            this.tvVues.TabIndex = 0;
            this.tvVues.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvVues_AfterSelect);
            // 
            // bLoadXml
            // 
            this.bLoadXml.Location = new System.Drawing.Point(12, 12);
            this.bLoadXml.Name = "bLoadXml";
            this.bLoadXml.Size = new System.Drawing.Size(40, 23);
            this.bLoadXml.TabIndex = 1;
            this.bLoadXml.Text = "Load";
            this.bLoadXml.UseVisualStyleBackColor = true;
            this.bLoadXml.Click += new System.EventHandler(this.bLoadXml_Click);
            // 
            // tbXml
            // 
            this.tbXml.Location = new System.Drawing.Point(58, 15);
            this.tbXml.Name = "tbXml";
            this.tbXml.ReadOnly = true;
            this.tbXml.Size = new System.Drawing.Size(182, 20);
            this.tbXml.TabIndex = 2;
            // 
            // bLoadVue
            // 
            this.bLoadVue.Enabled = false;
            this.bLoadVue.Location = new System.Drawing.Point(12, 286);
            this.bLoadVue.Name = "bLoadVue";
            this.bLoadVue.Size = new System.Drawing.Size(62, 23);
            this.bLoadVue.TabIndex = 3;
            this.bLoadVue.Text = "Load Vue";
            this.bLoadVue.UseVisualStyleBackColor = true;
            this.bLoadVue.Click += new System.EventHandler(this.bLoadVue_Click);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(178, 286);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(62, 23);
            this.bClose.TabIndex = 4;
            this.bClose.Text = "Close";
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // FormVisu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 321);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.bLoadVue);
            this.Controls.Add(this.tbXml);
            this.Controls.Add(this.bLoadXml);
            this.Controls.Add(this.tvVues);
            this.Name = "FormVisu";
            this.Text = "FormVisu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvVues;
        private System.Windows.Forms.Button bLoadXml;
        private System.Windows.Forms.TextBox tbXml;
        private System.Windows.Forms.Button bLoadVue;
        private System.Windows.Forms.Button bClose;
    }
}