using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DrawTools
{
    public partial class FormCnxBase : Form
    {
        private Form1 parent;

        public new Form1 Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        public FormCnxBase(Form1 p)
        {
            Parent = p;

            InitializeComponent();
            // get user dsn's
            Microsoft.Win32.RegistryKey reg = (Microsoft.Win32.Registry.CurrentUser).OpenSubKey("Software");
            if (reg != null)
            {
                reg = reg.OpenSubKey("ODBC");
                if (reg != null)
                {
                    reg = reg.OpenSubKey("ODBC.INI");
                    if (reg != null)
                    {
                        reg = reg.OpenSubKey("ODBC Data Sources");
                        if (reg != null)
                        {
                            // Get all DSN entries defined in DSN_LOC_IN_REGISTRY.
                            foreach (string sName in reg.GetValueNames()) lbBase.Items.Add(sName);
                        }
                        try
                        {
                            reg.Close();
                        }
                        catch { /* ignore this exception if we couldn't close */ }
                    }
                }
            }
        }

        private void bSelect_Click(object sender, EventArgs e)
        {
            if (lbBase.SelectedItem != null)
            {
                Parent.SelectedBase = (string)lbBase.SelectedItem;
                Close();
            }
        }

        private void bQuitter_Click(object sender, EventArgs e)
        {
            Parent.SelectedBase = null;
            Close();
        }
    }
}
