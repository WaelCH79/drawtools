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


    public class CadreRefN1
    {
        public string GuidCadreRefN1;
        public ArrayList aIndicator;
        public enum Indicator
        {
            iDateSup,
            iNorme,
            iUsed,
            NbrIndicator
        }

        public CadreRefN1(string sGuid)
        {
            GuidCadreRefN1 = sGuid;
            aIndicator = new ArrayList();
        }
    }

	public class ToolCadreRefN : DrawTools.ToolRectangle
	{
        public ToolCadreRefN(DrawArea da)
		{
            Owner = da;
            Cursor = new Cursor(GetType(), "module.cur");
		}

        public bool CreateObjetFromMouse(DrawArea drawArea, int i, string sGuid, Point e)
        {
            DrawCadreRefN dcrn;
            bool Create = false;

            if (drawArea.GraphicsList[i].GetType() == typeof(DrawCadreRefN1) && drawArea.GraphicsList[i].ParentPointInObject(new Point(e.X, e.Y)))
            {
                DrawCadreRefN1 dcrn1 = (DrawCadreRefN1)drawArea.GraphicsList[i];
                LoadSimpleObject(sGuid);
                dcrn = (DrawCadreRefN)Owner.GraphicsList[0];

                dcrn1.AttachLink(dcrn, DrawObject.TypeAttach.Child);
                dcrn.AttachLink(dcrn1, DrawObject.TypeAttach.Parent);
                dcrn.rectangle.X = e.X; dcrn.rectangle.Y = e.Y;
                dcrn.Normalize();
                Create = true;
            }
            else
            {
                LoadSimpleObject(sGuid);
                dcrn = (DrawCadreRefN)Owner.GraphicsList[0];
                dcrn.rectangle.X = e.X; dcrn.rectangle.Y = e.Y;
                dcrn.Normalize();
                Create = true;
            }
            return Create;
        }
        
        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            DrawCadreRefN dcrn;

            if (drawArea.AddObjet)
            {
                drawArea.AddObjet = false;

                LoadSimpleObject((string)Owner.Owner.tvObjet.SelectedNode.Name);
                int n = Owner.Owner.drawArea.GraphicsList.FindObjet(0, (string)Owner.Owner.tvObjet.SelectedNode.Name);
                while(((DrawObject)Owner.GraphicsList[n]).GetType()==typeof(DrawCadreRefN1))
                    n = Owner.Owner.drawArea.GraphicsList.FindObjet(n+1, (string)Owner.Owner.tvObjet.SelectedNode.Name);
                dcrn = (DrawCadreRefN)Owner.GraphicsList[n];
                dcrn.rectangle.X = e.X; dcrn.rectangle.Y = e.Y;
                dcrn.Normalize();
                if (Owner.Owner.tvObjet.SelectedNode.Nodes[0].Nodes.Count == 0) dcrn.SetValueFromName("Couleur", "Coral");
                
            }
            else
            {
                dcrn = new DrawCadreRefN(drawArea.Owner, e.X, e.Y, 1, 1, drawArea.GraphicsList.Count);

                AddNewObject(drawArea, dcrn, true);

            }
        }
       
        public override void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
        {
            drawArea.Cursor = Cursor;
            DrawCadreRefN dcrn;
            
            if (e.Button == MouseButtons.Left)
            {
                int n = Owner.Owner.drawArea.GraphicsList.FindObjet(0, (string)Owner.Owner.tvObjet.SelectedNode.Name);
                while (((DrawObject)Owner.GraphicsList[n]).GetType() == typeof(DrawCadreRefN1))
                    n = Owner.Owner.drawArea.GraphicsList.FindObjet(n + 1, (string)Owner.Owner.tvObjet.SelectedNode.Name);
                dcrn = (DrawCadreRefN)Owner.GraphicsList[n];
                int Height = dcrn.HEIGHTCADREREF;
                int HeightIndicator = dcrn.HEIGHTINDICATOR;

                Point point = new Point(e.X, dcrn.Rectangle.Top + 2 * Height + HeightIndicator + 10);
                dcrn.MoveHandleTo(point, 5);
                drawArea.Refresh();
            }
        }

        public override string GetTypeSimpleTable()
        {
            return "CadreRef";
        }

        public void CreateCadreRefN1_Indicator(DrawCadreRefN dcrn)
        {
            ArrayList aCadreRefN1 = dcrn.FindCadreRefN1FromTv();
            int Axe = dcrn.AXE;
            int Height = dcrn.HEIGHTCADREREF;
            int HeightIndicator = dcrn.HEIGHTINDICATOR;
            ArrayList aDateSup = new ArrayList();
            ArrayList aNorme = new ArrayList();
            ArrayList aUsed = new ArrayList();
            
            aDateSup.Add(0.0);
            
            aNorme.Add(0.0);
            
            aUsed.Add(0.0); //nbr used N-1
            aUsed.Add(0.0); //nbr used N
            aUsed.Add(0.0); //nbr used N+1


            CadreRefN1 crn = new CadreRefN1("");
            crn.aIndicator.Add(aDateSup);
            crn.aIndicator.Add(aNorme);
            crn.aIndicator.Add(aUsed);
            ArrayList aI;

            for (int i = 0; i < aCadreRefN1.Count; i++)
            {
                CadreRefN1 cr = (CadreRefN1)aCadreRefN1[i];
                Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.CadreRefN1].LoadSimpleObject(cr.GuidCadreRefN1);
                int j = Owner.GraphicsList.FindObjet(0, cr.GuidCadreRefN1);

                DrawCadreRefN1 dcrn1 = (DrawCadreRefN1)Owner.GraphicsList[j];
                dcrn.AttachLink(dcrn1, DrawObject.TypeAttach.Child);
                dcrn1.AttachLink(dcrn, DrawObject.TypeAttach.Parent);

                aI = (ArrayList)cr.aIndicator[(int)CadreRefN1.Indicator.iDateSup];
                if ((double)aDateSup[0] < (double)aI[0]) aDateSup[0] = aI[0];

                aI = (ArrayList)cr.aIndicator[(int)CadreRefN1.Indicator.iNorme];
                if ((double)aNorme[0] < (double)aI[0]) aNorme[0] = aI[0];

                //if ((int)crn.aIndicator[(int)CadreRefN1.Indicator.iNorme] < (int)cr.aIndicator[(int)CadreRefN1.Indicator.iNorme])
                //    crn.aIndicator[(int)CadreRefN1.Indicator.iNorme] = cr.aIndicator[(int)CadreRefN1.Indicator.iNorme];

                aI = (ArrayList) cr.aIndicator[(int)CadreRefN1.Indicator.iUsed];
                for (int n = 0; n < aI.Count; n++) aUsed[n] = (double)aUsed[n] + (double)aI[n];
            }

            aI = (ArrayList)crn.aIndicator[(int)CadreRefN1.Indicator.iNorme];
            aI[0] = (double)aI[0] + (double)Form1.ImgList.NetSstG;
            
            if (dcrn.LstChild.Count != 0)
            {
                for (int i = 0; i < (int)CadreRefN1.Indicator.NbrIndicator; i++)
                {
                    Owner.Owner.drawArea.tools[(int)DrawArea.DrawToolType.Indicator].CreatObjetFromCadreRef((ArrayList)crn.aIndicator[i]);

                    DrawIndicator di = (DrawIndicator)Owner.GraphicsList[0];
                    dcrn.AttachLink(di, DrawObject.TypeAttach.Child);
                    di.AttachLink(dcrn, DrawObject.TypeAttach.Parent);
                }
            }


        }

        /// <summary>
        /// Left mouse is released.
        /// New object is created and resized.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            DrawCadreRefN dcrn; // = (DrawCadreRefN)drawArea.GraphicsList[0];

            int n = Owner.Owner.drawArea.GraphicsList.FindObjet(0, (string)Owner.Owner.tvObjet.SelectedNode.Name);
            while (((DrawObject)Owner.GraphicsList[n]).GetType() == typeof(DrawCadreRefN1))
                n = Owner.Owner.drawArea.GraphicsList.FindObjet(n + 1, (string)Owner.Owner.tvObjet.SelectedNode.Name);
            dcrn = (DrawCadreRefN)Owner.GraphicsList[n];
            dcrn.Normalize();
            //CreateCadreRefN1_Indicator(dcrn);
                       
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
            drawArea.Owner.SetStateOfControls();
        }

        public override string GetWhere(string sType, string sGType, string GuidGVue, string sGuidVueSrvPhy)
        {
            return "WHERE GuidGVue ='" + GuidGVue + "' and GuidObjet=Guid" + sGType + " and " + sGType + ".Guid" + sType + "=" + sType + ".Guid" + sType;
        }

        public override void LoadObject(char TypeData, string sGuidgvue, string sData)
        {
            string Select, From, Where;
            string sType = GetTypeSimpleTable();
            string sGType = GetTypeSimpleGTable();
            //string sGType = GetTypeSimpleTable();

            CnxBase ocnx = Owner.Owner.oCnxBase;
            ArrayList aGuidCadreREf = new ArrayList();

            Select = "SELECT CadreRef.GuidCadreRef ";
            From = GetFrom(sType, sGType);
            Where = GetWhere(sType, sGType, sGuidgvue, sData);
            if (ocnx.CBRecherche(Select + " " + From + " " + Where))
            {
                while (ocnx.Reader.Read()) aGuidCadreREf.Add(ocnx.Reader.GetString(0));
                ocnx.CBReaderClose();
            }
            else ocnx.CBReaderClose();

            Select = GetSelect(sType, sGType);
            for (int i = 0; i < aGuidCadreREf.Count; i++)
            {
                ocnx.CBRecherche(Select + " " + From + " " + Where + " AND CadreRef.GuidCadreRef='" + aGuidCadreREf[i] + "'");
                CreatObjetsFromBD(true, ConfDataBase.FieldOption.Select);
                ocnx.CBReaderClose();

                DrawCadreRefN dcrn; // = (DrawCadreRefN)drawArea.GraphicsList[0];
                int n = Owner.Owner.drawArea.GraphicsList.FindObjet(0, (string)aGuidCadreREf[i]);
                if ((Owner.GraphicsList[n]).GetType() != typeof(DrawCadreRefN)) n = Owner.Owner.drawArea.GraphicsList.FindObjet(++n, (string)aGuidCadreREf[i]);
                dcrn = (DrawCadreRefN)Owner.GraphicsList[n];
                dcrn.AligneObjet();
            }            
        }

        public override void CreatObjetFromBD(ArrayList LstValue, ArrayList LstValueG, bool bSelectedRoot = false)
        {
            DrawCadreRefN dcrn;
            bool selected = false;

            dcrn = new DrawCadreRefN(Owner.Owner, LstValue, LstValueG);
            if (dcrn.rectangle.X == 0) selected = true;
            CreateCadreRefN1_Indicator(dcrn);
            AddNewObject(Owner.Owner.drawArea, dcrn, selected);
            Owner.Owner.drawArea.GraphicsList.MoveLastToBack();


            //base.CreatObjetFromBD(From1 f, LstValue, LstValueG);
        }

	}

    
}
