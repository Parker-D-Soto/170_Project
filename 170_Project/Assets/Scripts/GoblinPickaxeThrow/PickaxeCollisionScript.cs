using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeCollisionScript : MonoBehaviour
{
    public float moveSpeed = 7f;
    Rigidbody2D rb;

    MovementTest target;
    Vector2 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<MovementTest>();
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
        if (hitInfo.gameObject.name.Equals("Player"))
        {
            Debug.Log("The player has been hit");
            Destroy(gameObject);
        }
    }
}
