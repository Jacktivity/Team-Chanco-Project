using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Character[] Units => transform.GetComponentsInChildren<Character>();
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

                unitToMove.MoveUnit(walkPath.Last());

                if(walked)
                {
                    //Attack here
                }

                break;
            default:
                break;
        }

        return true;
    }

    public void TESTMoveUnit()
    {
        var moveTo = pathfinder.Map.ElementAt(UnityEngine.Random.Range(0, pathfinder.Map.Length - 1));

        Debug.Log(moveTo.name);

        Units.First().MoveUnit(moveTo);
    }
}


public enum AIStates
{
    Regroup, Retreat, Attack
}
