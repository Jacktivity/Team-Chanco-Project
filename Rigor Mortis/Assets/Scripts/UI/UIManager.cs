using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]Pathfinder pathFinder;
    [SerializeField]GridManager gridManager;
    [SerializeField]PlayerManager playerManager;

    [SerializeField]Canvas battleCanvas, prepCanvas, fixedCanvas, pauseCanvas, winCanvas, loseCanvas;

    private int turnNumber;

    [SerializeField]GameObject attackButton, targetCharacterButton, popupArea, moveButton, cancelActionBtn, waitButton, floatingText;
    [SerializeField]private Vector3 targetCharacterOffset;
    [SerializeField]private Vector3 baseAttackPosition, originalBaseAttackPosition;
    [SerializeField]private GameObject attackPanel;
    public List<GameObject> popUpButtons;
    public List<GameObject> activePopUpButtons;

    private Vector2 attackPanelOriginalScale, attackPanelEdges;
    [SerializeField]int minButtons = 3;
    [SerializeField]int maxButtons = 20;
    bool attackPanalShrinkButtons;

    [SerializeField]GameObject marker;
    List<GameObject> markers;

    [SerializeField]Slider healthBar;
    List<Slider> healthBars;

    //[SerializeField] Slider APBar;
    //List<Slider> APBars;

    public BlockScript[] blocksInRange;
    public static EventHandler<GameStates> gameStateChange;

    [SerializeField]GameStates currentState;
    [SerializeField]GameStates resumeState;

    public bool isPaused;
    public bool gameOver;

    float buttonSpace;
    private Vector3 buttonSpacing;


    [SerializeField] Text turnDisplay;
    public Text attackText, hitText, hitStatText, rangeText, rangeStatText, magicText, magicStatText, damageText, damageStatText;
    public Text scorePointsText;
    public Text placementText;

    [SerializeField]GameObject apRightArrow, apLeftArrow;

    public static EventHandler<SpawnFloatingTextEventArgs> createFloatingText;

    public enum GameStates
    {
        placementPhase, playerTurn, enemyTurn, paused, winState, loseState
    }

    // Start is called before the first frame update
    void Start()
    {
        healthBars = new List<Slider>();
        //APBars = new List<Slider>();
        markers = new List<GameObject>();
        popUpButtons = new List<GameObject>();
        activePopUpButtons = new List<GameObject>();

        gameStateChange += GameStateChanged;
        gameStateChange?.Invoke(this, GameStates.placementPhase);

        ChooseAttackButton.attackChosen += DisplayTargets;
        Character.attackEvent += ClearAttackUI;

        baseAttackPosition = popupArea.transform.localPosition;
        originalBaseAttackPosition = baseAttackPosition;

        attackPanelOriginalScale = attackPanel.GetComponent<RectTransform>().sizeDelta;
        attackPanelEdges = new Vector2(350, attackPanelOriginalScale.y);
        attackPanalShrinkButtons = false;
        ButtonSpaceUpdate(attackPanalShrinkButtons);

        createFloatingText += CreateFloatingText;
    }
    
    private void OnDestroy()
    {
        gameStateChange -= GameStateChanged;
        var atkBtnDel = ChooseAttackButton.attackChosen.GetInvocationList();
        foreach (var del in atkBtnDel)
        {
            ChooseAttackButton.attackChosen -= (del as EventHandler<ChooseAttackButton.CharacterAttack>);
        }
    }

    private void CreateFloatingText(object sender, SpawnFloatingTextEventArgs e)
    {
        var floatingTextInstance = Instantiate(floatingText, e.character.transform.position, fixedCanvas.transform.rotation, fixedCanvas.transform).GetComponent<FloatingText>();
        floatingTextInstance.SetUp(e.character, e.message, e.textColour);
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

    void ButtonSpaceUpdate(bool isSmallPanel)
    {
        if (!isSmallPanel) {
            buttonSpace = 60;
            baseAttackPosition = originalBaseAttackPosition;
        } else {
            buttonSpace = 30;
            baseAttackPosition = new Vector3(originalBaseAttackPosition.x + buttonSpace, originalBaseAttackPosition.y - (buttonSpace/2), originalBaseAttackPosition.z);
        }

        buttonSpacing = new Vector3(buttonSpace, 0, 0);
    }

    private void DisplayTargets(object sender, ChooseAttackButton.CharacterAttack e)
    {
        DeleteCurrentPopupButtons();
        attackPanalShrinkButtons = true;
        ButtonSpaceUpdate(attackPanalShrinkButtons);

        var targetsInRange = e.attacker.pathfinder.GetAttackTiles(e.attacker, e.attackChosen)
            .Where(b => b.Occupied ? b.occupier.tag == "Enemy" || b.occupier.tag == "Breakable_Terrain" : false)
            .Select(t => t.occupier.GetComponent<Character>());

        if (targetsInRange.Count() == 0)
        {
            //Say no people to hit
            //var btn = Instantiate(targetCharacterButton, attackPanel.transform);
            //popUpButtons.Add(btn);
            //btn.transform.position = baseAttackPosition;
            //btn.GetComponentInChildren<Text>().text = "No Targets";
            CreateCancelButton(e.attacker, true);
        }
        else
        {
            CreateCancelButton(e.attacker, false);

            for (int i = 0; i < targetsInRange.Count(); i++)
            {
                var button = Instantiate(targetCharacterButton, attackPanel.transform);
                var moveOffset = buttonSpacing * (i + 1);
                popUpButtons.Add(button);
                button.transform.localPosition = baseAttackPosition + moveOffset;
                //button.GetComponentInChildren<Text>().text = targetsInRange.ElementAt(i).name;
                var enemySelect = button.GetComponent<EnemySelectButton>();
                enemySelect.AssignData(e.attacker, targetsInRange.ElementAt(i));
                enemySelect.attackText = attackText;
                enemySelect.previousText = attackText.text;
                enemySelect.uiManager = this;
                EnableAPText(e.attackChosen);
            }
        }

        if (popUpButtons.Count() > minButtons)
        {
            ExpandPanel();
        } else {
            ResetPanelSize();
            ShrinkButtons();
            ResetArrows();
        }
    }

    private void EnableAPText(Attack atk)
    {
        hitText.enabled = true;
        hitStatText.enabled = true;
        hitStatText.text = atk.Accuracy + "%";
        rangeText.enabled = true;
        rangeStatText.enabled = true;
        rangeStatText.text = atk.Range + "";
        magicText.enabled = true;
        magicStatText.enabled = true;
        /*if (atk.MagicalDamage != null) {
            magicStatText.text = atk.MagicalDamage.ToString() + " MP";
        } else {*/
            magicStatText.text = "0 MP";
        //}
        damageText.enabled = true;
        damageStatText.enabled = true;
        damageStatText.text = atk.AverageDamage + "";
    }

    public void DisableAPText()
    {
        hitText.enabled = false;
        hitStatText.enabled = false;
        rangeText.enabled = false;
        rangeStatText.enabled = false;
        magicText.enabled = false;
        magicStatText.enabled = false;
        damageText.enabled = false;
        damageStatText.enabled = false;
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

        popUpButtons.Clear();
        popUpButtons = new List<GameObject>();

        activePopUpButtons.Clear();
        activePopUpButtons = new List<GameObject>();

        DisableAPText();
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
        attackText.text = "";
        gridManager.CycleTurns();
        gridManager.nextUnit();
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
                    attackText.text = "";
                }
            }
        }
        gridManager.CycleTurns();
        gameStateChange?.Invoke(this, GameStates.enemyTurn);
    }

    public void CreateActionButtons(IEnumerable<Attack> _attacks, Character character)
    {
        DeleteCurrentPopupButtons();
        DisableAPText();
        attackPanalShrinkButtons = false;
        ButtonSpaceUpdate(attackPanalShrinkButtons);
        attackText.text = character.name;

        if (character.CanAttack) {
            var moveOffset = buttonSpacing * (_attacks.Count() + 1);
            MakeMoveButton( new Vector3( 0, 0, 0 ), character );

            for (int i = 0; i < _attacks.Count(); i++) {
                CreateAttackButton( _attacks, character, i );
            }

            moveOffset = new Vector3( buttonSpace * (_attacks.Count() + 1), 0, 0 );
            MakeWaitButton( moveOffset, character );
            if (popUpButtons.Count() > minButtons) {
                ExpandPanel();
            } else {
                ResetPanelSize();
                ResetArrows();
            }
        } else if (character.CanMove) {
            MakeMoveButton( new Vector3( (buttonSpace / 2), 0, 0 ), character );
            MakeWaitButton( new Vector3( buttonSpace + ( buttonSpace / 2), 0, 0 ), character );
        }
    }

    void ShrinkButtons()
    {
        foreach (GameObject button in popUpButtons)
        {
            button.GetComponent<RectTransform>().sizeDelta = new Vector2(button.GetComponent<RectTransform>().rect.width / 2, button.GetComponent<RectTransform>().rect.height / 2);
        }
    }

    public void ResetPanelSize()
    {
        attackPanel.GetComponent<RectTransform>().sizeDelta = attackPanelOriginalScale;
    }

    private void ExpandPanel() {
        int amountOver = popUpButtons.Count - minButtons;

        if (attackPanalShrinkButtons) {
            ShrinkButtons();
        }

        attackPanel.GetComponent<RectTransform>().sizeDelta = attackPanelEdges + new Vector2(amountOver * buttonSpace, 0);
        if (attackPanel.GetComponent<RectTransform>().sizeDelta.x < 350 && attackPanalShrinkButtons)
            attackPanel.GetComponent<RectTransform>().sizeDelta = attackPanelOriginalScale;

        foreach (GameObject button in popUpButtons)
        {
            button.transform.position = new Vector3(button.transform.position.x - (((buttonSpace / 2) * amountOver) * battleCanvas.scaleFactor), button.transform.position.y, button.transform.position.z);
        }

        ResetArrows();
    }

    void ResetArrows()
    {
        apLeftArrow.transform.localPosition = new Vector3(-attackPanel.GetComponent<RectTransform>().rect.width * 0.45f, apLeftArrow.transform.localPosition.y, apLeftArrow.transform.localPosition.z);
        apRightArrow.transform.localPosition = new Vector3(attackPanel.GetComponent<RectTransform>().rect.width * 0.45f, apRightArrow.transform.localPosition.y, apRightArrow.transform.localPosition.z);
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
            if (i < popUpButtons.Count() && i != 0 || activePopUpButtons.Count < maxButtons) {
                i -= maxButtons;
                for (int k = 0; k < maxButtons; k++) {
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
            if (i < popUpButtons.Count() && i != 0 || activePopUpButtons.Count < maxButtons) {
                i -= maxButtons+2;
                for (int k = 0; k < maxButtons; k++) {
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
        button.transform.localPosition = baseAttackPosition + popUpOffset;
        popUpButtons.Add(button);

        button.GetComponent<ChooseAttackButton>().character = character;
        button.GetComponent<ChooseAttackButton>().gridManager = gridManager;
        button.GetComponent<ChooseAttackButton>().attackText = attackText;
        button.GetComponent<ChooseAttackButton>().previousText = attackText.text;
        button.GetComponent<ChooseAttackButton>().attack = _attacks.ElementAt(i);
        //button.GetComponentInChildren<Text>().text = _attacks.ElementAt(i).Name;
    }

    private void MakeMoveButton(Vector3 buttonOffset, Character character)
    {
        var moveBtn = Instantiate(moveButton, attackPanel.transform );
        popUpButtons.Add(moveBtn);
        moveBtn.transform.localPosition = baseAttackPosition + buttonOffset;
        moveBtn.GetComponent<MoveButton>().SetUpMoveButton(character);
        moveBtn.GetComponent<MoveButton>().attackText = attackText;
        moveBtn.GetComponent<MoveButton>().previousText = attackText.text;
    }

    private void MakeWaitButton(Vector3 buttonOffset, Character character)
    {
        var waitBtn = Instantiate(waitButton, attackPanel.transform);
        popUpButtons.Add(waitBtn);
        waitBtn.transform.localPosition = baseAttackPosition + buttonOffset;
        waitBtn.GetComponent<Button>().onClick.AddListener(delegate { Wait(character); });
        waitBtn.GetComponent<WaitButton>().attackText = attackText;
        waitBtn.GetComponent<WaitButton>().previousText = attackText.text;
        waitBtn.GetComponent<WaitButton>().gridManager = gridManager;
        waitBtn.GetComponent<WaitButton>().character = character;
        waitBtn.GetComponent<WaitButton>().uiManager = this;
    }

    public void CreateCancelButton(Character unit, bool onlyButton)
    {
        var cancel = Instantiate(cancelActionBtn, attackPanel.transform);
        popUpButtons.Add(cancel);
        cancel.transform.localPosition = baseAttackPosition;
        cancel.GetComponent<CancelAction>().SetActions(unit/*, popUpButtons.Select(b => b.GetComponent<MoveButton>()).Where(b => b != null)*/);
        cancel.GetComponent<CancelAction>().attackText = attackText;
        cancel.GetComponent<CancelAction>().previousText = attackText.text;
        cancel.GetComponent<CancelAction>().uiManager = this;

        if (onlyButton)
            cancel.transform.localPosition = new Vector3(0, baseAttackPosition.y, baseAttackPosition.z);
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
        unit.gameObject.AddComponent<UnitSliders>().unit = unit;
        unit.gameObject.GetComponent<UnitSliders>().healthSlider = newSlider.GetComponent<Slider>();
        unit.gameObject.GetComponent<UnitSliders>().manaSlider = newSlider.GetComponentsInChildren<Slider>()[1];

        //if (unit.tag == "Player")
        //{
        //    Slider apSlider = Instantiate(APBar, unit.transform.position, fixedCanvas.transform.rotation, fixedCanvas.transform);
        //    APBars.Add(apSlider);
        //    unit.gameObject.AddComponent<ActionPointBar>().unit = unit;
        //    unit.gameObject.GetComponent<ActionPointBar>().slider = apSlider;
        //}
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
        gridManager.nextUnit();
    }

    public void MainMenuReturn() {
        popUpButtons.Clear();

        MainMenu.mainMenuStateChange?.Invoke(this, MainMenu.MainMenuStates.mainCanvas);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
        SceneManager.UnloadSceneAsync(1, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
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
