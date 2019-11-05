using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool playerTurn;
    private Coroutine enemyTurnCoroutine;
    static int turnNumber = 0;
    UIManager uiManager;
    //[SerializeField] private EnemyAI enemyAIContainer;

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
            if (player.GetComponent<TestPlayerScript>().hasTurn)
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
            enemyTurnCoroutine = StartCoroutine(MovingEnemies(enemy));
            yield return enemyTurnCoroutine;
        }
        ResetPlayerTurn();
    }

    public void ResetPlayerTurn()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.GetComponent<TestPlayerScript>().hasTurn = true;
        }
        playerTurn = true;

        turnNumber++;
        uiManager.UpdateTurnNumber(turnNumber);
    }

    public void CycleTurns()
    {
        if (!CheckPlayerTurn())
        {
            StartCoroutine(MoveEnemies());
        }
    }
    IEnumerator MovingEnemies(GameObject enemy)
    {
        //enemyAIContainer.MoveUnit();
        yield return new WaitForSeconds(1);
    }

    public int GetTurnNumber()
    {
        return turnNumber;
    }
}
