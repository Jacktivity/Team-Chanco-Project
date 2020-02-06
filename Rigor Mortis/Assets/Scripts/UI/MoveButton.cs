using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Character character;
    private BlockScript[] walkTiles, sprintTiles;
    public static EventHandler pointerExit;
    private GridManager gridManager;
    private UIManager uiManager;
    public Text attackText;
    private bool hideGridMoveOnExit = true;
    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void SetUpMoveButton(Character unit)
    {
        character = unit;
        if(unit.MaxAP)
        {
            sprintTiles = unit.pathfinder.GetTilesInRange(unit.floor, unit.movemenSprint + unit.movementSpeed, unit.isFlying, unit.isFlying, unit.isFlying);
            walkTiles = unit.pathfinder.GetTilesInRange(unit.floor, unit.movementSpeed, unit.isFlying, unit.isFlying, unit.isFlying);
        }
        else if(unit.HasAttacked)
        {
            sprintTiles = new BlockScript[0];
            walkTiles = unit.pathfinder.GetTilesInRange(unit.floor, unit.movementSpeed, unit.isFlying, unit.isFlying, unit.isFlying);
        }
        else
        {
            sprintTiles = new BlockScript[0];
            walkTiles = unit.pathfinder.GetTilesInRange(unit.floor, unit.movemenSprint, unit.isFlying, unit.isFlying, unit.isFlying);
        }


    }

    public void ButtonClicked()
    {
        hideGridMoveOnExit = false;
        attackText.text = "";

        uiManager.DeleteCurrentPopupButtons();
        //uiManager.CreateCancelButton(character);

        FindObjectOfType<PlayerCharacterMover>().SetMovement(character, walkTiles, sprintTiles);
    }

    //public void MoveCharacterCheck(object sender, BlockScript e)
    //{
    //    if (canMove == false)
    //        RemoveMoveCheck();
    //    else if (sprintTiles.Contains(e) || walkTiles.Contains(e))
    //    {
    //        uiManager.DeleteCurrentPopupButtons();
    //        RemoveMoveCheck();
    //        character.MoveUnit(character.pathfinder.GetPath(character.floor, (block) => block == e, character.isFlying == false));
    //        gridManager.ClearMap();
    //        gridManager.ClearMap();
    //    }        
    //}

    //public void RemoveMoveCheck()
    //{
    //    BlockScript.blockClicked -= MoveCharacterCheck;
    //}    


    public void OnPointerEnter(PointerEventData eventData)
    {
        //Update with PLayerCharacterMover link

        gridManager.ColourTiles(sprintTiles, false);
        gridManager.ColourTiles(walkTiles, true);
        attackText.text = "Move";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerExit?.Invoke(this, new EventArgs());
        attackText.text = "";
    }
}
