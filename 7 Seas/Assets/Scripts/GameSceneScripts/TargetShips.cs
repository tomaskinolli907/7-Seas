using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetShips : MonoBehaviour
{

    public TargetShip targetShip0;
    public TargetShip targetShip1;
    public TargetShip targetShip2;
    public TargetShip targetShip3;
    public TargetShip targetShip4;
    public TargetShip targetShip5;


    public List<Sprite> targetShipSprites;

    public TilingEngine tE;
    public TargetShip[] tarShipList;
    public Player player;
    public Sprite tsSprite;
    public GameLoop gameLoop;
    public Vector2[] randomizedMap;

    public List<int> tarShipGoldArray;

    private void Awake()
    {
        //initialize array with targ ships
        tarShipList = new TargetShip[6];
        tarShipList[0] = targetShip0;
        tarShipList[1] = targetShip1;
        tarShipList[2] = targetShip2;
        tarShipList[3] = targetShip3;
        tarShipList[4] = targetShip4;
        tarShipList[5] = targetShip5;

        //if dice exist then load gold amounts
        if (ES2.Exists((PlayerPrefs.GetString("Difficulty") + "tarShipGold")))
        {
            tarShipGoldArray.Clear();
            tarShipGoldArray = ES2.LoadList<int>(PlayerPrefs.GetString("Difficulty") + "tarShipGold");
        }

        //create instances of target ships and set their gold and names
        for (int i = 0; i < tarShipList.Length; ++i)
        {
            tarShipList[i] = (TargetShip)Instantiate(tarShipList[i]);
            tarShipList[i].gameObject.name = "TargetShip" + (i + 1);
            tarShipList[i].gold = tarShipGoldArray[i];
        }

    }

    // Use this for initialization
    void Start()
    {
        randomizedMap = new Vector2[32 * 32];
        RandomizeMap();

        if (PlayerPrefs.GetString("cannonVisited") == "false")
        {
            InitializeTargetShip();
        }

    }

    //set target ship positions
    public void InitializeTargetShip()
    {
        int index = 0;
        for (int i = 0; i < tarShipList.Length; ++i)
        {
            player.playerMap[(int)randomizedMap[i].x, (int)randomizedMap[i].y] = new TileSprite("tsShip", tsSprite, Tiles.targetShip, Vector2.zero);
            tarShipList[index].transform.position = new Vector3(randomizedMap[i].y * tE.tileSize, -randomizedMap[i].x * tE.tileSize, -5);
            tarShipList[index].GetComponent<SpriteRenderer>().sprite = targetShipSprites[i];

            ++index;

            if (index == gameLoop.MAX_PLAYERS)
                return;

        }
    }

    //randomize map locations to put target ships at
    public void RandomizeMap()
    {

        int i = 0;
        //make a list of only water tiles
        for (var y = 0; y < tE.MapSize.y; y++)
        {
            for (var x = 0; x < tE.MapSize.x; x++)
            {
                if (tE.terrainMap[x, y].TileType == Tiles.shoal || tE.terrainMap[x, y].TileType == Tiles.ocean || tE.terrainMap[x, y].TileType == Tiles.deepblue)
                {
                    randomizedMap[i] = new Vector2(y, x);
                    ++i;

                }
            }
        }

        for (int j = 0; j < 6; ++j)
        {
            do
            {
                randomizedMap[j] = randomizedMap[Random.Range(0, randomizedMap.Length)];

            } while (CheckDuplicateLocation(randomizedMap[j], j) || randomizedMap[j] == Vector2.zero);
        }

    }

    //check if we have target ships wanting to go to the same spot
    bool CheckDuplicateLocation(Vector2 key, int size)
    {
        for (int j = 0; j < size; ++j)
        {
            if (randomizedMap[j] == key)
            {
                return true;
            }

        }
        return false;
    }

    //check if a targetship position is the same as another ship or a player
    bool CheckPositionCollision(Collision2D tarShip)
    {

        foreach (var targetShip in gameLoop.targetShips.tarShipList)
        {
            if (targetShip.transform.position.x == tarShip.transform.position.x
                && targetShip.transform.position.y == tarShip.transform.position.y
                && targetShip != tarShip.gameObject.GetComponent<TargetShip>() as TargetShip)
            {
                return true;
            }
        }

        foreach (var player in gameLoop.players.playersList)
        {
            if (player.transform.position.x == tarShip.transform.position.x
                && player.transform.position.y == tarShip.transform.position.y)
            {
                return true;
            }
        }

        return false;
    }

    //reposition a target ship
    public void RepositionShip(Collision2D tarShip)
    {
        var prevPos = tarShip.transform.position;
        do
        {
            var posX = Random.Range(0, 32);
            var posY = Random.Range(0, 32);
            if (tE.terrainMap[posX, posY].TileType == Tiles.shoal || tE.terrainMap[posX, posY].TileType == Tiles.ocean || tE.terrainMap[posX, posY].TileType == Tiles.deepblue)
            {
                tarShip.transform.position = new Vector3(posX * tE.tileSize, -posY * tE.tileSize, -5);
            }

        } while (CheckPositionCollision(tarShip) && tarShip.transform.position != prevPos);

    }

}
