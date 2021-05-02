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
    public float hookBulletForce = 20f;

    public GameObject newProjectiles;
    public GameObject bulletSpawn;
    public bool findProjectile;
    public bool holdobject;
    public GameObject hitEffect;

    public bool firing = false;

    public float grappleToSpeed = 10f;

    public GameObject grappleToObject;

    public Vector2 direction;

    public LineController lc;
    public List<Transform> lPoints = new List<Transform>();



    void Start()
    {
        //player currently does not have an object they grappled.

        //findProjectile = false;   
        holdobject = false;
        //grappleToSpeed = (1.0f * grappleToSpeed) * Time.deltaTime;

        lPoints.Add(transform);
        lc.SetPoints(lPoints);
    }

    void Update()
    {
        if (!gameObject.GetComponent<Updated_Player_Stats>().Check_Dialogue_Status() && !gameObject.GetComponent<Updated_Player_Stats>().Check_Grapple_Status() && !gameObject.GetComponent<Updated_Player_Stats>().Check_Dash_Status())
        {
            if (findProjectile == false && !firing)
            {
                //Debug.Log("ready");
                if (Input.GetButtonDown("Fire1"))
                {
                    Debug.Log("you are clicking me");
                    DetectObject();

                }
            }
            if (holdobject && Input.GetButtonDown("Fire1"))
            {

                //Debug.Log("you are clicking me");
                foundObject();

            }
        }
        //Debug.Log(findProjectile);
    }

    private void FixedUpdate()
    {
        if (gameObject.GetComponent<Updated_Player_Stats>().Check_Grapple_Status())
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.MovePosition(rb.position + direction * Time.deltaTime * grappleToSpeed);
        }
    }

    public void DetectObject()
    {
        firing = true;
        //Debug.Log(grapple.name +"current object");
        bulletSpawn = Instantiate(grapple, firePoint.position, firePoint.rotation);
        lPoints.Add(bulletSpawn.transform);
        lc.SetPoints(lPoints);
        Rigidbody2D rb = bulletSpawn.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * hookBulletForce, ForceMode2D.Impulse);
        Destroy(bulletSpawn, 1.0f);
        //Debug.Log(grapple.name + " hittest1");
        // Debug.Log(findProjectile);

    }

    public void foundObject()
    {
        firing = false;
        Rigidbody2D rb = newProjectiles.AddComponent<Rigidbody2D>(); //add Rigidbody2D when ready to shoot.
        //rb.isKinematic = true;
        Physics2D.IgnoreLayerCollision(10,10); //ignore pickaxe and clone pickaxe from colliding
        Physics2D.IgnoreLayerCollision(9,10); //ignores clone pickaxe and dividing wall from colliding
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
            if (player.GetComponent<Updated_Player_Stats>().Check_Grapple_Status())
            {
                player.GetComponent<Updated_Player_Stats>().Deactivate_Grapple();
            }
        } 
    }*/

    private void OnCollisionEnter2D(Collision2D hitInfo)
    {
        //Debug.Log("I'm FREEE BABY");

        if (hitInfo.gameObject.tag == "Wall")
        {
            //Debug.Log("I'm FREEE BABY");
            if (player.GetComponent<Updated_Player_Stats>().Check_Grapple_Status())
            {
                player.GetComponent<Updated_Player_Stats>().Deactivate_Grapple();
            }
        }
    }

    public bool changeStatus(bool changeStatus)
    {
        if (changeStatus == true)
        {
            //Debug.Log("hit1");
            return findProjectile = false;

        }
        if (findProjectile == false)
        {
            //Debug.Log("hit2");
            return findProjectile = true;

        }
        return true;
    }
}

