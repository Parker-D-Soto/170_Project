using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Stats
{
   
    
    private string announcement;

    private bool enabled;

    private float speed;

    private float damage;

    private float spawned;

    private float timer;

    private float reps;

    public Attack_Stats(string baseAnnouncement = "test test test", bool baseEnable = false, float baseSpeed = 15, float baseDamage = 1, float baseSpawned = 3, float baseTime = 2, float baseReps = 1)
    {
        announcement = baseAnnouncement;
        enabled = baseEnable;
        speed = baseSpeed;
        damage = baseDamage;
        spawned = baseSpawned;
        timer = baseTime;
        reps = baseReps;
    }

    public string GetAnnouncement()
    {
        return announcement;
    }

    public void SetAnnouncement(string saying)
    {
        announcement = saying;
    }

    public bool GetEnabled()
    {
        return enabled;
    }

    public void SetEnabled(bool enable)
    {
        enabled = enable;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void ModifySpeed(float faster)
    {
        speed += faster;
    }

    public float GetDamage()
    {
        return damage;
    }

    public void ModifyDamage(float moreDamage)
    {
        damage += moreDamage;
    }

    public float GetSpawned()
    {
        return spawned;
    }

    public void ModifySpawned(float moreSpawned)
    {
        spawned += moreSpawned;
    }

    public float GetTimer()
    {
        return timer;
    }

    public void ModifyTimer(float moreTime)
    {
        timer += moreTime;
    }

    public float GetReps()
    {
        return reps;
    }

    public void ModifyReps(float moreReps)
    {
        reps += moreReps;
    }

    public override string ToString()
    {
        string stats = "Announcement: " + announcement + ", " + "Enabled: " + enabled + ", " + "Speed: " + speed + ", "
            + "Damage: " + damage + ", " + "Spawned: " + spawned + ", " + "Timer: " + timer + ", " + "Time until Repeat: " + reps;
        return stats;
    }
}
