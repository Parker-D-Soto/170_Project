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
    public GameObject eyes;
    Vector3[] positions;
    GameObject[] eyeLocations;
    public void SpawnChargeGobboNearPlayer()
    {
        //GameObject.Find("Boss_Attack_Canvas/Next_Attack").GetComponent<Text>().text = "Charge the enemy";

        //GameObject.Find("Boss_Attack_Canvas/Next_Attack").GetComponent<Text>().text = "Get Em Boys!";
        if(player != null)
        {
            Vector3 playerPosition = player.position;
            positions = new Vector3[gobbosInFireSquad];
            eyeLocations = new GameObject[gobbosInFireSquad];
            List<GameObject> spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint").ToList<GameObject>();
            for (int i = 0; i < gobbosInFireSquad; i++)
            {
                GameObject closest = spawnPoints.First();
                float shortestDistance = Mathf.Infinity;

                foreach (GameObject spawnPoint in spawnPoints)
                {
                    float distance = Vector2.Distance(spawnPoint.transform.position, playerPosition);
                    if (distance < shortestDistance)
                    {
                        closest = spawnPoint;
                        shortestDistance = distance;
                    }
                }
                if(closest.transform.position.y >= 303)
                {
                    eyeLocations[i] = Instantiate(eyes, closest.transform.position, Quaternion.identity);
                }
                else
                {
                    eyeLocations[i] = Instantiate(eyes, closest.transform.position, Quaternion.AngleAxis(90, new Vector3(0, 0, 1)));
                }
                positions[i] = closest.transform.position;
                spawnPoints.Remove(closest);
            }

            Invoke("Spawnner", 0.5f);
        }
        
    }


    //WILL PROBABLY CHANGE THIS WHEN NEW SPRITES ARE IN
    private void Spawnner()
    {
        foreach (Vector3 point in positions)
        {
            foreach (GameObject eye in eyeLocations)
            {
                Destroy(eye);
            }

            if(point.y >= 303)
            {
                Instantiate(fireSquad, point, Quaternion.identity);
                
            }
            else if(point.x < 0)
            {
                Instantiate(fireSquad, point, Quaternion.AngleAxis(90, new Vector3(0, 0, 1)));
            }
            else
            {
                Instantiate(fireSquad, point, Quaternion.AngleAxis(90, new Vector3(0, 0, 1)));
            }
            
        }
        
    }

    //dies with one shot if hit by player
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            //Debug.Log("die from one hit");
            Object.Destroy(this.gameObject);
        }
    
    }
}
