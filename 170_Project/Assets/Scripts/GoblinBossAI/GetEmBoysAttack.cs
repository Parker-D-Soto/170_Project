using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Spawn serveral goblins around the player in random placement
public class GetEmBoysAttack : MonoBehaviour
{
    public GameObject gobbo;        //gameObject to be spawned
    public float radius;            //distance from player to spawn
    private Vector2 position;       //player's position
    public GameObject target;       //target to spawn around (a.k.a. player)
    public int howManyGobbos;       //number of goblins to spawn
    private float[] boundaries = { -90, 1000, -120, 300 };


    public void SummonCircleOfGobbos()
    {
        //GameObject.Find("Boss_Attack_Canvas/Next_Attack").GetComponent<Text>().text = "Get Em Boys!";
        if(target != null)
        {
            position = target.GetComponent<Transform>().position;
            for (int i = 0; i < howManyGobbos; i++)
            {
                //spawn point for a specific goblin
                Vector2 spawnPoint = RandomCircle(position, radius);

                //spawn one goblin at that spawn point
                Instantiate(gobbo, spawnPoint, Quaternion.identity);

            }
        }

    }

    //chooses a random point a certain distance from the player
    private Vector3 RandomCircle(Vector2 center, float radius)
    {
        float ang = UnityEngine.Random.value * 360;
        Vector2 pos;

        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);

        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);

        if (pos.x < boundaries[0] || pos.x > boundaries[1])
        {
            pos.x = center.x + radius * -1 * Mathf.Sin(ang * Mathf.Deg2Rad);
        }
        if (pos.y < boundaries[2] || pos.y > boundaries[3])
        {
            pos.y = center.y + radius * -1 * Mathf.Cos(ang * Mathf.Deg2Rad);
        }

        return pos;
    }

    //helps visualize the distance
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(target.GetComponent<Transform>().position, radius);
    }
}
