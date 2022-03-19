using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    //public static List<GameObject> ships = new List<GameObject>();
    //private List<GameObject> ships;
    //public static GameObject activeShip;
    public static int activeShipIndex;
    public static bool loaded = false;
    public static int windStrength;
    public static int playersDone = 0;
    public static int RedTeamGold = 0;
    public static int BlueTeamGold = 0;
    public static bool WormRedDead = false;
    public static bool WormBlackDead = false;
    public static bool WormGoldDead = false;
    public static bool WormBlueDead = false;

    public static bool TreasureShip1Dead = false;
    public static bool TreasureShip2Dead = false;
    public static bool TreasureShip3Dead = false;
    public static bool TreasureShip4Dead = false;

    public static List<Vector3> shipPositions = new List<Vector3>();
    public static List<Quaternion> shipRotations = new List<Quaternion>();


    public static void ResetGameManager()
    {
        activeShipIndex = 0;
        loaded = false;
        windStrength = 0;
        playersDone = 0;
        RedTeamGold = 0;
        BlueTeamGold = 0;
        WormRedDead = false;
        WormBlackDead = false;
        WormGoldDead = false;
        WormBlueDead = false;

        TreasureShip1Dead = false;
        TreasureShip2Dead = false;
        TreasureShip3Dead = false;
        TreasureShip4Dead = false;

        shipPositions = new List<Vector3>();
        shipRotations = new List<Quaternion>();
    }
}