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
    public partial class FormInfrastructure : Form
    {
        public static int HImg = 18;

        private Form1 parent;
        private bool InsertApplicationRowFromDB;
        private ControlDoc cw;
        private string XmlImportFile;
        private int iPopulation, iPopulationIndex;
        private FormProgress fpg;


        private string[] statusCadreRef = {" ", "Oui", "↑ Oui ", "→ Oui", "Non", "↑ Non", "→ Non" };
        private enum istatusCadreRef 
        {
            Vide = 0,
            Oui = 1,
            Dep0ui = 2,
            OpOui = 3,
            Non = 4,
            DepNon = 5,
            OpNon = 6
        }

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

        public FormInfrastructure(Form1 p, WorkApplication wkApp=null)
        {
            Parent = p;
            string sConditionOption = "";

            InitializeComponent();
            InsertApplicationRowFromDB = false;

            if (wkApp == null)
            {
                if (Parent.oCnxBase.CBRecherche("SELECT Parameter From OptionsDraw WHERE NumOption=3")) // uniquement celles installees en production (Oui)
                    if (Parent.oCnxBase.Reader.GetString(0) == "Oui") sConditionOption = " and installee=1";
                Parent.oCnxBase.CBReaderClose();

                if (Parent.oCnxBase.CBRecherche("SELECT Application.GuidApplication, AppVersion.GuidAppVersion, NomApplication FROM Application, AppVersion Where Application.GuidApplication=AppVersion.GuidApplication " + sConditionOption))
                {
                    List<string[]> lstApp = new List<string[]>();
                    while (Parent.oCnxBase.Reader.Read())
                    {
                        string[] aEnreg = new string[3];
                        aEnreg[0] = Parent.oCnxBase.Reader.GetString(0);
                        aEnreg[1] = Parent.oCnxBase.Reader.GetString(1);
                        aEnreg[2] = Parent.oCnxBase.Reader.GetString(2);
                        lstApp.Add(aEnreg);
                    }
                    Parent.oCnxBase.CBReaderClose();
                    for (int i = 0; i < lstApp.Count; i++)
                    {
                        WorkApplication wApp = new WorkApplication(Parent, lstApp[i][0], lstApp[i][2], lstApp[i][1]);
                        InitApplication(wApp);
                    }
                }
                else Parent.oCnxBase.CBReaderClose();

            } else InitApplication(wkApp);
            
        }
               
        
        public void InitApplication(WorkApplication wkApp)
        {
            InsertApplicationRowFromDB = true;
            string sTypeVue = "2-Infrastructure";
            //"SELECT Techno.GuidTechno, Techno.GuidTechnoRef, Techno.GuidServer From DansVue, Techno, GTechno WHERE GuidVue ='98cc2d52-0d43-4b53-8223-d13ed9806f32' and GuidObjet=GuidGTechno and GTechno.GuidTechno=Techno.GuidTechno"
            
            if (Parent.oCnxBase.CBRecherche("SELECT Version, GuidVue, NomVue FROM AppVersion, Vue, TypeVue WHERE AppVersion.GuidAppVersion ='" + wkApp.GuidAppVersion + "' AND AppVersion.GuidAppVersion=Vue.GuidAppVersion AND Vue.GuidTypeVue=TypeVue.GuidTypeVue AND NomTypeVue='" + sTypeVue + "'"))
            {
                while (Parent.oCnxBase.Reader.Read())
                {
                    string[] row = new string[9];
                    row[0] = wkApp.Guid.ToString();                 //GuidAppcation
                    row[1] = wkApp.Application;                     //NomApplication
                    row[2] = wkApp.GuidAppVersion.ToString();       //GuidAppVersion
                    row[3] = Parent.oCnxBase.Reader.GetString(0);   //Version
                    row[4] = Parent.oCnxBase.Reader.GetString(1);   //GuidVue
                    row[5] = Parent.oCnxBase.Reader.GetString(2);   //NomVue
                    //row[6] = "";                                  //Date
                    //row[7] = "";                                  //Statut
                    row[8] = sTypeVue;                              //GuidTypeVue


                    dgApplication.Rows.Add(row);
                }
                Parent.oCnxBase.CBReaderClose();
                for (int i = 0; i < dgApplication.Rows.Count; i++)
                {
                    //StatutComposants((string)dgApplication.Rows[i].Cells[4].Value);
                    //Form1.ImgList fimglist = StatutComposants((string)dgApplication.Rows[i].Cells[2].Value, 0);
                    //dgApplication.Rows[i].Cells[5].Value = new Bitmap(Parent.sImgList[(int)fimglist]);
                    //dgApplication.Rows[i].Cells[6].Value = (int)fimglist;
                }
            }
            else
            {
                Parent.oCnxBase.CBReaderClose();
            }
            InsertApplicationRowFromDB = false;
        }

        public Form1.ImgList StatutComposants(string sVue)
        {
            Form1.ImgList fimglistApp = Form1.ImgList.fail;
            Form1.ImgList fimglistEng = Form1.ImgList.fail;

            dgComposant.Rows.Clear();


            if (Parent.oCnxBase.CBRecherche("SELECT MIN(ValIndicator), Max(Norme) From Vue, DansVue, GServer, Server, ServerTypeLink, ServerType, Techno, TechnoRef, IndicatorLink, Indicator WHERE  Vue.GuidVue ='" + sVue + "' and Vue.GuidGVue=DansVue.GuidGVue and NomIndicator='1-Fin Support' and DansVue.GuidObjet=GuidGServer and GServer.GuidServer=Server.GuidServer and Server.GuidServer = ServerTYpeLink.GuidServer and ServerTypeLink.GuidServerType = ServerType.GuidServerType and ServerType.GuidServerType=Techno.GuidTechnoHost and TechnoRef.GuidTechnoRef=Techno.GuidTechnoRef and TechnoRef.GuidTechnoRef=IndicatorLink.GuidObjet and IndicatorLink.GuidIndicator=Indicator.GuidIndicator GROUP BY Vue.GuidVue"))
            {
                DateTime dt = DateTime.Now; //, dMin = DateTime.MaxValue;
                //dt = new DateTime(2020, 12, 30);
                TimeSpan ts = new TimeSpan(180, 0, 0, 0);
                Bitmap img = null;

                DateTime dtc = DateTime.FromOADate(Parent.oCnxBase.Reader.GetDouble(0));

                if (DateTime.Compare(dtc, dt) < 0) fimglistEng = Form1.ImgList.fail;
                else if (DateTime.Compare(dtc - ts, dt) < 0) fimglistEng = Form1.ImgList.alert;
                else fimglistEng = Form1.ImgList.pass;
                fimglistApp = fimglistEng;

                dgApplication.Rows[dgApplication.RowCount - 1].Cells[6].Value = dtc.ToShortDateString();
                img = (Bitmap)Image.FromFile(Parent.sPathRoot + @"\bouton\" + Parent.sImgList[(int)fimglistApp], true);
                dgApplication.Rows[dgApplication.RowCount - 1].Cells[7].Value = new Bitmap(img, new Size(HImg * img.Width / img.Height, HImg));
                dgApplication.Rows[dgApplication.RowCount - 1].Cells[8].Value = (int)fimglistApp;

                Parent.oCnxBase.CBReaderClose();

            }
            else Parent.oCnxBase.CBReaderClose();

            

            return fimglistApp;
        }

        public Form1.ImgList StatutComposants(string sVue, int iFonction)
        {
            Form1.ImgList fimglistApp = Form1.ImgList.pass;
            Form1.ImgList fimglistEng = Form1.ImgList.fail;
            bool MajColApp = false;
            Guid guid = Parent.GuidVue;

            if (Parent.oCnxBase.CBRecherche("SELECT DISTINCT TechnoRef.GuidTechnoRef, NomTechnoRef, Version, ValIndicator, 0, Norme From Vue, DansVue, GServer, Server, ServerTypeLink, ServerType, Techno, TechnoRef, IndicatorLink, Indicator WHERE	Vue.GuidVue ='" + sVue + "' and Vue.GuidGVue=DansVue.GuidGVue and NomIndicator='1-Fin Support' and DansVue.GuidObjet=GuidGServer and GServer.GuidServer=Server.GuidServer and Server.GuidServer = ServerTYpeLink.GuidServer and ServerTypeLink.GuidServerType = ServerType.GuidServerType and ServerType.GuidServerType=Techno.GuidTechnoHost and TechnoRef.GuidTechnoRef=Techno.GuidTechnoRef and TechnoRef.GuidTechnoRef=IndicatorLink.GuidObjet and IndicatorLink.GuidIndicator=Indicator.GuidIndicator Order by NomTechnoRef"))
            //if (Parent.oCnxBase.CBRecherche("SELECT DISTINCT " + sGuidTechnoRef + "NomTechnoRef, Version, DateFinMain, 0, Norme From DansVue, GServer, Server, ServerTypeLink, ServerType, Techno, TechnoRef WHERE	GuidVue ='" + sVue + "' and GuidObjet=GuidGServer and GServer.GuidServer=Server.GuidServer and Server.GuidServer = ServerTYpeLink.GuidServer and ServerTypeLink.GuidServerType = ServerType.GuidServerType and ServerType.GuidServerType=Techno.GuidServerType and TechnoRef.GuidTechnoRef=Techno.GuidTechnoRef"))
            //if (Parent.oCnxBase.CBRecherche("SELECT DISTINCT TechnoRef.GuidTechnoRef, NomTechnoRef, Version, DateFinMain, 0, Norme From DansVue, Techno, GTechno, TechnoRef WHERE GuidVue ='" + sVue + "' and TechnoRef.GuidTechnoRef=Techno.GuidTechnoRef and GuidObjet=GuidGTechno and GTechno.GuidTechno=Techno.GuidTechno"))
            {
                DateTime dt = DateTime.Now, dMin=DateTime.MaxValue;
                TimeSpan ts = new TimeSpan(180, 0, 0, 0);
                Bitmap img=null;

                while (Parent.oCnxBase.Reader.Read())
                {
                    DateTime dtc = DateTime.FromOADate(Parent.oCnxBase.Reader.GetDouble(3));
                    switch (iFonction)
                    {
                        case -1:
                            string[] row = new string[7];
                            if (DateTime.Compare(dtc, dt) < 0) fimglistEng = Form1.ImgList.fail;
                            else if (DateTime.Compare(dtc - ts, dt) < 0) fimglistEng = Form1.ImgList.alert;
                            else fimglistEng = Form1.ImgList.pass;

                            row[0] = Parent.oCnxBase.Reader.GetString(0);
                            row[1] = Parent.oCnxBase.Reader.GetString(1);
                            if (!Parent.oCnxBase.Reader.IsDBNull(2)) row[2] = Parent.oCnxBase.Reader.GetString(2);
                            row[3] = dtc.ToShortDateString();

                            dgComposant.Rows.Add(row);
                            img = (Bitmap)Image.FromFile(Parent.sPathRoot + @"\bouton\" + Parent.sImgList[(int)fimglistEng], true);
                            dgComposant.Rows[dgComposant.RowCount - 1].Cells[5].Value = new Bitmap(img, new Size(HImg * img.Width / img.Height, HImg));

                            img = (Bitmap)Image.FromFile(Parent.sPathRoot + @"\bouton\" + Parent.sImgList[(int)Form1.ImgList.Nettbd + Parent.oCnxBase.Reader.GetInt32(5)], true);
                            dgComposant.Rows[dgComposant.RowCount - 1].Cells[6].Value = new Bitmap(img, new Size(HImg * img.Width / img.Height, HImg));

                            break;
                        case -2:
                            string s = Parent.oCnxBase.Reader.GetString(1);
                            cw.InsertRowFromReaderId("Tec" + guid.ToString().Replace("-", ""), Parent.oCnxBase.Reader, "TabTechnoRef");
                            break;
                        default:
                            break;
                    }
                    if (MajColApp)
                    {
                        dgApplication.Rows[iFonction].Cells[4].Value = dMin.ToShortDateString();
                        img = (Bitmap)Image.FromFile(Parent.sPathRoot + @"\bouton\" + Parent.sImgList[(int)fimglistApp], true);
                        dgApplication.Rows[iFonction].Cells[5].Value = new Bitmap(img, new Size(HImg * img.Width / img.Height, HImg));
                        dgApplication.Rows[iFonction].Cells[6].Value = (int)fimglistApp;
                    }
                        if (fimglistEng > fimglistApp) fimglistApp = fimglistEng;
                    // Bitmap markingUnderMouse = (Bitmap)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                }
                Parent.oCnxBase.CBReaderClose();
            } else Parent.oCnxBase.CBReaderClose();

            return fimglistApp;
        }

        void dgApplication_SelectionChanged(object sender, System.EventArgs e)
        {
            
            if (!InsertApplicationRowFromDB)
            {
                dgComposant.Rows.Clear();
                StatutComposants((string) dgApplication.SelectedRows[0].Cells[4].Value, -1);
            }
            //throw new System.NotImplementedException();
        }

        private void CreatReport(DataGridViewRow r)
        {
            string sBookServer = "Server";
            string sBookApp = "Applications";
            Guid guid = Parent.GuidVue;
            ArrayList lstGuidVuePhy = new ArrayList();
            if (cw.Exist(sBookApp) > -1)
            {
                //Chargement des Tables specifiques
                Parent.oCnxBase.ConfDB.AddTabApp();
                
                DrawTab oTab = new DrawTab(Parent, "TabApp");

                oTab.LstValue.Add(r.Cells[1].Value.ToString()); // NomApplication
                oTab.LstValue.Add(r.Cells[6].Value); // iStatus

                cw.InsertRowFromId(sBookApp, oTab);
            }
            if (cw.Exist(sBookServer) > -1)
            {
                string sVueBookmark = "n" + r.Cells[2].Value.ToString().Replace("-", "");
                string sDiagi = "Diag" + r.Cells[2].Value.ToString().Replace("-", "");
                string sTechi = "Tec" + r.Cells[2].Value.ToString().Replace("-", "");
                string sFlowi = "Flo" + r.Cells[2].Value.ToString().Replace("-", "");
                string sServeri = "Ser" + r.Cells[2].Value.ToString().Replace("-", "");
                string sPhysicali = "Phy" + r.Cells[0].Value.ToString().Replace("-", "");
                                
                Parent.GuidVue = new Guid((string)r.Cells[4].Value);
                Parent.wkApp = new WorkApplication(Parent, (string)r.Cells[4].Value, null, (string)r.Cells[2].Value);
                Parent.sTypeVue = (string)r.Cells[8].Value;
                cw.setDocPath(Parent.GetFullPath(Parent.wkApp) + "\\Report");

                //Parent.oCnxBase.CBRecherche("SELECT GuidVue, NomVue, NomTypeVue FROM Vue, TypeVue WHERE Vue.GuidTypeVue=TypeVue.GuidTypeVue AND (NomTypeVue='3-Production' OR NomTypeVue='5-Pre-Production' OR NomTypeVue='4-Hors Production' OR NomTypeVue='F-Service Infra') AND GuidApplication='" + Parent.GuidApplication + "'");
                Parent.oCnxBase.CBRecherche("SELECT GuidVue, NomVue, NomTypeVue FROM Vue, TypeVue WHERE Vue.GuidTypeVue=TypeVue.GuidTypeVue AND GroupVue='d' AND GuidAppVersion='" + Parent.wkApp.GuidAppVersion + "'");
                while (Parent.oCnxBase.Reader.Read()) lstGuidVuePhy.Add((object)(Parent.oCnxBase.Reader.GetString(0) + "," + Parent.oCnxBase.Reader.GetString(1) + "," + Parent.oCnxBase.Reader.GetString(2)));
                Parent.oCnxBase.CBReaderClose();

                cw.InsertTextFromId(sBookServer, false, r.Cells[1].Value + " Infrastructure (" + r.Cells[5].Value + ")\n", "Titre 1");
                cw.InsertTextFromId(sBookServer, false, "Infrastructure architecture diagrams\n", "Titre 2");
                cw.InsertTextFromId(sBookServer, false, "\n", null);
                cw.CreatIdFromIdP(sDiagi, sBookServer);
                cw.InsertTextFromId(sBookServer, false, "Technologies used\n", "Titre 2");
                cw.InsertTextFromId(sBookServer, false, "\n", null);
                cw.CreatIdFromIdP(sTechi, sBookServer);
                cw.InsertTextFromId(sBookServer, false, "Infrastructure flows\n", "Titre 2");
                cw.InsertTextFromId(sBookServer, false, "\n", null);
                cw.CreatIdFromIdP(sFlowi, sBookServer);
                cw.InsertTextFromId(sBookServer, false, "Server Type\n", "Titre 2");
                cw.InsertTextFromId(sBookServer, false, "\n", null);
                cw.CreatIdFromIdP(sServeri, sBookServer);
                cw.InsertTextFromId(sBookServer, false, "Physical architecture diagrams\n", "Titre 2");
                cw.InsertTextFromId(sBookServer, false, "\n", null);
                cw.CreatIdFromIdP(sPhysicali, sBookServer);

                Parent.LoadVue();              

                //inserer le diagram de la vue
                string sDiagram = parent.SaveDiagram(r.Cells[3].Value.ToString(), Parent.wkApp, "");
                if (cw.Exist(sVueBookmark) == -1)
                {
                    if (cw.Exist(sDiagi) > -1)
                    {
                        cw.InsertTextFromId(sDiagi, false, "\n", null);
                        cw.CreatIdFromIdP(sVueBookmark, sDiagi);
                        cw.InsertTextFromId(sVueBookmark, true, "\n", null);
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
                //Techno Utilisées
                if (cw.Exist(sTechi) > -1)
                {
                    //Entete Tableau Techno utilisées
                    Parent.oCnxBase.ConfDB.AddTabTechnoRef();//Chargement des Tables specifiques
                    n = Parent.oCnxBase.ConfDB.FindTable("TabTechnoRef");
                    if (n > -1)
                    {
                        Table t = (Table)Parent.oCnxBase.ConfDB.LstTable[n];
                        cw.InsertHeadTabFromId(sTechi, false, t, null);
                    }
                    //Insert Techno dans le tableau
                    StatutComposants(guid.ToString(), -2);

                }
                //Entete Tableau Link chapitre Applicatif
                if (cw.Exist(sFlowi) > -1)
                {
                    n = Parent.oCnxBase.ConfDB.FindTable("TechLink");
                    if (n > -1)
                    {
                        Table t = (Table)Parent.oCnxBase.ConfDB.LstTable[n];
                        cw.InsertHeadTabFromId(sFlowi, true, t, null);
                    }
                }


                DrawObject o = null;
                for (int j = 0; j < Parent.drawArea.GraphicsList.Count; j++)
                {
                    o = Parent.drawArea.GraphicsList[j];
                    if (o.GetType() == typeof(DrawServer) || o.GetType() == typeof(DrawTechLink)) o.CWInsert(cw, 'I');
                }


                Parent.drawArea.GraphicsList.Clear();
                for (int i = 0; i < lstGuidVuePhy.Count; i++)
                {
                    string[] fields = ((string)lstGuidVuePhy[i]).Split(','); //GuidVue, NomVue, NomTypeVue

                    Parent.GuidVue = new Guid(fields[0]);
                    Parent.tbTypeVue.Text = fields[2];
                    Parent.sTypeVue = fields[2];
                    //Parent.cbTypeVue.SelectedItem = fields[2]; 
                    //string sDiagBookmark = "Diag" + fields[0].Replace("-", "");
                    sVueBookmark = "n" + fields[0].Replace("-", "");
                    Parent.LoadVue();

                    cw.InsertTextFromId(sPhysicali, false, fields[1] + "\n", "Titre 3");
                    cw.InsertTextFromId(sPhysicali, false, "\n", null);
                    cw.CreatIdFromIdP(sVueBookmark, sPhysicali);

                    sDiagram = parent.SaveDiagram(fields[1], Parent.wkApp, "");
                    cw.InsertTextFromId(sVueBookmark, true, "\n", null);
                    cw.InsertImgFromId(sVueBookmark, false, sDiagram, null);
                    cw.InsertTextFromId(sVueBookmark, false, "\n", null);
                                        
                    Parent.drawArea.GraphicsList.Clear();
                }

                
            }

            Parent.wkApp = null;
            Parent.drawArea.GraphicsList.Clear();
            //DrawObject o = null;
        }

        private void bReport_Click(object sender, EventArgs e)
        {
            cw = null;
            cw = new ControlWord(Parent, Parent.sPathRoot + @"\Infra_modele-V3R1-Eng.dotx", true);
            if (cw != null)
            {
                CreatReport(dgApplication.SelectedRows[0]);
            }
        }

        private void initBackGroundProcessReport()
        {
            fpg.oWork.DoWork += new DoWorkEventHandler(DoWorkReport);
        }

        private void DoWorkReport(object sender, DoWorkEventArgs e)
        {
            //NOTE : Never play with the UI thread here...
            cw = null;
            cw = new ControlWord(Parent, Parent.sPathRoot + @"\Infra_modele-V3R1-Eng.dotx", true);

            for (int i = 0; i < dgApplication.Rows.Count; i++)
            {
                fpg.oWork.ReportProgress((i + 1) * 100 / dgApplication.Rows.Count, dgApplication.Rows[i].Cells[1].Value);

                //If cancel button was pressed while the execution is in progress
                //Change the state from cancellation ---> cancel'ed
                if (fpg.oWork.CancellationPending)
                {
                    e.Cancel = true;
                    fpg.oWork.ReportProgress(0);
                    return;
                }
                CreatReport(dgApplication.Rows[i]);
                //Form1.ImgList fimglist = StatutComposants((string)dgApplication.Rows[i].Cells[2].Value, 0);
                //dgApplication.Rows[i].Cells[5].Value = new Bitmap(Parent.sImgList[(int)fimglist]);
            }
        }

        private void bReportAll_Click(object sender, EventArgs e)
        {
            /*fpg = new FormProgress(this, true);
            initBackGroundProcessReport();
            fpg.Show(Owner.Owner);
            fpg.init();
            */
            /*
            cw = null;

            ControlWord ocw = null;
            ocw = new ControlWord(Parent, Parent.sPathRoot + @"\Infra_modele-V3R1-Eng.dotx", true);
            cw = (ControlDoc)ocw;
            */
   

            ControlHtml och = null;
            och = new ControlHtml(Parent, Parent.sPathRoot + @"\infra.html");
            cw = (ControlDoc)och;


            //for (int i = 0; i < dgApplication.Rows.Count; i++)
            for (int i = 0; i < 1; i++)
            {
                CreatReport(dgApplication.Rows[i]);
                //Form1.ImgList fimglist = StatutComposants((string)dgApplication.Rows[i].Cells[2].Value, 0);
                //dgApplication.Rows[i].Cells[5].Value = new Bitmap(Parent.sImgList[(int)fimglist]);
            }

            cw.SaveDoc();
        }

        private void ExportAppXml(XmlDB xmlDB, DataGridViewRow r)
        {
            ArrayList lstVue = new ArrayList();

            //Création de l'application
            Parent.XmlCreatXmldb(xmlDB, r.Cells[0].Value.ToString());
            /*
            if (xmlDB.SetCursor("root"))
            {
                XmlElement el = xmlDB.XmlCreatEl(xmlDB.GetCursor(), "Application", "GuidApplication");
                XmlElement elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");
                xmlDB.XmlSetAttFromEl(elAtts, "GuidApplication", "s", r.Cells[0].Value.ToString());
                xmlDB.XmlSetAttFromEl(elAtts, "NomApplication", "s", r.Cells[1].Value.ToString());
                xmlDB.CursorClose();

                if (Parent.oCnxBase.CBRecherche("SELECT GuidVue, NomVue, GuidEnvironnement, GuidVueInf, TypeVue.GuidTypeVue, NomTypeVue FROM Vue, TypeVue WHERE Vue.GuidTypeVue=TypeVue.GuidTypeVue AND GuidApplication='" + r.Cells[0].Value.ToString() + "' ORDER BY NomTypeVue"))
                    while (Parent.oCnxBase.Reader.Read())
                    {
                        string sEnv = "", sGuidVueInf = "";
                        if (!Parent.oCnxBase.Reader.IsDBNull(2)) sEnv = Parent.oCnxBase.Reader.GetString(2);
                        if (!Parent.oCnxBase.Reader.IsDBNull(3)) sGuidVueInf = Parent.oCnxBase.Reader.GetString(3);
                        lstVue.Add(Parent.oCnxBase.Reader.GetString(0) + "," + Parent.oCnxBase.Reader.GetString(1) + "," + sEnv + "," + sGuidVueInf + "," + Parent.oCnxBase.Reader.GetString(4) + "," + Parent.oCnxBase.Reader.GetString(5));
                    }
                Parent.oCnxBase.CBReaderClose();
                Parent.oCnxBase.CreaXmlApplication(xmlDB, r.Cells[0].Value.ToString(), lstVue);   
            }
            */
        }

        private void bExportAll_Click(object sender, EventArgs e)
        {
            Parent.drawArea.GraphicsList.Clear();
            XmlDB xmlDB = new XmlDB(parent, "Applications");
            int iCourant = 0, iFichier = 0, iMax = 0;

            fpg = new FormProgress(this, false);
            fpg.Show(Owner.Owner);
            fpg.initbar(dgApplication.Rows.Count);


            for (int i = 0; i < dgApplication.Rows.Count; i++, iCourant++)
            {
                fpg.stepbar(dgApplication.Rows[i].Cells[1].Value.ToString(), 0);
                Parent.XmlCreatXmldb(xmlDB, dgApplication.Rows[i].Cells[0].Value.ToString());
                if (iCourant > iMax)
                {
                    xmlDB.docXml.Save(Parent.sPathRoot + "\\Apps" + iFichier + "-" + iCourant + ".xml");
                    xmlDB.docXml.RemoveAll();
                    iFichier++; iCourant = 0;
                    xmlDB = new XmlDB(parent, "Applications");
                }
                //ExportAppXml(xmlDB, dgApplication.Rows[i]);
            }
            xmlDB.docXml.Save(Parent.sPathRoot + "\\Apps" + iFichier + "-" + iCourant + ".xml");
            xmlDB.docXml.RemoveAll();
            fpg.Close();
        }

        private void bExport_Click(object sender, EventArgs e)
        {
            Parent.drawArea.GraphicsList.Clear();
            XmlDB xmlDB = new XmlDB(Parent, "Applications");


            WorkApplication wApp = new WorkApplication(Parent, dgApplication.SelectedRows[0].Cells[0].Value.ToString(), dgApplication.SelectedRows[0].Cells[1].Value.ToString(), dgApplication.SelectedRows[0].Cells[2].Value.ToString());

            Parent.XmlCreatXmldb(xmlDB, dgApplication.SelectedRows[0].Cells[0].Value.ToString());
            xmlDB.docXml.Save(Parent.GetFullPath(wApp) + "\\" + dgApplication.SelectedRows[0].Cells[1].Value.ToString() + "Serveur.xml");
            xmlDB.docXml.RemoveAll();
        }

        private void ImportXml(XmlElement elParent, DoWorkEventArgs e)
        {
            //Att bug sur l'appel de la fonction deletencardlink : delete tout les enreg lies a la carte : il faut tenir compte des autres vue

            if (elParent.Name == "Application" && elParent.ParentNode.Name == "Applications")  ReportFormProgress(elParent.GetAttribute("sNomApplication"), e);

            IEnumerator ienum = elParent.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                switch (Node.NodeType)
                {
                    case XmlNodeType.Element:
                        Parent.oCnxBase.CreatEnregFromXml((XmlElement)Node);
                        ImportXml((XmlElement)Node, e);
                        break;
                }
            }
            
        }

        private void ReportFormProgress(string sTexte, DoWorkEventArgs e)
        {
            fpg.oWork.ReportProgress(++iPopulationIndex * 100 / iPopulation, sTexte);

            //If cancel button was pressed while the execution is in progress
            //Change the state from cancellation ---> cancel'ed
            if (fpg.oWork.CancellationPending)
            {
                e.Cancel = true;
                fpg.oWork.ReportProgress(0);
                return;
            }
        }

        
        private void DoWork(object sender, DoWorkEventArgs e)
        {
            //NOTE : Never play with the UI thread here...

            Parent.docXml = new XmlDocument();
            Parent.docXml.Load(XmlImportFile);
            XmlElement root = Parent.docXml.DocumentElement;
            if (root.Name == "Applications")
            {
                iPopulation = root.ChildNodes.Count;
                iPopulationIndex = 0;
                ImportXml(root, e);
            }
        }

        private void initBackGroundProcess()
        {
            fpg.oWork.DoWork += new DoWorkEventHandler(DoWork);
        }

        private void bImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = Parent.sPathRoot +  "\\";
            openFileDialog1.Filter = "xml files (*.xml)|*.xml";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                XmlImportFile = openFileDialog1.FileName;
                fpg = new FormProgress(this, true);
                initBackGroundProcess();
                fpg.Show(Owner.Owner);
                fpg.init();
            }
        }
    }
}
