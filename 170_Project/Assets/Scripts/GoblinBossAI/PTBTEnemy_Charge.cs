using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PTBTEnemy_Charge : MonoBehaviour
{

    private Rigidbody2D rb;
    public int damage;
    public float runSpeed;
    private Vector2 runDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(gameObject.transform.position.x == 30)
        {
            runDirection = new Vector2(1, 0);
        }
        else if(gameObject.transform.position.x == 890)
        {
            runDirection = new Vector2(-1, 0);
        }
        else if(gameObject.transform.position.y == 30)
        {
            runDirection = new Vector2(0, 1);
        }
        else
        {
            runDirection = new Vector2(0, -1);
        }

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void OnCollisionEnter2D(Collision2D collider){
        //destroy enemy/ lose health if collision occurs
        if(collider.gameObject.tag.Equals("Player")){
            GameObject.FindGameObjectWithTag("Player").GetComponent<Updated_Player_Stats>().gotHit(damage);
            Destroy(gameObject);

        }


    }

    private void Move()
    {
        rb.velocity = runDirection * runSpeed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
