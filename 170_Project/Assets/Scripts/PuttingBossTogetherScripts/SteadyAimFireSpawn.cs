using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SteadyAimFireSpawn : MonoBehaviour
{
    public GameObject fireSquad;
    public int gobbosInFireSquad = 3;
    public Transform player;

    public void SpawnFireSquadNearPlayer(float spawned, float speed, float damage, float duration, float reps)
    {
        gobbosInFireSquad = (int)spawned;

        //GameObject.Find("Boss_Attack_Canvas/Next_Attack").GetComponent<Text>().text = "Steady...Aim..FIRE";
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
        GameObject[] shooters = GameObject.FindGameObjectsWithTag("FireGobbo");
        foreach (GameObject shooter in shooters)
        {
            shooter.GetComponent<Destroyer>().selfDestructTimer = duration;
            shooter.GetComponent<enemyFire>().SetSpeed(speed);
            shooter.GetComponent<enemyFire>().SetDamage(damage);
            shooter.GetComponent<enemyFire>().startTimeBtwShots = reps;
        }
    }
}
