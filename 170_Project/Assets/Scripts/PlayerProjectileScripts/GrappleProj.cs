using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleProj : MonoBehaviour
{


    public GameObject player;
    public GameObject grappleObject;
    public Transform firePoint;
    public GameObject grapple;
    public float bulletForce = 20f;

    public GameObject newProjectiles;
    public GameObject bulletSpawn;
    public bool findProjectile;
    public bool holdobject;
    public GameObject hitEffect;

    public bool firing = false;

    public float grappleToSpeed;

    public GameObject grappleToObject;

    public Vector2 direction;



    void Start()
    {
        //player currently does not have an object they grappled.

        //findProjectile = false;   
        holdobject = false;
        grappleToSpeed = (1.0f * grappleToSpeed) * Time.deltaTime;
    }
    void FixedUpdate()
    {
        if (!gameObject.GetComponent<Updated_Player_Stats>().Check_Dialogue_Status() && !gameObject.GetComponent<Updated_Player_Stats>().Check_Grapple_Status())
        {
            if (findProjectile == false && !firing)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    DetectObject();

                }
            }
            if (holdobject && Input.GetButtonDown("Fire2"))
            {

                Debug.Log("you are clicking me");
                foundObject();

            }
        }

        if (gameObject.GetComponent<Updated_Player_Stats>().Check_Grapple_Status())
        {
            player.GetComponent<Rigidbody2D>().MovePosition(player.GetComponent<Rigidbody2D>().position + direction * grappleToSpeed * Time.fixedDeltaTime);
        }
        //Debug.Log(findProjectile);
    }

    public void DetectObject()
    {
        firing = true;
        //Debug.Log(grapple.name +"current object");
        bulletSpawn = Instantiate(grapple, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bulletSpawn.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        Destroy(bulletSpawn, 1f);
        //Debug.Log(grapple.name + " hittest1");
        // Debug.Log(findProjectile);

    }

    public void foundObject()
    {
        firing = false;
        Rigidbody2D rb = newProjectiles.AddComponent<Rigidbody2D>(); //add Rigidbody2D when ready to shoot.
        rb.gravityScale = 0;
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        Destroy(newProjectiles, 5f);
        findProjectile = false;
        holdobject = false;
    }

    /*public void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "Wall")
        {
            Debug.Log("I'm FREEE BABY");
            player.GetComponent<Updated_Player_Stats>().Toggle_Grapple_Status();
        } 
    }*/

    public bool changeStatus(bool changeStatus)
    {
        if (changeStatus == true)
        {
            Debug.Log("hit1");
            return findProjectile = false;

        }
        if (findProjectile == false)
        {
            Debug.Log("hit2");
            return findProjectile = true;

        }
        return true;
    }
}
