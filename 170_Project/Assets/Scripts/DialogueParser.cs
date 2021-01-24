using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueParser : MonoBehaviour
{

    [SerializeField] private DialogueContainer dialogue;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button currentButton;
    [SerializeField] private Transform buttonContainer;
    [SerializeField] private TextMeshProUGUI effectTest;

    // Start is called before the first frame update
    void Start()
    {
        var narrativeData = dialogue.NodeLinks.First();
        ProceedToNarrative(narrativeData.TargetNodeGuid);
    }

    private void ProceedToNarrative(string narrativeDataGUID)
    {
        if(dialogue.DialogueNodeData.Find(x => x.GUID == narrativeDataGUID).ExitPoint)
        {
            //go to next scene
            GoToNextScene();
        }
        var text = dialogue.DialogueNodeData.Find(x => x.GUID == narrativeDataGUID).DialogueText;
        var choices = dialogue.NodeLinks.Where(x => x.BaseNodeGuid == narrativeDataGUID);
        dialogueText.text = ProcessProperties(text);
        effectTest.text = dialogue.DialogueNodeData.Find(x => x.GUID == narrativeDataGUID).Mutation;
        var buttons = buttonContainer.GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            Destroy(buttons[i].gameObject);
        }
        

        foreach(var choice in choices)
        {
            var button = Instantiate(currentButton, buttonContainer);
            button.GetComponentInChildren<Text>().text = ProcessProperties(choice.PortName);
            button.onClick.AddListener(() => ProceedToNarrative(choice.TargetNodeGuid));
            
            
        }
    }

    private void GoToNextScene()
    {
        throw new NotImplementedException();
    }

    private string ProcessProperties(string text)
    {
        foreach(var exposedProperty in dialogue.ExposedProperties)
        {
            text = text.Replace($"[{exposedProperty.PropertyName}]", exposedProperty.PropertyValue);
        }
        return text;
    }
}
