using UnityEngine;
using System.Collections;

public class TileAspects : MonoBehaviour {

    public TileSprite tile;
    public TilingEngine tE;
    //public GameObject player;
    public Players players;
    public GameLoop gameLoop;
    public Vector3 startPoint;
    public Vector3 endPoint;
    bool move;
    float timeStartedMoving;
    float timeTaken = .2f;
    Player pL;
    // Use this for initialization
    void Start () {
        move = false;
        //Debug.Log("player pos" + player.transform.position);
	}

    void FixedUpdate()
    {
        
        //if set to move, lerp between players start and end points
        if (move)
        {
            float timeSinceMoving = Time.time - timeStartedMoving;
            float percentComp = timeSinceMoving / timeTaken;
            pL.transform.position = Vector3.Lerp(startPoint, endPoint, percentComp);

            //if we're close enough, stop lerping
            if (percentComp >= 1.0f)
            {
                move = false;
            }

        }


    }

    //hide tiles we dont see
    void CullTiles()
    {
        var camVertSize = Camera.main.orthographicSize * 2;
        var camPosX = Camera.main.transform.position.x - Camera.main.orthographicSize - 1;
        var camPosY = Mathf.Abs(Camera.main.transform.position.y + Camera.main.orthographicSize - 1);

        //camera goes from x: 15 to 47 and y: -15 to -47
        for (var y = 0; y < tE.MapSize.y; y++)
        {
            for (var x = 0; x < tE.MapSize.x; x++)
            {
                tE._tiles[x, y].SetActive(false);

                if (x > Mathf.RoundToInt(camPosX) / 2 - 1 && x <= (Mathf.RoundToInt(camPosX) + camVertSize) / 2 + 1)
                {
                    if (y >= Mathf.RoundToInt(camPosY) / 2 - 1 && y <= (Mathf.RoundToInt(camPosY) + camVertSize) / 2 + 1)
                    {
                        tE._tiles[x, y].SetActive(true);

                    }
                }

            }

        }
    }

    //things to do when mouse is clicked
    void OnMouseDown()
    {
        pL = players.playersList[gameLoop.playersTurn - 1];
        Vector2 vect1 = new Vector2(pL.transform.position.x/tE.tileSize, -pL.transform.position.y/tE.tileSize);
        Vector2 vect2 = new Vector2(tile.TileLocation.x, tile.TileLocation.y);
        float dist = Vector2.Distance(vect2, vect1);
        //Debug.Log("distance of vects: " + dist);

        //if not already moving
        if (!move && Input.touchCount < 2)
        {
            //if tile to move to is a circle or buried treasure spot
            if (tile.TileType == Tiles.circle || tile.TileType == Tiles.burTreas || tile.TileType == Tiles.highLightTile)
            { 
                //and if tile destination is one away
                if (dist == 1f || dist == Mathf.Sqrt(2))
                {
                    //set stats for lerping to new tile
                    move = true;
                    timeStartedMoving = Time.time;
                    startPoint = pL.transform.position;
                    endPoint = new Vector3(tile.TileLocation.x * tE.tileSize, -tile.TileLocation.y * tE.tileSize, pL.transform.position.z);

                    //mark player's location
                    pL.shipPos = tile.TileLocation;

                    //mark what type of tile the player is on
                    pL.shipOn = (int)tE.terrainMap[(int)tile.TileLocation.x, (int)tile.TileLocation.y].TileType;

                    //rotate the player in the direction of the movement
                    float xDir = vect2.x - vect1.x;
                    float yDir = vect2.y - vect1.y;
                    var playerSprite = pL.GetComponentInChildren<SpriteRenderer>();
                    
                    if (xDir == 0 && yDir == -1)//north
                    {
                        playerSprite.transform.rotation = Quaternion.identity;
                        
                        playerSprite.transform.Rotate(0, 0, 90);
                    }
                    else if (xDir == 0 && yDir == 1)//south
                    {
                        playerSprite.transform.rotation = Quaternion.identity;
                        playerSprite.transform.Rotate(0, 0, -90);
                    }
                    else if (xDir == -1 && yDir == 0)//east
                    {
                        playerSprite.transform.rotation = Quaternion.identity;
                        playerSprite.transform.Rotate(0, 0, 180);
                    }
                    else if (xDir == 1 && yDir == 0)//west
                    {
                        playerSprite.transform.rotation = Quaternion.identity;
                        playerSprite.transform.Rotate(0, 0, 0);
                    }
                    else if (xDir == 1 && yDir == -1)//northeast
                    {
                        playerSprite.transform.rotation = Quaternion.identity;
                        playerSprite.transform.Rotate(0, 0, 45);
                    }
                    else if (xDir == -1 && yDir == 1)//southwest
                    {
                        playerSprite.transform.rotation = Quaternion.identity;
                        playerSprite.transform.Rotate(0, 0, 225);
                    }
                    else if (xDir == 1 && yDir == 1)//southeast
                    {
                        playerSprite.transform.rotation = Quaternion.identity;
                        playerSprite.transform.Rotate(0, 0, 315);
                    }
                    else if (xDir == -1 && yDir == -1)//northwest
                    {
                        playerSprite.transform.rotation = Quaternion.identity;
                        playerSprite.transform.Rotate(0, 0, 135);
                    }
                }
                //if went to buried treasure spot, then bury the treasure
                if(tile.TileType == Tiles.burTreas)
                {
                    pL.buriedTreasure += pL.gold;
                    pL.gold = 0;
                }
            }

            gameLoop.showAvailableMoves();

        }

        CullTiles();





    }

}
