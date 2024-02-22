using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.Data.Odbc;
using System.Xml;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolServer3D : DrawTools.ToolRectangle
	{
        private string[] aTypeServer = {"Server",  "AppUser" , "Application"};
        //private string[] aTypeServer = { "Server", "User" };
        private int iTypeServer;
        public XmlExcel xmlLink;

        public ToolServer3D(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            InitPropriete("7bbec2f8-f314-432e-9635-7aa42f4718d8");
		}

        

        public override void CreatObjetsFromBD(bool CloseReader, ConfDataBase.FieldOption ConfField)
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
                LstValue = t.InitValueFieldFromBD(Reader, ConfField);

                if (CloseReader) Reader.Close();
                CreatObjetFromBD(LstValue, null);
            }
            if (CloseReader) Reader.Close();
        }

        public override void LoadObject(char typeData, string sGuidgvue, string sData)
        {
            string Select, From, Where;
            string sType = GetTypeSimpleTable();
            string sGType = "";

            CnxBase ocnx = Owner.Owner.oCnxBase;

            for (iTypeServer = 0; iTypeServer < aTypeServer.Length; iTypeServer++)
            {
                Select = GetSelect(sType, sGType);
                From = GetFrom(sType, sGType);
                Where = GetWhere(sType, sGType, sGuidgvue, sData);
                if (ocnx.CBRecherche(Select + " " + From + " " + Where  + " order by app.trigramme, NomServerPhy"))
                {
                    while (ocnx.Reader.Read())
                    {
                        CreatObjetsFromBD(false, ConfDataBase.FieldOption.Select);
                    }
                    ocnx.CBReaderClose();
                }
                else ocnx.CBReaderClose();
            }
        }

        public override string GetSelect(string sTable, string sGTable)
        {
            string[] aPlus = { ", GuidMainFonction, Fonction.Image", ", '', AppUser.Image", ", '', Application.Image" };
            string[] aTrigramme = { "app.Trigramme", "app.Trigramme", "Application.Trigramme" };
            return "SELECT Distinct ServerPhy.GuidServerPhy, NomServerPhy, Description, Type, CPUcore, RAM, "  + aTrigramme[iTypeServer] + ", " + aTypeServer[iTypeServer] + ".Guid" + aTypeServer[iTypeServer] + ", " + aTypeServer[iTypeServer] + ".Nom" + aTypeServer[iTypeServer] + aPlus[iTypeServer] + ", VlanClass.GuidVlanClass, NomVlanClass, Code";
        }

        public override string GetFrom(string sType, string sGType)
        {
            string[] aPlus = { "Fonction,", "", "" };
            return "From Application app, AppVersion appver, Vue, Environnement, DansVue, GServerPhy, ServerPhy, " + aTypeServer[iTypeServer] + "Link, " + aTypeServer[iTypeServer] + ", " + aPlus[iTypeServer] + " NCard, VLan, VlanClass";
        }

        public override string GetWhere(string sType, string sGType, string GuidGVue, string sGuidVueSrvPhy)
        {
            string[] aPlus = { "and Server.GuidMainFonction=Fonction.GuidFonction", "", "" };
            string sqlString1 = "Where Vue.GuidVue='" + sGuidVueSrvPhy + "' and Vue.GuidAppVersion=appver.GuidAppVersion and appver.GuidApplication=app.GuidApplication and Vue.GuidEnvironnement=Environnement.GuidEnvironnement and Vue.GuidGVue=DansVue.GuidGVue and GuidObjet=GServerPhy.GuidGServerPhy and GServerPhy.GuidServerPhy=ServerPhy.GuidServerPhy and ServerPhy.GuidServerPhy=" + aTypeServer[iTypeServer] + "Link.GuidServerPhy and " + aTypeServer[iTypeServer] + "Link.Guid" + aTypeServer[iTypeServer] + "=" + aTypeServer[iTypeServer] + ".Guid" + aTypeServer[iTypeServer] + " " + aPlus[iTypeServer] + " and ServerPhy.GuidServerPhy=GuidHote and NCard.GuidVLan=VLan.GuidVLan and Vlan.GuidVlanClass=VlanClass.GuidVlanClass";
            string sqlString2 = " and " + aTypeServer[iTypeServer] + ".Guid" + aTypeServer[iTypeServer] + " IN (SELECT Distinct " + aTypeServer[iTypeServer] + ".Guid" + aTypeServer[iTypeServer] + " FROM Vue vinf, Vue v, DansVue, G" + aTypeServer[iTypeServer] + ", " + aTypeServer[iTypeServer] + " WHERE vinf.GuidVue='" + sGuidVueSrvPhy + "' and vinf.GuidVueInf=v.GuidVue and v.GuidGVue=DansVue.GuidGVue AND DansVue.GuidObjet=G" + aTypeServer[iTypeServer] + ".GuidG" + aTypeServer[iTypeServer] + " AND G" + aTypeServer[iTypeServer] + ".Guid" + aTypeServer[iTypeServer] + "=" + aTypeServer[iTypeServer] + ".Guid" + aTypeServer[iTypeServer] + ")";
            if(aTypeServer[iTypeServer]== "AppUser" )
                sqlString2 = " and " + aTypeServer[iTypeServer] + ".Guid" + aTypeServer[iTypeServer] + " IN (SELECT Distinct " + aTypeServer[iTypeServer] + ".Guid" + aTypeServer[iTypeServer] + " FROM Vue vinf, Vue v, DansVue, GTechUser" + ", " + aTypeServer[iTypeServer] + " WHERE vinf.GuidVue='" + sGuidVueSrvPhy + "' and vinf.GuidVueInf=v.GuidVue and v.GuidGVue=DansVue.GuidGVue AND DansVue.GuidObjet=GTechUser" + ".GuidGTechUser" + " AND GTechUser" + ".Guid" + aTypeServer[iTypeServer] + "=" + aTypeServer[iTypeServer] + ".Guid" + aTypeServer[iTypeServer] + ")";

            string sqlString3 = " and VLan.GuidVLan IN (Select VLan.GuidVLan from Vue, DansVue, GVLan, VLan Where Vue.GuidVue='" + sGuidVueSrvPhy + "' and Vue.GuidGVue=DansVue.GuidGVue and GuidObjet=GVLan.GuidGVLan and GVLan.GuidVLan=VLan.GuidVLan)";
            return sqlString1 + sqlString2 + sqlString3;
        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {

            DrawServer3D ds3;
            bool selected = false;
            Table t = (Table)Owner.Owner.oCnxBase.ConfDB.LstTable[Owner.Owner.oCnxBase.ConfDB.FindTable("Server3D")];

            ArrayList lstElServerPhy = xmlLink.XmlGetLstElFromName(xmlLink.root, "ServerPhy");
            int idx = xmlLink.XmlGetNbrElFromNameAndAtt(xmlLink.root, "ServerPhy", "Guid", (string)LstValue[0]);
            if(idx>0)
            {
                
                XmlElement el = xmlLink.XmlFindElFromContaintAtt(xmlLink.root, "Nom", LstValue[t.getIField("Trigramme")] + "_", 0);
                if (el != null)
                {
                    XmlElement elLink = xmlLink.XmlGetFirstElFromName(el, "Link");
                    if (elLink != null)
                    {
                        ds3 = new DrawServer3D(Owner.Owner, LstValue);
                        if (ds3.rectangle.X > -1) AddNewObject3d(Owner.Owner.drawArea, ds3, selected);
                    }
                } else
                {
                    ds3 = new DrawServer3D(Owner.Owner, LstValue);
                    // if ds3.rectangle.X == -1 --> la table vlanClass n'est pas correctement paramétrer (idxLingne, idxDeb, idxMax, code)
                    if (ds3.rectangle.X > -1) AddNewObject3d(Owner.Owner.drawArea, ds3, selected);
                }
            }
        }

        public void AddNewObject3d(DrawArea da, DrawServer3D ds3, bool selected)
        {
            //Existance objet
            int idx = da.GraphicsList.FindObjet(0, ds3.GuidkeyObjet.ToString());
            if (idx < 0)
            {
                DrawGrid dg = (DrawGrid)Owner.Owner.drawArea.GraphicsList[Owner.Owner.drawArea.GraphicsList.Count - 1];
                dg.MajidxDebut((string)ds3.GetValueFromName("GuidVlanClass"));
                AddNewObject(Owner.Owner.drawArea, ds3, selected);
            }
            else
            {
                DrawServer3D ds3find = (DrawServer3D)da.GraphicsList[idx];
                if ((string)ds3find.GetValueFromName("FImage") == "defaut3D.png")
                    ds3find.SetValueFromName("FImage", (string)ds3.GetValueFromName("FImage"));
            }
        }
        
	}
}
