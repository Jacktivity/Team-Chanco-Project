﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pathfinder : MonoBehaviour
{
    public BlockScript[] Map => GetComponentsInChildren<BlockScript>().Where(t => t.Occupied == false).ToArray();
    
    public BlockScript[] CompleteMap => GetComponentsInChildren<BlockScript>();

    public BlockScript[] GetPath(BlockScript start, Func<BlockScript,bool> searchCriteria)
    {
        var pathDictionary = new Dictionary<BlockScript, BlockScript>();
        var distDictionary = new Dictionary<BlockScript, float>();

        BlockScript targetTile = null;

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
            return new BlockScript[] { };
        }
        else
        {
            return PathFromDictionary(pathDictionary, ref targetTile);
        }
    }

    public BlockScript[] GetTilesInRange(BlockScript start, float range)
    {
        var distDictionary = new Dictionary<BlockScript, float>();

        var gameMap = Map.ToList();
        var traversableTerrain = new HashSet<BlockScript>() { start };

        foreach (var node in gameMap)
        {
            distDictionary.Add(node, float.PositiveInfinity);
        }

        distDictionary[start] = 0;

        while(gameMap.Count > 0)
        {
            var pathTile = gameMap.OrderBy(t => distDictionary[t]).First();

            ////If all traversible tiles do not contain unchecked nodes
            //if(traversableTerrain.All(t => t.AdjacentTiles().All(a => distDictionary[a] != float.PositiveInfinity)))
            //{
            //    break;
            //}

            gameMap.Remove(pathTile);

            foreach (var tile in pathTile.AdjacentTiles().Where(t => t.Occupied == false))
            {
                float pathLength = Vector2.Distance(new Vector2(tile.coordinates.x, tile.coordinates.z),
                    new Vector2(pathTile.coordinates.x, pathTile.coordinates.z))
                    * tile.MoveModifier;
                pathLength += distDictionary[pathTile];

                //If distanceDictionary[pathTile] is infinite, then tile has not been explored, or a shorter path has been found
                if(pathLength < distDictionary[tile])
                {
                    distDictionary[tile] = pathLength;
                    traversableTerrain.Add(tile);
                    //if (pathLength < range)
                    //{
                        
                    //}
                }                
            }
        }

        var check = distDictionary;

        return traversableTerrain.Where(t => distDictionary[t] <= range).ToArray();
    }

    public void DELETEPathTileTest(BlockScript block)
    {
        var x = block;
    }

    private static void SearchAdjacentTiles(Dictionary<BlockScript, BlockScript> pathDictionary, Dictionary<BlockScript, float> distDictionary, BlockScript pathTile)
    {
        foreach (var tile in pathTile.AdjacentTiles().Where(t => t.Occupied == false))
        {
            float pathLength = Vector2.Distance(new Vector2(tile.coordinates.x, tile.coordinates.z),
                new Vector2(pathTile.coordinates.x, pathTile.coordinates.z))
                * tile.MoveModifier;
            pathLength += distDictionary[pathTile];
            if (pathLength < distDictionary[tile])
            {
                distDictionary[tile] = pathLength;
                pathDictionary[tile] = pathTile;
            }
        }
    }
       

    private static BlockScript[] PathFromDictionary(Dictionary<BlockScript, BlockScript> pathDictionary, ref BlockScript targetTile)
    {
        var foundPath = new List<BlockScript>();

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