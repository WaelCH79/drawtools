using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
    /// 

    public class ObjectAndNiveau
    {
        public string sGuid;
        public double dNiveau;
        public object oParam;
        
        public ObjectAndNiveau(string s, double d, object o)
        {
            sGuid = s; dNiveau = d; oParam=o;
        }
    }

    //---------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------

    public class SortEffectif : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            Effectif oEffx = (Effectif)x, oEffy = (Effectif)y;
            if (oEffx.iNiv < 0)
            {
                if (oEffx.Val < oEffy.Val) return -1;
                if (oEffx.Val > oEffy.Val) return 1;
            }
            else {
                Niveau oNivx = (Niveau)oEffx.lstNivEffectif[oEffx.iNiv];
                Niveau oNivy = (Niveau)oEffy.lstNivEffectif[oEffy.iNiv];
                if (oNivx.Val < oNivy.Val) return -1;
                if (oNivx.Val > oNivy.Val) return 1;
            }
            return 0;
        }
    }

    public class Critere
    {
        public string GuidCritere;
        public string NomCritere;
        public bool Calc; // si true --> prise en compte du niveau sur le calcul de l'agrega avec d'autre niveau
        public double dMin;
        public double dMax;
        Form1 F;

        public Critere(Form1 f, string sGuid, string sNom)
        {
            GuidCritere = sGuid;
            NomCritere = sNom;
            Calc = false;

            F = f;
        }

    }
    public class Effectif
    {
        public string GuidEffectif;
        public string NomEffectif;
        public ArrayList lstNivEffectif;
        public int iNiv; //index liste Niv pour la fonction sort
        public double Val; // calcul en fonction de plusieurs Niv
        public int iCouleur;
        Form1 F;

        public Effectif(Form1 f, string sGuid, string sNom, ArrayList lstCriteres, int iCoul)
        {
            GuidEffectif = sGuid;
            NomEffectif = sNom;
            iCouleur = iCoul;
            lstNivEffectif = new ArrayList();
            F = f;
            if (lstCriteres != null)
            {
                for (int i = 0; i < lstCriteres.Count; i++)
                {
                    Critere oCritere = (Critere)lstCriteres[i];
                    lstNivEffectif.Add(GetNiv(oCritere.GuidCritere, oCritere.NomCritere));
                }
            }
            else lstNivEffectif = null;
        }

        public int FindEffectifFromLST(ArrayList lstEffectifs)
        {
            for (int i = 0; i < lstEffectifs.Count; i++)
            {
                Effectif oEff = (Effectif)lstEffectifs[i];

                if (oEff.GuidEffectif == GuidEffectif) return i;
            }
            return -1;
        }

        private Niveau GetNiv(string sGuid, string texte)
        {
            Niveau Niv = null;
            if (texte != null)
            {
                switch (texte[0])
                {
                    case '0': // Fin commercialisation

                        Niv = new FinCommercialNiveau(F, sGuid, texte);
                        break;
                    case '1': // Support
                        Niv = new SupportNiveau(F, sGuid, texte);
                        break;
                    case '2': // Business
                        Niv = new BusinessNiveau(F, sGuid, texte);
                        break;
                    case '3': //Cout
                        Niv = new CoutNiveau(F, sGuid, texte);
                        break;
                    case '4': // Complexite
                        Niv = new ComplexiteNiveau(F, sGuid, texte);
                        break;
                    case '5': // Expertise
                        Niv = new ExpertiseNiveau(F, sGuid, texte);
                        break;
                    case '6': // Securite 
                        Niv = new SecuriteNiveau(F, sGuid, texte);
                        /*if (Parent.oCnxBase.CBRecherche("SELECT GuidIndicatorRef FROM Indicator WHERE GuidIndicator='" + sGuid + "'"))
                        {
                            CriticiteNiveau CriNiv1 = new CriticiteNiveau(Parent, Parent.oCnxBase.Reader.GetString(0), "");
                            Niv[1] = CriNiv1;
                            retour = true;
                        }
                        Parent.oCnxBase.CBReaderClose();*/
                        break;
                    case '7': // Criticite
                        Niv = new CriticiteNiveau(F, sGuid, texte);
                        break;
                    case '9': // Obsolescence
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                        Niv = new Obsolescence(F, sGuid, texte);
                        break;
                    case 'A': // Instance
                        Niv = new InstanceNiveau(F, sGuid, texte);
                        break;
                    case 'F': // F-Impact business
                        Niv = new ImpactBusinessNiveau(F, sGuid, texte);
                        break;
                    case 'G': // G-Impact
                        Niv = new ImpactNiveau(F, sGuid, texte);
                        break;
                }
            }
            return Niv;
        }
    }

    //---------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------

    public abstract class Niveau
    {
        static public int NbrNivCriticite=4;
        public string GuidNiveau;
        public string NomNiveau;
        public double Val;
        public double Nbr;
        public Form1 F;

        public virtual void Calcul(ArrayList LstVal)
        {
        }

        public virtual bool CheckValidite()
        {
            return true;
        }

        public virtual string GetTexttHtml()
        {
            return Val.ToString();
        }

        public virtual string GetStyleHtml()
        {
            return "";
        }

        public virtual string GetAlertMin()
        {
            return "ValMin"; //si ValMin --> Val minimum prise en compte, si ValMax --> Val maximum prise en compte pour les calculs d'agrega voir fonction CalcAgregaEffectif 
        }

        public virtual int GetColor()
        {
            return Color.Black.ToArgb();
        }

        public virtual void PostCalcul()
        {
        }

        public virtual double CalculWithRef(double ValRef)
        {
            return 0;
        }

        public virtual double GetMoyenne(double Min, double Max)
        {
            return (Max + Min) / 2;
        }
        public virtual ArrayList GetQueryForApp(Tool o, string GuidObj, bool bOption) { return new ArrayList(); }
        public virtual ArrayList GetQueryForServer(Tool o, string GuidObj, bool bOption) { return new ArrayList(); }
        public virtual ArrayList GetQueryForTechno(Tool o, string GuidObj, bool bOption) { return new ArrayList(); }

        public void MaxNiv(double[] Niv, double[] Nivi)
        {
            for (int i = 0; i < Niv.Length; i++)
                if (Niv[i] < Nivi[i]) Niv[i] = Nivi[i];
        }

        public void MinNiv(double[] Niv, double[] Nivi)
        {
            for (int i = 0; i < Niv.Length; i++)
                if (Niv[i] > Nivi[i]) Niv[i] = Nivi[i];
        }
                
        public void ExtractNiv(double Value, double[] Niv)
        {
            int iout;
            for (int i = 0; i < Niv.Length; i++)
            {
                Niv[i] = Math.DivRem((int)Value, (int)Math.Pow(10, Niv.Length - 1 - i), out iout);
                Value -= Niv[i] * Math.Pow(10, Niv.Length - 1 - i);
            }
        }

        public double ConcatNiv(double[] Niv)
        {
            double Value=0;
            for (int i = 0; i < Niv.Length; i++)
                Value += Niv[i] * Math.Pow(10, Niv.Length - 1 - i);
            return Value;
        }

        public virtual double IconStatus(double ValRef)
        {
            return 0;
        }
        
        public double GetValWithRef(double[] Niv, double[] NivRef)
        {
            //Calcul le delta + le Min des delta
            double[] NivDif = new double[NbrNivCriticite];
            double Min = double.MaxValue, ValDef=0;
            for (int i = 0; i < NivDif.Length; i++) 
            {
                NivDif[i] = Niv[i] - NivRef[i];
                if (Min > NivDif[i]) Min = NivDif[i];
            }

            if (Min < 0)
            {
                for (int i = 0; i < NivDif.Length; i++)
                    if (NivDif[i] < 0) ValDef += NivDif[i] * ((NbrNivCriticite-1) * 10); else ValDef += NivDif[i];
            }
            else
            {
                for (int i = 0; i < NivDif.Length; i++) ValDef += NivDif[i];
            }
            return ValDef;
        }

        public virtual double GetMaxNiv(double Val, double Vali)
        {
            double[] Niv = new double[NbrNivCriticite];
            double[] Nivi = new double[NbrNivCriticite];
            ExtractNiv(Val, Niv);
            ExtractNiv(Vali, Nivi);
            MaxNiv(Niv, Nivi);
            return ConcatNiv(Niv);
        }

        public virtual double GetMinNiv(double Val, double Vali)
        {
            double[] Niv = new double[NbrNivCriticite];
            double[] Nivi = new double[NbrNivCriticite];
            ExtractNiv(Val, Niv);
            ExtractNiv(Vali, Nivi);
            MinNiv(Niv, Nivi);
            return ConcatNiv(Niv);
        }
    }

    public class VirtualNiveau : Niveau
    {
        public VirtualNiveau()
        {
        }
    }

    public class FinCommercialNiveau : Niveau
    {
        public FinCommercialNiveau(Form1 f, string sGuid, string sNom)
        {
            GuidNiveau = sGuid;
            NomNiveau = sNom;
            Val = DateTime.MaxValue.ToOADate();
            F = f;

        }
        public override string GetAlertMin()
        {
            return "ValMax"; //si ValMin --> Val minimum prise en compte, si ValMax --> Val maximum prise en compte pour les calculs d'agrega voir fonction CalcAgregaEffectif 
        }

        public override ArrayList GetQueryForApp(Tool o, string GuidObj, bool bOption) { return o.FinCommercialForAppQuery(GuidObj, bOption); }
        public override ArrayList GetQueryForServer(Tool o, string GuidObj, bool bOption) { return o.FinCommercialForServerQuery(GuidObj, bOption); }
        public override ArrayList GetQueryForTechno(Tool o, string GuidObj, bool bOption) { return o.FinCommercialForTechnoQuery(GuidObj, bOption); }

        public override void Calcul(ArrayList LstVal)
        {
            for (int i = 0; i < LstVal.Count; i++)
            {
                double vali = ((ObjectAndNiveau)LstVal[i]).dNiveau;
                if (vali < Val) Val = vali;
            }
            //base.Calcul();
        }

        public override void PostCalcul()
        {
            Val = Val / Nbr;
        }

        public override double GetMoyenne(double Min, double Max)
        {
            return (DateTime.Now.ToOADate());
            //return base.GetMoyenne(Min, Max);
        }
    }

    public class SupportNiveau : Niveau
    {
        public SupportNiveau(Form1 f, string sGuid, string sNom)
        {
            GuidNiveau = sGuid;
            NomNiveau = sNom;
            Val = DateTime.MaxValue.ToOADate();
            F = f;
        }
        public override bool CheckValidite()
        {
            if (Val == DateTime.MaxValue.ToOADate()) return false;
            return true;
            //return base.CheckValidite();
        }

        public override int GetColor()
        {
            if (Val - DateTime.Now.ToOADate() - 180 < 0)  return 0x2534FB; // Color.Red.ToArgb();
            else if (Val - DateTime.Now.ToOADate() - 550 < 0) return 0x1290EE; // Color.Orange.ToArgb();
            return 0x1B9D2A; // Color.Green.ToArgb();
        }
        public override string GetAlertMin()
        {
            return "ValMax"; //si ValMin --> Val minimum prise en compte, si ValMax --> Val maximum prise en compte pour les calculs d'agrega voir fonction CalcAgregaEffectif 
        }

        public override ArrayList GetQueryForApp(Tool o, string GuidObj, bool bOption) { return o.SupportForAppQuery(GuidObj, bOption); }
        public override ArrayList GetQueryForServer(Tool o, string GuidObj, bool bOption) { return o.SupportForServerQuery(GuidObj, bOption); }
        public override ArrayList GetQueryForTechno(Tool o, string GuidObj, bool bOption) { return o.SupportForTechnoQuery(GuidObj, bOption); }

        public override void Calcul(ArrayList LstVal)
        {
            for (int i = 0; i < LstVal.Count; i++)
            {
                double vali = ((ObjectAndNiveau)LstVal[i]).dNiveau;
                if (vali < Val) Val = vali;
            }
            //base.Calcul();
        }

        public override double GetMoyenne(double Min, double Max)
        {
            return (DateTime.Now.ToOADate()+360);
            //return (DateTime..Now.ToOADate());
            //return base.GetMoyenne(Min, Max);
        }

        public override string GetTexttHtml()
        {
            return DateTime.FromOADate(Val).ToShortDateString();
            //return base.GetTexttHtml();
        }
    }

    public class BusinessNiveau : Niveau
    {
        public BusinessNiveau(Form1 f, string sGuid, string sNom)
        {
            GuidNiveau = sGuid;
            NomNiveau = sNom;
            Val = 0;
            F = f;
        }

        public override ArrayList GetQueryForApp(Tool o, string GuidObj, bool bOption) { return o.BusinessForAppQuery(GuidObj, bOption); }
        public override ArrayList GetQueryForServer(Tool o, string GuidObj, bool bOption) { return o.BusinessForServerQuery(GuidObj, bOption); }
        public override ArrayList GetQueryForTechno(Tool o, string GuidObj, bool bOption) { return o.BusinessForTechnoQuery(GuidObj, bOption); }
        /*
        public override double GetMoyenne(double Min, double Max)
        {
            return (Max - Min) / 2;
            //return base.GetMoyenne(Min, Max);
        }*/

        public override void Calcul(ArrayList LstVal)
        {
            for (int i = 0; i < LstVal.Count; i++)
            {
                double vali = ((ObjectAndNiveau)LstVal[i]).dNiveau;
                Val += vali;
            }
            //base.Calcul();
        }
    }

    public class InstanceNiveau : Niveau
    {
        public InstanceNiveau(Form1 f, string sGuid, string sNom)
        {
            GuidNiveau = sGuid;
            NomNiveau = sNom;
            Val = 0;
            F = f;
        }

        public override ArrayList GetQueryForApp(Tool o, string GuidObj, bool bOption) { return o.InstanceForAppQuery(GuidObj, bOption); }
        public override ArrayList GetQueryForServer(Tool o, string GuidObj, bool bOption) { return o.InstanceForServerQuery(GuidObj, bOption); }
        public override ArrayList GetQueryForTechno(Tool o, string GuidObj, bool bOption) { return o.InstanceForTechnoQuery(GuidObj, bOption); }

        public override void Calcul(ArrayList LstVal)
        {
            for (int i = 0; i < LstVal.Count; i++)
            {
                double vali = ((ObjectAndNiveau)LstVal[i]).dNiveau;
                Val += vali;
            }
            //base.Calcul();
        }
    }

    public class Obsolescence : Niveau
    {
        public Obsolescence(Form1 f, string sGuid, string sNom)
        {
            GuidNiveau = sGuid;
            NomNiveau = sNom;
            Val = 0; Nbr = 0;
            F = f;
        }

        public override ArrayList GetQueryForApp(Tool o, string GuidObj, bool bOption) { return o.ObsolescenceForAppQuery(GuidObj, bOption); }
        public override ArrayList GetQueryForServer(Tool o, string GuidObj, bool bOption) { return o.ObsolescenceForServerQuery(GuidObj, bOption); }
        public override ArrayList GetQueryForTechno(Tool o, string GuidObj, bool bOption) { return o.ObsolescenceForTechnoQuery(GuidObj, bOption); }
        /*
        public override double GetMoyenne(double Min, double Max)
        {
            return (Max - Min) / 2;
            //return base.GetMoyenne(Min, Max);
        }*/

        public override void Calcul(ArrayList LstVal)
        {
            for (int i = 0; i < LstVal.Count; i++)
            {
                double vali = ((ObjectAndNiveau)LstVal[i]).dNiveau;
                Val += vali; Nbr++;
            }
            //base.Calcul();
        }

        public override void PostCalcul()
        {
            if (Nbr != 0) { Val = Val / Nbr; Nbr = 0; }
        }

        public override double GetMoyenne(double Min, double Max)
        {
            return 3;
            //return base.GetMoyenne(Min, Max);
        }

        public override string GetStyleHtml()
        {
            string sFormat = "";
            if (Val < 1) sFormat = "background-color:red";
            else if (Val < 2) sFormat = "background-color:orange";
            else if (Val < 3) sFormat = "background-color:yellow";
            else if (Val < 4) sFormat = "background-color:green";
            else sFormat = "background-color:blue";

            return sFormat;
        }

        public override string GetTexttHtml()
        {
            string sFormat = " ";
        
            return sFormat;
        }
    }

    public class CoutParam
    {
        public double cpup;
        public double ramp;

        public CoutParam(double Cpu, double Ram, double Cpua, double Rama)
        {
            if (Cpu != 0) cpup = Cpua / Cpu; else cpup = 0;
            if (Ram != 0) ramp = Rama / Ram; else ramp = 0;
        }
    }
        
    public class CoutNiveau : Niveau
    {
        public CoutNiveau(Form1 f, string sGuid, string sNom)
        {
            GuidNiveau = sGuid;
            NomNiveau = sNom;
            Val = 0;
            F = f;
        }

        public override ArrayList GetQueryForApp(Tool o, string GuidObj, bool bOption) { return o.CoutForAppQuery(GuidObj, bOption); }
        public override ArrayList GetQueryForServer(Tool o, string GuidObj, bool bOption) { return o.CoutForServerQuery(GuidObj, bOption); }
        public override ArrayList GetQueryForTechno(Tool o, string GuidObj, bool bOption) { return o.CoutForTechnoQuery(GuidObj, bOption); }

        public override void Calcul(ArrayList LstVal)
        {
            for (int i = 0; i < LstVal.Count; i++)
            {
                ObjectAndNiveau objniv = (ObjectAndNiveau)LstVal[i];
                CoutParam oparam = (CoutParam)objniv.oParam;
                double vali = objniv.dNiveau;
                if (oparam != null)
                {
                    if(oparam.cpup != 1 || oparam.ramp!=1) vali = Math.Max(oparam.cpup, oparam.ramp) * vali;
                }
                F.oCnxBase.SWwriteLog(0,"Nom " + NomNiveau + " : " + vali,false);
                Val += vali;
            }
            //base.Calcul();
        }
    }

    public class ComplexiteNiveau : Niveau
    {
        public ComplexiteNiveau(Form1 f, string sGuid, string sNom)
        {
            GuidNiveau = sGuid;
            NomNiveau = sNom;
            Val = 0;
            F = f;
        }
        public override int GetColor()
        {
            //if (Val <3) return 0x2534FB; // Color.Red.ToArgb();
            //else if (Val <6) return 0x1290EE; // Color.Orange.ToArgb();
            return 0x1B9D2A; // Color.Green.ToArgb();
        }
        public override string GetAlertMin()
        {
            return "ValMin"; //si ValMin --> Val minimum prise en compte, si ValMax --> Val maximum prise en compte pour les calculs d'agrega voir fonction CalcAgregaEffectif 
                             // plus la valeur est min mieux c'est
        }

        public override ArrayList GetQueryForApp(Tool o, string GuidObj, bool bOption) { return o.ComplexiteForAppQuery(GuidObj, bOption); }
        public override ArrayList GetQueryForServer(Tool o, string GuidObj, bool bOption) { return o.ComplexiteForServerQuery(GuidObj, bOption); }
        public override ArrayList GetQueryForTechno(Tool o, string GuidObj, bool bOption) { return o.ComplexiteForTechnoQuery(GuidObj, bOption); }

        public override void Calcul(ArrayList LstVal)
        {
            for (int i = 0; i < LstVal.Count; i++)
            {
                double vali = ((ObjectAndNiveau)LstVal[i]).dNiveau;
                Val += vali;
            }
            //base.Calcul();
        }

    }

    public class ImpactNiveau : Niveau
    {
        public ImpactNiveau(Form1 f, string sGuid, string sNom)
        {
            GuidNiveau = sGuid;
            NomNiveau = sNom;
            Val = 0;
            F = f;
        }
        public override int GetColor()
        {
            //if (Val <3) return 0x2534FB; // Color.Red.ToArgb();
            //else if (Val <6) return 0x1290EE; // Color.Orange.ToArgb();
            return 0x1B9D2A; // Color.Green.ToArgb();
        }
        public override string GetAlertMin()
        {
            return "ValMin"; //si ValMin --> Val minimum prise en compte, si ValMax --> Val maximum prise en compte pour les calculs d'agrega voir fonction CalcAgregaEffectif 
                             // plus la valeur est min mieux c'est
        }

        public override ArrayList GetQueryForApp(Tool o, string GuidObj, bool bOption) { return o.ImpactForAppQuery(GuidObj, bOption); }
        public override ArrayList GetQueryForServer(Tool o, string GuidObj, bool bOption) { return o.ImpactForServerQuery(GuidObj, bOption); }
        public override ArrayList GetQueryForTechno(Tool o, string GuidObj, bool bOption) { return o.ImpactForTechnoQuery(GuidObj, bOption); }

        public override void Calcul(ArrayList LstVal)
        {
            for (int i = 0; i < LstVal.Count; i++)
            {
                double vali = ((ObjectAndNiveau)LstVal[i]).dNiveau;
                Val += vali;
            }
            //base.Calcul();
        }

    }

    public class ExpertiseNiveau : Niveau
    {
        public ExpertiseNiveau(Form1 f, string sGuid, string sNom)
        {
            GuidNiveau = sGuid;
            NomNiveau = sNom;
            Val = 0;
            F = f;
        }
        public override string GetAlertMin()
        {
            return "ValMax"; //si ValMin --> Val minimum prise en compte, si ValMax --> Val maximum prise en compte pour les calculs d'agrega voir fonction CalcAgregaEffectif 
                             // ValMin : plus la valeur est min mieux c'est, ValMax : plus la valeur est min mieux c'est
        }
        public override int GetColor()
        {
            //if (Val <3) return 0x2534FB; // Color.Red.ToArgb();
            //else if (Val <6) return 0x1290EE; // Color.Orange.ToArgb();
            return 0x1B9D2A; // Color.Green.ToArgb();
        }

        public override ArrayList GetQueryForApp(Tool o, string GuidObj, bool bOption) { return o.ExpertiseForAppQuery(GuidObj, bOption); }
        public override ArrayList GetQueryForServer(Tool o, string GuidObj, bool bOption) { return o.ExpertiseForServerQuery(GuidObj, bOption); }
        public override ArrayList GetQueryForTechno(Tool o, string GuidObj, bool bOption) { return o.ExpertiseForTechnoQuery(GuidObj, bOption); }

        public override void Calcul(ArrayList LstVal)
        {
            for (int i = 0; i < LstVal.Count; i++)
            {
                double vali = ((ObjectAndNiveau)LstVal[i]).dNiveau;
                Val += vali;
            }
            //base.Calcul();
        }
    }

    public class CriticiteNiveau : Niveau
    {
        public CriticiteNiveau(Form1 f, string sGuid, string sNom)
        {
            GuidNiveau = sGuid;
            NomNiveau = sNom;
            Val = 0;
            F = f;
        }

        public override ArrayList GetQueryForApp(Tool o, string GuidObj, bool bOption) { return o.CriticiteForAppQuery(GuidObj, bOption); }
        public override ArrayList GetQueryForServer(Tool o, string GuidObj, bool bOption) { return o.CriticiteForServerQuery(GuidObj, bOption); }
        public override ArrayList GetQueryForTechno(Tool o, string GuidObj, bool bOption) { return o.CriticiteForTechnoQuery(GuidObj, bOption); }

        public override double GetMoyenne(double Min, double Max)
        {
            return (Max - Min) / 2;
            //return base.GetMoyenne(Min, Max);
        }

        public override void Calcul(ArrayList LstVal)
        {
            for (int i = 0; i < LstVal.Count; i++)
            {
                double vali = ((ObjectAndNiveau)LstVal[i]).dNiveau;
                Val = GetMaxNiv(Val, vali);
            }
            //base.Calcul();
        }
    }

    public class SecuriteNiveau : Niveau
    {
        public SecuriteNiveau(Form1 f, string sGuid, string sNom)
        {
            GuidNiveau = sGuid;
            NomNiveau = sNom;
            double[] MaxVal = new double[NbrNivCriticite];
            for (int i = 0; i < MaxVal.Length; i++) MaxVal[i] = 9;
            Val = ConcatNiv(MaxVal);
            F = f;
        }

        public override ArrayList GetQueryForApp(Tool o, string GuidObj, bool bOption) { return o.SecuriteForAppQuery(GuidObj, bOption); }
        public override ArrayList GetQueryForServer(Tool o, string GuidObj, bool bOption) { return o.SecuriteForServerQuery(GuidObj, bOption); }
        public override ArrayList GetQueryForTechno(Tool o, string GuidObj, bool bOption) { return o.SecuriteForTechnoQuery(GuidObj, bOption); }

        public override void Calcul(ArrayList LstVal)
        {
            for (int i = 0; i < LstVal.Count; i++)
            {
                double vali = ((ObjectAndNiveau)LstVal[i]).dNiveau;
                Val = GetMinNiv(Val, vali);
            }
            //base.Calcul();
        }

        public override double IconStatus(double ValRef)
        {
            double[] Niv = new double[NbrNivCriticite];
            double[] NivRef = new double[NbrNivCriticite];
            double[] NivDif = new double[NbrNivCriticite];
            
            ExtractNiv(Val, Niv);
            ExtractNiv(ValRef, NivRef);
            
            for (int i = 0; i < NivDif.Length; i++)
            {
                NivDif[i] = Niv[i] - NivRef[i];
                if (NivDif[i] < 0) NivDif[i] = Math.Abs(NivDif[i]); else NivDif[i] = 0;
            }
            return ConcatNiv(NivDif);
        }

        public override double CalculWithRef(double ValRef)
        {
            double[] Niv = new double[NbrNivCriticite];
            double[] NivRef = new double[NbrNivCriticite];
            ExtractNiv(Val, Niv);
            ExtractNiv(ValRef, NivRef);
            return GetValWithRef(Niv, NivRef);
            //base.CalculWithRef(ValRef);
        }

        public override double GetMoyenne(double Min, double Max)
        {
            return 0;
            //return base.GetMoyenne(Min, Max);
        }
    }

    public class ImpactBusinessNiveau : Niveau
    {
        public ImpactBusinessNiveau(Form1 f, string sGuid, string sNom)
        {
            GuidNiveau = sGuid;
            NomNiveau = sNom;
            Val = 0;
            F = f;
        }

        public override ArrayList GetQueryForApp(Tool o, string GuidObj, bool bOption) { return o.ImpactBusinessForAppQuery(GuidObj, bOption); }
        public override ArrayList GetQueryForServer(Tool o, string GuidObj, bool bOption) { return o.ImpactBusinessForServerQuery(GuidObj, bOption); }
        public override ArrayList GetQueryForTechno(Tool o, string GuidObj, bool bOption) { return o.ImpactBusinessForTechnoQuery(GuidObj, bOption); }

        public override void Calcul(ArrayList LstVal)
        {
            for (int i = 0; i < LstVal.Count; i++)
            {
                double vali = ((ObjectAndNiveau)LstVal[i]).dNiveau;
                Val += vali;
            }
            //base.Calcul();
        }
    }

    public class ToolPtNiveau : DrawTools.ToolRectangle
	{
        public static string[] ssCat = { "Applications", "Fonctions", "Technologies", "Interfaces", "Materiel", "Baies", "Emplacement" };

        public ToolPtNiveau()
        {
        }

        public ToolPtNiveau(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
		}

        
        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawPtNiveau di;
            bool selected = false;

            di = new DrawPtNiveau(Owner.Owner, LstValue, LstValueG);

            AddNewObject(Owner.Owner.drawArea, di, selected);
        }

	}
}
