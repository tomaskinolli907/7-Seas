using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FCP
{
    public class Toggle : MonoBehaviour
    {
        public GameObject cannon1;
        public GameObject cannon2;

        // Update is called once per frame
        /*void Update()
        {
            if(Input.GetKeyDown(KeyCode.K))
            {
                GetComponent<Outline>().enabled = !GetComponent<Outline>().enabled;
            }
        }*/

        private void OnMouseDown()
        {
            cannon1.GetComponent<Outline>().enabled = false;
            cannon2.GetComponent<Outline>().enabled = false;
            GetComponent<Outline>().enabled = !GetComponent<Outline>().enabled;
        }
    }
}