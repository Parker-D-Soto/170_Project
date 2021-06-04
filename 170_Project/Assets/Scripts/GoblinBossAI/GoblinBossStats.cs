using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Panda;
public class GoblinBossStats : Updated_Boss_Stats
{
    //Attack Enable/Disable Function
    public override void SearchAttacks(string potentialAttack, bool isEnabled)
    {
        //Debug.Log("Potential Attack: " + potentialAttack + ", isEnabled: " + isEnabled);
        if (attacks.ContainsKey(potentialAttack))
        {
            attacks[potentialAttack] = isEnabled;
            phases[potentialAttack] = isEnabled;
        }
        //Debug.Log("Potential Attack: " + potentialAttack + ", isEnabled: " + attacks[potentialAttack]);
    }

    private void makeNoise(string clip)
    {
        SoundManagerScript.PlaySound(clip);
    }

    //Behavior Tree Board Checking Tasks Start
    //Check if specific attack is enabled
    [Task]
    public void CheckAttacks(string checkedAttack)
    {
        //Debug.Log("Attack: " + checkedAttack + ", Enabled: " + attacks[checkedAttack]);
        if (phases[checkedAttack] == true)
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
    
    [Task]
    public void PickaxeThrow()
    {
        makeNoise("BossThrow");
        gameObject.GetComponent<PTBTGoblinPickaxeThrow>().ThrowPickaxe();
        Task.current.Succeed();
    }
    
    [Task]
    public void GoblinCharge()
    {
        gameObject.GetComponent<PTBTChargeGobboSpawn>().SpawnChargeGobboNearPlayer();
        Task.current.Succeed();
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

  /*  [Task]
    public void Melee()
    {
        gameObject.GetComponent<PTBTBossMeleeAttackArea>().Melee();
        Task.current.Succeed();

    }*/

    [Task]
    public void AnnounceAttack(string attack)
    {
        switch (attack)
        {
            case "steadyAimFire":
                GameObject.Find("Boss_Attack_Canvas/Next_Attack").GetComponent<Text>().text = "Steady...Aim..FIRE";
                break;
            case "getEmBoys":
                GameObject.Find("Boss_Attack_Canvas/Next_Attack").GetComponent<Text>().text = "Get Em Boys!";
                break;
            case "surroundEm":
                GameObject.Find("Boss_Attack_Canvas/Next_Attack").GetComponent<Text>().text = "Surround em boys";
                break;
            case "pickaxeThrow":
                GameObject.Find("Boss_Attack_Canvas/Next_Attack").GetComponent<Text>().text = "Have a Pickaxe";
                break;
            case "goblinCharge":
                GameObject.Find("Boss_Attack_Canvas/Next_Attack").GetComponent<Text>().text = "Charge the enemy";
                break;
            default:
                Debug.Log("UnknownAttack: " + attack);
                Task.current.Fail();
                break;
        }
        Task.current.Succeed();
    }
}
