using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory{

    //public Dictionary<int, string> dict = new Dictionary<int, string>();
    public string Name;
    public Sprite Image;
    public Color Color;
    public int KegAmnt;
    public int CanonBallAmnt;
    public int WindAmnt;
    public int MovementAmnt;

    public Inventory()
    {
        Name = "Unset";
        //Image = Sprite();
        //Image = new Sprite();
        Color = Color.white;
        KegAmnt = 0;
        CanonBallAmnt = 0;
        WindAmnt = 0;
    }

    public Inventory(string name, Sprite image, Color color, int kegAmnt, int canonBallAmnt, int windAmnt)
    {
        Name = name;
        Image = image;
        KegAmnt = kegAmnt;
        CanonBallAmnt = canonBallAmnt;
        WindAmnt = windAmnt;
    }
}
