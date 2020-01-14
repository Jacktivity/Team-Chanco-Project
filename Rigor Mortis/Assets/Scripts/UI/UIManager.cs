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
    [SerializeField]GridManager gridManager;

    [SerializeField]Canvas battleCanvas, prepCanvas, fixedCanvas, pauseCanvas, winCanvas, loseCanvas;

    [SerializeField]Text turnDisplay;

    [SerializeField]GameObject attackButton, targetCharacterButton, popupArea, moveButton;
    [SerializeField]private Vector3 baseAttackPosition, targetCharacterOffset;

    public List<GameObject> popUpButtons;


    [SerializeField]GameObject marker;
    List<GameObject> markers;

    [SerializeField]Slider healthBar;
    List<Slider> healthBars;
    public Vector3 healthBarOffset;

    public BlockScript[] blocksInRange;
    public static EventHandler<GameStates> gameStateChange;

    [SerializeField]GameStates currentState;
    [SerializeField]GameStates resumeState;
    public bool isPaused;
    

    public enum GameStates
    {
        placementPhase, playerTurn, enemyTurn, paused, winState, loseState
    }

    // Start is called before the first frame update
    void Start()
    {
        healthBars = new List<Slider>();
        markers = new List<GameObject>();
        popUpButtons = new List<GameObject>();


        gameStateChange += GameStateChanged;
        gameStateChange?.Invoke(this, UIManager.GameStates.placementPhase);

        ChooseAttackButton.attackChosen += DisplayTargets;
        Character.attackEvent += ClearAttackUI;
    }


    private void Update() {
        if (Input.GetKeyDown( KeyCode.Escape )) {
            if (!isPaused) {
                resumeState = currentState;
                gameStateChange?.Invoke(this, UIManager.GameStates.paused);
            } else {
                Resume();
            }
        }
    }

    public void Resume() {
        isPaused = false;
        UIManager.gameStateChange?.Invoke( this, resumeState );
    }

    private void ClearAttackUI(object sender, AttackEventArgs e)
    {
        DeleteCurrentPopupButtons();
    }    

    private void DisplayTargets(object sender, ChooseAttackButton.CharacterAttack e)
    {
        DeleteCurrentPopupButtons();

        var targetsInRange = e.attacker.pathfinder.GetTilesInRange(e.attacker.floor, e.attackChosen.Range, true)
            .Where(b => b.Occupied ? b.occupier.tag == "Enemy" : false)
            .Select(t => t.occupier.GetComponent<Character>());


        
        if (targetsInRange.Count() == 0)
        {
            //Say no people to hit
            var btn = Instantiate(targetCharacterButton, popupArea.transform);            
            popUpButtons.Add(btn);
            btn.GetComponentInChildren<Text>().text = "No Targets";
        }
        else
        {
            for (int i = 0; i < targetsInRange.Count(); i++)
            {
                var button = Instantiate(targetCharacterButton, popupArea.transform);
                var moveOffset = new Vector3(0, 30 * i, 0);
                popUpButtons.Add(button);
                button.transform.localPosition = baseAttackPosition + moveOffset; 
                button.GetComponentInChildren<Text>().text = targetsInRange.ElementAt(i).name;
                var enemySelect = button.GetComponent<EnemySelectButton>();
                enemySelect.AssignData(e.attacker, targetsInRange.ElementAt(i));
            }
        }
    }

    public void DeleteCurrentPopupButtons()
    {
        //Clear the previous target buttons
        foreach (var btn in popUpButtons)
        {
            Destroy(btn);
        }
        popUpButtons = new List<GameObject>();
    }

    public void UpdateTurnNumber(int turn)
    {
        turnDisplay.text = "Turn " + turn;
    }

    public void Wait()
    {
        gridManager.CycleTurns();
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

            var moveOffset = new Vector3(0, 30 * (_attacks.Count() + 1), 0);
            MakeMoveButton(moveOffset, character);
        }  
        else if(character.CanMove)
        {
            MakeMoveButton(new Vector3(0, 0, 0), character);
        }
        else
        {
            //No actions can be taken add button to represent this
        }
    }

    private void CreateAttackButton(IEnumerable<Attack> _attacks, Character character, int i)
    {
        Vector3 popUpOffset = new Vector3(0, 30 * i, 0);

        GameObject button = Instantiate(attackButton, popupArea.transform);
        button.transform.localPosition = baseAttackPosition + popUpOffset;
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
        moveBtn.transform.localPosition = baseAttackPosition + buttonOffset;
        moveBtn.GetComponent<MoveButton>().SetUpMoveButton(character);
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

    public void InstantiateHealthBar(Character unit) {
        Slider newSlider = Instantiate(healthBar, unit.transform.position, fixedCanvas.transform.rotation, fixedCanvas.transform);
        healthBars.Add(newSlider);
        unit.gameObject.AddComponent<HealthBar>().unit = unit;
        unit.gameObject.GetComponent<HealthBar>().slider = newSlider;
    }


    // Unit Assignment
    public void SetUnit(Character unit) {
        gridManager.SetSelectedUnit(unit);
    }

    //Set Canvas'
    private void GameStateChanged(object sender, GameStates state) {
        if (!isPaused) {
            currentState = state;

            switch (state) {
                case GameStates.placementPhase:
                    SetPrepCanvas( true );
                    SetBattleCanvas( false );
                    SetFixedCanvas( false );
                    SetPauseCanvas( false );
                    break;

                case GameStates.playerTurn:
                    SetPrepCanvas( false );
                    SetBattleCanvas( true );
                    SetFixedCanvas( true );
                    SetPauseCanvas( false );
                    break;

                case GameStates.enemyTurn:
                    SetPrepCanvas( false );
                    SetBattleCanvas( false );
                    SetFixedCanvas( true );
                    SetPauseCanvas( false );
                    break;

                case GameStates.winState:
                    SetPrepCanvas(false);
                    SetBattleCanvas(false);
                    SetFixedCanvas(false);
                    SetPauseCanvas(false);
                    SetWinCanvas(true);
                    break;

                case GameStates.loseState:
                    SetPrepCanvas(false);
                    SetBattleCanvas(false);
                    SetFixedCanvas(false);
                    SetPauseCanvas(false);
                    SetLoseCanvas(true);
                    break;

                case GameStates.paused:
                    isPaused = true;
                    SetPrepCanvas( false );
                    SetBattleCanvas( false );
                    SetFixedCanvas( false );
                    SetPauseCanvas( true );
                    break;
            }
        } else {
            resumeState = state;
        }
    }

    public void SetWinCanvas(bool enabled)
    {
        if (!winCanvas.gameObject.active)
        {
            winCanvas.gameObject.SetActive(true);
        }
        winCanvas.enabled = enabled;
    }

    public void SetLoseCanvas(bool enabled)
    {
        if (!loseCanvas.gameObject.active)
        {
            loseCanvas.gameObject.SetActive(true);
        }
        loseCanvas.enabled = enabled;
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

    public void SetPauseCanvas(bool enabled) {
        if (!pauseCanvas.gameObject.active) {
            pauseCanvas.gameObject.SetActive(true);
        }
        pauseCanvas.enabled = enabled;
    }
}
