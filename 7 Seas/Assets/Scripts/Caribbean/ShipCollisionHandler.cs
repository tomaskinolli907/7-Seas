using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollisionHandler : MonoBehaviour
{
    public string collidingTag = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        collidingTag = other.tag;
    }
}
