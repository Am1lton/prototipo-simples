using System;
using JetBrains.Annotations;
using UnityEngine;

public class Health : MonoBehaviour
    {
        [SerializeField] private int health = 3;
        [SerializeField] private int maxHealth = 3;
        [CanBeNull] public event Action OnDeath;
        [CanBeNull] public event Action OnTakeDamage;

        public virtual void TakeDamage(int damage)
        {
            OnTakeDamage?.Invoke();
            
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
            if (OnDeath != null)
                OnDeath.Invoke();
            else
                DefaultDeath();
        }

        public virtual void DefaultDeath()
        {
            Destroy(gameObject);
        }
    }