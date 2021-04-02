using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private float timeTillSelfDestruct;
    public float selfDestructTimer;
    public bool destroyWhenOffScreen;
    // Start is called before the first frame update
    void Start()
    {
        timeTillSelfDestruct = selfDestructTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!destroyWhenOffScreen)
        {
            if (timeTillSelfDestruct <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                timeTillSelfDestruct -= Time.deltaTime;
            }
        }

    }

    public float AnnounceSelfDestruct()
    {
        return timeTillSelfDestruct;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
