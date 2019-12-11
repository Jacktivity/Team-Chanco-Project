using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField]Pathfinder pathFinder;

    [SerializeField] GridManager gridManager;

    [SerializeField]Canvas battleCanvas;
    [SerializeField]Canvas prepCanvas;
    [SerializeField]Canvas fixedCanvas;

    [SerializeField]Text turnDisplay;

    public bool attacking = false;

    [SerializeField] GameObject attackButton;
    public List<GameObject> popUpButtons;

    [SerializeField] Slider healthBar;
    List<Slider> healthBars;
    List<Character> unitList;
    public Vector3 healthBarOffset;

    public BlockScript[] blocksInRange;
    public static EventHandler<UIManager.PlacementStates> placementStateChange;

    public enum PlacementStates
    {
        placementPhase, playerTurn, enemyTurn
    }

    // Start is called before the first frame update
    void Start()
    {
        //pathFinder = GetComponent<Pathfinder>();
        //attackManager = GetComponent<AttackManager>();
        //gridManager = GetComponent<GridManager>();
        //attackButton = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/UI/AttackButton.prefab", typeof(GameObject));
        unitList = new List<Character>();
        healthBars = new List<Slider>();

        BuildUnits();

        placementStateChange += PlacementStateChanged;
        placementStateChange?.Invoke(this, UIManager.PlacementStates.placementPhase);
    }

    public void UpdateTurnNumber(int turn)
    {
        turnDisplay.text = "Turn " + turn;
    }

    public void wait()
    {
        gridManager.CycleTurns();
    }

    public void DisplayAttacks(IEnumerable<Attack> _attacks, Character character)
    {
        foreach (GameObject button in popUpButtons)
        {
            Destroy(button);
        }
        popUpButtons.Clear();

        popUpButtons = new List<GameObject>();

        for (int i = 0; i < _attacks.Count(); i++)
        {
            Vector3 popUpOffset = new Vector3(0, 30 * i, 0);

            GameObject button = Instantiate(attackButton, new Vector3(), battleCanvas.transform.rotation, battleCanvas.transform);
            button.transform.localPosition = new Vector3(250, 100, 0) + popUpOffset;
            popUpButtons.Add(button);

            button.GetComponent<ChooseAttackButton>().character = character;
            button.GetComponent<ChooseAttackButton>().gridManager = gridManager;
            button.GetComponent<ChooseAttackButton>().attack = _attacks.ElementAt(i);
            button.GetComponentInChildren<Text>().text = _attacks.ElementAt(i).Name;
        }
    }

    public void ClearRangeBlocks()
    {
        foreach (var block in blocksInRange)
        {
            block.GetComponent<Renderer>().material.color = block.Normal;
        }
    }


    //Health Bars
    void BuildUnits() {
        foreach (Character unit in FindObjectsOfType<Character>())
        {
            unitList.Add(unit);
            InstantiateHealthBar(unit);
        }
    }

    void InstantiateHealthBar(Character unit) {
        Slider newSlider = Instantiate(healthBar, unit.transform.position + healthBarOffset, fixedCanvas.transform.rotation, fixedCanvas.transform);
        healthBars.Add(newSlider);
        unit.gameObject.AddComponent<HealthBar>().unit = unit;
        unit.gameObject.GetComponent<HealthBar>().slider = newSlider;
        unit.gameObject.GetComponent<HealthBar>().offset = healthBarOffset;
    }

    public void AddUnit(Character newUnit) {
        unitList.Add(newUnit);
        InstantiateHealthBar(newUnit);
    }


    // Unit Assignment
    public void setUnit(Character unit) {
        gridManager.SetSelectedUnit(unit);
    }


    //Canvas
    private void PlacementStateChanged(object sender, PlacementStates state) {
        switch(state)
        {
            case PlacementStates.placementPhase:
                SetPrepCanvas(true);
                SetBattleCanvas(false);
                SetFixedCanvas(false);
            break;

            case PlacementStates.playerTurn:
                SetPrepCanvas(false);
                SetBattleCanvas(true);
                SetFixedCanvas(true);
                break;

            case PlacementStates.enemyTurn:
                SetPrepCanvas(false);
                SetBattleCanvas(false);
                SetFixedCanvas(true);
                break;
        }
    }


    public void SetPrepCanvas(bool enabled) {
        prepCanvas.enabled = enabled;
    }

    public void SetBattleCanvas(bool enabled) {
        battleCanvas.enabled = enabled;
    }

    public void SetFixedCanvas(bool enabled) {
        fixedCanvas.enabled = enabled;
    }
}
