using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pathfinder : MonoBehaviour
{
    public BlockScript[] Map => GetComponentsInChildren<BlockScript>().Where(t => t.Occupied == false).ToArray();
    
    public BlockScript[] CompleteMap => GetComponentsInChildren<BlockScript>();

    public BlockScript[] GetPath(BlockScript start, Func<BlockScript,bool> searchCriteria, bool ignoreMoveModifier, bool flying = false)
    {
        var pathDictionary = new Dictionary<BlockScript, BlockScript>();
        var distDictionary = new Dictionary<BlockScript, float>();

        BlockScript targetTile = null;

        var gameMap = flying? CompleteMap.ToList() : Map.ToList();
        if (gameMap.Contains(start) == false)
            gameMap.Add(start);

        foreach (var node in gameMap)
        {
            pathDictionary.Add(node, null);
            distDictionary.Add(node, float.MaxValue);
        }

        distDictionary[start] = 0;

        while(gameMap.Count > 0)
        {
            var pathTile = gameMap.OrderBy(t => distDictionary[t]).First(t => distDictionary[t] != float.MaxValue);

            //Path is blocked, all remaining vlaues are of float.MaxValue (unchecked / cannot be reached)
            if(pathTile == null)
            {
                break;
            }

            bool searchComplete = searchCriteria(pathTile);

            if (searchComplete && pathTile.Occupied == false)
            {
                targetTile = pathTile;
                break;
            }

            gameMap.Remove(pathTile);

            EuclidianAdjacencySearch(pathDictionary, distDictionary, pathTile, ignoreMoveModifier);

            var distances = distDictionary.Values;
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

    public BlockScript[] AttackRangeCheck(BlockScript startBlock, int attackRange)
    {
        var centrePoint = startBlock.coordinates;

        var rangeBlocks = new HashSet<BlockScript>();

        //Gets all perimiter values
        for (int x = 0; x < attackRange; x++)
        {
            for (int y = attackRange; y > 0; y--)
            {
                var north = centrePoint + new Vector3(0, 0, 0);
            }
        }

        throw new Exception();
    }


    public int GetDistance(BlockScript startBlock, BlockScript endBlock)
    {
        var pathDictionary = new Dictionary<BlockScript, BlockScript>();
        var distDictionary = new Dictionary<BlockScript, int>();

        BlockScript targetTile = null;

        var gameMap = Map.ToList();

        foreach (var node in gameMap)
        {
            pathDictionary.Add(node, null);
            distDictionary.Add(node, int.MaxValue);
        }

        distDictionary[startBlock] = 0;

        while (gameMap.Count > 0)
        {
            var pathTile = gameMap.OrderBy(t => distDictionary[t]).First();

            bool searchComplete = pathTile == endBlock;

            if (searchComplete)
            {
                targetTile = pathTile;
                break;
            }

            gameMap.Remove(pathTile);

            IntegerAdjacencySearch(pathDictionary, distDictionary, pathTile);
        }

        if (targetTile == null)
        {
            //No path found to target, return empty path
            return int.MaxValue;
        }
        else
        {
            return distDictionary[endBlock];
        }
    }

    public BlockScript[] GetTilesInRange(BlockScript start, float range, bool ignoreMoveModifier, bool canSearchOccupied = true, bool flying = false)
    {
        var distDictionary = new Dictionary<BlockScript, float>();

        var gameMap = flying? CompleteMap.ToList() : Map.ToList();

        if (gameMap.Contains(start) == false)
            gameMap.Add(start);

        //var traversableTerrain = new HashSet<BlockScript>() { start };

        foreach (var node in CompleteMap)
        {
            distDictionary.Add(node, float.PositiveInfinity);
        }

        distDictionary[start] = 0;

        while(gameMap.Count > 0)
        {
            var pathTile = gameMap.OrderBy(t => distDictionary[t]).First();

            //If the shortest distance path unexplored is greater than the range
            //then all searches paths are within range, and all unsearched paths will be further away
            if(distDictionary[pathTile] > range)
            {
                break;
            }

            gameMap.Remove(pathTile);

            var adjacent = canSearchOccupied ? pathTile.AdjacentTiles() : pathTile.UnoccupiedAdjacentTiles();

            foreach (var tile in adjacent)
            {
                float pathLength = Vector2.Distance(new Vector2(tile.coordinates.x, tile.coordinates.z),
                    new Vector2(pathTile.coordinates.x, pathTile.coordinates.z));

                if(ignoreMoveModifier == false)
                    pathLength *= tile.MoveModifier;

                pathLength += distDictionary[pathTile];

                //If distanceDictionary[pathTile] is infinite, then tile has not been explored, or a shorter path has been found
                if(pathLength < distDictionary[tile])
                {
                    distDictionary[tile] = pathLength;
                    //traversableTerrain.Add(tile);
                }                
            }
        }

        return CompleteMap.Where(t => distDictionary[t] <= range).ToArray();
    }

    private static void EuclidianAdjacencySearch(Dictionary<BlockScript, BlockScript> pathDictionary, Dictionary<BlockScript, float> distDictionary, BlockScript pathTile, bool ignoreMoveModifier)
    {
        foreach (var tile in pathTile.AdjacentTiles().Where(t => t.Occupied == false))
        {
            float pathLength = Vector2.Distance(new Vector2(tile.coordinates.x, tile.coordinates.z),
                new Vector2(pathTile.coordinates.x, pathTile.coordinates.z));

            if (ignoreMoveModifier == false)
                pathLength *= tile.MoveModifier;

            pathLength += distDictionary[pathTile];
            if (pathLength < distDictionary[tile])
            {
                distDictionary[tile] = pathLength;
                pathDictionary[tile] = pathTile;
            }
        }
    }

    private static void IntegerAdjacencySearch(Dictionary<BlockScript, BlockScript> pathDictionary, Dictionary<BlockScript, int> distDictionary, BlockScript pathTile)
    {
        foreach (var tile in pathTile.UnoccupiedAdjacentTiles().Where(t => t.Occupied == false))
        {
            int pathLength = distDictionary[pathTile] + 1;
            if (pathLength < distDictionary[tile])
            {
                distDictionary[tile] = pathLength;
                pathDictionary[tile] = pathTile;
            }
        }
    }


    private static BlockScript[] PathFromDictionary(Dictionary<BlockScript, BlockScript> pathDictionary, ref BlockScript targetTile)
    {
        var foundPath = new List<BlockScript>() { targetTile };

        while (pathDictionary[targetTile] != null)
        {
            var step = pathDictionary[targetTile];
            foundPath.Add(step);
            targetTile = step;
        }

        foundPath.Reverse();
        return foundPath.ToArray();
    }
}
