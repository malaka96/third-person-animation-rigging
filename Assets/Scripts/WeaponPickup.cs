using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public WeaponIk weaponPrefab;

    private void OnTriggerEnter(Collider other)
    {
        WeaponCollector weaponCollector = other.GetComponent<WeaponCollector>();
        if (weaponCollector)
        {
            WeaponIk newWeapon = Instantiate(weaponPrefab);
            weaponCollector.EquipWeapon(newWeapon);
        }
    }
}
