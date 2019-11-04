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
        hitPoints = 20;
        accuracy = 75;
        armour = 3;
        evade = 25;
        manaPoints = 0;
        power = 7;
        resistance = 1;
        movementSpeed = 5;
        movemenSprint = 5;

        attacks = new HashSet<Attacks>();

        attacks.Add(AttackLibrary.attacks.First(a => a.name == "RustySword"));
    }
}
