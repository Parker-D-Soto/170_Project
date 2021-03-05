using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleProjObject : MonoBehaviour
{
    public GrappleProj grapHook;

    public void Awake()
    {
        //Debug.Log("Im alive");
    }

    public void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "grapple")
        {
            grapHook.firing = false;
            Destroy(gameObject);
            grapHook.holdobject = true;
            // Debug.Log(hitInfo.name);
            // GameObject newProjectile = Instantiate(hitInfo.gameObject, firePoint.position, firePoint.rotation);
            grapHook.grappleObject = GameObject.Find(hitInfo.name);
            //grapple turns into gameobject that it collides with.                                   
            //bulletSpawn = GameObject.Find(grapple.name);                                       
            //Debug.Log(bulletSpawn.name + " new object");
            grapHook.newProjectiles = Instantiate(grapHook.grappleObject, grapHook.firePoint.position, grapHook.firePoint.rotation); //spawn that gameobject onto the field.
            grapHook.newProjectiles.gameObject.tag = "clone";
            grapHook.newProjectiles.transform.parent = grapHook.player.transform;
            grapHook.holdobject = grapHook.changeStatus(grapHook.findProjectile);
            //Debug.Log(grapHook.holdobject + " new status");
            //Destroy(gameObject);
        }
        else if (hitInfo.tag == "Wall")
        {
            Destroy(gameObject);
            Debug.Log("AWAY I GOOOOO");
            grapHook.grappleToObject = hitInfo.gameObject;
            grapHook.player.GetComponent<Updated_Player_Stats>().Toggle_Grapple_Status();
            grapHook.direction = (hitInfo.gameObject.transform.position - grapHook.player.transform.position).normalized;
            //Destroy(gameObject);
        }
    }

    public void OnDestroy()
    {
        //Debug.Log("I died");
        grapHook.firing = false;
    }
}
