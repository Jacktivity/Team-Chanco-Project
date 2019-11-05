using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Canvas battleCanvas;
    Canvas prepCanvas;

    Text turnDisplay;

    bool attacking = false;
    public HashSet<Attacks> attacks;

    // Start is called before the first frame update
    void Start()
    {
        battleCanvas = GameObject.Find("BattleCanvas").GetComponent<Canvas>();
        turnDisplay = GameObject.Find("TurnNumberDisplay").GetComponentInChildren<Text>();
    }

    public void UpdateTurnNumber(int turn)
    {
        turnDisplay.text = "Turn " + turn;
    }

    public void Attack(Character character)
    {
        attacking = true;
        //attacks = character.Attack();
        //DisplayAttacks(attacks);
    }

    public void DisplayAttacks(HashSet<Attacks> _attacks)
    {
        foreach(var attack in _attacks)
        {

        }
    }
}
