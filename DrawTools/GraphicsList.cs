using System;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Drawing;
using System.Security.Permissions;
using System.Globalization;
using System.Collections;
using System.Diagnostics;
using System.Reflection;


namespace DrawTools
{
	/// <summary>
	/// List of graphic objects
	/// </summary>
    [Serializable]
    public class GraphicsList : ISerializable
    {
        private ArrayList graphicsList;
        private ArrayList graphicsListBatch;
        private ArrayList graphicsListRoot;

        private const string entryCount = "Count";
        private const string entryType = "Type";
        private Form1 owner;

        public Form1 Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
            }
        }

        public GraphicsList(Form1 o)
        {
            this.Owner = o;
            graphicsList = new ArrayList();
            graphicsListBatch = new ArrayList();
            graphicsListRoot = graphicsListBatch;
        }

        public void Switch(bool bWitch)
        {
            if(bWitch)
            {
                ArrayList aTemp;
                aTemp = graphicsList;
                graphicsList = graphicsListBatch;
                graphicsListBatch = aTemp;
                graphicsListRoot = graphicsList;
            }
            else
            {
                graphicsList = new ArrayList();
                graphicsListBatch = new ArrayList();
                graphicsListRoot = graphicsListBatch;
            }
            
        }

        protected GraphicsList(SerializationInfo info, StreamingContext context)
        {
            graphicsList = new ArrayList();

            int n = info.GetInt32(entryCount);
            string typeName;
            object drawObject;

            for ( int i = 0; i < n; i++ )
            {
                typeName = info.GetString(
                    String.Format(CultureInfo.InvariantCulture,
                        "{0}{1}",
                    entryType, i));

                drawObject = Assembly.GetExecutingAssembly().CreateInstance(
                    typeName);

                ((DrawObject)drawObject).LoadFromStream(info, i);
        
                graphicsList.Add(drawObject);
            }

        }

        public void Remove(int i)
        {
            graphicsListRoot.RemoveAt(i);
        }

        /// <summary>
        /// Save object to serialization stream
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(entryCount, graphicsList.Count);

            int i = 0;

            foreach ( DrawObject o in graphicsList )
            {
                info.AddValue(
                    String.Format(CultureInfo.InvariantCulture,
                        "{0}{1}",
                        entryType, i),
                    o.GetType().FullName);

                o.SaveToStream(info, i);

                i++;
            }
        }

        public void Draw(Graphics g)
        {
            int n = graphicsList.Count;
            DrawObject o;

            // Enumerate list in reverse order
            // to get first object on the top
            for (int i = n - 1; i >= 0; i-- )
            {
                o = (DrawObject)graphicsList[i];

                o.Draw(g);

                if ( o.Selected == true )
                {
                    o.DrawTracker(g);
                }
            }
        }

        /// <summary>
        /// Clear all objects in the list
        /// </summary>
        /// <returns>
        /// true if at least one object is deleted
        /// </returns>
        public bool Clear()
        {
            bool result = (graphicsListRoot.Count > 0);
            graphicsListRoot.Clear();
            return result;
        }

        /// <summary>
        /// Count and this [nIndex] allow to read all graphics objects
        /// from GraphicsList in the loop.
        /// </summary>
        public int Count
        {
            get
            {
                return graphicsListRoot.Count;
            }
        }

        public DrawObject this [int index]
        {
            get
            {
                if ( index < 0  ||  index >= graphicsListRoot.Count )
                    return null;

                return ((DrawObject)graphicsListRoot[index]);
            }
        }

        /// <summary>
        /// SelectedCount and GetSelectedObject allow to read
        /// selected objects in the loop
        /// </summary>
        public int SelectionCount
        {
            get
            {
                int n = 0;

                foreach (DrawObject o in graphicsListRoot)
                {
                    if ( o.Selected )
                        n++;
                }

                return n;
            }
        }

        
        public int SelectionCadreRefCount
        {
            get
            {
                int n = 0;

                foreach (DrawObject o in graphicsListRoot)
                {
                    if (o.Selected)
                    {
                        DrawObject dob = (DrawObject) o;
                        if (dob.GetType() == typeof(DrawCadreRefN)) n++;
                    }
                        
                }

                return n;
            }
        }

        public int GetXMax(int y)
        {
            int xmax = 0;
            foreach (DrawObject o in graphicsListRoot)
            {
                int x=0;
                if (y != 0)
                {
                    //int YMin = o.YMin, YMax=
                    if (y >= o.YMin()  && y <= o.YMax()) x = o.XMax();
                }
                else x = o.XMax();
                if (x > xmax) xmax = x;
            }
            return xmax;
        }

        public void PutObjetDefinif()
        {
            foreach (DrawObject o in graphicsListRoot)
            {
                if (o.Temporaire) o.CreatNewGuid();
            }
        }

        public int GetYMax(int x)
        {
            int ymax = 0;
            foreach (DrawObject o in graphicsListRoot)
            {
                int y = 0;
                if (x != 0)
                {
                    if (x >= o.XMin() && x <= o.XMax()) y = o.YMax();
                }
                else y = o.YMax();
                if (y> ymax) ymax = y;
            }
            return ymax;
        }

        public DrawObject GetSelectedObject(int index)
        {
            int n = -1;

            foreach (DrawObject o in graphicsListRoot)
            {
                if ( o.Selected )
                {
                    n++;

                    if ( n == index )
                        return o;
                }
            }
            return null;
        }

        public void Add(DrawObject obj)
        {
            // insert to the top of z-order
            if (FindObjet(0, obj.GuidkeyObjet.ToString()) == -1) graphicsListRoot.Insert(0, obj);
        }

        public void SelectInRectangle(Rectangle rectangle)
        {
            UnselectAll();

            foreach (DrawObject o in graphicsListRoot)
            {
                if ( o.IntersectsWith(rectangle) )
                    o.Selected = true;
            }
        }

        public void UnselectAll()
        {
            if (SelectionCount == 1 && Owner.dataGrid.Rows.Count != 0)
            {
                DrawObject o = GetSelectedObject(0);
                o.SaveProp(Owner.dataGrid);
            }

            foreach (DrawObject o in graphicsListRoot)
            {
                o.Selected = false;
                o.bover = false;
            }
            Owner.ClearPropObjet();
        }

        public void SelectAll()
        {
            foreach (DrawObject o in graphicsListRoot)
            {
                o.Selected = true;
            }
        }

        /// <summary>
        /// Delete selected items
        /// </summary>
        /// <returns>
        /// true if at least one object is deleted
        /// </returns>
        public bool DeleteSelection()
        {
            bool result = false;

            int n = graphicsListRoot.Count;

            for ( int i = n-1; i >= 0; i-- )
            {
                if ( ((DrawObject)graphicsListRoot[i]).Selected )
                {
                    graphicsListRoot.RemoveAt(i);
                    result = true;
                }
            }

            return result;
        }

        public int GetNewX(string sType, int X, int Y)
        {
            int n = graphicsListRoot.Count;
            int x1 = X;

            DrawObject o = null;

            for (int i = 0; i < n; i++)
            {
                o = (DrawObject)graphicsListRoot[i];
                if (o.GetType().Name.Substring("Draw".Length) == sType)
                {
                    if (X == ((DrawRectangle)o).Rectangle.X && Y == ((DrawRectangle)o).Rectangle.Y)
                    {
                        int xn = ((DrawRectangle)o).Rectangle.X + ((DrawRectangle)o).Rectangle.Width + 10;
                        x1 = GetNewX(sType, xn, Y);
                    }
                }
            }
            return x1;
        }

        public void MajValueObjects(string sGuid, string Value0, string Value1)
        {
            int j = FindObjetFromValue(0, 0, sGuid);
            while (j != -1)
            {
                DrawObject o = (DrawObject)graphicsListRoot[j];
                
                o.SetValueFromNameTodgv(Owner.dataGrid, "NomTechnoRef", (object)Value0);
                o.SetValueFromNameTodgv(Owner.dataGrid, "GuidTechnoRef", (object)Value1);
                o.SetValueFromName("NomTechnoRef", (object)Value0);
                o.SetValueFromName("GuidTechnoRef", (object)Value1);
                j = FindObjetFromValue(++j, 0, sGuid);
            }
        }

        public int FindObjet(int debut, string sGuidKeyObjet)
        {
            int n = graphicsListRoot.Count;

            if (sGuidKeyObjet != null && sGuidKeyObjet != "")
            {
                DrawObject o = null;

                for (int i = debut; i < n; i++)
                {
                    o = (DrawObject)graphicsListRoot[i];
                    if (o.GuidkeyObjet.ToString() == sGuidKeyObjet)
                        return i;
                }
            }
            return -1;
        }

        public int FindObjetFromValue(int debut, int idxValue, string sValue)
        {
            int n = graphicsListRoot.Count;

            DrawObject o = null;

            for (int i = debut; i < n; i++)
            {
                o = (DrawObject)graphicsListRoot[i];
                if (o.LstValue!=null && sValue == (string)o.LstValue[idxValue]) return i;
            }
            return -1;
        }

        public int FindObjetG(string sGuidKey)
        {
            int n = graphicsListRoot.Count;

            DrawObject o = null;

            for (int i = 0; i < n; i++)
            {
                o = (DrawObject)graphicsListRoot[i];
                if (o.Guidkey.ToString() == sGuidKey) return i;
            }
            return -1;
        }

        /// <summary>
        /// Move selected items to front (beginning of the list)
        /// </summary>
        /// <returns>
        /// true if at least one object is moved
        /// </returns>
        public void MoveIndexToTop(int index)
        {
            DrawObject o = (DrawObject)graphicsListRoot[index];
            graphicsListRoot.RemoveAt(index);
            graphicsListRoot.Insert(0,o);
        }


        /// <summary>
        /// Move selected items to front (beginning of the list)
        /// </summary>
        /// <returns>
        /// true if at least one object is moved
        /// </returns>
        public bool MoveSelectionToFront()
        {
            int n;
            int i;
            ArrayList tempList;

            tempList = new ArrayList();
            n = graphicsListRoot.Count;

            // Read source list in reverse order, add every selected item
            // to temporary list and remove it from source list
            for ( i = n - 1; i >= 0; i-- )
            {
                if ( ((DrawObject)graphicsListRoot[i]).Selected )
                {
                    tempList.Add(graphicsListRoot[i]);
                    graphicsListRoot.RemoveAt(i);
                }
            }

            // Read temporary list in direct order and insert every item
            // to the beginning of the source list
            n = tempList.Count;

            for ( i = 0; i < n; i++ )
            {
                graphicsListRoot.Insert(0, tempList[i]);
            }

            return ( n > 0 );
        }

        /// <summary>
        /// Move selected items to back (end of the list)
        /// </summary>
        /// <returns>
        /// true if at least one object is moved
        /// </returns>
        public bool MoveSelectionToBack()
        {
            int n;
            int i;
            ArrayList tempList;

            tempList = new ArrayList();
            n = graphicsListRoot.Count;

            // Read source list in reverse order, add every selected item
            // to temporary list and remove it from source list
            for (i = n - 1; i >= 0; i--)
            {
                if (((DrawObject)graphicsListRoot[i]).Selected)
                {
                    tempList.Add(graphicsListRoot[i]);
                    graphicsListRoot.RemoveAt(i);
                }
            }

            // Read temporary list in reverse order and add every item
            // to the end of the source list
            n = tempList.Count;

            for (i = n - 1; i >= 0; i--)
            {
                graphicsListRoot.Add(tempList[i]);
            }

            return (n > 0);
        }

        /*public void MoveToBack(int i)
        {
            graphicsList.Add(graphicsList[i]);
            graphicsList.RemoveAt(i);
        }*/

        public bool MoveLastToBack()
        {
            ArrayList tempList;

            tempList = new ArrayList();

            tempList.Add(graphicsListRoot[0]);
            graphicsListRoot.RemoveAt(0);
            graphicsListRoot.Add(tempList[0]);

            return (true);
        }

        /// <summary>
        /// Get properties from selected objects and fill GraphicsProperties instance
        /// </summary>
        /// <returns></returns>
        private GraphicsProperties GetProperties()
        {
            GraphicsProperties properties = new GraphicsProperties();

            int n = SelectionCount;

            if ( n < 1 )
                return properties;

            DrawObject o = GetSelectedObject(0);

            int firstColor = o.Color.ToArgb();
            int firstPenWidth = o.PenWidth;

            bool allColorsAreEqual = true;
            bool allWidthAreEqual = true;

            for ( int i = 1; i < n; i++ )
            {
                if ( GetSelectedObject(i).Color.ToArgb() != firstColor )
                    allColorsAreEqual = false;

                if ( GetSelectedObject(i).PenWidth != firstPenWidth )
                    allWidthAreEqual = false;
            }

            if ( allColorsAreEqual )
            {
                properties.ColorDefined = true;
                properties.Color = Color.FromArgb(firstColor);
            }

            if ( allWidthAreEqual )
            {
                properties.PenWidthDefined = true;
                properties.PenWidth = firstPenWidth;
            }

            return properties;
        }

        /// <summary>
        /// Apply properties for all selected objects
        /// </summary>
        private void ApplyProperties(GraphicsProperties properties)
        {
            foreach ( DrawObject o in graphicsListRoot )
            {
                if ( o.Selected )
                {
                    if ( properties.ColorDefined )
                    {
                        o.Color = properties.Color;
                        DrawObject.LastUsedColor = properties.Color;
                    }

                    if ( properties.PenWidthDefined )
                    {
                        o.PenWidth = properties.PenWidth;
                        DrawObject.LastUsedPenWidth = properties.PenWidth;
                    }
                }
            }
        }

        /// <summary>
        /// Show Properties dialog. Return true if list is changed
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public bool ShowPropertiesDialog(IWin32Window parent)
        {
            if ( SelectionCount < 1 )
                return false;

            GraphicsProperties properties = GetProperties();
            PropertiesDialog dlg = new PropertiesDialog();
            dlg.Properties = properties;

            if ( dlg.ShowDialog(parent) != DialogResult.OK )
                return false;

            ApplyProperties(properties);

            return true;
        }
	}
}
