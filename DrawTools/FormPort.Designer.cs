﻿namespace DrawTools
{
    partial class FormPort
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
            this.TBProtocole = new System.Windows.Forms.TextBox();
            this.TBNomS = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.BtEffacer = new System.Windows.Forms.Button();
            this.TBPort = new System.Windows.Forms.TextBox();
            this.dgVLan = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.TBService = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgVLan)).BeginInit();
            this.SuspendLayout();
            // 
            // TBProtocole
            // 
            this.TBProtocole.Location = new System.Drawing.Point(156, 110);
            this.TBProtocole.Name = "TBProtocole";
            this.TBProtocole.Size = new System.Drawing.Size(199, 26);
            this.TBProtocole.TabIndex = 3;
            // 
            // TBNomS
            // 
            this.TBNomS.Location = new System.Drawing.Point(156, 67);
            this.TBNomS.Name = "TBNomS";
            this.TBNomS.Size = new System.Drawing.Size(199, 26);
            this.TBNomS.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 25);
            this.label1.TabIndex = 27;
            this.label1.Text = "Recherche";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 20);
            this.label2.TabIndex = 28;
            this.label2.Text = "Nom du Service";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 20);
            this.label3.TabIndex = 29;
            this.label3.Text = "Protocole";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 20);
            this.label6.TabIndex = 34;
            this.label6.Text = "Port";
            // 
            // BtEffacer
            // 
            this.BtEffacer.Location = new System.Drawing.Point(409, 88);
            this.BtEffacer.Name = "BtEffacer";
            this.BtEffacer.Size = new System.Drawing.Size(111, 36);
            this.BtEffacer.TabIndex = 6;
            this.BtEffacer.Text = "Rechercher";
            this.BtEffacer.UseVisualStyleBackColor = true;
            this.BtEffacer.Click += new System.EventHandler(this.button1_Click);
            // 
            // TBPort
            // 
            this.TBPort.Location = new System.Drawing.Point(156, 153);
            this.TBPort.Name = "TBPort";
            this.TBPort.Size = new System.Drawing.Size(199, 26);
            this.TBPort.TabIndex = 5;
            // 
            // dgVLan
            // 
            this.dgVLan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgVLan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVLan.Location = new System.Drawing.Point(28, 279);
            this.dgVLan.Name = "dgVLan";
            this.dgVLan.RowHeadersWidth = 62;
            this.dgVLan.RowTemplate.Height = 28;
            this.dgVLan.Size = new System.Drawing.Size(1390, 446);
            this.dgVLan.TabIndex = 37;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(409, 129);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 36);
            this.button2.TabIndex = 7;
            this.button2.Text = "Effacer";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TBService
            // 
            this.TBService.Location = new System.Drawing.Point(156, 199);
            this.TBService.Name = "TBService";
            this.TBService.Size = new System.Drawing.Size(199, 26);
            this.TBService.TabIndex = 38;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 20);
            this.label4.TabIndex = 39;
            this.label4.Text = "Service";
            // 
            // FormPort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(1444, 746);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TBService);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dgVLan);
            this.Controls.Add(this.TBPort);
            this.Controls.Add(this.BtEffacer);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TBNomS);
            this.Controls.Add(this.TBProtocole);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPort";
            this.Text = "Liste des Ports";
            this.Load += new System.EventHandler(this.FormPort_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgVLan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TBProtocole;
        private System.Windows.Forms.TextBox TBNomS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button BtEffacer;
        private System.Windows.Forms.TextBox TBPort;
        private System.Windows.Forms.DataGridView dgVLan;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox TBService;
        private System.Windows.Forms.Label label4;
    }
}