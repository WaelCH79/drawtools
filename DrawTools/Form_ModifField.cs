using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Xml;

namespace DrawTools
{
    public partial class Form_ModifField : Form
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

        private void InitEvenement()
        {
            //cbNomTypeVue.SelectedIndexChanged += cbNomTypeVue_SelectedIndexChanged;
            bAnnuler.Click += BAnnuler_Click;
            bExport.Click += BExport_Click;
            bImport.Click += BImport_Click;
         
        }

        private void BImport_Click(object sender, EventArgs e)
        {
            if (tbTable.Text != "")
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.InitialDirectory = Parent.sPathRoot + "\\";
                openFileDialog1.Filter = "xml files (*.xml)|*.xml";
                openFileDialog1.FilterIndex = 1;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    XmlExcel xmlExcel = new XmlExcel(Parent, openFileDialog1.FileName, true);
                    if (xmlExcel.root.Name == tbTable.Text)
                    {
                        ArrayList lstRow = xmlExcel.XmlGetLstElFromName(xmlExcel.root, "row");
                        xmlExcel.xmlSaveRowToDBTable(lstRow, tbTable.Text);
                    }

                }
            }
            Close();
        }

        private void BExport_Click(object sender, EventArgs e)
        {
            if (tbField.Text != "" && tbGuid.Text != "" && tbTable.Text != "")
            {
                if (Parent.oCnxBase.CBRecherche("SELECT " + tbGuid.Text + "," + tbField.Text + " From " + tbTable.Text))
                {
                    XmlExcel xmlExcel = new XmlExcel(Parent, tbTable.Text);

                    while (Parent.oCnxBase.Reader.Read())
                    {
                        xmlExcel.CreatXmlFromReader();
                    }
                    Parent.oCnxBase.CBReaderClose();
                    xmlExcel.docXml.Save("C:\\DAT\\listeObj.xml");
                    Close();
                }
                Parent.oCnxBase.CBReaderClose();
            }
        }

        private void BAnnuler_Click(object sender, EventArgs e)
        {
            Close();
        }

        public Form_ModifField(Form1 p)
        {
            Parent = p;
            InitializeComponent();
            InitEvenement();
        }
    }

}
