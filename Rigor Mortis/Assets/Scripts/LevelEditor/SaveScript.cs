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
    levelsLevel level = new levelsLevel();

    public GameObject blockcontainer;
    public void SaveMap()
    {
        var blockdetailsContainer = blockcontainer.GetComponentsInChildren<BlockScript>();

        var maxY = blockdetailsContainer.Max(b => b.coordinates.y);
        var maxZ = blockdetailsContainer.Max(b => b.coordinates.z);

        levels.level = level;
        levels.level.maps = new levelsLevelMap[(int)maxY+1];
        levels.level.rotations = new levelsLevelRotation[(int)maxY];

        int y = 0;
        int z = 0;
        //needs dealing on yAxis
        //needs to deal with empty spaces
        while(y <= maxY)
        {
            levels.level.maps[y] = new levelsLevelMap();
            levels.level.maps[y].mapline = new levelsLevelMapMapline[(int)maxZ + 1];
            while(z <= maxZ)
            {
                levels.level.maps[y].mapline[z] = new levelsLevelMapMapline();

                var row = blockdetailsContainer
                .Where(b => b.coordinates.z == z && b.coordinates.y == y)
                .OrderBy(b => b.coordinates.x)
                .Select(b => b.blockType).ToArray();

                var rowLength = row.Length;

                levels.level.maps[y].mapline[z].value = string.Join(",", row.Select(i => i.ToString()).ToArray());

                z++;
            }
            y++;
        }




        //foreach (BlockScript block in bContainerOrdered)
        //{

        //   if(block.coordinates.z > z )
        //    {
        //        levels.level.maps[y].mapline[z].value = string.Join(", ", tempblocks.Select(i => i.ToString()).ToArray());
        //        levels.level.rotations[y].rotationline[z].value = string.Join(", ", tempblocksrotations.Select(i => i.ToString()).ToArray());

        //        z++;
        //        levels.level.maps[y].mapline[z] = new levelsLevelMapMapline();
        //        levels.level.rotations[y].rotationline[z] = new levelsLevelRotationRotationline();

        //        tempblocks = new int[x];
        //        tempblocksrotations = new int[x];
        //        for (int i = 0; i < tempblocks.Length; i++) tempblocks[i] = -1;
        //    }
        //    if (block.coordinates.y > y)
        //    {
        //        levels.level.maps[y].mapline[z].value = string.Join(", ", tempblocks.Select(i => i.ToString()).ToArray());
        //        levels.level.rotations[y].rotationline[z].value = string.Join(", ", tempblocksrotations.Select(i => i.ToString()).ToArray());

        //        y++;

        //        levels.level.maps[y] = new levelsLevelMap();
        //        levels.level.maps[y].mapline = maplines;

        //        levels.level.rotations[y] = new levelsLevelRotation();
        //        levels.level.rotations[y].rotationline = rotationlines;

        //        tempblocks = new int[x];
        //        tempblocksrotations = new int[x];
        //        for (int i = 0; i < tempblocks.Length; i++) tempblocks[i] = -1;
        //    }

        //    tempblocks[(int)block.coordinates.x] = block.blockType;
        //    switch ((int)block.transform.eulerAngles.y)
        //    {
        //        case 90:
        //            tempblocksrotations[(int)block.coordinates.x] = 1;
        //            break;
        //        case 180:
        //            tempblocksrotations[(int)block.coordinates.x] = 2;
        //            break;
        //        case -90:
        //            tempblocksrotations[(int)block.coordinates.x] = 3;
        //            break;
        //        default:
        //            break;
        //    }
        //}

        //levels.level.maps[y].mapline[z].value = string.Join(", ", tempblocks.Select(i => i.ToString()).ToArray());
        //levels.level.rotations[y].rotationline[z].value = string.Join(", ", tempblocksrotations.Select(i => i.ToString()).ToArray());


        var serializer = new XmlSerializer(typeof(levels));
        var stream = new FileStream("./Assets/Levels/test.xml", FileMode.Create);
        serializer.Serialize(stream, levels);
        stream.Close();
    }
}
