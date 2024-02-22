using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Word;
 
using System.Windows.Forms;
using System.Collections;
using System.Data.Odbc;

namespace DrawTools
{
    public class Enreg
    {
        public Form1 F;
        public ArrayList LstValue;
        public DataGridView dg;
        public string sTable;

        public Enreg()
        {
        }

        public virtual void LstValueClear()
        {
            LstValue.Clear();
            InitProp();
        }

        public virtual string GetWhere(Table t, string sForeinValue)
        {
            string whereO = t.GetFields(ConfDataBase.FieldOption.ForeignKey);
            if (whereO == "") return "";
            return "WHERE " + whereO + "='" + sForeinValue + "'";
        }

        public virtual void SaveEnreg(int row, bool Force)
        {
            int n;
            LstValueClear();
            if (Force || dg.IsCurrentRowDirty)
            {
                n = F.oCnxBase.ConfDB.FindTable(sTable);
                if (n > -1)
                {
                    Table t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                    LstValue = t.InitValue();
                    dg.ClearSelection();
                    n = dg.Rows.Count - 2;
                    dg.Rows[n].Selected = true;
                    DataGridViewCellCollection dgvcc = dg.Rows[row].Cells;


                    for (int i = 0; i < dg.ColumnCount; i++)
                    {
                        int j = t.FindField(t.LstField, dg.Columns[i].HeaderCell.ToolTipText);
                        // if(j <= -1) j = t.FindFieldFromLib(dg.Columns[i].HeaderCell.ToolTipText);
                        if (j > -1)
                        {
                            if (dg.Columns[i].GetType() == typeof(DataGridViewTextBoxColumn)) LstValue[j] = t.initProp(LstValue[j], j, dgvcc[i].Value, true);
                            else if (dg.Columns[i].GetType() == typeof(DataGridViewComboBoxColumn))
                            {
                                cObj o = (cObj)dgvcc[i].Value;
                                if (o != null) LstValue[j] = t.initProp(LstValue[j], j, o.Guid, true);
                            }
                            
                        }
                    }
                    if (!F.oCnxBase.ExistGuid(this)) F.oCnxBase.CreatObject(this);
                    else F.oCnxBase.UpdateObject(this);
                }
            }
        }

        public virtual void RowsAdded(List<String[]> lstForeinKey)
        {
            Table t;
            ConfDataBase ConfDB = F.oCnxBase.ConfDB;
            int n = ConfDB.FindTable(sTable);

            if (n > -1)
            {
                t = (Table)ConfDB.LstTable[n];
                if (dg.ColumnCount == LstValue.Count)
                {
                    for (int i = 0; i < dg.ColumnCount; i++)
                    {
                        Field fl = (Field)t.LstField[i];
                        if ((fl.fieldOption & ConfDataBase.FieldOption.Key) != 0)
                        {
                            if (dg.CurrentRow.Cells[i].Value == null) dg.CurrentRow.Cells[i].Value = (object)Guid.NewGuid().ToString();
                        }
                        else if ((fl.fieldOption & ConfDataBase.FieldOption.ForeignKey) != 0)
                        {
                            string[] elk = lstForeinKey.Find(el => el[0] == fl.Name);
                            if (elk != null) dg.CurrentRow.Cells[fl.Name].Value = elk[1];
                        }
                    }
                }
            }
        }

        public int GetIndexcObjFromList(ArrayList lst, string sGuid)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                cObj o = (cObj)lst[i];
                if (sGuid == o.Guid) return i;
            }
            return -1;
        }

        public virtual void LoadEnreg(string sForeinValue)
        {
            string Select, From, Where;
            CnxBase ocnx = F.oCnxBase;

            Table t;
            ConfDataBase ConfDB = F.oCnxBase.ConfDB;
            int n = ConfDB.FindTable(sTable);
            if (n > -1)
            {
                t = (Table)ConfDB.LstTable[n];
                //Select = "SELECT " + t.GetSelectField(ConfDataBase.FieldOption.InterneBD);
                Select = "SELECT " + t.GetSelectField(ConfDataBase.FieldOption.Base);
                From = "FROM " + sTable;
                
                Where = "Where " + t.GetDefaultForeignKey() + "='" + sForeinValue + "'";
                if (ocnx.CBRecherche(Select + " " + From + " " + Where))
                {
                    while (ocnx.Reader.Read())
                    {
                        ArrayList lstValue = new ArrayList();
                        lstValue = t.InitValueFieldFromBD(ocnx.Reader, ConfDataBase.FieldOption.Select);

                        //string[] row = { aEnreg[1], NomSource.Substring(1), IPSource.Substring(1), LocationSource.Substring(1), NomCible.Substring(1), IPCible.Substring(1), LocationCible.Substring(1), aEnreg[2], aEnreg[3], aEnreg[4] };
                        dg.Rows.Add();
                        for (int i = 0; i < dg.ColumnCount; i++)
                        {
                            int j = t.FindField(t.LstField, dg.Columns[i].HeaderCell.ToolTipText);
                            //if(j <= -1) j = t.FindFieldFromLib(dg.Columns[i].HeaderCell.ToolTipText);
                            if (j > -1)
                            {
                                if (dg.Columns[i].GetType() == typeof(DataGridViewTextBoxColumn)) dg.Rows[dg.RowCount - 2].Cells[i].Value = lstValue[j].ToString();
                                else if (dg.Columns[i].GetType() == typeof(DataGridViewComboBoxColumn))
                                {
                                    if ((string)lstValue[j] != "")
                                    {
                                        ArrayList lstTechnoArea = (ArrayList)((DataGridViewComboBoxColumn)dg.Columns[i]).DataSource;
                                        int idx = GetIndexcObjFromList(lstTechnoArea, ((string)lstValue[j]).Trim());
                                        if (idx > -1) dg.Rows[dg.RowCount - 2].Cells[i].Value = lstTechnoArea[idx];
                                    }
                                }
                            }
                        }
                        
                        /*
                        string[] row = new string[dg.ColumnCount];

                        for (int i = 0; i < dg.ColumnCount; i++)
                        {
                            //dg.Columns[i].
                            int j = t.FindField(dg.Columns[i].HeaderCell.ToolTipText);
                            //"DataGridViewTextBoxColumn"
                            //"DataGridViewComboBoxColumn"



                            if (j > -1) row[i] = lstValue[j].ToString();
                            
                        }
                        dg.Rows.Add(row);*/
                    }
                    ocnx.CBReaderClose();
                }
                else ocnx.CBReaderClose();
            }
        }

        public virtual void LoadEnreg1(string sForeinValue)
        {
            string Select, From, Where;
            CnxBase ocnx = F.oCnxBase;

            Table t;
            ConfDataBase ConfDB = F.oCnxBase.ConfDB;
            int n = ConfDB.FindTable(sTable);
            if (n > -1)
            {
                t = (Table)ConfDB.LstTable[n];
                //Select = "SELECT " + t.GetSelectField(ConfDataBase.FieldOption.InterneBD);
                Select = "SELECT " + t.GetSelectField(ConfDataBase.FieldOption.Base);
                From = "FROM " + sTable;
                if(sForeinValue!="") Where = GetWhere(t, sForeinValue); else Where ="";
                if (ocnx.CBRecherche(Select + " " + From + " " + Where))
                {
                    while (ocnx.Reader.Read())
                    {
                        //string[] row = { aEnreg[1], NomSource.Substring(1), IPSource.Substring(1), LocationSource.Substring(1), NomCible.Substring(1), IPCible.Substring(1), LocationCible.Substring(1), aEnreg[2], aEnreg[3], aEnreg[4] };
                        string[] row = new string[t.LstField.Count];
                        for (int j=0,i = 0; j < t.LstField.Count; j++)
                        {
                            if ((((Field)t.LstField[j]).fieldOption & ConfDataBase.FieldOption.Select) != 0 && (((Field)t.LstField[j]).fieldOption & ConfDataBase.FieldOption.InterneBD) != 0)
                            {
                                switch (((Field)t.LstField[j]).Type)
                                {
                                    case 's':
                                        if (F.oCnxBase.Reader.IsDBNull(i)) row[i] = "";
                                        else row[i] = F.oCnxBase.Reader.GetString(i);
                                        break;
                                    case 'p': //picture
                                    case 'q': //picture
                                    case 'i':
                                        row[i] = F.oCnxBase.Reader.GetInt32(i).ToString();
                                        break;
                                    case 'd':
                                        row[i] = F.oCnxBase.Reader.GetInt64(i).ToString();
                                        break;
                                    case 't':
                                        if(!F.oCnxBase.Reader.IsDBNull(i)) row[i] = F.oCnxBase.Reader.GetDate(i).ToShortDateString();
                                        else row[i]="";
                                        if (row[i][0] == '0') row[i] = "";
                                        break;
                                }
                                i++;
                            }
                        }
                        dg.Rows.Add(row);
                    }
                    ocnx.CBReaderClose();
                }
                else ocnx.CBReaderClose();
            }
        }

        public virtual void InitProp()
        {

            Table t;
            int n = F.oCnxBase.ConfDB.FindTable(sTable);
            if (n > -1)
            {
                t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                for (int i = 0; i < t.LstField.Count; i++)
                {
                    switch (((Field)t.LstField[i]).Type)
                    {
                        case 's':
                        case 'o': // roadmap
                            LstValue.Add("");
                            break;
                        case 'p': //picture
                        case 'q': //picture
                        case 'i':
                            LstValue.Add((int)0);
                            break;
                        case 'd':
                            LstValue.Add((double)0);
                            break;
                        case 't':
                            //LstValue.Add(DateTime.Parse("01/01/1900"));
                            LstValue.Add("");
                            break;
                    }
                }
            }
        }
    }
}


