using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleProjObject : MonoBehaviour
{
    public GrappleProj grapHook;
    public Sprite throwSprite;

    public void Awake()
    {
        //Debug.Log("Im alive");
    }

    public void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log("Trigger : " + hitInfo.tag);
        if (hitInfo.tag == "grapple")
        {
            //Debug.Log("Collide Getting Called");

            grapHook.firing = false;
            Destroy(gameObject);
            grapHook.holdobject = true;
            // Debug.Log(hitInfo.name);
            // GameObject newProjectile = Instantiate(hitInfo.gameObject, firePoint.position, firePoint.rotation);
            grapHook.grappleObject = GameObject.Find(hitInfo.name);
            grapHook.grappleObject.GetComponent<Crystal_Recharge>().Toggle_Recharge();
            //grapple turns into gameobject that it collides with.                                   
            //bulletSpawn = GameObject.Find(grapple.name);                                       
            //Debug.Log(bulletSpawn.name + " new object");
            grapHook.newProjectiles = Instantiate(grapHook.grappleObject, grapHook.firePoint.position, grapHook.firePoint.rotation); //spawn that gameobject onto the field.
            grapHook.newProjectiles.gameObject.tag = "clone";
            grapHook.newProjectiles.gameObject.GetComponent<SpriteRenderer>().sprite = throwSprite;
            grapHook.newProjectiles.transform.parent = grapHook.player.transform;
            grapHook.holdobject = grapHook.changeStatus(grapHook.findProjectile);
            //Debug.Log(grapHook.holdobject + " new status");
            //Destroy(gameObject);
            grapHook.newProjectiles.GetComponent<CircleCollider2D>().enabled = true;
        }
        else if (hitInfo.tag == "Wall")
        {
            //Destroy(gameObject);
            Debug.Log("AWAY I GOOOOO");
            grapHook.grappleToObject = hitInfo.gameObject;
            grapHook.player.GetComponent<Updated_Player_Stats>().Activate_Grapple();
            grapHook.direction = (gameObject.transform.position - grapHook.player.transform.position).normalized;
            Destroy(gameObject);
        }
    }

    public void OnDestroy()
    {
        //Debug.Log("Player Hook died");
        grapHook.firing = false;

        grapHook.lPoints.Remove(transform);
        grapHook.lc.SetPoints(grapHook.lPoints);
    }
}

