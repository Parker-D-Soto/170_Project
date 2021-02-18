using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    public Slider myhealthBar;

    void Start()
    {
        myhealthBar.value = gameObject.GetComponent<Destroyer>().AnnounceSelfDestruct();
    }

    void Update()
    {
        myhealthBar.value = gameObject.GetComponent<Destroyer>().AnnounceSelfDestruct();
    }
}
