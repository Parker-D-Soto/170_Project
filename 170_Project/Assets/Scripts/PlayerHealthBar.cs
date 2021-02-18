using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider myhealthBar;

    void Start()
    {
        myhealthBar.value = gameObject.GetComponent<Updated_Player_Stats>().Check_Health();
    }

    void Update()
    {
        myhealthBar.value = gameObject.GetComponent<Updated_Player_Stats>().Check_Health();
    }
}
