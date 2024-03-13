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
            this.cbGuidFonctionService = new System.Windows.Forms.ComboBox();
            this.cbNomFonctionService = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbNomGroupService = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bCreate = new System.Windows.Forms.Button();
            this.bModify = new System.Windows.Forms.Button();
            this.bDelete = new System.Windows.Forms.Button();
            this.bClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbGroupService);
            this.groupBox1.Controls.Add(this.tbGroupServiceFiltre);
            this.groupBox1.Location = new System.Drawing.Point(18, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(320, 372);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Service";
            // 
            // lbGroupService
            // 
            this.lbGroupService.FormattingEnabled = true;
            this.lbGroupService.ItemHeight = 20;
            this.lbGroupService.Location = new System.Drawing.Point(9, 69);
            this.lbGroupService.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbGroupService.Name = "lbGroupService";
            this.lbGroupService.Size = new System.Drawing.Size(300, 284);
            this.lbGroupService.TabIndex = 2;
            // 
            // tbGroupServiceFiltre
            // 
            this.tbGroupServiceFiltre.Location = new System.Drawing.Point(9, 29);
            this.tbGroupServiceFiltre.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbGroupServiceFiltre.Name = "tbGroupServiceFiltre";
            this.tbGroupServiceFiltre.Size = new System.Drawing.Size(300, 26);
            this.tbGroupServiceFiltre.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbGuidFonctionService);
            this.groupBox2.Controls.Add(this.cbNomFonctionService);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tbNomGroupService);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(350, 18);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(297, 194);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detail";
            // 
            // cbGuidFonctionService
            // 
            this.cbGuidFonctionService.FormattingEnabled = true;
            this.cbGuidFonctionService.Location = new System.Drawing.Point(36, 152);
            this.cbGuidFonctionService.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbGuidFonctionService.Name = "cbGuidFonctionService";
            this.cbGuidFonctionService.Size = new System.Drawing.Size(229, 28);
            this.cbGuidFonctionService.TabIndex = 7;
            this.cbGuidFonctionService.Visible = false;
            // 
            // cbNomFonctionService
            // 
            this.cbNomFonctionService.FormattingEnabled = true;
            this.cbNomFonctionService.Location = new System.Drawing.Point(9, 132);
            this.cbNomFonctionService.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbNomFonctionService.Name = "cbNomFonctionService";
            this.cbNomFonctionService.Size = new System.Drawing.Size(277, 28);
            this.cbNomFonctionService.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 106);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Fonction";
            // 
            // tbNomGroupService
            // 
            this.tbNomGroupService.Location = new System.Drawing.Point(9, 58);
            this.tbNomGroupService.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbNomGroupService.Name = "tbNomGroupService";
            this.tbNomGroupService.Size = new System.Drawing.Size(277, 26);
            this.tbNomGroupService.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nom Service";
            // 
            // bCreate
            // 
            this.bCreate.Location = new System.Drawing.Point(350, 222);
            this.bCreate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bCreate.Name = "bCreate";
            this.bCreate.Size = new System.Drawing.Size(93, 35);
            this.bCreate.TabIndex = 4;
            this.bCreate.Text = "Create";
            this.bCreate.UseVisualStyleBackColor = true;
            // 
            // bModify
            // 
            this.bModify.Location = new System.Drawing.Point(452, 222);
            this.bModify.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bModify.Name = "bModify";
            this.bModify.Size = new System.Drawing.Size(93, 35);
            this.bModify.TabIndex = 5;
            this.bModify.Text = "Modify";
            this.bModify.UseVisualStyleBackColor = true;
            // 
            // bDelete
            // 
            this.bDelete.Location = new System.Drawing.Point(554, 222);
            this.bDelete.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new System.Drawing.Size(93, 35);
            this.bDelete.TabIndex = 6;
            this.bDelete.Text = "Delete";
            this.bDelete.UseVisualStyleBackColor = true;
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(554, 355);
            this.bClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(93, 35);
            this.bClose.TabIndex = 7;
            this.bClose.Text = "Close";
            this.bClose.UseVisualStyleBackColor = true;
            // 
            // FormGroupService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 409);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.bDelete);
            this.Controls.Add(this.bModify);
            this.Controls.Add(this.bCreate);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
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