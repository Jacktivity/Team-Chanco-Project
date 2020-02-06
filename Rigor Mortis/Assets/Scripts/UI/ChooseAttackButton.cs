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

    public Sprite moveSprite, swordSprite, zapSprite, axeSprite;

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
        var tiles = character.pathfinder.GetTilesInRange(character.floor, attack.Range, true, true, true);
        gridManager.ColourTiles(tiles, false);
        if (disabled) {
            attackText.text = attack.Name + "\n" + "No Targets";
        } else {
            attackText.text = attack.Name;
        }
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
    public enum Type
    {
        Move, RustySword, SpectralSword, Zap
    }

    public void ActionPanelButtonSpriteSwitch( Sprite sprite ) {
        button.GetComponent<Image>().sprite = sprite;
    }
}
