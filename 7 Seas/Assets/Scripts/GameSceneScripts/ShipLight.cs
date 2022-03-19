using UnityEngine;
using System.Collections;

public class ShipLight : MonoBehaviour {

    //Light shipLight;
    public Light shipLight;
    public float speed = 1.0f;
    public float angleSpeed = 1.0f;
    public Texture2D cookieNone;
    public Texture2D cookieHardLight;
    public GameObject darkness;
    public GameObject shipWheel;
    public GameLoop gameLoop;
    public TimeOfDay timeOfDay;

    // Use this for initialization
    void Start () {
        shipLight = GetComponent<Light>();
        shipLight.type = LightType.Spot;
        shipLight.cookie = cookieNone;
        //shipLight.type = LightType.Directional;
        shipLight.enabled = false;
        RenderSettings.ambientLight = Color.white;
        shipLight.intensity = 1.25f;
        shipLight.cookie = cookieHardLight;
        shipLight.spotAngle = 13.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {

        //if it's this players turn
        if (this.GetComponentInParent<Player>().isMyTurn)
        {
            shipLight.enabled = true;
            shipLight.intensity = 5.625f * timeOfDay.times[timeOfDay.currentTime];

            //some tests to increase and decrease the spotlight diameter, probably take out later
            if (Input.GetKey(KeyCode.W))
            {
                shipLight.enabled = true;
                shipLight.spotAngle += angleSpeed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                shipLight.enabled = true;
                shipLight.spotAngle -= angleSpeed;
            }


            //fog off 5x5
            if (Input.GetKeyDown(KeyCode.E))
            {
                RenderSettings.ambientLight = new Color32(50, 50, 50, 255);
                /*shipLight.cookie = cookieNone;
                shipLight.type = LightType.Directional;
                shipLight.intensity = 1.25f;*/

                shipLight.cookie = cookieHardLight;
                shipLight.type = LightType.Spot;
                shipLight.intensity = 4.5f;
                shipLight.spotAngle = 22.5f;
                shipWheel.transform.localScale = new Vector3(3.4f, 3.4f, 1);


            }
            //fog on 3x3
            if (Input.GetKeyDown(KeyCode.Q))
            {
                RenderSettings.ambientLight = Color.black;
                //shipLight.enabled = true;
                shipLight.cookie = cookieHardLight;
                shipLight.type = LightType.Spot;
                shipLight.intensity = 6f;
                shipLight.spotAngle = 13.0f;
                shipWheel.transform.localScale = new Vector3(2f, 2f, 1);
            }
            //completely light
            if (Input.GetKeyDown(KeyCode.R))
            {
                shipLight.enabled = false;
                RenderSettings.ambientLight = Color.white;
                shipLight.cookie = cookieHardLight;
                shipLight.enabled = false;
                shipLight.intensity = 1.25f;
                shipWheel.transform.localScale = new Vector3(2f, 2f, 1);
            }
        }
        //else turn off the light
        else
        {
            shipLight.enabled = false;
        }



    }


}
