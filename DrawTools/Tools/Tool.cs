using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data.Odbc;
using System.Xml;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DrawTools
{
	/// <summary>
	/// Base class for all drawing tools
	/// </summary>

    public class TemplateDt
    {
        public Form1 F;
        public string GuidTemplate;
        public string GuidLayer;

        private Color cCouleur;
        private Color cLineCouleur;
        private Color cPen1Couleur;
        private Color cPen2Couleur;
        private Color cBkGrCouleur;
        private int iLineDash;
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

        public Color Couleur { get { return cCouleur; } set { cCouleur = value; } }
        public Color LineCouleur { get { return cLineCouleur; } set { cLineCouleur = value; } }
        public int LineDash { get { return iLineDash; } set { iLineDash = value; } }
        public Color Pen1Couleur { get { return cPen1Couleur; } set { cPen1Couleur = value; } }
        public Color Pen2Couleur { get { return cPen2Couleur; } set { cPen2Couleur = value; } }
        public Color BkGrCouleur { get { return cBkGrCouleur; } set { cBkGrCouleur = value; } }
        public int LineWidth { get { return iLineWidth; } set { iLineWidth = value; } }
        public int Line1Width { get { return iLine1Width; } set { iLine1Width = value; } }
        public int Fill { get { return iFill; } set { iFill = value; } }
        public bool Contour { get { return bContour; } set { bContour = value; } }
        public bool Arrondi { get { return bArrondi; } set { bArrondi = value; } }
        public bool Ombre { get { return bOmbre; } set { bOmbre = value; } }
        public string Icon0 { get { return sIcon0; } set { sIcon0 = value; } }
        public string Icon1b { get { return sIcon1b; } set { sIcon1b = value; } }
        public string Icon2b { get { return sIcon2b; } set { sIcon2b = value; } }
        public string Icon3b { get { return sIcon3b; } set { sIcon3b = value; } }
        public string Icon4b { get { return sIcon4b; } set { sIcon4b = value; } }
        public string Icon5b { get { return sIcon5b; } set { sIcon5b = value; } }
        public string Icon6b { get { return sIcon6b; } set { sIcon6b = value; } }
        public string Icon7b { get { return sIcon7b; } set { sIcon7b = value; } }
        public string Icon8b { get { return sIcon8b; } set { sIcon8b = value; } }
        public string Icon9b { get { return sIcon9b; } set { sIcon9b = value; } }
        public string Icon10b { get { return sIcon10b; } set { sIcon10b = value; } }
        public string Icon11b { get { return sIcon11b; } set { sIcon11b = value; } }
        public string Icon1t { get { return sIcon1t; } set { sIcon1t = value; } }
        public string Icon2t { get { return sIcon2t; } set { sIcon2t = value; } }
        public string Icon3t { get { return sIcon3t; } set { sIcon3t = value; } }
        public string Icon4t { get { return sIcon4t; } set { sIcon4t = value; } }
        public string Icon5t { get { return sIcon5t; } set { sIcon5t = value; } }
        public string Icon6t { get { return sIcon6t; } set { sIcon6t = value; } }
        public string Icon7t { get { return sIcon7t; } set { sIcon7t = value; } }
        public string Icon8t { get { return sIcon8t; } set { sIcon8t = value; } }
        public string Icon9t { get { return sIcon9t; } set { sIcon9t = value; } }
        public string Icon10t { get { return sIcon10t; } set { sIcon10t = value; } }
        public string Icon11t { get { return sIcon11t; } set { sIcon11t = value; } }

        public TemplateDt(Form1 f, string GuidL, string Guid)
        {
            F = f;
            GuidTemplate = Guid;
            GuidLayer = GuidL;
        }

        public void InitPropriete(string GuidStaticTable)
        {
            //initialisation des proprietes 
            ArrayList LstProp = new ArrayList(), LstVal = new ArrayList();
            int i;

            F.oCnxBase.CBAddArrayListProp(GuidTemplate, GuidStaticTable, LstProp, LstVal);

            i = LstProp.IndexOf("Couleur"); if (i > -1) Couleur = Color.FromName((string)LstVal[i]); else Couleur = Color.White;
            i = LstProp.IndexOf("LineCouleur"); if (i > -1) LineCouleur = Color.FromName((string)LstVal[i]); else LineCouleur = Color.Black;
            i = LstProp.IndexOf("LineDash"); if (i > -1) LineDash = Convert.ToInt32((string)LstVal[i]); else LineDash = 0;
            i = LstProp.IndexOf("Pen1Couleur"); if (i > -1) Pen1Couleur = Color.FromName((string)LstVal[i]); else Pen1Couleur = Color.Black;
            i = LstProp.IndexOf("Pen2Couleur"); if (i > -1) Pen1Couleur = Color.FromName((string)LstVal[i]); else Pen2Couleur = Color.LightGray;
            i = LstProp.IndexOf("BkGrCouleur"); if (i > -1) BkGrCouleur = Color.FromName((string)LstVal[i]); else BkGrCouleur = Color.Transparent;
            i = LstProp.IndexOf("LineWidth"); if (i > -1) LineWidth = Convert.ToInt32((string)LstVal[i]); else LineWidth = 1;
            i = LstProp.IndexOf("Line1Width"); if (i > -1) Line1Width = Convert.ToInt32((string)LstVal[i]); else Line1Width = 1;
            i = LstProp.IndexOf("Fill"); if (i > -1) Fill = Convert.ToInt32((string)LstVal[i]); else Fill = 0;
            i = LstProp.IndexOf("Contour"); if (i > -1) Contour = Convert.ToBoolean((string)LstVal[i]); else Contour = true;
            i = LstProp.IndexOf("Arrondi"); if (i > -1) Arrondi = Convert.ToBoolean((string)LstVal[i]); else Arrondi = false;
            i = LstProp.IndexOf("Ombre"); if (i > -1) Ombre = Convert.ToBoolean((string)LstVal[i]); else Ombre = false;
            i = LstProp.IndexOf("Icon0"); if (i > -1) Icon0 = (string)LstVal[i]; else Icon0 = "";
            i = LstProp.IndexOf("Icon1b"); if (i > -1) Icon1b = (string)LstVal[i]; else Icon1b = "";
            i = LstProp.IndexOf("Icon2b"); if (i > -1) Icon2b = (string)LstVal[i]; else Icon2b = "";
            i = LstProp.IndexOf("Icon3b"); if (i > -1) Icon3b = (string)LstVal[i]; else Icon3b = "";
            i = LstProp.IndexOf("Icon4b"); if (i > -1) Icon4b = (string)LstVal[i]; else Icon4b = "";
            i = LstProp.IndexOf("Icon5b"); if (i > -1) Icon5b = (string)LstVal[i]; else Icon5b = "";
            i = LstProp.IndexOf("Icon6b"); if (i > -1) Icon6b = (string)LstVal[i]; else Icon6b = "";
            i = LstProp.IndexOf("Icon7b"); if (i > -1) Icon7b = (string)LstVal[i]; else Icon7b = "";
            i = LstProp.IndexOf("Icon8b"); if (i > -1) Icon8b = (string)LstVal[i]; else Icon8b = "";
            i = LstProp.IndexOf("Icon9b"); if (i > -1) Icon9b = (string)LstVal[i]; else Icon9b = "";
            i = LstProp.IndexOf("Icon10b"); if (i > -1) Icon10b = (string)LstVal[i]; else Icon10b = "";
            i = LstProp.IndexOf("Icon11b"); if (i > -1) Icon11b = (string)LstVal[i]; else Icon11b = "";
            i = LstProp.IndexOf("Icon1t"); if (i > -1) Icon1t = (string)LstVal[i]; else Icon1t = "";
            i = LstProp.IndexOf("Icon2t"); if (i > -1) Icon2t = (string)LstVal[i]; else Icon2t = "";
            i = LstProp.IndexOf("Icon3t"); if (i > -1) Icon3t = (string)LstVal[i]; else Icon3t = "";
            i = LstProp.IndexOf("Icon4t"); if (i > -1) Icon4t = (string)LstVal[i]; else Icon4t = "";
            i = LstProp.IndexOf("Icon5t"); if (i > -1) Icon5t = (string)LstVal[i]; else Icon5t = "";
            i = LstProp.IndexOf("Icon6t"); if (i > -1) Icon6t = (string)LstVal[i]; else Icon6t = "";
            i = LstProp.IndexOf("Icon7t"); if (i > -1) Icon7t = (string)LstVal[i]; else Icon7t = "";
            i = LstProp.IndexOf("Icon8t"); if (i > -1) Icon8t = (string)LstVal[i]; else Icon8t = "";
            i = LstProp.IndexOf("Icon9t"); if (i > -1) Icon9t = (string)LstVal[i]; else Icon9t = "";
            i = LstProp.IndexOf("Icon10t"); if (i > -1) Icon10t = (string)LstVal[i]; else Icon10t = "";
            i = LstProp.IndexOf("Icon11t"); if (i > -1) Icon11t = (string)LstVal[i]; else Icon11t = "";
        }


    }

    public class LayerList
    {
        public ArrayList lstTemplate;
        public string GuidStaticTable;

        public LayerList(string Guid)
        {
            GuidStaticTable = Guid;
            lstTemplate = new ArrayList();
        }

        public void AddTemplate(Form1 f, string GuidLayer, string GuidTemplate)
        {
            TemplateDt Template = new TemplateDt(f, GuidLayer, GuidTemplate);
            Template.InitPropriete(GuidStaticTable);
            lstTemplate.Add(Template);
        }

        public void Clear()
        {
            for (int i = lstTemplate.Count - 1; i > 0; i--)
                lstTemplate.RemoveAt(i);
        }

    }

    public abstract class Tool
    {
        public DrawArea Owner;
        public ContextMenu mnuObj;
        public MenuItem mnuProp;
        public MenuItem mnuDel;
        public string guidCurContext;
        public DrawArea.DrawToolType eCurTool;
        public FormExplorObj foexplor = null;
        public LayerList[] oLayers;

        public delegate void EVALCRITERES(Effectif oEffectif, bool bOption);
        public EVALCRITERES EvalCriteres;

        public delegate ToolAxes.RetourNiveau CREATNIVEAU(DrawPtNiveau oPtNiveau, bool bOption);
        public CREATNIVEAU CreatNiveau;
        
        public delegate string FONCEVAL(string sObjet, string sGuid);
        public FONCEVAL foncEval;

        public ArrayList lstFonctionEval;

        public int getIndexEval(string sFoncEval)
        {
            for (int i = 0; i < lstFonctionEval.Count; i++)
            {
                if (sFoncEval == lstFonctionEval[i]) return i;
            }
            return -1;
        }

        public virtual void initEval(string sFoncEval)
        {
        }

        /// <summary>
        /// Left nous button is pressed
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public virtual void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
        }

        protected void initContextMenu()
        {
            mnuProp = new MenuItem();
            mnuProp.Index = 0;
            mnuProp.Text = "Properties";
            mnuProp.Click += mnuProp_Click;
            mnuDel = new MenuItem();
            mnuDel.Index = 1;
            mnuDel.Text = "Delete";
            mnuDel.Click += mnuDel_Click;
        }


        /// <summary>
        /// Mouse is moved, left mouse button is pressed or none button is pressed
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public virtual void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
        {
        }


        /// <summary>
        /// Left mouse button is released
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public virtual void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
        }

        public virtual int GetIndexObjetFromPoint(Point pt)
        {
            for (int i = 0; i < Owner.Owner.drawArea.GraphicsList.Count; i++)
            {
                DrawObject o = Owner.Owner.drawArea.GraphicsList[i];
                if (o.PointInObject(pt)) return i; 
            }
            return -1;
        }

        public ArrayList DefauftNivQuery(string GuidObj, bool bOption)
        {
            
            Owner.Owner.oCnxBase.SWwriteLog(10, "Ajout dans la liste de l'objet en cours: " + GuidObj, true);
            ArrayList lst = new ArrayList();
            lst.Add(new ObjectAndNiveau(GuidObj, 0, null));
            return lst;
        }

        public virtual ArrayList FinCommercialForAppQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList SupportForAppQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList ExpertiseForAppQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList ComplexiteForAppQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList ImpactForAppQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList ObsolescenceForAppQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList BusinessForAppQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList InstanceForAppQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList ImpactBusinessForAppQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList CriticiteForAppQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList CoutForAppQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList SecuriteForAppQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }

        public virtual ArrayList FinCommercialForServerQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList SupportForServerQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList ComplexiteForServerQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList ImpactForServerQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList ExpertiseForServerQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList ObsolescenceForServerQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList BusinessForServerQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList InstanceForServerQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList ImpactBusinessForServerQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList CriticiteForServerQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList CoutForServerQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList SecuriteForServerQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }

        public virtual ArrayList FinCommercialForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList SupportForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList ComplexiteForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList ImpactForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList ExpertiseForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList ObsolescenceForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList BusinessForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList InstanceForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList ImpactBusinessForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList CriticiteForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList CoutForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }
        public virtual ArrayList SecuriteForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivQuery(GuidObj, bOption); }


        void mnuProp_Click(object sender, EventArgs e)
        {
            if (guidCurContext != null)
            {
                string sObj = eCurTool.ToString();
                Owner.Owner.oCnxBase.CBRecherche("Select Nom" + sObj + " From " + sObj + " Where Guid" + sObj + " = '" + guidCurContext + "'");
                if (Owner.Owner.oCnxBase.Reader.Read())
                {
                    string sNom = Owner.Owner.oCnxBase.Reader.GetString(0);
                    Owner.Owner.oCnxBase.CBReaderClose();
                    MessageBox.Show("propriete : " + sNom);
                }
                else Owner.Owner.oCnxBase.CBReaderClose();
            }
            guidCurContext = null;
            eCurTool = DrawArea.DrawToolType.Pointer;
            //throw new NotImplementedException();
        }

        void mnuDel_Click(object sender, EventArgs e)
        {           
            string Select, From, Where;
            DrawTools.CnxBase ocnx =Owner.Owner.oCnxBase;
            string sTable = GetTypeSimpleTable();
            string sCurGuidObj = Owner.Owner.oCureo.GuidObj.ToString();

            Table t;
            int n = ocnx.ConfDB.FindTable(sTable);
            if (n > -1)
            {
                t = (Table)ocnx.ConfDB.LstTable[n];
                Select = "SELECT " + GetSimpleSelect(t);
                From = GetSimpleFrom(sTable);
                Where = GetSimpleWhere(sTable, sCurGuidObj);
                if (ocnx.CBRecherche(Select + " " + From + " " + Where))
                {
                    string msg = "Voulez-vous supprimer de la base l'objet : " + ocnx.Reader.GetString(1);
                    ocnx.CBReaderClose();
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    result = MessageBox.Show(msg, "suppression", buttons);

                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        Owner.Owner.oCureo.tn.Remove();
                        ocnx.CBWrite("DELETE " + From + " " + Where); //ROM DansTypeVue Where GuidObjet = '" + Owner.Owner.sCurGuidObj + "'");
                    }

                }
                
            }
            ocnx.CBReaderClose();

            Owner.Owner.oCureo = null;
            guidCurContext = null;
            eCurTool = DrawArea.DrawToolType.Pointer;
            //throw new NotImplementedException();
        }

        public virtual ArrayList LstQuery(string Query)
        {
            ArrayList lst = new ArrayList();
            Owner.Owner.oCnxBase.CBRecherche(Query);
            while (Owner.Owner.oCnxBase.Reader.Read())
            {
                Owner.Owner.oCnxBase.SWwriteLog(10, "Ajout dans la liste de l'objet en cours: " + Owner.Owner.oCnxBase.Reader.GetString(1) + "     (" + Owner.Owner.oCnxBase.Reader.GetString(0) + ")", false);
                lst.Add(new ObjectAndNiveau(Owner.Owner.oCnxBase.Reader.GetString(0), 0, null));
            }
            Owner.Owner.oCnxBase.CBReaderClose();
            return lst;
        }

        public virtual ArrayList LstQueryCout(string Query)
        {
            ArrayList lst = new ArrayList();
            Owner.Owner.oCnxBase.CBRecherche(Query);
            while (Owner.Owner.oCnxBase.Reader.Read())
            {
                Owner.Owner.oCnxBase.SWwriteLog(10, "Ajout dans la liste de l'objet en cours: " + Owner.Owner.oCnxBase.Reader.GetString(1) + "     (" + Owner.Owner.oCnxBase.Reader.GetString(0) + ")", false);
                lst.Add(new ObjectAndNiveau(Owner.Owner.oCnxBase.Reader.GetString(0), 0, new CoutParam(Owner.Owner.oCnxBase.Reader.GetDouble(2), Owner.Owner.oCnxBase.Reader.GetDouble(3), Owner.Owner.oCnxBase.Reader.GetDouble(4), Owner.Owner.oCnxBase.Reader.GetDouble(5))));
            }
            Owner.Owner.oCnxBase.CBReaderClose();
            return lst;
        }

        public virtual void CreatNiveauForApp(Effectif oEffectif, bool bOption)
        {
            ArrayList lst = null;

            for (int i = 0; i < oEffectif.lstNivEffectif.Count; i++)
            {
                Niveau oNiv = (Niveau)oEffectif.lstNivEffectif[i];
                if (oNiv != null)
                {
                    Owner.Owner.oCnxBase.SWwriteLog(6, "Debut des calculs du critere " + oNiv.NomNiveau + " sur les objets " + GetType().Name.Substring("Tool".Length), true);
                    lst = oNiv.GetQueryForApp(this, oEffectif.GuidEffectif, bOption);
                    for (int j = 0; j < lst.Count; j++)
                    {
                        CalcIndicator((ObjectAndNiveau)lst[j], oNiv);
                    }
                    oNiv.PostCalcul();
                    Owner.Owner.oCnxBase.SWwriteLog(6, "", true);
                }
            }
        }

        /*public virtual ToolAxes.RetourNiveau CreatNiveauForApp(DrawPtNiveau oPtNiveau, bool bOption)
        {
            ToolAxes.RetourNiveau rn = ToolAxes.RetourNiveau.Vide;
            ArrayList lst = null;

            Owner.Owner.oCnxBase.SWwriteLog(6, "Debut des calculs du Niveau " + oPtNiveau.NivAbs[0].NomNiveau + " sur les objets " + GetType().Name.Substring("Tool".Length), true);
            
            Owner.Owner.oCnxBase.SWwriteLog(8, "Creation de la liste des objets " + GetType().Name.Substring("Tool".Length) + " pour le Niveau Abscisse", true);
            lst = oPtNiveau.NivAbs[0].GetQueryForApp(this, oPtNiveau.GuidObj, bOption);
            
            for (int i = 0; i < lst.Count; i++)
            {
                if (CalcIndicator((ObjectAndNiveau)lst[i], oPtNiveau.NivAbs[0]))
                {
                    if (oPtNiveau.NivAbs[1] != null)
                    {
                        if (CalcIndicator((ObjectAndNiveau)lst[i], oPtNiveau.NivAbs[1])) rn |= ToolAxes.RetourNiveau.Absisse;
                    }
                    else rn |= ToolAxes.RetourNiveau.Absisse;
                }
            }

            oPtNiveau.NivAbs[0].PostCalcul();

            Owner.Owner.oCnxBase.SWwriteLog(8, "Creation de la liste des objets " + GetType().Name.Substring("Tool".Length) + " pour le Niveau Ordonnee", true);

            lst = oPtNiveau.NivOrd[0].GetQueryForApp(this, oPtNiveau.GuidObj, bOption);
            
            for (int i = 0; i < lst.Count; i++)
            {

                if (CalcIndicator((ObjectAndNiveau)lst[i], oPtNiveau.NivOrd[0]))
                {
                    if (oPtNiveau.NivOrd[1] != null)
                    {
                        if (CalcIndicator((ObjectAndNiveau)lst[i], oPtNiveau.NivOrd[1])) rn |= ToolAxes.RetourNiveau.Ordonnee;
                    }
                    else rn |= ToolAxes.RetourNiveau.Ordonnee;
                }
            }

            oPtNiveau.NivOrd[0].PostCalcul();

            return rn;
        }*/

        public virtual void CreatNiveauForServer(Effectif oEffectif, bool bOption)
        {
            ArrayList lst = null;

            for (int i = 0; i < oEffectif.lstNivEffectif.Count; i++)
            {
                Niveau oNiv = (Niveau)oEffectif.lstNivEffectif[i];
                if (oNiv != null)
                {
                    Owner.Owner.oCnxBase.SWwriteLog(6, "Debut des calculs du critere " + oNiv.NomNiveau + " sur les objets " + GetType().Name.Substring("Tool".Length), true);
                    lst = oNiv.GetQueryForServer(this, oEffectif.GuidEffectif, bOption);
                    for (int j = 0; j < lst.Count; j++)
                    {
                        CalcIndicator((ObjectAndNiveau)lst[j], oNiv);
                    }
                    oNiv.PostCalcul();
                    Owner.Owner.oCnxBase.SWwriteLog(6, "", true);
                }
            }
        }

        /*public virtual ToolAxes.RetourNiveau CreatNiveauForServer(DrawPtNiveau oPtNiveau, bool bOption)
        {
            ToolAxes.RetourNiveau rn = ToolAxes.RetourNiveau.Vide;
            ArrayList lst = null;

            Owner.Owner.oCnxBase.SWwriteLog(0,"Debut des calculs du Niveau " + oPtNiveau.NivAbs[0].NomNiveau + " sur les objets serveurs", false);

            lst = oPtNiveau.NivAbs[0].GetQueryForServer(this, oPtNiveau.GuidObj, bOption);
            for (int i = 0; i < lst.Count; i++)
            {
                if (CalcIndicator((ObjectAndNiveau)lst[i], oPtNiveau.NivAbs[0]))
                {
                    if (oPtNiveau.NivAbs[1] != null)
                    {
                        if (CalcIndicator((ObjectAndNiveau)lst[i], oPtNiveau.NivAbs[1])) rn |= ToolAxes.RetourNiveau.Absisse;
                    }
                    else rn |= ToolAxes.RetourNiveau.Absisse;
                }
            }
            oPtNiveau.NivAbs[0].PostCalcul();

            lst = oPtNiveau.NivOrd[0].GetQueryForServer(this, oPtNiveau.GuidObj, bOption);
            for (int i = 0; i < lst.Count; i++)
            {

                if (CalcIndicator((ObjectAndNiveau)lst[i], oPtNiveau.NivOrd[0]))
                {
                    if (oPtNiveau.NivOrd[1] != null)
                    {
                        if (CalcIndicator((ObjectAndNiveau)lst[i], oPtNiveau.NivOrd[1])) rn |= ToolAxes.RetourNiveau.Ordonnee;
                    }
                    else rn |= ToolAxes.RetourNiveau.Ordonnee;
                }
            }
            oPtNiveau.NivOrd[0].PostCalcul();

            return rn;
        }*/


        public virtual void CreatNiveauForTechno(Effectif oEffectif, bool bOption)
        {
            ArrayList lst = null;

            for (int i = 0; i < oEffectif.lstNivEffectif.Count; i++)
            {
                Niveau oNiv = (Niveau)oEffectif.lstNivEffectif[i];
                if (oNiv != null)
                {
                    Owner.Owner.oCnxBase.SWwriteLog(6, "Debut des calculs du critere " + oNiv.NomNiveau + " sur les objets " + GetType().Name.Substring("Tool".Length), true);
                    lst = oNiv.GetQueryForTechno(this, oEffectif.GuidEffectif, bOption);
                    for (int j = 0; j < lst.Count; j++)
                    {
                        CalcIndicator((ObjectAndNiveau)lst[j], oNiv);
                    }
                    oNiv.PostCalcul();
                    Owner.Owner.oCnxBase.SWwriteLog(6, "", true);
                }
            }
        }
        /*public virtual ToolAxes.RetourNiveau CreatNiveauForTechno(DrawPtNiveau oPtNiveau, bool bOption)
        {
            ToolAxes.RetourNiveau rn = ToolAxes.RetourNiveau.Vide;
            ArrayList lst = null;

            Owner.Owner.oCnxBase.SWwriteLog(0, "Debut des calculs du Niveau " + oPtNiveau.NivAbs[0].NomNiveau + " sur les objets serveurs", false);

            lst = oPtNiveau.NivAbs[0].GetQueryForTechno(this, oPtNiveau.GuidObj, bOption);
            for (int i = 0; i < lst.Count; i++)
            {
                if (CalcIndicator((ObjectAndNiveau)lst[i], oPtNiveau.NivAbs[0]))
                {
                    if (oPtNiveau.NivAbs[1] != null)
                    {
                        if (CalcIndicator((ObjectAndNiveau)lst[i], oPtNiveau.NivAbs[1])) rn |= ToolAxes.RetourNiveau.Absisse;
                    }
                    else rn |= ToolAxes.RetourNiveau.Absisse;
                }
            }
            oPtNiveau.NivAbs[0].PostCalcul();

            lst = oPtNiveau.NivOrd[0].GetQueryForTechno(this, oPtNiveau.GuidObj, bOption);
            for (int i = 0; i < lst.Count; i++)
            {

                if (CalcIndicator((ObjectAndNiveau)lst[i], oPtNiveau.NivOrd[0]))
                {
                    if (oPtNiveau.NivOrd[1] != null)
                    {
                        if (CalcIndicator((ObjectAndNiveau)lst[i], oPtNiveau.NivOrd[1])) rn |= ToolAxes.RetourNiveau.Ordonnee;
                    }
                    else rn |= ToolAxes.RetourNiveau.Ordonnee;
                }
            }
            oPtNiveau.NivOrd[0].PostCalcul();

            return rn;
        }*/

        public virtual bool CalcIndicator(ObjectAndNiveau objniv, Niveau Niv)
        {
            return false;
        }
        
        public virtual void InitToolTip()
        {
            //prevu uniquement pour toolpointer
        }

        public virtual string GetSelect(string sTable, string sGTable)
        {
            Table t, tg;
            ConfDataBase ConfDB = Owner.Owner.oCnxBase.ConfDB;
            int n = ConfDB.FindTable(sTable);
            int m = ConfDB.FindTable(sGTable);
            if (n > -1 && m > -1)
            {
                t = (Table)ConfDB.LstTable[n];
                tg = (Table)ConfDB.LstTable[m];
                string selectO = t.GetSelectField(ConfDataBase.FieldOption.InterneBD);
                //string selectO = t.GetSelectField(ConfDataBase.FieldOption.Base);
                string selectGO = tg.GetSelectField(ConfDataBase.FieldOption.Base);
                if (selectO == "") return "SELECT " + selectGO;
                if (selectGO == "") return "SELECT " + selectO;
                return "SELECT Distinct " + selectO + ", " + selectGO; // +", GuidLayer";
            }

            return null;
        }

        /*
        public virtual string GetWhereLayer()
        {
            string sWhereLayer = " and ( ";
            int deb=1;
            bool bDebOr = false;

            if (Owner.Owner.sLayers != "")
            {
                string[] aValue = Owner.Owner.sLayers.Split('(', ')');
                if (aValue.Length>1 && aValue[1] == "null")
                {
                    sWhereLayer = " and (GuidLayer is null ";
                    deb=3;
                    bDebOr = true;
                }
                for (int i = deb; i < aValue.Length; i += 2)
                {
                    sWhereLayer += (bDebOr ? "or " : " ") + "GuidLayer='" + aValue[i] + "' ";
                    bDebOr = true;
                }
            }
            sWhereLayer += ")";
            return sWhereLayer;
        }*/

        public virtual string GetKey(string sTable, Table t = null)
        {
            ConfDataBase ConfDB = Owner.Owner.oCnxBase.ConfDB;
            if (t == null)
            {
                int n = ConfDB.FindTable(sTable);

                if (n > -1)
                    t = (Table)ConfDB.LstTable[n];
            }

            if (t != null)
            {
                string Key = t.GetKey();
                if (Key.Length == 0) return ((Field)t.LstField[0]).Name;
                return Key;
            }
            return null;
        }

        public virtual string GetFrom(string sType, string sGType)
        {
            return null;
        }

        public virtual string GetSimpleFrom(string sTable)
        {
            /*int n = Owner.Owner.oCnxBase.ConfDB.FindFieldFromLib(sTable, "HyperLien");
            if (n > -1) 
                return "FROM " + sTable + ", Comment";
            else*/ 
                return "FROM " + sTable;
        }

        public virtual string GetFromG(string sType)
        {
            return "From DansVue, G" + sType;
            //return base.GetFrom(sType);
        }

        public virtual string GetWhere(string sType, string sGType, string GuidGVue, string GuidVueSrvPhy)
        {
            return null;
        }

        public virtual string GetSimpleWhere(string sTable, List<string> lstKey, Table t = null)
        {
            string sKey = GetKey(sTable, t);
            string sWhere = "";
            string[] aKey = sKey.Split(',');
            if (aKey.Length == lstKey.Count)
            {
                for (int i = 0; i < aKey.Length; i++)
                {
                    sWhere += " and " + sTable + "." + aKey[i] + "='" + lstKey[i] + "'";
                }
                return "Where " + sWhere.Substring(4);
            }

            /*int n = Owner.Owner.oCnxBase.ConfDB.FindFieldFromLib(sTable, "HyperLien");
            if (n > -1) 
                return "WHERE Guid" + sTable + "='" + GuidObjet + "' AND " + sTable + ".GuidComment=Comment.GuidComment";
            else */
            //return "WHERE Guid" + sTable + "='" + GuidObjet + "'";
            return sWhere;
        }

        public virtual string GetSimpleWhere(string sTable, string GuidObjet)
        {
            /*int n = Owner.Owner.oCnxBase.ConfDB.FindFieldFromLib(sTable, "HyperLien");
            if (n > -1) 
                return "WHERE Guid" + sTable + "='" + GuidObjet + "' AND " + sTable + ".GuidComment=Comment.GuidComment";
            else */
                return "WHERE Guid" + sTable + "='" + GuidObjet + "'";
        }

        public virtual string GetWhereG(string sType, string GuidGVue)
        {
            return "WHERE GuidGVue ='" + GuidGVue + "' and GuidObjet=GuidG" + sType;
            //return base.GetWhere(sType);
        }

        public virtual void ExpandObj(FormExplorObj feo, ExpObj eo)
        {
        }

        public virtual void ExpandObjRoot(int deb, int fin, FormExplorObj feo, ExpObj eo, string[] ssCategorie)
        {
            for (int i = deb; i <= fin; i++)
            {
                Guid g = Guid.NewGuid();
                TreeNode t = eo.tn.Nodes.Add(g.ToString(), ssCategorie[i]);
                int n = eo.tn.Nodes.Count;
                eo.tn.Nodes[n - 1].ForeColor = Color.Gray;
                ExpObj eobj = new ExpObj(g, eo.GuidObj, eo.ObjTool, i, t);
                feo.lstObj.Add((object)eobj);
            }
        }

        public virtual void LoadTemplate(ArrayList lstTemplate)
        {
            oLayers[0].Clear();
            for (int i = 0; i < lstTemplate.Count; i++)
            {
                string[] aValue = ((string)lstTemplate[i]).Split(';');
                
                oLayers[0].AddTemplate(Owner.Owner, aValue[0], aValue[1]);
            }

        }

        public virtual int GetTemplate(string GuidLayer, int index=0)
        {
            if (GuidLayer != null && GuidLayer != "" && oLayers[0].lstTemplate.Count > 1)
            {
                for (int i = 1; i < oLayers[0].lstTemplate.Count; i++)
                {
                    TemplateDt oTdt = (TemplateDt)oLayers[index].lstTemplate[i];
                    if (oTdt.GuidLayer == GuidLayer) return i;
                }
            }
            return 0;
        }

        public virtual void LoadObject(char typeData, string sGuidgvue, string sData)
        {
            string Select, From, Where;
            string sType = GetTypeSimpleTable();
            string sGType = GetTypeSimpleGTable();
            //string sGType = GetTypeSimpleTable();
                           
            CnxBase ocnx = Owner.Owner.oCnxBase;

            Select = GetSelect(sType, sGType);
            From = GetFrom(sType, sGType);
            Where = GetWhere(sType, sGType, sGuidgvue, sData);
            if (ocnx.CBRecherche(Select + " " + From + " " + Where))
            {
                while (ocnx.Reader.Read())
                {
                    CreatObjetsFromBD(false, ConfDataBase.FieldOption.Select);
                }
                ocnx.CBReaderClose();
            }
            else ocnx.CBReaderClose();
        }

        public virtual void LoadObjectSansGraph(string Where="")
        {
                        
            string Select, From;
            string sType = GetTypeSimpleTable();
            CnxBase ocnx = Owner.Owner.oCnxBase;
            Table t;

            int n = ocnx.ConfDB.FindTable(sType);
            if (n > -1)
            {
                t = (Table)ocnx.ConfDB.LstTable[n];
                Select = "Select " + GetSimpleSelect(t);
                From = GetSimpleFrom(sType);
                if (ocnx.CBRecherche(Select + " " + From + " " + Where))
                {
                    while (ocnx.Reader.Read())
                    {
                        CreatObjetsFromBDSansGraph(false, t);
                    }
                    ocnx.CBReaderClose();
                }
                else ocnx.CBReaderClose();
            }
        }

        public virtual void LoadObjecttoList(string Where = "")
        {
            string Select, From;
            string sType = GetTypeSimpleTable();
            CnxBase ocnx = Owner.Owner.oCnxBase;
            Table t;

            int n = ocnx.ConfDB.FindTable(sType);
            if (n > -1)
            {
                t = (Table)ocnx.ConfDB.LstTable[n];
                Select = "Select " + GetSimpleSelect(t);
                From = GetSimpleFrom(sType);
                if (ocnx.CBRecherche(Select + " " + From + " " + Where))
                {
                    while (ocnx.Reader.Read())
                    {
                        CreatObjetsFromBDtoList(false);
                    }
                    ocnx.CBReaderClose();
                }
                else ocnx.CBReaderClose();
            }
        }

        public virtual string GetTypeSimpleTable()
        {
            return GetType().Name.Substring("Tool".Length);
        }

        public virtual string GetTypeSimpleGTable()
        {
            return "G" + GetType().Name.Substring("Tool".Length);
        }

        public virtual void CreatObjetFromCadreRef(int iIndicator)
        {
        }

        public virtual void CreatObjetFromCadreRef(ArrayList aIndicator)
        {
        }

        public virtual string GetSimpleSelect(Table t) {
            return t.GetSelectField(ConfDataBase.FieldOption.NomCourt);
        }

        public virtual string GetSimpleInterneBD(Table t)
        {
            return t.GetSelectField(ConfDataBase.FieldOption.NomCourt | ConfDataBase.FieldOption.InterneBD);
        }

        public virtual void LoadSimpleObject(string GuidObjet)
        {
            string Select, From, Where;
            string sType = GetTypeSimpleTable();
            //string sGType = GetTypeSimpleGTable();
            CnxBase ocnx = Owner.Owner.oCnxBase;


            Table t;
            int n = ocnx.ConfDB.FindTable(sType);
            if (n > -1)
            {
                t = (Table)ocnx.ConfDB.LstTable[n];
                Select = "SELECT " + GetSimpleSelect(t);
                From = GetSimpleFrom(sType);
                Where = GetSimpleWhere(sType, GuidObjet);
                if (ocnx.CBRecherche(Select + " " + From + " " + Where))
                {
                    if(ocnx.Reader.Read())
                    {
                        CreatObjetsFromBD(true, ConfDataBase.FieldOption.Select);
                    }
                    else ocnx.CBReaderClose();
                }
                else ocnx.CBReaderClose();
            }
        }

        public virtual void LoadSimpleObjectSansGraph(ExpObj exObj)
        {
            string Select, From, Where;
            string sType = GetTypeSimpleTable();
            //string sGType = GetTypeSimpleGTable();
            CnxBase ocnx = Owner.Owner.oCnxBase;


            Table t;
            int n = ocnx.ConfDB.FindTable(sType);
            if (n > -1)
            {
                t = (Table)ocnx.ConfDB.LstTable[n];
                Select = "SELECT " + GetSimpleSelect(t);
                From = GetSimpleFrom(sType);
                Where = GetSimpleWhere(sType, exObj.GuidObj.ToString());
                if (ocnx.CBRecherche(Select + " " + From + " " + Where))
                {
                    if (ocnx.Reader.Read())
                    {
                        CreatObjetsFromBDSansGraph(true, t);
                    }
                    else ocnx.CBReaderClose();
                }
                else ocnx.CBReaderClose();
            }

            // Label
            if(exObj.oDraw != null)
            {
                exObj.oDraw.GetLabelLinks();
            }
        }

        public virtual Table GetTable(string sType, string sFindKeyForDynamicTable = null)
        {
            CnxBase ocnx = Owner.Owner.oCnxBase;

            int n = ocnx.ConfDB.FindTable(sType);
            if (n > -1) return (Table)ocnx.ConfDB.LstTable[n];
            return null;
        }

        public virtual void LoadSimpleObjectSansGraph(List<string> lstKey, Table t)
        {
            string Select, From, Where;
            string sType = GetTypeSimpleTable();
            //string sGType = GetTypeSimpleGTable();
            CnxBase ocnx = Owner.Owner.oCnxBase;

            if (t != null)
            {
                Select = "SELECT " + GetSimpleSelect(t);
                From = GetSimpleFrom(sType);
                Where = GetSimpleWhere(sType, lstKey);
                if (ocnx.CBRecherche(Select + " " + From + " " + Where))
                {
                    if (ocnx.Reader.Read())
                    {
                        CreatObjetsFromBDSansGraph(true, t);
                    }
                    ocnx.CBReaderClose();
                }
                else ocnx.CBReaderClose();
            }
        }


        public virtual void LoadSimpleObjectSansGraph(List<string> lstKey)
        {
            string Select, From, Where;
            string sType = GetTypeSimpleTable();
            //string sGType = GetTypeSimpleGTable();
            CnxBase ocnx = Owner.Owner.oCnxBase;

            Table t = GetTable(sType, lstKey[0]);
            if (t != null)
            {
                Select = "SELECT " + GetSimpleSelect(t);
                From = GetSimpleFrom(sType);
                Where = GetSimpleWhere(sType, lstKey);
                if (ocnx.CBRecherche(Select + " " + From + " " + Where))
                {
                    if (ocnx.Reader.Read())
                    {
                        CreatObjetsFromBDSansGraph(true, t);
                    }
                    ocnx.CBReaderClose();
                }
                else ocnx.CBReaderClose();
            }
        }

    

        public virtual string GetsType(bool Reel)
        {
            //return GetType().Name.Substring("Tool".Length);
            return GetTypeSimpleTable();
        }

        public virtual void AddNewObjectFromDraw(DrawArea drawArea, DrawObject o, bool selected)
        {
        }

        public virtual void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
        }

        public virtual DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            return null;
        }

        public virtual void CreatObjetFromXml(ArrayList LstValue, ArrayList LstValueG)
        {
            CreatObjetFromBD(LstValue, LstValueG);
        }

        public virtual void CreatObjetFromBD(ArrayList LstValue)
        {
        }

        public virtual void CreatObjetFromBDtoList(ArrayList LstValue)
        {
        }

        public virtual void LoadObjectXml(XmlNode Node)
        {
            Table t, tg;
            ArrayList LstValue;
            ArrayList LstValueG;
            ArrayList aGNode = new ArrayList();
            ConfDataBase ConfDB = Owner.Owner.oCnxBase.ConfDB;
            string sTablen = GetTypeSimpleTable();
            string sTablem = GetTypeSimpleGTable();
            int n = ConfDB.FindTable(sTablen);
            int m = ConfDB.FindTable(sTablem);
            aGNode = Owner.Owner.GetNode(Node, sTablem);

            if (n > -1 && m > -1)
            {
                t = (Table)ConfDB.LstTable[n];
                tg = (Table)ConfDB.LstTable[m];
                LstValue = t.InitValueFieldFromXmlNode(Node);

                if (aGNode.Count == 0) aGNode.Add(Node);
                for (int i = 0; i < aGNode.Count; i++)
                {
                    LstValueG = tg.InitValueFieldFromXmlNode((XmlNode)aGNode[i]);
                    CreatObjetFromXml(LstValue, LstValueG);
                }
            }
        }

        public virtual void CreatObjetsFromBD(bool CloseReader, ConfDataBase.FieldOption ConfField)
        {
            Table t, tg;
            ArrayList LstValue;
            ArrayList LstValueG;
            ConfDataBase ConfDB = Owner.Owner.oCnxBase.ConfDB;
            OdbcDataReader Reader = Owner.Owner.oCnxBase.Reader;

            string sTablen = GetTypeSimpleTable();
            string sTablem = GetTypeSimpleGTable();

            int n = ConfDB.FindTable(sTablen);
            //int m = ConfDB.FindTable("G" + sTablem);
            int m = ConfDB.FindTable(sTablem);
            if (n > -1 && m > -1)
            {
                t = (Table)ConfDB.LstTable[n];
                LstValue = t.InitValueFieldFromBD(Reader, ConfField);

                tg = (Table)ConfDB.LstTable[m];
                LstValueG = tg.InitValueFieldFromBD(Reader, ConfField);
                if (CloseReader) Reader.Close();
                CreatObjetFromBD(LstValue, LstValueG);
            }
            if (CloseReader) Reader.Close();
        }

        public virtual void CreatObjetsFromBDSansGraph(bool CloseReader, Table t)
        {
            ArrayList LstValue;
            ConfDataBase ConfDB = Owner.Owner.oCnxBase.ConfDB;
            OdbcDataReader Reader = Owner.Owner.oCnxBase.Reader;

            string sTablen = GetTypeSimpleTable();

            //int n = ConfDB.FindTable(sTablen);
            //if (n > -1)
            //{
                //t = (Table)ConfDB.LstTable[n];
                LstValue = t.InitValueFieldFromBD(Reader, ConfDataBase.FieldOption.Select);

                if (CloseReader) Reader.Close();
                CreatObjetFromBD(LstValue);
            //}
            if (CloseReader) Reader.Close();
        }

        public virtual void CreatObjetsFromBDtoList(bool CloseReader)
        {
            Table t;
            ArrayList LstValue;
            ConfDataBase ConfDB = Owner.Owner.oCnxBase.ConfDB;
            OdbcDataReader Reader = Owner.Owner.oCnxBase.Reader;

            string sTablen = GetTypeSimpleTable();

            int n = ConfDB.FindTable(sTablen);
            if (n > -1)
            {
                t = (Table)ConfDB.LstTable[n];
                LstValue = t.InitValueFieldFromBD(Reader, ConfDataBase.FieldOption.Select);

                if (CloseReader) Reader.Close();
                CreatObjetFromBDtoList(LstValue);
            }
            if (CloseReader) Reader.Close();
        }

        public virtual void RemoveLink(DrawObject d, string NomObjaLier, DrawObject.TypeAttach firstLink, DrawObject.TypeAttach secondLink)
        {
            int n = -1;
            object o = d.GetValueFromName(NomObjaLier);
            if (o != null && (string)o != "")
            {
                n = Owner.GraphicsList.FindObjet(0, (string)o);
                if (n > -1)
                {
                    d.RemoveLink(Owner.GraphicsList[n], firstLink);
                    Owner.GraphicsList[n].RemoveLink(d, secondLink);
                }
            }
        }

        public virtual int CreatObjetLink(DrawObject d, string NomObjaLier, DrawObject.TypeAttach firstLink, DrawObject.TypeAttach secondLink)
        {
            int n = -1;
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

        
    }

    public class ToolApplicationClass : DrawTools.Tool
    {
        public ToolApplicationClass(DrawArea da)
        {
            Owner = da;
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawApplicationClass da;

            da = new DrawApplicationClass(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(da);
        }
    }

    public class ToolApplicationType : DrawTools.Tool
    {
        public ToolApplicationType(DrawArea da)
        {
            Owner = da;
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawApplicationType da;

            da = new DrawApplicationType(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(da);
        }
    }

    public class ToolArborescence : DrawTools.Tool
    {
        public ToolArborescence(DrawArea da)
		{
            Owner = da;
		}

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawArborescence da;

            da = new DrawArborescence(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(da);
        }
    }

    
    public class ToolBackupClass : DrawTools.Tool
    {
        public ToolBackupClass(DrawArea da)
        {
            Owner = da;
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawBackupClass db;

            db = new DrawBackupClass(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(db);
        }
    }

    public class ToolCadreRef : DrawTools.Tool
    {
        public ToolCadreRef(DrawArea da)
        {
            Owner = da;
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawCadreRef dc;

            dc = new DrawCadreRef(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(dc);
        }
    }


    public class ToolCadreRefApp : DrawTools.Tool
    {
        public ToolCadreRefApp(DrawArea da)
        {
            Owner = da;
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawCadreRefApp dc;

            dc = new DrawCadreRefApp(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(dc);
        }
    }

    public class ToolCadreRefFonc : DrawTools.Tool
    {
        public ToolCadreRefFonc(DrawArea da)
        {
            Owner = da;
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawCadreRefFonc dc;

            dc = new DrawCadreRefFonc(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(dc);
        }
    }

    public class ToolDiskClass : DrawTools.Tool
    {
        public ToolDiskClass(DrawArea da)
        {
            Owner = da;
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawDiskClass db;

            db = new DrawDiskClass(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(db);
        }
    }

    public class ToolEnvironnement : DrawTools.Tool
    {
        public ToolEnvironnement(DrawArea da)
        {
            Owner = da;
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawEnvironnement de;

            de = new DrawEnvironnement(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(de);
        }
    }
    public class ToolExploitClass : DrawTools.Tool
    {
        public ToolExploitClass(DrawArea da)
        {
            Owner = da;
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawExploitClass de;

            de = new DrawExploitClass(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(de);
        }
    }

    public class ToolFonctionService : DrawTools.Tool
    {
        public ToolFonctionService(DrawArea da)
        {
            Owner = da;
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawFonctionService de;

            de = new DrawFonctionService(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(de);
        }
    }

    public class ToolGroupService : DrawTools.Tool
    {
        public ToolGroupService(DrawArea da)
        {
            Owner = da;
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawGroupService ds;

            ds = new DrawGroupService(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(ds);
        }
    }
    
    public class ToolOptionsDraw : DrawTools.Tool
    {
        public ToolOptionsDraw(DrawArea da)
        {
            Owner = da;
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawOptionsDraw dd;

            dd = new DrawOptionsDraw(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(dd);
        }
    }

    public class ToolTechnoArea : DrawTools.Tool
    {
        public ToolTechnoArea(DrawArea da)
        {
            Owner = da;
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawTechnoArea dt;

            dt = new DrawTechnoArea(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(dt);
        }
    }

    public class ToolProduitApp : DrawTools.Tool
    {
        public ToolProduitApp(DrawArea da)
        {
            Owner = da;
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawProduitApp dp;

            dp = new DrawProduitApp(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(dp);
        }
    }


    public class ToolService : DrawTools.Tool
    {
        public ToolService(DrawArea da)
        {
            Owner = da;
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawService ds;

            ds = new DrawService(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(ds);
        }
    }

    public class ToolServiceLink : DrawTools.Tool
    {
        public ToolServiceLink(DrawArea da)
        {
            Owner = da;
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawServiceLink ds;

            ds = new DrawServiceLink(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(ds);
        }
    }
    public class ToolStaticTable : DrawTools.Tool
    {
        public ToolStaticTable(DrawArea da)
        {
            Owner = da;
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawStaticTable ds;

            ds = new DrawStaticTable(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(ds);
        }
    }

    public class ToolStatut : DrawTools.Tool
    {
        public ToolStatut(DrawArea da)
        {
            Owner = da;
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawStatut ds;

            ds = new DrawStatut(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(ds);
        }
    }

    public class ToolTemplate : DrawTools.Tool
    {
        public ToolTemplate(DrawArea da)
        {
            Owner = da;
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawTemplate dt;

            dt = new DrawTemplate(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(dt);
        }
    }

    public class ToolLayer : DrawTools.Tool
    {
        public ToolLayer(DrawArea da)
        {
            Owner = da;
        }

        public override string GetSimpleSelect(Table t)
        {
            return t.GetSelectField(ConfDataBase.FieldOption.Select);
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawLayer dl;
            
            dl = new DrawLayer(Owner.Owner, LstValue);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dl;
            else Owner.Owner.drawArea.GraphicsList.Add(dl);
        }
    }

    public class ToolLayerLink : DrawTools.Tool
    {
        public ToolLayerLink(DrawArea da)
        {
            Owner = da;
        }

        public override string GetSimpleSelect(Table t)
        {
            return t.GetSelectField(ConfDataBase.FieldOption.Select);
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawLayerLink dl;

            dl = new DrawLayerLink(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(dl);
        }
    }

    public class ToolAppVersion : DrawTools.Tool
    {
        public ToolAppVersion(DrawArea da)
        {
            Owner = da;
        }

        public override string GetSimpleSelect(Table t)
        {
            return t.GetSelectField(ConfDataBase.FieldOption.Select);
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawAppVersion av;

            av = new DrawAppVersion(Owner.Owner, LstValue);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = av;
            else Owner.Owner.drawArea.GraphicsList.Add(av);
        }
    }

    public class ToolTypeVue : DrawTools.Tool
    {
        public ToolTypeVue(DrawArea da)
        {
            Owner = da;
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawTypeVue dt;

            dt = new DrawTypeVue(Owner.Owner, LstValue);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dt;
            else Owner.Owner.drawArea.GraphicsList.Add(dt);
        }
    }

    public class ToolPackage : DrawTools.Tool
    {
        
        public ToolPackage(DrawArea da)
        {
            Owner = da;
        }

        public override string GetSimpleWhere(string sTable, string GuidObjet)
        {
            return "WHERE GuidMCompServ='"  + GuidObjet + "'";
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawPackage dp;

            dp = new DrawPackage(Owner.Owner, LstValue);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dp;
            else Owner.Owner.drawArea.GraphicsList.Add(dp);
        }
    }

    public class ToolPackageDynamic : DrawTools.Tool
    {
        public override void LoadSimpleObjectSansGraph(List<string> lstKey, Table t)
        {
            string Select, From, Where;
            string sType = GetTypeSimpleTable();
            //string sGType = GetTypeSimpleGTable();
            CnxBase ocnx = Owner.Owner.oCnxBase;

            if (t != null)
            {
                Select = "SELECT " + GetSimpleSelect(t);
                From = GetSimpleFrom(sType);
                Where = GetSimpleWhere(sType, lstKey, t);
                if (ocnx.CBRecherche(Select + " " + From + " " + Where))
                {
                    ArrayList LstValue = t.InitValue();
                    string sTablen = GetTypeSimpleTable();

                    while (ocnx.Reader.Read())
                    {
                        LstValue = t.CompleteValueFieldFromBD(ocnx.Reader, ConfDataBase.FieldOption.Select, LstValue);
                        int iParam = t.FindField(t.LstField, "GuidParam");
                        int iValue = t.FindField(t.LstField, "Value");
                        int iParamValue = t.FindField(t.LstField, (string)LstValue[iParam]);
                        LstValue[iParamValue] = LstValue[iValue];
                    }
                    CreatObjetFromBD(LstValue);
                    ocnx.CBReaderClose();
                }
                else ocnx.CBReaderClose();
            }
        }

        public override string GetTypeSimpleTable()
        {
            return "Package";
        }
        public override Table GetTable(string sType, string sFindKeyForDynamicTable)
        {
            Table t = new Table("PackageDynamic");
            CnxBase ocnx = Owner.Owner.oCnxBase;

            t.LstField.Add(new Field("GuidMCompServ", "GuidMCompServ", 's', 0, 0, ConfDataBase.FieldOption.InterneBD | ConfDataBase.FieldOption.ReadOnly | ConfDataBase.FieldOption.NonVisible | ConfDataBase.FieldOption.TabNonVisible | ConfDataBase.FieldOption.Select | ConfDataBase.FieldOption.Key | ConfDataBase.FieldOption.Mandatory));
            t.LstField.Add(new Field("GuidVue", "GuidVue", 's', 0, 0, ConfDataBase.FieldOption.InterneBD | ConfDataBase.FieldOption.ReadOnly | ConfDataBase.FieldOption.NonVisible | ConfDataBase.FieldOption.TabNonVisible | ConfDataBase.FieldOption.Select | ConfDataBase.FieldOption.Key | ConfDataBase.FieldOption.Mandatory));
            t.LstField.Add(new Field("GuidParam", "GuidParam", 's', 0, 0, ConfDataBase.FieldOption.InterneBD | ConfDataBase.FieldOption.ReadOnly | ConfDataBase.FieldOption.NonVisible | ConfDataBase.FieldOption.TabNonVisible | ConfDataBase.FieldOption.Select));
            t.LstField.Add(new Field("Value", "Value", 's', 0, 0, ConfDataBase.FieldOption.InterneBD | ConfDataBase.FieldOption.ReadOnly | ConfDataBase.FieldOption.NonVisible | ConfDataBase.FieldOption.TabNonVisible | ConfDataBase.FieldOption.Select));

            //recherche des champs dans les tables Paramlink
            if (ocnx.CBRecherche("Select Param.GuidParam, NomParam From Param, ParamLink, CadreRefApp, ProduitApp, MainComposantRef, MCompServ Where Param.GuidParam=ParamLink.GuidParam and ParamLink.GuidCadreRefApp=CadreRefApp.GuidCadreRefApp and CadreRefApp.GuidCadreRefApp=ProduitApp.GuidCadreRefApp and ProduitApp.GuidProduitApp=MainComposantRef.GuidProduitApp and MainComposantRef.GuidMainComposantRef=MCompServ.GuidMainComposantRef and GuidMCompServ='" + sFindKeyForDynamicTable + "'"))
            {
                while (ocnx.Reader.Read())
                    t.LstField.Add(new Field(ocnx.Reader.GetString(0), ocnx.Reader.GetString(1), 's', 0, 0, ConfDataBase.FieldOption.Param));
            }
            ocnx.CBReaderClose();

            return t;
        }
        public ToolPackageDynamic(DrawArea da)
        {
            Owner = da;
        }

        public override string GetSimpleWhere(string sTable, string GuidObjet)
        {
            return "WHERE GuidMCompServ='" + GuidObjet + "'";
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawPackageDynamic dpd;

            dpd = new DrawPackageDynamic(Owner.Owner, LstValue);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dpd;
        }
    }

    public class ToolVlanClass : DrawTools.Tool
    {
        public ToolVlanClass(DrawArea da)
        {
            Owner = da;
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {
            DrawVlanClass dv;

            dv = new DrawVlanClass(Owner.Owner, LstValue);
            Owner.Owner.drawArea.GraphicsList.Add(dv);
        }
    }
}
