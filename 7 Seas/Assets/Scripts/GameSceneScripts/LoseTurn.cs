using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseTurn : MonoBehaviour
{
    public GameObject loseTurnWindow;
    public GameLoop gameLoop;
    Player currentPlayer;
    public RoundEvents roundEvents;
    public GameObject endTurnButton;
    public GameObject pirateWheel;
    public DiceManager diceManager;
    bool flag = false;
    public Text loseWindowText;
    public GameObject winWindow;
    public GameObject alertWindow;

    //shows turn loss window if win window isnt up
    //if alert window is already up, hide it
    public void ShowWindow(string message)
    {
        if (!winWindow.activeSelf)
        {
            endTurnButton.SetActive(false);
            loseTurnWindow.SetActive(true);
            loseWindowText.text = message;
        }
        if(alertWindow.activeSelf)
        {
            alertWindow.SetActive(false);
        }
    }

    //hides window
    public void HideWindow()
    {
        if(!gameLoop.pirateWheel.activeSelf)
            endTurnButton.SetActive(true);

        loseTurnWindow.SetActive(false);
    }

    //if current player lost his turn set flag bool to true
    public void CheckTurnLoss()
    {
        currentPlayer = gameLoop.players.playersList[gameLoop.playersTurn - 1];
        flag = true;
    }

    //if current player has lost his turn, display the loss turn window with the proper message based on the event
    private void Update()
    {
        if(flag)
        {
            if(Vector2.Distance(currentPlayer.transform.position, Camera.main.transform.position) < 10
                && !pirateWheel.activeSelf)
            {
                if(diceManager.ghostDiceTotal == 3)
                {
                    ShowWindow("Lose Turn, 3 Ghost Dice!");
                    flag = false;
                }

                else if (roundEvents.currentEvent == "Lose Turn Shoal")
                {
                    if (currentPlayer.shipOn == 0)
                    {
                        ShowWindow(roundEvents.currentEvent);
                        flag = false;
                    }
                }
                else if (roundEvents.currentEvent == "Lose Turn Ocean")
                {
                    if (currentPlayer.shipOn == 1)
                    {
                        ShowWindow(roundEvents.currentEvent);
                        flag = false;
                    }
                }
                else if (roundEvents.currentEvent == "Lose Turn Deep Blue")
                {
                    if (currentPlayer.shipOn == 2)
                    {
                        ShowWindow(roundEvents.currentEvent);
                        flag = false;
                    }
                }
            }
        }
    }


}
