using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pathfinder : MonoBehaviour
{
    public iMapTile[] Map => GetComponentsInChildren<iMapTile>().Where(t => t.Occupied == false).ToArray();
    
    public iMapTile[] CompleteMap => GetComponentsInChildren<iMapTile>();

    public iMapTile[] GetPath(iMapTile start, Func<iMapTile,bool> searchCriteria)
    {
        var pathDictionary = new Dictionary<iMapTile, iMapTile>();
        var distDictionary = new Dictionary<iMapTile, float>();

        iMapTile targetTile = null;

        var gameMap = Map.ToList();

        foreach (var node in gameMap)
        {
            pathDictionary.Add(node, null);
            distDictionary.Add(node, float.MaxValue);
        }

        distDictionary[start] = 0;

        while(gameMap.Count > 0)
        {
            var pathTile = gameMap.OrderBy(t => distDictionary[t]).First();

            bool searchComplete = pathTile.AdjacentTiles().Any(t => searchCriteria(t));

            if (searchComplete)
            {
                targetTile = pathTile;
                break;
            }

            gameMap.Remove(pathTile);

            SearchAdjacentTiles(pathDictionary, distDictionary, pathTile);
        }

        if (targetTile == null)
        {
            //No path found to target, return empty path
            return new iMapTile[] { };
        }
        else
        {
            return PathFromDictionary(pathDictionary, ref targetTile);
        }
    }

    public iMapTile[] GetTilesInRange(iMapTile start, float range)
    {
        var distDictionary = new Dictionary<iMapTile, float>();

        var gameMap = Map.ToList();
        var traversableTerrain = new HashSet<iMapTile>();

        foreach (var node in gameMap)
        {
            distDictionary.Add(node, float.PositiveInfinity);
        }

        distDictionary[start] = 0;

        while(gameMap.Count > 0)
        {
            var pathTile = gameMap.OrderBy(t => distDictionary[t]).First();

            //If all traversible tiles do not contain unchecked nodes
            if(traversableTerrain.All(t => t.AdjacentTiles().All(a => distDictionary[a] != float.PositiveInfinity)))
            {
                break;
            }

            gameMap.Remove(pathTile);

            foreach (var tile in pathTile.AdjacentTiles().Where(t => t.Occupied == false))
            {
                float pathLength = Vector2.Distance(new Vector2(tile.XCord, tile.YCord), new Vector2(pathTile.XCord, pathTile.YCord)) * tile.MoveModifier;
                pathLength += distDictionary[pathTile];

                //If distanceDictionary[pathTile] is infinite, then tile has not been explored, or a shorter path has been found
                if(pathLength < distDictionary[pathTile])
                {
                    distDictionary[tile] = pathLength;
                    if (pathLength < range)
                    {
                        traversableTerrain.Add(tile);
                    }
                }                
            }
        }

        return traversableTerrain.ToArray();
    }

    private static void SearchAdjacentTiles(Dictionary<iMapTile, iMapTile> pathDictionary, Dictionary<iMapTile, float> distDictionary, iMapTile pathTile)
    {
        foreach (var tile in pathTile.AdjacentTiles().Where(t => t.Occupied == false))
        {
            float pathLength = Vector2.Distance(new Vector2(tile.XCord, tile.YCord), new Vector2(pathTile.XCord, pathTile.YCord)) * tile.MoveModifier;
            pathLength += distDictionary[pathTile];
            if (pathLength < distDictionary[tile])
            {
                distDictionary[tile] = pathLength;
                pathDictionary[tile] = pathTile;
            }
        }
    }
       

    private static iMapTile[] PathFromDictionary(Dictionary<iMapTile, iMapTile> pathDictionary, ref iMapTile targetTile)
    {
        var foundPath = new List<iMapTile>();

        while (targetTile != null)
        {
            var step = pathDictionary[targetTile];
            foundPath.Add(step);
            targetTile = step;
        }

        foundPath.Reverse();
        return foundPath.ToArray();
    }
}
