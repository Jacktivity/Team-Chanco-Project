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

    // Start is called before the first frame update
    void Start()
    {
        GridManager.unitSpawned += (s, e) => { e.characterClicked += (sender, character) => PlayerUnitChosen(e); };
        GridManager.enemySpawned += (s, e) => { e.characterClicked += (sender, character) => EnemyUnitChosen(e); };
        BlockScript.blockClicked += (s, e) => BlockClicked(e);
    }

    private void BlockClicked(BlockScript t)
    {
        if(turnManager.playerTurn && selectedPlayer != null)
        {
            if(selectedPlayer.movedThisTurn == false && selectedPlayer.hasTurn)
            {
                bool sprinting = walkTiles.Contains(t) == false && sprintTiles.Contains(t);
                if(sprinting)
                {
                    selectedPlayer.hasTurn = false;
                    selectedPlayer.MoveUnit(selectedPlayer.pathfinder.GetPath(selectedPlayer.floor, (b) => b == t, selectedPlayer.isFlying == false));
                }
                else if (walkTiles.Contains(t))
                {
                    selectedPlayer.movedThisTurn = true;
                    selectedPlayer.MoveUnit(selectedPlayer.pathfinder.GetPath(selectedPlayer.floor, (b) => b == t, selectedPlayer.isFlying == false));
                }
                else
                {
                    Debug.Log("Clicked invalid block");
                }
            }
        }

        turnManager.CheckPlayerTurn();
    }

    private void CheckEnemyUnitClicks()
    {
        var enemyUnits = FindObjectOfType<EnemyAI>().Units;
        Debug.Log(enemyUnits.Length);
        enemyUnits.Select(u => u.characterClicked += (s, e) => { EnemyUnitChosen(u); });
    }

    public void PlayerUnitChosen(Character unit)
    {
        if (turnManager.playerTurn)
        {
            selectedPlayer = unit;
            walkTiles = unit.pathfinder.GetTilesInRange(unit.floor, unit.movementSpeed, unit.isFlying == false);
            sprintTiles = unit.pathfinder.GetTilesInRange(unit.floor, unit.movementSpeed + unit.movemenSprint, unit.isFlying == false);
            
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
