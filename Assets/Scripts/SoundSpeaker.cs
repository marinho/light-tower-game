using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundSpeaker : MonoBehaviour
{
    [SerializeField] bool isPlaying;
    [SerializeField] UnityEvent onPlay;
    [SerializeField] UnityEvent onStop;
    [SerializeField] SongScoreDisplay songScoreDisplay;

    static float highSoundVolume = .5f;
    static float lowSoundVolume = .0f;

    Renderer objectRenderer;

    void Awake() {
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.enabled = false;
    }

    void Update() {
        UpdatePlayingState();
    }

    public void SetVisible() {
        objectRenderer.enabled = true;
    }

    public void Use()
    {
        if (isPlaying) {
            isPlaying = false;
            onStop.Invoke();
        } else if (songScoreDisplay.CanPlayOneMoreSong()) {
            isPlaying = true;
            onPlay.Invoke();
        }
    }

    void UpdatePlayingState() {
        var audioSource = GetComponent<AudioSource>();
        var animator = GetComponent<Animator>();
        audioSource.volume = isPlaying ? highSoundVolume : lowSoundVolume;
        animator.SetBool("isPlaying", isPlaying);
    }

}
