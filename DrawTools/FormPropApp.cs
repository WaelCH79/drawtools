using System;
using System.Collections;
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
    public partial class FormPropApp : Form
    {
        private Form1 parent;
        private List<string[]> lstApp;
        private List<string[]> lstVersion;
        private string sNew;
        private string sGuidAppVersion;
        private int iLastSelectedIndex;
        private int iRefLastSelectedIndex;
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

        public FormPropApp(Form1 p, string sGuidApp, int iOp=0)
        {
            Parent = p;
            InitializeComponent();
            bAction.Text = "";
            sNew = "<nouvelle>";
            indexOp = iOp;
            lstVersion = new List<string[]>();
            iLastSelectedIndex = -1;
            iRefLastSelectedIndex = -1;
            sGuidAppVersion = "";
            if (indexOp != 0)
            {
                
                if (Parent.oCnxBase.CBRecherche("SELECT GuidApplication, NomApplication, Trigramme, GuidAppVersion, GuidArborescence FROM Application WHERE GuidApplication='" + sGuidApp + "'"))
                {
                    if(!Parent.oCnxBase.Reader.IsDBNull(0)) tbGuidApp.Text = Parent.oCnxBase.Reader.GetString(0);
                    if(!Parent.oCnxBase.Reader.IsDBNull(1)) tbNom.Text = Parent.oCnxBase.Reader.GetString(1);
                    if(!Parent.oCnxBase.Reader.IsDBNull(2)) tbTrigramme.Text = Parent.oCnxBase.Reader.GetString(2);
                    if (!Parent.oCnxBase.Reader.IsDBNull(3)) sGuidAppVersion = Parent.oCnxBase.Reader.GetString(3);
                    if (!Parent.oCnxBase.Reader.IsDBNull(4)) tbCode.Text = Parent.oCnxBase.Reader.GetString(4);
                    Parent.oCnxBase.CBReaderClose();
                    switch (indexOp)
                    {
                        case 1: // Modif App
                            grpVersion.Visible = true;
                            grpVersion.Location = new Point(12, 96);
                            grpVersion.Width = 275; grpVersion.Height = 132;
                            grpEcoSystem.Visible = true;
                            grpEcoSystem.Location = new Point(293, 96);
                            grpEcoSystem.Width = 275; grpEcoSystem.Height = 132;
                            if (Parent.oCnxBase.CBRecherche("SELECT GuidAppVersion, Version FROM AppVersion WHERE GuidApplication='" + sGuidApp + "' order by Version"))
                            {
                                while (Parent.oCnxBase.Reader.Read())
                                {
                                    string[] aEnreg = new string[2];
                                    aEnreg[0] = Parent.oCnxBase.Reader.GetString(0); aEnreg[1] = Parent.oCnxBase.Reader.GetString(1);
                                    lstVersion.Add(aEnreg);
                                }
                            }
                            Parent.oCnxBase.CBReaderClose();
                            refresh_cbVersion();
                            if (sGuidAppVersion != "") cbVersion.SelectedIndex = lstVersion.FindIndex(el => el[0] == sGuidAppVersion) + 1;
                            break;
                        case 2: // Copy App
                            grpNewApp.Visible = true;
                            grpNewApp.Location = new Point(12, 96);
                            grpNewApp.Width = 275; grpNewApp.Height = 132;
                            
                            lstApp = new List<string[]>();
                            Parent.oCnxBase.CBAddComboBox("SELECT GuidApplication, NomApplication, Trigramme FROM Application ORDER BY NomApplication", lstApp, cbApplication2, 1);

                            break;
                        case 3: // Copy Ver
                            grpVersion.Visible = true;
                            grpVersion.Location = new Point(12, 96);
                            grpVersion.Width = 275; grpNewApp.Height = 132;

                            grpNewVersion.Visible = true;
                            grpNewVersion.Location = new Point(293, 96);
                            grpNewVersion.Width = 275; grpNewApp.Height = 132;
                            bAction.Visible = false;
                            if (Parent.oCnxBase.CBRecherche("SELECT GuidAppVersion, Version FROM AppVersion WHERE GuidApplication='" + sGuidApp + "' order by Version"))
                            {
                                while (Parent.oCnxBase.Reader.Read())
                                {
                                    string[] aEnreg = new string[2];
                                    aEnreg[0] = Parent.oCnxBase.Reader.GetString(0); aEnreg[1] = Parent.oCnxBase.Reader.GetString(1);
                                    lstVersion.Add(aEnreg);
                                }
                            }
                            Parent.oCnxBase.CBReaderClose();
                            init_cbVersions();
                            break;
                        case 4: // Add Pattern
                            break;
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
                //cbVersion.Enabled = false; tbStatut.Enabled = false; bAction.Enabled = false;
                tbGuidApp.Text = Guid.NewGuid().ToString();
                grpEcoSystem.Visible = true;
                grpEcoSystem.Location = new Point(293, 96);
                grpEcoSystem.Width = 275; grpEcoSystem.Height = 132;
            }
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void refresh_cbVersion()
        {
            bAction.Text = "";
            cbVersion.SelectedIndex = -1;
            cbVersion.Items.Clear();
            cbVersion.Text = "";
            iLastSelectedIndex = -1;
            switch (indexOp)
            {
                case 1: // Modif App
                    cbVersion.Items.Add(sNew);
                    break;
                case 2: // Copy App
                    break;
                case 3: // Copy Ver

                    break;
                case 4: // Move Ver
                    break;
            }

            for (int i = 0; i < lstVersion.Count; i++)
                cbVersion.Items.Add(lstVersion[i][1]);
        }

        private void init_cbVersions()
        {
            bAction.Visible = false;
            cbVersion.SelectedIndex = -1; cbVersion.Items.Clear();
            cbVersion2.SelectedIndex = -1; cbVersion2.Items.Clear();
            cbVersion.Text = ""; cbVersion2.Text = "";
            iLastSelectedIndex = -1;

            for (int i = 0; i < lstVersion.Count; i++)
            {
                cbVersion.Items.Add(lstVersion[i][1]);
                cbVersion2.Items.Add(lstVersion[i][1]);
            }
        }
        private void bAction_Click(object sender, EventArgs e)
        {
            if (bAction.Text != "")
            {
                if(bAction.Text == "Add")
                {
                    string[] aEnreg = new string[2];
                    aEnreg[0] = Guid.NewGuid().ToString();
                    aEnreg[1] = cbVersion.Text;
                    lstVersion.Add(aEnreg);
                    if(lstVersion.Count==1) sGuidAppVersion = aEnreg[0];
                } else
                {
                    string sFind = (string) cbVersion.Items[iLastSelectedIndex];
                    string[] aEnreg = lstVersion.Find(el => el[1] == sFind);
                    sGuidAppVersion = aEnreg[0];
                }
                refresh_cbVersion();
                if (sGuidAppVersion != "") cbVersion.SelectedIndex = lstVersion.FindIndex(el => el[0] == sGuidAppVersion) + 1;
            }
            bAction.Enabled = false;
        }

        private void cbApplication2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbApplication2.SelectedIndex >= 0)
            {
                tbGuidApplication2.Text = lstApp[cbApplication2.SelectedIndex][0];
                tbTrigramme2.Text = lstApp[cbApplication2.SelectedIndex][2];
            }
        }

        private void cbVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            bAction.Enabled = true;
            iLastSelectedIndex = cbVersion.SelectedIndex;
            if (cbVersion.SelectedIndex >= 0)
            {
                switch(indexOp)
                {
                    case 1: // Modif App
                        if (cbVersion.SelectedIndex > 0) bAction.Text = "Update";
                        else bAction.Text = "Add";
                        break;
                    case 3: // Copy Ver

                        break;
                }
                
            }
            else bAction.Text = "";
        }

        private void ChangeObjet(XmlDB xmlDB, XmlElement elAfterAppVersion, XmlElement elCur, string sObj, List<string[]> lstChangeObjet)
        {
            string sGuid = xmlDB.XmlGetAttValueAFromAttValueB(elCur, "Value", "Nom", "Guid" + sObj);
            if (sGuid != "")
            {
                // vérifier si l'objet n'a pas été déjà changé dans la liste

                if (lstChangeObjet.Find(elFind => elFind[1] == (string)sGuid) == null)
                {
                    string sNewGuid = Guid.NewGuid().ToString();
                    xmlDB.XmlSetAllAttNewValue(elAfterAppVersion, "Guid" + sObj, sGuid, sNewGuid);
                    xmlDB.XmlSetAllAttNewValue(elCur, "GuidObject", sGuid, sNewGuid);
                    xmlDB.XmlSetAllAttNewValue(elCur, "GuidObjet", sGuid, sNewGuid);

                    //Pour les modules
                    xmlDB.XmlSetAllAttNewValue(elAfterAppVersion, "GuidModuleIn", sGuid, sNewGuid);
                    xmlDB.XmlSetAllAttNewValue(elAfterAppVersion, "GuidModuleOut", sGuid, sNewGuid);

                    // Pour les composants, les interfaces, les bases, les files
                    xmlDB.XmlSetAllAttNewValue(elAfterAppVersion, "GuidComposantL1In", sGuid, sNewGuid);
                    xmlDB.XmlSetAllAttNewValue(elAfterAppVersion, "GuidComposantL1Out", sGuid, sNewGuid);

                    // Pour les servers
                    xmlDB.XmlSetAllAttNewValue(elAfterAppVersion, "GuidServerIn", sGuid, sNewGuid);
                    xmlDB.XmlSetAllAttNewValue(elAfterAppVersion, "GuidServerOut", sGuid, sNewGuid);


                    //ChangeGObjet(xmlDB, elCur, sObj);

                    // enregistrer l'objet changé
                    string[] aObj = new string[2];
                    aObj[0] = sObj;
                    aObj[1] = sNewGuid;
                    lstChangeObjet.Add(aObj);
                    sGuid = sNewGuid;
                }
            }
            //Change le Text de objet
            xmlDB.XmlSetTexteFromEl(elCur, sGuid);
        }

        /*
        private void ChangeGObjet(XmlDB xmlDB, XmlElement elCur, string sObj)
        {
            
            string sNewGuidG = Guid.NewGuid().ToString();
            string sGuidG = xmlDB.XmlGetAttValueAFromAttValueB(elCur, "Value", "Nom", "GuidG" + sObj);
            xmlDB.XmlSetAllAttNewValue(elCur, "GuidG" + sObj, sGuidG, sNewGuidG);
            xmlDB.XmlSetAllAttNewValue(elCur, "GuidObjet", sGuidG, sNewGuidG);
            xmlDB.XmlSetAllAttNewValue(elCur, "GuidGObjet", sGuidG, sNewGuidG);
            
        }
        */


        private void CopyVue(XmlDB xmlDB, XmlElement elVue, XmlElement elGlobal, List<string[]> lstChangeObjet)
        {

            string sGuidVue = xmlDB.XmlGetAttValueAFromAttValueB(elVue, "Value", "Nom", "GuidVue");
            string sGuidGVue = xmlDB.XmlGetAttValueAFromAttValueB(elVue, "Value", "Nom", "GuidGVue");
            //string sGuidTypeVue = xmlDB.XmlGetAttValueAFromAttValueB(elVue, "Value", "Nom", "GuidTypeVue");
            string sNewGuidVue = Guid.NewGuid().ToString();
            string sNewGuidGVue = Guid.NewGuid().ToString();
            // change le GuidVue
            xmlDB.XmlSetAllAttNewValue(elGlobal, "GuidVue", sGuidVue, sNewGuidVue);
            xmlDB.XmlSetAllAttNewValue(elGlobal, "GuidVueInf", sGuidVue, sNewGuidVue);
            xmlDB.XmlSetAllAttNewValue(elVue, "GuidObject", sGuidVue, sNewGuidVue);
            // change le GuidGVue
            xmlDB.XmlSetAllAttNewValue(elVue, "GuidGVue", sGuidGVue, sNewGuidGVue);
            // change Texte
            xmlDB.XmlSetTexteFromEl(elVue, sNewGuidGVue);

            XmlElement elAfterVue = xmlDB.XmlGetFirstElFromParent(elVue, "After");
            IEnumerator ienum = elAfterVue.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    XmlElement elCur = (XmlElement)Node;

                    Table t;
                    int n = Parent.oCnxBase.ConfDB.FindTable(elCur.Name);
                    if (n > -1)
                    {
                        t = (Table)Parent.oCnxBase.ConfDB.LstTable[n];

                        //string sKey = t.GetKey();
                        switch (t.Name)
                        {
                            case "Module":
                            case  "AppUser" :
                            case "Link":
                            case "MainComposant":
                            case "Composant":
                            case "Base":
                            case "Interface":
                            case "File":
                            case "Server":
                            case "TechLink":
                            case "InterLink":
                            case "MCompServ":
                                ChangeObjet(xmlDB, elGlobal, elCur, t.Name, lstChangeObjet);
                                break;
                        }
                    }
                }
            }
        }

        private bool GoCopyVesion(string sGuidApplication, string sGuidAppVersion1, string sGuidAppVersion2)
        {
            //List<DrawObject> lstObjet = new List<DrawObject>();
            //Parent.drawArea.tools[(int)DrawArea.DrawToolType.Vue].LoadObjecttoList(" Where GuidAppVersion='" + sGuidAppVersion1 + "'");
            //for (int i = 0; i < Parent.lstObject.Count; i++) lstObjet.Add(Parent.lstObject[i]);
            //Parent.lstObject.Clear();



            XmlDB xmlDB = new XmlDB(Parent, "Applications");
            ArrayList lstVues = new ArrayList();
            XmlElement el=null;
            XmlElement elAfterAppVersion = null;
            List<string[]> lstChangeObjet = new List<string[]>();

            if (sGuidApplication != null && sGuidAppVersion1 != null && sGuidAppVersion2 != null)
            {
                Parent.wkApp = new WorkApplication(Parent, sGuidApplication, "", sGuidAppVersion1);
                Parent.XmlCreatXmldb(xmlDB, sGuidApplication, sGuidAppVersion1);
                if (xmlDB == null) return false;

                xmlDB.docXml.Save(Parent.GetFullPath(Parent.wkApp) + "\\" + "app" + ".xml");

                el = xmlDB.XmlGetElFromInnerText(xmlDB.root, Parent.wkApp.Guid.ToString());
                if(!xmlDB.SetCursor(xmlDB.XmlGetFirstElFromName(el, "AppVersion"))) return false;

                // change le GuidAppVersion
                xmlDB.XmlSetAllAttNewValue(xmlDB.GetCursor(), "GuidAppVersion", sGuidAppVersion1, sGuidAppVersion2);
                
                // supprime la version
                el = xmlDB.XmlFindElFromAtt(xmlDB.GetCursor(), "Nom", "Version");

                if (el == null) return false;
                el.ParentNode.RemoveChild(el);

                elAfterAppVersion = xmlDB.XmlGetFirstElFromParent(xmlDB.GetCursor(), "After");
                xmlDB.CursorClose();
                //if (!xmlDB.SetCursor(elAfterAppVersion)) return false;
                lstVues = Parent.XmlGetLstElFromName(elAfterAppVersion, "Vue", 1);

                for (int i = 0; i < lstVues.Count; i++)
                    xmlDB.XmlCopyVue((XmlElement)lstVues[i], elAfterAppVersion, lstChangeObjet);

                xmlDB.docXml.Save(Parent.GetFullPath(Parent.wkApp) + "\\" + "appNew" + ".xml");

                xmlDB.ImportXml(xmlDB.docXml.DocumentElement);

                Parent.wkApp = null;
            }

            return true;
        }

        private void ImportXml(XmlElement elParent)
        {
            //Att bug sur l'appel de la fonction deletencardlink : delete tout les enreg lies a la carte : il faut tenir compte des autres vue

            IEnumerator ienum = elParent.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                switch (Node.NodeType)
                {
                    case XmlNodeType.Element:
                        Parent.oCnxBase.CreatEnregFromXml((XmlElement)Node);
                        ImportXml((XmlElement)Node);
                        break;
                }
            }
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            switch (indexOp)
            {
                case 0:
                case 1:
                    if (tbNom.Text.Length == 0 || tbCode.Text.Length == 0 || tbTrigramme.Text.Length == 0)
                        MessageBox.Show("Les informations obligatoires sont : Nom, Trigramme, Code");
                    else
                    {
                        string sGuidApp = sGuidAppVersion == "" ? null : sGuidAppVersion;
                        if (!Parent.oCnxBase.CBRecherche("SELECT GuidApplication FROM Application where GuidApplication ='" + tbGuidApp.Text + "'"))
                        {
                            Parent.oCnxBase.CBReaderClose();
                            Parent.oCnxBase.CBWrite("INSERT INTO Application (GuidApplication, NomApplication, Trigramme, GuidAppVersion, GuidArborescence) VALUES ('" + tbGuidApp.Text + "','" + tbNom.Text + "','" + tbTrigramme.Text + "','" + sGuidApp + "','" + tbCode.Text + "')");
                            Parent.oCnxBase.CBWrite("INSERT INTO DansTypeVue (GuidTypeVue, GuidObjet, TypeObjet) VALUES ('49c88d3d-f32f-44fe-ad6c-35977c5b812e','" + tbGuidApp.Text + "','Application')");
                        }
                        else
                        {
                            Parent.oCnxBase.CBReaderClose();
                            Parent.oCnxBase.CBWrite("UPDATE Application SET NomApplication='" + tbNom.Text + "', Trigramme='" + tbTrigramme.Text + "',  GuidArborescence='" + tbCode.Text + "', GuidAppVersion='" + sGuidApp + "' WHERE GuidApplication = '" + tbGuidApp.Text + "'");
                        }
                        for (int i = 0; i < lstVersion.Count; i++)
                        {
                            string[] aEnreg = lstVersion[i];
                            if (!Parent.oCnxBase.CBRecherche("SELECT GuidAppVersion FROM AppVersion where GuidAppVersion ='" + aEnreg[0] + "'"))
                            {
                                Parent.oCnxBase.CBReaderClose();
                                Parent.oCnxBase.CBWrite("INSERT INTO AppVersion (GuidAppVersion, Version, GuidApplication) VALUES ('" + aEnreg[0] + "','" + aEnreg[1] + "','" + tbGuidApp.Text + "')");
                            }
                            else
                            {
                                Parent.oCnxBase.CBReaderClose();
                                Parent.oCnxBase.CBWrite("UPDATE AppVersion SET Version='" + aEnreg[1] + "' WHERE GuidAppVersion = '" + aEnreg[0] + "'");
                            }

                        }
                        Parent.InitCbApplication();

                        // fill combobox
                        Parent.cbApplication.SelectedItem = $"{tbNom.Text} [{ tbTrigramme.Text}]";

                        if(lstVersion.Count != 0)
                        {
                            Parent.cbVersion.SelectedItem = cbVersion.SelectedItem;
                        }

                        Close();
                    }
                    break;
                case 2: // Copy App
                    break;
                case 3: // Copy Ver
                    if (cbVersion.SelectedIndex != -1 && cbVersion2.SelectedIndex != -1)
                    {
                        string msg = "Attention...\nVous allez copier la version " + cbVersion.SelectedItem + " vers la version " + cbVersion2.SelectedItem + ".\nTous les objets de la version " + cbVersion2.SelectedItem + " existant seront supprimés.\n";
                        msg += "Voulez-vous continuer?";
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        DialogResult result;
                        result = MessageBox.Show(msg, "suppression", buttons);

                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            string[] aEnregVer1 = lstVersion.Find(el => el[1] == (string)cbVersion.Items[cbVersion.SelectedIndex]);
                            string[] aEnregVer2 = lstVersion.Find(el => el[1] == (string)cbVersion2.Items[cbVersion2.SelectedIndex]);
                            GoCopyVesion(tbGuidApp.Text, aEnregVer1[0], aEnregVer2[0]);
                        }
                    }

                    break;
                case 4: // Move Ver
                    break;
            }
            Close();
        }
    }
}
