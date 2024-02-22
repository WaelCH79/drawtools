namespace DrawTools
{
    partial class FormMsgAndLinkOk
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
            this.llPath = new System.Windows.Forms.LinkLabel();
            this.lPath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(171, 62);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 0;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            // 
            // llPath
            // 
            this.llPath.AutoSize = true;
            this.llPath.Location = new System.Drawing.Point(12, 32);
            this.llPath.Name = "llPath";
            this.llPath.Size = new System.Drawing.Size(23, 13);
            this.llPath.TabIndex = 2;
            this.llPath.TabStop = true;
            this.llPath.Text = "link";
            // 
            // lPath
            // 
            this.lPath.AutoSize = true;
            this.lPath.Location = new System.Drawing.Point(12, 9);
            this.lPath.Name = "lPath";
            this.lPath.Size = new System.Drawing.Size(24, 13);
            this.lPath.TabIndex = 3;
            this.lPath.Text = "text";
            // 
            // FormMessageOk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 98);
            this.Controls.Add(this.lPath);
            this.Controls.Add(this.llPath);
            this.Controls.Add(this.bOK);
            this.Name = "FormMessageOk";
            this.Text = "FormMessageOk";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.LinkLabel llPath;
        private System.Windows.Forms.Label lPath;
    }
}