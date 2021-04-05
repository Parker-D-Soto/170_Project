using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip DialogueBG, FightBG;
    public static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        //loading sounds from sound folder
        FightBG = Resources.Load<AudioClip> ("oldversion-beat6");
        DialogueBG = Resources.Load<AudioClip> ("beat3");
        
        audioSrc = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
