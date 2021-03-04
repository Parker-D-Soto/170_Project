using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PTBTGoblinPickaxeThrow : MonoBehaviour
{
    public GameObject pickaxe;
    float timeForNextThrow;
    public float howManyThrows = 1;
    public float durationOfAttack = 1;
    float timeInAttack = 0;
    int pickaxeThrown = 0;

    public void ThrowPickaxe(float numberOfThrows, float attackLengthOfTime, float damage, float speed)
    {
        howManyThrows = numberOfThrows;
        durationOfAttack = attackLengthOfTime;
        //GameObject.Find("Boss_Attack_Canvas/Next_Attack").GetComponent<Text>().text = "Have a Pickaxe";
        timeForNextThrow = durationOfAttack / howManyThrows;
        //Debug.Log("timeForNextThrow: "+timeForNextThrow);
        while (pickaxeThrown < howManyThrows)
        {
            if (timeInAttack >= timeForNextThrow)
            {
                //Debug.Log("timeInAttack: " + timeInAttack);
                timeInAttack = 0f;
                //Debug.Log("timeInAttack: " + timeInAttack);
                Instantiate(pickaxe, gameObject.transform.position, Quaternion.identity);
                pickaxeThrown++;
                //Debug.Log("pickaxe thrown: " + pickaxeThrown);
            }
            else
            {
                timeInAttack += Time.deltaTime;
                //Debug.Log("timeInAttack: " + timeInAttack);

            }

        }

        pickaxeThrown = 0;

        GameObject[] pickaxes = GameObject.FindGameObjectsWithTag("GoblinPickaxe");
        foreach (GameObject pickaxe in pickaxes)
        {
            pickaxe.GetComponent<PTBTPickaxeCollisionScript>().moveSpeed = speed;
            pickaxe.GetComponent<PTBTPickaxeCollisionScript>().damage = (int)damage;
        }
            
    }
}
