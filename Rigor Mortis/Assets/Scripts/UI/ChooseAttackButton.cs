using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ChooseAttackButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //public AttackManager attackManager;
    public static EventHandler<CharacterAttack> attackChosen;
    public static EventHandler pointerExit;
    public Button targetInRangeButton;
    public GridManager gridManager;
    public Character character;
    public Attack attack;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseAttack()
    {
        attackChosen?.Invoke(this, new CharacterAttack(character, attack));
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        gridManager.ClearMap();
        gridManager.ColourTiles(character.pathfinder.GetTilesInRange(character.floor, attack.Range, true), false);
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
