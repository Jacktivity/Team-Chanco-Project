using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NecromancerCharacter : Character
{
    [SerializeField] GameObject necroButton;
    // Start is called before the first frame update
    void Start()
    {
        cost = 0;
        maxHitPoints = 40;
        accuracy = 75;
        armour = 2;
        evade = 25;
        manaPoints = 50;
        power = 8;
        resistance = 5;
        movementSpeed = 6;
        movemenSprint = 6;

        attacks = new Attack[] {
            AttackLibrary.StaffWhack,
            AttackLibrary.TeslaStab,
            AttackLibrary.TeslaZap
        };

        if(gameObject.tag == "Player")
        {
            necroButton = GameObject.Find("NecromancerButton");
            necroButton.SetActive(false);
        }
    }
}
