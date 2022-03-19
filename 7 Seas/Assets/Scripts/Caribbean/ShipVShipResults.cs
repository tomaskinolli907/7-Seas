using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipVShipResults : MonoBehaviour
{
    public Text WinnerText;
    public Text Player1ScoreText;
    public Text Player2ScoreText;
    public Text WinnerResults;
    private int P1Score;
    private int P2Score;
    // Start is called before the first frame update
    void Start()
    {
        P1Score = PlayerPrefs.GetInt("Player1Score");
        P2Score = PlayerPrefs.GetInt("Player2Score");
        int results = CalculateWinner() * 100;
        if (P1Score > P2Score)
        {
            WinnerText.text = PlayersManager.Opponent1.Name.ToUpper() + " Wins!".ToUpper();
            WinnerResults.text = PlayersManager.Opponent1.Name.ToUpper() + " gets: ".ToUpper() + results + " gold!".ToUpper();
            // if the winner is on team 1 then add the gold to red team and subtract from blue team
            if (PlayersManager.Opponent1.Team == 1)
            {
                GameManager.RedTeamGold += results;
                int loserGold = GameManager.BlueTeamGold;
                if ((loserGold - results) < 0)
                    GameManager.BlueTeamGold = 0;
                else
                    GameManager.BlueTeamGold -= results;
            }
            //If winner is blue team
            else
            {
                GameManager.BlueTeamGold += results;
                int loserGold = GameManager.RedTeamGold;
                if ((loserGold - results) < 0)
                    GameManager.RedTeamGold = 0;
                else
                    GameManager.RedTeamGold -= results;
            }
            //PlayersManager.Opponent1.Gold += results;
            PlayersManager.Opponent1.Mutiny -= 20;
            if (PlayersManager.Opponent1.Mutiny < 0)
            {
                PlayersManager.Opponent1.Mutiny = 0;
            }
            //PlayersManager.Opponent2.Gold -= results;
            PlayersManager.Opponent2.Mutiny += 10;
            if (PlayersManager.Opponent2.Mutiny >= 100)
            {
                PlayersManager.Opponent2.Mutiny = 100;
            }
            //if (PlayersManager.Opponent2.Gold < 0)
            //{
            //    PlayersManager.Opponent2.Gold = 0;
            //}
        }
        else if (P1Score < P2Score)
        {
            WinnerText.text = PlayersManager.Opponent2.Name.ToUpper() + " Wins!".ToUpper();
            WinnerResults.text = PlayersManager.Opponent2.Name.ToUpper() + " gets: ".ToUpper() + results + " gold!".ToUpper();
            //PlayersManager.Opponent2.Gold += results;
            if (PlayersManager.Opponent2.Id <= 4)
            {
                GameManager.RedTeamGold += results;
                int loserGold = GameManager.BlueTeamGold;
                if ((loserGold - results) < 0)
                    GameManager.BlueTeamGold = 0;
                else
                    GameManager.BlueTeamGold -= results;
            }
            //If winner is blue team
            else
            {
                GameManager.BlueTeamGold += results;
                int loserGold = GameManager.RedTeamGold;
                if ((loserGold - results) < 0)
                    GameManager.RedTeamGold = 0;
                else
                    GameManager.RedTeamGold -= results;
            }
            PlayersManager.Opponent2.Mutiny -= 20;
            if (PlayersManager.Opponent2.Mutiny < 0)
            {
                PlayersManager.Opponent2.Mutiny = 0;
            }
            //PlayersManager.Opponent1.Gold -= results;
            PlayersManager.Opponent1.Mutiny += 10;
            if (PlayersManager.Opponent1.Mutiny >= 100)
            {
                PlayersManager.Opponent1.Mutiny = 100;
            }
            //if (PlayersManager.Opponent1.Gold < 0)
            //{
            //    PlayersManager.Opponent1.Gold = 0;
           // }
        }
        else
        {
            WinnerText.text = "Yar...tis a draw!".ToUpper();
            WinnerResults.text = "No booty for either of you powder monkeys!".ToUpper();
        }

        Player1ScoreText.text = PlayersManager.Opponent1.Name.ToUpper() + " Score: " + P1Score;
        Player2ScoreText.text = PlayersManager.Opponent2.Name.ToUpper() + " Score: " + P2Score;
        PlayerPrefs.Save();

        PlayersManager.Opponent1.Health -= P2Score;
        PlayersManager.Opponent2.Health -= P1Score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int CalculateWinner()
    {
        return (Math.Abs(P1Score - P2Score));
    }
}