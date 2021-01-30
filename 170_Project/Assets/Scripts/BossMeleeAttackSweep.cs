using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Alternate version of Melee attack where hurtbox sweeps from one corner of the boss to the other: INCOMPLETE
//See BossMeleeAttackArea for more detailed descriptions
public class BossMeleeAttackSweep : MonoBehaviour
{
    private float attackCooldown;
    public float startAttackCooldown;
    public float attackRadius;
    public LayerMask targets;
    public Transform attackPos;
    public int damage;

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        if (AttackEvent() && attackCooldown <= 0)
        {
            
            Collider2D[] attackArea = Physics2D.OverlapCircleAll(attackPos.position, attackRadius, targets);
            attackCooldown = startAttackCooldown;
            foreach (var target in attackArea)
            {
                target.GetComponent<Player_Stats>().gotHit();
                Debug.Log("player in range");
            }
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    private void ProcessInput()
    {
        var facing = Input.GetAxisRaw("Horizontal");
        if (facing == 0)
        {

        }

        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRadius);
    }

    bool AttackEvent()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            return true;
        }
        return false;
        
    }
}
