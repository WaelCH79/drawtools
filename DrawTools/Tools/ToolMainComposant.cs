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
	public class ToolMainComposant : DrawTools.ToolRectangle
	{
        public static string[] ssCat = { "Fonction", "Interfaces", "Modules", "Fichiers", "Bases", "Composant en Entree", "Composant en Sortie", "Serveur de Production", "Serveur HorsProduction", "Serveur de Pre-production" }; 
		public ToolMainComposant(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");

            //initialisation des proprietes
            oLayers = new LayerList[1];
            oLayers[0] = new LayerList("bb1bac30-3442-4086-b28c-57a875608bd1");
            oLayers[0].AddTemplate(Owner.Owner, "", Owner.Owner.sGuidTemplate);
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawMainComposant dmc;
            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                for (int i = 0; i < drawArea.GraphicsList.Count; i++)
                {
                    if (drawArea.GraphicsList[i].GetType() == typeof(DrawServer) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                    {
                        DrawServer ds = (DrawServer)drawArea.GraphicsList[i];

                        dmc = new DrawMainComposant(drawArea.Owner);

                        ds.AttachLink(dmc, DrawObject.TypeAttach.Child);
                        dmc.AttachLink(ds, DrawObject.TypeAttach.Parent);
                        ds.AligneObjet();
                        dmc.Normalize();
                        AddNewObject(Owner.Owner.drawArea, dmc, true);

                        break;
                    }

                    else if (drawArea.GraphicsList[i].GetType() == typeof(DrawTechUser) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                    {
                        DrawTechUser dtu = (DrawTechUser)drawArea.GraphicsList[i];

                        dmc = new DrawMainComposant(drawArea.Owner);

                        dtu.AttachLink(dmc, DrawObject.TypeAttach.Child);
                        dmc.AttachLink(dtu, DrawObject.TypeAttach.Parent);
                        dtu.AligneObjet();
                        dmc.Normalize();
                        AddNewObject(Owner.Owner.drawArea, dmc, true);

                        break;
                    }
                    else if (drawArea.GraphicsList[i].GetType() == typeof(DrawGensas) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                    {
                        DrawGensas dgs = (DrawGensas)drawArea.GraphicsList[i];

                        dmc = new DrawMainComposant(drawArea.Owner);

                        dgs.AttachLink(dmc, DrawObject.TypeAttach.Child);
                        dmc.AttachLink(dgs, DrawObject.TypeAttach.Parent);
                        dgs.AligneObjet();
                        dmc.Normalize();
                        AddNewObject(Owner.Owner.drawArea, dmc, true);

                        break;
                    }
                    else if (drawArea.GraphicsList[i].GetType() == typeof(DrawGenpod) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
                    {
                        DrawGenpod dgp = (DrawGenpod)drawArea.GraphicsList[i];

                        dmc = new DrawMainComposant(drawArea.Owner);

                        dgp.AttachLink(dmc, DrawObject.TypeAttach.Child);
                        dmc.AttachLink(dgp, DrawObject.TypeAttach.Parent);
                        dgp.AligneObjet();
                        dmc.Normalize();
                        AddNewObject(Owner.Owner.drawArea, dmc, true);

                        break;
                    }
                }
                drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
                drawArea.Capture = false;
                drawArea.Refresh();
                drawArea.Owner.SetStateOfControls();

            }
            else AddNewObject(drawArea, new DrawMainComposant(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count), true);                        
        }

        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawMainComposant dm = null;
            string sTypeVue = Owner.Owner.tbTypeVue.Text; // (string)Owner.Owner.cbTypeVue.SelectedItem;
            switch (sTypeVue[0])
            {
                case '1': // 1-Applicative
                case 'U':
                    dm = new DrawMainComposant(Owner.Owner, dic);
                    if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dm;
                    else Owner.Owner.drawArea.GraphicsList.Add(dm);
                    break;

                case '2': // 2-Infrastructure
                case '3': // Production --> D-Info Server
                case '4': // Hors-Production --> D-Info Server
                case '5': // Pre-Production --> D-Info Server
                case 'F': // Service Infra --> D-Info Server
                case 'D': // D-Info Server
                    dm = new DrawMainComposant(Owner.Owner, dic);
                    if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dm;
                    else {
                        AddNewObject(Owner.Owner.drawArea, dm, false);
                        //Owner.Owner.drawArea.GraphicsList.Add(dc);
                        CreatObjetLink(dm, "GuidServer", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
                    }
                    break;
            }

            
            return dm;
        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {

            DrawMainComposant dmc;
            string sTypeVue = Owner.Owner.tbTypeVue.Text; // (string)Owner.Owner.cbTypeVue.SelectedItem;
            switch (sTypeVue[0])
            {
                case '1': // 1-Applicative
                case 'U':
                    dmc = new DrawMainComposant(Owner.Owner, LstValue, LstValueG);
                    if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dmc;
                    else AddNewObject(Owner.Owner.drawArea, dmc, false);
                    break;

                case '2': // 2-Infrastructure
                case '3': // Production --> D-Info Server
                case '4': // Hors-Production --> D-Info Server
                case '5': // Pre-Production --> D-Info Server
                case 'F': // Service Infra --> D-Info Server
                case 'D': // D-Info Server
                    bool selected = false;
                    dmc = new DrawMainComposant(Owner.Owner, LstValue, LstValueG);
                    if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = dmc;
                    else
                    {
                        CreatObjetLink(dmc, "GuidServer", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
                        AddNewObject(Owner.Owner.drawArea, dmc, selected);
                        if (dmc.rectangle.X == 0)
                        {
                            ArrayList lstMCompServ = Owner.Owner.oCnxBase.CreatMCompServ(dmc);
                            for (int i = 0; i < lstMCompServ.Count; i++)
                            {
                                Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.ServMComp].LoadSimpleObject((string)lstMCompServ[i]);
                                int j = Owner.GraphicsList.FindObjet(0, (string)lstMCompServ[i]);
                                DrawMCompServ dmcs = (DrawMCompServ)Owner.GraphicsList[j];
                                dmcs.GuidkeyObjet = Guid.NewGuid();
                                dmc.AttachLink(dmcs, DrawObject.TypeAttach.Child);
                                dmcs.AttachLink(dmc, DrawObject.TypeAttach.Parent);
                            }
                        }
                    }
                    break;
            }
        }

        public void AddNodeObjExpFromList(FormExplorObj feo, char Sens, string[] aLists, string[] aListd, ExpObj eo, DrawArea.DrawToolType dt)
        {
            string sType = Owner.Owner.drawArea.tools[(int)dt].GetsType(true);
            string sFinFrom = "", sFinWhere = "", sDebSelect = "d";
            string Senss = "Out", Sensd = "in";
            if (Sens == 'o') { Senss = "In"; Sensd = "Out"; }
            if (aListd.Length > 1)
            {
                sFinFrom = "," + sType;
                sFinWhere = " AND d.Guid" + sType + "=" + sType + ".Guid" + sType + " AND NOT " + sType + ".Guid" + sType + "='" + eo.GuidObj.ToString() + "'";
                sDebSelect = sType;
            }
            else aListd[0] = sType;
            for (int i = 0; i < aLists.Length; i++)
            {
                for (int j = 0; j < aListd.Length; j++)
                {
                    Owner.Owner.oCnxBase.CBAddNodeObjExp(feo, "SELECT DISTINCT " + sDebSelect + ".Guid" + sType + ", Nom" + sType + " FROM " + aLists[i] + " s, " + aListd[j] + " d, Link" + sFinFrom + " WHERE GuidComposant" + Senss + "=s.Guid" + aLists[i] + " AND GuidComposant" + Sensd + "=d.Guid" + aListd[j] + sFinWhere + " AND s.GuidMainComposant='" + eo.GuidObj.ToString() + "'", eo.tn, dt);
                }
            }
        }

        public override void LoadObjectXml(XmlNode Node)
        {
            if (Owner.Owner.tbTypeVue.Text[0] == '1') base.LoadObjectXml(Node);
            else
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
                aGNode = Owner.Owner.GetNode(Node, "MCompApp");

                if (n > -1 && m > -1)
                {
                    t = (Table)ConfDB.LstTable[n];
                    tg = (Table)ConfDB.LstTable[m];
                    LstValue = t.InitValueFieldFromXmlNode(Node);


                    for (int i = 0; i < aGNode.Count; i++)
                    {
                        LstValueG = tg.InitValueFieldFromXmlNode((XmlNode)aGNode[i]);
                        int idx = t.FindField(t.LstField, "GuidServer");
                        if (idx > -1) LstValue[idx] = ((XmlElement)aGNode[i]).GetAttribute("sGuidServer");
                        CreatObjetFromXml(LstValue, LstValueG);
                    }
                }
            }
        }

        public override void CreatObjetFromXml(ArrayList LstValue, ArrayList LstValueG)
        {
            if (Owner.Owner.tbTypeVue.Text[0] == '1') base.CreatObjetFromXml(LstValue, LstValueG);
            else
            {
                DrawMainComposant dm;
                bool selected = false;
                int n;

                dm = new DrawMainComposant(Owner.Owner, LstValue, LstValueG);
                AddNewObject(Owner.Owner.drawArea, dm, selected);
                n = CreatObjetLink(dm, "GuidServer", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
                if (n > -1)
                {
                    DrawServer ds = (DrawServer)Owner.GraphicsList[n];
                    ds.AligneObjet();
                }
                n = CreatObjetLink(dm, "GuidAppUser", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
                if (n > -1)
                {
                    DrawAppUser du = (DrawAppUser)Owner.GraphicsList[n];
                    //du.AligneObjet();
                }
            }
        }

        public override void ExpandObj(FormExplorObj feo,  ExpObj eo)
        {
            //MessageBox.Show(eo.tn.Parent.Text);
            CnxBase ocnx = Owner.Owner.oCnxBase;
            string[] aList1 = { "Composant", "Interface", "Base", "File" };
            string[] aList2 = { "" };

            switch (eo.iCat)
            {
                case -1:
                    ExpandObjRoot(0, ssCat.Length - 1, feo, eo, ssCat);
                    break;
                case 0: //Fonction
                    ocnx.CBAddNodeObjExp(feo, "SELECT Module.GuidModule, NomModule FROM Module WHERE GuidMainComposant='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Module);
                    break;
                case 1: //Interfaces
                    ocnx.CBAddNodeObjExp(feo, "SELECT Interface.GuidInterface, NomInterface FROM Interface WHERE GuidMainComposant='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Interface);
                    break;
                case 2: // Modules
                    ocnx.CBAddNodeObjExp(feo, "SELECT Composant.GuidComposant, NomComposant FROM Composant WHERE GuidMainComposant='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Composant);
                    break;
                case 3: //Fichiers
                    ocnx.CBAddNodeObjExp(feo, "SELECT File.GuidFile, NomFile FROM File WHERE GuidMainComposant='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.File);
                    break;
                case 4: //Bases
                    ocnx.CBAddNodeObjExp(feo, "SELECT Base.GuidBase, NomBase FROM Base WHERE GuidMainComposant='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.Base);
                    break;
                case 5: //Composant en Entree
                    AddNodeObjExpFromList(feo, 'i', aList1, aList2, eo, DrawArea.DrawToolType.AppUser);
                    AddNodeObjExpFromList(feo, 'i', aList1, aList2, eo, DrawArea.DrawToolType.Application);                
                    AddNodeObjExpFromList(feo, 'i', aList1, aList1, eo, DrawArea.DrawToolType.MainComposant);
                    break;
                case 6: //Composant en sortie
                    AddNodeObjExpFromList(feo, 'o', aList1, aList2, eo, DrawArea.DrawToolType.AppUser);
                    AddNodeObjExpFromList(feo, 'o', aList1, aList2, eo, DrawArea.DrawToolType.Application);
                    AddNodeObjExpFromList(feo, 'o', aList1, aList1, eo, DrawArea.DrawToolType.MainComposant);
                    break;
                case 7: //Serveur de Production
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT ServerPhy.GuidServerPhy, NomServerPhy FROM Vue, DansVue, GServerPhy, ServerPhy, ServerLink, MainComposant WHERE Vue.GuidGVue=DansVue.GuidGVue AND DansVue.GuidObjet=GServerPhy.GuidGServerPhy AND GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy AND GuidTypeVue='2a4c3691-e714-4d05-9400-8fbbb06f2d62' AND ServerPhy.GuidServerPhy= ServerLink.GuidServerPhy AND ServerLink.GuidServer=MainComposant.GuidServer AND MainComposant.GuidMainComposant='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.ServerPhy);
                    break;
                case 8: //Serveur de Developpement
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT ServerPhy.GuidServerPhy, NomServerPhy FROM Vue, DansVue, GServerPhy, ServerPhy, ServerLink, MainComposant WHERE Vue.GuidGVue=DansVue.GuidGVue AND DansVue.GuidObjet=GServerPhy.GuidGServerPhy AND GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy AND GuidTypeVue='ef667e58-a617-49fd-91a8-2beeda856475' AND ServerPhy.GuidServerPhy= ServerLink.GuidServerPhy AND ServerLink.GuidServer=MainComposant.GuidServer AND MainComposant.GuidMainComposant='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.ServerPhy);
                    break;
                case 9: //Serveur de Pre-production
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT ServerPhy.GuidServerPhy, NomServerPhy FROM Vue, DansVue, GServerPhy, ServerPhy, ServerLink, MainComposant WHERE Vue.GuidGVue=DansVue.GuidGVue AND DansVue.GuidObjet=GServerPhy.GuidGServerPhy AND GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy AND GuidTypeVue='7afca945-9d41-48fb-b634-5b6ffda90d4e' AND ServerPhy.GuidServerPhy= ServerLink.GuidServerPhy AND ServerLink.GuidServer=MainComposant.GuidServer AND MainComposant.GuidMainComposant='" + eo.GuidObj.ToString() + "'", eo.tn, DrawArea.DrawToolType.ServerPhy);
                    break;
            }

            //base.ExpandObj(eo);
        }

	}

    
}
