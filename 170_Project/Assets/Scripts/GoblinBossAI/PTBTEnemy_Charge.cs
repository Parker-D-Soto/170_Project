using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PTBTEnemy_Charge : MonoBehaviour
{

    private Transform tf;
    private SpriteRenderer visible;
    public int damage;
    public float runSpeed;
    private Vector3 runDirection;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        visible = GetComponent<SpriteRenderer>();
        if(tf.position.y < 300)
        {
            anim.SetBool("Side", true);
            if (tf.position.x < 0)
            {
                runDirection = new Vector3(1, 0, 0) * runSpeed * Time.deltaTime;
            }
            else
            {
                runDirection = new Vector3(-1, 0, 0) * runSpeed * Time.deltaTime;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
        }
        else
        {
            runDirection = new Vector3(0, -1, 0) * runSpeed * Time.deltaTime;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (tf.position.y >= 315)
        {
            visible.color = Color.clear;
        }
        else if(tf.position.x <= -90)
        {
            visible.color = Color.clear;
        }
        else if(tf.position.x >= 1000)
        {
            visible.color = Color.clear;
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        //destroy enemy/ lose health if collision occurs
        if(collider.gameObject.tag.Equals("Player")){
            GameObject.FindGameObjectWithTag("Player").GetComponent<Updated_Player_Stats>().gotHit(damage);
        }
    }

    private void Move()
    {
        tf.position = tf.position + runDirection;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
