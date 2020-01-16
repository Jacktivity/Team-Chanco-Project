using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

public class GridManager : MonoBehaviour
{

    [Header("Enemy Data")]
    [SerializeField] private GameObject enemyContainter;
    [SerializeField] private Character[] enemyPrefabs;

    [Header("Player Data")]
    [SerializeField] private GameObject playerContainter;
    [SerializeField] public Character[] playerPrefabs;

    [Header("MapData")]
    [SerializeField] private GameObject[] tiles;
    [SerializeField] private TextAsset levelMap;

    [SerializeField] private Character SelectedUnit;

    [SerializeField] private Color spawnPoint, lowSpeedTile, highSpeedTile;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private EnemyAI enemyAIContainer;


    private int unitIndex;

    GameObject[] playerUnits;

    public bool moveMode;
    public BlockScript selectedBlock;

    public BlockScript[] Map => GetComponentsInChildren<BlockScript>();

    public Color SpawnColor => spawnPoint;
    public static EventHandler<Character> unitSpawned;
    public static EventHandler<EnemySpawn> enemySpawned;
    public static EventHandler<BlockScript[]> mapGenerated;

    private int placementPoints;

    GridXML.levels xmlData;

    /*Turn Manager*/
    public bool playerTurn;
    private Coroutine enemyTurnCoroutine;
    static int turnNumber = 1;
    public static EventHandler turnEnded;

    // Start is called before the first frame update
    void Start()
    {
        xmlData = XmlReader<GridXML.levels>.ReadXMLAsBytes(levelMap.bytes);
        GenerateLevel();
        PlaceEnemy();
        UnitPlacement();

        BlockScript.blockClicked += (s, e) => BlockClicked(e);
        turnEnded += (s, e) => ClearMap();
    }

    public void ColourTiles(IEnumerable<BlockScript> tiles, bool walking)
    {
        if (walking)
            foreach (var tile in tiles)
                tile.ChangeColour(lowSpeedTile);
        else
            foreach (var tile in tiles)
                tile.ChangeColour(highSpeedTile);
    }

    public void ClearMap()
    {
        foreach (var tile in Map)
        {
            tile.ChangeColour(tile.Normal);
        }

        //if(playerTurn && playerManager.selectedPlayer.movedThisTurn == false)
        //{
        //    playerManager.PlayerUnitChosen(playerManager.selectedPlayer);
        //}
    }

    private void BlockClicked(BlockScript tile)
    {
        if(tile.placeable && SelectedUnit != null && tile.Occupied == false)
        {
            var costOfUnit = SelectedUnit.cost;
            if ((GetPlacementPoints() - costOfUnit) >= 0)
            {
                var unitPos = SelectedUnit.GetComponent<Collider>().bounds.center + SelectedUnit.GetComponent<Collider>().bounds.extents;
                var tilePos = tile.GetComponent<Collider>().bounds.center + tile.GetComponent<Collider>().bounds.extents;


                SpawnUnit(new Vector3(tile.transform.position.x, unitPos.y + tilePos.y, tile.transform.position.z), tile);
                ReducePlacementPoints(costOfUnit);

                playerUnits = GameObject.FindGameObjectsWithTag("Player");
                unitIndex = 0;
                var firstUnit = playerUnits[unitIndex].GetComponent<Character>();
                playerManager.PlayerUnitChosen(firstUnit);
            }
        }
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

        foreach(var map in level.maps)
        {
            placementPoints += map.placementpoints;
            var rotationlines = level.rotations.ElementAt(map.layer).rotationline.SelectMany((r, x) => r.value.Split(',').Select((v, z) => new { Value = int.Parse(v), ZPos = z, XPos = x })).ToArray();
            var anonMap = map.mapline.SelectMany((m, x) => m.value.Split(',').Select((v, z) => new { Value = int.Parse(v), ZPos = z, YPos = map.layer,XPos = x })).ToArray();
            var mythingy = rotationlines.Length;
            for (int i = 0; i < anonMap.Length; i++)
            {
                var pos = anonMap[i];
                var rot = rotationlines[i];
                if (pos.Value >= 0)
                {
                    GameObject tile = Instantiate(tiles[pos.Value], new Vector3(pos.XPos, pos.YPos, pos.ZPos), Quaternion.Euler(new Vector3(tiles[pos.Value].transform.rotation.x, (90 * rot.Value), tiles[pos.Value].transform.rotation.z)), gameObject.transform);
                    tile.GetComponent<BlockScript>().coordinates = new Vector3(pos.XPos, pos.YPos, pos.ZPos);
                    tile.name = tile.name.Replace("(Clone)", "");
                    tile.name = tile.name + '(' + pos.XPos + ','+ pos.YPos+ ',' + pos.ZPos + ')';
                }
                BlockScript.blockMousedOver += (s, e) => { if (moveMode) selectedBlock = e; };
            }
        }
    }

    public void nextUnit()
    {
        unitIndex++;
        if (unitIndex >= playerUnits.Count())
        {
            unitIndex = 0;
        }
        var comingUnit = playerManager.selectedPlayer;
        playerUnits[unitIndex].GetComponentInChildren<Renderer>().material.color = Color.white;
        comingUnit = playerUnits[unitIndex].GetComponent<Character>();
        playerManager.PlayerUnitChosen(comingUnit);
        //attackManager.AssignAttacker(comingUnit);
    }

    void PlaceEnemy()
    {
        var enemies = xmlData.level.enemies;

        foreach(var enemy in enemies)
        {
            var tile = Map.First(t => t.coordinates.x == enemy.posX && t.coordinates.y == enemy.posY && t.coordinates.z == enemy.posZ);
            var unitPos = enemyPrefabs[enemy.type].GetComponent<Collider>().bounds.center + enemyPrefabs[enemy.type].GetComponent<Collider>().bounds.extents;
            Character placedEnemy = Instantiate(enemyPrefabs[enemy.type], new Vector3(enemy.posX, unitPos.y + tile.transform.position.y, enemy.posZ), new Quaternion(), enemyContainter.transform);
            placedEnemy.name = enemy.name;
            placedEnemy.SetFloor(tile);
            placedEnemy.tag = "Enemy";
            tile.occupier = placedEnemy.gameObject;

            var linkedUnits = enemy.linkedUnits.Split(',').Select(v => int.Parse(v));

            enemySpawned?.Invoke(this, new EnemySpawn(placedEnemy, (AIStates)enemy.behaviour, enemy.id, linkedUnits));
            playerManager.AddUnit(placedEnemy);
          // eventSystem.AddUnit(placedEnemy);
        }
    }

    void UnitPlacement()
    {
        var placeables = xmlData.level.placeables;

        var map = gameObject.GetComponentsInChildren<BlockScript>();

        var placeableTiles = placeables.Select(s => map.First(tile => tile.coordinates.x == s.posX && tile.coordinates.y == s.posY && tile.coordinates.z == s.posZ));

        foreach(var spawnTile in placeableTiles)
        {
            spawnTile.placeable = true;
            spawnTile.ChangeColour(spawnPoint);
        }
    }

    public void moveUnitMode()
    {
        moveMode = true;

    }

    public void SpawnUnit(Vector3 location, BlockScript tile)
    {

        var unit = Instantiate(SelectedUnit, location, SelectedUnit.transform.rotation, playerContainter.transform);
        if (unit.isCaptain)
        {
            ResetSelectedUnit();
        }
        unit.tag = "Player";
        unit.pathfinder = gameObject.GetComponent<Pathfinder>();
        unitSpawned?.Invoke(this, unit);
        unit.SetFloor(tile);
        tile.occupier = unit.gameObject;
        playerManager.AddUnit(unit);


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
    public void FinishPlacement()
    {
        if(playerManager.activePlayerNecromancers.Count() > 0)
        {
            ReducePlacementPoints(placementPoints);
        }
    }

    public void ReducePlacementPoints(int reduction)
    {
        Debug.Log(placementPoints + "-" + reduction);
        placementPoints -= reduction;

        if (placementPoints <= 0 && (playerManager.activePlayerNecromancers.Count() > 0 || reduction == 0 ))
        {
            CycleTurns();

            UIManager.gameStateChange?.Invoke(this, UIManager.GameStates.playerTurn);

            var remainingSpawnTiles = Map.Where(t => t.placeable);
            foreach (var tile in remainingSpawnTiles)
            {
                tile.placeable = false;
                tile.ChangeColour(tile.Normal);
            }
        }
    }

    public bool CheckPlayerTurn()
    {
        playerTurn = false;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            var character = player.GetComponent<Character>();
            if (character.ActionPoints > 0 && character.moving == false)
            {
                playerTurn = true;
            }
        }
        return playerTurn;
    }

    public IEnumerator MoveEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Character>().APReset();
            enemyTurnCoroutine = StartCoroutine(MovingEnemies(enemy));
            yield return enemyTurnCoroutine;
        }
        turnEnded?.Invoke(this, new EventArgs());
        ResetPlayerTurn();
        UIManager.gameStateChange?.Invoke(this, UIManager.GameStates.playerTurn);
    }

    public void ResetPlayerTurn()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            var playerScript = player.GetComponent<Character>();
            playerScript.APReset();

            player.gameObject.GetComponentInChildren<Renderer>().material.color = Color.white;
        }
        playerTurn = true;
        nextUnit();
        turnNumber++;
        uiManager.UpdateTurnNumber(turnNumber);
    }

    public void CycleTurns()
    {
        if (!CheckPlayerTurn())
        {
            turnEnded?.Invoke(this, new EventArgs());
            StartCoroutine(MoveEnemies());
        }
    }
    IEnumerator MovingEnemies(GameObject enemy)
    {
        enemyAIContainer.MoveUnit(enemy.GetComponent<Character>());

        yield return new WaitForSeconds(1);
    }

    public int GetTurnNumber()
    {
        return turnNumber;
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

        private levelsLevelMap[] mapsField;

        private levelsLevelRotation[] rotationsField;

        private levelsLevelEnemy[] enemiesField;

        private levelsLevelPlaceable[] placeablesField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("map", IsNullable = false)]
        public levelsLevelMap[] maps
        {
            get
            {
                return this.mapsField;
            }
            set
            {
                this.mapsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("rotation", IsNullable = false)]
        public levelsLevelRotation[] rotations
        {
            get
            {
                return this.rotationsField;
            }
            set
            {
                this.rotationsField = value;
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

        private byte layerField;

        private byte placementpointsField;

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
        public byte layer
        {
            get
            {
                return this.layerField;
            }
            set
            {
                this.layerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte placementpoints
        {
            get
            {
                return this.placementpointsField;
            }
            set
            {
                this.placementpointsField = value;
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
    public partial class levelsLevelRotation
    {

        private levelsLevelRotationRotationline[] rotationlineField;

        private byte layerField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("rotation-line")]
        public levelsLevelRotationRotationline[] rotationline
        {
            get
            {
                return this.rotationlineField;
            }
            set
            {
                this.rotationlineField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte layer
        {
            get
            {
                return this.layerField;
            }
            set
            {
                this.layerField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class levelsLevelRotationRotationline
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

        private byte idField;

        private string nameField;

        private byte typeField;

        private byte posXField;

        private byte posYField;

        private byte posZField;

        private byte behaviourField;

        private string linkedUnitsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
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
        public byte posY
        {
            get
            {
                return this.posYField;
            }
            set
            {
                this.posYField = value;
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte behaviour
        {
            get
            {
                return this.behaviourField;
            }
            set
            {
                this.behaviourField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string linkedUnits
        {
            get
            {
                return this.linkedUnitsField;
            }
            set
            {
                this.linkedUnitsField = value;
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

        private byte posYField;

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
        public byte posY
        {
            get
            {
                return this.posYField;
            }
            set
            {
                this.posYField = value;
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
