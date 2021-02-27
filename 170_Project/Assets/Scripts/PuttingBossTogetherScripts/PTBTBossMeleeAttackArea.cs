using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*Boss Attack: Player is damaged if they are within a certain distance from the boss
 and some triggering event occurs after the attack's cooldown period
 Assumes player has the Player_Stats.cs component*/
public class PTBTBossMeleeAttackArea : MonoBehaviour
{

    public float attackRadius;          //radius the attack covers
    public LayerMask targets;           //list things this attack can damage
    public Transform attackPos;         //center point of damage circle
    public int damage = 1;

    // Update is called once per frame
    public void Melee()
    {
        //GameObject.Find("Boss_Attack_Canvas/Next_Attack").GetComponent<Text>().text = "Shouldn't have gotten close";
        //Collect a list of all the colliders found in the circle
        Collider2D[] attackArea = Physics2D.OverlapCircleAll(attackPos.position, attackRadius, targets);
        foreach (var target in attackArea)
        {
            //if the target has the Player_Stats component register a hit
            if (target.GetComponent<Updated_Player_Stats>())
            {
                target.GetComponent<Updated_Player_Stats>().gotHit(damage);

            }
        }
        
    }
    //helps visualize the damage area, attached to an empty gameobject attached to boss
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRadius);
    }
}
