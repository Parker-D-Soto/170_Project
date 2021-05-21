using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalSceneScript : MonoBehaviour
{
    private ProgressSaverMaster saver;

    // Start is called before the first frame update
    void Start()
    {
        saver = GameObject.FindGameObjectWithTag("Saver").GetComponent<ProgressSaverMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.tag == "Player"){
            Debug.Log("move to the next room");
            Destroy(saver.gameObject);
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); move to the tree boss
            SceneManager.LoadScene(0); // move to main menu
            
        }
    }
}
