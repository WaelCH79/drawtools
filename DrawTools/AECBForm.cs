using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Collections;
using System.IO;
using Microsoft.Office.Interop.Word;
using System.Xml;


namespace DrawTools
{
    
    public partial class AECBForm : Form
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

        public AECBForm(Form1 p)
        {
            Parent = p;
            InitializeComponent();
        }
        public void init()
        {
            tbApplication.Text = Parent.cbApplication.Text; // Parent.cbApplication.SelectedItem.ToString();
            tbAppVersion.Text = Parent.wkApp.Version;
            //Parent.oCnxBase.CBAddComboBox("SELECT GuidVue, NomVue FROM Vue, TypeVue Where GuidApplication='" + Parent.GuidApplication + "' AND Vue.GuidTypeVue=TypeVue.GuidTypeVue AND( NomTypeVue='3-Production' OR NomTypeVue='5-Pre-Production' OR NomTypeVue='4-Hors Production' OR NomTypeVue='F-Service Infra')", cbGuidVue, cbVue);
            //Parent.oCnxBase.CBAddComboBox("SELECT GuidVue, NomVue FROM Vue, TypeVue Where GuidApplication='" + Parent.GuidApplication + "' AND Vue.GuidTypeVue=TypeVue.GuidTypeVue AND GroupVue='d'", cbGuidVue, cbVue);
            Parent.oCnxBase.CBAddComboBox("SELECT GuidVue, NomVue FROM Vue, TypeVue Where GuidAppVersion='" + Parent.GetGuidAppVersion() + "' AND Vue.GuidTypeVue=TypeVue.GuidTypeVue AND GroupVue='d'", cbGuidVue, cbVue);
            ShowDialog(Parent);
        }

        private void AnalyseFluxNode(XmlExcel xmlFlux, XmlElement Node)
        {
            DrawTab oTab = new DrawTab(parent, "TabEACB");
            string[] aServices, aProtocols, aPorts;
            string sSeparator = ";";

            int nbrLigne = Parent.XmlAnalyseFluxNode(sSeparator, xmlFlux, Node, oTab);

            if (nbrLigne != 0)
            {
                aServices = ((string)oTab.GetValueFromName("Service")).Replace(sSeparator, "!").Split('!');
                aProtocols = ((string)oTab.GetValueFromName("Protocol")).Replace(sSeparator, "!").Split('!');
                aPorts = ((string)oTab.GetValueFromName("Ports")).Replace(sSeparator, "!").Split('!');
                for (int i = 0; i < nbrLigne; i++)
                {
                    string[] row = { (string)oTab.GetValueFromName("Id"), (string)oTab.GetValueFromName("NomFlux"),
                                      (string)oTab.GetValueFromName("NomSrc"), (string)oTab.GetValueFromName("IPSrc"), (string)oTab.GetValueFromName("LocSrc"),
                                      (string)oTab.GetValueFromName("NomCbl"), (string)oTab.GetValueFromName("IPCbl"), (string)oTab.GetValueFromName("LocCbl"),
                                      aServices[i], aProtocols[i], aPorts[i] };
                    dgEACB.Rows.Add(row);
                }
            }
        }

        private void bGo_Click(object sender, EventArgs e)
        {
            Parent.oCnxBase.ConfDB.AddTabEACB();
            dgEACB.Rows.Clear();
            
            XmlExcel xmlFlux = Parent.XmlCreatFlux(Parent.GuidVue.ToString(), (string)cbGuidVue.Items[cbVue.SelectedIndex]);
            
            IEnumerator ienum = xmlFlux.root.GetEnumerator();
            XmlNode Node;

            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element && Node.Name == "Flux")
                {
                    AnalyseFluxNode(xmlFlux, (XmlElement)Node);
                }
            }

            /*


            string sGuidTechLink = "", sNomService = "";
            
            if (oCnxBase.CBRecherche("SELECT TechLink.GuidTechLink, TechLink.NomTechLink, NomService, Protocole, Id, Ports From Vue vInfra, Vue vDeploy, DansVue, TechLink, GTechLink, GroupService, ServiceLink, Service WHERE vInfra.GuidVue ='" + Parent.GuidVue + "' and vDeploy.GuidVue ='" + (string)cbGuidVue.Items[cbVue.SelectedIndex] + "' and vInfra.GuidGvue=DansVue.GuidGVue and GuidObjet=GuidGTechLink and GTechLink.GuidTechLink=TechLink.GuidTechLink AND TechLink.GuidGroupService=GroupService.GuidGroupService AND GroupService.GuidGroupService=ServiceLink.GuidGroupService AND ServiceLink.GuidService=Service.GuidService and vDeploy.GuidEnvironnement=ServiceLink.GuidEnvironnement ORDER BY GuidGTechLink, NomService"))

            {
                while (oCnxBase.Reader.Read())
                {
                    string sNom = oCnxBase.Reader.GetString(1);
                    string sProtocole = oCnxBase.Reader.GetString(3);
                    string sId = ""; if (!oCnxBase.Reader.IsDBNull(4)) sId = oCnxBase.Reader.GetString(4);
                    string sPort = oCnxBase.Reader.GetString(5);

                    if (sGuidTechLink != oCnxBase.Reader.GetString(0))
                    {
                        sGuidTechLink = oCnxBase.Reader.GetString(0);
                        sNomService = oCnxBase.Reader.GetString(2);

                        aflux.Add(sGuidTechLink + ";" + sNom + ";" + sNomService + ";" + sProtocole + ";" + sId + ";" + sPort);
                    }
                    else
                    {
                        if (sNomService != oCnxBase.Reader.GetString(2))
                        {
                            sNomService = oCnxBase.Reader.GetString(2);
                            aflux.Add(sGuidTechLink + ";" + sNom + ";" + sNomService + ";" + sProtocole + ";" + sId + ";" + sPort);
                        }
                        else aflux[aflux.Count - 1] += "," + sPort;
                    }
                }

            }
            oCnxBase.CBReaderClose();
            for (int i = 0; i < aflux.Count; i++)
            {
                IPSource = ""; NomSource = ""; LocationSource = "";
                string[] aEnreg = ((string)aflux[i]).Split(';');
                //if (oCnxBase.CBRecherche("SELECT IPAddr, NomServerPhy, NomLocation From NCardLinkOut, NCard, ServerPhy, Location, GServerPhy, DansVue WHERE ServerPhy.GuidLocation=Location.GuidLocation and DansVue.GuidVue='" + (string)cbGuidVue.Items[cbVue.SelectedIndex] + "' and DansVue.GuidObjet=GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and NCardLinkOut.GuidNCard=NCard.GuidNCard AND NCard.GuidHote=ServerPhy.GuidServerPhy AND GuidTechLink='" + aEnreg[0] + "'"))
                string sSql = "Select Distinct IPAddr, NomServerPhy, NomLocation From TechLink, AppUserLink, ServerPhy, Location, NCard, NCardLinkOut Where Techlink.GuidServerIn = AppUserLink.GuidAppUser and AppUserLink.GuidVue = '" + (string)cbGuidVue.Items[cbVue.SelectedIndex] + "' and TechLink.GuidTechLink = '" + aEnreg[0] + "' and ServerPhy.GuidServerPhy = AppUserLink.GuidServerPhy and ServerPhy.GuidServerPhy in (Select ServerPhy.GuidServerPhy From Vue, DansVue, GServerPhy, ServerPhy Where Vue.GuidVue = '" + (string)cbGuidVue.Items[cbVue.SelectedIndex] + "' and Vue.GuidGVue = DansVue.GuidGVue and DansVue.GuidObjet = GuidGServerPhy and GServerPhy.GuidServerPhy = ServerPhy.GuidServerPhy) and ServerPhy.GuidLocation = Location.GuidLocation and NCard.GuidHote = ServerPhy.GuidServerPhy and NCard.GuidNCard = NCardLinkOut.GuidNCard and NCardLinkOut.GuidTechLink = TechLink.GuidTechLink ";
                sSql += "Union Select Distinct IPAddr, NomServerPhy, NomLocation From TechLink, ApplicationLink, ServerPhy, Location, NCard, NCardLinkOut Where Techlink.GuidServerIn = ApplicationLink.GuidApplication and ApplicationLink.GuidVue = '" + (string)cbGuidVue.Items[cbVue.SelectedIndex] + "' and TechLink.GuidTechLink = '" + aEnreg[0] + "' and ServerPhy.GuidServerPhy = ApplicationLink.GuidServerPhy and ServerPhy.GuidServerPhy in (Select ServerPhy.GuidServerPhy From Vue, DansVue, GServerPhy, ServerPhy Where Vue.GuidVue = '" + (string)cbGuidVue.Items[cbVue.SelectedIndex] + "' and Vue.GuidGVue = DansVue.GuidGVue and DansVue.GuidObjet = GuidGServerPhy and GServerPhy.GuidServerPhy = ServerPhy.GuidServerPhy) and ServerPhy.GuidLocation = Location.GuidLocation and NCard.GuidHote = ServerPhy.GuidServerPhy and NCard.GuidNCard = NCardLinkOut.GuidNCard and NCardLinkOut.GuidTechLink = TechLink.GuidTechLink ";
                sSql += "Union Select Distinct IPAddr, NomServerPhy, NomLocation From TechLink, ServerLink, ServerPhy, Location, NCard, NCardLinkOut Where Techlink.GuidServerIn = ServerLink.GuidServer and ServerLink.GuidVue = '" + (string)cbGuidVue.Items[cbVue.SelectedIndex] + "' and TechLink.GuidTechLink = '" + aEnreg[0] + "' and ServerPhy.GuidServerPhy = ServerLink.GuidServerPhy and ServerPhy.GuidServerPhy in (Select ServerPhy.GuidServerPhy From Vue, DansVue, GServerPhy, ServerPhy Where Vue.GuidVue = '" + (string)cbGuidVue.Items[cbVue.SelectedIndex] + "' and Vue.GuidGVue = DansVue.GuidGVue and DansVue.GuidObjet = GuidGServerPhy and GServerPhy.GuidServerPhy = ServerPhy.GuidServerPhy) and ServerPhy.GuidLocation = Location.GuidLocation and NCard.GuidHote = ServerPhy.GuidServerPhy and NCard.GuidNCard = NCardLinkOut.GuidNCard and NCardLinkOut.GuidTechLink = TechLink.GuidTechLink ";
                sSql += "Union SELECT Distinct IPAddr, NomCluster, null From NCardLinkOut, NCard, Cluster, GCluster, Vue, DansVue WHERE Vue.GuidVue='" + (string)cbGuidVue.Items[cbVue.SelectedIndex] + "' and Vue.GuidGVue=DansVue.GuidGVue and DansVue.GuidObjet=GuidGCluster and GCluster.GuidCluster=Cluster.GuidCluster and NCardLinkOut.GuidNCard=NCard.GuidNCard AND NCard.GuidHote=Cluster.GuidCluster AND GuidTechLink='" + aEnreg[0] + "' and NCard.GuidNCard in (SELECT NCard.GuidNCard from NCard, GNCard, Vue, DansVue WHERE Vue.GuidVue='" + (string)cbGuidVue.Items[cbVue.SelectedIndex] + "' and Vue.GuidGVue=DansVue.GuidGVue and DansVue.GuidObjet=GuidGNCard and GNCard.GuidNCard=NCard.GuidNCard)";
                //if (oCnxBase.CBRecherche("SELECT IPAddr, NomServerPhy, NomLocation From NCardLinkOut, NCard, ServerPhy, Location, GServerPhy, Vue, DansVue WHERE ServerPhy.GuidLocation=Location.GuidLocation and Vue.GuidVue='" + (string)cbGuidVue.Items[cbVue.SelectedIndex] + "' and Vue.GuidGVue=DansVue.GuidGVue and DansVue.GuidObjet=GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and NCardLinkOut.GuidNCard=NCard.GuidNCard AND NCard.GuidHote=ServerPhy.GuidServerPhy AND GuidTechLink='" + aEnreg[0] + "' and NCard.GuidNCard in (SELECT NCard.GuidNCard from NCard, GNCard, Vue, DansVue WHERE Vue.GuidVue='" + (string)cbGuidVue.Items[cbVue.SelectedIndex] + "' and Vue.GuidGVue=DansVue.GuidGVue and DansVue.GuidObjet=GuidGNCard and GNCard.GuidNCard=NCard.GuidNCard)"))
                if (oCnxBase.CBRecherche(sSql))
                {
                    while (oCnxBase.Reader.Read())
                    {
                        IPSource += "," + oCnxBase.Reader.GetString(0);
                        NomSource += "," + oCnxBase.Reader.GetString(1);
                        if (oCnxBase.Reader.IsDBNull(2)) LocationSource += ","; else LocationSource += "," + oCnxBase.Reader.GetString(2);
                    }
                }
                
                oCnxBase.CBReaderClose();

                IPCible = ""; NomCible = ""; LocationCible = "";
                //if (oCnxBase.CBRecherche("SELECT distinct IPAddr, NomServerPhy, Location From NCardLinkIn, NCard, ServerPhy, DansTypeVue, Vue WHERE DansTypeVue.GuidTypeVue =Vue.GuidTypeVue AND NomVue='" + cbVue.SelectedItem.ToString() + "' and GuidObjet=NCard.GuidNCard and NCardLinkIn.GuidNCard=NCard.GuidNCard AND NCard.GuidServerPhy=ServerPhy.GuidServerPhy AND GuidTechLink='" + aEnreg[0] + "'"))
                //if (oCnxBase.CBRecherche("SELECT IPAddr, NomServerPhy, Location From NCardLinkIn, NCard, ServerPhy, DansTypeVue, Vue WHERE DansTypeVue.GuidTypeVue =Vue.GuidTypeVue AND GuidVue='" + (string)cbGuidVue.Items[cbVue.SelectedIndex] + "' and GuidObjet=NCard.GuidNCard and NCardLinkIn.GuidNCard=NCard.GuidNCard AND NCard.GuidServerPhy=ServerPhy.GuidServerPhy AND GuidTechLink='" + aEnreg[0] + "'"))
                //if (oCnxBase.CBRecherche("SELECT IPAddr, NomServerPhy, NomLocation From NCardLinkIn, NCard, ServerPhy, Location, GServerPhy, DansVue, DansTypeVue, Vue WHERE DansTypeVue.GuidTypeVue =Vue.GuidTypeVue and ServerPhy.GuidLocation=Location.GuidLocation and DansVue.GuidVue =Vue.GuidVue AND Vue.GuidVue='" + (string)cbGuidVue.Items[cbVue.SelectedIndex] + "' and DansTypeVue.GuidObjet=NCard.GuidNCard and DansVue.GuidObjet=GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and NCardLinkIn.GuidNCard=NCard.GuidNCard AND NCard.GuidHote=ServerPhy.GuidServerPhy AND GuidTechLink='" + aEnreg[0] + "'"))
                //if (oCnxBase.CBRecherche("SELECT IPAddr, NomServerPhy, NomLocation From NCardLinkIn, NCard, ServerPhy, Location, GServerPhy, DansVue, Vue, Environnement WHERE ServerPhy.GuidLocation=Location.GuidLocation and DansVue.GuidVue='" + (string)cbGuidVue.Items[cbVue.SelectedIndex] + "' and DansVue.GuidObjet=GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and NCardLinkIn.GuidNCard=NCard.GuidNCard AND NCard.GuidHote=ServerPhy.GuidServerPhy AND GuidTechLink='" + aEnreg[0] + "'"))
                //if (oCnxBase.CBRecherche("SELECT Distinct IPAddr, NomServerPhy, NomLocation From NCardLinkIn, NCard, ServerPhy, Location, GServerPhy, DansVue, Vue, Environnement WHERE ServerPhy.GuidLocation=Location.GuidLocation and Vue.GuidVue='" + (string)cbGuidVue.Items[cbVue.SelectedIndex] + "' and Vue.GuidGVue=DansVue.GuidGVue and DansVue.GuidObjet=GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and NCardLinkIn.GuidNCard=NCard.GuidNCard AND NCard.GuidHote=ServerPhy.GuidServerPhy AND GuidTechLink='" + aEnreg[0] + "' and NCard.GuidNCard in (SELECT NCard.GuidNCard from NCard, GNCard, Vue, DansVue WHERE Vue.GuidVue='" + (string)cbGuidVue.Items[cbVue.SelectedIndex] + "' and Vue.GuidGVue=DansVue.GuidGVue and DansVue.GuidObjet=GuidGNCard and GNCard.GuidNCard=NCard.GuidNCard)"))
                sSql = "Select Distinct IPAddr, NomServerPhy, NomLocation From TechLink, AppUserLink, ServerPhy, Location, NCard, NCardLinkIn Where Techlink.GuidServerOut = AppUserLink.GuidAppUser and AppUserLink.GuidVue = '" + (string)cbGuidVue.Items[cbVue.SelectedIndex] + "' and TechLink.GuidTechLink = '" + aEnreg[0] + "' and ServerPhy.GuidServerPhy = AppUserLink.GuidServerPhy and ServerPhy.GuidServerPhy in (Select ServerPhy.GuidServerPhy From Vue, DansVue, GServerPhy, ServerPhy Where Vue.GuidVue = '" + (string)cbGuidVue.Items[cbVue.SelectedIndex] + "' and Vue.GuidGVue = DansVue.GuidGVue and DansVue.GuidObjet = GuidGServerPhy and GServerPhy.GuidServerPhy = ServerPhy.GuidServerPhy) and ServerPhy.GuidLocation = Location.GuidLocation and NCard.GuidHote = ServerPhy.GuidServerPhy and NCard.GuidNCard = NCardLinkIn.GuidNCard and NCardLinkIn.GuidTechLink = TechLink.GuidTechLink ";
                sSql += "Union Select Distinct IPAddr, NomServerPhy, NomLocation From TechLink, ApplicationLink, ServerPhy, Location, NCard, NCardLinkIn Where Techlink.GuidServerOut = ApplicationLink.GuidApplication and ApplicationLink.GuidVue = '" + (string)cbGuidVue.Items[cbVue.SelectedIndex] + "' and TechLink.GuidTechLink = '" + aEnreg[0] + "' and ServerPhy.GuidServerPhy = ApplicationLink.GuidServerPhy and ServerPhy.GuidServerPhy in (Select ServerPhy.GuidServerPhy From Vue, DansVue, GServerPhy, ServerPhy Where Vue.GuidVue = '" + (string)cbGuidVue.Items[cbVue.SelectedIndex] + "' and Vue.GuidGVue = DansVue.GuidGVue and DansVue.GuidObjet = GuidGServerPhy and GServerPhy.GuidServerPhy = ServerPhy.GuidServerPhy) and ServerPhy.GuidLocation = Location.GuidLocation and NCard.GuidHote = ServerPhy.GuidServerPhy and NCard.GuidNCard = NCardLinkIn.GuidNCard and NCardLinkIn.GuidTechLink = TechLink.GuidTechLink ";
                sSql += "Union Select Distinct IPAddr, NomServerPhy, NomLocation From TechLink, ServerLink, ServerPhy, Location, NCard, NCardLinkIn Where Techlink.GuidServerOut = ServerLink.GuidServer and ServerLink.GuidVue = '" + (string)cbGuidVue.Items[cbVue.SelectedIndex] + "' and TechLink.GuidTechLink = '" + aEnreg[0] + "' and ServerPhy.GuidServerPhy = ServerLink.GuidServerPhy and ServerPhy.GuidServerPhy in (Select ServerPhy.GuidServerPhy From Vue, DansVue, GServerPhy, ServerPhy Where Vue.GuidVue = '" + (string)cbGuidVue.Items[cbVue.SelectedIndex] + "' and Vue.GuidGVue = DansVue.GuidGVue and DansVue.GuidObjet = GuidGServerPhy and GServerPhy.GuidServerPhy = ServerPhy.GuidServerPhy) and ServerPhy.GuidLocation = Location.GuidLocation and NCard.GuidHote = ServerPhy.GuidServerPhy and NCard.GuidNCard = NCardLinkIn.GuidNCard and NCardLinkIn.GuidTechLink = TechLink.GuidTechLink ";
                sSql += "Union SELECT Distinct IPAddr, NomCluster, null From NCardLinkIn, NCard, Cluster, GCluster, Vue, DansVue WHERE Vue.GuidVue='" + (string)cbGuidVue.Items[cbVue.SelectedIndex] + "' and Vue.GuidGVue=DansVue.GuidGVue and DansVue.GuidObjet=GuidGCluster and GCluster.GuidCluster=Cluster.GuidCluster and NCardLinkIn.GuidNCard=NCard.GuidNCard AND NCard.GuidHote=Cluster.GuidCluster AND GuidTechLink='" + aEnreg[0] + "' and NCard.GuidNCard in (SELECT NCard.GuidNCard from NCard, GNCard, Vue, DansVue WHERE Vue.GuidVue='" + (string)cbGuidVue.Items[cbVue.SelectedIndex] + "' and Vue.GuidGVue=DansVue.GuidGVue and DansVue.GuidObjet=GuidGNCard and GNCard.GuidNCard=NCard.GuidNCard)";
                if (oCnxBase.CBRecherche(sSql))
                {
                    while (oCnxBase.Reader.Read())
                    {
                        IPCible += "," + oCnxBase.Reader.GetString(0);
                        NomCible += "," + oCnxBase.Reader.GetString(1);
                        if (oCnxBase.Reader.IsDBNull(2)) LocationCible += ","; else LocationCible += "," + oCnxBase.Reader.GetString(2);
                    }
                }
                oCnxBase.CBReaderClose();

                if (IPSource != "" && IPCible != "")
                {
                    string[] row = { aEnreg[4], aEnreg[1], NomSource.Substring(1), IPSource.Substring(1), LocationSource.Substring(1), NomCible.Substring(1), IPCible.Substring(1), LocationCible.Substring(1), aEnreg[2], aEnreg[3], aEnreg[5] };
                    dgEACB.Rows.Add(row);
                }
            }
            */
        }

        private void bExport_Click(object sender, EventArgs e)
        {                      
            string sBuffer = "";

            for (int j = 0; j < dgEACB.Columns.Count; j++)
            {
                sBuffer += dgEACB.Columns[j].HeaderText + ";";
            }
            sBuffer += "\n";
            for(int i=0; i < dgEACB.Rows.Count-1; i++) {
                for (int j = 0; j < dgEACB.Columns.Count; j++)
                {
                    sBuffer += dgEACB.Rows[i].Cells[j].Value.ToString() + ";";
                }
                //sBuffer[sBuffer.Length - 1] = '\n';
                sBuffer += "\n";
            }

            string fullpath=Parent.GetFullPath(Parent.wkApp);

            if (fullpath != null)
            {
                using (StreamWriter sw = File.CreateText(fullpath + "\\AECB" + (string)cbVue.SelectedItem + ".csv"))
                {
                    sw.WriteLine(sBuffer);
                }
            }
        }

        private void bQuit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
