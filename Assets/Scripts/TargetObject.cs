using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetObject : MonoBehaviour
{
    [SerializeField] LightSpot lightSpot;
    [SerializeField] GameObject objectToShowAfterAttack;
    [SerializeField] UnityEvent onAttack;

    void Awake() {
        lightSpot.gameObject.SetActive(false);
        objectToShowAfterAttack.gameObject.SetActive(false);
    }

    public void Attack() {
        if (lightSpot.gameObject.activeInHierarchy) {
            return;
        }
        lightSpot.gameObject.SetActive(true);
        objectToShowAfterAttack.gameObject.SetActive(true);
        onAttack.Invoke();
        LightsHandler.Instance.IncreaseLightsOnCount();
    }
}
