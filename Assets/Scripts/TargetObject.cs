using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    [SerializeField] LightSpot lightSpot;
    [SerializeField] GameObject objectToShowAfterAttack;

    void Awake() {
        lightSpot.gameObject.SetActive(false);
        objectToShowAfterAttack.gameObject.SetActive(false);
    }

    public void Attack() {
        lightSpot.gameObject.SetActive(true);
        objectToShowAfterAttack.gameObject.SetActive(true);
    }
}
