using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundSpeaker : MonoBehaviour
{
    [SerializeField] bool isPlaying;
    [SerializeField] UnityEvent onPlay;
    [SerializeField] UnityEvent onStop;

    static float highSoundVolume = .5f;
    static float lowSoundVolume = .0f;

    Renderer renderer;

    void Awake() {
        renderer = GetComponent<Renderer>();
        renderer.enabled = false;
    }

    void Update() {
        UpdatePlayingState();
    }

    public void SetVisible() {
        renderer.enabled = true;
    }

    public void Use()
    {
        isPlaying = !isPlaying;
        if (isPlaying) {
            onPlay.Invoke();
        } else {
            onStop.Invoke();
        }
    }

    void UpdatePlayingState() {
        var audioSource = GetComponent<AudioSource>();
        var animator = GetComponent<Animator>();
        audioSource.volume = isPlaying ? highSoundVolume : lowSoundVolume;
        animator.SetBool("isPlaying", isPlaying);
    }

}
