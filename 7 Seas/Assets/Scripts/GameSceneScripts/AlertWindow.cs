using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertWindow : MonoBehaviour {

    public GameObject alertWindow;
    public Text alertWindowText;
    public LoseTurn loseturn;
    public GameObject winWindow;

    public void ShowAlertWindow(string message)
    {
        //if the lose turn window isnt up nor the win window then we can show the alert window
        if (!loseturn.loseTurnWindow.activeSelf && !winWindow.activeSelf)
        {
            alertWindow.SetActive(true);
            alertWindowText.text = message;
        }
    }

    public void HideAlertWindow()
    {
        alertWindow.SetActive(false);
    }
}
