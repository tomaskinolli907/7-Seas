using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Add_Player : MonoBehaviour
{
    public const int WIN_CONDITION = 500;
    public const int DEFAULT_HEALTH = 100;
    public const int MAX_PLAYERS = 8;
    public List<PlayerC> players;
    public InputField player1Name;
    public InputField player2Name;
    public InputField player3Name;
    public InputField player4Name;
    public InputField player5Name;
    public InputField player6Name;
    public InputField player7Name;
    public InputField player8Name;
    //public string name1;
    //public string name2;
    //public string name3;
    //public string name4;
    //public string name5;
    //public string name6;
    //public string name7;
    //public string name8;
    public void Awake()
    {
        PlayersManager.SetPlayerManager(this.gameObject);
    }
    public void CreatePlayers()
    {
        string[] names = { player1Name.text, player2Name.text, player3Name.text, player4Name.text,
            player5Name.text, player6Name.text, player7Name.text, player8Name.text };

        PlayersManager.PopulatePlayers(names);

        PlayerPrefs.SetInt("RedTeamGold", 0);
        PlayerPrefs.SetInt("BlueTeamGold", 0);

        PlayerPrefs.Save();

        PlayersManager.SetOpponents(PlayersManager.GetPlayers()[0], PlayersManager.GetPlayers()[4]); // testing stuff!
    }
    
    void Update()
    {
        if (PlayerPrefs.GetInt("RedTeamGold") >= WIN_CONDITION)
        {
            // Red Beards win condition met
            Destroy(this.gameObject);
            PlayerPrefs.SetInt("RedTeamGold", 0);
            PlayerPrefs.SetInt("BlueTeamGold", 0);
        }
        else if (PlayerPrefs.GetInt("BlueTeamGold") >= WIN_CONDITION)
        {
            // Blue Beards win condition met
            Destroy(this.gameObject);
            PlayerPrefs.SetInt("RedTeamGold", 0);
            PlayerPrefs.SetInt("BlueTeamGold", 0);
        }


    }
    //public void setget()
    //{
    //    name1 = player1Name.text;
    //    name2 = player2Name.text;
    //    name3 = player3Name.text;
    //    name4 = player4Name.text;
    //    name5 = player5Name.text;
    //    name6 = player6Name.text;
    //    name7 = player7Name.text;
    //    name8 = player8Name.text;

    //    PlayerPrefs.SetString("Player1Name", name1);
    //    PlayerPrefs.SetInt("Player1Gold", 0);
    //    PlayerPrefs.SetInt("Player1Health", 100);
    //    PlayerPrefs.SetString("Player2Name", name2);
    //    PlayerPrefs.SetInt("Player2Gold", 0);
    //    PlayerPrefs.SetInt("Player2Health", 100);
    //    PlayerPrefs.SetString("Player3Name", name3);
    //    PlayerPrefs.SetInt("Player3Gold", 0);
    //    PlayerPrefs.SetInt("Player3Health", 100);
    //    PlayerPrefs.SetString("Player4Name", name4);
    //    PlayerPrefs.SetInt("Player4Gold", 0);
    //    PlayerPrefs.SetInt("Player4Health", 100);
    //    PlayerPrefs.SetString("Player5Name", name5);
    //    PlayerPrefs.SetInt("Player5Gold", 0);
    //    PlayerPrefs.SetInt("Player5Health", 100);
    //    PlayerPrefs.SetString("Player6Name", name6);
    //    PlayerPrefs.SetInt("Player6Gold", 0);
    //    PlayerPrefs.SetInt("Player6Health", 100);
    //    PlayerPrefs.SetString("Player7Name", name7);
    //    PlayerPrefs.SetInt("Player7Gold", 0);
    //    PlayerPrefs.SetInt("Player7Health", 100);
    //    PlayerPrefs.SetString("Player8Name", name8);
    //    PlayerPrefs.SetInt("Player8Gold", 0);
    //    PlayerPrefs.SetInt("Player8Health", 100);

    //    PlayerPrefs.SetInt("Team1Gold", 0);
    //    PlayerPrefs.SetInt("Team2Gold", 0);
    //    PlayerPrefs.Save();
       
    //}
    
}
