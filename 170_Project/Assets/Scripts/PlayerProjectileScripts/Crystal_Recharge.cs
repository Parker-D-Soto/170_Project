using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_Recharge : MonoBehaviour
{

    private bool rechargCryst;
    public Animator anim;
    public CircleCollider2D col;

    public float secondsToWait = 30;
    
    void Start()
    {
        rechargCryst = false;
        anim.SetBool("recharging", rechargCryst);
    }

    // Update is called once per frame
    public void Toggle_Recharge()
    {
        //Debug.Log("Recharge Getting Called");
        rechargCryst = !rechargCryst;
        col.enabled = !col.enabled;
        anim.SetBool("recharging", rechargCryst);
        if (rechargCryst)
        {
            Invoke("Toggle_Recharge", secondsToWait);
        }
    }

    public bool GetRechBool()
    {
        return rechargCryst;
    }
}
