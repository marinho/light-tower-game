using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongScoreDisplay : MonoBehaviour
{
    [SerializeField] [Range(0, 7)] int songPoints;

    static float onePointWidth = 62.5f;

    void Awake()
    {
        songPoints = 0;
    }

    void Update()
    {
        var t = GetComponent<RectTransform>();
        var newWidth = onePointWidth * songPoints;
        t.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
    }

    public void IncreaseSongPoints() {
        songPoints++;
    }
}
