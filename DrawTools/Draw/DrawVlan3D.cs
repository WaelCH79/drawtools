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
	public class DrawVlan3D : DrawTools.DrawRectangle
	{
        public DrawVlan3D(Form1 of, string sNom, int x, int y, int w, int h)
        {
            F = of;
            object o= null;
            OkMove = false;
            LstParent = null;
            LstChild = null;
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = new ArrayList();
            Guidkey = Guid.NewGuid();
            LstValue.Add(Guid.NewGuid().ToString()); LstValue.Add(sNom);
            o = GetValueFromLib("Guid");
            if(o!=null)
                GuidkeyObjet = new Guid((string) o);
            
            o = GetValueFromLib("Nom");
            if (o != null)
                Texte = (string)o;
            
            Rectangle = new Rectangle(x,y,w,h);
            
            Initialize();
        }
                
        public override void Draw(Graphics g)
        {
            /*
            GraphicsPath gp = null;
            Pen linepen = new Pen(Color.Black, 1);
            LinearGradientMode mode = LinearGradientMode.BackwardDiagonal;
            int NbrColor = 2;

            Rectangle r = rectangle;
            gp = new GraphicsPath();
            gp.AddLine(r.X + radius / 2, r.Y, r.X + r.Width, r.Y + r.Height - radius / 2);
            gp.AddArc(r.X + r.Width - radius, r.Y + r.Height - radius, radius, radius, 0, 90);
            gp.AddLine(r.X + r.Width - radius / 2, r.Y + r.Height, r.X, r.Y + radius / 2);
            gp.AddArc(r.X, r.Y, radius, radius, 180, 90);

            PaintBackAeroGlass(g, r, NbrColor, Color.Orange, mode, gp);
            */
            ToolVLan to = (ToolVLan)F.drawArea.tools[(int)DrawArea.DrawToolType.VLan];
            Rectangle r = rectangle;

            g.TranslateTransform(r.X, r.Y);
            g.RotateTransform(23);
            r.X = 0; r.Y = 0;
            AffRec(g, r, to, Color.White, Color.Orange);
            DrawGrpTxt(g, 2, 0, r.Left + Axe, r.Top, 0, to.Pen1Couleur, to.BkGrCouleur);
            g.ResetTransform();
            //AffRec(g, new Rectangle(r.X, r.Y, radius, r.Height), to, Color.Orange, Color.Orange);

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
