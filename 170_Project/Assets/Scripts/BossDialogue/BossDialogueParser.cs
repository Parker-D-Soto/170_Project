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
    public TextMeshProUGUI showingEffects;
    public RectTransform showPanel;
    //[SerializeField] private TextMeshProUGUI effectTest;
    //[SerializeField] private TextMeshProUGUI bossStats;
    //[SerializeField] private TextMeshProUGUI bossAttacks;
    public GameObject Boss;
    public BossHealthBar BossHealthBar;


    //public GameObject UI;

    private ProgressSaverMaster saver;
    private bool changesMade;
    private bool peacefulChange;
    private float effects_start;
    private Vector3 effect_trans;
    private float changedPosition;
    Regex rxName = new Regex(@"^\b((?<name>\w+))\b", RegexOptions.IgnoreCase);
    Regex rxQuantity = new Regex(@"(?<quantity>(-|\+)\d*)\s*$", RegexOptions.IgnoreCase);
    Regex rxEnabled = new Regex(@"(?<enabled>(dis|en)(able))\s*$", RegexOptions.IgnoreCase);
    

    // Start is called before the first frame update
    void Start()
    {
        changedPosition = 0;
        effects_start = showingEffects.rectTransform.position.y;
        effect_trans = showingEffects.rectTransform.position;
        saver = GameObject.FindGameObjectWithTag("Saver").GetComponent<ProgressSaverMaster>();
        if (saver.startWithDialogue)
        {
            SoundManagerScript.PlaySound("Dialogue");
            var narrativeData = dialogue.NodeLinks.First();
            ProceedToNarrative(narrativeData.TargetNodeGuid);
        }
        else
        {
            SoundManagerScript.PlaySound("Dialogue");
            Boss.GetComponent<Updated_Boss_Stats>().attacks = saver.goblinAttacks; 
            Boss.GetComponent<Updated_Boss_Stats>().health = saver.goblinStartHealth;
            Boss.GetComponent<Updated_Boss_Stats>().cooldown = saver.goblinCooldown;
            Boss.GetComponent<Updated_Boss_Stats>().startup = saver.goblinStartup;
            Boss.GetComponent<Updated_Boss_Stats>().speed = saver.goblinSpeed;
            GoToNextScene();
        }

        
    }
    void Update()
    {
        if (changesMade)
        {
            if (peacefulChange)
            {
                if(showingEffects.color != Color.green)
                {
                    showingEffects.color = Color.green;
                }
                effect_trans.y += 30 * Time.deltaTime;
            }
            else
            {
                if (showingEffects.color != Color.red)
                {
                    showingEffects.color = Color.red;
                }
                effect_trans.y -= 30 * Time.deltaTime;
            }
            showingEffects.rectTransform.position = effect_trans;
        }
        else
        {
            showingEffects.color = Color.clear;
            effect_trans.y = effects_start;
        }

        if (Mathf.Abs(effect_trans.y-effects_start) > 40)
        {
            showingEffects.color = Color.clear;
            effect_trans.y = effects_start;
            changesMade = false;
        }
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
            button.GetComponentInChildren<TextMeshProUGUI>().text = ProcessProperties(choice.PortName);
            button.onClick.AddListener(() => ProceedToNarrative(choice.TargetNodeGuid));


        }
    }

    private void MutateBoss(string mutation)
    {
        string category;
        string numberChange;
        int number = 0;
        string attackEnabled;
        bool isEnabled = false;
        //Debug.Log("In MutateBoss");
        Match name = rxName.Match(mutation);
        Match change = rxQuantity.Match(mutation);
        Match enabled = rxEnabled.Match(mutation);
        /*For Debug Start
        Debug.Log("name: " + name.Value);
        Debug.Log("changes: " + change.Value);
        Debug.Log("enabled: " + enabled.Value);
        //For Debug End*/

        if (name.Success)
        {
            category = name.ToString();
            if (change.Success)
            {
                numberChange = change.ToString();
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
            Debug.Log(mutation);
            Debug.Log(category);
            switch (category)
            {
                case "health":
                    Boss.GetComponent<Updated_Boss_Stats>().Modify_Health(number);
                    if(number > 0)
                    {
                        showingEffects.color = Color.red;
                        showingEffects.text = "Health Increased";
                        peacefulChange = false;
                    }
                    else
                    {
                        showingEffects.color = Color.green;
                        showingEffects.text = "Health Decreased";
                        peacefulChange = true;
                    }
                    changesMade = true;
                    break;
                case "cooldown":
                    Boss.GetComponent<Updated_Boss_Stats>().Modify_Cooldown(number);
                    if (number < 0)
                    {
                        showingEffects.color = Color.red;
                        showingEffects.text = "Cooldown Shortened";
                        peacefulChange = false;
                    }
                    else
                    {
                        showingEffects.color = Color.green;
                        showingEffects.text = "Cooldown Lengthened";
                        peacefulChange = true;
                    }
                    changesMade = true;
                    break;
                case "startup":
                    Boss.GetComponent<Updated_Boss_Stats>().Modify_Startup(number);
                    if (number < 0)
                    {
                        showingEffects.color = Color.red;
                        showingEffects.text = "Startup Shortened";
                        peacefulChange = false;
                    }
                    else
                    {
                        showingEffects.color = Color.green;
                        showingEffects.text = "Startup Lengthened";
                        peacefulChange = true;
                    }
                    changesMade = true;
                    break;
                case "speed":
                    Boss.GetComponent<Updated_Boss_Stats>().Modify_Speed(number);
                    if (number > 0)
                    {
                        showingEffects.color = Color.red;
                        showingEffects.text = "Speed Increased";
                        peacefulChange = false;
                    }
                    else
                    {
                        showingEffects.color = Color.green;
                        showingEffects.text = "Speed Decreased";
                        peacefulChange = true;
                    }
                    changesMade = true;
                    break;
                case "none":
                    changesMade = false;
                    break;
                default:
                    Boss.GetComponent<Updated_Boss_Stats>().SearchAttacks(category, isEnabled);
                    Debug.Log(isEnabled);
                    if (isEnabled)
                    {
                        peacefulChange = false;
                        showingEffects.color = Color.red;
                        switch (category)
                        {
                            case "steadyAimFire":
                                changesMade = true;
                                showingEffects.text = "Steady, Aim, FIRE!";
                                showingEffects.color = Color.red;
                                Debug.Log(showingEffects.text);
                                Debug.Log(showingEffects.color);
                                break;
                            case "getEmBoys":
                                changesMade = true;
                                showingEffects.text = "Get 'em Boys!";
                                showingEffects.color = Color.red;
                                break;
                            case "surroundEm":
                                changesMade = true;
                                showingEffects.text = "Surround 'Em!";
                                showingEffects.color = Color.red;
                                break;
                            case "goblinCharge":
                                changesMade = true;
                                showingEffects.text = "CHARGE!";
                                showingEffects.color = Color.red;
                                break;
                            case "pickaxeThrow":
                                changesMade = true;
                                showingEffects.text = "Have a Pickaxe!";
                                showingEffects.color = Color.red;
                                break;
                            default:
                                changesMade = false;
                                showingEffects.text = category;
                                showingEffects.color = Color.black;
                                break;
                        }

                        peacefulChange = false;
                    }
                    else
                    {
                        peacefulChange = true;
                        showingEffects.color = Color.green;
                        switch (category)
                        {
                            case "steadyAimFire":
                                changesMade = true;
                                showingEffects.text = "Steady, Aim, FIRE!";
                                break;
                            case "getEmBoys":
                                changesMade = true;
                                showingEffects.text = "Get 'em Boys!";
                                break;
                            case "surroundEm":
                                changesMade = true;
                                showingEffects.text = "Surround 'Em!";
                                break;
                            case "goblinCharge":
                                changesMade = true;
                                showingEffects.text = "CHARGE!";
                                break;
                            case "pickaxeThrow":
                                changesMade = true;
                                showingEffects.text = "Have a Pickaxe!";
                                break;
                            default:
                                changesMade = false;
                                showingEffects.text = category;
                                showingEffects.color = Color.black;
                                break;
                        }
                        peacefulChange = true;
                    }
                    changesMade = true;
                    break;
            }
        }
    }


    private void GoToNextScene()
    {
        //Debug.Log("GOBLIN BOSS HEALTH IS " + Boss.GetComponent<Updated_Boss_Stats>().health);
        if (saver.startWithDialogue)
        {
            saver.SaveStats();
        }
        //saver.SaveStats();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Updated_Player_Stats>().Toggle_Dialogue_Status();
        Boss.GetComponent<Updated_Boss_Stats>().Toggle_Dialogue_Status();
        Boss.GetComponent<Updated_Boss_Stats>().SetUpWaves();
        BossHealthBar.UpdateMaxHealth(Boss.GetComponent<Updated_Boss_Stats>().health);
        SoundManagerScript.StopSound("Dialogue");
        SoundManagerScript.PlaySound("Fight");
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

