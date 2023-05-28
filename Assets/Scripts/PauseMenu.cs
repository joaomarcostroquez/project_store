using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //Animator _animator;

    [SerializeField]
    GameObject pauseMenuUI;

    [SerializeField]
    LoadNextScene _loadNextScene;

    bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        //_animator = GetComponent<Animator>();

        pauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
        {
            if (isPaused)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
        //_animator.SetBool("isPaused", isPaused);
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Continue()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void UnpauseChangeScene(string sceneName)
    {
        Time.timeScale = 1;
        _loadNextScene.LoadSceneName(sceneName);
    }

    public void PauseButton()
    {
        if (isPaused)
            Continue();
        else
            Pause();
    }
}
