using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsManager : MonoBehaviour
{
    public GameObject ship;
    public GameObject[] resultsGUI;
    public static PlayerShip[] players = new PlayerShip[2];
    public static GameObject[] ships = new GameObject[2];
    CannonLoader cannonLoader;

    // Start is called before the first frame update
    void Start()
    {
        cannonLoader = new CannonLoader();

        if (PlayerPrefs.GetString("Enemy").Equals("Player"))
        {
            ships[0] = Instantiate(players[0].GetShip());
            ships[1] = Instantiate(players[0].GetShip());

            ships[0].transform.position = new Vector3(0, 1010, 0);
            ships[1].transform.position = new Vector3(0, 1010, 0);

            ships[0].transform.GetChild(0).transform.rotation = Quaternion.Euler(Vector3.zero);
            ships[1].transform.GetChild(0).transform.rotation = Quaternion.Euler(Vector3.zero);

            ships[0].SetActive(false);
            ships[1].SetActive(false);

            DisplayGUI(resultsGUI[0]);
        }
        else if (PlayerPrefs.GetString("Enemy").Equals("Treasure"))
        {
            ship.SetActive(true);

            DisplayGUI(resultsGUI[1]);
        }
        else
        {
            DisplayGUI(resultsGUI[2]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayGUI(GameObject GUI)
    {
        GUI.SetActive(true);
    }

    public void ExitResults()
    {
        Destroy(ships[0]);
        Destroy(ships[1]);

        ship.SetActive(false);

        cannonLoader.ExitResultsScene();
    }
}
