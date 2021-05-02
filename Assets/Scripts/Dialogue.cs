using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{

    [Serializable]
    private class DialogueSentence2
    {
        public string text;
        public GameObject uiObject;
        public Sprite sprite;
    }

    [SerializeField] List<DialogueSentence2> sentences;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onEnd;

    private int currentSentence = -1;

    void Awake() {
        foreach (var box in GetComponentsInChildren<DialogeBox>()) {
            box.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!gameObject.activeInHierarchy) {
            return;
        }

        if (Input.GetButtonUp("Talk")) {
            if (sentences.Count > currentSentence + 1) {
                NextSentence();
            } else {
                EndDialogue();
            }
        }
    }

    void NextSentence() {
        HideCurrentSentence();
        currentSentence++;
        var sentence = sentences[currentSentence];
        sentence.uiObject.SetActive(true);
        var dialogueBox = sentences[currentSentence].uiObject.GetComponent<DialogeBox>();
        dialogueBox.DisplayText(sentence.text, sentence.sprite);
    }

    void HideCurrentSentence() {
        if (currentSentence >= 0) {
            sentences[currentSentence].uiObject.SetActive(false);
        }
    }

    public void StartDialogue() {
        gameObject.SetActive(true);
        NextSentence();
        onStart.Invoke();
    }

    public void EndDialogue() {
        HideCurrentSentence();
        gameObject.SetActive(false);
        onEnd.Invoke();
    }
}
