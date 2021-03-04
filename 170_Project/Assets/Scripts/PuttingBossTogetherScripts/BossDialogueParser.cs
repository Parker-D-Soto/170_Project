using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossDialogueParser : MonoBehaviour
{

    [SerializeField] private DialogueContainer dialogue;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button currentButton;
    [SerializeField] private Transform buttonContainer;
    //[SerializeField] private TextMeshProUGUI effectTest;
    //[SerializeField] private TextMeshProUGUI bossStats;
    //[SerializeField] private TextMeshProUGUI bossAttacks;
    public GameObject Boss;
    //public GameObject UI;

    Regex rxName = new Regex(@"^\b((?<name>\w+))\b", RegexOptions.IgnoreCase);
    string statChanges = @"((-|\+)\d*)";
    string attackChanges = @",\s*([a-zA-Z]*?)\s*((-|\+)\d*)\s*";
    Regex rxEnabled = new Regex(@"(?<enabled>(dis|en)(able))\s*", RegexOptions.IgnoreCase);


    // Start is called before the first frame update
    void Start()
    {

        var narrativeData = dialogue.NodeLinks.First();
        ProceedToNarrative(narrativeData.TargetNodeGuid);
    }

    private void ProceedToNarrative(string narrativeDataGUID)
    {
        if (dialogue.DialogueNodeData.Find(x => x.GUID == narrativeDataGUID).ExitPoint)
        {
            //go to next scene
            GoToNextScene();
        }
        var text = dialogue.DialogueNodeData.Find(x => x.GUID == narrativeDataGUID).DialogueText;
        var choices = dialogue.NodeLinks.Where(x => x.BaseNodeGuid == narrativeDataGUID);
        dialogueText.text = ProcessProperties(text);
        /*For Debug Start
        effectTest.text = dialogue.DialogueNodeData.Find(x => x.GUID == narrativeDataGUID).Mutation;
        //For Debug End*/
        MutateBoss(dialogue.DialogueNodeData.Find(x => x.GUID == narrativeDataGUID).Mutation);
        /*For Debug Start
        string attackString= "";
        foreach (KeyValuePair<string, bool> attack in Boss.GetComponent<GoblinBossStats>().attacks)
        {
            attackString += "\n" + attack.Key + ": " + attack.Value;
        }
        bossStats.text = "Stats:"+"\nhealth: " + Boss.GetComponent<Updated_Boss_Stats>().health + "\ncooldown: " + Boss.GetComponent<Updated_Boss_Stats>().cooldown
                            + "\nstartup: " + Boss.GetComponent<Updated_Boss_Stats>().startup;
        bossAttacks.text = "attacks: " + attackString;
        //For Debug End*/
        var buttons = buttonContainer.GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            Destroy(buttons[i].gameObject);
        }


        foreach (var choice in choices)
        {
            var button = Instantiate(currentButton, buttonContainer);
            button.GetComponentInChildren<Text>().text = ProcessProperties(choice.PortName);
            button.onClick.AddListener(() => ProceedToNarrative(choice.TargetNodeGuid));


        }
    }

    private void MutateBoss(string mutation)
    { 
        string category;
        string numberChange;
        string attackNumbers;
        string attackStat;
        (string, float) attackPairing;
        List<(string, float)> attackModifiers = new List<(string, float)>();
        int number = 0;
        string attackEnabled;
        bool isEnabled = false;
        //Debug.Log("In MutateBoss");
        MatchCollection name = rxName.Matches(mutation);
        Match statChange = Regex.Match(mutation, statChanges, RegexOptions.IgnoreCase);
        MatchCollection attackChange = Regex.Matches(mutation, attackChanges, RegexOptions.IgnoreCase);
        Match enabled = rxEnabled.Match(mutation);
        /*//For Debug Start
        Debug.Log("name count: " + name.Count);
        foreach (var item in name)
        {
            Debug.Log("name: " + item);
        }
        Debug.Log("change count: " + attackChange.Count);
        foreach (Match item in attackChange)
        {
            Debug.Log("stat: " + item.Groups[1].Value);
            Debug.Log("value: " + item.Groups[2].Value);
        }
        
        Debug.Log("statChanges: " + statChange.Value);
        Debug.Log("enabled: " + enabled.Value);
        //For Debug End*/

        if (name.Count == 1)
        {
            category = name[0].Value;
            if (statChange.Success)
            {
                numberChange = statChange.ToString();
                number = Int32.Parse(numberChange);

            }
            if (enabled.Success)
            {
                attackEnabled = enabled.ToString();
                if (attackEnabled.Equals("enable"))
                {
                    isEnabled = true;
                }

            }

            foreach (Match attackItem in attackChange)
            {
                attackStat = attackItem.Groups[1].Value;
                attackNumbers = attackItem.Groups[2].Value;
                attackPairing = (attackStat, float.Parse(attackNumbers));
                attackModifiers.Add(attackPairing);
            }

            switch (category)
            {
                case "health":
                    Boss.GetComponent<Updated_Boss_Stats>().Modify_Health(number);
                    break;
                case "cooldown":
                    Boss.GetComponent<Updated_Boss_Stats>().Modify_Cooldown(number);
                    break;
                case "startup":
                    Boss.GetComponent<Updated_Boss_Stats>().Modify_Startup(number);
                    break;
                case "speed":
                    Boss.GetComponent<Updated_Boss_Stats>().Modify_Speed(number);
                    break;
                case "none":
                    break;
                default:
                    Boss.GetComponent<Updated_Boss_Stats>().SearchAttacks(category, attackModifiers, isEnabled);
                    break;
            }
        }
        
    }


    private void GoToNextScene()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Updated_Player_Stats>().Toggle_Dialogue_Status();
        Boss.GetComponent<Updated_Boss_Stats>().Toggle_Dialogue_Status();
        Destroy(gameObject);
    }

    private string ProcessProperties(string text)
    {
        foreach (var exposedProperty in dialogue.ExposedProperties)
        {
            text = text.Replace($"[{exposedProperty.PropertyName}]", exposedProperty.PropertyValue);
        }
        return text;
    }
}

