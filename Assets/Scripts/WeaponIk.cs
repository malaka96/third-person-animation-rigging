using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIk : MonoBehaviour
{
    public string weaponName;
    public WeaponCollector.WeaponSlot weaponSlot;
    [HideInInspector] public Animator rigController;
    private CinemachineImpulseSource _impulseSource;
    private WeaponRecoil weaponRecoil;
    private string activeWeaponName;

    private bool canShoot = false;

    private void Awake()
    {
        //weaponRecoil = GetComponent<WeaponRecoil>();
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        if (canShoot)
        {

            if (Input.GetMouseButton(1))
            {
                rigController.SetBool("holster_gun", true);

                if (Input.GetMouseButtonDown(0))
                {
                    Shoot();
                }
            }
            else
            {
                rigController.SetBool("holster_gun", false);
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                rigController.SetBool("reloadBool", true);
            }
            else
            {
                rigController.SetBool("reloadBool", false);
            }

        }
    }

    void Shoot()
    {
            ApplyRecoil();
    }

    private void ApplyRecoil()
    {
        rigController.Play("weapon_recoil_" + weaponName, 1, 0.0f);
        if (_impulseSource != null)
        {
            _impulseSource.GenerateImpulse();
        }
    }

    public void CanShoot(bool newState)
    {
        canShoot = newState;
    }
}

