using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DrawTools
{
    
    public partial class FormSI : Form
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

        public FormSI(Form1 p)
        {
            Parent = p;
            InitializeComponent();

            tvDonnees.Nodes.Clear();
            //rbTypeRech = DrawPtNiveau.rbTypeRecherche.Application;
            Parent.InitCadreRef1(tvDonnees.Nodes, "Root");
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            if (tvDonnees.SelectedNode.Name != null)
            {
                //olstApp.lstApp = GetLst(tvDonnees.SelectedNode);

                Close();
            }
        }

        private ArrayList GetLst(TreeNode tn)
        {
            ArrayList LstObject = new ArrayList();

            if (tn.Nodes.Count == 0 && tn.ForeColor == Color.Blue)
            {
                string[] Name = tn.Name.Split(',');
                LstObject.Add(Name[0]);
            }
            for (int i = 0; i < tn.Nodes.Count; i++)
            {
                if (tn.Nodes[i].ForeColor == Color.Blue)
                {
                    string[] Name = tn.Nodes[i].Name.Split(',');
                    LstObject.Add(Name[0]);
                }
                else
                {
                    ArrayList lstSsObject = GetLst(tn.Nodes[i]);
                    for (int j = 0; j < lstSsObject.Count; j++) LstObject.Add(lstSsObject[j]);
                }
            }
            return LstObject;
        }
    }

    public class App
    {
        private string sGuid;
        private string sNom;
        private ArrayList lstBefore;
        private ArrayList lstAfter;
        private int iCriticiteMetier;
        private int iCriticiteMetierPropage;
        private bool bPassage;
        private bool bStopBloucle;
        private int iNbrPassage;


        public ArrayList lstAppBefore
        {
            get { return lstBefore;  }
            set { lstBefore = value; }
        }

        public ArrayList lstAppAfter
        {
            get { return lstAfter; }
            set { lstAfter = value; }
        }

        public string GuidApplication
        {
            get { return sGuid;  }
            set { sGuid = value; }
        }

        public string NomApplication
        {
            get { return sNom; }
            set { sNom = value; }
        }

        public bool Passage
        {
            get { return bPassage; }
            set { bPassage = value; }
        }

        public bool StopBoucle
        {
            get { return bStopBloucle; }
            set { bStopBloucle = value; }
        }

        public int CriticiteInitiale
        {
            get { return iCriticiteMetier; }
        }
        public int CriticiteMetier
        {
            get { return iCriticiteMetierPropage; }
            set { iCriticiteMetierPropage = value; }
        }

        public int Impact
        {
            get { return iNbrPassage; }
            set { iNbrPassage = value; }
        }

        public App( string sG, string sN, int icriticiteM)
        {
            lstBefore = new ArrayList();
            lstAfter = new ArrayList();
            sGuid = sG;
            sNom = sN;
            iCriticiteMetier = icriticiteM;
            iCriticiteMetierPropage = icriticiteM;
            bPassage = false;
            bStopBloucle = false;
            iNbrPassage = 0;
        }

        public void IncrPassage()
        {
            iNbrPassage++;
        }

        public int AjusterCriticiteMetier()
        {
            int iCriticite = 0, iNivCriticite = 0;
            if (iNbrPassage == 0) iCriticite = CriticiteInitiale;
            else iCriticite = CriticiteMetier;
            switch (iCriticite)
            {
                case 1:
                    iNivCriticite = 3;
                    break;
                case 2:
                    iNivCriticite = 2;
                    break;
                case 3:
                    iNivCriticite = 1;
                    break;
            }
            return iNivCriticite;
        }

        public void Propage()
        {
            Passage = true;
            bStopBloucle = true;
            if (CriticiteMetier > CriticiteInitiale) CriticiteMetier = CriticiteInitiale;
            for (int i=0; i<lstAppAfter.Count; i++)
            {
                App a = (App)lstAppAfter[i];
                if (a.CriticiteMetier > CriticiteMetier) a.CriticiteMetier = CriticiteMetier;
                a.IncrPassage();
                if(!a.Passage) a.Propage();
            }
        }

        public void ReversePropage()
        {
            Passage = true;
            bStopBloucle = true;
            for (int i = 0; i < lstAppBefore.Count; i++)
            {
                App a = (App)lstAppBefore[i];
                a.IncrPassage();
                if (!a.Passage) a.ReversePropage();
                if (CriticiteMetier > a.CriticiteMetier) CriticiteMetier = a.CriticiteMetier;
            }
        }

    }

    public class AppList
    {
        ArrayList lstApp;

        public AppList()
        {
            lstApp = new ArrayList();
        }

        public void Add(App a)
        {
            lstApp.Add(a);
        }

        public int Count()
        {
            return lstApp.Count;
        }
        public int SearchApp(string sGuid)
        {
            for (int i = 0; i < lstApp.Count; i++)
            {
                App a = (App) lstApp[i];
                if (a.GuidApplication == sGuid) return i;
            }
            return -1;
        }

        public App GetAppObj(int i)
        {
            return (App) lstApp[i];
        }

        public void InitPassage()
        {
            for (int i = 0; i < lstApp.Count; i++)
            {
                App a = (App)lstApp[i];
                a.StopBoucle = false;
            }
        }

        public void InitStopBoucle()
        {
            for (int i = 0; i < lstApp.Count; i++)
            {
                App a = (App)lstApp[i];
                a.Passage = false;
            }
        }

        public void Propagation()
        {
            InitStopBoucle();
            for (int i=0; i<lstApp.Count; i++)
            {
                InitPassage();
                App a = (App)lstApp[i];
                a.Propage();
            }
            InitStopBoucle();
            for (int i = 0; i < lstApp.Count; i++)
            {
                InitPassage();
                App a = (App)lstApp[i];
                a.ReversePropage();
            }
        }
    }

}
