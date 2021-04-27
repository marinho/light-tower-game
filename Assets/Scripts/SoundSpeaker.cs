using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSpeaker : MonoBehaviour
{
    [SerializeField] bool isPlaying;
    [SerializeField] UIControlPad uiControlPad;

    static float soundVolume = .5f;
    private AudioSource audioSource;
    private Animator animator;
    private bool playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        UpdatePlayingState();
    }

    void Update()
    {
        // Fire1 = Square
        // Fire2 = X
        // Fire3 = Circle
        if (Input.GetButtonUp("Use") && playerInRange) {
            isPlaying = !isPlaying;
            UpdatePlayingState();
        }
    }

    void UpdatePlayingState() {
        audioSource.volume = isPlaying ? soundVolume : 0f;
        animator.SetBool("isPlaying", isPlaying);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            uiControlPad.SetCircleEnable(playerInRange);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            uiControlPad.SetCircleEnable(playerInRange);
        }
    }

}
