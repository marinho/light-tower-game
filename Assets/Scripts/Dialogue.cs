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
    [SerializeField] UnityEvent onNextSentence;

    int currentSentence = -1;
    bool canChangeToNextSentence = true;

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
        if (!canChangeToNextSentence) {
            return;
        }

        HideCurrentSentence();
        currentSentence++;
        var sentence = sentences[currentSentence];
        sentence.uiObject.SetActive(true);
        var dialogueBox = sentences[currentSentence].uiObject.GetComponent<DialogeBox>();
        dialogueBox.DisplayText(sentence.text, sentence.sprite); // FIXME some null object bug in here
        onNextSentence.Invoke();
    }

    void HideCurrentSentence() {
        if (currentSentence >= 0 && sentences.Count > 0) {
            sentences[currentSentence].uiObject.SetActive(false);
        }
    }

    public int GetCurrentSentenceIndex() {
        return currentSentence;
    }

    public void StartDialogue() {
        gameObject.SetActive(true);
        NextSentence();
        Player.Instance.DisablePlayerMovements();
        onStart.Invoke();
    }

    public void EndDialogue() {
        HideCurrentSentence();
        gameObject.SetActive(false);
        Player.Instance.EnablePlayerMovements();
        onEnd.Invoke();
    }

    public void EnableChangeToNextSentence() {
        canChangeToNextSentence = true;
    }

    public void DisableChangeToNextSentence() {
        canChangeToNextSentence = false;
    }
}
