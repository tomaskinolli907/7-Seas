using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCombatTarget : MonoBehaviour
{
    public void MoveTargetToRandomPosition()
    {
        transform.localPosition = new Vector3(1.5f, Random.Range(1f, 4f), Random.Range(-2f, 2f));
    }
}
