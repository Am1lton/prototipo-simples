using Player;
using UnityEditor;
using UnityEngine;

    public class PowerUp : Coin.Coin
    {
        [SerializeField] private MonoScript storedPowerScript;

        protected override void Start()
        {
            base.Start();
            if (storedPowerScript == null) Destroy(gameObject);
        }

        public override void OnCollect(Transform collector)
        {
            if (collector.TryGetComponent(out PowerUpSystem powerUpSystem))
            {
                powerUpSystem.AddPowerUp(storedPowerScript);
                Destroy(gameObject);
            }
        }
    }