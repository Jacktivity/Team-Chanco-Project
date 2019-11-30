using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLibrary
{
    public static Attacks[] attacks = new Attacks[] {
        // Melee Attacks
        new Attacks(2, 1, 0, 0, 1, 1,"StaffWhack"),
        new Attacks(4, 2, 0, 0, 1, 1, "RustySword"),
        new Attacks(0, 0, 2, 4, 1, 1, "SpectralSword"),
        new Attacks(2, 1, 0, 0, 1, 0.8f, "Whack"),
        // Magic Attacks
        new Attacks(0, 0, 4, 4, 1, 1, "TeslaStab"),
        new Attacks(0, 0, 4, 4, 10, 1.2f, "TeslaZap"),
        new Attacks(0, 0, 4, 4, 10, 1.2f, "Zap"),
    };

    public static Attack StaffWhack => new Attack(1, 1f, "StaffWhack", physicalDmg: new Dice(1, 2));
    public static Attack RustySword => new Attack(1, 1, "RustySword", physicalDmg: new Dice(2, 2));
    public static Attack SpectralSword => new Attack(1, 1, "SpectralSword", magicDmg: new Dice(2, 2));
    public static Attack Whack => new Attack(1, 0.8f, "Whack", physicalDmg: new Dice(1, 2));

    public static Attack TeslaStab => new Attack(1, 1, "TeslaStab", magicDmg: new Dice(4, 1));
    public static Attack TeslaZap => new Attack(10, 1.2f, "TeslaZap", magicDmg: new Dice(4, 1));
    public static Attack Zap => new Attack(10, 1.2f, "Zap", magicDmg: new Dice(4, 1));
}

public class Attacks
{
    public int physicalMaxAttack;
    public int physicalMinAttack;
    public int magicalMaxAttack;
    public int magicalMinAttack;
    public int range;
    public float accuracy;
    public string name;

    public Attacks(int _PhyMaxAtk, int _PhyMinDam, int _magicMaxAtk, int _magicMinAtk,int _range, float _acc, string _name)
    {
        physicalMaxAttack = _PhyMaxAtk;
        physicalMinAttack = _PhyMinDam;
        magicalMaxAttack = _magicMaxAtk;
        magicalMinAttack = _magicMinAtk;
        range = _range;
        accuracy = _acc;
        name = _name;
    }
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
    public int Mana;

    public Attack(int atkRange, float accuracy, string name, Dice physicalDmg = null, Dice magicDmg = null, int atkArea = 1, bool targetEmptyTiles = false, int manaCost = 0)
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

    public Tuple<int,int> RollDamage()
    {
        int phys = PhysicalDamage?.Roll() ?? 0;
        int magic = MagicalDamage?.Roll() ?? 0;

        return new Tuple<int, int>(phys,magic);
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
