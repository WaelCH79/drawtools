namespace DrawTools
{
    partial class FormTadUpdating
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
            this.cbApp = new System.Windows.Forms.ComboBox();
            this.bOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chkTemplate = new System.Windows.Forms.CheckBox();
            this.lstTad = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bClose = new System.Windows.Forms.Button();
            this.gbTypeDoc = new System.Windows.Forms.GroupBox();
            this.rbCatDoc = new System.Windows.Forms.RadioButton();
            this.rbTadDoc = new System.Windows.Forms.RadioButton();
            this.gbFormat = new System.Windows.Forms.GroupBox();
            this.rbHtmlDoc = new System.Windows.Forms.RadioButton();
            this.rbDocDoc = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.tbVersion = new System.Windows.Forms.TextBox();
            this.bLayer = new System.Windows.Forms.Button();
            this.cbGuid = new System.Windows.Forms.ComboBox();
            this.rtCommentaire = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbVersion = new System.Windows.Forms.ComboBox();
            this.lChemin = new System.Windows.Forms.Label();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.epCommentaire = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbTypeDoc.SuspendLayout();
            this.gbFormat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epCommentaire)).BeginInit();
            this.SuspendLayout();
            // 
            // cbApp
            // 
            this.cbApp.CausesValidation = false;
            this.cbApp.FormattingEnabled = true;
            this.cbApp.Location = new System.Drawing.Point(17, 171);
            this.cbApp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbApp.Name = "cbApp";
            this.cbApp.Size = new System.Drawing.Size(540, 28);
            this.cbApp.TabIndex = 0;
            this.cbApp.SelectedIndexChanged += new System.EventHandler(this.cbApp_SelectedIndexChanged);
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(22, 709);
            this.bOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(112, 35);
            this.bOK.TabIndex = 1;
            this.bOK.Text = "Execute";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 145);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Application";
            // 
            // chkTemplate
            // 
            this.chkTemplate.AutoSize = true;
            this.chkTemplate.CausesValidation = false;
            this.chkTemplate.Location = new System.Drawing.Point(18, 362);
            this.chkTemplate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkTemplate.Name = "chkTemplate";
            this.chkTemplate.Size = new System.Drawing.Size(138, 24);
            this.chkTemplate.TabIndex = 3;
            this.chkTemplate.Text = "From template";
            this.chkTemplate.UseVisualStyleBackColor = true;
            this.chkTemplate.CheckedChanged += new System.EventHandler(this.chkTemplate_CheckedChanged);
            // 
            // lstTad
            // 
            this.lstTad.CausesValidation = false;
            this.lstTad.FormattingEnabled = true;
            this.lstTad.ItemHeight = 20;
            this.lstTad.Location = new System.Drawing.Point(179, 391);
            this.lstTad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstTad.Name = "lstTad";
            this.lstTad.Size = new System.Drawing.Size(378, 104);
            this.lstTad.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(174, 362);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "From existing documents";
            // 
            // bClose
            // 
            this.bClose.CausesValidation = false;
            this.bClose.Location = new System.Drawing.Point(368, 709);
            this.bClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(112, 35);
            this.bClose.TabIndex = 6;
            this.bClose.Text = "Quitter";
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // gbTypeDoc
            // 
            this.gbTypeDoc.Controls.Add(this.rbCatDoc);
            this.gbTypeDoc.Controls.Add(this.rbTadDoc);
            this.gbTypeDoc.Location = new System.Drawing.Point(22, 19);
            this.gbTypeDoc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbTypeDoc.Name = "gbTypeDoc";
            this.gbTypeDoc.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbTypeDoc.Size = new System.Drawing.Size(258, 112);
            this.gbTypeDoc.TabIndex = 7;
            this.gbTypeDoc.TabStop = false;
            this.gbTypeDoc.Text = "Type Document";
            // 
            // rbCatDoc
            // 
            this.rbCatDoc.AutoSize = true;
            this.rbCatDoc.Location = new System.Drawing.Point(10, 65);
            this.rbCatDoc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbCatDoc.Name = "rbCatDoc";
            this.rbCatDoc.Size = new System.Drawing.Size(143, 24);
            this.rbCatDoc.TabIndex = 1;
            this.rbCatDoc.TabStop = true;
            this.rbCatDoc.Text = "CAT Document";
            this.rbCatDoc.UseVisualStyleBackColor = true;
            this.rbCatDoc.CheckedChanged += new System.EventHandler(this.rbCatDoc_CheckedChanged);
            // 
            // rbTadDoc
            // 
            this.rbTadDoc.AutoSize = true;
            this.rbTadDoc.Location = new System.Drawing.Point(9, 29);
            this.rbTadDoc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbTadDoc.Name = "rbTadDoc";
            this.rbTadDoc.Size = new System.Drawing.Size(144, 24);
            this.rbTadDoc.TabIndex = 0;
            this.rbTadDoc.TabStop = true;
            this.rbTadDoc.Text = "TAD Document";
            this.rbTadDoc.UseVisualStyleBackColor = true;
            this.rbTadDoc.CheckedChanged += new System.EventHandler(this.rbTadDoc_CheckedChanged);
            // 
            // gbFormat
            // 
            this.gbFormat.Controls.Add(this.rbHtmlDoc);
            this.gbFormat.Controls.Add(this.rbDocDoc);
            this.gbFormat.Location = new System.Drawing.Point(289, 19);
            this.gbFormat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbFormat.Name = "gbFormat";
            this.gbFormat.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbFormat.Size = new System.Drawing.Size(269, 112);
            this.gbFormat.TabIndex = 8;
            this.gbFormat.TabStop = false;
            this.gbFormat.Text = "Format Document";
            // 
            // rbHtmlDoc
            // 
            this.rbHtmlDoc.AutoSize = true;
            this.rbHtmlDoc.Location = new System.Drawing.Point(9, 65);
            this.rbHtmlDoc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbHtmlDoc.Name = "rbHtmlDoc";
            this.rbHtmlDoc.Size = new System.Drawing.Size(145, 24);
            this.rbHtmlDoc.TabIndex = 1;
            this.rbHtmlDoc.TabStop = true;
            this.rbHtmlDoc.Text = "Html Document";
            this.rbHtmlDoc.UseVisualStyleBackColor = true;
            this.rbHtmlDoc.CheckedChanged += new System.EventHandler(this.rbHtmlDoc_CheckedChanged);
            // 
            // rbDocDoc
            // 
            this.rbDocDoc.AutoSize = true;
            this.rbDocDoc.Location = new System.Drawing.Point(9, 29);
            this.rbDocDoc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbDocDoc.Name = "rbDocDoc";
            this.rbDocDoc.Size = new System.Drawing.Size(141, 24);
            this.rbDocDoc.TabIndex = 0;
            this.rbDocDoc.TabStop = true;
            this.rbDocDoc.Text = "Doc Document";
            this.rbDocDoc.UseVisualStyleBackColor = true;
            this.rbDocDoc.CheckedChanged += new System.EventHandler(this.rbDocDoc_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 214);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Version Application";
            // 
            // tbVersion
            // 
            this.tbVersion.Location = new System.Drawing.Point(408, 240);
            this.tbVersion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbVersion.Name = "tbVersion";
            this.tbVersion.Size = new System.Drawing.Size(148, 26);
            this.tbVersion.TabIndex = 10;
            // 
            // bLayer
            // 
            this.bLayer.Location = new System.Drawing.Point(339, 235);
            this.bLayer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bLayer.Name = "bLayer";
            this.bLayer.Size = new System.Drawing.Size(42, 35);
            this.bLayer.TabIndex = 11;
            this.bLayer.Text = "+";
            this.bLayer.UseVisualStyleBackColor = true;
            this.bLayer.Click += new System.EventHandler(this.bLayer_Click);
            // 
            // cbGuid
            // 
            this.cbGuid.CausesValidation = false;
            this.cbGuid.FormattingEnabled = true;
            this.cbGuid.Location = new System.Drawing.Point(117, 152);
            this.cbGuid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbGuid.Name = "cbGuid";
            this.cbGuid.Size = new System.Drawing.Size(187, 28);
            this.cbGuid.TabIndex = 12;
            this.cbGuid.Visible = false;
            // 
            // rtCommentaire
            // 
            this.epCommentaire.SetError(this.rtCommentaire, "Le champs commentaire est obligatoire");
            this.rtCommentaire.Location = new System.Drawing.Point(18, 535);
            this.rtCommentaire.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rtCommentaire.Name = "rtCommentaire";
            this.rtCommentaire.Size = new System.Drawing.Size(538, 149);
            this.rtCommentaire.TabIndex = 13;
            this.rtCommentaire.Text = "";
            this.rtCommentaire.Validating += new System.ComponentModel.CancelEventHandler(this.rtCommentaire_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 508);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Commentaire";
            // 
            // cbVersion
            // 
            this.cbVersion.CausesValidation = false;
            this.cbVersion.FormattingEnabled = true;
            this.cbVersion.Location = new System.Drawing.Point(18, 239);
            this.cbVersion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbVersion.Name = "cbVersion";
            this.cbVersion.Size = new System.Drawing.Size(316, 28);
            this.cbVersion.TabIndex = 15;
            this.cbVersion.SelectedIndexChanged += new System.EventHandler(this.cbVersion_SelectedIndexChanged);
            // 
            // lChemin
            // 
            this.lChemin.AutoSize = true;
            this.lChemin.Location = new System.Drawing.Point(18, 286);
            this.lChemin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lChemin.Name = "lChemin";
            this.lChemin.Size = new System.Drawing.Size(42, 20);
            this.lChemin.TabIndex = 16;
            this.lChemin.Text = "Path";
            // 
            // tbPath
            // 
            this.tbPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPath.Location = new System.Drawing.Point(17, 311);
            this.tbPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbPath.Name = "tbPath";
            this.tbPath.ReadOnly = true;
            this.tbPath.Size = new System.Drawing.Size(541, 19);
            this.tbPath.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(405, 214);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 20);
            this.label5.TabIndex = 18;
            this.label5.Text = "Version gen";
            // 
            // epCommentaire
            // 
            this.epCommentaire.ContainerControl = this;
            // 
            // FormTadUpdating
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 760);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.lChemin);
            this.Controls.Add(this.cbVersion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rtCommentaire);
            this.Controls.Add(this.cbGuid);
            this.Controls.Add(this.bLayer);
            this.Controls.Add(this.tbVersion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gbFormat);
            this.Controls.Add(this.gbTypeDoc);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstTad);
            this.Controls.Add(this.chkTemplate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.cbApp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTadUpdating";
            this.Text = "Le champ commentaire est obligatoire";
            this.gbTypeDoc.ResumeLayout(false);
            this.gbTypeDoc.PerformLayout();
            this.gbFormat.ResumeLayout(false);
            this.gbFormat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epCommentaire)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        
        #endregion

        private System.Windows.Forms.ComboBox cbApp;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkTemplate;
        private System.Windows.Forms.ListBox lstTad;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.GroupBox gbTypeDoc;
        private System.Windows.Forms.RadioButton rbCatDoc;
        private System.Windows.Forms.RadioButton rbTadDoc;
        private System.Windows.Forms.GroupBox gbFormat;
        private System.Windows.Forms.RadioButton rbHtmlDoc;
        private System.Windows.Forms.RadioButton rbDocDoc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbVersion;
        private System.Windows.Forms.Button bLayer;
        private System.Windows.Forms.ComboBox cbGuid;
        private System.Windows.Forms.RichTextBox rtCommentaire;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbVersion;
        private System.Windows.Forms.Label lChemin;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ErrorProvider epCommentaire;
    }
}