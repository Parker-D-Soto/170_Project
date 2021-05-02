using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttack : MonoBehaviour
{
    public bool Attacking = false;
    public Animator anim;

    public float attackRange;
    public int damage = 1;
    public float attackTimeDelay;
    private Updated_Player_Stats pStats;

    private float lastTimeAttack;
    private Transform target;


    //For non-attacking Goblin
    /*public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("collision");
        //Debug.Log(collision.gameObject.name);
        
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("collision");
            //May have to put this into an enemy script later.
            collision.gameObject.GetComponent<Updated_Player_Stats>().gotHit(damage);

        }
    }*/

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        pStats = target.GetComponent<Updated_Player_Stats>();
    }

    private void Update()
    {
        if (pStats.checkAlive())
        {
            float distanceFromPlayer = Vector2.Distance(transform.position, target.position);
            if (distanceFromPlayer <= attackRange)
            {
                if (!Attacking)
                {
                    lastTimeAttack = Time.time;
                    Attacking = true;
                    anim.SetBool("Attack", Attacking);
                }
                if (Time.time > lastTimeAttack + attackTimeDelay)
                {
                    Debug.Log("Attempted Damage");
                    target.GetComponent<Updated_Player_Stats>().gotHit(damage);
                    lastTimeAttack = Time.time;
                    Attacking = false;
                }
            }
            else
            {
                if (Time.time > lastTimeAttack + attackTimeDelay)
                {
                    Attacking = false;
                    anim.SetBool("Attack", Attacking);
                }
            }
        }
    }
    //For attacking Goblin
}
