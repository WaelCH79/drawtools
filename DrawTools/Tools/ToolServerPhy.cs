using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolServerPhy : DrawTools.ToolRectangle
	{       
        public string GetNom(string sObjet, string sGuid)
        {
            return "Nom";
        }

        public string GetCpu(string sObjet, string sGuid)
        {
            return "Cpu";
        }

        public override void initEval(string sFoncEval)
        {
            int i=getIndexEval(sFoncEval);
            if (i > -1)
            {
                switch (i)
                {
                    case 0:
                        foncEval = new FONCEVAL(GetNom);
                        break;
                    case 1:
                        foncEval = new FONCEVAL(GetCpu);
                        break;
                }
            }
        }

        public static string[] ssCat = { "Applications", "Fonctions", "Technologies", "Interfaces", "Materiel", "Baies", "Emplacement"}; 
        public ToolServerPhy(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            InitPropriete("7bbec2f8-f314-432e-9635-7aa42f4718d8");
            lstFonctionEval = new ArrayList();
            lstFonctionEval.Add("nom");
            lstFonctionEval.Add("cpu");
		}

        public bool CreateObjetFromMouse(DrawArea drawArea, int i, string sGuid, Point e)
        {
            DrawServerPhy dsp;
            bool Create = false;

            if ( i >-1 && drawArea.GraphicsList[i].GetType() == typeof(DrawCluster) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
            {
                DrawCluster dc = (DrawCluster)drawArea.GraphicsList[i];
                LoadSimpleObject(sGuid);
                int j = Owner.GraphicsList.FindObjet(0, (string)Owner.Owner.tvObjet.SelectedNode.Name);
                dsp = (DrawServerPhy)Owner.GraphicsList[j];
                dc.AttachLink(dsp, DrawObject.TypeAttach.Child);
                dsp.AttachLink(dc, DrawObject.TypeAttach.Parent);
                dsp.rectangle.X = e.X; dsp.rectangle.Y = e.Y;
                Create = true;
            }
            else
            {
                LoadSimpleObject(sGuid);
                int j = Owner.GraphicsList.FindObjet(0, (string)Owner.Owner.tvObjet.SelectedNode.Name);
                dsp = (DrawServerPhy)Owner.GraphicsList[j];
                dsp.rectangle.X = e.X; dsp.rectangle.Y = e.Y;
                dsp.Normalize();
                Owner.GraphicsList.MoveIndexToTop(j);
                Create = true;
                
            }

            dsp.GetServerLinks( "AppUser" );
            //dsp.GetServerLinks("Application");
            dsp.GetServerLinksApp();
            dsp.GetServerLinksApp("Server");
            dsp.Normalize();

            return Create;
        }


        //--------------------------------- Application ------------------------------------------
        public ArrayList DefauftNivForAppQuery(string GuidObj, bool bOption)
        {
            if (bOption)
                // en tenant compte des application externe
                return LstQuery("SELECT ServerPhy.GuidServerPhy, ServerPhy.NomServerPhy FROM Application, Vue, DansVue, ServerPhy, GServerPhy WHERE Vue.GuidGVue=DansVue.GuidGVue and GuidObjet=GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and Vue.GuidAppVersion=Application.GuidAppVersion and GuidApplication='" + GuidObj + "'");
            else
            {
                string sql1 = "SELECT distinct ServerPhy.GuidServerPhy, ServerPhy.NomServerPhy FROM Application, Vue, DansVue, ServerPhy, GServerPhy, serverlink WHERE Vue.GuidGVue=DansVue.GuidGVue and GuidObjet=GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and ServerPhy.GuidServerPhy=ServerLink.GuidServerPhy and GuidServer IN ";
                string sql2 = "(SELECT Server.GuidServer FROM Vue vinf, Application, Vue v, DansVue, GServer, Server WHERE vinf.GuidVueInf=v.GuidVue and v.GuidGVue=DansVue.GuidGVue and DansVue.GuidObjet=GServer.GuidGServer and GServer.GuidServer=Server.GuidServer and v.GuidAppVersion=Application.GuidAppVersion and GuidApplication = '" + GuidObj + "' ) and Vue.GuidAppVersion=Application.GuidAppVersion and GuidApplication='" + GuidObj + "'";
                return LstQuery(sql1 + sql2);
            }
        }

        //--------------------------------- Application ------------------------------------------
        public override ArrayList CoutForAppQuery(string GuidObj, bool bOption) { return LstQueryCout("SELECT DISTINCT ServerPhy.GuidServerPhy, ServerPhy.NomServerPhy, CPUCore, RAM, CPUCoreA, RAMA FROM Application, Vue, DansVue, ServerPhy, GServerPhy, ServerLink WHERE Vue.GuidGVue=DansVue.GuidGVue and GuidObjet=GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and ServerPhy.GuidServerPhy=ServerLink.GuidServerPhy and Vue.GuidAppVersion=Application.GuidAppVersion and GuidApplication='" + GuidObj + "'"); }
        public override ArrayList FinCommercialForAppQuery(string GuidObj, bool bOption) { return DefauftNivForAppQuery(GuidObj, bOption); }
        public override ArrayList SupportForAppQuery(string GuidObj, bool bOption) { return DefauftNivForAppQuery(GuidObj, bOption); }
        public override ArrayList ComplexiteForAppQuery(string GuidObj, bool bOption) { return DefauftNivForAppQuery(GuidObj, bOption); }
        public override ArrayList ExpertiseForAppQuery(string GuidObj, bool bOption) { return DefauftNivForAppQuery(GuidObj, bOption); }
        public override ArrayList ObsolescenceForAppQuery(string GuidObj, bool bOption) { return DefauftNivForAppQuery(GuidObj, bOption); }
        public override ArrayList BusinessForAppQuery(string GuidObj, bool bOption) { return DefauftNivForAppQuery(GuidObj, bOption); }
        public override ArrayList InstanceForAppQuery(string GuidObj, bool bOption) { return DefauftNivForAppQuery(GuidObj, bOption); }
        public override ArrayList CriticiteForAppQuery(string GuidObj, bool bOption) { return DefauftNivForAppQuery(GuidObj, bOption); }
        public override ArrayList SecuriteForAppQuery(string GuidObj, bool bOption) { return DefauftNivForAppQuery(GuidObj, bOption); }
        public override ArrayList ImpactForAppQuery(string GuidObj, bool bOption) { return DefauftNivForAppQuery(GuidObj, bOption); }
        public override ArrayList ImpactBusinessForAppQuery(string GuidObj, bool bOption) { return DefauftNivForAppQuery(GuidObj, bOption); }


        //--------------------------------- Techno ------------------------------------------
        public ArrayList DefauftNivForTechnoQuery(string GuidObj, bool bOption) { return LstQuery("SELECT DISTINCT ServerLink.GuidServerPhy, NomServerPhy FROM Serverphy, ServerLink, Server, ServerTypeLink, ServerType, Techno WHERE ServerPhy.GuidServerPhy=ServerLink.GuidServerPhy and ServerLink.GuidServer=Server.GuidServer and Server.GuidServer=ServerTypeLink.GuidServer and ServerTypeLink.GuidServerType=ServerType.GuidServerType and ServerType.GuidServerType=Techno.GuidTechnoHost and Techno.GuidTechnoRef='" + GuidObj + "'"); }
        public override ArrayList CoutForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivForTechnoQuery(GuidObj, bOption); }
        public override ArrayList FinCommercialForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivForTechnoQuery(GuidObj, bOption); }
        public override ArrayList SupportForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivForTechnoQuery(GuidObj, bOption); }
        public override ArrayList ComplexiteForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivForTechnoQuery(GuidObj, bOption); }
        public override ArrayList ExpertiseForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivForTechnoQuery(GuidObj, bOption); }
        public override ArrayList ObsolescenceForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivForTechnoQuery(GuidObj, bOption); }
        public override ArrayList BusinessForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivForTechnoQuery(GuidObj, bOption); }
        public override ArrayList InstanceForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivForTechnoQuery(GuidObj, bOption); }
        public override ArrayList CriticiteForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivForTechnoQuery(GuidObj, bOption); }
        public override ArrayList SecuriteForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivForTechnoQuery(GuidObj, bOption); }
        public override ArrayList ImpactForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivForTechnoQuery(GuidObj, bOption); }
        public override ArrayList ImpactBusinessForTechnoQuery(string GuidObj, bool bOption) { return DefauftNivForTechnoQuery(GuidObj, bOption); }


        public override bool CalcIndicator(ObjectAndNiveau objniv, Niveau Niv)
        {
            ArrayList LstNiveau = new ArrayList();

            //Server
            Owner.Owner.oCnxBase.SWwriteLog(12, "Creation de la liste des objets " + GetType().Name.Substring("Tool".Length) + " et de son Niveau par rapport à l'objet " + objniv.sGuid, true);
            Owner.Owner.oCnxBase.CBRecherche("SELECT DISTINCT ServerPhy.NomServerPhy, IndicatorLink.ValIndicator FROM ServerPhy, IndicatorLink WHERE ServerPhy.GuidServerPhy=IndicatorLink.GuidObjet and ServerPhy.GuidServerPhy='" + objniv.sGuid + "' and GuidIndicator='" + Niv.GuidNiveau + "'");
            while (Owner.Owner.oCnxBase.Reader.Read())
            {
                Owner.Owner.oCnxBase.SWwriteLog(14, "Ajout dans la liste l'objet : " + Owner.Owner.oCnxBase.Reader.GetString(0) + "     (" + objniv.sGuid + ")", false);
                if(objniv.oParam!=null){}
                LstNiveau.Add(new ObjectAndNiveau(objniv.sGuid, Owner.Owner.oCnxBase.Reader.GetDouble(1), objniv.oParam));
            }
            Owner.Owner.oCnxBase.CBReaderClose();

            if (LstNiveau.Count == 0) return false; else Niv.Calcul(LstNiveau);
            return true;
        }

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            DrawServerPhy dsp;

            if (drawArea.AddObjet)
            {
                CreateObjetFromMouse(drawArea, GetIndexObjetFromPoint(pt), (string)Owner.Owner.tvObjet.SelectedNode.Name, pt);

                drawArea.AddObjet = false;
                
            }
            else
            {
                dsp = new DrawServerPhy(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);
                AddNewObject(drawArea, dsp, true);
            }
        }

        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            Owner.Owner.drawArea.GraphicsList.MoveSelectionToBack();
            base.OnMouseUp(drawArea, e);
        }


        public override string GetFrom(string sType, string sGType)
        {
            return "From DansVue, " + sType + " Left Join LayerLink On Guid" + sType + "=GuidObj and layerlink.GuidAppVersion='" + Owner.Owner.GetGuidAppVersion()  + "', " + sGType + ", Location, DiskClass, BackupClass, ExploitClass, TechnoRef";
        }

        public override string GetWhere(string sType, string sGType, string GuidGVue, string sGuidVueSrvPhy)
        {
            return "WHERE GuidGVue ='" + GuidGVue + "' and GuidObjet=Guid" + sGType + " and " + sGType + ".Guid" + sType + "=" + sType + ".Guid" + sType + " AND " + sType + ".GuidLocation=Location.GuidLocation" + " AND " + sType + ".GuidDiskClass=DiskClass.GuidDiskClass" + " AND " + sType + ".GuidBackupClass=BackupClass.GuidBackupClass" + " AND " + sType + ".GuidExploitClass=ExploitClass.GuidExploitClass" + " AND " + sType + ".GuidTechnoRef=TechnoRef.GuidTechnoRef" + Owner.Owner.wkApp.GetWhereLayer();
        }

        
        public override string GetSimpleFrom(string sTable)
        {
            return "FROM " + sTable + ", Location, DiskClass, BackupClass, ExploitClass, TechnoRef";
        }

        public override string GetSimpleWhere(string sTable, string GuidObjet)
        {
            return "WHERE Guid" + sTable + "='" + GuidObjet + "' AND " + sTable + ".GuidLocation=Location.GuidLocation" + " AND " + sTable + ".GuidDiskClass=DiskClass.GuidDiskClass" + " AND " + sTable + ".GuidBackupClass=BackupClass.GuidBackupClass" + " AND " + sTable + ".GuidExploitClass=ExploitClass.GuidExploitClass" + " AND " + sTable + ".GuidTechnoRef=TechnoRef.GuidTechnoRef";
        }

        public override string GetSimpleSelect(Table t)
        {
            return t.GetSelectField(ConfDataBase.FieldOption.InterneBD);
        }

        /*
        public override void LoadSimpleObject(string GuidObjet)
        {
            string Select, From, Where;
            string sType = GetTypeSimpleTable();
            CnxBase ocnx = Owner.Owner.oCnxBase;


            Table t;
            int n = ocnx.ConfDB.FindTable(sType);
            if (n > -1)
            {
                t = (Table)ocnx.ConfDB.LstTable[n];
                Select = "SELECT " + t.GetSelectField(ConfDataBase.FieldOption.InterneBD);
                From = GetSimpleFrom(sType);
                Where = GetSimpleWhere(sType, GuidObjet);
                if (ocnx.CBRecherche(Select + " " + From + " " + Where))
                {
                    if (ocnx.Reader.Read())
                    {
                        CreatObjetsFromBD(true, ConfDataBase.FieldOption.Select);
                    }
                    else ocnx.CBReaderClose();
                }
                else ocnx.CBReaderClose();
            }
        }
        */

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {

            DrawServerPhy ds;
            bool selected = false;

            ds = new DrawServerPhy(Owner.Owner, LstValue, LstValueG);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = ds;
            else
            {
                int j = Owner.Owner.drawArea.GraphicsList.FindObjet(0, ds.GuidkeyObjet.ToString());
                if (j < 0)
                {
#if CLUSTERREADY
#else
                    CreatObjetLink(ds, "GuidCluster", DrawObject.TypeAttach.Parent, DrawObject.TypeAttach.Child);
#endif
                    if (ds.rectangle.X == 0)
                    {
                        selected = true;
                        AddNewObject(Owner.Owner.drawArea, ds, selected);
                        string sTypeVue = Owner.Owner.tbTypeVue.Text; // (string)Owner.Owner.cbTypeVue.SelectedItem;
                        switch (sTypeVue[0])
                        {
                            case '3': // 3-Production
                            case '5':
                            case '4':
                            case 'F':
                            case 'U':

                                ArrayList lstNCard = Owner.Owner.oCnxBase.CreatNcardHote(ds);

                                for (int i = 0; i < lstNCard.Count; i++)
                                {
                                    Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.NCard].LoadSimpleObject((string)lstNCard[i]);
                                    j = Owner.GraphicsList.FindObjet(0, (string)lstNCard[i]);
                                    DrawNCard dc = (DrawNCard)Owner.GraphicsList[j];
                                    ds.AttachLink(dc, DrawObject.TypeAttach.Child);
                                    dc.AttachLink(ds, DrawObject.TypeAttach.Parent);
                                }
                                break;
                            case 'A':
                                break;
                        }

                    }
                    else
                        AddNewObject(Owner.Owner.drawArea, ds, selected);
                }
            }
        }

        public override void ExpandObj(FormExplorObj feo, ExpObj eo)
        {
            //MessageBox.Show(eo.tn.Parent.Text);
            CnxBase ocnx = Owner.Owner.oCnxBase;
            string sVersion = "Application.GuidAppVersion = AppVersion.GuidAppVersion";
            if (eo.bChkAllVersion) sVersion = "Application.GuidApplication = AppVersion.GuidApplication"; 

            switch (eo.iCat)
            {
                case -1:
                    ExpandObjRoot(0, ssCat.Length - 1, feo, eo, ssCat);
                    break;
                case 0: //Applications
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Application.GuidApplication, NomApplication FROM Application, AppVersion, Vue, DansVue, GServerPhy, ServerPhy WHERE " + sVersion + " AND AppVersion.GuidAppVersion = Vue.GuidAppVersion AND Vue.GuidGVue = DansVue.GuidGVue AND DansVue.GuidObjet = GServerPhy.GuidGServerPhy AND GServerPhy.GuidServerPhy = ServerPhy.GuidServerPhy AND ServerPhy.GuidServerPhy='" + eo.GuidObj.ToString() + "' Order By NomApplication", eo.tn, DrawArea.DrawToolType.Application);
                    break;
                case 1: // Fonctions
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT Fonction.GuidFonction, NomFonction FROM Fonction, Server, ServerLink WHERE Fonction.GuidFonction=Server.GuidMainFonction AND Server.GuidServer=ServerLink.GuidServer AND GuidServerPhy='" + eo.GuidObj.ToString() + "' Order By NomFonction", eo.tn, DrawArea.DrawToolType.Fonction);
                    break;
                case 2: //Technologies
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT TechnoRef.GuidTechnoRef, NomTechnoRef FROM TechnoRef, Techno, ServerType, ServerTypeLink, Server, ServerLink WHERE TechnoRef.GuidTechnoRef=Techno.GuidTechnoRef AND Techno.GuidTechnoHost=ServerType.GuidServerType AND ServerType.GuidServerType=ServerTypeLink.GuidServerType AND ServerTypeLink.GuidServer=Server.GuidServer AND Server.GuidServer=ServerLink.GuidServer AND ServerLink.GuidServerPhy='" + eo.GuidObj.ToString() + "' Order By NomTechnoRef", eo.tn, DrawArea.DrawToolType.TechnoRef);
                    break;
                case 3: //Interfaces
                    break;
                case 4: //Materiel
                    
                    break;
                case 5: //Baies
                    
                    break;
                case 6: //Emplacement
                    break;
                case 7:
                    break;
            }
        }
	}
}
