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
    #region statblock
    public int cost, maxHitPoints, accuracy, strength, power, evade, armour, resistance, movementSpeed, movemenSprint, manaPoints;
    #endregion

    private int currentHitPoints;

    public bool isFlying;
    public bool isCaptain;

    public IEnumerable<Attack> attacks;

    public TurnManager turnManager;
    public UIManager uiManager;
    //public AttackManager attackManager;
    public Pathfinder pathfinder;
    public bool hasTurn, movedThisTurn;
    public BlockScript floor;
    public bool moving = false;
    float counterTime;

    Color colourStart;

    public EventHandler<Character> characterClicked;
    public EventHandler<Character> moveComplete;
    public static EventHandler<AttackEventArgs> attackEvent;
    public Attack SelectedAttack { get; private set; }
    public BlockScript attackSourceBlock;

    private IEnumerable<BlockScript> path;
    private int pathIndex;
    private BlockScript moveToBlock;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>();
        //attackManager = FindObjectOfType<AttackManager>();
        colourStart = gameObject.GetComponentInChildren<Renderer>().material.color;
        previousForward = transform.forward;

        attackEvent += DamageCheck;
    }

    public void Attack()
    {
        if(SelectedAttack != null)
        {
            var baseDamage = SelectedAttack.RollDamage();
            if (baseDamage.Magical > 0)
                baseDamage.Magical += power;
            if (baseDamage.Physical > 0)
                baseDamage.Physical += strength;

            var tilesInRange = pathfinder.GetTilesInRange(attackSourceBlock, SelectedAttack.Area, true);
            var charactersToHit = tilesInRange.Where(t => t.Occupied).Select(s => s.occupier.GetComponent<Character>()).ToArray();

            attackEvent?.Invoke(this, new AttackEventArgs(charactersToHit, baseDamage.Magical, baseDamage.Physical));
        }
    }

    private void Movement()
    {
        if (moving)
        {
            counterTime += Time.deltaTime * 6;
            moveToBlock = path.ElementAt(pathIndex);

            float journey = Vector3.Distance(transform.position, (moveToBlock.transform.position + transform.up));


            transform.position = Vector3.Lerp(
                new Vector3(previousBlock.transform.position.x, transform.position.y, previousBlock.transform.position.z),
                new Vector3(moveToBlock.transform.position.x, transform.position.y, moveToBlock.transform.position.z),
                counterTime);


            var angle = moveToBlock.transform.position - previousBlock.transform.position;

            transform.forward = Vector3.Lerp(previousForward, angle, counterTime);

            HealthBar healthBar = GetComponent<HealthBar>();
            Vector3 offset = healthBar.offset;
            healthBar.slider.transform.position = transform.position + offset;
            floor.occupier = gameObject;
            if (counterTime >= 1)
            {
                counterTime = 0;
                floor = moveToBlock;
                moveToBlock.occupier = gameObject;
                previousBlock = moveToBlock;

                previousForward = transform.forward;
                if (pathIndex >= path.Count() - 1)
                {
                    moving = false;
                    if (animator != null)
                        animator.SetBool("Moving", moving);
                    pathIndex = 0;
                    moveComplete?.Invoke(this, this);
                }
                else
                {
                    pathIndex++;
                }
            }
        }
    }

    protected void DamageCheck(object sender, AttackEventArgs e)
    {
        if (e.AttackedCharacters.Contains(this))
        {
            if (e.MagicDamage > resistance)
                TakeDamage(e.MagicDamage - resistance);

            if (e.PhysicalDamage > armour)
                TakeDamage(e.PhysicalDamage - armour);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHitPoints -= damage;

        if(currentHitPoints <= 0)
        {
            DestroyUnit();
        }
    }

    protected void DestroyUnit()
    {
        floor.occupier = null;
        gameObject.GetComponent<HealthBar>().slider.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        Slider slider = GetComponent<HealthBar>().slider;
        slider.gameObject.SetActive(false);
    }

    public float GetHealth()
    {
        return maxHitPoints;
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

        //if (hasTurn || gameObject.tag == "Enemy")
        //{
        //    if (attackManager.waiting)
        //    {
        //        attackManager.waiting = false;
        //        hasTurn = false;
        //        if(gameObject.tag == "Player")
        //        {
        //            floor.manager.GetComponent<GridManager>().nextUnit();
        //        }
        //        turnManager.CycleTurns();

        //    }

        //    if (/*uiManager.attackerAssigned == false && */attackManager.targetAssigned == false && tag == "Player")
        //    {
        //        attackManager.AssignAttacker(this);
        //    }

        //    if (uiManager.attacking)
        //    {
        //        if (attackManager.attackerAssigned && attackManager.targetAssigned == false && tag == "Enemy")
        //        {
        //            attackManager.AssignTarget(this);
        //        }
        //    }
        //}
    }

    private void Update()
    {
        if (!hasTurn)
        {
            //Make highlighter of transparent material? Outline renderer etc?        
            gameObject.GetComponentInChildren<Renderer>().material.color = Color.gray;
        }
        Movement();
    }
}

public struct AttackEventArgs
{
    public Character[] AttackedCharacters;
    public int MagicDamage;
    public int PhysicalDamage;

    public AttackEventArgs(IEnumerable<Character> attackedCharaters, int magicDmg, int physDmg)
    {
        AttackedCharacters = attackedCharaters.ToArray();
        MagicDamage = magicDmg;
        PhysicalDamage = physDmg;
    }
}
