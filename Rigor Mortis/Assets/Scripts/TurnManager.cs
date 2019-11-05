using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool playerTurn;
    private Coroutine coroutine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool checkPlayeTurn()
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

    public IEnumerator moveEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            coroutine = StartCoroutine(movingEnemies(enemy));
            yield return coroutine;
        }
        resetPlayerTurn();
    }

    public void resetPlayerTurn()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.GetComponent<TestPlayerScript>().hasTurn = true;
        }
        playerTurn = true;
    }

    public void cycleTurns()
    {
        if (!checkPlayeTurn())
        {
            StartCoroutine(moveEnemies());
        }
    }
    IEnumerator movingEnemies(GameObject enemy)
    {
        yield return new WaitForSeconds(1);
    }
}
