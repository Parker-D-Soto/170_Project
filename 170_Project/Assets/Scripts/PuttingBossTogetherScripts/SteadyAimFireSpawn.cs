﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SteadyAimFireSpawn : MonoBehaviour
{
    public GameObject fireSquad;
    public int gobbosInFireSquad = 3;
    public Transform player;

    public void SpawnFireSquadNearPlayer()
    {
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
    }
}
