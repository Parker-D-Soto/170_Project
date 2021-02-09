using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy_Charge : MonoBehaviour
{
    public float enemyType; // 1 = one attack death, 3 = multiple attacks to die
    private float health_num = 3;
    public Text enemyHealth;
    private Rigidbody2D rb;
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.one * force, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collider){
        //destroy enemy/ lose health if collision occurs
        if(collider.gameObject.CompareTag("Player")){
            if(enemyType == 1){
                Object.Destroy(this.gameObject);
            }
            if(enemyType == 2){
                health_num -= 1;

                //displaying enemy's health
                this.enemyHealth.text = "Enemy Health: " + this.health_num.ToString();
                
                if(health_num == 0){
                    Object.Destroy(this.gameObject);
                }
            }
        }

        //bounce of wall
        if(collider.gameObject.CompareTag("RightWall")){
            rb.AddForce(Vector2.left * force, ForceMode2D.Impulse);
        }

        if(collider.gameObject.CompareTag("LeftWall")){
            rb.AddForce(Vector2.right * force, ForceMode2D.Impulse);
        }

        if(collider.gameObject.CompareTag("TopWall")){
            rb.AddForce(Vector2.down * force, ForceMode2D.Impulse);
        }

        if(collider.gameObject.CompareTag("BottomWall")){
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }
    }
}
