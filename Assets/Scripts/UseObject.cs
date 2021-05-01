using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlPadInputButton {
    Use,   // Circle
    Talk  // Square
    // Jump,  // Triangle
    // Attack // X
}

public class UseObject : MonoBehaviour
{
    [SerializeField] GameObject screenToShow;
    [SerializeField] bool isActive = true;
    [SerializeField] UIControlPad uiControlPad;
    [SerializeField] ControlPadInputButton inputButton;

    private bool playerInRange = false;
    private Material initialMaterial;
    private string inputButtonKey;

    void Start() {
        if (inputButton == ControlPadInputButton.Talk) {
            inputButtonKey = "Talk";
        } else {
            inputButtonKey = "Use";
        }
    }

    private void Update()
    {
        if (Input.GetButtonUp(inputButtonKey) && playerInRange)
        {
            if (screenToShow != null) {
                if (!screenToShow.activeInHierarchy) {
                    ShowAttachedScreen();
                    if (screenToShow.GetComponent<Dialogue>() != null) {
                        screenToShow.GetComponent<Dialogue>().StartDialogue();
                    }
                }
            } else if (gameObject.GetComponent<SoundSpeaker>() != null) {
                gameObject.GetComponent<SoundSpeaker>().Use();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isActive)
        {
            playerInRange = true;
            EnableControlPadButton(playerInRange);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isActive)
        {
            playerInRange = false;
            EnableControlPadButton(playerInRange);
        }
    }

    void EnableControlPadButton(bool enabled) {
        if (inputButton == ControlPadInputButton.Talk) {
            uiControlPad.SetSquareEnabled(playerInRange);
        } else {
            uiControlPad.SetCircleEnabled(playerInRange);
        }
    }

    private void ShowAttachedScreen()
    {
        if (playerInRange && isActive)
        {
            screenToShow.SetActive(true);
        }
    }

    public void SetScreenToShow(GameObject screenObject) {
        screenToShow = screenObject;
    }

}
