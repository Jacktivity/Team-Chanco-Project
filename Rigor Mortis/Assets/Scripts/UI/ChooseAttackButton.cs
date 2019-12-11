using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChooseAttackButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //public AttackManager attackManager;
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

    public void ChooseAttack() => character.selectedAttack = attack;//uiManager.Attack();


    public void OnPointerEnter(PointerEventData eventData)
    {
        //gridManager.ClearMap();
        gridManager.ColourTiles(character.pathfinder.GetTilesInRange(character.floor, attack.Range, true), false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gridManager.ClearMap();
    }
}
