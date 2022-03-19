using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ports
{
    public Sprite portSprite;
    public Color playerColor;

    public Ports()
    {
        //portSprite = new Sprite();
        playerColor = Color.white;
    }

    public Ports(Sprite PortSprite, Color PlayerColor)
    {
        portSprite = PortSprite;
        playerColor = PlayerColor;
    }

}
