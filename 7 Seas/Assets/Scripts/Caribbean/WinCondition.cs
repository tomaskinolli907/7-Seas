using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    private int redTeamGold;
    private int blueTeamGold;


    // Update is called once per frame
    void Update()
    {
        if (GameManager.RedTeamGold >= 5000 || GameManager.BlueTeamGold >= 5000)
            SceneManager.LoadScene("MultiPirateWin");
    }
}
