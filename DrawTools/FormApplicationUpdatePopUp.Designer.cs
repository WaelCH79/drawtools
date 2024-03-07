namespace DrawTools
{
    partial class FormApplicationUpdatePopUp
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
            this.components = new System.ComponentModel.Container();
            this.label9 = new System.Windows.Forms.Label();
            this.bCancel = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            this.tbLabelAppVersion = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbNomAppli = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTrigramme = new System.Windows.Forms.TextBox();
            this.grpVersion = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbNomTypeVue = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TBLabelVue = new System.Windows.Forms.TextBox();
            this.tbPrefixNom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.EP_TXT_NomAppli = new System.Windows.Forms.ErrorProvider(this.components);
            this.EP_TXT_Trigramme = new System.Windows.Forms.ErrorProvider(this.components);
            this.EP_TXT_NomVue = new System.Windows.Forms.ErrorProvider(this.components);
            this.EP_TXT_Version = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.grpVersion.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP_TXT_NomAppli)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP_TXT_Trigramme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP_TXT_NomVue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP_TXT_Version)).BeginInit();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 32);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 20);
            this.label9.TabIndex = 6;
            this.label9.Text = "Version";
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(177, 378);
            this.bCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(112, 35);
            this.bCancel.TabIndex = 28;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(25, 378);
            this.bOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(112, 35);
            this.bOK.TabIndex = 27;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // tbLabelAppVersion
            // 
            this.tbLabelAppVersion.Location = new System.Drawing.Point(152, 26);
            this.tbLabelAppVersion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbLabelAppVersion.Name = "tbLabelAppVersion";
            this.tbLabelAppVersion.Size = new System.Drawing.Size(250, 26);
            this.tbLabelAppVersion.TabIndex = 11;
            this.tbLabelAppVersion.Validating += new System.ComponentModel.CancelEventHandler(this.tbAppVersion_Validating);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbNomAppli);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbTrigramme);
            this.groupBox1.Location = new System.Drawing.Point(25, 31);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(834, 84);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Identification";
            // 
            // tbNomAppli
            // 
            this.tbNomAppli.Location = new System.Drawing.Point(152, 29);
            this.tbNomAppli.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbNomAppli.Name = "tbNomAppli";
            this.tbNomAppli.Size = new System.Drawing.Size(330, 26);
            this.tbNomAppli.TabIndex = 0;
            this.tbNomAppli.Validating += new System.ComponentModel.CancelEventHandler(this.tbNom_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nom Application";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(534, 34);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Trigramme";
            // 
            // tbTrigramme
            // 
            this.tbTrigramme.Location = new System.Drawing.Point(646, 29);
            this.tbTrigramme.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbTrigramme.Name = "tbTrigramme";
            this.tbTrigramme.Size = new System.Drawing.Size(48, 26);
            this.tbTrigramme.TabIndex = 3;
            this.tbTrigramme.TextChanged += new System.EventHandler(this.tbTrigramme_TextChanged);
            this.tbTrigramme.Validating += new System.ComponentModel.CancelEventHandler(this.tbTrigramme_Validating);
            // 
            // grpVersion
            // 
            this.grpVersion.Controls.Add(this.tbLabelAppVersion);
            this.grpVersion.Controls.Add(this.label9);
            this.grpVersion.Location = new System.Drawing.Point(23, 125);
            this.grpVersion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpVersion.Name = "grpVersion";
            this.grpVersion.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpVersion.Size = new System.Drawing.Size(834, 83);
            this.grpVersion.TabIndex = 26;
            this.grpVersion.TabStop = false;
            this.grpVersion.Text = "Version";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbNomTypeVue);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.TBLabelVue);
            this.groupBox2.Controls.Add(this.tbPrefixNom);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(25, 218);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(836, 139);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Vue";
            // 
            // cbNomTypeVue
            // 
            this.cbNomTypeVue.FormattingEnabled = true;
            this.cbNomTypeVue.Location = new System.Drawing.Point(147, 75);
            this.cbNomTypeVue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbNomTypeVue.Name = "cbNomTypeVue";
            this.cbNomTypeVue.Size = new System.Drawing.Size(330, 28);
            this.cbNomTypeVue.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 80);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Type Vue";
            // 
            // TBLabelVue
            // 
            this.TBLabelVue.Location = new System.Drawing.Point(186, 29);
            this.TBLabelVue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TBLabelVue.Name = "TBLabelVue";
            this.TBLabelVue.Size = new System.Drawing.Size(295, 26);
            this.TBLabelVue.TabIndex = 0;
            this.TBLabelVue.Validating += new System.ComponentModel.CancelEventHandler(this.TBVue_Validating);
            // 
            // tbPrefixNom
            // 
            this.tbPrefixNom.Location = new System.Drawing.Point(147, 29);
            this.tbPrefixNom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbPrefixNom.Name = "tbPrefixNom";
            this.tbPrefixNom.ReadOnly = true;
            this.tbPrefixNom.Size = new System.Drawing.Size(42, 26);
            this.tbPrefixNom.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 34);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Nom Vue";
            // 
            // EP_TXT_NomAppli
            // 
            this.EP_TXT_NomAppli.ContainerControl = this;
            // 
            // EP_TXT_Trigramme
            // 
            this.EP_TXT_Trigramme.ContainerControl = this;
            // 
            // EP_TXT_NomVue
            // 
            this.EP_TXT_NomVue.ContainerControl = this;
            // 
            // EP_TXT_Version
            // 
            this.EP_TXT_Version.ContainerControl = this;
            // 
            // FormApplicationUpdatePopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 439);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpVersion);
            this.Name = "FormApplicationUpdatePopUp";
            this.Text = "Application";
            this.Load += new System.EventHandler(this.FormApplicationUpdatePopUp_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpVersion.ResumeLayout(false);
            this.grpVersion.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP_TXT_NomAppli)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP_TXT_Trigramme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP_TXT_NomVue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP_TXT_Version)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.TextBox tbLabelAppVersion;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbNomAppli;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbTrigramme;
        private System.Windows.Forms.GroupBox grpVersion;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbNomTypeVue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TBLabelVue;
        private System.Windows.Forms.TextBox tbPrefixNom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ErrorProvider EP_TXT_NomAppli;
        private System.Windows.Forms.ErrorProvider EP_TXT_Trigramme;
        private System.Windows.Forms.ErrorProvider EP_TXT_NomVue;
        private System.Windows.Forms.ErrorProvider EP_TXT_Version;
    }
}