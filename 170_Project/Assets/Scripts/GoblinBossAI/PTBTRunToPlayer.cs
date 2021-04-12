using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Function that makes goblins run in player's direction (don't chase player, keep running in one direction)
public class PTBTRunToPlayer : MonoBehaviour
{
    private GameObject goblin;                             //attached rigidbody
    private Transform target;                               //target to run to
    private Vector2 runDirection;                           //direction goblins run in
    public float runSpeed;                                  //speed goblins run at
    public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        //Find player's position
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        goblin = gameObject;

        //Find direction to run in
        runDirection = new Vector2(target.position.x - goblin.GetComponent<Transform>().position.x, target.position.y - goblin.GetComponent<Transform>().position.y).normalized;
        Debug.Log(goblin.GetComponent<BoxCollider2D>().size);
    }

    // Update is called once per frame
    void Update()
    {
        //for movement
        Move();

        //hurt player if in contact
        /*Collider2D[] body = Physics2D.OverlapBoxAll(goblin.GetComponent<Transform>().position, goblin.GetComponent<BoxCollider2D>().size,0);
        foreach (var target in body)
        {
            if(target.GetComponent<Updated_Player_Stats>() != null)
            {
                target.GetComponent<Updated_Player_Stats>().gotHit(damage);
            }
        }*/
    }

    private void Move()
    {
        goblin.GetComponent<Rigidbody2D>().velocity = runDirection * runSpeed;
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
