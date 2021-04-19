
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    public float moveSpeed = 10f;

    public Rigidbody2D rb;
    public Vector2 movement;

    void Update()
    {
        //input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


    }

    void FixedUpdate()
    {
        //player movement
        if (!gameObject.GetComponent<Updated_Player_Stats>().Check_Dialogue_Status() && !gameObject.GetComponent<Updated_Player_Stats>().Check_Grapple_Status() && !gameObject.GetComponent<Updated_Player_Stats>().Check_Dash_Status())
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }

    }
}
