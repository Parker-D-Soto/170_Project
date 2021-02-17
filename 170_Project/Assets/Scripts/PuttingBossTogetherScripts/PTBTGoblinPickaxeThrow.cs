using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTBTGoblinPickaxeThrow : MonoBehaviour
{
    public GameObject pickaxe;
    float timeForNextThrow;
    public int howManyThrows = 1;
    public float durationOfAttack = 1;
    float timeInAttack = 0;
    int pickaxeThrown = 0;

    public void ThrowPickaxe()
    {
        timeForNextThrow = durationOfAttack / (float)howManyThrows;
        Debug.Log("timeForNextThrow: "+timeForNextThrow);
        while (pickaxeThrown < howManyThrows)
        {
            if (timeInAttack >= timeForNextThrow)
            {
                Debug.Log("timeInAttack: " + timeInAttack);
                timeInAttack = 0f;
                Debug.Log("timeInAttack: " + timeInAttack);
                Instantiate(pickaxe, gameObject.transform.position, Quaternion.identity);
                pickaxeThrown++;
                Debug.Log("pickaxe thrown: " + pickaxeThrown);
            }
            else
            {
                timeInAttack += Time.deltaTime;
                Debug.Log("timeInAttack: " + timeInAttack);

            }

        }

        pickaxeThrown = 0;
            
    }
}
