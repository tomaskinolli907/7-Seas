using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Creator : MonoBehaviour
{
    public Transform pos;
    public GameObject ship;
    // Start is called before the first frame update
    void Start()
    {
        FileStream F = new FileStream("Num of players.txt", FileMode.Open, FileAccess.Read);
        int players = (int)F.ReadByte();
        Debug.Log(players);
        for(int i = 0; i<players; i++)
        {
            Instantiate(ship, new Vector3(pos.position.x+(400f*i),pos.position.y, pos.position.z), pos.rotation);
            GameObject.Find("Player 1(Clone)").name = "Player " + (i + 1);
        }
    }

    // Update is called once per frame
    
}
