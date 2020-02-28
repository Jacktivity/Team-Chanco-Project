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

    [SerializeField] private Color spawnPoint, lowSpeedTile, highSpeedTile, attackTile, missTile;
    [SerializeField] private Vector2 highlightCurve, highlightSoft;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private EnemyAI enemyAIContainer;


    private int unitIndex;

    GameObject[] playerUnits;

    public bool moveMode, activeAI;
    public BlockScript selectedBlock;

    public BlockScript[] Map => GetComponentsInChildren<BlockScript>();

    public Color SpawnColor => spawnPoint;
    public Color WalkColour => lowSpeedTile;
    public Color SprintColour => highSpeedTile;
    public Color AttackTile => attackTile;
    public Color MissTile => missTile;
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
        if (PersistantData.levelAssigned) {
            xmlData = XmlReader<GridXML.levels>.ReadXMLAsBytes(PersistantData.level.bytes);
        } else {
            xmlData = XmlReader<GridXML.levels>.ReadXMLAsBytes(levelMap.bytes);
        }

        GenerateLevel();
        PlaceEnemy();
        UnitPlacement();

        activeAI = true;

        BlockScript.blockClicked += (s, e) => BlockClicked(e);
        turnEnded += TurnEndedEvent;
        uiManager.PlacementPoint(placementPoints);
        UIManager.gameStateChange += AIRunCheck;

        mapGenerated?.Invoke(this, Map);
        SetObjective();
    }
    private void OnDestroy()
    {
        UIManager.gameStateChange -= AIRunCheck;
        turnEnded -= TurnEndedEvent;

        var unitSpawnDelegates = unitSpawned.GetInvocationList();
        foreach (var del in unitSpawnDelegates)
        {
            unitSpawned -= (del as EventHandler<Character>);
        }

        var enemySpawn = enemySpawned.GetInvocationList();
        foreach (var del in enemySpawn)
        {
            enemySpawned -= (del as EventHandler<EnemySpawn>);
        }

        var map = mapGenerated?.GetInvocationList() ?? new Delegate[0]; //TODO Look into why this throws an error on ocassion when unloading level
        foreach (var del in map)
        {
            mapGenerated -= (del as EventHandler<BlockScript[]>);
        }

        var atkEvtDel = Character.attackEvent.GetInvocationList();
        foreach (var del in atkEvtDel)
        {
            Character.attackEvent -= (del as EventHandler<AttackEventArgs>);
        }
    }

    public void TurnEndedEvent(object sender, EventArgs e)
    {
        ClearMap();
    }

    private void AIRunCheck(object sender, UIManager.GameStates e)
    {
        activeAI = e != UIManager.GameStates.loseState && e != UIManager.GameStates.winState;
    }

    public void ColourTiles(IEnumerable<BlockScript> tiles, Color colour)
    {
        foreach (var tile in tiles)
        {
            var brightEdges = new List<Directions>();

            if (tile.N? tiles.Contains(tile.N.GetComponent<BlockScript>()) == false : true)
                brightEdges.Add(Directions.North);
            if (tile.S? tiles.Contains(tile.S.GetComponent<BlockScript>()) == false : true)
                brightEdges.Add(Directions.South);
            if (tile.E? tiles.Contains(tile.E.GetComponent<BlockScript>()) == false : true)
                brightEdges.Add(Directions.East);
            if (tile.W? tiles.Contains(tile.W.GetComponent<BlockScript>()) == false : true)
                brightEdges.Add(Directions.West);


            tile.Highlight(true);
            tile.SetHighlightColour(colour, brightEdges, highlightSoft, highlightCurve);
        }
    }

    public void ClearMap()
    {
        foreach (var tile in Map)
        {
            tile.Highlight(false);
            tile.SetHighlightColour(Color.grey);
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
                var unitPos = SelectedUnit.heightOffset;
                var tilePos = tile.transform.position;


                SpawnUnit(new Vector3(tile.transform.position.x, unitPos + tilePos.y, tile.transform.position.z), tile);
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
        var level = xmlData;
        placementPoints = xmlData.maps.placementpoints;

        foreach(var map in level.maps.map)
        {
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
                    var blockscript = tile.GetComponent<BlockScript>();
                    blockscript.coordinates = new Vector3(pos.XPos, pos.YPos, pos.ZPos);

                    tile.name = tile.name.Replace("(Clone)", "");
                    tile.name = tile.name + '(' + pos.XPos + ','+ pos.YPos+ ',' + pos.ZPos + ')';


                    var sBlock = Map.FirstOrDefault(t => t.coordinates == blockscript.coordinates + new Vector3(0, 0, -1));
                    if (sBlock != null)
                    {
                        sBlock.N = tile;
                        blockscript.S = sBlock.gameObject;
                    }
                    var swBlock = Map.FirstOrDefault(t => t.coordinates == blockscript.coordinates + new Vector3(-1, 0, -1));
                    if (swBlock != null)
                    {
                        swBlock.NE = tile;
                        blockscript.SW = swBlock.gameObject;
                    }
                    var wBlock = Map.FirstOrDefault(t => t.coordinates == blockscript.coordinates + new Vector3(-1, 0, 0));
                    if (wBlock != null)
                    {
                        wBlock.E = tile;
                        blockscript.W = wBlock.gameObject;
                    }
                    var nwBlock = Map.FirstOrDefault(t => t.coordinates == blockscript.coordinates + new Vector3(-1, 0, 1));
                    if (nwBlock != null)
                    {
                        nwBlock.SE = tile;
                        blockscript.NW = nwBlock.gameObject;
                    }
                    var below = Map.FirstOrDefault(t => t.coordinates == blockscript.coordinates + new Vector3(0, -1, 0));
                    if(below != null)
                    {
                        below.occupier = tile;
                    }
                }
                BlockScript.blockMousedOver += (s, e) => { if (moveMode) selectedBlock = e; };
            }
        }

        var triggerzones = xmlData.triggerzones;

        var sortingMap = gameObject.GetComponentsInChildren<BlockScript>();
        var triggerzonesTiles = triggerzones.Select(s => sortingMap.First(tile => tile.coordinates.x == s.posX && tile.coordinates.y == s.posY && tile.coordinates.z == s.posZ));
        int triggerIndex = 0;
        foreach (var triggerTile in triggerzonesTiles)
        {
            triggerTile.trigger = true;
            triggerTile.triggerId = triggerzones[triggerIndex].id;
            triggerIndex++;
        }


        var exitzones = xmlData.exitzones;

        var exitTiles = exitzones.Select(s => sortingMap.First(tile => tile.coordinates.x == s.posX && tile.coordinates.y == s.posY && tile.coordinates.z == s.posZ));
        foreach (var exitTile in exitTiles)
        {
            exitTile.exit = true;
        }


        foreach (var ramp in Map.Where(t => t.tag == "Floor-Transition"))
        {
            ramp.N = ramp.NE = ramp.E = ramp.SE = ramp.S = ramp.SW = ramp.W = ramp.NW = null;

            var rotation = ((int)ramp.transform.rotation.eulerAngles.y / 90);

            Vector3 upperStep, lowerStep;

            switch (rotation % 2)
            {
                //North - South step
                case 0:
                    upperStep = new Vector3(0, 0, 1);
                    lowerStep = new Vector3(0, -1, -1);
                    ramp.N = Map.FirstOrDefault(t => t.coordinates == ramp.coordinates + upperStep)?.gameObject ?? null;

                    if (ramp.N == null)
                    {
                        upperStep = new Vector3(0, 0, -1);
                        lowerStep = new Vector3(0, -1, 1);
                        var south = Map.First(t => t.coordinates == ramp.coordinates + upperStep);
                        ramp.S = south.gameObject;
                        south.N = ramp.gameObject;

                        var north = Map.First(t => t.coordinates == ramp.coordinates + lowerStep);
                        ramp.N = north.gameObject;
                        north.S = ramp.gameObject;
                    }
                    else
                    {
                        ramp.N.GetComponent<BlockScript>().S = ramp.gameObject;

                        var south = Map.First(t => t.coordinates == ramp.coordinates + lowerStep);
                        ramp.S = south.gameObject;
                        south.N = ramp.gameObject;
                    }
                    break;
                //East - West Step
                case 1:
                    upperStep = new Vector3(-1, 0, 0);
                    lowerStep = new Vector3(1, -1, 0);
                    ramp.W = Map.FirstOrDefault(t => t.coordinates == ramp.coordinates + upperStep)?.gameObject ?? null;

                    if (ramp.W == null)
                    {
                        upperStep = new Vector3(1, 0, 0);
                        lowerStep = new Vector3(-1, -1, 0);

                        var east = Map.First(t => t.coordinates == ramp.coordinates + upperStep);

                        ramp.E = east.gameObject;
                        east.W = ramp.gameObject;

                        var west = Map.First(t => t.coordinates == ramp.coordinates + lowerStep);
                        ramp.W = west.gameObject;
                        west.E = ramp.gameObject;
                    }
                    else
                    {
                        ramp.E = Map.First(t => t.coordinates == ramp.coordinates + lowerStep).gameObject;
                    }
                    break;
                default:
                    break;
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
        playerUnits[unitIndex].GetComponent<Character>().godRay.SetActive(true);
        comingUnit = playerUnits[unitIndex].GetComponent<Character>();
        playerManager.PlayerUnitChosen(comingUnit);
        //attackManager.AssignAttacker(comingUnit);
    }

    void PlaceEnemy()
    {
        var enemies = xmlData.enemies;

        foreach(var enemy in enemies)
        {
            var tile = Map.First(t => t.coordinates.x == enemy.posX && t.coordinates.y == enemy.posY && t.coordinates.z == enemy.posZ);
            var unitPos = enemyPrefabs[enemy.type].GetComponent<Collider>().bounds.center + enemyPrefabs[enemy.type].GetComponent<Collider>().bounds.extents;

            if (!enemy.onTrigger)
            {
                Character placedEnemy = Instantiate(enemyPrefabs[enemy.type], new Vector3(enemy.posX, unitPos.y + tile.transform.position.y, enemy.posZ), new Quaternion(), enemyContainter.transform);
                placedEnemy.name = enemy.name;
                placedEnemy.SetFloor(tile);
                placedEnemy.tag = "Enemy";
                placedEnemy.isCaptain = enemy.captain;
                placedEnemy.delaySpawn = enemy.delay;
                placedEnemy.onTrigger = enemy.onTrigger;
                placedEnemy.triggerId = enemy.triggerId;
                placedEnemy.repeatSpawn = enemy.repeat;
                tile.occupier = placedEnemy.gameObject;

                var linkedUnits = new int[0];

                bool hasLinkedUnits = enemy.linkedUnits != "";

                if (hasLinkedUnits)
                {
                    linkedUnits = enemy.linkedUnits.Split(',').Select(v => int.Parse(v)).ToArray();
                }

                enemySpawned?.Invoke(this, new EnemySpawn(placedEnemy, (AIStates)enemy.behaviour, enemy.id, linkedUnits));
                playerManager.AddUnit(placedEnemy);
            }
          // eventSystem.AddUnit(placedEnemy);
        }
    }

    void UnitPlacement()
    {
        ClearMap();

        var placeables = xmlData.placeables;

        var map = gameObject.GetComponentsInChildren<BlockScript>();

        var placeableTiles = placeables.Select(s => map.First(tile => tile.coordinates.x == s.posX && tile.coordinates.y == s.posY && tile.coordinates.z == s.posZ));

        ColourTiles(placeableTiles, SpawnColor);

        foreach(var spawnTile in placeableTiles)
        {
            spawnTile.placeable = true;
        }

        ColourTiles(Map.Where(t => t.placeable), SpawnColor);
    }

    void DynamicallySpawnUnit(BlockScript tile)
    {
        var enemies = xmlData.enemies;

        foreach (var enemy in enemies)
        {
            var unitPos = enemyPrefabs[enemy.type].GetComponent<Collider>().bounds.center + enemyPrefabs[enemy.type].GetComponent<Collider>().bounds.extents;

            if (enemy.onTrigger)
            {
                Character placedEnemy = Instantiate(enemyPrefabs[enemy.type], new Vector3(enemy.posX, unitPos.y + tile.transform.position.y, enemy.posZ), new Quaternion(), enemyContainter.transform);
                placedEnemy.name = enemy.name;
                placedEnemy.SetFloor(tile);
                placedEnemy.tag = "Enemy";
                placedEnemy.isCaptain = enemy.captain;
                placedEnemy.delaySpawn = enemy.delay;
                placedEnemy.onTrigger = enemy.onTrigger;
                placedEnemy.triggerId = enemy.triggerId;
                placedEnemy.repeatSpawn = enemy.repeat;
                tile.occupier = placedEnemy.gameObject;

                var linkedUnits = new int[0];

                bool hasLinkedUnits = enemy.linkedUnits != "";

                if (hasLinkedUnits)
                {
                    linkedUnits = enemy.linkedUnits.Split(',').Select(v => int.Parse(v)).ToArray();
                }

                enemySpawned?.Invoke(this, new EnemySpawn(placedEnemy, (AIStates)enemy.behaviour, enemy.id, linkedUnits));
                playerManager.AddUnit(placedEnemy);
            }
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
        ReducePlacementPoints(placementPoints);
    }

    public void ReducePlacementPoints(int reduction)
    {
        //Debug.Log(placementPoints + "-" + reduction);
        placementPoints -= reduction;
        uiManager.PlacementPoint(placementPoints);

        if (placementPoints <= 0 && (playerManager.activePlayerCaptains.Count() > 0 || reduction == 0 ))
        {
            CycleTurns();

            UIManager.gameStateChange?.Invoke(this, UIManager.GameStates.playerTurn);

            var remainingSpawnTiles = Map.Where(t => t.placeable);
            foreach (var tile in remainingSpawnTiles)
            {
                tile.placeable = false;
                tile.SetHighlightColour(tile.Normal);
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
        foreach(Character player in playerManager.activePlayers)
        {
            if (player.floor.trigger)
            {
                DynamicallySpawnUnit(player.floor);
                player.floor.trigger = false;
            }
            player.APReset();
            player.godRay.SetActive(false);
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
            uiManager.GameOverCheck();
            UIManager.gameStateChange?.Invoke(this, UIManager.GameStates.enemyTurn);
            turnEnded?.Invoke(this, new EventArgs());
            StartCoroutine(MoveEnemies());
        }
    }
    IEnumerator MovingEnemies(GameObject enemy)
    {
        enemyAIContainer.MoveUnit(enemy.GetComponent<Character>());

        yield return new WaitForSeconds(1);
    }

    public void SetObjective()
    {
        uiManager.SetObjectiveText();
    }

    public int GetObjective()
    {
        return xmlData.maps.objective;
    }

    public int GetPar()
    {
        return xmlData.maps.par;
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

        private levelsMaps mapsField;

        private levelsRotation[] rotationsField;

        private levelsEnemy[] enemiesField;

        private levelsPlaceable[] placeablesField;

        private levelsExitzone[] exitzonesField;

        private levelsTriggerzone[] triggerzonesField;

        /// <remarks/>
        public levelsMaps maps
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
        public levelsRotation[] rotations
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
        public levelsEnemy[] enemies
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
        public levelsPlaceable[] placeables
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

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("exitzone", IsNullable = false)]
        public levelsExitzone[] exitzones
        {
            get
            {
                return this.exitzonesField;
            }
            set
            {
                this.exitzonesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("triggerzone", IsNullable = false)]
        public levelsTriggerzone[] triggerzones
        {
            get
            {
                return this.triggerzonesField;
            }
            set
            {
                this.triggerzonesField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class levelsMaps
    {

        private levelsMapsMap[] mapField;

        private byte placementpointsField;

        private byte objectiveField;

        private byte parField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("map")]
        public levelsMapsMap[] map
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte objective
        {
            get
            {
                return this.objectiveField;
            }
            set
            {
                this.objectiveField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte par
        {
            get
            {
                return this.parField;
            }
            set
            {
                this.parField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class levelsMapsMap
    {

        private levelsMapsMapMapline[] maplineField;

        private byte layerField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("mapline")]
        public levelsMapsMapMapline[] mapline
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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class levelsMapsMapMapline
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
    public partial class levelsRotation
    {

        private levelsRotationRotationline[] rotationlineField;

        private byte layerField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("rotationline")]
        public levelsRotationRotationline[] rotationline
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
    public partial class levelsRotationRotationline
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
    public partial class levelsEnemy
    {

        private byte idField;

        private string nameField;

        private byte typeField;

        private byte posXField;

        private byte posYField;

        private byte posZField;

        private byte behaviourField;

        private string linkedUnitsField;

        private bool captainField;

        private byte delayField;

        private bool repeatField;

        private bool onTriggerField;

        private byte triggerIdField;

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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool captain
        {
            get
            {
                return this.captainField;
            }
            set
            {
                this.captainField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte delay
        {
            get
            {
                return this.delayField;
            }
            set
            {
                this.delayField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool repeat
        {
            get
            {
                return this.repeatField;
            }
            set
            {
                this.repeatField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool onTrigger
        {
            get
            {
                return this.onTriggerField;
            }
            set
            {
                this.onTriggerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte triggerId
        {
            get
            {
                return this.triggerIdField;
            }
            set
            {
                this.triggerIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class levelsPlaceable
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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class levelsExitzone
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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class levelsTriggerzone
    {

        private byte idField;

        private byte posXField;

        private byte posYField;

        private byte posZField;

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
