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
                health.OnTakeDamage += RemoveMostRecentPowerUp;
            }
        }

        private void OnDisable()
        {
            if (TryGetComponent(out Health health))
            {
                health.OnTakeDamage -= RemoveMostRecentPowerUp;
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
            var component = gameObject.GetComponent(powerNames.Pop());
            if (component == null) return;
            Destroy(component);
        }
    }
}
