using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MovementHandler : MonoBehaviour
{
    private  List<PlayerC> players = PlayersManager.GetPlayers();
    private  Text objectivesText;
    private  Text windText;
    private  Text movesLeftText;
    private int moves = 0;
    private int originalMoves;

    private  GameObject rollDiceBTN;
    private  GameObject resetBTN;
    private  GameObject doneBTN;
    private  GameObject upBTN;
    private  GameObject rightBTN;
    private  GameObject downBTN;
    private  GameObject leftBTN;

    //private List<GameObject> ships = GameManager.ships;
    private List<GameObject> ships = new List<GameObject>();
    private GameObject activeShip;

    public GameObject WormRed;
    public GameObject WormBlack;
    public GameObject WormGold;
    public GameObject WormBlue;
    public GameObject TreasureShip1;
    public GameObject TreasureShip2;
    public GameObject TreasureShip3;
    public GameObject TreasureShip4;
    
    private  Vector3 originalShipPosition;
    private  Quaternion originalShipRotation;
    private  Vector3 previousPosition;
    private  Quaternion previousRotation;
    public  Text PlayerText;
    public  Text PlayerHealthText;
    public Text RedTeamGoldText;
    public Text BlueTeamGoldText;
    public Text MutinyText;
    public Camera activeCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        objectivesText = GameObject.Find("Objectives").GetComponent<Text>();
        windText = GameObject.Find("Wind").GetComponent<Text>();
        movesLeftText = GameObject.Find("MovesLeft").GetComponent<Text>();
        rollDiceBTN = GameObject.Find("RollDiceBTN");
        resetBTN = GameObject.Find("ResetMovementBTN");
        doneBTN = GameObject.Find("DoneBTN");
        upBTN = GameObject.Find("UpBTN");
        rightBTN = GameObject.Find("RightBTN");
        downBTN = GameObject.Find("DownBTN");
        leftBTN = GameObject.Find("LeftBTN");

        movesLeftText.enabled = false;
        resetBTN.GetComponent<Button>().interactable = false;
        doneBTN.GetComponent<Button>().interactable = false;
        upBTN.GetComponent<Button>().interactable = false;
        rightBTN.GetComponent<Button>().interactable = false;
        downBTN.GetComponent<Button>().interactable = false;
        leftBTN.GetComponent<Button>().interactable = false;

        
            for (int i = 0; i < 8; i++) //Go up to num players
            {
                string shipName = "P" + (i + 1);

                ships.Add(GameObject.Find(shipName));
            }

           


            objectivesText.text = "ROLL THE DICE TO MOVE!";

        GameManager.windStrength = Random.Range(0, 2);

            switch (GameManager.windStrength)
            {
                case 0:
                    windText.text = "WIND: CALM, NO BONUS MOVES";
                    break;
                case 1:
                    windText.text = "WIND: MILD, 1 BONUS MOVE";
                    break;
                case 2:
                    windText.text = "WIND: WILD, 2 BONUS MOVES";
                    break;
            }

        //windText.text = "WIND BLOWING " + directions[windDirection];

        if (GameManager.loaded)
        {//Second load and above
            activeShip = ships[GameManager.activeShipIndex];
            activeShip.GetComponent<MapEventHandler>().active = false;
            originalShipPosition = activeShip.transform.position;
            originalShipRotation = activeShip.transform.rotation;
            LoadMovementHandler();

            moves = 0;
            rollDiceBTN.GetComponent<Button>().interactable = false;
            resetBTN.GetComponent<Button>().interactable = false;
            doneBTN.GetComponent<Button>().interactable = true;

            if (GameManager.WormRedDead) WormRed.Destroy();
            else WormRed.GetComponent<BoxCollider>().enabled = true;
            if (GameManager.WormBlackDead) WormBlack.Destroy();
            else WormBlack.GetComponent<BoxCollider>().enabled = true;
            if (GameManager.WormGoldDead) WormGold.Destroy();
            else WormGold.GetComponent<BoxCollider>().enabled = true;
            if (GameManager.WormBlueDead) WormBlue.Destroy();
            else WormBlue.GetComponent<BoxCollider>().enabled = true;
            if (GameManager.TreasureShip1Dead) TreasureShip1.Destroy();
            else TreasureShip1.GetComponent<BoxCollider>().enabled = true;
            if (GameManager.TreasureShip2Dead) TreasureShip2.Destroy();
            else TreasureShip2.GetComponent<BoxCollider>().enabled = true;
            if (GameManager.TreasureShip3Dead) TreasureShip3.Destroy();
            else TreasureShip3.GetComponent<BoxCollider>().enabled = true;
            if (GameManager.TreasureShip4Dead) TreasureShip4.Destroy();
            else TreasureShip4.GetComponent<BoxCollider>().enabled = true;

            activeShip.GetComponent<MapEventHandler>().active = true;
        }
        else
        {//First time load
            GameManager.loaded = true;
            activeShip = ships[0];
            activeShip.GetComponent<MapEventHandler>().active = true;
            originalShipPosition = activeShip.transform.position;
            originalShipRotation = activeShip.transform.rotation;

            WormRed.GetComponent<BoxCollider>().enabled = true;
            WormBlack.GetComponent<BoxCollider>().enabled = true;
            WormGold.GetComponent<BoxCollider>().enabled = true;
            WormBlue.GetComponent<BoxCollider>().enabled = true;
            TreasureShip1.GetComponent<BoxCollider>().enabled = true;
            TreasureShip2.GetComponent<BoxCollider>().enabled = true;
            TreasureShip3.GetComponent<BoxCollider>().enabled = true;
            TreasureShip4.GetComponent<BoxCollider>().enabled = true;
        }

    }

    void SetUITextForPlayer()
    {
        PlayerText.text = "IT'S YOUR TURN, @!";
        PlayerHealthText.text = "HEALTH: @";
        RedTeamGoldText.text = "REDBEARD'S GOLD: " + GameManager.RedTeamGold.ToString();
        BlueTeamGoldText.text = "BLUEBEARD'S GOLD: " + GameManager.BlueTeamGold.ToString();
        MutinyText.text = "MUTINY: @%";

        PlayerText.text = PlayerText.text.Replace("@", players[GameManager.activeShipIndex].Name.ToUpper());
        PlayerHealthText.text = PlayerHealthText.text.Replace("@", players[GameManager.activeShipIndex].Health.ToString());
        MutinyText.text = MutinyText.text.Replace("@", players[GameManager.activeShipIndex].Mutiny.ToString());
        if (players[GameManager.activeShipIndex].Mutiny >= 50 && players[GameManager.activeShipIndex].Mutiny < 100)
            MutinyText.text += " -1 TO YER MOVES, GET TO THE FORT!";
        if (players[GameManager.activeShipIndex].Mutiny >= 100)
            MutinyText.text += " -2 TO YER MOVES, GET TO THE FORT!";

        //if (players[GameManager.activeShipIndex].Team == 1) // red team
        //{
        //    //TeamGoldText.text = TeamGoldText.text.Replace("@", PlayerPrefs.GetInt("RedTeamGold").ToString());
        //}
        //else
        //{
        //    //TeamGoldText.text = TeamGoldText.text.Replace("@", PlayerPrefs.GetInt("BlueTeamGold").ToString());
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (activeCamera != null)
        {
            activeCamera.transform.position = new Vector3(activeShip.transform.position.x, 100, activeShip.transform.position.z);
            movesLeftText.text = moves.ToString();

            if (moves <= 0)
            {
                upBTN.GetComponent<Button>().interactable = false;
                rightBTN.GetComponent<Button>().interactable = false;
                downBTN.GetComponent<Button>().interactable = false;
                leftBTN.GetComponent<Button>().interactable = false;
            }

            if (activeShip.GetComponent<ShipCollisionHandler>().collidingTag == "OOB" || activeShip.GetComponent<ShipCollisionHandler>().collidingTag == "Obstacle")
            {
                activeShip.GetComponent<ShipCollisionHandler>().collidingTag = null;
                moves++;
                activeShip.transform.position = previousPosition;
                activeShip.transform.rotation = previousRotation;
                upBTN.GetComponent<Button>().interactable = true;
                rightBTN.GetComponent<Button>().interactable = true;
                downBTN.GetComponent<Button>().interactable = true;
                leftBTN.GetComponent<Button>().interactable = true;
            }

            if (    (activeShip.GetComponent<ShipCollisionHandler>().collidingTag == "RedFort" && players[GameManager.activeShipIndex].Team == 1) 
                 || (activeShip.GetComponent<ShipCollisionHandler>().collidingTag == "BlueFort" && players[GameManager.activeShipIndex].Team == 2))
            {
                activeShip.GetComponent<ShipCollisionHandler>().collidingTag = null;
                moves = 0;
                activeShip.transform.position = previousPosition;
                activeShip.transform.rotation = previousRotation;
                players[GameManager.activeShipIndex].Health += 20;
                if (players[GameManager.activeShipIndex].Health > 100)
                {
                    players[GameManager.activeShipIndex].Health = 100;
                }
                players[GameManager.activeShipIndex].Mutiny = 0;
                rollDiceBTN.GetComponent<Button>().interactable = false;
                resetBTN.GetComponent<Button>().interactable = false;
                doneBTN.GetComponent<Button>().interactable = true;
            }

            if(players[GameManager.activeShipIndex].Health <= 0)
            {
                objectivesText.text = "YER SHIP SUNK MATEY!";
                activeShip.transform.Translate(0f, -2f, 0f);
            }


            SetUITextForPlayer();

        }
    }

    public void SaveMovementHandler()
    {
        GameManager.shipPositions.Clear();
        GameManager.shipRotations.Clear();

        for(int i = 0; i < 8; i++)
        {
            GameManager.shipPositions.Add(ships[i].transform.position);
            GameManager.shipRotations.Add(ships[i].transform.rotation);
        }
    }

    public void LoadMovementHandler()
    {
        for (int i = 0; i < 8; i++)
        {
            ships[i].transform.position = GameManager.shipPositions[i];
            ships[i].transform.rotation = GameManager.shipRotations[i];
        }
    }

    public void RollDiceClick()
    {
        activeShip.GetComponent<MapEventHandler>().active = true;
        moves = Random.Range(1, 6);
        
        originalShipPosition = activeShip.transform.position;
        originalShipRotation = activeShip.transform.rotation;
        
        rollDiceBTN.GetComponent<Button>().interactable = false;
        resetBTN.SetActive(true);

        movesLeftText.enabled = true;
        resetBTN.GetComponent<Button>().interactable = true;
        doneBTN.GetComponent<Button>().interactable = true;
        upBTN.GetComponent<Button>().interactable = true;
        rightBTN.GetComponent<Button>().interactable = true;
        downBTN.GetComponent<Button>().interactable = true;
        leftBTN.GetComponent<Button>().interactable = true;

        if (players[GameManager.activeShipIndex].Mutiny >= 50 && players[GameManager.activeShipIndex].Mutiny < 100)
        {
            //Debug.Log("MUTINY OVER %)");
            moves--;
            MutinyText.text += " -1 TO YER MOVES, GET TO THE FORT!";
            if (moves < 1)
            {
                moves = 1;
            }
        }
        if (players[GameManager.activeShipIndex].Mutiny >= 100)
        {
            players[GameManager.activeShipIndex].Mutiny = 100;
            moves -= 2;
            if (moves < 1)
            {
                moves = 1;
            }
            MutinyText.text += " -2 TO YER MOVES, GET TO THE FORT!";
        }

        moves += GameManager.windStrength;

        originalMoves = moves;
        objectivesText.text = "STEAL THE OTHER TEAM'S GOLD!"; //Can add in the player's name here
    }

    public void ResetMovesClick()
    {
        moves = originalMoves;
        activeShip.transform.position = originalShipPosition;
        activeShip.transform.rotation = originalShipRotation;

        upBTN.GetComponent<Button>().interactable = true;
        rightBTN.GetComponent<Button>().interactable = true;
        downBTN.GetComponent<Button>().interactable = true;
        leftBTN.GetComponent<Button>().interactable = true;
    }

    public void DoneClick()
    {
        activeShip.GetComponent<MapEventHandler>().active = false;
        if (players[GameManager.activeShipIndex].Health <= 0)
        {
            if (players[GameManager.activeShipIndex].Team == 1) activeShip.transform.position = new Vector3(0f, 0f, 80f);
            else activeShip.transform.position = new Vector3(0f, 0f, -80f);
            players[GameManager.activeShipIndex].Mutiny = 20;
            players[GameManager.activeShipIndex].Health = 100;
        }
        objectivesText.text = "ROLL THE DICE TO MOVE!";

        players[GameManager.activeShipIndex].Position = activeShip.transform.position;
        players[GameManager.activeShipIndex].Rotation = activeShip.transform.rotation;

        GameManager.playersDone++;
        if(GameManager.playersDone >= 8)
        {//New round
            GameManager.playersDone = 0;
            GameManager.activeShipIndex = 0;
            GameManager.activeShipIndex = (GameManager.activeShipIndex) % 8;

            GameManager.windStrength = Random.Range(0, 2);

            switch (GameManager.windStrength)
            {
                case 0:
                    windText.text = "WIND: CALM, NO BONUS MOVES";
                    break;
                case 1:
                    windText.text = "WIND: MILD, 1 BONUS MOVE";
                    break;
                case 2:
                    windText.text = "WIND: WILD, 2 BONUS MOVES";
                    break;
            }

            foreach (PlayerC player in players)
            {
                player.Mutiny += 10;
                if (player.Mutiny > 100)
                {
                    player.Mutiny = 100;
                }
            }
        }
        else
        {
            GameManager.activeShipIndex = GameManager.activeShipIndex + 4;
            if (GameManager.activeShipIndex >= 8) GameManager.activeShipIndex = (GameManager.activeShipIndex % 8) + 1;
        }

        activeShip = ships[GameManager.activeShipIndex];
        activeShip.GetComponent<MapEventHandler>().active = true;


        movesLeftText.enabled = false;
        rollDiceBTN.GetComponent<Button>().interactable = true;
        resetBTN.GetComponent<Button>().interactable = false;
        doneBTN.GetComponent<Button>().interactable = false;
        upBTN.GetComponent<Button>().interactable = false;
        rightBTN.GetComponent<Button>().interactable = false;
        downBTN.GetComponent<Button>().interactable = false;
        leftBTN.GetComponent<Button>().interactable = false;

    }

    public void UpClick()
    {
        moves--;

        previousPosition = activeShip.transform.position;
        previousRotation = activeShip.transform.rotation;

        activeShip.transform.position = new Vector3(activeShip.transform.position.x,
            activeShip.transform.position.y, activeShip.transform.position.z + 10);

        activeShip.transform.eulerAngles = new Vector3(0, 0, 0);
        PlayerPrefs.SetString("lastMove", "up");
        PlayerPrefs.Save();
    }
    public void RightClick()
    {
        moves--;

        previousPosition = activeShip.transform.position;
        previousRotation = activeShip.transform.rotation;

        activeShip.transform.position = new Vector3(activeShip.transform.position.x + 10,
            activeShip.transform.position.y, activeShip.transform.position.z);

        activeShip.transform.eulerAngles = new Vector3(0, 90, 0);
        PlayerPrefs.SetString("lastMove", "right");
        PlayerPrefs.Save();
    }
    public void LeftClick()
    {
        moves--;

        previousPosition = activeShip.transform.position;
        previousRotation = activeShip.transform.rotation;

        activeShip.transform.position = new Vector3(activeShip.transform.position.x - 10,
            activeShip.transform.position.y, activeShip.transform.position.z);

        activeShip.transform.eulerAngles = new Vector3(0, 270, 0);
        PlayerPrefs.SetString("lastMove", "left");
        PlayerPrefs.Save();
    }
    public void DownClick()
    {
        moves--;

        previousPosition = activeShip.transform.position;
        previousRotation = activeShip.transform.rotation;

        activeShip.transform.position = new Vector3(activeShip.transform.position.x,
            activeShip.transform.position.y, activeShip.transform.position.z - 10);

        activeShip.transform.eulerAngles = new Vector3(0, 180, 0);
        PlayerPrefs.SetString("lastMove", "down");
        PlayerPrefs.Save();
    }
}
