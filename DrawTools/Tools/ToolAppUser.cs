using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolAppUser : DrawTools.ToolRectangle
	{
        public static string[] ssCat = { "Applications"};
        public ToolAppUser(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            //initialisation des proprietes
            oLayers = new LayerList[1];
            oLayers[0] = new LayerList("b6c2787f-e024-4a15-8f3b-1c72d7e3fbfb");
            oLayers[0].AddTemplate(Owner.Owner, "", Owner.Owner.sGuidTemplate);

		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawAppUser du;
            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;
                LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);

                du = (DrawAppUser)Owner.GraphicsList[0];
                du.rectangle.X = e.X; du.rectangle.Y = e.Y;
                du.Normalize();
            }
            else
            {
                du = new DrawAppUser(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);
                AddNewObject(drawArea, du, true);
            }
        }

        public override DrawObject CreatObjetFromJson(Dictionary<string, object> dic)
        {
            DrawAppUser du;

            du = new DrawAppUser(Owner.Owner, dic);
            if (Owner.Owner.oCureo != null) Owner.Owner.oCureo.oDraw = du;
            else Owner.Owner.drawArea.GraphicsList.Add(du);
            return du;
        }


        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawAppUser du;
            bool selected = false;

            du = new DrawAppUser(Owner.Owner, LstValue, LstValueG);
            if (du.rectangle.X == 0) selected = true;
            AddNewObject(Owner.Owner.drawArea, du, selected);

            TreeNode[] ArrayTreeNode = Owner.Owner.tvObjet.Nodes.Find((string)LstValue[0], true);
            if (ArrayTreeNode.Length == 1) ArrayTreeNode[0].Remove();
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
                    ocnx.CBAddNodeObjExp(feo, "SELECT DISTINCT App.GuidApplication, App.NomApplication FROM Application App, AppVersion, Vue, DansVue, GAppUser, AppUser WHERE App.GuidApplication = AppVersion.GuidApplication AND AppVersion.GuidAppVersion = Vue.GuidAppVersion AND Vue.GuidGVue = DansVue.GuidGVue AND DansVue.GuidObjet = GAppUser.GuidGAppUser AND GAppUser.GuidAppUser = AppUser.GuidAppUser AND AppUser.GuidAppUser ='" + eo.GuidObj.ToString() + "' Order by App.NomApplication", eo.tn, DrawArea.DrawToolType.Application);
                    break;
            }

            //base.ExpandObj(eo);
        }
    }
}
