using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SteadyAimFireSpawn : MonoBehaviour
{
    public GameObject fireSquad;
    public int gobbosInFireSquad = 3;
    public Transform playerPosition;

    public void SpawnFireSquadNearPlayer()
    {
        List <GameObject> spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint").ToList<GameObject>();
        for (int i = 0; i < gobbosInFireSquad; i++)
        {
            GameObject closest = spawnPoints.First();
            float shortestDistance = 1000;

            foreach (GameObject spawnPoint in spawnPoints)
            {
                float distance = Vector2.Distance(spawnPoint.transform.position,playerPosition.position);
                if(distance < shortestDistance)
                {
                    closest = spawnPoint;
                }
            }

            Instantiate(fireSquad, closest.transform.position, Quaternion.identity);
            spawnPoints.Remove(closest);
        }
    }
}
