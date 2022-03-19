using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayersManager
{
    private static GameObject PlayerManager;
    private static List<PlayerC> players = new List<PlayerC>();
    public static PlayerC Opponent1;
    public static PlayerC Opponent2;
    public static PlayerC testPlayer;

    public static void SetPlayerManager(this GameObject obj)
    {
        PlayerManager = obj;
        Object.DontDestroyOnLoad(obj);
    }

    public static void Destroy(this GameObject obj)
    {
        PlayerManager = null;
        Object.Destroy(obj);
    }

    public static GameObject GetPlayerManager()
    {
        return PlayerManager;
    }

    public static List<PlayerC> GetPlayers()
    {
        return players;
    }

    public static void PopulatePlayers(string[] names)
    {
        players.Clear();
        for (int i = 1; i <= 8; i++)
        {
            PlayerC newPlayer = new PlayerC()
            {
                Id = i,
                Gold = 0,
                Health = 100,
                Name = names[i - 1],
                IsFighting = false
            };
            if (i < 5) //players 1-4 red beards
            {
                newPlayer.Team = 1;
            }
            else //players 5-8 blue beards
            {
                newPlayer.Team = 2;
            }
            players.Add(newPlayer);
        }

        // Set up team gold counts set at 0 for red and blue team
        PlayerPrefs.SetInt("RedTeamGold", 0);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("BlueTeamGold", 0);
        PlayerPrefs.Save();
    }

    public static void ClearPlayers()
    {
        players.Clear();
    }

    //Used in Add_player in the CreatePlayers function
    public static void SetOpponents(PlayerC First, PlayerC Second)
    {
        Opponent1 = First;
        Opponent2 = Second;
    }
    //test function to add a player
    //public static void SetPlayer()
    //{
    //    Player newPlayer = new Player()
    //    {
    //        Id = 1,
    //        Gold = 0,
    //        Health = 100,
    //        Name = "Jeff",
    //        IsFighting = false
    //    };
    //    players.Add(newPlayer);
    //    testPlayer = newPlayer;
    //    Debug.Log(testPlayer.Name);

    //}
}
