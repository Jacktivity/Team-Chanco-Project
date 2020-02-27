using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TombGuard : Character
{
    // Start is called before the first frame update
    void Start()
    {
        name = "Tomb_Guard";
        ID = 6;
        cost = 30;
        maxHitPoints = 25;
        accuracy = 80;
        armour = 7;
        evade = 0;
        manaPoints = 0;
        power = 10;
        resistance = 1;
        movementSpeed = 2;
        movemenSprint = 2;

        currentHitPoints = maxHitPoints;

        attacks = new Attack[] { AttackLibrary.RustySword };
    }
}