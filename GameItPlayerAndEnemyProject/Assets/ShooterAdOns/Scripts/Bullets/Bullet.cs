using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class will contain the common properties and methods for the bullet
/// </summary>
public abstract class Bullet : MonoBehaviour
{
    public BulletData bulletData;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, bulletData.lifeTime);
        
    }

    private void Update()
    {
        rb.velocity = transform.forward * bulletData.speed;
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        // Call the OnHit method when a collision occurs
        OnHit();
    }

    // Method to be overridden by subclasses
    public abstract void OnHit();
}
