﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField]Pathfinder pathFinder;
    [SerializeField]GridManager gridManager;
    [SerializeField]PlayerManager playerManager;

    [SerializeField]Canvas battleCanvas, prepCanvas, fixedCanvas, pauseCanvas, winCanvas, loseCanvas;

    [SerializeField]Text turnDisplay;
    private int turnNumber;

    [SerializeField]GameObject attackButton, targetCharacterButton, popupArea, moveButton, cancelActionBtn, waitButton;
    [SerializeField]private Vector3 targetCharacterOffset;
    [SerializeField]private Vector3 baseAttackPosition;

    public List<GameObject> popUpButtons;


    [SerializeField]GameObject marker;
    List<GameObject> markers;

    [SerializeField]Slider healthBar;
    List<Slider> healthBars;

    [SerializeField] Slider APBar;
    List<Slider> APBars;

    public BlockScript[] blocksInRange;
    public static EventHandler<GameStates> gameStateChange;

    [SerializeField]GameStates currentState;
    [SerializeField]GameStates resumeState;

    public bool isPaused;
    public bool gameOver;

    private Vector3 buttonSpacing = new Vector3(30, 0, 0);

    public Text scorePointsText;
    public Text placementText;

    private Score score;

    public enum GameStates
    {
        placementPhase, playerTurn, enemyTurn, paused, winState, loseState
    }

    // Start is called before the first frame update
    void Start()
    {
        healthBars = new List<Slider>();
        APBars = new List<Slider>();
        markers = new List<GameObject>();
        popUpButtons = new List<GameObject>();

        score = FindObjectOfType<Score>();

        gameStateChange += GameStateChanged;
        gameStateChange?.Invoke(this, GameStates.placementPhase);

        ChooseAttackButton.attackChosen += DisplayTargets;
        Character.attackEvent += ClearAttackUI;

        baseAttackPosition = popupArea.transform.position;
    }


    private void Update() {
        if (Input.GetKeyDown( KeyCode.Escape )) {
            if (!isPaused) {
                resumeState = currentState;
                gameStateChange?.Invoke(this, GameStates.paused);
            } else {
                Resume();
            }
        }
    }

    public void MenuButtonPress()
    {
        resumeState = currentState;
        gameStateChange?.Invoke(this, GameStates.paused);
    }

    public void Resume() {
        isPaused = false;
        gameStateChange?.Invoke( this, resumeState );
    }

    private void ClearAttackUI(object sender, AttackEventArgs e)
    {
        DeleteCurrentPopupButtons();
    }

    private void DisplayTargets(object sender, ChooseAttackButton.CharacterAttack e)
    {
        DeleteCurrentPopupButtons();

        var targetsInRange = e.attacker.pathfinder.GetTilesInRange(e.attacker.floor, e.attackChosen.Range, true)
            .Where(b => b.Occupied ? b.occupier.tag == "Enemy" || b.occupier.tag == "Breakable_Terrain" : false)
            .Select(t => t.occupier.GetComponent<Character>());

        CreateCancelButton(e.attacker);

        if (targetsInRange.Count() == 0)
        {
            //Say no people to hit
            var btn = Instantiate(targetCharacterButton, popupArea.transform);
            popUpButtons.Add(btn);
            btn.transform.localPosition = baseAttackPosition;
            btn.GetComponentInChildren<Text>().text = "No Targets";
        }
        else
        {
            for (int i = 0; i < targetsInRange.Count(); i++)
            {
                var button = Instantiate(targetCharacterButton, popupArea.transform);
                var moveOffset = buttonSpacing * i;
                popUpButtons.Add(button);
                button.transform.localPosition = baseAttackPosition + moveOffset;
                button.GetComponentInChildren<Text>().text = targetsInRange.ElementAt(i).name;
                var enemySelect = button.GetComponent<EnemySelectButton>();
                enemySelect.AssignData(e.attacker, targetsInRange.ElementAt(i));
            }
        }
    }

    public void CreateCancelButton(Character unit)
    {
        var cancel = Instantiate(cancelActionBtn, popupArea.transform);
        popUpButtons.Add(cancel);
        cancel.transform.localPosition = baseAttackPosition - buttonSpacing;
        cancel.GetComponent<CancelAction>().SetActions(unit, popUpButtons.Select(b => b.GetComponent<MoveButton>()).Where(b => b != null));
    }

    public void DeleteCurrentPopupButtons()
    {
        //Clear the previous target buttons
        foreach (var btn in popUpButtons)
        {
            if (btn.GetComponent<MoveButton>() != null)
            {
                btn.GetComponent<MoveButton>().canMove = false;
            }

            Destroy(btn);
        }
        popUpButtons = new List<GameObject>();
    }

    public void UpdateTurnNumber(int turn)
    {
        turnNumber = turn;
        turnDisplay.text = "Turn " + turnNumber;
    }

    public void Wait(Character unit)
    {
        unit.ClearActionPoints();
        DeleteCurrentPopupButtons();
        gridManager.CycleTurns();
    }

    public void EndTurn()
    {
        if (currentState == GameStates.playerTurn) {
            foreach (Character unit in playerManager.unitList)
            {
                if (unit.tag == "Player")
                {
                    unit.ClearActionPoints();
                    DeleteCurrentPopupButtons();
                    unit.godRay.SetActive(false);
                }
            }
        }
        gridManager.CycleTurns();
        gameStateChange?.Invoke(this, GameStates.enemyTurn);
    }

    public void DisplayActionButtons(IEnumerable<Attack> _attacks, Character character)
    {
        DeleteCurrentPopupButtons();

        if(character.CanAttack)
        {
            for (int i = 0; i < _attacks.Count(); i++)
            {
                CreateAttackButton(_attacks, character, i);
            }

            var moveOffset = buttonSpacing * (_attacks.Count() + 1);
            MakeMoveButton(moveOffset, character);

            moveOffset = new Vector3( 30 * (_attacks.Count() + 2), 0, 0);
            MakeWaitButton(moveOffset, character);
        }
        else if(character.CanMove)
        {
            MakeMoveButton(new Vector3(0, 0, 0), character);
            MakeWaitButton(new Vector3(0, 0, 0), character);
        }
        else
        {
            //No actions can be taken add button to represent this
        }
    }

    private void CreateAttackButton(IEnumerable<Attack> _attacks, Character character, int i)
    {
        Vector3 popUpOffset = buttonSpacing * i;

        GameObject button = Instantiate(attackButton, popupArea.transform);
        button.transform.position = baseAttackPosition + popUpOffset;
        popUpButtons.Add(button);

        button.GetComponent<ChooseAttackButton>().character = character;
        button.GetComponent<ChooseAttackButton>().gridManager = gridManager;
        button.GetComponent<ChooseAttackButton>().attack = _attacks.ElementAt(i);
        button.GetComponentInChildren<Text>().text = _attacks.ElementAt(i).Name;
    }

    private void MakeMoveButton(Vector3 buttonOffset, Character character)
    {
        var moveBtn = Instantiate(moveButton, popupArea.transform);
        popUpButtons.Add(moveBtn);
        moveBtn.transform.position = baseAttackPosition + buttonOffset;
        moveBtn.GetComponent<MoveButton>().SetUpMoveButton(character);
    }

    private void MakeWaitButton(Vector3 buttonOffset, Character character)
    {
        var waitBtn = Instantiate(waitButton, popupArea.transform);
        popUpButtons.Add(waitBtn);
        waitBtn.transform.position = baseAttackPosition + buttonOffset;
        waitBtn.GetComponent<Button>().onClick.AddListener(delegate { Wait(character); });
    }

    public void ClearRangeBlocks()
    {
        foreach (var block in blocksInRange)
        {
            block.GetComponent<Renderer>().material.color = block.Normal;
        }
    }

    //Minimap Markers
    public void InstantiateMarker(Character unit)
    {
        switch (unit.tag) {
            case "Enemy":
            GameObject enemyMarker = Instantiate(marker, unit.transform.position, transform.rotation);
            enemyMarker.transform.SetParent(unit.transform);
            enemyMarker.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            markers.Add(enemyMarker);
            break;

            case "Player":
            GameObject playerMarker = Instantiate(marker, unit.transform.position, transform.rotation);
            playerMarker.transform.SetParent(unit.transform);
            playerMarker.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            markers.Add(playerMarker);
            break;

            default:
            break;
        }
    }

    public void InstantiateUIBars(Character unit) {
        Slider newSlider = Instantiate(healthBar, unit.transform.position, fixedCanvas.transform.rotation, fixedCanvas.transform);
        healthBars.Add(newSlider);
        unit.gameObject.AddComponent<HealthBar>().unit = unit;
        unit.gameObject.GetComponent<HealthBar>().slider = newSlider;

        if(unit.tag == "Player")
        {
            Slider apSlider = Instantiate(APBar, unit.transform.position, fixedCanvas.transform.rotation, fixedCanvas.transform);
            APBars.Add(apSlider);
            unit.gameObject.AddComponent<ActionPointBar>().unit = unit;
            unit.gameObject.GetComponent<ActionPointBar>().slider = apSlider;
        }
    }

    public void PlacementPoint(int amount)
    {
        placementText.text = "" + amount;
    }

    // Unit Assignment
    public void SetUnit(Character unit) {
        gridManager.SetSelectedUnit(unit);
    }

    // Unit Assignment
    public void FinishPlacement()
    {
        gridManager.FinishPlacement();
    }

    public void GameOverCheck()
    {
        if (playerManager.activeEnemyNecromancers.Count <= 0)
        {
            //scorePointsText.text = "Score: " + score.EndTurnWin(/*gridManager.levelID, turnNumber*/);
            UIManager.gameStateChange?.Invoke(this, UIManager.GameStates.winState);
            gameOver = true;
        }
        else if (playerManager.activePlayerNecromancers.Count <= 0)
        {
            //scorePointsText.text = "Score: " + score.EndTurnLose();
            UIManager.gameStateChange?.Invoke(this, UIManager.GameStates.loseState);
            gameOver = true;
        }
    }

    //Set Canvas'
    private void GameStateChanged(object sender, GameStates state) {
        if (!isPaused && !gameOver) {
            switch (state) {
                case GameStates.placementPhase:
                    currentState = GameStates.placementPhase;

                    SetPrepCanvas( true );
                    SetBattleCanvas( false );
                    SetFixedCanvas( false );
                    SetPauseCanvas( false );
                    break;

                case GameStates.playerTurn:
                    currentState = GameStates.playerTurn;

                    SetPrepCanvas( false );
                    SetBattleCanvas( true );
                    SetFixedCanvas( true );
                    SetPauseCanvas( false );
                    break;

                case GameStates.enemyTurn:
                    currentState = GameStates.enemyTurn;

                    SetPrepCanvas( false );
                    SetBattleCanvas( false );
                    SetFixedCanvas( true );
                    SetPauseCanvas( false );
                    break;

                case GameStates.winState:
                    currentState = GameStates.winState;

                    SetPrepCanvas(false);
                    SetBattleCanvas(false);
                    SetFixedCanvas(false);
                    SetPauseCanvas(false);
                    SetWinCanvas(true);
                    break;

                case GameStates.loseState:
                    currentState = GameStates.loseState;

                    SetPrepCanvas(false);
                    SetBattleCanvas(false);
                    SetFixedCanvas(false);
                    SetPauseCanvas(false);
                    SetLoseCanvas(true);
                    break;

                case GameStates.paused:
                    currentState = GameStates.paused;

                    isPaused = true;
                    SetPrepCanvas( false );
                    SetBattleCanvas( false );
                    SetFixedCanvas( false );
                    SetPauseCanvas( true );
                    break;
            }
        }
        else {
            resumeState = state;
        }
    }

    public void SetWinCanvas(bool enabled)
    {
        if (!winCanvas.gameObject.activeSelf && enabled) {
            winCanvas.gameObject.SetActive(true);
        }

        if (!winCanvas.enabled != enabled) {
            winCanvas.enabled = enabled;
        }
    }

    public void SetLoseCanvas(bool enabled)
    {
        if (!loseCanvas.gameObject.activeSelf && enabled) {
            loseCanvas.gameObject.SetActive(true);
        }

        if (loseCanvas.enabled != enabled) {
            loseCanvas.enabled = enabled;
        }
    }

    public void SetPrepCanvas(bool enabled) {
        if (!prepCanvas.gameObject.activeSelf && enabled) {
            prepCanvas.gameObject.SetActive(true);
        }

        if (prepCanvas.enabled != enabled) {
            prepCanvas.enabled = enabled;
        }
    }

    public void SetBattleCanvas(bool enabled) {
        if (!battleCanvas.gameObject.activeSelf && enabled) {
            battleCanvas.gameObject.SetActive(true);
        }

        if (battleCanvas.enabled != enabled) {
            battleCanvas.enabled = enabled;
        }
    }

    public void SetFixedCanvas(bool enabled) {
        if (!fixedCanvas.gameObject.activeSelf && enabled) {
            fixedCanvas.gameObject.SetActive(true);
        }

        if (fixedCanvas.enabled != enabled) {
            fixedCanvas.enabled = enabled;
        }
    }

    public void SetPauseCanvas(bool enabled) {
        if (!pauseCanvas.gameObject.activeSelf && enabled) {
            pauseCanvas.gameObject.SetActive(true);
        }

        if (pauseCanvas.enabled != enabled) {
            pauseCanvas.enabled = enabled;
        }
    }
}
