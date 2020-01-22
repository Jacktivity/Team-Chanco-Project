using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLibrary
{
    public static Attack StaffWhack => new Attack(1, 1f, "Staff Whack", physicalDmg: new Dice(1, 2));
    public static Attack RustySword => new Attack(1, 1, "Rusty Sword", physicalDmg: new Dice(2, 2));
    public static Attack SpectralSword => new Attack(1, 1, "Spectral Sword", magicDmg: new Dice(2, 2));
    public static Attack Whack => new Attack(1, 0.8f, "Whack", physicalDmg: new Dice(1, 2));

    public static Attack TeslaStab => new Attack(1, 1, "Tesla Stab", magicDmg: new Dice(4, 1));
    public static Attack TeslaZap => new Attack(10, 1.2f, "Tesla Zap", magicDmg: new Dice(4, 1));
    public static Attack Zap => new Attack(10, 1.2f, "Zap", magicDmg: new Dice(4, 1));

    public static Attack Headbutt => new Attack(1, 0.8f, "Headbutt", physicalDmg: new Dice(1,2));
    public static Attack Firebolt => new Attack(10, 1.2f, "Firebolt", magicDmg: new Dice(10, 1), manaCost:10);


    public static Attack CaplockRifle => new Attack(10, 0.8f, "Caplock Rifle", physicalDmg: new Dice(2, 2));
    public static Attack Axe => new Attack(1, 1, "Axe", physicalDmg: new Dice(3, 2));
    public static Attack Spear => new Attack(2, 1.1f, "Spear", physicalDmg: new Dice(2, 2));

}

public class Attack
{
    public Dice PhysicalDamage { get; private set; }
    public Dice MagicalDamage { get; private set; }
    public int Range { get; private set; }
    public float Accuracy { get; private set; }
    public string Name { get; private set; }
    public int Area { get; private set; }
    public bool TargetEmptyTiles { get; private set; }
    public int Mana { get; private set; }

    public Attack(int atkRange, float accuracy, string name, Dice physicalDmg = null, Dice magicDmg = null, int atkArea = 0, bool targetEmptyTiles = false, int manaCost = 0)
    {
        PhysicalDamage = physicalDmg;
        MagicalDamage = magicDmg;
        Range = atkRange;
        Accuracy = accuracy;
        Name = name;
        Area = atkArea;
        TargetEmptyTiles = targetEmptyTiles;
        Mana = manaCost;
    }

    public Damage RollDamage()
    {
        int phys = PhysicalDamage?.Roll() ?? 0;
        int magic = MagicalDamage?.Roll() ?? 0;

        return new Damage(phys, magic);
    }

    public override string ToString()
    {
        return Name;
    }

    //Assumes that attacks all have unique names
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public double AverageDamage => (PhysicalDamage?.Average() ?? 0) + (MagicalDamage?.Average() ?? 0);
}

public struct Damage
{
    public int Physical;
    public int Magical;

    public Damage(int physicalDamage, int magicalDamage)
    {
        Physical = physicalDamage;
        Magical = magicalDamage;
    }
}

public class Dice
{
    public int Faces { get; private set; }
    public int Count { get; private set; }

    //Follows standard tabletop naming convention for a collection of dice
    //i.e 2d4 (2 Dice with 4 Faces each) is made with Dice(2,4)
    public Dice(int count, int faces)
    {
        Faces = faces;
        Count = count;
    }

    public int Roll()
    {
        if(Faces < 1)
        {
            return 0;
        }

        int roll = 0;
        for (int i = 0; i < Count; i++)
        {
            roll += UnityEngine.Random.Range(1, Faces + 1);
        }

        return roll;
    }

    public double Average()
    {
        return (((double)Faces + 1) / 2) * Count;
    }
}
