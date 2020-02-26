using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PersistantData
{
    public static float score;
    public static TextAsset level;
    public static bool levelAssigned = false;

    public static void RemoveScore(int amount)
    {
        score += amount;
    }

    public static void AddScore(int amount)
    {
        score += amount;
        Debug.Log("SCORE NOW EQUALS: " + score);
    }

    public static float GetScore()
    {
        return score;
    }

    public static float EndTurnWin(int turns, int par)
    {
        float spdMP = 1;
        Debug.Log("Initial Speed Multiplier : " + spdMP + " Par: " + par + " Turns: " + turns);
        if(turns < par) {
            for(int i = 0; i < (par - turns); i++)
            {
                spdMP = spdMP + 0.1f;
                Debug.Log("Speed Multiplier : " + spdMP);
            }
        } else if(turns > par) {
            for (int i = 0; i < (turns - par); i++)
            {
                if (spdMP > 0.1) { //What to do if more than 10 turns under par?
                    spdMP = spdMP - 0.1f;
                    Debug.Log("Speed Multiplier : " + spdMP);
                }
            }
        }
        Debug.Log("Score : " + (score * spdMP));
        score = Mathf.RoundToInt(score * spdMP);
        return score;
    }

    public static float EndTurnLose()
    {
        return score;
    }
}
