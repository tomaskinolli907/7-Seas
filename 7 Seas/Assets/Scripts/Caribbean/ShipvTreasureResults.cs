using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipvTreasureResults : MonoBehaviour
{
    public Text GoldEarned;
    // Start is called before the first frame update
    void Start()
    {
        PlayersManager.Opponent1.Mutiny -= 20;
        if (PlayersManager.Opponent1.Mutiny < 0)
        {
            PlayersManager.Opponent1.Mutiny = 0;
        }
        GoldEarned.text = "GOLD EARNED: " + PlayerPrefs.GetInt("Treasure Score");
        if (PlayersManager.Opponent1.Team == 1)
        {
            GameManager.RedTeamGold += PlayerPrefs.GetInt("Treasure Score");
        }
        else
        {
            GameManager.BlueTeamGold += PlayerPrefs.GetInt("Treasure Score");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
