using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Character selectedPlayer, selectedEnemy;
    public BlockScript[] walkTiles, sprintTiles;
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private UIManager uiManager;


    // Start is called before the first frame update
    void Start()
    {

        GridManager.unitSpawned += (s, e) => { e.characterClicked += (sender, character) => PlayerUnitChosen(e); };
        GridManager.enemySpawned += (s, e) => { e.characterClicked += (sender, character) => EnemyUnitChosen(e); };
        BlockScript.blockClicked += (s, e) => BlockClicked(e);
    }

    private void BlockClicked(BlockScript t)
    {
        bool unitCanMove = selectedPlayer != null && turnManager.playerTurn;

        if (unitCanMove && t.occupier == null && uiManager.attacking == false)
        {
            if (selectedPlayer.movedThisTurn == false && selectedPlayer.hasTurn)
            {
                bool sprinting = walkTiles.Contains(t) == false && sprintTiles.Contains(t);
                if (sprinting)
                {
                    selectedPlayer.hasTurn = false;
                    selectedPlayer.turnManager.CycleTurns();
                    selectedPlayer.MoveUnit(selectedPlayer.pathfinder.GetPath(selectedPlayer.floor, (b) => b == t, selectedPlayer.isFlying == false));
                    gridManager.ClearMap();
                }
                else if (walkTiles.Contains(t))
                {
                    selectedPlayer.movedThisTurn = true;
                    selectedPlayer.MoveUnit(selectedPlayer.pathfinder.GetPath(selectedPlayer.floor, (b) => b == t, selectedPlayer.isFlying == false));
                    gridManager.ClearMap();
                }
                else
                {
                    Debug.Log("Clicked invalid block");
                }
            }
        }
        else
        {
            if(selectedPlayer != null)
                Debug.Log(selectedPlayer.attackManager.attackerAssigned);
        }
    }

    public void PlayerUnitChosen(Character unit)
    {
        if (turnManager.playerTurn)
        {
            selectedPlayer = unit;
            selectedPlayer.GetComponent<Renderer>().material.color = Color.yellow;

            walkTiles = unit.pathfinder.GetTilesInRange(unit.floor, unit.movementSpeed, unit.isFlying == false).Where(t => t.Occupied == false).ToArray();


            sprintTiles = unit.pathfinder.GetTilesInRange(unit.floor, unit.movementSpeed + unit.movemenSprint, unit.isFlying == false).Where(t => t.Occupied == false).ToArray();

            gridManager.ColourTiles(sprintTiles, false);
            gridManager.ColourTiles(walkTiles, true);
        }            
        else
            selectedPlayer = null;
    }

    public void EnemyUnitChosen(Character unit)
    {
        Debug.Log("Enemy clicked");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
