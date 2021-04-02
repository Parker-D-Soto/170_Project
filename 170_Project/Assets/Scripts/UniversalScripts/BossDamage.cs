using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    public GameObject boss;
    // Start is called before the first frame update
    public int damage = 1;

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "clone")
        {
            gameObject.GetComponent<Updated_Boss_Stats>().Modify_Health(-1*damage);
            Debug.Log("boss takes damage");
            Destroy(hitInfo.gameObject);
        }
    }
}
