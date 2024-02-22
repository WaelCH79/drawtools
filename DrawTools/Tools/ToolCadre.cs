#define WRITE //Form1 & ToolPointer
using System;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;


namespace DrawTools
{
	/// <summary>
	/// Pointer tool
	/// </summary>
	public class ToolCadre : DrawTools.ToolObject
    {
        private enum SelectionMode
        {
            None,
            NetSelection,   // group selection is active
            Move,           // object(s) are moves
            Size            // object is resized
        }

        private SelectionMode selectMode = SelectionMode.None;


        // Keep state about last and current point (used to move and resize objects)
        private Point lastPoint = new Point(0,0);
        private Point startPoint = new Point(0, 0);

        
		public ToolCadre(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "Rectangle.cur");
        }

        /// <summary>
        /// Left mouse button is pressed
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point point = new Point(e.X, e.Y);
            selectMode = SelectionMode.None;
                        

            // Net selection
            if ( selectMode == SelectionMode.None )
            {
                // click on background
                if ( ( Control.ModifierKeys & Keys.Control ) == 0 )
                    drawArea.GraphicsList.UnselectAll();

                selectMode = SelectionMode.NetSelection;
                drawArea.DrawNetRectangle = true;
            }

            lastPoint.X = e.X;
            lastPoint.Y = e.Y;
            startPoint.X = e.X;
            startPoint.Y = e.Y;

            drawArea.Capture = true;

            drawArea.NetRectangle = DrawRectangle.GetNormalizedRectangle(startPoint, lastPoint);

            drawArea.Refresh();
        }

               

        /// <summary>
        /// Mouse is moved.
        /// None button is pressed, ot left button is pressed.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
        {
            drawArea.Cursor = Cursor;
            Point point = new Point(e.X, e.Y);

            if ( e.Button != MouseButtons.Left )
                return;

            /// Left button is pressed
            
            // Find difference between previous and current position
            int dx = e.X - lastPoint.X;
            int dy = e.Y - lastPoint.Y;

            lastPoint.X = e.X;
            lastPoint.Y = e.Y;
          
            if ( selectMode == SelectionMode.NetSelection )
            {
                drawArea.NetRectangle = DrawRectangle.GetNormalizedRectangle(startPoint, lastPoint);
                drawArea.MajObjets();
                return;
            }
        }

        /// <summary>
        /// Right mouse button is released
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            
            selectMode = SelectionMode.None;
            int h = drawArea.NetRectangle.Height, w = drawArea.NetRectangle.Width;
            if(Owner.Owner.oCnxBase.CBRecherche("Select Vue.GuidVue, GuidGVue, Xmax, Ymax From Pattern, Vue WHERE Pattern.GuidVue=Vue.GuidVue and GuidPattern='" + Owner.Owner.tvObjet.SelectedNode.Name + "'"))
            {
                Point pTranslate = new Point();
                pTranslate.X = drawArea.NetRectangle.X; pTranslate.Y = drawArea.NetRectangle.Y;
                double xratio = (double)drawArea.NetRectangle.Width / (double)Owner.Owner.oCnxBase.Reader.GetInt32(2);
                double yratio = (double)drawArea.NetRectangle.Height / (double)Owner.Owner.oCnxBase.Reader.GetInt32(3);
                string sguidvue = Owner.Owner.oCnxBase.Reader.GetString(0), sguidgvue = Owner.Owner.oCnxBase.Reader.GetString(1);
                Owner.Owner.oCnxBase.CBReaderClose();
                Owner.Owner.LoadVue(sguidvue, sguidgvue, pTranslate, xratio, yratio);
            }
            Owner.Owner.oCnxBase.CBReaderClose();

            //Owner.Owner.LoadVue()

            drawArea.DrawNetRectangle = false;
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }
	}
}
