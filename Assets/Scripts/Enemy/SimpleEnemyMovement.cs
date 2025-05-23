using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class SimpleEnemyMovement : MonoBehaviour
    {
        private Collider collider;
        private Rigidbody rb;
        
        [SerializeField] private float speed = 3f;
        [SerializeField] private float ledgeDetectionLenght = 0.3f;
        [SerializeField] private float obstacleDetectionLenght = 0.3f;
        
        private float xOffset;
        private float yOffset;
    
        [SerializeField] private LayerMask groundLayer;
        private void Start()
        {
            collider = GetComponent<Collider>();
            rb = GetComponent<Rigidbody>();
        
            xOffset = collider.bounds.extents.x;
            yOffset = collider.bounds.extents.y;
        }

        private void Update()
        {
            
            Vector3 ledgeDetectionOrigin = transform.position + new Vector3(xOffset * transform.right.x, -yOffset, 0);
            if(!Physics.Raycast(ledgeDetectionOrigin, Vector3.down, ledgeDetectionLenght, groundLayer))
                OnLedge();
            
            if (Physics.Raycast(transform.position + (transform.right * xOffset), transform.right, obstacleDetectionLenght,
                    groundLayer))
                OnObstacle();
        }

        private void FixedUpdate()
        {
            rb.velocity = new Vector3(speed * transform.right.x, rb.velocity.y, rb.velocity.z);
        }

        private void OnLedge()
        {
            Debug.Log("Ledge");
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y > 0 ? 0 : 180, transform.rotation.z);
        }

        private void OnObstacle()
        {
            Debug.Log("Obstacle");
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y > 0 ? 0 : 180, transform.rotation.z);
        }
    }
}
