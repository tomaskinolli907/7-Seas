using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TilingEngine : MonoBehaviour
{
    public int parrotFrequency = 50;

    public List<TileSprite> TileSprites;
    public Vector2 MapSize;
    public Sprite DefaultImage;
    public GameObject TerrainTileContainerPrefab;
    public GameObject CircleTileContainerPrefab;
    public GameObject HighlightTileContainerPrefab;
    public GameObject TilePrefab;
    public GameObject lighthouseLightPrefab;
    public List<GameObject> lighthouseLights = new List<GameObject>();
    //public LoadMap loadMap;
    string mapText;
    public Vector2 CurrentPosition;
    public Vector2 ViewPortSize;
    public float tileSize;
    //public SpriteRenderer renderer;

    public TileSprite[,] terrainMap;
    public TileSprite[,] circleMap;
    public TileSprite[,] tileHighlights;
    public Sprite tileHighlight;
    private GameObject controller;

    //public List<GameObject> _tiles = new List<GameObject>();
    public GameObject[,] _tiles;
    Color[] tileColors = new Color[8];
    //Vector2 tileLocation;
    public List<GameObject> circlesList = new List<GameObject>();
    public List<GameObject> highlightTileList = new List<GameObject>();
    public DiceManager diceManager;
    public List<GameObject> volcanoList;
    public Sprite[,] parrotsArray = new Sprite[32,32];

    public GameObject diceList;

    public void Start()
    {
        MapSize = new Vector2(32, 32);
        _tiles = new GameObject[(int)MapSize.x, (int)MapSize.y];
        controller = GameObject.Find("TilingEngine");
        terrainMap = new TileSprite[(int)MapSize.x, (int)MapSize.y];
        circleMap = new TileSprite[(int)MapSize.x, (int)MapSize.y];
        tileHighlights = new TileSprite[(int)MapSize.x, (int)MapSize.y];

        mapText = controller.GetComponent<LoadMap>().text;

        //set all the colors
        tileColors[0] = new Color32(255,204,0,255);
        tileColors[1] = new Color32(0,204,255, 255);
        tileColors[2] = new Color32(204,102,51, 255);
        tileColors[3] = new Color32(51,204,102, 255);
        tileColors[4] = new Color32(255,102,51, 255);
        tileColors[5] = new Color32(204,51,255, 255);
        tileColors[6] = new Color32(255, 0, 0, 255);
        tileColors[7] = new Color32(255,255,255, 255);

        //DefaultTiles();
        SetTiles();
        AddTilesToWorld();

        //diceList = GameObject.Find("DiceList");
    }

    private void DefaultTiles()
    {
        for (var y = 0; y < MapSize.y; y++)
        {
            for (var x = 0; x < MapSize.x; x++)
            {
                terrainMap[x, y] = new TileSprite("shoal", DefaultImage, Tiles.shoal, new Vector2(x,y));
            }
        }
    }

    private void SetTiles()
    {
        var index = 0;
        var num = 0;

        for (var y = 0; y < MapSize.y; y++)
        {
            for (var x = 0; x < MapSize.x; x++)
            {
                num = mapText[index] - '0';

                //ignore the new line character
                while (num < 0)
                {
                    index = index + 1;
                    num = mapText[index] - '0';
                }
                terrainMap[x, y] = new TileSprite(TileSprites[num].Name, TileSprites[num].TileImage, TileSprites[num].TileType, new Vector2(x, y));

                //circleMap[x, y] = new TileSprite(TileSprites[Random.Range(9, 18)].Name, TileSprites[Random.Range(9, 18)].TileImage, TileSprites[Random.Range(9, 18)].TileType, new Vector2(x, y));
                circleMap[x, y] = new TileSprite(TileSprites[9].Name, TileSprites[9].TileImage, Tiles.none, new Vector2(x, y));

                tileHighlights[x, y] = new TileSprite(TileSprites[16].Name, TileSprites[16].TileImage, Tiles.none, new Vector2(x, y));

                ++index;
            }
        }
    }

    private void AddTilesToWorld()
    {
        //create the terrain tile map
        for (var y = 0; y < MapSize.y; y++)
        {
            for (var x = 0; x < MapSize.x; x++)
            {
                var tX = x * tileSize;
                var tY = y * tileSize;


                //add tile prefabs and position
                var t = Instantiate(TilePrefab);
                t.isStatic = true;
                t.transform.position = new Vector3(tX, -tY, 0);
                t.transform.SetParent(TerrainTileContainerPrefab.transform);
                var renderer = t.GetComponent<SpriteRenderer>();
                renderer.sprite = terrainMap[x, y].TileImage;

                if (terrainMap[x, y].TileType == Tiles.reef ||
                    terrainMap[x, y].TileType == Tiles.land ||
                    terrainMap[x, y].TileType == Tiles.lighthhouse ||
                    terrainMap[x, y].TileType == Tiles.ports ||
                    terrainMap[x, y].TileType == Tiles.volcano )
                {
                    t.GetComponent<BoxCollider2D>().enabled = true;
                }

                _tiles[x, y] = t;

                //set lighthouse lights
                if(terrainMap[x,y].TileType == Tiles.lighthhouse)
                {
                    var l = Instantiate(lighthouseLightPrefab);
                    //l.SetActive(true);
                    l.transform.position = new Vector3(tX, -tY, l.transform.position.z);
                    lighthouseLights.Add(l);
                }


                //set volcano tiles
                if(terrainMap[x,y].TileType == Tiles.volcano)
                {
                    volcanoList.Add(t);
                }
                
            }
        }

        //create the circles tile map
        var masterDiceList = diceList.GetComponent<MasterDiceList>().diceClassList;
        for (var y = 0; y < MapSize.y; y++)
        {
            for (var x = 0; x < MapSize.x; x++)
            {
                var t = Instantiate(TilePrefab);
                t.transform.SetParent(CircleTileContainerPrefab.transform);
                var renderer = t.GetComponent<SpriteRenderer>();

                if (terrainMap[x, y].TileType == Tiles.shoal || terrainMap[x, y].TileType == Tiles.ocean || terrainMap[x, y].TileType == Tiles.deepblue
                    ||terrainMap[x,y].TileType == Tiles.burTreas)
                {
                    var tX = x * tileSize;
                    var tY = y * tileSize;

                    //exclude tile colors from going on buried treasure tile
                    if (terrainMap[x, y].TileType != Tiles.burTreas)
                    {
                        //add circle tile prefabs and position
                        TilePrefab.layer = LayerMask.NameToLayer("Default");
                        
                        t.transform.position = new Vector3(tX, -tY, -1);

                        //roll frequency and set parrot to square
                        if (Random.Range(0, 100) <= parrotFrequency)
                        {
                            renderer.sprite = masterDiceList[Random.Range(0, 8)].ChildDiceImage;
                            parrotsArray[x,y] = renderer.sprite;
                        }
                        //else fill in default white squares instead of parrots
                        else
                        {
                            renderer.sprite = DefaultImage;
                            parrotsArray[x, y] = renderer.sprite;
                        }

                        renderer.tag = "traversable";
                        circleMap[x, y].TileType = Tiles.circle;

                    }
                    else
                    {
                        renderer.sprite = DefaultImage;
                        parrotsArray[x, y] = renderer.sprite;
                    }

                    //add highlight tiles
                    var h = Instantiate(TilePrefab);
                    h.transform.position = new Vector3(tX, -tY, -1);
                    h.transform.SetParent(HighlightTileContainerPrefab.transform);
                    var hRenderer = h.GetComponent<SpriteRenderer>();
                    hRenderer.sprite = tileHighlights[x,y].TileImage;
                    hRenderer.tag = "traversable";
                    //var th = h.GetComponent<TileAspects>();
                    tileHighlights[x, y].TileType = Tiles.highLightTile;
                    //th.tile = tileHighlights[x, y];
                    highlightTileList.Add(h);
                }
                else
                {
                    renderer.sprite = DefaultImage;
                    parrotsArray[x, y] = renderer.sprite;
                }
                circlesList.Add(t);
            }

        }


    }


    private TileSprite FindTile(Tiles tile)
    {
        foreach (TileSprite tileSprite in TileSprites)
        {
            if (tileSprite.TileType == tile) return tileSprite;
        }
        return null;
    }
}
