using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTBTDash : MonoBehaviour
{
    public float dashSpeed = 750; //set dash speed
    //public float dashDistance = 1000;
    private Vector2 direction;
    public Rigidbody2D rg;

    //public Animator anim;

    bool dash = true;
    //bool inDash = false;
    public float dashCooldownCeil = 1f;  //modify cooldown
    float dashCooldown;
    //public float inDashTime = 100;

    float curSpeed;
    public float speedUp = 250;
    public float speedDown = 25;
    bool hitMax;

    void FixedUpdate()
    {
        //bug.Log(gameObject.GetComponent<Updated_Player_Stats>().Check_Grapple_Status());
        if (gameObject.GetComponent<Updated_Player_Stats>().Check_Dash_Status())
        {
            if (curSpeed >= dashSpeed)
            {
                hitMax = true;
            }

            if (curSpeed >= 0)
            {
                    if (!hitMax)
                    {
                        curSpeed += speedUp;
                    }
                    else
                    {
                        curSpeed -= speedDown;
                    }
                
            }
            else
            {
                hitMax = false;
                curSpeed = 0;
            }
            rg.velocity = direction * curSpeed;
            //Debug.Log("curSpeed: " + curSpeed);
        }

        if (dashCooldown <= 0)
        {
            dash = true;
            //dashCooldown = 2;
        }
        else if (/*inDashTime == 1*/ curSpeed < 0 && gameObject.GetComponent<Updated_Player_Stats>().Check_Dash_Status())
        {
            gameObject.GetComponent<Updated_Player_Stats>().Toggle_Dash_Status();
            //inDashTime = 3;
        }
        else
        {
            //Debug.Log(inDashTime);
            dashCooldown-= Time.deltaTime;
            //inDashTime-= Time.deltaTime;
        }
    }
    private void Update()
    {
        if (dash && Input.GetKey(KeyCode.Space) && !gameObject.GetComponent<Updated_Player_Stats>().Check_Dialogue_Status() && !gameObject.GetComponent<Updated_Player_Stats>().Check_Grapple_Status() && !gameObject.GetComponent<Updated_Player_Stats>().Check_Dash_Status())
        {
            //Debug.Log("dash");
            //Vector2 mouseDirection = (Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2)).normalized;
            //direction = (new Vector3(rg.position.x, rg.position.y) - Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized;
            //direction = (new Vector3(rg.position.x, rg.position.y) - Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized;
            direction = gameObject.GetComponent<MovementTest>().movement.normalized;
            //rg.AddForce(mouseDirection * dashSpeed * Time.fixedDeltaTime);
            //Vector2 newPosition = ((Vector2)gameObject.transform.position + mouseDirection * dashDistance);
            //gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, newPosition, dashDistance); //need to find someway to update: might need to use states
            dash = false;
            dashCooldown = dashCooldownCeil; //update cooldown
            //inDashTime = 3;
            gameObject.GetComponent<Updated_Player_Stats>().Toggle_Dash_Status();
        }
    }
}
