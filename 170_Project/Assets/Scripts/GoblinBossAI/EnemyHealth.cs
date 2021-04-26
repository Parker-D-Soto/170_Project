using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject bar;
    public int health;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("test");
    }

    //lower enemy's health
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.layer == LayerMask.NameToLayer("Pickaxe")){
            if(GameObject.Find("Player").GetComponent<GrappleProj>().holdobject == false){ //accessing holdobject from GrappleProj script
                Debug.Log("colliding with pickaxe");
                health = health - 1;
                if(health == 0){
                    Object.Destroy(this.gameObject);
                }else{
                    bar.transform.localScale = new Vector3(health/3.0f, 1.0f, 1.0f);
                    Debug.Log("enemy hit");
                }
            }
        }
    
    }
}
