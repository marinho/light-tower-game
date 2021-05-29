using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SongScoreDisplay : Singleton<SongScoreDisplay>
{
    [SerializeField] [Range(0, 7)] int songPoints;
    [SerializeField] [Range(0, 7)] int songsPlaying;
    [SerializeField] Image muteImage;
    [SerializeField] UnityEvent onSongPointsIncrease;
    [SerializeField] UnityEvent onReachedMaximumSongPoints;

    static float onePointWidth = 62.5f;
    int maximumSongPoints = 7;

    // Prevent non-singleton constructor use.
    protected SongScoreDisplay() { }

    void Awake()
    {
        songPoints = 0;
        songsPlaying = 0;
    }

    void Update()
    {
        var newWidth = onePointWidth * songPoints;
        var t = GetComponent<RectTransform>();
        t.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);

        var muteWidth = onePointWidth * (songPoints - songsPlaying);
        var muteTransform = muteImage.GetComponent<RectTransform>();
        muteTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, muteWidth);
    }

    public void IncreaseSongPoints() {
        songPoints++;
        onSongPointsIncrease.Invoke();

        if (songPoints == maximumSongPoints) {
            onReachedMaximumSongPoints.Invoke();
        }
    }

    public void IncreaseSongsPlaying() {
        songsPlaying++;
    }

    public void DecreaseSongsPlaying() {
        songsPlaying--;
    }

    public bool CanPlayOneMoreSong() {
        return songsPlaying < songPoints;
    }
}
