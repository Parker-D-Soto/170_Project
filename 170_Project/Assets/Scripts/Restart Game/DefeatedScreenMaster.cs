using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatedScreenMaster : MonoBehaviour
{

    private ProgressSaverMaster saver;

    public void Start()
    {
        saver = GameObject.FindGameObjectWithTag("Saver").GetComponent<ProgressSaverMaster>();
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        Destroy(saver.gameObject);
        SceneManager.LoadScene("Main Menu");
    }

    public void ResartFight()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
