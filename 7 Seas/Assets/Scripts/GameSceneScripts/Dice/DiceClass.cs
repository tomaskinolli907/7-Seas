using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DiceClass {
    public string Name;
    public Sprite DiceImage;
    public Sprite ChildDiceImage;
    //public Tiles TileType;
    //public Vector2 TileLocation;
    public Color DiceColor;

    public DiceClass(string name, Sprite diceImage, Sprite childDiceImage, Color diceColor)
    {
        Name = name;
        DiceImage = diceImage;
        ChildDiceImage = childDiceImage;
        DiceColor = diceColor;
    }


}
