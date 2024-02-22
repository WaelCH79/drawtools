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
    public partial class FormddmSubnet : Form
    {
        private Form1 parent;

        public new Form1 Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public FormddmSubnet(Form1 p)
        {
            Parent = p;
            InitializeComponent();
            InitEvent();
            loadTab();
        }

        public void InitEvent()
        {
        }

        public void loadTab()
        {
            if (Parent.oCnxBase.CBRecherche("Select GuidddmSubnet, NomddmSubnet, VLan.GuidVlan, NomVlan, UpdateDisco From ddmSubnet Left Join VLan On ddmSubnet.GuidVlan = VLan.GuidVLan Order by NomddmSubnet"))
            {
                int icol = Parent.oCnxBase.Reader.FieldCount;
                while (Parent.oCnxBase.Reader.Read())
                {
                    dgSubnet.Rows.Add();
                    DataGridViewRow row = dgSubnet.Rows[dgSubnet.Rows.Count - 1];
                    for (int i = 0; i < icol; i++)
                    {
                        string sCol = Parent.oCnxBase.Reader.GetName(i);
                        switch(Parent.oCnxBase.Reader.GetFieldType(i).Name)
                        {
                            case "String":
                                row.Cells["tb" + sCol].Value = Parent.oCnxBase.Reader.IsDBNull(i) ? "" : Parent.oCnxBase.Reader.GetString(i);
                                break;
                            case "Int32":
                                row.Cells["tb" + sCol].Value = Parent.oCnxBase.Reader.IsDBNull(i) ? "" : Parent.oCnxBase.Reader.GetInt32(i).ToString();
                                break;
                            case "Double":
                                row.Cells["tb" + sCol].Value = Parent.oCnxBase.Reader.IsDBNull(i) ? "" : Parent.oCnxBase.Reader.GetDouble(i).ToString();
                                break;
                            case "DateTime":
                                row.Cells["tb" + sCol].Value = Parent.oCnxBase.Reader.IsDBNull(i) ? "" : Parent.oCnxBase.Reader.GetDate(i).ToShortDateString();
                                break;
                        }
                        
                    }
                }
            }
            Parent.oCnxBase.CBReaderClose();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
