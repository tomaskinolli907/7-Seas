using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour {

    //public Canvas quitMenu;
    public Button startText;
    public Button exitText;

    //soundfx
    public AudioClip pirateSound;
    private AudioSource source;

    //get camera
    public Camera mainCamera;

    //denote we want to start level
    bool levelStarted;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start ()
    {
        levelStarted = false;
        //quitMenu = quitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        //quitMenu.enabled = false;
	}
	
	public void ExitPress()
    {
        //quitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
    }

    public void NoPress()
    {
        //quitMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
    }

    public void StartLevel()
    {
        mainCamera.GetComponent<AudioSource>().enabled = false;
        source.PlayOneShot(pirateSound, 1.0f);
        levelStarted = true;


    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (!source.isPlaying && levelStarted)
        {
            SceneManager.LoadScene(1);
        }
    }
}
