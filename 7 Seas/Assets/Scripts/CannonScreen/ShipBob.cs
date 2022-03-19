using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBob : MonoBehaviour
{

    float prevY;
    public float floatSpeed = .5f;
    public float floatStrength = 24f;

    void Start()
    {
        prevY = transform.position.y;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x,
            prevY + ((float)Mathf.Sin(Time.time *floatSpeed)/floatStrength),
            transform.position.z);
    }



}
