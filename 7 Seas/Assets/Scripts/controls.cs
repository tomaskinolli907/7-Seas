using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controls : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    private int currentCol = 1;
    private int currentX = -23;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShiftRight()
    {
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x + 176, mainCamera.transform.position.y, -100);

        currentCol++;

        currentX += 16;

    }

}
