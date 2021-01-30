using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Stats for player, mainly used to test attacks for hits and damage
//Combination of Boss_Stats and Player_Dmg_State
public class Player_Stats : MonoBehaviour
{
    //health tracking
    private int health;

    private bool alive;

    //Variables for damgage state
    Color damage_color;
    bool damaged = false;
    bool flash = true;
    float timer = 3; //3 second damage state effect

    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive && health <= 0)
        {
            alive = false;
        }

        //damage state effect

        var sprite = GetComponent<SpriteRenderer>();

        //damage state effect
        if (damaged)
        {

            //countdown for effect
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
                if (flash)
                {
                    sprite.color = Color.grey;
                    flash = false;
                }
                else
                {
                    sprite.color = Color.white;
                    flash = true;
                }
            }
            else
            {
                Debug.Log("Done");
                sprite.color = Color.white;
                damaged = false;
                //reset timer to 3 second for next countdown
                timer = 3;
            }


        }
    }

    public void Minus_Health(int damage)
    {
        health = health - damage;
        Debug.Log("Health: " + health);
    }
    //function to call in boss attacks when player is hit
    public void gotHit()
    {

        if (!damaged)
        {
            Minus_Health(1);
            damaged = true;
        }


        
    }

}
