using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject bar;
    public float health;
    public int type; // 1 = firegobbo  2 = getemgobob
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //test
    }

    //lower enemy's health
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.layer == LayerMask.NameToLayer("Pickaxe")){
            if(GameObject.Find("Player UM").GetComponent<GrappleProj>().holdobject == false){ //accessing holdobject from GrappleProj script
                if(type == 1){
                    Debug.Log("firegobbo collided with crystal");
                    Object.Destroy(this.gameObject);
                }else{
                    Debug.Log("getemgobbo collided with crystal");
                    health = health - 0.4f;
                    if(health <= 0){
                        Object.Destroy(this.gameObject);
                    }else{
                        //bar.transform.localScale = new Vector3(health/3.0f, 1.0f, 1.0f);
                        bar.transform.localScale = new Vector3(180.0f*health, 150.0f, 1.0f);
                        Debug.Log("enemy got hit");
                        Debug.Log(bar.transform.localScale);
                    }
                }
                
            }
        }
    
    }
}
