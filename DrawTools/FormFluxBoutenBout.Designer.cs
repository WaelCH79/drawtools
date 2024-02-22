namespace DrawTools
{
    partial class FormFluxBoutenBout
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tvFluxApp = new System.Windows.Forms.TreeView();
            this.tvFluxTech = new System.Windows.Forms.TreeView();
            this.bEnd = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.bOK);
            this.splitContainer1.Panel2.Controls.Add(this.bEnd);
            this.splitContainer1.Size = new System.Drawing.Size(797, 437);
            this.splitContainer1.SplitterDistance = 380;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tvFluxApp);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tvFluxTech);
            this.splitContainer2.Size = new System.Drawing.Size(797, 380);
            this.splitContainer2.SplitterDistance = 382;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 1;
            // 
            // tvFluxApp
            // 
            this.tvFluxApp.AllowDrop = true;
            this.tvFluxApp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvFluxApp.Location = new System.Drawing.Point(0, 0);
            this.tvFluxApp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tvFluxApp.Name = "tvFluxApp";
            this.tvFluxApp.Size = new System.Drawing.Size(382, 380);
            this.tvFluxApp.TabIndex = 0;
            // 
            // tvFluxTech
            // 
            this.tvFluxTech.AllowDrop = true;
            this.tvFluxTech.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvFluxTech.Location = new System.Drawing.Point(0, 0);
            this.tvFluxTech.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tvFluxTech.Name = "tvFluxTech";
            this.tvFluxTech.Size = new System.Drawing.Size(410, 380);
            this.tvFluxTech.TabIndex = 1;
            // 
            // bEnd
            // 
            this.bEnd.Location = new System.Drawing.Point(158, 16);
            this.bEnd.Name = "bEnd";
            this.bEnd.Size = new System.Drawing.Size(75, 23);
            this.bEnd.TabIndex = 0;
            this.bEnd.Text = "annuler";
            this.bEnd.UseVisualStyleBackColor = true;
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(26, 16);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 1;
            this.bOK.Text = "valider";
            this.bOK.UseVisualStyleBackColor = true;
            // 
            // FormFluxBoutenBout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 437);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormFluxBoutenBout";
            this.Text = "FormFluxBoutenBout";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvFluxApp;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView tvFluxTech;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bEnd;
    }
}