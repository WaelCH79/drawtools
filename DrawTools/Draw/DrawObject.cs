using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Globalization;
using System.Collections;
using System.Runtime.Serialization;
using System.Data.Odbc;
using MOI = Microsoft.Office.Interop;
 
using System.Xml;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace DrawTools
{
    /// <summary>
    /// Base class for all draw objects
    /// </summary>

    public enum modeGraphic
    {
        detail,
        resume,
        nom,
        icon,
        forme,
        vide,
    }


    public abstract class DrawObject
	{
        #region Members
        // Object Static proprtties
        public static object missing = System.Type.Missing;
        public static int[] HeightFont = { 9, 8, 7, 12 };
        public static int[] WidthFont = { 5, 5, 5, 6 };
        public static int HeightPtNiveau = 20;
        public static int WidthPtNiveau = 100;
        public static int WidthEspacePtNiveau = 20;
        public static int HeightCard = 14;
        public static int HeightServer = 25; //HeightServer = 20;
        public static int HeightNomServer = 12;
        public static int HeightCadreRef = 20;
        public static int HeightIndicator = 18;
        public static int HeightCompFonc = 15;
        public static int HeightMCompServ = 15;
        public static int HeightTechno = 15;
        public static int HeightInfNCard = 10;
        public static int HeightInterface = 10;
        public static int HeightU = 7;
        public static int HeightSite = 20;
        public static int HeightServerSite = 30;
        public static int HeightPtCnx = 25;
        public static int HeightApplication = 40;
        public static int HeightFlux = 50;
        public static int HeightFluxServer = 20;
        public static int HeightNode = 70;
        public static int HeightMinNameSpace = 70;
        public static int HeightMaxIcon = 30;
        public static int HeightShiftIcon = HeightMaxIcon / 3;
        public static int HeightKsService = 40;
        public static int HeightKsIngress = 40;
        public static int WidthMaxIcon = 30;
        public static int WidthShiftIcon = WidthMaxIcon / 3;
        public static int WidthMinNameSpace = 100;
        public static int WidthFlux = 200;
        public static int WidthFluxId = 30;
        public static int WidthFluxService = 50;
        public static int WidthFluxMaxDefault = 1200;
        public static int WidthFluxMinIntervaleServer = 200;
        public static int WidthApplication = 10;
        public static int WidthBaie = 110;
        public static int WidthProduit = 200;        
        public static int WidthServerSite = 150;
        public static int WidthPtCnx = 40;
        public static int LibWidth = 2;
        public static int radius = 10;
        public static int Axe = 3;
        public static int imgHeight = 65;
        public static int imgWidth = 55;
        public static int imgLogoHeight = 35;
        public static int imgLogoWidth = 35;
        public static int imgIconHeight = 26;
        public static int imgIconWidth = 33;
        public static int imgSmallIconHeight = 15;
        public static int imgSmallIconWidth = 15;
        public static int iLongLib = 250;
        public static int iLongAn = 55;

        public static int iLegendWidth = 300;
        public static int xMaxA4paysage = 1400 + iLegendWidth; //origne: 1150;
        public static int yMaxA4paysage = 1050; //origine: 780;
        public static double radianFace = 0.4; //origine: 0.29;
        public static double radianProfondeur = 0.45; //origine: 0.38;
        public static double lozangeFace = 62; //origine: 60; 
        public static double lozangeProfondeur = 160; //origine: 140;
        
        
           
        // Object properties
        private Guid guidkey;
        private Guid guidkeyobjet;
        private Form1 f;
        private bool bTemporaire;
        private bool selected;
        private bool tooltip;
        private bool okmove;
        private bool align;
        private Color color;
        private int penWidth;
        private string sName;
        private ArrayList lstLinkOut;       // list of Link
        private ArrayList lstLinkIn;
        private ArrayList lstParent;
        private ArrayList lstChild;
        private string sTexte;
        public string[] sValue;
        private ArrayList lstValue;
        private ArrayList lstValueExtention;
        private modeGraphic mg;
        public Dictionary<string, object> dicObj=null;
        public bool bover;
        public int image0Width;
        public int image0Height;

        // Last used property values (may be kept in the Registry)
        private static Color lastUsedColor = Color.Black;
        private static int lastUsedPenWidth = 1;
        
        // Entry names for serialization
        private const string entryColor = "Color";
        private const string entryPenWidth = "PenWidth";


        public enum TypeAttach {
            Entree,
            Sortie,
            Parent,
            Child,
        };

        #endregion

        #region Properties

        /// <summary>
        /// Selection flag
        /// </summary>
        /// 

        public int EpaisseurCard { get{return HeightCard;}}
        public int AXE {get{return Axe;}}
        public int HEIGHTCADREREF {get{return HeightCadreRef;}}
        public int HEIGHTINDICATOR {get{return HeightIndicator;}}
        public int HEIGHTAPPLICATION { get { return HeightApplication; } }
        public int WIDTHAPPLICATION { get { return WidthApplication; } }
        public int HEIGHTPTNIVEAU { get { return HeightPtNiveau; } }
        public int WIDTHESPACEPTNIVEAU { get { return WidthEspacePtNiveau; } }
        public int WIDTHPTNIVEAU { get { return WidthPtNiveau; } }
        
        public Form1 F
        {
            get
            {
                return f;
            }
            set
            {
                f = value;
            }
        }

        
        public Guid Guidkey
        {
            get { return guidkey; }
            set { guidkey = value; }
        }

        public Guid GuidkeyObjet
        {
            get { return guidkeyobjet; }
            set { guidkeyobjet = value;}
        }

        public string Texte
        {
            get { return sTexte; }
            set { sTexte = value; }
        }

        public string Name
        {
            get { return sName;  }
            set { sName = value; }
        }

        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        public bool OkMove
        {
            get { return okmove; }
            set { okmove = value;}
        }

        public modeGraphic ModeGraphic
        {
            get { return mg; }
            set { mg = value; }
        }

        public bool Align
        {
            get { return align; }
            set { align = value; }
        }

        public bool Temporaire
        {
            get { return bTemporaire; }
            set { bTemporaire = value; }
        }

        public bool ToolTip
        {
            get { return tooltip; }
            set { tooltip = value;}
        }

        public ArrayList LstValue
        {
            get { return lstValue; }
            set { lstValue = value; }
        }

        public ArrayList LstValueExtention
        {
            get { return lstValueExtention; }
            set { lstValueExtention = value; }
        }

        public ArrayList LstParent
        {
            get
            {
                return lstParent;
            }
            set
            {
                lstParent = value;
            }
        }

        public ArrayList LstChild
        {
            get
            {
                return lstChild;
            }
            set
            {
                lstChild = value;
            }
        }


        public ArrayList LstLinkIn
        {
            get
            {
                return lstLinkIn;
            }
            set
            {
                lstLinkIn = value;
            }
        }

        public ArrayList LstLinkOut
        {
            get
            {
                return lstLinkOut;
            }
            set
            {
                lstLinkOut = value;
            }
        }

        
        /// <summary>
        /// Color
        /// </summary>
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        /// <summary>
        /// Pen width
        /// </summary>
        public int PenWidth
        {
            get
            {
                return penWidth;
            }
            set
            {
                penWidth = value;
            }
        }

        /// <summary>
        /// Number of handles
        /// </summary>
        public virtual int HandleCount
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Last used color
        /// </summary>
        public static Color LastUsedColor
        {
            get
            {
                return lastUsedColor;
            }
            set
            {
                lastUsedColor = value;
            }
        }

        /// <summary>
        /// Last used pen width
        /// </summary>
        public static int LastUsedPenWidth
        {
            get
            {
                return lastUsedPenWidth;
            }
            set
            {
                lastUsedPenWidth = value;
            }
        }

        #endregion

        #region Virtual Functions

        /// <summary>
        /// Draw object
        /// </summary>
        /// <param name="g"></param>
        public virtual void Draw(Graphics g)
        {
        }

        public virtual int HandleEvent(Point point,int Handle)
        {
            return Handle;
        }

        /// <summary>
        /// Get handle point by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public virtual Point GetHandle(int handleNumber)
        {
            return new Point(0, 0);
        }

        public virtual void CreatNewGuid()
        {
            Temporaire = false;
        }

        public virtual void Link()
        {
            ControlWord cw;

            if (F.wkApp.TadFile != null && System.IO.File.Exists(F.wkApp.TadFile))
            {
                cw = new ControlWord(F, F.wkApp.TadFile, false);
                string sBook = GetType().Name.Substring("Draw".Length, 3) + F.tbTypeVue.Text[0] + GuidkeyObjet.ToString().Replace("-", "");
                if (cw.Exist(sBook) > -1) cw.DisplayBook(sBook);
                cw.Close();
            }
        }

        public virtual void dataGrid_CellClick(DataGridView odgv, DataGridViewCellEventArgs e)
        {
        }

        public virtual int GetTopYNCard()
        {
            return YMin();
        }

        public virtual int GetBottomYNCard()
        {
            return YMax();
        }

        /// <summary>
        /// Get handle rectangle by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public virtual Rectangle GetHandleRectangle(int handleNumber)
        {
            Point point = GetHandle(handleNumber);

            return new Rectangle(point.X - 3, point.Y - 3, 7, 7);
        }

        /// <summary>
        /// Draw tracker for selected object
        /// </summary>
        /// <param name="g"></param>
        public virtual void DrawTracker(Graphics g)
        {
            if ( ! Selected )
                return;

            SolidBrush brush = new SolidBrush(Color.Blue);

            for ( int i = 1; i <= HandleCount; i++ )
            {
                g.FillRectangle(brush, GetHandleRectangle(i));
            }

            brush.Dispose();
        }

        /// <summary>
        /// Hit test.
        /// Return value: -1 - no hit
        ///                0 - hit anywhere
        ///                > 1 - handle number
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual int HitTest(Point point)
        {
            return -1;
        }


        /// <summary>
        /// Test whether point is inside of the object
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual bool PointInObject(Point point)
        {
            return false;
        }

        public virtual bool AttachPointInObject(Point point)
        {
            return false;
        }

        public void GetLabelLinks()
        {
            int idx = GetIndexFromName("GuidLabel");
            if (idx > -1)
            {
                string field = "";
                string sQuery = "SELECT Label.GuidLabel, NomLabel FROM Label, LabelLink WHERE Label.GuidLabel=LabelLink.GuidLabel and GuidObjet ='" + GuidkeyObjet + "'";
                if (F.oCnxBase.CBRecherche(sQuery))
                {
                    while (F.oCnxBase.Reader.Read())
                    {
                        field += ";" + F.oCnxBase.Reader.GetString(1) + "   (" + F.oCnxBase.Reader.GetString(0) + ")";
                    }
                    F.oCnxBase.CBReaderClose();
                    LstValue[idx] = field.Substring(1);
                }
                else F.oCnxBase.CBReaderClose();
            }
        }

        public void SetLabelLinks()
        {
            List<string[]> lstLink = new List<string[]>();
            object o = GetValueFromName("GuidLabel");
            int idx = GetIndexFromName("GuidLabel");
            if (idx > -1 || o != null)
            {
                string Link = (string)o;
                if (Link != "") // les labels ne sont pas analysés (et donc supprimés) si la liste des labels est vide. Il est possible de sauvegarder un objet sans avoir charger ses labels
                {

                    if (F.oCnxBase.CBRecherche("Select GuidLabel, GuidObjet From LabelLink  Where GuidObjet='" + GuidkeyObjet + "'"))
                    {
                        while (F.oCnxBase.Reader.Read())
                        {
                            string[] sTabCur = new string[3];
                            sTabCur[0] = F.oCnxBase.Reader.GetString(0);
                            sTabCur[1] = F.oCnxBase.Reader.GetString(1);
                            sTabCur[2] = " ";
                            lstLink.Add(sTabCur);
                        }
                    }
                    F.oCnxBase.CBReaderClose();

                    if (o != null)
                    {
                        if (Link != "")
                        {
                            string[] aLink = Link.Split(new Char[] { '(', ')' });
                            for (int i = 1; i < aLink.Length; i += 2)
                            {

                                string[] sTabCur;
                                sTabCur = lstLink.Find(elFind => elFind[0] == aLink[i].Trim());
                                if (sTabCur != null) sTabCur[2] = "x";
                                if (!F.oCnxBase.ExistLabelLink(aLink[i].Trim(), GuidkeyObjet.ToString()))
                                {
                                    F.oCnxBase.CBWrite("INSERT INTO LabelLink (GuidLabel, GuidObjet) VALUES ('" + aLink[i].Trim() + "','" + GuidkeyObjet + "')");
                                }

                            }

                        }
                        for (int i = 0; i < lstLink.Count; i++)
                        {
                            if (lstLink[i][2] != "x")
                                F.oCnxBase.CBWrite("DELETE FROM LabelLink WHERE GuidLabel='" + lstLink[i][0] + "' AND GuidObjet ='" + lstLink[i][1] + "'");
                        }
                    }
                }
            }
        }

        public virtual bool ParentPointInObject(Point point)
        {
            return false;
        }

        public virtual void initLinkIn()
        {
            
        }

        public virtual void initLinkOut()
        {
            
        }

        public virtual void initLinkParent()
        {

        }

        public virtual void initLinkChild()
        {

        }

        public virtual void initBaseLinkIn()
        {

        }

        public virtual void initBaseLinkOut()
        {

        }

        public virtual void initBaseLinkParent()
        {

        }

        public virtual void initBaseLinkChild()
        {

        }
       
        public virtual void RemoveNew(bool Graph)
        {
            string sType = this.GetType().Name.Substring("Draw".Length);
            
            if (LstLinkIn != null && LstLinkIn.Count > 0)
            {
                for (int i = 0; i < LstLinkIn.Count; i++)
                {
                    DrawObject o = (DrawObject)LstLinkIn[i];
                    if (Graph)
                    {
                        o.LstLinkOut.Remove((object)this);
                        o.initLinkOut();
                    }
                    else o.initBaseLinkOut();
                }
            }
            if (LstLinkOut != null && LstLinkOut.Count > 0)
            {
                for (int i = 0; i < LstLinkOut.Count; i++)
                {
                    DrawObject o = (DrawObject)LstLinkOut[i];
                    if (Graph)
                    {
                        o.LstLinkIn.Remove((object)this);
                        o.initLinkIn();
                    }
                    else o.initBaseLinkIn();
                }
            }
            if (LstChild != null && LstChild.Count > 0)
            {
                for (int i = 0; i < LstChild.Count; i++)
                {
                    DrawObject o = (DrawObject)LstChild[i];
                    if (Graph)
                    {
                        o.LstParent.Remove((object)this);
                        o.initLinkParent();
                    }
                    else o.initBaseLinkParent();
                }
            }
            if (LstParent != null && LstParent.Count > 0)
            {
                for (int i = 0; i < LstParent.Count; i++)
                {
                    DrawObject o = (DrawObject)LstParent[i];
                    if (Graph)
                    {
                        o.LstChild.Remove((object)this);
                        o.initLinkChild();
                    }
                    else initBaseLinkChild();
                }
            }
        }

        public virtual void VisioDraw(ArrayList lstGuid, ArrayList lstShape, MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
        }

        /*public virtual MOI.Visio.Shape VisioDraw(MOI.Visio.Page page, double yPage, double qxPage, double qyPage)
        {
            return null;
        }*/

        public virtual bool RemoveG()
        {
            string sType = this.GetType().Name.Substring("Draw".Length);
            Table t;
            int n = F.oCnxBase.ConfDB.FindTable("G" + sType);
            if (n > -1)
            {
                t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                string objKey = t.GetFields(ConfDataBase.FieldOption.ObjKey);
                if (objKey != "")
                {

                    if (F.oCnxBase.CBRecherche("SELECT GuidG" + sType + " FROM G" + sType + ", DansVue Where GuidObjet=GuidG" + sType + " AND " + objKey + "='" + GuidkeyObjet + "' AND GuidGVue='" + F.GuidGVue + "'"))
                    {
                        string o = F.oCnxBase.Reader.GetString(0);
                        F.oCnxBase.CBReaderClose();
                        RemoveG(sType, o);
                        RemoveNew(false);
                        return true;
                    }
                    else F.oCnxBase.CBReaderClose();
                }
            }
            return false;
        }

        public virtual void RemoveGSpecifique(string obj)
        {
            
        }

        public virtual void RemoveG(string sType, string obj)
        {
            F.oCnxBase.CBWrite("DELETE FROM DansVue Where GuidObjet = '" + obj + "'");
            RemoveGSpecifique(obj);
            F.oCnxBase.CBWrite("DELETE FROM G" + sType + " Where GuidG" + sType + " = '" + obj + "'");
        }

        public virtual bool Remove(bool bConfirmation=true)
        {
            string sType = this.GetType().Name.Substring("Draw".Length);
            Table t;
            int n = F.oCnxBase.ConfDB.FindTable(sType);
            if (n > -1)
            {
                t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                string sObjGKey = t.GetLibFromName("LinkTables");
                ArrayList lstGTableKey = new ArrayList();
                if (sObjGKey == "") lstGTableKey.Add("G" + sType);
                else
                {
                    string[] splt = sObjGKey.Split(';');
                    for (int i = 0; i < splt.Length; i++) lstGTableKey.Add(splt[i]);
                }
                string objKey = t.GetFields(ConfDataBase.FieldOption.Key);
                if (objKey != "")
                {
                    ArrayList lstguidG = new ArrayList();
                    ArrayList lstVue = new ArrayList();

                    for (int i = 0; i < lstGTableKey.Count; i++)
                    {
                        if (F.oCnxBase.isDataTableExist((string)lstGTableKey[i]))
                        {
                            if (F.oCnxBase.CBRecherche("SELECT Guid" + lstGTableKey[i] + ", App.NomApplication, NomVue, Version FROM DansVue, Vue, " + lstGTableKey[i] + ", Application App, AppVersion Where App.GuidApplication=AppVersion.GuidApplication and Vue.GuidAppVersion=AppVersion.GuidAppVersion And Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=Guid" + lstGTableKey[i] + " AND " + lstGTableKey[i] + "." + objKey + "='" + GuidkeyObjet + "' Order by App.NomApplication"))
                            {
                                while (F.oCnxBase.Reader.Read())
                                {
                                    lstguidG.Add(F.oCnxBase.Reader.GetString(0));
                                    lstVue.Add(F.oCnxBase.Reader.GetString(1) + " - " + F.oCnxBase.Reader.GetString(3) + " - " + F.oCnxBase.Reader.GetString(2));
                                }
                                F.oCnxBase.CBReaderClose();
                            }
                            else F.oCnxBase.CBReaderClose();
                        }
                    }
                    if (bConfirmation && lstVue.Count != 0)
                    {
                        string msg = "L'objet est présent dans les vues suivantes:\n";
                        for (int i = 0; i < lstVue.Count; i++) msg += "   -" + (string)lstVue[i] + "\n";
                        msg += "Voulez-vous continuer?";
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        DialogResult result;
                        result = MessageBox.Show(msg, "suppression", buttons);
                        if (result == System.Windows.Forms.DialogResult.Yes) bConfirmation = false;

                    }
                    else bConfirmation = false;

                    if (!bConfirmation)
                    {
                        for (int i = 0; i < lstguidG.Count; i++)
                        {
                            RemoveG(sType, (string)lstguidG[i]);
                        }
                        F.oCnxBase.CBWrite("DELETE FROM DansTypeVue Where GuidObjet = '" + GuidkeyObjet + "'");
                        //if (F.oCnxBase.isDataTableExist(sType))
                        F.oCnxBase.CBWrite("DELETE FROM " + sType + " Where Guid" + sType + " = '" + GuidkeyObjet + "'");
                        return true;
                    }
                }
            }
            return false;
        }


        /// <summary>
        /// Retourne ou se trouvre le point par rapport à l'objet
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual int LePointEstSitue(Point point)
        {
            return 0;
        }
        
        /// <summary>
        /// Retourne le point le plus proche de l'objet
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual Point GetPointObject(Point point)
        {
            return point;
        }

        public virtual void CompleteLink(TypeAttach Attach)
        {
            
        }

        /// <summary>
        /// Attache l'objet Link à un Objet
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual void AttachLink(DrawObject o, TypeAttach Attach)
        {
            switch (Attach)
            {
                case TypeAttach.Child:
                    if (this.LstChild.IndexOf(o) == -1) LstChild.Add(o);
                    break;
                case TypeAttach.Entree:
                   if (LstLinkIn.IndexOf(o) == -1) LstLinkIn.Add(o);
                    break;
                case TypeAttach.Parent:
                    if (LstParent!=null && LstParent.IndexOf(o) == -1) LstParent.Add(o);
                    break;
                case TypeAttach.Sortie:
                    if (LstLinkOut.IndexOf(o) == -1) LstLinkOut.Add(o);
                    break;
            }
                        
        }

        public virtual void RemoveLink(Object o, TypeAttach Attach)
        {
            int n;
            switch (Attach)
            {
                case TypeAttach.Child:
                    if (LstChild != null)
                    {
                        n = LstChild.IndexOf(o);
                        if (n > -1) LstChild.RemoveAt(n);
                    }
                    break;
                case TypeAttach.Entree:
                    if (LstLinkIn != null)
                    {
                        n = LstLinkIn.IndexOf(o);
                        if (n > -1) LstLinkIn.RemoveAt(n);
                    }
                    break;
                case TypeAttach.Parent:
                    if (LstParent != null)
                    {
                        n = LstParent.IndexOf(o);
                        if (n > -1) LstParent.RemoveAt(n);
                    }
                    break;
                case TypeAttach.Sortie:
                    if (LstLinkOut != null)
                    {
                        n = LstLinkOut.IndexOf(o);
                        if (n > -1) LstLinkOut.RemoveAt(n);
                    }
                    break;
            }

        }

        public virtual void DelObjAttach(string sGuid, ArrayList lst)
        {
            for (int i = 0; i < lst.Count; i++) if (sGuid == ((DrawObject)lst[i]).GuidkeyObjet.ToString()) lst.RemoveAt(i);
        }


        /*

        public virtual int CreatObjetLink(DrawObject d, string NomObjaLier, DrawObject.TypeAttach firstLink, DrawObject.TypeAttach secondLink)
        {
            int n=-1;
            object o = d.GetValueFromName(NomObjaLier);
            if (o != null && (string)o != "")
            {
                n = Owner.GraphicsList.FindObjet(0, (string)o);
                if (n > -1)
                {
                    d.AttachLink(Owner.GraphicsList[n], firstLink);
                    Owner.GraphicsList[n].AttachLink(d, secondLink);
                }
            }
            return n;
        }
        */

        public virtual void DelLinkInProperty()
        {

        }

        public virtual void ClearAttach(DrawObject oLinkObj, TypeAttach firstLink, TypeAttach secondLink)
        {
            DelLinkInProperty();

            int iLinkFirst = -1, iLinkSecond = -1;
            //recherche index link first (sur l'objet en cours)
            switch (firstLink)
            {
                case TypeAttach.Child: iLinkFirst = LstChild.IndexOf(oLinkObj); break;
                case TypeAttach.Entree: iLinkFirst = LstLinkIn.IndexOf(oLinkObj); break;
                case TypeAttach.Parent: iLinkFirst = LstParent.IndexOf(oLinkObj); break;
                case TypeAttach.Sortie: iLinkFirst = LstLinkOut.IndexOf(oLinkObj); break;

            }
            //recherche index link sencond (sur l'objet en cible)
            switch (secondLink)
            {
                case TypeAttach.Child: iLinkSecond = oLinkObj.LstChild.IndexOf(this); break;
                case TypeAttach.Entree: iLinkSecond = oLinkObj.LstLinkIn.IndexOf(this); break;
                case TypeAttach.Parent: iLinkSecond = oLinkObj.LstParent.IndexOf(this); break;
                case TypeAttach.Sortie: iLinkSecond = oLinkObj.LstLinkOut.IndexOf(this); break;
            }
            // clear si les index != de -1
            if (iLinkFirst > -1 && iLinkSecond > -1)
            {
                switch (firstLink)
                {
                    case TypeAttach.Child: LstChild.RemoveAt(iLinkFirst); break;
                    case TypeAttach.Entree: LstLinkIn.RemoveAt(iLinkFirst); break;
                    case TypeAttach.Parent: LstParent.RemoveAt(iLinkFirst); break;
                    case TypeAttach.Sortie: LstLinkOut.RemoveAt(iLinkFirst); break;

                }
                //recherche index link sencond (sur l'objet en cible)
                switch (secondLink)
                {
                    case TypeAttach.Child: oLinkObj.LstChild.RemoveAt(iLinkSecond); break;
                    case TypeAttach.Entree: oLinkObj.LstLinkIn.RemoveAt(iLinkSecond); break;
                    case TypeAttach.Parent: oLinkObj.LstParent.RemoveAt(iLinkSecond); break;
                    case TypeAttach.Sortie: oLinkObj.LstLinkOut.RemoveAt(iLinkSecond); break;
                }
            }
        }

        public virtual void ClearAttach(TypeAttach Attach)
        {
            switch (Attach)
            {
                case TypeAttach.Child:
                    break;
                case TypeAttach.Entree:
                    if (LstLinkIn.Count != 0)
                    {
                        for (int i = 0; i < LstLinkIn.Count; i++)
                        {
                            int j = F.drawArea.GraphicsList.FindObjet(0, ((DrawObject)LstLinkIn[i]).GuidkeyObjet.ToString());
                            if (j > -1)
                            {
                                DrawObject o = (DrawObject)F.drawArea.GraphicsList[j];
                                o.DelObjAttach(GuidkeyObjet.ToString(), o.LstLinkOut);
                                DelObjAttach(((DrawObject)LstLinkIn[i]).GuidkeyObjet.ToString(), LstLinkIn);
                            }
                        }
                    }
                    break;
                case TypeAttach.Parent:
                    break;
                case TypeAttach.Sortie:
                    if (LstLinkOut.Count!=0) {
                        for (int i = 0; i < LstLinkOut.Count; i++)
                        {
                            int j = F.drawArea.GraphicsList.FindObjet(0, ((DrawObject) LstLinkOut[i]).GuidkeyObjet.ToString());
                            if (j > -1)
                            {
                                DrawObject o = (DrawObject)F.drawArea.GraphicsList[j];
                                o.DelObjAttach(GuidkeyObjet.ToString(), o.LstLinkIn);
                                DelObjAttach(((DrawObject)LstLinkOut[i]).GuidkeyObjet.ToString(), LstLinkOut);
                            }
                        }
                    }
                    break;
            }
        }

        public void AffMasque(Graphics g, Rectangle r, Color Couleur, int fill, bool bContour, GraphicsPath masque)
        {
            Pen pen = new Pen(Couleur, 1);
            int NbrColor = 1;
            LinearGradientMode mode= LinearGradientMode.Vertical;
            switch (fill)
            {
                case 0: // --> pas de remplissage
                    break;
                case 1: // --> dégrader une couleur vertical
                    mode = LinearGradientMode.Vertical;
                    break;
                case 2: // --> dégrader deux couleurs vertical
                    mode = LinearGradientMode.Vertical;
                    NbrColor = 2;
                    break;
                case 3: // --> dégrader une couleur horizontal
                    mode = LinearGradientMode.Horizontal;
                    break;
                case 4: // --> dégrader deux couleurs horizontal
                    mode = LinearGradientMode.Horizontal;
                    NbrColor = 2;
                    break;
                case 5: // --> dégrader une couleur BackwardDiagonal
                    mode = LinearGradientMode.BackwardDiagonal;
                    break;
                case 6: // --> dégrader deux couleurs ForwardDiagonal
                    mode = LinearGradientMode.BackwardDiagonal;
                    NbrColor = 2;
                    break;
                case 7: // --> dégrader une couleur BackwardDiagonal
                    mode = LinearGradientMode.ForwardDiagonal;
                    break;
                case 8: // --> dégrader deux couleurs ForwardDiagonal
                    mode = LinearGradientMode.ForwardDiagonal;
                    NbrColor = 2;
                    break;
            }

            if (fill > 0) PaintBackAeroGlass(g, r, NbrColor, Couleur, mode, masque);

            if (bContour)
            {
                
            }
        }

        public void AffRec(Graphics g, Rectangle r, TemplateDt t, int Width)
        {
            AffRec(g, r, t.LineCouleur, Width, t.Couleur, t.Fill, t.Contour, t.Arrondi, t.Ombre, t.LineDash);

        }

        public void AffRec(Graphics g, Rectangle r, ToolObject to, int Width)
        {
            AffRec(g, r, to.LineCouleur, Width, to.Couleur, to.Fill, to.Contour, to.Arrondi, to.Ombre);

        }

        public void AffRec(Graphics g, Rectangle r, ToolObject to, Color LineCouleur, Color Couleur)
        {
            AffRec(g, r, LineCouleur, to.LineWidth, Couleur, to.Fill, to.Contour, to.Arrondi, to.Ombre);
            AffIcon(g, r, to);
        }

        public void AffRec(Graphics g, Rectangle r, ToolObject to)
        {
            AffRec(g, r, to.LineCouleur, to.LineWidth, to.Couleur, to.Fill, to.Contour, to.Arrondi, to.Ombre);
            AffIcon(g, r, to);
        }

        public void AffRec(Graphics g, Rectangle r, Color LineCouleur, int iWidth, Color Couleur, int iFill, bool bContour, bool bArrondi, bool bOmbre, int iLineDash = 0)
        {
            // fill = 0 --> pas de remplissage
            // fill = 1 --> dégrader vertical

            GraphicsPath gp = null;
            Pen linepen = new Pen(LineCouleur, iWidth);
            if (iLineDash == 1)
            {
                float[] dashValues = {5, 5};
                linepen.DashPattern = dashValues;
            }
            int NbrColor = 1;
            LinearGradientMode mode = LinearGradientMode.Vertical;
            switch (iFill)
            {
                case 0: // --> pas de remplissage
                    break;
                case 1: // --> dégrader une couleur vertical
                    mode = LinearGradientMode.Vertical;
                    break;
                case 2: // --> dégrader deux couleurs vertical
                    mode = LinearGradientMode.Vertical;
                    NbrColor = 2;

                    break;
                case 3: // --> dégrader une couleur horizontal
                    mode = LinearGradientMode.Horizontal;
                    break;
                case 4: // --> dégrader deux couleurs horizontal
                    mode = LinearGradientMode.Horizontal;
                    NbrColor = 2;
                    break;
                case 5: // --> dégrader une couleur BackwardDiagonal
                    mode = LinearGradientMode.BackwardDiagonal;
                    break;
                case 6: // --> dégrader deux couleurs ForwardDiagonal
                    mode = LinearGradientMode.BackwardDiagonal;
                    NbrColor = 2;
                    break;
                case 7: // --> dégrader une couleur BackwardDiagonal
                    mode = LinearGradientMode.ForwardDiagonal;
                    break;
                case 8: // --> dégrader deux couleurs ForwardDiagonal
                    mode = LinearGradientMode.ForwardDiagonal;
                    NbrColor = 2;
                    break;
            }
            if (bArrondi)
            {
                /*
                gp = new GraphicsPath();
                gp.AddLine(r.X + radius/2, r.Y, r.X + r.Width, r.Y + r.Height - radius/2);
                gp.AddArc(r.X + r.Width - radius, r.Y + r.Height - radius, radius, radius, 0, 90);
                gp.AddLine(r.X + r.Width - radius/2, r.Y + r.Height, r.X, r.Y + radius/2);
                gp.AddArc(r.X, r.Y, radius, radius, 180, 90);
                */
                gp = new GraphicsPath();
                gp.AddLine(r.X + radius, r.Y, r.X + r.Width - (radius * 2), r.Y);
                gp.AddArc(r.X + r.Width - (radius * 2), r.Y, radius * 2, radius * 2, 270, 90);
                gp.AddLine(r.X + r.Width, r.Y + radius, r.X + r.Width, r.Y + r.Height - (radius * 2));
                gp.AddArc(r.X + r.Width - (radius * 2), r.Y + r.Height - (radius * 2), radius * 2, radius * 2, 0, 90);
                gp.AddLine(r.X + r.Width - (radius * 2), r.Y + r.Height, r.X + radius, r.Y + r.Height);
                gp.AddArc(r.X, r.Y + r.Height - (radius * 2), radius * 2, radius * 2, 90, 90);
                gp.AddLine(r.X, r.Y + r.Height - (radius * 2), r.X, r.Y + radius);
                gp.AddArc(r.X, r.Y, radius * 2, radius * 2, 180, 90);
            }

            if (iFill > 0) PaintBackAeroGlass(g, r, NbrColor, Couleur, mode, gp);

            if (bContour)
            {
                if (bArrondi)
                {
                    g.DrawLine(linepen, r.Left + radius, r.Y, r.Right - radius + 1, r.Y);
                    g.DrawArc(linepen, r.Right - radius * 2, r.Y, radius * 2, radius * 2, 270, 90);
                    g.DrawLine(linepen, r.Right, r.Top + radius, r.Right, r.Bottom - radius + 1);
                    g.DrawArc(linepen, r.Right - radius * 2, r.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
                    g.DrawLine(linepen, r.Right - radius + 1, r.Bottom, r.Left + radius, r.Bottom);
                    g.DrawArc(linepen, r.Left, r.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
                    g.DrawLine(linepen, r.Left, r.Bottom - radius + 1, r.Left, r.Top + radius);
                    g.DrawArc(linepen, r.Left, r.Top, radius * 2, radius * 2, 180, 90);
                }
                else
                {
                    g.DrawRectangle(linepen, r);
                }
            }
            if (bOmbre)
            {
                int ipen = 2, iombre = ipen;
                if (!bContour) iombre = ipen - 1;
                Pen pen = new Pen(Color.Gray, ipen);
                if (bArrondi)
                {
                    int rad = radius + 1;
                    g.DrawLine(pen, r.Right + 1, r.Top + rad + 2, r.Right + 1, r.Bottom + 2 - rad);
                    g.DrawArc(pen, r.Right + 1 - (rad * 2), r.Bottom + 1 - (rad * 2), rad * 2, rad * 2, 0, 90);
                    g.DrawLine(pen, r.Right + 2 - rad, r.Bottom + 1, r.Left + 2 + rad, r.Bottom + 1);
                }
                else
                {
                    g.DrawLine(pen, r.Right + iombre, r.Top + ipen, r.Right + iombre, r.Bottom + iombre);
                    g.DrawLine(pen, r.Right + iombre, r.Bottom + iombre, r.Left + iombre, r.Bottom + iombre);
                }
            }
        }

        public Rectangle AffIcon(Graphics g, Rectangle r, TemplateDt t)
        {
            Rectangle retourR = new Rectangle();
            if (t.Icon0 != "")
            {
                Bitmap image1 = (Bitmap)Image.FromFile(F.sPathRoot + @"\bouton\" + t.Icon0, true);
                double ratio = (double)image1.Width / (double)image1.Height;

                if(r.Width != 0 && r.Height != 0)
                    retourR = new Rectangle(r.Left, r.Top + HeightFont[0], (int)((r.Height - HeightFont[0]) * ratio), r.Height - HeightFont[0]);
                else if(r.Width ==0)
                    retourR = new Rectangle(r.Left, r.Top, r.Height * image1.Width / image1.Height, r.Height);
                else
                    retourR = new Rectangle(r.Left, r.Top, r.Width, r.Width * image1.Height / image1.Width);
                image0Width = retourR.Width;
                image0Height = retourR.Height;
                g.DrawImage(image1, retourR);
            }

            object oType = GetValueFromName("TypeIb");
            if (oType != null && (int)oType != 0)
            {
                string sFile = "";
                switch ((int)oType)
                {
                    case 1:  if (t.Icon1b  != "") sFile = F.sPathRoot + @"\bouton\" + t.Icon1b;  break;
                    case 2:  if (t.Icon2b  != "") sFile = F.sPathRoot + @"\bouton\" + t.Icon2b;  break;
                    case 3:  if (t.Icon3b  != "") sFile = F.sPathRoot + @"\bouton\" + t.Icon3b;  break;
                    case 4:  if (t.Icon4b  != "") sFile = F.sPathRoot + @"\bouton\" + t.Icon4b;  break;
                    case 5:  if (t.Icon5b  != "") sFile = F.sPathRoot + @"\bouton\" + t.Icon5b;  break;
                    case 6:  if (t.Icon6b  != "") sFile = F.sPathRoot + @"\bouton\" + t.Icon6b;  break;
                    case 7:  if (t.Icon7b  != "") sFile = F.sPathRoot + @"\bouton\" + t.Icon7b;  break;
                    case 8:  if (t.Icon8b  != "") sFile = F.sPathRoot + @"\bouton\" + t.Icon8b;  break;
                    case 9:  if (t.Icon9b  != "") sFile = F.sPathRoot + @"\bouton\" + t.Icon9b;  break;
                    case 10: if (t.Icon10b != "") sFile = F.sPathRoot + @"\bouton\" + t.Icon10b; break;
                    case 11: if (t.Icon11b != "") sFile = F.sPathRoot + @"\bouton\" + t.Icon11b; break;
                }
                if (sFile != "")
                {
                    Bitmap image2 = (Bitmap)Image.FromFile(sFile, true);
                    g.DrawImage(image2, new Rectangle(r.Left - 2 * AXE , r.Top + r.Height * 3 / 4, r.Height / 2 * image2.Width / image2.Height, r.Height / 2));
                }
            }
            oType = GetValueFromName("TypeIt");
            if (oType != null && (int)oType != 0)
            {
                string sFile = "";
                switch ((int)oType)
                {
                    case 1:  if (t.Icon1t  != "") sFile = F.sPathRoot + @"\bouton\" + t.Icon1t;  break;
                    case 2:  if (t.Icon2t  != "") sFile = F.sPathRoot + @"\bouton\" + t.Icon2t;  break;
                    case 3:  if (t.Icon3t  != "") sFile = F.sPathRoot + @"\bouton\" + t.Icon3t;  break;
                    case 4:  if (t.Icon4t  != "") sFile = F.sPathRoot + @"\bouton\" + t.Icon4t;  break;
                    case 5:  if (t.Icon5t  != "") sFile = F.sPathRoot + @"\bouton\" + t.Icon5t;  break;
                    case 6:  if (t.Icon6t  != "") sFile = F.sPathRoot + @"\bouton\" + t.Icon6t;  break;
                    case 7:  if (t.Icon7t  != "") sFile = F.sPathRoot + @"\bouton\" + t.Icon7t;  break;
                    case 8:  if (t.Icon8t  != "") sFile = F.sPathRoot + @"\bouton\" + t.Icon8t;  break;
                    case 9:  if (t.Icon9t  != "") sFile = F.sPathRoot + @"\bouton\" + t.Icon9t;  break;
                    case 10: if (t.Icon10t != "") sFile = F.sPathRoot + @"\bouton\" + t.Icon10t; break;
                    case 11: if (t.Icon11t != "") sFile = F.sPathRoot + @"\bouton\" + t.Icon11t; break;
                }
                if (sFile != "")
                {
                    Bitmap image2 = (Bitmap)Image.FromFile(sFile, true);
                    int ImgW = r.Width / 2; if ( ImgW > WidthMaxIcon) ImgW = WidthMaxIcon;
                    int ImgH = ImgW * image2.Height / image2.Width;
                    //g.DrawImage(image2, new Rectangle(r.Left - 2 * AXE, r.Top - r.Height * 1 / 4, r.Height / 2 * image2.Width / image2.Height, r.Height / 2));
                    //g.DrawImage(image2, new Rectangle(r.Left - ImgW /3, r.Top - ImgH / 3, ImgW, ImgH));
                    g.DrawImage(image2, new Rectangle(r.Left - WidthShiftIcon, r.Top - HeightShiftIcon, ImgW, ImgH));
                }
            }
            return retourR;
        }

        public void AffIcon(Graphics g, Rectangle r, ToolObject to)
        {
            if (to.Icon0 != "")
            {
                Bitmap image1 = (Bitmap)Image.FromFile(F.sPathRoot + @"\bouton\" + to.Icon0, true);
                g.DrawImage(image1, new Rectangle(r.Left, r.Top + HeightFont[0], r.Width, r.Height - HeightFont[0]));
            }

            object oType = GetValueFromName("TypeIb");
            if (oType != null && (int)oType != 0)
            {
                string sFile = "";
                switch ((int)oType)
                {
                    case 1: if (to.Icon1b != "") sFile = F.sPathRoot + @"\bouton\" + to.Icon1b; break;
                    case 2: if (to.Icon2b != "") sFile = F.sPathRoot + @"\bouton\" + to.Icon2b; break;
                    case 3: if (to.Icon3b != "") sFile = F.sPathRoot + @"\bouton\" + to.Icon3b; break;
                    case 4: if (to.Icon4b != "") sFile = F.sPathRoot + @"\bouton\" + to.Icon4b; break;
                    case 5: if (to.Icon5b != "") sFile = F.sPathRoot + @"\bouton\" + to.Icon5b; break;
                    case 6: if (to.Icon6b != "") sFile = F.sPathRoot + @"\bouton\" + to.Icon6b; break;
                    case 7: if (to.Icon7b != "") sFile = F.sPathRoot + @"\bouton\" + to.Icon7b; break;
                    case 8: if (to.Icon8b != "") sFile = F.sPathRoot + @"\bouton\" + to.Icon8b; break;
                    case 9: if (to.Icon9b != "") sFile = F.sPathRoot + @"\bouton\" + to.Icon9b; break;
                    case 10: if (to.Icon10b != "") sFile = F.sPathRoot + @"\bouton\" + to.Icon10b; break;
                    case 11: if (to.Icon11b != "") sFile = F.sPathRoot + @"\bouton\" + to.Icon11b; break;
                }
                if (sFile != "")
                {
                    Bitmap image2 = (Bitmap)Image.FromFile(sFile, true);
                    g.DrawImage(image2, new Rectangle(r.Left - 2 * AXE, r.Top + r.Height * 3 / 4, r.Height / 2 * image2.Width / image2.Height, r.Height / 2));
                }
            }
            oType = GetValueFromName("TypeIt");
            if (oType != null && (int)oType != 0)
            {
                string sFile = "";
                switch ((int)oType)
                {
                    case 1: if (to.Icon1t != "") sFile = F.sPathRoot + @"\bouton\" + to.Icon1t; break;
                    case 2: if (to.Icon2t != "") sFile = F.sPathRoot + @"\bouton\" + to.Icon2t; break;
                    case 3: if (to.Icon3t != "") sFile = F.sPathRoot + @"\bouton\" + to.Icon3t; break;
                    case 4: if (to.Icon4t != "") sFile = F.sPathRoot + @"\bouton\" + to.Icon4t; break;
                    case 5: if (to.Icon5t != "") sFile = F.sPathRoot + @"\bouton\" + to.Icon5t; break;
                    case 6: if (to.Icon6t != "") sFile = F.sPathRoot + @"\bouton\" + to.Icon6t; break;
                    case 7: if (to.Icon7t != "") sFile = F.sPathRoot + @"\bouton\" + to.Icon7t; break;
                    case 8: if (to.Icon8t != "") sFile = F.sPathRoot + @"\bouton\" + to.Icon8t; break;
                    case 9: if (to.Icon9t != "") sFile = F.sPathRoot + @"\bouton\" + to.Icon9t; break;
                    case 10: if (to.Icon10t != "") sFile = F.sPathRoot + @"\bouton\" + to.Icon10t; break;
                    case 11: if (to.Icon11t != "") sFile = F.sPathRoot + @"\bouton\" + to.Icon11t; break;
                }
                if (sFile != "")
                {
                    Bitmap image2 = (Bitmap)Image.FromFile(sFile, true);
                    g.DrawImage(image2, new Rectangle(r.Left - 2 * AXE, r.Top - r.Height * 1 / 4, r.Height / 2 * image2.Width / image2.Height, r.Height / 2));
                }
            }
        }

                
        public Color LightColor(Color C)
        {
            return C;
        }

        public void PaintBackAeroGlass(Graphics graphics, Rectangle zone, int NbrColor, Color couleur, LinearGradientMode mode, GraphicsPath masque)
        {
            Int32 Int_Heiht;
            Rectangle rect1, rect2;

            LinearGradientBrush brush1 = null, brush2 = null;

            try
            {
                Int_Heiht = Convert.ToInt32(zone.Height / 2);
                rect1 = new Rectangle(zone.X, zone.Y, zone.Width, zone.Height);
                rect2 = new Rectangle(zone.X, zone.Y, zone.Width, zone.Height);
                if(NbrColor==2)
                {
                    rect1 = new Rectangle(zone.X, zone.Y, zone.Width, Int_Heiht);
                    rect2 = new Rectangle(zone.X, zone.Y + Int_Heiht, zone.Width, zone.Height - Int_Heiht);
                    brush2 = new LinearGradientBrush(rect2, Color.FromArgb(190, couleur), Color.FromArgb(210, couleur), mode);
                }
                brush1 = new LinearGradientBrush(rect1, Color.FromArgb(50, couleur), Color.FromArgb(160, couleur), mode);

                //Paint des 2 rect
                if (masque == null)
                {
                    //sans masque
                    graphics.FillRectangle(new SolidBrush(Color.White), zone);
                    graphics.FillRectangle(brush1, rect1);
                    if (NbrColor == 2) graphics.FillRectangle(brush2, rect2);
                }
                else
                {
                    Region r = null;
                    try
                    {
                        //Avec masque
                        graphics.FillPath(new SolidBrush(Color.White), masque);

                        r = new Region(masque);
                        r.Intersect(rect1);
                        graphics.FillRegion(brush1, r);

                        if (NbrColor == 2)
                        {
                            r = new Region(masque);
                            r.Intersect(rect2);
                            graphics.FillRegion(brush2, r);
                        }
                    }
                    finally
                    {
                        //Libération mémoire
                        if (r != null) r.Dispose();
                        r = null;
                    }
                }
            }
            catch { }
            finally
            {
                if (brush1 != null) brush1.Dispose();
                brush1 = null;
                if (brush2 != null) brush2.Dispose();
                brush2 = null;
            }
        }

        public virtual int DrawGrpTxt1(object obj, int iGrp, int iajustement, double x, double y, int Np, Color couleur, Color bkgrcouleur)
        {
            Graphics g = null;

            g = (Graphics)obj;
            
            //string sType = this.GetType().Name.Substring("Draw".Length);
            int iWidth = 0;
            string sType = GetTypeSimpleTable();
            Table t;
            int n, Nprop = Np;
            n = F.oCnxBase.ConfDB.FindTable(this.GetType().Name.Substring("Draw".Length));

            if (n < 0) n = F.oCnxBase.ConfDB.FindTable(sType);

            bool Vide = true;

            if (n > -1)
            {
                t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                string s = "";
                Vide = true;
                for (int i = 0; i < t.LstField.Count; i++)
                {
                    if (((Field)t.LstField[i]).GrpAffiche == iGrp)
                    {
                        switch (Nprop)
                        {
                            case 0:
                                break;
                            case 1:
                                s += ((Field)t.LstField[i]).Libelle + ":";
                                //Nprop = 0;
                                break;
                            case 2:
                                object o = null;
                                o = GetValueFromLib("Nom");
                                if (o != null) s += (string)o + ": ";
                                break;
                        }
                        if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.CacheVal) != 0)
                        {
                            ArrayList aL = new ArrayList();
                            aL = GetValueEtCache(LstValue[i].ToString());
                            if (aL.Count != 0)
                            {
                                s += (string)aL[0];
                                Vide = false;
                            }
                        }
                        else
                        {
                            string f = LstValue[i].ToString();
                            if (f != "" && f != "0.0")
                            {
                                s += LstValue[i].ToString();
                                Vide = false;
                            }
                        }

                        if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.Concat) != 0)
                        {
                            if (i < t.LstField.Count - 1 && LstValue[i + 1].ToString() != "") s += " - ";
                        }
                        else
                        {
                            if (!Vide)
                            {
                                if (g != null)
                                {
                                    SolidBrush brsh = new SolidBrush(couleur);
                                    SolidBrush bkgrbrsh = new SolidBrush(bkgrcouleur);
                                    Font nameFont = new Font("Arial", HeightFont[iGrp + iajustement - 1]);
                                    SizeF stringSize = new SizeF();
                                    stringSize = g.MeasureString(s, nameFont);

                                    g.TranslateTransform((float)x, (float)y);
                                    g.RotateTransform(25);
                                    //g.FillRectangle(bkgrbrsh, (float)x, (float)y, stringSize.Width, stringSize.Height);                                   
                                    g.FillRectangle(bkgrbrsh, 0, 0, stringSize.Width, stringSize.Height);                                   
                                    //g.DrawString(s, new Font("Arial", HeightFont[iGrp + iajustement - 1]), brsh, (int)x, (int)y);
                                    g.DrawString(s, new Font("Arial", HeightFont[iGrp + iajustement - 1]), brsh, 0, 0);
                                    g.ResetTransform();

                                    y += HeightFont[iGrp + iajustement - 1] + Axe;
                                }
                                
                                Nprop = Np;
                                iWidth = s.Length * WidthFont[iGrp + iajustement - 1];
                            }
                            s = "";
                            Vide = true;
                        }
                    }
                }
                
            }
            return iWidth;
        }

        public virtual int DrawGrpTxt(object obj, int iGrp, int iajustement, double x, double y, int Np, Color couleur, Color bkgrcouleur)
        {
            Graphics g = null;
            MOI.Visio.Shape shape = null;
            string sVisio = "";

            if (obj != null)
            {
                if (obj.GetType() == typeof(Graphics)) g = (Graphics)obj; else shape = (MOI.Visio.Shape)obj;
            }


            //string sType = this.GetType().Name.Substring("Draw".Length);
            int iWidth = 0;
            string sType = GetTypeSimpleTable();
            Table t;
            int n, Nprop = Np;
            n = F.oCnxBase.ConfDB.FindTable(this.GetType().Name.Substring("Draw".Length));

            if (n < 0) n = F.oCnxBase.ConfDB.FindTable(sType);

            bool Vide = true;

            if (n > -1)
            {
                t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                string s = "";
                Vide = true;
                for (int i = 0; i < t.LstField.Count; i++)
                {
                    if (((Field)t.LstField[i]).GrpAffiche == iGrp)
                    {
                        switch (Nprop)
                        {
                            case 0:
                                break;
                            case 1:
                                s += ((Field)t.LstField[i]).Libelle + ":";
                                //Nprop = 0;
                                break;
                            case 2:
                                object o = null;
                                o = GetValueFromLib("Nom");
                                if (o != null) s += (string)o + ": ";
                                break;
                        }
                        if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.CacheVal) != 0)
                        {
                            ArrayList aL = new ArrayList();
                            aL = GetValueEtCache(LstValue[i].ToString());
                            if (aL.Count != 0)
                            {
                                s += (string)aL[0];
                                Vide = false;
                            }
                        }
                        else
                        {
                            string f = LstValue[i].ToString();
                            if (f != "" && f != "0.0")
                            {
                                s += LstValue[i].ToString();
                                Vide = false;
                            }
                        }

                        if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.Concat) != 0)
                        {
                            if (i < t.LstField.Count - 1 && LstValue[i + 1].ToString() != "") s += " - ";
                        }
                        else
                        {
                            if (!Vide)
                            {
                                if (g != null)
                                {
                                    SolidBrush brsh = new SolidBrush(couleur);
                                    SolidBrush bkgrbrsh = new SolidBrush(bkgrcouleur);
                                    Font nameFont = new Font("Arial", HeightFont[iGrp + iajustement - 1]);
                                    SizeF stringSize = new SizeF();
                                    stringSize = g.MeasureString(s, nameFont);

                                    g.FillRectangle(bkgrbrsh, (float)x, (float)y, stringSize.Width, stringSize.Height);

                                    g.DrawString(s, new Font("Arial", HeightFont[iGrp + iajustement - 1]), brsh, (int)x, (int)y);

                                    y += HeightFont[iGrp + iajustement - 1] + Axe;
                                }
                                else
                                {
                                    sVisio += "\n" + s;

                                }
                                Nprop = Np;
                                iWidth = s.Length * WidthFont[iGrp + iajustement - 1];
                            }
                            s = "";
                            Vide = true;
                        }
                    }
                }
                
                STExtention stEx = t.lstExtention.Find(el => el.sGuidExtention == (string) GetValueFromName("GuidManagedsvc"));
                if (stEx.sGuidExtention != null)
                {
                    for (int i = 0; i < stEx.lstFieldExtention.Count; i++)
                    {
                        if (((Field)stEx.lstFieldExtention[i]).GrpAffiche == iGrp)
                        {
                            switch (Nprop)
                            {
                                case 0:
                                    break;
                                case 1:
                                    s += ((Field)stEx.lstFieldExtention[i]).Libelle + ":";
                                    //Nprop = 0;
                                    break;
                                case 2:
                                    object o = null;
                                    o = GetValueFromLib("Nom");
                                    if (o != null) s += (string)o + ": ";
                                    break;
                            }

                            string f = LstValueExtention[i].ToString();
                            if (f != "" && f != "0.0")
                            {
                                s += LstValueExtention[i].ToString();
                                Vide = false;
                            }


                            if ((((Field)stEx.lstFieldExtention[i]).fieldOption & ConfDataBase.FieldOption.Concat) != 0)
                            {
                                if (i < stEx.lstFieldExtention.Count - 1 && LstValueExtention[i + 1].ToString() != "") s += " - ";
                            }
                            else
                            {
                                if (!Vide)
                                {
                                    if (g != null)
                                    {
                                        SolidBrush brsh = new SolidBrush(couleur);
                                        SolidBrush bkgrbrsh = new SolidBrush(bkgrcouleur);
                                        Font nameFont = new Font("Arial", HeightFont[iGrp + iajustement - 1]);
                                        SizeF stringSize = new SizeF();
                                        stringSize = g.MeasureString(s, nameFont);

                                        g.FillRectangle(bkgrbrsh, (float)x, (float)y, stringSize.Width, stringSize.Height);

                                        g.DrawString(s, new Font("Arial", HeightFont[iGrp + iajustement - 1]), brsh, (int)x, (int)y);

                                        y += HeightFont[iGrp + iajustement - 1] + Axe;
                                    }
                                    else
                                    {
                                        sVisio += "\n" + s;

                                    }
                                    Nprop = Np;
                                    iWidth = s.Length * WidthFont[iGrp + iajustement - 1];
                                }
                                s = "";
                                Vide = true;
                            }
                        }
                    }
                }
                
                if (sVisio != "" && shape != null)
                {
                    //Couleur
                    shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionCharacter, (short)MOI.Visio.VisRowIndices.visRowCharacter, (short)MOI.Visio.VisCellIndices.visCharacterColor).FormulaU = "RGB(" + couleur.R.ToString() + "," + couleur.G.ToString() + "," + couleur.B.ToString() + ")";
                    //Taille
                    shape.Characters.set_CharProps((short)MOI.Visio.VisCellIndices.visCharacterSize, (short)HeightFont[iGrp + iajustement - 1]);
                    //Largeur
                    if (x != 0)
                    {
                        shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowTextXForm, (short)MOI.Visio.VisCellIndices.visXFormWidth).ResultIU = LibWidth;
                        shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowTextXForm, (short)MOI.Visio.VisCellIndices.visXFormPinX).ResultIU = x;
                    }
                    //Hauteur par rapport au bas de la forme
                    if (y != 0) shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowTextXForm, (short)MOI.Visio.VisCellIndices.visXFormPinY).ResultIU = y;
                    //Marge Gauche
                    shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowText, (short)MOI.Visio.VisCellIndices.visTxtBlkLeftMargin).FormulaU = "2 pt";
                    //Marge Droite
                    shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowText, (short)MOI.Visio.VisCellIndices.visTxtBlkRightMargin).FormulaU = "1 pt";
                    //Marge Haute
                    shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowText, (short)MOI.Visio.VisCellIndices.visTxtBlkTopMargin).FormulaU = "1 pt";
                    //Marge Basse
                    shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowText, (short)MOI.Visio.VisCellIndices.visTxtBlkBottomMargin).FormulaU = "1 pt";
                    //Gauche
                    shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionParagraph, (short)MOI.Visio.VisRowIndices.visRowParagraph, (short)MOI.Visio.VisCellIndices.visHorzAlign).ResultIU = 0;

                    shape.Text = sVisio.Substring(1);
                }
            }
            return iWidth;
        }

        public virtual int DrawGrpTxt(object obj, int iGrp, string s, double x, double y, Color couleur)
        {
            Graphics g = null;
            MOI.Visio.Shape shape = null;
            string sVisio = "";

            if (obj != null)
            {
                if (obj.GetType() == typeof(Graphics)) g = (Graphics)obj; else shape = (MOI.Visio.Shape)obj;
            }


            //string sType = this.GetType().Name.Substring("Draw".Length);
            int iWidth = 0;

            if (g != null)
            {
                SolidBrush brsh = new SolidBrush(couleur);
                //if(fill) brsh = new SolidBrush(Color.White);
                SolidBrush bkgrbrsh = new SolidBrush(Color.GhostWhite);
                                    
                Font nameFont = new Font("Arial", HeightFont[iGrp]);
                SizeF stringSize = new SizeF();
                stringSize = g.MeasureString(s, nameFont);
                
                g.FillRectangle(bkgrbrsh, (float)x, (float)y, stringSize.Width, stringSize.Height);
                g.DrawString(s, nameFont, brsh, (int)x, (int)y);

                y += HeightFont[iGrp] + Axe;
            }
            else
            {
                sVisio += "\n" + s;

            }
            iWidth = s.Length * WidthFont[iGrp];


            if (sVisio != "" && shape != null)
            {
                //Couleur
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionCharacter, (short)MOI.Visio.VisRowIndices.visRowCharacter, (short)MOI.Visio.VisCellIndices.visCharacterColor).FormulaU = "RGB(" + couleur.R.ToString() + "," + couleur.G.ToString() + "," + couleur.B.ToString() + ")";
                //Taille
                shape.Characters.set_CharProps((short)MOI.Visio.VisCellIndices.visCharacterSize, (short)HeightFont[iGrp]);
                //Largeur
                if (x != 0)
                {
                    shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowTextXForm, (short)MOI.Visio.VisCellIndices.visXFormWidth).ResultIU = LibWidth;
                    shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowTextXForm, (short)MOI.Visio.VisCellIndices.visXFormPinX).ResultIU = x;
                }
                //Hauteur par rapport au bas de la forme
                if (y != 0) shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowTextXForm, (short)MOI.Visio.VisCellIndices.visXFormPinY).ResultIU = y;
                //Marge Gauche
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowText, (short)MOI.Visio.VisCellIndices.visTxtBlkLeftMargin).FormulaU = "2 pt";
                //Marge Droite
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowText, (short)MOI.Visio.VisCellIndices.visTxtBlkRightMargin).FormulaU = "1 pt";
                //Marge Haute
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowText, (short)MOI.Visio.VisCellIndices.visTxtBlkTopMargin).FormulaU = "1 pt";
                //Marge Basse
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionObject, (short)MOI.Visio.VisRowIndices.visRowText, (short)MOI.Visio.VisCellIndices.visTxtBlkBottomMargin).FormulaU = "1 pt";
                //Gauche
                shape.get_CellsSRC((short)MOI.Visio.VisSectionIndices.visSectionParagraph, (short)MOI.Visio.VisRowIndices.visRowParagraph, (short)MOI.Visio.VisCellIndices.visHorzAlign).ResultIU = 0;

                shape.Text = sVisio.Substring(1);
            }

            return iWidth;
        }

        
        public virtual object GetValueFromLib(string lib)
        {
            string sType = GetTypeSimpleTable();

            int n = F.oCnxBase.ConfDB.FindFieldFromLib(sType,lib);
            if (n > -1)
                return LstValue[n];
            return null;
        }

        public virtual object GetValueFromName(string name)
        {
            string sType = GetTypeSimpleTable();

            int n = F.oCnxBase.ConfDB.FindField(sType, name);
            if (n > -1)
                return LstValue[n];
            return null;
        }

        public virtual object GetValueFromName(Table t, string name)
        {
            string sType = GetTypeSimpleTable();

            int n = F.oCnxBase.ConfDB.FindField(t, sType, name);
            if (n > -1)
                return LstValue[n];
            return null;
        }

        public virtual object GetValueFromIndex(int index)
        {
            string sType = GetTypeSimpleTable();

            //return LstValue[n];
            return null;
        }

        public virtual string GetLibFromName(string name)
        {
            string sType = GetTypeSimpleTable();

            int n = F.oCnxBase.ConfDB.FindField(sType, name);
            if (n > -1)
                return F.oCnxBase.ConfDB.FindLib(sType, name);
            return null;
        }

        public virtual void SetValueFromNameTodgv(DataGridView dgv, string name, object o)
        {
            string sType = this.GetType().Name.Substring("Draw".Length);

            int n = F.oCnxBase.ConfDB.FindField(sType, name);
            if (n > -1)
                dgv.Rows[n].Cells[1].Value = o;
        }

        public virtual bool SetValueFromName(string name, object o, bool bExtention = false)
        {
            string sType = this.GetTypeSimpleTable();
            bool retour = false;

            if (o == null) return retour;

            int n = F.oCnxBase.ConfDB.FindTable(sType);
            if (n > -1)
            {
                Table t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                ArrayList LstV = LstValue;
                ArrayList LstF = t.LstField;
                if (bExtention)
                {
                    LstV = LstValueExtention;

                    STExtention stEx = t.lstExtention.Find(el => el.sGuidExtention == (string) GetValueFromName("GuidManagedsvc"));
                    LstF = stEx.lstFieldExtention;
                }

                n = t.FindField(LstF, name);

                if (n > -1)
                {
                    Field f = (Field)LstF[n];
                    switch (f.Type)
                    {
                        case 's':
                        case 'o':
                            if (o.GetType() != typeof(string)) LstV[n] = Convert.ToString(o);
                            else LstV[n] = o;
                            retour = true;
                            break;
                        case 'p': //picture & 'o' path de l'image
                        case 'q': //picture
                        case 'i':
                            try
                            {
                                if (o.GetType() != typeof(Int32)) LstV[n] = Convert.ToInt32(o);
                                else LstV[n] = o;
                                retour = true;
                            }
                            catch { }
                            break;
                        case 'd':
                            try
                            {
                                if (o.GetType() != typeof(Double)) LstV[n] = Convert.ToDouble(o);
                                else LstV[n] = o;
                                retour = true;
                            }
                            catch { }
                            break;
                        case 't':
                            if (o.GetType() == typeof(string) && (string)o != "")
                            {
                                try
                                {
                                    if (o.GetType() != typeof(Double)) LstV[n] = DateTime.FromOADate(Convert.ToDouble(o));
                                    retour = true;
                                }
                                catch { }

                                //else LstValue[n] = o;
                            }
                            break;
                    }

                }
            }
            return retour;
        }

        public virtual void SetValueFromLib(string name, object o)
        {
            string sType = this.GetTypeSimpleTable();

            int n = F.oCnxBase.ConfDB.FindFieldFromLib(sType, name);
            if (n > -1)
                LstValue[n] = o;
        }

        public virtual int GetIndexFromName(string name)
        {
            //string sType = this.GetType().Name.Substring("Draw".Length);
            string sType = GetTypeSimpleTable();

            return F.oCnxBase.ConfDB.FindField(sType, name);
        }

        
        public virtual ArrayList GetName(string prename)
        {
            //string sType = this.GetType().Name.Substring("Draw".Length);
            string sType = GetTypeSimpleTable();

            return F.oCnxBase.ConfDB.FindNames(sType, prename);
        }

        public virtual int GetIndexFromLib(string Lib)
        {
            string sType = GetTypeSimpleTable();

            return F.oCnxBase.ConfDB.FindFieldFromLib(sType, Lib);
        }

        public virtual int GetIndexFromGName(string name)
        {
            string sType = "G" + GetTypeSimpleTable();

            return F.oCnxBase.ConfDB.FindField(sType, name);
        }

        public virtual void InitProp(string name, object o, bool Init)
        {
            string sType = this.GetType().Name.Substring("Draw".Length);
            
            int n = F.oCnxBase.ConfDB.FindTable(sType);
            if (n > -1)
            {
                Table t;
                t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                n = t.getIField(name);
                if (n > -1)
                {
                    object oLstValue = LstValue[n];
                    LstValue[n] = t.initProp(oLstValue, n, o, Init);
                }

                /*for (int i = 0; i < t.LstField.Count; i++)
                {
                    if (((Field)t.LstField[i]).Name == name)
                    {
                        switch (((Field)t.LstField[i]).Type)
                        {
                            case 's':
                                if (Init || LstValue[i]=="") LstValue[i] = (string)o;
                                else LstValue[i] += " - " + (string)o;
                                break;
                            case 'i':
                                if(Init) LstValue[i] = (int) o;
                                else LstValue[i] = (int) LstValue[i] + (int)o;
                                break;
                            case 'd':
                                if (Init) LstValue[i] = (double)o;
                                else LstValue[i] = (double)LstValue[i] + (double)o;
                                break;
                            case 'a':
                                LstValue[i] = (ArrayList)o;
                                break;
                        }
                        break;
                    }
                }*/
            }
        }

        public virtual void InitValueFromDic(Dictionary<string, object> dic)
        {
            string sType = GetTypeSimpleTable();
            Table t;
            int n = F.oCnxBase.ConfDB.FindTable(sType);
            if (n > -1)
            {
                t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                t.InitValue(LstValue, dic);
            }
        }

        public virtual void InitProp(Table t = null)
        {
            string sType = GetTypeSimpleTable();
            if (t == null)
            {
                int n = F.oCnxBase.ConfDB.FindTable(sType);
                if (n > -1)
                {
                    t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                }
            }
            if (t != null)
            {
                LstValue = t.InitValue();
                LstValueExtention = t.InitValueExtention((string) GetValueFromName("GuidManagedsvc"));
                SetValueFromLib("Guid", GuidkeyObjet.ToString());
                SetValueFromLib("Nom", Texte);
            }
        }

        public virtual string SaveValueEtCache(ArrayList ValueEtCache)
        {
            string[] aValue = ((string)ValueEtCache[0]).Split(';');
            string[] aCache = ((string)ValueEtCache[1]).Split(';');
            string Value = "";

            for (int i = 0; i < aValue.Length; i++)
            {
                Value += ";" + aValue[i] + "  ( " + aCache[i] + ")";
            }

            return Value.Substring(1);
        }

        public virtual void SaveProp(DataGridView dgv, Table t = null)
        {
            //string sType = this.GetType().Name.Substring("Draw".Length);
            string sType = GetTypeSimpleTable();

            if(t== null)
            {
                int n = F.oCnxBase.ConfDB.FindTable(sType);

                if (n > -1)
                    t = (Table)F.oCnxBase.ConfDB.LstTable[n];
            }
            
            if (t != null)
            {
                //for (int i = 0; i < dgv.Rows.Count; i++)
                for (int i = 0; i < t.LstField.Count; i++)
                {
                    switch (((Field)t.LstField[i]).Type)
                    {
                        case 's':
                            if (dgv.Rows[i].Cells[1].Value == null) LstValue[i] = "";
                            else
                            {
                                if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.CacheVal) != 0)
                                {
                                    ArrayList ValueEtCache = new ArrayList();
                                    ValueEtCache.Add(dgv.Rows[i].Cells[1].Value.ToString().Trim());
                                    ValueEtCache.Add(dgv.Rows[i].Cells[3].Value.ToString());
                                    if (((Field)t.LstField[i]).NameCpy != "") SetValueFromName(((Field)t.LstField[i]).NameCpy, dgv.Rows[i].Cells[3].Value.ToString());
                                    if ((string)ValueEtCache[0] == "") LstValue[i] = "";
                                    else LstValue[i] = SaveValueEtCache(ValueEtCache);
                                }
                                else LstValue[i] = dgv.Rows[i].Cells[1].Value.ToString();
                                if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.LinkTv) != 0)
                                {
                                    TreeNode[] ArrayTreeNode = F.tvObjet.Nodes.Find(((Field)t.LstField[i]).Name, true);
                                    if (ArrayTreeNode.Length == 1)
                                    {
                                        for (int j = ArrayTreeNode[0].Nodes.Count; j > 0; j--)
                                        {
                                            ArrayTreeNode[0].Nodes[j-1].Remove();
                                        }
                                    }
                                }
                            }
                            break;
                        case 'i':
                            if (string.IsNullOrEmpty(dgv.Rows[i].Cells[1].Value.ToString())) LstValue[i] = Convert.ToInt32(0);
                            else LstValue[i] = Convert.ToInt32(dgv.Rows[i].Cells[1].Value);
                            break;
                        case 'd':
                            if (string.IsNullOrEmpty(dgv.Rows[i].Cells[1].Value.ToString())) LstValue[i] = Convert.ToDouble(0);
                            else LstValue[i] = Convert.ToDouble(dgv.Rows[i].Cells[1].Value);
                            break;
                        case 'a':
                            ArrayList aArray = new ArrayList();
                            if (dgv.Rows[i].Cells[1].Value == null) aArray.Add(0.0);
                            else
                            {
                                string s = (string)dgv.Rows[i].Cells[1].Value;
                                string[] aValue = s.Split(';');
                                for (int j=0; j < aValue.Length; j++) aArray.Add(Convert.ToDouble(aValue[j]));
                            }
                            LstValue[i] = aArray;
                            break;
                    }
                }
                
                STExtention stEx = t.lstExtention.Find(el => el.sGuidExtention == (string)GetValueFromName("GuidManagedsvc"));
                if (stEx.sGuidExtention != null)
                {
                    for (int i = 0; i < stEx.lstFieldExtention.Count; i++)
                    {
                        if (dgv.Rows[t.LstField.Count + i].Cells[1].Value == null) LstValueExtention[i] = "";
                        else LstValueExtention[i] = dgv.Rows[t.LstField.Count + i].Cells[1].Value.ToString();
                    }
                }
                
            }
        }
        public virtual DrawArea.DrawToolType GetToolTypeForObjExp()
        {
            return DrawArea.DrawToolType.Pointer;
        }

        public virtual Guid GetGuidForObjExp()
        {
            return GuidkeyObjet;
        }

        public virtual bActions MajDelActions(bActions stActions) {
            return stActions;
        }

        public virtual ArrayList GetValueEtCache(string Value)
            // structure de la chaine Value : Value1  (GuidValue1);Valuen (GuidValuen)
        {
            ArrayList ValueEtCache = new ArrayList();
            string sValue = "";
            string sCache = "";
            if (Value != "")
            {
                string[] aValue = Value.Split(';');
                for (int i = 0; i < aValue.Length; i++)
                {
                    string[] s = aValue[i].Split('(', ')');
                    sValue += ";" + s[0].Trim(); sCache += ";" + s[1].Trim();
                }

                ValueEtCache.Add(sValue.Substring(1)); ValueEtCache.Add(sCache.Substring(1));
            }
            return ValueEtCache;
        }

        public virtual int ExternProperty(Field f, CWTable cwtb, int iRow, int iCol)
        {
            int IncRow = 0;
            ArrayList lst;

            if (LstChild != null) lst = LstChild;
            else if (LstLinkIn != null) lst = LstLinkIn;
            else lst = LstLinkOut;
            
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].GetType().Name.Substring("Draw".Length) == f.Name)
                {
                    int NbrRow = ((DrawObject)lst[i]).InsertProperties(cwtb, iRow + IncRow, iCol);
                    cwtb.Tb.Cell(iRow + IncRow, iCol-1).Range.Text = f.Libelle;
                    cwtb.LstMerges.Add(new CWMerge(iRow + IncRow, iCol - 1,iRow + IncRow + NbrRow, iCol - 1));
                    //tb.Cell(iRow + IncRow, iCol - 1).Merge(tb.Cell(iRow + IncRow + NbrRow, iCol - 1));
                    IncRow += NbrRow + 1;
                }
            }
            return IncRow;
        }

        public virtual int InsertProperties(CWTable cwtb, int iRow, int iCol)
        {
            int n,m;
            int IncRow = 1;
            F.tbGuid.Text = Guidkey.ToString();
            //F.tbNom.Text = Texte;

            string sType = this.GetType().Name.Substring("Draw".Length);
            n = F.oCnxBase.ConfDB.FindTable(sType);
            if (n > -1)
            {
                Table t = (Table) F.oCnxBase.ConfDB.LstTable[n];

                object numR = 1; // t.GetNbrTabField() + 1;
                object numC = 2;
                object r;
                float w = cwtb.Tb.Cell(iRow, iCol).Width;
                object stBoldBlue = "dtBoldBlue";
                //object stdtBookmark = "dtBookmark";
                                                
                r = cwtb.Tb.Rows[iRow];
                cwtb.Tb.Rows.Add(ref r);
                cwtb.Tb.Cell(iRow, iCol).Split(ref numR, ref numC);
                
                //tb.Cell(iRow, 1).Range.Text = "IP Address";
                cwtb.Tb.Cell(iRow, iCol).Width = 100; cwtb.Tb.Cell(iRow, iCol + 1).Width = w - 100;
                r = cwtb.Tb.Rows[iRow];
                cwtb.Tb.Rows.Add(ref r);
                cwtb.Tb.Cell(iRow, iCol).Range.Text = "Property";
                cwtb.Tb.Cell(iRow, iCol).Range.set_Style(ref stBoldBlue);
                cwtb.Tb.Cell(iRow, iCol + 1).Range.Text = "Value";
                cwtb.Tb.Cell(iRow, iCol + 1).Range.set_Style(ref stBoldBlue);
                
                for (int i = 0; i < t.LstField.Count; i++)
                {
                    if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.TabNonVisible) == 0)
                    {
                        if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.ObjKey) != 0)
                        {
                            m = F.drawArea.GraphicsList.FindObjet(0, LstValue[i].ToString());
                            if (m > -1)
                            {
                                DrawObject o = (DrawObject)F.drawArea.GraphicsList[m];
                                if (o.Texte.Length != 0)
                                {
                                    r = cwtb.Tb.Rows[iRow + IncRow];
                                    cwtb.Tb.Rows.Add(ref r);
                                    cwtb.Tb.Cell(iRow + IncRow, iCol).Range.Text = ((Field)t.LstField[i]).Libelle;
                                    //cwtb.Tb.Cell(iRow + IncRow, iCol).Range.set_Style(ref stdtBookmark);
                                    cwtb.Tb.Cell(iRow + IncRow, iCol + 1).Range.Text = o.Texte.Replace(';', '\n');
                                    //cwtb.Tb.Cell(iRow + IncRow, iCol + 1).Range.set_Style(ref stdtBookmark);
                                    IncRow++;
                                }
                            }
                        } else if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.CacheVal) != 0)
                        {
                            if (lstValue[i].ToString().Length != 0)
                            {
                                ArrayList ValueEtCache = GetValueEtCache(LstValue[i].ToString());
                                r = cwtb.Tb.Rows[iRow + IncRow];
                                cwtb.Tb.Rows.Add(ref r);

                                cwtb.Tb.Cell(iRow + IncRow, iCol).Range.Text = ((Field)t.LstField[i]).Libelle;
                                //cwtb.Tb.Cell(iRow + IncRow, iCol).Range.set_Style(ref stdtBookmark);
                                if (ValueEtCache.Count == 0)
                                    cwtb.Tb.Cell(iRow + IncRow, iCol + 1).Range.Text = LstValue[i].ToString().Replace(';', '\n');
                                else
                                    cwtb.Tb.Cell(iRow + IncRow, iCol + 1).Range.Text = ValueEtCache[0].ToString().Replace(';', '\n');
                                //cwtb.Tb.Cell(iRow + IncRow, iCol + 1).Range.set_Style(ref stdtBookmark);
                                IncRow++;
                            }
                        }
                        else if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.ExternProperty) != 0)
                        {
                            //cwtb.Tb.Cell(iRow + IncRow, iCol).Range.Text = ((Field)t.LstField[i]).Libelle;
                            IncRow += ExternProperty((Field)t.LstField[i], cwtb, iRow + IncRow, iCol + 1);
                        }
                        else
                        {
                            if (lstValue[i].ToString().Length != 0)
                            {
                                r = cwtb.Tb.Rows[iRow + IncRow];
                                cwtb.Tb.Rows.Add(ref r);
                                cwtb.Tb.Cell(iRow + IncRow, iCol).Range.Text = ((Field)t.LstField[i]).Libelle;
                                //cwtb.Tb.Cell(iRow + IncRow, iCol).Range.set_Style(ref stdtBookmark);
                                switch (((Field)t.LstField[i]).Type)
                                {
                                    case 'p':
                                        cwtb.Tb.Cell(iRow + IncRow, iCol + 1).Range.InlineShapes.AddPicture(System.IO.Directory.GetCurrentDirectory() + '\\' + F.sImgList[(int)Form1.ImgList.Nettbd + (int)LstValue[i]], ref missing, ref missing, ref missing);
                                        break;
                                    case 'o':
                                        cwtb.Tb.Cell(iRow + IncRow, iCol + 1).Range.InlineShapes.AddPicture(LstValue[i].ToString(), ref missing, ref missing, ref missing);
                                        break;
                                    default:
                                        cwtb.Tb.Cell(iRow + IncRow, iCol + 1).Range.Text = LstValue[i].ToString().Replace(';', '\n');
                                        break;
                                }
                                
                                //cwtb.Tb.Cell(iRow + IncRow, iCol + 1).Range.set_Style(ref stdtBookmark);
                                IncRow++;
                            }
                        }
                    }
                }
                cwtb.Tb.Rows[iRow + IncRow].Delete();
            }
            return IncRow-1;
        }



        public virtual void InsertProperties(Microsoft.Office.Interop.Word.Table tb, int iRow)
        {
            int n, m;
            F.tbGuid.Text = Guidkey.ToString();
            //F.tbNom.Text = Texte;

            string sType = GetTypeSimpleTable();

            n = F.oCnxBase.ConfDB.FindTable(sType);
            if (n > -1)
            {
                Table t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                int j = 2;
                tb.Rows.Add(ref missing);
                for (int i = 0; i < t.LstField.Count; i++)
                {
                    if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.TabNonVisible) == 0)
                    {
                        if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.ObjKey) != 0)
                        {
                            m = F.drawArea.GraphicsList.FindObjet(0, LstValue[i].ToString());
                            if (m > -1)
                            {
                                DrawObject o = (DrawObject)F.drawArea.GraphicsList[m];
                                tb.Cell(iRow, j - 1).Range.Text = o.Texte;
                            }
                        }
                        else if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.CacheVal) != 0)
                        {
                            ArrayList ValueEtCache = GetValueEtCache(LstValue[i].ToString());
                            if (ValueEtCache.Count == 0)
                                tb.Cell(iRow, j - 1).Range.Text = LstValue[i].ToString();
                            else
                                //tb.Cell(iRow, j - 1).Range.Text = ValueEtCache[0].ToString() + "," + ValueEtCache[1].ToString();
                                tb.Cell(iRow, j - 1).Range.Text = ValueEtCache[0].ToString();
                        }
                        else
                        {
                            switch (((Field)t.LstField[i]).Type)
                            {
                                case 'i':
                                case 'd':
                                case 's':
                                    tb.Cell(iRow, j - 1).Range.Text = LstValue[i].ToString();
                                    break;
                                case 'p':
                                    if (LstValue[i] == null) LstValue[i] = 0;
                                    tb.Cell(iRow, j - 1).Range.InlineShapes.AddPicture(System.IO.Directory.GetCurrentDirectory() + '\\' + F.sImgList[(int)LstValue[i]], ref missing, ref missing, ref missing);
                                    tb.Cell(iRow, j - 1).Range.InlineShapes[1].Height=15;
                                    tb.Cell(iRow, j - 1).Range.InlineShapes[1].Width = 15;
                                    break;
                            }
                        }
                        j++;
                    }
                }
            }
        }


        public virtual XmlElement XmlInsertProperties(XmlExcel xmlExcel, XmlElement elP, bool bComment = true )
        {
            int n, m;
            F.tbGuid.Text = Guidkey.ToString();

            if (elP == null) elP = xmlExcel.XmlCreatEl(xmlExcel.root, "Root");
            
            string sType = this.GetType().Name.Substring("Draw".Length);
            n = F.oCnxBase.ConfDB.FindTable(sType);
            if (n > -1)
            {
                Table t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                {
                    XmlElement tr = xmlExcel.XmlCreatEl(elP, "tr");
                    XmlElement td;
                    td = xmlExcel.XmlCreatEl(tr, "td"); td.InnerText = "Property";
                    td = xmlExcel.XmlCreatEl(tr, "td"); td.InnerText = "Value";
                }

                for (int i = 0; i < t.LstField.Count; i++)
                {
                    if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.TabNonVisible) == 0)
                    {
                        if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.ObjKey) != 0)
                        {
                            m = F.drawArea.GraphicsList.FindObjet(0, LstValue[i].ToString());
                            if (m > -1)
                            {
                                DrawObject o = (DrawObject)F.drawArea.GraphicsList[m];
                                if (o.Texte.Length != 0)
                                {
                                    XmlElement tr = xmlExcel.XmlCreatEl(elP, "tr");
                                    XmlElement td;
                                    td = xmlExcel.XmlCreatEl(tr, "td"); td.InnerText = ((Field)t.LstField[i]).Libelle;
                                    td = xmlExcel.XmlCreatEl(tr, "td");
                                    string[] aValue = o.Texte.Split(';');
                                    for (int iV = 0; iV < aValue.Length; iV++)
                                    {
                                        XmlElement tds = xmlExcel.XmlCreatEl(td, "p"); tds.InnerText = aValue[iV];
                                    }
                                }
                            }
                        }
                        else if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.CacheVal) != 0)
                        {
                            if (lstValue[i].ToString().Length != 0)
                            {
                                ArrayList ValueEtCache = GetValueEtCache(LstValue[i].ToString());
                                XmlElement tr = xmlExcel.XmlCreatEl(elP, "tr");
                                XmlElement td = xmlExcel.XmlCreatEl(tr, "td"); td.InnerText = ((Field)t.LstField[i]).Libelle;
                                if (ValueEtCache.Count == 0)
                                {
                                    td = xmlExcel.XmlCreatEl(tr, "td"); 
                                    string[] aValue = LstValue[i].ToString().Split(';');
                                    for (int iV = 0; iV < aValue.Length; iV++)
                                    {
                                        XmlElement tds = xmlExcel.XmlCreatEl(td, "p"); tds.InnerText = aValue[iV];
                                    }
                                }
                                else
                                {
                                    td = xmlExcel.XmlCreatEl(tr, "td");
                                    string[] aValue = ValueEtCache[0].ToString().Split(';');
                                    for (int iV = 0; iV < aValue.Length; iV++)
                                    {
                                        XmlElement tds = xmlExcel.XmlCreatEl(td, "p"); tds.InnerText = aValue[iV];
                                    }
                                }
                            }
                        }
                        else if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.ExternProperty) != 0)
                        {
                            ArrayList lst;

                            if (LstChild != null) lst = LstChild;
                            else if (LstLinkIn != null) lst = LstLinkIn;
                            else lst = LstLinkOut;

                            for (int ilst = 0; ilst < lst.Count; ilst++)
                            {
                                if (lst[ilst].GetType().Name.Substring("Draw".Length) == ((Field)t.LstField[i]).Name)
                                {
                                    XmlElement tr = xmlExcel.XmlCreatEl(elP, "tr");
                                    XmlElement td = xmlExcel.XmlCreatEl(tr, "td"); td.InnerText = ((Field)t.LstField[i]).Libelle;
                                    td = xmlExcel.XmlCreatEl(tr, "td");
                                    XmlElement tds = xmlExcel.XmlCreatEl(td, "p");

                                    //XmlElement troot = xmlExcel.XmlCreatEl(tds, "Root");
                                    //troot = 
                                        ((DrawObject)lst[ilst]).XmlInsertProperties(xmlExcel, xmlExcel.XmlCreatEl(tds, "Root"));
                                }
                            }
                        }
                        else
                        {
                            if (lstValue[i].ToString().Length != 0)
                            {
                                XmlElement tr = xmlExcel.XmlCreatEl(elP, "tr");
                                XmlElement td = xmlExcel.XmlCreatEl(tr, "td"); td.InnerText = ((Field)t.LstField[i]).Libelle;
                                td = xmlExcel.XmlCreatEl(tr, "td"); 
                                switch (((Field)t.LstField[i]).Type)
                                {
                                    case 'p':
                                        td.InnerText = System.IO.Directory.GetCurrentDirectory() + '\\' + F.sImgList[(int)Form1.ImgList.Nettbd + (int)LstValue[i]];
                                        break;
                                    case 'o':
                                        td.InnerText = LstValue[i].ToString();
                                        break;
                                    default:

                                        string[] aValue = LstValue[i].ToString().Split(';');
                                        for (int iV = 0; iV < aValue.Length; iV++)
                                        {
                                            XmlElement tds = xmlExcel.XmlCreatEl(td, "p"); tds.InnerText = aValue[iV];
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }

                STExtention stEx = t.lstExtention.Find(el => el.sGuidExtention == (string)GetValueFromName("GuidManagedsvc"));
                if (stEx.sGuidExtention != null)
                {
                    for (int i = 0; i < stEx.lstFieldExtention.Count; i++)
                    {
                        if(((string)LstValueExtention[i]).Length !=0)
                        {
                            XmlElement tr = xmlExcel.XmlCreatEl(elP, "tr");
                            XmlElement td = xmlExcel.XmlCreatEl(tr, "td"); td.InnerText = ((Field) stEx.lstFieldExtention[i]).Libelle;
                            td = xmlExcel.XmlCreatEl(tr, "td"); td.InnerText = (string)LstValueExtention[i];
                        }
                    }
                }

                //Commentaire
                if (bComment)
                {
                    byte[] rawData;
                    if (F.oCnxBase.CBRecherche("Select NomProp, HyperLien, Size, RichText From Comment Where GuidObject = '" + GetKeyComment() + "' and Policy='T' order by Id"))
                    {
                        int nByte = F.oCnxBase.Reader.GetInt32(2);
                        if (nByte > 0)
                        {
                            rawData = new byte[nByte];
                            F.oCnxBase.Reader.GetBytes(3, 0, rawData, 0, nByte);
                            System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
                            rtBox.Rtf = System.Text.Encoding.UTF8.GetString(rawData);

                            XmlElement tr = xmlExcel.XmlCreatEl(elP, "tr");
                            XmlElement td = xmlExcel.XmlCreatEl(tr, "td"); td.InnerText = F.oCnxBase.Reader.GetString(0);
                            td = xmlExcel.XmlCreatEl(tr, "td");
                            XmlElement tds = xmlExcel.XmlCreatEl(td, "p"); tds.InnerText = rtBox.Text.Replace("\n","<br>");
                        }
                    }
                    F.oCnxBase.CBReaderClose();
                }
                return elP;
            }
            return null;
        }

        public byte[] GetrawData(string sNomProp)
        {
            //Commentaire
            byte[] rawData = null;
            if (F.oCnxBase.CBRecherche("Select NomProp, HyperLien, Size, RichText From Comment Where GuidObject = '" + GetKeyComment() + "' and NomProp='" + sNomProp + "'"))
            {
                int nByte = F.oCnxBase.Reader.GetInt32(2);
                if (nByte > 0)
                {
                    rawData = new byte[nByte];
                    F.oCnxBase.Reader.GetBytes(3, 0, rawData, 0, nByte);
                }
            }
            F.oCnxBase.CBReaderClose();
            return rawData;
        }

        public void XmlCreatDansVueEl(XmlDB xmlDB, XmlElement elParent, string sTypeObj)
        {

            XmlElement el = xmlDB.XmlCreatEl(elParent, "DansVue", "GuidGVue,GuidObjet");
            XmlElement elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");
            Guid guid = F.GuidGVue;

            xmlDB.XmlSetAttFromEl(elAtts, "GuidGVue", "s", guid.ToString());
            xmlDB.XmlSetAttFromEl(elAtts, "GuidObjet", "s", Guidkey.ToString());
            xmlDB.XmlSetAttFromEl(elAtts, "TypeObjet", "s", "G" + sTypeObj);
        }

        public virtual XmlElement XmlCreatGObject(XmlDB xmlDB, XmlElement elParent)
        {
            return null;
        }

        public virtual void XmlInsertGObject(XmlDB xmlDB, XmlElement elParent, string sTypeObj)
        {
            if(XmlCreatGObject(xmlDB, elParent)!=null)
                XmlCreatDansVueEl(xmlDB, elParent, sTypeObj);
        }

        public virtual void XmlCreatDansTypeVueEl(XmlDB xmlDB, XmlElement elParent, string sTypeObj)
        {
            XmlElement el = xmlDB.XmlCreatEl(elParent, "DansTypeVue", "GuidTypeVue,GuidObjet");
            XmlElement elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");
            Guid guid = F.GuidTypeVue;

            xmlDB.XmlSetAttFromEl(elAtts, "GuidTypeVue", "s", guid.ToString());
            xmlDB.XmlSetAttFromEl(elAtts, "GuidObjet", "s", GuidkeyObjet.ToString());
            xmlDB.XmlSetAttFromEl(elAtts, "TypeObjet", "s", sTypeObj);

        }

        /*
        public virtual void XmlCreatDansTypeVueEl(XmlElement elRoot, string sTypeObj)
        {
            
            XmlElement el;
            //DansTypeVue
            el = F.docXml.CreateElement("DansTypeVue");
            if (F.oCnxBase.bAttribut)
            {
                el.SetAttribute("SearchKey", "GuidTypeVue,GuidObjet");
                XmlElement elAtts = F.docXml.CreateElement("Attributs");
                F.XmlSetAttFromEl(F.docXml, elAtts, "GuidTypeVue", "s", F.GuidTypeVue.ToString());
                F.XmlSetAttFromEl(F.docXml, elAtts, "GuidObjet", "s", GuidkeyObjet.ToString());
                F.XmlSetAttFromEl(F.docXml, elAtts, "TypeObjet", "s", sTypeObj);
                el.AppendChild(elAtts);
            }
            else
            {
                el.SetAttribute("SearchKey", "sGuidTypeVue,sGuidObjet");
                el.SetAttribute("sGuidTypeVue", F.GuidTypeVue.ToString());
                el.SetAttribute("sGuidObjet", GuidkeyObjet.ToString());
                el.SetAttribute("sTypeObjet", sTypeObj);
            }
            elRoot.AppendChild(el);
        }*/

        public virtual XmlElement XmlInsertProperties(XmlExcel xmlExcel)
        {
            int n, m;
            F.tbGuid.Text = Guidkey.ToString();
            //F.tbNom.Text = Texte;


            string sType = GetTypeSimpleTable();

            n = F.oCnxBase.ConfDB.FindTable(sType);
            if (n > -1)
            {
                Table t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                XmlElement el = xmlExcel.XmlCreatEl(xmlExcel.root, "Row");

                for (int i = 0; i < t.LstField.Count; i++)
                {
                    if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.TabNonVisible) == 0)
                    {
                        if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.ObjKey) != 0)
                        {
                            m = F.drawArea.GraphicsList.FindObjet(0, LstValue[i].ToString());
                            if (m > -1)
                            {
                                DrawObject o = (DrawObject)F.drawArea.GraphicsList[m];
                                el.SetAttribute(((Field)t.LstField[i]).Name, o.Texte);
                            }
                        }
                        else if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.CacheVal) != 0)
                        {
                            ArrayList ValueEtCache = GetValueEtCache(LstValue[i].ToString());
                            if (ValueEtCache.Count == 0)
                                el.SetAttribute(((Field)t.LstField[i]).Name, LstValue[i].ToString());
                            else
                                el.SetAttribute(((Field)t.LstField[i]).Name, ValueEtCache[0].ToString());
                        }
                        else
                        {
                            switch (((Field)t.LstField[i]).Type)
                            {
                                case 'i':
                                case 'd':
                                case 's':
                                    el.SetAttribute(((Field)t.LstField[i]).Name, LstValue[i].ToString());
                                    break;
                                case 'p':
                                    if (LstValue[i] == null) LstValue[i] = 0;
                                    el.SetAttribute(((Field)t.LstField[i]).Name, System.IO.Directory.GetCurrentDirectory() + '\\' + F.sImgList[(int)LstValue[i]]);
                                    break;
                            }
                        }
                    }
                }
                return el;
            }
            return null;
        }

        public virtual XmlElement xmlCreatObjetFromCursor(XmlDB xmlDB)
        {
            string sType = GetTypeSimpleTable();
            XmlElement el = xmlDB.XmlCreatEl(xmlDB.GetCursor(), sType);
            xmlDB.XmlAddText(el, GuidkeyObjet.ToString());
            XmlElement elAtts = xmlDB.XmlGetFirstElFromParent(el, "Attributs");

            Table t;
            int n = F.oCnxBase.ConfDB.FindTable(sType);
            if (n > -1)
            {
                t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                //if (t.LstField.Count != 0) el.SetAttribute("SearchKey", ((Field)t.LstField[0]).Name);
                if (t.LstField.Count != 0) el.SetAttribute("SearchKey", t.GetKey());

                for (int i = 0; i < t.LstField.Count; i++)
                {
                    if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.InterneBD) != 0 && (((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.Calcule) == 0)
                    {

                        switch (((Field)t.LstField[i]).Type)
                        {
                            case 's':
                                if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.ExternKeyTable) != 0 && (string)LstValue[i] != "")
                                {
                                    xmlDB.XmlCreatExternRef(xmlDB.XmlGetFirstElFromParent(el, "Before"), n, ((Field)t.LstField[i]).TableEx, (string)LstValue[i]);
                                }
                                if ((string)LstValue[i] != "") xmlDB.XmlSetAttFromEl(elAtts, ((Field)t.LstField[i]).Name, "s", (string)LstValue[i]);
                                break;
                            case 'i':
                                if ((int)LstValue[i] != 0) xmlDB.XmlSetAttFromEl(elAtts, ((Field)t.LstField[i]).Name, "i", ((int)LstValue[i]).ToString());
                                break;
                            case 'd':
                                if ((double)LstValue[i] != 0) xmlDB.XmlSetAttFromEl(elAtts, ((Field)t.LstField[i]).Name, "d", ((double)LstValue[i]).ToString(new CultureInfo("en-Us")));
                                break;
                        }
                    }
                }
                //Indicator
                xmlDB.XmlCreatIndicator(el);
                //Comment
                xmlDB.XmlCreatComment(el, GuidkeyObjet.ToString());

            }
            return el;

        }

        public virtual XmlElement XmlCreatObject(XmlDB xmlDB)
        {
            XmlElement el = xmlDB.XmlGetElFromInnerText(xmlDB.GetCursor(), GuidkeyObjet.ToString());
            if (el == null) return xmlCreatObjetFromCursor(xmlDB); //else return el;
            return null;
        }

        
        public virtual void InitDatagrid(DataGridView dgv, Table t=null )
        {
            int n;
            F.tbGuid.Text = Guidkey.ToString();
            //F.tbNom.Text = Texte;

            dgv.Rows.Clear();
            string sType = GetTypeSimpleTable();
            //string sType = this.GetType().Name.Substring("Draw".Length);

            if (t == null)
            {
                n = F.oCnxBase.ConfDB.FindTable(sType);
                if (n > -1) t = (Table)F.oCnxBase.ConfDB.LstTable[n];
            }

            if (t != null)
            {
                for (int i = 0; i < t.LstField.Count; i++)
                {
                    if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.CacheVal) != 0)
                    {
                        ArrayList ValueEtCache = GetValueEtCache(LstValue[i].ToString());
                        if (ValueEtCache.Count == 0)
                        {
                            string[] row = { ((Field)t.LstField[i]).Libelle, LstValue[i].ToString(), "", "" };
                            //F.dataGrid.Rows.Add(row);
                            dgv.Rows.Add(row);
                        }
                        else
                        {
                            string[] row = { ((Field)t.LstField[i]).Libelle, ValueEtCache[0].ToString(), "", ValueEtCache[1].ToString() };
                            //F.dataGrid.Rows.Add(row);
                            dgv.Rows.Add(row);
                        }
                    }
                    else
                    {
                        if (lstValue[i].GetType() == typeof(ArrayList))
                        {
                            ArrayList aArray = (ArrayList)lstValue[i];
                            string sA = "";
                            for (int m = 0; m < aArray.Count; m++) sA += ";" + aArray[m].ToString();
                            string[] row = { ((Field)t.LstField[i]).Libelle, sA.Substring(1), "", "" };
                            //F.dataGrid.Rows.Add(row);
                            dgv.Rows.Add(row);
                        }
                        else
                        {
                            string[] row = { ((Field)t.LstField[i]).Libelle, LstValue[i].ToString(), "", "" };
                            //F.dataGrid.Rows.Add(row);
                            dgv.Rows.Add(row);
                        }
                    }
                    if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.ReadOnly) != 0)
                        //F.dataGrid.Rows[F.dataGrid.Rows.Count-1].ReadOnly = true;
                        dgv.Rows[dgv.Rows.Count - 1].ReadOnly = true;

                    if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.NonVisible) != 0)
                        //F.dataGrid.Rows[F.dataGrid.Rows.Count - 1].Visible = false;
                        dgv.Rows[dgv.Rows.Count - 1].Visible = false;

                    if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.LinkTv) != 0)
                        MajTreeView(((Field)t.LstField[i]).Name);

                }
                STExtention stEx = t.lstExtention.Find(el => el.sGuidExtention == (string) GetValueFromName("GuidManagedsvc"));
                if (stEx.sGuidExtention != null)
                {
                    for (int i = 0; i < stEx.lstFieldExtention.Count; i++)
                    {
                        string[] row = { ((Field)stEx.lstFieldExtention[i]).Libelle, LstValueExtention[i].ToString(), "", "" };
                        dgv.Rows.Add(row);
                    }
                }
            }
            
        }

        public virtual void SaveExtention()
        {
            int n;
            Table t=null;
            string sType = GetTypeSimpleTable();
            CnxBase cnx = F.oCnxBase;
            n = cnx.ConfDB.FindTable(sType);

            if (n > -1)
            {
                t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                STExtention stEx = t.lstExtention.Find(el => el.sGuidExtention == (string)GetValueFromName("GuidManagedsvc"));

                if (stEx.sGuidExtention != null)
                {
                    for (int i = 0; i < stEx.lstFieldExtention.Count; i++)
                    {
                        if (cnx.ExistExtentionValue(GuidkeyObjet.ToString(), ((Field)stEx.lstFieldExtention[i]).Name))
                        {
                            //update
                            F.oCnxBase.CBWrite("UPDATE ExtentionParamValue Set Value = '" + LstValueExtention[i] + "' Where GuidObject = '" + GuidkeyObjet.ToString() + "'and GuidParam = '" + ((Field)stEx.lstFieldExtention[i]).Name + "'");
                        }
                        else
                        {
                            //create
                            F.oCnxBase.CBWrite("INSERT INTO ExtentionParamValue (GuidObject, GuidParam, Value) VALUES ('" + GuidkeyObjet.ToString() + "','" + ((Field)stEx.lstFieldExtention[i]).Name + "','" + LstValueExtention[i] + "')");
                        }
                    }
                }
            }
        }

        public virtual void MajTreeView(string Name)
        {
        }

        public virtual void ChangeName(string s)
        {
            Texte = s;
        }


        /// <summary>
        /// Vérifie si l'objet à déplacer n'est attaché avec d'autres objets
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual int MovePossible(DrawObject o)
        {
            return -1;
        }

        /// <summary>
        /// Get curesor for the handle
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public virtual Cursor GetHandleCursor(int handleNumber)
        {
            return Cursors.Default;
        }

        /// <summary>
        /// Test whether object intersects with rectangle
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public virtual bool IntersectsWith(Rectangle rectangle)
        {
            return false;
        }

        /// <summary>
        /// Move object
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public virtual void Move(int deltaX, int deltaY)
        {
        }

        /// <summary>
        /// Move un handle de l'object
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public virtual Point MoveHandle(int handleNumber, int deltaX, int deltaY)
        {
            return new Point(0, 0);
        }

        /// <summary>
        /// Move handle to the point
        /// </summary>
        /// <param name="point"></param>
        /// <param name="handleNumber"></param>
        public virtual void MoveHandleTo(Point point, int handleNumber)
        {
        }

        /// <summary>
        /// Dump (for debugging)
        /// </summary>
        public virtual void Dump()
        {
            Trace.WriteLine("");
            Trace.WriteLine(this.GetType().Name);
            Trace.WriteLine("Selected = " + selected.ToString(CultureInfo.InvariantCulture));
        }

        public virtual bool existDB()
        {
            string sType = GetTypeSimpleTable();

            Table t;
            int n = F.oCnxBase.ConfDB.FindTable(sType);
            if (n > -1)
            {
                t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                string sKey = t.GetKey(), sTable = GetTable(sType), sSearch = "", sAnd = " and ";
                string[] saTemp = sKey.Split(',');

                for (int i = 0; i < saTemp.Length; i++)
                    sSearch += sAnd + saTemp[i] + "='" + (string) GetValueFromName(saTemp[i]) + "'";
                sSearch = sSearch.Substring(sAnd.Length);

                if (F.oCnxBase.CBRecherche("SELECT " + sKey + " FROM " + sTable + " WHERE " + sSearch))
                {
                    F.oCnxBase.CBReaderClose();
                    return true;
                }
                F.oCnxBase.CBReaderClose();
            }
            return false;
        }

        public virtual void Update()
        {
            //string sType = o.GetsType(false);
            string sType = GetTypeSimpleTable();
            string sFields = "";
            string sSearch = "";
            string sVirgule = " , ", sAnd = " and ";

            Table t;
            int n = F.oCnxBase.ConfDB.FindTable(sType);
            if (n > -1)
            {
                t = (Table)F.oCnxBase.ConfDB.LstTable[n];

                for (int i = 0; i < t.LstField.Count; i++)
                {
                    if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.InterneBD) != 0)
                    {

                        if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.Key) != 0)
                        {
                            switch (((Field)t.LstField[i]).Type)
                            {
                                case 's':
                                    if ((string)LstValue[i] != "")
                                    {
                                        sSearch += sAnd + ((Field)t.LstField[i]).Name + "='" + LstValue[i] + "'";
                                    }
                                    break;
                                case 'p': //picture
                                case 'q': //picture
                                case 'i':
                                    if ((int)LstValue[i] != 0)
                                    {
                                        sSearch += sAnd + ((Field)t.LstField[i]).Name + "=" + LstValue[i];
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            switch (((Field)t.LstField[i]).Type)
                            {
                                case 's':
                                    if ((string)LstValue[i] != "")
                                        sFields += sVirgule + ((Field)t.LstField[i]).Name + "='" + LstValue[i] + "'";
                                    else
                                        sFields += sVirgule + ((Field)t.LstField[i]).Name + "= null";
                                    break;
                                case 'p': //picture
                                case 'q': //picture
                                case 'i':
                                    if ((int)LstValue[i] != 0)
                                    {
                                        sFields += sVirgule + ((Field)t.LstField[i]).Name + "=" + LstValue[i];
                                    }
                                    break;
                                case 'd':
                                    if ((double)LstValue[i] != 0)
                                    {
                                        sFields += sVirgule + ((Field)t.LstField[i]).Name + "=" + LstValue[i].ToString().Replace(',', '.');
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            F.oCnxBase.CBWrite("Update " + GetTable(sType) + " Set "  + sFields.Substring(sVirgule.Length) + " Where " + sSearch.Substring(sAnd.Length));
        }

        /// <summary>
        /// Save Object to the Data Base
        /// </summary>
        public virtual void saveObjecttoDB(Table t=null)
        {
            if(!existDB()) F.oCnxBase.CreatObject(this); // Insert de la Table Objet
            else
                Update(); // Update de la Table Objet
            
            //Label
            SetLabelLinks();
        }

        /// <summary>
        /// Save Object to the Data Base
        /// </summary>
        public virtual void savetoDB()
        {
        }

        public bool savetoDBFait()
        {
            if (F.lstSaveObj.IndexOf(GuidkeyObjet.ToString()) > -1)
                return true;
            return false;
        }

        public void savetoDBOK()
        {
            F.lstSaveObj.Add(GuidkeyObjet.ToString());
        }

        public virtual void saveobjtoDB()
        {

        }

        /// <summary>
        /// Save Object to the Data Base
        /// </summary>
        public virtual XmlElement savetoXml(XmlDB xmlDB, bool GObj)
        {
            return XmlCreatObject(xmlDB); // Table Objet
        }

        public virtual int GetGrpAff()
        {
            return 0;
        }

        public virtual string GetKeyComment()
        {
            return GuidkeyObjet.ToString();
        }

        public virtual string GetLib()
        {
            int iGrp = GetGrpAff();
            string s = "";
            string sType = GetTypeSimpleTable();
            Table t;
            int n = F.oCnxBase.ConfDB.FindTable(this.GetType().Name.Substring("Draw".Length));
            if (n < 0) n = F.oCnxBase.ConfDB.FindTable(sType);

            if (n > -1)
            {
                t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                for (int i = 0; i < t.LstField.Count; i++)
                {
                    if (((Field)t.LstField[i]).GrpAffiche == iGrp)
                    {
                        if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.CacheVal) != 0)
                        {
                            ArrayList aL = new ArrayList();
                            aL = GetValueEtCache(LstValue[i].ToString());
                            if (aL.Count != 0)  s += (string)aL[0];
                        }
                        else
                        {
                            string f = LstValue[i].ToString();
                            if (f != "" && f != "0.0") s += LstValue[i].ToString();
                        }
                        if ((((Field)t.LstField[i]).fieldOption & ConfDataBase.FieldOption.Concat) != 0)
                        {
                            if (i < t.LstField.Count - 1 && LstValue[i + 1].ToString() != "") s += " - ";
                        }
                    }
                }
            }
            return s;
        }

        public virtual void CWInsertProp(ControlDoc cw, string sBook, string sPolicy)
        {
            bool bFirstLigne = true;
            if (F.oCnxBase.CBRecherche("Select NomProp, HyperLien, Size, RichText From Comment Where GuidObject = '" + GetKeyComment() + "' and Policy='" + sPolicy + "' order by Id"))
            {
                while (F.oCnxBase.Reader.Read())
                {
                    int nByte = F.oCnxBase.Reader.GetInt32(2);
                    if (nByte > 0)
                    {
                        byte[] rawData = new byte[nByte];
                        F.oCnxBase.Reader.GetBytes(3, 0, rawData, 0, nByte);
                        switch (sPolicy[0])
                        {
                            case 'P':
                                cw.InsertTextFromId(sBook, false, F.oCnxBase.Reader.GetString(0) + "\n", "Titre 6");
                                cw.InsertRichTextFromId(sBook, false, rawData);
                                cw.InsertTextFromId(sBook, false, "\n", null);
                                break;
                            case 'L':
                                if (bFirstLigne) cw.InsertTextFromId(sBook, false, GetLib() + "\n", "UnderLine");
                                bFirstLigne = false;
                                cw.InsertRichTextFromId(sBook, false, rawData);
                                cw.InsertTextFromId(sBook, false, "\n", null);
                                break;
                        }

                    }
                }
            }
            F.oCnxBase.CBReaderClose();
        }

        /// <summary>
        /// Save Object to the Data Base
        /// </summary>
        public virtual void CWInsert(ControlDoc cw, char cTypeVue)
        {

        }

        public virtual string CWInsertChild(ControlDoc cw, char cTypeVue)
        {
            string sType = GetType().Name.Substring("Draw".Length);
            string sShortType = sType.ToString().Substring(0, 3);
            if(sShortType == "Gen" || sShortType == "Ins") sShortType = sType.Substring(3, sType.Length-3);
            string sGuidP =  sShortType + cTypeVue + ((DrawObject)LstParent[0]).GuidkeyObjet.ToString().Replace("-", "");
            string sGuid = cTypeVue + GuidkeyObjet.ToString().Replace("-", "");
            //sType ne doit pas depasse 4 caracteres
            string sBook = sType.Substring(0, 3) + sGuid;
            string sTabBookmark = "Tab" + sGuid;
            if (cw.Exist("n" + sGuid) > -1)
            {
                cw.InsertTextFromId("n" + sGuid, true, Texte, "Titre 5");
                cw.InsertTextFromId(sTabBookmark, true, "\n", null);
                cw.InsertTabFromId(sTabBookmark, true, this, null, false, null);
            }
            else if (cw.Exist(sGuidP) > -1)
            {
                cw.InsertTextFromId(sGuidP, false, "\n", null);
                cw.CreatIdFromIdP(sBook, sGuidP);
                cw.InsertTextFromId(sBook, true, Texte + "\n", "Titre 5");
                cw.CreatIdFromIdP("n" + sGuid, sBook);
                CWInsertProp(cw, sBook, "P");
                cw.InsertTextFromId(sBook, false, "Properties\n", "Titre 6");
                cw.InsertTextFromId(sBook, false, "\n", null);
                cw.CreatIdFromIdP(sTabBookmark, sBook);
                cw.InsertTextFromId(sTabBookmark, true, "\n", null);
                cw.InsertTabFromId(sTabBookmark, false, this, null, false, null);
            }

            return sBook;
        }

        public virtual void CWInsertChild(ControlDoc cw, string sBook)
        {
            
        }

        /// <summary>
        /// Normalize object.
        /// Call this function in the end of object resizing.
        /// </summary>
        public virtual void Normalize()
        {
        }

                /// <summary>
        /// Save object to serialization stream
        /// </summary>
        /// <param name="info"></param>
        /// <param name="orderNumber"></param>
        public virtual void SaveToStream(SerializationInfo info, int orderNumber)
        {
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                    "{0}{1}",
                    entryColor, orderNumber),
                Color.ToArgb());

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryPenWidth, orderNumber),
                PenWidth);
        }

        public virtual string GetsType(bool Reel)
        {
            return GetType().Name.Substring("Draw".Length);
        }

        public virtual string GetTypeSimpleTable()
        {
            return GetType().Name.Substring("Draw".Length);
        }

        public virtual string GetTable(string sType) {
            return sType;
        }

        public virtual string GetSelectExtention()
        {
            return null;
        }

        public virtual string GetTypeSimpleGTable()
        {
            return "G" + GetType().Name.Substring("Draw".Length);
        }

        public virtual int GetIndexFromListe(ArrayList Lst, Guid Guid)
        {
            for(int i=0; i<Lst.Count; i++)
            {
                DrawObject o = (DrawObject)Lst[i];
                if (o.GuidkeyObjet == Guid) return i;
            }
            return -1;
        }

        public virtual int GetNbrChild(Type t)
        {
            if (t != null)
            {
                int CountObj = 0;

                for (int i = 0; i < LstChild.Count; i++)
                    if (LstChild[i].GetType() == t) CountObj++;

                return CountObj;
            }
            else return LstChild.Count;
        }

        public virtual int GetIndexFirtObj(string sType)
        {
            for (int i = 0; i < LstChild.Count; i++)
                if (LstChild[i].GetType().Name.Substring("Draw".Length) == sType) return i;

            return -1;
        }

        public virtual int XMin()
        {
            return 0;
        }

        public virtual int XMax()
        {
            return 0;
        }

        public virtual int YMin()
        {
            return 0;
        }

        public virtual int YMax()
        {
            return 0;
        }

        /// <summary>
        /// Load object from serialization stream
        /// </summary>
        /// <param name="info"></param>
        /// <param name="orderNumber"></param>
        public virtual void LoadFromStream(SerializationInfo info, int orderNumber)
        {
            int n = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                    "{0}{1}",
                    entryColor, orderNumber));

            Color = Color.FromArgb(n);

            PenWidth = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryPenWidth, orderNumber));
        }

        #endregion

        #region Other functions

        /// <summary>
        /// Initialization
        /// </summary>
        protected void Initialize()
        {
            color = lastUsedColor;
            penWidth = LastUsedPenWidth;
            bover = false;
        }

        #endregion
    }

    public class DrawTab : DrawObject
    {
        public string sType;

        public DrawTab(Form1 of, string stype)
        {
            F = of;
            sType = stype;
            LstValue = new ArrayList();
        }

        public override string GetTypeSimpleTable()
        {
            return sType;
        }

    }

    public class DrawApplicationClass : DrawObject
    {
        public DrawApplicationClass(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }
    }

    public class DrawApplicationType : DrawObject
    {
        public DrawApplicationType(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }
    }

    public class DrawArborescence : DrawObject
    {
        public DrawArborescence(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();

        }
    }
    public class DrawBackupClass : DrawObject
    {
        public DrawBackupClass(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }
    }

    public class DrawCadreRef : DrawObject
    {
        public DrawCadreRef(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }
    }

    public class DrawCadreRefApp : DrawObject
    {
        public DrawCadreRefApp(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }
    }

    public class DrawCadreRefFonc : DrawObject
    {
        public DrawCadreRefFonc(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }
    }

    public class DrawDiskClass : DrawObject
    {
        public DrawDiskClass(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }
    }

    public class DrawEnvironnement : DrawObject
    {
        public DrawEnvironnement(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }
    }


    public class DrawExploitClass : DrawObject
    {
        public DrawExploitClass(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }
    }

    public class DrawFonction : DrawObject
    {
        public DrawFonction(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }
    }

    public class DrawFonctionService : DrawObject
    {
        public DrawFonctionService(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }
    }

    public class DrawGroupService : DrawObject
    {
        public DrawGroupService(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }
    }

    /*
    public class DrawIndicatorInfo : DrawObject
    {
        public DrawIndicatorInfo(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }

        public override string GetTable(string sType)
        {
            return "Indicator";
        }
    }
    */
    public class DrawOptionsDraw : DrawObject
    {
        public DrawOptionsDraw(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }
    }

    public class DrawTechnoArea : DrawObject
    {
        public DrawTechnoArea(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }
    }

    public class DrawProduitApp : DrawObject
    {
        public DrawProduitApp(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }
    }

    public class DrawService : DrawObject
    {
        public DrawService(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }
    }
    public class DrawServiceLink : DrawObject
    {
        public DrawServiceLink(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }
    }

    public class DrawStaticTable : DrawObject
    {
        public DrawStaticTable(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }
    }

    public class DrawStatut : DrawObject
    {
        public DrawStatut(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }
    }

    public class DrawTemplate : DrawObject
    {
        public DrawTemplate(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }
    }

    public class DrawLayer : DrawObject
    {
        public DrawLayer(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }
    }

    public class DrawLayerLink : DrawObject
    {
        public DrawLayerLink(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }
    }

    public class DrawVue : DrawObject
    {
        public DrawVue(Form1 of, string sGuid)
        {
        }
        
        public DrawVue(ArrayList lstVal)
        {
            LstValue = lstVal;
        }

        public DrawVue(Form1 of, ArrayList lstVal)
        {
            F = of;
            object o = null;

            LstParent = null;
            LstChild = null;
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = lstVal;

            o = GetValueFromLib("Guid");
            if (o != null)
                GuidkeyObjet = new Guid((string)o);
            o = GetValueFromLib("Nom");
        }

        public void GoCopyVesion(string sGuidAppVersion2)
        {
            string sType = GetTypeSimpleTable();

            Table t;
            int n = F.oCnxBase.ConfDB.FindTable(sType);
            if (n > -1)
            {
                t = (Table)F.oCnxBase.ConfDB.LstTable[n];
                //if (F.oCnxBase.CBRecherche("SELECT " + t.GetSelectField(ConfDataBase.FieldOption.Select) + " FROM " + t.Name + " WHERE GuidVue='" + get + "'"))
                {
                   /* while (Parent.oCnxBase.Reader.Read())
                    {
                        string[] aEnreg = new string[2];
                        aEnreg[0] = Parent.oCnxBase.Reader.GetString(0); aEnreg[1] = Parent.oCnxBase.Reader.GetString(1);
                        lstVersion.Add(aEnreg);
                    }*/
                }
                //Parent.oCnxBase.CBReaderClose();
            }
            
        }

        public override void savetoDB()
        {

            if (!savetoDBFait())
            {
                base.savetoDB();
                //Label
                SetLabelLinks();

                savetoDBOK();
            }
        }
        public override void dataGrid_CellClick(DataGridView odgv, DataGridViewCellEventArgs e)
        {
            int n;

            n = GetIndexFromName("GuidLabel");
            if (n > -1 && e.RowIndex == n) // Link Label
            {
                FormLabel fl = new FormLabel(F, odgv);
                fl.AddtvLabelClassFromDB();
                fl.AddlDestinationFromProp();
                //fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fl.ShowDialog(F);
            }

            n = GetIndexFromName("NomEcosystem");
            if (n > -1 && e.RowIndex == n) // Link Ecosystem
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromDB("Select GuidEcosystem, NomEcosystem From Ecosystem order by NomEcosystem", "Value");
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }

            n = GetIndexFromName("PWord");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormPropWord fp = new FormPropWord(F, this);
                fp.ShowDialog(F);
            }
        }
    }

    public class DrawTypeVue : DrawObject
    {
        public DrawTypeVue(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }
    }
    public class DrawVlanClass : DrawObject
    {
        public DrawVlanClass(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }
    }

    public class DrawAppVersion : DrawObject
    {
        public DrawAppVersion(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet =  Guid.NewGuid();

            object o = null;
            o = GetValueFromLib("Guid");
            if (o != null) GuidkeyObjet = new Guid((string)o);
        }
    }
    public class DrawPackage : DrawObject
    {
        public DrawPackage(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }

        public DrawPackage(Form1 of, List<string> lstKey)
        {
            F = of;
            //F.oCnxBase.CBWrite("Insert Into Package (GuidMCompServ, GuidVue) Values ('" + lstKey[0] + "','" + lstKey[1] + "')");
            LstValue = new ArrayList();
            InitProp();
            SetValueFromName("GuidMCompServ", lstKey[0]);
            SetValueFromName("GuidVue", lstKey[1]);
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }

        
    }

    public class DrawPackageDynamic : DrawObject
    {
        public DrawPackageDynamic(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
            }

        public DrawPackageDynamic(Form1 of, List<string> lstKey, Table t)
        {
            F = of;
            //F.oCnxBase.CBWrite("Insert Into Package (GuidMCompServ, GuidVue) Values ('" + lstKey[0] + "','" + lstKey[1] + "')");
            LstValue = new ArrayList();
            InitProp(t);
            SetValueFromName("GuidMCompServ", lstKey[0]);
            SetValueFromName("GuidVue", lstKey[1]);
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }

        public override string GetTypeSimpleTable()
        {
            return "Package";
        }

        public override void saveObjecttoDB(Table t = null)
        {
            if (t != null)
            {
                string sKey = t.GetKey(), sTable = GetTable(GetTypeSimpleTable()), sSearch = "", sAnd = " and ";
                string[] saTemp = sKey.Split(',');

                for (int i = 0; i < saTemp.Length; i++)
                    sSearch += sAnd + saTemp[i] + "='" + (string)GetValueFromName(saTemp[i]) + "'";
                sSearch = sSearch.Substring(sAnd.Length);
                string[] sTabParam = t.GetSelectFieldFromOption(ConfDataBase.FieldOption.Param).Split(',');
                for (int i = 0; i < sTabParam.Length; i++)
                {
                    string sParam = sTabParam[i].Trim();
                    string sValue = (string)GetValueFromName(t, sParam);

                    F.oCnxBase.CBWrite("Delete From Package Where " + sSearch + sAnd + "GuidParam='" + sParam + "'");
                    if (sValue != "")
                    {
                        F.oCnxBase.CBWrite("INSERT INTO Package (GuidMCompServ, GuidVue, GuidParam, Value) VALUES ('" + (string)GetValueFromName(t, "GuidMCompServ") + "','" + (string)GetValueFromName(t, "GuidVue") + "','" + sParam + "','" + sValue + "')");
                    }
                }

                /*if (F.oCnxBase.CBRecherche("SELECT " + sKey + " FROM " + sTable + " WHERE " + sSearch))
                {
                    F.oCnxBase.CBReaderClose();
                    return true;
                }
                F.oCnxBase.CBReaderClose();*/
            }
            //if (!existDB()) F.oCnxBase.CreatObject(this); // Insert de la Table Objet
            //else
            //    Update(); // Update de la Table Objet
        }
    }

    public class DrawTechnoRef : DrawRectangle
    {
        public DrawTechnoRef(Form1 of)
        {
            F = of;

            LstValue = new ArrayList();
            InitProp();
        }

    }

    public class DrawTabPackage : DrawObject
    {
        public DrawTabPackage(Form1 of, ArrayList lstVal)
        {
            F = of;

            LstValue = lstVal;
            Guidkey = Guid.NewGuid();
            GuidkeyObjet = Guid.NewGuid();
        }
    }
}
