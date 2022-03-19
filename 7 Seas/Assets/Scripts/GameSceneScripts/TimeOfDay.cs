using UnityEngine;
using System.Collections;

public class TimeOfDay : MonoBehaviour {


    public GameObject timeDay;

    public float[] times;

    public string time;
    public int currentTime;
    byte noonShade = 255 * 1;
    byte afternoonShade = 255/2;
    byte eveningShade = 255/4; 
    byte midnightShade = 255 *0;
    byte dawnShade = 255/4;
    byte morningShade = 255/2;


    // Use this for initialization
    void Start () {
        currentTime = 0;
        times = new float[6];
        times[0] = 0f;
        times[1] = .25f;
        times[2] = .50f;
        times[3] = .80f;
        times[4] = .50f;
        times[5] = .25f;
        RenderSettings.ambientLight = Color.white;
        time = "noon";
    }

    public void ChangeTimeOfDay(int timeOfDay)
    {
        //get current time and change the ambientlight accordingly
        currentTime = timeOfDay;
        if (timeOfDay == 0)
        {
            RenderSettings.ambientLight = new Color32(noonShade, noonShade, noonShade, 255);
            time = "noon";
        }
        else if (timeOfDay == 1)
        {
            RenderSettings.ambientLight = new Color32(afternoonShade, afternoonShade, afternoonShade, 255);
            time = "afternoon";
        }
        else if (timeOfDay == 2)
        {
            RenderSettings.ambientLight = new Color32(eveningShade, eveningShade, eveningShade, 255);
            time = "evening";
        }
        else if (timeOfDay == 3)
        {
            RenderSettings.ambientLight = new Color32(midnightShade, midnightShade, midnightShade, 255);
            time = "midnight";
        }
        else if (timeOfDay == 4)
        {
            RenderSettings.ambientLight = new Color32(dawnShade, dawnShade, dawnShade, 255);
            time = "dawn";
        }
        else if (timeOfDay == 5)
        {
            RenderSettings.ambientLight = new Color32(morningShade, morningShade, morningShade, 255);
            time = "morning";
        }
        else
            Debug.Log("Time of day ERROR!!!");
    }

}
