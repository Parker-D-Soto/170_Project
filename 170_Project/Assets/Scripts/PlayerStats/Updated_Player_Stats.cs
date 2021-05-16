﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Stats for player, mainly used to test attacks for hits and damage
//Combination of Boss_Stats and Player_Dmg_State
public class Updated_Player_Stats : MonoBehaviour
{
    //health tracking
    private int health;
    private bool inDialogue = true;
    private bool inGrapple = false;
    private bool inDash = false;
    private bool alive;

    public Animator anim;

    public LineController lc;
    public GrappleProj gP;

    public GameObject gameOverCanvas;

    public GameObject SoundEffects;

    public AudioSource hitSound;
    public AudioSource deathSound;
    private AudioSource[] soundEffects;

    //Variables for damgage state
    Color damage_color;
    bool damaged = false;
    bool flash = true;
    float timer = 3; //3 second damage state effect

    float flashTimerReset = 0.24f;
    float flashTimer = 0.24f;
    float flashTimerTemp = 0.24f;
    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        alive = true;

        soundEffects = SoundEffects.GetComponents<AudioSource>();
        hitSound = soundEffects[0];
        deathSound = soundEffects[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (alive && health <= 0)
        {
            Debug.Log("What comes first");
            alive = false;
            deathSound.Play();
            gP.lPoints.Remove(transform);
            lc.SetPoints(gP.lPoints);
            gameOverCanvas.SetActive(true);
            Destroy(gameObject);
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
                flashTimerTemp -= Time.deltaTime;
                if (flash)
                {
                    sprite.color = new Color(0.5f,0.5f,0.5f,0.5f);

                    if (flashTimerTemp < 0)
                    {
                        flash = false;
                        flashTimer -= 0.02f;
                        flashTimerTemp = flashTimer;
                    }

                }
                else
                {
                    sprite.color = Color.white;

                    if (flashTimerTemp < 0)
                    {
                        flash = true;
                        flashTimerTemp = flashTimer;
                    }
                }
            }
            else
            {
                //Debug.Log("Done");
                sprite.color = Color.white;
                damaged = false;
                anim.SetBool("inHurt", damaged);
                //reset timer to 3 second for next countdown
                timer = 3;
                flashTimer = flashTimerReset;
            }
        }
    }

    public int Check_Health()
    {
        return health;
    }

    public void Minus_Health(int damage)
    {
        health = health - damage;
        //Debug.Log("Health: " + health);
    }
    //function to call in boss attacks when player is hit
    public void gotHit(int damage)
    {

        if (!damaged && !inDash)
        {
            Minus_Health(damage);
            hitSound.Play();
            damaged = true;
            anim.SetBool("inHurt", damaged);
        }



    }

    public void Toggle_Dialogue_Status()
    {
        inDialogue = !inDialogue;
    }

    public bool Check_Dialogue_Status()
    {
        return inDialogue;
    }

    public void Activate_Grapple()
    {
        inGrapple = true;

    }

    public void Deactivate_Grapple()
    {
        inGrapple = false;

    }

    public bool Check_Grapple_Status()
    {
        return inGrapple;
    }

    public void Toggle_Dash_Status()
    {
        inDash = !inDash;
        anim.SetBool("inRoll", inDash);
    }

    public bool Check_Dash_Status()
    {
        return inDash;
    }

    public bool checkAlive()
    {
        return alive;
    }

}