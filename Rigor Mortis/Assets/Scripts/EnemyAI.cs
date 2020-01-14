using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Character[] Units => GetComponentsInChildren<Character>();

    private Dictionary<Character, AIStates> enemyMood;

    private AIStates currentPlayState = AIStates.Attack;
    [SerializeField] private Pathfinder pathfinder;

    public void Start()
    {
        enemyMood = new Dictionary<Character, AIStates>();
        GridManager.enemySpawned += (s, e) => enemyMood.Add(e, AIStates.Attack);
    }

    public bool MoveUnit()
    {
        var unitToMove = Units.First(u => u.MaxAP);

        if (unitToMove == null)
            return false;

        switch (currentPlayState)
        {
            case AIStates.Regroup:
                break;
            case AIStates.Retreat:
                break;
            case AIStates.Attack:

                var longestAttack = unitToMove.attacks.OrderByDescending(a => a.Range).First();
                
                var path = pathfinder.GetPath(unitToMove.floor, (s) => pathfinder.GetTilesInRange(s, longestAttack.Range,true).Any(t => t.Occupied? t.occupier.CompareTag("Player"):false), unitToMove.isFlying == false);

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
                    unitToMove.moveComplete += AIAttack;
                }


                break;
            default:
                break;
        }
        return true;
    }

    private void AIAttack(object sender, Character unit)
    {
        var longestAttack = unit.attacks.OrderByDescending(s => s.Range).First();

        Debug.Log(longestAttack.Name);

        var tilesInRange = pathfinder.GetTilesInRange(unit.floor, longestAttack.Range, true);

        var unitsToHit = tilesInRange.Where(t => t.Occupied ? t.occupier.tag == "Player" : false).Select(c => c.occupier.GetComponent<Character>());

        if(unitsToHit.Count() != 0)
        {
            unit.selectedAttack = longestAttack;
            unit.attackSourceBlock = unitsToHit.First().floor;
            unit.Attack();
        }        

        unit.moveComplete -= AIAttack;
    }
}


public enum AIStates
{
    Regroup, Retreat, Attack
}
