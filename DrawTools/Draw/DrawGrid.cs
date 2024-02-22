using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using MOI = Microsoft.Office.Interop;
 
using System.Data.Odbc;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
    public class DrawGrid : DrawTools.DrawObject
    {
        public ArrayList lstLigne;
        public ArrayList lstVlanClass;

        public DrawGrid()
        {
            Initialize();
        }

        private void SetVlanClass()
        {
            if (F.oCnxBase.CBRecherche("Select GuidVlanClass, NomVlanClass, idxLigne, idxDeb, idxMax, code from VLanClass"))
            {
                OdbcDataReader Reader = F.oCnxBase.Reader;
                while (F.oCnxBase.Reader.Read())
                    lstVlanClass.Add(new GridVlanClass(Reader.GetString(0), Reader.GetString(1),Reader.GetInt16(2), Reader.GetInt16(3), Reader.GetInt16(4), Reader.GetString(5)));
            }
            F.oCnxBase.CBReaderClose();
        }

        private void SetGrid()
        {
            lstLigne.Add(CreateLigne(iLegendWidth, 0));
            ArrayList lstPointRef = (ArrayList)lstLigne[0];

            for (int i = 0; i < lstPointRef.Count; i++)
            {
                double x = ((Point)lstPointRef[i]).X, y = ((Point)lstPointRef[i]).Y;
                while (x < xMaxA4paysage && x >= iLegendWidth && y < yMaxA4paysage && y >= 0)
                {
                    if (!PointExist(lstLigne, new Point((int)x, (int)y)))
                        lstLigne.Insert(0, CreateLigne(x, y));
                    x += lozangeProfondeur * Math.Cos(radianProfondeur);
                    y -= lozangeProfondeur * Math.Sin(radianProfondeur);
                }
                x = ((Point)lstPointRef[i]).X; y = ((Point)lstPointRef[i]).Y;
                while (x < xMaxA4paysage && x >= iLegendWidth && y < yMaxA4paysage && y >= 0)
                {
                    if (!PointExist(lstLigne, new Point((int)x, (int)y)))
                        lstLigne.Add(CreateLigne(x, y));
                    x -= lozangeProfondeur * Math.Cos(radianProfondeur);
                    y += lozangeProfondeur * Math.Sin(radianProfondeur);
                }

            }
        }
        private bool PointExist(ArrayList lstLigne, Point pt)
        {
            for (int i = 0; i < lstLigne.Count; i++)
            {
                ArrayList lstPoint = (ArrayList)lstLigne[i];
                for (int j = 0; j < lstPoint.Count; j++)
                {
                    Point ptcur = (Point)lstPoint[j];
                    if (Math.Abs(pt.X - ptcur.X) < 3 && Math.Abs(pt.Y - ptcur.Y) < 3) return true;
                }
            }
            return false;
        }

        private ArrayList CreateLigne(double x, double y)
        {
            ArrayList lstPoint = new ArrayList();

            while (x < xMaxA4paysage && x >= 0 && y < yMaxA4paysage && y >= 0)
            {

                lstPoint.Add(new Point((int)x, (int)y));
                x += lozangeFace * Math.Cos(radianFace);
                y += lozangeFace * Math.Sin(radianFace);
            }

            return lstPoint;
        }

        public DrawGrid(Form1 of)
        {
            F = of;
            lstLigne = new ArrayList();
            lstVlanClass = new ArrayList();
            Initialize();
            SetGrid();
            SetVlanClass();
        }

        public void MajidxDebut(string sGuidVLanClass)
        {
            for (int i = 0; i < lstVlanClass.Count; i++)
            {
                GridVlanClass gv = (GridVlanClass)lstVlanClass[i];
                if (sGuidVLanClass == gv.GuidVlanClass) {
                    gv.idxDebut++;
                    break;
                }
            }
        }

        public Point GetidxPoint(string sGuidVLanClass)
        {
            Point pt = new Point(-1,-1);
            for (int i = 0; i < lstVlanClass.Count; i++)
            {
                GridVlanClass gv = (GridVlanClass) lstVlanClass[i];
                if (sGuidVLanClass == gv.GuidVlanClass)
                {
                    //pt.X --> idxLigne
                    //pt.Y --> idx
                    if (gv.idxDebut <= gv.idxMax)
                    {
                        pt.X = gv.idxLigne;
                        pt.Y = gv.idxDebut;
                        //gv.idxDebut++;
                        return pt;
                    }
                    else break;
                }
            }

            return pt;
        }

        public void CreatVlan3D()
        {
            for (int i = 0; i < lstVlanClass.Count; i++)
            {
                GridVlanClass gv = (GridVlanClass)lstVlanClass[i];
                if (gv.idxDebutRef != gv.idxDebut)
                {
                    if (gv.idxDebut <= gv.idxMax)
                    {
                        ArrayList lstpt = (ArrayList)lstLigne[gv.idxLigne];
                        int iLeft = (int) (((Point)lstpt[gv.idxDebutRef]).X - 10 * Math.Cos(radianFace));
                        int iTop = (int)(((Point)lstpt[gv.idxDebutRef]).Y - 10 * Math.Sin(radianFace));
                        int iRight = (int)(((Point)lstpt[gv.idxDebut - 1]).X + 30);
                        if (iRight - iLeft < 130) iRight = iLeft + 130;
                        DrawVlan3D dv = new DrawVlan3D(F, gv.NomVlanClass, iLeft, iTop + 65, iRight - iLeft, 15);
                        F.drawArea.GraphicsList.Add(dv);
                    }
                }
            }
        }

        public override void Draw(Graphics g)
        {
            /*
            Pen pen = new Pen(Color, PenWidth);

            for (int i = 0; i < lstLigne.Count; i++)
            {
                ArrayList lstPt = (ArrayList)lstLigne[i];
                g.DrawLine(pen, ((Point) lstPt[0]).X, ((Point)lstPt[0]).Y, ((Point)lstPt[lstPt.Count-1]).X, ((Point)lstPt[lstPt.Count - 1]).Y);
            }
            */
        }

    }

    public class GridVlanClass
    {
        private string sGuidVlanClass;
        private string sNomVlanClass;
        private int iLigne;
        private int iDebutRef;
        private int iDebut;
        private int iMax;
        private string sCode;

        public string GuidVlanClass
        {
            get { return sGuidVlanClass; }
        }

        public string NomVlanClass
        {
            get { return sNomVlanClass; }
        }

        public string Code
        {
            get { return sCode; }
        }

        public int idxLigne
        {
            get { return iLigne; }
        }

        public int idxDebutRef
        {
            get { return iDebutRef; }
        }

        public int idxDebut
        {
            get { return iDebut; }
            set { iDebut = value; }
        }

        public int idxMax
        {
            get { return iMax; }
        }
        
        public GridVlanClass(string sGuidVL, string sNom, int idxL, int idxD, int idxM, string sC)
        {
            sGuidVlanClass = sGuidVL;
            sNomVlanClass = sNom;
            iLigne = idxL;
            iDebut = idxD;
            iDebutRef = idxD;
            iMax = idxM;
            sCode = sC;
        }
    }
}
