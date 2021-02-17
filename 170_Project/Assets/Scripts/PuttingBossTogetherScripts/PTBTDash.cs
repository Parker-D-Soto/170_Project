using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTBTDash : MonoBehaviour
{
   public float dashSpeed = 300000; //set dash speed

    public Rigidbody2D rg;

    bool dash = true;
    int dashCooldown = 60;  //modify cooldown

    void FixedUpdate()
    {
        if (dashCooldown == 0)
        {
            dash = true;
        }
        else
        {
            dashCooldown--;
        }

        rg.velocity = Vector2.zero;

        if (dash && Input.GetKey(KeyCode.Space) && !gameObject.GetComponent<Updated_Player_Stats>().Check_Dialogue_Status())
        {
            Debug.Log("dash");
            //Vector2 mouseDirection = (Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2)).normalized;
            Vector2 mouseDirection = (Input.mousePosition - new Vector3(gameObject.transform.position.x, gameObject.transform.position.y)).normalized;
            //rg.AddForce(mouseDirection * dashSpeed * Time.fixedDeltaTime);
            Vector2 newPosition = ((Vector2)gameObject.transform.position + mouseDirection * dashSpeed);
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, newPosition, dashSpeed);
            dash = false;
            dashCooldown = 60; //update cooldown
        }
    }
}
