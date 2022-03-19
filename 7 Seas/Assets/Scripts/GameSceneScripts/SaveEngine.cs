using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveEngine : MonoBehaviour {

    public GameLoop gameLoop;
    public TilingEngine tE;
    public PlayerMove playerMove;
    public TargetShips targetShips;
    bool saveExists = false;

    //the save engine
    public void Save()
    {
        PlayerPrefs.SetInt("LoadGame", 0);

        //turn a saved game boolean to true
        saveExists = true;
        ES2.Save(saveExists, Application.persistentDataPath + "/saveExists");
        ES2.Save(gameLoop.playersTurn, Application.persistentDataPath + "/playersTurn");

        //save player count and gold target
        ES2.Save(gameLoop.numOfPlayers, Application.persistentDataPath + "/numOfPlayers");
        ES2.Save(gameLoop.treasureTargetAmnt, Application.persistentDataPath + "/treasureTargetAmnt");

        //players saves
        for (int i = 0; i < gameLoop.numOfPlayers; ++i)
        {
            //save player info
            var player = gameLoop.players.originalPlayersList[i];
            ES2.Save(player.safeColor, Application.persistentDataPath + "/safeColor" + i);
            ES2.Save(player.transform.position, Application.persistentDataPath + "/playerPosition" + i);
            ES2.Save(player.homePort, Application.persistentDataPath + "/playerHomePort" + i);
            ES2.Save(player.buriedTreasure, Application.persistentDataPath + "/playerBT" + i);
            ES2.Save(player.shipsPirated, Application.persistentDataPath + "/playerShipsPirated" + i);
            ES2.Save(player.powderKegs, Application.persistentDataPath + "/playerPK" + i);
            ES2.Save(player.canonBalls, Application.persistentDataPath + "/playerCB" + i);
            ES2.Save(player.onSafeColor, Application.persistentDataPath + "/playerOnSafeColor" + i);
            ES2.Save(player.shipPos, Application.persistentDataPath + "/playerShipPos" + i);
            ES2.Save(player.gold, Application.persistentDataPath + "/playerGold" + i);
            ES2.Save(player.loseThisTurn, Application.persistentDataPath + "/playerLoseThisTurn" + i);
            ES2.Save(player.shipOn, Application.persistentDataPath + "/playerShipOn" + i); 
            ES2.Save(player.isMyTurn, Application.persistentDataPath + "/playerIsMyTurn" + i);


        }

        //target ship saves
        for(int i = 0; i < targetShips.tarShipList.Length; ++i)
        {
            ES2.Save(targetShips.tarShipList[i].GetComponent<SpriteRenderer>().sprite, Application.persistentDataPath + "/tarShipImage" + i);
            ES2.Save(targetShips.tarShipList[i].transform.position, Application.persistentDataPath + "/tarShipLocation" + i);

        }

        ES2.Save(playerMove.windCount, Application.persistentDataPath + "/windCount");
        ES2.Save(playerMove.movementCount, Application.persistentDataPath + "/movementCount");

        //wind direction
        ES2.Save(gameLoop.direction, Application.persistentDataPath + "/windDirection");

        //save dice
        ES2.Save(gameLoop.diceShip.GetComponent<Image>().sprite, Application.persistentDataPath + "/diceShip");
        ES2.Save(gameLoop.diceColor.GetComponent<Image>().sprite, Application.persistentDataPath + "/diceColor");
        ES2.Save(gameLoop.diceMov.GetComponent<Image>().sprite, Application.persistentDataPath + "/diceMov");
        ES2.Save(gameLoop.diceWind.GetComponent<Image>().sprite, Application.persistentDataPath + "/diceWind");
        ES2.Save(gameLoop.diceResource.GetComponent<Image>().sprite, Application.persistentDataPath + "/diceResource");

        ES2.Save(gameLoop.playersOrder, Application.persistentDataPath + "/playersOrder");

    }


    //the load engine
    public void Load()
    {
        if (ES2.Exists(Application.persistentDataPath + "/saveExists"))
        {
            saveExists = ES2.Load<bool>(Application.persistentDataPath + "/saveExists");
        }

        if (saveExists)
        {
            Debug.Log("Loading");

            //turn a saved game boolean to false
            saveExists = false;
            ES2.Save(saveExists, Application.persistentDataPath + "/saveExists");

            //load dice
            gameLoop.diceShip.GetComponent<Image>().sprite = ES2.Load<Sprite>(Application.persistentDataPath + "/diceShip");
            gameLoop.diceColor.GetComponent<Image>().sprite = ES2.Load<Sprite>(Application.persistentDataPath + "/diceColor");
            gameLoop.diceMov.GetComponent<Image>().sprite = ES2.Load<Sprite>(Application.persistentDataPath + "/diceMov");
            gameLoop.diceWind.GetComponent<Image>().sprite = ES2.Load<Sprite>(Application.persistentDataPath + "/diceWind");
            gameLoop.diceResource.GetComponent<Image>().sprite = ES2.Load<Sprite>(Application.persistentDataPath + "/diceResource");

            //load treasure target amount
            PlayerPrefs.SetInt("TreasureAmount", ES2.Load<int>(Application.persistentDataPath + "/treasureTargetAmnt"));


            //players loads
            for (int i = 0; i < gameLoop.numOfPlayers; ++i)
            {
                var player = gameLoop.players.originalPlayersList[i];
                player.safeColor = ES2.Load<Color>(Application.persistentDataPath + "/safeColor" + i);
                player.transform.position = ES2.Load<Vector3>(Application.persistentDataPath + "/playerPosition" + i);
                player.homePort = ES2.Load<Vector2>(Application.persistentDataPath + "/playerHomePort" + i);
                player.buriedTreasure = ES2.Load<int>(Application.persistentDataPath + "/playerBT" + i);
                player.shipsPirated = ES2.LoadArray<int>(Application.persistentDataPath + "/playerShipsPirated" + i);
                player.powderKegs = ES2.Load<int>(Application.persistentDataPath + "/playerPK" + i);
                player.canonBalls = ES2.Load<int>(Application.persistentDataPath + "/playerCB" + i);
                player.onSafeColor = ES2.Load<bool>(Application.persistentDataPath + "/playerOnSafeColor" + i);
                player.shipPos = ES2.Load<Vector2>(Application.persistentDataPath + "/playerShipPos" + i);
                player.gold = ES2.Load<int>(Application.persistentDataPath + "/playerGold" + i);
                player.loseThisTurn = ES2.Load<bool>(Application.persistentDataPath + "/playerLoseThisTurn" + i);
                player.shipOn = ES2.Load<int>(Application.persistentDataPath + "/playerShipOn" + i);
                player.isMyTurn = ES2.Load<bool>(Application.persistentDataPath + "/playerIsMyTurn" + i);
                player.transform.position = new Vector3(player.shipPos.x * 2, -player.shipPos.y * 2, player.transform.position.z);

            }

            //target ship loads
            for (int i = 0; i < targetShips.tarShipList.Length; ++i)
            {
                targetShips.tarShipList[i].GetComponent<SpriteRenderer>().sprite = ES2.Load<Sprite>(Application.persistentDataPath + "/tarShipImage" + i);
                targetShips.tarShipList[i].transform.position = ES2.Load<Vector3>(Application.persistentDataPath + "/tarShipLocation" + i);
            }


            //load everything else
            gameLoop.playersOrder = ES2.LoadList<int>(Application.persistentDataPath + "/playersOrder");
            gameLoop.LoadPlayerOrder();


            gameLoop.playersTurn = ES2.Load<int>(Application.persistentDataPath + "/playersTurn");
            playerMove.windCount = ES2.Load<int>(Application.persistentDataPath + "/windCount");
            playerMove.movementCount = ES2.Load<int>(Application.persistentDataPath + "/movementCount"); ;

            playerMove.SetWindCount();
            playerMove.SetMovementCount();
            gameLoop.direction = ES2.Load<int>(Application.persistentDataPath + "/windDirection");

            playerMove.SetWindCount();
            playerMove.SetMovementCount();
            gameLoop.showPlayersColors();


        }


    }




}
