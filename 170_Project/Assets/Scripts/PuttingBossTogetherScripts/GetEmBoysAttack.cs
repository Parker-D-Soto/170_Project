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
    
    
    public void SummonCircleOfGobbos()
    {
        //GameObject.Find("Boss_Attack_Canvas/Next_Attack").GetComponent<Text>().text = "Get Em Boys!";
        position = target.GetComponent<Transform>().position;
        for (int i = 0; i < howManyGobbos; i++)
        {
            //spawn point for a specific goblin
            Vector2 spawnPoint = RandomCircle(position, radius);

            //spawn one goblin at that spawn point
            Instantiate(gobbo, spawnPoint, Quaternion.identity);

        }
    }

    //chooses a random point a certain distance from the player
    private Vector3 RandomCircle(Vector2 center, float radius)
    {
        float ang = UnityEngine.Random.value * 360;
        Vector2 pos;

        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);

        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);

        return pos;
    }

    //helps visualize the distance
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(target.GetComponent<Transform>().position, radius);
    }
}
