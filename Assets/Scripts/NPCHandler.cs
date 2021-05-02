using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHandler : MonoBehaviour
{
    // Bassist

    [SerializeField] GameObject bassistIdle;
    [SerializeField] GameObject bassistPlaying;
    [SerializeField] GameObject secondDialogueForBassistIdle;

    public void UnsetDialogueForBassistIdle() {
        bassistIdle.GetComponent<UseObject>().SetScreenToShow(null);
    }

    public void SetSecondDialogueForBassistIdle() {
        bassistIdle.GetComponent<UseObject>().SetScreenToShow(secondDialogueForBassistIdle);
    }

    public void ChangeBassistToPlaying() {
        bassistIdle.SetActive(false);
        bassistPlaying.SetActive(true);
        var soundSpeaker = bassistPlaying.GetComponent<NPC>().soundSpeaker;
        if (soundSpeaker != null) {
            soundSpeaker.Use();
        }
    }

    // Papa Capim

    [SerializeField] GameObject papaCapimIdle;
    [SerializeField] GameObject papaCapimPlaying;
    [SerializeField] GameObject secondDialogueForPapaCapimIdle;

    public void UnsetDialogueForPapaCapimIdle() {
        papaCapimIdle.GetComponent<UseObject>().SetScreenToShow(null);
    }

    public void SetSecondDialogueForPapaCapimIdle() {
        papaCapimIdle.GetComponent<UseObject>().SetScreenToShow(secondDialogueForPapaCapimIdle);
    }

    public void ChangePapaCapimToPlaying() {
        papaCapimIdle.SetActive(false);
        papaCapimPlaying.SetActive(true);
        var soundSpeaker = papaCapimPlaying.GetComponent<NPC>().soundSpeaker;
        if (soundSpeaker != null) {
            soundSpeaker.Use();
        }
    }
}
