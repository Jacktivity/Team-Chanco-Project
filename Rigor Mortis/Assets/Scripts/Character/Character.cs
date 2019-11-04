using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Character : MonoBehaviour
{
    //0 = necromancer, 1 = skeleton, 2 = SteamingSkull, 3 = SpectralSkeleton, 4 = TombGuard
    public int cost, hitPoints, accuracy, power, evade, armour, resistance, movementSpeed, movemenSprint, manaPoints;
    
    public bool isFlying;
    public bool isCaptain;

    public HashSet<Attacks> attacks;

    public bool hasTurn;
    [SerializeField] GameObject floor;
    //TurnManager turnManager;

    private void Start()
    {
        attacks = new HashSet<Attacks>();
        AssignUnit();
    }

    public void AssignUnit()
    {
        switch (transform.name)
        {
            case ("Necromancer"):
                attacks.Add(AttackLibrary.attacks.First(a => a.name == "StaffWhack"));
                attacks.Add(AttackLibrary.attacks.First(a => a.name == "TeslaStab"));
                attacks.Add(AttackLibrary.attacks.First(a => a.name == "TeslaZap"));
                break;

            case ("Skeleton"):
                attacks.Add(AttackLibrary.attacks.First(a => a.name == "RustySword"));
                break;

           case ("SteamingSkull"):
                attacks.Add(AttackLibrary.attacks.First(a => a.name == "Whack"));
                attacks.Add(AttackLibrary.attacks.First(a => a.name == "Zap"));
                break;

            case ("SpectralSkeleton"):
                attacks.Add(AttackLibrary.attacks.First(a => a.name == "SpectralSword"));
                break;

            case ("TombGuard"):
                attacks.Add(AttackLibrary.attacks.First(a => a.name == "RustySword"));
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

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Floor")
        {
            floor = collision.transform.gameObject;
        }
    }
}
