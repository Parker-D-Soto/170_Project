using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    //Needs LineRenderer Object
    //Needs Points that will be connected by the line
    //In this instance it should be the Player and the Projectile shot by the player

    private LineRenderer lr;
    private List<Transform> points = new List<Transform>();

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void SetPoints(List<Transform> newPoints)
    {
        lr.positionCount = newPoints.Count;
        points = newPoints;
    }

    private void Update()
    {
        
        for(int i = 0; i < points.Count; i++)
        {
            if(points.Count == 0)
            {
                Debug.Log("I is 0");
            }
            lr.SetPosition(i, points[i].position);
        }
    }
}
