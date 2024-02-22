using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace DrawTools
{
    public partial class FormPropVue : Form
    {
        private Form1 parent;
        private bool bInitializeFields;
        private int indexOp;

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

        private void InitEvenement() {
            cbNomTypeVue.SelectedIndexChanged += cbNomTypeVue_SelectedIndexChanged;
            /*
            //Affichage de la liste des environnements en fonction du type choisi
            oCnxBase.CBAddComboBox("SELECT NomEnvironnement FROM Environnement, TypeVue Where NomTypeVue='" + sTypeVue + "' AND TypeVue.GuidTypeVue=Environnement.GuidTypeVue ORDER BY NomEnvironnement", this.cbEnv);
            if (cbEnv.Items.Count != 0)
            {
                if (sNomEnvironnement != null && sNomEnvironnement != "")
                {
                    int i = cbEnv.Items.IndexOf(sNomEnvironnement.ToString());
                    if (i != -1) cbEnv.SelectedIndex = i;
                }
                else cbEnv.SelectedIndex = 0;
            }

            oCnxBase.CBAddComboBox("SELECT GuidVue, NomVue FROM Vue, TypeVue Where Vue.GuidTypeVue=TypeVue.GuidTypeVueInf And GuidApplication='" + sGuidApplication + "'AND NomTypeVue='" + cbTypeVue.Text + "' ORDER BY NomVue", this.cbGuidVueInf, this.cbVueInf);
            if (cbGuidVueInf.Items.Count != 0)
            {
                if (sGuidVueInf != null && sGuidVueInf != "")
                {
                    int i = cbGuidVueInf.Items.IndexOf(sGuidVueInf.ToString());
                    if (i != -1) cbVueInf.SelectedIndex = i;
                }
                else
                {
                    cbVueInf.SelectedIndex = 0;
                    sGuidVueInf = (string)this.cbGuidVueInf.Items[this.cbVueInf.SelectedIndex];
                }
            }

            */
        }

        void cbNomTypeVue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bInitializeFields)
            {
                if (cbNomTypeVue.SelectedIndex != -1)
                {
                    cbEnv.Items.Clear(); cbGuidEnv.Items.Clear(); cbEnv.Text = ""; cbGuidEnv.Text = "";
                    cbNomVueInf.Items.Clear(); cbGuidVueInf.Items.Clear(); cbNomVueInf.Text = ""; cbGuidVueInf.Text = "";
                    Parent.oCnxBase.CBAddComboBox("SELECT GuidEnvironnement, NomEnvironnement FROM Environnement Where GuidTypeVue='" + cbGuidTypeVue.Items[cbNomTypeVue.SelectedIndex] + "' ORDER BY NomEnvironnement", this.cbGuidEnv, this.cbEnv);
                    Parent.oCnxBase.CBAddComboBox("SELECT GuidVue, NomVue FROM Vue, TypeVue Where Vue.GuidTypeVue=TypeVue.GuidTypeVueInf And GuidAppVersion='" + tbNomApplication.Text + "'AND TypeVue.GuidTypeVue='" + cbGuidTypeVue.Items[cbNomTypeVue.SelectedIndex] + "' ORDER BY NomVue", this.cbGuidVueInf, this.cbNomVueInf);

                }
            }
            /*oCnxBase.CBAddComboBox("SELECT NomEnvironnement FROM Environnement, TypeVue Where NomTypeVue='" + sTypeVue + "' AND TypeVue.GuidTypeVue=Environnement.GuidTypeVue ORDER BY NomEnvironnement", this.cbEnv);
            if (cbEnv.Items.Count != 0)
            {
                if (sNomEnvironnement != null && sNomEnvironnement != "")
                {
                    int i = cbEnv.Items.IndexOf(sNomEnvironnement.ToString());
                    if (i != -1) cbEnv.SelectedIndex = i;
                }
                else cbEnv.SelectedIndex = 0;
            }

            oCnxBase.CBAddComboBox("SELECT GuidVue, NomVue FROM Vue, TypeVue Where Vue.GuidTypeVue=TypeVue.GuidTypeVueInf And GuidApplication='" + sGuidApplication + "'AND NomTypeVue='" + cbTypeVue.Text + "' ORDER BY NomVue", this.cbGuidVueInf, this.cbVueInf);
            if (cbGuidVueInf.Items.Count != 0)
            {
                if (sGuidVueInf != null && sGuidVueInf != "")
                {
                    int i = cbGuidVueInf.Items.IndexOf(sGuidVueInf.ToString());
                    if (i != -1) cbVueInf.SelectedIndex = i;
                }
                else
                {
                    cbVueInf.SelectedIndex = 0;
                    sGuidVueInf = (string)this.cbGuidVueInf.Items[this.cbVueInf.SelectedIndex];
                }
            }*/
            //throw new NotImplementedException();
        }

        public FormPropVue(Form1 p, string sGuidVue, WorkApplication wkA, int iOp = 0)
        {
            Parent = p;
            InitializeComponent();
            InitEvenement();
            this.cbGuidTypeVue.Items.Clear();
            this.cbNomTypeVue.Items.Clear();
            bInitializeFields = false;
            indexOp = iOp;
            Parent.oCnxBase.CBAddComboBox("SELECT GuidTypeVue, NomTypeVue FROM TypeVue ORDER BY NomTypeVue", this.cbGuidTypeVue, this.cbNomTypeVue);
            //Parent.oCnxBase.CBAddComboBox("SELECT GuidVue, NomVue FROM Vue Where GuidApplication='" + sGuidApplication + "' ORDER BY NomVue", this.cbGuidTypeVue, this.cbNomTypeVue);
            if (wkA.GuidAppVersion != null)
            {
                if (sGuidVue != null)
                {
                    if (Parent.oCnxBase.CBRecherche("Select GuidVue, NomVue, GuidAppVersion, GuidTypeVue, GuidVueInf, GuidEnvironnement from Vue Where GuidVue='" + sGuidVue + "' and GuidAppVersion='" + wkA.GuidAppVersion + "'"))
                    {
                        string sGuidTypeVue = null, sGuidVueInf = null, sGuidEnv = null;
                        tbGuidVue.Text = Parent.oCnxBase.Reader.GetString(0);
                        int idx_ = Parent.oCnxBase.Reader.GetString(1).IndexOf('_');
                        if (idx_ >=0)
                        {
                            tbPrefixNom.Text = Parent.oCnxBase.Reader.GetString(1).Substring(0,idx_);
                            tbNom.Text = Parent.oCnxBase.Reader.GetString(1).Substring(idx_+1); ;
                        } else tbNom.Text = Parent.oCnxBase.Reader.GetString(1); 
                        if (!Parent.oCnxBase.Reader.IsDBNull(2)) tbNomApplication.Text = Parent.oCnxBase.Reader.GetString(2);
                        if (!Parent.oCnxBase.Reader.IsDBNull(3)) sGuidTypeVue = Parent.oCnxBase.Reader.GetString(3);
                        if (!Parent.oCnxBase.Reader.IsDBNull(4)) sGuidVueInf = Parent.oCnxBase.Reader.GetString(4);
                        if (!Parent.oCnxBase.Reader.IsDBNull(5)) sGuidEnv = Parent.oCnxBase.Reader.GetString(5);
                        Parent.oCnxBase.CBReaderClose();

                        if (sGuidTypeVue != null)
                        {
                            int idx = cbGuidTypeVue.FindString(sGuidTypeVue);
                            cbGuidTypeVue.SelectedIndex = idx; cbNomTypeVue.SelectedIndex = idx;
                        }
                        if (sGuidVueInf != null)
                        {
                            int idx = cbGuidVueInf.FindString(sGuidVueInf);
                            cbGuidVueInf.SelectedIndex = idx; cbNomVueInf.SelectedIndex = idx;
                        }
                        if (sGuidEnv != null)
                        {
                            int idx = cbGuidEnv.FindString(sGuidEnv);
                            cbGuidEnv.SelectedIndex = idx; cbEnv.SelectedIndex = idx;
                        }
                        if (indexOp == 2) // copy vue
                        {
                            grpEcoSystem.Visible = false;
                            tbNom.ReadOnly = true;
                            grpNewView.Visible = true;
                            grpNewView.Location = new Point(12, 68);
                            grpNewView.Width = 557; grpNewView.Height = 78;
                        }
                    }
                    else
                    {
                        Parent.oCnxBase.CBReaderClose();
                        Close();
                    }
                }
                else
                {
                    tbGuidVue.Text = Guid.NewGuid().ToString();
                    tbPrefixNom.Text = Parent.GetTrigramme(wkA.Guid.ToString());
                    tbNomApplication.Text = wkA.GuidAppVersion.ToString();
                }
                bInitializeFields = false; ;
            }
            else Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            bCancel.Enabled = false;
            bOK.Enabled = false;
            switch(indexOp)
            {
                case 0: // creat
                case 1: // update
                    if (tbNom.Text.Length == 0 || cbNomTypeVue.SelectedIndex == -1)
                    {
                        MessageBox.Show("Les informations obligatoires sont : Nom, type de vue");
                    }
                    else
                    {
                        if (!Parent.oCnxBase.CBRecherche("SELECT GuidVue FROM Vue where GuidVue ='" + tbGuidVue.Text + "'"))
                        {
                            Parent.oCnxBase.CBReaderClose();
                            if (cbNomVueInf.SelectedIndex != -1)
                            {
                                if (cbEnv.SelectedIndex != -1)
                                    Parent.oCnxBase.CBWrite("INSERT INTO Vue (GuidVue, NomVue, GuidGVue, GuidAppVersion, GuidTypeVue, GuidVueInf, GuidEnvironnement) VALUES ('" + tbGuidVue.Text + "','" + tbPrefixNom.Text + "_" + tbNom.Text + "','" + tbGuidVue.Text + "','" + tbNomApplication.Text + "','" + cbGuidTypeVue.Items[cbNomTypeVue.SelectedIndex] + "','" + cbGuidVueInf.Items[cbNomVueInf.SelectedIndex] + "','" + cbGuidEnv.Items[cbEnv.SelectedIndex] + "')");
                                else
                                    Parent.oCnxBase.CBWrite("INSERT INTO Vue (GuidVue, NomVue, GuidGVue, GuidAppVersion, GuidTypeVue, GuidVueInf) VALUES ('" + tbGuidVue.Text + "','" + tbPrefixNom.Text + "_" + tbNom.Text + "','" + tbGuidVue.Text + "','" + tbNomApplication.Text + "','" + cbGuidTypeVue.Items[cbNomTypeVue.SelectedIndex] + "','" + cbGuidVueInf.Items[cbNomVueInf.SelectedIndex] + "')");
                            }
                            else
                                Parent.oCnxBase.CBWrite("INSERT INTO Vue (GuidVue, NomVue, GuidGVue, GuidAppVersion, GuidTypeVue) VALUES ('" + tbGuidVue.Text + "','" + tbPrefixNom.Text + "_" + tbNom.Text + "','" + tbGuidVue.Text + "','" + tbNomApplication.Text + "','" + cbGuidTypeVue.Items[cbNomTypeVue.SelectedIndex] + "')");
                        }
                        else
                        {
                            Parent.oCnxBase.CBReaderClose();
                            if (cbNomVueInf.SelectedIndex != -1)
                            {
                                if (cbEnv.SelectedIndex != -1)
                                    Parent.oCnxBase.CBWrite("UPDATE Vue SET NomVue='" + tbPrefixNom.Text + "_" + tbNom.Text + "', GuidAppVersion='" + tbNomApplication.Text + "', GuidTypeVue='" + cbGuidTypeVue.Items[cbNomTypeVue.SelectedIndex] + "', GuidVueInf='" + cbGuidVueInf.Items[cbNomVueInf.SelectedIndex] + "', GuidEnvironnement='" + cbGuidEnv.Items[cbEnv.SelectedIndex] + "'  WHERE GuidVue = '" + tbGuidVue.Text + "'");
                                else
                                    Parent.oCnxBase.CBWrite("UPDATE Vue SET NomVue='" + tbPrefixNom.Text + "_" + tbNom.Text + "', GuidAppVersion='" + tbNomApplication.Text + "', GuidTypeVue='" + cbGuidTypeVue.Items[cbNomTypeVue.SelectedIndex] + "', GuidVueInf='" + cbGuidVueInf.Items[cbNomVueInf.SelectedIndex] + "'  WHERE GuidVue = '" + tbGuidVue.Text + "'");
                            }
                            else
                                Parent.oCnxBase.CBWrite("UPDATE Vue SET NomVue='" + tbPrefixNom.Text + "_" + tbNom.Text + "', GuidAppVersion='" + tbNomApplication.Text + "', GuidTypeVue='" + cbGuidTypeVue.Items[cbNomTypeVue.SelectedIndex] + "'  WHERE GuidVue = '" + tbGuidVue.Text + "'");
                        }
                    }
                    break;
                case 2: // copy
                    if (tbNomNouvelleVue.Text != "")
                    {
                        XmlDB xmlDB = new XmlDB(Parent, "Applications");
                        List<string[]> lstChangeObjet = new List<string[]>();
                        XmlElement el = null;

                        Parent.XmlCreatXmlVue(xmlDB, Parent.wkApp, tbGuidVue.Text);
                        if (xmlDB != null)
                        {
                            el = xmlDB.XmlGetElFromInnerText(xmlDB.root, Parent.wkApp.Guid.ToString());
                            if (xmlDB.SetCursor(xmlDB.XmlGetFirstElFromName(el, "AppVersion")))
                            {
                                XmlElement elAfterAppVersion = xmlDB.XmlGetFirstElFromParent(xmlDB.GetCursor(), "After");
                                xmlDB.CursorClose();
                                XmlElement elVue = xmlDB.XmlGetFirstElFromParent(elAfterAppVersion, "Vue");
                                if (elVue != null)
                                {
                                    string sNomVue = xmlDB.XmlGetAttValueAFromAttValueB(elVue, "Value", "Nom", "NomVue");
                                    // change NomVue
                                    xmlDB.XmlSetAllAttNewValue(elVue, "NomVue", sNomVue, tbPrefixNom.Text + "_" + tbNomNouvelleVue.Text);

                                    xmlDB.XmlCopyVue(elVue, elAfterAppVersion, lstChangeObjet);

                                    //xmlDB.docXml.Save(Parent.GetFullPath(Parent.wkApp) + "\\" + "appNew" + "Serveur.xml");
                                    xmlDB.ImportXml(xmlDB.docXml.DocumentElement);
                                }
                            }
                        }

                    }
                    break;
            }
            Parent.InitCbApplication();
            this.Close();
        }
    }
}
