using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ChooseAttackButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //public AttackManager attackManager;
    private UIManager uiManager;
    public static EventHandler<CharacterAttack> attackChosen;
    public static EventHandler pointerExit;
    public GridManager gridManager;
    public Character character;
    public Attack attack;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    public void ChooseAttack()
    {
        uiManager.DeleteCurrentPopupButtons();
        attackChosen?.Invoke(this, new CharacterAttack(character, attack));        
    }
       
    public void OnPointerEnter(PointerEventData eventData)
    {
        gridManager.ClearMap();
        var tiles = character.pathfinder.GetTilesInRange(character.floor, attack.Range, true);
        gridManager.ColourTiles(tiles, false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gridManager.ClearMap();
        pointerExit?.Invoke(this, new EventArgs());        
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
}
