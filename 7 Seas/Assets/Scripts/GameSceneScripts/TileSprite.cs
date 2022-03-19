using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class TileSprite
{
    public string Name;
    public Sprite TileImage;
    public Tiles TileType;
    public Vector2 TileLocation;
    public Color TileColor;


    public TileSprite()
    {
        Name = "Unset";
        
        TileType = Tiles.none;
        TileLocation = Vector2.zero;
        TileColor = Color.white;
    }

    //code to initialize tile aspects to the tile sprites
    public TileSprite(string name, Sprite image, Tiles tile, Vector2 location)
    {
        Name = name;
        TileImage = image;
        TileType = tile;
        TileLocation = location;
    }

    

}
