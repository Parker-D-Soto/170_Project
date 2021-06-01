﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject volumeMenuUI;
    bool inSetting = false;

    private ProgressSaverMaster saver;

    public void Start()
    {
        saver = GameObject.FindGameObjectWithTag("Saver").GetComponent<ProgressSaverMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(inSetting){
                Back();
            }else{
                if(GameIsPaused) {
                    Resume();
                } else {
                    Pause();
                }
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
        Destroy(saver.gameObject);
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame() {
        Debug.Log("Quiting Game");
        Application.Quit();
    }

    public void Settings() {
        pauseMenuUI.SetActive(false);
        volumeMenuUI.SetActive(true);
        inSetting = true;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Back(){
        volumeMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        inSetting = false;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
