using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class NPCHandler : MonoBehaviour
{

    public void IncreaseSongPoints() {
        SongScoreDisplay.Instance.IncreaseSongPoints();
    }

    // Bassist

    [SerializeField] GameObject bassistIdle;
    [SerializeField] GameObject bassistPlaying;
    [SerializeField] Dialogue secondDialogueForBassistIdle;

    public void SetSecondDialogueForBassistIdle() {
        bassistIdle.GetComponent<UseObject>().SetScreenToShow(secondDialogueForBassistIdle.gameObject);
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
    [SerializeField] Dialogue secondDialogueForPapaCapimIdle;

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
        npc.ShowRandomSayingText();
    }

    public void SetSecondDialogueForPapaCapimIdle() {
        papaCapimIdle.GetComponent<UseObject>().SetScreenToShow(secondDialogueForPapaCapimIdle.gameObject);
    }

    public void ChangePapaCapimToPlaying() {
        papaCapimIdle.SetActive(false);
        papaCapimPlaying.SetActive(true);
    }

    // Grandma

    [SerializeField] GameObject grandmaIdle;
    [SerializeField] GameObject grandmaWalking;
    [SerializeField] Dialogue secondDialogueForGrandma;
    [SerializeField] Dialogue thirdDialogueForGrandma;
    [SerializeField] Transform grandmaWalkingDestination;

    public void VerifyIfGrandmasItemsAreTaken() {
        var itemContainer = ItemContainer.Instance;
        var itemsCount = itemContainer.CountItemsOfTag("Grandma Item");
        if (itemsCount < 2) {
            return;
        }

        grandmaIdle.GetComponent<UseObject>().SetScreenToShow(secondDialogueForGrandma.gameObject);
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
        npc.ShowRandomSayingText();

        grandmaWalking.GetComponent<UseObject>().SetScreenToShow(thirdDialogueForGrandma.gameObject);
    }

    // Priest

    [SerializeField] GameObject priestSad;
    [SerializeField] GameObject priestHappy;
    [SerializeField] Dialogue secondDialogueForPriest;
    
    public void VerifyIfPriestsItemsAreTaken() {
        var itemContainer = ItemContainer.Instance;
        var itemsCount = itemContainer.CountItemsOfTag("Priest Item");
        if (itemsCount < 1) {
            return;
        }

        priestSad.GetComponent<UseObject>().SetScreenToShow(secondDialogueForPriest.gameObject);
    }

    public void ChangePriestToHappy() {
        priestSad.SetActive(false);
        priestHappy.SetActive(true);
    }

    // Twins

    [SerializeField] GameObject twinEmployedSad;
    [SerializeField] GameObject twinEmployedHappy;
    [SerializeField] Dialogue secondDialogueForTwinEmployed;
    [SerializeField] GameObject twinJoblessSad;
    [SerializeField] GameObject twinJoblessHappy;
    [SerializeField] Dialogue secondDialogueForTwinJobless;

    public void TwinEmployedFirstToBeContacted() {
        twinJoblessSad.GetComponent<UseObject>().SetScreenToShow(secondDialogueForTwinJobless.gameObject);
    }

    public void TwinJoblessFirstToBeContacted() {
        twinEmployedSad.GetComponent<UseObject>().SetScreenToShow(secondDialogueForTwinEmployed.gameObject);
    }

    public void ChangeTwinJoblessToWalking() {
        // grandmaIdle.SetActive(false);
        // grandmaWalking.SetActive(true);
        var twinWalking = twinJoblessSad;
        var destination = twinEmployedSad.transform;

        var animator = twinWalking.GetComponent<Animator>();

        var walkToPosition = new Vector3(
            destination.position.x,
            twinWalking.transform.position.y,
            twinWalking.transform.position.z
        );
        twinWalking.GetComponent<Walker>().WalkTo(walkToPosition);
    }

    public void UpdateAfterTwinArrivesDestination() {
        IncreaseSongPoints();
    }

    // Tower Entrance

    [SerializeField] Transform towerEntrancePosition;
    [SerializeField] Transform towerTopPosition;
    [SerializeField] GameObject towerEntranceSprites;
    [SerializeField] Dialogue dialogueForTowerEntranceOpening;
    [SerializeField] GameObject darkMap;
    bool towerEntranceIsOpening = false;

    public void BeforeOpenTowerEntrance() {
        towerEntranceIsOpening = true;
        darkMap.SetActive(false);
        dialogueForTowerEntranceOpening.StartDialogue();
    }

    public void UpdateOnTowerEntranceDialogueNextSentence() {
        int currentSentence = dialogueForTowerEntranceOpening.GetCurrentSentenceIndex();
        if (currentSentence == 1) {
            dialogueForTowerEntranceOpening.DisableChangeToNextSentence();
            CameraMovement.Instance.MoveTo(towerEntrancePosition.position);
        }
        else if (currentSentence == 2) {
            towerEntranceSprites.SetActive(true);
        }
        else if (currentSentence == 3) {
            dialogueForTowerEntranceOpening.DisableChangeToNextSentence();
            CameraMovement.Instance.MoveTo(towerTopPosition.position);
        }
        else if (currentSentence == 4) {
            dialogueForTowerEntranceOpening.DisableChangeToNextSentence();
            CameraMovement.Instance.MoveTo(Player.Instance.transform.position);
            towerEntranceIsOpening = false;
        }

    }

    public void CameraReachedTarget() {
        if (!towerEntranceIsOpening) {
            return;
        }
        dialogueForTowerEntranceOpening.EnableChangeToNextSentence();
    }

    public void BackToPlayerAfterTowerOpening() {
        darkMap.SetActive(true);
    }

}
