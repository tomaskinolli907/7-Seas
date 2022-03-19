using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public GameObject RedShip;
    public GameObject BlueShip;
    public Text WinText;
    private int RedScore;
    private int BlueScore;

    // Start is called before the first frame update
    void Start()
    {
        RedScore = GameManager.RedTeamGold;
        BlueScore = GameManager.BlueTeamGold;

        if (RedScore > BlueScore)
        {
            BlueShip.SetActive(false);
            RedShip.SetActive(true);
            WinText.text = WinText.text.Replace("@", "THE RED BEARDS");
        }
        else if (RedScore < BlueScore)
        {
            BlueShip.SetActive(true);
            RedShip.SetActive(false);
            WinText.text = WinText.text.Replace("@", "THE BLUE BEARDS");
        }
        else
        {
            BlueShip.SetActive(false);
            RedShip.SetActive(false);
            WinText.text = "'TIS A DRAW...BUT HOW?";
        }

        GameManager.ResetGameManager();
    }
}
