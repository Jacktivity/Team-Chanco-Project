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

        var maxY = (int)blockdetailsContainer.Max(b => b.coordinates.y) +1;
        var maxZ = (int)blockdetailsContainer.Max(b => b.coordinates.z) +1;
        var maxX = (int)blockdetailsContainer.Max(b => b.coordinates.x) +1;

        levels.maps = new levelsMaps();
        levels.maps.map = new levelsMapsMap[maxY];
        levels.rotations = new levelsRotation[maxY];
        levels.enemies = new levelsEnemy[enemycontainerdetails.Length];
        levels.placeables = new levelsPlaceable[placeableDetails.Length];
        levels.maps.placementpoints = (byte)placementPointsValue;
        levels.maps.objective = objective.value.ToString();

        int y = 0;
        int z = 0;
        int p = 0;

        while(y < maxY)
        {
            levels.maps.map[y] = new levelsMapsMap();
            levels.maps.map[y].layer = (byte)y;
            levels.maps.map[y].mapline = new levelsMapsMapMapline[(int)maxZ];
            levels.rotations[y] = new levelsRotation();
            levels.rotations[y].rotationline = new levelsRotationRotationline[(int)maxZ];
            while (z < maxZ)
            {
                var pos = new int[maxX];
                var rot = new int[maxX];

                levels.maps.map[y].mapline[z] = new levelsMapsMapMapline();
                levels.rotations[y].rotationline[z] = new levelsRotationRotationline();

                var row = blockdetailsContainer
                .Where(b => b.coordinates.z == z && b.coordinates.y == y)
                .OrderBy(b => b.coordinates.x)
                .Select(b => b).ToArray();

                for (int i = 0; i < pos.Length; i++) pos[i] = -1;
                
                foreach(var block in row)
                {
                    pos[(int)block.coordinates.x] = block.blockType;

                    switch ((int)block.transform.eulerAngles.y)
                    {
                        case 90:
                            rot[(int)block.coordinates.x] = 1;
                            break;
                        case 180:
                            rot[(int)block.coordinates.x] = 2;
                            break;
                        case 270:
                            rot[(int)block.coordinates.x] = 3;
                            break;
                        case -180:
                            rot[(int)block.coordinates.x] = 2;
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

                }

                levels.maps.map[y].mapline[z].value = string.Join(",", pos.Select(i => i.ToString()).ToArray());
                levels.rotations[y].rotationline[z].value = string.Join(",", rot.Select(i => i.ToString()).ToArray());

                z++;
            }
            z = 0;
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
            levels.enemies[i] = enemy;
        }

        var serializer = new XmlSerializer(typeof(levels));
        var stream = new FileStream("./Assets/Levels/" + levelName.text + ".xml", FileMode.Create);
        serializer.Serialize(stream, levels);
        stream.Close();
    }
}
