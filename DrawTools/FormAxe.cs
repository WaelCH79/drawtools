using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MOI = Microsoft.Office.Interop;

namespace DrawTools
{
    public partial class FormAxe : Form
    {
        static public object missing = System.Type.Missing;
        private Form1 parent;
        private ToolAxes ta;
        private DrawAxes da;
        private Form1.rbTypeRecherche rbTypeRech;
        private List<string> lstLibTabTechno;
        private List<string[]> lstCadreRef;
        private ArrayList lstTechnoArea;
        //private ArrayList lstcol;

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

        public FormAxe(Form1 p, DrawAxes d, ToolAxes t)
        {
            Parent = p;
            ta = t;
            da = d;
            ta.bAppExt = false;
            InitializeComponent();

            Parent.oCnxBase.CBAddComboBox("SELECT GuidIndicator, NomIndicator FROM Indicator ORDER BY NomIndicator", cbGuidAbsisse, cbAbsisse);
            Parent.oCnxBase.CBAddComboBox("SELECT GuidIndicator, NomIndicator FROM Indicator ORDER BY NomIndicator", cbGuidAbsisse2, cbAbsisse2);
            Parent.oCnxBase.CBAddComboBox("SELECT GuidIndicator, NomIndicator FROM Indicator ORDER BY NomIndicator", cbGuidOrdonnee, cbOrdonnee);
            Parent.oCnxBase.CBAddComboBox("SELECT GuidIndicator, NomIndicator FROM Indicator ORDER BY NomIndicator", cbGuidOrdonnee2, cbOrdonnee2);
            Parent.oCnxBase.CBAddComboBox("SELECT GuidIndicator, NomIndicator FROM Indicator ORDER BY NomIndicator", cbGuidFiltre, cbFiltre);
            rbApp.Checked = true;
            rbTypeRech = Form1.rbTypeRecherche.Application;
            lstTechnoArea = new ArrayList();
            //lstcol = new ArrayList();
            lstLibTabTechno = new List<string>();
            lstCadreRef = new List<string[]>();
            parent.initlstTabTechno(lstLibTabTechno);
            //InitCadreRef1(tvDonnees.Nodes, "Root");
        }

        private void InitRoot(TreeNodeCollection tn, string guidParent)
        {
            tn.Add("Root", "System Information");
            InitVue(tn[tn.Count - 1].Nodes, "Root");
        }

        
        private void InitVue(TreeNodeCollection tn, string guidParent)
        {
            string sSelect = "";
            string sPays = "", sEnv = "";
            TreeNodeCollection tnPays = null, tnEnv = null;

            sSelect  = "select distinct CodePays, NomTypeVue, ServerPhy.GuidServerPhy, NomServerPhy ";
            sSelect += "from serverphy Left Join ncard On ServerPhy.GuidServerPhy = ncard.GuidHote Left join vlan on ncard.GuidVLan = vlan.GuidVLan, gserverphy, dansvue, vue, appversion, application, typevue ";
            sSelect += "where serverphy.GuidServerPhy = gserverphy.GuidServerPhy and gserverphy.GuidGServerPhy = dansvue.GuidObjet and dansvue.GuidGVue = vue.GuidGVue and ";
            sSelect += " vue.GuidAppVersion = appversion.GuidAppVersion and vue.GuidAppVersion = application.GuidAppversion and vue.GuidTypeVue = typevue.GuidTypeVue and Installee = 1 ";
            sSelect += "order by CodePays, NomTypeVue, NomServerPhy";

            Parent.oCnxBase.CBRecherche(sSelect);
            while (Parent.oCnxBase.Reader.Read())
            {
                string sCodePays = Parent.oCnxBase.Reader.IsDBNull(0) ? "Generic" : Parent.oCnxBase.Reader.GetString(0);
                if(sCodePays == sPays)
                {
                    if(Parent.oCnxBase.Reader.GetString(1) == sEnv)
                    {
                        tnEnv.Add(Parent.oCnxBase.Reader.GetString(2), Parent.oCnxBase.Reader.GetString(3));
                    } else
                    {
                        sEnv = Parent.oCnxBase.Reader.GetString(1);
                        tnPays.Add(sPays + sEnv, sEnv);
                        tnEnv = tnPays[tnPays.Count - 1].Nodes;
                        tnEnv.Add(Parent.oCnxBase.Reader.GetString(2), Parent.oCnxBase.Reader.GetString(3));
                    }
                } else
                {
                    tn.Add(sCodePays, sCodePays);
                    sPays = sCodePays; tnPays =  tn[tn.Count - 1].Nodes;
                    sEnv = Parent.oCnxBase.Reader.GetString(1);
                    tnPays.Add(sPays + sEnv, sEnv);
                    tnEnv = tnPays[tnPays.Count - 1].Nodes;
                    tnEnv.Add(Parent.oCnxBase.Reader.GetString(2), Parent.oCnxBase.Reader.GetString(3));
                }
            }
            Parent.oCnxBase.CBReaderClose();

            /*
            ArrayList guidTypeVue = new ArrayList();
            ArrayList NomTypeVue = new ArrayList();
            

            //sSelect = "Select GuidCadreRef, NomCadreRef FROM CadreRef WHERE GuidParent='" + guidParent + "' AND (TypeCadreRef='" + switchTechFonc + "' OR TypeCadreRef IS NULL)";
            sSelect = "Select GuidTypeVue, NomTypeVue FROM TypeVue Where (GuidTypeVue = 'ef667e58-a617-49fd-91a8-2beeda856475' or GuidTypeVue = '7afca945-9d41-48fb-b634-5b6ffda90d4e' or GuidTypeVue = '2a4c3691-e714-4d05-9400-8fbbb06f2d62') ORDER BY NomTypeVue";

            if (Parent.oCnxBase.CBRecherche(sSelect))
            {
                while (Parent.oCnxBase.Reader.Read())
                {
                    guidTypeVue.Add((object)Parent.oCnxBase.Reader.GetString(0));
                    NomTypeVue.Add((object)Parent.oCnxBase.Reader.GetString(1));
                }
            }
            Parent.oCnxBase.CBReaderClose();
            sSelect = "Select GuidCadreRefFonc FROM CadreRefFonc WHERE GuidParentFonc='" + guidParent + "'";
            if (Parent.oCnxBase.CBRecherche(sSelect))
            {
                string sGuidCadreRef = Parent.oCnxBase.Reader.GetString(0);
                Parent.oCnxBase.CBReaderClose();
                for (int i = 0; i < guidTypeVue.Count; i++)
                {
                    tn.Add((string)guidTypeVue[i], (string)NomTypeVue[i]);

                    InitCadreRef(tn[tn.Count - 1].Nodes, sGuidCadreRef);
                }
            }
            Parent.oCnxBase.CBReaderClose();
            */
        }

        private void InitCadreRef(TreeNodeCollection tn, string guidParent)
        {
            ArrayList guidCadreRef = new ArrayList();
            ArrayList NomCadreRef = new ArrayList();
            string sSelect = "";

            //sSelect = "Select GuidCadreRef, NomCadreRef FROM CadreRef WHERE GuidParent='" + guidParent + "' AND (TypeCadreRef='" + switchTechFonc + "' OR TypeCadreRef IS NULL)";
            sSelect = "Select GuidCadreRefFonc, NomCadreRefFonc FROM CadreRefFonc WHERE GuidParentFonc='" + guidParent + "'";

            if (Parent.oCnxBase.CBRecherche(sSelect))
            {
                while (Parent.oCnxBase.Reader.Read())
                {
                    guidCadreRef.Add((object)Parent.oCnxBase.Reader.GetString(0));
                    NomCadreRef.Add((object)Parent.oCnxBase.Reader.GetString(1));
                }
            }
            Parent.oCnxBase.CBReaderClose();
            for (int i = 0; i < guidCadreRef.Count; i++)
            {
                tn.Add((string)guidCadreRef[i], (string)NomCadreRef[i]);
                IniApp(tn[tn.Count - 1].Nodes, (string)guidCadreRef[i]);
                InitCadreRef(tn[tn.Count - 1].Nodes, (string)guidCadreRef[i]);
            }
        }

        private void IniApp(TreeNodeCollection tn, string guidParent)
        {
            ArrayList guidApp = new ArrayList();
            ArrayList NomApp = new ArrayList();
            string sSelect = "Select GuidApplication, NomApplication FROM Application WHERE GuidCadreRef='" + guidParent + "'";
            Parent.oCnxBase.CBRecherche(sSelect);
            while (Parent.oCnxBase.Reader.Read())
            {
                guidApp.Add((object)Parent.oCnxBase.Reader.GetString(0));
                NomApp.Add((object)Parent.oCnxBase.Reader.GetString(1));
            }
            Parent.oCnxBase.CBReaderClose();
            for (int i = 0; i < guidApp.Count; i++)
            {
                tn.Add((string)guidApp[i], (string) NomApp[i]);
                //Font fontpro = new Font("arial", 8);
                //tn[tn.Count - 1].NodeFont = new Font(fontpro, FontStyle.Bold);
                //tn[tn.Count - 1].ForeColor = Color.Blue;
                InitServer(tn[tn.Count - 1], (string)guidApp[i]);
            }
        }

        private void InitServer(TreeNode tn, string guidParent)
        {
            string sGuidVue = GetVue(tn);
            
            string sSelect = "SELECT ServerPhy.GuidServerPhy, NomServerPhy From Application, Vue, DansVue, ServerPhy, GServerPhy WHERE Application.GuidAppversion= Vue.GuidAppVersion and DansVue.GuidGVue=Vue.GuidGVue and GuidObjet=GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and Vue.GuidTypeVue ='" + sGuidVue + "' and Application.GuidApplication = '" + guidParent + "'";
            Parent.oCnxBase.CBRecherche(sSelect);
            while (Parent.oCnxBase.Reader.Read())
            {
                tn.Nodes.Add(Parent.oCnxBase.Reader.GetString(0)+","+(int)DrawArea.DrawToolType.ServerPhy, Parent.oCnxBase.Reader.GetString(1));
                Font fontpro = new Font("arial", 8);
                tn.Nodes[tn.Nodes.Count - 1].NodeFont = new Font(fontpro, FontStyle.Bold);
                tn.Nodes[tn.Nodes.Count - 1].ForeColor = Color.Blue;
            }
            Parent.oCnxBase.CBReaderClose();
        }

        private string GetVue(TreeNode tn)
        {
            string sGuidVue = "";
            if (tn.Parent != null && tn.Parent.Parent!=null) return GetVue(tn.Parent);
            else return tn.Name;

            return sGuidVue;
        }


        private void InitCadreRefTech(TreeNodeCollection tn, string guidParent)
        {
            ArrayList guidCadreRef = new ArrayList();
            ArrayList NomCadreRef = new ArrayList();
            string sSelect = "";

            //sSelect = "Select GuidCadreRef, NomCadreRef FROM CadreRef WHERE GuidParent='" + guidParent + "' AND (TypeCadreRef='" + switchTechFonc + "' OR TypeCadreRef IS NULL)";
            sSelect = "Select GuidCadreRef, NomCadreRef FROM CadreRef WHERE GuidParent='" + guidParent + "'"; //software et hard  AND (TypeCadreRef='" + switchTechFonc + "' OR TypeCadreRef IS NULL)";
                //"Select GuidCadreRefApp, NomCadreRefApp FROM CadreRefApp WHERE GuidParentApp='" + guidParent + "'";
                //"Select GuidCadreRefFonc, NomCadreRefFonc FROM CadreRefFonc WHERE GuidParentFonc='" + guidParent + "'";

            if (Parent.oCnxBase.CBRecherche(sSelect))
            {
                while (Parent.oCnxBase.Reader.Read())
                {
                    guidCadreRef.Add((object)Parent.oCnxBase.Reader.GetString(0));
                    NomCadreRef.Add((object)Parent.oCnxBase.Reader.GetString(1));
                }
            }
            Parent.oCnxBase.CBReaderClose();
            for (int i = 0; i < guidCadreRef.Count; i++)
            {
                tn.Add((string)guidCadreRef[i], (string)NomCadreRef[i]);
                InitProduit(tn[tn.Count - 1].Nodes, (string)guidCadreRef[i]);
                InitCadreRefTech(tn[tn.Count - 1].Nodes, (string)guidCadreRef[i]);
            }
        }

        
        private void InitProduit(TreeNodeCollection tn, string guidParent)
        {
            ArrayList guidCadreRef = new ArrayList();
            ArrayList NomCadreRef = new ArrayList();
            string sSelect = "";

            //sSelect = "Select GuidCadreRef, NomCadreRef FROM CadreRef WHERE GuidParent='" + guidParent + "' AND (TypeCadreRef='" + switchTechFonc + "' OR TypeCadreRef IS NULL)";
            sSelect = "Select GuidProduit, NomProduit FROM Produit WHERE GuidCadreRef='" + guidParent + "'";

            if (Parent.oCnxBase.CBRecherche(sSelect))
            {
                while (Parent.oCnxBase.Reader.Read())
                {
                    guidCadreRef.Add((object)Parent.oCnxBase.Reader.GetString(0));
                    NomCadreRef.Add((object)Parent.oCnxBase.Reader.GetString(1));
                }
            }
            Parent.oCnxBase.CBReaderClose();
            for (int i = 0; i < guidCadreRef.Count; i++)
            {
                tn.Add((string)guidCadreRef[i], (string)NomCadreRef[i]);
                
                sSelect = "Select distinct Technoref.GuidTechnoRef, NomTechnoRef FROM technoref, techno, servertypelink, Server, gserver, dansvue, vue, application WHERE Technoref.GuidTechnoRef=Techno.GuidTechnoRef and Techno.GuidTechnoHost=ServerTypeLink.GuidServerType and servertypelink.GuidServer=Server.GuidServer and server.GuidServer=gserver.GuidServer and gserver.GuidGServer=dansvue.GuidObjet and dansvue.GuidGVue=vue.GuidGVue and vue.GuidAppVersion=application.GuidAppVersion and GuidProduit='" + (string)guidCadreRef[i] + "'";
                Parent.oCnxBase.CBRecherche(sSelect);
                while (Parent.oCnxBase.Reader.Read())
                {
                    tn[tn.Count - 1].Nodes.Add(Parent.oCnxBase.Reader.GetString(0)+","+(int)DrawArea.DrawToolType.TechnoRef, Parent.oCnxBase.Reader.GetString(1));
                    Font fontpro = new Font("arial", 8);
                    tn[tn.Count - 1].Nodes[tn[tn.Count - 1].Nodes.Count - 1].NodeFont = new Font(fontpro, FontStyle.Bold);
                    tn[tn.Count - 1].Nodes[tn[tn.Count - 1].Nodes.Count - 1].ForeColor = Color.Blue;
                }
                Parent.oCnxBase.CBReaderClose();
                
            }
        }

        private bool GetNiv(Niveau[] Niv, string sGuid, string texte)
        {
            bool retour = false;
            switch (texte[0])
            {
                case '0': // Fin commercialisation
                    FinCommercialNiveau CommNiv = new FinCommercialNiveau(Parent, sGuid, texte);
                    Niv[0] = CommNiv;
                    retour = true;
                    Parent.oCnxBase.CBReaderClose();
                    break;
                case '1': // Support
                    SupportNiveau SupNiv = new SupportNiveau(Parent, sGuid, texte);
                    Niv[0] = SupNiv;
                    retour = true;
                    break;
                case '2': // Business
                    BusinessNiveau BusNiv = new BusinessNiveau(Parent, sGuid, texte);
                    Niv[0] = BusNiv;
                    retour = true;
                    break;
                case '3': //Cout
                    CoutNiveau CoutNiv = new CoutNiveau(Parent, sGuid, texte);
                    Niv[0] = CoutNiv;
                    retour = true;
                    break;
                case '4': // Complexite
                    ComplexiteNiveau ComNiv = new ComplexiteNiveau(Parent, sGuid, texte);
                    Niv[0] = ComNiv;
                    retour = true;
                    break;
                case '5': // Expertise
                    ExpertiseNiveau ExpNiv = new ExpertiseNiveau(Parent, sGuid, texte);
                    Niv[0] = ExpNiv;
                    retour = true;
                    break;
                case '6': // Securite 
                    SecuriteNiveau SecNiv = new SecuriteNiveau(Parent, sGuid, texte);
                    Niv[0] = SecNiv;
                    if (Parent.oCnxBase.CBRecherche("SELECT GuidIndicatorRef FROM Indicator WHERE GuidIndicator='" + sGuid + "'"))
                    {
                        CriticiteNiveau CriNiv1 = new CriticiteNiveau(Parent, Parent.oCnxBase.Reader.GetString(0), "");
                        Niv[1] = CriNiv1;
                        retour = true;
                    }
                    Parent.oCnxBase.CBReaderClose();
                    break;
                case '7': // Criticite
                    CriticiteNiveau CriNiv = new CriticiteNiveau(Parent, sGuid, texte);
                    Niv[0] = CriNiv;
                    retour = true;
                    break;
                case '9': // Obsolescence
                case 'B':
                case 'C':
                case 'D':
                case 'E':
                    Obsolescence ObsNiv = new Obsolescence(Parent, sGuid, texte);
                    Niv[0] = ObsNiv;
                    retour = true;
                    break;
                case 'F': // ImpactBusiness
                    ImpactBusinessNiveau ImpBusNiv = new ImpactBusinessNiveau(Parent, sGuid, texte);
                    Niv[0] = ImpBusNiv;
                    retour = true;
                    break;
                case 'G': // Impact
                    ImpactNiveau ImpNiv = new ImpactNiveau(Parent, sGuid, texte);
                    Niv[0] = ImpNiv;
                    retour = true;
                    break;
            }
            return retour;
        }

        private int ExistPtNiveau(ArrayList lst, string sTexte)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                DrawPtNiveau dpn = (DrawPtNiveau) lst[i];
                if (dpn.Texte == sTexte) return i;
            }
            return -1;
        }

        /*private ArrayList GetLst(TreeNode tn)
        {
            ArrayList LstObject = new ArrayList();

            if (tn.Nodes.Count == 0 && tn.ForeColor == Color.Blue)
            {
                Niveau[] NivX = new Niveau[2];
                Niveau[] NivY = new Niveau[2];
                if (GetNiv(NivX, (string)cbGuidAbsisse.Items[cbAbsisse.SelectedIndex], (string)cbAbsisse.SelectedItem))
                {
                    if (GetNiv(NivY, (string)cbGuidOrdonnee.Items[cbOrdonnee.SelectedIndex], (string)cbOrdonnee.SelectedItem))
                    {
                        string[] Name = tn.Name.Split(',');
                        LstObject.Add(new DrawPtNiveau(Parent, Name[0], tn.Text, NivX, NivY, rbTypeRech, (DrawArea.DrawToolType) Convert.ToInt16(Name[1])));
                    }
                }
            }
            for (int i = 0; i < tn.Nodes.Count; i++)
            {
                if (tn.Nodes[i].ForeColor == Color.Blue)
                {
                    Niveau[] NivX = new Niveau[2];
                    Niveau[] NivY = new Niveau[2];
                    if (GetNiv(NivX, (string)cbGuidAbsisse.Items[cbAbsisse.SelectedIndex], (string)cbAbsisse.SelectedItem))
                    {
                        if (GetNiv(NivY, (string)cbGuidOrdonnee.Items[cbOrdonnee.SelectedIndex], (string)cbOrdonnee.SelectedItem))
                        {
                            string[] Name = tn.Nodes[i].Name.Split(',');
                            LstObject.Add(new DrawPtNiveau(Parent, Name[0], tn.Nodes[i].Text, NivX, NivY, rbTypeRech, (DrawArea.DrawToolType)Convert.ToInt16(Name[1])));
                        }
                    }
                }
                else
                {
                    ArrayList lstSsObject = GetLst(tn.Nodes[i]);
                    for (int j = 0; j < lstSsObject.Count; j++)
                        if (ExistPtNiveau(LstObject, ((DrawPtNiveau) lstSsObject[j]).Texte) == -1) LstObject.Add(lstSsObject[j]);
                }
            }
            return LstObject;
        }*/


        //---------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            if (tvDonnees.SelectedNode.Name != null && (string)cbAbsisse.SelectedItem != null && (string)cbOrdonnee.SelectedItem != null)
            {
                ArrayList lstCriteres = new ArrayList();
                lstCriteres.Add(new Critere(Parent, (string)cbGuidAbsisse.Items[cbAbsisse.SelectedIndex], (string)cbAbsisse.SelectedItem));
                if(cbAbsisse2.SelectedItem != null) lstCriteres.Add(new Critere(Parent, (string)cbGuidAbsisse2.Items[cbAbsisse2.SelectedIndex], (string)cbAbsisse2.SelectedItem)); else lstCriteres.Add(new Critere(Parent, null, null));
                lstCriteres.Add(new Critere(Parent, (string)cbGuidOrdonnee.Items[cbOrdonnee.SelectedIndex], (string)cbOrdonnee.SelectedItem));
                if (cbOrdonnee2.SelectedItem != null) lstCriteres.Add(new Critere(Parent, (string)cbGuidOrdonnee2.Items[cbOrdonnee2.SelectedIndex], (string)cbOrdonnee2.SelectedItem)); else lstCriteres.Add(new Critere(Parent, null, null));

                Parent.report(tvDonnees.SelectedNode, tvDonnees.SelectedNode.Name, lstCriteres, rbTypeRech);

                Close();
            }
        }

        private ArrayList GetLstEffectifServerByCadre(string sGuidNode, ArrayList lstCriteres)
        {
            ArrayList LstEffectif = new ArrayList();
            if (Parent.oCnxBase.CBRecherche("Select Distinct ServerPhy.GuidServerPhy, NomServerPhy From Vue, DansVue, ServerPhy, GServerPhy, Application Where DansVue.GuidGVue=Vue.GuidGVue and GuidObjet=GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and  Vue.GuidApplication=Application.GuidApplication and Application.GuidCadreRef = '" + sGuidNode + "'"))
            {
                while (Parent.oCnxBase.Reader.Read())
                {
                    LstEffectif.Add(new Effectif(Parent, Parent.oCnxBase.Reader.GetString(0), Parent.oCnxBase.Reader.GetString(1), lstCriteres, 0));
                }
            }
            Parent.oCnxBase.CBReaderClose();

            return LstEffectif;
        }
        private ArrayList GetLstEffectifAppByCadre(string sGuidNode, ArrayList lstCriteres)
        {
            ArrayList LstEffectif = new ArrayList();

            if (Parent.oCnxBase.CBRecherche("Select GuidApplication, NomApplication FROM Application WHERE GuidCadreRef = '" + sGuidNode + "'"))
            {
                while (Parent.oCnxBase.Reader.Read())
                {
                    LstEffectif.Add(new Effectif(Parent, Parent.oCnxBase.Reader.GetString(0), Parent.oCnxBase.Reader.GetString(1), lstCriteres, 0));
                }
            }
            Parent.oCnxBase.CBReaderClose();

            return LstEffectif;
        }

        private ArrayList GetLstEffectifTechnoByProduct(string sGuidNode, ArrayList lstCriteres)
        {
            ArrayList LstEffectif = new ArrayList();

            if (Parent.oCnxBase.CBRecherche("Select GuidTechnoRef, NomTechnoRef FROM Produit, TechnoRef WHERE TechnoRef.GuidProduit = Produit.GuidProduit and GuidCadreRef='" + sGuidNode + "'"))
            {
                while (Parent.oCnxBase.Reader.Read())
                {
                    LstEffectif.Add( new Effectif(Parent,Parent.oCnxBase.Reader.GetString(0), Parent.oCnxBase.Reader.GetString(1), lstCriteres, 0));
                }
            }
            Parent.oCnxBase.CBReaderClose();

            return LstEffectif;
        }

        private ArrayList GetLstEffectifTechno(string sGuidNode, ArrayList lstCriteres)
        {
            ArrayList LstEffectif = new ArrayList();
            ArrayList LstCadreRef = new ArrayList();

            {
                ArrayList lstEffectifTemp = GetLstEffectifTechnoByProduct(sGuidNode, lstCriteres);
                for (int j = 0; j < lstEffectifTemp.Count; j++) LstEffectif.Add(lstEffectifTemp[j]);
            }

            string sSelect = "Select GuidCadreRef FROM CadreRef WHERE GuidParent='" + sGuidNode + "'"; //software et hard  AND (TypeCadreRef='" + switchTechFonc + "' OR TypeCadreRef IS NULL)";
                                                                                                       //"Select GuidCadreRefApp, NomCadreRefApp FROM CadreRefApp WHERE GuidParentApp='" + guidParent + "'";
                                                                                                       //"Select GuidCadreRefFonc, NomCadreRefFonc FROM CadreRefFonc WHERE GuidParentFonc='" + guidParent + "'";

            if (Parent.oCnxBase.CBRecherche(sSelect))
            {
                while (Parent.oCnxBase.Reader.Read())
                    LstCadreRef.Add((object)Parent.oCnxBase.Reader.GetString(0));
            }
            Parent.oCnxBase.CBReaderClose();
            for (int i = 0; i < LstCadreRef.Count; i++)
            {
                ArrayList lstEffectifTechnoTemp = GetLstEffectifTechno((string)LstCadreRef[i], lstCriteres);
                for (int j = 0; j < lstEffectifTechnoTemp.Count; j++) LstEffectif.Add(lstEffectifTechnoTemp[j]);
            }

            return LstEffectif;
        }

        private ArrayList GetLstEffectif(string sGuidNode, Form1.rbTypeRecherche rbTypeRech, ArrayList lstCriteres)
        {
            ArrayList LstEffectif = new ArrayList();
            ArrayList LstCadreRef = new ArrayList();
            string sSelect = null;

            {
                ArrayList lstEffectifTemp = null;
                if ((rbTypeRech & Form1.rbTypeRecherche.Application) != 0)
                {
                    sSelect = "Select GuidCadreRefFonc FROM CadreRefFonc WHERE GuidParentFonc='" + sGuidNode + "'";
                    lstEffectifTemp = GetLstEffectifAppByCadre(sGuidNode, lstCriteres);
                }
                else if ((rbTypeRech & Form1.rbTypeRecherche.Server) != 0)
                {
                    sSelect = "Select GuidCadreRefFonc FROM CadreRefFonc WHERE GuidParentFonc='" + sGuidNode + "'";
                    lstEffectifTemp = GetLstEffectifServerByCadre(sGuidNode, lstCriteres);
                }
                else if ((rbTypeRech & Form1.rbTypeRecherche.Techno) != 0)
                {
                    sSelect = "Select GuidCadreRef FROM CadreRef WHERE GuidParent='" + sGuidNode + "'"; //software et hard  AND (TypeCadreRef='" + switchTechFonc + "' OR TypeCadreRef IS NULL)";
                                                                                                        //"Select GuidCadreRefApp, NomCadreRefApp FROM CadreRefApp WHERE GuidParentApp='" + guidParent + "'";
                                                                                                        //"Select GuidCadreRefFonc, NomCadreRefFonc FROM CadreRefFonc WHERE GuidParentFonc='" + guidParent + "'";
                    lstEffectifTemp = GetLstEffectifTechnoByProduct(sGuidNode, lstCriteres);
                }
                for (int j = 0; j < lstEffectifTemp.Count; j++) LstEffectif.Add(lstEffectifTemp[j]);
            }

            if (Parent.oCnxBase.CBRecherche(sSelect))
            {
                while (Parent.oCnxBase.Reader.Read())
                    LstCadreRef.Add((object)Parent.oCnxBase.Reader.GetString(0));
            }
            Parent.oCnxBase.CBReaderClose();
            for (int i = 0; i < LstCadreRef.Count; i++)
            {
                ArrayList lstEffectifTemp = GetLstEffectif((string)LstCadreRef[i], rbTypeRech, lstCriteres);
                for (int j = 0; j < lstEffectifTemp.Count; j++) LstEffectif.Add(lstEffectifTemp[j]);
            }

            return LstEffectif;
        }

        /*
        private void report(string sGuidNode, ArrayList lstCriteres, Form1.rbTypeRecherche rbTypeRech)
        {
            ArrayList lstEffectif = null;

            lstEffectif = GetLstEffectif(sGuidNode, rbTypeRech, lstCriteres);

            FormProgress fp = new FormProgress(Parent, false);
            fp.Show(Parent);
            Parent.oCnxBase.SWopen(@"C:\_logfiles\test.txt");
            Parent.oCnxBase.SWwriteLog(0, "debut calcul Niveau", true);
            for (int i = 0; i < lstCriteres.Count; i++)
            {
                ArrayList lstCritere = (ArrayList)lstCriteres[i];
                Parent.oCnxBase.SWwriteLog(0, "Critere " + i + " : " + (string)lstCritere[1], false);
            }
            Parent.oCnxBase.SWwriteLog(0, "", true);

            if (lstEffectif != null && lstEffectif.Count >= 1)
            {
                //if (((DrawPtNiveau)LstPtNiveau[0]).NivAbs[0].GuidNiveau == "5f051eef-3016-4c68-a4f5-926e0ab6eb68" || ((DrawPtNiveau)LstPtNiveau[0]).NivOrd[0].GuidNiveau == "5f051eef-3016-4c68-a4f5-926e0ab6eb68")
                //{
                //    // Calcul des indicateurs Obsolescence en fonction de la date de début & fin de comfinement
                //    CalcIndicateurObsolescence();
                //}

                //ArrayList lstPtNiveauOK = new ArrayList();

                if ((rbTypeRech & Form1.rbTypeRecherche.Application) != 0)
                {
                    if (lstEffectif.Count == 1)
                    {
                        //CreatLstNiveauApp(drawArea.tools, lstPtNiveauOK);
                    }
                    else
                    {
                        Parent.oCnxBase.SWwriteLog(0, "Initialisation des deleges sur chacun des objets", true);
                        for (int i = 0; i < Parent.drawArea.tools.Length; i++) ((Tool)Parent.drawArea.tools[i]).EvalCriteres = new Tool.EVALCRITERES(((Tool)Parent.drawArea.tools[i]).CreatNiveauForApp);
                        Parent.oCnxBase.SWwriteLog(0, "", true);
                        CreatLstNiveau(fp, Parent.drawArea.tools, lstEffectif);
                    }
                }
                else if ((rbTypeRech & Form1.rbTypeRecherche.Server) != 0)
                {
                    Parent.oCnxBase.SWwriteLog(0, "Initialisation des deleges sur chacun des objets", true);
                    for (int i = 0; i < Parent.drawArea.tools.Length; i++) ((Tool)Parent.drawArea.tools[i]).EvalCriteres = new Tool.EVALCRITERES(((Tool)Parent.drawArea.tools[i]).CreatNiveauForServer);
                    Parent.oCnxBase.SWwriteLog(0, "", true);
                    CreatLstNiveau(fp, Parent.drawArea.tools, lstEffectif);
                }
                else if ((rbTypeRech & Form1.rbTypeRecherche.Techno) != 0)
                {
                    Parent.oCnxBase.SWwriteLog(0, "Initialisation des deleges sur chacun des objets", true);
                    for (int i = 0; i < Parent.drawArea.tools.Length; i++) ((Tool)Parent.drawArea.tools[i]).EvalCriteres = new Tool.EVALCRITERES(((Tool)Parent.drawArea.tools[i]).CreatNiveauForTechno);
                    Parent.oCnxBase.SWwriteLog(0, "", true);
                    CreatLstNiveau(fp, Parent.drawArea.tools, lstEffectif);
                }
            }
            
            Parent.oCnxBase.SWclose();
        }
        */

        public void CreatLstNiveau(FormProgress fp, Tool[] aTool, ArrayList lstEffectifs)
        {
            Parent.oCnxBase.SWwriteLog(2, "Les calculs des " + lstEffectifs.Count + " Effectifs", true);

            fp.initbar(lstEffectifs.Count);
            for (int i = 0; i < lstEffectifs.Count; i++)
            {
                Effectif oEffectif = (Effectif)lstEffectifs[i];
                Parent.oCnxBase.SWwriteLog(4, "calcul de l'éffectif  " + oEffectif.NomEffectif, true);
                fp.stepbar(oEffectif.NomEffectif, 0);
                for (int k = 0; k < aTool.Length; k++) ((Tool)aTool[k]).EvalCriteres(oEffectif, ckbAppExt.Checked);
                
/*
                if ((rn & RetourNiveau.Absisse) != 0 && (rn & RetourNiveau.Ordonnee) != 0)
                {
                    DrawPtNiveau dpn;
                    dpn = (DrawPtNiveau)LstPtNiveau[i];

                    Owner.Owner.oCnxBase.SWwriteLog(6, "le point Niveau : " + ((DrawPtNiveau)LstPtNiveau[i]).Texte + " possede une abscisse : " + dpn.NivAbs[0].Val + " et une ordonnée : " + dpn.NivOrd[0].Val, true);

                    if (dpn.NivAbs[1] != null)
                    {
                        dpn.SetValueFromName("NivAbs", dpn.NivAbs[0].CalculWithRef(dpn.NivAbs[1].Val));
                        dpn.SetValueFromName("IconStatusX", dpn.NivAbs[0].IconStatus(dpn.NivAbs[1].Val));
                    }
                    else dpn.SetValueFromName("NivAbs", dpn.NivAbs[0].Val);
                    if (dpn.NivOrd[1] != null)
                    {
                        dpn.SetValueFromName("NivOrd", dpn.NivOrd[0].CalculWithRef(dpn.NivOrd[1].Val));
                        dpn.SetValueFromName("IconStatusY", dpn.NivOrd[0].IconStatus(dpn.NivOrd[1].Val));
                    }
                    else dpn.SetValueFromName("NivOrd", dpn.NivOrd[0].Val);
                    lstPtNiveauOK.Add(LstPtNiveau[i]);
                }*/
            }
            Parent.oCnxBase.SWwriteLog(2, "", true);
        }

        //---------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------

        private void OK_Click(object sender, EventArgs e)
        {
            
            ToolAxes tl = (ToolAxes) Parent.drawArea.tools[(int)DrawArea.DrawToolType.Axes];
            if (tvDonnees.SelectedNode.Name != null && (string)cbAbsisse.SelectedItem != null && (string)cbOrdonnee.SelectedItem != null)
            {
                if (ckbAppExt.Checked) ta.bAppExt = true; else ta.bAppExt = false;
                ArrayList lstCriteres = new ArrayList();
                ArrayList lstEffectif = new ArrayList();
                lstCriteres.Add(new Critere(Parent, (string)cbGuidAbsisse.Items[cbAbsisse.SelectedIndex], (string)cbAbsisse.SelectedItem));
                if (cbAbsisse2.SelectedItem != null) lstCriteres.Add(new Critere(Parent, (string)cbGuidAbsisse2.Items[cbAbsisse2.SelectedIndex], (string)cbAbsisse2.SelectedItem)); else lstCriteres.Add(new Critere(Parent, null, null));
                lstCriteres.Add(new Critere(Parent, (string)cbGuidOrdonnee.Items[cbOrdonnee.SelectedIndex], (string)cbOrdonnee.SelectedItem));
                if (cbOrdonnee2.SelectedItem != null) lstCriteres.Add(new Critere(Parent, (string)cbGuidOrdonnee2.Items[cbOrdonnee2.SelectedIndex], (string)cbOrdonnee2.SelectedItem)); else lstCriteres.Add(new Critere(Parent, null, null));


                if ((string)cbFiltre.SelectedItem != null && (string)cbParamFiltre.SelectedItem != null)
                {
                    ArrayList lstCriteresFiltre = new ArrayList();

                    lstCriteresFiltre.Add(new Critere(Parent, (string)cbGuidFiltre.Items[cbFiltre.SelectedIndex], (string)cbFiltre.SelectedItem));
                    ArrayList lstEffectifFiltre = Parent.report(tvDonnees.SelectedNode, tvDonnees.SelectedNode.Name, lstCriteresFiltre, rbTypeRech);

                    //double XBorneMinFiltre = double.MaxValue, XBorneMaxFiltre = double.MinValue, YBorneMinFiltre = double.MaxValue, YBorneMaxFiltre = double.MinValue;
                    //double XBorneMoyFiltre = 0, YBorneMoyFiltre = 0;
                    for (int i = 0; i < lstEffectifFiltre.Count; i++)
                    {
                        Effectif eff = (Effectif)lstEffectifFiltre[i];
                        Niveau Niv = (Niveau)eff.lstNivEffectif[0];
                        double moy = Niv.GetMoyenne(0, 1);
                        if (Niv.Val <= moy)
                        {
                            Effectif oEffApp = new Effectif(Parent, eff.GuidEffectif, eff.NomEffectif, lstCriteres, 0);
                            lstEffectif.Add(oEffApp);
                        }
                    }
                    Parent.report(lstEffectif, lstCriteres, rbTypeRech);
                }
                else
                {
                    lstEffectif = Parent.report(tvDonnees.SelectedNode, tvDonnees.SelectedNode.Name, lstCriteres, rbTypeRech);
                }
                da.SetValueFromName("XAxe", ((string)cbAbsisse.SelectedItem).Substring(2));
                da.SetValueFromName("YAxe", ((string)cbOrdonnee.SelectedItem).Substring(2));

                double XBorneMin = double.MaxValue, XBorneMax = double.MinValue, YBorneMin = double.MaxValue, YBorneMax = double.MinValue;
                double XBorneMoy = 0, YBorneMoy = 0;
                for (int i = 0; i < lstEffectif.Count; i++)
                {
                    Niveau NivAbs = (Niveau) ((Effectif)lstEffectif[i]).lstNivEffectif[0];
                    Niveau NivAbs2 = (Niveau)((Effectif)lstEffectif[i]).lstNivEffectif[1];
                    Niveau NivOrd = (Niveau)((Effectif)lstEffectif[i]).lstNivEffectif[2];
                    Niveau NivOrd2 = (Niveau)((Effectif)lstEffectif[i]).lstNivEffectif[3];

                    if (NivAbs2 != null) NivAbs.Val = NivAbs.Val * NivAbs2.Val;
                    if (NivOrd2 != null) NivOrd.Val = NivOrd.Val * NivOrd2.Val;
                    if (XBorneMin > NivAbs.Val) XBorneMin = NivAbs.Val;
                    if (XBorneMax < NivAbs.Val) XBorneMax = NivAbs.Val;
                    if (YBorneMin > NivOrd.Val) YBorneMin = NivOrd.Val;
                    if (YBorneMax < NivOrd.Val) YBorneMax = NivOrd.Val;
                    XBorneMoy = (XBorneMoy * i + NivAbs.GetMoyenne(XBorneMin, XBorneMax)) / (i + 1);
                    YBorneMoy = (YBorneMoy * i + NivOrd.GetMoyenne(YBorneMin, YBorneMax)) / (i + 1);
                }
                da.SetValueFromName("XBorneMin", XBorneMin);
                da.SetValueFromName("XBorneMax", XBorneMax);
                da.SetValueFromName("XBorneMoy", XBorneMoy);
                da.SetValueFromName("YBorneMin", YBorneMin);
                da.SetValueFromName("YBorneMax", YBorneMax);
                da.SetValueFromName("YBorneMoy", YBorneMoy);

                int idx0 = 0, idx1 = 0, idx2 = 0, idx3 = 0;
                for (int i=0; i<lstEffectif.Count; i++)
                {
                    Effectif oEff = (Effectif)lstEffectif[i];

                    DrawPtNiveau dpn = new DrawPtNiveau(Parent, da, oEff.GuidEffectif, oEff.NomEffectif, oEff.lstNivEffectif, rbTypeRech, (DrawArea.DrawToolType)Convert.ToInt16(Name[1]));

                    double X = (double)dpn.GetValueFromName("NivAbs") - (double)da.GetValueFromName("XBorneMoy");
                    double Y = (double)dpn.GetValueFromName("NivOrd") - (double)da.GetValueFromName("YBorneMoy");

                    if (X <= 0 && Y > 0)
                    {
                        dpn.rectangle.Y = da.Rectangle.Top + da.Rectangle.Height / 2 - dpn.HEIGHTPTNIVEAU * idx0++ - dpn.WIDTHESPACEPTNIVEAU;
                        dpn.rectangle.X = da.Rectangle.Left - dpn.WIDTHESPACEPTNIVEAU - dpn.WIDTHPTNIVEAU;
                    }
                    else if (X > 0 && Y > 0)
                    {
                        dpn.rectangle.Y = da.Rectangle.Top + da.Rectangle.Height / 2 - dpn.HEIGHTPTNIVEAU * idx1++ - dpn.WIDTHESPACEPTNIVEAU;
                        dpn.rectangle.X = da.Rectangle.Right + dpn.WIDTHESPACEPTNIVEAU;
                    }
                    else if (X > 0 && Y <= 0)
                    {
                        dpn.rectangle.Y = da.Rectangle.Top + da.Rectangle.Height / 2 + dpn.HEIGHTPTNIVEAU * idx2++ + dpn.WIDTHESPACEPTNIVEAU;
                        dpn.rectangle.X = da.Rectangle.Right + dpn.WIDTHESPACEPTNIVEAU;
                    }
                    else if (X <= 0 && Y <= 0)
                    {
                        dpn.rectangle.Y = da.Rectangle.Top + da.Rectangle.Height / 2 + dpn.HEIGHTPTNIVEAU * idx3++ + dpn.WIDTHESPACEPTNIVEAU;
                        dpn.rectangle.X = da.Rectangle.Left - dpn.WIDTHESPACEPTNIVEAU - dpn.WIDTHPTNIVEAU;
                    }

                    dpn.SetValueFromName("GuidAxes", da.GuidkeyObjet.ToString());
                    tl.CreatObjetLink(dpn, "GuidAxes", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
                    tl.AddNewObject(Parent.drawArea, dpn, false);
                    //Parent.drawArea.GraphicsList.Add(dptn);
                }

                Close();
            }
            //MessageBox.Show(tvDonnees.SelectedNode.Name);
            //if (cbVue1.SelectedItem != null && ((string)cbVue1.SelectedItem).Length != 0) sGuidVue = (string)cbGuidVue.Items[cbVue1.SelectedIndex];
            //MessageBox.Show((string)cbGuidAbsisse.Items[cbAbsisse.SelectedIndex]);
            //MessageBox.Show((string)cbGuidOrdonnee.Items[cbOrdonnee.SelectedIndex]);
        }

        private void rbApp_CheckedChanged(object sender, EventArgs e)
        {
            tvDonnees.Nodes.Clear();
            rbTypeRech = Form1.rbTypeRecherche.Application;
            Parent.InitCadreRef1(tvDonnees.Nodes, "Root");
        }

        private void rbServ_CheckedChanged(object sender, EventArgs e)
        {
            tvDonnees.Nodes.Clear();
            rbTypeRech = Form1.rbTypeRecherche.Server;
            InitRoot(tvDonnees.Nodes, "Root");
        }

        private void rbTech_CheckedChanged(object sender, EventArgs e)
        {
            tvDonnees.Nodes.Clear();
            rbTypeRech = Form1.rbTypeRecherche.Techno;
            InitCadreRefTech(tvDonnees.Nodes, "Root");
        }

        private void bExportTecho_Click(object sender, EventArgs e)
        {
            if (tvDonnees.SelectedNode.Name != null)
            {
                Parent.oCnxBase.CBAddArrayListcObj("SELECT GuidTechnoArea, NomTechnoArea FROM TechnoArea ORDER BY NomTechnoArea", lstTechnoArea);
                lstCadreRef = parent.oCnxBase.InitCadreRef();

                //Range oRng;
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-us");
                MOI.Excel.Application oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = true;
                MOI.Excel.Workbook oWB = oXL.Workbooks.Add(missing);
                MOI.Excel._Worksheet oSheet = (MOI.Excel._Worksheet)oWB.ActiveSheet;
                int row = Export(tvDonnees.SelectedNode, oSheet, 1, 4);
            }
        }

        
        private int Export(TreeNode tn, MOI.Excel._Worksheet oSheet, int row, int col)
        {
            //GuidTechnoRef, TechnologyName, TechnologyVersion, TechnologyType, ObsoScore, GuidProduit, DerogationEndDate, GroupITStandardInCompetition, 
            //RoadmapUpcomingStartDate, RoadmapUpcomingEndDate, RoadmapReferenceStartDate, RoadmapReferenceEndDate, RoadmapConfinedStartDate,
            //RoadmapConfinedEndDate, RoadmapDecommissionedStartDate, RoadmapDecommissionedEndDate, RoadmapSupplierEndOfSupportDate, UserID, UpdateDate

            string requete = "";

            requete += "select distinct";
            requete += "   guidtechnoref, nomtechnoref, version, norme, obsoLink.valindicator, produit.guidproduit, ' ', ' ',";
            requete += "   upcomingstart, upcomingend, referencestart, referenceend, confinedstart, confinedend, decommstart, decommend, finsupportLink.valindicator ";
            requete += "from ";
            requete += "   technoref, indicatorlink finsupportLink, indicator finsupport, indicatorlink obsolink, indicator obso, produit left join comment on produit.guidproduit = comment.guidobject left join technoarea on produit.guidtechnoarea = technoarea.guidtechnoarea ";
            requete += "where ";
            requete += "   produit.guidproduit = technoref.guidproduit and technoref.guidtechnoref = finsupportLink.guidobjet and finsupportLink.guidindicator = finsupport.guidindicator and";
            requete += "   technoref.guidtechnoref = obsoLink.guidobjet and obsoLink.guidindicator = obso.guidindicator and";
            //requete += "   indicatorlink.guidindicator = indicator.guidindicator and nomprop = 'Usage' and nomindicator = '1-Fin Support' and";
            requete += "   ( nomprop = 'Usage' or nomprop is null) and finsupport.nomindicator = '1-Fin Support' and obso.nomindicator = '9-Obsolescence' and produit.guidproduit in (";
            requete += "      select distinct guidproduit";
            requete += "      from";
            requete += "         cadrereffonc, application, vue, dansvue, gserver, server, servertypelink, servertype, techno, technoref";
            requete += "      where";
            requete += "         application.guidcadreref = cadrereffonc.guidcadrereffonc and application.guidappversion = vue.guidappversion and";
            requete += "         vue.guidgvue = dansvue.guidgvue and dansvue.guidobjet = gserver.guidgserver and gserver.guidserver = server.guidserver and";
            requete += "         server.guidserver = servertypelink.guidserver and servertypelink.guidservertype = servertype.guidservertype and";
            requete += "         servertype.guidservertype = techno.GuidTechnoHost and techno.guidtechnoref = technoref.guidtechnoref and";
            requete += "         guidtypevue = 'd5b533a9-06ac-4f8c-a5ab-e345b0212542' and guidcadrereffonc = '" + tvDonnees.SelectedNode.Name + "'";
            requete += "      )";
            requete += "order by produit.guidproduit";

            //for (int k = 0; k < col; k++) lstcol.Add("");
            if (row == 1)
            {
                for (int i = 0; i < lstLibTabTechno.Count; i++)
                    oSheet.Cells[row, i + 1] = lstLibTabTechno[i];
            }
            row = Parent.oCnxBase.fillTechnoXls(requete, oSheet, lstCadreRef, row, col);
            for (int i = 0; i < tn.Nodes.Count; i++)
            {
                if (tn.Nodes[i].Nodes.Count > 0) row = Export(tn.Nodes[i], oSheet, row, col);
            }
            return row;
        }
    }
}
