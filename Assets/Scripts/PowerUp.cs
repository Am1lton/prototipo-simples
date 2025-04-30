using UnityEngine;

    public class PowerUp : Coin.Coin, ICollectable
    {
        [SerializeField] private MonoBehaviour storedPowerMonoBehaviour;

        protected override void Start()
        {
            base.Start();
            if (storedPowerMonoBehaviour == null) Destroy(gameObject);
        }

        public override void OnCollect(Transform collector)
        {
            if (collector.TryGetComponent(out PowerUpSystem powerUpSystem))
            {
                powerUpSystem.AddPowerUp(storedPowerMonoBehaviour);
            }
        }
    }