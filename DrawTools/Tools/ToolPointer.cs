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
	public class ToolPointer : DrawTools.Tool
	{
        private enum SelectionMode
        {
            None,
            NetSelection,   // group selection is active
            Move,           // object(s) are moves
            Size            // object is resized
        }

        private SelectionMode selectMode = SelectionMode.None;

        // Object which is currently resized:
        private DrawObject resizedObject;
        private int resizedObjectHandle;
        private ControlWord cw;

        public ToolTip tooltip;
        //public System.Windows.Forms.RichTextBox rtbText;
        public System.Windows.Forms.Button button;
        public DrawObject otooltip;

        // Keep state about last and current point (used to move and resize objects)
        private Point lastPoint = new Point(0,0);
        private Point startPoint = new Point(0, 0);

        public int RESIZEOBJECTHANDLE
        {
            get
            {
                return resizedObjectHandle;
            }
            set
            {
                resizedObjectHandle = value;
            }
        }

		public ToolPointer(DrawArea da)
		{
            Owner = da;
            tooltip = new ToolTip();
            button = new Button();
            otooltip = null;
            cw = null;
            
		}

        /// <summary>
        /// Left mouse button is pressed
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            InitToolTip();
            Point point = new Point(e.X, e.Y);
            selectMode = SelectionMode.None;


            // Test for resizing (only if control is selected, cursor is on the handle)
            int n = drawArea.GraphicsList.SelectionCount;

            for (int i = 0; i < n; i++)
            {
                DrawObject o = drawArea.GraphicsList.GetSelectedObject(i);

                int handleNumber = o.HitTest(point);

                if (handleNumber > 0)
                {
                    selectMode = SelectionMode.Size;

                    // keep resized object in class members
                    resizedObject = o;
                    resizedObjectHandle = handleNumber;

                    // Since we want to resize only one object, unselect all other objects
                    drawArea.GraphicsList.UnselectAll();

                    o.Selected = true;
                    resizedObjectHandle = o.HandleEvent(point, resizedObjectHandle);

                    break;
                }
            }

            
            // Test for move (cursor is on the object)
            if ( selectMode == SelectionMode.None )
            {
                int n1 = drawArea.GraphicsList.Count;
                DrawObject o = null;

                for ( int i = 0; i < n1; i++ )
                {
                    if ( drawArea.GraphicsList[i].HitTest(point) == 0 )
                    {
                        o = drawArea.GraphicsList[i];
                        break;
                    }
                }

                if ( o != null )
                {
                    selectMode = SelectionMode.Move;

                    // Unselect all if Ctrl is not pressed and clicked object is not selected yet
                    if ( ( Control.ModifierKeys & Keys.Control ) == 0  && !o.Selected )
                        drawArea.GraphicsList.UnselectAll();

                    // Select clicked object
                    o.Selected = true;
                    
                    if(o.GetType() == typeof(DrawTechLink))
                    {
                        o.bover = true;
                        List<string> lstTechLink = Owner.Owner.oCnxBase.getListTechLinkFromLink(o.GuidkeyObjet.ToString(), Owner.Owner.GuidVue.ToString());
                        for (int i = 0; i < lstTechLink.Count; i++)
                        {
                            int objIndex = drawArea.GraphicsList.FindObjet(0, lstTechLink[i]);
                            if(objIndex > -1)
                            {
                                DrawObject obj = drawArea.GraphicsList[objIndex];
                                obj.bover = true;
                            }
                        }
                    }

                    if ((Control.ModifierKeys & Keys.Control) != 0 && n > 0)
                    {
                    }
                    else
                    {

                        //o.InitDatagrid(drawArea.Owner);
                        o.InitDatagrid(Owner.Owner.dataGrid);
                        //InitDatagrid(Owner.Owner.dataGrid, o.LstValue, o.GetTypeSimpleTable());
                        drawArea.OSelected = o;
                        /*strin[] row1 = {"Nom",};
                        string[] row2 = {"Version", "" };
                        string[] row3 = {"Editeur", "" };
                        drawArea.Owner.dataGrid.Rows.Add(row1);
                        drawArea.Owner.dataGrid.Rows.Add(row2);
                        drawArea.Owner.dataGrid.Rows.Add(row3);*/

                        //MessageBox.Show("-"+drawArea.Owner.dataGrid.Rows[0].Cells[1].Value+"-");
                    }
                    drawArea.Cursor = Cursors.SizeAll;

                }
            }

            // Net selection
            if ( selectMode == SelectionMode.None )
            {
                Owner.GraphicsList.UnselectAll();
                // click on background

                /*
                if ( ( Control.ModifierKeys & Keys.Control ) == 0 )
                    drawArea.GraphicsList.UnselectAll();

                */
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

        public override void InitToolTip()
        {
            tooltip.RemoveAll();
            button.Size = new System.Drawing.Size(75, 23);
            button.Text = "Test";
            if (otooltip != null) otooltip.ToolTip = false;
            otooltip = null;
            if (cw != null) cw.Close();

        }

        public string FormatCol(string s)
        {
            string sFormatee = "";
            int j = 0, maxCar = 100;

            for (int i = 0; i < s.Length; i++,j++)
            {
                if (s[i] == '\r') j = 0;
                if (j >= maxCar && s[i]==' ') { sFormatee += "\r"; j = 0; }
                else sFormatee += s[i];
            }
            return sFormatee;
        }


        /// <summary>
        /// Mouse is moved.
        /// None button is pressed, ot left button is pressed.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
        {
            Point point = new Point(e.X, e.Y);

            // set cursor when mouse button is not pressed
            if ( e.Button == MouseButtons.None )
            {
                Cursor cursor = null;
                DrawObject o;
                bool tooltipactive = false;


                for ( int i = 0; i < drawArea.GraphicsList.Count; i++ )
                {
                    o = drawArea.GraphicsList[i];
                    int n = o.HitTest(point);

                    if ( n > 0 )
                    {
                        cursor = o.GetHandleCursor(n);
                        break;
                    }
                    if (o.PointInObject(point) && Owner.Owner.wkApp.TadFile!=null)
                    {
                        if (Owner.Owner.bActiveToolTip && !o.ToolTip && otooltip != o && Owner.Owner.oCnxBase.CBRecherche("Select NomProp, HyperLien, Size, RichText From Comment Where GuidObject = '" + o.GetKeyComment() + "' order by Id"))
                        {
                            InitToolTip();
                            otooltip = o;
                            tooltip.ToolTipTitle = o.Texte;
                            //tooltip.IsBalloon = true;
                            tooltip.ShowAlways = true;
                            string sTexte = "";
                            while (Owner.Owner.oCnxBase.Reader.Read())
                            {
                                int nByte = Owner.Owner.oCnxBase.Reader.GetInt32(2);
                                if (nByte > 0)
                                {
                                    byte[] rawData = new byte[nByte];
                                    Owner.Owner.oCnxBase.Reader.GetBytes(3, 0, rawData, 0, nByte);
                                    System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
                                    rtBox.Rtf = System.Text.Encoding.UTF8.GetString(rawData);
                                    sTexte += rtBox.Text + "\n";
                                }
                            }
                            //IWin32Window
                            tooltip.SetToolTip(button, "?");
                            tooltip.Show(sTexte, Owner, point);
                            o.ToolTip = true;
                        }
                        Owner.Owner.oCnxBase.CBReaderClose();
                        tooltipactive = true;
                        break;
                    }
                    //drawArea.Refresh();
                }

                if ( cursor == null )
                    cursor = Cursors.Default;
                drawArea.Cursor = cursor;

                if (!tooltipactive & otooltip != null) InitToolTip();

                return;
            }

            if ( e.Button != MouseButtons.Left )
                return;

            /// Left button is pressed
#if WRITE

            
            // Find difference between previous and current position
            int dx = e.X - lastPoint.X;
            int dy = e.Y - lastPoint.Y;

            lastPoint.X = e.X;
            lastPoint.Y = e.Y;

            // resize
            if ( selectMode == SelectionMode.Size )
            {
                if ( resizedObject != null )
                {
                    resizedObject.MoveHandleTo(point, resizedObjectHandle);
                    drawArea.MajObjets();
                }
            }

            // move
            if ( selectMode == SelectionMode.Move )
            {
                int n = drawArea.GraphicsList.SelectionCount;

                for ( int i = 0; i < n; i++ )
                {
                    drawArea.GraphicsList.GetSelectedObject(i).Move(dx, dy);
                }

                drawArea.Cursor = Cursors.SizeAll;
                drawArea.MajObjets();
            }

            if ( selectMode == SelectionMode.NetSelection )
            {
                drawArea.NetRectangle = DrawRectangle.GetNormalizedRectangle(startPoint, lastPoint);
                drawArea.MajObjets();
                return;
            }
#endif
        }

        /// <summary>
        /// Right mouse button is released
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            if ( selectMode == SelectionMode.NetSelection )
            {
                // Group selection
                drawArea.GraphicsList.SelectInRectangle(drawArea.NetRectangle);

                selectMode = SelectionMode.None;
                drawArea.DrawNetRectangle = false;
            }

            if ( resizedObject != null )
            {
                // after resizing
                if (resizedObject.GetType() == typeof(DrawLink))
                {
                    Point point = new Point(e.X, e.Y);

                    if (resizedObjectHandle == 1)
                    {
                        for (int j = 0; j < drawArea.GraphicsList.Count; j++)
                        {
                            if (drawArea.GraphicsList[j].AttachPointInObject(point))
                            {
                                resizedObject.ClearAttach(DrawObject.TypeAttach.Entree);
                                drawArea.GraphicsList[j].AttachLink(resizedObject, DrawObject.TypeAttach.Sortie);
                                resizedObject.AttachLink(drawArea.GraphicsList[j], DrawObject.TypeAttach.Entree);
                                resizedObject.CompleteLink(DrawObject.TypeAttach.Entree);
                                
                                break;
                            }
                        }
                    }
                    else if (resizedObjectHandle == resizedObject.HandleCount)
                    {
                        for (int j = 0; j < drawArea.GraphicsList.Count; j++)
                        {
                            if (drawArea.GraphicsList[j].AttachPointInObject(point))
                            {
                                resizedObject.ClearAttach(DrawObject.TypeAttach.Sortie);
                                drawArea.GraphicsList[j].AttachLink(resizedObject, DrawObject.TypeAttach.Entree);
                                resizedObject.AttachLink(drawArea.GraphicsList[j], DrawObject.TypeAttach.Sortie);
                                resizedObject.CompleteLink(DrawObject.TypeAttach.Sortie);
                                
                                break;
                            }
                        }
                    }
                }
                else if (resizedObject.GetType() == typeof(DrawTechLink) || resizedObject.GetType() == typeof(DrawInterLink))
                {
                    Point point = new Point(e.X, e.Y);

                    if (resizedObjectHandle == 1)
                    {
                        for (int j = 0; j < drawArea.GraphicsList.Count; j++)
                        {
                            if (drawArea.GraphicsList[j].AttachPointInObject(point))
                            {
                                resizedObject.ClearAttach(DrawObject.TypeAttach.Entree);
                                drawArea.GraphicsList[j].AttachLink(resizedObject, DrawObject.TypeAttach.Sortie);
                                resizedObject.AttachLink(drawArea.GraphicsList[j], DrawObject.TypeAttach.Entree);
                                resizedObject.CompleteLink(DrawObject.TypeAttach.Entree);

                                break;
                            }
                        }
                    }
                    else if (resizedObjectHandle == resizedObject.HandleCount)
                    {
                        for (int j = 0; j < drawArea.GraphicsList.Count; j++)
                        {
                            if (drawArea.GraphicsList[j].AttachPointInObject(point))
                            {
                                resizedObject.ClearAttach(DrawObject.TypeAttach.Sortie);
                                drawArea.GraphicsList[j].AttachLink(resizedObject, DrawObject.TypeAttach.Entree);
                                resizedObject.AttachLink(drawArea.GraphicsList[j], DrawObject.TypeAttach.Sortie);
                                resizedObject.CompleteLink(DrawObject.TypeAttach.Sortie);
                                break;
                            }
                        }
                    }
                }
                else if (resizedObject.GetType() == typeof(DrawVLan))
                {
                    Point point = new Point(e.X, e.Y);
                    
                    if (resizedObjectHandle > 9)
                    {
                        DrawVLan dvl = (DrawVLan)resizedObject;
                        int indexPoint = resizedObjectHandle - 10;

                        //clear le link NCard
                        if (indexPoint < dvl.LstLinkOut.Count)
                        {
                            DrawObject oOld = (DrawObject)dvl.LstLinkOut[indexPoint];

                            oOld.ClearAttach(dvl, DrawObject.TypeAttach.Entree, DrawObject.TypeAttach.Sortie);
                            Point pt = (Point)dvl.pointArray[indexPoint];
                            dvl.pointArray.RemoveAt(indexPoint); // dans la fonction clearAttach
                            dvl.pointArray.Add(pt);
                        }

                        // creat link
                        bool bAttachLink = false;
                        for (int i = 0; i < drawArea.GraphicsList.Count; i++)
                        {
                            if (drawArea.GraphicsList[i].GetType() == typeof(DrawNCard) && drawArea.GraphicsList[i].AttachPointInObject(point))
                            {
                                bAttachLink = true;
                                DrawNCard dnc = (DrawNCard)drawArea.GraphicsList[i];
                                point = drawArea.GraphicsList[i].GetPointObject(point);

                                dnc.InitProp("GuidVlan", dvl.GuidkeyObjet.ToString(), true);
                                CreatObjetLink(dnc, "GuidVlan", DrawObject.TypeAttach.Entree, DrawObject.TypeAttach.Sortie);
                                break;
                            }
                            else if (drawArea.GraphicsList[i].GetType() == typeof(DrawRouter) && drawArea.GraphicsList[i].AttachPointInObject(point))
                            {
                                bAttachLink = true;
                                DrawRouter dr = (DrawRouter)drawArea.GraphicsList[i];

                                point = drawArea.GraphicsList[i].GetPointObject(point);
                                dr.AttachLink(resizedObject, DrawObject.TypeAttach.Entree);
                                resizedObject.AttachLink(dr, DrawObject.TypeAttach.Sortie);
                                //((DrawVLan)resizedObject).AttachHandle.Add(resizedObjectHandle);
                                break;
                            }
                        }
                        if (!bAttachLink && indexPoint < dvl.pointArray.Count)
                            dvl.pointArray.RemoveAt(resizedObjectHandle - 10);
                    }
                }
                else if (resizedObject.GetType() == typeof(DrawSanSwitch))
                {
                    Point point = new Point(e.X, e.Y);
                    for (int i = 0; i < drawArea.GraphicsList.Count; i++)
                    {
                        if (drawArea.GraphicsList[i].GetType() == typeof(DrawSanCard) && drawArea.GraphicsList[i].AttachPointInObject(point))
                        {
                            DrawSanCard dsc = (DrawSanCard)drawArea.GraphicsList[i];
                            object o;

                            point = drawArea.GraphicsList[i].GetPointObject(point);
                            o = dsc.GetValueFromName("GuidSanSwitch");
                            if ((string)o == "")
                            {
                                dsc.AttachLink(resizedObject, DrawObject.TypeAttach.Entree);
                                dsc.InitProp("GuidSanSwitch", (object)((DrawSanSwitch)dsc.LstLinkIn[0]).GuidkeyObjet.ToString(), true);
                                resizedObject.AttachLink(dsc, DrawObject.TypeAttach.Sortie);
                                dsc.InitProp("Port", (object)((DrawSanSwitch)resizedObject).AttachHandle.Count.ToString(), true);
                                ((DrawSanSwitch)resizedObject).AttachHandle.Add(resizedObjectHandle);
                            }
                            break;
                        }
                    }
                }
                else if (resizedObject.GetType() == typeof(DrawCnx))
                {
                    Point point = new Point(e.X, e.Y);
                    for (int i = 0; i < drawArea.GraphicsList.Count; i++)
                    {
                        if (drawArea.GraphicsList[i].GetType() == typeof(DrawPtCnx) && drawArea.GraphicsList[i].AttachPointInObject(point))
                        {
                            DrawPtCnx dpc = (DrawPtCnx)drawArea.GraphicsList[i];

                            point = drawArea.GraphicsList[i].GetPointObject(point);
                            dpc.AttachLink(resizedObject, DrawObject.TypeAttach.Entree);
                            dpc.InitProp("GuidCnx", (object)((DrawCnx)dpc.LstLinkIn[0]).GuidkeyObjet.ToString(), true);
                            resizedObject.AttachLink(dpc, DrawObject.TypeAttach.Sortie);
                            ((DrawCnx)resizedObject).AttachHandle.Add(resizedObjectHandle);
                            break;
                        }
                    }

                }
                else if (resizedObject.GetType() == typeof(DrawZone))
                {
                    Point point = new Point(e.X, e.Y);
                    for (int i = 0; i < drawArea.GraphicsList.Count; i++)
                    {
                        /*
                        if (drawArea.GraphicsList[i].GetType() == typeof(DrawMachine)&& drawArea.GraphicsList[i].PointInObject(point))
                        {
                            DrawMachine dm = (DrawMachine)drawArea.GraphicsList[i];

                            point = drawArea.GraphicsList[i].GetPointObject(point);
                            dm.AttachLink(resizedObject, DrawObject.TypeAttach.Entree);
                            dm.InitProp("GuidZone", (object)((DrawZone)dm.LstLinkIn[0]).GuidkeyObjet.ToString(), true);
                            resizedObject.AttachLink(dm, DrawObject.TypeAttach.Sortie);
                            ((DrawZone)resizedObject).AttachHandle.Add(resizedObjectHandle);
                            break;
                        } else if (drawArea.GraphicsList[i].GetType() == typeof(DrawLun) && drawArea.GraphicsList[i].PointInObject(point))
                        {
                            DrawLun dl = (DrawLun)drawArea.GraphicsList[i];

                            point = drawArea.GraphicsList[i].GetPointObject(point);
                            dl.AttachLink(resizedObject, DrawObject.TypeAttach.Entree);
                            dl.InitProp("GuidZone", (object)((DrawZone)dl.LstLinkIn[0]).GuidkeyObjet.ToString(), true);
                            resizedObject.AttachLink(dl, DrawObject.TypeAttach.Sortie);
                            ((DrawZone)resizedObject).AttachHandle.Add(resizedObjectHandle);
                            break;
                        }*/
                    }
                }
                resizedObject.Normalize();
                resizedObject = null;
            }
            
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }
	}
}
