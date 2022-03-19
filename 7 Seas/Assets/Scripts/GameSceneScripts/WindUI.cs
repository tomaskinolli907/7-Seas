using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WindUI{

    public Sprite Image;
    public int windAmnt;

    public WindUI()
    {
       
        windAmnt = 0;
    }

    public WindUI(Sprite image, int windamnt)
    {
        Image = image;
        windAmnt = windamnt;
    }
}
