
#define _APIREADY

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DrawTools
{
    public partial class FormPropWord : Form
    {
        private Form1 parent;
        private DrawObject o;
        private ArrayList lstRawData;

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

        public void InitProp(string sPropriete, string cPolicy)
        {
            int ideb = dgProp.Rows.Count;
            ArrayList lstPropName = o.GetName(sPropriete);

            for (int i = 0; i < lstPropName.Count; i++)
            {
                string[] row = new string[3];
                row[0] = o.GetLibFromName((string)lstPropName[i]);
                row[1] = "";
                row[2] = cPolicy;
                byte[] rData = new byte[0]; lstRawData.Add(rData);

#if APIREADY
                Newtonsoft.Json.Linq.JArray aComments = (Newtonsoft.Json.Linq.JArray)o.dicObj["comments"];
                for (int j = 0; j < aComments.Count; j++)
                {
                    Newtonsoft.Json.Linq.JObject jo = (Newtonsoft.Json.Linq.JObject)aComments[j];
                    
                    if (jo.GetValue("nomprop").ToString() == o.GetLibFromName((string)lstPropName[i]))
                    {
                        Dictionary<string, object> values = Newtonsoft.Json.Linq.JObject.FromObject(jo).ToObject<Dictionary<string, object>>();
                        object oData;
                        values.TryGetValue("richtext", out oData);
                        byte[] rawData = new byte[oData.ToString().Length];
                        int idx = 0;
                        foreach (byte b in oData.ToString()) rawData[idx++] = b;
                        lstRawData[i + ideb] = rawData;
                        break;
                    }

                }
#else

                if (Parent.oCnxBase.CBRecherche("Select HyperLien, Size, RichText From Comment Where GuidObject = '" + o.GetKeyComment() + "' AND NomProp='" + o.GetLibFromName((string)lstPropName[i]) + "'"))
                {
                    if (!Parent.oCnxBase.Reader.IsDBNull(0)) row[1] = Parent.oCnxBase.Reader.GetString(0);
                    int nByte = Parent.oCnxBase.Reader.GetInt32(1);
                    if (!Parent.oCnxBase.Reader.IsDBNull(2))
                    {
                        byte[] rawData = new byte[nByte];
                        Parent.oCnxBase.Reader.GetBytes(2, 0, rawData, 0, nByte);
                        lstRawData[i+ideb] = rawData;
                    }
                }
                parent.oCnxBase.CBReaderClose();
#endif
                dgProp.Rows.Add(row);

            }
        }

        public FormPropWord(Form1 p, DrawObject obj)
        {
            Parent = p;
            o = obj;
            lstRawData = new ArrayList();

            InitializeComponent();

            InitProp("Prop", "P");
            InitProp("InLine", "L");
            InitProp("Tab", "T"); 
        }


        void dgProp_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1) //lien hypertexte
            {
                //MessageBox.Show((string)dgProp.CurrentCell.Value);
                //System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
                //string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                //myProcess.StartInfo.FileName = "c:\\dat\\test.txt"; //(string)dgProp.CurrentCell.Value; 
				//myProcess.StartInfo.Verb = "Set Property";
				//myProcess.StartInfo.CreateNoWindow = true;
                //myProcess.Start();
                if((string)dgProp.CurrentCell.Value!="") System.Diagnostics.Process.Start((string)dgProp.CurrentCell.Value);
            }
            else if (e.ColumnIndex == 3) // Bouton Set(...)
            {
                if (fileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //tbRootPath.Text = folderBrowserDialog1.SelectedPath;
                    dgProp.CurrentRow.Cells[1].Value = fileDialog1.FileName;
                }
            }
            else if (e.ColumnIndex == 4) // Bouton Sup(x)
            {
                dgProp.CurrentRow.Cells[1].Value = "";
            }
            else if (e.ColumnIndex == 5) // Bouton RichText(T)
            {

                FormRichText frt = new FormRichText(this, (byte[]) lstRawData[dgProp.CurrentRow.Index]);
                frt.init();
            }
        }

        public void setRawDataInCurrentRow(byte[] rawData) {
            lstRawData[dgProp.CurrentRow.Index] = rawData;
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            Parent.oCnxBase.CBWrite("DELETE FROM Comment Where GuidObject = '" + o.GetKeyComment() + "'");
            for (int i = 0; i < dgProp.RowCount; i++)
            {
                if ((string)dgProp.Rows[i].Cells[1].Value != "")
                {

                    if (lstRawData[i] != null)
                    {
                        int iSize = ((byte[])lstRawData[i]).Length;
                        Parent.oCnxBase.CBWriteWithObj("INSERT INTO Comment (GuidObject, NomProp, HyperLien, Size, RichText, Policy) VALUES ('" + o.GetKeyComment() + "','" + dgProp.Rows[i].Cells[0].Value + "','" + dgProp.Rows[i].Cells[1].Value + "'," + iSize + ", ?, '" + dgProp.Rows[i].Cells[2].Value + "')", (byte[])lstRawData[i]);
                    }
                    else
                        Parent.oCnxBase.CBWrite("INSERT INTO Comment (GuidObject, NomProp, HyperLien) VALUES ('" + o.GetKeyComment() + "','" + dgProp.Rows[i].Cells[0].Value + "','" + dgProp.Rows[i].Cells[1].Value + "')");
                }
                else
                {
                    if (lstRawData[i] != null)
                    {
                        int iSize = ((byte[])lstRawData[i]).Length;
                        Parent.oCnxBase.CBWriteWithObj("INSERT INTO Comment (GuidObject, NomProp, Size, RichText, Policy) VALUES ('" + o.GetKeyComment() + "','" + dgProp.Rows[i].Cells[0].Value + "'," + iSize + ", ?, '" + dgProp.Rows[i].Cells[2].Value + "')", (byte[])lstRawData[i]);
                    }
                }
            }
            
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
