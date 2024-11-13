using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Cinemachine;
using StarterAssets;
using UnityEditor.Animations;

public class PersonController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    private StarterAssetsInputs starterAssetsInputs;
    private ThirdPersonController thirdPersonController;

    [SerializeField] private float aimCameraSensivity;
    [SerializeField] private float normalCameraSensivity;
    [SerializeField] private Transform aimObject;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Rig aimLayer;
   // [SerializeField] private float aimDuration = 0.4f;


    public Transform rayCastOrigin;
    public float rotationSpeed = 100f; // Adjust the speed of the rotation
    private float rotationX = 0f;

    private Animator anim;
    private AnimatorOverrideController overrideController;

    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();

        rotationX = transform.localEulerAngles.y;
    }

    private void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    weaponRifle.SetActive(false);
        //    aimLayer.weight = 0f;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    aimLayer.weight = 1.0f;
        //    weaponRifle.SetActive(true);
        //    Invoke(nameof(SetAnimationDelay), 0.0001f);
        //}

        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 scrennCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(scrennCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 999f, aimColliderLayerMask))
        {
            aimObject.position = hitInfo.point;
            mouseWorldPosition = hitInfo.point;

            Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 10f);
        }


        if (starterAssetsInputs.aim)
        {
            //aim riglayer
            //aimLayer.weight += Time.deltaTime / aimDuration;

            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.CameraSensivity(aimCameraSensivity);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

            thirdPersonController.PlayerRotation(false);



            /*   float mouseX = Input.GetAxis("Mouse X");

               // Accumulate the rotation based on mouse movement and speed
               rotationX += mouseX * rotationSpeed * Time.deltaTime;

               // Apply the rotation to the GameObject's Y axis (horizontal rotation)
               transform.localRotation = Quaternion.Euler(0f, rotationX, 0f);
            */

            // transform.forward = Vector3.Lerp(transform.forward,aimPos,Time.deltaTime * 20f); 
        }
        else
        {
            //aim riglayer
            // aimLayer.weight -= Time.deltaTime / aimDuration;

            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.CameraSensivity(normalCameraSensivity);
            thirdPersonController.PlayerRotation(true);
        }
    }

}//class
