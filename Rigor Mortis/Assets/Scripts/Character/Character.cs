using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //0 = necromancer, 1 = skeleton, 2 = SteamingSkull, 3 = SpectralSkeleton, 4 = TombGuard
    public int cost, hitPoints, accuracy, power, evade, armour, resistance, movementSpeed, manaPoints;

    protected enum MeleeAttack { StaffWhack, RustySword, SpectralSword, Whack };
    protected enum SpellAttack { TeslaStab, TeslaZap, Zap };
    protected enum RangedAttack { ClockworkRifle };

    private MeleeAttack[] meleeAttacks;
    private SpellAttack[] spellAttacks;
    private RangedAttack[] rangedAttacks;

    public bool isFlying;
    public bool isCaptain;

    private void Start()
    {
        AssignUnit();
    }

    public void AssignUnit()
    {
        switch(transform.name)
        {
            case ("Necromancer"):
                meleeAttacks = new MeleeAttack[] { MeleeAttack.StaffWhack };
                spellAttacks = new SpellAttack[] { SpellAttack.TeslaStab, SpellAttack.TeslaZap };
                spellAttacks = SpellAttack.Zap;
                break;

            case ("Skeleton"):
                meleeAttacks = new MeleeAttack[] { MeleeAttack.RustySword };
                break;

            case ("SteamingSkull"):
                meleeAttacks = new MeleeAttack[] { MeleeAttack.Whack };
                spellAttacks = new SpellAttack[] { SpellAttack.Zap };
                break;

            case ("SpectralSkeleton"):
                meleeAttacks = new MeleeAttack[] { MeleeAttack.SpectralSword };
                break;

            case ("TombGuard"):
                meleeAttacks = new MeleeAttack[] { MeleeAttack.RustySword };
                break;
        }
    }

    public void TakeDamage(int damage)
    {
        hitPoints = hitPoints - damage;
    }

    public float GetHealth()
    {
        return hitPoints;
    }
}
