using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCannonball : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cannonball"))
        {
            Destroy(other.gameObject);
        }
    }
}
