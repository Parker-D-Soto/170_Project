using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float speed;
    private Rigidbody2D body;
    public Camera Camera;

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
        var transform = this.GetComponent<Transform>();

        //get mouse input
        //Vector2 mouse_pos = Input.mousePosition;
        Vector3 mouse_pos = Camera.ScreenToWorldPoint(Input.mousePosition); //makes position the same range as player position
        mouse_pos.z = 0;

        //checking if mouse is clicked
        if(Input.GetMouseButtonDown(0)){
            bool close_enough = false;

            //Debug.Log("position: "+ transform.position.x);
            //Debug.Log("mouse: "+ mouse_pos.x);
            //Debug.Log("value: "+ Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(mouse_pos.x)));
 

            //check distance
            if(Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(mouse_pos.x))< 2.0f && Mathf.Abs(Mathf.Abs(transform.position.y) - Mathf.Abs(mouse_pos.y))< 2.0f){
                close_enough = true;
            }

            //if close enough then call grappling function
            if(close_enough == true){
                grappling(mouse_pos, transform.position);
            }


        }
    }

    //grappling effect
    void grappling(Vector2 mouse_pos, Vector3 position){
        float x_pos = mouse_pos.x;
        float y_pos = mouse_pos.y;
        Vector3 new_pos = new Vector3(x_pos, y_pos, position.z);
        this.GetComponent<Transform>().position = new_pos;
        //Debug.Log("old pos: " + position);
        //Debug.Log("new pos: " + new_pos);

    }
}
