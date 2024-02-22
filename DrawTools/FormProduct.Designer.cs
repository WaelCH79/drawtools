namespace DrawTools
{
    partial class FormProduct
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
            this.tGuidCadreRefFonc = new System.Windows.Forms.TextBox();
            this.tGuidMainComposant = new System.Windows.Forms.TextBox();
            this.tEditeur = new System.Windows.Forms.TextBox();
            this.tNom = new System.Windows.Forms.TextBox();
            this.bUp = new System.Windows.Forms.Button();
            this.dgProduit = new System.Windows.Forms.DataGridView();
            this.GuidProduit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomProduit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Editeur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GuidCadreRef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbTechnoArea = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgTechnoRef = new System.Windows.Forms.DataGridView();
            this.GuidTechnoRef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomTechnoRef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NormeG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Norme = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IndexImgOS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GuidProduitE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImgOs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Indicator = new System.Windows.Forms.DataGridViewButtonColumn();
            this.UpComingStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpComingEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReferenceStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReferenceEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConfinedStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConfinedEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DecommStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DecommEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.bImpPdt = new System.Windows.Forms.Button();
            this.bCalcIndicator = new System.Windows.Forms.Button();
            this.bVersion = new System.Windows.Forms.Button();
            this.bCatalogue = new System.Windows.Forms.Button();
            this.bImpTech = new System.Windows.Forms.Button();
            this.bAdd = new System.Windows.Forms.Button();
            this.bExport = new System.Windows.Forms.Button();
            this.tbNewCadreRef = new System.Windows.Forms.TextBox();
            this.tvCadreRef = new System.Windows.Forms.TreeView();
            this.fileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgProduit)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTechnoRef)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tGuidCadreRefFonc);
            this.groupBox1.Controls.Add(this.tGuidMainComposant);
            this.groupBox1.Controls.Add(this.tEditeur);
            this.groupBox1.Controls.Add(this.tNom);
            this.groupBox1.Controls.Add(this.bUp);
            this.groupBox1.Controls.Add(this.dgProduit);
            this.groupBox1.Location = new System.Drawing.Point(639, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(644, 393);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "disable";
            this.groupBox1.Text = "Produit";
            // 
            // tGuidCadreRefFonc
            // 
            this.tGuidCadreRefFonc.Location = new System.Drawing.Point(359, 314);
            this.tGuidCadreRefFonc.Margin = new System.Windows.Forms.Padding(4);
            this.tGuidCadreRefFonc.Name = "tGuidCadreRefFonc";
            this.tGuidCadreRefFonc.Size = new System.Drawing.Size(191, 22);
            this.tGuidCadreRefFonc.TabIndex = 5;
            this.tGuidCadreRefFonc.Visible = false;
            // 
            // tGuidMainComposant
            // 
            this.tGuidMainComposant.Location = new System.Drawing.Point(83, 314);
            this.tGuidMainComposant.Margin = new System.Windows.Forms.Padding(4);
            this.tGuidMainComposant.Name = "tGuidMainComposant";
            this.tGuidMainComposant.Size = new System.Drawing.Size(216, 22);
            this.tGuidMainComposant.TabIndex = 4;
            this.tGuidMainComposant.Visible = false;
            // 
            // tEditeur
            // 
            this.tEditeur.Location = new System.Drawing.Point(352, 356);
            this.tEditeur.Margin = new System.Windows.Forms.Padding(4);
            this.tEditeur.Name = "tEditeur";
            this.tEditeur.Size = new System.Drawing.Size(284, 22);
            this.tEditeur.TabIndex = 3;
            this.tEditeur.Visible = false;
            // 
            // tNom
            // 
            this.tNom.Location = new System.Drawing.Point(56, 356);
            this.tNom.Margin = new System.Windows.Forms.Padding(4);
            this.tNom.Name = "tNom";
            this.tNom.Size = new System.Drawing.Size(288, 22);
            this.tNom.TabIndex = 2;
            // 
            // bUp
            // 
            this.bUp.Location = new System.Drawing.Point(13, 355);
            this.bUp.Margin = new System.Windows.Forms.Padding(4);
            this.bUp.Name = "bUp";
            this.bUp.Size = new System.Drawing.Size(35, 25);
            this.bUp.TabIndex = 1;
            this.bUp.Text = "<";
            this.bUp.UseVisualStyleBackColor = true;
            this.bUp.Click += new System.EventHandler(this.bUp_Click);
            // 
            // dgProduit
            // 
            this.dgProduit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgProduit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GuidProduit,
            this.NomProduit,
            this.Editeur,
            this.Cat,
            this.GuidCadreRef,
            this.cbTechnoArea});
            this.dgProduit.Location = new System.Drawing.Point(13, 21);
            this.dgProduit.Margin = new System.Windows.Forms.Padding(4);
            this.dgProduit.MultiSelect = false;
            this.dgProduit.Name = "dgProduit";
            this.dgProduit.ReadOnly = true;
            this.dgProduit.RowHeadersWidth = 31;
            this.dgProduit.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgProduit.Size = new System.Drawing.Size(623, 329);
            this.dgProduit.TabIndex = 0;
            this.dgProduit.Tag = "";
            this.dgProduit.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgProduit_RowsAdded);
            this.dgProduit.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgProduit_RowValidating);
            this.dgProduit.SelectionChanged += new System.EventHandler(this.dgProduit_SelectionChanged);
            // 
            // GuidProduit
            // 
            this.GuidProduit.HeaderText = "Guid";
            this.GuidProduit.Name = "GuidProduit";
            this.GuidProduit.ToolTipText = "Guid";
            this.GuidProduit.Visible = false;
            // 
            // NomProduit
            // 
            this.NomProduit.HeaderText = "Nom";
            this.NomProduit.Name = "NomProduit";
            this.NomProduit.ToolTipText = "Nom";
            this.NomProduit.Width = 180;
            // 
            // Editeur
            // 
            this.Editeur.HeaderText = "Editeur";
            this.Editeur.Name = "Editeur";
            this.Editeur.ToolTipText = "Editeur";
            this.Editeur.Width = 140;
            // 
            // Cat
            // 
            this.Cat.HeaderText = "Cat";
            this.Cat.Name = "Cat";
            this.Cat.ToolTipText = "Catalogue";
            this.Cat.Width = 30;
            // 
            // GuidCadreRef
            // 
            this.GuidCadreRef.HeaderText = "GuidCadreRef";
            this.GuidCadreRef.Name = "GuidCadreRef";
            this.GuidCadreRef.ToolTipText = "GuidCadreRef";
            this.GuidCadreRef.Visible = false;
            // 
            // cbTechnoArea
            // 
            this.cbTechnoArea.HeaderText = "TechnoArea";
            this.cbTechnoArea.Name = "cbTechnoArea";
            this.cbTechnoArea.ToolTipText = "GuidTechnoArea";
            this.cbTechnoArea.Width = 220;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgTechnoRef);
            this.groupBox2.Location = new System.Drawing.Point(11, 401);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1272, 198);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Technologie";
            // 
            // dgTechnoRef
            // 
            this.dgTechnoRef.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgTechnoRef.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GuidTechnoRef,
            this.NomTechnoRef,
            this.Version,
            this.NormeG,
            this.Norme,
            this.IndexImgOS,
            this.GuidProduitE,
            this.ImgOs,
            this.Indicator,
            this.UpComingStart,
            this.UpComingEnd,
            this.ReferenceStart,
            this.ReferenceEnd,
            this.ConfinedStart,
            this.ConfinedEnd,
            this.DecommStart,
            this.DecommEnd});
            this.dgTechnoRef.Location = new System.Drawing.Point(9, 25);
            this.dgTechnoRef.Margin = new System.Windows.Forms.Padding(4);
            this.dgTechnoRef.Name = "dgTechnoRef";
            this.dgTechnoRef.RowHeadersWidth = 24;
            this.dgTechnoRef.Size = new System.Drawing.Size(1255, 162);
            this.dgTechnoRef.TabIndex = 0;
            this.dgTechnoRef.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgTechnoRef_CellClick);
            this.dgTechnoRef.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgTechnoRef_RowsAdded);
            this.dgTechnoRef.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgTechnoRef_RowValidating);
            // 
            // GuidTechnoRef
            // 
            this.GuidTechnoRef.HeaderText = "Guid";
            this.GuidTechnoRef.Name = "GuidTechnoRef";
            this.GuidTechnoRef.ToolTipText = "GuidTechnoRef";
            this.GuidTechnoRef.Visible = false;
            // 
            // NomTechnoRef
            // 
            this.NomTechnoRef.HeaderText = "Nom";
            this.NomTechnoRef.Name = "NomTechnoRef";
            this.NomTechnoRef.ToolTipText = "NomTechnoRef";
            this.NomTechnoRef.Width = 200;
            // 
            // Version
            // 
            this.Version.HeaderText = "Version";
            this.Version.Name = "Version";
            this.Version.ToolTipText = "Version";
            this.Version.Width = 60;
            // 
            // NormeG
            // 
            this.NormeG.HeaderText = "NrG";
            this.NormeG.Name = "NormeG";
            this.NormeG.ToolTipText = "NormeG";
            this.NormeG.Width = 30;
            // 
            // Norme
            // 
            this.Norme.HeaderText = "Nr";
            this.Norme.Name = "Norme";
            this.Norme.ToolTipText = "Norme";
            this.Norme.Width = 30;
            // 
            // IndexImgOS
            // 
            this.IndexImgOS.HeaderText = "OS";
            this.IndexImgOS.Name = "IndexImgOS";
            this.IndexImgOS.ToolTipText = "IndexImgOS";
            this.IndexImgOS.Width = 30;
            // 
            // GuidProduitE
            // 
            this.GuidProduitE.HeaderText = "GuidProduit";
            this.GuidProduitE.Name = "GuidProduitE";
            this.GuidProduitE.ToolTipText = "GuidProduit";
            this.GuidProduitE.Visible = false;
            // 
            // ImgOs
            // 
            this.ImgOs.HeaderText = "ImgOS";
            this.ImgOs.Name = "ImgOs";
            this.ImgOs.Visible = false;
            // 
            // Indicator
            // 
            this.Indicator.HeaderText = "Ind";
            this.Indicator.Name = "Indicator";
            this.Indicator.Width = 30;
            // 
            // UpComingStart
            // 
            this.UpComingStart.HeaderText = "UpComS";
            this.UpComingStart.Name = "UpComingStart";
            this.UpComingStart.ToolTipText = "UpComingStart";
            this.UpComingStart.Width = 66;
            // 
            // UpComingEnd
            // 
            this.UpComingEnd.HeaderText = "UpComE";
            this.UpComingEnd.Name = "UpComingEnd";
            this.UpComingEnd.ToolTipText = "UpComingEnd";
            this.UpComingEnd.Width = 66;
            // 
            // ReferenceStart
            // 
            this.ReferenceStart.HeaderText = "RefS";
            this.ReferenceStart.Name = "ReferenceStart";
            this.ReferenceStart.ToolTipText = "ReferenceStart";
            this.ReferenceStart.Width = 66;
            // 
            // ReferenceEnd
            // 
            this.ReferenceEnd.HeaderText = "RefE";
            this.ReferenceEnd.Name = "ReferenceEnd";
            this.ReferenceEnd.ToolTipText = "ReferenceEnd";
            this.ReferenceEnd.Width = 66;
            // 
            // ConfinedStart
            // 
            this.ConfinedStart.HeaderText = "ConfinedS";
            this.ConfinedStart.Name = "ConfinedStart";
            this.ConfinedStart.ToolTipText = "ConfinedStart";
            this.ConfinedStart.Width = 66;
            // 
            // ConfinedEnd
            // 
            this.ConfinedEnd.HeaderText = "ConfinedE";
            this.ConfinedEnd.Name = "ConfinedEnd";
            this.ConfinedEnd.ToolTipText = "ConfinedEnd";
            this.ConfinedEnd.Width = 66;
            // 
            // DecommStart
            // 
            this.DecommStart.HeaderText = "DecommS";
            this.DecommStart.Name = "DecommStart";
            this.DecommStart.ToolTipText = "DecommStart";
            this.DecommStart.Width = 66;
            // 
            // DecommEnd
            // 
            this.DecommEnd.HeaderText = "DecommE";
            this.DecommEnd.Name = "DecommEnd";
            this.DecommEnd.ToolTipText = "DecommEnd";
            this.DecommEnd.Width = 66;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.bImpPdt);
            this.groupBox3.Controls.Add(this.bCalcIndicator);
            this.groupBox3.Controls.Add(this.bVersion);
            this.groupBox3.Controls.Add(this.bCatalogue);
            this.groupBox3.Controls.Add(this.bImpTech);
            this.groupBox3.Controls.Add(this.bAdd);
            this.groupBox3.Controls.Add(this.bExport);
            this.groupBox3.Controls.Add(this.tbNewCadreRef);
            this.groupBox3.Controls.Add(this.tvCadreRef);
            this.groupBox3.Location = new System.Drawing.Point(11, 7);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(620, 393);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Cadre de Référence";
            // 
            // bImpPdt
            // 
            this.bImpPdt.Location = new System.Drawing.Point(295, 21);
            this.bImpPdt.Margin = new System.Windows.Forms.Padding(4);
            this.bImpPdt.Name = "bImpPdt";
            this.bImpPdt.Size = new System.Drawing.Size(82, 26);
            this.bImpPdt.TabIndex = 8;
            this.bImpPdt.Text = "Imp. Pdt";
            this.bImpPdt.UseVisualStyleBackColor = true;
            // 
            // bCalcIndicator
            // 
            this.bCalcIndicator.Location = new System.Drawing.Point(540, 21);
            this.bCalcIndicator.Margin = new System.Windows.Forms.Padding(4);
            this.bCalcIndicator.Name = "bCalcIndicator";
            this.bCalcIndicator.Size = new System.Drawing.Size(72, 26);
            this.bCalcIndicator.TabIndex = 7;
            this.bCalcIndicator.Text = "Indicator";
            this.bCalcIndicator.UseVisualStyleBackColor = true;
            // 
            // bVersion
            // 
            this.bVersion.Location = new System.Drawing.Point(500, 21);
            this.bVersion.Margin = new System.Windows.Forms.Padding(4);
            this.bVersion.Name = "bVersion";
            this.bVersion.Size = new System.Drawing.Size(40, 26);
            this.bVersion.TabIndex = 6;
            this.bVersion.Text = "VN";
            this.bVersion.UseVisualStyleBackColor = true;
            // 
            // bCatalogue
            // 
            this.bCatalogue.Location = new System.Drawing.Point(460, 21);
            this.bCatalogue.Margin = new System.Windows.Forms.Padding(4);
            this.bCatalogue.Name = "bCatalogue";
            this.bCatalogue.Size = new System.Drawing.Size(39, 26);
            this.bCatalogue.TabIndex = 5;
            this.bCatalogue.Text = "Cat";
            this.bCatalogue.UseVisualStyleBackColor = true;
            // 
            // bImpTech
            // 
            this.bImpTech.Location = new System.Drawing.Point(377, 21);
            this.bImpTech.Margin = new System.Windows.Forms.Padding(4);
            this.bImpTech.Name = "bImpTech";
            this.bImpTech.Size = new System.Drawing.Size(82, 26);
            this.bImpTech.TabIndex = 4;
            this.bImpTech.Text = "Imp. Tech.";
            this.bImpTech.UseVisualStyleBackColor = true;
            // 
            // bAdd
            // 
            this.bAdd.Location = new System.Drawing.Point(186, 21);
            this.bAdd.Margin = new System.Windows.Forms.Padding(4);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(48, 26);
            this.bAdd.TabIndex = 1;
            this.bAdd.Text = "Add";
            this.bAdd.UseVisualStyleBackColor = true;
            // 
            // bExport
            // 
            this.bExport.Location = new System.Drawing.Point(235, 21);
            this.bExport.Margin = new System.Windows.Forms.Padding(4);
            this.bExport.Name = "bExport";
            this.bExport.Size = new System.Drawing.Size(60, 26);
            this.bExport.TabIndex = 3;
            this.bExport.Text = "Export";
            this.bExport.UseVisualStyleBackColor = true;
            // 
            // tbNewCadreRef
            // 
            this.tbNewCadreRef.Location = new System.Drawing.Point(9, 23);
            this.tbNewCadreRef.Margin = new System.Windows.Forms.Padding(4);
            this.tbNewCadreRef.Name = "tbNewCadreRef";
            this.tbNewCadreRef.Size = new System.Drawing.Size(169, 22);
            this.tbNewCadreRef.TabIndex = 2;
            // 
            // tvCadreRef
            // 
            this.tvCadreRef.Location = new System.Drawing.Point(8, 54);
            this.tvCadreRef.Margin = new System.Windows.Forms.Padding(4);
            this.tvCadreRef.Name = "tvCadreRef";
            this.tvCadreRef.Size = new System.Drawing.Size(604, 328);
            this.tvCadreRef.TabIndex = 0;
            // 
            // FormProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1303, 612);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormProduct";
            this.Text = "FormProduct";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgProduit)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgTechnoRef)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.TreeView tvCadreRef;
        private System.Windows.Forms.TextBox tbNewCadreRef;
        private System.Windows.Forms.DataGridView dgProduit;
        private System.Windows.Forms.DataGridView dgTechnoRef;
        private System.Windows.Forms.TextBox tGuidCadreRefFonc;
        private System.Windows.Forms.TextBox tGuidMainComposant;
        private System.Windows.Forms.TextBox tEditeur;
        private System.Windows.Forms.TextBox tNom;
        private System.Windows.Forms.Button bUp;
        private System.Windows.Forms.Button bExport;
        private System.Windows.Forms.Button bImpTech;
        private System.Windows.Forms.OpenFileDialog fileDialog1;
        private System.Windows.Forms.Button bCatalogue;
        private System.Windows.Forms.Button bVersion;
        private System.Windows.Forms.Button bCalcIndicator;
        private System.Windows.Forms.DataGridViewTextBoxColumn GuidProduit;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomProduit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Editeur;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cat;
        private System.Windows.Forms.DataGridViewTextBoxColumn GuidCadreRef;
        private System.Windows.Forms.DataGridViewComboBoxColumn cbTechnoArea;
        private System.Windows.Forms.DataGridViewTextBoxColumn GuidTechnoRef;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomTechnoRef;
        private System.Windows.Forms.DataGridViewTextBoxColumn Version;
        private System.Windows.Forms.DataGridViewTextBoxColumn NormeG;
        private System.Windows.Forms.DataGridViewTextBoxColumn Norme;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndexImgOS;
        private System.Windows.Forms.DataGridViewTextBoxColumn GuidProduitE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImgOs;
        private System.Windows.Forms.DataGridViewButtonColumn Indicator;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpComingStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpComingEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReferenceStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReferenceEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConfinedStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConfinedEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn DecommStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn DecommEnd;
        private System.Windows.Forms.Button bImpPdt;
    }
}