namespace DrawTools
{
    partial class FormGroupService
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbGroupService = new System.Windows.Forms.ListBox();
            this.tbGroupServiceFiltre = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbNomGroupService = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bCreate = new System.Windows.Forms.Button();
            this.bModify = new System.Windows.Forms.Button();
            this.bDelete = new System.Windows.Forms.Button();
            this.bClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbNomFonctionService = new System.Windows.Forms.ComboBox();
            this.cbGuidFonctionService = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbGroupService);
            this.groupBox1.Controls.Add(this.tbGroupServiceFiltre);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(213, 242);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Service";
            // 
            // lbGroupService
            // 
            this.lbGroupService.FormattingEnabled = true;
            this.lbGroupService.Location = new System.Drawing.Point(6, 45);
            this.lbGroupService.Name = "lbGroupService";
            this.lbGroupService.Size = new System.Drawing.Size(201, 186);
            this.lbGroupService.TabIndex = 2;
            // 
            // tbGroupServiceFiltre
            // 
            this.tbGroupServiceFiltre.Location = new System.Drawing.Point(6, 19);
            this.tbGroupServiceFiltre.Name = "tbGroupServiceFiltre";
            this.tbGroupServiceFiltre.Size = new System.Drawing.Size(201, 20);
            this.tbGroupServiceFiltre.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbGuidFonctionService);
            this.groupBox2.Controls.Add(this.cbNomFonctionService);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tbNomGroupService);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(233, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(198, 126);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detail";
            // 
            // tbNomGroupService
            // 
            this.tbNomGroupService.Location = new System.Drawing.Point(6, 38);
            this.tbNomGroupService.Name = "tbNomGroupService";
            this.tbNomGroupService.Size = new System.Drawing.Size(186, 20);
            this.tbNomGroupService.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nom Service";
            // 
            // bCreate
            // 
            this.bCreate.Location = new System.Drawing.Point(233, 144);
            this.bCreate.Name = "bCreate";
            this.bCreate.Size = new System.Drawing.Size(62, 23);
            this.bCreate.TabIndex = 4;
            this.bCreate.Text = "Create";
            this.bCreate.UseVisualStyleBackColor = true;
            // 
            // bModify
            // 
            this.bModify.Location = new System.Drawing.Point(301, 144);
            this.bModify.Name = "bModify";
            this.bModify.Size = new System.Drawing.Size(62, 23);
            this.bModify.TabIndex = 5;
            this.bModify.Text = "Modify";
            this.bModify.UseVisualStyleBackColor = true;
            // 
            // bDelete
            // 
            this.bDelete.Location = new System.Drawing.Point(369, 144);
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new System.Drawing.Size(62, 23);
            this.bDelete.TabIndex = 6;
            this.bDelete.Text = "Delete";
            this.bDelete.UseVisualStyleBackColor = true;
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(369, 231);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(62, 23);
            this.bClose.TabIndex = 7;
            this.bClose.Text = "Close";
            this.bClose.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Fonction";
            // 
            // cbNomFonctionService
            // 
            this.cbNomFonctionService.FormattingEnabled = true;
            this.cbNomFonctionService.Location = new System.Drawing.Point(6, 86);
            this.cbNomFonctionService.Name = "cbNomFonctionService";
            this.cbNomFonctionService.Size = new System.Drawing.Size(186, 21);
            this.cbNomFonctionService.TabIndex = 6;
            // 
            // cbGuidFonctionService
            // 
            this.cbGuidFonctionService.FormattingEnabled = true;
            this.cbGuidFonctionService.Location = new System.Drawing.Point(24, 99);
            this.cbGuidFonctionService.Name = "cbGuidFonctionService";
            this.cbGuidFonctionService.Size = new System.Drawing.Size(154, 21);
            this.cbGuidFonctionService.TabIndex = 7;
            this.cbGuidFonctionService.Visible = false;
            // 
            // FormGroupService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 266);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.bDelete);
            this.Controls.Add(this.bModify);
            this.Controls.Add(this.bCreate);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormGroupService";
            this.Text = "FormGroupService";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lbGroupService;
        private System.Windows.Forms.TextBox tbGroupServiceFiltre;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbNomGroupService;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bCreate;
        private System.Windows.Forms.Button bModify;
        private System.Windows.Forms.Button bDelete;
        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.ComboBox cbGuidFonctionService;
        private System.Windows.Forms.ComboBox cbNomFonctionService;
        private System.Windows.Forms.Label label1;
    }
}