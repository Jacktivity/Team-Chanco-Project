using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Enemy[] Units => GetComponentsInChildren<Enemy>();

    private Dictionary<Character, AIStates> enemyMood;
    [SerializeField] private Pathfinder pathfinder;

    public void Start()
    {
        enemyMood = new Dictionary<Character, AIStates>();
        GridManager.enemySpawned += EnemySpawnEvent;
    }

    private void EnemySpawnEvent(object sender, EnemySpawn e)
    {
        enemyMood.Add(e.unit, e.defaultState);
        var enemy = e.unit.gameObject.AddComponent<Enemy>();
        enemy.AssignData(e);

        Character.attackEvent += AIStateChangeCheck;
    }

    private void AIStateChangeCheck(object sender, AttackEventArgs e)
    {

    }

    public bool MoveUnit(Character unit)
    {
        Attack attackToUse;
        BlockScript[] path;

        switch (enemyMood[unit])
        {
            case AIStates.Stationary:
                break;
            case AIStates.Defensive:
                attackToUse = unit.UseableAttacks.OrderByDescending(a => a.Range).First();
                

                break;
            case AIStates.Aggressive:                
                attackToUse = unit.UseableAttacks.OrderByDescending(a => a.Range).First();
            
                path = pathfinder.GetPath(unit.floor, (s) => pathfinder.GetTilesInRange(s, attackToUse.Range, true).Any(t => t.Occupied ? t.occupier.CompareTag("Player") : false), unit.isFlying == false);                

                var walkPath = path.Take(unit.movementSpeed);

                bool walked = true;

                if (walkPath.Last() != path.Last())
                {
                    walked = false;
                    walkPath = path.Take(unit.movemenSprint + unit.movementSpeed);
                }

                unit.MoveUnit(walkPath);

                if (walked)
                {
                    unit.moveComplete += AIAttack;
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
    Aggressive, Defensive, Stationary, Thief
}

public struct EnemySpawn
{
    public Character unit;
    public int id;
    public IEnumerable<int> linkedUnits;
    public AIStates defaultState;


    public EnemySpawn(Character character, AIStates state, int unitID, IEnumerable<int> linkedUnitIds)
    {
        unit = character;
        defaultState = state;
        id = unitID;
        linkedUnits = linkedUnitIds;
    }
}
