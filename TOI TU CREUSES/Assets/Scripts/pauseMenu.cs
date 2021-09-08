using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public GameObject pauseScreen;
    public string retryScene;
    public string quitScene;
    private bool isPaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            PauseON();
            isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            PauseOFF();
            isPaused = false;
        }
    }

    public void PauseON()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void PauseOFF()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void Retry()
    {
        SceneManager.LoadScene(retryScene);
    }

    public void Quit()
    {
        SceneManager.LoadScene(quitScene);
    }
}
