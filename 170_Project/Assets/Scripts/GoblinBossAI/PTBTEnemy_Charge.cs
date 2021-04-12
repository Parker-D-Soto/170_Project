using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PTBTEnemy_Charge : MonoBehaviour
{

    private Transform tf;
    public int damage;
    public float runSpeed;
    private Vector3 runDirection;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        if(gameObject.transform.position.x == 95)
        {
            runDirection = new Vector3(1, 0, 0) * runSpeed * Time.deltaTime;
        }
        else if(gameObject.transform.position.x == 855)
        {
            runDirection = new Vector3(-1, 0, 0) * runSpeed * Time.deltaTime;
        }
        else if(gameObject.transform.position.y == -90)
        {
            runDirection = new Vector3(0, 1, 0) * runSpeed * Time.deltaTime;
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
    }

    void OnTriggerEnter2D(Collider2D collider){
        //destroy enemy/ lose health if collision occurs
        if(collider.gameObject.tag.Equals("Player")){
            GameObject.FindGameObjectWithTag("Player").GetComponent<Updated_Player_Stats>().gotHit(damage);
            Destroy(gameObject);

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
