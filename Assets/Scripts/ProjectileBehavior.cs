using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField]private float projectileLifeTime = 3f;
    [SerializeField]private int projectileDamage = 1;
    private Transform parent;

    private bool canCollide = true;
    private Rigidbody rb;
    
    public float projectileSpeed = 18f;
    
    private void Start()
    {
        parent = transform.parent;
        transform.parent = null;
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.right * projectileSpeed;
        Destroy(gameObject, projectileLifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canCollide)
        {
            Destroy(gameObject);
            return;
        }
        
        if (other.transform == parent) return;
        
        if (other.gameObject.TryGetComponent(out Health health))
        {
            health.TakeDamage(projectileDamage);
            Destroy(gameObject);
            canCollide = false;
            return;
        }
        canCollide = false;
        Destroy(gameObject);
    }
}
