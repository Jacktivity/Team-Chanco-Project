using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIManager : MonoBehaviour
{
    Canvas battleCanvas;
    Canvas prepCanvas;
    Canvas fixedCanvas;

    Text turnDisplay;

    public bool attacking = false;
    public bool waiting = false;
    public bool attackerAssigned = false;
    public bool targetAssigned = false;

    public HashSet<Attacks> attacks;

    public Character attacker;
    public Character target;

    public GameObject popUpButton;

    // Start is called before the first frame update
    void Start()
    {
        battleCanvas = GameObject.Find("BattleCanvas").GetComponent<Canvas>();
        prepCanvas = GameObject.Find("PrepCanvas").GetComponent<Canvas>();
        fixedCanvas = GameObject.Find("FixedCanvas").GetComponent<Canvas>();

        turnDisplay = GameObject.Find("TurnNumberDisplay").GetComponentInChildren<Text>();

        popUpButton = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/UI/PopUpButton.prefab", typeof(GameObject));
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
    }

    public void AssignTarget(Character character)
    {
        targetAssigned = !targetAssigned;
        target = character;

        attacks = attacker.Attack();
        DisplayAttacks(attacks);

        //Attack(); 
    }

    public void Attack()
    {
        Debug.Log("Attacked! " + attacker.name + " attacked " + target.name);

        attackerAssigned = false;
        targetAssigned = false;
        attacking = false;

        attacker = null;
        target = null;
    }

    public void wait()
    {
        waiting = true;
    }

    public void DisplayAttacks(HashSet<Attacks> _attacks)
    {
        Vector3 popUpOffset = new Vector3(2f,0,0);
        Vector3 instantiationPoint = target.transform.position + popUpOffset;

        for(int i = 0; i < _attacks.Count; i++)
        {
            popUpOffset = new Vector3(0, 0, 1 * i);

            GameObject button = Instantiate(popUpButton, instantiationPoint + popUpOffset, battleCanvas.transform.rotation, fixedCanvas.transform);
            button.GetComponentInChildren<Text>().text = _attacks.ElementAt(i).name;
        }
    }
}
