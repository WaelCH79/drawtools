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
    public partial class FormFlowRule : Form
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

        public void InitEvent()
        {
            this.bAjouter.Click += BAjouter_Click;
            this.bModifier.Click += BModifier_Click;
            this.bSuprimer.Click += BSuprimer_Click;
            this.bFermer.Click += BFermer_Click;
            this.cbTypeSrc.SelectedIndexChanged += CbTypeSrc_SelectedIndexChanged;
            this.cbTypeCbl.SelectedIndexChanged += CbTypeCbl_SelectedIndexChanged;
            this.dgFlowRule.SelectionChanged += DgFlowRule_SelectionChanged;
        }

        private void DgFlowRule_SelectionChanged(object sender, EventArgs e)
        {
            if (dgFlowRule.SelectedRows.Count == 1)
            {
                foreach (DataGridViewCell el in dgFlowRule.SelectedRows[0].Cells)
                {
                    switch (el.ColumnIndex)
                    {
                        case 0: // TypeSrc
                            cbTypeSrc.SelectedIndex = cbTypeSrc.FindStringExact((string)el.Value);
                            //if (cbTypeSrc.SelectedItem != el.Value)
                            //    if (cbTypeSrc.SelectedIndex == 0) cbTypeSrc.SelectedIndex = 1; else cbTypeSrc.SelectedIndex = 0;
                            break;
                        case 1: // GuidSrc
                            break;
                        case 2: // NomSrc
                            cbNomSrc.SelectedIndex = cbNomSrc.FindStringExact((string)el.Value);
                            break;
                        case 3: // TypeCbl
                            cbTypeCbl.SelectedIndex = cbTypeCbl.FindStringExact((string)el.Value);
                            //if (cbTypeCbl.SelectedItem != el.Value)
                            //    if (cbTypeCbl.SelectedIndex == 0) cbTypeCbl.SelectedIndex = 1; else cbTypeCbl.SelectedIndex = 0;
                            break;
                        case 4: // GuidCbl
                            break;
                        case 5: // NomCbl
                            cbNomCbl.SelectedIndex = cbNomCbl.FindStringExact((string)el.Value);
                            break;
                        case 6: // GuidSrv
                            break;
                        case 7: // NomSrv
                            cbNomSrv.SelectedIndex = cbNomSrv.FindStringExact((string)el.Value);
                            break;
                    }

                }
            }
        }

        private void BFermer_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool DeleteMatriceFluxRow()
        {
            if (dgFlowRule.SelectedRows.Count == 1)
            {
                string GuidSrc = (string)dgFlowRule.SelectedRows[0].Cells[1].Value;
                string GuidCbl = (string)dgFlowRule.SelectedRows[0].Cells[4].Value;
                string GuidSrv = (string)dgFlowRule.SelectedRows[0].Cells[6].Value;
                dgFlowRule.Rows.RemoveAt(dgFlowRule.SelectedRows[0].Index);
                Parent.oCnxBase.CBWrite("Delete From MatriceFlux Where GuidSource='" + GuidSrc + "' and GuidCible='" + GuidCbl + "' and GuidGroupService='" + GuidSrv + "'");
            }
            else return false;
            return true;
        }

        private void BSuprimer_Click(object sender, EventArgs e)
        {
            DeleteMatriceFluxRow();
        }

        private void BModifier_Click(object sender, EventArgs e)
        {
            WriteMatriceFluxRow(true);
        }
        private void CbTypeCbl_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbGuidCbl.Items.Clear(); cbNomCbl.Items.Clear();
            
            if ((string)cbTypeCbl.SelectedItem == "Classe")
                Parent.oCnxBase.CBAddComboBox("SELECT GuidVlanClass, NomVlanClass FROM VlanClass ORDER BY NomVlanClass", cbGuidCbl, cbNomCbl);
            else if ((string)cbTypeCbl.SelectedItem == "Groupe")
                Parent.oCnxBase.CBAddComboBox("SELECT GuidNCard, IPAddr FROM NCard Where IPAddr like 'G%' ORDER BY IPAddr", cbGuidCbl, cbNomCbl);
            else
                Parent.oCnxBase.CBAddComboBox("SELECT GuidLabel, NomLabel FROM Label Where GuidLabelClass = 'a3dbca08-6c23-4baf-82f7-4fe0c24547fa' ORDER BY NomLabel", cbGuidCbl, cbNomCbl);
        }

        private void CbTypeSrc_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbGuidSrc.Items.Clear(); cbNomSrc.Items.Clear();
            if ((string)cbTypeSrc.SelectedItem == "Classe")
                Parent.oCnxBase.CBAddComboBox("SELECT GuidVlanClass, NomVlanClass FROM VlanClass ORDER BY NomVlanClass", cbGuidSrc, cbNomSrc);
            else if ((string)cbTypeSrc.SelectedItem == "Groupe")
                Parent.oCnxBase.CBAddComboBox("SELECT GuidNCard, IPAddr FROM NCard Where IPAddr like 'G%' ORDER BY IPAddr", cbGuidSrc, cbNomSrc);
            else
                Parent.oCnxBase.CBAddComboBox("SELECT GuidLabel, NomLabel FROM Label Where GuidLabelClass = 'a3dbca08-6c23-4baf-82f7-4fe0c24547fa' ORDER BY NomLabel", cbGuidSrc, cbNomSrc);
        }

        private string getTypeObj(string sType)
        {
            switch(sType)
            {
                case "VlanClass":
                    return "Classe";
                case "NCard":
                    return "Groupe";
                case "Role":
                    return "Role";
            }
            return "";
        }

        private string getTable(string sObj)
        {
            switch (sObj)
            {
                case "Classe":
                    return "VlanClass";
                case "Groupe":
                    return "NCard";
                case "Role":
                    return "Role";
            }
            return "";
        }

        public FormFlowRule(Form1 p)
        {
            Parent = p;
          
            InitializeComponent();
            cbTypeSrc.SelectedIndex = 0;
            Parent.oCnxBase.CBAddComboBox("SELECT GuidVlanClass, NomVlanClass FROM VlanClass ORDER BY NomVlanClass", cbGuidSrc, cbNomSrc);
            cbTypeCbl.SelectedIndex = 0;
            Parent.oCnxBase.CBAddComboBox("SELECT GuidVlanClass, NomVlanClass FROM VlanClass ORDER BY NomVlanClass", cbGuidCbl, cbNomCbl);
            Parent.oCnxBase.CBAddComboBox("SELECT GuidGroupService, NomGroupService FROM GroupService ORDER BY NomGroupService", cbGuidSrv, cbNomSrv);

            if (Parent.oCnxBase.CBRecherche("Select GuidSource, GuidCible, GroupService.GuidGroupService, NomGroupService, TypeSource, TypeCible From MatriceFlux, GroupService  Where MatriceFlux.GuidGroupService = GroupService.GuidGroupService Order by TypeSource, TypeCible"))
            {
                while (Parent.oCnxBase.Reader.Read())
                {
                    dgFlowRule.Rows.Add();
                    DataGridViewRow row = dgFlowRule.Rows[dgFlowRule.Rows.Count - 1];
                    row.Cells["tbGuidSrc"].Value = Parent.oCnxBase.Reader.GetString(0);
                    row.Cells["tbTypeSrc"].Value = getTypeObj(Parent.oCnxBase.Reader.GetString(4));
                    row.Cells["tbGuidCbl"].Value = Parent.oCnxBase.Reader.GetString(1);
                    row.Cells["tbTypeCbl"].Value = getTypeObj(Parent.oCnxBase.Reader.GetString(5));
                    row.Cells["tbGuidSrv"].Value = Parent.oCnxBase.Reader.GetString(2);
                    row.Cells["tbNomSrv"].Value = Parent.oCnxBase.Reader.GetString(3);
                }
            }
            Parent.oCnxBase.CBReaderClose();

            for (int i = 0; i < dgFlowRule.RowCount; i++)
            {
                string sRequete = "";
                DataGridViewRow row = dgFlowRule.Rows[i];
                switch (row.Cells["tbTypeSrc"].Value)
                {
                    case "Classe":
                        sRequete = "Select NomVlanClass From VlanClass Where GuidVlanClass= '" + row.Cells["tbGuidSrc"].Value + "' ";
                        break;
                    case "Groupe":
                        sRequete = "Select IPAddr From NCard Where GuidNCard= '" + row.Cells["tbGuidSrc"].Value + "' ";
                        break;
                    case "Role":
                        sRequete = "Select NomLabel From Label Where GuidLabel= '" + row.Cells["tbGuidSrc"].Value + "' ";
                        break;
                }
                if (Parent.oCnxBase.CBRecherche(sRequete)) row.Cells["tbNomSrc"].Value = Parent.oCnxBase.Reader.GetString(0);
                Parent.oCnxBase.CBReaderClose();
                switch (row.Cells["tbTypeCbl"].Value)
                {
                    case "Classe":
                        sRequete = "Select NomVlanClass From VlanClass Where GuidVlanClass= '" + row.Cells["tbGuidCbl"].Value + "' ";
                        break;
                    case "Groupe":
                        sRequete = "Select NomNCard From NCard Where GuidNCard= '" + row.Cells["tbGuidCbl"].Value + "' ";
                        break;
                    case "Role":
                        sRequete = "Select NomLabel From Label Where GuidLabel= '" + row.Cells["tbGuidCbl"].Value + "' ";
                        break;
                }
                if (Parent.oCnxBase.CBRecherche(sRequete)) row.Cells["tbNomCbl"].Value = Parent.oCnxBase.Reader.GetString(0);
                Parent.oCnxBase.CBReaderClose();
            }
            /*

            string sql1 = "Select GuidSource, Source.NomVlanClass NomSource, TypeSource, GuidCible, Cible.NomVlanClass NomCible, TypeCible, GroupService.GuidGroupService, NomGroupService From MatriceFlux, VlanClass Source, VlanClass Cible, GroupService Where GuidSource = Source.GuidVlanClass and GuidCible = Cible.GuidVlanClass and MatriceFlux.GuidGroupService = GroupService.GuidGroupService";
            string sql2 = "Select GuidSource, Concat(Source.IPAddr, ' - ', Source.NomNCard) NomSource, TypeSource, GuidCible, Cible.NomVlanClass NomCible, TypeCible, GroupService.GuidGroupService, NomGroupService From MatriceFlux, NCard Source, VlanClass Cible, GroupService Where GuidSource = Source.GuidNCard and GuidCible = Cible.GuidVlanClass and MatriceFlux.GuidGroupService = GroupService.GuidGroupService";
            string sql3 = "Select GuidSource, Source.NomVlanClass NomSource, TypeSource, GuidCible, Concat(Cible.IPAddr, ' - ', Cible.NomNCard) NomCible, TypeCible, GroupService.GuidGroupService, NomGroupService From MatriceFlux, VlanClass Source, NCard Cible, GroupService Where GuidSource = Source.GuidVlanClass and GuidCible = Cible.GuidNCard and MatriceFlux.GuidGroupService = GroupService.GuidGroupService";
            string sql4 = "Select GuidSource, Concat(Source.IPAddr, ' - ', Source.NomNCard) NomSource, TypeSource, GuidCible, Concat(Cible.IPAddr, ' - ', Cible.NomNCard) NomCible, TypeCible, GroupService.GuidGroupService, NomGroupService From MatriceFlux, NCard Source, NCard Cible, GroupService Where GuidSource = Source.GuidNCard and GuidCible = Cible.GuidNCard and MatriceFlux.GuidGroupService = GroupService.GuidGroupService";

            if (Parent.oCnxBase.CBRecherche(sql1 + " union " + sql2 + " union " + sql3 + " union " + sql4 + " Order by NomSource, NomCible, NomGroupService"))
            {
                while (Parent.oCnxBase.Reader.Read())
                {
                    dgFlowRule.Rows.Add();

                    DataGridViewRow row = dgFlowRule.Rows[dgFlowRule.Rows.Count - 1];
                    if (Parent.oCnxBase.Reader.GetString(2) == "NCard") row.Cells["tbTypeSrc"].Value = "Groupe"; else row.Cells["tbTypeSrc"].Value = "Classe";
                    row.Cells["tbGuidSrc"].Value = Parent.oCnxBase.Reader.GetString(0);
                    row.Cells["tbNomSrc"].Value = Parent.oCnxBase.Reader.GetString(1);
                    if (Parent.oCnxBase.Reader.GetString(5) == "NCard") row.Cells["tbTypeCbl"].Value = "Groupe"; else row.Cells["tbTypeCbl"].Value = "Classe";
                    row.Cells["tbGuidCbl"].Value = Parent.oCnxBase.Reader.GetString(3); 
                    row.Cells["tbNomCbl"].Value = Parent.oCnxBase.Reader.GetString(4);
                    row.Cells["tbGuidSrv"].Value = Parent.oCnxBase.Reader.GetString(6);
                    row.Cells["tbNomSrv"].Value = Parent.oCnxBase.Reader.GetString(7);
                }
            }
            Parent.oCnxBase.CBReaderClose();
            */
            InitEvent();
            //DataGridViewComboBoxColumn cbSeaaaa = (DataGridViewComboBoxColumn)dgFlowRule.Columns[9];
        }

        private void WriteMatriceFluxRow(bool bModif=false)
        {
            if (cbNomSrc.SelectedIndex != -1 && cbNomCbl.SelectedIndex != -1 && cbNomSrv.SelectedIndex != -1)
            {
                if (!Parent.oCnxBase.CBRecherche("Select GuidSource From MatriceFlux Where GuidSource='" + cbGuidSrc.Items[cbNomSrc.SelectedIndex] + "' and GuidCible = '" + cbGuidCbl.Items[cbNomCbl.SelectedIndex] + "' and GuidGroupService = '" + cbGuidSrv.Items[cbNomSrv.SelectedIndex] + "'"))
                {
                    Parent.oCnxBase.CBReaderClose();
                    string sTableSrc = "VlanClass", sTableCbl = "VlanClass";
                    dgFlowRule.Rows.Add();

                    DataGridViewRow row = dgFlowRule.Rows[dgFlowRule.Rows.Count - 1];
                    row.Cells["tbTypeSrc"].Value = cbTypeSrc.SelectedItem;
                    row.Cells["tbGuidSrc"].Value = cbGuidSrc.Items[cbNomSrc.SelectedIndex];
                    row.Cells["tbNomSrc"].Value = cbNomSrc.SelectedItem;
                    row.Cells["tbTypeCbl"].Value = cbTypeCbl.SelectedItem;
                    row.Cells["tbGuidCbl"].Value = cbGuidCbl.Items[cbNomCbl.SelectedIndex];
                    row.Cells["tbNomCbl"].Value = cbNomCbl.SelectedItem;
                    row.Cells["tbGuidSrv"].Value = cbGuidSrv.Items[cbNomSrv.SelectedIndex];
                    row.Cells["tbNomSrv"].Value = cbNomSrv.SelectedItem;
                    sTableSrc = getTable((string)row.Cells["tbTypeSrc"].Value);
                    sTableCbl = getTable((string)row.Cells["tbTypeCbl"].Value);

                    if(bModif)
                    {
                        if(DeleteMatriceFluxRow()) Parent.oCnxBase.CBWrite("Insert Into MatriceFlux (GuidSource, GuidCible, GuidGroupService, TypeSource, TypeCible) Values ('" + row.Cells["tbGuidSrc"].Value + "','" + row.Cells["tbGuidCbl"].Value + "','" + row.Cells["tbGuidSrv"].Value + "','" + sTableSrc + "','" + sTableCbl + "')");
                    }
                    else Parent.oCnxBase.CBWrite("Insert Into MatriceFlux (GuidSource, GuidCible, GuidGroupService, TypeSource, TypeCible) Values ('" + row.Cells["tbGuidSrc"].Value + "','" + row.Cells["tbGuidCbl"].Value + "','" + row.Cells["tbGuidSrv"].Value + "','" + sTableSrc + "','" + sTableCbl + "')");
                }
                else
                {
                    Parent.oCnxBase.CBReaderClose();
                    MessageBox.Show("La règle existe déjà.");
                }

            }
            else MessageBox.Show("La règle n'est pas entièrement saisie.");
        }

        private void BAjouter_Click(object sender, EventArgs e)
        {
            WriteMatriceFluxRow();
            
        }
    }

}
