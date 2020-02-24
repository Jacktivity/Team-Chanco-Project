using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class ChooseAttackButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //public AttackManager attackManager;
    private UIManager uiManager;
    public Text attackText;
    public static EventHandler<CharacterAttack> attackChosen;
    public static EventHandler pointerExit;
    public GridManager gridManager;
    public Character character;
    public Attack attack;
    private Button button;
    private bool disabled;
    public String previousText;

    public Sprite staffWhackSprite, rustySwordSprite, spectralSwordSprite, whackSprite, teslaStabSprite, teslaZapSprite, zapSprite, headbuttSprite, fireboltSprite, caplockRifleSprite, axeSprite, spearSprite;
    public Sprite staffWhackSpriteHL, rustySwordSpriteHL, spectralSwordSpriteHL, whackSpriteHL, teslaStabSpriteHL, teslaZapSpriteHL, zapSpriteHL, headbuttSpriteHL, fireboltSpriteHL, caplockRifleSpriteHL, axeSpriteHL, spearSpriteHL;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        button = GetComponent<Button>();
        attackText.text = previousText;

        var targetsInRange = character.pathfinder.GetAttackTiles(character, attack)
            .Where(b => b.Occupied ? b.occupier.tag == "Enemy" || b.occupier.tag == "Breakable_Terrain" : false)
            .Select(t => t.occupier.GetComponent<Character>());

        if(targetsInRange.Count() == 0)
        {
            button.interactable = false;
            disabled = true;
        } else
        {
            disabled = false;
        }

        SetAttackSprite(attack);
    }

    private void OnDestroy()
    {
        var attackChosenDelegates = attackChosen.GetInvocationList();
        foreach (var del in attackChosenDelegates)
        {
            attackChosen -= (del as EventHandler<CharacterAttack>);
        }
    }

    public void ChooseAttack()
    {
        uiManager.DeleteCurrentPopupButtons();
        attackChosen?.Invoke(this, new CharacterAttack(character, attack));
        attackText.text = "";
    }
       
    public void OnPointerEnter(PointerEventData eventData)
    {
        gridManager.ClearMap();
        if (disabled) {
            attackText.text = attack.Name + "\n" + "No Targets";
        } else {
            attackText.text = attack.Name;
        }

        var tiles = character.pathfinder.GetAttackTiles(character, attack);
        var allTiles = character.pathfinder.GetTilesInRange(character.floor, attack.Range, true, true, true);
        gridManager.ColourTiles(allTiles, gridManager.MissTile);
        gridManager.ColourTiles(tiles, gridManager.AttackTile);
        attackText.text = attack.Name;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gridManager.ClearMap();
        pointerExit?.Invoke(this, new EventArgs());
        attackText.text = previousText;
    }    

    public class CharacterAttack
    {
        public Character attacker;
        public Attack attackChosen;

        public CharacterAttack(Character character, Attack attack)
        {
            attacker = character;
            attackChosen = attack;
        }
    }

    public void SetAttackSprite(Attack atk)
    {
        switch (atk.AttackID) {
            case 0:
                ActionPanelButtonSpriteSwitch(staffWhackSprite, staffWhackSpriteHL);
                break;
            case 1:
                ActionPanelButtonSpriteSwitch(rustySwordSprite, rustySwordSpriteHL);
                break;
            case 2:
                ActionPanelButtonSpriteSwitch(spectralSwordSprite, spectralSwordSpriteHL);
                break;
            case 3:
                ActionPanelButtonSpriteSwitch(whackSprite, whackSpriteHL);
                break;
            case 4:
                ActionPanelButtonSpriteSwitch(teslaStabSprite, teslaStabSpriteHL);
                break;
            case 5:
                ActionPanelButtonSpriteSwitch(teslaZapSprite, teslaZapSpriteHL);
                break;
            case 6:
                ActionPanelButtonSpriteSwitch(zapSprite, zapSpriteHL);
                break;
            case 7:
                ActionPanelButtonSpriteSwitch(headbuttSprite, headbuttSpriteHL);
                break;
            case 8:
                ActionPanelButtonSpriteSwitch(fireboltSprite, fireboltSpriteHL);
                break;
            case 9:
                ActionPanelButtonSpriteSwitch(caplockRifleSprite, caplockRifleSpriteHL);
                break;
            case 10:
                ActionPanelButtonSpriteSwitch(axeSprite, axeSpriteHL);
                break;
            case 11:
                ActionPanelButtonSpriteSwitch(spearSprite, spearSpriteHL);
                break;
            default:
                ActionPanelButtonSpriteSwitch(axeSprite, axeSpriteHL);
                Debug.LogError("Missing switch case for attack ID " + atk.AttackID);
                break;
        }
    }

    public void ActionPanelButtonSpriteSwitch( Sprite sprite, Sprite spriteHL ) {
        SpriteState st = new SpriteState();
        st = button.spriteState;

        if (sprite == null || spriteHL == null) {
            sprite = axeSprite;
            spriteHL = axeSpriteHL;

            Debug.Log("Missing sprite for attack ID " + attack.AttackID + " (" + attack.Name + ")");
        }

        button.GetComponent<Image>().sprite = sprite;
        st.pressedSprite = spriteHL;
        st.highlightedSprite = spriteHL;
        button.spriteState = st;
    }
}
