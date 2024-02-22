namespace DrawTools
{
    partial class FormPropVue
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
            this.tbNom = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbNomApplication = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbPrefixNom = new System.Windows.Forms.TextBox();
            this.tbGuidVue = new System.Windows.Forms.TextBox();
            this.grpEcoSystem = new System.Windows.Forms.GroupBox();
            this.cbGuidEnv = new System.Windows.Forms.ComboBox();
            this.cbGuidVueInf = new System.Windows.Forms.ComboBox();
            this.cbEnv = new System.Windows.Forms.ComboBox();
            this.cbNomVueInf = new System.Windows.Forms.ComboBox();
            this.cbGuidTypeVue = new System.Windows.Forms.ComboBox();
            this.cbNomTypeVue = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.grpNewView = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbNomNouvelleVue = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.grpEcoSystem.SuspendLayout();
            this.grpNewView.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbNom
            // 
            this.tbNom.Location = new System.Drawing.Point(124, 19);
            this.tbNom.Name = "tbNom";
            this.tbNom.Size = new System.Drawing.Size(198, 20);
            this.tbNom.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nom Vue";
            // 
            // tbNomApplication
            // 
            this.tbNomApplication.Location = new System.Drawing.Point(101, 19);
            this.tbNomApplication.Name = "tbNomApplication";
            this.tbNomApplication.ReadOnly = true;
            this.tbNomApplication.Size = new System.Drawing.Size(100, 20);
            this.tbNomApplication.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Application";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(356, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Vue inférieure";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbNom);
            this.groupBox1.Controls.Add(this.tbPrefixNom);
            this.groupBox1.Controls.Add(this.tbGuidVue);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(557, 50);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Identification";
            // 
            // tbPrefixNom
            // 
            this.tbPrefixNom.Location = new System.Drawing.Point(98, 19);
            this.tbPrefixNom.Name = "tbPrefixNom";
            this.tbPrefixNom.ReadOnly = true;
            this.tbPrefixNom.Size = new System.Drawing.Size(221, 20);
            this.tbPrefixNom.TabIndex = 22;
            // 
            // tbGuidVue
            // 
            this.tbGuidVue.Location = new System.Drawing.Point(325, 19);
            this.tbGuidVue.Name = "tbGuidVue";
            this.tbGuidVue.ReadOnly = true;
            this.tbGuidVue.Size = new System.Drawing.Size(221, 20);
            this.tbGuidVue.TabIndex = 21;
            this.tbGuidVue.Visible = false;
            // 
            // grpEcoSystem
            // 
            this.grpEcoSystem.Controls.Add(this.cbGuidEnv);
            this.grpEcoSystem.Controls.Add(this.cbGuidVueInf);
            this.grpEcoSystem.Controls.Add(this.cbEnv);
            this.grpEcoSystem.Controls.Add(this.cbNomVueInf);
            this.grpEcoSystem.Controls.Add(this.cbGuidTypeVue);
            this.grpEcoSystem.Controls.Add(this.cbNomTypeVue);
            this.grpEcoSystem.Controls.Add(this.label2);
            this.grpEcoSystem.Controls.Add(this.label33);
            this.grpEcoSystem.Controls.Add(this.tbNomApplication);
            this.grpEcoSystem.Controls.Add(this.label6);
            this.grpEcoSystem.Controls.Add(this.label7);
            this.grpEcoSystem.Location = new System.Drawing.Point(12, 68);
            this.grpEcoSystem.Name = "grpEcoSystem";
            this.grpEcoSystem.Size = new System.Drawing.Size(557, 78);
            this.grpEcoSystem.TabIndex = 17;
            this.grpEcoSystem.TabStop = false;
            this.grpEcoSystem.Text = "Eco-System";
            // 
            // cbGuidEnv
            // 
            this.cbGuidEnv.FormattingEnabled = true;
            this.cbGuidEnv.Location = new System.Drawing.Point(434, 40);
            this.cbGuidEnv.Name = "cbGuidEnv";
            this.cbGuidEnv.Size = new System.Drawing.Size(92, 21);
            this.cbGuidEnv.TabIndex = 23;
            this.cbGuidEnv.Visible = false;
            // 
            // cbGuidVueInf
            // 
            this.cbGuidVueInf.FormattingEnabled = true;
            this.cbGuidVueInf.Location = new System.Drawing.Point(434, 14);
            this.cbGuidVueInf.Name = "cbGuidVueInf";
            this.cbGuidVueInf.Size = new System.Drawing.Size(92, 21);
            this.cbGuidVueInf.TabIndex = 22;
            this.cbGuidVueInf.Visible = false;
            // 
            // cbEnv
            // 
            this.cbEnv.FormattingEnabled = true;
            this.cbEnv.Location = new System.Drawing.Point(446, 45);
            this.cbEnv.Name = "cbEnv";
            this.cbEnv.Size = new System.Drawing.Size(100, 21);
            this.cbEnv.TabIndex = 21;
            // 
            // cbNomVueInf
            // 
            this.cbNomVueInf.FormattingEnabled = true;
            this.cbNomVueInf.Location = new System.Drawing.Point(446, 19);
            this.cbNomVueInf.Name = "cbNomVueInf";
            this.cbNomVueInf.Size = new System.Drawing.Size(100, 21);
            this.cbNomVueInf.TabIndex = 20;
            // 
            // cbGuidTypeVue
            // 
            this.cbGuidTypeVue.FormattingEnabled = true;
            this.cbGuidTypeVue.Location = new System.Drawing.Point(242, 31);
            this.cbGuidTypeVue.Name = "cbGuidTypeVue";
            this.cbGuidTypeVue.Size = new System.Drawing.Size(92, 21);
            this.cbGuidTypeVue.TabIndex = 19;
            this.cbGuidTypeVue.Visible = false;
            // 
            // cbNomTypeVue
            // 
            this.cbNomTypeVue.FormattingEnabled = true;
            this.cbNomTypeVue.Location = new System.Drawing.Point(101, 45);
            this.cbNomTypeVue.Name = "cbNomTypeVue";
            this.cbNomTypeVue.Size = new System.Drawing.Size(221, 21);
            this.cbNomTypeVue.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Type Vue";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(356, 48);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(78, 13);
            this.label33.TabIndex = 16;
            this.label33.Text = "Environnement";
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(12, 152);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 19;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(93, 152);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 20;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // grpNewView
            // 
            this.grpNewView.Controls.Add(this.label3);
            this.grpNewView.Controls.Add(this.tbNomNouvelleVue);
            this.grpNewView.Location = new System.Drawing.Point(205, 152);
            this.grpNewView.Name = "grpNewView";
            this.grpNewView.Size = new System.Drawing.Size(353, 60);
            this.grpNewView.TabIndex = 21;
            this.grpNewView.TabStop = false;
            this.grpNewView.Text = "Nouvelle Vue";
            this.grpNewView.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Nom Nouvelle  Vue";
            // 
            // tbNomNouvelleVue
            // 
            this.tbNomNouvelleVue.Location = new System.Drawing.Point(124, 19);
            this.tbNomNouvelleVue.Name = "tbNomNouvelleVue";
            this.tbNomNouvelleVue.Size = new System.Drawing.Size(165, 20);
            this.tbNomNouvelleVue.TabIndex = 0;
            // 
            // FormPropVue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 190);
            this.Controls.Add(this.grpNewView);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpEcoSystem);
            this.Name = "FormPropVue";
            this.Text = "FormPropVue";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpEcoSystem.ResumeLayout(false);
            this.grpEcoSystem.PerformLayout();
            this.grpNewView.ResumeLayout(false);
            this.grpNewView.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbNom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbNomApplication;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grpEcoSystem;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox tbGuidVue;
        private System.Windows.Forms.ComboBox cbNomTypeVue;
        private System.Windows.Forms.ComboBox cbGuidTypeVue;
        private System.Windows.Forms.TextBox tbPrefixNom;
        private System.Windows.Forms.ComboBox cbGuidEnv;
        private System.Windows.Forms.ComboBox cbGuidVueInf;
        private System.Windows.Forms.ComboBox cbEnv;
        private System.Windows.Forms.ComboBox cbNomVueInf;
        private System.Windows.Forms.GroupBox grpNewView;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbNomNouvelleVue;
    }
}