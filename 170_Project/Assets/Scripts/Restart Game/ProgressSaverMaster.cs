using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressSaverMaster : MonoBehaviour
{

    private static ProgressSaverMaster instance;

    private GoblinBossStats bossSaves;
    //public Updated_Player_Stats playerSaves;

    public bool startWithDialogue = true;

    public Dictionary<string, bool> goblinAttacks;
    public int goblinStartHealth;
    public float goblinCooldown;
    public float goblinStartup;
    public float goblinSpeed;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        bossSaves = GameObject.FindGameObjectWithTag("boss").GetComponent<GoblinBossStats>();
        goblinStartHealth = bossSaves.health;
        goblinCooldown = bossSaves.cooldown;
        goblinStartup = bossSaves.startup;
        goblinSpeed = bossSaves.speed;
    }

    public void SaveStats(bool saving)
    {
        //Debug.Log("SAVING SAVING SAVING");
        startWithDialogue = false;
        if (saving)
        {
            Debug.Log("Saving");
            /*foreach (KeyValuePair<string, bool> attack in bossSaves.attacks)
            {
                string attackName = attack.Key;
                bool enabled;
                if (attack.Value)
                {
                    enabled = true;
                }
                else
                {
                    enabled = false;
                }
                goblinAttacks.Add(attackName, enabled);
            }*/
            goblinAttacks = new Dictionary<string, bool>(bossSaves.attacks);
            goblinStartHealth = bossSaves.health;
            goblinCooldown = bossSaves.cooldown;
            goblinStartup = bossSaves.startup;
            goblinSpeed = bossSaves.speed;
        }
        
    }
}
