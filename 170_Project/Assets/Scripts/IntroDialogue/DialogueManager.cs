using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    public Button contButton;
    //private GameObject start;
    public List<GameObject> scenes;
    public Dialogue dialogue;
    private bool typing;
    private Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        StartDialogue(dialogue);
        //start = GameObject.FindGameObjectWithTag("boss");
    }

    public void StartDialogue (Dialogue dialogue) {
        //start.GetComponent<Button>().interactable = false;
        //start.GetComponent<Image>().color = Color.clear;
        //start.GetComponentInChildren<TextMeshProUGUI>().color = Color.clear;
        animator.SetBool("isOpen", true);
        //Debug.Log("Starting Conversation with " + dialogue.name);
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence () {
        contButton.interactable = false;
        if(sentences.Count == 0) {
            EndDialogue();
            return;
        }
        else if (sentences.Count <= 1)
        {
            foreach (var scene in scenes)
            {
                scene.GetComponent<Image>().color = Color.clear;
            }
            scenes[4].GetComponent<Image>().color = Color.white;
        }
        else if(sentences.Count > 1 && sentences.Count <= 2)
        {
            foreach (var scene in scenes)
            {
                scene.GetComponent<Image>().color = Color.clear;
            }
            scenes[3].GetComponent<Image>().color = Color.white;
        }
        else if (sentences.Count > 2 && sentences.Count <= 6)
        {
            foreach (var scene in scenes)
            {
                scene.GetComponent<Image>().color = Color.clear;
            }
            scenes[2].GetComponent<Image>().color = Color.white;
        }
        else if (sentences.Count > 6 && sentences.Count <= 7)
        {
            foreach (var scene in scenes)
            {
                scene.GetComponent<Image>().color = Color.clear;
            }
            scenes[1].GetComponent<Image>().color = Color.white;
        }
        else if (sentences.Count > 7 && sentences.Count <= 10)
        {
            foreach (var scene in scenes)
            {
                scene.GetComponent<Image>().color = Color.clear;
            }
            scenes[0].GetComponent<Image>().color = Color.white;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        //Debug.Log(sentence);
    }

    IEnumerator TypeSentence (string sentence) {
        typing = true;
        int length = sentence.Length;
        int count = 0;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            count++;
            dialogueText.text += letter;
            yield return null;
        }
        contButton.interactable = true;
    }
    void EndDialogue() {
        //Debug.Log("End Of Introduction");
        animator.SetBool("isOpen", false);
        SceneManager.LoadScene(1);

    }
}
