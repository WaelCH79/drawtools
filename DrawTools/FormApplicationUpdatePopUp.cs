using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows.Forms;
using BLL;

namespace DrawTools
{
    public partial class FormApplicationUpdatePopUp : Form
    {
        private ApplicationRepo aRepo;
        private Form formeParent;
        private bool validate;

        public Guid GuidVue { get; private set; }

        public FormApplicationUpdatePopUp(Form formeParent)
        {
            InitializeComponent();
            aRepo = new ApplicationRepo();
            this.formeParent = formeParent;
            
        }

        #region Evenement

        private void FormApplicationUpdatePopUp_Load(object sender, EventArgs e)
        {
            LoadComboBoxTypeView();
        }


        private void bOK_Click(object sender, EventArgs e)
        {
            validate = true;
            ValidateChildren(ValidationConstraints.Enabled);
            
            if(validate)
            {
                this.Save();
                DialogResult = DialogResult.Yes;
                Form1 fm = new Form1(GuidVue.ToString());
                fm.MdiParent = this.formeParent.MdiParent;
                fm.Show();
                this.formeParent.Close();
                this.Close();
            }            
        }

        private void tbTrigramme_TextChanged(object sender, EventArgs e)
        {
            tbPrefixNom.Text = tbTrigramme.Text;
        }

        private void tbNom_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValideteTB(tbNomAppli, EP_TXT_NomAppli, e);
        }

        private void tbTrigramme_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValideteTB(tbTrigramme, EP_TXT_Trigramme, e);
        }

        private void tbAppVersion_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValideteTB(tbLabelAppVersion, EP_TXT_Version, e);
        }

        private void TBVue_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValideteTB(TBLabelVue, EP_TXT_NomVue, e);
        }


        #endregion

        
        private void LoadComboBoxTypeView()
        {
            DataSet ds = new DataSet();

            MySQLConnection con = new MySQLConnection(); //create connection

            //create sql query for get data
            string sqlQuery = "SELECT GuidTypeVue, NomTypeVue FROM TypeVue ORDER BY NomTypeVue";

            //Executing
            ds = con.LoadData(sqlQuery);

            Dictionary<string, string> dict = ds.Tables[0].AsEnumerable()
                                    .ToDictionary(
                                    row => row.Field<string>("GuidTypeVue"),
                                    row => row.Field<string>("NomTypeVue"));

            // Bind the combobox
            cbNomTypeVue.DataSource = new BindingSource(dict, null);
            cbNomTypeVue.DisplayMember = "Value";
            cbNomTypeVue.ValueMember = "Key";
        }

        private void ValideteTB(TextBox tb, ErrorProvider ep, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                e.Cancel = true;
                tb.Focus();
                ep.SetError(tb, "textbox should not be left blank!");
                validate = false;
            }
            else
            {
                e.Cancel = false;
                ep.SetError(tb, "");
            }
        }

        private void Save()
        {
            List<string> queryList = new List<string>();
            var guidApp = Guid.NewGuid().ToString();
            var guidAppVersion = Guid.NewGuid().ToString();
            var GuidVue = Guid.NewGuid().ToString();
            queryList.Add($"INSERT INTO Application (GuidApplication, NomApplication, Trigramme,  GuidArborescence) VALUES ('{guidApp}','{tbNomAppli}','{tbTrigramme.Text}','764079ad-621b-4c57-92be-9d1530fb20cb')");
            queryList.Add($"INSERT INTO DansTypeVue (GuidTypeVue, GuidObjet, TypeObjet) VALUES ('49c88d3d-f32f-44fe-ad6c-35977c5b812e','{guidApp}','Application')");
            queryList.Add($"INSERT INTO AppVersion (GuidAppVersion, Version, GuidApplication) VALUES ('{guidAppVersion}','{tbLabelAppVersion.Text}','{guidApp}')");
            queryList.Add($"INSERT INTO Vue (GuidVue, NomVue, GuidGVue, GuidAppVersion, GuidTypeVue) VALUES ('{GuidVue.ToString()}','{tbPrefixNom.Text + "_" + TBLabelVue.Text}','{GuidVue.ToString()} ','{guidAppVersion}','{cbNomTypeVue.SelectedValue.ToString()}')");
            aRepo.CreateApplication(queryList);
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            this.Close();
        }
    }
}
