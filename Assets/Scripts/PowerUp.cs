using System;
using UnityEngine;
using Classes;

    public class PowerUp : MonoBehaviour, ICollectable
    {
        [SerializeField] private Power storedPower;

        private void Start()
        {
            if (storedPower == null) storedPower = Power.Empty;
        }

        public void OnCollect(Transform collector)
        {
            if (storedPower == Power.Empty) return;
            if (collector.TryGetComponent(out PowerUpSystem powerUpSystem))
            {
                powerUpSystem.AddPowerUp(storedPower);
            }
        }
    }