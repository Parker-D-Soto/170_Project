using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    public float moveSpeed = 10f;

    public Rigidbody2D rb;
    Vector2 movement;

    void Update()
    {
        //input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


    }

    void FixedUpdate()
    {
        //player movement
        if (!GetComponent<Updated_Player_Stats>().Check_Dashing() && !gameObject.GetComponent<Updated_Player_Stats>().Check_Dialogue_Status())
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }

        if (GetComponent<Updated_Player_Stats>().Check_Dashing())
        {
            
            transform.position = Vector2.MoveTowards(transform.position, GetComponent<PTBTDash>().CheckDestination(), GetComponent<PTBTDash>().dashSpeed*Time.deltaTime);
            if (transform.position.x == GetComponent<PTBTDash>().CheckDestination().x && transform.position.y == GetComponent<PTBTDash>().CheckDestination().y)
            {
                //Debug.Log("GetDashing: " + GetComponent<Updated_Player_Stats>().Check_Dashing());
                GetComponent<Updated_Player_Stats>().End_Dashing();
            }
        }

    }


}
