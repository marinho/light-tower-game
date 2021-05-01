using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHandler : MonoBehaviour
{
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
        var soundSpeaker = bassistPlaying.GetComponentInChildren<SoundSpeaker>();
        soundSpeaker.Use();
    }
}
