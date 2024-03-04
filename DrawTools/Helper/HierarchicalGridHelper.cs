using DAL;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace DrawTools.Helper
{
    internal class HierarchicalGridHelper
    {
        #region Variables
        public DataGridView MasterDGVs = new DataGridView();
        public DataGridView DetailDGVs = new DataGridView();
        private Form formApplication;
        static String ImageName = "toggle.png";
        String FilterColumnName = "";
        DataTable DetailgridDT;
        int gridColumnIndex = 0;

        String EventFucntions;
        # endregion

        //Set all the telerik Grid layout
        #region Layout

        public static void Layouts(DataGridView appDGV, Color BackgroundColor, Color RowsBackColor, Color AlternatebackColor, Boolean AutoGenerateColumns, Color HeaderColor, Boolean HeaderVisual, Boolean RowHeadersVisible, Boolean AllowUserToAddRows)
        {
            //Grid Back ground Color
            appDGV.BackgroundColor = BackgroundColor;

            //Grid Back Color
            appDGV.RowsDefaultCellStyle.BackColor = RowsBackColor;

            //GridColumnStylesCollection Alternate Rows Backcolr
            appDGV.AlternatingRowsDefaultCellStyle.BackColor = AlternatebackColor;

            // Auto generated here set to tru or false.
            appDGV.AutoGenerateColumns = AutoGenerateColumns;

            //Column Header back Color
            appDGV.ColumnHeadersDefaultCellStyle.BackColor = HeaderColor;
            //
            appDGV.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //header Visisble
            appDGV.EnableHeadersVisualStyles = HeaderVisual;

            // Enable the row header
            appDGV.RowHeadersVisible = RowHeadersVisible;

            // to Hide the Last Empty row here we use false.
            appDGV.AllowUserToAddRows = AllowUserToAddRows;
        }
        #endregion

        //Add your grid to your selected Control and set height,width,position of your grid.
        #region Variables
        public static void Generategrid(DataGridView AppDGV, Control cntrlName, int width, int height, int xval, int yval)
        {
            AppDGV.Location = new Point(xval, yval);
            AppDGV.Size = new Size(width, height);
            AppDGV.ScrollBars = ScrollBars.Both;
            cntrlName.Controls.Add(AppDGV);
        }
        #endregion

        //Template Column In this column we can add Textbox,Lable,Check Box,Dropdown box and etc
        #region Templatecolumn
        public static void Templatecolumn(DataGridView AppDGV, GridControlTypes AppControlTypes, String cntrlnames, String Headertext, String ToolTipText, Boolean Visible, int width, DataGridViewTriState Resizable, DataGridViewContentAlignment cellAlignment, DataGridViewContentAlignment headerAlignment, Color CellTemplateBackColor, DataTable dtsource, String DisplayMember, String ValueMember, Color CellTemplateforeColor)
        {
            switch (AppControlTypes)
            {
                case GridControlTypes.CheckBox:
                    DataGridViewCheckBoxColumn dgvChk = new DataGridViewCheckBoxColumn();
                    dgvChk.ValueType = typeof(bool);
                    dgvChk.Name = cntrlnames;

                    dgvChk.HeaderText = Headertext;
                    dgvChk.ToolTipText = ToolTipText;
                    dgvChk.Visible = Visible;
                    dgvChk.Width = width;
                    dgvChk.SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvChk.Resizable = Resizable;
                    dgvChk.DefaultCellStyle.Alignment = cellAlignment;
                    dgvChk.HeaderCell.Style.Alignment = headerAlignment;
                    if (CellTemplateBackColor.Name.ToString() != "Transparent")
                    {
                        dgvChk.CellTemplate.Style.BackColor = CellTemplateBackColor;
                    }
                    dgvChk.DefaultCellStyle.ForeColor = CellTemplateforeColor;
                    AppDGV.Columns.Add(dgvChk);
                    break;
                case GridControlTypes.BoundColumn:
                    DataGridViewColumn dgvbound = new DataGridViewTextBoxColumn();
                    dgvbound.DataPropertyName = cntrlnames;
                    dgvbound.Name = cntrlnames;
                    dgvbound.HeaderText = Headertext;
                    dgvbound.ToolTipText = ToolTipText;
                    dgvbound.Visible = Visible;
                    dgvbound.Width = width;
                    dgvbound.SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvbound.Resizable = Resizable;
                    dgvbound.DefaultCellStyle.Alignment = cellAlignment;
                    dgvbound.HeaderCell.Style.Alignment = headerAlignment;
                    dgvbound.ReadOnly = true;
                    if (CellTemplateBackColor.Name.ToString() != "Transparent")
                    {
                        dgvbound.CellTemplate.Style.BackColor = CellTemplateBackColor;
                    }
                    dgvbound.DefaultCellStyle.ForeColor = CellTemplateforeColor;
                    AppDGV.Columns.Add(dgvbound);
                    break;
                case GridControlTypes.TextBox:
                    DataGridViewTextBoxColumn dgvText = new DataGridViewTextBoxColumn();
                    dgvText.ValueType = typeof(decimal);
                    dgvText.DataPropertyName = cntrlnames;
                    dgvText.Name = cntrlnames;
                    dgvText.HeaderText = Headertext;
                    dgvText.ToolTipText = ToolTipText;
                    dgvText.Visible = Visible;
                    dgvText.Width = width;
                    dgvText.SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvText.Resizable = Resizable;
                    dgvText.DefaultCellStyle.Alignment = cellAlignment;
                    dgvText.HeaderCell.Style.Alignment = headerAlignment;
                    if (CellTemplateBackColor.Name.ToString() != "Transparent")
                    {
                        dgvText.CellTemplate.Style.BackColor = CellTemplateBackColor;
                    }
                    dgvText.DefaultCellStyle.ForeColor = CellTemplateforeColor;
                    AppDGV.Columns.Add(dgvText);
                    break;
                case GridControlTypes.ComboBox:
                    DataGridViewComboBoxColumn dgvcombo = new DataGridViewComboBoxColumn();
                    dgvcombo.ValueType = typeof(decimal);
                    dgvcombo.Name = cntrlnames;
                    dgvcombo.DataSource = dtsource;
                    dgvcombo.DisplayMember = DisplayMember;
                    dgvcombo.ValueMember = ValueMember;
                    dgvcombo.Visible = Visible;
                    dgvcombo.Width = width;
                    dgvcombo.SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvcombo.Resizable = Resizable;
                    dgvcombo.DefaultCellStyle.Alignment = cellAlignment;
                    dgvcombo.HeaderCell.Style.Alignment = headerAlignment;
                    if (CellTemplateBackColor.Name.ToString() != "Transparent")
                    {
                        dgvcombo.CellTemplate.Style.BackColor = CellTemplateBackColor;

                    }
                    dgvcombo.DefaultCellStyle.ForeColor = CellTemplateforeColor;
                    AppDGV.Columns.Add(dgvcombo);
                    break;

                case GridControlTypes.Button:
                    DataGridViewButtonColumn dgvButtons = new DataGridViewButtonColumn();
                    dgvButtons.Name = cntrlnames;
                    dgvButtons.FlatStyle = FlatStyle.Popup;
                    dgvButtons.DataPropertyName = cntrlnames;
                    dgvButtons.Visible = Visible;
                    dgvButtons.Width = width;
                    dgvButtons.SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvButtons.Resizable = Resizable;
                    dgvButtons.DefaultCellStyle.Alignment = cellAlignment;
                    dgvButtons.HeaderCell.Style.Alignment = headerAlignment;
                    if (CellTemplateBackColor.Name.ToString() != "Transparent")
                    {
                        dgvButtons.CellTemplate.Style.BackColor = CellTemplateBackColor;
                    }
                    dgvButtons.DefaultCellStyle.ForeColor = CellTemplateforeColor;
                    AppDGV.Columns.Add(dgvButtons);
                    break;
                case GridControlTypes.ImageColumn:
                    DataGridViewImageColumn dgvnestedBtn = new DataGridViewImageColumn();
                    dgvnestedBtn.Name = cntrlnames;
                    ImageName = @"bmp\expand.png";

                    dgvnestedBtn.Image = Image.FromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ImageName));
                    // dgvnestedBtn.DataPropertyName = cntrlnames;
                    dgvnestedBtn.Visible = Visible;
                    dgvnestedBtn.Width = width;
                    dgvnestedBtn.SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvnestedBtn.Resizable = Resizable;
                    dgvnestedBtn.DefaultCellStyle.Alignment = cellAlignment;
                    dgvnestedBtn.HeaderCell.Style.Alignment = headerAlignment;
                    AppDGV.Columns.Add(dgvnestedBtn);
                    break;
            }




        }

        #endregion


        public void DGVMasterGridClickEvents(DataGridView AppMasterDGV, DataGridView AppDetailDGV, int columnIndexs, DataTable DetailTable, String FilterColumn)
        {
            MasterDGVs = AppMasterDGV;
            DetailDGVs = AppDetailDGV;
            gridColumnIndex = columnIndexs;
            DetailgridDT = DetailTable;
            FilterColumnName = FilterColumn;

            MasterDGVs.CellContentClick += new DataGridViewCellEventHandler(DGVMasterGridCellContentClick_Event);
            MasterDGVs.CellLeave += new DataGridViewCellEventHandler(DGVMasterGridCellCellLeaveClick_Event);


        }

        public void DGVDetailGridClickEvents(DataGridView ShanuDetailDGV, Form formApplication)
        {

            DetailDGVs = ShanuDetailDGV;
            this.formApplication = formApplication;
            DetailDGVs.CellContentClick += new DataGridViewCellEventHandler(detailDGVs_CellContentClick_Event);


        }

        private void detailDGVs_CellContentClick_Event(object sender, DataGridViewCellEventArgs e)
        {

            var guidApp = DetailDGVs.Rows[DetailDGVs.CurrentRow.Index].Cells[0].Value.ToString();
            Form1 fm = new Form1(guidApp);
            fm.MdiParent = this.formApplication.MdiParent;
            fm.Show();
            this.formApplication.Close();
        }


        // Image Colukmn Click evnet
        #region Image Colukmn Click Event
        public void DGVMasterGridClickEvents(DataGridView AppMasterDGV, DataGridView AppDetailDGV, int columnIndexs, GridEventTypes eventtype, GridControlTypes types, String FilterColumn)
        {
            MasterDGVs = AppMasterDGV;
            DetailDGVs = AppDetailDGV;
            gridColumnIndex = columnIndexs;

            DataSet ds = new DataSet();

            MySQLConnection con = new MySQLConnection(); //create connection

            //create sql query for get data
            string sqlQuery = "SELECT  GuidAppVersion, row_number() over (order by GuidAppVersion desc) AS \"Num\",GuidApplication, Version , Statut FROM drawtools.appversion";

            //Executing
            ds = con.LoadData(sqlQuery);

            DetailgridDT = ds.Tables[0];
            FilterColumnName = FilterColumn;

            MasterDGVs.CellContentClick += new DataGridViewCellEventHandler(DGVMasterGridCellContentClick_Event);


        }
        private void DGVMasterGridCellContentClick_Event(object sender, DataGridViewCellEventArgs e)
        {

            MasterDGVs.Rows[e.RowIndex].Cells[0].Value = Image.FromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ImageName));

            if (e.ColumnIndex == gridColumnIndex)
            {
                if (ImageName == @"bmp\expand.png")
                {
                    DetailDGVs.Visible = true;
                    ImageName = @"bmp\toggle.png";
                    MasterDGVs.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Image.FromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ImageName));


                    String Filterexpression = MasterDGVs.Rows[e.RowIndex].Cells[FilterColumnName].Value.ToString();

                    MasterDGVs.Controls.Add(DetailDGVs);

                    Rectangle dgvRectangle = MasterDGVs.GetCellDisplayRectangle(1, e.RowIndex, true);
                    DetailDGVs.Size = new Size(MasterDGVs.Width - 200, (DetailDGVs.Rows.Count * 20) + 50);
                    DetailDGVs.Location = new Point(dgvRectangle.X, dgvRectangle.Y + 20);


                    DataView detailView = new DataView(DetailgridDT);
                    detailView.RowFilter = FilterColumnName + " = '" + Filterexpression + "'";                  

                    DetailDGVs.DataSource = detailView;


                }
                else
                {
                    ImageName = @"bmp\expand.png";
                    //  cols.Image = Image.FromFile(ImageName);
                    MasterDGVs.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Image.FromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ImageName));
                    DetailDGVs.Visible = false;
                }
            }
            else
            {
                DetailDGVs.Visible = false;

            }
        }

        private void DGVMasterGridCellCellLeaveClick_Event(object sender, DataGridViewCellEventArgs e)
        {
            ImageName = @"bmp\expand.png";
            //  cols.Image = Image.FromFile(ImageName);
            MasterDGVs.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Image.FromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ImageName));
            DetailDGVs.Visible = false;
        }

        #endregion


    }
}
//Enum decalaration for DataGridView Column Type ex like Textbox Column ,Button Column
public enum GridControlTypes { BoundColumn, TextBox, ComboBox, CheckBox, DateTimepicker, Button, NumericTextBox, ColorDialog, ImageColumn }
public enum GridEventTypes { CellClick, cellContentClick, EditingControlShowing }
