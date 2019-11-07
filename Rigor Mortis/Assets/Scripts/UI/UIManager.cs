using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIManager : MonoBehaviour
{
    [SerializeField]Pathfinder pathFinder;
    [SerializeField] GridManager gridManager;
    [SerializeField] TurnManager turnManager;
    [SerializeField]Canvas battleCanvas;
    [SerializeField]Canvas prepCanvas;
    [SerializeField]Canvas fixedCanvas;

    [SerializeField]Text turnDisplay;

    [SerializeField]AttackManager attackManager;

    public bool attacking = false;

    public GameObject attackButton;
    public List<GameObject> popUpButtons;

    public BlockScript[] blocksInRange;

    // Start is called before the first frame update
    void Start()
    {
        pathFinder = GetComponent<Pathfinder>();
        attackManager = GetComponent<AttackManager>();
        gridManager = GetComponent<GridManager>();
        attackButton = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/UI/AttackButton.prefab", typeof(GameObject));
    }

    public void UpdateTurnNumber(int turn)
    {
        turnDisplay.text = "Turn " + turn;
    }

    public void ToggleAttack()
    {
        attacking = !attacking;
        if (attackManager.attackerAssigned) {
            DisplayAttacks(attackManager.attacks);
        }
        if(!attacking)
        {
            ClearRangeBlocks();
            //attackManager.ClearAttack();
        }
    }

    public void wait()
    {
        attackManager.waiting = true;
        turnManager.CycleTurns();
    }

    public void DisplayAttacks(HashSet<Attacks> _attacks)
    {
        foreach (GameObject button in popUpButtons)
        {
            Destroy(button);
        }
        popUpButtons.Clear();

        if (attacking) {
            Vector3 popUpOffset = new Vector3(2f, 0, 0);
            Vector3 instantiationPoint = fixedCanvas.transform.position + popUpOffset;
            popUpButtons = new List<GameObject>();

            for (int i = 0; i < _attacks.Count; i++)
            {
                popUpOffset = new Vector3(325, 30 * i, 100);

                GameObject button = Instantiate(attackButton, instantiationPoint + popUpOffset, battleCanvas.transform.rotation, battleCanvas.transform);
                popUpButtons.Add(button);
                button.GetComponent<ChooseAttackButton>().attackManager = attackManager;
                button.GetComponent<ChooseAttackButton>().attack = _attacks.ElementAt(i);
                button.GetComponentInChildren<Text>().text = _attacks.ElementAt(i).name;
            }
        }// else
        //{
        //    attackManager.ClearAttack();
        //}
    }

    public void ClearRangeBlocks()
    {
        foreach (var block in blocksInRange)
        {
            block.GetComponent<Renderer>().material.color = block.Normal;
        }
    }
}
