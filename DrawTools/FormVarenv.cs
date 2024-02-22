using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace DrawTools
{

    public partial class FormVarenv : Form
    {
        private Form1 parent;
        private bool bLoadedForm;
        private bool bValidated;
        private Varenv oVarenv;
        public new Form1 Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }

        public FormVarenv(Form1 p, string Genpod, string Container)
        {
            Parent = p;
            bLoadedForm = false;
            InitializeComponent();
            tbGuidPod.Text = Genpod;
            tbGuidContainer.Text = Container;

        }

        public void init()
        {

            bOk.Click += BOk_Click;
            bCancel.Click += BCancel_Click;
            dgvVarenv.Validated += DgvVarenv_Validated;
            dgvVarenv.RowsAdded += DgvVarenv_RowsAdded;

            oVarenv = new Varenv(Parent, dgvVarenv, "Varenv");
            bLoadedForm = true;
            ShowDialog(Parent);
        }

        private void DgvVarenv_Validated(object sender, EventArgs e)
        {
            bValidated = true;
            for (int i = 0; i < dgvVarenv.Rows.Count-1; i++)
            {
                DataGridViewRow dgvr = dgvVarenv.Rows[i];
                if (dgvr.Cells["Nom"].Value == null || dgvr.Cells["Nom"].Value.ToString() == "") bValidated = false;
                if (dgvr.Cells["Valeur"].Value == null || dgvr.Cells["Valeur"].Value.ToString() == "") bValidated = false;
            }
            //throw new NotImplementedException();
        }

        private void DgvVarenv_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (bLoadedForm)
            {
                List<String[]> lstForeinKey = new List<string[]>();
                string[] aForeinKey = new string[2];
                aForeinKey[0] = dgvVarenv.Columns["GuidGenpod"].HeaderCell.ToolTipText;
                aForeinKey[1] = tbGuidPod.Text;
                lstForeinKey.Add(aForeinKey);
                aForeinKey = new string[2];
                aForeinKey[0] = dgvVarenv.Columns["GuidContainer"].HeaderCell.ToolTipText;
                aForeinKey[1] = tbGuidContainer.Text;
                lstForeinKey.Add(aForeinKey);
                oVarenv.RowsAdded(lstForeinKey);
            }
            //throw new NotImplementedException();
        }

        private void BOk_Click(object sender, EventArgs e)
        {
            if (bValidated)
            {
                for (int i = 0; i < dgvVarenv.Rows.Count; i++) oVarenv.SaveEnreg(i, true);

                Close();
            }
            else
            {
                MessageBox.Show("Certaines Cellules ne sont pas renseignées, la Validation ne peut être acceptée");
            }
            //throw new NotImplementedException();
        }

        private void BCancel_Click(object sender, EventArgs e)
        {
            Close();
            //throw new NotImplementedException();
        }
    }
    public class Varenv : Enreg
    {
        public Varenv(Form1 f, DataGridView d, string t)
        {
            F = f;
            dg = d;
            LstValue = new ArrayList();
            sTable = t;
            InitProp();
        }
    }
}
