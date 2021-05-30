using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float time = 5;

    private Transform player;
    private Vector2 target;
    private Vector2 current;
    private Vector2 direction;
    //private bool destroy;

    void Start()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;

            target = player.position;
            current = transform.position;
            direction = (target - current);

            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            current = transform.position;

            transform.position = current + (direction * speed * Time.deltaTime);
            time = time - Time.deltaTime;
            if(time < 0)
            {
                DestroyProjectile();
            }
        }
        else
        {
            DestroyProjectile();
        }


    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            DestroyProjectile();
        }
    }

    void DestroyProjectile(){
        Destroy(gameObject);
    }
}