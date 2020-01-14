using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Character character;
    private BlockScript[] walkTiles, sprintTiles;
    private GridManager gridManager;
    private UIManager uiManager;
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
            sprintTiles = unit.pathfinder.GetTilesInRange(unit.floor, unit.movemenSprint + unit.movementSpeed, unit.isFlying, false);
            walkTiles = unit.pathfinder.GetTilesInRange(unit.floor, unit.movementSpeed, unit.isFlying, false);
        }
        else if(unit.HasAttacked)
        {
            sprintTiles = new BlockScript[0];
            walkTiles = unit.pathfinder.GetTilesInRange(unit.floor, unit.movementSpeed, unit.isFlying, false);
        }
        else
        {
            sprintTiles = new BlockScript[0];
            walkTiles = unit.pathfinder.GetTilesInRange(unit.floor, unit.movemenSprint, unit.isFlying, false);
        }


    }

    public void ButtonClicked()
    {
        hideGridMoveOnExit = false;

        uiManager.DeleteCurrentPopupButtons();
        uiManager.CreateCancelButton(character);

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
        gridManager.ColourTiles(sprintTiles, false);
        gridManager.ColourTiles(walkTiles, true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hideGridMoveOnExit)
            gridManager.ClearMap();
    }
}
