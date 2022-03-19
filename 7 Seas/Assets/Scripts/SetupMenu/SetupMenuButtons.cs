using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetupMenuButtons : MonoBehaviour
{
    //soundfx
    public AudioClip pirateSound;
    private AudioSource source;

    //denote we want to start level or other
    bool levelStarted;
    bool exitStarted;
    bool customStarted;
    bool returnToMenuStarted;

    //get camera
    public Camera mainCamera;

    // Use this for initialization
    void Start()
    {
        source = GameObject.Find("SoundFX").GetComponent<AudioSource>();
        levelStarted = false;
        exitStarted = true;
        customStarted = false;
    }

    public void PlayGame()
    {
        //if everything is set, load the game
        if (PlayerPrefs.GetInt("PlayerCount") >= 2 && PlayerPrefs.GetInt("TreasureAmount") > 0)
        {
            mainCamera.GetComponent<AudioSource>().enabled = false;
            source.PlayOneShot(pirateSound, 1.0f);
            levelStarted = true;
        }

        //else put an error message here that they need 2 or more players

    }

    public void CustomBuild()
    {
        mainCamera.GetComponent<AudioSource>().enabled = false;
        source.PlayOneShot(pirateSound, 1.0f);
        customStarted = true;
    }

    public void ReturnToMenu()
    {
        mainCamera.GetComponent<AudioSource>().enabled = false;
        source.PlayOneShot(pirateSound, 1.0f);
        returnToMenuStarted = true;
    }

    public void ExitGame()
    {
        mainCamera.GetComponent<AudioSource>().enabled = false;
        source.PlayOneShot(pirateSound, 1.0f);
        exitStarted = true;
        Application.Quit();
    }

    private void Update()
    {
        //load diff scenes after sound effects are finished
        if (!source.isPlaying && levelStarted)
        {
            SceneManager.LoadScene(2);
        }
        else if (!source.isPlaying && exitStarted)
        {
            Application.Quit();
        }
        else if (!source.isPlaying && customStarted)
        {
            SceneManager.LoadScene(3);
        }
        else if (!source.isPlaying && returnToMenuStarted)
        {
            SceneManager.LoadScene(1);
        }
    }


}
