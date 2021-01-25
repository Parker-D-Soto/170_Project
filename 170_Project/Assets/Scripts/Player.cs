using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void FixedUpdate() //where physic updates go
    {
        //connecting keyboard to player movement
        float moveXaxis = Input.GetAxis("Horizontal");
        float moveYaxis = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveXaxis, moveYaxis);

        body.AddForce(movement * speed);
    }
}
