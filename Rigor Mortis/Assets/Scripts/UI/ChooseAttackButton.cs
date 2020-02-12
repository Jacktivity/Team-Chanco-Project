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

        var targetsInRange = character.pathfinder.GetTilesInRange(character.floor, attack.Range, true, true, true)
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
        switch (atk.Name) {
            case "Staff Whack":
                ActionPanelButtonSpriteSwitch(staffWhackSprite, staffWhackSpriteHL);
                break;
            case "Rusty Sword":
                ActionPanelButtonSpriteSwitch(rustySwordSprite, rustySwordSpriteHL);
                break;
            case "Spectral Sword":
                ActionPanelButtonSpriteSwitch(spectralSwordSprite, spectralSwordSpriteHL);
                break;
            case "Whack":
                ActionPanelButtonSpriteSwitch(whackSprite, whackSpriteHL);
                break;
            case "Tesla Stab":
                ActionPanelButtonSpriteSwitch(teslaStabSprite, teslaStabSpriteHL);
                break;
            case "Tesla Zap":
                ActionPanelButtonSpriteSwitch(teslaZapSprite, teslaZapSpriteHL);
                break;
            case "Zap":
                ActionPanelButtonSpriteSwitch(zapSprite, zapSpriteHL);
                break;
            case "Headbutt":
                ActionPanelButtonSpriteSwitch(headbuttSprite, headbuttSpriteHL);
                break;
            case "Firebolt":
                ActionPanelButtonSpriteSwitch(fireboltSprite, fireboltSpriteHL);
                break;
            case "Caplock Rifle":
                ActionPanelButtonSpriteSwitch(caplockRifleSprite, caplockRifleSpriteHL);
                break;
            case "Axe":
                ActionPanelButtonSpriteSwitch(axeSprite, axeSpriteHL);
                break;
            case "Spear":
                ActionPanelButtonSpriteSwitch(spearSprite, spearSpriteHL);
                break;
            default:
                ActionPanelButtonSpriteSwitch(axeSprite, axeSpriteHL);
                break;
        }
    }

    public void ActionPanelButtonSpriteSwitch( Sprite sprite, Sprite spriteHL ) {
        SpriteState st = new SpriteState();

        if(sprite == null) {
            button.GetComponent<Image>().sprite = axeSprite;
            st.pressedSprite = axeSpriteHL;
            st.highlightedSprite = axeSpriteHL;
        }

        button.GetComponent<Image>().sprite = sprite;
        st.pressedSprite = spriteHL;
        st.highlightedSprite = spriteHL;
    }
}
