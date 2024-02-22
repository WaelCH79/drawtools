using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawPtNiveau : DrawTools.DrawRectangle
	{
        
        static private Color Couleur = Color.Blue;
        static private Color LineCouleur = Color.Blue;
        static private int LineWidth = 1;
        //public double NivAbs, NivOrd;

        public string GuidObj;
        public Form1.rbTypeRecherche TypeR;
        public DrawArea.DrawToolType dToolType;

		public DrawPtNiveau()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawPtNiveau(Form1 of, DrawAxes da, string sGuidObj, string T, ArrayList lstNivEffectifs, Form1.rbTypeRecherche tr, DrawArea.DrawToolType tooltype)
        {
            if (lstNivEffectifs.Count == 4)
            {
                F = of;
                OkMove = true;
                Align = true;
                Rectangle = new Rectangle(1, 1, WidthPtNiveau, HeightPtNiveau);
                LstParent = new ArrayList();
                LstChild = null;
                LstLinkIn = null;
                LstLinkOut = null;
                LstValue = new ArrayList();
                Guidkey = Guid.NewGuid();
                GuidkeyObjet = Guid.NewGuid();
                LstParent.Add(da);
                GuidObj = sGuidObj;
                Texte = T;
                TypeR = tr;
                dToolType = tooltype;

                InitProp();
                SetValueFromName("NivAbs", ((Niveau)lstNivEffectifs[0]).Val);
                SetValueFromName("NivOrd", ((Niveau)lstNivEffectifs[2]).Val);

                Initialize();
            }
        }

        /*public DrawPtNiveau(Form1 of, string sGuidObj, string T, Niveau NivX, Niveau NivY, Form1.rbTypeRecherche tr, DrawArea.DrawToolType tooltype)
        {
            F = of;
            OkMove = true;
            Align = true;
            Rectangle = new Rectangle(1, 1, WidthPtNiveau, HeightPtNiveau);
            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = new ArrayList();
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
            GuidObj = sGuidObj;
            Texte = T;
            NivAbs = NivX;
            NivOrd = NivY;
            TypeR = tr;
            dToolType = tooltype;

            InitProp();
            Initialize();
        }*/

        /*public DrawPtNiveau(Form1 of, string sGuidObj, string T, DrawPtNiveau oPtNiv, DrawArea.DrawToolType tooltype)
        {
            F = of;
            OkMove = true;
            Align = true;
            Rectangle = new Rectangle(1, 1, WidthPtNiveau, HeightPtNiveau);
            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = new ArrayList();
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
            GuidObj = sGuidObj;
            Texte = T;
            NivAbs = new Niveau();  initNiv(NivAbs, oPtNiv.NivAbs);
            //NivOrd = new Niveau[2]; //initNiv(NivOrd, oPtNiv.NivOrd);
            TypeR = oPtNiv.TypeR;
            dToolType = tooltype;
            
            InitProp();
            Initialize();
        }*/

        /*public void initNiv(Niveau[] Niv, Niveau[] Origne)
        {
            if (Origne[0].GetType() == typeof(SupportNiveau)) Niv[0] = new SupportNiveau(F, Origne[0].GuidNiveau, Origne[0].NomNiveau);
            else if (Origne[0].GetType() == typeof(BusinessNiveau)) Niv[0] = new BusinessNiveau(F, Origne[0].GuidNiveau, Origne[0].NomNiveau);
            else if (Origne[0].GetType() == typeof(CoutNiveau)) Niv[0] = new CoutNiveau(F, Origne[0].GuidNiveau, Origne[0].NomNiveau);
            else if (Origne[0].GetType() == typeof(ComplexiteNiveau)) Niv[0] = new ComplexiteNiveau(F, Origne[0].GuidNiveau, Origne[0].NomNiveau);
            else if (Origne[0].GetType() == typeof(ExpertiseNiveau)) Niv[0] = new ExpertiseNiveau(F, Origne[0].GuidNiveau, Origne[0].NomNiveau);
            else if (Origne[0].GetType() == typeof(SecuriteNiveau))
            {
                Niv[0] = new SecuriteNiveau(F, Origne[0].GuidNiveau, Origne[0].NomNiveau);
                if (Origne[1] != null) Niv[1] = new CriticiteNiveau(F, Origne[1].GuidNiveau, Origne[1].NomNiveau);
            }
        }*/

        public DrawPtNiveau(Form1 of, ArrayList lstVal, ArrayList lstValG)
        {
            F = of;
            object o= null;
            OkMove = true;
            Align = true;
            InitRectangle(lstValG);
            LstParent = new ArrayList();
            LstChild = null;
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = lstVal;
            Guidkey = Guid.NewGuid();

            o = GetValueFromLib("Guid");
            if(o!=null)
                GuidkeyObjet = new Guid((string) o);
            o = GetValueFromLib("Nom");
            if (o != null)
                Texte = (string)o;

            Initialize();
        }

        public override Guid GetGuidForObjExp()
        {
            return new Guid(GuidObj);
        }

        public override DrawArea.DrawToolType GetToolTypeForObjExp()
        {
            return dToolType;
        }

        public void AligneObjet()
        {
            
        }
             

        public override bool AttachPointInObject(Point point)
        {
            return false;
        }

        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(LineCouleur, LineWidth);
            Rectangle r =  DrawRectangle.GetNormalizedRectangle(Rectangle);

            int Espace = 40;
            DrawAxes da = (DrawAxes)LstParent[0];
            double NivAbs = (double)GetValueFromName("NivAbs"), NivOrd = (double)GetValueFromName("NivOrd");
            double XBorneMax =(double)da.GetValueFromName("XBorneMax")+1, XBorneMin=(double)da.GetValueFromName("XBorneMin")-1;
            double YBorneMax =(double)da.GetValueFromName("YBorneMax")+1, YBorneMin=(double)da.GetValueFromName("YBorneMin")-1;
            
            int Xdeb = da.Rectangle.Left, Ydeb = da.Rectangle.Bottom;

            if (NivAbs > (double)da.GetValueFromName("XBorneMoy"))
            {
                XBorneMin = (double)da.GetValueFromName("XBorneMoy");
                Xdeb += da.Rectangle.Width / 2;
            }
            else XBorneMax = (double)da.GetValueFromName("XBorneMoy");
            if (NivOrd >= (double)da.GetValueFromName("YBorneMoy"))
            {
                YBorneMin = (double)da.GetValueFromName("YBorneMoy");
                Ydeb -= da.Rectangle.Height / 2;
            }
            else YBorneMax = (double)da.GetValueFromName("YBorneMoy");
            double TangX = (da.Rectangle.Width / 2 - Espace) / (XBorneMax - XBorneMin);
            double TangY = (da.Rectangle.Height / 2 - Espace) / (YBorneMax - YBorneMin);


            int x, y, xLigne;
            x = Espace / 2 + (int)(TangX * NivAbs + Xdeb - XBorneMin * TangX);
            y = (int)(Ydeb + YBorneMin * TangY - TangY * NivOrd) - Espace / 2;

            DrawGrpTxt(g, 3, 0, r.Left + Axe + HEIGHTPTNIVEAU, r.Top + r.Height / 2 - 3 * HeightFont[2] / 4, 0, Color.Black, Color.Transparent);
            AffRec(g, new Rectangle(x, y, 3, 3), LineCouleur, LineWidth, Couleur, 5, true, false, false);

            int Deb = 1;

            if (x > r.Right) { xLigne = r.Right; Deb = -HEIGHTPTNIVEAU; } else xLigne = r.Left;
            g.DrawLine(new Pen(Color.LightGray), xLigne, r.Top, xLigne, r.Bottom);
            g.DrawLine(new Pen(Color.LightGray), x, y, xLigne, r.Top + r.Height / 2);
            
            if ((double)GetValueFromName("IconStatusX") != 0)
            {
                double[] Niv = new double[Niveau.NbrNivCriticite];
                VirtualNiveau vn = new VirtualNiveau();
                vn.ExtractNiv((double)GetValueFromName("IconStatusX"), Niv);

                for (int i = 0; i < Niv.Length; i++)
                {
                    Color c = Color.Azure;
                    switch (i)
                    {
                        case 0: c = Color.DodgerBlue; break;
                        case 1: c = Color.LightGreen; break;
                        case 2: c = Color.Orange; break;
                        case 3: c = Color.Red; break;
                    }
                    g.FillRectangle(new System.Drawing.SolidBrush(c), (float)(xLigne + Deb + i * (3 + 2)), (float)(r.Bottom + 2 - Niv[i] * 2), (float)3, (float)Niv[i] * 2 + 1);
                }
            }
            if ((double)GetValueFromName("IconStatusY") != 0)
            {
                double[] Niv = new double[Niveau.NbrNivCriticite];
                VirtualNiveau vn = new VirtualNiveau();
                vn.ExtractNiv((double)GetValueFromName("IconStatusY"), Niv);
               
                for (int i = 0; i < Niv.Length; i++)
                {
                    Color c = Color.Azure;
                    switch (i)
                    {
                        case 0: c = Color.DodgerBlue; break;
                        case 1: c = Color.LightGreen; break;
                        case 2: c = Color.Orange; break;
                        case 3: c=Color.Red; break;
                    }
                    g.FillRectangle(new System.Drawing.SolidBrush(c), (float)(xLigne + Deb + i * (3 + 2)), (float)(r.Bottom + 2 - Niv[i] * 2), (float)3, (float)Niv[i] * 2 + 1);
                }
            }


            pen.Dispose();
        }

        /// <summary>
        /// Vérifie si l'objet à déplacer peut l'être
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override int MovePossible(DrawObject o)
        {
            return 0;
        }

        /// <summary>
        /// Move handle to new point (resizing)
        /// </summary>
        /// <param name="point"></param>
        /// <param name="handleNumber"></param>
        public override void MoveHandleTo(Point point, int handleNumber)
        {
            int left = Rectangle.Left;
            int top = Rectangle.Top;
            int right = Rectangle.Right;
            int bottom = Rectangle.Bottom;

            switch (handleNumber)
            {
                case 1:
                    left = point.X;
                    top = point.Y;
                    break;
                case 2:
                    top = point.Y;
                    break;
                case 3:
                    right = point.X;
                    top = point.Y;
                    break;
                case 4:
                    right = point.X;
                    break;
                case 5:
                    right = point.X;
                    bottom = point.Y;
                    break;
                case 6:
                    bottom = point.Y;
                    break;
                case 7:
                    left = point.X;
                    bottom = point.Y;
                    break;
                case 8:
                    left = point.X;
                    break;
            }

            SetRectangle(left, top, right - left, bottom - top);
            AligneObjet();
        }

        public override void savetoDB()
        {
            if (!savetoDBFait())
            {
                savetoDBOK();
            }
        }
    }
}
