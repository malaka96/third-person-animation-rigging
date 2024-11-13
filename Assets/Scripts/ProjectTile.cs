using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float bulletSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Destroy(this.gameObject, 2f);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = transform.forward * bulletSpeed * Time.fixedDeltaTime;
    }

 
}
