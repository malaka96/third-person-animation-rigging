using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private bool isShooting, readyToShooting;
    [SerializeField] private float shootingDelay = 2f;
   // private bool allowReset = true;
    public enum ShootingMode
    {
        single,
        auto
    }
    public ShootingMode shootingMode;

    [Header("Reloading")]
    [SerializeField] private float reloadingTime;
    [SerializeField] private int magazineSize, bulletLeft;
    private bool isReloading = false;

    [Header("ProjectTile")]
    [SerializeField] private Transform aimTransform;
    [SerializeField] private GameObject projectTile;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();


    void Start()
    {
        readyToShooting = true;
        bulletLeft = magazineSize;
    }


    void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screeenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screeenCenterPoint);

        if (Physics.Raycast(ray, out RaycastHit hit, 999f, aimColliderLayerMask))
        {
            aimTransform.position = hit.point;
            mouseWorldPosition = hit.point;
        }
        
        if(shootingMode == ShootingMode.single)
        {
            isShooting = Input.GetMouseButtonDown(0);
        }

        if(shootingMode == ShootingMode.auto)
        {
            isShooting = Input.GetMouseButton(0);
        }

        //calling shoot method
        if(isShooting && readyToShooting && bulletLeft > 0)
        {
            Shoot(mouseWorldPosition);
        }
        
        //reloading weapon
        if(Input.GetKeyDown(KeyCode.R) && !isShooting && !isReloading && bulletLeft < magazineSize)
        {
            RealoadWeapon();
        }
        if(isShooting && bulletLeft == 0 && readyToShooting)
        {
            RealoadWeapon();
        }
    }

    void Shoot(Vector3 mouseWorldPosition)
    {
        readyToShooting = false;
        bulletLeft--;

        Vector3 aimDir = (mouseWorldPosition - bulletSpawnPoint.position).normalized;
        Instantiate(projectTile, bulletSpawnPoint.position, Quaternion.LookRotation(aimDir, Vector3.up));

        Invoke("ResetShoot", shootingDelay);
    }

    void RealoadWeapon()
    {
        isReloading = true;
        Invoke("ResetRealoading", reloadingTime);
    }

    void ResetRealoading()
    {
        isReloading = false;
        bulletLeft = magazineSize;
    }

    void ResetShoot()
    {
        readyToShooting = true;
    }
}
