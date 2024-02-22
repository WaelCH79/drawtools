using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;


namespace DrawTools
{

    
	/// <summary>
	/// Base class for all tools which create new graphic object
	/// </summary>
	public abstract class ToolObject : DrawTools.Tool
	{
        private Cursor cursor;

        private Color cCouleur;
        private Color cLineCouleur;
        private Color cPen1Couleur;
        private Color cBkGrCouleur;
        private int iLineWidth;
        private int iLine1Width;
        private int iFill;
        private bool bContour;
        private bool bArrondi;
        private bool bOmbre;
        private string sIcon0;
        private string sIcon1b;
        private string sIcon2b;
        private string sIcon3b;
        private string sIcon4b;
        private string sIcon5b;
        private string sIcon6b;
        private string sIcon7b;
        private string sIcon8b;
        private string sIcon9b;
        private string sIcon10b;
        private string sIcon11b;
        private string sIcon1t;
        private string sIcon2t;
        private string sIcon3t;
        private string sIcon4t;
        private string sIcon5t;
        private string sIcon6t;
        private string sIcon7t;
        private string sIcon8t;
        private string sIcon9t;
        private string sIcon10t;
        private string sIcon11t;




        /// <summary>
        /// Tool cursor.
        /// </summary>
        protected Cursor Cursor { get { return cursor; } set { cursor = value; } }
                
        public Color Couleur { get { return cCouleur; } set { cCouleur = value; } }
        public Color LineCouleur { get { return cLineCouleur; } set { cLineCouleur = value; } }
        public Color Pen1Couleur { get { return cPen1Couleur; } set { cPen1Couleur = value; } }
        public Color BkGrCouleur { get { return cBkGrCouleur; } set { cBkGrCouleur = value; } }
        public int LineWidth { get { return iLineWidth; } set { iLineWidth = value; } }
        public int Line1Width { get { return iLine1Width; } set { iLine1Width = value; } }
        public int Fill { get { return iFill; } set { iFill = value; } }
        public bool Contour { get { return bContour; } set { bContour = value; } }
        public bool Arrondi { get { return bArrondi; } set { bArrondi = value; } }
        public bool Ombre { get { return bOmbre; } set { bOmbre = value; } }
        public string Icon0   { get { return sIcon0;   } set { sIcon0   = value; } }
        public string Icon1b  { get { return sIcon1b;  } set { sIcon1b  = value; } }
        public string Icon2b  { get { return sIcon2b;  } set { sIcon2b  = value; } }
        public string Icon3b  { get { return sIcon3b;  } set { sIcon3b  = value; } }
        public string Icon4b  { get { return sIcon4b;  } set { sIcon4b  = value; } }
        public string Icon5b  { get { return sIcon5b;  } set { sIcon5b  = value; } }
        public string Icon6b  { get { return sIcon6b;  } set { sIcon6b  = value; } }
        public string Icon7b  { get { return sIcon7b;  } set { sIcon7b  = value; } }
        public string Icon8b  { get { return sIcon8b;  } set { sIcon8b  = value; } }
        public string Icon9b  { get { return sIcon9b;  } set { sIcon9b  = value; } }
        public string Icon10b { get { return sIcon10b; } set { sIcon10b = value; } }
        public string Icon11b { get { return sIcon11b; } set { sIcon11b = value; } }
        public string Icon1t  { get { return sIcon1t;  } set { sIcon1t = value;  } }
        public string Icon2t  { get { return sIcon2t;  } set { sIcon2t = value;  } }
        public string Icon3t  { get { return sIcon3t;  } set { sIcon3t = value;  } }
        public string Icon4t  { get { return sIcon4t;  } set { sIcon4t = value;  } }
        public string Icon5t  { get { return sIcon5t;  } set { sIcon5t = value;  } }
        public string Icon6t  { get { return sIcon6t;  } set { sIcon6t = value;  } }
        public string Icon7t  { get { return sIcon7t;  } set { sIcon7t = value;  } }
        public string Icon8t  { get { return sIcon8t;  } set { sIcon8t = value;  } }
        public string Icon9t  { get { return sIcon9t;  } set { sIcon9t = value;  } }
        public string Icon10t { get { return sIcon10t; } set { sIcon10t = value; } }
        public string Icon11t { get { return sIcon11t; } set { sIcon11t = value; } }

        public void InitPropriete(string sGuidObjProp)
        {
            //initialisation des proprietes 
            ArrayList LstProp = new ArrayList(), LstVal = new ArrayList();
            int i;

            Owner.Owner.oCnxBase.CBAddArrayListProp(sGuidObjProp, LstProp, LstVal);
            
            i = LstProp.IndexOf("Couleur"); if (i > -1) Couleur = Color.FromName((string)LstVal[i]); else Couleur = Color.White;
            i = LstProp.IndexOf("LineCouleur"); if (i > -1) LineCouleur = Color.FromName((string)LstVal[i]); else LineCouleur = Color.Black;
            i = LstProp.IndexOf("Pen1Couleur"); if (i > -1) Pen1Couleur = Color.FromName((string)LstVal[i]); else Pen1Couleur = Color.Black;
            i = LstProp.IndexOf("BkGrCouleur"); if (i > -1) BkGrCouleur = Color.FromName((string)LstVal[i]); else BkGrCouleur = Color.Transparent;
            i = LstProp.IndexOf("LineWidth"); if (i > -1) LineWidth = Convert.ToInt32((string)LstVal[i]); else LineWidth = 1;
            i = LstProp.IndexOf("Line1Width"); if (i > -1) Line1Width = Convert.ToInt32((string)LstVal[i]); else Line1Width = 1;
            i = LstProp.IndexOf("Fill"); if (i > -1) Fill = Convert.ToInt32((string)LstVal[i]); else Fill = 0;
            i = LstProp.IndexOf("Contour"); if (i > -1) Contour = Convert.ToBoolean((string)LstVal[i]); else Contour = true;
            i = LstProp.IndexOf("Arrondi"); if (i > -1) Arrondi = Convert.ToBoolean((string)LstVal[i]); else Arrondi = false;
            i = LstProp.IndexOf("Ombre"); if (i > -1) Ombre = Convert.ToBoolean((string)LstVal[i]); else Ombre = false;
            i = LstProp.IndexOf("Icon0");   if (i > -1) Icon0   = (string)LstVal[i]; else Icon0   = "";
            i = LstProp.IndexOf("Icon1b");  if (i > -1) Icon1b  = (string)LstVal[i]; else Icon1b  = "";
            i = LstProp.IndexOf("Icon2b");  if (i > -1) Icon2b  = (string)LstVal[i]; else Icon2b  = "";
            i = LstProp.IndexOf("Icon3b");  if (i > -1) Icon3b  = (string)LstVal[i]; else Icon3b  = "";
            i = LstProp.IndexOf("Icon4b");  if (i > -1) Icon4b  = (string)LstVal[i]; else Icon4b  = "";
            i = LstProp.IndexOf("Icon5b");  if (i > -1) Icon5b  = (string)LstVal[i]; else Icon5b  = "";
            i = LstProp.IndexOf("Icon6b");  if (i > -1) Icon6b  = (string)LstVal[i]; else Icon6b  = "";
            i = LstProp.IndexOf("Icon7b");  if (i > -1) Icon7b  = (string)LstVal[i]; else Icon7b  = "";
            i = LstProp.IndexOf("Icon8b");  if (i > -1) Icon8b  = (string)LstVal[i]; else Icon8b  = "";
            i = LstProp.IndexOf("Icon9b");  if (i > -1) Icon9b  = (string)LstVal[i]; else Icon9b  = "";
            i = LstProp.IndexOf("Icon10b"); if (i > -1) Icon10b = (string)LstVal[i]; else Icon10b = "";
            i = LstProp.IndexOf("Icon11b"); if (i > -1) Icon11b = (string)LstVal[i]; else Icon11b = "";
            i = LstProp.IndexOf("Icon1t");  if (i > -1) Icon1t  = (string)LstVal[i]; else Icon1t  = "";
            i = LstProp.IndexOf("Icon2t");  if (i > -1) Icon2t  = (string)LstVal[i]; else Icon2t  = "";
            i = LstProp.IndexOf("Icon3t");  if (i > -1) Icon3t  = (string)LstVal[i]; else Icon3t  = "";
            i = LstProp.IndexOf("Icon4t");  if (i > -1) Icon4t  = (string)LstVal[i]; else Icon4t  = "";
            i = LstProp.IndexOf("Icon5t");  if (i > -1) Icon5t  = (string)LstVal[i]; else Icon5t  = "";
            i = LstProp.IndexOf("Icon6t");  if (i > -1) Icon6t  = (string)LstVal[i]; else Icon6t  = "";
            i = LstProp.IndexOf("Icon7t");  if (i > -1) Icon7t  = (string)LstVal[i]; else Icon7t  = "";
            i = LstProp.IndexOf("Icon8t");  if (i > -1) Icon8t  = (string)LstVal[i]; else Icon8t  = "";
            i = LstProp.IndexOf("Icon9t");  if (i > -1) Icon9t  = (string)LstVal[i]; else Icon9t  = "";
            i = LstProp.IndexOf("Icon10t"); if (i > -1) Icon10t = (string)LstVal[i]; else Icon10t = "";
            i = LstProp.IndexOf("Icon11t"); if (i > -1) Icon11t = (string)LstVal[i]; else Icon11t = "";
        }

        
        /// <summary>
        /// Left mouse is released.
        /// New object is created and resized.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            drawArea.GraphicsList[0].Normalize();
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;

            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }

        /// <summary>
        /// Add new object to draw area.
        /// Function is called when user left-clicks draw area,
        /// and one of ToolObject-derived tools is active.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="o"></param>
        public void AddNewObject(DrawArea drawArea, DrawObject o, bool selected)
        {
            drawArea.GraphicsList.Add(o);
            if (selected)
            {
                drawArea.GraphicsList.UnselectAll();
                o.Selected = true;
                drawArea.OSelected = o;
                //o.InitDatagrid(Owner.Owner.dataGrid);
            }
            drawArea.GraphicsList.Owner.ClearPropObjet();
            drawArea.Capture = true;
            drawArea.MajObjets();

        }

        public override void AddNewObjectFromDraw(DrawArea drawArea, DrawObject o, bool selected)
        {
            AddNewObject(drawArea, o, selected);
        }
	}
}
