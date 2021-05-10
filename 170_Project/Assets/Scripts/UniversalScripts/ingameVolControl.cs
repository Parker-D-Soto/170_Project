using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ingameVolControl : MonoBehaviour
{
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volFloat");
    }

    // Update is called once per frame
    void Update()
    {
        //SetVolValue(PlayerPrefs.GetFloat("volFloat"));
        //Debug.Log("PlayerPrefab = " + PlayerPrefs.GetFloat("volFloat"));

        slider.value = PlayerPrefs.GetFloat("volFloat");
    }

    public void SetgameLevel(float sliderValue){
        PlayerPrefs.SetFloat("volFloat",sliderValue);
        Debug.Log("PlayerPrefab = " + PlayerPrefs.GetFloat("volFloat"));
    }

    /*public static void SetVolValue(float sliderValue){
        audioSrc.volume = sliderValue; //0-1 value
        Debug.Log("volume changed to: " + sliderValue);
    }*/
}
