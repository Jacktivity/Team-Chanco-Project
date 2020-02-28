using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Character selectedPlayer, selectedEnemy;
    public BlockScript[] walkTiles, sprintTiles;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private UIManager uiManager;

    public List<Character> globalUnitList;
    public List<Character> activeEnemyCaptains;
    public List<Character> activePlayerCaptains;
    public List<Character> activeEnemies;
    public List<Character> activePlayers;
    public List<Character> activePlayersInTurn;

    // Start is called before the first frame update
    void Start()
    {
        globalUnitList = new List<Character>();
        activeEnemyCaptains = new List<Character>();
        activePlayerCaptains = new List<Character>();
        activeEnemies = new List<Character>();
        activePlayers = new List<Character>();
        activePlayersInTurn = new List<Character>();

        GridManager.unitSpawned += (s, e) => { e.characterClicked += (sender, character) => PlayerUnitChosen(e); };
        GridManager.unitSpawned += (s, e) => { e.moveComplete += (sender, character) => gridManager.CycleTurns(); };
        GridManager.unitSpawned += (s, e) => { e.attackComplete += (sender, character) => gridManager.CycleTurns(); };
        GridManager.enemySpawned += (s, e) => { e.unit.characterClicked += (sender, character) => EnemyUnitChosen(e.unit); };
        //BlockScript.blockClicked += (s, e) => BlockClicked(e);
        ChooseAttackButton.pointerExit += ResetMapMovement;
        MoveButton.pointerExit += ResetMapMovement;
    }

    private void OnDestroy()
    {
        ChooseAttackButton.pointerExit -= ResetMapMovement;        
        MoveButton.pointerExit -= ResetMapMovement;
    }

    private void ResetMapMovement(object sender, EventArgs e)
    {
        var playerMover = FindObjectOfType<PlayerCharacterMover>();
        if (playerMover.MovableUnit)
        {
            HighlightMovementTiles(playerMover.Unit);
        }
    }

    public void AddUnit(Character unit)
    {
        globalUnitList.Add(unit);
        uiManager.InstantiateUIBars(unit);
        uiManager.InstantiateMarker(unit);

        if(unit.tag == "Player") {
            activePlayers.Add(unit);
        } else if(unit.tag == "Enemy") {
            activeEnemies.Add(unit);
        }

        if (unit.isCaptain)
        {
            AddCaptain(unit);
        }
    }

    public void RemoveUnit(Character unit)
    {
        if (unit.tag == "Enemy") {
            activeEnemies.Remove(unit);
        } else if (unit.tag == "Player") {
            activePlayers.Remove(unit);
        }
    }

    public void AddCaptain(Character unit)
    {
        if (unit.tag == "Enemy") {
            activeEnemyCaptains.Add(unit);
        } else if(unit.tag == "Player") {
            activePlayerCaptains.Add(unit);
        }
    }

    public void RemoveCaptain(Character unit)
    {
        if (unit.tag == "Enemy")
        {
            activeEnemyCaptains.Remove(unit);
        } else if (unit.tag == "Player") {
            activePlayerCaptains.Remove(unit);
        }
    }

    private void MovePlayerToBlock(BlockScript tile)
    {
        bool sprinting = walkTiles.Contains(tile) == false && sprintTiles.Contains(tile);
        if (sprinting)
        {
            selectedPlayer.MoveUnit(selectedPlayer.pathfinder.GetPath(selectedPlayer.floor, tile, selectedPlayer.isFlying == false));
            gridManager.ClearMap();
            if(selectedPlayer.tag =="Player")
            {
                gridManager.nextUnit();
            }
        }
        else if (walkTiles.Contains(tile))
        {
            selectedPlayer.MoveUnit(selectedPlayer.pathfinder.GetPath(selectedPlayer.floor, tile, selectedPlayer.isFlying == false));
            gridManager.ClearMap();
        }
        else
        {
            Debug.Log("Clicked invalid block");
        }
    }

    public void PlayerUnitChosen(Character unit)
    {
        
        if (gridManager.playerTurn && unit.ActionPoints >= 0)
        {
            uiManager.CreateActionButtons( unit.attacks, unit);            

            if (selectedPlayer != null)
            {
                selectedPlayer.godRay.SetActive(false);
                selectedPlayer.ScaleVFX(false);
                gridManager.ClearMap();
            }

            selectedPlayer = unit;
            selectedPlayer.ScaleVFX(true);
            selectedPlayer.godRay.SetActive(true);
            HighlightMovementTiles(unit);
            GetComponent<PlayerCharacterMover>().SetMovement(unit, walkTiles, sprintTiles);
        }
        else
            selectedPlayer = null;
    }

    public void HighlightMovementTiles(Character unit)
    {
        if (unit.MaxAP)
        {
            walkTiles = unit.pathfinder.GetTilesInRange(unit.floor, unit.movementSpeed, unit.isFlying, unit.isFlying, unit.isFlying).Where(t => t.Occupied == false).ToArray();
            sprintTiles = unit.pathfinder.GetTilesInRange(unit.floor, unit.movementSpeed + unit.movemenSprint, unit.isFlying, unit.isFlying, unit.isFlying).Where(t => t.Occupied == false).ToArray();
        }
        else if(unit.CanMove)
        {
            sprintTiles = unit.pathfinder.GetTilesInRange(unit.floor, unit.movemenSprint, unit.isFlying, unit.isFlying, unit.isFlying).Where(t => t.Occupied == false).ToArray();
            walkTiles = new BlockScript[0];
        }
        else
        {
            walkTiles = sprintTiles = new BlockScript[0];
        }

        gridManager.ColourTiles(sprintTiles, gridManager.SprintColour);
        gridManager.ColourTiles(walkTiles, gridManager.WalkColour);
    }

    public void EnemyUnitChosen(Character unit)
    {
        Debug.Log("Enemy clicked");
    }
}
