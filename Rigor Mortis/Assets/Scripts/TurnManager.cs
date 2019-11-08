using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool playerTurn;
    private Coroutine enemyTurnCoroutine;
    static int turnNumber = 1;
    UIManager uiManager;
    public static EventHandler turnEnded;
    public GridManager gridManager;
    [SerializeField] private EnemyAI enemyAIContainer;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Find("EventSystem").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckPlayerTurn()
    {
        playerTurn = false;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            var character = player.GetComponent<Character>();
            if (character.hasTurn && character.moving == false)
            {
                playerTurn = true;
            }
        }
        return playerTurn;
    }

    public IEnumerator MoveEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Character>().hasTurn = true;
            enemyTurnCoroutine = StartCoroutine(MovingEnemies(enemy));
            yield return enemyTurnCoroutine;
        }
        turnEnded?.Invoke(this, new EventArgs());
        ResetPlayerTurn();
    }

    public void ResetPlayerTurn()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            var playerScript = player.GetComponent<Character>();
            playerScript.hasTurn = true;
            playerScript.movedThisTurn = false;

            player.gameObject.GetComponentInChildren<Renderer>().material.color = Color.white;
        }
        playerTurn = true;
        gridManager.nextUnit();
        turnNumber++;
        uiManager.UpdateTurnNumber(turnNumber);
    }

    public void CycleTurns()
    {
        if (!CheckPlayerTurn())
        {
            turnEnded?.Invoke(this, new EventArgs());
            StartCoroutine(MoveEnemies());
        }
    }
    IEnumerator MovingEnemies(GameObject enemy)
    {
        enemyAIContainer.MoveUnit();

        yield return new WaitForSeconds(1);
    }

    public int GetTurnNumber()
    {
        return turnNumber;
    }
}
