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
    [SerializeField] private GameObject[] tiles;
    [SerializeField] private GameObject enemyContainter;
    [SerializeField] private Character[] enemyPrefabs;
    [SerializeField] private GameObject playerContainter;
    [SerializeField] public Character[] playerPrefabs;
    [SerializeField] private TextAsset levelMap;
    [SerializeField] private Character SelectedUnit;
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private HealthBarManager eventSystem;
    public bool moveMode;
    public BlockScript selectedBlock;

    private int placementPoints;

    GridXML.levels xmlData;

    // Start is called before the first frame update
    void Start()
    {
        xmlData = XmlReader<GridXML.levels>.ReadXMLAsBytes(levelMap.bytes);
        GenerateLevel();
        PlaceEnemy();
        UnitPlacement();
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
        var level = xmlData.level;
       
        var maplines = level.map.mapline.Select(m => m.value.Split(',').Select(v => int.Parse(v)).ToArray()).ToArray();

        var anonMap = level.map.mapline.SelectMany((m, x) => m.value.Split(',').Select((v, z) => new { Value = int.Parse(v), ZPos = z, XPos = x })).ToArray();
        foreach(var pos in anonMap)
        {
            if (pos.Value >= 0)
            {
                GameObject tile = Instantiate(tiles[pos.Value], new Vector3(pos.XPos, 0, pos.ZPos), tiles[pos.Value].transform.rotation, gameObject.transform);
                tile.GetComponent<BlockScript>().coordinates = new Vector3(pos.XPos, 0, pos.ZPos);
                tile.name = tile.name.Replace("(Clone)", "");
                tile.name = tile.name + '(' + pos.XPos + ',' + pos.ZPos + ')';
            }
        }

        placementPoints = level.map.placementPoints;
    }

    void PlaceEnemy()
    {
        var enemies = xmlData.level.enemies;

        foreach(var enemy in enemies)
        {
            Character placedEnemy = Instantiate(enemyPrefabs[enemy.type], new Vector3(enemy.posX, 1, enemy.posZ), new Quaternion(), enemyContainter.transform);
            placedEnemy.name = enemy.name;
            placedEnemy.tag = "Enemy";
          // eventSystem.AddUnit(placedEnemy);
        }
    }

    void UnitPlacement()
    {
        var placeables = xmlData.level.placeables;

        var map = gameObject.GetComponentsInChildren<BlockScript>();

        var placeableTiles = placeables.Select(s => map.First(tile => tile.coordinates.x == s.posX && tile.coordinates.z == s.posZ));

        foreach(var placeable in placeableTiles)
        {
            placeable.placeable = true;
        }
    }

    public void moveUnitMode()
    {
        moveMode = true;

    }

    public void SpawnUnit(Vector3 location)
    {
        var unit = Instantiate(SelectedUnit, location, SelectedUnit.transform.rotation, playerContainter.transform);
        unit.GetComponent<Character>().turnManager = turnManager;
        unit.tag = "Player";
        unit.pathfinder = gameObject.GetComponent<Pathfinder>();
       // eventSystem.AddUnit(SelectedUnit);
    }
    public Character GetSelectedUnit()
    {
        return SelectedUnit;
    }
    public void SetSelectedUnit(Character unit)
    {
        SelectedUnit = unit;
    }
    public void ResetSelectedUnit()
    {
        SelectedUnit = null;
    }
    public int GetPlacementPoints()
    {
        return placementPoints;
    }
    public void ReducePlacementPoints(int reduction)
    {
        placementPoints -= reduction;

        if (placementPoints <= 0)
        {
            Canvas canvas = GameObject.Find("PrepCanvas").GetComponent<Canvas>();
            canvas.enabled = false;
            turnManager.CycleTurns();
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
        public static T ReadXMLAsBytes(byte[] xmlData)
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
}
namespace GridXML
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class levels
    {

        private levelsLevel levelField;

        /// <remarks/>
        public levelsLevel level
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

        private levelsLevelMap mapField;

        private levelsLevelEnemy[] enemiesField;

        private levelsLevelPlaceable[] placeablesField;

        /// <remarks/>
        public levelsLevelMap map
        {
            get
            {
                return this.mapField;
            }
            set
            {
                this.mapField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("enemy", IsNullable = false)]
        public levelsLevelEnemy[] enemies
        {
            get
            {
                return this.enemiesField;
            }
            set
            {
                this.enemiesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("placeable", IsNullable = false)]
        public levelsLevelPlaceable[] placeables
        {
            get
            {
                return this.placeablesField;
            }
            set
            {
                this.placeablesField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class levelsLevelMap
    {

        private levelsLevelMapMapline[] maplineField;

        private byte placementPointsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("map-line")]
        public levelsLevelMapMapline[] mapline
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
        public byte placementPoints
        {
            get
            {
                return this.placementPointsField;
            }
            set
            {
                this.placementPointsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class levelsLevelMapMapline
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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class levelsLevelEnemy
    {

        private string nameField;

        private byte typeField;

        private byte posXField;

        private byte posZField;

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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte posX
        {
            get
            {
                return this.posXField;
            }
            set
            {
                this.posXField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte posZ
        {
            get
            {
                return this.posZField;
            }
            set
            {
                this.posZField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class levelsLevelPlaceable
    {

        private byte posXField;

        private byte posZField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte posX
        {
            get
            {
                return this.posXField;
            }
            set
            {
                this.posXField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte posZ
        {
            get
            {
                return this.posZField;
            }
            set
            {
                this.posZField = value;
            }
        }
    }



}