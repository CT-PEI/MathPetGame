using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int score;

    //In Unity, start/update functions in MonoBehaviour should be removed if empty. This will improve performance because they will not be called.
    public void IncrementScore()
    {
        score++;
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
    }
}
