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
    public partial class FormIndicator : Form
    {
        private Form1 parent;
        private string sGuid;

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

        public FormIndicator(Form1 p, string s)
        {
            Parent = p;
            sGuid = s;
            InitializeComponent();

            Parent.oCnxBase.CBRecherche("SELECT GuidIndicator, NomIndicator, Type FROM Indicator");
            while (Parent.oCnxBase.Reader.Read())
            {
                string[] row = new string[3];
                row[0] = parent.oCnxBase.Reader.GetString(0);
                row[1] = parent.oCnxBase.Reader.GetString(1);
                row[2] = parent.oCnxBase.Reader.GetString(2);
                dgIndicator.Rows.Add(row);
            }
            Parent.oCnxBase.CBReaderClose();
            CompleteData();

        }

        private int GetIndexRow(int iCell, string sVal)
        {
            for (int i = 0; i < dgIndicator.RowCount; i++)
                if (dgIndicator.Rows[i].Cells[iCell].Value.ToString() == sVal) return i;
            return -1;
        }

        private void CompleteData()
        {
            Parent.oCnxBase.CBRecherche("Select GuidIndicator, ValIndicator FROM IndicatorLink WHERE GuidObjet='" + sGuid + "'");
            while (Parent.oCnxBase.Reader.Read())
            {
                int n = GetIndexRow(0,Parent.oCnxBase.Reader.GetString(0));
                if (n != -1)
                {
                    double d = Parent.oCnxBase.Reader.GetDouble(1);
                    switch (dgIndicator.Rows[n].Cells[2].Value.ToString()[0]) // Type
                    {
                        case 'i':
                        case 'd':
                            dgIndicator.Rows[n].Cells[3].Value = d.ToString();
                            break;
                        case 't':
                            //dgIndicator.Rows[n].Cells[3].Value = Parent.oCnxBase.Reader.GetDate(1).ToString();
                            dgIndicator.Rows[n].Cells[3].Value = DateTime.FromOADate(d).ToShortDateString();
                            break;
                    }
                }
            }
            Parent.oCnxBase.CBReaderClose();
        }

        private bool ifDouble(string sDouble)
        {
            try
            {
                Convert.ToDouble(sDouble);
            }
            catch (Exception e) 
            {
                return false;
            }

            return true;
        }

        private bool ifDate(string sDate)
        {
            try
            {
                Convert.ToDateTime(sDate);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            Parent.oCnxBase.CBWrite("DELETE FROM IndicatorLink WHERE GuidObjet='" + sGuid + "'");
            for (int i = 0; i < dgIndicator.RowCount; i++)
            {
                if (dgIndicator.Rows[i].Cells[3].Value != null)
                {
                    double d = 0;
                    switch (dgIndicator.Rows[i].Cells[2].Value.ToString()[0])
                    {
                        case 'i':
                        case 'd':
                            d = Convert.ToDouble(dgIndicator.Rows[i].Cells[3].Value.ToString());
                            break;
                        case 't':
                            d = Convert.ToDateTime(dgIndicator.Rows[i].Cells[3].Value.ToString()).ToOADate();
                            break;
                    }
                    Parent.oCnxBase.CBWrite("INSERT INTO IndicatorLink (GuidObjet, GuidIndicator, ValIndicator) VALUES ('" + sGuid + "','" + dgIndicator.Rows[i].Cells[0].Value + "'," + d + ")");
                }
            }
            this.Close();
        }

        /*
        private void button1_Click(object sender, EventArgs e)
        {
            ArrayList lstGuid = new ArrayList();
            ArrayList lstDate = new ArrayList();
            Parent.oCnxBase.CBRecherche("SELECT GuidTechnoRef, DateFinMain FROM TechnoRef");
            while (Parent.oCnxBase.Reader.Read())
            {
                lstGuid.Add(Parent.oCnxBase.Reader.GetString(0));
                lstDate.Add((double) ((DateTime)Parent.oCnxBase.Reader.GetDate(1)).ToOADate());
            }
            Parent.oCnxBase.CBReaderClose();
            for(int i=0;i<lstGuid.Count;i++)
            {
                Parent.oCnxBase.CBWrite("INSERT INTO IndicatorLink (GuidObjet, GuidIndicator, ValIndicator) VALUES ('" + lstGuid[i] + "','b00b12bd-a447-47e6-92f6-e3b76ad22830'," + (double)lstDate[i] + ")");
            }
        }*/
    }
}
