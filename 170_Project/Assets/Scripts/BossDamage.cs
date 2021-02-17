using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    public GameObject boss;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "clone")
        {
            Debug.Log("boss takes damage");
            Destroy(hitInfo.gameObject);
        }
    }
}
