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
    public bool hasTurn;
    public BlockScript floor;
    bool moving = false;
    float startTime;


    IEnumerable<BlockScript> path;
    int pathIndex;
    BlockScript block;

    private void Awake()
    {
        attacks = new HashSet<Attacks>();
        uiManager = GameObject.Find("EventSystem").GetComponent<UIManager>();

        startTime = Time.time;
    }

    private void Update()
    {
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
        path = moveTo;
        pathIndex = 0;
        moving = true;
       
    }

    void OnCollisionEnter(Collision collision)
    {
        var blockScript = collision.gameObject.GetComponent<BlockScript>();
        if (blockScript != null)
            floor = blockScript;
    }
    private void OnMouseDown()
    {
        if (hasTurn && this.tag == "Player")
        {
            hasTurn = false;
            turnManager.CycleTurns();
        }

        if(uiManager.attacking)
        {
            if(/*uiManager.attackerAssigned == false && */uiManager.targetAssigned == false && tag == "Player")
            {
                uiManager.AssignAttacker(this);
            } 

            if(uiManager.attackerAssigned && uiManager.targetAssigned == false && tag == "Enemy")
            {
                uiManager.AssignTarget(this);
            }
        }
    }
}
