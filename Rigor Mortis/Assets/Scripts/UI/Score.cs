using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    static int score;

    public void RemoveScore(int amount)
    {
        score += amount;
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    public int GetScore()
    {
        return score;
    }

    public int Par(int level)
    {
        switch (level) {
            case 1:
            return 8;

            case 2:
            return 13;

            case 3:
            return 7;

            case 4:
            return 6;

            case 5:
            return 10;

            case 6:
            return 8;

            default:
            return 0;
        }
    }

    public int EndTurnWin(int level, int turns)
    {
        float amount = 0;
        if(turns < Par(level)) {
            amount = (turns - Par(level)) * 0.1f;
            amount = Mathf.Abs(amount);

            return Mathf.RoundToInt(score * (-amount));
        } else if(turns > Par(level)) {
            amount = (turns - Par(level)) * 0.1f;
            amount++;

            return Mathf.RoundToInt(score * amount);
        } else {
            return score;
        }
    }

    public int EndTurnLose()
    {
        return score;
    }
}
