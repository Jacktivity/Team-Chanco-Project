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
    public float heightOffset;
    [SerializeField] private GameObject VFXGameObject;
    [SerializeField] private Vector3 deselectedVFXscale, selectedVFXscale;

    //0 = necromancer, 1 = skeleton, 2 = SteamingSkull, 3 = SpectralSkeleton, 4 = TombGuard
    #region statblock
    public int ID, cost, maxHitPoints, accuracy, power, evade, armour, resistance, movementSpeed, movemenSprint, manaPoints;
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

    //private PersistantData score;
    public GameObject godRay, AP_VFX_Full, AP_VFX_Half, AP_VFX_Empty;

    public bool beingAttacked;
    public EnemySelectButton beingAttackedButton;

    public int type;

    private void Awake()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        animator = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>();
        playerManager = FindObjectOfType<PlayerManager>();
        //score = FindObjectOfType<PersistantData>();
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

    private void OnDestroy()
    {
        attackEvent -= DamageCheck;
    }

    private void Start()
    {
        currentHitPoints = maxHitPoints;
    }

    public Attack[] UseableAttacks => attacks.Where(a => a.Mana <= manaPoints).ToArray();

    public void ClearActionPoints()
    {
        ActionPoints = 0;
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

    public void ScaleVFX(bool unitSelected)
    {
        var scale = unitSelected ? selectedVFXscale : deselectedVFXscale;

        foreach (var vfxTransform in VFXGameObject.GetComponentsInChildren<Transform>())
        {
            vfxTransform.localScale = scale;
        }
    }

    public void SpendAP(int actionPoints) => ActionPoints -= actionPoints;

    private void Movement()
    {
        if (moving)
        {
            counterTime += Time.deltaTime * moveAnimationSpeed;
            moveToBlock = path.ElementAt(pathIndex);

            transform.position = Vector3.Lerp(
                new Vector3(previousBlock.Location().x, previousBlock.Location().y + heightOffset, previousBlock.Location().z),
                new Vector3(moveToBlock.Location().x, moveToBlock.Location().y + heightOffset, moveToBlock.Location().z),
                counterTime);


            var angle = previousBlock.Location() - moveToBlock.Location();

            transform.forward = Vector3.Lerp(new Vector3(previousForward.x, 0, previousForward.z), new Vector3(angle.x, 0, angle.z), counterTime);
            
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

            var health = currentHitPoints;

            if (dodgeRoll < toHit)
            {
                if (e.MagicDamage > resistance)
                    TakeDamage(e.MagicDamage - resistance, true);

                if (e.PhysicalDamage > armour)
                    TakeDamage(e.PhysicalDamage - armour);
            }
            else
            {
                //Attack missed
                UIManager.createFloatingText?.Invoke(this, new SpawnFloatingTextEventArgs(this, "Miss", Color.yellow));
            }
            beingAttacked = false;                          
        }
    }

    public void TakeDamage(int damage, bool magicDamage = false)
    {
        currentHitPoints -= damage;

        var textColour = magicDamage ? Color.blue : Color.red;

        UIManager.createFloatingText?.Invoke(this, new SpawnFloatingTextEventArgs(this, (0 - damage).ToString(), textColour));

        if (currentHitPoints <= 0)
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
                PersistantData.RemoveScore(50);
            } else {
                PersistantData.RemoveScore(cost);
            }

            Slider APSlider = GetComponent<ActionPointBar>().slider;
            APSlider.gameObject.SetActive(false);
        } else if(tag == "Enemy") {
            if (name == "Necromancer")
            {
                PersistantData.AddScore(50);
            }
            else
            {
                PersistantData.AddScore(cost);
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

            //Debug.Log(this.tag + " " + this.name + " Action Points: " + ActionPoints);
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
            //if (tag == "Player")
            //{
            //    gameObject.GetComponent<ActionPointBar>().slider.value = ActionPoints;
            //}
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
