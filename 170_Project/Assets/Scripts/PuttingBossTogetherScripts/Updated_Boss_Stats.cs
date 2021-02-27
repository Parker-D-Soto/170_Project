using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class Updated_Boss_Stats : MonoBehaviour
{
    public int health = 100;

    protected string last_attack = "";

    protected bool alive = true;

    public float cooldown = 1;

    protected float timeSinceLastAttack = 0;

    public float startup = 1f;

    protected float timeLeftOnStartup = 0f;

    public float speed = 5;

    protected bool inDialogue = true;

    public virtual void SearchAttacks(string potentialAttack, bool isEnabled)
    {
        Debug.Log("Something went wrong");
    }

    // Update is called once per frame
    void Update()
    {
        if(alive && health <= 0)
        {
            alive = false;
        }

        if (!alive)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void Modify_Speed(int speedChangedBy)
    {
        speed += (float)speedChangedBy / 1000;
    }

    public void Modify_Startup(int startupChangedBy)
    {
        startup += (float)startupChangedBy / 1000;
    }

    public void Modify_Cooldown(int cooldownChangedBy)
    {
        cooldown += (float)cooldownChangedBy / 1000;
    }

    public void Modify_Health(int healthChangedBy)
    {
        health += healthChangedBy;
    }

    public void Toggle_Dialogue_Status()
    {
        inDialogue = !inDialogue;
    }

    public void Change_Last_Attack(string new_attack)
    {
        last_attack = new_attack;
    }

    //checks to make sure some amount of time has passed since the last attack occurred and the next attack being called
    [Task]
    public void IsCooldownOver()
    {
        if (timeSinceLastAttack <= 0)
        {
            Task.current.Succeed();
        }
        else
        {
            timeSinceLastAttack -= Time.deltaTime;
            Task.current.Fail();
        }
    }


    //resets cooldown clock after attack
    [Task]
    public void ResetCooldown()
    {

        timeSinceLastAttack = cooldown;
        Task.current.Succeed();

    }

    //Gives player time to understand what attack is being called
    [Task]
    public void CheckStartup()
    {
        if(timeLeftOnStartup < startup)
        {
            //Debug.Log("Complete: " + timeLeftOnStartup);
            Task.current.Succeed();
        }
        /*else
        {
            Task.current.Fail();
        }*/
    }

    //increments timer
    [Task]
    public void IncrementStartup()
    {
        if(timeLeftOnStartup < startup)
        {
            timeLeftOnStartup += Time.deltaTime;
            //Debug.Log("Increment: " + timeLeftOnStartup);
        }
        else
        {
            //Debug.Log("Attack");
            Task.current.Succeed();
        }
        
        
    }

    //Gives player time to understand what attack is being called
    [Task]
    public void ResetStartup()
    {

        timeLeftOnStartup = 0f;
        //Debug.Log("Reset: " + timeLeftOnStartup);
        Task.current.Succeed();
    }

    //Succeeds if chosen attack isn't the same as the last attack
    [Task]
    public void SameAsLastAttack(string attackName)
    {
        if (attackName.Equals(last_attack,StringComparison.OrdinalIgnoreCase))
        {
            Task.current.Fail();
        }
        else
        {
            Change_Last_Attack(attackName);
            Task.current.Succeed();
        }
    }

    //Check to see if Scene is still in dialogue section
    [Task]
    public void CheckDialogueStatus()
    {
        if (inDialogue)
        {
            Task.current.Fail();
        }
        else
        {
            Task.current.Succeed();
        }
    }

    //Checks to see if the boss is Alive
    [Task]
    public void IsAlive()
    {
        if (alive)
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
}
