using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public int score;
    public Text scoreText;

    void Awake()
    {
        score = 0;
    }

    public void AddToScore(int amount)
    {
        score += amount;
        scoreText.text = "Score : " +score.ToString();
    }
}
