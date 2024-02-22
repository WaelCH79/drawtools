using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using MOI = Microsoft.Office.Interop;
 

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawLink3D : DrawTools.DrawRectangle
	{
        ArrayList lstServerPhy1, lstServerPhy2;
        int idxConnector1, idxConnector2, iNbrConnector1, iNbrConnector2;
        Point f1begin, f1end, f1attach;
        Point f2begin, f2end, f2attach;
        Point pt3middle, pt4middle;
        int idxLink;
        int yRef; // Coordonnée (lorsque x=0) du segment middle de chaque link. 
        //Color colorLink;

        public int YREF { get { return yRef; } }
        public int IDX { get { return idxLink; } set { idxLink = value; } }

        public int NbrLink3D()
        {
            int CountObj = 0;

            for (int i = 0; i < F.drawArea.GraphicsList.Count; i++)
                if (F.drawArea.GraphicsList[i].GetType() == typeof(DrawLink3D)) CountObj++;
            return CountObj;
        }

        public DrawLink3D(Form1 of, string sId, string sNom, ArrayList lstSrvPhy1, int idxCnx1, int iNbrCnx1, ArrayList lstSrvPhy2, int idxCnx2, int iNbrCnx2)
        {
            F = of;
            object o = null;
            OkMove = false;
            LstParent = null;
            LstChild = null;
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = new ArrayList();
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
            LstValue.Add(GuidkeyObjet.ToString()); LstValue.Add(sId); LstValue.Add(sNom);
            lstServerPhy1 = lstSrvPhy1; idxConnector1 = idxCnx1; iNbrConnector1 = iNbrCnx1;
            lstServerPhy2 = lstSrvPhy2; idxConnector2 = idxCnx2; iNbrConnector2 = iNbrCnx2;

            o = GetValueFromLib("Nom");
            if (o != null)
                Texte = (string)o;



            //Initialize();
            IDX = NbrLink3D();
            Random randomGen = new Random();
            System.Threading.Thread.Sleep(1);
            Color = Color.FromArgb(randomGen.Next(255), randomGen.Next(255), randomGen.Next(255));
            //Color = F.colors[randomGen.Next(0, F.colors.Length)];
            CalcPointsLink();
        }


        public void CalcPointsLink()
        {
            int idxLigne1, idxLigne2;   // si idxLigne1 > idxLigne2 le départ du link (1) est situé en dessous du serveur,
                                        // si idxLigne1 < idxLign2 le départ du link est situé au dessus du serveur
                                        // si ideLigne1 = idxLigne2 le départ et l'arrivée est en-dessous
            Point pt1; //= new Point(); //pt1.X = xMaxA4paysage; pt1.Y = yMaxA4paysage;
            Point pt2;// = new Point(); //pt2.X = xMaxA4paysage; pt2.Y = yMaxA4paysage;


            InitRectangle(out pt1, out idxLigne1, out pt2, out idxLigne2);
            f1begin.X = 0; f1end.X = 0; f1begin.Y = 0; f1end.Y = 0;
            f2begin.X = 0; f2end.X = 0; f2begin.Y = 0; f2end.Y = 0;
            f1attach.X = 0; f1attach.Y = 0; f2attach.X = 0; f2attach.Y = 0;
            if (idxLigne1 > idxLigne2) // fx1 sup & fx2 inf
            {
                // si supérieur décalage de 20
                f1begin.X = (int)(pt1.X + 20 * Math.Cos(radianProfondeur));
                f1begin.Y = (int)(pt1.Y - 20 * Math.Sin(radianProfondeur));
                f1end.X = (int)(f1begin.X + (lozangeFace * (lstServerPhy1.Count - 1) + (lozangeFace - 5)) * Math.Cos(radianFace));
                f1end.Y = (int)(f1begin.Y + (lozangeFace * (lstServerPhy1.Count - 1) + (lozangeFace - 5)) * Math.Sin(radianFace));


                //pour des lignes suplémentaire incrément de 8
                //fx1 = (float)(r.X + 28 * Math.Cos(radianProfondeur));
                //fy1 = (float)(r.Y - 28 * Math.Sin(radianProfondeur));

                // si inférieur translation de 50 + décalage de 45
                f2begin.X = (int)(pt2.X - 45 * Math.Cos(radianProfondeur));
                f2begin.Y = (int)(pt2.Y + 50 + 45 * Math.Sin(radianProfondeur));
                f2end.X = (int)(f2begin.X + (lozangeFace * (lstServerPhy2.Count - 1) + (lozangeFace - 5)) * Math.Cos(radianFace));
                f2end.Y = (int)(f2begin.Y + (lozangeFace * (lstServerPhy2.Count - 1) + (lozangeFace - 5)) * Math.Sin(radianFace));

                //pour des lignes suplémentaire incrément de 8
                //fx1 = (float)(r.X - 53 * Math.Cos(radianProfondeur));
                //fy1 = (float)(r.Y + 50 + 53 * Math.Sin(radianProfondeur));

            }
            else if (idxLigne1 < idxLigne2) // fx1 inf & fx2 sup
            {
                f1begin.X = (int)(pt1.X - 45 * Math.Cos(radianProfondeur));
                f1begin.Y = (int)(pt1.Y + 50 + 45 * Math.Sin(radianProfondeur));
                f1end.X = (int)(f1begin.X + (lozangeFace * (lstServerPhy1.Count - 1) + (lozangeFace - 5)) * Math.Cos(radianFace));
                f1end.Y = (int)(f1begin.Y + (lozangeFace * (lstServerPhy1.Count - 1) + (lozangeFace - 5)) * Math.Sin(radianFace));

                f2begin.X = (int)(pt2.X + 20 * Math.Cos(radianProfondeur));
                f2begin.Y = (int)(pt2.Y - 20 * Math.Sin(radianProfondeur));
                f2end.X = (int)(f2begin.X + (lozangeFace * (lstServerPhy2.Count - 1) + (lozangeFace - 5)) * Math.Cos(radianFace));
                f2end.Y = (int)(f2begin.Y + (lozangeFace * (lstServerPhy2.Count - 1) + (lozangeFace - 5)) * Math.Sin(radianFace));

            }
            else // fx1 sup & fx2 sup
            {
                f1begin.X = (int)(pt1.X + 20 * Math.Cos(radianProfondeur));
                f1begin.Y = (int)(pt1.Y - 20 * Math.Sin(radianProfondeur));
                f1end.X = (int)(f1begin.X + (lozangeFace * (lstServerPhy1.Count - 1) + (lozangeFace - 5)) * Math.Cos(radianFace));
                f1end.Y = (int)(f1begin.Y + (lozangeFace * (lstServerPhy1.Count - 1) + (lozangeFace - 5)) * Math.Sin(radianFace));

                f2begin.X = (int)(pt2.X + 20 * Math.Cos(radianProfondeur));
                f2begin.Y = (int)(pt2.Y - 20 * Math.Sin(radianProfondeur));
                f2end.X = (int)(f2begin.X + (lozangeFace * (lstServerPhy2.Count - 1) + (lozangeFace - 5)) * Math.Cos(radianFace));
                f2end.Y = (int)(f2begin.Y + (lozangeFace * (lstServerPhy2.Count - 1) + (lozangeFace - 5)) * Math.Sin(radianFace));

            }

            f1attach.X = f1begin.X + (f1end.X - f1begin.X) * (1 + idxConnector1) / (1 + iNbrConnector1);
            f1attach.Y = f1begin.Y + (f1end.Y - f1begin.Y) * (1 + idxConnector1) / (1 + iNbrConnector1);
            f2attach.X = f2begin.X + (f2end.X - f2begin.X) * (1 + idxConnector2) / (1 + iNbrConnector2);
            f2attach.Y = f2begin.Y + (f2end.Y - f2begin.Y) * (1 + idxConnector2) / (1 + iNbrConnector2);

            if (idxLigne1 == idxLigne2)
            {
                pt3middle.X = (int)(f2attach.X + 8 * Math.Cos(radianProfondeur));
                pt3middle.Y = (int)(f2attach.Y - 8 * Math.Sin(radianProfondeur));
                pt4middle.X = (int)(f1attach.X + 8 * Math.Cos(radianProfondeur));
                pt4middle.Y = (int)(f1attach.Y - 8 * Math.Sin(radianProfondeur));
            }
            else {
                Point pt3 = new Point(), pt4 = new Point();
                pt3.X = (int)((f2attach.Y - f1attach.Y + f1attach.X * Math.Tan(radianFace) + f2attach.X * Math.Tan(radianProfondeur)) / (Math.Tan(radianFace) + Math.Tan(radianProfondeur)));
                pt3.Y = (int)(f1attach.Y + (pt3.X - f1attach.X) * Math.Tan(radianFace));
                pt4.X = (int)((f1attach.Y - f2attach.Y + f1attach.X * Math.Tan(radianProfondeur) + f2attach.X * Math.Tan(radianFace)) / (Math.Tan(radianFace) + Math.Tan(radianProfondeur)));
                pt4.Y = (int)(f1attach.Y + (f1attach.X - pt4.X) * Math.Tan(radianProfondeur));

                pt4middle.X = (f1attach.X + pt4.X) / 2;
                pt4middle.Y = (f1attach.Y + pt4.Y) / 2;

                pt3middle.X = (f2attach.X + pt3.X) / 2;
                pt3middle.Y = (f2attach.Y + pt3.Y) / 2;
            }

            // calcul du yRef
            yRef = (pt4middle.X * (pt4middle.Y - pt3middle.Y) + pt4middle.Y * (pt3middle.X - pt4middle.X)) / (pt3middle.X - pt4middle.X);
            for (int i=0; i<F.drawArea.GraphicsList.Count; i++)
            {
                if(F.drawArea.GraphicsList[i].GetType() == typeof(DrawLink3D))
                {
                    DrawLink3D dl3d = (DrawLink3D)F.drawArea.GraphicsList[i];
                    if(yRef==dl3d.YREF)
                    {
                        pt3middle.X += (int) (8 * Math.Cos(radianProfondeur));
                        pt3middle.Y -= (int) (8 * Math.Sin(radianProfondeur));
                        pt4middle.X += (int)(8 * Math.Cos(radianProfondeur));
                        pt4middle.Y -= (int)(8 * Math.Sin(radianProfondeur));
                        yRef = (pt4middle.X * (pt4middle.Y - pt3middle.Y) + pt4middle.Y * (pt3middle.X - pt4middle.X)) / (pt3middle.X - pt4middle.X);
                        i = -1;
                    }
                }
            }

            if (pt3middle.X < 0)
            {
                Point pt3middletemp = new Point();
                pt3middletemp.X = 1;
                pt3middletemp.Y = (int)(f2attach.Y + (f2attach.X - pt3middletemp.X) * Math.Tan(radianProfondeur));
                int deltax = pt3middletemp.X - pt3middle.X;
                int deltay = pt3middletemp.Y - pt3middle.Y;
                pt3middle.X += deltax;
                pt3middle.Y += deltay;
                pt4middle.X += deltax;
                pt4middle.Y += deltay;
            }
            if (pt4middle.X < 0)
            {
                Point pt4middletemp = new Point();
                pt4middletemp.X = 1;
                pt4middletemp.Y = (int)(f1attach.Y + (f1attach.X - pt4middletemp.X) * Math.Tan(radianProfondeur));
                int deltax = pt4middletemp.X - pt4middle.X;
                int deltay = pt4middletemp.Y - pt4middle.Y;
                pt3middle.X += deltax;
                pt3middle.Y += deltay;
                pt4middle.X += deltax;
                pt4middle.Y += deltay;
            }
        }
        public void InitRectangle(out Point pt1, out int idxLigne1, out Point pt2, out int idxLigne2)
        {
            idxLigne1 = 0; idxLigne2 = 0;
            pt1 = new Point(); pt1.X = xMaxA4paysage; pt1.Y = yMaxA4paysage;
            pt2 = new Point(); pt2.X = xMaxA4paysage; pt2.Y = yMaxA4paysage;
            for (int i = 0; i < lstServerPhy1.Count; i++)
            {
                int idxObj = F.drawArea.GraphicsList.FindObjet(0, (string)lstServerPhy1[i]);
                if (idxObj > -1)
                {
                    DrawServer3D dServerPhy = (DrawServer3D)F.drawArea.GraphicsList[idxObj];
                    if (dServerPhy.Rectangle.X < pt1.X)
                    {
                        pt1.X = dServerPhy.rectangle.X;
                        pt1.Y = dServerPhy.rectangle.Y;
                        idxLigne1 = Convert.ToInt32((string) dServerPhy.GetValueFromName("IdxLigne"));
                    }
                }
            }
            for (int i = 0; i < lstServerPhy2.Count; i++)
            {
                int idxObj = F.drawArea.GraphicsList.FindObjet(0, (string)lstServerPhy2[i]);
                if (idxObj > -1)
                {
                    DrawServer3D dServerPhy = (DrawServer3D)F.drawArea.GraphicsList[idxObj];
                    if (dServerPhy.Rectangle.X < pt2.X)
                    {
                        pt2.X = dServerPhy.rectangle.X;
                        pt2.Y = dServerPhy.rectangle.Y;
                        idxLigne2 = Convert.ToInt32((string)dServerPhy.GetValueFromName("IdxLigne"));
                    }
                }
            }
            if (pt1.X < pt2.X)
            {
                if (pt1.Y < pt2.Y)
                {
                    rectangle.X = pt1.X;
                    rectangle.Y = pt1.Y;
                }
                else
                {
                    rectangle.X = pt1.X;
                    rectangle.Y = pt2.Y;
                }
            }
            else
            {
                if (pt1.Y < pt2.Y)
                {
                    rectangle.X = pt2.X;
                    rectangle.Y = pt1.Y;
                }
                else
                {
                    rectangle.X = pt2.X;
                    rectangle.Y = pt2.Y;
                }
            }
            rectangle.Width = Math.Abs(pt1.X - pt2.X);
            rectangle.Height = Math.Abs(pt1.Y - pt2.Y);
        }

        public override void Draw(Graphics g)
        {
            Rectangle r = rectangle;


            Pen pen = new Pen(Color, 2);
            SolidBrush brsh = new SolidBrush(Color);

            g.DrawLine(pen, 10, 20 * (IDX + 1), 40, 20 * (IDX + 1));
            g.DrawString((string)LstValue[1] + " - " + (string)LstValue[2], new Font("Arial", 8), brsh, (int)60, (int)20 * (IDX + 1)-8);
            //DrawGrpTxt(g, 1, 0, 50 , 20 * IDX, 0, Color, Color.White);

            g.DrawLine(pen, f1begin.X, f1begin.Y, f1end.X, f1end.Y);
            g.DrawLine(pen, f2begin.X, f2begin.Y, f2end.X, f2end.Y);

            g.DrawLine(pen, f1attach.X, f1attach.Y, pt4middle.X, pt4middle.Y);
            g.DrawLine(pen, f2attach.X, f2attach.Y, pt3middle.X, pt3middle.Y);
            g.DrawLine(pen, pt3middle.X, pt3middle.Y, pt4middle.X, pt4middle.Y);

            SolidBrush bkgrbrsh = new SolidBrush(Color.White);
            Font nameFont = new Font("Arial", 8);
            SizeF stringSize = new SizeF();
            stringSize = g.MeasureString((string)LstValue[1], nameFont);
            g.FillRectangle(bkgrbrsh, (float)(pt3middle.X + pt4middle.X) / 2, (float)(pt3middle.Y + pt4middle.Y) / 2, stringSize.Width, stringSize.Height);
            g.DrawString((string)LstValue[1], nameFont, brsh, (float)(pt3middle.X + pt4middle.X) / 2, (float)(pt3middle.Y + pt4middle.Y) / 2);

           







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
        }
	}
}
