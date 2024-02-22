namespace DrawTools
{
    partial class FormLayer
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
            this.cbApplication = new System.Windows.Forms.ComboBox();
            this.cbGuidApplication = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbLayer = new System.Windows.Forms.ListBox();
            this.tbLayerFiltre = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbGuidTemplate = new System.Windows.Forms.ComboBox();
            this.cbTemplate = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lNom = new System.Windows.Forms.Label();
            this.tbNomLayer = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.bCreate = new System.Windows.Forms.Button();
            this.bModify = new System.Windows.Forms.Button();
            this.bDelete = new System.Windows.Forms.Button();
            this.bClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbApplication
            // 
            this.cbApplication.FormattingEnabled = true;
            this.cbApplication.Location = new System.Drawing.Point(6, 19);
            this.cbApplication.Name = "cbApplication";
            this.cbApplication.Size = new System.Drawing.Size(435, 21);
            this.cbApplication.TabIndex = 0;
            // 
            // cbGuidApplication
            // 
            this.cbGuidApplication.FormattingEnabled = true;
            this.cbGuidApplication.Location = new System.Drawing.Point(250, 12);
            this.cbGuidApplication.Name = "cbGuidApplication";
            this.cbGuidApplication.Size = new System.Drawing.Size(133, 21);
            this.cbGuidApplication.TabIndex = 1;
            this.cbGuidApplication.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbLayer);
            this.groupBox1.Controls.Add(this.tbLayerFiltre);
            this.groupBox1.Location = new System.Drawing.Point(12, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 163);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Layer";
            // 
            // lbLayer
            // 
            this.lbLayer.FormattingEnabled = true;
            this.lbLayer.Location = new System.Drawing.Point(6, 45);
            this.lbLayer.Name = "lbLayer";
            this.lbLayer.Size = new System.Drawing.Size(205, 108);
            this.lbLayer.TabIndex = 5;
            // 
            // tbLayerFiltre
            // 
            this.tbLayerFiltre.Location = new System.Drawing.Point(6, 19);
            this.tbLayerFiltre.Name = "tbLayerFiltre";
            this.tbLayerFiltre.Size = new System.Drawing.Size(205, 20);
            this.tbLayerFiltre.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbGuidTemplate);
            this.groupBox2.Controls.Add(this.cbTemplate);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lNom);
            this.groupBox2.Controls.Add(this.tbNomLayer);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Location = new System.Drawing.Point(242, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(224, 134);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detail";
            // 
            // cbGuidTemplate
            // 
            this.cbGuidTemplate.FormattingEnabled = true;
            this.cbGuidTemplate.Location = new System.Drawing.Point(96, 62);
            this.cbGuidTemplate.Name = "cbGuidTemplate";
            this.cbGuidTemplate.Size = new System.Drawing.Size(81, 21);
            this.cbGuidTemplate.TabIndex = 12;
            this.cbGuidTemplate.Visible = false;
            // 
            // cbTemplate
            // 
            this.cbTemplate.FormattingEnabled = true;
            this.cbTemplate.Location = new System.Drawing.Point(6, 85);
            this.cbTemplate.Name = "cbTemplate";
            this.cbTemplate.Size = new System.Drawing.Size(205, 21);
            this.cbTemplate.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Template";
            // 
            // lNom
            // 
            this.lNom.AutoSize = true;
            this.lNom.Location = new System.Drawing.Point(7, 19);
            this.lNom.Name = "lNom";
            this.lNom.Size = new System.Drawing.Size(29, 13);
            this.lNom.TabIndex = 9;
            this.lNom.Text = "Nom";
            // 
            // tbNomLayer
            // 
            this.tbNomLayer.Location = new System.Drawing.Point(6, 36);
            this.tbNomLayer.Name = "tbNomLayer";
            this.tbNomLayer.Size = new System.Drawing.Size(205, 20);
            this.tbNomLayer.TabIndex = 7;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(-230, -69);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(454, 63);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Couches Existantes";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbApplication);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.cbGuidApplication);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(454, 50);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Application";
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(-230, -69);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(454, 63);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Couches Existantes";
            // 
            // bCreate
            // 
            this.bCreate.Location = new System.Drawing.Point(242, 208);
            this.bCreate.Name = "bCreate";
            this.bCreate.Size = new System.Drawing.Size(49, 23);
            this.bCreate.TabIndex = 6;
            this.bCreate.Text = "Create";
            this.bCreate.UseVisualStyleBackColor = true;
            // 
            // bModify
            // 
            this.bModify.Location = new System.Drawing.Point(297, 208);
            this.bModify.Name = "bModify";
            this.bModify.Size = new System.Drawing.Size(49, 23);
            this.bModify.TabIndex = 7;
            this.bModify.Text = "Modify";
            this.bModify.UseVisualStyleBackColor = true;
            // 
            // bDelete
            // 
            this.bDelete.Location = new System.Drawing.Point(352, 208);
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new System.Drawing.Size(49, 23);
            this.bDelete.TabIndex = 8;
            this.bDelete.Text = "Delete";
            this.bDelete.UseVisualStyleBackColor = true;
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(407, 208);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(49, 23);
            this.bClose.TabIndex = 9;
            this.bClose.Text = "Close";
            this.bClose.UseVisualStyleBackColor = true;
            // 
            // FormLayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 242);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.bDelete);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.bModify);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bCreate);
            this.Name = "FormLayer";
            this.Text = "FormLayer";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbApplication;
        private System.Windows.Forms.ComboBox cbGuidApplication;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbLayerFiltre;
        private System.Windows.Forms.ListBox lbLayer;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button bCreate;
        private System.Windows.Forms.Button bModify;
        private System.Windows.Forms.Button bDelete;
        private System.Windows.Forms.TextBox tbNomLayer;
        private System.Windows.Forms.Label lNom;
        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.ComboBox cbGuidTemplate;
        private System.Windows.Forms.ComboBox cbTemplate;
        private System.Windows.Forms.Label label1;
    }
}