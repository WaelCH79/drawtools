using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using HtmlAgilityPack;


namespace DrawTools
{
    public partial class FormTadUpdating : Form
    {
        public enum rbTypeDocument
        {
            Vide = 0x00,
            Tad = 0x01,
            Cat = 0x02,
        }

        private Form1 parent;
        private ControlDoc cw;
        private string fullpath;
        private rbTypeDocument rbTypeDoc;
        private List<string[]> lstAppVersion;
        
        public new Form1 Parent
        {
            get { return parent; }
            set { parent = value;}
        }

        public FormTadUpdating(Form1 p)
        {
            Parent = p;
            cw = null;
            fullpath = null;
            lstAppVersion = new List<string[]>();
            
            InitializeComponent();

            DateTime dToday = DateTime.Now;
            tbVersion.Text = dToday.ToString("yyyy") + dToday.ToString("MM") + dToday.ToString("dd");
        }

        void chkTemplate_CheckedChanged(object sender, System.EventArgs e)
        {
            if (chkTemplate.Checked) lstTad.Enabled = false;
            else lstTad.Enabled = true;
        }

        void cbApp_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            cbVersion.Text = ""; cbVersion.Items.Clear();
            lstAppVersion.Clear();

            if (cbApp.SelectedIndex != -1)
            {
                if (Parent.oCnxBase.CBRecherche("SELECT AppVersion.GuidAppVersion, Version, GuidVue, NomVue FROM Vue, AppVersion Where GuidApplication = '" + (string)cbGuid.Items[cbApp.SelectedIndex] + "' and AppVersion.GuidAppVersion = Vue.GuidAppVersion ORDER BY Version, NomVue"))
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

            //Parent.SetGuidApplication(new Guid((string)cbApp.SelectedItem));
            
        }

        void cbVersion_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cbApp.SelectedIndex != -1 && cbVersion.SelectedIndex != -1)
            {
                
                string[] aEnreg = lstAppVersion.Find(el => el[1] == (string)cbVersion.Items[cbVersion.SelectedIndex]);
                Parent.wkApp = new WorkApplication(Parent, (string)cbGuid.Items[cbApp.SelectedIndex], (string)cbApp.SelectedItem, aEnreg[0]);
                fullpath = Parent.GetFullPath(Parent.wkApp);
                tbPath.Text = fullpath + "\\cat\\";
                init_lstTad();
                

            }
        }

        void init_lstTad()
        {
            if (tbPath.Text != "" && chkTemplate.Checked)
            {
                fullpath = Parent.GetFullPath(Parent.wkApp);
                if (fullpath != null)
                {
                    string extention = "";
                    if (rbTypeDoc == rbTypeDocument.Tad) extention = "*.doc?"; else extention = "*.pp*";
                    lstTad.Items.Clear();
                    try
                    {
                        string[] docfiles = System.IO.Directory.GetFiles(fullpath + "\\cat\\", extention);
                        foreach (string file in docfiles)
                        {
                            lstTad.Items.Add(file.Substring(file.LastIndexOf('\\') + 1));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public void init()
        {
            //string fullpath = Parent.GetFullPath;
            rbTadDoc.Checked = true;
            rbHtmlDoc.Checked = true;
            rbTypeDoc = rbTypeDocument.Tad;
            Parent.oCnxBase.CBAddComboBox("SELECT GuidApplication, NomApplication FROM Application ORDER BY NomApplication", this.cbGuid, this.cbApp);
            //Parent.oCnxBase.CBAddComboBox("SELECT NomApplication FROM Application", cbApp);
            

            ShowDialog(Parent);
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            if (tbVersion.Text == "") MessageBox.Show("Le champ Version doit être renseigné");
            else
            {
                if (rbTypeDoc == rbTypeDocument.Tad) CreatTad(); else CreatCat();
            }
            Parent.initVarApp();
            Close();
        }

        private void CreatCat()
        {
            ArrayList lstGuidVue = new ArrayList();
            ArrayList lstVues = new ArrayList();
            string sGuidVueInf = "";

            if (fullpath != null)
            {
                ControlPPT oppt = null;
                oppt = new ControlPPT(Parent, Parent.sPathRoot + @"\cat_modele-V3.R2-Eng.potx", Microsoft.Office.Core.MsoTriState.msoTrue);
                cw = (ControlDoc)oppt;
                if (cw != null)
                {
                    int iSlide, iShape;
                    stpptStyle pptS;
                    stpptforme pptF;

                    string sEnv = "";

                    XmlDB xmlDB = new XmlDB(Parent, "Applications");

                    Parent.XmlCreatXmldb(xmlDB, Parent.wkApp.Guid.ToString(), Parent.wkApp.GuidAppVersion.ToString());
                    xmlDB.docXml.Save("c:\\dat\\xmlDB.xml");
                    XmlElement elApp = xmlDB.XmlGetElFromInnerText(xmlDB.root, Parent.wkApp.Guid.ToString());

                    XmlElement elAppVersion = xmlDB.XmlGetFirstElFromName(elApp, "AppVersion");
                    XmlElement elAfterAppVersion = xmlDB.XmlGetFirstElFromParent(elAppVersion, "After");
                    lstVues = Parent.XmlGetLstElFromName(elAfterAppVersion, "Vue", 1);
                    
                    if (!Parent.oCnxBase.CBRecherche("SELECT GuidVue, GuidGVue, NomVue, NomTypeVue, GuidAppVersion, Vue.GuidTypeVue,  GuidEnvironnement FROM Vue, typeVue WHERE Vue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidAppVersion ='" + Parent.wkApp.GuidAppVersion + "' Order by NomTypeVue"))
                    {
                        Parent.oCnxBase.CBReaderClose();
                    }
                    else
                    {
                        // (0) GuidVue, (1) GuidGVue (2) NomVue, (3) NomTypeVue, (4) GuidAppVersion, (5) GuidTypeVue, (6) GuidEnvironnement
                        while (Parent.oCnxBase.Reader.Read())
                        {
                            if (Parent.oCnxBase.Reader.GetString(4) == "d5b533a9-06ac-4f8c-a5ab-e345b0212542") sGuidVueInf = Parent.oCnxBase.Reader.GetString(0);
                            if (!Parent.oCnxBase.Reader.IsDBNull(6)) sEnv = Parent.oCnxBase.Reader.GetString(6);
                            lstGuidVue.Add((object)(Parent.oCnxBase.Reader.GetString(0) + "," + Parent.oCnxBase.Reader.GetString(1) + "," + Parent.oCnxBase.Reader.GetString(2) + "," + Parent.oCnxBase.Reader.GetString(3) + "," + Parent.oCnxBase.Reader.GetString(4) + "," + Parent.oCnxBase.Reader.GetString(5) + "," + sEnv));
                        }
                        Parent.oCnxBase.CBReaderClose();

                        pptS.size = 18; pptS.bold = Microsoft.Office.Core.MsoTriState.msoTrue; pptS.Couleur = System.Drawing.Color.White.ToArgb();
                        cw.InsertTextFromId("1,1", true, "Nanterre, " + DateTime.Now.ToLongDateString(), (object)pptS);
                        //pptS.size = 32;
                        //cw.InsertTextFromId("1,1", false, "application", (object)pptS);
                        pptS.size = 25;
                        cw.InsertTextFromId("1,3", true, Parent.wkApp.Application + "\nVersion 1.0", (object)pptS);
                        iSlide = (int)cw.CreatSlide("sujet", "Description & Propriétés");

                        parent.oCureo = new ExpObj(Parent.wkApp.Guid, Parent.wkApp.Application, DrawArea.DrawToolType.Application);
                        Parent.drawArea.tools[(int)parent.oCureo.ObjTool].LoadSimpleObjectSansGraph(parent.oCureo);

                        
                        if (parent.oCureo.oDraw != null)
                        {
                            pptF.x = 35; pptF.y = 85; pptF.width = 670; pptF.height = 185;
                            iShape = (int)cw.InsertFormeFromId(iSlide.ToString(), true, "Description",  (object)pptF);

                            DrawObject o = parent.oCureo.oDraw;
                            XmlDB xmlDB1 = new XmlDB(parent, "Applications");
                            if (xmlDB.SetCursor("root"))
                            {
                                o.XmlCreatObject(xmlDB);
                                xmlDB.CursorClose();
                            }
                            byte[] rawData = o.GetrawData("Description");
                            if (rawData != null)
                            {
                                System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
                                rtBox.Rtf = System.Text.Encoding.UTF8.GetString(rawData);
                                pptS.size = 10; pptS.bold = Microsoft.Office.Core.MsoTriState.msoFalse; pptS.Couleur = System.Drawing.Color.DarkSlateGray.ToArgb();
                                cw.InsertTextFromId(iSlide.ToString()+","+iShape.ToString(),  true, rtBox.Text, (object)pptS);
                            }
                            pptF.x = 35; pptF.y = 345; pptF.width = 670; pptF.height = 125;
                            iShape = (int)cw.InsertFormeFromId(iSlide.ToString(), true, "Propriété", (object)pptF);

                            pptS.size = 8; pptS.bold = Microsoft.Office.Core.MsoTriState.msoFalse; pptS.Couleur = System.Drawing.Color.DarkSlateGray.ToArgb();
                            Parent.oCnxBase.ConfDB.AddTabAppforCAT();
                            cw.InsertTabFromId(iSlide.ToString() + "," + iShape.ToString(), true, Parent.wkApp.Guid.ToString(), xmlDB, (object)pptS, "TabAppforCAT");

                            parent.oCureo = null;
                        }
                    }

                    /*iSlide = cp.GetSlideFromTag("Secu");
                    if (iSlide != -1)
                    {
                        cp.AddSlide_CreatTitle(iSlide + 1, "Synthèse Sécurité");
                    }

                    iSlide = cp.GetSlideFromTag("Proj");
                    if (iSlide != -1)
                    {
                        cp.AddSlide_CreatTitle(iSlide + 1, "Focus Projet");
                    }*/

                    ArrayList lstAssetGlobalGuid = new ArrayList();
                    for (int i = 0; i < lstVues.Count; i++)
                    {
                        string[] fields = ((string)lstGuidVue[i]).Split(',');
                        // (0) GuidVue, (1) GuidGVue (2) NomVue, (3) NomTypeVue, (4) GuidAppVersion, (5) GuidTypeVue, (6) GuidEnvironnement


                        Parent.GuidVue = new Guid(xmlDB.GetAttr(lstVues[i], "GuidVue"));
                        Parent.tbTypeVue.Text = xmlDB.GetAttr(lstVues[i], "NomTypeVue");

                        Parent.GuidVue = new Guid(fields[0]);
                        Guid guid = Parent.GuidVue;
                        Parent.GuidGVue = new Guid(fields[1]);
                        Parent.sTypeVue = fields[3];
                        string sVueBookmark = "n" + fields[2];
                        Parent.cbGuidVue.Items.Add(Parent.GuidVue); Parent.cbGuidVue.SelectedIndex = 0; // GuidVue
                        Parent.cbVue.Items.Add(fields[2]); Parent.cbVue.SelectedIndex = 0; //NomVue

                        
                        Parent.LoadVue();
                        string sDiagram = parent.SaveDiagram(fields[2], Parent.wkApp, "");

                        switch (Parent.tbTypeVue.Text[0])
                        {
                            case '0': //0-Fonctionnelle   Diagf3fa584ded554e14831e0d0d3acded8e
                                ArrayList lstUser = new ArrayList();
                                iSlide = cw.Exist("fonc" + i);
                                if (iSlide == -1)
                                {
                                    iSlide = (int)cw.CreatSlide("fonc" + i, "Schéma fonctionnel");
                                    pptF.x = 100; pptF.y = 70; pptF.width = 590; pptF.height = 280;
                                    cw.InsertImgFromId(iSlide.ToString(), true, sDiagram, pptF);
                                    pptF.x = 35; pptF.y = 370; pptF.width = 325; pptF.height = 105;
                                    iShape = (int)cw.InsertFormeFromId(iSlide.ToString(), true, "Utilisateurs", (object)pptF);
                                    pptS.size = 10; pptS.bold = Microsoft.Office.Core.MsoTriState.msoFalse; pptS.Couleur = System.Drawing.Color.DarkSlateGray.ToArgb();
                                    cw.InsertTextFromId(iSlide.ToString() + "," + iShape.ToString(), true, xmlDB.GetObjetComment(guid.ToString(), "User", "txt"), (object)pptS);

                                    pptF.x = 380; pptF.y = 370; pptF.width = 325; pptF.height = 105;
                                    iShape = (int)cw.InsertFormeFromId(iSlide.ToString(), true, "Modules fonctionnelles", (object)pptF);
                                    pptS.size = 10; pptS.bold = Microsoft.Office.Core.MsoTriState.msoFalse; pptS.Couleur = System.Drawing.Color.DarkSlateGray.ToArgb();
                                    cw.InsertTextFromId(iSlide.ToString() + "," + iShape.ToString(), true, xmlDB.GetObjetComment(guid.ToString(), "Module", "txt"), (object)pptS);
                                }
                                break;
                            case '1': // 1-Applicative   Diag49c88d3df32f44fead6c35977c5b812e
                                iSlide = cw.Exist("app" + i);
                                if (iSlide == -1)
                                {
                                    iSlide = (int)cw.CreatSlide("app" + i, "Schéma applicatif");
                                    pptF.x = 50; pptF.y = 70; pptF.width = 650; pptF.height = 280;
                                    cw.InsertImgFromId(iSlide.ToString(), true, sDiagram, pptF);
                                    pptF.x = 35; pptF.y = 370; pptF.width = 325; pptF.height = 105;
                                    iShape = (int)cw.InsertFormeFromId(iSlide.ToString(), true, "Composants", (object)pptF);
                                    pptS.size = 10; pptS.bold = Microsoft.Office.Core.MsoTriState.msoFalse; pptS.Couleur = System.Drawing.Color.DarkSlateGray.ToArgb();
                                    cw.InsertTextFromId(iSlide.ToString() + "," + iShape.ToString(), true, xmlDB.GetObjetComment(guid.ToString(), "Container", "txt"), (object)pptS);
                                    ArrayList lstComposant = xmlDB.GetObjets(guid.ToString(), "MainComposant", 1);
                                    cw.InsertTextFromId(iSlide.ToString() + "," + iShape.ToString(), false, "L'application " + Parent.wkApp.Application + " s'articule autour de " + lstComposant.Count + " containers.", (object)pptS);

                                    pptF.x = 380; pptF.y = 370; pptF.width = 325; pptF.height = 105;
                                    iShape = (int)cw.InsertFormeFromId(iSlide.ToString(), true, "Dépendances", (object)pptF);
                                    pptS.size = 10; pptS.bold = Microsoft.Office.Core.MsoTriState.msoFalse; pptS.Couleur = System.Drawing.Color.DarkSlateGray.ToArgb();
                                    cw.InsertTextFromId(iSlide.ToString() + "," + iShape.ToString(), true, xmlDB.GetObjetComment(guid.ToString(), "Application", "txt"), (object)pptS);
                                    ArrayList lstApplication = xmlDB.GetObjets(guid.ToString(), "Application", 1);
                                    cw.InsertTextFromId(iSlide.ToString() + "," + iShape.ToString(), false, "L'application " + Parent.wkApp.Application + " s'interface avec " + lstApplication.Count + " applications externes.", (object)pptS);
                                }
                                break;
                            case '2': // 2-Infrastructure Diagd5b533a906ac4f8ca5abe345b0212542
                                iSlide = cw.Exist("inf" + i);
                                if (iSlide == -1)
                                {
                                    iSlide = (int)cw.CreatSlide("inf" + i, "Schéma d'infrastructure");
                                    pptF.x = 20; pptF.y = 70; pptF.width = 500; pptF.height = 400;
                                    cw.InsertImgFromId(iSlide.ToString(), true, sDiagram, pptF);
                                    pptF.x = 530; pptF.y = 80; pptF.width = 180; pptF.height = 190;
                                    iShape = (int)cw.InsertFormeFromId(iSlide.ToString(), true, "Techno. Obsolescentes", (object)pptF);
                                    pptS.size = 10; pptS.bold = Microsoft.Office.Core.MsoTriState.msoFalse; pptS.Couleur = System.Drawing.Color.DarkSlateGray.ToArgb();
                                    cw.InsertTextFromId(iSlide.ToString() + "," + iShape.ToString(), true, xmlDB.GetObjetComment(guid.ToString(), "MainComposant", "txt"), (object)pptS);
                                    ArrayList lstTechnoRef = xmlDB.GetObjets(guid.ToString(), "TechnoRef", 3);
                                    cw.InsertTextFromId(iSlide.ToString() + "," + iShape.ToString(), false, "L'application " + Parent.wkApp.Application + " utilise " + lstTechnoRef.Count + " Technologies.", (object)pptS);

                                    ArrayList lstTechnoRefTab = new ArrayList();
                                    {
                                        ArrayList lstFields = new ArrayList();
                                        lstFields.Add("Technologies"); lstFields.Add("Fin Decomm");
                                        lstTechnoRefTab.Add(lstFields);
                                    }
                                    for (int iTech = 0; iTech < lstTechnoRef.Count; iTech++)
                                    {
                                        XmlElement elTech = (XmlElement)lstTechnoRef[iTech];
                                        string sNomTechnoRef = Parent.XmlGetAttValueAFromAttValueB(elTech, "Value", "Nom", "NomTechnoRef");
                                        string sDateDecomm = Parent.XmlGetAttValueAFromAttValueB(elTech, "Value", "Nom", "DecommEnd");
                                        DateTime dTech = Convert.ToDateTime(sDateDecomm);
                                        if (DateTime.Now > new DateTime(dTech.Year == 1 ? dTech.Year: dTech.Year - 1, dTech.Month, dTech.Day))
                                        {
                                            ArrayList lstFields = new ArrayList();
                                            lstFields.Add(sNomTechnoRef); lstFields.Add(sDateDecomm);
                                            lstTechnoRefTab.Add(lstFields);
                                        }
                                    }
                                    pptS.size = 6; pptS.bold = Microsoft.Office.Core.MsoTriState.msoFalse; pptS.Couleur = System.Drawing.Color.DarkSlateGray.ToArgb();
                                    cw.InsertTabFromId(iSlide.ToString() + "," + iShape.ToString(), true, lstTechnoRefTab, (object)pptS, null);

                                    pptF.x = 530; pptF.y = 285; pptF.width = 180; pptF.height = 190;
                                    iShape = (int)cw.InsertFormeFromId(iSlide.ToString(), true, "Flux", (object)pptF);
                                    ArrayList lstTechLink = xmlDB.GetObjets(guid.ToString(), "TechLink", 1);
                                    ArrayList lstFlux = new ArrayList();
                                    for (int iFlux = 0; iFlux < lstTechLink.Count; iFlux++)
                                    {
                                        XmlElement elFlux = (XmlElement)lstTechLink[iFlux];
                                        string sNomFonction = Parent.XmlGetAttValueAFromAttValueB(elFlux, "Value", "Nom", "NomFonctionService");
                                        if (lstFlux.IndexOf(sNomFonction)==-1)
                                        {
                                            lstFlux.Add(sNomFonction);
                                        }
                                    }
                                    if(lstFlux.Count>0)
                                    {
                                        string sFlux = "Les flux se décomposent en " + lstFlux.Count + " catégories:";
                                        for(int iFlux = 0; iFlux < lstFlux.Count; iFlux++)
                                        {
                                            sFlux += "\n- " + lstFlux[iFlux];
                                        }
                                        pptS.size = 8; pptS.bold = Microsoft.Office.Core.MsoTriState.msoFalse; pptS.Couleur = System.Drawing.Color.DarkSlateGray.ToArgb();
                                        cw.InsertTextFromId(iSlide.ToString() + "," + iShape.ToString(), true, sFlux, (object)pptS);
                                    }
                                }
                                break;
                            case '6': // 6-Sites   Diag86d1385eb293433ebb6cb2f8fa25b116
                                iSlide = cw.Exist("realSite" + i);
                                if (iSlide == -1)
                                {
                                    iSlide = (int)cw.CreatSlide("realSite" + i, "Schéma d'implémentation");
                                    pptF.x = 50; pptF.y = 50; pptF.width = 680; pptF.height = 280;
                                    cw.InsertImgFromId(iSlide.ToString(), true, sDiagram, pptF);
                                    pptF.x = 35; pptF.y = 370; pptF.width = 670; pptF.height = 115;
                                    iShape = (int)cw.InsertFormeFromId(iSlide.ToString(), true, "Flux", (object)pptF);
                                }
                                break;
                            case '3': // 3-Production   Diag2a4c3691e7144d0594008fbbb06f2d62
                                {
                                    ArrayList lstAssetGuid = xmlDB.GetObjets(guid.ToString(), "ServerPhy", 1);
                                    for (int iAsset = 0; iAsset < lstAssetGuid.Count; iAsset++) lstAssetGlobalGuid.Add(lstAssetGuid[iAsset]); 
                                }
                                iSlide = cw.Exist("realProd" + i);
                                if (iSlide == -1)
                                {
                                    iSlide = (int)cw.CreatSlide("realProd"+ i, "Déploiement de l'environnement Production");
                                    pptF.x = 50; pptF.y = 70; pptF.width = 660; pptF.height = 400;
                                    cw.InsertImgFromId(iSlide.ToString(), true, sDiagram, pptF);
                                }
                                break;
                            case '5': // 5-Pre-Production   Diag7afca9459d4148fbb6345b6ffda90d4e
                                {
                                    ArrayList lstAssetGuid = xmlDB.GetObjets(guid.ToString(), "ServerPhy", 1);
                                    for (int iAsset = 0; iAsset < lstAssetGuid.Count; iAsset++) lstAssetGlobalGuid.Add(lstAssetGuid[iAsset]);
                                }
                                iSlide = cw.Exist("realPProd" + i);
                                if (iSlide == -1)
                                {
                                    iSlide = (int)cw.CreatSlide("realPProd" + i, "Déploiement de l'environnement Preproduction");
                                    pptF.x = 50; pptF.y = 70; pptF.width = 660; pptF.height = 400;
                                    cw.InsertImgFromId(iSlide.ToString(), true, sDiagram, pptF);
                                }
                                break;
                            case '4': // 4-Hors Production   Diagef667e58a61749fd91a82beeda856475
                                {
                                    ArrayList lstAssetGuid = xmlDB.GetObjets(guid.ToString(), "ServerPhy", 1);
                                    for (int iAsset = 0; iAsset < lstAssetGuid.Count; iAsset++) lstAssetGlobalGuid.Add(lstAssetGuid[iAsset]);
                                }
                                iSlide = cw.Exist("realHProd" + i);
                                if (iSlide == -1)
                                {
                                    iSlide = (int)cw.CreatSlide("realHProd" + i, "Déploiement de l'environnement Horsproduction");
                                    pptF.x = 50; pptF.y = 70; pptF.width = 660; pptF.height = 400;
                                    cw.InsertImgFromId(iSlide.ToString(), true, sDiagram, pptF);
                                }
                                break;
                            //case 'F': // F-Service Infra   Diag011f72c9c9d04800af3aafd174a49c93
                            //    iSlide = cp.GetSlideFromTag("real");
                            //    if (iSlide != -1)
                            //    {
                            //        cp.AddSlide_CreatTitle(iSlide + 1, "Déploiement des environnements service d'infrastructure");
                            //        cp.AddPicture(iSlide + 1, sDiagram, 50, 90, 660, 400);
                            //   }
                            //    break;
                            case '8': // 8-ZoningProd   Diagad05bb2da9ce4a4f9beb61d8d09c52e6
                            case '7': // 7-ZoningHorsProd   Diagc1a91afa2a6e40ac985d75dfa424c3da
                                break;
                            case 'A': // A-SanProd   Diagafa4d849f7d64692bc0e8b2b79c246fa
                            case '9': // 9-SanHorsProd   Diag9066daac8b184a269908d78fb078c84a
                                break;
                            case 'C': // C-CTIProd   Diagd5a9652be99b48f4a892d4dffc35935d
                            case 'B': // B-CTIHorsProd   Diagd5b677fc031041e0b4bcc5a777b04823
                                break;
                        }

                        Parent.drawArea.GraphicsList.Clear();
                    }
                    // summary des vues de déploiement
                    iSlide = cw.Exist("realTab");
                    if (iSlide == -1)
                    {
                        iSlide = (int)cw.CreatSlide("realTab", "Tabeau récapitulatif des déploiements");
                        pptF.x = 25; pptF.y = 80; pptF.width = 665; pptF.height = 390;
                        iShape = (int)cw.InsertFormeFromId(iSlide.ToString(), true, "Déploiement des assets", (object)pptF);

                        ArrayList lstAsset = new ArrayList();
                        {
                            ArrayList lstFields = new ArrayList();
                            lstFields.Add("Nom Asset"); lstFields.Add("Environnement");
                            lstAsset.Add(lstFields);
                        }
                        for (int iTech = 0; iTech < lstAssetGlobalGuid.Count; iTech++)
                        {
                            XmlElement elTech = (XmlElement)lstAssetGlobalGuid[iTech];
                            string sNomServerPhy = Parent.XmlGetAttValueAFromAttValueB(elTech, "Value", "Nom", "NomServerPhy");
                            string sDescription = Parent.XmlGetAttValueAFromAttValueB(elTech, "Value", "Nom", "Description");
                            ArrayList lstFields = new ArrayList();
                            lstFields.Add(sNomServerPhy); lstFields.Add(sDescription);
                            lstAsset.Add(lstFields);
                        }
                        pptS.size = 6; pptS.bold = Microsoft.Office.Core.MsoTriState.msoFalse; pptS.Couleur = System.Drawing.Color.DarkSlateGray.ToArgb();
                        cw.InsertTabFromId(iSlide.ToString() + "," + iShape.ToString(), true, lstAsset, (object)pptS, null);
                    }
                }
            }
        }

        private string GetVueInfFromVueDeploy()
        {

            return "";
        }

        private void CreatTad()
        {
            ArrayList lstGuidVue = new ArrayList();
            string sGuidVueInfra = "", sGuidGVueInfra = "";
            string sGuidAppVersion = "";
            string[] sGuidVueHProd = { "", "", "" };
            string sDocPath = "";

            if (fullpath != null && Parent.wkApp != null)
            {
                cw = null;

                if (rbDocDoc.Checked)
                {
                    ControlWord ocw = null;
                    if (chkTemplate.Checked) ocw = new ControlWord(Parent, Parent.sPathRoot + @"\dat_modele-V3R3-Eng.dotx", true);
                    else
                    {
                        if (lstTad.SelectedItem != null) ocw = new ControlWord(Parent, fullpath + "\\cat\\" + lstTad.SelectedItem, true);
                    }
                    cw = (ControlDoc)ocw;
                }
                else
                {
                    ControlHtml och = null;
                    och = new ControlHtml(Parent, Parent.sPathRoot + @"\dat.html");
                    cw = (ControlDoc)och;
                }


                if (cw != null)
                {
                    
                    if (!Parent.oCnxBase.CBRecherche("SELECT Vue.GuidVue, Vue.GuidGVue, Vue.NomVue, NomTypeVue, Vue.GuidAppVersion, Vue.GuidTypeVue,  Vue.GuidEnvironnement, Vue.GuidVueInf, VueMoins1.GuidGVue GuidGVueMoins1 FROM Vue  Left Join Vue VueMoins1 On Vue.GuidVueInf=VueMoins1.GuidVue, typeVue WHERE Vue.GuidTypeVue=TypeVue.GuidTypeVue AND Vue.GuidAppVersion ='" + Parent.wkApp.GuidAppVersion + "' Order by NomTypeVue"))
                    // (0)GuidVue, (1)GuidGVue, (2)NomVue, (3)NomTypeVue, (4)GuidAppVersion, (5)GuidTypeVue, (6)GuidEnvironnement, (7) GuidVueInf, (8) GuidGVueMoins1
                    {
                        Parent.oCnxBase.CBReaderClose();
                    }
                    else
                    {
                        sDocPath = fullpath + "\\cat\\" + tbVersion.Text;
                        cw.setDocPath(sDocPath);
                        while (Parent.oCnxBase.Reader.Read())
                        {
                            if (Parent.oCnxBase.Reader.GetString(5) == "d5b533a9-06ac-4f8c-a5ab-e345b0212542") { sGuidVueInfra = Parent.oCnxBase.Reader.GetString(0); sGuidGVueInfra = Parent.oCnxBase.Reader.GetString(1); }
                            string sEnv = "", sGuidVueMoins1= "", sGuidGVueMoins1 = "";
                            if (!Parent.oCnxBase.Reader.IsDBNull(6)) sEnv = Parent.oCnxBase.Reader.GetString(5);
                            if (!Parent.oCnxBase.Reader.IsDBNull(7)) sGuidVueMoins1 = Parent.oCnxBase.Reader.GetString(7);
                            if (!Parent.oCnxBase.Reader.IsDBNull(8)) sGuidGVueMoins1 = Parent.oCnxBase.Reader.GetString(8);
                            lstGuidVue.Add((object)(Parent.oCnxBase.Reader.GetString(0) + "," + Parent.oCnxBase.Reader.GetString(1) + "," + Parent.oCnxBase.Reader.GetString(2) + "," + Parent.oCnxBase.Reader.GetString(3) + "," + Parent.oCnxBase.Reader.GetString(4) + "," + Parent.oCnxBase.Reader.GetString(5) + "," + sEnv + "," + sGuidVueMoins1 + "," + sGuidGVueMoins1));
                            sGuidAppVersion = Parent.oCnxBase.Reader.GetString(4);
                        }
                        Parent.oCnxBase.CBReaderClose();
                    }

                    //Insert Titre
                    if (cw.Exist("Title") > -1)
                        cw.InsertTextFromId("Title", false, (string)cbApp.SelectedItem + "\n", "Titre 0");
                    if (cw.Exist("Version") > -1)
                        cw.InsertTextFromId("Version", false, "version: " + cbVersion.Text + "/" + tbVersion.Text, "Titre 0");

                    //Chargement des Tables specifiques
                    string sType;
                    Parent.oCnxBase.ConfDB.AddTabVersion();
                    Parent.oCnxBase.ConfDB.AddTabSan();
                    Parent.oCnxBase.ConfDB.AddTabEACB();
                    Parent.oCnxBase.ConfDB.AddTabServerList();


                    //Entete Tableau Link chapitre Applicatif
                    sType = "Link";
                    if (cw.Exist(sType + "1") > -1)
                    {
                        int n = Parent.oCnxBase.ConfDB.FindTable(sType);
                        if (n > -1)
                        {
                            Table t = (Table)Parent.oCnxBase.ConfDB.LstTable[n];
                            cw.InsertHeadTabFromId(sType + "1", true, t, null);
                        }
                    }

                    //Entete Tableau TechLink chapitre Infra
                    sType = "TechLink";
                    if (cw.Exist(sType + "2") > -1)
                    {
                        int n = Parent.oCnxBase.ConfDB.FindTable(sType);
                        if (n > -1)
                        {
                            Table t = (Table)Parent.oCnxBase.ConfDB.LstTable[n];
                            cw.InsertHeadTabFromId(sType + "2", true, t, null);
                        }
                    }

                    //Entete Tableau Zoning Prod (8-Prod) & HorsProd (7-HorsProd)
                    sType = "Zone";
                    if (cw.Exist(sType + "8") > -1)
                    {
                        int n = Parent.oCnxBase.ConfDB.FindTable(sType);
                        if (n > -1)
                        {
                            Table t = (Table)Parent.oCnxBase.ConfDB.LstTable[n];
                            cw.InsertHeadTabFromId(sType + "8", true, t, null);
                        }
                    }
                    if (cw.Exist(sType + "7") > -1)
                    {
                        int n = Parent.oCnxBase.ConfDB.FindTable(sType);
                        if (n > -1)
                        {
                            Table t = (Table)Parent.oCnxBase.ConfDB.LstTable[n];
                            cw.InsertHeadTabFromId(sType + "7", true, t, null);
                        }
                    }

                    //Entete Tableau San Prod (8-Prod) & HorsProd (7-HorsProd)
                    sType = "San";
                    if (cw.Exist(sType + "A") > -1)
                    {

                        int n = Parent.oCnxBase.ConfDB.FindTable("TabSan");
                        if (n > -1)
                        {
                            Table t = (Table)Parent.oCnxBase.ConfDB.LstTable[n];
                            cw.InsertHeadTabFromId(sType + "A", true, t, null);
                        }
                    }
                    if (cw.Exist(sType + "9") > -1)
                    {
                        int n = Parent.oCnxBase.ConfDB.FindTable("TabSan");
                        if (n > -1)
                        {
                            Table t = (Table)Parent.oCnxBase.ConfDB.LstTable[n];
                            cw.InsertHeadTabFromId(sType + "9", true, t, null);
                        }
                    }

                    //Description de l'application sous le titre Enterprise Architecture Study
                    if (cw.Exist("AppDescription") > -1)
                    {
                        // charge l'objet Application
                        Parent.oCureo = new ExpObj(Parent.wkApp.Guid, "", DrawArea.DrawToolType.Application);
                        Parent.drawArea.tools[(int)Parent.oCureo.ObjTool].LoadSimpleObjectSansGraph(Parent.oCureo);

                        if (Parent.oCureo.oDraw != null)
                        {
                            // Insert la description
                            DrawApplication da = (DrawApplication)parent.oCureo.oDraw;



                            da.CWInsertProp(cw, "AppDescription", "P");

                            cw.InsertTextFromId("AppDescription", false, "Properties\n", "Titre 6");
                            cw.InsertTextFromId("AppDescription", false, "\n", null);
                            cw.CreatIdFromIdP("AppProperties", "AppDescription");
                            cw.InsertTextFromId("AppProperties", true, "\n", null);
                            cw.InsertTabFromId("AppProperties", false, da, null, false, null);

                        }
                        parent.oCureo = null;
                    }

                    //Insert Bookmark et titres pour les vues de déploiement
                    if (cw.Exist("TitreServerInfrastructure") == -1)
                    {
                        cw.InsertTextFromId("ServerInfrastructure", false, "\n", null);

                        cw.CreatIdFromIdP("TitreServerInfrastructure", "ServerInfrastructure");
                        cw.InsertTextFromId("TitreServerInfrastructure", true, "SERVER INFRASTRUCTURE\n", "Titre 1");

                        //Entete Table des serveurs chapitre Déploiement
                        sType = "ServerList";
                        cw.InsertTextFromId("TitreServerInfrastructure", false, "Server List\n", "Titre 2");
                        cw.InsertTextFromId("TitreServerInfrastructure", false, "\n", null);
                        cw.CreatIdFromIdP(sType, "TitreServerInfrastructure");
                        int n = Parent.oCnxBase.ConfDB.FindTable(sType);
                        if (n > -1)
                        {
                            Table t = (Table)Parent.oCnxBase.ConfDB.LstTable[n];
                            cw.InsertHeadTabFromId(sType, true, t, null);
                            if (sGuidAppVersion != "") CreatServerListTab(sType);

                        }

                    }
                    
                    for (int i = 0; i < lstGuidVue.Count; i++)
                    {
                        string[] fields = ((string)lstGuidVue[i]).Split(',');
                        string sEnv = "";

                        switch (fields[3][0])
                        {
                            case '3':
                                sEnv = "Production";
                                break;
                            case '4':
                                sEnv = "Hors-Production";
                                break;
                            case '5':
                                sEnv = "Pre-production";
                                break;
                            case 'F':
                                sEnv = "Service Infra";
                                break;
                        }

                        switch (fields[3][0])
                        {
                            case '3':
                            case '4':
                            case '5':
                            case 'F':
                                string sBook = fields[0].Replace("-", "");
                                string sBookView = "View" + sBook;
                                if (cw.Exist("n" + sBookView) > -1)
                                {
                                    cw.InsertTextFromId("n" + sBookView, true, sEnv + " Environment: " + fields[2], "Titre 2");
                                }
                                else
                                {
                                    cw.InsertTextFromId("TitreServerInfrastructure", false, "\n", null);
                                    cw.CreatIdFromIdP(sBookView, "TitreServerInfrastructure");
                                    cw.InsertTextFromId(sBookView, true, sEnv + " Environment: " + fields[2] + "\n", "Titre 2");
                                    cw.CreatIdFromIdP("n" + sBookView, sBookView);
                                    cw.InsertTextFromId(sBookView, false, "Infrastructure Diagram\n", "Titre 3");
                                    cw.InsertTextFromId(sBookView, false, "\n", null);
                                    cw.CreatIdFromIdP("DiagInf" + sBook, sBookView);
                                    cw.InsertTextFromId(sBookView, false, sEnv + " External Application\n", "Titre 3");
                                    cw.InsertTextFromId(sBookView, false, "\n", null);
                                    cw.CreatIdFromIdP("AppExt" + sBook, sBookView);
                                    cw.InsertTextFromId(sBookView, false, sEnv + " Users\n", "Titre 3");
                                    cw.InsertTextFromId(sBookView, false, "\n", null);
                                    cw.CreatIdFromIdP("AppUser" + sBook, sBookView);
                                    cw.InsertTextFromId(sBookView, false, sEnv + " Clusters\n", "Titre 3");
                                    cw.InsertTextFromId(sBookView, false, "\n", null);
                                    cw.CreatIdFromIdP("Cluster" + sBook, sBookView);
                                    cw.InsertTextFromId(sBookView, false, sEnv + " Servers\n", "Titre 3");
                                    cw.InsertTextFromId(sBookView, false, "\n", null);
                                    cw.CreatIdFromIdP("Server" + sBook, sBookView);
                                    cw.InsertTextFromId(sBookView, false, sEnv + " Kubernetes Services\n", "Titre 3");
                                    cw.InsertTextFromId(sBookView, false, "\n", null);
                                    cw.CreatIdFromIdP("Insks" + sBook, sBookView);
                                    cw.InsertTextFromId(sBookView, false, sEnv + " Cloud Mananged Services\n", "Titre 3");
                                    cw.InsertTextFromId(sBookView, false, "\n", null);
                                    cw.CreatIdFromIdP("Inssas" + sBook, sBookView);
                                    cw.InsertTextFromId(sBookView, false, sEnv + " Vlan\n", "Titre 3");
                                    cw.InsertTextFromId(sBookView, false, "\n", null);
                                    cw.CreatIdFromIdP("VLan" + sBook, sBookView);
                                    cw.InsertTextFromId(sBookView, false, sEnv + " Sequences\n", "Titre 3");
                                    cw.InsertTextFromId(sBookView, false, "\n", null);
                                    cw.CreatIdFromIdP("Seq" + sBook, sBookView);
                                    cw.InsertTextFromId(sBookView, false, sEnv + " Flows\n", "Titre 3");
                                    cw.InsertTextFromId(sBookView, false, "\n", null);
                                    cw.CreatIdFromIdP("Flow" + sBook, sBookView);
                                }
                                break;
                        }
                    }
                    
                    //Entree des objects de chaque vue

                    
                    for (int i = 0; i < lstGuidVue.Count; i++)
                    {
                        Parent.ClearVue(true);
                        // (0)GuidVue, (1)GuidGVue, (2)NomVue, (3)NomTypeVue, (4)GuidAppVersion, (5)GuidTypeVue, (6)GuidEnvironnement, (7) GuidVueMoins1, (8) GuidGVueMoins1

                
                        string[] fields = ((string)lstGuidVue[i]).Split(',');

                        string sGuidVueMoins1 = "", sGuidGVueMoins1 = "";

                        //if (fields[5] == "86d1385e-b293-433e-bb6c-b2f8fa25b116")

                        if(fields[3][0] == '6')
                            Parent.GuidVue = new Guid(fields[0]);

                        Parent.GuidVue = new Guid(fields[0]);
                        Parent.GuidGVue = new Guid(fields[1]);
                        string sVueBookmark = "n" + fields[2];
                        Parent.cbGuidVue.Items.Add(Parent.GuidVue); Parent.cbGuidVue.SelectedIndex = 0; // GuidVue
                        Parent.cbVue.Items.Add(fields[2]); Parent.cbVue.SelectedIndex = 0; //NomVue

                        string sDiagBookmark = "Diag" + fields[5].Replace("-", ""); //GuidTypeVue
                        Parent.tbTypeVue.Text = fields[3];                          // NomTypeVue
                        Parent.sTypeVue = fields[3];
                        sGuidVueMoins1 = fields[7];
                        sGuidGVueMoins1 = fields[8];

                        Parent.LoadVue();

                        DrawObject o = null;


                        for (int j = 0; j < Parent.drawArea.GraphicsList.Count; j++)
                        {
                            o = Parent.drawArea.GraphicsList[j];
                            //if (o.GetType() == typeof(DrawServer))
                            if (Parent.tbTypeVue.Text[0] != '1' || (o.GetType() != typeof(DrawComposant) && o.GetType() != typeof(DrawInterface) && o.GetType() != typeof(DrawBase) && o.GetType() != typeof(DrawFile)))
                            {
                                // recette : 514c254c-68d3-4980-9ef6-9e811c5c770a
                                // integration : 57bdd6fc-0821-412c-a57d-cb8e7c405b61
                                // dev : 63044337-24f4-4562-8c2b-1a7d9374f1fd
                                // qualif : 8ca15033-07a6-4ea7-8392-907fd3cb7e16
                                // prod : 96699e1c-d501-48c1-8585-3fce75a4c2e7
                                o.CWInsert(cw, (Parent.tbTypeVue.Text[0]));
                            }

                        }

                        
                        //inserer le diagram de la vue
                        string sDiagram = parent.SaveDiagramFromPath(fields[2], cw.getImagePath(), "");
                        if (cw.Exist(sVueBookmark) == -1)
                        {
                            if (cw.Exist(sDiagBookmark) > -1)
                            {
                                cw.InsertTextFromId(sDiagBookmark, false, "\n", null);
                                cw.CreatIdFromIdP(sVueBookmark, sDiagBookmark);
                                cw.InsertTextFromId(sVueBookmark, true, "Vue : " + fields[2] + "\n", "Titre 3");
                                cw.InsertTextFromId(sVueBookmark, false, "\n", null);

                                //Tableau Vue
                                Parent.oCureo = new ExpObj(Parent.GuidVue, "", DrawArea.DrawToolType.Vue);
                                Parent.drawArea.tools[(int)Parent.oCureo.ObjTool].LoadSimpleObjectSansGraph(Parent.oCureo);

                                if (Parent.oCureo.oDraw != null)
                                {
                                    // Insert la description
                                    DrawVue dv = (DrawVue)parent.oCureo.oDraw;
                                    cw.InsertTextFromId(sVueBookmark, false, "Properties\n", "Titre 6");
                                    cw.InsertTabFromId(sVueBookmark, false, dv, null, false, null);
                                    cw.InsertTextFromId(sVueBookmark, false, "\n", null);
                                }
                                parent.oCureo = null;
                                
                                cw.InsertTextFromId(sVueBookmark, false, "Diagram \n", "Titre 4");
                                cw.InsertImgFromId(sVueBookmark, false, sDiagram, null);
                                cw.InsertTextFromId(sVueBookmark, false, "\n", null);

                            }
                        }
                        else
                        {
                            cw.InsertTextFromId(sVueBookmark, true, "\n", null);
                            cw.InsertImgFromId(sVueBookmark, false, sDiagram, null);
                            cw.InsertTextFromId(sVueBookmark, false, "\n", null);
                        }
                        

                        int n = -1;
                        switch (Parent.tbTypeVue.Text[0])
                        {
                            case '0': //0-Fonctionnelle   Diagf3fa584ded554e14831e0d0d3acded8e

                                break;
                            case '1': // 1-Applicative   Diag49c88d3df32f44fead6c35977c5b812e

                                break;
                            case '2': // 2-Infrastructure Diagd5b533a906ac4f8ca5abe345b0212542
                                break;
                            case '6': // 6-Sites   Diag86d1385eb293433ebb6cb2f8fa25b116
                                n = Parent.oCnxBase.ConfDB.FindTable("TabEACB");
                                if (n > -1)
                                {
                                    Table t = (Table)Parent.oCnxBase.ConfDB.LstTable[n];

                                    //CWTableau tEACB = Parent.oCnxBase.CreatEACBTab(sGuidVueInf, fields[0]);
                                    string bmrk = "Flow3";
                                    string inbmrk = "flo" + fields[0].Replace("-", "");
                                    if (cw.Exist(inbmrk) > -1)
                                    {
                                        cw.InsertHeadTabFromId(inbmrk, true, t, null);
                                        //CreatEACBTab(inbmrk, sGuidVueInf, fields[0]);
                                    }
                                    else if (cw.Exist(bmrk) > -1)
                                    {
                                        cw.InsertTextFromId(bmrk, false, "\n", null);
                                        cw.CreatIdFromIdP(inbmrk, bmrk);
                                        cw.InsertTextFromId(inbmrk, false, "\n", null);
                                        cw.InsertHeadTabFromId(inbmrk, false, t, null);
                                        CreatEACBTabInterSite(inbmrk, fields[0]);
                                    }
                                }

                                sDiagBookmark = "Diag" + Parent.tbTypeVue.Text[0] + fields[5].Replace("-", "");
                                sVueBookmark = "n" + Parent.tbTypeVue.Text[0] + fields[0].Replace("-", "");


                                if (cw.Exist(sVueBookmark) > -1)
                                {
                                    cw.InsertTextFromId(sVueBookmark, true, "\n", null);
                                    cw.InsertImgFromId(sVueBookmark, false, sDiagram, null);
                                    cw.InsertTextFromId(sVueBookmark, false, "\n", null);
                                }
                                else if (cw.Exist(sDiagBookmark) > -1)
                                {
                                    cw.InsertTextFromId(sDiagBookmark, false, "\n", null);
                                    cw.CreatIdFromIdP(sVueBookmark, sDiagBookmark);
                                    cw.InsertTextFromId(sVueBookmark, true, "\n", null);
                                    cw.InsertImgFromId(sVueBookmark, false, sDiagram, null);
                                    cw.InsertTextFromId(sVueBookmark, false, "\n", null);
                                }

                                break;
                            case '3': // 3-Production   Diag2a4c3691e7144d0594008fbbb06f2d62

                                GetVueInfFromVueDeploy();
                                if (sGuidGVueMoins1.Length > 0)
                                {
                                    n = Parent.oCnxBase.ConfDB.FindTable("TabEACB");
                                    if (n > -1)
                                    {
                                        Table t = (Table)Parent.oCnxBase.ConfDB.LstTable[n];

                                        //CWTableau tEACB = Parent.oCnxBase.CreatEACBTab(sGuidVueInf, fields[0]);
                                        string bmrk = "Flow" + fields[0].Replace("-", "");
                                        string inbmrk = "flo" + fields[0].Replace("-", "");
                                        if (cw.Exist(inbmrk) > -1)
                                        {
                                            cw.InsertHeadTabFromId(inbmrk, true, t, null);
                                            CreatEACBTab(inbmrk, sGuidVueMoins1, fields[0]);
                                        }
                                        else if (cw.Exist(bmrk) > -1)
                                        {
                                            cw.InsertTextFromId(bmrk, false, "\n", null);
                                            cw.CreatIdFromIdP(inbmrk, bmrk);
                                            cw.InsertTextFromId(inbmrk, false, "\n", null);
                                            cw.InsertHeadTabFromId(inbmrk, false, t, null);
                                            CreatEACBTab(inbmrk, sGuidVueMoins1, fields[0]);
                                        }
                                    }

                                    // Insersion des diagrammes sequence de flux
                                    Parent.drawArea.GraphicsList.Clear();
                                    ArrayList lstImg = Parent.MakeSequenceFluxFonc(Parent.GuidVue.ToString(), sGuidVueMoins1, cw);
                                    sDiagBookmark = "Seq" + fields[0].Replace("-", "");
                                    if (cw.Exist(sDiagBookmark) > -1)
                                    {
                                        for (int idiag = 0; idiag < lstImg.Count; idiag++)
                                        {
                                            cw.InsertTextFromId(sDiagBookmark, false, "\n", null);
                                            cw.InsertImgFromId(sDiagBookmark, false, (string)lstImg[idiag], null);
                                        }
                                    }

                                    //insersion diagramme inf/deploy
                                    Parent.drawArea.GraphicsList.Clear();
                                    Parent.GuidVue = new Guid(sGuidVueMoins1);
                                    Parent.GuidGVue = new Guid(sGuidGVueMoins1);
                                    Parent.MakeVueInf(fields[0]);
                                    sDiagram = parent.SaveDiagramFromPath("Inf" + fields[2], cw.getImagePath(), "");
                                    sDiagBookmark = "DiagInf" + fields[0].Replace("-", "");
                                    sVueBookmark = "nInf" + fields[0].Replace("-", "");

                                    if (cw.Exist(sVueBookmark) > -1)
                                    {
                                        cw.InsertTextFromId(sVueBookmark, true, "\n", null);
                                        cw.InsertImgFromId(sVueBookmark, false, sDiagram, null);
                                        cw.InsertTextFromId(sVueBookmark, false, "\n", null);
                                    }
                                    else if (cw.Exist(sDiagBookmark) > -1)
                                    {
                                        cw.InsertTextFromId(sDiagBookmark, false, "\n", null);
                                        cw.CreatIdFromIdP(sVueBookmark, sDiagBookmark);
                                        cw.InsertTextFromId(sVueBookmark, true, "\n", null);
                                        cw.InsertImgFromId(sVueBookmark, false, sDiagram, null);
                                        cw.InsertTextFromId(sVueBookmark, false, "\n", null);
                                        parent.oCureo = new ExpObj(new Guid(fields[0]), fields[2], DrawArea.DrawToolType.Vue);
                                        Parent.drawArea.tools[(int)parent.oCureo.ObjTool].LoadSimpleObjectSansGraph(parent.oCureo);
                                        if (parent.oCureo.oDraw != null)
                                        {
                                            DrawVue dv = (DrawVue)parent.oCureo.oDraw;
                                            dv.CWInsertProp(cw, sVueBookmark, "P");
                                        }
                                        parent.oCureo = null;
                                    }



                                }
                                break;
                            case '5': // 5-Pre-Production   Diag7afca9459d4148fbb6345b6ffda90d4e
                                
                                if (sGuidGVueMoins1.Length > 0)
                                {
                                    n = Parent.oCnxBase.ConfDB.FindTable("TabEACB");
                                    if (n > -1)
                                    {
                                        Table t = (Table)Parent.oCnxBase.ConfDB.LstTable[n];

                                        string bmrk = "Flow" + fields[0].Replace("-", "");
                                        string inbmrk = "flo" + fields[0].Replace("-", "");
                                        if (cw.Exist(inbmrk) > -1)
                                        {
                                            cw.InsertHeadTabFromId(inbmrk, true, t, null);
                                            CreatEACBTab(inbmrk, sGuidVueMoins1, fields[0]);
                                        }
                                        else if (cw.Exist(bmrk) > -1)
                                        {
                                            cw.InsertTextFromId(bmrk, false, "\n", null);
                                            cw.CreatIdFromIdP(inbmrk, bmrk);
                                            cw.InsertTextFromId(inbmrk, false, "\n", null);
                                            cw.InsertHeadTabFromId(inbmrk, false, t, null);
                                            CreatEACBTab(inbmrk, sGuidVueMoins1, fields[0]);
                                        }
                                    }

                                    // Insersion des diagrammes sequence de flux
                                    Parent.drawArea.GraphicsList.Clear();
                                    ArrayList lstImg = Parent.MakeSequenceFluxFonc(Parent.GuidVue.ToString(), sGuidVueMoins1, cw);
                                    sDiagBookmark = "Seq" + fields[0].Replace("-", "");
                                    if (cw.Exist(sDiagBookmark) > -1)
                                    {
                                        for (int idiag = 0; idiag < lstImg.Count; idiag++)
                                        {
                                            cw.InsertTextFromId(sDiagBookmark, false, "\n", null);
                                            cw.InsertImgFromId(sDiagBookmark, false, (string)lstImg[idiag], null);
                                        }
                                    }

                                    //insersion diagramme inf/deploy
                                    Parent.drawArea.GraphicsList.Clear();
                                    Parent.GuidVue = new Guid(sGuidVueMoins1);
                                    Parent.GuidGVue = new Guid(sGuidGVueMoins1);
                                    Parent.MakeVueInf(fields[0]);
                                    sDiagram = parent.SaveDiagramFromPath("Inf" + fields[2], cw.getImagePath(), "");
                                    sDiagBookmark = "DiagInf" + fields[0].Replace("-", "");
                                    sVueBookmark = "nInf" + fields[0].Replace("-", "");

                                    if (cw.Exist(sVueBookmark) > -1)
                                    {
                                        cw.InsertTextFromId(sVueBookmark, true, "\n", null);
                                        cw.InsertImgFromId(sVueBookmark, false, sDiagram, null);
                                        cw.InsertTextFromId(sVueBookmark, false, "\n", null);
                                    }
                                    else if (cw.Exist(sDiagBookmark) > -1)
                                    {
                                        cw.InsertTextFromId(sDiagBookmark, false, "\n", null);
                                        cw.CreatIdFromIdP(sVueBookmark, sDiagBookmark);
                                        cw.InsertTextFromId(sVueBookmark, true, "\n", null);
                                        cw.InsertImgFromId(sVueBookmark, false, sDiagram, null);
                                        cw.InsertTextFromId(sVueBookmark, false, "\n", null);
                                        parent.oCureo = new ExpObj(new Guid(fields[0]), fields[2], DrawArea.DrawToolType.Vue);
                                        Parent.drawArea.tools[(int)parent.oCureo.ObjTool].LoadSimpleObjectSansGraph(parent.oCureo);
                                        if (parent.oCureo.oDraw != null)
                                        {
                                            DrawVue dv = (DrawVue)parent.oCureo.oDraw;
                                            dv.CWInsertProp(cw, sVueBookmark, "P");
                                        }
                                        parent.oCureo = null;
                                    }


                                }

                                break;
                            case '4': // 4-Hors Production   Diagef667e58a61749fd91a82beeda856475
                                if (sGuidGVueMoins1.Length > 0)
                                {
                                    n = Parent.oCnxBase.ConfDB.FindTable("TabEACB");
                                    if (n > -1)
                                    {
                                        Table t = (Table)Parent.oCnxBase.ConfDB.LstTable[n];


                                        string bmrk = "Flow" + fields[0].Replace("-", "");
                                        string inbmrk = "flo" + fields[0].Replace("-", "");
                                        if (cw.Exist(inbmrk) > -1)
                                        {
                                            cw.InsertHeadTabFromId(inbmrk, true, t, null);
                                            CreatEACBTab(inbmrk, sGuidVueMoins1, fields[0]);
                                        }
                                        else if (cw.Exist(bmrk) > -1)
                                        {
                                            cw.InsertTextFromId(bmrk, false, "\n", null);
                                            cw.CreatIdFromIdP(inbmrk, bmrk);
                                            cw.InsertTextFromId(inbmrk, false, "\n", null);
                                            cw.InsertHeadTabFromId(inbmrk, false, t, null);
                                            CreatEACBTab(inbmrk, sGuidVueMoins1, fields[0]);
                                        }
                                    }

                                    // Insersion des diagrammes sequence de flux
                                    Parent.drawArea.GraphicsList.Clear();
                                    ArrayList lstImg = Parent.MakeSequenceFluxFonc(Parent.GuidVue.ToString(), sGuidVueMoins1, cw);
                                    sDiagBookmark = "Seq" + fields[0].Replace("-", "");
                                    if (cw.Exist(sDiagBookmark) > -1)
                                    {
                                        for (int idiag = 0; idiag < lstImg.Count; idiag++)
                                        {
                                            cw.InsertTextFromId(sDiagBookmark, false, "\n", null);
                                            cw.InsertImgFromId(sDiagBookmark, false, (string)lstImg[idiag], null);
                                        }
                                    }

                                    //insersion diagramme inf/deploy
                                    Parent.drawArea.GraphicsList.Clear();
                                    Parent.GuidVue = new Guid(sGuidVueMoins1);
                                    Parent.GuidGVue = new Guid(sGuidGVueMoins1);
                                    Parent.MakeVueInf(fields[0]);
                                    sDiagram = parent.SaveDiagramFromPath("Inf" + fields[2], cw.getImagePath(), "");
                                    sDiagBookmark = "DiagInf" + fields[0].Replace("-", "");
                                    sVueBookmark = "nInf" + fields[0].Replace("-", "");

                                    if (cw.Exist(sVueBookmark) > -1)
                                    {
                                        cw.InsertTextFromId(sVueBookmark, true, "\n", null);
                                        cw.InsertImgFromId(sVueBookmark, false, sDiagram, null);
                                        cw.InsertTextFromId(sVueBookmark, false, "\n", null);
                                    }
                                    else if (cw.Exist(sDiagBookmark) > -1)
                                    {
                                        cw.InsertTextFromId(sDiagBookmark, false, "\n", null);
                                        cw.CreatIdFromIdP(sVueBookmark, sDiagBookmark);
                                        cw.InsertTextFromId(sVueBookmark, true, "\n", null);
                                        cw.InsertImgFromId(sVueBookmark, false, sDiagram, null);
                                        cw.InsertTextFromId(sVueBookmark, false, "\n", null);
                                        parent.oCureo = new ExpObj(new Guid(fields[0]), fields[2], DrawArea.DrawToolType.Vue);
                                        Parent.drawArea.tools[(int)parent.oCureo.ObjTool].LoadSimpleObjectSansGraph(parent.oCureo);
                                        if (parent.oCureo.oDraw != null)
                                        {
                                            DrawVue dv = (DrawVue)parent.oCureo.oDraw;
                                            dv.CWInsertProp(cw, sVueBookmark, "P");
                                        }
                                        parent.oCureo = null;
                                    }
                                }
                                break;
                            case 'F': // F-Service Infra   Diag011f72c9c9d04800af3aafd174a49c93
                                
                                if (sGuidVueInfra.Length > 0)
                                {
                                    n = Parent.oCnxBase.ConfDB.FindTable("TabEACB");
                                    if (n > -1)
                                    {
                                        Table t = (Table)Parent.oCnxBase.ConfDB.LstTable[n];


                                        string bmrk = "Flow" + fields[0].Replace("-", "");
                                        string inbmrk = "flo" + fields[0].Replace("-", "");
                                        if (cw.Exist(inbmrk) > -1)
                                        {
                                            cw.InsertHeadTabFromId(inbmrk, true, t, null);
                                            CreatEACBTab(inbmrk, sGuidVueInfra, fields[0]);
                                        }
                                        else if (cw.Exist(bmrk) > -1)
                                        {
                                            cw.InsertTextFromId(bmrk, false, "\n", null);
                                            cw.CreatIdFromIdP(inbmrk, bmrk);
                                            cw.InsertTextFromId(inbmrk, false, "\n", null);
                                            cw.InsertHeadTabFromId(inbmrk, false, t, null);
                                            CreatEACBTab(inbmrk, sGuidVueInfra, fields[0]);
                                        }
                                    }

                                    Parent.drawArea.GraphicsList.Clear();
                                    Parent.GuidVue = new Guid(sGuidVueInfra);
                                    Parent.GuidGVue = new Guid(sGuidGVueInfra);
                                    Parent.MakeVueInf(fields[0]);
                                    sDiagram = parent.SaveDiagramFromPath("Inf" + fields[1], cw.getImagePath(), "");
                                    sDiagBookmark = "DiagInf" + fields[0].Replace("-", "");
                                    sVueBookmark = "nInf" + fields[0].Replace("-", "");

                                    if (cw.Exist(sVueBookmark) > -1)
                                    {
                                        cw.InsertTextFromId(sVueBookmark, true, "\n", null);
                                        cw.InsertImgFromId(sVueBookmark, false, sDiagram, null);
                                        cw.InsertTextFromId(sVueBookmark, false, "\n", null);
                                    }
                                    else if (cw.Exist(sDiagBookmark) > -1)
                                    {
                                        cw.InsertTextFromId(sDiagBookmark, false, "\n", null);
                                        cw.CreatIdFromIdP(sVueBookmark, sDiagBookmark);
                                        cw.InsertTextFromId(sVueBookmark, true, "\n", null);
                                        cw.InsertImgFromId(sVueBookmark, false, sDiagram, null);
                                        cw.InsertTextFromId(sVueBookmark, false, "\n", null);
                                        parent.oCureo = new ExpObj(new Guid(fields[0]), fields[1], DrawArea.DrawToolType.Vue);
                                        Parent.drawArea.tools[(int)parent.oCureo.ObjTool].LoadSimpleObjectSansGraph(parent.oCureo);
                                        if (parent.oCureo.oDraw != null)
                                        {
                                            DrawVue dv = (DrawVue)parent.oCureo.oDraw;
                                            dv.CWInsertProp(cw, sVueBookmark, "P");
                                        }
                                        parent.oCureo = null;
                                    }
                                }
                                break;
                            case '8': // 8-ZoningProd   Diagad05bb2da9ce4a4f9beb61d8d09c52e6
                            case '7': // 7-ZoningHorsProd   Diagc1a91afa2a6e40ac985d75dfa424c3da
                                break;
                            case 'A': // A-SanProd   Diagafa4d849f7d64692bc0e8b2b79c246fa
                            case '9': // 9-SanHorsProd   Diag9066daac8b184a269908d78fb078c84a


                                break;
                            case 'C': // C-CTIProd   Diagd5a9652be99b48f4a892d4dffc35935d
                            case 'B': // B-CTIHorsProd   Diagd5b677fc031041e0b4bcc5a777b04823
                                break;
                        }


                        Parent.drawArea.GraphicsList.Clear();
                    }

                    //creer la log
                    
                    string sguidLog = Parent.oCnxBase.DBLog(Parent.wkApp.GuidAppVersion.ToString(), Parent.wkApp.Application, "a1c46dc9-9c21-4f60-89ab-c3976fcecc75", tbVersion.Text);
                    if (rtCommentaire.Text != "")
                    {
                        byte[] rawData = System.Text.Encoding.UTF8.GetBytes(rtCommentaire.Rtf);
                        int iSize = rawData.Length;
                        Parent.oCnxBase.CBWriteWithObj("INSERT INTO Comment (GuidObject, NomProp, Size, RichText, Policy) VALUES ('" + sguidLog + "','Description'," + iSize + ", ?, 'P')", rawData);
                    }

                    //Entete liste des versions du document publiées
                    sType = "TabVersion";
                    if (cw.Exist(sType) > -1)
                    {
                        int n = Parent.oCnxBase.ConfDB.FindTable(sType);
                        if (n > -1)
                        {
                            Table t = (Table)Parent.oCnxBase.ConfDB.LstTable[n];
                            cw.InsertHeadTabFromId(sType, true, t, null);
                            if (sGuidAppVersion != "") CreatMajVersionTab(cw, sType, sGuidAppVersion);
                        }
                    }
                    
                }
                
                cw.SaveDoc();
                parent.wkApp = null;
            }
            //if (!oCnxBase.CBRecherche("SELECT NomTypeVue FROM TypeVue, Vue WHERE GuidVue ='" + GuidVue + "' and TypeVue.GuidTypeVue=Vue.GuidTypeVue"))
        }

        public int GetStatutfluxRules(string sGuidSource, string sIPSource, string sGuidVlanSource, string sGuidVlanClassSource, string sGuidCible, string sIPCible, string sGuidVlanCible, string sGuidVlanClassCible, string sGuidGroupService)
        {
            int statut = 0;
            
            string[] aGuidSource = sGuidSource.Replace("<br>","\n").Split('\n');
            string[] aIPSource = sIPSource.Replace("<br>", "\n").Split('\n');
            string[] aGuidVlanSource = sGuidVlanSource.Replace("<br>", "\n").Split('\n');
            string[] aGuidVlanClassSource = sGuidVlanClassSource.Replace("<br>", "\n").Split('\n');
            string[] aGuidCible = sGuidCible.Replace("<br>", "\n").Split('\n');
            string[] aIPCible = sIPCible.Replace("<br>", "\n").Split('\n');
            string[] aGuidVlanCible = sGuidVlanCible.Replace("<br>", "\n").Split('\n');
            string[] aGuidVlanClassCible = sGuidVlanClassCible.Replace("<br>", "\n").Split('\n');

            //for (int i = 0; i < aGuidSource.Length; i++) if(((string) aIPSource[i]).Length == 0 ) 

            List<string[]> lstGuidSource = new List<string[]>();
            List<string[]> lstGuidCible = new List<string[]>();
            List<string> lstGuidVlan = new List<string>();

            // même vlan
            foreach (var el in aGuidVlanSource) if (lstGuidVlan.Find(e => e == el) == null) lstGuidVlan.Add(el);
            foreach (var el in aGuidVlanCible) if (lstGuidVlan.Find(e => e == el) == null) lstGuidVlan.Add(el);
            if (lstGuidVlan.Count == 1 && lstGuidVlan[0] != "") return 1;

            // relation groupe BNP
            foreach (var el in aGuidVlanClassSource) if (el == "a1d4a308-9493-40d5-9840-0d40c4d6901a") return 4;
            foreach (var el in aGuidVlanClassCible) if (el == "a1d4a308-9493-40d5-9840-0d40c4d6901a") return 4;

            // analyse des règles
            for (int i = 0; i < aGuidSource.Length; i++) if (((string)aIPSource[i]).Length != 0 && !Parent.isNum(aIPSource[i][0]))
                {
                    string[] aEnreg = new string[3];
                    aEnreg[0] = aGuidSource[i];
                    switch(aIPSource[i][0])
                    {
                        case 'G':
                            aEnreg[1] = "NCard";
                            break;
                        case 'R':
                            aEnreg[1] = "Role";
                            break;
                        default:
                            aEnreg[1] = "";
                            break;
                    }
                    aEnreg[2] = aGuidVlanSource[i];
                    lstGuidSource.Add(aEnreg);
                }
            for (int i = 0; i < aGuidCible.Length; i++) if (((string)aIPCible[i]).Length != 0 && !Parent.isNum(aIPCible[i][0]))
                {
                    string[] aEnreg = new string[4];
                    aEnreg[0] = aGuidCible[i];
                    switch (aIPCible[i][0])
                    {
                        case 'G':
                            aEnreg[1] = "NCard";
                            break;
                        case 'R':
                            aEnreg[1] = "Role";
                            break;
                        default:
                            aEnreg[1] = "";
                            break;
                    }
                    aEnreg[2] = aGuidVlanCible[i];
                    lstGuidCible.Add(aEnreg);
                }

            foreach (var el in aGuidVlanClassSource) if (lstGuidSource.Find(e => e[0] == el) == null)
                {
                    string[] aEnreg = new string[4];
                    aEnreg[0] = el;
                    aEnreg[1] = "VlanClass";
                    aEnreg[2] = "";
                    aEnreg[3] = "";
                    lstGuidSource.Add(aEnreg);
                }
            foreach (var el in aGuidVlanClassCible) if (lstGuidCible.Find(e => e[0] == el) == null)
                {
                    string[] aEnreg = new string[4];
                    aEnreg[0] = el;
                    aEnreg[1] = "VlanClass";
                    aEnreg[2] = "";
                    aEnreg[3] = "";
                    lstGuidCible.Add(aEnreg);
                }
            
            foreach(var elSource in lstGuidSource)
            {
                foreach (var elCible in lstGuidCible)
                {

                    if (Parent.oCnxBase.CBRecherche("Select GuidSource From MatriceFlux Where GuidSource='" + elSource[0] + "' and GuidCible = '" + elCible[0] + "' and GuidGroupService = '" + sGuidGroupService + "'"))
                    {
                        Parent.oCnxBase.CBReaderClose();
                        if (elSource[1] == "VlanClass" && elCible[1] == "VlanClass") statut = 2; else statut = 3;
                        break;
                    }
                    Parent.oCnxBase.CBReaderClose();
                }
                if (statut != 0) break;
            }
            return statut;
        }

        public void AnalyseFluxNode(string brmk, XmlExcel xmlFlux, XmlElement Node, DrawTab oTab)
        {
            HtmlNode oNode = null;
            string[] aServices, aProtocols, aPorts;
            string sSeparator = "<br>";
            int nbrLigne = Parent.XmlAnalyseFluxNode(sSeparator, xmlFlux, Node, oTab);

            if (nbrLigne != 0) {

                if (cw.Exist(brmk) > -1)
                {
                    aServices = ((string)oTab.GetValueFromName("Service")).Replace(sSeparator, "!").Split('!');
                    aProtocols = ((string)oTab.GetValueFromName("Protocol")).Replace(sSeparator, "!").Split('!');
                    aPorts = ((string)oTab.GetValueFromName("Ports")).Replace(sSeparator, "!").Split('!');
                    for (int i = 0; i < nbrLigne; i++)
                    {
                        oTab.SetValueFromName("Service", aServices[i]);
                        oTab.SetValueFromName("Protocol", aProtocols[i]);
                        oTab.SetValueFromName("Ports", aPorts[i]);

                        oNode = (HtmlNode)cw.InsertRowFromId(brmk, oTab);

                        if (oNode != null)
                        {
                            switch (GetStatutfluxRules((string)oTab.GetValueFromName("GuidNCardSrc"), (string)oTab.GetValueFromName("IPSrc"), (string)oTab.GetValueFromName("GuidVlanSrc"), (string)oTab.GetValueFromName("GuidVlanClassSrc"),
                                                       (string)oTab.GetValueFromName("GuidNCardCbl"), (string)oTab.GetValueFromName("IPCbl"), (string)oTab.GetValueFromName("GuidVlanCbl"), (string)oTab.GetValueFromName("GuidVlanClassCbl"),
                                                       (string)oTab.GetValueFromName("GuidGroupService")))
                            {
                                case 0: //Flux inexistant dans les règles flux
                                    break;
                                case 1: // Flux dans le même vlan
                                    oNode.Attributes.Add("class", "fluxinternal");
                                    break;
                                case 2: // Flux vlanClass -> vlanClass
                                    oNode.Attributes.Add("class", "fluxvlan");
                                    break;
                                case 3: // Flux Groupe dynamique(IP)
                                    oNode.Attributes.Add("class", "fluxgroupe");
                                    break;
                                case 4: // Flux Groupe BNP
                                        //oNode.SetAttributeValue("style", "background-color:gray");
                                    oNode.Attributes.Add("class", "fluxbnpp");
                                    //oNode.InnerHtml += " class=\"fluxbnnp\"";
                                    break;
                            }
                        }

                    }
                    
                }
            }

        }
        public void CreatEACBTabInterSite(string brmk, string sGuidVue)
        {
            CnxBase cn = Parent.oCnxBase;
            DrawTab oTab;

            XmlExcel xmlFlux = new XmlExcel(Parent, "FluxSites");
            XmlElement elServices;

            if (cn.CBRecherche("SELECT DISTINCT InterLink.GuidInterLink, InterLink.Id, NomInterLink, GuidServerSiteIn, GuidServerSiteOut, InterLink.GuidGroupService, NomGroupService, Service.GuidService, NomService, Protocole, Ports From Vue, DansVue, InterLink, GInterLink, GroupService, ServiceLink, Service WHERE	Vue.GuidVue ='" + sGuidVue + "' and Vue.GuidGvue=DansVue.GuidGVue and GuidObjet=GuidGInterLink and GInterLink.GuidInterLink=InterLink.GuidInterLink AND InterLink.GuidGroupService=GroupService.GuidGroupService  AND GroupService.GuidGroupService=ServiceLink.GuidGroupService AND ServiceLink.GuidService=Service.GuidService ORDER BY Id"))
            {
                string sTechLinkLast = null;

                while (cn.Reader.Read())
                {
                    if (cn.Reader.GetString(0) != sTechLinkLast)
                    {
                        sTechLinkLast = cn.Reader.GetString(0);

                        XmlElement elFlux = xmlFlux.XmlCreatEl(xmlFlux.root, "Flux");
                        elFlux.SetAttribute("Guid", cn.Reader.GetString(0));
                        if (cn.Reader.IsDBNull(1)) elFlux.SetAttribute("Id", ""); else elFlux.SetAttribute("Id", cn.Reader.GetString(1));
                        elFlux.SetAttribute("Nom", cn.Reader.GetString(2));
                        elFlux.SetAttribute("Selected", "No");

                        XmlElement elO = xmlFlux.XmlCreatEl(elFlux, "Origine");
                        elO.SetAttribute("Guid", cn.Reader.GetString(3));

                        XmlElement elC = xmlFlux.XmlCreatEl(elFlux, "Cible");
                        elC.SetAttribute("Guid", cn.Reader.GetString(4));

                        XmlElement elGS = xmlFlux.XmlCreatEl(elFlux, "GroupService");
                        elGS.SetAttribute("Guid", cn.Reader.GetString(5));
                        elGS.SetAttribute("Nom", cn.Reader.GetString(6));
                        elServices = xmlFlux.XmlCreatEl(elGS, "Services");
                    }
                    else
                    {
                        XmlElement elTechLink = xmlFlux.XmlFindElFromAtt(xmlFlux.root, "Guid", sTechLinkLast);
                        elServices = xmlFlux.XmlGetFirstElFromName(elTechLink, "Services");

                    }
                    if (!cn.Reader.IsDBNull(7))
                    {
                        XmlElement elS = xmlFlux.XmlCreatEl(elServices, "Service");
                        elS.SetAttribute("Guid", cn.Reader.GetString(7));
                        elS.SetAttribute("Nom", cn.Reader.GetString(8));
                        elS.SetAttribute("Protocol", cn.Reader.GetString(9));
                        elS.SetAttribute("Ports", cn.Reader.GetString(10));
                    }
                }
            }
            cn.CBReaderClose();


            if (cn.CBRecherche("select distinct InterLink.Id, InterLink.GuidInterLink, GuidServerSiteIn, null, ServerPhy.GuidServerPhy, NomServerPhy, NomLocation, NCard.GuidNCard, IPAddr, Alias.GuidAlias, NomAlias, NomNCard, IPNat, VLan.GuidVLan, VlanClass.GuidVlanClass from Vue, DansVue, InterLink, GInterLink, ServerPhy, Location, NCard left join Alias on NCard.GuidNCard = Alias.GuidNCard left join VLan on NCard.GuidVLan = VLan.GuidVLan left join VlanClass on VLan.GuidVlanClass = VlanClass.GuidVlanClass where Vue.GuidVue = '" + sGuidVue + "' and Vue.GuidGvue = DansVue.GuidGVue and GuidObjet = GuidGInterLink and GInterLink.GuidInterLink = InterLink.GuidInterLink and GuidServerSiteIn = ServerPhy.GuidServerPhy and ServerPhy.GuidLocation = Location.GuidLocation and ServerPhy.GuidServerPhy = NCard.GuidHote order by InterLink.Id, InterLink.GuidInterLink"))
            {
                Parent.CompleteXmlFluxFromResultSql(xmlFlux, "Origine", "Server");
                
            }
            cn.CBReaderClose();

            if (cn.CBRecherche("select distinct InterLink.Id, InterLink.GuidInterLink, GuidServerSiteOut, null, ServerPhy.GuidServerPhy, NomServerPhy, NomLocation, NCard.GuidNCard, IPAddr, Alias.GuidAlias, NomAlias, NomNCard, IPNat, VLan.GuidVLan, VlanClass.GuidVlanClass from Vue, DansVue, InterLink, GInterLink, ServerPhy, Location, NCard left join Alias on NCard.GuidNCard = Alias.GuidNCard left join VLan on NCard.GuidVLan = VLan.GuidVLan left join VlanClass on VLan.GuidVlanClass = VlanClass.GuidVlanClass where Vue.GuidVue = '" + sGuidVue + "' and Vue.GuidGvue = DansVue.GuidGVue and GuidObjet = GuidGInterLink and GInterLink.GuidInterLink = InterLink.GuidInterLink and GuidServerSiteOut = ServerPhy.GuidServerPhy and ServerPhy.GuidLocation = Location.GuidLocation and ServerPhy.GuidServerPhy = NCard.GuidHote  order by InterLink.Id, InterLink.GuidInterLink"))
            {
                Parent.CompleteXmlFluxFromResultSql(xmlFlux, "Cible", "Server");
            }
            cn.CBReaderClose();

            Parent.ActiveXmlFluxInterSiteFromDB(xmlFlux, "Out", sGuidVue);
            Parent.ActiveXmlFluxInterSiteFromDB(xmlFlux, "In", sGuidVue);

            IEnumerator ienum = xmlFlux.root.GetEnumerator();
            XmlNode Node;

            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element && Node.Name == "Flux")
                {
                    oTab = new DrawTab(parent, "TabEACB");

                    AnalyseFluxNode(brmk, xmlFlux, (XmlElement)Node, oTab);

                }
            }

            /*
            ArrayList aflux = new ArrayList();
            string IPSource, IPNatSource, NomSource, LocationSource, IPCible, IPNatCible, NomCible, LocationCible;

            if (cn.CBRecherche("SELECT DISTINCT InterLink.GuidInterLink, Id, NomInterLink, NomGroupService, Protocole, Ports From Vue, DansVue, InterLink, GInterLink, GroupService, ServiceLink, Service WHERE	Vue.GuidVue ='" + sGuidVue + "' and Vue.GuidGvue=DansVue.GuidGVue and GuidObjet=GuidGInterLink and GInterLink.GuidInterLink=InterLink.GuidInterLink AND InterLink.GuidGroupService=GroupService.GuidGroupService AND GroupService.GuidGroupService=ServiceLink.GuidGroupService AND ServiceLink.GuidService=Service.GuidService ORDER BY Id"))
            {
                while (cn.Reader.Read())
                {
                    string sId = "";
                    if (!cn.Reader.IsDBNull(1)) sId = cn.Reader.GetString(1);
                    aflux.Add(cn.Reader.GetString(0) + ";" + sId + ";" + cn.Reader.GetString(2) + ";" + cn.Reader.GetString(3) + ";" + cn.Reader.GetString(4) + ";" + cn.Reader.GetString(5));
                }

            }
            cn.CBReaderClose();

            for (int i = 0; i < aflux.Count; i++)
            {
                IPSource = ""; IPNatSource = ""; NomSource = ""; LocationSource = "";
                string[] aEnreg = ((string)aflux[i]).Split(';');
                //if (cn.CBRecherche("SELECT IPAddr, NomServerPhy, NomLocation From NCardLinkOut, NCard, ServerPhy, Location, GServerPhy, DansVue, DansTypeVue, Vue WHERE DansTypeVue.GuidTypeVue =Vue.GuidTypeVue and ServerPhy.GuidLocation=Location.GuidLocation and DansVue.GuidVue =Vue.GuidVue AND Vue.GuidVue='" + sGuidVue + "' and DansTypeVue.GuidObjet=NCard.GuidNCard and DansVue.GuidObjet=GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and NCardLinkOut.GuidNCard=NCard.GuidNCard AND NCard.GuidHote=ServerPhy.GuidServerPhy AND GuidTechLink='" + aEnreg[0] + "'"))
                if (cn.CBRecherche("SELECT DISTINCT IPAddr, IPNat, NomServerPhy, NomLocation From NCardInterLinkIn, NCard, ServerPhy, Location WHERE ServerPhy.GuidLocation=Location.GuidLocation and NCardInterLinkIn.GuidNCard=NCard.GuidNCard AND NCard.GuidHote=ServerPhy.GuidServerPhy AND GuidInterLink='" + aEnreg[0] + "'"))
                {
                    while (cn.Reader.Read())
                    {
                        IPSource += "\n" + cn.Reader.GetString(0);
                        if (cn.Reader.IsDBNull(1)) IPNatSource += "\n"; else IPNatSource += "\n" + cn.Reader.GetString(1);
                        NomSource += "\n" + cn.Reader.GetString(2);
                        if (cn.Reader.IsDBNull(3)) LocationSource += "\n"; else LocationSource += "\n" + cn.Reader.GetString(3);
                    }
                }
                else
                {
                    / *
                    cn.CBReaderClose();
                    if (cn.CBRecherche("SELECT IPAddr, NomCluster From NCardLinkOut, NCard, Cluster, GCluster, DansVue, DansTypeVue, Vue WHERE DansTypeVue.GuidTypeVue =Vue.GuidTypeVue and DansVue.GuidVue =Vue.GuidVue AND Vue.GuidVue='" + sGuidVue + "' and DansTypeVue.GuidObjet=NCard.GuidNCard and DansVue.GuidObjet=GuidGCluster and GCluster.GuidCluster=Cluster.GuidCluster and NCardLinkOut.GuidNCard=NCard.GuidNCard AND NCard.GuidHote=Cluster.GuidCluster AND GuidTechLink='" + aEnreg[0] + "'"))
                    {
                        while (cn.Reader.Read())
                        {
                            IPSource += "," + cn.Reader.GetString(0);
                            NomSource += "," + cn.Reader.GetString(1);
                            if (cn.Reader.IsDBNull(2)) LocationSource += ","; else LocationSource += "," + cn.Reader.GetString(2);
                        }
                    }* /
                }
                cn.CBReaderClose();

                IPCible = ""; IPNatCible = ""; NomCible = ""; LocationCible = "";
                //if (cn.CBRecherche("SELECT IPAddr, NomServerPhy, NomLocation From NCardLinkIn, NCard, ServerPhy, Location, GServerPhy, DansVue, DansTypeVue, Vue WHERE DansTypeVue.GuidTypeVue =Vue.GuidTypeVue and ServerPhy.GuidLocation=Location.GuidLocation and DansVue.GuidVue =Vue.GuidVue AND Vue.GuidVue='" + sGuidVue + "' and DansTypeVue.GuidObjet=NCard.GuidNCard and DansVue.GuidObjet=GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and NCardLinkIn.GuidNCard=NCard.GuidNCard AND NCard.GuidHote=ServerPhy.GuidServerPhy AND GuidTechLink='" + aEnreg[0] + "'"))
                if (cn.CBRecherche("SELECT DISTINCT IPAddr, IPNat, NomServerPhy, NomLocation From NCardInterLinkOut, NCard, ServerPhy, Location WHERE ServerPhy.GuidLocation=Location.GuidLocation and NCardInterLinkOut.GuidNCard=NCard.GuidNCard AND NCard.GuidHote=ServerPhy.GuidServerPhy AND GuidInterLink='" + aEnreg[0] + "'"))
                {
                    while (cn.Reader.Read())
                    {
                        IPCible += "\n" + cn.Reader.GetString(0);
                        if (cn.Reader.IsDBNull(1)) IPNatCible += "\n"; else IPNatCible += "\n" + cn.Reader.GetString(1);
                        NomCible += "\n" + cn.Reader.GetString(2);
                        if (cn.Reader.IsDBNull(3)) LocationCible += "\n"; else LocationCible += "\n" + cn.Reader.GetString(3);
                    }
                }
                else
                {
                    / *
                    cn.CBReaderClose();
                    if (cn.CBRecherche("SELECT IPAddr, NomCluster From NCardLinkIn, NCard, Cluster, GCluster, DansVue, DansTypeVue, Vue WHERE DansTypeVue.GuidTypeVue =Vue.GuidTypeVue and DansVue.GuidVue =Vue.GuidVue AND Vue.GuidVue='" + sGuidVue + "' and DansTypeVue.GuidObjet=NCard.GuidNCard and DansVue.GuidObjet=GuidGCluster and GCluster.GuidCluster=Cluster.GuidCluster and NCardLinkIn.GuidNCard=NCard.GuidNCard AND NCard.GuidHote=Cluster.GuidCluster AND GuidTechLink='" + aEnreg[0] + "'"))
                    {
                        while (cn.Reader.Read())
                        {
                            IPCible += "," + cn.Reader.GetString(0);
                            NomCible += "," + cn.Reader.GetString(1);
                            //if (oCnxBase.Reader.IsDBNull(2)) LocationCible += ","; else LocationCible += "," + oCnxBase.Reader.GetString(2);
                            LocationCible = ",";
                        }
                    }* /
                }
                cn.CBReaderClose();

                if (IPSource != "" && IPCible != "")
                {
                    oTab = new DrawTab(parent, "TabEACB");

                    oTab.LstValue.Add(aEnreg[1]);
                    oTab.LstValue.Add(aEnreg[2]);
                    oTab.LstValue.Add(LocationSource.Substring(1));
                    oTab.LstValue.Add(NomSource.Substring(1));
                    oTab.LstValue.Add(IPSource.Substring(1));
                    oTab.LstValue.Add(IPNatSource.Substring(1));
                    oTab.LstValue.Add(LocationCible.Substring(1));
                    oTab.LstValue.Add(NomCible.Substring(1));
                    oTab.LstValue.Add(IPCible.Substring(1));
                    oTab.LstValue.Add(IPNatCible.Substring(1));
                    oTab.LstValue.Add(aEnreg[3]);
                    oTab.LstValue.Add(aEnreg[4]);
                    oTab.LstValue.Add(aEnreg[5]);
                    if (cw.Exist(brmk) > -1) cw.InsertRowFromId(brmk, oTab);
                }
                
            }*/
        }

        public void CreatEACBTab(string brmk, string sGuidVueInf, string sGuidVueDeploy)
        {
            DrawTab oTab;

            XmlExcel xmlFlux = Parent.XmlCreatFlux(sGuidVueInf, sGuidVueDeploy);

            IEnumerator ienum = xmlFlux.root.GetEnumerator();
            XmlNode Node;

            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element && Node.Name == "Flux")
                {
                    oTab = new DrawTab(parent, "TabEACB");

                    AnalyseFluxNode(brmk, xmlFlux, (XmlElement)Node, oTab);

                }
            }

            
        }

        /*
        private void rbTadTypeDoc_CheckedChanged(object sender, EventArgs e)
        {
            lstTad.Items.Clear();
            rbTypeRech = DrawPtNiveau.rbTypeRecherche.Application;
            InitCadreRef1(tvDonnees.Nodes, "Root");
        }*/

        public void CreatMajVersionTab(ControlDoc cw, string brmk, string guidobjet)
        {
            CnxBase cn = Parent.oCnxBase;
            DrawTab oTab;
            /*
             
                t.LstField.Add(new Field("Version", "Version", 's', 0, 100, FieldOption.Select));
                t.LstField.Add(new Field("Date", "Date", 's', 0, 100, FieldOption.Select));
                t.LstField.Add(new Field("Commentaire", "Commentaire", 's', 0, 600, FieldOption.Select));
                t.LstField.Add(new Field("Architecte", "Architecte", 's', 0, 150, FieldOption.Select)); 
            
            */

            //ArrayList aMaj = new ArrayList();


            if (cn.CBRecherche("SELECT description, date, size, richText, nomcompte, guidlog, guidobjet, nomobjet, guidaction, guidcompte from log left join comment On guidlog=guidobject WHERE guidobjet ='" + guidobjet + "' ORDER BY date desc"))
            {
                while (cn.Reader.Read())
                {
                    oTab = new DrawTab(parent, brmk);

                    oTab.LstValue.Add(cn.Reader.GetString(0));
                    oTab.LstValue.Add(cn.Reader.GetDateTime(1).ToString());
                    if (cn.Reader.IsDBNull(2)) oTab.LstValue.Add("");
                    else
                    {
                        int nByte = Parent.oCnxBase.Reader.GetInt32(2);
                        byte[] rawData = new byte[nByte];
                        cn.Reader.GetBytes(3, 0, rawData, 0, nByte);
                        System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
                        rtBox.Rtf = System.Text.Encoding.UTF8.GetString(rawData);
                        oTab.LstValue.Add(cw.Format(rtBox.Rtf));
                    }

                    string[] aUser = Parent.lstKnownDTUser.Find(el => el[0] == cn.Reader.GetString(4));
                    if(aUser == null) oTab.LstValue.Add(cn.Reader.GetString(4)); else oTab.LstValue.Add(aUser[1]);
                    cw.InsertRowFromId(brmk, oTab);
                }
            }
            cn.CBReaderClose();
        }

        public void CreatServerListTab(string brmk)
        {
            XmlExcel xmlExcel = Parent.oCnxBase.Genere_ListeServer(true);
            XmlElement root = xmlExcel.docXml.DocumentElement;
            IEnumerator ienum = root.GetEnumerator();
            XmlNode Node;
            DrawTab oTab;

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
                                oTab = new DrawTab(parent, brmk);
                                oTab.InitProp();
                                IEnumerator ienumField = Node.GetEnumerator();
                                XmlNode NodeField;
                                while (ienumField.MoveNext())
                                {
                                    NodeField = (XmlNode)ienumField.Current;
                                    switch (NodeField.NodeType)
                                    {
                                        case XmlNodeType.Element:
                                            oTab.SetValueFromName(NodeField.Name, (string) NodeField.InnerText);
                                            break;
                                    }
                                }
                                if (cw.Exist(brmk) > -1) cw.InsertRowFromId(brmk, oTab);
                            }
                        }
                        break;
                }
            }
            //Parent.docXml.RemoveAll();
        }

        private void rbCatDoc_CheckedChanged(object sender, EventArgs e)
        {
            lstTad.Items.Clear();
            rbTypeDoc = rbTypeDocument.Cat;
            init_lstTad();
            gbFormat.Enabled = false;
            chkTemplate.Enabled = true;
            lstTad.Enabled = true;
        }

        private void rbTadDoc_CheckedChanged(object sender, EventArgs e)
        {
            lstTad.Items.Clear();
            rbTypeDoc = rbTypeDocument.Tad;
            init_lstTad();
            gbFormat.Enabled = true;
            rbDocDoc.Checked = true;
        }

        private void rbDocDoc_CheckedChanged(object sender, EventArgs e)
        {
            chkTemplate.Enabled = true;
            lstTad.Enabled = true;
        }

        private void rbHtmlDoc_CheckedChanged(object sender, EventArgs e)
        {
            chkTemplate.Enabled = false;
            lstTad.Enabled = false;
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bLayer_Click(object sender, EventArgs e)
        {
            Parent.wkApp.SetLayers();
        }
    }
}
