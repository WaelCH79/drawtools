using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DrawTools
{
    public partial class FormRulesPremisse : Form
    {
        private FormRules parent;

        public new FormRules Parent
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
            
        public void init()
        {
            
            for (int i = 0; i < Parent.Parent.drawArea.tools.Length; i++)
            {
                cbObjet.Items.Add(Parent.Parent.drawArea.tools[i].GetType().Name.Substring("Tool".Length));
            }

            ShowDialog(Parent);
        }
        
        public FormRulesPremisse(FormRules p)
        {
            Parent = p;
            InitializeComponent();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            if (tbPremisse.Text != "" && (string)cbObjet.SelectedItem != "" && (string)cbEvaluation.SelectedItem != "" && (string)cbOperation.SelectedItem != "" && tbValeur.Text != "" && (string)cbConnecteur.SelectedItem != "")
            {
                Parent.CreateRowPremisse(tbPremisse.Text, (string) cbObjet.SelectedItem, (string) cbEvaluation.SelectedItem, (string) cbOperation.SelectedItem, tbValeur.Text, (string) cbConnecteur.SelectedItem);
                Close();
            }
            else MessageBox.Show("Tout les champs ne sont pas remplis");
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        void cbObjet_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string sObjetSelected = (string)cbObjet.SelectedItem;
            int iTool = Parent.Parent.drawArea.getiTools((string)cbObjet.SelectedItem);
            if (iTool > -1)
            {
                cbEvaluation.Items.Clear();
                Tool tObjet = Parent.Parent.drawArea.tools[iTool];
                if (tObjet.lstFonctionEval != null)
                {
                    for (int i = 0; i < tObjet.lstFonctionEval.Count; i++)
                    {
                        cbEvaluation.Items.Add(tObjet.lstFonctionEval[i]);
                    }
                }
            }

            //throw new System.NotImplementedException();
        }

    }
}
