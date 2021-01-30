using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*Boss Attack: Player is damaged if they are within a certain distance from the boss
 and some triggering event occurs after the attack's cooldown period
 Assumes player has the Player_Stats.cs component*/
public class BossMeleeAttackArea : MonoBehaviour
{
    private float attackCooldown;       //cooldown timer
    public float startAttackCooldown;   //cooldown time
    public float attackRadius;          //radius the attack covers
    public LayerMask targets;           //list things this attack can damage
    public Transform attackPos;         //center point of damage circle
    public int damage;

    // Update is called once per frame
    void Update()
    {
        //Something has to trigger the event and some time must have passed since this
        //attack was used
        if (AttackEvent() && attackCooldown <= 0)
        {
            //Collect a list of all the colliders found in the circle
            Collider2D[] attackArea = Physics2D.OverlapCircleAll(attackPos.position, attackRadius, targets);
            attackCooldown = startAttackCooldown;
            foreach (var target in attackArea)
            {
                //if the target has the Player_Stats component register a hit
                if (target.GetComponent<Player_Stats>())
                {
                    target.GetComponent<Player_Stats>().gotHit();

                }
            }
        }
        else
        {
            //timer countdown
            attackCooldown -= Time.deltaTime;
        }
    }
    //helps visualize the damage area, attached to an empty gameobject attached to boss
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRadius);
    }
    //triggering event code, currently pressing space for testing purposes
    bool AttackEvent()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            return true;
        }
        return false;
        
    }
}
