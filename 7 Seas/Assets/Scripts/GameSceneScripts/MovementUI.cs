using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovementUI{

    public Sprite Image;
    public int MovementAmnt;

    public MovementUI()
    {
        //Image = Sprite();
        MovementAmnt = 0;
    }

    public MovementUI(Sprite image, int movementAmnt)
    {
        Image = image;
        MovementAmnt = movementAmnt;
    }
}
