namespace DrawTools
{
    partial class FormProgress
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
            this.pbTraitement = new System.Windows.Forms.ProgressBar();
            this.lText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pbTraitement
            // 
            this.pbTraitement.Location = new System.Drawing.Point(12, 31);
            this.pbTraitement.Name = "pbTraitement";
            this.pbTraitement.Size = new System.Drawing.Size(490, 23);
            this.pbTraitement.TabIndex = 0;
            // 
            // lText
            // 
            this.lText.AutoSize = true;
            this.lText.Location = new System.Drawing.Point(12, 9);
            this.lText.MaximumSize = new System.Drawing.Size(490, 0);
            this.lText.Name = "lText";
            this.lText.Size = new System.Drawing.Size(104, 13);
            this.lText.TabIndex = 1;
            this.lText.Text = "Debut du Traitement";
            // 
            // FormProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 66);
            this.ControlBox = false;
            this.Controls.Add(this.lText);
            this.Controls.Add(this.pbTraitement);
            this.Name = "FormProgress";
            this.Text = "Progression";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbTraitement;
        private System.Windows.Forms.Label lText;
    }
}