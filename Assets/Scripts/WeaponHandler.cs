using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] GameObject candleWeapon;

    public GameObject currentWeapon;

    public void SetCurrentWeapon(GameObject weapon) {
        if (currentWeapon != null && currentWeapon.activeInHierarchy) {
            currentWeapon.SetActive(false);
        }
        currentWeapon = weapon;
        currentWeapon.SetActive(true);
    }

    public GameObject GetCurrentWeapon() {
        return currentWeapon;
    }

    public void HandleCandleWeapon() {
        SetCurrentWeapon(candleWeapon);
    }
}
