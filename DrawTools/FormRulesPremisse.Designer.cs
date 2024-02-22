namespace DrawTools
{
    partial class FormRulesPremisse
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
            this.tbPremisse = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbValeur = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.bCancel = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            this.cbObjet = new System.Windows.Forms.ComboBox();
            this.cbEvaluation = new System.Windows.Forms.ComboBox();
            this.cbOperation = new System.Windows.Forms.ComboBox();
            this.cbConnecteur = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nom Premisse";
            // 
            // tbPremisse
            // 
            this.tbPremisse.Location = new System.Drawing.Point(92, 6);
            this.tbPremisse.Name = "tbPremisse";
            this.tbPremisse.Size = new System.Drawing.Size(124, 20);
            this.tbPremisse.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Objet";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Evaluation";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Opération";
            // 
            // tbValeur
            // 
            this.tbValeur.Location = new System.Drawing.Point(92, 110);
            this.tbValeur.Name = "tbValeur";
            this.tbValeur.Size = new System.Drawing.Size(124, 20);
            this.tbValeur.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Valeur";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 139);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Connecteur";
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(141, 162);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 12;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(15, 162);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 13;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // cbObjet
            // 
            this.cbObjet.FormattingEnabled = true;
            this.cbObjet.Location = new System.Drawing.Point(92, 31);
            this.cbObjet.Name = "cbObjet";
            this.cbObjet.Size = new System.Drawing.Size(124, 21);
            this.cbObjet.TabIndex = 14;
            this.cbObjet.SelectedIndexChanged+=cbObjet_SelectedIndexChanged;
            // 
            // cbEvaluation
            // 
            this.cbEvaluation.FormattingEnabled = true;
            this.cbEvaluation.Location = new System.Drawing.Point(92, 57);
            this.cbEvaluation.Name = "cbEvaluation";
            this.cbEvaluation.Size = new System.Drawing.Size(124, 21);
            this.cbEvaluation.TabIndex = 15;
            // 
            // cbOperation
            // 
            this.cbOperation.FormattingEnabled = true;
            this.cbOperation.Items.AddRange(new object[] {
            "<",
            "=",
            ">"});
            this.cbOperation.Location = new System.Drawing.Point(92, 83);
            this.cbOperation.Name = "cbOperation";
            this.cbOperation.Size = new System.Drawing.Size(124, 21);
            this.cbOperation.TabIndex = 16;
            // 
            // cbConnecteur
            // 
            this.cbConnecteur.FormattingEnabled = true;
            this.cbConnecteur.Items.AddRange(new object[] {
            "ET",
            "OU"});
            this.cbConnecteur.Location = new System.Drawing.Point(92, 135);
            this.cbConnecteur.Name = "cbConnecteur";
            this.cbConnecteur.Size = new System.Drawing.Size(124, 21);
            this.cbConnecteur.TabIndex = 17;
            // 
            // FormRulesPremisse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 194);
            this.Controls.Add(this.cbConnecteur);
            this.Controls.Add(this.cbOperation);
            this.Controls.Add(this.cbEvaluation);
            this.Controls.Add(this.cbObjet);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbValeur);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbPremisse);
            this.Controls.Add(this.label1);
            this.Name = "FormRulesPremisse";
            this.Text = "FormRulesPremisse";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPremisse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbValeur;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.ComboBox cbObjet;
        private System.Windows.Forms.ComboBox cbEvaluation;
        private System.Windows.Forms.ComboBox cbOperation;
        private System.Windows.Forms.ComboBox cbConnecteur;
    }
}