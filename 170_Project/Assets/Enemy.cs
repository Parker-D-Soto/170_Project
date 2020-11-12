using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float enemyType; // 1 = one attack death, 3 = multiple attacks to die
    private float health_num = 3;
    public Text enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //destroy enemy/ lose health if collision occurs
    void OnCollisionEnter2D(Collision2D collider){
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
    }
}
