using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    int health;
    public Image[] hearts;
    public Image portrait;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        health = GetComponent<Updated_Player_Stats>().Check_Health();

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }

        }
        
    }
}
