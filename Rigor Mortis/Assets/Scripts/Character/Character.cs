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
    public int cost, maxHitPoints, accuracy, power, evade, armour, resistance, movementSpeed, movemenSprint, manaPoints;
    #endregion

    [SerializeField] protected int currentHitPoints;

    public bool isFlying;
    public bool isCaptain;

    public IEnumerable<Attack> attacks;

    public UIManager uiManager;
    //public AttackManager attackManager;
    public Pathfinder pathfinder;
    public PlayerManager playerManager;
    //public bool hasTurn, movedThisTurn;
    private int maxActionPoints;
    public int ActionPoints { get; private set; }
    public BlockScript floor;
    public bool moving = false;
    private float counterTime;
    private const float moveAnimationSpeed = 6;


    public EventHandler<Character> characterClicked;
    public EventHandler<Character> moveComplete, attackComplete;
    public static EventHandler<AttackEventArgs> attackEvent;
    public Attack selectedAttack;
    public BlockScript attackSourceBlock;

    private IEnumerable<BlockScript> path;
    private int pathIndex;
    private BlockScript moveToBlock;

    private Score score;
    public GameObject godRay, AP_VFX_Full, AP_VFX_Half, AP_VFX_Empty;

    public bool beingAttacked;
    public EnemySelectButton beingAttackedButton;


    private void Awake()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        animator = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>();
        playerManager = FindObjectOfType<PlayerManager>();
        score = FindObjectOfType<Score>();
        //attackManager = FindObjectOfType<AttackManager>();
        previousForward = transform.forward;
        attackEvent += DamageCheck;
        //Movement costs 2AP, Attacking costs 3AP
        ActionPoints = maxActionPoints = 4;
        godRay.SetActive(false);

        ChooseAttackButton.attackChosen += (s, e) =>
        {
            var attackEvent = e as ChooseAttackButton.CharacterAttack;
            if (attackEvent.attacker == this)
                selectedAttack = attackEvent.attackChosen;
        };
    }

    private void Start()
    {
        currentHitPoints = maxHitPoints;
    }

    public Attack[] UseableAttacks => attacks.Where(a => a.Mana <= manaPoints).ToArray();

    public void ClearActionPoints()
    {
        ActionPoints = 0;
        if (tag == "Player") {
            gameObject.GetComponent<ActionPointBar>().slider.value = ActionPoints;
        }
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
            manaPoints -= selectedAttack.Mana;
            if (tag == "Player" ) {
                gameObject.GetComponent<ActionPointBar>().slider.value = ActionPoints;
            }
            var baseDamage = selectedAttack.RollDamage();

            if (baseDamage.Magical > 0)
                baseDamage.Magical += power;
            if (baseDamage.Physical > 0)
                baseDamage.Physical += power;
            
            var tilesInRange = pathfinder.GetTilesInRange(attackSourceBlock, selectedAttack.Area, true);
            var charactersToHit = tilesInRange.Where(t => t.Occupied).Select(s => s.occupier.GetComponent<Character>()).ToArray();

            attackEvent?.Invoke(this, new AttackEventArgs(charactersToHit, baseDamage.Magical, baseDamage.Physical, selectedAttack.Accuracy * accuracy));
            attackComplete?.Invoke(this, this);

        }
        godRay.SetActive(false);
    }

    public void SpendAP(int actionPoints) => ActionPoints -= actionPoints;

    private void Movement()
    {
        if (moving)
        {
            counterTime += Time.deltaTime * moveAnimationSpeed;
            moveToBlock = path.ElementAt(pathIndex);

            //float journey = Vector3.Distance(transform.position, (moveToBlock.transform.position + transform.up));


            transform.position = Vector3.Lerp(
                new Vector3(previousBlock.transform.position.x, transform.position.y, previousBlock.transform.position.z),
                new Vector3(moveToBlock.transform.position.x, moveToBlock.transform.position.y + 1, moveToBlock.transform.position.z),
                counterTime);


            var angle = moveToBlock.transform.position - previousBlock.transform.position;

            transform.forward = Vector3.Lerp(new Vector3(previousForward.x, 0, previousForward.z), new Vector3(angle.x, 0, angle.y), counterTime);

            HealthBar healthBar = GetComponent<HealthBar>();
            Vector3 healthOffset = healthBar.offset;
            healthBar.slider.transform.position = transform.position + healthOffset;

            if (tag == "Player") {
                ActionPointBar actionPointBar = gameObject.GetComponent<ActionPointBar>();
                Vector3 actionPointOffset = actionPointBar.offset;
                actionPointBar.slider.transform.position = transform.position + actionPointOffset;

                gameObject.GetComponent<ActionPointBar>().slider.value = ActionPoints;
            }


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
            godRay.SetActive(false);
        }
    }

    protected void DamageCheck(object sender, AttackEventArgs e)
    {
        if (e.AttackedCharacters.Contains(this))
        {
            var toHit = e.Accuracy - evade;

            var dodgeRoll = UnityEngine.Random.Range(1, 101);


            if (dodgeRoll < toHit)
            {
                if (e.MagicDamage > resistance)
                    TakeDamage(e.MagicDamage - resistance);

                if (e.PhysicalDamage > armour)
                    TakeDamage(e.PhysicalDamage - armour);
            }
            beingAttacked = false;

            
        }
    }

    public void TakeDamage(int damage)
    {
        currentHitPoints -= damage;

        if(currentHitPoints <= 0)
        {
            if(name == "Necromancer")
            {
                playerManager.RemoveNecromancer(this);
                uiManager.GameOverCheck();
            }
            DestroyUnit();
        }
        else
        {
            gameObject.GetComponent<HealthBar>().slider.value = currentHitPoints;
        }
    }

    protected void DestroyUnit()
    {
        floor.occupier = null;
        if(gameObject.tag != "Breakable_Terrain")
        {
            gameObject.GetComponent<HealthBar>().slider.gameObject.SetActive(false);
            Slider healthSlider = GetComponent<HealthBar>().slider;
            healthSlider.gameObject.SetActive(false);
        }

        if (tag == "Player") {
            if(name == "Necromancer") {
                score.RemoveScore(50);
            } else {
                score.RemoveScore(cost);
            }

            Slider APSlider = GetComponent<ActionPointBar>().slider;
            APSlider.gameObject.SetActive(false);
        } else if(tag == "Enemy") {
            if (name == "Necromancer")
            {
                score.AddScore(50);
            }
            else
            {
                score.AddScore(cost);
            }
        }

        this.gameObject.SetActive(false);
    }

    public void MoveUnit(IEnumerable<BlockScript> moveTo)
    {     
        if(moveTo.Count() > 0)
        {
            ActionPoints -= 2;

            if (moveTo.Count() > movementSpeed + 1)
                ActionPoints -= 2;

            Debug.Log(this.tag + " " + this.name + " Action Points: " + ActionPoints);
            path = moveTo;
            pathIndex = 0;
            moving = true;

            if (animator != null)
                animator.SetBool("Moving", moving);            
        } 
    }

    public float GetHealth => currentHitPoints;

    public bool MaxAP => ActionPoints == maxActionPoints;

    public bool CanAttack => ActionPoints >= 2;

    public bool HasAttacked => ActionPoints == maxActionPoints - 3;

    public bool CanMove => ActionPoints > 0;

    public void APReset()
    {
        if (!uiManager.gameOver) {
            ActionPoints = maxActionPoints;
            if (tag == "Player")
            {
                gameObject.GetComponent<ActionPointBar>().slider.value = ActionPoints;
            }
        }
    }

    private void OnMouseDown()
    {
        characterClicked?.Invoke(this, this);      
        if(beingAttacked)
        {
            beingAttackedButton.SelectTarget();
        }
        
    }

    private void OnMouseOver()
    {
        if(beingAttacked)
        {
            godRay.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (beingAttacked)
        {
            godRay.SetActive(false);
        }

    }

    private void Update()
    {
        if(tag == "Player")
        {
            if (MaxAP)
            {
                AP_VFX_Full.SetActive(true);
                AP_VFX_Half.SetActive(false);
                AP_VFX_Empty.SetActive(false);
            }
            else if (CanMove)
            {
                AP_VFX_Full.SetActive(false);
                AP_VFX_Half.SetActive(true);
                AP_VFX_Empty.SetActive(false);
            }
            else
            {
                AP_VFX_Full.SetActive(false);
                AP_VFX_Half.SetActive(false);
                AP_VFX_Empty.SetActive(true);
            }
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
