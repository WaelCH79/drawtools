using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DrawTools
{
    public partial class FormPropriete : Form
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

        public FormPropriete(Form1 p)
        {
            Parent = p;
            InitializeComponent();
        }

        public void CompleteField()
        {
            if (Parent.oCnxBase.CBRecherche("SELECT NumOption, Parameter From OptionsDraw"))
            {
                while (Parent.oCnxBase.Reader.Read())
                {
                    switch (Parent.oCnxBase.Reader.GetInt32(0))
                    {
                        case 0:
                            tbRootPath.Text = Parent.oCnxBase.Reader.GetString(1);
                            break;
                        case 1:
                            if (Parent.oCnxBase.Reader.GetString(1) == "Oui") cbComposant.Checked = true; else cbComposant.Checked = false;
                            break;
                        case 2:
                            if (Parent.oCnxBase.Reader.GetString(1) == "Oui") cbppt.Checked = true; else cbppt.Checked = false;
                            break;
                        case 3:
                            if (Parent.oCnxBase.Reader.GetString(1) == "Oui") cbInstallee.Checked = true; else cbInstallee.Checked = false;
                            break;
                    }

                }
            }
            Parent.oCnxBase.CBReaderClose();
            for (int i = 0; i < 1; i++)
            {
                switch (i)
                {
                    case 0:
                        if (Parent.bActiveToolTip) cbToolTip.Checked = true; else cbToolTip.Checked = false;
                        break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                tbRootPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            string sValue="";
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        sValue = tbRootPath.Text;
                        break;
                    case 1:
                        if (cbComposant.Checked) sValue = "Oui"; else sValue = "Non";
                        break;
                    case 2:
                        if (cbppt.Checked) sValue = "Oui"; else sValue = "Non";
                        break;
                    case 3:
                        if (cbInstallee.Checked) sValue = "Oui"; else sValue = "Non";
                        break;
                }

                if (Parent.oCnxBase.CBRecherche("SELECT NumOption From OptionsDraw Where NumOption=" + i))
                {
                    Parent.oCnxBase.CBReaderClose();
                    Parent.oCnxBase.CBWrite("UPDATE OptionsDraw SET Parameter='" + sValue + "' WHERE NumOption = " + i);
                }
                else
                {
                    Parent.oCnxBase.CBReaderClose();
                    Parent.oCnxBase.CBWrite("INSERT INTO OptionsDraw (NumOption, Parameter) VALUES (" + i + ",'" + sValue + "')");
                }
            }

            for (int i = 0; i < 1; i++)
            {
                switch (i)
                {
                    case 0:
                        if (cbToolTip.Checked) Parent.bActiveToolTip = true; else Parent.bActiveToolTip = false;
                        break;
                }                
            }

            this.Close();
        }

        private void bAnnuler_Click(object sender, EventArgs e)
        {

            this.Close();
        }
    }
}
