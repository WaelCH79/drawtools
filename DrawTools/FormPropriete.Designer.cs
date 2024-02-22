namespace DrawTools
{
    partial class FormPropriete
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbRootPath = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            this.bAnnuler = new System.Windows.Forms.Button();
            this.cbComposant = new System.Windows.Forms.CheckBox();
            this.cbppt = new System.Windows.Forms.CheckBox();
            this.cbInstallee = new System.Windows.Forms.CheckBox();
            this.cbToolTip = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Root Path";
            // 
            // tbRootPath
            // 
            this.tbRootPath.Location = new System.Drawing.Point(12, 22);
            this.tbRootPath.Name = "tbRootPath";
            this.tbRootPath.Size = new System.Drawing.Size(241, 20);
            this.tbRootPath.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(253, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 19);
            this.button1.TabIndex = 2;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(15, 233);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(55, 21);
            this.bOK.TabIndex = 3;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bAnnuler
            // 
            this.bAnnuler.Location = new System.Drawing.Point(76, 233);
            this.bAnnuler.Name = "bAnnuler";
            this.bAnnuler.Size = new System.Drawing.Size(55, 21);
            this.bAnnuler.TabIndex = 4;
            this.bAnnuler.Text = "Annuler";
            this.bAnnuler.UseVisualStyleBackColor = true;
            this.bAnnuler.Click += new System.EventHandler(this.bAnnuler_Click);
            // 
            // cbComposant
            // 
            this.cbComposant.AutoSize = true;
            this.cbComposant.Location = new System.Drawing.Point(12, 53);
            this.cbComposant.Name = "cbComposant";
            this.cbComposant.Size = new System.Drawing.Size(274, 17);
            this.cbComposant.TabIndex = 5;
            this.cbComposant.Text = "Prise en compte uniquement des composants utilisés";
            this.cbComposant.UseVisualStyleBackColor = true;
            // 
            // cbppt
            // 
            this.cbppt.AutoSize = true;
            this.cbppt.Location = new System.Drawing.Point(12, 76);
            this.cbppt.Name = "cbppt";
            this.cbppt.Size = new System.Drawing.Size(163, 17);
            this.cbppt.TabIndex = 6;
            this.cbppt.Text = "Schémas format présentation";
            this.cbppt.UseVisualStyleBackColor = true;
            // 
            // cbInstallee
            // 
            this.cbInstallee.AutoSize = true;
            this.cbInstallee.Location = new System.Drawing.Point(12, 99);
            this.cbInstallee.Name = "cbInstallee";
            this.cbInstallee.Size = new System.Drawing.Size(291, 17);
            this.cbInstallee.TabIndex = 7;
            this.cbInstallee.Text = "Prise en Compte des applications installée en production";
            this.cbInstallee.UseVisualStyleBackColor = true;
            // 
            // cbToolTip
            // 
            this.cbToolTip.AutoSize = true;
            this.cbToolTip.Location = new System.Drawing.Point(12, 122);
            this.cbToolTip.Name = "cbToolTip";
            this.cbToolTip.Size = new System.Drawing.Size(125, 17);
            this.cbToolTip.TabIndex = 8;
            this.cbToolTip.Text = "Activer les info-bulles";
            this.cbToolTip.UseVisualStyleBackColor = true;
            // 
            // FormPropriete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 266);
            this.Controls.Add(this.cbToolTip);
            this.Controls.Add(this.cbInstallee);
            this.Controls.Add(this.cbppt);
            this.Controls.Add(this.cbComposant);
            this.Controls.Add(this.bAnnuler);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbRootPath);
            this.Controls.Add(this.label1);
            this.Name = "FormPropriete";
            this.Text = "FormPropriete";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbRootPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bAnnuler;
        private System.Windows.Forms.CheckBox cbComposant;
        private System.Windows.Forms.CheckBox cbppt;
        private System.Windows.Forms.CheckBox cbInstallee;
        private System.Windows.Forms.CheckBox cbToolTip;
    }
}