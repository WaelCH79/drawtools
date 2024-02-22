using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using MOI = Microsoft.Office.Interop;
 

namespace DrawTools
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	public class DrawServer3D : DrawTools.DrawRectangle
	{
        public int thickColor
        {
            get
            {
                return (int) this.GetValueFromName("thickColor");
            }
            set
            {
                this.InitProp("thickColor", (object) value, true);
            }
        }

        public int forme
        {
            get
            {
                return (int)this.GetValueFromName("Forme"); ;
            }
            set
            {
                this.InitProp("Forme", (object)value, true);
            }
        }

        public int CPUCoreA
        {
            get
            {
                return (int)this.GetValueFromName("CPUCoreA"); ;
            }
            set
            {
                this.InitProp("CPUCoreA", (object)value, true);
            }
        }

        public int RAMA
        {
            get
            {
                return (int)this.GetValueFromName("RAMA"); ;
            }
            set
            {
                this.InitProp("RAMA", (object)value, true);
            }
        }

        public DrawServer3D()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}
        
        public DrawServer3D(Form1 of, ArrayList lstVal)
        {
            F = of;
            object o= null;
            OkMove = false;
            LstParent = null;
            LstChild = null;
            LstLinkIn = null;
            LstLinkOut = null;
            LstValue = lstVal;
            Guidkey = Guid.NewGuid();

            o = GetValueFromLib("Guid");
            if(o!=null)
                GuidkeyObjet = new Guid((string) o);
            
            o = GetValueFromLib("Nom");
            if (o != null)
                Texte = (string)o;
            DrawGrid dg = (DrawGrid)F.drawArea.GraphicsList[F.drawArea.GraphicsList.Count-1];
            Point pt = dg.GetidxPoint((string)GetValueFromName("GuidVlanClass"));
            //SetValueFromName("CodeVlanClass", dg.GetCode((string)GetValueFromName("GuidVlanClass")));
            
            if (pt.X < 0) Rectangle = new Rectangle(-1, -1, 0, 0);
            else
            {
                SetValueFromName("IdxLigne", (object)pt.X);
                ArrayList lstpt = (ArrayList)dg.lstLigne[pt.X];
                Rectangle = new Rectangle(((Point)lstpt[pt.Y]).X, ((Point)lstpt[pt.Y]).Y, 0, 0);
            }
            // initialisation de fichier image
            string sType = (string) GetValueFromName("Type");
            if ((string)GetValueFromName("NomServer") == "") sType = "e";

            //if (GetValueFromName("Image") != null && GetValueFromName("Type") !=null && GetValueFromName("Code") !=null)
            //{
                string sImage = (string)GetValueFromName("Image") + "3D" + sType + (string)GetValueFromName("Code") + ".png";
                if(File.Exists(F.sPathRoot +  @"\bouton\" + sImage)) SetValueFromName("FImage", sImage);
                else
                SetValueFromName("FImage", "defaut3D.png");
            //}
            //else SetValueFromName("FImage", "defaut3D.png");
            
            Initialize();
        }
                
        public override void Draw(Graphics g)
        {

            if (Rectangle.Left >= 0)
            {
                Bitmap image2 = (Bitmap)Image.FromFile(F.sPathRoot + @"\bouton\" + (string)GetValueFromName("FImage"), true);
                double wImage = (lozangeFace - 5) * Math.Cos(radianFace);
                rectangle.Width = (int) wImage;
                rectangle.Height = (int)wImage * image2.Height / image2.Width;
                g.DrawImage(image2, Rectangle);

                DrawGrpTxt1(g, 3, -1, rectangle.Left - 5, rectangle.Top + 50, 0, Color.DarkBlue, Color.White);

            }
        }

        
                
        /// <summary>
        /// Move handle to new point (resizing)
        /// </summary>
        /// <param name="point"></param>
        /// <param name="handleNumber"></param>
        public override void MoveHandleTo(Point point, int handleNumber)
        {
            int left = Rectangle.Left;
            int top = Rectangle.Top;
            int right = Rectangle.Right;
            int bottom = Rectangle.Bottom;

            switch (handleNumber)
            {
                case 1:
                    left = point.X;
                    top = point.Y;
                    break;
                case 2:
                    top = point.Y;
                    break;
                case 3:
                    right = point.X;
                    top = point.Y;
                    break;
                case 4:
                    right = point.X;
                    break;
                case 5:
                    right = point.X;
                    bottom = point.Y;
                    break;
                case 6:
                    bottom = point.Y;
                    break;
                case 7:
                    left = point.X;
                    bottom = point.Y;
                    break;
                case 8:
                    left = point.X;
                    break;
            }

            SetRectangle(left, top, right - left, bottom - top);
        }

        

        public override void dataGrid_CellClick(DataGridView odgv, DataGridViewCellEventArgs e)
        {
            //if (odgv.CurrentCell.RowIndex == 2) // Ligne Link Applicatif
            int n;

            n = GetIndexFromName("NomLocation");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromTv("8-Localisation");
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }

            n = GetIndexFromName("GuidAppUser");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromTv("TemplateUser");
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }

            n = GetIndexFromName("GuidApplication");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromTv("TemplateApplication");
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }

            n = GetIndexFromName("GuidServer");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromDB("Select Server.GuidServer, NomServer From Server, Vue, DansVue, GServer Where Vue.GuidVue='" + F.sGuidVueInf + "' and Vue.GuidGVue=DansVue.GuidGVue AND GuidObjet=GuidGServer AND GServer.GuidServer=Server.GuidServer", "Value");

                //fcp.AddlSourceFromTv("TemplateServer");
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }
            n = GetIndexFromName("NomDiskClass");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromDB("Select GuidDiskClass, NomDiskClass From DiskClass", "Value");
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }
            n = GetIndexFromName("NomBackupClass");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromDB("Select GuidBackupClass, NomBackupClass From BackupClass", "Value");
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }
            n = GetIndexFromName("NomExploitClass");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromDB("Select GuidExploitClass, NomExploitClass From ExploitClass", "Value");
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }
            n = GetIndexFromName("NomTechnoRef");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormChangeProp fcp = new FormChangeProp(F, odgv);
                fcp.AddlSourceFromTv("Patrimoine");
                fcp.AddlDestinationFromProp(this.GetType().Name.Substring("Draw".Length));
                fcp.ShowDialog(F);
            }
            n = GetIndexFromName("Indicator");
            if (n > -1 && e.RowIndex == n) // Link Serveur d'Infra
            {
                FormIndicator fi = new FormIndicator(F, GuidkeyObjet.ToString());
                fi.ShowDialog(F);
            }
        }

	}
}
