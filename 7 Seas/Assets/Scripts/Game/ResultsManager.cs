using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsManager : MonoBehaviour
{
    CannonLoader cannonLoader;

    // Start is called before the first frame update
    void Start()
    {
        cannonLoader = new CannonLoader();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitResults()
    {
        cannonLoader.ExitResultsScene();
    }
}
