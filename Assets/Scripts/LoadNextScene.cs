using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    Scene currentScene;

    public Animator fadeTransition, musicFade;
    public float fadeHalfDuration = 1f;

    [Header("Timed Transition")]
    [SerializeField] private bool isTimedTransition = false;
    [SerializeField] private float transitionTimer;
    [SerializeField] private string transitionToScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }
    private void Update()
    {
        if (isTimedTransition)
        {
            if (transitionTimer <= 0)
                LoadSceneName(transitionToScene);

            transitionTimer -= Time.deltaTime;
        }
    }

    public void LoadNextSceneIndex()
    {
        StartCoroutine(FadeToScene(currentScene.buildIndex + 1));
    }

    public void LoadSceneName(string sceneName)
    {
        Debug.Log(sceneName);
        StartCoroutine(FadeToScene(sceneName));
    }

    public void LoadSceneIndex(int sceneIndex)
    {
        StartCoroutine(FadeToScene(sceneIndex));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator FadeToScene(int sceneIndex)
    {
        if (fadeTransition != null)
            fadeTransition.SetTrigger("Start");
        if (musicFade != null)
            musicFade.SetTrigger("StopAudio");

        yield return new WaitForSeconds(fadeHalfDuration);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private IEnumerator FadeToScene(string sceneName)
    {
        if (fadeTransition != null)
            fadeTransition.SetTrigger("Start");
        if (musicFade != null)
            musicFade.SetTrigger("StopAudio");

        yield return new WaitForSeconds(fadeHalfDuration);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
