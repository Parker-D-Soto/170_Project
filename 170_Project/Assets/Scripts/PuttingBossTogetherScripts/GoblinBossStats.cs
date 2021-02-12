using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
public class GoblinBossStats : Updated_Boss_Stats
{

    public Dictionary<string, bool> attacks = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase) {
        {"melee", true},
        {"pickaxeThrow", true},
        {"goblinCharge", true },
        {"getEmBoys", false },
        {"steadyAimFire", false },
        {"surroundEm", false }
    };
    //Attack Enable/Disable Function
    public override void SearchAttacks(string potentialAttack, bool isEnabled)
    {
        if (attacks.ContainsKey(potentialAttack))
        {
            attacks[potentialAttack] = isEnabled;
        }
    }


    //Behavior Tree Board Checking Tasks Start
    //Check if specific attack is enabled
    [Task]
    public void CheckAttacks(string checkedAttack)
    {
        if (attacks[checkedAttack] == true)
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
    
    
    [Task]
    public void SurroundEm()
    {
        gameObject.GetComponent<PTBTSurroundEmAttack>().SummonCircleOfGobbos();
        Task.current.Succeed();
    }

    [Task]
    public void GetEmBoys()
    {
        gameObject.GetComponent<GetEmBoysAttack>().SummonCircleOfGobbos();
        Task.current.Succeed();

    }

    [Task]
    public void SteadyAimFire()
    {
        gameObject.GetComponent<SteadyAimFireSpawn>().SpawnFireSquadNearPlayer();
        Task.current.Succeed();

    }

    [Task]
    public void Melee()
    {
        gameObject.GetComponent<PTBTBossMeleeAttackArea>().Melee();
        Task.current.Succeed();

    }

}
