using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSpeaker : MonoBehaviour
{
    [SerializeField] bool isPlaying;

    static float highSoundVolume = .5f;
    static float lowSoundVolume = .0f;
    private AudioSource audioSource;
    private Animator animator;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        UpdatePlayingState();
    }

    public void Use()
    {
        isPlaying = !isPlaying;
        UpdatePlayingState();
    }

    void UpdatePlayingState() {
        audioSource.volume = isPlaying ? highSoundVolume : lowSoundVolume;
        animator.SetBool("isPlaying", isPlaying);
    }

}
