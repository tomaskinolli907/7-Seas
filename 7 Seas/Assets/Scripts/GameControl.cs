using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

    public static GameControl control;

    public float health;
    public float experience;
    GameLoop gameLoop;
    public List<Player> playersList;

    void Awake()
    {
        /*if(control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if(control != this)
        {
            Destroy(gameObject);
        }*/
    }

    void Start()
    {
        //gameLoop = GameObject.Find("GameLoop").GetComponent<GameLoop>();
        //playersList = gameLoop.players.playersList;
    }

    public void StoreData()
    {
        //playersList = new List<Player>();
       // playersList.Add( gameLoop.players.playersList[0]);
    }

    public void RetrieveData()
    {
        //gameLoop.players.playersList = playersList;
    }

}
