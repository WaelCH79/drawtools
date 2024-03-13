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
    public partial class FormVLan : Form
    {

        public Form1 FParent { get; set; }

        public FormVLan(Form1 p)
        {
            this.FParent = p;
            InitializeComponent();
        }


        // to generate Master Datagridview with your coding
        public void VLanGrid_Initialize()
        {

            dgVLan.Columns.Clear();
            this.InitGVColumn("NumeLigne", "N°", "N°", true, 50, DataGridViewTriState.True);
            this.InitGVColumn("NomVlan", "Nom", "Nom VLAN", true, 150, DataGridViewTriState.True);
            this.InitGVColumn("NumVlan", "Numéro VLAN", "Numéro VLAN", true, 50, DataGridViewTriState.True);
            this.InitGVColumn("NomReseau", "Nom du Réseau", "Nom du Réseau", true, 150, DataGridViewTriState.True);
            this.InitGVColumn("Passerelle", "Passerelle", "Passerelle", true, 150, DataGridViewTriState.True);
            this.InitGVColumn("CodePays", "Code Pays", "CodePays", true, 70, DataGridViewTriState.True);

        }

        private void InitGVColumn(String cntrlnames, String Headertext, String ToolTipText, Boolean Visible, int width, DataGridViewTriState Resizable)
        {

            DataGridViewColumn dgvbound = new DataGridViewTextBoxColumn();
            dgvbound.DataPropertyName = cntrlnames;
            dgvbound.Name = cntrlnames;
            dgvbound.HeaderText = Headertext;
            dgvbound.ToolTipText = ToolTipText;
            dgvbound.Visible = Visible;
            dgvbound.Width = width;
            dgvbound.SortMode = DataGridViewColumnSortMode.Automatic;
            dgvbound.Resizable = Resizable;
            dgvbound.ReadOnly = true;
            dgVLan.Columns.Add(dgvbound);
        }

        private void VLanGrid_Load()
        {
            FParent.oCnxBase.PopulateDataGridFromQuery("SELECT GuidVlan, row_number() over (order by GuidVlan desc) AS \"NumeLigne\", NomVlan, NumVlan, NomReseau, Passerelle, CodePays FROM drawtools.vlan", dgVLan);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string queryFind = "SELECT GuidVlan, row_number() over (order by GuidVlan desc) AS \"NumeLigne\", NomVlan, NumVlan, NomReseau, Passerelle, CodePays" +
                " FROM drawtools.vlan WHERE 1 = 1 ";

            if(! string.IsNullOrEmpty(TBNomVL.Text.Trim()))
            {
                queryFind += $" And NomVlan Like '%{TBNomVL.Text.Trim()}%'";
            }
            if (!string.IsNullOrEmpty(TBNumeVl.Text.Trim()))
            {
                queryFind += $" And NumVlan Like '%{TBNumeVl.Text.Trim()}%'";
            }
            if (!string.IsNullOrEmpty(TbNomReseau.Text.Trim()))
            {
                queryFind += $" And NomReseau Like '%{TbNomReseau.Text.Trim()}%'";
            }
            if (!string.IsNullOrEmpty(TBPasserelle.Text.Trim()))
            {
                queryFind += $" And Passerelle Like '%{TBPasserelle.Text.Trim()}%'";
            }
            if (!string.IsNullOrEmpty(TBCodePays.Text.Trim()))
            {
                queryFind += $" And CodePays Like '%{TBCodePays.Text.Trim()}%'";
            }

            FParent.oCnxBase.PopulateDataGridFromQuery(queryFind, dgVLan);

        }

        private void FormVLan_Load(object sender, EventArgs e)
        {
            dgVLan.AutoGenerateColumns = false;
            VLanGrid_Initialize();
            VLanGrid_Load();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TBNomVL.Text = string.Empty;
            TBNumeVl.Text = string.Empty;
            TbNomReseau.Text = string.Empty;
            TBPasserelle.Text = string.Empty;
            TBCodePays.Text = string.Empty;

            FParent.oCnxBase.PopulateDataGridFromQuery("SELECT GuidVlan, row_number() over (order by GuidVlan desc) AS \"NumeLigne\", NomVlan, NumVlan, NomReseau, Passerelle, CodePays FROM drawtools.vlan", dgVLan);
        }
    }
}
