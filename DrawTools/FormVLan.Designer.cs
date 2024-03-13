namespace DrawTools
{
    partial class FormVLan
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
            this.TBNumeVl = new System.Windows.Forms.TextBox();
            this.TBNomVL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TbNomReseau = new System.Windows.Forms.TextBox();
            this.TBPasserelle = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.BtEffacer = new System.Windows.Forms.Button();
            this.TBCodePays = new System.Windows.Forms.TextBox();
            this.dgVLan = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgVLan)).BeginInit();
            this.SuspendLayout();
            // 
            // TBNumeVl
            // 
            this.TBNumeVl.Location = new System.Drawing.Point(156, 110);
            this.TBNumeVl.Name = "TBNumeVl";
            this.TBNumeVl.Size = new System.Drawing.Size(199, 26);
            this.TBNumeVl.TabIndex = 3;
            // 
            // TBNomVL
            // 
            this.TBNomVL.Location = new System.Drawing.Point(156, 67);
            this.TBNomVL.Name = "TBNomVL";
            this.TBNomVL.Size = new System.Drawing.Size(199, 26);
            this.TBNomVL.TabIndex = 1;
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
            this.label2.Location = new System.Drawing.Point(24, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 20);
            this.label2.TabIndex = 28;
            this.label2.Text = "Nom VLAN";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 20);
            this.label3.TabIndex = 29;
            this.label3.Text = "Numéro VLAN";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(399, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 20);
            this.label4.TabIndex = 33;
            this.label4.Text = "Passerelle";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(399, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 20);
            this.label5.TabIndex = 32;
            this.label5.Text = "Nom du réseau";
            // 
            // TbNomReseau
            // 
            this.TbNomReseau.Location = new System.Drawing.Point(531, 67);
            this.TbNomReseau.Name = "TbNomReseau";
            this.TbNomReseau.Size = new System.Drawing.Size(199, 26);
            this.TbNomReseau.TabIndex = 2;
            // 
            // TBPasserelle
            // 
            this.TBPasserelle.Location = new System.Drawing.Point(531, 110);
            this.TBPasserelle.Name = "TBPasserelle";
            this.TBPasserelle.Size = new System.Drawing.Size(199, 26);
            this.TBPasserelle.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 20);
            this.label6.TabIndex = 34;
            this.label6.Text = "Code Pays";
            // 
            // BtEffacer
            // 
            this.BtEffacer.Location = new System.Drawing.Point(756, 67);
            this.BtEffacer.Name = "BtEffacer";
            this.BtEffacer.Size = new System.Drawing.Size(111, 36);
            this.BtEffacer.TabIndex = 6;
            this.BtEffacer.Text = "Rechercher";
            this.BtEffacer.UseVisualStyleBackColor = true;
            this.BtEffacer.Click += new System.EventHandler(this.button1_Click);
            // 
            // TBCodePays
            // 
            this.TBCodePays.Location = new System.Drawing.Point(156, 159);
            this.TBCodePays.Name = "TBCodePays";
            this.TBCodePays.Size = new System.Drawing.Size(199, 26);
            this.TBCodePays.TabIndex = 5;
            // 
            // dgVLan
            // 
            this.dgVLan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgVLan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVLan.Location = new System.Drawing.Point(28, 210);
            this.dgVLan.Name = "dgVLan";
            this.dgVLan.RowTemplate.Height = 28;
            this.dgVLan.Size = new System.Drawing.Size(1063, 515);
            this.dgVLan.TabIndex = 37;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(756, 108);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 36);
            this.button2.TabIndex = 7;
            this.button2.Text = "Effacer";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormVLan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(1117, 746);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dgVLan);
            this.Controls.Add(this.TBCodePays);
            this.Controls.Add(this.BtEffacer);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TbNomReseau);
            this.Controls.Add(this.TBPasserelle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TBNomVL);
            this.Controls.Add(this.TBNumeVl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormVLan";
            this.Text = "Liste des VLAN";
            this.Load += new System.EventHandler(this.FormVLan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgVLan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TBNumeVl;
        private System.Windows.Forms.TextBox TBNomVL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TbNomReseau;
        private System.Windows.Forms.TextBox TBPasserelle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button BtEffacer;
        private System.Windows.Forms.TextBox TBCodePays;
        private System.Windows.Forms.DataGridView dgVLan;
        private System.Windows.Forms.Button button2;
    }
}