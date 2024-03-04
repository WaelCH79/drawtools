namespace DrawTools
{
    partial class FormApplicationList
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
            this.txt_ApplicationSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnNewApp = new System.Windows.Forms.Button();
            this.pDataGV = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // txt_ApplicationSearch
            // 
            this.txt_ApplicationSearch.Location = new System.Drawing.Point(26, 112);
            this.txt_ApplicationSearch.Name = "txt_ApplicationSearch";
            this.txt_ApplicationSearch.Size = new System.Drawing.Size(254, 26);
            this.txt_ApplicationSearch.TabIndex = 1;
            this.txt_ApplicationSearch.TextChanged += new System.EventHandler(this.txt_ApplicationSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Rechercher";
            // 
            // BtnNewApp
            // 
            this.BtnNewApp.Location = new System.Drawing.Point(26, 12);
            this.BtnNewApp.Name = "BtnNewApp";
            this.BtnNewApp.Size = new System.Drawing.Size(274, 35);
            this.BtnNewApp.TabIndex = 3;
            this.BtnNewApp.Text = "Créer une nouvelle application";
            this.BtnNewApp.UseVisualStyleBackColor = true;
            this.BtnNewApp.Click += new System.EventHandler(this.BtnNewApp_Click);
            // 
            // pDataGV
            // 
            this.pDataGV.AutoSize = true;
            this.pDataGV.Location = new System.Drawing.Point(26, 169);
            this.pDataGV.Name = "pDataGV";
            this.pDataGV.Size = new System.Drawing.Size(0, 0);
            this.pDataGV.TabIndex = 4;
            // 
            // FormApplicationList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 664);
            this.ControlBox = false;
            this.Controls.Add(this.pDataGV);
            this.Controls.Add(this.BtnNewApp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_ApplicationSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormApplicationList";
            this.Text = "Liste des applications";
            this.Load += new System.EventHandler(this.FormApplicationList_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txt_ApplicationSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnNewApp;
        private System.Windows.Forms.Panel pDataGV;
    }
}