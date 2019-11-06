using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIManager : MonoBehaviour
{
    [SerializeField]Pathfinder pathFinder;

    [SerializeField]Canvas battleCanvas;
    [SerializeField]Canvas prepCanvas;
    [SerializeField]Canvas fixedCanvas;

    [SerializeField]Text turnDisplay;

    public bool attacking = false;
    public bool waiting = false;
    public bool attackerAssigned = false;
    public bool targetAssigned = false;

    public HashSet<Attacks> attacks;

    public Character attacker;
    public Character target;

    public GameObject attackButton;
    public List<GameObject> popUpButtons;

    Attacks attack;
    BlockScript[] blocksInRange;

    // Start is called before the first frame update
    void Start()
    {
        pathFinder = pathFinder.GetComponent<Pathfinder>();
        turnDisplay = GetComponent<Text>();

        attackButton = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/UI/AttackButton.prefab", typeof(GameObject));
    }

    public void UpdateTurnNumber(int turn)
    {
        turnDisplay.text = "Turn " + turn;
    }

    public void ToggleAttack()
    {
        attacking = !attacking;
        //attacks = character.Attack();
        //DisplayAttacks(attacks);
    }

    public void AssignAttacker(Character character)
    {
        attackerAssigned = !attackerAssigned;
        attacker = character;

        attacks = attacker.Attack();
        DisplayAttacks(attacks);
    }

    public void AssignTarget(Character character)
    {
        targetAssigned = !targetAssigned;
        target = character;

        Attack();

        //attacks = attacker.Attack();
        //DisplayAttacks(attacks);

        //Attack(); 
    }

    public void AssignAttack(Attacks _attack)
    {
        attack = _attack;

        blocksInRange = pathFinder.GetTilesInRange(attacker.floor, attack.range, true);

        foreach(var block in blocksInRange)
        {
            block.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public void Attack()
    {
        if (blocksInRange.Contains<BlockScript>(target.floor)) {
            int damage = Random.Range(attack.physicalMinAttack, attack.physicalMaxAttack);
            target.TakeDamage(damage);

            Debug.Log("Attacked! " + attacker.name + " attacked " + target.name + " with " + attack.name + " dealing " + damage + " damage. Leaving " + target.name + " with " + target.GetHealth() + " health left");

            attackerAssigned = false;
            targetAssigned = false;
            attacking = false;

            attacker = null;
            target = null;

            foreach (GameObject button in popUpButtons)
            {
                Destroy(button);
            }
            popUpButtons.Clear();
        } else
        {
            Debug.Log("Target is out of range");
        }
    }

    public void wait()
    {
        waiting = true;
    }

    public void DisplayAttacks(HashSet<Attacks> _attacks)
    {
        Vector3 popUpOffset = new Vector3(2f,0,0);
        Vector3 instantiationPoint = fixedCanvas.transform.position + popUpOffset;
        popUpButtons = new List<GameObject>();

        for(int i = 0; i < _attacks.Count; i++)
        {
            popUpOffset = new Vector3(0, 0, 1 * i);

            GameObject button = Instantiate(attackButton, instantiationPoint + popUpOffset, battleCanvas.transform.rotation, fixedCanvas.transform);
            popUpButtons.Add(button);
            button.GetComponent<ChooseAttackButton>().uiManager = this;
            button.GetComponent<ChooseAttackButton>().attack = _attacks.ElementAt(i);
            button.GetComponentInChildren<Text>().text = _attacks.ElementAt(i).name;
        }
    }
}
