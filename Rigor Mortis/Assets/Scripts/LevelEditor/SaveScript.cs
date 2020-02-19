using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridXML;
using System.Linq;
using System.Xml.Serialization;
using System.IO;

public class SaveScript : MonoBehaviour
{
    levels levels = new levels();
    levelsLevel level = new levelsLevel();
    levelsLevelMap[] map = new levelsLevelMap[100];
    levelsLevelMapMapline[] maplines = new levelsLevelMapMapline[100];

    levelsLevelRotation[] rotations = new levelsLevelRotation[100];
    levelsLevelRotationRotationline[] rotationlines = new levelsLevelRotationRotationline[100];




    public GameObject blockcontainer;
    public void SaveMap()
    {
        levels.level = level;
        levels.level.maps = map;
        levels.level.maps[0] = new levelsLevelMap();
        levels.level.maps[0].mapline = maplines;

        levels.level.rotations = rotations;
        levels.level.rotations[0] = new levelsLevelRotation();
        levels.level.rotations[0].rotationline = rotationlines;


        var bContainerOrdered = blockcontainer.GetComponentsInChildren<BlockScript>().OrderBy(b => b.coordinates.x).OrderBy(b => b.coordinates.z).OrderBy(b => b.coordinates.y);
        int x = 0;
        int y = 0;
        int z = 0;
        var tempblocks = new int[100];
        var tempblocksrotations = new int[100];
        for(int i =0; i < tempblocks.Length; i ++) tempblocks[i] = -1;
        foreach (BlockScript block in bContainerOrdered)
        {
            tempblocks[(int)block.coordinates.x] = block.blockType;
            switch((int)block.transform.rotation.y)
            {
                case 90:
                    tempblocksrotations[(int)block.coordinates.x] = 1;
                    break;
                case 180:
                    tempblocksrotations[(int)block.coordinates.x] = 2;
                    break;
                case -90:
                    tempblocksrotations[(int)block.coordinates.x] = 3;
                    break;
                default:
                    break;
            }
           
            if(block.coordinates.z > z )
            {
                levels.level.maps[y].mapline[z] = new levelsLevelMapMapline();
                levels.level.rotations[y].rotationline[z] = new levelsLevelRotationRotationline();

                levels.level.maps[y].mapline[z].value = string.Join(", ", tempblocks.Select(i => i.ToString()).ToArray());
                levels.level.rotations[y].rotationline[z].value = string.Join(", ", tempblocksrotations.Select(i => i.ToString()).ToArray());
                tempblocks = new int[100];
                tempblocksrotations = new int[100];
                for (int i = 0; i < tempblocks.Length; i++) tempblocks[i] = -1;
                for (int i = 0; i < tempblocks.Length; i++) tempblocks[i] = -1;
                z++;
            }
            if (block.coordinates.y > y)
            {
                y++;

                levels.level.maps[y] = new levelsLevelMap();
                levels.level.maps[y].mapline = maplines;

                levels.level.rotations[y] = new levelsLevelRotation();
                levels.level.rotations[y].rotationline = rotationlines;
            }
            x++;
        }

        var serializer = new XmlSerializer(typeof(levels));
        var stream = new FileStream("./Assets/Levels/test.xml", FileMode.Create);
        serializer.Serialize(stream, levels);
        stream.Close();
    }
}
