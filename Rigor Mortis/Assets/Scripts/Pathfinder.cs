using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pathfinder : MonoBehaviour
{
    public BlockScript[] Map => GetComponentsInChildren<BlockScript>().Where(t => t.Occupied == false).ToArray();
    
    public BlockScript[] CompleteMap => GetComponentsInChildren<BlockScript>();

    public BlockScript[] GetPath(BlockScript start, BlockScript end, bool ignoreMoveModifier, bool flying = false) => flying ? GetFlightPath(start, end, ignoreMoveModifier) : GetPath(start, (t) => t == end, ignoreMoveModifier, flying);

    private BlockScript[] GetFlightPath(BlockScript start, BlockScript end, bool ignoreMoveModifier)
    {
        //
        var path = new List<BlockScript> { start };

        var current = start;

        while(current.coordinates != end.coordinates)
        {
            var nextCords = current.coordinates;

            if(current.coordinates.x != end.coordinates.x)
            {
                var diff = current.coordinates.x - end.coordinates.x;
                if(diff < 0)
                {
                    nextCords += new Vector3(1, 0, 0);
                }
                else
                {
                    nextCords -= new Vector3(1, 0, 0);
                }
            }            
            else
            {
                var diff = current.coordinates.z - end.coordinates.z;
                if (diff < 0)
                {
                    nextCords += new Vector3(0, 0, 1);
                }
                else
                {
                    nextCords -= new Vector3(0, 0, 1);
                }
            }

            if (current.coordinates.y != end.coordinates.y)
            {
                var diff = current.coordinates.y - end.coordinates.y;
                if (diff < 0)
                {
                    nextCords += new Vector3(0, 1, 0);
                }
                else
                {
                    nextCords -= new Vector3(0, 1, 0);
                }
            }

            current = CompleteMap.First(t => t.coordinates.x == nextCords.x && t.coordinates.z == nextCords.z);

            if(current.Occupied)
            {
                //var transform = current.transform;
                current = new BlockScript();
                current.coordinates = nextCords;
                //current.transform.position = transform.position + new Vector3(0,100,0);
            }

            path.Add(current);
        }

        Debug.Log(string.Join(",", path.Select(t => t.coordinates.ToString())));

        return path.ToArray();

        //return new BlockScript[] { start, end };
    }

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

    //Change to A*
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

    public BlockScript[] GetAttackTiles(Character attacker, Attack attack, BlockScript altAttackBlock = null)
    {
        var attackBlock = attacker?.floor ?? altAttackBlock;
        var allTiles = GetTilesInRange(attackBlock, attack.Range, true, true, true);

        var attackTiles = new List<BlockScript>();

        var tagsToIgnore = new List<string>();

        if(attack.PassAllies)
        {
            tagsToIgnore.Add(attacker.gameObject.tag);
        }

        if(attack.Piercing)
        {
            tagsToIgnore.Add(attacker.gameObject.tag == "Enemy" ? "Player" : "Enemy");
        }

        foreach (var tile in allTiles)
        {
            if (AttackTileValid(attacker, attackBlock, tile, tagsToIgnore))
                attackTiles.Add(tile);

            //Debug.Log(tile.gameObject.name + ":" + string.Join(",", data.Select(s => s.collider.gameObject.name)));
        }

        return attackTiles.ToArray();
    }

    private static bool AttackTileValid(Character attacker, BlockScript attackFrom, BlockScript tile, List<string> tagsToIgnore = null)
    {
        Vector3 origin, destination;
        Collider startCollider, endCollider;

        var startTileCollider = attacker.GetComponent<Collider>();
        var endTileCollider = tile.GetComponent<Collider>();

        if (attackFrom.Occupied)
        {
            startCollider = attackFrom.occupier.GetComponent<Collider>();
            origin = startCollider.bounds.center;// + new Vector3(0, startCollider.bounds.extents.y, 0);
        }
        else
        {
            startCollider = attackFrom.GetComponent<Collider>();
            origin = startCollider.bounds.center + new Vector3(0, startTileCollider.bounds.extents.y, 0);
        }

        if (tile.Occupied)
        {
            endCollider = tile.occupier.GetComponent<Collider>();
            destination = endCollider.bounds.center;// + new Vector3(0, endCollider.bounds.extents.y, 0);
        }
        else
        {
            endCollider = endTileCollider;
            destination = endCollider.bounds.center + new Vector3(0, endCollider.bounds.extents.y * 0.75f, 0);
        }

        var ray = new Ray(origin, destination - origin);

        var data = Physics.RaycastAll(ray, (destination - origin).magnitude);

        Debug.DrawRay(origin, destination - origin, Color.red, 10f);

        var validColliders = new Collider[] { startCollider, endCollider, startTileCollider, endTileCollider };

        if (data.All(d => validColliders.Contains(d.collider) || tagsToIgnore.Contains(d.collider.gameObject.tag)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public BlockScript[] GetTilesInRange(BlockScript start, float range, bool ignoreMoveModifier, bool canSearchOccupied = true, bool flying = false)
    {
        return flying ? GetFlyingTilesInRange(start, range, canSearchOccupied) : GetWalkingTiles(start, range, ignoreMoveModifier, canSearchOccupied, flying);
    }

    //Needs to not be able to bypass impassable walls
    private BlockScript[] GetFlyingTilesInRange(BlockScript start, float range, bool canSearchOccupied)
    {
        var allowedBlocks = new HashSet<Vector2>();

        var blockRange = (int)range;

        for (int x = 0; x <= blockRange; x++)
        {
            for (int z = 0; z <= blockRange; z++)
            {
                if(x + z <= blockRange)
                {
                    allowedBlocks.Add(new Vector2(start.coordinates.x + x, start.coordinates.z + z));
                    allowedBlocks.Add(new Vector2(start.coordinates.x + x, start.coordinates.z - z));
                    allowedBlocks.Add(new Vector2(start.coordinates.x - x, start.coordinates.z + z));
                    allowedBlocks.Add(new Vector2(start.coordinates.x - x, start.coordinates.z - z));
                }
            }
        }

        var map = canSearchOccupied ? CompleteMap : Map;

        return map.Where(t => allowedBlocks.Contains(new Vector2(t.coordinates.x, t.coordinates.z))).ToArray();
    }

    private BlockScript[] GetWalkingTiles(BlockScript start, float range, bool ignoreMoveModifier, bool canSearchOccupied, bool flying)
    {
        var distDictionary = new Dictionary<BlockScript, float>();

        var gameMap = flying ? CompleteMap.ToList() : Map.ToList();

        if (gameMap.Contains(start) == false)
            gameMap.Add(start);

        //var traversableTerrain = new HashSet<BlockScript>() { start };

        foreach (var node in CompleteMap)
        {
            distDictionary.Add(node, float.PositiveInfinity);
        }

        distDictionary[start] = 0;

        while (gameMap.Count > 0)
        {
            var pathTile = gameMap.OrderBy(t => distDictionary[t]).First();

            //If the shortest distance path unexplored is greater than the range
            //then all searches paths are within range, and all unsearched paths will be further away
            if (distDictionary[pathTile] > range)
            {
                break;
            }

            gameMap.Remove(pathTile);

            var adjacent = canSearchOccupied ? pathTile.AdjacentTiles() : pathTile.UnoccupiedAdjacentTiles();

            foreach (var tile in adjacent)
            {
                float pathLength = Vector2.Distance(new Vector2(tile.coordinates.x, tile.coordinates.z),
                    new Vector2(pathTile.coordinates.x, pathTile.coordinates.z));

                if (ignoreMoveModifier == false)
                    pathLength *= tile.MoveModifier;

                pathLength += distDictionary[pathTile];

                //If distanceDictionary[pathTile] is infinite, then tile has not been explored, or a shorter path has been found
                if (pathLength < distDictionary[tile])
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

    public BlockScript[] EnemyAttackCheck(Character unit, BlockScript attackSource, Attack selectedAttack)
    {
        var blocksInRange = new List<BlockScript>();

        var playerUnitPositions = CompleteMap.Where(t => t.Occupied ? t.occupier.tag == "Player" : false);

        foreach (var tile in playerUnitPositions)
        {

        }

        return blocksInRange.ToArray();
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
