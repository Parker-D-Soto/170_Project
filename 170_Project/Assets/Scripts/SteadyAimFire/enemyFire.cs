using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFire : MonoBehaviour
{
	private float timeBtwShots;
    public float startTimeBtwShots;
    float speed = 30;
    float damage = 1;
    public GameObject projectile;
    private Transform player;

	void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
	}
	
	void Update() {
        if (timeBtwShots <= 0){
             Instantiate(projectile, transform.position, Quaternion.identity);
            GameObject[] shots = GameObject.FindGameObjectsWithTag("GoblinProjectile");
            foreach (GameObject shot in shots)
            {
                shot.GetComponent<projectile>().speed = speed;
                shot.GetComponent<DamagePlayerOnTouch>().damage = (int)damage;
            }
             timeBtwShots = startTimeBtwShots;
        } else {
            timeBtwShots -= Time.deltaTime;
        }
	}

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

}