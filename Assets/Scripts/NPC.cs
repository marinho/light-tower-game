using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [SerializeField] List<string> randomSayings;
    [SerializeField] Text textDisplayTemplate;
    [SerializeField] [Range(0f, 10f)] float timeToShowRandomSayingInSecons = 2;
    [SerializeField] [Range(0f, 10f)] float timeGapBetweenSayingsInSecons = 2;
    [SerializeField] [Range(0f, 1f)] float randomSayingThreshold = .1f;
    [SerializeField] Vector3 offset;
    [SerializeField] Camera usedCamera;
    [SerializeField] public SoundSpeaker soundSpeaker;


    float randomSayingTimeCounter;
    float gapSayingTimeCounter;
    Text textDisplay;

    void Update() {
        if (textDisplay != null) {
            randomSayingTimeCounter -= Time.deltaTime;

            if (randomSayingTimeCounter <= 0) {
                Destroy(textDisplay);
                textDisplay = null;
                gapSayingTimeCounter = timeGapBetweenSayingsInSecons;
            } else {
                UpdateTextDisplayPosition();
            }
        } else  if (gapSayingTimeCounter > 0) {
            gapSayingTimeCounter -= Time.deltaTime;
        } else {
            ShowRandomSaying();
        }
    }

    void ShowRandomSaying() {
        if (randomSayings.Count == 0 || timeToShowRandomSayingInSecons == 0) {
            return;
        }

        float randRate = Random.Range(0f, 1f);
        if (randRate > randomSayingThreshold) {
            return;
        }

        // TODO: improve to return from here if gameObject is out of screen

        randomSayingTimeCounter = timeToShowRandomSayingInSecons;

        textDisplay = CloneTextDisplayTemplate();
        textDisplay.text = randomSayings[Random.Range(0, randomSayings.Count - 1)];
        textDisplay.gameObject.SetActive(true);
        UpdateTextDisplayPosition();
    }

    void UpdateTextDisplayPosition() {
        var npcTransform = gameObject.transform;
        textDisplay.transform.position = usedCamera.WorldToScreenPoint(npcTransform.position + offset);
    }

    Text CloneTextDisplayTemplate() {
        var newText = Instantiate(textDisplayTemplate);
        newText.transform.SetParent(textDisplayTemplate.canvas.transform);

        var newTextRectTransform = newText.GetComponent<RectTransform>();
        var templateRectTransform = textDisplayTemplate.GetComponent<RectTransform>();
        newTextRectTransform.sizeDelta = templateRectTransform.sizeDelta;
        newTextRectTransform.localScale = templateRectTransform.localScale;
        newTextRectTransform.position = templateRectTransform.position;

        return newText;
    }
}
