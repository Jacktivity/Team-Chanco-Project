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

    public TurnManager turnManager;
    public bool hasTurn;
    [SerializeField] GameObject floor;
    //TurnManager turnManager;

    private void Start()
    {
        attacks = new HashSet<Attacks>();
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
    private void OnMouseDown()
    {
        if (hasTurn)
        {
            hasTurn = false;
            turnManager.CycleTurns();
        }
    }
}
