using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("IntroTextScene");
    }

  public void QuitGame()
    {
        Debug.Log("You have Quit the Game!");
        Application.Quit();
    }

  public void Sound() {
    Debug.Log("moving to sound settings");
    SceneManager.LoadScene("volume-control");
  }

  public void Menu() {
    SceneManager.LoadScene("Main Menu");
  }
}
