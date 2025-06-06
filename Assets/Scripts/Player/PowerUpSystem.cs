using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class PowerUpSystem : MonoBehaviour
    {
        private Stack<string> powerNames = new();
        [SerializeField] private ParticleSystem damageParticle;
        
        private void OnEnable()
        {
            if (TryGetComponent(out Health health))
            {
                health.OnTakeDamage += RemoveMostRecentPowerUp;
                health.useDefaultDamage = false;
            }
        }

        private void OnDisable()
        {
            if (TryGetComponent(out Health health))
            {
                health.OnTakeDamage -= RemoveMostRecentPowerUp;
                health.useDefaultDamage = true;
            }
        }
        
        public void AddPowerUp(MonoScript power)
        {
            if ( power == null) return;
            if (powerNames.Contains(power.name)) return;
        
            powerNames.Push(power.name);
            gameObject.AddComponent(power.GetClass());
        }

        private void RemoveMostRecentPowerUp()
        {
            if (damageParticle)
                Instantiate(damageParticle, transform.position, Quaternion.identity);
            if (powerNames.Count == 0)
            {
                Debug.Log("Died");
                return;
            }
            
            Component component = gameObject.GetComponent(powerNames.Pop());
            if (component != null)
                Destroy(component);
        }
    }
}
