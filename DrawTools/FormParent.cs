using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawTools
{
    public partial class FormParent : Form
    {
        public FormParent(Form formChild)
        {
            InitializeComponent();
            formChild.MdiParent = this;
            formChild.Show();
        }

        private void ShowChildForm(Form form)
        {
            foreach (Form child in MdiChildren)
            {
                child.Close();
            }
            form.MdiParent = this;
            form.Show();
        }
    }
}
