using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LoadTargetScreenButton : MonoBehaviour
{
    public AudioClip audioclip;
    public void LoadSceneNum(int num)
    {
        if(num == -1)
        {
            Application.Quit();
        }
        else if(num < -2 || num >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("Can't load scene num " + num + ", SceneManager only has " + SceneManager.sceneCountInBuildSettings + " scenes in BuildSettings!");
            return;
        }
        
        LoadingScreenManager.LoadScene(num, audioclip);
    }
}
