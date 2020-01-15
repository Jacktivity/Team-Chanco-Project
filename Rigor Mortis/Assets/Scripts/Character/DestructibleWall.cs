using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DestructibleWall : Character
{
    // Start is called before the first frame update
    void Start()
    {
        name = "DestructibleWall";
        cost = 0;
        maxHitPoints = 1;
        accuracy = 0;
        armour = 2;
        evade = 0;
        manaPoints = 0;
        power = 0;
        resistance = 5;
        movementSpeed = 0;
        movemenSprint = 0;

        currentHitPoints = maxHitPoints;
    }
}
