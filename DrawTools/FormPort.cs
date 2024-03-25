using System;
using System.Windows.Forms;

namespace DrawTools
{
    public partial class FormPort : Form
    {

        public Form1 FParent { get; set; }

        public FormPort(Form1 p)
        {
            this.FParent = p;
            InitializeComponent();
        }


        // to generate Master Datagridview with your coding
        public void VLanGrid_Initialize()
        {

            dgVLan.Columns.Clear();
            this.InitGVColumn("NumeLigne", "N°", "N°", true, 40, DataGridViewTriState.True);
            this.InitGVColumn("NomService", "Nom", "Nom du Service", true, 150, DataGridViewTriState.True);
            this.InitGVColumn("Protocole", "Protocole", "Protocole", true, 70, DataGridViewTriState.True);
            this.InitGVColumn("Ports", "Ports", "Ports", true, 120, DataGridViewTriState.True);
            this.InitGVColumn("Description", "Description", "Description", true, 250, DataGridViewTriState.True);
            this.InitGVColumn("NomGroupServices", "Services", "Services", true, 200, DataGridViewTriState.True);

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
            dgvbound.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgVLan.Columns.Add(dgvbound);
        }

        private void VLanGrid_Load()
        {
            FParent.oCnxBase.PopulateDataGridFromQuery("select GuidService, row_number() over (order by NomService ) AS NumeLigne, " +
                                                        " NomService, InfoSup, Protocole, Ports, Description, NomGroupServices  from (select distinct service.*, (select  GROUP_CONCAT(distinct NomGroupService SEPARATOR ', ') from servicelink "+
                                                        " LEFT JOIN groupservice ON servicelink.GuidGroupService = groupservice.GuidGroupService "+
                                                        " where service.GuidService = servicelink.GuidService) as NomGroupServices " +
                                                        " from service " +
                                                        " LEFT JOIN servicelink ON service.GuidService = servicelink.GuidService " +
                                                        " LEFT JOIN groupservice ON servicelink.GuidGroupService = groupservice.GuidGroupService " +
                                                        " ) as tmp order by NomService", dgVLan);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string queryFind = "Select GuidService, row_number() over (order by NomService ) AS NumeLigne,  NomService, InfoSup,  " +
                                " Protocole, Ports, Description, NomGroupServices   " +
                                " from (select distinct service.*,  " +
                                " (select  GROUP_CONCAT(distinct NomGroupService SEPARATOR ', ') from servicelink  " +
                                " LEFT JOIN groupservice ON servicelink.GuidGroupService = groupservice.GuidGroupService " +
                                "  where service.GuidService = servicelink.GuidService) as NomGroupServices  from service "+
                                "  LEFT JOIN servicelink ON service.GuidService = servicelink.GuidService " +
                                "  LEFT JOIN groupservice ON servicelink.GuidGroupService = groupservice.GuidGroupService  ) as tmp " +
                                "  WHERE 1 = 1 ";

            if(! string.IsNullOrEmpty(TBNomS.Text.Trim()))
            {
                queryFind += $" And NomService Like '%{TBNomS.Text.Trim()}%'";
            }
            if (!string.IsNullOrEmpty(TBProtocole.Text.Trim()))
            {
                queryFind += $" And Protocole Like '%{TBProtocole.Text.Trim()}%'";
            }         
            if (!string.IsNullOrEmpty(TBPort.Text.Trim()))
            {
                queryFind += $" And Ports Like '%{TBPort.Text.Trim()}%'";
            }

            if (!string.IsNullOrEmpty(TBService.Text.Trim()))
            {
                queryFind += $" And NomGroupServices Like '%{TBService.Text.Trim()}%'";
            }

            queryFind += "order by NomService";
            FParent.oCnxBase.PopulateDataGridFromQuery(queryFind, dgVLan);

        }

        private void FormPort_Load(object sender, EventArgs e)
        {
            dgVLan.AutoGenerateColumns = false;
            VLanGrid_Initialize();
            VLanGrid_Load();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TBNomS.Text = string.Empty;
            TBProtocole.Text = string.Empty;
            TBPort.Text = string.Empty;

            FParent.oCnxBase.PopulateDataGridFromQuery("SELECT GuidService, row_number() over (order by GuidService desc) AS NumeLigne, NomService, InfoSup, Protocole, Ports, Description FROM drawtools.service", dgVLan);
        }
    }
}
