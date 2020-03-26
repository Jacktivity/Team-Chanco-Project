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
    }

    public static float GetScore()
    {
        return score;
    }

    public static float EndTurnWin(int turns, int par)
    {
        float spdMP = 1;
        if(turns < par) {
            for(int i = 0; i < (par - turns); i++)
            {
                spdMP = spdMP + 0.1f;
            }
        } else if(turns > par) {
            for (int i = 0; i < (turns - par); i++)
            {
                if (spdMP > 0.1) { //What to do if more than 10 turns under par?
                    spdMP = spdMP - 0.1f;
                }
            }
        }
        score = Mathf.RoundToInt(score * spdMP);
        return score;
    }

    public static float EndTurnLose()
    {
        return score;
    }
}
