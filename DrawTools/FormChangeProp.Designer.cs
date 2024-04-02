namespace DrawTools
{
    partial class FormChangeProp
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
            this.lLinkApp = new System.Windows.Forms.ListBox();
            this.lLinkAppAdd = new System.Windows.Forms.ListBox();
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.bAdd = new System.Windows.Forms.Button();
            this.bSup = new System.Windows.Forms.Button();
            this.tbProp = new System.Windows.Forms.TextBox();
            this.bAddProp = new System.Windows.Forms.Button();
            this.bDelProp = new System.Windows.Forms.Button();
            this.cbAlias = new System.Windows.Forms.ComboBox();
            this.cbGuidAlias = new System.Windows.Forms.ComboBox();
            this.tvCadreRef = new System.Windows.Forms.TreeView();
            this.ckbDefaultLayer = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lLinkApp
            // 
            this.lLinkApp.FormattingEnabled = true;
            this.lLinkApp.ItemHeight = 20;
            this.lLinkApp.Location = new System.Drawing.Point(18, 58);
            this.lLinkApp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lLinkApp.Name = "lLinkApp";
            this.lLinkApp.Size = new System.Drawing.Size(333, 344);
            this.lLinkApp.TabIndex = 0;
            // 
            // lLinkAppAdd
            // 
            this.lLinkAppAdd.FormattingEnabled = true;
            this.lLinkAppAdd.ItemHeight = 20;
            this.lLinkAppAdd.Location = new System.Drawing.Point(509, 58);
            this.lLinkAppAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lLinkAppAdd.Name = "lLinkAppAdd";
            this.lLinkAppAdd.Size = new System.Drawing.Size(353, 344);
            this.lLinkAppAdd.TabIndex = 1;
            this.lLinkAppAdd.SelectedIndexChanged += new System.EventHandler(this.lLinkAppAdd_SelectedIndexChanged);
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(637, 425);
            this.OK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(99, 35);
            this.OK.TabIndex = 2;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(750, 425);
            this.Cancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(99, 35);
            this.Cancel.TabIndex = 3;
            this.Cancel.Text = "Annuler";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // bAdd
            // 
            this.bAdd.Location = new System.Drawing.Point(393, 128);
            this.bAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(82, 35);
            this.bAdd.TabIndex = 4;
            this.bAdd.Text = ">>";
            this.bAdd.UseVisualStyleBackColor = true;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // bSup
            // 
            this.bSup.Location = new System.Drawing.Point(393, 172);
            this.bSup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bSup.Name = "bSup";
            this.bSup.Size = new System.Drawing.Size(82, 35);
            this.bSup.TabIndex = 5;
            this.bSup.Text = "<<";
            this.bSup.UseVisualStyleBackColor = true;
            this.bSup.Click += new System.EventHandler(this.bSup_Click);
            // 
            // tbProp
            // 
            this.tbProp.Location = new System.Drawing.Point(18, 18);
            this.tbProp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbProp.Name = "tbProp";
            this.tbProp.Size = new System.Drawing.Size(168, 26);
            this.tbProp.TabIndex = 6;
            this.tbProp.TextChanged += new System.EventHandler(this.tbProp_TextChanged);
            // 
            // bAddProp
            // 
            this.bAddProp.Enabled = false;
            this.bAddProp.Location = new System.Drawing.Point(196, 15);
            this.bAddProp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bAddProp.Name = "bAddProp";
            this.bAddProp.Size = new System.Drawing.Size(33, 35);
            this.bAddProp.TabIndex = 7;
            this.bAddProp.Text = "+";
            this.bAddProp.UseVisualStyleBackColor = true;
            this.bAddProp.Click += new System.EventHandler(this.bAddProp_Click);
            // 
            // bDelProp
            // 
            this.bDelProp.Enabled = false;
            this.bDelProp.Location = new System.Drawing.Point(228, 15);
            this.bDelProp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bDelProp.Name = "bDelProp";
            this.bDelProp.Size = new System.Drawing.Size(33, 35);
            this.bDelProp.TabIndex = 8;
            this.bDelProp.Text = "-";
            this.bDelProp.UseVisualStyleBackColor = true;
            this.bDelProp.Click += new System.EventHandler(this.bDelProp_Click);
            // 
            // cbAlias
            // 
            this.cbAlias.FormattingEnabled = true;
            this.cbAlias.Location = new System.Drawing.Point(30, 26);
            this.cbAlias.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbAlias.Name = "cbAlias";
            this.cbAlias.Size = new System.Drawing.Size(240, 28);
            this.cbAlias.TabIndex = 9;
            this.cbAlias.Visible = false;
            // 
            // cbGuidAlias
            // 
            this.cbGuidAlias.FormattingEnabled = true;
            this.cbGuidAlias.Location = new System.Drawing.Point(272, 17);
            this.cbGuidAlias.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbGuidAlias.Name = "cbGuidAlias";
            this.cbGuidAlias.Size = new System.Drawing.Size(79, 28);
            this.cbGuidAlias.TabIndex = 10;
            this.cbGuidAlias.Visible = false;
            // 
            // tvCadreRef
            // 
            this.tvCadreRef.Location = new System.Drawing.Point(20, 18);
            this.tvCadreRef.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tvCadreRef.Name = "tvCadreRef";
            this.tvCadreRef.Size = new System.Drawing.Size(331, 384);
            this.tvCadreRef.TabIndex = 11;
            this.tvCadreRef.Visible = false;
            // 
            // ckbDefaultLayer
            // 
            this.ckbDefaultLayer.AutoSize = true;
            this.ckbDefaultLayer.Location = new System.Drawing.Point(529, 20);
            this.ckbDefaultLayer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ckbDefaultLayer.Name = "ckbDefaultLayer";
            this.ckbDefaultLayer.Size = new System.Drawing.Size(130, 24);
            this.ckbDefaultLayer.TabIndex = 12;
            this.ckbDefaultLayer.Text = "Default Layer";
            this.ckbDefaultLayer.UseVisualStyleBackColor = true;
            this.ckbDefaultLayer.Visible = false;
            // 
            // FormChangeProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 477);
            this.Controls.Add(this.ckbDefaultLayer);
            this.Controls.Add(this.cbGuidAlias);
            this.Controls.Add(this.cbAlias);
            this.Controls.Add(this.bDelProp);
            this.Controls.Add(this.bAddProp);
            this.Controls.Add(this.tbProp);
            this.Controls.Add(this.bSup);
            this.Controls.Add(this.bAdd);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.lLinkAppAdd);
            this.Controls.Add(this.lLinkApp);
            this.Controls.Add(this.tvCadreRef);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormChangeProp";
            this.Text = "LinkApp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lLinkApp;
        private System.Windows.Forms.ListBox lLinkAppAdd;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.Button bSup;
        private System.Windows.Forms.TextBox tbProp;
        private System.Windows.Forms.Button bAddProp;
        private System.Windows.Forms.Button bDelProp;
        private System.Windows.Forms.ComboBox cbAlias;
        private System.Windows.Forms.ComboBox cbGuidAlias;
        private System.Windows.Forms.TreeView tvCadreRef;
        private System.Windows.Forms.CheckBox ckbDefaultLayer;
    }
}