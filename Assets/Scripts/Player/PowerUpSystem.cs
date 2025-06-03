using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class PowerUpSystem : MonoBehaviour
    {
        private Stack<string> powerNames = new();

        private void OnEnable()
        {
            if (TryGetComponent(out Health health))
            {
                Debug.Log("Subscribed to health");
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
