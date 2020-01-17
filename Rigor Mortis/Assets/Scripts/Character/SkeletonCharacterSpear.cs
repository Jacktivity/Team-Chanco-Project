using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkeletonCharacterSpear : Character
{
    // Start is called before the first frame update
    void Start()
    {
        name = "Skeleton_Spear";
        cost = 15;
        maxHitPoints = 20;
        accuracy = 90;
        armour = 3;
        evade = 20;
        manaPoints = 0;
        power = 7;
        resistance = 1;
        movementSpeed = 3;
        movemenSprint = 2;
        
        currentHitPoints = maxHitPoints;

        attacks = new Attack[] { AttackLibrary.Spear };
    }
}
