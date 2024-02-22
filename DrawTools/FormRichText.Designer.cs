namespace DrawTools
{
    partial class FormRichText
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRichText));
            this.rtbText = new System.Windows.Forms.RichTextBox();
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbBold = new System.Windows.Forms.ToolStripButton();
            this.tbItalic = new System.Windows.Forms.ToolStripButton();
            this.tbUnderline = new System.Windows.Forms.ToolStripButton();
            this.tbBullet = new System.Windows.Forms.ToolStripButton();
            this.tcbFont = new System.Windows.Forms.ToolStripComboBox();
            this.tcbSize = new System.Windows.Forms.ToolStripComboBox();
            this.tbImg = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbText
            // 
            this.rtbText.Location = new System.Drawing.Point(12, 42);
            this.rtbText.Name = "rtbText";
            this.rtbText.Size = new System.Drawing.Size(688, 295);
            this.rtbText.TabIndex = 0;
            this.rtbText.Text = "";
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(12, 343);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 1;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(93, 343);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 2;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbBold,
            this.tbItalic,
            this.tbUnderline,
            this.tbBullet,
            this.tcbFont,
            this.tcbSize,
            this.tbImg});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(712, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbBold
            // 
            this.tbBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbBold.Image = ((System.Drawing.Image)(resources.GetObject("tbBold.Image")));
            this.tbBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbBold.Name = "tbBold";
            this.tbBold.Size = new System.Drawing.Size(23, 22);
            this.tbBold.Text = "tbBold";
            // 
            // tbItalic
            // 
            this.tbItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbItalic.Image = ((System.Drawing.Image)(resources.GetObject("tbItalic.Image")));
            this.tbItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbItalic.Name = "tbItalic";
            this.tbItalic.Size = new System.Drawing.Size(23, 22);
            this.tbItalic.Text = "tbItalic";
            // 
            // tbUnderline
            // 
            this.tbUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbUnderline.Image = ((System.Drawing.Image)(resources.GetObject("tbUnderline.Image")));
            this.tbUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbUnderline.Name = "tbUnderline";
            this.tbUnderline.Size = new System.Drawing.Size(23, 22);
            this.tbUnderline.Text = "tbUnderline";
            // 
            // tbBullet
            // 
            this.tbBullet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbBullet.Image = ((System.Drawing.Image)(resources.GetObject("tbBullet.Image")));
            this.tbBullet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbBullet.Name = "tbBullet";
            this.tbBullet.Size = new System.Drawing.Size(23, 22);
            this.tbBullet.Text = "tbBullet";
            // 
            // tcbFont
            // 
            this.tcbFont.Name = "tcbFont";
            this.tcbFont.Size = new System.Drawing.Size(130, 25);
            this.tcbFont.Visible = false;
            // 
            // tcbSize
            // 
            this.tcbSize.Name = "tcbSize";
            this.tcbSize.Size = new System.Drawing.Size(75, 25);
            this.tcbSize.Visible = false;
            // 
            // tbImg
            // 
            this.tbImg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbImg.Image = ((System.Drawing.Image)(resources.GetObject("tbImg.Image")));
            this.tbImg.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbImg.Name = "tbImg";
            this.tbImg.Size = new System.Drawing.Size(23, 22);
            this.tbImg.Text = "tbImg";
            this.tbImg.Visible = false;
            // 
            // FormRichText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 374);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.rtbText);
            this.Name = "FormRichText";
            this.Text = "FormRitchText";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

                                                         

        #endregion




        private System.Windows.Forms.RichTextBox rtbText;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tbBold;
        private System.Windows.Forms.ToolStripButton tbItalic;
        private System.Windows.Forms.ToolStripButton tbUnderline;
        private System.Windows.Forms.ToolStripComboBox tcbFont;
        private System.Windows.Forms.ToolStripComboBox tcbSize;
        private System.Windows.Forms.ToolStripButton tbImg;
        private System.Windows.Forms.ToolStripButton tbBullet;
    }
}