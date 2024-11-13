using Cinemachine;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    private CinemachineImpulseSource _impulseSource;
   // [HideInInspector] public Animator rigController;

    void Start()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void ApplyRecoil(string weaponName)
    {
        if (_impulseSource != null)
        {
            _impulseSource.GenerateImpulse();
        }
    }
}
