using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCheckZone : MonoBehaviour
{
    Vector3 diceVelocity;
    // Start is called before the first frame update
    void FixedUpdate()
    {
        diceVelocity = GenericDice.diceVelocity;
    }

    private void OnTriggerStay(Collider col)
    {
        if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f)
        {
            switch (col.gameObject.name)
            {
                case "SIDE1":
                    TextToScreen.diceNumber = 6;
                    break;

                case "SIDE2":
                    TextToScreen.diceNumber = 5;
                    break;

                case "SIDE3":
                    TextToScreen.diceNumber = 4;
                    break;

                case "SIDE4":
                    TextToScreen.diceNumber = 3;
                    break;

                case "SIDE5":
                    TextToScreen.diceNumber = 2;
                    break;

                case "SIDE6":
                    TextToScreen.diceNumber = 1;
                    break;
            }
                
            StartCoroutine(Impact());
        }
    }

    private IEnumerator Impact()
    {
        Explosion.impact = true;

        yield return new WaitForSeconds(1);

        Explosion.impact = false;
    }
}
