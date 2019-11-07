using System;
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
    public UIManager uiManager;
    public AttackManager attackManager;
    public Pathfinder pathfinder;
    public bool hasTurn, movedThisTurn;
    public BlockScript floor;
    bool moving = false;
    float startTime;

    Color colourStart;

    public EventHandler<Character> characterClicked;

    IEnumerable<BlockScript> path;
    int pathIndex;
    BlockScript block;

    private void Awake()
    {
        attacks = new HashSet<Attacks>();
        uiManager = GameObject.Find("EventSystem").GetComponent<UIManager>();
        attackManager = GameObject.Find("EventSystem").GetComponent<AttackManager>();
        colourStart = this.gameObject.GetComponent<Renderer>().material.color;
        startTime = Time.time;
    }

    private void Update()
    {

        if (!hasTurn)
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.grey;
            if (gameObject.tag == "Player")
            {
                turnManager.CycleTurns();
            }
        } else
        {
            this.gameObject.GetComponent<Renderer>().material.color = colourStart;
        }
        if (moving)
        {
            block = path.ElementAt(pathIndex);
            float distCovered = (Time.time - startTime) * 0.06f;
            float journey = Vector3.Distance(transform.position, (block.gameObject.transform.position + gameObject.transform.up));
            float fractionOfJourney = distCovered / journey;
            transform.position = Vector3.Lerp(transform.position, (block.gameObject.transform.position + gameObject.transform.up), fractionOfJourney);
            floor.occupier = gameObject;
            if(fractionOfJourney >= 1)
            {
                if(pathIndex >= path.Count() - 1)
                {
                    moving = false;
                    pathIndex = 0;
                    startTime = Time.time;
                } else
                {
                    pathIndex++;
                }
                
            }
        }
    }

    public void TakeDamage(int damage)
    {
        hitPoints = hitPoints - damage;

        if(hitPoints <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public HashSet<Attacks> Attack()
    {
        return attacks;
    }

    public float GetHealth()
    {
        return hitPoints;
    }

    public void MoveUnit(IEnumerable<BlockScript> moveTo)
    {
        if(moveTo.Count() > 0)
        {
            path = moveTo;
            pathIndex = 0;
            moving = true;
        } 
    }

    void OnCollisionEnter(Collision collision)
    {
        var blockScript = collision.gameObject.GetComponent<BlockScript>();
        if (blockScript != null)
            floor = blockScript;
    }
    private void OnMouseDown()
    {

        characterClicked?.Invoke(this, this);
        if (hasTurn || gameObject.tag == "Enemy")
        {
            if (attackManager.waiting)
            {
                attackManager.waiting = false;
                hasTurn = false;
                turnManager.CycleTurns();
            }

            if (uiManager.attacking)
            {
                if (/*uiManager.attackerAssigned == false && */attackManager.targetAssigned == false && tag == "Player")
                {
                    attackManager.AssignAttacker(this);
                }

                if (attackManager.attackerAssigned && attackManager.targetAssigned == false && tag == "Enemy")
                {
                    attackManager.AssignTarget(this);
                }
            }
        }
        /*if(!uiManager.waiting && !uiManager.attacking ) temp for testing
            {
                var moveArea = pathfinder.GetTilesInRange(floor, movementSpeed, false);

                foreach(var tile in moveArea)
                {
                    tile.GetComponent<Renderer>().material.color = Color.blue;
                }
            }*/
    }
}
