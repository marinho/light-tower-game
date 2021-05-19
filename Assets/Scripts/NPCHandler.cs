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

    public void UnsetDialogueForPapaCapimPlaying() {
        papaCapimPlaying.GetComponent<UseObject>().SetScreenToShow(null);
    }

    public void EnableSeedeaterBirdAndPianoSoundSpeaker() {
        seedeaterBird.SetActive(true);
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

    // Grandma

    [SerializeField] GameObject grandmaIdle;
    [SerializeField] GameObject grandmaWalking;
    [SerializeField] GameObject secondDialogueForGrandma;
    [SerializeField] GameObject thirdDialogueForGrandma;
    [SerializeField] Transform grandmaWalkingDestination;

    public void UnsetDialogueForGrandmaIdle() {
        grandmaIdle.GetComponent<UseObject>().SetScreenToShow(null);
    }

    public void UnsetDialogueForGrandmaWalking() {
        grandmaWalking.GetComponent<UseObject>().SetScreenToShow(null);
    }

    public void VerifyIfGrandmasItemsAreTaken(ItemContainer itemContainer) {
        var itemsCount = itemContainer.CountItemsOfTag("Grandma Item");
        if (itemsCount < 2) {
            return;
        }

        grandmaIdle.GetComponent<UseObject>().SetScreenToShow(secondDialogueForGrandma);
    }

    public void ChangeGrandmaToWalking() {
        grandmaIdle.SetActive(false);
        grandmaWalking.SetActive(true);

        var animator = grandmaWalking.GetComponent<Animator>();

        var walkToPosition = new Vector3(
            grandmaWalkingDestination.position.x,
            grandmaWalking.transform.position.y,
            grandmaWalking.transform.position.z
        );
        grandmaWalking.GetComponent<Walker>().WalkTo(walkToPosition);
    }

    public void UpdateAfterGrandmaArrivesDestination() {
        var npc = grandmaWalking.GetComponent<NPC>();

        var newSayings = new List<string>();
        newSayings.Add("Oh... finally arrived. I will reward her for this help.");
        newSayings.Add("Little, talk to me, before you leave?");

        npc.randomSayings = newSayings;
        npc.DestroyRandomSayingTextDisplay();
        npc.ShowRandomSaying();

        grandmaWalking.GetComponent<UseObject>().SetScreenToShow(thirdDialogueForGrandma);
    }

}
