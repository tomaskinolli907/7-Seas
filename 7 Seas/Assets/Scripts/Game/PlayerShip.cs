using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    float totalTreasure;
    float currTreasure;
    int playerNum;
    Vector3 currPosition;
    Vector3 prevPosition;

    public PlayerShip (int num)
    {
        playerNum = num;
        totalTreasure = 0;
        currTreasure = 0;
    }

    public int GetPlayerNum()
    {
        return playerNum;
    }

    public void SetTreasure(float amount)
    {
        currTreasure = amount;
    }

    public float GetTreasure()
    {
        return currTreasure;
    }

    public void DepositTreasure()
    {
        totalTreasure += currTreasure;

        currTreasure = 0;
    }

    public void SetCurrentPosition(Vector3 pos)
    {
        currPosition = pos;
    }

    public void SetPreviousPosition(Vector3 pos)
    {
        prevPosition = pos;
    }
}
