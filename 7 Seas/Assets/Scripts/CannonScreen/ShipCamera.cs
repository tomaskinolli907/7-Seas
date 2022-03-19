using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCamera : MonoBehaviour
{

    public GameObject ship;
    float prevTrans;
    private void Start()
    {
        prevTrans = transform.position.y;
    }

    private void FixedUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, prevTrans + ship.transform.eulerAngles.z/3, transform.position.z);
        //transform.eulerAngles = new Vector3(ship.transform.eulerAngles.z + transform.rotation.x, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
