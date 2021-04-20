using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFire : MonoBehaviour
{
	private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;
    private Transform player;
    private Quaternion rotation;
	void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
	
	void Update() {
        if (timeBtwShots <= 0){
             Instantiate(projectile, transform.position, rotation);
             timeBtwShots = startTimeBtwShots;
        } else {
            timeBtwShots -= Time.deltaTime;
        }
	}
}