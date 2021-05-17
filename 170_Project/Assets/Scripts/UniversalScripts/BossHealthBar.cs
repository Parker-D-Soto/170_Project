using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider myhealthBar;

    public Text healthText;

    public void ModifySlider(int newHealth)
    {
        myhealthBar.value = newHealth;

        healthText.text = myhealthBar.value + " / " + myhealthBar.maxValue;
    }

    public void UpdateMaxHealth(int newMaxHealth)
    {
        myhealthBar.maxValue = newMaxHealth;

        myhealthBar.value = newMaxHealth;

        healthText.text = myhealthBar.value + " / " + myhealthBar.maxValue;
    }
}
