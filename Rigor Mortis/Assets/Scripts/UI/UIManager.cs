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
    [SerializeField]PlayerManager playerManager;

    [SerializeField]Canvas battleCanvas, prepCanvas, fixedCanvas, pauseCanvas, winCanvas, loseCanvas;

    private int turnNumber;

    [SerializeField]GameObject attackButton, targetCharacterButton, popupArea, moveButton, cancelActionBtn, waitButton;
    [SerializeField]private Vector3 targetCharacterOffset;
    [SerializeField]private Vector3 baseAttackPosition;
    [SerializeField]private GameObject attackPanel;
    [SerializeField]private Button actionPanelButton;
    public List<GameObject> popUpButtons;
    public List<GameObject> activePopUpButtons;


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

    float buttonSpace;
    private Vector3 buttonSpacing;

    [SerializeField] Text turnDisplay;
    public Text attackText;
    public Text scorePointsText;
    public Text placementText;

    private Score score;

    [SerializeField]GameObject apRightArrow, apLeftArrow;

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
        activePopUpButtons = new List<GameObject>();

        score = FindObjectOfType<Score>();

        gameStateChange += GameStateChanged;
        gameStateChange?.Invoke(this, GameStates.placementPhase);

        ChooseAttackButton.attackChosen += DisplayTargets;
        Character.attackEvent += ClearAttackUI;

        baseAttackPosition = popupArea.transform.position;

        buttonSpace = 60;
        buttonSpacing = new Vector3(buttonSpace, 0, 0);
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

        var targetsInRange = e.attacker.pathfinder.GetTilesInRange(e.attacker.floor, e.attackChosen.Range, true, true, true)
            .Where(b => b.Occupied ? b.occupier.tag == "Enemy" || b.occupier.tag == "Breakable_Terrain" : false)
            .Select(t => t.occupier.GetComponent<Character>());

        //CreateCancelButton(e.attacker);

        if (targetsInRange.Count() == 0)
        {
            //Say no people to hit
            //var btn = Instantiate(targetCharacterButton, attackPanel.transform);
            //popUpButtons.Add(btn);
            //btn.transform.position = baseAttackPosition;
            //btn.GetComponentInChildren<Text>().text = "No Targets";
            CreateCancelButton(e.attacker);
        }
        else
        {
            for (int i = 0; i < targetsInRange.Count(); i++)
            {
                var button = Instantiate(targetCharacterButton, attackPanel.transform);
                var moveOffset = buttonSpacing * i;
                popUpButtons.Add(button);
                button.transform.position = baseAttackPosition + moveOffset;
                //button.GetComponentInChildren<Text>().text = targetsInRange.ElementAt(i).name;
                var enemySelect = button.GetComponent<EnemySelectButton>();
                enemySelect.AssignData(e.attacker, targetsInRange.ElementAt(i));
                enemySelect.attackText = attackText;
            }
        }

        if (popUpButtons.Count() > 3)
        {
            LimitAPButtons();
            apRightArrow.SetActive(true);
        }
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

        apRightArrow.SetActive(false);
        apLeftArrow.SetActive(false);

        popUpButtons.Clear();
        popUpButtons = new List<GameObject>();

        activePopUpButtons.Clear();
        activePopUpButtons = new List<GameObject>();        
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

    public void CreateActionButtons(IEnumerable<Attack> _attacks, Character character)
    {
        DeleteCurrentPopupButtons();

        if (character.CanAttack) {
            var moveOffset = buttonSpacing * (_attacks.Count() + 1);
            MakeMoveButton( new Vector3( 0, 0, 0 ), character );

            for (int i = 0; i < _attacks.Count(); i++) {
                CreateAttackButton( _attacks, character, i );
            }

            moveOffset = new Vector3( buttonSpace * (_attacks.Count() + 1), 0, 0 );
            MakeWaitButton( moveOffset, character );
            if (popUpButtons.Count() > 3) {
                LimitAPButtons();
                apRightArrow.SetActive(true);
            }
        } else if (character.CanMove) {
            MakeMoveButton( new Vector3( 0, 0, 0 ), character );
            MakeWaitButton( new Vector3( buttonSpace, 0, 0 ), character );
        }
    }

    private void LimitAPButtons() {
        for(int i = 3; i < popUpButtons.Count(); i++) {
            GameObject button = popUpButtons[i];
            button.SetActive(false);
        }

        foreach(GameObject button in popUpButtons) {
            if (button.activeSelf) {
                activePopUpButtons.Add(button);
            }
        }
    }

    public void ResetAPButtons(bool increment) {
        foreach(GameObject button in popUpButtons) {
            if (increment) {
                button.transform.position = button.transform.position + new Vector3( -buttonSpace, 0, 0 );
            } else {
                button.transform.position = button.transform.position + new Vector3(buttonSpace, 0, 0 );
            }
            if (!activePopUpButtons.Contains(button)) {
                button.SetActive(false);
            } else {
                button.SetActive(true);
            }
        }
    }

    public void IncrementAPButtons() {
        if (popUpButtons.Count() > 0 && activePopUpButtons[activePopUpButtons.Count-1] != popUpButtons[popUpButtons.Count-1]) {
            int i = 0;
            for(int j = 1; j < popUpButtons.Count() + 1; j++) {
                if (activePopUpButtons.Contains( popUpButtons[j - 1] )) {
                    activePopUpButtons.Remove( popUpButtons[j - 1] );
                    i = j;
                }
            }
            if (i < popUpButtons.Count() && i != 0 || activePopUpButtons.Count < 3 ) {
                i -= 3;
                for (int k = 0; k < 3; k++) {
                    i++;
                    if (i < popUpButtons.Count) {
                        activePopUpButtons.Add( popUpButtons[i] );
                    }
                }
                ResetAPButtons(true);
            }
        }

        if(activePopUpButtons[0] != popUpButtons[0]) {
            apLeftArrow.SetActive( true );
        }
        if(activePopUpButtons[activePopUpButtons.Count - 1] == popUpButtons[popUpButtons.Count - 1]) {
            apRightArrow.SetActive(false);
        }
    }

    public void DecrementAPButtons() {
        if (popUpButtons.Count() > 0 && activePopUpButtons[0] != popUpButtons[0]) {
            int i = 0;
            for (int j = 1; j < popUpButtons.Count() + 1; j++) {
                if (activePopUpButtons.Contains( popUpButtons[j - 1] )) {
                    activePopUpButtons.Remove( popUpButtons[j - 1] );
                    i = j;
                }
            }
            if (i < popUpButtons.Count() && i != 0 || activePopUpButtons.Count < 3) {
                i -= 5;
                for (int k = 0; k < 3; k++) {
                    i++;
                    if (i < popUpButtons.Count) {
                        activePopUpButtons.Add( popUpButtons[i] );
                    }
                }
                ResetAPButtons( false );
            }
        }

        if(activePopUpButtons[0] == popUpButtons[0]) {
            apLeftArrow.SetActive( false );
        }
        if (activePopUpButtons[activePopUpButtons.Count - 1] != popUpButtons[popUpButtons.Count - 1]) {
            apRightArrow.SetActive( true );
        }
    }

    private void CreateAttackButton(IEnumerable<Attack> _attacks, Character character, int i)
    {
        Vector3 popUpOffset = buttonSpacing * i;
        if(character.CanMove) {
            popUpOffset = buttonSpacing * (i + 1);
        }

        GameObject button = Instantiate(attackButton, attackPanel.transform);
        button.transform.position = baseAttackPosition + popUpOffset;
        popUpButtons.Add(button);

        button.GetComponent<ChooseAttackButton>().character = character;
        button.GetComponent<ChooseAttackButton>().gridManager = gridManager;
        button.GetComponent<ChooseAttackButton>().attackText = attackText;
        button.GetComponent<ChooseAttackButton>().attack = _attacks.ElementAt(i);
        //button.GetComponentInChildren<Text>().text = _attacks.ElementAt(i).Name;
    }

    private void MakeMoveButton(Vector3 buttonOffset, Character character)
    {
        var moveBtn = Instantiate(moveButton, attackPanel.transform );
        popUpButtons.Add(moveBtn);
        moveBtn.transform.position = baseAttackPosition + buttonOffset;
        moveBtn.GetComponent<MoveButton>().SetUpMoveButton(character);
        moveBtn.GetComponent<MoveButton>().attackText = attackText;
    }

    private void MakeWaitButton(Vector3 buttonOffset, Character character)
    {
        var waitBtn = Instantiate(waitButton, attackPanel.transform);
        popUpButtons.Add(waitBtn);
        waitBtn.transform.position = baseAttackPosition + buttonOffset;
        waitBtn.GetComponent<Button>().onClick.AddListener(delegate { Wait(character); });
        waitBtn.GetComponent<WaitButton>().attackText = attackText;
        waitBtn.GetComponent<WaitButton>().gridManager = gridManager;
        waitBtn.GetComponent<WaitButton>().character = character;
        waitBtn.GetComponent<WaitButton>().uiManager = this;
    }

    public void CreateCancelButton(Character unit)
    {
        var cancel = Instantiate(cancelActionBtn, attackPanel.transform);
        popUpButtons.Add(cancel);
        cancel.transform.position = baseAttackPosition;
        cancel.GetComponent<CancelAction>().SetActions(unit/*, popUpButtons.Select(b => b.GetComponent<MoveButton>()).Where(b => b != null)*/);
        cancel.GetComponent<CancelAction>().attackText = attackText;
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
        if (!winCanvas.gameObject.activeInHierarchy && enabled) {
            winCanvas.gameObject.SetActive(true);
        }

        if (winCanvas.enabled != enabled) {
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
