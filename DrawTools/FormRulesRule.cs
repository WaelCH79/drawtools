using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DrawTools
{
    public partial class FormRulesRule : Form
    {
        private FormRules parent;

        public new FormRules Parent
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
            
        
        public void init()
        {

            ShowDialog(Parent);
        }
        public FormRulesRule(FormRules p)
        {
             Parent = p;
            InitializeComponent();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            Parent.CreateRowRule(tbNomRule.Text);
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
