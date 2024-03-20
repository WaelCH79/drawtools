namespace DrawTools
{
    partial class FormService
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
            this.bClose = new System.Windows.Forms.Button();
            this.bDelete = new System.Windows.Forms.Button();
            this.bModify = new System.Windows.Forms.Button();
            this.bCreate = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbPorts = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbProtocole = new System.Windows.Forms.TextBox();
            this.Protocole = new System.Windows.Forms.Label();
            this.tbInfoSup = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbNomService = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbService = new System.Windows.Forms.ListBox();
            this.tbServiceFiltre = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(652, 355);
            this.bClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(93, 35);
            this.bClose.TabIndex = 13;
            this.bClose.Text = "Close";
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // bDelete
            // 
            this.bDelete.Location = new System.Drawing.Point(550, 355);
            this.bDelete.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new System.Drawing.Size(93, 35);
            this.bDelete.TabIndex = 12;
            this.bDelete.Text = "Delete";
            this.bDelete.UseVisualStyleBackColor = true;
            this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
            // 
            // bModify
            // 
            this.bModify.Location = new System.Drawing.Point(448, 355);
            this.bModify.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bModify.Name = "bModify";
            this.bModify.Size = new System.Drawing.Size(93, 35);
            this.bModify.TabIndex = 11;
            this.bModify.Text = "Modify";
            this.bModify.UseVisualStyleBackColor = true;
            this.bModify.Click += new System.EventHandler(this.bModify_Click);
            // 
            // bCreate
            // 
            this.bCreate.Location = new System.Drawing.Point(346, 355);
            this.bCreate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bCreate.Name = "bCreate";
            this.bCreate.Size = new System.Drawing.Size(93, 35);
            this.bCreate.TabIndex = 10;
            this.bCreate.Text = "Create";
            this.bCreate.UseVisualStyleBackColor = true;
            this.bCreate.Click += new System.EventHandler(this.bCreate_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbDescription);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tbPorts);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tbProtocole);
            this.groupBox2.Controls.Add(this.Protocole);
            this.groupBox2.Controls.Add(this.tbInfoSup);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tbNomService);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(346, 18);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(399, 328);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detail";
            // 
            // tbDescription
            // 
            this.tbDescription.Location = new System.Drawing.Point(9, 282);
            this.tbDescription.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(379, 26);
            this.tbDescription.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 258);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Description";
            // 
            // tbPorts
            // 
            this.tbPorts.Location = new System.Drawing.Point(9, 222);
            this.tbPorts.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbPorts.Name = "tbPorts";
            this.tbPorts.Size = new System.Drawing.Size(379, 26);
            this.tbPorts.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 198);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Ports";
            // 
            // tbProtocole
            // 
            this.tbProtocole.Location = new System.Drawing.Point(9, 163);
            this.tbProtocole.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbProtocole.Name = "tbProtocole";
            this.tbProtocole.Size = new System.Drawing.Size(379, 26);
            this.tbProtocole.TabIndex = 7;
            // 
            // Protocole
            // 
            this.Protocole.AutoSize = true;
            this.Protocole.Location = new System.Drawing.Point(9, 140);
            this.Protocole.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Protocole.Name = "Protocole";
            this.Protocole.Size = new System.Drawing.Size(76, 20);
            this.Protocole.TabIndex = 8;
            this.Protocole.Text = "Protocole";
            // 
            // tbInfoSup
            // 
            this.tbInfoSup.Location = new System.Drawing.Point(9, 105);
            this.tbInfoSup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbInfoSup.Name = "tbInfoSup";
            this.tbInfoSup.Size = new System.Drawing.Size(379, 26);
            this.tbInfoSup.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 82);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Information supplémentaire";
            // 
            // tbNomService
            // 
            this.tbNomService.Location = new System.Drawing.Point(9, 46);
            this.tbNomService.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbNomService.Name = "tbNomService";
            this.tbNomService.Size = new System.Drawing.Size(379, 26);
            this.tbNomService.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nom Port";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbService);
            this.groupBox1.Controls.Add(this.tbServiceFiltre);
            this.groupBox1.Location = new System.Drawing.Point(18, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(320, 372);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ports";
            // 
            // lbService
            // 
            this.lbService.FormattingEnabled = true;
            this.lbService.ItemHeight = 20;
            this.lbService.Location = new System.Drawing.Point(9, 69);
            this.lbService.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbService.Name = "lbService";
            this.lbService.Size = new System.Drawing.Size(300, 284);
            this.lbService.TabIndex = 2;
            // 
            // tbServiceFiltre
            // 
            this.tbServiceFiltre.Location = new System.Drawing.Point(9, 29);
            this.tbServiceFiltre.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbServiceFiltre.Name = "tbServiceFiltre";
            this.tbServiceFiltre.Size = new System.Drawing.Size(300, 26);
            this.tbServiceFiltre.TabIndex = 2;
            this.tbServiceFiltre.TextChanged += new System.EventHandler(this.tbServiceFiltre_TextChanged);
            // 
            // FormService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 408);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.bDelete);
            this.Controls.Add(this.bModify);
            this.Controls.Add(this.bCreate);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormService";
            this.Text = "FormService";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.Button bDelete;
        private System.Windows.Forms.Button bModify;
        private System.Windows.Forms.Button bCreate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbNomService;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lbService;
        private System.Windows.Forms.TextBox tbServiceFiltre;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbPorts;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbProtocole;
        private System.Windows.Forms.Label Protocole;
        private System.Windows.Forms.TextBox tbInfoSup;
        private System.Windows.Forms.Label label1;
    }
}