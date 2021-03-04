using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PTBTChargeGobboSpawn : MonoBehaviour
{
    public GameObject fireSquad;
    public int gobbosInFireSquad = 3;
    public Transform player;

    public void SpawnChargeGobboNearPlayer(float spawned, float duration, float damage, float speed)
    {
        gobbosInFireSquad = (int)spawned;
        //GameObject.Find("Boss_Attack_Canvas/Next_Attack").GetComponent<Text>().text = "Charge the enemy";
        //GameObject.Find("Boss_Attack_Canvas/Next_Attack").GetComponent<Text>().text = "Get Em Boys!";
        Vector3 playerPosition = player.position;
        List <GameObject> spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint").ToList<GameObject>();
        for (int i = 0; i < gobbosInFireSquad; i++)
        {
            GameObject closest = spawnPoints.First();
            float shortestDistance = Mathf.Infinity;

            foreach (GameObject spawnPoint in spawnPoints)
            {
                float distance = Vector2.Distance(spawnPoint.transform.position,playerPosition);
                if(distance < shortestDistance)
                {
                    closest = spawnPoint;
                    shortestDistance = distance;
                }
            }

            Instantiate(fireSquad, closest.transform.position, Quaternion.identity);
            spawnPoints.Remove(closest);
        }

        GameObject[] chargers = GameObject.FindGameObjectsWithTag("ChargeGobbo");
        foreach (GameObject charger in chargers)
        {
            //charger.GetComponent<Destroyer>().selfDestructTimer = duration;
            charger.GetComponent<PTBTEnemy_Charge>().damage = (int)damage;
            charger.GetComponent<PTBTEnemy_Charge>().runSpeed = speed;
        }
    }
}
