using Player;
using UnityEditor;
using UnityEngine;

    [RequireComponent(typeof(MeshRenderer))]
    public class PowerUp : Coin.Coin
    {
        [SerializeField] private MonoScript storedPowerScript;
        [SerializeField] private float RespawnTime = 5f;
        
        private float timer = 0f;
        private Material mat;
        private bool active = true;

        protected override void Start()
        {
            base.Start();
            mat = GetComponent<MeshRenderer>().material;
            if (storedPowerScript == null) Destroy(gameObject);
        }

        private void Update()
        {
            base.Update();
            
            if (mat.color.a < Color.white.a)
            {
                timer += Time.deltaTime;
            }
            
            if (timer >= RespawnTime)
            {
                timer = 0f;
                mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, Color.white.a);
                active = true;
            }
        }
        
        public override void OnCollect(Transform collector)
        {
            if (!active) return;

            if (!collector.TryGetComponent(out PowerUpSystem powerUpSystem)) return;
            
            var main = collectParticle.main;
            main.startColor = GetComponent<MeshRenderer>().material.color;
            Instantiate(collectParticle, transform.position, Quaternion.identity);
            
            powerUpSystem.AddPowerUp(storedPowerScript);
            
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0.25f);
            active = false;
        }
    }