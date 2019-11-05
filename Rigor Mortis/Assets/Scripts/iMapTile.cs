using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iMapTile
{
    //Placeholder interface for the map tiles
    bool Occupied { get; set; }
    float MoveModifier { get; set; }
    int XCord { get; set; }
    int YCord { get; set; }
    int ZCord { get; set; }
    iMapTile[] AdjacentTiles();    
}
