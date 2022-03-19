using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;    // the text object that displays the score, populate e.g. via inspector

    private int score;

    public void IncrementScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
