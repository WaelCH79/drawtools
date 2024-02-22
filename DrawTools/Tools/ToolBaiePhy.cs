using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolBaiePhy : DrawTools.ToolRectangle
	{
        public ToolBaiePhy(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            DrawBaiePhy db;

            if (drawArea.AddObjet)
            {
                string Element;
                drawArea.AddObjet = false;
                LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);
                db = (DrawBaiePhy)Owner.GraphicsList[0];
                db.rectangle.X = e.X; db.rectangle.Y = e.Y;
                db.Normalize();

                Element = db.FindElementFromTv();
                if (Element != null)
                {
                    Owner.Owner.oCnxBase.CmdText = "";
                    FormChangeProp fcp = new FormChangeProp(Owner.Owner, null);
                    fcp.AddlSourceFromString(Element);
                    fcp.ShowDialog(Owner.Owner);
                    if (fcp.Valider)
                    {
                        string[] aValue;
                        
                        aValue = Owner.Owner.oCnxBase.CmdText.Split('(', ')');
                        db.NbrCreatChild = (aValue.Length - 1) / 2;
                        ToolMachineCTI tm = (ToolMachineCTI)drawArea.tools[(int)DrawArea.DrawToolType.MachineCTI];
                        ToolDrawer td = (ToolDrawer)drawArea.tools[(int)DrawArea.DrawToolType.Drawer];

                        for (int i = 1; i < aValue.Length; i += 2)
                        {
                            if (aValue[i - 1].IndexOf("-D") == -1) tm.CreateSrv(drawArea, aValue[i], new Point(db.rectangle.X, db.rectangle.Y), db.GuidkeyObjet.ToString());
                            else td.CreateSrv(drawArea, aValue[i], new Point(db.rectangle.X, db.rectangle.Y), db.GuidkeyObjet.ToString());
                        }
                    }
                }
            }
            else
            {
                db = new DrawBaiePhy(drawArea.Owner, e.X, e.Y, 1, drawArea.GraphicsList.Count);
                AddNewObject(drawArea, db, true);
                drawArea.GraphicsList[0].Normalize();
            }
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;

            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawBaiePhy db;

            db = new DrawBaiePhy(Owner.Owner, LstValue, LstValueG);
            AddNewObject(Owner.Owner.drawArea, db, false);
            //base.CreatObjetFromBD(From1 f, LstValue, LstValueG);
        }

	}

    
}
