using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.tag == "Player"){
            Debug.Log("move to the next room");    
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); move to the tree boss
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2); // move to main menu
            
        }
    }
}
