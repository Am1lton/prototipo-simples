using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Health : MonoBehaviour
    {
        [SerializeField] private int health = 3;
        [SerializeField] private int maxHealth = 3;
        [SerializeField] [CanBeNull] private UnityEvent onDeath;

        public virtual void TakeDamage(int damage)
        {
            if (health - damage <= 0)
            {
                health = 0;
                Death();
            }
            else
                health -= damage;
        }

        public virtual void Heal(int healingAmount)
        {
            health = health + healingAmount >= maxHealth ? maxHealth : health + healingAmount;
        }
        
        protected virtual void Death()
        {
            if (onDeath == null) return;
            
            if (onDeath.GetPersistentEventCount() == 0)
                Destroy(gameObject);
            else
                onDeath.Invoke();
        }
    }