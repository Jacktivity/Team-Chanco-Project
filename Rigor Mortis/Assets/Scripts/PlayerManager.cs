using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Character selectedPlayer, selectedEnemy;
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private GridManager gridManager;

    // Start is called before the first frame update
    void Start()
    {        
        gridManager.unitSpawned += (s, e) => { e.characterClicked += (sender, character) => PlayerUnitChosen(e); };
        gridManager.enemySpawned += (s, e) => { e.characterClicked += (sender, character) => EnemyUnitChosen(e); };
    }

    private void CheckEnemyUnitClicks()
    {
        var enemyUnits = FindObjectOfType<EnemyAI>().Units;
        Debug.Log(enemyUnits.Length);
        enemyUnits.Select(u => u.characterClicked += (s, e) => { EnemyUnitChosen(u); });
    }

    public void PlayerUnitChosen(Character unit)
    {
        Debug.Log("Player clicked");

        if (turnManager.playerTurn)
            selectedPlayer = unit;
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
