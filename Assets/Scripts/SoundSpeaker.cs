using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSpeaker : MonoBehaviour
{
    [SerializeField] bool isPlaying;

    static float highSoundVolume = .5f;
    static float lowSoundVolume = .0f;

    void Start()
    {
        // UpdatePlayingState();
    }

    void Update() {
        UpdatePlayingState();
    }

    public void Use()
    {
        isPlaying = !isPlaying;
        // UpdatePlayingState();
    }

    void UpdatePlayingState() {
        var audioSource = GetComponent<AudioSource>();
        var animator = GetComponent<Animator>();
        audioSource.volume = isPlaying ? highSoundVolume : lowSoundVolume;
        animator.SetBool("isPlaying", isPlaying);
    }

}
