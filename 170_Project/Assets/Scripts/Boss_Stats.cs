using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Stats : MonoBehaviour
{
    private int health;

    private int last_attack;

    private bool alive;


    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        last_attack = 0;
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(alive && health <= 0)
        {
            alive = false;
        }

    }

    public void Minus_Health(int damage)
    {
        health = health - damage;
    }

    public void Change_Last_Attack(int new_attack)
    {
        last_attack = new_attack;
    }
}
