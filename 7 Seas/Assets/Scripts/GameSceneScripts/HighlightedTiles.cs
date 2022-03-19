using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightedTiles : MonoBehaviour
{

    public GameObject highlightedTile;
    public Transform playerTrans;
    public GameObject[] highlightedTiles = new GameObject[8];
    public GameLoop gameLoop;

    // Use this for initialization
    void Start()
    {
        highlightedTiles[0] = highlightedTile;
        for (int i = 1; i < 8; ++i)
        {
            highlightedTiles[i] = Instantiate(highlightedTile);
        }
        //playerTrans = this.GetComponentInParent<Transform>();	
        //HighlightTilePos();

    }

    //select the tiles around the current player to highlight
    void Update()
    {

        if (GetComponentInParent<Player>().isMyTurn)
        {
            int index = 0;
            for (int x = -1; x < 2; ++x)
            {
                for (int y = -1; y < 2; ++y)
                {
                    if (x != 0 || y != 0)
                    {
                        highlightedTiles[index].SetActive(true);
                        highlightedTiles[index].transform.position = new Vector3(playerTrans.transform.position.x + x * 2,
                                                 playerTrans.transform.position.y + y * 2,
                                                 highlightedTiles[index].transform.position.z);
                        ++index;
                    }

                }
            }
        }
        else
        {
            for(int i=0; i<8; ++i)
            {
                highlightedTiles[i].SetActive(false);
            }
        }




    }
}
