using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Xml;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
    /// 
	public class ToolAxes : DrawTools.ToolRectangle
	{
        public enum RetourNiveau
        {
            Vide = 0x00,
            Absisse = 0x01,
            Ordonnee = 0x02,
        }

        public static string[] ssCat = { "Application", "Composant", "Module en Entree", "Module en Sortie" };

        public bool bAppExt;

        public ToolAxes(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            bAppExt = false;
		}
               

        /// <summary>
        /// Left mouse is released.
        /// New object is created and resized.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            DrawAxes da = (DrawAxes)drawArea.GraphicsList[0];
            FormAxe fa = new FormAxe(Owner.Owner, da, this);
            fa.ShowDialog(Owner.Owner);

            double XBorneMin = double.MaxValue, XBorneMax = double.MinValue, YBorneMin = double.MaxValue, YBorneMax = double.MinValue;
            int iNbr = 0;
            for (int i=0; i<drawArea.GraphicsList.Count; i++)
            {
                DrawObject o = (DrawObject)drawArea.GraphicsList[i];
                if(o.GetType() == typeof(DrawPtNiveau))
                {
                    double NivAbs = (double)o.GetValueFromName("NivAbs");
                    double NivOrd = (double)o.GetValueFromName("NivOrd");

                    if (XBorneMin > NivAbs) XBorneMin = NivAbs;
                    if (XBorneMax < NivAbs) XBorneMax = NivAbs;
                    if (YBorneMin > NivOrd) YBorneMin = NivOrd;
                    if (YBorneMax < NivOrd) YBorneMax = NivOrd;
                    //XBorneMoy = (XBorneMoy * iNbr + ((Niveau)lstCriteres[0]).GetMoyenne(XBorneMin, XBorneMax)) / (iNbr + 1);
                    //YBorneMoy = (YBorneMoy * iNbr + ((Niveau)lstCriteres[1]).GetMoyenne(YBorneMin, YBorneMax)) / (iNbr + 1);
                    iNbr++;
                }
            }
            /*
            da.SetValueFromName("XBorneMin", XBorneMin);
            da.SetValueFromName("XBorneMax", XBorneMax);
            da.SetValueFromName("XBorneMoy", XBorneMoy);
            da.SetValueFromName("YBorneMin", YBorneMin);
            da.SetValueFromName("YBorneMax", YBorneMax);
            da.SetValueFromName("YBorneMoy", YBorneMoy);
            */
            int idx0 = 0, idx1 = 0, idx2 = 0, idx3 = 0;
            for (int i = 0; i < drawArea.GraphicsList.Count; i++)
            {
                DrawObject o = (DrawObject)drawArea.GraphicsList[i];
                if (o.GetType() == typeof(DrawPtNiveau))
                {
                    DrawPtNiveau dpn = (DrawPtNiveau)drawArea.GraphicsList[i];
                    double X = (double)dpn.GetValueFromName("NivAbs") - (double)da.GetValueFromName("XBorneMoy");
                    double Y = (double)dpn.GetValueFromName("NivOrd") - (double)da.GetValueFromName("YBorneMoy");

                    if (X <= 0 && Y > 0)
                    {
                        dpn.rectangle.Y = da.Rectangle.Top + da.Rectangle.Height / 2 - dpn.HEIGHTPTNIVEAU * idx0++ - dpn.WIDTHESPACEPTNIVEAU;
                        dpn.rectangle.X = da.Rectangle.Left - dpn.WIDTHESPACEPTNIVEAU - dpn.WIDTHPTNIVEAU;
                    }
                    else if (X > 0 && Y > 0)
                    {
                        dpn.rectangle.Y = da.Rectangle.Top + da.Rectangle.Height / 2 - dpn.HEIGHTPTNIVEAU * idx1++ - dpn.WIDTHESPACEPTNIVEAU;
                        dpn.rectangle.X = da.Rectangle.Right + dpn.WIDTHESPACEPTNIVEAU;
                    }
                    else if (X > 0 && Y <= 0)
                    {
                        dpn.rectangle.Y = da.Rectangle.Top + da.Rectangle.Height / 2 + dpn.HEIGHTPTNIVEAU * idx2++ + dpn.WIDTHESPACEPTNIVEAU;
                        dpn.rectangle.X = da.Rectangle.Right + dpn.WIDTHESPACEPTNIVEAU;
                    }
                    else if (X <= 0 && Y <= 0)
                    {
                        dpn.rectangle.Y = da.Rectangle.Top + da.Rectangle.Height / 2 + dpn.HEIGHTPTNIVEAU * idx3++ + dpn.WIDTHESPACEPTNIVEAU;
                        dpn.rectangle.X = da.Rectangle.Left - dpn.WIDTHESPACEPTNIVEAU - dpn.WIDTHPTNIVEAU;
                    }

                    dpn.SetValueFromName("GuidAxes", da.GuidkeyObjet.ToString());
                    CreatObjetLink(dpn, "GuidAxes", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
                    //AddNewObject(Owner.Owner.drawArea, dpn, false);

                }
            }

            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }

        
        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawAxes da;

            da = new DrawAxes(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);
            AddNewObject(drawArea, da, true);
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {

            DrawAxes da;

            da = new DrawAxes(Owner.Owner, LstValue, LstValueG);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = da;
            else AddNewObject(Owner.Owner.drawArea, da, false);

            //base.CreatObjetFromBD(From1 f, LstValue, LstValueG);

        }

        public override void ExpandObj(FormExplorObj feo, ExpObj eo)
        {
            //MessageBox.Show(eo.tn.Parent.Text);
            CnxBase ocnx = Owner.Owner.oCnxBase;

            switch (eo.iCat)
            {
                case -1:
                    ExpandObjRoot(0, ssCat.Length - 1, feo, eo, ssCat);
                    break;
                case 0: //Application
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Application.GuidApplication, NomApplication FROM Application, Vue, DansVue, GModule, Module WHERE Application.GuidApplication=Vue.GuidApplication AND Vue.GuidGVue=DansVue.GuidGVue AND DansVue.GuidObjet=GModule.GuidGModule AND GModule.GuidModule=Module.GuidModule AND Module.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Application);
                    break;
                case 1: // Composant
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT MainComposant.GuidMainComposant, NomMainComposant FROM Module, MainComposant WHERE MainComposant.GuidMainComposant=Module.GuidMainComposant AND Module.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.MainComposant);
                    break;
                case 2: // Module en Entree
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT AppUser.GuidAppUser, NomAppUser FROM Module, Link, User WHERE Module.GuidModule=Link.GuidModuleOut AND Link.GuidModuleIn=AppUser.GuidAppUser AND Module.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.AppUser);
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Module.GuidModule, Module.NomModule FROM Module m, Link, Module WHERE m.GuidModule=Link.GuidModuleOut AND Link.GuidModuleIn=Module.GuidModule AND m.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Module);
                    break;
                case 3: // Module en Sortie
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Module.GuidModule, Module.NomModule FROM Module m, Link, Module WHERE m.GuidModule=Link.GuidModuleIn AND Link.GuidModuleOut=Module.GuidModule AND m.GuidModule='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Module);
                    break;
            }

            //base.ExpandObj(eo);
        }
	}
}
