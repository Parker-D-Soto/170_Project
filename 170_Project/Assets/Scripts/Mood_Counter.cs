using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mood_Counter : MonoBehaviour
{
    int currentCounter;

    Transform parent;

    private void Awake()
    {
        currentCounter = 0;

        parent = this.transform.parent;
    }

    public void IncrementAndUpdate()
    {
        currentCounter++;

        this.GetComponent<UnityEngine.UI.Text>().text = currentCounter.ToString();
    }
}
