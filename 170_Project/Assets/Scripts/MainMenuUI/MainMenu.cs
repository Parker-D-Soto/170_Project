using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

  void Start(){
    //SoundManagerScript.PlaySound("Menu");
    


  }

  public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SoundManagerScript.StopSound("Menu");
        SceneManager.LoadScene("IntroTextScene");
    }

  public void QuitGame()
    {
        //Debug.Log("You have Quit the Game!");
        Application.Quit();
    }

  public void Sound() {
    //Debug.Log("moving to sound settings");
    SceneManager.LoadScene("volume-control");
  }

  public void Menu() {
    SceneManager.LoadScene("Main Menu");
  }
}
