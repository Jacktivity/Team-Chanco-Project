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
        new Attacks(0, 0, 4, 4, 10, 1.2f, "Whack"),
    };

    public static Dictionary<string, Attacks> atkLibrary = new Dictionary<string, Attacks>()
    {
        { "Bob", new Attacks(0, 0, 0, 0, 0, 0, "Bob") },
        { "StaffWhack", new Attacks(2, 1, 0, 0, 1, 1,"StaffWhack") },
        { "RustySword", new Attacks(4, 2, 0, 0, 1, 1, "RustySword")},
        { "SpectralSword", new Attacks(0, 0, 2, 4, 1, 1, "SpectralSword")},
        { "Whack", new Attacks(2, 1, 0, 0, 1, 0.8f, "Whack")},
        // Magic Attacks
        { "TeslaStab", new Attacks(0, 0, 4, 4, 1, 1, "TeslaStab")},
        { "TeslaZap", new Attacks(0, 0, 4, 4, 10, 1.2f, "TeslaZap")},
        { "Whack", new Attacks(0, 0, 4, 4, 10, 1.2f, "Whack")},
    };
}

public class Attacks
{
    int physicalMaxAttack;
    int physicalMinAttack;
    int magicalMaxAttack;
    int magicalMinAttack;
    int range;
    float accuracy;
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
