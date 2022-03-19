using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapEventHandler : MonoBehaviour
{
    private MovementHandler MovementHandlerScript;
    private List<PlayerC> players = PlayersManager.GetPlayers();
    public bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        MovementHandlerScript = GameObject.Find("MovementHandler").GetComponent<MovementHandler>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            if (other.tag == "Monster")
            {
                PlayersManager.Opponent1 = players[GameManager.activeShipIndex];
                MovementHandlerScript.SaveMovementHandler();

                switch (other.gameObject.name)
                {
                    case "Worm Red":
                        GameManager.WormRedDead = true;
                        break;
                    case "Worm Black":
                        GameManager.WormBlackDead = true;
                        break;
                    case "Worm Gold":
                        GameManager.WormGoldDead = true;
                        break;
                    case "Worm Blue":
                        GameManager.WormBlueDead = true;
                        break;
                }
                other.gameObject.Destroy();
                active = false;
                SceneManager.LoadScene("MonsterBattleWorm");
            }
            //if ((other.tag == "RedFort" && players[GameManager.activeShipIndex].Team == 1)
            // || (other.tag == "BlueFort" && players[GameManager.activeShipIndex].Team == 2))
            //{
            //    players[GameManager.activeShipIndex].Health += 20;
            //    if (players[GameManager.activeShipIndex].Health > 100)
            //    {
            //        players[GameManager.activeShipIndex].Health = 100;
            //    }
            //    players[GameManager.activeShipIndex].Mutiny = 0;
            //}
            if (other.tag == "Ship")
            {
                int colliderIndex = int.Parse(other.gameObject.name[1].ToString()) - 1;
                Debug.Log(colliderIndex);
                if (players[colliderIndex].Team != players[GameManager.activeShipIndex].Team)
                {
                    // bumps the ship off of the enemy ship and reduces moves to 0
                    switch (PlayerPrefs.GetString("lastMove"))
                    {
                        case ("up"):
                            MovementHandlerScript.DownClick();
                            break;
                        case ("down"):
                            MovementHandlerScript.UpClick();
                            break;
                        case ("left"):
                            MovementHandlerScript.RightClick();
                            break;
                        case ("right"):
                            MovementHandlerScript.LeftClick();
                            break;
                    }

                    PlayersManager.Opponent1 = players[GameManager.activeShipIndex];
                    PlayersManager.Opponent2 = players[colliderIndex];
                    MovementHandlerScript.SaveMovementHandler();
                    active = false;
                    SceneManager.LoadScene("Ship v Ship");
                }
            }
            if (other.tag == "Treasure Ship")
            {
                PlayersManager.Opponent1 = players[GameManager.activeShipIndex];
                MovementHandlerScript.SaveMovementHandler();

                switch (other.gameObject.name)
                {
                    case "Treasure Ship 1":
                        GameManager.TreasureShip1Dead = true;
                        break;
                    case "Treasure Ship 2":
                        GameManager.TreasureShip2Dead = true;
                        break;
                    case "Treasure Ship 3":
                        GameManager.TreasureShip3Dead = true;
                        break;
                    case "Treasure Ship 4":
                        GameManager.TreasureShip4Dead = true;
                        break;
                }
                other.gameObject.Destroy();
                active = false;
                SceneManager.LoadScene("Ship v Treasure");
            }
        }
    }
}