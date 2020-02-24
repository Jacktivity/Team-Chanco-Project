using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PersistantData
{
    public static int score;
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

    public static int GetScore()
    {
        return score;
    }

    public static int EndTurnWin(/*int level, int turns*/)
    {
        return score;
    }

    public static int EndTurnLose()
    {
        return score;
    }
}
