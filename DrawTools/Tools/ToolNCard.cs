using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
    public class ToolNCard : DrawTools.ToolRectangle
	{
        public static string[] ssCat = { "Applications" };
        public ToolNCard(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            InitPropriete("dce25a30-7e13-426a-a6a1-b67fc9a6f852");
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            for (int i = 0; i < drawArea.GraphicsList.Count; i++)
            {
                if (drawArea.GraphicsList[i].GetType() == typeof(DrawServerPhy) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                {
                    DrawServerPhy ds = (DrawServerPhy) drawArea.GraphicsList[i];
                    DrawNCard dc = new DrawNCard(drawArea.Owner, new Point(e.X, e.Y), ds);
                    AddNewObject(drawArea, dc, true);
                    ds.AttachLink(dc,DrawObject.TypeAttach.Child);
                    dc.AttachLink(ds, DrawObject.TypeAttach.Parent);
                    dc.InitProp("GuidHote", (object)((DrawServerPhy)dc.LstParent[0]).GuidkeyObjet.ToString(), true);
                    //ds.InitDatagrid(Owner.Owner.dataGrid);
                    ds.AligneObjet();
                    dc.Normalize();
                    drawArea.GraphicsList.UnselectAll();
                    break;
                }
                else if (drawArea.GraphicsList[i].GetType() == typeof(DrawCluster) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                {
                    DrawCluster dc = (DrawCluster)drawArea.GraphicsList[i];
                    DrawNCard dnc = new DrawNCard(drawArea.Owner, new Point(e.X, e.Y), dc);
                    AddNewObject(drawArea, dnc, true);
                    dc.AttachLink(dnc, DrawObject.TypeAttach.Child);
                    dnc.AttachLink(dc, DrawObject.TypeAttach.Parent);
                    dnc.InitProp("GuidHote", (object)((DrawCluster)dnc.LstParent[0]).GuidkeyObjet.ToString(), true);
                    //dnc.InitDatagrid(Owner.Owner.dataGrid);
                    dc.AligneObjet();
                    dnc.Normalize();
                    drawArea.GraphicsList.UnselectAll();
                    break;
                }
            }
            
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }

        
        /// <summary>
        /// Left mouse is released.
        /// New object is created and resized.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            //drawArea.Owner.SetStateOfControls();
        }

        public override string GetFrom(string sType, string sGType)
        {
            return "From DansVue, " + sType + " Left Join LayerLink On " + sType + ".Guid" + sType + "=GuidObj" + " and layerlink.GuidAppVersion='" + Owner.Owner.GetGuidAppVersion() + "' left Join Vlan On  NCard.GuidVlan=Vlan.GuidVlan, " + sGType;
        }

        public override string GetWhere(string sType, string sGType, string GuidGVue, string sGuidVueSrvPhy)
        {
            return "WHERE GuidGVue ='" + GuidGVue + "' and GuidObjet=Guid" + sGType + " and " + sGType + ".Guid" + sType + "=" + sType + ".Guid" + sType + Owner.Owner.wkApp.GetWhereLayer() + " ORDER BY " + sType + ".Nom" + sType;
        }

        public override string GetSimpleSelect(Table t)
        {
            return t.GetSelectField(ConfDataBase.FieldOption.InterneBD);
        }

        public override string GetSimpleFrom(string sTable)
        {
            return "FROM " + sTable + " left Join Vlan On  NCard.GuidVlan=Vlan.GuidVlan";
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
                case 0: //Applications
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Application.GuidApplication, NomApplication FROM Application, AppVersion, Vue, DansVue, GNCard, NCard WHERE Application.GuidApplication = AppVersion.GuidApplication AND AppVersion.GuidAppVersion = Vue.GuidAppVersion AND Vue.GuidGVue = DansVue.GuidGVue AND DansVue.GuidObjet = GNCard.GuidGNCard AND GNCard.GuidNCard = NCard.GuidNCard AND NCard.GuidNCard='" + eo.GuidObj.ToString() + "' Order By NomApplication", eo.tn, DrawArea.DrawToolType.Application);
                    break;
                
            }
        }

        public void CreatObjetFromParent(DrawObject o)
        {
            ArrayList lstNCard = null;
            if (o.GetType() == typeof(DrawInssvc))
                lstNCard = Owner.Owner.oCnxBase.GetlstNCardFromsvc((string)o.GetValueFromName("GuidInssvc"));
            if (o.GetType() == typeof(DrawInsing))
                lstNCard = Owner.Owner.oCnxBase.GetlstNCardFromsvc((string)o.GetValueFromName("GuidInsing"));

            for (int i = 0; i < lstNCard.Count; i++)
            {
                Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.NCard].LoadSimpleObject((string)lstNCard[i]);
            }
        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawNCard dnc;
            int n;

            dnc = new DrawNCard(Owner.Owner, LstValue, LstValueG);
            int j = Owner.Owner.drawArea.GraphicsList.FindObjet(0, dnc.GuidkeyObjet.ToString());
            if (j < 0)
            {
                n = CreatObjetLink(dnc, "GuidHote", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
                if (dnc.LstParent.Count > 0)
                {
                    if (((DrawObject)dnc.LstParent[0]).GetType() == typeof(DrawServerPhy))
                    {
                        DrawServerPhy ds = (DrawServerPhy)dnc.LstParent[0];
                        ds.AligneObjet();
                    }
                    if (((DrawObject)dnc.LstParent[0]).GetType() == typeof(DrawCluster))
                    {
                        DrawCluster dc = (DrawCluster)dnc.LstParent[0];
                        dc.AligneObjet();
                    }
                }
                
                if (n > -1)
                {
                    AddNewObject(Owner.Owner.drawArea, dnc, false);
                    
                    /*    dnc.Hauteur = dnc.setHauteur(new Point(dnc.rectangle.X, dnc.rectangle.Y), (DrawRectangle)Owner.GraphicsList[n]);
                        n = CreatObjetLink(dnc, "GuidVlan", DrawObject.TypeAttach.Entree, DrawObject.TypeAttach.Sortie);
                        if (n > -1)
                        {
                            dv = (DrawVLan)Owner.GraphicsList[n];
                            dnc.HandleVLan = dv.GetAttchHandle(dnc.Rectangle);
                            dv.AttachHandle.Add(dnc.HandleVLan + 10); // +10, car il y a 9 points prient par le rectangle du VLAn

                        }
                        */
                }
                //AddNewObject(Owner.Owner.drawArea, dnc, selected);
            }
        }
	}
}
