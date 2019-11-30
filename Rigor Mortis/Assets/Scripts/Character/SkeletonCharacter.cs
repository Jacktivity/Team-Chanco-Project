using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkeletonCharacter : Character
{
    // Start is called before the first frame update
    void Start()
    {
        cost = 10;
        maxHitPoints = 20;
        accuracy = 75;
        armour = 3;
        evade = 25;
        manaPoints = 0;
        power = 7;
        resistance = 1;
        movementSpeed = 3;
        movemenSprint = 2;

        attacks = new Attack[] { AttackLibrary.RustySword };
    }
}
