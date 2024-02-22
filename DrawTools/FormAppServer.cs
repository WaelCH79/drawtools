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
    public partial class FormAppServer : Form
    {
        private Form1 parent;
        List<String[]> lstAppVersion;

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

        public FormAppServer(Form1 p)
        {
            Parent = p;
            InitializeComponent();
            InitEvent();
            lstAppVersion = new List<string[]>();

            Parent.oCnxBase.CBAddComboBox("SELECT GuidApplication, NomApplication FROM Application ORDER BY NomApplication", this.cbGuidApp, this.cbApp);
            
        }

        private void InitEvent()
        {
            this.cbApp.SelectedIndexChanged += new System.EventHandler(this.cbApp_SelectedIndexChanged);
            this.cbVersion.SelectedIndexChanged += new System.EventHandler(this.cbVersion_SelectedIndexChanged);
            this.bExport.Click += new System.EventHandler(this.bExport_Click);
            this.bExportAll.Click += new System.EventHandler(this.bExportAll_Click);
        }

        void cbApp_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            dgServer.Rows.Clear();
            cbVersion.Text = ""; cbVersion.Items.Clear();
            lstAppVersion.Clear();

            if (cbApp.SelectedIndex != -1)
            {
                if (Parent.oCnxBase.CBRecherche("SELECT AppVersion.GuidAppVersion, Version, GuidVue, NomVue FROM Vue, AppVersion Where GuidApplication = '" + (string) cbGuidApp.Items[cbApp.SelectedIndex] + "' and AppVersion.GuidAppVersion = Vue.GuidAppVersion ORDER BY Version, NomVue"))
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

        void cbVersion_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            dgServer.Rows.Clear();
            
            if (cbApp.SelectedIndex != -1 && cbVersion.SelectedIndex != -1)
            {
                string[] aEnreg = lstAppVersion.Find(el => el[1] == (string)cbVersion.Items[cbVersion.SelectedIndex]);
                Parent.wkApp = new WorkApplication(Parent, (string)cbGuidApp.Items[cbApp.SelectedIndex], "", aEnreg[0]);

                XmlExcel xmlExcel = Parent.oCnxBase.Genere_ListeServer(true);

                XmlElement root = xmlExcel.docXml.DocumentElement;
                IEnumerator ienum = root.GetEnumerator();
                XmlNode Node;
                while (ienum.MoveNext())
                {
                    Node = (XmlNode)ienum.Current;
                    switch (Node.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (Node.Name == "row")
                            {
                                if (Node.Attributes[0].Name == "NbrField")
                                {
                                    int i = 0;
                                    string[] row = new string[Convert.ToInt16(Node.Attributes[0].Value.ToString())];
                                    IEnumerator ienumField = Node.GetEnumerator();
                                    XmlNode NodeField;
                                    while (ienumField.MoveNext())
                                    {
                                        NodeField = (XmlNode)ienumField.Current;
                                        switch (NodeField.NodeType)
                                        {
                                            case XmlNodeType.Element:
                                                row[i++] = NodeField.InnerText;
                                                break;
                                        }
                                    }
                                    dgServer.Rows.Add(row);
                                }
                            }
                            break;
                    }
                }
                Parent.wkApp = null;
            }
        }

        private void bExport_Click(object sender, EventArgs e)
        {
            if (cbApp.SelectedIndex != -1)
            {
                if (cbApp.SelectedIndex != -1 && cbVersion.SelectedIndex != -1)
                {
                    string[] aEnreg = lstAppVersion.Find(el => el[1] == (string)cbVersion.Items[cbVersion.SelectedIndex]);
                    Parent.wkApp = new WorkApplication(Parent, (string)cbGuidApp.Items[cbApp.SelectedIndex], "", aEnreg[0]);

                    XmlExcel xmlApp = Parent.oCnxBase.Genere_ListeServer(true);
                    //xmlApp.docXml.Save("C:\\DAT\\listeApp.xml");
                    string sPath = Parent.GetFullPath(Parent.wkApp) + "\\" + (string)cbApp.Items[cbApp.SelectedIndex] + " ListServer.xml";
                    xmlApp.XmlSave(sPath);
                    FormMsgAndLinkOk f = new FormMsgAndLinkOk(Parent, "Le fichier a été enregistré sous le nom:", sPath);
                    f.init();

                    Parent.wkApp = null;
                }
            }
        }

        private void bExportAll_Click(object sender, EventArgs e)
        {
            XmlExcel xmlApp = Parent.oCnxBase.Genere_ListeServer();
            string sPath = "C:\\DAT\\listeApp.xml";
            xmlApp.docXml.Save(sPath);
            xmlApp.XmlSave(sPath);
            FormMsgAndLinkOk f = new FormMsgAndLinkOk(Parent, "Le fichier a été enregistré sous le nom:", sPath);
            f.init();
        }
    }
}
