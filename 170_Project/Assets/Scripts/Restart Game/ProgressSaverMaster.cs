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
        goblinAttacks = bossSaves.attacks;
        goblinStartHealth = bossSaves.health;
        goblinCooldown = bossSaves.cooldown;
        goblinStartup = bossSaves.startup;
        goblinSpeed = bossSaves.speed;
    }

    public void SaveStats()
    {
        //Debug.Log("SAVING SAVING SAVING");
        startWithDialogue = false;
        goblinAttacks = bossSaves.attacks;
        goblinStartHealth = bossSaves.health;
        goblinCooldown = bossSaves.cooldown;
        goblinStartup = bossSaves.startup;
        goblinSpeed = bossSaves.speed;
    }
}
