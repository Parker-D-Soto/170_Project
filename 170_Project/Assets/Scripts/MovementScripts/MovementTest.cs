
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Animator anim;

    public Rigidbody2D rb;
    public Vector2 movement;

    void Update()
    {
        //input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        /*anim.SetFloat("vertSpeed", movement.y);
        anim.SetFloat("horizSpeed", movement.x);
        if(movement.y >= movement.x)
        {
            anim.SetBool("vertPrio", true);
        }
        else
        {
            anim.SetBool("vertPrio", false);
        }*/
    }

    void FixedUpdate()
    {
        //player movement
        if (!gameObject.GetComponent<Updated_Player_Stats>().Check_Dialogue_Status() && !gameObject.GetComponent<Updated_Player_Stats>().Check_Grapple_Status() && !gameObject.GetComponent<Updated_Player_Stats>().Check_Dash_Status())
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            anim.SetFloat("vertSpeed", movement.y);
            anim.SetFloat("horizSpeed", movement.x);
            if (Mathf.Abs(movement.y) >= Mathf.Abs(movement.x))
            {
                anim.SetBool("vertPrio", true);
            }
            else
            {
                //Debug.Log("vertPrio is OFF");
                //Debug.Log("movement in x is " + movement.x);
                anim.SetBool("vertPrio", false);
            }
        }

    }
}
