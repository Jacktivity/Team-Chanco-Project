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

    public UIManager uiManager;
    //public AttackManager attackManager;
    public Pathfinder pathfinder;
    //public bool hasTurn, movedThisTurn;
    private int maxActionPoints;
    public int ActionPoints { get; private set; }
    public BlockScript floor;
    public bool moving = false;
    float counterTime;

    Color colourStart;

    public EventHandler<Character> characterClicked;
    public EventHandler<Character> moveComplete;
    public static EventHandler<AttackEventArgs> attackEvent;
    public Attack selectedAttack;
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
        //Movement costs 2AP, Attacking costs 3AP
        ActionPoints = maxActionPoints = 4;

        ChooseAttackButton.attackChosen += (s, e) =>
        {
            var attackEvent = e as ChooseAttackButton.CharacterAttack;
            if (attackEvent.attacker == this)
                selectedAttack = attackEvent.attackChosen;
        };
    }
       

    public void SetFloor(BlockScript tile)
    {
        previousBlock = tile;
        floor = tile;
    }

    public void Attack()
    {
        if(selectedAttack != null)
        {
            ActionPoints -= 3;

            var baseDamage = selectedAttack.RollDamage();

            if (baseDamage.Magical > 0)
                baseDamage.Magical += power;
            if (baseDamage.Physical > 0)
                baseDamage.Physical += strength;
            
            var tilesInRange = pathfinder.GetTilesInRange(attackSourceBlock, selectedAttack.Area, true);
            var charactersToHit = tilesInRange.Where(t => t.Occupied).Select(s => s.occupier.GetComponent<Character>()).ToArray();

            attackEvent?.Invoke(this, new AttackEventArgs(charactersToHit, baseDamage.Magical, baseDamage.Physical, selectedAttack.Accuracy * accuracy));


            //TODO: Replace with AP
            // hasTurn = false;
            // movedThisTurn = true;
            ActionPoints -= 1;
        }
    }

    public void SpendAP(int actionPoints) => ActionPoints -= actionPoints;

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
                floor.occupier = null;
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
            var toHit = e.Accuracy - evade;
            var dodgeRoll = UnityEngine.Random.Range(1, 101);
            if(dodgeRoll < toHit)
            {
                if (e.MagicDamage > resistance)
                    TakeDamage(e.MagicDamage - resistance);

                if (e.PhysicalDamage > armour)
                    TakeDamage(e.PhysicalDamage - armour);
            }
            
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

    public void MoveUnit(IEnumerable<BlockScript> moveTo)
    {
        ActionPoints -= 2;

        if(moveTo.Count() > 0)
        {
            path = moveTo;
            pathIndex = 0;
            moving = true;

            if (animator != null)
                animator.SetBool("Moving", moving);            
        } 
    }

    public float GetHealth => maxHitPoints;

    public bool CanAttack => ActionPoints >= 2;

    public bool CanMove => ActionPoints > 0;

    public void APReset()
    {
        ActionPoints = maxActionPoints;
    }

    private void OnMouseDown()
    {
        characterClicked?.Invoke(this, this);
        
        uiManager.DisplayAttacks(attacks, this);
    }

    private void Update()
    {
        //TODO Remove and check where things go wrong
        if (ActionPoints <= 0)
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
    public float Accuracy;

    public AttackEventArgs(IEnumerable<Character> attackedCharaters, int magicDmg, int physDmg, float accuracy)
    {
        AttackedCharacters = attackedCharaters.ToArray();
        MagicDamage = magicDmg;
        PhysicalDamage = physDmg;
        Accuracy = accuracy;
    }
}
