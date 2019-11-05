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
    public BlockScript floor;
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

    public void MoveUnit(BlockScript moveTo)
    {
        floor.occupier = null;
        floor = moveTo;
        gameObject.transform.position = moveTo.gameObject.transform.position + gameObject.transform.up;
        floor.occupier = gameObject;
    }

    void OnCollisionEnter(Collision collision)
    {
        var blockScript = collision.gameObject.GetComponent<BlockScript>();
        if (blockScript != null)
            floor = blockScript;
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
