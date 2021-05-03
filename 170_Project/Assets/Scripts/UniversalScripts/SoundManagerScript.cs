using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip DialogueBG, FightBG;
    public static AudioSource audioSrc;
    public AudioMixer mixer;
    //public float vol;
    float value;

    // Start is called before the first frame update
    void Start()
    {
        //loading sounds from sound folder
        FightBG = Resources.Load<AudioClip> ("oldversion-beat6");
        DialogueBG = Resources.Load<AudioClip> ("beat3");
        
        audioSrc = GetComponent<AudioSource> ();
        mixer.GetFloat("MusicVol", out value);
    }

    // Update is called once per frame
    void Update()
    {
        mixer.GetFloat("MusicVol", out value);

        //mixer starts off at 0, so if player didnt change volume then default is 0.3f
        if(value == 0){
            value = 0.3f;
        }
        
        SetVolValue(value);    
    }

    public static void PlaySound (string clip){
        switch(clip){
            case "Fight":
                //audioSrc.AudioClip("oldverison-beat6");
                audioSrc.PlayOneShot(FightBG);
                break;
            case "Dialogue":
                audioSrc.PlayOneShot(DialogueBG);
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
        } 
    }

    public static void SetVolValue(float sliderValue){
        audioSrc.volume = sliderValue; //0-1 value
        Debug.Log("volume changed to: " + sliderValue);
    }
}
