using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class SimpleEnemyMovement : MonoBehaviour
    {
        public bool canMove = true;
        
        private Collider collider;
        private Rigidbody rb;
        
        [SerializeField] private float speed = 3f;
        [SerializeField] private float ledgeDetectionLength = 0.3f;
        [SerializeField] private float obstacleDetectionLength = 0.3f;
        
        private float xOffset;
        private float yOffset;
    
        [SerializeField] private LayerMask groundLayer;
        private void Reset() => groundLayer = LayerMask.GetMask("Ground");
        private void Start()
        {
            collider = GetComponent<Collider>();
            rb = GetComponent<Rigidbody>();
        
            xOffset = collider.bounds.extents.x;
            yOffset = collider.bounds.extents.y;
        }

        private void Update()
        {
            if (!canMove) return;
            
            Vector3 ledgeDetectionOrigin = transform.position + new Vector3(xOffset * transform.right.x, -yOffset + 0.1f, 0);
            if(!Physics.Raycast(ledgeDetectionOrigin, Vector3.down, ledgeDetectionLength, groundLayer))
                OnLedge();
            
            if (Physics.Raycast(transform.position + (transform.right * xOffset), transform.right, obstacleDetectionLength,
                    groundLayer))
                OnObstacle();
        }

        private void FixedUpdate()
        {
            if (!canMove) return;
            rb.velocity = new Vector3(speed * transform.right.x, rb.velocity.y, rb.velocity.z);
        }

        private void OnLedge()
        {
            transform.right = transform.right.x < 0 ? Vector3.right : Vector3.left;
        }

        private void OnObstacle()
        {
            transform.right = transform.right.x < 0 ? Vector3.right : Vector3.left;
        }
    }
}
