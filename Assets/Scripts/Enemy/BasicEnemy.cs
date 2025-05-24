using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class BasicEnemy : MonoBehaviour
{
    private Health health;
    
    private void Start() => health = GetComponent<Health>();

    private void OnTriggerEnter(Collider other)
    {
        health.TakeDamage(1);
    }
}
