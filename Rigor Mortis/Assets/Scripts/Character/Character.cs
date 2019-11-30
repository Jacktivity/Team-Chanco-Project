using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    private Animator animator;
    private BlockScript previousBlock;
    private Vector3 previousForward;
    //0 = necromancer, 1 = skeleton, 2 = SteamingSkull, 3 = SpectralSkeleton, 4 = TombGuard
    public int cost, hitPoints, accuracy, power, evade, armour, resistance, movementSpeed, movemenSprint, manaPoints;
    
    public bool isFlying;
    public bool isCaptain;

    public IEnumerable<Attack> attacks;

    public TurnManager turnManager;
    public UIManager uiManager;
    public AttackManager attackManager;
    public Pathfinder pathfinder;
    public bool hasTurn, movedThisTurn;
    public BlockScript floor;
    public bool moving = false;
    float counterTime;

    Color colourStart;

    public EventHandler<Character> characterClicked;
    public EventHandler<Character> moveComplete;
    public static EventHandler<Character[]> attack;
    public Attack SelectedAttack { get; private set; }

    IEnumerable<BlockScript> path;
    int pathIndex;
    BlockScript block;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        uiManager = GameObject.Find("EventSystem").GetComponent<UIManager>();
        attackManager = GameObject.Find("EventSystem").GetComponent<AttackManager>();
        colourStart = gameObject.GetComponentInChildren<Renderer>().material.color;
        previousForward = transform.forward;
    }

    private void Update()
    {
        if(!hasTurn)
        {
            //Make highlighter of transparent material? Outline renderer etc?        
            gameObject.GetComponentInChildren<Renderer>().material.color = Color.gray;
        }
        if (moving)
        {
            counterTime += Time.deltaTime * 6;
            block = path.ElementAt(pathIndex);

            float journey = Vector3.Distance(transform.position, (block.transform.position + transform.up));

            transform.position = Vector3.Lerp(
                new Vector3(previousBlock.transform.position.x, transform.position.y, previousBlock.transform.position.z),
                new Vector3(block.transform.position.x, transform.position.y, block.transform.position.z),
                counterTime);

            var angle = block.transform.position - previousBlock.transform.position;

            transform.forward = Vector3.Lerp(previousForward, angle, counterTime);

            HealthBar healthBar = GetComponent<HealthBar>();
            Vector3 offset = healthBar.offset;
            healthBar.slider.transform.position = transform.position + offset;
            floor.occupier = gameObject;
            if(counterTime >= 1)
            {
                counterTime = 0;
                previousBlock = block;
                previousForward = transform.forward;
                if(pathIndex >= path.Count() - 1)
                {
                    moving = false;
                    if (animator != null)
                        animator.SetBool("Moving", moving);
                    pathIndex = 0;
                    moveComplete?.Invoke(this, this);
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
            floor.occupier = null;
            gameObject.GetComponent<HealthBar>().slider.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
            Slider slider = GetComponent<HealthBar>().slider;
            slider.gameObject.SetActive(false);
        }
    }

    public IEnumerable<Attack> Attack()
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

            if (animator != null)
                animator.SetBool("Moving", moving);
            //gameObject.GetComponentInChildren<Renderer>().material.color = colourStart;

            attackManager.ClearAttack();
        } 
    }

    void OnCollisionEnter(Collision collision)
    {
        var blockScript = collision.gameObject.GetComponent<BlockScript>();
        if (blockScript != null)
        {
            floor = blockScript;
            if (previousBlock == null)
                previousBlock = floor;                
        }
            
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
                if(gameObject.tag == "Player")
                {
                    floor.manager.GetComponent<GridManager>().nextUnit();
                }
                turnManager.CycleTurns();

            }

            if (/*uiManager.attackerAssigned == false && */attackManager.targetAssigned == false && tag == "Player")
            {
                attackManager.AssignAttacker(this);
            }

            if (uiManager.attacking)
            {
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
