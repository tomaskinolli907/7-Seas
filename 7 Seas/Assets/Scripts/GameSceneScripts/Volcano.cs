using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano : MonoBehaviour
{

    public TilingEngine tE;
    public TileSprite[,] tiles;
    public GameLoop gameLoop;
    public RoundEvents roundEvents;
    //public int lavaDistance = 5;
    int[] lavaAmount;
    public List<GameObject> lavatilesList = new List<GameObject>();
    Player currentPlayer;
    public Players players;
    public Shader shader1;
    public Shader shader2;
    public Sprite shoalLavaTile;
    public Sprite oceanLavaTile;
    public Sprite deepblueLavaTile;
    public Sprite volcano1;
    public Sprite volcano2;
    public Sprite volcano3;
    public Sprite volcano4;
    public Sprite volcano5;
    bool setVolcShader = false;
    public int lavaRadius;
    public LoseTurn loseTurn;


    // Use this for initialization
    void Start()
    {
        tiles = tE.terrainMap;
        lavaAmount = new int[1024];
    }

    //change the look of the volcano based on its lava level
    public void changeVolcano(int[] lavalLevel)
    {
        for (int i = 0; i < tE.volcanoList.Count; ++i)
        {
            if (lavalLevel[i] <= 1)
            {
                tE.volcanoList[i].GetComponent<SpriteRenderer>().sprite = volcano1;
            }
            else if (lavalLevel[i] == 2)
            {
                tE.volcanoList[i].GetComponent<SpriteRenderer>().sprite = volcano2;
            }
            else if (lavalLevel[i] == 3)
            {
                tE.volcanoList[i].GetComponent<SpriteRenderer>().sprite = volcano3;
            }
            else if (lavalLevel[i] == 4)
            {
                tE.volcanoList[i].GetComponent<SpriteRenderer>().sprite = volcano4;
            }
            else if (lavalLevel[i] >= 5)
            {
                tE.volcanoList[i].GetComponent<SpriteRenderer>().sprite = volcano5;
            }

            //set volcano shader
            if(!setVolcShader)
            {
                tE.volcanoList[i].GetComponent<Renderer>().material.shader = shader2;
            }

        }
        setVolcShader = true;
    }

    //check if volcano event is up if so add lava
    public void checkevents()
    {
        if(roundEvents.currentEvent == "Volcanoes Get Lava")
        {
            AddLava();
        }
    }
    //add lava each round
    public void addroundlava()
    {
        if (gameLoop.playersTurn == 1)
        {
            for (int i = 0; i < tE.volcanoList.Count; ++i)
            {
                int mynum = Random.Range(0, 3);
                lavaAmount[i] += mynum; //random lava amount
            }
        }
        changeVolcano(lavaAmount);
    }

    //adds the actual lava
    void AddLava()
    {
        for (int i = 0; i < tE.volcanoList.Count; ++i)
        {
            int num = Random.Range(0, 3); //returns a rand numb between 0 and 2
            lavaAmount[i] += num; //random lava amount
        }
        changeVolcano(lavaAmount);
    }

    //checks if lava amount is at erupt pont of >= 5
    void CheckLavaAmount()
    {
        for (int i = 0; i < tE.volcanoList.Count; ++i)
        {
            if (lavaAmount[i] >= 5)
            {
                Erupt(tE.volcanoList[i], lavaAmount[i]);
                gameLoop.ParrotAnchorShaderChange();
                lavaAmount[i] = 0;
            }
        }
    }

    //check if a player is on lava and punish him accordingly
    public void CheckPlayerOnLava()
    {
        //get current player
        currentPlayer = players.playersList[gameLoop.playersTurn - 1];
        foreach (var item in lavatilesList)
        {
            if (item != null)
            {
                if (item.transform.position == new Vector3(currentPlayer.transform.position.x,
                                                           currentPlayer.transform.position.y,
                                                           item.transform.position.z))
                {
                    //Debug.Log("Touched Lava");
                    loseTurn.ShowWindow("Lose Turn, Engulfed in Lava");
                    currentPlayer.transform.position = new Vector3(currentPlayer.homePort.x * (int)gameLoop.tE.tileSize,
                        -currentPlayer.homePort.y * (int)gameLoop.tE.tileSize, currentPlayer.transform.position.z);
                    currentPlayer.shipPos = currentPlayer.homePort;
                }
            }
        }
    }

    //erupts the volcanos
    void Erupt(GameObject volcano, int lavaAmt)
    {
        //fill the tiles in a radius around the volcano position by the lava amount
        for (int y = 0; y < 32; ++y)
        {
            for (int x = 0; x < 32; ++x)
            {
                if (Mathf.Abs(x - volcano.transform.position.x/2) <= lavaRadius)
                {

                    if (Mathf.Abs(y + volcano.transform.position.y/2) <= lavaRadius)
                    {
                        //mark the tile as lava if its ocean surrounding volcano
                        if (tE.terrainMap[x, y].TileType == Tiles.shoal)
                        {
                            tE._tiles[x, y].GetComponent<SpriteRenderer>().sprite = shoalLavaTile;
                            tE._tiles[x, y].GetComponent<Renderer>().material.shader = shader2;
                            lavatilesList.Add(tE._tiles[x, y]);
                        }
                        else if (tE.terrainMap[x, y].TileType == Tiles.ocean)
                        {
                            tE._tiles[x, y].GetComponent<SpriteRenderer>().sprite = oceanLavaTile;
                            tE._tiles[x, y].GetComponent<Renderer>().material.shader = shader2;
                            lavatilesList.Add(tE._tiles[x, y]);
                        }
                        else if (tE.terrainMap[x, y].TileType == Tiles.deepblue)
                        {
                            tE._tiles[x, y].GetComponent<SpriteRenderer>().sprite = deepblueLavaTile;
                            tE._tiles[x, y].GetComponent<Renderer>().material.shader = shader2;
                            lavatilesList.Add(tE._tiles[x, y]);
                        }
                    }

                }

            }

            CheckPlayerOnLava();
            
        }

        //set boolean for each player if they are on lava
        foreach (var pL in players.playersList)
        {
            foreach(var lT in lavatilesList)
            if (lT.transform.position == new Vector3(pL.transform.position.x,
                                                           pL.transform.position.y,
                                                           lT.transform.position.z))
                {
                    pL.isOnLava = true;
                    //Debug.Log("Boolean Changed!");
                }
        }


    }

    //resets the lava at the end of a round
    public void WipeLava()
    {
        if (gameLoop.playersTurn == 1)
        {
            for (int y = 0; y < 32; ++y)
            {
                for (int x = 0; x < 32; ++x)
                {
                    if (tE._tiles[x, y].GetComponent<SpriteRenderer>().sprite == shoalLavaTile
                        || tE._tiles[x, y].GetComponent<SpriteRenderer>().sprite == oceanLavaTile
                        || tE._tiles[x, y].GetComponent<SpriteRenderer>().sprite == deepblueLavaTile)
                    {
                        tE._tiles[x, y].GetComponent<SpriteRenderer>().sprite = tE.terrainMap[x, y].TileImage;
                        tE._tiles[x, y].GetComponent<Renderer>().material.shader = shader1;
                        //tE.circleMap[x,y].GetComponent<SpriteRenderer>().enabled = true;
                        //tE._tiles[x, y].GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                        //tE._tiles[x, y].GetComponent<SpriteRenderer>().color = Color.white;
                        lavatilesList.Clear();
                    }
                    foreach(var pL in players.playersList)
                    {
                        pL.isOnLava = false;
                    }
                }
            }
        }
    }

    //if L is pressed add lava for debugging purposes
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("add lava!");
            AddLava();
        }

        CheckLavaAmount();
    }


}
