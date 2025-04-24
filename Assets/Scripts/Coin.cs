using UnityEngine;
using Player;

namespace Coin
{
    [RequireComponent(typeof(Collider))]
    public class Coin : MonoBehaviour, ICollectable
    {
        private Vector3 startPosition;

        private void Start() => startPosition = transform.position;

        private void Update()
        {
            RotateInPlace();
            
            transform.position = startPosition + Vector3.up * (Mathf.Sin(Time.time * 2) * 0.2f + 0.7f);
        }

        private void RotateInPlace()
        {
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.y += Time.deltaTime * 150;
            transform.rotation = Quaternion.Euler(rotation);
        }
        
        
        #region ICollectable
            public void OnCollect(Transform collector)
            {
                if (collector.TryGetComponent(out PlayerScore playerScore))
                {
                    playerScore.AddScore(1);
                    Destroy(gameObject);
                }
            }

            private void OnTriggerEnter(Collider other)
            {
                OnCollect(other.transform);
            }
        #endregion
    }
}