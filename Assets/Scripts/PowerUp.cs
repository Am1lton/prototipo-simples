using UnityEngine;

    public class PowerUp : Coin.Coin
    {
        [SerializeField] private Component storedPowerComponent;

        protected override void Start()
        {
            base.Start();
            if (storedPowerComponent == null) Destroy(gameObject);
        }

        public override void OnCollect(Transform collector)
        {
            if (collector.TryGetComponent(out PowerUpSystem powerUpSystem))
            {
                powerUpSystem.AddPowerUp(storedPowerComponent);
            }
        }
    }