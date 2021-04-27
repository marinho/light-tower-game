using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSpeaker : MonoBehaviour
{
    [SerializeField] bool isPlaying;
    static float soundVolume = .5f;
    private AudioSource audioSource;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        UpdatePlayingState();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdatePlayingState() {
        audioSource.volume = isPlaying ? soundVolume : 0f;
        animator.SetBool("isPlaying", isPlaying);
    }
}
