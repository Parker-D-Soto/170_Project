using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip DialogueBG, FightBG, CrystalSound, SmackSound, MenuBG, BossThrow, CrystalGrab, HookShoot;
    public static AudioSource audioSrc;
    //public AudioMixer mixer;
    //public float vol;
    float value;

    // Start is called before the first frame update
    void Start()
    {
        //loading sounds from sound folder
        FightBG = Resources.Load<AudioClip> ("Fight Scene");
        DialogueBG = Resources.Load<AudioClip> ("Dialogue Scene");
        CrystalSound = Resources.Load<AudioClip>("crystalFinishGrowth");
        SmackSound = Resources.Load<AudioClip>("bossSmack");
        MenuBG = Resources.Load<AudioClip>("Menu Scene");
        BossThrow = Resources.Load<AudioClip>("GoblinBossThrow");
        CrystalGrab = Resources.Load<AudioClip>("CrystalGrab");
        HookShoot = Resources.Load<AudioClip>("hookshot");


        audioSrc = GetComponent<AudioSource> ();
        audioSrc.loop = true;
        //mixer.GetFloat("MusicVol", out value);

        //slider.value = PlayerPrefs.GetFloat("volFloat");

        
    }

    // Update is called once per frame
    void Update()
    {
        /*mixer.GetFloat("MusicVol", out value);

        //mixer starts off at 0, so if player didnt change volume then default is 0.3f
        if(value == 0){
            value = 0.3f;
        }*/
        
        SetVolValue(PlayerPrefs.GetFloat("volFloat"));
        Debug.Log("PlayerPrefab = " + PlayerPrefs.GetFloat("volFloat"));

    }

    public static void PlaySound (string clip){
        switch(clip){
            case "Fight":
                //audioSrc.PlayOneShot(FightBG);
                if(audioSrc.clip != null){
                    audioSrc.Stop(); //stop previous audio clip from playing so only one clip plays at a time
                }
                audioSrc.clip = FightBG;
                audioSrc.Play();
                break;
            case "Dialogue":
                //audioSrc.PlayOneShot(DialogueBG);
                if(audioSrc.clip != null){
                    audioSrc.Stop(); //stop previous audio clip from playing so only one clip plays at a time
                }
                audioSrc.clip = DialogueBG;
                audioSrc.Play();
                break;
            case "Crystal":
                audioSrc.PlayOneShot(CrystalSound);
                break;
            case "Smack":
                audioSrc.PlayOneShot(SmackSound);
                break;
            case "BossThrow":
                audioSrc.PlayOneShot(BossThrow);
                break;
            case "Grab":
                audioSrc.PlayOneShot(CrystalGrab);
                break;
            case "Shoot":
                audioSrc.PlayOneShot(HookShoot);
                break;
            case "Menu":
                if(audioSrc.clip != null){
                    audioSrc.Stop(); //stop previous audio clip from playing so only one clip plays at a time
                }
                audioSrc.clip = MenuBG;
                audioSrc.Play();
                break;
        }
    }

    public static void StopSound (string clip){
       switch(clip){
            case "Fight":
                //audioSrc.AudioClip("oldverison-beat6");
                audioSrc.Stop();
                break;
            case "Dialogue":
                audioSrc.Stop();
                break;
            case "Menu":
                audioSrc.Stop();
                break;
        } 
    }

    public static void SetVolValue(float sliderValue){
        audioSrc.volume = sliderValue; //0-1 value
        Debug.Log("volume changed to: " + sliderValue);
    }

}
