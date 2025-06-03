using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class Health : MonoBehaviour
    {
        [SerializeField] private int health = 3;
        [SerializeField] private int maxHealth = 3;
        [CanBeNull] public event Action OnDeath;
        [CanBeNull] public event Action OnTakeDamage;

        [NonSerialized] public bool useDefaultDamage = true;

        private void Start()
        {
        }
        
        public virtual void TakeDamage(int damage)
        {
            OnTakeDamage?.Invoke();
            
            if (useDefaultDamage)
                DefaultTakeDamage(damage);
        }

        public virtual void Heal(int healingAmount)
        {
            health = health + healingAmount >= maxHealth ? maxHealth : health + healingAmount;
        }
        
        protected virtual void Death()
        {
            if (OnDeath == null)
                DefaultDeath();
            else
                OnDeath.Invoke();
        }

        public virtual void DefaultDeath()
        {
            Destroy(gameObject);
        }
        
        public virtual void DefaultTakeDamage(int damage)
        {
            if (health - damage <= 0)
            {
                health = 0;
                Death();
            }
            else
                health -= damage; 
        }
    }