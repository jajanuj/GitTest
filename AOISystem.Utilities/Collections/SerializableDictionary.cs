using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AOISystem.Utilities.Collections
{
    [Serializable]
    [XmlRoot("Dictionary")]
    public class SerializableDictionary<KT, VT> : Dictionary<KT, VT>, IXmlSerializable
    {

        public SerializableDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
        public SerializableDictionary()
        {

        }
        public XmlSchema GetSchema()
        {
            return (null);
        }

        public void ReadXml(XmlReader reader)
        {
            Boolean wasEmpty = reader.IsEmptyElement;

            reader.Read();

            if (wasEmpty)
            {
                return;
            }

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                KT key;
                if (reader.Name == "Item")
                {
                    reader.Read();
                    Type keytype = Type.GetType(reader.GetAttribute("type"));
                    if (keytype != null)
                    {
                        reader.Read();
                        key = (KT)new XmlSerializer(keytype).Deserialize(reader);
                        reader.ReadEndElement();
                        Type valuetype = (reader.HasAttributes) ? Type.GetType(reader.GetAttribute("type")) : null;
                        if (valuetype != null)
                        {
                            reader.Read();
                            Add(key, (VT)new XmlSerializer(valuetype).Deserialize(reader));
                            reader.ReadEndElement();
                        }
                        else
                        {
                            Add(key, default(VT));
                            reader.Skip();
                        }
                    }
                    reader.ReadEndElement();

                    reader.MoveToContent();
                }
            }

            reader.ReadEndElement();

        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            for (int i = 0; i < Keys.Count; i++)
            {
                KT key = Keys.ElementAt(i);
                VT value = this.ElementAt(i).Value;
                //create <item>
                writer.WriteStartElement("Item");
                //create <key> under <item>
                writer.WriteStartElement("Key");
                writer.WriteAttributeString(string.Empty, "type", string.Empty, key.GetType().AssemblyQualifiedName);
                new XmlSerializer(key.GetType()).Serialize(writer, key);
                //end </key> element               
                writer.WriteEndElement();
                //create <value> under <item>
                writer.WriteStartElement("value");
                if (value != null)
                {
                    writer.WriteAttributeString(string.Empty, "type", string.Empty, value.GetType().AssemblyQualifiedName);
                    new XmlSerializer(value.GetType()).Serialize(writer, value);
                }
                //end </value>  
                writer.WriteEndElement();
                //end </item>
                writer.WriteEndElement();
            }
        }
    }
}
