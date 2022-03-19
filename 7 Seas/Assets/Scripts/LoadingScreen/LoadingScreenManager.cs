using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour {

	public Image loadingIcon;
	public Image loadingDoneIcon;
	public Text loadingText;
	public Image progressBar;
	public Image fadeOverlay;

    //soundfx
    public static AudioClip pirateSound;
    public AudioSource source;
    public float time;

    public float waitOnLoadEnd = 0.25f;
	public float fadeDuration = 0.25f;
    public float timeLeft;

	public LoadSceneMode loadSceneMode = LoadSceneMode.Single;
	public ThreadPriority loadThreadPriority;

	public AudioListener audioListener;

	AsyncOperation operation;
	Scene currentScene;

	public static int sceneToLoad = -1;

	static int loadingSceneIndex = 4;

	public static void LoadScene(int levelNum, AudioClip audioclip)
    {
        pirateSound = audioclip;				
		Application.backgroundLoadingPriority = ThreadPriority.High;
		sceneToLoad = levelNum;
		//SceneManager.LoadScene(loadingSceneIndex);
		SceneManager.LoadScene(sceneToLoad);
	}

	void Start()
    {
        if (sceneToLoad < 0)
        {
            Application.Quit();
            return;
        }

        playSound();
        time = Time.time;
        fadeOverlay.gameObject.SetActive(true);
		currentScene = SceneManager.GetActiveScene();
		StartCoroutine(LoadAsync(sceneToLoad));

	}

    void playSound()
    {
        source.PlayOneShot(pirateSound, 1.0f);
    }

    private void Update()
    {
        var elapsedTime = Time.time - time;
        timeLeft = pirateSound.length - elapsedTime;
        if (timeLeft < 0)
            timeLeft = 0;
    }

    private IEnumerator LoadAsync(int levelNum) {
		ShowLoadingVisuals();

		yield return null;


        FadeIn();
		StartOperation(levelNum);

		float lastProgress = 0f;

		while (DoneLoading() == false) {
			yield return null;

			if (Mathf.Approximately(operation.progress, lastProgress) == false) {
				progressBar.fillAmount = 1 - operation.progress;
				lastProgress = operation.progress;
			}
		}

		if (loadSceneMode == LoadSceneMode.Additive)
			audioListener.enabled = false;

		ShowCompletionVisuals();

        //yield return new WaitForSeconds(waitOnLoadEnd);
        yield return new WaitForSeconds(timeLeft);

        FadeOut();

        yield return new WaitForSeconds(fadeDuration);


        if (loadSceneMode == LoadSceneMode.Additive)
            SceneManager.UnloadSceneAsync(currentScene.name);
        else
			operation.allowSceneActivation = true;
	}

	private void StartOperation(int levelNum) {
		Application.backgroundLoadingPriority = loadThreadPriority;
		operation = SceneManager.LoadSceneAsync(levelNum, loadSceneMode);


		if (loadSceneMode == LoadSceneMode.Single)
			operation.allowSceneActivation = false;
	}

	private bool DoneLoading() {
		return (loadSceneMode == LoadSceneMode.Additive && operation.isDone) || (loadSceneMode == LoadSceneMode.Single && operation.progress >= 0.9f); 
	}

	void FadeIn() {
		fadeOverlay.CrossFadeAlpha(0, fadeDuration, true);
	}

	void FadeOut() {
		fadeOverlay.CrossFadeAlpha(1, fadeDuration, true);
	}

	void ShowLoadingVisuals() {
		loadingIcon.gameObject.SetActive(true);
		loadingDoneIcon.gameObject.SetActive(false);

		progressBar.fillAmount = 1f;
		loadingText.text = "Loading...";
	}

	void ShowCompletionVisuals() {
		loadingIcon.gameObject.SetActive(false);
		loadingDoneIcon.gameObject.SetActive(true);

		progressBar.fillAmount = 0f;
		loadingText.text = "Loading Done";
	}

}