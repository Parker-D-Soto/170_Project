using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileAttack : MonoBehaviour
{
    public GameObject player;
    public Transform firePoint;
    public GameObject grapple;
    public float bulletForce = 20f;

    public static GameObject newProjectiles;
    public GameObject bulletSpawn;
    public static bool findProjectile;
    public static bool holdobject;
    public GameObject hitEffect;


    void Start()
    {
        //player currently does not have an object they grappled.

        //findProjectile = false;   
        holdobject = false;
    }
    void Update()
    {
        if (findProjectile == false)
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
        //Debug.Log(findProjectile);
    }

    public void DetectObject()
    {
        //Debug.Log(grapple.name +"current object");
        bulletSpawn = Instantiate(grapple, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bulletSpawn.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        Destroy(bulletSpawn, 500f);
        //Debug.Log(grapple.name + " hittest1");
        // Debug.Log(findProjectile);

    }

    public void foundObject()
    {

        Rigidbody2D rb = newProjectiles.AddComponent<Rigidbody2D>(); //add Rigidbody2D when ready to shoot.
        rb.gravityScale = 0;
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        Destroy(newProjectiles, 300f);
        findProjectile = false;
        holdobject = false;
    }

    public void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "grapple")
        {
            holdobject = !holdobject;
            Destroy(gameObject);
            // Debug.Log(hitInfo.name);
            // GameObject newProjectile = Instantiate(hitInfo.gameObject, firePoint.position, firePoint.rotation);
            grapple = GameObject.Find(hitInfo.name);
            //grapple turns into gameobject that it collides with.                                   
            //bulletSpawn = GameObject.Find(grapple.name);                                       
            //Debug.Log(bulletSpawn.name + " new object");
            newProjectiles = Instantiate(grapple, firePoint.position, firePoint.rotation); //spawn that gameobject onto the field.
            newProjectiles.gameObject.tag = "clone";
            newProjectiles.transform.parent = player.transform;
            holdobject = changeStatus(findProjectile);
            Debug.Log(holdobject + " new status");
        }


    }

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
