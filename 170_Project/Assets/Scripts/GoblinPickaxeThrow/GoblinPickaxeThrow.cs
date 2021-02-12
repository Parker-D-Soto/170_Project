using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinPickaxeThrow : MonoBehaviour
{
    public GameObject pickaxe;

    float fireRate;
    float timeForNextAttack;
    public static bool attackEnable;
    // Start is called before the first frame update
    void Start()
    {
        attackEnable = true;
        fireRate = 2f;
        timeForNextAttack = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackEnable == true)
        {
            ThrowPickaxe();
        }
    }

    void ThrowPickaxe()
    {
        if (Time.time > timeForNextAttack)
        {
            Instantiate(pickaxe, transform.position, Quaternion.identity);
            timeForNextAttack = Time.time + fireRate;
        }
    }
}
