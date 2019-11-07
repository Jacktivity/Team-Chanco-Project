using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField]UIManager uiManager;
    [SerializeField]Pathfinder pathFinder;
    [SerializeField] public GridManager gridManager;

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

    public void Attack(Character attacking, Character defending, Attacks attack)
    {
        ClearAttack();

        attacker = attacking;
        target = defending;
        this.attack = attack;

        uiManager.blocksInRange = pathFinder.GetTilesInRange(attacker.floor, attack.range, true);

        attackerAssigned = true;
        targetAssigned = true;
        attackAssigned = true;

        Attack();
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
        //Attack in progress
        if (uiManager.blocksInRange.Contains<BlockScript>(target.floor))
        {
            int attackRoll = Random.Range(1, 100);
            float hitChance = (attacker.accuracy * attack.accuracy) - (target.evade /*+ terrain.defence */);
            int randomDamageValue = -1;
            int damage = -1;
            if (attackRoll <= hitChance) {
                if (attack.physicalMaxAttack > attack.magicalMaxAttack)
                {
                    randomDamageValue = Random.Range(attack.physicalMinAttack, attack.physicalMaxAttack);
                    damage = (attacker.power + randomDamageValue) - target.armour;
                } else
                {
                    randomDamageValue = Random.Range(attack.magicalMinAttack, attack.magicalMaxAttack);
                    damage = (attacker.power + randomDamageValue) - target.resistance;
                }

                target.TakeDamage(damage);
                Debug.Log("Attacked! " + attacker.name + " attacked " + target.name + " with " + attack.name + " dealing (" + " (Attacker Power: " +  attacker.power + " + " + " Damage " + randomDamageValue + ") - " + " Target Armour: " + target.armour + " OR Target Resistance: " + target.resistance + ") Overall Damage = " + damage + ". Leaving " + target.name + " with " + target.GetHealth() + " health left.");
            }
            else
            {
                Debug.Log("The attack missed! The attack roll was " + attackRoll + " and the hit chance was " + hitChance);
            }

            uiManager.ClearRangeBlocks();
            attacker.hasTurn = false;

            if(attacker.gameObject.tag == "Player")
                attacker.GetComponent<Character>().turnManager.CycleTurns();
            ClearAttack();
        }
        else
        {
            Debug.Log("Target is out of range");
        }

        gridManager.ClearMap();
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
