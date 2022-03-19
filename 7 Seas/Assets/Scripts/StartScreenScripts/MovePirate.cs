using System.Collections;
using UnityEngine;

public class MovePirate : MonoBehaviour
{
    public Transform ship;

    private Vector3 origPos;
    private bool wait;

    // Start is called before the first frame update
    void Start()
    {
        origPos = ship.position;

        ScreenWait();
    }

    // Update is called once per frame
    void Update()
    {
        if (ship.position.z >= 15 && !wait)
        {
            ship.position = origPos;

            ScreenWait();
        }
        else
        {
            ship.position = new Vector3(ship.position.x + 0.02f, ship.position.y, ship.position.z + 0.02f);
        }
    }

    private IEnumerator ScreenWait()
    {
        wait = true;

        yield return new WaitForSeconds(10);

        wait = false;
    }
}
