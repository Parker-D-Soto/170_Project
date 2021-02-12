using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerOnTouch : MonoBehaviour
{
    public int damage = 1;

    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Player")
        {
            //May have to put this into an enemy script later.
            collision.gameObject.GetComponent<Updated_Player_Stats>().gotHit(damage);

        }
    }
}
