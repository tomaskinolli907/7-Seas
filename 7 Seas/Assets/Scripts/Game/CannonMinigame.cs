using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonMinigame : MonoBehaviour
{
    public static GameObject[] ships = new GameObject[2];
    public static int currShip = 1;
    public Material[] skyBox;
    public GameObject ButtonManager;
    public GameObject SceneManager;
    public GameObject[] targets;
    public GameObject treasureShip;
    public Slider healthSlider;
    public Color healthColor;

    public static bool setPlayer;
    public static bool setTreasure;
    public static bool setMonster;


    static GameObject currTreasureShip;
    static GameObject monster;
    GameObject target;

    void Start()
    {
        ships[0].AddComponent<ship_movement>();
        ships[1].AddComponent<ship_movement>();

        ships[0].GetComponent<ship_movement>().height = 1010;
        ships[1].GetComponent<ship_movement>().height = 1010;
    }

    void Update()
    {
        if (setPlayer && currShip == 1)
        {
            setPlayer = false;

            currShip++;

            target = Instantiate(targets[0], ships[0].transform.GetChild(0));

            target.SetActive(true);

            ButtonManager.GetComponent<ButtonFunctionality>().SetEnemy(ships[0]);

            ships[1].SetActive(false);

            PlayerPrefs.SetString("Enemy", "Player");

            RenderSettings.skybox = skyBox[0];
        }
        else if (setPlayer && currShip == 2)
        {
            setPlayer = false;

            currShip++;

            target = Instantiate(targets[0], ships[1].transform.GetChild(0));

            target.SetActive(true);

            ButtonManager.GetComponent<ButtonFunctionality>().SetEnemy(ships[1]);

            ships[0].SetActive(false);

            RenderSettings.skybox = skyBox[0];
        }
        else if (setTreasure)
        {
            setTreasure = false;

            currTreasureShip = Instantiate(treasureShip);

            currTreasureShip.SetActive(true);

            ButtonManager.GetComponent<ButtonFunctionality>().SetEnemy(currTreasureShip);

            RenderSettings.skybox = skyBox[0];
        }
        else
        {
            if (setMonster)
            {
                setMonster = false;

                healthSlider.fillRect.GetComponent<Image>().color = healthColor;
                healthSlider.gameObject.SetActive(true);

                monster.SetActive(true);

                monster.GetComponent<Monstermovement>().slider = healthSlider;

                ButtonManager.GetComponent<ButtonFunctionality>().SetEnemy(monster);

                RenderSettings.skybox = skyBox[1];
            }
        }
    }

    public static void SetShips(GameObject enemyShip, GameObject playerShip)
    {
        DestroyObjects();

        ships[0] = Instantiate(enemyShip);
        ships[1] = Instantiate(playerShip);

        ships[0].transform.GetChild(0).transform.rotation = Quaternion.Euler(Vector3.zero);
        ships[1].transform.GetChild(0).transform.rotation = Quaternion.Euler(Vector3.zero);

        ships[0].transform.position = new Vector3(0, 1010, 0);
        ships[1].transform.position = new Vector3(0, 1010, 0);
    }

    public static void SetMonster(GameObject gameMonster)
    {
        monster = Instantiate(gameMonster);

        monster.transform.position = new Vector3(-50, 1010, 15);
    }

    public static void DestroyObjects()
    {
        Destroy(ships[0]);
        Destroy(ships[1]);
        Destroy(currTreasureShip);
        Destroy(monster);
    }

    public static void ChangeShips()
    {
        Destroy(ships[0]);

        ships[1].SetActive(true);
    }

    public void CheckEmptyHealthBar()
    {
        if (healthSlider.value == 0)
        {
            Color color = healthSlider.fillRect.GetComponent<Image>().color;
            color = new Color(color.r, color.g, color.b, 0);
        }
    }
}
