using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField]UIManager uiManager;
    [SerializeField]Pathfinder pathFinder;

    [SerializeField] public GridManager gridManager;

    Attack attack;

    public Character attacker;
    public Character target;

    public IEnumerable<Attack> attacks;

    public bool waiting = false;
    public bool attackerAssigned = false;
    public bool targetAssigned = false;
    public bool attackAssigned = false;

    private void Start()
    {
        pathFinder = pathFinder.GetComponent<Pathfinder>();
        uiManager = GetComponent<UIManager>();
        gridManager = gridManager.GetComponent<GridManager>();
    }

    public void Attack(Character attacking, Character defending, Attack attack)
    {
        ClearAttack();

        attacker = attacking;
        target = defending;
        this.attack = attack;

        uiManager.blocksInRange = pathFinder.GetTilesInRange(attacker.floor, attack.Range, true);

        attackerAssigned = true;
        targetAssigned = true;
        attackAssigned = true;

        Attack();
    }

    public void AssignAttacker(Character character)
    {
    //    attackerAssigned = true;
    //    attacker = character;

    //    attacks = attacker.Attack();
    //    if (uiManager.attacking) {
    //        uiManager.DisplayAttacks(attacks);
    //    }

    //    if (attackAssigned)
    //    {
    //        attack = null;
    //        attackAssigned = false;
    //    }
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

    public void AssignAttack(Attack _attack)
    {
        attackAssigned = true;
        attack = _attack;

        uiManager.ClearRangeBlocks();

        uiManager.blocksInRange = pathFinder.GetTilesInRange(attacker.floor, attack.Range, true);

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
            int attackRoll1 = Random.Range(1, 101);
            int attackRoll2 = Random.Range(1, 101);
            int attackRoll = (attackRoll1 + attackRoll2) / 2;
            float hitChance = 100 - (attacker.accuracy * attack.Accuracy) - (target.evade /*+ terrain.defence */);
            if (attackRoll >= hitChance)
            {
                var damage = attack.RollDamage();               

                target.TakeDamage(damage.Magical);
                target.TakeDamage(damage.Physical);
                HealthBar healthBar = target.GetComponent<HealthBar>();
                healthBar.slider.value = target.GetHealth();
                Debug.Log("Attacked! " + attacker.name + " attacked " + target.name + " with " + attack.Name + " dealing (" + " (Attacker Power: " +  attacker.power + " + " + " Damage " + damage.Magical + damage.Physical + ") - " + " Target Armour: " + target.armour + " OR Target Resistance: " + target.resistance + ") Overall Damage = " + damage + ". Leaving " + target.name + " with " + target.GetHealth() + " health left.");
            }
            else
            {
                Debug.Log("The attack missed! The attack roll was " + attackRoll + " and the hit chance was " + hitChance);
            }

            uiManager.ClearRangeBlocks();
            attacker.hasTurn = false;
            if (attacker.tag == "Player")
            {
                gridManager.nextUnit();
            }

            if (attacker.gameObject.tag == "Player")
                gridManager.CycleTurns();
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
        gridManager.ClearMap();
    }
}
