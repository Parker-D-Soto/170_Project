using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTBTDash : MonoBehaviour
{
    public float dashSpeed = 300; //set dash speed
    public float dashDistance = 1000;
    public Rigidbody2D rg;

    bool dash = true;
    int dashCooldown = 2;  //modify cooldown

    private Vector2 destination;

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
            //Debug.Log("dash");
            //Vector2 mouseDirection = (Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2)).normalized;
            //Vector2 mouseDirection = (Input.mousePosition - new Vector3(gameObject.transform.position.x, gameObject.transform.position.y)).normalized;
            //rg.AddForce(mouseDirection * dashSpeed * Time.fixedDeltaTime);
            //Vector2 newPosition = ((Vector2)gameObject.transform.position + mouseDirection * dashDistance);
            //gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, newPosition, dashDistance); //need to find someway to update: might need to use states
            dash = false;
            destination = MoveToNewPosition();
            GetComponent<Updated_Player_Stats>().Start_Dashing();
            dashCooldown = 2; //update cooldown
        }
    }


    private Vector2 MoveToNewPosition()
    {
        
        var mouseDirection = new Vector2(Input.mousePosition.x - transform.position.x, Input.mousePosition.y - transform.position.y).normalized;
        //Debug.Log("mouseDirection: " + mouseDirection);
        var dashVector = mouseDirection * dashDistance;
        var targetPosition = new Vector2(transform.position.x + dashVector.x, transform.position.y + dashVector.y);
        return targetPosition;
    }

    public Vector2 CheckDestination()
    {
        return destination;
    }
}
