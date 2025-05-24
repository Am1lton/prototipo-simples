using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField]private float projectileLifeTime = 3f;
    [SerializeField]private int projectileDamage = 1;
    [SerializeField] private int projectilePiercing = 1;
    public Transform parent;

    private Rigidbody rb;
    
    public float projectileSpeed = 18f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.right * projectileSpeed;
        Destroy(gameObject, projectileLifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == parent) return;
        
        if (other.gameObject.TryGetComponent(out Health health))
        {
            health.TakeDamage(projectileDamage);
            projectilePiercing--;
            if (projectilePiercing <= 0) Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
