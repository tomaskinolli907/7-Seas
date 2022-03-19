using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadFiles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string[] players = { "f", "f", "f", "f", "f", "f", "f", "f" };


        System.IO.File.WriteAllText(Application.persistentDataPath + "/Players.txt", string.Join("\n", players));
        System.IO.File.WriteAllText(Application.persistentDataPath + "/Difficulty.txt", "");
        for (int i = 1; i <= 8; i++)
        {
            System.IO.File.WriteAllText(Application.persistentDataPath + "/Player" + i + ".txt", "");
        }
        Debug.Log(Application.persistentDataPath);
    }

}
