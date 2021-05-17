using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject volumeMenuUI;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame() {
        Debug.Log("Quiting Game");
        Application.Quit();
    }

    public void Settings() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = true;
        volumeMenuUI.SetActive(true);
    }

    public void Back(){
        volumeMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = true;
        pauseMenuUI.SetActive(true);
    }
}
