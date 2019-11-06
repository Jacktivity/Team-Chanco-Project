using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Character[] Units => GetComponentsInChildren<Character>();
    private AIStates currentPlayState = AIStates.Attack;
    [SerializeField] private Pathfinder pathfinder;

    public bool MoveUnit()
    {
        var unitToMove = Units.First(u => u.hasTurn);

        if (unitToMove == null)
            return false;

        switch (currentPlayState)
        {
            case AIStates.Regroup:
                break;
            case AIStates.Retreat:
                break;
            case AIStates.Attack:
                var path = pathfinder.GetPath(unitToMove.floor, (s) => s.AdjacentTiles().Any(t => t.Occupied? t.occupier.CompareTag("Player"):false), false);

                var walkPath = path.Take(unitToMove.movementSpeed);

                bool walked = true;

                if(walkPath.Last() != path.Last())
                {
                    walked = false;
                    walkPath = path.Take(unitToMove.movemenSprint + unitToMove.movementSpeed);
                }

                unitToMove.MoveUnit(walkPath);

                if(walked)
                {
                    //Attack here
                }

                break;
            default:
                break;
        }

        unitToMove.hasTurn = false;
        return true;
    }

    public void TESTMoveUnit()
    {
        var enemy = Units.First();
        Debug.Log(enemy.name);
        var path = pathfinder.GetPath(enemy.floor, Check, false);
        Debug.Log(path.Count());
    }

    public bool Check(BlockScript block)
    {
        var returnVal = block.AdjacentTiles().Where(s => s.Occupied).Any(t => t.occupier.tag == "Player");
        return returnVal;
    }
}


public enum AIStates
{
    Regroup, Retreat, Attack
}
