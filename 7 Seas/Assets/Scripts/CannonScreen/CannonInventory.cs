using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonInventory : MonoBehaviour
{
    public Text kegamount;
    public Text CannonBallAmount;
    public GameObject targetShip;
    public Text WinOrLoseText;
    public GameObject WinOrLoseWin;
    bool flag = false;
    int  playerNum;
   public int kegs;
   public int Cannon;
    public int treasures = 10;
    int playerGold;
    void Start()
    {
        //WinOrLoseWin.SetActive(false); // sets it invisible.
        HideWindow();
        FindKegAmnt();
        FindCannonBallAmount();

        treasures = PlayerPrefs.GetInt("collidedShipGold");
    }

    void FindKegAmnt()
    {

       int playerCount =  PlayerPrefs.GetInt("PlayerCount");
        bool playerTurn = false;
        //loop the number of the players.
        for (int i=0; i<playerCount; ++i)
        {
           playerTurn = ES2.Load<bool>(Application.persistentDataPath + "/playerIsMyTurn" + i);
            if(playerTurn)
            {
                playerNum = i; // which player is it.
                break;
            }
        }

        // get amount of kegs from the current player.
        kegs = ES2.Load<int>(Application.persistentDataPath + "/playerPK" + playerNum);
        kegamount.text = "Kegs: " + kegs.ToString();

    }

    void FindCannonBallAmount()
    {
        int PlayerCount = PlayerPrefs.GetInt("PlayerCount");
        bool PlayerTurn = false;
        for(int i =0; i<PlayerCount;++i)
        {
            PlayerTurn = ES2.Load<bool>(Application.persistentDataPath + "/playerIsMyTurn" + i);
            if(PlayerTurn)
            {
                playerNum = i;
                break;
            }
        }
        Cannon = ES2.Load<int>(Application.persistentDataPath + "/playerCB" + playerNum);
        CannonBallAmount.text = "CBs: " + Cannon.ToString();
    }

    void WinOrLoseWindow()
    {
        // if the player hits the target ship 
        if(targetShip.GetComponent<ShipCollision>().isHit && flag == false)
        {
            flag = true;
            playerGold = ES2.Load<int>(Application.persistentDataPath + "/playerGold" + playerNum);
            playerGold = playerGold + treasures;
            ES2.Save(playerGold, Application.persistentDataPath + "/playerGold" + playerNum);
            SaveInventory();
            WinOrLoseText.text = "You Win";
            WinOrLoseWin.SetActive(true);
    
         }
    }

    public void SaveInventory()
    {
        ES2.Save(kegs, Application.persistentDataPath + "/playerPK" + playerNum);
        ES2.Save(Cannon, Application.persistentDataPath + "/playerCB" + playerNum);
    }

    public void HideWindow()
    {
        WinOrLoseWin.SetActive(false);
        //Debug.Log("window is hidden");

    }

    public void CheckAmmo()
    {
        if (kegs<=0 || Cannon <=0)
        {
            Debug.Log("You are out of Ammo");
        }
    }

    void Update()
    {
        WinOrLoseWindow();
    }

	
}
