using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

public class WeaponCollector : MonoBehaviour
{
    WeaponIk[] equip_Weapons = new WeaponIk[2];
    int activeWeaponIndex;
    public Animator rigController;

    public enum WeaponSlot
    {
        Primary = 0,
        Secondary = 1
    }
    public Rig handIkRig;
    public Transform[] weaponSlots;
    public Transform weaponLeftGrip;
    public Transform weaponRightGrip;

    private int weaponSlotIndex; //store collect weapon index number to parent object

    private void Awake()
    {
        WeaponIk ExsistingWeaponik = GetComponentInChildren<WeaponIk>();
        if (ExsistingWeaponik)
            EquipWeapon(ExsistingWeaponik);

        rigController.updateMode = AnimatorUpdateMode.Fixed;
        rigController.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
        rigController.cullingMode = AnimatorCullingMode.AlwaysAnimate;
        rigController.updateMode = AnimatorUpdateMode.Normal;

    }

    WeaponIk GetWeaponIndex(int index)
    {
        if(index < 0 || index >= equip_Weapons.Length) return null;
        return equip_Weapons[index];
    }

    private void Update()
    {
        var weaponik = GetWeaponIndex(activeWeaponIndex);
        if (weaponik)
        {

        }
        else
        {

        }

        //if (Input.GetMouseButton(1))
        //{
        //    rigController.SetBool("holster_gun", true);
        //}
        //else
        //{
        //    rigController.SetBool("holster_gun", false);
        //}

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
                SetActivateWeapon(WeaponSlot.Primary);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
                SetActivateWeapon(WeaponSlot.Secondary);
        }
    }

    public void EquipWeapon(WeaponIk newWeapon)
    {
        weaponSlotIndex = (int)newWeapon.weaponSlot;
        var weaponik = GetWeaponIndex(weaponSlotIndex);
        if (weaponik)
        {
            Destroy(weaponik.gameObject);
        }
        weaponik = newWeapon;
        weaponik.CanShoot(false);
        weaponik.rigController = rigController;
        weaponik.transform.parent = weaponSlots[weaponSlotIndex];
        weaponik.transform.localPosition = Vector3.zero + new Vector3(0f, 0f, 0.2710001f);
        weaponik.transform.localRotation = Quaternion.identity;

        equip_Weapons[weaponSlotIndex] = weaponik;
        SetActivateWeapon(newWeapon.weaponSlot);
    }

    void SetActivateWeapon(WeaponSlot weaponSlotIndex)
    {
        int holsterIndex = activeWeaponIndex;
        int activateIndex = (int)weaponSlotIndex;

        if(holsterIndex == activateIndex)
        {
            holsterIndex -= 1;
        }

        StartCoroutine(SwitchWeapon(holsterIndex, activateIndex));
    }

    IEnumerator SwitchWeapon(int holsterIndex, int activateIndex)
    {
        yield return StartCoroutine(HolsterWeapon(holsterIndex));
        yield return StartCoroutine(ActivateWeapon(activateIndex));
        activeWeaponIndex = activateIndex;
    }

    IEnumerator HolsterWeapon(int index)
    {
        var weaponIk = GetWeaponIndex(index);
        if (weaponIk)
        {
            weaponIk.CanShoot(false);
            rigController.SetBool("holster_gun", false);
            do
            {
                yield return new WaitForEndOfFrame();
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);

            
        }
    }

    IEnumerator ActivateWeapon(int index)
    {
        var weaponIk = GetWeaponIndex(index);
        if (weaponIk)
        {
            weaponIk.CanShoot(true);
            rigController.SetBool("holster_gun", false);
            rigController.Play("weapon_" + weaponIk.weaponName);
           // rigController.Play("weapon_recoil_" + weaponIk.weaponName, 1, 0.0f);
            do
            {
                yield return new WaitForEndOfFrame();
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);


        }
    }

}//class
