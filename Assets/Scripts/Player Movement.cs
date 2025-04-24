using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody rb;
        private Collider col;
        private LayerMask groundLayer;

        [SerializeField] private float speed = 10f;
        [SerializeField] private float jumpForce = 30f;
        [SerializeField] private float extraGravity = 85f;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            
            rb.drag = 0f;
            rb.angularDrag = 0f;
            
            col = GetComponent<Collider>();
            groundLayer = LayerMask.GetMask("Ground");
        }

        private void FixedUpdate()
        {
           float horizontalVelocity = Input.GetAxisRaw("Horizontal");
           float verticalVelocity = Input.GetAxisRaw("Vertical");
           
           Vector3 moveInput = new Vector3(horizontalVelocity, 0, verticalVelocity).normalized * speed;
           
           rb.velocity = new Vector3(moveInput.x, rb.velocity.y, moveInput.z);

           if (!IsOnGround())
           {
               rb.AddForce(Vector3.down * extraGravity, ForceMode.Acceleration);
           }
            
        }
        private void Update()
        {

           if (Input.GetKeyDown(KeyCode.Space) && IsOnGround())
           {
                rb.AddForce(0, jumpForce, 0, ForceMode.VelocityChange);
           }
        }
        
        public bool IsOnGround()
        {
            float height = col.bounds.size.y;
            return Physics.Raycast(transform.position, Vector3.down, height * 0.6f, groundLayer);
        }
    }
}
