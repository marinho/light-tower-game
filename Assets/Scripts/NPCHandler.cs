using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCHandler : MonoBehaviour
{
    [SerializeField] Image songScoreDisplay;

    public void IncreaseSongPoints() {
        var songScore = songScoreDisplay.GetComponent<SongScoreDisplay>();
        songScore.IncreaseSongPoints();
    }

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
        // TODO: set drums soundSpeaker visible
        if (soundSpeaker != null) {
            soundSpeaker.Use();
        }
    }

    // Papa Capim

    [SerializeField] GameObject papaCapimIdle;
    [SerializeField] GameObject papaCapimPlaying;
    [SerializeField] GameObject seedeaterBird;
    [SerializeField] GameObject secondDialogueForPapaCapimIdle;

    public void UnsetDialogueForPapaCapimIdle() {
        papaCapimIdle.GetComponent<UseObject>().SetScreenToShow(null);
    }

    public void EnableSeedeaterBirdAndPianoSoundSpeaker() {
        seedeaterBird.SetActive(true);
        // TODO: set piano soundSpeaker visible
    }

    public void BirdWasCaught() {
        var npc = papaCapimIdle.GetComponent<NPC>();

        var newSayings = new List<string>();
        newSayings.Add("Oh, bird of my dreams! You are back!");
        newSayings.Add("Talk to me, girl? He is back! It's sooo much happiness!");

        npc.randomSayings = newSayings;
        npc.DestroyRandomSayingTextDisplay();
        npc.ShowRandomSaying();
    }

    public void SetSecondDialogueForPapaCapimIdle() {
        papaCapimIdle.GetComponent<UseObject>().SetScreenToShow(secondDialogueForPapaCapimIdle);
    }

    public void ChangePapaCapimToPlaying() {
        papaCapimIdle.SetActive(false);
        papaCapimPlaying.SetActive(true);
    }

}
