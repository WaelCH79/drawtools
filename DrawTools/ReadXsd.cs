using System;
using System.Xml.Schema;
using System.Xml;


namespace DrawTools
{
	/// <summary>
	/// Ellipse tool
	/// </summary>
    public class ReadXsd
    {
        public ReadXsd()
        {
        }

        public void ValidationCallback(object sender, ValidationEventArgs args)
        {
        }


        public void OpenXsd(string fileXsd)
        {
            string sType;
            XmlTextReader reader = new XmlTextReader(fileXsd);
            XmlSchema myschema = new XmlSchema();
            XmlSchema.Read(reader, ValidationCallback);

            for (int i = 0; i < myschema.Items.Count; i++)
            {
                sType = myschema.Items[i].ToString();
            }

            while (reader.Read())
            {
                //sType = reader.NodeType.ToString();
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        sType = reader.Name;

                        for (int i = 0; i < reader.AttributeCount; i++)
                        {
                            sType = reader.GetAttribute(i);
                        }
                        break;
                }
            }
        }
    }

}
