using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Xml;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolInsks : DrawTools.ToolRectangle
	{
		public ToolInsks(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            oLayers = new LayerList[1];
            oLayers[0] = new LayerList("355266ed-dcd4-425b-a4fa-4daf9d42de8f");
            oLayers[0].AddTemplate(Owner.Owner, "", Owner.Owner.sGuidTemplate);
        }

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            string sGuid = "";

            Owner.Owner.bCreatsousObj = true;
            
            if (drawArea.AddObjet)
            {
                if (Owner.Owner.tvObjet.SelectedNode.Parent.Name[0] == '2') // Objects instancés
                {
                    if(Owner.Owner.oCnxBase.CBRecherche("Select GuidInsks from Insns Where GuidInsns = '" + (string)Owner.Owner.tvObjet.SelectedNode.Name + "'"))
                    {
                        if(!Owner.Owner.oCnxBase.Reader.IsDBNull(0))
                        {
                            sGuid = Owner.Owner.oCnxBase.Reader.GetString(0);
                            Owner.Owner.bCreatsousObj = false;
                        }
                    }
                    Owner.Owner.oCnxBase.CBReaderClose();
                }
                else sGuid = (string)Owner.Owner.tvObjet.SelectedNode.Name;

                if(sGuid !="")
                    //CreateObjetFromMouse(drawArea, GetIndexObjetFromPoint(pt), sGuid, pt);
                    CreateObjetFromMouse(drawArea, sGuid, pt);

                drawArea.AddObjet = false;
                
            }
            else AddNewObject(drawArea, new DrawInsks(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count), true);            
        }
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            DrawInsks dik = (DrawInsks)drawArea.GraphicsList[0];

            dik.Normalize();
            /*
            // Creation de l'objet node si l'objet ks n'en possede pas. Dans le cas contraire, il faut charger le l'objet node. 
            DrawInsnd dind = new DrawInsnd(Owner.Owner, drawArea.GraphicsList.Count);

            dik.AttachLink(dind, DrawObject.TypeAttach.Child);
            dind.AttachLink(dik, DrawObject.TypeAttach.Parent);
            AddNewObject(Owner.Owner.drawArea, dind, false);

            {
                DrawNCard dnc = new DrawNCard(Owner.Owner, dind);

                dind.AttachLink(dnc, DrawObject.TypeAttach.Child);
                dnc.AttachLink(dind, DrawObject.TypeAttach.Parent);
                dnc.InitProp("GuidHote", (object)((DrawInsnd)dnc.LstParent[0]).GuidkeyObjet.ToString(), true);
                AddNewObject(Owner.Owner.drawArea, dnc, false);
            }
            */
            ArrayList lstiks = new ArrayList();
            lstiks.Add(dik);

            if(Owner.Owner.bCreatsousObj)
                Owner.Owner.oCnxBase.LoadNameSpace(lstiks);
            Owner.Owner.oCnxBase.LoadNode(lstiks);
            Owner.Owner.bCreatsousObj = true;

            //ToolInsns tins = (ToolInsns)Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.Insns];
            //tins.CreatObjetFromks(drawArea, dik);
            //ToolInsnd tind = (ToolInsnd)Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.Insnd];
            //tind.CreatObjetFromks(drawArea, dik);

            dik.AligneObjet();
            dik.InitDatagrid(Owner.Owner.dataGrid);

            Owner.Owner.drawArea.GraphicsList.MoveSelectionToBack();
            Owner.Owner.drawArea.GraphicsList.UnselectAll();
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();

        }
        

        public bool CreateObjetFromMouse(DrawArea drawArea, string sGuid, Point e)
        {
            bool Create = false;

            Owner.Owner.oCureo = new ExpObj(new Guid(sGuid), "", DrawArea.DrawToolType.Insks);
            drawArea.tools[(int)Owner.Owner.oCureo.ObjTool].LoadSimpleObjectSansGraph(Owner.Owner.oCureo);

            if (Owner.Owner.oCureo.oDraw != null)
            {
                AddNewObject(drawArea, new DrawInsks(drawArea.Owner, e.X, e.Y, 1, 1, Owner.Owner.oCureo.oDraw.LstValue, drawArea.GraphicsList.Count), true);
                Owner.Owner.oCureo = null;
                Create = true;
            }
            else
            {
                // Utilisation de la modélisation applicative (Gen...)
                Owner.Owner.oCureo = new ExpObj(new Guid(sGuid), "", DrawArea.DrawToolType.Genks);
                drawArea.tools[(int)Owner.Owner.oCureo.ObjTool].LoadSimpleObjectSansGraph(Owner.Owner.oCureo);

                if (Owner.Owner.oCureo.oDraw != null)
                {
                    AddNewObject(drawArea, new DrawInsks(drawArea.Owner, e.X, e.Y, 1, 1, (DrawGenks)Owner.Owner.oCureo.oDraw, drawArea.GraphicsList.Count), true);
                    Owner.Owner.oCureo = null;
                    Create = true;
                }
            }


            
            return Create;
        }

        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawInsks dik;

            dik = new DrawInsks(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dik;
            else AddNewObject(Owner.Owner.drawArea, dik, false);
            return dik;
        }

        public override void CreatObjetFromBD(ArrayList LstValue)
        {

            DrawInsks diks;

            diks = new DrawInsks(Owner.Owner, LstValue);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = diks;
            else Owner.Owner.drawArea.GraphicsList.Add(diks);
        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawInsks dik;
            bool selected = false;

            dik = new DrawInsks( Owner.Owner, LstValue, LstValueG);
            if (dik.rectangle.X == 0)
            {
                selected = true;
            } else AddNewObject(Owner.Owner.drawArea, dik, selected);
        }

	}
}
