using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DrawTools
{
    public struct bActions
    {
        public bool delBase;
        public bool delObjGraphique;
        public bool delObjVue;
        public bool bConfirmation;
    }
    public partial class FormDeleteObject : Form
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

        public FormDeleteObject(Form1 p)
        {
            Parent = p;
            InitializeComponent();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            GraphicsList graphlst = Parent.drawArea.GraphicsList;
            int n = graphlst.Count;

            for (int i = n - 1; i >= 0; i--)
            {
                DrawObject o = (DrawObject)graphlst[i];
                if (o.Selected)
                {
                    bActions stActions = new bActions();

                    stActions.delBase = ckbBase.Checked;
                    stActions.delObjGraphique = ckbGraph.Checked;
                    stActions.delObjVue = ckbGraphNew.Checked;
                    stActions.bConfirmation = true;
                    
                    stActions = o.MajDelActions(stActions);

                    if (stActions.delBase)
                    {
                        stActions.delObjGraphique = false;
                        if (!o.Remove(stActions.bConfirmation)) stActions.delObjVue = false;
                        else o.RemoveNew(false);
                    }
                    if (stActions.delObjGraphique)
                    {
                        if(!o.RemoveG()) stActions.delObjVue = false;
                    }
                    if (stActions.delObjVue)
                    {
                        o.RemoveNew(true);
                        graphlst.Remove(i);
                    }
                }
            }
            Parent.drawArea.MajObjets();
            this.Close();
        }

        private void bAnnuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void ckbBase_CheckedChanged(object sender, System.EventArgs e)
        {
            if (ckbBase.Checked)
            {
                ckbGraph.Checked = true;
                ckbGraph.Enabled = false;
            }
            else ckbGraph.Enabled = true;

            //throw new System.NotImplementedException();
        }

        void ckbGraph_CheckedChanged(object sender, System.EventArgs e)
        {
            if (ckbGraph.Checked)
            {
                ckbGraphNew.Checked = true;
                ckbGraphNew.Enabled = false;
            }
            else ckbGraphNew.Enabled = true;
            //throw new System.NotImplementedException();
        }
    }
}
