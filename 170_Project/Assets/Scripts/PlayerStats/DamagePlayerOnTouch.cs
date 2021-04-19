using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerOnTouch : MonoBehaviour
{
    public int damage = 1;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");
        Debug.Log(collision.gameObject.name);
        
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("collision");
            //May have to put this into an enemy script later.
            collision.gameObject.GetComponent<Updated_Player_Stats>().gotHit(damage);

        }
    }
}
