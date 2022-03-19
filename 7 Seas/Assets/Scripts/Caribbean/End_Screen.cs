using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class End_Screen : MonoBehaviour
{
    public Text currentScore;
    public Text highScore;
    // Start is called before the first frame update
    void Start()
    {
        int score = PlayerPrefs.GetInt("score");
        int highestScore = PlayerPrefs.GetInt("highScore");
        Text currScore = currentScore.GetComponent<Text>();
        Text hiScore = highScore.GetComponent<Text>();

        currScore.text = "Score: " + score;
        hiScore.text = "High Score: " + highestScore;
        PlayerPrefs.SetInt("score", 0);

        Debug.Log(" Escore" + score);
        Debug.Log("EHighestScore: " + highestScore);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
