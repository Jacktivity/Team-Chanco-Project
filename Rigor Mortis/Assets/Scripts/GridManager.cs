using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

public class GridManager : MonoBehaviour
{
    public GameObject[] tiles;

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();
    }

    /**
     * Generate Level
     * This creates a reader to get the information from the xml
     * Select the level by name
     * Get each of the map lines in the level
     * Get each value in the arrays, set them to a multidimensional using their position in array as co-ordinates
     * */
    void GenerateLevel()
    {
        var xml = XmlReader<xml.levels>.ReadXML("./Assets/Levels/test_map.xml");

        var map = xml.level.First(m => m.name == "test-level");
        var maplines = map.mapline.Select(m => m.value.Split(',').Select(v => int.Parse(v)).ToArray()).ToArray();

        var anonMap = map.mapline.SelectMany((m,x) => m.value.Split(',').Select((v,z) => new { Value = int.Parse(v), ZPos = z, XPos = x })).ToArray();
        foreach(var pos in anonMap)
        {
            if(pos.Value >= 0)
            {
                GameObject tile = Instantiate(tiles[pos.Value], new Vector3(pos.XPos, 0, pos.ZPos), new Quaternion());
                tile.GetComponent<BlockScript>().coordinates = new Vector3(pos.XPos, 0, pos.ZPos);
            }
        }
    }
    /**
     * This is the xml reader that will get the file by path
     * Serialise the data of the xml
     * Read the data of the file
     * Return the deseriabled data
     */
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
    }
}
namespace xml
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class levels
    {

        private levelsLevel[] levelField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("level")]
        public levelsLevel[] level
        {
            get
            {
                return this.levelField;
            }
            set
            {
                this.levelField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class levelsLevel
    {

        private levelsLevelMapline[] maplineField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("map-line")]
        public levelsLevelMapline[] mapline
        {
            get
            {
                return this.maplineField;
            }
            set
            {
                this.maplineField = value;
            }
        }

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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class levelsLevelMapline
    {

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }


}
