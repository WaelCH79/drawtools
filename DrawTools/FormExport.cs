using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace DrawTools
{
    public partial class FormExport : Form
    {
        private Form1 parent;
        private List<string[]> lstAppVersion;

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

        public FormExport(Form1 p)
        {
            Parent = p;
            lstAppVersion = new List<string[]>();
            InitializeComponent();
            Parent.oCnxBase.CBAddComboBox("SELECT GuidApplication, NomApplication FROM Application ORDER BY NomApplication", cbGuidApplication, cbApplication);
        }

        void cbApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbVersion.Text = ""; cbVersion.Items.Clear();
            lstAppVersion.Clear();

            if (cbApplication.SelectedIndex != -1)
            {
                if (Parent.oCnxBase.CBRecherche("SELECT AppVersion.GuidAppVersion, Version, GuidVue, NomVue FROM Vue, AppVersion Where GuidApplication = '" + (string)cbGuidApplication.Items[cbApplication.SelectedIndex] + "' and AppVersion.GuidAppVersion = Vue.GuidAppVersion ORDER BY Version, NomVue"))
                {
                    while (Parent.oCnxBase.Reader.Read())
                    {
                        string[] aEnreg = new string[4];
                        aEnreg[0] = Parent.oCnxBase.Reader.GetString(0);
                        aEnreg[1] = Parent.oCnxBase.Reader.GetString(1);
                        aEnreg[2] = Parent.oCnxBase.Reader.GetString(2);
                        aEnreg[3] = Parent.oCnxBase.Reader.GetString(3);
                        lstAppVersion.Add(aEnreg);
                    }
                }
                Parent.oCnxBase.CBReaderClose();
                for (int i = 0; i < lstAppVersion.Count; i++)
                {
                    if (cbVersion.FindString(lstAppVersion[i][1]) == -1)
                        cbVersion.Items.Add(lstAppVersion[i][1]);
                }
            }

        }

        private void bOK_Click(object sender, EventArgs e)
        {
            XmlDB xmlDB = new XmlDB(Parent, "Applications");

            if (cbApplication.SelectedIndex != -1 && cbVersion.SelectedIndex != -1)
            {
                string[] aEnreg = lstAppVersion.Find(el => el[1] == (string)cbVersion.Items[cbVersion.SelectedIndex]);
                Parent.wkApp = new WorkApplication(Parent, (string)cbGuidApplication.Items[cbApplication.SelectedIndex], "", aEnreg[0]);
                Parent.XmlCreatXmldb(xmlDB, (string)cbGuidApplication.Items[cbApplication.SelectedIndex], aEnreg[0]);
                if (xmlDB != null)
                    xmlDB.docXml.Save(Parent.GetFullPath(Parent.wkApp) + "\\" + (string)cbApplication.Items[cbApplication.SelectedIndex] + "Serveur.xml");
                Parent.wkApp = null;
            }
        }
    }
}
