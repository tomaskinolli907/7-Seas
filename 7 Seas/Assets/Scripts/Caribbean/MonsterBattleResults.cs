using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBattleResults : MonoBehaviour
{
    public Text HealthLostText;
    public Text MonsterStatusText;
    // Start is called before the first frame update
    void Start()
    {
        HealthLostText.text = HealthLostText.text.Replace("@", PlayerPrefs.GetInt("DamageDoneMonster").ToString());

        if (PlayerPrefs.GetString("MonsterStatus") == "Dead")
        {
            int GoldEarned = Random.Range(700, 1400);
            MonsterStatusText.text = "THE MONSTER HAS BEEN SLAIN!  YOUR CREW REJOICES AS YOU TURN IN YOUR MONSTER PARTS FOR: " + GoldEarned + " GOLD!";
            if (PlayersManager.Opponent1.Team == 1)
            {
                GameManager.RedTeamGold += GoldEarned;
            }
            else
            {
                GameManager.BlueTeamGold += GoldEarned;
            }
            PlayersManager.Opponent1.Mutiny -= 50;
            if (PlayersManager.Opponent1.Mutiny <= 0)
            {
                PlayersManager.Opponent1.Mutiny = 0;
            }
        }
        else
        {
            MonsterStatusText.text = "THE MONSTER GOT AWAY!  YOUR CREW LOSES HOPE AFTER SUCH A DEFEAT.";
            PlayersManager.Opponent1.Mutiny += 20;
            if (PlayersManager.Opponent1.Mutiny >= 100)
            {
                PlayersManager.Opponent1.Mutiny = 100;
            }
        }
    }
}
