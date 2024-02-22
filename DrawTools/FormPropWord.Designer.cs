namespace DrawTools
{
    partial class FormPropWord
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
            this.dgProp = new System.Windows.Forms.DataGridView();
            this.fileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.Application = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GuidVue = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Policy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Set = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Sup = new System.Windows.Forms.DataGridViewButtonColumn();
            this.RichText = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgProp)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgProp);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(642, 286);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Object";
            // 
            // dgProp
            // 
            this.dgProp.AllowUserToAddRows = false;
            this.dgProp.AllowUserToDeleteRows = false;
            this.dgProp.AllowUserToResizeColumns = false;
            this.dgProp.AllowUserToResizeRows = false;
            this.dgProp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgProp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Application,
            this.GuidVue,
            this.Policy,
            this.Set,
            this.Sup,
            this.RichText});
            this.dgProp.Location = new System.Drawing.Point(8, 19);
            this.dgProp.MultiSelect = false;
            this.dgProp.Name = "dgProp";
            this.dgProp.ReadOnly = true;
            this.dgProp.RowHeadersVisible = false;
            this.dgProp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgProp.Size = new System.Drawing.Size(625, 261);
            this.dgProp.TabIndex = 0;
            this.dgProp.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgProp_CellClick);
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(20, 303);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(65, 21);
            this.bOK.TabIndex = 4;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(91, 303);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(65, 21);
            this.bCancel.TabIndex = 5;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // Application
            // 
            this.Application.HeaderText = "Propriete";
            this.Application.Name = "Application";
            this.Application.ReadOnly = true;
            this.Application.Width = 180;
            // 
            // GuidVue
            // 
            this.GuidVue.HeaderText = "Lien";
            this.GuidVue.Name = "GuidVue";
            this.GuidVue.ReadOnly = true;
            this.GuidVue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.GuidVue.Width = 380;
            // 
            // Policy
            // 
            this.Policy.HeaderText = "Policy";
            this.Policy.Name = "Policy";
            this.Policy.ReadOnly = true;
            this.Policy.Visible = false;
            // 
            // Set
            // 
            this.Set.HeaderText = "...";
            this.Set.Name = "Set";
            this.Set.ReadOnly = true;
            this.Set.Width = 20;
            // 
            // Sup
            // 
            this.Sup.HeaderText = "x";
            this.Sup.Name = "Sup";
            this.Sup.ReadOnly = true;
            this.Sup.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Sup.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Sup.Width = 20;
            // 
            // RichText
            // 
            this.RichText.HeaderText = "T";
            this.RichText.Name = "RichText";
            this.RichText.ReadOnly = true;
            this.RichText.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.RichText.Width = 20;
            // 
            // FormPropWord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 331);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormPropWord";
            this.Text = "FormPropWord";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgProp)).EndInit();
            this.ResumeLayout(false);

        }
       
        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgProp;
        private System.Windows.Forms.OpenFileDialog fileDialog1;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Application;
        private System.Windows.Forms.DataGridViewLinkColumn GuidVue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Policy;
        private System.Windows.Forms.DataGridViewButtonColumn Set;
        private System.Windows.Forms.DataGridViewButtonColumn Sup;
        private System.Windows.Forms.DataGridViewButtonColumn RichText;

    }
}