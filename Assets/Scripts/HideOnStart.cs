using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnStart : MonoBehaviour
{
    [SerializeField] List<GameObject> elementsToHide;

    void Start()
    {
        foreach (var elementToHide in elementsToHide) {
            elementToHide.SetActive(false);
        }
    }

}
