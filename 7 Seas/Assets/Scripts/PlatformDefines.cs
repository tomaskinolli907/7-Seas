using UnityEngine;
using System.Collections;

public class PlatformDefines : MonoBehaviour
{
    void Start()
    {
        var qualityLevel = QualitySettings.GetQualityLevel();

#if UNITY_EDITOR
        Debug.Log("Unity Editor");
#endif

#if UNITY_ANDROID
        Debug.Log("Android");
        Debug.Log(qualityLevel);
        Application.targetFrameRate = 30;

        QualitySettings.vSyncCount = 0;

        QualitySettings.antiAliasing = 0;

        QualitySettings.SetQualityLevel(2);
        QualitySettings.shadowCascades = 2;
        QualitySettings.shadowDistance = 70;

        if (qualityLevel == 0)
        {
            QualitySettings.shadowCascades = 0;
            QualitySettings.shadowDistance = 15;
        }

        else if (qualityLevel == 5)
        {
            QualitySettings.shadowCascades = 2;
            QualitySettings.shadowDistance = 70;
            //QualitySettings.SetQualityLevel(3);
        }

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
#endif

#if UNITY_IOS
        Debug.Log("Iphone");
#endif

#if UNITY_STANDALONE_OSX
        Debug.Log("Stand Alone OSX");
#endif

#if UNITY_STANDALONE_WIN
        Debug.Log("Stand Alone Windows");
#endif

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            QualitySettings.SetQualityLevel(0);

        else if (Input.GetKeyDown(KeyCode.Alpha1))
            QualitySettings.SetQualityLevel(1);

        else if (Input.GetKeyDown(KeyCode.Alpha2))
            QualitySettings.SetQualityLevel(2);

        else if (Input.GetKeyDown(KeyCode.Alpha3))
            QualitySettings.SetQualityLevel(3);

        else if (Input.GetKeyDown(KeyCode.Alpha4))
            QualitySettings.SetQualityLevel(4);

        else if (Input.GetKeyDown(KeyCode.Alpha5))
            QualitySettings.SetQualityLevel(5);

    }
}