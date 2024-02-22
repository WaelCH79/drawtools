using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Microsoft.Office.Interop.Excel;
using System.Xml;

namespace DrawTools
{
    
    public partial class FormProduct : Form
    {
        static public object missing = System.Type.Missing;
        static public int InitNbrCol = 6;
        private Form1 parent;
        private ArrayList lstTechnoArea;
        private bool InsertProduitRowFromDB;
        private bool InsertTechnoRefRowFromDB;
        private char switchTechFonc;
        //private ArrayList lstcol;
        public Produit oProduit;
        public TechnoRef oTechnoRef;
        public Microsoft.Office.Interop.Excel.Application oXL;
        private ControlDoc cw;
        public _Workbook oWB;
        public _Worksheet oSheet;
        private List<string> lstLibTabTechno;
        private List<string> lstLibTabProduit;
        private List<string[]> lstCadreRef;

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

        private void InitCadreRef(TreeNodeCollection tn, string guidParent)
        {
            ArrayList guidCadreRef = new ArrayList();
            ArrayList NomCadreRef = new ArrayList();
            string sSelect="";

            switch (switchTechFonc)
            {
                case 'S':
                case 'H':
                    sSelect = "Select GuidCadreRef, NomCadreRef FROM CadreRef WHERE GuidParent='" + guidParent + "' AND (TypeCadreRef='" + switchTechFonc + "' OR TypeCadreRef IS NULL)";
                    break;
                case 'A':
                    sSelect = "Select GuidCadreRefApp, NomCadreRefApp FROM CadreRefApp WHERE GuidParentApp='" + guidParent + "'";
                    break;
            }

            if(Parent.oCnxBase.CBRecherche(sSelect))
            {
                while(Parent.oCnxBase.Reader.Read()) {
                    guidCadreRef.Add((object) Parent.oCnxBase.Reader.GetString(0));
                    NomCadreRef.Add((object) Parent.oCnxBase.Reader.GetString(1));
                }
            }
            Parent.oCnxBase.CBReaderClose();
            for(int i=0; i<guidCadreRef.Count; i++) {
                tn.Add((string) guidCadreRef[i], (string) NomCadreRef[i]);
                InitCadreRef(tn[tn.Count-1].Nodes, (string)guidCadreRef[i]);
            }
        }

        private bool verifyCol(List<string> lstTab, List<string> lstTabRef)
        {
            int i = 0;
            while ((string)((Range)oSheet.Cells[1, ++i]).Value2 != null)
                lstTab.Add((string)((Range)oSheet.Cells[1, i]).Value2);
            for (i = 0; i < lstTabRef.Count; i++)
            {
                if (lstTab.FindIndex(elFind => elFind == lstTabRef[i]) < 0)
                    return false;
            }
            return true;
        }

        private void bImpPdt_Click(object sender, EventArgs e)
        {
            fileDialog1.DefaultExt = "xlsx";
            fileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx";
            List<string> lstTabFileImport = new List<string>();
            LogFile LogF = new LogFile(Parent, @"C:\DAT\ImportProduit.log");

            LogF.SWwriteLog(0, "Creation de la Log");

            if (fileDialog1.ShowDialog() == DialogResult.OK)
            {
                //tbRootPath.Text = folderBrowserDialog1.SelectedPath;
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-us");
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = true;
                oWB = oXL.Workbooks.Open(fileDialog1.FileName, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
                oSheet = (_Worksheet)oWB.ActiveSheet;
                LogF.SWwriteLog(0, "Ouverture du fichier xls : " + fileDialog1.FileName);
                if (verifyCol(lstTabFileImport, lstLibTabProduit))
                {
                    LogF.SWwriteLog(0, "format fichier ok");
                    int row = 1;
                    
                    while ((string)((Range)oSheet.Cells[++row, 2]).Value2 != null)
                    {
                        LogF.SWwriteLog(0, "Tratement de la ligne :" + row);
                        List<string> lstRowData = new List<string>();
                        for (int i = 0; i < lstLibTabProduit.Count; i++)
                        {
                            DateTime d;
                            int iCol = lstTabFileImport.FindIndex(elFind => elFind == lstLibTabProduit[i]) + 1;
                            object oVal = (oSheet.Cells[row, iCol]).Value2;
                            if (oVal != null)
                            {
                                switch (oVal.GetType().Name)
                                {
                                    case "Double":
                                        lstRowData.Add(((double)oVal).ToString());
                                        break;
                                    case "String":
                                    default:

                                        try
                                        {
                                            d = DateTime.ParseExact((string)oVal, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                            lstRowData.Add(d.ToOADate().ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            lstRowData.Add((string)oVal);
                                        }
                                        break;
                                }
                            }
                            else lstRowData.Add("");
                        }
                        ImportProduit(LogF, row, lstRowData, lstLibTabProduit);
                        LogF.SWwriteLog(0, "Fin de Traitement de la ligne", true);
                    }
                } else LogF.SWwriteLog(0, "le fichier n'est pas au bon format");

                LogF.SWclose();
                Close();
            }
        }

        private void bImpTech_Click(object sender, EventArgs e)
        {
            fileDialog1.DefaultExt = "xlsx";
            fileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx";
            LogFile LogF = new LogFile(Parent, @"C:\DAT\ImportTechno.log");
            List<string> lstTabFileImport = new List<string>();

            LogF.SWwriteLog(0, "Creation de la Log");

            if (fileDialog1.ShowDialog() == DialogResult.OK)
            {
                //tbRootPath.Text = folderBrowserDialog1.SelectedPath;
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-us");
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = true;
                oWB = oXL.Workbooks.Open(fileDialog1.FileName, missing,missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
                oSheet = (_Worksheet)oWB.ActiveSheet;
                LogF.SWwriteLog(0, "Ouverture du fichier xls : " + fileDialog1.FileName);
                if (verifyCol(lstTabFileImport, lstLibTabTechno))
                {
                    LogF.SWwriteLog(0, "format fichier ok");
                    int row = 1;
                    
                    LogF.SWwriteLog(0, "Creation de la Log");
                    while ((string)((Range)oSheet.Cells[++row, 2]).Value2 != null)
                    {
                        LogF.SWwriteLog(0, "Tratement de la ligne :" + row);
                        List<string> lstRowData = new List<string>();
                        for (int i = 0; i < lstLibTabTechno.Count; i++)
                        {
                            DateTime d;
                            int iCol = lstTabFileImport.FindIndex(elFind => elFind == lstLibTabTechno[i]) + 1;
                            object oVal = (oSheet.Cells[row, iCol]).Value2;
                            if (oVal != null)
                            {
                                switch (oVal.GetType().Name)
                                {
                                    case "Double":
                                        lstRowData.Add(((double)oVal).ToString());
                                        break;
                                    case "String":
                                    default:

                                        try
                                        {
                                            d = DateTime.ParseExact((string)oVal, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                            lstRowData.Add(d.ToOADate().ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            lstRowData.Add((string)oVal);
                                        }
                                        break;
                                }
                            }
                            else lstRowData.Add("");
                        }
                        Import(LogF, row, lstRowData, lstLibTabTechno);
                        LogF.SWwriteLog(0, "Fin de Traitement de la ligne", true);
                    }
                    

                } else LogF.SWwriteLog(0, "le fichier n'est pas au bon format");
                LogF.SWclose();
                Close();
            }
        }

        private bool Import(LogFile LogF, int row, List<string> lstRowData, List<string> lstTabLib)
        {
            //GuidTechnoRef, TechnologyName, TechnologyVersion, TechnologyType, ObsoScore, DerogationEndDate, GroupITStandardInCompetition, 
            //RoadmapUpcomingStartDate, RoadmapUpcomingEndDate, RoadmapReferenceStartDate, RoadmapReferenceEndDate, RoadmapConfinedStartDate,
            //RoadmapConfinedEndDate, RoadmapDecommissionedStartDate, RoadmapDecommissionedEndDate, RoadmapSupplierEndOfSupportDate, UserID, UpdateDate

            int iCol = 0;
            Double dSupplierEndOfSupportDate = 0;


            LogF.SWwriteLog(1, "row:" + row + " Creation Objet TechnoRef");
            DrawTechnoRef dwTechnoRef = new DrawTechnoRef(Parent);

            // Guid
            iCol = lstTabLib.FindIndex(elFind => elFind == "GuidTechnoRef");
            if (iCol < 0 || lstRowData[iCol].Length == 0)
            {
                LogF.SWwriteLog(1, "row:" + row + " Erreur - Champ TechnologyName");
                return false;
            }
            dwTechnoRef.SetValueFromName("GuidTechnoRef", (object)lstRowData[iCol]);
            dwTechnoRef.GuidkeyObjet = new Guid((string)dwTechnoRef.GetValueFromName("GuidTechnoRef"));
            LogF.SWwriteLog(1, "row:" + row + " Guid de la TechnoRef : " + dwTechnoRef.GetValueFromName("GuidTechnoRef"));

            // TechnologyName
            iCol = lstTabLib.FindIndex(elFind => elFind == "TechnologyName");
            if( iCol < 0 || lstRowData[iCol].Length == 0)
            {
                LogF.SWwriteLog(1, "row:" + row + " Erreur - Champ TechnologyName");
                return false;
            }
            dwTechnoRef.SetValueFromName("NomTechnoRef", (object)lstRowData[iCol]);
            LogF.SWwriteLog(1, "row:" + row + " Nom de la Techno : " + dwTechnoRef.GetValueFromName("NomTechnoRef"));

            
            iCol = lstTabLib.FindIndex(elFind => elFind == "TechnologyVersion");
            if (iCol < 0 || lstRowData[iCol].Length == 0)
            {
                LogF.SWwriteLog(1, "row:" + row + " Erreur - Champ TechnologyVersion");
                return false;
            }
            dwTechnoRef.SetValueFromName("Version", (object)lstRowData[iCol]);

            iCol = lstTabLib.FindIndex(elFind => elFind == "GuidProduit");
            if (iCol < 0 || lstRowData[iCol].Length == 0)
            {
                LogF.SWwriteLog(1, "row:" + row + " Erreur - Champ GuidProduit");
                return false;
            }
            dwTechnoRef.SetValueFromName("GuidProduit", (object)lstRowData[iCol]);
            Parent.oCnxBase.CBReaderClose();
            if (!Parent.oCnxBase.CBRecherche("Select GuidProduit From Produit Where GuidProduit='" + dwTechnoRef.GetValueFromName("GuidProduit") + "'"))
            {
                // Erreur sur le fichier
                LogF.SWwriteLog(1, "row:" + row + " Erreur - Le Guid produit n'existe pas dans le referentiel : " + dwTechnoRef.GetValueFromName("GuidProduit"));
                Parent.oCnxBase.CBReaderClose();
                return false;
            }
            Parent.oCnxBase.CBReaderClose();

            string sDate;
            iCol = lstTabLib.FindIndex(elFind => elFind == "RoadmapUpcomingStartDate");
            sDate = "UpcomingStart";
            if (iCol > -1)
            {
                if ((string)lstRowData[iCol] != "")
                {
                    dwTechnoRef.SetValueFromName(sDate, (object)lstRowData[iCol]);
                    DateTime dt = new DateTime();
                    try
                    {
                        dt = DateTime.FromOADate(Convert.ToDouble(lstRowData[iCol]));
                    }
                    catch (Exception ex)
                    {
                        LogF.SWwriteLog(1, "row:" + row + " Erreur format data :" + ex.Message);
                        return false;
                    }
                    LogF.SWwriteLog(1, "row:" + row + " " + sDate + " : " + dwTechnoRef.GetValueFromName(sDate));
                }
            }

            iCol = lstTabLib.FindIndex(elFind => elFind == "RoadmapUpcomingEndDate");
            sDate = "UpcomingEnd";
            if (iCol > -1)
            {
                if ((string)lstRowData[iCol] != "")
                {
                    dwTechnoRef.SetValueFromName(sDate, (object)lstRowData[iCol]);
                    DateTime dt = new DateTime();
                    try
                    {
                        dt = DateTime.FromOADate(Convert.ToDouble(lstRowData[iCol]));
                    }
                    catch (Exception ex)
                    {
                        LogF.SWwriteLog(1, "row:" + row + " Erreur format data :" + ex.Message);
                        return false;
                    }
                    LogF.SWwriteLog(1, "row:" + row + " " + sDate + " : " + dwTechnoRef.GetValueFromName(sDate));
                }
            }

            iCol = lstTabLib.FindIndex(elFind => elFind == "RoadmapReferenceStartDate");
            sDate = "ReferenceStart";
            if (iCol > -1)
            {
                if ((string)lstRowData[iCol] != "")
                {
                    dwTechnoRef.SetValueFromName(sDate, (object)lstRowData[iCol]);
                    DateTime dt = new DateTime();
                    try
                    {
                        dt = DateTime.FromOADate(Convert.ToDouble(lstRowData[iCol]));
                    }
                    catch (Exception ex)
                    {
                        LogF.SWwriteLog(1, "row:" + row + " Erreur format data :" + ex.Message);
                        return false;
                    }
                    LogF.SWwriteLog(1, "row:" + row + " " + sDate + " : " + dwTechnoRef.GetValueFromName(sDate));
                }
            }

            iCol = lstTabLib.FindIndex(elFind => elFind == "RoadmapReferenceEndDate");
            sDate = "ReferenceEnd";
            if (iCol > -1)
            {
                if ((string)lstRowData[iCol] != "")
                {
                    dwTechnoRef.SetValueFromName(sDate, (object)lstRowData[iCol]);
                    DateTime dt = new DateTime();
                    try
                    {
                        dt = DateTime.FromOADate(Convert.ToDouble(lstRowData[iCol]));
                    }
                    catch (Exception ex)
                    {
                        LogF.SWwriteLog(1, "row:" + row + " Erreur format data :" + ex.Message);
                        return false;
                    }
                    LogF.SWwriteLog(1, "row:" + row + " " + sDate + " : " + dwTechnoRef.GetValueFromName(sDate));
                }
            }

            iCol = lstTabLib.FindIndex(elFind => elFind == "RoadmapConfinedStartDate");
            sDate = "ConfinedStart";
            if (iCol > -1)
            {
                if ((string)lstRowData[iCol] != "")
                {
                    dwTechnoRef.SetValueFromName(sDate, (object)lstRowData[iCol]);
                    DateTime dt = new DateTime();
                    try
                    {
                        dt = DateTime.FromOADate(Convert.ToDouble(lstRowData[iCol]));
                    }
                    catch (Exception ex)
                    {
                        LogF.SWwriteLog(1, "row:" + row + " Erreur format data :" + ex.Message);
                        return false;
                    }
                    LogF.SWwriteLog(1, "row:" + row + " " + sDate + " : " + dwTechnoRef.GetValueFromName(sDate));
                } else
                {
                    LogF.SWwriteLog(1, "row:" + row + " " + sDate + " est absente");
                    return false;
                }
            }

            iCol = lstTabLib.FindIndex(elFind => elFind == "RoadmapConfinedEndDate");
            sDate = "ConfinedEnd";
            if (iCol > -1)
            {
                if ((string)lstRowData[iCol] != "")
                {
                    dwTechnoRef.SetValueFromName(sDate, (object)lstRowData[iCol]);
                    DateTime dt = new DateTime();
                    try
                    {
                        dt = DateTime.FromOADate(Convert.ToDouble(lstRowData[iCol]));
                    }
                    catch (Exception ex)
                    {
                        LogF.SWwriteLog(1, "row:" + row + " Erreur format data :" + ex.Message);
                        return false;
                    }
                    LogF.SWwriteLog(1, "row:" + row + " " + sDate + " : " + dwTechnoRef.GetValueFromName(sDate));
                }
                else
                {
                    LogF.SWwriteLog(1, "row:" + row + " " + sDate + " est absente");
                    return false;
                }
            }
 
            iCol = lstTabLib.FindIndex(elFind => elFind == "RoadmapDecommissionedStartDate");
            sDate = "DecommStart";
            if (iCol > -1)
            {
                if ((string)lstRowData[iCol] != "")
                {
                    dwTechnoRef.SetValueFromName(sDate, (object)lstRowData[iCol]);
                    DateTime dt = new DateTime();
                    try
                    {
                        dt = DateTime.FromOADate(Convert.ToDouble(lstRowData[iCol]));
                    }
                    catch (Exception ex)
                    {
                        LogF.SWwriteLog(1, "row:" + row + " Erreur format data :" + ex.Message);
                        return false;
                    }
                    LogF.SWwriteLog(1, "row:" + row + " " + sDate + " : " + dwTechnoRef.GetValueFromName(sDate));
                }
                else
                {
                    LogF.SWwriteLog(1, "row:" + row + " " + sDate + " est absente");
                    return false;
                }
            }

            iCol = lstTabLib.FindIndex(elFind => elFind == "RoadmapDecommissionedEndDate");
            sDate = "DecommEnd";
            if (iCol > -1)
            {
                if ((string)lstRowData[iCol] != "")
                {
                    dwTechnoRef.SetValueFromName(sDate, (object)lstRowData[iCol]);
                    DateTime dt = new DateTime();
                    try
                    {
                        dt = DateTime.FromOADate(Convert.ToDouble(lstRowData[iCol]));
                    }
                    catch (Exception ex)
                    {
                        LogF.SWwriteLog(1, "row:" + row + " Erreur format data :" + ex.Message);
                        return false;
                    }
                    /*
                    if (dt.Day != 23)
                    {
                        LogF.SWwriteLog(1, "row:" + row + " Erreur - La valeur " + sDate + " n'est pas une date ou n'est pas conforne");
                        return false;
                    }
                    */
                    LogF.SWwriteLog(1, "row:" + row + " " + sDate + " : " + dwTechnoRef.GetValueFromName(sDate));
                }
                else
                {
                    LogF.SWwriteLog(1, "row:" + row + " " + sDate + " est absente");
                    return false;
                }
            }

            //Date fin de support
            iCol = lstTabLib.FindIndex(elFind => elFind == "RoadmapSupplierEndOfSupportDate");
            if (iCol < 0 || lstRowData[iCol].Length == 0)
            {
                //Erreur sur le fichier
                LogF.SWwriteLog(1, "row:" + row + " Erreur - Champ RoadmapSupplierEndOfSupportDate");
                return false;
            }
            try
            {
                dSupplierEndOfSupportDate = Convert.ToDouble(lstRowData[iCol]);
            }
            catch (Exception ex)
            {
                LogF.SWwriteLog(1, "row:" + row + " Erreur format data :" + ex.Message);
                return false;
            }
            LogF.SWwriteLog(1, "row:" + row + " " + sDate + " : " + dSupplierEndOfSupportDate);
            
            LogF.SWwriteLog(1, "row:" + row + " Savegarde object TechnoRef");
            dwTechnoRef.saveobjtoDB();
                        
            //Enregistrement de la date de fin de support
            //TechnoRef.GuidTechnoRef = IndicatorLink.GuidObjet and IndicatorLink.GuidIndicator = Indicator.GuidIndicator and NomProp = 'Usage' and NomIndicator = '1-Fin Support'
            // 1-Fin Support --> b00b12bd-a447-47e6-92f6-e3b76ad22830
            if (!Parent.oCnxBase.CBRecherche("Select GuidObjet From IndicatorLink  Where GuidIndicator = 'b00b12bd-a447-47e6-92f6-e3b76ad22830' and GuidObjet='" + dwTechnoRef.GetValueFromName("GuidTechnoRef") + "'"))
            {
                Parent.oCnxBase.CBReaderClose();
                // Nouveau Fin de support
                LogF.SWwriteLog(1, "row:" + row + " Create Date fin support");
                Parent.oCnxBase.CBWrite("INSERT INTO IndicatorLink (GuidObjet, GuidIndicator, ValIndicator) VALUES ('" + dwTechnoRef.GetValueFromName("GuidTechnoRef") + "','b00b12bd-a447-47e6-92f6-e3b76ad22830'," + dSupplierEndOfSupportDate + ")");
            }
            else
            {
                // Mise à jour Fin de Support
                Parent.oCnxBase.CBReaderClose();
                LogF.SWwriteLog(1, "row:" + row + " Mise à jour Date fin support");
                Parent.oCnxBase.CBWrite("UPDATE IndicatorLink SET ValIndicator= " + dSupplierEndOfSupportDate + " Where GuidObjet='" + dwTechnoRef.GetValueFromName("GuidTechnoRef") + "' and GuidIndicator='b00b12bd-a447-47e6-92f6-e3b76ad22830'");
            }

            return true;
        }

        private bool ImportProduit(LogFile LogF, int row, List<string> lstRowData, List<string> lstTabLib)
        {
            //GuidProduit, NomProduit Supplier Scope, SupportTeam, Entity, SubEntity, MandatedEntityForSupport, 
            //Root, Family, Subfamily, Topic, TechnologyArea, UseCase, Comments, ShowOrHide, Catalogue, UserID, UpdateDate

            int iCol = 0;
            //Double dSupplierEndOfSupportDate = 0;
            string sUseCase = null, sGuidCadreRef = null;


            LogF.SWwriteLog(1, "row:" + row + " Creation Objet Produit");
            DrawProduit dwProduit = new DrawProduit(Parent);

            // GuidProduit
            iCol = lstTabLib.FindIndex(elFind => elFind == "GuidProduit");
            if (iCol < 0 || lstRowData[iCol].Length == 0)
            {
                LogF.SWwriteLog(1, "row:" + row + " Erreur - Champ GuidProduit absent");
                return false;
            }
            dwProduit.SetValueFromName("GuidProduit", (object)lstRowData[iCol]);
            dwProduit.GuidkeyObjet = new Guid((string)dwProduit.GetValueFromName("GuidProduit"));
            LogF.SWwriteLog(1, "row:" + row + " Guid du Produit : " + dwProduit.GetValueFromName("GuidProduit"));

            // NomProduit
            iCol = lstTabLib.FindIndex(elFind => elFind == "NomProduit");
            if (iCol < 0 || lstRowData[iCol].Length == 0)
            {
                LogF.SWwriteLog(1, "row:" + row + " Erreur - Champ NomProduit absent");
                return false;
            }
            dwProduit.SetValueFromName("NomProduit", (object)lstRowData[iCol]);
            LogF.SWwriteLog(1, "row:" + row + " Nom du Produit : " + dwProduit.GetValueFromName("NomProduit"));

            // Supplier
            iCol = lstTabLib.FindIndex(elFind => elFind == "Supplier");
            if (iCol < 0 || lstRowData[iCol].Length == 0)
            {
                LogF.SWwriteLog(1, "row:" + row + " Erreur - Champ Supplier absent");
                return false;
            }
            dwProduit.SetValueFromName("Editeur", (object)lstRowData[iCol]);
            LogF.SWwriteLog(1, "row:" + row + " Editeur : " + dwProduit.GetValueFromName("Editeur"));

            
            //Use Case
            iCol = lstTabLib.FindIndex(elFind => elFind == "UseCase");
            if (iCol < 0 || lstRowData[iCol].Length == 0)
            {
                LogF.SWwriteLog(1, "row:" + row + " Erreur - Champ UseCase inexistant ou vide");
                return false;
            }
            sUseCase = lstRowData[iCol];


            //Cadre de référence : GuidCadreRef de la table Produit
            iCol = lstTabLib.FindIndex(elFind => elFind == "Family");
            if (iCol < 0 || lstRowData[iCol].Length == 0)
            {
                //Erreur sur le fichier
                LogF.SWwriteLog(1, "row:" + row + " Erreur - Champ Family absent");
                return false;
            }
            Parent.oCnxBase.CBReaderClose();
            if (!Parent.oCnxBase.CBRecherche("Select GuidCadreRef From CadreRef Where NomCadreRef='" + lstRowData[iCol] + "'"))
            {
                // Erreur sur le fichier
                LogF.SWwriteLog(1, "row:" + row + " Erreur - Champ Family n'existe pas dans le referentiel : " + lstRowData[iCol]);
                Parent.oCnxBase.CBReaderClose();
                return false;
            }
            sGuidCadreRef = Parent.oCnxBase.Reader.GetString(0);
            Parent.oCnxBase.CBReaderClose();
            iCol = lstTabLib.FindIndex(elFind => elFind == "SubFamily");
            if (iCol < 0 || lstRowData[iCol].Length == 0)
            {
                //Erreur sur le fichier
                LogF.SWwriteLog(1, "row:" + row + " Erreur - Champ SubFamily absent");
                return false;
            }
            if (!Parent.oCnxBase.CBRecherche("Select GuidCadreRef From CadreRef Where GuidParent= '" + sGuidCadreRef + "' and NomCadreRef='" + lstRowData[iCol] + "'"))
            {
                LogF.SWwriteLog(1, "row:" + row + " Erreur - Champ SubFamily n'existe pas dans le referentiel : " + lstRowData[iCol]);
                Parent.oCnxBase.CBReaderClose();
                return false;
            }
            sGuidCadreRef = Parent.oCnxBase.Reader.GetString(0);
            Parent.oCnxBase.CBReaderClose();
            iCol = lstTabLib.FindIndex(elFind => elFind == "Topic");
            if (iCol < 0 || lstRowData[iCol].Length == 0)
            {
                // Erreur sur le Fichier
                LogF.SWwriteLog(1, "row:" + row + " Erreur - Pas de Champ Topic absent");
                return false;
            }
            if (!Parent.oCnxBase.CBRecherche("Select GuidCadreRef From CadreRef Where GuidParent= '" + sGuidCadreRef + "' and NomCadreRef='" + lstRowData[iCol] + "'"))
            {
                LogF.SWwriteLog(1, "row:" + row + " Erreur - Champ Topic n'existe pas dans le referentiel : " + lstRowData[iCol]);
                Parent.oCnxBase.CBReaderClose();
                return false;
            }
            sGuidCadreRef = Parent.oCnxBase.Reader.GetString(0);
            Parent.oCnxBase.CBReaderClose();
            dwProduit.SetValueFromName("GuidCadreRef", sGuidCadreRef);

            //Technologie Area : GuidTechnoArea de la table TechnoArea
            iCol = lstTabLib.FindIndex(elFind => elFind == "TechnologyArea");
            if (iCol < 0 || lstRowData[iCol].Length == 0)
            {
                //Erreur sur le fichier
                LogF.SWwriteLog(1, "row:" + row + " Erreur - TechnologyArea absent");
                return false;
            }
            if (!Parent.oCnxBase.CBRecherche("Select GuidTechnoArea From TechnoArea Where NomTechnoArea='" + lstRowData[iCol] + "'"))
            {
                // Erreur sur le fichier
                LogF.SWwriteLog(1, "row:" + row + " Erreur - Champ TechnologieArea n'existe pas dans le referentiel : " + lstRowData[iCol]);
                Parent.oCnxBase.CBReaderClose();
                return false;
            }
            dwProduit.SetValueFromName("GuidTechnoArea", Parent.oCnxBase.Reader.GetString(0));
            Parent.oCnxBase.CBReaderClose();

            LogF.SWwriteLog(1, "row:" + row + " Sauvegarde object Produit");
            dwProduit.saveobjtoDB();

            /*
            if (!Parent.oCnxBase.CBRecherche("Select Produit.GuidProduit From Produit Where Produit.GuidProduit='" + dwProduit.GuidkeyObjet + "'"))
            {
                Parent.oCnxBase.CBReaderClose();
                // Nouveau Produit
                LogF.SWwriteLog(1, "row:" + row + " Nouveau Produit");
                
                
            }
            else
            {
                
                
            }
            Parent.oCnxBase.CBReaderClose();
            */
            
            //Enregistrement du use case
            // Select HyperLien, Size, RichText From Comment Where GuidObject = '705144ef-30f1-42d3-a1bd-f505096c7e2d' AND NomProp='Usage'

            System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
            rtBox.Text = sUseCase;
            if (rtBox.Text != "")
            {
                byte[] rawData = System.Text.Encoding.UTF8.GetBytes(rtBox.Rtf);
                int iSize = rawData.Length;

                //Mise à jour des tables pour une techno.
                if (!Parent.oCnxBase.CBRecherche("Select GuidObject From Comment Where GuidObject='" + dwProduit.GetValueFromName("GuidProduit") + "' and NomProp='Usage'"))
                {
                    Parent.oCnxBase.CBReaderClose();
                    // Nouveau Use Case
                    LogF.SWwriteLog(1, "row:" + row + " Creation Use Case");
                    Parent.oCnxBase.CBWriteWithObj("INSERT INTO Comment (GuidObject, NomProp, Size, RichText, Policy) VALUES ('" + (string)dwProduit.GetValueFromName("GuidProduit") + "','Usage'," + iSize + ", ?, 'L')", rawData);
                }
                else
                {
                    // Mise à jour Use Case
                    LogF.SWwriteLog(1, "row:" + row + " Mise à jour Use Case");
                    Parent.oCnxBase.CBReaderClose();
                    Parent.oCnxBase.CBWriteWithObj("UPDATE Comment Set Size=" + iSize + ", RichText= ? Where GuidObject='" + (string)dwProduit.GetValueFromName("GuidProduit") + "' and NomProp='Usage'", rawData);

                }
            }
            return true;
        }

        private int get_level(int row, int level)
        {
            if(level==7) return 0;
            string sName = (string)((Range)oSheet.Cells[row, level + InitNbrCol]).Value2;
            if (sName == null) return get_level(row, level + 1); else return level;
        }

        private int Import(string sparent, int row, int levelC)
        {
            int idiff = levelC - get_level(row, 1);
            while (idiff == 0) // même niveau
            {
                string sGuid = (string)((Range)oSheet.Cells[row, 1]).Value2;
                string sName = (string)((Range)oSheet.Cells[row, levelC + InitNbrCol]).Value2;
                if (levelC < 5) // check CadreRef
                {
                    if (Parent.oCnxBase.CBRecherche("SELECT NomCadreRef, GuidParent FROM CadreRef WHERE GuidCadreRef='" + sGuid +"'"))
                    {
                        Parent.oCnxBase.Reader.Read();
                        if (sName != Parent.oCnxBase.Reader.GetString(0) || sparent != Parent.oCnxBase.Reader.GetString(1)) row = 0;
                    }
                    Parent.oCnxBase.CBReaderClose();
                }
                else if (levelC == 5) // update ou insere Produit
                {
                    if (sGuid == null)
                    {
                        sGuid = Guid.NewGuid().ToString();
                        Parent.oCnxBase.CBWrite("INSERT INTO Produit (GuidProduit, NomProduit, Editeur, GuidCadreRef) VALUES ('" + sGuid + "', '" + sName + "', '" + (string)((Range)oSheet.Cells[row, levelC + InitNbrCol + 1]).Value2 + "', '" + sparent + "')");
                    }
                    else
                        Parent.oCnxBase.CBWrite("UPDATE Produit SET NomProduit='" + sName + "', Editeur='" + (string)((Range)oSheet.Cells[row, levelC + InitNbrCol + 1]).Value2 + "', GuidCadreRef='" + sparent + "' WHERE GuidProduit='" + sGuid + "'");
                }
                else if (levelC == 6)
                {
                    if (sGuid == null) // update ou insere TechnoRef
                        Parent.oCnxBase.CBWrite("INSERT INTO TechnoRef (GuidTechnoRef, NomTechnoRef, Version, Norme, GuidProduit) VALUES ('" + Guid.NewGuid().ToString() + "', '" + sName + "', '" + ((Range)oSheet.Cells[row, levelC + InitNbrCol + 1]).Value2.ToString() + "', " + (string)((Range)oSheet.Cells[row, levelC + InitNbrCol + 3]).Value2.ToString() + ", '" + sparent + "')");
                    else
                        Parent.oCnxBase.CBWrite("UPDATE TechnoRef SET NomTechnoRef='" + sName + "', Version='" + (string)((Range)oSheet.Cells[row, levelC + InitNbrCol + 1]).Value2.ToString() + "', Norme=" + (string)((Range)oSheet.Cells[row, levelC + InitNbrCol + 3]).Value2.ToString() + ", GuidProduit='" + sparent + "' WHERE GuidTechnoRef='" + sGuid + "'");

                }
                
                idiff = levelC - get_level(++row, 1);
                if (idiff < 0) // niveau sup
                {
                    row = Import(sGuid, row, levelC + 1);
                    idiff = levelC - get_level(row, 1);
                }
                /*else if (idiff == levelC) // erreur
                {
                }
                else if (idiff > 0) // niveau inf
                {
                }*/
            }
            return row;
        }

        
        private  int Export(TreeNodeCollection tn,  int row, int col)
        {
            if (row == 1)
            {
                for (int i = 0; i < lstLibTabTechno.Count; i++)
                    oSheet.Cells[row, i + 1] = lstLibTabTechno[i];
            }
            for (int i = 0; i < tn.Count; i++)
            {
                /*switch (col)
                {
                    case 2:
                        ((Range)oSheet.Cells[row, col + InitNbrCol]).Font.Bold = true;
                        break;
                    case 3:
                        ((Range)oSheet.Cells[row, col + InitNbrCol]).Font.Italic = true;
                        break;
                    case 4:
                        ((Range)oSheet.Cells[row, col + InitNbrCol]).Font.Underline = true;
                        break;
                }
                ((Range)oSheet.Cells[row, 1]).EntireRow.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);*/


                //GuidTechnoRef, TechnologyName, TechnologyVersion, TechnologyType, ObsoScore, GuidProduit, DerogationEndDate, GroupITStandardInCompetition, 
                //RoadmapUpcomingStartDate, RoadmapUpcomingEndDate, RoadmapReferenceStartDate, RoadmapReferenceEndDate, RoadmapConfinedStartDate,
                //RoadmapConfinedEndDate, RoadmapDecommissionedStartDate, RoadmapDecommissionedEndDate, RoadmapSupplierEndOfSupportDate, UserID, UpdateDate

                if (tn[i].Nodes.Count > 0) row = Export(tn[i].Nodes, row, col + 1);
                else
                {
                    string requete = "";
                    requete += "SELECT distinct";
                    requete += "   GuidTechnoRef, NomTechnoRef, Version, Norme, obsoLink.Valindicator, produit.guidproduit, ' ', ' ',";
                    requete += "   UpComingStart, UpComingEnd, ReferenceStart, ReferenceEnd, ConfinedStart, ConfinedEnd, DecommStart, DecommEnd, finsupportLink.valindicator ";
                    requete += "FROM ";
                    requete += "   TechnoRef, IndicatorLink finsupportLink, Indicator finsupport, indicatorlink obsolink, indicator obso, Produit ";
                    requete += "WHERE ";
                    requete += "   Produit.GuidProduit=TechnoRef.GuidProduit and ";
                    requete += "   TechnoRef.GuidTechnoRef=finsupportLink.GuidObjet and finsupportLink.GuidIndicator=finsupport.GuidIndicator and finsupport.NomIndicator = '1-Fin Support' and ";
                    requete += "   TechnoRef.GuidTechnoRef = obsoLink.GuidObjet and obsolink.GuidIndicator = obso.GuidIndicator and obso.nomindicator = '9-Obsolescence' and ";
                    requete += "   GuidCadreRef='" + tn[i].Name + "' ORDER BY Produit.GuidProduit";

                    row = Parent.oCnxBase.fillTechnoXls(requete , oSheet, lstCadreRef, row, col);
                }
            }
            return row;
        }

        private void bExport_Click(object sender, EventArgs e)
        {
            //Range oRng;
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-us");
            oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.Visible = true;
            oWB = oXL.Workbooks.Add(missing);
            oSheet = (_Worksheet)oWB.ActiveSheet;
            int row = Export(tvCadreRef.Nodes,1,1);
            //oSheet.Cells[++row, 1] = "00000000-0000-0000-0000-000000000000";

        }

        public void initEvent()
        {
            this.bCalcIndicator.Click += new System.EventHandler(this.bCalcIndicator_Click);
            this.bVersion.Click += new System.EventHandler(this.bVersion_Click);
            this.bCatalogue.Click += new System.EventHandler(this.bCatalogue_Click);
            this.bImpTech.Click += new System.EventHandler(this.bImpTech_Click);
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            this.bExport.Click += new System.EventHandler(this.bExport_Click);
            this.tvCadreRef.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvCadreRef_AfterSelect);
            this.bImpPdt.Click += new System.EventHandler(this.bImpPdt_Click);
        }

        public FormProduct(Form1 p, char switchtechfonc)
        {
            Parent = p;
            InitializeComponent();
            initEvent();
            Compte.SetRight(this);
            switchTechFonc = switchtechfonc;
            lstTechnoArea = new ArrayList();
            //lstcol = new ArrayList();
            lstLibTabTechno = new List<string>();
            lstLibTabProduit = new List<string>();
            parent.initlstTabTechno(lstLibTabTechno);
            parent.initlstTabProduit(lstLibTabProduit);
            lstCadreRef = parent.oCnxBase.InitCadreRef();

            switch (switchTechFonc)
            {
                case 'S':
                case 'H':
                    oProduit = new Produit(p, dgProduit, "Produit");
                    oTechnoRef = new TechnoRef(p, dgTechnoRef, "TechnoRef");
                    Parent.oCnxBase.CBAddArrayListcObj("SELECT GuidTechnoArea, NomTechnoArea FROM TechnoArea ORDER BY NomTechnoArea", lstTechnoArea);
                    cbTechnoArea.DataSource = lstTechnoArea;
                    cbTechnoArea.DisplayMember = "Name";
                    cbTechnoArea.ValueMember = "This";
                    dgProduit.Columns[0].HeaderCell.ToolTipText = "GuidProduit";
                    dgProduit.Columns[1].HeaderCell.ToolTipText = "NomProduit";
                    dgProduit.Columns[4].HeaderCell.ToolTipText = "GuidCadreRef";
                    dgTechnoRef.Columns[0].HeaderCell.ToolTipText = "GuidTechnoRef";
                    dgTechnoRef.Columns[1].HeaderCell.ToolTipText = "NomTechnoRef";
                    dgTechnoRef.Columns[6].HeaderCell.ToolTipText = "GuidProduit";

                    break;
                case 'A':
                    dgTechnoRef.Columns.Remove("NormeG");
                    dgTechnoRef.Columns.Remove("Norme");
                    dgTechnoRef.Columns.Remove("IndexImgOS");
                    dgTechnoRef.Columns.Remove("ImgOs");
                    dgTechnoRef.Columns.Remove("Indicator");
                    dgTechnoRef.Columns.Remove("UpComingStart");
                    dgTechnoRef.Columns.Remove("UpComingEnd");
                    dgTechnoRef.Columns.Remove("ReferenceStart");
                    dgTechnoRef.Columns.Remove("ReferenceEnd");
                    dgTechnoRef.Columns.Remove("ConfinedStart");
                    dgTechnoRef.Columns.Remove("ConfinedEnd");
                    dgTechnoRef.Columns.Remove("DecommStart");
                    dgTechnoRef.Columns.Remove("DecommEnd");
                    dgProduit.Columns.Remove("cbTechnoArea");
                    dgProduit.Columns[0].HeaderCell.ToolTipText = "GuidProduitApp";
                    dgProduit.Columns[1].HeaderCell.ToolTipText = "NomProduitApp";
                    dgProduit.Columns[4].HeaderCell.ToolTipText = "GuidCadreRefApp";
                    dgTechnoRef.Columns[0].HeaderCell.ToolTipText = "GuidMainComposantRef";
                    dgTechnoRef.Columns[1].HeaderCell.ToolTipText = "NomMainComposantRef";
                    dgTechnoRef.Columns[3].HeaderCell.ToolTipText = "GuidProduitApp";

                    oProduit = new Produit(p, dgProduit, "ProduitApp");
                    oTechnoRef = new TechnoRef(p, dgTechnoRef, "MainComposantRef");
                    break;
            }
            InsertProduitRowFromDB = false;
            InsertTechnoRefRowFromDB = false;
            
            InitCadreRef(tvCadreRef.Nodes, "Root");
            tvCadreRef.SelectedNode = tvCadreRef.Nodes[0];
        }

        public void InitMainComposantToTextBox(DrawMainComposant dm, bool bbup)
        {
            tGuidMainComposant.Text = (string) dm.GetValueFromName("GuidMainComposant");
            tNom.Text = (string)dm.GetValueFromName("NomMainComposant");
            tEditeur.Text = (string)dm.GetValueFromName("Editeur");
            bUp.Enabled = bbup;
        }

        void tvCadreRef_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            InsertProduitRowFromDB = true;
            dgProduit.Rows.Clear();
            dgTechnoRef.Rows.Clear();
            //oProduit.LoadEnreg("Where GuidCadreRef='" + tvCadreRef.SelectedNode.Name + "'");
            oProduit.LoadEnreg(tvCadreRef.SelectedNode.Name);
            InsertProduitRowFromDB = false;

            //MessageBox.Show(tvCadreRef.SelectedNode.Name);
            //throw new System.NotImplementedException();
        }

        void RowsAdded(Enreg enreg, object ForeinKey)
        {
            int n;
            n = Parent.oCnxBase.ConfDB.FindTable(enreg.sTable);
            if (n > -1)
            {
                Table t = (Table)Parent.oCnxBase.ConfDB.LstTable[n];
                //if (enreg.dg.ColumnCount == enreg.LstValue.Count)
                //{
                for (int i = 0; i < enreg.dg.ColumnCount; i++)
                {
                    int j = t.FindField(t.LstField, enreg.dg.Columns[i].HeaderCell.ToolTipText);
                    //if(j <= -1) j = t.FindFieldFromLib(enreg.dg.Columns[i].HeaderCell.ToolTipText);
                    if (j > -1)
                    {

                        if ((((Field)t.LstField[j]).fieldOption & ConfDataBase.FieldOption.Key) != 0)
                        {
                            if (enreg.dg.CurrentRow.Cells[i].Value == null) enreg.dg.CurrentRow.Cells[i].Value = (object)Guid.NewGuid().ToString();
                        }
                        else if ((((Field)t.LstField[j]).fieldOption & ConfDataBase.FieldOption.ForeignKey) != 0)
                        {
                            if (enreg.dg.CurrentRow.Cells[i].Value == null) enreg.dg.CurrentRow.Cells[i].Value = ForeinKey;
                        }
                    }
                }
                //}
            }
        }

        void dgTechnoRef_RowsAdded(object sender, System.Windows.Forms.DataGridViewRowsAddedEventArgs e)
        {
            if (!InsertTechnoRefRowFromDB)
            {
                oTechnoRef.LstValueClear();
                int n = -1;
                switch (switchTechFonc)
                {
                    case 'S':
                    case 'H':
                        n = Parent.oCnxBase.ConfDB.FindField(oProduit.sTable, "GuidProduit");
                        break;
                    case 'A':
                        n = Parent.oCnxBase.ConfDB.FindField(oProduit.sTable, "GuidProduitApp");
                        break;
                }
                if (n > -1 && dgProduit.SelectedRows.Count == 1)
                {
                    RowsAdded(oTechnoRef, (object)dgProduit.SelectedRows[0].Cells[n].Value);
                }
            }
            //throw new System.NotImplementedException();
        }

        void dgTechnoRef_RowValidating(object sender, System.Windows.Forms.DataGridViewCellCancelEventArgs e)
        {
            oTechnoRef.SaveEnreg(e.RowIndex, false);
            //throw new System.NotImplementedException();
        }

        void dgProduit_SelectionChanged(object sender, System.EventArgs e)
        {
            if (!InsertProduitRowFromDB)
            {
                int n = -1;
                switch (switchTechFonc)
                {
                    case 'S':
                    case 'H':
                        n = Parent.oCnxBase.ConfDB.FindField(oProduit.sTable, "GuidProduit");
                        break;
                    case 'A':
                        n = Parent.oCnxBase.ConfDB.FindField(oProduit.sTable, "GuidProduitApp");
                        break;
                }
                if (n > -1 && dgProduit.SelectedRows.Count==1)
                {
                    if (oProduit.dg.SelectedRows[0].Cells[n].Value != null)
                    {
                        //MessageBox.Show("Change");
                        InsertTechnoRefRowFromDB = true;
                        dgTechnoRef.Rows.Clear();
                        //oTechnoRef.LoadEnreg("Where GuidProduit='" + (string)oProduit.dg.SelectedRows[0].Cells[n].Value + "'");
                        oTechnoRef.LoadEnreg((string)oProduit.dg.SelectedRows[0].Cells[n].Value);
                        InsertTechnoRefRowFromDB = false;
                    }
                }
            }
            //throw new System.NotImplementedException();
        }


        void dgProduit_RowsAdded(object sender, System.Windows.Forms.DataGridViewRowsAddedEventArgs e)
        {
            if (!InsertProduitRowFromDB)
            {
                oProduit.LstValueClear();
                RowsAdded(oProduit, (object)tvCadreRef.SelectedNode.Name);
            }
            //throw new System.NotImplementedException();
        }

        void dgProduit_RowValidating(object sender, System.Windows.Forms.DataGridViewCellCancelEventArgs e)
        {
            oProduit.SaveEnreg(e.RowIndex, false);
            //throw new System.NotImplementedException();
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            Guid guidCadreRef = Guid.NewGuid();

            tvCadreRef.SelectedNode.Nodes.Add(guidCadreRef.ToString(), tbNewCadreRef.Text);
            switch (switchTechFonc)
            {
                case 'S':
                case 'H':
                    Parent.oCnxBase.CBWrite("INSERT INTO CadreRef (GuidCadreRef, NomCadreRef, GuidParent) VALUES ('" + guidCadreRef.ToString() + "','" + tbNewCadreRef.Text + "','" + tvCadreRef.SelectedNode.Name + "')");
                    break;
                case 'A':
                    Parent.oCnxBase.CBWrite("INSERT INTO CadreRefApp (GuidCadreRefApp, NomCadreRefApp, GuidParentApp)VALUES ('" + guidCadreRef.ToString() + "','" + tbNewCadreRef.Text + "','" + tvCadreRef.SelectedNode.Name + "')");
                    break;
            }
        }

        private void bUp_Click(object sender, EventArgs e)
        {
            if (!InsertProduitRowFromDB)
            {
                InsertProduitRowFromDB = true;
                oProduit.LstValueClear();
                string[] row = { tGuidMainComposant.Text, tNom.Text, tEditeur.Text, tvCadreRef.SelectedNode.Name };
                oProduit.dg.Rows.Add(row);
                InsertProduitRowFromDB = false;
                oProduit.SaveEnreg(oProduit.dg.RowCount-1, true);

                DataGridView odgv;
                odgv = (DataGridView)Parent.dataGrid;
                odgv.CurrentRow.Cells[1].Value = tvCadreRef.SelectedNode.Name;
                bUp.Enabled = false;
            }
        }

        private void dgTechnoRef_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8) //Bouton Indicator
            {
                FormIndicator fi = new FormIndicator(Parent, (string)dgTechnoRef.Rows[e.RowIndex].Cells[0].Value);
                fi.ShowDialog(Parent);
            }

        }

        private void InitCriteres(ArrayList lstCriteres)
        {
            for (int i = 0; i < lstCriteres.Count; i++)
            {
                Critere oCri = (Critere)lstCriteres[i];
                oCri.Calc = false;
            }
        }

        private void EnteteFamille(TreeNode tnFamille)
        {
            string sId = "Cat" + tnFamille.Name.Replace("-", "");
            int iSlide = (int)cw.CreatSlide(sId, tnFamille.Text), iShape = -1;
            //cw.InsertStatutTechnoFromId(iSlide.ToString(), true, tn.Nodes[i].Name, null);
            // "e380a641-f809-45a5-b2ad-9814ed7c44e9"

            ArrayList lstCriteres = new ArrayList();
            lstCriteres.Add(new Critere(Parent, "b00b12bd-a447-47e6-92f6-e3b76ad22830", "1-Fin Support"));
            lstCriteres.Add(new Critere(Parent, "90aaebca-b358-45b0-9c84-81cd6c66bfdc", "2-Business"));
            lstCriteres.Add(new Critere(Parent, "590e5709-506e-4062-8281-69e7d765a3da", "4-Complexité"));
            lstCriteres.Add(new Critere(Parent, "7b45b5fd-6d9f-4c41-9bb0-e822a898f497", "5-Expertise"));

            ArrayList lstEffectif = Parent.report(tnFamille, tnFamille.Name, lstCriteres, Form1.rbTypeRecherche.Techno);
            STStyleGraph stStyle;
            Critere oCri;
            ArrayList lstIndexSeries;
            stpptforme pptF;

            // Forme Information
            pptF.x = 15; pptF.y = 80; pptF.width = 690; pptF.height = 100;
            iShape = (int)cw.InsertFormeFromId(iSlide.ToString(), true, "Information", (object)pptF);

            //Graph sur le Support
            InitCriteres(lstCriteres);
            oCri = (Critere)lstCriteres[0];
            oCri.Calc = true;
            oCri = (Critere)lstCriteres[1];
            oCri.Calc = true;
            for (int j = 0; j < lstEffectif.Count; j++) ((Effectif)lstEffectif[j]).iNiv = -1;  // tri sur la valeur agregee
            Parent.CalcAgregaEffectif(lstEffectif, lstCriteres);

            pptF.x = 15; pptF.y = 195; pptF.width = 220; pptF.height = 100;
            iShape = (int)cw.InsertFormeFromId(iSlide.ToString(), true, "Top5 Obsolescence", (object)pptF);

            stStyle.bAxeXVisible = false; stStyle.bTitreVisible = false;
            stStyle.iIndexMainSerie = 0; stStyle.b3dActif = true;
            lstIndexSeries = new ArrayList(); lstIndexSeries.Add(-1); stStyle.lstIndexSeries = lstIndexSeries;
            stStyle.iNbrMaxEffectif = 5; stStyle.bStart = false; stStyle.bLegendVisible = false;

            cw.InsertGraphBarFromId(iSlide.ToString() + "," + iShape.ToString(), true, lstEffectif, stStyle);

            //Applications liées sur les 5 derniers Technos
            ArrayList lstTechno = new ArrayList();
            int iDeb = 0;
            if (lstEffectif.Count > 5) iDeb = lstEffectif.Count - 5;
            for (int iTech = iDeb; iTech < lstEffectif.Count; iTech++)
            {
                Effectif o = (Effectif)lstEffectif[iTech];
                lstTechno.Add(o.GuidEffectif);
            }
            ArrayList lstCriteresApp = new ArrayList();
            lstCriteresApp.Add(new Critere(Parent, "b00b12bd-a447-47e6-92f6-e3b76ad22830", "1-Fin Support"));
            lstCriteresApp.Add(new Critere(Parent, "90aaebca-b358-45b0-9c84-81cd6c66bfdc", "2-Business"));

            ArrayList lstEffectifsApp = Parent.GetApplications(lstTechno, lstCriteresApp);
            Parent.report(lstEffectifsApp, lstCriteresApp, Form1.rbTypeRecherche.Application);

            InitCriteres(lstCriteres);
            oCri = (Critere)lstCriteresApp[0]; oCri.Calc = true;
            oCri = (Critere)lstCriteresApp[1]; oCri.Calc = true;
            for (int j = 0; j < lstEffectifsApp.Count; j++) ((Effectif)lstEffectifsApp[j]).iNiv = -1;  // tri sur la valeur agregee
            Parent.CalcAgregaEffectif(lstEffectifsApp, lstCriteresApp);

            pptF.x = 250; pptF.y = 195; pptF.width = 220; pptF.height = 100;
            iShape = (int)cw.InsertFormeFromId(iSlide.ToString(), true, "Top5 App Obsolecentes", (object)pptF);

            stStyle.bAxeXVisible = false; stStyle.bTitreVisible = false;
            stStyle.iIndexMainSerie = 0; stStyle.b3dActif = true;
            lstIndexSeries = new ArrayList(); lstIndexSeries.Add(-1); stStyle.lstIndexSeries = lstIndexSeries;
            stStyle.iNbrMaxEffectif = 5; stStyle.bStart = false; stStyle.bLegendVisible = false;

            cw.InsertGraphBarFromId(iSlide.ToString() + "," + iShape.ToString(), true, lstEffectifsApp, stStyle);


            //Graph sur la competence
            InitCriteres(lstCriteres);
            oCri = (Critere)lstCriteres[1]; oCri.Calc = true;
            oCri = (Critere)lstCriteres[2]; oCri.Calc = true;
            oCri = (Critere)lstCriteres[3]; oCri.Calc = true;
            for (int j = 0; j < lstEffectif.Count; j++) ((Effectif)lstEffectif[j]).iNiv = -1;  // tri sur la valeur agregee
            Parent.CalcAgregaEffectif(lstEffectif, lstCriteres);

            pptF.x = 485; pptF.y = 195; pptF.width = 220; pptF.height = 100;
            iShape = (int)cw.InsertFormeFromId(iSlide.ToString(), true, "Top5 Besoin Expertise", (object)pptF);

            stStyle.bAxeXVisible = false; stStyle.bTitreVisible = false;
            stStyle.iIndexMainSerie = 3; stStyle.b3dActif = true;
            lstIndexSeries = new ArrayList(); lstIndexSeries.Add(-1); stStyle.lstIndexSeries = lstIndexSeries;
            stStyle.iNbrMaxEffectif = 5; stStyle.bStart = false; stStyle.bLegendVisible = false;

            cw.InsertGraphBarFromId(iSlide.ToString() + "," + iShape.ToString(), true, lstEffectif, stStyle);

            //Graph sur le % de techno et instance
            int iTechno = 0, iConfinedStart = 0, iInstance = 0, iInstanceConfinedStart = 0;
            int iTechnoG = 0, iConfinedStartG = 0, iInstanceG = 0, iInstanceConfinedStartG = 0;

            if (Parent.oCnxBase.CBRecherche("Select Techno, ConfinedStart, Instance, InstanceConfinedStart From CadreRef Where GuidCadreRef='" + tnFamille.Name + "'"))
            {
                if (!Parent.oCnxBase.Reader.IsDBNull(0)) iTechno = Parent.oCnxBase.Reader.GetInt32(0);
                if (!Parent.oCnxBase.Reader.IsDBNull(1)) iConfinedStart = Parent.oCnxBase.Reader.GetInt32(1);
                if (!Parent.oCnxBase.Reader.IsDBNull(2)) iInstance = Parent.oCnxBase.Reader.GetInt32(2);
                if (!Parent.oCnxBase.Reader.IsDBNull(3)) iInstanceConfinedStart = Parent.oCnxBase.Reader.GetInt32(3);
            }
            Parent.oCnxBase.CBReaderClose();

            //if (Parent.oCnxBase.CBRecherche("Select Techno, ConfinedStart, Instance, InstanceConfinedStart From CadreRef Where GuidCadreRef='" + tnFamille.Name + "'"))
            if (Parent.oCnxBase.CBRecherche("Select Techno, ConfinedStart, Instance, InstanceConfinedStart From CadreRef Where GuidCadreRef='" + tnFamille.Parent.Name + "'"))
            {
                if (!Parent.oCnxBase.Reader.IsDBNull(0)) iTechnoG = Parent.oCnxBase.Reader.GetInt32(0);
                if (!Parent.oCnxBase.Reader.IsDBNull(1)) iConfinedStartG = Parent.oCnxBase.Reader.GetInt32(1);
                if (!Parent.oCnxBase.Reader.IsDBNull(2)) iInstanceG = Parent.oCnxBase.Reader.GetInt32(2);
                if (!Parent.oCnxBase.Reader.IsDBNull(3)) iInstanceConfinedStartG = Parent.oCnxBase.Reader.GetInt32(3);
            }
            Parent.oCnxBase.CBReaderClose();

            //Techno
            InitCriteres(lstCriteres);
            ArrayList lstEffPourcentage = new ArrayList();
            Effectif oEff = new Effectif(Parent, "0000", "Nbr Techno sauf domaine en cours", null, 0); lstEffPourcentage.Add(oEff); oEff.Val = iTechnoG - iTechno; // non visible
            oEff = new Effectif(Parent, "0000", "Nbr Techno domaine en cours sauf fin de support", null, 0x1B9D2A); lstEffPourcentage.Add(oEff); oEff.Val = iTechno - iConfinedStart; // Color.Green.ToArgb();
            oEff = new Effectif(Parent, "0000", "Nbr Techno fin de support", null, 0x2534FB); lstEffPourcentage.Add(oEff); oEff.Val = iConfinedStart; // Color.Red.ToArgb();

            pptF.x = 15; pptF.y = 310; pptF.width = 220; pptF.height = 100;
            iShape = (int)cw.InsertFormeFromId(iSlide.ToString(), true, "% Area/Techno Map", (object)pptF);

            stStyle.bAxeXVisible = false; stStyle.bTitreVisible = false;
            stStyle.iIndexMainSerie = -1; stStyle.b3dActif = false;
            lstIndexSeries = new ArrayList(); lstIndexSeries.Add(-1); stStyle.lstIndexSeries = lstIndexSeries;
            stStyle.iNbrMaxEffectif = 2; stStyle.bStart = false; stStyle.bLegendVisible = false;

            cw.InsertGraphPieFromId(iSlide.ToString() + "," + iShape.ToString(), true, lstEffPourcentage, stStyle);

            //Instance
            InitCriteres(lstCriteres);
            lstEffPourcentage = new ArrayList();
            oEff = new Effectif(Parent, "0000", "Nbr Techno sauf domaine en cours", null, 0); lstEffPourcentage.Add(oEff); oEff.Val = iInstanceG - iInstance; // non visible
            oEff = new Effectif(Parent, "0000", "Nbr Techno domaine en cours sauf fin de support", null, 0x1B9D2A); lstEffPourcentage.Add(oEff); oEff.Val = iInstance - iInstanceConfinedStart; ; // Color.Green.ToArgb();
            oEff = new Effectif(Parent, "0000", "Nbr Techno fin de support", null, 0x2534FB); lstEffPourcentage.Add(oEff); oEff.Val = iInstanceConfinedStart; ; // Color.Red.ToArgb();

            pptF.x = 250; pptF.y = 310; pptF.width = 220; pptF.height = 100;
            iShape = (int)cw.InsertFormeFromId(iSlide.ToString(), true, "% Vol Area/Techno Map", (object)pptF);

            stStyle.bAxeXVisible = false; stStyle.bTitreVisible = false;
            stStyle.iIndexMainSerie = -1; stStyle.b3dActif = false;
            lstIndexSeries = new ArrayList(); lstIndexSeries.Add(-1); stStyle.lstIndexSeries = lstIndexSeries;
            stStyle.iNbrMaxEffectif = 2; stStyle.bStart = false; stStyle.bLegendVisible = false;

            cw.InsertGraphPieFromId(iSlide.ToString() + "," + iShape.ToString(), true, lstEffPourcentage, stStyle);

            //Situation Groupe
            InitCriteres(lstCriteres);
            lstEffPourcentage = new ArrayList();
            oEff = new Effectif(Parent, "0000", "Non définit", null, 0x00CD00); lstEffPourcentage.Add(oEff);
            oEff = new Effectif(Parent, "0000", "Std Groupe", null, 0x00CC00); lstEffPourcentage.Add(oEff);
            oEff = new Effectif(Parent, "0000", "Std Métier", null, 0x99FF66); lstEffPourcentage.Add(oEff);
            oEff = new Effectif(Parent, "0000", "conflit Version", null, 0x99FFCC); lstEffPourcentage.Add(oEff);
            oEff = new Effectif(Parent, "0000", "conflit Product", null, 0x66CCFF); lstEffPourcentage.Add(oEff);
            oEff = new Effectif(Parent, "0000", "décomm", null, 0x5050FF); lstEffPourcentage.Add(oEff);
            for (int k = 0; k < lstEffectif.Count; k++)
            {
                Effectif o = (Effectif)lstEffectif[k];
                if (Parent.oCnxBase.CBRecherche("Select Norme From TechnoRef Where GuidTechnoRef='" + o.GuidEffectif + "'"))
                {
                    Effectif oEffk;
                    if (!Parent.oCnxBase.Reader.IsDBNull(0) && Parent.oCnxBase.Reader.GetInt32(0) > -1)
                    {
                        oEffk = (Effectif)lstEffPourcentage[Parent.oCnxBase.Reader.GetInt32(0)];
                        //         //  (0) - Non Définit
                        //NetSstG, //3 (1) - Standard Group
                        //NetSst,  //4 (2)- Standard LS    
                        //NetSstV, //5 (3)- Standard LS - Conflit Version
                        //NetSstP, //6 (4)- Standard LS - Conflit Product
                        //NetSnst, //7 (5)- Décommissionnement     
                    }
                    else
                        oEffk = (Effectif)lstEffPourcentage[4];
                    oEffk.Val++;
                }
                Parent.oCnxBase.CBReaderClose();
            }

            pptF.x = 485; pptF.y = 310; pptF.width = 220; pptF.height = 100;
            iShape = (int)cw.InsertFormeFromId(iSlide.ToString(), true, "Statut Techno", (object)pptF);

            stStyle.bAxeXVisible = false; stStyle.bTitreVisible = false;
            stStyle.iIndexMainSerie = -1; stStyle.b3dActif = false;
            lstIndexSeries = new ArrayList(); lstIndexSeries.Add(-1); stStyle.lstIndexSeries = lstIndexSeries;
            stStyle.iNbrMaxEffectif = 0; stStyle.bStart = false; stStyle.bLegendVisible = true;

            cw.InsertGraphPieFromId(iSlide.ToString() + "," + iShape.ToString(), true, lstEffPourcentage, stStyle);

            //Préconisation
            pptF.x = 15; pptF.y = 425; pptF.width = 690; pptF.height = 100;
            iShape = (int)cw.InsertFormeFromId(iSlide.ToString(), true, "Préconisation", (object)pptF);
        }

        private void CreatCatalogue(TreeNode tn)
        {
            if (tn.Parent != null && tn.Parent.Parent == null) EnteteFamille(tn);
            for (int i = 0; i < tn.Nodes.Count; i++) CreatCatalogue(tn.Nodes[i]);
            CreatProduct(tn);
        }

        /*
        private void CreatCatalogue(TreeNode tn, string sIdRoot, int Level)
        {
            for (int i = 0; i < tn.Nodes.Count; i++)
            {
                string sId = "Cat" + tn.Nodes[i].Name.Replace("-", "");

                if (Level == 1) EnteteFamille(sId, tn.Nodes[i]); //tn.Nodes[i].Parent.Parent

                CreatCatalogue(tn.Nodes[i], sId, Level + 1);
            }
            CreatProduct(tn);
        }
        */
        private void CreatReport(TreeNode tn, string sBookRoot, int Level)
        {
            int iBook = cw.Exist(sBookRoot);
            if (iBook > -1)
            {
                for (int i = 0; i < tn.Nodes.Count; i++)
                //int max = tn.Nodes.Count > 3 ? 3 : tn.Nodes.Count; 
                //for (int i = 0; i < max; i++)
                {
                    string sGuid = tn.Nodes[i].Name.Replace("-", "");

                    cw.InsertTextFromId(sBookRoot, false, "\n", null);
                    cw.CreatIdFromIdP("Cat" + sGuid, sBookRoot);
                    if (Level < 3)
                    {
                        cw.InsertTextFromId("Cat" + sGuid, true, tn.Nodes[i].Text + "\n", "Titre " + Level);
                        cw.CreatIdFromIdP("n" + sGuid, "Cat" + sGuid);
                    }
                    CreatReport(tn.Nodes[i], "Cat" + sGuid, Level + 1);
                }
                CreatProduct(tn, sBookRoot);
            }
            Parent.drawArea.GraphicsList.Clear();
        }

        private void CreatProduct(TreeNode tn)
        {
            int iSlide = 0;
            stpptforme pptF;
            ArrayList lstProduit = new ArrayList();
            if (Parent.oCnxBase.CBRecherche("SELECT GuidProduit, NomProduit, NomTechnoArea, editeur FROM Produit left join TechnoArea on Produit.GuidTechnoArea=TechnoArea.GuidTechnoArea WHERE Catalogue=1 AND GuidCadreRef='" + tn.Name + "'"))
            {
                while (Parent.oCnxBase.Reader.Read())
                {
                    ArrayList lstEnreg = new ArrayList();
                    lstEnreg.Add(Parent.oCnxBase.Reader.GetString(0)); 
                    lstEnreg.Add(Parent.oCnxBase.Reader.GetString(1));
                    if (Parent.oCnxBase.Reader.IsDBNull(2)) lstEnreg.Add(""); else lstEnreg.Add(Parent.oCnxBase.Reader.GetString(2));
                    if (Parent.oCnxBase.Reader.IsDBNull(3)) lstEnreg.Add(""); else lstEnreg.Add(Parent.oCnxBase.Reader.GetString(3));
                    lstProduit.Add(lstEnreg);
                }
            }
            Parent.oCnxBase.CBReaderClose();
           
            for (int i = 0; i < lstProduit.Count; i++)
            {
                ArrayList lstEnreg = (ArrayList)lstProduit[i];
                iSlide = (int)cw.CreatSlide("Cat" + ((string)lstEnreg[0]).Replace("-", ""), (string)lstEnreg[1]);
                InsertProperties(iSlide.ToString(), true, lstEnreg, tn);

                pptF.x = 15; pptF.y = 185; pptF.width = 690; pptF.height = 75;
                InsertBulle(iSlide.ToString(), true, (string)lstEnreg[0], "Usage", "Cas d'usage", (object)pptF);

                cw.InsertRoadmapFromId(iSlide.ToString(), true, (string)lstEnreg[0], null);

                pptF.x = 365; pptF.y = 275; pptF.width = 340; pptF.height = 110;
                InsertBulle(iSlide.ToString(), true, (string)lstEnreg[0], "Choix", "Arbre de décision", (object)pptF);

                pptF.x = 15; pptF.y = 400; pptF.width = 690; pptF.height = 75;
                InsertBulle(iSlide.ToString(), true, (string)lstEnreg[0], "Description", "Commentaire", (object)pptF);
            }
        }

        private void InsertProperties(string Id, bool init, ArrayList lstFielddProduct, TreeNode tn)
        {
            stpptStyle pptS;
            stpptStyleTab pptTabS;
            stpptforme pptF;
            pptF.x = 15; pptF.y = 80; pptF.width = 690; pptF.height = 90;

            int iShape = (int)cw.InsertFormeFromId(Id, true, "Caractéristiques", (object)pptF);

            ArrayList lstProperties = new ArrayList();
            if(tn.Parent!=null && tn.Parent.Text != "System Information")
            {
                if(tn.Parent.Parent!=null && tn.Parent.Text != "System Information")
                {
                    {
                        ArrayList lstEng = new ArrayList();
                        lstEng.Add("Famille");
                        lstEng.Add(tn.Parent.Parent.Text);
                        lstProperties.Add(lstEng);
                    }
                    {
                        ArrayList lstEng = new ArrayList();
                        lstEng.Add("Sous famille");
                        lstEng.Add(tn.Parent.Text);
                        lstProperties.Add(lstEng);
                    }
                    {
                        ArrayList lstEng = new ArrayList();
                        lstEng.Add("topic");
                        lstEng.Add(tn.Text);
                        lstProperties.Add(lstEng);
                    }
                } else {
                    {
                        ArrayList lstEng = new ArrayList();
                        lstEng.Add("famille");
                        lstEng.Add(tn.Parent.Text);
                        lstProperties.Add(lstEng);
                    }
                    {
                        ArrayList lstEng = new ArrayList();
                        lstEng.Add("Sous famille");
                        lstEng.Add(tn.Text);
                        lstProperties.Add(lstEng);
                    }
                }
            } else {
                {
                    ArrayList lstEng = new ArrayList();
                    lstEng.Add("famille");
                    lstEng.Add(tn.Text);
                    lstProperties.Add(lstEng);
                }
            }
            
            {
                ArrayList lstEng = new ArrayList();
                lstEng.Add("Techno Area");
                lstEng.Add(lstFielddProduct[2]);
                lstProperties.Add(lstEng);
            }
            {
                ArrayList lstEng = new ArrayList();
                lstEng.Add("Editeur");
                lstEng.Add(lstFielddProduct[3]);
                lstProperties.Add(lstEng);
            }
            {
                if (Parent.oCnxBase.CBRecherche("SELECT Min(Norme) FROM TechnoRef WHERE GuidProduit='" + lstFielddProduct[0] + "' Group By GuidProduit"))
                {
                    ArrayList lstEng = new ArrayList();
                    lstEng.Add("Statut");
                    lstEng.Add(Parent.sStatutTechno[Parent.oCnxBase.Reader.GetInt32(0)]);
                    lstProperties.Add(lstEng);
                }
                Parent.oCnxBase.CBReaderClose();
            }
            {
                ArrayList lstEng = new ArrayList();
                lstEng.Add("Support");
                lstEng.Add("");
                lstProperties.Add(lstEng);
            }
            {
                ArrayList lstEng = new ArrayList();
                lstEng.Add("Contact");
                lstEng.Add("");
                lstProperties.Add(lstEng);
            }
            pptS.size = 6; pptS.bold = Microsoft.Office.Core.MsoTriState.msoFalse; pptS.Couleur = System.Drawing.Color.DarkSlateGray.ToArgb();
            pptTabS.tabHeader = false;
            cw.InsertTabFromId(Id + "," + iShape.ToString(), true, lstProperties, (object)pptS, (object) pptTabS);
        }

        private void InsertArbre(string Id, bool init, string sGuidProduct)
        {
            //stpptforme pptF;

            //int iShape = (int)cw.InsertFormeFromId(Id, true, "Arbre de décision", (object)pptF);
        }


        private void InsertBulle(string Id, bool init, string sGuidProduct, string sGuidProp, string sNomProp, object ostyle)
        {
            stpptStyle pptS;

            pptS.size = 9; pptS.bold = Microsoft.Office.Core.MsoTriState.msoFalse; pptS.Couleur = System.Drawing.Color.DarkSlateGray.ToArgb();
            int iShape = (int)cw.InsertFormeFromId(Id, true, sNomProp, ostyle);

            if (Parent.oCnxBase.CBRecherche("SELECT Size, RichText FROM Produit left join Comment on GuidProduit=Comment.GuidObject Where GuidProduit='" + sGuidProduct + "' And NomProp='" + sGuidProp + "'"))
            {
                while (Parent.oCnxBase.Reader.Read())
                {
                    if (!Parent.oCnxBase.Reader.IsDBNull(0))
                    {
                        int nByte = Parent.oCnxBase.Reader.GetInt32(0);
                        if (nByte > 0)
                        {
                            byte[] rawData = new byte[nByte];
                            Parent.oCnxBase.Reader.GetBytes(1, 0, rawData, 0, nByte);
                            System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
                            rtBox.Rtf = System.Text.Encoding.UTF8.GetString(rawData);

                            cw.InsertTextFromId(Id + ',' + iShape.ToString(), true, rtBox.Text, pptS);
                        }
                    }
                }
            }
            Parent.oCnxBase.CBReaderClose();

        }
        private void InsertObservation(string Id, bool init, string sGuidProduct)
        {
            //stpptforme pptF;

            //int iShape = (int)cw.InsertFormeFromId(Id, true, "Commentaire", (object)pptF);
        }

        private void CreatProduct(TreeNode tn, string sBookRoot)
        {
            ArrayList lstProduit = new ArrayList();
            if (Parent.oCnxBase.CBRecherche("SELECT GuidProduit FROM Produit WHERE Catalogue=1 AND GuidCadreRef='" + tn.Name + "'"))
            {
                while (Parent.oCnxBase.Reader.Read()) lstProduit.Add(Parent.oCnxBase.Reader.GetString(0));
                /*{

                    string sGuid = Parent.oCnxBase.Reader.GetString(0).Replace("-", "");

                    cw.InsertTexBookmark(sBookRoot, false, "\n", null);
                    cw.CreatBookmark("Pro" + sGuid, sBookRoot, 1);
                    cw.InsertTexBookmark("Pro" + sGuid, true, Parent.oCnxBase.Reader.GetString(1) + "\n", "Titre 6");
                    cw.CreatBookmark("n" + sGuid, "Pro" + sGuid, 1);
                }*/
            }
            Parent.oCnxBase.CBReaderClose();
            for (int i = 0; i < lstProduit.Count; i++)
            {
                Parent.drawArea.GraphicsList.Clear();
               
                DrawRoadmap dr = new DrawRoadmap(Parent, (string)lstProduit[i], switchTechFonc);
                Parent.drawArea.GraphicsList.UnselectAll();
                Parent.drawArea.GraphicsList.Add(dr);
                //Parent.drawArea.Refresh();

                //Application 'Temp' (00613319-a3c1-420e-b12b-ebcc3e7b9b9e) est utilisee pour sauvegarde la roadmap
                WorkApplication wkTmp = new WorkApplication(Parent, "00613319-a3c1-420e-b12b-ebcc3e7b9b9e", "Temp", "00613319-a3c1-420e-b12b-ebcc3e7b9b9e");
                string sDiagram = Parent.SaveDiagram("Roadmap", wkTmp, "");

                string sGuid = ((string)lstProduit[i]).Replace("-", "");
                string sFileUseCase = Parent.sPathRoot + @"\produit\" + sGuid + ".txt";
                string sUseCase = "";

                Parent.drawArea.tools[(int) DrawArea.DrawToolType.Produit].LoadSimpleObject((string)lstProduit[i]);
                DrawProduit dp = (DrawProduit)Parent.drawArea.GraphicsList[0];
                if(tn.Parent != null) dp.SetValueFromName("Subdomain", tn.Text);
                if(tn.Parent.Parent != null) dp.SetValueFromName("Domain", tn.Parent.Text);
                if(tn.Parent.Parent.Parent != null) dp.SetValueFromName("Family", tn.Parent.Parent.Text);
                if (Parent.oCnxBase.CBRecherche("SELECT MIN(NormeG), MIN(Norme) FROM TechnoRef WHERE GuidProduit='" + (string)lstProduit[i] + "' GROUP BY GuidProduit"))
                {
                    dp.SetValueFromName("NormeG", Parent.oCnxBase.Reader.GetInt32(0));
                    dp.SetValueFromName("Norme", Parent.oCnxBase.Reader.GetInt32(1));
                }
                Parent.oCnxBase.CBReaderClose();
                if (File.Exists(sFileUseCase))
                {
                    using (StreamReader sr = new StreamReader(sFileUseCase))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null) sUseCase += line + '\n';
                    }
                    dp.SetValueFromName("UseCase", sUseCase);
                }
                if (sDiagram != null) dp.SetValueFromName("Roadmap", sDiagram);

                cw.InsertTextFromId(sBookRoot, false, "\n", null);
                cw.CreatIdFromIdP("Pro" + sGuid, sBookRoot);
                cw.InsertTabFromId("Pro" + sGuid, true, dp, null, true, "Editeur");
                dp.Selected = true;
                Parent.drawArea.GraphicsList.DeleteSelection();
            }
            
        }

        private void bCatalogue_Click(object sender, EventArgs e)
        {
            int TypeCat = 3;
            TreeNode tn = tvCadreRef.Nodes[0];

            if (tvCadreRef.SelectedNode != null) tn = tvCadreRef.SelectedNode;
            switch (TypeCat) {
                case 1:
                    {
                        ControlWord ocw;
                        ocw = new ControlWord(Parent, Parent.sPathRoot + @"\catalogue_modele-V0R2-Eng.dotx", true);
                        cw = (ControlDoc)ocw;
                        if (cw != null)
                        {
                            CreatReport(tn, "Catalogue", 1);
                        }
                    }
                    break;
                case 2:
                    {
                        ControlHtml och = null;
                        och = new ControlHtml(Parent, Parent.sPathRoot + @"\dat.html");
                        cw = (ControlDoc)och;
                    }
                    break;
                case 3:
                    {
                        ControlPPT oppt = null;
                        oppt = new ControlPPT(Parent, Parent.sPathRoot + @"\revue_modele-V3.R2-Eng.potx", Microsoft.Office.Core.MsoTriState.msoTrue);
                        cw = (ControlDoc)oppt;
                        if (cw != null)
                        {
                            CreatCatalogue(tn);
                        }
                    }
                    break;
            }

            
        }

        

        private void bVersion_Click(object sender, EventArgs e)
        {
            ArrayList lstTechno = new ArrayList();
            if (parent.oCnxBase.CBRecherche("SELECT GuidProduit, GuidTechnoRef, ValIndicator FROM TechnoRef, IndicatorLink WHERE GuidTechnoRef=GuidObjet AND GuidIndicator='b00b12bd-a447-47e6-92f6-e3b76ad22830' ORDER BY GuidProduit, ValIndicator"))
            {
                int y = DateTime.Now.Year;
                double dnow = DateTime.Now.ToOADate();
                while (parent.oCnxBase.Reader.Read())
                {
                    double d = parent.oCnxBase.Reader.GetDouble(2);
                    if (d < dnow) lstTechno.Add(parent.oCnxBase.Reader.GetString(0) + ";" + parent.oCnxBase.Reader.GetString(1) + ";1;0;0");
                    else
                    {
                        if(lstTechno.Count>0 && parent.oCnxBase.Reader.GetString(0).IndexOf((string)lstTechno[lstTechno.Count-1])>-1)
                            lstTechno.Add(parent.oCnxBase.Reader.GetString(0) + ";" + parent.oCnxBase.Reader.GetString(1) + ";0;0;1");
                        else lstTechno.Add(parent.oCnxBase.Reader.GetString(0) + ";" + parent.oCnxBase.Reader.GetString(1) + ";0;1;0");
                    }
                }
            }
            parent.oCnxBase.CBReaderClose();
            for (int i=0; i < lstTechno.Count; i++)
            {
                string[] aTechno = ((string)lstTechno[i]).Split(';');
                parent.oCnxBase.CBWrite("Update TechnoRef Set VNMoins1=" + aTechno[2] + ", VN=" + aTechno[3] + ", VNplus1=" + aTechno[4] + " WHERE GuidTechnoRef='" + aTechno[1] + "'");
            }
        }

        struct stIndicatoreCadreRef
        {
            public int iTechno;    // nombre de techno pour un cadreRef
            public int iInstance;  // nombre d'instance pour un cadreRef
            public int iConfinedStart; // nombre de techno qui sont en debut de confinement dans l'annee 
            public int iInstanceConfinedStart; // nombre d'instance pour un cardreRef qui sont en debut de confinement dans l'annee 
        }
        private stIndicatoreCadreRef CalcIndicator(TreeNode tn)
        {
            stIndicatoreCadreRef oIndicator;
            oIndicator.iTechno = 0;
            oIndicator.iInstance = 0;
            oIndicator.iConfinedStart = 0;
            oIndicator.iInstanceConfinedStart = 0;
            if (tn.Nodes.Count == 0)
            {
                ArrayList lstCriteres = new ArrayList();
                lstCriteres.Add(new Critere(Parent, "4b8e1794-2681-4b7a-b4d5-4ad4637860cb", "A-Instance"));

                ArrayList lstEffectif = Parent.report(tn, tn.Name, lstCriteres, Form1.rbTypeRecherche.Techno);
                for (int i = 0; i < lstEffectif.Count; i++)
                {
                    Effectif oEff = (Effectif)lstEffectif[i];
                    Niveau oNiv = (Niveau)oEff.lstNivEffectif[0];
                    oIndicator.iInstance += (int)oNiv.Val;
                    if (Parent.oCnxBase.CBRecherche("Select ConfinedStart From TechnoRef Where GuidTechnoRef='" + oEff.GuidEffectif + "'"))
                    {
                        if (!Parent.oCnxBase.Reader.IsDBNull(0))
                        {
                            DateTime dt = Parent.oCnxBase.Reader.GetDate(0);
                            if (dt.ToOADate() - DateTime.Now.ToOADate() - 360 < 0)
                            {
                                oIndicator.iConfinedStart++;
                                oIndicator.iInstanceConfinedStart += (int)oNiv.Val;
                            }
                        }
                    }
                    Parent.oCnxBase.CBReaderClose();
                }
                oIndicator.iTechno = lstEffectif.Count;
            }
            else {
                for (int i = 0; i < tn.Nodes.Count; i++)
                {
                    stIndicatoreCadreRef oIndicatorI;

                    oIndicatorI = CalcIndicator(tn.Nodes[i]);
                    oIndicator.iTechno += oIndicatorI.iTechno;
                    oIndicator.iInstance += oIndicatorI.iInstance;
                    oIndicator.iConfinedStart += oIndicatorI.iConfinedStart;
                    oIndicator.iInstanceConfinedStart += oIndicatorI.iInstanceConfinedStart;
                }
            }
            // Modif des indicator CadreRef dans la base
            Parent.oCnxBase.CBWrite("Update CadreRef Set Techno=" + oIndicator.iTechno + ", Instance=" + oIndicator.iInstance + ", ConfinedStart=" + oIndicator.iConfinedStart + ", InstanceConfinedStart=" + oIndicator.iInstanceConfinedStart + " Where GuidCadreRef='" + tn.Name + "'");

            return oIndicator;
        }

        private void CalcIndicateurInstance()
        {
            // delete indicator 4b8e1794-2681-4b7a-b4d5-4ad4637860cb (instance)
            Parent.oCnxBase.CBWrite("Delete from IndicatorLink Where GuidIndicator='4b8e1794-2681-4b7a-b4d5-4ad4637860cb'");

            //insere indicator 4b8e1794-2681-4b7a-b4d5-4ad4637860cb (istance)
            ArrayList lstGuidServerPhy = new ArrayList();
            if (Parent.oCnxBase.CBRecherche("SELECT GuidServerPhy FROM ServerPhy"))
                while (Parent.oCnxBase.Reader.Read()) lstGuidServerPhy.Add(Parent.oCnxBase.Reader.GetString(0));
            Parent.oCnxBase.CBReaderClose();
            for (int i = 0; i < lstGuidServerPhy.Count; i++)
                Parent.oCnxBase.CBWrite("Insert Into IndicatorLink (GuidObjet, GuidIndicator, ValIndicator) Values ('" + lstGuidServerPhy[i] + "', '4b8e1794-2681-4b7a-b4d5-4ad4637860cb', 1)");
        }

        private void CalcIndicateurObsolescence()
        {
            string[] aGuidIndicatorObsolescence = { "5f051eef-3016-4c68-a4f5-926e0ab6eb68", "1e5421a8-457f-429a-99bd-d76e3cfa055a", "82ddd17e-baff-47f3-95fd-89b7ca3588f1", "13b8ede4-156d-49de-84e6-9d2e6b8ce3e0", "e9b6a159-4b26-476c-99de-aa1878dc79ee" };
            // suppression de tous les indicateurs obsolescence
            for (int i=0; i<aGuidIndicatorObsolescence.Length; i++)
                Parent.oCnxBase.CBWrite("delete  from indicatorlink where GuidIndicator='" + aGuidIndicatorObsolescence[i] + "'");

            // creation list des indicateurs avec leurperiode [ConfinedStart, ConfinedEnd]
            XmlElement root = Parent.oCnxBase.xmlGetIndicatorObsolescence();

            // Calcul de l'indicateur pour chaque objet de la liste
            IEnumerator ienum = root.GetEnumerator();
            XmlNode Node;
            while (ienum.MoveNext())
            {
                Node = (XmlNode)ienum.Current;
                switch (Node.NodeType)
                {
                    case XmlNodeType.Element:
                        if (Node.Name == "row")
                        {
                            double dPeriodeStart = Convert.ToDouble(((XmlElement)Node).GetAttribute("PeriodeStart")), dPeriodeEnd = Convert.ToDouble(((XmlElement)Node).GetAttribute("PeriodeEnd"));

                            for (int i = 0; i < aGuidIndicatorObsolescence.Length; i++)
                            {
                                double dVal = 0;
                                double dnow = DateTime.Now.ToOADate() + i * 365;

                                if (dPeriodeStart < dPeriodeEnd)
                                {
                                    double delta = dPeriodeEnd - dPeriodeStart;
                                    if (dnow < dPeriodeStart + delta * 0.5) dVal = 5;
                                    else if (dnow < dPeriodeStart + delta * 0.75) dVal = 4;
                                    else if (dnow < dPeriodeStart + delta) dVal = 3;
                                    else if (dnow < dPeriodeStart + delta * 1.25) dVal = 2;
                                    else if (dnow < dPeriodeStart + delta * 1.5) dVal = 1;
                                }
                                Parent.oCnxBase.CBWrite("insert into indicatorlink (GuidObjet, GuidIndicator, ValIndicator) Values ('" + ((XmlElement)Node).GetAttribute("GuidObjet") + "', '" + aGuidIndicatorObsolescence[i] + "', " + dVal + ")");
                            }
                        }
                        break;
                }
            }
            Parent.docXml.RemoveAll();
        }

        private void bCalcIndicator_Click(object sender, EventArgs e)
        {
            CalcIndicateurInstance();
            CalcIndicateurObsolescence();
            CalcIndicator(tvCadreRef.Nodes[0]); // agregation de certains indicateurs sur les familles de produit
        }
    }


    public class Produit : Enreg
    {
        public Produit(Form1 f, DataGridView d, string t)
        {
            F = f;
            dg = d;
            LstValue = new ArrayList();
            sTable = t;
            InitProp();
        }

        public Produit(Form1 f, string t)
        {
            F = f;
            dg = null;
            LstValue = new ArrayList();
            sTable = t;
            InitProp();
        }
    }

    public class TechnoRef : Enreg
    {
        public TechnoRef(Form1 f, DataGridView d, string t)
        {
            F = f;
            dg = d;
            sTable = t;
            LstValue = new ArrayList();
            InitProp();
        }
    }
}
