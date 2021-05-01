using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogeBox : MonoBehaviour
{
    [SerializeField] Text textDisplay;
    [SerializeField] Image imageDisplay;

    public void DisplayText(string text, Sprite sprite) {
        textDisplay.text = text;
        if (sprite != null) {
            imageDisplay.sprite = sprite;
        }
    }
}
