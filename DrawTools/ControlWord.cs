using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Office.Core;
using MOI = Microsoft.Office.Interop;
//using System.Windows.Forms;
using System.Collections;
using System.Data.Odbc;
using System.Drawing;
using HtmlAgilityPack;
using System.IO;

namespace DrawTools
{

    struct STStyleGraph
    {
        public ArrayList lstIndexSeries;
        public int iIndexMainSerie;
        public int iNbrMaxEffectif;
        public bool bStart; //prendre en compte les effectifs au debut si iNbrMax < nbrEffect (true) ou à la fin (false)
        public bool bTitreVisible;
        public bool b3dActif;
        public bool bAxeXVisible;
        public bool bLegendVisible;
    }
    public class CWObject
    {
        private MOI.Word.Range rngZone;

        public MOI.Word.Range rng
        {
            get
            {
                return rngZone;
            }
            set
            {
                rngZone = value;
            }
        }
        public CWObject()
        {
        }
    }

    
    public class CWMerge : CWObject
    {
        public int xSrc;
        public int ySrc;
        public int xCbl;
        public int yCbl;

        public CWMerge(int x1, int y1, int x2, int y2)
        {
            xSrc = x1;
            ySrc = y1;
            xCbl = x2;
            yCbl = y2;
        }
    }
    
    
    public class CWTable : CWObject
    {
        private ArrayList lstMerges;
        private Microsoft.Office.Interop.Word.Table tb;

        public ArrayList LstMerges
        {
            get
            {
                return lstMerges;
            }
            set
            {
                lstMerges = value;
            }
        }

        public Microsoft.Office.Interop.Word.Table Tb
        {
            get
            {
                return tb;
            }
            set
            {
                tb = value;
            }
        }

        public CWTable(Microsoft.Office.Interop.Word.Table TB)
        {
            lstMerges = new ArrayList();
            Tb = TB;
        }

        public void Merge()
        {
            for (int i = 0; i < LstMerges.Count; i++)
            {
                MergeCell(i);
            }
        }

        public void MergeCell(int iMerge)
        {
            CWMerge cwMerge = (CWMerge)LstMerges[iMerge];

            Tb.Cell(cwMerge.xSrc, cwMerge.ySrc).Merge(Tb.Cell(cwMerge.xCbl, cwMerge.yCbl));
        }
    }

    public abstract class ControlDoc
    {
        protected Form1 F;
        protected string sDocPath;

        public virtual int Exist(string sSearch)
        {
            return -1;
        }

        public virtual void InsertHeadTabFromId(string Id, bool init, Table t, object ostyle)
        {
        }

        public virtual void InsertTextFromId(string Id, bool init, string texte, object ostyle)
        {
        }

        public virtual string Format(string rtf)
        {
            return rtf;
        }

        public virtual object InsertGraphBarFromId(string Id, bool init, ArrayList lstEffectif, object oStyle)
        {
            return null;
        }

        public virtual object InsertGraphPieFromId(string Id, bool init, ArrayList lstEffectif, object oStyle)
        {
            return null;
        }


        public virtual object InsertRoadmapFromId(string Id, bool init, string sGuidProduct, object oStyle)
        {
            return null;
        }

        public virtual void CreatIdFromIdP(string Id, string IdP)
        {
        }

        public virtual void InsertImgFromId(string Id, bool init, string pathing, object oStyle)
        {
        }

        public virtual object CreatSlide(string item, string titre)
        {
            return null;
        }

        public virtual object InsertFormeFromId(string Id, bool init, string texte, object oStyle)
        {
            return null;
        }

        public virtual void InitTabFromId(string Id)
        {
        }

        public virtual void InsertTabFromId(string Id, bool init, DrawObject o, object ostyle, bool bIndex, string sEntry, bool bComment=true)
        {
        }

        public virtual void InsertTabFromId(string Id, bool init, ArrayList lstTab, object ostyle, object ostyleTab)
        {
        }

        public virtual void InsertTabFromId(string Id, bool init, string sGuidObj, XmlDB xmlDB, object ostyle, string sType = "")
        {
        }

        public virtual object InsertRowFromId(string Id, DrawObject o)
        {
            return null;
        }

        public virtual void InsertRowFromReaderId(string Id, OdbcDataReader r, string sType)
        {
        }

        public virtual void InsertRichTextFromId(string Id, bool init, byte[] bRtf)
        {
        }

        public virtual void SaveDoc()
        {
        }

        public virtual void setDocPath(string sPath)
        {
            if (!Directory.Exists(sPath)) Directory.CreateDirectory(sPath);
            sDocPath = sPath;
        }

        public virtual string getImagePath()
        {
            return sDocPath + "\\images";
        }

        
    }

    struct stpptStyleTab
    {
        public bool tabHeader;
    }

    struct stpptStyle
    {
        public float size;
        public MsoTriState bold;
        public int Couleur;
    }

    struct stpptforme
    {
        public int x;
        public int y;
        public int width;
        public int height;
    }

    public class ControlPPT : DrawTools.ControlDoc
    {
        private XmlExcel xmlExcel;
        private static object missing = System.Type.Missing;
        private MOI.PowerPoint.Application appppt;
        private MOI.PowerPoint.Presentations PresSet;
        private MOI.PowerPoint._Presentation Pres;
        private MOI.PowerPoint.Slides Slides;
        //private Form1 F;


        public MOI.PowerPoint.Application AppPpt
        {
            get { return appppt; }
        }

        public ControlPPT(Form1 form, string sTemplate, MsoTriState Visible)
        {
            appppt = new MOI.PowerPoint.Application();

            AppPpt.Visible = Visible;
            PresSet = AppPpt.Presentations;
            Pres = PresSet.Open(sTemplate, MsoTriState.msoFalse, MsoTriState.msoTrue, MsoTriState.msoTrue);
            Slides = Pres.Slides;
            F = form;

            xmlExcel = new XmlExcel(F, "Rows");

        }

        public override int Exist(string sSearch)
        {
            for (int i = 1; i <= Slides.Count; i++)
            {
                if (Slides[i].Tags["Item"] == sSearch) return i;
            }
            return -1;
        }

        public override void InsertImgFromId(string Id, bool init, string pathing, object oStyle)
        {
            MOI.PowerPoint.Slide dia = Slides[Convert.ToInt32(Id)];
            stpptforme pptF = (stpptforme) oStyle;
            MOI.PowerPoint.Shape sh;
            Bitmap Img = (Bitmap)Image.FromFile(pathing, true);
            if (pptF.height * Img.Width / Img.Height <= pptF.width)
                sh = dia.Shapes.AddPicture(pathing, MsoTriState.msoFalse, MsoTriState.msoTrue, pptF.x, pptF.y, pptF.height * Img.Width / Img.Height, pptF.height);
            else
                sh = dia.Shapes.AddPicture(pathing, MsoTriState.msoFalse, MsoTriState.msoTrue, pptF.x, pptF.y, pptF.width, pptF.width * Img.Height / Img.Width);
        }

        public override object InsertGraphPieFromId(string Id, bool init, ArrayList lstEffectif, object oStyle)
        {
            string[] aIds = Id.Split(',');

            int iSlide = Convert.ToInt32(aIds[0]);
            int iShape = Convert.ToInt32(aIds[1]);

            MOI.PowerPoint.Slide dia = Slides[iSlide];
            MOI.PowerPoint.Shape shape;
            STStyleGraph stStyleGraph = (STStyleGraph)oStyle;
            //Insert les valeurs
            ArrayList lstIndexSeries = (ArrayList)stStyleGraph.lstIndexSeries;


            shape = dia.Shapes[iShape];

            int idx = dia.Shapes.Count + 1;
            if (stStyleGraph.iNbrMaxEffectif == 0)
            {
                dia.Shapes.AddChart(XlChartType.xlPie, shape.Left + 5, shape.Top + 5, shape.Width - 5, shape.Height - 5);
            }
            else
            {
                dia.Shapes.AddChart(XlChartType.xlPieOfPie, shape.Left + 5, shape.Top + 5, shape.Width - 5, shape.Height - 5);
            }
            MOI.PowerPoint.Chart chart = dia.Shapes[idx].Chart;

            MOI.PowerPoint.ChartData chartData = chart.ChartData;

            MOI.Excel._Workbook oWB = (MOI.Excel._Workbook)chartData.Workbook;
            //MOI.Excel._Workbook oWB = (MOI.Excel._Workbook) chartData.Workbook.Activate;
            MOI.Excel._Worksheet oSheet = (MOI.Excel._Worksheet)oWB.ActiveSheet;
            MOI.Excel.ListObject oTab = oSheet.ListObjects.Item[1];

            //Insert les valeurs
            for (int i = 0; i < lstEffectif.Count; i++)
            {
                Effectif oEffectif = (Effectif)lstEffectif[i];

                ((MOI.Excel.Range)oSheet.Cells[i + 2, 1]).Value2 = oEffectif.NomEffectif;
                ((MOI.Excel.Range)oSheet.Cells[i + 2, 2]).Value2 = oEffectif.Val;
            }
            MOI.Excel.Range rng = (MOI.Excel.Range)oSheet.Range[oSheet.Cells[1, 1], oSheet.Cells[lstEffectif.Count + 1, 2]];
            oTab.Resize(rng);


            MOI.PowerPoint.Series series = (MOI.PowerPoint.Series)chart.SeriesCollection(1); // init la serie

            MOI.PowerPoint.ChartGroup chartGroup = (MOI.PowerPoint.ChartGroup)chart.ChartGroups(1);
            chartGroup.SplitType = MOI.PowerPoint.XlChartSplitType.xlSplitByPosition;
            chartGroup.SplitValue = stStyleGraph.iNbrMaxEffectif; // Nbr de sous-valeurs dans le second secteur
            series.Explosion = stStyleGraph.iNbrMaxEffectif;
            chartGroup.HasSeriesLines = true;
            chartGroup.SeriesLines.Format.Line.Visible = MsoTriState.msoTrue;
            chartGroup.SeriesLines.Format.Line.ForeColor.RGB = Color.LightGray.ToArgb();
            chartGroup.GapWidth = 20;
            chartGroup.SecondPlotSize = 75;

            for (int i = 0; i < lstEffectif.Count; i++)
            {
                Effectif oEffectif = (Effectif)lstEffectif[i];

                MOI.PowerPoint.Point pt = (MOI.PowerPoint.Point)series.Points(i + 1);
                pt.Border.Color = Color.LightGray.ToArgb();

                if (oEffectif.iCouleur == 0) pt.Fill.Visible = MsoTriState.msoFalse;
                else
                {
                    pt.Interior.Color = oEffectif.iCouleur;
                    if (oEffectif.Val != 0)
                    {
                        pt.HasDataLabel = true;
                        pt.DataLabel.ShowValue = false;
                        pt.DataLabel.ShowPercentage = true;
                        pt.DataLabel.Font.Color = System.Drawing.Color.Black.ToArgb();
                        pt.DataLabel.Font.Size = 8;
                        pt.DataLabel.Position = MOI.PowerPoint.XlDataLabelPosition.xlLabelPositionCenter;
                    }
                }
            }
            if (stStyleGraph.iNbrMaxEffectif != 0)
            {
                MOI.PowerPoint.Point pt = (MOI.PowerPoint.Point)series.Points(lstEffectif.Count + 1);
                pt.Interior.Color = Color.LightGray.ToArgb();
                pt.HasDataLabel = true;
                pt.DataLabel.ShowValue = false;
                pt.DataLabel.ShowPercentage = true;
                pt.DataLabel.Font.Color = System.Drawing.Color.Black.ToArgb();
                pt.DataLabel.Font.Size = 8;
                pt.DataLabel.Position = MOI.PowerPoint.XlDataLabelPosition.xlLabelPositionCenter;
            }

            if (!stStyleGraph.bTitreVisible) chart.ChartTitle.Delete();
            if (!stStyleGraph.bLegendVisible) chart.HasLegend = false;
            else
            {
                chart.Legend.Font.Size = 8;
                chart.Legend.Font.Color = System.Drawing.Color.Black.ToArgb();
                chart.Legend.Height += 5;
            }
            if (!stStyleGraph.bAxeXVisible) chart.HasAxis[MOI.PowerPoint.XlAxisType.xlValue] = false;
            oWB.Close();

            return dia.Shapes.Count;
        }

        // voir STStyleGraph --> oStyle
        public override object InsertGraphBarFromId(string Id, bool init, ArrayList lstEffectif, object oStyle)
        {
            string[] aIds = Id.Split(',');
            
            int iSlide = Convert.ToInt32(aIds[0]);
            int iShape = Convert.ToInt32(aIds[1]);
            MOI.PowerPoint.Slide dia = Slides[iSlide];
            MOI.PowerPoint.Shape shape;
            STStyleGraph stStyleGraph = (STStyleGraph) oStyle;

            shape = dia.Shapes[iShape];

            int idx = dia.Shapes.Count + 1;
            dia.Shapes.AddChart(XlChartType.xlBarClustered, shape.Left + 5, shape.Top + 5, shape.Width - 5, shape.Height - 5);
            MOI.PowerPoint.Chart chart = dia.Shapes[idx].Chart;

            MOI.PowerPoint.ChartData chartData = chart.ChartData;
            MOI.Excel._Workbook oWB = (MOI.Excel._Workbook)chartData.Workbook;
            MOI.Excel._Worksheet oSheet = (MOI.Excel._Worksheet)oWB.ActiveSheet;
            MOI.Excel.ListObject oTab = oSheet.ListObjects.Item[1];

            //Insert les valeurs
            ArrayList lstIndexSeries = (ArrayList)stStyleGraph.lstIndexSeries;
            int iDebTechno = 0; int iFinTechno = lstEffectif.Count;
            if(stStyleGraph.iNbrMaxEffectif>0)
            {
                if(stStyleGraph.bStart)
                {
                    if (lstEffectif.Count > stStyleGraph.iNbrMaxEffectif) iFinTechno = stStyleGraph.iNbrMaxEffectif;
                }
                else
                {
                    if (lstEffectif.Count > stStyleGraph.iNbrMaxEffectif) iDebTechno = lstEffectif.Count - stStyleGraph.iNbrMaxEffectif;
                }
            }
            for (int i = iDebTechno, j = 1; i < iFinTechno; i++, j++)
            {
                Effectif oEffectif = (Effectif)lstEffectif[i];
                
                MOI.Excel.Range rng = (MOI.Excel.Range)oSheet.Range[oSheet.Cells[1, 1], oSheet.Cells[j + 1, lstIndexSeries.Count + 1 ]];
                oTab.Resize(rng);

                ((MOI.Excel.Range)oSheet.Cells[j + 1, 1]).Value2 = oEffectif.NomEffectif;
                Niveau oNiv = null;
                for (int k = 0; k < lstIndexSeries.Count; k++)
                {
                    MOI.PowerPoint.Series series = (MOI.PowerPoint.Series)chart.SeriesCollection(k + 1); // init la serie
                    if (stStyleGraph.b3dActif) // effet 3D
                    {
                        series.Format.ThreeD.BevelTopType = MsoBevelType.msoBevelCircle;
                        series.Format.ThreeD.BevelTopDepth = 3; series.Format.ThreeD.BevelTopInset = 3;
                    }

                    int iIndex = (int)lstIndexSeries[k];
                    if (iIndex == -1)
                    {
                        oNiv = (Niveau)oEffectif.lstNivEffectif[stStyleGraph.iIndexMainSerie];
                        ((MOI.Excel.Range)oSheet.Cells[j + 1, k + 2]).Value2 = oEffectif.Val;
                    }
                    else
                    {
                        oNiv = (Niveau)oEffectif.lstNivEffectif[(int)lstIndexSeries[k]];
                        ((MOI.Excel.Range)oSheet.Cells[j + 1, k + 2]).Value2 = oNiv.Val;
                    }

                    // Couleur de la serie
                    MOI.PowerPoint.Point pt = (MOI.PowerPoint.Point)series.Points(j);
                    pt.Interior.Color = oNiv.GetColor();
                }
                

                // si graph --> bulle: Le code suivant montre comment insérer un texte sur la bulle
                /* pt.HasDataLabel = true;
                pt.DataLabel.Font.Color = System.Drawing.Color.Black.ToArgb();
                pt.DataLabel.Text = oEffectif.NomEffectif;*/
            }
            if (!stStyleGraph.bTitreVisible) chart.ChartTitle.Delete();
            if (!stStyleGraph.bLegendVisible) chart.HasLegend = false;
            if(!stStyleGraph.bAxeXVisible) chart.HasAxis[MOI.PowerPoint.XlAxisType.xlValue] = false;

            MOI.PowerPoint.Axis yaxis = (MOI.PowerPoint.Axis)chart.Axes(MOI.PowerPoint.XlAxisType.xlCategory, MOI.PowerPoint.XlAxisGroup.xlPrimary);
            yaxis.MajorGridlines.Format.Line.ForeColor.RGB = 0;
            yaxis.Format.Line.ForeColor.RGB = 0;
            yaxis.TickLabels.Font.Size = 8;
            yaxis.TickLabels.Font.Color = System.Drawing.Color.Black.ToArgb();

            oWB.Close();
            return dia.Shapes.Count;
        }


        public override object InsertRoadmapFromId(string Id, bool init, string sGuidProduct, object oStyle)
        {
            int iSlide = Convert.ToInt32(Id);
            MOI.PowerPoint.Slide dia = Slides[iSlide];
            MOI.PowerPoint.Shape shape;
            stpptforme pptF;
            pptF.x = 15; pptF.y = 275; pptF.width = 340; pptF.height = 110;

            int iShape = (int)InsertFormeFromId(iSlide.ToString(), true, "Roadmap", (object)pptF);
            shape = dia.Shapes[iShape];
            if (F.oCnxBase.CBRecherche("SELECT GuidTechnoRef, NomTechnoRef, UpComingStart, UpComingEnd, ReferenceStart, ReferenceEnd, ConfinedStart, ConfinedEnd, DecommStart, DecommEnd, ValIndicator FROM TechnoRef, IndicatorLink WHERE GuidProduit='" + sGuidProduct + "' AND GuidTechnoRef=GuidObjet AND GuidIndicator='b00b12bd-a447-47e6-92f6-e3b76ad22830' ORDER BY ValIndicator"))
            {

                int idx = dia.Shapes.Count + 1, row = 0;
                dia.Shapes.AddChart(XlChartType.xlBarStacked, shape.Left + 5, shape.Top + 5, shape.Width - 5, shape.Height - 5);
                MOI.PowerPoint.Chart chart = dia.Shapes[idx].Chart;

                MOI.PowerPoint.ChartData chartData = chart.ChartData;
                MOI.Excel._Workbook oWB = (MOI.Excel._Workbook)chartData.Workbook;
                MOI.Excel._Worksheet oSheet = (MOI.Excel._Worksheet)oWB.ActiveSheet;
                MOI.Excel.ListObject oTab = oSheet.ListObjects.Item[1];

                ((MOI.Excel.Range)oSheet.Cells[1, 2]).Value2 = ".";
                ((MOI.Excel.Range)oSheet.Cells[1, 3]).Value2 = "upcomming";
                ((MOI.Excel.Range)oSheet.Cells[1, 4]).Value2 = "reference";
                ((MOI.Excel.Range)oSheet.Cells[1, 5]).Value2 = "confined";
                ((MOI.Excel.Range)oSheet.Cells[1, 6]).Value2 = "decomm";


                row = 2;
                DateTime d = new DateTime(DateTime.Now.Year, 1, 1);
                double dMin = d.ToOADate(), dMax = dMin + 365 * 5;
                while (F.oCnxBase.Reader.Read())
                {
                    //double dUpStart, dUpEnd, dRefStart, dRefEnd, dConfStart, dConfEnd, dDecomStart, dDecomEnd;
                    ((MOI.Excel.Range)oSheet.Cells[row, 1]).Value2 = F.oCnxBase.Reader.GetString(1);

                    double[] dStart = { 0, 0, 0, 0 }, dEnd = { 0, 0, 0, 0 };
                    bool bInitPeriodeDebut = true;
                    ((MOI.Excel.Range)oSheet.Cells[row, 2]).Value2 = 0;
                    for (int i = 0; i < dStart.Length; i++)
                    {
                        if (!F.oCnxBase.Reader.IsDBNull(2 + i * 2)) dStart[i] = F.oCnxBase.Reader.GetDate(2 + i * 2).ToOADate();
                        if (!F.oCnxBase.Reader.IsDBNull(3 + i * 2)) dEnd[i] = F.oCnxBase.Reader.GetDate(3 + i * 2).ToOADate();
                        if (dEnd[i] <= dMin) ((MOI.Excel.Range)oSheet.Cells[row, 3 + i]).Value2 = 0;
                        else
                        {
                            if (dStart[i] >= dMax) ((MOI.Excel.Range)oSheet.Cells[row, 3 + i]).Value2 = 0;
                            else
                            {
                                if (dStart[i] <= dMin) dStart[i] = dMin;
                                if (dEnd[i] >= dMax) dEnd[i] = dMax;
                                if (dEnd[i] - dStart[i] < 0) ((MOI.Excel.Range)oSheet.Cells[row, 3 + i]).Value2 = 0;
                                else
                                {
                                    ((MOI.Excel.Range)oSheet.Cells[row, 3 + i]).Value2 = dEnd[i] - dStart[i];
                                    //((MOI.Excel.Range)oSheet.Cells[row, 3 + i]).Value2 = dEnd[i] - dStart[i];
                                    if (bInitPeriodeDebut)
                                    {
                                        ((MOI.Excel.Range)oSheet.Cells[row, 2]).Value2 = dStart[i];
                                        bInitPeriodeDebut = false;
                                    }
                                }
                            }
                        }
                    }
                    row++;
                }
                F.oCnxBase.CBReaderClose();

                MOI.Excel.Range rng = (MOI.Excel.Range)oSheet.Range[oSheet.Cells[1, 1], oSheet.Cells[row - 1, 6]];
                oTab.Resize(rng);

                //chart.HasLegend = false;
                chart.Legend.Font.Color = System.Drawing.Color.Black.ToArgb();
                chart.Legend.Font.Size = 8;
                chart.Legend.Position = MOI.PowerPoint.XlLegendPosition.xlLegendPositionBottom;

                for (int i = 1; i <= 5; i++)
                {
                    MOI.PowerPoint.Series series = (MOI.PowerPoint.Series)chart.SeriesCollection(i);
                    switch (series.Name[0])
                    {
                        case '.':
                            series.Format.Fill.Visible = MsoTriState.msoFalse;
                            break;
                        case 'u':
                            series.Format.Fill.ForeColor.RGB = System.Drawing.Color.FromArgb(104, 242, 120).ToArgb(); //.FromArgb(120, 242, 104).ToArgb();
                            break;
                        case 'r':
                            series.Format.Fill.ForeColor.RGB = System.Drawing.Color.FromArgb(17, 197, 38).ToArgb(); //.FromArgb(38, 197, 17).ToArgb();
                            break;
                        case 'c':
                            series.Format.Fill.ForeColor.RGB = System.Drawing.Color.FromArgb(134, 153, 250).ToArgb(); //.FromArgb(250, 153, 134).ToArgb();
                            break;
                        case 'd':
                            series.Format.Fill.ForeColor.RGB = System.Drawing.Color.FromArgb(10, 45, 220).ToArgb(); //.FromArgb(220, 45, 10).ToArgb();
                            break;
                    }
                }

                MOI.PowerPoint.Axis valaxis = (MOI.PowerPoint.Axis)chart.Axes(MOI.PowerPoint.XlAxisType.xlValue, MOI.PowerPoint.XlAxisGroup.xlPrimary);
                //valaxis.HasTitle = true;
                //Setting values axis units
                //valaxis.MajorUnit = 6.8;
                //valaxis.MinorUnitIsAuto = true;
                valaxis.MinimumScaleIsAuto = false;
                valaxis.MinimumScale = dMin;
                valaxis.MaximumScaleIsAuto = false;
                valaxis.MaximumScale = dMax;
                valaxis.MajorUnitIsAuto = false;
                valaxis.MajorUnit = 366;
                valaxis.MajorGridlines.Format.Line.ForeColor.RGB = 0;
                valaxis.Format.Line.ForeColor.RGB = 0;
                valaxis.TickLabels.Font.Size = 8;
                valaxis.TickLabels.Font.Color = System.Drawing.Color.Black.ToArgb();
                valaxis.TickLabels.NumberFormat = "yyyy";

                MOI.PowerPoint.Axis yaxis = (MOI.PowerPoint.Axis)chart.Axes(MOI.PowerPoint.XlAxisType.xlCategory, MOI.PowerPoint.XlAxisGroup.xlPrimary);
                yaxis.MajorGridlines.Format.Line.ForeColor.RGB = 0;
                yaxis.Format.Line.ForeColor.RGB = 0;
                yaxis.TickLabels.Font.Size = 8;
                yaxis.TickLabels.Font.Color = System.Drawing.Color.Black.ToArgb();

                oWB.Close();
            }
            F.oCnxBase.CBReaderClose();

            return dia.Shapes.Count;
        }


        public override object InsertFormeFromId(string Id, bool init, string texte, object oStyle)
        {
            MOI.PowerPoint.Slide dia = Slides[Convert.ToInt32(Id)];
            stpptforme stForme = (stpptforme)oStyle;

            MOI.PowerPoint.Shape forme = dia.Shapes.AddShape(MsoAutoShapeType.msoShapeRectangle, stForme.x, stForme.y, stForme.width, stForme.height);
            forme.Shadow.Type = MsoShadowType.msoShadow6;
            forme.Shadow.Transparency = 0;
            forme.Shadow.OffsetX = 3;
            forme.Shadow.OffsetY = 3;
            forme.Shadow.ForeColor.RGB = System.Drawing.Color.FromArgb(63, 103, 25).ToArgb();
            forme.Fill.ForeColor.RGB = System.Drawing.Color.White.ToArgb();
            forme.Line.Style = MsoLineStyle.msoLineSingle;
            forme.Line.ForeColor.RGB = System.Drawing.Color.FromArgb(99, 147, 20).ToArgb();

            forme = dia.Shapes.AddShape(MsoAutoShapeType.msoShapeRectangle, stForme.x + 10, stForme.y - 8, 150, 16);
            forme.Fill.ForeColor.RGB = System.Drawing.Color.FromArgb(63, 103, 25).ToArgb();
            forme.Fill.TwoColorGradient(MsoGradientStyle.msoGradientDiagonalUp, 1);
            forme.Fill.BackColor.RGB = System.Drawing.Color.FromArgb(100, 158, 42).ToArgb();
            AddTexte(forme.TextFrame.TextRange, true, texte, 10, MsoTriState.msoTrue, System.Drawing.Color.White.ToArgb());

            forme = dia.Shapes.AddShape(MsoAutoShapeType.msoShapeRectangle, stForme.x + 5, stForme.y + 15, stForme.width - 10, stForme.height - 20);
            forme.Fill.ForeColor.RGB = System.Drawing.Color.FromArgb(249, 249, 249).ToArgb();
            forme.TextFrame.TextRange.Font.Size = 10;
            forme.TextFrame.TextRange.Font.Color.RGB = System.Drawing.Color.FromArgb(86, 86, 86).ToArgb();
            forme.TextFrame.VerticalAnchor = MsoVerticalAnchor.msoAnchorTop;
            forme.TextFrame.TextRange.ParagraphFormat.Alignment = MOI.PowerPoint.PpParagraphAlignment.ppAlignLeft;

            return dia.Shapes.Count;
        }

        public void InserRowTab(int iSlide, int iShape, object oTab, string[] aCols)
        {
            MOI.PowerPoint.Slide dia = Slides[iSlide];
            MOI.PowerPoint.Shape forme = dia.Shapes[iShape];

            MOI.PowerPoint.Shape Tab = (MOI.PowerPoint.Shape)oTab;
            Tab.Table.Rows.Add();
            for (int iCol = 1; iCol <= aCols.Length; iCol++)
            {
                //Tab.Table.Cell(1, iCol).Shape.TextFrame.TextRange.Font.Size = 10;
                Tab.Table.Cell(Tab.Table.Rows.Count, iCol).Shape.TextFrame.TextRange.Text = aCols[iCol - 1];
            }

        }

        public object InserHeadTab(int iSlide, int iShape, bool append, int nbrTab, int idxTab, string[] aCols, object ostyle, object ostyleTab)
        {
            MOI.PowerPoint.Slide dia = Slides[iSlide];
            MOI.PowerPoint.Shape forme = dia.Shapes[iShape];
            stpptStyle pptS = (stpptStyle)ostyle;
            stpptStyleTab pptSTab;
            if (ostyleTab == null)
            {
                pptSTab.tabHeader = true;
            }
            else pptSTab = (stpptStyleTab)ostyleTab;


            int iWidth = (int)forme.Width / nbrTab;
            MOI.PowerPoint.Shape Tab = dia.Shapes.AddTable(1, aCols.Length, iWidth * idxTab + forme.Left + 5, forme.Top + 5, iWidth - 10, 1);
            if (!pptSTab.tabHeader)
            {
                Tab.Table.FirstRow = false;
                Tab.Table.HorizBanding = false;
                Tab.Table.VertBanding = true;
            }
            for (int iCol = 1; iCol <= aCols.Length; iCol++)
            {
                Tab.Table.Cell(1, iCol).Shape.TextFrame.TextRange.Font.Size = pptS.size;
                Tab.Table.Cell(1, iCol).Shape.TextFrame.TextRange.Text = aCols[iCol - 1];
            }
            return Tab;
        }

        public int GetNbrRowMaxFromTab(int iSlide, int iShape, object ostyle)
        {
            MOI.PowerPoint.Slide dia = Slides[iSlide];
            MOI.PowerPoint.Shape forme = dia.Shapes[iShape];

            stpptStyle pptS = (stpptStyle) ostyle;

            MOI.PowerPoint.Shape Tab = dia.Shapes.AddTable(1, 1, forme.Left + 5, forme.Top + 5, forme.Width - 10, 1);
            Tab.Table.Cell(1, 1).Shape.TextFrame.TextRange.Font.Size = pptS.size;
            while (Tab.Height < forme.Height - 10) Tab.Table.Rows.Add();
            if (Tab.Table.Rows.Count >= 1) Tab.Table.Rows[1].Delete();

            int iRow = Tab.Table.Rows.Count;
            Tab.Delete();
            return iRow;
        }

        public int GetRows(object oTab)
        {
            MOI.PowerPoint.Shape Tab = (MOI.PowerPoint.Shape)oTab;
            return Tab.Table.Rows.Count;
        }

        public override void InsertTabFromId(string Id, bool init, ArrayList lstTab, object ostyle, object ostyleTab)
        {
            string[] aIds = Id.Split(',');
            stpptStyleTab pptSTab;
            if (ostyleTab == null)
            {
                pptSTab.tabHeader = true;
            } else pptSTab = (stpptStyleTab)ostyleTab;
            if (aIds.Length == 2)
            {
                int iSlide = Convert.ToInt32(aIds[0]);
                int iShape = Convert.ToInt32(aIds[1]);
                int iNbrRowMax = 0, iNbrRow = 0;

                iNbrRowMax = GetNbrRowMaxFromTab(iSlide, iShape, ostyle);


                iNbrRow = lstTab.Count;

                int iTabTemp = iNbrRow / iNbrRowMax + (iNbrRow % iNbrRowMax > 0 ? 1 : 0);
                int iTab = iTabTemp, idxTab = 0;
                if (pptSTab.tabHeader) iTab = (iNbrRow + iTabTemp - 1) / iNbrRowMax + 1;

                if (iNbrRow > 1)
                {
                    string[] aHeadCols = new string[((ArrayList)lstTab[0]).Count];
                    for (int j = 0; j < ((ArrayList)lstTab[0]).Count; j++) aHeadCols[j] = (string)((ArrayList)lstTab[0])[j];
                    object oTab = InserHeadTab(iSlide, iShape, true, iTab, idxTab++, aHeadCols, ostyle, ostyleTab);

                    for (int i = 1; i < lstTab.Count;  i++)
                    {
                        string[] aCols = new string[((ArrayList)lstTab[i]).Count];
                        for (int j = 0; j < ((ArrayList)lstTab[i]).Count; j++) aCols[j] = (string)((ArrayList)lstTab[i])[j];

                        InserRowTab(iSlide, iShape, oTab, aCols);
                        if (GetRows(oTab) >= iNbrRowMax && i < lstTab.Count - 1)
                        {
                            if (!pptSTab.tabHeader )
                            {
                                i++;
                                for (int j = 0; j < ((ArrayList)lstTab[0]).Count; j++) aHeadCols[j] = (string)((ArrayList)lstTab[i])[j];
                            }
                            oTab = InserHeadTab(iSlide, iShape, true, iTab, idxTab++, aHeadCols, ostyle, ostyleTab);
                        }
                    }
                }
            }
        }

        public override void InsertTabFromId(string Id, bool init, string sGuidObj, XmlDB xmlDB, object ostyle, string sType="")
        {
            if (xmlDB.SetCursor(sGuidObj))
            {
                string[] aIds = Id.Split(',');
                if (aIds.Length == 2)
                {
                    int iSlide = Convert.ToInt32(aIds[0]);
                    int iShape = Convert.ToInt32(aIds[1]);
                    if(sType=="") sType =  xmlDB.XmlGetName(xmlDB.GetCursor());
                    int iNbrRowMax = 0, iNbrFieldTab = 0;

                    iNbrRowMax = GetNbrRowMaxFromTab(iSlide, iShape, ostyle);

                    
                    int n = F.oCnxBase.ConfDB.FindTable(sType);
                    if (n > -1)
                    {
                        Table t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                        iNbrFieldTab = t.GetNbrTabField();
                        int iTabTemp = iNbrFieldTab / iNbrRowMax + 1;
                        int iTab = (iNbrFieldTab + iTabTemp) / iNbrRowMax + 1, idxTab = 0;

                        string[] aCols = { "Propriété", "Valeur" };

                        object oTab = InserHeadTab(iSlide, iShape, true, iTab, idxTab++, aCols, ostyle, null);

                        for (int i = 0; i < t.LstField.Count; i++)
                        {
                            if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.TabNonVisible) == 0)
                            {
                                aCols[0] = ((Field)t.LstField[i]).Name;
                                if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.ExternKeyTable) != 0)
                                {
                                    int m = ((Field)t.LstField[i]).TableEx;
                                    if (m > -1)
                                    {
                                        Table tex = (Table)F.oCnxBase.ConfDB.LstTable[((Field)t.LstField[i]).TableEx];
                                        aCols[0] = tex.GetFields(ConfDataBase.FieldOption.NomRef);
                                    }
                                    
                                }
                                aCols[1] = xmlDB.XmlGetAttValueAFromAttValueB(xmlDB.GetCursor(), "Value", "Nom", aCols[0]);
                                InserRowTab(iSlide, iShape, oTab, aCols);
                                if (GetRows(oTab) >= iNbrRowMax)
                                {
                                    aCols[0] = "Propriété"; aCols[1] = "Valeur";
                                    oTab = InserHeadTab(iSlide, iShape, true, iTab, idxTab++, aCols, ostyle, null);
                                }
                            }
                        }
                    }
                }
                xmlDB.CursorClose();
            }
        }

        public void AddTexte(MOI.PowerPoint.TextRange txt, bool init, string s, float size, MsoTriState bold, int Couleur)
        {
            //txt.Font.Size = size;
            //txt.Font.Bold = bold;
            //txt.Font.Color.RGB = Couleur;
            if (init) txt.Text = s;
            else
            {
                txt = txt.InsertAfter("\n" + s);
                //txt = txt.InsertAfter(s);
            }
            txt.Font.Size = size;
            txt.Font.Bold = bold;
            txt.Font.Color.RGB = Couleur;
        }

        public override object CreatSlide(string item, string titre)
        {
            int iSlide = Slides.Count + 1;
            Slides.AddSlide(iSlide, Slides[iSlide - 1].CustomLayout);
            MOI.PowerPoint.Slide dia = Slides[iSlide];
            if (dia.Shapes.Count == 5) dia.Shapes[1].Delete();
            Slides[iSlide].Tags.Add("Item", item);
            MOI.PowerPoint.Shape forme = dia.Shapes[1];
            MOI.PowerPoint.TextRange txt = forme.TextFrame.TextRange;


            AddTexte(txt, true, titre, txt.Font.Size, txt.Font.Bold, txt.Font.Color.RGB);
            return iSlide;
        }

        public override void InsertTabFromId(string Id, bool init, DrawObject o, object ostyle, bool bIndex, string sEntry, bool bComment = true)
        {

            System.Xml.XmlElement elxml = o.XmlInsertProperties(xmlExcel, null);
            
        }

        public override void InsertTextFromId(string Id, bool init, string texte, object ostyle)
        {
            stpptStyle pptS = (stpptStyle)ostyle;
            string[] sIds = Id.Split(',');
            if (sIds.Length == 2)
            {
                int iSlide = Convert.ToInt32(sIds[0]);
                int iShape = Convert.ToInt32(sIds[1]);

                MOI.PowerPoint.Slide dia = Slides[iSlide];
                MOI.PowerPoint.Shape forme = dia.Shapes[iShape];
                MOI.PowerPoint.TextRange txt = forme.TextFrame.TextRange;
                AddTexte(txt, init, texte, pptS.size, pptS.bold, pptS.Couleur);

            }
        }
    }

    public class ControlHtml : DrawTools.ControlDoc
    {
        private HtmlDocument doc;
        private XmlExcel xmlExcel;

        public ControlHtml(Form1 form, string sTemplate)
        {
            
            F = form;

            doc = new HtmlDocument();
            doc.Load(sTemplate);

            xmlExcel = new XmlExcel(F, "Rows");
            
            //form.docXml = new System.Xml.XmlDocument();
            //form.docXml.LoadXml("<" + sTemplate.Substring(sTemplate.LastIndexOf('\\') + 1) + "></" + sTemplate.Substring(sTemplate.LastIndexOf('\\') + 1) + ">");
        }

        public override int Exist(string sSearch)
        {
            if (doc.GetElementbyId(sSearch) != null) return (0);
            return (-1);
        }

        public override void SaveDoc()
        {
            doc.Save(sDocPath + @"\index.html");
        }


        public override void InsertHeadTabFromId(string Id, bool init, Table t, object ostyle)
        {
            string Style = "DtTableauDAT";
            if (ostyle != null) Style = (string)ostyle;

            HtmlNode el = doc.GetElementbyId(Id);
            HtmlNode Tab = null, th = null, tf = null, tb = null, trow = null;
            //el.AppendChild(dev);
            if (init) el.RemoveAllChildren();
            Tab = doc.CreateElement("table");
            th = doc.CreateElement("thead");
            tf = doc.CreateElement("tfoot");
            tb = doc.CreateElement("tbody");

            trow = doc.CreateElement("tr");

            int j = 1;
            for (int i = 0; i < t.LstField.Count; i++)
            {
                if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.TabNonVisible) == 0)
                {
                    HtmlNode tcell = doc.CreateElement("th");
                    tcell.InnerHtml = ((Field)t.LstField[i]).Libelle;
                    tcell.Attributes.Add("style", "width:" + ((Field)t.LstField[i]).Width + "px");
                    trow.AppendChild(tcell);
                    //tb.Columns[j].Width = ((Field)t.LstField[i]).Width;
                    j++;
                }
            }
            th.AppendChild(trow);
            Tab.AppendChild(th);
            Tab.AppendChild(tf);
            Tab.AppendChild(tb);
            el.AppendChild(Tab);
        }

        public override void InsertTextFromId(string Id, bool init, string texte, object ostyle)
        {
            string sNode = "p";
            bool bUnderline = false;
            if (ostyle != null)
            {
                if ((string)ostyle == "Titre 0") sNode = "h0";
                else if ((string)ostyle == "Titre 1") sNode = "h1";
                else if ((string)ostyle == "Titre 2") sNode = "h2";
                else if ((string)ostyle == "Titre 3") sNode = "h3";
                else if ((string)ostyle == "Titre 4") sNode = "h4";
                else if ((string)ostyle == "Titre 5") sNode = "h5";
                else if ((string)ostyle == "Titre 6") sNode = "h6";
                else if ((string)ostyle == "Titre 7") sNode = "h7";
                else if ((string)ostyle == "UnderLine") bUnderline = true;
                else if ((string)ostyle == "p") bUnderline = true;
            }


            HtmlNode el = doc.GetElementbyId(Id);

            if (init) el.RemoveAllChildren();

            HtmlNode Node = doc.CreateElement(sNode);
            if (bUnderline) Node.InnerHtml = "<u>" + texte.Replace("\n", " ") + "</u>";
            else Node.InnerHtml = texte.Replace("\n"," ");
            el.AppendChild(Node);
        }

        public override void CreatIdFromIdP(string Id, string IdP)
        {
            HtmlNode el = doc.GetElementbyId(IdP);
            HtmlNode Node = doc.CreateElement("div");
            Node.Attributes.Add("id", Id);
            el.AppendChild(Node);
        }

        private HtmlNode GetElFromEl(HtmlNode el, string sSearch)
        {
            for (int i = 0; i < el.ChildNodes.Count; i++)
            {
                HtmlNode eli = el.ChildNodes[i];
                if (eli.Name == sSearch) return eli;
                HtmlNode elx = GetElFromEl(eli, sSearch);
                if (elx != null) return elx;
            }
            return null;
        }

        public override object InsertRowFromId(string Id, DrawObject o)
        {
            HtmlNode el = doc.GetElementbyId(Id);
            HtmlNode eltBody = GetElFromEl(el, "tbody");

            if (eltBody != null)
            {
                System.Xml.XmlElement elxml = o.XmlInsertProperties(xmlExcel);

                if (elxml.Attributes.Count > 0)
                {
                    HtmlNode trow = doc.CreateElement("tr");
                    for (int i = 0; i < elxml.Attributes.Count; i++)
                    {
                        HtmlNode td = doc.CreateElement("td");
                        td.InnerHtml = elxml.Attributes[i].Value;
                        trow.AppendChild(td);
                    }
                    eltBody.AppendChild(trow);
                    return trow;
                }
            }
            return null;
        }

        private string OpenTag(char cTag, ref int ActivedLevel, ref char[] aTag)
        {
            char[] aTagLoc = { ' ', ' ', ' ', ' ', ' ' };
            string sHtml = "";
            for (int i = 0; i < aTag.Length; i++) aTagLoc[i] = aTag[i];
            if (ActivedLevel < 5)
            {
                if (cTag == 'l')
                {
                    int j = ActivedLevel;
                    for (int i = ActivedLevel - 1; i >= 0; i--) sHtml += CloseTag(aTag[ActivedLevel - 1], ref ActivedLevel, ref aTag);
                    aTag[ActivedLevel] = cTag;
                    sHtml += "<ul><li>";
                    ActivedLevel = 1;
                    for (int i = 0; i < j; i++) sHtml += OpenTag(aTagLoc[i], ref ActivedLevel, ref aTag);
                }
                else
                {
                    aTag[ActivedLevel++] = cTag;
                    sHtml += "<" + cTag + ">";
                }

            }
            return sHtml;
        }

        private string CloseTag(char cTag, ref int ActivedLevel, ref char[] aTag)
        {
            char[] aTagLoc = { ' ', ' ', ' ', ' ', ' ' };
            string sHtml = "";
            for (int i = 0; i < aTag.Length; i++) aTagLoc[i] = aTag[i];
            int j = ActivedLevel;

            for (int i = ActivedLevel - 1; i >= 0; i--)
            {
                if (cTag == aTag[i])
                {
                    if (cTag == 'l') sHtml += "</ul>"; else sHtml += "</" + aTag[i] + ">";
                    ActivedLevel--;
                    for (int k = i + 1; k < j; k++) sHtml += OpenTag(aTagLoc[k], ref ActivedLevel, ref aTag);
                    break;
                }
                else
                {
                    sHtml += "</" + aTag[i] + ">";
                    ActivedLevel--;
                }
            }
            return sHtml;
        }

        public override string Format(string rtf)
        {
            int idx = 0;
            char[] aTab = { ' ', ' ', ' ', ' ', ' ' }; int iActivedLevel = 0;
            //string sHtml = "<p>";
            string sHtml = OpenTag('p', ref iActivedLevel, ref aTab);
            bool bRTFControl = false;
            bool bExistBullet = false;
            while (idx < rtf.Length)
            {
                char chNext = rtf[idx];
                if (chNext == '{' || chNext == '}' || chNext == '\\')
                {
                    // Escaped char
                    idx++;
                    bRTFControl = true;
                    continue;
                }
                if (bRTFControl)
                {
                    bRTFControl = false;
                    Regex r2 = new Regex(@"([\*'a-z\-]*)([0-9]*)([ }]*)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    Match m = r2.Match(rtf, idx);
                    string stCtrlWord = m.Groups[1].ToString();
                    string stCtrlParam = m.Groups[2].ToString();
                    string stCtrlEspace = m.Groups[3].ToString();

                    if (stCtrlWord == "fcharset")
                    {
                        //idx = rtf.IndexOf(";", idx + m.Length) - m.Length + 1;
                        //idx += m.Length;
                        idx = rtf.IndexOf(";", idx + m.Length) + 1;
                    }
                    else if (stCtrlWord == "fs")
                    {
                        idx += m.Length;
                    }
                    else if (stCtrlWord == "fnil")
                    {
                        int idx1 = rtf.IndexOf(";", idx + m.Length) + 1;
                        int idx2 = rtf.IndexOf(@"\", idx + m.Length);
                        if (idx1 <= idx2) idx = idx1; else idx = idx2;
                    }
                    else if (stCtrlWord == "colortbl")
                    {
                        idx = rtf.IndexOf("}", idx + m.Length);
                    }
                    else if (stCtrlWord.Length > 0 && stCtrlWord[0] == '\'')
                    {
                        if (rtf.Substring(idx + 1, 2) != "B7") sHtml += (char)Convert.ToInt32(rtf.Substring(idx + 1, 2), 16);
                        idx += 3;
                    }
                    else if (stCtrlWord == "par")
                    {
                        //if (bExistBullet) { sHtml += "</ul>"; bExistBullet = false; }
                        if (bExistBullet) { sHtml += CloseTag('l', ref iActivedLevel, ref aTab); bExistBullet = false; }
                        //sHtml += "</p><p>";
                        sHtml += CloseTag('p', ref iActivedLevel, ref aTab) + OpenTag('p', ref iActivedLevel, ref aTab);
                        idx += m.Length;
                    }
                    else if (stCtrlWord == "pntext")
                    {
                        bExistBullet = true;
                        //sHtml += "<ul><li>";
                        sHtml += OpenTag('l', ref iActivedLevel, ref aTab);

                        idx += m.Length;
                    }
                    else if (stCtrlWord == "b")
                    {
                        if (stCtrlParam.Length > 0 && stCtrlParam == "0")
                        {
                            //sHtml += "</b>";
                            sHtml += CloseTag('b', ref iActivedLevel, ref aTab);
                        }
                        //else sHtml += "<b>";
                        else sHtml += OpenTag('b', ref iActivedLevel, ref aTab);
                        idx += m.Length;
                    }
                    else if (stCtrlWord == "i")
                    {
                        if (stCtrlParam.Length > 0 && stCtrlParam == "0")
                        {
                            //sHtml += "</i>";
                            sHtml += CloseTag('i', ref iActivedLevel, ref aTab);
                        }
                        //else sHtml += "<i>";
                        else sHtml += OpenTag('i', ref iActivedLevel, ref aTab);
                        idx += m.Length;
                    }
                    else if (stCtrlWord == "ul")
                    {
                        //sHtml += "<u>";
                        sHtml += OpenTag('u', ref iActivedLevel, ref aTab);
                        idx += m.Length;
                    }
                    else if (stCtrlWord == "ulnone")
                    {
                        //sHtml += "</u>";
                        sHtml += CloseTag('u', ref iActivedLevel, ref aTab);
                        idx += m.Length;
                    }
                    else if (stCtrlWord == "li")
                    {
                        idx += m.Length;
                    }
                    else if (stCtrlWord == "rquote")
                    {
                        sHtml += "'";
                        idx += m.Length;
                    }
                    else
                    {
                        //string control non traite
                        idx += m.Length;
                    }
                    if (stCtrlEspace.Length > 1)
                        for (int i = 1; i < stCtrlEspace.Length; i++) sHtml += " ";
                }
                else
                {
                    if (chNext == '\r' || chNext == '\n')
                    {
                        idx++;
                        continue;
                    }
                    sHtml += rtf[idx++];
                }
            }
            sHtml += "</p";
            return sHtml;
        }

        
        public override void InsertRichTextFromId(string Id, bool init, byte[] bRtf)
        {
            if (bRtf.Length != 0)
            {
                HtmlNode el = doc.GetElementbyId(Id);
                HtmlNode Node = doc.CreateElement("p");
                
                if (init) el.RemoveAllChildren();
                
                System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
                rtBox.Rtf = System.Text.Encoding.UTF8.GetString(bRtf);
                Node.InnerHtml = Format(rtBox.Rtf);
                el.AppendChild(Node);
            }
        }

        public override void InsertImgFromId(string Id, bool init, string pathing, object oStyle)
        {

            HtmlNode el = doc.GetElementbyId(Id);
            
            if (init) el.RemoveAllChildren();
            HtmlNode Node = doc.CreateElement("img");
            pathing.Substring(pathing.LastIndexOf("\\"));
            Node.Attributes.Add("src", "images/" + pathing.Substring(pathing.LastIndexOf("\\") + 1));
            el.AppendChild(Node);
            //File.Move(pathimg, @"c:\Temp\aaaa" + @"\images" + pathimg.Substring(pathimg.LastIndexOf("\\")));
        }

        public override void InsertTabFromId(string Id, bool init, DrawObject o, object ostyle, bool bIndex, string sEntry, bool bComment = true)
        {
            string Style = "DtTableauDAT";
            if (ostyle != null) Style = (string)ostyle;

            HtmlNode el = doc.GetElementbyId(Id);
            if (init) el.RemoveAllChildren();

            System.Xml.XmlElement elxml = o.XmlInsertProperties(xmlExcel, null, bComment);

            CreateTabFromXmlEl(el, elxml);            
        }

        public override void InsertRowFromReaderId(string Id, OdbcDataReader r, string sType)
        {
            int n = F.oCnxBase.ConfDB.FindTable(sType);
            if (n > -1)
            {
                Field field;
                Table t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                HtmlNode el = doc.GetElementbyId(Id);
                HtmlNode elTbody = GetElFromEl(el, "tbody");
                HtmlNode elrow = doc.CreateElement("tr"); elTbody.AppendChild(elrow);
                HtmlNode elcol;


                //for (int i = 0; i < r.FieldCount; i++)
                for (int i = 0, iw = 1, j = 0; j < t.LstField.Count; j++)
                {
                    field = (Field)t.LstField[j];
                    if ((((Field)t.LstField[j]).fieldOption & ConfDataBase.FieldOption.Select) != 0)
                    {
                        if ((((Field)t.LstField[j]).fieldOption & ConfDataBase.FieldOption.TabNonVisible) == 0)
                        {
                            elcol =doc.CreateElement("td"); elrow.AppendChild(elcol);
                            switch (field.Type)
                            //switch (r.GetFieldType(i).ToString()[7])
                            {
                                case 's': //typeof(System.String):
                                    string sField = "";
                                    if (!r.IsDBNull(i)) sField = r.GetString(i);
                                    elcol.InnerHtml = sField;
                                    break;
                                case 't': //typeof(System.DateTime):
                                    if (r.GetFieldType(i) == typeof(double)) elcol.InnerHtml = DateTime.FromOADate(r.GetDouble(i)).ToShortDateString();
                                    else elcol.InnerHtml = r.GetDate(i).ToShortDateString();
                                    break;
                                case 'i': //typeof(System.Int16):
                                    elcol.InnerHtml = r.GetInt16(i).ToString();
                                    break;
                                case 'p': //typeof(System.Int16):
                                    //elcol.InnerHtml = "<img src=\"" + System.IO.Directory.GetCurrentDirectory() + '\\' + F.sImgList[(int)Form1.ImgList.Nettbd + r.GetInt16(i)] + "\">";
                                    elcol.InnerHtml = "<img src=\"..\\..\\..\\" + F.sImgList[(int)Form1.ImgList.Nettbd + r.GetInt16(i)] + "\">";
                                    break;
                                case 'q': //typeof(System.Int16):
                                    Form1.ImgList fimglistEng = Form1.ImgList.fail;
                                    DateTime dt = DateTime.Now, dtc;
                                    TimeSpan ts = new TimeSpan(180, 0, 0, 0);
                                    //int ifield = t.FindField("DateFinMain");
                                    if (r.GetFieldType(i - 1) == typeof(double)) dtc = DateTime.FromOADate(r.GetDouble(i - 1));
                                    else dtc = r.GetDate(i - 1);
                                    if (DateTime.Compare(dtc, dt) < 0) fimglistEng = Form1.ImgList.fail;
                                    else if (DateTime.Compare(dtc - ts, dt) < 0) fimglistEng = Form1.ImgList.alert;
                                    else fimglistEng = Form1.ImgList.pass;
                                    //elcol.InnerHtml = "<img id=\"imgTab\" src=\"" + System.IO.Directory.GetCurrentDirectory() + '\\' + F.sImgList[(int)fimglistEng] + "\">";
                                    elcol.InnerHtml = "<img id=\"imgTab\" src=\"..\\..\\..\\" + F.sImgList[(int)fimglistEng] + "\">";
                                    break;
                            }
                            iw++;
                        }
                        i++;
                    }
                }
            }
        }

        private void CreateTabFromXmlEl(HtmlNode el, System.Xml.XmlElement elxml)
        {
            if (elxml.HasChildNodes)
            {
                HtmlNode Tab = doc.CreateElement("table"), th = doc.CreateElement("thead"), tf = doc.CreateElement("tfoot");
                HtmlNode tb = doc.CreateElement("tbody"), trow = null, tcol = null, span = null;
                el.AppendChild(Tab);
                Tab.AppendChild(th); Tab.AppendChild(tf); Tab.AppendChild(tb);
                bool bHeader = true;

                IEnumerator ienum = elxml.GetEnumerator();
                System.Xml.XmlNode NodeRow;

                while (ienum.MoveNext())
                {
                    NodeRow = (System.Xml.XmlNode)ienum.Current;
                    if (NodeRow.NodeType == System.Xml.XmlNodeType.Element)
                    {
                        if (NodeRow.Name == "tr")
                        {
                            
                            trow = doc.CreateElement("tr"); tb.AppendChild(trow);
                            IEnumerator ienumCol = NodeRow.GetEnumerator();
                            System.Xml.XmlNode NodeCol;

                            while (ienumCol.MoveNext())
                            {
                                NodeCol = (System.Xml.XmlNode)ienumCol.Current;
                                if (NodeCol.NodeType == System.Xml.XmlNodeType.Element)
                                {
                                    if (NodeCol.Name == "td")
                                    {
                                        if(bHeader) tcol = doc.CreateElement("th"); else tcol = doc.CreateElement("td");
                                        trow.AppendChild(tcol);

                                        IEnumerator ienumSpan = NodeCol.GetEnumerator();
                                        System.Xml.XmlNode NodeSpan;

                                        while (ienumSpan.MoveNext())
                                        {
                                            NodeSpan = (System.Xml.XmlNode)ienumSpan.Current;
                                            if (NodeSpan.NodeType == System.Xml.XmlNodeType.Text) tcol.InnerHtml = NodeSpan.Value;
                                            else if (NodeSpan.NodeType == System.Xml.XmlNodeType.Element)
                                            {
                                                if (NodeSpan.Name == "p")
                                                {
                                                    span = doc.CreateElement("p"); tcol.AppendChild(span);
                                                    IEnumerator ienumTab = NodeSpan.GetEnumerator();
                                                    System.Xml.XmlNode NodeTab;
                                                    while (ienumTab.MoveNext())
                                                    {
                                                        NodeTab = (System.Xml.XmlNode)ienumTab.Current;
                                                        if (NodeTab.NodeType == System.Xml.XmlNodeType.Text) span.InnerHtml = NodeTab.Value;
                                                        else if (NodeTab.NodeType == System.Xml.XmlNodeType.Element)
                                                        {
                                                            if (NodeTab.Name == "Root")
                                                            {
                                                                CreateTabFromXmlEl(span, (System.Xml.XmlElement)NodeTab);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                        }
                        bHeader = false;
                    }
                }
            }
        }
    }
    
    public class ControlWord : DrawTools.ControlDoc
    {
        
        private static object missing = System.Type.Missing;
        private MOI.Word.Application appdoc;
        //private MOI.Word.DocumentClass appdoc;
        private MOI.Word.Document oDoc;
        private object template;
        private bool visible;

        public MOI.Word.Application Appdoc
        //public MOI.Word.Application Appdoc
        {
            get
            {
                return appdoc;
            }
        }

        public ControlWord(Form1 form, string sTemplate, bool bVisible)
        {
            //appdoc = new MOI.Word.DocumentClass();
            appdoc = new MOI.Word.Application();
            
            template = sTemplate;
            visible = bVisible;
            F = form;

            Appdoc.Application.Documents.Add(ref template, ref missing, ref missing, ref missing);
            oDoc = Appdoc.Application.ActiveDocument;
            oDoc.ActiveWindow.Visible = visible;
        }

        public void Close()
        {
            if(oDoc!=null) oDoc.Close(ref missing, ref missing, ref missing);
            oDoc = null;
            appdoc = null;
        }

        public override int Exist(string sSearch)
        {
            if (oDoc.Bookmarks.Exists(sSearch)) return 0;
            return -1;
        }

        public string GetText()
        {
            return oDoc.Content.Text;
        }

        public void DisplayBook(string bmrk)
        {
            object searchtype = MOI.Word.WdGoToItem.wdGoToBookmark;
            object Bmrk = bmrk;

            MOI.Word.Range rng = oDoc.GoTo(ref searchtype, ref missing, ref missing, ref Bmrk);
            rng.Select();
            rng.Copy();

            ControlWord cw = new ControlWord(F, F.sPathRoot + @"\dat_modele-V3R1-Eng.dotx", true);
            cw.oDoc.Content.Paste();
        }

        public void ReplaceBookmark(string bmrk, string texte)
        {
            object searchtype = MOI.Word.WdGoToItem.wdGoToBookmark;
            object Bmrk = bmrk;
            object oUnit = MOI.Word.WdUnits.wdCharacter;
            object oCount = -1;

            MOI.Word.Range rng = oDoc.GoTo(ref searchtype, ref missing, ref missing, ref Bmrk);
            rng.MoveEnd(ref oUnit, ref oCount);
            rng.Text = texte;
        }

        public string GetParagraphe(string sBook, string sTitre)
        {
            object searchtype = MOI.Word.WdGoToItem.wdGoToBookmark;
            object Bmrk = sBook;
            object findtext = sTitre;
            object oUnit = MOI.Word.WdUnits.wdParagraph;
            object oCount = 1;
            string sReturn = "";

            MOI.Word.Range rng = oDoc.GoTo(ref searchtype, ref missing, ref missing, ref Bmrk);
            object FinRng= rng.End;
            if (rng.Find.Execute(ref findtext, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing))
            {
                object DebRng = rng.End+1;
                rng = oDoc.Range(ref DebRng, ref FinRng);
                for (int i = 1; i <= rng.Paragraphs.Count; i++)
                {
                    if (((MOI.Word.Style)rng.Paragraphs[i].get_Style()).NameLocal != "Normal") break;
                    if(rng.Paragraphs[i].Range.Text[0]!='\r') sReturn += rng.Paragraphs[i].Range.Text;
                }
            }
            return sReturn;
        }
       

        public override void InsertRichTextFromId(string Id, bool init, byte[] bRtf)
        {
            object searchtype = MOI.Word.WdGoToItem.wdGoToBookmark;
            object direction = MOI.Word.WdCollapseDirection.wdCollapseEnd;
            object Bmrk = Id;
            object oUnit = MOI.Word.WdUnits.wdCharacter;
            object oCount = -1;
            object Style = "Normal";

            if (bRtf.Length != 0)
            {
                MOI.Word.Range rng = oDoc.GoTo(ref searchtype, ref missing, ref missing, ref Bmrk);
                if (init)
                {
                    rng.MoveEnd(ref oUnit, ref oCount);
                }
                else
                {
                    rng.Collapse(ref direction);
                    rng.SetRange(rng.End - 1, rng.End - 1);
                }
                System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
                rtBox.Rtf = System.Text.Encoding.UTF8.GetString(bRtf);
                rtBox.SelectAll(); rtBox.Copy();
                rng.Paste();
                //rng.Text = rtBox.Text;
                //rng.set_Style(ref Style);
                InsertTextFromId(Id, false, "\n", null);
            }
        }

        public override void InsertTextFromId(string Id, bool init, string texte, object ostyle)
        {
            object searchtype = MOI.Word.WdGoToItem.wdGoToBookmark;
            object direction = MOI.Word.WdCollapseDirection.wdCollapseEnd;
            object Bmrk = Id;
            object oUnit = MOI.Word.WdUnits.wdCharacter;
            object oCount = -1;
            object Style;
            if (ostyle == null) Style = "Normal"; else Style = ostyle;

            MOI.Word.Range rng = oDoc.GoTo(ref searchtype, ref missing, ref missing, ref Bmrk);
            if (init)
            {
                rng.MoveEnd(ref oUnit, ref oCount);
            }
            else
            {
                rng.Collapse(ref direction);
                rng.SetRange(rng.End - 1, rng.End -1);
            }
            rng.Text = texte;
            rng.set_Style(ref Style);
        }

        public override object InsertRowFromId(string Id, DrawObject o)
        {
            object searchtype = MOI.Word.WdGoToItem.wdGoToBookmark;
            object Bmrk = Id;

            MOI.Word.Range rng = oDoc.GoTo(ref searchtype, ref missing, ref missing, ref Bmrk);

            Microsoft.Office.Interop.Word.Table tb = rng.Tables[1];
            o.InsertProperties(tb, tb.Rows.Count);
            return tb.Rows;

        }

        public override void InsertRowFromReaderId(string Id, OdbcDataReader r, string sType)
        {
            object searchtype = MOI.Word.WdGoToItem.wdGoToBookmark;
            object Bmrk = Id;

            int n =  F.oCnxBase.ConfDB.FindTable(sType);
            if (n > -1)
            {
                Field field;
                Table t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                MOI.Word.Range rng = oDoc.GoTo(ref searchtype, ref missing, ref missing, ref Bmrk);

                Microsoft.Office.Interop.Word.Table tb = rng.Tables[1];
                tb.Rows.Add(ref missing);
                
                //for (int i = 0; i < r.FieldCount; i++)
                for (int i=0, iw = 1, j = 0; j < t.LstField.Count; j++)
                {
                    field = (Field)t.LstField[j];
                    if ((((Field)t.LstField[j]).fieldOption & ConfDataBase.FieldOption.Select) != 0)
                    {
                        if ((((Field)t.LstField[j]).fieldOption & ConfDataBase.FieldOption.TabNonVisible) == 0)
                        {
                            switch (field.Type)
                            //switch (r.GetFieldType(i).ToString()[7])
                            {
                                case 's': //typeof(System.String):
                                    string sField = "";
                                    if (!r.IsDBNull(i)) sField = r.GetString(i);
                                    tb.Cell(tb.Rows.Count - 1, iw).Range.Text = sField;
                                    break;
                                case 't': //typeof(System.DateTime):
                                    if (r.GetFieldType(i) == typeof(double)) tb.Cell(tb.Rows.Count - 1, iw).Range.Text = DateTime.FromOADate(r.GetDouble(i)).ToShortDateString();
                                    else tb.Cell(tb.Rows.Count - 1, iw).Range.Text = r.GetDate(i).ToShortDateString();
                                    break;
                                case 'i': //typeof(System.Int16):
                                    tb.Cell(tb.Rows.Count - 1, iw).Range.Text = r.GetInt16(i).ToString();
                                    break;
                                case 'p': //typeof(System.Int16):
                                    tb.Cell(tb.Rows.Count - 1, iw).Range.InlineShapes.AddPicture(System.IO.Directory.GetCurrentDirectory() + '\\' + F.sImgList[(int)Form1.ImgList.Nettbd + r.GetInt16(i)], ref missing, ref missing, ref missing);
                                    tb.Cell(tb.Rows.Count - 1, iw).Range.InlineShapes[1].Height = 15;
                                    tb.Cell(tb.Rows.Count - 1, iw).Range.InlineShapes[1].Width = 15;
                                    break;
                                case 'q': //typeof(System.Int16):
                                    Form1.ImgList fimglistEng = Form1.ImgList.fail;
                                    DateTime dt = DateTime.Now, dtc;
                                    TimeSpan ts = new TimeSpan(180, 0, 0, 0);
                                    //int ifield = t.FindField("DateFinMain");
                                    if (r.GetFieldType(i - 1) == typeof(double)) dtc = DateTime.FromOADate(r.GetDouble(i - 1));
                                    else dtc = r.GetDate(i - 1);
                                    if (DateTime.Compare(dtc, dt) < 0) fimglistEng = Form1.ImgList.fail;
                                    else if (DateTime.Compare(dtc - ts, dt) < 0) fimglistEng = Form1.ImgList.alert;
                                    else fimglistEng = Form1.ImgList.pass;
                                    tb.Cell(tb.Rows.Count - 1, iw).Range.InlineShapes.AddPicture(System.IO.Directory.GetCurrentDirectory() + '\\' + F.sImgList[(int)fimglistEng], ref missing, ref missing, ref missing);
                                    tb.Cell(tb.Rows.Count - 1, iw).Range.InlineShapes[1].Height = 15;
                                    tb.Cell(tb.Rows.Count - 1, iw).Range.InlineShapes[1].Width = 15;
                                    break;
                            }
                            iw++;
                        }
                        i++;
                    }
                }
            }
        }

        public override void InsertHeadTabFromId(string Id, bool init, Table t, object ostyle)
        {
            object searchtype = MOI.Word.WdGoToItem.wdGoToBookmark;
            object direction = MOI.Word.WdCollapseDirection.wdCollapseEnd;
            object Bmrk = Id;
            object oUnit = MOI.Word.WdUnits.wdCharacter;
            object oCount = -1;
            object Style;
            Microsoft.Office.Interop.Word.Table tb;

            if (ostyle == null) Style = "DtTableauDAT"; else Style = ostyle;

            MOI.Word.Range rng = oDoc.GoTo(ref searchtype, ref missing, ref missing, ref Bmrk);
            if (init && rng.Tables.Count > 0)
            {
                tb = rng.Tables[1];
                tb.Delete();
                rng.Collapse(ref direction);
                rng.SetRange(rng.End - 1, rng.End - 1);
            }
            else
            {
                rng.Collapse(ref direction);
                rng.SetRange(rng.End - 1, rng.End - 1);
            }

            rng.Tables.Add(rng, 2, t.GetNbrTabField(), ref missing, ref missing);
            rng = oDoc.GoTo(ref searchtype, ref missing, ref missing, ref Bmrk);

            tb = rng.Tables[1];
            tb.set_Style(ref Style);


            int j = 1;
            for (int i = 0; i < t.LstField.Count; i++)
            {
                if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.TabNonVisible) == 0)
                {
                    tb.Cell(1, j).Range.Text = ((Field)t.LstField[i]).Libelle;
                    tb.Columns[j].Width = ((Field)t.LstField[i]).Width;
                    j++;
                }
            }
        }

        public override void InsertTabFromId(string Id, bool init, DrawObject o, object ostyle, bool bIndex, string sEntry, bool bComment = true)
        {
            object searchtype = MOI.Word.WdGoToItem.wdGoToBookmark;
            object direction = MOI.Word.WdCollapseDirection.wdCollapseEnd;
            object Bmrk = Id;
            object oUnit = MOI.Word.WdUnits.wdCharacter;
            object oCount = 2;
            object Style;
            Microsoft.Office.Interop.Word.Table tb;
            if (ostyle == null) Style = "DtTableauDAT"; else Style = ostyle;

            MOI.Word.Range rng = oDoc.GoTo(ref searchtype, ref missing, ref missing, ref Bmrk);
            if (init && rng.Tables.Count>0)
            {
                tb = rng.Tables[1];
                tb.Delete();
            }
            rng.SetRange(rng.End - 1, rng.End - 1);
            rng.Tables.Add(rng, 2, 1, ref missing, ref missing);
            rng = oDoc.GoTo(ref searchtype, ref missing, ref missing, ref Bmrk);
            tb = rng.Tables[1];
            tb.set_Style(ref Style);
                        
            CWTable cwtb = new CWTable(tb);
            tb.Cell(1, 1).Range.Text = o.Texte;
            if (bIndex)
            {
                if (sEntry != null)
                {
                    object entry = (string) o.GetValueFromName(sEntry) + ":" + o.Texte;
                    object obmrk = Id;

                    oDoc.Indexes.MarkAllEntries(tb.Cell(1, 1).Range, ref entry, ref missing, ref missing, ref missing, ref obmrk, ref missing, ref missing);
                }
            }
            o.InsertProperties(cwtb, 2,1);
            cwtb.Merge();
        }


        public override void InsertImgFromId(string Id, bool init, string pathing, object oStyle)
        {
            object searchtype = MOI.Word.WdGoToItem.wdGoToBookmark;
            object direction = MOI.Word.WdCollapseDirection.wdCollapseEnd;
            object Bmrk = Id;
            object oUnit = MOI.Word.WdUnits.wdCharacter;
            object oCount = -1;

            MOI.Word.Range rng = oDoc.GoTo(ref searchtype, ref missing, ref missing, ref Bmrk);
            if (init)
            {
                rng.MoveEnd(ref oUnit, ref oCount);
            }
            else
            {
                rng.Collapse(ref direction);
                rng.SetRange(rng.End - 1, rng.End - 1);
            }
            rng.InlineShapes.AddPicture(pathing, ref missing, ref missing, ref missing);
        }

        public override void CreatIdFromIdP(string Id, string IdP)
        {
            object searchtype = MOI.Word.WdGoToItem.wdGoToBookmark;
            object inBmrk = IdP;
            object unit = MOI.Word.WdUnits.wdParagraph;
            object count = -1;
            object cset = '\n';

            MOI.Word.Range rng = oDoc.GoTo(ref searchtype, ref missing, ref missing, ref inBmrk);
            rng.MoveEnd(ref unit, ref count);
            rng.SetRange(rng.End, rng.End);
            count = -1;
            rng.MoveStart(ref unit, ref count);
           
            object orng = rng;
            MOI.Word.Bookmark bookmark1 = oDoc.Bookmarks.Add(Id, ref orng);
                
        }
    }
}


