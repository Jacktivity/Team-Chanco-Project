using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Enemy[] Units => GetComponentsInChildren<Enemy>();

    private Dictionary<Character, AIStates> enemyMood;
    private Dictionary<int, Character> enemyIDToCharacterScript;
    [SerializeField] private Pathfinder pathfinder;

    public void Start()
    {
        enemyMood = new Dictionary<Character, AIStates>();
        enemyIDToCharacterScript = new Dictionary<int, Character>();
        GridManager.enemySpawned += EnemySpawnEvent;
    }

    private void EnemySpawnEvent(object sender, EnemySpawn spawn)
    {
        enemyMood.Add(spawn.unit, spawn.defaultState);
        enemyIDToCharacterScript.Add(spawn.id, spawn.unit);

        var enemy = spawn.unit.gameObject.AddComponent<Enemy>();
        enemy.AssignData(spawn);

        Character.attackEvent += AIStateChangeCheck;
        Enemy.stateChange += CheckStateChange;
    }

    private void CheckStateChange(object sender, Enemy.AIStateChangeEvent e)
    {
        if(enemyIDToCharacterScript.ContainsKey(e.changeUnitID))
        {
            enemyMood[enemyIDToCharacterScript[e.changeUnitID]] = e.stateToChangeTo;
        }
    }

    private void AIStateChangeCheck(object sender, AttackEventArgs e)
    {
        foreach (var unit in e.AttackedCharacters)
        {            
            if(enemyMood.ContainsKey(unit))
            {
                if(enemyMood[unit] != AIStates.Stationary)
                {
                    enemyMood[unit] = AIStates.Aggressive;

                    var enemyScript = unit.GetComponent<Enemy>();
                    foreach (var linkedUnit in enemyScript.LinkedUnitIDs)
                    {
                        Enemy.stateChange?.Invoke(this, new Enemy.AIStateChangeEvent(AIStates.Aggressive, linkedUnit));
                    }
                }
            }
        }
    }

    public bool MoveUnit(Character unit)
    {
        Attack attackToUse;
        BlockScript[] path;
        
        if(unit.GetComponent<Enemy>().DefaultBehaviour == AIStates.Thief)
        {
            if(pathfinder.Map.Any(s => s.occupier?.CompareTag("Tresure") ?? false))
            {
                ChangeState(AIStates.Thief, unit.GetComponent<Enemy>());
            }
        }
        
        switch (enemyMood[unit])
        {
            case AIStates.Stationary:

                AIAttackCheck(this, unit);
                unit.ClearActionPoints();
                break;

            case AIStates.Defensive:

                attackToUse = unit.UseableAttacks.OrderByDescending(a => a.Range).First();

                path = pathfinder.GetPath(unit.floor, (s) => pathfinder.GetTilesInRange(s, attackToUse.Range, true, true, true).Any(t => t.Occupied ? t.occupier.CompareTag("Player") : false), unit.isFlying == false)
                    .Take(unit.movementSpeed).ToArray();

                if(path.Length != 0)
                {
                    ChangeState(AIStates.Aggressive, unit.GetComponent<Enemy>());
                }

                unit.MoveUnit(path);

                unit.moveComplete += AIAttackCheck;
                break;

            case AIStates.Aggressive:                
                attackToUse = unit.UseableAttacks.OrderByDescending(a => a.Range).First();
            
                path = pathfinder.GetPath(unit.floor, (s) => pathfinder.GetTilesInRange(s, attackToUse.Range, true, true, true).Any(t => t.Occupied? t.occupier.CompareTag("Player") : false), unit.isFlying == false);                

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
                    unit.moveComplete += AIAttackCheck;
                }
                break;

            case AIStates.Thief:

                path = pathfinder.GetPath(unit.floor, (s) => s.AdjacentTiles().Any(t => t.occupier?.CompareTag("Tresure") ?? false), unit.isFlying, unit.isFlying);

                if(path.Take(unit.movementSpeed) == path)
                {
                    unit.MoveUnit(path);
                    unit.moveComplete += ObjectInteractCheck;
                }

                break;
            default:
                break;
        }
        return true;
    }

    private void ObjectInteractCheck(object sender, Character e)
    {
        //Interact with an adjacent object
        throw new NotImplementedException();
    }

    public void ChangeState(AIStates state, Enemy unit)
    {
        enemyMood[unit.Unit] = state;
        foreach (var linkedUnit in unit.LinkedUnitIDs)
        {
            Enemy.stateChange?.Invoke(this, new Enemy.AIStateChangeEvent(state, linkedUnit));
        }        
    }

    private void AIAttackCheck(object sender, Character unit)
    {
        //Improve to use highest attack per possible
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

        unit.moveComplete -= AIAttackCheck;
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
    public int[] linkedUnits;
    public AIStates defaultState;


    public EnemySpawn(Character character, AIStates state, int unitID, int[] linkedUnitIds)
    {
        unit = character;
        defaultState = state;
        id = unitID;
        linkedUnits = linkedUnitIds;
    }
}
