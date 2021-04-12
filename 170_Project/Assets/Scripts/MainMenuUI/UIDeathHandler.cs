using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIDeathHandler : MonoBehaviour
{
    public GameObject player;
    public GameObject boss;
    public GameObject uiWinText;
    public GameObject scenePortal;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(boss == null) {
            uiWinText.SetActive(true);
            scenePortal.SetActive(true);
            
        } 
        else {
            uiWinText.SetActive(false);
            scenePortal.SetActive(false);
        }

        if(player == null) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); // move to main menu
        }
    }

    
}
