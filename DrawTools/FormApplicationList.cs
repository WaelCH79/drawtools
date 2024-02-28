using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;

namespace DrawTools
{
    public partial class FormApplicationList : Form
    {
        public FormApplicationList()
        {
            InitializeComponent();
        }

        ///// <summary>
        ///// The main entry point for the application.
        ///// </summary>
        //[STAThread]
        //static void Main(string[] args)
        //{

        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.DoEvents();

        //    // Check command line
        //    if (args.Length > 1)
        //    {
        //        MessageBox.Show("Incorrect number of arguments. Usage: DrawTools.exe [file]", "DrawTools");
        //    }


        //    FormApplicationList form = new FormApplicationList();
        //    Application.Run(form);

        //}

        private void FormApplicationList_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            MySQLConnection con = new MySQLConnection(); //create connection

            //create sql query for get data
            string sqlQuery = "SELECT  GuidApplication, row_number() over (order by GuidApplication desc) AS \"N°\",NomApplication as \"Nom de l'application\", Trigramme  FROM drawtools.application";

            //Executing
            ds = con.LoadData(sqlQuery);
            dgvApplications.AutoGenerateColumns = true;
            // Define the table for the grid.            

            dgvApplications.DataSource = ds.Tables[0].DefaultView;
            dgvApplications.Columns[0].Visible = false;
            this.WindowState = FormWindowState.Maximized;

        }

        private void txt_ApplicationSearch_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            MySQLConnection con = new MySQLConnection(); //create connection

            //create sql query for get data
            string sqlQuery = "SELECT  row_number() over (order by GuidApplication desc) AS \"N°\",NomApplication as \"Nom de l'application\", Trigramme  FROM drawtools.application" +
                " Where NomApplication Like '%" + txt_ApplicationSearch.Text + "%' OR Trigramme Like '%" + txt_ApplicationSearch.Text + "%'";

            //Executing
            ds = con.LoadData(sqlQuery);
            dgvApplications.DataSource = ds.Tables[0].DefaultView;
        }

        private void dgvApplications_DoubleClick(object sender, EventArgs e)
        {
            var guidApp = dgvApplications.Rows[dgvApplications.CurrentRow.Index].Cells[0].Value.ToString();
          
            Form1 fm=new Form1(guidApp);
            fm.MdiParent = this.MdiParent;
            fm.Show();
            this.Close();
            
        }

        private void BtnNewApp_Click(object sender, EventArgs e)
        {          
                FormPropApp fpa = new FormPropApp(this, null);
                fpa.ShowDialog(this);           
        }
    }

}
