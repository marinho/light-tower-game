using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseObject : MonoBehaviour
{
    [SerializeField] GameObject screenToShow;
    [SerializeField] bool isActive = true;
    [SerializeField] UIControlPad uiControlPad;

    private bool playerInRange = false;
    private Material initialMaterial;

    private void Update()
    {
        if (Input.GetButtonUp("Use") && playerInRange)
        {
            if (screenToShow != null) {
                ShowAttachedScreen();
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
            uiControlPad.SetCircleEnabled(playerInRange);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isActive)
        {
            playerInRange = false;
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

    private SpriteRenderer GetSpriteRenderer()
    {
        return gameObject.GetComponent<SpriteRenderer>();
    }
}
