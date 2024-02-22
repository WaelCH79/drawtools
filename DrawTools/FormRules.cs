using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DrawTools
{
    public partial class FormRules : Form
    {
        private Form1 parent;
        
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
            
        public FormRules(Form1 p)
        {
            Parent = p;
            InitializeComponent();
        }

        public void init()
        {

            ShowDialog(Parent);
        }

        private void bAddRule_Click(object sender, EventArgs e)
        {
            if (cbDomaine.SelectedItem != null)
            {
                FormRulesRule frr = new FormRulesRule(this);
                frr.init();

            }
        }

        public void CreateRowRule(string sRule)
        {
            if (sRule != "")
            {
                string[] row = {  Guid.NewGuid().ToString(), sRule};
                dgRules.Rows.Add(row);
            }
        }


        public void CreateRowPremisse(string sPremisse, string sObjet, string sEvaluation, string sOperation, string sValeur, string sConnecteur)
        {
            string[] row = { Guid.NewGuid().ToString(), sPremisse, sObjet, sEvaluation, sOperation, sValeur, sConnecteur };
            dgPremisse.Rows.Add(row);

        }

        private void bAddPremisse_Click(object sender, EventArgs e)
        {
            if (dgPremisse.SelectedRows!=null)
            {
                FormRulesPremisse frp = new FormRulesPremisse(this);
                frp.init();

            }
        }

        private void bDelPremisse_Click(object sender, EventArgs e)
        {

        }

        private void bDelRule_Click(object sender, EventArgs e)
        {

        }

    }
}
