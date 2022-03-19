using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizePlayerOrder : MonoBehaviour
{
    private T[] RandomizeOrder<T>(T[] players)
    {
        SortedList<float, T> playersSorted = new SortedList<float, T>(); // Create sorted list. ambiguous type T for now
        for (int i = 0; i < players.Length; i++)
        {
            playersSorted.Add(Random.value, players[i]); // Each player added with key between 0-1, list becomes sorted by this. 
        }
        return new List<T>(playersSorted.Values).ToArray(); // Return as an array
    }
}
