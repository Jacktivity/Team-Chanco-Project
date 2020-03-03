using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FloatingSkullCharacter : Character
{
    // Start is called before the first frame update
    void Start()
    {
        name = "FlamingSkull";
        ID = 2;
        cost = 15;
        maxHitPoints = 12;
        accuracy = 90;
        armour = 1;
        evade = 25;
        manaPoints = 50;
        power = 1;
        resistance = 1;
        movementSpeed = 4;
        movemenSprint = 2;

        currentHitPoints = maxHitPoints;

        attacks = new Attack[] {
            AttackLibrary.Headbutt,
            AttackLibrary.Firebolt
        };
    }
}
