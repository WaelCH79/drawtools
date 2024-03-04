using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DAL;
using DrawTools.Helper;
using Microsoft.Office.Interop.Visio;

namespace DrawTools
{
    public partial class FormApplicationList : Form
    {

        #region Variables
        // Declared for the Master grid
        DataGridView Master_AppDGV = new DataGridView();
        // Declared for the Detail grid
        DataGridView Detail_VersionDGV = new DataGridView();

        List<int> lstNumericTextBoxColumns;

        Helper.HierarchicalGridHelper hierchicalGridHelper = new Helper.HierarchicalGridHelper();
        public int ColumnIndex;
        System.Data.DataTable dtName = new System.Data.DataTable();
        # endregion

        public FormApplicationList()
        {
            InitializeComponent();
        }



        private void FormApplicationList_Load(object sender, EventArgs e)
        {
            // To bind the Master data to List 
            //Master_BindData();

            // To bind the Detail data to List 
           // Detail_BindData();


            MasterGrid_Initialize();
            MasterGrid_Load();
            DetailGrid_Load();
            DetailGrid_Initialize();
            Detail_VersionDGV.Visible = false;



            //dgvApplications.DataSource = ds.Tables[0].DefaultView;
            //dgvApplications.Columns[0].Visible = false;

            //DataGridViewButtonColumn buttonColEdit = new DataGridViewButtonColumn();
            //buttonColEdit.Name = "Edit";
            //buttonColEdit.Text = "Edit";
            //buttonColEdit.UseColumnTextForButtonValue = true;
            //DataGridViewButtonColumn buttonColDelete = new DataGridViewButtonColumn();
            //buttonColDelete.Name = "Delete";
            //buttonColDelete.Text = "Delete";
            //buttonColDelete.UseColumnTextForButtonValue = true;

            //dgvApplications.Columns.Add(buttonColEdit);
            //dgvApplications.Columns.Add(buttonColDelete);

            this.WindowState = FormWindowState.Maximized;

        }

        private void Master_BindData()
        {      
            DataSet ds = new DataSet();

            MySQLConnection con = new MySQLConnection(); //create connection

            //create sql query for get data
            string sqlQuery = "SELECT  GuidApplication, row_number() over (order by GuidApplication desc) AS \"N°\",NomApplication as \"Nom de l'application\", Trigramme," +
                " CodeAp as \"Code application\", Perimetre,  GuidCadreRef, Installee FROM drawtools.application";

            //Executing
            ds = con.LoadData(sqlQuery);

        }

        // to generate Master Datagridview with your coding
        public void MasterGrid_Initialize()
        {
            //First generate the grid Layout Design
            Helper.HierarchicalGridHelper.Layouts(Master_AppDGV, System.Drawing.Color.LightSteelBlue, System.Drawing.Color.AliceBlue, System.Drawing.Color.WhiteSmoke, false, System.Drawing.Color.SteelBlue, false, false, false);

            //Set Height,width and add panel to your selected control
            Helper.HierarchicalGridHelper.Generategrid(Master_AppDGV, pDataGV, 800, 450, 10, 10);

            // System.Drawing.Color Image Column creation
            Helper.HierarchicalGridHelper.Templatecolumn(Master_AppDGV, GridControlTypes.ImageColumn, "  ", "GuidApplication", "", true, 26, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleRight, System.Drawing.Color.Transparent, null, "", "", System.Drawing.Color.Black);


            // BoundColumn creation
            Helper.HierarchicalGridHelper.Templatecolumn(Master_AppDGV, GridControlTypes.BoundColumn, "Num", "N°", "N°", true, 50, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, System.Drawing.Color.Transparent, null, "", "", System.Drawing.Color.Black);

            // BoundColumn creation
            Helper.HierarchicalGridHelper.Templatecolumn(Master_AppDGV, GridControlTypes.BoundColumn, "NomApplication", "Nom de l'application", "Nom de l'application", true, 300, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, System.Drawing.Color.Transparent, null, "", "", System.Drawing.Color.Black);


            // BoundColumn creation
            Helper.HierarchicalGridHelper.Templatecolumn(Master_AppDGV, GridControlTypes.BoundColumn, "Trigramme", "Trigramme", "Trigramme", true, 150, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, System.Drawing.Color.Transparent, null, "", "", System.Drawing.Color.Black);


            // BoundColumn creation
            Helper.HierarchicalGridHelper.Templatecolumn(Master_AppDGV, GridControlTypes.BoundColumn, "GuidApplication", "GuidApplication", "GuidApplication", false, 1, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleCenter, System.Drawing.Color.Transparent, null, "", "", System.Drawing.Color.Black);


            // BoundColumn creation
            Helper.HierarchicalGridHelper.Templatecolumn(Master_AppDGV, GridControlTypes.BoundColumn, "CodeAp", "Code application", "Code application", true, 120, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, System.Drawing.Color.Transparent, null, "", "", System.Drawing.Color.Black);

        }

        private void MasterGrid_Load()
        {
            MySQLConnection con = new MySQLConnection(); //create connection

            DataSet ds = new DataSet();

            //create sql query for get data
            string sqlQuery = "SELECT  GuidApplication, row_number() over (order by GuidApplication desc) AS \"Num\",NomApplication , Trigramme," +
                " CodeAp , Perimetre,  GuidCadreRef, Installee FROM drawtools.application";

            //Executing
            ds = con.LoadData(sqlQuery);

            // Bind data to DGV.
            Master_AppDGV.DataSource = ds.Tables[0].DefaultView;
        }

        private void DetailGrid_Load()
        {
            DataSet ds = new DataSet();

            MySQLConnection con = new MySQLConnection(); //create connection

            //create sql query for get data
            string sqlQuery = "SELECT  GuidAppVersion, row_number() over (order by GuidAppVersion desc) AS \"Num\",GuidApplication, Version , Statut FROM drawtools.appversion";

            //Executing
            ds = con.LoadData(sqlQuery);


            // Image Colum Click Event - In  this method we create an event for cell click and we will display the Detail grid with result.

            hierchicalGridHelper.DGVMasterGridClickEvents(Master_AppDGV, Detail_VersionDGV, Master_AppDGV.Columns[0].Index, ds.Tables[0], "GuidApplication");

        }

        // to generate Detail Datagridview with your coding
        public void DetailGrid_Initialize()
        {

            //First generate the grid Layout Design
            HierarchicalGridHelper.Layouts(Detail_VersionDGV, System.Drawing.Color.White, System.Drawing.Color.AliceBlue,  System.Drawing.Color.WhiteSmoke, false, System.Drawing.Color.SteelBlue, false, false, false);

            //Set Height,width and add panel to your selected control
            HierarchicalGridHelper.Generategrid(Detail_VersionDGV, pDataGV, 150, 50, 5, 5);

            // BoundColumn creation
            Helper.HierarchicalGridHelper.Templatecolumn(Detail_VersionDGV, GridControlTypes.BoundColumn, "Version", "Version", "Version", true, 80, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, System.Drawing.Color.Transparent, null, "", "", System.Drawing.Color.Black);


            // BoundColumn creation
            Helper.HierarchicalGridHelper.Templatecolumn(Detail_VersionDGV, GridControlTypes.BoundColumn, "Statut", "Statut", "Statut", true, 320, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, System.Drawing.Color.Transparent, null, "", "", System.Drawing.Color.Black);


            // BoundColumn creation
            Helper.HierarchicalGridHelper.Templatecolumn(Detail_VersionDGV, GridControlTypes.BoundColumn, "GuidAppVersion", "GuidAppVersion", "GuidAppVersion", false, 1, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleCenter, System.Drawing.Color.Transparent, null, "", "", System.Drawing.Color.Black);


            hierchicalGridHelper.DGVDetailGridClickEvents(Detail_VersionDGV, this);


        }

        private void txt_ApplicationSearch_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            MySQLConnection con = new MySQLConnection(); //create connection

            //create sql query for get data
            string sqlQuery = "SELECT  GuidApplication, row_number() over (order by GuidApplication desc) AS \"Num\",NomApplication , Trigramme," +
                " CodeAp , Perimetre,  GuidCadreRef, Installee FROM drawtools.application" +
                " Where NomApplication Like '%" + txt_ApplicationSearch.Text + "%' OR Trigramme Like '%" + txt_ApplicationSearch.Text + "%'";

            //Executing
            ds = con.LoadData(sqlQuery);
            Master_AppDGV.DataSource = ds.Tables[0].DefaultView;
        }

        private void BtnNewApp_Click(object sender, EventArgs e)
        {          
                FormPropApp fpa = new FormPropApp(this, null);
                fpa.ShowDialog(this);           
        }
    }

}
