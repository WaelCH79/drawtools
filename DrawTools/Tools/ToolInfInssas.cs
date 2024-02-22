using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data.Odbc;


namespace DrawTools
{
	/// <summary>
	/// Module tool
	/// </summary>
	public class ToolInfInssas : DrawTools.ToolRectangle
	{
        public ToolInfInssas(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
            InitPropriete("dc541485-b4f9-4f6b-b2b6-26369cacd772");
		}

        
        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            
        }

        
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {                      
            
        }

        
        public override string GetTypeSimpleTable()
        {
            return "Inssas";
        } 

        public override string GetsType(bool Reel)
        {
            if (Reel) return base.GetsType(Reel);
            return "Inssas";
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawInfInssas dis;
            bool selected = false;

            dis = new DrawInfInssas(Owner.Owner, LstValue, LstValueG);
            AddNewObject(Owner.Owner.drawArea, dis, selected);
            if (dis.rectangle.X == 0)
            {

                selected = true;
                /*
                ArrayList lstInfLabel = Owner.Owner.oCnxBase.CreatInfLabel(dis);
                for (int i = 0; i < lstInfNCard.Count; i++)
                {
                    Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.InfNCard].LoadSimpleObject((string)lstInfNCard[i]);
                    int j = Owner.GraphicsList.FindObjet(0, (string)lstInfNCard[i]);
                    DrawInfNCard dinc = (DrawInfNCard)Owner.GraphicsList[j];
                    dinc.GuidkeyObjet = Guid.NewGuid(); //meme que infserver
                    dis.AttachLink(dinc, DrawObject.TypeAttach.Child);
                    dinc.AttachLink(dis, DrawObject.TypeAttach.Parent);
                }
                */
            }
        }

	}
}
