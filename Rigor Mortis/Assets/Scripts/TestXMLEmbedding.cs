using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class TestXMLEmbedding : MonoBehaviour
{
    [SerializeField] private TextAsset sampleXML;
    // Start is called before the first frame update
    void Start()
    {

        var test = XmlReader<Xml.TestXml>.ReadXMLFromBytes(sampleXML.bytes);

        Debug.Log(test.Data.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class XmlReader<T> where T : class
{
    public static T ReadXML(string path)
    {
        var _serializer = new XmlSerializer(typeof(T));

        var xml = File.ReadAllBytes(path);

        using (var memoryStream = new MemoryStream(xml))
        {
            using (var reader = new XmlTextReader(memoryStream))
            {
                return (T)_serializer.Deserialize(reader);
            }
        }
    }

    public static T ReadXMLFromBytes(byte[] xmlData)
    {
        var _serializer = new XmlSerializer(typeof(T));

        using (var memoryStream = new MemoryStream(xmlData))
        {
            using (var reader = new XmlTextReader(memoryStream))
            {
                return (T)_serializer.Deserialize(reader);
            }
        }
    }
}

namespace Xml
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class TestXml
    {

        private TestXmlData dataField;

        /// <remarks/>
        public TestXmlData Data
        {
            get
            {
                return this.dataField;
            }
            set
            {
                this.dataField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class TestXmlData
    {

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }


}