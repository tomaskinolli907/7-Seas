using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighthouseLights : MonoBehaviour
{

    //public Light lighthouseLight;
    public TilingEngine tE;
    public List<GameObject> lighthouseLights;
    public Texture2D cookieHardLight;
    public TimeOfDay timeOfDay;
    Light lightComponent;
    public Shader shader1;
    public Shader shader2;
    public Shader shader3;

    // Use this for initialization
    void Start()
    {
        lighthouseLights = tE.lighthouseLights;

        foreach (var lighthouseLight in lighthouseLights)
        {
            //lighthouseLight.SetActive(true);
            lightComponent = lighthouseLight.GetComponent<Light>();
            //lightComponent.enabled = true;
            lightComponent.type = LightType.Spot;
            lightComponent.intensity = 0f;
            //lightComponent.cookie = cookieHardLight;
            lightComponent.spotAngle = 13.0f;
        }
    }

    public void ChangeLhLight()
    {
        foreach (var lighthouseLight in lighthouseLights)
        {
            lightComponent = lighthouseLight.GetComponent<Light>();
            //lightComponent.intensity = 3.0f * timeOfDay.times[timeOfDay.currentTime];

            //light up the lighthouses
            var posX = (int)(lightComponent.transform.position.x / 2f);
            var posY = (int)(lightComponent.transform.position.y / -2f);

            //in a square around the lighthouse light the tiles
            for (int x = -1; x <= 1; ++x)
            {
                for (int y = -1; y <= 1; ++y)
                {
                    tE._tiles[posX + x, posY + y].GetComponent<Renderer>().material.shader = shader2;

                }
            }

            //light up the remaining spots to create a diamond shape
            tE._tiles[(int)Mathf.Clamp(posX + 2, 0, 31), posY].GetComponent<Renderer>().material.shader = shader2;
            tE._tiles[(int)Mathf.Clamp(posX - 2, 0, 31), posY].GetComponent<Renderer>().material.shader = shader2;
            tE._tiles[posX, (int)Mathf.Clamp(posY + 2,0,31)].GetComponent<Renderer>().material.shader = shader2;
            tE._tiles[posX, (int)Mathf.Clamp(posY - 2, 0, 31)].GetComponent<Renderer>().material.shader = shader2;


            //adjust the light intensity based on the time of day light
            if (timeOfDay.currentTime == 3)
            {
                lightComponent.intensity = 4.5f;
                lightComponent.spotAngle = 22.5f;
            }
            else
                lightComponent.intensity = 0f;
        }

    }







}
