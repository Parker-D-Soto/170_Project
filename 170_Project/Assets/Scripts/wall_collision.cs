using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_collision : MonoBehaviour
{

    private Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        // if player touches wall, then body type = dynamic;
        if (collider.gameObject.CompareTag("Wall"))
        {
            //body.isKinematic=false
            body.bodyType = RigidbodyType2D.Dynamic;
            if (gameObject.GetComponent<Updated_Player_Stats>().Check_Grapple_Status())
            {
                gameObject.GetComponent<Updated_Player_Stats>().Toggle_Grapple_Status();
            }
        }
    }

    void OnCollisionExit2D(Collision2D collider)
    {
        // if player stop touching wall, then body type = kinetic;
        if (collider.gameObject.CompareTag("Wall"))
        {
            //body.isKinematic=false
            body.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}
