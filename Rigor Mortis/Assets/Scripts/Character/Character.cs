using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int cost, hitPoints, accuracy, power, evade, armour, resistance, movementSpeed, manaPoints;
    enum MeleeAttack{StaffWhack, RustySword, SpectralSword, Whack};
    enum RangedAttack{TeslaStab, TeslaZap, Zap};
    public bool isFlying;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
