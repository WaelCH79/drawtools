namespace DrawTools
{
    partial class FormAxe
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
            this.OK = new System.Windows.Forms.Button();
            this.tvDonnees = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbServ = new System.Windows.Forms.RadioButton();
            this.rbTech = new System.Windows.Forms.RadioButton();
            this.rbApp = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbGuidAbsisse2 = new System.Windows.Forms.ComboBox();
            this.cbAbsisse2 = new System.Windows.Forms.ComboBox();
            this.cbGuidAbsisse = new System.Windows.Forms.ComboBox();
            this.cbAbsisse = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbGuidOrdonnee2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbOrdonnee2 = new System.Windows.Forms.ComboBox();
            this.cbGuidOrdonnee = new System.Windows.Forms.ComboBox();
            this.cbOrdonnee = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ckbAppExt = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbParamFiltre = new System.Windows.Forms.ComboBox();
            this.cbGuidFiltre = new System.Windows.Forms.ComboBox();
            this.cbFiltre = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(27, 568);
            this.OK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(99, 35);
            this.OK.TabIndex = 3;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // tvDonnees
            // 
            this.tvDonnees.Location = new System.Drawing.Point(9, 140);
            this.tvDonnees.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tvDonnees.Name = "tvDonnees";
            this.tvDonnees.Size = new System.Drawing.Size(396, 369);
            this.tvDonnees.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbServ);
            this.groupBox1.Controls.Add(this.rbTech);
            this.groupBox1.Controls.Add(this.rbApp);
            this.groupBox1.Controls.Add(this.tvDonnees);
            this.groupBox1.Location = new System.Drawing.Point(18, 19);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(415, 525);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Données";
            // 
            // rbServ
            // 
            this.rbServ.AutoSize = true;
            this.rbServ.Location = new System.Drawing.Point(9, 55);
            this.rbServ.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbServ.Name = "rbServ";
            this.rbServ.Size = new System.Drawing.Size(168, 24);
            this.rbServ.TabIndex = 8;
            this.rbServ.Text = "Patrimoine Serveur";
            this.rbServ.UseVisualStyleBackColor = true;
            this.rbServ.CheckedChanged += new System.EventHandler(this.rbServ_CheckedChanged);
            // 
            // rbTech
            // 
            this.rbTech.AutoSize = true;
            this.rbTech.Location = new System.Drawing.Point(9, 85);
            this.rbTech.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbTech.Name = "rbTech";
            this.rbTech.Size = new System.Drawing.Size(187, 24);
            this.rbTech.TabIndex = 7;
            this.rbTech.TabStop = true;
            this.rbTech.Text = "Patrimoine Technique";
            this.rbTech.UseVisualStyleBackColor = true;
            this.rbTech.CheckedChanged += new System.EventHandler(this.rbTech_CheckedChanged);
            // 
            // rbApp
            // 
            this.rbApp.AutoSize = true;
            this.rbApp.Location = new System.Drawing.Point(9, 26);
            this.rbApp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbApp.Name = "rbApp";
            this.rbApp.Size = new System.Drawing.Size(178, 24);
            this.rbApp.TabIndex = 6;
            this.rbApp.TabStop = true;
            this.rbApp.Text = "Patrimoine Applicatif";
            this.rbApp.UseVisualStyleBackColor = true;
            this.rbApp.CheckedChanged += new System.EventHandler(this.rbApp_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cbGuidAbsisse2);
            this.groupBox2.Controls.Add(this.cbAbsisse2);
            this.groupBox2.Controls.Add(this.cbGuidAbsisse);
            this.groupBox2.Controls.Add(this.cbAbsisse);
            this.groupBox2.Location = new System.Drawing.Point(442, 169);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(307, 129);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Abscisse";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 80);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "2nd";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "1er";
            // 
            // cbGuidAbsisse2
            // 
            this.cbGuidAbsisse2.FormattingEnabled = true;
            this.cbGuidAbsisse2.Location = new System.Drawing.Point(148, 80);
            this.cbGuidAbsisse2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbGuidAbsisse2.Name = "cbGuidAbsisse2";
            this.cbGuidAbsisse2.Size = new System.Drawing.Size(104, 28);
            this.cbGuidAbsisse2.TabIndex = 3;
            this.cbGuidAbsisse2.Visible = false;
            // 
            // cbAbsisse2
            // 
            this.cbAbsisse2.FormattingEnabled = true;
            this.cbAbsisse2.Location = new System.Drawing.Point(60, 71);
            this.cbAbsisse2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbAbsisse2.Name = "cbAbsisse2";
            this.cbAbsisse2.Size = new System.Drawing.Size(232, 28);
            this.cbAbsisse2.TabIndex = 2;
            // 
            // cbGuidAbsisse
            // 
            this.cbGuidAbsisse.FormattingEnabled = true;
            this.cbGuidAbsisse.Location = new System.Drawing.Point(136, 20);
            this.cbGuidAbsisse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbGuidAbsisse.Name = "cbGuidAbsisse";
            this.cbGuidAbsisse.Size = new System.Drawing.Size(113, 28);
            this.cbGuidAbsisse.TabIndex = 1;
            this.cbGuidAbsisse.Visible = false;
            // 
            // cbAbsisse
            // 
            this.cbAbsisse.FormattingEnabled = true;
            this.cbAbsisse.Location = new System.Drawing.Point(60, 29);
            this.cbAbsisse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbAbsisse.Name = "cbAbsisse";
            this.cbAbsisse.Size = new System.Drawing.Size(232, 28);
            this.cbAbsisse.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.cbGuidOrdonnee2);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.cbOrdonnee2);
            this.groupBox3.Controls.Add(this.cbGuidOrdonnee);
            this.groupBox3.Controls.Add(this.cbOrdonnee);
            this.groupBox3.Location = new System.Drawing.Point(442, 311);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(307, 122);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ordonnée";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 80);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "2nd";
            // 
            // cbGuidOrdonnee2
            // 
            this.cbGuidOrdonnee2.FormattingEnabled = true;
            this.cbGuidOrdonnee2.Location = new System.Drawing.Point(153, 80);
            this.cbGuidOrdonnee2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbGuidOrdonnee2.Name = "cbGuidOrdonnee2";
            this.cbGuidOrdonnee2.Size = new System.Drawing.Size(84, 28);
            this.cbGuidOrdonnee2.TabIndex = 4;
            this.cbGuidOrdonnee2.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 34);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "1er";
            // 
            // cbOrdonnee2
            // 
            this.cbOrdonnee2.FormattingEnabled = true;
            this.cbOrdonnee2.Location = new System.Drawing.Point(60, 71);
            this.cbOrdonnee2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbOrdonnee2.Name = "cbOrdonnee2";
            this.cbOrdonnee2.Size = new System.Drawing.Size(232, 28);
            this.cbOrdonnee2.TabIndex = 3;
            // 
            // cbGuidOrdonnee
            // 
            this.cbGuidOrdonnee.FormattingEnabled = true;
            this.cbGuidOrdonnee.Location = new System.Drawing.Point(153, 19);
            this.cbGuidOrdonnee.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbGuidOrdonnee.Name = "cbGuidOrdonnee";
            this.cbGuidOrdonnee.Size = new System.Drawing.Size(84, 28);
            this.cbGuidOrdonnee.TabIndex = 2;
            this.cbGuidOrdonnee.Visible = false;
            // 
            // cbOrdonnee
            // 
            this.cbOrdonnee.FormattingEnabled = true;
            this.cbOrdonnee.Location = new System.Drawing.Point(60, 29);
            this.cbOrdonnee.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbOrdonnee.Name = "cbOrdonnee";
            this.cbOrdonnee.Size = new System.Drawing.Size(232, 28);
            this.cbOrdonnee.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ckbAppExt);
            this.groupBox4.Location = new System.Drawing.Point(442, 446);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(307, 98);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Option";
            // 
            // ckbAppExt
            // 
            this.ckbAppExt.AutoSize = true;
            this.ckbAppExt.Location = new System.Drawing.Point(9, 39);
            this.ckbAppExt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ckbAppExt.Name = "ckbAppExt";
            this.ckbAppExt.Size = new System.Drawing.Size(210, 24);
            this.ckbAppExt.TabIndex = 0;
            this.ckbAppExt.Text = "Applications de proximité";
            this.ckbAppExt.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(135, 568);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 35);
            this.button1.TabIndex = 11;
            this.button1.Text = "test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbParamFiltre);
            this.groupBox5.Controls.Add(this.cbGuidFiltre);
            this.groupBox5.Controls.Add(this.cbFiltre);
            this.groupBox5.Location = new System.Drawing.Point(442, 19);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Size = new System.Drawing.Size(307, 129);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Filtre";
            // 
            // cbParamFiltre
            // 
            this.cbParamFiltre.FormattingEnabled = true;
            this.cbParamFiltre.Items.AddRange(new object[] {
            "<Moyenne",
            ">Moyenne"});
            this.cbParamFiltre.Location = new System.Drawing.Point(9, 71);
            this.cbParamFiltre.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbParamFiltre.Name = "cbParamFiltre";
            this.cbParamFiltre.Size = new System.Drawing.Size(232, 28);
            this.cbParamFiltre.TabIndex = 2;
            // 
            // cbGuidFiltre
            // 
            this.cbGuidFiltre.FormattingEnabled = true;
            this.cbGuidFiltre.Location = new System.Drawing.Point(76, 19);
            this.cbGuidFiltre.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbGuidFiltre.Name = "cbGuidFiltre";
            this.cbGuidFiltre.Size = new System.Drawing.Size(124, 28);
            this.cbGuidFiltre.TabIndex = 1;
            this.cbGuidFiltre.Visible = false;
            // 
            // cbFiltre
            // 
            this.cbFiltre.FormattingEnabled = true;
            this.cbFiltre.Location = new System.Drawing.Point(9, 29);
            this.cbFiltre.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbFiltre.Name = "cbFiltre";
            this.cbFiltre.Size = new System.Drawing.Size(232, 28);
            this.cbFiltre.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(243, 568);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(127, 35);
            this.button2.TabIndex = 12;
            this.button2.Text = "Export Techno";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.bExportTecho_Click);
            // 
            // FormAxe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 626);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.OK);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAxe";
            this.Text = "FormAxe";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.TreeView tvDonnees;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbApp;
        private System.Windows.Forms.RadioButton rbTech;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbAbsisse;
        private System.Windows.Forms.ComboBox cbOrdonnee;
        private System.Windows.Forms.ComboBox cbGuidAbsisse;
        private System.Windows.Forms.ComboBox cbGuidOrdonnee;
        private System.Windows.Forms.RadioButton rbServ;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox ckbAppExt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox cbGuidFiltre;
        private System.Windows.Forms.ComboBox cbFiltre;
        private System.Windows.Forms.ComboBox cbParamFiltre;
        private System.Windows.Forms.ComboBox cbGuidAbsisse2;
        private System.Windows.Forms.ComboBox cbAbsisse2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbGuidOrdonnee2;
        private System.Windows.Forms.ComboBox cbOrdonnee2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
    }
}