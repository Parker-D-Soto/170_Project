using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Mood_Tracker : MonoBehaviour
{
    public enum Mood {Happy, Mad, Complacent};

    Button currentButton;

    Mood currentMood;

    private void Start()
    {
        ResetConversation();
        currentButton = this.transform.GetComponent<Button>();
        currentButton.onClick.AddListener(IncrementText);
        currentButton.onClick.AddListener(ResetConversation);
    }

    public void SetMood(Mood newMood)
    {
        currentMood = newMood;
    }

    public void ResetConversation()
    {
        Transform selectionButtons = transform.parent;

        int index = 0;

        List<Mood> moods = new List<Mood>() { Mood.Happy, Mood.Mad, Mood.Complacent};
        for(int i = 0; i < selectionButtons.childCount; i++)
        {
            Transform currentObj = selectionButtons.GetChild(i);

            index = Random.Range(0, moods.Count);

            Mood newMood = moods[index];

            Mood_Tracker currentMT = currentObj.GetComponent<Mood_Tracker>();

            //Debug.Log(currentObj);

            currentMT.SetMood(newMood);

            currentObj.GetComponentInChildren<Text>().text = newMood.ToString();

            moods.Remove(newMood);
        }
    }

    public void IncrementText()
    {
        switch (currentMood)
        {
            case Mood.Happy:
                GameObject.Find("Happy_Counter").GetComponent<Mood_Counter>().IncrementAndUpdate();
                break;
            case Mood.Mad:
                GameObject.Find("Mad_Counter").GetComponent<Mood_Counter>().IncrementAndUpdate();
                break;
            case Mood.Complacent:
                GameObject.Find("Complacent_Counter").GetComponent<Mood_Counter>().IncrementAndUpdate();
                break;
        }
    }


}
