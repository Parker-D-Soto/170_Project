using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Panda;
public class GoblinBossStats : Updated_Boss_Stats
{

    public Dictionary<string, Attack_Stats> attacks = new Dictionary<string, Attack_Stats>(StringComparer.OrdinalIgnoreCase) {
        //{"melee", true},
        {"pickaxeThrow", new Attack_Stats("Have a Pickaxe",true, 50, 1, 1, 1)},
        {"goblinCharge", new Attack_Stats("Charge the enemy", true, 50) },
        {"getEmBoys", new Attack_Stats("Get Em Boys!", false, 50, 1, 2, 20)},
        {"steadyAimFire", new Attack_Stats("Steady...Aim..FIRE", false, 30, 1, 3, 5, 2)},
        {"surroundEm", new Attack_Stats("Surround em boys", false, 15, 1, 5)}
    };
    //Attack Enable/Disable Function
    public override void SearchAttacks(string potentialAttack, List<(string, float)> attacksList, bool isEnabled = true)
    {
        if (attacks.ContainsKey(potentialAttack))
        {
            attacks[potentialAttack].SetEnabled(isEnabled);
        }
        if (attacksList.Count > 0)
        {
            foreach ((string, float) attackChange in attacksList)
            {
                string stat = attackChange.Item1;
                switch (stat)
                {
                    case "damage":
                        attacks[potentialAttack].ModifyDamage(attackChange.Item2);
                        break;
                    case "speed":
                        attacks[potentialAttack].ModifySpeed(attackChange.Item2);
                        break;
                    case "spawn":
                        attacks[potentialAttack].ModifySpawned(attackChange.Item2);
                        break;
                    case "timer":
                        attacks[potentialAttack].ModifyTimer(attackChange.Item2);
                        break;
                    default:
                        Debug.Log("Bad attack Stat: " + stat);
                        break;
                }
            }
        }
    }


    //Behavior Tree Board Checking Tasks Start
    //Check if specific attack is enabled
    [Task]
    public void CheckAttacks(string checkedAttack)
    {
        if (attacks[checkedAttack].GetEnabled() == true)
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
        Attack_Stats pickaxeStats = attacks["pickaxeThrow"];
        Debug.Log("Pickaxe: " + pickaxeStats.GetSpawned());
        gameObject.GetComponent<PTBTGoblinPickaxeThrow>().ThrowPickaxe(pickaxeStats.GetSpawned(),pickaxeStats.GetTimer(), pickaxeStats.GetDamage(),pickaxeStats.GetSpeed());
        Task.current.Succeed();
    }
    
    [Task]
    public void GoblinCharge()
    {
        Attack_Stats chargeStats = attacks["goblinCharge"];
        Debug.Log("Goblin Charge: " + chargeStats.GetSpawned());
        gameObject.GetComponent<PTBTChargeGobboSpawn>().SpawnChargeGobboNearPlayer(chargeStats.GetSpawned(),chargeStats.GetTimer(),chargeStats.GetDamage(),chargeStats.GetSpeed());
        Task.current.Succeed();
    }

    [Task]
    public void SurroundEm()
    {
        Attack_Stats surroundStats = attacks["surroundEm"];
        Debug.Log("SurroundEm: " + surroundStats.GetSpawned());
        gameObject.GetComponent<PTBTSurroundEmAttack>().SummonCircleOfGobbos(surroundStats.GetSpawned(),surroundStats.GetSpeed(),surroundStats.GetDamage());
        Task.current.Succeed();
    }

    [Task]
    public void GetEmBoys()
    {
        Attack_Stats getEmStats = attacks["getEmBoys"];
        Debug.Log("GetEm: " + getEmStats.GetSpawned());
        gameObject.GetComponent<GetEmBoysAttack>().SummonCircleOfGobbos(getEmStats.GetSpawned(), getEmStats.GetSpeed(), getEmStats.GetDamage(), getEmStats.GetTimer());
        Task.current.Succeed();

    }

    [Task]
    public void SteadyAimFire()
    {
        Attack_Stats steadyStats = attacks["steadyAimFire"];
        Debug.Log("SteadyAimFire: " + steadyStats.GetSpawned());
        gameObject.GetComponent<SteadyAimFireSpawn>().SpawnFireSquadNearPlayer(steadyStats.GetSpawned(),steadyStats.GetSpeed(),steadyStats.GetDamage(), steadyStats.GetTimer(), steadyStats.GetReps());
        Task.current.Succeed();

    }

    [Task]
    public void Melee()
    {
        gameObject.GetComponent<PTBTBossMeleeAttackArea>().Melee();
        Task.current.Succeed();

    }

    [Task]
    public void AnnounceAttack(string attack)
    {
        if (attacks.ContainsKey(attack))
        {
            GameObject.Find("Boss_Attack_Canvas/Next_Attack").GetComponent<Text>().text = attacks[attack].GetAnnouncement();
            Task.current.Succeed();
        }
        else
        {
            Debug.Log("UnknownAttack: " + attack);
            Task.current.Fail();
        }
        /*switch (attack)
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
        Task.current.Succeed();*/
    }
}
