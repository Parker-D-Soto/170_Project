using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider myhealthBar;

    void Start()
    {
        myhealthBar.value = gameObject.GetComponent<Updated_Boss_Stats>().health;
    }

    void Update()
    {
        myhealthBar.value = gameObject.GetComponent<Updated_Boss_Stats>().health;
    }
}
