using UnityEngine;
using System.Collections;

public class TargetShip : MonoBehaviour {

    public string Name;
    public Sprite[] tarShipImage;
    //public Tiles TileType;
    public Vector2 tarShipLocation;
    //public Renderer renderer;
    public int gold = 10;

    public TargetShip()
    {
        Name = "Unset";
      
    }

    public TargetShip(string name, Sprite image, Vector2 location, int Gold)
    {
        Name = name;
        
        // TileType = tile;
        tarShipLocation = location;
        gold = Gold;
    }


}
