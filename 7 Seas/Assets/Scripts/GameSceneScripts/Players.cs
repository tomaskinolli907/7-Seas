using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Players : MonoBehaviour {

    public Player player0;
    public Player player1;
    public Player player2;
    public Player player3;
    public Player player4;
    public Player player5;
    public Player player6;
    public Player player7;

    public TilingEngine tE;
    public List<Player> originalPlayersList;
    public List<Player> playersList;
    public Player player;
    public Sprite[] playerSprite;
    public Sprite player1Sprite;
    public GameLoop gameLoop;
    public GameObject gameBoarder1;
    public GameObject gameBoarder2;
    public GameObject diceSidebarBG;
    public GameObject ButtonsSidebarBG;
    public GameObject LatLongBG;
    public GameObject eventSpinButton;
    public PortSystem portSystem;



    // Use this for initialization
    void Start ()
    {
        playerSprite = new Sprite[8];

        InitializeListOfPlayers();

    }

    //initialize the players
    public void InitializeListOfPlayers()
    {
        originalPlayersList = new List<Player>();
        originalPlayersList.Add(player0);
        originalPlayersList.Add(player1);
        originalPlayersList.Add(player2);
        originalPlayersList.Add(player3);
        originalPlayersList.Add(player4);
        originalPlayersList.Add(player5);
        originalPlayersList.Add(player6);
        originalPlayersList.Add(player7);

        //create all of the players in game
        for (int i = 0; i < gameLoop.numOfPlayers; ++i)
        {
            //playersList[i] = tempList[i];
            originalPlayersList[i] = (Player)Instantiate(originalPlayersList[i]);
            originalPlayersList[i].gameObject.name = "Player" + (i + 1);
            originalPlayersList[i].playerNum = i + 1;
        }
        playersList = new List<Player>(originalPlayersList);
        playersList[0].isMyTurn = true;

    }

    public void InitializePlayer()
    {
        int index = 0;
        for(int i=0; i < gameLoop.portsArray.Count; ++i)
        {
            int posX = (int)gameLoop.portsArray[i].x;
            int posY = (int)gameLoop.portsArray[i].y;

            player.playerMap[posX, posY] = new TileSprite("player 1", player1Sprite, Tiles.player, tE.terrainMap[posX, posY].TileLocation); //make a new player
            //playersList[index].GetComponent<SpriteRenderer>().sprite = playerSprite[index];
            playersList[index].transform.position = new Vector3(posY * tE.tileSize, -posX * tE.tileSize, -5); //move player to a port position
            playersList[index].homePort = new Vector2(posY, posX); //set the players home position
            playersList[index].shipPos = new Vector2(posY, posX); //set the players position 
            playersList[index].shipOn = 8; //the player is on a port type



            ++index;

            //stop when reached the desired number of players
            if (index == gameLoop.numOfPlayers)
                return;
            
        }

    }

    public void SetPortColors()
    {
        int index = 0;
        for (int i = 0; i < gameLoop.portsArray.Count; ++i)
        {
            int posX = (int)gameLoop.portsArray[i].x;
            int posY = (int)gameLoop.portsArray[i].y;
            foreach (var port in portSystem.portsList)
            {
                if (playersList[index].safeColor == port.playerColor)
                {
                    //tE.terrainMap[posX, posY].TileImage = port.portSprite;
                    tE._tiles[posY, posX].GetComponent<SpriteRenderer>().sprite = port.portSprite;
                    //Debug.Log("port changed: " + playersList[index].name);
                }
                else
                {
                    //Debug.Log("port not changed!!!");
                    //Debug.Log("player color: " + playersList[index].safeColor);
                   //Debug.Log("port color: " + port.playerColor);
                }
                
            }
            ++index;
            if (index == gameLoop.numOfPlayers)
                return;
        }
    }

    public void setTurn()
    {
        foreach (Player player in playersList)
        {
            player.isMyTurn = false;
        }
        playersList[gameLoop.playersTurn - 1].isMyTurn = true;
        setBoarderColor();
    }

    public void setBoarderColor()
    {
        Color color = playersList[gameLoop.playersTurn - 1].safeColor;
        color.a = .7f;
        //playersList[gameLoop.playersTurn - 1].safeColor = color;

        gameBoarder1.GetComponent<Image>().color = color;
        gameBoarder2.GetComponent<Image>().color = color;
        diceSidebarBG.GetComponent<Image>().color = color;
        ButtonsSidebarBG.GetComponent<Image>().color = color;
        LatLongBG.GetComponent<Image>().color = color;
        eventSpinButton.GetComponent<Image>().color = playersList[gameLoop.playersTurn - 1].safeColor;
    }


}
