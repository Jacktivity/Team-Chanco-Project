using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCharacterMover : MonoBehaviour
{
    private Character playerUnitToMove;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private UIManager uiManager;
    private BlockScript[] walkTiles, sprintTiles;

    private void Start()
    {
        BlockScript.blockClicked += MoveUnit;
    }

    public bool MovableUnit => playerUnitToMove != null;
    public Character Unit => playerUnitToMove;

    public void SetMovement(Character unit, BlockScript[] walkTo, BlockScript[] sprintTo)
    {
        if(gameObject.GetComponentsInChildren<Character>().Contains(unit))
        {
            playerUnitToMove = unit;
            walkTiles = walkTo;
            sprintTiles = sprintTo;
        }        
    }

    public void ResetMovement()
    {
        playerUnitToMove = null;
        walkTiles = new BlockScript[0];
        sprintTiles = new BlockScript[0];
    }

    private void MoveUnit(object sender, BlockScript e)
    {
        if(playerUnitToMove != null)
        {
            if ((walkTiles.Contains(e) || sprintTiles.Contains(e)) && playerUnitToMove.CanMove)
            {
                uiManager.attackText.text = "";

                playerUnitToMove.MoveUnit(playerUnitToMove.pathfinder.GetPath(playerUnitToMove.floor, e, playerUnitToMove.isFlying, playerUnitToMove.isFlying));
                playerUnitToMove = null;
                walkTiles = sprintTiles = new BlockScript[0];
                gridManager.ClearMap();
                uiManager.DeleteCurrentPopupButtons();
            }            
        }
    }
}
