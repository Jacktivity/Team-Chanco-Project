using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridXML;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;

public class SaveScript : MonoBehaviour
{
    levels levels = new levels();
    public Slider placementPoints;
    public Text levelName;
    public GameObject blockcontainer, enemycontainer;
    public Dropdown objective;
    public void SaveMap()
    {
        var placementPointsValue = placementPoints.value * 5;
        var blockdetailsContainer = blockcontainer.GetComponentsInChildren<BlockScript>();
        var enemycontainerdetails = enemycontainer.GetComponentsInChildren<Character>();
        var placeableDetails = blockcontainer.GetComponentsInChildren<BlockScript>().Where(b => b.placeable).ToArray();
        var exitDetails = blockcontainer.GetComponentsInChildren<BlockScript>().Where(b => b.exit).ToArray();
        var triggerDetails = blockcontainer.GetComponentsInChildren<BlockScript>().Where(b => b.trigger).ToArray();

        var maxY = (int)blockdetailsContainer.Max(b => b.coordinates.y) +1;
        var maxZ = (int)blockdetailsContainer.Max(b => b.coordinates.z) +1;
        var maxX = (int)blockdetailsContainer.Max(b => b.coordinates.x) +1;

        levels.maps = new levelsMaps();
        levels.maps.map = new levelsMapsMap[maxY];
        levels.rotations = new levelsRotation[maxY];
        levels.enemies = new levelsEnemy[enemycontainerdetails.Length];
        levels.placeables = new levelsPlaceable[placeableDetails.Length];
        levels.exitzones = new levelsExitzone[exitDetails.Length];
        levels.triggerzones = new levelsTriggerzone[triggerDetails.Length];
        levels.maps.placementpoints = (byte)placementPointsValue;
        levels.maps.objective = (byte)objective.value;

        int y = 0;
        int z = maxZ - 1;
        int p = 0;
        int e = 0;
        int t = 0;
        int x = maxX - 1;


        while (y < maxY)
        {
            
            levels.maps.map[y] = new levelsMapsMap();
            levels.maps.map[y].layer = (byte)y;
            levels.maps.map[y].mapline = new levelsMapsMapMapline[(int)maxX];
            levels.rotations[y] = new levelsRotation();
            levels.rotations[y].rotationline = new levelsRotationRotationline[(int)maxX];
            maxX = levels.maps.map[y].mapline.Length;

            while (x >= 0)
            {
                var pos = new int[maxZ];
                var rot = new int[maxZ];

                levels.maps.map[y].mapline[x] = new levelsMapsMapMapline();
                levels.rotations[y].rotationline[x] = new levelsRotationRotationline();

                var row = blockdetailsContainer
                .Where(b => b.coordinates.x == x && b.coordinates.y == y)
                .OrderBy(b => b.coordinates.y).OrderBy(b => b.coordinates.z).OrderBy(b => b.coordinates.x)
                .Select(b => b).ToArray();

                for (int i = 0; i < pos.Length; i++) pos[i] = -1;
                
                foreach(var block in row)
                {
                    pos[(int)block.coordinates.z] = block.blockType;

                    switch ((int)block.transform.eulerAngles.y)
                    {
                        case 0:
                            rot[(int)block.coordinates.z] = 0;
                            if(block.tag == "Floor-Transition")
                            {
                                rot[(int)block.coordinates.x] = 1;
                            }
                            break;
                        case 90:
                            rot[(int)block.coordinates.z] = 1;
                            break;
                        case -90:
                            rot[(int)block.coordinates.z] = 3;
                            break;
                        case -180:
                            rot[(int)block.coordinates.z] = 2;
                            break;
                        case 180:
                            rot[(int)block.coordinates.z] = 2;
                            break;
                        case 270:
                            rot[(int)block.coordinates.z] = 3;
                            break;
                        default:
                            break;
                    }

                    if (block.placeable)
                    {
                        var placeable = new levelsPlaceable();
                        placeable.posX = (byte)block.coordinates.x;
                        placeable.posY = (byte)block.coordinates.y;
                        placeable.posZ = (byte)block.coordinates.z;
                        levels.placeables[p] = placeable;
                        p++;

                    }
                    if (block.trigger)
                    {
                        var trigger = new levelsTriggerzone();
                        trigger.posX = (byte)block.coordinates.x;
                        trigger.posY = (byte)block.coordinates.y;
                        trigger.posZ = (byte)block.coordinates.z;
                        trigger.id = (byte)block.triggerId;
                        levels.triggerzones[t] = trigger;
                        t++;

                    }
                    if (block.exit)
                    {
                        var exit = new levelsExitzone();
                        exit.posX = (byte)block.coordinates.x;
                        exit.posY = (byte)block.coordinates.y;
                        exit.posZ = (byte)block.coordinates.z;
                        levels.exitzones[e] = exit;
                        e++;

                    }

                }

                levels.maps.map[y].mapline[x].value = string.Join(",", pos.Select(i => i.ToString()).ToArray());
                levels.rotations[y].rotationline[x].value = string.Join(",", rot.Select(i => i.ToString()).ToArray());

                x--;
            }
            x = maxX - 1;
            y++;
        }
        for (int i = 0; i < enemycontainerdetails.Length; i++)
        {
            var enemy = new levelsEnemy();
            var enemyDetails = enemycontainerdetails[i];

            enemy.id = (byte)i;
            enemy.name = enemyDetails.name;
            enemy.type = (byte)enemyDetails.type;
            enemy.posX = (byte)enemyDetails.floor.coordinates.x;
            enemy.posY = (byte)(enemyDetails.floor.coordinates.y);
            enemy.posZ = (byte)enemyDetails.floor.coordinates.z;
            enemy.behaviour = (byte)0;
            enemy.linkedUnits = "";
            enemy.delay = (byte)enemyDetails.delaySpawn;
            enemy.captain = enemyDetails.isCaptain;
            enemy.repeat = enemyDetails.repeatSpawn;
            enemy.triggerId = (byte)enemyDetails.triggerId;
            enemy.onTrigger = enemyDetails.onTrigger;

            levels.enemies[i] = enemy;
        }

        var serializer = new XmlSerializer(typeof(levels));
        var stream = new FileStream("./Assets/Resources/CustomLevels/" + levelName.text + ".xml", FileMode.Create);
        serializer.Serialize(stream, levels);
        stream.Close();
    }
}
