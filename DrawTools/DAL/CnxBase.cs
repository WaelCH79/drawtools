#define WRITE //Form1 & ToolPointer
#define CLUSTERREADY

using IdentityModel.OidcClient;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Diagnostics;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using MOI = Microsoft.Office.Interop;

namespace DrawTools
{
    public class CnxBase
    {
        private OdbcConnection connection;
        private OdbcCommand command;
        private int currentReader;
        private OdbcDataReader[] reader = new OdbcDataReader[3];
        private Form1 parent;
        public ConfDataBase ConfDB;
        public System.IO.StreamWriter sw = null;
        public bool bAttribut;
        private string s_connectionString;

        //private OdbcDataReader reader2;

        public CnxBase(Form1 F)
        {
            bAttribut = true;

            //#if WRITE

            //F.SelectedBase = null;
            //Form fs = new FormCnxBase(F);
            //fs.ShowDialog(F);
            F.SelectedBase = "cmdbRead";
            s_connectionString = "DSN=" + F.SelectedBase;
            if (F.SelectedBase != null)
            {
                connection = new OdbcConnection(s_connectionString);
                command = new OdbcCommand();
                currentReader = 0;
                command.Connection = connection;
                command.Connection.Open();
                parent = F;
                ConfDB = new ConfDataBase();
            }
        }

        public OdbcConnection Connection
        {
            get
            {
                return connection;
            }
        }

        public string DBLog(string guidObj, string nomObj, string guidAction, string desc)
        {
            string guid = Guid.NewGuid().ToString();

            CBWrite("insert into log (guidlog, date, guidobjet, nomobjet, guidaction, guidcompte, nomcompte, description) values ('" + guid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "','" + guidObj + "','" + nomObj + "','" + guidAction + "','" + Compte.guid + "','" + Compte.id + "','" + desc + "')");

            return (guid);
        }

        public void SWopen(string s)
        {
            if (sw != null) MessageBox.Show("Attention, le streamwriter n'est pas null");
            sw = System.IO.File.CreateText(s);
        }

        public void SWwriteLog(int indent, string s, bool l)
        {
            string sIndent = "";

            for (int i = 0; i < indent; i++) sIndent += " ";
            sw.WriteLine(DateTime.Now.ToLongTimeString() + sIndent + s);
            if (l) sw.WriteLine(sIndent + "------------------------------");
        }

        public void SWclose()
        {
            sw.Close();
            sw = null;
        }


        public string CmdText
        {
            get
            {
                return command.CommandText;
            }
            set
            {
                command.CommandText = value;
            }
        }
        public OdbcCommand Cmd
        {
            get
            {
                return command;
            }
        }

        public OdbcDataReader Reader
        {
            get
            {
                return reader[currentReader];
            }
            set
            {
                reader[currentReader] = value;
            }
        }

        public List<string> getListTechLinkFromLink(string guid, string guidvue)
        {
            List<string> lstTechLink = new List<string>();
            string sql = "select distinct tlinkrest.guidtechlink from techlinkapp tlinkrest where ";
            sql += "tlinkrest.guidvue = '" + guidvue + "' and tlinkrest.guidlink in (select guidlink from techlinkapp ";
            sql += "where guidtechlink = '" + guid + "' and guidvue = '" + guidvue + "')";
            //sql += "tlinkrest.guidvue = '" + guidvue + "' and tlinkrest.guidlink in (select guidlink from techlink tlinkselect, techlinkapp ";
            //sql += "where tlinkselect.guidtechlink = techlinkapp.guidtechlink and tlinkselect.guidtechlink = '" + guid + "' and ";
            //sql += "techlinkapp.guidvue = '" + guidvue + "')";
            if (CBRecherche(sql))
                while (Reader.Read()) lstTechLink.Add(Reader.GetString(0));
            CBReaderClose();
            return lstTechLink;
        }

        public List<string[]> InitCadreRef()
        {
            List<string[]> lstCadreRef = new List<string[]>();
            if (CBRecherche("select guidcadreref, nomcadreref, guidparent from cadreref"))
            {
                while (Reader.Read())
                {
                    string[] el = new string[3];
                    el[0] = Reader.GetString(0); el[1] = Reader.GetString(1);
                    el[2] = Reader.GetString(2);
                    lstCadreRef.Add(el);
                }
            }
            CBReaderClose();
            return lstCadreRef;
        }

        public void PopulateDataGridFromQuery(string strSelect, DataGridView dgv)
        {
            dgv.DataSource = null;
            SelectReader();
            try
            {
                CmdText = strSelect;
                if (sw != null)
                    SWwriteLog(0, CmdText, false);
                DataTable dt = new DataTable();
                dt.Load(Cmd.ExecuteReader());
                dgv.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public ArrayList getContextTechno(string guidCadreRef, List<string[]> lstcadreref)
        {
            ArrayList lstContext1 = new ArrayList(), lstContext = new ArrayList();
            string elCur = guidCadreRef;

            do
            {
                string[] elFind = lstcadreref.Find(el => el[0] == elCur);
                lstContext1.Add(elFind[1]);
                elCur = elFind[2];
            } while (elCur != "Root");
            for (int i = lstContext1.Count - 1; i >= 0; i--) lstContext.Add(lstContext1[i]);

            return lstContext;
        }



        public List<String[]> GetLstAppWithSoftsarePackage()
        {
            List<String[]> lstApplication = new List<string[]>();

            if (CBRecherche("Select GuidApplication, SoftwarePackage From Application"))
            {
                while (Reader.Read())
                {
                    string[] aApp = new string[2];
                    aApp[0] = Reader.GetString(0);
                    aApp[1] = Reader.GetString(1);
                    lstApplication.Add(aApp);
                }
            }
            CBReaderClose();

            return lstApplication;
        }

        public int fillTechnoXls(string requete, MOI.Excel._Worksheet oSheet, List<string[]> lstcadreref, int row, int col)
        {
            //(0)GuidTechnoRef, (1)TechnologyName, (2)TechnologyVersion, (3)TechnologyType, (4)ObsoScore, (5)GuidProduit, (6)DerogationEndDate, (7)GroupITStandardInCompetition, 
            //(8)RoadmapUpcomingStartDate, (9)RoadmapUpcomingEndDate, (10)RoadmapReferenceStartDate, (11)RoadmapReferenceEndDate, (12)RoadmapConfinedStartDate,
            //(13)RoadmapConfinedEndDate, (14)RoadmapDecommissionedStartDate, (15)RoadmapDecommissionedEndDate, (16)RoadmapSupplierEndOfSupportDate, (17)UserID, (18)UpdateDate

            ArrayList lstcol = new ArrayList();
            if (CBRecherche(requete))
            {
                while (Reader.Read())
                {
                    int iStattutTechno = 0;
                    if (!Reader.IsDBNull(3)) iStattutTechno = Reader.GetInt32(3);

                    //iStattutTechno: (0)Not_Defined, (1)Group_IT_Standard, (2)Complementary_Choice, (3)Conflict_Version, (4)Conflict_Product, (5)Decommissioning
                    if (iStattutTechno < 6)
                    {
                        //Guid Techno
                        oSheet.Cells[++row, 1] = Reader.GetString(0);

                        //TechnologyName, TechnologyVersion, TechnologyType
                        if (!Reader.IsDBNull(1)) oSheet.Cells[row, 2] = Reader.GetString(1);
                        if (!Reader.IsDBNull(2)) oSheet.Cells[row, 3] = Reader.GetString(2);
                        oSheet.Cells[row, 4] = parent.sStatutTechno[iStattutTechno];

                        //Obso

                        //guidproduit
                        if (!Reader.IsDBNull(5)) oSheet.Cells[row, 6] = Reader.GetString(5);

                        //Dates
                        if (!Reader.IsDBNull(8)) oSheet.Cells[row, 9] = Reader.GetDate(8);
                        if (!Reader.IsDBNull(9)) oSheet.Cells[row, 10] = Reader.GetDate(9);
                        if (!Reader.IsDBNull(10)) oSheet.Cells[row, 11] = Reader.GetDate(10);
                        if (!Reader.IsDBNull(11)) oSheet.Cells[row, 12] = Reader.GetDate(11);
                        if (!Reader.IsDBNull(12)) oSheet.Cells[row, 13] = Reader.GetDate(12);
                        if (!Reader.IsDBNull(13)) oSheet.Cells[row, 14] = Reader.GetDate(13);
                        if (!Reader.IsDBNull(14)) oSheet.Cells[row, 15] = Reader.GetDate(14);
                        if (!Reader.IsDBNull(15)) oSheet.Cells[row, 16] = Reader.GetDate(15);

                        if (!Reader.IsDBNull(16)) oSheet.Cells[row, 17] = DateTime.FromOADate(Reader.GetDouble(16)).ToShortDateString();

                        // UserID , Date
                        oSheet.Cells[row, 18] = "Gilles Aldeguer";
                        oSheet.Cells[row, 19] = DateTime.Now.ToShortDateString();

                    }
                }
            }
            CBReaderClose();
            return row;
        }

        public void InitCadreRef(char TechFonc, TreeNodeCollection tn, string guidParent)
        {
            ArrayList guidCadreRef = new ArrayList();
            ArrayList NomCadreRef = new ArrayList();
            string sSelect = "";

            switch (TechFonc)
            {
                case 'T':
                    sSelect = "Select GuidCadreRef, NomCadreRef FROM CadreRef WHERE GuidParent='" + guidParent + "' ORDER BY NomCadreRef";
                    break;
                case 'F':
                    sSelect = "Select GuidCadreRefFonc, NomCadreRefFonc FROM CadreRefFonc WHERE GuidParentFonc='" + guidParent + "' ORDER BY NomCadreRefFonc";
                    break;
            }

            if (CBRecherche(sSelect))
            {
                while (Reader.Read())
                {
                    guidCadreRef.Add((object)Reader.GetString(0));
                    NomCadreRef.Add((object)Reader.GetString(1));
                }
            }
            CBReaderClose();
            for (int i = 0; i < guidCadreRef.Count; i++)
            {
                tn.Add((string)guidCadreRef[i], (string)NomCadreRef[i]);
                InitCadreRef(TechFonc, tn[tn.Count - 1].Nodes, (string)guidCadreRef[i]);
            }
        }

        public void CBAddComboBox(string strSelect, List<string[]> lstApp, ComboBox cb, int index)
        {
            SelectReader();
            lstApp.Clear();
            cb.Items.Clear();
            try
            {
                CmdText = strSelect;
                Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    string[] aEnreg = new string[Reader.FieldCount];
                    for (int i = 0; i < Reader.FieldCount; i++)
                    {
                        switch (Reader.GetFieldType(i).Name)
                        {
                            case "String":
                                if (Reader.IsDBNull(i)) aEnreg[i] = ""; else aEnreg[i] = Reader.GetString(i);
                                break;
                            case "Int32":
                                if (Reader.IsDBNull(i)) aEnreg[i] = "0"; else aEnreg[i] = Reader.GetInt32(i).ToString();
                                break;
                            case "Double":
                                if (Reader.IsDBNull(i)) aEnreg[i] = "0"; else aEnreg[i] = Reader.GetDouble(i).ToString();
                                break;
                            case "DateTime":
                                if (Reader.IsDBNull(i)) aEnreg[i] = ""; else aEnreg[i] = Reader.GetDate(i).ToShortDateString();
                                break;
                        }
                    }
                    lstApp.Add(aEnreg);
                    cb.Items.Add(aEnreg[index]);
                }
                CBReaderClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CBAddComboBox(string strSelect, ComboBox cb)
        {
            SelectReader();
            cb.Items.Clear();
            try
            {
                CmdText = strSelect;
                Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    if (Reader.FieldCount > 1 && !Reader.IsDBNull(1) && Reader.GetString(1) != "") cb.Items.Add(Reader.GetString(0) + "[" + Reader.GetString(1) + "]");
                    else cb.Items.Add((string)Reader[0]);
                }
                CBReaderClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CBAddComboBox(string strSelect, ComboBox cb1, ComboBox cb2)
        {
            SelectReader();
            cb1.Items.Clear(); cb2.Items.Clear();
            try
            {
                CmdText = strSelect;
                Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    cb1.Items.Add((string)Reader[0]);
                    if (Reader.FieldCount > 2 && !Reader.IsDBNull(2) && Reader.GetString(2) != "") cb2.Items.Add(Reader.GetString(1) + " [" + Reader.GetString(2) + "]");
                    else cb2.Items.Add((string)Reader[1]);
                }
                CBReaderClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CBAddComboBox(string strSelect, ComboBox cb1, ComboBox cb2, ComboBox cb3)
        {
            SelectReader();
            cb1.Items.Clear(); cb2.Items.Clear(); cb3.Items.Clear();
            try
            {
                CmdText = strSelect;
                Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    cb1.Items.Add((string)Reader[0]);
                    cb2.Items.Add((string)Reader[1]);
                    cb3.Items.Add((string)Reader[2]);
                }
                CBReaderClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*
        public void CBAddComboBox(string strSelect, DataGridViewComboBoxColumn cb1, DataGridViewComboBoxColumn cb2)
        {
            SelectReader();
            cb1.Items.Clear(); cb2.Items.Clear();
            try
            {
                CmdText = strSelect;
                Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    cb1.Items.Add((string)Reader[0]);
                    cb2.Items.Add((string)Reader[1]);
                }
                CBReaderClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/

        public void CBAddArrayListProp(string sGuidStaticTable, ArrayList lstProp, ArrayList lstVal)
        {
            SelectReader();
            try
            {
                CmdText = "SELECT Propriete, Val FROM StaticTable WHERE GuidStaticProfil='" + parent.sGuidTemplate + "' AND GuidStaticTable='" + sGuidStaticTable + "'";
                Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    lstProp.Add((string)Reader[0]);
                    lstVal.Add((string)Reader[1]);
                }
                CBReaderClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CBAddArrayListProp(string sGuidTemplate, string sGuidStaticTable, ArrayList lstProp, ArrayList lstVal)
        {
            SelectReader();
            try
            {
                CmdText = "SELECT Propriete, Val FROM StaticTable WHERE GuidStaticProfil='" + sGuidTemplate + "' AND GuidStaticTable='" + sGuidStaticTable + "'";
                Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    lstProp.Add((string)Reader[0]);
                    lstVal.Add((string)Reader[1]);
                }
                CBReaderClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CBAddArrayListcObj(string strSelect, ArrayList lst1)
        {
            SelectReader();
            try
            {
                CmdText = strSelect;
                Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    lst1.Add(new cObj((string)Reader[0], (string)Reader[1]));
                }
                CBReaderClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string GetValueInStringNomGuid(string NomGuid, int idx)
        {
            string[] aNomCle = NomGuid.Split(new Char[] { '(', ')' });
            if (aNomCle.Length == 3 && idx < 2)
            {
                return aNomCle[idx].Trim();
            }
            return "";
        }

        public void CBAddListBoxItem(string Nom, string Guid, ListBox lb)
        {
            lb.Items.Add(Nom + "     (" + Guid + ")");
        }


        public bool CBAddListBox(string strSelect, ListBox lb)
        {
            bool bRetour = false;
            SelectReader();
            lb.Items.Clear();
            try
            {
                CmdText = strSelect;
                Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    bRetour = true;
                    if (Reader.FieldCount == 1) lb.Items.Add(Reader.GetString(0));
                    else CBAddListBoxItem(Reader.GetString(1), Reader.GetString(0), lb);

                }
                CBReaderClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return bRetour;
        }

        public void CBAddNodeWithTv(string strSelect, TreeNodeCollection tn)
        {
            if (CBRecherche(strSelect))
            {
                while (Reader.Read())
                {
                    TreeNode[] ArrayTreeNode = parent.tvObjet.Nodes.Find(Reader.GetString(0), true);
                    if (ArrayTreeNode.Length == 1)
                        tn.Add((string)Reader[0], (string)Reader[1]);
                }
            }
            CBReaderClose();
        }

        public void CBAddNodeWithKeyObjet(string strSelect, TreeNodeCollection tn)
        {
            if (CBRecherche(strSelect))
            {
                while (Reader.Read())
                {
                    int n = parent.drawArea.GraphicsList.FindObjet(0, Reader.GetString(0));
                    if (n > -1) tn.Add((string)Reader[1], (string)Reader[2]);
                }
            }
            CBReaderClose();
        }

        public void CBAddNode(string strSelect, XmlDocument xmlDoc, XmlElement el)
        {
            SelectReader();
            try
            {
                CmdText = strSelect;
                Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    if (Reader.FieldCount == 3)
                    {
                        parent.XmlCreatElKeyNode(xmlDoc, el, Reader.GetString(0), Reader.GetString(1));
                        XmlElement elAtts = parent.XmlGetFirstElFromParent(el, "Attributs");
                        parent.XmlSetAttFromEl(xmlDoc, elAtts, "NumInfo", "d", Reader.GetInt32(2).ToString());
                    }
                    else parent.XmlCreatElKeyNode(xmlDoc, el, Reader.GetString(0), Reader.GetString(1));
                }
                CBReaderClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CBAddNode(string strSelect, TreeNodeCollection tn)
        {
            SelectReader();
            try
            {
                CmdText = strSelect;
                Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    if (Reader.FieldCount == 3) tn.Add((string)Reader[0], (string)Reader[1] + "                     -" + Reader.GetInt32(2).ToString());
                    else tn.Add(Reader.GetString(0), Reader.GetString(1));
                }
                CBReaderClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CBAddNodeObjExp(FormExplorObj feo, string strSelect, TreeNode tn, DrawArea.DrawToolType dt)
        {
            SelectReader();
            try
            {
                CmdText = strSelect;
                Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {

                    if (feo.FindObj(Reader.GetString(0), tn.Name) == null)
                    {
                        Guid g = Guid.NewGuid();
                        TreeNode t = tn.Nodes.Add(g.ToString(), Reader.GetString(1));
                        int n = tn.Nodes.Count;
                        Font fontpro = new Font("arial", 8);
                        tn.Nodes[n - 1].NodeFont = new Font(fontpro, FontStyle.Bold);
                        tn.Nodes[n - 1].ForeColor = Color.Blue;
                        //(tn.Nodes[n-1].NodeFont, tn.Nodes[n-1].NodeFont.Style | FontStyle.Bold);
                        ExpObj eo = new ExpObj(g, new Guid(Reader.GetString(0)), dt, -1, t);
                        feo.lstObj.Add((object)eo);

                    }
                    else
                        tn = tn;
                }
                CBReaderClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void SelectReader()
        {
            if (Reader != null)
            {
                if (!Reader.IsClosed && currentReader < 3)
                {
                    currentReader++;
                }
            }
        }

        public bool CBRecherche(string strSelect)
        {
            SelectReader();
            try
            {
                CmdText = strSelect;
                if (sw != null)
                    SWwriteLog(0, CmdText, false);
                Reader = Cmd.ExecuteReader();
                return Reader.HasRows;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        public void CBReaderClose()
        {
            Reader.Close();
            CmdText = "";
            if (currentReader > 0) currentReader--;
        }

        public Guid CBWriteScalar(string strSelect)

        {
#if WRITE
            try
            {

                // open a new connection using a default connection string I have defined elsewhere
                using (OdbcConnection connection = new OdbcConnection(s_connectionString))
                {
                    // ODBC command and transaction objects
                    OdbcCommand command = new OdbcCommand();
                    //OdbcTransaction transaction = null;

                    // tell the command to use our connection
                    command.Connection = connection;

                    try
                    {
                        // open the connection
                        connection.Open();

                        // start the transaction
                        //transaction = connection.BeginTransaction();

                        // Assign transaction object for a pending local transaction.
                        command.Connection = connection;
                        //command.Transaction = transaction;

                        // run the insert using a non query call
                        command.CommandText = strSelect;
                        var t = command.ExecuteNonQuery();
                        
                        /* now we want to make a second call to MYSQL to get the new index 
                           value it created for the primary key.  This is called using scalar so it will
                            return the value of the SQL  statement.  We convert that to an int for later use.*/
                        //command.CommandText = "SELECT LAST_INSERT_ID();";
                        // Guid id;
                        //var t = command.ExecuteScalar();
                       // Guid.TryParse((command.ExecuteScalar()).ToString(), out Guid id);
                        // Commit the transaction.
                       // transaction.Commit();

                       // return id;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);

                        //try
                        //{
                        //    // Attempt to roll back the transaction.
                        //    transaction.Rollback();
                        //}
                        //catch
                        //{
                        //    // Do nothing here; transaction is not active.
                        //}
                    }
                }


                CmdText = strSelect.Replace("\\","\\\\");
                //Cmd.Parameters.AddWithValue(

                Cmd.ExecuteScalar();
               
            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message);
            }
            return new Guid();
#endif
        }


        public void CBWrite(string strSelect)
        {
#if WRITE
            try
            {
                CmdText = strSelect.Replace("\\", "\\\\");
                //Cmd.Parameters.AddWithValue(

                Cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
#endif
        }

        public void CBWriteWithObj(string strSelect, object o)
        {
#if WRITE
            try
            {
                CmdText = CmdText = strSelect.Replace("\\", "\\\\");
                //Cmd.Parameters.Add(Param, OdbcType.Binary);
                //Cmd.Parameters[Param].Value = o;
                //Cmd.Parameters.AddWithValue("buffer", o);

                //Cmd.Parameters.Add("param1", OdbcType.Binary).Value = o;
                if(o!=null) Cmd.Parameters.AddWithValue("param1", o);
                //Cmd.ExecuteNonQuery();
                
                Cmd.ExecuteScalar();
                Cmd.Parameters.Clear();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
#endif
        }

        public void DeleteVue(string sGuidGVue)
        {
            if (sGuidGVue != null)
            {
                // Suppression des objets GModule de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Module], sGuidGVue);

                // Suppression des objets GModule de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.AppUser], sGuidGVue);

                // Suppression des objets GModule de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Application], sGuidGVue);

                // Suppression des objets GMainComposant de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.MainComposant], sGuidGVue);

                // Suppression des objets GCompFonc de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.CompFonc], sGuidGVue);

                // Suppression des objets GComposant de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Composant], sGuidGVue);

                // Suppression des objets GInterface de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Interface], sGuidGVue);

                // Suppression des objets GFile de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.File], sGuidGVue);

                // Suppression des objets GBase de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Base], sGuidGVue);

                // Suppression des objets GTechUser de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.TechUser], sGuidGVue);

                // Suppression des objets GServer de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Server], sGuidGVue);

                // Suppression des objets GServMComp de l'ancienne Vue
                //DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.ServMComp], sGuidGVue);

                // Suppression des objets GLink de l'ancienne Vue
                DeleteGObjetEtPoint(parent.drawArea.tools[(int)DrawArea.DrawToolType.Link], sGuidGVue);

                // Suppression des objets GTechLink de l'ancienne Vue
                DeleteGObjetEtPoint(parent.drawArea.tools[(int)DrawArea.DrawToolType.TechLink], sGuidGVue);

                // Suppression des objets GServerPhy de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.ServerPhy], sGuidGVue);

                // Suppression des objets GVLan de l'ancienne Vue
                DeleteGObjetEtPoint(parent.drawArea.tools[(int)DrawArea.DrawToolType.VLan], sGuidGVue);

                // Suppression des objets GRouter de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Router], sGuidGVue);

                // Suppression des objets GNCard de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.NCard], sGuidGVue);

                // Suppression des objets GCluster de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Cluster], sGuidGVue);

                // Suppression des objets GBaie de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Baie], sGuidGVue);

                // Suppression des objets GMachine de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Machine], sGuidGVue);

                // Suppression des objets GVirtuel de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Virtuel], sGuidGVue);

                // Suppression des objets GLun de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Lun], sGuidGVue);

                // Suppression des objets GLun de l'ancienne Vue
                DeleteGObjetEtPoint(parent.drawArea.tools[(int)DrawArea.DrawToolType.Zone], sGuidGVue);

                // Suppression des objets GLun de l'ancienne Vue
                DeleteGObjetEtPoint(parent.drawArea.tools[(int)DrawArea.DrawToolType.ServerSite], sGuidGVue);

                // Suppression des objets GLun de l'ancienne Vue
                DeleteGObjetEtPoint(parent.drawArea.tools[(int)DrawArea.DrawToolType.Location], sGuidGVue);

                // Suppression des objets GLun de l'ancienne Vue
                DeleteGObjetEtPoint(parent.drawArea.tools[(int)DrawArea.DrawToolType.PtCnx], sGuidGVue);

                // Suppression des objets GLun de l'ancienne Vue
                DeleteGObjetEtPoint(parent.drawArea.tools[(int)DrawArea.DrawToolType.InterLink], sGuidGVue);

                // Suppression des objets GLun de l'ancienne Vue
                DeleteGObjetEtPoint(parent.drawArea.tools[(int)DrawArea.DrawToolType.Cnx], sGuidGVue);

                // Suppression des objets GBaieCTI de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.BaieCTI], sGuidGVue);

                // Suppression des objets GSanWitch de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.ISL], sGuidGVue);

                // Suppression des objets GSanWitch de l'ancienne Vue
                DeleteGObjetEtPoint(parent.drawArea.tools[(int)DrawArea.DrawToolType.SanSwitch], sGuidGVue);

                // Suppression des objets GSanCard de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.SanCard], sGuidGVue);

                // Suppression des objets GBaieDPhy de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.BaieDPhy], sGuidGVue);

                // Suppression des objets GDrawer de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.Drawer], sGuidGVue);

                // Suppression des objets GBaiePhy de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.BaiePhy], sGuidGVue);

                // Suppression des objets GMachine de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.MachineCTI], sGuidGVue);

                // Suppression des objets GCadreRef de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.CadreRefN], sGuidGVue);

                // Suppression des objets GCadreRef de l'ancienne Vue
                DeleteGObject(parent.drawArea.tools[(int)DrawArea.DrawToolType.CadreRefEnd], sGuidGVue);

                //CBWrite("DELETE FROM Vue WHERE GuidVue='" + sGuidVue + "'");
            }
        }


        public void UpdateObject(DrawObject o)
        {
            //string sType = o.GetsType(false);
            string sType = o.GetTypeSimpleTable();
            string Update = "UPDATE " + sType;
            string Set = "SET ";
            string Where = "WHERE ";

            Table t;
            int n = ConfDB.FindTable(sType);
            if (n > -1)
            {
                t = (Table)ConfDB.LstTable[n];
                bool InsFieldW = false, InsFieldR = false;

                for (int i = 0; i < t.LstField.Count; i++)
                {
                    if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.InterneBD) != 0)
                    {

                        if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.Key) != 0)
                        {
                            switch (((Field)t.LstField[i]).Type)
                            {
                                case 's':
                                    if ((string)o.LstValue[i] != "" && ((string)o.LstValue[i])[0] != 32)
                                    {
                                        if (InsFieldW) Set += " and ";
                                        Where += ((Field)t.LstField[i]).Name + "='" + o.LstValue[i] + "'";
                                        InsFieldW = true;
                                    }
                                    break;
                                case 'p': //picture
                                case 'q': //picture
                                case 'i':
                                    if ((int)o.LstValue[i] != 0)
                                    {
                                        if (InsFieldW) Set += " and ";
                                        Where += ((Field)t.LstField[i]).Name + "=" + o.LstValue[i];
                                        InsFieldW = true;
                                    }
                                    break;
                                case 'd':
                                    if ((double)o.LstValue[i] != 0)
                                    {
                                        if (InsFieldR) Set += ", ";
                                        Set += ((Field)t.LstField[i]).Name + "=" + o.LstValue[i];
                                        InsFieldR = true;
                                    }
                                    break;
                                case 't':
                                    //if (o.LstValue[i] != null && (string)o.LstValue[i] != "" && (DateTime)o.LstValue[i] != DateTime.MinValue)
                                    if (o.LstValue[i] != null && (string)o.LstValue[i] != "")
                                    {
                                        DateTime dt = (DateTime)o.LstValue[i];
                                        if (InsFieldR) Set += ", ";
                                        Set += ((Field)t.LstField[i]).Name + "='" + dt.ToString("yyyy-MM-dd") + "'"; //dt.ToShortDateString() + "'";
                                        InsFieldR = true;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            switch (((Field)t.LstField[i]).Type)
                            {
                                case 's':
                                    if (InsFieldR) Set += " , ";
                                    InsFieldR = true;
                                    if ((string)o.LstValue[i] != "")
                                        Set += ((Field)t.LstField[i]).Name + "='" + o.LstValue[i].ToString().Replace("'", "''") + "'";
                                    else
                                        Set += ((Field)t.LstField[i]).Name + "= null";
                                    break;
                                case 'p': //picture
                                case 'q': //picture
                                case 'i':
                                    if ((int)o.LstValue[i] != 0)
                                    {
                                        if (InsFieldR) Set += " , ";
                                        Set += ((Field)t.LstField[i]).Name + "=" + o.LstValue[i];
                                        InsFieldR = true;
                                    }
                                    break;
                                case 'd':
                                    if ((double)o.LstValue[i] != 0)
                                    {
                                        if (InsFieldR) Set += " , ";
                                        Set += ((Field)t.LstField[i]).Name + "=" + o.LstValue[i].ToString().Replace(',', '.');
                                        InsFieldR = true;
                                    }
                                    break;
                                case 't':
                                    if (o.LstValue[i] != null && (DateTime)o.LstValue[i] != DateTime.MinValue)
                                    {
                                        DateTime dt = (DateTime)o.LstValue[i];
                                        if (InsFieldR) Set += ", ";
                                        Set += ((Field)t.LstField[i]).Name + "='" + dt.ToString("yyyy-MM-dd") + "'"; //dt.ToShortDateString() + "'";
                                        InsFieldR = true;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            //DataTable datatable1 = connection.GetSchema("Tables", new string[] { null, sType, null });
            //Fdatatable1 = connection.GetSchema(OdbcMetaDataCollectionNames.Views);
            //datatable1 = connection.GetSchema(OdbcMetaDataCollectionNames.Indexes);
            //datatable1 = connection.GetSchema(OdbcMetaDataCollectionNames.Procedures);
            //datatable1 = connection.GetSchema(OdbcMetaDataCollectionNames.Columns);
            //DataTable datatable1 = connection.GetSchema(OdbcMetaDataCollectionNames.Columns, new string[] { sType });
            //DataTable datatable = connection.GetSchema("columns", new[] { null, null, sType, "updatedate" });

            // cette fonction UpdateObject existe aussi avec l'objet enreg
            if (isDataTableContainColumn("updatedate", sType))
                Set += ", updatedate='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
            CBWrite(Update + " " + Set + " " + Where);
        }

        public bool isDataTableContainColumn(string sColumnName, string sTableName)
        {
            CBRecherche("select * from " + sTableName + " where 1=0");

            //DataTable table = connection.GetSchema("Tables", new string[] { null, null, sTableName });
            //DataColumnCollection columns = table.Columns;

            DataTable table = Reader.GetSchemaTable();
            Reader.Close();

            //For each field in the table...
            foreach (DataRow Field in table.Rows)
            {
                if (Field[0].ToString() == sColumnName) return true;

            }
            //if (columns.Contains(sColumnName)) return true;

            return false;
        }

        public bool isDataTableExist(string sTableName)
        {
            bool bExist = false;
            CmdText = "select count(*) from " + sTableName;
            try
            {
                Reader = Cmd.ExecuteReader();

                bExist = true;

            }
            catch
            {
                bExist = false;
            }
            CBReaderClose();
            return bExist;
        }

        public object GetValueFromNameInXmlReader(XmlElement el, string ValueName)
        {
            XmlElement elFind = parent.XmlFindElFromAtt(el, "Name", ValueName);
            if (elFind != null)
            {
                string sType = elFind.GetAttribute("Type");
                if (sType == "String")
                {
                    if (elFind.GetAttribute("Value") == "null") return null;
                    return elFind.GetAttribute("Value");
                }
                else if (sType == "Double")
                {
                    if (elFind.GetAttribute("Value") == "null") return 0;
                    return Convert.ToDouble(elFind.GetAttribute("Value"));
                }
                else if (sType == "DateTime")
                {
                    if (elFind.GetAttribute("Value") == "null") return DateTime.MinValue;
                    return Convert.ToDateTime(elFind.GetAttribute("Value"));
                }
            }
            //if (elCur.GetAttribute(sAtt) != null) elCur.SetAttribute(sAtt, sValue);
            //XmlAttributeCollection lstAtt = el.Attributes;

            return null;
        }

        public XmlDocument CreatXmlDocFromXmlFiles(XmlDocument xmlData, XmlDocument xmlTemplate)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<Export></Export>");

            CreatXmlDocFromXmlFiles(xmlDoc, xmlData, xmlTemplate);

            return xmlDoc;
        }

        public void CreatXmlDocFromXmlFiles(XmlDocument xmlDoc, XmlDocument xmlData, XmlDocument xmlTemplate)
        {
            XmlElement root = xmlDoc.DocumentElement;
            XmlElement rootData = xmlData.DocumentElement;
            XmlElement rootTemplate = xmlTemplate.DocumentElement;

            IEnumerator ienum = rootData.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    XmlElement elRow = xmlDoc.CreateElement("row");
                    XmlElement elRowData = (XmlElement)Node;

                    IEnumerator ienumTemplate = rootTemplate.GetEnumerator();
                    XmlNode NodeTemplate;
                    while (ienumTemplate.MoveNext())
                    {
                        NodeTemplate = (XmlNode)ienumTemplate.Current;
                        if (NodeTemplate.NodeType == XmlNodeType.Element)
                        {
                            XmlElement elTemplate = (XmlElement)NodeTemplate;
                            string sElName = elTemplate.GetAttribute("Name");
                            if (sElName != null)
                            {
                                XmlElement el = xmlDoc.CreateElement(sElName);
                                string[] aTemplate = elTemplate.GetAttribute("Template").Split('+');
                                string sTemplate = "";
                                for (int i = 0; i < aTemplate.Length; i++)
                                {
                                    if (aTemplate[i][0] == '@')
                                    {
                                        XmlElement elField = parent.XmlFindElFromAtt(elRowData, "Name", aTemplate[i].Substring(1));
                                        if (elField != null) sTemplate += elField.GetAttribute("Value");
                                    }
                                    else
                                    {
                                        sTemplate += aTemplate[i];
                                    }
                                }
                                el.InnerText = sTemplate;
                                elRow.AppendChild(el);
                            }
                        }
                    }
                    root.AppendChild(elRow);
                }
            }
        }

        public XmlDocument CreatXmlDocFromDB()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<Reader></Reader>");
            XmlElement root = xmlDoc.DocumentElement;


            while (Reader.Read())
            {
                XmlElement xmlRow = xmlDoc.CreateElement("row");
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    XmlElement xmlField = xmlDoc.CreateElement("Field");
                    xmlField.SetAttribute("Name", Reader.GetName(i));
                    xmlField.SetAttribute("Type", Reader.GetFieldType(i).ToString().Substring(7)); //System.
                    if (Reader.IsDBNull(i)) xmlField.SetAttribute("Value", "null");
                    else
                    {
                        if (Reader.GetFieldType(i) == typeof(string)) xmlField.SetAttribute("Value", Reader.GetString(i));
                        else if (Reader.GetFieldType(i) == typeof(Double)) xmlField.SetAttribute("Value", Reader.GetDouble(i).ToString());
                        else if (Reader.GetFieldType(i) == typeof(DateTime)) xmlField.SetAttribute("Value", Reader.GetDateTime(i).ToShortDateString());
                    }
                    xmlRow.AppendChild(xmlField);
                }
                root.AppendChild(xmlRow);
            }
            CBReaderClose();

            return xmlDoc;
        }

        public void CreatDansTypeVue(Guid GuidObjet, string TypeObjet)
        {
            CBWrite("INSERT INTO DansTypeVue (GuidTypeVue, GuidObjet, TypeObjet) VALUES ('" + parent.GuidTypeVue + "','" + GuidObjet + "','" + TypeObjet + "')");
        }

        public void CreatServiceLink(string sGroupService, string sEnvironnement, ListBox lb)
        {
            for (int i = 0; i < lb.Items.Count; i++)
            {
                CBWrite("Insert Into ServiceLink (GuidGroupService, GuidEnvironnement, GuidService) Values ('" + sGroupService + "','" + sEnvironnement + "','" + GetValueInStringNomGuid((string)lb.Items[i], 1) + "')");
            }
        }

        public void CreatTechLink(DrawTechLink dl)
        {

            CBWrite("INSERT INTO TechLink (GuidTechLink, NomTechLink, GuidServerIn, GuidServerOut) VALUES ('" + dl.GuidkeyObjet + "','" + dl.Texte + "','" + ((DrawObject)dl.LstLinkIn[0]).GuidkeyObjet + "','" + ((DrawObject)dl.LstLinkOut[0]).GuidkeyObjet + "')");
            string sLinkApp = dl.sValue[2];

            string[] aLinkApp = sLinkApp.Split(new Char[] { '(', ')' });
            for (int i = 1; i < aLinkApp.Length; i += 2)
            {
                CBWrite("UPDATE Link SET GuidTechLink='" + dl.GuidkeyObjet + "' WHERE GuidLink = '" + aLinkApp[i] + "'");
            }
        }

        /*
        public void CreatMCompServ(DrawServMComp ds)
        {
            for (int i = 0; i<ds.LstParent.Count; i++)
            {
                if (!CBRecherche("Select GuidMainComposant From MCompServ WHERE GuidMainComposant='" + ds.GuidkeyObjet + "' AND GuidServer='" + ((DrawObject)ds.LstParent[i]).GuidkeyObjet + "'"))
                {
                    CBReaderClose();
                    CBWrite("INSERT INTO MCompServ (GuidMainComposant, GuidServer) VALUES ('" + ds.GuidkeyObjet + "','" + ((DrawObject)ds.LstParent[i]).GuidkeyObjet + "')");
                }
                else CBReaderClose();
            }
        }*/

        public void CreatTechLinkApp(DrawTechLink dt)
        {
            object o;

            o = dt.GetValueFromName("LinksApplicatifs");
            if (o != null && (string)o != "")
            {
                string[] aLinkApp = ((string)o).Split(new Char[] { '(', ')' });
                for (int i = 1; i < aLinkApp.Length; i += 2)
                {
                    if (!CBRecherche("Select GuidTechLink From TechLinkApp" + " WHERE GuidTechLink='" + dt.GuidkeyObjet + "' AND GuidLink='" + aLinkApp[i].Trim() + "'"))
                    {
                        CBReaderClose();
                        CBWrite("INSERT INTO TechLinkApp (GuidTechLink, GuidLink) VALUES ('" + dt.GuidkeyObjet + "','" + aLinkApp[i].Trim() + "')");
                    }
                    else CBReaderClose();
                }
            }
        }

        public void DeleteServerTypeLink(DrawServer ds)
        {
            CBWrite("Delete From Techno Where GuidTechnoHost in (Select GuidServerType From ServerTypeLink Where GuidServer='" + ds.GuidkeyObjet + "')");
            CBWrite("Delete From ServerTypeLink WHERE GuidServer='" + ds.GuidkeyObjet + "'");
        }

        public List<string[]> GetListTechno(string sType, string sTypeRef, string Guid, string prefix = "")
        {
            List<string[]> lstTechno = new List<string[]>();
            //if (CBRecherche("Select GuidTechno, GuidServerType From Techno Where GuidServerType in (Select GuidServerType From ServerTypeLink Where GuidServer='" + ds.GuidkeyObjet + "')"))
            if (CBRecherche("Select GuidTechno, GuidTechnoHost From Techno Where GuidTechnoHost in (Select Guid" + sType + " From " + prefix + sType + "Link Where Guid" + sTypeRef + "='" + Guid + "')"))
            {

                while (Reader.Read())
                {
                    string[] aEnreg = new string[2];
                    aEnreg[0] = Reader.GetString(0);
                    aEnreg[1] = Reader.GetString(1);
                    lstTechno.Add(aEnreg);
                }
                CBReaderClose();
            }
            else CBReaderClose();
            return lstTechno;
        }


        public List<string[]> GetListMCompInfra(string sTypeRef, string Guid, string sTableLink = "MCompApp")
        {
            List<string[]> lstMCompServ = new List<string[]>();
            if (CBRecherche("Select GuidMCompServ, GuidMainComposant From MCompServ Where GuidMainComposant in (Select GuidMainComposant From " + sTableLink + "  Where Guid" + sTypeRef + "='" + Guid + "')"))
            {

                while (Reader.Read())
                {
                    string[] aEnreg = new string[2];
                    aEnreg[0] = Reader.GetString(0);
                    aEnreg[1] = Reader.GetString(1);
                    lstMCompServ.Add(aEnreg);
                }
                CBReaderClose();
            }
            else CBReaderClose();
            return lstMCompServ;
        }

        public List<string> GetListObjByObjRef(string sType, string sTypeRef, string Guid, string prefix = "")
        {
            List<string> lstObj = new List<string>();
            if (CBRecherche("Select Guid" + sType + " FROM " + prefix + sType + "Link WHERE Guid" + sTypeRef + "='" + Guid + "'"))
            {
                while (Reader.Read()) lstObj.Add(Reader.GetString(0));
                CBReaderClose();
            }
            else CBReaderClose();
            return lstObj;
        }

        public List<string> GetListObjByObjRefFromSas(string sType, string sTypeRef, string Guid)
        {
            List<string> lstObj = new List<string>();
            if (CBRecherche("Select GuidServerType FROM SasServerTypeLink" + sType + "Link WHERE Guid" + sTypeRef + "='" + Guid + "'"))
            {
                while (Reader.Read()) lstObj.Add(Reader.GetString(0));
                CBReaderClose();
            }
            else CBReaderClose();
            return lstObj;
        }

        public List<string> GetListMCompObjLink(string sTypeRef, string Guid, string sTableLink = "MCompApp")
        {
            List<string> lstMCompApp = new List<string>();
            if (CBRecherche("Select GuidMainComposant FROM " + sTableLink + " WHERE Guid" + sTypeRef + "='" + Guid + "'"))
            {
                while (Reader.Read()) lstMCompApp.Add(Reader.GetString(0));
                CBReaderClose();
            }
            else CBReaderClose();
            return lstMCompApp;
        }
        public void DeleteMCompApp(DrawServer ds)
        {
            CBWrite("Delete From MCompServ Where GuidMainComposant in (Select GuidMainComposant From MCompApp Where GuidServer='" + ds.GuidkeyObjet + "')");
            CBWrite("DELETE FROM MCompApp WHERE GuidServer='" + ds.GuidkeyObjet + "'");
        }

        public void DeleteUserMComp(DrawTechUser dtu)
        {
            //CBWrite("Delete From MCompServ Where GuidMainComposant in (Select GuidMainComposant From MCompApp Where GuidServer='" + ds.GuidkeyObjet + "')");
            CBWrite("DELETE FROM AppUserMComp WHERE GuidAppUser='" + dtu.GuidkeyObjet + "'");
        }

        public void DeleteUserTypeLink(DrawTechUser dtu)
        {
            CBWrite("DELETE FROM AppUserTypeLink WHERE GuidAppUser='" + dtu.GuidkeyObjet + "'");
        }

        public void CreatUserTypeLink(DrawTechUser dtu, DrawServerType dst)
        {
            CBWrite("INSERT INTO AppUserTypeLink (GuidAppUser, GuidServerType) VALUES ('" + dtu.GuidkeyObjet + "','" + (string)dst.LstValue[0] + "')");
        }

        public void CreatMCompApp(DrawServer ds, DrawMainComposant dmc)
        {
            CBWrite("INSERT INTO MCompApp (GuidServer, GuidMainComposant) VALUES ('" + ds.GuidkeyObjet + "','" + (string)dmc.LstValue[0] + "')");
        }

        public void CreatMCompSas(DrawGensas dgs, DrawMainComposant dmc)
        {
            CBWrite("INSERT INTO MCompSas (GuidGenSas, GuidMainComposant) VALUES ('" + dgs.GuidkeyObjet + "','" + (string)dmc.LstValue[0] + "')");
        }

        public void CreatMCompPod(DrawGenpod dgp, DrawMainComposant dmc)
        {
            CBWrite("INSERT INTO MCompPod (GuidGenPod, GuidMainComposant) VALUES ('" + dgp.GuidkeyObjet + "','" + (string)dmc.LstValue[0] + "')");
        }

        public void CreatUserMComp(DrawTechUser dtu, DrawMainComposant dmc)
        {
            CBWrite("INSERT INTO AppUserMComp (GuidAppUser, GuidMainComposant) VALUES ('" + dtu.GuidkeyObjet + "','" + (string)dmc.LstValue[0] + "')");
        }

        public void CreatServerTypeLink(DrawServer ds, DrawServerType dst)
        {
            CBWrite("INSERT INTO ServerTypeLink (GuidServer, GuidServerType) VALUES ('" + ds.GuidkeyObjet + "','" + (string)dst.LstValue[0] + "')");
        }

        public void CreatObjLink(string sTypeLink, string GuidTypeLink, string sTypeRef, string GuidTypeRef, string prefix = "")
        {
            CBWrite("INSERT INTO " + prefix + sTypeLink + "Link (Guid" + sTypeRef + ", Guid" + sTypeLink + ") VALUES ('" + GuidTypeRef + "','" + GuidTypeLink + "')");
        }


        public void DeleteServiceLink(string sGuidGroupService)
        {
            CBWrite("Delete From ServiceLink Where GuidGroupService='" + sGuidGroupService + "'");
        }

        public void DeleteObjetsLink(Guid GuidVue)
        {
            CBWrite("Delete From ApplicationLink Where GuidVue='" + GuidVue.ToString() + "'");
            CBWrite("Delete From AppUserLink Where GuidVue='" + GuidVue.ToString() + "'");
            ArrayList aLstServerPhy = new ArrayList();
            if (CBRecherche("Select GuidServerPhy From ServerLink Where GuidVue='" + GuidVue.ToString() + "'"))
            {
                // suppression uniquement de serverphy non présent dans la vue. Le reste est géré avec les packages associés
                while (Reader.Read()) aLstServerPhy.Add(Reader.GetString(0));
            }
            CBReaderClose();
            for (int i = 0; i < aLstServerPhy.Count; i++)
            {
                if (parent.drawArea.GraphicsList.FindObjet(0, (string)aLstServerPhy[i]) < 0)
                {
                    CBWrite("Delete From ServerLink Where GuidVue='" + GuidVue.ToString() + "' and GuidServerPhy='" + aLstServerPhy[i] + "'");
                }
            }
            //CBWrite("Delete From ServerLink Where GuidVue='" + GuidVue.ToString() + "'"); // avant il faut gérer les pacakges dans la vue est direct en base
        }

        public void DeleteNCardLink(string GVue, string Sens, string sGuidVueInf)
        {
            ArrayList aEnreg = new ArrayList();
            //string sQuery = "SELECT NCardLink" + Sens + ".GuidNCard, NCardLink" + Sens + ".GuidTechLink, NomTechLink FROM DansVue, GNCard, NCardLink" + Sens + ", TechLink WHERE GuidVue ='" + GVue + "' AND GuidObjet=GuidGNCard AND GNCard.GuidNCard=NCardLink" + Sens + ".GuidNCard AND NCardLink" + Sens + ".GuidTechLink=TechLink.GuidTechLink ORDER BY NCardLink" + Sens + ".GuidNCard";
            //if (Sens == "Out")
            //{
            string sQuery = "SELECT NCardLink" + Sens + ".GuidNCard, NCardLink" + Sens + ".GuidTechLink, NomTechLink FROM Vue, DansVue, GNCard, NCardLink" + Sens + ", TechLink WHERE Vue.GuidVue ='" + GVue + "' and Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGNCard AND GNCard.GuidNCard=NCardLink" + Sens + ".GuidNCard AND NCardLink" + Sens + ".GuidTechLink=TechLink.GuidTechLink ";
            sQuery += "AND TechLink.GuidTechLink IN (Select GuidTechLink FROM Vue, DansVue, GTechLink Where Vue.GuidVue='" + sGuidVueInf + "' and Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGTechLink) ORDER BY NCardLink" + Sens + ".GuidNCard";
            //}

            if (CBRecherche(sQuery))
            {
                while (Reader.Read())
                {
                    aEnreg.Add(Reader.GetString(0) + ";" + Reader.GetString(1));
                }
                CBReaderClose();
            }
            else CBReaderClose();
            for (int i = 0; i < aEnreg.Count; i++)
            {
                string[] Enreg = ((string)aEnreg[i]).Split(';');
                CBWrite("DELETE FROM NCardLink" + Sens + "  WHERE GuidNCard='" + Enreg[0] + "' AND GuidTechLink='" + Enreg[1] + "'");
            }
        }

        public void DelNCardAlias(DrawNCard dnc)
        {
        }

        public void MajNCardAlias(DrawNCard dnc)
        {
            object o;
            o = dnc.GetValueFromName("Alias");
            if (o != null && (string)o != "")
            {
                string[] aAlias = ((string)o).Split(new Char[] { '(', ')', ';' });
                for (int i = 1; i < aAlias.Length; i += 3)
                {
                    if (!CBRecherche("Select GuidAlias From Alias WHERE GuidAlias='" + aAlias[i].Trim() + "'"))
                    {
                        CBReaderClose();
                        CBWrite("Insert Into Alias (GuidAlias, NomAlias, GuidNCard) Values('" + aAlias[i].Trim() + "','" + aAlias[i - 1].Trim() + "','" + dnc.GuidkeyObjet + "')");
                    }
                    else
                    {
                        CBReaderClose();
                        CBWrite("Update Alias Set NomAlias='" + aAlias[i - 1].Trim() + "' Where GuidAlias='" + aAlias[i].Trim() + "'");
                    }
                }
            }
        }

        public void CreatNCardLink(DrawNCard dnc, string Sens)
        {
            object o;

            o = dnc.GetValueFromName("Flux" + Sens);
            if (o != null && (string)o != "")
            {
                string[] aLinkApp = ((string)o).Split(new Char[] { '(', ')' });
                for (int i = 1; i < aLinkApp.Length; i += 2)
                    CBWrite("INSERT INTO NCardLink" + Sens + " (GuidNCard, GuidTechLink) VALUES ('" + dnc.GuidkeyObjet + "','" + aLinkApp[i].Trim() + "')");
            }
        }

        public void CreatNCardLinkIn(DrawNCard dnc)
        {
            object o;

            o = dnc.GetValueFromName("FluxIn");
            if (o != null && (string)o != "")
            {
                string[] aLinkApp = ((string)o).Split(new Char[] { '(', ':', ')' });
                for (int i = 1; i < aLinkApp.Length; i += 3)
                    CBWrite("INSERT INTO NCardLinkIn (GuidNCard, GuidTechLink, GuidAlias) VALUES ('" + dnc.GuidkeyObjet + "','" + aLinkApp[i].Trim() + "','" + aLinkApp[i + 1].Trim() + "')");

            }
        }

        public void CreatRouterLink(DrawRouter dr)
        {
            for (int i = 0; i < dr.LstLinkIn.Count; i++)
            {
                if (!CBRecherche("Select GuidRouter FROM RouterLink WHERE GuidRouter='" + dr.GuidkeyObjet + "' AND GuidVLan='" + ((DrawVLan)dr.LstLinkIn[i]).GuidkeyObjet + "'"))
                {
                    CBReaderClose();
                    CBWrite("INSERT INTO RouterLink (GuidRouter, GuidVLan) VALUES ('" + dr.GuidkeyObjet + "','" + ((DrawVLan)dr.LstLinkIn[i]).GuidkeyObjet + "')");
                }
                else CBReaderClose();
            }
        }

        public void LoadExtention()
        {
            Table t = null; int n; string sType;

            for (int i = 0; i < parent.drawArea.GraphicsList.Count; i++)
            {
                DrawObject dobj = (DrawObject)parent.drawArea.GraphicsList[i];

                string sSelect = dobj.GetSelectExtention();
                string sGuid;

                if (sSelect != null)
                {

                    sType = dobj.GetTypeSimpleTable();
                    sGuid = (string)dobj.GetValueFromName("GuidManagedsvc");

                    //Chargement des méta données de l'objet
                    n = ConfDB.FindTable(sType);
                    if (n > -1) t = (Table)ConfDB.LstTable[n];

                    if (t != null && sGuid != null)
                    {
                        STExtention stEx = t.lstExtention.Find(el => el.sGuidExtention == sGuid);
                        if (stEx.sGuidExtention == null)
                        {
                            if (CBRecherche(sSelect + " and Type='" + sType + "' order by pos"))
                            {
                                stEx.sGuidExtention = sGuid;
                                stEx.lstFieldExtention = new ArrayList();

                                while (Reader.Read())
                                {
                                    stEx.lstFieldExtention.Add(new Field(Reader.GetString(1), Reader.GetString(2), 's', Reader.GetInt32(3), 0, ConfDataBase.FieldOption.Base));
                                }
                                t.lstExtention.Add(stEx);
                            }
                            CBReaderClose();
                        }
                        if (stEx.sGuidExtention != null)
                        {
                            dobj.LstValueExtention = t.InitValueExtention(sGuid);

                            CBRecherche("Select GuidParam, Value From ExtentionParamValue Where GuidObject = '" + dobj.GuidkeyObjet + "'");
                            while (Reader.Read())
                                dobj.SetValueFromName(Reader.GetString(0), Reader.GetString(1), true);
                            CBReaderClose();
                        }
                    }

                }
            }
        }


        public void LoadSousObjets(int iTypeObjet)
        {
            ArrayList LstObjet = new ArrayList();
            switch (iTypeObjet)
            {
                case (int)DrawArea.DrawToolType.Server:
                    for (int i = 0; i < parent.drawArea.GraphicsList.Count; i++)
                    {
                        if (parent.drawArea.GraphicsList[i].GetType() == typeof(DrawServer)) LstObjet.Add(parent.drawArea.GraphicsList[i]);
                    }
                    LoadServerType_Techno(LstObjet);
                    LoadMainComposant_ServMComp(LstObjet);
                    break;
                case (int)DrawArea.DrawToolType.Gensas:
                    for (int i = 0; i < parent.drawArea.GraphicsList.Count; i++)
                    {
                        if (parent.drawArea.GraphicsList[i].GetType() == typeof(DrawGensas)) LstObjet.Add(parent.drawArea.GraphicsList[i]);
                    }
                    LoadManagedsvc_Techno(LstObjet);
                    LoadSvcServerType_Techno(LstObjet);
                    LoadMainComposant_ServMSas(LstObjet);
                    break;
                case (int)DrawArea.DrawToolType.Genpod:
                    for (int i = 0; i < parent.drawArea.GraphicsList.Count; i++)
                    {
                        if (parent.drawArea.GraphicsList[i].GetType() == typeof(DrawGenpod)) LstObjet.Add(parent.drawArea.GraphicsList[i]);
                    }
                    LoadContainer_Techno(LstObjet);
                    LoadMainComposant_ServMpod(LstObjet);
                    break;
                case (int)DrawArea.DrawToolType.TechUser:
                    for (int i = 0; i < parent.drawArea.GraphicsList.Count; i++)
                    {
                        if (parent.drawArea.GraphicsList[i].GetType() == typeof(DrawTechUser)) LstObjet.Add(parent.drawArea.GraphicsList[i]);
                    }
                    LoadUserType_Techno(LstObjet);
                    LoadMainComposant_UserMComp(LstObjet);
                    break;
                case (int)DrawArea.DrawToolType.Cluster:
                    for (int i = 0; i < parent.drawArea.GraphicsList.Count; i++)
                    {
                        if (parent.drawArea.GraphicsList[i].GetType() == typeof(DrawCluster)) LstObjet.Add(parent.drawArea.GraphicsList[i]);
                    }
                    LoadServerPhy_Cluster(LstObjet);
                    break;
                case (int)DrawArea.DrawToolType.ServerPhy:
                    for (int i = 0; i < parent.drawArea.GraphicsList.Count; i++)
                    {
                        if (parent.drawArea.GraphicsList[i].GetType() == typeof(DrawServerPhy)) LstObjet.Add(parent.drawArea.GraphicsList[i]);
                    }
                    LoadNcard(LstObjet);
                    break;
                case (int)DrawArea.DrawToolType.Insks:
                    for (int i = 0; i < parent.drawArea.GraphicsList.Count; i++)
                    {
                        if (parent.drawArea.GraphicsList[i].GetType() == typeof(DrawInsks))
                            LstObjet.Add(parent.drawArea.GraphicsList[i]);
                    }
                    LoadNameSpace(LstObjet);
                    LoadNode(LstObjet);
                    break;
            }
        }

        public void LoadService_AliasInf(string sGuidVueSrvPhy)
        {
            ArrayList LstInfLink = new ArrayList();
            for (int i = 0; i < parent.drawArea.GraphicsList.Count; i++)
            {
                if (parent.drawArea.GraphicsList[i].GetType() == typeof(DrawInfLink)) LstInfLink.Add(parent.drawArea.GraphicsList[i]);
            }

            for (int i = 0; i < LstInfLink.Count; i++)
            {
                DrawInfLink dl = (DrawInfLink)LstInfLink[i];
                if (CBRecherche("SELECT NomAlias FROM NCardLinkIn, Alias, NCard, GNCard, Vue, DansVue WHERE GuidTechLink='" + dl.GuidkeyObjet + "' AND NCardLinkIn.GuidAlias=Alias.GuidAlias AND NCardLinkIn.GuidNCard=Alias.GuidNCard AND NCardLinkIn.GuidNCard=NCard.GuidNCard AND NCard.GuidNCard=GNCard.GuidNCard AND GNCard.GuidGNCard=DansVue.GuidObjet AND DansVue.GuidGVue=Vue.GuidGVue AND Vue.GuidVue='" + sGuidVueSrvPhy + "'"))
                {
                    string sAlias = "";
                    while (Reader.Read()) sAlias += "," + Reader.GetString(0);
                    dl.SetValueFromName("Alias", sAlias.Substring(1));
                }
                CBReaderClose();
                ArrayList lstVal = new ArrayList();
                lstVal = dl.GetValueEtCache((string)dl.GetValueFromName("NomGroupService"));
                if (lstVal.Count > 1 && lstVal[1] != null)
                {
                    if (CBRecherche("SELECT Ports FROM ServiceLink, Service, Environnement, Vue WHERE GuidGroupService='" + lstVal[1] + "' AND GuidVue='" + sGuidVueSrvPhy + "' AND ServiceLink.GuidService=Service.GuidService AND ServiceLink.GuidEnvironnement=Environnement.GuidEnvironnement AND Environnement.GuidEnvironnement=Vue.GuidEnvironnement"))
                    {
                        string sPorts = "";
                        while (Reader.Read()) sPorts += ";" + Reader.GetString(0);
                        dl.SetValueFromName("NomService", sPorts.Substring(1));
                    }
                    CBReaderClose();
                }
            }

        }

        public void LoadSousObjetsInf(string sGuidVueSrvPhy, int iTypeObjet)
        {

            ArrayList LstObjet = new ArrayList();
            switch (iTypeObjet)
            {
                case (int)DrawArea.DrawToolType.Server:
                    for (int i = 0; i < parent.drawArea.GraphicsList.Count; i++)
                    {
                        if (parent.drawArea.GraphicsList[i].GetType() == typeof(DrawServer)) LstObjet.Add(parent.drawArea.GraphicsList[i]);
                    }

                    LoadServerPhy_NCard(LstObjet, sGuidVueSrvPhy);
                    LoadMainComposant_ServMComp(LstObjet);

                    break;
                case (int)DrawArea.DrawToolType.Gensas:
                    for (int i = 0; i < parent.drawArea.GraphicsList.Count; i++)
                    {
                        if (parent.drawArea.GraphicsList[i].GetType() == typeof(DrawGensas)) LstObjet.Add(parent.drawArea.GraphicsList[i]);
                    }

                    LoadObjet_Label(LstObjet, sGuidVueSrvPhy);
                    //LoadMainComposant_ServMComp(LstObjet);

                    break;
            }

        }

        public void LoadMainComposant_UserMComp(ArrayList LstUser)
        {
            for (int i = 0; i < LstUser.Count; i++)
            {
                DrawTechUser dtu = (DrawTechUser)LstUser[i];
                if (dtu.GetNbrChild(typeof(DrawMainComposant)) == 0)
                {
                    ArrayList LstMainComposant = new ArrayList();
                    if (CBRecherche("SELECT GuidMainComposant FROM AppUserMComp WHERE GuidAppUser='" + dtu.GuidkeyObjet + "'"))
                        while (Reader.Read()) LstMainComposant.Add(Reader.GetString(0));
                    CBReaderClose();
                    for (int j = 0; j < LstMainComposant.Count; j++) dtu.LoadMainComposant_UserMComp((string)LstMainComposant[j]);
                    dtu.AligneObjet();
                }
            }
        }

        public void LoadMainComposant_ServMSas(ArrayList LstGensas)
        {
            for (int i = 0; i < LstGensas.Count; i++)
            {
                DrawGensas dgs = (DrawGensas)LstGensas[i];
                if (dgs.GetNbrChild(typeof(DrawMainComposant)) == 0)
                {
                    ArrayList LstMainComposant = new ArrayList();
                    if (CBRecherche("SELECT GuidMainComposant FROM MCompSas WHERE GuidGensas='" + dgs.GuidkeyObjet + "'"))
                        while (Reader.Read()) LstMainComposant.Add(Reader.GetString(0));
                    CBReaderClose();
                    for (int j = 0; j < LstMainComposant.Count; j++) dgs.LoadMainComposant_Serv((string)LstMainComposant[j]);
                    dgs.AligneObjet();
                }
            }
        }

        public void LoadMainComposant_ServMpod(ArrayList LstGenpod)
        {
            for (int i = 0; i < LstGenpod.Count; i++)
            {
                DrawGenpod dgp = (DrawGenpod)LstGenpod[i];
                if (dgp.GetNbrChild(typeof(DrawMainComposant)) == 0)
                {
                    ArrayList LstMainComposant = new ArrayList();
                    if (CBRecherche("SELECT GuidMainComposant FROM MComppod WHERE GuidGenpod='" + dgp.GuidkeyObjet + "'"))
                        while (Reader.Read()) LstMainComposant.Add(Reader.GetString(0));
                    CBReaderClose();
                    for (int j = 0; j < LstMainComposant.Count; j++) dgp.LoadMainComposant_Serv((string)LstMainComposant[j]);
                    dgp.AligneObjet();
                }
            }
        }

        public void LoadMainComposant_ServMComp(ArrayList LstServer)
        {
            for (int i = 0; i < LstServer.Count; i++)
            {
                DrawServer ds = (DrawServer)LstServer[i];
                if (ds.GetNbrChild(typeof(DrawMainComposant)) == 0)
                {
                    ArrayList LstMainComposant = new ArrayList();
                    if (CBRecherche("SELECT GuidMainComposant FROM MCompApp WHERE GuidServer='" + ds.GuidkeyObjet + "'"))
                        while (Reader.Read()) LstMainComposant.Add(Reader.GetString(0));
                    CBReaderClose();
                    for (int j = 0; j < LstMainComposant.Count; j++) ds.LoadMainComposant_Serv((string)LstMainComposant[j]);
                    ds.AligneObjet();
                }
            }
        }

        public void LoadUserType_Techno(ArrayList LstUser)
        {
            for (int i = 0; i < LstUser.Count; i++)
            {
                DrawTechUser dtu = (DrawTechUser)LstUser[i];
                if (dtu.GetNbrChild(typeof(DrawServerType)) == 0)
                {
                    ArrayList LstUserType = new ArrayList();
                    if (CBRecherche("SELECT GuidServerType FROM AppUserTypeLink WHERE GuidAppUser='" + dtu.GuidkeyObjet + "'"))
                        while (Reader.Read()) LstUserType.Add(Reader.GetString(0));
                    CBReaderClose();
                    for (int j = 0; j < LstUserType.Count; j++) dtu.LoadUserType_Techno((string)LstUserType[j]);
                    dtu.AligneObjet();
                }
            }
        }

        public void LoadNcard(ArrayList LstServerPhy)
        {
            for (int i = 0; i < LstServerPhy.Count; i++)
            {
                DrawServerPhy ds = (DrawServerPhy)LstServerPhy[i];
                if (ds.GetNbrChild(typeof(DrawNCard)) == 0)
                {
                    ArrayList LstNCard = new ArrayList();
                    if (CBRecherche("SELECT GuidNCard FROM NCard WHERE GuidHote='" + ds.GuidkeyObjet + "' Order by NomNCard"))
                        while (Reader.Read()) LstNCard.Add(Reader.GetString(0));
                    CBReaderClose();
                    for (int j = 0; j < LstNCard.Count; j++)
                        ds.LoadNCard((string)LstNCard[j]);
                    //ds.AligneObjet();
                }
            }
        }

        public void LoadNameSpace(ArrayList LstInsks)
        {
            for (int i = 0; i < LstInsks.Count; i++)
            {
                DrawInsks diks = (DrawInsks)LstInsks[i];

                ArrayList Lstns = new ArrayList();
                // avoir la liste instancier sur le ks par rapport à la vue
                if (CBRecherche("Select GuidInsns From Insns Where GuidVue = '" + parent.GuidVue + "' and GuidInsks = '" + diks.GetValueFromName("GuidInsks") + "'"))
                {
                    while (Reader.Read()) Lstns.Add(Reader.GetString(0));
                    CBReaderClose();
                    for (int j = 0; j < Lstns.Count; j++) diks.Loadns((string)Lstns[j]);
                }
                else
                {
                    CBReaderClose();
                    // Il n'existe pas d'instance namespace sur la vue
                    ToolInsns tins = (ToolInsns)parent.drawArea.tools[(int)DrawArea.DrawToolType.Insns];
                    tins.CreatObjetFromks(parent.drawArea, diks);


                }
                diks.AligneObjet();
            }
        }

        public void LoadNode(ArrayList LstInsks)
        {
            for (int i = 0; i < LstInsks.Count; i++)
            {
                // pour le node
                // avoir la liste instancier sur l'objet et non la vue
                DrawInsks diks = (DrawInsks)LstInsks[i];
                ArrayList Lstnd = new ArrayList();
                // avoir la liste des noeuds instanciés sur le ks 
                if (CBRecherche("Select GuidInsnd From Insnd Where GuidInsks = '" + diks.GetValueFromName("GuidInsks") + "'"))
                {
                    while (Reader.Read()) Lstnd.Add(Reader.GetString(0));
                    CBReaderClose();
                    for (int j = 0; j < Lstnd.Count; j++) diks.Loadnd((string)Lstnd[j]);
                }
                else
                {
                    CBReaderClose();
                    /*
                    // Il n'existe pas d'instance node sur le ks
                    ToolInsnd tind = (ToolInsnd)parent.drawArea.tools[(int)DrawArea.DrawToolType.Insnd];
                    tind.CreatObjetFromks(parent.drawArea, diks);
                    */
                }

                diks.AligneObjet();
            }
        }

        public void LoadServerPhy_Cluster(ArrayList LstCluster)
        {
            for (int i = 0; i < LstCluster.Count; i++)
            {
                DrawCluster dc = (DrawCluster)LstCluster[i];
                if (dc.GetNbrChild(typeof(DrawServerPhy)) == 0)
                {
                    ArrayList LstServerPhy = new ArrayList();
                    if (CBRecherche("SELECT GuidServerPhy FROM ServerPhy WHERE GuidCluster='" + dc.GuidkeyObjet + "' Order By NomServerPhy"))
                        while (Reader.Read()) LstServerPhy.Add(Reader.GetString(0));
                    CBReaderClose();
                    for (int j = 0; j < LstServerPhy.Count; j++)
                        dc.LoadServerPhy((string)LstServerPhy[j]);
                    dc.AligneObjet();
                }
            }
        }

        public void LoadServerType_Techno(ArrayList LstServer)
        {
            for (int i = 0; i < LstServer.Count; i++)
            {
                DrawServer ds = (DrawServer)LstServer[i];

                if (ds.GetNbrChild(typeof(DrawServerType)) == 0)
                {
                    ArrayList LstServerType = new ArrayList();
                    if (CBRecherche("SELECT GuidServerType FROM ServerTypeLink WHERE GuidServer='" + ds.GuidkeyObjet + "'"))
                        while (Reader.Read()) LstServerType.Add(Reader.GetString(0));
                    CBReaderClose();
                    //for (int j = 0; j < LstServerType.Count; j++) ds.LoadServerType_Techno((string)LstServerType[j]);
                    for (int j = 0; j < LstServerType.Count; j++) ds.LoadServerType((string)LstServerType[j]);
                    ds.AligneObjet();
                }
            }
        }

        public void LoadSvcServerType_Techno(ArrayList LstGensas)
        {
            for (int i = 0; i < LstGensas.Count; i++)
            {
                DrawGensas dgs = (DrawGensas)LstGensas[i];

                if (dgs.GetNbrChild(typeof(DrawServerType)) == 0)
                {
                    ArrayList LstServerType = new ArrayList();
                    if (CBRecherche("SELECT GuidServerType FROM SvcServerTypeLink WHERE GuidGensas='" + dgs.GuidkeyObjet + "'"))
                        while (Reader.Read()) LstServerType.Add(Reader.GetString(0));
                    CBReaderClose();
                    //for (int j = 0; j < LstServerType.Count; j++) ds.LoadServerType_Techno((string)LstServerType[j]);
                    for (int j = 0; j < LstServerType.Count; j++) dgs.LoadServerType((string)LstServerType[j]);
                    dgs.AligneObjet();
                }
            }
        }

        public void LoadContainer_Techno(ArrayList LstGenpod)
        {
            for (int i = 0; i < LstGenpod.Count; i++)
            {
                DrawGenpod dgp = (DrawGenpod)LstGenpod[i];
                if (dgp.GetNbrChild(typeof(DrawContainer)) == 0)
                {
                    ArrayList LstContainer = new ArrayList();
                    if (CBRecherche("SELECT GuidContainer FROM ContainerLink WHERE GuidGenpod='" + dgp.GuidkeyObjet + "'"))
                        while (Reader.Read()) LstContainer.Add(Reader.GetString(0));
                    CBReaderClose();
                    for (int j = 0; j < LstContainer.Count; j++) dgp.LoadContainer_Techno((string)LstContainer[j]);
                    dgp.AligneObjet();
                }
            }
        }

        public void LoadManagedsvc_Techno(ArrayList LstGensas)
        {
            for (int i = 0; i < LstGensas.Count; i++)
            {
                DrawGensas dgs = (DrawGensas)LstGensas[i];
                if (dgs.GetNbrChild(typeof(DrawManagedsvc)) == 0)
                {
                    ArrayList LstManagedsvc = new ArrayList();
                    if (CBRecherche("SELECT GuidManagedsvc FROM ManagedsvcLink WHERE GuidGensas='" + dgs.GuidkeyObjet + "'"))
                        while (Reader.Read()) LstManagedsvc.Add(Reader.GetString(0));
                    CBReaderClose();
                    for (int j = 0; j < LstManagedsvc.Count; j++) dgs.LoadManagedsvc_Techno((string)LstManagedsvc[j]);
                    dgs.AligneObjet();
                }
            }
        }

        public void LoadObjet_Label(ArrayList LstGenObjet, string sGuidVueSrvPhy)
        {
            for (int i = 0; i < LstGenObjet.Count; i++)
            {
                DrawGensas dobj = (DrawGensas)LstGenObjet[i];
                ArrayList LstInsobj = new ArrayList();


                if (CBRecherche("select distinct Inssas.GuidInssas, NomInssas from Inssas, GInssas, DansVue, Vue, Gensas where Inssas.GuidInssas = GInssas.GuidInssas and GInssas.GuidGInssas = DansVue.GuidObjet and DansVue.GuidGVue = Vue.GuidGVue and Vue.GuidVue = '" + sGuidVueSrvPhy + "' and Inssas. GuidGensas = '" + dobj.GuidkeyObjet + "'"))
                    while (Reader.Read()) LstInsobj.Add(Reader.GetString(0));
                CBReaderClose();
                for (int j = 0; j < LstInsobj.Count; j++) dobj.LoadInsobj_Label((string)LstInsobj[j]);
                dobj.AligneObjet();
            }
        }

        public void LoadServerPhy_NCard(ArrayList LstServer, string sGuidVueSrvPhy)
        {
            for (int i = 0; i < LstServer.Count; i++)
            {
                DrawServer ds = (DrawServer)LstServer[i];
                ArrayList LstServerPhy = new ArrayList();
                if (CBRecherche("SELECT GuidServerPhy FROM ServerLink WHERE GuidServer='" + ds.GuidkeyObjet + "' AND GuidVue='" + sGuidVueSrvPhy + "'"))
                    while (Reader.Read()) LstServerPhy.Add(Reader.GetString(0));
                CBReaderClose();
                for (int j = 0; j < LstServerPhy.Count; j++) ds.LoadServerPhy_NCard((string)LstServerPhy[j]);
                ds.AligneObjet();
            }
        }

        /*public void RestoreLink()
        {
            for (int i = 0; i < parent.drawArea.GraphicsList.Count; i++)
            {
                if (parent.drawArea.GraphicsList[i].GetType() == typeof(DrawNCard))
                {
                    DrawNCard dnc = (DrawNCard)parent.drawArea.GraphicsList[i];
                    dnc.saveLinkAlias();
                }
            }
        }*/

        public void LoadTechLinkApp()
        {
            string sQuery = "SELECT TechLinkApp.GuidTechLink, TechLinkApp.GuidLink, NomLink FROM DansVue, GTechLink, TechLinkApp, Link WHERE GuidGVue ='" + parent.GuidGVue + "' AND GuidObjet=GuidGTechLink AND GTechLink.GuidTechLink=TechLinkApp.GuidTechLink AND TechLinkApp.GuidLink=Link.GuidLink ORDER BY TechLinkApp.GuidTechLink";

            if (CBRecherche(sQuery))
            {
                DrawTechLink dl = null;
                string Link = "";
                int n;

                while (Reader.Read())
                {
                    if (dl != null && dl.GuidkeyObjet.ToString() == Reader.GetString(0))
                        Link += ";" + Reader.GetString(2) + "  (" + Reader.GetString(1) + ")";
                    else
                    {
                        if (dl != null) dl.InitProp("LinksApplicatifs", (object)Link, true);

                        n = parent.drawArea.GraphicsList.FindObjet(0, Reader.GetString(0));
                        if (n > -1)
                        {
                            dl = (DrawTechLink)parent.drawArea.GraphicsList[n];
                            Link = Reader.GetString(2) + "  (" + Reader.GetString(1) + ")";
                        }
                    }
                }
                if (dl != null) dl.InitProp("LinksApplicatifs", (object)Link, true);
                CBReaderClose();
            }
            else CBReaderClose();
        }

        public void LoadAlias()
        {
            string sQuery = "SELECT Distinct NCard.GuidNCard, GuidAlias, NomAlias FROM DansVue, GNCard, NCard, Alias WHERE GuidGVue ='" + parent.GuidGVue + "' AND GuidObjet=GuidGNCard AND GNCard.GuidNCard=NCard.GuidNCard AND NCard.GuidNCard=Alias.GuidNCard ORDER BY NCard.GuidNCard";
            if (CBRecherche(sQuery))
            {
                DrawNCard dnc = null;
                string Alias = "";
                int n;

                while (Reader.Read())
                {
                    if (dnc != null && dnc.GuidkeyObjet.ToString() == Reader.GetString(0))
                        Alias += ";" + Reader.GetString(2) + "  (" + Reader.GetString(1) + ")";
                    else
                    {
                        if (dnc != null) dnc.InitProp("Alias", (object)Alias, true);

                        n = parent.drawArea.GraphicsList.FindObjet(0, Reader.GetString(0));
                        if (n > -1)
                        {
                            dnc = (DrawNCard)parent.drawArea.GraphicsList[n];
                            Alias = Reader.GetString(2) + "  (" + Reader.GetString(1) + ")";
                        }
                    }
                }
                if (dnc != null) dnc.InitProp("Alias", (object)Alias, true);
                CBReaderClose();
            }
            else CBReaderClose();
        }

        public void LoadNCardLinkIn()
        {
            string sQuery = "SELECT Distinct NCardLinkIn.GuidNCard, NCardLinkIn.GuidTechLink, NomTechLink, GuidAlias FROM DansVue, GNCard, NCardLinkIn, TechLink WHERE GuidGVue ='" + parent.GuidGVue + "' AND GuidObjet=GuidGNCard AND GNCard.GuidNCard=NCardLinkIn.GuidNCard AND NCardLinkIn.GuidTechLink=TechLink.GuidTechLink ";
            sQuery += "AND TechLink.GuidTechLink IN (Select GuidTechLink FROM Vue, DansVue, GTechLink Where Vue.GuidVue='" + parent.sGuidVueInf + "' AND Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGTechLink)  ORDER BY NCardLinkIn.GuidNCard";
            if (CBRecherche(sQuery))
            {
                DrawNCard dnc = null;
                string Link = "";
                int n;

                while (Reader.Read())
                {
                    if (dnc != null && dnc.GuidkeyObjet.ToString() == Reader.GetString(0))
                        if (Reader.IsDBNull(3)) Link += ";" + Reader.GetString(2) + "  (" + Reader.GetString(1) + ")";
                        else Link += ";" + Reader.GetString(2) + "  (" + Reader.GetString(1) + ":" + Reader.GetString(3) + ")";
                    else
                    {
                        if (dnc != null) dnc.InitProp("FluxIn", (object)Link, true);

                        n = parent.drawArea.GraphicsList.FindObjet(0, Reader.GetString(0));
                        if (n > -1)
                        {
                            dnc = (DrawNCard)parent.drawArea.GraphicsList[n];
                            if (Reader.IsDBNull(3)) Link = Reader.GetString(2) + "  (" + Reader.GetString(1) + ")";
                            else Link = Reader.GetString(2) + "  (" + Reader.GetString(1) + ":" + Reader.GetString(3) + ")";
                        }
                    }
                }
                if (dnc != null) dnc.InitProp("FluxIn", (object)Link, true);
                CBReaderClose();
            }
            else CBReaderClose();
        }


        public void LoadNCardLink(string Sens)
        {
            string sQuery = "SELECT Distinct NCardLink" + Sens + ".GuidNCard, NCardLink" + Sens + ".GuidTechLink, NomTechLink FROM DansVue, GNCard, NCardLink" + Sens + ", TechLink WHERE GuidGVue ='" + parent.GuidGVue + "' AND GuidObjet=GuidGNCard AND GNCard.GuidNCard=NCardLink" + Sens + ".GuidNCard AND NCardLink" + Sens + ".GuidTechLink=TechLink.GuidTechLink ";
            sQuery += "AND TechLink.GuidTechLink IN (Select GuidTechLink FROM Vue, DansVue, GTechLink Where Vue.GuidVue='" + parent.sGuidVueInf + "' AND Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGTechLink) ORDER BY NCardLink" + Sens + ".GuidNCard";

            //string sQuery = "SELECT NomTechLink FROM DansVue, GTechLink, TechLink WHERE GuidVue ='" + parent.GuidVue + "' AND GuidObjet=GuidGTechLink AND GTechLink.GuidTechLink=TechLink.GuidTechLink";
            //string sQuery = "SELECT NCardLink" + Sens + ".GuidNCard, NCardLink" + Sens + ".GuidTechLink, NomTechLink FROM DansVue, GTechLink, TechLink, NCardLink" + Sens + " WHERE GuidVue ='" + parent.GuidVue + "' AND GuidObjet=GuidGTechLink AND GTechLink.GuidTechLink=TechLink.GuidTechLink  AND NCardLink" + Sens + ".GuidTechLink=GTechLink.GuidTechLink ORDER BY NCardLink" + Sens + ".GuidNCard";

            if (CBRecherche(sQuery))
            {
                DrawNCard dnc = null;
                string Link = new string(' ', 0);
                int n;

                while (Reader.Read())
                {
                    if (dnc != null && dnc.GuidkeyObjet.ToString() == Reader.GetString(0))
                        Link += ";" + Reader.GetString(2) + "  (" + Reader.GetString(1) + ")";
                    else
                    {
                        if (dnc != null) dnc.InitProp("Flux" + Sens, (object)Link, true);

                        n = parent.drawArea.GraphicsList.FindObjet(0, Reader.GetString(0));
                        if (n > -1)
                        {
                            dnc = (DrawNCard)parent.drawArea.GraphicsList[n];
                            Link = new string(' ', 0);
                            Link = Reader.GetString(2) + "  (" + Reader.GetString(1) + ")";
                        }
                    }
                }
                if (dnc != null) dnc.InitProp("Flux" + Sens, (object)Link, true);
                CBReaderClose();
            }
            else CBReaderClose();
        }

        public int LinkPointVlanWithCard(DrawVLan dvl, DrawObject oLink, int iTopY, int iBottomY)
        {
            int Hauteur = 0;
            ArrayList lstIndexPointVlanX = new ArrayList();

            for (int j = 0; j < dvl.pointArray.Count; j++)
            {
                int x = ((Point)dvl.pointArray[j]).X;
                if (x >= oLink.XMin() && x <= oLink.XMax()) lstIndexPointVlanX.Add(j);
            }

            //int iTopY = oParent.GetTopYNCard(), iBottomY = oParent.GetBottomYNCard();
            int index = -1, iCard = iTopY, deltaMax = 10, delta = deltaMax;
            for (int j = 0; j < lstIndexPointVlanX.Count; j++)
            {
                delta = Math.Abs((((Point)dvl.pointArray[(int)lstIndexPointVlanX[j]]).Y - iTopY));
                if (delta < deltaMax)
                {
                    if (index == -1) index = (int)lstIndexPointVlanX[j];

                    if (Math.Abs((((Point)dvl.pointArray[index]).Y - iCard)) > delta)
                    {
                        index = (int)lstIndexPointVlanX[j]; iCard = iTopY;
                    }
                }
                delta = Math.Abs((((Point)dvl.pointArray[(int)lstIndexPointVlanX[j]]).Y - iBottomY));
                if (delta < deltaMax)
                {
                    if (index == -1) index = (int)lstIndexPointVlanX[j];

                    if (Math.Abs((((Point)dvl.pointArray[index]).Y - iCard)) > delta)
                    {
                        index = (int)lstIndexPointVlanX[j]; iCard = iBottomY;
                    }
                }
            }
            if (index != -1 && dvl.pointArray.Count > dvl.LstLinkOut.Count)
            {
                int indexFromPointArray = dvl.LstLinkOut.Count;
                oLink.AttachLink(dvl, DrawObject.TypeAttach.Entree);
                dvl.AttachLink(oLink, DrawObject.TypeAttach.Sortie);

                Point pt = (Point)dvl.pointArray[index];


                if (iCard == iTopY)
                {
                    pt.Y = iTopY;
                }
                else
                {
                    Hauteur = 1;
                    pt.Y = iBottomY;
                }
                if (index == indexFromPointArray) dvl.pointArray[index] = pt;
                else
                {
                    Point ptTemp = (Point)dvl.pointArray[indexFromPointArray];
                    dvl.pointArray[indexFromPointArray] = pt;
                    dvl.pointArray[index] = ptTemp;
                }
            }
            return Hauteur;
        }

        public void LoadRouterLink()
        {

            string sQuery = "SELECT RouterLink.GuidRouter, RouterLink.GuidVLan FROM DansVue, GRouter, RouterLink WHERE GuidGVue ='" + parent.GuidGVue + "' AND GuidObjet=GuidGRouter AND GRouter.GuidRouter=RouterLink.GuidRouter ORDER BY RouterLink.GuidRouter";

            if (CBRecherche(sQuery))
            {
                DrawRouter dr = null;
                int n;

                while (Reader.Read())
                {
                    if (dr != null && dr.GuidkeyObjet.ToString() == Reader.GetString(0))
                    {
                        n = parent.drawArea.GraphicsList.FindObjet(0, Reader.GetString(1));
                        if (n > -1)
                        {
                            DrawVLan dv = (DrawVLan)parent.drawArea.GraphicsList[n];
                            LinkPointVlanWithCard(dv, dr, dr.YMin(), dr.YMax());
                        }
                    }
                    else
                    {
                        n = parent.drawArea.GraphicsList.FindObjet(0, Reader.GetString(0));
                        if (n > -1)
                        {
                            dr = (DrawRouter)parent.drawArea.GraphicsList[n];

                            n = parent.drawArea.GraphicsList.FindObjet(0, Reader.GetString(1));
                            if (n > -1)
                            {
                                DrawVLan dv = (DrawVLan)parent.drawArea.GraphicsList[n];
                                LinkPointVlanWithCard(dv, dr, dr.YMin(), dr.YMax());
                            }
                        }
                    }
                }
                CBReaderClose();
            }
            else CBReaderClose();
        }

        public string CreatObjectString(DrawObject o)
        {
            string sType = o.GetTypeSimpleTable();
            string RequeteField = "INSERT INTO " + o.GetTable(sType) + " (";
            string RequeteValue = "VALUES (";

            Table t;
            int n = ConfDB.FindTable(sType);
            if (n > -1)
            {
                t = (Table)ConfDB.LstTable[n];
                bool InsField = false;
                for (int i = 0; i < t.LstField.Count; i++)
                {
                    if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.InterneBD) != 0)
                    {
                        switch (((Field)t.LstField[i]).Type)
                        {
                            case 's':
                                if ((string)o.LstValue[i] != "")
                                {
                                    if (InsField) { RequeteField += ", "; RequeteValue += ", "; }
                                    RequeteField += ((Field)t.LstField[i]).Name;
                                    RequeteValue += "'" + o.LstValue[i] + "'";
                                    InsField = true;
                                }
                                break;
                            case 'p': //picture
                            case 'q': //picture
                            case 'i':
                                //if ((int)o.LstValue[i] != 0)
                                //{
                                if (InsField) { RequeteField += ", "; RequeteValue += ", "; }
                                RequeteField += ((Field)t.LstField[i]).Name;
                                RequeteValue += o.LstValue[i];
                                InsField = true;
                                //}
                                break;
                            case 'd':
                                if ((double)o.LstValue[i] != 0)
                                {
                                    if (InsField) { RequeteField += ", "; RequeteValue += ", "; }
                                    RequeteField += ((Field)t.LstField[i]).Name;
                                    RequeteValue += o.LstValue[i];
                                    InsField = true;
                                }
                                break;
                            case 't':
                                //if (o.LstValue[i] != null && o.LstValue[i].ToString() != "" && (DateTime)o.LstValue[i] != DateTime.MinValue)
                                if (o.LstValue[i] != null && o.LstValue[i].ToString() != "")
                                {
                                    DateTime dt = (DateTime)o.LstValue[i];
                                    if (InsField) { RequeteField += ", "; RequeteValue += ", "; }
                                    RequeteField += ((Field)t.LstField[i]).Name;
                                    RequeteValue += "'" + dt.ToString("yyyy-MM-dd") + "'";
                                    InsField = true;
                                }
                                break;
                        }
                    }
                }
                RequeteField += ") ";
                RequeteValue += ")";
            }

            return (RequeteField + RequeteValue);
        }

        public void CreatObject(DrawObject o)
        {
            CBWrite(CreatObjectString(o));
        }

        public XmlElement GetElFromInnerXml(XmlElement parent, string sInnerXmlSearch)
        {
            IEnumerator ienum = parent.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                if (Node.NodeType == XmlNodeType.Text)
                    if (Node.InnerText == sInnerXmlSearch) return parent;
                if (Node.NodeType == XmlNodeType.Element)
                {
                    XmlElement elss = GetElFromInnerXml((XmlElement)Node, sInnerXmlSearch);
                    if (elss != null) return elss;
                }
            }
            return null;
        }


        public void CreaXmlApplication(XmlDB xmlDB, XmlElement elApp, string sGuidApplication, string sGuidAppVersion, List<string[]> lstVue, ArrayList lstLayer = null)
        {
            //XmlElement elApp = xmlDB.XmlGetElFromInnerText(xmlDB.root, sGuidApp);
            XmlElement elAppAfter = xmlDB.XmlGetFirstElFromParent(elApp, "After");
            for (int i = 0; i < lstVue.Count; i++)
            {
                string[] aEnregVue = lstVue[i]; //GuidVue, NomVue, GuidGVue, GuidEnvironnement, GuidVueInf, GuidTypeVue, NomTypeVue
                if (xmlDB.SetCursor(elAppAfter))
                {
                    string sGuidEnvironnement = aEnregVue[3];
                    if (aEnregVue[4] != "") parent.sGuidVueInf = (new Guid(aEnregVue[4])).ToString(); else parent.sGuidVueInf = null;

                    //parent.GuidApplication = new Guid(sGuidApp);
                    parent.wkApp = new WorkApplication(parent, sGuidApplication, null, sGuidAppVersion);

                    parent.ClearVue(true);

                    parent.GuidVue = new Guid(aEnregVue[0]);
                    parent.cbGuidVue.Items.Add(parent.GuidVue); parent.cbGuidVue.SelectedIndex = 0; // GuidVue
                    parent.cbVue.Items.Add(aEnregVue[1]); parent.cbVue.SelectedIndex = 0; //NomVue
                    parent.GuidGVue = new Guid(aEnregVue[2]); //GuidGVue
                    parent.tbTypeVue.Text = aEnregVue[6]; // NomTypeVue
                    parent.sTypeVue = aEnregVue[6]; // NomTypeVue
                    parent.GuidTypeVue = new Guid(aEnregVue[5]); // GuidTypeVue

                    parent.oCureo = new ExpObj(parent.GuidVue, (string)parent.cbVue.SelectedItem, DrawArea.DrawToolType.Vue);
                    parent.drawArea.tools[(int)parent.oCureo.ObjTool].LoadSimpleObjectSansGraph(parent.oCureo);
                    if (parent.oCureo.oDraw != null)
                    {
                        //Création de la Vue
                        DrawVue dv = (DrawVue)parent.oCureo.oDraw;
                        XmlElement elVue = dv.savetoXml(xmlDB, true);
                        parent.oCureo = null;
                        //parent.drawArea.GraphicsList[j];

                        //XmlElement elVue = xmlDB.XmlCreatEl(xmlDB.GetCursor(), "Vue", "NomVue,GuidTypeVue,GuidApplication", fields[0]);
                        //XmlElement elAtts = xmlDB.XmlGetFirstElFromParent(elVue, "Attributs");
                        //xmlDB.XmlSetAttFromEl(elAtts, "GuidVue", "s", fields[0]);
                        //xmlDB.XmlSetAttFromEl(elAtts, "NomVue", "s", fields[1]);
                        //if (sGuidEnvironnement != "") xmlDB.XmlSetAttFromEl(elAtts, "GuidEnvironnement", "s", sGuidEnvironnement);
                        //if (parent.sGuidVueInf != null) xmlDB.XmlSetAttFromEl(elAtts, "GuidVueInf", "s", parent.sGuidVueInf);
                        //xmlDB.XmlSetAttFromEl(elAtts, "GuidTypeVue", "s", fields[4]);
                        //xmlDB.XmlSetAttFromEl(elAtts, "GuidApplication", "s", sGuidApp);

                        //xmlDB.XmlCreatExternRef(xmlDB.XmlGetFirstElFromParent(elVue, "Before"), ConfDB.FindTable("Environnement"), sGuidEnvironnement);
                        //xmlDB.XmlCreatComment(elVue, fields[0]);

                        xmlDB.CursorClose();

                        parent.LoadVue();
                        int nbrObj = parent.drawArea.GraphicsList.Count;
                        DrawObject obj = null;
                        for (int j = 0; j < nbrObj; j++)
                        {
                            obj = parent.drawArea.GraphicsList[j];
                            xmlDB.CursorClose();
                            if (xmlDB.SetCursor(xmlDB.XmlGetFirstElFromParent(elVue, "After"))) obj.savetoXml(xmlDB, true);
                            xmlDB.CursorClose();
                        }
                    }
                    parent.drawArea.GraphicsList.Clear();
                }
            }
            // Création des layers à la suite des vue
            if (lstLayer != null)
            {
                ConfDB.AddLayer();
                ConfDB.AddLayerLink();

                for (int i = 0; i < lstLayer.Count; i++)
                {
                    //XmlElement elApp = xmlDB.XmlGetElFromInnerText(xmlDB.root, sGuidApp);
                    if (xmlDB.SetCursor(elAppAfter))
                    {

                        string[] fields = ((string)lstLayer[i]).Split(','); //GuidLayer, NomLayer
                        parent.oCureo = new ExpObj(new Guid(fields[0]), fields[1], DrawArea.DrawToolType.Layer);
                        parent.drawArea.tools[(int)parent.oCureo.ObjTool].LoadSimpleObjectSansGraph(parent.oCureo);

                        DrawLayer dl = (DrawLayer)parent.oCureo.oDraw;
                        XmlElement elLayer = dl.savetoXml(xmlDB, true);
                        parent.oCureo = null;

                        xmlDB.CursorClose();
                        parent.drawArea.GraphicsList.Clear();

                        if (xmlDB.SetCursor(xmlDB.XmlGetFirstElFromParent(elLayer, "After")))
                        {

                            parent.drawArea.tools[(int)DrawArea.DrawToolType.LayerLink].LoadObjectSansGraph("Where GuidLayer = '" + fields[0] + "'");
                            for (int j = 0; j < parent.drawArea.GraphicsList.Count; j++)
                            {
                                DrawLayerLink dll = (DrawLayerLink)parent.drawArea.GraphicsList[j];
                                dll.savetoXml(xmlDB, true);
                            }

                            parent.drawArea.GraphicsList.Clear();
                            xmlDB.CursorClose();
                        }
                    }
                }
            }
        }

        public void CreatDansVue(Guid GuidObjet, string TypeObjet)
        {
            CBWrite("INSERT INTO DansVue (GuidGVue, GuidObjet, TypeObjet) VALUES ('" + parent.GuidGVue + "','" + GuidObjet + "','" + TypeObjet + "')");
        }

        public void CreatGLink(DrawPolygon dl)
        {
            string sType = dl.GetTypeSimpleTable();

            CBWrite("INSERT INTO G" + sType + " (GuidG" + sType + ", Guid" + sType + ", Pos) VALUES ('" + dl.Guidkey + "','" + dl.GuidkeyObjet + "'," + (int)dl.GetValueFromName("Pos") + ")");

            //Table GPoint
            for (int i = 0; i < dl.pointArray.Count; i++)
            {
                CBWrite("INSERT INTO GPoint (GuidGObjet, I, X, Y) VALUES ('" + dl.Guidkey + "','" + i + "','" + ((Point)dl.pointArray[i]).X + "','" + ((Point)dl.pointArray[i]).Y + "')");
            }
        }

        /*
        public void CreatGLink(DrawLink dl)
        {
            CBWrite("INSERT INTO GLink (GuidGLink, GuidLink, Pos) VALUES ('" + dl.Guidkey + "','" + dl.GuidkeyObjet + "',"  + (int) dl.GetValueFromName("Pos") + ")");
                    
            //Table GPoint
            for (int i = 0; i < dl.pointArray.Count; i++)
            {
                CBWrite("INSERT INTO GPoint (GuidGObjet, I, X, Y) VALUES ('" + dl.Guidkey + "','" + i + "','" + ((Point)dl.pointArray[i]).X + "','" + ((Point)dl.pointArray[i]).Y + "')");
            }
        }

        public void CreatGTechLink(DrawTechLink dl)
        {
            CBWrite("INSERT INTO GTechLink (GuidGTechLink, GuidTechLink, Pos) VALUES ('" + dl.Guidkey + "','" + dl.GuidkeyObjet + "'," + (int)dl.GetValueFromName("Pos") + ")");
            
            //Table GPoint
            for (int i = 0; i < dl.pointArray.Count; i++)
            {
                CBWrite("INSERT INTO GPoint (GuidGObjet, I, X, Y) VALUES ('" + dl.Guidkey + "','" + i + "','" + ((Point)dl.pointArray[i]).X + "','" + ((Point)dl.pointArray[i]).Y + "')");
            }
        }

        public void CreatGInterLink(DrawInterLink dl)
        {
            CBWrite("INSERT INTO GInterLink (GuidGInterLink, GuidInterLink, Pos) VALUES ('" + dl.Guidkey + "','" + dl.GuidkeyObjet + "'," + (int)dl.GetValueFromName("Pos") + ")");

            //Table GPoint
            for (int i = 0; i < dl.pointArray.Count; i++)
            {
                CBWrite("INSERT INTO GPoint (GuidGObjet, I, X, Y) VALUES ('" + dl.Guidkey + "','" + i + "','" + ((Point)dl.pointArray[i]).X + "','" + ((Point)dl.pointArray[i]).Y + "')");
            }
        }
        */
        public void CreatGZone(DrawZone dz)
        {
            CBWrite("INSERT INTO GZone (GuidGZone, GuidZone, Pos) VALUES ('" + dz.Guidkey + "','" + dz.GuidkeyObjet + "'," + (int)dz.GetValueFromName("Pos") + ")");

            //Table GPoint
            for (int i = 0; i < dz.pointArray.Count; i++)
            {
                CBWrite("INSERT INTO GPoint (GuidGObjet, I, X, Y) VALUES ('" + dz.Guidkey + "','" + i + "','" + ((Point)dz.pointArray[i]).X + "','" + ((Point)dz.pointArray[i]).Y + "')");
            }
        }

        public void CreatGObjectRect(DrawRectangle o)
        {
            string sType = o.GetTypeSimpleTable();
            //string sTypeF = o.GetsType(false);
            string sTypeG = o.GetTypeSimpleGTable();
            if (sTypeG == "GServerPhy")
            {
                //CBWrite("INSERT INTO " + sTypeG + " (Guid" + sTypeG + ", Guid" + sType + ", X, Y, Width, Height, Forme, thickColor, CPUCoreA, RAMA) VALUES ('" + o.Guidkey + "','" + o.GuidkeyObjet + "','" + o.Rectangle.X + "','" + o.Rectangle.Y + "','" + o.Rectangle.Width + "','" + o.Rectangle.Height + "','" + (int)o.GetValueFromName("Forme") + "','" + (int)o.GetValueFromName("thickColor") + "','" + (double)o.GetValueFromName("CPUCoreA") + "','" + (double)o.GetValueFromName("RAMA") + "')");
                CBWrite("INSERT INTO " + sTypeG + " (Guid" + sTypeG + ", Guid" + sType + ", X, Y, Width, Height, Forme, thickColor) VALUES ('" + o.Guidkey + "','" + o.GuidkeyObjet + "','" + o.Rectangle.X + "','" + o.Rectangle.Y + "','" + o.Rectangle.Width + "','" + o.Rectangle.Height + "','" + (int)o.GetValueFromName("Forme") + "','" + (int)o.GetValueFromName("thickColor") + "')");
            }
            else CBWrite("INSERT INTO " + sTypeG + " (Guid" + sTypeG + ", Guid" + sType + ", X, Y, Width, Height) VALUES ('" + o.Guidkey + "','" + o.GuidkeyObjet + "','" + o.Rectangle.X + "','" + o.Rectangle.Y + "','" + o.Rectangle.Width + "','" + o.Rectangle.Height + "')");
        }


        /*
        public void CreatGServMComp(DrawServMComp o)
        {
            string sTypeT = o.GetsType(true);
            string sTypeF = o.GetsType(false);
            CBWrite("INSERT INTO G" + sTypeT + " (GuidG" + sTypeT + ", Guid" + sTypeF + ", GuidServeur, X, Y, Width, Height) VALUES ('" + o.Guidkey + "','" + o.GuidkeyObjet + "','" + ((DrawServer) o.LstParent[0]).GuidkeyObjet + "','" + o.Rectangle.X + "','" + o.Rectangle.Y + "','" + o.Rectangle.Width + "','" + o.Rectangle.Height + "')");
        }
        */

        public void CreatGVLanPoint(DrawVLan dv)
        {
            //Table GPoint
            for (int i = 0; i < dv.pointArray.Count; i++)
            {
                if (!CBRecherche("Select GuidGObjet FROM GPoint WHERE GuidGObjet='" + dv.Guidkey + "' AND I=" + i))
                {
                    CBReaderClose();
                    CBWrite("INSERT INTO GPoint (GuidGObjet, I, X, Y) VALUES ('" + dv.Guidkey + "','" + i + "','" + ((Point)dv.pointArray[i]).X + "','" + ((Point)dv.pointArray[i]).Y + "')");
                }
                else CBReaderClose();
            }
        }

        public void CreatGSanSwitchPoint(DrawSanSwitch dss)
        {
            //Table GPoint
            for (int i = 0; i < dss.pointArray.Count; i++)
            {
                if (!CBRecherche("Select GuidGObjet FROM GPoint WHERE GuidGObjet='" + dss.Guidkey + "' AND I=" + i))
                {
                    CBReaderClose();
                    CBWrite("INSERT INTO GPoint (GuidGObjet, I, X, Y) VALUES ('" + dss.Guidkey + "','" + i + "','" + ((Point)dss.pointArray[i]).X + "','" + ((Point)dss.pointArray[i]).Y + "')");
                }
                else CBReaderClose();
            }
        }

        public void CreatGCnxPoint(DrawCnx dc)
        {
            //Table GPoint
            for (int i = 0; i < dc.pointArray.Count; i++)
            {
                if (!CBRecherche("Select GuidGObjet FROM GPoint WHERE GuidGObjet='" + dc.Guidkey + "' AND I=" + i))
                {
                    CBReaderClose();
                    CBWrite("INSERT INTO GPoint (GuidGObjet, I, X, Y) VALUES ('" + dc.Guidkey + "','" + i + "','" + ((Point)dc.pointArray[i]).X + "','" + ((Point)dc.pointArray[i]).Y + "')");
                }
                else CBReaderClose();
            }
        }

        public string GetSelectSearchKey(string[] Keys)
        {
            string sSelect = "";
            int index = 1;
            // si suppression battribut -> supprimer le substrings(index)
            if (bAttribut) index = 0;
            for (int i = 0; i < Keys.Length; i++)
            {
                sSelect += "," + Keys[i].Substring(index);
            }

            return sSelect.Substring(1);
        }

        public string XmlGetWhereSearchKey(XmlElement el, string[] Keys)
        {
            string sSelect = "";
            for (int i = 0; i < Keys.Length; i++)
            {
                if (bAttribut)
                {
                    XmlElement elAtts = parent.XmlFindFirstElFromName(el, "Attributs", 1);
                    if (elAtts != null)
                    {
                        switch (parent.XmlGetAttValueAFromAttValueB(elAtts, "Type", "Nom", Keys[i])[0])
                        {
                            case 's':
                                sSelect += " AND " + Keys[i] + "='" + parent.XmlGetAttValueAFromAttValueB(elAtts, "Value", "Nom", Keys[i]) + "'";
                                break;
                            case 'i':
                                sSelect += " AND " + Keys[i] + "=" + parent.XmlGetAttValueAFromAttValueB(elAtts, "Value", "Nom", Keys[i]);
                                break;
                            case 'd':
                                sSelect += " AND " + Keys[i] + "=" + parent.XmlGetAttValueAFromAttValueB(elAtts, "Value", "Nom", Keys[i]);
                                break;
                        }
                    }
                }
                else
                {
                    switch (Keys[i][0])
                    {
                        case 's':
                            sSelect += " AND " + Keys[i].Substring(1) + "='" + el.GetAttribute(Keys[i]) + "'";
                            break;
                        case 'i':
                            sSelect += " AND " + Keys[i].Substring(1) + "=" + el.GetAttribute(Keys[i]);
                            break;
                        case 'd':
                            sSelect += " AND " + Keys[i].Substring(1) + "=" + el.GetAttribute(Keys[i]);
                            break;
                    }
                }
            }
            return sSelect.Substring(5);
        }

        public void XmlUpdateFromXml(XmlElement el, string[] Keys)
        {
            string cmdUpdate = "UPDATE " + el.Name + " SET ";
            string setval = "";
            byte[] rawData = null;

            if (bAttribut)
            {
                XmlElement elAtts = parent.XmlFindFirstElFromName(el, "Attributs", 1);
                if (elAtts != null)
                {
                    IEnumerator ienum = elAtts.GetEnumerator();
                    XmlNode Node;

                    while (ienum.MoveNext())
                    {
                        Node = (XmlNode)ienum.Current;
                        if (Node.NodeType == XmlNodeType.Element)
                        {
                            XmlElement elAtt = (XmlElement)Node;
                            if (elAtt.Name == "Attribut")
                            {
                                switch (elAtt.GetAttribute("Type")[0])
                                {
                                    case 's':
                                        if (GetSelectSearchKey(Keys).IndexOf(elAtt.GetAttribute("Nom")) < 0) setval += ", " + elAtt.GetAttribute("Nom") + "='" + elAtt.GetAttribute("Value") + "'";
                                        break;
                                    case 'i':
                                        if (GetSelectSearchKey(Keys).IndexOf(elAtt.GetAttribute("Nom")) < 0) setval += ", " + elAtt.GetAttribute("Nom") + "=" + elAtt.GetAttribute("Value");
                                        break;
                                    case 'd':
                                        if (GetSelectSearchKey(Keys).IndexOf(elAtt.GetAttribute("Nom")) < 0) setval += ", " + elAtt.GetAttribute("Nom") + "=" + elAtt.GetAttribute("Value");
                                        break;
                                    case 'b':
                                        if (GetSelectSearchKey(Keys).IndexOf(elAtt.GetAttribute("Nom")) < 0 && rawData == null)
                                        {
                                            setval += ", " + elAtt.GetAttribute("Nom") + "= ?";
                                            rawData = StringToByteArray(elAtt.GetAttribute("Value"));
                                        }
                                        break;
                                }

                            }
                        }
                    }
                }
            }
            else
            {
                XmlAttributeCollection lstAtt = el.Attributes;
                XmlAttribute att = lstAtt[0];

                for (int i = 1; i < lstAtt.Count; i++)
                {
                    switch (lstAtt[i].Name[0])
                    {
                        case 's':
                            if (att.Value.IndexOf(lstAtt[i].Name) < 0) setval += ", " + lstAtt[i].Name.Substring(1) + "='" + lstAtt[i].Value + "'";
                            break;
                        case 'i':
                            if (att.Name.IndexOf(lstAtt[i].Name) < 0) setval += ", " + lstAtt[i].Name.Substring(1) + "=" + lstAtt[i].Value;
                            break;
                        case 'd':
                            if (att.Name.IndexOf(lstAtt[i].Name) < 0) setval += ", " + lstAtt[i].Name.Substring(1) + "=" + lstAtt[i].Value;
                            break;
                        case 'b':
                            if (att.Name.IndexOf(lstAtt[i].Name) < 0 && rawData == null)
                            {
                                setval += ", " + lstAtt[i].Name.Substring(1) + "= ?";
                                rawData = StringToByteArray(lstAtt[i].Value);
                            }
                            break;
                    }
                }
            }
            //if (el.Name == "NCardLinkIn") MessageBox.Show(cmdUpdate + " " + setval.Substring(2) + " WHERE " + GetWhereSearchKey(el, Keys));
            if (el.Name != "NCardLinkIn")
            {
                // si table = NCardLinkIn il y aura obligatoirement une duplique entry : l'enregistrement est une clé à trois valeur donc non modifiable
                if (setval != "") CBWriteWithObj(cmdUpdate + " " + setval.Substring(2) + " WHERE " + XmlGetWhereSearchKey(el, Keys), rawData);
            }
        }

        public void XmlCreateFromXml(XmlElement el)
        {
            //if (el.Name == "GPoint")  {}
            string cmdInsert = "INSERT INTO " + el.Name + " (", cmdInsert2 = ") VALUES (";
            string setname = "", setval = "";
            byte[] rawData = null;

            if (bAttribut)
            {
                XmlElement elAtts = parent.XmlFindFirstElFromName(el, "Attributs", 1);
                if (elAtts != null)
                {
                    IEnumerator ienum = elAtts.GetEnumerator();
                    XmlNode Node;

                    while (ienum.MoveNext())
                    {
                        Node = (XmlNode)ienum.Current;
                        if (Node.NodeType == XmlNodeType.Element)
                        {
                            XmlElement elAtt = (XmlElement)Node;
                            if (elAtt.Name == "Attribut")
                            {
                                switch (elAtt.GetAttribute("Type")[0])
                                {
                                    case 's':
                                        setname += ", " + elAtt.GetAttribute("Nom");
                                        setval += ", '" + elAtt.GetAttribute("Value") + "'";
                                        break;
                                    case 'i':
                                        setname += ", " + elAtt.GetAttribute("Nom"); setval += ", " + elAtt.GetAttribute("Value");
                                        break;
                                    case 'd':
                                        setname += ", " + elAtt.GetAttribute("Nom"); setval += ", " + elAtt.GetAttribute("Value");
                                        break;
                                    case 'b':
                                        if (rawData == null)
                                        {
                                            setname += ", " + elAtt.GetAttribute("Nom");
                                            setval += ", ?";
                                            rawData = StringToByteArray(elAtt.GetAttribute("Value"));
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                XmlAttributeCollection lstAtt = el.Attributes;
                for (int i = 1; i < lstAtt.Count; i++)
                {
                    switch (lstAtt[i].Name[0])
                    {
                        case 's':
                            setname += ", " + lstAtt[i].Name.Substring(1); setval += ", '" + lstAtt[i].Value + "'";
                            break;
                        case 'i':
                            setname += ", " + lstAtt[i].Name.Substring(1); setval += ", " + lstAtt[i].Value;
                            break;
                        case 'd':
                            setname += ", " + lstAtt[i].Name.Substring(1); setval += ", " + lstAtt[i].Value;
                            break;
                        case 'b':
                            if (rawData == null)
                            {
                                setname += ", " + lstAtt[i].Name.Substring(1);
                                setval += ", ?";
                                rawData = StringToByteArray(lstAtt[i].Value);
                            }
                            break;
                    }
                }
            }
            //if (el.Name == "NCardLinkIn") MessageBox.Show(cmdInsert + " " + setname.Substring(2) + cmdInsert2 + setval.Substring(2) + ")");
            CBWriteWithObj(cmdInsert + " " + setname.Substring(2) + cmdInsert2 + setval.Substring(2) + ")", rawData);
        }

        public byte[] StringToByteArray(String hData)
        {
            int iCars = hData.Length;
            byte[] bytes = new byte[iCars / 2];
            for (int i = 0; i < iCars; i += 2)
                bytes[i / 2] = Convert.ToByte(hData.Substring(i, 2), 16);
            return bytes;
        }

        private void CalcProvision(string sGuidVue, XmlExcel xmlExcel, bool full)
        {
            int n = -1;
            string sType = "", sqlString1 = "", sqlString2 = "", sqlString3 = "";
            if (full) { ConfDB.AddTabServerList(); sType = "ServerList"; }
            else { ConfDB.AddTabServerTechList(); sType = "ServerTechList"; }

            n = ConfDB.FindTable(sType);
            if (n > -1)
            {
                Table t = (Table)ConfDB.LstTable[n];
                string field = t.GetSelectFieldFromOption(ConfDataBase.FieldOption.NomCourt);

                if (full)
                {
                    //sqlString1 = "Select Distinct " + t.GetSelectFieldFromOption(ConfDataBase.FieldOption.NomCourt) + " From Application, AppVersion, Vue, Environnement, DansVue, GServerPhy, ServerPhy  Left Join LayerLink On ServerPhy.GuidServerPhy=GuidObj and layerlink.GuidAppVersion='" + parent.GetGuidAppVersion() + "', ServerLink, Server, NCard, VLan Where Vue.GuidVue='" + sGuidVue + "' and Application.GuidApplication=AppVersion.GuidApplication and Vue.GuidAppVersion=AppVersion.GuidAppVersion and Vue.GuidEnvironnement=Environnement.GuidEnvironnement and Vue.GuidGVue=DansVue.GuidGVue and GuidObjet=GServerPhy.GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and ServerPhy.GuidServerPhy=ServerLink.GuidServerPhy and ServerLink.GuidServer=Server.GuidServer and ServerPhy.GuidServerPhy=GuidHote and NCard.GuidVLan=VLan.GuidVLan" + parent.wkApp.GetWhereLayer();
                    sqlString1 = "Select Distinct " + t.GetSelectFieldFromOption(ConfDataBase.FieldOption.NomCourt) + " From Application, AppVersion, Vue, Environnement, DansVue, GServerPhy, ServerPhy  Left Join LayerLink On ServerPhy.GuidServerPhy=GuidObj and layerlink.GuidAppVersion='" + parent.GetGuidAppVersion() + "', ServerLink, Server, NCard Left Join VLan On NCard.GuidVLan=VLan.GuidVLan Where Vue.GuidVue='" + sGuidVue + "' and Application.GuidApplication=AppVersion.GuidApplication and Vue.GuidAppVersion=AppVersion.GuidAppVersion and Vue.GuidEnvironnement=Environnement.GuidEnvironnement and Vue.GuidGVue=DansVue.GuidGVue and GuidObjet=GServerPhy.GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and ServerPhy.GuidServerPhy=ServerLink.GuidServerPhy and ServerLink.GuidServer=Server.GuidServer and ServerPhy.GuidServerPhy=GuidHote" + parent.wkApp.GetWhereLayer();
                }
                else
                {
                    sqlString1 = "Select Distinct " + t.GetSelectFieldFromOption(ConfDataBase.FieldOption.NomCourt) + " From Application, AppVersion, Vue, Environnement, DansVue, GServerPhy, ServerPhy  Left Join LayerLink On ServerPhy.GuidServerPhy=GuidObj and layerlink.GuidAppVersion='" + parent.GetGuidAppVersion() + "', ServerLink, Server, NCard, VLan, ServerTypeLink, ServerType, Techno, TechnoRef Where Vue.GuidVue='" + sGuidVue + "' and Application.GuidApplication=AppVersion.GuidApplication and Vue.GuidAppVersion=AppVersion.GuidAppVersion and Vue.GuidEnvironnement=Environnement.GuidEnvironnement and Vue.GuidGVue=DansVue.GuidGVue and GuidObjet=GServerPhy.GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and ServerPhy.GuidServerPhy=ServerLink.GuidServerPhy and ServerLink.GuidServer=Server.GuidServer and ServerPhy.GuidServerPhy=GuidHote and NCard.GuidVLan=VLan.GuidVLan" + parent.wkApp.GetWhereLayer() + " and Server.GuidServer=ServerTypeLink.GuidServer and ServerTypeLink.GuidServerType=ServerType.GuidServerType and ServerType.GuidServerType=Techno.GuidTechnoHost and Techno.GuidTechnoRef=TechnoRef.GuidTechnoRef ";
                }

                sqlString2 = " and Server.GuidServer IN (SELECT Server.GuidServer FROM Vue vinf, Vue v, DansVue, GServer, Server WHERE vinf.GuidVue='" + sGuidVue + "' and vinf.GuidVueInf=v.GuidVue and v.GuidGVue=DansVue.GuidGVue AND DansVue.GuidObjet=GServer.GuidGServer AND GServer.GuidServer=Server.GuidServer)";
                sqlString3 = " order by NomServer, NomServerPhy, NomVLan";

                if (CBRecherche(sqlString1 + sqlString2 + sqlString3))
                {
                    while (Reader.Read())
                        xmlExcel.CreatXmlFromReader(sType, ConfDataBase.FieldOption.NomCourt);
                }
                CBReaderClose();
            }
        }

        public XmlElement xmlGetIndicatorObsolescence()
        {
            parent.docXml = new XmlDocument();
            parent.docXml.LoadXml("<Indicator></Indicator>");
            XmlElement root = parent.docXml.DocumentElement;

            //if (CBRecherche("Select ind1.GuidObjet, ind1.ValIndicator, ind2.ValIndicator From indicatorlink ind1, indicatorlink ind2  Where ind1.GuidObjet=ind2.GuidObjet and ind1.GuidIndicator='" + "3b854340-f70e-474b-8d4a-7eaa4263b9ec" + "' and ind2.GuidIndicator='" + "b00b12bd-a447-47e6-92f6-e3b76ad22830" + "'"))
            //if (CBRecherche("Select GuidTechnoRef, ConfinedStart, ConfinedEnd, ReferenceStart, DecommEnd From TechnoRef"))
            if (CBRecherche("Select GuidTechnoRef, ConfinedStart, ConfinedEnd, ReferenceStart, valindicator From TechnoRef, indicatorlink where technoref.GuidTechnoRef = indicatorlink.GuidObjet and indicatorlink.GuidIndicator = 'b00b12bd-a447-47e6-92f6-e3b76ad22830'"))
            {
                while (Reader.Read())
                {
                    XmlElement elrow = parent.docXml.CreateElement("row");

                    if (!Reader.IsDBNull(0))
                    {
                        elrow.SetAttribute("GuidObjet", Reader.GetString(0));
                        if (Reader.IsDBNull(3) || Reader.IsDBNull(4))
                        {
                            if (Reader.IsDBNull(1)) elrow.SetAttribute("PeriodeStart", "0"); else elrow.SetAttribute("PeriodeStart", Reader.GetDateTime(1).ToOADate().ToString());
                            if (Reader.IsDBNull(2)) elrow.SetAttribute("PeriodeEnd", "0"); else elrow.SetAttribute("PeriodeEnd", Reader.GetDateTime(2).ToOADate().ToString());
                        }
                        else
                        {
                            elrow.SetAttribute("PeriodeStart", Reader.GetDateTime(3).ToOADate().ToString());
                            elrow.SetAttribute("PeriodeEnd", Reader.GetDouble(4).ToString());
                            //elrow.SetAttribute("PeriodeEnd", Reader.GetDateTime(4).ToOADate().ToString());
                        }

                        /*el = parent.docXml.CreateElement("GuidObjet");
                        el.InnerText = Reader.GetString(0); elrow.AppendChild(el);
                        el = parent.docXml.CreateElement("FinCommercial");
                        if (Reader.IsDBNull(1)) el.InnerText = "0"; else el.InnerText = Reader.GetDouble(1).ToString(); elrow.AppendChild(el);
                        el = parent.docXml.CreateElement("FinSupport");
                        if (Reader.IsDBNull(2)) el.InnerText = "0"; else el.InnerText = Reader.GetDouble(2).ToString(); elrow.AppendChild(el);*/

                        root.AppendChild(elrow);
                    }
                }
            }
            CBReaderClose();
            parent.docXml.Save("c:\\dat\\temptechno.xml");
            return root;
        }

        public void Genere_ListApp()
        {
            //parent.docXml = new XmlDocument();
            //parent.docXml.LoadXml("<ListApplications></ListApplications>");
            //XmlElement root = parent.docXml.DocumentElement;

            // Export Serveur
            /*
            ArrayList aListVue = new ArrayList();
            if (CBRecherche("SELECT Distinct GuidVue FROM Vue, TypeVue WHERE Vue.GuidTypeVue='2a4c3691-e714-4d05-9400-8fbbb06f2d62' OR Vue.GuidTypeVue='ef667e58-a617-49fd-91a8-2beeda856475'  OR Vue.GuidTypeVue='7afca945-9d41-48fb-b634-5b6ffda90d4e'"))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml("<Export></Export>");
                XmlDocument xmlDoc2 = new XmlDocument();
                xmlDoc2.LoadXml("<row><Field Name=\"CI_NameServer\" Template=\"@NomServerPhy\"></Field><Field Name=\"CI_NameApplication\" Template=\"@Trigramme+-+@NomApplication+ - +@NomEnvironnement\"></Field></row>");
                while (Reader.Read())
                {
                    aListVue.Add(Reader.GetString(0));
                }
                CBReaderClose();

                for (int i = 0; i < aListVue.Count; i++)
                {
                    //NomVLan, IPAddr, Type, CPUCore, RAM
                    string sqlString1 = "Select Distinct  NomApplication, Trigramme, NomEnvironnement, NomServerPhy From Application, Vue, Environnement, DansVue, GServerPhy, ServerPhy, ServerLink, Server, NCard, VLan Where Vue.GuidVue='" + aListVue[i] + "' and Vue.GuidApplication=Application.GuidApplication and Vue.GuidEnvironnement=Environnement.GuidEnvironnement and Vue.GuidVue=DansVue.GuidVue and GuidObjet=GServerPhy.GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and ServerPhy.GuidServerPhy=ServerLink.GuidServerPhy and ServerLink.GuidServer=Server.GuidServer and ServerPhy.GuidServerPhy=GuidHote and NCard.GuidVLan=VLan.GuidVLan and ServerPhy.Type <> '' and (Application.GuidStatut is null or Application.GuidStatut<>'adc55f3f-abcc-41c9-a8b2-76b6083e8064') and Trigramme <> ''";
                    string sqlString2 = " and Server.GuidServer IN (SELECT Server.GuidServer FROM Vue vinf, Vue v, DansVue, GServer, Server WHERE vinf.GuidVue='" + aListVue[i] + "' and vinf.GuidVueInf=v.GuidVue and v.GuidVue=DansVue.GuidVue AND DansVue.GuidObjet=GServer.GuidGServer AND GServer.GuidServer=Server.GuidServer)";
                    
                    if (CBRecherche(sqlString1 + sqlString2))// + sqlString3))
                    {
                            XmlDocument xmlDoc1 = CreatXmlDocFromDB();
                            CreatXmlDocFromXmlFiles(xmlDoc, xmlDoc1, xmlDoc2);
                    }
                    CBReaderClose();

                }
                xmlDoc.Save(parent.sPathRoot + "\\ListServeur.xml");
            }
            else CBReaderClose();
            */


            // Export Application

            if (CBRecherche("Select Distinct * From Vue, Environnement, AppVersion Left Join Statut On Application.GuidStatut=Statut.GuidStatut, Application Left Join ApplicationType On Application.GuidApplicationType=ApplicationType.GuidApplicationType Left Join ApplicationClass On Application.GuidApplicationClass=ApplicationClass.GuidApplicationClass Where Application.GuidApplication=AppVersion.GuidApplication And AppVersion.GuidAppVersion=Vue.GuidAppVersion And Vue.GuidEnvironnement=Environnement.GuidEnvironnement and Trigramme <> '' and (AppVersion.GuidStatut is null or AppVersion.GuidStatut<>'adc55f3f-abcc-41c9-a8b2-76b6083e8064')"))
            //if (CBRecherche("Select Distinct * From Application, Vue, Environnement Where Application.GuidApplication=Vue.GuidApplication And Vue.GuidEnvironnement=Environnement.GuidEnvironnement"))
            {
                XmlDocument xmlDoc1 = CreatXmlDocFromDB();

                XmlDocument xmlDoc2 = new XmlDocument();
                xmlDoc2.LoadXml("<row><Field Name=\"CI_ID\" Template=\"@Trigramme+-+@NomApplication+ - +@NomEnvironnement\"></Field><Field Name=\"CI_Name\" Template=\"@Trigramme+-+@NomApplication+ - +@NomEnvironnement\"></Field><Field Name=\"CI_Description\" Template=\"@Trigramme+-+@NomApplication+ - +@NomEnvironnement\"></Field><Field Name=\"Tier1\" Template=\"Application\"></Field><Field Name=\"Tier2\" Template=\"@NomApplicationType\"></Field><Field Name=\"Tier3\" Template=\"@NomApplication+_(+@Trigramme+)\"></Field><Field Name=\"Product_Name\" Template=\"@Trigramme+-+@NomApplication+-+@NomApplicationClass\"></Field><Field Name=\"Environnement\" Template=\"@NomEnvironnement\"></Field><Field Name=\"Status\" Template=\"@NomStatut\"></Field></row>");
                XmlDocument xmlDoc = CreatXmlDocFromXmlFiles(xmlDoc1, xmlDoc2);
                xmlDoc.Save(parent.sPathRoot + "\\ListApplications.xml");
                //root.AppendChild(CreatXmlFromDB("Application", ConfDataBase.FieldOption.InterneBD));
            }
            CBReaderClose();
        }

        public void Genere_ListeServer2(XmlExcel xmlExcel, bool full)
        {
            ArrayList aListVue = new ArrayList();

            string requete = "SELECT Distinct GuidVue FROM Vue, TypeVue WHERE GuidAppVersion='" + parent.GetGuidAppVersion() + "' AND (Vue.GuidTypeVue='2a4c3691-e714-4d05-9400-8fbbb06f2d62' OR Vue.GuidTypeVue='ef667e58-a617-49fd-91a8-2beeda856475'  OR Vue.GuidTypeVue='7afca945-9d41-48fb-b634-5b6ffda90d4e')";


            if (CBRecherche(requete))
            {
                while (Reader.Read())
                {
                    aListVue.Add(Reader.GetString(0));
                }
                CBReaderClose();
                for (int i = 0; i < aListVue.Count; i++)
                {
                    //CalcProvision((string)aListVue[i], root);
                    CalcProvision((string)aListVue[i], xmlExcel, full);
                }
            }
            else CBReaderClose();
        }

        public XmlExcel Genere_ListeServer()
        {
            XmlExcel xmlExcel = new XmlExcel(parent, "ListServer");
            List<string[]> lstAppVersion = new List<string[]>();

            if (CBRecherche("SELECT Application.GuidApplication, NomApplication, AppVersion.GuidAppVersion FROM Application, AppVersion Where Application.GuidApplication = AppVersion.GuidApplication ORDER BY NomApplication"))
            {
                while (Reader.Read())
                {
                    string[] aEnreg = new string[3];
                    aEnreg[0] = Reader.GetString(0);    // GuidApplication
                    aEnreg[1] = Reader.GetString(1);    // NomApplication
                    aEnreg[2] = Reader.GetString(2);    // GuidAppVersion
                    lstAppVersion.Add(aEnreg);
                }
            }
            CBReaderClose();

            for (int i = 0; i < lstAppVersion.Count; i++)
            {
                parent.wkApp = new WorkApplication(parent, lstAppVersion[i][0], lstAppVersion[i][1], lstAppVersion[i][2]);
                Genere_ListeServer2(xmlExcel, false);
            }
            parent.wkApp = null;

            return xmlExcel;
        }

        public XmlExcel Genere_ListeServer(bool full)
        {
            ArrayList aListVue = new ArrayList();
            XmlExcel xmlExcel = new XmlExcel(parent, "ListServer");
            Genere_ListeServer2(xmlExcel, full);

            return xmlExcel;
        }

        public void CreatEnregFromXml(XmlElement el)
        {
            XmlElement elParent = (XmlElement)el.ParentNode;

            XmlAttributeCollection lstAtt = elParent.Attributes;
            if (el.Name == "Attributs" && lstAtt.Count != 0 && lstAtt[0].Name == "SearchKey")
            //if (el.Name == "ProduitApp") att = att;
            {
                string[] Keys = lstAtt[0].Value.Split(',');
                if (CBRecherche("SELECT " + GetSelectSearchKey(Keys) + " FROM " + elParent.Name + " WHERE " + XmlGetWhereSearchKey(elParent, Keys)))
                {
                    CBReaderClose();
                    if (el.Name == "Vue")
                    {
                        //deleteVue + Objetsassocies
                        //DeleteNCardLink(el.GetAttribute("sGuidVue"), "In");
                        //DeleteNCardLink(el.GetAttribute("sGuidVue"), "Out");
                        //DeleteVue(el.GetAttribute("sGuidVue"));
                        DeleteVue(parent.XmlGetTextEl(elParent));
                        XmlCreateFromXml(elParent); // Create
                    }
                    else
                        XmlUpdateFromXml(elParent, Keys); //Update

                }
                else
                {
                    CBReaderClose();
                    XmlCreateFromXml(elParent); //Create
                }
            }
        }

        public ArrayList CreatNcardHote(DrawRectangle ds)
        {
            ArrayList lstGuidNCard = new ArrayList();

            if (CBRecherche("SELECT GuidNCard FROM NCard WHERE GuidHote='" + ds.GuidkeyObjet + "' ORDER BY NomNCard"))
            {
                while (Reader.Read())
                {
                    lstGuidNCard.Add((object)Reader.GetString(0));
                }
                CBReaderClose();
            }
            CBReaderClose();

            return lstGuidNCard;
        }

        public ArrayList CreatNameSpace(DrawRectangle dr)
        {
            ArrayList lstGuidNameSpace = new ArrayList();

            FormChangeProp fcp = new FormChangeProp(parent, null);
            fcp.AddlSourceFromDB("SELECT GuidInsns, NomInsns FROM Insns WHERE GuidInsks='" + dr.GuidkeyObjet + "' ORDER BY NomInsns", "Create");
            fcp.ShowDialog(parent);

            if (fcp.Valider)
            {
                string[] aValue = CmdText.Split('(', ')');
                for (int i = 1; i < aValue.Length; i += 2)
                    lstGuidNameSpace.Add(aValue[i].Trim());
            }
            return lstGuidNameSpace;
        }

        public ArrayList CreatNcardCluster(DrawRectangle ds)
        {
            ArrayList lstGuidNCard = new ArrayList();

            FormChangeProp fcp = new FormChangeProp(parent, null);
            fcp.AddlSourceFromDB("SELECT GuidNCard, NomNCard FROM NCard WHERE GuidHote='" + ds.GuidkeyObjet + "' ORDER BY NomNCard", "Create");
            fcp.ShowDialog(parent);

            if (fcp.Valider)
            {
                string[] aValue = CmdText.Split('(', ')');
                for (int i = 1; i < aValue.Length; i += 2)
                    lstGuidNCard.Add(aValue[i].Trim());
            }
            return lstGuidNCard;
        }

        public ArrayList CreatTechnoServer(DrawRectangle dst)
        {
            ArrayList lstGuidTechno = new ArrayList();

            if (CBRecherche("SELECT GuidTechno FROM Techno WHERE GuidTechnoHost='" + dst.GuidkeyObjet + "'"))
            {
                while (Reader.Read())
                {
                    lstGuidTechno.Add((object)Reader.GetString(0));
                }
                CBReaderClose();
            }
            CBReaderClose();
            return lstGuidTechno;
        }

        public ArrayList CreatInfNCard(DrawRectangle dst)
        {
            ArrayList lstInfNCard = new ArrayList();

            if (CBRecherche("SELECT GuidNCard FROM NCard WHERE GuidHote='" + dst.GuidkeyObjet + "'"))
            {
                while (Reader.Read())
                {
                    lstInfNCard.Add((object)Reader.GetString(0));
                }
                CBReaderClose();
            }
            CBReaderClose();
            return lstInfNCard;
        }

        public ArrayList CreatInfLabel(DrawRectangle dst)
        {
            ArrayList lstInfNCard = new ArrayList();

            if (CBRecherche("SELECT GuidNCard FROM NCard WHERE GuidHote='" + dst.GuidkeyObjet + "'"))
            {
                while (Reader.Read())
                {
                    lstInfNCard.Add((object)Reader.GetString(0));
                }
                CBReaderClose();
            }
            CBReaderClose();
            return lstInfNCard;
        }
        public ArrayList CreatMCompServ(DrawRectangle dmc)
        {
            ArrayList lstGuidMCompServ = new ArrayList();

            if (CBRecherche("SELECT GuidMCompServ FROM MCompServ WHERE GuidMainComposant='" + dmc.GuidkeyObjet + "'"))
            {
                while (Reader.Read())
                {
                    lstGuidMCompServ.Add((object)Reader.GetString(0));
                }
                CBReaderClose();
            }
            CBReaderClose();
            return lstGuidMCompServ;
        }

        public ArrayList CreatServerCluster(DrawRectangle ds)
        {
            ArrayList lstGuidServerPhy = new ArrayList();

            if (CBRecherche("SELECT GuidServerPhy FROM ServerPhy WHERE GuidCluster='" + ds.GuidkeyObjet + "' ORDER BY NomServerPhy"))
            {
                while (Reader.Read())
                {
                    lstGuidServerPhy.Add((object)Reader.GetString(0));
                }
                CBReaderClose();
            }
            CBReaderClose();
            return lstGuidServerPhy;
        }

        public List<String[]> GetlstGening(string sGuidGenpod)
        {
            List<String[]> lstGening = new List<String[]>();

            if (CBRecherche("Select svclink.GuidServerIn guiding, NomGening nom From Techlink podlink, Gensvc, Techlink svclink, Gening WHERE svclink.GuidServerIn = Gening.GuidGening And podlink.GuidServerIn = svclink.GuidServerOut And podlink.GuidServerIn = Gensvc.GuidGensvc And podlink.GuidServerOut = '" + sGuidGenpod + "'"))
            {
                while (Reader.Read())
                {
                    String[] aEnreg = new String[2];
                    aEnreg[0] = Reader.GetString(0); aEnreg[1] = Reader.GetString(1);
                    lstGening.Add(aEnreg);
                }
                CBReaderClose();
            }
            CBReaderClose();
            return lstGening;
        }


        public ArrayList GetlstNCardFromsvc(string sGuidInssvc)
        {
            ArrayList lstNCard = new ArrayList();

            if (CBRecherche("SELECT GuidNCard FROM NCard WHERE GuidHote='" + sGuidInssvc + "'"))
            {
                while (Reader.Read())
                {
                    lstNCard.Add((object)Reader.GetString(0));
                }
                CBReaderClose();
            }
            CBReaderClose();
            return lstNCard;
        }
        public List<String[]> GetlstGensvc(string sGuidGenpod)
        {
            List<String[]> lstGensvc = new List<String[]>();
            string sqlRequest = "SELECT GuidServerIn guid, NomGensvc nom FROM TechLink, Gensvc WHERE GuidServerIn = GuidGensvc and GuidServerOut = '" + sGuidGenpod + "'";

            sqlRequest += " Union Select linkref.GuidServerOut guid, NomGensvc nom FROM Techlink linkref, Gensvc WHERE GuidServerOut = GuidGensvc and linkref.GuidServerOut not in (";
            sqlRequest += "    Select linknext.GuidServerIn from TechLink linknext, Genpod where linknext.GuidServerOut = Genpod.GuidGenpod) and ";
            sqlRequest += "         linkref.GuidServerIn = '" + sGuidGenpod + "'";

            if (CBRecherche(sqlRequest))
            {
                while (Reader.Read())
                {
                    string[] aEnreg = new string[2];
                    aEnreg[0] = Reader.GetString(0); aEnreg[1] = Reader.GetString(1);
                    lstGensvc.Add(aEnreg);
                }
                CBReaderClose();
            }
            CBReaderClose();
            return lstGensvc;
        }

        public List<String[]> GetlstGenpod(int iTypeObjet, string sGuid)
        {
            List<String[]> lstGenpod = new List<string[]>();

            string request = "";

            switch (iTypeObjet)
            {
                case (int)DrawArea.DrawToolType.Genks:
                    request = "SELECT GuidGenpod, NomGenpod FROM Genpod WHERE GuidGenks='" + sGuid + "' ORDER BY NomGenpod";
                    break;
                case (int)DrawArea.DrawToolType.Insks:
                    request = "SELECT GuidGenpod, NomGenpod FROM Genpod, Genks, Insks WHERE Genpod.GuidGenks = Genks.GuidGenks and Genks.GuidGenks = Insks.GuidGenks and GuidInsks='" + sGuid + "' ORDER BY NomGenpod";
                    break;
            }
            if (CBRecherche(request))
            {
                while (Reader.Read())
                {
                    string[] aEnreg = new string[2];
                    aEnreg[0] = Reader.GetString(0); aEnreg[1] = Reader.GetString(1);
                    lstGenpod.Add(aEnreg);
                }
            }
            CBReaderClose();
            return lstGenpod;
        }

        public List<String[]> GetlstInssvc(string sGuid)
        {
            List<String[]> lstInssvc = new List<string[]>();

            if (CBRecherche("SELECT GuidInssvc, GuidGensvc FROM Inssvc WHERE GuidInspod='" + sGuid + "' ORDER BY NomInssvc"))
            {
                while (Reader.Read())
                {
                    string[] aEnreg = new string[2];
                    aEnreg[0] = Reader.GetString(0); aEnreg[1] = Reader.GetString(1);
                    lstInssvc.Add(aEnreg);
                }
            }
            CBReaderClose();
            return lstInssvc;
        }

        public List<String[]> GetlstInsing(string sGuid)
        {
            List<String[]> lstInsing = new List<string[]>();

            if (CBRecherche("SELECT GuidInsing, GuidGening FROM Insing WHERE GuidInspod='" + sGuid + "' ORDER BY NomInsing"))
            {
                while (Reader.Read())
                {
                    string[] aEnreg = new string[2];
                    aEnreg[0] = Reader.GetString(0); aEnreg[1] = Reader.GetString(1);
                    lstInsing.Add(aEnreg);
                }
            }
            CBReaderClose();
            return lstInsing;
        }
        public List<String[]> GetlstInspod(string sGuid)
        {
            List<String[]> lstInspod = new List<string[]>();

            if (CBRecherche("SELECT GuidInspod, GuidGenpod FROM Inspod WHERE GuidInsns='" + sGuid + "' ORDER BY NomInspod"))
            {
                while (Reader.Read())
                {
                    string[] aEnreg = new string[2];
                    aEnreg[0] = Reader.GetString(0); aEnreg[1] = Reader.GetString(1);
                    lstInspod.Add(aEnreg);
                }
            }
            CBReaderClose();
            return lstInspod;
        }

        public ArrayList CreatServerLocation(DrawLocation dl)
        {
            ArrayList lstGuidServerPhy = new ArrayList();

            if (CBRecherche("SELECT GuidServerPhy FROM ServerPhy WHERE GuidLocation='" + dl.GuidkeyObjet + "' ORDER BY NomServerPhy"))
            {

                while (Reader.Read())
                {
                    TreeNode[] ArrayTreeNode = parent.tvObjet.Nodes.Find(Reader.GetString(0), true);
                    if (ArrayTreeNode.Length > 0)
                        lstGuidServerPhy.Add((object)Reader.GetString(0));
                }
                CBReaderClose();
            }
            CBReaderClose();
            return lstGuidServerPhy;
        }

        public bool ExistGuid(DrawObject dob)
        {
            //string sType = dob.GetsType(false);
            string sType = dob.GetTypeSimpleTable();
            //dob.GetType().Name.Substring("Draw".Length);
            if (CBRecherche("SELECT Guid" + sType + " FROM " + sType + " WHERE Guid" + sType + " ='" + dob.GuidkeyObjet + "'"))
            {
                CBReaderClose();
                return true;
            }
            CBReaderClose();
            return false;
        }

        public bool ExistGuid(int idx, DrawObject dob)
        {
            //string sType = dob.GetsType(false);
            string sType = dob.GetTypeSimpleTable();
            //dob.GetType().Name.Substring("Draw".Length);
            if (CBRecherche("SELECT Guid" + sType + " FROM " + sType + " WHERE Guid" + sType + " ='" + (string)dob.LstValue[idx] + "'"))
            {
                CBReaderClose();
                return true;
            }
            CBReaderClose();
            return false;
        }

        public bool ExistGuid(Enreg o)
        {
            string sType = o.sTable;

            if (CBRecherche("SELECT Guid" + sType + " FROM " + sType + " WHERE Guid" + sType + " ='" + (string)o.LstValue[0] + "'"))
            {
                CBReaderClose();
                return true;
            }
            CBReaderClose();
            return false;
        }

        public void CreatObject(Enreg o)
        {
            string sType = o.sTable;
            string RequeteField = "INSERT INTO " + sType + " (";
            string RequeteValue = "VALUES (";

            Table t;
            int n = ConfDB.FindTable(sType);
            if (n > -1)
            {
                t = (Table)ConfDB.LstTable[n];
                bool InsField = false;
                for (int i = 0; i < t.LstField.Count; i++)
                {
                    if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.InterneBD) != 0)
                    {
                        switch (((Field)t.LstField[i]).Type)
                        {
                            case 's':
                                if ((string)o.LstValue[i] != "")
                                {
                                    if (InsField) { RequeteField += ", "; RequeteValue += ", "; }
                                    RequeteField += ((Field)t.LstField[i]).Name;
                                    RequeteValue += "'" + o.LstValue[i] + "'";
                                    InsField = true;
                                }
                                break;
                            case 'p': //picture
                            case 'q': //picture
                            case 'i':
                                if ((int)o.LstValue[i] != 0)
                                {
                                    if (InsField) { RequeteField += ", "; RequeteValue += ", "; }
                                    RequeteField += ((Field)t.LstField[i]).Name;
                                    RequeteValue += o.LstValue[i];
                                    InsField = true;
                                }
                                break;
                            case 'd':
                                if ((double)o.LstValue[i] != 0)
                                {
                                    if (InsField) { RequeteField += ", "; RequeteValue += ", "; }
                                    RequeteField += ((Field)t.LstField[i]).Name;
                                    RequeteValue += o.LstValue[i];
                                    InsField = true;
                                }
                                break;
                            case 't':
                                //if (o.LstValue[i] != null && o.LstValue[i].ToString() != "" && (DateTime)o.LstValue[i] != DateTime.MinValue)
                                if (o.LstValue[i] != null && o.LstValue[i].ToString() != "")
                                {
                                    DateTime dt = (DateTime)o.LstValue[i];
                                    if (InsField) { RequeteField += ", "; RequeteValue += ", "; }
                                    RequeteField += ((Field)t.LstField[i]).Name;
                                    RequeteValue += "'" + dt.ToString("yyyy-MM-dd") + "'";
                                    InsField = true;
                                }
                                break;

                        }
                    }
                }
                RequeteField += ") ";
                RequeteValue += ")";
            }

            CBWrite(RequeteField + RequeteValue);
        }

        public void UpdateObject(Enreg o)
        {
            string sType = o.sTable;
            string Update = "UPDATE " + sType;
            string Set = "SET ";
            string Where = "WHERE ";

            Table t;
            int n = ConfDB.FindTable(sType);
            if (n > -1)
            {
                t = (Table)ConfDB.LstTable[n];
                bool InsFieldW = false, InsFieldR = false;

                for (int i = 0; i < t.LstField.Count; i++)
                {
                    if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.InterneBD) != 0)
                    {

                        if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.Key) != 0)
                        {
                            switch (((Field)t.LstField[i]).Type)
                            {
                                case 's':
                                    if ((string)o.LstValue[i] != "")
                                    {
                                        if (InsFieldW) Set += " and ";
                                        Where += ((Field)t.LstField[i]).Name + "='" + o.LstValue[i] + "'";
                                        InsFieldW = true;
                                    }
                                    break;
                                case 'p': //picture
                                case 'q': //picture
                                case 'i':
                                    if ((int)o.LstValue[i] != 0)
                                    {
                                        if (InsFieldW) Set += " and ";
                                        Where += ((Field)t.LstField[i]).Name + "=" + o.LstValue[i];
                                        InsFieldW = true;
                                    }
                                    break;
                                case 'd':
                                    if ((double)o.LstValue[i] != 0)
                                    {
                                        if (InsFieldR) Set += ", ";
                                        Set += ((Field)t.LstField[i]).Name + "=" + o.LstValue[i];
                                        InsFieldR = true;
                                    }
                                    break;
                                case 't':
                                    //if (o.LstValue[i] != null && (string)o.LstValue[i] != "" && (DateTime)o.LstValue[i] != DateTime.MinValue)
                                    if (o.LstValue[i] != null && (string)o.LstValue[i] != "")
                                    {
                                        DateTime dt = (DateTime)o.LstValue[i];
                                        if (InsFieldR) Set += ", ";
                                        Set += ((Field)t.LstField[i]).Name + "='" + dt.ToString("yyyy-MM-dd") + "'"; //dt.ToShortDateString() + "'";
                                        InsFieldR = true;
                                    }
                                    break;

                            }
                        }
                        else
                        {
                            switch (((Field)t.LstField[i]).Type)
                            {
                                case 's':
                                    if ((string)o.LstValue[i] != "")
                                    {
                                        if (InsFieldR) Set += ", ";
                                        Set += ((Field)t.LstField[i]).Name + "='" + o.LstValue[i] + "'";
                                        InsFieldR = true;
                                    }
                                    break;
                                case 'p': //picture
                                case 'q': //picture
                                case 'i':
                                    if ((int)o.LstValue[i] != null)
                                    {
                                        if (InsFieldR) Set += ", ";
                                        Set += ((Field)t.LstField[i]).Name + "=" + o.LstValue[i];
                                        InsFieldR = true;
                                    }
                                    break;
                                case 'd':
                                    if ((double)o.LstValue[i] != null)
                                    {
                                        if (InsFieldR) Set += ", ";
                                        Set += ((Field)t.LstField[i]).Name + "=" + o.LstValue[i];
                                        InsFieldR = true;
                                    }
                                    break;
                                case 't':
                                    if (o.LstValue[i] != null && (DateTime)o.LstValue[i] != DateTime.MinValue)
                                    //if (o.LstValue[i] != null && (string)o.LstValue[i] != "" && (DateTime)o.LstValue[i] != DateTime.MinValue)
                                    {
                                        DateTime dt = (DateTime)o.LstValue[i];
                                        if (InsFieldR) Set += ", ";
                                        Set += ((Field)t.LstField[i]).Name + "='" + dt.ToString("yyyy-MM-dd") + "'"; //dt.ToShortDateString() + "'";
                                        //Set += ((Field)t.LstField[i]).Name + "='" + dt.ToShortDateString() + "'";
                                        InsFieldR = true;
                                    }
                                    break;

                            }
                        }
                    }
                }
            }
            // cette fonction UpdateObject existe aussi avec l'objet drawobject
            DataTable datatable = connection.GetSchema("columns", new[] { null, null, sType, "updatedate" });
            if (datatable.Rows.Count == 1)
                Set += ", updatedate='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
            CBWrite(Update + " " + Set + " " + Where);
        }

        public bool ExistGuid(DrawObject dob, string sType)
        {
            if (CBRecherche("SELECT Guid" + sType + " FROM " + sType + " WHERE Guid" + sType + " ='" + dob.GuidkeyObjet + "'"))
            {
                CBReaderClose();
                return true;
            }
            CBReaderClose();
            return false;
        }

        public bool ExistLabelLink(string GuidLabel, string GuidObjet)
        {
            if (CBRecherche("Select GuidLabel From LabelLink Where GuidLabel='" + GuidLabel + "' and GuidObjet='" + GuidObjet + "'"))
            {
                CBReaderClose();
                return true;
            }
            CBReaderClose();
            return false;
        }

        public bool ExistExtentionValue(string GuidObj, string GuidParam)
        {
            if (CBRecherche("SELECT GuidParam FROM ExtentionParamValue WHERE GuidObject = '" + GuidObj + "' and GuidParam = '" + GuidParam + "'"))
            {
                CBReaderClose();
                return true;
            }
            CBReaderClose();

            return false;
        }

        public bool ExistServerLink(string field, string GuidServerPhy, string GuidVue, string Guidfield)
        {
            if (CBRecherche("SELECT " + field + " FROM " + field.Substring("Guid".Length) + "Link WHERE GuidServerPhy='" + GuidServerPhy + "' and GuidVue='" + GuidVue + "' and " + field + "='" + Guidfield + "'"))
            {
                CBReaderClose();
                return true;
            }
            CBReaderClose();
            return false;
        }

        public bool ExistSanCardLink(string field, string GuidSanCardSrc, string GuidVue, string GuidSanCardCbl)
        {
            if (CBRecherche("SELECT GuidSanCardSrc FROM " + field.Substring("Guid".Length) + "Link WHERE GuidSanCardSrc='" + GuidSanCardSrc + "' and GuidVue='" + GuidVue + "' and GuidSanCardCbl='" + GuidSanCardCbl + "'"))
            {
                CBReaderClose();
                return true;
            }
            CBReaderClose();
            return false;
        }

        public bool ExistISLLink(string field, string Guidfield, string GuidServerPhy)
        {
            if (CBRecherche("SELECT " + field + " FROM " + field.Substring("Guid".Length) + "Link WHERE " + field + "='" + Guidfield + "' and GuidServerPhy='" + GuidServerPhy + "'"))
            {
                CBReaderClose();
                return true;
            }
            CBReaderClose();
            return false;
        }


        public bool ExistGuidG(DrawObject dob)
        {
            string sType = dob.GetTypeSimpleGTable();
            if (CBRecherche("SELECT Guid" + sType + " FROM " + sType + " WHERE Guid" + sType + " ='" + dob.Guidkey + "'"))
            {
                CBReaderClose();
                return true;
            }
            CBReaderClose();
            return false;
        }

        public bool ExistGuidG(DrawObject dob, string sType)
        {
            if (CBRecherche("SELECT GuidG" + sType + " FROM G" + sType + " WHERE GuidG" + sType + " ='" + dob.Guidkey + "'"))
            {
                CBReaderClose();
                return true;
            }
            CBReaderClose();
            return false;
        }

        public string FindGuidFromNom(string sTable, string sNom)
        {
            string sGuid = null;
            if (CBRecherche("SELECT Guid" + sTable + " FROM " + sTable + " where Nom" + sTable + " ='" + sNom + "'"))
            {
                sGuid = Reader.GetString(0);
                CBReaderClose();
            }
            else CBReaderClose();
            return sGuid;
        }
        /*
        public void CreatAppli()
        {
            //if (!CBRecherche("SELECT GuidApplication FROM Application where NomApplication ='" + parent.cbApplication.Text + "'"))
            if (!CBRecherche("SELECT GuidApplication FROM Application where GuidApplication ='" + parent.GetGuidApplication() + "'"))
            {
                CBReaderClose();
                parent.GuidApplication = Guid.NewGuid();
                CBWrite("INSERT INTO Application (GuidApplication, NomApplication, GuidCode) VALUES ('" + parent.GuidApplication + "','" + parent.cbApplication.Text + "','764079ad-621b-4c57-92be-9d1530fb20cb')");
            }
            else
            {
                parent.GuidApplication = new Guid(Reader.GetString(0));
                CBReaderClose();
            }
        }*/

        public void LoadLink()
        {
            string sTypeVue = parent.tbTypeVue.Text; // (string)parent.cbTypeVue.SelectedItem;
            string sQuery = "";
            sQuery = "SELECT GuidGTechLink, TechLink.GuidTechLink, NomTechLink, GuidServerIn, GuidServerOut, X, Y FROM DansVue, GTechLink, TechLink, GPoint WHERE GuidGVue ='" + parent.GuidGVue + "' AND DansVue.GuidObjet=GTechLink.GuidGTechLink AND GTechLink.GuidTechLink=TechLink.GuidTechLink AND GTechLink.GuidGTechLink=GPoint.GuidGObjet ORDER BY GuidGTechLink, I";

            if (CBRecherche(sQuery))
            {
                string sGuidGLink = "";
                string sGuidLink = "";
                string sNomLink = "";
                string sGuidModuleIn = "";
                string sGuidModuleOut = "";
                DrawTechLink dl;
                ArrayList pointArray = new ArrayList();

                while (Reader.Read())
                {
                    if (Reader.GetString(0) == sGuidGLink)
                    {
                        pointArray.Add(new Point(Reader.GetInt16(5), Reader.GetInt16(6)));
                    }
                    else
                    {
                        if (pointArray.Count != 0)
                        {
                            DrawObject obj;

                            // add GLink Objet
                            dl = new DrawTechLink(parent, sGuidLink, sNomLink, pointArray);

                            obj = parent.RechercheObjet(sGuidModuleIn);
                            dl.LstLinkIn.Add(obj); obj.LstLinkOut.Add(dl);
                            obj = parent.RechercheObjet(sGuidModuleOut);
                            dl.LstLinkOut.Add(obj); obj.LstLinkIn.Add(dl);
                            parent.drawArea.GraphicsList.Add(dl);


                            TreeNode[] ArrayTreeNode = parent.tvObjet.Nodes.Find(sGuidLink, true);
                            if (ArrayTreeNode.Length == 1) ArrayTreeNode[0].Remove();
                        }
                        pointArray.Clear();
                        sGuidGLink = Reader.GetString(0);
                        sGuidLink = Reader.GetString(1);
                        sNomLink = Reader.GetString(2);
                        sGuidModuleIn = Reader.GetString(3);
                        sGuidModuleOut = Reader.GetString(4);
                        pointArray.Add(new Point(Reader.GetInt16(5), Reader.GetInt16(6)));
                    }
                }
                if (pointArray.Count != 0)
                {
                    // add GLink Objet
                    DrawObject obj;

                    dl = new DrawTechLink(parent, sGuidLink, sNomLink, pointArray);

                    obj = parent.RechercheObjet(sGuidModuleIn);
                    dl.LstLinkIn.Add(obj); obj.LstLinkOut.Add(dl);
                    obj = parent.RechercheObjet(sGuidModuleOut);
                    dl.LstLinkOut.Add(obj); obj.LstLinkIn.Add(dl);
                    parent.drawArea.GraphicsList.Add(dl);


                    TreeNode[] ArrayTreeNode = parent.tvObjet.Nodes.Find(sGuidLink, true);
                    if (ArrayTreeNode.Length == 1) ArrayTreeNode[0].Remove();
                }
                CBReaderClose();
            }
            else CBReaderClose();
        }


        public void LinkNCardWithVLan()
        {
            for (int i = 0; i < parent.drawArea.GraphicsList.Count; i++)
            {
                DrawObject o = (DrawObject)parent.drawArea.GraphicsList[i];
                if (o.GetType() == typeof(DrawNCard))
                {

                    DrawNCard dnc = (DrawNCard)o;
                    DrawObject oParent = (DrawObject)dnc.LstParent[0];
                    int j = parent.drawArea.GraphicsList.FindObjet(0, (string)dnc.GetValueFromName("GuidVlan"));
                    if (j != -1)
                    {
                        DrawVLan dvl = (DrawVLan)parent.drawArea.GraphicsList[j];

                        dnc.Hauteur = LinkPointVlanWithCard(dvl, dnc, oParent.GetTopYNCard(), oParent.GetBottomYNCard());
                        /*
                        ArrayList lstIndexPointVlanX = new ArrayList();                       

                        for (j = 0; j < dvl.pointArray.Count; j++)
                        {
                            int x = ((Point)dvl.pointArray[j]).X;
                            if (x >= dnc.XMin() && x <= dnc.XMax()) lstIndexPointVlanX.Add(j);
                        }

                        int iTopY = oParent.GetTopYNCard(), iBottomY = oParent.GetBottomYNCard();
                        //int iMiddleTopY = dnc.YMin(), iMiddleBottomY = dnc.YMax();
                        int index = -1, iCard = iTopY, deltaMax = 10, delta = deltaMax;
                        for(j=0; j<lstIndexPointVlanX.Count; j++)
                        {
                            

                            delta = Math.Abs((((Point)dvl.pointArray[(int)lstIndexPointVlanX[j]]).Y - iTopY));
                            if (delta < deltaMax)
                            {
                                if (index == -1) index = (int)lstIndexPointVlanX[j];

                                if (Math.Abs((((Point)dvl.pointArray[index]).Y - iCard)) > delta)
                                {
                                    index = (int)lstIndexPointVlanX[j]; iCard = iTopY;
                                }
                            }
                            delta = Math.Abs((((Point)dvl.pointArray[(int)lstIndexPointVlanX[j]]).Y - iBottomY));
                            if (delta < deltaMax)
                            {
                                if (index == -1) index = (int)lstIndexPointVlanX[j];

                                if (Math.Abs((((Point)dvl.pointArray[index]).Y - iCard)) > delta)
                                {
                                    index = (int)lstIndexPointVlanX[j]; iCard = iBottomY;
                                }
                            }
                        }
                        if (index != -1)
                        {
                            int indexFromPointArray = dvl.LstLinkOut.Count;
                            dnc.AttachLink(dvl, DrawObject.TypeAttach.Entree);
                            dvl.AttachLink(dnc, DrawObject.TypeAttach.Sortie);

                            Point pt = (Point)dvl.pointArray[index];
                            

                            if (iCard == iTopY)
                            {
                                dnc.Hauteur = 0;
                                pt.Y = oParent.GetTopYNCard();
                            }
                            else
                            {
                                dnc.Hauteur = 1;
                                pt.Y = oParent.GetBottomYNCard();
                            }
                            //dnc.HandleVLan = index;
                            if(index == indexFromPointArray) dvl.pointArray[index] = pt;
                            else
                            {
                                Point ptTemp = (Point) dvl.pointArray[indexFromPointArray];
                                dvl.pointArray[indexFromPointArray] = pt;
                                dvl.pointArray[index] = ptTemp;
                            }
                            //dvl.pointArray.RemoveAt(index);
                            //dvl.pointArray.Insert(index,pt);
                            //dnc.HandleVLan = index;
                            //dvl.AttachHandle.Add(dnc.HandleVLan + 10); // + 9 car il y a 9 points prient par le rectangle du VLAn
                        }*/
                    }
                }
            }
        }

        public void LoadVLanPoint()
        {
            string sQuery = "SELECT GuidVLan, GPoint.X, GPoint.Y FROM DansVue, GVLan, GPoint WHERE GuidGVue ='" + parent.GuidGVue + "' AND GuidObjet=GuidGVLan AND GuidGVLan=GuidGObjet ORDER BY GuidGVLan, I";

            if (CBRecherche(sQuery))
            {
                DrawVLan dv = null;
                ArrayList pointArray = new ArrayList();
                //string sGuidGVLan = "";

                while (Reader.Read())
                {
                    if (dv != null && dv.GuidkeyObjet.ToString() == Reader.GetString(0))
                    {
                        pointArray.Add(new Point(Reader.GetInt16(1), Reader.GetInt16(2)));
                    }
                    else
                    {
                        if (pointArray.Count != 0)
                        {
                            dv.pointArray = pointArray;
                        }
                        pointArray = new ArrayList();
                        dv = null;
                        int i = parent.drawArea.GraphicsList.FindObjet(0, Reader.GetString(0));
                        if (i != -1)
                        {

                            dv = (DrawVLan)parent.drawArea.GraphicsList[i];
                            pointArray.Add(new Point(Reader.GetInt16(1), Reader.GetInt16(2)));
                        }
                    }
                }
                if (pointArray.Count != 0) dv.pointArray = pointArray;
                CBReaderClose();
            }
            else CBReaderClose();
        }

        public void LoadSanSwitchPoint()
        {
            string sQuery = "SELECT GuidSanSwitch, GPoint.X, GPoint.Y FROM DansVue, GSanSwitch, GPoint WHERE GuidGVue ='" + parent.GuidGVue + "' AND GuidObjet=GuidGSanSwitch AND GuidGSanSwitch=GuidGObjet ORDER BY GuidGSanSwitch, I";

            if (CBRecherche(sQuery))
            {
                DrawSanSwitch dss = null;
                ArrayList pointArray = new ArrayList();
                //string sGuidGVLan = "";

                while (Reader.Read())
                {
                    if (dss != null && dss.GuidkeyObjet.ToString() == Reader.GetString(0))
                    {
                        pointArray.Add(new Point(Reader.GetInt16(1), Reader.GetInt16(2)));
                    }
                    else
                    {
                        if (pointArray.Count != 0)
                        {
                            dss.pointArray = pointArray;
                        }
                        pointArray = new ArrayList();
                        dss = null;
                        int i = parent.drawArea.GraphicsList.FindObjet(0, Reader.GetString(0));
                        if (i != -1)
                        {
                            dss = (DrawSanSwitch)parent.drawArea.GraphicsList[i];
                            pointArray.Add(new Point(Reader.GetInt16(1), Reader.GetInt16(2)));
                        }
                    }
                }
                if (pointArray.Count != 0) dss.pointArray = pointArray;
                CBReaderClose();
            }
            else CBReaderClose();
        }

        public void LoadCnxPoint()
        {
            string sQuery = "SELECT GuidCnx, GPoint.X, GPoint.Y FROM DansVue, GCnx, GPoint WHERE GuidGVue ='" + parent.GuidGVue + "' AND GuidObjet=GuidGCnx AND GuidGCnx=GuidGObjet ORDER BY GuidGCnx, I";

            if (CBRecherche(sQuery))
            {
                DrawCnx dc = null;
                ArrayList pointArray = new ArrayList();
                //string sGuidGVLan = "";

                while (Reader.Read())
                {
                    if (dc != null && dc.GuidkeyObjet.ToString() == Reader.GetString(0))
                    {
                        pointArray.Add(new Point(Reader.GetInt16(1), Reader.GetInt16(2)));
                    }
                    else
                    {
                        if (pointArray.Count != 0)
                        {
                            dc.pointArray = pointArray;
                        }
                        pointArray = new ArrayList();
                        dc = null;
                        int i = parent.drawArea.GraphicsList.FindObjet(0, Reader.GetString(0));
                        if (i != -1)
                        {
                            dc = (DrawCnx)parent.drawArea.GraphicsList[i];
                            pointArray.Add(new Point(Reader.GetInt16(1), Reader.GetInt16(2)));
                        }
                    }
                }
                if (pointArray.Count != 0) dc.pointArray = pointArray;
                CBReaderClose();
            }
            else CBReaderClose();
        }

        public void LoadZonePoint()
        {
            string sQuery = "SELECT GuidZone, GPoint.X, GPoint.Y FROM DansVue, GZone, GPoint WHERE GuidGVue ='" + parent.GuidGVue + "' AND GuidObjet=GuidGZone AND GuidGZone=GuidGObjet ORDER BY GuidZone, I";

            if (CBRecherche(sQuery))
            {
                DrawZone dz = null;
                ArrayList pointArray = new ArrayList();
                //string sGuidGVLan = "";

                while (Reader.Read())
                {
                    if (dz != null && dz.GuidkeyObjet.ToString() == Reader.GetString(0))
                    {
                        pointArray.Add(new Point(Reader.GetInt16(1), Reader.GetInt16(2)));
                    }
                    else
                    {
                        if (pointArray.Count != 0)
                        {
                            dz.pointArray = pointArray;
                        }
                        pointArray = new ArrayList();
                        dz = null;
                        int i = parent.drawArea.GraphicsList.FindObjet(0, Reader.GetString(0));
                        if (i != -1)
                        {
                            dz = (DrawZone)parent.drawArea.GraphicsList[i];
                            pointArray.Add(new Point(Reader.GetInt16(1), Reader.GetInt16(2)));
                        }
                    }
                }
                if (pointArray.Count != 0) dz.pointArray = pointArray;
                CBReaderClose();
            }
            else CBReaderClose();
        }


        public void DeleteGObject(Tool tool, string sGuidGVue)
        {
            string Select, From, Where;
            string sType = tool.GetType().Name.Substring("Tool".Length);

            ArrayList LstObjet = new ArrayList();

            Select = "SELECT GuidG" + sType;
            From = tool.GetFromG(sType);
            Where = tool.GetWhereG(sType, sGuidGVue);

            if (CBRecherche(Select + " " + From + " " + Where))
            {
                while (Reader.Read()) LstObjet.Add(Reader.GetString(0));
                CBReaderClose();
                for (int i = 0; i < LstObjet.Count; i++)
                    CBWrite("DELETE FROM G" + sType + "  WHERE GuidG" + sType + "='" + (string)LstObjet[i] + "'");
            }
            else CBReaderClose();
        }

        public void DeleteGPoint(string GuidObjet)
        {
            if (CBRecherche("SELECT GuidGObjet From GPoint Where GuidGObjet='" + GuidObjet + "'"))
            {
                CBReaderClose();
                CBWrite("DELETE FROM GPoint WHERE GuidGObjet='" + GuidObjet + "'");
            }
            else CBReaderClose();
        }

        public void DeleteGObjetEtPoint(Tool tool, string sGuidGVue)
        {
            string Select, From, Where;
            string sType = tool.GetType().Name.Substring("Tool".Length);

            ArrayList LstObjet = new ArrayList();

            Select = "SELECT GuidG" + sType;
            From = tool.GetFromG(sType);
            Where = tool.GetWhereG(sType, sGuidGVue);

            if (CBRecherche(Select + " " + From + " " + Where))
            {
                while (Reader.Read()) LstObjet.Add(Reader.GetString(0));
                CBReaderClose();
                for (int i = 0; i < LstObjet.Count; i++)
                {
                    DeleteGPoint((string)LstObjet[i]);
                    CBWrite("DELETE FROM G" + sType + "  WHERE GuidG" + sType + "='" + (string)LstObjet[i] + "'");
                }
            }
            else CBReaderClose();
        }

    }
}
