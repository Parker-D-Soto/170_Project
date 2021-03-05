using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTBTPickaxeCollisionScript : MonoBehaviour
{
    public float moveSpeed = 240;
    Rigidbody2D rb;
    GameObject target;
    Vector2 moveDirection;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag.Equals("Player"))
        {
            hitInfo.GetComponent<Updated_Player_Stats>().gotHit(damage);
            Destroy(gameObject);
        }
    }
}
