using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[System.Serializable]
public class InventorySystem : MonoBehaviour
{
    public Text kegText;
    public Text cbText;
    public GameLoop gameLoop;
    public List<Inventory> NumberDice;
    public List<Inventory> ResourceDice;
    public List<Inventory> WindDice;
    public List<Inventory> TarShipDice;
    public List<Inventory> ParrotDice;



    public GameObject rD;



    public void addResourceInventory()
    {
        Sprite s = rD.GetComponent<Image>().sprite;
        var currentPlayer = gameLoop.players.playersList[gameLoop.playersTurn - 1];
        //loop through resource dice inventory list
        foreach (var item in ResourceDice)
        {
            //if list image matches dice image
            if (s.name == item.Image.name)
            {
                //add the keg and canon amount to player inventory
                currentPlayer.powderKegs += item.KegAmnt;
                currentPlayer.canonBalls += item.CanonBallAmnt;
                kegText.text = "Kegs: " + currentPlayer.powderKegs;
                cbText.text = "CBs: " + currentPlayer.canonBalls;
                break;
            }

        }
    }



}
