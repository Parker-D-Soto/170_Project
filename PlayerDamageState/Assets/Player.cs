using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float speed;
    private Rigidbody2D body;
    Color damage_color;
    bool damaged = false;
    bool flash = true;
    float timer = 3; //3 second damage state effect

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

        var sprite = GetComponent<SpriteRenderer>();
        
        //damage state effect
        if(damaged){
            //countdown for effect
            if(timer >= 0){
                timer -= Time.deltaTime;
                Debug.Log("start");
                if(flash){
                    sprite.color = Color.grey;
                    flash = false;
                }
                else{
                    sprite.color = Color.white;
                    flash = true;
                }
            }
            else{
                Debug.Log("Done");
                sprite.color = Color.white;
                damaged = false;
                //reset timer to 3 second for next countdown
                timer = 3;
            }


        
            
        }
    }

    // if player touches enemy, then it goes into damage state
    void OnCollisionEnter2D(Collision2D collider){
        if(collider.gameObject.CompareTag("EnemyTag")){
            damaged = true;
        }
    }
}
