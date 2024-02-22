using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawTools
{
    public partial class FormPattern : Form
    {
        private Form1 parent;
        public new Form1 Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public FormPattern(Form1 p)
        {
            Parent = p;
            InitializeComponent();
            InitEvent();
        }

        public void InitEvent()
        {
            bOK.Click += BOK_Click;
            bCancel.Click += BCancel_Click;
        }

        private void BCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            //throw new NotImplementedException();
        }

        private void BOK_Click(object sender, EventArgs e)
        {
            if (tbNomPattern.Text != "")
            {
                Parent.oCnxBase.CBWrite("Insert Into Pattern  (GuidPattern, NomPattern, GuidVue, xMax, yMax) Value ('" + Guid.NewGuid() + "', '" + tbNomPattern.Text + "', '" + Parent.GetGuidVue() + "', " + Parent.drawArea.GraphicsList.GetXMax(0) + "," + Parent.drawArea.GraphicsList.GetYMax(0) + ")");
                this.Close();
            }
            else MessageBox.Show("Le Nom du pattern est un champ obligatoire.");
            //throw new NotImplementedException();
        }
    }
}
