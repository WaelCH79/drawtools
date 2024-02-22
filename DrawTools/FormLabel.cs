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
    public partial class FormLabel : Form
    {
        private Form1 parent;
        private DataGridView odgv;
        public string sLabel;


        public new Form1 Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        public FormLabel(Form1 p, DataGridView dgv)
        {
            Parent = p;
            odgv = dgv;
            InitializeComponent();
            InitEvent();

        }

        public void InitEvent()
        {
            bAdd.Click += BAdd_Click;
            tvLabelClass.AfterSelect += TvLabelClass_AfterSelect;
            bSave.Click += BSave_Click;
            bAnnuler.Click += BAnnuler_Click;
            bAddLink.Click += BAddLink_Click;
            bSupLink.Click += BSupLink_Click;
            dgvLabel.SelectionChanged += DgvLabel_SelectionChanged;
        }

        private void DgvLabel_SelectionChanged(object sender, EventArgs e)
        {
            bSupLink.Enabled = true;
            //throw new NotImplementedException();
        }

        private void BSupLink_Click(object sender, EventArgs e)
        {
            dgvLabel.Rows.RemoveAt(dgvLabel.CurrentRow.Index);
            if(dgvLabel.RowCount == 0) bSupLink.Enabled = false;
            //throw new NotImplementedException();
        }

        private void BAddLink_Click(object sender, EventArgs e)
        {
            int r = dgvLabel.RowCount;
            dgvLabel.Rows.Add();
            dgvLabel.Rows[r].Cells["GuidLabel"].Value = (object)tvLabelClass.SelectedNode.Name;
            dgvLabel.Rows[r].Cells["NomLabel"].Value = (object)tvLabelClass.SelectedNode.Text;
            //throw new NotImplementedException();
        }

        private void BAnnuler_Click(object sender, EventArgs e)
        {
            Close();
            //throw new NotImplementedException();
        }

        private void BSave_Click(object sender, EventArgs e)
        {
            string Cell1 = "", Cell3 = "";
            odgv.CurrentRow.Cells[1].Value = "";
            odgv.CurrentRow.Cells[3].Value = "";

            for (int i = 0; i < dgvLabel.RowCount; i++)
            {
                Cell1 += ";" + dgvLabel.Rows[i].Cells["NomLabel"].Value.ToString();
                Cell3 += ";" + dgvLabel.Rows[i].Cells["GuidLabel"].Value.ToString();
            }
            if (Cell3.Length > 0) odgv.CurrentRow.Cells[3].Value = Cell3.Substring(1);
            if (Cell1.Length > 0) odgv.CurrentRow.Cells[1].Value = Cell1.Substring(1);
            Close();
        }

        public void AddlDestinationFromProp()
        {
            string[] aNomValue = odgv.CurrentRow.Cells[1].Value.ToString().Split(';');
            string[] aGuidValue = odgv.CurrentRow.Cells[3].Value.ToString().Split(';');

            if(aNomValue.Length == aGuidValue.Length && aNomValue[0]!="")
            {
                for(int i=0; i<aGuidValue.Length; i++)
                {
                    int r = dgvLabel.RowCount;
                    dgvLabel.Rows.Add();
                    dgvLabel.Rows[r].Cells["GuidLabel"].Value = (object)aGuidValue[i];
                    dgvLabel.Rows[r].Cells["NomLabel"].Value = (object)aNomValue[i];
                }
            }
        }

        private void TvLabelClass_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(tvLabelClass.SelectedNode.Parent== null)
            {
                bAddLink.Enabled = false;
                bAdd.Enabled = true;
                bSup.Enabled = true;
            } else
            {
                bAddLink.Enabled = true;
                bAdd.Enabled = false;
                bSup.Enabled = false;
            }
            //throw new NotImplementedException();
        }

        private void BAdd_Click(object sender, EventArgs e)
        {
            string gLabelClass = "";
            string gEnv = "1ec95fbc-a2b7-4a55-a34c-e6f107789ea8";
            string gRole = "a3dbca08-6c23-4baf-82f7-4fe0c24547fa";
            string gLoc = "ba94399d-b106-43ef-aa48-2f024e5296d2";
            string gApp = "4f26a1bd-d840-4014-aa39-6912d7bcdb18";
            if (tvLabelClass.SelectedNode != null) {
                if (tvLabelClass.SelectedNode.Name == gApp) // Application
                {
                    gLabelClass = gApp;
                    FormLabelApp fle = new FormLabelApp(this);

                    fle.ShowDialog(this);
                    
                    if (fle.Valider)
                    {
                        // Il doit y avoir l'unicité des valeurs Label
                        string g = Guid.NewGuid().ToString();
                        string n = sLabel;
                        Parent.oCnxBase.CBWrite("insert into Label (GuidLabel, NomLabel, GuidLabelClass) values ('" + g + "','" + n + "','" + gLabelClass + "')");
                        tvLabelClass.SelectedNode.Nodes.Add(g, n);
                        sLabel = "";
                    }
                    
                } else if (tvLabelClass.SelectedNode.Name == gEnv) // Environnement
                {
                    gLabelClass = gEnv;
                    FormLabelEnv fle = new FormLabelEnv(this);

                    fle.ShowDialog(this);
                    if(fle.Valider)
                    {
                        // Il doit y avoir l'unicité des valeurs Label
                        string g = Guid.NewGuid().ToString();
                        string n = fle.sRetourLabel;
                        Parent.oCnxBase.CBWrite("insert into Label (GuidLabel, NomLabel, GuidLabelClass) values ('" + g + "','" + n + "','" + gLabelClass + "')");
                        tvLabelClass.SelectedNode.Nodes.Add(g , n);
                    }

                } else if (tvLabelClass.SelectedNode.Name == gLoc) // Location
                {
                    gLabelClass = gLoc;
                    FormLabelLocation fle = new FormLabelLocation(this);

                    fle.ShowDialog(this);
                    if (fle.Valider)
                    {
                        // Il doit y avoir l'unicité des valeurs Label
                        string g = Guid.NewGuid().ToString();
                        string n = sLabel;
                        Parent.oCnxBase.CBWrite("insert into Label (GuidLabel, NomLabel, GuidLabelClass) values ('" + g + "','" + n + "','" + gLabelClass + "')");
                        tvLabelClass.SelectedNode.Nodes.Add(g, n);
                        sLabel = "";
                    }
                } else if (tvLabelClass.SelectedNode.Name == gRole) // Role
                {
                    gLabelClass = gRole;
                    FormLabelRole fle = new FormLabelRole(this);

                    fle.ShowDialog(this);
                    if (fle.Valider)
                    {
                        // Il doit y avoir l'unicité des valeurs Label
                        string g = Guid.NewGuid().ToString();
                        string n = sLabel;
                        Parent.oCnxBase.CBWrite("insert into Label (GuidLabel, NomLabel, GuidLabelClass) values ('" + g + "','" + n + "','" + gLabelClass + "')");
                        tvLabelClass.SelectedNode.Nodes.Add(g, n);
                        sLabel = "";
                    }
                } else
                {
                    MessageBox.Show("fenetre par defaut");
                }
            }
            //throw new NotImplementedException();
        }

        public void AddElLabelClass( string[] el)
        {
            tvLabelClass.Nodes.Add(el[0], el[1]);
            TreeNode[] tn = tvLabelClass.Nodes.Find(el[0], true);

            if(tn.Length > 0)
            {
                if (Parent.oCnxBase.CBRecherche("Select GuidLabel, NomLabel from Label where GuidLabelClass = '" + tn[0].Name + "' order by NomLabel"))
                {
                    while (Parent.oCnxBase.Reader.Read())
                    {
                        tn[0].Nodes.Add(Parent.oCnxBase.Reader.GetString(0), Parent.oCnxBase.Reader.GetString(1));
                    }
                }
                Parent.oCnxBase.CBReaderClose();
            }
        }

        public void AddtvLabelClassFromDB()
        {
            List<string[]> lstLabelClass = new List<string[]>();

            if (Parent.oCnxBase.CBRecherche("Select GuidLabelClass, NomLabelClass from LabelClass order by NomLabelClass"))
            {
                while (Parent.oCnxBase.Reader.Read())
                {
                    string[] el = new string[2];
                    el[0] = Parent.oCnxBase.Reader.GetString(0);
                    el[1] = Parent.oCnxBase.Reader.GetString(1);
                    lstLabelClass.Add(el);
                    //tvLabelClass.Nodes.Add(Parent.oCnxBase.Reader.GetString(0), Parent.oCnxBase.Reader.GetString(1));
                }
            }
            Parent.oCnxBase.CBReaderClose();

            for (int i = 0; i < lstLabelClass.Count; i++) AddElLabelClass(lstLabelClass[i]);
        }
    }
}
