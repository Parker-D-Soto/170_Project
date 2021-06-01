using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;

    void Start()
    {
        //SoundManagerScript.PlaySound("Menu");
        slider.value = PlayerPrefs.GetFloat("volFloat");
    }

    public void SetLevel(float sliderValue){
        
       //SoundManagerScript.SetVolValue(Mathf.Log10(sliderValue)*20);
       //Debug.Log("slider value: " + Mathf.Log10(sliderValue)*20);
       Debug.Log("slider value: " + sliderValue);
       //mixer.SetFloat("MusicVol",Mathf.Log10(sliderValue)*20);
       mixer.SetFloat("MusicVol",sliderValue);
       //SoundManagerScript.SetVolValue(0.3f);
       PlayerPrefs.SetFloat("volFloat",sliderValue);
       Debug.Log("PlayerPrefab = " + PlayerPrefs.GetFloat("volFloat"));
       
       //if(Input.GetMouseButtonUp(0)){
           //SoundManagerScript.PlaySound("Smack");
       //}
       
       
       
    }
}
