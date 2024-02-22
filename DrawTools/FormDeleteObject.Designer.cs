namespace DrawTools
{
    partial class FormDeleteObject
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
            this.bOK = new System.Windows.Forms.Button();
            this.bAnnuler = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ckbGraph = new System.Windows.Forms.CheckBox();
            this.ckbBase = new System.Windows.Forms.CheckBox();
            this.ckbGraphNew = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(23, 128);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(53, 21);
            this.bOK.TabIndex = 0;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bAnnuler
            // 
            this.bAnnuler.Location = new System.Drawing.Point(147, 128);
            this.bAnnuler.Name = "bAnnuler";
            this.bAnnuler.Size = new System.Drawing.Size(53, 21);
            this.bAnnuler.TabIndex = 1;
            this.bAnnuler.Text = "Annuler";
            this.bAnnuler.UseVisualStyleBackColor = true;
            this.bAnnuler.Click += new System.EventHandler(this.bAnnuler_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Suppression des Objets séléctionnés";
            // 
            // ckbGraph
            // 
            this.ckbGraph.AutoSize = true;
            this.ckbGraph.Location = new System.Drawing.Point(60, 65);
            this.ckbGraph.Name = "ckbGraph";
            this.ckbGraph.Size = new System.Drawing.Size(101, 17);
            this.ckbGraph.TabIndex = 3;
            this.ckbGraph.Text = "Objet graphique";
            this.ckbGraph.UseVisualStyleBackColor = true;
            this.ckbGraph.CheckedChanged += new System.EventHandler(ckbGraph_CheckedChanged);
            // 
            // ckbBase
            // 
            this.ckbBase.AutoSize = true;
            this.ckbBase.Location = new System.Drawing.Point(60, 88);
            this.ckbBase.Name = "ckbBase";
            this.ckbBase.Size = new System.Drawing.Size(105, 17);
            this.ckbBase.TabIndex = 4;
            this.ckbBase.Text = "Objet Permanent";
            this.ckbBase.UseVisualStyleBackColor = true;
            this.ckbBase.CheckedChanged += new System.EventHandler(this.ckbBase_CheckedChanged);
            // 
            // ckbGraphNew
            // 
            this.ckbGraphNew.AutoSize = true;
            this.ckbGraphNew.Location = new System.Drawing.Point(60, 43);
            this.ckbGraphNew.Name = "ckbGraphNew";
            this.ckbGraphNew.Size = new System.Drawing.Size(116, 17);
            this.ckbGraphNew.TabIndex = 5;
            this.ckbGraphNew.Text = "Objet nouvelle Vue";
            this.ckbGraphNew.UseVisualStyleBackColor = true;
            // 
            // FormDeleteObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 164);
            this.Controls.Add(this.ckbGraphNew);
            this.Controls.Add(this.ckbBase);
            this.Controls.Add(this.ckbGraph);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bAnnuler);
            this.Controls.Add(this.bOK);
            this.Name = "FormDeleteObject";
            this.Text = "FormDeleteObject";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        
        #endregion

        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bAnnuler;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ckbGraph;
        private System.Windows.Forms.CheckBox ckbBase;
        private System.Windows.Forms.CheckBox ckbGraphNew;
    }
}