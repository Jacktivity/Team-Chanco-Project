﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] UIManager uiManager;
    [SerializeField] Pathfinder pathFinder;

    Attacks attack;

    public Character attacker;
    public Character target;

    public HashSet<Attacks> attacks;

    public bool waiting = false;
    public bool attackerAssigned = false;
    public bool targetAssigned = false;
    public bool attackAssigned = false;

    private void Start()
    {
        pathFinder = pathFinder.GetComponent<Pathfinder>();
        uiManager = GetComponent<UIManager>();
    }

    public void AssignAttacker(Character character)
    {
        attackerAssigned = true;
        attacker = character;

        attacks = attacker.Attack();
        uiManager.DisplayAttacks(attacks);

        if (attackAssigned)
        {
            attack = null;
            attackAssigned = false;
        }
    }

    public void AssignTarget(Character character)
    {
        targetAssigned = true;
        target = character;

        if (attackerAssigned && attackAssigned)
        {
            Attack();
        }
        //attacks = attacker.Attack();
        //DisplayAttacks(attacks);

        //Attack(); 
    }

    public void AssignAttack(Attacks _attack)
    {
        attackAssigned = true;
        attack = _attack;

        uiManager.ClearRangeBlocks();

        uiManager.blocksInRange = pathFinder.GetTilesInRange(attacker.floor, attack.range, true);

        foreach (var block in uiManager.blocksInRange)
        {
            block.GetComponent<Renderer>().material.color = Color.red;
        }

        if (targetAssigned && attackerAssigned)
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (uiManager.blocksInRange.Contains<BlockScript>(target.floor))
        {
            int damage = Random.Range(attack.physicalMinAttack, attack.physicalMaxAttack);
            target.TakeDamage(damage);

            Debug.Log("Attacked! " + attacker.name + " attacked " + target.name + " with " + attack.name + " dealing " + damage + " damage. Leaving " + target.name + " with " + target.GetHealth() + " health left");

            ClearAttack();
        }
        else
        {
            Debug.Log("Target is out of range");
        }
    }

    public void ClearAttack()
    {
        attacker = null;
        target = null;

        attackerAssigned = false;
        targetAssigned = false;
        uiManager.attacking = false;
        attackerAssigned = false;
        attackAssigned = false;

        foreach (GameObject button in uiManager.popUpButtons)
        {
            Destroy(button);
        }
        uiManager.popUpButtons.Clear();
    }
}