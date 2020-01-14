using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkeletonCharacter : Character
{
    // Start is called before the first frame update
    void Start()
    {
        name = "Skeleton";
        cost = 10;
        maxHitPoints = 20;
        accuracy = 15;
        armour = 3;
        evade = 25;
        manaPoints = 0;
        power = 1;
        resistance = 1;
        movementSpeed = 3;
        movemenSprint = 2;
        
        currentHitPoints = maxHitPoints;

        attacks = new Attack[] { AttackLibrary.RustySword };
    }
}
